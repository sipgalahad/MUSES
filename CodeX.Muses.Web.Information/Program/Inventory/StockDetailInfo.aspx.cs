using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using DevExpress.Utils;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;
using DevExpress.Web.ASPxEditors;
using CodeX.Common;


namespace CodeX.Muses.Web.Information.Program
{
    public partial class StockDetailInfo : BasePageList
    {
        protected int PageCount = 0;
        protected int RowCount = 0;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;     
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.STOCK_DETAIL_INFO;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            txtDateFrom.Text = DateTime.Now.AddDays(-7).ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtDateTo.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        protected string OnGetLocationFilterExpression()
        {
            return string.Format("{0};{1};;", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID);
        }

        public string OnGetFilterExpression()
        {
            return Request.Form[hdnFilterExpression.UniqueID];
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            if (hdnLocationID.Value != "")
            {
                hdnFilterExpression.Value = string.Format("LocationID = {0} AND MovementDate BETWEEN '{1}' AND '{2}'", hdnLocationID.Value, Helper.GetDatePickerValue(txtDateFrom.Text).ToString("yyyyMMdd"), Helper.GetDatePickerValue(txtDateTo.Text).ToString("yyyyMMdd"));
                if (isCountPageCount)
                {
                    string filterExpression = string.Format("LocationID = {0} AND ItemName1 LIKE '%{1}%' AND IsDeleted = 0", hdnLocationID.Value, txtItemName.Text);
                    rowCount = BusinessLayer.GetvItemBalanceRowCount(filterExpression);
                    pageCount = Helper.GetPageCount(rowCount, 10);
                }

                List<GetItemMovementPerPeriodeDetail> lstEntity = BusinessLayer.GetItemMovementPerPeriodeDetail(string.Format("{0}|{1}", Helper.GetDatePickerValue(txtDateFrom.Text).ToString("yyyyMMdd"), Helper.GetDatePickerValue(txtDateTo.Text).ToString("yyyyMMdd")), Convert.ToInt32(hdnLocationID.Value), txtItemName.Text, pageIndex, 10);
                lvwView.DataSource = lstEntity;
                lvwView.DataBind();
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
    }
}