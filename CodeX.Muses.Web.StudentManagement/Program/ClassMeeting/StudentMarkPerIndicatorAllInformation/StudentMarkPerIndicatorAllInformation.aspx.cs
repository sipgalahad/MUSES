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
    public partial class StudentMarkPerIndicatorAllInformation : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.WS_STUDENT_MARK_PER_INDICATOR_ALL;
        }

        List<vClassSubjectTask> lstClassTask = null;
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
            hdnLessonType.Value = cboLessonType.Value.ToString();

            BindGridView();
        }

        private void BindGridView()
        {
            string lessonType = cboLessonType.Value.ToString();
            if (lessonType == "")
                lessonType = Request.Form[hdnLessonType.UniqueID];
            lstClassSubjectTaskIndicator = BusinessLayer.GetvClassSubjectTaskIndicatorList(string.Format("ClassSubjectID = {0} AND CurriculumMarkTypeID = {1} AND IsDeleted = 0", AppSession.ClassSubject.ClassSubjectID, lessonType));
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

            lstClassTask = BusinessLayer.GetvClassSubjectTaskList(string.Format("ClassSubjectID = {0} AND IsDeleted = 0", AppSession.ClassSubject.ClassSubjectID));

            totalColSpan = 0;
            rptSubjectIndicatorHeader.DataSource = lstIndicator;
            rptSubjectIndicatorHeader.DataBind();
            rptSubjectIndicatorHeader2.DataSource = lstIndicator;
            rptSubjectIndicatorHeader2.DataBind();

            thMark.ColSpan = totalColSpan;

            lstStudentMark = BusinessLayer.GetvClassStudentSubjectTaskMarkList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID));

            vClassSubject classSubject = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID)).FirstOrDefault();
            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", classSubject.SchoolClassID));
            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();
        }

        int totalColSpan = 0;

        protected void rptSubjectIndicatorHeader_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTaskIndicator indicator = (vClassSubjectTaskIndicator)e.Item.DataItem;

                List<vClassSubjectTaskIndicator> classSubjectTask = lstClassSubjectTaskIndicator.Where(p => p.SubjectIndicatorID == indicator.SubjectIndicatorID).ToList();
                HtmlTableCell thSubjectIndicator = (HtmlTableCell)e.Item.FindControl("thSubjectIndicator");
                thSubjectIndicator.ColSpan = classSubjectTask.Count;
                totalColSpan += classSubjectTask.Count;
            }
        }

        protected void rptSubjectIndicatorHeader2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTaskIndicator indicator = (vClassSubjectTaskIndicator)e.Item.DataItem;

                List<vClassSubjectTaskIndicator> classSubjectTask = lstClassSubjectTaskIndicator.Where(p => p.SubjectIndicatorID == indicator.SubjectIndicatorID).ToList();
                Repeater rptClassTaskHeader = (Repeater)e.Item.FindControl("rptClassTaskHeader");
                rptClassTaskHeader.DataSource = classSubjectTask;
                rptClassTaskHeader.DataBind();
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        List<vClassStudentSubjectTaskMark> lstStudentMark = null;
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

                List<vClassSubjectTaskIndicator> classSubjectTask = lstClassSubjectTaskIndicator.Where(p => p.SubjectIndicatorID == indicator.SubjectIndicatorID).ToList();
                Repeater rptStudentMark = (Repeater)e.Item.FindControl("rptStudentMark");
                rptStudentMark.DataSource = classSubjectTask;
                rptStudentMark.DataBind();
            }
        }

        protected void rptStudentMark_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTaskIndicator subjectTask = (vClassSubjectTaskIndicator)e.Item.DataItem;
                vClassStudent student = ((RepeaterItem)e.Item.Parent.Parent.Parent.Parent).DataItem as vClassStudent;

                vClassStudentSubjectTaskMark entity = lstStudentMark.FirstOrDefault(p => p.ClassSubjectTaskID == subjectTask.ClassSubjectTaskID && p.StudentID == student.StudentID);
                if (entity != null)
                {
                    HtmlGenericControl divStudentMark = (HtmlGenericControl)e.Item.FindControl("divStudentMark");
                    switch (subjectTask.GCMarkType)
                    {
                        case Constant.SubjectMarkType.NUMBER: divStudentMark.InnerHtml = entity.Mark.ToString(); break;
                        case Constant.SubjectMarkType.OPTION: divStudentMark.InnerHtml = entity.MarkTypeDtName; break;
                        case Constant.SubjectMarkType.TEXT: divStudentMark.InnerHtml = entity.DescriptionMark; break;
                    }
                }
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        public override Control OnGetExportControl()
        {
            string lessonType = cboLessonType.Value.ToString();
            if (lessonType == "")
                lessonType = Request.Form[hdnLessonType.UniqueID];
            lstClassSubjectTaskIndicator = BusinessLayer.GetvClassSubjectTaskIndicatorList(string.Format("ClassSubjectID = {0} AND CurriculumMarkTypeID = {1} AND IsDeleted = 0", AppSession.ClassSubject.ClassSubjectID, lessonType));
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

            lstClassTask = BusinessLayer.GetvClassSubjectTaskList(string.Format("ClassSubjectID = {0} AND IsDeleted = 0", AppSession.ClassSubject.ClassSubjectID));

            totalColSpan = 0;
            rptSubjectIndicatorHeaderPrint.DataSource = lstIndicator;
            rptSubjectIndicatorHeaderPrint.DataBind();
            rptSubjectIndicatorHeaderPrint2.DataSource = lstIndicator;
            rptSubjectIndicatorHeaderPrint2.DataBind();

            thMark.ColSpan = totalColSpan;

            lstStudentMark = BusinessLayer.GetvClassStudentSubjectTaskMarkList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID));

            vClassSubject classSubject = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID)).FirstOrDefault();
            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", classSubject.SchoolClassID));
            rptStudentPrint.DataSource = lstStudent;
            rptStudentPrint.DataBind();

            HtmlGenericControl div = new HtmlGenericControl("DIV");
            div.Controls.Add(pnlPrint);
            return div;
        }
    }
}