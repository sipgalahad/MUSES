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
    public partial class ROrganizationDtEntryCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            RProject entity = BusinessLayer.GetRProject(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.ProjectCode, entity.ProjectName);

            BindGridView();

            Helper.SetControlEntrySetting(txtDisplayOrder, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtPosition, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(tacEmployeeCoordinator, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }


        #region HTML Getter
        protected string OnGetEmployeeFilterExpression()
        {
            return string.Format("GCEmployeeStatus = '{0}' AND EmployeeID NOT IN (SELECT EmployeeID FROM vRProjectOrganizationMember WHERE ProjectID = {1}) AND IsDeleted = 0", Constant.EmployeeStatus.FULL_TIME_EMPLOYED, hdnID.Value);
        }

        protected string OnGetParentFilterExpression() 
        {
            return string.Format("ProjectID = {0} AND IsHeader = 1 AND IsDeleted = 0", hdnID.Value);
        }
        #endregion

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetvRProjectOrganizationList(string.Format("ProjectID = {0}", hdnID.Value));
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

        private void ControlToEntity(RProjectOrganization entity)
        {
            entity.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
            entity.IsHeader = chkIsHeader.Checked;
            if (hdnParent.Value != "" && hdnParent.Value != "0")
                entity.ParentID = Convert.ToInt32(hdnParent.Value);
            else
                entity.ParentID = null;
            entity.Position = txtPosition.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            RProjectOrganizationDao entityDao = new RProjectOrganizationDao(ctx);
            RProjectOrganizationMemberDao entityDtDao = new RProjectOrganizationMemberDao(ctx);
            try
            {
                RProjectOrganization entity = new RProjectOrganization();
                ControlToEntity(entity);
                entity.ProjectID = Convert.ToInt32(hdnID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                entity.ProjectOrganizationID = BusinessLayer.GetRProjectOrganizationMaxID(ctx);

                RProjectOrganizationMember entityCoordinator = new RProjectOrganizationMember();
                entityCoordinator.ProjectOrganizationID = entity.ProjectOrganizationID;
                entityCoordinator.EmployeeID = Convert.ToInt32(hdnEmployeeCoordinatorID.Value);
                entityCoordinator.IsCoordinator = true;
                entityDtDao.Insert(entityCoordinator);
                if (hdnEmployeeSave.Value != "")
                {
                    string[] lstStudentID = hdnEmployeeSave.Value.Split(',');
                    foreach (string studentID in lstStudentID)
                    {
                        RProjectOrganizationMember entityDt = new RProjectOrganizationMember();
                        entityDt.ProjectOrganizationID = entity.ProjectOrganizationID;
                        entityDt.EmployeeID = Convert.ToInt32(studentID);
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
            RProjectOrganizationDao entityDao = new RProjectOrganizationDao(ctx);
            RProjectOrganizationMemberDao entityDtDao = new RProjectOrganizationMemberDao(ctx);
            try
            {
                RProjectOrganization entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

                List<RProjectOrganizationMember> lstEntityDt = BusinessLayer.GetRProjectOrganizationMemberList(string.Format("ProjectOrganizationID = {0}", entity.ProjectOrganizationID), ctx);

                int newCoordinatorID = Convert.ToInt32(hdnEmployeeCoordinatorID.Value);
                RProjectOrganizationMember entityCoordinator = lstEntityDt.FirstOrDefault(p => p.IsCoordinator);
                if (newCoordinatorID != entityCoordinator.EmployeeID)
                {
                    entityDtDao.Delete(entityCoordinator.ProjectOrganizationID, entityCoordinator.EmployeeID);
                    entityCoordinator.EmployeeID = newCoordinatorID;
                    entityDtDao.Insert(entityCoordinator);
                }
                
                lstEntityDt.Remove(entityCoordinator);

                if (hdnEmployeeSave.Value != "")
                {
                    string[] lstStudentID = hdnEmployeeSave.Value.Split(',');
                    foreach (string studentID in lstStudentID)
                    {
                        RProjectOrganizationMember entityDt = lstEntityDt.FirstOrDefault(p => p.EmployeeID == Convert.ToInt32(studentID));
                        if (entityDt == null)
                        {
                            entityDt = new RProjectOrganizationMember();
                            entityDt.ProjectOrganizationID = entity.ProjectOrganizationID;
                            entityDt.EmployeeID = Convert.ToInt32(studentID);
                            entityDt.IsCoordinator = false;
                            entityDtDao.Insert(entityDt);
                        }
                        else
                            lstEntityDt.Remove(entityDt);
                    }
                }

                foreach (RProjectOrganizationMember entityDt in lstEntityDt)
                {
                    entityDtDao.Delete(entityDt.ProjectOrganizationID, entityDt.EmployeeID);
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
    }
}