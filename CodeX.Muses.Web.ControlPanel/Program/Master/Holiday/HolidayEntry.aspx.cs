using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using DevExpress.Web.ASPxEditors;
using System.Globalization;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class HolidayEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.HOLIDAY;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                SetControlProperties();
                Holiday entity = BusinessLayer.GetHoliday(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            cboHolidayDate.Focus();
        }

        protected override void SetControlProperties()
        {
            cboHolidayMonth.DataSource = Enumerable.Range(1, 12).Select(a => new
            {
                MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(a),
                MonthNumber = a
            });
            cboHolidayMonth.TextField = "MonthName";
            cboHolidayMonth.ValueField = "MonthNumber";
            cboHolidayMonth.EnableCallbackMode = false;
            cboHolidayMonth.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboHolidayMonth.DropDownStyle = DropDownStyle.DropDownList;
            cboHolidayMonth.DataBind();

            cboHolidayYear.DataSource = Enumerable.Range(DateTime.Now.Year, 10).Reverse();
            cboHolidayYear.EnableCallbackMode = false;
            cboHolidayYear.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboHolidayYear.DropDownStyle = DropDownStyle.DropDownList;
            cboHolidayYear.DataBind();
            cboHolidayYear.Items.Insert(0, new ListEditItem { Value = "", Text = "" });

            cboHolidayDate.DataSource = Enumerable.Range(1, 31);
            cboHolidayDate.EnableCallbackMode = false;
            cboHolidayDate.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboHolidayDate.DropDownStyle = DropDownStyle.DropDownList;
            cboHolidayDate.DataBind();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(cboHolidayDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboHolidayMonth, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(chkIsAnnual, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboHolidayYear, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtHolidayName, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(Holiday entity)
        {
            cboHolidayDate.Value = entity.HolidayDate.ToString();
            cboHolidayMonth.Value = entity.HolidayMonth.ToString();
            chkIsAnnual.Checked = entity.IsAnnualHoliday;
            if (!entity.IsAnnualHoliday)
                cboHolidayYear.Value = entity.HolidayYear.ToString();
            txtHolidayName.Text = entity.HolidayName;
        }

        private void ControlToEntity(Holiday entity)
        {
            entity.HolidayDate = Convert.ToInt16(cboHolidayDate.Value);
            entity.HolidayMonth = Convert.ToInt16(cboHolidayMonth.Value);
            entity.IsAnnualHoliday = chkIsAnnual.Checked;
            if (!entity.IsAnnualHoliday)
                entity.HolidayYear = Convert.ToInt16(cboHolidayYear.Value);
            else
                entity.HolidayYear = 0;
            entity.HolidayName = txtHolidayName.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = "";
            if (chkIsAnnual.Checked)
                FilterExpression = string.Format("HolidayDate = {0} AND HolidayMonth = {1} AND IsAnnualHoliday = 1", cboHolidayDate.Value, cboHolidayMonth.Value);
            else
                FilterExpression = string.Format("HolidayDate = {0} AND HolidayMonth = {1} AND IsAnnualHoliday = 0 AND HolidayYear = {2}", cboHolidayDate.Value, cboHolidayMonth.Value, cboHolidayYear.Value);

            List<Holiday> lst = BusinessLayer.GetHolidayList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " This Date is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = "";
            if (chkIsAnnual.Checked)
                FilterExpression = string.Format("HolidayDate = {0} AND HolidayMonth = {1} AND IsAnnualHoliday = 1 AND ID != {2}", cboHolidayDate.Value, cboHolidayMonth.Value, hdnID.Value);
            else
                FilterExpression = string.Format("HolidayDate = {0} AND HolidayMonth = {1} AND IsAnnualHoliday = 0 AND HolidayYear = {2} AND ID != {3}", cboHolidayDate.Value, cboHolidayMonth.Value, cboHolidayYear.Value, hdnID.Value);

            List<Holiday> lst = BusinessLayer.GetHolidayList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " This Date is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            HolidayDao entityDao = new HolidayDao(ctx);
            bool result = false;
            try
            {
                Holiday entity = new Holiday();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetHolidayMaxID(ctx).ToString();
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
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
                Holiday entity = BusinessLayer.GetHoliday(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateHoliday(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
    }
}