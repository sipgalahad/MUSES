using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;
using DevExpress.Web.ASPxEditors;
using System.Globalization;
using CodeX.Data.Core.Dal;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.Information.Program
{
    public partial class TariffInformation : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.TARIFF_INFORMATION;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            hdnVATPercentage.Value = BusinessLayer.GetSettingParameter(Constant.SettingParameter.VAT_PERCENTAGE).ParameterValue;

            RowCountPerPage = 16;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
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

        protected string GetFilterExpression()
        {
            string filterExpression = "IsDeleted = 0";
            if (hdnFilterExpressionQuickSearch.Value == "Search")
                hdnFilterExpressionQuickSearch.Value = " ";
            if (hdnFilterExpressionQuickSearch.Value != "")
                filterExpression += string.Format(" AND {0}", hdnFilterExpressionQuickSearch.Value);
            return filterExpression;
        }

        List<vItemTariffCustom> lstItemTariff = null;
        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = GetFilterExpression();

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetItemMasterRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 16);
            }

            List<ItemMaster> lstEntity = BusinessLayer.GetItemMasterList(filterExpression, 16, pageIndex, "ItemName1 ASC");
            string lstItemID = String.Join(",", lstEntity.Select(p => p.ItemID).ToList());
            if (lstItemID != "")
            {
                lstItemTariff = BusinessLayer.GetvItemTariffCustomList(string.Format("ItemID IN ({0})", lstItemID));
            }
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (lstItemTariff != null)
                {
                    ItemMaster entity = (ItemMaster)e.Row.DataItem;
                    vItemTariffCustom itemTariff = lstItemTariff.FirstOrDefault(p => p.ItemID == entity.ItemID);
                    HtmlGenericControl divCurrentTariff = (HtmlGenericControl)e.Row.FindControl("divCurrentTariff");
                    HtmlGenericControl divCurrentTariffAfterVAT = (HtmlGenericControl)e.Row.FindControl("divCurrentTariffAfterVAT");
                    decimal tariff = 0;
                    if (itemTariff != null)
                        tariff = Convert.ToDecimal(itemTariff.Tariff);
                    divCurrentTariff.InnerHtml = tariff.ToString("N");
                    divCurrentTariffAfterVAT.InnerHtml = Math.Round(tariff * (100 + Convert.ToDecimal(hdnVATPercentage.Value)) / 100).ToString("N");


                }
            }
        }
    }
}