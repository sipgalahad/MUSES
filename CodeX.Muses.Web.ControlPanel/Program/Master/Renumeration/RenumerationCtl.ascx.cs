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
using CodeX.Muses.Web.Information.Program;

namespace CodeX.Muses.Web.Information.Program
{
    public partial class RenumerationCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int CurrPage = 1;

        //private StockDetailInfo DetailPage
        //{
        //    get { return (StockDetailInfo)Page; }
        //}

        public override void InitializeDataControl(string param)
        {
            //String[] lstParam = param.Split('|');
            //hdnItemID.Value = lstParam[0];

            //ItemMaster im = BusinessLayer.GetItemMaster(Convert.ToInt32(hdnItemID.Value));
            //txtItemName.Text = string.Format("{0} - {1}", im.ItemCode, im.ItemName1);

            RenumerationHd entityHd = BusinessLayer.GetRenumerationHd(Convert.ToInt32(param));
            txtHeader.Text = String.Format("{0} - {1}", entityHd.RenumerationName, entityHd.RenumerationCode);
            hdnID.Value = entityHd.CurrentTransactionID.ToString();
            BindGridView();
        }

        private void BindGridView()
        {
            //string filterExpression = DetailPage.OnGetFilterExpression();
            //filterExpression += string.Format(" AND ItemID = {0}", hdnItemID.Value);
            //if (isCountPageCount)
            //{
            //    int rowCount = BusinessLayer.GetvItemMovementRowCount(filterExpression);
            //    pageCount = Helper.GetPageCount(rowCount, 10);
            //}

            //List<vItemMovement> lstDistributionDt = BusinessLayer.GetvItemMovementList(filterExpression, 10, pageIndex);

            grdPopupView.DataSource = BusinessLayer.GetvTransRenumerationDtList(String.Format("TransactionID = {0} AND IsDeleted = 0",Convert.ToInt32(hdnID.Value)));
            grdPopupView.DataBind();
        }

        protected void cbpPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView();
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView();
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
    }
}