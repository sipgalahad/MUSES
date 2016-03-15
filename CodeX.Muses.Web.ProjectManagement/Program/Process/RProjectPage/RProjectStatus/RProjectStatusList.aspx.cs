using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Data.Model;
using CodeX.Web.Common;
using CodeX.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxEditors;
using System.Net;
using CodeX.Data.Core.Dal;

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class RProjectStatusList : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.RPROJECT_STATUS;
        }

        public string OnGetMyProjectOrganizationID()
        {
            return Request.Form[hdnMyProjectOrganizationID.UniqueID];
        }

        protected override void InitializeDataControl()
        {
            BindGridView();
        }

        vRProjectOrganizationMember entityOrganizationMember = null;
        #region Bind Grid View
        private void BindGridView()
        {
            if (AppSession.IsMyProject)
            {
                entityOrganizationMember = BusinessLayer.GetvRProjectOrganizationMemberList(string.Format("ProjectID = {0} AND EmployeeID = {1}", AppSession.ProjectID, AppSession.UserLogin.EmployeeID)).FirstOrDefault();
                hdnMyProjectOrganizationID.Value = entityOrganizationMember.ProjectOrganizationID.ToString();
            }
            grdView.DataSource = BusinessLayer.GetvRProjectOrganizationList(string.Format("ProjectID = {0}", AppSession.ProjectID));
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlInputHidden hdnIsAllowAccess = e.Row.FindControl("hdnIsAllowAccess") as HtmlInputHidden;
                if (AppSession.IsMyProject)
                {
                    vRProjectOrganization entity = e.Row.DataItem as vRProjectOrganization;
                    if (entity.DisplayPath.Contains("/" + entityOrganizationMember.ProjectOrganizationID + "/"))
                        hdnIsAllowAccess.Value = "1";
                    else
                        hdnIsAllowAccess.Value = "0";
                }
                else
                    hdnIsAllowAccess.Value = "1";
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion

        #region Bind Grid View
        private void BindGridView2()
        {
            string filterExpression = string.Format("ProjectID = {0} AND IsDeleted = 0", AppSession.ProjectID);
            if (!chkIsShowAllGroup.Checked && hdnProjectOrganizationID.Value != "" && hdnProjectOrganizationID.Value != "0")
                filterExpression += string.Format(" AND ProjectTaskGroupID IN (SELECT ProjectTaskGroupID FROM vRProjectTaskAssign WHERE DisplayPath LIKE '%/{0}/%')", hdnProjectOrganizationID.Value);
            grdView2.DataSource = BusinessLayer.GetRProjectTaskGroupList(filterExpression);
            grdView2.DataBind();
        }

        protected void cbpView2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView2();
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
                if (hdnEntryID.Value.ToString() != "")
                {
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                if (OnDeleteEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity(RProjectTaskGroup entity)
        {
            entity.ProjectTaskGroupName = txtProjectTaskGroupName.Text;
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            RProjectTaskGroupDao entityDao = new RProjectTaskGroupDao(ctx);
            try
            {
                RProjectTaskGroup entity = new RProjectTaskGroup();
                ControlToEntity(entity);
                entity.ProjectID = AppSession.ProjectID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            RProjectTaskGroupDao entityDao = new RProjectTaskGroupDao(ctx);
            try
            {
                RProjectTaskGroup entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                RProjectTaskGroup entity = BusinessLayer.GetRProjectTaskGroup(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateRProjectTaskGroup(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion
    }
}