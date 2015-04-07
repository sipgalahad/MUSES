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
    public partial class SubjectMeetingPlanDtCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            SubjectMeetingPlanHd entity = BusinessLayer.GetSubjectMeetingPlanHd(Convert.ToInt32(hdnID.Value));
            txtMeetingNo.Text = entity.MeetingNo.ToString();

            string filterExpression = String.Format("ParentID = '{0}' AND StandardCodeID < '{0}^020' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SUBJECT_MEETING_PLAN_DT_TYPE);
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(filterExpression);
            Methods.SetComboBoxField<StandardCode>(cboGCSubjectMeetingPlanDtType, lstSc, "StandardCodeName", "StandardCodeID");
            cboGCSubjectMeetingPlanDtType.SelectedIndex = 0;

            filterExpression = String.Format("ParentID = '{0}' AND StandardCodeID > '{0}^020' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SUBJECT_MEETING_PLAN_DT_TYPE);
            lstSc = BusinessLayer.GetStandardCodeList(filterExpression);
            Methods.SetComboBoxField<StandardCode>(cboGCSubjectMeetingPlanDtType2, lstSc, "StandardCodeName", "StandardCodeID");
            cboGCSubjectMeetingPlanDtType2.SelectedIndex = 0;

            BindGridView();
            BindGridView2();
        }

        private void BindGridView()
        {
            string filterExpression = string.Format("SubjectMeetingPlanHdID = {0} AND GCSubjectMeetingPlanDtType = '{1}'", hdnID.Value, cboGCSubjectMeetingPlanDtType.Value);
            grdView.DataSource = BusinessLayer.GetSubjectMeetingPlanDtList(filterExpression);
            grdView.DataBind();
        }

        private void BindGridView2()
        {
            string filterExpression = string.Format("SubjectMeetingPlanHdID = {0} AND GCSubjectMeetingPlanDtType = '{1}'", hdnID.Value, cboGCSubjectMeetingPlanDtType2.Value);
            grdView2.DataSource = BusinessLayer.GetSubjectMeetingPlanDtList(filterExpression);
            grdView2.DataBind();
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected void cbpViewPopup2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView2();
        }
    }
}