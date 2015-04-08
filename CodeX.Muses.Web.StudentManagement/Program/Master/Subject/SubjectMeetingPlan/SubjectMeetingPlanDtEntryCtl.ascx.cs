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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class SubjectMeetingPlanDtEntryCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');
            hdnID.Value = temp[0];
            SubjectMeetingPlanHd entity = BusinessLayer.GetSubjectMeetingPlanHd(Convert.ToInt32(hdnID.Value));
            txtMeetingNo.Text = entity.MeetingNo.ToString();

            string filterExpression = "";
            if (temp[1] == "1")
                filterExpression = String.Format("ParentID = '{0}' AND StandardCodeID < '{0}^020' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SUBJECT_MEETING_PLAN_DT_TYPE);
            else
                filterExpression = String.Format("ParentID = '{0}' AND StandardCodeID > '{0}^020' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SUBJECT_MEETING_PLAN_DT_TYPE);
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(filterExpression);
            Methods.SetComboBoxField<StandardCode>(cboGCSubjectMeetingPlanDtType, lstSc, "StandardCodeName", "StandardCodeID");
            cboGCSubjectMeetingPlanDtType.SelectedIndex = 0;

            BindGridView();

            Helper.SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false), "mpTrxPopup");
        }

        private void BindGridView()
        {
            string filterExpression = string.Format("SubjectMeetingPlanHdID = {0} AND GCSubjectMeetingPlanDtType = '{1}'", hdnID.Value, cboGCSubjectMeetingPlanDtType.Value);
            grdView.DataSource = BusinessLayer.GetSubjectMeetingPlanDtList(filterExpression);
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

        private void ControlToEntity(SubjectMeetingPlanDt entity)
        {
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                SubjectMeetingPlanDt entity = new SubjectMeetingPlanDt();
                ControlToEntity(entity);
                entity.GCSubjectMeetingPlanDtType = cboGCSubjectMeetingPlanDtType.Value.ToString();
                entity.SubjectMeetingPlanHdID = Convert.ToInt32(hdnID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertSubjectMeetingPlanDt(entity);
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
                SubjectMeetingPlanDt entity = BusinessLayer.GetSubjectMeetingPlanDt(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSubjectMeetingPlanDt(entity);
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
                SubjectMeetingPlanDt entity = BusinessLayer.GetSubjectMeetingPlanDt(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSubjectMeetingPlanDt(entity);
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