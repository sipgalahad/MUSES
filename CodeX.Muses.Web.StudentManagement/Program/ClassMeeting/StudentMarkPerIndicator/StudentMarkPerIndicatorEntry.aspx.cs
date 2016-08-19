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
    public partial class StudentMarkPerIndicatorEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.TSC_STUDENT_MARK_PER_INDICATOR;
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

        decimal finalMark = 0;
        decimal minMark = 0;
        decimal maxMark = 0;
        string minMarkDesc = "";
        string maxMarkDesc = "";
        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                finalMark = 0;
                minMark = 999999999;
                maxMark = 0;
                minMarkDesc = "";
                maxMarkDesc = "";
                Repeater rptSubjectIndicator = (Repeater)e.Item.FindControl("rptSubjectIndicator");
                rptSubjectIndicator.DataSource = lstIndicator;
                rptSubjectIndicator.DataBind();

                HtmlGenericControl divStudentFinalMark = (HtmlGenericControl)e.Item.FindControl("divStudentFinalMark");
                HtmlInputHidden hdnMinDesc = (HtmlInputHidden)e.Item.FindControl("hdnMinDesc");
                HtmlInputHidden hdnMaxDesc = (HtmlInputHidden)e.Item.FindControl("hdnMaxDesc");
                decimal avg = 0;
                if (lstIndicator.Count > 0)
                    avg = finalMark / lstIndicator.Count;
                divStudentFinalMark.InnerHtml = avg.ToString("N");
                hdnMinDesc.Value = minMarkDesc;
                hdnMaxDesc.Value = maxMarkDesc;
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
                    decimal mark = 0;
                    if (hdnSummaryType.Value == Constant.FinalMarkSummaryType.AVERAGE)
                        mark = lstStudentMark1.Average(p => p.Mark); 
                    else
                        mark = lstStudentMark1.Max(p => p.Mark);

                    if (mark > maxMark)
                    {
                        maxMark = mark;
                        maxMarkDesc = indicator.SubjectIndicatorName;
                    }
                    if (mark < minMark)
                    {
                        minMark = mark;
                        minMarkDesc = indicator.SubjectIndicatorName;
                    }

                    divStudentMark.InnerHtml = mark.ToString("N");
                    finalMark += mark;
                }
                else
                    divStudentMark.InnerHtml = "-";
            }
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ClassStudentSubjectMarkDao entityStudentSubjectMarkDao = new ClassStudentSubjectMarkDao(ctx);
            try
            {
                int ClassSubjectID = Convert.ToInt32(hdnParentClassSubjectID.Value);
                int markType = Convert.ToInt32(cboLessonType.Value);
                List<ClassStudentSubjectMark> lstStudentFinalMark = BusinessLayer.GetClassStudentSubjectMarkList(string.Format("PeriodSectionID = {0} AND ClassSubjectID = {1} AND CurriculumMarkTypeID = {2}", AppSession.ClassSubject.PeriodSectionID, hdnParentClassSubjectID.Value, cboLessonType.Value), ctx);
                string[] lstSaveValue = hdnListSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    int studentID = Convert.ToInt32(temp[0]);
                    decimal mark = Convert.ToDecimal(temp[1]);
                    string minMarkDesc = temp[2];
                    string maxMarkDesc = temp[3];

                    ClassStudentSubjectMark studentFinalMark = lstStudentFinalMark.FirstOrDefault(p => p.StudentID == studentID);
                    if (studentFinalMark == null)
                    {
                        studentFinalMark = new ClassStudentSubjectMark();
                        studentFinalMark.ClassSubjectID = ClassSubjectID;
                        studentFinalMark.StudentID = studentID;
                        studentFinalMark.PeriodSectionID = AppSession.ClassSubject.PeriodSectionID;
                        studentFinalMark.CurriculumMarkTypeID = markType;
                        studentFinalMark.Mark = mark;
                        studentFinalMark.CompetencyDescription = string.Format("Memiliki kemampuan {0}, namun perlu peningkatan {1}", minMarkDesc, maxMarkDesc);
                        entityStudentSubjectMarkDao.Insert(studentFinalMark);
                    }
                    else
                    {
                        studentFinalMark.Mark = mark;
                        studentFinalMark.CompetencyDescription = string.Format("Memiliki kemampuan {0}, namun perlu peningkatan {1}", minMarkDesc, maxMarkDesc);
                        entityStudentSubjectMarkDao.Update(studentFinalMark);
                        lstStudentFinalMark.Remove(studentFinalMark);
                    }
                }
                foreach (ClassStudentSubjectMark studentFinalMark in lstStudentFinalMark)
                {
                    entityStudentSubjectMarkDao.Delete(studentFinalMark.ClassSubjectID, studentFinalMark.StudentID, studentFinalMark.PeriodSectionID, studentFinalMark.CurriculumMarkTypeID);
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
    }
}