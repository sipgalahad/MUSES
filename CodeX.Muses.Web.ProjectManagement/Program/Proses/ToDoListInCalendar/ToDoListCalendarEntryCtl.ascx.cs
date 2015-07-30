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
    public partial class ToDoListCalendarEntryCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            //hdnEntryID.Value = param == "" ? "0" : param;
            String[] data = param.Split('|');
            if (data[0] != "")
                hdnEntryID.Value = data[0];
            else
                hdnEntryID.Value = "";

            hdnEmployeeCoordinatorID.Value = AppSession.UserLogin.EmployeeID.ToString();
            String ProjectFilterExpression = "";
            List<Project> lstProject = BusinessLayer.GetProjectList(ProjectFilterExpression);
            Methods.SetComboBoxField(cboProject, lstProject, "ProjectName", "ProjectID");
            cboProject.SelectedIndex = 0;

            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(String.Format("IsDeleted = 0 AND IsActive = 1 AND ParentID = '{0}'", Constant.StandardCode.PROJECT_TASK_PRIORITY));
            Methods.SetComboBoxField(cboPriority, lstStandardCode, "StandardCodeName", "StandardCodeID");
            cboPriority.SelectedIndex = 0;

            if (hdnEntryID.Value != "")
            {
                vProjectTask entity = BusinessLayer.GetvProjectTaskList(String.Format("ProjectTaskID = {0}", Convert.ToInt32(hdnEntryID.Value)))[0];
                cboProject.ClientEnabled = false;
                EntityToControl(entity);
            }
            else 
            {
                txtStartDateDt.Text = data[1];
                txtStartTimeDt.Text = DateTime.Now.ToString("HH:mm");
                txtEndDate.Text = data[1];
                txtEndTime.Text = DateTime.Now.ToString("HH:mm");
            }
            
            if (AppSession.UserLogin.EmployeeID != null && AppSession.UserLogin.EmployeeID != 0)
            {
                ProjectFilterExpression = String.Format("GCProjectStatus NOT IN ('{0}','{1}') AND " +
                                          "ProjectID IN (SELECT ProjectID FROM vTeamDt WHERE EmployeeCoordinatorID = '{2}' OR ListEmployeeID1 LIKE '%;{2};%')", Constant.ProjectStatus.CANCELED, Constant.ProjectStatus.COMPLETE, AppSession.UserLogin.EmployeeID);
            }
            else
            {
                ProjectFilterExpression = String.Format("GCProjectStatus NOT IN ('{0}','{1}')", Constant.ProjectStatus.CANCELED, Constant.ProjectStatus.COMPLETE);
            }

            #region Project Task Scheduled
            Helper.SetControlEntrySetting(txtProjectTaskCode, new ControlEntrySetting(true, false, true), "mpTrx");
            Helper.SetControlEntrySetting(txtProjectTaskName, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboPriority, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(tacTeamDt, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtStartDateDt, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtStartTimeDt, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtEndDate, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtEndTime, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtRemarksDt, new ControlEntrySetting(true, true, false), "mpTrx");
            #endregion
        }

        //protected override void OnControlEntrySetting()
        //{

        //    base.OnControlEntrySetting();
        //}

        private void EntityToControl(vProjectTask entity) 
        {
            txtProjectTaskCode.Text = entity.ProjectTaskCode;
            txtProjectTaskName.Text = entity.ProjectTaskName;
            txtStartDateDt.Text = entity.StartDateInDatePicker;
            txtStartTimeDt.Text = entity.StartTime;
            txtEndDate.Text = entity.EndDateInDatePicker;
            txtEndTime.Text = entity.EndTime;
            hdnTeamDtID.Value = tacTeamDt.Value = entity.TeamDtID.ToString();
            tacTeamDt.Text = entity.Position;
            cboPriority.Value = entity.GCProjectTaskPriority;
            txtRemarksDt.Text = entity.Remarks;

        }

        #region ProjectTaskDt
        private void ControlToEntity(ProjectTask entity) 
        {
            entity.ProjectTaskCode = txtProjectTaskCode.Text;
            entity.ProjectTaskName = txtProjectTaskName.Text;
            entity.ProjectID = Convert.ToInt32(cboProject.Value);
            entity.TeamDtID = Convert.ToInt32(hdnTeamDtID.Value);
            entity.StartDate = Helper.GetDatePickerValue(txtStartDateDt.Text);
            entity.StartTime = txtStartTimeDt.Text;
            entity.EndDate = Helper.GetDatePickerValue(txtEndDate.Text);
            entity.EndTime = txtEndTime.Text;
            entity.GCProjectTaskType = Constant.ProjectTaskType.SCHEDULED;
            if (AppSession.UserLogin.EmployeeID != null && AppSession.UserLogin.EmployeeID != 0)
                entity.OwnerID = AppSession.UserLogin.EmployeeID;
            else
                entity.OwnerID = null;
            entity.GCProjectTaskPriority = cboPriority.Value.ToString();
            entity.Remarks = txtRemarksDt.Text;
        }

        private bool OnSaveEditEntityDt(ref String errMessage)
        {
            bool result = true;
            try
            {
                ProjectTask entity = BusinessLayer.GetProjectTask(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateProjectTask(entity);
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }

        private bool OnSaveAddEntityDt(ref String errMessage)
        {
            bool result = true;
            try
            {
                ProjectTask entity = new ProjectTask();
                ControlToEntity(entity);
                entity.GCProjectTaskStatus = Constant.ProjectTaskStatus.OPEN;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertProjectTask(entity);
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }

        private bool OnDeleteEntityDt(ref String errMessage)
        {
            bool result = true;
            try
            {
                ProjectTask entity = BusinessLayer.GetProjectTask(Convert.ToInt32(hdnEntryID.Value));
                entity.GCProjectTaskStatus = Constant.ProjectTaskStatus.VOID;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateProjectTask(entity);
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }
        #endregion

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
                    if (OnSaveEditEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddEntityDt(ref errMessage))
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
        #endregion
    }
}