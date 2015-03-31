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

namespace CodeX.Muses.Web.StudentManagement.Report
{
    public partial class BSuratPenentuanPembayaranRpt : BaseCustomReportCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        List<StudentFee> lstRegFee = null;

        public override void Bind(string filterExpression, string[] param)
        {
            vPeriodAdmission pa = BusinessLayer.GetvPeriodAdmissionList(String.Format("PeriodAdmissionID = {0}", AppSession.PeriodAdmissionID))[0];
            
            vRegistration entity = BusinessLayer.GetvRegistrationList(filterExpression)[0];
            String text = divReportHeader.InnerHtml;
            text = text.Replace("{Periode}", pa.SchoolPeriodName);
            divReportHeader.InnerHtml = text;
                
            text = divDataSiswa.InnerHtml;
            text = text.Replace("{ProspectiveStudentName}",entity.ProspectiveStudentName);
            text = text.Replace("{Address}",entity.HomeAddress);
            text = text.Replace("{PhoneNo}",entity.PhoneNo1);
            text = text.Replace("{Class}",entity.Grade);
            text = text.Replace("{ProspectiveStudentCode}", entity.ProspectiveStudentCode);
            divDataSiswa.InnerHtml = text;
            List<vStudentFeeComp> lstStudentFee = BusinessLayer.GetvStudentFeeCompList(String.Format("{0} AND IsDeleted = 0",filterExpression));
            rptStudentFeeComp.DataSource = lstStudentFee;
            rptStudentFeeComp.DataBind();

            lstRegFee = BusinessLayer.GetStudentFeeList(String.Format("RegistrationID = {0} AND IsDeleted = 0", entity.RegistrationID));

            text = tdTotalLineAmount.InnerHtml;
            text = text.Replace("{TotalLineAmount}", lstStudentFee.Sum(x => x.TotalAmount).ToString("N2"));
            tdTotalLineAmount.InnerHtml = text;

            rptPayment.DataSource = lstStudentFee;
            rptPayment.DataBind();

            text = divPageFooter.InnerHtml;
            vSite site = BusinessLayer.GetvSiteList(String.Format("SiteID = '{0}'",AppSession.UserLogin.SiteID))[0];
            text = text.Replace("{City}", site.City);
            text = text.Replace("{DateNow}", DateTime.Now.ToString(Constant.FormatString.DATE_FORMAT));
            divPageFooter.InnerHtml = text;
        }

        protected void rptPayment_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                vStudentFeeComp entity = (vStudentFeeComp)e.Item.DataItem;
                Repeater rptPaymentDt = (Repeater)e.Item.FindControl("rptPaymentDt");
                rptPaymentDt.DataSource = lstRegFee.Where(x => x.StudentFeeCompID == entity.StudentFeeCompID);
                rptPaymentDt.DataBind();
            }
        }
    }
}