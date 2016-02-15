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
using System.Web.UI.HtmlControls;
using System.Globalization;

namespace CodeX.Muses.Web.Information.Program
{
    public partial class StudentPaymentSummaryInformation : BasePageList
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.STUDENT_PAYMENT_SUMMARY;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<vSite> lstSite = BusinessLayer.GetvSiteList(String.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsHeader = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<vSite>(cboSite, lstSite, "SiteName", "SiteID");
            cboSite.SelectedIndex = 0;

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

            BindGridView();
        }

        List<vARReceivingHd> lstARReceivingHd = null;
        List<vARReceivingDt> lstEntityDt = null;
        List<vARInvoiceReceiving> lstARInvoiceReceiving = null;
        #region Bind Grid View
        private void BindGridView()
        {
            hdnTempPeriodText.Value = string.Format("BULAN {0} {1}", cboMonth.Text, cboYear.Value);

            DateTime startDate = new DateTime(Convert.ToInt32(cboYear.Value), Convert.ToInt32(cboMonth.Value), 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            List<DateTime> lstDateTime = new List<DateTime>();
            for (var dt = startDate; dt <= endDate; dt = dt.AddDays(1))
            {
                lstDateTime.Add(dt);
            }

            string filterExpression = string.Format("SiteID = '{0}' AND MONTH(ReceivingDate) = {1} AND YEAR(ReceivingDate) = {2} AND GCTransactionStatus != '{3}' AND BusinessPartnerID IS NULL", cboSite.Value, cboMonth.Value, cboYear.Value, Constant.TransactionStatus.VOID);
            lstARReceivingHd = BusinessLayer.GetvARReceivingHdList(filterExpression);

            if (lstARReceivingHd.Count > 0)
            {
                string lstARReceivingID = string.Join(",", lstARReceivingHd.Select(p => p.ARReceivingID).ToList());
                lstEntityDt = BusinessLayer.GetvARReceivingDtList(string.Format("ARReceivingID IN ({0}) AND GCARPaymentMethod IN ('{1}')", lstARReceivingID, Constant.PaymentMethod.DOWN_PAYMENT_RETURN));
                lstARInvoiceReceiving = BusinessLayer.GetvARInvoiceReceivingList(string.Format("ARReceivingID IN ({0}) AND ReceivingAmount != 0", lstARReceivingID));
            }
            else
            {
                lstEntityDt = new List<vARReceivingDt>();
                lstARInvoiceReceiving = new List<vARInvoiceReceiving>();
            }

            rptView.DataSource = lstDateTime;
            rptView.DataBind();

            divTotalPemb.InnerHtml = totalUangPemb.ToString("N2");
            divTotalUsek.InnerHtml = totalUangSek.ToString("N2");
            divTotalKeg.InnerHtml = totalUangKeg.ToString("N2");
            divTotalDenda.InnerHtml = totalDenda.ToString("N");
            divTotalAll.InnerHtml = (totalUangPemb + totalUangSek + totalUangKeg + totalDenda).ToString("N2");
        }

        decimal totalUangPemb = 0;
        decimal totalUangSek = 0;
        decimal totalUangKeg = 0;
        decimal totalDenda = 0;
        protected void rptView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DateTime dt = (DateTime)e.Item.DataItem;
                List<vARReceivingHd> lstARReceivingHd1 = lstARReceivingHd.Where(p => p.ReceivingDate == dt).ToList();
                List<vARReceivingDt> lstARReceivingDt = lstEntityDt.Where(p => p.ReceivingDate == dt).ToList();
                List<vARInvoiceReceiving> lstARInvoiceReceiving1 = lstARInvoiceReceiving.Where(p => p.ReceivingDate == dt).ToList();

                HtmlGenericControl divPemb = e.Item.FindControl("divPemb") as HtmlGenericControl;
                HtmlGenericControl divSek = e.Item.FindControl("divUsek") as HtmlGenericControl;
                HtmlGenericControl divKeg = e.Item.FindControl("divKeg") as HtmlGenericControl;
                HtmlGenericControl divDenda = e.Item.FindControl("divDenda") as HtmlGenericControl;
                HtmlGenericControl divTotal = e.Item.FindControl("divTotal") as HtmlGenericControl;
                decimal pemb = lstARInvoiceReceiving1.Where(p => p.StudentFeeCompTypeID == 1).Sum(p => p.ReceivingAmount);
                decimal usek = lstARInvoiceReceiving1.Where(p => p.StudentFeeCompTypeID == 2).Sum(p => p.ReceivingAmount - p.cfPenaltyAmount);
                decimal keg = lstARInvoiceReceiving1.Where(p => p.StudentFeeCompTypeID == 3).Sum(p => p.ReceivingAmount);
                decimal denda = lstARInvoiceReceiving1.Where(p => p.StudentFeeCompTypeID == 2).Sum(p => p.cfPenaltyAmount);

                usek -= lstARReceivingDt.Sum(p => p.PaymentAmount);
                usek += lstARReceivingHd1.Sum(p => p.DepositAmount);

                totalUangPemb += pemb;
                totalUangSek += usek;
                totalUangKeg += keg;
                totalDenda += denda;

                decimal total = pemb + usek + keg + denda;
                divPemb.InnerHtml = pemb.ToString("N2");
                divSek.InnerHtml = usek.ToString("N2");
                divKeg.InnerHtml = keg.ToString("N2");
                divDenda.InnerHtml = denda.ToString("N2");
                divTotal.InnerHtml = total.ToString("N2");
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion

        public override Control OnGetExportControl(ref bool isShowTitle, ref string fileName)
        {
            isShowTitle = false;
            HtmlGenericControl div = new HtmlGenericControl("DIV");
            HtmlGenericControl div2 = new HtmlGenericControl("DIV");
            HtmlGenericControl h41 = new HtmlGenericControl("DIV");
            HtmlGenericControl h42 = new HtmlGenericControl("DIV");
            HtmlGenericControl h43 = new HtmlGenericControl("DIV");
            h41.InnerHtml = "PENERIMAAN UANG SEKOLAH, U.KEGIATAN & U.PEMBANGUNAN";
            h42.InnerHtml = hdnExportPeriodText.Value;
            h43.InnerHtml = string.Format("Unit {0}", AppSession.UserLogin.SiteName);
            div.Controls.Add(h41);
            div.Controls.Add(h42);
            div.Controls.Add(h43);
            div2.InnerHtml = hdnExportControl.Value;
            div.Controls.Add(div2);
            return div;
        }
    }
}