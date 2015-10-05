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
using CodeX.Common;

namespace CodeX.Muses.Web.Finance.Report
{
    public partial class BSuratPenagihanPiutangRpt : BaseCustomReportCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        public override void Bind(string filterExpression, string[] param)
        {
            List<vARInvoiceDt> lstARInvoiceDt = BusinessLayer.GetvARInvoiceDtList(String.Format("ARInvoiceID IN ({0})",param[0]));
            Int32 id = lstARInvoiceDt[0].StudentID;

            vStudent student = BusinessLayer.GetvStudentList(String.Format("StudentID = {0}",id))[0];
            String text = divStudent.InnerHtml;
            text = text.Replace("{StudentName}", student.Name);
            text = text.Replace("{Grade}", student.Grade);
            text = text.Replace("{Class}", student.SchoolClassName);
            divStudent.InnerHtml = text;

            var lstObject = lstARInvoiceDt.GroupBy(x => x.StudentFeeCompTypeName).Select(
                    y => new { GroupName = y.Key,
                    TotalAmount = y.Sum(z => z.ClaimedAmount), 
                    Remarks = String.Join("; ", y.ToList().Where(x => x.StudentFeeCompTypeName == y.Key).Select(g => g.cfStudentFeeCompTypeName))});
            rptPiutang.DataSource = lstObject;
            rptPiutang.DataBind();

            List<SiteParameter> lstParam = BusinessLayer.GetSiteParameterList(String.Format("ParameterCode IN ('{0}','{1}') AND SiteID = '{2}'",Constant.SiteParameter.HEADMASTER,Constant.SiteParameter.FINANCE_MANAGER, student.SiteID));
            Site site = BusinessLayer.GetSite(student.SiteID);
            Address address = BusinessLayer.GetAddress(site.AddressID);
            
            text = divPageFooter.InnerHtml;
            text = text.Replace("{HeadMaster}",lstParam.FirstOrDefault(x => x.ParameterCode == Constant.SiteParameter.HEADMASTER).ParameterValue);
            text = text.Replace("{FinanceManager}", lstParam.FirstOrDefault(x => x.ParameterCode == Constant.SiteParameter.FINANCE_MANAGER).ParameterValue);
            text = text.Replace("{SiteName}", site.SiteName);
            String phoneNo = address.PhoneNo1;
            if(address.PhoneNo2 != "")
                phoneNo += ", "+address.PhoneNo2;
            text = text.Replace("{PhoneNo}", phoneNo);
            divPageFooter.InnerHtml = text;
        }
    }
}