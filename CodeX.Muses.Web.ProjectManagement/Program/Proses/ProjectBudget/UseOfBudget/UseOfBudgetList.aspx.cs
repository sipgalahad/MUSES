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
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxEditors;
using System.Net;

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class UseOfBudgetList : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        List<ProjectTaskLog> lstLog;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.USE_OF_BUDGET;
        }

        protected override void InitializeDataControl()
        {
            hdnEmployeeCoordinatorID.Value = AppSession.UserLogin.EmployeeID.ToString();
            String ProjectFilterExpression = String.Format("((listParentID LIKE '%,{0},%') OR ProjectID = {0})", AppSession.ProjectID);

            if (AppSession.UserLogin.EmployeeID != null && AppSession.UserLogin.EmployeeID != 0)
            {
                ProjectFilterExpression += String.Format("GCProjectStatus NOT IN ('{0}','{1}') AND " +
                                          "ProjectID IN (SELECT ProjectID FROM vTeamDt WHERE EmployeeCoordinatorID = '{2}' OR ListEmployeeID1 LIKE '%;{2};%')", Constant.ProjectStatus.CANCELED, Constant.ProjectStatus.COMPLETE, AppSession.UserLogin.EmployeeID);
            }
            else
            {
                ProjectFilterExpression += String.Format(" AND GCProjectStatus NOT IN ('{0}','{1}')", Constant.ProjectStatus.CANCELED, Constant.ProjectStatus.COMPLETE);
            }

            List<vProject> lstProject = BusinessLayer.GetvProjectList(ProjectFilterExpression);
            hdnLstParentID.Value = String.Join(",", lstProject.Select(x => x.ProjectID));
            lstProject.Insert(0, new vProject { ProjectName = "All", ProjectID = 0 });
            Methods.SetComboBoxField(cboProject, lstProject, "ProjectName", "ProjectID");
            cboProject.SelectedIndex = 0;

            RowCountPerPage = Constant.GridViewPageSize.GRID_MATRIX;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        #region Bind Grid View
        private String OnGetFilterExpression() 
        {
            String filterExpression = "";
            if (cboProject.Value.ToString() != "0")
                filterExpression += String.Format("ProjectID IN ({0})", cboProject.Value);
            else
                filterExpression += String.Format("ProjectID IN ({0})", hdnLstParentID.Value);
            return filterExpression;
        }
        private List<ProjectTaskBudget> lstProjectTaskBudget;
        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            String filterExpression = OnGetFilterExpression();

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvProjectBudgetDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MATRIX);
            }

            List<vProjectBudgetDt> lstEntity = BusinessLayer.GetvProjectBudgetDtList(filterExpression);
            String lstBudgetDtID = "0";
            if (lstEntity.Count > 0)
                lstBudgetDtID = String.Join(",",lstEntity.Select(x => x.BudgetDtID));
            lstProjectTaskBudget = BusinessLayer.GetProjectTaskBudgetList(String.Format("BudgetDtID IN ({0})",lstBudgetDtID));
            
            lvwView.DataSource = lstEntity;
            lvwView.DataBind();
        }

        protected void lvwView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                vProjectBudgetDt entity = e.Item.DataItem as vProjectBudgetDt;
                int idx = e.Item.DataItemIndex;

                HtmlInputHidden hdnAmount = e.Item.FindControl("hdnAmount") as HtmlInputHidden;
                HtmlTableCell UsedTaskAmount = e.Item.FindControl("UsedTaskAmount") as HtmlTableCell;
                HtmlInputText txtUsedAmount = e.Item.FindControl("txtUsedAmount") as HtmlInputText;
                txtUsedAmount.Attributes.Add("class", "txtCurrency txtUsedAmount" + idx);

                if (lstProjectTaskBudget.Count > 0)
                {
                    ProjectTaskBudget temp = lstProjectTaskBudget.FirstOrDefault(x => x.BudgetDtID == entity.BudgetDtID);
                    if (temp != null)
                    {
                        hdnAmount.Value = temp.UsedBudget.ToString();
                        UsedTaskAmount.InnerHtml = String.Format("<label class='lblLink lblUsedTaskAmount'>{0}</label>", temp.UsedBudget.ToString("N"));
                        txtUsedAmount.Value = (entity.UsedAmount - temp.UsedBudget).ToString();
                    }
                    else
                    {
                        hdnAmount.Value = "0.00";
                        UsedTaskAmount.InnerHtml = "0.00";
                        txtUsedAmount.Value = entity.UsedAmount.ToString();
                    }
                }
                else
                {
                    hdnAmount.Value = "0.00";
                    UsedTaskAmount.InnerHtml = "0.00";
                    txtUsedAmount.Value = entity.UsedAmount.ToString();
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
        #endregion

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
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
                ProjectBudgetDt entity = BusinessLayer.GetProjectBudgetDt(Convert.ToInt32(hdnID.Value));
                entity.UsedAmount = Convert.ToDecimal(hdnUsedAmount.Value);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateProjectBudgetDt(entity);
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