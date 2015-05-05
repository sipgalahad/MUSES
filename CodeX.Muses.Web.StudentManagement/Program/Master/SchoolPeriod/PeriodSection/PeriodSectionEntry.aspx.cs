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
    public partial class PeriodSectionEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SP_SCHOOL_PERIOD_SECTION;
        }
        protected override void InitializeDataControl()
        {
            SchoolPeriod entitySchoolPeriod = BusinessLayer.GetSchoolPeriod(AppSession.SchoolPeriodID);
            List<CurriculumSchoolPeriodSection> lstPeriodSection = BusinessLayer.GetCurriculumSchoolPeriodSectionList(string.Format("CurriculumID = {0} AND IsDeleted = 0", entitySchoolPeriod.CurriculumID));
            Methods.SetComboBoxField<CurriculumSchoolPeriodSection>(cboCurriculumSchoolPeriodSection, lstPeriodSection, "CurriculumSchoolPeriodSectionName", "CurriculumSchoolPeriodSectionID");
            cboCurriculumSchoolPeriodSection.SelectedIndex = 0;

            BindGridView();

            Helper.SetControlEntrySetting(txtPeriodSectionCode, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboCurriculumSchoolPeriodSection, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtPeriodSectionName, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtStartDate, new ControlEntrySetting(true, true, false), "mpTrx");
            Helper.SetControlEntrySetting(txtEndDate, new ControlEntrySetting(true, true, false), "mpTrx");
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = string.Format("SchoolPeriodID = {0} AND GCPeriodSectionStatus != '{1}'", AppSession.SchoolPeriodID, Constant.SchoolPeriodStatus.VOID);
            List<vPeriodSection> lstEntity = BusinessLayer.GetvPeriodSectionList(filterExpression);
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

        private void ControlToEntity(PeriodSection entity)
        {
            entity.PeriodSectionCode = txtPeriodSectionCode.Text;
            entity.CurriculumSchoolPeriodSectionID = Convert.ToInt32(cboCurriculumSchoolPeriodSection.Value);
            entity.PeriodSectionName = txtPeriodSectionName.Text;
            entity.StartDate = Helper.GetDatePickerValue(txtStartDate.Text);
            entity.EndDate = Helper.GetDatePickerValue(txtEndDate.Text);
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                PeriodSection entity = new PeriodSection();
                ControlToEntity(entity);
                entity.SchoolPeriodID = AppSession.SchoolPeriodID;
                entity.GCPeriodSectionStatus = Constant.SchoolPeriodStatus.OPEN;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertPeriodSection(entity);
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
                PeriodSection entity = BusinessLayer.GetPeriodSection(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdatePeriodSection(entity);
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
                PeriodSection entity = BusinessLayer.GetPeriodSection(Convert.ToInt32(hdnEntryID.Value));
                entity.GCPeriodSectionStatus = Constant.SchoolPeriodStatus.VOID;
                entity.LastUpdatedDate = DateTime.Now;
                BusinessLayer.UpdatePeriodSection(entity);
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