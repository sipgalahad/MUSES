using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Data.Model;
using CodeX.Web.Common;
using CodeX.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ClassWeeklyScheduleList : BasePageList
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.CLASS_WEEKLY_SCHEDULE;
        }

        protected string OnGetClassStudyTypeRegular()
        {
            return Constant.ClassStudyType.REGULAR;
        }

        protected string OnGetPeriodSectionFilterExpression()
        {
            return string.Format("GCPeriodSectionStatus != '{0}'", Constant.SchoolPeriodStatus.VOID);
        }

        protected string OnGetPeriodSectionNowFilterExpression()
        {
            return string.Format("'{0}' BETWEEN StartDate AND EndDate", DateTime.Now.ToString("yyyyMMdd"));
        }

        protected string OnGetSchoolPeriodNowFilterExpression()
        {
            return string.Format("GCSchoolPeriodStatus != '{0}' AND StartDate <= '{1}' AND EndDate >= '{1}'", Constant.SchoolPeriodStatus.VOID, DateTime.Now.ToString("yyyyMMdd"));
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
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

            List<vSite> lstSite = BusinessLayer.GetvSiteList(String.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsHeader = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<vSite>(cboSite, lstSite, "SiteName", "SiteID");
            cboSite.SelectedIndex = 0;

            BindGridView();
        }

        #region Bind Grid View
        private void BindGridView()
        {
            if (tacSchoolClass.Value != "")
            {
                if (tacSchoolPeriod.Value != "" && tacSchoolPeriod.Value.ToString() != "0")
                {
                    SchoolPeriod schoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SchoolPeriodID = {0}", tacSchoolPeriod.Value)).FirstOrDefault();
                    DailySchedulePackage entity = BusinessLayer.GetDailySchedulePackage(schoolPeriod.DailySchedulePackageID);
                    List<DailyScheduleTypeDt> lstEntityDt = BusinessLayer.GetDailyScheduleTypeDtList(string.Format("DailyScheduleTypeID IN ({0},{1},{2},{3},{4},{5}) AND IsDeleted = 0",
                        entity.DailyScheduleTypeID1 == null ? "0" : entity.DailyScheduleTypeID1.ToString(),
                        entity.DailyScheduleTypeID2 == null ? "0" : entity.DailyScheduleTypeID2.ToString(),
                        entity.DailyScheduleTypeID3 == null ? "0" : entity.DailyScheduleTypeID3.ToString(),
                        entity.DailyScheduleTypeID4 == null ? "0" : entity.DailyScheduleTypeID4.ToString(),
                        entity.DailyScheduleTypeID5 == null ? "0" : entity.DailyScheduleTypeID5.ToString(),
                        entity.DailyScheduleTypeID6 == null ? "0" : entity.DailyScheduleTypeID6.ToString()
                    ));


                    lstClassSchedule = BusinessLayer.GetvClassScheduleList(string.Format("SchoolPeriodID = {0} AND SchoolClassID = {1} AND GCClassStudyType = '{2}' AND IsDeleted = 0", tacSchoolPeriod.Value, tacSchoolClass.Value, Constant.ClassStudyType.REGULAR));

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

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion
    }
}