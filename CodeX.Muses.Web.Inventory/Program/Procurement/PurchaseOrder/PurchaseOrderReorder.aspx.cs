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
    public partial class PurchaseOrderReorder : BasePageTrx
    {
        private string[] lstSelectedMember = null;
        private string[] lstQty = null;
        private string[] lstGCItemUnit = null;
        private string[] lstItemUnit = null;
        private string[] lstConversionFactor = null;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.REORDER_PURCHASE_ORDER;
        }

        #region Html Getter
        protected string OnGetFilterExpressionLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.PURCHASE_ORDER);
        }
        protected string OnGetFilterExpressionServiceUnit()
        {
            if (hdnListSiteServiceUnitID.Value != "")
                return string.Format("SiteServiceUnitID IN ({0}) AND IsDeleted = 0", hdnListSiteServiceUnitID.Value);
            return "1 = 0";
        }
        protected string OnGetFilterExpressionSupplier()
        {
            return string.Format("GCBusinessPartnerType = '{0}' AND IsDeleted = 0", Constant.BusinessObjectType.SUPPLIER);
        }
        #endregion

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

            SetControlProperties();

            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();
            int PageCount = 1;
            int RowCount = 1;

            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.REORDER_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboReorderType, lstSc, "StandardCodeName", "StandardCodeID");
            cboReorderType.SelectedIndex = 0;

            BindGridView(1, true, ref PageCount, ref RowCount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
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

        protected override void SetControlProperties()
        {
            List<StandardCode> listStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}','{2}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PURCHASE_ORDER_TYPE, Constant.StandardCode.FRANCO_REGION, Constant.StandardCode.CURRENCY_CODE));
            List<Term> listTerm = BusinessLayer.GetTermList(string.Format("isDeleted = 0"));
            Methods.SetComboBoxField<StandardCode>(cboPurchaseOrderType, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.PURCHASE_ORDER_TYPE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboFrancoRegion, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.FRANCO_REGION).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboCurrency, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.CURRENCY_CODE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<Term>(cboTerm, listTerm, "TermName", "TermID");
            cboPurchaseOrderType.SelectedIndex = 0;
            cboFrancoRegion.SelectedIndex = 0;
            cboCurrency.SelectedIndex = 0;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtItemOrderDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(txtItemOrderDeliveryDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(txtItemOrderExpiredDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(hdnSupplierID, new ControlEntrySetting(true, true, true,"0"));
            SetControlEntrySetting(txtSupplierCode, new ControlEntrySetting(true, true, true));

            SetControlEntrySetting(hdnSiteServiceUnitID, new ControlEntrySetting(false, false, false, hdnDefaultSiteServiceUnitID.Value));
            SetControlEntrySetting(txtServiceUnitCode, new ControlEntrySetting(true, false, true, hdnDefaultServiceUnitCode.Value));
            SetControlEntrySetting(txtServiceUnitName, new ControlEntrySetting(false, false, true, hdnDefaultServiceUnitName.Value));
            
            SetControlEntrySetting(cboPurchaseOrderType, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(cboTerm, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(cboFrancoRegion, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(cboCurrency, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtKurs, new ControlEntrySetting(true, true, true, "1.00"));
        }

        #region Load

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vItemMaster entity = e.Row.DataItem as vItemMaster;

                vPurchaseOrderDtQtyOnOrderPerItemPerSiteServiceUnit entityQtyOnOrder = lstQtyOnOrder.FirstOrDefault(p => p.ItemID == entity.ItemID);

                decimal qtyOnOrder = 0;
                if (entityQtyOnOrder != null)
                    qtyOnOrder = entityQtyOnOrder.QtyOnOrder;

                List<ItemBalance> lstItemBalance1 = lstItemBalance.Where(p => p.ItemID == entity.ItemID).ToList();
                vItemPlanningCustom itemPlanning = lstItemPlanning.FirstOrDefault(p => p.ItemID == entity.ItemID);
                TextBox txtQty = e.Row.FindControl("txtQty") as TextBox;
                TextBox txtTotalQty = e.Row.FindControl("txtTotalQty") as TextBox;
                CheckBox chkIsSelected = (CheckBox)e.Row.FindControl("chkIsSelected");

                HtmlGenericControl divMinimum = e.Row.FindControl("divMinimum") as HtmlGenericControl;
                HtmlGenericControl divMaximum = e.Row.FindControl("divMaximum") as HtmlGenericControl;
                HtmlGenericControl lblEndingBalance = e.Row.FindControl("lblEndingBalance") as HtmlGenericControl;
                HtmlGenericControl lblQtyOnOrder = e.Row.FindControl("lblQtyOnOrder") as HtmlGenericControl;
                HtmlInputHidden hdnGCItemUnit = (HtmlInputHidden)e.Row.FindControl("hdnGCItemUnit");
                HtmlInputHidden hdnItemUnit = (HtmlInputHidden)e.Row.FindControl("hdnItemUnit");
                HtmlInputHidden hdnConversionFactor = (HtmlInputHidden)e.Row.FindControl("hdnConversionFactor");
                HtmlGenericControl lblItemUnit = (HtmlGenericControl)e.Row.FindControl("lblItemUnit");

                decimal quantityMIN = lstItemBalance1.Sum(p => p.QuantityMIN);
                decimal quantityMAX = lstItemBalance1.Sum(p => p.QuantityMAX);
                decimal quantityEND = lstItemBalance1.Sum(p => p.QuantityEND);

                divMinimum.InnerHtml = quantityMIN.ToString("0.00");
                divMaximum.InnerHtml = quantityMAX.ToString("0.00");
                lblEndingBalance.InnerHtml = quantityEND.ToString("0.00");
                lblQtyOnOrder.InnerHtml = qtyOnOrder.ToString("0.00");
                hdnGCItemUnit.Value = itemPlanning.GCPurchaseUnit;
                hdnItemUnit.Value = itemPlanning.PurchaseUnit;
                lblItemUnit.InnerText = string.Format("{0} ({1})", itemPlanning.PurchaseUnit, itemPlanning.PurchaseUnitConversionFactor.ToString("G29"));
                hdnConversionFactor.Value = itemPlanning.PurchaseUnitConversionFactor.ToString();

                decimal conversionFactor = itemPlanning.PurchaseUnitConversionFactor;
                Decimal autoQty = (quantityMAX - quantityEND - qtyOnOrder);
                if (autoQty < 0) autoQty = 0;
                if (lstSelectedMember.Contains(entity.ItemID.ToString()))
                {
                    int idx = Array.IndexOf(lstSelectedMember, entity.ItemID.ToString());
                    chkIsSelected.Checked = true;
                    txtQty.ReadOnly = false;
                    txtQty.Text = lstQty[idx];
                    hdnConversionFactor.Value = lstConversionFactor[idx];
                    hdnGCItemUnit.Value = lstGCItemUnit[idx];
                    hdnItemUnit.Value = lstItemUnit[idx];
                    lblItemUnit.Attributes.Add("class", "lblItemUnit lblLink");
                    conversionFactor = Convert.ToDecimal(lstConversionFactor[idx]);
                    lblItemUnit.InnerHtml = string.Format("{0} ({1})", lstItemUnit[idx], conversionFactor.ToString("G29"));
                }
                else
                {
                    lblItemUnit.Attributes.Add("class", "lblItemUnit lblDisabled");
                }
                autoQty = Math.Ceiling(autoQty / conversionFactor);
                txtQty.Text = autoQty.ToString("0.00");
                txtTotalQty.Text = (autoQty * conversionFactor).ToString("0.00");
            }
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnLstLocationID.Value != "")
            {
                if (cboReorderType.Value.ToString() == Constant.ReorderType.STATIC)
                    filterExpression = string.Format("ItemID IN (SELECT ItemID FROM ItemBalance WHERE LocationID IN ({0}) AND GCReorderType = '{1}' AND IsDeleted = 0 GROUP BY ItemID HAVING SUM(QuantityEND) <= SUM(QuantityMIN)) AND IsDeleted = 0", hdnLstLocationID.Value, cboReorderType.Value);
            }

            if (isCountPageCount)
            {
                if (cboReorderType.Value.ToString() == Constant.ReorderType.STATIC)
                    rowCount = BusinessLayer.GetvItemMasterRowCount(filterExpression);
                else
                    rowCount = BusinessLayer.GetItemUsagePurchaseRequestROPRowCount(hdnLstLocationID.Value, "");
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split('|');
            lstQty = hdnListQty.Value.Split('|');
            lstGCItemUnit = hdnListGCItemUnit.Value.Split('|');
            lstItemUnit = hdnListItemUnit.Value.Split('|');
            lstConversionFactor = hdnListConversionFactor.Value.Split('|');
            List<vItemMaster> lstEntity = null;
            List<GetItemUsagePurchaseRequestROPList> lstEntity2 = null;
            string lstItemID = "";
            if (cboReorderType.Value.ToString() == Constant.ReorderType.STATIC)
            {
                lstEntity = BusinessLayer.GetvItemMasterList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ItemName1 ASC");
                lstItemID = string.Join(",", lstEntity.Select(p => p.ItemID).ToList());
            }
            else
            {
                if (lstItemID != "" && hdnLstLocationID.Value != "")
                    lstEntity2 = BusinessLayer.GetItemUsagePurchaseRequestROPList(hdnLstLocationID.Value, "", pageIndex, Constant.GridViewPageSize.GRID_MASTER);
                else
                    lstEntity2 = new List<GetItemUsagePurchaseRequestROPList>();
                lstItemID = string.Join(",", lstEntity2.Select(p => p.ItemID).ToList());
            }

            if (lstItemID != "" && hdnSiteServiceUnitID.Value != "" && hdnSiteServiceUnitID.Value != "0")
                lstQtyOnOrder = BusinessLayer.GetvPurchaseOrderDtQtyOnOrderPerItemPerSiteServiceUnitList(string.Format("SiteServiceUnitID = {0} AND ItemID IN ({1})", hdnSiteServiceUnitID.Value, lstItemID));
            else
                lstQtyOnOrder = new List<vPurchaseOrderDtQtyOnOrderPerItemPerSiteServiceUnit>();

            if (lstItemID != "" && hdnLstLocationID.Value != "")
                lstItemBalance = BusinessLayer.GetItemBalanceList(string.Format("LocationID IN ({0}) AND ItemID IN ({1}) AND GCReorderType = '{2}' AND IsDeleted = 0", hdnLstLocationID.Value, lstItemID, cboReorderType.Value));
            else
                lstItemBalance = new List<ItemBalance>();
            if (lstItemID != "")
                lstItemPlanning = BusinessLayer.GetvItemPlanningCustomList(string.Format("SiteID = '{0}' AND ItemID IN ({1}) AND IsDeleted = 0", AppSession.UserLogin.SiteID, lstItemID));
            else
                lstItemPlanning = new List<vItemPlanningCustom>();

            if (cboReorderType.Value.ToString() == Constant.ReorderType.STATIC)
            {
                grdView.DataSource = lstEntity;
                grdView.DataBind();

                pnlView.Visible = true;
                pnlView2.Visible = false;
            }
            else
            {
                grdView2.DataSource = lstEntity2;
                grdView2.DataBind();

                pnlView.Visible = false;
                pnlView2.Visible = true;
            }
        }

        protected void grdView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GetItemUsagePurchaseRequestROPList entity = e.Row.DataItem as GetItemUsagePurchaseRequestROPList;
                vPurchaseOrderDtQtyOnOrderPerItemPerSiteServiceUnit entityQtyOnOrder = lstQtyOnOrder.FirstOrDefault(p => p.ItemID == entity.ItemID);

                decimal qtyOnOrder = 0;
                if (entityQtyOnOrder != null)
                    qtyOnOrder = entityQtyOnOrder.QtyOnOrder;

                List<ItemBalance> lstItemBalance1 = lstItemBalance.Where(p => p.ItemID == entity.ItemID).ToList();
                vItemPlanningCustom itemPlanning = lstItemPlanning.FirstOrDefault(p => p.ItemID == entity.ItemID);
                decimal quantityEND = lstItemBalance1.Sum(p => p.QuantityEND);

                TextBox txtQty = e.Row.FindControl("txtQty") as TextBox;
                TextBox txtTotalQty = e.Row.FindControl("txtTotalQty") as TextBox;
                HtmlGenericControl lblQtyOnOrder = e.Row.FindControl("lblQtyOnOrder") as HtmlGenericControl;
                HtmlGenericControl lblEndingBalance = e.Row.FindControl("lblEndingBalance") as HtmlGenericControl;
                CheckBox chkIsSelected = (CheckBox)e.Row.FindControl("chkIsSelected");
                HtmlInputHidden hdnGCItemUnit = (HtmlInputHidden)e.Row.FindControl("hdnGCItemUnit");
                HtmlInputHidden hdnConversionFactor = (HtmlInputHidden)e.Row.FindControl("hdnConversionFactor");
                HtmlGenericControl lblItemUnit = (HtmlGenericControl)e.Row.FindControl("lblItemUnit");

                lblQtyOnOrder.InnerHtml = qtyOnOrder.ToString("0.00");
                lblEndingBalance.InnerHtml = quantityEND.ToString("0.00");
                hdnGCItemUnit.Value = itemPlanning.GCPurchaseUnit;
                lblItemUnit.InnerText = string.Format("{0} ({1})", itemPlanning.PurchaseUnit, itemPlanning.PurchaseUnitConversionFactor.ToString("G29"));
                hdnConversionFactor.Value = itemPlanning.PurchaseUnitConversionFactor.ToString();

                decimal conversionFactor = itemPlanning.PurchaseUnitConversionFactor;
                Decimal autoQty = (entity.QtyOrder - qtyOnOrder);
                if (autoQty < 0) autoQty = 0;

                if (lstSelectedMember.Contains(entity.ItemID.ToString()))
                {
                    int idx = Array.IndexOf(lstSelectedMember, entity.ItemID.ToString());
                    chkIsSelected.Checked = true;
                    txtQty.ReadOnly = false;
                    txtQty.Text = lstQty[idx];
                    hdnConversionFactor.Value = lstConversionFactor[idx];
                    hdnGCItemUnit.Value = lstGCItemUnit[idx];
                    lblItemUnit.Attributes.Add("class", "lblItemUnit lblLink");
                    conversionFactor = Convert.ToDecimal(lstConversionFactor[idx]);
                    lblItemUnit.InnerHtml = string.Format("{0} ({1})", lstItemUnit[idx], conversionFactor.ToString("G29"));
                }
                else
                {
                    lblItemUnit.Attributes.Add("class", "lblItemUnit lblDisabled");
                }
                autoQty = Math.Ceiling(autoQty / conversionFactor);
                txtQty.Text = autoQty.ToString("0.00");
                txtTotalQty.Text = (autoQty * conversionFactor).ToString("0.00");
            }
        }

        List<vItemPlanningCustom> lstItemPlanning = null;
        List<ItemBalance> lstItemBalance = null;
        List<vPurchaseOrderDtQtyOnOrderPerItemPerSiteServiceUnit> lstQtyOnOrder = null;
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

        #region Save
        public void SavePurchaseOrderHd(IDbContext ctx, ref int purchaseOrderID, ref string retval)
        {
            PurchaseOrderHdDao entityHdDao = new PurchaseOrderHdDao(ctx);
            PurchaseOrderHd entityHd = new PurchaseOrderHd();
            entityHd.OrderDate = Helper.GetDatePickerValue(txtItemOrderDate.Text);
            entityHd.DeliveryDate = Helper.GetDatePickerValue(txtItemOrderDeliveryDate.Text);
            entityHd.POExpiredDate = Helper.GetDatePickerValue(txtItemOrderExpiredDate.Text);
            entityHd.GCPurchaseOrderType = cboPurchaseOrderType.Value.ToString();
            entityHd.TermID = Convert.ToInt32(cboTerm.Value.ToString());
            entityHd.BusinessPartnerID = Convert.ToInt32(hdnSupplierID.Value);
            entityHd.SiteServiceUnitID = Convert.ToInt32(hdnSiteServiceUnitID.Value);
            entityHd.GCFrancoRegion = cboFrancoRegion.Value.ToString();
            entityHd.GCCurrencyCode = cboCurrency.Value.ToString();
            entityHd.CurrencyRate = Convert.ToDecimal(txtKurs.Text);
            entityHd.IsIncludeVAT = false;
            entityHd.TransactionCode = Constant.TransactionCode.PURCHASE_ORDER;
            entityHd.PurchaseOrderNo = BusinessLayer.GenerateTransactionNo(entityHd.TransactionCode, entityHd.OrderDate, ctx);
            entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
            entityHd.DownPaymentAmount = Convert.ToDecimal(0.00);
            entityHd.FinalDiscountPercentage = 0;
            entityHd.FinalDiscountAmount = 0;
            entityHd.VATAmount = 0;
            entityHd.VATPercentage = 0;
            entityHd.TotalNetTransactionAmount = entityHd.TransactionAmount + entityHd.VATAmount - entityHd.FinalDiscountAmount - entityHd.DownPaymentAmount;
            ctx.CommandType = CommandType.Text;
            ctx.Command.Parameters.Clear();
            entityHd.CreatedBy = AppSession.UserLogin.UserID;
            entityHdDao.Insert(entityHd);
            purchaseOrderID = BusinessLayer.GetPurchaseOrderHdMaxID(ctx);
            retval = entityHd.PurchaseOrderNo;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage, ref string retval)
        {
            bool result = true;
            String[] paramID = hdnSelectedMember.Value.Substring(1).Split('|');
            String[] paramQty = hdnListQty.Value.Substring(1).Split('|');
            String[] paramGCItemUnit = hdnListGCItemUnit.Value.Substring(1).Split('|');
            String[] paramConversionFactor = hdnListConversionFactor.Value.Substring(1).Split('|');
            IDbContext ctx = DbFactory.Configure(true);
            int purchaseOrderID = 0;
            PurchaseOrderDtDao entityPurchaseOrderDtDao = new PurchaseOrderDtDao(ctx);
            try
            {
                SavePurchaseOrderHd(ctx, ref purchaseOrderID, ref retval);

                string lstItemID = "";
                foreach (String id in paramID)
                {
                    if (lstItemID != "")
                        lstItemID += ",";
                    lstItemID += id;
                }
                List<ItemMaster> lstEntityItemMaster = BusinessLayer.GetItemMasterList(string.Format("ItemID IN ({0})", lstItemID), ctx);

                for (int ct = 0; ct < paramID.Length; ct++)
                {
                    ItemMaster entityItemMaster = lstEntityItemMaster.FirstOrDefault(p => p.ItemID == Convert.ToInt32(paramID[ct]));
                    PurchaseOrderDt entityPurchaseOrderDt = new PurchaseOrderDt();
                    entityPurchaseOrderDt.ItemID = entityItemMaster.ItemID;
                    entityPurchaseOrderDt.Quantity = Convert.ToDecimal(paramQty[ct]);
                    entityPurchaseOrderDt.GCPurchaseUnit = paramGCItemUnit[ct];
                    entityPurchaseOrderDt.GCBaseUnit = entityItemMaster.GCItemUnit;
                    entityPurchaseOrderDt.ConversionFactor = Convert.ToDecimal(paramConversionFactor[ct]);
                    entityPurchaseOrderDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;

                    GetItemMasterPurchase itemPurchase = BusinessLayer.GetItemMasterPurchaseList(AppSession.UserLogin.SiteID, entityItemMaster.ItemID, Convert.ToInt32(hdnSupplierID.Value), ctx).FirstOrDefault();
                    if (itemPurchase != null)
                    {
                        entityPurchaseOrderDt.DiscountPercentage1 = itemPurchase.Discount;
                        entityPurchaseOrderDt.UnitPrice = itemPurchase.Price * entityPurchaseOrderDt.ConversionFactor;
                    }
                    else
                    {
                        entityPurchaseOrderDt.UnitPrice = Convert.ToDecimal(0.00);
                        entityPurchaseOrderDt.DiscountPercentage1 = Convert.ToDecimal(0.00);
                    }
                    ctx.CommandType = CommandType.Text;
                    ctx.Command.Parameters.Clear();

                    entityPurchaseOrderDt.CreatedBy = AppSession.UserLogin.UserID;
                    entityPurchaseOrderDt.PurchaseOrderID = purchaseOrderID;
                    entityPurchaseOrderDtDao.Insert(entityPurchaseOrderDt);
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