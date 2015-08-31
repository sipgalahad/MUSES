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
    public partial class ProjectTaskBudgetEntryCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            string[] data = param.Split('|');
            hdnID.Value = data[0];
            hdnProjectID.Value = data[1];
            vProjectTask entity = BusinessLayer.GetvProjectTaskList(String.Format("ProjectTaskID = {0}",Convert.ToInt32(hdnID.Value)))[0];
            txtHeaderText.Text = string.Format("{0} - {1}", entity.ProjectTaskCode, entity.ProjectTaskName);
            txtPTStartDate.Text = entity.StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtPTStartTime.Text = entity.StartTime;
            txtPTEndDate.Text = entity.EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtPTEndTime.Text = entity.EndTime;
            txtPTRemarks.Text = entity.CustomRemarks;

            BindGridView();

            //Helper.SetControlEntrySetting(txtNoteName, new ControlEntrySetting(true, false, true), "mpTrxPopup");
            //Helper.SetControlEntrySetting(txtStartDate, new ControlEntrySetting(true, false, true), "mpTrxPopup");
            //Helper.SetControlEntrySetting(txtStartTime, new ControlEntrySetting(true, false, true), "mpTrxPopup");
            //Helper.SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false), "mpTrxPopup");
        }

        #region HTML Getter
        protected string OnGetProjectBudgetFilterExpression()
        {
            return string.Format("ProjectID = {0} AND ItemID IS NULL", AppSession.ProjectID);
        }
        #endregion

        private string OnGetFilterExpression() 
        {
            String filterExpression = string.Format("ProjectTaskID = {0} AND IsDeleted = 0", hdnID.Value);
            return filterExpression;
        }

        private void BindGridView()
        {
            String filterExpression = OnGetFilterExpression();
            grdView.DataSource = BusinessLayer.GetvProjectTaskBudgetList(filterExpression);
            grdView.DataBind();
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

        private void ControlToEntity(ProjectTaskBudget entity)
        {
            entity.BudgetDtID = Convert.ToInt32(hdnBudgetDtID.Value);
            entity.UsedBudget = Convert.ToDecimal(txtUsedAmount.Text);
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            
            try
            {
                ProjectTaskBudget entity = new ProjectTaskBudget();
                ControlToEntity(entity);
                entity.ProjectTaskID = Convert.ToInt32(hdnID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertProjectTaskBudget(entity);
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
                ProjectTaskBudget entity = BusinessLayer.GetProjectTaskBudget(Convert.ToInt32(hdnEntryID.Value),Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateProjectTaskBudget(entity);
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
                BusinessLayer.DeleteProjectTaskBudget(Convert.ToInt32(hdnEntryID.Value), Convert.ToInt32(hdnID.Value));
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