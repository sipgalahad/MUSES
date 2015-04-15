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
using System.Globalization;

namespace CodeX.Muses.Web.Accounting.Report
{
    public partial class LGeneralLedgerRpt : BaseCustomReportCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        public class Period 
        {
            int _year;
            int _month;

            public int Year
            {
                get { return _year; }
                set { _year = value; }
            }
            public int Month
            {
                get { return _month; }
                set { _month = value; }
            }
        }

        public override void Bind(string filterExpression, string[] param)
        {
            String periodStart = param[0].Substring(0, 8);
            String periodEnd = param[0].Substring(8, 8);

            int yearStart = Convert.ToInt32(periodStart.Substring(0,4));
            int yearEnd = Convert.ToInt32(periodEnd.Substring(0,4));
            int monthStart = Convert.ToInt32(periodStart.Substring(4, 2));
            int monthEnd = Convert.ToInt32(periodEnd.Substring(4, 2));
            List<Period> lstPeriod = new List<Period>();

            for (; yearStart <= yearEnd; yearStart++) 
            {
                int temp = 12;
                if (yearStart < yearEnd) temp = 13 - monthStart;
                else temp = monthEnd - monthStart + 1;

                lstPeriod.AddRange(Enumerable.Range(monthStart, temp).Select(a => new Period
                {
                    Year = yearStart,
                    Month = a
                }));
                monthStart = 1;
            }

            rptPeriod.DataSource = lstPeriod;
            rptPeriod.DataBind();
        }

        protected void rptPeriod_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                List<GetGLBalancePerPeriod> lstEntity = null;
                Period period = e.Item.DataItem as Period;

                HtmlTableCell tdKetPeriod = e.Item.FindControl("tdKetPeriod") as HtmlTableCell;
                tdKetPeriod.InnerHtml = String.Format("Period : {0} {1}",DateTimeFormatInfo.CurrentInfo.GetMonthName(period.Month), period.Year);

                Repeater rptGLBalance = e.Item.FindControl("rptGLBalance") as Repeater;
                HtmlTableCell tdTotalBalanceBEGIN = e.Item.FindControl("tdTotalBalanceBEGIN") as HtmlTableCell;
                HtmlTableCell tdTotalBalanceDEBIT = e.Item.FindControl("tdTotalBalanceDEBIT") as HtmlTableCell;
                HtmlTableCell tdTotalBalanceCREDIT = e.Item.FindControl("tdTotalBalanceCREDIT") as HtmlTableCell;
                HtmlTableCell tdTotalBalanceEND = e.Item.FindControl("tdTotalBalanceEND") as HtmlTableCell;

                lstEntity = BusinessLayer.GetGLBalancePerPeriodList(AppSession.UserLogin.SiteID, period.Year, period.Month, true, 1, 5000);
                
                rptGLBalance.DataSource = lstEntity;
                rptGLBalance.DataBind();

                Decimal TotalBalanceBEGIN = lstEntity.Sum(x => x.BalanceBEGIN);
                Decimal TotalBalanceDEBIT = lstEntity.Sum(x => x.BalanceDEBIT);
                Decimal TotalBalanceCREDIT = lstEntity.Sum(x => x.BalanceCREDIT);
                Decimal TotalBalanceEND = lstEntity.Sum(x => x.BalanceEND);

                tdTotalBalanceBEGIN.InnerHtml = tdTotalBalanceBEGIN.InnerHtml.Replace("{TotalBalanceBEGIN}", TotalBalanceBEGIN < 0 ? String.Format("({0})", (TotalBalanceBEGIN * -1).ToString("N")) : TotalBalanceBEGIN.ToString("N"));
                tdTotalBalanceDEBIT.InnerHtml = tdTotalBalanceDEBIT.InnerHtml.Replace("{TotalBalanceDEBIT}", TotalBalanceDEBIT < 0 ? String.Format("({0})", (TotalBalanceDEBIT * -1).ToString("N")) : TotalBalanceDEBIT.ToString("N"));
                tdTotalBalanceCREDIT.InnerHtml = tdTotalBalanceCREDIT.InnerHtml.Replace("{TotalBalanceCREDIT}", TotalBalanceCREDIT < 0 ? String.Format("({0})", (TotalBalanceCREDIT * -1).ToString("N")) : TotalBalanceCREDIT.ToString("N"));
                tdTotalBalanceEND.InnerHtml = tdTotalBalanceEND.InnerHtml.Replace("{TotalBalanceEND}", TotalBalanceEND < 0 ? String.Format("({0})", (TotalBalanceEND * -1).ToString("N")) : TotalBalanceEND.ToString("N"));
            }
        }

        protected void rptGLBalance_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                GetGLBalancePerPeriod entity = e.Item.DataItem as GetGLBalancePerPeriod;
                HtmlTableCell tdBalanceBEGIN = e.Item.FindControl("tdBalanceBEGIN") as HtmlTableCell;
                HtmlTableCell tdBalanceDEBIT = e.Item.FindControl("tdBalanceDEBIT") as HtmlTableCell;
                HtmlTableCell tdBalanceCREDIT = e.Item.FindControl("tdBalanceCREDIT") as HtmlTableCell;
                HtmlTableCell tdBalanceEND = e.Item.FindControl("tdBalanceEND") as HtmlTableCell;

                tdBalanceBEGIN.InnerHtml = entity.BalanceBEGIN < 0 ? String.Format("({0})", (entity.BalanceBEGIN * -1).ToString("N")) : entity.BalanceBEGIN.ToString("N");
                tdBalanceDEBIT.InnerHtml = entity.BalanceDEBIT < 0 ? String.Format("({0})", (entity.BalanceDEBIT * -1).ToString("N")) : entity.BalanceDEBIT.ToString("N");
                tdBalanceCREDIT.InnerHtml = entity.BalanceCREDIT < 0 ? String.Format("({0})", (entity.BalanceCREDIT * -1).ToString("N")) : entity.BalanceCREDIT.ToString("N");
                tdBalanceEND.InnerHtml = entity.BalanceEND < 0 ? String.Format("({0})", (entity.BalanceEND * -1).ToString("N")) : entity.BalanceEND.ToString("N");
            }
        }


    }
}