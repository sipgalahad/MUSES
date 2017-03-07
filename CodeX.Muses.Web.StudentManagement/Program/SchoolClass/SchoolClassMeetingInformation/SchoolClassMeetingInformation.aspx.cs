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
    public partial class SchoolClassMeetingInformation : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            string id = Request.QueryString["id"];
            if (id == "cs")
                return Constant.MenuCode.StudentManagement.SC_CLASS_MEETING;
            return Constant.MenuCode.StudentManagement.MTSC_CLASS_MEETING;
        }
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 5;
        protected int CurrPage = 1;
        protected override void InitializeDataControl()
        {
            List<vClassSubject> lstClassSubject = BusinessLayer.GetvClassSubjectList(string.Format("SchoolClassID = {0} AND SubjectGCClassStudyType = '{1}' AND IsDeleted = 0", AppSession.SchoolClass.SchoolClassID, Constant.ClassStudyType.REGULAR));
            Methods.SetComboBoxField<vClassSubject>(cboSubject, lstClassSubject, "SubjectName", "ClassSubjectID");

            BindGridView(CurrPage, true, ref PageCount, ref RowCount);   
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            if (cboSubject.Value != null)
            {
                string filterExpression = string.Format("PeriodSectionID = {0} AND ClassSubjectID = {1}", AppSession.SchoolClass.PeriodSectionID, cboSubject.Value);

                if (isCountPageCount)
                {
                    rowCount = BusinessLayer.GetvClassMeetingRowCount(filterExpression);
                    pageCount = Helper.GetPageCount(rowCount, RowCountPerPage);
                }

                List<vClassMeeting> lstEntity = BusinessLayer.GetvClassMeetingList(filterExpression, RowCountPerPage, pageIndex, "MeetingDate DESC");
                rptMeetingView.DataSource = lstEntity;
                rptMeetingView.DataBind();
            }
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

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        protected void cbpMeetingDetail_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            ClassMeeting entityClassMeeting = BusinessLayer.GetClassMeeting(Convert.ToInt32(hdnClassMeetingID.Value));
            txtRemarks.Text = entityClassMeeting.Remarks;
            txtNextMeetingRemarks.Text = entityClassMeeting.NextMeetingRemarks;
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }
    }
}