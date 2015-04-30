using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class SchoolPeriodEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SCHOOL_PERIOD;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                SetControlProperties();
                SchoolPeriod entity = BusinessLayer.GetSchoolPeriod(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            txtSchoolPeriodCode.Focus();
        }

        protected override void SetControlProperties()
        {
            List<Curriculum> lstCurriculum = BusinessLayer.GetCurriculumList(string.Format("IsDeleted = 0"));
            Methods.SetComboBoxField<Curriculum>(cboCurriculum, lstCurriculum, "CurriculumName", "CurriculumID");
            cboCurriculum.SelectedIndex = 0;

            List<DailySchedulePackage> lstSchedule = BusinessLayer.GetDailySchedulePackageList(string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<DailySchedulePackage>(cboDailySchedulePackage, lstSchedule, "DailySchedulePackageName", "DailySchedulePackageID");
            cboDailySchedulePackage.SelectedIndex = 0;

            Methods.SetComboBoxField<DailySchedulePackage>(cboExamSchedulePackage, lstSchedule, "DailySchedulePackageName", "DailySchedulePackageID");
            cboExamSchedulePackage.SelectedIndex = 0;

            List<StudentFinalMarkFormulaHd> lstFormula = BusinessLayer.GetStudentFinalMarkFormulaHdList(string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<StudentFinalMarkFormulaHd>(cboTheoryFinalMarkFormula, lstFormula, "StudentFinalMarkFormulaName", "StudentFinalMarkFormulaID");
            cboTheoryFinalMarkFormula.SelectedIndex = 0;

            Methods.SetComboBoxField<StudentFinalMarkFormulaHd>(cboPracticeFinalMarkFormula, lstFormula, "StudentFinalMarkFormulaName", "StudentFinalMarkFormulaID");
            cboPracticeFinalMarkFormula.SelectedIndex = 0;

            List<StudentProgressRuleHd> lstProgress = BusinessLayer.GetStudentProgressRuleHdList(string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<StudentProgressRuleHd>(cboStudentProgressRule, lstProgress, "StudentProgressRuleName", "StudentProgressRuleID");
            cboStudentProgressRule.SelectedIndex = 0;

            Methods.SetComboBoxField<StudentProgressRuleHd>(cboExtracurricularProgressRule, lstProgress, "StudentProgressRuleName", "StudentProgressRuleID");
            cboExtracurricularProgressRule.SelectedIndex = 0;

            List<GradePromotionFormulaHd> lstGradePromotionFormula = BusinessLayer.GetGradePromotionFormulaHdList(string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<GradePromotionFormulaHd>(cboGradePromotionFormula, lstGradePromotionFormula, "GradePromotionFormulaName", "GradePromotionFormulaID");
            cboGradePromotionFormula.SelectedIndex = 0;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtSchoolPeriodCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtSchoolPeriodName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtStartDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtEndDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboCurriculum, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboDailySchedulePackage, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboExamSchedulePackage, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboTheoryFinalMarkFormula, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboPracticeFinalMarkFormula, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboStudentProgressRule, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboExtracurricularProgressRule, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboGradePromotionFormula, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(SchoolPeriod entity)
        {
            txtSchoolPeriodCode.Text = entity.SchoolPeriodCode;
            txtSchoolPeriodName.Text = entity.SchoolPeriodName;
            txtStartDate.Text = entity.StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtEndDate.Text = entity.EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtRemarks.Text = entity.Remarks;
            cboCurriculum.Value = entity.CurriculumID.ToString();
            cboDailySchedulePackage.Value = entity.DailySchedulePackageID.ToString();
            cboExamSchedulePackage.Value = entity.ExamSchedulePackageID.ToString();
            cboTheoryFinalMarkFormula.Value = entity.TheoryFinalMarkFormulaID.ToString();
            cboPracticeFinalMarkFormula.Value = entity.PracticeFinalMarkFormulaID.ToString();
            cboStudentProgressRule.Value = entity.StudentProgressRuleID.ToString();
            cboExtracurricularProgressRule.Value = entity.ExtracurricularProgressRuleID.ToString();
            cboGradePromotionFormula.Value = entity.GradePromotionFormulaID.ToString();
        }

        private void ControlToEntity(SchoolPeriod entity)
        {
            entity.SchoolPeriodCode = txtSchoolPeriodCode.Text;
            entity.SchoolPeriodName = txtSchoolPeriodName.Text;
            entity.StartDate = Helper.GetDatePickerValue(txtStartDate.Text);
            entity.EndDate = Helper.GetDatePickerValue(txtEndDate.Text);
            entity.CurriculumID = Convert.ToInt32(cboCurriculum.Value);
            entity.DailySchedulePackageID = Convert.ToInt32(cboDailySchedulePackage.Value);
            entity.ExamSchedulePackageID = Convert.ToInt32(cboExamSchedulePackage.Value);
            entity.TheoryFinalMarkFormulaID = Convert.ToInt32(cboTheoryFinalMarkFormula.Value);
            entity.PracticeFinalMarkFormulaID = Convert.ToInt32(cboPracticeFinalMarkFormula.Value);
            entity.StudentProgressRuleID = Convert.ToInt32(cboStudentProgressRule.Value);
            entity.ExtracurricularProgressRuleID = Convert.ToInt32(cboExtracurricularProgressRule.Value);
            entity.GradePromotionFormulaID = Convert.ToInt32(cboGradePromotionFormula.Value);
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("SchoolPeriodCode = '{0}'", txtSchoolPeriodCode.Text);
            List<SchoolPeriod> lst = BusinessLayer.GetSchoolPeriodList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " School Period With Code " + txtSchoolPeriodCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("SchoolPeriodCode = '{0}' AND SchoolPeriodID != {1}", txtSchoolPeriodCode.Text, hdnID.Value);
            List<SchoolPeriod> lst = BusinessLayer.GetSchoolPeriodList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " School Period With Code " + txtSchoolPeriodCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            SchoolPeriodDao entityDao = new SchoolPeriodDao(ctx);
            bool result = false;
            try
            {
                SchoolPeriod entity = new SchoolPeriod();
                ControlToEntity(entity);
                entity.SiteID = AppSession.UserLogin.SiteID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetSchoolPeriodMaxID(ctx).ToString();
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            try
            {
                SchoolPeriod entity = BusinessLayer.GetSchoolPeriod(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSchoolPeriod(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }
    }
}