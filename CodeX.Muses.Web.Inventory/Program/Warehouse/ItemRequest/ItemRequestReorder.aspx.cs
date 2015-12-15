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
    public partial class ItemRequestReorder : BasePageTrx
    {
        private string[] lstSelectedMember = null;
        private string[] lstQty = null;
        private string[] lstGCItemUnit = null;
        private string[] lstItemUnit = null;
        private string[] lstConversionFactor = null;
        public override string OnGetMenuCode()
        {
            if (Page.Request.QueryString.Count > 0 && Page.Request.QueryString["type"] == "cs")
                return Constant.MenuCode.Inventory.REORDER_ITEM_REQUEST_CROSS_SITE;
            return Constant.MenuCode.Inventory.REORDER_ITEM_REQUEST;
        }

        #region Html Getter
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
            return string.Format("{0};0;{1};", AppSession.UserLogin.SiteID, hdnTransactionCodeItemDistribution.Value);
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
                hdnTransactionCode.Value = Constant.TransactionCode.ITEM_REQUEST_CROSS_SITE;
                hdnTransactionCodeItemDistribution.Value = Constant.TransactionCode.ITEM_DISTRIBUTION_CROSS_SITE;
            }
            else
            {
                hdnTransactionCode.Value = Constant.TransactionCode.ITEM_REQUEST;
                hdnTransactionCodeItemDistribution.Value = Constant.TransactionCode.ITEM_DISTRIBUTION;
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

                    BindLocation();
                }
            }
            lstUserLocation = BusinessLayer.GetLocationUserList(AppSession.UserLogin.SiteID, 0, hdnTransactionCodeItemDistribution.Value, "");
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
            cboReorderType.SelectedIndex = 0;

            BindGridView(1, true, ref PageCount, ref RowCount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtNotes, new ControlEntrySetting(true, true, false));

            SetControlEntrySetting(hdnFromLocationID, new ControlEntrySetting(false, false, false, hdnDefaultLocationID.Value));
            SetControlEntrySetting(txtFromLocationCode, new ControlEntrySetting(true, false, true, hdnDefaultLocationCode.Value));
            SetControlEntrySetting(txtFromLocationName, new ControlEntrySetting(false, false, true, hdnDefaultLocationName.Value));
            SetControlEntrySetting(lblFromSiteServiceUnit, new ControlEntrySetting(true, false));
            SetControlEntrySetting(hdnFromSiteServiceUnitID, new ControlEntrySetting(false, false, false, hdnDefaultSiteServiceUnitID.Value));
            SetControlEntrySetting(txtFromServiceUnitCode, new ControlEntrySetting(true, false, true, hdnDefaultServiceUnitCode.Value));
            SetControlEntrySetting(txtFromServiceUnitName, new ControlEntrySetting(false, false, true, hdnDefaultServiceUnitName.Value));

            SetControlEntrySetting(lblToSiteServiceUnit, new ControlEntrySetting(true, false));
            SetControlEntrySetting(hdnToSiteServiceUnitID, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtToServiceUnitCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtToServiceUnitName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(hdnLstFilterFromLocationItemGroup, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(hdnLstFilterToLocationItemGroup, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtItemOrderDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(txtItemOrderTime, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.TIME_FORMAT)));
            
        }

        #region Load
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vItemMaster entity = e.Row.DataItem as vItemMaster;

                vItemRequestDtQtyOnOrderPerItemPerFromSiteServiceUnit entityQtyOnOrder = lstQtyOnOrder.FirstOrDefault(p => p.ItemID == entity.ItemID);

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
                hdnGCItemUnit.Value = itemPlanning.GCDistributionUnit;
                hdnItemUnit.Value = itemPlanning.DistributionUnit;
                lblItemUnit.InnerText = string.Format("{0} ({1})", itemPlanning.DistributionUnit, itemPlanning.DistributionUnitConversionFactor.ToString("G29"));
                hdnConversionFactor.Value = itemPlanning.DistributionUnitConversionFactor.ToString();

                decimal conversionFactor = itemPlanning.DistributionUnitConversionFactor;
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
                filterExpression = string.Format("ItemID IN (SELECT ItemID FROM ItemBalance WHERE LocationID IN ({0}) AND GCReorderType = '{1}' AND IsDeleted = 0 GROUP BY ItemID HAVING SUM(QuantityEND) <= SUM(QuantityMIN)) AND IsDeleted = 0", hdnLstLocationID.Value, cboReorderType.Value);

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

            if (lstItemID != "" && hdnLstLocationID.Value != "")
                lstItemBalance = BusinessLayer.GetItemBalanceList(string.Format("LocationID IN ({0}) AND ItemID IN ({1}) AND GCReorderType = '{2}' AND IsDeleted = 0", hdnLstLocationID.Value, lstItemID, cboReorderType.Value));
            else
                lstItemBalance = new List<ItemBalance>();
            if (lstItemID != "")
                lstItemPlanning = BusinessLayer.GetvItemPlanningCustomList(string.Format("SiteID = '{0}' AND ItemID IN ({1}) AND IsDeleted = 0", AppSession.UserLogin.SiteID, lstItemID));
            else
                lstItemPlanning = new List<vItemPlanningCustom>();
            if (lstItemID != "" && hdnFromSiteServiceUnitID.Value != "" && hdnFromSiteServiceUnitID.Value != "0")
                lstQtyOnOrder = BusinessLayer.GetvItemRequestDtQtyOnOrderPerItemPerFromSiteServiceUnitList(string.Format("FromSiteServiceUnitID = {0} AND ItemID IN ({1})", hdnFromSiteServiceUnitID.Value, lstItemID));
            else
                lstQtyOnOrder = new List<vItemRequestDtQtyOnOrderPerItemPerFromSiteServiceUnit>();

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
                vItemRequestDtQtyOnOrderPerItemPerFromSiteServiceUnit entityQtyOnOrder = lstQtyOnOrder.FirstOrDefault(p => p.ItemID == entity.ItemID);

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
                hdnGCItemUnit.Value = itemPlanning.GCDistributionUnit;
                lblItemUnit.InnerText = string.Format("{0} ({1})", itemPlanning.DistributionUnit, itemPlanning.DistributionUnitConversionFactor.ToString("G29"));
                hdnConversionFactor.Value = itemPlanning.DistributionUnitConversionFactor.ToString();

                decimal conversionFactor = itemPlanning.DistributionUnitConversionFactor;
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
        List<vItemRequestDtQtyOnOrderPerItemPerFromSiteServiceUnit> lstQtyOnOrder = null;
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
        public void SaveItemRequestHd(IDbContext ctx, ref int itemReqID, ref string retval)
        {
            ItemRequestHdDao entityHdDao = new ItemRequestHdDao(ctx);
            ItemRequestHd entityHd = new ItemRequestHd();
            entityHd.FromSiteServiceUnitID = Convert.ToInt32(hdnFromSiteServiceUnitID.Value);
            entityHd.FromLocationID = Convert.ToInt32(hdnFromLocationID.Value);
            entityHd.ToSiteServiceUnitID = Convert.ToInt32(hdnToSiteServiceUnitID.Value);
            entityHd.TransactionDate = Helper.GetDatePickerValue(txtItemOrderDate.Text);
            entityHd.TransactionTime = txtItemOrderTime.Text;
            entityHd.Remarks = txtNotes.Text;
            entityHd.TransactionCode = hdnTransactionCode.Value;
            entityHd.ItemRequestNo = BusinessLayer.GenerateTransactionNo(entityHd.TransactionCode, entityHd.TransactionDate, ctx);
            retval = entityHd.ItemRequestNo;
            entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
            ctx.CommandType = CommandType.Text;
            ctx.Command.Parameters.Clear();
            entityHd.CreatedBy = AppSession.UserLogin.UserID;
            entityHdDao.Insert(entityHd);
            itemReqID = BusinessLayer.GetItemRequestHdMaxID(ctx);
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage, ref string retval)
        {
            bool result = true;
            String[] paramID = hdnSelectedMember.Value.Substring(1).Split('|');
            String[] paramQty = hdnListQty.Value.Substring(1).Split('|');
            String[] paramGCItemUnit = hdnListGCItemUnit.Value.Substring(1).Split('|');
            String[] paramConversionFactor = hdnListConversionFactor.Value.Substring(1).Split('|');
            IDbContext ctx = DbFactory.Configure(true);
            int itemRequestID = 0;
            ItemRequestDtDao entityItemRequestDtDao = new ItemRequestDtDao(ctx);
            try
            {
                SaveItemRequestHd(ctx, ref itemRequestID, ref retval);

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
                    ItemRequestDt entityItemReqDt = new ItemRequestDt();
                    entityItemReqDt.ItemRequestID = itemRequestID;
                    entityItemReqDt.ItemID = entityItemMaster.ItemID;
                    entityItemReqDt.Quantity = Convert.ToDecimal(paramQty[ct]);
                    entityItemReqDt.GCItemUnit = paramGCItemUnit[ct];
                    entityItemReqDt.GCBaseUnit = entityItemMaster.GCItemUnit;
                    entityItemReqDt.ConversionFactor = Convert.ToDecimal(paramConversionFactor[ct]);
                    entityItemReqDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                    entityItemReqDt.CreatedBy = AppSession.UserLogin.UserID;
                    entityItemRequestDtDao.Insert(entityItemReqDt);
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

        private void BindLocation()
        {
            Repeater rptLocation = (Repeater)ddeLocation.FindControl("rptLocation");
            string filterExpression = "1 = 0";
            if (hdnFromSiteServiceUnitID.Value != "")
                filterExpression = string.Format("LocationID IN (SELECT LocationID FROM vServiceUnitLocationCustom WHERE SiteServiceUnitID = {0} AND IsHeader = 0) AND IsDeleted = 0", hdnFromSiteServiceUnitID.Value);
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
                chkLocation.Checked = true;
                chkLocation.Attributes.Add("locationname", obj.LocationName);
                chkLocation.Attributes.Add("locationid", obj.LocationID.ToString());
            }
        }
    }
}