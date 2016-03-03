using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;
using DevExpress.Web.ASPxEditors;
using System.Globalization;
using CodeX.Data.Core.Dal;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.Information.Program
{
    public partial class StudentFeeStatusSummaryInformation : BasePageList
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.STUDENT_FEE_STATUS_SUMMARY;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<vSite> lstSite = BusinessLayer.GetvSiteList(String.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsHeader = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<vSite>(cboSite, lstSite, "SiteName", "SiteID");
            cboSite.SelectedIndex = 0;

            #region Data Month
            cboMonth.DataSource = Enumerable.Range(1, 12).Select(a => new
            {
                MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(a),
                MonthNumber = a
            });
            cboMonth.TextField = "MonthName";
            cboMonth.ValueField = "MonthNumber";
            cboMonth.EnableCallbackMode = false;
            cboMonth.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboMonth.DropDownStyle = DropDownStyle.DropDownList;
            cboMonth.DataBind();
            cboMonth.Value = DateTime.Now.Month.ToString();

            cboYear.DataSource = Enumerable.Range(DateTime.Now.Year - 99, 100).Reverse();
            cboYear.EnableCallbackMode = false;
            cboYear.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboYear.DropDownStyle = DropDownStyle.DropDownList;
            cboYear.DataBind();
            cboYear.SelectedIndex = 0;
            #endregion

            hdnSelectedYear.Value = cboYear.Value.ToString();
            hdnSelectedMonth.Value = cboMonth.Value.ToString();
            hdnSiteName.Value = cboSite.Text;
        }

        public void BindGridView()
        {
            List<vStudentFeeStatusPerClassSummary> lstEntity = BusinessLayer.GetvStudentFeeStatusPerClassSummaryList(string.Format("TransactionMonth = {0} AND TransactionYear = {1} AND SiteID = '{2}' ORDER BY SchoolClassCode", cboMonth.Value, cboYear.Value, cboSite.Value));
            rptView.DataSource = lstEntity;
            rptView.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        public override Control OnGetExportControl()
        {
            List<vStudentFeeStatusPerClassSummary> lstEntity = BusinessLayer.GetvStudentFeeStatusPerClassSummaryList(string.Format("TransactionMonth = {0} AND TransactionYear = {1} AND SiteID = '{2}' ORDER BY SchoolClassCode", hdnSelectedMonth.Value, hdnSelectedYear.Value, hdnSiteID.Value));
            rptView.DataSource = lstEntity;
            rptView.DataBind();

            HtmlGenericControl div = new HtmlGenericControl("DIV");
            HtmlGenericControl h4 = new HtmlGenericControl("h4");
            HtmlGenericControl h42 = new HtmlGenericControl("h42");
            HtmlGenericControl div2 = new HtmlGenericControl("DIV");
            h4.InnerHtml = String.Format("Site : {0}", hdnSiteName.Value);
            h42.InnerHtml = String.Format("Periode : {0} - {1}", hdnSelectedYear.Value, hdnSelectedMonth.Value);
            div2.InnerHtml = hdnExportControl.Value;
            div.Controls.Add(h4);
            div.Controls.Add(h42);
            div.Controls.Add(div2);
            return div;
        }
    }
}