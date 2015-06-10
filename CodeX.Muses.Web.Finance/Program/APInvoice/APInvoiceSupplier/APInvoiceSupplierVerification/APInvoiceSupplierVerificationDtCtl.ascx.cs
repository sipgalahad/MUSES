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

namespace CodeX.Muses.Web.Finance.Program
{
    public partial class APInvoiceSupplierVerificationDtCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        public override void InitializeDataControl(string param)
        {
            hdnPurchaseInvoiceID.Value = param;

            PurchaseInvoiceHd entity = BusinessLayer.GetPurchaseInvoiceHd(Convert.ToInt32(hdnPurchaseInvoiceID.Value));
            txtPurchaseInvoiceNo.Text = entity.PurchaseInvoiceNo;
            txtTotalAmount.Text = entity.TotalTransactionAmount.ToString("N");
            txtGrandTotalPI.Text = entity.TotalNetTransactionAmount.ToString("N");
            txtChargesPI.Text = entity.ChargesAmount.ToString("N");
            txtFinalDiscountPIPctg.Text = entity.FinalDiscount.ToString("N");

            decimal finalDiscount = (entity.TotalTransactionAmount * entity.FinalDiscount) / 100;
            decimal totalAfterDisc = entity.TotalTransactionAmount - finalDiscount;
            txtFinalDIscountPI.Text = finalDiscount.ToString("N");
            txtStampPI.Text = entity.StampAmount.ToString("N");
            txtPPHPIPctg.Text = entity.PPHPercentage.ToString();
            txtPPHPI.Text = (finalDiscount * entity.PPHPercentage / 100).ToString("N");
            txtPPNPI.Text = (finalDiscount * entity.VATPercentage / 100).ToString("N");
            chkPPN.Checked = entity.VATPercentage > 0;

            RowCountPerPage = Constant.GridViewPageSize.GRID_POPUP;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = string.Format("PurchaseInvoiceID = {0} AND IsDeleted = 0", hdnPurchaseInvoiceID.Value);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvPurchaseInvoiceDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 10);
            }

            List<vPurchaseInvoiceDt> lstDistributionDt = BusinessLayer.GetvPurchaseInvoiceDtList(filterExpression, 10, pageIndex);
            lvwView.DataSource = lstDistributionDt;
            lvwView.DataBind();
        }

        protected void cbpPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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