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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ClassAttendanceInformation : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            string id = Request.QueryString["id"];
            if (id == "tcs")
                return Constant.MenuCode.StudentManagement.TCS_ATTENDANCE_HISTORY;
            return Constant.MenuCode.StudentManagement.WS_ATTENDANCE_HISTORY;
        }

        List<ClassMeeting> lstClassMeeting = null;
        protected override void InitializeDataControl()
        {
            BindGridView();
        }

        private void BindGridView()
        {
            lstClassMeeting = BusinessLayer.GetClassMeetingList(string.Format("ClassSubjectID = {0} AND PeriodSectionID = {1} AND IsDeleted = 0", AppSession.ClassSubject.ClassSubjectID, AppSession.ClassSubject.PeriodSectionID));
            rptHeader.DataSource = lstClassMeeting;
            rptHeader.DataBind();
            thAttendance.ColSpan = lstClassMeeting.Count;

            lstClassMeetingAttendance = BusinessLayer.GetvClassMeetingAttendanceList(string.Format("ClassSubjectID = {0} AND PeriodSectionID = {1}", AppSession.ClassSubject.ClassSubjectID, AppSession.ClassSubject.PeriodSectionID));

            ClassSubject classSubject = BusinessLayer.GetClassSubject(AppSession.ClassSubject.ClassSubjectID);
            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", classSubject.SchoolClassID));
            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
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

        public override Control OnGetExportControl()
        {
            lstClassMeeting = BusinessLayer.GetClassMeetingList(string.Format("ClassSubjectID = {0} AND IsDeleted = 0", AppSession.ClassSubject.ClassSubjectID));
            rptHeaderPrint.DataSource = lstClassMeeting;
            rptHeaderPrint.DataBind();
            thAttendancePrint.ColSpan = lstClassMeeting.Count;

            lstClassMeetingAttendance = BusinessLayer.GetvClassMeetingAttendanceList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID));

            ClassSubject classSubject = BusinessLayer.GetClassSubject(AppSession.ClassSubject.ClassSubjectID);
            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", classSubject.SchoolClassID));
            rptStudentPrint.DataSource = lstStudent;
            rptStudentPrint.DataBind();
            HtmlGenericControl div = new HtmlGenericControl("DIV");
            div.Controls.Add(pnlPrint);
            return div;
        }
    }
}