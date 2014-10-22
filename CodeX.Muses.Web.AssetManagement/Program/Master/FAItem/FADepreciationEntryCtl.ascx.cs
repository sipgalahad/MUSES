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
        protected int CurrPage = 1;
        public override void InitializeDataControl(string param)
        {
            hdnFixedAssetID.Value = param;
            BindGridView(CurrPage, true, ref PageCount);
        }

        protected void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        { 
            String filterExpression = String.Format("FixedAssetID = {0}",hdnFixedAssetID.Value);
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetFADepreciationRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<FADepreciation> entity = BusinessLayer.GetFADepreciationList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
            grdPopupView.DataSource = entity;
            grdPopupView.DataBind();
        }

        protected void cbpPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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