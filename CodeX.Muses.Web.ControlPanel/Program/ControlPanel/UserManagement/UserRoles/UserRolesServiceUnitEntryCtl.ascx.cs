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

namespace CodeX.Web.ControlPanel.Program
{
    public partial class UserRolesServiceUnitEntryCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        public override void InitializeDataControl(string param)
        {
            hdnRoleID.Value = param;
            UserRole entity = BusinessLayer.GetUserRole(Convert.ToInt32(hdnRoleID.Value));
            txtRoleName.Text = entity.RoleName;

            List<Site> lstSite = BusinessLayer.GetSiteList("IsHeader = 0");
            Methods.SetComboBoxField<Site>(cboSite, lstSite, "SiteName", "SiteID");
            cboSite.SelectedIndex = 0;

            List<Department> lstDepartment = BusinessLayer.GetDepartmentList("IsActive = 1");
            Methods.SetComboBoxField<Department>(cboDepartment, lstDepartment, "DepartmentName", "DepartmentID");
            cboDepartment.SelectedIndex = 0;

            List<Int32> lstServiceUnitID = BusinessLayer.GetServiceUnitUserRoleSiteServiceUnitIDList(string.Format("RoleID = {0} AND IsDeleted = 0", hdnRoleID.Value));
            hdnOldSelectedServiceUnit.Value = hdnSelectedServiceUnit.Value = String.Join(",", lstServiceUnitID.ToArray());

            BindGridView(1, true, ref PageCount);
        }


        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = string.Format("SiteID = '{0}' AND DepartmentID = '{1}' AND IsDeleted = 0", cboSite.Value, cboDepartment.Value);

            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvSiteServiceUnitRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 8);
            }
            lstSelectedServiceUnit = hdnSelectedServiceUnit.Value.Split(',');
            List<vSiteServiceUnit> lstEntity = BusinessLayer.GetvSiteServiceUnitList(filterExpression, 8, pageIndex);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        private string[] lstSelectedServiceUnit = null;
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                    e.Row.Cells[i].Text = GetLabel(e.Row.Cells[i].Text);
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vSiteServiceUnit entity = e.Row.DataItem as vSiteServiceUnit;

                CheckBox chkServiceUnit = (CheckBox)e.Row.FindControl("chkServiceUnit");
                if (lstSelectedServiceUnit.Contains(entity.SiteServiceUnitID.ToString()))
                    chkServiceUnit.Checked = true;
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
            List<string> lstOldServiceUnit = new List<string>(hdnOldSelectedServiceUnit.Value.Split(','));
            List<string> listSelectedServiceUnit = new List<string>(hdnSelectedServiceUnit.Value.Split(','));

            int roleID = Convert.ToInt32(hdnRoleID.Value);

            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ServiceUnitUserRoleDao entityDao = new ServiceUnitUserRoleDao(ctx);
            try
            {
                foreach (String oldData in lstOldServiceUnit)
                {
                    if (!listSelectedServiceUnit.Contains(oldData))
                    {
                        ServiceUnitUserRole entity = BusinessLayer.GetServiceUnitUserRoleList(string.Format("RoleID = {0} AND ServiceUnitID = {1} AND IsDeleted = 0", roleID, oldData), ctx)[0];
                        entity.IsDeleted = true;
                        entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDao.Update(entity);
                    }
                }
                foreach (String newData in listSelectedServiceUnit)
                {
                    if (!lstOldServiceUnit.Contains(newData))
                    {
                        ServiceUnitUserRole entity = new ServiceUnitUserRole();
                        entity.RoleID = roleID;
                        entity.SiteServiceUnitID = Convert.ToInt32(newData);
                        entity.CreatedBy = AppSession.UserLogin.UserID;
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