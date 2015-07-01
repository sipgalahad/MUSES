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
    public partial class ProjectTaskDetailList : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        List<ProjectTaskLog> lstLog;
        List<vTeamDt> lstTeamDt;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.PROJECT_TASK_DETAIL;
        }

        protected string OnGetEmployeeFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND GCEmployeeStatus = '{1}' AND IsDeleted = 0", AppSession.UserLogin.SiteID, Constant.EmployeeStatus.FULL_TIME_EMPLOYED);
        }

        protected override void InitializeDataControl()
        {
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(String.Format("IsDeleted = 0 AND IsActive = 1 AND ParentID = '{0}'", Constant.StandardCode.PROJECT_TASK_STATUS));
            lstStandardCode.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "All" });
            Methods.SetComboBoxField(cboStatus, lstStandardCode.Where(x => x.StandardCodeID != Constant.ProjectTaskStatus.VOID).ToList(), "StandardCodeName", "StandardCodeID");
            cboStatus.SelectedIndex = 0;

            List<Project> lstProject = BusinessLayer.GetProjectList(String.Format("GCProjectStatus NOT IN ('{0}','{1}')",Constant.ProjectStatus.COMPLETE, Constant.ProjectStatus.CANCELED));
            if (lstProject.Count() > 0) 
            {
                lstTeamDt = BusinessLayer.GetvTeamDtList(String.Format("ProjectID IN ({0}) AND EmployeeCoordinatorID = {1} AND IsDeleted = 0", String.Join(",", lstProject.Select(x => x.ProjectID)), AppSession.UserLogin.EmployeeID));
                if (lstTeamDt.Count > 0) 
                {
                    hdnLstTeamDtID.Value = String.Format("{0},{1}", String.Join(",", lstTeamDt.Select(x => x.TeamDtID)), String.Join(",", lstTeamDt.Select(x => x.Downline)));

                    List<TeamDt> lst = BusinessLayer.GetTeamDtList(String.Format("TeamDtID IN ({0})", hdnLstTeamDtID.Value));
                    String tempTeamDt = String.Join(",", lst.Select(x => x.EmployeeCoordinatorID));
                    List<TeamDtMember> lstMember = BusinessLayer.GetTeamDtMemberList(String.Format("TeamDtID IN ({0})", hdnLstTeamDtID.Value));
                    String tempMember = String.Join(",", lstMember.Select(x => x.EmployeeID));
                    hdnLstEmployeeID.Value = String.Format("{0},{1}", tempTeamDt, tempMember);
                }
            }
            RowCountPerPage = Constant.GridViewPageSize.GRID_MATRIX;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        #region Bind Grid View
        private String OnGetFilterExpression() 
        {
            String filterExpression = string.Format("(ProjectID = {0} OR ProjectTree LIKE '%|{0}|%') AND GCProjectTaskStatus != '{1}' AND GCProjectTaskType = '{2}'", AppSession.ProjectID, Constant.ProjectTaskStatus.VOID, Constant.ProjectTaskType.SCHEDULED);
            if (hdnLstTeamDtID.Value.ToString() != "")
                filterExpression += String.Format(" AND TeamDtID IN ({0})", hdnLstTeamDtID.Value);
            if (tacEmployee.Value.ToString() != "")
                filterExpression += String.Format(" AND (OwnerID = {0} OR ListAssigneeID LIKE '%|{0}|%')", tacEmployee.Value);
            if (cboStatus.Value != null && cboStatus.Value.ToString() != "")
                filterExpression += String.Format(" AND GCProjectTaskStatus = '{0}'", cboStatus.Value);
            if (txtStartDate.Text != "" && txtEndDate.Text != "")
                filterExpression += String.Format(" AND EndDate BETWEEN '{0}' AND '{1}'", Helper.GetDatePickerValue(txtStartDate.Text), Helper.GetDatePickerValue(txtEndDate.Text));
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            String filterExpression = OnGetFilterExpression();
            
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvProjectTaskCustomRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MATRIX);
            }
            List<vProjectTaskCustom> lstEntity = BusinessLayer.GetvProjectTaskCustomList(filterExpression, Constant.GridViewPageSize.GRID_MATRIX, pageIndex, "Position");
            if(lstEntity.Count() > 0)
                lstLog = BusinessLayer.GetProjectTaskLogList(String.Format("ProjectTaskID IN ({0})", String.Join(",",lstEntity.Select(x => x.ProjectTaskID))));
            
            grdView.DataSource = lstEntity;
            grdView.DataBind();
            hdnTotalTask.Value = lstEntity.Count().ToString();
            hdnOpen.Value = lstEntity.Where(x => x.GCProjectTaskStatus == Constant.ProjectTaskStatus.OPEN).ToList().Count().ToString();
            hdnInProgress.Value = lstEntity.Where(x => x.GCProjectTaskStatus == Constant.ProjectTaskStatus.IN_PROGRESS).ToList().Count().ToString();
            hdnClosed.Value = lstEntity.Where(x => x.GCProjectTaskStatus == Constant.ProjectTaskStatus.CLOSED).ToList().Count().ToString();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vProjectTaskCustom entity = e.Row.DataItem as vProjectTaskCustom;

                HtmlGenericControl lblProjectTaskName = e.Row.FindControl("lblProjectTaskName") as HtmlGenericControl;
                HtmlGenericControl lblAssignName = e.Row.FindControl("lblAssignName") as HtmlGenericControl;
                HtmlGenericControl divStatus = e.Row.FindControl("divStatus") as HtmlGenericControl;

                if (lstLog.Where(x => x.ProjectTaskID == entity.ProjectTaskID).Count() > 0) 
                {
                    lblProjectTaskName.Attributes.Add("class", "lblLink lblProjectTaskName");
                }
                    

                String lstString = WebUtility.HtmlEncode(entity.EmployeeName != "" ? String.Format("{0};{1}", entity.EmployeeName,entity.ListAssigneeName) : entity.ListAssigneeName);
                List<String> lstName = lstString.Split(';').ToList();

                if (lstName.Count() > 3) 
                {
                    lblAssignName.Attributes.Add("class", "lblLink lblAssignName");
                }
                    

                if (entity.GCProjectTaskStatus != Constant.ProjectTaskStatus.CLOSED && entity.EndDate.Date < DateTime.Now.Date) 
                {
                    divStatus.Style.Add("color", "white");
                    divStatus.Style.Add("background-color", "red");
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
    }
}