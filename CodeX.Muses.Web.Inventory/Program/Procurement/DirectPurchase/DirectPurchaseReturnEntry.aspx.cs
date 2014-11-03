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
            hdnVATPercentage.Value = BusinessLayer.GetSettingParameter(Constant.SettingParameter.VAT_PERCENTAGE).ParameterValue;

            SetControlProperties();
            decimal tempTransactionAmount = -1;
            BindGridView(1, true, ref PageCount, ref RowCount, ref tempTransactionAmount);
            Helper.SetControlEntrySetting(txtQuantity, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtItemCode, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboItemUnit, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboReason, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        protected string GetVATPercentageLabel()
        {
            return hdnVATPercentage.Value;
        }

        #region Filter Expression Search Dialog
        protected string OnGetFilterExpressionItemProduct()
        {
            return string.Format("GCItemType = '{0}' AND IsDeleted = 0", Constant.ItemType.PRODUCT);
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
            List<StandardCode> listStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsDeleted = 0", Constant.StandardCode.PURCHASE_RETURN_TYPE, Constant.StandardCode.PURCHASE_RETURN_REASON));
            Methods.SetComboBoxField<StandardCode>(cboReturnType, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.PURCHASE_RETURN_TYPE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboReason, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.PURCHASE_RETURN_REASON).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnPRID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(hdnDirectPurchaseID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtDirectPurchaseReturnNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtPurchaseReturnDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(lblSupplier, new ControlEntrySetting(true, false));
            SetControlEntrySetting(txtSupplierCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtSupplierName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(lblLocation, new ControlEntrySetting(true, false));
            SetControlEntrySetting(txtLocationCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtLocationName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(lblDirectPurchaseNo, new ControlEntrySetting(true, false));
            SetControlEntrySetting(txtDirectPurchaseNo, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(cboReturnType, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtReferenceNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtReferenceDate, new ControlEntrySetting(true, true, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));

            SetControlEntrySetting(txtPPN, new ControlEntrySetting(false, false, true, "0"));
            SetControlEntrySetting(txtTotalReturSaldo, new ControlEntrySetting(false, false, true, "0"));
            SetControlEntrySetting(txtTotalRetur, new ControlEntrySetting(false, false, true, "0"));
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
            hdnPRID.Value = entity.DirectPurchaseReturnID.ToString();
            txtDirectPurchaseReturnNo.Text = entity.DirectPurchaseReturnNo;
            hdnDirectPurchaseID.Value = entity.DirectPurchaseID.ToString();
            txtDirectPurchaseNo.Text = entity.DirectPurchaseNo;
            txtPurchaseReturnDate.Text = entity.ReturnDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            if (entity.ReferenceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT) != Constant.ConstantDate.DEFAULT_NULL)
                txtReferenceDate.Text = entity.ReferenceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            else
                txtReferenceDate.Text = "";
            hdnSupplierID.Value = entity.BusinessPartnerID.ToString();
            txtSupplierCode.Text = entity.BusinessPartnerCode;
            txtSupplierName.Text = entity.SupplierName;
            txtReferenceNo.Text = entity.ReferenceNo;
            hdnLocationID.Value = entity.LocationID.ToString();
            txtLocationCode.Text = entity.LocationCode;
            txtLocationName.Text = entity.LocationName;
            cboReturnType.Value = entity.GCDirectPurchaseReturnType.ToString();
            txtNotes.Text = entity.Remarks;
            chkPPN.Checked = entity.IsIncludeVAT;
            txtTotalRetur.Text = entity.TransactionAmount.ToString();

            decimal tempTransactionAmount = -1;
            BindGridView(1, true, ref PageCount, ref RowCount, ref tempTransactionAmount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount, ref decimal transactionAmount)
        {
            string filterExpression = "1 = 0";
            if (hdnPRID.Value != "0")
                filterExpression = string.Format("DirectPurchaseReturnID = {0} AND GCItemDetailStatus != '{1}'", hdnPRID.Value, Constant.TransactionStatus.VOID);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvDirectPurchaseReturnDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            if (transactionAmount > -1)
                transactionAmount = BusinessLayer.GetDirectPurchaseReturnHd(Convert.ToInt32(hdnPRID.Value)).TransactionAmount;
            List<vDirectPurchaseReturnDt> lstEntity = BusinessLayer.GetvDirectPurchaseReturnDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ItemName1 ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
        #endregion

        #region Save Edit Header
        public void SavePurchaseReturnHd(IDbContext ctx, ref int PRID, ref string PRNo)
        {
            DirectPurchaseReturnHdDao entityHdDao = new DirectPurchaseReturnHdDao(ctx);
            if (hdnPRID.Value == "0")
            {
                DirectPurchaseReturnHd entityHd = new DirectPurchaseReturnHd();
                entityHd.ReturnDate = Helper.GetDatePickerValue(txtPurchaseReturnDate.Text);
                entityHd.DirectPurchaseID = Convert.ToInt32(hdnDirectPurchaseID.Value);
                entityHd.LocationID = Convert.ToInt32(hdnLocationID.Value);
                entityHd.BusinessPartnerID = Convert.ToInt32(hdnSupplierID.Value);
                entityHd.GCDirectPurchaseReturnType = cboReturnType.Value.ToString();
                entityHd.ReferenceNo = Request.Form[txtReferenceNo.UniqueID];
                entityHd.ReferenceDate = Helper.GetDatePickerValue(Request.Form[txtReferenceDate.UniqueID]);
                entityHd.IsIncludeVAT = chkPPN.Checked;
                if (entityHd.IsIncludeVAT)
                    entityHd.VATPercentage = Convert.ToDecimal(hdnVATPercentage.Value);
                else
                    entityHd.VATPercentage = 0;
                entityHd.Remarks = txtNotes.Text;
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
                PRID = Convert.ToInt32(hdnPRID.Value);
                PRNo = txtDirectPurchaseReturnNo.Text;
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
                entity.DirectPurchaseReturnID = Convert.ToInt32(hdnPRID.Value);
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
                DirectPurchaseReturnHd entity = BusinessLayer.GetDirectPurchaseReturnHd(Convert.ToInt32(hdnPRID.Value));
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
                DirectPurchaseReturnHd entity = entityHdDao.Get(Convert.ToInt32(hdnPRID.Value));
                entity.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                List<DirectPurchaseReturnDt> lstEntity = BusinessLayer.GetDirectPurchaseReturnDtList(string.Format("DirectPurchaseReturnID = {0} AND GCItemDetailStatus != '{1}'", hdnPRID.Value, Constant.TransactionStatus.VOID), ctx);
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
                DirectPurchaseReturnHd entity = entityHdDao.Get(Convert.ToInt32(hdnPRID.Value));
                entity.GCTransactionStatus = Constant.TransactionStatus.VOID;
                List<DirectPurchaseReturnDt> lstEntity = BusinessLayer.GetDirectPurchaseReturnDtList(string.Format("DirectPurchaseReturnID = {0} AND GCItemDetailStatus != '{1}'", hdnPRID.Value, Constant.TransactionStatus.VOID), ctx);
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
            DirectPurchaseHdDao entityHdDao = new DirectPurchaseHdDao(ctx);
            if (type == "confirm")
            {
                try
                {
                    DirectPurchaseReturnHd entity = BusinessLayer.GetDirectPurchaseReturnHd(Convert.ToInt32(hdnPRID.Value));
                    entity.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                    List<DirectPurchaseReturnDt> lstEntity = BusinessLayer.GetDirectPurchaseReturnDtList(string.Format("DirectPurchaseReturnID = {0} AND GCItemDetailStatus != '{1}'", hdnPRID.Value, Constant.TransactionStatus.VOID));
                    foreach (DirectPurchaseReturnDt entityDt in lstEntity)
                    {
                        entityDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
                        entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        BusinessLayer.UpdateDirectPurchaseReturnDt(entityDt);
                    }
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.UpdateDirectPurchaseReturnHd(entity);
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
                    DirectPurchaseReturnHd entity = BusinessLayer.GetDirectPurchaseReturnHd(Convert.ToInt32(hdnPRID.Value));
                    entity.GCTransactionStatus = Constant.TransactionStatus.VOID;
                    List<DirectPurchaseReturnDt> lstEntity = BusinessLayer.GetDirectPurchaseReturnDtList(string.Format("DirectPurchaseReturnID = {0} AND GCItemDetailStatus != '{1}'", hdnPRID.Value, Constant.TransactionStatus.VOID));
                    foreach (DirectPurchaseReturnDt entityDt in lstEntity)
                    {
                        entityDt.GCItemDetailStatus = Constant.TransactionStatus.VOID;
                        entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        BusinessLayer.UpdateDirectPurchaseReturnDt(entityDt);
                    }
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.UpdateDirectPurchaseReturnHd(entity);
                    retval = entity.DirectPurchaseReturnNo;

                    DirectPurchaseHd entityDirect = BusinessLayer.GetDirectPurchaseHdList(string.Format("DirectPurchaseID = {0}", hdnDirectPurchaseID.Value))[0];
                    entityDirect.IsHasPurchaseReturn = false;
                    entityDirect.DirectPurchaseReturnID = null;
                    entityHdDao.Update(entityDirect);

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
                    PRID = Convert.ToInt32(hdnPRID.Value);
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
            entityDt.Quantity = Convert.ToDecimal(txtQuantity.Text);
            entityDt.GCItemUnit = cboItemUnit.Value.ToString();
            entityDt.GCBaseUnit = hdnGCBaseUnit.Value;
            entityDt.UnitPrice = Convert.ToDecimal(Request.Form[txtPrice.UniqueID]);
            entityDt.ConversionFactor = Convert.ToDecimal(hdnConversionFactor.Value);
            entityDt.DiscountPercentage1 = Convert.ToDecimal(Request.Form[txtDiscount.UniqueID]);
            entityDt.DiscountPercentage2 = 0;
            entityDt.GCPurchaseReturnReason = cboReason.Value.ToString();
            if (entityDt.GCPurchaseReturnReason == "X162^999")
            {
                entityDt.PurchaseReturnReason = txtReason.Text;
            }
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

                DirectPurchaseHd entity = BusinessLayer.GetDirectPurchaseHdList(string.Format("DirectPurchaseID = {0}", hdnDirectPurchaseID.Value))[0];
                entity.IsHasPurchaseReturn = true;
                entity.DirectPurchaseReturnID = Convert.ToInt32(hdnPRID.Value);
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