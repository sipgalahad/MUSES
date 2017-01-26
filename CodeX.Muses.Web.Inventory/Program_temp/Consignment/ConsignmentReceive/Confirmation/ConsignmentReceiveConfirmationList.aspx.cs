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
    public partial class ConsignmentReceiveConfirmationList : BasePageList
    {
        protected int PageCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.CONSIGNMENT_RECEIVE_CONFIRMED;
        }

        public override void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowEdit = IsAllowDelete = false;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            BindGridView(1, true, ref PageCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += String.Format("TransactionCode = '{0}' AND GCTransactionStatus = '{1}' AND PurchaseReceiveID IN (SELECT PurchaseReceiveID From PurchaseReceiveDt WHERE PurchaseOrderID IN (SELECT PurchaseOrderID FROM vPurchaseOrderDt WHERE ReceivedQuantity < Quantity))", Constant.TransactionCode.CONSIGNMENT_RECEIVE, Constant.TransactionStatus.OPEN);

            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvPurchaseReceiveHdRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vPurchaseReceiveHd> lstEntity = BusinessLayer.GetvPurchaseReceiveHdList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ReceivedDate DESC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        //protected override bool OnCustomButtonClick(string type, ref string errMessage)
        //{
        //    bool result = true;
        //    IDbContext ctx = DbFactory.Configure(true);
        //    PurchaseReceiveHdDao purchaseHdDao = new PurchaseReceiveHdDao(ctx);
        //    PurchaseReceiveDtDao purchaseDtDao = new PurchaseReceiveDtDao(ctx);
        //    try
        //    {
        //        //string filterExpressionPurchaseReceiveHd = String.Format("PurchaseReceiveID IN ({0})", hdnParam.Value);

        //        //List<PurchaseReceiveHd> lstPurchaseReceiveHd = BusinessLayer.GetPurchaseReceiveHdList(filterExpressionPurchaseReceiveHd);

        //        //foreach (PurchaseReceiveHd purchaseHd in lstPurchaseReceiveHd)
        //        //{
        //        //    purchaseHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
        //        //    purchaseHd.LastUpdatedBy = AppSession.UserLogin.UserID;
        //        //    purchaseHdDao.Update(purchaseHd);
        //        //}

        //        //List<PurchaseReceiveDt> lstPurchaseReceiveDt = BusinessLayer.GetPurchaseReceiveDtList(filterExpressionPurchaseReceiveHd);
        //        //foreach (PurchaseReceiveDt purchaseDt in lstPurchaseReceiveDt)
        //        //{
        //        //    purchaseDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
        //        //    purchaseDt.LastUpdatedBy = AppSession.UserLogin.UserID;
        //        //    purchaseDtDao.Update(purchaseDt);
        //        //}

        //        //ctx.CommitTransaction();
        //    }
        //    catch (Exception ex)
        //    {
        //        errMessage = ex.Message;
        //        result = false;
        //        ctx.RollBackTransaction();
        //    }
        //    finally
        //    {
        //        ctx.Close();
        //    }
        //    return result;
        //}
    }
}