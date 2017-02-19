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

namespace CodeX.Muses.Web.Accounting.Program
{
    public partial class TreasuryQuickPicksCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;

        private TreasuryEntry DetailPage
        {
            get { return (TreasuryEntry)Page; }
        }

        public override void InitializeDataControl(string param)
        {
            hdnParam.Value = param;
            BindGridView(1, true, ref PageCount);
        }

        protected void cbpPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        private string GetFilterExpression()
        {
            string filterExpression = "IsHeader = 0";
            return filterExpression;
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Site entity = e.Row.DataItem as Site;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (lstSelectedMember.Contains(entity.ReferenceID.ToString()))
                    chkIsSelected.Checked = true;
            }
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetSiteRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 10);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<Site> lstEntity = BusinessLayer.GetSiteList(filterExpression, 10, pageIndex, "ReferenceID ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
    }
}