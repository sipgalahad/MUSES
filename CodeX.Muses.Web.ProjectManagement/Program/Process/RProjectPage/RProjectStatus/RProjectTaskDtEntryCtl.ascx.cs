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
    public partial class RProjectTaskDtEntryCtl : BaseViewPopupCtl
    {
        protected string OnGetDueDateRange()
        {
            return Constant.DueDateType.RANGE;
        }
        protected string OnGetDueDateEndDate()
        {
            return Constant.DueDateType.DUE_DATE_END_DATE;
        }
        protected string OnGetDueDateNoDueDate()
        {
            return Constant.DueDateType.NO_DUE_DATE;
        }
        protected string OnGetUserID()
        {
            return AppSession.UserLogin.UserID.ToString();
        }

        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');
            hdnID.Value = temp[0];
            hdnProjectOrganizationID.Value = temp[1];

            RProjectOrganization entityOrganization = BusinessLayer.GetRProjectOrganization(Convert.ToInt32(hdnProjectOrganizationID.Value));
            txtPosition.Text = hdnPosition.Value = entityOrganization.Position;

            RProjectTaskGroup entity = BusinessLayer.GetRProjectTaskGroup(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0}", entity.ProjectTaskGroupName);

            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(String.Format("ParentID IN ('{0}','{1}','{2}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PROJECT_TASK_PRIORITY, Constant.StandardCode.PROJECT_TASK_STATUS, Constant.StandardCode.DUE_DATE_TYPE));
            List<StandardCode> lstProjectStatus = lstStandardCode.Where(p => p.ParentID == Constant.StandardCode.PROJECT_TASK_STATUS).ToList();
            Methods.SetComboBoxField(cboPriority, lstStandardCode.Where(p => p.ParentID == Constant.StandardCode.PROJECT_TASK_PRIORITY).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboStatus, lstProjectStatus, "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboDueDateType, lstStandardCode.Where(p => p.ParentID == Constant.StandardCode.DUE_DATE_TYPE).ToList(), "StandardCodeName", "StandardCodeID");
            cboPriority.SelectedIndex = 0;

            Repeater rptFilterStatus = (Repeater)ddeFilterStatus.FindControl("rptFilterStatus");
            rptFilterStatus.DataSource = lstProjectStatus;
            rptFilterStatus.DataBind();

            BindGridView();

            Helper.SetControlEntrySetting(cboPriority, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(cboStatus, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(cboDueDateType, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtProjectTaskName, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(tacOrganizationCoordinator, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        protected void rptFilterStatus_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                StandardCode obj = (StandardCode)e.Item.DataItem;
                CheckBox chkFilterStatus = (CheckBox)e.Item.FindControl("chkFilterStatus");
                if (obj.StandardCodeID != Constant.ProjectTaskStatus.CLOSED && obj.StandardCodeID != Constant.ProjectTaskStatus.VOID)
                    chkFilterStatus.Checked = true;
                chkFilterStatus.Attributes.Add("standardcodename", obj.StandardCodeName);
                chkFilterStatus.Attributes.Add("standardcodeid", obj.StandardCodeID);
            }
        }

        private RProjectStatusList DetailPage
        {
            get { return (RProjectStatusList)Page; }
        }

        #region HTML Getter
        protected string OnGetOrganizationFilterExpression()
        {
            if (AppSession.IsMyProject)
                return string.Format("ProjectID = {0} AND DisplayPath LIKE '%/{1}/%' AND IsDeleted = 0", AppSession.ProjectID, DetailPage.OnGetMyProjectOrganizationID());
            return string.Format("ProjectID = {0} AND IsDeleted = 0", AppSession.ProjectID);
        }
        #endregion

        private void BindGridView()
        {
            string filterExpression = "";
            if (chkIsShowAllTask.Checked)
            {
                if (AppSession.IsMyProject)
                    filterExpression = string.Format("ProjectTaskGroupID IN ({0}) AND GCProjectTaskStatus != '{1}' AND ProjectTaskID IN (SELECT ProjectTaskID FROM vRProjectTaskAssign WHERE DisplayPath LIKE '%/{2}/%') ORDER BY GCProjectTaskPriority DESC", hdnID.Value, Constant.ProjectTaskStatus.VOID, DetailPage.OnGetMyProjectOrganizationID());
                else
                    filterExpression = string.Format("ProjectTaskGroupID IN ({0}) AND GCProjectTaskStatus != '{1}' ORDER BY GCProjectTaskPriority DESC", hdnID.Value, Constant.ProjectTaskStatus.VOID);
            }
            else
                filterExpression = string.Format("ProjectTaskGroupID IN ({0}) AND GCProjectTaskStatus != '{1}' AND ProjectTaskID IN (SELECT ProjectTaskID FROM vRProjectTaskAssign WHERE DisplayPath LIKE '%/{2}/%') ORDER BY GCProjectTaskPriority DESC", hdnID.Value, Constant.ProjectTaskStatus.VOID, hdnProjectOrganizationID.Value);

            lstEntity = BusinessLayer.GetvRProjectTaskList(filterExpression);
            totalTask = lstEntity.Count;

            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(String.Format("ParentID IN ('{0}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PROJECT_TASK_STATUS));
            rptRemarks.DataSource = lstStandardCode.Where(p => p.StandardCodeID != Constant.ProjectTaskStatus.VOID);
            rptRemarks.DataBind();

            string[] lstProjectStatus = hdnLstFilterStatusID.Value.Split(',');
            List<vRProjectTask> lstEntity1 = null;
            lstEntity1 = lstEntity.Where(p => lstProjectStatus.Contains(p.GCProjectTaskStatus)).ToList();
            grdView.DataSource = lstEntity1;
            grdView.DataBind();
        }

        List<vRProjectTask> lstEntity = null;
        int totalTask = 0;
        protected void rptRemarks_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                StandardCode entity = e.Item.DataItem as StandardCode;
                HtmlTableCell tdStatistic = (HtmlTableCell)e.Item.FindControl("tdStatistic");
                tdStatistic.InnerHtml = string.Format("{0}/{1}", lstEntity.Where(p => p.GCProjectTaskStatus == entity.StandardCodeID).Count(), totalTask);
            }
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vRProjectTask entity = e.Row.DataItem as vRProjectTask;
                e.Row.CssClass = string.Format("tr{0}", entity.GCProjectTaskStatus.Split('^')[1]);
            }
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        private void BindGridView2()
        {
            string filterExpression = "1 = 0";
            if (hdnProjectTaskID.Value != "")
                filterExpression = string.Format("ProjectTaskID = {0} AND IsDeleted = 0 ORDER BY CreatedDate DESC", hdnProjectTaskID.Value);
            grdView2.DataSource = BusinessLayer.GetvRProjectTaskLogList(filterExpression);
            grdView2.DataBind();
        }

        protected void cbpViewPopup2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView2();
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
                if (hdnEntryID.Value.ToString() != "")
                {
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
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

        private void ControlToEntity(RProjectTask entity)
        {
            entity.GCProjectTaskPriority = cboPriority.Value.ToString();
            entity.GCProjectTaskStatus = cboStatus.Value.ToString();
            entity.GCDueDateType = cboDueDateType.Value.ToString();
            entity.ProjectTaskName = txtProjectTaskName.Text;
            entity.Remarks = txtRemarks.Text;

            if (entity.GCDueDateType == Constant.DueDateType.RANGE)
            {
                entity.StartDate = Helper.GetDatePickerValue(txtStartDate);
                entity.EndDate = Helper.GetDatePickerValue(txtEndDate);
            }
            else if (entity.GCDueDateType == Constant.DueDateType.DUE_DATE_END_DATE)
                entity.EndDate = Helper.GetDatePickerValue(txtDueDateEndDate);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            RProjectTaskDao entityDao = new RProjectTaskDao(ctx);
            RProjectTaskAssignDao entityDtDao = new RProjectTaskAssignDao(ctx);
            try
            {
                RProjectTask entity = new RProjectTask();
                ControlToEntity(entity);
                entity.ProjectTaskGroupID = Convert.ToInt32(hdnID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                entity.ProjectTaskID = BusinessLayer.GetRProjectTaskMaxID(ctx);

                RProjectTaskAssign entityCoordinator = new RProjectTaskAssign();
                entityCoordinator.ProjectTaskID = entity.ProjectTaskID;
                entityCoordinator.ProjectOrganizationID = Convert.ToInt32(hdnOrganizationCoordinatorID.Value);
                entityCoordinator.IsCoordinator = true;
                entityDtDao.Insert(entityCoordinator);
                if (hdnOrganizationSave.Value != "")
                {
                    string[] lstStudentID = hdnOrganizationSave.Value.Split(',');
                    foreach (string studentID in lstStudentID)
                    {
                        RProjectTaskAssign entityDt = new RProjectTaskAssign();
                        entityDt.ProjectTaskID = entity.ProjectTaskID;
                        entityDt.ProjectOrganizationID = Convert.ToInt32(studentID);
                        entityDt.IsCoordinator = false;
                        entityDtDao.Insert(entityDt);
                    }
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

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            RProjectTaskDao entityDao = new RProjectTaskDao(ctx);
            RProjectTaskAssignDao entityDtDao = new RProjectTaskAssignDao(ctx);
            try
            {
                RProjectTask entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

                List<RProjectTaskAssign> lstEntityDt = BusinessLayer.GetRProjectTaskAssignList(string.Format("ProjectTaskID = {0}", entity.ProjectTaskID), ctx);

                RProjectTaskAssign entityCoordinator = lstEntityDt.FirstOrDefault(p => p.IsCoordinator);
                entityCoordinator.ProjectOrganizationID = Convert.ToInt32(hdnOrganizationCoordinatorID.Value);
                entityDtDao.Update(entityCoordinator);
                lstEntityDt.Remove(entityCoordinator);

                if (hdnOrganizationSave.Value != "")
                {
                    string[] lstStudentID = hdnOrganizationSave.Value.Split(',');
                    foreach (string studentID in lstStudentID)
                    {
                        RProjectTaskAssign entityDt = lstEntityDt.FirstOrDefault(p => p.ProjectOrganizationID == Convert.ToInt32(studentID));
                        if (entityDt == null)
                        {
                            entityDt = new RProjectTaskAssign();
                            entityDt.ProjectTaskID = entity.ProjectTaskID;
                            entityDt.ProjectOrganizationID = Convert.ToInt32(studentID);
                            entityDt.IsCoordinator = false;
                            entityDtDao.Insert(entityDt);
                        }
                        else
                            lstEntityDt.Remove(entityDt);
                    }
                }

                foreach (RProjectTaskAssign entityDt in lstEntityDt)
                {
                    entityDtDao.Delete(entityDt.ProjectTaskAssignID);
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

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                RProjectOrganization entity = BusinessLayer.GetRProjectOrganization(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateRProjectOrganization(entity);
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

        #region Process Detail2
        protected void cbpProcessPopup2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntry2ID.Value.ToString() != "")
                {
                    if (OnSaveEditRecordEntityDt2(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt2(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                if (OnDeleteEntityDt2(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity2(RProjectTaskLog entity)
        {
            entity.LogDate = Helper.GetDatePickerValue(txtLogDate.Text);
            entity.LogTime = txtLogTime.Text;
            entity.LogText = txtLogText.Text;
        }

        private bool OnSaveAddRecordEntityDt2(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            RProjectTaskLogDao entityDao = new RProjectTaskLogDao(ctx);
            try
            {
                RProjectTaskLog entity = new RProjectTaskLog();
                ControlToEntity2(entity);
                entity.ProjectTaskID = Convert.ToInt32(hdnProjectTaskID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                
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

        private bool OnSaveEditRecordEntityDt2(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            RProjectTaskLogDao entityDao = new RProjectTaskLogDao(ctx);
            try
            {
                RProjectTaskLog entity = entityDao.Get(Convert.ToInt32(hdnEntry2ID.Value));
                ControlToEntity2(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

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

        private bool OnDeleteEntityDt2(ref string errMessage)
        {
            try
            {
                RProjectTaskLog entity = BusinessLayer.GetRProjectTaskLog(Convert.ToInt32(hdnEntry2ID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateRProjectTaskLog(entity);
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