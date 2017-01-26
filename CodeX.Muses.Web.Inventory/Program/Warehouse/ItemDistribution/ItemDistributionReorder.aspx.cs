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
using System.Web.UI.HtmlControls;
using CodeX.Common;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class ItemDistributionReorder : BasePageTrx
    {
        private string[] lstSelectedMember = null;
        private string[] lstQty = null;
        private string[] lstGCItemUnit = null;
        private string[] lstItemUnit = null;
        private string[] lstConversionFactor = null;
        public override string OnGetMenuCode()
        {
            if (Page.Request.QueryString.Count > 0 && Page.Request.QueryString["type"] == "cs")
                return Constant.MenuCode.Inventory.REORDER_ITEM_DISTRIBUTION_CROSS_SITE;
            return Constant.MenuCode.Inventory.REORDER_ITEM_DISTRIBUTION; 
        }

        #region Html Getter
        protected string OnGetReorderTypeStatic()
        {
            return Constant.ReorderType.STATIC;
        }
        protected string OnGetFilterExpressionFromServiceUnit()
        {
            if (hdnListFromSiteServiceUnitID.Value != "")
                return string.Format("SiteServiceUnitID IN ({0}) AND IsDeleted = 0", hdnListFromSiteServiceUnitID.Value);
            return "1 = 0";
        }
        protected string OnGetFilterExpressionToServiceUnit()
        {
            if (hdnListToSiteServiceUnitID.Value != "")
                return string.Format("SiteServiceUnitID IN ({0}) AND IsDeleted = 0", hdnListToSiteServiceUnitID.Value);
            return "1 = 0";
        }
        protected string OnGetFilterExpressionFromLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, hdnTransactionCode.Value);
        }
        protected string OnGetFilterExpressionToLocation()
        {
            return string.Format("{0};0;{1};", AppSession.UserLogin.SiteID, hdnTransactionCodeItemRequest.Value);
        }
        protected string OnGetFilterExpressionItemGroup()
        {
            return string.Format("GCItemType = '{0}' AND IsDeleted = 0", Constant.ItemType.PRODUCT);
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
            if (Page.Request.QueryString.Count > 0 && Page.Request.QueryString["type"] == "cs")
            {
                hdnTransactionCode.Value = Constant.TransactionCode.ITEM_DISTRIBUTION_CROSS_SITE;
                hdnTransactionCodeItemRequest.Value = Constant.TransactionCode.ITEM_REQUEST_CROSS_SITE;
            }
            else
            {
                hdnTransactionCode.Value = Constant.TransactionCode.ITEM_DISTRIBUTION;
                hdnTransactionCodeItemRequest.Value = Constant.TransactionCode.ITEM_REQUEST;
            }
            List<GetLocationUserList> lstUserLocation = BusinessLayer.GetLocationUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, hdnTransactionCode.Value, "");
            if (lstUserLocation.Count > 0)
            {
                List<ServiceUnitLocation> lstServiceUnitLocation = BusinessLayer.GetServiceUnitLocationList(string.Format("LocationID IN ({0})", string.Join(",", lstUserLocation.Select(p => p.LocationID).ToList())));
                hdnListFromSiteServiceUnitID.Value = string.Join(",", lstServiceUnitLocation.Select(p => p.SiteServiceUnitID).ToList());

                List<vSiteServiceUnit> lstSiteServiceUnit = BusinessLayer.GetvSiteServiceUnitList(OnGetFilterExpressionFromServiceUnit());
                if (lstSiteServiceUnit.Count == 1)
                {
                    vSiteServiceUnit serviceUnit = lstSiteServiceUnit.FirstOrDefault();
                    hdnFromSiteServiceUnitID.Value = hdnDefaultSiteServiceUnitID.Value = serviceUnit.SiteServiceUnitID.ToString();
                    hdnDefaultServiceUnitCode.Value = serviceUnit.ServiceUnitCode;
                    hdnDefaultServiceUnitName.Value = serviceUnit.ServiceUnitName;

                    if (lstUserLocation.Count == 1)
                    {
                        GetLocationUserList location = lstUserLocation.FirstOrDefault();
                        hdnDefaultLocationID.Value = location.LocationID.ToString();
                        hdnDefaultLocationCode.Value = location.LocationCode;
                        hdnDefaultLocationName.Value = location.LocationName;
                    }
                }
            }

            lstUserLocation = BusinessLayer.GetLocationUserList(AppSession.UserLogin.SiteID, 0, hdnTransactionCodeItemRequest.Value, "");
            if (lstUserLocation.Count > 0)
            {
                List<ServiceUnitLocation> lstServiceUnitLocation = BusinessLayer.GetServiceUnitLocationList(string.Format("LocationID IN ({0})", string.Join(",", lstUserLocation.Select(p => p.LocationID).ToList())));
                hdnListToSiteServiceUnitID.Value = string.Join(",", lstServiceUnitLocation.Select(p => p.SiteServiceUnitID).ToList());
            }
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();
            int PageCount = 1;
            int RowCount = 1;

            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.REORDER_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboReorderType, lstSc, "StandardCodeName", "StandardCodeID");
            StandardCode defaultSc = lstSc.FirstOrDefault(p => p.IsDefault);
            if(defaultSc == null)
                defaultSc = lstSc.FirstOrDefault();
            cboReorderType.Value = defaultSc.StandardCodeID;

            List<Variable> lstViewType = new List<Variable>();
            lstViewType.Add(new Variable { Code = "0", Value = GetLabel("Pengeluaran > Qty Akhir") });
            lstViewType.Add(new Variable { Code = "1", Value = GetLabel("Ada Pengeluaran") });
            lstViewType.Add(new Variable { Code = "2", Value = GetLabel("Semua Item") });
            Methods.SetComboBoxField<Variable>(cboViewType, lstViewType, "Value", "Code");
            cboViewType.SelectedIndex = 0;

            lstViewType = new List<Variable>();
            lstViewType.Add(new Variable { Code = "0", Value = GetLabel("Stok <= Minimum") });
            lstViewType.Add(new Variable { Code = "1", Value = GetLabel("Semua Item") });
            Methods.SetComboBoxField<Variable>(cboViewTypeStatic, lstViewType, "Value", "Code");
            cboViewTypeStatic.SelectedIndex = 0;

            BindGridView(1, true, ref PageCount, ref RowCount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(lblFromSiteServiceUnit, new ControlEntrySetting(true, false));
            SetControlEntrySetting(hdnFromSiteServiceUnitID, new ControlEntrySetting(true, false, true, hdnDefaultSiteServiceUnitID.Value));
            SetControlEntrySetting(txtFromServiceUnitCode, new ControlEntrySetting(true, false, true, hdnDefaultServiceUnitCode.Value));
            SetControlEntrySetting(txtFromServiceUnitName, new ControlEntrySetting(false, false, true, hdnDefaultServiceUnitName.Value));
            SetControlEntrySetting(lblFromLocation, new ControlEntrySetting(true, false));
            SetControlEntrySetting(hdnFromLocationID, new ControlEntrySetting(true, false, true, hdnDefaultLocationID.Value));
            SetControlEntrySetting(txtFromLocationCode, new ControlEntrySetting(true, false, true, hdnDefaultLocationCode.Value));
            SetControlEntrySetting(txtFromLocationName, new ControlEntrySetting(false, false, true, hdnDefaultLocationName.Value));
            SetControlEntrySetting(hdnLstFilterFromLocationItemGroup, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(hdnLstFilterToLocationItemGroup, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(lblToSiteServiceUnit, new ControlEntrySetting(true, false));
            SetControlEntrySetting(hdnToSiteServiceUnitID, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtToServiceUnitCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtToServiceUnitName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(lblToLocation, new ControlEntrySetting(true, false));
            SetControlEntrySetting(hdnToLocationID, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtToLocationCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtToLocationName, new ControlEntrySetting(false, false, true));

            SetControlEntrySetting(txtNotes, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtItemOrderDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(txtItemOrderTime, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.TIME_FORMAT)));
        }

        #region Load
        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnToLocationID.Value != "")
            {
                if (cboReorderType.Value.ToString() == Constant.ReorderType.STATIC)
                {
                    if (cboViewTypeStatic.Value.ToString() == "0")
                        filterExpression = string.Format("ItemID IN (SELECT ItemID FROM ItemBalance WHERE LocationID IN ({0}) AND GCReorderType = '{1}' AND IsDeleted = 0 GROUP BY ItemID HAVING SUM(QuantityEND) <= SUM(QuantityMIN)) AND ItemName1 LIKE '%{2}%' AND IsDeleted = 0", hdnToLocationID.Value, cboReorderType.Value, txtItemName.Text);
                    else
                        filterExpression = string.Format("ItemID IN (SELECT ItemID FROM ItemBalance WHERE LocationID IN ({0}) AND GCReorderType = '{1}' AND IsDeleted = 0 GROUP BY ItemID) AND ItemName1 LIKE '%{2}%' AND IsDeleted = 0", hdnToLocationID.Value, cboReorderType.Value, txtItemName.Text);
                }
            }

            if (isCountPageCount)
            {
                if (cboReorderType.Value.ToString() == Constant.ReorderType.STATIC)
                    rowCount = BusinessLayer.GetvItemMasterRowCount(filterExpression);
                else
                {
                    if (hdnToLocationID.Value != "")
                        rowCount = BusinessLayer.GetItemUsageItemRequestROPRowCount(Convert.ToInt32(hdnToLocationID.Value), hdnToLocationID.Value, txtItemName.Text, hdnItemGroupID.Value, cboViewType.Value.ToString());
                    else
                        rowCount = 0;
                }
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split('|');
            lstQty = hdnListQty.Value.Split('|');
            lstGCItemUnit = hdnListGCItemUnit.Value.Split('|');
            lstItemUnit = hdnListItemUnit.Value.Split('|');
            lstConversionFactor = hdnListConversionFactor.Value.Split('|');
            List<vItemMaster> lstEntity = null;
            List<GetItemUsageItemRequestROPList> lstEntity2 = null;
            string lstItemID = "";
            if (cboReorderType.Value.ToString() == Constant.ReorderType.STATIC)
            {
                lstEntity = BusinessLayer.GetvItemMasterList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ItemName1 ASC");
                lstItemID = string.Join(",", lstEntity.Select(p => p.ItemID).ToList());
            }
            else
            {
                if (hdnToLocationID.Value != "")
                    lstEntity2 = BusinessLayer.GetItemUsageItemRequestROPList(Convert.ToInt32(hdnToLocationID.Value), hdnToLocationID.Value, txtItemName.Text, hdnItemGroupID.Value, cboViewType.Value.ToString(), pageIndex, Constant.GridViewPageSize.GRID_MASTER);
                else
                    lstEntity2 = new List<GetItemUsageItemRequestROPList>();
                lstItemID = string.Join(",", lstEntity2.Select(p => p.ItemID).ToList());
            }

            if (lstItemID != "" && hdnToLocationID.Value != "")
                lstItemBalance = BusinessLayer.GetItemBalanceList(string.Format("LocationID IN ({0}) AND ItemID IN ({1}) AND GCReorderType = '{2}' AND IsDeleted = 0", hdnToLocationID.Value, lstItemID, cboReorderType.Value));
            else
                lstItemBalance = new List<ItemBalance>();
            if (lstItemID != "")
                lstItemPlanning = BusinessLayer.GetvItemPlanningCustomList(string.Format("SiteID = '{0}' AND ItemID IN ({1}) AND IsDeleted = 0", AppSession.UserLogin.SiteID, lstItemID));
            else
                lstItemPlanning = new List<vItemPlanningCustom>();
            if (lstItemID != "" && hdnFromSiteServiceUnitID.Value != "" && hdnToLocationID.Value != "0")
                lstQtyOnOrder = BusinessLayer.GetvItemDistributionDtQtyOnOrderPerItemPerToLocationList(string.Format("ToLocationID = {0} AND ItemID IN ({1})", hdnToLocationID.Value, lstItemID));
            else
                lstQtyOnOrder = new List<vItemDistributionDtQtyOnOrderPerItemPerToLocation>();

            filterExpression = "1 = 0";
            if (hdnFromLocationID.Value != "" && lstItemID != "")
                filterExpression = string.Format("LocationID = {0} AND ItemID IN ({1}) AND IsDeleted = 0", hdnFromLocationID.Value, lstItemID);
            lstItemBalanceFromLocation = BusinessLayer.GetItemBalanceList(filterExpression);

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

        List<ItemBalance> lstItemBalanceFromLocation = null;
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vItemMaster entity = e.Row.DataItem as vItemMaster;

                vItemDistributionDtQtyOnOrderPerItemPerToLocation entityQtyOnOrder = lstQtyOnOrder.FirstOrDefault(p => p.ItemID == entity.ItemID);

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
                HtmlGenericControl divFromLocationQty = (HtmlGenericControl)e.Row.FindControl("divFromLocationQty");

                decimal quantityMIN = lstItemBalance1.Sum(p => p.QuantityMIN);
                decimal quantityMAX = lstItemBalance1.Sum(p => p.QuantityMAX);
                decimal quantityEND = lstItemBalance1.Sum(p => p.QuantityEND);

                divMinimum.InnerHtml = quantityMIN.ToString("0.00");
                divMaximum.InnerHtml = quantityMAX.ToString("0.00");
                lblEndingBalance.InnerHtml = quantityEND.ToString("0.00");
                lblQtyOnOrder.InnerHtml = qtyOnOrder.ToString("0.00");
                hdnGCItemUnit.Value = itemPlanning.GCDistributionUnit;
                hdnItemUnit.Value = itemPlanning.DistributionUnit;
                lblItemUnit.InnerText = string.Format("{0} ({1})", itemPlanning.DistributionUnit, itemPlanning.DistributionUnitConversionFactor.ToString("G29"));
                hdnConversionFactor.Value = itemPlanning.DistributionUnitConversionFactor.ToString();

                decimal conversionFactor = itemPlanning.DistributionUnitConversionFactor;
                Decimal autoQty = 0;
                if (quantityEND <= quantityMIN)
                {
                    autoQty = (quantityMAX - quantityEND - qtyOnOrder);
                    if (autoQty < 0) autoQty = 0;
                }
                ItemBalance itemBalanceFromLocation = lstItemBalanceFromLocation.FirstOrDefault(p => p.ItemID == entity.ItemID);
                if (itemBalanceFromLocation != null)
                {
                    if (autoQty > itemBalanceFromLocation.QuantityEND)
                        autoQty = itemBalanceFromLocation.QuantityEND;
                    txtQty.Attributes.Add("max", itemBalanceFromLocation.QuantityEND.ToString());

                    divFromLocationQty.InnerHtml = itemBalanceFromLocation.QuantityEND.ToString("0.00");
                }
                else
                {
                    divFromLocationQty.InnerHtml = "0";
                }
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
                    txtTotalQty.Text = (Convert.ToDecimal(lstQty[idx]) * Convert.ToDecimal(lstConversionFactor[idx])).ToString("0.00");
                }
                else
                {
                    lblItemUnit.Attributes.Add("class", "lblItemUnit lblDisabled");
                    autoQty = Math.Ceiling(autoQty / conversionFactor);
                    txtQty.Text = autoQty.ToString("0.00");
                    txtTotalQty.Text = (autoQty * conversionFactor).ToString("0.00");
                }
            }
        }

        protected void grdView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GetItemUsageItemRequestROPList entity = e.Row.DataItem as GetItemUsageItemRequestROPList;
                vItemDistributionDtQtyOnOrderPerItemPerToLocation entityQtyOnOrder = lstQtyOnOrder.FirstOrDefault(p => p.ItemID == entity.ItemID);

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
                HtmlInputHidden hdnItemUnit = (HtmlInputHidden)e.Row.FindControl("hdnItemUnit");
                HtmlInputHidden hdnConversionFactor = (HtmlInputHidden)e.Row.FindControl("hdnConversionFactor");
                HtmlGenericControl lblItemUnit = (HtmlGenericControl)e.Row.FindControl("lblItemUnit");
                HtmlGenericControl divFromLocationQty = (HtmlGenericControl)e.Row.FindControl("divFromLocationQty");

                lblQtyOnOrder.InnerHtml = qtyOnOrder.ToString("0.00");
                lblEndingBalance.InnerHtml = quantityEND.ToString("0.00");
                hdnGCItemUnit.Value = itemPlanning.GCDistributionUnit;
                hdnItemUnit.Value = itemPlanning.DistributionUnit;
                lblItemUnit.InnerText = string.Format("{0} ({1})", itemPlanning.DistributionUnit, itemPlanning.DistributionUnitConversionFactor.ToString("G29"));
                hdnConversionFactor.Value = itemPlanning.DistributionUnitConversionFactor.ToString();

                decimal conversionFactor = itemPlanning.DistributionUnitConversionFactor;
                Decimal autoQty = (entity.QtyOrder - qtyOnOrder - quantityEND);
                if (autoQty < 0) autoQty = 0;
                ItemBalance itemBalanceFromLocation = lstItemBalanceFromLocation.FirstOrDefault(p => p.ItemID == entity.ItemID);
                if (itemBalanceFromLocation != null)
                {
                    if (autoQty > itemBalanceFromLocation.QuantityEND)
                        autoQty = itemBalanceFromLocation.QuantityEND;
                    txtQty.Attributes.Add("max", itemBalanceFromLocation.QuantityEND.ToString());

                    divFromLocationQty.InnerHtml = itemBalanceFromLocation.QuantityEND.ToString("0.00");
                }
                else
                {
                    divFromLocationQty.InnerHtml = "0";
                }

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
                    txtTotalQty.Text = (Convert.ToDecimal(lstQty[idx]) * Convert.ToDecimal(lstConversionFactor[idx])).ToString("0.00");
                }
                else
                {
                    lblItemUnit.Attributes.Add("class", "lblItemUnit lblDisabled");
                    autoQty = Math.Ceiling(autoQty / conversionFactor);
                    txtQty.Text = autoQty.ToString("0.00");
                    txtTotalQty.Text = (autoQty * conversionFactor).ToString("0.00");
                }
            }
        }

        List<vItemPlanningCustom> lstItemPlanning = null;
        List<ItemBalance> lstItemBalance = null;
        List<vItemDistributionDtQtyOnOrderPerItemPerToLocation> lstQtyOnOrder = null;
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
        public void SaveItemDistributionHd(IDbContext ctx, ref int distributionID, ref string retval)
        {
            ItemDistributionHdDao entityHdDao = new ItemDistributionHdDao(ctx);
            ItemDistributionHd entityHd = new ItemDistributionHd();
            entityHd.FromSiteServiceUnitID = Convert.ToInt32(hdnFromSiteServiceUnitID.Value);
            entityHd.ToSiteServiceUnitID = Convert.ToInt32(hdnToSiteServiceUnitID.Value);
            entityHd.FromLocationID = Convert.ToInt32(hdnFromLocationID.Value);
            entityHd.ToLocationID = Convert.ToInt32(hdnToLocationID.Value);
            entityHd.DeliveryDate = Helper.GetDatePickerValue(txtItemOrderDate.Text);
            entityHd.DeliveryTime = txtItemOrderTime.Text;
            entityHd.DeliveryRemarks = txtNotes.Text;
            entityHd.TransactionCode = hdnTransactionCode.Value;
            entityHd.DistributionNo = BusinessLayer.GenerateTransactionNo(entityHd.TransactionCode, entityHd.DeliveryDate, ctx);
            retval = entityHd.DistributionNo;
            entityHd.GCDistributionStatus = Constant.DistributionStatus.OPEN;
            ctx.CommandType = CommandType.Text;
            ctx.Command.Parameters.Clear();
            entityHd.CreatedBy = AppSession.UserLogin.UserID;
            entityHdDao.Insert(entityHd);
            distributionID = BusinessLayer.GetItemDistributionHdMaxID(ctx);
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage, ref string retval)
        {
            bool result = true;
            String[] paramID = hdnSelectedMember.Value.Substring(1).Split('|');
            String[] paramQty = hdnListQty.Value.Substring(1).Split('|');
            String[] paramGCItemUnit = hdnListGCItemUnit.Value.Substring(1).Split('|');
            String[] paramConversionFactor = hdnListConversionFactor.Value.Substring(1).Split('|');
            IDbContext ctx = DbFactory.Configure(true);

            int distributionID = 0;
            ItemDistributionDtDao entityItemDistributionDtDao = new ItemDistributionDtDao(ctx);
            try
            {
                SaveItemDistributionHd(ctx, ref distributionID, ref retval);
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
                    ItemDistributionDt entityItemDistributionDt = new ItemDistributionDt();
                    entityItemDistributionDt.DistributionID = distributionID;
                    entityItemDistributionDt.ItemID = entityItemMaster.ItemID;
                    entityItemDistributionDt.Quantity = Convert.ToDecimal(paramQty[ct]);
                    entityItemDistributionDt.GCItemUnit = paramGCItemUnit[ct];
                    entityItemDistributionDt.GCBaseUnit = entityItemMaster.GCItemUnit;
                    entityItemDistributionDt.ConversionFactor = Convert.ToDecimal(paramConversionFactor[ct]);
                    entityItemDistributionDt.GCItemDetailStatus = Constant.DistributionStatus.OPEN;
                    entityItemDistributionDt.CreatedBy = AppSession.UserLogin.UserID;
                    entityItemDistributionDtDao.Insert(entityItemDistributionDt);
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