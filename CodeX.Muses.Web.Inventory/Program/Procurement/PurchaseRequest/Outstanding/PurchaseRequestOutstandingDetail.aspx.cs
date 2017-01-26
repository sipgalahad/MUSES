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
using DevExpress.Web.ASPxEditors;
using System.IO;
using System.Text.RegularExpressions;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class PurchaseRequestOutstandingDetail : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        private string[] lstSelectedMember = null;
        private string[] lstItemName = null;
        private string[] lstDiscount1 = null;
        private string[] lstDiscount2 = null;
        private string[] lstPrice = null;
        private string[] lstQty = null;
        private string[] lstSupplierID = null;
        private string[] lstSupplierName = null;
        private string[] lstNonMasterSupplierName = null;
        private string[] lstIsFromMasterSupplier = null;
        private string[] lstGCPurchaseUnit = null;
        private string[] lstPurchaseUnit = null;
        private string[] lstConversionFactor = null;
        private string[] lstTermID = null;
        private string[] lstSupplierItemName = null;
        private string[] lstGCPurchaseMethod = null;

        protected string filterExpressionSupplier = "";

        protected string OnGetFilterExpressionLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.PURCHASE_ORDER);
        }
        protected string OnGetPurchaseMethodDirectPurchase()
        {
            return Constant.PurchaseMethod.DIRECT_PURCHASE;
        }
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.APPROVED_PURCHASE_REQUEST;
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
            filterExpressionSupplier = string.Format("GCBusinessPartnerType = '{0}' AND IsDeleted = 0", Constant.BusinessObjectType.SUPPLIER);
            hdnPurchaseRequestID.Value = Page.Request.QueryString["id"];
            vPurchaseRequestHd entityPurchaseRequest = BusinessLayer.GetvPurchaseRequestHdList(String.Format("PurchaseRequestID = '{0}'", Convert.ToInt32(hdnPurchaseRequestID.Value)))[0];

            EntityToControl(entityPurchaseRequest);

            List<StandardCode> listStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}','{2}','{3}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PURCHASE_ORDER_TYPE, Constant.StandardCode.FRANCO_REGION, Constant.StandardCode.CURRENCY_CODE, Constant.StandardCode.DIRECT_PURCHASE_TYPE));
            StandardCode scDefaultPurchaseOrderType = listStandardCode.FirstOrDefault(p => p.ParentID == Constant.StandardCode.PURCHASE_ORDER_TYPE && p.IsDefault);
            if (scDefaultPurchaseOrderType == null)
                scDefaultPurchaseOrderType = listStandardCode.FirstOrDefault(p => p.ParentID == Constant.StandardCode.PURCHASE_ORDER_TYPE);
            StandardCode scDefaultFrancoRegion = listStandardCode.FirstOrDefault(p => p.ParentID == Constant.StandardCode.FRANCO_REGION && p.IsDefault);
            if (scDefaultFrancoRegion == null)
                scDefaultFrancoRegion = listStandardCode.FirstOrDefault(p => p.ParentID == Constant.StandardCode.FRANCO_REGION);
            StandardCode scDefaultCurrencyCode = listStandardCode.FirstOrDefault(p => p.ParentID == Constant.StandardCode.CURRENCY_CODE && p.IsDefault);
            if (scDefaultCurrencyCode == null)
                scDefaultCurrencyCode = listStandardCode.FirstOrDefault(p => p.ParentID == Constant.StandardCode.CURRENCY_CODE);
            StandardCode scDefaultDirectPurchaseType = listStandardCode.FirstOrDefault(p => p.ParentID == Constant.StandardCode.DIRECT_PURCHASE_TYPE && p.IsDefault);
            if (scDefaultDirectPurchaseType == null)
                scDefaultDirectPurchaseType = listStandardCode.FirstOrDefault(p => p.ParentID == Constant.StandardCode.PURCHASE_ORDER_TYPE);

            hdnNonMasterSupplierID.Value = BusinessLayer.GetSettingParameter(Constant.SettingParameter.NON_MASTER_SUPPLIER).ParameterValue;
            hdnDefaultPurchaseOrderType.Value = entityPurchaseRequest.GCPurchaseOrderType;
            hdnDefaultFrancoRegion.Value = scDefaultFrancoRegion.StandardCodeID;
            hdnDefaultCurrencyCode.Value = scDefaultCurrencyCode.StandardCodeID;
            hdnDefaultDirectPurchaseType.Value = scDefaultDirectPurchaseType.StandardCodeID;
        }

        private void EntityToControl(vPurchaseRequestHd entity)
        {
            hdnPurchaseRequestID.Value = entity.PurchaseRequestID.ToString();
            txtOrderNo.Text = entity.PurchaseRequestNo;
            txtItemOrderDate.Text = entity.TransactionDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtItemOrderTime.Text = entity.TransactionTime;
            hdnSiteServiceUnitID.Value = entity.SiteServiceUnitID.ToString();
            txtServiceUnitCode.Text = entity.ServiceUnitCode;
            txtServiceUnitName.Text = entity.ServiceUnitName;
            hdnToSiteServiceUnitID.Value = entity.ToSiteServiceUnitID.ToString();
            txtToServiceUnitCode.Text = entity.ToServiceUnitCode;
            txtToServiceUnitName.Text = entity.ToServiceUnitName;
            txtNotes.Text = entity.Remarks;

            List<vPurchaseRequestDt> lstEntityDt = BusinessLayer.GetvPurchaseRequestDtList(string.Format("PurchaseRequestID = {0} AND GCItemDetailStatus = '{1}' AND IsSelected = 1 AND IsDeleted = 0", hdnPurchaseRequestID.Value, Constant.TransactionStatus.APPROVED));
            if (lstEntityDt.Count > 0)
            {
                hdnSelectedMember.Value = string.Format("|{0}", string.Join("|", lstEntityDt.Select(p => p.ID).ToList()));
                hdnListConversionFactor.Value = string.Format("|{0}", string.Join("|", lstEntityDt.Select(p => p.ProcessConversionFactor).ToList()));
                hdnDiscount1.Value = string.Format("|{0}", string.Join("|", lstEntityDt.Select(p => p.ProcessDiscountPercentage1).ToList()));
                hdnDiscount2.Value = string.Format("|{0}", string.Join("|", lstEntityDt.Select(p => p.ProcessDiscountPercentage2).ToList()));
                hdnPrice.Value = string.Format("|{0}", string.Join("|", lstEntityDt.Select(p => p.ProcessUnitPrice).ToList()));
                hdnPurchaseOrderQty.Value = string.Format("|{0}", string.Join("|", lstEntityDt.Select(p => p.ProcessQuantity).ToList()));
                hdnListSupplierID.Value = string.Format("|{0}", string.Join("|", lstEntityDt.Select(p => p.ProcessBusinessPartnerID).ToList()));
                hdnListSupplierName.Value = string.Format("|{0}", string.Join("|", lstEntityDt.Select(p => p.ProcessBusinessPartnerName).ToList()));
                hdnListGCPurchaseUnit.Value = string.Format("|{0}", string.Join("|", lstEntityDt.Select(p => p.GCProcessUnit).ToList()));
                hdnListPurchaseUnit.Value = string.Format("|{0}", string.Join("|", lstEntityDt.Select(p => p.ProcessUnit).ToList()));
                hdnListTermID.Value = string.Format("|{0}", string.Join("|", lstEntityDt.Select(p => p.ProcessTermID).ToList()));
                hdnListSupplierItemName.Value = string.Format("|{0}", string.Join("|", lstEntityDt.Select(p => p.ProcessSupplierItemName).ToList()));
                hdnListGCPurchaseMethod.Value = string.Format("|{0}", string.Join("|", lstEntityDt.Select(p => p.GCPurchaseMethod).ToList()));
                hdnListNonMasterSupplierName.Value = string.Format("|{0}", string.Join("|", lstEntityDt.Select(p => p.ProcessBusinessPartnerName).ToList()));
                hdnListIsFromMasterSupplier.Value = string.Format("|{0}", string.Join("|", lstEntityDt.Select(p => p.cfIsFromMasterSupplier).ToList()));
                hdnListItemName.Value = string.Format("|{0}", string.Join("|", lstEntityDt.Select(p => p.ItemName1).ToList()));
            }

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            GetLocationItemGroupAndBindLocation(entity.SiteServiceUnitID);
            BindGridView(1, true, ref PageCount, ref RowCount);
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

        List<vDirectPurchaseDtQtyOnOrderPerItemPerSiteServiceUnit> lstDirectPurchaseQtyOnOrder = null;
        List<vPurchaseOrderDtQtyOnOrderPerItemPerSiteServiceUnit> lstPurchaseOrderQtyOnOrder = null;
        List<StandardCode> lstPurchaseMethod = null;
        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnPurchaseRequestID.Value != "")
                filterExpression = string.Format("PurchaseRequestID = {0} AND GCItemDetailStatus = '{1}' AND IsDeleted = 0", hdnPurchaseRequestID.Value, Constant.TransactionStatus.APPROVED);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvPurchaseRequestDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            lstConversionFactor = hdnListConversionFactor.Value.Split('|');
            lstItemName = hdnListItemName.Value.Split('|');
            lstSelectedMember = hdnSelectedMember.Value.Split('|');
            lstDiscount1 = hdnDiscount1.Value.Split('|');
            lstDiscount2 = hdnDiscount2.Value.Split('|');
            lstPrice = hdnPrice.Value.Split('|');
            lstQty = hdnPurchaseOrderQty.Value.Split('|');
            lstSupplierID = hdnListSupplierID.Value.Split('|');
            lstSupplierName = hdnListSupplierName.Value.Split('|');
            lstGCPurchaseUnit = hdnListGCPurchaseUnit.Value.Split('|');
            lstPurchaseUnit = hdnListPurchaseUnit.Value.Split('|');
            lstTermID = hdnListTermID.Value.Split('|');
            lstSupplierItemName = hdnListSupplierItemName.Value.Split('|');
            lstGCPurchaseMethod = hdnListGCPurchaseMethod.Value.Split('|');
            lstNonMasterSupplierName = hdnListNonMasterSupplierName.Value.Split('|');
            lstIsFromMasterSupplier = hdnListIsFromMasterSupplier.Value.Split('|');

            lstPurchaseMethod = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PURCHASE_METHOD));

            List<vPurchaseRequestDtOutstanding> lstEntity = BusinessLayer.GetvPurchaseRequestDtOutstandingList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, " ItemName1 ASC");

            string lstItemID = string.Join(",", lstEntity.Select(p => p.ItemID).ToList());
            if (hdnLstLocationID.Value != "" && lstItemID != "")
                lstItemBalance = BusinessLayer.GetItemBalanceList(string.Format("LocationID IN ({0}) AND ItemID IN ({1}) AND IsDeleted = 0", hdnLstLocationID.Value, lstItemID));
            else
                lstItemBalance = new List<ItemBalance>();
            if (lstItemID != "" && hdnToSiteServiceUnitID.Value != "" && hdnToSiteServiceUnitID.Value != "0")
            {
                lstPurchaseOrderQtyOnOrder = BusinessLayer.GetvPurchaseOrderDtQtyOnOrderPerItemPerSiteServiceUnitList(string.Format("SiteServiceUnitID = {0} AND ItemID IN ({1})", hdnToSiteServiceUnitID.Value, lstItemID));
                lstDirectPurchaseQtyOnOrder = BusinessLayer.GetvDirectPurchaseDtQtyOnOrderPerItemPerSiteServiceUnitList(string.Format("SiteServiceUnitID = {0} AND ItemID IN ({1})", hdnToSiteServiceUnitID.Value, lstItemID));
            }
            else
            {
                lstPurchaseOrderQtyOnOrder = new List<vPurchaseOrderDtQtyOnOrderPerItemPerSiteServiceUnit>();
                lstDirectPurchaseQtyOnOrder = new List<vDirectPurchaseDtQtyOnOrderPerItemPerSiteServiceUnit>();
            }

            lvwView.DataSource = lstEntity;
            lvwView.DataBind();

        }
        List<ItemBalance> lstItemBalance = null;
        protected void lvwView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                vPurchaseRequestDtOutstanding entity = e.Item.DataItem as vPurchaseRequestDtOutstanding;
                CheckBox chkIsSelected = (CheckBox)e.Item.FindControl("chkIsSelected");
                HtmlInputHidden hdnOriginalSupplierID = (HtmlInputHidden)e.Item.FindControl("hdnOriginalSupplierID");
                HtmlInputHidden hdnSupplierID = (HtmlInputHidden)e.Item.FindControl("hdnSupplierID");
                HtmlInputHidden hdnTermID = (HtmlInputHidden)e.Item.FindControl("hdnTermID");
                HtmlInputHidden hdnGCPurchaseUnit = (HtmlInputHidden)e.Item.FindControl("hdnGCPurchaseUnit");
                HtmlInputHidden hdnConversionFactor = (HtmlInputHidden)e.Item.FindControl("hdnConversionFactor");
                HtmlGenericControl lblItemName = (HtmlGenericControl)e.Item.FindControl("lblItemName");
                TextBox txtDiscount1 = (TextBox)e.Item.FindControl("txtDiscount1");
                TextBox txtDiscount2 = (TextBox)e.Item.FindControl("txtDiscount2");
                TextBox txtPurchaseQty = (TextBox)e.Item.FindControl("txtPurchaseQty");
                TextBox txtPrice = (TextBox)e.Item.FindControl("txtPrice");
                TextBox txtNonMasterSupplierName = (TextBox)e.Item.FindControl("txtNonMasterSupplierName");
                HtmlGenericControl lblSupplier = (HtmlGenericControl)e.Item.FindControl("lblSupplier");
                CheckBox chkIsFromMasterSupplier = (CheckBox)e.Item.FindControl("chkIsFromMasterSupplier");
                HtmlGenericControl lblPurchaseUnit = (HtmlGenericControl)e.Item.FindControl("lblPurchaseUnit");
                HtmlGenericControl lblPurchaseUnitPrice = (HtmlGenericControl)e.Item.FindControl("lblPurchaseUnitPrice");
                HtmlTableCell tdSupplierItemName = (HtmlTableCell)e.Item.FindControl("tdSupplierItemName");
                HtmlGenericControl lblStock = e.Item.FindControl("lblStock") as HtmlGenericControl;
                HtmlGenericControl lblQtyOnOrder = e.Item.FindControl("lblQtyOnOrder") as HtmlGenericControl;
                ASPxComboBox cboPurchaseMethod = (ASPxComboBox)e.Item.FindControl("cboPurchaseMethod");
                cboPurchaseMethod.ClientInstanceName = string.Format("cboPurchaseMethod{0}", e.Item.DataItemIndex);
                Methods.SetComboBoxField<StandardCode>(cboPurchaseMethod, lstPurchaseMethod, "StandardCodeName", "StandardCodeID");
                cboPurchaseMethod.Value = entity.GCPurchaseMethod;
                vPurchaseOrderDtQtyOnOrderPerItemPerSiteServiceUnit entityPurchaseOrderQtyOnOrder = lstPurchaseOrderQtyOnOrder.FirstOrDefault(p => p.ItemID == entity.ItemID);
                vDirectPurchaseDtQtyOnOrderPerItemPerSiteServiceUnit entityDirectPurchaseQtyOnOrder = lstDirectPurchaseQtyOnOrder.FirstOrDefault(p => p.ItemID == entity.ItemID);

                decimal qtyOnOrder = 0;
                if (entityPurchaseOrderQtyOnOrder != null)
                    qtyOnOrder += entityPurchaseOrderQtyOnOrder.QtyOnOrder;
                if (entityDirectPurchaseQtyOnOrder != null)
                    qtyOnOrder += entityDirectPurchaseQtyOnOrder.QtyOnOrder;
                lblStock.InnerHtml = lstItemBalance.Where(p => p.ItemID == entity.ItemID).Sum(p => p.QuantityEND).ToString();

                lblQtyOnOrder.InnerHtml = qtyOnOrder.ToString("0.00");

                if (entity.BusinessPartnerID == 0)
                    lblSupplier.InnerHtml = "Pilih Supplier";
                else
                    lblSupplier.InnerHtml = entity.BusinessPartnerName;
                txtPurchaseQty.Text = entity.Quantity.ToString();
                lblPurchaseUnit.InnerText = string.Format("{0} ({1})", entity.PurchaseUnit, entity.ConversionFactor.ToString("G29"));
                txtPrice.Text = entity.UnitPrice.ToString();
                txtDiscount1.Text = entity.DiscountPercentage.ToString();
                hdnOriginalSupplierID.Value = hdnSupplierID.Value = entity.BusinessPartnerID.ToString();
                hdnGCPurchaseUnit.Value = entity.GCPurchaseUnit;
                hdnConversionFactor.Value = entity.ConversionFactor.ToString();
                lblItemName.InnerHtml = entity.ItemName1;

                lblSupplier.Attributes.Remove("class");
                lblPurchaseUnit.Attributes.Remove("class");
                if (lstSelectedMember.Contains(entity.ID.ToString()))
                {
                    int idx = Array.IndexOf(lstSelectedMember, entity.ID.ToString());
                    lblItemName.InnerHtml = lstItemName[idx];
                    chkIsSelected.Checked = true;
                    txtPrice.ReadOnly = false;
                    txtDiscount1.ReadOnly = false;
                    txtDiscount2.ReadOnly = false;
                    txtPurchaseQty.ReadOnly = false;
                    txtDiscount1.Text = lstDiscount1[idx];
                    txtDiscount2.Text = lstDiscount2[idx];
                    txtPrice.Text = lstPrice[idx];
                    txtPurchaseQty.Text = lstQty[idx];
                    hdnConversionFactor.Value = lstConversionFactor[idx];
                    hdnOriginalSupplierID.Value = hdnSupplierID.Value = lstSupplierID[idx];
                    hdnTermID.Value = lstTermID[idx];
                    lblSupplier.InnerHtml = lstSupplierName[idx];
                    hdnGCPurchaseUnit.Value = lstGCPurchaseUnit[idx];
                    lblPurchaseUnit.InnerHtml = lstPurchaseUnit[idx];
                    tdSupplierItemName.InnerHtml = lstSupplierItemName[idx];
                    lblSupplier.Attributes.Add("class", "lblSupplier lblLink");
                    lblPurchaseUnit.Attributes.Add("class", "lblPurchaseUnit lblLink");
                    cboPurchaseMethod.Value = lstGCPurchaseMethod[idx];

                    chkIsFromMasterSupplier.Enabled = true;
                    txtNonMasterSupplierName.ReadOnly = false;
                    bool isFromMasterSupplier = lstIsFromMasterSupplier[idx] == "1";
                    if (isFromMasterSupplier)
                    {
                        chkIsFromMasterSupplier.Checked = true;
                        txtNonMasterSupplierName.Text = "";
                        txtNonMasterSupplierName.Style.Add("display", "none");
                        cboPurchaseMethod.ClientEnabled = true;
                    }
                    else
                    {
                        chkIsFromMasterSupplier.Checked = false;
                        txtNonMasterSupplierName.Text = lstNonMasterSupplierName[idx];
                        lblSupplier.InnerHtml = "Pilih Supplier";
                        lblSupplier.Style.Add("display", "none");
                        cboPurchaseMethod.ClientEnabled = false;
                    }               
                }
                else
                {
                    lblSupplier.Attributes.Add("class", "lblSupplier lblDisabled");
                    lblPurchaseUnit.Attributes.Add("class", "lblPurchaseUnit lblDisabled");
                    chkIsFromMasterSupplier.Enabled = false;
                    txtNonMasterSupplierName.ReadOnly = true;
                    chkIsFromMasterSupplier.Checked = true;
                    txtNonMasterSupplierName.Style.Add("display", "none");
                    cboPurchaseMethod.ClientEnabled = true;
                }

                if (lblSupplier.InnerHtml == "")
                    lblSupplier.InnerHtml = "Pilih Supplier";
                lblPurchaseUnitPrice.InnerHtml = lblPurchaseUnit.InnerHtml;
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

        private void SavePurchaseOrderHd(IDbContext ctx, ref int purchaseOrderID, ref string retval, vSupplier supplier)
        {
            PurchaseOrderHdDao entityHdDao = new PurchaseOrderHdDao(ctx);
            PurchaseOrderHd entityHd = new PurchaseOrderHd();
            BusinessPartnersDao entityBusinessPartnerDao = new BusinessPartnersDao(ctx);
            entityHd.TransactionCode = Constant.TransactionCode.PURCHASE_ORDER;
            entityHd.OrderDate = DateTime.Now;
            entityHd.PurchaseOrderNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.PURCHASE_ORDER, entityHd.OrderDate, ctx);
            ctx.CommandType = CommandType.Text;
            ctx.Command.Parameters.Clear();
            retval += "1^" + entityHd.PurchaseOrderNo + "^" + supplier.BusinessPartnerName + ";";
            entityHd.DeliveryDate = Helper.GetDatePickerValue(txtItemOrderDate.Text);
            entityHd.POExpiredDate = Helper.GetDatePickerValue(txtItemOrderDate.Text);
            entityHd.GCPurchaseOrderType = hdnDefaultPurchaseOrderType.Value;
            entityHd.BusinessPartnerID = supplier.BusinessPartnerID;
            entityHd.TermID = supplier.TermID > 0 ? supplier.TermID : 1;
            entityHd.GCFrancoRegion = hdnDefaultFrancoRegion.Value;
            entityHd.GCCurrencyCode = hdnDefaultCurrencyCode.Value;
            entityHd.CurrencyRate = Convert.ToDecimal(1.00);
            entityHd.IsIncludeVAT = false;
            entityHd.FinalDiscountPercentage = 0;
            entityHd.FinalDiscountAmount = 0;
            entityHd.VATAmount = 0;
            entityHd.VATPercentage = 0;
            entityHd.SiteServiceUnitID = Convert.ToInt32(hdnToSiteServiceUnitID.Value);
            entityHd.ToSiteServiceUnitID = Convert.ToInt32(hdnSiteServiceUnitID.Value);
            entityHd.DownPaymentAmount = Convert.ToDecimal(0.00);
            entityHd.TotalNetTransactionAmount = entityHd.TransactionAmount + entityHd.VATAmount - entityHd.FinalDiscountAmount - entityHd.DownPaymentAmount;
            
            entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
            entityHd.CreatedBy = AppSession.UserLogin.UserID;
            purchaseOrderID = entityHdDao.Insert(entityHd);
        }

        private void SaveDirectPurchaseHd(IDbContext ctx, ref int directPurchaseID, ref string retval, int BusinessPartnerID, String NonMasterSupplierName)
        {
            DirectPurchaseHdDao entityHdDao = new DirectPurchaseHdDao(ctx);
            DirectPurchaseHd entityHd = new DirectPurchaseHd();
            BusinessPartnersDao entityBusinessPartnerDao = new BusinessPartnersDao(ctx);
            entityHd.PurchaseDate = DateTime.Now;
            entityHd.DirectPurchaseNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.DIRECT_PURCHASE, entityHd.PurchaseDate, ctx);
            ctx.CommandType = CommandType.Text;
            ctx.Command.Parameters.Clear();
            entityHd.GCDirectPurchaseType = hdnDefaultDirectPurchaseType.Value;
            entityHd.BusinessPartnerID = BusinessPartnerID;
            if (BusinessPartnerID == Convert.ToInt32(hdnNonMasterSupplierID.Value))
            {
                entityHd.BusinessPartnerName = NonMasterSupplierName;
                retval += "2^" + entityHd.DirectPurchaseNo + "^" + NonMasterSupplierName + ";";
            }
            else
            {
                entityHd.BusinessPartnerName = null;
                retval += "2^" + entityHd.DirectPurchaseNo + "^" + entityBusinessPartnerDao.Get(BusinessPartnerID).BusinessPartnerName + ";";
            }
            entityHd.IsIncludeVAT = false;
            entityHd.FinalDiscountAmount = 0;
            entityHd.VATAmount = 0;
            entityHd.VATPercentage = 0;
            entityHd.LocationID = Convert.ToInt32(hdnLocationID.Value);
            entityHd.SiteServiceUnitID = Convert.ToInt32(hdnToSiteServiceUnitID.Value);
            entityHd.ToSiteServiceUnitID = Convert.ToInt32(hdnSiteServiceUnitID.Value);
            entityHd.TotalNetTransactionAmount = entityHd.TransactionAmount + entityHd.VATAmount - entityHd.FinalDiscountAmount;

            entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
            entityHd.CreatedBy = AppSession.UserLogin.UserID;
            directPurchaseID = entityHdDao.Insert(entityHd);
        }

        class CPurchaseRequest
        {
            public String ID { get; set; }
            public String Discount1 { get; set; }
            public String Discount2 { get; set; }
            public String Price { get; set; }
            public String QtyPO { get; set; }
            public String SupplierID { get; set; }
            public String SupplierName { get; set; }
            public String GCPurchaseUnit { get; set; }
            public String ConversionFactor { get; set; }
            public String TermID { get; set; }
            public String GCPurchaseMethod { get; set; }
        }

        #region Getter
        public string GetSelectedMember() { return hdnSelectedMember.Value; }
        public string GetPurchaseOrderQty() { return hdnPurchaseOrderQty.Value; }
        public string GetPrice() { return hdnPrice.Value; }
        public string GetDiscount1() { return hdnDiscount1.Value; }
        public string GetDiscount2() { return hdnDiscount2.Value; }
        public string GetListSupplierID() { return hdnListSupplierID.Value; }
        public string GetListGCPurchaseUnit() { return hdnListGCPurchaseUnit.Value; }
        public string GetListConversionFactor() { return hdnListConversionFactor.Value; }
        public string GetListTermID() { return hdnListTermID.Value; }
        #endregion

        protected override bool OnCustomButtonClick(string type, ref string errMessage, ref string retval)
        {
            retval = "";
            bool result = true;
            String[] paramID = hdnSelectedMember.Value.Substring(1).Split('|');
            String[] paramQuantityPO = hdnPurchaseOrderQty.Value.Substring(1).Split('|');
            String[] paramPrice = hdnPrice.Value.Substring(1).Split('|');
            String[] paramDiscount1 = hdnDiscount1.Value.Substring(1).Split('|');
            String[] paramDiscount2 = hdnDiscount2.Value.Substring(1).Split('|');
            String[] paramSupplierID = hdnListSupplierID.Value.Substring(1).Split('|');
            String[] paramGCPurchaseUnit = hdnListGCPurchaseUnit.Value.Substring(1).Split('|');
            String[] paramConversionFactor = hdnListConversionFactor.Value.Substring(1).Split('|');
            String[] paramTermID = hdnListTermID.Value.Substring(1).Split('|');
            String[] paramGCPurchaseMethod = hdnListGCPurchaseMethod.Value.Substring(1).Split('|');
            String[] paramNonMasterSupplierName = hdnListNonMasterSupplierName.Value.Substring(1).Split('|');

            List<CPurchaseRequest> listEntityTempPRAll = new List<CPurchaseRequest>();

            IDbContext ctx = DbFactory.Configure(true);
            int purchaseOrderID = 0;
            int directPurchaseID = 0;
            PurchaseOrderDtDao entityPurchaseOrderDtDao = new PurchaseOrderDtDao(ctx);
            DirectPurchaseDtDao entityDirectPurchaseDtDao = new DirectPurchaseDtDao(ctx);
            PurchaseRequestDtDao entityPurchaseRequestDtDao = new PurchaseRequestDtDao(ctx);
            PurchaseRequestHdDao entityPurchaseRequestHdDao = new PurchaseRequestHdDao(ctx);
            PurchaseRequestPODao entityPRPODao = new PurchaseRequestPODao(ctx);
            PurchaseRequestDPDao entityPRDPDao = new PurchaseRequestDPDao(ctx);
            try
            {
                if (type == "save")
                {
                    string filterExpression = string.Format("PurchaseRequestID = {0} AND GCItemDetailStatus = '{1}' AND IsDeleted = 0", hdnPurchaseRequestID.Value, Constant.TransactionStatus.APPROVED);
                    List<PurchaseRequestDt> lstEntityPurchaseReqDt = BusinessLayer.GetPurchaseRequestDtList(filterExpression, ctx);
                    for (int i = 0; i < paramID.Length; i++)
                    {
                        PurchaseRequestDt entityPurchaseReqDt = lstEntityPurchaseReqDt.FirstOrDefault(p => p.ID == Convert.ToInt32(paramID[i]));
                        entityPurchaseReqDt.IsSelected = true;
                        entityPurchaseReqDt.GCPurchaseMethod = paramGCPurchaseMethod[i];
                        entityPurchaseReqDt.ProcessBusinessPartnerID = Convert.ToInt32(paramSupplierID[i]);
                        entityPurchaseReqDt.ProcessBusinessPartnerName = paramNonMasterSupplierName[i];
                        entityPurchaseReqDt.ProcessQuantity = Convert.ToDecimal(paramQuantityPO[i]);
                        entityPurchaseReqDt.GCProcessUnit = paramGCPurchaseUnit[i];
                        entityPurchaseReqDt.ProcessConversionFactor = Convert.ToDecimal(paramConversionFactor[i]);
                        entityPurchaseReqDt.ProcessUnitPrice = Convert.ToDecimal(paramPrice[i]);
                        entityPurchaseReqDt.ProcessDiscountPercentage1 = Convert.ToDecimal(paramDiscount1[i]);
                        entityPurchaseReqDt.ProcessDiscountPercentage2 = Convert.ToDecimal(paramDiscount1[i]);
                        entityPurchaseReqDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityPurchaseRequestDtDao.Update(entityPurchaseReqDt);
                        lstEntityPurchaseReqDt.Remove(entityPurchaseReqDt);
                    }

                    foreach (PurchaseRequestDt entityPurchaseReqDt in lstEntityPurchaseReqDt)
                    {
                        if (entityPurchaseReqDt.IsSelected)
                        {
                            entityPurchaseReqDt.IsSelected = false;
                            entityPurchaseReqDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityPurchaseRequestDtDao.Update(entityPurchaseReqDt);
                        }
                    }
                }
                else if (type == "approve")
                {
                    string filterExpression = string.Format("PurchaseRequestID = {0} AND GCItemDetailStatus = '{1}' AND IsDeleted = 0", hdnPurchaseRequestID.Value, Constant.TransactionStatus.APPROVED);
                    List<PurchaseRequestDt> lstAllEntityPurchaseReqDt = BusinessLayer.GetPurchaseRequestDtList(filterExpression, ctx);
                    for (int i = 0; i < paramID.Length; i++)
                    {
                        CPurchaseRequest entityTempPR = new CPurchaseRequest();
                        entityTempPR.ID = paramID[i];
                        entityTempPR.QtyPO = paramQuantityPO[i];
                        entityTempPR.Discount1 = paramDiscount1[i];
                        entityTempPR.Discount2 = paramDiscount2[i];
                        entityTempPR.SupplierID = paramSupplierID[i];
                        entityTempPR.SupplierName = paramNonMasterSupplierName[i];
                        entityTempPR.Price = paramPrice[i];
                        entityTempPR.GCPurchaseUnit = paramGCPurchaseUnit[i];
                        entityTempPR.ConversionFactor = paramConversionFactor[i];
                        entityTempPR.TermID = paramTermID[i];
                        entityTempPR.GCPurchaseMethod = paramGCPurchaseMethod[i];
                        listEntityTempPRAll.Add(entityTempPR);
                        
                        PurchaseRequestDt entityPurchaseReqDt = lstAllEntityPurchaseReqDt.FirstOrDefault(p => p.ID == Convert.ToInt32(paramID[i]));
                        entityPurchaseReqDt.IsSelected = true;
                        entityPurchaseReqDt.GCPurchaseMethod = paramGCPurchaseMethod[i];
                        entityPurchaseReqDt.ProcessBusinessPartnerID = Convert.ToInt32(paramSupplierID[i]);
                        entityPurchaseReqDt.ProcessBusinessPartnerName = paramNonMasterSupplierName[i];
                        entityPurchaseReqDt.ProcessQuantity = Convert.ToDecimal(paramQuantityPO[i]);
                        entityPurchaseReqDt.GCProcessUnit = paramGCPurchaseUnit[i];
                        entityPurchaseReqDt.ProcessConversionFactor = Convert.ToDecimal(paramConversionFactor[i]);
                        entityPurchaseReqDt.ProcessUnitPrice = Convert.ToDecimal(paramPrice[i]);
                        entityPurchaseReqDt.ProcessDiscountPercentage1 = Convert.ToDecimal(paramDiscount1[i]);
                        entityPurchaseReqDt.ProcessDiscountPercentage2 = Convert.ToDecimal(paramDiscount1[i]);
                        entityPurchaseReqDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityPurchaseRequestDtDao.Update(entityPurchaseReqDt);
                        lstAllEntityPurchaseReqDt.Remove(entityPurchaseReqDt);
                    }

                    foreach (PurchaseRequestDt entityPurchaseReqDt in lstAllEntityPurchaseReqDt)
                    {
                        if (entityPurchaseReqDt.IsSelected)
                        {
                            entityPurchaseReqDt.IsSelected = false;
                            entityPurchaseReqDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityPurchaseRequestDtDao.Update(entityPurchaseReqDt);
                        }
                    }

                    var lstBusinessPartner = (from p in paramSupplierID
                                              select new { BusinessPartnerID = Convert.ToInt32(p) }).GroupBy(p => new { p.BusinessPartnerID }).Select(p => p.First()).ToList();

                    string lstBusinessPartnerID = string.Join(",", lstBusinessPartner.Select(p => p.BusinessPartnerID).ToList());
                    List<vSupplier> lstSupplier = BusinessLayer.GetvSupplierList(String.Format("BusinessPartnerID IN ({0})", lstBusinessPartnerID), ctx);

                    List<PurchaseRequestDt> lstEntityPurchaseReqDt = BusinessLayer.GetPurchaseRequestDtList(string.Format("ID IN ({0})", hdnSelectedMember.Value.Substring(1).Replace('|', ',')), ctx);

                    #region Purchase Order
                    List<CPurchaseRequest> listEntityTempPRPO = listEntityTempPRAll.Where(p => p.GCPurchaseMethod == Constant.PurchaseMethod.PURCHASE_ORDER).ToList();
                    for (int i = 0; i < lstBusinessPartner.Count; ++i)
                    {
                        List<CPurchaseRequest> lstCPRPerSupplier = listEntityTempPRPO.Where(p => p.SupplierID == lstBusinessPartner[i].BusinessPartnerID.ToString()).ToList();
                        if (lstCPRPerSupplier.Count > 0)
                        {
                            vSupplier supplier = lstSupplier.FirstOrDefault(p => p.BusinessPartnerID == (int?)lstBusinessPartner[i].BusinessPartnerID);
                            SavePurchaseOrderHd(ctx, ref purchaseOrderID, ref retval, supplier);

                            int ctrItem = 0;
                            foreach (CPurchaseRequest entityCPurchaseReqDt in lstCPRPerSupplier)
                            {
                                ctrItem++;
                                if (supplier.MaxPOItem > 0 && ctrItem > supplier.MaxPOItem)
                                {
                                    SavePurchaseOrderHd(ctx, ref purchaseOrderID, ref retval, supplier);
                                    ctrItem = 1;
                                }
                                PurchaseRequestDt entityPurchaseReqDt = lstEntityPurchaseReqDt.Where(p => p.ID.ToString() == entityCPurchaseReqDt.ID).ToList()[0];
                                entityPurchaseReqDt.GCItemDetailStatus = Constant.TransactionStatus.PROCESSED;
                                PurchaseOrderDt entityPurchaseOrderDt = new PurchaseOrderDt();
                                //entityPurchaseOrderDt.PurchaseRequestID = entityPurchaseReqDt.PurchaseRequestID;
                                entityPurchaseOrderDt.ItemID = entityPurchaseReqDt.ItemID;
                                entityPurchaseOrderDt.ItemName1 = entityPurchaseReqDt.ItemName1;
                                entityPurchaseOrderDt.Quantity = Convert.ToDecimal(entityCPurchaseReqDt.QtyPO);
                                entityPurchaseOrderDt.GCPurchaseUnit = entityCPurchaseReqDt.GCPurchaseUnit;
                                entityPurchaseOrderDt.GCBaseUnit = entityPurchaseReqDt.GCBaseUnit;
                                entityPurchaseOrderDt.ConversionFactor = Convert.ToDecimal(entityCPurchaseReqDt.ConversionFactor);
                                entityPurchaseOrderDt.UnitPrice = Convert.ToDecimal(entityCPurchaseReqDt.Price);
                                entityPurchaseOrderDt.DiscountPercentage1 = Convert.ToDecimal(entityCPurchaseReqDt.Discount1);
                                entityPurchaseOrderDt.DiscountPercentage2 = Convert.ToDecimal(entityCPurchaseReqDt.Discount2);
                                entityPurchaseOrderDt.IsBonusItem = false;

                                decimal lineAmount = entityPurchaseOrderDt.UnitPrice * entityPurchaseOrderDt.Quantity;
                                entityPurchaseOrderDt.DiscountAmount1 = (lineAmount * entityPurchaseOrderDt.DiscountPercentage1) / 100;
                                entityPurchaseOrderDt.DiscountAmount2 = ((lineAmount - entityPurchaseOrderDt.DiscountAmount1) * entityPurchaseOrderDt.DiscountPercentage2) / 100;
                                entityPurchaseOrderDt.LineAmount = lineAmount - entityPurchaseOrderDt.DiscountAmount1 - entityPurchaseOrderDt.DiscountAmount2;
                                entityPurchaseOrderDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                                entityPurchaseOrderDt.CreatedBy = AppSession.UserLogin.UserID;
                                entityPurchaseReqDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                                entityPurchaseRequestDtDao.Update(entityPurchaseReqDt);

                                entityPurchaseOrderDt.PurchaseOrderID = purchaseOrderID;
                                entityPurchaseOrderDtDao.Insert(entityPurchaseOrderDt);

                                PurchaseRequestPO entityPRPO = new PurchaseRequestPO();
                                entityPRPO.PurchaseOrderID = purchaseOrderID;
                                entityPRPO.ItemID = entityPurchaseOrderDt.ItemID;
                                entityPRPO.ItemName1 = entityPurchaseOrderDt.ItemName1;
                                entityPRPO.PurchaseRequestID = Convert.ToInt32(hdnPurchaseRequestID.Value);
                                entityPRPO.OrderQuantity = entityPurchaseOrderDt.Quantity;
                                entityPRPODao.Insert(entityPRPO);
                            }
                        }
                    }
                    #endregion

                    #region Direct Purchase
                    List<CPurchaseRequest> listEntityTempPRDP = listEntityTempPRAll.Where(p => p.GCPurchaseMethod == Constant.PurchaseMethod.DIRECT_PURCHASE).ToList();
                    for (int i = 0; i < lstBusinessPartner.Count; ++i)
                    {
                        if (lstBusinessPartner[i].BusinessPartnerID.ToString() == hdnNonMasterSupplierID.Value)
                        {
                            List<CPurchaseRequest> lstCPRPerSupplier = listEntityTempPRDP.Where(p => p.SupplierID == lstBusinessPartner[i].BusinessPartnerID.ToString()).ToList();
                            var lstBusinessPartner1 = (from p in lstCPRPerSupplier
                                                      select new { BusinessPartnerID = Convert.ToInt32(p.SupplierID), BusinessPartnerName = p.SupplierName }).GroupBy(p => new { p.BusinessPartnerID, p.BusinessPartnerName }).Select(p => p.First()).ToList();
                            for (int j = 0; j < lstBusinessPartner1.Count; ++j)
                            {
                                List<CPurchaseRequest> lstCPRPerSupplier1 = lstCPRPerSupplier.Where(p => p.SupplierName == lstBusinessPartner1[j].BusinessPartnerName).ToList();
                                SaveDirectPurchaseHdDt(ctx, entityPurchaseRequestDtDao, entityDirectPurchaseDtDao, entityPRDPDao, lstEntityPurchaseReqDt, lstCPRPerSupplier1, ref directPurchaseID, ref retval, lstBusinessPartner1[j].BusinessPartnerID, lstBusinessPartner1[j].BusinessPartnerName);
                            }
                        }
                        else
                        {
                            List<CPurchaseRequest> lstCPRPerSupplier = listEntityTempPRDP.Where(p => p.SupplierID == lstBusinessPartner[i].BusinessPartnerID.ToString()).ToList();
                            SaveDirectPurchaseHdDt(ctx, entityPurchaseRequestDtDao, entityDirectPurchaseDtDao, entityPRDPDao, lstEntityPurchaseReqDt, lstCPRPerSupplier, ref directPurchaseID, ref retval, lstBusinessPartner[i].BusinessPartnerID, "");
                        }
                    }
                    #endregion
                }
                else if (type == "decline")
                {
                    List<PurchaseRequestDt> lstEntityPurchaseReqDt = BusinessLayer.GetPurchaseRequestDtList(string.Format("ID IN ({0})", hdnSelectedMember.Value.Substring(1).Replace('|', ',')), ctx);
                    foreach (PurchaseRequestDt purchaseEntity in lstEntityPurchaseReqDt)
                    {
                        purchaseEntity.GCItemDetailStatus = Constant.TransactionStatus.VOID;
                        purchaseEntity.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityPurchaseRequestDtDao.Update(purchaseEntity);
                    }
                }
                int count = BusinessLayer.GetPurchaseRequestDtRowCount(string.Format("PurchaseRequestID = {0} AND GCItemDetailStatus = '{1}' AND IsDeleted = 0", hdnPurchaseRequestID.Value, Constant.TransactionStatus.APPROVED), ctx);
                retval += "|" + count;
                if (count == 0)
                {
                    PurchaseRequestHd entityPurchaseRequestHd = entityPurchaseRequestHdDao.Get(Convert.ToInt32(hdnPurchaseRequestID.Value));
                    entityPurchaseRequestHd.GCTransactionStatus = Constant.TransactionStatus.CLOSED;
                    entityPurchaseRequestHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityPurchaseRequestHdDao.Update(entityPurchaseRequestHd);
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

        private void SaveDirectPurchaseHdDt(IDbContext ctx, PurchaseRequestDtDao entityPurchaseRequestDtDao, DirectPurchaseDtDao entityDirectPurchaseDtDao, PurchaseRequestDPDao entityPRDPDao, 
            List<PurchaseRequestDt> lstEntityPurchaseReqDt, List<CPurchaseRequest> lstCPRPerSupplier, 
            ref int directPurchaseID, ref string retval, int BusinessPartnerID, string BusinessPartnerName)
        {
            if (lstCPRPerSupplier.Count > 0)
            {
                List<DirectPurchaseDt> lstDirectPurchaseDt = new List<DirectPurchaseDt>();

                SaveDirectPurchaseHd(ctx, ref directPurchaseID, ref retval, BusinessPartnerID, BusinessPartnerName);
                foreach (CPurchaseRequest entityCPurchaseReqDt in lstCPRPerSupplier)
                {
                    PurchaseRequestDt entityPurchaseReqDt = lstEntityPurchaseReqDt.Where(p => p.ID.ToString() == entityCPurchaseReqDt.ID).ToList()[0];
                    entityPurchaseReqDt.GCItemDetailStatus = Constant.TransactionStatus.PROCESSED;
                    DirectPurchaseDt entityDirectPurchaseDt = new DirectPurchaseDt();
                    //entityDirectPurchaseDt.PurchaseRequestID = entityPurchaseReqDt.PurchaseRequestID;
                    entityDirectPurchaseDt.ItemID = entityPurchaseReqDt.ItemID;
                    entityDirectPurchaseDt.ItemName1 = entityPurchaseReqDt.ItemName1;
                    entityDirectPurchaseDt.Quantity = Convert.ToDecimal(entityCPurchaseReqDt.QtyPO);
                    entityDirectPurchaseDt.GCItemUnit = entityCPurchaseReqDt.GCPurchaseUnit;
                    entityDirectPurchaseDt.GCBaseUnit = entityPurchaseReqDt.GCBaseUnit;
                    entityDirectPurchaseDt.ConversionFactor = Convert.ToDecimal(entityCPurchaseReqDt.ConversionFactor);
                    entityDirectPurchaseDt.UnitPrice = Convert.ToDecimal(entityCPurchaseReqDt.Price);

                    decimal lineAmount = entityDirectPurchaseDt.UnitPrice * entityDirectPurchaseDt.Quantity;
                    entityDirectPurchaseDt.DiscountAmount = 0;
                    entityDirectPurchaseDt.LineAmount = lineAmount - entityDirectPurchaseDt.DiscountAmount;
                    entityDirectPurchaseDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                    entityDirectPurchaseDt.CreatedBy = AppSession.UserLogin.UserID;
                    entityPurchaseReqDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityPurchaseRequestDtDao.Update(entityPurchaseReqDt);

                    entityDirectPurchaseDt.DirectPurchaseID = directPurchaseID;
                    entityDirectPurchaseDtDao.Insert(entityDirectPurchaseDt);

                    PurchaseRequestDP entityPRDP = new PurchaseRequestDP();
                    entityPRDP.DirectPurchaseID = directPurchaseID;
                    entityPRDP.ItemID = entityDirectPurchaseDt.ItemID;
                    entityPRDP.ItemName1 = entityDirectPurchaseDt.ItemName1;
                    entityPRDP.PurchaseRequestID = Convert.ToInt32(hdnPurchaseRequestID.Value);
                    entityPRDP.PurchaseQuantity = entityDirectPurchaseDt.Quantity;
                    entityPRDPDao.Insert(entityPRDP);
                }
            }
        }

        private void BindLocation()
        {
            Repeater rptLocation = (Repeater)ddeLocation.FindControl("rptLocation");
            string filterExpression = "1 = 0";
            if (hdnLstFilterLocationItemGroup.Value != "")
                filterExpression = string.Format("LocationID IN (SELECT LocationID FROM vLocationItemGroupPath WHERE {0}) AND IsDeleted = 0 AND IsHeader = 0", hdnLstFilterLocationItemGroup.Value);
            List<Location> lstLocation = BusinessLayer.GetLocationList(filterExpression);
            hdnLstLocationID.Value = string.Join(",", lstLocation.Select(p => p.LocationID).ToList());
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

        #region Import Data
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            if (param[0] == "upload")
            {
                string imageData = hdnUploadedFile1.Value;
                if (imageData != "")
                {
                    string[] parts = Regex.Split(imageData, ",").Skip(1).ToArray();
                    imageData = String.Join(",", parts);
                }

                byte[] data = Convert.FromBase64String(imageData);
                StreamReader stream = new StreamReader(new MemoryStream(data));

                if (OnUploadFile(stream, ref errMessage, panel))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            panel.JSProperties["cpResult"] = result;
        }

        public bool OnUploadFile(StreamReader stream, ref String errMessage, ASPxCallbackPanel panel)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseRequestDtDao entityDtDao = new PurchaseRequestDtDao(ctx);

            try
            {
                string lstID = "";
                string lstDiscount1 = "";
                string lstDiscount2 = "";
                string lstPrice = "";
                string lstGCPurchaseUnit = "";
                string lstPurchaseUnit = "";
                string lstPurchaseQty = "";
                string lstConversionFactor = "";
                string lstBusinessPartnerID = "";
                string lstBusinessPartnerName = "";
                string lstTermID = "";
                string lstSupplierItemName = "";
                string lstGCPurchaseMethod = "";
                string lstIsFromMasterSupplier = "";
                string lstNonMasterSupplierName = "";

                string filterExpression = string.Format("PurchaseRequestID = {0} AND GCItemDetailStatus = '{1}' AND IsDeleted = 0", hdnPurchaseRequestID.Value, Constant.TransactionStatus.APPROVED);
                List<vPurchaseRequestDtOutstanding> lstvPurchaseRequestDt = BusinessLayer.GetvPurchaseRequestDtOutstandingList(filterExpression, ctx);
                int ctr = 0;
                while (true)
                {
                    string line = stream.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    if (ctr > 5)
                    {
                        String[] lstTemp = line.Split(',');
                        string itemCode = lstTemp[1];

                        vPurchaseRequestDtOutstanding vPurchaseRequestDt = lstvPurchaseRequestDt.FirstOrDefault(p => p.ItemCode == itemCode);
                        if (vPurchaseRequestDt != null)
                        {
                            decimal qty = -1;
                            if (Decimal.TryParse(lstTemp[5], out qty))
                            {
                                lstID += string.Format("|{0}", vPurchaseRequestDt.ID);
                                lstDiscount1 += string.Format("|0");
                                lstDiscount2 += string.Format("|0");
                                lstPrice += string.Format("|0");
                                lstGCPurchaseUnit += string.Format("|{0}", vPurchaseRequestDt.GCPurchaseUnit);
                                lstPurchaseUnit += string.Format("|{0}", vPurchaseRequestDt.PurchaseUnit);
                                lstPurchaseQty += string.Format("|{0}", qty);
                                lstConversionFactor += string.Format("|{0}", vPurchaseRequestDt.ConversionFactor);
                                lstBusinessPartnerID += string.Format("|{0}", vPurchaseRequestDt.BusinessPartnerID);
                                lstBusinessPartnerName += string.Format("|{0}", vPurchaseRequestDt.BusinessPartnerName);
                                lstTermID += string.Format("|{0}", vPurchaseRequestDt.TermID);
                                lstSupplierItemName += string.Format("|{0}", vPurchaseRequestDt.SupplierItemName);
                                lstGCPurchaseMethod += string.Format("|{0}", Constant.PurchaseMethod.PURCHASE_ORDER);
                                lstIsFromMasterSupplier += "|1";
                                lstNonMasterSupplierName += "|";
                            }
                        }
                    }
                    ctr++;
                }
                panel.JSProperties["cpListID"] = lstID;
                panel.JSProperties["cpListDiscount1"] = lstDiscount1;
                panel.JSProperties["cpListDiscount2"] = lstDiscount2;
                panel.JSProperties["cpListPrice"] = lstPrice;
                panel.JSProperties["cpListGCPurchaseUnit"] = lstGCPurchaseUnit;
                panel.JSProperties["cpListPurchaseUnit"] = lstPurchaseUnit;
                panel.JSProperties["cpListPurchaseQty"] = lstPurchaseQty;
                panel.JSProperties["cpListConversionFactor"] = lstConversionFactor;
                panel.JSProperties["cpListBusinessPartnerID"] = lstBusinessPartnerID;
                panel.JSProperties["cpListBusinessPartnerName"] = lstBusinessPartnerName;
                panel.JSProperties["cpListTermID"] = lstTermID;
                panel.JSProperties["cpListSupplierName"] = lstSupplierItemName;
                panel.JSProperties["cpListGCPurchaseMethod"] = lstGCPurchaseMethod;
                panel.JSProperties["cpListIsFromMasterSupplier"] = lstIsFromMasterSupplier;
                panel.JSProperties["cpListNonMasterSupplierName"] = lstNonMasterSupplierName;
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