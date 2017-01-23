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
    public partial class PurchaseRequestReorder : BasePageTrx
    {
        private string[] lstSelectedMember = null;
        private string[] lstQty = null;
        private string[] lstGCItemUnit = null;
        private string[] lstItemUnit = null;
        private string[] lstConversionFactor = null;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.REORDER_PURCHASE_REQUEST;
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
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();
            int PageCount = 1;
            int RowCount = 1;

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

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnSiteServiceUnitID, new ControlEntrySetting(false, false, false, hdnDefaultSiteServiceUnitID.Value));
            SetControlEntrySetting(txtServiceUnitCode, new ControlEntrySetting(true, false, true, hdnDefaultServiceUnitCode.Value));
            SetControlEntrySetting(txtServiceUnitName, new ControlEntrySetting(false, false, true, hdnDefaultServiceUnitName.Value));

            SetControlEntrySetting(txtPurchaseRequestDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(txtPurchaseRequestTime, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.TIME_FORMAT)));
            SetControlEntrySetting(txtNotes, new ControlEntrySetting(true, true, false));
        }

        #region Load
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vItemMaster entity = e.Row.DataItem as vItemMaster;

                vPurchaseRequestDtQtyOnOrderPerItemPerSiteServiceUnit entityQtyOnOrder = lstQtyOnOrder.FirstOrDefault(p => p.ItemID == entity.ItemID);

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
                if (hdnLstLocationID.Value != "")
                    lstEntity2 = BusinessLayer.GetItemUsagePurchaseRequestROPList(hdnLstLocationID.Value, "", pageIndex, Constant.GridViewPageSize.GRID_MASTER);
                else
                    lstEntity2 = new List<GetItemUsagePurchaseRequestROPList>();
                lstItemID = string.Join(",", lstEntity2.Select(p => p.ItemID).ToList());
            }

            if (lstItemID != "" && hdnSiteServiceUnitID.Value != "" && hdnSiteServiceUnitID.Value != "0")
                lstQtyOnOrder = BusinessLayer.GetvPurchaseRequestDtQtyOnOrderPerItemPerSiteServiceUnitList(string.Format("SiteServiceUnitID = {0} AND ItemID IN ({1})", hdnSiteServiceUnitID.Value, lstItemID));
            else
                lstQtyOnOrder = new List<vPurchaseRequestDtQtyOnOrderPerItemPerSiteServiceUnit>();

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
                vPurchaseRequestDtQtyOnOrderPerItemPerSiteServiceUnit entityQtyOnOrder = lstQtyOnOrder.FirstOrDefault(p => p.ItemID == entity.ItemID);

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
                Decimal autoQty = (entity.QtyOrder - qtyOnOrder - quantityEND);
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
        List<vPurchaseRequestDtQtyOnOrderPerItemPerSiteServiceUnit> lstQtyOnOrder = null;
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
        public void SavePurchaseRequestHd(IDbContext ctx, ref int purchaseReqID, ref string retval)
        {
            PurchaseRequestHdDao entityHdDao = new PurchaseRequestHdDao(ctx);
            PurchaseRequestHd entityHd = new PurchaseRequestHd();
            entityHd.SiteServiceUnitID = Convert.ToInt32(hdnSiteServiceUnitID.Value);
            entityHd.TransactionDate = Helper.GetDatePickerValue(txtPurchaseRequestDate.Text);
            entityHd.TransactionTime = txtPurchaseRequestTime.Text;
            entityHd.Remarks = txtNotes.Text;
            entityHd.PurchaseRequestNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.PURCHASE_REQUEST, entityHd.TransactionDate, ctx);
            retval = entityHd.PurchaseRequestNo;
            entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
            ctx.CommandType = CommandType.Text;
            ctx.Command.Parameters.Clear();
            entityHd.CreatedBy = AppSession.UserLogin.UserID;
            entityHdDao.Insert(entityHd);
            purchaseReqID = BusinessLayer.GetPurchaseRequestHdMaxID(ctx);
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage, ref string retval)
        {
            bool result = true;
            String[] paramID = hdnSelectedMember.Value.Substring(1).Split('|');
            String[] paramQty = hdnListQty.Value.Substring(1).Split('|');
            String[] paramGCItemUnit = hdnListGCItemUnit.Value.Substring(1).Split('|');
            String[] paramConversionFactor = hdnListConversionFactor.Value.Substring(1).Split('|');
            IDbContext ctx = DbFactory.Configure(true);
            int purchaseRequestID = 0;
            PurchaseRequestDtDao entityPurchaseRequestDtDao = new PurchaseRequestDtDao(ctx);
            try
            {
                SavePurchaseRequestHd(ctx, ref purchaseRequestID, ref retval);

                string lstItemID = "";
                foreach (String id in paramID)
                {
                    if (lstItemID != "")
                        lstItemID += ",";
                    lstItemID += id;
                }
                List<ItemMaster> lstEntityItemMaster = BusinessLayer.GetItemMasterList(string.Format("ItemID IN ({0})", lstItemID), ctx);

                List<ItemPlanning> lstItemPlanning = BusinessLayer.GetItemPlanningList(string.Format("SiteID = '{0}' AND ItemID IN ({1}) AND IsDeleted = 0", AppSession.UserLogin.SiteID, lstItemID), ctx);
                for (int ct = 0; ct < paramID.Length; ct++)
                {
                    ItemMaster entityItemMaster = lstEntityItemMaster.FirstOrDefault(p => p.ItemID == Convert.ToInt32(paramID[ct]));
                    PurchaseRequestDt entityPurchaseReqDt = new PurchaseRequestDt();
                    entityPurchaseReqDt.PurchaseRequestID = purchaseRequestID;
                    entityPurchaseReqDt.ItemID = entityItemMaster.ItemID;
                    entityPurchaseReqDt.Quantity = Convert.ToDecimal(paramQty[ct]);
                    entityPurchaseReqDt.GCPurchaseUnit = paramGCItemUnit[ct];
                    entityPurchaseReqDt.GCBaseUnit = entityItemMaster.GCItemUnit;
                    entityPurchaseReqDt.ConversionFactor = Convert.ToDecimal(paramConversionFactor[ct]);
                    entityPurchaseReqDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;

                    ItemPlanning itemPlanning = lstItemPlanning.FirstOrDefault(p => p.ItemID == entityItemMaster.ItemID);
                    if (itemPlanning != null)
                        entityPurchaseReqDt.BusinessPartnerID = itemPlanning.BusinessPartnerID;
                    else
                        entityPurchaseReqDt.BusinessPartnerID = null;

                    int businessPartnerID = (entityPurchaseReqDt.BusinessPartnerID == null ? 0 : (int)entityPurchaseReqDt.BusinessPartnerID);
                    GetItemMasterPurchase itemPurchase = BusinessLayer.GetItemMasterPurchaseList(AppSession.UserLogin.SiteID, entityItemMaster.ItemID, businessPartnerID, ctx).FirstOrDefault();
                    if (itemPurchase != null)
                    {
                        entityPurchaseReqDt.UnitPrice = itemPurchase.Price * entityPurchaseReqDt.ConversionFactor;
                        entityPurchaseReqDt.DiscountPercentage = itemPurchase.Discount;
                    }
                    else
                    {
                        entityPurchaseReqDt.UnitPrice = Convert.ToDecimal(0.00);
                        entityPurchaseReqDt.DiscountPercentage = Convert.ToDecimal(0.00);
                    }
                    ctx.CommandType = CommandType.Text;
                    ctx.Command.Parameters.Clear();
                    entityPurchaseReqDt.CreatedBy = AppSession.UserLogin.UserID;
                    entityPurchaseRequestDtDao.Insert(entityPurchaseReqDt);
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