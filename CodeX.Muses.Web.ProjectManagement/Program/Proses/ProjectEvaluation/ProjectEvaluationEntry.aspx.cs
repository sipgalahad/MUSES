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
    public partial class ProjectEvaluationEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.PROJECT_EVALUATION;
        }

        protected override void InitializeDataControl()
        {
            txtStartDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtStartTime.Text = DateTime.Now.ToString("HH:mm");

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;

            Project entity = BusinessLayer.GetProject(AppSession.ProjectID);
            txtProjectIndicator.Text = entity.ProjectIndicator;
            txtProjectTarget.Text = entity.ProjectTarget;
            txtProjectAchievment.Text = entity.ProjectAchievement;
    
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        #region Bind Grid View
        private String OnGetFilterExpression() 
        {
            String filterExpression = String.Format("ProjectID = {0} AND ProjectTaskID IS NULL AND IsDeleted = 0", AppSession.ProjectID);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            String filterExpression = OnGetFilterExpression();
            
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvProjectTaskLogRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            List<vProjectTaskLog> lstEntity = BusinessLayer.GetvProjectTaskLogList(filterExpression, Constant.GridViewPageSize.GRID_MATRIX, pageIndex); ;
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vProjectTaskLog entity = e.Row.DataItem as vProjectTaskLog;

                if (entity.EmployeeID != AppSession.UserLogin.EmployeeID) 
                {
                    HtmlGenericControl divDetailEdit = e.Row.FindControl("divDetailEdit") as HtmlGenericControl;
                    HtmlGenericControl divDetailDelete = e.Row.FindControl("divDetailDelete") as HtmlGenericControl;
                    divDetailEdit.Style.Add("display", "none");
                    divDetailDelete.Style.Add("display", "none");
                }
                //if(lstLog.Where(x => x.ProjectTaskID == entity.ProjectTaskID).Count() > 0)
                //    lblProjectTaskName.Attributes.Add("class", "lblLink");
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

        #region Process Detail
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

        private void ControlToEntity(ProjectTaskLog entity)
        {
            entity.NoteName = txtNoteName.Text;
            entity.NoteDate = Helper.GetDatePickerValue(txtStartDate.Text);
            entity.NoteTime = txtStartTime.Text;
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;

            try
            {
                ProjectTaskLog entity = new ProjectTaskLog();
                ControlToEntity(entity);
                if (AppSession.UserLogin.EmployeeID != 0)
                    entity.EmployeeID = AppSession.UserLogin.EmployeeID;
                else
                    entity.EmployeeID = null;
                entity.ProjectID = AppSession.ProjectID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertProjectTaskLog(entity);
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            try
            {
                ProjectTaskLog entity = BusinessLayer.GetProjectTaskLog(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateProjectTaskLog(entity);
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                ProjectTaskLog entity = BusinessLayer.GetProjectTaskLog(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateProjectTaskLog(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            if (type == "save") 
            {
                try
                {
                    Project entity = BusinessLayer.GetProject(AppSession.ProjectID);
                    entity.ProjectIndicator = txtProjectIndicator.Text;
                    entity.ProjectTarget = txtProjectTarget.Text;
                    entity.ProjectAchievement = txtProjectAchievment.Text;
                    BusinessLayer.UpdateProject(entity);
                }
                catch (Exception ex)
                {
                    errMessage = ex.Message;
                }
            }
            return result;
        }
        #endregion
    }
}