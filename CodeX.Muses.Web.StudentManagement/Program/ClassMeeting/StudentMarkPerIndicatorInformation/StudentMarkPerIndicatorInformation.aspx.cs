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
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class StudentMarkPerIndicatorInformation : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.WS_STUDENT_MARK_PER_INDICATOR;
        }

        List<vClassStudentSubjectTaskMark> lstStudentMark = null;
        List<vClassSubjectTaskIndicator> lstIndicator = null;
        List<vClassSubjectTaskIndicator> lstClassSubjectTaskIndicator = null;
        protected override void InitializeDataControl()
        {
            vClassSubject entityClassSubject = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID)).FirstOrDefault();
            txtPassingGrade.Text = entityClassSubject.PassingGrade.ToString();
            if (entityClassSubject.ParentID == 0)
                hdnParentClassSubjectID.Value = entityClassSubject.ClassSubjectID.ToString();
            else
                hdnParentClassSubjectID.Value = entityClassSubject.ParentID.ToString();
            hdnSubjectID.Value = entityClassSubject.SubjectID.ToString();
            hdnSchoolPeriodID.Value = entityClassSubject.SchoolPeriodID.ToString();

            List<vCurriculumSubjectMarkType> lstCurriculumMarkType = BusinessLayer.GetvCurriculumSubjectMarkTypeList(string.Format("CurriculumID = {0} AND SubjectID = {1} AND CurriculumSubjectGroupID = {2} AND IsAllowTask = 1 AND IsDeleted = 0", AppSession.ClassSubject.CurriculumID, hdnSubjectID.Value, entityClassSubject.CurriculumSubjectGroupID));
            Methods.SetComboBoxField<vCurriculumSubjectMarkType>(cboLessonType, lstCurriculumMarkType, "CurriculumMarkTypeName", "CurriculumMarkTypeID");
            cboLessonType.SelectedIndex = 0;

            BindGridView();
        }

        private void BindGridView()
        {
            vPeriodFinalMarkFormula entityFormula = BusinessLayer.GetvPeriodFinalMarkFormulaList(string.Format("SchoolPeriodID = {0} AND CurriculumMarkTypeID = {1}", hdnSchoolPeriodID.Value, cboLessonType.Value)).FirstOrDefault();
            hdnSummaryType.Value = Constant.FinalMarkSummaryType.AVERAGE;
            if (entityFormula != null)
            {
                if (entityFormula.GCFinalMarkSource == Constant.FinalMarkSource.INDICATOR)
                    hdnSummaryType.Value = entityFormula.GCSummaryType;
            }

            lstClassSubjectTaskIndicator = BusinessLayer.GetvClassSubjectTaskIndicatorList(string.Format("ClassSubjectID = {0} AND CurriculumMarkTypeID = {1} AND IsDeleted = 0", AppSession.ClassSubject.ClassSubjectID, cboLessonType.Value));
            lstIndicator = (from p in lstClassSubjectTaskIndicator
                            select new vClassSubjectTaskIndicator { SubjectIndicatorID = p.SubjectIndicatorID, SubjectIndicatorName = p.SubjectIndicatorName, CurriculumMarkTypeID = p.CurriculumMarkTypeID }).GroupBy(p => new { p.SubjectIndicatorID, p.SubjectIndicatorName, p.CurriculumMarkTypeID }).Select(p => p.First()).ToList();

            List<vClassSubjectTask> lstClassSubjectTask = (from p in lstClassSubjectTaskIndicator
                                                           select new vClassSubjectTask { ClassSubjectTaskID = p.ClassSubjectTaskID }).GroupBy(p => new { p.ClassSubjectTaskID }).Select(p => p.First()).ToList();

            if (lstClassSubjectTask.Count > 0)
            {
                string lstClassTaskID = String.Join(",", lstClassSubjectTask.Select(p => p.ClassSubjectTaskID).ToList());
                lstStudentMark = BusinessLayer.GetvClassStudentSubjectTaskMarkList(string.Format("ClassSubjectTaskID IN ({0})", lstClassTaskID));
            }
            else
                lstStudentMark = new List<vClassStudentSubjectTaskMark>();

            rptSubjectIndicatorHeader.DataSource = lstIndicator;
            rptSubjectIndicatorHeader.DataBind();

            ClassSubject classSubject = BusinessLayer.GetClassSubject(AppSession.ClassSubject.ClassSubjectID);
            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", classSubject.SchoolClassID));
            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();

        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }
        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptSubjectIndicator = (Repeater)e.Item.FindControl("rptSubjectIndicator");
                rptSubjectIndicator.DataSource = lstIndicator;
                rptSubjectIndicator.DataBind();
            }
        }

        protected void rptSubjectIndicator_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTaskIndicator indicator = (vClassSubjectTaskIndicator)e.Item.DataItem;
                vClassStudent student = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vClassStudent;

                List<vClassSubjectTaskIndicator> lstClassSubjectTaskIndicator1 = lstClassSubjectTaskIndicator.Where(p => p.SubjectIndicatorID == indicator.SubjectIndicatorID).ToList();
                List<vClassStudentSubjectTaskMark> lstStudentMark1 = lstStudentMark.Where(p => lstClassSubjectTaskIndicator1.Any(q => q.ClassSubjectTaskID == p.ClassSubjectTaskID) && p.StudentID == student.StudentID).ToList();

                HtmlGenericControl divStudentMark = (HtmlGenericControl)e.Item.FindControl("divStudentMark");
                if (lstStudentMark1.Count > 0)
                {
                    if (hdnSummaryType.Value == Constant.FinalMarkSummaryType.AVERAGE)
                        divStudentMark.InnerHtml = lstStudentMark1.Average(p => p.Mark).ToString("N");
                    else
                        divStudentMark.InnerHtml = lstStudentMark1.Max(p => p.Mark).ToString("N");
                }
                else
                    divStudentMark.InnerHtml = "-";
            }
        }
    }
}