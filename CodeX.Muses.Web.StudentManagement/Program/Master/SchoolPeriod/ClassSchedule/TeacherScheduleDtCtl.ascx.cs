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
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class TeacherScheduleDtCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SCHOOL_DAILY_SCHEDULE_TYPE, Constant.StandardCode.SCHOOL_DAY));
            rptRemarks.DataSource = lstSc.Where(p => p.ParentID == Constant.StandardCode.SCHOOL_DAILY_SCHEDULE_TYPE).ToList();
            rptRemarks.DataBind();

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

            hdnID.Value = param;
            Employee entityTeacher = BusinessLayer.GetEmployee(Convert.ToInt32(hdnID.Value));

            txtTeacherCode.Text = entityTeacher.EmployeeCode;
            txtTeacherName.Text = entityTeacher.FullName;

            SchoolPeriod schoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SchoolPeriodID = {0}", AppSession.SchoolPeriodID)).FirstOrDefault();
            DailySchedulePackage entity = BusinessLayer.GetDailySchedulePackage(schoolPeriod.DailySchedulePackageID);
            List<DailyScheduleTypeDt> lstEntityDt = BusinessLayer.GetDailyScheduleTypeDtList(string.Format("DailyScheduleTypeID IN ({0},{1},{2},{3},{4},{5}) AND IsDeleted = 0",
                entity.DailyScheduleTypeID1 == null ? "0" : entity.DailyScheduleTypeID1.ToString(),
                entity.DailyScheduleTypeID2 == null ? "0" : entity.DailyScheduleTypeID2.ToString(),
                entity.DailyScheduleTypeID3 == null ? "0" : entity.DailyScheduleTypeID3.ToString(),
                entity.DailyScheduleTypeID4 == null ? "0" : entity.DailyScheduleTypeID4.ToString(),
                entity.DailyScheduleTypeID5 == null ? "0" : entity.DailyScheduleTypeID5.ToString(),
                entity.DailyScheduleTypeID6 == null ? "0" : entity.DailyScheduleTypeID6.ToString()
            ));

            lstClassSchedule = BusinessLayer.GetvClassScheduleList(string.Format("SchoolPeriodID = {0} AND TeacherID = {1} AND GCClassStudyType = '{2}' AND IsDeleted = 0", AppSession.SchoolPeriodID, hdnID.Value, Constant.ClassStudyType.REGULAR));

            spnNumSlot.InnerHtml = lstClassSchedule.Count.ToString(); // lstClassSchedule.Count.ToString();
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
                    tdHtmlText.InnerHtml = string.Format("{0} - {1}<br/>{2}<br/>(<b>{3}</b>)<br/>{4}", entityTypeDt.StartTime, entityTypeDt.EndTime, entity.SchoolClassName, entity.SubjectName, entity.RoomName);
                }
                else
                    tdHtmlText.InnerHtml = string.Format("{0} - {1}", entityTypeDt.StartTime, entityTypeDt.EndTime);
            }
        }
        List<vClassSchedule> lstClassSchedule = null;
    }
}