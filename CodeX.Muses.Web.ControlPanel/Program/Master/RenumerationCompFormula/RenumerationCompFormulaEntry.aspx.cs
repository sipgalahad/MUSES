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
    public partial class RenumerationCompFormulaEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.RENUMERATION_COMP_FORMULA;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                RenumerationCompFormulaHd entity = BusinessLayer.GetRenumerationCompFormulaHd(Convert.ToInt32(ID));
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            ctlEntityCode.InitializeMasterCodingControl(Constant.MasterCode.RENUMERATION_COMP_FORMULA);
            ctlEntityCode.SetControlVisibility(IsAdd);
            ctlEntityCode.SetFocus();
        }

        public override void OnAddRecord()
        {
            ctlEntityCode.SetControlVisibility(true);
        }

        protected override void SetControlProperties()
        {
            List<vRenumerationComp> lstRc = BusinessLayer.GetvRenumerationCompList(string.Format("IsDeleted = 0"));
            Methods.SetComboBoxField<vRenumerationComp>(cboRenumerationCompID, lstRc, "RenumerationCompName", "RenumerationCompID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtRenumerationCompFormulaName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboRenumerationCompID, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
           
        }

        private void EntityToControl(RenumerationCompFormulaHd entity)
        {
            ctlEntityCode.SetText(entity.FormulaCode);
            txtRenumerationCompFormulaName.Text = entity.FormulaName;
            cboRenumerationCompID.Value = entity.RenumerationCompID.ToString();
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(RenumerationCompFormulaHd entity, IDbContext ctx)
        {
            entity.FormulaName = txtRenumerationCompFormulaName.Text;
            entity.RenumerationCompID = Convert.ToInt32(cboRenumerationCompID.Value);
            entity.Remarks = txtRemarks.Text;
            entity.FormulaCode = ctlEntityCode.GetCode(entity.FormulaName, ctx);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            RenumerationCompFormulaHdDao entityDao = new RenumerationCompFormulaHdDao(ctx);
            bool result = false;
            try
            {
                RenumerationCompFormulaHd entity = new RenumerationCompFormulaHd();
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
            RenumerationCompFormulaHdDao entityDao = new RenumerationCompFormulaHdDao(ctx);
            bool result = false;
            try
            {
                RenumerationCompFormulaHd entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
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