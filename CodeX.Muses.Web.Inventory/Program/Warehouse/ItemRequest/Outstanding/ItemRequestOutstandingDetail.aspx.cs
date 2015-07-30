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
        private string[] lstConsumption = null;
        private string[] lstPurchaseRequest = null;

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.APPROVED_ITEM_REQUEST;
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

            bool IsAllowPurchaseRequest = true;
            bool IsAllowItemConsumption = true;
            bool IsAllowItemDistribution = true;

            int? restrictionID = BusinessLayer.GetLocation(entityItemRequest.ToLocationID).RestrictionID;
            if (restrictionID != null)
            {
                List<RestrictionDt> lstRestrictionDt = BusinessLayer.GetRestrictionDtList(string.Format("RestrictionID = {0}", restrictionID));
                IsAllowPurchaseRequest = lstRestrictionDt.FirstOrDefault(p => p.TransactionCode == Constant.TransactionCode.PURCHASE_REQUEST) != null;
                IsAllowItemConsumption = lstRestrictionDt.FirstOrDefault(p => p.TransactionCode == Constant.TransactionCode.ITEM_CONSUMPTION) != null;
                IsAllowItemDistribution = lstRestrictionDt.FirstOrDefault(p => p.TransactionCode == Constant.TransactionCode.ITEM_DISTRIBUTION) != null;
            }

            hdnIsAllowPurchaseRequest.Value = IsAllowPurchaseRequest ? "1" : "0";
            hdnIsAllowItemConsumption.Value = IsAllowItemConsumption ? "1" : "0";
            hdnIsAllowItemDistribution.Value = IsAllowItemDistribution ? "1" : "0";

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
            hdnLocationIDFrom.Value = entity.FromLocationID.ToString();
            txtLocationCode.Text = entity.FromLocationCode;
            txtLocationName.Text = entity.FromLocationName;
            hdnLocationIDTo.Value = entity.ToLocationID.ToString();
            txtLocationCodeTo.Text = entity.ToLocationCode;
            txtLocationNameTo.Text = entity.ToLocationName;
            txtNotes.Text = entity.Remarks;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnOrderID.Value != "")
                filterExpression = string.Format("ItemRequestID = {0} AND GCItemDetailStatus = '{1}' AND IsDeleted = 0", hdnOrderID.Value, Constant.TransactionStatus.APPROVED);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvItemRequestDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            lstDistribution = hdnParamDistribution.Value.Split(',');
            lstConsumption = hdnParamConsumption.Value.Split(',');
            lstPurchaseRequest = hdnParamPurchaseReq.Value.Split(',');
            List<vItemRequestDt> lstEntity = BusinessLayer.GetvItemRequestDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ItemName1 ASC");

            string lsItemID = string.Join(",", lstEntity.Select(p => p.ItemID).ToList());
            lstItemRequestDtRealizationPerItem = BusinessLayer.GetvItemRequestDtRealizationPerItemList(string.Format("ItemID IN ({0})", lsItemID));

            lvwView.DataSource = lstEntity;
            lvwView.DataBind();

        }

        List<vItemRequestDtRealizationPerItem> lstItemRequestDtRealizationPerItem = null;
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

                decimal availableQty = 0;
                vItemRequestDtRealizationPerItem itemRequestDtRealizationPerItem = lstItemRequestDtRealizationPerItem.FirstOrDefault(p => p.ItemID == entity.ItemID);
                if (itemRequestDtRealizationPerItem != null)
                {
                    availableQty = entity.EndingBalance - itemRequestDtRealizationPerItem.ItemRequestQuantity;
                    if (entity.PurchaseRequestQty > 0)
                        availableQty += entity.Quantity;
                }
                else
                    availableQty = entity.EndingBalance;
                if (availableQty < 0)
                    availableQty = 0;
                lblAvailableStock.InnerHtml = availableQty.ToString();

                Helper.SetControlEntrySetting(txtDistribution, new ControlEntrySetting(true, true, true), "mpEntry");
                Helper.SetControlEntrySetting(txtPurchaseRequest, new ControlEntrySetting(true, true, true), "mpEntry");
                Helper.SetControlEntrySetting(txtConsumption, new ControlEntrySetting(true, true, true), "mpEntry");

                if (entity.Quantity > entity.EndingBalance)
                {
                    txtDistribution.Text = entity.EndingBalance.ToString();
                    if (hdnIsAllowPurchaseRequest.Value == "1" && entity.PurchaseRequestQty == 0)
                        txtPurchaseRequest.Text = (entity.Quantity - entity.EndingBalance).ToString();
                }
                else txtDistribution.Text = entity.Quantity.ToString();

                txtConsumption.Attributes.Add("max", entity.EndingBalance.ToString());
                txtDistribution.Attributes.Add("max", entity.EndingBalance.ToString());

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
                    chkIsSelected.Checked = true;
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
            entityHd.FromLocationID = Convert.ToInt32(hdnLocationIDTo.Value);
            entityHd.TransactionDate = Helper.GetDatePickerValue(txtItemOrderDate.Text);
            entityHd.TransactionTime = txtItemOrderTime.Text;
            entityHd.Remarks = string.Format("Permintaan Pembelian untuk permintaan Nomor {0} dari {1}", Request.Form[txtOrderNo.UniqueID], Request.Form[txtLocationName.UniqueID]);
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
            entityHd.FromLocationID = Convert.ToInt32(hdnLocationIDTo.Value);
            entityHd.ToLocationID = Convert.ToInt32(hdnLocationIDFrom.Value);
            entityHd.DeliveryDate = Helper.GetDatePickerValue(txtItemOrderDate.Text);
            entityHd.DeliveryTime = txtItemOrderTime.Text;
            entityHd.DeliveryRemarks = string.Format("Distribusi untuk permintaan Nomor {0} dari {1}", Request.Form[txtOrderNo.UniqueID], Request.Form[txtLocationName.UniqueID]);
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
            //entityHd.ItemRequestID = Convert.ToInt32(hdnOrderID.Value);
            entityHd.FromLocationID = Convert.ToInt32(hdnLocationIDTo.Value);
            entityHd.ToLocationID = null;
            entityHd.TransactionDate = Helper.GetDatePickerValue(txtItemOrderDate.Text);
            entityHd.GCConsumptionType = hdnDefaultGCConsumptionType.Value;
            //entityHd.DeliveryTime = txtItemOrderTime.Text;
            entityHd.Remarks = string.Format("Pemakaian untuk permintaan Nomor {0} dari {1}", Request.Form[txtOrderNo.UniqueID], Request.Form[txtLocationName.UniqueID]);
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
            bool flagPR = false;
            bool flagID = false;
            bool flagIC = false;
            String[] paramID = hdnSelectedMember.Value.Substring(1).Split(',');
            String[] paramPurchaseRequest = hdnParamPurchaseReq.Value.Substring(1).Split(',');
            String[] paramItemDistribution = hdnParamDistribution.Value.Substring(1).Split(',');
            String[] paramItemConsumption = hdnParamConsumption.Value.Substring(1).Split(',');

            string purchaseRequestNo = "";
            string distributionNo = "";
            string itemConsumptionNo = "";

            foreach (String temp in paramPurchaseRequest)
            {
                if (Convert.ToDecimal(temp) != 0) flagPR = true;
            }

            foreach (String temp in paramItemDistribution)
            {
                if (Convert.ToDecimal(temp) != 0) flagID = true;
            }

            foreach (String temp in paramItemConsumption)
            {
                if (Convert.ToDecimal(temp) != 0) flagIC = true;
            }

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
                if (type == "approve")
                {
                    if (flagPR)
                    {
                        SavePurchaseRequestHd(ctx, ref purchaseRequestID, ref purchaseRequestNo);
                        for (int ct = 0; ct < paramID.Length; ct++)
                        {
                            if (Convert.ToDecimal(paramPurchaseRequest[ct]) == 0) continue;
                            ItemRequestDt entityItemReqDt = entityItemRequestDtDao.Get(Convert.ToInt32(paramID[ct]));
                            List<vSupplierItemPlaning> vPlan = BusinessLayer.GetvSupplierItemPlaningList(string.Format("ItemID = {0}", entityItemReqDt.ItemID), ctx);
                            PurchaseRequestDt itemDt = new PurchaseRequestDt();

                            //entityItemReqDt.GCItemDetailStatus = Constant.TransactionStatus.PROCESSED;
                            itemDt.PurchaseRequestID = purchaseRequestID;
                            itemDt.ItemID = Convert.ToInt32(entityItemReqDt.ItemID);
                            itemDt.Quantity = Convert.ToDecimal(paramPurchaseRequest[ct]);
                            itemDt.ConversionFactor = entityItemReqDt.ConversionFactor;
                            itemDt.GCPurchaseUnit = entityItemReqDt.GCItemUnit;
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
                            entityItemReqDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityItemRequestDtDao.Update(entityItemReqDt);
                            prDtDao.Insert(itemDt);
                        }
                    }

                    if (flagID)
                    {
                        SaveItemDistributionHd(ctx, ref distributionID, ref distributionNo);
                        for (int ct = 0; ct < paramID.Length; ct++)
                        {
                            if (Convert.ToDecimal(paramItemDistribution[ct]) == 0) continue;
                            ItemRequestDt entityItemReqDt = entityItemRequestDtDao.Get(Convert.ToInt32(paramID[ct]));
                            ItemDistributionDt itemDt = new ItemDistributionDt();
                            entityItemReqDt.GCItemDetailStatus = Constant.TransactionStatus.PROCESSED;
                            itemDt.DistributionID = distributionID;
                            itemDt.ItemID = Convert.ToInt32(entityItemReqDt.ItemID);
                            itemDt.Quantity = Convert.ToDecimal(paramItemDistribution[ct]);
                            itemDt.ConversionFactor = entityItemReqDt.ConversionFactor;
                            itemDt.GCItemUnit = entityItemReqDt.GCItemUnit;
                            itemDt.GCBaseUnit = entityItemReqDt.GCBaseUnit;
                            itemDt.GCItemDetailStatus = Constant.DistributionStatus.OPEN;
                            itemDt.CreatedBy = AppSession.UserLogin.UserID;

                            entityItemReqDt.DistributionQty = itemDt.Quantity;
                            entityItemReqDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityItemRequestDtDao.Update(entityItemReqDt);
                            idDtDao.Insert(itemDt);
                        }
                    }

                    if (flagIC)
                    {
                        SaveItemConsumptionHd(ctx, ref itemConsumptionID, ref itemConsumptionNo);
                        for (int ct = 0; ct < paramID.Length; ct++)
                        {
                            if (Convert.ToDecimal(paramItemConsumption[ct]) == 0) continue;
                            ItemRequestDt entityItemReqDt = entityItemRequestDtDao.Get(Convert.ToInt32(paramID[ct]));
                            ItemTransactionDt itemDt = new ItemTransactionDt();
                            entityItemReqDt.GCItemDetailStatus = Constant.TransactionStatus.PROCESSED;
                            itemDt.TransactionID = itemConsumptionID;
                            itemDt.ItemID = Convert.ToInt32(entityItemReqDt.ItemID);
                            itemDt.Quantity = Convert.ToDecimal(paramItemConsumption[ct]);
                            itemDt.ConversionFactor = entityItemReqDt.ConversionFactor;
                            itemDt.GCItemUnit = entityItemReqDt.GCItemUnit;
                            itemDt.GCBaseUnit = entityItemReqDt.GCBaseUnit;
                            itemDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                            itemDt.CreatedBy = AppSession.UserLogin.UserID;

                            entityItemReqDt.ConsumptionQty = itemDt.Quantity;
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