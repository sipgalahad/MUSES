using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;
using CodeX.Data.Core.Dal;
using System.Data;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class PurchaseOrderCloseNewCtl : BaseEntryPopupCtl
    {
        protected string OnGetFilterExpressionSupplier()
        {
            return string.Format("GCBusinessPartnerType = '{0}' AND IsDeleted = 0", Constant.BusinessObjectType.SUPPLIER);
        }
        public override void InitializeDataControl(string param)
        {
            hdnPurchaseOrderID.Value = param;

            vPurchaseOrderHd entity = BusinessLayer.GetvPurchaseOrderHdList(string.Format("PurchaseOrderID = {0}", hdnPurchaseOrderID.Value)).FirstOrDefault();
            hdnSupplierID.Value = entity.BusinessPartnerID.ToString();
            txtSupplierCode.Text = entity.BusinessPartnerCode;
            txtSupplierName.Text = entity.BusinessPartnerName;

            List<Term> listTerm = BusinessLayer.GetTermList(string.Format("IsDeleted = 0"));
            Methods.SetComboBoxField<Term>(cboTerm, listTerm, "TermName", "TermID");
            cboTerm.Value = entity.TermID.ToString();
        }

        private void CopyToEntityHd(PurchaseOrderHd newEntity, PurchaseOrderHd oldEntity)
        {
            newEntity.SiteServiceUnitID = oldEntity.SiteServiceUnitID;
            newEntity.DeliveryDate = oldEntity.DeliveryDate;
            newEntity.POExpiredDate = oldEntity.POExpiredDate;
            newEntity.BusinessPartnerID = Convert.ToInt32(hdnSupplierID.Value);
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
            newEntity.TermID = Convert.ToInt32(cboTerm.Value);
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
            newEntityDt.DiscountAmount1 = oldEntityDt.DiscountAmount1;
            newEntityDt.DiscountPercentage2 = oldEntityDt.DiscountPercentage2;
            newEntityDt.DiscountAmount2 = oldEntityDt.DiscountAmount2;
            newEntityDt.LineAmount = oldEntityDt.LineAmount;
            newEntityDt.IsBonusItem = oldEntityDt.IsBonusItem;
            newEntityDt.Remarks = oldEntityDt.Remarks;
            newEntityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseOrderHdDao POHdDao = new PurchaseOrderHdDao(ctx);
            PurchaseOrderDtDao PODtDao = new PurchaseOrderDtDao(ctx);
            try
            {
                string filterExpression = String.Format("PurchaseOrderID = {0} AND IsDeleted = 0", hdnPurchaseOrderID.Value);

                PurchaseOrderHd oldPurchaseOrderHd = POHdDao.Get(Convert.ToInt32(hdnPurchaseOrderID.Value));
                oldPurchaseOrderHd.GCTransactionStatus = Constant.TransactionStatus.CLOSED;
                oldPurchaseOrderHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                POHdDao.Update(oldPurchaseOrderHd);

                List<PurchaseOrderDt> lstOldPurchaseOrderDt = BusinessLayer.GetPurchaseOrderDtList(filterExpression, ctx);
                foreach (PurchaseOrderDt PODt in lstOldPurchaseOrderDt)
                {
                    PODt.GCItemDetailStatus = Constant.TransactionStatus.CLOSED;
                    PODt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    PODtDao.Update(PODt);
                }

                PurchaseOrderHd entityHd = new PurchaseOrderHd();
                CopyToEntityHd(entityHd, oldPurchaseOrderHd);
                entityHd.TransactionCode = Constant.TransactionCode.PURCHASE_ORDER;
                entityHd.PurchaseOrderNo = BusinessLayer.GenerateTransactionNo(entityHd.TransactionCode, entityHd.OrderDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;

                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHd.PurchaseOrderID = POHdDao.Insert(entityHd);
                ctx.Command.Parameters.Clear();
                string filterExpressionPODt = String.Format("PurchaseOrderID = {0} AND ReceivedInformation IS NULL", hdnPurchaseOrderID.Value);
                List<PurchaseOrderDt> lstPurchaseOrderDtnew = BusinessLayer.GetPurchaseOrderDtList(filterExpressionPODt, ctx);
                foreach (PurchaseOrderDt entity in lstPurchaseOrderDtnew)
                {
                    PurchaseOrderDt entityDt = new PurchaseOrderDt();
                    CopyToEntityDt(entityDt, entity);
                    entityDt.PurchaseOrderID = entityHd.PurchaseOrderID;
                    entityDt.CreatedBy = AppSession.UserLogin.UserID;
                    PODtDao.Insert(entityDt);
                }
                retval = entityHd.PurchaseOrderNo;

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