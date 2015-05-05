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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class SubjectCurriculumMeetingPlanInformationDtCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');

            var subjectID = temp[0];
            hdnSubjectCurriculumID.Value = temp[1];
            hdnCurriculumMeetingPlanID.Value = temp[2];
            hdnParentID.Value = temp[3];
            hdnIsPerSchoolPeriodSection.Value = temp[4];
            hdnCurriculumSchoolPeriodSectionID.Value = temp[5];

            if (hdnIsPerSchoolPeriodSection.Value == "1")
                txtSchoolPeriodSectionName.Text = BusinessLayer.GetCurriculumSchoolPeriodSection(Convert.ToInt32(hdnCurriculumSchoolPeriodSectionID.Value)).CurriculumSchoolPeriodSectionName;
            else
                trSchoolPeriodSection.Style.Add("display", "none");

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
                List<vSubjectCurriculumSyllabus> lstReference = BusinessLayer.GetvSubjectCurriculumSyllabusList(string.Format("SubjectID = {0} AND CurriculumSyllabusID = {1} AND IsDeleted = 0", subjectID, entityDt.CurriculumSyllabusReferenceID));
                Methods.SetComboBoxField<vSubjectCurriculumSyllabus>(cboReferenceID, lstReference, "SubjectCurriculumSyllabusName", "SubjectCurriculumSyllabusID");
            }
            hdnIsUsingCode.Value = entityDt.IsUsingCode ? "1" : "0";

            hdnSubjectCurriculumMeetingPlanID.Value = temp[6];
            hdnIsAdd.Value = "0";

            SubjectCurriculumMeetingPlan entity = BusinessLayer.GetSubjectCurriculumMeetingPlan(Convert.ToInt32(hdnSubjectCurriculumMeetingPlanID.Value));
            txtSubjectCurriculumMeetingPlanCode.Text = entity.SubjectCurriculumMeetingPlanCode;
            txtSubjectCurriculumMeetingPlanName.Text = entity.SubjectCurriculumMeetingPlanName;
            txtRemarks.Text = entity.Remarks;
            cboReferenceID.Value = entity.ReferenceID.ToString();
        }
    }
}