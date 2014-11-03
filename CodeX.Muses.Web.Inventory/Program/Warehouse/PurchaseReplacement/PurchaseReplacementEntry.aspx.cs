using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;
using DevExpress.Web.ASPxCallbackPanel;
using System.Data;
using CodeX.Common;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class PurchaseReplacementEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.PURCHASE_REPLACEMENT;
        }

        #region Html Getter
        protected string GetLocationFilterExpression()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.PURCHASE_RETURN_REPLACEMENT);
        }

        protected string GetSupplierFilterExpression()
        {
            return string.Format("GCBusinessPartnerType = '{0}' AND IsDeleted = 0", Constant.BusinessObjectType.SUPPLIER);
        }

        protected string GetPurchaseReturnFilterExpression()
        {
            return string.Format("GCTransactionStatus = '{0}' AND GCPurchaseReturnType = '{1}'", Constant.TransactionStatus.APPROVED, Constant.PurchaseReturnType.REPLACEMENT);
        }

        protected string OnGetFilterExpressionItemProduct()
        {
            return string.Format("GCItemType = '{0}' AND IsDeleted = 0", Constant.ItemType.PRODUCT);
        }

        protected string GetTransactionStatusVoid()
        {
            return Constant.TransactionStatus.VOID;
        }
        #endregion

        protected override void InitializeDataControl()
        {
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();
            SetControlProperties();
            hdnIsEditable.Value = "1";

            int count = BusinessLayer.GetLocationUserRowCount(string.Format("UserID = {0} AND IsDeleted = 0", AppSession.UserLogin.UserID));
            if (count > 0)
                hdnRecordFilterExpression.Value = string.Format("LocationID IN (SELECT LocationID FROM LocationUser WHERE UserID = {0} AND IsDeleted = 0)", AppSession.UserLogin.UserID);
            else
            {
                count = BusinessLayer.GetLocationUserRoleRowCount(string.Format("RoleID IN (SELECT RoleID FROM UserInRole WHERE UserID = {0} AND SiteID = '{1}') AND IsDeleted = 0", AppSession.UserLogin.UserID, AppSession.UserLogin.SiteID));
                if (count > 0)
                    hdnRecordFilterExpression.Value = string.Format("LocationID IN (SELECT LocationID FROM LocationUserRole WHERE RoleID IN (SELECT RoleID FROM UserInRole WHERE UserID = {0} AND SiteID = '{1}') AND IsDeleted = 0)", AppSession.UserLogin.UserID, AppSession.UserLogin.SiteID);
                else
                    hdnRecordFilterExpression.Value = "";
            }

            BindGridView(1, true, ref PageCount, ref RowCount);
            Helper.SetControlEntrySetting(txtOldItemCode, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtQuantity, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtItemCode, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboItemUnit, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnPurchaseReplacementID, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(txtPurchaseReplacementNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtReplacementDate, new ControlEntrySetting(true, false, true, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(hdnLocationID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtLocationCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtLocationName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(hdnSupplierID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSupplierCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtSupplierName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(hdnPurchaseReturnID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtPurchaseReturnNo, new ControlEntrySetting(true, false, true));
            
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));

            SetControlEntrySetting(lblLocation, new ControlEntrySetting(true, false));
            SetControlEntrySetting(lblPurchaseReturn, new ControlEntrySetting(true, false));
            SetControlEntrySetting(lblSupplier, new ControlEntrySetting(true, false));
        }

        #region Load Entity
        public override void OnAddRecord()
        {
            hdnPageCount.Value = "0";
            hdnIsEditable.Value = "1";
        }

        protected string IsEditable()
        {
            return hdnIsEditable.Value;
        }

        protected string GetFilterExpression()
        {
            return hdnRecordFilterExpression.Value;
        }

        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvPurchaseReplacementHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vPurchaseReplacementHd entity = BusinessLayer.GetvPurchaseReplacementHd(filterExpression, PageIndex, "PurchaseReplacementID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvPurchaseReplacementHdRowIndex(filterExpression, keyValue, "PurchaseReplacementID DESC");
            vPurchaseReplacementHd entity = BusinessLayer.GetvPurchaseReplacementHd(filterExpression, PageIndex, "PurchaseReplacementID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vPurchaseReplacementHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
                hdnIsEditable.Value = "0";
            }
            else
                hdnIsEditable.Value = "1";
            hdnPurchaseReplacementID.Value = entity.PurchaseReplacementID.ToString();
            txtPurchaseReplacementNo.Text = entity.PurchaseReplacementNo;
            txtReplacementDate.Text = entity.ReplacementDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            hdnSupplierID.Value = entity.BusinessPartnerID.ToString();
            txtSupplierCode.Text = entity.BusinessPartnerCode;
            txtSupplierName.Text = entity.BusinessPartnerName;
            hdnPurchaseReturnID.Value = entity.PurchaseReturnID.ToString();
            txtPurchaseReturnNo.Text = entity.PurchaseReturnNo;
            hdnLocationID.Value = entity.LocationID.ToString();
            txtLocationCode.Text = entity.LocationCode;
            txtLocationName.Text = entity.LocationName;
            txtReferenceNo.Text = entity.ReferenceNo;
            if (entity.ReferenceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT) != Constant.ConstantDate.DEFAULT_NULL)
                txtReferenceDate.Text = entity.ReferenceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            else
                txtReferenceDate.Text = "";
            txtRemarks.Text = entity.Remarks;

            BindGridView(1, true, ref PageCount, ref RowCount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnPurchaseReplacementID.Value != "")
                filterExpression = string.Format("PurchaseReplacementID = {0} AND GCItemDetailStatus != '{1}'", hdnPurchaseReplacementID.Value, Constant.TransactionStatus.VOID);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvPurchaseReplacementDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vPurchaseReplacementDt> lstEntity = BusinessLayer.GetvPurchaseReplacementDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "FromItemName1 ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
        #endregion

        #region Save
        public void SavePurchaseReplacementHd(IDbContext ctx, ref int PurchaseReplacementID)
        {
            PurchaseReplacementHdDao entityHdDao = new PurchaseReplacementHdDao(ctx);
            if (hdnPurchaseReplacementID.Value == "0")
            {
                PurchaseReplacementHd entityHd = new PurchaseReplacementHd();
                entityHd.ReplacementDate = Helper.GetDatePickerValue(txtReplacementDate);
                entityHd.BusinessPartnerID = Convert.ToInt32(hdnSupplierID.Value);
                entityHd.PurchaseReturnID = Convert.ToInt32(hdnPurchaseReturnID.Value);
                entityHd.LocationID = Convert.ToInt32(hdnLocationID.Value);
                entityHd.ReferenceNo = txtReferenceNo.Text;
                if (txtReferenceDate.Text != "")
                    entityHd.ReferenceDate = Helper.GetDatePickerValue(txtReferenceDate);
                else
                    entityHd.ReferenceDate = Helper.InitializeDateTimeNull();
                entityHd.Remarks = txtRemarks.Text;

                entityHd.PurchaseReplacementNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.PURCHASE_RETURN_REPLACEMENT, entityHd.ReplacementDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;

                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);
                PurchaseReplacementID = BusinessLayer.GetPurchaseReplacementHdMaxID(ctx);
            }
            else
            {
                PurchaseReplacementID = Convert.ToInt32(hdnPurchaseReplacementID.Value);
            }
        }

        private void ControlToEntity(PurchaseReplacementHd entity)
        {
            entity.ReplacementDate = Helper.GetDatePickerValue(txtReplacementDate);
            entity.BusinessPartnerID = Convert.ToInt32(hdnSupplierID.Value);
            entity.PurchaseReturnID = Convert.ToInt32(hdnPurchaseReturnID.Value);
            entity.LocationID = Convert.ToInt32(hdnLocationID.Value);
            entity.ReferenceNo = txtReferenceNo.Text;
            if (txtReferenceDate.Text != "")
                entity.ReferenceDate = Helper.GetDatePickerValue(txtReferenceDate);
            else
                entity.ReferenceDate = Helper.InitializeDateTimeNull();
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            try
            {
                PurchaseReplacementHd entity = new PurchaseReplacementHd();
                ControlToEntity(entity);
                entity.PurchaseReplacementNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.PURCHASE_RETURN_REPLACEMENT, entity.ReplacementDate);
                entity.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertPurchaseReplacementHd(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            try
            {
                PurchaseReplacementHd entity = BusinessLayer.GetPurchaseReplacementHd(Convert.ToInt32(hdnPurchaseReplacementID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdatePurchaseReplacementHd(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        protected override bool OnApproveRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseReplacementHdDao purchaseHdDao = new PurchaseReplacementHdDao(ctx);
            PurchaseReplacementDtDao purchaseDtDao = new PurchaseReplacementDtDao(ctx);
            try
            {
                PurchaseReplacementHd purchaseHd = purchaseHdDao.Get(Convert.ToInt32(hdnPurchaseReplacementID.Value));
                purchaseHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                purchaseHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                purchaseHdDao.Update(purchaseHd);

                string filterExpressionPurchaseOrderHd = String.Format("PurchaseReplacementID = {0} AND GCItemDetailStatus != '{1}'", hdnPurchaseReplacementID.Value, Constant.TransactionStatus.VOID);
                List<PurchaseReplacementDt> lstPurchaseOrderDt = BusinessLayer.GetPurchaseReplacementDtList(filterExpressionPurchaseOrderHd);
                foreach (PurchaseReplacementDt purchaseDt in lstPurchaseOrderDt)
                {
                    purchaseDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
                    purchaseDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    purchaseDtDao.Update(purchaseDt);
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

        protected override bool OnVoidRecord(ref string errMessage)
        {
            try
            {
                PurchaseReplacementHd entity = BusinessLayer.GetPurchaseReplacementHd(Convert.ToInt32(hdnPurchaseReplacementID.Value)); 
                entity.GCTransactionStatus = Constant.TransactionStatus.VOID;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdatePurchaseReplacementHd(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            int purchaseReplacementID = 0;
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    purchaseReplacementID = Convert.ToInt32(hdnPurchaseReplacementID.Value);
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage, ref purchaseReplacementID))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                purchaseReplacementID = Convert.ToInt32(hdnPurchaseReplacementID.Value);
                if (OnDeleteEntityDt(ref errMessage, purchaseReplacementID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpReplacementID"] = purchaseReplacementID.ToString();
        }

        private void ControlToEntity(PurchaseReplacementDt entityDt)
        {
            entityDt.FromItemID = Convert.ToInt32(hdnOldItemID.Value);
            entityDt.ToItemID = Convert.ToInt32(hdnItemID.Value);
            entityDt.Quantity = Convert.ToDecimal(txtQuantity.Text);
            entityDt.GCItemUnit = cboItemUnit.Value.ToString();
            entityDt.GCBaseUnit = hdnGCBaseUnit.Value;
            entityDt.ConversionFactor = Convert.ToDecimal(hdnItemConversionFactor.Value);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int purchaseReplacementID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseReplacementDtDao entityDtDao = new PurchaseReplacementDtDao(ctx);
            try
            {
                SavePurchaseReplacementHd(ctx, ref purchaseReplacementID);
                PurchaseReplacementDt entityDt = new PurchaseReplacementDt();
                ControlToEntity(entityDt);
                entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                entityDt.PurchaseReplacementID = purchaseReplacementID;
                entityDt.CreatedBy = AppSession.UserLogin.UserID;
                entityDtDao.Insert(entityDt);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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
            PurchaseReplacementDtDao entityDtDao = new PurchaseReplacementDtDao(ctx);
            try
            {
                PurchaseReplacementDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entityDt);
                entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDtDao.Update(entityDt);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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
            PurchaseReplacementDtDao entityDtDao = new PurchaseReplacementDtDao(ctx);
            try
            {
                PurchaseReplacementDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                entityDt.GCItemDetailStatus = Constant.TransactionStatus.VOID;
                entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDtDao.Update(entityDt);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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

        #region Callback
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
    }
}