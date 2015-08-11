using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;
using CodeX.Common;
using System.Data;

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class ProjectEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.PROJECT;
        }

        #region Html Getter
        protected string OnGetEmployeeFilterExpression() 
        {
            return "IsDeleted = 0";
        }
        protected string OnGetProjectFilterExpression() 
        {
            return String.Format("IsHeader = 1 AND GCProjectStatus != '{0}'", Constant.ProjectStatus.CANCELED);
        }
        #endregion

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                String filterExpression = String.Format("ProjectID = {0}", Convert.ToInt32(ID));
                vProject entity = BusinessLayer.GetvProjectList(filterExpression)[0];
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }

            txtProjectCode.Focus();
        }

        protected override void SetControlProperties()
        {
            
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtProjectCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtProjectName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtStartDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtEndDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(chkIsHeader, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(tacPIC, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(vProject entity)
        {
            txtProjectCode.Text = entity.ProjectCode;
            txtProjectName.Text = entity.ProjectName;
            txtStartDate.Text = entity.StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtEndDate.Text = entity.EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            chkIsHeader.Checked = entity.IsHeader;
            tacParent.Value = entity.ParentID.ToString();
            hdnParentID.Value = entity.ParentID.ToString();
            hdnProjectLevel.Value = entity.ProjectLevel.ToString();
            tacParent.Text = entity.ParentProjectName;

            tacPIC.Value = entity.PersonInCharge.ToString();
            tacPIC.Text = entity.EmployeeName;
            txtProjectIndicator.Text = entity.ProjectIndicator;
            txtProjectTarget.Text = entity.ProjectTarget;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(Project entity)
        {
            entity.ProjectCode = txtProjectCode.Text;
            entity.ProjectName = txtProjectName.Text;
            entity.StartDate = Helper.GetDatePickerValue(txtStartDate.Text);
            entity.EndDate = Helper.GetDatePickerValue(txtEndDate.Text);
            entity.IsHeader = chkIsHeader.Checked;
            if (hdnParentID.Value != "" && hdnParentID.Value != "0")
            {
                entity.ParentID = Convert.ToInt32(hdnParentID.Value);
                entity.ProjectLevel = Convert.ToInt32(hdnProjectLevel.Value) + 1;
            }
            else 
            {
                entity.ParentID = null;
                entity.ProjectLevel = 0;
            } 
            entity.PersonInCharge = Convert.ToInt32(tacPIC.Value);
            entity.ProjectIndicator = txtProjectIndicator.Text;
            entity.ProjectTarget = txtProjectTarget.Text;
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            ProjectDao entityDao = new ProjectDao(ctx);
            bool result = false;
            try
            {
                Project entity = new Project();

                ControlToEntity(entity);
                entity.GCProjectStatus = Constant.ProjectStatus.OPEN;
                entity.SiteID = AppSession.UserLogin.SiteID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entity.CreatedDate = DateTime.Now;
                entityDao.Insert(entity);
                entity.ProjectID = BusinessLayer.GetProjectMaxID(ctx);
                retval = entity.ProjectID.ToString();
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ProjectDao entityDao = new ProjectDao(ctx);
            try
            {
                Project entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
    }
}