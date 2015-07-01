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
    public partial class ToDoListInCalendar : BasePageList
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.TO_DO_LIST_IN_CALENDAR;
        }

        #region HTML Getter
        public String GetProjectTaskFilterExpression() 
        {
            String filterExpression = "";
            if (AppSession.UserLogin.EmployeeID != null && AppSession.UserLogin.EmployeeID != 0)
                filterExpression += String.Format("(OwnerID = {0} OR ProjectTaskID IN (SELECT ProjectTaskID FROM MemberTask WHERE AssigneeID = {0})) AND GCProjectTaskStatus != '{1}'", AppSession.UserLogin.EmployeeID, Constant.ProjectTaskStatus.VOID);
            else
                filterExpression += string.Format("GCProjectTaskStatus != '{0}'", Constant.ProjectTaskStatus.VOID);
            filterExpression += String.Format(" AND GCProjectTaskType = '{0}'", Constant.ProjectTaskType.SCHEDULED);
            return filterExpression;
        }
        #endregion

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            
        }

        private string GetFilterExpression()
        {
            string filterExpression = hdnFilterExpression.Value;
            return filterExpression;
        }

        private void EntityToControl(vProjectTask entity)
        {
            txtHeaderText.Text = String.Format("{0} - {1}", entity.ProjectTaskCode, entity.ProjectTaskName);
            txtPTStartDate.Text = entity.StartDateInDatePicker;
            txtPTStartTime.Text = entity.StartTime;
            txtPTEndDate.Text = entity.EndDateInDatePicker;
            txtPTEndTime.Text = entity.EndTime;
            txtPTRemarks.Text = entity.Remarks;
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";

            vProjectTask entity = BusinessLayer.GetvProjectTaskList(String.Format("ProjectTaskID = {0}", hdnID.Value))[0];
            EntityToControl(entity);
            
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            
        }
    }
}