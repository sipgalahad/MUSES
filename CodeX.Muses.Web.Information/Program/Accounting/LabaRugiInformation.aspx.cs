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
using CodeX.Web.Common;
using CodeX.Web.Common.UI;
using System.Web.UI.HtmlControls;
using CodeX.Data.Model;
using CodeX.Common;

namespace CodeX.Web.Accounting.Program
{
    public partial class LabaRugiInformation : BasePageList
    {
        protected int PageCount = 1;

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.LABA_RUGI_INFORMATION;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            #region Data Month
            cboMonth.DataSource = Enumerable.Range(1, 13).Select(a => new
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

            cboLevel.DataSource = Enumerable.Range(1, 7);
            cboLevel.EnableCallbackMode = false;
            cboLevel.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboLevel.DropDownStyle = DropDownStyle.DropDownList;
            cboLevel.DataBind();
            cboLevel.SelectedIndex = 0;
            #endregion

            BindGridView(1, true, ref PageCount);
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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
            List<GetGLBalanceProfitLossPerPeriodPerLevel> lstEntity = null;
            if (cboYear.Value != null) 
            {
                int count = Constant.GridViewPageSize.GRID_MASTER;

                lstEntity = BusinessLayer.GetGLBalanceProfitLossPerPeriodPerLevelList(AppSession.UserLogin.SiteID, Convert.ToInt32(cboYear.Value), Convert.ToInt32(cboMonth.Value), Convert.ToInt32(cboLevel.Value), pageIndex, Constant.GridViewPageSize.GRID_MASTER);

                if (isCountPageCount)
                {
                    int totalRow = lstEntity.Count > 0 ? lstEntity.FirstOrDefault().TotalRow : 0;
                    pageCount = Helper.GetPageCount(totalRow, count);
                }
            }
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

    }
}