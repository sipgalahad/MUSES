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
    public partial class ProjectTaskStructureCtl : BaseViewPopupCtl
    {

        private ProjectTaskList DetailPage
        {
            get { return (ProjectTaskList)Page; }
        }

        public override void InitializeDataControl(string param)
        {
            String[] data = param.Split('|');
            hdnID.Value = data[0];
            hdnProjectID.Value = data[3];
            txtProjectTaskName.Text = string.Format("{0} - {1}", data[1], data[2]);

            BindGridView();
        }

        protected string OnGetProjectTaskFilterExpression()
        {
            string filterExpression = String.Format("ProjectID = {0} AND GCProjectTaskStatus NOT IN ('{1}','{2}') AND ProjectTaskID NOT IN (SELECT PrevProjectTaskID FROM ProjectTaskStructure WHERE ProjectTaskID = {3})", hdnProjectID.Value, Constant.ProjectTaskStatus.VOID, Constant.ProjectTaskStatus.CLOSED, hdnID.Value);
            if(hdnID.Value != "")
                filterExpression += String.Format(" AND ProjectTaskID != {0}", hdnID.Value);
            return filterExpression;
        }

        private void BindGridView()
        {
            String filterExpression = "1 = 0";
            if (hdnID.Value != "")
                filterExpression = string.Format("ProjectTaskID = {0}", hdnID.Value);
            grdPopupView.DataSource = BusinessLayer.GetvProjectTaskStructureList(filterExpression);
            grdPopupView.DataBind();
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        #region Process Detail
        protected void cbpProcessPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (OnSaveAddRecordEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else if (param[0] == "delete")
            {
                if (OnDeleteEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ProjectTaskStructureDao entityDao = new ProjectTaskStructureDao(ctx);
            try
            {
                int projectTaskID = 0;
                if (hdnID.Value != "")
                    projectTaskID = Convert.ToInt32(hdnID.Value);
                DetailPage.OnSaveAddRecordEntity(ref projectTaskID, ctx);
                ProjectTaskStructure entity = new ProjectTaskStructure();
                entity.ProjectTaskID = projectTaskID;
                entity.PrevProjectTaskID = Convert.ToInt32(hdnProjectTaskID.Value);
                entityDao.Insert(entity);
                hdnID.Value = projectTaskID.ToString();
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
            }
            finally 
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                BusinessLayer.DeleteProjectTaskStructure(Convert.ToInt32(hdnID.Value), Convert.ToInt32(hdnEntryID.Value));
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion
    }
}