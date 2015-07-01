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
using CodeX.Common;
using CodeX.Data.Core.Dal;

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class ProjectTaskAssignCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            ProjectTask entity = BusinessLayer.GetProjectTask(Convert.ToInt32(hdnID.Value));
            hdnTeamDtID.Value = entity.TeamDtID.ToString();
            txtProjectTaskName.Text = string.Format("{0} - {1}", entity.ProjectTaskCode, entity.ProjectTaskName);

            BindGridView();

            //Helper.SetControlEntrySetting(txtDisplayOrder, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            //Helper.SetControlEntrySetting(txtPosition, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            //Helper.SetControlEntrySetting(tacEmployeeCoordinator, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        protected string OnGetEmployeeFilterExpression()
        {
            string filterExpression = "";
            filterExpression = String.Format("(EmployeeID IN (SELECT EmployeeID FROM TeamDtMember WHERE TeamDtID = {0}) OR EmployeeID = (SELECT EmployeeCoordinatorID FROM TeamDt WHERE TeamDtID = {0}) OR "+
                                             "EmployeeID IN (SELECT EmployeeCoordinatorID FROM TeamDt WHERE ReportTo = {0}) OR EmployeeID IN (SELECT EmployeeID FROM TeamDtMember WHERE TeamDtID IN (SELECT TeamDtID FROM TeamDt WHERE ReportTo = {0}))) AND " +
                                             "EmployeeID NOT IN (SELECT AssigneeID FROM MemberTask WHERE ProjectTaskID = {1}) AND " +
                                             "EmployeeID NOT IN (SELECT ISNULL(OwnerID,0) FROM ProjectTask WHERE ProjectTaskID = {1}) AND " +
                                             "SiteID = '{2}' AND GCEmployeeStatus = '{3}' AND IsDeleted = 0", hdnTeamDtID.Value, hdnID.Value, AppSession.UserLogin.SiteID, Constant.EmployeeStatus.FULL_TIME_EMPLOYED);
            return filterExpression;
        }

        private void BindGridView()
        {
            grdPopupView.DataSource = BusinessLayer.GetvMemberTaskList(string.Format("ProjectTaskID = {0}", hdnID.Value));
            grdPopupView.DataBind();
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        #region Process Detail
        protected void cbpProcessPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        private void ControlToEntity(MemberTask entity)
        {
            entity.ProjectTaskID = Convert.ToInt32(hdnID.Value);
            entity.AssigneeID = Convert.ToInt32(hdnEmployeeCoordinatorID.Value);
            entity.IsAllowChangeStatus = chkIsAllowChangeStatus.Checked;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            try
            {
                MemberTask entity = new MemberTask();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertMemberTask(entity);
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            try
            {
                MemberTask entity = BusinessLayer.GetMemberTask(Convert.ToInt32(hdnID.Value), Convert.ToInt32(hdnEntryID.Value));
                entity.IsAllowChangeStatus = chkIsAllowChangeStatus.Checked;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateMemberTask(entity);
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                BusinessLayer.DeleteMemberTask(Convert.ToInt32(hdnID.Value), Convert.ToInt32(hdnEntryID.Value));
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