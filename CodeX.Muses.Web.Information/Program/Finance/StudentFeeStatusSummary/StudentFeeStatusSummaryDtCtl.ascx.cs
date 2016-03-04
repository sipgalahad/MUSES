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
    public partial class StudentFeeStatusSummaryDtCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int CurrPage = 1;

        public override void InitializeDataControl(string param)
        {
            String[] lstParam = param.Split('|');
            hdnSchoolClassID.Value = lstParam[0];
            hdnMonth.Value = lstParam[1];
            hdnYear.Value = lstParam[2];
            hdnType.Value = lstParam[3];

            if (hdnType.Value == "1")
                txtHeaderText2.Text = "Sudah Bayar";
            else if (hdnType.Value == "0")
                txtHeaderText2.Text = "Belum Bayar";
            else
                txtHeaderText2.Text = "Semua";
            txtHeaderText.Text = BusinessLayer.GetSchoolClass(Convert.ToInt32(hdnSchoolClassID.Value)).SchoolClassName;

            BindGridView(1, true, ref PageCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = string.Format("SchoolClassID = {0} AND StudentFeeCompTypeID = 2 AND TransactionMonth = {1} AND TransactionYear = {2} AND IsDeleted = 0", hdnSchoolClassID.Value, hdnMonth.Value, hdnYear.Value);
            if(hdnType.Value != "")
                filterExpression += string.Format(" AND IsPaid = {0}", hdnType.Value);

            List<vStudentFee> lstEntity = BusinessLayer.GetvStudentFeeList(filterExpression);
            grdPopupView.DataSource = lstEntity;
            grdPopupView.DataBind();
        }
        
        public override Control OnGetExportControl(ref bool isShowTitle, ref string fileName)
        {
            //DateTime dt = new DateTime(Convert.ToInt32(Request.Form[hdnYear.UniqueID]), Convert.ToInt32(Request.Form[hdnMonth.UniqueID]), 1);
            //isShowTitle = false;
            //fileName = string.Format("{0}_{1}_{2}", Request.Form[txtHeaderText.UniqueID], Request.Form[txtHeaderText2.UniqueID], dt.ToString("yyyyMM"));
            //List<GetStudentReceiveSummaryDt> lstEntity = BusinessLayer.GetStudentReceiveSummaryDt(Request.Form[hdnSiteID.UniqueID], Convert.ToInt32(Request.Form[hdnYear.UniqueID]), Convert.ToInt32(Request.Form[hdnMonth.UniqueID]), Request.Form[hdnType.UniqueID], Convert.ToInt32(Request.Form[hdnStudentFeeCompTypeID.UniqueID]));
            //grdPopupView.DataSource = lstEntity;
            //grdPopupView.DataBind();
            //HtmlGenericControl div = new HtmlGenericControl("DIV");
            //HtmlGenericControl h4 = new HtmlGenericControl("h4");
            //HtmlGenericControl h42 = new HtmlGenericControl("h4");
            //HtmlGenericControl h43 = new HtmlGenericControl("h4");
            //h4.InnerHtml = String.Format("Tipe : {0}", Request.Form[txtHeaderText.UniqueID]);
            //h42.InnerHtml = String.Format("Jenis Pembayaran : {0}", Request.Form[txtHeaderText2.UniqueID]);
            //h43.InnerHtml = String.Format("Periode : {0}", dt.ToString("MMM yyyy"));
            //div.Controls.Add(h4);
            //div.Controls.Add(h42);
            //div.Controls.Add(h43);
            //div.Controls.Add(grdPopupView);
            //return div;
            return null;
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