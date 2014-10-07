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
using System.Data;
using CodeX.Common;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class CreditNoteEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.CREDIT_NOTE;
        }

        protected override void InitializeDataControl()
        {
            hdnVATPercentage.Value = BusinessLayer.GetSettingParameter(Constant.SettingParameter.VAT_PERCENTAGE).ParameterValue;
            SetControlProperties();
        }

        protected override void SetControlProperties()
        {
            string filterExpression = string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SUPPLIER_CREDIT_NOTE_TYPE);
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(filterExpression);
            Methods.SetComboBoxField<StandardCode>(cboGCCreditNoteType, lstStandardCode, "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtCreditNoteNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtCreditNoteDate, new ControlEntrySetting(true, false, true, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(hdnSupplierID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSupplierCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtSupplierName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(hdnPurchaseReturnID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtPurchaseReturnNo, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(cboGCCreditNoteType, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtCNAmount, new ControlEntrySetting(true, true, true, 0));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(chkPPN, new ControlEntrySetting(true, true, false));

            SetControlEntrySetting(lblPurchaseReturn, new ControlEntrySetting(true, false));
            SetControlEntrySetting(lblSupplier, new ControlEntrySetting(true, false));
        }

        #region Filter Expression Search Dialog
        protected string GetSupplierFilterExpression()
        {
            return string.Format("GCBusinessPartnerType = '{0}'", Constant.BusinessObjectType.SUPPLIER);
        }

        protected string GetPurchaseReturnFilterExpression()
        {
            return string.Format("GCTransactionStatus = '{0}' AND GCPurchaseReturnType = '{1}'", Constant.TransactionStatus.APPROVED, Constant.PurchaseReturnType.CREDIT_NOTE);
        }
        #endregion

        #region Load Entity
        protected string GetFilterExpression()
        {
            return "";
        }

        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvSupplierCreditNoteRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vSupplierCreditNote entity = BusinessLayer.GetvSupplierCreditNote(filterExpression, PageIndex, "CreditNoteID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvSupplierCreditNoteRowIndex(filterExpression, keyValue, "CreditNoteID DESC");
            vSupplierCreditNote entity = BusinessLayer.GetvSupplierCreditNote(filterExpression, PageIndex, "CreditNoteID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vSupplierCreditNote entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
            }
            hdnCreditNoteID.Value = entity.CreditNoteID.ToString();
            txtCreditNoteNo.Text = entity.CreditNoteNo;
            txtCreditNoteDate.Text = entity.CreditNoteDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            hdnSupplierID.Value = entity.BusinessPartnerID.ToString();
            txtSupplierCode.Text = entity.BusinessPartnerCode;
            txtSupplierName.Text = entity.BusinessPartnerName;
            hdnPurchaseReturnID.Value = entity.PurchaseReturnID.ToString();
            txtPurchaseReturnNo.Text = entity.PurchaseReturnNo;
            cboGCCreditNoteType.Value = entity.GCCreditNoteType;
            txtCNAmount.Text = entity.CNAmount.ToString();
            chkPPN.Checked = entity.IsIncludeVAT;
            txtRemarks.Text = entity.Remarks;
        }
        #endregion

        #region Save
        private void ControlToEntity(SupplierCreditNote entity)
        {
            entity.CreditNoteDate = Helper.GetDatePickerValue(txtCreditNoteDate);
            entity.BusinessPartnerID = Convert.ToInt32(hdnSupplierID.Value);
            entity.PurchaseReturnID = Convert.ToInt32(hdnPurchaseReturnID.Value);
            entity.GCCreditNoteType = cboGCCreditNoteType.Value.ToString();
            entity.CNAmount = Convert.ToDecimal(txtCNAmount.Text);
            entity.IsIncludeVAT = chkPPN.Checked;
            if (entity.IsIncludeVAT)
                entity.VATPercentage = Convert.ToDecimal(hdnVATPercentage.Value);
            else
                entity.VATPercentage = 0;
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            bool result = true;
            SupplierCreditNoteDao entityHdDao = new SupplierCreditNoteDao(ctx);
            try
            {
                SupplierCreditNote entity = new SupplierCreditNote();
                ControlToEntity(entity);
                entity.CreditNoteNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.SUPPLIER_CREDIT_NOTE, entity.CreditNoteDate);
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entity.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entity);
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

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            try
            {
                SupplierCreditNote entity = BusinessLayer.GetSupplierCreditNote(Convert.ToInt32(hdnCreditNoteID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSupplierCreditNote(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        protected override bool OnApproveRecord(ref string errMessage)
        {
            try
            {
                SupplierCreditNote entity = BusinessLayer.GetSupplierCreditNote(Convert.ToInt32(hdnCreditNoteID.Value));
                ControlToEntity(entity);
                entity.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSupplierCreditNote(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        protected override bool OnVoidRecord(ref string errMessage)
        {
            try
            {
                SupplierCreditNote entity = BusinessLayer.GetSupplierCreditNote(Convert.ToInt32(hdnCreditNoteID.Value));
                ControlToEntity(entity);
                entity.GCTransactionStatus = Constant.TransactionStatus.VOID;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSupplierCreditNote(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion
    }
}