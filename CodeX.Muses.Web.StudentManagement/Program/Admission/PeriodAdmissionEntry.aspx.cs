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
    public partial class PeriodAdmissionEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.PERIOD_ADMISSION;
        }

        protected override void InitializeDataControl()
        {
            String[] param = Request.QueryString["id"].Split('|');
            if (param[0] == "edit")
            {
                IsAdd = false;
                String schoolPeriodID = param[1];
                hdnID.Value = schoolPeriodID;
                PeriodAdmission entity = BusinessLayer.GetPeriodAdmission(Convert.ToInt32(schoolPeriodID));

                EntityToControl(entity);
                hdnSchoolPeriodID.Value = entity.SchoolPeriodID.ToString();
            }
            else
            {
                hdnSchoolPeriodID.Value = param[1];
                IsAdd = true;
            }
            txtPeriodAdmissionCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtPeriodAdmissionCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtPeriodAdmissionName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtStartDate, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtEndDate, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRegistrationStartDate, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRegistrationEndDate, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(PeriodAdmission entity)
        {
            txtPeriodAdmissionCode.Text = entity.PeriodAdmissionCode;
            txtPeriodAdmissionName.Text = entity.PeriodAdmissionName;
            txtStartDate.Text = entity.StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtEndDate.Text = entity.EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtRegistrationStartDate.Text = entity.RegistrationStartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtRegistrationEndDate.Text = entity.RegistrationEndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(PeriodAdmission entity)
        {
            entity.PeriodAdmissionCode = txtPeriodAdmissionCode.Text;
            entity.PeriodAdmissionName = txtPeriodAdmissionName.Text;
            entity.StartDate = Helper.GetDatePickerValue(txtStartDate.Text);
            entity.EndDate = Helper.GetDatePickerValue(txtEndDate.Text);
            entity.RegistrationStartDate = Helper.GetDatePickerValue(txtRegistrationStartDate.Text);
            entity.RegistrationEndDate = Helper.GetDatePickerValue(txtRegistrationEndDate.Text);
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("PeriodAdmissionCode = '{0}'", txtPeriodAdmissionCode.Text);
            List<PeriodAdmission> lst = BusinessLayer.GetPeriodAdmissionList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Admission With Code " + txtPeriodAdmissionCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("PeriodAdmissionCode = '{0}' AND PeriodAdmissionID != {1}", txtPeriodAdmissionCode.Text, hdnID.Value);
            List<PeriodAdmission> lst = BusinessLayer.GetPeriodAdmissionList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Admission With Code " + txtPeriodAdmissionCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            PeriodAdmissionDao entityDao = new PeriodAdmissionDao(ctx);
            bool result = false;
            try
            {
                PeriodAdmission entity = new PeriodAdmission();
                ControlToEntity(entity);
                entity.SchoolPeriodID = Convert.ToInt32(hdnSchoolPeriodID.Value);
                entity.GCPeriodAdmissionStatus = Constant.SchoolPeriodStatus.OPEN;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetPeriodAdmissionMaxID(ctx).ToString();
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
                PeriodAdmission entity = BusinessLayer.GetPeriodAdmission(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdatePeriodAdmission(entity);
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