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
    public partial class OrganizationDtEntryCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            Project entity = BusinessLayer.GetProject(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.ProjectCode, entity.ProjectName);

            BindGridView();

            Helper.SetControlEntrySetting(txtDisplayOrder, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtPosition, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(tacEmployeeCoordinator, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }


        #region HTML Getter
        protected string OnGetEmployeeFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND GCEmployeeStatus = '{1}' AND IsDeleted = 0", AppSession.UserLogin.SiteID, Constant.EmployeeStatus.FULL_TIME_EMPLOYED);
        }

        protected string OnGetTeamDtFilterExpression() 
        {
            return string.Format("IsHeader = 1 AND IsDeleted = 0");
        }
        #endregion

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetvTeamDtList(string.Format("ProjectID = {0} ORDER BY DisplayOrder ASC", hdnID.Value));
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

        private void ControlToEntity(TeamDt entity)
        {
            entity.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
            entity.EmployeeCoordinatorID = Convert.ToInt32(hdnEmployeeCoordinatorID.Value);
            entity.IsHeader = chkIsHeader.Checked;
            if (hdnReportTo.Value != "" && hdnReportTo.Value != "0")
                entity.ReportTo = Convert.ToInt32(hdnReportTo.Value);
            else
                entity.ReportTo = null;
            entity.Position = txtPosition.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TeamDtDao entityDao = new TeamDtDao(ctx);
            TeamDtMemberDao entityDtDao = new TeamDtMemberDao(ctx);
            try
            {
                TeamDt entity = new TeamDt();
                ControlToEntity(entity);
                entity.ProjectID = Convert.ToInt32(hdnID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                entity.TeamDtID = BusinessLayer.GetTeamDtMaxID(ctx);

                if (hdnEmployeeSave.Value != "")
                {
                    string[] lstStudentID = hdnEmployeeSave.Value.Split(',');
                    foreach (string studentID in lstStudentID)
                    {
                        TeamDtMember entityDt = new TeamDtMember();
                        entityDt.TeamDtID = entity.TeamDtID;
                        entityDt.EmployeeID = Convert.ToInt32(studentID);
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
            TeamDtDao entityDao = new TeamDtDao(ctx);
            TeamDtMemberDao entityDtDao = new TeamDtMemberDao(ctx);
            try
            {
                TeamDt entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

                List<TeamDtMember> lstEntityDt = BusinessLayer.GetTeamDtMemberList(string.Format("TeamDtID = {0}", entity.TeamDtID), ctx);
                if (hdnEmployeeSave.Value != "")
                {
                    string[] lstStudentID = hdnEmployeeSave.Value.Split(',');
                    foreach (string studentID in lstStudentID)
                    {
                        TeamDtMember entityDt = lstEntityDt.FirstOrDefault(p => p.EmployeeID == Convert.ToInt32(studentID));
                        if (entityDt == null)
                        {
                            entityDt = new TeamDtMember();
                            entityDt.TeamDtID = entity.TeamDtID;
                            entityDt.EmployeeID = Convert.ToInt32(studentID);
                            entityDtDao.Insert(entityDt);
                        }
                        else
                            lstEntityDt.Remove(entityDt);
                    }
                }

                foreach (TeamDtMember entityDt in lstEntityDt)
                {
                    entityDtDao.Delete(entityDt.TeamDtID, entityDt.EmployeeID);
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
                TeamDt entity = BusinessLayer.GetTeamDt(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateTeamDt(entity);
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