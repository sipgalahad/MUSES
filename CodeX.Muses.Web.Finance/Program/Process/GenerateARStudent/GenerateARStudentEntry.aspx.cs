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
using System.Web.UI.HtmlControls;
namespace CodeX.Muses.Web.Finance.Program
{
    public partial class GenerateARStudentEntry : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Finance.GENERATE_AR_INVOICE_STUDENT;
        }

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

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<vSite> lstSite = BusinessLayer.GetvSiteList(String.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsHeader = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<vSite>(cboSite, lstSite, "SiteName", "SiteID");
            cboSite.SelectedIndex = 0;

            DateTime date = DateTime.Now.AddMonths(1);
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
            cboMonth.Value = date.Month.ToString();

            cboYear.DataSource = Enumerable.Range(DateTime.Now.Year - 1, 2).Reverse();
            cboYear.EnableCallbackMode = false;
            cboYear.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboYear.DropDownStyle = DropDownStyle.DropDownList;
            cboYear.DataBind();
            cboYear.Value = date.Year.ToString();

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        private string GetFilterExpression()
        {
            string filterExpression = string.Format("GCStudentStatus = '{0}'", Constant.StudentStatus.ACTIVE);
            if (hdnFilterExpressionQuickSearch.Value != "")
                filterExpression += string.Format(" AND {0}", hdnFilterExpressionQuickSearch.Value);
            if (tacSchoolClass.Value != "")
                filterExpression += string.Format(" AND SchoolClassID = {0}", tacSchoolClass.Value);
            else
                filterExpression += string.Format(" AND SiteID = '{0}'", cboSite.Value);
            return filterExpression;
        }

        private string[] lstID = null;
        List<vStudentFeeDt> lstStudentFeeDt = null;
        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvStudentRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            lstID = hdnSelectedValue.Value.Split(',');
            List<vStudent> lstEntity = BusinessLayer.GetvStudentList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "StudentName ASC");
            if (lstEntity.Count > 0)
            {
                string lstStudentID = string.Join(",", lstEntity.Select(p => p.StudentID).ToList());
                lstStudentFeeDt = BusinessLayer.GetvStudentFeeDtList(string.Format("StudentID IN ({0}) AND DueDate LIKE '{1}-{2}%' AND StudentFeeDtID NOT IN (SELECT StudentFeeDtID FROM vARInvoiceDt WHERE GCTransactionStatus != '{3}') AND IsPaid = 0", lstStudentID, cboYear.Value, cboMonth.Value.ToString().PadLeft(2, '0'), Constant.TransactionStatus.VOID));
            }
            else
                lstStudentFeeDt = new List<vStudentFeeDt>();

            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vStudent entity = (vStudent)e.Row.DataItem;
                decimal totalAmount = lstStudentFeeDt.Where(p => p.StudentID == entity.StudentID).Sum(p => p.TotalStudentAmount);
                HtmlGenericControl lblStudentAmount = (HtmlGenericControl)e.Row.FindControl("lblStudentAmount");
                lblStudentAmount.InnerHtml = totalAmount.ToString("N");

                CheckBox chkIsSelected = (CheckBox)e.Row.FindControl("chkIsSelected");
                if (totalAmount == 0)
                    chkIsSelected.Visible = false;
                if (lstID.Contains(entity.StudentID.ToString()))
                    chkIsSelected.Checked = true;
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

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            bool result = true;
            try
            {
                BusinessLayer.GenerateARInvoiceStudent(hdnSelectedValue.Value, cboSite.Value.ToString(), Convert.ToInt32(cboMonth.Value), Convert.ToInt32(cboYear.Value), AppSession.UserLogin.UserID, ctx);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }

            return result;
        }
    }
}