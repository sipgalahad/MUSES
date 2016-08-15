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
            string filterExpression = string.Format("SubjectCurriculumID = {0} AND GCCurriculumSyllabusType = '{1}' AND IsAllowTask = 1 AND IsDeleted = 0", hdnSubjectCurriculumID.Value, Constant.CurriculumSyllabusType.INDICATOR);
            if (hdnIsPeriodClassTypeSubjectIndicatorExists.Value == "1")
                filterExpression += string.Format(" AND SubjectCurriculumSyllabusID IN (SELECT SubjectIndicatorID FROM PeriodClassTypeSubjectIndicator WHERE PeriodClassTypeSubjectID = {0} AND GCPeriodSection = '{1}')", hdnPeriodClassTypeSubjectID.Value, AppSession.ClassSubject.GCPeriodSection);
            return filterExpression;
        }
        public override void InitializeDataControl(string param)
        {
            vClassMeeting classMeeting = BusinessLayer.GetvClassMeetingList(string.Format("ClassMeetingID = {0}", AppSession.ClassSubject.ClassMeetingID)).FirstOrDefault();
            vClassSubject classSubject = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID)).FirstOrDefault();
            hdnCurriculumSubjectGroupID.Value = classSubject.CurriculumSubjectGroupID.ToString();
            hdnSubjectCurriculumID.Value = classSubject.SubjectCurriculumID.ToString();
            hdnClassMeetingID.Value = AppSession.ClassSubject.ClassMeetingID.ToString();
            hdnSubjectID.Value = classSubject.SubjectID.ToString();
            hdnPeriodClassTypeSubjectID.Value = classSubject.PeriodClassTypeSubjectID.ToString();
            hdnSchoolClassInitial.Value = classSubject.SchoolClassInitial;
            hdnSubjectInitial.Value = classSubject.SubjectInitial;
            hdnSubjectGroupInitial.Value = classSubject.CurriculumSubjectGroupInitial;
            if (!classSubject.IsUseMidSemeterRapor)
                trIsIncludeInMidSemeterRapor.Style.Add("display", "none");

            if (BusinessLayer.GetPeriodClassTypeSubjectIndicatorRowCount(String.Format("PeriodClassTypeSubjectID = {0}", classSubject.PeriodClassTypeSubjectID)) > 0)
                hdnIsPeriodClassTypeSubjectIndicatorExists.Value = "1";
            else
                hdnIsPeriodClassTypeSubjectIndicatorExists.Value = "0";

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
            //txtFinalMarkPercentage.Focus();       
        }

        protected void SetControlProperties()
        {
            List<vCurriculumSubjectMarkType> lstCurriculumMarkType = BusinessLayer.GetvCurriculumSubjectMarkTypeList(string.Format("CurriculumID = {0} AND SubjectID = {1} AND CurriculumSubjectGroupID = {2} AND IsAllowTask = 1 AND IsDeleted = 0", AppSession.ClassSubject.CurriculumID, hdnSubjectID.Value, hdnCurriculumSubjectGroupID.Value));
            Methods.SetComboBoxField<vCurriculumSubjectMarkType>(cboLessonType, lstCurriculumMarkType, "CurriculumMarkTypeName", "CurriculumMarkTypeID");
            cboLessonType.SelectedIndex = 0;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtClassTaskCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtTopic, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboLessonType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(tacTaskType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtFinalMarkPercentage, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(txtTaskDate, new ControlEntrySetting(true, true, true, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtStartDate, new ControlEntrySetting(true, true, false, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtStartTime, new ControlEntrySetting(true, true, false, Constant.DefaultValueEntry.TIME_NOW));
            SetControlEntrySetting(txtEndDate, new ControlEntrySetting(true, true, false, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtEndTime, new ControlEntrySetting(true, true, false, Constant.DefaultValueEntry.TIME_NOW));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(chkIsIncludeInMidSemesterRapor, new ControlEntrySetting(true, true, false));
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
            chkIsIncludeInMidSemesterRapor.Checked = entity.IsIncludeInMidSemesterRapor;
        }

        private void ControlToEntity(ClassSubjectTask entity)
        {
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
            entity.IsIncludeInMidSemesterRapor = chkIsIncludeInMidSemesterRapor.Checked;
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
                entity.ClassTaskCode = string.Format("{0}-{1}-{2}-{3}", hdnSchoolClassInitial.Value, hdnSubjectInitial.Value, hdnSubjectGroupInitial.Value, hdnCurriculumMarkTypeDtInitial.Value);
                string maxCode = BusinessLayer.GetClassSubjectTaskMaxCode(ctx, string.Format("ClassTaskCode LIKE '{0}%'", entity.ClassTaskCode));
                int ctr = 1;
                if (maxCode != "")
                    ctr = Convert.ToInt32(maxCode.Substring(maxCode.Length - 3)) + 1;
                entity.ClassTaskCode = string.Format("{0}{1}", entity.ClassTaskCode, ctr.ToString().PadLeft(3, '0'));
                entity.PeriodSectionID = AppSession.ClassSubject.PeriodSectionID;
                entity.ClassSubjectID = AppSession.ClassSubject.ClassSubjectID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                entity.ClassSubjectTaskID = BusinessLayer.GetClassSubjectTaskMaxID(ctx);

                if (hdnSubjectIndicatorSave.Value != "")
                {
                    string[] lstSaveValue = hdnSubjectIndicatorSave.Value.Split('|');
                    foreach (string saveValue in lstSaveValue)
                    {
                        string[] temp = saveValue.Split(',');
                        int classSubjectTaskIndicatorID = Convert.ToInt32(temp[0]);
                        string subjectIndicatorID = temp[1];
                        string subjectIndicatorName = temp[2];

                        ClassSubjectTaskIndicator entityIndicator = new ClassSubjectTaskIndicator();
                        entityIndicator.ClassSubjectTaskID = entity.ClassSubjectTaskID;
                        if (subjectIndicatorID == "")
                        {
                            entityIndicator.SubjectIndicatorID = null;
                            entityIndicator.SubjectIndicatorName = subjectIndicatorName;
                        }
                        else
                        {
                            entityIndicator.SubjectIndicatorID = Convert.ToInt32(subjectIndicatorID);
                            entityIndicator.SubjectIndicatorName = null;
                        }
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
                    string[] lstSaveValue = hdnSubjectIndicatorSave.Value.Split('|');
                    foreach (string saveValue in lstSaveValue)
                    {
                        string[] temp = saveValue.Split(',');
                        int classSubjectTaskIndicatorID = Convert.ToInt32(temp[0]);
                        string subjectIndicatorID = temp[1];
                        string subjectIndicatorName = temp[2];

                        ClassSubjectTaskIndicator entityIndicator = lstEntityIndicator.FirstOrDefault(p => p.ClassSubjectTaskIndicatorID == Convert.ToInt32(classSubjectTaskIndicatorID));
                        if (entityIndicator == null)
                        {
                            entityIndicator = new ClassSubjectTaskIndicator();
                            entityIndicator.ClassSubjectTaskID = entity.ClassSubjectTaskID;
                            if (subjectIndicatorID == "")
                            {
                                entityIndicator.SubjectIndicatorID = null;
                                entityIndicator.SubjectIndicatorName = subjectIndicatorName;
                            }
                            else
                            {
                                entityIndicator.SubjectIndicatorID = Convert.ToInt32(subjectIndicatorID);
                                entityIndicator.SubjectIndicatorName = null;
                            }
                            entityIndicatorDao.Insert(entityIndicator);
                        }
                        else
                        {
                            if (subjectIndicatorID == "")
                            {
                                entityIndicator.SubjectIndicatorID = null;
                                entityIndicator.SubjectIndicatorName = subjectIndicatorName;
                            }
                            else
                            {
                                entityIndicator.SubjectIndicatorID = Convert.ToInt32(subjectIndicatorID);
                                entityIndicator.SubjectIndicatorName = null;
                            }
                            entityIndicatorDao.Update(entityIndicator);
                            lstEntityIndicator.Remove(entityIndicator);
                        }
                    }
                }

                foreach (ClassSubjectTaskIndicator entityIndicator in lstEntityIndicator)
                {
                    entityIndicatorDao.Delete(entityIndicator.ClassSubjectTaskIndicatorID);
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