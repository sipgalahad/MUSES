using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Data.Model;
using CodeX.Web.Common;
using CodeX.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;
using System.Text;
using CodeX.Data.Core.Dal;
using System.Web.UI.HtmlControls;
using CodeX.Web.CommonLibs.MasterPage;
using CodeX.Common;
using System.Globalization;
using DevExpress.Web.ASPxEditors;

namespace CodeX.Muses.Web.Information.Program
{
    public partial class ProspectiveStudentPaymentInformation : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.PROSPECTIVE_STUDENT_PAYMENT_INFORMATION;
        }

        #region HTML Getter
        protected string OnGetSchoolPeriodNowFilterExpression()
        {
            return string.Format("GCSchoolPeriodStatus != '{0}' AND StartDate <= '{1}' AND EndDate >= '{1}'", Constant.SchoolPeriodStatus.VOID, DateTime.Now.ToString("yyyyMMdd"));
        }
        #endregion

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<vSite> lstSite = BusinessLayer.GetvSiteList(String.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsHeader = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<vSite>(cboSite, lstSite, "SiteName", "SiteID");
            cboSite.SelectedIndex = 0;

            hdnSiteID.Value = cboSite.Value.ToString();
            hdnSiteName.Value = cboSite.Text;

            RowCountPerPage = 10;
            // BindGridView(CurrPage, true, ref PageCount, ref RowCount);
            BindGridView();
        }

        private string GetFilterExpression()
        {
            if (tacSchoolPeriod.Value != "")
            {
                string filterExpression = "";
                if (Request.Form[hdnSiteID.UniqueID] != null && Request.Form[hdnSiteID.UniqueID] != "")
                    filterExpression = string.Format("SiteID = '{0}'", Request.Form[hdnSiteID.UniqueID]);
                else
                    filterExpression = string.Format("SiteID = '{0}'", hdnSiteID.Value);
                string filterExpressionQuickSearch = hdnFilterExpressionQuickSearch.Value;
                if (filterExpressionQuickSearch != "")
                    filterExpressionQuickSearch = Request.Form[hdnFilterExpressionQuickSearch.UniqueID];
                if (filterExpressionQuickSearch != "")
                    filterExpression += string.Format(" AND {0}", filterExpressionQuickSearch);

                filterExpression += string.Format(" AND SchoolPeriodID = {0} AND GCRegistrationStatus IN ('{1}','{2}','{3}','{4}')", tacSchoolPeriod.Value, Constant.RegistrationStatus.AR_PROCESSED, Constant.RegistrationStatus.PAID, Constant.RegistrationStatus.SETTLED, Constant.RegistrationStatus.CLOSED);
                return filterExpression;
            }
            return "1 = 0";
        }

        decimal totalUsek = 0;
        decimal totalUsekDiskon = 0;
        decimal totalUsekBayar = 0;
        decimal totalUsekSisa = 0;

        decimal totalKeg = 0;
        decimal totalKegDiskon = 0;
        decimal totalKegBayar = 0;
        decimal totalKegSisa = 0;

        decimal totalPemb = 0;
        decimal totalPembDiskon = 0;
        decimal totalPembBayar = 0;
        decimal totalPembSisa = 0;
        List<vStudentFee> lstStudentFee = null;
        List<vARInvoiceDt> lstARInvoiceDt = null;
        //private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        private void BindGridView()
        {
            string filterExpression = GetFilterExpression();
            //if (isCountPageCount)
            //{
            //    rowCount = BusinessLayer.GetvRegistrationRowCount(filterExpression);
            //    pageCount = Helper.GetPageCount(rowCount, 10);
            //}

            //List<vRegistration> lstEntity = BusinessLayer.GetvRegistrationList(filterExpression, 10, pageIndex, "ProspectiveStudentName ASC");

            totalUsek = 0;
            totalUsekDiskon = 0;
            totalUsekBayar = 0;
            totalUsekSisa = 0;

            totalKeg = 0;
            totalKegDiskon = 0;
            totalKegBayar = 0;
            totalKegSisa = 0;

            totalPemb = 0;
            totalPembDiskon = 0;
            totalPembBayar = 0;
            totalPembSisa = 0;
            List<vRegistration> lstEntity = BusinessLayer.GetvRegistrationList(filterExpression);

            string lstProspectiveStudentID = string.Join(",", lstEntity.Select(p => p.ProspectiveStudentID).ToList());
            if (lstProspectiveStudentID != "")
            {
                lstStudentFee = BusinessLayer.GetvStudentFeeList(string.Format("ProspectiveStudentID IN ({0}) AND IsDeleted = 0", lstProspectiveStudentID));
                lstARInvoiceDt = BusinessLayer.GetvARInvoiceDtList(string.Format("ProspectiveStudentID IN ({0}) AND GCTransactionStatus != '{1}' AND IsDeleted = 0", lstProspectiveStudentID, Constant.TransactionStatus.VOID));
            }

            rptView.DataSource = lstEntity;
            rptView.DataBind();

            divTotalUsek.InnerHtml = totalUsek.ToString("N");
            divTotalUsekDiskon.InnerHtml = totalUsekDiskon.ToString("N");
            divTotalUsekBayar.InnerHtml = totalUsekBayar.ToString("N");
            divTotalUsekSisa.InnerHtml = totalUsekSisa.ToString("N");

            divTotalKeg.InnerHtml = totalKeg.ToString("N");
            divTotalKegDiskon.InnerHtml = totalKegDiskon.ToString("N");
            divTotalKegBayar.InnerHtml = totalKegBayar.ToString("N");
            divTotalKegSisa.InnerHtml = totalKegSisa.ToString("N");

            divTotalPemb.InnerHtml = totalPemb.ToString("N");
            divTotalPembDiskon.InnerHtml = totalPembDiskon.ToString("N");
            divTotalPembBayar.InnerHtml = totalPembBayar.ToString("N");
            divTotalPembSisa.InnerHtml = totalPembSisa.ToString("N");
        }

        protected void rptView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                vRegistration entity = e.Item.DataItem as vRegistration;

                List<vARInvoiceDt> lstARInvoiceDt1 = lstARInvoiceDt.Where(p => p.ProspectiveStudentID == entity.ProspectiveStudentID).ToList();
                List<vStudentFee> lstStudentFee1 = lstStudentFee.Where(p => p.ProspectiveStudentID == entity.ProspectiveStudentID).ToList();

                HtmlGenericControl divUsekTotal = e.Item.FindControl("divUsekTotal") as HtmlGenericControl;
                HtmlGenericControl divUsekDiskon = e.Item.FindControl("divUsekDiskon") as HtmlGenericControl;
                HtmlGenericControl divUsekBayar = e.Item.FindControl("divUsekBayar") as HtmlGenericControl;
                HtmlGenericControl divUsekSisa = e.Item.FindControl("divUsekSisa") as HtmlGenericControl;
                List<vStudentFee> lstStudentFeeUsek = lstStudentFee1.Where(p => p.StudentFeeCompTypeID == 2).ToList();
                decimal total = lstStudentFeeUsek.Sum(p => p.TransactionAmount);
                decimal discount = lstStudentFeeUsek.Sum(p => p.TotalDiscountAmount);
                decimal bayar = lstARInvoiceDt1.Where(p => p.StudentFeeCompTypeID == 2).Sum(p => p.PaymentAmount);
                decimal sisa = (total - discount - bayar);
                divUsekTotal.InnerHtml = total.ToString("N");
                divUsekDiskon.InnerHtml = discount.ToString("N");
                divUsekBayar.InnerHtml = bayar.ToString("N");
                divUsekSisa.InnerHtml = sisa.ToString("N");
                totalUsek += total;
                totalUsekDiskon += discount;
                totalUsekBayar += bayar;
                totalUsekSisa += sisa;

                HtmlGenericControl divKegTotal = e.Item.FindControl("divKegTotal") as HtmlGenericControl;
                HtmlGenericControl divKegDiskon = e.Item.FindControl("divKegDiskon") as HtmlGenericControl;
                HtmlGenericControl divKegBayar = e.Item.FindControl("divKegBayar") as HtmlGenericControl;
                HtmlGenericControl divKegSisa = e.Item.FindControl("divKegSisa") as HtmlGenericControl;
                List<vStudentFee> lstStudentFeeKeg = lstStudentFee1.Where(p => p.StudentFeeCompTypeID == 3).ToList();
                total = lstStudentFeeKeg.Sum(p => p.TransactionAmount);
                discount = lstStudentFeeKeg.Sum(p => p.TotalDiscountAmount);
                bayar = lstARInvoiceDt1.Where(p => p.StudentFeeCompTypeID == 3).Sum(p => p.PaymentAmount);
                sisa = (total - discount - bayar);
                divKegTotal.InnerHtml = total.ToString("N");
                divKegDiskon.InnerHtml = discount.ToString("N");
                divKegBayar.InnerHtml = bayar.ToString("N");
                divKegSisa.InnerHtml = sisa.ToString("N");
                totalKeg += total;
                totalKegDiskon += discount;
                totalKegBayar += bayar;
                totalKegSisa += sisa;

                HtmlGenericControl divPembTotal = e.Item.FindControl("divPembTotal") as HtmlGenericControl;
                HtmlGenericControl divPembDiskon = e.Item.FindControl("divPembDiskon") as HtmlGenericControl;
                HtmlGenericControl divPembBayar = e.Item.FindControl("divPembBayar") as HtmlGenericControl;
                HtmlGenericControl divPembSisa = e.Item.FindControl("divPembSisa") as HtmlGenericControl;
                List<vStudentFee> lstStudentFeePemb = lstStudentFee1.Where(p => p.StudentFeeCompTypeID == 1).ToList();
                total = lstStudentFeePemb.Sum(p => p.TransactionAmount);
                discount = lstStudentFeePemb.Sum(p => p.TotalDiscountAmount);
                bayar = lstARInvoiceDt1.Where(p => p.StudentFeeCompTypeID == 1).Sum(p => p.PaymentAmount);
                sisa = (total - discount - bayar);
                divPembTotal.InnerHtml = total.ToString("N");
                divPembDiskon.InnerHtml = discount.ToString("N");
                divPembBayar.InnerHtml = bayar.ToString("N");
                divPembSisa.InnerHtml = sisa.ToString("N");
                totalPemb += total;
                totalPembDiskon += discount;
                totalPembBayar += bayar;
                totalPembSisa += sisa;
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            //int pageCount = 1;
            //int rowCount = 1;
            //string result = "";
            //if (e.Parameter != null && e.Parameter != "")
            //{
            //    string[] param = e.Parameter.Split('|');
            //    if (param[0] == "changepage")
            //    {
            //        BindGridView(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
            //        result = "changepage";
            //    }
            //    else // refresh
            //    {
            //        BindGridView(1, true, ref pageCount, ref rowCount);
            //        result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
            //    }
            //}

            //ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            //panel.JSProperties["cpResult"] = result;
            BindGridView();
        }

        public override Control OnGetExportControl(ref bool isShowTitle, ref string fileName)
        {
            fileName = string.Format("PSB{0}_{1}", Request.Form[hdnSiteName.UniqueID], DateTime.Now.ToString("yyyyMMdd"));
            isShowTitle = false;

            BindGridView();

            HtmlGenericControl div = new HtmlGenericControl("DIV");
            HtmlGenericControl h4 = new HtmlGenericControl("h4");

            HtmlGenericControl h1Title = new HtmlGenericControl("h2");
            h1Title.InnerHtml = "PENERIMAAN SISWA BARU";
            div.Controls.Add(h1Title);
            
            h4.InnerHtml = String.Format("Unit : {0}", Request.Form[hdnSiteName.UniqueID]);
            div.Controls.Add(h4);
            div.Controls.Add(pnlGridView);
            return div;
        }
    }
}