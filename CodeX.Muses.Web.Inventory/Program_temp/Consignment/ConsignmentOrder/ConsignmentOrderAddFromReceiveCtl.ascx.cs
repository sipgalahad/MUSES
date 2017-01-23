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
using System.Web.UI.HtmlControls;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class ConsignmentOrderAddFromReceiveCtl : BaseEntryPopupCtl
    {
        protected string filterExpressionPurchaseOrder = "";
        public override void InitializeDataControl(string param)
        {
            //hdnSupplierID.Value = param;
            //filterExpressionPurchaseOrder = string.Format("BusinessPartnerID = '{0}' AND GCTransactionStatus = '{1}' AND TransactionCode = '{2}'", hdnSupplierID.Value, Constant.TransactionStatus.APPROVED, Constant.TransactionCode.CONSIGNMENT_ORDER);
            BindGridView();
        }

        public String GetPurchaseReceiveExpression()
        {
            Int32 SupplierID = ((ConsignmentOrderEntry)Page).GetSupplierID();
            return String.Format("PurchaseReceiveID IN (SELECT PurchaseReceiveID FROM vPurchaseReceivePOCustom WHERE (ReceivedQuantity - OrderQuantity) > 0) AND SupplierID = {0}", SupplierID);
        }

        private void BindGridView()
        {
            string filterExpression = "1 = 0";
            if (hdnPurchaseReceiveID.Value != "")
            {
                int orderID = ((ConsignmentOrderEntry)Page).GetOrderID();
                filterExpression = string.Format("PurchaseReceiveID = {0} AND (ReceivedQuantity - OrderQuantity) > 0 AND ItemID NOT IN (SELECT ItemID FROM PurchaseOrderDt WHERE PurchaseOrderID = {1}) ORDER BY ItemName1 ASC", hdnPurchaseReceiveID.Value, orderID);
            }

            List<vPurchaseReceivePOCustom> lstEntity = BusinessLayer.GetvPurchaseReceivePOCustomList(filterExpression);
            grdPopupView.DataSource = lstEntity;
            grdPopupView.DataBind();
        }

        protected void grdPopupView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vPurchaseReceivePOCustom entity = e.Row.DataItem as vPurchaseReceivePOCustom;
                TextBox txtPOQty = e.Row.FindControl("txtPOQty") as TextBox;
                TextBox txtReceivedQty = e.Row.FindControl("txtReceivedQty") as TextBox;
                TextBox txtQuantity = e.Row.FindControl("txtQuantity") as TextBox;
                txtPOQty.Text = entity.OrderQuantity.ToString();
                txtReceivedQty.Text = entity.ReceivedQuantity.ToString();
                txtQuantity.Text = (entity.ReceivedQuantity - entity.OrderQuantity).ToString();
            }
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                BindGridView();
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntityDt(PurchaseOrderDt entityDt, String[] data)
        {
            entityDt.ItemID = Convert.ToInt32(data[0]);
            entityDt.Quantity = Convert.ToDecimal(data[1]);
            entityDt.GCPurchaseUnit = data[2];
            entityDt.GCBaseUnit = data[3];
            entityDt.ConversionFactor = Convert.ToDecimal(data[4]);
            entityDt.UnitPrice = Convert.ToDecimal(data[5]);
            entityDt.DiscountPercentage1 = Convert.ToDecimal(data[6]);
            entityDt.DiscountPercentage2 = Convert.ToDecimal(data[7]);
            entityDt.LineAmount = entityDt.CustomSubTotal;
            entityDt.ReceivedQuantity = Convert.ToDecimal(data[9]);
            entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseReceivePODao purchaseReceivePODao = new PurchaseReceivePODao(ctx);
            PurchaseOrderDtDao purchaseOrderDtDao = new PurchaseOrderDtDao(ctx);
            PurchaseReceiveDtDao purchaseReceiveDtDao = new PurchaseReceiveDtDao(ctx);
            PurchaseOrderHdDao purchaseOrderHdDao = new PurchaseOrderHdDao(ctx);

            try
            {
                Int32 OrderID = ((ConsignmentOrderEntry)Page).GetOrderID();
                Int32 PurchaseReceiveID = Convert.ToInt32(hdnPurchaseReceiveID.Value);

                ((ConsignmentOrderEntry)Page).SavePurchaseOrderHd(ctx, ref OrderID);
                List<String> items = hdnLstItem.Value.Split('|').ToList();
                items.Remove("");
                PurchaseOrderDt entityDt = null;
                PurchaseReceivePO entityPO = null;
                PurchaseReceiveDt receiveDt = null;

                foreach (String item in items)
                {
                    String[] data = item.Split(';');
                    Int32 itemID = Convert.ToInt32(data[0]);

                    entityDt = new PurchaseOrderDt();
                    entityDt.PurchaseOrderID = OrderID;
                    ControlToEntityDt(entityDt, data);
                    purchaseOrderDtDao.Insert(entityDt);

                    entityPO = new PurchaseReceivePO();
                    entityPO.ItemID = itemID;
                    entityPO.PurchaseOrderID = OrderID;
                    entityPO.PurchaseReceiveID = PurchaseReceiveID;
                    entityPO.ReceivedQuantity = Convert.ToDecimal(data[8]);
                    purchaseReceivePODao.Insert(entityPO);

                    receiveDt = BusinessLayer.GetPurchaseReceiveDtList(String.Format("PurchaseReceiveID = {0} AND ItemID = {1} AND GCItemDetailStatus != '{2}'", PurchaseReceiveID, itemID, Constant.TransactionStatus.VOID), ctx)[0];
                    receiveDt.PurchaseOrderID = OrderID;
                    receiveDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    purchaseReceiveDtDao.Update(receiveDt);
                }

                retval = purchaseOrderHdDao.Get(OrderID).PurchaseOrderNo;
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
    }
}