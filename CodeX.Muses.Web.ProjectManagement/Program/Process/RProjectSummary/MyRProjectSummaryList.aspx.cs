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
            List<vRProjectTask> lstNewTask = BusinessLayer.GetvRProjectTaskList(string.Format("ProjectTaskID IN (SELECT ProjectTaskID FROM RProjectTaskAssign WHERE ProjectOrganizationID IN (SELECT ProjectOrganizationID FROM RProjectOrganizationMember WHERE EmployeeID = {0})) AND GCProjectTaskStatus IN ('{1}','{2}','{3}') AND CreatedDate >= '{4}'", AppSession.UserLogin.EmployeeID, Constant.ProjectTaskStatus.OPEN, Constant.ProjectTaskStatus.IN_PROGRESS, Constant.ProjectTaskStatus.NEED_CONFIRMATION, DateTime.Now.AddDays(-1).ToString("yyyyMMdd")), 5, 1, "EndDate ASC");

            string lstProjectID = "";
            if (lstNewTask.Count > 0)
                lstProjectID = string.Join(",", lstNewTask.Select(p => p.ProjectID).ToList());
            
            List<vRProjectTask> lstOldTask = BusinessLayer.GetvRProjectTaskList(string.Format("ProjectTaskID IN (SELECT ProjectTaskID FROM RProjectTaskAssign WHERE ProjectOrganizationID IN (SELECT ProjectOrganizationID FROM RProjectOrganizationMember WHERE EmployeeID = {0})) AND GCProjectTaskStatus IN ('{1}','{2}','{3}') AND CreatedDate < '{4}'", AppSession.UserLogin.EmployeeID, Constant.ProjectTaskStatus.OPEN, Constant.ProjectTaskStatus.IN_PROGRESS, Constant.ProjectTaskStatus.NEED_CONFIRMATION, DateTime.Now.AddDays(-1).ToString("yyyyMMdd")), 5, 1, "EndDate ASC");
            if (lstOldTask.Count > 0)
            {
                if (lstProjectID != "")
                    lstProjectID += ",";
                lstProjectID = string.Join(",", lstOldTask.Select(p => p.ProjectID).ToList());
            }


            List<vRProjectTask> lstNeedVerifiedTask = BusinessLayer.GetvRProjectTaskList(string.Format("AssignedByPosition IN (SELECT ProjectOrganizationID FROM RProjectOrganizationMember WHERE EmployeeID = {0}) AND GCProjectTaskStatus IN ('{1}') AND IsVerified = 0", AppSession.UserLogin.EmployeeID, Constant.ProjectTaskStatus.CLOSED, DateTime.Now.AddDays(-1).ToString("yyyyMMdd")), 5, 1, "EndDate ASC");
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
    }
}