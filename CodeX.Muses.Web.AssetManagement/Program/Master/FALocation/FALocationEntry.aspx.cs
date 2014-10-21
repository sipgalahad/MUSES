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

namespace Codex.Muses.Web.Accounting.Program
{
    public partial class FALocationEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.AssetManagement.FA_LOCATION;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                FALocation entity = BusinessLayer.GetFALocation(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
            }
            txtFALocationCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtFALocationCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtFALocationName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(FALocation entity)
        {
            txtFALocationCode.Text = entity.FALocationCode;
            txtFALocationName.Text = entity.FALocationName;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(FALocation entity)
        {
            entity.FALocationCode = txtFALocationCode.Text;
            entity.FALocationName = txtFALocationName.Text;
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("FALocationCode = '{0}'", txtFALocationCode.Text);
            List<FALocation> lst = BusinessLayer.GetFALocationList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Location With Code " + txtFALocationCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("FALocationCode = '{0}' AND FALocationID != {1}", txtFALocationCode.Text, hdnID.Value);
            List<FALocation> lst = BusinessLayer.GetFALocationList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Location With Code " + txtFALocationCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            FALocationDao entityDao = new FALocationDao(ctx);
            bool result = false;
            try
            {
                FALocation entity = new FALocation();
                ControlToEntity(entity);
                entity.SiteID = AppSession.UserLogin.SiteID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetFALocationMaxID(ctx).ToString();
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
                FALocation entity = BusinessLayer.GetFALocation(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateFALocation(entity);
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