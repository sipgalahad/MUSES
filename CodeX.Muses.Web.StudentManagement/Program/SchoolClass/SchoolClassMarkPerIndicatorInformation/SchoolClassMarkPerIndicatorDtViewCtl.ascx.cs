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
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class SchoolClassMarkPerIndicatorDtViewCtl : BaseViewPopupCtl
    {
        List<vClassSubjectTaskIndicator> lstClassSubjectTaskIndicator = null;
        List<vClassSubjectTask> lstClassSubjectTask = null;
        List<vClassStudentSubjectTaskMark> lstStudentMark = null;
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');
            hdnStudentID.Value = temp[0];
            hdnCurriculumMarkTypeID.Value = temp[1];
            hdnSummaryType.Value = temp[2];
            hdnClassSubjectID.Value = temp[3];

            Student entity = BusinessLayer.GetStudent(Convert.ToInt32(hdnStudentID.Value));
            txtHeaderName.Text = entity.StudentName;

            lstClassSubjectTaskIndicator = BusinessLayer.GetvClassSubjectTaskIndicatorList(string.Format("ClassSubjectID = {0} AND CurriculumMarkTypeID = {1} AND IsDeleted = 0", hdnClassSubjectID.Value, hdnCurriculumMarkTypeID.Value));
            List<vClassSubjectTaskIndicator> lstIndicator = (from p in lstClassSubjectTaskIndicator
                                                             select new vClassSubjectTaskIndicator { SubjectIndicatorID = p.SubjectIndicatorID, SubjectIndicatorName = p.SubjectIndicatorName, CurriculumMarkTypeID = p.CurriculumMarkTypeID }).GroupBy(p => new { p.SubjectIndicatorID, p.SubjectIndicatorName, p.CurriculumMarkTypeID }).Select(p => p.First()).ToList();

            string filterExpression = string.Format("PeriodSectionID = {0} AND ClassSubjectID = {1} AND CurriculumMarkTypeID = {2}", AppSession.SchoolClass.PeriodSectionID, hdnClassSubjectID.Value, hdnCurriculumMarkTypeID.Value);
            lstClassSubjectTask = BusinessLayer.GetvClassSubjectTaskList(filterExpression);

            if (lstClassSubjectTask.Count > 0)
            {
                string lstClassTaskID = String.Join(",", lstClassSubjectTask.Select(p => p.ClassSubjectTaskID).ToList());
                lstStudentMark = BusinessLayer.GetvClassStudentSubjectTaskMarkList(string.Format("ClassSubjectTaskID IN ({0}) AND StudentID = {1}", lstClassTaskID, hdnStudentID.Value));
            }
            else
                lstStudentMark = new List<vClassStudentSubjectTaskMark>();

            if (hdnSummaryType.Value == Constant.FinalMarkSummaryType.AVERAGE)
                thFinalMarkHeader.InnerHtml = "Nilai<br/>(Rata-Rata)";
            else
                thFinalMarkHeader.InnerHtml = "Nilai<br/>(Tertinggi)";

            tdClassTask.ColSpan = lstClassSubjectTask.Count;
            rptClassTaskHeader.DataSource = lstClassSubjectTask;
            rptClassTaskHeader.DataBind();

            rptSubjectIndicator.DataSource = lstIndicator;
            rptSubjectIndicator.DataBind();
        }

        decimal maxMark = -1;
        decimal totalMark = -1;
        decimal countMark = -1;
        protected void rptSubjectIndicator_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                maxMark = -1;
                totalMark = 0;
                countMark = 0;

                Repeater rptClassTask = (Repeater)e.Item.FindControl("rptClassTask");
                rptClassTask.DataSource = lstClassSubjectTask;
                rptClassTask.DataBind();

                HtmlTableCell tdFinalMark = (HtmlTableCell)e.Item.FindControl("tdFinalMark");
                if (hdnSummaryType.Value == Constant.FinalMarkSummaryType.AVERAGE)
                {
                    if (countMark == 0)
                        tdFinalMark.InnerHtml = "-";
                    else
                        tdFinalMark.InnerHtml = (totalMark / countMark).ToString("N");
                }
                else
                    tdFinalMark.InnerHtml = maxMark.ToString("N");
            }
        }

        protected void rptClassTask_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTask subjectTask = (vClassSubjectTask)e.Item.DataItem;
                vClassSubjectTaskIndicator indicator = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vClassSubjectTaskIndicator;

                HtmlGenericControl divClassTask = (HtmlGenericControl)e.Item.FindControl("divClassTask");
                if (lstClassSubjectTaskIndicator.Count(p => p.ClassSubjectTaskID == subjectTask.ClassSubjectTaskID && p.SubjectIndicatorID == indicator.SubjectIndicatorID) > 0)
                {
                    vClassStudentSubjectTaskMark studentMark1 = lstStudentMark.FirstOrDefault(p => p.ClassSubjectTaskID == subjectTask.ClassSubjectTaskID);
                    if (studentMark1 != null)
                    {
                        if (hdnSummaryType.Value == Constant.FinalMarkSummaryType.AVERAGE)
                        {
                            countMark++;
                            totalMark += studentMark1.Mark;
                        }
                        else
                        {
                            if (studentMark1.Mark > maxMark)
                                maxMark = studentMark1.Mark; 
                        }

                        divClassTask.InnerHtml = studentMark1.Mark.ToString();
                    }
                    else
                        divClassTask.InnerHtml = "-";
                }
                else
                    divClassTask.InnerHtml = "-";
            }
        }
    }
}