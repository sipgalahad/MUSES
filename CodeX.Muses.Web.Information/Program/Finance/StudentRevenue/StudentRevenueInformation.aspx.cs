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
    public partial class StudentRevenueInformation : BasePageList
    {
        protected int PageCount = 0;
        protected int RowCount = 0;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;     
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.STUDENT_REVENUE_INFO;
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
        List<GetStudentRevenue> lstStudentRevenue = null;

        class CStudentFeeCompTypeTotal
        {
            public string SiteID { get; set; }
            public int StudentFeeCompTypeID { get; set; }
            public decimal TotalAmount { get; set; }
        }
        List<CStudentFeeCompTypeTotal> lstStudentFeeCompTypeTotal = null;
        int totalStudentCount = 0;

        #region Bind Grid View
        private void BindGridView()
        {
            hdnTempPeriodText.Value = string.Format("BULAN {0} {1}", cboMonth.Text, cboYear.Value);
            totalStudentCount = 0;

            lstStudentFeeCompType = BusinessLayer.GetStudentFeeCompTypeList(string.Format("IsDeleted = 0"));
            rptStudentFeeCompType.DataSource = lstStudentFeeCompType;
            rptStudentFeeCompType.DataBind();

            lstStudentFeeCompTypeTotal = new List<CStudentFeeCompTypeTotal>();

            List<vSite> lstSite = BusinessLayer.GetvSiteList(String.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsHeader = 0", AppSession.UserLogin.SiteID));
            foreach (vSite site in lstSite)
            {
                foreach (StudentFeeCompType studentFeeCompType in lstStudentFeeCompType)
                {
                    lstStudentFeeCompTypeTotal.Add(new CStudentFeeCompTypeTotal { SiteID = site.SiteID, StudentFeeCompTypeID = studentFeeCompType.StudentFeeCompTypeID, TotalAmount = 0 });
                }
            }

            rptSite.DataSource = lstSite;
            rptSite.DataBind();

            rptStudentFeeCompTypeGrandTotal.DataSource = lstStudentFeeCompType;
            rptStudentFeeCompTypeGrandTotal.DataBind();

            tdTotalStudentCount.InnerHtml = totalStudentCount.ToString();
        }

        protected void rptSite_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vSite entity = (vSite)e.Item.DataItem;
                int studentCount = BusinessLayer.GetStudentRowCount(string.Format("SiteID = '{0}' AND GCStudentStatus = '{1}' AND IsDeleted = 0", entity.SiteID, Constant.StudentStatus.ACTIVE));
                totalStudentCount += studentCount;
                HtmlTableCell tdStudentCount = (HtmlTableCell)e.Item.FindControl("tdStudentCount");
                tdStudentCount.InnerHtml = studentCount.ToString();

                lstStudentRevenue = BusinessLayer.GetStudentRevenue(entity.SiteID, Convert.ToInt32(cboYear.Value), Convert.ToInt32(cboMonth.Value));

                Repeater rptStudentFeeCompTypeDt = (Repeater)e.Item.FindControl("rptStudentFeeCompTypeDt");
                rptStudentFeeCompTypeDt.DataSource = lstStudentFeeCompType;
                rptStudentFeeCompTypeDt.DataBind();

                Repeater rptStudentFeeCompTypeTotal = (Repeater)e.Item.FindControl("rptStudentFeeCompTypeTotal");
                rptStudentFeeCompTypeTotal.DataSource = lstStudentFeeCompType;
                rptStudentFeeCompTypeTotal.DataBind();
            }
        }

        protected void rptStudentFeeCompTypeDt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                StudentFeeCompType entity = (StudentFeeCompType)e.Item.DataItem;
                vSite site = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vSite;

                CStudentFeeCompTypeTotal studentFeeCompTypeTotal = lstStudentFeeCompTypeTotal.FirstOrDefault(p => p.SiteID == site.SiteID && p.StudentFeeCompTypeID == entity.StudentFeeCompTypeID);
                List<GetStudentRevenue> lstStudentRevenue1 = lstStudentRevenue.Where(p => p.StudentFeeCompTypeID == entity.StudentFeeCompTypeID).ToList();

                decimal studentAmount = 0;
                decimal payerAmount = 0;
                decimal prospectiveStudentAmount = 0;
                decimal scholarshipAmount = 0;
                GetStudentRevenue entityRevenueStudentAmount = lstStudentRevenue1.FirstOrDefault(p => p.Code == "StudentFeeDtStudent");
                GetStudentRevenue entityRevenueStudentScholarship = lstStudentRevenue1.FirstOrDefault(p => p.Code == "StudentScholarship");
                GetStudentRevenue entityRevenuePayerAmount = lstStudentRevenue1.FirstOrDefault(p => p.Code == "StudentFeeDtStudentPayer");
                GetStudentRevenue entityRevenueProspectiveStudentAmount = lstStudentRevenue1.FirstOrDefault(p => p.Code == "StudentFeeDtProspectiveStudent");
                GetStudentRevenue entityRevenueProspectiveStudentScholarship = lstStudentRevenue1.FirstOrDefault(p => p.Code == "ProspectiveStudentScholarship");
                if (entityRevenueStudentAmount != null)
                    studentAmount += entityRevenueStudentAmount.TotalAmount;
                if (entityRevenueStudentScholarship != null)
                {
                    studentAmount += entityRevenueStudentScholarship.TotalAmount;
                    scholarshipAmount += entityRevenueStudentScholarship.TotalAmount;
                }
                if (entityRevenuePayerAmount != null)
                    payerAmount += entityRevenuePayerAmount.TotalAmount;

                if (entityRevenueProspectiveStudentAmount != null)
                    prospectiveStudentAmount += entityRevenueProspectiveStudentAmount.TotalAmount;
                if (entityRevenueProspectiveStudentScholarship != null)
                {
                    prospectiveStudentAmount += entityRevenueProspectiveStudentScholarship.TotalAmount;
                    scholarshipAmount += entityRevenueProspectiveStudentScholarship.TotalAmount;
                }

                HtmlGenericControl divStudentAmount = (HtmlGenericControl)e.Item.FindControl("divStudentAmount");
                HtmlGenericControl divPayerAmount = (HtmlGenericControl)e.Item.FindControl("divPayerAmount");
                HtmlGenericControl divProspectiveStudentAmount = (HtmlGenericControl)e.Item.FindControl("divProspectiveStudentAmount");
                HtmlGenericControl divScholarshipAmount = (HtmlGenericControl)e.Item.FindControl("divScholarshipAmount");
                divStudentAmount.InnerHtml = studentAmount.ToString("N");
                divPayerAmount.InnerHtml = payerAmount.ToString("N");
                divProspectiveStudentAmount.InnerHtml = prospectiveStudentAmount.ToString("N");
                divScholarshipAmount.InnerHtml = scholarshipAmount.ToString("N");

                studentFeeCompTypeTotal.TotalAmount += (studentAmount + payerAmount + prospectiveStudentAmount - scholarshipAmount);
            }
        }

        protected void rptStudentFeeCompTypeTotal_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                StudentFeeCompType entity = (StudentFeeCompType)e.Item.DataItem;
                vSite site = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vSite;

                HtmlTableCell tdStudentFeeCompTypeTotal = (HtmlTableCell)e.Item.FindControl("tdStudentFeeCompTypeTotal");
                CStudentFeeCompTypeTotal studentFeeCompTypeTotal = lstStudentFeeCompTypeTotal.FirstOrDefault(p => p.SiteID == site.SiteID && p.StudentFeeCompTypeID == entity.StudentFeeCompTypeID);
                tdStudentFeeCompTypeTotal.InnerHtml = studentFeeCompTypeTotal.TotalAmount.ToString("N");
            }
        }

        protected void rptStudentFeeCompTypeGrandTotal_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                StudentFeeCompType entity = (StudentFeeCompType)e.Item.DataItem;

                HtmlTableCell tdStudentFeeCompTypeTotal = (HtmlTableCell)e.Item.FindControl("tdStudentFeeCompTypeTotal");
                tdStudentFeeCompTypeTotal.InnerHtml = lstStudentFeeCompTypeTotal.Where(p => p.StudentFeeCompTypeID == entity.StudentFeeCompTypeID).Sum(p => p.TotalAmount).ToString("N");
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
            HtmlGenericControl h42 = new HtmlGenericControl("DIV");
            HtmlGenericControl h43 = new HtmlGenericControl("DIV");
            h41.InnerHtml = "PENDAPATAN UANG SEKOLAH, U.KEGIATAN & U.PEMBANGUNAN";
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