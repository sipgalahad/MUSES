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
    public partial class TeacherSubstitutionPerDateEntry : BasePageTrx
    {
        protected string OnGetRoomFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.TSB_TEACHER_SUBSTITUTION_PER_DATE;
        }
        protected override void InitializeDataControl()
        {
            vTeacherAbsence teacherAbsence = BusinessLayer.GetvTeacherAbsenceList(string.Format("TeacherAbsenceID = {0}", AppSession.TeacherAbsenceID)).FirstOrDefault();
            hdnTeacherID.Value = teacherAbsence.TeacherID.ToString();
            hdnTeacherName.Value = teacherAbsence.TeacherName;
            hdnSchoolPeriodID.Value = teacherAbsence.SchoolPeriodID.ToString();
            txtSchoolDate.Text = teacherAbsence.StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);

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
            BindGridClassSubject();
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            DateTime schoolDate = Helper.GetDatePickerValue(txtSchoolDate.Text);
            int day = (int)schoolDate.DayOfWeek;
            SchoolPeriod schoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SchoolPeriodID = {0}", hdnSchoolPeriodID.Value)).FirstOrDefault();
            DailySchedulePackage entity = BusinessLayer.GetDailySchedulePackage(schoolPeriod.DailySchedulePackageID);
            string filterExpression = "1 = 0";
            switch (day)
            {
                case 1: filterExpression = string.Format("DailyScheduleTypeID = {0} AND IsDeleted = 0", entity.DailyScheduleTypeID1 == null ? "0" : entity.DailyScheduleTypeID1.ToString());break;
                case 2: filterExpression = string.Format("DailyScheduleTypeID = {0} AND IsDeleted = 0", entity.DailyScheduleTypeID2 == null ? "0" : entity.DailyScheduleTypeID2.ToString()); break;
                case 3: filterExpression = string.Format("DailyScheduleTypeID = {0} AND IsDeleted = 0", entity.DailyScheduleTypeID3 == null ? "0" : entity.DailyScheduleTypeID3.ToString()); break;
                case 4: filterExpression = string.Format("DailyScheduleTypeID = {0} AND IsDeleted = 0", entity.DailyScheduleTypeID4 == null ? "0" : entity.DailyScheduleTypeID4.ToString()); break;
                case 5: filterExpression = string.Format("DailyScheduleTypeID = {0} AND IsDeleted = 0", entity.DailyScheduleTypeID5 == null ? "0" : entity.DailyScheduleTypeID5.ToString()); break;
                case 6: filterExpression = string.Format("DailyScheduleTypeID = {0} AND IsDeleted = 0", entity.DailyScheduleTypeID6 == null ? "0" : entity.DailyScheduleTypeID6.ToString()); break;
            }
            List<DailyScheduleTypeDt> lstEntityDt = new List<DailyScheduleTypeDt>();
            rptDay1.DataSource = lstEntityDt;
            rptDay1.DataBind();
            rptDay2.DataSource = lstEntityDt;
            rptDay2.DataBind();
            rptDay3.DataSource = lstEntityDt;
            rptDay3.DataBind();
            rptDay4.DataSource = lstEntityDt;
            rptDay4.DataBind();
            rptDay5.DataSource = lstEntityDt;
            rptDay5.DataBind();
            rptDay6.DataSource = lstEntityDt;
            rptDay6.DataBind();

            lstEntityDt = BusinessLayer.GetDailyScheduleTypeDtList(filterExpression);

            lstTeacherSubstitution = BusinessLayer.GetvTeacherSubstitutionList(string.Format("TeacherAbsenceID = {0} AND SchoolDate = '{1}' AND IsDeleted = 0", AppSession.TeacherAbsenceID, schoolDate.ToString("yyyyMMdd")));
            List<vClassSchedule> lstTempClassSchedule = BusinessLayer.GetvClassScheduleList(string.Format("SchoolPeriodID = {0} AND TeacherID = {1} AND IsDeleted = 0", hdnSchoolPeriodID.Value, hdnTeacherID.Value));
            lstClassSchedule = lstTempClassSchedule.Where(p => p.GCClassStudyType == Constant.ClassStudyType.REGULAR).ToList();
            switch (day)
            {
                case 1: rptDay1.DataSource = lstEntityDt;
                        rptDay1.DataBind();break;
                case 2: rptDay2.DataSource = lstEntityDt;
                        rptDay2.DataBind(); break;
                case 3: rptDay3.DataSource = lstEntityDt;
                        rptDay3.DataBind(); break;
                case 4: rptDay4.DataSource = lstEntityDt;
                        rptDay4.DataBind(); break;
                case 5: rptDay5.DataSource = lstEntityDt;
                        rptDay5.DataBind(); break;
                case 6: rptDay6.DataSource = lstEntityDt;
                        rptDay6.DataBind(); break;
            }
        }

        private void BindGridClassSubject()
        {
            string filterExpression = string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
            List<vTeacher> lstEntity = BusinessLayer.GetvTeacherList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
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
                if (entity != null)
                {
                    HtmlTableCell tdValue = (HtmlTableCell)e.Item.FindControl("tdValue");
                    tdValue.InnerHtml = entity.ClassScheduleID.ToString();

                    vTeacherSubstitution entityTeacherSubstitution = lstTeacherSubstitution.FirstOrDefault(p => p.ClassScheduleID == entity.ClassScheduleID);
                    if (entityTeacherSubstitution != null)
                    {
                        HtmlTableCell tdTeacherSubstitutionID = (HtmlTableCell)e.Item.FindControl("tdTeacherSubstitutionID");
                        tdTeacherSubstitutionID.InnerHtml = entityTeacherSubstitution.TeacherSubstitutionID.ToString();

                        HtmlTableCell tdTeacherID = (HtmlTableCell)e.Item.FindControl("tdTeacherID");
                        tdTeacherID.InnerHtml = entityTeacherSubstitution.TeacherID.ToString();

                        tdHtmlText.InnerHtml = string.Format("<div style='float:right' class='divDetailDelete'></div>{0}<br/><b class='bTeacherName'>{1}</b><br/><label>{2}</label>", entity.SubjectName, entityTeacherSubstitution.TeacherName, entity.RoomName);
                    }
                    else
                        tdHtmlText.InnerHtml = string.Format("<div style='float:right' class='divDetailDelete'></div>{0}<br/><b class='bTeacherName'>{1}</b><br/><label>{2}</label>", entity.SubjectName, entity.TeacherName, entity.RoomName);
                }
                else
                {
                    tdHtmlText.InnerHtml = string.Format("{0} - {1}", entityTypeDt.StartTime, entityTypeDt.EndTime);
                }
            }
        }
        List<vTeacherSubstitution> lstTeacherSubstitution = null;
        List<vClassSchedule> lstClassSchedule = null;

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        protected void cbpClassSubject_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridClassSubject();
        }
        #endregion

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TeacherSubstitutionDao entityDtDao = new TeacherSubstitutionDao(ctx);
            try
            {
                DateTime schoolDate = Helper.GetDatePickerValue(txtSchoolDate); 
                List<TeacherSubstitution> lstTeacherSubstitution = BusinessLayer.GetTeacherSubstitutionList(string.Format("TeacherAbsenceID = {0} AND SchoolDate = '{1}' AND IsDeleted = 0", AppSession.TeacherAbsenceID, schoolDate.ToString("yyyyMMdd")), ctx);
                if (hdnLstTeacherID.Value != "")
                {
                    string[] lstClassScheduleID = hdnLstClassScheduleID.Value.Split(',');
                    string[] lstTeacherID = hdnLstTeacherID.Value.Split(',');
                    string[] lstTeacherSubstitutionID = hdnLstTeacherSubstitutionID.Value.Split(',');

                    for (int ct = 0; ct < lstClassScheduleID.Length; ++ct)
                    {
                        TeacherSubstitution entityDt = lstTeacherSubstitution.FirstOrDefault(p => p.TeacherSubstitutionID == Convert.ToInt32(lstTeacherSubstitutionID[ct]));
                        if (entityDt == null)
                        {
                            entityDt = new TeacherSubstitution();
                            entityDt.SchoolDate = schoolDate;
                            entityDt.TeacherAbsenceID = AppSession.TeacherAbsenceID;
                            entityDt.TeacherID = Convert.ToInt32(lstTeacherID[ct]);
                            entityDt.ClassScheduleID = Convert.ToInt16(lstClassScheduleID[ct]);
                            entityDt.CreatedBy = AppSession.UserLogin.UserID;
                            entityDtDao.Insert(entityDt);
                        }
                        else
                        {
                            entityDt.TeacherID = Convert.ToInt32(lstTeacherID[ct]);
                            entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityDtDao.Update(entityDt);

                            lstTeacherSubstitution.Remove(entityDt);
                        }
                    }
                }
                foreach (TeacherSubstitution entity in lstTeacherSubstitution)
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