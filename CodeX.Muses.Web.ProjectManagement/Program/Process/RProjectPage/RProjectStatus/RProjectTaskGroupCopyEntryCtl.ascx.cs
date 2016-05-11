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
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class RProjectTaskGroupCopyEntryCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnOrganizationCoordinatorID.Value = param;

            BindGridView();

            Helper.SetControlEntrySetting(txtProjectTaskGroupName, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        private void BindGridView()
        {
            string filterExpression = string.Format("ProjectID = {0} AND IsDeleted = 0", AppSession.ProjectID);
            List<RProjectTaskGroup> lstEntity = BusinessLayer.GetRProjectTaskGroupList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
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

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity(RProjectTaskGroup entity)
        {
            entity.ProjectTaskGroupName = txtProjectTaskGroupName.Text;
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            RProjectTaskGroupDao entityDao = new RProjectTaskGroupDao(ctx);
            RProjectTaskDao entityTaskDao = new RProjectTaskDao(ctx);
            RProjectTaskAssignDao entityDtDao = new RProjectTaskAssignDao(ctx);
            try
            {
                RProjectTaskGroup entity = new RProjectTaskGroup();
                ControlToEntity(entity);
                entity.ProjectID = AppSession.ProjectID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                entity.ProjectTaskGroupID = BusinessLayer.GetRProjectTaskGroupMaxID(ctx);

                List<RProjectTask> lstProjectTask = BusinessLayer.GetRProjectTaskList(string.Format("ProjectTaskGroupID = {0} AND GCProjectTaskStatus != '{1}'", hdnCopyProjectTaskGroupID.Value, Constant.ProjectTaskStatus.VOID), ctx);
                foreach (RProjectTask entityTask in lstProjectTask)
                {
                    entityTask.ProjectTaskGroupID = entity.ProjectTaskGroupID;
                    entityTask.GCProjectTaskStatus = Constant.ProjectTaskStatus.OPEN;
                    entityTask.CreatedBy = AppSession.UserLogin.UserID;
                    entityTask.LastUpdatedBy = null;
                    entityTask.LastUpdatedDate = Helper.InitializeDateTimeNull();
                    entityTask.ProjectTaskID = entityTaskDao.Insert(entityTask);

                    RProjectTaskAssign entityCoordinator = new RProjectTaskAssign();
                    entityCoordinator.ProjectTaskID = entityTask.ProjectTaskID;
                    entityCoordinator.ProjectOrganizationID = Convert.ToInt32(hdnOrganizationCoordinatorID.Value);
                    entityCoordinator.IsCoordinator = true;
                    entityDtDao.Insert(entityCoordinator);
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
    }
}