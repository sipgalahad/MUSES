using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using System.Web.UI.HtmlControls;
using CodeX.Web.Common;

namespace CodeX.Muses.Web.StudentManagement.Report
{
    public partial class BResepRpt : BaseCustomReportCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void Bind(string filterExpression, string[] param)
        {
            //vARInvoiceHd entity = BusinessLayer.GetvARInvoiceHdList(filterExpression)[0];
            //tdARInvoiceNo.InnerHtml = entity.ARInvoiceNo;
            //divCBTN.InnerHtml = entity.CustomerBillToName;

            //vSite site = BusinessLayer.GetvSiteList(String.Format("SiteID = '{0}'",AppSession.UserLogin.SiteID))[0];

            //lstInvoiceDt = BusinessLayer.GetvARInvoiceDtList(String.Format("ARInvoiceID = {0}", entity.ARInvoiceID));
            //Bank bank = BusinessLayer.GetBankList(String.Format("BankID = {0}", lstInvoiceDt[0].BankID))[0];

            //List<TempGroup> temp = lstInvoiceDt.GroupBy(x => x.BusinessPartnerID).Select(cl => new TempGroup{ GroupID = cl.First().BusinessPartnerID, 
            //        GroupName = cl.First().BusinessPartnerName, 
            //        ARInvoiceDateInMonth = cl.First().ARInvoiceDate.ToString("MMMM yyyy"),
            //        Amount = cl.Sum(x => x.ClaimedAmount) }).ToList();

            //rptBusinessPartner.DataSource = temp;
            //rptBusinessPartner.DataBind();

            //String text = divBodyHead.InnerHtml;
            //text = text.Replace("{CustomerBillToName}", entity.CustomerBillToName);
            //text = text.Replace("{SiteName}", site.SiteName);
            //text = text.Replace("{City}", site.City);
            //divBodyHead.InnerHtml = text;

            //text = divBodyFooter.InnerHtml;
            //text = text.Replace("{TotalClaimedAmount}", entity.TotalClaimedAmount.ToString("N2"));
            //text = text.Replace("{TotalClaimedAmountInString}", entity.TotalClaimedAmountInString);
            //text = text.Replace("{SiteName}", site.SiteName);
            //text = text.Replace("{City}", site.City);
            //text = text.Replace("{Bank}", bank.BankName);
            //text = text.Replace("{AccountNo}", bank.BankAccountNo);
            //text = text.Replace("{AccountName}", bank.BankAccountName);
            //text = text.Replace("{FaxNo}", site.FaxNo1);
            //divBodyFooter.InnerHtml = text;

            //text = divLetterFooter.InnerHtml;
            //text = text.Replace("{City}", site.City);
            //text = text.Replace("{DateNow}", DateTime.Now.ToString("dd MMMM yyyy"));
            //divLetterFooter.InnerHtml = text;

            //rptPatientBusinessPartner.DataSource = temp;
            //rptPatientBusinessPartner.DataBind();
        }

        protected void rptPatientBusinessPartner_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item) 
            //{
            //    TempGroup entity = (TempGroup)e.Item.DataItem;
            //    Repeater rpPatientDetail = (Repeater)e.Item.FindControl("rpPatientDetail");
            //    rpPatientDetail.DataSource = lstInvoiceDt.Where(x => x.BusinessPartnerID == entity.GroupID);
            //    rpPatientDetail.DataBind();
            //}
        }
    }
}