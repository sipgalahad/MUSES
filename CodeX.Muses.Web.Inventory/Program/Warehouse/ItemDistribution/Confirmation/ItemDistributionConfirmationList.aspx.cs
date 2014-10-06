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
    public partial class ItemDistributionConfirmationList : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.ITEM_DISTRIBUTION_CONFIRMED;
        }

        #region Html Getter
        protected string OnGetFilterExpressionToLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.ITEM_REQUEST);
        }
        #endregion

        public override void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowEdit = IsAllowDelete = false;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += String.Format("GCDistributionStatus = '{0}' AND ToLocationID = {1}", Constant.DistributionStatus.ON_DELIVERY, hdnLocationIDFrom.Value);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvItemDistributionHdRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vItemDistributionHd> lstEntity = BusinessLayer.GetvItemDistributionHdList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "DeliveryDate DESC");
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
            if (type == "approve")
            {
                bool result = true;
                IDbContext ctx = DbFactory.Configure(true);
                ItemDistributionHdDao distributionHdDao = new ItemDistributionHdDao(ctx);
                ItemDistributionDtDao distributionDtDao = new ItemDistributionDtDao(ctx);
                try
                {
                    string filterExpressionDistributionHd = String.Format("DistributionID IN ({0})", hdnParam.Value);

                    List<ItemDistributionHd> lstItemDistributionHd = BusinessLayer.GetItemDistributionHdList(filterExpressionDistributionHd);

                    foreach (ItemDistributionHd distributionHd in lstItemDistributionHd)
                    {
                        distributionHd.GCDistributionStatus = Constant.DistributionStatus.RECEIVED;
                        distributionHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                        distributionHdDao.Update(distributionHd);
                    }

                    List<ItemDistributionDt> lstItemDistributionDt = BusinessLayer.GetItemDistributionDtList(filterExpressionDistributionHd);
                    foreach (ItemDistributionDt distributionDt in lstItemDistributionDt)
                    {
                        distributionDt.GCItemDetailStatus = Constant.DistributionStatus.RECEIVED;
                        distributionDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        distributionDtDao.Update(distributionDt);
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
            else
            {
                bool result = true;
                IDbContext ctx = DbFactory.Configure(true);
                ItemDistributionHdDao distributionHdDao = new ItemDistributionHdDao(ctx);
                ItemDistributionDtDao distributionDtDao = new ItemDistributionDtDao(ctx);
                try
                {
                    string filterExpressionDistributionHd = String.Format("DistributionID IN ({0})", hdnParam.Value);
                    List<ItemDistributionHd> lstItemDistributionHd = BusinessLayer.GetItemDistributionHdList(filterExpressionDistributionHd);

                    foreach (ItemDistributionHd distributionHd in lstItemDistributionHd)
                    {
                        distributionHd.GCDistributionStatus = Constant.DistributionStatus.OPEN;
                        distributionHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                        distributionHdDao.Update(distributionHd);
                    }

                    List<ItemDistributionDt> lstItemDistributionDt = BusinessLayer.GetItemDistributionDtList(filterExpressionDistributionHd);
                    foreach (ItemDistributionDt distributionDt in lstItemDistributionDt)
                    {
                        distributionDt.GCItemDetailStatus = Constant.DistributionStatus.OPEN;
                        distributionDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        distributionDtDao.Update(distributionDt);
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
}