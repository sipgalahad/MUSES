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
    public partial class ClassAttendanceSummaryInformation : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            string id = Request.QueryString["id"];
            if (id == "tcs")
                return Constant.MenuCode.StudentManagement.TCS_CLASS_ATTENDANCE_SUMMARY;
            return Constant.MenuCode.StudentManagement.WS_CLASS_ATTENDANCE_SUMMARY;
        }

        List<StandardCode> lstAttendanceStatus = null;
        protected override void InitializeDataControl()
        {
            BindGridView();
        }
        
        private void BindGridView()
        {
            lstAttendanceStatus = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.STUDENT_ATTENDANCE));
            thHeaderAttendance.ColSpan = lstAttendanceStatus.Count;

            rptHeader.DataSource = lstAttendanceStatus;
            rptHeader.DataBind();

            lstClassMeetingAttendance = BusinessLayer.GetvClassMeetingAttendanceList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID));

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
                rptStudentAttendance.DataSource = lstAttendanceStatus;
                rptStudentAttendance.DataBind();
            }
        }

        protected void rptStudentAttendance_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                StandardCode classMeeting = (StandardCode)e.Item.DataItem;
                vClassStudent student = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vClassStudent;
                HtmlGenericControl divStudentAttendance = (HtmlGenericControl)e.Item.FindControl("divStudentAttendance");
                divStudentAttendance.InnerHtml = lstClassMeetingAttendance.Where(p => p.GCAttendanceStatus == classMeeting.StandardCodeID && p.StudentID == student.StudentID).Count().ToString();
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        public override Control OnGetExportControl()
        {
            lstAttendanceStatus = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.STUDENT_ATTENDANCE));
            thHeaderAttendancePrint.ColSpan = lstAttendanceStatus.Count;

            rptHeaderPrint.DataSource = lstAttendanceStatus;
            rptHeaderPrint.DataBind();

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