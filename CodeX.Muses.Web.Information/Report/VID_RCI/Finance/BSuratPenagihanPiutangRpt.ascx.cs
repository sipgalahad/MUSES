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

        protected string printMargin = "";
        public override void Bind(string filterExpression, string[] param)
        {
            string[] temp = param[0].Split(';');
            int studentID = Convert.ToInt32(temp[0]);
            DateTime date = Helper.GetDatePickerValue(temp[1]);

            vStudent std = BusinessLayer.GetvStudentList(String.Format("StudentID = {0}", studentID)).FirstOrDefault();
            GetARStudentPerDate entity = BusinessLayer.GetARStudentPerDate(false, studentID.ToString(), date).FirstOrDefault();

            printMargin = BusinessLayer.GetSiteParameter(std.SiteID, Constant.SiteParameter.STUDENT_BILL_PRINT_MARGIN).ParameterValue;

            String text = divPiutang.InnerHtml;
            if (std != null)
            {
                SiteParameter sp = BusinessLayer.GetSiteParameter(std.SiteID, Constant.SiteParameter.SCHOOL_TYPE);
                string schoolType = BusinessLayer.GetStandardCode(sp.ParameterValue).StandardCodeName;
                text = text.Replace("{StudentName}", std.Name);
                text = text.Replace("{SchoolType}", schoolType);
                text = text.Replace("{Class}", String.Format("{0} / {1}", std.SchoolClassName.Replace("Kelas ", ""), std.StudentCode));
            }
            text = text.Replace("{Usek}", entity.Col2.ToString("N2"));
            text = text.Replace("{Kegiatan}", entity.Col3.ToString("N2"));
            text = text.Replace("{Pembangunan}", entity.Col1.ToString("N2"));
            text = text.Replace("{UsekRemarks}", entity.Remarks);
            

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

            vSite objSite = BusinessLayer.GetvSiteList(string.Format("SiteID = '{0}'", std.SiteID)).FirstOrDefault();

            divCityDateNow.InnerHtml = string.Format("{0}, {1}", objSite.City, DateTime.Now.ToString(Constant.FormatString.DATE_FORMAT));
        }
    }
}