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
    public partial class SchoolClassAttendanceSummaryInformation : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            string id = Request.QueryString["id"];
            if (id == "cs")
                return Constant.MenuCode.StudentManagement.SC_STUDENT_ATTENDANCE;
            return Constant.MenuCode.StudentManagement.MTSC_STUDENT_ATTENDANCE;
        }

        List<StandardCode> lstAttendanceStatus = null;
        protected override void InitializeDataControl()
        {
            List<vClassSubject> lstClassSubject = BusinessLayer.GetvClassSubjectList(string.Format("SchoolClassID = {0} AND SubjectGCClassStudyType = '{1}' AND IsDeleted = 0", AppSession.SchoolClass.SchoolClassID, Constant.ClassStudyType.REGULAR));
            lstClassSubject.Insert(0, new vClassSubject { ClassSubjectID = 0, SubjectName = "-- Harian --" });
            Methods.SetComboBoxField<vClassSubject>(cboSubject, lstClassSubject, "SubjectName", "ClassSubjectID");
            cboSubject.SelectedIndex = 0;

            BindGridView();
        }
        
        private void BindGridView()
        {
            if (cboSubject.Value != null && cboSubject.Value.ToString() != "0")
                lstClassMeetingAttendance = BusinessLayer.GetvClassMeetingAttendanceList(string.Format("ClassSubjectID = {0}", cboSubject.Value));
            else
                lstClassStudentDailyAttendance = BusinessLayer.GetvClassStudentDailyAttendanceList(string.Format("SchoolClassID = {0} AND PeriodSectionID = {1}", AppSession.SchoolClass.SchoolClassID, AppSession.SchoolClass.PeriodSectionID));

            lstAttendanceStatus = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.STUDENT_ATTENDANCE));
            thHeaderAttendance.ColSpan = lstAttendanceStatus.Count;

            rptHeader.DataSource = lstAttendanceStatus;
            rptHeader.DataBind();

            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", AppSession.SchoolClass.SchoolClassID));
            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        List<vClassMeetingAttendance> lstClassMeetingAttendance = null;
        List<vClassStudentDailyAttendance> lstClassStudentDailyAttendance = null;
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
                if (lstClassMeetingAttendance != null)
                    divStudentAttendance.InnerHtml = lstClassMeetingAttendance.Where(p => p.GCAttendanceStatus == classMeeting.StandardCodeID && p.StudentID == student.StudentID).Count().ToString();
                else
                    divStudentAttendance.InnerHtml = lstClassStudentDailyAttendance.Where(p => p.GCAttendanceStatus == classMeeting.StandardCodeID && p.StudentID == student.StudentID).Count().ToString();
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

            if (Request.Form[hdnSubject.UniqueID] != "" && Request.Form[hdnSubject.UniqueID] != "0")
                lstClassMeetingAttendance = BusinessLayer.GetvClassMeetingAttendanceList(string.Format("ClassSubjectID = {0}", Request.Form[hdnSubject.UniqueID]));
            else
                lstClassStudentDailyAttendance = BusinessLayer.GetvClassStudentDailyAttendanceList(string.Format("SchoolClassID = {0} AND PeriodSectionID = {1}", AppSession.SchoolClass.SchoolClassID, AppSession.SchoolClass.PeriodSectionID));

            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", AppSession.SchoolClass.SchoolClassID));
            rptStudentPrint.DataSource = lstStudent;
            rptStudentPrint.DataBind();
            HtmlGenericControl div = new HtmlGenericControl("DIV");
            div.Controls.Add(pnlPrint);
            return div;
        }
    }
}