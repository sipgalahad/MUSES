using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common;
using CodeX.Common;
using CodeX.Data.Core.Dal;
using CodeX.Web.CustomControl;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class StudentSummaryViewDtCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;

        protected int PageCount2 = 1;
        protected int RowCount2 = 1;
        protected int RowCountPerPage2 = 1;
        protected int CurrPage2 = 1;
        public override void InitializeDataControl(string param)
        {
            hdnStudentID.Value = param;
            Student entity = BusinessLayer.GetStudent(Convert.ToInt32(param));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.StudentCode, entity.StudentName);

            RowCountPerPage2 = RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
            BindGridView2(CurrPage2, true, ref PageCount2, ref RowCount2);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = string.Format("StudentID = {0} AND PeriodSectionID = {1} AND ClassSubjectID = {2} AND IsDeleted = 0", hdnStudentID.Value, AppSession.ClassSubject.PeriodSectionID, AppSession.ClassSubject.ClassSubjectID);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetStudentNoteRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<StudentNote> lstEntity = BusinessLayer.GetStudentNoteList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "NoteDate DESC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount, ref rowCount);
                    result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        List<ClassMeeting> lstClassMeeting = null;
        private void BindGridView2(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = string.Format("ClassSubjectID = {0} AND IsDeleted = 0", AppSession.ClassSubject.ClassSubjectID);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetClassMeetingRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            lstClassMeeting = BusinessLayer.GetClassMeetingList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
            rptHeader.DataSource = lstClassMeeting;
            rptHeader.DataBind();

            string lstClassMeetingID = string.Join(",", lstClassMeeting.Select(p => p.ClassMeetingID).ToList());
            if (lstClassMeetingID != "")
            {
                lstClassMeetingAttendance = BusinessLayer.GetvClassMeetingAttendanceList(string.Format("ClassMeetingID IN ({0})", lstClassMeetingID));
                rptStudentAttendance.DataSource = lstClassMeeting;
                rptStudentAttendance.DataBind();
            }
        }

        List<vClassMeetingAttendance> lstClassMeetingAttendance = null;
        protected void rptStudentAttendance_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ClassMeeting classMeeting = (ClassMeeting)e.Item.DataItem;
                vClassMeetingAttendance entity = lstClassMeetingAttendance.FirstOrDefault(p => p.ClassMeetingID == classMeeting.ClassMeetingID);
                if (entity != null)
                {
                    HtmlGenericControl divStudentAttendance = (HtmlGenericControl)e.Item.FindControl("divStudentAttendance");
                    divStudentAttendance.InnerHtml = entity.AttendanceStatus.Substring(0, 1);
                }
            }
        }

        protected void cbpViewPopup2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView2(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView2(1, true, ref pageCount, ref rowCount);
                    result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
    }
}