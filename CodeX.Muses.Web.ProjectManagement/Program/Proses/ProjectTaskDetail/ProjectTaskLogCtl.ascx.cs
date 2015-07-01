using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common;
using CodeX.Common;
using CodeX.Data.Core.Dal;

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class ProjectTaskLogCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            ProjectTask entity = BusinessLayer.GetProjectTask(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.ProjectTaskCode, entity.ProjectTaskName);
            
            BindGridView();
        }

        private string OnGetFilterExpression() 
        { 
            String filterExpression = "";
            filterExpression += string.Format("ProjectTaskID = {0} AND IsDeleted = 0", hdnID.Value);
            return filterExpression;
        }

        private void BindGridView()
        {
            String filterExpression = OnGetFilterExpression();
            grdView.DataSource = BusinessLayer.GetvProjectTaskLogList(filterExpression);
            grdView.DataBind();
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}