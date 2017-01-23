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
    public partial class PODPQtyOnOrderCtl : BaseViewPopupCtl
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

            BindGridView();
        }

        private void BindGridView()
        {
            string filterExpression = string.Format("SiteServiceUnitID = {0} AND ItemID = {1} AND GCTransactionStatus NOT IN ('{2}','{3}') AND IsDeleted = 0", hdnSiteServiceUnitID.Value, hdnItemID.Value, Constant.TransactionStatus.CLOSED, Constant.TransactionStatus.VOID);
            List<vPurchaseOrderDt> lstEntity = BusinessLayer.GetvPurchaseOrderDtList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();

            filterExpression = string.Format("SiteServiceUnitID = {0} AND ItemID = {1} AND GCTransactionStatus NOT IN ('{2}','{3}','{4}') AND GCItemDetailStatus != '{4}'", hdnSiteServiceUnitID.Value, hdnItemID.Value, Constant.TransactionStatus.APPROVED, Constant.TransactionStatus.CLOSED, Constant.TransactionStatus.VOID);
            List<vDirectPurchaseDt> lstEntity2 = BusinessLayer.GetvDirectPurchaseDtList(filterExpression);
            grdView2.DataSource = lstEntity2;
            grdView2.DataBind();
        }

        protected void cbpEntryPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}