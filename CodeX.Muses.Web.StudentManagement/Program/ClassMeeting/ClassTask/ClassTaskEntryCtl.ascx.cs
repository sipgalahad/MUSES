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
    public partial class ClassTaskEntryCtl : BaseEntryPopupCtl
    {
        protected string OnGetSubjectIndicatorFilterExpression()
        {
            return string.Format("SubjectMatterID = {0}", hdnSubjectCurriculumID.Value);
        }
        public override void InitializeDataControl(string param)
        {
            if (param != "")
            {
                IsAdd = false;
                String ID = param;
                hdnID.Value = ID;
                SetControlProperties();
                vClassSubjectTask entity = BusinessLayer.GetvClassSubjectTaskList(string.Format("ClassSubjectTaskID = {0}", ID)).FirstOrDefault();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
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
            List<CurriculumMarkType> lstCurriculumMarkType = BusinessLayer.GetCurriculumMarkTypeList(string.Format("CurriculumID = {0} AND IsDeleted = 0", AppSession.ClassSubject.CurriculumID));
            Methods.SetComboBoxField<CurriculumMarkType>(cboLessonType, lstCurriculumMarkType, "CurriculumMarkTypeName", "CurriculumMarkTypeID");
            cboLessonType.SelectedIndex = 0;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtClassTaskCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtTopic, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboLessonType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(tacTaskType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtFinalMarkPercentage, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(txtTaskDate, new ControlEntrySetting(true, true, true, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtStartDate, new ControlEntrySetting(true, true, false, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtStartTime, new ControlEntrySetting(true, true, false, Constant.DefaultValueEntry.TIME_NOW));
            SetControlEntrySetting(txtEndDate, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtEndTime, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(vClassSubjectTask entity)
        {
            hdnID.Value = entity.ClassSubjectTaskID.ToString();
            txtClassTaskCode.Text = entity.ClassTaskCode;
            txtTopic.Text = entity.Topic;
            cboLessonType.Value = entity.CurriculumMarkTypeID.ToString();
            tacTaskType.Value = entity.CurriculumMarkTypeDtID.ToString();
            tacTaskType.Text = entity.CurriculumMarkTypeDtName;
            txtFinalMarkPercentage.Text = entity.FinalMarkPercentage.ToString();
            txtTaskDate.Text = entity.TaskDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtStartDate.Text = entity.StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtEndDate.Text = entity.EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtStartTime.Text = entity.StartTime;
            txtEndTime.Text = entity.EndTime;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(ClassSubjectTask entity)
        {
            entity.ClassTaskCode = txtClassTaskCode.Text;
            entity.Topic = txtTopic.Text;
            entity.CurriculumMarkTypeID = Convert.ToInt32(cboLessonType.Value);
            entity.CurriculumMarkTypeDtID = Convert.ToInt32(hdnTaskTypeID.Value);
            entity.FinalMarkPercentage = Convert.ToInt16(txtFinalMarkPercentage.Text);
            entity.TaskDate = Helper.GetDatePickerValue(txtTaskDate.Text);
            entity.StartDate = Helper.GetDatePickerValue(txtStartDate.Text);
            entity.EndDate = Helper.GetDatePickerValue(txtEndDate.Text);
            entity.StartTime = txtStartTime.Text;
            entity.EndTime = txtEndTime.Text;
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            ClassSubjectTaskDao entityDao = new ClassSubjectTaskDao(ctx);
            ClassSubjectTaskIndicatorDao entityIndicatorDao = new ClassSubjectTaskIndicatorDao(ctx);
            bool result = false;
            try
            {
                ClassSubjectTask entity = new ClassSubjectTask();
                ControlToEntity(entity);
                entity.PeriodSectionID = AppSession.ClassSubject.PeriodSectionID;
                entity.ClassSubjectID = AppSession.ClassSubject.ClassSubjectID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                entity.ClassSubjectTaskID = BusinessLayer.GetClassSubjectTaskMaxID(ctx);

                if (hdnSubjectIndicatorSave.Value != "")
                {
                    string[] lstSubjectIndicatorID = hdnSubjectIndicatorSave.Value.Split(',');
                    foreach (string subjectIndicatorID in lstSubjectIndicatorID)
                    {
                        ClassSubjectTaskIndicator entityIndicator = new ClassSubjectTaskIndicator();
                        entityIndicator.ClassSubjectTaskID = entity.ClassSubjectTaskID;
                        entityIndicator.SubjectIndicatorID = Convert.ToInt32(subjectIndicatorID);
                        entityIndicatorDao.Insert(entityIndicator);
                    }
                }
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            bool result = false;
            IDbContext ctx = DbFactory.Configure(true);
            ClassSubjectTaskDao entityDao = new ClassSubjectTaskDao(ctx);
            ClassSubjectTaskIndicatorDao entityIndicatorDao = new ClassSubjectTaskIndicatorDao(ctx);
            try
            {
                ClassSubjectTask entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

                List<ClassSubjectTaskIndicator> lstEntityIndicator = BusinessLayer.GetClassSubjectTaskIndicatorList(string.Format("ClassSubjectTaskID = {0}", entity.ClassSubjectTaskID), ctx);
                if (hdnSubjectIndicatorSave.Value != "")
                {
                    string[] lstSubjectIndicatorID = hdnSubjectIndicatorSave.Value.Split(',');
                    foreach (string subjectIndicatorID in lstSubjectIndicatorID)
                    {
                        ClassSubjectTaskIndicator entityIndicator = lstEntityIndicator.FirstOrDefault(p => p.SubjectIndicatorID == Convert.ToInt32(subjectIndicatorID));
                        if (entityIndicator == null)
                        {
                            entityIndicator = new ClassSubjectTaskIndicator();
                            entityIndicator.ClassSubjectTaskID = entity.ClassSubjectTaskID;
                            entityIndicator.SubjectIndicatorID = Convert.ToInt32(subjectIndicatorID);
                            entityIndicatorDao.Insert(entityIndicator);
                        }
                        else
                            lstEntityIndicator.Remove(entityIndicator);
                    }
                }

                foreach (ClassSubjectTaskIndicator entityIndicator in lstEntityIndicator)
                {
                    entityIndicatorDao.Delete(entityIndicator.ClassSubjectTaskID, entityIndicator.SubjectIndicatorID);
                }
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
    }
}