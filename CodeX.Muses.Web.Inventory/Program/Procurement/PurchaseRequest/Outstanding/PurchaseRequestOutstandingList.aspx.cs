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
    public partial class PurchaseRequestOutstandingList : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.APPROVED_PURCHASE_REQUEST;
        }

        public override void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowEdit = IsAllowDelete = false;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<GetServiceUnitUserList> lstUserServiceUnit = BusinessLayer.GetServiceUnitUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, string.Format("SiteServiceUnitID IN (SELECT SiteServiceUnitID FROM vSiteServiceUnit WHERE IsAllowPurchase = 1)"));
            if (lstUserServiceUnit.Count > 0)
                hdnListSiteServiceUnitID.Value = string.Join(",", lstUserServiceUnit.Select(p => p.SiteServiceUnitID).ToList());
            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            if (hdnListSiteServiceUnitID.Value != "")
                filterExpression += String.Format("ToSiteServiceUnitID IN ({0}) AND GCTransactionStatus = '{1}'", hdnListSiteServiceUnitID.Value, Constant.TransactionStatus.APPROVED);
            else
                filterExpression += "1 = 0";

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvPurchaseRequestHdRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vPurchaseRequestHd> lstEntity = BusinessLayer.GetvPurchaseRequestHdList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex,"TransactionDate DESC");
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