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
using CodeX.Data.Core.Dal;

namespace CodeX.Muses.Web.Accounting.Program
{
    public partial class COABudgetMonthList : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Accounting.COA_BUDGET_MONTH;
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
            lstCOABudget = BusinessLayer.GetCOABudgetList(string.Format("GLAccount IN ({0}) AND PeriodNo LIKE '{1}%' AND IsDeleted = 0", string.Join(",", lstEntity.Select(p => p.GLAccountID)), cboYear.Value));

            lvwView.DataSource = lstEntity;
            lvwView.DataBind();
        }

        List<COABudget> lstCOABudget = null;
        protected void lvwView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                vChartOfAccount entity = e.Item.DataItem as vChartOfAccount;
                List<COABudget> lstEntityCOABudget = lstCOABudget.Where(p => p.GLAccount == entity.GLAccountID).ToList();

                COABudget entityCOABudget = null;
                for (int i = 1; i <= 12; ++i)
                {
                    HtmlInputText txtBudgetAmount = e.Item.FindControl("txtBudgetAmount" + i) as HtmlInputText;
                    if (entity.IsHeader)
                        txtBudgetAmount.Style.Add("display", "none");
                    else
                    {
                        string periodNo = string.Format("{0}{1}", cboYear.Value, i.ToString("00"));
                        entityCOABudget = lstEntityCOABudget.FirstOrDefault(p => p.PeriodNo == periodNo);
                        if (entityCOABudget != null)
                            txtBudgetAmount.Value = entityCOABudget.BudgetAmount.ToString();
                    }
                }

                HtmlInputText txtBudgetAmountYear = e.Item.FindControl("txtBudgetAmountYear") as HtmlInputText;
                if (entity.IsHeader)
                    txtBudgetAmountYear.Style.Add("display", "none");
                else
                {
                    entityCOABudget = lstEntityCOABudget.FirstOrDefault(p => p.PeriodNo == cboYear.Value.ToString());
                    if (entityCOABudget != null)
                        txtBudgetAmountYear.Value = entityCOABudget.BudgetAmount.ToString();
                }

                HtmlInputButton btnSave = e.Item.FindControl("btnSave") as HtmlInputButton;
                if (entity.IsHeader)
                    btnSave.Style.Add("display", "none");
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
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            COABudgetDao stockTakingDtDao = new COABudgetDao(ctx);
            try
            {
                Int32 GLAccount = Convert.ToInt32(param[1]);
                Decimal budgetAmount = Convert.ToDecimal(param[2]);

                List<COABudget> lstEntity = BusinessLayer.GetCOABudgetList(string.Format("GLAccount = {0} AND IsDeleted = 0", GLAccount), ctx);
                for (int i = 1; i <= 13; ++i)
                {
                    SaveCOABudget(stockTakingDtDao, GLAccount, lstEntity, i, param);
                }

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        private void SaveCOABudget(COABudgetDao stockTakingDtDao, int GLAccount, List<COABudget> lstEntity, int ctr, string[] param)
        {
            String periodNo = "";
            if (ctr < 13)
                periodNo = string.Format("{0}{1}", cboYear.Value, ctr.ToString("00"));
            else
                periodNo = cboYear.Value.ToString();
            Decimal budgetAmount = Convert.ToDecimal(param[ctr + 1]);

            COABudget entity = lstEntity.FirstOrDefault(p => p.PeriodNo == periodNo);
            if (entity == null)
            {
                if (budgetAmount != 0)
                {
                    entity = new COABudget();
                    entity.GLAccount = GLAccount;
                    entity.PeriodNo = periodNo;
                    entity.BudgetAmount = budgetAmount;
                    entity.IsDeleted = false;
                    entity.CreatedBy = AppSession.UserLogin.UserID;
                    stockTakingDtDao.Insert(entity);
                }
            }
            else
            {
                entity.BudgetAmount = budgetAmount;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                stockTakingDtDao.Update(entity);
            }
        }
        #endregion
    }
}