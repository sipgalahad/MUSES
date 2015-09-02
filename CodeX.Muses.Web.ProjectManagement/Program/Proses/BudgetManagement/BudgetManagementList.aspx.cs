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
using CodeX.Common;
using DevExpress.Web.ASPxEditors;

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class BudgetManagementList : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.BUDGET_MANAGEMENT;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            hdnFilterExpression.Value = filterExpression;
            hdnID.Value = keyValue;
            filterExpression = GetFilterExpression();
            if (keyValue != "")
            {
                int row = BusinessLayer.GetvProjectRowIndex(filterExpression, keyValue) + 1;
                CurrPage = Helper.GetPageCount(row, Constant.GridViewPageSize.GRID_MASTER);
            }
            else
                CurrPage = 1;

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        public override void SetFilterParameter(ref string[] fieldListText, ref string[] fieldListValue)
        {
            fieldListText = new string[] { "Budget Code", "Budget Name" };
            fieldListValue = new string[] { "BudgetCode", "BudgetName" };
        }

        private string GetFilterExpression()
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("SiteID = '{0}' AND GCTransactionStatus != '{1}'", AppSession.UserLogin.SiteID, Constant.TransactionStatus.VOID);
            return filterExpression;
        }
        private List<StandardCode> lstStatus = null;

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvProjectBudgetHdRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            lstStatus = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsDeleted = 0 AND IsActive = 1", Constant.StandardCode.TRANSACTION_STATUS));

            List<vProjectBudgetHd> lstEntity = BusinessLayer.GetvProjectBudgetHdList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
            grdView.DataSource = lstEntity;
            grdView.DataBind();

            
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vProjectBudgetHd entity = e.Row.DataItem as vProjectBudgetHd;

                ASPxComboBox cboStatus = e.Row.FindControl("cboStatus") as ASPxComboBox;
                cboStatus.ClientInstanceName = string.Format("cboStatus{0}", e.Row.DataItemIndex);
                Methods.SetComboBoxField(cboStatus, lstStatus, "StandardCodeName", "StandardCodeID");
                cboStatus.Value = entity.GCTransactionStatus;
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

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnID.Value.ToString() != "")
                {
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool OnSaveEditRecordEntityDt(ref String errMessage)
        {
            bool result = true;
            try
            {
                ProjectBudgetHd entity = BusinessLayer.GetProjectBudgetHd(Convert.ToInt32(hdnID.Value));
                entity.GCTransactionStatus = hdnStatus.Value;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateProjectBudgetHd(entity);
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }
    }
}