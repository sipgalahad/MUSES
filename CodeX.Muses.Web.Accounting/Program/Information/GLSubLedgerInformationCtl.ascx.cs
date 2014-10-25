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
    public partial class GLSubLedgerInformationCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;

        public override void InitializeDataControl(string param)
        {
            List<String> lstParam = param.Split('|').ToList();
            hdnGLAccountID.Value = lstParam[0];
            hdnSubledger.Value = lstParam[1];
            hdnYear.Value = lstParam[2];
            hdnMonth.Value = lstParam[3];
            txtGLAccountNo.Text = lstParam[4];
            txtGLAccountName.Text = lstParam[5];
            BindGridView(1, true, ref PageCount);
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref PageCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref PageCount);
                    result = "refresh|" + PageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            List<GetGLBalanceDtPerSubLedger> lstEntity = null;
            
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetGLBalanceDtPerSubLedgerRowCount(Convert.ToInt32(hdnGLAccountID.Value), Convert.ToInt32(hdnSubledger.Value), Convert.ToInt32(hdnYear.Value), Convert.ToInt32(hdnMonth.Value));
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            lstEntity = BusinessLayer.GetGLBalanceDtPerSubLedgerList(Convert.ToInt32(hdnGLAccountID.Value), Convert.ToInt32(hdnSubledger.Value), Convert.ToInt32(hdnYear.Value), Convert.ToInt32(hdnMonth.Value), pageCount, Constant.GridViewPageSize.GRID_MASTER);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
    }
}