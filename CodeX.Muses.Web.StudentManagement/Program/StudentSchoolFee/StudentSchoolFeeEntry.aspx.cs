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
using CodeX.Data.Core.Dal;
using DevExpress.Web.ASPxEditors;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class StudentSchoolFeeEntry : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.STUDENT_SCHOOL_FEE;
        }

        protected string OnGetPeriodSectionFilterExpression()
        {
            return string.Format("GCPeriodSectionStatus != '{0}'", Constant.SchoolPeriodStatus.VOID);
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<SchoolPeriod> lstSchoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SiteID = '{0}' AND GCSchoolPeriodStatus != '{1}'", AppSession.UserLogin.SiteID, Constant.SchoolPeriodStatus.VOID));
            Methods.SetComboBoxField<SchoolPeriod>(cboSchoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
            Methods.SetComboBoxField<SchoolPeriod>(cboNextSchoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
            SchoolPeriod selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);
            
            if (selectedSchoolPeriod == null)
                cboSchoolPeriod.SelectedIndex = 0;
            else
                cboSchoolPeriod.Value = selectedSchoolPeriod.SchoolPeriodID.ToString();
            cboNextSchoolPeriod.SelectedIndex = 0;

            List<PeriodSection> lstPeriodSection = BusinessLayer.GetPeriodSectionList(string.Format("'{0}' BETWEEN StartDate AND EndDate", DateTime.Now.ToString("yyyyMMdd")));
            if (lstPeriodSection.Count > 0)
            {
                PeriodSection periodSection = lstPeriodSection.FirstOrDefault();
                tacPeriodSection.Value = periodSection.PeriodSectionID.ToString();
                tacPeriodSection.Text = periodSection.PeriodSectionName;
            }
            BindGridView();
        }

        private string GetFilterExpression()
        {
            if (tacSchoolClass.Value == "")
                return "1 = 0";
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("SchoolClassID = {0}", tacSchoolClass.Value);
            return filterExpression;
        }

        List<Scholarship> lstScholarship = null;
        List<ClassStudentMark> lstStudentMark = null;
        private void BindGridView()
        {
            string filterExpression = GetFilterExpression();
            if (tacSchoolClass.Value != "")
                lstStudentMark = BusinessLayer.GetClassStudentMarkList(string.Format("SchoolClassID = {0} AND PeriodSectionID = {1}", tacSchoolClass.Value, tacPeriodSection.Value));
            List<vClassStudent> lstEntity = BusinessLayer.GetvClassStudentList(String.Format("{0} AND GCClassStudentStatus != '{1}' AND StudentSchoolClassID IS NOT NULL", filterExpression, Constant.ClassStudentStatus.OPEN));
            lstScholarship = BusinessLayer.GetScholarshipList(String.Format("SiteID = '{0}' AND GCScholarshipType != '{1}' AND IsDeleted = 0",AppSession.UserLogin.SiteID, Constant.ScholarshipType.ADMISSION));
            Scholarship sch = new Scholarship();
            sch.ScholarshipID = 0;
            sch.ScholarshipName = "";
            lstScholarship.Insert(0, sch);

            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vClassStudent entity = (vClassStudent)e.Row.DataItem;
                ASPxComboBox cboScholarship = e.Row.FindControl("cboScholarship") as ASPxComboBox;
                cboScholarship.ClientInstanceName = string.Format("cboScholarship{0}", e.Row.DataItemIndex);
                Methods.SetComboBoxField(cboScholarship, lstScholarship, "ScholarshipName", "ScholarshipID");

                //ClassStudentMark studentMark = lstStudentMark.FirstOrDefault(p => p.StudentID == entity.StudentID);
                //if (studentMark != null)
                //{
                //    HtmlGenericControl lblFinalMark = (HtmlGenericControl)e.Row.FindControl("lblFinalMark");
                //    lblFinalMark.InnerHtml = studentMark.FinalMark.ToString();    
                //}
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "promote")
            {
                if (OnProcessSchoolFeeEntity(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool OnProcessSchoolFeeEntity(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            
            try
            {
                BusinessLayer.ProcessReRegistrationStudent(hdnSelectedValue.Value, Convert.ToInt32(cboNextSchoolPeriod.Value), AppSession.UserLogin.UserID, ctx);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                ctx.RollBackTransaction();
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
    }
}