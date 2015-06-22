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
    public partial class PurchaseOrderApprovalDetail : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        private string[] lstSelectedMember = null;

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.PURCHASE_ORDER_APPROVAL;
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
            vPurchaseOrderHd entityItemRequest = BusinessLayer.GetvPurchaseOrderHdList(String.Format("PurchaseOrderID = '{0}'", Convert.ToInt32(hdnOrderID.Value)))[0];
            EntityToControl(entityItemRequest);
        }

        private void EntityToControl(vPurchaseOrderHd entity)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                isShowWatermark = true;
                //watermarkText = entity.TransactionStatusWatermark;
            }
            hdnOrderID.Value = entity.PurchaseOrderID.ToString();
            txtOrderNo.Text = entity.PurchaseOrderNo;
            txtItemOrderDate.Text = entity.OrderDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtSupplierName.Text = entity.BusinessPartnerName;
            txtNotes.Text = entity.Remarks;
            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vPurchaseOrderDt entity = e.Row.DataItem as vPurchaseOrderDt;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (entity.GCItemDetailStatus == Constant.TransactionStatus.APPROVED || lstSelectedMember.Contains(entity.ID.ToString()))
                    chkIsSelected.Checked = true;
            }
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

            lstSelectedMember = hdnSelectedMember.Value.Split(',');
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

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseOrderDtDao purchaseDtDao = new PurchaseOrderDtDao(ctx);
            try
            {
                string filterExpressionSetDefaultDt = String.Format("PurchaseOrderID = {0}", hdnOrderID.Value);
                List<PurchaseOrderDt> lstPurchaseOrderDtSetDefault = BusinessLayer.GetPurchaseOrderDtList(filterExpressionSetDefaultDt, ctx);
                foreach (PurchaseOrderDt purchaseDt in lstPurchaseOrderDtSetDefault)
                {
                    purchaseDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                    purchaseDtDao.Update(purchaseDt);
                }

                string filterExpressionPurchaseOrderDt = String.Format("ID IN ({0})", hdnSelectedMember.Value.Substring(1));
                List<PurchaseOrderDt> lstPurchaseOrderDt = BusinessLayer.GetPurchaseOrderDtList(filterExpressionPurchaseOrderDt, ctx);
                foreach (PurchaseOrderDt purchaseDt in lstPurchaseOrderDt)
                {
                    purchaseDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
                    purchaseDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    purchaseDtDao.Update(purchaseDt);
                }
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