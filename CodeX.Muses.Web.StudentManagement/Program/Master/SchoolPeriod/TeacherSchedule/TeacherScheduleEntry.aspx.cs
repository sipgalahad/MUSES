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
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class TeacherScheduleEntry : BasePageTrx
    {
        protected string OnGetTeacherFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SP_TEACHER_SCHEDULE;
        }
        protected override void InitializeDataControl()
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

            BindGridView();
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }
        List<vTeacherSchedule> lstTeacherSchedule = null;
        private void BindGridView()
        {
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
            lstTeacherSchedule = BusinessLayer.GetvTeacherScheduleList(string.Format("SchoolPeriodID = {0} AND IsDeleted = 0", AppSession.SchoolPeriodID));
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
                List<vTeacherSchedule> lstEntity = lstTeacherSchedule.Where(p => p.DayNumber == DayNumber && p.HoursIndex == entityTypeDt.HoursIndex).ToList();
                Repeater rptTeacherScheduleDt = (Repeater)e.Item.FindControl("rptTeacherScheduleDt");
                rptTeacherScheduleDt.DataSource = lstEntity;
                rptTeacherScheduleDt.DataBind();
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TeacherScheduleDao entityDtDao = new TeacherScheduleDao(ctx);
            try
            {
                string[] lstSaveValue = hdnSaveValue.Value.Split('|');

                List<TeacherSchedule> lstTeacherSchedule = BusinessLayer.GetTeacherScheduleList(string.Format("SchoolPeriodID = {0} AND IsDeleted = 0", AppSession.SchoolPeriodID), ctx);
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(',');
                    int teacherID = Convert.ToInt32(temp[0]);
                    short hoursIndex = Convert.ToInt16(temp[1]);
                    short dayNumber = Convert.ToInt16(temp[2]);
                    TeacherSchedule entityDt = lstTeacherSchedule.FirstOrDefault(p => p.TeacherID == teacherID && p.DayNumber == dayNumber && p.HoursIndex == hoursIndex);
                    if (entityDt == null)
                    {
                        entityDt = new TeacherSchedule();
                        entityDt.SchoolPeriodID = AppSession.SchoolPeriodID;
                        entityDt.HoursIndex = hoursIndex;
                        entityDt.DayNumber = dayNumber;
                        entityDt.TeacherID = teacherID;
                        entityDt.CreatedBy = AppSession.UserLogin.UserID;
                        entityDtDao.Insert(entityDt);
                    }
                    else
                        lstTeacherSchedule.Remove(entityDt);
                }

                foreach (TeacherSchedule entity in lstTeacherSchedule)
                {
                    entity.IsDeleted = true;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Update(entity);
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                result = false;
                errMessage = ex.Message;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
    }
}