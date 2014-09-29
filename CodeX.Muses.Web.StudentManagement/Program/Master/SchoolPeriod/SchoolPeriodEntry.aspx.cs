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
                SchoolPeriod entity = BusinessLayer.GetSchoolPeriod(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
            }
            txtSchoolPeriodCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtSchoolPeriodCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtSchoolPeriodName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtStartDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtEndDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(SchoolPeriod entity)
        {
            txtSchoolPeriodCode.Text = entity.SchoolPeriodCode;
            txtSchoolPeriodName.Text = entity.SchoolPeriodName;
            txtStartDate.Text = entity.StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtEndDate.Text = entity.EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(SchoolPeriod entity)
        {
            entity.SchoolPeriodCode = txtSchoolPeriodCode.Text;
            entity.SchoolPeriodName = txtSchoolPeriodName.Text;
            entity.StartDate = Helper.GetDatePickerValue(txtStartDate.Text);
            entity.EndDate = Helper.GetDatePickerValue(txtEndDate.Text);
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("SchoolPeriodCode = '{0}'", txtSchoolPeriodCode.Text);
            List<SchoolPeriod> lst = BusinessLayer.GetSchoolPeriodList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Login Attribute With Code " + txtSchoolPeriodCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("SchoolPeriodCode = '{0}' AND SchoolPeriodID != {1}", txtSchoolPeriodCode.Text, hdnID.Value);
            List<SchoolPeriod> lst = BusinessLayer.GetSchoolPeriodList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Login Attribute With Code " + txtSchoolPeriodCode.Text + " is already exist!";

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