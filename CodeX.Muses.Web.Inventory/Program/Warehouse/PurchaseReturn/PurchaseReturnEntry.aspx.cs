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
    public partial class PurchaseReturnEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.PURCHASE_RETURN;
        }

        #region Filter Expression Search Dialog
        protected string GetPurchaseReturnCreditNote()
        {
            return Constant.PurchaseReturnType.CREDIT_NOTE;
        }
        protected string OnGetFilterExpressionServiceUnit()
        {
            if (hdnListSiteServiceUnitID.Value != "")
                return string.Format("SiteServiceUnitID IN ({0}) AND IsDeleted = 0", hdnListSiteServiceUnitID.Value);
            return "1 = 0";
        }
        protected string OnGetFilterExpressionLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.PURCHASE_RETURN);
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
        protected string OnGetPurchaseReceiveFilterExpression()
        {
            return string.Format("TransactionCode = '{0}' AND GCTransactionStatus IN ('{1}','{2}')", Constant.TransactionCode.PURCHASE_RECEIVE, Constant.TransactionStatus.PROCESSED, Constant.TransactionStatus.CLOSED);
        }
        #endregion

        protected override void InitializeDataControl()
        {
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();
            hdnVATPercentage.Value = BusinessLayer.GetSettingParameter(Constant.SettingParameter.VAT_PERCENTAGE).ParameterValue;
            List<GetLocationUserList> lstUserLocation = BusinessLayer.GetLocationUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.PURCHASE_RETURN, "");
            if (lstUserLocation.Count > 0)
            {
                List<GetServiceUnitUserList> lstUserServiceUnit = BusinessLayer.GetServiceUnitUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, string.Format("SiteServiceUnitID IN (SELECT SiteServiceUnitID FROM ServiceUnitLocation WHERE LocationID IN ({0}))", string.Join(",", lstUserLocation.Select(p => p.LocationID).ToList())));
                hdnListSiteServiceUnitID.Value = string.Join(",", lstUserServiceUnit.Select(p => p.SiteServiceUnitID).ToList());
                if (lstUserServiceUnit.Count == 1)
                {
                    GetServiceUnitUserList serviceUnit = lstUserServiceUnit.FirstOrDefault();
                    hdnDefaultSiteServiceUnitID.Value = serviceUnit.SiteServiceUnitID.ToString();
                    hdnDefaultServiceUnitCode.Value = serviceUnit.ServiceUnitCode;
                    hdnDefaultServiceUnitName.Value = serviceUnit.ServiceUnitName;
                }
            }

            SetControlProperties(); 
            decimal tempTransactionAmount = -1;
            BindGridView(1, true, ref PageCount, ref RowCount, ref tempTransactionAmount);
            Helper.SetControlEntrySetting(txtQuantity, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtItemCode, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboItemUnit, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboReason, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        protected string GetTransactionStatusVoid()
        {
            return Constant.TransactionStatus.VOID;
        }

        protected string GetVATPercentageLabel()
        {
            return hdnVATPercentage.Value;
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> listStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}','{2}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PURCHASE_RETURN_TYPE, Constant.StandardCode.PURCHASE_RETURN_REASON, Constant.StandardCode.SUPPLIER_CREDIT_NOTE_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboReturnType, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.PURCHASE_RETURN_TYPE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboReason, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.PURCHASE_RETURN_REASON).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");

            List<StandardCode> lstCNType = listStandardCode.Where(p => p.ParentID == Constant.StandardCode.SUPPLIER_CREDIT_NOTE_TYPE).ToList<StandardCode>();
            StandardCode defaultCNType = lstCNType.FirstOrDefault(p => p.IsDefault);
            if (defaultCNType == null)
                defaultCNType = lstCNType.FirstOrDefault();
            hdnCNType.Value = defaultCNType.StandardCodeID;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnPRID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtPurchaseReturnDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(lblSupplier, new ControlEntrySetting(true, false));
            SetControlEntrySetting(txtSupplierCode, new ControlEntrySetting(true, false, true,""));
            SetControlEntrySetting(txtSupplierName, new ControlEntrySetting(false, false, true,""));
            SetControlEntrySetting(hdnPurchaseReceiveID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(lblPurchaseReceiveNo, new ControlEntrySetting(true, false));
            SetControlEntrySetting(txtPurchaseReceiveNo, new ControlEntrySetting(true, false, true,""));

            SetControlEntrySetting(hdnSiteServiceUnitID, new ControlEntrySetting(true, false, true, hdnDefaultSiteServiceUnitID.Value));
            SetControlEntrySetting(txtServiceUnitCode, new ControlEntrySetting(true, false, true, hdnDefaultServiceUnitCode.Value));
            SetControlEntrySetting(lblSiteServiceUnit, new ControlEntrySetting(true, false));
            SetControlEntrySetting(txtServiceUnitName, new ControlEntrySetting(false, false, true, hdnDefaultServiceUnitName.Value));
            SetControlEntrySetting(txtLocationCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(lblLocation, new ControlEntrySetting(true, false));
            SetControlEntrySetting(txtLocationName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtReferenceDate, new ControlEntrySetting(false, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(cboReturnType, new ControlEntrySetting(true, false, true));

            SetControlEntrySetting(txtNotes, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtTransactionAmount, new ControlEntrySetting(false, false, true, "0"));
            SetControlEntrySetting(txtPPN, new ControlEntrySetting(false, false, true, "0"));
            SetControlEntrySetting(txtTotalNetTransactionAmount, new ControlEntrySetting(false, false, true, "0"));
        }

        #region Load Entity
        public override void OnAddRecord()
        {
            hdnPageCount.Value = "0";
            chkPPN.Checked = false;
            txtTransactionAmount.Text = "0";
            txtPPN.Text = "0";
            txtTotalNetTransactionAmount.Text = "0";
            hdnIsEditable.Value = "1";
        }

        protected string IsEditable()
        {
            return hdnIsEditable.Value;
        }

        protected string GetFilterExpression()
        {
            if (hdnListSiteServiceUnitID.Value != "")
            {
                string filterExpression = "";
                if (filterExpression != "")
                    filterExpression += " AND ";
                filterExpression += string.Format("SiteServiceUnitID IN ({0}) AND TransactionCode = '{1}'", hdnListSiteServiceUnitID.Value, Constant.TransactionCode.PURCHASE_RETURN);
                return filterExpression;
            }
            return "1 = 0";
        }

        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvPurchaseReturnHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vPurchaseReturnHd entity = BusinessLayer.GetvPurchaseReturnHd(filterExpression, PageIndex, "PurchaseReturnID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvPurchaseReturnHdRowIndex(filterExpression, keyValue, "PurchaseReturnID DESC");
            vPurchaseReturnHd entity = BusinessLayer.GetvPurchaseReturnHd("", PageIndex, "PurchaseReturnID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vPurchaseReturnHd entity, ref bool isShowWatermark, ref string watermarkText)
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

            hdnPRID.Value = entity.PurchaseReturnID.ToString();
            txtReturnNo.Text = entity.PurchaseReturnNo;
            hdnPurchaseReceiveID.Value = entity.PurchaseReceiveID.ToString();
            txtPurchaseReceiveNo.Text = entity.PurchaseReceiveNo;
            txtPurchaseReturnDate.Text = entity.ReturnDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtReferenceDate.Text = entity.ReferenceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            hdnSupplierID.Value = entity.BusinessPartnerID.ToString();
            txtSupplierCode.Text = entity.BusinessPartnerCode;
            txtSupplierName.Text = entity.SupplierName;
            txtReferenceNo.Text = entity.ReferenceNo;
            hdnLocationID.Value = entity.LocationID.ToString();
            txtLocationCode.Text = entity.LocationCode;
            txtLocationName.Text = entity.LocationName;
            cboReturnType.Value = entity.GCPurchaseReturnType.ToString();
            txtNotes.Text = entity.Remarks;
            chkIsAutoUpdateStock.Checked = entity.IsAutoUpdateStock;
            chkPPN.Checked = entity.IsIncludeVAT;
            chkIsAutoUpdateStock.Checked = entity.IsAutoUpdateStock;
            txtTransactionAmount.Text = entity.TransactionAmount.ToString();

            if (entity.PurchaseReturnType == Constant.PurchaseReturnType.REPLACEMENT)
                chkIsAutoUpdateStock.Enabled = false;
            else
                chkIsAutoUpdateStock.Enabled = true;

            decimal tempTransactionAmount = -1;
            BindGridView(1, true, ref PageCount, ref RowCount, ref tempTransactionAmount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount, ref decimal transactionAmount)
        {
            string filterExpression = "1 = 0";
            if (hdnPRID.Value != "0")
                filterExpression = string.Format("PurchaseReturnID = {0} AND GCItemDetailStatus != '{1}'", hdnPRID.Value, Constant.TransactionStatus.VOID);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvPurchaseReturnDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            if (transactionAmount > -1)
                transactionAmount = BusinessLayer.GetPurchaseReturnHd(Convert.ToInt32(hdnPRID.Value)).TransactionAmount;

            List<vPurchaseReturnDt> lstEntity = BusinessLayer.GetvPurchaseReturnDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ItemName1 ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
        #endregion

        #region Save Edit Header
        private void ControlToEntity(PurchaseReturnHd entityHd)
        {
            entityHd.ReturnDate = Helper.GetDatePickerValue(txtPurchaseReturnDate.Text);
            entityHd.PurchaseReceiveID = Convert.ToInt32(hdnPurchaseReceiveID.Value);
            entityHd.SiteServiceUnitID = Convert.ToInt32(hdnSiteServiceUnitID.Value);
            entityHd.LocationID = Convert.ToInt32(hdnLocationID.Value);
            entityHd.BusinessPartnerID = Convert.ToInt32(hdnSupplierID.Value);
            entityHd.GCPurchaseReturnType = cboReturnType.Value.ToString();
            entityHd.ReferenceNo = Request.Form[txtReferenceNo.UniqueID];
            entityHd.ReferenceDate = Helper.GetDatePickerValue(txtReferenceDate.Text);
            entityHd.IsIncludeVAT = chkPPN.Checked;
            entityHd.Remarks = txtNotes.Text;
            if (entityHd.IsIncludeVAT)
                entityHd.VATPercentage = Convert.ToDecimal(hdnVATPercentage.Value);
            else
                entityHd.VATPercentage = 0;
            entityHd.VATAmount = Convert.ToDecimal(Request.Form[txtPPN.UniqueID]);
            entityHd.IsAutoUpdateStock = chkIsAutoUpdateStock.Checked;
            entityHd.TotalNetTransactionAmount = entityHd.TransactionAmount + entityHd.VATAmount;
        }

        public void SavePurchaseReturnHd(IDbContext ctx, ref int PRID, ref string PRNo)
        {
            PurchaseReturnHdDao entityHdDao = new PurchaseReturnHdDao(ctx);
            if (hdnPRID.Value == "0")
            {
                PurchaseReturnHd entityHd = new PurchaseReturnHd();
                ControlToEntity(entityHd);
                entityHd.TransactionCode = Constant.TransactionCode.PURCHASE_RETURN;
                entityHd.PurchaseReturnNo = BusinessLayer.GenerateTransactionNo(entityHd.TransactionCode, entityHd.ReturnDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);
                PRID = BusinessLayer.GetPurchaseReturnHdMaxID(ctx);
                PRNo = entityHd.PurchaseReturnNo;
            }
            else
            {
                PRID = Convert.ToInt32(hdnPRID.Value);
                PRNo = txtReturnNo.Text;
                PurchaseReturnHd entityHd = entityHdDao.Get(PRID);
                ControlToEntity(entityHd);
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
                int PRID = 0;
                string purchaseReturnNo = "";
                SavePurchaseReturnHd(ctx, ref PRID, ref purchaseReturnNo);
                retval = PRID.ToString();
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
                PurchaseReturnHd entity = BusinessLayer.GetPurchaseReturnHd(Convert.ToInt32(hdnPRID.Value));
                if (entity.GCTransactionStatus == Constant.TransactionStatus.OPEN)
                {
                    ControlToEntity(entity);
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.UpdatePurchaseReturnHd(entity);
                }
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
            PurchaseReturnHdDao purchaseReturnHdDao = new PurchaseReturnHdDao(ctx);
            PurchaseReturnDtDao purchaseReturnDtDao = new PurchaseReturnDtDao(ctx);
            SupplierCreditNoteDao entityHdDao = new SupplierCreditNoteDao(ctx);
            try
            {
                PurchaseReturnHd purchaseReturnHd = purchaseReturnHdDao.Get(Convert.ToInt32(hdnPRID.Value));
                if (purchaseReturnHd.GCTransactionStatus == Constant.TransactionStatus.OPEN || purchaseReturnHd.GCTransactionStatus == Constant.TransactionStatus.WAIT_FOR_APPROVAL)
                {
                    ControlToEntity(purchaseReturnHd);
                    purchaseReturnHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                    purchaseReturnHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    purchaseReturnHdDao.Update(purchaseReturnHd);

                    string filterExpressionPurchaseReturnHd = String.Format("PurchaseReturnID IN ({0})", hdnPRID.Value);
                    List<PurchaseReturnDt> lstPurchaseReturnDt = BusinessLayer.GetPurchaseReturnDtList(filterExpressionPurchaseReturnHd);
                    foreach (PurchaseReturnDt purchaseDt in lstPurchaseReturnDt)
                    {
                        purchaseDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
                        purchaseDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        purchaseReturnDtDao.Update(purchaseDt);
                    }

                    if (purchaseReturnHd.GCPurchaseReturnType == Constant.PurchaseReturnType.CREDIT_NOTE)
                    {
                        SupplierCreditNote entity = new SupplierCreditNote();
                        entity.CreditNoteDate = purchaseReturnHd.ReturnDate;
                        entity.BusinessPartnerID = purchaseReturnHd.BusinessPartnerID;
                        entity.PurchaseReturnID = purchaseReturnHd.PurchaseReturnID;
                        entity.GCCreditNoteType = hdnCNType.Value;
                        entity.CNAmount = purchaseReturnHd.TotalNetTransactionAmount;
                        entity.PurchaseReturnAmount = purchaseReturnHd.TotalNetTransactionAmount;
                        entity.IsIncludeVAT = chkPPN.Checked;
                        if (entity.IsIncludeVAT)
                            entity.VATPercentage = Convert.ToDecimal(hdnVATPercentage.Value);
                        else
                            entity.VATPercentage = 0;
                        entity.Remarks = "";
                        entity.CreditNoteNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.SUPPLIER_CREDIT_NOTE, entity.CreditNoteDate, ctx);
                        ctx.CommandType = CommandType.Text;
                        ctx.Command.Parameters.Clear();
                        entity.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                        entity.CreatedBy = AppSession.UserLogin.UserID;
                        entity.CreditNoteID = entityHdDao.Insert(entity);

                        entity = entityHdDao.Get(entity.CreditNoteID);
                        entity.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                        entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityHdDao.Update(entity);
                    }
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
            try
            {
                PurchaseReturnHd entity = BusinessLayer.GetPurchaseReturnHd(Convert.ToInt32(hdnPRID.Value));
                if (entity.GCTransactionStatus == Constant.TransactionStatus.OPEN)
                {
                    ControlToEntity(entity);
                    entity.GCTransactionStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.UpdatePurchaseReturnHd(entity);
                }
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        protected override bool OnVoidRecord(ref string errMessage)
        {
            try
            {
                PurchaseReturnHd entity = BusinessLayer.GetPurchaseReturnHd(Convert.ToInt32(hdnPRID.Value));
                if (entity.GCTransactionStatus == Constant.TransactionStatus.OPEN)
                {
                    entity.GCTransactionStatus = Constant.TransactionStatus.VOID;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.UpdatePurchaseReturnHd(entity);
                }
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        #endregion

        #region Callback Trigger
        protected void cboItemUnit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            List<StandardCode> lst = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND (StandardCodeID IN (SELECT GCAlternateUnit FROM ItemAlternateUnit WHERE ItemID = {1}) OR StandardCodeID = (SELECT GCItemUnit FROM ItemMaster WHERE ItemID = {1}))", Constant.StandardCode.ITEM_UNIT, hdnItemID.Value));
            Methods.SetComboBoxField<StandardCode>(cboItemUnit, lst, "StandardCodeName", "StandardCodeID");
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

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            int PRID = 0;
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";

            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage, ref PRID))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                PRID = Convert.ToInt32(hdnEntryID.Value);
                if (OnDeleteEntityDt(ref errMessage, PRID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpOrderID"] = PRID.ToString();
        }

        private void ControlToEntity(PurchaseReturnDt entityDt)
        {
            entityDt.ItemID = Convert.ToInt32(hdnItemID.Value);
            entityDt.Quantity = Convert.ToDecimal(txtQuantity.Text);
            entityDt.GCItemUnit = cboItemUnit.Value.ToString();
            entityDt.GCBaseUnit = hdnGCBaseUnit.Value;
            entityDt.UnitPrice = Convert.ToDecimal(Request.Form[txtPrice.UniqueID]);
            entityDt.ConversionFactor = Convert.ToDecimal(hdnConversionFactor.Value);
            entityDt.DiscountPercentage1 = Convert.ToDecimal(Request.Form[txtDiscountPercentage1.UniqueID]);
            entityDt.DiscountAmount1 = Convert.ToDecimal(Request.Form[txtDiscountAmount1.UniqueID]);
            entityDt.DiscountPercentage2 = Convert.ToDecimal(Request.Form[txtDiscountPercentage2.UniqueID]);
            entityDt.DiscountAmount2 = Convert.ToDecimal(Request.Form[txtDiscountAmount2.UniqueID]);
            entityDt.GCPurchaseReturnReason = cboReason.Value.ToString();
            if (entityDt.GCPurchaseReturnReason == "X162^999")
                entityDt.PurchaseReturnReason = txtReason.Text;
            entityDt.LineAmount = Convert.ToDecimal(Request.Form[txtLineAmount.UniqueID]);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int PRID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseReturnDtDao entityDtDao = new PurchaseReturnDtDao(ctx);
            try
            {
                string purchaseReturnNo = "";
                SavePurchaseReturnHd(ctx, ref PRID, ref purchaseReturnNo);
                PurchaseReturnDt entityDt = new PurchaseReturnDt();
                ControlToEntity(entityDt);
                entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                entityDt.PurchaseReturnID = PRID;
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
            PurchaseReturnDtDao entityDtDao = new PurchaseReturnDtDao(ctx);
            try
            {
                PurchaseReturnDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
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
            PurchaseReturnDtDao entityDtDao = new PurchaseReturnDtDao(ctx);
            try
            {
                PurchaseReturnDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
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
    }
}