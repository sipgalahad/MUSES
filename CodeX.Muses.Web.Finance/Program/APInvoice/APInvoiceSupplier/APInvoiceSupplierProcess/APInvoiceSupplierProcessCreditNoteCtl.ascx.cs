using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using System.Web.UI.HtmlControls;
using CodeX.Data.Core.Dal;
using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxEditors;
using CodeX.Common;
using System.Data;

namespace CodeX.Muses.Web.Finance.Program
{
    public partial class APInvoiceSupplierProcessCreditNoteCtl : BaseEntryPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            IsAdd = true;
            hdnVATPercentage.Value = BusinessLayer.GetSettingParameter(Constant.SettingParameter.VAT_PERCENTAGE).ParameterValue;

            string filterExpression = string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SUPPLIER_CREDIT_NOTE_TYPE);
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(filterExpression);
            Methods.SetComboBoxField<StandardCode>(cboGCCreditNoteType, lstStandardCode, "StandardCodeName", "StandardCodeID");

            hdnID.Value = param;

            vPurchaseInvoiceDt entityInvoiceDt = BusinessLayer.GetvPurchaseInvoiceDtList(string.Format("ID = {0}", hdnID.Value)).FirstOrDefault();
            vPurchaseReturnHd entityReturnHd = BusinessLayer.GetvPurchaseReturnHdList(string.Format("PurchaseReturnID = {0}", entityInvoiceDt.PurchaseReturnID)).FirstOrDefault();
            hdnSupplierID.Value = entityReturnHd.BusinessPartnerID.ToString();
            txtSupplierCode.Text = entityReturnHd.BusinessPartnerCode;
            txtSupplierName.Text = entityReturnHd.SupplierName;
            hdnPurchaseReturnID.Value = entityReturnHd.PurchaseReturnID.ToString();
            txtPurchaseReturnNo.Text = entityReturnHd.PurchaseReturnNo;
            chkPPN.Checked = entityReturnHd.IsIncludeVAT;
            hdnPurchaseReturnAmount.Value = entityReturnHd.TotalNetTransactionAmount.ToString();

            SupplierCreditNote entityCreditNote = BusinessLayer.GetSupplierCreditNoteList(string.Format("PurchaseReturnID = {0}", entityInvoiceDt.PurchaseReturnID)).FirstOrDefault();
            if (entityCreditNote == null)
            {
                IsAdd = true;
                txtCNAmount.Text = entityReturnHd.TotalNetTransactionAmount.ToString();
            }
            else
            {
                IsAdd = false;
                txtCNAmount.Text = entityCreditNote.CNAmount.ToString();
                txtCreditNoteNo.Text = entityCreditNote.CreditNoteNo;
                hdnCreditNoteID.Value = entityCreditNote.CreditNoteID.ToString();
                txtCreditNoteDate.Text = entityCreditNote.CreditNoteDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                txtRemarks.Text = entityCreditNote.Remarks;
                cboGCCreditNoteType.Value = entityCreditNote.GCCreditNoteType.ToString();
            }
        }
        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtCreditNoteDate, new ControlEntrySetting(true, false, true, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(cboGCCreditNoteType, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtCNAmount, new ControlEntrySetting(true, true, true, 0));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        private void ControlToEntity(SupplierCreditNote entity)
        {
            entity.GCCreditNoteType = cboGCCreditNoteType.Value.ToString();
            entity.CNAmount = Convert.ToDecimal(txtCNAmount.Text);
            entity.PurchaseReturnAmount = Convert.ToDecimal(hdnPurchaseReturnAmount.Value);
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            bool result = true;
            SupplierCreditNoteDao entityHdDao = new SupplierCreditNoteDao(ctx);
            PurchaseInvoiceDtDao purchaseInvoiceDtDao = new PurchaseInvoiceDtDao(ctx);
            try
            {
                SupplierCreditNote entity = new SupplierCreditNote();
                entity.CreditNoteDate = Helper.GetDatePickerValue(txtCreditNoteDate);
                entity.BusinessPartnerID = Convert.ToInt32(hdnSupplierID.Value);
                entity.PurchaseReturnID = Convert.ToInt32(hdnPurchaseReturnID.Value);
                entity.IsIncludeVAT = chkPPN.Checked;
                if (entity.IsIncludeVAT)
                    entity.VATPercentage = Convert.ToDecimal(hdnVATPercentage.Value);
                else
                    entity.VATPercentage = 0;
                ControlToEntity(entity);
                entity.CreditNoteNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.SUPPLIER_CREDIT_NOTE, entity.CreditNoteDate);
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entity.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entity);
                int creditNoteID = BusinessLayer.GetSupplierCreditNoteMaxID(ctx);

                PurchaseInvoiceDt purchaseInvoiceDt = purchaseInvoiceDtDao.Get(Convert.ToInt32(hdnID.Value));
                purchaseInvoiceDt.CreditNoteAmount = entity.CNAmount;
                purchaseInvoiceDt.LineAmount = purchaseInvoiceDt.TransactionAmount - purchaseInvoiceDt.DiscountAmount - purchaseInvoiceDt.FinalDiscountAmount + purchaseInvoiceDt.VATAmount + purchaseInvoiceDt.PPH23Amount + purchaseInvoiceDt.PPH25Amount + purchaseInvoiceDt.StampAmount + purchaseInvoiceDt.ChargesAmount - purchaseInvoiceDt.DownPaymentAmount - purchaseInvoiceDt.CreditNoteAmount;
                purchaseInvoiceDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                purchaseInvoiceDtDao.Update(purchaseInvoiceDt);

                entity = entityHdDao.Get(creditNoteID);
                entity.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Update(entity);

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
            IDbContext ctx = DbFactory.Configure(true);
            bool result = true;
            SupplierCreditNoteDao entityHdDao = new SupplierCreditNoteDao(ctx);
            PurchaseInvoiceDtDao purchaseInvoiceDtDao = new PurchaseInvoiceDtDao(ctx);
            try
            {
                SupplierCreditNote entity = entityHdDao.Get(Convert.ToInt32(hdnCreditNoteID.Value));
                entity.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Update(entity);

                entity = entityHdDao.Get(Convert.ToInt32(hdnCreditNoteID.Value));
                ControlToEntity(entity);
                entity.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Update(entity);

                PurchaseInvoiceDt purchaseInvoiceDt = purchaseInvoiceDtDao.Get(Convert.ToInt32(hdnID.Value));
                purchaseInvoiceDt.CreditNoteAmount = entity.CNAmount;
                purchaseInvoiceDt.LineAmount = purchaseInvoiceDt.TransactionAmount - purchaseInvoiceDt.DiscountAmount - purchaseInvoiceDt.FinalDiscountAmount + purchaseInvoiceDt.VATAmount + purchaseInvoiceDt.PPH23Amount + purchaseInvoiceDt.PPH25Amount + purchaseInvoiceDt.StampAmount + purchaseInvoiceDt.ChargesAmount - purchaseInvoiceDt.DownPaymentAmount - purchaseInvoiceDt.CreditNoteAmount;
                purchaseInvoiceDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                purchaseInvoiceDtDao.Update(purchaseInvoiceDt);

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
    }
}