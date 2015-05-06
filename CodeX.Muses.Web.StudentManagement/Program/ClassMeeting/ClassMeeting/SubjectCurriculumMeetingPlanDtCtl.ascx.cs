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
    public partial class SubjectCurriculumMeetingPlanDtCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnCurriculumID.Value = AppSession.ClassSubject.CurriculumID.ToString();
            string[] temp = param.Split('|');

            hdnID.Value = temp[0];
            hdnSubjectID.Value = temp[1];
            vSubjectCurriculumMeetingPlan entity = BusinessLayer.GetvSubjectCurriculumMeetingPlanList(string.Format("SubjectCurriculumMeetingPlanID = {0}", hdnID.Value)).FirstOrDefault();
            txtMeetingNo.Text = entity.SubjectCurriculumMeetingPlanName.ToString();
            hdnCurriculumMeetingPlanID.Value = entity.CurriculumMeetingPlanID.ToString();

            if (entity.ParentID > 0)
                hdnParentSubjectCurriculumMeetingPlanID.Value = entity.ParentID.ToString();
            else
                divGroup.Style.Add("display", "none");
        }
    }
}