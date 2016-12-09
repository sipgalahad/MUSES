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
    public partial class RenumerationCompEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.RENUMERATION_COMP;
        }

        protected string OnGetRenumerationCompTypeDeduction()
        {
            return Constant.RenumerationCompType.DEDUCTION;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                RenumerationComp entity = BusinessLayer.GetRenumerationComp(Convert.ToInt32(ID));
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            ctlEntityCode.InitializeMasterCodingControl(Constant.MasterCode.RENUMERATION_COMP);
            ctlEntityCode.SetControlVisibility(IsAdd);
            ctlEntityCode.SetFocus();
        }

        public override void OnAddRecord()
        {
            ctlEntityCode.SetControlVisibility(true);
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.RENUMERATION_COMP_TYPE, Constant.StandardCode.RENUMERATION_COMP_SOURCE));
            Methods.SetComboBoxField<StandardCode>(cboRenumerationCompType, lstSc.Where(p => p.ParentID == Constant.StandardCode.RENUMERATION_COMP_TYPE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboRenumerationCompSource, lstSc.Where(p => p.ParentID == Constant.StandardCode.RENUMERATION_COMP_SOURCE).ToList(), "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtRenumerationCompName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboRenumerationCompType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboRenumerationCompSource, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(RenumerationComp entity)
        {
            ctlEntityCode.SetText(entity.RenumerationCompCode);
            txtRenumerationCompName.Text = entity.RenumerationCompName;
            cboRenumerationCompType.Value = entity.GCRenumerationCompType;
            cboRenumerationCompSource.Value = entity.GCRenumerationCompSource;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(RenumerationComp entity, IDbContext ctx)
        {
            entity.RenumerationCompName = txtRenumerationCompName.Text;
            entity.GCRenumerationCompType = cboRenumerationCompType.Value.ToString();
            if (entity.GCRenumerationCompType == Constant.RenumerationCompType.DEDUCTION)
                entity.GCRenumerationCompSource = null;
            else
                entity.GCRenumerationCompSource = cboRenumerationCompSource.Value.ToString();
            entity.Remarks = txtRemarks.Text;
            entity.RenumerationCompCode = ctlEntityCode.GetCode(entity.RenumerationCompName, ctx);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            RenumerationCompDao entityDao = new RenumerationCompDao(ctx);
            bool result = false;
            try
            {
                RenumerationComp entity = new RenumerationComp();
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
            RenumerationCompDao entityDao = new RenumerationCompDao(ctx);
            bool result = false;
            try
            {
                RenumerationComp entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
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