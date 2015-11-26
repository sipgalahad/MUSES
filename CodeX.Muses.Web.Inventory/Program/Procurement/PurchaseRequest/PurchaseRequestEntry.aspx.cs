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
    public partial class PurchaseRequestEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.PURCHASE_REQUEST;
        }

        #region Html Getter
        protected string OnGetItemQtyOnOrderFilterExpression()
        {
            return string.Format("SiteServiceUnitID = [SiteServiceUnitID] AND ItemID = [ItemID] AND GCTransactionStatus NOT IN ('{0}','{1}','{2}') AND IsDeleted = 0", Constant.TransactionStatus.CLOSED, Constant.TransactionStatus.PROCESSED, Constant.TransactionStatus.VOID);
        }
        protected string OnGetFilterExpressionLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.PURCHASE_REQUEST);
        }
        protected string OnGetFilterExpressionServiceUnit()
        {
            if (hdnListSiteServiceUnitID.Value != "")
                return string.Format("SiteServiceUnitID IN ({0}) AND IsDeleted = 0", hdnListSiteServiceUnitID.Value);
            return "1 = 0";
        }
        protected string OnGetFilterExpressionItemProduct()
        {
            return string.Format("GCItemType = '{0}' AND IsDeleted = 0", Constant.ItemType.PRODUCT);
        }
        protected string OnGetFilterExpressionSupplier()
        {
            return string.Format("GCBusinessPartnerType = '{0}' AND IsDeleted = 0", Constant.BusinessObjectType.SUPPLIER);
        }
        #endregion

        protected override void InitializeDataControl()
        {
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();

            SetControlProperties();
            hdnIsEditable.Value = "1";

            List<GetLocationUserList> lstUserLocation = BusinessLayer.GetLocationUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.PURCHASE_REQUEST, "");
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

            List<SettingParameter> lstSettingParameter = BusinessLayer.GetSettingParameterList(string.Format("ParameterCode IN ('{0}')", Constant.SettingParameter.NON_MASTER_ITEM));
            hdnNonMasterItemID.Value = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.NON_MASTER_ITEM).ParameterValue;

            List<StandardCode> listStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.ITEM_UNIT));
            Methods.SetComboBoxField<StandardCode>(cboNonMasterItemUnit, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.ITEM_UNIT).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");

            BindGridView(1, true, ref PageCount, ref RowCount);
            Helper.SetControlEntrySetting(txtNonMasterItemName, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(cboNonMasterItemUnit, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtQuantity, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtItemCode, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(cboItemUnit, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtPrice, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtDiscount, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnRequestID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtOrderNo, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblSiteServiceUnit, new ControlEntrySetting(true, false));
            SetControlEntrySetting(txtServiceUnitCode, new ControlEntrySetting(true, false, true, hdnDefaultServiceUnitCode.Value));
            SetControlEntrySetting(txtServiceUnitName, new ControlEntrySetting(false, false, true, hdnDefaultServiceUnitName.Value));
            SetControlEntrySetting(hdnSiteServiceUnitID, new ControlEntrySetting(false, false, false, hdnDefaultSiteServiceUnitID.Value));

            SetControlEntrySetting(txtItemOrderDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(txtItemOrderTime, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.TIME_FORMAT)));
            SetControlEntrySetting(txtNotes, new ControlEntrySetting(true, true, false));            
        }

        #region Load Entity
        public override void OnAddRecord()
        {
            hdnPageCount.Value = "0";
            hdnIsEditable.Value = "1";
        }

        protected string IsEditable()
        {
            return hdnIsEditable.Value;
        }
        protected string GetFilterExpression()
        {
            return hdnRecordFilterExpression.Value;
        }

        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvPurchaseRequestHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vPurchaseRequestHd entity = BusinessLayer.GetvPurchaseRequestHd(filterExpression, PageIndex, "PurchaseRequestID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvPurchaseRequestHdRowIndex(filterExpression, keyValue, "PurchaseRequestID DESC");
            vPurchaseRequestHd entity = BusinessLayer.GetvPurchaseRequestHd(filterExpression, PageIndex, "PurchaseRequestID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vPurchaseRequestHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                hdnIsEditable.Value = "0";
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
            }
            else
                hdnIsEditable.Value = "1";

            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN && entity.GCTransactionStatus != Constant.TransactionStatus.VOID)
                hdnPrintStatus.Value = "true";
            else
                hdnPrintStatus.Value = "false";

            hdnRequestID.Value = entity.PurchaseRequestID.ToString();
            txtOrderNo.Text = entity.PurchaseRequestNo;
            txtItemOrderDate.Text = entity.TransactionDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtItemOrderTime.Text = entity.TransactionTime;
            hdnSiteServiceUnitID.Value = entity.SiteServiceUnitID.ToString();
            txtServiceUnitCode.Text = entity.ServiceUnitCode;
            txtServiceUnitName.Text = entity.ServiceUnitName;
            txtNotes.Text = entity.Remarks;

            BindGridView(1, true, ref PageCount, ref RowCount);
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

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnRequestID.Value != "")
                filterExpression = string.Format("PurchaseRequestID = {0} AND IsDeleted = 0", hdnRequestID.Value);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvPurchaseRequestDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vPurchaseRequestDt> lstEntity = BusinessLayer.GetvPurchaseRequestDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ItemName1 ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
        #endregion

        #region Save Edit Header
        private void ControlToEntityHd(PurchaseRequestHd entityHd) 
        {
            entityHd.SiteServiceUnitID = Convert.ToInt32(hdnSiteServiceUnitID.Value);
            entityHd.TransactionDate = Helper.GetDatePickerValue(txtItemOrderDate.Text);
            entityHd.TransactionTime = txtItemOrderTime.Text;
            entityHd.Remarks = txtNotes.Text;
        }

        public void SavePurchaseRequestHd(IDbContext ctx, ref int OrderID)
        {
            PurchaseRequestHdDao entityHdDao = new PurchaseRequestHdDao(ctx);
            if (hdnRequestID.Value == "0")
            {
                PurchaseRequestHd entityHd = new PurchaseRequestHd();
                ControlToEntityHd(entityHd);
                entityHd.PurchaseRequestNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.PURCHASE_REQUEST, entityHd.TransactionDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();

                entityHd.CreatedBy = AppSession.UserLogin.UserID;

                entityHdDao.Insert(entityHd);

                OrderID = BusinessLayer.GetPurchaseRequestHdMaxID(ctx);
            }
            else
            {
                OrderID = Convert.ToInt32(hdnRequestID.Value);
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            try
            {
                int OrderID = 0;
                SavePurchaseRequestHd(ctx, ref OrderID);
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
                PurchaseRequestHd entity = BusinessLayer.GetPurchaseRequestHd(Convert.ToInt32(hdnRequestID.Value));
                ControlToEntityHd(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdatePurchaseRequestHd(entity);
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
            PurchaseRequestHdDao purchaseHdDao = new PurchaseRequestHdDao(ctx);
            PurchaseRequestDtDao purchaseDtDao = new PurchaseRequestDtDao(ctx);
            try
            {
                PurchaseRequestHd purchaseHd = purchaseHdDao.Get(Convert.ToInt32(hdnRequestID.Value));
                ControlToEntityHd(purchaseHd);
                purchaseHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                purchaseHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                purchaseHdDao.Update(purchaseHd);

                string filterExpressionPurchaseRequestHd = String.Format("PurchaseRequestID = {0} AND IsDeleted = 0", hdnRequestID.Value);
                List<PurchaseRequestDt> lstPurchaseRequestDt = BusinessLayer.GetPurchaseRequestDtList(filterExpressionPurchaseRequestHd, ctx);
                foreach (PurchaseRequestDt purchaseDt in lstPurchaseRequestDt)
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
            PurchaseRequestHdDao purchaseHdDao = new PurchaseRequestHdDao(ctx);
            PurchaseRequestDtDao purchaseDtDao = new PurchaseRequestDtDao(ctx);
            try
            {
                PurchaseRequestHd purchaseHd = purchaseHdDao.Get(Convert.ToInt32(hdnRequestID.Value));
                ControlToEntityHd(purchaseHd);
                purchaseHd.GCTransactionStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                purchaseHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                purchaseHdDao.Update(purchaseHd);

                string filterExpressionPurchaseRequestHd = String.Format("PurchaseRequestID = {0} AND IsDeleted = 0", hdnRequestID.Value);
                List<PurchaseRequestDt> lstPurchaseRequestDt = BusinessLayer.GetPurchaseRequestDtList(filterExpressionPurchaseRequestHd, ctx);
                foreach (PurchaseRequestDt purchaseDt in lstPurchaseRequestDt)
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
            PurchaseRequestHdDao purchaseHdDao = new PurchaseRequestHdDao(ctx);
            PurchaseRequestDtDao purchaseDtDao = new PurchaseRequestDtDao(ctx);
            try
            {
                PurchaseRequestHd purchaseHd = purchaseHdDao.Get(Convert.ToInt32(hdnRequestID.Value));
                purchaseHd.GCTransactionStatus = Constant.TransactionStatus.VOID;
                purchaseHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                purchaseHdDao.Update(purchaseHd);

                string filterExpressionPurchaseRequestHd = String.Format("PurchaseRequestID = {0} AND IsDeleted = 0", hdnRequestID.Value);
                List<PurchaseRequestDt> lstPurchaseRequestDt = BusinessLayer.GetPurchaseRequestDtList(filterExpressionPurchaseRequestHd, ctx);
                foreach (PurchaseRequestDt purchaseDt in lstPurchaseRequestDt)
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
            PurchaseRequestHdDao purchaseHdDao = new PurchaseRequestHdDao(ctx);
            PurchaseRequestDtDao purchaseDtDao = new PurchaseRequestDtDao(ctx);
            try
            {
                PurchaseRequestHd purchaseHd = purchaseHdDao.Get(Convert.ToInt32(hdnRequestID.Value));
                ControlToEntityHd(purchaseHd);
                purchaseHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                purchaseHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                purchaseHdDao.Update(purchaseHd);

                string filterExpressionPurchaseRequestHd = String.Format("PurchaseRequestID = {0} AND IsDeleted = 0", hdnRequestID.Value);
                List<PurchaseRequestDt> lstPurchaseRequestDt = BusinessLayer.GetPurchaseRequestDtList(filterExpressionPurchaseRequestHd, ctx);
                foreach (PurchaseRequestDt purchaseDt in lstPurchaseRequestDt)
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
            List<StandardCode> lst = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND (StandardCodeID IN (SELECT GCAlternateUnit FROM ItemAlternateUnit WHERE ItemID = {1}) OR StandardCodeID = (SELECT GCItemUnit FROM ItemMaster WHERE ItemID = {1}))", Constant.StandardCode.ITEM_UNIT, hdnItemID.Value));
            Methods.SetComboBoxField<StandardCode>(cboItemUnit, lst, "StandardCodeName", "StandardCodeID");
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
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount, ref rowCount);
                    result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
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
                    OrderID = Convert.ToInt32(hdnRequestID.Value);
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
                OrderID = Convert.ToInt32(hdnRequestID.Value);
                if (OnDeleteEntityDt(ref errMessage, OrderID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpOrderID"] = OrderID.ToString();
        }

        private void ControlToEntity(PurchaseRequestDt entityDt)
        {
            entityDt.ItemID = Convert.ToInt32(hdnItemID.Value);
            if (chkIsFromMasterItem.Checked)
            {
                entityDt.ItemName1 = null;
                entityDt.GCPurchaseUnit = cboItemUnit.Value.ToString();
                entityDt.GCBaseUnit = hdnGCBaseUnit.Value;
                entityDt.ConversionFactor = Convert.ToDecimal(hdnItemUnitValue.Value);
            }
            else
            {
                entityDt.ItemName1 = txtNonMasterItemName.Text;
                entityDt.GCPurchaseUnit = entityDt.GCBaseUnit = cboNonMasterItemUnit.Value.ToString();
                entityDt.ConversionFactor = 1;
            }
            entityDt.Quantity = Convert.ToDecimal(txtQuantity.Text);
            entityDt.Remarks = txtNotesDt.Text;
            if (hdnSupplierID.Value != "" && hdnSupplierID.Value != "0") { entityDt.BusinessPartnerID = Convert.ToInt32(hdnSupplierID.Value); }
            else entityDt.BusinessPartnerID = null;
            entityDt.UnitPrice = Convert.ToDecimal(txtPrice.Text);
            if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
            entityDt.DiscountPercentage = Convert.ToDecimal(txtDiscount.Text);
            entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int OrderID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseRequestDtDao entityDtDao = new PurchaseRequestDtDao(ctx);
            try
            {
                SavePurchaseRequestHd(ctx, ref OrderID);
                PurchaseRequestDt entityDt = new PurchaseRequestDt();
                ControlToEntity(entityDt);
                entityDt.PurchaseRequestID = OrderID;
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
            PurchaseRequestDtDao entityDtDao = new PurchaseRequestDtDao(ctx);
            try
            {
                PurchaseRequestDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
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
            PurchaseRequestDtDao entityDtDao = new PurchaseRequestDtDao(ctx);
            try
            {
                PurchaseRequestDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
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