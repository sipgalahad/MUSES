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
using System.Data;
using CodeX.Web.CommonLibs.Program;
using CodeX.Common;

namespace CodeX.Muses.Web.Finance.Program
{
    public partial class APInvoiceSupplierPayment : BasePageTrx
    {
        protected int PageCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Finance.AP_INVOICE_SUPPLIER_PAYMENT;
        }

        protected String IsAdd()
        {
            return hdnIsAdd.Value;
        }
        protected override void InitializeDataControl()
        {
            hdnIsAdd.Value = "1";

            cboPaymentMethod.SelectedIndex = cboCurrency.SelectedIndex = 0;
            Helper.SetControlEntrySetting(txtPaymentDate, new ControlEntrySetting(true, false, false), "mpEntry");
            Helper.SetControlEntrySetting(txtReferenceDate, new ControlEntrySetting(true, false, false), "mpEntry");

            txtPaymentDate.Text = txtReferenceDate.Text = DateTime.Today.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
        }

        public override void OnAddRecord()
        {
            hdnIsAdd.Value = "1";
            IsAdd();
            BindGridView();
            cboPaymentMethod.SelectedIndex = cboCurrency.SelectedIndex = 0;
            trBank.Attributes.Add("style", "display:none");
            trBankRef.Attributes.Add("style", "display:none");
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> listStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}','{2}','{3}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.CURRENCY_CODE, Constant.StandardCode.SUPPLIER_PAYMENT_METHOD, Constant.StandardCode.ITEM_TYPE, Constant.StandardCode.PURCHASE_TYPE));
            List<Bank> listBank = BusinessLayer.GetBankList(string.Format("IsDeleted = 0"));
            Methods.SetComboBoxField<StandardCode>(cboPurchaseType, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.PURCHASE_TYPE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboItemType, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.ITEM_TYPE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboCurrency, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.CURRENCY_CODE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboPaymentMethod, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.SUPPLIER_PAYMENT_METHOD).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<Bank>(cboBank, listBank, "BankName", "BankID");
            cboItemType.SelectedIndex = 0;
            cboPurchaseType.SelectedIndex = 0;
            BindGridView();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnSupplierPaymentID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtKurs, new ControlEntrySetting(true, true, true, "1.00"));
            SetControlEntrySetting(cboCurrency, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(cboPurchaseType, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(cboItemType, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(hdnIsAdd, new ControlEntrySetting(false, false, false, "1"));
            SetControlEntrySetting(cboPaymentMethod, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtPaymentDate, new ControlEntrySetting(true, false, false, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(txtReferenceDate, new ControlEntrySetting(true, false, false, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(cboBank, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtBankReferenceNo, new ControlEntrySetting(true, false, true));
        }

        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvSupplierPaymentHdRowCount(filterExpression);
        }

        public string GetFilterExpression()
        {
            string filterExpression = String.Format("BusinessPartnerID = {0}", AppSession.BusinessPartnerID);
            return filterExpression;
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            hdnIsAdd.Value = "0";
            string filterExpression = GetFilterExpression();
            vSupplierPaymentHd entity = BusinessLayer.GetvSupplierPaymentHd(filterExpression, PageIndex, "SupplierPaymentID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            hdnIsAdd.Value = "0";
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvSupplierPaymentHdRowIndex(filterExpression, keyValue, "SupplierPaymentID DESC");
            vSupplierPaymentHd entity = BusinessLayer.GetvSupplierPaymentHd(filterExpression, PageIndex, "SupplierPaymentID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vSupplierPaymentHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
            }
            hdnSupplierPaymentID.Value = entity.SupplierPaymentID.ToString();
            txtPaymentNo.Text = entity.SupplierPaymentNo;
            txtPaymentDate.Text = entity.PaymentDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtRemarks.Text = entity.Remarks;
            cboPaymentMethod.Value = entity.GCSupplierPaymentMethod;
            cboPurchaseType.Value = entity.GCPurchaseType;
            cboItemType.Value = entity.GCItemType;
            txtReferenceNo.Text = entity.ReferenceNo;
            txtReferenceDate.Text = entity.ReferenceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            cboCurrency.Value = entity.GCCurrencyCode;
            txtKurs.Text = entity.CurrencyRate.ToString();

            if (entity.GCSupplierPaymentMethod == Constant.SupplierPaymentMethod.TRANSFER || entity.GCSupplierPaymentMethod == Constant.SupplierPaymentMethod.GIRO || entity.GCSupplierPaymentMethod == Constant.SupplierPaymentMethod.CHEQUE)
            {
                trBank.Attributes.Remove("style");
                trBankRef.Attributes.Remove("style");
                cboBank.Value = entity.BankID.ToString();
                txtBankReferenceNo.Text = entity.BankReferenceNo;
            }
            else
            {
                trBank.Attributes.Add("style", "display:none");
                trBankRef.Attributes.Add("style", "display:none");
            }
            BindGridView();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                BindGridView();
                result = "refresh";
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void BindGridView()
        {
            string filterExpression = "1 = 0";
            if (hdnIsAdd.Value == "0")
            {
                if (hdnSupplierPaymentID.Value != "" && hdnSupplierPaymentID.Value != "0")
                {
                    filterExpression = string.Format("SupplierPaymentID = {0} AND BusinessPartnerID = {1} AND IsVerified = 1", hdnSupplierPaymentID.Value, AppSession.BusinessPartnerID);
                    List<vPurchaseInvoiceHdPayment> lstEntity = BusinessLayer.GetvPurchaseInvoiceHdPaymentList(filterExpression);
                    grdView.DataSource = lstEntity;
                    grdView.DataBind();
                }
            }
            else
            {
                filterExpression = string.Format("BusinessPartnerID = {0} AND GCItemType = '{1}' AND GCPurchaseType = '{2}' AND IsVerified = 1 AND GCTransactionStatus NOT IN ('{3}','{4}','{5}')", AppSession.BusinessPartnerID, cboItemType.Value, cboPurchaseType.Value, Constant.TransactionStatus.CLOSED, Constant.TransactionStatus.OPEN, Constant.TransactionStatus.VOID);
                List<vPurchaseInvoiceHd> lst = BusinessLayer.GetvPurchaseInvoiceHdList(filterExpression);
                lvwView.DataSource = lst;
                lvwView.DataBind();
            }
        }

        protected void lvwView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                vPurchaseInvoiceHd entity = e.Item.DataItem as vPurchaseInvoiceHd;
                TextBox txtBayar = (TextBox)e.Item.FindControl("txtPembayaran");
                txtBayar.Text = Convert.ToDecimal(entity.CustomSisaHutang).ToString();
            }
        }

        #region Get Constant
        protected string GetSupplierPaymentMethodTransfer()
        {
            return Constant.SupplierPaymentMethod.TRANSFER;
        }
        protected string GetSupplierPaymentMethodGiro()
        {
            return Constant.SupplierPaymentMethod.GIRO;
        }
        protected string GetSupplierPaymentMethodCheque()
        {
            return Constant.SupplierPaymentMethod.CHEQUE;
        }
        #endregion

        #region save edit
        public void SaveSupplierPaymentHd(IDbContext ctx, ref int SupplierPaymentID)
        {
            SupplierPaymentHdDao entityHdDao = new SupplierPaymentHdDao(ctx);
            if (hdnSupplierPaymentID.Value == "0")
            {
                SupplierPaymentHd entityHd = new SupplierPaymentHd();
                entityHd.PaymentDate = Helper.GetDatePickerValue(txtPaymentDate.Text);
                entityHd.ReferenceDate = Helper.GetDatePickerValue(txtReferenceDate.Text);
                entityHd.GCPurchaseType = cboPurchaseType.Value.ToString();
                entityHd.GCItemType = cboItemType.Value.ToString();

                entityHd.GCCurrencyCode = cboCurrency.Value.ToString();
                entityHd.BusinessPartnerID = AppSession.BusinessPartnerID;
                entityHd.CurrencyRate = Convert.ToDecimal(txtKurs.Text);
                entityHd.Remarks = txtRemarks.Text;

                entityHd.ReferenceNo = txtReferenceNo.Text;
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                entityHd.ReferenceDate = Helper.GetDatePickerValue(txtReferenceDate.Text);
                entityHd.GCSupplierPaymentMethod = cboPaymentMethod.Value.ToString();
                if (entityHd.GCSupplierPaymentMethod == Constant.SupplierPaymentMethod.TRANSFER ||
                    entityHd.GCSupplierPaymentMethod == Constant.SupplierPaymentMethod.GIRO ||
                    entityHd.GCSupplierPaymentMethod == Constant.SupplierPaymentMethod.CHEQUE)
                {
                    entityHd.BankID = Convert.ToInt32(cboBank.Value.ToString());
                    entityHd.BankReferenceNo = txtBankReferenceNo.Text;
                }
                entityHd.SupplierPaymentNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.SUPPLIER_PAYMENT_VERIFICATION, entityHd.PaymentDate, ctx);

                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);
                SupplierPaymentID = BusinessLayer.GetSupplierPaymentHdMaxID(ctx);
            }
            else
            {
                SupplierPaymentID = Convert.ToInt32(hdnSupplierPaymentID.Value);
            }
        }


        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SupplierPaymentDtDao entityDtDao = new SupplierPaymentDtDao(ctx);
            PurchaseInvoiceHdDao entityInvoiceHdDao = new PurchaseInvoiceHdDao(ctx);
            PurchaseInvoiceDtDao entityInvoiceDtDao = new PurchaseInvoiceDtDao(ctx);
            PurchaseInvoiceDtPaymentDao entityInvoiceDtPaymentDao = new PurchaseInvoiceDtPaymentDao(ctx);
            PurchaseInvoiceHdPaymentDao entityInvoiceHdPaymentDao = new PurchaseInvoiceHdPaymentDao(ctx);
            try
            {
                int SupplierPaymentID = 0;
                SaveSupplierPaymentHd(ctx, ref SupplierPaymentID);

                string[] lstSelectedPurchaseInvoiceID = hdnSelectedMember.Value.Split(',');
                string[] lstSelectedPayment = hdnSelectedPayment.Value.Split(',');

                List<PurchaseInvoiceHd> lstPurchaseInvoiceHd = BusinessLayer.GetPurchaseInvoiceHdList(string.Format("PurchaseInvoiceID IN ({0})", hdnSelectedMember.Value));
                List<PurchaseInvoiceDt> lstPurchaseInvoiceDt = BusinessLayer.GetPurchaseInvoiceDtList(string.Format("PurchaseInvoiceID IN ({0}) AND LineAmount > PaymentAmount AND IsDeleted = 0", hdnSelectedMember.Value));
                for (int i = 0; i < lstSelectedPurchaseInvoiceID.Length; ++i)
                {
                    SupplierPaymentDt entityDt = new SupplierPaymentDt();
                    entityDt.SupplierPaymentID = SupplierPaymentID;
                    entityDt.PurchaseInvoiceID = Convert.ToInt32(lstSelectedPurchaseInvoiceID[i]);
                    entityDt.PaymentAmount = Convert.ToDecimal(lstSelectedPayment[i]);
                    entityDt.CreatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Insert(entityDt);
                    PurchaseInvoiceHd entityInvoice = lstPurchaseInvoiceHd.FirstOrDefault(p => p.PurchaseInvoiceID == entityDt.PurchaseInvoiceID);
                    entityInvoice.NumberOfPayment += 1;
                    entityInvoice.PaymentAmount += entityDt.PaymentAmount;
                    entityInvoice.LastUpdatedBy = AppSession.UserLogin.UserID;
                    if (entityInvoice.TotalNetTransactionAmount == entityInvoice.PaymentAmount)
                        entityInvoice.GCTransactionStatus = Constant.TransactionStatus.CLOSED;
                    entityInvoiceHdDao.Update(entityInvoice);

                    decimal paymentAmount = entityDt.PaymentAmount;
                    List<PurchaseInvoiceDt> lstPurchaseInvoiceDt1 = lstPurchaseInvoiceDt.Where(p => p.PurchaseInvoiceID == entityDt.PurchaseInvoiceID).ToList();
                    foreach (PurchaseInvoiceDt purchaseInvoiceDt in lstPurchaseInvoiceDt1)
                    {
                        if (paymentAmount > 0)
                        {
                            decimal tempPaymentAmount = paymentAmount;
                            decimal remainingAmount = purchaseInvoiceDt.LineAmount - purchaseInvoiceDt.PaymentAmount;
                            if (tempPaymentAmount > remainingAmount)
                                tempPaymentAmount = remainingAmount;
                            purchaseInvoiceDt.PaymentAmount += tempPaymentAmount;
                            entityInvoiceDtDao.Update(purchaseInvoiceDt);

                            PurchaseInvoiceDtPayment entityInvoiceDtPayment = new PurchaseInvoiceDtPayment();
                            entityInvoiceDtPayment.PurchaseInvoiceDtID = purchaseInvoiceDt.ID;
                            entityInvoiceDtPayment.SupplierPaymentID = SupplierPaymentID;
                            entityInvoiceDtPayment.PaymentAmount = tempPaymentAmount;
                            entityInvoiceDtPayment.PaymentDate = Helper.GetDatePickerValue(txtPaymentDate.Text);
                            entityInvoiceDtPaymentDao.Insert(entityInvoiceDtPayment);

                            paymentAmount -= tempPaymentAmount;
                        }
                    }

                    if (paymentAmount > 0)
                    {
                        decimal tempPaymentAmount = paymentAmount;
                        decimal remainingAmount = entityInvoice.TotalNetTransactionAmount - entityInvoice.TotalTransactionAmount - entityInvoice.PaymentAmount;
                        if (tempPaymentAmount > remainingAmount)
                            tempPaymentAmount = remainingAmount;
                        PurchaseInvoiceHdPayment entityInvoiceHdPayment = new PurchaseInvoiceHdPayment();
                        entityInvoiceHdPayment.PurchaseInvoiceID = entityInvoice.PurchaseInvoiceID;
                        entityInvoiceHdPayment.SupplierPaymentID = SupplierPaymentID;
                        entityInvoiceHdPayment.PaymentAmount = tempPaymentAmount;
                        entityInvoiceHdPayment.PaymentDate = Helper.GetDatePickerValue(txtPaymentDate.Text);
                        entityInvoiceHdPaymentDao.Insert(entityInvoiceHdPayment);
                    }
                    else if (entityInvoice.TotalNetTransactionAmount < entityInvoice.TotalTransactionAmount)
                    {
                        if (entityInvoice.PaymentAmount == entityInvoice.TotalNetTransactionAmount)
                        {
                            PurchaseInvoiceHdPayment entityInvoiceHdPayment = new PurchaseInvoiceHdPayment();
                            entityInvoiceHdPayment.PurchaseInvoiceID = entityInvoice.PurchaseInvoiceID;
                            entityInvoiceHdPayment.SupplierPaymentID = SupplierPaymentID;
                            entityInvoiceHdPayment.PaymentAmount = entityInvoice.TotalNetTransactionAmount - entityInvoice.TotalTransactionAmount;
                            entityInvoiceHdPayment.PaymentDate = Helper.GetDatePickerValue(txtPaymentDate.Text);
                            entityInvoiceHdPaymentDao.Insert(entityInvoiceHdPayment);
                        }
                    }
                }

                retval = SupplierPaymentID.ToString();
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
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
                SupplierPaymentHd entity = BusinessLayer.GetSupplierPaymentHd(Convert.ToInt32(hdnSupplierPaymentID.Value));
                entity.PaymentDate = Helper.GetDatePickerValue(txtPaymentDate.Text);
                entity.ReferenceDate = Helper.GetDatePickerValue(txtReferenceDate.Text);

                entity.GCCurrencyCode = cboCurrency.Value.ToString();
                entity.BusinessPartnerID = AppSession.BusinessPartnerID;
                entity.CurrencyRate = Convert.ToDecimal(txtKurs.Text);
                entity.Remarks = txtRemarks.Text;

                entity.ReferenceNo = txtReferenceNo.Text;
                entity.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                entity.ReferenceDate = Helper.GetDatePickerValue(txtReferenceDate.Text);
                if (cboPaymentMethod.Value.ToString() == Constant.SupplierPaymentMethod.TRANSFER ||
                    cboPaymentMethod.Value.ToString() == Constant.SupplierPaymentMethod.GIRO ||
                    cboPaymentMethod.Value.ToString() == Constant.SupplierPaymentMethod.CHEQUE)
                {
                    entity.BankID = Convert.ToInt32(cboBank.Value.ToString());
                    if (txtBankReferenceNo.Text != "")
                        entity.BankReferenceNo = txtReferenceNo.Text;
                }
                entity.GCSupplierPaymentMethod = cboPaymentMethod.Value.ToString();
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;

                BusinessLayer.UpdateSupplierPaymentHd(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion

        #region Void Entity
        protected override bool OnVoidRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseInvoiceHdDao pinvoiceDao = new PurchaseInvoiceHdDao(ctx);
            SupplierPaymentHdDao supplierPaymentHdDao = new SupplierPaymentHdDao(ctx);
            try
            {
                string filterExpression = string.Format("SupplierPaymentID = {0} AND IsVerified = 1", hdnSupplierPaymentID.Value);
                List<vPurchaseInvoiceHdPayment> lstEntity = BusinessLayer.GetvPurchaseInvoiceHdPaymentList(filterExpression, ctx);
                string lstPurchaseInvoiceID = string.Join(",", lstEntity.Select(p => p.PurchaseInvoiceID).ToList());

                List<PurchaseInvoiceHd> lstPurchaseInvoiceHd = BusinessLayer.GetPurchaseInvoiceHdList(string.Format("PurchaseInvoiceID IN ({0})", lstPurchaseInvoiceID), ctx);
                foreach (vPurchaseInvoiceHdPayment purchaseInvoiceHdPayment in lstEntity)
                {
                    PurchaseInvoiceHd pInvoice = lstPurchaseInvoiceHd.FirstOrDefault(p => p.PurchaseInvoiceID == purchaseInvoiceHdPayment.PurchaseInvoiceID);
                    pInvoice.NumberOfPayment -= 1;
                    pInvoice.PaymentAmount -= purchaseInvoiceHdPayment.PaymentAmount;
                    pInvoice.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                    pInvoice.LastUpdatedBy = AppSession.UserLogin.UserID;
                    pinvoiceDao.Update(pInvoice);
                }

                SupplierPaymentHd entity = supplierPaymentHdDao.Get(Convert.ToInt32(hdnSupplierPaymentID.Value));
                entity.GCTransactionStatus = Constant.TransactionStatus.VOID;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                supplierPaymentHdDao.Update(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                ctx.RollBackTransaction();
                result = false;
            }
            finally
            {
                ctx.Close();
            }

            return result;
        }
        #endregion
    }
}