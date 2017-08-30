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
    public partial class PurchaseBudgetEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.PURCHASE_BUDGET;
        }

        #region Html Getter
        protected string OnGetFilterExpressionServiceUnit()
        {
            return string.Format("{0};{1};1 = 1", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID);
        }
        protected string OnGetTransactionStatusVoid()
        {
            return Constant.TransactionStatus.VOID;
        }
        protected string OnGetFilterExpressionItemProduct()
        {
            return string.Format("GCItemType = '{0}' AND IsDeleted = 0", Constant.ItemType.PRODUCT);
        }
        #endregion

        protected override void InitializeDataControl()
        {
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();
            List<GetServiceUnitUserList> lstUserServiceUnit = BusinessLayer.GetServiceUnitUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, "");
            if (lstUserServiceUnit.Count == 1)
            {
                GetServiceUnitUserList serviceUnit = lstUserServiceUnit.FirstOrDefault();
                hdnDefaultSiteServiceUnitID.Value = serviceUnit.SiteServiceUnitID.ToString();
                hdnDefaultServiceUnitCode.Value = serviceUnit.ServiceUnitCode;
                hdnDefaultServiceUnitName.Value = serviceUnit.ServiceUnitName;
            }

            SetControlProperties();
            hdnIsEditable.Value = "1";

            BindGridView(1, true, ref PageCount, ref RowCount);

            Helper.SetControlEntrySetting(txtQuantity, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtItemCode, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboItemUnit, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtConversion, new ControlEntrySetting(false, false, true), "mpTrx");
            Helper.SetControlEntrySetting(txtTotalAmount, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        protected override void SetControlProperties()
        {
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnTransactionID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtTransactionNo, new ControlEntrySetting(true, true, false,""));
            SetControlEntrySetting(txtTransactionDate, new ControlEntrySetting(true, false, false, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtYear, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(lblSiteServiceUnit, new ControlEntrySetting(true, false));
            SetControlEntrySetting(txtServiceUnitCode, new ControlEntrySetting(true, false, true, ""));
            SetControlEntrySetting(txtServiceUnitName, new ControlEntrySetting(false, false, false, ""));
            SetControlEntrySetting(hdnSiteServiceUnitID, new ControlEntrySetting(false, false));

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
            string filterExpression = hdnRecordFilterExpression.Value;
            return filterExpression;
        }
        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvPurchaseBudgetHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vPurchaseBudgetHd entity = BusinessLayer.GetvPurchaseBudgetHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvPurchaseBudgetHdRowIndex(filterExpression, keyValue, "TransactionID DESC");
            vPurchaseBudgetHd entity = BusinessLayer.GetvPurchaseBudgetHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vPurchaseBudgetHd entity, ref bool isShowWatermark, ref string watermarkText)
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

            hdnTransactionID.Value = entity.TransactionID.ToString();
            txtTransactionNo.Text = entity.TransactionNo;
            txtTransactionDate.Text = entity.TransactionDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtYear.Text = entity.YearPeriod.ToString();
            hdnSiteServiceUnitID.Value = entity.SiteServiceUnitID.ToString();
            txtServiceUnitCode.Text = entity.ServiceUnitCode;
            txtServiceUnitName.Text = entity.ServiceUnitName;

            txtRemarks.Text = entity.Remarks;

            BindGridView(1, true, ref PageCount, ref RowCount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnTransactionID.Value != "")
                filterExpression = string.Format("TransactionID = {0} AND GCItemDetailStatus != '{1}'", hdnTransactionID.Value, Constant.TransactionStatus.VOID);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvPurchaseBudgetDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vPurchaseBudgetDt> lstEntity = BusinessLayer.GetvPurchaseBudgetDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ItemName1 ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
        #endregion

        #region Save Header
        public void SavePurchaseBudgetHd(IDbContext ctx, ref int TransactionID)
        {
            PurchaseBudgetHdDao entityHdDao = new PurchaseBudgetHdDao(ctx);
            if (hdnTransactionID.Value == "0")
            {
                PurchaseBudgetHd entityHd = new PurchaseBudgetHd();
                entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
                entityHd.YearPeriod = Convert.ToInt32(txtYear.Text);
                entityHd.SiteServiceUnitID = Convert.ToInt32(hdnSiteServiceUnitID.Value);
                entityHd.Remarks = txtRemarks.Text;
                entityHd.TransactionNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.PURCHASE_BUDGET, entityHd.TransactionDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;

                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                TransactionID = entityHdDao.Insert(entityHd);
            }
            else
            {
                TransactionID = Convert.ToInt32(hdnTransactionID.Value);
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            try
            {
                int OrderID = 0;
                SavePurchaseBudgetHd(ctx, ref OrderID);
                retval = OrderID.ToString();
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
                PurchaseBudgetHd entityHd = BusinessLayer.GetPurchaseBudgetHd(Convert.ToInt32(hdnTransactionID.Value));
                if (entityHd.GCTransactionStatus == Constant.TransactionStatus.OPEN)
                {
                    entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
                    entityHd.YearPeriod = Convert.ToInt32(txtYear.Text);
                    entityHd.SiteServiceUnitID = Convert.ToInt32(hdnSiteServiceUnitID.Value);
                    entityHd.Remarks = txtRemarks.Text;
                    entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.UpdatePurchaseBudgetHd(entityHd);
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
            PurchaseBudgetHdDao itemTransactionHdDao = new PurchaseBudgetHdDao(ctx);
            PurchaseBudgetDtDao itemTransactionDtDao = new PurchaseBudgetDtDao(ctx);
            try
            {
                PurchaseBudgetHd itemTransactionHd = itemTransactionHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                if (itemTransactionHd.GCTransactionStatus == Constant.TransactionStatus.OPEN || itemTransactionHd.GCTransactionStatus == Constant.TransactionStatus.WAIT_FOR_APPROVAL)
                {
                    itemTransactionHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                    itemTransactionHd.Remarks = txtRemarks.Text;
                    itemTransactionHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    itemTransactionHdDao.Update(itemTransactionHd);

                    string filterExpressionPurchaseOrderHd = String.Format("TransactionID = {0} AND GCItemDetailStatus != '{1}'", hdnTransactionID.Value, Constant.TransactionStatus.VOID);
                    List<PurchaseBudgetDt> lstPurchaseBudgetDt = BusinessLayer.GetPurchaseBudgetDtList(filterExpressionPurchaseOrderHd, ctx);
                    foreach (PurchaseBudgetDt itemTransactionDt in lstPurchaseBudgetDt)
                    {
                        itemTransactionDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
                        itemTransactionDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        itemTransactionDtDao.Update(itemTransactionDt);
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
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseBudgetHdDao itemTransactionHdDao = new PurchaseBudgetHdDao(ctx);
            PurchaseBudgetDtDao itemTransactionDtDao = new PurchaseBudgetDtDao(ctx);
            try
            {
                PurchaseBudgetHd itemTransactionHd = itemTransactionHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                if (itemTransactionHd.GCTransactionStatus == Constant.TransactionStatus.OPEN)
                {
                    itemTransactionHd.GCTransactionStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                    itemTransactionHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    itemTransactionHdDao.Update(itemTransactionHd);

                    string filterExpressionPurchaseOrderHd = String.Format("TransactionID = {0} AND GCItemDetailStatus != '{1}'", hdnTransactionID.Value, Constant.TransactionStatus.VOID);
                    List<PurchaseBudgetDt> lstPurchaseBudgetDt = BusinessLayer.GetPurchaseBudgetDtList(filterExpressionPurchaseOrderHd, ctx);
                    foreach (PurchaseBudgetDt itemTransactionDt in lstPurchaseBudgetDt)
                    {
                        itemTransactionDt.GCItemDetailStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                        itemTransactionDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        itemTransactionDtDao.Update(itemTransactionDt);
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

        protected override bool OnReopenRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseBudgetHdDao itemTransactionHdDao = new PurchaseBudgetHdDao(ctx);
            PurchaseBudgetDtDao itemTransactionDtDao = new PurchaseBudgetDtDao(ctx);
            try
            {
                PurchaseBudgetHd itemTransactionHd = itemTransactionHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                if (itemTransactionHd.GCTransactionStatus == Constant.TransactionStatus.APPROVED || itemTransactionHd.GCTransactionStatus == Constant.TransactionStatus.WAIT_FOR_APPROVAL)
                {
                    itemTransactionHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                    itemTransactionHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    itemTransactionHdDao.Update(itemTransactionHd);

                    string filterExpressionPurchaseOrderHd = String.Format("TransactionID = {0} AND GCItemDetailStatus != '{1}'", hdnTransactionID.Value, Constant.TransactionStatus.VOID);
                    List<PurchaseBudgetDt> lstPurchaseBudgetDt = BusinessLayer.GetPurchaseBudgetDtList(filterExpressionPurchaseOrderHd, ctx);
                    foreach (PurchaseBudgetDt itemTransactionDt in lstPurchaseBudgetDt)
                    {
                        itemTransactionDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                        itemTransactionDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        itemTransactionDtDao.Update(itemTransactionDt);
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

        protected override bool OnVoidRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseBudgetHdDao itemTransactionHdDao = new PurchaseBudgetHdDao(ctx);
            PurchaseBudgetDtDao itemTransactionDtDao = new PurchaseBudgetDtDao(ctx);
            try
            {
                PurchaseBudgetHd itemTransactionHd = itemTransactionHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                if (itemTransactionHd.GCTransactionStatus == Constant.TransactionStatus.OPEN)
                {
                    itemTransactionHd.GCTransactionStatus = Constant.TransactionStatus.VOID;
                    itemTransactionHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    itemTransactionHdDao.Update(itemTransactionHd);

                    string filterExpressionPurchaseOrderHd = String.Format("TransactionID = {0} AND GCItemDetailStatus != '{1}'", hdnTransactionID.Value, Constant.TransactionStatus.VOID);
                    List<PurchaseBudgetDt> lstPurchaseBudgetDt = BusinessLayer.GetPurchaseBudgetDtList(filterExpressionPurchaseOrderHd, ctx);
                    foreach (PurchaseBudgetDt itemTransactionDt in lstPurchaseBudgetDt)
                    {
                        itemTransactionDt.GCItemDetailStatus = Constant.TransactionStatus.VOID;
                        itemTransactionDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        itemTransactionDtDao.Update(itemTransactionDt);
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
                    adjustmentID = Convert.ToInt32(hdnTransactionID.Value);
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
                adjustmentID = Convert.ToInt32(hdnTransactionID.Value);
                if (OnDeleteEntityDt(ref errMessage, adjustmentID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpTransactionID"] = adjustmentID.ToString();
        }

        private void ControlToEntity(PurchaseBudgetDt entityDt)
        {
            entityDt.ItemID = Convert.ToInt32(hdnItemID.Value);
            entityDt.Quantity = Convert.ToDecimal(txtQuantity.Text);
            entityDt.GCItemUnit = cboItemUnit.Value.ToString().Split('|')[0];
            entityDt.GCBaseUnit = hdnGCBaseUnit.Value;
            entityDt.ConversionFactor = Convert.ToDecimal(hdnConversionFactor.Value);
            entityDt.BaseQuantity = entityDt.Quantity * entityDt.ConversionFactor;
            entityDt.TotalAmount = Convert.ToDecimal(txtTotalAmount.Text);
            entityDt.Remarks = txtNotesDt.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int TransactionID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseBudgetDtDao entityDtDao = new PurchaseBudgetDtDao(ctx);
            try
            {
                SavePurchaseBudgetHd(ctx, ref TransactionID);
                PurchaseBudgetDt entityDt = new PurchaseBudgetDt();
                ControlToEntity(entityDt);
                entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                entityDt.TransactionID = TransactionID;
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
            PurchaseBudgetDtDao entityDtDao = new PurchaseBudgetDtDao(ctx);
            try
            {
                PurchaseBudgetDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
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
            PurchaseBudgetDtDao entityDtDao = new PurchaseBudgetDtDao(ctx);
            try
            {
                PurchaseBudgetDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
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

        #region Callback
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