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
    public partial class PurchaseOrderOutstandingDtCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected string GetVATPercentageLabel()
        {
            return hdnVATPercentage.Value;
        }

        public override void InitializeDataControl(string param)
        {
            hdnOrderID.Value = param;
            hdnVATPercentage.Value = BusinessLayer.GetSettingParameter(Constant.SettingParameter.VAT_PERCENTAGE).ParameterValue;
            vPurchaseOrderHd entityItemRequest = BusinessLayer.GetvPurchaseOrderHdList(String.Format("PurchaseOrderID = '{0}'", Convert.ToInt32(hdnOrderID.Value)))[0];
            EntityToControl(entityItemRequest);
        }

        private void EntityToControl(vPurchaseOrderHd entity)
        {
            hdnOrderID.Value = entity.PurchaseOrderID.ToString();
            txtOrderNo.Text = entity.PurchaseOrderNo;
            txtItemOrderDate.Text = entity.OrderDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtSupplierName.Text = entity.BusinessPartnerName;
            txtServiceUnitName.Text = entity.ServiceUnitName;
            txtToServiceUnitName.Text = entity.ToServiceUnitName;
            txtNotes.Text = entity.Remarks;
            txtExpiredDate.Text = entity.POExpiredDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtPurchaseOrderType.Text = entity.PurchaseOrderType;
            txtTermCondition.Text = entity.TermName;
            txtFrancoRegion.Text = entity.FrancoRegion;
            txtCurrencyCode.Text = entity.CurrencyCode;
            txtCurrencyRate.Text = entity.CurrencyRate.ToString();
            txtDeliveryDate.Text = entity.DeliveryDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtTransactionAmount.Text = entity.TransactionAmount.ToString();
            txtFinalDiscountPercentage.Text = entity.FinalDiscountPercentage.ToString();
            txtFinalDiscountAmount.Text = entity.FinalDiscountAmount.ToString();
            txtPPN.Text = entity.VATAmount.ToString();
            chkPPN.Checked = entity.IsIncludeVAT;
            txtDP.Text = entity.DownPaymentAmount.ToString();
            txtTotalNetTransactionAmount.Text = entity.TotalNetTransactionAmount.ToString();
            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }


        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnOrderID.Value != "")
                filterExpression = string.Format("PurchaseOrderID = {0} AND IsDeleted = 0", hdnOrderID.Value);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvPurchaseOrderDtOutstandingInfoRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vPurchaseOrderDtOutstandingInfo> lstEntity = BusinessLayer.GetvPurchaseOrderDtOutstandingInfoList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
            grdViewPopup.DataSource = lstEntity;
            grdViewPopup.DataBind();
        }

        protected void grdViewPopup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vPurchaseOrderDtOutstandingInfo entity = e.Row.DataItem as vPurchaseOrderDtOutstandingInfo;
                if (entity.ReceivedQuantity != entity.Quantity)
                    e.Row.CssClass = "trOutstanding";
            }
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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