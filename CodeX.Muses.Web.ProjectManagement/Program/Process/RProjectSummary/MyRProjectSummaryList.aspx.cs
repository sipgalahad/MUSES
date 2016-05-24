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
            List<vRProjectTask> lstNewTask = BusinessLayer.GetvRProjectTaskList("");
            rptNewTask.DataSource = lstNewTask;
            rptNewTask.DataBind();
        }
    }
}