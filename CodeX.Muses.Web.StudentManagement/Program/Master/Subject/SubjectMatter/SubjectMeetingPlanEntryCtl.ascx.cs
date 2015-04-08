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
    public partial class SubjectMeetingPlanEntryCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            SubjectMatterHd entity = BusinessLayer.GetSubjectMatterHd(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.SubjectMatterCode, entity.SubjectMatterName);

            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PERIOD_SECTION));
            Methods.SetComboBoxField<StandardCode>(cboGCPeriodSection, lstSc, "StandardCodeName", "StandardCodeID");
            cboGCPeriodSection.SelectedIndex = 0;

            SubjectCompetencyStandardSummary entitySummary = BusinessLayer.GetSubjectCompetencyStandardSummaryList(string.Format("SubjectMatterID = {0} AND GCPeriodSection = '{1}'", entity.SubjectMatterID, cboGCPeriodSection.Value)).FirstOrDefault();
            txtHeaderText2.Text = entitySummary.SummaryName;

            BindGridView();

            Helper.SetControlEntrySetting(txtMeetingNo, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetSubjectMeetingPlanHdList(string.Format("SubjectMatterID = {0} AND GCPeriodSection = '{1}' AND IsDeleted = 0 ORDER BY MeetingNo ASC", hdnID.Value, cboGCPeriodSection.Value));
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

        private void ControlToEntity(SubjectMeetingPlanHd entity)
        {
            entity.MeetingNo = Convert.ToInt16(txtMeetingNo.Text);
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                SubjectMeetingPlanHd entity = new SubjectMeetingPlanHd();
                ControlToEntity(entity);
                entity.GCPeriodSection = cboGCPeriodSection.Value.ToString();
                entity.SubjectMatterID = Convert.ToInt32(hdnID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertSubjectMeetingPlanHd(entity);
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
                SubjectMeetingPlanHd entity = BusinessLayer.GetSubjectMeetingPlanHd(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSubjectMeetingPlanHd(entity);
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
                SubjectMeetingPlanHd entity = BusinessLayer.GetSubjectMeetingPlanHd(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSubjectMeetingPlanHd(entity);
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