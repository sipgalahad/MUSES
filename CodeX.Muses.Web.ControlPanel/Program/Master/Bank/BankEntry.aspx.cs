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
    public partial class BankEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.BANK;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                Bank entity = BusinessLayer.GetBank(Convert.ToInt32(ID));
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            txtBankCode.Focus();
        }

        protected override void SetControlProperties()
        {
            List<Site> lst = BusinessLayer.GetSiteList("");
            Methods.SetComboBoxField<Site>(cboSite, lst, "SiteName", "SiteID");

            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.BANK_EXPORT_DATA_TYPE, Constant.StandardCode.BANK_TRANSACTION_TYPE));
            lstSc.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });
            Methods.SetComboBoxField<StandardCode>(cboBankExportDataType, lstSc.Where(p => p.ParentID == Constant.StandardCode.BANK_EXPORT_DATA_TYPE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboBankTransactionType, lstSc.Where(p => p.ParentID == Constant.StandardCode.BANK_TRANSACTION_TYPE || p.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtBankCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtBankName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtBankAccountNo, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtBankAccountName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboSite, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboBankTransactionType, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboBankExportDataType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtAdministrationAmount, new ControlEntrySetting(true, true, true, "0"));
        }

        private void EntityToControl(Bank entity)
        {
            txtBankCode.Text = entity.BankCode;
            txtBankName.Text = entity.BankName;
            txtBankAccountNo.Text = entity.BankAccountNo;
            txtBankAccountName.Text = entity.BankAccountName;
            cboSite.Value = entity.SiteID;
            cboBankTransactionType.Value = entity.GCBankTransactionType;
            cboBankExportDataType.Value = entity.GCBankExportDataType;
            txtAdministrationAmount.Text = entity.AdministrationAmount.ToString();
        }

        private void ControlToEntity(Bank entity)
        {
            entity.BankCode = txtBankCode.Text;
            entity.BankName = txtBankName.Text;
            entity.BankAccountNo = txtBankAccountNo.Text;
            entity.BankAccountName = txtBankAccountName.Text;
            entity.SiteID = cboSite.Value.ToString();
            if (cboBankTransactionType.Value == null)
                entity.GCBankTransactionType = null;
            else
                entity.GCBankTransactionType = cboBankTransactionType.Value.ToString();
            entity.GCBankExportDataType = cboBankExportDataType.Value.ToString();
            entity.AdministrationAmount = Convert.ToDecimal(txtAdministrationAmount.Text);

        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("BankCode = '{0}'", txtBankCode.Text);
            List<Bank> lst = BusinessLayer.GetBankList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Bank With Code " + txtBankCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("BankCode = '{0}' AND BankID != {1}", txtBankCode.Text, hdnID.Value);
            List<Bank> lst = BusinessLayer.GetBankList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Bank With Code " + txtBankCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            BankDao entityDao = new BankDao(ctx);
            bool result = false;
            try
            {
                Bank entity = new Bank();
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
                Bank entity = BusinessLayer.GetBank(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateBank(entity);
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