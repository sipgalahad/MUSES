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
    public partial class StudentPaymentSummaryInformation : BasePageList
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.STUDENT_PAYMENT_SUMMARY_INFO;
        }

        protected string OnGetClassStudyTypeRegular()
        {
            return Constant.ClassStudyType.REGULAR;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
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

            BindGridView();
        }

        List<StudentFeeCompType> lstStudentFeeCompType = null;
        List<Site> lstSite = null;
        List<GetStudentReceiveSummary> lstStudentReceive = null;

        class CStudentFeeCompTypeTotal
        {
            public string SiteID { get; set; }
            public int StudentFeeCompTypeID { get; set; }
            public decimal TotalAmount { get; set; }
        }
        List<CStudentFeeCompTypeTotal> lstStudentFeeCompTypeTotal = null;

        #region Bind Grid View
        private void BindGridView()
        {
            hdnTempPeriodText.Value = string.Format("BULAN {0} {1}", cboMonth.Text, cboYear.Value);

            lstSite = BusinessLayer.GetSiteList(String.Format("ParentID = '{0}' OR SiteID = '{0}'", AppSession.UserLogin.SiteID));
            lstStudentFeeCompType = BusinessLayer.GetStudentFeeCompTypeList(string.Format("IsDeleted = 0"));
            rptSite.DataSource = lstSite;
            rptSite.DataBind();

            lstStudentFeeCompTypeTotal = new List<CStudentFeeCompTypeTotal>();

            foreach (Site site in lstSite)
            {
                foreach (StudentFeeCompType studentFeeCompType in lstStudentFeeCompType)
                {
                    lstStudentFeeCompTypeTotal.Add(new CStudentFeeCompTypeTotal { SiteID = site.SiteID, StudentFeeCompTypeID = studentFeeCompType.StudentFeeCompTypeID, TotalAmount = 0 });
                }
            }

            rptStudentFeeCompType.DataSource = lstStudentFeeCompType;
            rptStudentFeeCompType.DataBind();

            rptSiteGrandTotal.DataSource = lstSite;
            rptSiteGrandTotal.DataBind();

            tdStudentFeeCompTypeGrandTotal.InnerHtml = lstStudentReceive.Sum(p => p.TotalAmount).ToString("N");
        }

        protected void rptStudentFeeCompType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                StudentFeeCompType entity = (StudentFeeCompType)e.Item.DataItem;

                lstStudentReceive = BusinessLayer.GetStudentReceiveSummary(entity.SiteID, Convert.ToInt32(cboYear.Value), Convert.ToInt32(cboMonth.Value));

                Repeater rptSiteDt1 = (Repeater)e.Item.FindControl("rptSiteDt1");
                rptSiteDt1.DataSource = lstSite;
                rptSiteDt1.DataBind();
                Repeater rptSiteDt2 = (Repeater)e.Item.FindControl("rptSiteDt2");
                rptSiteDt2.DataSource = lstSite;
                rptSiteDt2.DataBind();
                Repeater rptSiteDt3 = (Repeater)e.Item.FindControl("rptSiteDt3");
                rptSiteDt3.DataSource = lstSite;
                rptSiteDt3.DataBind();
                Repeater rptSiteDt4 = (Repeater)e.Item.FindControl("rptSiteDt4");
                rptSiteDt4.DataSource = lstSite;
                rptSiteDt4.DataBind();
                Repeater rptSiteDt5 = (Repeater)e.Item.FindControl("rptSiteDt5");
                rptSiteDt5.DataSource = lstSite;
                rptSiteDt5.DataBind();

                HtmlTableCell tdTotalThisMonth = (HtmlTableCell)e.Item.FindControl("tdTotalThisMonth");
                HtmlTableCell tdTotalDP = (HtmlTableCell)e.Item.FindControl("tdTotalDP");
                HtmlTableCell tdTotalProspectiveStudent = (HtmlTableCell)e.Item.FindControl("tdTotalProspectiveStudent");
                HtmlTableCell tdTotalAR = (HtmlTableCell)e.Item.FindControl("tdTotalAR");
                HtmlTableCell tdTotalStudentFeeCompType = (HtmlTableCell)e.Item.FindControl("tdTotalStudentFeeCompType");
                tdTotalThisMonth.InnerHtml = lstStudentReceive.Where(p => p.Code == "ThisMonth" && p.StudentFeeCompTypeID == entity.StudentFeeCompTypeID).Sum(p => p.TotalAmount).ToString("N");
                tdTotalDP.InnerHtml = lstStudentReceive.Where(p => p.Code == "DownPayment" && p.StudentFeeCompTypeID == entity.StudentFeeCompTypeID).Sum(p => p.TotalAmount).ToString("N");
                tdTotalProspectiveStudent.InnerHtml = lstStudentReceive.Where(p => p.Code == "ProspectiveStudent" && p.StudentFeeCompTypeID == entity.StudentFeeCompTypeID).Sum(p => p.TotalAmount).ToString("N");
                tdTotalAR.InnerHtml = lstStudentReceive.Where(p => p.Code == "ARStudent" && p.StudentFeeCompTypeID == entity.StudentFeeCompTypeID).Sum(p => p.TotalAmount).ToString("N");
                tdTotalStudentFeeCompType.InnerHtml = lstStudentReceive.Where(p => p.StudentFeeCompTypeID == entity.StudentFeeCompTypeID).Sum(p => p.TotalAmount).ToString("N");

                Repeater rptSiteTotal = (Repeater)e.Item.FindControl("rptSiteTotal");
                rptSiteTotal.DataSource = lstSite;
                rptSiteTotal.DataBind();
            }
        }

        private void rptSiteDt_ItemDataBound(object sender, RepeaterItemEventArgs e, string type)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Site entity = (Site)e.Item.DataItem;
                StudentFeeCompType studentFeeCompType = ((RepeaterItem)e.Item.Parent.Parent).DataItem as StudentFeeCompType;

                CStudentFeeCompTypeTotal studentFeeCompTypeTotal = lstStudentFeeCompTypeTotal.FirstOrDefault(p => p.SiteID == entity.SiteID && p.StudentFeeCompTypeID == studentFeeCompType.StudentFeeCompTypeID);

                decimal totalAmount = 0;
                GetStudentReceiveSummary studentReceive = lstStudentReceive.FirstOrDefault(p => p.StudentFeeCompTypeID == studentFeeCompType.StudentFeeCompTypeID && p.Code == type);
                if (studentReceive != null)
                {
                    totalAmount = studentReceive.TotalAmount;
                    studentFeeCompTypeTotal.TotalAmount += totalAmount;
                }
                HtmlTableCell tdStudentReceiveAmount = (HtmlTableCell)e.Item.FindControl("tdStudentReceiveAmount");
                tdStudentReceiveAmount.InnerHtml = totalAmount.ToString("N");
            }
        }

        protected void rptSiteDt2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            rptSiteDt_ItemDataBound(sender, e, "ThisMonth");
        }

        protected void rptSiteDt3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            rptSiteDt_ItemDataBound(sender, e, "DownPayment");
        }

        protected void rptSiteDt4_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            rptSiteDt_ItemDataBound(sender, e, "ProspectiveStudent");
        }

        protected void rptSiteDt5_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            rptSiteDt_ItemDataBound(sender, e, "ARStudent");
        }

        protected void rptSiteTotal_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Site entity = (Site)e.Item.DataItem;
                StudentFeeCompType studentFeeCompType = ((RepeaterItem)e.Item.Parent.Parent).DataItem as StudentFeeCompType;

                HtmlTableCell tdStudentFeeCompTypeTotal = (HtmlTableCell)e.Item.FindControl("tdStudentFeeCompTypeTotal");
                CStudentFeeCompTypeTotal studentFeeCompTypeTotal = lstStudentFeeCompTypeTotal.FirstOrDefault(p => p.SiteID == entity.SiteID && p.StudentFeeCompTypeID == studentFeeCompType.StudentFeeCompTypeID);
                tdStudentFeeCompTypeTotal.InnerHtml = studentFeeCompTypeTotal.TotalAmount.ToString("N");
            }
        }

        protected void rptSiteGrandTotal_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Site entity = (Site)e.Item.DataItem;

                HtmlTableCell tdStudentFeeCompTypeTotal = (HtmlTableCell)e.Item.FindControl("tdStudentFeeCompTypeTotal");
                tdStudentFeeCompTypeTotal.InnerHtml = lstStudentFeeCompTypeTotal.Where(p => p.SiteID == entity.SiteID).Sum(p => p.TotalAmount).ToString("N");
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion

        public override Control OnGetExportControl(ref bool isShowTitle)
        {
            isShowTitle = false;
            HtmlGenericControl div = new HtmlGenericControl("DIV");
            HtmlGenericControl div2 = new HtmlGenericControl("DIV");
            HtmlGenericControl h41 = new HtmlGenericControl("DIV");
            HtmlGenericControl h42 = new HtmlGenericControl("DIV");
            HtmlGenericControl h43 = new HtmlGenericControl("DIV");
            h41.InnerHtml = "PENERIMAAN UANG SEKOLAH, U.KEGIATAN & U.PEMBANGUNAN";
            h42.InnerHtml = hdnExportPeriodText.Value;
            h43.InnerHtml = string.Format("Unit {0}", AppSession.UserLogin.SiteName);
            div.Controls.Add(h41);
            div.Controls.Add(h42);
            div.Controls.Add(h43);
            div2.InnerHtml = hdnExportControl.Value;
            div.Controls.Add(div2);
            return div;
        }
    }
}