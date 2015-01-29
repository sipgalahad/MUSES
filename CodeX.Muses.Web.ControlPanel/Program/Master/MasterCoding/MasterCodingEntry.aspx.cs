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
    public partial class MasterCodingEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.MASTER_CODING;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                MasterCoding entity = BusinessLayer.GetMasterCoding(ID);
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            txtDefaultPrefix.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lst = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsDeleted = 0 AND IsActive = 1", Constant.StandardCode.PREFIX_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboPrefixType, lst, "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtMasterCode, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtMasterName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(cboPrefixType, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtDefaultPrefix, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtPrefixLength, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtCounterDigit, new ControlEntrySetting(true, true, true));
            //SetControlEntrySetting(chkIsBySite, new ControlEntrySetting(true, true, true));
            //SetControlEntrySetting(chkIsAllowChangeInitial, new ControlEntrySetting(true, true, true));
            //SetControlEntrySetting(chkIsEditable, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(MasterCoding entity)
        {
            txtMasterCode.Text = entity.MasterCode;
            txtMasterName.Text = entity.MasterName;
            cboPrefixType.Value = entity.GCPrefixType;
            txtDefaultPrefix.Text = entity.DefaultPrefix;
            txtPrefixLength.Text = entity.PrefixLength.ToString();
            chkIsBySite.Checked = entity.IsBySite;
            txtCounterDigit.Text = entity.CounterDigit.ToString();
            chkIsAllowChangeInitial.Checked = entity.IsAllowChangeInitial;
            chkIsEditable.Checked = entity.IsEditable;
        }

        private void ControlToEntity(MasterCoding entity)
        {
            entity.DefaultPrefix = txtDefaultPrefix.Text;
            entity.PrefixLength = Convert.ToInt16(txtPrefixLength.Text);
            entity.IsBySite = chkIsBySite.Checked;
            entity.CounterDigit = Convert.ToInt16(txtCounterDigit.Text);
            entity.IsAllowChangeInitial = chkIsAllowChangeInitial.Checked;
            entity.IsEditable = chkIsEditable.Checked;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            //string FilterExpression = string.Format("BankCode = '{0}'", txtBankCode.Text);
            //List<Bank> lst = BusinessLayer.GetBankList(FilterExpression);

            //if (lst.Count > 0)
            //    errMessage = " Bank With Code " + txtBankCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            //string FilterExpression = string.Format("BankCode = '{0}' AND BankID != {1}", txtBankCode.Text, hdnID.Value);
            //List<Bank> lst = BusinessLayer.GetBankList(FilterExpression);

            //if (lst.Count > 0)
            //    errMessage = " Bank With Code " + txtBankCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            MasterCodingDao entityDao = new MasterCodingDao(ctx);
            bool result = false;
            try
            {
                MasterCoding entity = new MasterCoding();
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetBankMaxID(ctx).ToString();
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
                MasterCoding entity = BusinessLayer.GetMasterCoding(hdnID.Value);
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateMasterCoding(entity);
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