using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class PurchaseOrderOutstandingDetail : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.PURCHASE_ORDER;
        }
        protected string GetVATPercentageLabel()
        {
            return hdnVATPercentage.Value;
        }

        public override void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowEdit = IsAllowDelete = false;
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowAdd = false;
        }

        protected override void InitializeDataControl()
        {
            hdnOrderID.Value = Page.Request.QueryString["id"];
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
            txtNotes.Text = entity.Remarks;
            txtExpiredDate.Text = entity.POExpiredDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtPurchaseOrderType.Text = entity.PurchaseOrderType;
            txtTermCondition.Text = entity.TermName;
            txtFrancoRegion.Text = entity.FrancoRegion;
            txtCurrencyCode.Text = entity.CurrencyCode;
            txtCurrencyRate.Text = entity.CurrencyRate.ToString();
            txtDeliveryDate.Text = entity.DeliveryDateInString;
            txtTransactionAmount.Text = entity.TransactionAmount.ToString("N");
            txtFinalDiscountPercentage.Text = entity.FinalDiscountPercentage.ToString("N");
            txtFinalDiscountAmount.Text = entity.FinalDiscountAmount.ToString("N");
            txtPPN.Text = entity.VATAmount.ToString("N");
            chkPPN.Checked = entity.IsIncludeVAT;
            txtDP.Text = entity.DownPaymentAmount.ToString("N");
            txtTotalNetTransactionAmount.Text = entity.TotalNetTransactionAmount.ToString("N");
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
                rowCount = BusinessLayer.GetvPurchaseOrderDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vPurchaseOrderDt> lstEntity = BusinessLayer.GetvPurchaseOrderDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
            grdView.DataSource = lstEntity;
            grdView.DataBind();

        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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