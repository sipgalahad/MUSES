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
using System.Data;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class PurchaseReceiveConfirmationEditDtCtl : BaseEntryPopupCtl
    {
        protected string GetVATPercentageLabel()
        {
            return hdnVATPercentage.Value;
        }

        public override void InitializeDataControl(string param)
        {
            IsAdd = false;
            hdnVATPercentage.Value = BusinessLayer.GetSettingParameter(Constant.SettingParameter.VAT_PERCENTAGE).ParameterValue;

            List<StandardCode> listStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.CURRENCY_CODE, Constant.StandardCode.CHARGES_TYPE));
            List<Term> listTerm = BusinessLayer.GetTermList(string.Format("IsDeleted = 0"));
            Methods.SetComboBoxField<StandardCode>(cboChargesType, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.CHARGES_TYPE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboCurrency, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.CURRENCY_CODE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<Term>(cboTerm, listTerm, "TermName", "TermID");
            cboChargesType.SelectedIndex = 0;
            cboCurrency.SelectedIndex = 0;

            string[] temp = param.Split('|');
            hdnPurchaseReceiveID.Value = temp[0];
            hdnIsRevision.Value = temp[1];

            PurchaseReceiveHd entity = BusinessLayer.GetPurchaseReceiveHd(Convert.ToInt32(hdnPurchaseReceiveID.Value));
            txtPurchaseReceiveNo.Text = entity.PurchaseReceiveNo;
            txtPurchaseReceiveDate.Text = entity.ReceivedDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtPurchaseReceiveTime.Text = entity.ReceivedTime;
            txtReferenceNo.Text = entity.ReferenceNo;
            if (entity.ReferenceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT) != Constant.ConstantDate.DEFAULT_NULL)
                txtDateReferrence.Text = entity.ReferenceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            else
                txtDateReferrence.Text = "";
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
            txtTotalNetTransactionAmount.Text = entity.TotalNetTransactionAmount.ToString();
            txtFinalDiscountPercentage.Text = entity.FinalDiscountPercentage.ToString();
            txtFinalDiscountAmount.Text = entity.FinalDiscountAmount.ToString();

            vSupplier sup = BusinessLayer.GetvSupplierList(string.Format("BusinessPartnerID = {0}", entity.BusinessPartnerID)).FirstOrDefault();
            hdnIsLineAmountRounded.Value = sup.IsLineAmountRounded ? "1" : "0";
            hdnLineAmountRoundedFormat.Value = sup.LineAmountRoundedFormat.ToString();
            hdnIsTotalAmountRounded.Value = sup.IsTotalAmountRounded ? "1" : "0";
            hdnTotalAmountRoundedFormat.Value = sup.TotalAmountRoundedFormat.ToString();
            txtSupplier.Text = sup.BusinessPartnerName;

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
            PurchaseReceiveHdDao purchaseReceiveHdDao = new PurchaseReceiveHdDao(ctx);
            PurchaseReceiveDtDao purchaseReceiveDtDao = new PurchaseReceiveDtDao(ctx);

            string[] lstSaveValue = hdnSaveValue.Value.Split('|');
            try
            {
                string tempGCTransactionStatus = "";
                PurchaseReceiveHd purchaseReceiveHd = purchaseReceiveHdDao.Get(Convert.ToInt32(hdnPurchaseReceiveID.Value));
                tempGCTransactionStatus = purchaseReceiveHd.GCTransactionStatus;
                purchaseReceiveHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                purchaseReceiveHdDao.Update(purchaseReceiveHd);

                purchaseReceiveHd = purchaseReceiveHdDao.Get(Convert.ToInt32(hdnPurchaseReceiveID.Value));
                purchaseReceiveHd.ReferenceNo = Request.Form[txtReferenceNo.UniqueID];
                purchaseReceiveHd.ReferenceDate = Helper.GetDatePickerValue(Request.Form[txtDateReferrence.UniqueID]);
                purchaseReceiveHd.TermID = Convert.ToInt32(cboTerm.Value);
                purchaseReceiveHd.IsIncludeVAT = chkPPN.Checked;
                if (purchaseReceiveHd.IsIncludeVAT)
                    purchaseReceiveHd.VATPercentage = Convert.ToInt32(hdnVATPercentage.Value);
                else
                    purchaseReceiveHd.VATPercentage = 0;
                purchaseReceiveHd.VATAmount = Convert.ToDecimal(Request.Form[txtPPN.UniqueID]);
                purchaseReceiveHd.Remarks = Request.Form[txtNotes.UniqueID];
                purchaseReceiveHd.ChargesAmount = Convert.ToDecimal(txtCharges.Text);
                purchaseReceiveHd.DownPaymentReferenceNo = txtDPReferrenceNo.Text;
                purchaseReceiveHd.GCChargesType = cboChargesType.Value.ToString();
                purchaseReceiveHd.FinalDiscountPercentage = Convert.ToDecimal(Request.Form[txtFinalDiscountPercentage.UniqueID]);
                purchaseReceiveHd.FinalDiscountAmount = Convert.ToDecimal(Request.Form[txtFinalDiscountAmount.UniqueID]);
                purchaseReceiveHd.DownPaymentAmount = Convert.ToDecimal(txtDP.Text);
                purchaseReceiveHd.CurrencyRate = Convert.ToDecimal(txtKurs.Text);
                purchaseReceiveHd.TransactionAmountBeforeRounded = purchaseReceiveHd.TransactionAmount + purchaseReceiveHd.VATAmount - purchaseReceiveHd.FinalDiscountAmount + purchaseReceiveHd.StampAmount + purchaseReceiveHd.ChargesAmount - purchaseReceiveHd.DownPaymentAmount;
                purchaseReceiveHd.TotalNetTransactionAmount = Convert.ToDecimal(Request.Form[txtTotalNetTransactionAmount.UniqueID]);
                purchaseReceiveHd.RoundedAmount = purchaseReceiveHd.TotalNetTransactionAmount - purchaseReceiveHd.TransactionAmountBeforeRounded;

                List<PurchaseReceiveDt> lstPurchaseReceiveDt = BusinessLayer.GetPurchaseReceiveDtList(string.Format("ID IN ({0})", hdnLstID.Value), ctx);

                ctx.CommandText = "ALTER TABLE PurchaseReceiveDt DISABLE TRIGGER onPurchaseReceieveDtChanged";
                DaoBase.ExecuteNonQuery(ctx);
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();

                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    PurchaseReceiveDt purchaseReceiveDt = lstPurchaseReceiveDt.FirstOrDefault(p => p.ID == Convert.ToInt32(temp[0]));
                    purchaseReceiveDt.UnitPrice = Convert.ToDecimal(temp[1]);
                    purchaseReceiveDt.DiscountPercentage1 = Convert.ToDecimal(temp[2]);
                    purchaseReceiveDt.DiscountAmount1 = Convert.ToDecimal(temp[3]);
                    purchaseReceiveDt.DiscountPercentage2 = Convert.ToDecimal(temp[4]);
                    purchaseReceiveDt.DiscountAmount2 = Convert.ToDecimal(temp[5]);
                    purchaseReceiveDt.LineAmountBeforeRounded = (purchaseReceiveDt.Quantity * purchaseReceiveDt.UnitPrice) - purchaseReceiveDt.DiscountAmount1 - purchaseReceiveDt.DiscountAmount2;
                    purchaseReceiveDt.LineAmount = Convert.ToDecimal(temp[6]);
                    purchaseReceiveDt.RoundedAmount = purchaseReceiveDt.LineAmount - purchaseReceiveDt.LineAmountBeforeRounded;
                    purchaseReceiveDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    purchaseReceiveDtDao.Update(purchaseReceiveDt);
                }
                ctx.CommandText = "ALTER TABLE PurchaseReceiveDt ENABLE TRIGGER onPurchaseReceieveDtChanged";
                DaoBase.ExecuteNonQuery(ctx);
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();

                purchaseReceiveHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                purchaseReceiveHdDao.Update(purchaseReceiveHd);

                purchaseReceiveHd = purchaseReceiveHdDao.Get(Convert.ToInt32(hdnPurchaseReceiveID.Value));
                purchaseReceiveHd.GCTransactionStatus = tempGCTransactionStatus;
                if (hdnIsRevision.Value == "1")
                    purchaseReceiveHd.RevisionNo++;
                purchaseReceiveHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                purchaseReceiveHdDao.Update(purchaseReceiveHd);

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
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