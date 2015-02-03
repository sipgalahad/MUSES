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
            SetControlEntrySetting(chkIsBySite, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(chkIsAllowChangeInitial, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(chkIsEditable, new ControlEntrySetting(true, true, false));
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
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }
    }
}