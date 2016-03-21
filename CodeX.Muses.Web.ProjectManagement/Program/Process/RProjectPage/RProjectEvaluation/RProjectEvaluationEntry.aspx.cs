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
    public partial class RProjectEvaluationEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.RPROJECT_EVALUATION;
        }

        protected string OnGetUserID()
        {
            return AppSession.UserLogin.UserID.ToString();
        }

        protected override void InitializeDataControl()
        {
            RProject entity = BusinessLayer.GetRProject(AppSession.ProjectID);
            txtProjectIndicator.Text = entity.ProjectIndicator;
            txtProjectTarget.Text = entity.ProjectTarget;
            txtProjectAchievment.Text = entity.ProjectAchievement;

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        #region Bind Grid View
        private String OnGetFilterExpression() 
        {
            String filterExpression = String.Format("ProjectID = {0} AND IsDeleted = 0", AppSession.ProjectID);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            String filterExpression = OnGetFilterExpression();
            
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvRProjectLogRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            List<vRProjectLog> lstEntity = BusinessLayer.GetvRProjectLogList(filterExpression, Constant.GridViewPageSize.GRID_MATRIX, pageIndex); ;
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vProjectTaskLog entity = e.Row.DataItem as vProjectTaskLog;

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

        private void ControlToEntity(RProjectLog entity)
        {
            entity.LogDate = Helper.GetDatePickerValue(txtLogDate.Text);
            entity.LogTime = txtLogTime.Text;
            entity.LogText = txtLogText.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;

            try
            {
                RProjectLog entity = new RProjectLog();
                ControlToEntity(entity);
                entity.ProjectID = AppSession.ProjectID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertRProjectLog(entity);
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
                RProjectLog entity = BusinessLayer.GetRProjectLog(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateRProjectLog(entity);
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
                RProjectTaskLog entity = BusinessLayer.GetRProjectTaskLog(Convert.ToInt32(hdnEntryID.Value));
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

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            if (type == "save") 
            {
                try
                {
                    RProject entity = BusinessLayer.GetRProject(AppSession.ProjectID);
                    entity.ProjectIndicator = txtProjectIndicator.Text;
                    entity.ProjectTarget = txtProjectTarget.Text;
                    entity.ProjectAchievement = txtProjectAchievment.Text;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.UpdateRProject(entity);
                    return true;
                }
                catch (Exception ex)
                {
                    Helper.InsertErrorLog(ex);
                    errMessage = ex.Message;
                    return false;
                }
            }
            return false;
        }
        #endregion
    }
}