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

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        private string GetFilterExpression()
        {
            String filterExpression = String.Format("SiteID = '{0}' AND GCStudentStatus = '{1}' AND IsDeleted = 0", cboSite.Value, Constant.StudentStatus.ACTIVE);
            if (tacSchoolClass.Value != "")
                filterExpression += string.Format(" AND SchoolClassID = {0}", tacSchoolClass.Value);
            if (hdnFilterExpressionQuickSearch.Value != "")
                filterExpression += string.Format(" AND {0}", hdnFilterExpressionQuickSearch.Value);
            if (chkNotPaid.Checked)
                filterExpression += string.Format(" AND StudentID IN (SELECT StudentID FROM ARInvoiceHd WHERE StudentID IS NOT NULL AND GCTransactionStatus NOT IN ('{0}','{1}'))", Constant.TransactionStatus.CLOSED, Constant.TransactionStatus.VOID);

            return filterExpression;
        }

        List<ARInvoiceHd> lstARInvoiceHd = null;
        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = GetFilterExpression();

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvStudentRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vStudent> lstEntity = BusinessLayer.GetvStudentList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "VirtualAccountNo");
            if (lstEntity.Count > 0)
            {
                string lstStudentID = string.Join(",", lstEntity.Select(p => p.StudentID).ToList());
                lstARInvoiceHd = BusinessLayer.GetARInvoiceHdList(string.Format("StudentID IN ({0}) AND GCTransactionStatus NOT IN ('{1}','{2}')", lstStudentID, Constant.TransactionStatus.CLOSED, Constant.TransactionStatus.VOID));
            }
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vStudent entity = (vStudent)e.Row.DataItem;
                Decimal totalClaimedAmount = lstARInvoiceHd.Where(p => p.StudentID == entity.StudentID).Sum(p => p.TotalClaimedAmount);
                HtmlGenericControl lblClaimedAmount = (HtmlGenericControl)e.Row.FindControl("lblClaimedAmount");
                lblClaimedAmount.InnerHtml = totalClaimedAmount.ToString("N");
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
    }
}