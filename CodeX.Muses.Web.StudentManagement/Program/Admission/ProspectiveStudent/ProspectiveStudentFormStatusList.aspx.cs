using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using System.Data;
using CodeX.Data.Core.Dal;
using CodeX.Common;
using DevExpress.Web.ASPxCallbackPanel;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ProspectiveStudentFormStatusList : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.PA_PROSPECTIVE_STUDENT_FORM_STATUS;
        }

        protected string GetProsepectiveStudentFilterExpression() 
        {
            string filterExpression = string.Format("PeriodAdmissionID = {0} AND GCRegistrationStatus != '{1}'", AppSession.PeriodAdmissionID, Constant.RegistrationStatus.VOID);
            return filterExpression;
        }

        protected override void InitializeDataControl()
        {
            BindGridView();
        }

        #region Bind Grid View
        List<ProspectiveStudentFolderStatus> lstFolderStatus = null;
        private void BindGridView()
        {
            string filterExpression = string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
            List<vProspectiveStudentFolder> lstEntity = BusinessLayer.GetvProspectiveStudentFolderList(filterExpression);
            
            if (hdnProspectiveStudentID.Value == "") hdnProspectiveStudentID.Value = "0";
            lstFolderStatus = BusinessLayer.GetProspectiveStudentFolderStatusList(String.Format("ProspectiveStudentID = {0}",hdnProspectiveStudentID.Value));
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vProspectiveStudentFolder entity = e.Row.DataItem as vProspectiveStudentFolder;
                ProspectiveStudentFolderStatus entityFolderStatus = lstFolderStatus.FirstOrDefault(x => x.FormID == entity.FormID);
                CheckBox chkIsExist = e.Row.FindControl("chkIsExist") as CheckBox;
                CheckBox chkIsCompleted = e.Row.FindControl("chkIsCompleted") as CheckBox;
                TextBox txtRemarks = e.Row.FindControl("txtRemarks") as TextBox;
                if (entityFolderStatus != null)
                {
                    chkIsExist.Checked = entityFolderStatus.IsExists;
                    chkIsCompleted.Checked = entityFolderStatus.IsCompleted;
                    txtRemarks.Text = entityFolderStatus.Remarks;
                }
                else 
                {
                    chkIsCompleted.Enabled = false;
                }
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";

            if (param[0] == "save")
            {
                if (OnSaveEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool OnSaveEntityDt(ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            ProspectiveStudentFolderStatusDao entityDao = new ProspectiveStudentFolderStatusDao(ctx);
            bool result = true;
            try
            {
                List<String> lstSelectedItem = hdnSelectedValue.Value.Split('|').ToList();
                lstSelectedItem.Remove("");
                List<ProspectiveStudentFolderStatus> lstPsf = BusinessLayer.GetProspectiveStudentFolderStatusList(String.Format("ProspectiveStudentID = {0}",hdnProspectiveStudentID.Value),ctx);
                foreach(String item in lstSelectedItem)
                {
                    String[] temp = item.Split(',').ToArray();
                    String id = temp[0];
                    String IsComplete = temp[1];
                    String remarks = temp[2];

                    ProspectiveStudentFolderStatus entity = lstPsf.FirstOrDefault(x => x.FormID == Convert.ToInt32(id));
                    if (entity == null)
                    {
                        entity = new ProspectiveStudentFolderStatus();
                        entity.ProspectiveStudentID = Convert.ToInt32(hdnProspectiveStudentID.Value);
                        entity.FormID = Convert.ToInt32(id);
                        entity.IsExists = true;
                        entity.IsCompleted = IsComplete == "1" ? true : false;
                        entity.Remarks = remarks;
                        entity.LastUpdatedBy = entity.CreatedBy = AppSession.UserLogin.UserID;
                        entity.LastUpdatedDate = DateTime.Now;
                        entityDao.Insert(entity);
                    }
                    else 
                    {
                        lstPsf.Remove(entity);
                    }
                }
                foreach (ProspectiveStudentFolderStatus obj in lstPsf)
                {
                    entityDao.Delete(obj.ProspectiveStudentID, obj.FormID);
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }

            return result;
        }
        #endregion
    }
}