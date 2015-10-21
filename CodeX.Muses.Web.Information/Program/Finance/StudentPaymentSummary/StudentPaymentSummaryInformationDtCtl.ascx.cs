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
    public partial class StudentPaymentSummaryInformationDtCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int CurrPage = 1;

        public override void InitializeDataControl(string param)
        {
            String[] lstParam = param.Split('|');
            hdnSiteID.Value = lstParam[0];
            hdnMonth.Value = lstParam[1];
            hdnYear.Value = lstParam[2];
            hdnType.Value = lstParam[3];
            hdnStudentFeeCompTypeID.Value = lstParam[4];

            if (hdnType.Value == "ThisMonth")
                txtHeaderText.Text = "Bulan Ini";
            else if (hdnType.Value == "DownPayment")
                txtHeaderText.Text = "Uang Muka";
            else if (hdnType.Value == "ProspectiveStudent")
                txtHeaderText.Text = "Siswa Baru";
            else if (hdnType.Value == "ARStudent")
                txtHeaderText.Text = "Piutang";

            txtHeaderText2.Text = BusinessLayer.GetStudentFeeCompType(Convert.ToInt32(hdnStudentFeeCompTypeID.Value)).StudentFeeCompTypeName;

            BindGridView(1, true, ref PageCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            List<GetStudentReceiveSummaryDt> lstEntity = BusinessLayer.GetStudentReceiveSummaryDt(hdnSiteID.Value, Convert.ToInt32(hdnYear.Value), Convert.ToInt32(hdnMonth.Value), hdnType.Value, Convert.ToInt32(hdnStudentFeeCompTypeID.Value));
            grdPopupView.DataSource = lstEntity;
            grdPopupView.DataBind();
        }
        
        public override Control OnGetExportControl(ref bool isShowTitle, ref string fileName)
        {
            DateTime dt = new DateTime(Convert.ToInt32(Request.Form[hdnYear.UniqueID]), Convert.ToInt32(Request.Form[hdnMonth.UniqueID]), 1);
            isShowTitle = false;
            fileName = string.Format("{0}_{1}_{2}", Request.Form[txtHeaderText.UniqueID], Request.Form[txtHeaderText2.UniqueID], dt.ToString("yyyyMM"));
            List<GetStudentReceiveSummaryDt> lstEntity = BusinessLayer.GetStudentReceiveSummaryDt(Request.Form[hdnSiteID.UniqueID], Convert.ToInt32(Request.Form[hdnYear.UniqueID]), Convert.ToInt32(Request.Form[hdnMonth.UniqueID]), Request.Form[hdnType.UniqueID], Convert.ToInt32(Request.Form[hdnStudentFeeCompTypeID.UniqueID]));
            grdPopupView.DataSource = lstEntity;
            grdPopupView.DataBind();
            HtmlGenericControl div = new HtmlGenericControl("DIV");
            HtmlGenericControl h4 = new HtmlGenericControl("h4");
            HtmlGenericControl h42 = new HtmlGenericControl("h4");
            HtmlGenericControl h43 = new HtmlGenericControl("h4");
            h4.InnerHtml = String.Format("Tipe : {0}", Request.Form[txtHeaderText.UniqueID]);
            h42.InnerHtml = String.Format("Jenis Pembayaran : {0}", Request.Form[txtHeaderText2.UniqueID]);
            h43.InnerHtml = String.Format("Periode : {0}", dt.ToString("MMM yyyy"));
            div.Controls.Add(h4);
            div.Controls.Add(h42);
            div.Controls.Add(h43);
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