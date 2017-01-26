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
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class ItemRequestOutstandingDetail : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        private string[] lstSelectedMember = null;
        private string[] lstDistribution = null;
        private string[] lstDistributionGCItemUnit = null;
        private string[] lstDistributionItemUnit = null;
        private string[] lstDistributionConversionFactor = null;
        private string[] lstConsumption = null;
        private string[] lstConsumptionGCItemUnit = null;
        private string[] lstConsumptionItemUnit = null;
        private string[] lstConsumptionConversionFactor = null;
        private string[] lstPurchaseRequest = null;
        private string[] lstPurchaseRequestGCItemUnit = null;
        private string[] lstPurchaseRequestItemUnit = null;
        private string[] lstPurchaseRequestConversionFactor = null;

        public override string OnGetMenuCode()
        {
            if (Page.Request.QueryString.Count > 0 && Page.Request.QueryString["type"] == "cs")
                return Constant.MenuCode.Inventory.APPROVED_ITEM_REQUEST_CROSS_SITE;
            return Constant.MenuCode.Inventory.APPROVED_ITEM_REQUEST;
        }
        protected string OnGetFilterExpressionToLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, "");
        }
        protected string OnGetRestrictionTransactionCodeFilterExpression()
        {
            return string.Format("RestrictionID = [RestrictionID] AND TransactionCode IN ('{0}','{1}')", Constant.TransactionCode.ITEM_DISTRIBUTION, Constant.TransactionCode.ITEM_CONSUMPTION);
        }
        protected string OnGetTransactionCodePurchaseRequest()
        {
            return Constant.TransactionCode.PURCHASE_REQUEST;
        }
        protected string OnGetTransactionCodeItemDistribution()
        {
            return Constant.TransactionCode.ITEM_DISTRIBUTION;
        }
        protected string OnGetTransactionCodeItemConsumption()
        {
            return Constant.TransactionCode.ITEM_CONSUMPTION;
        }

        public override void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowEdit = IsAllowDelete = false;
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowAdd = false;
        }

        protected override void InitializeDataControl()
        {
            hdnOrderID.Value = Page.Request.QueryString["id"];
            vItemRequestHd entityItemRequest = BusinessLayer.GetvItemRequestHdList(string.Format("ItemRequestID = {0}", hdnOrderID.Value))[0];

            string filterExpression = string.Format("LocationID IN (SELECT LocationID FROM ServiceUnitLocation WHERE SiteServiceUnitID = {0})", entityItemRequest.ToSiteServiceUnitID);
            List<GetLocationUserList> lstUserLocation = BusinessLayer.GetLocationUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.ITEM_DISTRIBUTION, filterExpression);
            hdnLstLocationID.Value = string.Join(",", lstUserLocation.Select(p => p.LocationID).ToList());
            
            if (lstUserLocation.Count == 1)
            {
                GetLocationUserList location = lstUserLocation.FirstOrDefault();
                hdnToLocationID.Value = location.LocationID.ToString();
                txtToLocationCode.Text = location.LocationCode;
                txtToLocationName.Text = location.LocationName;
                hdnRestrictionID.Value = location.RestrictionID.ToString();

                List<LocationItemGroup> lstLocationItemGroup = BusinessLayer.GetLocationItemGroupList(string.Format("LocationID = {0}", location.LocationID));
                if (lstLocationItemGroup.Count > 0)
                    hdnLstFilterToLocationItemGroup.Value = string.Format("({0})", String.Join(" OR ", lstLocationItemGroup.Select(p => string.Format("DisplayPath LIKE '%/{0}/%'", p.ItemGroupID))));
                else
                    hdnLstFilterToLocationItemGroup.Value = "";

                filterExpression = OnGetRestrictionTransactionCodeFilterExpression();
                filterExpression.Replace("[RestrictionID]", hdnRestrictionID.Value);

                bool isAllowItemConsumption = false;
                bool isAllowItemDistribution = false;
                List<RestrictionDt> lstRestrictionDt = BusinessLayer.GetRestrictionDtList(filterExpression);
                foreach (RestrictionDt restrictionDt in lstRestrictionDt)
                {
                    if (restrictionDt.TransactionCode == OnGetTransactionCodeItemDistribution())
                        isAllowItemDistribution = true;
                    else if (restrictionDt.TransactionCode == OnGetTransactionCodeItemConsumption())
                        isAllowItemConsumption = true;
                }

                hdnIsAllowItemDistribution.Value = isAllowItemDistribution ? "1" : "0";
                hdnIsAllowItemConsumption.Value = isAllowItemConsumption ? "1" : "0";
            }

            filterExpression = string.Format("LocationID IN (SELECT LocationID FROM ServiceUnitLocation WHERE SiteServiceUnitID = {0})", entityItemRequest.ToSiteServiceUnitID);
            lstUserLocation = BusinessLayer.GetLocationUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.PURCHASE_REQUEST, filterExpression);
            hdnIsAllowPurchaseRequest.Value = lstUserLocation.Count > 0 ? "1" : "0";

            EntityToControl(entityItemRequest);

            List<StandardCode> lstGCConsumptionType = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.CONSUMPTION_TYPE));
            StandardCode GCConsumptionType = lstGCConsumptionType.FirstOrDefault(p => p.IsDefault);
            if (GCConsumptionType == null)
                GCConsumptionType = lstGCConsumptionType.FirstOrDefault();
            hdnDefaultGCConsumptionType.Value = GCConsumptionType.StandardCodeID;
        }

        private void EntityToControl(vItemRequestHd entity)
        {
            hdnOrderID.Value = entity.ItemRequestID.ToString();
            txtOrderNo.Text = entity.ItemRequestNo;
            txtItemOrderDate.Text = entity.TransactionDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtItemOrderTime.Text = entity.TransactionTime;

            hdnFromSiteServiceUnitID.Value = entity.FromSiteServiceUnitID.ToString();
            txtFromServiceUnitCode.Text = entity.FromServiceUnitCode;
            txtFromServiceUnitName.Text = entity.FromServiceUnitName;

            hdnFromLocationID.Value = entity.FromLocationID.ToString();
            txtFromLocationCode.Text = entity.FromLocationCode;
            txtFromLocationName.Text = entity.FromLocationName;
            hdnToSiteServiceUnitID.Value = entity.ToSiteServiceUnitID.ToString();
            txtToServiceUnitCode.Text = entity.ToServiceUnitCode;
            txtToServiceUnitName.Text = entity.ToServiceUnitName;
            txtNotes.Text = entity.Remarks;

            List<vItemRequestDt> lstEntityDt = BusinessLayer.GetvItemRequestDtList(string.Format("ItemRequestID = {0} AND GCItemDetailStatus = '{1}' AND IsSelected = 1 AND IsDeleted = 0", hdnOrderID.Value, Constant.TransactionStatus.APPROVED));
            if (lstEntityDt.Count > 0)
            {
                hdnSelectedMember.Value = string.Format(",{0}", string.Join(",", lstEntityDt.Select(p => p.ID).ToList()));
                hdnParamDistribution.Value = string.Format(",{0}", string.Join(",", lstEntityDt.Select(p => p.DistributionQty).ToList()));
                hdnParamDistributionGCItemUnit.Value = string.Format(",{0}", string.Join(",", lstEntityDt.Select(p => p.GCDistributionItemUnit).ToList()));
                hdnParamDistributionItemUnit.Value = string.Format(",{0}", string.Join(",", lstEntityDt.Select(p => p.DistributionItemUnit).ToList()));
                hdnParamDistributionConversionFactor.Value = string.Format(",{0}", string.Join(",", lstEntityDt.Select(p => p.DistributionConversionFactor).ToList()));
                hdnParamConsumption.Value = string.Format(",{0}", string.Join(",", lstEntityDt.Select(p => p.ConsumptionQty).ToList()));
                hdnParamConsumptionGCItemUnit.Value = string.Format(",{0}", string.Join(",", lstEntityDt.Select(p => p.GCConsumptionItemUnit).ToList()));
                hdnParamConsumptionItemUnit.Value = string.Format(",{0}", string.Join(",", lstEntityDt.Select(p => p.ConsumptionItemUnit).ToList()));
                hdnParamConsumptionConversionFactor.Value = string.Format(",{0}", string.Join(",", lstEntityDt.Select(p => p.ConsumptionConversionFactor).ToList()));
                hdnParamPurchaseRequest.Value = string.Format(",{0}", string.Join(",", lstEntityDt.Select(p => p.PurchaseRequestQty).ToList()));
                hdnParamPurchaseRequestGCItemUnit.Value = string.Format(",{0}", string.Join(",", lstEntityDt.Select(p => p.GCPurchaseRequestItemUnit).ToList()));
                hdnParamPurchaseRequestItemUnit.Value = string.Format(",{0}", string.Join(",", lstEntityDt.Select(p => p.PurchaseRequestItemUnit).ToList()));
                hdnParamPurchaseRequestConversionFactor.Value = string.Format(",{0}", string.Join(",", lstEntityDt.Select(p => p.PurchaseRequestConversionFactor).ToList()));
            }


            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnOrderID.Value != "")
            {
                filterExpression = string.Format("ItemRequestID = {0} AND GCItemDetailStatus = '{1}' AND IsDeleted = 0", hdnOrderID.Value, Constant.TransactionStatus.APPROVED);
                if (hdnLstFilterToLocationItemGroup.Value != "")
                    filterExpression += string.Format(" AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE {0})", hdnLstFilterToLocationItemGroup.Value);
            }

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvItemRequestDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            lstDistribution = hdnParamDistribution.Value.Split(',');
            lstDistributionGCItemUnit = hdnParamDistributionGCItemUnit.Value.Split(',');
            lstDistributionItemUnit = hdnParamDistributionItemUnit.Value.Split(',');
            lstDistributionConversionFactor = hdnParamDistributionConversionFactor.Value.Split(',');
            lstConsumption = hdnParamConsumption.Value.Split(',');
            lstConsumptionGCItemUnit = hdnParamConsumptionGCItemUnit.Value.Split(',');
            lstConsumptionItemUnit = hdnParamConsumptionItemUnit.Value.Split(',');
            lstConsumptionConversionFactor = hdnParamConsumptionConversionFactor.Value.Split(',');
            lstPurchaseRequest = hdnParamPurchaseRequest.Value.Split(',');
            lstPurchaseRequestGCItemUnit = hdnParamPurchaseRequestGCItemUnit.Value.Split(',');
            lstPurchaseRequestItemUnit = hdnParamPurchaseRequestItemUnit.Value.Split(',');
            lstPurchaseRequestConversionFactor = hdnParamPurchaseRequestConversionFactor.Value.Split(',');
            List<vItemRequestDt> lstEntity = BusinessLayer.GetvItemRequestDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ItemName1 ASC");

            string lsItemID = string.Join(",", lstEntity.Select(p => p.ItemID).ToList());
            if (lsItemID != "")
                lstItemRequestDtRealizationPerItem = BusinessLayer.GetvItemRequestDtRealizationPerItemList(string.Format("ItemID IN ({0})", lsItemID));
            else
                lstItemRequestDtRealizationPerItem = new List<vItemRequestDtRealizationPerItem>();

            if (lsItemID != "" && hdnToLocationID.Value != "")
                lstItemBalance = BusinessLayer.GetItemBalanceList(string.Format("LocationID = {0} AND ItemID IN ({1})", hdnToLocationID.Value, lsItemID));
            else
                lstItemBalance = new List<ItemBalance>();
            if (lsItemID != "" && hdnFromLocationID.Value != "")
                lstFromItemBalance = BusinessLayer.GetItemBalanceList(string.Format("LocationID = {0} AND ItemID IN ({1})", hdnFromLocationID.Value, lsItemID));
            else
                lstFromItemBalance = new List<ItemBalance>();

            lvwView.DataSource = lstEntity;
            lvwView.DataBind();
        }

        List<vItemRequestDtRealizationPerItem> lstItemRequestDtRealizationPerItem = null;
        List<ItemBalance> lstItemBalance = null;
        List<ItemBalance> lstFromItemBalance = null;
        protected void lvwView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                vItemRequestDt entity = e.Item.DataItem as vItemRequestDt;
                CheckBox chkIsSelected = (CheckBox)e.Item.FindControl("chkIsSelected");
                TextBox txtDistribution = (TextBox)e.Item.FindControl("txtDistribution");
                TextBox txtPurchaseRequest = (TextBox)e.Item.FindControl("txtPurchaseRequest");
                TextBox txtConsumption = (TextBox)e.Item.FindControl("txtConsumption");
                HtmlGenericControl lblAvailableStock = (HtmlGenericControl)e.Item.FindControl("lblAvailableStock");
                HtmlGenericControl lblEndingBalance = (HtmlGenericControl)e.Item.FindControl("lblEndingBalance");

                HtmlGenericControl lblDistributionItemUnit = (HtmlGenericControl)e.Item.FindControl("lblDistributionItemUnit");
                HtmlGenericControl lblConsumptionItemUnit = (HtmlGenericControl)e.Item.FindControl("lblConsumptionItemUnit");
                HtmlGenericControl lblPurchaseRequestItemUnit = (HtmlGenericControl)e.Item.FindControl("lblPurchaseRequestItemUnit");

                HtmlInputHidden hdnGCDistributionItemUnit = (HtmlInputHidden)e.Item.FindControl("hdnGCDistributionItemUnit");
                HtmlInputHidden hdnDistributionItemUnit = (HtmlInputHidden)e.Item.FindControl("hdnDistributionItemUnit");
                HtmlInputHidden hdnDistributionConversionFactor = (HtmlInputHidden)e.Item.FindControl("hdnDistributionConversionFactor");
                HtmlInputHidden hdnGCConsumptionItemUnit = (HtmlInputHidden)e.Item.FindControl("hdnGCConsumptionItemUnit");
                HtmlInputHidden hdnConsumptionItemUnit = (HtmlInputHidden)e.Item.FindControl("hdnConsumptionItemUnit");
                HtmlInputHidden hdnConsumptionConversionFactor = (HtmlInputHidden)e.Item.FindControl("hdnConsumptionConversionFactor");
                HtmlInputHidden hdnGCPurchaseRequestItemUnit = (HtmlInputHidden)e.Item.FindControl("hdnGCPurchaseRequestItemUnit");
                HtmlInputHidden hdnPurchaseRequestItemUnit = (HtmlInputHidden)e.Item.FindControl("hdnPurchaseRequestItemUnit");
                HtmlInputHidden hdnPurchaseRequestConversionFactor = (HtmlInputHidden)e.Item.FindControl("hdnPurchaseRequestConversionFactor");
                HtmlInputHidden hdnQuantityEND = (HtmlInputHidden)e.Item.FindControl("hdnQuantityEND");

                decimal endingBalance = lstItemBalance.Where(p => p.ItemID == entity.ItemID).Sum(p => p.QuantityEND);
                lblEndingBalance.InnerHtml = endingBalance.ToString("0.00");
                hdnQuantityEND.Value = endingBalance.ToString();

                ItemBalance itemBalanceFrom = lstFromItemBalance.FirstOrDefault(p => p.ItemID == entity.ItemID);

                decimal availableQty = 0;
                vItemRequestDtRealizationPerItem itemRequestDtRealizationPerItem = lstItemRequestDtRealizationPerItem.FirstOrDefault(p => p.ItemID == entity.ItemID);
                if (itemRequestDtRealizationPerItem != null)
                {
                    availableQty = endingBalance - itemRequestDtRealizationPerItem.ItemRequestQuantity;
                    if (entity.PurchaseRequestQty > 0)
                        availableQty += entity.Quantity;
                }
                else
                    availableQty = endingBalance;
                if (availableQty < 0)
                    availableQty = 0;
                lblAvailableStock.InnerHtml = availableQty.ToString();

                Helper.SetControlEntrySetting(txtDistribution, new ControlEntrySetting(true, true, true), "mpEntry");
                Helper.SetControlEntrySetting(txtPurchaseRequest, new ControlEntrySetting(true, true, true), "mpEntry");
                Helper.SetControlEntrySetting(txtConsumption, new ControlEntrySetting(true, true, true), "mpEntry");

                decimal dist = Math.Floor(endingBalance / entity.ConversionFactor);
                if (entity.Quantity * entity.ConversionFactor > endingBalance)
                {
                    if (itemBalanceFrom != null && itemBalanceFrom.GCDistributionType == Constant.DistributionType.CONSUMPTION)
                        txtConsumption.Text = dist.ToString();
                    else
                        txtDistribution.Text = dist.ToString();
                    if (hdnIsAllowPurchaseRequest.Value == "1" && entity.PurchaseRequestQty == 0)
                        txtPurchaseRequest.Text = (entity.Quantity - dist).ToString();
                }
                else
                {
                    if (itemBalanceFrom != null && itemBalanceFrom.GCDistributionType == Constant.DistributionType.CONSUMPTION)
                        txtConsumption.Text = entity.Quantity.ToString();
                    else
                        txtDistribution.Text = entity.Quantity.ToString();
                }
                txtConsumption.Attributes.Add("max", dist.ToString());
                txtDistribution.Attributes.Add("max", dist.ToString());

                lblDistributionItemUnit.InnerHtml = entity.cfDistributionItemUnit;
                lblConsumptionItemUnit.InnerHtml = entity.cfConsumptionItemUnit;
                lblPurchaseRequestItemUnit.InnerHtml = entity.cfPurchaseRequestItemUnit;
                hdnGCDistributionItemUnit.Value = entity.GCDistributionItemUnit;
                hdnDistributionItemUnit.Value = entity.DistributionItemUnit;
                hdnDistributionConversionFactor.Value = entity.DistributionConversionFactor.ToString("G29");
                hdnGCConsumptionItemUnit.Value = entity.GCConsumptionItemUnit;
                hdnConsumptionItemUnit.Value = entity.ConsumptionItemUnit;
                hdnConsumptionConversionFactor.Value = entity.ConsumptionConversionFactor.ToString("G29");
                hdnGCPurchaseRequestItemUnit.Value = entity.GCPurchaseRequestItemUnit;
                hdnPurchaseRequestItemUnit.Value = entity.PurchaseRequestItemUnit;
                hdnPurchaseRequestConversionFactor.Value = entity.PurchaseRequestConversionFactor.ToString("G29");

                if (lstSelectedMember.Contains(entity.ID.ToString()))
                {
                    int idx = Array.IndexOf(lstSelectedMember, entity.ID.ToString());
                    if (hdnIsAllowItemDistribution.Value == "1")
                        txtDistribution.ReadOnly = false;
                    if (hdnIsAllowPurchaseRequest.Value == "1")
                        txtPurchaseRequest.ReadOnly = false;
                    if (hdnIsAllowItemConsumption.Value == "1")
                        txtConsumption.ReadOnly = false;
                    txtDistribution.Text = lstDistribution[idx];
                    txtPurchaseRequest.Text = lstPurchaseRequest[idx];
                    txtConsumption.Text = lstConsumption[idx];

                    lblDistributionItemUnit.Attributes.Add("class", "lblDistributionItemUnit lblLink");
                    lblConsumptionItemUnit.Attributes.Add("class", "lblConsumptionItemUnit lblLink");
                    lblPurchaseRequestItemUnit.Attributes.Add("class", "lblPurchaseRequestItemUnit lblLink");

                    lblDistributionItemUnit.InnerHtml = string.Format("{0} ({1})", lstDistributionItemUnit[idx], Convert.ToDecimal(lstDistributionConversionFactor[idx]).ToString("G29"));
                    lblConsumptionItemUnit.InnerHtml = string.Format("{0} ({1})", lstConsumptionItemUnit[idx], Convert.ToDecimal(lstConsumptionConversionFactor[idx]).ToString("G29"));
                    lblPurchaseRequestItemUnit.InnerHtml = string.Format("{0} ({1})", lstPurchaseRequestItemUnit[idx], Convert.ToDecimal(lstPurchaseRequestConversionFactor[idx]).ToString("G29"));

                    hdnGCDistributionItemUnit.Value = lstDistributionGCItemUnit[idx];
                    hdnDistributionItemUnit.Value = lstDistributionItemUnit[idx];
                    hdnDistributionConversionFactor.Value = lstDistributionConversionFactor[idx];
                    hdnGCConsumptionItemUnit.Value = lstConsumptionGCItemUnit[idx];
                    hdnConsumptionItemUnit.Value = lstConsumptionItemUnit[idx];
                    hdnConsumptionConversionFactor.Value = lstConsumptionConversionFactor[idx];
                    hdnGCPurchaseRequestItemUnit.Value = lstPurchaseRequestGCItemUnit[idx];
                    hdnPurchaseRequestItemUnit.Value = lstPurchaseRequestItemUnit[idx];
                    hdnPurchaseRequestConversionFactor.Value = lstPurchaseRequestConversionFactor[idx];

                    chkIsSelected.Checked = true;
                }
                else
                {
                    lblDistributionItemUnit.Attributes.Add("class", "lblDistributionItemUnit lblDisabled");
                    lblConsumptionItemUnit.Attributes.Add("class", "lblConsumptionItemUnit lblDisabled");
                    lblPurchaseRequestItemUnit.Attributes.Add("class", "lblPurchaseRequestItemUnit lblDisabled");
                }
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

        public void SavePurchaseRequestHd(IDbContext ctx, ref int purchaseRequestID, ref string purchaseRequestNo)
        {
            PurchaseRequestHdDao entityHdDao = new PurchaseRequestHdDao(ctx);
            PurchaseRequestHd entityHd = new PurchaseRequestHd();
            entityHd.ItemRequestID = Convert.ToInt32(hdnOrderID.Value);
            entityHd.SiteServiceUnitID = Convert.ToInt32(hdnToSiteServiceUnitID.Value);
            entityHd.TransactionDate = Helper.GetDatePickerValue(txtItemOrderDate.Text);
            entityHd.TransactionTime = txtItemOrderTime.Text;
            entityHd.Remarks = string.Format("Permintaan Pembelian untuk permintaan Nomor {0} dari {1}", Request.Form[txtOrderNo.UniqueID], Request.Form[txtFromLocationName.UniqueID]);
            entityHd.PurchaseRequestNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.PURCHASE_REQUEST, entityHd.TransactionDate, ctx);
            entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
            ctx.CommandType = CommandType.Text;
            ctx.Command.Parameters.Clear();
            entityHd.CreatedBy = AppSession.UserLogin.UserID;
            entityHdDao.Insert(entityHd);
            purchaseRequestID = BusinessLayer.GetPurchaseRequestHdMaxID(ctx);
            purchaseRequestNo = entityHd.PurchaseRequestNo;
        }

        public void SaveItemDistributionHd(IDbContext ctx, ref int distributionID, ref string distributionNo)
        {
            ItemDistributionHdDao entityHdDao = new ItemDistributionHdDao(ctx);
            ItemDistributionHd entityHd = new ItemDistributionHd();
            entityHd.ItemRequestID = Convert.ToInt32(hdnOrderID.Value);
            entityHd.FromSiteServiceUnitID = Convert.ToInt32(hdnToSiteServiceUnitID.Value);
            entityHd.ToSiteServiceUnitID = Convert.ToInt32(hdnFromSiteServiceUnitID.Value);
            entityHd.FromLocationID = Convert.ToInt32(hdnToLocationID.Value);
            entityHd.ToLocationID = Convert.ToInt32(hdnFromLocationID.Value);
            entityHd.DeliveryDate = Helper.GetDatePickerValue(txtItemOrderDate.Text);
            entityHd.DeliveryTime = txtItemOrderTime.Text;
            entityHd.DeliveryRemarks = string.Format("Distribusi untuk permintaan Nomor {0} dari {1}", Request.Form[txtOrderNo.UniqueID], Request.Form[txtFromLocationName.UniqueID]);
            entityHd.TransactionCode = Constant.TransactionCode.ITEM_DISTRIBUTION;
            entityHd.DistributionNo = BusinessLayer.GenerateTransactionNo(entityHd.TransactionCode, entityHd.DeliveryDate, ctx);
            entityHd.GCDistributionStatus = Constant.DistributionStatus.OPEN;
            ctx.CommandType = CommandType.Text;
            ctx.Command.Parameters.Clear();
            entityHd.CreatedBy = AppSession.UserLogin.UserID;
            entityHdDao.Insert(entityHd);
            distributionID = BusinessLayer.GetItemDistributionHdMaxID(ctx);
            distributionNo = entityHd.DistributionNo;
        }

        public void SaveItemConsumptionHd(IDbContext ctx, ref int transactionID, ref string transactionNo)
        {
            ItemTransactionHdDao entityHdDao = new ItemTransactionHdDao(ctx);
            ItemTransactionHd entityHd = new ItemTransactionHd();
            entityHd.FromLocationID = Convert.ToInt32(hdnToLocationID.Value);
            entityHd.ToLocationID = null;
            entityHd.SiteServiceUnitID = Convert.ToInt32(hdnFromSiteServiceUnitID.Value);
            entityHd.TransactionDate = Helper.GetDatePickerValue(txtItemOrderDate.Text);
            entityHd.GCConsumptionType = hdnDefaultGCConsumptionType.Value;
            entityHd.Remarks = string.Format("Pemakaian untuk permintaan Nomor {0} dari {1}", Request.Form[txtOrderNo.UniqueID], Request.Form[txtFromLocationName.UniqueID]);
            entityHd.TransactionCode = Constant.TransactionCode.ITEM_CONSUMPTION;
            entityHd.TransactionNo = BusinessLayer.GenerateTransactionNo(entityHd.TransactionCode, entityHd.TransactionDate, ctx);
            entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
            ctx.CommandType = CommandType.Text;
            ctx.Command.Parameters.Clear();
            entityHd.CreatedBy = AppSession.UserLogin.UserID;
            entityHdDao.Insert(entityHd);
            transactionID = BusinessLayer.GetItemTransactionHdMaxID(ctx);
            transactionNo = entityHd.TransactionNo;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage, ref string retval)
        {
            bool result = true;
            String[] paramID = hdnSelectedMember.Value.Substring(1).Split(',');
            String[] paramPurchaseRequest = hdnParamPurchaseRequest.Value.Substring(1).Split(',');
            String[] paramDistribution = hdnParamDistribution.Value.Substring(1).Split(',');
            String[] paramConsumption = hdnParamConsumption.Value.Substring(1).Split(',');
            String[] paramPurchaseRequestConversionFactor = hdnParamPurchaseRequestConversionFactor.Value.Substring(1).Split(',');
            String[] paramDistributionConversionFactor = hdnParamDistributionConversionFactor.Value.Substring(1).Split(',');
            String[] paramConsumptionConversionFactor = hdnParamConsumptionConversionFactor.Value.Substring(1).Split(',');
            String[] paramPurchaseRequestGCItemUnit = hdnParamPurchaseRequestGCItemUnit.Value.Substring(1).Split(',');
            String[] paramDistributionGCItemUnit = hdnParamDistributionGCItemUnit.Value.Substring(1).Split(',');
            String[] paramConsumptionGCItemUnit = hdnParamConsumptionGCItemUnit.Value.Substring(1).Split(',');

            string purchaseRequestNo = "";
            string distributionNo = "";
            string itemConsumptionNo = "";

            IDbContext ctx = DbFactory.Configure(true);
            int purchaseRequestID = 0;
            int distributionID = 0;
            int itemConsumptionID = 0;
            PurchaseRequestDtDao prDtDao = new PurchaseRequestDtDao(ctx);
            ItemDistributionDtDao idDtDao = new ItemDistributionDtDao(ctx);
            ItemTransactionDtDao itDtDao = new ItemTransactionDtDao(ctx);
            ItemRequestDtDao entityItemRequestDtDao = new ItemRequestDtDao(ctx);
            ItemRequestHdDao entityItemRequestHdDao = new ItemRequestHdDao(ctx);
            ItemTransactionHdDao entityItemTransactionHdDao = new ItemTransactionHdDao(ctx);
            try
            {

                if (type == "save")
                {
                    List<ItemRequestDt> lstEntityDt = BusinessLayer.GetItemRequestDtList(string.Format("ItemRequestID = {0} AND GCItemDetailStatus = '{1}' AND IsDeleted = 0", hdnOrderID.Value, Constant.TransactionStatus.APPROVED), ctx);

                    if (hdnIsPurchaseRequestExist.Value == "1")
                    {
                        for (int ct = 0; ct < paramID.Length; ct++)
                        {
                            if (Convert.ToDecimal(paramPurchaseRequest[ct]) == 0) continue;
                            ItemRequestDt entityItemReqDt = lstEntityDt.FirstOrDefault(p => p.ID == Convert.ToInt32(paramID[ct]));
                            entityItemReqDt.PurchaseRequestQty = Convert.ToDecimal(paramPurchaseRequest[ct]);
                            entityItemReqDt.GCPurchaseRequestItemUnit = paramPurchaseRequestGCItemUnit[ct];
                            entityItemReqDt.PurchaseRequestConversionFactor = Convert.ToDecimal(paramPurchaseRequestConversionFactor[ct]);
                            entityItemReqDt.IsSelected = true;
                            entityItemReqDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityItemRequestDtDao.Update(entityItemReqDt);

                            lstEntityDt.Remove(entityItemReqDt);
                        }
                    }

                    if (hdnIsItemDistributionExist.Value == "1")
                    {
                        for (int ct = 0; ct < paramID.Length; ct++)
                        {
                            if (Convert.ToDecimal(paramDistribution[ct]) == 0) continue;
                            ItemRequestDt entityItemReqDt = lstEntityDt.FirstOrDefault(p => p.ID == Convert.ToInt32(paramID[ct]));
                            entityItemReqDt.DistributionQty = Convert.ToDecimal(paramDistribution[ct]);
                            entityItemReqDt.DistributionConversionFactor = Convert.ToDecimal(paramDistributionConversionFactor[ct]);
                            entityItemReqDt.GCDistributionItemUnit = paramDistributionGCItemUnit[ct];
                            entityItemReqDt.IsSelected = true;
                            entityItemReqDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityItemRequestDtDao.Update(entityItemReqDt);

                            lstEntityDt.Remove(entityItemReqDt);
                        }
                    }

                    if (hdnIsItemConsumptionExist.Value == "1")
                    {
                        for (int ct = 0; ct < paramID.Length; ct++)
                        {
                            if (Convert.ToDecimal(paramConsumption[ct]) == 0) continue;
                            ItemRequestDt entityItemReqDt = lstEntityDt.FirstOrDefault(p => p.ID == Convert.ToInt32(paramID[ct]));
                            entityItemReqDt.ConsumptionQty = Convert.ToDecimal(paramConsumption[ct]);
                            entityItemReqDt.ConsumptionConversionFactor = Convert.ToDecimal(paramConsumptionConversionFactor[ct]);
                            entityItemReqDt.GCConsumptionItemUnit = paramConsumptionGCItemUnit[ct];
                            entityItemReqDt.IsSelected = true;
                            entityItemReqDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityItemRequestDtDao.Update(entityItemReqDt);

                            lstEntityDt.Remove(entityItemReqDt);
                        }
                    }

                    foreach (ItemRequestDt entityItemReqDt in lstEntityDt.Where(p => p.IsSelected).ToList())
                    {
                        entityItemReqDt.IsSelected = false;
                        entityItemReqDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityItemRequestDtDao.Update(entityItemReqDt);
                    }
                }
                else if (type == "approve")
                {
                    if (hdnIsPurchaseRequestExist.Value == "1")
                    {
                        SavePurchaseRequestHd(ctx, ref purchaseRequestID, ref purchaseRequestNo);
                        for (int ct = 0; ct < paramID.Length; ct++)
                        {
                            if (Convert.ToDecimal(paramPurchaseRequest[ct]) == 0) continue;
                            ItemRequestDt entityItemReqDt = entityItemRequestDtDao.Get(Convert.ToInt32(paramID[ct]));
                            List<vSupplierItemPlaning> vPlan = BusinessLayer.GetvSupplierItemPlaningList(string.Format("ItemID = {0}", entityItemReqDt.ItemID), ctx);
                            PurchaseRequestDt itemDt = new PurchaseRequestDt();

                            itemDt.PurchaseRequestID = purchaseRequestID;
                            itemDt.ItemID = Convert.ToInt32(entityItemReqDt.ItemID);
                            itemDt.Quantity = Convert.ToDecimal(paramPurchaseRequest[ct]);
                            itemDt.ConversionFactor = Convert.ToDecimal(paramPurchaseRequestConversionFactor[ct]);
                            itemDt.GCPurchaseUnit = paramPurchaseRequestGCItemUnit[ct];
                            itemDt.GCBaseUnit = entityItemReqDt.GCBaseUnit;
                            if (vPlan.Count > 0)
                            {
                                itemDt.BusinessPartnerID = vPlan[0].BusinessPartnerID;
                                itemDt.UnitPrice = vPlan[0].UnitPrice * entityItemReqDt.ConversionFactor;
                                itemDt.DiscountPercentage = vPlan[0].Discount;
                            }
                            else
                            {
                                itemDt.BusinessPartnerID = null;
                                itemDt.UnitPrice = Convert.ToDecimal(0.00);
                                itemDt.DiscountPercentage = Convert.ToDecimal(0.00);
                            }
                            itemDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                            itemDt.CreatedBy = AppSession.UserLogin.UserID;

                            entityItemReqDt.PurchaseRequestQty = itemDt.Quantity;
                            entityItemReqDt.GCPurchaseRequestItemUnit = itemDt.GCPurchaseUnit;
                            entityItemReqDt.PurchaseRequestConversionFactor = itemDt.ConversionFactor;
                            entityItemReqDt.IsSelected = true;
                            entityItemReqDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityItemRequestDtDao.Update(entityItemReqDt);
                            prDtDao.Insert(itemDt);
                        }
                    }

                    if (hdnIsItemDistributionExist.Value == "1")
                    {
                        SaveItemDistributionHd(ctx, ref distributionID, ref distributionNo);
                        for (int ct = 0; ct < paramID.Length; ct++)
                        {
                            if (Convert.ToDecimal(paramDistribution[ct]) == 0) continue;
                            ItemRequestDt entityItemReqDt = entityItemRequestDtDao.Get(Convert.ToInt32(paramID[ct]));
                            ItemDistributionDt itemDt = new ItemDistributionDt();
                            entityItemReqDt.GCItemDetailStatus = Constant.TransactionStatus.PROCESSED;
                            itemDt.DistributionID = distributionID;
                            itemDt.ItemID = Convert.ToInt32(entityItemReqDt.ItemID);
                            itemDt.Quantity = Convert.ToDecimal(paramDistribution[ct]);
                            itemDt.ConversionFactor = Convert.ToDecimal(paramDistributionConversionFactor[ct]);
                            itemDt.GCItemUnit = paramDistributionGCItemUnit[ct];
                            itemDt.GCBaseUnit = entityItemReqDt.GCBaseUnit;
                            itemDt.GCItemDetailStatus = Constant.DistributionStatus.OPEN;
                            itemDt.CreatedBy = AppSession.UserLogin.UserID;

                            entityItemReqDt.DistributionQty = itemDt.Quantity;
                            entityItemReqDt.DistributionConversionFactor = itemDt.ConversionFactor;
                            entityItemReqDt.GCDistributionItemUnit = itemDt.GCItemUnit;
                            entityItemReqDt.IsSelected = true;
                            entityItemReqDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityItemRequestDtDao.Update(entityItemReqDt);
                            idDtDao.Insert(itemDt);
                        }
                    }

                    if (hdnIsItemConsumptionExist.Value == "1")
                    {
                        SaveItemConsumptionHd(ctx, ref itemConsumptionID, ref itemConsumptionNo);
                        for (int ct = 0; ct < paramID.Length; ct++)
                        {
                            if (Convert.ToDecimal(paramConsumption[ct]) == 0) continue;
                            ItemRequestDt entityItemReqDt = entityItemRequestDtDao.Get(Convert.ToInt32(paramID[ct]));
                            ItemTransactionDt itemDt = new ItemTransactionDt();
                            entityItemReqDt.GCItemDetailStatus = Constant.TransactionStatus.PROCESSED;
                            itemDt.TransactionID = itemConsumptionID;
                            itemDt.ItemID = Convert.ToInt32(entityItemReqDt.ItemID);
                            itemDt.Quantity = Convert.ToDecimal(paramConsumption[ct]);
                            itemDt.ConversionFactor = Convert.ToDecimal(paramConsumptionConversionFactor[ct]);
                            itemDt.GCItemUnit = paramConsumptionGCItemUnit[ct];
                            itemDt.GCBaseUnit = entityItemReqDt.GCBaseUnit;
                            itemDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                            itemDt.CreatedBy = AppSession.UserLogin.UserID;

                            entityItemReqDt.ConsumptionQty = itemDt.Quantity;
                            entityItemReqDt.ConsumptionConversionFactor = itemDt.ConversionFactor;
                            entityItemReqDt.GCConsumptionItemUnit = itemDt.GCItemUnit;
                            entityItemReqDt.IsSelected = true;
                            entityItemReqDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityItemRequestDtDao.Update(entityItemReqDt);
                            itDtDao.Insert(itemDt);
                        }
                    }
                }
                else
                {
                    List<ItemRequestDt> lstEntityItemReqDt = BusinessLayer.GetItemRequestDtList(string.Format("ID IN ({0})", hdnSelectedMember.Value.Substring(1)));
                    foreach (ItemRequestDt itemReq in lstEntityItemReqDt)
                    {
                        itemReq.GCItemDetailStatus = Constant.TransactionStatus.VOID;
                        itemReq.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityItemRequestDtDao.Update(itemReq);
                    }
                }

                int count = BusinessLayer.GetItemRequestDtRowCount(string.Format("ItemRequestID = {0} AND GCItemDetailStatus = '{1}' AND IsDeleted = 0", hdnOrderID.Value, Constant.TransactionStatus.APPROVED), ctx);
                retval = string.Format("{0}|{1}|{2}|{3}", count, purchaseRequestNo, distributionNo, itemConsumptionNo);
                if (count == 0)
                {
                    ItemRequestHd entityItemRequestHd = entityItemRequestHdDao.Get(Convert.ToInt32(hdnOrderID.Value));
                    if (type == "approve") entityItemRequestHd.GCTransactionStatus = Constant.TransactionStatus.CLOSED;
                    else entityItemRequestHd.GCTransactionStatus = Constant.TransactionStatus.VOID;
                    entityItemRequestHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityItemRequestHdDao.Update(entityItemRequestHd);
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
    }
}