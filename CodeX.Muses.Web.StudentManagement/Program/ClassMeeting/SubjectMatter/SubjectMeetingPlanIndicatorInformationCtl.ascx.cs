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
    public partial class SubjectMeetingPlanIndicatorInformationCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            SubjectMeetingPlanHd entity = BusinessLayer.GetSubjectMeetingPlanHd(Convert.ToInt32(hdnID.Value));
            txtMeetingNo.Text = entity.MeetingNo.ToString();

            BindGridView();
        }

        private void BindGridView()
        {
            string filterExpression = string.Format("SubjectMeetingPlanID = {0}", hdnID.Value);
            grdView.DataSource = BusinessLayer.GetvSubjectMeetingPlanIndicatorList(filterExpression);
            grdView.DataBind();
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}