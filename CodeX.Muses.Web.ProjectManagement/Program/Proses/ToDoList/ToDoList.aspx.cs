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
using CodeX.Data.Core.Dal;

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class ToDoList : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;

        protected int PageCount1 = 1;
        protected int RowCount1 = 1;
        protected int RowCountPerPage1 = 1;
        protected int CurrPage1 = 1;

        private List<StandardCode> lstTaskStatus;
        private List<vMemberTask> lstMemberTask;
        List<vProjectTaskStructure> lstTaskStructure;

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.TO_DO_LIST;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            #region initialize ComoboBox
            //String tempfilterExpression = "";
            hdnEmployeeCoordinatorID.Value = AppSession.UserLogin.EmployeeID.ToString();
            String ProjectFilterExpression = "";

            if (AppSession.UserLogin.EmployeeID != null && AppSession.UserLogin.EmployeeID != 0)
            {
                ProjectFilterExpression = String.Format("GCProjectStatus NOT IN ('{0}','{1}') AND " +
                                          "ProjectID IN (SELECT ProjectID FROM vTeamDt WHERE EmployeeCoordinatorID = '{2}' OR ListEmployeeID1 LIKE '%;{2};%')", Constant.ProjectStatus.CANCELED, Constant.ProjectStatus.COMPLETE, AppSession.UserLogin.EmployeeID);
            }
            else
            {
                ProjectFilterExpression = String.Format("GCProjectStatus NOT IN ('{0}','{1}')", Constant.ProjectStatus.CANCELED, Constant.ProjectStatus.COMPLETE);
            }

            List<Project> lstProject = BusinessLayer.GetProjectList(ProjectFilterExpression);
            Methods.SetComboBoxField(cboProject, lstProject, "ProjectName", "ProjectID");
            cboProject.SelectedIndex = 0;

            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(String.Format("IsDeleted = 0 AND IsActive = 1 AND ParentID IN ('{0}','{1}')",Constant.StandardCode.PROJECT_TASK_STATUS, Constant.StandardCode.PROJECT_TASK_TYPE));
            lstStandardCode.Insert(0,new StandardCode { StandardCodeID = "",StandardCodeName = "All", ParentID = Constant.StandardCode.PROJECT_TASK_STATUS});
            Methods.SetComboBoxField(cboStatus, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.PROJECT_TASK_STATUS && x.StandardCodeID != Constant.ProjectTaskStatus.VOID).ToList(), "StandardCodeName", "StandardCodeID");
            cboStatus.SelectedIndex = 0;

            Methods.SetComboBoxField(cboTaskType, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.PROJECT_TASK_TYPE).ToList(),"StandardCodeName","StandardCodeID");
            cboTaskType.SelectedIndex = 0;

            lstStandardCode = BusinessLayer.GetStandardCodeList(String.Format("IsDeleted = 0 AND IsActive = 1 AND ParentID = '{0}'", Constant.StandardCode.PROJECT_TASK_PRIORITY));
            Methods.SetComboBoxField(cboPriority, lstStandardCode, "StandardCodeName", "StandardCodeID");
            cboPriority.SelectedIndex = 0;

            List<Variable> lstVariable = new List<Variable>() {
                    new Variable() { Code = "", Value = "0"},
                    new Variable() { Code = "Sunday", Value = "1" }, 
                    new Variable() { Code = "Monday", Value = "2"},
                    new Variable() { Code = "Tuesday", Value = "3"},
                    new Variable() { Code = "Wednesday", Value = "4"},
                    new Variable() { Code = "Thursday", Value = "5"},
                    new Variable() { Code = "Friday", Value = "6"},
                    new Variable() { Code = "Saturday", Value = "7"},
            };
            Methods.SetComboBoxField(cboScheduledDay, lstVariable, "Code", "Value");
            cboScheduledDay.SelectedIndex = 0;
            #endregion

            txtStartDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtStartTime.Text = DateTime.Now.ToString("HH:mm");
            txtEndDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtEndTime.Text = DateTime.Now.ToString("HH:mm");
            txtScheduledStartDate.Text = DateTime.Now.ToString("dd");

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            RowCountPerPage1 = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
            BindGridView1(CurrPage1, true, ref PageCount1, ref RowCount1);
        }

        #region HTML Getter
        public string GetFLoatingTaskType() 
        {
            return Constant.ProjectTaskType.FLOATING_TASK;
        }
        public string GetProjectTaskLowPriority() 
        {
            return Constant.ProjectTaskPriority.MEDIUM;
        }

        #endregion

        protected override void SetControlProperties()
        {
            #region Project Task Scheduled
            Helper.SetControlEntrySetting(txtProjectTaskCode, new ControlEntrySetting(true, false, true), "mpTrx");
            Helper.SetControlEntrySetting(txtProjectTaskName, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboPriority, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(tacTeamDt, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtStartDate, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtStartTime, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtEndDate, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtEndTime, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false), "mpTrx");
            #endregion
        }

        public override void SetFilterParameter(ref string[] fieldListText, ref string[] fieldListValue)
        {
            fieldListText = new string[] { "ProjectTask Code", "ProjectTask Name" };
            fieldListValue = new string[] { "ProjectTaskCode", "ProjectTaskName" };
        }

        private string GetFilterExpression()
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            if (AppSession.UserLogin.EmployeeID != null && AppSession.UserLogin.EmployeeID != 0)
                filterExpression += String.Format("ProjectID = {0} AND (OwnerID = {1} OR ProjectTaskID IN (SELECT ProjectTaskID FROM MemberTask WHERE AssigneeID = {1})) AND GCProjectTaskStatus != '{2}'", cboProject.Value == null ? 0 : cboProject.Value, AppSession.UserLogin.EmployeeID, Constant.ProjectTaskStatus.VOID);
            else
                filterExpression += string.Format("ProjectID = {0} AND GCProjectTaskStatus != '{1}'", cboProject.Value == null ? 0 : cboProject.Value, Constant.ProjectTaskStatus.VOID);
            
            if (cboStatus.Value != null && cboStatus.Value.ToString() != "")
                filterExpression += String.Format(" AND GCProjectTaskStatus = '{0}'", cboStatus.Value);
            return filterExpression;
        }

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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
            else if (param[0] == "changestatus")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    if (OnSaveEditStatusRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "save1") 
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    if (OnSaveEditRecordEntityDt1(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt1(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete1")
            {
                if (OnDeleteEntityDt1(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        #region ProjectTask Scheduled
        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = GetFilterExpression();
            
            if (!chkIsShowClosed.Checked)
                filterExpression += String.Format(" AND GCProjectTaskStatus != '{0}'", Constant.ProjectTaskStatus.CLOSED);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvProjectTaskRowCount(String.Format("{0} AND GCProjectTaskType = '{1}'",filterExpression, Constant.ProjectTaskType.SCHEDULED));
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            lstTaskStatus = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsDeleted = 0 AND IsActive = 1", Constant.StandardCode.PROJECT_TASK_STATUS));

            if (AppSession.UserLogin.EmployeeID != 0)
                lstMemberTask = BusinessLayer.GetvMemberTaskList(String.Format("AssigneeID = {0} AND GCProjectTaskStatus NOT IN ('{1}','{2}')", AppSession.UserLogin.EmployeeID, Constant.ProjectTaskStatus.VOID, Constant.ProjectTaskStatus.CLOSED));
            List<vProjectTask> lstEntity = BusinessLayer.GetvProjectTaskList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
            if (lstEntity.Count > 0)
                lstTaskStructure = BusinessLayer.GetvProjectTaskStructureList(String.Format("ProjectTaskID IN ({0})", String.Join(",", lstEntity.Select(x => x.ProjectTaskID))));

            grdView.DataSource = lstEntity.Where(x => x.GCProjectTaskType == Constant.ProjectTaskType.SCHEDULED);
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
                vProjectTask entity = e.Row.DataItem as vProjectTask;

                ASPxComboBox cboTaskStatus = e.Row.FindControl("cboTaskStatus") as ASPxComboBox;
                cboTaskStatus.ClientInstanceName = string.Format("cboTaskStatus{0}", e.Row.DataItemIndex);


                int row = lstTaskStructure.Where(x => x.ProjectTaskID == entity.ProjectTaskID).Count();
                int finish = lstTaskStructure.Where(x => x.ProjectTaskID == entity.ProjectTaskID && x.PrevGCProjectTaskStatus == Constant.ProjectTaskStatus.CLOSED).Count();
                if (row != finish)
                    Methods.SetComboBoxField(cboTaskStatus, lstTaskStatus.Where(x => x.StandardCodeID != Constant.ProjectTaskStatus.CLOSED && x.StandardCodeID != Constant.ProjectTaskStatus.VOID).ToList(), "StandardCodeName", "StandardCodeID");
                else
                    Methods.SetComboBoxField(cboTaskStatus, lstTaskStatus, "StandardCodeName", "StandardCodeID");
                cboTaskStatus.Value = entity.GCProjectTaskStatus;

                HtmlInputButton btnSave = e.Row.FindControl("btnSave") as HtmlInputButton;

                if (AppSession.UserLogin.EmployeeID != 0 && entity.OwnerID != AppSession.UserLogin.EmployeeID)
                {
                    vMemberTask mt = lstMemberTask.FirstOrDefault(x => x.ProjectTaskID == entity.ProjectTaskID);
                    if (mt != null)
                    {
                        cboTaskStatus.ClientEnabled = mt.IsAllowChangeStatus;
                        if (!mt.IsAllowChangeStatus) btnSave.Style.Add("display", "none");
                    }

                    HtmlGenericControl divDetailEdit = (HtmlGenericControl)e.Row.FindControl("divDetailEdit");
                    HtmlGenericControl divDetailDelete = (HtmlGenericControl)e.Row.FindControl("divDetailDelete");
                    divDetailEdit.Style.Add("display", "none");
                    divDetailDelete.Style.Add("display", "none");
                }

                if (entity.GCProjectTaskStatus == Constant.ProjectTaskStatus.VOID || entity.GCProjectTaskStatus == Constant.ProjectTaskStatus.CLOSED)
                {
                    HtmlGenericControl divDetailEdit = (HtmlGenericControl)e.Row.FindControl("divDetailEdit");
                    HtmlGenericControl divDetailDelete = (HtmlGenericControl)e.Row.FindControl("divDetailDelete");
                    cboTaskStatus.ClientEnabled = false;
                    btnSave.Style.Add("display", "none");

                    divDetailEdit.Style.Add("display", "none");
                    divDetailDelete.Style.Add("display", "none");
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

        private void ControlToEntity(ProjectTask entity, ProjectScheduledTask pst)
        {
            entity.ProjectTaskCode = txtProjectTaskCode.Text;
            entity.ProjectTaskName = txtProjectTaskName.Text;
            entity.ProjectID = Convert.ToInt32(cboProject.Value);
            entity.TeamDtID = Convert.ToInt32(hdnTeamDtID.Value);

            if (chkIsScheduled.Checked)
            {
                pst.ScheduledTaskCode = txtProjectTaskCode.Text;
                pst.ScheduledTaskName = txtProjectTaskName.Text;
                pst.GCScheduledTaskType = Constant.ProjectScheduledTaskType.RANGED_TIME;
                pst.StartDate = Helper.GetDatePickerValue(txtStartDate.Text);
                pst.StartTime = txtStartTime.Text;
                pst.EndDate = Helper.GetDatePickerValue(txtEndDate.Text);
                pst.EndTime = txtEndTime.Text;

                if (txtScheduledStartDate.Text != "" && txtScheduledStartDate.Text != "0")
                {
                    pst.RepeatedDate = Convert.ToInt32(txtScheduledStartDate.Text);
                    pst.RepeatedDay = null;

                    String[] temp = txtStartDate.Text.Split('-');
                    DateTime start = new DateTime(Convert.ToInt32(temp[2]), Convert.ToInt32(temp[1]), Convert.ToInt32(txtScheduledStartDate.Text));
                    entity.StartDate = start;
                    entity.StartTime = txtStartTime.Text;
                    entity.EndDate = start;
                    entity.EndTime = txtEndTime.Text;
                }
                else 
                {
                    pst.RepeatedDay = Convert.ToInt32(cboScheduledDay.Value);
                    pst.RepeatedDate = null;

                    DateTime now = DateTime.Now;
                    var culture = System.Globalization.CultureInfo.CurrentCulture;
                    List<Variable> lstVariable = Enumerable.Range(0, 7)
                                                .Select(day => DateTime.Now.AddDays(day))
                                                .Select(x => new Variable() { Code = culture.DateTimeFormat.GetDayName(x.DayOfWeek), Value = x.Day.ToString() }).ToList();
                    DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(lstVariable.FirstOrDefault(x => x.Code == cboScheduledDay.Text).Value));
                    entity.StartDate = start;
                    entity.StartTime = txtStartTime.Text;
                    entity.EndDate = start;
                    entity.EndTime = txtEndTime.Text;
                }
            }
            else 
            {
                entity.StartDate = Helper.GetDatePickerValue(txtStartDate.Text);
                entity.StartTime = txtStartTime.Text;
                entity.EndDate = Helper.GetDatePickerValue(txtEndDate.Text);
                entity.EndTime = txtEndTime.Text;
            }

            entity.GCProjectTaskType = Constant.ProjectTaskType.SCHEDULED;
            if (AppSession.UserLogin.EmployeeID != null && AppSession.UserLogin.EmployeeID != 0)
                entity.OwnerID = AppSession.UserLogin.EmployeeID;
            else
                entity.OwnerID = null;
            entity.GCProjectTaskPriority = cboPriority.Value.ToString();
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveEditStatusRecordEntityDt(ref String errMessage)
        {
            bool result = true;
            try
            {
                ProjectTask entity = BusinessLayer.GetProjectTask(Convert.ToInt32(hdnEntryID.Value));
                entity.GCProjectTaskStatus = hdnStatus.Value;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateProjectTask(entity);
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }

        private bool OnSaveEditRecordEntityDt(ref String errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ProjectTaskDao entityDao = new ProjectTaskDao(ctx);
            ProjectScheduledTaskDao pstDao = new ProjectScheduledTaskDao(ctx);
            try
            {
                ProjectTask entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ProjectScheduledTask pst = pstDao.Get(Convert.ToInt32(entity.ScheduledTaskID));
                ControlToEntity(entity, pst);
                if (chkIsScheduled.Checked)
                {
                    pst.LastUpdatedBy = AppSession.UserLogin.UserID;
                    pstDao.Update(pst);
                }
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                ctx.RollBackTransaction();
                result = false;
            }
            finally 
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnSaveAddRecordEntityDt(ref String errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ProjectTaskDao entityDao = new ProjectTaskDao(ctx);
            ProjectScheduledTaskDao pstDao = new ProjectScheduledTaskDao(ctx);
            try
            {
                ProjectTask entity = new ProjectTask();
                ProjectScheduledTask pst = new ProjectScheduledTask();
                ControlToEntity(entity, pst);
                if (chkIsScheduled.Checked)
                {
                    pst.CreatedBy = AppSession.UserLogin.UserID;
                    pstDao.Insert(pst);
                    entity.ScheduledTaskID = BusinessLayer.GetProjectScheduledTaskMaxID(ctx);
                }
                else
                    entity.ScheduledTaskID = null;

                entity.GCProjectTaskStatus = Constant.ProjectTaskStatus.OPEN;
                entity.CreatedBy = AppSession.UserLogin.UserID;

                entityDao.Insert(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                ctx.RollBackTransaction();
                result = false;
            }
            finally 
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnDeleteEntityDt(ref String errMessage)
        {
            bool result = true;
            try
            {
                ProjectTask entity = BusinessLayer.GetProjectTask(Convert.ToInt32(hdnEntryID.Value));
                entity.GCProjectTaskStatus = Constant.ProjectTaskStatus.VOID;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateProjectTask(entity);
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }
        #endregion

        #region ProjectTask Floating
        private void BindGridView1(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvProjectTaskRowCount(String.Format("{0} AND GCProjectTaskType = '{1}'", filterExpression, Constant.ProjectTaskType.FLOATING_TASK));
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            lstTaskStatus = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsDeleted = 0 AND IsActive = 1", Constant.StandardCode.PROJECT_TASK_STATUS));

            if (AppSession.UserLogin.EmployeeID != 0)
                lstMemberTask = BusinessLayer.GetvMemberTaskList(String.Format("AssigneeID = {0} AND GCProjectTaskStatus NOT IN ('{1}','{2}')", AppSession.UserLogin.EmployeeID, Constant.ProjectTaskStatus.VOID, Constant.ProjectTaskStatus.CLOSED));
            
            List<vProjectTask> lstEntity = BusinessLayer.GetvProjectTaskList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
            if (lstEntity.Count > 0)
                lstTaskStructure = BusinessLayer.GetvProjectTaskStructureList(String.Format("ProjectTaskID IN ({0})", String.Join(",", lstEntity.Select(x => x.ProjectTaskID))));

            grdView1.DataSource = lstEntity.Where(x => x.GCProjectTaskType == Constant.ProjectTaskType.FLOATING_TASK);
            grdView1.DataBind();

            hdnTotalTask1.Value = lstEntity.Count().ToString();
            hdnOpen1.Value = lstEntity.Where(x => x.GCProjectTaskStatus == Constant.ProjectTaskStatus.OPEN).ToList().Count().ToString();
            hdnInProgress1.Value = lstEntity.Where(x => x.GCProjectTaskStatus == Constant.ProjectTaskStatus.IN_PROGRESS).ToList().Count().ToString();
            hdnClosed1.Value = lstEntity.Where(x => x.GCProjectTaskStatus == Constant.ProjectTaskStatus.CLOSED).ToList().Count().ToString();
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

        private void ControlToEntity1(ProjectTask entity)
        {
            entity.ProjectTaskCode = txtProjectTaskCode.Text;
            entity.ProjectTaskName = txtProjectTaskName.Text;
            entity.ProjectID = Convert.ToInt32(cboProject.Value);
            entity.TeamDtID = Convert.ToInt32(tacTeamDt.Value);
            entity.StartDate = DateTime.Now;
            entity.StartTime = DateTime.Now.ToString("HH:mmm");
            entity.EndDate = DateTime.Now;
            entity.EndTime = DateTime.Now.ToString("HH:mmm");
            entity.GCProjectTaskType = Constant.ProjectTaskType.FLOATING_TASK;
            
            if (AppSession.UserLogin.EmployeeID != null && AppSession.UserLogin.EmployeeID != 0)
                entity.OwnerID = AppSession.UserLogin.EmployeeID;
            else
                entity.OwnerID = null;
            
            entity.GCProjectTaskPriority = Constant.ProjectTaskPriority.LOW;
            entity.GCProjectTaskStatus = Constant.ProjectTaskStatus.CLOSED;
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveEditRecordEntityDt1(ref String errMessage)
        {
            bool result = true;
            try
            {
                ProjectTask entity = BusinessLayer.GetProjectTask(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity1(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateProjectTask(entity);
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }

        private bool OnSaveAddRecordEntityDt1(ref String errMessage)
        {
            bool result = true;
            try
            {
                ProjectTask entity = new ProjectTask();
                ControlToEntity1(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertProjectTask(entity);
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }

        private bool OnDeleteEntityDt1(ref String errMessage)
        {
            bool result = true;
            try
            {
                ProjectTask entity = BusinessLayer.GetProjectTask(Convert.ToInt32(hdnEntryID.Value));
                entity.GCProjectTaskStatus = Constant.ProjectTaskStatus.VOID;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateProjectTask(entity);
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }
        #endregion
    }
}