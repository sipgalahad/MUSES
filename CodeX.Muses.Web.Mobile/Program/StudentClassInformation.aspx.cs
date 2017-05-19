using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common;
using CodeX.Data.Model;
using System.Web.Security;
using System.Text;
using CodeX.Common;
using CodeX.Web.Common.UI;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.Mobile.Program
{
    public partial class StudentClassInformation : BasePageContent
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Mobile.STUDENT_CLASS_INFO;
        }

        protected string OnGetPeriodSectionFilterExpression()
        {
            return string.Format("GCPeriodSectionStatus != '{0}'", Constant.SchoolPeriodStatus.VOID);
        }

        List<vClassMeetingAttendance> lstAttendance = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hdnStudentID.Value = AppSession.StudentLogin.UserID.ToString();
                vStudent student = BusinessLayer.GetvStudentList(string.Format("StudentID = {0}", hdnStudentID.Value)).FirstOrDefault();
                hdnSiteID.Value = student.SiteID;
                txtSchoolDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);

                h3Title.InnerHtml = student.StudentName;
                divClass.InnerHtml = student.SchoolClassName;

                imgPatientImage.Src = student.StudentImageUrl;
                imgPatientImage.Attributes.Add("gender", student.GCGender);

                List<SchoolPeriod> lstSchoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SiteID = '{0}' AND GCSchoolPeriodStatus != '{1}'", hdnSiteID.Value, Constant.SchoolPeriodStatus.VOID));
                Methods.SetComboBoxField<SchoolPeriod>(cboSchoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
                SchoolPeriod selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);
                if (selectedSchoolPeriod == null)
                    cboSchoolPeriod.SelectedIndex = 0;
                else
                    cboSchoolPeriod.Value = selectedSchoolPeriod.SchoolPeriodID.ToString();

                if (cboSchoolPeriod.Value != null)
                {
                    List<PeriodSection> lstPeriodSection = BusinessLayer.GetPeriodSectionList(string.Format("SchoolPeriodID = {0} AND '{1}' BETWEEN StartDate AND EndDate", cboSchoolPeriod.Value, DateTime.Now.ToString("yyyyMMdd")));
                    if (lstPeriodSection.Count > 0)
                    {
                        PeriodSection periodSection = lstPeriodSection.FirstOrDefault();
                        tacPeriodSection.Value = periodSection.PeriodSectionID.ToString();
                        tacPeriodSection.Text = periodSection.PeriodSectionName;
                    }

                    vClassStudent schoolClass = BusinessLayer.GetvClassStudentList(string.Format("SchoolPeriodID = {0} AND StudentID = {1}", cboSchoolPeriod.Value, hdnStudentID.Value)).FirstOrDefault();
                    if (schoolClass != null)
                    {
                        hdnSchoolClassID.Value = schoolClass.SchoolClassID.ToString();
                        List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SCHOOL_DAILY_SCHEDULE_TYPE, Constant.StandardCode.SCHOOL_DAY));
                        List<StandardCode> lstSchoolDay = lstSc.Where(p => p.ParentID == Constant.StandardCode.SCHOOL_DAY).ToList();
                        decimal width = 100 / lstSchoolDay.Count;
                        if (lstSchoolDay.Count(p => p.StandardCodeID == string.Format("{0}^001", Constant.StandardCode.SCHOOL_DAY)) < 1)
                            tdSchoolDay1.Style.Add("display", "none");
                        if (lstSchoolDay.Count(p => p.StandardCodeID == string.Format("{0}^002", Constant.StandardCode.SCHOOL_DAY)) < 1)
                            tdSchoolDay2.Style.Add("display", "none");
                        if (lstSchoolDay.Count(p => p.StandardCodeID == string.Format("{0}^003", Constant.StandardCode.SCHOOL_DAY)) < 1)
                            tdSchoolDay3.Style.Add("display", "none");
                        if (lstSchoolDay.Count(p => p.StandardCodeID == string.Format("{0}^004", Constant.StandardCode.SCHOOL_DAY)) < 1)
                            tdSchoolDay4.Style.Add("display", "none");
                        if (lstSchoolDay.Count(p => p.StandardCodeID == string.Format("{0}^005", Constant.StandardCode.SCHOOL_DAY)) < 1)
                            tdSchoolDay5.Style.Add("display", "none");
                        if (lstSchoolDay.Count(p => p.StandardCodeID == string.Format("{0}^006", Constant.StandardCode.SCHOOL_DAY)) < 1)
                            tdSchoolDay6.Style.Add("display", "none");
                        tdSchoolDay1.Style.Add("width", string.Format("{0}%", width));
                        tdSchoolDay2.Style.Add("width", string.Format("{0}%", width));
                        tdSchoolDay3.Style.Add("width", string.Format("{0}%", width));
                        tdSchoolDay4.Style.Add("width", string.Format("{0}%", width));
                        tdSchoolDay5.Style.Add("width", string.Format("{0}%", width));
                        tdSchoolDay6.Style.Add("width", string.Format("{0}%", width));

                        selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault(p => p.SchoolPeriodID == Convert.ToInt32(cboSchoolPeriod.Value));
                        DailySchedulePackage entity = BusinessLayer.GetDailySchedulePackage(selectedSchoolPeriod.DailySchedulePackageID);
                        List<DailyScheduleTypeDt> lstEntityDt = BusinessLayer.GetDailyScheduleTypeDtList(string.Format("DailyScheduleTypeID IN ({0},{1},{2},{3},{4},{5}) AND IsDeleted = 0",
                            entity.DailyScheduleTypeID1 == null ? "0" : entity.DailyScheduleTypeID1.ToString(),
                            entity.DailyScheduleTypeID2 == null ? "0" : entity.DailyScheduleTypeID2.ToString(),
                            entity.DailyScheduleTypeID3 == null ? "0" : entity.DailyScheduleTypeID3.ToString(),
                            entity.DailyScheduleTypeID4 == null ? "0" : entity.DailyScheduleTypeID4.ToString(),
                            entity.DailyScheduleTypeID5 == null ? "0" : entity.DailyScheduleTypeID5.ToString(),
                            entity.DailyScheduleTypeID6 == null ? "0" : entity.DailyScheduleTypeID6.ToString()
                        ));


                        lstClassSchedule = BusinessLayer.GetvClassScheduleList(string.Format("SchoolPeriodID = {0} AND SchoolClassID = {1} AND GCClassStudyType = '{2}' AND IsDeleted = 0", cboSchoolPeriod.Value, hdnSchoolClassID.Value, Constant.ClassStudyType.REGULAR));

                        rptDay1.DataSource = lstEntityDt.Where(p => p.DailyScheduleTypeID == entity.DailyScheduleTypeID1).ToList();
                        rptDay1.DataBind();
                        rptDay2.DataSource = lstEntityDt.Where(p => p.DailyScheduleTypeID == entity.DailyScheduleTypeID2).ToList();
                        rptDay2.DataBind();
                        rptDay3.DataSource = lstEntityDt.Where(p => p.DailyScheduleTypeID == entity.DailyScheduleTypeID3).ToList();
                        rptDay3.DataBind();
                        rptDay4.DataSource = lstEntityDt.Where(p => p.DailyScheduleTypeID == entity.DailyScheduleTypeID4).ToList();
                        rptDay4.DataBind();
                        rptDay5.DataSource = lstEntityDt.Where(p => p.DailyScheduleTypeID == entity.DailyScheduleTypeID5).ToList();
                        rptDay5.DataBind();
                        rptDay6.DataSource = lstEntityDt.Where(p => p.DailyScheduleTypeID == entity.DailyScheduleTypeID6).ToList();
                        rptDay6.DataBind();
                    }
                }

                List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SCHOOL_PERIOD_SCHEDULE_TYPE));
                List<StandardCode> lstScheduleType = lstStandardCode.Where(p => p.ParentID == Constant.StandardCode.SCHOOL_PERIOD_SCHEDULE_TYPE).ToList();
                rptRemarks.DataSource = lstScheduleType;
                rptRemarks.DataBind();

                rptDateStyle.DataSource = lstScheduleType;
                rptDateStyle.DataBind();

                if (cboSchoolPeriod.Value != null)
                {
                    SchoolPeriod schoolPeriod = BusinessLayer.GetSchoolPeriod(Convert.ToInt32(cboSchoolPeriod.Value));
                    hdnMaxDate.Value = schoolPeriod.EndDate.ToString("yyyy-MM-dd");
                    hdnMinDate.Value = schoolPeriod.StartDate.ToString("yyyy-MM-dd");
                    hdnYear.Value = DateTime.Now.Year.ToString();
                    hdnMonth.Value = DateTime.Now.Month.ToString();
                }

                List<vSchoolAnnouncement> lstAnnouncement = BusinessLayer.GetvSchoolAnnouncementList(string.Format("SiteID = '{0}' AND EndDate >= '{1}' AND StartDate <= '{1}' AND IsDeleted = 0", hdnSiteID.Value, DateTime.Now.ToString("yyyyMMdd")));
                //List<SchoolAnnouncement> lstAnnouncement = BusinessLayer.GetSchoolAnnouncementList("");
                grdAnnouncement.DataSource = lstAnnouncement;
                grdAnnouncement.DataBind();

                
                List<vClassSubject> lstClassSubject = BusinessLayer.GetvClassSubjectList(string.Format("SchoolClassID = {0} AND SubjectGCClassStudyType = '{1}' AND IsDeleted = 0", hdnSchoolClassID.Value, Constant.ClassStudyType.REGULAR));
                string lstClassSubjectID = string.Join(",", lstClassSubject.Select(p => p.ClassSubjectID).ToList());
                if (lstClassSubjectID != "")
                    lstClassMeetingAttendance = BusinessLayer.GetvClassMeetingAttendanceList(string.Format("ClassSubjectID IN ({0}) AND StudentID = {1}", lstClassSubjectID, hdnStudentID.Value));
                lstClassStudentDailyAttendance = BusinessLayer.GetvClassStudentDailyAttendanceList(string.Format("SchoolClassID = {0} AND PeriodSectionID = {1} AND StudentID = {2}", hdnSchoolClassID.Value, tacPeriodSection.Value, hdnStudentID.Value));

                lstAttendanceStatus = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.STUDENT_ATTENDANCE));

                rptHeader.DataSource = lstAttendanceStatus;
                rptHeader.DataBind();
                thHeaderAttendance.ColSpan = lstAttendanceStatus.Count;

                lstClassSubject.Insert(0, new vClassSubject { ClassSubjectID = 0, SubjectName = "-- Harian --" });
                rptSubject.DataSource = lstClassSubject;
                rptSubject.DataBind();

                string filterExpression = string.Format("SiteID = '{0}' AND IsDeleted = 0", hdnSiteID.Value);
                /*if (!chkShowAll.Checked)
                {
                    Int32 Month = Convert.ToInt32(hdnMonth.Value);
                    filterExpression += String.Format(" AND (StartDate LIKE '{0}-{1}%' OR EndDate LIKE '{0}-{1}%')", hdnYear.Value, Month.ToString("00"));
                }*/
                List<vPeriodSchedule> lstEntity = BusinessLayer.GetvPeriodScheduleList(filterExpression);
                grdSchedule.DataSource = lstEntity;
                grdSchedule.DataBind();

                BindGridView();
            }
        }

        protected void cbpSchedule_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        List<StandardCode> lstAttendanceStatus = null;
        List<vClassMeetingAttendance> lstClassMeetingAttendance = null;
        List<vClassStudentDailyAttendance> lstClassStudentDailyAttendance = null;
        protected void rptSubject_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptStudentAttendance = (Repeater)e.Item.FindControl("rptStudentAttendance");
                rptStudentAttendance.DataSource = lstAttendanceStatus;
                rptStudentAttendance.DataBind();
            }
        }

        protected void rptStudentAttendance_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                StandardCode classMeeting = (StandardCode)e.Item.DataItem;
                vClassSubject subject = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vClassSubject;
                HtmlGenericControl divStudentAttendance = (HtmlGenericControl)e.Item.FindControl("divStudentAttendance");
                if (subject.SubjectID == 0)
                    divStudentAttendance.InnerHtml = lstClassStudentDailyAttendance.Where(p => p.GCAttendanceStatus == classMeeting.StandardCodeID).Count().ToString();
                else
                    divStudentAttendance.InnerHtml = lstClassMeetingAttendance.Where(p => p.GCAttendanceStatus == classMeeting.StandardCodeID && p.ClassSubjectID == subject.ClassSubjectID).Count().ToString();
                    
            }
        }


        protected void rptDay1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            rptDay_ItemDataBound(e, 1);
        }

        protected void rptDay2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            rptDay_ItemDataBound(e, 2);
        }

        protected void rptDay3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            rptDay_ItemDataBound(e, 3);
        }

        protected void rptDay4_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            rptDay_ItemDataBound(e, 4);
        }

        protected void rptDay5_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            rptDay_ItemDataBound(e, 5);
        }

        protected void rptDay6_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            rptDay_ItemDataBound(e, 6);
        }

        private void rptDay_ItemDataBound(RepeaterItemEventArgs e, Int16 DayNumber)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                DailyScheduleTypeDt entityTypeDt = e.Item.DataItem as DailyScheduleTypeDt;
                vClassSchedule entity = lstClassSchedule.FirstOrDefault(p => p.DayNumber == DayNumber && p.HoursIndex == entityTypeDt.HoursIndex);
                HtmlTableCell tdHtmlText = (HtmlTableCell)e.Item.FindControl("tdHtmlText");
                HtmlTableCell tdClassSubjectID = (HtmlTableCell)e.Item.FindControl("tdClassSubjectID");
                HtmlTableCell tdClassScheduleID = (HtmlTableCell)e.Item.FindControl("tdClassScheduleID");
                if (entity != null)
                {
                    tdClassSubjectID.InnerHtml = entity.ClassSubjectID.ToString();
                    tdClassScheduleID.InnerHtml = entity.ClassScheduleID.ToString();
                    tdHtmlText.InnerHtml = string.Format("{0} - {1}<br/>{2}<br/>(<b>{3}</b>)<br/>{4}", entityTypeDt.StartTime, entityTypeDt.EndTime, entity.cfTeacherName, entity.SubjectName, entity.RoomName);
                }
                else
                    tdHtmlText.InnerHtml = string.Format("{0} - {1}", entityTypeDt.StartTime, entityTypeDt.EndTime);
            }
        }
        List<vClassSchedule> lstClassSchedule = null;

        private void BindGridView()
        {
            DateTime schoolDate = Helper.GetDatePickerValue(txtSchoolDate.Text);
            vClassStudentDailyAttendance entityAttendance = BusinessLayer.GetvClassStudentDailyAttendanceList(string.Format("StudentID = {0} AND SchoolDate = '{1}'", hdnStudentID.Value, schoolDate.ToString("yyyyMMdd"))).FirstOrDefault();
            if (entityAttendance != null)
                spnAttendanceStatus.InnerHtml = entityAttendance.AttendanceStatus;

            SchoolPeriod entitySchoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SiteID = '{0}' AND GCSchoolPeriodStatus != '{1}' AND '{2}' BETWEEN StartDate AND EndDate", hdnSiteID.Value, Constant.SchoolPeriodStatus.VOID, schoolDate.ToString("yyyyMMdd"))).FirstOrDefault();
            if (entitySchoolPeriod != null)
            {
                vClassStudent classStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolPeriodID = {0} AND StudentID = {1}", entitySchoolPeriod.SchoolPeriodID, hdnStudentID.Value)).FirstOrDefault();
                if (classStudent != null)
                {
                    List<vClassMeeting> lstClassMeeting = BusinessLayer.GetvClassMeetingList(string.Format("SchoolClassID = {0} AND MeetingDate = '{1}' AND IsDeleted = 0", classStudent.SchoolClassID, schoolDate.ToString("yyyyMMdd"))); ;

                    if (lstClassMeeting.Count > 0)
                    {
                        string lstClassMeetingID = string.Join(",", lstClassMeeting.Select(p => p.ClassMeetingID).ToList());
                        lstAttendance = BusinessLayer.GetvClassMeetingAttendanceList(string.Format("ClassMeetingID IN ({0}) AND StudentID = {1}", lstClassMeetingID, hdnStudentID.Value));
                    }
                    else
                        lstAttendance = new List<vClassMeetingAttendance>();

                    rptClassMeeting.DataSource = lstClassMeeting;
                    rptClassMeeting.DataBind();

                    List<vClassSubjectTask> lstClassTask = BusinessLayer.GetvClassSubjectTaskList(string.Format("SchoolClassID = {0} AND EndDate = '{1}' AND IsDeleted = 0", classStudent.SchoolClassID, schoolDate.ToString("yyyyMMdd")));
                    if (lstClassTask.Count > 0)
                    {
                        string lstClassTaskID = string.Join(",", lstClassTask.Select(p => p.ClassSubjectTaskID).ToList());
                        lstStudentTaskMark = BusinessLayer.GetClassStudentSubjectTaskMarkList(string.Format("ClassSubjectTaskID IN ({0})", lstClassTaskID));
                    }
                    else
                        lstStudentTaskMark = new List<ClassStudentSubjectTaskMark>();

                    rptClassTask.DataSource = lstClassTask;
                    rptClassTask.DataBind();
                }
            }
        }

        private List<ClassStudentSubjectTaskMark> lstStudentTaskMark = null;
        protected void rptClassTask_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                vClassSubjectTask classTask = (vClassSubjectTask)e.Item.DataItem;
                List<ClassStudentSubjectTaskMark> lstMark = lstStudentTaskMark.Where(p => p.ClassSubjectTaskID == classTask.ClassSubjectTaskID).ToList();

                ClassStudentSubjectTaskMark mark = lstMark.FirstOrDefault(p => p.StudentID == Convert.ToInt32(hdnStudentID.Value));
                if (mark != null)
                {
                    HtmlGenericControl divMark = (HtmlGenericControl)e.Item.FindControl("divMark");
                    divMark.InnerHtml = mark.Mark.ToString();
                }
                if (lstMark.Count > 0)
                {
                    HtmlGenericControl divAttendanceStatus = (HtmlGenericControl)e.Item.FindControl("divAttendanceStatus");
                    divAttendanceStatus.InnerHtml = lstMark.Average(p => p.Mark).ToString();
                }
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected void rptClassMeeting_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                vClassMeeting classMeeting = (vClassMeeting)e.Item.DataItem;
                vClassMeetingAttendance attendance = lstAttendance.FirstOrDefault(p => p.ClassMeetingID == classMeeting.ClassMeetingID);
                if (attendance != null)
                {
                    HtmlGenericControl divAttendanceStatus = (HtmlGenericControl)e.Item.FindControl("divAttendanceStatus");
                    divAttendanceStatus.InnerHtml = attendance.AttendanceStatus;
                }
            }
        }
    }
}