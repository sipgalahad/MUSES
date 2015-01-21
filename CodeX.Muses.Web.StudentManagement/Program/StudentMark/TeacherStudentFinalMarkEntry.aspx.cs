using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class TeacherStudentFinalMarkEntry : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.TEACHER_STUDENT_FINAL_MARK;
        }

        protected string OnGetPeriodSectionFilterExpression()
        {
            return string.Format("GCPeriodSectionStatus != '{0}'", Constant.SchoolPeriodStatus.VOID);
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<SchoolPeriod> lstSchoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SiteID = '{0}' AND GCSchoolPeriodStatus != '{1}'", AppSession.UserLogin.SiteID, Constant.SchoolPeriodStatus.VOID));
            Methods.SetComboBoxField<SchoolPeriod>(cboSchoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
            SchoolPeriod selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);
            if (selectedSchoolPeriod == null)
                cboSchoolPeriod.SelectedIndex = 0;
            else
                cboSchoolPeriod.Value = selectedSchoolPeriod.SchoolPeriodID.ToString();

            List<PeriodSection> lstPeriodSection = BusinessLayer.GetPeriodSectionList(string.Format("'{0}' BETWEEN StartDate AND EndDate", DateTime.Now.ToString("yyyyMMdd")));
            if (lstPeriodSection.Count > 0)
            {
                PeriodSection periodSection = lstPeriodSection.FirstOrDefault();
                tacPeriodSection.Value = periodSection.PeriodSectionID.ToString();
                tacPeriodSection.Text = periodSection.PeriodSectionName;
            }

            List<vSchoolClass> lstSchoolClass = BusinessLayer.GetvSchoolClassList(string.Format("SchoolPeriodID = {0} AND TeacherID = {1} AND IsDeleted = 0", cboSchoolPeriod.Value, AppSession.UserLogin.EmployeeID));
            if (lstSchoolClass != null)
            {
                vSchoolClass schoolClass = lstSchoolClass.FirstOrDefault();
                hdnClassID.Value = schoolClass.SchoolClassID.ToString();
                txtClassName.Text = schoolClass.SchoolClassName;
            }
            BindGridView();
        }

        private string GetFilterExpression()
        {
            if (hdnClassID.Value == "")
                return "1 = 0";
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("SchoolClassID = {0}", hdnClassID.Value);
            return filterExpression;
        }

        List<ClassStudentMark> lstStudentMark = null;
        private void BindGridView()
        {
            string filterExpression = GetFilterExpression();
            if (hdnClassID.Value != "")
                lstStudentMark = BusinessLayer.GetClassStudentMarkList(string.Format("SchoolClassID = {0} AND PeriodSectionID = {1}", hdnClassID.Value, tacPeriodSection.Value));
            List<vClassStudent> lstEntity = BusinessLayer.GetvClassStudentList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vClassStudent entity = (vClassStudent)e.Row.DataItem;
                ClassStudentMark studentMark = lstStudentMark.FirstOrDefault(p => p.StudentID == entity.StudentID);
                if (studentMark != null)
                {
                    HtmlGenericControl lblFinalMark = (HtmlGenericControl)e.Row.FindControl("lblFinalMark");
                    lblFinalMark.InnerHtml = studentMark.FinalMark.ToString();
                }
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}