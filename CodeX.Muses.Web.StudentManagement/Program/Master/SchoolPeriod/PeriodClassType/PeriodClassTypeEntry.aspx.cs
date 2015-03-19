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
    public partial class PeriodClassTypeEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SP_SCHOOL_PERIOD_CLASS_TYPE;
        }
        protected override void InitializeDataControl()
        {
            List<ClassType> lstClassType = BusinessLayer.GetClassTypeList(string.Format("SiteID = '{0}' AND GCClassStudyType = '{1}' AND IsDeleted = 0", AppSession.UserLogin.SiteID, Constant.ClassStudyType.REGULAR));
            Methods.SetComboBoxField<ClassType>(cboClassType, lstClassType, "ClassTypeName", "ClassTypeID");
            cboClassType.SelectedIndex = 0;

            List<DailySchedulePackage> lstSchedule = BusinessLayer.GetDailySchedulePackageList(string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<DailySchedulePackage>(cboDailySchedulePackage, lstSchedule, "DailySchedulePackageName", "DailySchedulePackageID");
            cboDailySchedulePackage.SelectedIndex = 0;

            List<StudentFinalMarkFormulaHd> lstFormula = BusinessLayer.GetStudentFinalMarkFormulaHdList(string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID));
            lstFormula.Insert(0, new StudentFinalMarkFormulaHd { StudentFinalMarkFormulaID = 0, StudentFinalMarkFormulaName = "" }); 
            Methods.SetComboBoxField<StudentFinalMarkFormulaHd>(cboTheoryFinalMarkFormula, lstFormula, "StudentFinalMarkFormulaName", "StudentFinalMarkFormulaID");
            cboTheoryFinalMarkFormula.SelectedIndex = 0;

            Methods.SetComboBoxField<StudentFinalMarkFormulaHd>(cboPracticeFinalMarkFormula, lstFormula, "StudentFinalMarkFormulaName", "StudentFinalMarkFormulaID");
            cboPracticeFinalMarkFormula.SelectedIndex = 0;

            BindGridView();

            Helper.SetControlEntrySetting(cboClassType, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboDailySchedulePackage, new ControlEntrySetting(true, true, false), "mpTrx");
            Helper.SetControlEntrySetting(txtNoOfClass, new ControlEntrySetting(true, true, false), "mpTrx");
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = string.Format("SchoolPeriodID = {0} AND GCClassStudyType = '{1}' AND IsDeleted = 0", AppSession.SchoolPeriodID, Constant.ClassStudyType.REGULAR);
            List<vPeriodClassType> lstEntity = BusinessLayer.GetvPeriodClassTypeList(filterExpression);
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
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                if (OnDeleteEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity(PeriodClassType entity)
        {
            entity.ClassTypeID = Convert.ToInt32(cboClassType.Value);
            entity.DailySchedulePackageID = Convert.ToInt32(cboDailySchedulePackage.Value);
            if (cboTheoryFinalMarkFormula.Value == null || cboTheoryFinalMarkFormula.Value.ToString() == "0")
                entity.TheoryFinalMarkFormulaID = null;
            else
                entity.TheoryFinalMarkFormulaID = Convert.ToInt32(cboTheoryFinalMarkFormula.Value);

            if (cboPracticeFinalMarkFormula.Value == null || cboPracticeFinalMarkFormula.Value.ToString() == "0")
                entity.PracticeFinalMarkFormulaID = null;
            else
                entity.PracticeFinalMarkFormulaID = Convert.ToInt32(cboPracticeFinalMarkFormula.Value);
            entity.NoOfClass = Convert.ToInt16(Request.Form[txtNoOfClass.UniqueID]);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                PeriodClassType entity = new PeriodClassType();
                ControlToEntity(entity);
                entity.SchoolPeriodID = AppSession.SchoolPeriodID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertPeriodClassType(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            try
            {
                PeriodClassType entity = BusinessLayer.GetPeriodClassType(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdatePeriodClassType(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                PeriodClassType entity = BusinessLayer.GetPeriodClassType(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedDate = DateTime.Now;
                BusinessLayer.UpdatePeriodClassType(entity);
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