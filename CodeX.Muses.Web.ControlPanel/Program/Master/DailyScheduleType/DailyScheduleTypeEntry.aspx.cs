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

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class DailyScheduleTypeEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.SCHOOL_DAILY_SCHEDULE_TYPE;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                DailyScheduleTypeHd entity = BusinessLayer.GetDailyScheduleTypeHd(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
            }
            txtDailyScheduleTypeCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtDailyScheduleTypeCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtDailyScheduleTypeName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true));
        }

        private void EntityToControl(DailyScheduleTypeHd entity)
        {
            txtDailyScheduleTypeCode.Text = entity.DailyScheduleTypeCode;
            txtDailyScheduleTypeName.Text = entity.DailyScheduleTypeName;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(DailyScheduleTypeHd entity)
        {
            entity.DailyScheduleTypeCode = txtDailyScheduleTypeCode.Text;
            entity.DailyScheduleTypeName = txtDailyScheduleTypeName.Text;
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("DailyScheduleTypeCode = '{0}'", txtDailyScheduleTypeCode.Text);
            List<DailyScheduleTypeHd> lst = BusinessLayer.GetDailyScheduleTypeHdList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Ruangan Dengan Kode " + txtDailyScheduleTypeCode.Text + " Sudah Ada!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("DailyScheduleTypeCode = '{0}' AND DailyScheduleTypeID != {1}", txtDailyScheduleTypeCode.Text, hdnID.Value);
            List<DailyScheduleTypeHd> lst = BusinessLayer.GetDailyScheduleTypeHdList(FilterExpression);

            if (lst.Count > 0)
                errMessage = "Ruangan Dengan Kode " + txtDailyScheduleTypeCode.Text + " Sudah Ada!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            DailyScheduleTypeHdDao entityDao = new DailyScheduleTypeHdDao(ctx);
            bool result = false;
            try
            {
                DailyScheduleTypeHd entity = new DailyScheduleTypeHd();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetDailyScheduleTypeHdMaxID(ctx).ToString();
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
                DailyScheduleTypeHd entity = BusinessLayer.GetDailyScheduleTypeHd(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateDailyScheduleTypeHd(entity);
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