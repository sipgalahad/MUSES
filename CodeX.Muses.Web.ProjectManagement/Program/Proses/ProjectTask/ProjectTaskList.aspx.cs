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
using CodeX.Data.Core.Dal;
using System.Text.RegularExpressions;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class ProjectTaskList : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        private List<MemberTask> lstMemberTask = null;

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.PROJECT_TASK;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            #region initialize ComoboBox
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

            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(String.Format("IsDeleted = 0 AND IsActive = 1 AND ParentID = '{0}'",Constant.StandardCode.PROJECT_TASK_PRIORITY));
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

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        #region HTML Get FilterExpression
        protected string OnGetEmployeeFilterExpression()
        {
            string filterExpression = "";
            filterExpression = String.Format("SiteID = '{0}' AND GCEmployeeStatus = '{1}' AND IsDeleted = 0", AppSession.UserLogin.SiteID, Constant.EmployeeStatus.FULL_TIME_EMPLOYED);
            return filterExpression;
        }
        #endregion

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
                //filterExpression += String.Format("TeamDtID IN (SELECT TeamDtID FROM vTeamDt WHERE EmployeeCoordinatorID = {0} OR ListEmployeeID LIKE '%{0}%') " +
                //                                    "AND ProjectID = {1} AND OwnerID = {0} AND GCProjectTaskStatus NOT IN ('{2}','{3}')", AppSession.UserLogin.EmployeeID, cboProject.Value == null ? 0 : cboProject.Value, Constant.ProjectTaskStatus.VOID, Constant.ProjectTaskStatus.CLOSED);
                filterExpression += String.Format("ProjectID = {1} AND OwnerID = {0} AND GCProjectTaskStatus NOT IN ('{2}','{3}')", AppSession.UserLogin.EmployeeID, cboProject.Value == null ? 0 : cboProject.Value, Constant.ProjectTaskStatus.VOID, Constant.ProjectTaskStatus.CLOSED);
            else
                filterExpression += string.Format("ProjectID = {0} AND GCProjectTaskStatus NOT IN ('{1}','{2}')", cboProject.Value == null  ? 0 : cboProject.Value, Constant.ProjectTaskStatus.VOID, Constant.ProjectTaskStatus.CLOSED);
            filterExpression += String.Format(" AND GCProjectTaskType = '{0}'", Constant.ProjectTaskType.SCHEDULED);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvProjectTaskRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vProjectTask> lstEntity = BusinessLayer.GetvProjectTaskList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
            if(lstEntity.Count > 0) 
                lstMemberTask = BusinessLayer.GetMemberTaskList(String.Format("ProjectTaskID IN ({0})", String.Join(",",lstEntity.Select(x => x.ProjectTaskID))));
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        private void BindPopupGridView()
        {
            grdPopupView.DataSource = BusinessLayer.GetvMemberTaskList(string.Format("ProjectTaskID = {0}", hdnPopupID.Value));
            grdPopupView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vProjectTask entity = e.Row.DataItem as vProjectTask;

                if (entity.OwnerID != AppSession.UserLogin.EmployeeID) 
                {
                    HtmlGenericControl divDetailEdit = (HtmlGenericControl)e.Row.FindControl("divDetailEdit");
                    HtmlGenericControl divDetailDelete = (HtmlGenericControl)e.Row.FindControl("divDetailDelete");
                    divDetailEdit.Style.Add("display", "none");
                    divDetailDelete.Style.Add("display", "none");
                }
                HtmlGenericControl lblAssign = (HtmlGenericControl)e.Row.FindControl("lblAssign");
                MemberTask temp = lstMemberTask.FirstOrDefault(x => x.ProjectTaskID == entity.ProjectTaskID);
                if(temp != null)lblAssign.InnerHtml = GetLabel("Assign*");
                else lblAssign.InnerHtml = GetLabel("Assign");
                //if (entity.EmployeeCoordinatorID != AppSession.UserLogin.EmployeeID && AppSession.UserLogin.EmployeeID != 0) 
                //{
                //    HtmlGenericControl lblAssign = (HtmlGenericControl)e.Row.FindControl("lblAssign");    
                //    lblAssign.Style.Add("display", "none");
                //}
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

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            ProjectTask entity = BusinessLayer.GetProjectTask(Convert.ToInt32(hdnPopupID.Value));
            hdnTeamDtID.Value = entity.TeamDtID.ToString();
            BindPopupGridView();
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

        public bool OnSaveAddRecordEntity(ref Int32 ProjectTaskID, IDbContext ctx) 
        {
            bool result = true;
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
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                result = false;
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

        #region Process Popup Detail
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
                    if (OnPopupSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnPopupSaveAddRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                if (OnPopupDeleteEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else if (param[0] == "email") 
            {
                if (OnPopupSendEmail(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity(MemberTask entity)
        {
            entity.ProjectTaskID = Convert.ToInt32(hdnID.Value);
            entity.AssigneeID = Convert.ToInt32(hdnEmployeeCoordinatorID.Value);
            entity.IsAllowChangeStatus = chkIsAllowChangeStatus.Checked;
        }

        private bool OnPopupSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            try
            {
                MemberTask entity = new MemberTask();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertMemberTask(entity);
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }

        private bool OnPopupSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            try
            {
                MemberTask entity = BusinessLayer.GetMemberTask(Convert.ToInt32(hdnID.Value), Convert.ToInt32(hdnEntryID.Value));
                entity.IsAllowChangeStatus = chkIsAllowChangeStatus.Checked;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateMemberTask(entity);
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }

        private bool OnPopupDeleteEntityDt(ref string errMessage)
        {
            try
            {
                MemberTask entity = BusinessLayer.GetMemberTask(Convert.ToInt32(hdnID.Value), Convert.ToInt32(hdnEntryID.Value));
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateMemberTask(entity);
                BusinessLayer.DeleteMemberTask(Convert.ToInt32(hdnID.Value), Convert.ToInt32(hdnEntryID.Value));
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnPopupSendEmail(ref string errMessage) 
        {
            bool result = true;
            try
            {
                ProjectTask task = BusinessLayer.GetProjectTask(Convert.ToInt32(hdnPopupID.Value));
                List<Employee> lstEmployee = BusinessLayer.GetEmployeeList(String.Format("EmployeeID = {0}", hdnSelectedValue.Value));
                SendEmail(task, lstEmployee);
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }

        public String GetSmtpAddress(String email)
        {
            String[] data = email.Split('@');
            String SmtpAddress = "";
            switch (data[1])
            {
                case "hotmail.com": SmtpAddress = string.Format("smtp.live.com"); break;
                case "gmail.com": SmtpAddress = string.Format("smtp.gmail.com"); break;
                case "yahoo.com": SmtpAddress = string.Format("smtp.mail.yahoo.com"); break;
                default: SmtpAddress = String.Format("smtp.{0}", data[1]); break;
            }
            return SmtpAddress;
        }

        public Int32 GetPort(String email)
        {
            String[] data = email.Split('@');
            Int32 port = 0;
            switch (data[1])
            {
                case "hotmail.com": port = 587; break;
                case "gmail.com": port = 587; break;
                case "yahoo.com": port = 587; break;
                default: port = 25; break;
            }
            return port;
        }

        public void SendEmail(ProjectTask task, List<Employee> lstEmployee)
        {
            string emailFrom = "";
            string password = "";
            Employee user = BusinessLayer.GetEmployee(Convert.ToInt32(AppSession.UserLogin.EmployeeID));
            if (user != null) 
            {
                emailFrom = user.EmailAddress1 != "" ? user.EmailAddress1 : user.EmailAddress2;
                password = "";
            }
            
            //string emailTo = String.Join(";", lstEmployee.Select(x => x.EmailAddress1));
            string subject = "Remainder Kegiatan";
            string body = String.Format("Remainder kegiatan \"{0}\" dengan deadline {1}", task.ProjectTaskName, task.EndDate.ToString(Constant.FormatString.DATE_REPORT_FORMAT));//BusinessLayer.GetTemplateText(TemplateID).TemplateContent;

            string smtpAddress = GetSmtpAddress(emailFrom);
            int portNumber = GetPort(emailFrom);
            bool enableSSL = true;

            #region Send Email
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                foreach (String email in lstEmployee.Select(x => x.EmailAddress1))
                    mail.To.Add(email);
                
                mail.Subject = subject;
                mail.Body = body;//ConvertMessage(emailTo, body);
                mail.IsBodyHtml = true;
                // Can set to false, if you are sending pure text.

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
            #endregion
        }
        #endregion
    }
}