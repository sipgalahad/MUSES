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
    public partial class MyRProjectPageList : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.MY_RPROJECT_PAGE_LIST;
        }
        protected string OnGetProjectStatusClosed()
        {
            return Constant.TransactionStatus.CLOSED;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<Variable> lstFilterStatus = new List<Variable>();
            lstFilterStatus.Add(new Variable { Code = "0", Value = "Semua" });
            lstFilterStatus.Add(new Variable { Code = "1", Value = "Belum Selesai" });
            lstFilterStatus.Add(new Variable { Code = "2", Value = "Sudah Selesai" });
            Methods.SetComboBoxField<Variable>(cboFilterStatus, lstFilterStatus, "Value", "Code");
            cboFilterStatus.Value = "1";

            hdnFilterExpression.Value = filterExpression;
            hdnID.Value = keyValue;
            filterExpression = GetFilterExpression();
            if (keyValue != "")
            {
                int row = BusinessLayer.GetvRProjectRowIndex(filterExpression, keyValue) + 1;
                CurrPage = Helper.GetPageCount(row, Constant.GridViewPageSize.GRID_MASTER);
            }
            else
                CurrPage = 1;

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        public override void SetFilterParameter(ref string[] fieldListText, ref string[] fieldListValue)
        {
            fieldListText = new string[] { "Project Code", "Project Name" };
            fieldListValue = new string[] { "ProjectCode", "ProjectName" };
        }

        private string GetFilterExpression()
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("EmployeeID = {0}", AppSession.UserLogin.EmployeeID);
            if (cboFilterStatus.Value.ToString() == "0")
                filterExpression += string.Format(" AND GCProjectStatus != '{0}'", Constant.TransactionStatus.VOID);
            else if (cboFilterStatus.Value.ToString() == "1")
                filterExpression += string.Format(" AND GCProjectStatus NOT IN ('{0}','{1}')", Constant.TransactionStatus.CLOSED, Constant.TransactionStatus.VOID);
            else
                filterExpression += string.Format(" AND GCProjectStatus = '{0}'", Constant.TransactionStatus.CLOSED);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvRProjectOrganizationMemberRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            
            List<vRProjectOrganizationMember> lstEntity = BusinessLayer.GetvRProjectOrganizationMemberList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
            if (lstEntity.Count > 0)
            {
                string lstProjectID = string.Join(",", lstEntity.Select(p => p.ProjectID).ToList());
                string filterExpressionTask = "";
                foreach (vRProjectOrganizationMember entity in lstEntity)
                {
                    if (filterExpressionTask != "")
                        filterExpressionTask += " OR ";
                    filterExpressionTask += string.Format("(ProjectID = {0} AND DisplayPath LIKE '%/{1}/%')", entity.ProjectID, entity.ProjectOrganizationID);
                }
                lstProjectTaskAssign = BusinessLayer.GetvRProjectTaskAssignList(filterExpressionTask);
            }
            else
                lstProjectTaskAssign = new List<vRProjectTaskAssign>();
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        List<vRProjectTaskAssign> lstProjectTaskAssign = null;
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vRProjectOrganizationMember entity = e.Row.DataItem as vRProjectOrganizationMember;
                HtmlGenericControl divPercentage = e.Row.FindControl("divPercentage") as HtmlGenericControl;
                HtmlGenericControl divDueDate = e.Row.FindControl("divDueDate") as HtmlGenericControl;
                HtmlInputHidden hdnIsProjectAdmin = e.Row.FindControl("hdnIsProjectAdmin") as HtmlInputHidden;

                hdnIsProjectAdmin.Value = entity.IsProjectAdmin ? "1" : "0";

                List<RProjectTask> lstTask = (from p in lstProjectTaskAssign.Where(p => p.DisplayPath.Contains("/" + entity.ProjectOrganizationID + "/")).ToList()
                                              select new RProjectTask { ProjectTaskID = p.ProjectTaskID, GCProjectTaskStatus = p.GCProjectTaskStatus, GCDueDateType = p.GCDueDateType, EndDate = p.EndDate }).GroupBy(p => p.ProjectTaskID).Select(p => p.First()).ToList();

                List<RProjectTask> lstUnfinishedTask = lstTask.Where(p => p.GCProjectTaskStatus != Constant.ProjectTaskStatus.CLOSED && p.GCDueDateType != Constant.DueDateType.NO_DUE_DATE).ToList();
                String endDate = "";
                if (lstUnfinishedTask.Count > 0)
                    endDate = lstUnfinishedTask.Min(p => p.EndDate).ToString(Constant.FormatString.DATE_FORMAT);

                divDueDate.InnerHtml = endDate;
                double percentage = 0;
                if (lstTask.Count > 0)
                {
                    int count = lstTask.Count;
                    int finishedCount = lstTask.Where(p => p.GCProjectTaskStatus == Constant.ProjectTaskStatus.CLOSED).Count();
                    percentage = (double)finishedCount * 100 / (double)count;
                    divPercentage.InnerHtml = string.Format("{1}/{2} ({0}%)", percentage, finishedCount, count);
                }
                else
                    divPercentage.InnerHtml = "-";
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
            if (type == "close")
            {
                if (hdnID.Value.ToString() != "")
                {
                    int count = BusinessLayer.GetvRProjectTaskRowCount(string.Format("ProjectID = {0} AND GCProjectTaskStatus NOT IN ('{1}','{2}')", AppSession.ProjectID, Constant.ProjectTaskStatus.CLOSED, Constant.ProjectTaskStatus.VOID));
                    if (count > 0)
                    {
                        errMessage = "Ada Tugas Belum Selesai. Project Belum Bisa Ditutup";
                        return false;
                    }
                    else
                    {
                        RProject entity = BusinessLayer.GetRProject(Convert.ToInt32(hdnID.Value));
                        entity.GCProjectStatus = Constant.TransactionStatus.CLOSED;
                        entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                        BusinessLayer.UpdateRProject(entity);
                        return true;
                    }
                }
                return false;
            }
            else
            {
                if (hdnID.Value.ToString() != "")
                {
                    RProject entity = BusinessLayer.GetRProject(Convert.ToInt32(hdnID.Value));
                    entity.GCProjectStatus = Constant.TransactionStatus.OPEN;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.UpdateRProject(entity);
                    return true;
                }
                return false;
            }
        }
    }
}