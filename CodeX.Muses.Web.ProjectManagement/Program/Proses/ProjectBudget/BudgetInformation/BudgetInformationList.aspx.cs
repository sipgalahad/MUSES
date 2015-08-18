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
    public partial class BudgetInformationList : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.PROJECT_BUDGET_INFORMATION;
        }

        protected override void InitializeDataControl()
        {
            hdnEmployeeCoordinatorID.Value = AppSession.UserLogin.EmployeeID.ToString();
            String ProjectFilterExpression = String.Format("((listParentID LIKE '%,{0},%') OR ProjectID = {0})", AppSession.ProjectID);

            if (AppSession.UserLogin.EmployeeID != null && AppSession.UserLogin.EmployeeID != 0)
            {
                ProjectFilterExpression = String.Format("GCProjectStatus NOT IN ('{0}','{1}') AND " +
                                          "ProjectID IN (SELECT ProjectID FROM vTeamDt WHERE EmployeeCoordinatorID = '{2}' OR ListEmployeeID1 LIKE '%;{2};%')", Constant.ProjectStatus.CANCELED, Constant.ProjectStatus.COMPLETE, AppSession.UserLogin.EmployeeID);
            }
            else
            {
                ProjectFilterExpression = String.Format("GCProjectStatus NOT IN ('{0}','{1}')", Constant.ProjectStatus.CANCELED, Constant.ProjectStatus.COMPLETE);
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

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            String filterExpression = OnGetFilterExpression();

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvProjectBudgetRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MATRIX);
            }

            List<StandardCode> lstFundType = BusinessLayer.GetStandardCodeList(String.Format("IsDeleted = 0 AND IsActive = 1 AND ParentID = '{0}'", Constant.StandardCode.PROJECT_FUNDING));
            rptViewHeader.DataSource = lstFundType.OrderBy(x => x.StandardCodeID);
            rptViewHeader.DataBind();

            thDana.ColSpan = lstFundType.Count();

            List<vProjectBudget> lstEntity = BusinessLayer.GetvProjectBudgetList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
            grdView.DataSource = lstEntity;
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

        protected void grdView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vProjectBudget entity = e.Item.DataItem as vProjectBudget;
                Repeater rptViewItem = e.Item.FindControl("rptViewItem") as Repeater;
                String[] lst = entity.ListFund.Split('|');
                rptViewItem.DataSource = lst;
                rptViewItem.DataBind();
            }

            if (grdView.Items.Count > 0)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    HtmlTableRow trEmpty = (HtmlTableRow)e.Item.FindControl("trEmpty");
                    trEmpty.Style.Add("Display", "none");
                }
            }
        }
        #endregion

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }
    }
}