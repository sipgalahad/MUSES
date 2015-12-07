using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class ItemPlanningEntryCtl : BaseEntryPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            IsAdd = false;

            hdnItemPlanningID.Value = param;

            vItemPlanning entity = BusinessLayer.GetvItemPlanningList(string.Format("ID = {0}", hdnItemPlanningID.Value))[0];
            txtItemName.Text = entity.ItemName1;
            txtSiteName.Text = entity.SiteName;
            hdnItemID.Value = entity.ItemID.ToString();

            SetControlProperties();
            EntityToControl(entity);
        }

        private void SetControlProperties()
        {
            List<vItemAlternateUnitCustom> lst = BusinessLayer.GetvItemAlternateUnitCustomList(string.Format("ItemID = {0}", hdnItemID.Value));
            lst.Insert(0, new vItemAlternateUnitCustom { AlternateUnit = "", GCAlternateUnit = "", ConversionFactor = 0 });
            Methods.SetComboBoxField<vItemAlternateUnitCustom>(cboPurchaseUnit, lst, "cfAlternateUnit", "cfID");
            cboPurchaseUnit.SelectedIndex = -1;

            Methods.SetComboBoxField<vItemAlternateUnitCustom>(cboDistributionUnit, lst, "cfAlternateUnit", "cfID");
            cboDistributionUnit.SelectedIndex = -1;

            List<StandardCode> lstPurchaseMethod = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PURCHASE_METHOD));
            Methods.SetComboBoxField<StandardCode>(cboPurchaseMethod, lstPurchaseMethod, "StandardCodeName", "StandardCodeID");
            cboPurchaseMethod.SelectedIndex = 0;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnSupplierID, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtSupplierCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtSupplierName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtLeadTime, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtTolerance, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtSafetyTime, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtTimeFence, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtSafetyStock, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtBasePrice, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtMinOrderQty, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtMaxOrderQty, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboPurchaseUnit, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboDistributionUnit, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboPurchaseMethod, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(chkIsROPSettingDefault, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtNDaysBackward, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(txtNDaysForward, new ControlEntrySetting(true, true, true, "0"));
        }

        private void EntityToControl(vItemPlanning entity)
        {
            hdnSupplierID.Value = entity.BusinessPartnerID.ToString();
            txtSupplierCode.Text = entity.BusinessPartnerCode;
            txtSupplierName.Text = entity.BusinessPartnerName;
            txtLeadTime.Text = entity.LeadTime.ToString();
            txtTolerance.Text = entity.ToleranceQty.ToString();
            txtSafetyTime.Text = entity.SafetyTime.ToString();
            txtTimeFence.Text = entity.TimeFence.ToString();
            txtSafetyStock.Text = entity.SafetyStock.ToString();
            txtBasePrice.Text = entity.AveragePrice.ToString();
            txtMinOrderQty.Text = entity.MinOrderQty.ToString();
            txtMaxOrderQty.Text = entity.MaxOrderQty.ToString();
            if (entity.GCPurchaseUnit != "" && entity.GCPurchaseUnit != null)
                cboPurchaseUnit.Value = string.Format("{0}|{1}", entity.GCPurchaseUnit, entity.PurchaseUnitConversionFactor.ToString("G29"));
            else
                cboPurchaseUnit.Value = "";
            if (entity.GCDistributionUnit != "" && entity.GCDistributionUnit != null)
                cboDistributionUnit.Value = string.Format("{0}|{1}", entity.GCDistributionUnit, entity.DistributionUnitConversionFactor.ToString("G29"));
            else
                cboDistributionUnit.Value = "";
            cboPurchaseMethod.Value = entity.GCPurchaseMethod;
            chkIsROPSettingDefault.Checked = entity.IsROPSettingDefault;
            txtNDaysBackward.Text = entity.NDaysBackward.ToString();
            txtNDaysForward.Text = entity.NDaysForward.ToString();
        }

        private void ControlToEntity(ItemPlanning entity)
        {
            if (hdnSupplierID.Value != "" && hdnSupplierID.Value != "0")
                entity.BusinessPartnerID = Convert.ToInt32(hdnSupplierID.Value);
            else
                entity.BusinessPartnerID = null;
            entity.LeadTime = Convert.ToByte(txtLeadTime.Text);
            entity.ToleranceQty = Convert.ToDecimal(txtTolerance.Text);
            entity.SafetyTime = Convert.ToByte(txtSafetyTime.Text);
            entity.TimeFence = Convert.ToByte(txtTimeFence.Text);
            entity.SafetyStock = Convert.ToDecimal(txtSafetyStock.Text);
            entity.AveragePrice = Convert.ToDecimal(txtBasePrice.Text);
            entity.MinOrderQty = Convert.ToDecimal(txtMinOrderQty.Text);
            entity.MaxOrderQty = Convert.ToDecimal(txtMaxOrderQty.Text);
            if (cboPurchaseUnit.Value != null && cboPurchaseUnit.Value.ToString() != "|0")
            {
                string[] tempPurchaseUnit = cboPurchaseUnit.Value.ToString().Split('|');
                entity.GCPurchaseUnit = tempPurchaseUnit[0].ToString();
                entity.PurchaseUnitConversionFactor = Convert.ToDecimal(tempPurchaseUnit[1]);
            }
            else
            {
                entity.GCPurchaseUnit = null;
                entity.PurchaseUnitConversionFactor = 0;
            }
            if (cboDistributionUnit.Value != null && cboDistributionUnit.Value.ToString() != "|0")
            {
                string[] tempDistributionUnit = cboDistributionUnit.Value.ToString().Split('|');
                entity.GCDistributionUnit = tempDistributionUnit[0].ToString();
                entity.DistributionUnitConversionFactor = Convert.ToDecimal(tempDistributionUnit[1]);
            }
            else
            {
                entity.GCDistributionUnit = null;
                entity.DistributionUnitConversionFactor = 0;
            }
            entity.GCPurchaseMethod = cboPurchaseMethod.Value.ToString();
            entity.IsROPSettingDefault = chkIsROPSettingDefault.Checked;
            entity.NDaysBackward = Convert.ToInt32(txtNDaysBackward.Text);
            entity.NDaysForward = Convert.ToInt32(txtNDaysForward.Text);
        }

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            try
            {
                ItemPlanning entity = BusinessLayer.GetItemPlanning(Convert.ToInt32(hdnItemPlanningID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateItemPlanning(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        protected string OnGetFilterExpressionSupplier()
        {
            return string.Format("GCBusinessPartnerType = '{0}'", Constant.BusinessObjectType.SUPPLIER);
        }
    }    
}