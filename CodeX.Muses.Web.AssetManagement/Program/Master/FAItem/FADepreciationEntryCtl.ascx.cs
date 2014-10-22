using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace Codex.Muses.Web.AssetManagement.Program
{
    public partial class FADepreciationEntryCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        public override void InitializeDataControl(string param)
        {
            hdnFixedAssetID.Value = param;
            RowCountPerPage = Constant.GridViewPageSize.GRID_POPUP;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        protected void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        { 
            String filterExpression = String.Format("FixedAssetID = {0}",hdnFixedAssetID.Value);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetFADepreciationRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_POPUP);
            }

            List<FADepreciation> entity = BusinessLayer.GetFADepreciationList(filterExpression, Constant.GridViewPageSize.GRID_POPUP, pageIndex);
            grdPopupView.DataSource = entity;
            grdPopupView.DataBind();
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

        protected void cbpPopupProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e) 
        {
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                if (e.Parameter == "process")
                {
                    ProsesFADepreciation(ref result);
                }
                else
                {
                    //TODO Batal Proses Jurnal Penyusutan
                    //DeleteFADepreciation(ref result);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool ProsesFADepreciation(ref String errMessage) 
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure();
            FADepreciationDao entityDao = new FADepreciationDao(ctx);
            try
            {
                BusinessLayer.GenerateFADepreciation(Convert.ToInt32(hdnFixedAssetID.Value), AppSession.UserLogin.UserID, ctx);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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

        private bool DeleteFADepreciation(ref String errMessage) 
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure();
            FADepreciationDao entityDao = new FADepreciationDao(ctx);
            try
            {
                String filterExpression = String.Format("FixedAssetID = {0} AND GLJurnalID IS NOT NULL", hdnFixedAssetID.Value);
                //entityDao.Update()
                //foreach (int id in lstDepreciationID) entityDao.Delete(id);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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