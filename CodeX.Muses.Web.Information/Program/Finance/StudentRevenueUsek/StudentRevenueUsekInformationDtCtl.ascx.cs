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
using System.Web.UI.HtmlControls;
using CodeX.Data.Core.Dal;
using CodeX.Muses.Web.Information.Program;

namespace CodeX.Muses.Web.Information.Program
{
    public partial class StudentRevenueUsekInformationDtCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int CurrPage = 1;

        public override void InitializeDataControl(string param)
        {
            String[] lstParam = param.Split('|');
            hdnSiteID.Value = lstParam[0];
            hdnMonth.Value = lstParam[1];
            hdnYear.Value = lstParam[2];

            txtHeaderText.Text = BusinessLayer.GetSite(hdnSiteID.Value).SiteName;
            txtHeaderText2.Text = new DateTime(Convert.ToInt32(hdnYear.Value), Convert.ToInt32(hdnMonth.Value), 1).ToString("MMM yyyy");

            BindGridView(1, true, ref PageCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            List<vStudentFee> lstEntity = BusinessLayer.GetvStudentFeeList(string.Format("SiteID = '{0}' AND TransactionMonth = {1} AND TransactionYear = {2} AND StudentIsDeleted = 0 AND IsDeleted = 0 ORDER BY StudentCode", hdnSiteID.Value, hdnMonth.Value, hdnYear.Value));
            grdPopupView.DataSource = lstEntity;
            grdPopupView.DataBind();
        }
        
        public override Control OnGetExportControl(ref bool isShowTitle, ref string fileName)
        {
            DateTime dt = new DateTime(Convert.ToInt32(Request.Form[hdnYear.UniqueID]), Convert.ToInt32(Request.Form[hdnMonth.UniqueID]), 1);
            isShowTitle = false;
            fileName = string.Format("{0}_{1}_{2}", Request.Form[txtHeaderText.UniqueID], Request.Form[txtHeaderText2.UniqueID], dt.ToString("yyyyMM"));
            List<vStudentFee> lstEntity = BusinessLayer.GetvStudentFeeList(string.Format("SiteID = '{0}' AND TransactionMonth = {1} AND TransactionYear = {2} AND StudentIsDeleted = 0 AND IsDeleted = 0 ORDER BY StudentCode", Request.Form[hdnSiteID.UniqueID], Request.Form[hdnMonth.UniqueID], Request.Form[hdnYear.UniqueID]));
            grdPopupView.DataSource = lstEntity;
            grdPopupView.DataBind();
            HtmlGenericControl div = new HtmlGenericControl("DIV");
            HtmlGenericControl h4 = new HtmlGenericControl("h4");
            HtmlGenericControl h42 = new HtmlGenericControl("h4");
            HtmlGenericControl h43 = new HtmlGenericControl("h4");
            h4.InnerHtml = String.Format("Unit : {0}", Request.Form[txtHeaderText.UniqueID]);
            h42.InnerHtml = String.Format("Bulan : {0}", Request.Form[txtHeaderText2.UniqueID]);
            div.Controls.Add(h4);
            div.Controls.Add(h42);
            div.Controls.Add(grdPopupView);
            return div;
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

        public override void SetToolbarVisibility(ref bool IsAllowExport)
        {
            IsAllowExport = true;
        }    
    }
}