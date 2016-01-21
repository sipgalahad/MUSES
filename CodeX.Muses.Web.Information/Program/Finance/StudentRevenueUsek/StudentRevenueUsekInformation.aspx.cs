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
    public partial class StudentRevenueUsekInformation : BasePageList
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.STUDENT_REVENUE_USEK_INFO;
        }

        List<vSite> lstSite = null;
        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            lstSite = BusinessLayer.GetvSiteList(String.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsHeader = 0", AppSession.UserLogin.SiteID));

            List<SchoolPeriod> lstSchoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SiteID = '{0}' AND GCSchoolPeriodStatus != '{1}'", lstSite.FirstOrDefault().SiteID, Constant.SchoolPeriodStatus.VOID));
            Methods.SetComboBoxField<SchoolPeriod>(cboSchoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
            SchoolPeriod selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);
            if (selectedSchoolPeriod == null)
            {
                cboSchoolPeriod.SelectedIndex = 0;
                selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault();
            }
            else
                cboSchoolPeriod.Value = selectedSchoolPeriod.SchoolPeriodID.ToString();
            BindGridView();
        }

        List<DateTime> lstDateTime = null;
        List<vStudentUsekSummary> lstUsek = null;
        List<vStudentUkegUpembSummary> lstUkegUpemb = null;
        SchoolPeriod selectedSchoolPeriod = null;
        #region Bind Grid View
        private void BindGridView()
        {
            if (lstSite == null)
                lstSite = BusinessLayer.GetvSiteList(String.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsHeader = 0", AppSession.UserLogin.SiteID));

            selectedSchoolPeriod = BusinessLayer.GetSchoolPeriod(Convert.ToInt32(cboSchoolPeriod.Value));

            lstDateTime = new List<DateTime>();
            for (var dt = selectedSchoolPeriod.StartDate; dt <= selectedSchoolPeriod.EndDate; dt = dt.AddMonths(1))
            {
                lstDateTime.Add(dt);
            }

            lstUsek = BusinessLayer.GetvStudentUsekSummaryList("");
            lstUkegUpemb = BusinessLayer.GetvStudentUkegUpembSummaryList(string.Format("StartDate = '{0}' AND EndDate = '{1}'", selectedSchoolPeriod.StartDate, selectedSchoolPeriod.EndDate));

            rptSite.DataSource = lstSite;
            rptSite.DataBind();

            rptMonth.DataSource = lstDateTime;
            rptMonth.DataBind();

            rptStudentFeeMonthTotal.DataSource = lstDateTime;
            rptStudentFeeMonthTotal.DataBind();

            tdStudentFeeUkeg.InnerHtml = lstUkegUpemb.Where(p => p.StudentFeeCompTypeID == 3).Sum(p => p.TransactionAmount).ToString("N");
            tdStudentFeeUpemb.InnerHtml = lstUkegUpemb.Where(p => p.StudentFeeCompTypeID == 1).Sum(p => p.TransactionAmount).ToString("N");
        }

        protected void rptSite_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vSite entity = (vSite)e.Item.DataItem;

                Repeater rptStudentFeeMonth = (Repeater)e.Item.FindControl("rptStudentFeeMonth");
                rptStudentFeeMonth.DataSource = lstDateTime;
                rptStudentFeeMonth.DataBind();

                HtmlGenericControl lblStudentFeeUkeg = (HtmlGenericControl)e.Item.FindControl("lblStudentFeeUkeg");
                HtmlGenericControl lblStudentFeeUpemb = (HtmlGenericControl)e.Item.FindControl("lblStudentFeeUpemb");

                decimal ukeg = 0;
                decimal upemb = 0;

                vStudentUkegUpembSummary entityUkeg = lstUkegUpemb.FirstOrDefault(p => p.SiteID == entity.SiteID && p.StudentFeeCompTypeID == 3);
                if (entityUkeg != null)
                    ukeg = entityUkeg.TransactionAmount;
                vStudentUkegUpembSummary entityUpemb = lstUkegUpemb.FirstOrDefault(p => p.SiteID == entity.SiteID && p.StudentFeeCompTypeID == 1);
                if (entityUpemb != null)
                    upemb = entityUpemb.TransactionAmount;

                lblStudentFeeUkeg.Attributes.Add("siteid", entity.SiteID);
                lblStudentFeeUkeg.Attributes.Add("startdate", selectedSchoolPeriod.StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT));
                lblStudentFeeUkeg.Attributes.Add("enddate", selectedSchoolPeriod.EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT));
                lblStudentFeeUkeg.InnerHtml = ukeg.ToString("N");
                lblStudentFeeUpemb.Attributes.Add("siteid", entity.SiteID);
                lblStudentFeeUpemb.Attributes.Add("startdate", selectedSchoolPeriod.StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT));
                lblStudentFeeUpemb.Attributes.Add("enddate", selectedSchoolPeriod.EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT));
                lblStudentFeeUpemb.InnerHtml = upemb.ToString("N");
            }
        }

        protected void rptStudentFeeMonth_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                DateTime dt = (DateTime)e.Item.DataItem;
                vSite site = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vSite;

                vStudentUsekSummary usek = lstUsek.FirstOrDefault(p => p.SiteID == site.SiteID && p.TransactionMonth == dt.Month && p.TransactionYear == dt.Year);
                HtmlGenericControl lblStudentFeeMonth = (HtmlGenericControl)e.Item.FindControl("lblStudentFeeMonth");
                decimal transactionAmount = 0;
                if (usek != null)
                    transactionAmount = usek.TransactionAmount;
                lblStudentFeeMonth.Attributes.Add("siteid", site.SiteID);
                lblStudentFeeMonth.Attributes.Add("month", dt.Month.ToString());
                lblStudentFeeMonth.Attributes.Add("year", dt.Year.ToString());

                lblStudentFeeMonth.InnerHtml = transactionAmount.ToString("N");
            }
        }

        protected void rptStudentFeeMonthTotal_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                DateTime dt = (DateTime)e.Item.DataItem;

                decimal transactionAmount = lstUsek.Where(p => p.TransactionMonth == dt.Month && p.TransactionYear == dt.Year).Sum(p => p.TransactionAmount);
                HtmlTableCell tdStudentFeeMonth = (HtmlTableCell)e.Item.FindControl("tdStudentFeeMonth");
                tdStudentFeeMonth.InnerHtml = transactionAmount.ToString("N");
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
            h41.InnerHtml = "PENDAPATAN UANG SEKOLAH";
            div.Controls.Add(h41);
            div2.InnerHtml = hdnExportControl.Value;
            div.Controls.Add(div2);
            return div;
        }
    }
}