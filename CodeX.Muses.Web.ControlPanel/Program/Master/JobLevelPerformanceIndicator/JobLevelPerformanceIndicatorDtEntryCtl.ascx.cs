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

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class JobLevelPerformanceIndicatorDtEntryCtl : BaseViewPopupCtl
    {
        protected string OnGetEmployeeFilterExpression()
        {
            return string.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }

        #region Html Getter
        protected string OnGetGCScheduleTypeFromComponent()
        {
            return Constant.RenumerationSheduleType.FIXED;
        }
        #endregion

        public override void InitializeDataControl(string param)
        {
            
            hdnID.Value = param;
            JobLevelPerformanceIndicator entity = BusinessLayer.GetJobLevelPerformanceIndicatorList(String.Format("JobLevelPerformanceIndicatorID = {0} ", Convert.ToInt32(hdnID.Value))).FirstOrDefault();
            txtHeaderText.Text = String.Format("{0}", entity.JobLevelPerformanceIndicatorName);

            BindGridView();

            Helper.SetControlEntrySetting(tacJobLevelID, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            
  
        }

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetvJobLevelPerformanceIndicatorDtList(string.Format("JobLevelPerformanceIndicatorID = {0}  ", hdnID.Value));
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

        private void ControlToEntity(JobLevelPerformanceIndicatorDt entity)
        {
            entity.JobLevelID = Convert.ToInt32(tacJobLevelID.Text);
           
            
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                JobLevelPerformanceIndicatorDt entity = new JobLevelPerformanceIndicatorDt();
                ControlToEntity(entity);
                entity.JobLevelPerformanceIndicatorID = Convert.ToInt32(hdnID.Value);
                BusinessLayer.InsertJobLevelPerformanceIndicatorDt(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            try
            {
                JobLevelPerformanceIndicatorDt entity = BusinessLayer.GetJobLevelPerformanceIndicatorDt(Convert.ToInt32(hdnID.Value),Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                BusinessLayer.UpdateJobLevelPerformanceIndicatorDt(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                BusinessLayer.DeleteJobLevelPerformanceIndicatorDt(Convert.ToInt32(hdnID.Value), Convert.ToInt32(hdnEntryID.Value));
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