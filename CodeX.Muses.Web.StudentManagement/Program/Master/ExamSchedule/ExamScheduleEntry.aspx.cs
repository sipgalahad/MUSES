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
    public partial class ExamScheduleEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.EXAM_SCHEDULE;
        }
        protected string OnGetRoomFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }

        protected string OnGetTeacherFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }

        protected string OnGetPeriodSectionFilterExpression()
        {
            return string.Format("GCPeriodSectionStatus != '{0}'", Constant.SchoolPeriodStatus.VOID);
        }

        protected override void InitializeDataControl()
        {
            List<SchoolPeriod> lstSchoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SiteID = '{0}' AND GCSchoolPeriodStatus != '{1}'", AppSession.UserLogin.SiteID, Constant.SchoolPeriodStatus.VOID));
            Methods.SetComboBoxField<SchoolPeriod>(cboSchoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
            SchoolPeriod selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);
            if (selectedSchoolPeriod == null)
            {
                cboSchoolPeriod.SelectedIndex = 0;
                selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault();
            }
            else
                cboSchoolPeriod.Value = selectedSchoolPeriod.SchoolPeriodID.ToString();

            List<DailySchedulePackage> lstSchedule = BusinessLayer.GetDailySchedulePackageList(string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<DailySchedulePackage>(cboExamSchedulePackage, lstSchedule, "DailySchedulePackageName", "DailySchedulePackageID");
            cboExamSchedulePackage.Value = selectedSchoolPeriod.ExamSchedulePackageID.ToString();

            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0 AND TagProperty = '1'", Constant.StandardCode.TASK_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboExaminationType, lstSc, "StandardCodeName", "StandardCodeID");
            cboExaminationType.SelectedIndex = 0;

            Helper.SetControlEntrySetting(cboSchoolPeriod, new ControlEntrySetting(true, true, true), "mpFilterGenerate");
            Helper.SetControlEntrySetting(tacPeriodSection, new ControlEntrySetting(true, true, true), "mpFilterGenerate");
            Helper.SetControlEntrySetting(tacClassType, new ControlEntrySetting(true, true, true), "mpFilterGenerate");
            Helper.SetControlEntrySetting(cboExaminationType, new ControlEntrySetting(true, true, true), "mpFilterGenerate");
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
        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string filterExpression = "1 = 0";
            if (tacClassType.Value != null && tacClassType.Value.ToString() != "0")
                filterExpression = string.Format("SchoolPeriodID = {0} AND PeriodClassTypeID = {1} AND IsDeleted = 0", AppSession.SchoolPeriodID, tacClassType.Value);
            List<vPeriodClassTypeSubject> lstEntity = BusinessLayer.GetvPeriodClassTypeSubjectList(filterExpression);
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
                int dayOfWeek = (int)date.DayOfWeek;
                if (lstSchoolDay.Count(p => p.StandardCodeID == string.Format("{0}^00{1}", Constant.StandardCode.SCHOOL_DAY, dayOfWeek)) > 0)
                {
                    entityDt.ExamDate = date;
                    lstEntityDt.Add(entityDt);
                }
                date = date.AddDays(1);
            }
            rptSchedule.DataSource = lstEntityDt;
            rptSchedule.DataBind();
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

        private void ControlToEntity(ExamScheduleHd entityHd)
        {
            entityHd.ExamSchedulePackageID = Convert.ToInt32(cboExamSchedulePackage.Value);
            entityHd.PeriodClassTypeID = Convert.ToInt32(tacClassType.Value);
            entityHd.PeriodSectionID = Convert.ToInt32(tacPeriodSection.Value);
            entityHd.StartDate = Helper.GetDatePickerValue(txtStartDate.Text);
            entityHd.EndDate = Helper.GetDatePickerValue(txtEndDate.Text);
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            if (type == "save")
            {
                bool result = true;
                IDbContext ctx = DbFactory.Configure(true);
                ExamScheduleHdDao entityHdDao = new ExamScheduleHdDao(ctx);
                ExamScheduleDtDao entityDtDao = new ExamScheduleDtDao(ctx);
                try
                {
                    ExamScheduleHd entityHd = new ExamScheduleHd();
                    ControlToEntity(entityHd);
                    entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                    entityHd.CreatedBy = AppSession.UserLogin.UserID;
                    entityHdDao.Insert(entityHd);
                    entityHd.ExamScheduleID = BusinessLayer.GetExamScheduleHdMaxID(ctx);

                    string[] lstSaveValue = hdnSaveValue.Value.Split('|');
                    foreach (string saveValue in lstSaveValue)
                    {
                        string[] temp = saveValue.Split(',');
                        ExamScheduleDt entityDt = new ExamScheduleDt();
                        entityDt.ExamScheduleID = entityHd.ExamScheduleID;
                        entityDt.SubjectID = Convert.ToInt32(temp[0]);
                        entityDt.ExamDate = Helper.GetDatePickerValue(temp[1]);
                        entityDt.HoursIndex = Convert.ToInt16(temp[2]);
                        entityDt.CreatedBy = AppSession.UserLogin.UserID;
                        entityDtDao.Insert(entityDt);
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