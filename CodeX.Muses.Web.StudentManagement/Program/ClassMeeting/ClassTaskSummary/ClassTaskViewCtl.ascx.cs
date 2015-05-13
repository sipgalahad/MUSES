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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ClassTaskViewCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            String ID = param;
            hdnID.Value = ID;
            SetControlProperties();
            vClassSubjectTask entity = BusinessLayer.GetvClassSubjectTaskList(string.Format("ClassSubjectTaskID = {0}", ID)).FirstOrDefault();
            EntityToControl(entity);
            vClassMeeting classMeeting = BusinessLayer.GetvClassMeetingList(string.Format("ClassMeetingID = {0}", AppSession.ClassSubject.ClassMeetingID)).FirstOrDefault();
            vClassSubject classSubject = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID)).FirstOrDefault();
            hdnSubjectCurriculumID.Value = classSubject.SubjectCurriculumID.ToString();
            if (classMeeting != null)
                hdnSubjectMeetingPlanHdID.Value = classMeeting.SubjectMeetingPlanHdID.ToString();
            else
                hdnSubjectMeetingPlanHdID.Value = "0";
            hdnClassMeetingID.Value = AppSession.ClassSubject.ClassMeetingID.ToString();
            //txtFinalMarkPercentage.Focus();       
        }

        protected void SetControlProperties()
        {
            List<CurriculumMarkType> lstCurriculumMarkType = BusinessLayer.GetCurriculumMarkTypeList(string.Format("CurriculumID = {0} AND IsAllowTask = 1 AND IsDeleted = 0", AppSession.ClassSubject.CurriculumID));
            Methods.SetComboBoxField<CurriculumMarkType>(cboLessonType, lstCurriculumMarkType, "CurriculumMarkTypeName", "CurriculumMarkTypeID");
            cboLessonType.SelectedIndex = 0;
        }

        private void EntityToControl(vClassSubjectTask entity)
        {
            hdnID.Value = entity.ClassSubjectTaskID.ToString();
            txtClassTaskCode.Text = entity.ClassTaskCode;
            txtTopic.Text = entity.Topic;
            cboLessonType.Value = entity.CurriculumMarkTypeID.ToString();
            txtTaskType.Text = entity.CurriculumMarkTypeDtName;
            txtFinalMarkPercentage.Text = entity.FinalMarkPercentage.ToString();
            txtTaskDate.Text = entity.TaskDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtStartDate.Text = entity.StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtEndDate.Text = entity.EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtStartTime.Text = entity.StartTime;
            txtEndTime.Text = entity.EndTime;
            txtRemarks.Text = entity.Remarks;

            List<vClassSubjectTaskIndicator> lstIndicator = BusinessLayer.GetvClassSubjectTaskIndicatorList(string.Format("ClassSubjectTaskID = {0}", entity.ClassSubjectTaskID));
            rptIndicator.DataSource = lstIndicator;
            rptIndicator.DataBind();
        }
    }
}