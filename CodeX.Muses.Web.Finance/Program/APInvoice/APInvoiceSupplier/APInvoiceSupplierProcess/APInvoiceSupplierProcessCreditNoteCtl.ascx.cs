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
        List<StandardCode> lstCreditNoteType;
        public override void InitializeDataControl(string param)
        {
            IsAdd = false;
            hdnVATPercentage.Value = BusinessLayer.GetSettingParameter(Constant.SettingParameter.VAT_PERCENTAGE).ParameterValue;
            hdnID.Value = param;

            string filterExpression = string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SUPPLIER_CREDIT_NOTE_TYPE);
            lstCreditNoteType = BusinessLayer.GetStandardCodeList(filterExpression);

            List<vPurchaseInvoiceDtCreditNote> lstEntity = BusinessLayer.GetvPurchaseInvoiceDtCreditNoteList(string.Format("PurchaseInvoiceDtID = {0}", hdnID.Value));
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vPurchaseInvoiceDtCreditNote entity = (vPurchaseInvoiceDtCreditNote)e.Row.DataItem;
                DropDownList ddlCreditNoteType = (DropDownList)e.Row.FindControl("ddlCreditNoteType");
                TextBox txtCreditNoteDate = (TextBox)e.Row.FindControl("txtCreditNoteDate");
                TextBox txtCNAmount = (TextBox)e.Row.FindControl("txtCNAmount");

                Methods.SetComboBoxField<StandardCode>(ddlCreditNoteType, lstCreditNoteType, "StandardCodeName", "StandardCodeID");
                ddlCreditNoteType.SelectedValue = entity.GCCreditNoteType;
                txtCNAmount.Text = entity.CNAmount.ToString();
                txtCreditNoteDate.Text = entity.CreditNoteDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            }
        }

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            bool result = true;
            SupplierCreditNoteDao entityHdDao = new SupplierCreditNoteDao(ctx);
            PurchaseInvoiceDtDao purchaseInvoiceDtDao = new PurchaseInvoiceDtDao(ctx);
            try
            {
                decimal totalCNAmount = 0;
                string[] lstSaveValue = hdnLstSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    Int32 creditNoteID = Convert.ToInt32(temp[0]);
                    DateTime creditNoteDate = Helper.GetDatePickerValue(temp[1]);
                    String GCCreditNoteType = temp[2];
                    Decimal CNAmount = Convert.ToDecimal(temp[3]);

                    SupplierCreditNote entity = entityHdDao.Get(creditNoteID);
                    entity.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityHdDao.Update(entity);

                    entity = entityHdDao.Get(creditNoteID);
                    entity.CreditNoteDate = creditNoteDate;
                    entity.GCCreditNoteType = GCCreditNoteType;
                    entity.CNAmount = CNAmount;
                    entity.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityHdDao.Update(entity);
                    totalCNAmount += CNAmount;
                }

                PurchaseInvoiceDt purchaseInvoiceDt = purchaseInvoiceDtDao.Get(Convert.ToInt32(hdnID.Value));
                purchaseInvoiceDt.CreditNoteAmount = totalCNAmount;
                purchaseInvoiceDt.LineAmount = purchaseInvoiceDt.TransactionAmount - purchaseInvoiceDt.DiscountAmount - purchaseInvoiceDt.FinalDiscountAmount + purchaseInvoiceDt.VATAmount + purchaseInvoiceDt.PPH23Amount + purchaseInvoiceDt.PPH25Amount + purchaseInvoiceDt.StampAmount + purchaseInvoiceDt.ChargesAmount - purchaseInvoiceDt.DownPaymentAmount - purchaseInvoiceDt.CreditNoteAmount;
                purchaseInvoiceDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                purchaseInvoiceDtDao.Update(purchaseInvoiceDt);

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
    }
}