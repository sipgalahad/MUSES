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
    public partial class SubjectMeetingPlanDtInformationCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            SubjectMeetingPlanHd entity = BusinessLayer.GetSubjectMeetingPlanHd(Convert.ToInt32(hdnID.Value));
            txtMeetingNo.Text = entity.MeetingNo.ToString();

            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SUBJECT_MEETING_PLAN_DT_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboGCSubjectMeetingPlanDtType, lstSc, "StandardCodeName", "StandardCodeID");
            cboGCSubjectMeetingPlanDtType.SelectedIndex = 0;

            BindGridView();
        }

        private void BindGridView()
        {
            string filterExpression = string.Format("SubjectMeetingPlanHdID = {0} AND GCSubjectMeetingPlanDtType = '{1}'", hdnID.Value, cboGCSubjectMeetingPlanDtType.Value);
            grdView.DataSource = BusinessLayer.GetSubjectMeetingPlanDtList(filterExpression);
            grdView.DataBind();
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}