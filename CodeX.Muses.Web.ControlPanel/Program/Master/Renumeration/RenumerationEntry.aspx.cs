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
    public partial class RenumerationEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.RENUMERATION;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                RenumerationHd entity = BusinessLayer.GetRenumerationHd(Convert.ToInt32(ID));
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            ctlEntityCode.InitializeMasterCodingControl(Constant.MasterCode.RENUMERATION);
            ctlEntityCode.SetControlVisibility(IsAdd);
            ctlEntityCode.SetFocus();
        }

        public override void OnAddRecord()
        {
            ctlEntityCode.SetControlVisibility(true);
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.RENUMERATION_COMP_SOURCE));
            Methods.SetComboBoxField<StandardCode>(cboRenumerationCompSource, lstSc, "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtRenumerationName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboRenumerationCompSource, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));           
        }

        private void EntityToControl(RenumerationHd entity)
        {
            ctlEntityCode.SetText(entity.RenumerationCode);
            txtRenumerationName.Text = entity.RenumerationName;
            cboRenumerationCompSource.Value = entity.GCRenumerationCompSource;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(RenumerationHd entity, IDbContext ctx)
        {
            entity.RenumerationName = txtRenumerationName.Text;
            entity.GCRenumerationCompSource = cboRenumerationCompSource.Value.ToString();
            entity.Remarks = txtRemarks.Text;
            entity.RenumerationCode = ctlEntityCode.GetCode(entity.RenumerationName, ctx);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            RenumerationHdDao entityDao = new RenumerationHdDao(ctx);
            bool result = false;
            try
            {
                RenumerationHd entity = new RenumerationHd();
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
            RenumerationHdDao entityDao = new RenumerationHdDao(ctx);
            bool result = false;
            try
            {
                RenumerationHd entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
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