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
    public partial class DirectPurchaseEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.DIRECT_PURCHASE;
        }

        protected override void InitializeDataControl()
        {
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();
            List<SettingParameter> lstSettingParameter = BusinessLayer.GetSettingParameterList(string.Format("ParameterCode IN ('{0}','{1}','{2}')", Constant.SettingParameter.VAT_PERCENTAGE, Constant.SettingParameter.NON_MASTER_SUPPLIER, Constant.SettingParameter.NON_MASTER_ITEM));
            hdnVATPercentage.Value = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.VAT_PERCENTAGE).ParameterValue;
            hdnNonMasterSupplierID.Value = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.NON_MASTER_SUPPLIER).ParameterValue;
            hdnNonMasterItemID.Value = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.NON_MASTER_ITEM).ParameterValue;

            List<GetServiceUnitUserList> lstUserServiceUnit = BusinessLayer.GetServiceUnitUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, string.Format("SiteServiceUnitID IN (SELECT SiteServiceUnitID FROM vSiteServiceUnit WHERE IsAllowPurchase = 1)"));
            if (lstUserServiceUnit.Count == 1)
            {
                GetServiceUnitUserList serviceUnit = lstUserServiceUnit.FirstOrDefault();
                hdnDefaultSiteServiceUnitID.Value = serviceUnit.SiteServiceUnitID.ToString();
                hdnDefaultServiceUnitCode.Value = serviceUnit.ServiceUnitCode;
                hdnDefaultServiceUnitName.Value = serviceUnit.ServiceUnitName;
            }
            if (lstUserServiceUnit.Count > 0)
            {
                hdnListSiteServiceUnitID.Value = string.Join(",", lstUserServiceUnit.Select(p => p.SiteServiceUnitID).ToList());
                hdnRecordFilterExpression.Value = string.Format("SiteServiceUnitID IN ({0})", string.Join(",", lstUserServiceUnit.Select(p => p.SiteServiceUnitID).ToList()));
            }

            List<GetLocationUserList> lstUserLocation = BusinessLayer.GetLocationUserList(AppSession.UserLogin.SiteID, 0, Constant.TransactionCode.DIRECT_PURCHASE, "");
            if (lstUserLocation.Count > 0)
            {
                List<vServiceUnitLocation> lstServiceUnitLocation = BusinessLayer.GetvServiceUnitLocationList(string.Format("LocationID IN ({0})", string.Join(",", lstUserLocation.Select(p => p.LocationID).ToList())));
                hdnListToSiteServiceUnitID.Value = string.Join(",", lstServiceUnitLocation.Select(p => p.SiteServiceUnitID).ToList());

                if (lstServiceUnitLocation.Count == 1)
                {
                    vServiceUnitLocation serviceUnit = lstServiceUnitLocation.FirstOrDefault();
                    hdnDefaultToSiteServiceUnitID.Value = serviceUnit.SiteServiceUnitID.ToString();
                    hdnDefaultToServiceUnitCode.Value = serviceUnit.ServiceUnitCode;
                    hdnDefaultToServiceUnitName.Value = serviceUnit.ServiceUnitName;
                }
            }

            hdnGCTransactionStatus.Value = "";

            SetControlProperties();

            decimal tempTransactionAmount = -1;
            BindGridView(1, true, ref PageCount, ref RowCount, ref tempTransactionAmount);
            Helper.SetControlEntrySetting(txtNonMasterItemName, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtQuantity, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtItemGroupCode, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtItemCode, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboItemUnit, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboNonMasterItemUnit, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtPrice, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtDiscountPercentage, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtDiscountAmount, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        protected string GetVATPercentageLabel()
        {
            return hdnVATPercentage.Value;
        }

        #region Filter Expression Search Dialog
        protected string OnGetFilterExpressionServiceUnit()
        {
            if (hdnListSiteServiceUnitID.Value != "")
                return string.Format("SiteServiceUnitID IN ({0}) AND IsDeleted = 0", hdnListSiteServiceUnitID.Value);
            return "1 = 0";
        }
        protected string OnGetFilterExpressionToServiceUnit()
        {
            if (hdnListToSiteServiceUnitID.Value != "")
                return string.Format("SiteServiceUnitID IN ({0}) AND IsDeleted = 0", hdnListToSiteServiceUnitID.Value);
            return "1 = 0";
        }
        protected string OnGetFilterExpressionToLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.DIRECT_PURCHASE);
        }
        protected string OnGetFilterExpressionItemGroup()
        {
            return string.Format("GCItemType = '{0}' AND IsDeleted = 0", Constant.ItemType.PRODUCT);
        }
        protected string OnGetFilterExpressionItemProduct()
        {
            return string.Format("GCItemType = '{0}' AND GCItemStatus = '{1}' AND IsDeleted = 0", Constant.ItemType.PRODUCT, Constant.ItemStatus.ACTIVE);
        }
        protected string OnGetFilterExpressionSupplier()
        {
            return string.Format("GCBusinessPartnerType = '{0}' AND IsDeleted = 0", Constant.BusinessObjectType.SUPPLIER);
        }
        #endregion

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

        protected override void SetControlProperties()
        {
            Repeater rptSite = (Repeater)ddeSite.FindControl("rptSite");
            List<Site> lstSite = BusinessLayer.GetSiteList(string.Format("IsHeader = 0"));
            rptSite.DataSource = lstSite;
            rptSite.DataBind();

            List<StandardCode> listStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.DIRECT_PURCHASE_TYPE, Constant.StandardCode.ITEM_UNIT));
            Methods.SetComboBoxField<StandardCode>(cboDirectPurchaseType, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.DIRECT_PURCHASE_TYPE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            cboDirectPurchaseType.SelectedIndex = 0;

            Methods.SetComboBoxField<StandardCode>(cboNonMasterItemUnit, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.ITEM_UNIT).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnDirectPurchaseID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtDirectPurchaseNo, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtDirectPurchaseDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(lblLocation, new ControlEntrySetting(true, false));
            SetControlEntrySetting(lblSupplier, new ControlEntrySetting(true, false));
            SetControlEntrySetting(chkIsFromMasterSupplier, new ControlEntrySetting(true, false, false, true, true));
            SetControlEntrySetting(txtLocationCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtLocationName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtSupplierCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtSupplierName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtNonMasterSupplierName, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtServiceUnitCode, new ControlEntrySetting(true, true, true, hdnDefaultServiceUnitCode.Value));
            SetControlEntrySetting(lblSiteServiceUnit, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtServiceUnitName, new ControlEntrySetting(false, false, true, hdnDefaultServiceUnitName.Value));
            SetControlEntrySetting(txtToServiceUnitCode, new ControlEntrySetting(true, true, true, hdnDefaultToServiceUnitCode.Value));
            SetControlEntrySetting(lblToSiteServiceUnit, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtToServiceUnitName, new ControlEntrySetting(false, false, true, hdnDefaultToServiceUnitName.Value));

            SetControlEntrySetting(hdnLocationID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSiteServiceUnitID, new ControlEntrySetting(true, true, false, hdnDefaultSiteServiceUnitID.Value));
            SetControlEntrySetting(hdnToSiteServiceUnitID, new ControlEntrySetting(true, true, false, hdnDefaultToSiteServiceUnitID.Value));

            SetControlEntrySetting(cboDirectPurchaseType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtReferenceNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtReferenceDate, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));

            SetControlEntrySetting(txtTransactionAmount, new ControlEntrySetting(false, false, true, "0"));
            SetControlEntrySetting(txtPPN, new ControlEntrySetting(false, false, true, "0"));
            SetControlEntrySetting(txtFinalDiscountPercentage, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(txtFinalDiscountAmount, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(txtTotalNetTransactionAmount, new ControlEntrySetting(false, false, true, "0"));
        }

        #region Load Entity
        public override void OnAddRecord()
        {
            hdnPageCount.Value = "0";
            hdnIsEditable.Value = "1";
            hdnGCTransactionStatus.Value = "";
            chkPPN.Checked = false;
            chkIsFromMasterSupplier.Checked = true;
            chkIsFromMasterSupplier.Enabled = true;
        }
        protected string GetFilterExpression()
        {
            return hdnRecordFilterExpression.Value;
        }

        protected string IsEditable()
        {
            return hdnIsEditable.Value;
        }

        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvDirectPurchaseHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vDirectPurchaseHd entity = BusinessLayer.GetvDirectPurchaseHd(filterExpression, PageIndex, "DirectPurchaseID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvDirectPurchaseHdRowIndex(filterExpression, keyValue, "DirectPurchaseID DESC");
            vDirectPurchaseHd entity = BusinessLayer.GetvDirectPurchaseHd(filterExpression, PageIndex, "DirectPurchaseID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vDirectPurchaseHd entity, ref bool isShowWatermark, ref string watermarkText)
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
            hdnGCTransactionStatus.Value = entity.GCTransactionStatus;
            hdnDirectPurchaseID.Value = entity.DirectPurchaseID.ToString();
            txtDirectPurchaseNo.Text = entity.DirectPurchaseNo;
            txtDirectPurchaseDate.Text = entity.PurchaseDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            if (entity.ReferenceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT) != "01-01-1900")
                txtReferenceDate.Text = entity.ReferenceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            else
                txtReferenceDate.Text = "";
            hdnSupplierID.Value = entity.BusinessPartnerID.ToString();
            txtSupplierCode.Text = entity.BusinessPartnerCode;
            txtNonMasterSupplierName.Text = txtSupplierName.Text = entity.BusinessPartnerName;
            hdnIsLineAmountRounded.Value = entity.IsLineAmountRounded ? "1" : "0";
            hdnLineAmountRoundedFormat.Value = entity.LineAmountRoundedFormat.ToString();
            hdnIsTotalAmountRounded.Value = entity.IsTotalAmountRounded ? "1" : "0";
            hdnTotalAmountRoundedFormat.Value = entity.TotalAmountRoundedFormat.ToString();

            if (entity.BusinessPartnerID.ToString() != hdnNonMasterSupplierID.Value)
            {
                chkIsFromMasterSupplier.Checked = true;
                tblSupplierMaster.Style.Remove("display");
                txtNonMasterSupplierName.Style.Add("display", "none");
            }
            else
            {
                chkIsFromMasterSupplier.Checked = false;
                txtNonMasterSupplierName.Style.Remove("display");
                tblSupplierMaster.Style.Add("display", "none");
            }
            hdnSiteServiceUnitID.Value = entity.SiteServiceUnitID.ToString();
            txtServiceUnitCode.Text = entity.ServiceUnitCode;
            txtServiceUnitName.Text = entity.ServiceUnitName;
            hdnToSiteServiceUnitID.Value = entity.ToSiteServiceUnitID.ToString();
            txtToServiceUnitCode.Text = entity.ToServiceUnitCode;
            txtToServiceUnitName.Text = entity.ToServiceUnitName;
            hdnLocationID.Value = entity.LocationID.ToString();
            txtLocationCode.Text = entity.LocationCode;
            txtLocationName.Text = entity.LocationName;
            cboDirectPurchaseType.Value = entity.GCDirectPurchaseType;
            txtRemarks.Text = entity.Remarks;
            chkPPN.Checked = entity.IsIncludeVAT;
            txtTransactionAmount.Text = entity.TransactionAmount.ToString();
            if ((entity.TransactionAmount + entity.VATAmount) != 0)
                txtFinalDiscountPercentage.Text = (entity.FinalDiscountAmount * 100 / (entity.TransactionAmount + entity.VATAmount)).ToString();
            else
                txtFinalDiscountPercentage.Text = "0";
            txtFinalDiscountAmount.Text = entity.FinalDiscountAmount.ToString();

            decimal tempTransactionAmount = -1;
            BindGridView(1, true, ref PageCount, ref RowCount, ref tempTransactionAmount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();

            {
                List<LocationItemGroup> lstLocationItemGroup = BusinessLayer.GetLocationItemGroupList(string.Format("LocationID = {0}", entity.LocationID));
                string filterLocationItemGroup = String.Join(" OR ", lstLocationItemGroup.Select(p => string.Format("DisplayPath LIKE '%/{0}/%'", p.ItemGroupID)).ToList());
                if (filterLocationItemGroup != "")
                    hdnLstFilterLocationItemGroup.Value = string.Format("({0})", filterLocationItemGroup);
                else
                    hdnLstFilterLocationItemGroup.Value = "";
            }
            hdnLstSiteID.Value = entity.ListSiteID;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount, ref decimal transactionAmount)
        {
            string filterExpression = "1 = 0";
            if (hdnDirectPurchaseID.Value != "" && hdnDirectPurchaseID.Value != "0")
                filterExpression = string.Format("DirectPurchaseID = {0} AND GCItemDetailStatus != '{1}'", hdnDirectPurchaseID.Value, Constant.TransactionStatus.VOID);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvDirectPurchaseDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            if (transactionAmount > -1)
                transactionAmount = BusinessLayer.GetDirectPurchaseHd(Convert.ToInt32(hdnDirectPurchaseID.Value)).TransactionAmount;
            List<vDirectPurchaseDt> lstEntity = BusinessLayer.GetvDirectPurchaseDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ItemName1 ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
        #endregion

        #region Save Edit Header
        private void ControlToEntity(DirectPurchaseHd entityHd)
        {
            entityHd.PurchaseDate = Helper.GetDatePickerValue(txtDirectPurchaseDate.Text);
            if (txtReferenceDate.Text != "" && txtReferenceDate.Text != null)
                entityHd.ReferenceDate = Helper.GetDatePickerValue(txtReferenceDate.Text);
            entityHd.GCDirectPurchaseType = cboDirectPurchaseType.Value.ToString();
            entityHd.BusinessPartnerID = Convert.ToInt32(hdnSupplierID.Value);
            if (chkIsFromMasterSupplier.Checked)
                entityHd.BusinessPartnerName = null;
            else
                entityHd.BusinessPartnerName = txtNonMasterSupplierName.Text;
            entityHd.Remarks = txtRemarks.Text;
            entityHd.IsIncludeVAT = chkPPN.Checked;
            if (entityHd.IsIncludeVAT)
                entityHd.VATPercentage = Convert.ToDecimal(hdnVATPercentage.Value);
            else
                entityHd.VATPercentage = 0;
            entityHd.VATAmount = Convert.ToDecimal(Request.Form[txtPPN.UniqueID]);
            entityHd.FinalDiscountAmount = Convert.ToDecimal(txtFinalDiscountAmount.Text);
            entityHd.SiteServiceUnitID = Convert.ToInt32(hdnSiteServiceUnitID.Value);
            entityHd.ToSiteServiceUnitID = Convert.ToInt32(hdnToSiteServiceUnitID.Value);
            entityHd.LocationID = Convert.ToInt32(hdnLocationID.Value);
            entityHd.TotalNetTransactionAmount = entityHd.TransactionAmount + entityHd.VATAmount - entityHd.FinalDiscountAmount;
        }

        public void SaveDirectPurchaseHd(IDbContext ctx, ref int DirectPurchaseID)
        {
            DirectPurchaseHdDao entityHdDao = new DirectPurchaseHdDao(ctx);
            DirectPurchaseHdSiteDao entityHdSiteDao = new DirectPurchaseHdSiteDao(ctx);
            if (hdnDirectPurchaseID.Value == "0")
            {
                DirectPurchaseHd entityHd = new DirectPurchaseHd();
                ControlToEntity(entityHd);
                entityHd.DirectPurchaseNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.DIRECT_PURCHASE, entityHd.PurchaseDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                DirectPurchaseID = entityHdDao.Insert(entityHd);

                if (hdnLstSiteID.Value != "")
                {
                    string[] lstSiteID = hdnLstSiteID.Value.Split(',');
                    foreach (string siteID in lstSiteID)
                    {
                        DirectPurchaseHdSite entityDt = new DirectPurchaseHdSite();
                        entityDt.DirectPurchaseID = DirectPurchaseID;
                        entityDt.SiteID = siteID;
                        entityHdSiteDao.Insert(entityDt);
                    }
                }
            }
            else
            {
                DirectPurchaseID = Convert.ToInt32(hdnDirectPurchaseID.Value);
                DirectPurchaseHd entityHd = entityHdDao.Get(DirectPurchaseID);
                ControlToEntity(entityHd);
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Update(entityHd);

                List<DirectPurchaseHdSite> lstEntityDt = BusinessLayer.GetDirectPurchaseHdSiteList(string.Format("DirectPurchaseID = {0}", entityHd.DirectPurchaseID), ctx);
                if (hdnLstSiteID.Value != "")
                {
                    string[] lstSiteID = hdnLstSiteID.Value.Split(',');
                    foreach (string siteID in lstSiteID)
                    {
                        DirectPurchaseHdSite entityDt = lstEntityDt.FirstOrDefault(p => p.SiteID == siteID);
                        if (entityDt == null)
                        {
                            entityDt = new DirectPurchaseHdSite();
                            entityDt.DirectPurchaseID = entityHd.DirectPurchaseID;
                            entityDt.SiteID = siteID;
                            entityHdSiteDao.Insert(entityDt);
                        }
                        else
                            lstEntityDt.Remove(entityDt);
                    }
                }

                foreach (DirectPurchaseHdSite entityDt in lstEntityDt)
                {
                    entityHdSiteDao.Delete(entityDt.DirectPurchaseID, entityDt.SiteID);
                }
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            try
            {
                int DirectPurchaseID = 0;
                SaveDirectPurchaseHd(ctx, ref DirectPurchaseID);
                retval = DirectPurchaseID.ToString();
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
            DirectPurchaseHdDao entityHdDao = new DirectPurchaseHdDao(ctx);
            DirectPurchaseHdSiteDao entityHdSiteDao = new DirectPurchaseHdSiteDao(ctx);
            try
            {
                DirectPurchaseHd entity = BusinessLayer.GetDirectPurchaseHd(Convert.ToInt32(hdnDirectPurchaseID.Value));
                if (entity.GCTransactionStatus == Constant.TransactionStatus.OPEN)
                {
                    ControlToEntity(entity);
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.UpdateDirectPurchaseHd(entity);

                    List<DirectPurchaseHdSite> lstEntityDt = BusinessLayer.GetDirectPurchaseHdSiteList(string.Format("DirectPurchaseID = {0}", entity.DirectPurchaseID), ctx);
                    if (hdnLstSiteID.Value != "")
                    {
                        string[] lstSiteID = hdnLstSiteID.Value.Split(',');
                        foreach (string siteID in lstSiteID)
                        {
                            DirectPurchaseHdSite entityDt = lstEntityDt.FirstOrDefault(p => p.SiteID == siteID);
                            if (entityDt == null)
                            {
                                entityDt = new DirectPurchaseHdSite();
                                entityDt.DirectPurchaseID = entity.DirectPurchaseID;
                                entityDt.SiteID = siteID;
                                entityHdSiteDao.Insert(entityDt);
                            }
                            else
                                lstEntityDt.Remove(entityDt);
                        }
                    }

                    foreach (DirectPurchaseHdSite entityDt in lstEntityDt)
                    {
                        entityHdSiteDao.Delete(entityDt.DirectPurchaseID, entityDt.SiteID);
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
            DirectPurchaseHdDao directPurchaseHdDao = new DirectPurchaseHdDao(ctx);
            DirectPurchaseDtDao directPurchaseDtDao = new DirectPurchaseDtDao(ctx);
            ItemPlanningDao itemPlanningDao = new ItemPlanningDao(ctx);
            try
            {
                DirectPurchaseHd entity = directPurchaseHdDao.Get(Convert.ToInt32(hdnDirectPurchaseID.Value));
                if (entity.GCTransactionStatus == Constant.TransactionStatus.OPEN || entity.GCTransactionStatus == Constant.TransactionStatus.WAIT_FOR_APPROVAL)
                {
                    ControlToEntity(entity);
                    List<DirectPurchaseDt> lstDirectPurchaseDt = BusinessLayer.GetDirectPurchaseDtList(String.Format("DirectPurchaseID = {0} AND GCItemDetailStatus != '{1}'", entity.DirectPurchaseID, Constant.TransactionStatus.VOID), ctx);

                    String lstItemID = String.Join(",", lstDirectPurchaseDt.Select(p => p.ItemID).ToList());
                    string filterExpression = String.Format("SiteID = '{0}' AND ItemID IN ({1}) AND IsDeleted = 0", AppSession.UserLogin.SiteID, lstItemID);
                    List<ItemPlanning> lstItemPlanning = BusinessLayer.GetItemPlanningList(filterExpression, ctx);

                    List<PurchaseReceiveDt> lstPendingPurchaseReceiveDt = BusinessLayer.GetPurchaseReceiveDtList(String.Format("ItemID IN ({0}) AND GCItemDetailStatus IN ('{1}','{2}') AND QtyBeforeApproved != 0", lstItemID, Constant.TransactionStatus.OPEN, Constant.TransactionStatus.WAIT_FOR_APPROVAL), ctx);
                    List<DirectPurchaseDt> lstPendingDirectPurchaseDt = BusinessLayer.GetDirectPurchaseDtList(String.Format("DirectPurchaseID != {0} AND ItemID IN ({1}) AND GCItemDetailStatus IN ('{2}','{3}') AND QtyBeforeApproved != 0", entity.DirectPurchaseID, lstItemID, Constant.TransactionStatus.OPEN, Constant.TransactionStatus.WAIT_FOR_APPROVAL), ctx);
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
                        foreach (DirectPurchaseDt purchaseDt in lstDirectPurchaseDt)
                        {
                            purchaseDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
                            purchaseDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            directPurchaseDtDao.Update(purchaseDt);

                            if (purchaseDt.ItemID > 0)
                            {
                                ItemPlanning entityItemPlanning = lstItemPlanning.Where(x => x.ItemID == purchaseDt.ItemID).FirstOrDefault();
                                decimal purchaseUnitPrice = purchaseDt.UnitPrice;
                                decimal unitPrice = 0;
                                unitPrice = purchaseUnitPrice / purchaseDt.ConversionFactor;
                                if (entityItemPlanning.UnitPrice < unitPrice)
                                {
                                    entityItemPlanning.UnitPrice = unitPrice;
                                    entityItemPlanning.PurchaseUnitPrice = purchaseUnitPrice;
                                }
                                if (!entityItemPlanning.ListPendingPurchaseReceiveID.Contains(string.Format("|D{0}|", entity.DirectPurchaseID)))
                                {
                                    entityItemPlanning.ListPendingPurchaseReceiveID += string.Format("|D{0}|", entity.DirectPurchaseID);
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
                        directPurchaseHdDao.Update(entity);
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

        protected override bool OnProposeRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            DirectPurchaseHdDao directPurchaseHdDao = new DirectPurchaseHdDao(ctx);
            DirectPurchaseDtDao directPurchaseDtDao = new DirectPurchaseDtDao(ctx);
            try
            {
                DirectPurchaseHd entity = directPurchaseHdDao.Get(Convert.ToInt32(hdnDirectPurchaseID.Value));
                if (entity.GCTransactionStatus == Constant.TransactionStatus.OPEN)
                {
                    ControlToEntity(entity);
                    entity.GCTransactionStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                    List<DirectPurchaseDt> lstEntity = BusinessLayer.GetDirectPurchaseDtList(string.Format("DirectPurchaseID = {0} AND GCItemDetailStatus != '{1}'", hdnDirectPurchaseID.Value, Constant.TransactionStatus.VOID), ctx);
                    foreach (DirectPurchaseDt entityDt in lstEntity)
                    {
                        entityDt.GCItemDetailStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                        entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        directPurchaseDtDao.Update(entityDt);
                    }
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    directPurchaseHdDao.Update(entity);
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

        protected override bool OnReopenRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            DirectPurchaseHdDao purchaseHdDao = new DirectPurchaseHdDao(ctx);
            DirectPurchaseDtDao purchaseDtDao = new DirectPurchaseDtDao(ctx);

            try
            {
                DirectPurchaseHd entity = purchaseHdDao.Get(Convert.ToInt32(hdnDirectPurchaseID.Value));
                if (entity.GCTransactionStatus == Constant.TransactionStatus.APPROVED || entity.GCTransactionStatus == Constant.TransactionStatus.WAIT_FOR_APPROVAL)
                {
                    List<DirectPurchaseDt> lstDirectPurchaseDt = BusinessLayer.GetDirectPurchaseDtList(String.Format("DirectPurchaseID = {0} AND GCItemDetailStatus != '{1}'", entity.DirectPurchaseID, Constant.TransactionStatus.VOID), ctx);

                    List<String> lstRequiredPurchaseReceiveID = new List<String>();
                    String lstItemID = String.Join(",", lstDirectPurchaseDt.Select(p => p.ItemID).ToList());

                    string filterExpression = String.Format("SiteID = '{0}' AND ItemID IN ({1}) AND IsDeleted = 0", AppSession.UserLogin.SiteID, lstItemID);
                    List<ItemPlanning> lstItemPlanning = BusinessLayer.GetItemPlanningList(filterExpression, ctx);

                    string tempID = string.Format("D{0}", hdnDirectPurchaseID.Value);
                    foreach (ItemPlanning itemPlanning in lstItemPlanning)
                    {
                        if (itemPlanning.ListPendingPurchaseReceiveID != "")
                        {
                            string temp = itemPlanning.ListPendingPurchaseReceiveID.Substring(1, itemPlanning.ListPendingPurchaseReceiveID.Length - 2);
                            string[] lstPendingDirectPurchaseID = temp.Split(new string[] { "||" }, StringSplitOptions.None);
                            string prID = lstPendingDirectPurchaseID.Last();
                            if (prID != tempID)
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
                        foreach (DirectPurchaseDt purchaseDt in lstDirectPurchaseDt)
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
            DirectPurchaseHdDao directPurchaseHdDao = new DirectPurchaseHdDao(ctx);
            DirectPurchaseDtDao directPurchaseDtDao = new DirectPurchaseDtDao(ctx);
            try
            {
                DirectPurchaseHd entity = directPurchaseHdDao.Get(Convert.ToInt32(hdnDirectPurchaseID.Value));
                if (entity.GCTransactionStatus == Constant.TransactionStatus.OPEN)
                {
                    entity.GCTransactionStatus = Constant.TransactionStatus.VOID;
                    List<DirectPurchaseDt> lstEntity = BusinessLayer.GetDirectPurchaseDtList(string.Format("DirectPurchaseID = {0} AND GCItemDetailStatus != '{1}'", hdnDirectPurchaseID.Value, Constant.TransactionStatus.VOID), ctx);
                    foreach (DirectPurchaseDt entityDt in lstEntity)
                    {
                        entityDt.GCItemDetailStatus = Constant.TransactionStatus.VOID;
                        entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        directPurchaseDtDao.Update(entityDt);
                    }
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    directPurchaseHdDao.Update(entity);
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

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            int PurchaseID = 0;
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    PurchaseID = Convert.ToInt32(hdnDirectPurchaseID.Value);
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage, ref PurchaseID))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                PurchaseID = Convert.ToInt32(hdnDirectPurchaseID.Value);
                if (OnDeleteEntityDt(ref errMessage, PurchaseID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpPurchaseID"] = PurchaseID.ToString();
        }

        private void ControlToEntity(DirectPurchaseDt entityDt)
        {
            entityDt.ItemGroupID = Convert.ToInt32(hdnItemGroupID.Value);
            entityDt.ItemID = Convert.ToInt32(hdnItemID.Value);
            if (chkIsFromMasterItem.Checked)
            {
                entityDt.ItemName1 = null;
                entityDt.GCItemUnit = cboItemUnit.Value.ToString().Split('|')[0];
                entityDt.GCBaseUnit = hdnGCBaseUnit.Value;
                entityDt.ConversionFactor = Convert.ToDecimal(hdnConversionFactor.Value);
            }
            else
            {
                entityDt.ItemName1 = txtNonMasterItemName.Text;
                entityDt.GCItemUnit = entityDt.GCBaseUnit = cboNonMasterItemUnit.Value.ToString();
                entityDt.ConversionFactor = 1;
            }
            entityDt.Quantity = Convert.ToDecimal(txtQuantity.Text);
            entityDt.UnitPrice = Convert.ToDecimal(txtPrice.Text);
            entityDt.DiscountPercentage = Convert.ToDecimal(txtDiscountPercentage.Text);
            entityDt.DiscountAmount = Convert.ToDecimal(txtDiscountAmount.Text);
            entityDt.IsControlExpired = false;
            entityDt.LineAmount = Convert.ToDecimal(Request.Form[txtLineAmount.UniqueID]);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int DirectPurchaseID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            DirectPurchaseDtDao entityDtDao = new DirectPurchaseDtDao(ctx);
            try
            {
                SaveDirectPurchaseHd(ctx, ref DirectPurchaseID);
                DirectPurchaseDt entityDt = new DirectPurchaseDt();
                ControlToEntity(entityDt);
                entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                entityDt.DirectPurchaseID = DirectPurchaseID;
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
            DirectPurchaseDtDao entityDtDao = new DirectPurchaseDtDao(ctx);
            try
            {
                int DirectPurchaseID = 0;
                SaveDirectPurchaseHd(ctx, ref DirectPurchaseID);
                DirectPurchaseDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                if (entityDt.GCItemDetailStatus == Constant.TransactionStatus.OPEN)
                {
                    ControlToEntity(entityDt);
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Update(entityDt);
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
            DirectPurchaseDtDao entityDtDao = new DirectPurchaseDtDao(ctx);
            try
            {
                DirectPurchaseDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                if (entityDt.GCItemDetailStatus == Constant.TransactionStatus.OPEN)
                {
                    entityDt.GCItemDetailStatus = Constant.TransactionStatus.VOID;
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Update(entityDt);
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

        #region callBack Trigger
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
    }
}