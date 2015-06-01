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
using DevExpress.Web.ASPxEditors;

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
                BindCboGradePromotionFormula(); 
            }
            else
            {
                SetControlProperties();
                BindCboGradePromotionFormula(); 
                IsAdd = true;
            }
            txtSchoolPeriodCode.Focus();
        }

        protected void cboGradePromotionFormula_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindCboGradePromotionFormula();   
        }

        private void BindCboGradePromotionFormula()
        {
            List<GradePromotionFormulaHd> lstGradePromotionFormula = BusinessLayer.GetGradePromotionFormulaHdList(string.Format("CurriculumID = {0} AND IsDeleted = 0", cboCurriculum.Value));
            Methods.SetComboBoxField<GradePromotionFormulaHd>(cboGradePromotionFormula, lstGradePromotionFormula, "GradePromotionFormulaName", "GradePromotionFormulaID");
            cboGradePromotionFormula.SelectedIndex = 0;            
        }

        protected override void SetControlProperties()
        {
            List<Curriculum> lstCurriculum = BusinessLayer.GetCurriculumList(string.Format("IsDeleted = 0"));
            Methods.SetComboBoxField<Curriculum>(cboCurriculum, lstCurriculum, "CurriculumName", "CurriculumID");
            cboCurriculum.SelectedIndex = 0;

            List<DailySchedulePackage> lstSchedule = BusinessLayer.GetDailySchedulePackageList(string.Format("IsDeleted = 0"));
            Methods.SetComboBoxField<DailySchedulePackage>(cboDailySchedulePackage, lstSchedule, "DailySchedulePackageName", "DailySchedulePackageID");
            cboDailySchedulePackage.SelectedIndex = 0;

            Methods.SetComboBoxField<DailySchedulePackage>(cboExamSchedulePackage, lstSchedule, "DailySchedulePackageName", "DailySchedulePackageID");
            cboExamSchedulePackage.SelectedIndex = 0;
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
            cboGradePromotionFormula.Value = entity.GradePromotionFormulaID.ToString();

            lstFinalMarkFormula = BusinessLayer.GetPeriodFinalMarkFormulaList(string.Format("SchoolPeriodID = {0}", hdnID.Value));
            List<CurriculumMarkType> lstMarkType = BusinessLayer.GetCurriculumMarkTypeList(string.Format("CurriculumID = {0} AND IsAllowTask = 1 AND IsDeleted = 0", entity.CurriculumID));
            rptFinalMarkFormula.DataSource = lstMarkType;
            rptFinalMarkFormula.DataBind();
        }

        List<PeriodFinalMarkFormula> lstFinalMarkFormula = null;
        protected void rptFinalMarkFormula_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                CurriculumMarkType entity = (CurriculumMarkType)e.Item.DataItem;

                ASPxComboBox cboCurriculumFinalMarkFormulaID = (ASPxComboBox)e.Item.FindControl("cboCurriculumFinalMarkFormulaID");
                cboCurriculumFinalMarkFormulaID.ClientInstanceName = string.Format("cboCurriculumFinalMarkFormulaID{0}", e.Item.ItemIndex);

                List<CurriculumFinalMarkFormulaHd> lstFormula = BusinessLayer.GetCurriculumFinalMarkFormulaHdList(string.Format("CurriculumMarkTypeID = {0} AND IsDeleted = 0", entity.CurriculumMarkTypeID));
                lstFormula.Insert(0, new CurriculumFinalMarkFormulaHd { CurriculumFinalMarkFormulaID = 0, CurriculumFinalMarkFormulaName = "" });
                Methods.SetComboBoxField<CurriculumFinalMarkFormulaHd>(cboCurriculumFinalMarkFormulaID, lstFormula, "CurriculumFinalMarkFormulaName", "CurriculumFinalMarkFormulaID");

                PeriodFinalMarkFormula entityFinalMarkFormula = lstFinalMarkFormula.FirstOrDefault(p => p.CurriculumMarkTypeID == entity.CurriculumMarkTypeID);
                if (entityFinalMarkFormula != null)
                    cboCurriculumFinalMarkFormulaID.Value = entityFinalMarkFormula.CurriculumFinalMarkFormulaID.ToString();
            }
        }

        protected void cbpFinalMarkFormula_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            if (cboCurriculum.Value != null)
            {
                if (hdnID.Value != "")
                    lstFinalMarkFormula = BusinessLayer.GetPeriodFinalMarkFormulaList(string.Format("SchoolPeriodID = {0}", hdnID.Value));
                else
                    lstFinalMarkFormula = new List<PeriodFinalMarkFormula>();
                List<CurriculumMarkType> lstMarkType = BusinessLayer.GetCurriculumMarkTypeList(string.Format("CurriculumID = {0} AND IsAllowTask = 1 AND IsDeleted = 0", cboCurriculum.Value));
                rptFinalMarkFormula.DataSource = lstMarkType;
                rptFinalMarkFormula.DataBind();
            }
            else
            {
                List<CurriculumMarkType> lstMarkType = new List<CurriculumMarkType>();
                rptFinalMarkFormula.DataSource = lstMarkType;
                rptFinalMarkFormula.DataBind();
            }
            
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
            PeriodFinalMarkFormulaDao entityFinalMarkDao = new PeriodFinalMarkFormulaDao(ctx);
            bool result = false;
            try
            {
                SchoolPeriod entity = new SchoolPeriod();
                ControlToEntity(entity);
                entity.SiteID = AppSession.UserLogin.SiteID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                entity.SchoolPeriodID = BusinessLayer.GetSchoolPeriodMaxID(ctx);
                
                string[] lstSaveValue = hdnSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    PeriodFinalMarkFormula entityFinalMark = new PeriodFinalMarkFormula();
                    entityFinalMark.SchoolPeriodID = entity.SchoolPeriodID;
                    entityFinalMark.CurriculumMarkTypeID = Convert.ToInt32(temp[0]);
                    if (temp[1] != "")
                        entityFinalMark.CurriculumFinalMarkFormulaID = Convert.ToInt32(temp[1]);
                    else
                        entityFinalMark.CurriculumFinalMarkFormulaID = null;
                    entityFinalMarkDao.Insert(entityFinalMark);
                }

                retval = entity.SchoolPeriodID.ToString();
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
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SchoolPeriodDao entityDao = new SchoolPeriodDao(ctx);
            PeriodFinalMarkFormulaDao entityFinalMarkDao = new PeriodFinalMarkFormulaDao(ctx);
            try
            {
                SchoolPeriod entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

                List<PeriodFinalMarkFormula> lstEntityFinalMark = BusinessLayer.GetPeriodFinalMarkFormulaList(string.Format("SchoolPeriodID = {0}", entity.SchoolPeriodID), ctx);
                string[] lstSaveValue = hdnSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    int CurriculumMarkTypeID = Convert.ToInt32(temp[0]);

                    PeriodFinalMarkFormula entityFinalMark = lstEntityFinalMark.FirstOrDefault(p => p.CurriculumMarkTypeID == CurriculumMarkTypeID);
                    if (entityFinalMark == null)
                    {
                        entityFinalMark = new PeriodFinalMarkFormula();
                        entityFinalMark.SchoolPeriodID = entity.SchoolPeriodID;
                        entityFinalMark.CurriculumMarkTypeID = CurriculumMarkTypeID;
                        if (temp[1] != "")
                            entityFinalMark.CurriculumFinalMarkFormulaID = Convert.ToInt32(temp[1]);
                        else
                            entityFinalMark.CurriculumFinalMarkFormulaID = null;
                        entityFinalMarkDao.Insert(entityFinalMark);
                    }
                    else
                    {
                        if (temp[1] != "")
                            entityFinalMark.CurriculumFinalMarkFormulaID = Convert.ToInt32(temp[1]);
                        else
                            entityFinalMark.CurriculumFinalMarkFormulaID = null;
                        entityFinalMarkDao.Update(entityFinalMark);
                        lstEntityFinalMark.Remove(entityFinalMark);
                    }
                }

                foreach (PeriodFinalMarkFormula entityFinalMark in lstEntityFinalMark)
                {
                    entityFinalMarkDao.Delete(entityFinalMark.SchoolPeriodID, entityFinalMark.CurriculumMarkTypeID);
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
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