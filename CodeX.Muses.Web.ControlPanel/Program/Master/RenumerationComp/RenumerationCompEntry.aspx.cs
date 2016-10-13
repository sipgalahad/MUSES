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

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                RenumerationComp entity = BusinessLayer.GetRenumerationComp(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            txtRenumerationCompCode.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.RENUMERATION_COMP_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboRenumerationCompType, lstSc, "StandardCodeName", "StandardCodeID");
        }


        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtRenumerationCompCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRenumerationCompName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboRenumerationCompType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
           
        }

        private void EntityToControl(RenumerationComp entity)
        {
            txtRenumerationCompCode.Text = entity.RenumerationCompCode;
            txtRenumerationCompName.Text = entity.RenumerationCompName;
            cboRenumerationCompType.Value = entity.GCRenumerationCompType;
            txtRemarks.Text = entity.Remarks;
      
        }

        private void ControlToEntity(RenumerationComp entity)
        {
            entity.RenumerationCompCode = txtRenumerationCompCode.Text;
            entity.RenumerationCompName = txtRenumerationCompName.Text;
            entity.GCRenumerationCompType = cboRenumerationCompType.Value.ToString();
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("RenumerationCompCode = '{0}'", txtRenumerationCompCode.Text);
            List<RenumerationComp> lst = BusinessLayer.GetRenumerationCompList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Renumeration Component With Code " + txtRenumerationCompCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("RenumerationCompCode = '{0}' AND RenumerationCompID != {1}", txtRenumerationCompCode.Text, hdnID.Value);
            List<RenumerationComp> lst = BusinessLayer.GetRenumerationCompList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Renumeration Component With Code " + txtRenumerationCompCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            RenumerationCompDao entityDao = new RenumerationCompDao(ctx);
            bool result = false;
            try
            {
                RenumerationComp entity = new RenumerationComp();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                retval = entityDao.Insert(entity).ToString();
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
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
            try
            {
                RenumerationComp entity = BusinessLayer.GetRenumerationComp(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateRenumerationComp(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
    }
}