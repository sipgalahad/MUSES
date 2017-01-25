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
    public partial class PurchaseReceiveDetailCtl : BaseEntryPopupCtl
    {
        protected string OnGetFilterExpressionPurchaseOrder()
        {
            return string.Format("SiteServiceUnitID = {0} AND BusinessPartnerID = {1} AND GCTransactionStatus = '{2}'", hdnSiteServiceUnitID.Value, hdnSupplierID.Value, Constant.TransactionStatus.APPROVED);
        }
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');
            hdnSiteServiceUnitID.Value = temp[0];
            hdnSupplierID.Value = temp[1];
            hdnOrderID.Value = temp[2];
            hdnIsLineAmountRounded.Value = temp[3];
            hdnLineAmountRoundedFormat.Value = temp[4];
            hdnIsTotalAmountRounded.Value = temp[5];
            hdnTotalAmountRoundedFormat.Value = temp[6];

            if (hdnOrderID.Value != "" && hdnOrderID.Value != "0")
            {
                lblOrderNo.Attributes.Add("class", "lblNormal");
                txtOrderNo.Text = BusinessLayer.GetPurchaseOrderHd(Convert.ToInt32(hdnOrderID.Value)).PurchaseOrderNo;
            }
            else
                lblOrderNo.Attributes.Add("class", "lblLink");
            BindGridView();
        }

        private void BindGridView()
        {
            string filterExpression = "1 = 0";
            if (hdnOrderID.Value != "")
                filterExpression = string.Format("PurchaseOrderID = {0} AND (Quantity - ReceivedQuantity) > 0 AND IsDeleted = 0 ORDER BY ItemName1 ASC", hdnOrderID.Value);
            
            List<vPurchaseOrderDt> lstEntity = BusinessLayer.GetvPurchaseOrderDtList(filterExpression);
            lvwView.DataSource = lstEntity;
            lvwView.DataBind();
        }

        protected void lvwView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                vPurchaseOrderDt entity = e.Item.DataItem as vPurchaseOrderDt;
                TextBox txtReceivedItem = e.Item.FindControl("txtReceivedItem") as TextBox;
                TextBox txtUnitPrice = e.Item.FindControl("txtUnitPrice") as TextBox;
                TextBox txtDiscountPercentage1 = e.Item.FindControl("txtDiscountPercentage1") as TextBox;
                TextBox txtDiscountAmount1 = e.Item.FindControl("txtDiscountAmount1") as TextBox;
                TextBox txtDiscountPercentage2 = e.Item.FindControl("txtDiscountPercentage2") as TextBox;
                TextBox txtDiscountAmount2 = e.Item.FindControl("txtDiscountAmount2") as TextBox;
                TextBox txtLineAmount = e.Item.FindControl("txtLineAmount") as TextBox;
                TextBox txtExpired = e.Item.FindControl("txtExpired") as TextBox;
                TextBox txtBatchNo = e.Item.FindControl("txtBatchNo") as TextBox;
                HtmlGenericControl lblPurchaseUnit = e.Item.FindControl("lblPurchaseUnit") as HtmlGenericControl;
                txtReceivedItem.Text = (entity.Quantity - entity.ReceivedQuantity).ToString();
                txtUnitPrice.Text = entity.UnitPrice.ToString();
                decimal price = ((entity.Quantity - entity.ReceivedQuantity) * entity.UnitPrice);
                decimal discountAmount1 = price * entity.DiscountPercentage1 / 100;
                decimal discountAmount2 = (price - discountAmount1) * entity.DiscountPercentage2 / 100;

                txtDiscountPercentage1.Text = entity.DiscountPercentage1.ToString();
                txtDiscountPercentage2.Text = entity.DiscountPercentage2.ToString();
                
                txtDiscountAmount1.Text = discountAmount1.ToString();
                txtDiscountAmount2.Text = discountAmount2.ToString();
                lblPurchaseUnit.InnerText = string.Format("{0} ({1})", entity.PurchaseUnit, entity.ConversionFactor.ToString("G29"));
                decimal LineAmount = price - discountAmount1 - discountAmount2;
                if (entity.IsLineAmountRounded)
                    LineAmount = Math.Ceiling(LineAmount / entity.LineAmountRoundedFormat) * entity.LineAmountRoundedFormat;
                txtLineAmount.Text = LineAmount.ToString();

                Helper.SetControlEntrySetting(txtReceivedItem, new ControlEntrySetting(true, true, true), "mpEntryPopup");
                Helper.SetControlEntrySetting(txtUnitPrice, new ControlEntrySetting(true, true, true), "mpEntryPopup");
                Helper.SetControlEntrySetting(txtDiscountPercentage1, new ControlEntrySetting(true, true, true), "mpEntryPopup");
                Helper.SetControlEntrySetting(txtDiscountAmount1, new ControlEntrySetting(true, true, true), "mpEntryPopup");
                Helper.SetControlEntrySetting(txtDiscountPercentage2, new ControlEntrySetting(true, true, true), "mpEntryPopup");
                Helper.SetControlEntrySetting(txtDiscountAmount2, new ControlEntrySetting(true, true, true), "mpEntryPopup");
                Helper.SetControlEntrySetting(txtLineAmount, new ControlEntrySetting(true, true, true), "mpEntryPopup");
                Helper.SetControlEntrySetting(txtBatchNo, new ControlEntrySetting(true, true, true), "mpEntryPopup");
                Helper.SetControlEntrySetting(txtExpired, new ControlEntrySetting(true, true, true), "mpEntryPopup");

                if (!entity.IsControlExpired)
                {
                    txtExpired.Style.Add("display", "none");
                    txtBatchNo.Style.Add("display", "none");
                }
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

        protected string DateTimeNowDatePicker()
        {
            return DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseReceiveDtDao entityPurchaseReceiveDtDao = new PurchaseReceiveDtDao(ctx); 
            PurchaseOrderDtDao entityPODtDao = new PurchaseOrderDtDao(ctx);
            PurchaseOrderHdDao entityPOHdDao = new PurchaseOrderHdDao(ctx);
            PurchaseReceivePODao purchaseReceivePODao = new PurchaseReceivePODao(ctx);
            PurchaseReceiveDtExpiredDao entityPurchaseReceiveDtExpiredDao = new PurchaseReceiveDtExpiredDao(ctx);
            PurchaseRequestPODao purchaseRequestPODao = new PurchaseRequestPODao(ctx);
            try
            {
                List<PurchaseOrderDt> lstPODt = new List<PurchaseOrderDt>();                
                int purchaseReceiveID = 0;
                string purchaseReceiveNo = "";
                ((PurchaseReceiveEntry)Page).SavePurchaseReceiveHd(ctx, ref purchaseReceiveID, ref purchaseReceiveNo);

                List<PurchaseOrderDt> lstPurchaseOrderDt = BusinessLayer.GetPurchaseOrderDtList(string.Format("ID IN ({0})", hdnLstPurchaseOrderDtID.Value), ctx);
                List<PurchaseRequestPO> lstPurchaseRequestPO = BusinessLayer.GetPurchaseRequestPOList(string.Format("PurchaseOrderID = {0}", hdnOrderID.Value), ctx);
                string[] lstSaveValue = hdnSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    int purchaseOrderDtID = Convert.ToInt32(temp[0]);

                    PurchaseOrderDt entityPODt = lstPurchaseOrderDt.FirstOrDefault(p => p.ID == purchaseOrderDtID);

                    PurchaseReceiveDt entityPRDt = new PurchaseReceiveDt();
                    entityPRDt.PurchaseReceiveID = purchaseReceiveID;
                    entityPRDt.PurchaseOrderID = Convert.ToInt32(temp[1]);
                    entityPRDt.ItemID = entityPODt.ItemID;
                    entityPRDt.ItemName1 = entityPODt.ItemName1;
                    entityPRDt.Quantity = Convert.ToDecimal(temp[2]);
                    entityPRDt.GCItemUnit = temp[11];
                    entityPRDt.GCBaseUnit = entityPODt.GCBaseUnit;
                    entityPRDt.ConversionFactor = Convert.ToDecimal(temp[10]);
                    entityPRDt.UnitPrice = Convert.ToDecimal(temp[3]);
                    entityPRDt.DiscountPercentage1 = Convert.ToDecimal(temp[6]);
                    entityPRDt.DiscountAmount1 = Convert.ToDecimal(temp[7]);
                    entityPRDt.DiscountPercentage2 = Convert.ToDecimal(temp[8]);
                    entityPRDt.DiscountAmount2 = Convert.ToDecimal(temp[9]);
                    entityPRDt.IsBonusItem = false;
                    entityPRDt.IsControlExpired = (temp[4] != "");
                    entityPRDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                    entityPRDt.LineAmountBeforeRounded = (entityPRDt.Quantity * entityPRDt.UnitPrice) - entityPRDt.DiscountAmount1 - entityPRDt.DiscountAmount2;
                    entityPRDt.LineAmount = Convert.ToDecimal(temp[12]);
                    entityPRDt.RoundedAmount = entityPRDt.LineAmount - entityPRDt.LineAmountBeforeRounded;
                    entityPRDt.CreatedBy = AppSession.UserLogin.UserID;
                    entityPRDt.ID = entityPurchaseReceiveDtDao.Insert(entityPRDt);

                    PurchaseReceivePO entityPRPO = new PurchaseReceivePO();
                    entityPRPO.PurchaseOrderID = (int)entityPRDt.PurchaseOrderID;
                    entityPRPO.PurchaseReceiveID = entityPRDt.PurchaseReceiveID;
                    entityPRPO.ItemID = entityPRDt.ItemID;
                    entityPRPO.ItemName1 = entityPRDt.ItemName1;
                    entityPRPO.ReceivedQuantity = entityPRDt.Quantity;
                    purchaseReceivePODao.Insert(entityPRPO);

                    decimal receivedQty = entityPRDt.Quantity;
                    List<PurchaseRequestPO> lstPurchaseRequestPO1 = lstPurchaseRequestPO.Where(p => p.ItemID == entityPRDt.ItemID).ToList();
                    foreach (PurchaseRequestPO purchaseRequestPO in lstPurchaseRequestPO1)
                    {
                        decimal outstandingOrder = purchaseRequestPO.OrderQuantity - purchaseRequestPO.ReceivedQuantity;
                        if (receivedQty > outstandingOrder)
                        {
                            purchaseRequestPO.ReceivedQuantity += outstandingOrder;
                            receivedQty -= outstandingOrder;
                        }
                        else
                        {
                            purchaseRequestPO.ReceivedQuantity += receivedQty;
                            receivedQty = 0;
                        }
                        purchaseRequestPODao.Update(purchaseRequestPO);
                        if (receivedQty == 0)
                            break;
                    }

                    entityPODt.ReceivedQuantity += entityPRDt.Quantity;
                    entityPODt.ReceivedInformation += "|" + purchaseReceiveID + "|";
                    entityPODt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityPODtDao.Update(entityPODt);

                    if (entityPRDt.IsControlExpired)
                    {
                        PurchaseReceiveDtExpired entityExpiredDt = new PurchaseReceiveDtExpired();
                        entityExpiredDt.ID = entityPRDt.ID;
                        entityExpiredDt.BatchNumber = temp[4];
                        entityExpiredDt.Quantity = entityPRDt.Quantity;
                        entityExpiredDt.ExpiredDate = Helper.GetDatePickerValue(temp[5]);
                        entityPurchaseReceiveDtExpiredDao.Insert(entityExpiredDt);
                    }
                }

                int count = BusinessLayer.GetPurchaseOrderDtRowCount(string.Format("PurchaseOrderID = {0} AND Quantity > ReceivedQuantity AND IsDeleted = 0", hdnOrderID.Value), ctx);
                if (count < 1)
                {
                    PurchaseOrderHd entityPOHd = entityPOHdDao.Get(Convert.ToInt32(hdnOrderID.Value));
                    entityPOHd.IsFinalPO = true;
                    entityPOHd.GCTransactionStatus = Constant.TransactionStatus.CLOSED;
                    entityPOHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityPOHdDao.Update(entityPOHd);
                }

                retval = purchaseReceiveNo;
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