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
        protected string OnGetRoomFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }

        protected string OnGetTeacherFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }

        protected override void InitializeDataControl()
        {
            if (AppSession.ClassSubject.ClassMeetingID == 0)
            {
                vClassSchedule entity = BusinessLayer.GetvClassScheduleList(string.Format("ClassScheduleID = {0}", AppSession.ClassSubject.ClassScheduleID)).FirstOrDefault();
                txtMeetingDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                txtStartTime.Text = entity.StartTime;
                txtEndTime.Text = entity.EndTime;
                tacRoom.Value = entity.RoomID.ToString();
                tacRoom.Text = entity.RoomName;
                tacTeacher.Value = entity.TeacherID.ToString();
                tacTeacher.Text = entity.TeacherName;
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
                txtRemarks.Text = entity.Remarks;
                txtNextMeetingRemarks.Text = entity.NextMeetingRemarks;
            }
        
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
            entity.Remarks = txtRemarks.Text;
            entity.NextMeetingRemarks = txtNextMeetingRemarks.Text;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            if (type == "save")
            {
                bool result = true;
                IDbContext ctx = DbFactory.Configure(true);
                ClassMeetingDao entityDao = new ClassMeetingDao(ctx);
                try
                {
                    if (AppSession.ClassSubject.ClassMeetingID == 0)
                    {
                        ClassMeeting entity = new ClassMeeting();
                        ControlToEntity(entity);
                        entity.ClassSubjectID = AppSession.ClassSubject.ClassSubjectID;
                        entity.CreatedBy = AppSession.UserLogin.UserID;
                        entityDao.Insert(entity);
                        AppSession.ClassSubject.ClassMeetingID = BusinessLayer.GetClassMeetingMaxID(ctx);
                    }
                    else
                    {
                        ClassMeeting entity = entityDao.Get(AppSession.ClassSubject.ClassMeetingID);
                        ControlToEntity(entity);
                        entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDao.Update(entity);
                    }
                    ctx.CommitTransaction();
                }
                catch (Exception ex)
                {
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