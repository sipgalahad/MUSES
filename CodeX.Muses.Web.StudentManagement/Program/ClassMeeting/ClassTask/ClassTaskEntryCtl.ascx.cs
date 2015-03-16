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
        public override void InitializeDataControl(string param)
        {
            if (param != "")
            {
                IsAdd = false;
                String ID = param;
                hdnID.Value = ID;
                SetControlProperties();
                ClassSubjectTask entity = BusinessLayer.GetClassSubjectTask(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            //txtFinalMarkPercentage.Focus();       
        }

        protected void SetControlProperties()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("((ParentID = '{0}' AND TagProperty = '0') OR ParentID = '{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.TASK_TYPE, Constant.StandardCode.LESSON_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboTaskType, lstSc.Where(p => p.ParentID == Constant.StandardCode.TASK_TYPE).ToList(), "StandardCodeName", "StandardCodeID");
            cboTaskType.SelectedIndex = 0;

            List<StandardCode> lstLessonType = new List<StandardCode>();
            vClassSubject entityClassSubject = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID)).FirstOrDefault();
            if (entityClassSubject.GCLessonType == Constant.LessonType.THEORY_PRACTICE)
                Methods.SetComboBoxField<StandardCode>(cboLessonType, lstSc.Where(p => p.StandardCodeID == Constant.LessonType.THEORY || p.StandardCodeID == Constant.LessonType.PRACTICE).ToList(), "StandardCodeName", "StandardCodeID");
            else
            {
                trLessonType.Style.Add("display", "none");
                Methods.SetComboBoxField<StandardCode>(cboLessonType, lstSc.Where(p => p.StandardCodeID == entityClassSubject.GCLessonType).ToList(), "StandardCodeName", "StandardCodeID");
            }
            cboLessonType.SelectedIndex = 0;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtClassTaskCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtTopic, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboLessonType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboTaskType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtFinalMarkPercentage, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(txtTaskDate, new ControlEntrySetting(true, true, true, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtStartDate, new ControlEntrySetting(true, true, false, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtStartTime, new ControlEntrySetting(true, true, false, Constant.DefaultValueEntry.TIME_NOW));
            SetControlEntrySetting(txtEndDate, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtEndTime, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(ClassSubjectTask entity)
        {
            hdnID.Value = entity.ClassSubjectTaskID.ToString();
            txtClassTaskCode.Text = entity.ClassTaskCode;
            txtTopic.Text = entity.Topic;
            cboLessonType.Value = entity.GCLessonType;
            cboTaskType.Value = entity.GCTaskType;
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
            entity.GCLessonType = cboLessonType.Value.ToString();
            entity.GCTaskType = cboTaskType.Value.ToString();
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
            bool result = false;
            try
            {
                ClassSubjectTask entity = new ClassSubjectTask();
                ControlToEntity(entity);
                entity.PeriodSectionID = AppSession.ClassSubject.PeriodSectionID;
                entity.ClassSubjectID = AppSession.ClassSubject.ClassSubjectID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
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
            try
            {
                ClassSubjectTask entity = BusinessLayer.GetClassSubjectTask(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateClassSubjectTask(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }
    }
}