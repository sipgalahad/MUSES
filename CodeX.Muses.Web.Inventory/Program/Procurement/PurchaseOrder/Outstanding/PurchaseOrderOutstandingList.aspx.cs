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
            List<GetLocationUserList> lstUserLocation = BusinessLayer.GetLocationUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.PURCHASE_ORDER, "");
            if (lstUserLocation.Count > 0)
            {
                List<ServiceUnitLocation> lstServiceUnitLocation = BusinessLayer.GetServiceUnitLocationList(string.Format("LocationID IN ({0})", string.Join(",", lstUserLocation.Select(p => p.LocationID).ToList())));
                hdnListSiteServiceUnitID.Value = string.Join(",", lstServiceUnitLocation.Select(p => p.SiteServiceUnitID).ToList());
            }
            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            if (hdnListSiteServiceUnitID.Value != "")
                filterExpression += String.Format("TransactionCode = '{0}' AND SiteServiceUnitID IN ({1}) AND GCTransactionStatus = '{2}'", Constant.TransactionCode.PURCHASE_ORDER, hdnListSiteServiceUnitID.Value, Constant.TransactionStatus.APPROVED);
            else
                filterExpression += "1 = 0";

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvPurchaseOrderHdOutstandingRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vPurchaseOrderHdOutstanding> lstEntity = BusinessLayer.GetvPurchaseOrderHdOutstandingList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "OrderDate DESC");
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
            newEntity.SiteServiceUnitID = oldEntity.SiteServiceUnitID;
            newEntity.DeliveryDate = oldEntity.DeliveryDate;
            newEntity.POExpiredDate = oldEntity.POExpiredDate;
            newEntity.BusinessPartnerID = oldEntity.BusinessPartnerID;
            newEntity.PaymentRemarks = oldEntity.PaymentRemarks;
            newEntity.Remarks = oldEntity.Remarks;
            newEntity.IsIncludeVAT = oldEntity.IsIncludeVAT;
            newEntity.FinalDiscountPercentage = oldEntity.FinalDiscountPercentage;
            newEntity.FinalDiscountAmount = oldEntity.FinalDiscountAmount;
            newEntity.TotalNetTransactionAmount = oldEntity.TotalNetTransactionAmount;
            if (newEntity.IsIncludeVAT)
                newEntity.VATPercentage = oldEntity.VATPercentage;
            else
                newEntity.VATPercentage = 0;
            newEntity.VATAmount = oldEntity.VATAmount;
            newEntity.OrderDate = DateTime.Now;
            newEntity.GCPurchaseOrderType = oldEntity.GCPurchaseOrderType;
            newEntity.TermID = oldEntity.TermID;
            newEntity.GCFrancoRegion = oldEntity.GCFrancoRegion;
            newEntity.GCCurrencyCode = oldEntity.GCCurrencyCode;
            newEntity.CurrencyRate = oldEntity.CurrencyRate;
            newEntity.DownPaymentAmount = oldEntity.DownPaymentAmount;
            newEntity.LocationID = oldEntity.LocationID;
            newEntity.GCCurrencyCode = oldEntity.GCCurrencyCode;
            newEntity.CurrencyRate = oldEntity.CurrencyRate;
            newEntity.ReferencePurchaseOrderID = oldEntity.PurchaseOrderID;
        }

        private void CopyToEntityDtFinal(PurchaseOrderDt newEntityDt, PurchaseOrderDt oldEntityDt, PurchaseReceiveDt purchaseReceiveDt)
        {
            newEntityDt.ItemID = oldEntityDt.ItemID;
            newEntityDt.ReceivedQuantity = newEntityDt.Quantity = purchaseReceiveDt.Quantity;
            newEntityDt.ReceivedInformation = "|" + purchaseReceiveDt.PurchaseReceiveID + "|";
            newEntityDt.GCPurchaseUnit = purchaseReceiveDt.GCItemUnit;
            newEntityDt.GCBaseUnit = purchaseReceiveDt.GCBaseUnit;
            newEntityDt.ConversionFactor = purchaseReceiveDt.ConversionFactor;
            newEntityDt.PurchaseRequestID = oldEntityDt.PurchaseRequestID;
            newEntityDt.UnitPrice = purchaseReceiveDt.UnitPrice;
            newEntityDt.DiscountPercentage1 = purchaseReceiveDt.DiscountPercentage1;
            newEntityDt.DiscountAmount1 = purchaseReceiveDt.DiscountAmount1;
            newEntityDt.DiscountPercentage2 = purchaseReceiveDt.DiscountPercentage2;
            newEntityDt.DiscountAmount2 = purchaseReceiveDt.DiscountAmount2;
            newEntityDt.LineAmount = purchaseReceiveDt.LineAmount;
            newEntityDt.IsBonusItem = purchaseReceiveDt.IsBonusItem;
            newEntityDt.Remarks = oldEntityDt.Remarks;
            newEntityDt.GCItemDetailStatus = Constant.TransactionStatus.CLOSED;
        }

        private void CopyToEntityDtTemp(PurchaseOrderDt newEntityDt, PurchaseOrderDt oldEntityDt)
        {
            newEntityDt.ItemID = oldEntityDt.ItemID;
            newEntityDt.Quantity = oldEntityDt.Quantity - oldEntityDt.ReceivedQuantity;
            newEntityDt.GCPurchaseUnit = oldEntityDt.GCPurchaseUnit;
            newEntityDt.GCBaseUnit = oldEntityDt.GCBaseUnit;
            newEntityDt.ConversionFactor = oldEntityDt.ConversionFactor;
            newEntityDt.PurchaseRequestID = oldEntityDt.PurchaseRequestID;
            newEntityDt.UnitPrice = oldEntityDt.UnitPrice;

            decimal amount = newEntityDt.UnitPrice * newEntityDt.Quantity;
            newEntityDt.DiscountPercentage1 = oldEntityDt.DiscountPercentage1;
            newEntityDt.DiscountPercentage2 = oldEntityDt.DiscountPercentage2;

            newEntityDt.DiscountAmount1 = amount * newEntityDt.DiscountPercentage1 / 100;
            newEntityDt.DiscountAmount2 = (amount - newEntityDt.DiscountAmount1) * newEntityDt.DiscountPercentage2 / 100;
            newEntityDt.LineAmount = amount - newEntityDt.DiscountAmount1 - newEntityDt.DiscountAmount2;
            newEntityDt.IsBonusItem = oldEntityDt.IsBonusItem;
            newEntityDt.Remarks = oldEntityDt.Remarks;
            newEntityDt.LineAmount = oldEntityDt.CustomSubTotal;
            newEntityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage, ref string retval)
        {
            if (type == "close")
            {
                bool result = true;
                IDbContext ctx = DbFactory.Configure(true);
                PurchaseOrderHdDao POHdDao = new PurchaseOrderHdDao(ctx);
                PurchaseOrderDtDao PODtDao = new PurchaseOrderDtDao(ctx);
                try
                {
                    PurchaseOrderHd purchaseOrderHd = POHdDao.Get(Convert.ToInt32(hdnID.Value));
                    purchaseOrderHd.GCTransactionStatus = Constant.TransactionStatus.CLOSED;
                    purchaseOrderHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    POHdDao.Update(purchaseOrderHd);

                    List<PurchaseOrderDt> lstPurchaseOrderDt = BusinessLayer.GetPurchaseOrderDtList(String.Format("PurchaseOrderID = {0} AND IsDeleted = 0", hdnID.Value), ctx);
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
            else
            {
                bool result = true;
                IDbContext ctx = DbFactory.Configure(true);
                PurchaseOrderHdDao POHdDao = new PurchaseOrderHdDao(ctx);
                PurchaseOrderDtDao PODtDao = new PurchaseOrderDtDao(ctx);
                PurchaseReceiveHdDao PRHdDao = new PurchaseReceiveHdDao(ctx);
                PurchaseReceiveDtDao PRDtDao = new PurchaseReceiveDtDao(ctx);
                try
                {
                    PurchaseOrderHd purchaseOrderHd = POHdDao.Get(Convert.ToInt32(hdnID.Value));

                    PurchaseReceiveHd purchaseReceiveHd = BusinessLayer.GetPurchaseReceiveHdList(string.Format("PurchaseOrderID = {0}", purchaseOrderHd.PurchaseOrderID), ctx).FirstOrDefault();
                    if (purchaseReceiveHd == null)
                    {
                        result = false;
                        errMessage = "Belum Ada Penerimaan Untuk Pemesanan Dengan No <b>" + purchaseOrderHd.PurchaseOrderNo + "</b>";
                    }
                    else
                    {
                        purchaseOrderHd.GCTransactionStatus = Constant.TransactionStatus.CLOSED;
                        purchaseOrderHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                        POHdDao.Update(purchaseOrderHd);

                        List<PurchaseOrderDt> lstPurchaseOrderDt = BusinessLayer.GetPurchaseOrderDtList(String.Format("PurchaseOrderID = {0} AND IsDeleted = 0", hdnID.Value), ctx);
                        foreach (PurchaseOrderDt PODt in lstPurchaseOrderDt)
                        {
                            PODt.GCItemDetailStatus = Constant.TransactionStatus.CLOSED;
                            PODt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            PODtDao.Update(PODt);
                        }

                        PurchaseOrderHd finalEntityHd = new PurchaseOrderHd();
                        CopyToEntityHd(finalEntityHd, purchaseOrderHd);
                        finalEntityHd.TransactionCode = Constant.TransactionCode.PURCHASE_ORDER;
                        finalEntityHd.PurchaseOrderNo = BusinessLayer.GenerateTransactionNo(finalEntityHd.TransactionCode, finalEntityHd.OrderDate, ctx);
                        finalEntityHd.GCTransactionStatus = Constant.TransactionStatus.CLOSED;
                        finalEntityHd.IsFinalPO = true;
                        ctx.CommandType = CommandType.Text;
                        ctx.Command.Parameters.Clear();
                        finalEntityHd.CreatedBy = AppSession.UserLogin.UserID;
                        finalEntityHd.PurchaseOrderID = POHdDao.Insert(finalEntityHd);


                        PurchaseOrderHd outstandingEntityHd = new PurchaseOrderHd();
                        CopyToEntityHd(outstandingEntityHd, purchaseOrderHd);
                        outstandingEntityHd.TransactionCode = Constant.TransactionCode.PURCHASE_ORDER;
                        outstandingEntityHd.PurchaseOrderNo = BusinessLayer.GenerateTransactionNo(outstandingEntityHd.TransactionCode, outstandingEntityHd.OrderDate, ctx);
                        outstandingEntityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                        ctx.CommandType = CommandType.Text;
                        ctx.Command.Parameters.Clear();
                        outstandingEntityHd.CreatedBy = AppSession.UserLogin.UserID;
                        outstandingEntityHd.PurchaseOrderID = POHdDao.Insert(outstandingEntityHd);

                        purchaseReceiveHd.PurchaseOrderID = finalEntityHd.PurchaseOrderID;
                        purchaseReceiveHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                        PRHdDao.Update(purchaseReceiveHd);

                        List<PurchaseReceiveDt> lstPurchaseReceiveDt = BusinessLayer.GetPurchaseReceiveDtList(string.Format("PurchaseReceiveID = {0} AND GCItemDetailStatus != '{1}'", purchaseReceiveHd.PurchaseReceiveID, Constant.TransactionStatus.VOID), ctx);
                        foreach (PurchaseOrderDt entity in lstPurchaseOrderDt)
                        {
                            if (entity.ReceivedQuantity > 0) //final
                            {
                                PurchaseReceiveDt prDt = lstPurchaseReceiveDt.FirstOrDefault(p => p.ItemID == entity.ItemID);
                                PurchaseOrderDt entityDt = new PurchaseOrderDt();
                                CopyToEntityDtFinal(entityDt, entity, prDt);
                                entityDt.PurchaseOrderID = finalEntityHd.PurchaseOrderID;
                                entityDt.CreatedBy = AppSession.UserLogin.UserID;
                                PODtDao.Insert(entityDt);
                            }
                            if (entity.ReceivedQuantity < entity.Quantity) //outstanding
                            {
                                PurchaseOrderDt entityDt = new PurchaseOrderDt();
                                CopyToEntityDtTemp(entityDt, entity);
                                entityDt.PurchaseOrderID = outstandingEntityHd.PurchaseOrderID;
                                entityDt.CreatedBy = AppSession.UserLogin.UserID;
                                PODtDao.Insert(entityDt);
                            }
                        }
                        retval = finalEntityHd.PurchaseOrderNo + "|" + outstandingEntityHd.PurchaseOrderNo;
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
        }
    }
}