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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class StudentDailyAttendanceEntry : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.STUDENT_DAILY_ATTENDANCE;
        }

        protected string OnGetPeriodSectionFilterExpression()
        {
            return string.Format("GCPeriodSectionStatus != '{0}'", Constant.SchoolPeriodStatus.VOID);
        }

        protected string OnGetClassStudyTypeRegular()
        {
            return Constant.ClassStudyType.REGULAR;
        }

        List<StandardCode> lstAttendanceStatus = null;
        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<SchoolPeriod> lstSchoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SiteID = '{0}' AND GCSchoolPeriodStatus != '{1}'", AppSession.UserLogin.SiteID, Constant.SchoolPeriodStatus.VOID));
            Methods.SetComboBoxField<SchoolPeriod>(cboSchoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
            SchoolPeriod selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);
            if (selectedSchoolPeriod == null)
                cboSchoolPeriod.SelectedIndex = 0;
            else
                cboSchoolPeriod.Value = selectedSchoolPeriod.SchoolPeriodID.ToString();

            txtSchoolDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);

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

        private void BindGridView()
        {
            string filterExpression = GetFilterExpression();
            if (tacSchoolClass.Value != "")
                lstClassAttendance = BusinessLayer.GetClassStudentDailyAttendanceList(string.Format("SchoolClassID = {0} AND PeriodSectionID = {1} AND SchoolDate = '{2}'", tacSchoolClass.Value, tacPeriodSection.Value, Helper.GetDatePickerValue(txtSchoolDate).ToString("yyyyMMdd")));
            lstAttendanceStatus = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.STUDENT_ATTENDANCE));
            rptHeader.DataSource = lstAttendanceStatus;
            rptHeader.DataBind();

            List<vClassStudent> lstEntity = BusinessLayer.GetvClassStudentList(filterExpression);
            rptStudent.DataSource = lstEntity;
            rptStudent.DataBind();
        }

        List<ClassStudentDailyAttendance> lstClassAttendance = null;
        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassStudent entity = (vClassStudent)e.Item.DataItem;
                HtmlInputHidden hdnAttendance = (HtmlInputHidden)e.Item.FindControl("hdnAttendance");

                if (lstClassAttendance != null)
                {
                    ClassStudentDailyAttendance attendance = lstClassAttendance.FirstOrDefault(p => p.StudentID == entity.StudentID);
                    if (attendance != null)
                        hdnAttendance.Value = attendance.GCAttendanceStatus;
                }

                Repeater rptStudentAttendance = (Repeater)e.Item.FindControl("rptStudentAttendance");
                rptStudentAttendance.DataSource = lstAttendanceStatus;
                rptStudentAttendance.DataBind();
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ClassStudentDailyAttendanceDao entityDtDao = new ClassStudentDailyAttendanceDao(ctx);
            try
            {
                string[] lstSaveValue = hdnListSaveValue.Value.Split('|');

                DateTime schoolDate = Helper.GetDatePickerValue(txtSchoolDate);
                List<ClassStudentDailyAttendance> lstClassMeetingAttendance = BusinessLayer.GetClassStudentDailyAttendanceList(string.Format("SchoolClassID = {0} AND PeriodSectionID = {1} AND SchoolDate = '{2}'", tacSchoolClass.Value, tacPeriodSection.Value, schoolDate.ToString("yyyyMMdd")), ctx);
                foreach (String saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(',');
                    int studentID = Convert.ToInt32(temp[0]);
                    string GCAttendanceStatus = temp[1];
                    if (GCAttendanceStatus != "")
                    {
                        ClassStudentDailyAttendance entityDt = lstClassMeetingAttendance.FirstOrDefault(p => p.StudentID == studentID);
                        if (entityDt == null)
                        {
                            entityDt = new ClassStudentDailyAttendance();
                            entityDt.SchoolClassID = Convert.ToInt32(tacSchoolClass.Value);
                            entityDt.PeriodSectionID = Convert.ToInt32(tacPeriodSection.Value);
                            entityDt.SchoolDate = schoolDate;
                            entityDt.StudentID = studentID;
                            entityDt.GCAttendanceStatus = GCAttendanceStatus;
                            entityDtDao.Insert(entityDt);
                        }
                        else
                        {
                            entityDt.GCAttendanceStatus = GCAttendanceStatus;
                            entityDtDao.Update(entityDt);
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
    }
}