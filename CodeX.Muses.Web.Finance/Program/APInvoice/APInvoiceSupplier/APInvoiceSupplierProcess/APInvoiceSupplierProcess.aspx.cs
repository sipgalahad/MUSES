using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Core.Dal;
using System.Data;
using CodeX.Common;
using CodeX.Web.Finance.MasterPage;

namespace CodeX.Muses.Web.Finance.Program
{
    public partial class APInvoiceSupplierProcess : BasePageTrx
    {
        protected int PageCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Finance.AP_INVOICE_SUPPLIER_PROCESS;
        }

        private MPSupplierPageTrx MasterPage
        {
            get
            {
                return (MPSupplierPageTrx)Master;
            }
        }

        protected override void InitializeDataControl()
        {
            hdnBusinessPartnerID.Value = MasterPage.BusinessPartnerID.ToString();
            SetControlProperties();
            hdnTotalAmount.Value = hdnTotalAmountBeforeDP.Value = "0";

            List<SettingParameter> lstSettingParameter = BusinessLayer.GetSettingParameterList(string.Format("ParameterCode IN ('{0}','{1}','{2}')",
                                                                                               Constant.SettingParameter.IS_DISCOUNT_APPLIED_TO_AVERAGE_PRICE,
                                                                                               Constant.SettingParameter.IS_DISCOUNT_APPLIED_TO_UNIT_PRICE,
                                                                                               Constant.SettingParameter.VAT_PERCENTAGE));

            hdnIsDiscountAppliedToAveragePrice.Value = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.IS_DISCOUNT_APPLIED_TO_AVERAGE_PRICE).ParameterValue;
            hdnIsDiscountAppliedToUnitPrice.Value = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.IS_DISCOUNT_APPLIED_TO_UNIT_PRICE).ParameterValue;
            hdnPPNPctg.Value = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.VAT_PERCENTAGE).ParameterValue;

            BindGridView(1, true, ref PageCount, false);
            Helper.SetControlEntrySetting(cboGLAPOther, new ControlEntrySetting(true, true, false), "mpTrx");
            Helper.SetControlEntrySetting(txtDueDate, new ControlEntrySetting(true, true, false), "mpTrx");
            Helper.SetControlEntrySetting(txtInvoiceDate, new ControlEntrySetting(true, true, false), "mpTrx");
            Helper.SetControlEntrySetting(txtPurchaseInvoiceDate, new ControlEntrySetting(true, true, false), "mpTrx");
            Helper.SetControlEntrySetting(txtSupplierInvoiceDate, new ControlEntrySetting(true, true, false), "mpTrx");
            Helper.SetControlEntrySetting(txtTaxInvoiceDate, new ControlEntrySetting(true, true, false), "mpTrx");
            Helper.SetControlEntrySetting(txtInvoiceNo, new ControlEntrySetting(true, true, true), "mpTrxPopup");

            txtDueDate.Text = DateTime.Today.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtInvoiceDate.Text = DateTime.Today.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtPurchaseInvoiceDate.Text = DateTime.Today.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtSupplierInvoiceDate.Text = DateTime.Today.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtTaxInvoiceDate.Text = DateTime.Today.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> listStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}','{2}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.ITEM_TYPE, Constant.StandardCode.CURRENCY_CODE, Constant.StandardCode.PURCHASE_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboItemType, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.ITEM_TYPE && (p.StandardCodeID == Constant.ItemType.PRODUCT)).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            cboItemType.SelectedIndex = 0;

            Methods.SetComboBoxField<StandardCode>(cboCurrency, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.CURRENCY_CODE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            cboCurrency.SelectedIndex = 0;

            Methods.SetComboBoxField<StandardCode>(cboPurchaseType, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.PURCHASE_TYPE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            cboPurchaseType.SelectedIndex = 0;

            List<GLAPOther> lstGLAPOther = BusinessLayer.GetGLAPOtherList("IsDeleted = 0");
            Methods.SetComboBoxField<GLAPOther>(cboGLAPOther, lstGLAPOther, "APOtherName", "ID");
            cboGLAPOther.SelectedIndex = 0;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnPurchaseInvoiceID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(hdnTotalAmount, new ControlEntrySetting(true, true, true, "0.00"));
            SetControlEntrySetting(hdnTotalAmountBeforeDP, new ControlEntrySetting(true, true, true, "0.00"));
            SetControlEntrySetting(txtKurs, new ControlEntrySetting(true, true, true, "1.00"));
            SetControlEntrySetting(txtStampAmount, new ControlEntrySetting(true, true, false, "0.00"));
            SetControlEntrySetting(txtVAT, new ControlEntrySetting(true, true, false, "0.00"));
            SetControlEntrySetting(txtTransactionAmount, new ControlEntrySetting(true, true, false, "0.00"));
            SetControlEntrySetting(txtPPh23, new ControlEntrySetting(true, true, false, "0.00"));
            SetControlEntrySetting(txtDiscTransAmount, new ControlEntrySetting(false, false, false, "0.00"));
            SetControlEntrySetting(txtDiscountAmount, new ControlEntrySetting(true, true, false, "0.00"));
            SetControlEntrySetting(txtCreditNote, new ControlEntrySetting(true, true, false, "0.00"));
            SetControlEntrySetting(txtDownPayment, new ControlEntrySetting(true, true, false, "0.00"));
            SetControlEntrySetting(txtChargesAmount, new ControlEntrySetting(true, true, false, "0.00"));
            SetControlEntrySetting(txtPPNPI, new ControlEntrySetting(false, false, false, "0.00"));
            SetControlEntrySetting(txtPPHPI, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtStampPI, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtChargesPI, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtGrandTotalPI, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtFinalDIscountPI, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboItemType, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(cboPurchaseType, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(cboCurrency, new ControlEntrySetting(true, false, true));
        }

        public override void OnAddRecord()
        {
            hdnPageCount.Value = "0";
            hdnIsEditable.Value = "1";
            hdnPPHPctg.Value = "0";
            hdnTotalAmountBeforeDP.Value = hdnTotalAmount.Value = hdnStampPI.Value = hdnChargesPI.Value = hdnFinalDiscountPI.Value = "0";
            chkPPN.Checked = false;
        }

        public bool IsAllowEditPurchaseReceive()
        {
            return false;
        }

        protected string IsEditable()
        {
            return hdnIsEditable.Value;
        }

        #region Load Entity
        protected string GetFilterExpression()
        {
            return string.Format("BusinessPartnerID = {0}", MasterPage.BusinessPartnerID);
        }

        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvPurchaseInvoiceHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vPurchaseInvoiceHd entity = BusinessLayer.GetvPurchaseInvoiceHd(filterExpression, PageIndex, "PurchaseInvoiceID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvPurchaseInvoiceHdRowIndex(filterExpression, keyValue, "PurchaseInvoiceID DESC");
            vPurchaseInvoiceHd entity = BusinessLayer.GetvPurchaseInvoiceHd(filterExpression, PageIndex, "PurchaseInvoiceID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vPurchaseInvoiceHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
                hdnIsEditable.Value = "0";
            }
            else
                hdnIsEditable.Value = "1";

            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN && entity.GCTransactionStatus != Constant.TransactionStatus.VOID)
                hdnPrintStatus.Value = "true";
            else
                hdnPrintStatus.Value = "false";

            hdnPurchaseInvoiceID.Value = entity.PurchaseInvoiceID.ToString();
            txtPurchaseInvoiceNo.Text = entity.PurchaseInvoiceNo;
            txtPurchaseInvoiceDate.Text = entity.PurchaseInvoiceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtSupplierInvoiceNo.Text = entity.SupplierInvoiceNo;
            txtSupplierInvoiceDate.Text = entity.SupplierInvoiceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtTaxInvoiceNo.Text = entity.TaxInvoiceNo;
            txtTaxInvoiceDate.Text = entity.TaxInvoiceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtDueDate.Text = entity.DueDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            cboCurrency.Value = entity.GCCurrencyCode;
            cboItemType.Value = entity.GCItemType;
            cboPurchaseType.Value = entity.GCPurchaseType;
            txtKurs.Text = entity.CurrencyRate.ToString();
            txtRemarks.Text = entity.Remarks;

            if (entity.VATPercentage != 0)
                chkPPN.Checked = true;
            else
                chkPPN.Checked = false;

            hdnStampPI.Value = entity.StampAmount.ToString();
            hdnFinalDiscountPI.Value = entity.FinalDiscount.ToString();
            hdnChargesPI.Value = entity.ChargesAmount.ToString();
            hdnPPHPctg.Value = entity.PPHPercentage.ToString();
            hdnTotalAmount.Value = hdnTotalAmountBeforeDP.Value = "0";

            BindGridView(1, true, ref PageCount, false);
            decimal total = entity.TotalTransactionAmount;
            hdnTotalAmountBeforeDP.Value = total.ToString();
            //decimal totalDP = entity.TotalDownPaymentAmount;
            //total -= totalDP;
            hdnTotalAmount.Value = total.ToString();

            hdnTransactionStatus.Value = entity.GCTransactionStatus;
            hdnPageCount.Value = PageCount.ToString();
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, bool isCountTotalAmount)
        {
            string filterExpression = "1 = 0";
            if (hdnPurchaseInvoiceID.Value != "" && hdnPurchaseInvoiceID.Value != "0")
            {
                filterExpression = string.Format("PurchaseInvoiceID = {0} AND IsDeleted = 0", hdnPurchaseInvoiceID.Value);

                if (isCountTotalAmount)
                {
                    PurchaseInvoiceHd entity = BusinessLayer.GetPurchaseInvoiceHd(Convert.ToInt32(hdnPurchaseInvoiceID.Value));
                    decimal total = entity.TotalTransactionAmount;
                    hdnTotalAmountBeforeDP.Value = total.ToString();
                    hdnTotalAmount.Value = total.ToString();
                }
            }
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvPurchaseInvoiceDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vPurchaseInvoiceDt> lstEntity = BusinessLayer.GetvPurchaseInvoiceDtList(filterExpression);
            lvwView.DataSource = lstEntity;
            lvwView.DataBind();
        }
        #endregion

        #region Save Edit Header
        private void ControlToEntity(PurchaseInvoiceHd entityHd)
        {
            entityHd.SupplierInvoiceDate = Helper.GetDatePickerValue(txtSupplierInvoiceDate.Text);
            entityHd.PurchaseInvoiceDate = Helper.GetDatePickerValue(txtPurchaseInvoiceDate.Text);
            entityHd.TaxInvoiceDate = Helper.GetDatePickerValue(txtTaxInvoiceDate.Text);
            entityHd.DueDate = Helper.GetDatePickerValue(txtDueDate.Text);

            entityHd.GCItemType = cboItemType.Value.ToString();
            entityHd.GCPurchaseType = cboPurchaseType.Value.ToString();
            entityHd.GCCurrencyCode = cboCurrency.Value.ToString();
            entityHd.GCChargesType = "X157^001";
            entityHd.ChargesAmount = Convert.ToDecimal(txtChargesPI.Text);
            entityHd.BusinessPartnerID = MasterPage.BusinessPartnerID;
            entityHd.CurrencyRate = Convert.ToDecimal(txtKurs.Text);
            entityHd.Remarks = txtRemarks.Text;
            if (chkPPN.Checked)
                entityHd.VATPercentage = Convert.ToDecimal(hdnPPNPctg.Value);
            else
                entityHd.VATPercentage = 0;
            entityHd.FinalDiscount = Convert.ToDecimal(txtFinalDiscountPIPctg.Text);
            entityHd.PPHPercentage = Convert.ToDecimal(txtPPHPIPctg.Text);
            entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
            entityHd.StampAmount = Convert.ToDecimal(txtStampPI.Text);
            entityHd.SupplierInvoiceNo = txtSupplierInvoiceNo.Text;
            entityHd.TaxInvoiceNo = txtTaxInvoiceNo.Text;
        }

        public void SavePurchaseInvoiceHd(IDbContext ctx, ref int PurchaseInvoiceID)
        {
            PurchaseInvoiceHdDao entityHdDao = new PurchaseInvoiceHdDao(ctx);
            if (hdnPurchaseInvoiceID.Value == "0")
            {
                PurchaseInvoiceHd entityHd = new PurchaseInvoiceHd();
                ControlToEntity(entityHd);
                entityHd.PurchaseInvoiceNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.PURCHASE_INVOICE, entityHd.PurchaseInvoiceDate, ctx);
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);
                PurchaseInvoiceID = BusinessLayer.GetPurchaseInvoiceHdMaxID(ctx);
            }
            else
            {
                PurchaseInvoiceID = Convert.ToInt32(hdnPurchaseInvoiceID.Value);
            }
        }


        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            try
            {
                int PurchaseInvoiceID = 0;
                SavePurchaseInvoiceHd(ctx, ref PurchaseInvoiceID);
                retval = PurchaseInvoiceID.ToString();
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
                PurchaseInvoiceHd entity = BusinessLayer.GetPurchaseInvoiceHd(Convert.ToInt32(hdnPurchaseInvoiceID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdatePurchaseInvoiceHd(entity);
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
            bool result = true;

            IDbContext ctx = DbFactory.Configure(true);
            PurchaseInvoiceHdDao entityDao = new PurchaseInvoiceHdDao(ctx);
            PurchaseReceiveHdDao purchaseHdDao = new PurchaseReceiveHdDao(ctx);
            PurchaseReceiveDtDao purchaseDtDao = new PurchaseReceiveDtDao(ctx);
            ItemPlanningDao itemPlanningDao = new ItemPlanningDao(ctx);
            try
            {
                PurchaseInvoiceHd entity = BusinessLayer.GetPurchaseInvoiceHd(Convert.ToInt32(hdnPurchaseInvoiceID.Value));
                ControlToEntity(entity);
                entity.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

                List<PurchaseReceiveHd> lstPurchaseReceiveHd = BusinessLayer.GetPurchaseReceiveHdList(String.Format("PurchaseReceiveID IN (SELECT PurchaseReceiveID FROM PurchaseInvoiceDt WHERE PurchaseInvoiceID = {0} AND PurchaseReceiveID IS NOT NULL AND IsDeleted = 0) AND GCTransactionStatus = '{1}'", entity.PurchaseInvoiceID, Constant.TransactionStatus.PROCESSED), ctx);
                foreach (PurchaseReceiveHd purchaseReceiveHd in lstPurchaseReceiveHd)
                {
                    purchaseReceiveHd.GCTransactionStatus = Constant.TransactionStatus.CLOSED;
                    purchaseReceiveHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    purchaseHdDao.Update(purchaseReceiveHd);
                }
                string lstPurchaseReceiveID = string.Join(",", lstPurchaseReceiveHd.Select(p => p.PurchaseReceiveID).ToList());
                if (lstPurchaseReceiveID != "")
                {
                    List<PurchaseReceiveDt> lstPurchaseReceiveDt = BusinessLayer.GetPurchaseReceiveDtList(String.Format("PurchaseReceiveID IN ({0}) AND GCItemDetailStatus != '{1}'", lstPurchaseReceiveID, Constant.TransactionStatus.VOID), ctx);
                    foreach (PurchaseReceiveDt purchaseDt in lstPurchaseReceiveDt)
                    {
                        purchaseDt.GCItemDetailStatus = Constant.TransactionStatus.CLOSED;
                        purchaseDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        purchaseDtDao.Update(purchaseDt);
                    }
                }
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

        protected override bool OnReopenRecord(ref string errMessage)
        {
            bool result = true;

            IDbContext ctx = DbFactory.Configure(true);
            PurchaseInvoiceHdDao entityDao = new PurchaseInvoiceHdDao(ctx);
            PurchaseReceiveHdDao purchaseHdDao = new PurchaseReceiveHdDao(ctx);
            PurchaseReceiveDtDao purchaseDtDao = new PurchaseReceiveDtDao(ctx);
            ItemPlanningDao itemPlanningDao = new ItemPlanningDao(ctx);
            try
            {
                PurchaseInvoiceHd entity = BusinessLayer.GetPurchaseInvoiceHd(Convert.ToInt32(hdnPurchaseInvoiceID.Value));
                entity.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

                List<PurchaseReceiveHd> lstPurchaseReceiveHd = BusinessLayer.GetPurchaseReceiveHdList(String.Format("PurchaseReceiveID IN (SELECT PurchaseReceiveID FROM PurchaseInvoiceDt WHERE PurchaseInvoiceID = {0} AND PurchaseReceiveID IS NOT NULL AND IsDeleted = 0) AND GCTransactionStatus = '{1}'", entity.PurchaseInvoiceID, Constant.TransactionStatus.CLOSED), ctx);
                foreach (PurchaseReceiveHd purchaseReceiveHd in lstPurchaseReceiveHd)
                {
                    purchaseReceiveHd.GCTransactionStatus = Constant.TransactionStatus.PROCESSED;
                    purchaseReceiveHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    purchaseHdDao.Update(purchaseReceiveHd);
                }
                string lstPurchaseReceiveID = string.Join(",", lstPurchaseReceiveHd.Select(p => p.PurchaseReceiveID).ToList());
                if (lstPurchaseReceiveID != "")
                {
                    List<PurchaseReceiveDt> lstPurchaseReceiveDt = BusinessLayer.GetPurchaseReceiveDtList(String.Format("PurchaseReceiveID IN ({0}) AND GCItemDetailStatus != '{1}'", lstPurchaseReceiveID, Constant.TransactionStatus.VOID), ctx);
                    foreach (PurchaseReceiveDt purchaseDt in lstPurchaseReceiveDt)
                    {
                        purchaseDt.GCItemDetailStatus = Constant.TransactionStatus.PROCESSED;
                        purchaseDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        purchaseDtDao.Update(purchaseDt);
                    }
                }
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
        #endregion

        #region callBack Trigger
        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            hdnTotalAmount.Value = hdnTotalAmountBeforeDP.Value = "0";
            int pageCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount, false);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount, true);
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
        #endregion

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            int PurchaseInvoiceID = 0;
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    PurchaseInvoiceID = Convert.ToInt32(hdnPurchaseInvoiceID.Value);
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage, ref PurchaseInvoiceID))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                PurchaseInvoiceID = Convert.ToInt32(hdnPurchaseInvoiceID.Value);
                if (OnDeleteEntityDt(ref errMessage, PurchaseInvoiceID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpPurchaseInvoiceID"] = PurchaseInvoiceID.ToString();
        }

        private void ControlToEntity(PurchaseInvoiceDt entityDt)
        {
            entityDt.GLAPOtherID = Convert.ToInt32(cboGLAPOther.Value);
            entityDt.TransactionAmount = Convert.ToDecimal(Request.Form[txtTransactionAmount.UniqueID]);
            entityDt.FinalDiscountAmount = Convert.ToDecimal(Request.Form[txtDiscountAmount.UniqueID]);
            entityDt.VATAmount = Convert.ToDecimal(Request.Form[txtVAT.UniqueID]);
            entityDt.PPH23Amount = Convert.ToDecimal(Request.Form[txtPPh23.UniqueID]);
            entityDt.PPH25Amount = Convert.ToDecimal(Request.Form[txtPPh25.UniqueID]);
            entityDt.ChargesAmount = Convert.ToDecimal(Request.Form[txtChargesAmount.UniqueID]);
            entityDt.StampAmount = Convert.ToDecimal(Request.Form[txtStampAmount.UniqueID]);
            entityDt.DownPaymentAmount = Convert.ToDecimal(Request.Form[txtDownPayment.UniqueID]);
            entityDt.CreditNoteAmount = Convert.ToDecimal(Request.Form[txtCreditNote.UniqueID]);
            entityDt.ReferenceNo = txtInvoiceNo.Text;
            entityDt.ReferenceDate = Helper.GetDatePickerValue(txtInvoiceDate.Text);
            entityDt.LineAmount = entityDt.TransactionAmount - entityDt.DiscountAmount - entityDt.FinalDiscountAmount + entityDt.VATAmount + entityDt.PPH23Amount + entityDt.PPH25Amount + entityDt.StampAmount + entityDt.ChargesAmount - entityDt.DownPaymentAmount - entityDt.CreditNoteAmount;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int PurchaseInvoiceID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseInvoiceDtDao entityDtDao = new PurchaseInvoiceDtDao(ctx);
            try
            {
                SavePurchaseInvoiceHd(ctx, ref PurchaseInvoiceID);
                PurchaseInvoiceDt entityDt = new PurchaseInvoiceDt();
                ControlToEntity(entityDt);
                entityDt.PurchaseInvoiceID = PurchaseInvoiceID;
                entityDt.CreatedBy = AppSession.UserLogin.UserID;
                entityDtDao.Insert(entityDt);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                result = false;
                errMessage = ex.Message;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseInvoiceDtDao entityDtDao = new PurchaseInvoiceDtDao(ctx);
            try
            {
                PurchaseInvoiceDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entityDt);
                entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDtDao.Update(entityDt);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                result = false;
                errMessage = ex.Message;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnDeleteEntityDt(ref string errMessage, int ID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseInvoiceDtDao entityDtDao = new PurchaseInvoiceDtDao(ctx);
            try
            {
                PurchaseInvoiceDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                entityDt.IsDeleted = true;
                entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDtDao.Update(entityDt);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
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
        #endregion

        #region Void Entity
        protected override bool OnVoidRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseInvoiceHdDao entityDao = new PurchaseInvoiceHdDao(ctx);
            PurchaseInvoiceDtDao entityDtDao = new PurchaseInvoiceDtDao(ctx);
            try
            {
                PurchaseInvoiceHd entity = BusinessLayer.GetPurchaseInvoiceHd(Convert.ToInt32(hdnPurchaseInvoiceID.Value));
                entity.GCTransactionStatus = Constant.TransactionStatus.VOID;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

                List<PurchaseInvoiceDt> lstPurchaseInvoiceDt = BusinessLayer.GetPurchaseInvoiceDtList(String.Format("PurchaseInvoiceID = {0} AND IsDeleted = 0", entity.PurchaseInvoiceID), ctx);
                foreach (PurchaseInvoiceDt purchaseInvoiceDt in lstPurchaseInvoiceDt)
                {
                    purchaseInvoiceDt.IsDeleted = true;
                    purchaseInvoiceDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Update(purchaseInvoiceDt);
                }

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
        #endregion
    }
}