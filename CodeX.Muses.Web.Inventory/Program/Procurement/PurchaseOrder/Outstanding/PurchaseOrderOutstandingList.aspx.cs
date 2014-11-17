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
    public partial class PurchaseOrderOutstandingList : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.APPROVED_PURCHASE_ORDER;
        }

        public override void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowEdit = IsAllowDelete = false;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += String.Format("TransactionCode = '{0}' AND GCTransactionStatus = '{1}'", Constant.TransactionCode.PURCHASE_ORDER, Constant.TransactionStatus.APPROVED);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvPurchaseOrderHdRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vPurchaseOrderHd> lstEntity = BusinessLayer.GetvPurchaseOrderHdList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "OrderDate DESC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
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


        private void CopyToEntityHd(PurchaseOrderHd newEntity, PurchaseOrderHd oldEntity)
        {
            newEntity.DeliveryDate = oldEntity.DeliveryDate;
            newEntity.POExpiredDate = oldEntity.POExpiredDate;
            newEntity.BusinessPartnerID = oldEntity.BusinessPartnerID;
            newEntity.PaymentRemarks = oldEntity.PaymentRemarks;
            newEntity.Remarks = oldEntity.Remarks;
            newEntity.IsIncludeVAT = oldEntity.IsIncludeVAT;
            newEntity.FinalDiscount = oldEntity.FinalDiscount;
            if (newEntity.IsIncludeVAT)
                newEntity.VATPercentage = oldEntity.VATPercentage;
            else
                newEntity.VATPercentage = 0;

            newEntity.OrderDate = Helper.GetDatePickerValue(DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT));
            newEntity.GCPurchaseOrderType = oldEntity.GCPurchaseOrderType;
            newEntity.TermID = oldEntity.TermID;
            newEntity.GCFrancoRegion = oldEntity.GCFrancoRegion;
            newEntity.GCCurrencyCode = oldEntity.GCCurrencyCode;
            newEntity.CurrencyRate = oldEntity.CurrencyRate;
            newEntity.DownPaymentAmount = oldEntity.DownPaymentAmount;
            newEntity.LocationID = oldEntity.LocationID;
            newEntity.GCCurrencyCode = oldEntity.GCCurrencyCode;
            newEntity.CurrencyRate = oldEntity.CurrencyRate;
        }

        private void CopyToEntityDt(PurchaseOrderDt newEntityDt, PurchaseOrderDt oldEntityDt)
        {
            newEntityDt.ItemID = oldEntityDt.ItemID;
            newEntityDt.Quantity = oldEntityDt.Quantity;
            newEntityDt.GCPurchaseUnit = oldEntityDt.GCPurchaseUnit;
            newEntityDt.GCBaseUnit = oldEntityDt.GCBaseUnit;
            newEntityDt.ConversionFactor = oldEntityDt.ConversionFactor;
            newEntityDt.PurchaseRequestID = oldEntityDt.PurchaseRequestID;
            newEntityDt.UnitPrice = oldEntityDt.UnitPrice;
            newEntityDt.DiscountPercentage1 = oldEntityDt.DiscountPercentage1;
            newEntityDt.DiscountPercentage2 = oldEntityDt.DiscountPercentage2;
            newEntityDt.IsBonusItem = oldEntityDt.IsBonusItem;
            newEntityDt.Remarks = oldEntityDt.Remarks;
            newEntityDt.LineAmount = oldEntityDt.CustomSubTotal;
            newEntityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            int OrderID;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseOrderHdDao POHdDao = new PurchaseOrderHdDao(ctx);
            PurchaseOrderDtDao PODtDao = new PurchaseOrderDtDao(ctx);
            if (type == "close")
            {
                try
                {
                    string filterExpressionPOHd = String.Format("PurchaseOrderID = {0}", hdnID.Value);

                    List<PurchaseOrderHd> lstPurchaseOrderHd = BusinessLayer.GetPurchaseOrderHdList(filterExpressionPOHd);
                    foreach (PurchaseOrderHd POHd in lstPurchaseOrderHd)
                    {
                        POHd.GCTransactionStatus = Constant.TransactionStatus.CLOSED;
                        POHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                        POHdDao.Update(POHd);
                    }

                    List<PurchaseOrderDt> lstPurchaseOrderDt = BusinessLayer.GetPurchaseOrderDtList(filterExpressionPOHd, ctx);
                    foreach (PurchaseOrderDt PODt in lstPurchaseOrderDt)
                    {
                        PODt.GCItemDetailStatus = Constant.TransactionStatus.CLOSED;
                        PODt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        PODtDao.Update(PODt);
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
            }
            else
            {
                try
                {
                    string filterExpressionPOHd = String.Format("PurchaseOrderID = {0}", hdnID.Value);

                    List<PurchaseOrderHd> lstPurchaseOrderHd = BusinessLayer.GetPurchaseOrderHdList(filterExpressionPOHd);
                    foreach (PurchaseOrderHd POHd in lstPurchaseOrderHd)
                    {
                        POHd.GCTransactionStatus = Constant.TransactionStatus.CLOSED;
                        POHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                        POHdDao.Update(POHd);
                    }

                    List<PurchaseOrderDt> lstPurchaseOrderDt = BusinessLayer.GetPurchaseOrderDtList(filterExpressionPOHd, ctx);
                    foreach (PurchaseOrderDt PODt in lstPurchaseOrderDt)
                    {
                        PODt.GCItemDetailStatus = Constant.TransactionStatus.CLOSED;
                        PODt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        PODtDao.Update(PODt);
                    }

                    PurchaseOrderHd entityHd = new PurchaseOrderHd();
                    CopyToEntityHd(entityHd, lstPurchaseOrderHd[0]);
                    entityHd.PurchaseOrderNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.PURCHASE_ORDER, entityHd.OrderDate, ctx);
                    entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                    
                    ctx.CommandType = CommandType.Text;
                    ctx.Command.Parameters.Clear();
                    entityHd.CreatedBy = AppSession.UserLogin.UserID;
                    POHdDao.Insert(entityHd);
                    OrderID = BusinessLayer.GetPurchaseOrderHdMaxID(ctx);
                    ctx.Command.Parameters.Clear();
                    string filterExpressionPODt = String.Format("PurchaseOrderID = {0} AND ReceivedInformation IS NULL", hdnID.Value);
                    List<PurchaseOrderDt> lstPurchaseOrderDtnew = BusinessLayer.GetPurchaseOrderDtList(filterExpressionPODt, ctx);
                    foreach (PurchaseOrderDt entity in lstPurchaseOrderDtnew)
                    {
                        PurchaseOrderDt entityDt = new PurchaseOrderDt();
                        CopyToEntityDt(entityDt, entity);
                        entityDt.PurchaseOrderID = OrderID;
                        entityDt.CreatedBy = AppSession.UserLogin.UserID;
                        PODtDao.Insert(entityDt);
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

            }
            return result;
        }
    }
}