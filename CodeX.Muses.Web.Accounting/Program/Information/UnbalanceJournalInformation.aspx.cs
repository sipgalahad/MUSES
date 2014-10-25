using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Utils;
using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxEditors;
using CodeX.Web.Common.UI;
using CodeX.Data.Core.Dal;
using CodeX.Web.Common;
using CodeX.Data.Model;
using CodeX.Common;
using System.Web.UI.HtmlControls;

namespace Codex.Ronin.Web.Accounting.Program
{
    public partial class UnbalanceJournalInformation : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Accounting.UNBALANCE_JOURNAL;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            String filterExpressionTransactionType = String.Format("TransactionCode LIKE '{0}'",Constant.TransactionCode.JOURNAL);
            List<TransactionType> lstTransactionType = BusinessLayer.GetTransactionTypeList(filterExpressionTransactionType);
            Methods.SetComboBoxField(cboDataSource, lstTransactionType, "TransactionName", "TransactionCode");
            
            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(1, true, ref PageCount, ref RowCount);
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

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (cboDataSource.Value != null) 
            {
                filterExpression = String.Format("GCTransactionStatus != '{0}' AND TransactionCode = '{1}' AND (DebitAmount - CreditAmount) != 0", Constant.TransactionStatus.VOID, cboDataSource.Value);
            }

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvGLTransactionHdRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vGLTransactionHd> lstEntity = BusinessLayer.GetvGLTransactionHdList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "JournalNo");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        public override Control OnGetExportControl()
        {
            string filterExpression = "1 = 0";
            if (cboDataSource.Value != null)
            {
                filterExpression = String.Format("GCTransactionStatus != '{0}' AND TransactionCode = '{1}' AND (DebitAmount - CreditAmount) != 0", Constant.TransactionStatus.VOID, cboDataSource.Value);
            }
            List<vGLTransactionHd> lstEntity = BusinessLayer.GetvGLTransactionHdList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();

            HtmlGenericControl div = new HtmlGenericControl("DIV");
            //HtmlGenericControl h4 = new HtmlGenericControl("h4");
            //h4.InnerHtml = String.Format("Account : {0} {1}", txtGLAccountName.Text, txtSubLedgerDtName.Text);
            //div.Controls.Add(h4);
            div.Controls.Add(PanelContent1);
            return div;
        }
    }
}