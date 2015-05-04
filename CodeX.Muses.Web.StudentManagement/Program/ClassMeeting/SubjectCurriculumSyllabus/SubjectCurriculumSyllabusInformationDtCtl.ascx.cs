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
    public partial class SubjectCurriculumSyllabusInformationDtCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');

            var subjectID = temp[0];
            hdnSubjectCurriculumID.Value = temp[1];
            hdnCurriculumSyllabusID.Value = temp[2];
            hdnParentID.Value = temp[3];

            vCurriculumSyllabus entityDt = BusinessLayer.GetvCurriculumSyllabusList(String.Format("CurriculumSyllabusID = {0}", hdnCurriculumSyllabusID.Value)).FirstOrDefault();
            txtType.Text = entityDt.CurriculumSyllabusName;
            if (!entityDt.IsUsingCode)
                trCode.Style.Add("display", "none");
            if (entityDt.ReferenceID == 0)
                trReferenceID.Style.Add("display", "none");
            else
            {
                lblReference.InnerHtml = entityDt.ReferenceName;
                List<vSubjectCurriculumSyllabus> lstReference = BusinessLayer.GetvSubjectCurriculumSyllabusList(string.Format("SubjectID = {0} AND CurriculumSyllabusID = {1} AND IsDeleted = 0", subjectID, entityDt.ReferenceID));
                Methods.SetComboBoxField<vSubjectCurriculumSyllabus>(cboReferenceID, lstReference, "SubjectCurriculumSyllabusCode", "SubjectCurriculumSyllabusID");
            }
            hdnIsUsingCode.Value = entityDt.IsUsingCode ? "1" : "0";

            hdnSubjectCurriculumSyllabusID.Value = temp[4];
            hdnIsAdd.Value = "0";

            SubjectCurriculumSyllabus entity = BusinessLayer.GetSubjectCurriculumSyllabus(Convert.ToInt32(hdnSubjectCurriculumSyllabusID.Value));
            txtSubjectCurriculumSyllabusCode.Text = entity.SubjectCurriculumSyllabusCode;
            txtSubjectCurriculumSyllabusName.Text = entity.SubjectCurriculumSyllabusName;
            txtRemarks.Text = entity.Remarks;
            cboReferenceID.Value = entity.ReferenceID.ToString();
        }
    }
}