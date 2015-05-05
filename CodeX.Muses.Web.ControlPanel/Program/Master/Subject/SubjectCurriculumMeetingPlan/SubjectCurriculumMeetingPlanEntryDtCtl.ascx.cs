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

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class SubjectCurriculumMeetingPlanEntryDtCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');

            hdnSubjectCurriculumID.Value = temp[1];
            hdnCurriculumMeetingPlanID.Value = temp[2];
            hdnParentID.Value = temp[3];
            hdnIsPerSchoolTimeUnit.Value = temp[4];
            hdnCurriculumSchoolTimeUnitID.Value = temp[5];

            if (hdnIsPerSchoolTimeUnit.Value == "1")
                txtSchoolTimeUnitName.Text = BusinessLayer.GetCurriculumSchoolTimeUnit(Convert.ToInt32(hdnCurriculumSchoolTimeUnitID.Value)).CurriculumSchoolTimeUnitName;
            else
                trSchoolTimeUnit.Style.Add("display", "none");

            vCurriculumMeetingPlan entityDt = BusinessLayer.GetvCurriculumMeetingPlanList(String.Format("CurriculumMeetingPlanID = {0}", hdnCurriculumMeetingPlanID.Value)).FirstOrDefault();
            txtType.Text = entityDt.CurriculumMeetingPlanName;
            if (!entityDt.IsUsingCode)
                trCode.Style.Add("display", "none");
            if (entityDt.CurriculumSyllabusReferenceID == 0)
                trReferenceID.Style.Add("display", "none");
            else
            {
                trName.Style.Add("display", "none");
                lblReference.InnerHtml = entityDt.CurriculumSyllabusReferenceName;
                List<vSubjectCurriculumSyllabus> lstReference = BusinessLayer.GetvSubjectCurriculumSyllabusList(string.Format("SubjectID = {0} AND CurriculumSyllabusID = {1} AND IsDeleted = 0", AppSession.SubjectID, entityDt.CurriculumSyllabusReferenceID));
                Methods.SetComboBoxField<vSubjectCurriculumSyllabus>(cboReferenceID, lstReference, "SubjectCurriculumSyllabusName", "SubjectCurriculumSyllabusID");
            }
            hdnIsUsingCode.Value = entityDt.IsUsingCode ? "1" : "0";

            if (temp[0] == "edit")
            {
                hdnSubjectCurriculumMeetingPlanID.Value = temp[6];
                hdnIsAdd.Value = "0";

                SubjectCurriculumMeetingPlan entity = BusinessLayer.GetSubjectCurriculumMeetingPlan(Convert.ToInt32(hdnSubjectCurriculumMeetingPlanID.Value));
                txtSubjectCurriculumMeetingPlanCode.Text = entity.SubjectCurriculumMeetingPlanCode;
                txtSubjectCurriculumMeetingPlanName.Text = entity.SubjectCurriculumMeetingPlanName;
                txtRemarks.Text = entity.Remarks;
                cboReferenceID.Value = entity.ReferenceID.ToString();
            }
            else
            {
                hdnSubjectCurriculumMeetingPlanID.Value = "0";
            }

            Helper.SetControlEntrySetting(txtSubjectCurriculumMeetingPlanCode, new ControlEntrySetting(true, true, true), "mpEntryPopup");
            Helper.SetControlEntrySetting(txtSubjectCurriculumMeetingPlanName, new ControlEntrySetting(true, true, true), "mpEntryPopup");
            Helper.SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false), "mpEntryPopup");
        }

        protected void cbpEntryPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            LoadWords();

            string param = e.Parameter;

            string result = param + "|";
            string errMessage = "";

            if (hdnIsAdd.Value.ToString() == "0")
            {
                if (OnSaveEditRecord(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else
            {
                if (OnSaveAddRecord(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;

        }

        #region CRUD Process Method
        private void ControlToEntity(SubjectCurriculumMeetingPlan entity)
        {
            entity.SubjectCurriculumMeetingPlanCode = txtSubjectCurriculumMeetingPlanCode.Text;
            entity.SubjectCurriculumMeetingPlanName = txtSubjectCurriculumMeetingPlanName.Text;
            if (cboReferenceID.Value != null && cboReferenceID.Value.ToString() != "")
                entity.ReferenceID = Convert.ToInt32(cboReferenceID.Value);
            else
                entity.ReferenceID = null;
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SubjectCurriculumMeetingPlanDao entityDao = new SubjectCurriculumMeetingPlanDao(ctx);
            try
            {
                SubjectCurriculumMeetingPlan entity = new SubjectCurriculumMeetingPlan();
                ControlToEntity(entity);
                if (hdnParentID.Value != "")
                    entity.ParentID = Convert.ToInt32(hdnParentID.Value);
                else
                    entity.ParentID = null;
                if (hdnIsPerSchoolTimeUnit.Value == "1")
                    entity.CurriculumSchoolTimeUnitID = Convert.ToInt32(hdnCurriculumSchoolTimeUnitID.Value);
                else
                    entity.CurriculumSchoolTimeUnitID = null;
                entity.SubjectCurriculumID = Convert.ToInt32(hdnSubjectCurriculumID.Value);
                entity.CurriculumMeetingPlanID = Convert.ToInt32(hdnCurriculumMeetingPlanID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                result = false;
                errMessage = ex.Message;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnSaveEditRecord(ref string errMessage)
        {
            try
            {
                SubjectCurriculumMeetingPlan entity = BusinessLayer.GetSubjectCurriculumMeetingPlan(Convert.ToInt32(hdnSubjectCurriculumMeetingPlanID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSubjectCurriculumMeetingPlan(entity);
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