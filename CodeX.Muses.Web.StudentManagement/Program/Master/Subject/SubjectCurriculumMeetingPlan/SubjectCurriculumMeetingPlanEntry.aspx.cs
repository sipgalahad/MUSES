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
    public partial class SubjectCurriculumMeetingPlanEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            if (AppSession.SubjectMatterID > 0)
                return Constant.MenuCode.StudentManagement.SBM_SUBJECT_MEETING_PLAN;
            return Constant.MenuCode.StudentManagement.SB_SUBJECT_MEETING_PLAN;
        }

        protected string OnGetSubjectCurriculumFilterExpression()
        {
            return string.Format("SubjectID = {0} AND IsDeleted = 0", AppSession.SubjectID);
        }

        protected override void InitializeDataControl()
        {
            hdnSubjectID.Value = AppSession.SubjectID.ToString();

            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PERIOD_SECTION));
            Methods.SetComboBoxField<StandardCode>(cboGCPeriodSection, lstSc, "StandardCodeName", "StandardCodeID");
            cboGCPeriodSection.SelectedIndex = 0;

            //if (AppSession.SubjectMatterID > 0)
            //{
            //    SubjectCurriculum entityHd = BusinessLayer.GetSubjectCurriculum(AppSession.SubjectMatterID);
            //    tacSubjectCurriculum.Value = entityHd.SubjectCurriculumID.ToString();
            //    tacSubjectCurriculum.Text = entityHd.SubjectCurriculumName;
            //    tacSubjectCurriculum.Readonly = true;
            //}

            Helper.SetControlEntrySetting(tacSubjectCurriculum, new ControlEntrySetting(true, true, true), "mpFilter");
            Helper.SetControlEntrySetting(cboGCPeriodSection, new ControlEntrySetting(true, true, true), "mpFilter");
        }


        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            LoadWords();

            string[] param = e.Parameter.Split('|');

            string result = "";
            string errMessage = "";
            if (OnDeleteRecord(ref errMessage))
                result += "success";
            else
                result += string.Format("fail|{0}", errMessage);

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;

        }

        private bool OnDeleteRecord(ref string errMessage)
        {
            try
            {
                SubjectCurriculumMeetingPlan entity = BusinessLayer.GetSubjectCurriculumMeetingPlan(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
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
    }
}