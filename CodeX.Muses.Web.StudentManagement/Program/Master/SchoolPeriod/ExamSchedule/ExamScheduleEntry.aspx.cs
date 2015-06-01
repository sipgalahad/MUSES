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
    public partial class ExamScheduleEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SP_EXAM_SCHEDULE;
        }
        protected string OnGetRoomFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }

        protected string OnGetClassStudyTypeRegular()
        {
            return Constant.ClassStudyType.REGULAR;
        }

        protected string OnGetTeacherFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }

        protected string OnGetPeriodSectionFilterExpression()
        {
            return string.Format("GCPeriodSectionStatus != '{0}'", Constant.SchoolPeriodStatus.VOID);
        }

        protected string OnGetTransactionStatusApproved()
        {
            return Constant.TransactionStatus.APPROVED;
        }

        protected override void InitializeDataControl()
        {
            hdnSchoolPeriodID.Value = AppSession.SchoolPeriodID.ToString();
            SchoolPeriod entitySchoolPeriod = BusinessLayer.GetSchoolPeriod(AppSession.SchoolPeriodID);

            List<PeriodSection> lstPeriodSection = BusinessLayer.GetPeriodSectionList(string.Format("'{0}' BETWEEN StartDate AND EndDate", DateTime.Now.ToString("yyyyMMdd")));
            if (lstPeriodSection.Count > 0)
            {
                PeriodSection periodSection = lstPeriodSection.FirstOrDefault();
                tacPeriodSection.Value = periodSection.PeriodSectionID.ToString();
                tacPeriodSection.Text = periodSection.PeriodSectionName;
            }

            List<DailySchedulePackage> lstSchedule = BusinessLayer.GetDailySchedulePackageList(string.Format("IsDeleted = 0"));
            Methods.SetComboBoxField<DailySchedulePackage>(cboExamSchedulePackage, lstSchedule, "DailySchedulePackageName", "DailySchedulePackageID");
            cboExamSchedulePackage.Value = entitySchoolPeriod.ExamSchedulePackageID.ToString();
            
            List<vCurriculumMarkTypeDt> lstCurriculumMarkTypeDt = BusinessLayer.GetvCurriculumMarkTypeDtList(string.Format("CurriculumID = {0} AND IsExam = 1 AND IsDeleted = 0", entitySchoolPeriod.CurriculumID));
            Methods.SetComboBoxField<vCurriculumMarkTypeDt>(cboCurriculumMarkTypeDt, lstCurriculumMarkTypeDt, "cfCurriculumMarkTypeDtName", "CurriculumMarkTypeDtID");
            //cboCurriculumMarkTypeDt.SelectedIndex = 0;

            Helper.SetControlEntrySetting(tacPeriodSection, new ControlEntrySetting(true, true, true), "mpFilterGenerate");
            Helper.SetControlEntrySetting(tacClassType, new ControlEntrySetting(true, true, true), "mpFilterGenerate");
            Helper.SetControlEntrySetting(cboCurriculumMarkTypeDt, new ControlEntrySetting(true, true, true), "mpFilterGenerate");
            Helper.SetControlEntrySetting(cboExamSchedulePackage, new ControlEntrySetting(true, true, true), "mpFilterGenerate");
            Helper.SetControlEntrySetting(txtStartDate, new ControlEntrySetting(true, true, true), "mpFilterGenerate");
            Helper.SetControlEntrySetting(txtEndDate, new ControlEntrySetting(true, true, true), "mpFilterGenerate");
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        DailySchedulePackage schedulePackage = null;
        List<DailyScheduleTypeDt> lstScheduleType = null;
        List<vExamScheduleDt> lstExamScheduleDt = null;
        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string filterExpression = "1 = 0";
            if (tacClassType.Value != null && tacClassType.Value.ToString() != "0")
                filterExpression = string.Format("PeriodClassTypeID = {0} AND GCClassStudyType = '{1}' AND IsDeleted = 0", tacClassType.Value, Constant.ClassStudyType.REGULAR);
            List<vPeriodClassTypeSubject> lstEntity = BusinessLayer.GetvPeriodClassTypeSubjectList(filterExpression);
            if (hdnID.Value != "")
                lstExamScheduleDt = BusinessLayer.GetvExamScheduleDtList(string.Format("ExamScheduleID = {0} AND IsDeleted = 0", hdnID.Value));
            grdSubject.DataSource = lstEntity;
            grdSubject.DataBind();

            DateTime startDate = Helper.GetDatePickerValue(txtStartDate.Text);
            DateTime endDate = Helper.GetDatePickerValue(txtEndDate.Text);
            DateTime date = startDate;

            schedulePackage = BusinessLayer.GetDailySchedulePackage(Convert.ToInt32(cboExamSchedulePackage.Value));
            lstScheduleType = BusinessLayer.GetDailyScheduleTypeDtList(string.Format("DailyScheduleTypeID IN ({0},{1},{2},{3},{4},{5}) AND IsDeleted = 0",
                schedulePackage.DailyScheduleTypeID1 == null ? "0" : schedulePackage.DailyScheduleTypeID1.ToString(),
                schedulePackage.DailyScheduleTypeID2 == null ? "0" : schedulePackage.DailyScheduleTypeID2.ToString(),
                schedulePackage.DailyScheduleTypeID3 == null ? "0" : schedulePackage.DailyScheduleTypeID3.ToString(),
                schedulePackage.DailyScheduleTypeID4 == null ? "0" : schedulePackage.DailyScheduleTypeID4.ToString(),
                schedulePackage.DailyScheduleTypeID5 == null ? "0" : schedulePackage.DailyScheduleTypeID5.ToString(),
                schedulePackage.DailyScheduleTypeID6 == null ? "0" : schedulePackage.DailyScheduleTypeID6.ToString()
            ));
            List<StandardCode> lstSchoolDay = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SCHOOL_DAY));
            List<ExamScheduleDt> lstEntityDt = new List<ExamScheduleDt>();
            while (date <= endDate)
            {
                ExamScheduleDt entityDt = new ExamScheduleDt();
                short dayOfWeek = (short)date.DayOfWeek;
                if (lstSchoolDay.Count(p => p.StandardCodeID == string.Format("{0}^00{1}", Constant.StandardCode.SCHOOL_DAY, dayOfWeek)) > 0)
                {
                    entityDt.ExamDate = date;
                    entityDt.DayNumber = dayOfWeek;
                    lstEntityDt.Add(entityDt);
                }
                date = date.AddDays(1);
            }
            rptSchedule.DataSource = lstEntityDt;
            rptSchedule.DataBind();
        }

        protected void grdSubject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (lstExamScheduleDt != null)
                {
                    vPeriodClassTypeSubject entity = (vPeriodClassTypeSubject)e.Row.DataItem;
                    vExamScheduleDt entityDt = lstExamScheduleDt.FirstOrDefault(p => p.SubjectID == entity.SubjectID);
                    if (entityDt != null)
                    {
                        HtmlGenericControl divExamDateTime = (HtmlGenericControl)e.Row.FindControl("divExamDateTime");
                        HtmlGenericControl divExamDate = (HtmlGenericControl)e.Row.FindControl("divExamDate");
                        HtmlGenericControl divHoursIndex = (HtmlGenericControl)e.Row.FindControl("divHoursIndex");
                        HtmlGenericControl divDayNumber = (HtmlGenericControl)e.Row.FindControl("divDayNumber");

                        divExamDateTime.InnerHtml = string.Format("{0} ({1} - {2})", entityDt.ExamDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT), entityDt.StartTime, entityDt.EndTime);
                        divExamDate.InnerHtml = entityDt.ExamDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                        divHoursIndex.InnerHtml = entityDt.HoursIndex.ToString();
                        divDayNumber.InnerHtml = entityDt.DayNumber.ToString();
                    }
                }
            }
        }

        protected void rptSchedule_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ExamScheduleDt entity = (ExamScheduleDt)e.Item.DataItem;
                Repeater rptScheduleDt = (Repeater)e.Item.FindControl("rptScheduleDt");
                int dayOfWeek = (int)entity.ExamDate.DayOfWeek;
                switch (dayOfWeek)
                {
                    case 1: rptScheduleDt.DataSource = lstScheduleType.Where(p => p.DailyScheduleTypeID == schedulePackage.DailyScheduleTypeID1).ToList(); break;
                    case 2: rptScheduleDt.DataSource = lstScheduleType.Where(p => p.DailyScheduleTypeID == schedulePackage.DailyScheduleTypeID2).ToList(); break;
                    case 3: rptScheduleDt.DataSource = lstScheduleType.Where(p => p.DailyScheduleTypeID == schedulePackage.DailyScheduleTypeID3).ToList(); break;
                    case 4: rptScheduleDt.DataSource = lstScheduleType.Where(p => p.DailyScheduleTypeID == schedulePackage.DailyScheduleTypeID4).ToList(); break;
                    case 5: rptScheduleDt.DataSource = lstScheduleType.Where(p => p.DailyScheduleTypeID == schedulePackage.DailyScheduleTypeID5).ToList(); break;
                    case 6: rptScheduleDt.DataSource = lstScheduleType.Where(p => p.DailyScheduleTypeID == schedulePackage.DailyScheduleTypeID6).ToList(); break;
                }
                rptScheduleDt.DataBind();
            }
        }

        protected void rptScheduleDt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if (lstExamScheduleDt != null)
                {
                    DailyScheduleTypeDt entity = (DailyScheduleTypeDt)e.Item.DataItem;
                    ExamScheduleDt entityHd = ((RepeaterItem)e.Item.Parent.Parent).DataItem as ExamScheduleDt;
                    vExamScheduleDt entityDt = lstExamScheduleDt.FirstOrDefault(p => p.ExamDate == entityHd.ExamDate && p.HoursIndex == entity.HoursIndex);
                    if (entityDt != null)
                    {
                        HtmlTableCell tdHtmlText = (HtmlTableCell)e.Item.FindControl("tdHtmlText");
                        tdHtmlText.InnerHtml = string.Format("<div style='float:right' class='divDetailDelete'></div><b>{0}</b>", entityDt.SubjectName);
                    }
                }
            }
        }

        private void ControlToEntity(ExamScheduleHd entityHd)
        {
            entityHd.ExamSchedulePackageID = Convert.ToInt32(cboExamSchedulePackage.Value);
            entityHd.PeriodClassTypeID = Convert.ToInt32(tacClassType.Value);
            entityHd.PeriodSectionID = Convert.ToInt32(tacPeriodSection.Value);
            entityHd.StartDate = Helper.GetDatePickerValue(txtStartDate.Text);
            entityHd.EndDate = Helper.GetDatePickerValue(txtEndDate.Text);
            entityHd.CurriculumMarkTypeDtID = Convert.ToInt32(cboCurriculumMarkTypeDt.Value);
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage, ref string retval)
        {
            if (type == "save")
            {
                bool result = true;
                IDbContext ctx = DbFactory.Configure(true);
                ExamScheduleHdDao entityHdDao = new ExamScheduleHdDao(ctx);
                ExamScheduleDtDao entityDtDao = new ExamScheduleDtDao(ctx);
                try
                {
                    ExamScheduleHd entityHd = null;
                    List<ExamScheduleDt> lstExamScheduleDt = null;
                    if (hdnID.Value == "")
                    {
                        entityHd = new ExamScheduleHd();
                        ControlToEntity(entityHd);
                        entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                        entityHd.CreatedBy = AppSession.UserLogin.UserID;
                        entityHdDao.Insert(entityHd);
                        entityHd.ExamScheduleID = BusinessLayer.GetExamScheduleHdMaxID(ctx);

                        lstExamScheduleDt = new List<ExamScheduleDt>();
                    }
                    else
                    {
                        entityHd = entityHdDao.Get(Convert.ToInt32(hdnID.Value));
                        ControlToEntity(entityHd);
                        entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityHdDao.Update(entityHd);

                        lstExamScheduleDt = BusinessLayer.GetExamScheduleDtList(string.Format("ExamScheduleID = {0} AND IsDeleted = 0", hdnID.Value), ctx);
                    }
                    string[] lstSaveValue = hdnSaveValue.Value.Split('|');
                    foreach (string saveValue in lstSaveValue)
                    {
                        string[] temp = saveValue.Split(',');
                        int subjectID = Convert.ToInt32(temp[0]);
                        ExamScheduleDt entityDt = lstExamScheduleDt.FirstOrDefault(p => p.SubjectID == subjectID);
                        if (entityDt == null)
                        {
                            entityDt = new ExamScheduleDt();
                            entityDt.ExamScheduleID = entityHd.ExamScheduleID;
                            entityDt.SubjectID = subjectID;
                            entityDt.ExamDate = Helper.GetDatePickerValue(temp[1]);
                            entityDt.HoursIndex = Convert.ToInt16(temp[2]);
                            entityDt.DayNumber = Convert.ToInt16(temp[3]);
                            entityDt.CreatedBy = AppSession.UserLogin.UserID;
                            entityDtDao.Insert(entityDt);
                        }
                        else
                        {
                            entityDt.ExamDate = Helper.GetDatePickerValue(temp[1]);
                            entityDt.HoursIndex = Convert.ToInt16(temp[2]);
                            entityDt.DayNumber = Convert.ToInt16(temp[3]);
                            entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityDtDao.Update(entityDt);
                            lstExamScheduleDt.Remove(entityDt);
                        }
                    }

                    foreach (ExamScheduleDt entityDt in lstExamScheduleDt)
                    {
                        entityDt.IsDeleted = true;
                        entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDtDao.Update(entityDt);
                    }

                    retval = entityHd.ExamScheduleID.ToString();
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
            else if (type == "approve")
            {
                try
                {
                    ExamScheduleHd entity = BusinessLayer.GetExamScheduleHd(Convert.ToInt32(hdnID.Value));
                    entity.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.UpdateExamScheduleHd(entity);
                }
                catch (Exception ex)
                {
                    Helper.InsertErrorLog(ex);
                    errMessage = ex.Message;
                    return false;
                }
                return true;
            }
            else if (type == "reopen")
            {
                try
                {
                    ExamScheduleHd entity = BusinessLayer.GetExamScheduleHd(Convert.ToInt32(hdnID.Value));
                    entity.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.UpdateExamScheduleHd(entity);
                }
                catch (Exception ex)
                {
                    Helper.InsertErrorLog(ex);
                    errMessage = ex.Message;
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}