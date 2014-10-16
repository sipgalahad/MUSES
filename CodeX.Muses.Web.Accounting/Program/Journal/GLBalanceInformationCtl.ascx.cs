using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common;
using CodeX.Data.Core.Dal;
using System.Data;
using CodeX.Common;

namespace CodeX.Web.Accounting.Program
{
    public partial class GLBalanceInformationCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        public override void InitializeDataControl(string param)
        {
            List<String> lstParam = param.Split('|').ToList();
            hdnGLAccountID.Value = lstParam[0];
            ChartOfAccount coa = BusinessLayer.GetChartOfAccount(Convert.ToInt32(hdnGLAccountID.Value));
            
            txtGLAccountName.Text = coa.GLAccountName;
            txtGLAccountNo.Text = coa.GLAccountNo;
            hdnYear.Value = lstParam[1];
            hdnMonth.Value = lstParam[2];

            RowCountPerPage = Constant.GridViewPageSize.GRID_POPUP;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            List<GetGLBalancePerGLAccount> lstEntity = null;
            
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetGLBalancePerGLAccountRowCount(Convert.ToInt32(hdnGLAccountID.Value), Convert.ToInt32(hdnYear.Value), Convert.ToInt32(hdnMonth.Value));
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            lstEntity = BusinessLayer.GetGLBalancePerGLAccountList(Convert.ToInt32(hdnGLAccountID.Value), Convert.ToInt32(hdnYear.Value), Convert.ToInt32(hdnMonth.Value), pageIndex, Constant.GridViewPageSize.GRID_MASTER);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
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