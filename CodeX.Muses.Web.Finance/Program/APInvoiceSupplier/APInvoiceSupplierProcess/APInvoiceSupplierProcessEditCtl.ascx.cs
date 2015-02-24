using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using DevExpress.Web.ASPxCallbackPanel;
using System.Web.UI.HtmlControls;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Muses.Web.Finance.Program
{
    public partial class APInvoiceSupplierProcessEditCtl : BaseEntryPopupCtl
    {
        protected string GetVATPercentage()
        {
            return hdnVATPercentage.Value;
        }

        public override void InitializeDataControl(string param)
        {
            IsAdd = false;
            hdnVATPercentage.Value = BusinessLayer.GetSettingParameter(Constant.SettingParameter.VAT_PERCENTAGE).ParameterValue;

            List<StandardCode> listStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsDeleted = 0", Constant.StandardCode.CURRENCY_CODE, Constant.StandardCode.CHARGES_TYPE));
            List<Term> listTerm = BusinessLayer.GetTermList(string.Format("IsDeleted = 0"));
            Methods.SetComboBoxField<StandardCode>(cboChargesType, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.CHARGES_TYPE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboCurrency, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.CURRENCY_CODE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<Term>(cboTerm, listTerm, "TermName", "TermID");
            cboChargesType.SelectedIndex = 0;
            cboCurrency.SelectedIndex = 0;

            string[] temp = param.Split('|');
            hdnID.Value = temp[0];
            hdnPurchaseReceiveID.Value = temp[1];

            PurchaseReceiveHd entity = BusinessLayer.GetPurchaseReceiveHd(Convert.ToInt32(hdnPurchaseReceiveID.Value));
            txtPurchaseReceiveNo.Text = entity.PurchaseReceiveNo;
            txtPurchaseReceiveDate.Text = entity.ReceivedDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtPurchaseReceiveTime.Text = entity.ReceivedTime;
            txtReferenceNo.Text = entity.ReferenceNo;
            txtDateReferrence.Text = entity.ReferenceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtDPReferrenceNo.Text = entity.DownPaymentReferenceNo;
            txtDP.Text = entity.DownPaymentAmount.ToString();
            cboChargesType.Value = entity.GCChargesType.ToString();
            cboTerm.Value = entity.TermID.ToString();
            txtNotes.Text = entity.Remarks;
            cboCurrency.Value = entity.GCCurrencyCode.ToString();
            txtKurs.Text = entity.CurrencyRate.ToString();
            chkPPN.Checked = entity.IsIncludeVAT;
            txtTotalOrder.Text = entity.TransactionAmount.ToString();
            txtFinalDiscount.Text = entity.FinalDiscountAmount.ToString();

            BindGridView();
        }

        private void BindGridView()
        {
            string filterExpression = string.Format("PurchaseReceiveID = {0} AND GCItemDetailStatus != '{1}'", hdnPurchaseReceiveID.Value, Constant.TransactionStatus.VOID);
            List<vPurchaseReceiveDt> lstEntity = BusinessLayer.GetvPurchaseReceiveDtList(filterExpression);
            lvwView.DataSource = lstEntity;
            lvwView.DataBind();
        }

        protected void lvwView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                vPurchaseReceiveDt entity = (vPurchaseReceiveDt)e.Item.DataItem;
                CheckBox chkIsBonus = (CheckBox)e.Item.FindControl("chkIsBonus");
                TextBox txtUnitPrice = (TextBox)e.Item.FindControl("txtUnitPrice");
                TextBox txtDiscountPercentage1 = (TextBox)e.Item.FindControl("txtDiscountPercentage1");
                TextBox txtDiscountAmount1 = (TextBox)e.Item.FindControl("txtDiscountAmount1");
                TextBox txtDiscountPercentage2 = (TextBox)e.Item.FindControl("txtDiscountPercentage2");
                TextBox txtDiscountAmount2 = (TextBox)e.Item.FindControl("txtDiscountAmount2");
                TextBox txtLineAmount = (TextBox)e.Item.FindControl("txtLineAmount");
                chkIsBonus.Checked = entity.IsBonusItem;
                txtUnitPrice.Text = entity.UnitPrice.ToString();
                txtDiscountPercentage1.Text = entity.DiscountPercentage1.ToString();
                txtDiscountAmount1.Text = entity.DiscountAmount1.ToString();
                txtDiscountPercentage2.Text = entity.DiscountPercentage2.ToString();
                txtDiscountAmount2.Text = entity.DiscountAmount2.ToString();
                txtLineAmount.Text = entity.LineAmount.ToString();
            }
        }

        protected void cbpPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseInvoiceDtDao purchaseInvoiceDtDao = new PurchaseInvoiceDtDao(ctx);
            PurchaseReceiveHdDao purchaseReceiveHdDao = new PurchaseReceiveHdDao(ctx);
            PurchaseReceiveDtDao purchaseReceiveDtDao = new PurchaseReceiveDtDao(ctx);

            string[] lstID = hdnLstID.Value.Split(',');
            string[] lstUnitPrice = hdnLstUnitPrice.Value.Split(',');
            string[] lstDiscountPercentage1 = hdnLstDiscountPercentage1.Value.Split(',');
            string[] lstDiscountPercentage2 = hdnLstDiscountPercentage2.Value.Split(',');
            try
            {
                string tempGCTransactionStatus = "";
                PurchaseReceiveHd purchaseReceiveHd = purchaseReceiveHdDao.Get(Convert.ToInt32(hdnPurchaseReceiveID.Value));
                tempGCTransactionStatus = purchaseReceiveHd.GCTransactionStatus;
                purchaseReceiveHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                purchaseReceiveHdDao.Update(purchaseReceiveHd);

                purchaseReceiveHd = purchaseReceiveHdDao.Get(Convert.ToInt32(hdnPurchaseReceiveID.Value));
                purchaseReceiveHd.ReferenceNo = txtReferenceNo.Text;
                purchaseReceiveHd.ReferenceDate = Helper.GetDatePickerValue(txtDateReferrence);
                purchaseReceiveHd.TermID = Convert.ToInt32(cboTerm.Value);
                purchaseReceiveHd.IsIncludeVAT = chkPPN.Checked;
                if (purchaseReceiveHd.IsIncludeVAT)
                    purchaseReceiveHd.VATPercentage = Convert.ToInt32(hdnVATPercentage.Value);
                else
                    purchaseReceiveHd.VATPercentage = 0;
                purchaseReceiveHd.Remarks = txtNotes.Text;
                purchaseReceiveHd.ChargesAmount = Convert.ToDecimal(txtCharges.Text);
                purchaseReceiveHd.DownPaymentReferenceNo = txtDPReferrenceNo.Text;
                purchaseReceiveHd.GCChargesType = cboChargesType.Value.ToString();
                purchaseReceiveHd.FinalDiscountAmount = Convert.ToDecimal(Request.Form[txtFinalDiscount.UniqueID]);
                purchaseReceiveHd.DownPaymentAmount = Convert.ToDecimal(txtDP.Text);
                purchaseReceiveHd.CurrencyRate = Convert.ToDecimal(txtKurs.Text);

                List<PurchaseReceiveDt> lstPurchaseReceiveDt = BusinessLayer.GetPurchaseReceiveDtList(string.Format("ID IN ({0})", hdnLstID.Value), ctx);
                for (int i = 0; i < lstID.Count(); ++i)
                {
                    PurchaseReceiveDt purchaseReceiveDt = lstPurchaseReceiveDt.FirstOrDefault(p => p.ID == Convert.ToInt32(lstID[i]));
                    purchaseReceiveDt.UnitPrice = Convert.ToDecimal(lstUnitPrice[i]);
                    purchaseReceiveDt.DiscountPercentage1 = Convert.ToDecimal(lstDiscountPercentage1[i]);
                    purchaseReceiveDt.DiscountPercentage2 = Convert.ToDecimal(lstDiscountPercentage2[i]);
                    purchaseReceiveDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    purchaseReceiveDtDao.Update(purchaseReceiveDt);
                }
                purchaseReceiveHdDao.Update(purchaseReceiveHd);

                purchaseReceiveHd = purchaseReceiveHdDao.Get(Convert.ToInt32(hdnPurchaseReceiveID.Value));
                purchaseReceiveHd.TotalNetTransactionAmount = purchaseReceiveHd.TransactionAmount - purchaseReceiveHd.FinalDiscountAmount + purchaseReceiveHd.VATAmount + purchaseReceiveHd.StampAmount + purchaseReceiveHd.ChargesAmount - purchaseReceiveHd.DownPaymentAmount;
                purchaseReceiveHd.GCTransactionStatus = tempGCTransactionStatus;
                purchaseReceiveHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                purchaseReceiveHdDao.Update(purchaseReceiveHd);

                PurchaseInvoiceDt purchaseInvoiceDt = purchaseInvoiceDtDao.Get(Convert.ToInt32(hdnID.Value));
                purchaseInvoiceDt.ReferenceNo = purchaseReceiveHd.ReferenceNo;
                purchaseInvoiceDt.ReferenceDate = purchaseReceiveHd.ReferenceDate;
                purchaseInvoiceDt.ChargesAmount = purchaseReceiveHd.ChargesAmount;
                purchaseInvoiceDt.FinalDiscountAmount = purchaseReceiveHd.FinalDiscountAmount;
                //purchaseInvoiceDt.DiscountAmount = purchaseReceiveHd.DiscountAmount;
                purchaseInvoiceDt.ChargesAmount = purchaseReceiveHd.ChargesAmount;
                purchaseInvoiceDt.DownPaymentAmount = purchaseReceiveHd.DownPaymentAmount;
                purchaseInvoiceDt.StampAmount = purchaseReceiveHd.StampAmount;
                purchaseInvoiceDt.TransactionAmount = purchaseReceiveHd.TransactionAmount;
                purchaseInvoiceDt.VATAmount = Convert.ToDecimal(Request.Form[txtPPN.UniqueID]);
                purchaseInvoiceDt.LineAmount = purchaseInvoiceDt.TransactionAmount - purchaseInvoiceDt.DiscountAmount - purchaseInvoiceDt.FinalDiscountAmount + purchaseInvoiceDt.VATAmount + purchaseInvoiceDt.PPH23Amount + purchaseInvoiceDt.PPH25Amount + purchaseInvoiceDt.StampAmount + purchaseInvoiceDt.ChargesAmount - purchaseInvoiceDt.CreditNoteAmount;
                purchaseInvoiceDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                purchaseInvoiceDtDao.Update(purchaseInvoiceDt);

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
    }
}