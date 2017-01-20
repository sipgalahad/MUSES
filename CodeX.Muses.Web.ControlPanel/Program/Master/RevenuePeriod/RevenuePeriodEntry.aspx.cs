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
    public partial class RevenuePeriodEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.REVENUE_PERIOD;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                RevenuePeriod entity = BusinessLayer.GetRevenuePeriod(Convert.ToInt32(ID));
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
            SetControlEntrySetting(txtStartDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtEndDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
           
        }

        private void EntityToControl(RevenuePeriod entity)
        {
            txtStartDate.Text = entity.StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtEndDate.Text = entity.EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtRemarks.Text = entity.Remarks;
      
        }

        private void ControlToEntity(RevenuePeriod entity, IDbContext ctx)
        {
            entity.StartDate = Helper.GetDatePickerValue(Request.Form[txtStartDate.UniqueID]);
            entity.EndDate = Helper.GetDatePickerValue(Request.Form[txtEndDate.UniqueID]);
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            RevenuePeriodDao entityDao = new RevenuePeriodDao(ctx);
            bool result = false;
            try
            {
                RevenuePeriod entity = new RevenuePeriod();
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
            RevenuePeriodDao entityDao = new RevenuePeriodDao(ctx);
            bool result = false;
            try
            {
                RevenuePeriod entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
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