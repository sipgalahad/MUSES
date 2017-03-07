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
    public partial class SchoolClassMarkPerIndicatorInformation : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            string id = Request.QueryString["id"];
            if (id == "cs")
                return Constant.MenuCode.StudentManagement.SC_STUDENT_MARK_PER_INDICATOR;
            return Constant.MenuCode.StudentManagement.MTSC_STUDENT_MARK_PER_INDICATOR;
        }

        List<vClassStudentSubjectTaskMark> lstStudentMark = null;
        List<vClassSubjectTaskIndicator> lstIndicator = null;
        List<vClassSubjectTaskIndicator> lstClassSubjectTaskIndicator = null;
        protected override void InitializeDataControl()
        {
            List<vClassSubject> lstClassSubject = BusinessLayer.GetvClassSubjectList(string.Format("SchoolClassID = {0} AND SubjectGCClassStudyType = '{1}' AND IsDeleted = 0", AppSession.SchoolClass.SchoolClassID, Constant.ClassStudyType.REGULAR));
            Methods.SetComboBoxField<vClassSubject>(cboSubject, lstClassSubject, "SubjectName", "ClassSubjectID");

            BindGridView();
        }

        private void BindGridView()
        {
            if (cboSubject.Value != null)
            {
                vClassSubject entityClassSubject = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID = {0}", hdnSubject.Value)).FirstOrDefault();
                txtPassingGrade.Text = entityClassSubject.PassingGrade.ToString();
                if (entityClassSubject.ParentID == 0)
                    hdnParentClassSubjectID.Value = entityClassSubject.ClassSubjectID.ToString();
                else
                    hdnParentClassSubjectID.Value = entityClassSubject.ParentID.ToString();
                hdnSubjectID.Value = entityClassSubject.SubjectID.ToString();
                hdnSchoolPeriodID.Value = entityClassSubject.SchoolPeriodID.ToString();

                List<vCurriculumSubjectMarkType> lstCurriculumMarkType = BusinessLayer.GetvCurriculumSubjectMarkTypeList(string.Format("CurriculumID = {0} AND SubjectID = {1} AND CurriculumSubjectGroupID = {2} AND IsAllowTask = 1 AND IsDeleted = 0", AppSession.SchoolClass.CurriculumID, hdnSubjectID.Value, entityClassSubject.CurriculumSubjectGroupID));
                Methods.SetComboBoxField<vCurriculumSubjectMarkType>(cboLessonType, lstCurriculumMarkType, "CurriculumMarkTypeName", "CurriculumMarkTypeID");
                cboLessonType.SelectedIndex = 0;
                hdnLessonType.Value = cboLessonType.Value.ToString();

                vPeriodFinalMarkFormula entityFormula = BusinessLayer.GetvPeriodFinalMarkFormulaList(string.Format("SchoolPeriodID = {0} AND CurriculumMarkTypeID = {1}", hdnSchoolPeriodID.Value, cboLessonType.Value)).FirstOrDefault();
                hdnSummaryType.Value = Constant.FinalMarkSummaryType.AVERAGE;
                if (entityFormula != null)
                {
                    if (entityFormula.GCFinalMarkSource == Constant.FinalMarkSource.INDICATOR)
                        hdnSummaryType.Value = entityFormula.GCSummaryType;
                }

                List<Variable> lstSummaryType = new List<Variable>();
                lstSummaryType.Add(new Variable { Code = "0", Value = GetLabel("Rata-Rata") });
                lstSummaryType.Add(new Variable { Code = "1", Value = GetLabel("Tertinggi") });
                lstSummaryType.Add(new Variable { Code = "2", Value = GetLabel("Terrendah") });
                Methods.SetComboBoxField<Variable>(cboSummaryType, lstSummaryType, "Value", "Code");
                if (hdnSummaryType.Value == Constant.FinalMarkSummaryType.AVERAGE)
                    cboSummaryType.Value = "0";
                else
                    cboSummaryType.Value = "1";
                hdnCboSummaryType.Value = cboSummaryType.Value.ToString();

                lstClassSubjectTaskIndicator = BusinessLayer.GetvClassSubjectTaskIndicatorList(string.Format("ClassSubjectID = {0} AND PeriodSectionID = {1} AND CurriculumMarkTypeID = {2} AND IsDeleted = 0", cboSubject.Value, AppSession.SchoolClass.PeriodSectionID, cboLessonType.Value));
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

                List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", AppSession.SchoolClass.SchoolClassID));
                rptStudent.DataSource = lstStudent;
                rptStudent.DataBind();
            }
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
                    string summaryType = Request.Form[hdnCboSummaryType.UniqueID];
                    if (summaryType == "")
                        summaryType = hdnCboSummaryType.Value;
                    if (summaryType == "")
                        summaryType = cboSummaryType.Value.ToString();
                    if (summaryType == "0")
                        divStudentMark.InnerHtml = lstStudentMark1.Average(p => p.Mark).ToString("N");
                    else if (summaryType == "1")
                        divStudentMark.InnerHtml = lstStudentMark1.Max(p => p.Mark).ToString("N");
                    else
                        divStudentMark.InnerHtml = lstStudentMark1.Min(p => p.Mark).ToString("N");
                }
                else
                    divStudentMark.InnerHtml = "-";
            }
        }

        public override Control OnGetExportControl()
        {
            lstClassSubjectTaskIndicator = BusinessLayer.GetvClassSubjectTaskIndicatorList(string.Format("ClassSubjectID = {0} AND CurriculumMarkTypeID = {1} AND IsDeleted = 0", Request.Form[hdnSubject.UniqueID], Request.Form[hdnLessonType.UniqueID]));
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

            rptSubjectIndicatorHeader2.DataSource = lstIndicator;
            rptSubjectIndicatorHeader2.DataBind();

            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", AppSession.SchoolClass.SchoolClassID));
            rptStudent2.DataSource = lstStudent;
            rptStudent2.DataBind();

            HtmlGenericControl div = new HtmlGenericControl("DIV");
            div.Controls.Add(pnlPrint);
            return div;
        }
    }
}