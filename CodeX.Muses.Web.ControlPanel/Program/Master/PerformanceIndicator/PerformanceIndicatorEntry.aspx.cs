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
    public partial class PerformanceIndicatorEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.PERFORMANCE_INDICATOR;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                PerformanceIndicatorHd entity = BusinessLayer.GetPerformanceIndicatorHd(Convert.ToInt32(ID));
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            //ctlEntityCode.InitializeMasterCodingControl(Constant.MasterCode.PERFORMANCE_INDICATOR);
            //ctlEntityCode.SetControlVisibility(IsAdd);
            //ctlEntityCode.SetFocus();
        }

        public override void OnAddRecord()
        {
            //ctlEntityCode.SetControlVisibility(true);
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lstPeriod = BusinessLayer.GetStandardCodeList(string.Format("IsDeleted = 0 AND ParentID = '{0}' ", Constant.StandardCode.INDICATOR_MARK_PERIOD));
            Methods.SetComboBoxField<StandardCode>(cboPeriod, lstPeriod, "StandardCodeName", "StandardCodeID");

            List<StandardCode> lstType = BusinessLayer.GetStandardCodeList(string.Format("IsDeleted = 0 AND ParentID = '{0}' ", Constant.StandardCode.INDICATOR_MARK_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboType, lstType, "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtPerformanceIndicatorName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboPeriod, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
           
        }

        private void EntityToControl(PerformanceIndicatorHd entity)
        {
            txtPerformanceIndicatorName.Text = entity.PerformanceIndicatorName.ToString();
            cboPeriod.Value = entity.GCIndicatorMarkPeriod.ToString();
            cboType.Value = entity.GCIndicatorMarkType.ToString();
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(PerformanceIndicatorHd entity, IDbContext ctx)
        {
            entity.PerformanceIndicatorName = txtPerformanceIndicatorName.Text;
            entity.GCIndicatorMarkPeriod = cboPeriod.Value.ToString();
            entity.GCIndicatorMarkType = cboType.ToString();
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            PerformanceIndicatorHdDao entityDao = new PerformanceIndicatorHdDao(ctx);
            bool result = false;
            try
            {
                PerformanceIndicatorHd entity = new PerformanceIndicatorHd();
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
            PerformanceIndicatorHdDao entityDao = new PerformanceIndicatorHdDao(ctx);
            bool result = false;
            try
            {
                PerformanceIndicatorHd entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
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