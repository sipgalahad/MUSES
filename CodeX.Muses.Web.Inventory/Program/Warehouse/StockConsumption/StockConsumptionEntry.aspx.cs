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
using System.Data;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class StockConsumptionEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.ITEM_CONSUMPTION;
        }

        #region Html Getter
        protected string OnGetFilterExpressionLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.ITEM_CONSUMPTION);
        }
        protected string OnGetFilterExpressionItemProduct()
        {
            return string.Format("IsDeleted = 0");
        }
        #endregion

        protected override void InitializeDataControl()
        {
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();

            SetControlProperties();
            hdnIsEditable.Value = "1";

            int count = BusinessLayer.GetLocationUserRowCount(string.Format("UserID = {0} AND IsDeleted = 0", AppSession.UserLogin.UserID));
            if (count > 0)
                hdnRecordFilterExpression.Value = string.Format("FromLocationID IN (SELECT LocationID FROM LocationUser WHERE UserID = {0} AND IsDeleted = 0)", AppSession.UserLogin.UserID);
            else
            {
                count = BusinessLayer.GetLocationUserRoleRowCount(string.Format("RoleID IN (SELECT RoleID FROM UserInRole WHERE UserID = {0} AND SiteID = '{1}') AND IsDeleted = 0", AppSession.UserLogin.UserID, AppSession.UserLogin.SiteID));
                if (count > 0)
                    hdnRecordFilterExpression.Value = string.Format("FromLocationID IN (SELECT LocationID FROM LocationUserRole WHERE RoleID IN (SELECT RoleID FROM UserInRole WHERE UserID = {0} AND SiteID = '{1}') AND IsDeleted = 0)", AppSession.UserLogin.UserID, AppSession.UserLogin.SiteID);
                else
                    hdnRecordFilterExpression.Value = "";
            }

            BindGridView(1, true, ref PageCount, ref RowCount);

            Helper.SetControlEntrySetting(txtQuantity, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtItemCode, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboItemUnit, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtConversion, new ControlEntrySetting(false, false, true), "mpTrx");
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> listStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.CONSUMPTION_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboGCConsumptionType, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.CONSUMPTION_TYPE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnConsumptionID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtConsumptionNo, new ControlEntrySetting(true, true, false,""));
            SetControlEntrySetting(txtConsumptionDate, new ControlEntrySetting(true, false, false, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(lblLocation, new ControlEntrySetting(true, false));
            SetControlEntrySetting(txtLocationCode, new ControlEntrySetting(true, false, true, ""));
            SetControlEntrySetting(txtLocationName, new ControlEntrySetting(false, false, false,""));

            SetControlEntrySetting(cboGCConsumptionType, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false,""));
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
            string filterExpression = String.Format("TransactionCode = '{0}'", Constant.TransactionCode.ITEM_CONSUMPTION);
            if (hdnRecordFilterExpression.Value != "")
                filterExpression += string.Format(" AND {0}", hdnRecordFilterExpression.Value);
            return filterExpression;
        }
        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvItemTransactionHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vItemTransactionHd entity = BusinessLayer.GetvItemTransactionHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvItemTransactionHdRowIndex(filterExpression, keyValue, "TransactionID DESC");
            vItemTransactionHd entity = BusinessLayer.GetvItemTransactionHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vItemTransactionHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
                hdnIsEditable.Value = "0";
            }
            else
                hdnIsEditable.Value = "1";
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN || entity.GCTransactionStatus != Constant.TransactionStatus.VOID)
                hdnPrintStatus.Value = "true";
            else 
                hdnPrintStatus.Value = "false";

            hdnConsumptionID.Value = entity.TransactionID.ToString();
            txtConsumptionNo.Text = entity.TransactionNo;
            txtConsumptionDate.Text = entity.TransactionDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            hdnLocationID.Value = entity.FromLocationID.ToString();
            txtLocationCode.Text = entity.FromLocationCode;
            txtLocationName.Text = entity.FromLocationName;

            cboGCConsumptionType.Value = entity.GCConsumptionType;
            txtRemarks.Text = entity.Remarks;

            BindGridView(1, true, ref PageCount, ref RowCount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnConsumptionID.Value != "")
                filterExpression = string.Format("TransactionID = {0} AND GCItemDetailStatus != '{1}'", hdnConsumptionID.Value, Constant.TransactionStatus.VOID);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvItemTransactionDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vItemTransactionDt> lstEntity = BusinessLayer.GetvItemTransactionDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ItemName1 ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
        #endregion

        #region Save Header
        public void SaveItemConsumptionHd(IDbContext ctx, ref int ConsumptionID)
        {
            ItemTransactionHdDao entityHdDao = new ItemTransactionHdDao(ctx);
            if (hdnConsumptionID.Value == "0")
            {
                ItemTransactionHd entityHd = new ItemTransactionHd();
                entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtConsumptionDate.UniqueID]);
                entityHd.FromLocationID = Convert.ToInt32(hdnLocationID.Value);
                entityHd.ToLocationID = null;
                entityHd.GCConsumptionType = cboGCConsumptionType.Value.ToString();
                entityHd.Remarks = txtRemarks.Text;
                entityHd.TransactionCode = Constant.TransactionCode.ITEM_CONSUMPTION;
                entityHd.TransactionNo = BusinessLayer.GenerateTransactionNo(entityHd.TransactionCode, entityHd.TransactionDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;

                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);
                ConsumptionID = BusinessLayer.GetItemTransactionHdMaxID(ctx);
            }
            else
            {
                ConsumptionID = Convert.ToInt32(hdnConsumptionID.Value);
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            try
            {
                int OrderID = 0;
                SaveItemConsumptionHd(ctx, ref OrderID);
                retval = OrderID.ToString();
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

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            try
            {
                ItemTransactionHd entityHd = BusinessLayer.GetItemTransactionHd(Convert.ToInt32(hdnConsumptionID.Value));
                entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtConsumptionDate.UniqueID]);
                entityHd.FromLocationID = Convert.ToInt32(hdnLocationID.Value);
                entityHd.ToLocationID = null;
                entityHd.GCConsumptionType = cboGCConsumptionType.Value.ToString();
                entityHd.Remarks = txtRemarks.Text;
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateItemTransactionHd(entityHd);
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
            ItemTransactionHdDao itemTransactionHdDao = new ItemTransactionHdDao(ctx);
            ItemTransactionDtDao itemTransactionDtDao = new ItemTransactionDtDao(ctx);
            try
            {
                ItemTransactionHd itemTransactionHd = itemTransactionHdDao.Get(Convert.ToInt32(hdnConsumptionID.Value));
                itemTransactionHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                itemTransactionHd.Remarks = txtRemarks.Text;
                itemTransactionHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                itemTransactionHdDao.Update(itemTransactionHd);

                string filterExpressionPurchaseOrderHd = String.Format("TransactionID = {0} AND GCItemDetailStatus != '{1}'", hdnConsumptionID.Value, Constant.TransactionStatus.VOID);
                List<ItemTransactionDt> lstItemTransactionDt = BusinessLayer.GetItemTransactionDtList(filterExpressionPurchaseOrderHd, ctx);
                foreach (ItemTransactionDt itemTransactionDt in lstItemTransactionDt)
                {
                    itemTransactionDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
                    itemTransactionDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    itemTransactionDtDao.Update(itemTransactionDt);
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

        protected override bool OnProposeRecord(ref string errMessage)
        {
            try
            {
                ItemTransactionHd entity = BusinessLayer.GetItemTransactionHd(Convert.ToInt32(hdnConsumptionID.Value));
                entity.GCTransactionStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateItemTransactionHd(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        protected override bool OnVoidRecord(ref string errMessage)
        {
            try
            {
                ItemTransactionHd entity = BusinessLayer.GetItemTransactionHd(Convert.ToInt32(hdnConsumptionID.Value));
                entity.GCTransactionStatus = Constant.TransactionStatus.VOID;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateItemTransactionHd(entity);
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
            int adjustmentID = 0;
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    adjustmentID = Convert.ToInt32(hdnConsumptionID.Value);
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage, ref adjustmentID))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                adjustmentID = Convert.ToInt32(hdnConsumptionID.Value);
                if (OnDeleteEntityDt(ref errMessage, adjustmentID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpConsumptionID"] = adjustmentID.ToString();
        }

        private void ControlToEntity(ItemTransactionDt entityDt)
        {
            entityDt.ItemID = Convert.ToInt32(hdnItemID.Value);
            entityDt.Quantity = Convert.ToDecimal(txtQuantity.Text);
            entityDt.GCItemUnit = cboItemUnit.Value.ToString();
            entityDt.GCBaseUnit = hdnGCBaseUnit.Value;
            entityDt.ConversionFactor = Convert.ToDecimal(hdnItemConversionFactor.Value);
            entityDt.BaseQuantity = entityDt.Quantity * entityDt.ConversionFactor;
            //entityDt.GCConsumptionReason = cboGCConsumptionReason.Value.ToString();
            //entityDt.ConsumptionReason = txtConsumptionReason.Text;
            entityDt.Remarks = txtNotesDt.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int ConsumptionID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ItemTransactionDtDao entityDtDao = new ItemTransactionDtDao(ctx);
            try
            {
                SaveItemConsumptionHd(ctx, ref ConsumptionID);
                ItemTransactionDt entityDt = new ItemTransactionDt();
                ControlToEntity(entityDt);
                entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                entityDt.TransactionID = ConsumptionID;
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
            ItemTransactionDtDao entityDtDao = new ItemTransactionDtDao(ctx);
            try
            {
                ItemTransactionDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
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
            ItemTransactionDtDao entityDtDao = new ItemTransactionDtDao(ctx);
            try
            {
                ItemTransactionDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
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