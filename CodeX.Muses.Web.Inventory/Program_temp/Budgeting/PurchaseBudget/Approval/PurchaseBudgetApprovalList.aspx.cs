using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using CodeX.Data.Core.Dal;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class PurchaseBudgetApprovalList : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.PURCHASE_BUDGET_APPROVAL;
        }

        public override void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowEdit = IsAllowDelete = false;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<Variable> lstVariable = new List<Variable>();
            lstVariable.Add(new Variable { Code = "0", Value = "Semua" });
            lstVariable.Add(new Variable { Code = "1", Value = "Belum Diproses" });
            lstVariable.Add(new Variable { Code = "2", Value = "Sudah Diproses" });
            Methods.SetComboBoxField<Variable>(cboViewType, lstVariable, "Value", "Code");
            cboViewType.Value = "1";

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            if (cboViewType.Value != null && cboViewType.Value.ToString() == "0")
                filterExpression = String.Format("GCTransactionStatus IN ('{0}','{1}')", Constant.TransactionStatus.APPROVED, Constant.TransactionStatus.WAIT_FOR_APPROVAL);
            else if (cboViewType.Value != null && cboViewType.Value.ToString() == "1")
                filterExpression = String.Format("GCTransactionStatus = '{0}'", Constant.TransactionStatus.WAIT_FOR_APPROVAL);
            else
                filterExpression = String.Format("GCTransactionStatus = '{0}'", Constant.TransactionStatus.APPROVED);

            int count = BusinessLayer.GetServiceUnitUserRowCount(string.Format("UserID = {0} AND IsDeleted = 0", AppSession.UserLogin.UserID));

            if (count > 0)
                filterExpression += string.Format(" AND SiteServiceUnitID IN (SELECT SiteServiceUnitID FROM ServiceUnitUser WHERE UserID = {0} AND IsDeleted = 0)", AppSession.UserLogin.UserID);
            else
            {
                count = BusinessLayer.GetLocationUserRoleRowCount(string.Format("RoleID IN (SELECT RoleID FROM UserInRole WHERE UserID = {0} AND SiteID = '{1}') AND IsDeleted = 0", AppSession.UserLogin.UserID, AppSession.UserLogin.SiteID));
                if (count > 0)
                    filterExpression += string.Format(" AND SiteServiceUnitID IN (SELECT SiteServiceUnitID FROM ServiceUnitUserRole WHERE RoleID IN (SELECT RoleID FROM UserInRole WHERE UserID = {0} AND SiteID = '{1}') AND IsDeleted = 0)", AppSession.UserLogin.UserID, AppSession.UserLogin.SiteID);
            }

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvPurchaseBudgetHdRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vPurchaseBudgetHd> lstEntity = BusinessLayer.GetvPurchaseBudgetHdList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "TransactionID ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vPurchaseBudgetHd entity = (vPurchaseBudgetHd)e.Row.DataItem;
                CheckBox chkIsSelected = (CheckBox)e.Row.FindControl("chkIsSelected");
                if (entity.GCTransactionStatus != Constant.TransactionStatus.WAIT_FOR_APPROVAL)
                    chkIsSelected.Style.Add("display", "none");
            }
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
            if (type == "approve")
            {
                bool result = true;
                IDbContext ctx = DbFactory.Configure(true);
                PurchaseBudgetHdDao itemHdDao = new PurchaseBudgetHdDao(ctx);
                PurchaseBudgetDtDao itemDtDao = new PurchaseBudgetDtDao(ctx);
                try
                {
                    string filterExpressionItemRequestHd = String.Format("TransactionID IN ({0})", hdnParam.Value);
                    List<PurchaseBudgetHd> lstItemRequestHd = BusinessLayer.GetPurchaseBudgetHdList(filterExpressionItemRequestHd, ctx);
                    foreach (PurchaseBudgetHd itemHd in lstItemRequestHd)
                    {
                        itemHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                        itemHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                        itemHdDao.Update(itemHd);
                    }

                    string filterExpressionItemRequestDt = String.Format("TransactionID IN ({0}) AND GCItemDetailStatus != '{1}'", hdnParam.Value, Constant.TransactionStatus.VOID);
                    List<PurchaseBudgetDt> lstItemRequestDt = BusinessLayer.GetPurchaseBudgetDtList(filterExpressionItemRequestDt, ctx);
                    foreach (PurchaseBudgetDt itemDt in lstItemRequestDt)
                    {
                        itemDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
                        itemDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        itemDtDao.Update(itemDt);
                    }

                    ctx.CommitTransaction();
                }
                catch (Exception ex)
                {
                    Helper.InsertErrorLog(ex);
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
            else
            {
                bool result = true;
                IDbContext ctx = DbFactory.Configure(true);
                PurchaseBudgetHdDao itemHdDao = new PurchaseBudgetHdDao(ctx);
                PurchaseBudgetDtDao itemDtDao = new PurchaseBudgetDtDao(ctx);
                try
                {
                    string filterExpressionItemRequestHd = String.Format("TransactionID IN ({0})", hdnParam.Value);
                    List<PurchaseBudgetHd> lstItemRequestHd = BusinessLayer.GetPurchaseBudgetHdList(filterExpressionItemRequestHd, ctx);
                    foreach (PurchaseBudgetHd itemHd in lstItemRequestHd)
                    {
                        itemHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                        itemHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                        itemHdDao.Update(itemHd);
                    }

                    string filterExpressionItemRequestDt = String.Format("TransactionID IN ({0}) AND GCItemDetailStatus != '{1}'", hdnParam.Value, Constant.TransactionStatus.VOID);
                    List<PurchaseBudgetDt> lstItemRequestDt = BusinessLayer.GetPurchaseBudgetDtList(filterExpressionItemRequestDt, ctx);
                    foreach (PurchaseBudgetDt itemDt in lstItemRequestDt)
                    {
                        itemDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                        itemDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        itemDtDao.Update(itemDt);
                    }

                    ctx.CommitTransaction();
                }
                catch (Exception ex)
                {
                    Helper.InsertErrorLog(ex);
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
}