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
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class MyRProjectSummaryList : BasePageList
    {
        protected int PageCount1 = 1;
        protected int RowCount1 = 1;
        protected int PageCount2 = 1;
        protected int RowCount2 = 1;
        protected int PageCount3 = 1;
        protected int RowCount3 = 1;
        protected int RowCountPerPage = 5;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.MY_RPROJECT_SUMMARY_LIST;
        }
        protected string OnGetProjectStatusClosed()
        {
            return Constant.TransactionStatus.CLOSED;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            filterExpression = string.Format("ProjectTaskID IN (SELECT ProjectTaskID FROM RProjectTaskAssign WHERE ProjectOrganizationID IN (SELECT ProjectOrganizationID FROM RProjectOrganizationMember WHERE EmployeeID = {0})) AND GCProjectTaskStatus IN ('{1}','{2}','{3}') AND CreatedDate >= '{4}'", AppSession.UserLogin.EmployeeID, Constant.ProjectTaskStatus.OPEN, Constant.ProjectTaskStatus.IN_PROGRESS, Constant.ProjectTaskStatus.NEED_CONFIRMATION, DateTime.Now.AddDays(-1).ToString("yyyyMMdd"));
            RowCount1 = BusinessLayer.GetvRProjectTaskRowCount(filterExpression);
            PageCount1 = Helper.GetPageCount(RowCount1, 5);
            List<vRProjectTask> lstNewTask = BusinessLayer.GetvRProjectTaskList(filterExpression, 5, 1, "EndDate ASC");

            string lstProjectID = "";
            if (lstNewTask.Count > 0)
                lstProjectID = string.Join(",", lstNewTask.Select(p => p.ProjectID).ToList());
            
            filterExpression = string.Format("ProjectTaskID IN (SELECT ProjectTaskID FROM RProjectTaskAssign WHERE ProjectOrganizationID IN (SELECT ProjectOrganizationID FROM RProjectOrganizationMember WHERE EmployeeID = {0})) AND GCProjectTaskStatus IN ('{1}','{2}','{3}') AND CreatedDate < '{4}'", AppSession.UserLogin.EmployeeID, Constant.ProjectTaskStatus.OPEN, Constant.ProjectTaskStatus.IN_PROGRESS, Constant.ProjectTaskStatus.NEED_CONFIRMATION, DateTime.Now.AddDays(-1).ToString("yyyyMMdd"));
            RowCount2 = BusinessLayer.GetvRProjectTaskRowCount(filterExpression);
            PageCount2 = Helper.GetPageCount(RowCount2, 5);
            List<vRProjectTask> lstOldTask = BusinessLayer.GetvRProjectTaskList(filterExpression, 5, 1, "EndDate ASC");
            if (lstOldTask.Count > 0)
            {
                if (lstProjectID != "")
                    lstProjectID += ",";
                lstProjectID = string.Join(",", lstOldTask.Select(p => p.ProjectID).ToList());
            }

            filterExpression = string.Format("AssignedByPosition IN (SELECT ProjectOrganizationID FROM RProjectOrganizationMember WHERE EmployeeID = {0}) AND GCProjectTaskStatus IN ('{1}') AND IsVerified = 0", AppSession.UserLogin.EmployeeID, Constant.ProjectTaskStatus.CLOSED, DateTime.Now.AddDays(-1).ToString("yyyyMMdd"));
            RowCount3 = BusinessLayer.GetvRProjectTaskRowCount(filterExpression);
            PageCount3 = Helper.GetPageCount(RowCount3, 5);
            List<vRProjectTask> lstNeedVerifiedTask = BusinessLayer.GetvRProjectTaskList(filterExpression, 5, 1, "EndDate ASC");
            if (lstNeedVerifiedTask.Count > 0)
            {
                if (lstProjectID != "")
                    lstProjectID += ",";
                lstProjectID = string.Join(",", lstNeedVerifiedTask.Select(p => p.ProjectID).ToList());
            }

            if (lstProjectID != "")
                lstTaskProjectOrganizationMember = BusinessLayer.GetvRProjectOrganizationMemberList(string.Format("ProjectID IN ({0}) AND EmployeeID = {1}", lstProjectID, AppSession.UserLogin.EmployeeID));
            else
                lstTaskProjectOrganizationMember = new List<vRProjectOrganizationMember>();


            grdNewTask.DataSource = lstNewTask;
            grdNewTask.DataBind();
            grdOldTask.DataSource = lstOldTask;
            grdOldTask.DataBind();
            grdNeedVerification.DataSource = lstNeedVerifiedTask;
            grdNeedVerification.DataBind();
        }

        List<vRProjectOrganizationMember> lstTaskProjectOrganizationMember = null;
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vRProjectTask entity = e.Row.DataItem as vRProjectTask;
                HtmlGenericControl divPosition = e.Row.FindControl("divPosition") as HtmlGenericControl;
                HtmlInputHidden hdnProjectOrganizationID = e.Row.FindControl("hdnProjectOrganizationID") as HtmlInputHidden;
                HtmlInputHidden hdnProjectOrganizationIDDisplayPath = e.Row.FindControl("hdnProjectOrganizationIDDisplayPath") as HtmlInputHidden;

                vRProjectOrganizationMember member = lstTaskProjectOrganizationMember.FirstOrDefault(p => p.ProjectID == entity.ProjectID);
                hdnProjectOrganizationID.Value = member.ProjectOrganizationID.ToString();
                hdnProjectOrganizationIDDisplayPath.Value = member.DisplayPath;
                divPosition.InnerHtml = member.Position;
            }
        }

        private void BindGridView1(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = string.Format("ProjectTaskID IN (SELECT ProjectTaskID FROM RProjectTaskAssign WHERE ProjectOrganizationID IN (SELECT ProjectOrganizationID FROM RProjectOrganizationMember WHERE EmployeeID = {0})) AND GCProjectTaskStatus IN ('{1}','{2}','{3}') AND CreatedDate >= '{4}'", AppSession.UserLogin.EmployeeID, Constant.ProjectTaskStatus.OPEN, Constant.ProjectTaskStatus.IN_PROGRESS, Constant.ProjectTaskStatus.NEED_CONFIRMATION, DateTime.Now.AddDays(-1).ToString("yyyyMMdd"));
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvRProjectTaskRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 5);
            }
            List<vRProjectTask> lstEntity = BusinessLayer.GetvRProjectTaskList(filterExpression, 5, pageIndex, "EndDate ASC");
            string lstProjectID = "";
            if (lstEntity.Count > 0)
                lstProjectID = string.Join(",", lstEntity.Select(p => p.ProjectID).ToList());
            if (lstProjectID != "")
                lstTaskProjectOrganizationMember = BusinessLayer.GetvRProjectOrganizationMemberList(string.Format("ProjectID IN ({0}) AND EmployeeID = {1}", lstProjectID, AppSession.UserLogin.EmployeeID));
            else
                lstTaskProjectOrganizationMember = new List<vRProjectOrganizationMember>();

            grdNewTask.DataSource = lstEntity;
            grdNewTask.DataBind();
        }

        protected void cbpView1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView1(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView1(1, true, ref pageCount, ref rowCount);
                    result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void BindGridView2(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = string.Format("ProjectTaskID IN (SELECT ProjectTaskID FROM RProjectTaskAssign WHERE ProjectOrganizationID IN (SELECT ProjectOrganizationID FROM RProjectOrganizationMember WHERE EmployeeID = {0})) AND GCProjectTaskStatus IN ('{1}','{2}','{3}') AND CreatedDate < '{4}'", AppSession.UserLogin.EmployeeID, Constant.ProjectTaskStatus.OPEN, Constant.ProjectTaskStatus.IN_PROGRESS, Constant.ProjectTaskStatus.NEED_CONFIRMATION, DateTime.Now.AddDays(-1).ToString("yyyyMMdd"));
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvRProjectTaskRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 5);
            }
            List<vRProjectTask> lstEntity = BusinessLayer.GetvRProjectTaskList(filterExpression, 5, pageIndex, "EndDate ASC");
            string lstProjectID = "";
            if (lstEntity.Count > 0)
                lstProjectID = string.Join(",", lstEntity.Select(p => p.ProjectID).ToList());
            if (lstProjectID != "")
                lstTaskProjectOrganizationMember = BusinessLayer.GetvRProjectOrganizationMemberList(string.Format("ProjectID IN ({0}) AND EmployeeID = {1}", lstProjectID, AppSession.UserLogin.EmployeeID));
            else
                lstTaskProjectOrganizationMember = new List<vRProjectOrganizationMember>();

            grdOldTask.DataSource = lstEntity;
            grdOldTask.DataBind();
        }

        protected void cbpView2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView2(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView2(1, true, ref pageCount, ref rowCount);
                    result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void BindGridView3(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = string.Format("AssignedByPosition IN (SELECT ProjectOrganizationID FROM RProjectOrganizationMember WHERE EmployeeID = {0}) AND GCProjectTaskStatus IN ('{1}') AND IsVerified = 0", AppSession.UserLogin.EmployeeID, Constant.ProjectTaskStatus.CLOSED, DateTime.Now.AddDays(-1).ToString("yyyyMMdd"));
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvRProjectTaskRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 5);
            }
            List<vRProjectTask> lstEntity = BusinessLayer.GetvRProjectTaskList(filterExpression, 5, pageIndex, "EndDate ASC");
            string lstProjectID = "";
            if (lstEntity.Count > 0)
                lstProjectID = string.Join(",", lstEntity.Select(p => p.ProjectID).ToList());
            if (lstProjectID != "")
                lstTaskProjectOrganizationMember = BusinessLayer.GetvRProjectOrganizationMemberList(string.Format("ProjectID IN ({0}) AND EmployeeID = {1}", lstProjectID, AppSession.UserLogin.EmployeeID));
            else
                lstTaskProjectOrganizationMember = new List<vRProjectOrganizationMember>();

            grdNeedVerification.DataSource = lstEntity;
            grdNeedVerification.DataBind();
        }

        protected void cbpView3_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView3(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView3(1, true, ref pageCount, ref rowCount);
                    result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
    }
}