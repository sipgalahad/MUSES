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
    public partial class PurchaseOrderEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.PURCHASE_ORDER;
        }

        #region Html Getter
        protected string OnGetItemQtyOnOrderFilterExpression()
        {
            return string.Format("SiteServiceUnitID = [SiteServiceUnitID] AND ItemID = [ItemID] AND GCTransactionStatus NOT IN ('{0}','{1}','{2}') AND IsDeleted = 0", Constant.TransactionStatus.CLOSED, Constant.TransactionStatus.PROCESSED, Constant.TransactionStatus.VOID);
        }
        protected string OnGetFilterExpressionServiceUnit()
        {
            if (hdnListSiteServiceUnitID.Value != "")
                return string.Format("SiteServiceUnitID IN ({0}) AND IsDeleted = 0", hdnListSiteServiceUnitID.Value);
            return "1 = 0";
        }
        protected string OnGetFilterExpressionLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.PURCHASE_ORDER);
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
            hdnVATPercentage.Value = BusinessLayer.GetSettingParameter(Constant.SettingParameter.VAT_PERCENTAGE).ParameterValue;

            List<GetLocationUserList> lstUserLocation = BusinessLayer.GetLocationUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.PURCHASE_ORDER, "");
            if (lstUserLocation.Count > 0)
            {
                List<ServiceUnitLocation> lstServiceUnitLocation = BusinessLayer.GetServiceUnitLocationList(string.Format("LocationID IN ({0})", string.Join(",", lstUserLocation.Select(p => p.LocationID).ToList())));
                hdnListSiteServiceUnitID.Value = string.Join(",", lstServiceUnitLocation.Select(p => p.SiteServiceUnitID).ToList());

                List<vSiteServiceUnit> lstSiteServiceUnit = BusinessLayer.GetvSiteServiceUnitList(OnGetFilterExpressionServiceUnit());
                if (lstSiteServiceUnit.Count == 1)
                {
                    vSiteServiceUnit serviceUnit = lstSiteServiceUnit.FirstOrDefault();
                    hdnDefaultSiteServiceUnitID.Value = serviceUnit.SiteServiceUnitID.ToString();
                    hdnDefaultServiceUnitCode.Value = serviceUnit.ServiceUnitCode;
                    hdnDefaultServiceUnitName.Value = serviceUnit.ServiceUnitName;

                    GetLocationItemGroupAndBindLocation(serviceUnit.SiteServiceUnitID);
                }
            }

            hdnRecordFilterExpression.Value = string.Format("SiteServiceUnitID IN ({0})", hdnListSiteServiceUnitID.Value);

            SetControlProperties();
            decimal tempTransactionAmount = -1;
            BindGridView(1, true, ref PageCount, ref RowCount, ref tempTransactionAmount);

            Helper.SetControlEntrySetting(txtQuantity, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtItemCode, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(cboItemUnit, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtNonMasterItemName, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(cboNonMasterItemUnit, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> listStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}','{2}','{3}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PURCHASE_ORDER_TYPE, Constant.StandardCode.FRANCO_REGION, Constant.StandardCode.CURRENCY_CODE, Constant.StandardCode.ITEM_UNIT));
            List<Term> listTerm = BusinessLayer.GetTermList(string.Format("IsDeleted = 0"));
            Methods.SetComboBoxField<StandardCode>(cboPurchaseOrderType, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.PURCHASE_ORDER_TYPE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboFrancoRegion, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.FRANCO_REGION).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboCurrency, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.CURRENCY_CODE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboNonMasterItemUnit, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.ITEM_UNIT).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<Term>(cboTerm, listTerm, "TermName", "TermID");
            cboPurchaseOrderType.SelectedIndex = 0;
            cboFrancoRegion.SelectedIndex = 0;
            cboCurrency.SelectedIndex = 0;
            List<SettingParameter> lstSettingParameter = BusinessLayer.GetSettingParameterList(string.Format("ParameterCode IN ('{0}')", Constant.SettingParameter.NON_MASTER_ITEM));
            hdnNonMasterItemID.Value = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.NON_MASTER_ITEM).ParameterValue;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnOrderID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtOrderNo, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtItemOrderDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(txtItemOrderDeliveryDate, new ControlEntrySetting(true, true, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(txtItemOrderExpiredDate, new ControlEntrySetting(true, true, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));

            SetControlEntrySetting(lblSupplier, new ControlEntrySetting(true, false));
            SetControlEntrySetting(txtSupplierCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtSupplierName, new ControlEntrySetting(false, false, true));

            SetControlEntrySetting(cboPurchaseOrderType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboTerm, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboFrancoRegion, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboCurrency, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtKurs, new ControlEntrySetting(true, true, true, "1"));
            SetControlEntrySetting(chkPPN, new ControlEntrySetting(true, true, false));

            SetControlEntrySetting(txtTransactionAmount, new ControlEntrySetting(false, false, true, "0"));
            SetControlEntrySetting(txtFinalDiscountPercentage, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(txtFinalDiscountAmount, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(txtPPN, new ControlEntrySetting(false, false, true, "0"));
            SetControlEntrySetting(txtDP, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(txtTotalNetTransactionAmount, new ControlEntrySetting(false, false, true, "0"));

            SetControlEntrySetting(lblSiteServiceUnit, new ControlEntrySetting(true, false));
            SetControlEntrySetting(txtServiceUnitCode, new ControlEntrySetting(true, false, true, hdnDefaultServiceUnitCode.Value));
            SetControlEntrySetting(txtServiceUnitName, new ControlEntrySetting(false, false, true, hdnDefaultServiceUnitName.Value));
            SetControlEntrySetting(hdnSiteServiceUnitID, new ControlEntrySetting(false, false, false, hdnDefaultSiteServiceUnitID.Value));
        }

        #region Load Entity
        public override void OnAddRecord()
        {
            hdnPageCount.Value = "0";
            hdnRowCount.Value = "0";
            hdnIsEditable.Value = "1";
            BindLocation();
        }

        protected string IsEditable()
        {
            return hdnIsEditable.Value;
        }

        protected string GetFilterExpression()
        {
            string filterExpression = hdnRecordFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("TransactionCode = '{0}'", Constant.TransactionCode.PURCHASE_ORDER);
            return filterExpression;
        }

        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvPurchaseOrderHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vPurchaseOrderHd entity = BusinessLayer.GetvPurchaseOrderHd(filterExpression, PageIndex, "PurchaseOrderID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvPurchaseOrderHdRowIndex(filterExpression, keyValue, "PurchaseOrderID DESC");
            vPurchaseOrderHd entity = BusinessLayer.GetvPurchaseOrderHd(filterExpression, PageIndex, "PurchaseOrderID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vPurchaseOrderHd entity, ref bool isShowWatermark, ref string watermarkText)
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

            hdnOrderID.Value = entity.PurchaseOrderID.ToString();
            txtOrderNo.Text = entity.PurchaseOrderNo;
            txtItemOrderDate.Text = entity.OrderDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtItemOrderDeliveryDate.Text = entity.DeliveryDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtItemOrderExpiredDate.Text = entity.POExpiredDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            hdnSupplierID.Value = entity.BusinessPartnerID.ToString();
            txtSupplierCode.Text = entity.BusinessPartnerCode;
            txtSupplierName.Text = entity.BusinessPartnerName;
            hdnSiteServiceUnitID.Value = entity.SiteServiceUnitID.ToString();
            txtServiceUnitCode.Text = entity.ServiceUnitCode;
            txtServiceUnitName.Text = entity.ServiceUnitName;
            cboPurchaseOrderType.Value = entity.GCPurchaseOrderType;
            cboTerm.Value = entity.TermID.ToString();
            txtPaymentRemarks.Text = entity.PaymentRemarks;
            txtNotes.Text = entity.Remarks;
            cboFrancoRegion.Value = entity.GCFrancoRegion.ToString();
            cboCurrency.Value = entity.GCCurrencyCode.ToString();
            txtKurs.Text = entity.CurrencyRate.ToString();
            chkPPN.Checked = entity.IsIncludeVAT;
            txtFinalDiscountPercentage.Text = entity.FinalDiscountPercentage.ToString();
            txtFinalDiscountAmount.Text = entity.FinalDiscountAmount.ToString();
            txtTransactionAmount.Text = entity.TransactionAmount.ToString();

            decimal tempTransactionAmount = -1;
            BindGridView(1, true, ref PageCount, ref RowCount, ref tempTransactionAmount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
            GetLocationItemGroupAndBindLocation(entity.SiteServiceUnitID);
        }

        private void GetLocationItemGroupAndBindLocation(int SiteServiceUnitID)
        {
            string filterExpression = string.Format("{0}LocationID IN (SELECT LocationID FROM ServiceUnitLocation WHERE SiteServiceUnitID = {1})", OnGetFilterExpressionLocation(), SiteServiceUnitID);
            List<GetLocationUserList> lstLocation = BusinessLayer.GetLocationUserAccessList(filterExpression);
            string lstLocationID = String.Join(",", lstLocation.Select(p => p.LocationID).ToList());
            if (lstLocationID != "")
            {
                filterExpression = string.Format("LocationID IN ({0})", lstLocationID);
                List<LocationItemGroup> lstLocationItemGroup = BusinessLayer.GetLocationItemGroupList(filterExpression);
                string filterLocationItemGroup = String.Join(" OR ", lstLocationItemGroup.Select(p => string.Format("DisplayPath LIKE '%/{0}/%'", p.ItemGroupID)).ToList());
                if (filterLocationItemGroup != "")
                    hdnLstFilterLocationItemGroup.Value = string.Format("({0})", filterLocationItemGroup);
                else
                    hdnLstFilterLocationItemGroup.Value = "";
            }
            else
                hdnLstFilterLocationItemGroup.Value = "";
            BindLocation();
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount, ref decimal transactionAmount)
        {
            string filterExpression = "1 = 0";
            if (hdnOrderID.Value != "" && hdnOrderID.Value != "0")
                filterExpression = string.Format("PurchaseOrderID = {0} AND IsDeleted = 0", hdnOrderID.Value);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvPurchaseOrderDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            if (transactionAmount > -1)
                transactionAmount = BusinessLayer.GetPurchaseOrderHd(Convert.ToInt32(hdnOrderID.Value)).TransactionAmount;

            List<vPurchaseOrderDt> lstEntity = BusinessLayer.GetvPurchaseOrderDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ItemName1 ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
        #endregion

        #region Save Edit Header
        private void ControlToEntityHd(PurchaseOrderHd entity)
        {
            entity.DeliveryDate = Helper.GetDatePickerValue(txtItemOrderDeliveryDate.Text);
            entity.POExpiredDate = Helper.GetDatePickerValue(txtItemOrderExpiredDate.Text);
            entity.BusinessPartnerID = Convert.ToInt32(hdnSupplierID.Value);
            entity.PaymentRemarks = txtPaymentRemarks.Text;
            entity.Remarks = txtNotes.Text;
            entity.IsIncludeVAT = chkPPN.Checked;
            entity.FinalDiscountPercentage = Convert.ToDecimal(Request.Form[txtFinalDiscountPercentage.UniqueID]);
            entity.FinalDiscountAmount = Convert.ToDecimal(Request.Form[txtFinalDiscountAmount.UniqueID]);
            if (entity.IsIncludeVAT)
                entity.VATPercentage = Convert.ToDecimal(hdnVATPercentage.Value);
            else
                entity.VATPercentage = 0;
            entity.VATAmount = Convert.ToDecimal(Request.Form[txtPPN.UniqueID]);

            entity.OrderDate = Helper.GetDatePickerValue(txtItemOrderDate.Text);
            entity.GCPurchaseOrderType = cboPurchaseOrderType.Value.ToString();
            entity.TermID = Convert.ToInt32(cboTerm.Value.ToString());
            entity.GCFrancoRegion = cboFrancoRegion.Value.ToString();
            entity.GCCurrencyCode = cboCurrency.Value.ToString();
            entity.CurrencyRate = Convert.ToDecimal(txtKurs.Text);
            entity.DownPaymentAmount = Convert.ToDecimal(txtDP.Text);
            entity.SiteServiceUnitID = Convert.ToInt32(hdnSiteServiceUnitID.Value);
            entity.TotalNetTransactionAmount = entity.TransactionAmount + entity.VATAmount - entity.FinalDiscountAmount - entity.DownPaymentAmount;
        }

        public void SavePurchaseOrderHd(IDbContext ctx, ref int OrderID)
        {
            PurchaseOrderHdDao entityHdDao = new PurchaseOrderHdDao(ctx);
            if (hdnOrderID.Value == "0")
            {
                PurchaseOrderHd entityHd = new PurchaseOrderHd();
                ControlToEntityHd(entityHd);
                entityHd.TransactionCode = Constant.TransactionCode.PURCHASE_ORDER;
                entityHd.PurchaseOrderNo = BusinessLayer.GenerateTransactionNo(entityHd.TransactionCode, entityHd.OrderDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);
                OrderID = BusinessLayer.GetPurchaseOrderHdMaxID(ctx);
            }
            else
            {
                OrderID = Convert.ToInt32(hdnOrderID.Value);
                PurchaseOrderHd entityHd = entityHdDao.Get(OrderID);
                ControlToEntityHd(entityHd);
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
                int OrderID = 0;
                SavePurchaseOrderHd(ctx, ref OrderID);
                retval = OrderID.ToString();
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
            try
            {
                PurchaseOrderHd entity = BusinessLayer.GetPurchaseOrderHd(Convert.ToInt32(hdnOrderID.Value));
                ControlToEntityHd(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdatePurchaseOrderHd(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        protected override bool OnApproveRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseOrderHdDao purchaseHdDao = new PurchaseOrderHdDao(ctx);
            PurchaseOrderDtDao purchaseDtDao = new PurchaseOrderDtDao(ctx);
            try
            {
                PurchaseOrderHd purchaseHd = purchaseHdDao.Get(Convert.ToInt32(hdnOrderID.Value));
                ControlToEntityHd(purchaseHd);
                purchaseHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                purchaseHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                purchaseHdDao.Update(purchaseHd);

                string filterExpressionPurchaseOrderHd = String.Format("PurchaseOrderID = {0} AND IsDeleted = 0", hdnOrderID.Value);
                List<PurchaseOrderDt> lstPurchaseOrderDt = BusinessLayer.GetPurchaseOrderDtList(filterExpressionPurchaseOrderHd, ctx);
                foreach (PurchaseOrderDt purchaseDt in lstPurchaseOrderDt)
                {
                    purchaseDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
                    purchaseDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    purchaseDtDao.Update(purchaseDt);
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

        protected override bool OnProposeRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseOrderHdDao purchaseHdDao = new PurchaseOrderHdDao(ctx);
            PurchaseOrderDtDao purchaseDtDao = new PurchaseOrderDtDao(ctx);
            try
            {
                PurchaseOrderHd purchaseHd = purchaseHdDao.Get(Convert.ToInt32(hdnOrderID.Value));
                ControlToEntityHd(purchaseHd);
                purchaseHd.GCTransactionStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                purchaseHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                purchaseHdDao.Update(purchaseHd);

                string filterExpressionPurchaseOrderHd = String.Format("PurchaseOrderID = {0} AND IsDeleted = 0", hdnOrderID.Value);
                List<PurchaseOrderDt> lstPurchaseOrderDt = BusinessLayer.GetPurchaseOrderDtList(filterExpressionPurchaseOrderHd, ctx);
                foreach (PurchaseOrderDt purchaseDt in lstPurchaseOrderDt)
                {
                    purchaseDt.GCItemDetailStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                    purchaseDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    purchaseDtDao.Update(purchaseDt);
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
            PurchaseOrderHdDao purchaseHdDao = new PurchaseOrderHdDao(ctx);
            PurchaseOrderDtDao purchaseDtDao = new PurchaseOrderDtDao(ctx);
            try
            {
                PurchaseOrderHd purchaseHd = purchaseHdDao.Get(Convert.ToInt32(hdnOrderID.Value));
                purchaseHd.GCTransactionStatus = Constant.TransactionStatus.VOID;
                purchaseHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                purchaseHdDao.Update(purchaseHd);

                string filterExpressionPurchaseOrderHd = String.Format("PurchaseOrderID = {0} AND IsDeleted = 0", hdnOrderID.Value);
                List<PurchaseOrderDt> lstPurchaseOrderDt = BusinessLayer.GetPurchaseOrderDtList(filterExpressionPurchaseOrderHd, ctx);
                foreach (PurchaseOrderDt purchaseDt in lstPurchaseOrderDt)
                {
                    purchaseDt.GCItemDetailStatus = Constant.TransactionStatus.VOID;
                    purchaseDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    purchaseDtDao.Update(purchaseDt);
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
            PurchaseOrderHdDao purchaseHdDao = new PurchaseOrderHdDao(ctx);
            PurchaseOrderDtDao purchaseDtDao = new PurchaseOrderDtDao(ctx);
            try
            {
                PurchaseOrderHd purchaseHd = purchaseHdDao.Get(Convert.ToInt32(hdnOrderID.Value));
                ControlToEntityHd(purchaseHd);
                purchaseHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                purchaseHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                purchaseHdDao.Update(purchaseHd);

                string filterExpressionPurchaseOrderHd = String.Format("PurchaseOrderID = {0} AND IsDeleted = 0", hdnOrderID.Value);
                List<PurchaseOrderDt> lstPurchaseOrderDt = BusinessLayer.GetPurchaseOrderDtList(filterExpressionPurchaseOrderHd, ctx);
                foreach (PurchaseOrderDt purchaseDt in lstPurchaseOrderDt)
                {
                    purchaseDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                    purchaseDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    purchaseDtDao.Update(purchaseDt);
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
        protected void cboItemUnit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            List<vItemAlternateUnitCustom> lst = BusinessLayer.GetvItemAlternateUnitCustomList(string.Format("ItemID = {0}", hdnItemID.Value));
            Methods.SetComboBoxField<vItemAlternateUnitCustom>(cboItemUnit, lst, "cfAlternateUnit", "cfID");
            cboItemUnit.SelectedIndex = -1;
        }

        private void BindLocation()
        {
            Repeater rptLocation = (Repeater)ddeLocation.FindControl("rptLocation");
            string filterExpression = "1 = 0";
            if (hdnLstFilterLocationItemGroup.Value != "")
                filterExpression = string.Format("LocationID IN (SELECT LocationID FROM vLocationItemGroupPath WHERE {0}) AND IsDeleted = 0", hdnLstFilterLocationItemGroup.Value);
            List<Location> lstLocation = BusinessLayer.GetLocationList(filterExpression);
            rptLocation.DataSource = lstLocation;
            rptLocation.DataBind();
        }

        protected void cbpLocation_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindLocation();
        }

        protected void rptLocation_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Location obj = (Location)e.Item.DataItem;
                CheckBox chkLocation = (CheckBox)e.Item.FindControl("chkLocation");
                chkLocation.Checked = obj.IsControlQtyOnOrder;
                chkLocation.Attributes.Add("locationname", obj.LocationName);
                chkLocation.Attributes.Add("locationid", obj.LocationID.ToString());
            }
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
            int OrderID = 0;
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    OrderID = Convert.ToInt32(hdnOrderID.Value);
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage, ref OrderID))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                OrderID = Convert.ToInt32(hdnOrderID.Value);
                if (OnDeleteEntityDt(ref errMessage, OrderID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpOrderID"] = OrderID.ToString();
        }

        private void ControlToEntity(PurchaseOrderDt entityDt)
        {
            entityDt.ItemID = Convert.ToInt32(hdnItemID.Value);
            entityDt.Quantity = Convert.ToDecimal(txtQuantity.Text);

            if (chkIsFromMasterItem.Checked)
            {
                entityDt.ItemName1 = null;
                entityDt.GCPurchaseUnit = cboItemUnit.Value.ToString().Split('|')[0];
                entityDt.GCBaseUnit = hdnGCBaseUnit.Value;
                entityDt.ConversionFactor = Convert.ToDecimal(hdnConversionFactor.Value);
            }
            else
            {
                entityDt.ItemName1 = txtNonMasterItemName.Text;
                entityDt.GCPurchaseUnit = entityDt.GCBaseUnit = cboNonMasterItemUnit.Value.ToString();
                entityDt.ConversionFactor = 1;
            }
            entityDt.UnitPrice = Convert.ToDecimal(txtPrice.Text);
            entityDt.DiscountPercentage1 = Convert.ToDecimal(txtDiscountPercentage1.Text);
            entityDt.DiscountAmount1 = Convert.ToDecimal(txtDiscountAmount1.Text);
            entityDt.DiscountPercentage2 = Convert.ToDecimal(txtDiscountPercentage2.Text);
            entityDt.DiscountAmount2 = Convert.ToDecimal(txtDiscountAmount2.Text);
            entityDt.Remarks = txtNotesDt.Text;
            entityDt.LineAmount = Convert.ToDecimal(Request.Form[txtLineAmount.UniqueID]);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int OrderID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseOrderDtDao entityDtDao = new PurchaseOrderDtDao(ctx);
            try
            {
                SavePurchaseOrderHd(ctx, ref OrderID);
                PurchaseOrderDt entityDt = new PurchaseOrderDt();
                ControlToEntity(entityDt);
                entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                entityDt.PurchaseOrderID = OrderID;
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
            PurchaseOrderDtDao entityDtDao = new PurchaseOrderDtDao(ctx);
            try
            {
                PurchaseOrderDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
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
            PurchaseOrderDtDao entityDtDao = new PurchaseOrderDtDao(ctx);
            PurchaseRequestDtDao entityPRDtDao = new PurchaseRequestDtDao(ctx);
            PurchaseRequestHdDao entityPRHdDao = new PurchaseRequestHdDao(ctx);
            try
            {
                PurchaseOrderDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                if (entityDt.PurchaseRequestID != null)
                {
                    PurchaseRequestDt entityPRDt = BusinessLayer.GetPurchaseRequestDtList(String.Format("PurchaseRequestID = {0} AND ItemID = {1}", entityDt.PurchaseRequestID, entityDt.ItemID), ctx)[0];
                    entityPRDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                    entityPRDtDao.Update(entityPRDt);

                    PurchaseRequestHd entityPRHd = entityPRHdDao.Get(entityPRDt.PurchaseRequestID);
                    if (entityPRHd.GCTransactionStatus == Constant.TransactionStatus.CLOSED) entityPRHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                    else if (entityPRHd.GCTransactionStatus == Constant.TransactionStatus.APPROVED) entityPRHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                    entityPRHdDao.Update(entityPRHd);
                }

                entityDt.IsDeleted = true;
                entityDt.GCItemDetailStatus = Constant.TransactionStatus.VOID;
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
    }
}