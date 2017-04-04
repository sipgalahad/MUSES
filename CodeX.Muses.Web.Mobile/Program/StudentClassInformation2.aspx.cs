using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common;
using CodeX.Data.Model;
using System.Web.Security;
using System.Text;
using CodeX.Common;
using CodeX.Web.Common.UI;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.Mobile.Program
{
    public partial class StudentClassInformation2 : BasePageContent
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Mobile.STUDENT_CLASS_INFO;
        }

        List<vClassMeetingAttendance> lstAttendance = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hdnStudentID.Value = AppSession.StudentLogin.UserID.ToString();
                Student student = BusinessLayer.GetStudent(Convert.ToInt32(hdnStudentID.Value));
                hdnSiteID.Value = student.SiteID;
                txtSchoolDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                BindGridView();
            }
        }

        private void BindGridView()
        {
            DateTime schoolDate = Helper.GetDatePickerValue(txtSchoolDate.Text);
            vClassStudentDailyAttendance entityAttendance = BusinessLayer.GetvClassStudentDailyAttendanceList(string.Format("StudentID = {0} AND SchoolDate = '{1}'", hdnStudentID.Value, schoolDate.ToString("yyyyMMdd"))).FirstOrDefault();
            if (entityAttendance != null)
                spnAttendanceStatus.InnerHtml = entityAttendance.AttendanceStatus;

            SchoolPeriod entitySchoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SiteID = '{0}' AND GCSchoolPeriodStatus != '{1}' AND '{2}' BETWEEN StartDate AND EndDate", hdnSiteID.Value, Constant.SchoolPeriodStatus.VOID, schoolDate.ToString("yyyyMMdd"))).FirstOrDefault();
            if (entitySchoolPeriod != null)
            {
                vClassStudent classStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolPeriodID = {0} AND StudentID = {1}", entitySchoolPeriod.SchoolPeriodID, hdnStudentID.Value)).FirstOrDefault();
                if (classStudent != null)
                {
                    List<vClassMeeting> lstClassMeeting = BusinessLayer.GetvClassMeetingList(string.Format("SchoolClassID = {0} AND MeetingDate = '{1}' AND IsDeleted = 0", classStudent.SchoolClassID, schoolDate.ToString("yyyyMMdd"))); ;

                    if (lstClassMeeting.Count > 0)
                    {
                        string lstClassMeetingID = string.Join(",", lstClassMeeting.Select(p => p.ClassMeetingID).ToList());
                        lstAttendance = BusinessLayer.GetvClassMeetingAttendanceList(string.Format("ClassMeetingID IN ({0}) AND StudentID = {1}", lstClassMeetingID, hdnStudentID.Value));
                    }
                    else
                        lstAttendance = new List<vClassMeetingAttendance>();

                    rptClassMeeting.DataSource = lstClassMeeting;
                    rptClassMeeting.DataBind();
                }
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected void rptClassMeeting_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                vClassMeeting classMeeting = (vClassMeeting)e.Item.DataItem;
                vClassMeetingAttendance attendance = lstAttendance.FirstOrDefault(p => p.ClassMeetingID == classMeeting.ClassMeetingID);
                if (attendance != null)
                {
                    HtmlGenericControl divAttendanceStatus = (HtmlGenericControl)e.Item.FindControl("divAttendanceStatus");
                    divAttendanceStatus.InnerHtml = attendance.AttendanceStatus;
                }
            }
        }
    }
}