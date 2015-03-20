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
using DevExpress.Web.ASPxEditors;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ClassTaskSummaryEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.TCS_CLASS_TASK_SUMMARY;
        }

        protected int OnGetTableViewWidth()
        {
            if (hdnGCClassStudyType.Value == Constant.ClassStudyType.EXTRACURRICULAR)
                return 740 + (lstClassTask.Count * 90);
            return 1200 + (lstClassTask.Count * 90) + (lstTheoryGroup.Count * 130) + (lstPracticeGroup.Count * 130);
        }
        protected string OnGetSubjectMarkTypeNumber()
        {
            return Constant.SubjectMarkType.NUMBER;
        }
        protected string OnGetSubjectMarkTypeOption()
        {
            return Constant.SubjectMarkType.OPTION;
        }
        protected string OnGetSubjectMarkTypeText()
        {
            return Constant.SubjectMarkType.TEXT;
        }

        protected string OnGetTransactionStatusApproved()
        {
            return Constant.TransactionStatus.APPROVED;
        }

        List<vClassSubjectTaskCustom> lstClassTask = null;
        List<vClassSubjectTaskCustom> lstTheory = null;
        List<vClassSubjectTaskCustom> lstPractice = null;
        List<vClassSubjectTaskCustom> lstTheoryGroup = null;
        List<vClassSubjectTaskCustom> lstPracticeGroup = null;
        List<StudentProgressRuleDt> lstProgress = null;
        protected override void InitializeDataControl()
        {
            vClassSubject entityClassSubject = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID)).FirstOrDefault();
            hdnIsMainTeacher.Value = entityClassSubject.ParentID == 0 ? "1" : "0";
            txtPassingGrade.Text = entityClassSubject.PassingGrade.ToString();
            hdnGCSubjectMarkType.Value = entityClassSubject.GCSubjectMarkType;
            hdnGCClassStudyType.Value = entityClassSubject.SubjectGCClassStudyType;
            lstProgress = BusinessLayer.GetStudentProgressRuleDtList(string.Format("StudentProgressRuleID = {0} AND IsDeleted = 0", entityClassSubject.StudentProgressRuleID));
            SubjectMatterHd subjectMatterHd = BusinessLayer.GetSubjectMatterHd(entityClassSubject.SubjectMatterID);
            if (subjectMatterHd != null)
                hdnCompetencyStandard.Value = subjectMatterHd.CompetencyStandard;
            else
                hdnCompetencyStandard.Value = "";

            hdnListProgress.Value = string.Join("|", lstProgress.Select(p => string.Format("{0};{1};{2};{3}", p.StudentProgressRuleDtID, p.FromValue > -1 ? p.FromValue : entityClassSubject.PassingGrade, p.ToValue > -1 ? p.ToValue : entityClassSubject.PassingGrade, p.Remarks.Replace("{StandarKompetensi}", hdnCompetencyStandard.Value))));

            lstProgress.Insert(0, new StudentProgressRuleDt { StudentProgressRuleDtID = 0, StudentProgressRuleDtName = "" });

            lstClassTask = BusinessLayer.GetvClassSubjectTaskCustomList(string.Format("ClassSubjectID = {0} AND IsDeleted = 0", AppSession.ClassSubject.ClassSubjectID));

            #region Theory
            lstTheory = lstClassTask.Where(p => p.GCLessonType == Constant.LessonType.THEORY).ToList();

            lstTheoryGroup = (from p in lstTheory
                                              select new vClassSubjectTaskCustom {
                                                  TheoryFinalMarkFormulaDtID = p.TheoryFinalMarkFormulaDtID,
                                                  TheoryFinalMarkFormulaDtName = p.TheoryFinalMarkFormulaDtName,
                                                  TheoryDisplayOrder = p.TheoryDisplayOrder,
                                                  TheoryFinalMarkPercentage = p.TheoryFinalMarkPercentage
                                              }).GroupBy(p => p.TheoryFinalMarkFormulaDtID).Select(p => p.First()).OrderBy(p => p.TheoryDisplayOrder).ToList();
            rptHeaderTheoryTaskGroup.DataSource = lstTheoryGroup;
            rptHeaderTheoryTaskGroup.DataBind();

            spnTotalTheoryPercentage.InnerHtml = lstTheoryGroup.Sum(p => p.TheoryFinalMarkPercentage).ToString();

            rptHeaderTheoryGroup.DataSource = lstTheoryGroup;
            rptHeaderTheoryGroup.DataBind();

            if (lstTheory.Count < 1)
            {
                thFinalMarkTheory.Style.Add("display", "none");
                thFinalReadonlyMarkTheory.Style.Add("display", "none");
                thTheory.Style.Add("display", "none");
            }
            if (hdnGCClassStudyType.Value == Constant.ClassStudyType.EXTRACURRICULAR)
            {
                thTheory.ColSpan = lstTheory.Count;
                thFinalMarkTheory.Style.Add("display", "none");
                thFinalReadonlyMarkTheory.Style.Add("display", "none");
            }
            else
                thTheory.ColSpan = lstTheory.Count + 2 + (lstTheoryGroup.Count * 2);
            #endregion

            #region Practice
            lstPractice = lstClassTask.Where(p => p.GCLessonType == Constant.LessonType.PRACTICE).ToList();

            lstPracticeGroup = (from p in lstPractice
                                select new vClassSubjectTaskCustom
                                {
                                    PracticeFinalMarkFormulaDtID = p.PracticeFinalMarkFormulaDtID,
                                    PracticeFinalMarkFormulaDtName = p.PracticeFinalMarkFormulaDtName,
                                    PracticeDisplayOrder = p.PracticeDisplayOrder,
                                    PracticeFinalMarkPercentage = p.PracticeFinalMarkPercentage
                                }).GroupBy(p => p.PracticeFinalMarkFormulaDtID).Select(p => p.First()).OrderBy(p => p.PracticeDisplayOrder).ToList();
            rptHeaderPracticeTaskGroup.DataSource = lstPracticeGroup;
            rptHeaderPracticeTaskGroup.DataBind();

            spnTotalPracticePercentage.InnerHtml = lstPracticeGroup.Sum(p => p.PracticeFinalMarkPercentage).ToString();

            rptHeaderPracticeGroup.DataSource = lstPracticeGroup;
            rptHeaderPracticeGroup.DataBind();

            if (lstPractice.Count < 1)
            {
                thFinalMarkPractice.Style.Add("display", "none");
                thFinalReadonlyMarkPractice.Style.Add("display", "none");
                thPractice.Style.Add("display", "none");
            }
            if (hdnGCClassStudyType.Value == Constant.ClassStudyType.EXTRACURRICULAR)
            {
                thPractice.ColSpan = lstPractice.Count;
                thFinalMarkPractice.Style.Add("display", "none");
                thFinalReadonlyMarkPractice.Style.Add("display", "none");
            }
            else
                thPractice.ColSpan = lstPractice.Count + 2 + (lstPracticeGroup.Count * 2);
            #endregion

            string filterExpression = "";
            if (entityClassSubject.ParentID == 0)
            {
                filterExpression = string.Format("ClassSubjectID = {0} OR ClassSubjectID IN (SELECT ClassSubjectID FROM ClassSubject WHERE ParentID = {0} AND IsDeleted = 0)", AppSession.ClassSubject.ClassSubjectID);
                hdnParentClassSubjectID.Value = entityClassSubject.ClassSubjectID.ToString();
            }
            else
            {
                filterExpression = string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID);
                hdnParentClassSubjectID.Value = entityClassSubject.ParentID.ToString();
            }
            lstStudentMark = BusinessLayer.GetvClassStudentSubjectTaskMarkList(filterExpression);
            lstStudentGroupMark = BusinessLayer.GetClassStudentSubjectTaskGroupMarkList(string.Format("ClassSubjectID = {0} AND PeriodSectionID = {1}", AppSession.ClassSubject.ClassSubjectID, AppSession.ClassSubject.PeriodSectionID));

            filterExpression = string.Format("{0} AND PeriodSectionID = {1}", filterExpression, AppSession.ClassSubject.PeriodSectionID);
            lstStudentFinalMark = BusinessLayer.GetClassStudentSubjectMarkList(filterExpression);

            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", entityClassSubject.SchoolClassID));
            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();

            ClassSubjectSection entitySubjectSection = BusinessLayer.GetClassSubjectSectionList(string.Format("PeriodSectionID = {0} AND ClassSubjectID = {1}", AppSession.ClassSubject.PeriodSectionID, hdnParentClassSubjectID.Value)).FirstOrDefault();
            if (entitySubjectSection == null)
            {
                btnApprove.Style.Add("display", "none");
                btnReopen.Style.Add("display", "none");
            }
            else
            {
                hdnGCTransactionStatus.Value = entitySubjectSection.GCTransactionStatus;
                if (entitySubjectSection.GCTransactionStatus == Constant.TransactionStatus.APPROVED)
                {
                    btnApprove.Style.Add("display", "none");
                    btnSave.Style.Add("display", "none");
                }
                else
                {
                    btnReopen.Style.Add("display", "none");
                }
            }

            if (hdnGCClassStudyType.Value == Constant.ClassStudyType.EXTRACURRICULAR)
            {
                thAffective.Style.Add("display", "none");
                thAffectiveDescription.Style.Add("display", "none");
                thAffectiveMark.Style.Add("display", "none");
            }
        }

        #region Header
        #region Theory
        protected void rptHeaderTheoryTaskGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTaskCustom entityGroup = (vClassSubjectTaskCustom)e.Item.DataItem;
                HtmlTableCell thHeaderTheoryTaskGroup = (HtmlTableCell)e.Item.FindControl("thHeaderTheoryTaskGroup");
                if (hdnGCClassStudyType.Value == Constant.ClassStudyType.EXTRACURRICULAR)
                    thHeaderTheoryTaskGroup.ColSpan = lstTheory.Where(p => p.TheoryFinalMarkFormulaDtID == entityGroup.TheoryFinalMarkFormulaDtID).Count();
                else
                    thHeaderTheoryTaskGroup.ColSpan = lstTheory.Where(p => p.TheoryFinalMarkFormulaDtID == entityGroup.TheoryFinalMarkFormulaDtID).Count() + 2;
            }
        }

        protected void rptHeaderTheoryGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTaskCustom entityGroup = (vClassSubjectTaskCustom)e.Item.DataItem;
                Repeater rptHeaderTheory = (Repeater)e.Item.FindControl("rptHeaderTheory");
                rptHeaderTheory.DataSource = lstTheory.Where(p => p.TheoryFinalMarkFormulaDtID == entityGroup.TheoryFinalMarkFormulaDtID).OrderBy(p => p.TaskDate).ThenBy(p => p.ClassSubjectTaskID).ToList();
                rptHeaderTheory.DataBind();

                if (hdnGCClassStudyType.Value == Constant.ClassStudyType.EXTRACURRICULAR)
                {
                    HtmlTableCell thAverageMarkTheory = (HtmlTableCell)e.Item.FindControl("thAverageMarkTheory");
                    HtmlTableCell thFinalMarkTheory = (HtmlTableCell)e.Item.FindControl("thFinalMarkTheory");
                    thAverageMarkTheory.Style.Add("display", "none");
                    thFinalMarkTheory.Style.Add("display", "none");
                }
            }
        }
        #endregion

        #region Practice
        protected void rptHeaderPracticeTaskGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTaskCustom entityGroup = (vClassSubjectTaskCustom)e.Item.DataItem;
                HtmlTableCell thHeaderPracticeTaskGroup = (HtmlTableCell)e.Item.FindControl("thHeaderPracticeTaskGroup");
                if (hdnGCClassStudyType.Value == Constant.ClassStudyType.EXTRACURRICULAR)
                    thHeaderPracticeTaskGroup.ColSpan = lstPractice.Where(p => p.PracticeFinalMarkFormulaDtID == entityGroup.PracticeFinalMarkFormulaDtID).Count();
                else
                    thHeaderPracticeTaskGroup.ColSpan = lstPractice.Where(p => p.PracticeFinalMarkFormulaDtID == entityGroup.PracticeFinalMarkFormulaDtID).Count() + 2;
            }
        }

        protected void rptHeaderPracticeGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTaskCustom entityGroup = (vClassSubjectTaskCustom)e.Item.DataItem;
                Repeater rptHeaderPractice = (Repeater)e.Item.FindControl("rptHeaderPractice");
                rptHeaderPractice.DataSource = lstPractice.Where(p => p.PracticeFinalMarkFormulaDtID == entityGroup.PracticeFinalMarkFormulaDtID).OrderBy(p => p.TaskDate).ThenBy(p => p.ClassSubjectTaskID).ToList();
                rptHeaderPractice.DataBind();

                if (hdnGCClassStudyType.Value == Constant.ClassStudyType.EXTRACURRICULAR)
                {
                    HtmlTableCell thAverageMarkPractice = (HtmlTableCell)e.Item.FindControl("thAverageMarkPractice");
                    HtmlTableCell thFinalMarkPractice = (HtmlTableCell)e.Item.FindControl("thFinalMarkPractice");
                    thAverageMarkPractice.Style.Add("display", "none");
                    thFinalMarkPractice.Style.Add("display", "none");
                }
            }
        }
        #endregion
        #endregion

        List<ClassStudentSubjectTaskGroupMark> lstStudentGroupMark = null;
        List<vClassStudentSubjectTaskMark> lstStudentMark = null;
        List<ClassStudentSubjectMark> lstStudentFinalMark = null;
        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassStudent entity = (vClassStudent)e.Item.DataItem;
                ClassStudentSubjectMark studentFinalMark = lstStudentFinalMark.FirstOrDefault(p => p.StudentID == entity.StudentID);
                TextBox txtFinalStudentMarkTheory = (TextBox)e.Item.FindControl("txtFinalStudentMarkTheory");
                txtFinalStudentMarkTheory.Attributes.Add("itemindex", e.Item.ItemIndex.ToString());

                TextBox txtFinalStudentMarkPractice = (TextBox)e.Item.FindControl("txtFinalStudentMarkPractice");
                txtFinalStudentMarkPractice.Attributes.Add("itemindex", e.Item.ItemIndex.ToString());

                ASPxComboBox cboStudentProgressRule = (ASPxComboBox)e.Item.FindControl("cboStudentProgressRule");
                cboStudentProgressRule.ClientInstanceName = string.Format("cboStudentProgressRule{0}", e.Item.ItemIndex);
                Methods.SetComboBoxField<StudentProgressRuleDt>(cboStudentProgressRule, lstProgress, "StudentProgressRuleDtName", "StudentProgressRuleDtID");
                cboStudentProgressRule.ClientSideEvents.ValueChanged = "function(s,e){ onCboStudentProgressRuleValueChanged(s, " + e.Item.ItemIndex + "); }";

                if (studentFinalMark != null)
                {
                    TextBox txtAffectiveMark = (TextBox)e.Item.FindControl("txtAffectiveMark");
                    TextBox txtAffectiveDescription = (TextBox)e.Item.FindControl("txtAffectiveDescription");
                    TextBox txtProgressDescription = (TextBox)e.Item.FindControl("txtProgressDescription");
                    txtFinalStudentMarkTheory.Text = studentFinalMark.TheoryMark.ToString();
                    txtFinalStudentMarkPractice.Text = studentFinalMark.PracticeMark.ToString();
                    txtAffectiveMark.Text = studentFinalMark.AffectiveMark;
                    txtAffectiveDescription.Text = studentFinalMark.AffectiveDescription;
                    txtProgressDescription.Text = studentFinalMark.ProgressDescription;
                    cboStudentProgressRule.Value = studentFinalMark.StudentProgressRuleDtID.ToString();
                }

                Repeater rptStudentMarkTheoryGroup = (Repeater)e.Item.FindControl("rptStudentMarkTheoryGroup");
                rptStudentMarkTheoryGroup.DataSource = lstTheoryGroup;
                rptStudentMarkTheoryGroup.DataBind();
                Repeater rptStudentMarkPracticeGroup = (Repeater)e.Item.FindControl("rptStudentMarkPracticeGroup");
                rptStudentMarkPracticeGroup.DataSource = lstPracticeGroup;
                rptStudentMarkPracticeGroup.DataBind();

                HtmlTableCell tdTotalStudentMarkTheory = (HtmlTableCell)e.Item.FindControl("tdTotalStudentMarkTheory");
                HtmlTableCell tdFinalStudentMarkTheory = (HtmlTableCell)e.Item.FindControl("tdFinalStudentMarkTheory");

                HtmlTableCell tdTotalStudentMarkPractice = (HtmlTableCell)e.Item.FindControl("tdTotalStudentMarkPractice");
                HtmlTableCell tdFinalStudentMarkPractice = (HtmlTableCell)e.Item.FindControl("tdFinalStudentMarkPractice");

                if (lstTheory.Count < 1)
                {
                    tdTotalStudentMarkTheory.Style.Add("display", "none");
                    tdFinalStudentMarkTheory.Style.Add("display", "none");
                }
                if (lstPractice.Count < 1)
                {
                    tdTotalStudentMarkPractice.Style.Add("display", "none");
                    tdFinalStudentMarkPractice.Style.Add("display", "none");
                }

                if (hdnGCClassStudyType.Value == Constant.ClassStudyType.EXTRACURRICULAR)
                {
                    HtmlTableCell tdStudentAffectiveMark = (HtmlTableCell)e.Item.FindControl("tdStudentAffectiveMark");
                    HtmlTableCell tdStudentAffectiveDescription = (HtmlTableCell)e.Item.FindControl("tdStudentAffectiveDescription");
                    tdStudentAffectiveMark.Style.Add("display", "none");
                    tdStudentAffectiveDescription.Style.Add("display", "none");
                    tdTotalStudentMarkPractice.Style.Add("display", "none");
                    tdFinalStudentMarkPractice.Style.Add("display", "none");
                    tdTotalStudentMarkTheory.Style.Add("display", "none");
                    tdFinalStudentMarkTheory.Style.Add("display", "none");
                }
            }
        }

        #region Repeater Inside Student
        #region Theory
        protected void rptStudentMarkTheoryGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTaskCustom entityGroup = (vClassSubjectTaskCustom)e.Item.DataItem;
                vClassStudent student = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vClassStudent;
                Repeater rptStudentMarkTheory = (Repeater)e.Item.FindControl("rptStudentMarkTheory");
                rptStudentMarkTheory.DataSource = lstTheory.Where(p => p.TheoryFinalMarkFormulaDtID == entityGroup.TheoryFinalMarkFormulaDtID).OrderBy(p => p.TaskDate).ThenBy(p => p.ClassSubjectTaskID).ToList();
                rptStudentMarkTheory.DataBind();

                TextBox txtFinalStudentMarkTheoryGroup = (TextBox)e.Item.FindControl("txtFinalStudentMarkTheoryGroup");
                txtFinalStudentMarkTheoryGroup.Attributes.Add("formulapercentage", entityGroup.TheoryFinalMarkPercentage.ToString());
                txtFinalStudentMarkTheoryGroup.Attributes.Add("formuladtid", entityGroup.TheoryFinalMarkFormulaDtID.ToString());
                ClassStudentSubjectTaskGroupMark entityMark = lstStudentGroupMark.FirstOrDefault(p => p.StudentFinalMarkFormulaDtID == entityGroup.TheoryFinalMarkFormulaDtID && p.StudentID == student.StudentID);
                if (entityMark != null)
                    txtFinalStudentMarkTheoryGroup.Text = entityMark.Mark.ToString();

                if (hdnGCClassStudyType.Value == Constant.ClassStudyType.EXTRACURRICULAR)
                {
                    HtmlTableCell tdAverageStudentMarkTheoryGroup = (HtmlTableCell)e.Item.FindControl("tdAverageStudentMarkTheoryGroup");
                    HtmlTableCell tdFinalStudentMarkTheoryGroup = (HtmlTableCell)e.Item.FindControl("tdFinalStudentMarkTheoryGroup");
                    tdAverageStudentMarkTheoryGroup.Style.Add("display", "none");
                    tdFinalStudentMarkTheoryGroup.Style.Add("display", "none");
                }
            }
        }

        protected void rptStudentMarkTheory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTaskCustom subjectTask = (vClassSubjectTaskCustom)e.Item.DataItem;
                vClassStudent student = ((RepeaterItem)((RepeaterItem)e.Item.Parent.Parent).Parent.Parent).DataItem as vClassStudent;

                TextBox txtStudentMark = (TextBox)e.Item.FindControl("txtStudentMark");
                ASPxComboBox cboStudentMarkOption = (ASPxComboBox)e.Item.FindControl("cboStudentMarkOption");
                TextBox txtStudentMarkDescription = (TextBox)e.Item.FindControl("txtStudentMarkDescription");
                vClassStudentSubjectTaskMark studentMark = lstStudentMark.FirstOrDefault(p => p.ClassSubjectTaskID == subjectTask.ClassSubjectTaskID && p.StudentID == student.StudentID);

                int parentIndex = ((RepeaterItem)e.Item.Parent.Parent).ItemIndex;
                cboStudentMarkOption.ClientInstanceName = string.Format("cboStudentMarkOption{0}{1}{2}", "Theory", parentIndex.ToString("D2"), e.Item.ItemIndex.ToString("D2"));
                txtStudentMark.Attributes.Add("positiontag", string.Format("{0}{1}", parentIndex.ToString("D2"), e.Item.ItemIndex.ToString("D2")));
                txtStudentMark.Attributes.Add("formuladtid", subjectTask.TheoryFinalMarkFormulaDtID.ToString());
                HtmlGenericControl divMark = (HtmlGenericControl)e.Item.FindControl("divMark");
                switch (hdnGCSubjectMarkType.Value)
                {
                    case Constant.SubjectMarkType.NUMBER: divMark.Style.Add("display", "none"); cboStudentMarkOption.ClientVisible = false; txtStudentMarkDescription.Style.Add("display", "none"); break;
                    case Constant.SubjectMarkType.OPTION:
                        txtStudentMark.Style.Add("display", "none"); txtStudentMarkDescription.Style.Add("display", "none");
                        Methods.SetComboBoxField<StudentProgressRuleDt>(cboStudentMarkOption, lstProgress, "StudentProgressRuleDtName", "StudentProgressRuleDtID");
                        break;
                    case Constant.SubjectMarkType.TEXT: cboStudentMarkOption.ClientVisible = false; txtStudentMark.Style.Add("display", "none"); break;
                }
                HtmlGenericControl bIsRemedial = (HtmlGenericControl)e.Item.FindControl("bIsRemedial");
                if (studentMark != null)
                {
                    txtStudentMark.Text = studentMark.Mark.ToString();
                    cboStudentMarkOption.Value = studentMark.StudentProgressRuleDtID.ToString();
                    txtStudentMarkDescription.Text = studentMark.DescriptionMark;
                    if (!studentMark.IsRemedial)
                        bIsRemedial.Style.Add("display", "none");
                }
                else
                    bIsRemedial.Style.Add("display", "none");

                bIsRemedial.Attributes.Add("ClassSubjectTaskID", subjectTask.ClassSubjectTaskID.ToString());
            }
        }
        #endregion
        
        #region Practice
        protected void rptStudentMarkPracticeGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTaskCustom entityGroup = (vClassSubjectTaskCustom)e.Item.DataItem;
                vClassStudent student = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vClassStudent;
                Repeater rptStudentMarkPractice = (Repeater)e.Item.FindControl("rptStudentMarkPractice");
                rptStudentMarkPractice.DataSource = lstPractice.Where(p => p.PracticeFinalMarkFormulaDtID == entityGroup.PracticeFinalMarkFormulaDtID).OrderBy(p => p.TaskDate).ThenBy(p => p.ClassSubjectTaskID).ToList();
                rptStudentMarkPractice.DataBind();

                TextBox txtFinalStudentMarkPracticeGroup = (TextBox)e.Item.FindControl("txtFinalStudentMarkPracticeGroup");
                txtFinalStudentMarkPracticeGroup.Attributes.Add("formulapercentage", entityGroup.PracticeFinalMarkPercentage.ToString());
                txtFinalStudentMarkPracticeGroup.Attributes.Add("formuladtid", entityGroup.PracticeFinalMarkFormulaDtID.ToString());
                ClassStudentSubjectTaskGroupMark entityMark = lstStudentGroupMark.FirstOrDefault(p => p.StudentFinalMarkFormulaDtID == entityGroup.PracticeFinalMarkFormulaDtID && p.StudentID == student.StudentID);
                if (entityMark != null)
                    txtFinalStudentMarkPracticeGroup.Text = entityMark.Mark.ToString();
                
                if (hdnGCClassStudyType.Value == Constant.ClassStudyType.EXTRACURRICULAR)
                {
                    HtmlTableCell tdAverageStudentMarkPracticeGroup = (HtmlTableCell)e.Item.FindControl("tdAverageStudentMarkPracticeGroup");
                    HtmlTableCell tdFinalStudentMarkTheoryGroup = (HtmlTableCell)e.Item.FindControl("tdFinalStudentMarkTheoryGroup");
                    tdAverageStudentMarkPracticeGroup.Style.Add("display", "none");
                    tdFinalStudentMarkTheoryGroup.Style.Add("display", "none");
                }                
            }
        }

        protected void rptStudentMarkPractice_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTaskCustom subjectTask = (vClassSubjectTaskCustom)e.Item.DataItem;
                vClassStudent student = ((RepeaterItem)((RepeaterItem)e.Item.Parent.Parent).Parent.Parent).DataItem as vClassStudent;

                TextBox txtStudentMark = (TextBox)e.Item.FindControl("txtStudentMark");
                ASPxComboBox cboStudentMarkOption = (ASPxComboBox)e.Item.FindControl("cboStudentMarkOption");
                TextBox txtStudentMarkDescription = (TextBox)e.Item.FindControl("txtStudentMarkDescription");
                vClassStudentSubjectTaskMark studentMark = lstStudentMark.FirstOrDefault(p => p.ClassSubjectTaskID == subjectTask.ClassSubjectTaskID && p.StudentID == student.StudentID);

                int parentIndex = ((RepeaterItem)((RepeaterItem)e.Item.Parent.Parent).Parent.Parent).ItemIndex;
                cboStudentMarkOption.ClientInstanceName = string.Format("cboStudentMarkOption{0}{1}{2}", "Practice", parentIndex.ToString("D2"), e.Item.ItemIndex.ToString("D2"));
                txtStudentMark.Attributes.Add("positiontag", string.Format("{0}{1}", parentIndex.ToString("D2"), e.Item.ItemIndex.ToString("D2")));
                txtStudentMark.Attributes.Add("formuladtid", subjectTask.PracticeFinalMarkFormulaDtID.ToString());
                HtmlGenericControl divMark = (HtmlGenericControl)e.Item.FindControl("divMark");
                switch (hdnGCSubjectMarkType.Value)
                {
                    case Constant.SubjectMarkType.NUMBER: cboStudentMarkOption.ClientVisible = false; txtStudentMarkDescription.Style.Add("display", "none"); break;
                    case Constant.SubjectMarkType.OPTION:
                        divMark.Style.Add("display", "none"); txtStudentMark.Style.Add("display", "none"); txtStudentMarkDescription.Style.Add("display", "none");
                        Methods.SetComboBoxField<StudentProgressRuleDt>(cboStudentMarkOption, lstProgress, "StudentProgressRuleDtName", "StudentProgressRuleDtID");
                        break;
                    case Constant.SubjectMarkType.TEXT: divMark.Style.Add("display", "none"); cboStudentMarkOption.ClientVisible = false; txtStudentMark.Style.Add("display", "none"); break;
                }
                HtmlGenericControl bIsRemedial = (HtmlGenericControl)e.Item.FindControl("bIsRemedial");
                if (studentMark != null)
                {
                    txtStudentMark.Text = studentMark.Mark.ToString();
                    cboStudentMarkOption.Value = studentMark.StudentProgressRuleDtID.ToString();
                    txtStudentMarkDescription.Text = studentMark.DescriptionMark;
                    if (!studentMark.IsRemedial)
                        bIsRemedial.Style.Add("display", "none");
                }
                else
                    bIsRemedial.Style.Add("display", "none");

                bIsRemedial.Attributes.Add("ClassSubjectTaskID", subjectTask.ClassSubjectTaskID.ToString());
            }
        }
        #endregion      
        #endregion        

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            if (type == "save")
            {
                bool result = true;
                IDbContext ctx = DbFactory.Configure(true);
                ClassSubjectSectionDao entitySubjectSectionDao = new ClassSubjectSectionDao(ctx);
                ClassSubjectTaskDao entityDtDao = new ClassSubjectTaskDao(ctx);
                ClassStudentSubjectTaskMarkDao entityStudentSubjectTaskMarkDao = new ClassStudentSubjectTaskMarkDao(ctx);
                ClassStudentSubjectMarkDao entityStudentSubjectMarkDao = new ClassStudentSubjectMarkDao(ctx);
                ClassStudentSubjectTaskGroupMarkDao entityStudentSubjectTaskGroupMarkDao = new ClassStudentSubjectTaskGroupMarkDao(ctx);
                try
                {
                    string[] lstSaveValue = hdnListSaveHeaderValue.Value.Split('|');

                    List<ClassSubjectTask> lstClassTask = BusinessLayer.GetClassSubjectTaskList(string.Format("ClassSubjectID = {0} AND IsDeleted = 0", AppSession.ClassSubject.ClassSubjectID));
                    List<int> lstClassSubjectTaskID = new List<int>();
                    foreach (String saveValue in lstSaveValue)
                    {
                        string[] temp = saveValue.Split(',');
                        int ClassSubjectTaskID = Convert.ToInt32(temp[0]);
                        ClassSubjectTask entityDt = lstClassTask.FirstOrDefault(p => p.ClassSubjectTaskID == ClassSubjectTaskID);
                        short FinalMarkPercentage = Convert.ToInt16(temp[1]);
                        if (FinalMarkPercentage != entityDt.FinalMarkPercentage)
                        {
                            entityDt.FinalMarkPercentage = FinalMarkPercentage;
                            entityDtDao.Update(entityDt);
                        }
                        lstClassSubjectTaskID.Add(ClassSubjectTaskID);
                    }

                    ClassSubjectSection entitySubjectSection = BusinessLayer.GetClassSubjectSectionList(string.Format("PeriodSectionID = {0} AND ClassSubjectID = {1}", AppSession.ClassSubject.PeriodSectionID, hdnParentClassSubjectID.Value), ctx).FirstOrDefault();
                    if (entitySubjectSection == null)
                    {
                        entitySubjectSection = new ClassSubjectSection();
                        entitySubjectSection.ClassSubjectID = Convert.ToInt32(hdnParentClassSubjectID.Value);
                        entitySubjectSection.PeriodSectionID = AppSession.ClassSubject.PeriodSectionID;
                        entitySubjectSection.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                        entitySubjectSectionDao.Insert(entitySubjectSection);
                    }

                    List<ClassStudentSubjectTaskMark> lstStudentMark = BusinessLayer.GetClassStudentSubjectTaskMarkList(string.Format("ClassSubjectTaskID IN ({0})", string.Join(",", lstClassSubjectTaskID.Select(p => p).ToList())), ctx);
                    List<ClassStudentSubjectTaskGroupMark> lstStudentGroupMark = BusinessLayer.GetClassStudentSubjectTaskGroupMarkList(string.Format("ClassSubjectID = {0} AND PeriodSectionID = {1}", AppSession.ClassSubject.ClassSubjectID, AppSession.ClassSubject.PeriodSectionID), ctx);
                    List<ClassStudentSubjectMark> lstStudentFinalMark = BusinessLayer.GetClassStudentSubjectMarkList(string.Format("PeriodSectionID = {0} AND ClassSubjectID = {1}", AppSession.ClassSubject.PeriodSectionID, hdnParentClassSubjectID.Value), ctx);
                    lstSaveValue = hdnListSaveValue.Value.Split('|');
                    int ClassSubjectID = Convert.ToInt32(hdnParentClassSubjectID.Value);
                    foreach (String saveValue in lstSaveValue)
                    {
                        string[] lstSaveValue1 = saveValue.Split('|');
                        foreach (String saveValue1 in lstSaveValue1)
                        {
                            string[] temp = saveValue.Split('*');
                            int studentID = Convert.ToInt32(temp[0]);
                            decimal finalStudentMarkTheory = -1;
                            if (temp[1] != "-")
                                finalStudentMarkTheory = Convert.ToDecimal(temp[1]);
                            decimal finalStudentMarkPractice = -1;
                            if (temp[1] != "-")
                                finalStudentMarkPractice = Convert.ToDecimal(temp[2]);
                            ClassStudentSubjectMark studentFinalMark = lstStudentFinalMark.FirstOrDefault(p => p.StudentID == studentID);
                            if (studentFinalMark == null)
                            {
                                studentFinalMark = new ClassStudentSubjectMark();
                                studentFinalMark.ClassSubjectID = ClassSubjectID;
                                studentFinalMark.StudentID = studentID;
                                studentFinalMark.PeriodSectionID = AppSession.ClassSubject.PeriodSectionID;
                                studentFinalMark.TheoryMark = finalStudentMarkTheory;
                                studentFinalMark.PracticeMark = finalStudentMarkPractice;
                                studentFinalMark.AffectiveMark = temp[3];
                                studentFinalMark.AffectiveDescription = temp[4];
                                if (temp[5] == "")
                                    studentFinalMark.StudentProgressRuleDtID = null;
                                else
                                    studentFinalMark.StudentProgressRuleDtID = Convert.ToInt32(temp[5]);
                                studentFinalMark.ProgressDescription = temp[6];
                                entityStudentSubjectMarkDao.Insert(studentFinalMark);
                            }
                            else
                            {
                                studentFinalMark.TheoryMark = finalStudentMarkTheory;
                                studentFinalMark.PracticeMark = finalStudentMarkPractice;
                                studentFinalMark.AffectiveMark = temp[3];
                                studentFinalMark.AffectiveDescription = temp[4];
                                if (temp[5] == "")
                                    studentFinalMark.StudentProgressRuleDtID = null;
                                else
                                    studentFinalMark.StudentProgressRuleDtID = Convert.ToInt32(temp[5]);
                                studentFinalMark.ProgressDescription = temp[6];
                                entityStudentSubjectMarkDao.Update(studentFinalMark);
                            }

                            string[] lstSaveValue2 = temp[7].Split(',');
                            int ctr = 0;
                            foreach (String saveValue2 in lstSaveValue2)
                            {
                                if (saveValue2 != "")
                                {
                                    int ClassSubjectTaskID = lstClassSubjectTaskID[ctr];
                                    ClassStudentSubjectTaskMark studentMark = lstStudentMark.FirstOrDefault(p => p.ClassSubjectTaskID == ClassSubjectTaskID && p.StudentID == studentID);

                                    if (hdnGCSubjectMarkType.Value == Constant.SubjectMarkType.NUMBER)
                                    {
                                        Decimal mark = -1;
                                        if (saveValue2 != "-")
                                            mark = Convert.ToDecimal(saveValue2);
                                        if (studentMark == null)
                                        {
                                            if (mark > -1)
                                            {
                                                studentMark = new ClassStudentSubjectTaskMark();
                                                studentMark.StudentID = studentID;
                                                studentMark.ClassSubjectTaskID = ClassSubjectTaskID;
                                                studentMark.Mark = mark;
                                                entityStudentSubjectTaskMarkDao.Insert(studentMark);
                                            }
                                        }
                                        else if (studentMark.Mark != mark)
                                        {
                                            if (mark > -1)
                                            {
                                                studentMark.Mark = mark;
                                                entityStudentSubjectTaskMarkDao.Update(studentMark);
                                            }
                                            else
                                                entityStudentSubjectTaskMarkDao.Delete(ClassSubjectTaskID, studentID);
                                        }
                                    }
                                    else if (hdnGCSubjectMarkType.Value == Constant.SubjectMarkType.OPTION)
                                    {
                                        if (studentMark == null)
                                        {
                                            if (saveValue2 != "")
                                            {
                                                studentMark = new ClassStudentSubjectTaskMark();
                                                studentMark.StudentID = studentID;
                                                studentMark.ClassSubjectTaskID = ClassSubjectTaskID;
                                                studentMark.StudentProgressRuleDtID = Convert.ToInt32(saveValue2);
                                                entityStudentSubjectTaskMarkDao.Insert(studentMark);
                                            }
                                        }
                                        else if (studentMark.StudentProgressRuleDtID.ToString() != saveValue2)
                                        {
                                            if (saveValue2 != "")
                                            {
                                                studentMark.StudentProgressRuleDtID = Convert.ToInt32(saveValue2);
                                                entityStudentSubjectTaskMarkDao.Update(studentMark);
                                            }
                                            else
                                                entityStudentSubjectTaskMarkDao.Delete(ClassSubjectTaskID, studentID);
                                        }
                                    }
                                    else
                                    {
                                        if (studentMark == null)
                                        {
                                            if (saveValue2 != "")
                                            {
                                                studentMark = new ClassStudentSubjectTaskMark();
                                                studentMark.StudentID = studentID;
                                                studentMark.ClassSubjectTaskID = ClassSubjectTaskID;
                                                studentMark.DescriptionMark = saveValue2;
                                                entityStudentSubjectTaskMarkDao.Insert(studentMark);
                                            }
                                        }
                                        else if (studentMark.DescriptionMark != saveValue2)
                                        {
                                            if (saveValue2 != "")
                                            {
                                                studentMark.DescriptionMark = saveValue2;
                                                entityStudentSubjectTaskMarkDao.Update(studentMark);
                                            }
                                            else
                                                entityStudentSubjectTaskMarkDao.Delete(ClassSubjectTaskID, studentID);
                                        }
                                    }
                                }
                                ctr++;
                            }

                            lstSaveValue2 = temp[8].Split(',');
                            ctr = 0;
                            foreach (String saveValue2 in lstSaveValue2)
                            {
                                string[] temp1 = saveValue2.Split(')');
                                int StudentFinalMarkFormulaDtID = Convert.ToInt32(temp1[0]);
                                ClassStudentSubjectTaskGroupMark studentMark = lstStudentGroupMark.FirstOrDefault(p => p.StudentFinalMarkFormulaDtID == StudentFinalMarkFormulaDtID && p.StudentID == studentID);

                                Decimal mark = -1;
                                if (temp1[1] != "-" && temp1[1] != "")
                                    mark = Convert.ToDecimal(temp1[1]);
                                if (studentMark == null)
                                {
                                    if (mark > -1)
                                    {
                                        studentMark = new ClassStudentSubjectTaskGroupMark();
                                        studentMark.ClassSubjectID = AppSession.ClassSubject.ClassSubjectID;
                                        studentMark.PeriodSectionID = AppSession.ClassSubject.PeriodSectionID;
                                        studentMark.StudentID = studentID;
                                        studentMark.StudentFinalMarkFormulaDtID = StudentFinalMarkFormulaDtID;
                                        studentMark.Mark = mark;
                                        entityStudentSubjectTaskGroupMarkDao.Insert(studentMark);
                                    }
                                }
                                else if (studentMark.Mark != mark)
                                {
                                    if (mark > -1)
                                    {
                                        studentMark.Mark = mark;
                                        entityStudentSubjectTaskGroupMarkDao.Update(studentMark);
                                    }
                                    else
                                        entityStudentSubjectTaskGroupMarkDao.Delete(AppSession.ClassSubject.ClassSubjectID, AppSession.ClassSubject.PeriodSectionID, StudentFinalMarkFormulaDtID, studentID);
                                }
                            }
                        }
                    }
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
            else if (type == "approve")
            {
                try
                {
                    ClassSubjectSection entity = BusinessLayer.GetClassSubjectSection(Convert.ToInt32(hdnParentClassSubjectID.Value), AppSession.ClassSubject.PeriodSectionID);
                    entity.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                    BusinessLayer.UpdateClassSubjectSection(entity);
                    return true;
                }
                catch (Exception ex)
                {
                    Helper.InsertErrorLog(ex);
                    errMessage = ex.Message;
                    return false;
                }
            }
            else if (type == "reopen")
            {
                try
                {
                    ClassSubjectSection entity = BusinessLayer.GetClassSubjectSection(Convert.ToInt32(hdnParentClassSubjectID.Value), AppSession.ClassSubject.PeriodSectionID);
                    entity.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                    BusinessLayer.UpdateClassSubjectSection(entity);
                    return true;
                }
                catch (Exception ex)
                {
                    Helper.InsertErrorLog(ex);
                    errMessage = ex.Message;
                    return false;
                }
            }
            return false;
        }
    }
}