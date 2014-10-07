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
    public partial class ClassScheduleEntry : BasePageTrx
    {
        protected string OnGetRoomFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SP_CLASS_SCHEDULE;
        }
        protected override void InitializeDataControl()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SCHOOL_DAILY_SCHEDULE_TYPE));
            rptRemarks.DataSource = lstSc;
            rptRemarks.DataBind();

            List<vSchoolClass> lstClassType = BusinessLayer.GetvSchoolClassList(string.Format("SchoolPeriodID = {0} AND IsDeleted = 0", AppSession.SchoolPeriodID));
            Methods.SetComboBoxField<vSchoolClass>(cboClass, lstClassType, "SchoolClassName", "SchoolClassID");
            cboClass.SelectedIndex = 0;

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
            if (cboClass.Value != null && cboClass.Value.ToString() != "0")
            {
                vSchoolClass schoolClass = BusinessLayer.GetvSchoolClassList(string.Format("SchoolClassID = {0}", cboClass.Value)).FirstOrDefault();
                DailySchedulePackage entity = BusinessLayer.GetDailySchedulePackage(schoolClass.DailySchedulePackageID);
                List<DailyScheduleTypeDt> lstEntityDt = BusinessLayer.GetDailyScheduleTypeDtList(string.Format("DailyScheduleTypeID IN ({0},{1},{2},{3},{4},{5}) AND IsDeleted = 0",
                    entity.DailyScheduleTypeID1 == null ? "0" : entity.DailyScheduleTypeID1.ToString(),
                    entity.DailyScheduleTypeID2 == null ? "0" : entity.DailyScheduleTypeID2.ToString(),
                    entity.DailyScheduleTypeID3 == null ? "0" : entity.DailyScheduleTypeID3.ToString(),
                    entity.DailyScheduleTypeID4 == null ? "0" : entity.DailyScheduleTypeID4.ToString(),
                    entity.DailyScheduleTypeID5 == null ? "0" : entity.DailyScheduleTypeID5.ToString(),
                    entity.DailyScheduleTypeID6 == null ? "0" : entity.DailyScheduleTypeID6.ToString()
                ));
                lstClassSchedule = BusinessLayer.GetvClassScheduleList(string.Format("SchoolClassID = {0} AND IsDeleted = 0", cboClass.Value));
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

        private void BindGridClassSubject()
        {
            string filterExpression = "1 = 0";
            if (cboClass.Value != null && cboClass.Value.ToString() != "0")
            {
                filterExpression = string.Format("SchoolClassID = {0} AND IsDeleted = 0 ORDER BY SubjectName, TeacherName", cboClass.Value);
                lstClassSchedule = BusinessLayer.GetvClassScheduleList(string.Format("SchoolClassID = {0} AND IsDeleted = 0", cboClass.Value));
            }
            List<vClassSubject> lstEntity = BusinessLayer.GetvClassSubjectList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vClassSubject entity = (vClassSubject)e.Row.DataItem;
                HtmlGenericControl tdRemaining = (HtmlGenericControl)e.Row.FindControl("tdRemaining");
                tdRemaining.InnerHtml = (entity.NoMeetingHoursInWeek - lstClassSchedule.Where(p => p.ClassSubjectID == entity.ClassSubjectID).Count()).ToString();
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
                if (entity != null)
                {
                    HtmlTableCell tdValue = (HtmlTableCell)e.Item.FindControl("tdValue");
                    HtmlTableCell tdRoomID = (HtmlTableCell)e.Item.FindControl("tdRoomID");
                    tdValue.InnerHtml = entity.ClassSubjectID.ToString();
                    tdRoomID.InnerHtml = entity.RoomID.ToString();
                    tdHtmlText.InnerHtml = string.Format("<div style='float:right' class='divDetailDelete'></div>{0}<br/>{1}<br/><label class='lblLink lblRoom'>{2}</label>", entity.SubjectName, entity.TeacherName, entity.RoomName);
                }
                else
                {
                    tdHtmlText.InnerHtml = string.Format("{0} - {1}", entityTypeDt.StartTime, entityTypeDt.EndTime);
                }
            }
        }
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
            ClassScheduleDao entityDtDao = new ClassScheduleDao(ctx);
            try
            {
                string[] lstClassSubjectID = hdnLstClassSubjectID.Value.Split(',');
                string[] lstRoomID = hdnLstRoomID.Value.Split(',');
                string[] lstDayNumber = hdnLstDayNumber.Value.Split(',');
                string[] lstHoursIndex = hdnLstHoursIndex.Value.Split(',');
                int SchoolClassID = Convert.ToInt32(cboClass.Value);

                List<ClassSchedule> lstClassSchedule = BusinessLayer.GetClassScheduleList(string.Format("SchoolClassID = {0} AND IsDeleted = 0", SchoolClassID), ctx);
                for (int ct = 0; ct < lstClassSubjectID.Length; ++ct)
                {
                    Int16 dayNumber = Convert.ToInt16(lstDayNumber[ct]);
                    Int16 hoursIndex = Convert.ToInt16(lstHoursIndex[ct]);
                    ClassSchedule entityDt = lstClassSchedule.FirstOrDefault(p => p.DayNumber == dayNumber && p.HoursIndex == hoursIndex);
                    if (entityDt == null)
                    {
                        entityDt = new ClassSchedule();
                        entityDt.SchoolClassID = SchoolClassID;
                        entityDt.HoursIndex = hoursIndex;
                        entityDt.DayNumber = dayNumber;
                        entityDt.RoomID = Convert.ToInt32(lstRoomID[ct]);
                        entityDt.ClassSubjectID = Convert.ToInt16(lstClassSubjectID[ct]);
                        entityDt.CreatedBy = AppSession.UserLogin.UserID;
                        entityDtDao.Insert(entityDt);
                    }
                    else
                    {
                        entityDt.RoomID = Convert.ToInt32(lstRoomID[ct]);
                        entityDt.ClassSubjectID = Convert.ToInt16(lstClassSubjectID[ct]);
                        entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDtDao.Update(entityDt);
                    }
                }

                foreach (ClassSchedule entity in lstClassSchedule)
                {
                    if (!lstDayNumber.Contains(entity.DayNumber.ToString()) && !lstHoursIndex.Contains(entity.HoursIndex.ToString()))
                    {
                        entity.IsDeleted = true;
                        entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDtDao.Update(entity);
                    }
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