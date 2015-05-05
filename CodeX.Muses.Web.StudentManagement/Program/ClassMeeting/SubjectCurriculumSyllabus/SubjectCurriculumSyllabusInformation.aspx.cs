using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using System.Data;
using CodeX.Data.Core.Dal;
using CodeX.Common;
using DevExpress.Web.ASPxCallbackPanel;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class SubjectCurriculumSyllabusInformation : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.WS_SUBJECT_CURRICULUM_SYLLABUS;
        }

        protected string OnGetSubjectCurriculumFilterExpression()
        {
            return string.Format("SubjectID = {0} AND IsDeleted = 0", hdnSubjectID.Value);
        }

        protected override void InitializeDataControl()
        {
            vClassSubject classSubject = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID)).FirstOrDefault();
            hdnSubjectID.Value = classSubject.SubjectID.ToString();

            if (classSubject.SubjectCurriculumID > 0)
            {
                SubjectCurriculum entityHd = BusinessLayer.GetSubjectCurriculum(classSubject.SubjectCurriculumID);
                tacSubjectCurriculum.Value = entityHd.SubjectCurriculumID.ToString();
                tacSubjectCurriculum.Text = entityHd.SubjectCurriculumName;
                tacSubjectCurriculum.Readonly = true;
                hdnCurriculumID.Value = entityHd.CurriculumID.ToString();
                hdnIsPerSchoolPeriodSection.Value = entityHd.IsSyllabusPerSchoolPeriodSection ? "1" : "0";
                if (entityHd.IsSyllabusPerSchoolPeriodSection)
                {
                    trSchoolPeriodSection.Attributes.Remove("style");
                    string optVal = "";
                    List<CurriculumSchoolPeriodSection> lstSchoolPeriodSection = BusinessLayer.GetCurriculumSchoolPeriodSectionList(string.Format("CurriculumID = {0} AND IsDeleted = 0", AppSession.SubjectCurriculumID));
                    foreach (CurriculumSchoolPeriodSection schoolTimeUnit in lstSchoolPeriodSection)
                    {
                        optVal += string.Format("<option value='{0}'>{1}</option>", schoolTimeUnit.CurriculumSchoolPeriodSectionID, schoolTimeUnit.CurriculumSchoolPeriodSectionName);
                    }
                    cboSchoolPeriodSection.InnerHtml = optVal;
                }
                else
                    trSchoolPeriodSection.Attributes.Add("style", "display:none");
            }

            Helper.SetControlEntrySetting(tacSubjectCurriculum, new ControlEntrySetting(true, true, true), "mpFilter");
        }


        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }
    }
}