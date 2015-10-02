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
            //String DueDate = String.Format("{0}{1}", cboYear.Value, Convert.ToInt32(cboMonth.Value).ToString("00"));
            String filterExpression = String.Format("StudentID IN (SELECT StudentID FROM Student WHERE SiteID = '{0}')", cboSite.Value);
            if (chkNotPaid.Checked)
                filterExpression += String.Format(" AND GCTransactionStatus NOT IN ('{0}','{1}')", Constant.TransactionStatus.CLOSED, Constant.TransactionStatus.VOID);
            else
                filterExpression += String.Format(" AND GCTransactionStatus != '{0}'", Constant.TransactionStatus.VOID);
            if(tacSchoolClass.Value != "")
                filterExpression += string.Format(" AND StudentID IN (SELECT StudentID FROM ClassStudent WHERE SchoolClassID = {0})", tacSchoolClass.Value);
            if (hdnFilterExpressionQuickSearch.Value != "")
                filterExpression += string.Format(" AND {0}", hdnFilterExpressionQuickSearch.Value);
            
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = GetFilterExpression();
            
            //if (isCountPageCount)
            //{
            //    rowCount = BusinessLayer.GetvARInvoiceHdRowCount(filterExpression);
            //    pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            //}

            List<vARInvoiceHd> lstEntity = BusinessLayer.GetvARInvoiceHdList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
            var lstObject = (from grp in lstEntity
                             group grp by new { grp.StudentID, grp.StudentCode, grp.StudentName } into NewGrp
                             select new
                             {
                                 StudentID = NewGrp.Key.StudentID,
                                 StudentCode = NewGrp.Key.StudentCode,
                                 StudentName = NewGrp.Key.StudentName,
                                 TotalClaimedAmount = NewGrp.Sum(x => x.GCTransactionStatus == Constant.TransactionStatus.CLOSED ? 0 : x.TotalClaimedAmount),
                                 lstARInvoiceID = String.Join(",", NewGrp.Select(x => x.ARInvoiceID))
                             }).ToList();

            grdView.DataSource = lstObject;
            grdView.DataBind();

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