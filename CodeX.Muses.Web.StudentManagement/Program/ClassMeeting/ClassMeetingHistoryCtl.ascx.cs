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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ClassMeetingHistoryCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 5;
        protected int CurrPage = 1;
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');
            hdnPeriodSection.Value = temp[0];
            hdnClassSubjectID.Value = temp[1];
            hdnClassScheduleID.Value = temp[2];

            BindGridView(CurrPage, true, ref PageCount, ref RowCount);            
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = string.Format("PeriodSectionID = {0} AND ClassSubjectID = {1}", hdnPeriodSection.Value, hdnClassSubjectID.Value);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvClassMeetingRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, RowCountPerPage);
            }

            List<vClassMeeting> lstEntity = BusinessLayer.GetvClassMeetingList(filterExpression, RowCountPerPage, pageIndex, "MeetingDate DESC");
            rptMeetingView.DataSource = lstEntity;
            rptMeetingView.DataBind();
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
    }
}