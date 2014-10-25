using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;
using DevExpress.Web.ASPxEditors;
using System.Web.UI.HtmlControls;

namespace Codex.Muses.Web.Accounting.Program
{
    public partial class COABudgetYearList : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Accounting.COA_BUDGET_YEAR;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            cboYear.DataSource = Enumerable.Range(DateTime.Now.Year - 2, 5).Reverse();
            cboYear.EnableCallbackMode = false;
            cboYear.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboYear.DropDownStyle = DropDownStyle.DropDownList;
            cboYear.DataBind();
            cboYear.Value = DateTime.Now.Year.ToString();

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        public override void SetFilterParameter(ref string[] fieldListText, ref string[] fieldListValue)
        {
            fieldListText = new string[] { "No Perkiraan", "Nama Perkiraan", "Tipe Perkiraan" };
            fieldListValue = new string[] { "GLAccountNo", "GLAccountName", "GLAccountType" };
        }

        private string GetFilterExpression()
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvChartOfAccountRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 11);
            }

            List<vChartOfAccount> lstEntity = BusinessLayer.GetvChartOfAccountList(filterExpression, 11, pageIndex);
            lstCOABudget = BusinessLayer.GetCOABudgetList(string.Format("GLAccount IN ({0}) AND PeriodNo = '{1}' AND IsDeleted = 0", string.Join(",", lstEntity.Select(p => p.GLAccountID)), cboYear.Value));

            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        List<COABudget> lstCOABudget = null;
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vChartOfAccount entity = e.Row.DataItem as vChartOfAccount;
                HtmlInputText txtBudgetAmount = e.Row.FindControl("txtBudgetAmount") as HtmlInputText;
                HtmlInputButton btnSave = e.Row.FindControl("btnSave") as HtmlInputButton;
                if (entity.IsHeader)
                {
                    txtBudgetAmount.Style.Add("display", "none");
                    btnSave.Style.Add("display", "none");
                }
                else
                {
                    COABudget entityCOABudget = lstCOABudget.FirstOrDefault(p => p.GLAccount == entity.GLAccountID);
                    if (entityCOABudget != null)
                        txtBudgetAmount.Value = entityCOABudget.BudgetAmount.ToString();
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

        #region Save
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (UpdateCOABudget(param, ref errMessage))
                result += "success";
            else
                result += string.Format("fail|{0}", errMessage);

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool UpdateCOABudget(string[] param, ref string errMessage)
        {
            try
            {
                Int32 GLAccount = Convert.ToInt32(param[1]);
                Decimal budgetAmount = Convert.ToDecimal(param[2]);
                String PeriodNo = cboYear.Value.ToString();

                COABudget entity = BusinessLayer.GetCOABudgetList(string.Format("GLAccount = {0} AND PeriodNo = '{1}' AND IsDeleted = 0", GLAccount, PeriodNo)).FirstOrDefault();
                if (entity == null)
                {
                    entity = new COABudget();
                    entity.GLAccount = GLAccount;
                    entity.PeriodNo = PeriodNo;
                    entity.BudgetAmount = budgetAmount;
                    entity.IsDeleted = false;
                    entity.CreatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.InsertCOABudget(entity);
                }
                else
                {
                    entity.BudgetAmount = budgetAmount;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.UpdateCOABudget(entity);
                }
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion
    }
}