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

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class PurchaseBudgetDtCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');
            hdnSiteServiceUnitID.Value = temp[0];
            hdnItemID.Value = temp[1];

            vSiteServiceUnit siteServiceUnit = BusinessLayer.GetvSiteServiceUnitList(string.Format("SiteServiceUnitID = {0}", temp[0])).FirstOrDefault();
            txtServiceUnit.Text = string.Format("{0} ({1})", siteServiceUnit.ServiceUnitName, siteServiceUnit.ServiceUnitCode);

            ItemMaster item = BusinessLayer.GetItemMaster(Convert.ToInt32(temp[1]));
            txtItem.Text = string.Format("{0} ({1})", item.ItemName1, item.ItemCode);

            vPurchaseBudgetDt entityBudget = BusinessLayer.GetvPurchaseBudgetDtList(string.Format("SiteServiceUnitID = {0} AND YearPeriod = {1} AND GCTransactionStatus = '{2}' AND GCItemDetailStatus != '{3}' AND ItemID = {4}", hdnSiteServiceUnitID.Value, DateTime.Now.Year, Constant.TransactionStatus.APPROVED, Constant.TransactionStatus.VOID, hdnItemID.Value)).FirstOrDefault();

            string filterExpression = string.Format("ToSiteServiceUnitID = {0} AND ItemID = {1} AND YEAR(OrderDate) = {2} AND GCTransactionStatus NOT IN ('{3}') AND IsDeleted = 0", hdnSiteServiceUnitID.Value, hdnItemID.Value, DateTime.Now.Year, Constant.TransactionStatus.VOID);
            List<vPurchaseOrderDt> lstEntity = BusinessLayer.GetvPurchaseOrderDtList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();

            filterExpression = string.Format("ToSiteServiceUnitID = {0} AND ItemID = {1} AND YEAR(PurchaseDate) = {2} AND GCTransactionStatus NOT IN ('{3}') AND GCItemDetailStatus != '{3}'", hdnSiteServiceUnitID.Value, hdnItemID.Value, DateTime.Now.Year, Constant.TransactionStatus.VOID);
            List<vDirectPurchaseDt> lstEntity2 = BusinessLayer.GetvDirectPurchaseDtList(filterExpression);
            grdView2.DataSource = lstEntity2;
            grdView2.DataBind();

            if (entityBudget != null)
            {
                txtQty.Text = entityBudget.Quantity.ToString();
                txtTotalAmount.Text = entityBudget.TotalAmount.ToString("N");

                decimal qtyUsed = lstEntity.Sum(p => p.Quantity) + lstEntity2.Sum(p => p.Quantity);
                decimal totalAmountUsed = lstEntity.Sum(p => p.LineAmount) + lstEntity2.Sum(p => p.LineAmount);

                txtQtyUsed.Text = qtyUsed.ToString();
                txtTotalAmountUsed.Text = totalAmountUsed.ToString("N");
                txtQtyRemaining.Text = (entityBudget.Quantity - qtyUsed).ToString();
                txtTotalAmountRemaining.Text = (entityBudget.TotalAmount - totalAmountUsed).ToString("N");
            }
        }

        protected void cbpEntryPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
        }
    }
}