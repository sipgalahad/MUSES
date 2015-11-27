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
    public partial class APInvoiceSupplierProcessDtCtl : BaseViewPopupCtl
    {
        protected string GetVATPercentageLabel()
        {
            return hdnVATPercentage.Value;
        }

        public override void InitializeDataControl(string param)
        {
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

            vPurchaseReceiveHd entity = BusinessLayer.GetvPurchaseReceiveHdList(string.Format("PurchaseReceiveID = {0}", hdnPurchaseReceiveID.Value)).FirstOrDefault();
            txtPurchaseReceiveNo.Text = entity.PurchaseReceiveNo;
            txtPurchaseReceiveDate.Text = entity.ReceivedDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtPurchaseReceiveTime.Text = entity.ReceivedTime;
            hdnSiteServiceUnitID.Value = entity.SiteServiceUnitID.ToString();
            txtServiceUnitCode.Text = entity.ServiceUnitCode;
            txtServiceUnitName.Text = entity.ServiceUnitName;
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
            txtPPN.Text = entity.VATAmount.ToString();
            txtTransactionAmount.Text = entity.TransactionAmount.ToString();
            txtFinalDiscountPercentage.Text = entity.FinalDiscountPercentage.ToString();
            txtFinalDiscountAmount.Text = entity.FinalDiscountAmount.ToString();

            vPurchaseReturnHd entityReturn = BusinessLayer.GetvPurchaseReturnHdList(String.Format("PurchaseReceiveID = {0} AND GCTransactionStatus != '{1}'", entity.PurchaseReceiveID, Constant.TransactionStatus.VOID)).FirstOrDefault();
            if (entityReturn != null)
            {
                hdnPurchaseReturnID.Value = entityReturn.PurchaseReturnID.ToString();
                txtPurchaseReturnNo.Text = entityReturn.PurchaseReturnNo;
                txtPurchaseReturnDate.Text = entityReturn.ReturnDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                txtReturnTransactionAmount.Text = entityReturn.TransactionAmount.ToString();
                chkReturnPPN.Checked = entityReturn.IsIncludeVAT;
                txtPurchaseReturnType.Text = entityReturn.PurchaseReturnType;

                if (entityReturn.GCPurchaseReturnType == Constant.PurchaseReturnType.CREDIT_NOTE)
                {
                    string filterExpression = string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SUPPLIER_CREDIT_NOTE_TYPE);
                    List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(filterExpression);
                    Methods.SetComboBoxField<StandardCode>(cboCreditNoteType, lstStandardCode, "StandardCodeName", "StandardCodeID");

                    SupplierCreditNote entityCreditNote = BusinessLayer.GetSupplierCreditNoteList(string.Format("PurchaseReturnID = {0}", entityReturn.PurchaseReturnID)).FirstOrDefault();
                    if (entityCreditNote != null)
                    {
                        cboCreditNoteType.Value = entityCreditNote.GCCreditNoteType;
                        txtCNAmount.Text = entityCreditNote.CNAmount.ToString();
                    }
                    else
                    {
                        txtCNAmount.Text = entityReturn.TotalNetTransactionAmount.ToString();
                        cboCreditNoteType.SelectedIndex = 0;
                    }
                }
                else
                {
                    trCreditNoteAmount.Style.Add("display", "none");
                    trCreditNoteType.Style.Add("display", "none");
                }
            }
            else
            {
                hdnPurchaseReturnID.Value = "";
                //trPurchaseReturnNo.Style.Add("display", "none");
                //trPurchaseReturnDate.Style.Add("display", "none");
                //divPurchaseReturnFooter.Style.Add("display", "none");
                trCreditNoteAmount.Style.Add("display", "none");
                trCreditNoteType.Style.Add("display", "none");
            }

            BindGridView();
        }

        private void BindGridView()
        {
            if (hdnPurchaseReturnID.Value != "")
            {
                string filterExpressionReturn = string.Format("PurchaseReturnID = {0} AND GCItemDetailStatus != '{1}'", hdnPurchaseReturnID.Value, Constant.TransactionStatus.VOID);
                lstPurchaseReturnDt = BusinessLayer.GetPurchaseReturnDtList(filterExpressionReturn);
            }
            else
                lstPurchaseReturnDt = new List<PurchaseReturnDt>();
            string filterExpression = string.Format("PurchaseReceiveID = {0} AND GCItemDetailStatus != '{1}'", hdnPurchaseReceiveID.Value, Constant.TransactionStatus.VOID);
            List<vPurchaseReceiveDt> lstEntity = BusinessLayer.GetvPurchaseReceiveDtList(filterExpression);
            lvwView.DataSource = lstEntity;
            lvwView.DataBind();
        }

        List<PurchaseReturnDt> lstPurchaseReturnDt = null;
        protected void lvwView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                vPurchaseReceiveDt entity = (vPurchaseReceiveDt)e.Item.DataItem;
                CheckBox chkIsBonus = (CheckBox)e.Item.FindControl("chkIsBonus");
                chkIsBonus.Checked = entity.IsBonusItem;

                PurchaseReturnDt entityReturn = lstPurchaseReturnDt.FirstOrDefault(p => p.ItemID == entity.ItemID);
                if (entityReturn != null)
                {
                    HtmlTableCell tdReturnQuantity = (HtmlTableCell)e.Item.FindControl("tdReturnQuantity");
                    HtmlTableCell tdReturnLineAmount = (HtmlTableCell)e.Item.FindControl("tdReturnLineAmount");

                    tdReturnQuantity.InnerHtml = entityReturn.Quantity.ToString();
                    tdReturnLineAmount.InnerHtml = entityReturn.LineAmount.ToString("N");
                }

            }
        }

        protected void cbpPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}