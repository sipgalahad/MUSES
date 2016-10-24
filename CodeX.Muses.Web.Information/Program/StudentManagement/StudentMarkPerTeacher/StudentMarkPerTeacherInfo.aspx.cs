using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using DevExpress.Utils;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;
using DevExpress.Web.ASPxEditors;
using CodeX.Common;
using System.Web.UI.HtmlControls;


namespace CodeX.Muses.Web.Information.Program
{
    public partial class StudentMarkPerTeacherInfo : BasePageList
    {  
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.STUDENT_MARK_PER_TEACHER_INFO;
        }

        protected string OnGetSchoolPeriodNowFilterExpression()
        {
            return string.Format("GCSchoolPeriodStatus != '{0}' AND StartDate <= '{1}' AND EndDate >= '{1}'", Constant.SchoolPeriodStatus.VOID, DateTime.Now.ToString("yyyyMMdd"));
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<vSite> lstSite = BusinessLayer.GetvSiteList(String.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsHeader = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<vSite>(cboSite, lstSite, "SiteName", "SiteID");
            cboSite.SelectedIndex = 0;
        }

        List<ClassSubjectTask> lstClassSubjectTask = null;
        List<vClassStudentSubjectTaskMark> lstTaskMark = null;
        List<ClassStudent> lstStudent = null;
        private void BindGridView()
        {
            string filterExpression = "1 = 0";
            if (tacTeacher.Value != "" && tacSchoolPeriod.Value != "")
                filterExpression = string.Format("SchoolPeriodID = {0} AND (TeacherID = {1} OR AssistantTeacherID = {1})", tacSchoolPeriod.Value, tacTeacher.Value);
            List<vTeacherClassSubject> lstEntity = BusinessLayer.GetvTeacherClassSubjectList(filterExpression);

            if (lstEntity.Count > 0)
            {
                string lstClassSubjectID = string.Join(",", lstEntity.Select(p => p.ClassSubjectID).ToList());
                string lstSchoolClassID = string.Join(",", lstEntity.Select(p => p.SchoolClassID).ToList());
                lstClassSubjectTask = BusinessLayer.GetClassSubjectTaskList(string.Format("ClassSubjectID IN ({0}) AND IsDeleted = 0", lstClassSubjectID));
                lstStudent = BusinessLayer.GetClassStudentList(string.Format("SchoolClassID IN ({0})", lstSchoolClassID));
                if (lstClassSubjectTask.Count > 0)
                {
                    string lstClassSubjectTaskID = string.Join(",", lstClassSubjectTask.Select(p => p.ClassSubjectTaskID).ToList());
                    lstTaskMark = BusinessLayer.GetvClassStudentSubjectTaskMarkList(string.Format("ClassSubjectTaskID IN ({0})", lstClassSubjectTaskID));
                }
            }
            else
                lstClassSubjectTask = new List<ClassSubjectTask>();

            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vTeacherClassSubject entity = (vTeacherClassSubject)e.Row.DataItem;
                HtmlGenericControl divTaskCount = (HtmlGenericControl)e.Row.FindControl("divTaskCount");
                HtmlGenericControl divBelowPassingGradeCount = (HtmlGenericControl)e.Row.FindControl("divBelowPassingGradeCount");
                HtmlGenericControl divStudentCount = (HtmlGenericControl)e.Row.FindControl("divStudentCount");
                divTaskCount.InnerHtml = lstClassSubjectTask.Count(p => p.ClassSubjectID == entity.ClassSubjectID).ToString();
                divBelowPassingGradeCount.InnerHtml = lstTaskMark.Where(p => p.ClassSubjectID == entity.ClassSubjectID && p.Mark < entity.PassingGrade).GroupBy(p => new { p.StudentID }).Select(p => p.First()).Count().ToString();
                divStudentCount.InnerHtml = lstStudent.Count(p => p.SchoolClassID == entity.SchoolClassID).ToString();
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}