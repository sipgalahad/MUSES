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

namespace CodeX.Muses.Web.Accounting.Report
{
    public partial class LNeracaTRpt : BaseCustomReportCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public override void Bind(string filterExpression, string[] param)
        {
            string[] temp1 = param[1].Split(';');
            string[] temp2 = param[2].Split(';');
            subHeaderText.InnerHtml = string.Format("Periode : {0} {1}", temp2[1], temp1[1]);

            List<GetGLBalancePerPeriodForTBalance> lstEntity = BusinessLayer.GetGLBalancePerPeriodForTBalance(AppSession.UserLogin.SiteID, Convert.ToInt32(temp1[0]), Convert.ToInt32(temp2[0]));

            decimal totalAktiva = (decimal)lstEntity.Sum(p => p.AktivaBalanceEND);
            decimal totalPasiva = (decimal)lstEntity.Sum(p => p.PasivaBalanceEND);

            lstEntity.Add(new GetGLBalancePerPeriodForTBalance { AdditionalClassName = "trGrandTotal", AktivaGLAccountName = "TOTAL AKTIVA", AktivaBalanceEND = totalAktiva, PasivaGLAccountName = "TOTAL PASIVA", PasivaBalanceEND = totalPasiva });
            rptView.DataSource = lstEntity;
            rptView.DataBind();
        }
    }
}