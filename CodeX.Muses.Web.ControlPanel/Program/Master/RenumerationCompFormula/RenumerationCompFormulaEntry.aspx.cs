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
    public partial class RenumerationCompFormulaEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.RENUMERATION_COMP_FORMULA;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                RenumerationCompFormulaHd entity = BusinessLayer.GetRenumerationCompFormulaHd(Convert.ToInt32(ID));
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            txtRenumerationCompFormulaCode.Focus();
        }

        protected override void SetControlProperties()
        {
            List<vRenumerationComp> lstRc = BusinessLayer.GetvRenumerationCompList(string.Format("IsDeleted = 0"));
            Methods.SetComboBoxField<vRenumerationComp>(cboRenumerationCompID, lstRc, "RenumerationCompName", "RenumerationCompID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtRenumerationCompFormulaCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRenumerationCompFormulaName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboRenumerationCompID, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
           
        }

        private void EntityToControl(RenumerationCompFormulaHd entity)
        {
            txtRenumerationCompFormulaCode.Text = entity.FormulaCode;
            txtRenumerationCompFormulaName.Text = entity.FormulaName;
            cboRenumerationCompID.Value = entity.RenumerationCompID.ToString();
            txtRemarks.Text = entity.Remarks;
      
        }

        private void ControlToEntity(RenumerationCompFormulaHd entity)
        {
            entity.FormulaCode = txtRenumerationCompFormulaCode.Text;
            entity.FormulaName = txtRenumerationCompFormulaName.Text;
            entity.RenumerationCompID = Convert.ToInt32(cboRenumerationCompID.Value);
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("FormulaCode = '{0}'", txtRenumerationCompFormulaCode.Text);
            List<RenumerationCompFormulaHd> lst = BusinessLayer.GetRenumerationCompFormulaHdList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Renumeration With Code " + txtRenumerationCompFormulaCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("FormulaCode = '{0}' AND FormulaID != {1}", txtRenumerationCompFormulaCode.Text, hdnID.Value);
            List<RenumerationCompFormulaHd> lst = BusinessLayer.GetRenumerationCompFormulaHdList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Renumeration Component With Code " + txtRenumerationCompFormulaCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            RenumerationCompFormulaHdDao entityDao = new RenumerationCompFormulaHdDao(ctx);
            bool result = false;
            try
            {
                RenumerationCompFormulaHd entity = new RenumerationCompFormulaHd();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                retval = entityDao.Insert(entity).ToString();
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
                RenumerationCompFormulaHd entity = BusinessLayer.GetRenumerationCompFormulaHd(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateRenumerationCompFormulaHd(entity);
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