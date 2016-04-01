using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Data.Model;
using CodeX.Web.Common;
using CodeX.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;
using System.Text;
using CodeX.Data.Core.Dal;
using System.Web.UI.HtmlControls;
using CodeX.Web.CommonLibs.MasterPage;
using CodeX.Common;
using System.Globalization;
using DevExpress.Web.ASPxEditors;

namespace CodeX.Muses.Web.Information.Program
{
    public partial class StudentBillInformation : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.STUDENT_BILL_INFORMATION;
        }

        #region HTML Getter
        protected string OnGetClassStudyTypeRegular()
        {
            return Constant.ClassStudyType.REGULAR;
        }

        protected string OnGetSchoolPeriodNowFilterExpression()
        {
            return string.Format("GCSchoolPeriodStatus != '{0}' AND StartDate <= '{1}' AND EndDate >= '{1}'", Constant.SchoolPeriodStatus.VOID, DateTime.Now.ToString("yyyyMMdd"));
        }

        protected string OnGetPeriodSectionFilterExpression()
        {
            return string.Format("GCPeriodSectionStatus != '{0}'", Constant.SchoolPeriodStatus.VOID);
        }
        #endregion

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<vSite> lstSite = BusinessLayer.GetvSiteList(String.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsHeader = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<vSite>(cboSite, lstSite, "SiteName", "SiteID");
            cboSite.SelectedIndex = 0;

            txtDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        private string GetFilterExpression()
        {
            String filterExpression = String.Format("SiteID = '{0}' AND GCStudentStatus = '{1}' AND IsDeleted = 0", cboSite.Value, Constant.StudentStatus.ACTIVE);
            if (tacSchoolClass.Value != "")
                filterExpression += string.Format(" AND SchoolClassID = {0}", tacSchoolClass.Value);
            else if (hdnSchoolPeriodEndDate.Value != "")
                filterExpression += string.Format(" AND (StartSchoolDate IS NULL OR StartSchoolDate <= '{0}')", Helper.GetDatePickerValue(hdnSchoolPeriodEndDate.Value).ToString("yyyyMMdd"));
            if (hdnFilterExpressionQuickSearch.Value != "")
                filterExpression += string.Format(" AND {0}", hdnFilterExpressionQuickSearch.Value);

            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = GetFilterExpression();

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvStudentRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<GetARStudentPerDate> lstEntity = null;
            List<vStudent> lstStudent = BusinessLayer.GetvStudentList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "VirtualAccountNo");
            if (lstStudent.Count > 0)
            {
                string lstStudentID = string.Join(",", lstStudent.Select(p => p.StudentID).ToList());
                lstEntity = BusinessLayer.GetARStudentPerDate(false, lstStudentID, Helper.GetDatePickerValue(txtDate.Text));
            }
            else
                lstEntity = new List<GetARStudentPerDate>();

            rptView.DataSource = lstEntity;
            rptView.DataBind();
        }

        protected void rptView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                GetARStudentPerDate entity = (GetARStudentPerDate)e.Item.DataItem;

                if (IsExportExcel)
                {
                    HtmlTableCell tdPrint = e.Item.FindControl("tdPrint") as HtmlTableCell;
                    tdPrint.InnerHtml = entity.Remarks;
                }
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount, ref rowCount);
                    result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool IsExportExcel = false;
        public override Control OnGetExportControl(ref bool isShowTitle, ref string fileName)
        {
            trFooter.Style.Remove("display");
            IsExportExcel = true;
            thPrint.InnerHtml = "Keterangan Uang Sekolah";
            thPrint.Style.Add("width", "200px");

            fileName = string.Format("InformasiTagihan{0}_{1}", Helper.GetDatePickerValue(Request.Form[txtDate.UniqueID]).ToString("yyyyMMdd"), Request.Form[hdnSiteName.UniqueID]);
            isShowTitle = false;


            String filterExpression = String.Format("SiteID = '{0}' AND GCStudentStatus = '{1}' AND IsDeleted = 0", Request.Form[hdnSiteID.UniqueID], Constant.StudentStatus.ACTIVE);
            if (tacSchoolClass.Value != "")
                filterExpression += string.Format(" AND SchoolClassID = {0}", tacSchoolClass.Value);
            if (Request.Form[hdnFilterExpressionQuickSearch.UniqueID] != "")
                filterExpression += string.Format(" AND {0}", Request.Form[hdnFilterExpressionQuickSearch.UniqueID]);

            filterExpression += " ORDER BY VirtualAccountNo";
            List<vStudent> lstStudent = BusinessLayer.GetvStudentList(filterExpression);

            List<GetARStudentPerDate> lstEntity = null;
            if (lstStudent.Count > 0)
            {
                string lstStudentID = string.Join(",", lstStudent.Select(p => p.StudentID).ToList());
                lstEntity = BusinessLayer.GetARStudentPerDate(false, lstStudentID, Helper.GetDatePickerValue(Request.Form[txtDate.UniqueID]));
            }
            else
                lstEntity = new List<GetARStudentPerDate>();

            rptView.DataSource = lstEntity;
            rptView.DataBind();

            divTotalPemb.InnerHtml = lstEntity.Sum(p => p.Col1).ToString("N");
            divTotalUsek.InnerHtml = lstEntity.Sum(p => p.Col2).ToString("N");
            divTotalKeg.InnerHtml = lstEntity.Sum(p => p.Col3).ToString("N");
            divTotalAll.InnerHtml = lstEntity.Sum(p => p.Total).ToString("N");

            HtmlGenericControl div = new HtmlGenericControl("DIV");
            HtmlGenericControl h4 = new HtmlGenericControl("h4");
            HtmlGenericControl h42 = new HtmlGenericControl("h4");

            HtmlGenericControl h1Title = new HtmlGenericControl("h2");
            h1Title.InnerHtml = "YAYASAN RICCI";
            div.Controls.Add(h1Title);

            HtmlGenericControl h2Title = new HtmlGenericControl("h2");
            h2Title.InnerHtml = "INFORMASI TAGIHAN BELUM DIBAYAR";
            div.Controls.Add(h2Title);


            h4.InnerHtml = String.Format("Tanggal : {0}", Helper.GetDatePickerValue(Request.Form[txtDate.UniqueID]).ToString(Constant.FormatString.DATE_FORMAT));
            h42.InnerHtml = String.Format("Unit : {0}", Request.Form[hdnSiteName.UniqueID]);
            div.Controls.Add(h4);
            div.Controls.Add(h42);
            div.Controls.Add(pnlGridView);
            return div;
        }
    }
}