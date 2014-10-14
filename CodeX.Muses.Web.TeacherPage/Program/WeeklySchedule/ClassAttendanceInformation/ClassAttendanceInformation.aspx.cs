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

namespace CodeX.Muses.Web.TeacherPage.Program
{
    public partial class ClassAttendanceInformation : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.TeacherPage.WS_ATTENDANCE_HISTORY;
        }

        List<ClassMeeting> lstClassMeeting = null;
        protected override void InitializeDataControl()
        {
            lstClassMeeting = BusinessLayer.GetClassMeetingList(string.Format("ClassSubjectID = {0} AND IsDeleted = 0", AppSession.ClassSubject.ClassSubjectID));
            rptHeader.DataSource = lstClassMeeting;
            rptHeader.DataBind();

            lstClassMeetingAttendance = BusinessLayer.GetvClassMeetingAttendanceList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID));

            ClassSubject classSubject = BusinessLayer.GetClassSubject(AppSession.ClassSubject.ClassSubjectID);
            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", classSubject.SchoolClassID));
            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();
        }

        List<vClassMeetingAttendance> lstClassMeetingAttendance = null;
        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptStudentAttendance = (Repeater)e.Item.FindControl("rptStudentAttendance");
                rptStudentAttendance.DataSource = lstClassMeeting;
                rptStudentAttendance.DataBind();
            }
        }

        protected void rptStudentAttendance_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ClassMeeting classMeeting = (ClassMeeting)e.Item.DataItem;
                vClassStudent student = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vClassStudent;

                vClassMeetingAttendance entity = lstClassMeetingAttendance.FirstOrDefault(p => p.ClassMeetingID == classMeeting.ClassMeetingID && p.StudentID == student.StudentID);
                if (entity != null)
                {
                    HtmlGenericControl divStudentAttendance = (HtmlGenericControl)e.Item.FindControl("divStudentAttendance");
                    divStudentAttendance.InnerHtml = entity.AttendanceStatus.Substring(0, 1);
                }
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }
    }
}