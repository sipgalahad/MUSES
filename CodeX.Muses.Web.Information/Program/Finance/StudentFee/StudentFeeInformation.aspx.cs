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
    public partial class StudentFeeInformation : BasePageList
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.STUDENT_FEE;
        }

        protected string OnGetSchoolPeriodNowFilterExpression()
        {
            return string.Format("GCSchoolPeriodStatus != '{0}' AND StartDate <= '{1}' AND EndDate >= '{1}'", Constant.SchoolPeriodStatus.VOID, DateTime.Now.ToString("yyyyMMdd"));
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<vSite> lstSite = BusinessLayer.GetvSiteList(String.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsHeader = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<vSite>(cboSite, lstSite, "SiteName", "SiteID");
            cboSite.SelectedIndex = 0;
        }

        #region HTML Getter
        public String OnGetStudentFilterExpression() 
        {
            return String.Format("GCStudentStatus = '{1}' AND IsDeleted = 0", AppSession.UserLogin.SiteID, Constant.StudentStatus.ACTIVE);
        }
        public String OnGetSchoolPeriodFilterExpression() 
        {
            return String.Format("SiteID = '{0}' AND GCSchoolPeriodStatus = '{1}'", AppSession.UserLogin.SiteID, Constant.SchoolPeriodStatus.START);
        }
        #endregion

        private string GetFilterExpression()
        {
            if (tacStudent.Value == "")
                return "1 = 0";
            string filterExpression = String.Format("StudentID = {0} AND GCAdmissionPaymentPeriod IN ('{1}','{2}') AND IsDeleted = 0 AND SchoolPeriodID = {3}", tacStudent.Value, Constant.AdmissionPaymentPeriod.TAHUNAN, Constant.AdmissionPaymentPeriod.SEKALI_BAYAR, hdnSchoolPeriodID.Value);
            return filterExpression;
        }
        private string GetFilterExpression2()
        {
            string filterExpression = String.Format("StudentID = {0} AND GCAdmissionPaymentPeriod = '{1}' AND IsDeleted = 0 AND SchoolPeriodID = {2}", tacStudent.Value, Constant.AdmissionPaymentPeriod.BULANAN, hdnSchoolPeriodID.Value);
            return filterExpression;
        }

        List<vStudentFeeDt> lstStudentFeeDt = null;
        List<vStudentFee> lstStudentFee = null;
        public void BindGridView()
        {
            String filterExpression = GetFilterExpression();
            lstStudentFee = BusinessLayer.GetvStudentFeeList(filterExpression);
            String lstStudentFeeID = String.Join(",", lstStudentFee.Select(x => x.StudentFeeID));
            if (lstStudentFeeID != "") 
            {
                lstStudentFeeDt = BusinessLayer.GetvStudentFeeDtList(String.Format("StudentFeeID IN ({0}) AND IsDeleted = 0", lstStudentFeeID));
                rptStudentFeeComp.DataSource = lstStudentFee;
                rptStudentFeeComp.DataBind();
            }

            filterExpression = GetFilterExpression2();
            List<vStudentFeeComp> lstStudentFeeComp = BusinessLayer.GetvStudentFeeCompList(filterExpression);
            String lstStudentFeeCompID = String.Join(",", lstStudentFeeComp.Select(x => x.StudentFeeCompID));
            if (lstStudentFeeCompID != "")
            {
                lstStudentFee = BusinessLayer.GetvStudentFeeList(string.Format("StudentFeeCompID IN ({0}) AND IsDeleted = 0", lstStudentFeeCompID));
                rptStudentFeeComp2.DataSource = lstStudentFeeComp;
                rptStudentFeeComp2.DataBind();
            }
        }

        protected void rptStudentFeeComp_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vStudentFee entity = e.Item.DataItem as vStudentFee;
                List<vStudentFeeDt> lstTemp = lstStudentFeeDt.Where(x => x.StudentFeeID == entity.StudentFeeID).ToList();
                Repeater rptStudentFee = (Repeater)e.Item.FindControl("rptStudentFee");
                rptStudentFee.DataSource = lstTemp;
                rptStudentFee.DataBind();

                if (lstTemp.Count() > 0)
                {
                    decimal paymentAmount = lstStudentFeeDt.Where(x => x.StudentFeeID == entity.StudentFeeID && x.IsPaid).Sum(x => x.StudentAmount);
                    Decimal totalAmount = entity.StudentAmount - paymentAmount;

                    TextBox txtTotalAmount = e.Item.FindControl("txtTotalAmount") as TextBox;
                    TextBox txtTotalPaymentAmount = e.Item.FindControl("txtTotalPaymentAmount") as TextBox;
                    TextBox txtRemainingAmount = e.Item.FindControl("txtRemainingAmount") as TextBox;
                    txtRemainingAmount.Attributes.Add("class", String.Format("txtRemainingAmount{0} txtRemainingAmount txtCurrency", entity.StudentFeeID));
                    txtTotalAmount.Text = entity.StudentAmount.ToString();
                    txtTotalPaymentAmount.Text = paymentAmount.ToString();
                    txtRemainingAmount.Text = totalAmount.ToString();
                }
                else
                {
                    HtmlTableRow trDataHeader = e.Item.FindControl("trDataHeader") as HtmlTableRow;
                    HtmlTableRow trDataHeader1 = e.Item.FindControl("trDataHeader1") as HtmlTableRow;
                    HtmlTableRow trDataHeader2 = e.Item.FindControl("trDataHeader2") as HtmlTableRow;
                    trDataHeader.Style.Add("display", "none");
                    trDataHeader1.Style.Add("display", "none");
                    trDataHeader2.Style.Add("display", "none");

                    HtmlTableRow trDataDetail = e.Item.FindControl("trDataDetail") as HtmlTableRow;
                    trDataDetail.Style.Add("display", "none");
                }
            }
        }

        protected void rptStudentFeeComp2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vStudentFeeComp entity = e.Item.DataItem as vStudentFeeComp;
                List<vStudentFee> lstTemp = lstStudentFee.Where(x => x.StudentFeeCompID == entity.StudentFeeCompID).ToList();
                Repeater rptStudentFee = (Repeater)e.Item.FindControl("rptStudentFee");
                rptStudentFee.DataSource = lstTemp;
                rptStudentFee.DataBind();

                if (lstTemp.Count() > 0)
                {
                    TextBox txtTotalAmount = e.Item.FindControl("txtTotalAmount") as TextBox;
                    txtTotalAmount.Text = entity.TotalAmount.ToString();
                }
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}