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
    public partial class DirectPurchasePRDtCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        public override void InitializeDataControl(string param)
        {
            vDirectPurchaseDt entity = BusinessLayer.GetvDirectPurchaseDtList(string.Format("ID = {0}", param)).FirstOrDefault();
            txtItem.Text = string.Format("{0}", entity.ItemName1);

            hdnDirectPurchaseID.Value = entity.DirectPurchaseID.ToString();
            hdnItemID.Value = entity.ItemID.ToString();
            hdnItemName1.Value = entity.ItemName1;

            RowCountPerPage = Constant.GridViewPageSize.GRID_POPUP;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = string.Format("DirectPurchaseID = {0} AND ItemID = {1} AND ItemName1 = '{2}'", hdnDirectPurchaseID.Value, hdnItemID.Value, hdnItemName1.Value);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvPurchaseRequestDPRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_POPUP);
            }

            List<vPurchaseRequestDP> lstEntity = BusinessLayer.GetvPurchaseRequestDPList(filterExpression, Constant.GridViewPageSize.GRID_POPUP, pageIndex, "PurchaseRequestNo DESC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpEntryPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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
    }
}