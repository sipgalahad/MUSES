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
    public partial class RProjectTaskFileList : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.RPROJECT_TASK_FILE;
        }

        protected override void InitializeDataControl()
        {
            if (AppSession.IsMyProject)
            {
                vRProjectOrganizationMember entityOrganizationMember = BusinessLayer.GetvRProjectOrganizationMemberList(string.Format("ProjectID = {0} AND EmployeeID = {1}", AppSession.ProjectID, AppSession.UserLogin.EmployeeID)).FirstOrDefault();
                hdnMyProjectOrganizationID.Value = entityOrganizationMember.ProjectOrganizationID.ToString();
                hdnMyProjectOrganizationIDDisplayPath.Value = entityOrganizationMember.DisplayPath;
            }

            RowCountPerPage = Constant.GridViewPageSize.GRID_MATRIX;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        #region Bind Grid View
        private String OnGetFilterExpression() 
        {
            String filterExpression = string.Format("ProjectID = {0} AND IsDeleted = 0", AppSession.ProjectID);
            if (AppSession.IsMyProject)
                filterExpression += string.Format(" AND ProjectTaskID IN (SELECT ProjectTaskID FROM vRProjectTaskAssign WHERE DisplayPath LIKE '%/{0}/%')", hdnMyProjectOrganizationID.Value);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            String filterExpression = OnGetFilterExpression();
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvRProjectTaskFileRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vRProjectTaskFile> lstEntity = BusinessLayer.GetvRProjectTaskFileList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ProjectTaskFileID DESC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vRProjectTaskFile entity = e.Row.DataItem as vRProjectTaskFile;
                HtmlInputHidden hdnDownloadedFile = (HtmlInputHidden)e.Row.FindControl("hdnDownloadedFile");
                hdnDownloadedFile.Value = string.Format("{0}Project/{1}/{2}/{3}", AppConfigManager.CDXVirtualDirectory, AppSession.ProjectID, entity.ProjectTaskID, entity.Path);
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
    }
}