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
    public partial class StudentMarkPerClassInfo : BasePageList
    {  
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.STUDENT_MARK_PER_CLASS_INFO;
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

        List<vClassSubjectTask> lstClassSubjectTask = null;
        List<vClassStudentSubjectTaskMark> lstTaskMark = null;
        List<ClassStudent> lstStudent = null;
        private void BindGridView()
        {
            string filterExpression = "1 = 0";
            if (tacSchoolPeriod.Value != "")
                filterExpression = string.Format("SchoolPeriodID = {0} AND IsDeleted = 0", tacSchoolPeriod.Value);
            List<vSchoolClass> lstEntity = BusinessLayer.GetvSchoolClassList(filterExpression);

            if (lstEntity.Count > 0)
            {
                string lstSchoolClassID = string.Join(",", lstEntity.Select(p => p.SchoolClassID).ToList());
                lstClassSubjectTask = BusinessLayer.GetvClassSubjectTaskList(string.Format("SchoolClassID IN ({0}) AND IsDeleted = 0", lstSchoolClassID));
                lstStudent = BusinessLayer.GetClassStudentList(string.Format("SchoolClassID IN ({0})", lstSchoolClassID));
                if (lstClassSubjectTask.Count > 0)
                {
                    string lstClassSubjectTaskID = string.Join(",", lstClassSubjectTask.Select(p => p.ClassSubjectTaskID).ToList());
                    lstTaskMark = BusinessLayer.GetvClassStudentSubjectTaskMarkList(string.Format("ClassSubjectTaskID IN ({0})", lstClassSubjectTaskID));
                }
                else
                    lstTaskMark = new List<vClassStudentSubjectTaskMark>();
            }
            else
                lstClassSubjectTask = new List<vClassSubjectTask>();

            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vSchoolClass entity = (vSchoolClass)e.Row.DataItem;
                HtmlGenericControl divTaskCount = (HtmlGenericControl)e.Row.FindControl("divTaskCount");
                HtmlGenericControl divBelowPassingGradeCount = (HtmlGenericControl)e.Row.FindControl("divBelowPassingGradeCount");
                HtmlGenericControl divStudentCount = (HtmlGenericControl)e.Row.FindControl("divStudentCount");
                divTaskCount.InnerHtml = lstClassSubjectTask.Count(p => p.SchoolClassID == entity.SchoolClassID).ToString();
                divBelowPassingGradeCount.InnerHtml = lstTaskMark.Where(p => p.SchoolClassID == entity.SchoolClassID && p.Mark < p.PassingGrade).GroupBy(p => new { p.StudentID }).Select(p => p.First()).Count().ToString();
                divStudentCount.InnerHtml = lstStudent.Count(p => p.SchoolClassID == entity.SchoolClassID).ToString();
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}