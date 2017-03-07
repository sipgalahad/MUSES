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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class TeacherSchoolClassList : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.TEACHER_SCHOOL_CLASS;
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

            if (cboSchoolPeriod.Value != "")
            {
                List<PeriodSection> lstPeriodSection = BusinessLayer.GetPeriodSectionList(string.Format("SchoolPeriodID = {0} AND '{1}' BETWEEN StartDate AND EndDate", cboSchoolPeriod.Value, DateTime.Now.ToString("yyyyMMdd")));
                if (lstPeriodSection.Count > 0)
                {
                    PeriodSection periodSection = lstPeriodSection.FirstOrDefault();
                    tacPeriodSection.Value = periodSection.PeriodSectionID.ToString();
                    tacPeriodSection.Text = periodSection.PeriodSectionName;
                }
            }
            BindGridView();
        }

        private string GetFilterExpression()
        {
            if (AppSession.UserLogin.EmployeeID == null || AppSession.UserLogin.EmployeeID == 0)
                return "1 = 0";
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("SchoolPeriodID = {0} AND TeacherID = {1} AND IsDeleted = 0", cboSchoolPeriod.Value, AppSession.UserLogin.EmployeeID);
            return filterExpression;
        }

        private void BindGridView()
        {
            string filterExpression = GetFilterExpression();
            List<vSchoolClass> lstEntity = BusinessLayer.GetvSchoolClassList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}