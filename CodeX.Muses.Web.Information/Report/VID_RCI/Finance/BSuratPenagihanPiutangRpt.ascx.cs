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

namespace CodeX.Muses.Web.Information.Report
{
    public partial class BSuratPenagihanPiutangRpt : BaseCustomReportCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void Bind(string filterExpression, string[] param)
        {
            List<vARInvoiceDt> lstARInvoiceDt = BusinessLayer.GetvARInvoiceDtList(String.Format("StudentID = {0} AND GCTransactionStatus NOT IN ('{1}','{2}')", param[0], Constant.TransactionStatus.VOID, Constant.TransactionStatus.CLOSED));
            Int32 id = lstARInvoiceDt[0].StudentID;
            vStudent std = BusinessLayer.GetvStudentList(String.Format("StudentID = {0}", id)).FirstOrDefault();
            var lstObject = lstARInvoiceDt.GroupBy(x => x.GCAdmissionPaymentPeriod).Select(
                    y => new
                    {
                        GroupName = y.Key,
                        TotalAmount = y.Sum(z => z.ClaimedAmount),
                        Remarks = String.Join("; ", y.ToList().Where(x => x.StudentFeeCompTypeName == y.Key).Select(g => g.cfStudentFeeCompTypeName))
                    });

            String text = divPiutang.InnerHtml;
            if (std != null)
            {
                text = text.Replace("{StudentName}", std.Name);
                text = text.Replace("{Grade}", std.Grade);
                text = text.Replace("{Class}", String.Format("{0} / {1}", std.SchoolClassName.Replace("Kelas ", ""), std.StudentCode));
            }
            text = text.Replace("{Usek}", lstObject.Where(x => x.GroupName == Constant.AdmissionPaymentPeriod.BULANAN).Sum(p => p.TotalAmount).ToString("N2"));
            text = text.Replace("{Kegiatan}", lstObject.Where(x => x.GroupName == Constant.AdmissionPaymentPeriod.TAHUNAN).Sum(p => p.TotalAmount).ToString("N2"));
            text = text.Replace("{Pembangunan}", lstObject.Where(x => x.GroupName == Constant.AdmissionPaymentPeriod.SEKALI_BAYAR).Sum(p => p.TotalAmount).ToString("N2"));
            

            int month = DateTime.Now.Month;
            string cfMonth = "";
            if (month > 9)
                cfMonth = month.ToString();
            else
                cfMonth = string.Format("0{0}", month);
            string monthInRome = "";
            switch (month)
            {
                case 1: monthInRome = "I"; break;
                case 2: monthInRome = "II"; break;
                case 3: monthInRome = "III"; break;
                case 4: monthInRome = "IV"; break;
                case 5: monthInRome = "V"; break;
                case 6: monthInRome = "VI"; break;
                case 7: monthInRome = "VII"; break;
                case 8: monthInRome = "VIII"; break;
                case 9: monthInRome = "IX"; break;
                case 10: monthInRome = "X"; break;
                case 11: monthInRome = "XI"; break;
                case 12: monthInRome = "XII"; break;
            }
            string site = "";
            if (AppSession.UserLogin.SiteID == "001.02")
                site = "RII";
            else
                site = "RI";
            string no = string.Format("{0}/SKP/{1}/{2}/{3}", cfMonth, monthInRome, site, DateTime.Now.Year % 2000);
            text = text.Replace("{No}", no);
            divPiutang.InnerHtml = text;
        }
    }
}