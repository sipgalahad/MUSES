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
    public partial class SchoolDailyScheduleTypeEntry : BasePageEntry
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
                SchoolDailyScheduleTypeHd entity = BusinessLayer.GetSchoolDailyScheduleTypeHd(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
            }
            txtSchoolDailyScheduleTypeCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtSchoolDailyScheduleTypeCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtSchoolDailyScheduleTypeName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true));
        }

        private void EntityToControl(SchoolDailyScheduleTypeHd entity)
        {
            txtSchoolDailyScheduleTypeCode.Text = entity.SchoolDailyScheduleTypeCode;
            txtSchoolDailyScheduleTypeName.Text = entity.SchoolDailyScheduleTypeName;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(SchoolDailyScheduleTypeHd entity)
        {
            entity.SchoolDailyScheduleTypeCode = txtSchoolDailyScheduleTypeCode.Text;
            entity.SchoolDailyScheduleTypeName = txtSchoolDailyScheduleTypeName.Text;
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("SchoolDailyScheduleTypeCode = '{0}'", txtSchoolDailyScheduleTypeCode.Text);
            List<SchoolDailyScheduleTypeHd> lst = BusinessLayer.GetSchoolDailyScheduleTypeHdList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Ruangan Dengan Kode " + txtSchoolDailyScheduleTypeCode.Text + " Sudah Ada!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("SchoolDailyScheduleTypeCode = '{0}' AND SchoolDailyScheduleTypeHdID != {1}", txtSchoolDailyScheduleTypeCode.Text, hdnID.Value);
            List<SchoolDailyScheduleTypeHd> lst = BusinessLayer.GetSchoolDailyScheduleTypeHdList(FilterExpression);

            if (lst.Count > 0)
                errMessage = "Ruangan Dengan Kode " + txtSchoolDailyScheduleTypeCode.Text + " Sudah Ada!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            SchoolDailyScheduleTypeHdDao entityDao = new SchoolDailyScheduleTypeHdDao(ctx);
            bool result = false;
            try
            {
                SchoolDailyScheduleTypeHd entity = new SchoolDailyScheduleTypeHd();
                ControlToEntity(entity);
                entity.SiteID = AppSession.UserLogin.SiteID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetSchoolDailyScheduleTypeHdMaxID(ctx).ToString();
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
                SchoolDailyScheduleTypeHd entity = BusinessLayer.GetSchoolDailyScheduleTypeHd(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSchoolDailyScheduleTypeHd(entity);
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