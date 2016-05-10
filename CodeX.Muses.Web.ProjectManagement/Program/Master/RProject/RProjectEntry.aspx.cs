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
    public partial class RProjectEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.RPROJECT;
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
                vRProject entity = BusinessLayer.GetvRProjectList(filterExpression)[0];
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }

            ctlEntityCode.InitializeMasterCodingControl(Constant.MasterCode.PROJECT);
            ctlEntityCode.SetControlVisibility(IsAdd);
            ctlEntityCode.SetFocus();
        }

        public override void OnAddRecord()
        {
            ctlEntityCode.SetControlVisibility(true);
        }

        protected override void SetControlProperties()
        {
            
        }

        protected override void OnControlEntrySetting()
        {
            //SetControlEntrySetting(txtProjectCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtProjectName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtStartDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtEndDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(tacProjectGroup, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(vRProject entity)
        {
            ctlEntityCode.SetText(entity.ProjectCode);
            txtProjectName.Text = entity.ProjectName;
            txtStartDate.Text = entity.StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtEndDate.Text = entity.EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            tacProjectGroup.Value = entity.ProjectGroupID.ToString();
            tacProjectGroup.Text = entity.ProjectGroupName;

            txtProjectIndicator.Text = entity.ProjectIndicator;
            txtProjectTarget.Text = entity.ProjectTarget;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(RProject entity, IDbContext ctx)
        {
            entity.ProjectName = txtProjectName.Text;
            entity.StartDate = Helper.GetDatePickerValue(txtStartDate.Text);
            entity.EndDate = Helper.GetDatePickerValue(txtEndDate.Text);
            entity.ProjectGroupID = Convert.ToInt32(tacProjectGroup.Value);
            entity.ProjectIndicator = txtProjectIndicator.Text;
            entity.ProjectTarget = txtProjectTarget.Text;
            entity.Remarks = txtRemarks.Text;
            entity.ProjectCode = ctlEntityCode.GetCode(entity.ProjectName, ctx);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            RProjectDao entityDao = new RProjectDao(ctx);
            TeamDtDao teamDtDao = new TeamDtDao(ctx);
            bool result = false;
            try
            {
                RProject entity = new RProject();

                ControlToEntity(entity, ctx);
                entity.GCProjectStatus = Constant.ProjectStatus.OPEN;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                entity.ProjectID = BusinessLayer.GetRProjectMaxID(ctx);
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
            RProjectDao entityDao = new RProjectDao(ctx);
            try
            {
                RProject entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity, ctx);
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