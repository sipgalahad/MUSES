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
    public partial class DirectPurchaseReturnEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.DIRECT_PURCHASE_RETURN;
        }

        protected override void InitializeDataControl()
        {
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();
            List<SettingParameter> lstSettingParameter = BusinessLayer.GetSettingParameterList(string.Format("ParameterCode IN ('{0}','{1}','{2}')", Constant.SettingParameter.VAT_PERCENTAGE, Constant.SettingParameter.NON_MASTER_SUPPLIER, Constant.SettingParameter.NON_MASTER_ITEM));
            hdnVATPercentage.Value = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.VAT_PERCENTAGE).ParameterValue;
            hdnNonMasterSupplierID.Value = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.NON_MASTER_SUPPLIER).ParameterValue;
            hdnNonMasterItemID.Value = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.NON_MASTER_ITEM).ParameterValue;

            SetControlProperties();
            decimal tempTransactionAmount = -1;
            BindGridView(1, true, ref PageCount, ref RowCount, ref tempTransactionAmount);
            Helper.SetControlEntrySetting(txtQuantity, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboItemUnit, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboReason, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtPrice, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtDiscountPercentage, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtDiscountAmount, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        protected string GetVATPercentageLabel()
        {
            return hdnVATPercentage.Value;
        }

        #region Filter Expression Search Dialog
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

        protected string GetTransactionStatusVoid()
        {
            return Constant.TransactionStatus.VOID;
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> listStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}','{2}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PURCHASE_RETURN_TYPE, Constant.StandardCode.PURCHASE_RETURN_REASON, Constant.StandardCode.ITEM_UNIT));
            Methods.SetComboBoxField<StandardCode>(cboReturnType, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.PURCHASE_RETURN_TYPE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboReason, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.PURCHASE_RETURN_REASON).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboNonMasterItemUnit, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.ITEM_UNIT).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnDirectPurchaseReturnID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(hdnDirectPurchaseID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtDirectPurchaseReturnNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtPurchaseReturnDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(txtSupplierName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(hdnSiteServiceUnitID, new ControlEntrySetting(true, false));
            SetControlEntrySetting(txtServiceUnitCode, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtServiceUnitName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(hdnLocationID, new ControlEntrySetting(true, false));
            SetControlEntrySetting(txtLocationCode, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtLocationName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(lblDirectPurchaseNo, new ControlEntrySetting(true, false));
            SetControlEntrySetting(txtDirectPurchaseNo, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(cboReturnType, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtReferenceNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtReferenceDate, new ControlEntrySetting(true, true, false));

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
        }

        protected string IsEditable()
        {
            return hdnIsEditable.Value;
        }

        public override int OnGetRowCount()
        {
            return BusinessLayer.GetvDirectPurchaseReturnHdRowCount("");
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            vDirectPurchaseReturnHd entity = BusinessLayer.GetvDirectPurchaseReturnHd("", PageIndex, "DirectPurchaseReturnID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            PageIndex = BusinessLayer.GetvDirectPurchaseReturnHdRowIndex("", keyValue, "DirectPurchaseReturnID DESC");
            vDirectPurchaseReturnHd entity = BusinessLayer.GetvDirectPurchaseReturnHd("", PageIndex, "DirectPurchaseReturnID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vDirectPurchaseReturnHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
                hdnIsEditable.Value = "0";
            }
            else
                hdnIsEditable.Value = "1";
            hdnGCTransactionStatus.Value = entity.GCTransactionStatus;
            hdnDirectPurchaseReturnID.Value = entity.DirectPurchaseReturnID.ToString();
            txtDirectPurchaseReturnNo.Text = entity.DirectPurchaseReturnNo;
            hdnDirectPurchaseID.Value = entity.DirectPurchaseID.ToString();
            txtDirectPurchaseNo.Text = entity.DirectPurchaseNo;
            txtPurchaseReturnDate.Text = entity.ReturnDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            if (entity.ReferenceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT) != Constant.ConstantDate.DEFAULT_NULL)
                txtReferenceDate.Text = entity.ReferenceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            else
                txtReferenceDate.Text = "";
            hdnSupplierID.Value = entity.BusinessPartnerID.ToString();
            txtSupplierName.Text = entity.SupplierName;
            txtReferenceNo.Text = entity.ReferenceNo;
            hdnSiteServiceUnitID.Value = entity.SiteServiceUnitID.ToString();
            txtServiceUnitCode.Text = entity.ServiceUnitCode;
            txtServiceUnitName.Text = entity.ServiceUnitName;
            hdnLocationID.Value = entity.LocationID.ToString();
            txtLocationCode.Text = entity.LocationCode;
            txtLocationName.Text = entity.LocationName;
            cboReturnType.Value = entity.GCDirectPurchaseReturnType.ToString();
            txtNotes.Text = entity.Remarks;
            chkPPN.Checked = entity.IsIncludeVAT;
            txtTransactionAmount.Text = entity.TransactionAmount.ToString();

            decimal tempTransactionAmount = -1;
            BindGridView(1, true, ref PageCount, ref RowCount, ref tempTransactionAmount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount, ref decimal transactionAmount)
        {
            string filterExpression = "1 = 0";
            if (hdnDirectPurchaseReturnID.Value != "0")
                filterExpression = string.Format("DirectPurchaseReturnID = {0} AND GCItemDetailStatus != '{1}'", hdnDirectPurchaseReturnID.Value, Constant.TransactionStatus.VOID);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvDirectPurchaseReturnDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            if (transactionAmount > -1)
                transactionAmount = BusinessLayer.GetDirectPurchaseReturnHd(Convert.ToInt32(hdnDirectPurchaseReturnID.Value)).TransactionAmount;
            List<vDirectPurchaseReturnDt> lstEntity = BusinessLayer.GetvDirectPurchaseReturnDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ItemName1 ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
        #endregion

        #region Save Edit Header
        private void ControlToEntity(DirectPurchaseReturnHd entityHd)
        {
            entityHd.ReturnDate = Helper.GetDatePickerValue(txtPurchaseReturnDate.Text);
            entityHd.DirectPurchaseID = Convert.ToInt32(hdnDirectPurchaseID.Value);
            entityHd.SiteServiceUnitID = Convert.ToInt32(hdnSiteServiceUnitID.Value);
            entityHd.LocationID = Convert.ToInt32(hdnLocationID.Value);
            entityHd.BusinessPartnerID = Convert.ToInt32(hdnSupplierID.Value);
            if (entityHd.BusinessPartnerID != Convert.ToInt32(hdnNonMasterSupplierID.Value))
                entityHd.BusinessPartnerName = null;
            else
                entityHd.BusinessPartnerName = Request.Form[txtSupplierName.UniqueID];
            entityHd.GCDirectPurchaseReturnType = cboReturnType.Value.ToString();
            entityHd.ReferenceNo = Request.Form[txtReferenceNo.UniqueID];
            entityHd.ReferenceDate = Helper.GetDatePickerValue(Request.Form[txtReferenceDate.UniqueID]);
            entityHd.IsIncludeVAT = chkPPN.Checked;
            if (entityHd.IsIncludeVAT)
                entityHd.VATPercentage = Convert.ToDecimal(hdnVATPercentage.Value);
            else
                entityHd.VATPercentage = 0;
            entityHd.Remarks = txtNotes.Text;
            entityHd.VATAmount = Convert.ToDecimal(Request.Form[txtPPN.UniqueID]);
            entityHd.FinalDiscountAmount = Convert.ToDecimal(txtFinalDiscountAmount.Text);
            entityHd.LocationID = Convert.ToInt32(hdnLocationID.Value);
            entityHd.TotalNetTransactionAmount = entityHd.TransactionAmount + entityHd.VATAmount - entityHd.FinalDiscountAmount;
        }

        public void SavePurchaseReturnHd(IDbContext ctx, ref int PRID, ref string PRNo)
        {
            DirectPurchaseReturnHdDao entityHdDao = new DirectPurchaseReturnHdDao(ctx);
            if (hdnDirectPurchaseReturnID.Value == "0")
            {
                DirectPurchaseReturnHd entityHd = new DirectPurchaseReturnHd();
                ControlToEntity(entityHd);
                entityHd.DirectPurchaseReturnNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.DIRECT_PURCHASE_RETURN, entityHd.ReturnDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);
                PRID = BusinessLayer.GetDirectPurchaseReturnHdMaxID(ctx);
                PRNo = entityHd.DirectPurchaseReturnNo;
            }
            else
            {
                PRID = Convert.ToInt32(hdnDirectPurchaseReturnID.Value);
                PRNo = txtDirectPurchaseReturnNo.Text;
                DirectPurchaseReturnHd entityHd = entityHdDao.Get(PRID);
                ControlToEntity(entityHd);
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Update(entityHd);
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            DirectPurchaseHdDao entityHdDao = new DirectPurchaseHdDao(ctx);
            try
            {
                int PRID = 0;
                string purchaseReturnNo = "";
                SavePurchaseReturnHd(ctx, ref PRID, ref purchaseReturnNo);
                DirectPurchaseHd entity = entityHdDao.Get(Convert.ToInt32(hdnDirectPurchaseID.Value));
                entity.IsHasPurchaseReturn = true;
                entity.DirectPurchaseReturnID = Convert.ToInt32(hdnDirectPurchaseReturnID.Value);
                entityHdDao.Update(entity);

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
                DirectPurchaseReturnHd entity = BusinessLayer.GetDirectPurchaseReturnHd(Convert.ToInt32(hdnDirectPurchaseReturnID.Value));
                entity.Remarks = txtNotes.Text;
                entity.ReferenceDate = Helper.GetDatePickerValue(txtReferenceDate.Text);
                entity.IsIncludeVAT = chkPPN.Checked;
                if (entity.IsIncludeVAT)
                    entity.VATPercentage = Convert.ToDecimal(hdnVATPercentage.Value);
                else
                    entity.VATPercentage = 0;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateDirectPurchaseReturnHd(entity);
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
            DirectPurchaseReturnHdDao entityHdDao = new DirectPurchaseReturnHdDao(ctx);
            DirectPurchaseReturnDtDao entityDtDao = new DirectPurchaseReturnDtDao(ctx);
            DirectPurchaseHdDao purchaseHdDao = new DirectPurchaseHdDao(ctx);
            try
            {
                DirectPurchaseReturnHd entity = entityHdDao.Get(Convert.ToInt32(hdnDirectPurchaseReturnID.Value));
                entity.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                List<DirectPurchaseReturnDt> lstEntity = BusinessLayer.GetDirectPurchaseReturnDtList(string.Format("DirectPurchaseReturnID = {0} AND GCItemDetailStatus != '{1}'", hdnDirectPurchaseReturnID.Value, Constant.TransactionStatus.VOID), ctx);
                foreach (DirectPurchaseReturnDt entityDt in lstEntity)
                {
                    entityDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Update(entityDt);
                }
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Update(entity);
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

        protected override bool OnVoidRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            DirectPurchaseReturnHdDao entityHdDao = new DirectPurchaseReturnHdDao(ctx);
            DirectPurchaseReturnDtDao entityDtDao = new DirectPurchaseReturnDtDao(ctx);
            DirectPurchaseHdDao purchaseHdDao = new DirectPurchaseHdDao(ctx);
            try
            {
                DirectPurchaseReturnHd entity = entityHdDao.Get(Convert.ToInt32(hdnDirectPurchaseReturnID.Value));
                entity.GCTransactionStatus = Constant.TransactionStatus.VOID;
                List<DirectPurchaseReturnDt> lstEntity = BusinessLayer.GetDirectPurchaseReturnDtList(string.Format("DirectPurchaseReturnID = {0} AND GCItemDetailStatus != '{1}'", hdnDirectPurchaseReturnID.Value, Constant.TransactionStatus.VOID), ctx);
                foreach (DirectPurchaseReturnDt entityDt in lstEntity)
                {
                    entityDt.GCItemDetailStatus = Constant.TransactionStatus.VOID;
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Update(entityDt);
                }
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Update(entity);

                DirectPurchaseHd entityDirect = purchaseHdDao.Get(Convert.ToInt32(hdnDirectPurchaseID.Value));
                entityDirect.IsHasPurchaseReturn = false;
                entityDirect.DirectPurchaseReturnID = null;
                purchaseHdDao.Update(entityDirect);
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

        protected override bool OnCustomButtonClick(string type, ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            DirectPurchaseHdDao entityDirectPurchaseHdDao = new DirectPurchaseHdDao(ctx);
            DirectPurchaseReturnHdDao entityHdDao = new DirectPurchaseReturnHdDao(ctx);
            DirectPurchaseReturnDtDao entityDtDao = new DirectPurchaseReturnDtDao(ctx);
            if (type == "confirm")
            {
                try
                {
                    DirectPurchaseReturnHd entity = entityHdDao.Get(Convert.ToInt32(hdnDirectPurchaseReturnID.Value));
                    entity.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                    List<DirectPurchaseReturnDt> lstEntity = BusinessLayer.GetDirectPurchaseReturnDtList(string.Format("DirectPurchaseReturnID = {0} AND GCItemDetailStatus != '{1}'", hdnDirectPurchaseReturnID.Value, Constant.TransactionStatus.VOID), ctx);
                    foreach (DirectPurchaseReturnDt entityDt in lstEntity)
                    {
                        entityDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
                        entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDtDao.Update(entityDt);
                    }
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityHdDao.Update(entity);
                    retval = entity.DirectPurchaseReturnNo;
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
            }
            else if (type == "void")
            {
                try
                {
                    DirectPurchaseReturnHd entity = entityHdDao.Get(Convert.ToInt32(hdnDirectPurchaseReturnID.Value));
                    entity.GCTransactionStatus = Constant.TransactionStatus.VOID;
                    List<DirectPurchaseReturnDt> lstEntity = BusinessLayer.GetDirectPurchaseReturnDtList(string.Format("DirectPurchaseReturnID = {0} AND GCItemDetailStatus != '{1}'", hdnDirectPurchaseReturnID.Value, Constant.TransactionStatus.VOID));
                    foreach (DirectPurchaseReturnDt entityDt in lstEntity)
                    {
                        entityDt.GCItemDetailStatus = Constant.TransactionStatus.VOID;
                        entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDtDao.Update(entityDt);
                    }
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.UpdateDirectPurchaseReturnHd(entity);
                    retval = entity.DirectPurchaseReturnNo;

                    DirectPurchaseHd entityDirect = BusinessLayer.GetDirectPurchaseHdList(string.Format("DirectPurchaseID = {0}", hdnDirectPurchaseID.Value), ctx)[0];
                    entityDirect.IsHasPurchaseReturn = false;
                    entityDirect.DirectPurchaseReturnID = null;
                    entityDirectPurchaseHdDao.Update(entityDirect);

                    ctx.CommitTransaction();
                }
                catch (Exception ex)
                {
                    Helper.InsertErrorLog(ex);
                    ctx.RollBackTransaction();
                    errMessage = ex.Message;
                    result = false;
                }
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
                    PRID = Convert.ToInt32(hdnDirectPurchaseReturnID.Value);
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
            panel.JSProperties["cpPurchaseReturnID"] = PRID.ToString();
        }

        private void ControlToEntity(DirectPurchaseReturnDt entityDt)
        {
            entityDt.ItemID = Convert.ToInt32(hdnItemID.Value);
            if (entityDt.ItemID == Convert.ToInt32(hdnNonMasterItemID.Value))
            {
                entityDt.ItemName1 = Request.Form[txtItemName.UniqueID];
                entityDt.ConversionFactor = 1;
                entityDt.GCBaseUnit = entityDt.GCItemUnit = cboNonMasterItemUnit.Value.ToString();
            }
            else
            {
                entityDt.ItemName1 = null;
                entityDt.ConversionFactor = Convert.ToDecimal(hdnConversionFactor.Value);
                entityDt.GCItemUnit = cboItemUnit.Value.ToString();
                entityDt.GCBaseUnit = hdnGCBaseUnit.Value;
            }
            entityDt.Quantity = Convert.ToDecimal(txtQuantity.Text);
            entityDt.UnitPrice = Convert.ToDecimal(Request.Form[txtPrice.UniqueID]);
            entityDt.DiscountPercentage = Convert.ToDecimal(Request.Form[txtDiscountPercentage.UniqueID]);
            entityDt.DiscountAmount = Convert.ToDecimal(Request.Form[txtDiscountAmount.UniqueID]);
            entityDt.LineAmount = Convert.ToDecimal(Request.Form[txtLineAmount.UniqueID]);
            entityDt.GCPurchaseReturnReason = cboReason.Value.ToString();
            if (entityDt.GCPurchaseReturnReason == "X162^999")
                entityDt.PurchaseReturnReason = txtReason.Text;
            entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int PRID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            DirectPurchaseHdDao entityHdDao = new DirectPurchaseHdDao(ctx);
            DirectPurchaseReturnDtDao entityDtDao = new DirectPurchaseReturnDtDao(ctx);
            try
            {
                string purchaseReturnNo = "";
                SavePurchaseReturnHd(ctx, ref PRID, ref purchaseReturnNo);
                DirectPurchaseReturnDt entityDt = new DirectPurchaseReturnDt();
                ControlToEntity(entityDt);
                entityDt.DirectPurchaseReturnID = PRID;
                entityDt.CreatedBy = AppSession.UserLogin.UserID;
                entityDtDao.Insert(entityDt);

                DirectPurchaseHd entity = BusinessLayer.GetDirectPurchaseHdList(string.Format("DirectPurchaseID = {0}", hdnDirectPurchaseID.Value), ctx)[0];
                entity.IsHasPurchaseReturn = true;
                entity.DirectPurchaseReturnID = Convert.ToInt32(hdnDirectPurchaseReturnID.Value);
                entityHdDao.Update(entity);
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
            DirectPurchaseReturnDtDao entityDtDao = new DirectPurchaseReturnDtDao(ctx);
            try
            {
                int PRID = 0;
                string purchaseReturnNo = "";
                SavePurchaseReturnHd(ctx, ref PRID, ref purchaseReturnNo);
                DirectPurchaseReturnDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
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
            DirectPurchaseReturnDtDao entityDtDao = new DirectPurchaseReturnDtDao(ctx);
            try
            {
                DirectPurchaseReturnDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
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