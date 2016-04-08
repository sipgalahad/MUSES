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
    public partial class StudentPaymentInformation : BasePageList
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.STUDENT_PAYMENT_INFORMATION;
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
            List<vSite> lstSite = BusinessLayer.GetvSiteList(String.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsHeader = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<vSite>(cboSite, lstSite, "SiteName", "SiteID");
            cboSite.SelectedIndex = 0;

            hdnSiteID.Value = cboSite.Value.ToString();
            hdnSiteName.Value = cboSite.Text;

            txtTransactionDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            
            BindGridView();
        }

        private string GetFilterExpression()
        {
            string filterExpression = "";
            if (Request.Form[txtTransactionDate.UniqueID] != null && Request.Form[txtTransactionDate.UniqueID] != "")
                filterExpression = string.Format("ReceivingDate = '{0}'", Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]).ToString("yyyyMMdd"));
            else
                filterExpression = string.Format("ReceivingDate = '{0}'", Helper.GetDatePickerValue(txtTransactionDate.Text).ToString("yyyyMMdd"));
            if (Request.Form[hdnSiteID.UniqueID] != null && Request.Form[hdnSiteID.UniqueID] != "")
                filterExpression += string.Format(" AND SiteID = '{0}'", Request.Form[hdnSiteID.UniqueID]);
            else
                filterExpression += string.Format(" AND SiteID = '{0}'", hdnSiteID.Value);
            filterExpression += string.Format(" AND GCTransactionStatus != '{0}' AND BusinessPartnerID IS NULL ORDER BY VirtualAccountNo", Constant.TransactionStatus.VOID);

            return filterExpression;
        }

        List<ARReceivingDt> lstEntityDt = null;
        List<vARInvoiceReceiving> lstARInvoiceReceiving = null;
        private void BindGridView()
        {
            string filterExpression = GetFilterExpression();
            List<vARReceivingHd> lstEntity = BusinessLayer.GetvARReceivingHdList(filterExpression);
            
            if (lstEntity.Count > 0)
            {
                string lstARReceivingID = string.Join(",", lstEntity.Select(p => p.ARReceivingID).ToList());
                lstEntityDt = BusinessLayer.GetARReceivingDtList(string.Format("ARReceivingID IN ({0}) AND GCARPaymentMethod IN ('{1}')", lstARReceivingID, Constant.PaymentMethod.DOWN_PAYMENT_RETURN));
                lstARInvoiceReceiving = BusinessLayer.GetvARInvoiceReceivingList(string.Format("ARReceivingID IN ({0}) AND ReceivingAmount != 0", lstARReceivingID));
            }

            rptView.DataSource = lstEntity;
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
                vARReceivingHd entity = e.Item.DataItem as vARReceivingHd;
                List<ARReceivingDt> lstARReceivingDt = lstEntityDt.Where(p => p.ARReceivingID == entity.ARReceivingID).ToList();
                List<vARInvoiceReceiving> lstARInvoiceReceiving1 = lstARInvoiceReceiving.Where(p => p.ARReceivingID == entity.ARReceivingID).ToList();

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
                usek += entity.DepositAmount;

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

        public override Control OnGetExportControl(ref bool isShowTitle, ref string fileName)
        {
            fileName = string.Format("PenerimaanBankMandiri{0}_{1}", Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]).ToString("yyyyMMdd"), Request.Form[hdnSiteName.UniqueID]);
            isShowTitle = false;

            string filterExpression = GetFilterExpression();
            List<vARReceivingHd> lstEntity = BusinessLayer.GetvARReceivingHdList(filterExpression);
            rptView.DataSource = lstEntity;
            rptView.DataBind();


            HtmlGenericControl div = new HtmlGenericControl("DIV");
            HtmlGenericControl h4 = new HtmlGenericControl("h4");
            HtmlGenericControl h42 = new HtmlGenericControl("h4");

            HtmlGenericControl h1Title = new HtmlGenericControl("h2");
            h1Title.InnerHtml = "YAYASAN RICCI";
            div.Controls.Add(h1Title);

            HtmlGenericControl h2Title = new HtmlGenericControl("h2");
            h2Title.InnerHtml = "BUKTI PENERIMAAN BANK MANDIRI a/c. 128-000-555-3-224";
            div.Controls.Add(h2Title);


            h4.InnerHtml = String.Format("Tanggal : {0}", Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]).ToString(Constant.FormatString.DATE_FORMAT));
            h42.InnerHtml = String.Format("Unit : {0}", Request.Form[hdnSiteName.UniqueID]);
            div.Controls.Add(h4);
            div.Controls.Add(h42);
            div.Controls.Add(pnlGridView);
            return div;
        }
    }
}