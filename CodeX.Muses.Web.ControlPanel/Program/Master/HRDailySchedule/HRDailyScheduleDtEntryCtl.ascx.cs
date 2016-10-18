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
    public partial class HRDailyScheduleDtEntryCtl : BaseViewPopupCtl
    {

        public override void InitializeDataControl(string param)
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.HR_DAILY_SCHEDULE_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboDailyScheduleType, lstSc, "StandardCodeName", "StandardCodeID");

            hdnID.Value = param;
            HRDailyScheduleHd entity = BusinessLayer.GetHRDailyScheduleHd(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.DailyScheduleCode, entity.DailyScheduleName);

            BindGridView();

            Helper.SetControlEntrySetting(txtFromHour, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtToHour, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(cboDailyScheduleType, new ControlEntrySetting(true, true, true), "mpTrxPopup");
  
        }

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetvHRDailyScheduleDtList(string.Format("DailyScheduleID = {0} AND IsDeleted = 0 ORDER BY FromHour ASC", hdnID.Value));
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

        private void ControlToEntity(HRDailyScheduleDt entity)
        {
            entity.FromHour = txtFromHour.Text;
            entity.ToHour = txtToHour.Text;
            entity.GCDailyScheduleType = cboDailyScheduleType.Value.ToString();
            //if (entity.GCDailyScheduleType == Constant.SchoolDailyScheduleType.KBM)
            //    entity.HoursIndex = Convert.ToInt16(txtHoursIndex.Text);
            //else
            //    entity.HoursIndex = 0;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                HRDailyScheduleDt entity = new HRDailyScheduleDt();
                ControlToEntity(entity);
                entity.DailyScheduleID = Convert.ToInt32(hdnID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertHRDailyScheduleDt(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            try
            {
                HRDailyScheduleDt entity = BusinessLayer.GetHRDailyScheduleDt(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateHRDailyScheduleDt(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                HRDailyScheduleDt entity = BusinessLayer.GetHRDailyScheduleDt(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateHRDailyScheduleDt(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion
    }
}