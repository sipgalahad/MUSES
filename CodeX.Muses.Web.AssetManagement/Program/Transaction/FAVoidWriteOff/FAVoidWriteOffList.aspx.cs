using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Data.Model;
using CodeX.Web.Common;
using CodeX.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace Codex.Muses.Web.AssetManagement.Program
{
    public partial class FAVoidWriteOffList : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.AssetManagement.FA_VOID_WRITE_OFF;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        public override void SetFilterParameter(ref string[] fieldListText, ref string[] fieldListValue)
        {
            fieldListText = new string[] { "Code", "Name" };
            fieldListValue = new string[] { "FixedAssetCode", "FixedAssetName" };
        }

        private string GetFilterExpression()
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += String.Format("GCItemStatus = '{0}' AND IsDeleted = 0", Constant.ItemStatus.IN_ACTIVE);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvFAItemRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vFAItem> lstEntity = BusinessLayer.GetvFAItemList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
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

        protected override bool OnDeleteRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            FAItemDtDao faItemDao = new FAItemDtDao(ctx);
            FAWriteOffDao faWriteOffDao = new FAWriteOffDao(ctx);

            try
            {
                if (hdnID.Value.ToString() != "")
                {
                    FAItemDt entity = faItemDao.Get(Convert.ToInt32(hdnID.Value));
                    entity.GCItemStatus = Constant.ItemStatus.ACTIVE;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    faItemDao.Update(entity);

                    string filterExpression = String.Format("FixedAssetDtID = {0} AND GCTransactionStatus = '{1}'", entity.FixedAssetDtID, Constant.TransactionStatus.APPROVED);
                    FAWriteOff faWriteOff = BusinessLayer.GetFAWriteOffList(filterExpression,ctx)[0];
                    faWriteOff.GCTransactionStatus = Constant.TransactionStatus.VOID;
                    faWriteOffDao.Update(faWriteOff);

                    ctx.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
    }
}