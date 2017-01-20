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
    public partial class JobLevelWorkYearsEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.JOB_LEVEL_WORK_YEARS;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                JobLevelWorkYears entity = BusinessLayer.GetJobLevelWorkYears(Convert.ToInt32(ID));
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            //ctlEntityCode.InitializeMasterCodingControl(Constant.MasterCode.JOB_LEVEL);
            //ctlEntityCode.SetControlVisibility(IsAdd);
            //ctlEntityCode.SetFocus();
        }

        public override void OnAddRecord()
        {
            //ctlEntityCode.SetControlVisibility(true);
        }

        protected override void SetControlProperties()
        {
            //List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.JOB_LEVEL_TYPE));
            //Methods.SetComboBoxField<StandardCode>(cboJobLevelType, lstSc, "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtJobLevelWorkYearsName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtFromWorkYears, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtToWorkYears, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
           
        }

        private void EntityToControl(JobLevelWorkYears entity)
        {
            txtJobLevelWorkYearsName.Text = entity.JobLevelWorkYearsName;
            txtFromWorkYears.Text = entity.FromWorkYears.ToString();
            txtToWorkYears.Text = entity.ToWorkYears.ToString();
            txtRemarks.Text = entity.Remarks;
      
        }

        private void ControlToEntity(JobLevelWorkYears entity, IDbContext ctx)
        {
            entity.JobLevelWorkYearsName = txtJobLevelWorkYearsName.Text;
            entity.FromWorkYears = Convert.ToInt16(txtFromWorkYears.Text);
            entity.ToWorkYears = Convert.ToInt16(txtToWorkYears.Text);
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            JobLevelWorkYearsDao entityDao = new JobLevelWorkYearsDao(ctx);
            bool result = false;
            try
            {
                JobLevelWorkYears entity = new JobLevelWorkYears();
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
            JobLevelWorkYearsDao entityDao = new JobLevelWorkYearsDao(ctx);
            bool result = false;
            try
            {
                JobLevelWorkYears entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
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