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
    public partial class CustomerPaymentInformation : BasePageList
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.CUSTOMER_PAYMENT_INFORMATION;
        }

        #region HTML Getter
        protected string OnGetClassStudyTypeRegular()
        {
            return Constant.ClassStudyType.REGULAR;
        }

        protected string OnGetSchoolPeriodNowFilterExpression()
        {
            return string.Format("GCSchoolPeriodStatus != '{0}' AND StartDate <= '{1}' AND EndDate >= '{1}'", Constant.SchoolPeriodStatus.VOID, DateTime.Now.ToString("yyyyMMdd"));
        }

        protected string OnGetPeriodSectionFilterExpression()
        {
            return string.Format("GCPeriodSectionStatus != '{0}'", Constant.SchoolPeriodStatus.VOID);
        }
        #endregion

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<vCustomer> lstCustomer = BusinessLayer.GetvCustomerList("IsDeleted = 0");
            Methods.SetComboBoxField<vCustomer>(cboCustomer, lstCustomer, "BusinessPartnerName", "BusinessPartnerID");
            cboCustomer.SelectedIndex = 0;

            hdnCustomerID.Value = cboCustomer.Value.ToString();
            hdnCustomerName.Value = cboCustomer.Text;
            
            BindGridView();
        }

        private string GetFilterExpression()
        {
            string filterExpression = "1 = 0";
            if (tacARReceiving.Value != "" && tacARReceiving.Value != "0")
                filterExpression = string.Format("ARInvoiceID IN (SELECT ARInvoiceID FROM ARInvoiceReceiving WHERE ARReceivingID = {0})", tacARReceiving.Value);
            return filterExpression;
        }

        List<vARInvoiceDt> lstEntityDt = null;
        List<vARInvoiceDt> lstTransactionDate = null;
        private void BindGridView()
        {
            string filterExpression = GetFilterExpression();
            lstEntityDt = BusinessLayer.GetvARInvoiceDtList(filterExpression);

            lstTotalInvoiceDt = new List<vARInvoiceDt>();

            List<vARInvoiceDt> lstStudent = (from p in lstEntityDt
                                             select new vARInvoiceDt { PayedStudentID = p.PayedStudentID, PayedStudentCode = p.PayedStudentCode, PayedStudentName = p.PayedStudentName, SiteID = p.SiteID, PayedSchoolClassCode = p.PayedSchoolClassCode, PayedSchoolClassName = p.PayedSchoolClassName }).GroupBy(p => new { p.PayedStudentID }).Select(p => p.First()).ToList().OrderBy(p => p.PayedStudentCode).ToList();

            lstTransactionDate = (from p in lstEntityDt
                                                     select new vARInvoiceDt { TransactionMonth = p.TransactionMonth, TransactionYear = p.TransactionYear, GCAdmissionPaymentPeriod = p.GCAdmissionPaymentPeriod }).GroupBy(p => new { p.TransactionMonth, p.TransactionYear }).Select(p => p.First()).ToList().OrderBy(p => p.cfTransactionMonthYear).ToList();

            thReceivingMonth.ColSpan = lstTransactionDate.Count;
            rptReceivingMonth.DataSource = lstTransactionDate;
            rptReceivingMonth.DataBind();

            rptView.DataSource = lstStudent;
            rptView.DataBind();

            rptReceivingMonthTotal.DataSource = lstTransactionDate;
            rptReceivingMonthTotal.DataBind();

            divTotalAll.InnerHtml = lstTotalInvoiceDt.Where(p => p.TransactionMonth == 0 && p.TransactionYear == 0).Sum(p => p.ClaimedAmount).ToString("N");
        }

        List<vARInvoiceDt> lstTotalInvoiceDt = null;
        protected void rptView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                vARInvoiceDt entity = e.Item.DataItem as vARInvoiceDt;

                Repeater rptReceivingMonth = (Repeater)e.Item.FindControl("rptReceivingMonth");
                rptReceivingMonth.DataSource = lstTransactionDate;
                rptReceivingMonth.DataBind();

                HtmlGenericControl divTotalPayment = (HtmlGenericControl)e.Item.FindControl("divTotalPayment");
                decimal total = lstEntityDt.Where(p => p.PayedStudentID == entity.PayedStudentID).Sum(p => p.ClaimedAmount);
                divTotalPayment.InnerHtml = total.ToString("N");

                vARInvoiceDt totalInvoiceDt = lstTotalInvoiceDt.FirstOrDefault(p => p.TransactionMonth == 0 && p.TransactionYear == 0);
                if (totalInvoiceDt != null)
                    totalInvoiceDt.ClaimedAmount += total;
                else
                {
                    totalInvoiceDt = new vARInvoiceDt();
                    totalInvoiceDt.TransactionMonth = 0;
                    totalInvoiceDt.TransactionYear = 0;
                    totalInvoiceDt.ClaimedAmount = total;
                    lstTotalInvoiceDt.Add(totalInvoiceDt);
                }
            }
        }

        protected void rptReceivingMonth_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                vARInvoiceDt student = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vARInvoiceDt;
                vARInvoiceDt transactionMonth = e.Item.DataItem as vARInvoiceDt;

                HtmlGenericControl divPaymentAmount = (HtmlGenericControl)e.Item.FindControl("divPaymentAmount");
                decimal total = lstEntityDt.Where(p => p.PayedStudentID == student.PayedStudentID && p.TransactionMonth == transactionMonth.TransactionMonth && p.TransactionYear == transactionMonth.TransactionYear).Sum(p => p.ClaimedAmount);
                divPaymentAmount.InnerHtml = total.ToString("N");

                vARInvoiceDt totalInvoiceDt = lstTotalInvoiceDt.FirstOrDefault(p => p.TransactionMonth == transactionMonth.TransactionMonth && p.TransactionYear == transactionMonth.TransactionYear);
                if (totalInvoiceDt != null)
                    totalInvoiceDt.ClaimedAmount += total;
                else
                {
                    totalInvoiceDt = new vARInvoiceDt();
                    totalInvoiceDt.TransactionMonth = transactionMonth.TransactionMonth;
                    totalInvoiceDt.TransactionYear = transactionMonth.TransactionYear;
                    totalInvoiceDt.ClaimedAmount = total;
                    lstTotalInvoiceDt.Add(totalInvoiceDt);
                }
            }
        }
        protected void rptReceivingMonthTotal_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                vARInvoiceDt transactionMonth = e.Item.DataItem as vARInvoiceDt;
                HtmlGenericControl divTotal = (HtmlGenericControl)e.Item.FindControl("divTotal");
                decimal total = lstTotalInvoiceDt.FirstOrDefault(p => p.TransactionMonth == transactionMonth.TransactionMonth && p.TransactionYear == transactionMonth.TransactionYear).ClaimedAmount;
                divTotal.InnerHtml = total.ToString("N");
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        public override Control OnGetExportControl(ref bool isShowTitle, ref string fileName)
        {
            DateTime receivingDate = Helper.GetDatePickerValue(Request.Form[hdnReceivingDate.UniqueID]);
            fileName = string.Format("DaftarPenerimaBantuanPendidikan{0}_{1}", receivingDate.ToString("yyyyMMdd"), Request.Form[hdnCustomerName.UniqueID]);
            isShowTitle = false;

            BindGridView();

            HtmlGenericControl div = new HtmlGenericControl("DIV");
            HtmlGenericControl h4 = new HtmlGenericControl("h4");
            HtmlGenericControl h42 = new HtmlGenericControl("h4");

            HtmlGenericControl h1Title = new HtmlGenericControl("h2");
            h1Title.InnerHtml = "DAFTAR PENERIMA BANTUAN PENDIDIKAN";
            div.Controls.Add(h1Title);

            h4.InnerHtml = String.Format("Tanggal Pembayaran : {0}", receivingDate.ToString(Constant.FormatString.DATE_FORMAT));
            h42.InnerHtml = String.Format("Pemberi Bantuan : {0}", Request.Form[hdnCustomerName.UniqueID]);
            div.Controls.Add(h4);
            div.Controls.Add(h42);
            div.Controls.Add(pnlGridView);
            return div;
        }
    }
}