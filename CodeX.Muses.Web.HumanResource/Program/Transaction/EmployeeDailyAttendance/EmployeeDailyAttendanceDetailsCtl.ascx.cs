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
    public partial class EmployeeDailyAttendanceDetailsCtl : BaseViewPopupCtl
    {

        public override void InitializeDataControl(string param)
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.HR_DAILY_SCHEDULE_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboDailyScheduleType, lstSc, "StandardCodeName", "StandardCodeID");
            string[] lstParam = param.Split('|');
            hdnID.Value = lstParam[0];
            hdnTanggal.Value = Helper.GetDatePickerValue(lstParam[1]).ToString("yyyyMMdd");
            Employee entity = BusinessLayer.GetEmployee(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = entity.FullName.ToString();
            txtTanggal.Text = lstParam[1].ToString();
            BindGridView();

            Helper.SetControlEntrySetting(txtFromHour, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtToHour, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(cboDailyScheduleType, new ControlEntrySetting(true, true, true), "mpTrxPopup");
  
        }

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetEmployeeFingerprintLogList(string.Format("EmployeeID = {0} AND CONVERT(DATE,LogDateTime) = '{1}' ORDER BY LogDateTime ASC", hdnID.Value, hdnTanggal.Value));
            grdView.DataBind();
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        #region Process Detail
        protected void cbpProcessPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            //string result = "";
            //string errMessage = "";
            //string[] param = e.Parameter.Split('|');
            //result = param[0] + "|";
            //if (param[0] == "save")
            //{
            //    if (hdnEntryID.Value.ToString() != "")
            //    {
            //        if (OnSaveEditRecordEntityDt(ref errMessage))
            //            result += "success";
            //        else
            //            result += string.Format("fail|{0}", errMessage);
            //    }
            //    else
            //    {
            //        if (OnSaveAddRecordEntityDt(ref errMessage))
            //            result += "success";
            //        else
            //            result += string.Format("fail|{0}", errMessage);
            //    }
            //}
            //else if (param[0] == "delete")
            //{
            //    if (OnDeleteEntityDt(ref errMessage))
            //        result += "success";
            //    else
            //        result += string.Format("fail|{0}", errMessage);
            //}

            //ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            //panel.JSProperties["cpResult"] = result;
        }

       
        #endregion
    }
}