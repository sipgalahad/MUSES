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
            //return 1200 + (lstClassTask.Count * 90) + (lstTheoryGroup.Count * 130) + (lstPracticeGroup.Count * 130);
            return 2000;
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

        class CTaskCount
        {
            public int CurriculumMarkTypeID { get; set; }
            public int CurriculumFinalMarkFormulaDtID { get; set; }
            public int Count { get; set; }
        }

        List<CTaskCount> lstTaskCount = null;
        List<MarkTypeDt> lstMarkTypeDt = null;
        List<vClassSubjectTask> lstClassTask = null;
        List<StudentProgressRuleDt> lstProgress = null;
        List<vPeriodClassTypeSubjectFinalMarkFormulaCustom> lstFinalMarkFormulaHd = null;
        List<vCurriculumFinalMarkFormulaDt> lstFinalMarkFormulaDt = null;
        List<vCurriculumSubjectMarkType> lstCurriculumMarkType = null;
        protected override void InitializeDataControl()
        {
            lstTaskCount = new List<CTaskCount>();

            vClassSubject entityClassSubject = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID)).FirstOrDefault();

            lstFinalMarkFormulaHd = BusinessLayer.GetvPeriodClassTypeSubjectFinalMarkFormulaCustomList(string.Format("PeriodClassTypeSubjectID = {0}", entityClassSubject.PeriodClassTypeSubjectID));
            string lstFinalMarkFormulaID = string.Join(",", lstFinalMarkFormulaHd.Select(p => p.CurriculumFinalMarkFormulaID).ToList());
            if (lstFinalMarkFormulaID != "")
                lstFinalMarkFormulaDt = BusinessLayer.GetvCurriculumFinalMarkFormulaDtList(string.Format("CurriculumFinalMarkFormulaID IN ({0}) AND IsDeleted = 0", lstFinalMarkFormulaID));
            else
                lstFinalMarkFormulaDt = new List<vCurriculumFinalMarkFormulaDt>();

            lstClassTask = BusinessLayer.GetvClassSubjectTaskList(string.Format("ClassSubjectID = {0} AND IsDeleted = 0", AppSession.ClassSubject.ClassSubjectID));

            lstCurriculumMarkType = BusinessLayer.GetvCurriculumSubjectMarkTypeList(string.Format("CurriculumID = {0} AND SubjectID = {1} AND IsDeleted = 0", AppSession.ClassSubject.CurriculumID, entityClassSubject.SubjectID));

            string lstMarkTypeTaskID = string.Join(",", lstCurriculumMarkType.Select(p => p.TaskMarkTypeID).ToList());
            string lstMarkTypeFinalID = string.Join(",", lstCurriculumMarkType.Select(p => p.FinalMarkTypeID).ToList());
            string lstMarkTypePredicateID = string.Join(",", lstCurriculumMarkType.Select(p => p.PredicateMarkTypeID).ToList());
            lstMarkTypeDt = BusinessLayer.GetMarkTypeDtList(string.Format("MarkTypeID IN ({0},{1},{2}) AND IsDeleted = 0", lstMarkTypeTaskID, lstMarkTypeFinalID, lstMarkTypePredicateID));

            rptHeaderMarkType3.DataSource = lstCurriculumMarkType;
            rptHeaderMarkType3.DataBind();

            rptHeaderMarkType2.DataSource = lstCurriculumMarkType;
            rptHeaderMarkType2.DataBind();

            rptHeaderMarkType1.DataSource = lstCurriculumMarkType;
            rptHeaderMarkType1.DataBind();

            PeriodSection periodSection = BusinessLayer.GetPeriodSection(AppSession.ClassSubject.PeriodSectionID);

            hdnIsMainTeacher.Value = entityClassSubject.ParentID == 0 ? "1" : "0";
            txtPassingGrade.Text = entityClassSubject.PassingGrade.ToString();
            hdnGCClassStudyType.Value = entityClassSubject.SubjectGCClassStudyType;
            lstProgress = BusinessLayer.GetStudentProgressRuleDtList(string.Format("StudentProgressRuleID = {0} AND IsDeleted = 0", entityClassSubject.StudentProgressRuleID));
            SubjectMatterHd subjectMatterHd = BusinessLayer.GetSubjectMatterHd(entityClassSubject.SubjectMatterID);
            if (subjectMatterHd != null)
            {
                SubjectCompetencyStandardSummary entitySummary = BusinessLayer.GetSubjectCompetencyStandardSummaryList(string.Format("SubjectMatterID = {0} AND GCPeriodSection = '{1}'", subjectMatterHd.SubjectMatterID, periodSection.GCPeriodSection)).FirstOrDefault();
                if (entitySummary != null)
                    hdnCompetencyStandard.Value = entitySummary.SummaryName;
                else
                    hdnCompetencyStandard.Value = "";
            }
            else
                hdnCompetencyStandard.Value = "";

            hdnListProgress.Value = string.Join("|", lstProgress.Select(p => string.Format("{0};{1};{2};{3}", p.StudentProgressRuleDtID, p.FromValue > -1 ? p.FromValue : entityClassSubject.PassingGrade, p.ToValue > -1 ? p.ToValue : entityClassSubject.PassingGrade, p.Remarks.Replace("{StandarKompetensi}", hdnCompetencyStandard.Value))));

            lstProgress.Insert(0, new StudentProgressRuleDt { StudentProgressRuleDtID = 0, StudentProgressRuleDtName = "" });

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

            //if (hdnGCClassStudyType.Value == Constant.ClassStudyType.EXTRACURRICULAR)
            //{
            //    thAffective.Style.Add("display", "none");
            //    thAffectiveDescription.Style.Add("display", "none");
            //    thAffectiveMark.Style.Add("display", "none");
            //}
        }

        protected void rptHeaderMarkType1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vCurriculumSubjectMarkType entity = (vCurriculumSubjectMarkType)e.Item.DataItem;
                HtmlTableCell thHeader = (HtmlTableCell)e.Item.FindControl("thHeader");
                int colSpan = 0;

                List<CTaskCount> lstTaskCount1 = lstTaskCount.Where(p => p.CurriculumMarkTypeID == entity.CurriculumMarkTypeID).ToList();
                foreach (CTaskCount taskCount in lstTaskCount1)
                {
                    colSpan += taskCount.Count + 2;
                }
                if (entity.IsAllowTask)
                    colSpan += 2;
                else
                    colSpan += 1;

                if (entity.PredicateMarkTypeID > 0)
                    colSpan++;
                thHeader.ColSpan = colSpan;
            }
        }

        protected void rptHeaderMarkType2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vCurriculumSubjectMarkType entity = (vCurriculumSubjectMarkType)e.Item.DataItem;

                vPeriodClassTypeSubjectFinalMarkFormulaCustom entityFormulaHd = lstFinalMarkFormulaHd.FirstOrDefault(p => p.CurriculumMarkTypeID == entity.CurriculumMarkTypeID);
                if (entityFormulaHd != null)
                {
                    Repeater rptHeaderMarkType2Dt = (Repeater)e.Item.FindControl("rptHeaderMarkType2Dt");
                    List<vCurriculumFinalMarkFormulaDt> lstFormulaDt = lstFinalMarkFormulaDt.Where(p => p.CurriculumFinalMarkFormulaID == entityFormulaHd.CurriculumFinalMarkFormulaID).OrderBy(p => p.DisplayOrder).ToList();
                    rptHeaderMarkType2Dt.DataSource = lstFormulaDt;
                    rptHeaderMarkType2Dt.DataBind();

                    HtmlGenericControl spnTotalPercentage = (HtmlGenericControl)e.Item.FindControl("spnTotalPercentage");
                    spnTotalPercentage.InnerHtml = lstFormulaDt.Sum(p => p.FinalMarkPercentage).ToString();
                }
                if (!entity.IsAllowTask)
                {
                    HtmlTableCell thFinalReadonlyMark = (HtmlTableCell)e.Item.FindControl("thFinalReadonlyMark");
                    thFinalReadonlyMark.Style.Add("display", "none");
                }
                if (entity.PredicateMarkTypeID == 0)
                {
                    HtmlTableCell thPredicateMark = (HtmlTableCell)e.Item.FindControl("thPredicateMark");
                    thPredicateMark.Style.Add("display", "none");
                }
            }
        }
        protected void rptHeaderMarkType2Dt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vCurriculumFinalMarkFormulaDt entityGroup = (vCurriculumFinalMarkFormulaDt)e.Item.DataItem;
                string[] lstMarkTypeID = entityGroup.ListMarkTypeID.Split(',');
                int taskCount = lstTaskCount.Where(p => p.CurriculumFinalMarkFormulaDtID == entityGroup.CurriculumFinalMarkFormulaDtID).Sum(p => p.Count);

                HtmlTableCell thHeaderTaskGroup = (HtmlTableCell)e.Item.FindControl("thHeaderTaskGroup");
                if (taskCount == 0)
                    thHeaderTaskGroup.Style.Add("display", "none");
                else
                    thHeaderTaskGroup.ColSpan = taskCount + 2;
                //vClassSubjectTaskCustom entityGroup = (vClassSubjectTaskCustom)e.Item.DataItem;

                //if (hdnGCClassStudyType.Value == Constant.ClassStudyType.EXTRACURRICULAR)
                //    thHeaderTheoryTaskGroup.ColSpan = lstTheory.Where(p => p.TheoryFinalMarkFormulaDtID == entityGroup.TheoryFinalMarkFormulaDtID).Count();
                //else
                //    thHeaderTheoryTaskGroup.ColSpan = lstTheory.Where(p => p.TheoryFinalMarkFormulaDtID == entityGroup.TheoryFinalMarkFormulaDtID).Count() + 2;
            }
        }

        protected void rptHeaderMarkType3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vCurriculumSubjectMarkType entity = (vCurriculumSubjectMarkType)e.Item.DataItem;

                vPeriodClassTypeSubjectFinalMarkFormulaCustom entityFormulaHd = lstFinalMarkFormulaHd.FirstOrDefault(p => p.CurriculumMarkTypeID == entity.CurriculumMarkTypeID);
                if (entityFormulaHd != null)
                {
                    Repeater rptHeaderMarkType3Dt1 = (Repeater)e.Item.FindControl("rptHeaderMarkType3Dt1");
                    List<vCurriculumFinalMarkFormulaDt> lstFormulaDt = lstFinalMarkFormulaDt.Where(p => p.CurriculumFinalMarkFormulaID == entityFormulaHd.CurriculumFinalMarkFormulaID).OrderBy(p => p.DisplayOrder).ToList();
                    rptHeaderMarkType3Dt1.DataSource = lstFormulaDt;
                    rptHeaderMarkType3Dt1.DataBind();
                    //HtmlGenericControl spnTotalTheoryPercentage = (HtmlGenericControl)e.Item.FindControl("spnTotalTheoryPercentage");
                    //spnTotalTheoryPercentage.InnerHtml = lstFormulaDt.Sum(p => p.FinalMarkPercentage).ToString();
                }
            }
        }

        protected void rptHeaderMarkType3Dt1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vCurriculumFinalMarkFormulaDt entityGroup = (vCurriculumFinalMarkFormulaDt)e.Item.DataItem;
                vCurriculumSubjectMarkType markType = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vCurriculumSubjectMarkType;
                string[] lstMarkTypeID = entityGroup.ListMarkTypeID.Split(',');
                Repeater rptHeaderMarkType3Dt2 = (Repeater)e.Item.FindControl("rptHeaderMarkType3Dt2");
                List<vClassSubjectTask> lstTask = lstClassTask.Where(p => lstMarkTypeID.Contains(p.CurriculumMarkTypeDtID.ToString())).ToList();
                rptHeaderMarkType3Dt2.DataSource = lstTask.OrderBy(p => p.TaskDate).ThenBy(p => p.ClassSubjectTaskID).ToList();
                rptHeaderMarkType3Dt2.DataBind();

                if (lstTask.Count > 0)
                    lstTaskCount.Add(new CTaskCount { CurriculumMarkTypeID = markType.CurriculumMarkTypeID, CurriculumFinalMarkFormulaDtID = entityGroup.CurriculumFinalMarkFormulaDtID, Count = lstTask.Count });
                else
                {
                    HtmlTableCell thAverageMark = (HtmlTableCell)e.Item.FindControl("thAverageMark");
                    HtmlTableCell thFinalMark = (HtmlTableCell)e.Item.FindControl("thFinalMark");
                    thAverageMark.Style.Add("display", "none");
                    thFinalMark.Style.Add("display", "none");
                }
            }
        }

        List<ClassStudentSubjectTaskGroupMark> lstStudentGroupMark = null;
        List<vClassStudentSubjectTaskMark> lstStudentMark = null;
        List<ClassStudentSubjectMark> lstStudentFinalMark = null;
        
        
        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassStudent entity = (vClassStudent)e.Item.DataItem;
                ClassStudentSubjectMark studentFinalMark = lstStudentFinalMark.FirstOrDefault(p => p.StudentID == entity.StudentID);
                //TextBox txtFinalStudentMark = (TextBox)e.Item.FindControl("txtFinalStudentMark");
                //txtFinalStudentMark.Attributes.Add("itemindex", e.Item.ItemIndex.ToString());

                //TextBox txtFinalStudentMarkPractice = (TextBox)e.Item.FindControl("txtFinalStudentMarkPractice");
                //txtFinalStudentMarkPractice.Attributes.Add("itemindex", e.Item.ItemIndex.ToString());

                ASPxComboBox cboStudentProgressRule = (ASPxComboBox)e.Item.FindControl("cboStudentProgressRule");
                cboStudentProgressRule.ClientInstanceName = string.Format("cboStudentProgressRule{0}", e.Item.ItemIndex);
                Methods.SetComboBoxField<StudentProgressRuleDt>(cboStudentProgressRule, lstProgress, "StudentProgressRuleDtName", "StudentProgressRuleDtID");
                cboStudentProgressRule.ClientSideEvents.ValueChanged = "function(s,e){ onCboStudentProgressRuleValueChanged(s, " + e.Item.ItemIndex + ",'" + entity.PreferredName + "'); }";

                if (studentFinalMark != null)
                {
                    TextBox txtProgressDescription = (TextBox)e.Item.FindControl("txtProgressDescription");
                    //txtFinalStudentMark.Text = studentFinalMark.TheoryMark.ToString();
                    //txtFinalStudentMarkPractice.Text = studentFinalMark.PracticeMark.ToString();
                    txtProgressDescription.Text = studentFinalMark.ProgressDescription;
                    cboStudentProgressRule.Value = studentFinalMark.StudentProgressRuleDtID.ToString();
                }

                Repeater rptStudentMarkType = (Repeater)e.Item.FindControl("rptStudentMarkType");
                rptStudentMarkType.DataSource = lstCurriculumMarkType;
                rptStudentMarkType.DataBind();

                //if (lstTheory.Count < 1)
                //{
                //    tdTotalStudentMarkTheory.Style.Add("display", "none");
                //    tdFinalStudentMarkTheory.Style.Add("display", "none");
                //}
                //if (lstPractice.Count < 1)
                //{
                //    tdTotalStudentMarkPractice.Style.Add("display", "none");
                //    tdFinalStudentMarkPractice.Style.Add("display", "none");
                //}

                //if (hdnGCClassStudyType.Value == Constant.ClassStudyType.EXTRACURRICULAR)
                //{
                //    HtmlTableCell tdStudentAffectiveMark = (HtmlTableCell)e.Item.FindControl("tdStudentAffectiveMark");
                //    HtmlTableCell tdStudentAffectiveDescription = (HtmlTableCell)e.Item.FindControl("tdStudentAffectiveDescription");
                //    tdStudentAffectiveMark.Style.Add("display", "none");
                //    tdStudentAffectiveDescription.Style.Add("display", "none");
                //    tdTotalStudentMarkPractice.Style.Add("display", "none");
                //    tdFinalStudentMarkPractice.Style.Add("display", "none");
                //    tdTotalStudentMarkTheory.Style.Add("display", "none");
                //    tdFinalStudentMarkTheory.Style.Add("display", "none");
                //}
            }
        }

        protected void rptStudentMarkType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vCurriculumSubjectMarkType entity = (vCurriculumSubjectMarkType)e.Item.DataItem;

                if (!entity.IsAllowTask)
                {
                    HtmlTableCell tdTotalStudentMark = (HtmlTableCell)e.Item.FindControl("tdTotalStudentMark");
                    tdTotalStudentMark.Style.Add("display", "none");
                }
                ASPxComboBox cboPredicateStudentMarkOption = (ASPxComboBox)e.Item.FindControl("cboPredicateStudentMarkOption");
                cboPredicateStudentMarkOption.ClientInstanceName = string.Format("cboPredicateStudentMarkOption{0}", e.Item.ItemIndex.ToString("D2"));
                if (entity.PredicateMarkTypeID > 0)
                {
                    List<MarkTypeDt> lstMarkTypeDt1 = lstMarkTypeDt.Where(p => p.MarkTypeID == entity.PredicateMarkTypeID).ToList();
                    Methods.SetComboBoxField<MarkTypeDt>(cboPredicateStudentMarkOption, lstMarkTypeDt1, "MarkTypeDtName", "MarkTypeDtID");
                }
                else
                {
                    HtmlTableCell tdPredicateStudentMark = (HtmlTableCell)e.Item.FindControl("tdPredicateStudentMark");
                    tdPredicateStudentMark.Style.Add("display", "none");
                }

                TextBox txtFinalStudentMark = (TextBox)e.Item.FindControl("txtFinalStudentMark");
                txtFinalStudentMark.Attributes.Add("curriculummarktypeid", entity.CurriculumMarkTypeID.ToString());

                ASPxComboBox cboFinalStudentMarkOption = (ASPxComboBox)e.Item.FindControl("cboFinalStudentMarkOption");
                TextBox txtFinalStudentMarkDescription = (TextBox)e.Item.FindControl("txtFinalStudentMarkDescription");

                cboFinalStudentMarkOption.ClientInstanceName = string.Format("cboFinalStudentMarkOption{0}", e.Item.ItemIndex.ToString("D2"));

                switch (entity.FinalGCMarkType)
                {
                    case Constant.SubjectMarkType.NUMBER: cboFinalStudentMarkOption.ClientVisible = false; txtFinalStudentMarkDescription.Style.Add("display", "none"); break;
                    case Constant.SubjectMarkType.OPTION:
                        txtFinalStudentMark.Style.Add("display", "none"); txtFinalStudentMarkDescription.Style.Add("display", "none");
                        List<MarkTypeDt> lstMarkTypeDt1 = lstMarkTypeDt.Where(p => p.MarkTypeID == entity.FinalMarkTypeID).ToList();
                        Methods.SetComboBoxField<MarkTypeDt>(cboFinalStudentMarkOption, lstMarkTypeDt1, "MarkTypeDtName", "MarkTypeDtID");
                        break;
                    case Constant.SubjectMarkType.TEXT: cboFinalStudentMarkOption.ClientVisible = false; txtFinalStudentMark.Style.Add("display", "none"); break;
                }

                vPeriodClassTypeSubjectFinalMarkFormulaCustom entityFormulaHd = lstFinalMarkFormulaHd.FirstOrDefault(p => p.CurriculumMarkTypeID == entity.CurriculumMarkTypeID);
                if (entityFormulaHd != null)
                {
                    Repeater rptStudentMarkGroup = (Repeater)e.Item.FindControl("rptStudentMarkGroup");
                    List<vCurriculumFinalMarkFormulaDt> lstFormulaDt = lstFinalMarkFormulaDt.Where(p => p.CurriculumFinalMarkFormulaID == entityFormulaHd.CurriculumFinalMarkFormulaID).OrderBy(p => p.DisplayOrder).ToList();
                    List<vCurriculumFinalMarkFormulaDt> lstFormulaDt1 = new List<vCurriculumFinalMarkFormulaDt>();
                    foreach (CTaskCount taskCount in lstTaskCount)
                    {
                        vCurriculumFinalMarkFormulaDt entityFormulaDt = lstFormulaDt.FirstOrDefault(p => p.CurriculumFinalMarkFormulaDtID == taskCount.CurriculumFinalMarkFormulaDtID);
                        if (entityFormulaDt != null)
                            lstFormulaDt1.Add(entityFormulaDt);
                    }

                    rptStudentMarkGroup.DataSource = lstFormulaDt1;
                    rptStudentMarkGroup.DataBind();
                    //HtmlGenericControl spnTotalTheoryPercentage = (HtmlGenericControl)e.Item.FindControl("spnTotalTheoryPercentage");
                    //spnTotalTheoryPercentage.InnerHtml = lstFormulaDt.Sum(p => p.FinalMarkPercentage).ToString();
                }
                else
                {
                    //HtmlTableCell thFinalMark = (HtmlTableCell)e.Item.FindControl("thFinalMark");
                    //HtmlTableCell thFinalReadonlyMark = (HtmlTableCell)e.Item.FindControl("thFinalReadonlyMark");
                    //thFinalMark.Style.Add("display", "none");
                    //thFinalReadonlyMark.Style.Add("display", "none");
                }
            }
        }

        #region Repeater Inside Student
        #region Theory
        protected void rptStudentMarkGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vCurriculumFinalMarkFormulaDt entityGroup = (vCurriculumFinalMarkFormulaDt)e.Item.DataItem;
                vCurriculumSubjectMarkType markType = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vCurriculumSubjectMarkType;
                vClassStudent student = ((RepeaterItem)((RepeaterItem)e.Item.Parent.Parent).Parent.Parent).DataItem as vClassStudent;
                Repeater rptStudentMark = (Repeater)e.Item.FindControl("rptStudentMark");
                string[] lstMarkTypeID = entityGroup.ListMarkTypeID.Split(',');
                rptStudentMark.DataSource = lstClassTask.Where(p => lstMarkTypeID.Contains(p.CurriculumMarkTypeDtID.ToString())).OrderBy(p => p.TaskDate).ThenBy(p => p.ClassSubjectTaskID).ToList();
                rptStudentMark.DataBind();

                TextBox txtFinalStudentMarkGroup = (TextBox)e.Item.FindControl("txtFinalStudentMarkGroup");
                txtFinalStudentMarkGroup.Attributes.Add("formulapercentage", entityGroup.FinalMarkPercentage.ToString());
                txtFinalStudentMarkGroup.Attributes.Add("formuladtid", entityGroup.CurriculumFinalMarkFormulaDtID.ToString());
                txtFinalStudentMarkGroup.Attributes.Add("curriculummarktypeid", markType.CurriculumMarkTypeID.ToString());
                ClassStudentSubjectTaskGroupMark entityMark = lstStudentGroupMark.FirstOrDefault(p => p.StudentFinalMarkFormulaDtID == entityGroup.CurriculumFinalMarkFormulaDtID && p.StudentID == student.StudentID);
                if (entityMark != null)
                    txtFinalStudentMarkGroup.Text = entityMark.Mark.ToString();

                //if (hdnGCClassStudyType.Value == Constant.ClassStudyType.EXTRACURRICULAR)
                //{
                //    HtmlTableCell tdAverageStudentMarkGroup = (HtmlTableCell)e.Item.FindControl("tdAverageStudentMarkGroup");
                //    HtmlTableCell tdFinalStudentMarkGroup = (HtmlTableCell)e.Item.FindControl("tdFinalStudentMarkGroup");
                //    tdAverageStudentMarkGroup.Style.Add("display", "none");
                //    tdFinalStudentMarkGroup.Style.Add("display", "none");
                //}
            }
        }

        protected void rptStudentMark_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTask subjectTask = (vClassSubjectTask)e.Item.DataItem;
                vCurriculumFinalMarkFormulaDt entityFormula = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vCurriculumFinalMarkFormulaDt;
                vCurriculumSubjectMarkType markType = ((RepeaterItem)((RepeaterItem)e.Item.Parent.Parent).Parent.Parent).DataItem as vCurriculumSubjectMarkType;
                vClassStudent student = ((RepeaterItem)((RepeaterItem)((RepeaterItem)e.Item.Parent.Parent).Parent.Parent).Parent.Parent).DataItem as vClassStudent;

                TextBox txtStudentMark = (TextBox)e.Item.FindControl("txtStudentMark");
                ASPxComboBox cboStudentMarkOption = (ASPxComboBox)e.Item.FindControl("cboStudentMarkOption");
                TextBox txtStudentMarkDescription = (TextBox)e.Item.FindControl("txtStudentMarkDescription");
                vClassStudentSubjectTaskMark studentMark = lstStudentMark.FirstOrDefault(p => p.ClassSubjectTaskID == subjectTask.ClassSubjectTaskID && p.StudentID == student.StudentID);

                int parentIndex = ((RepeaterItem)e.Item.Parent.Parent).ItemIndex;
                cboStudentMarkOption.ClientInstanceName = string.Format("cboStudentMarkOption{0}{1}", parentIndex.ToString("D2"), e.Item.ItemIndex.ToString("D2"));
                txtStudentMark.Attributes.Add("positiontag", string.Format("{0}{1}", parentIndex.ToString("D2"), e.Item.ItemIndex.ToString("D2")));
                txtStudentMark.Attributes.Add("formuladtid", entityFormula.CurriculumFinalMarkFormulaDtID.ToString());
                HtmlGenericControl divMark = (HtmlGenericControl)e.Item.FindControl("divMark");
                switch (markType.TaskGCMarkType)
                {
                    case Constant.SubjectMarkType.NUMBER: cboStudentMarkOption.ClientVisible = false; txtStudentMarkDescription.Style.Add("display", "none"); break;
                    case Constant.SubjectMarkType.OPTION:
                        divMark.Style.Add("display", "none"); 
                        txtStudentMark.Style.Add("display", "none"); txtStudentMarkDescription.Style.Add("display", "none");
                        List<MarkTypeDt> lstMarkTypeDt1 = lstMarkTypeDt.Where(p => p.MarkTypeID == markType.TaskMarkTypeID).ToList();
                        Methods.SetComboBoxField<MarkTypeDt>(cboStudentMarkOption, lstMarkTypeDt1, "MarkTypeDtName", "MarkTypeDtID");
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
                            if (temp[2] != "-")
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

                                    //if (hdnGCSubjectMarkType.Value == Constant.SubjectMarkType.NUMBER)
                                    //{
                                    //    Decimal mark = -1;
                                    //    if (saveValue2 != "-")
                                    //        mark = Convert.ToDecimal(saveValue2);
                                    //    if (studentMark == null)
                                    //    {
                                    //        if (mark > -1)
                                    //        {
                                    //            studentMark = new ClassStudentSubjectTaskMark();
                                    //            studentMark.StudentID = studentID;
                                    //            studentMark.ClassSubjectTaskID = ClassSubjectTaskID;
                                    //            studentMark.Mark = mark;
                                    //            entityStudentSubjectTaskMarkDao.Insert(studentMark);
                                    //        }
                                    //    }
                                    //    else if (studentMark.Mark != mark)
                                    //    {
                                    //        if (mark > -1)
                                    //        {
                                    //            studentMark.Mark = mark;
                                    //            entityStudentSubjectTaskMarkDao.Update(studentMark);
                                    //        }
                                    //        else
                                    //            entityStudentSubjectTaskMarkDao.Delete(ClassSubjectTaskID, studentID);
                                    //    }
                                    //}
                                    //else if (hdnGCSubjectMarkType.Value == Constant.SubjectMarkType.OPTION)
                                    //{
                                    //    if (studentMark == null)
                                    //    {
                                    //        if (saveValue2 != "")
                                    //        {
                                    //            studentMark = new ClassStudentSubjectTaskMark();
                                    //            studentMark.StudentID = studentID;
                                    //            studentMark.ClassSubjectTaskID = ClassSubjectTaskID;
                                    //            studentMark.StudentProgressRuleDtID = Convert.ToInt32(saveValue2);
                                    //            entityStudentSubjectTaskMarkDao.Insert(studentMark);
                                    //        }
                                    //    }
                                    //    else if (studentMark.StudentProgressRuleDtID.ToString() != saveValue2)
                                    //    {
                                    //        if (saveValue2 != "")
                                    //        {
                                    //            studentMark.StudentProgressRuleDtID = Convert.ToInt32(saveValue2);
                                    //            entityStudentSubjectTaskMarkDao.Update(studentMark);
                                    //        }
                                    //        else
                                    //            entityStudentSubjectTaskMarkDao.Delete(ClassSubjectTaskID, studentID);
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    if (studentMark == null)
                                    //    {
                                    //        if (saveValue2 != "")
                                    //        {
                                    //            studentMark = new ClassStudentSubjectTaskMark();
                                    //            studentMark.StudentID = studentID;
                                    //            studentMark.ClassSubjectTaskID = ClassSubjectTaskID;
                                    //            studentMark.DescriptionMark = saveValue2;
                                    //            entityStudentSubjectTaskMarkDao.Insert(studentMark);
                                    //        }
                                    //    }
                                    //    else if (studentMark.DescriptionMark != saveValue2)
                                    //    {
                                    //        if (saveValue2 != "")
                                    //        {
                                    //            studentMark.DescriptionMark = saveValue2;
                                    //            entityStudentSubjectTaskMarkDao.Update(studentMark);
                                    //        }
                                    //        else
                                    //            entityStudentSubjectTaskMarkDao.Delete(ClassSubjectTaskID, studentID);
                                    //    }
                                    //}
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