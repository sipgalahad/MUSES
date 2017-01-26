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
    public partial class ConsignmentReceiveDetailCtl : BaseEntryPopupCtl
    {
        protected string OnGetFilterExpressionPurchaseOrder()
        {
            return string.Format("BusinessPartnerID = '{0}' AND GCTransactionStatus = '{1}'", hdnSupplierID.Value, Constant.TransactionStatus.APPROVED);
        }
        public override void InitializeDataControl(string param)
        {
            hdnSupplierID.Value = param;
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
                HtmlGenericControl lblPurchaseUnit = e.Item.FindControl("lblPurchaseUnit") as HtmlGenericControl;
                txtReceivedItem.Text = (entity.Quantity - entity.ReceivedQuantity).ToString();
                txtUnitPrice.Text = entity.UnitPrice.ToString();
                txtDiscountPercentage1.Text = entity.DiscountPercentage1.ToString();
                txtDiscountAmount1.Text = entity.DiscountAmount1.ToString();
                txtDiscountPercentage2.Text = entity.DiscountPercentage2.ToString();
                txtDiscountAmount2.Text = entity.DiscountAmount2.ToString();
                lblPurchaseUnit.InnerHtml = entity.PurchaseUnit;
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
            PurchaseReceiveDtExpiredDao entityPurchaseReceiveDtExpiredDao = new PurchaseReceiveDtExpiredDao(ctx);
            try
            {
                List<PurchaseOrderDt> lstPODt = new List<PurchaseOrderDt>();                
                int purchaseReceiveID = 0;
                string purchaseReceiveNo = "";
                ((PurchaseReceiveEntry)Page).SavePurchaseReceiveHd(ctx, ref purchaseReceiveID, ref purchaseReceiveNo);

                List<PurchaseOrderDt> lstPurchaseOrderDt = BusinessLayer.GetPurchaseOrderDtList(string.Format("ID IN ({0})", hdnLstPurchaseOrderDtID.Value), ctx);
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
                    entityPRDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                    entityPRDt.LineAmount = (entityPRDt.Quantity * entityPRDt.UnitPrice) - entityPRDt.DiscountAmount1 - entityPRDt.DiscountAmount2;
                    entityPODt.ReceivedQuantity += entityPRDt.Quantity;
                    entityPRDt.CreatedBy = AppSession.UserLogin.UserID;
                    entityPurchaseReceiveDtDao.Insert(entityPRDt);
                    Int32 ID = BusinessLayer.GetPurchaseReceiveDtMaxID(ctx);

                    PurchaseReceiveDtExpired entityExpiredDt = new PurchaseReceiveDtExpired();
                    entityExpiredDt.ID = ID;
                    entityExpiredDt.BatchNumber = temp[4];
                    entityExpiredDt.Quantity = entityPRDt.Quantity;
                    entityExpiredDt.ExpiredDate = Helper.GetDatePickerValue(temp[5]);
                    entityPurchaseReceiveDtExpiredDao.Insert(entityExpiredDt);
                }

                foreach (PurchaseOrderDt entityDt in lstPurchaseOrderDt)
                {
                    entityDt.ReceivedInformation += "|" + purchaseReceiveID + "|";
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityPODtDao.Update(entityDt);
                }

                int count = BusinessLayer.GetPurchaseOrderDtRowCount(string.Format("PurchaseOrderID = {0} AND Quantity > ReceivedQuantity AND IsDeleted = 0", hdnOrderID.Value), ctx);
                if (count < 1)
                {
                    PurchaseOrderHd entityPOHd = entityPOHdDao.Get(Convert.ToInt32(hdnOrderID.Value));
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