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
    public partial class ClassTaskPerIndicatorInformation : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.WS_CLASS_TASK_PER_INDICATOR;
        }

        List<vClassStudentSubjectTaskMark> lstStudentMark = null;
        List<vClassSubjectTask> lstClassSubjectTask = null;
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

            lstClassSubjectTaskIndicator = BusinessLayer.GetvClassSubjectTaskIndicatorList(string.Format("ClassSubjectID = {0} AND IsDeleted = 0", AppSession.ClassSubject.ClassSubjectID));
            List<vClassSubjectTaskIndicator> lstIndicator = (from p in lstClassSubjectTaskIndicator
                            select new vClassSubjectTaskIndicator { SubjectIndicatorID = p.SubjectIndicatorID, SubjectIndicatorName = p.SubjectIndicatorName, CurriculumMarkTypeID = p.CurriculumMarkTypeID }).GroupBy(p => new { p.SubjectIndicatorID, p.SubjectIndicatorName, p.CurriculumMarkTypeID }).Select(p => p.First()).ToList();

            string filterExpression = string.Format("PeriodSectionID = {0} AND ClassSubjectID = {1}", AppSession.ClassSubject.PeriodSectionID, AppSession.ClassSubject.ClassSubjectID);
            lstClassSubjectTask = BusinessLayer.GetvClassSubjectTaskList(filterExpression);

            lstStudentMark = BusinessLayer.GetvClassStudentSubjectTaskMarkList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID));
            thClassTask.ColSpan = lstClassSubjectTask.Count;

            rptClassTaskHeader.DataSource = lstClassSubjectTask;
            rptClassTaskHeader.DataBind();

            rptSubjectIndicator.DataSource = lstIndicator;
            rptSubjectIndicator.DataBind();
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        protected void rptSubjectIndicator_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTaskIndicator indicator = (vClassSubjectTaskIndicator)e.Item.DataItem;

                Repeater rptClassTask = (Repeater)e.Item.FindControl("rptClassTask");
                rptClassTask.DataSource = lstClassSubjectTask;
                rptClassTask.DataBind();

                HtmlTableCell tdAverage = (HtmlTableCell)e.Item.FindControl("tdAverage");
                HtmlTableCell tdMax = (HtmlTableCell)e.Item.FindControl("tdMax");
                HtmlTableCell tdMin = (HtmlTableCell)e.Item.FindControl("tdMin");

                List<vClassSubjectTaskIndicator> lstClassSubjectTask1 = lstClassSubjectTaskIndicator.Where(p => p.SubjectIndicatorID == indicator.SubjectIndicatorID).ToList();
                List<vClassStudentSubjectTaskMark> lstStudentMark1 = lstStudentMark.Where(p => lstClassSubjectTask1.Any(q => q.ClassSubjectTaskID == p.ClassSubjectTaskID)).ToList();
                if (lstStudentMark1.Count == 0)
                {
                    tdAverage.InnerHtml = "-";
                    tdMax.InnerHtml = "-";
                    tdMin.InnerHtml = "-";
                }
                else
                {
                    tdAverage.InnerHtml = lstStudentMark1.Average(p => p.Mark).ToString();
                    tdMax.InnerHtml = lstStudentMark1.Max(p => p.Mark).ToString();
                    tdMin.InnerHtml = lstStudentMark1.Min(p => p.Mark).ToString();
                }
            }
        }

        protected void rptClassTask_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTask subjectTask = (vClassSubjectTask)e.Item.DataItem;
                vClassSubjectTaskIndicator indicator = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vClassSubjectTaskIndicator;

                if(lstClassSubjectTaskIndicator.Count(p => p.ClassSubjectTaskID == subjectTask.ClassSubjectTaskID && p.SubjectIndicatorID == indicator.SubjectIndicatorID) == 0)
                {
                    HtmlGenericControl divClassTask = (HtmlGenericControl)e.Item.FindControl("divClassTask");
                    divClassTask.Style.Add("display", "none");
                }
            }
        }
    }
}