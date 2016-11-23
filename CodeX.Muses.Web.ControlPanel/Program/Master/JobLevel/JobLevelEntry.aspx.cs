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

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class JobLevelEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.JOB_LEVEL;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                JobLevel entity = BusinessLayer.GetJobLevel(Convert.ToInt32(ID));
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            ctlEntityCode.InitializeMasterCodingControl(Constant.MasterCode.JOB_LEVEL);
            ctlEntityCode.SetControlVisibility(IsAdd);
            ctlEntityCode.SetFocus();
        }

        public override void OnAddRecord()
        {
            ctlEntityCode.SetControlVisibility(true);
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.JOB_LEVEL_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboJobLevelType, lstSc, "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtJobLevelName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboJobLevelType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtFromWorkingYears, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtToWorkingYears, new ControlEntrySetting(true, true, true));
            //SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
           
        }

        private void EntityToControl(JobLevel entity)
        {
            ctlEntityCode.SetText(entity.JobLevelCode);
            txtJobLevelName.Text = entity.JobLevelName;
            cboJobLevelType.Value = entity.GCJobLevelType.ToString();
            txtFromWorkingYears.Text = entity.FromWorkingYears.ToString();
            txtToWorkingYears.Text = entity.ToWorkingYears.ToString();
            //txtRemarks.Text = entity.Remarks;
      
        }

        private void ControlToEntity(JobLevel entity, IDbContext ctx)
        {
            entity.JobLevelName = txtJobLevelName.Text;
            entity.GCJobLevelType = cboJobLevelType.Value.ToString();
            entity.FromWorkingYears = Convert.ToInt16(txtFromWorkingYears.Text);
            entity.ToWorkingYears = Convert.ToInt16(txtToWorkingYears.Text);
            //entity.Remarks = txtRemarks.Text;
            entity.JobLevelCode = ctlEntityCode.GetCode(entity.JobLevelName, ctx);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            JobLevelDao entityDao = new JobLevelDao(ctx);
            bool result = false;
            try
            {
                JobLevel entity = new JobLevel();
                ControlToEntity(entity, ctx);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                retval = entityDao.Insert(entity).ToString();
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
            IDbContext ctx = DbFactory.Configure(true);
            JobLevelDao entityDao = new JobLevelDao(ctx);
            bool result = false;
            try
            {
                JobLevel entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity, ctx);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);
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
    }
}