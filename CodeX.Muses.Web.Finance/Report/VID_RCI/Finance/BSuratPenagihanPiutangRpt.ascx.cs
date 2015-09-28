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
            List<vARInvoiceDt> lstARInvoiceDt = BusinessLayer.GetvARInvoiceDtList(String.Format("ARInvoiceID = {0}",param[0]));

            var lstObject = lstARInvoiceDt.GroupBy(x => x.StudentFeeCompTypeName).Select(
                    y => new { GroupName = y.Key,
                    TotalAmount = y.Sum(z => z.ClaimedAmount), 
                    Remarks = String.Join("; ", y.ToList().Where(x => x.StudentFeeCompTypeName == y.Key).Select(g => g.cfStudentFeeCompTypeName))});
            rptPiutang.DataSource = lstObject;
            rptPiutang.DataBind();
        }
    }
}