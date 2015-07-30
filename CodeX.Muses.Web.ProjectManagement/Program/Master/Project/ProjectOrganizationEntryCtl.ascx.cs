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

namespace CodeX.DTRACK.Web.ProjectManagement.Program
{
    public partial class ProjectOrganizationEntryCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            Project entity = BusinessLayer.GetProject(Convert.ToInt32(hdnID.Value));
            //hdnTeamDtID.Value = entity.TeamDtID.ToString();
            txtProjectTaskName.Text = string.Format("{0} - {1}", entity.ProjectCode, entity.ProjectName);

            BindGridView();

            //Helper.SetControlEntrySetting(txtDisplayOrder, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            //Helper.SetControlEntrySetting(txtPosition, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            //Helper.SetControlEntrySetting(tacEmployeeCoordinator, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        protected string OnGetTeamFilterExpression()
        {
            string filterExpression = "";
            filterExpression = string.Format("IsDeleted = 0", AppSession.UserLogin.SiteID, Constant.EmployeeStatus.FULL_TIME_EMPLOYED, hdnTeamDtID.Value);
            return filterExpression;
        }

        private void BindGridView()
        {
            grdPopupView.DataSource = BusinessLayer.GetvTeamDtList(string.Format("ProjectID = {0}", hdnID.Value));
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

        private void ControlToEntity(ProjectTeam entity)
        {
            entity.ProjectID = Convert.ToInt32(hdnID.Value);
            entity.TeamID = Convert.ToInt32(hdnTeamID.Value);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            try
            {
                ProjectTeam entity = new ProjectTeam();
                ControlToEntity(entity);
                BusinessLayer.InsertProjectTeam(entity);
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
                ProjectTeam entity = BusinessLayer.GetProjectTeam(Convert.ToInt32(hdnID.Value), Convert.ToInt32(hdnTeamID.Value));
                ControlToEntity(entity);
                BusinessLayer.UpdateProjectTeam(entity);
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
                BusinessLayer.DeleteProjectTeam(Convert.ToInt32(hdnID.Value), Convert.ToInt32(hdnTeamID.Value));
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