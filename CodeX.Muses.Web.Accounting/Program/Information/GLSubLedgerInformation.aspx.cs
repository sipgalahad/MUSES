using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Utils;
using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxEditors;
using System.Globalization;
using CodeX.Data.Core.Dal;
using CodeX.Web.Common;
using CodeX.Data.Model;
using CodeX.Web.Common.UI;
using CodeX.Common;
using System.Web.UI.HtmlControls;

namespace CodeX.Web.Accounting.Program
{
    public partial class GLSubLedgerInformation : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Accounting.BALANCE_INFORMATION_SUB_ACCOUNT;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            #region Data Month
            cboMonth.DataSource = Enumerable.Range(1, 12).Select(a => new
            {
                MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(a),
                MonthNumber = a
            });
            cboMonth.TextField = "MonthName";
            cboMonth.ValueField = "MonthNumber";
            cboMonth.EnableCallbackMode = false;
            cboMonth.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboMonth.DropDownStyle = DropDownStyle.DropDownList;
            cboMonth.DataBind();
            cboMonth.Value = DateTime.Now.Month.ToString();

            cboYear.DataSource = Enumerable.Range(DateTime.Now.Year - 99, 100).Reverse();
            cboYear.EnableCallbackMode = false;
            cboYear.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboYear.DropDownStyle = DropDownStyle.DropDownList;
            cboYear.DataBind();
            cboYear.SelectedIndex = 0;
            #endregion
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
            String periodNo = "";
            List<GetGLBalanceDtPerPeriod> lstEntity = new List<GetGLBalanceDtPerPeriod>();
            if (cboYear.Value != null && hdnGLAccountID.Value != null && hdnGLAccountID.Value != "")
            {
                periodNo = String.Format("{0}{1}", cboYear.Value, Convert.ToInt32(cboMonth.Value).ToString("00"));

                if (isCountPageCount)
                {
                    rowCount = BusinessLayer.GetGLBalanceDtPerPeriodRowCount(Convert.ToInt32(hdnGLAccountID.Value), Convert.ToInt32(cboYear.Value), Convert.ToInt32(cboMonth.Value));
                    pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
                }

                lstEntity = BusinessLayer.GetGLBalanceDtPerPeriodList(Convert.ToInt32(hdnGLAccountID.Value), Convert.ToInt32(cboYear.Value), Convert.ToInt32(cboMonth.Value), pageIndex, Constant.GridViewPageSize.GRID_MASTER);
            }
            else
            {
                lstEntity = new List<GetGLBalanceDtPerPeriod>();
                pageCount = 0;
            }
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        public override Control OnGetExportControl()
        {
            List<GetGLBalanceDtPerPeriod> lstEntity = BusinessLayer.GetGLBalanceDtPerPeriodList(Convert.ToInt32(hdnGLAccountID.Value), Convert.ToInt32(cboYear.Value), Convert.ToInt32(cboMonth.Value), 1, 5000);
            grdView.DataSource = lstEntity;
            grdView.DataBind();

            HtmlGenericControl div = new HtmlGenericControl("DIV");
            HtmlGenericControl h4 = new HtmlGenericControl("h4");
            h4.InnerHtml = String.Format("Periode : {0} - {1}", hdnSelectedYear.Value, hdnSelectedMonth.Value);
            div.Controls.Add(h4);
            div.Controls.Add(PanelContent1);
            return div;
        }
    }
}