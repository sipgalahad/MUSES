using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class UserReportEntryCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');
            hdnUserID.Value = temp[0];
            hdnSiteID.Value = temp[1];

            Site entitySite = BusinessLayer.GetSite(hdnSiteID.Value);
            txtSiteName.Text = entitySite.SiteName;
            User entity = BusinessLayer.GetUser(Convert.ToInt32(hdnUserID.Value));
            txtUserName.Text = entity.UserName;

            List<Module> lstModule = BusinessLayer.GetModuleList(string.Format("ModuleID != '{0}'", Constant.Module.CONTROL_PANEL_HQ));
            Methods.SetComboBoxField<Module>(cboModule, lstModule, "ModuleName", "ModuleID");
            cboModule.SelectedIndex = 0;

            List<StandardCode> lstReportType = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.REPORT_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboReportType, lstReportType, "StandardCodeName", "StandardCodeID");
            cboReportType.SelectedIndex = 0;

            List<UserReport> lstReport = BusinessLayer.GetUserReportList(string.Format("UserID = {0} AND SiteID = '{1}'", hdnUserID.Value, hdnSiteID.Value));
            hdnOldSelectedReport.Value = hdnSelectedReport.Value = String.Join(",", lstReport.Select(p => p.ReportID).ToList());

            BindGridView(1, true, ref PageCount);
        }


        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = string.Format("ModuleID = '{0}' AND GCReportType = '{1}' AND IsDeleted = 0", cboModule.Value, cboReportType.Value);
            filterExpression += string.Format(" AND ReportID IN (SELECT ReportID FROM UserRoleReport WHERE RoleID IN (SELECT RoleID FROM UserInRole WHERE SiteID = '{0}' AND UserID = {1}) AND IsDeleted = 0)", hdnSiteID.Value, hdnUserID.Value);
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvReportMasterRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 8);
            }
            lstSelectedReport = hdnSelectedReport.Value.Split(',');
            List<vReportMaster> lstEntity = BusinessLayer.GetvReportMasterList(filterExpression, 8, pageIndex);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        private string[] lstSelectedReport = null;
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                    e.Row.Cells[i].Text = GetLabel(e.Row.Cells[i].Text);
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vReportMaster entity = e.Row.DataItem as vReportMaster;

                CheckBox chkReport = (CheckBox)e.Row.FindControl("chkReport");
                if (lstSelectedReport.Contains(entity.ReportID.ToString()))
                    chkReport.Checked = true;
            }
        }

        protected void cbpEntryPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            LoadWords();
            int pageCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount);
                    result = "changepage";
                }
                else // refresh
                {

                    BindGridView(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool SaveData(ref string errMessage)
        {
            List<string> lstOldReport = new List<string>(hdnOldSelectedReport.Value.Split(','));
            List<string> listSelectedReport = new List<string>(hdnSelectedReport.Value.Split(','));

            int roleID = Convert.ToInt32(hdnUserID.Value);

            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            UserReportDao entityDao = new UserReportDao(ctx);
            try
            {
                foreach (String oldData in lstOldReport)
                {
                    if (!listSelectedReport.Contains(oldData))
                    {
                        BusinessLayer.DeleteUserReport(roleID, hdnSiteID.Value, Convert.ToInt32(oldData));
                    }
                }
                foreach (String newData in listSelectedReport)
                {
                    if (!lstOldReport.Contains(newData))
                    {
                        UserReport entity = new UserReport();
                        entity.UserID = roleID;
                        entity.SiteID = hdnSiteID.Value;
                        entity.ReportID = Convert.ToInt32(newData);
                        entityDao.Insert(entity);
                    }
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected void cbpViewPopupProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "save")
                {
                    string errMessage = "";
                    if (SaveData(ref errMessage))
                        result = "save|success";
                    else
                        result = "save|fail|" + errMessage;
                }
            }
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
    }
}