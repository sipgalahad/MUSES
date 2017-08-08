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

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class PurchaseReceiveEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.PURCHASE_RECEIVE;
        }

        #region Html Getter
        protected string OnGetPurchaseOrderFilterExpression()
        {
            return string.Format("GCTransactionStatus = '{0}'", Constant.TransactionStatus.APPROVED);
        }
        protected string OnGetFilterExpressionServiceUnit()
        {
            if (hdnListSiteServiceUnitID.Value != "")
                return string.Format("SiteServiceUnitID IN ({0}) AND IsDeleted = 0", hdnListSiteServiceUnitID.Value);
            return "1 = 0";
        }
        protected string OnGetFilterExpressionLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.PURCHASE_RECEIVE);
        }
        protected string OnGetFilterExpressionItemGroup()
        {
            return string.Format("GCItemType = '{0}' AND IsDeleted = 0", Constant.ItemType.PRODUCT);
        }
        protected string OnGetFilterExpressionItemProduct()
        {
            return string.Format("GCItemType = '{0}' AND IsDeleted = 0", Constant.ItemType.PRODUCT);
        }
        protected string OnGetFilterExpressionSupplier()
        {
            return string.Format("GCBusinessPartnerType = '{0}' AND IsDeleted = 0", Constant.BusinessObjectType.SUPPLIER);
        }
        protected string GetVATPercentageLabel()
        {
            return hdnVATPercentage.Value;
        }
        #endregion

        protected override void InitializeDataControl()
        {
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();
            List<GetLocationUserList> lstUserLocation = BusinessLayer.GetLocationUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.PURCHASE_RECEIVE, "");
            if (lstUserLocation.Count > 0)
            {
                List<GetServiceUnitUserList> lstUserServiceUnit = BusinessLayer.GetServiceUnitUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, string.Format("SiteServiceUnitID IN (SELECT SiteServiceUnitID FROM ServiceUnitLocation WHERE LocationID IN ({0}))", string.Join(",", lstUserLocation.Select(p => p.LocationID).ToList())));
                hdnListSiteServiceUnitID.Value = string.Join(",", lstUserServiceUnit.Select(p => p.SiteServiceUnitID).ToList());
                if (lstUserServiceUnit.Count == 1)
                {
                    GetServiceUnitUserList serviceUnit = lstUserServiceUnit.FirstOrDefault();
                    hdnDefaultSiteServiceUnitID.Value = serviceUnit.SiteServiceUnitID.ToString();
                    hdnDefaultServiceUnitCode.Value = serviceUnit.ServiceUnitCode;
                    hdnDefaultServiceUnitName.Value = serviceUnit.ServiceUnitName;
                }
            }

            List<SettingParameter> lstSettingParameter = BusinessLayer.GetSettingParameterList(string.Format("ParameterCode IN ('{0}','{1}','{2}')",
                                                                                               Constant.SettingParameter.VAT_PERCENTAGE,
                                                                                               Constant.SettingParameter.IS_DISCOUNT_APPLIED_TO_UNIT_PRICE, 
                                                                                               Constant.SettingParameter.IS_PURCHASE_RECEIVE_ALLOW_MULTI_PURCHASE_ORDER));

            hdnVATPercentage.Value = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.VAT_PERCENTAGE).ParameterValue;
            hdnIsDiscountAppliedToUnitPrice.Value = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.IS_DISCOUNT_APPLIED_TO_UNIT_PRICE).ParameterValue;
            hdnIsAllowMultiPO.Value = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.IS_PURCHASE_RECEIVE_ALLOW_MULTI_PURCHASE_ORDER).ParameterValue;

            if (hdnIsAllowMultiPO.Value == "1")
                trPurchaseOrder.Style.Add("display", "none");

            SetControlProperties();

            decimal tempTransactionAmount = -1;
            BindGridView(1, true, ref PageCount, ref RowCount, ref tempTransactionAmount);
            Helper.SetControlEntrySetting(txtQuantity, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtItemCode, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboItemUnit, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        protected void rptSite_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Site obj = (Site)e.Item.DataItem;
                CheckBox chkSite = (CheckBox)e.Item.FindControl("chkSite");
                chkSite.Attributes.Add("sitename", obj.SiteName);
                chkSite.Attributes.Add("siteid", obj.SiteID);
            }
        }

        protected string GetVATPercentage()
        {
            return hdnVATPercentage.Value;
        }

        protected override void SetControlProperties()
        {
            Repeater rptSite = (Repeater)ddeSite.FindControl("rptSite");
            List<Site> lstSite = BusinessLayer.GetSiteList(string.Format("IsHeader = 0"));
            rptSite.DataSource = lstSite;
            rptSite.DataBind();

            List<StandardCode> listStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsDeleted = 0", Constant.StandardCode.CURRENCY_CODE, Constant.StandardCode.CHARGES_TYPE));
            List<Term> listTerm = BusinessLayer.GetTermList(string.Format("isDeleted = 0"));
            Methods.SetComboBoxField<StandardCode>(cboChargesType, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.CHARGES_TYPE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboCurrency, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.CURRENCY_CODE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<Term>(cboTerm, listTerm, "TermName", "TermID");
            cboChargesType.SelectedIndex = 0;
            cboCurrency.SelectedIndex = 0;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnPRID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtPurchaseReceiveNo, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtPurchaseReceiveDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(txtPurchaseReceiveTime, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.TIME_FORMAT)));
            SetControlEntrySetting(lblSupplier, new ControlEntrySetting(true, false));
            SetControlEntrySetting(hdnSupplierID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSupplierCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtSupplierName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtFacturNo, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtDateReferrence, new ControlEntrySetting(true, true, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));

            SetControlEntrySetting(cboTerm, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtServiceUnitCode, new ControlEntrySetting(true, false, true, hdnDefaultServiceUnitCode.Value));
            SetControlEntrySetting(lblSiteServiceUnit, new ControlEntrySetting(true, false));
            SetControlEntrySetting(txtServiceUnitName, new ControlEntrySetting(false, false, true, hdnDefaultServiceUnitName.Value));
            SetControlEntrySetting(txtLocationCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(lblLocation, new ControlEntrySetting(true, false));
            SetControlEntrySetting(txtLocationName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(cboCurrency, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtKurs, new ControlEntrySetting(true, true, true, "1.00"));

            SetControlEntrySetting(txtPurchaseOrderNo, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(lblPurchaseOrderNo, new ControlEntrySetting(true, false));

            SetControlEntrySetting(hdnLocationID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSiteServiceUnitID, new ControlEntrySetting(true, true, false, hdnDefaultSiteServiceUnitID.Value));
            SetControlEntrySetting(hdnLstSiteID, new ControlEntrySetting(false, false));

            SetControlEntrySetting(txtFinalDiscountAmount, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(txtPPN, new ControlEntrySetting(false, false, true, "0"));
            SetControlEntrySetting(txtFinalDiscountPercentage, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(txtDP, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(txtTotalNetTransactionAmount, new ControlEntrySetting(true, true, true, "0"));
        }

        public override void OnAddRecord()
        {
            hdnPageCount.Value = "0";
            hdnRowCount.Value = "0";
            hdnIsEditable.Value = "1";
        }

        protected string IsEditable()
        {
            return hdnIsEditable.Value;
        }

        #region Load Entity
        protected string GetFilterExpression()
        {
            if (hdnListSiteServiceUnitID.Value != "")
            {
                string filterExpression = "";
                if (filterExpression != "")
                    filterExpression += " AND ";
                filterExpression += string.Format("SiteServiceUnitID IN ({0}) AND TransactionCode = '{1}'", hdnListSiteServiceUnitID.Value, Constant.TransactionCode.PURCHASE_RECEIVE);
                return filterExpression;
            }
            return "1 = 0";
        }

        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvPurchaseReceiveHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vPurchaseReceiveHd entity = BusinessLayer.GetvPurchaseReceiveHd(filterExpression, PageIndex, "PurchaseReceiveID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvPurchaseReceiveHdRowIndex(filterExpression, keyValue, "PurchaseReceiveID DESC");
            vPurchaseReceiveHd entity = BusinessLayer.GetvPurchaseReceiveHd(filterExpression, PageIndex, "PurchaseReceiveID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vPurchaseReceiveHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
                hdnIsEditable.Value = "0";
            }
            else
                hdnIsEditable.Value = "1";

            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN && entity.GCTransactionStatus != Constant.TransactionStatus.WAIT_FOR_APPROVAL && entity.GCTransactionStatus != Constant.TransactionStatus.VOID)
                hdnIsAllowPrintTemporary.Value = "1";
            else
                hdnIsAllowPrintTemporary.Value = "0";

            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN && entity.GCTransactionStatus != Constant.TransactionStatus.WAIT_FOR_APPROVAL && entity.GCTransactionStatus != Constant.TransactionStatus.APPROVED && entity.GCTransactionStatus != Constant.TransactionStatus.VOID)
                hdnIsAllowPrintFinal.Value = "1";
            else
                hdnIsAllowPrintFinal.Value = "0";

            hdnPRID.Value = entity.PurchaseReceiveID.ToString();
            txtPurchaseReceiveNo.Text = entity.PurchaseReceiveNo;
            txtPurchaseReceiveDate.Text = entity.ReceivedDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtPurchaseReceiveTime.Text = entity.ReceivedTime;
            txtDateReferrence.Text = entity.ReferenceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            hdnSupplierID.Value = entity.SupplierID.ToString();
            txtSupplierCode.Text = entity.SupplierCode;
            txtSupplierName.Text = entity.SupplierName;
            hdnIsLineAmountRounded.Value = entity.IsLineAmountRounded ? "1" : "0";
            hdnLineAmountRoundedFormat.Value = entity.LineAmountRoundedFormat.ToString();
            hdnIsTotalAmountRounded.Value = entity.IsTotalAmountRounded ? "1" : "0";
            hdnTotalAmountRoundedFormat.Value = entity.TotalAmountRoundedFormat.ToString();
            hdnPurchaseOrderID.Value = entity.PurchaseOrderID.ToString();
            txtPurchaseOrderNo.Text = entity.PurchaseOrderNo;
            txtFacturNo.Text = entity.ReferenceNo;
            hdnSiteServiceUnitID.Value = entity.SiteServiceUnitID.ToString();
            txtServiceUnitCode.Text = entity.ServiceUnitCode;
            txtServiceUnitName.Text = entity.ServiceUnitName;
            hdnLocationID.Value = entity.LocationID.ToString();
            txtLocationCode.Text = entity.LocationCode;
            txtLocationName.Text = entity.LocationName;
            txtCharges.Text = entity.ChargesAmount.ToString();
            txtDPReferrenceNo.Text = entity.DownPaymentReferenceNo;
            txtDP.Text = entity.DownPaymentAmount.ToString();
            cboChargesType.Value = entity.GCChargesType.ToString();
            cboTerm.Value = entity.TermID.ToString();
            txtNotes.Text = entity.Remarks;
            cboCurrency.Value = entity.GCCurrencyCode.ToString();
            txtKurs.Text = entity.CurrencyRate.ToString();
            chkPPN.Checked = entity.IsIncludeVAT;
            txtTransactionAmount.Text = entity.TransactionAmount.ToString();
            txtFinalDiscountPercentage.Text = entity.FinalDiscountPercentage.ToString();
            txtFinalDiscountAmount.Text = entity.FinalDiscountAmount.ToString();
            txtTotalNetTransactionAmount.Text = entity.TotalNetTransactionAmount.ToString();
            hdnLstSiteID.Value = entity.ListSiteID;

            decimal tempTransactionAmount = -1;
            BindGridView(1, true, ref PageCount, ref RowCount, ref tempTransactionAmount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount, ref decimal transactionAmount)
        {
            string filterExpression = "1 = 0";
            if (hdnPRID.Value != "")
                filterExpression = string.Format("PurchaseReceiveID = {0} AND GCItemDetailStatus != '{1}'", hdnPRID.Value, Constant.TransactionStatus.VOID);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvPurchaseReceiveDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            if (transactionAmount > -1)
                transactionAmount = BusinessLayer.GetPurchaseReceiveHd(Convert.ToInt32(hdnPRID.Value)).TransactionAmount;

            List<vPurchaseReceiveDt> lstEntity = BusinessLayer.GetvPurchaseReceiveDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ItemName1 ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vPurchaseReceiveDt entity = e.Row.DataItem as vPurchaseReceiveDt;
                CheckBox chkIsBonus = e.Row.FindControl("chkIsBonus") as CheckBox;
                chkIsBonus.Checked = entity.IsBonusItem;
            }
        }
        #endregion

        #region Save Edit Header
        private void ControlToEntityHd(IDbContext ctx, PurchaseReceiveHd entityHd)
        {
            TermDao termDao = new TermDao(ctx);
            entityHd.ReceivedDate = Helper.GetDatePickerValue(txtPurchaseReceiveDate.Text);
            entityHd.ReceivedTime = txtPurchaseReceiveTime.Text;
            entityHd.SiteServiceUnitID = Convert.ToInt32(hdnSiteServiceUnitID.Value);
            entityHd.LocationID = Convert.ToInt32(hdnLocationID.Value);
            entityHd.BusinessPartnerID = Convert.ToInt32(hdnSupplierID.Value);
            if (hdnPurchaseOrderID.Value != "" && hdnPurchaseOrderID.Value != "0")
                entityHd.PurchaseOrderID = Convert.ToInt32(hdnPurchaseOrderID.Value);
            else
                entityHd.PurchaseOrderID = null;
            entityHd.TermID = Convert.ToInt32(cboTerm.Value.ToString());
            entityHd.ReferenceNo = Request.Form[txtFacturNo.UniqueID];
            entityHd.ReferenceDate = Helper.GetDatePickerValue(Request.Form[txtDateReferrence.UniqueID]);

            entityHd.GCCurrencyCode = cboCurrency.Value.ToString();
            entityHd.CurrencyRate = Convert.ToDecimal(txtKurs.Text);
            entityHd.IsIncludeVAT = chkPPN.Checked;

            if (entityHd.IsIncludeVAT)
                entityHd.VATPercentage = Convert.ToInt32(hdnVATPercentage.Value);
            else
                entityHd.VATPercentage = 0;
            entityHd.VATAmount = Convert.ToDecimal(Request.Form[txtPPN.UniqueID]);

            entityHd.Remarks = txtNotes.Text;
            entityHd.ChargesAmount = Convert.ToDecimal(txtCharges.Text);
            entityHd.DownPaymentReferenceNo = txtDPReferrenceNo.Text;
            entityHd.GCChargesType = cboChargesType.Value.ToString();
            entityHd.FinalDiscountPercentage = Convert.ToDecimal(Request.Form[txtFinalDiscountPercentage.UniqueID]);
            entityHd.FinalDiscountAmount = Convert.ToDecimal(Request.Form[txtFinalDiscountAmount.UniqueID]);
            entityHd.DownPaymentAmount = Convert.ToDecimal(txtDP.Text);
            entityHd.TransactionAmountBeforeRounded = entityHd.TransactionAmount + entityHd.VATAmount - entityHd.FinalDiscountAmount + entityHd.StampAmount + entityHd.ChargesAmount - entityHd.DownPaymentAmount;
            entityHd.TotalNetTransactionAmount = Convert.ToDecimal(txtTotalNetTransactionAmount.Text);
            entityHd.RoundedAmount = entityHd.TotalNetTransactionAmount - entityHd.TransactionAmountBeforeRounded;
            int termDay = termDao.Get(entityHd.TermID).TermDay;
            entityHd.PaymentDueDate = entityHd.ReferenceDate.AddDays(termDay);
        }

        public void SavePurchaseReceiveHd(IDbContext ctx, ref int PRID, ref string PRNo)
        {
            PurchaseReceiveHdDao entityHdDao = new PurchaseReceiveHdDao(ctx);
            PurchaseReceiveHdSiteDao entityHdSiteDao = new PurchaseReceiveHdSiteDao(ctx);
            if (hdnPRID.Value == "0")
            {
                PurchaseReceiveHd entityHd = new PurchaseReceiveHd();
                ControlToEntityHd(ctx, entityHd);
                entityHd.TransactionCode = Constant.TransactionCode.PURCHASE_RECEIVE;
                entityHd.PurchaseReceiveNo = BusinessLayer.GenerateTransactionNo(entityHd.TransactionCode, entityHd.ReceivedDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                PRID = entityHdDao.Insert(entityHd);

                if (hdnLstSiteID.Value != "")
                {
                    string[] lstSiteID = hdnLstSiteID.Value.Split(',');
                    foreach (string siteID in lstSiteID)
                    {
                        PurchaseReceiveHdSite entityDt = new PurchaseReceiveHdSite();
                        entityDt.PurchaseReceiveID = PRID;
                        entityDt.SiteID = siteID;
                        entityHdSiteDao.Insert(entityDt);
                    }
                }
                PRNo = entityHd.PurchaseReceiveNo;
            }
            else
            {
                PRID = Convert.ToInt32(hdnPRID.Value);
                PRNo = Request.Params[txtPurchaseReceiveNo.UniqueID];
                PurchaseReceiveHd entityHd = entityHdDao.Get(PRID);
                ControlToEntityHd(ctx, entityHd);
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Update(entityHd);
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            try
            {
                int PRID = 0;
                string purchaseReceiveNo = "";
                SavePurchaseReceiveHd(ctx, ref PRID, ref purchaseReceiveNo);
                retval = PRID.ToString();
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

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseReceiveHdDao entityHdDao = new PurchaseReceiveHdDao(ctx);
            PurchaseReceiveHdSiteDao entityHdSiteDao = new PurchaseReceiveHdSiteDao(ctx);
            try
            {
                PurchaseReceiveHd entity = entityHdDao.Get(Convert.ToInt32(hdnPRID.Value));
                if (entity.GCTransactionStatus == Constant.TransactionStatus.OPEN)
                {
                    ControlToEntityHd(ctx, entity);
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityHdDao.Update(entity);

                    List<PurchaseReceiveHdSite> lstEntityDt = BusinessLayer.GetPurchaseReceiveHdSiteList(string.Format("PurchaseReceiveID = {0}", entity.PurchaseReceiveID), ctx);

                    if (hdnLstSiteID.Value != "")
                    {
                        string[] lstSiteID = hdnLstSiteID.Value.Split(',');
                        foreach (string siteID in lstSiteID)
                        {
                            PurchaseReceiveHdSite entityDt = lstEntityDt.FirstOrDefault(p => p.SiteID == siteID);
                            if (entityDt == null)
                            {
                                entityDt = new PurchaseReceiveHdSite();
                                entityDt.PurchaseReceiveID = entity.PurchaseReceiveID;
                                entityDt.SiteID = siteID;
                                entityHdSiteDao.Insert(entityDt);
                            }
                            else
                                lstEntityDt.Remove(entityDt);
                        }
                    }

                    foreach (PurchaseReceiveHdSite entityDt in lstEntityDt)
                    {
                        entityHdSiteDao.Delete(entityDt.PurchaseReceiveID, entityDt.SiteID);
                    }
                }
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

        protected override bool OnApproveRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseOrderHdDao purchaseOrderHdDao = new PurchaseOrderHdDao(ctx);
            PurchaseOrderDtDao purchaseOrderDtDao = new PurchaseOrderDtDao(ctx);
            PurchaseReceiveHdDao purchaseHdDao = new PurchaseReceiveHdDao(ctx);
            PurchaseReceiveDtDao purchaseDtDao = new PurchaseReceiveDtDao(ctx);
            ItemPlanningDao itemPlanningDao = new ItemPlanningDao(ctx);

            try
            {
                ctx.CommandText = "ALTER TABLE PurchaseReceiveDt DISABLE TRIGGER onPurchaseReceieveDtChanged";
                DaoBase.ExecuteNonQuery(ctx);
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                PurchaseReceiveHd entity = purchaseHdDao.Get(Convert.ToInt32(hdnPRID.Value));
                if (entity.GCTransactionStatus == Constant.TransactionStatus.OPEN || entity.GCTransactionStatus == Constant.TransactionStatus.WAIT_FOR_APPROVAL)
                {
                    ControlToEntityHd(ctx, entity);
                    List<PurchaseReceiveDt> lstPurchaseReceiveDt = BusinessLayer.GetPurchaseReceiveDtList(String.Format("PurchaseReceiveID = {0} AND GCItemDetailStatus != '{1}'", entity.PurchaseReceiveID, Constant.TransactionStatus.VOID), ctx);
                    
                    String lstItemID = String.Join(",", lstPurchaseReceiveDt.Select(p => p.ItemID).ToList());
                    string filterExpression = String.Format("SiteID = '{0}' AND ItemID IN ({1}) AND IsDeleted = 0", AppSession.UserLogin.SiteID, lstItemID);
                    List<ItemPlanning> lstItemPlanning = BusinessLayer.GetItemPlanningList(filterExpression, ctx);

                    List<PurchaseReceiveDt> lstPendingPurchaseReceiveDt = BusinessLayer.GetPurchaseReceiveDtList(String.Format("PurchaseReceiveID != {0} AND ItemID IN ({1}) AND GCItemDetailStatus IN ('{2}','{3}') AND QtyBeforeApproved != 0", entity.PurchaseReceiveID, lstItemID, Constant.TransactionStatus.OPEN, Constant.TransactionStatus.WAIT_FOR_APPROVAL), ctx);
                    List<DirectPurchaseDt> lstPendingDirectPurchaseDt = BusinessLayer.GetDirectPurchaseDtList(String.Format("ItemID IN ({0}) AND GCItemDetailStatus IN ('{1}','{2}') AND QtyBeforeApproved != 0", lstItemID, Constant.TransactionStatus.OPEN, Constant.TransactionStatus.WAIT_FOR_APPROVAL), ctx);
                    if (lstPendingPurchaseReceiveDt.Count > 0 || lstPendingDirectPurchaseDt.Count > 0)
                    {
                        string lstPurchaseReceiveID = String.Join(",", lstPendingPurchaseReceiveDt.Select(p => p.PurchaseReceiveID).ToList());
                        String lstPurchaseReceiveNo = "";
                        if (lstPendingPurchaseReceiveDt.Count > 0)
                        {
                            List<PurchaseReceiveHd> lstRequiredPurchaseReceiveHd = BusinessLayer.GetPurchaseReceiveHdList(String.Format("PurchaseReceiveID IN ({0})", lstPurchaseReceiveID), ctx);
                            lstPurchaseReceiveNo = String.Join(",", lstRequiredPurchaseReceiveHd.Select(p => string.Format("<b>{0}</b>", p.PurchaseReceiveNo)).ToList());
                        }
                        string lstDirectPurchaseID = String.Join(",", lstPendingDirectPurchaseDt.Select(p => p.DirectPurchaseID).ToList());
                        String lstDirectPurchaseNo = "";
                        if (lstPendingDirectPurchaseDt.Count > 0)
                        {
                            List<DirectPurchaseHd> lstRequiredDirectPurchaseHd = BusinessLayer.GetDirectPurchaseHdList(String.Format("DirectPurchaseID IN ({0})", lstDirectPurchaseID), ctx);
                            lstDirectPurchaseNo = String.Join(",", lstRequiredDirectPurchaseHd.Select(p => string.Format("<b>{0}</b>", p.DirectPurchaseNo)).ToList());
                        }
                        if (lstPurchaseReceiveNo != "")
                        {
                            errMessage = string.Format("Harap Proses Penerimaan Dengan Nomor {0} Terlebih Dahulu", lstPurchaseReceiveNo);
                            if (lstDirectPurchaseNo != "")
                                errMessage += "<br/>";
                        }
                        if (lstDirectPurchaseNo != "")
                            errMessage += string.Format("Harap Pembelian Tunai Dengan Nomor {0} Terlebih Dahulu", lstDirectPurchaseNo);
                        result = false;
                    }
                    else
                    {
                        foreach (PurchaseReceiveDt purchaseDt in lstPurchaseReceiveDt)
                        {
                            purchaseDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
                            purchaseDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            purchaseDtDao.Update(purchaseDt);

                            if (purchaseDt.ItemID > 0)
                            {
                                ItemPlanning entityItemPlanning = lstItemPlanning.Where(x => x.ItemID == purchaseDt.ItemID).FirstOrDefault();
                                decimal purchaseUnitPrice = purchaseDt.UnitPrice;
                                decimal unitPrice = 0;
                                if (hdnIsDiscountAppliedToUnitPrice.Value == "1")
                                {
                                    decimal discountAmount1 = (purchaseUnitPrice * purchaseDt.DiscountPercentage1) / 100;
                                    decimal discountAmount2 = ((purchaseUnitPrice - discountAmount1) * purchaseDt.DiscountPercentage2) / 100;
                                    purchaseUnitPrice = purchaseUnitPrice - (discountAmount1 + discountAmount2);
                                }
                                unitPrice = purchaseUnitPrice / purchaseDt.ConversionFactor;
                                if (entityItemPlanning.LastPurchasePrice < unitPrice)
                                {
                                    entityItemPlanning.LastPurchasePrice = unitPrice;
                                    if (entityItemPlanning.UnitPrice < unitPrice)
                                    {
                                        entityItemPlanning.UnitPrice = unitPrice;
                                        entityItemPlanning.PurchaseUnitPrice = purchaseUnitPrice;
                                    }
                                }
                                if (!entityItemPlanning.ListPendingPurchaseReceiveID.Contains(string.Format("|{0}|", entity.PurchaseReceiveID)))
                                {
                                    entityItemPlanning.ListPendingPurchaseReceiveID += string.Format("|{0}|", entity.PurchaseReceiveID);
                                    if (entityItemPlanning.ListPendingPurchaseReceiveID.Length > 1000)
                                        entityItemPlanning.ListPendingPurchaseReceiveID = entityItemPlanning.ListPendingPurchaseReceiveID.Substring(0, 1000);
                                }
                                entityItemPlanning.LastUpdatedBy = AppSession.UserLogin.UserID;
                                itemPlanningDao.Update(entityItemPlanning);
                            }
                        }
                        entity.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                        if (entity.ApprovedDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT) == Constant.ConstantDate.DEFAULT_NULL)
                            entity.ApprovedDate = DateTime.Now;
                        entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                        purchaseHdDao.Update(entity);
                    }
                }
                ctx.CommandText = "ALTER TABLE PurchaseReceiveDt ENABLE TRIGGER onPurchaseReceieveDtChanged";
                DaoBase.ExecuteNonQuery(ctx);
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
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

        protected override bool OnProposeRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseReceiveHdDao purchaseHdDao = new PurchaseReceiveHdDao(ctx);
            PurchaseReceiveDtDao purchaseDtDao = new PurchaseReceiveDtDao(ctx);

            try
            {
                PurchaseReceiveHd entity = purchaseHdDao.Get(Convert.ToInt32(hdnPRID.Value));
                if (entity.GCTransactionStatus == Constant.TransactionStatus.OPEN)
                {
                    ControlToEntityHd(ctx, entity);
                    entity.GCTransactionStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    purchaseHdDao.Update(entity);

                    List<PurchaseReceiveDt> lstPurchaseReceiveDt = BusinessLayer.GetPurchaseReceiveDtList(String.Format("PurchaseReceiveID = {0} AND GCItemDetailStatus != '{1}'", entity.PurchaseReceiveID, Constant.TransactionStatus.VOID), ctx);
                    foreach (PurchaseReceiveDt purchaseDt in lstPurchaseReceiveDt)
                    {
                        purchaseDt.GCItemDetailStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
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
            PurchaseReceiveHdDao purchaseHdDao = new PurchaseReceiveHdDao(ctx);
            PurchaseReceiveDtDao purchaseDtDao = new PurchaseReceiveDtDao(ctx);

            try
            {
                PurchaseReceiveHd entity = purchaseHdDao.Get(Convert.ToInt32(hdnPRID.Value));
                if (entity.GCTransactionStatus == Constant.TransactionStatus.APPROVED || entity.GCTransactionStatus == Constant.TransactionStatus.WAIT_FOR_APPROVAL)
                {
                    List<PurchaseReceiveDt> lstPurchaseReceiveDt = BusinessLayer.GetPurchaseReceiveDtList(String.Format("PurchaseReceiveID = {0} AND GCItemDetailStatus != '{1}'", entity.PurchaseReceiveID, Constant.TransactionStatus.VOID), ctx);

                    List<String> lstRequiredPurchaseReceiveID = new List<String>();
                    String lstItemID = String.Join(",", lstPurchaseReceiveDt.Select(p => p.ItemID).ToList());

                    string filterExpression = String.Format("SiteID = '{0}' AND ItemID IN ({1}) AND IsDeleted = 0", AppSession.UserLogin.SiteID, lstItemID);
                    List<ItemPlanning> lstItemPlanning = BusinessLayer.GetItemPlanningList(filterExpression, ctx);

                    foreach (ItemPlanning itemPlanning in lstItemPlanning)
                    {
                        if (itemPlanning.ListPendingPurchaseReceiveID != "")
                        {
                            string temp = itemPlanning.ListPendingPurchaseReceiveID.Substring(1, itemPlanning.ListPendingPurchaseReceiveID.Length - 2);
                            string[] lstPendingPurchaseReceiveID = temp.Split(new string[] { "||" }, StringSplitOptions.None);
                            string prID = lstPendingPurchaseReceiveID.Last();
                            if (prID != hdnPRID.Value)
                            {
                                if (lstRequiredPurchaseReceiveID.Count(p => p == prID) == 0)
                                    lstRequiredPurchaseReceiveID.Add(prID);
                            }
                        }
                    }

                    if (lstRequiredPurchaseReceiveID.Count > 0)
                    {
                        string lstPurchaseReceiveID = String.Join(",", lstRequiredPurchaseReceiveID.Where(p => !p.Contains("D")).Select(p => p).ToList());
                        String lstPurchaseReceiveNo = "";
                        if (lstPurchaseReceiveID != "")
                        {
                            List<PurchaseReceiveHd> lstRequiredPurchaseReceiveHd = BusinessLayer.GetPurchaseReceiveHdList(String.Format("PurchaseReceiveID IN ({0})", lstPurchaseReceiveID), ctx);
                            lstPurchaseReceiveNo = String.Join(",", lstRequiredPurchaseReceiveHd.Select(p => string.Format("<b>{0}</b>", p.PurchaseReceiveNo)).ToList());
                        }

                        string lstDirectPurchaseID = String.Join(",", lstRequiredPurchaseReceiveID.Where(p => p.Contains("D")).Select(p => p.Substring(1)).ToList());
                        String lstDirectPurchaseNo = "";
                        if (lstDirectPurchaseID != "")
                        {
                            List<DirectPurchaseHd> lstRequiredDirectPurchaseHd = BusinessLayer.GetDirectPurchaseHdList(String.Format("DirectPurchaseID IN ({0})", lstDirectPurchaseID), ctx);
                            lstDirectPurchaseNo = String.Join(",", lstRequiredDirectPurchaseHd.Select(p => string.Format("<b>{0}</b>", p.DirectPurchaseNo)).ToList());
                        }
                        if (lstPurchaseReceiveNo != "")
                        {
                            errMessage = string.Format("Harap Batalkan Penerimaan Dengan Nomor {0} Terlebih Dahulu", lstPurchaseReceiveNo);
                            if (lstDirectPurchaseNo != "")
                                errMessage += "<br/>";
                        }
                        if (lstDirectPurchaseNo != "")
                            errMessage += string.Format("Harap Batalkan Penerimaan Dengan Nomor {0} Terlebih Dahulu", lstDirectPurchaseNo);
                        result = false;
                    }
                    else
                    {
                        entity.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                        entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                        purchaseHdDao.Update(entity);
                        foreach (PurchaseReceiveDt purchaseDt in lstPurchaseReceiveDt)
                        {
                            purchaseDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                            purchaseDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            purchaseDtDao.Update(purchaseDt);
                        }
                    }
                }
                else
                {
                    result = false;
                    errMessage = "Transaksi Sudah Diproses. Tidak Bisa Dibuka Kembali";
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

        protected override bool OnVoidRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseReceiveHdDao purchaseHdDao = new PurchaseReceiveHdDao(ctx);
            PurchaseReceiveDtDao purchaseDtDao = new PurchaseReceiveDtDao(ctx);
            PurchaseOrderHdDao purchaseOrderHdDao = new PurchaseOrderHdDao(ctx);
            PurchaseOrderDtDao purchaseOrderDtDao = new PurchaseOrderDtDao(ctx);
            PurchaseRequestPODao purchaseRequestPODao = new PurchaseRequestPODao(ctx);
            PurchaseReceivePODao purchaseReceivePODao = new PurchaseReceivePODao(ctx);
            ItemPlanningDao itemPlanningDao = new ItemPlanningDao(ctx);
            try
            {
                PurchaseReceiveHd entity = purchaseHdDao.Get(Convert.ToInt32(hdnPRID.Value));
                if (entity.GCTransactionStatus == Constant.TransactionStatus.OPEN)
                {
                    List<PurchaseOrderDt> lstEntityPODt = BusinessLayer.GetPurchaseOrderDtList(string.Format("ReceivedInformation LIKE '%|{0}|%'", hdnPRID.Value), ctx);
                    List<PurchaseReceivePO> lstEntityPRPO = BusinessLayer.GetPurchaseReceivePOList(string.Format("PurchaseReceiveID = {0}", hdnPRID.Value), ctx);

                    string lstPurchaseOrderID = string.Join(",", lstEntityPODt.GroupBy(p => p.PurchaseOrderID).Select(p => p.Key).ToList());
                    List<PurchaseRequestPO> lstEntityPQPO = BusinessLayer.GetPurchaseRequestPOList(string.Format("PurchaseOrderID IN ({0})", lstPurchaseOrderID), ctx);

                    if (lstEntityPODt.Count > 0)
                    {
                        List<PurchaseOrderHd> lstEntityPOHd = BusinessLayer.GetPurchaseOrderHdList(string.Format("PurchaseOrderID IN ({0}) AND GCTransactionStatus = '{1}'", lstPurchaseOrderID, Constant.TransactionStatus.CLOSED), ctx);
                        foreach (PurchaseOrderDt entityPODt in lstEntityPODt)
                        {
                            entityPODt.ReceivedInformation = entityPODt.ReceivedInformation.Replace("|" + hdnPRID.Value + "|", "");
                            PurchaseReceiveDt tempReceiveDt = BusinessLayer.GetPurchaseReceiveDtList(string.Format("PurchaseReceiveID = {0} AND ItemID = {1}", hdnPRID.Value, entityPODt.ItemID), ctx)[0];
                            entityPODt.ReceivedQuantity -= tempReceiveDt.Quantity;

                            PurchaseReceivePO entityPRPO = lstEntityPRPO.FirstOrDefault(p => p.ItemID == entityPODt.ItemID);
                            entityPRPO.ReceivedQuantity -= tempReceiveDt.Quantity;
                            purchaseReceivePODao.Update(entityPRPO);

                            entityPODt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            purchaseOrderDtDao.Update(entityPODt);

                            List<PurchaseRequestPO> lstEntityPQPO1 = lstEntityPQPO.Where(p => p.PurchaseOrderID == entityPODt.PurchaseOrderID && p.ItemID == entityPODt.ItemID).ToList();
                            decimal receivedQty = lstEntityPQPO.Sum(p => p.ReceivedQuantity) - tempReceiveDt.Quantity;
                            foreach (PurchaseRequestPO entityPQPO in lstEntityPQPO1)
                            {
                                entityPQPO.ReceivedQuantity = 0;
                            }
                            foreach (PurchaseRequestPO entityPQPO in lstEntityPQPO1)
                            {
                                if (receivedQty > 0)
                                {
                                    decimal outstandingOrder = entityPQPO.OrderQuantity - entityPQPO.ReceivedQuantity;
                                    if (receivedQty > outstandingOrder)
                                    {
                                        entityPQPO.ReceivedQuantity += outstandingOrder;
                                        receivedQty -= outstandingOrder;
                                    }
                                    else
                                    {
                                        entityPQPO.ReceivedQuantity += receivedQty;
                                        receivedQty = 0;
                                    }
                                }
                                purchaseRequestPODao.Update(entityPQPO);
                            }
                        }

                        foreach (PurchaseOrderHd entityPOHd in lstEntityPOHd)
                        {
                            entityPOHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                            entityPOHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                            purchaseOrderHdDao.Update(entityPOHd);
                        }
                    }
                    entity.GCTransactionStatus = Constant.TransactionStatus.VOID;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    purchaseHdDao.Update(entity);

                    List<PurchaseReceiveDt> lstPurchaseReceiveDt = BusinessLayer.GetPurchaseReceiveDtList(String.Format("PurchaseReceiveID = {0} AND GCItemDetailStatus != '{1}'", entity.PurchaseReceiveID, Constant.TransactionStatus.VOID), ctx);
                    foreach (PurchaseReceiveDt purchaseDt in lstPurchaseReceiveDt)
                    {
                        purchaseDt.GCItemDetailStatus = Constant.TransactionStatus.VOID;
                        purchaseDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        purchaseDtDao.Update(purchaseDt);
                    }

                    String lstItemID = String.Join(",", lstPurchaseReceiveDt.Select(p => p.ItemID).ToList());

                    string filterExpression = String.Format("SiteID = '{0}' AND ItemID IN ({1}) AND IsDeleted = 0", AppSession.UserLogin.SiteID, lstItemID);
                    List<ItemPlanning> lstItemPlanning = BusinessLayer.GetItemPlanningList(filterExpression, ctx);
                    foreach (ItemPlanning entityItemPlanning in lstItemPlanning)
                    {
                        if (entityItemPlanning.ListPendingPurchaseReceiveID.Contains(string.Format("|{0}|", entity.PurchaseReceiveID)))
                        {
                            entityItemPlanning.ListPendingPurchaseReceiveID = entityItemPlanning.ListPendingPurchaseReceiveID.Replace(string.Format("|{0}|", entity.PurchaseReceiveID), "");
                            entityItemPlanning.LastUpdatedBy = AppSession.UserLogin.UserID;
                            itemPlanningDao.Update(entityItemPlanning);

                            BusinessLayer.UpdateChargesCostAmount(entity.PurchaseReceiveID, entityItemPlanning.ItemID, entityItemPlanning.AveragePrice, false, ctx);
                            ctx.CommandType = CommandType.Text;
                            ctx.Command.Parameters.Clear();
                        }
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

        #region Trigger Callback
        protected void cboItemUnit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            List<vItemAlternateUnitCustom> lst = BusinessLayer.GetvItemAlternateUnitCustomList(string.Format("ItemID = {0}", hdnItemID.Value));
            Methods.SetComboBoxField<vItemAlternateUnitCustom>(cboItemUnit, lst, "cfAlternateUnit", "cfID");
            cboItemUnit.SelectedIndex = -1;
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            decimal transactionAmount = 0;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    transactionAmount = -1;
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount, ref transactionAmount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount, ref rowCount, ref transactionAmount);
                    result = string.Format("refresh|{0}|{1}|{2}", pageCount, rowCount, transactionAmount);
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
            int PRID = 0;
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";

            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    PRID = Convert.ToInt32(hdnPRID.Value);
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage, ref PRID))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                PRID = Convert.ToInt32(hdnPRID.Value);
                if (OnDeleteEntityDt(ref errMessage, PRID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpOrderID"] = PRID.ToString();
        }

        private void ControlToEntity(PurchaseReceiveDt entityDt)
        {
            entityDt.ItemID = Convert.ToInt32(hdnItemID.Value);
            entityDt.Quantity = Convert.ToDecimal(txtQuantity.Text);
            entityDt.GCItemUnit = cboItemUnit.Value.ToString().Split('|')[0];
            entityDt.GCBaseUnit = hdnGCBaseUnit.Value;
            entityDt.ConversionFactor = Convert.ToDecimal(hdnConversionFactor.Value);
            entityDt.UnitPrice = Convert.ToDecimal(txtPrice.Text);
            entityDt.DiscountPercentage1 = Convert.ToDecimal(txtDiscountPercentage1.Text);
            entityDt.DiscountAmount1 = Convert.ToDecimal(txtDiscountAmount1.Text);
            entityDt.DiscountPercentage2 = Convert.ToDecimal(txtDiscountPercentage2.Text);
            entityDt.DiscountAmount2 = Convert.ToDecimal(txtDiscountAmount2.Text);
            entityDt.LineAmount = Convert.ToDecimal(Request.Form[txtLineAmount.UniqueID]);
            entityDt.LineAmountBeforeRounded = (entityDt.UnitPrice * entityDt.Quantity) - entityDt.DiscountAmount1 - entityDt.DiscountAmount2;
            entityDt.RoundedAmount = entityDt.LineAmount - entityDt.LineAmountBeforeRounded;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int PRID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseReceiveDtDao entityDtDao = new PurchaseReceiveDtDao(ctx);
            try
            {
                string purchaseReceiveNo = "";
                SavePurchaseReceiveHd(ctx, ref PRID, ref purchaseReceiveNo);
                PurchaseReceiveDt entityDt = new PurchaseReceiveDt();
                ControlToEntity(entityDt);
                entityDt.IsBonusItem = true;
                entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                entityDt.PurchaseReceiveID = PRID;
                entityDt.PurchaseOrderID = null;
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
            PurchaseReceiveDtDao entityDtDao = new PurchaseReceiveDtDao(ctx);
            PurchaseReceivePODao purchaseReceivePODao = new PurchaseReceivePODao(ctx);
            PurchaseOrderDtDao entityPODtDao = new PurchaseOrderDtDao(ctx);
            PurchaseRequestPODao purchaseRequestPODao = new PurchaseRequestPODao(ctx);
            try
            {
                decimal editedQuantity = 0;
                PurchaseReceiveDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));

                if (entityDt.GCItemDetailStatus == Constant.TransactionStatus.OPEN)
                {
                    decimal oldReceivedQty = entityDt.Quantity;
                    editedQuantity -= entityDt.Quantity;
                    ControlToEntity(entityDt);
                    editedQuantity += entityDt.Quantity;
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Update(entityDt);

                    if (entityDt.PurchaseOrderID != null)
                    {
                        PurchaseOrderDt entityPODt = BusinessLayer.GetPurchaseOrderDtList(String.Format("PurchaseOrderID = {0} AND ItemID = {1}", entityDt.PurchaseOrderID, entityDt.ItemID), ctx)[0];
                        entityPODt.ReceivedQuantity += editedQuantity;
                        entityPODt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityPODtDao.Update(entityPODt);

                        PurchaseReceivePO entityPRPO = BusinessLayer.GetPurchaseReceivePOList(string.Format("PurchaseReceiveID = {0} AND ItemID = {1}", entityDt.PurchaseReceiveID, entityDt.ItemID), ctx).FirstOrDefault();
                        entityPRPO.ReceivedQuantity += editedQuantity;
                        purchaseReceivePODao.Update(entityPRPO);

                        List<PurchaseRequestPO> lstEntityPQPO = BusinessLayer.GetPurchaseRequestPOList(string.Format("PurchaseOrderID = {0} AND ItemID = {1}", entityPRPO.PurchaseOrderID, entityDt.ItemID), ctx);
                        decimal receivedQty = lstEntityPQPO.Sum(p => p.ReceivedQuantity) + editedQuantity;
                        foreach (PurchaseRequestPO entityPQPO in lstEntityPQPO)
                        {
                            entityPQPO.ReceivedQuantity = 0;
                        }
                        foreach (PurchaseRequestPO entityPQPO in lstEntityPQPO)
                        {
                            if (receivedQty > 0)
                            {
                                decimal outstandingOrder = entityPQPO.OrderQuantity - entityPQPO.ReceivedQuantity;
                                if (receivedQty > outstandingOrder)
                                {
                                    entityPQPO.ReceivedQuantity += outstandingOrder;
                                    receivedQty -= outstandingOrder;
                                }
                                else
                                {
                                    entityPQPO.ReceivedQuantity += receivedQty;
                                    receivedQty = 0;
                                }
                            }
                            purchaseRequestPODao.Update(entityPQPO);
                        }
                    }
                }
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
            PurchaseReceiveDtDao entityDtDao = new PurchaseReceiveDtDao(ctx);
            PurchaseOrderDtDao entityPODtDao = new PurchaseOrderDtDao(ctx);
            PurchaseOrderHdDao entityPOHdDao = new PurchaseOrderHdDao(ctx);
            PurchaseRequestPODao purchaseRequestPODao = new PurchaseRequestPODao(ctx);
            PurchaseReceivePODao purchaseReceivePODao = new PurchaseReceivePODao(ctx);
            try
            {
                PurchaseReceiveDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                if (entityDt.GCItemDetailStatus == Constant.TransactionStatus.OPEN)
                {
                    entityDt.GCItemDetailStatus = Constant.TransactionStatus.VOID;
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Update(entityDt);
                    if (entityDt.PurchaseOrderID != null)
                    {
                        PurchaseOrderHd entityPOHd = entityPOHdDao.Get(Convert.ToInt32(entityDt.PurchaseOrderID));
                        entityPOHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                        entityPOHdDao.Update(entityPOHd);

                        PurchaseOrderDt entityPODt = BusinessLayer.GetPurchaseOrderDtList(String.Format("PurchaseOrderID = {0} AND ItemID = {1}", entityDt.PurchaseOrderID, entityDt.ItemID), ctx)[0];
                        entityPODt.ReceivedInformation = entityPODt.ReceivedInformation.Replace("|" + entityDt.PurchaseReceiveID + "|", "");
                        entityPODt.ReceivedQuantity -= entityDt.Quantity;
                        entityPODt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityPODtDao.Update(entityPODt);

                        PurchaseReceivePO entityPRPO = BusinessLayer.GetPurchaseReceivePOList(string.Format("PurchaseReceiveID = {0} AND ItemID = {1}", entityDt.PurchaseReceiveID, entityDt.ItemID), ctx).FirstOrDefault();
                        entityPRPO.ReceivedQuantity -= entityDt.Quantity;
                        purchaseReceivePODao.Update(entityPRPO);

                        List<PurchaseRequestPO> lstEntityPQPO = BusinessLayer.GetPurchaseRequestPOList(string.Format("PurchaseOrderID = {0} AND ItemID = {1}", entityPRPO.PurchaseOrderID, entityDt.ItemID), ctx);
                        decimal receivedQty = lstEntityPQPO.Sum(p => p.ReceivedQuantity) - entityDt.Quantity;
                        foreach (PurchaseRequestPO entityPQPO in lstEntityPQPO)
                        {
                            entityPQPO.ReceivedQuantity = 0;
                        }
                        foreach (PurchaseRequestPO entityPQPO in lstEntityPQPO)
                        {
                            if (receivedQty > 0)
                            {
                                decimal outstandingOrder = entityPQPO.OrderQuantity - entityPQPO.ReceivedQuantity;
                                if (receivedQty > outstandingOrder)
                                {
                                    entityPQPO.ReceivedQuantity += outstandingOrder;
                                    receivedQty -= outstandingOrder;
                                }
                                else
                                {
                                    entityPQPO.ReceivedQuantity += receivedQty;
                                    receivedQty = 0;
                                }
                            }
                            purchaseRequestPODao.Update(entityPQPO);
                        }
                    }
                }
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
    }
}