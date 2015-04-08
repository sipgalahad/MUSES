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
    public partial class TeacherPeriodClassTypeSubjectEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.TEACHER_PERIOD_CLASS_TYPE_SUBJECT;
        }

        protected override void InitializeDataControl()
        {
            List<SchoolPeriod> lstSchoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SiteID = '{0}' AND GCSchoolPeriodStatus != '{1}'", AppSession.UserLogin.SiteID, Constant.SchoolPeriodStatus.VOID));
            Methods.SetComboBoxField<SchoolPeriod>(cboSchoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
            SchoolPeriod selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);
            if (selectedSchoolPeriod == null)
                cboSchoolPeriod.SelectedIndex = 0;
            else
                cboSchoolPeriod.Value = selectedSchoolPeriod.SchoolPeriodID.ToString();

            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SUBJECT_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboSubjectType, lstSc, "StandardCodeName", "StandardCodeID");

            List<StudentFinalMarkFormulaHd> lstFormula = BusinessLayer.GetStudentFinalMarkFormulaHdList(string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID));
            lstFormula.Insert(0, new StudentFinalMarkFormulaHd { StudentFinalMarkFormulaID = 0, StudentFinalMarkFormulaName = "" });
            Methods.SetComboBoxField<StudentFinalMarkFormulaHd>(cboTheoryFinalMarkFormula, lstFormula, "StudentFinalMarkFormulaName", "StudentFinalMarkFormulaID");
            cboTheoryFinalMarkFormula.SelectedIndex = 0;

            Methods.SetComboBoxField<StudentFinalMarkFormulaHd>(cboPracticeFinalMarkFormula, lstFormula, "StudentFinalMarkFormulaName", "StudentFinalMarkFormulaID");
            cboPracticeFinalMarkFormula.SelectedIndex = 0;

            BindGridView();

            Helper.SetControlEntrySetting(tacSubject, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboSubjectType, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(tacSubjectMatter, new ControlEntrySetting(true, true, false), "mpTrx");
            Helper.SetControlEntrySetting(txtNoMeetingHoursInWeek, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtPassingGrade, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = "1 = 0";
            filterExpression = string.Format("SchoolPeriodID = {0} AND TeacherID = {1} AND IsDeleted = 0", cboSchoolPeriod.Value, AppSession.UserLogin.EmployeeID);
            List<vPeriodClassTypeSubject> lstEntity = BusinessLayer.GetvPeriodClassTypeSubjectList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity(PeriodClassTypeSubject entity)
        {
            entity.SubjectID = Convert.ToInt32(tacSubject.Value);
            entity.GCSubjectType = cboSubjectType.Value.ToString();
            if (tacSubjectMatter.Value != "" && tacSubjectMatter.Value != "0")
                entity.SubjectMatterID = Convert.ToInt32(tacSubjectMatter.Value);
            else
                entity.SubjectMatterID = null;
            entity.NoMeetingHoursInWeek = Convert.ToInt16(txtNoMeetingHoursInWeek.Text);
            entity.PassingGrade = Convert.ToInt16(txtPassingGrade.Text);
            if (cboTheoryFinalMarkFormula.Value == null || cboTheoryFinalMarkFormula.Value.ToString() == "0")
                entity.TheoryFinalMarkFormulaID = null;
            else
                entity.TheoryFinalMarkFormulaID = Convert.ToInt32(cboTheoryFinalMarkFormula.Value);

            if (cboPracticeFinalMarkFormula.Value == null || cboPracticeFinalMarkFormula.Value.ToString() == "0")
                entity.PracticeFinalMarkFormulaID = null;
            else
                entity.PracticeFinalMarkFormulaID = Convert.ToInt32(cboPracticeFinalMarkFormula.Value);
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            try
            {
                PeriodClassTypeSubject entity = BusinessLayer.GetPeriodClassTypeSubject(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdatePeriodClassTypeSubject(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion
    }
}