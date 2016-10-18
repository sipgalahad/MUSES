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
    public partial class RenumerationEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.RENUMERATION;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                RenumerationHd entity = BusinessLayer.GetRenumeration(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            txtRenumerationCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtRenumerationCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRenumerationName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
           
        }

        private void EntityToControl(RenumerationHd entity)
        {
            txtRenumerationCode.Text = entity.RenumerationCode;
            txtRenumerationName.Text = entity.RenumerationName;
            txtRemarks.Text = entity.Remarks;
      
        }

        private void ControlToEntity(RenumerationHd entity)
        {
            entity.RenumerationCode = txtRenumerationCode.Text;
            entity.RenumerationName = txtRenumerationName.Text;
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("RenumerationCode = '{0}'", txtRenumerationCode.Text);
            List<RenumerationHd> lst = BusinessLayer.GetRenumerationHdList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Renumeration With Code " + txtRenumerationCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("RenumerationCode = '{0}' AND RenumerationID != {1}", txtRenumerationCode.Text, hdnID.Value);
            List<RenumerationHd> lst = BusinessLayer.GetRenumerationHdList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Renumeration With Code " + txtRenumerationCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            RenumerationHdDao entityDao = new RenumerationHdDao(ctx);
            bool result = false;
            try
            {
                RenumerationHd entity = new RenumerationHd();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                retval = entityDao.Insert(entity).ToString();
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
                RenumerationHd entity = BusinessLayer.GetRenumeration(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateRenumeration(entity);
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