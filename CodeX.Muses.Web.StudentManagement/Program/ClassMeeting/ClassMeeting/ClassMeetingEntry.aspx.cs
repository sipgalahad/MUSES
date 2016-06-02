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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ClassMeetingEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.WS_CLASS_MEETING;
        }
        protected string OnGetSubjectCurriculumMeetingPlanFilterExpression()
        {
            return string.Format("SubjectCurriculumID = {0} AND GCCurriculumMeetingPlanType = '{1}' AND IsDeleted = 0", hdnSubjectCurriculumID.Value, Constant.CurriculumMeetingPlanType.MEETING);
        }
        protected string OnGetSubjectIndicatorFilterExpression()
        {
            return string.Format("SubjectCurriculumID = {0} AND GCCurriculumSyllabusType = '{1}' AND IsDeleted = 0", hdnSubjectCurriculumID.Value, Constant.CurriculumSyllabusType.INDICATOR);
        }
        protected string OnGetSubjectIndicatorMeetingPlanFilterExpression()
        {
            return string.Format("SubjectCurriculumID = {0} AND GCCurriculumMeetingPlanType = '{1}' AND IsDeleted = 0", hdnSubjectCurriculumID.Value, Constant.CurriculumMeetingPlanType.INDICATOR);
        }
        protected string OnGetRoomFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }

        protected string OnGetTeacherFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND TeacherID IN (SELECT TeacherID FROM TeacherSubject WHERE SubjectID = {1}) AND IsDeleted = 0", AppSession.UserLogin.SiteID, hdnSubjectID.Value);
        }

        protected override void InitializeDataControl()
        {
            if (AppSession.ClassSubject.ClassMeetingID == 0)
            {
                txtMeetingDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                if (AppSession.ClassSubject.ClassScheduleID > 0)
                {
                    vClassSchedule entity = BusinessLayer.GetvClassScheduleList(string.Format("ClassScheduleID = {0}", AppSession.ClassSubject.ClassScheduleID)).FirstOrDefault();
                    txtStartTime.Text = entity.StartTime;
                    txtEndTime.Text = entity.EndTime;
                    tacRoom.Value = entity.RoomID.ToString();
                    tacRoom.Text = entity.RoomName;
                    tacTeacher.Value = entity.TeacherID.ToString();
                    tacTeacher.Text = entity.TeacherName;
                    tacAssistantTeacher.Value = entity.AssistantTeacherID.ToString();
                    tacAssistantTeacher.Text = entity.AssistantTeacherName;
                }
                else
                {
                    vClassSubject entity = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID)).FirstOrDefault();
                    txtStartTime.Text = "";
                    txtEndTime.Text = "";
                    tacRoom.Value = entity.RoomID.ToString();
                    tacRoom.Text = entity.RoomName;
                    tacTeacher.Value = entity.TeacherID.ToString();
                    tacTeacher.Text = entity.TeacherName;
                    tacAssistantTeacher.Value = entity.AssistantTeacherID.ToString();
                    tacAssistantTeacher.Text = entity.AssistantTeacherName;
                }
                //if (AppSession.UserLogin.EmployeeID != null && AppSession.UserLogin.EmployeeID != 0)
                //{
                //    Employee emp = BusinessLayer.GetEmployee((int)AppSession.UserLogin.EmployeeID);
                //    if (emp.GCEmployeeType == Constant.EmployeeType.TEACHER)
                //    {
                //        tacTeacher.Value = AppSession.UserLogin.EmployeeID.ToString();
                //        tacTeacher.Text = AppSession.UserLogin.UserFullName;
                //    }
                //}
            }
            else
            {
                vClassMeeting entity = BusinessLayer.GetvClassMeetingList(string.Format("ClassMeetingID = {0}", AppSession.ClassSubject.ClassMeetingID)).FirstOrDefault();
                txtMeetingDate.Text = entity.MeetingDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                txtStartTime.Text = entity.StartTime;
                txtEndTime.Text = entity.EndTime;
                tacRoom.Value = entity.RoomID.ToString();
                tacRoom.Text = entity.RoomName;
                tacTeacher.Value = entity.TeacherID.ToString();
                tacTeacher.Text = entity.TeacherName;
                tacAssistantTeacher.Value = entity.AssistantTeacherID.ToString();
                tacAssistantTeacher.Text = entity.AssistantTeacherName;
                txtRemarks.Text = entity.Remarks;
                txtNextMeetingRemarks.Text = entity.NextMeetingRemarks;
                if (entity.SubjectCurriculumMeetingPlanID != 0)
                {
                    tacSubjectCurriculumMeetingPlan.Value = entity.SubjectCurriculumMeetingPlanID.ToString();
                    tacSubjectCurriculumMeetingPlan.Text = entity.SubjectCurriculumMeetingPlanName.ToString();
                }
            }
            hdnClassMeetingID.Value = AppSession.ClassSubject.ClassMeetingID.ToString();

            vClassSubject classSubject = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID)).FirstOrDefault();
            hdnSubjectCurriculumID.Value = classSubject.SubjectCurriculumID.ToString();
            hdnSubjectID.Value = classSubject.SubjectID.ToString();

            Helper.SetControlEntrySetting(txtMeetingDate, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(txtStartTime, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(txtEndTime, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(tacRoom, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(tacTeacher, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtNextMeetingRemarks, new ControlEntrySetting(true, true, false), "mpEntry");
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        private void ControlToEntity(ClassMeeting entity)
        {
            entity.MeetingDate = Helper.GetDatePickerValue(txtMeetingDate);
            entity.StartTime = txtStartTime.Text;
            entity.EndTime = txtEndTime.Text;
            entity.RoomID = Convert.ToInt32(tacRoom.Value);
            entity.TeacherID = Convert.ToInt32(tacTeacher.Value);
            if (tacAssistantTeacher.Value != "" && tacAssistantTeacher.Value != "0")
                entity.AssistantTeacherID = Convert.ToInt32(tacAssistantTeacher.Value);
            else
                entity.AssistantTeacherID = null;
            entity.Remarks = txtRemarks.Text;
            entity.NextMeetingRemarks = txtNextMeetingRemarks.Text;
            if (tacSubjectCurriculumMeetingPlan.Value != "")
                entity.SubjectCurriculumMeetingPlanID = Convert.ToInt32(tacSubjectCurriculumMeetingPlan.Value);
            else
                entity.SubjectCurriculumMeetingPlanID = null;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            if (type == "save")
            {
                bool result = true;
                IDbContext ctx = DbFactory.Configure(true);
                ClassMeetingDao entityDao = new ClassMeetingDao(ctx);
                ClassMeetingIndicatorDao entityIndicatorDao = new ClassMeetingIndicatorDao(ctx);
                try
                {
                    if (AppSession.ClassSubject.ClassMeetingID == 0)
                    {
                        ClassMeeting entity = new ClassMeeting();
                        ControlToEntity(entity);
                        entity.PeriodSectionID = AppSession.ClassSubject.PeriodSectionID;
                        entity.ClassSubjectID = AppSession.ClassSubject.ClassSubjectID;
                        entity.CreatedBy = AppSession.UserLogin.UserID;
                        entityDao.Insert(entity);
                        entity.ClassMeetingID = BusinessLayer.GetClassMeetingMaxID(ctx);

                        if (hdnSubjectIndicatorSave.Value != "")
                        {
                            string[] lstSaveValue = hdnSubjectIndicatorSave.Value.Split('|');
                            foreach (string saveValue in lstSaveValue)
                            {
                                string[] temp = saveValue.Split(',');
                                int classMeetingIndicatorID = Convert.ToInt32(temp[0]);
                                string subjectIndicatorID = temp[1];
                                string subjectIndicatorName = temp[2];
                                ClassMeetingIndicator entityIndicator = new ClassMeetingIndicator();
                                entityIndicator.ClassMeetingID = entity.ClassMeetingID;
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

                        ClassSubjectModel classSubject = new ClassSubjectModel();
                        classSubject.ClassScheduleID = AppSession.ClassSubject.ClassScheduleID;
                        classSubject.PeriodSectionID = AppSession.ClassSubject.PeriodSectionID;
                        classSubject.ClassSubjectID = AppSession.ClassSubject.ClassSubjectID;
                        classSubject.CurriculumID = AppSession.ClassSubject.CurriculumID;
                        classSubject.ClassMeetingID = entity.ClassMeetingID;

                        AppSession.ClassSubject = classSubject;
                    }
                    else
                    {
                        ClassMeeting entity = entityDao.Get(AppSession.ClassSubject.ClassMeetingID);
                        ControlToEntity(entity);
                        entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDao.Update(entity);

                        List<ClassMeetingIndicator> lstEntityIndicator = BusinessLayer.GetClassMeetingIndicatorList(string.Format("ClassMeetingID = {0}", entity.ClassMeetingID), ctx);
                        if (hdnSubjectIndicatorSave.Value != "")
                        {
                            string[] lstSaveValue = hdnSubjectIndicatorSave.Value.Split('|');
                            foreach (string saveValue in lstSaveValue)
                            {
                                string[] temp = saveValue.Split(',');
                                int classMeetingIndicatorID = Convert.ToInt32(temp[0]);
                                string subjectIndicatorID = temp[1];
                                string subjectIndicatorName = temp[2];
                                ClassMeetingIndicator entityIndicator = lstEntityIndicator.FirstOrDefault(p => p.ClassMeetingIndicatorID == Convert.ToInt32(classMeetingIndicatorID));
                                if (entityIndicator == null)
                                {
                                    entityIndicator = new ClassMeetingIndicator();
                                    entityIndicator.ClassMeetingID = entity.ClassMeetingID;
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

                        foreach (ClassMeetingIndicator entityIndicator in lstEntityIndicator)
                        {
                            entityIndicatorDao.Delete(entityIndicator.ClassMeetingIndicatorID);
                        }
                    }
                    ctx.CommitTransaction();
                }
                catch (Exception ex)
                {
                    Helper.InsertErrorLog(ex);
                    ctx.RollBackTransaction();
                    errMessage = ex.Message;
                    result = false;
                }
                finally
                {
                    ctx.Close();
                }
                return result;
            }
            return false;
        }
    }
}