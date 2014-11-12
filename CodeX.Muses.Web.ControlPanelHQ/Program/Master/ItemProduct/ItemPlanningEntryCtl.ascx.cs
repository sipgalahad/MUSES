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

namespace CodeX.Muses.Web.ControlPanelHQ.Program
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
            List<StandardCode> lstPurchaseUnit = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND (StandardCodeID IN (SELECT GCAlternateUnit FROM ItemAlternateUnit WHERE ItemID = {1}) OR StandardCodeID = (SELECT GCItemUnit FROM ItemMaster WHERE ItemID = {1}))", Constant.StandardCode.ITEM_UNIT, hdnItemID.Value));
            Methods.SetComboBoxField<StandardCode>(cboPurchaseUnit, lstPurchaseUnit, "StandardCodeName", "StandardCodeID");
            cboPurchaseUnit.SelectedIndex = -1;
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
            SetControlEntrySetting(cboPurchaseUnit, new ControlEntrySetting(true, true, true));
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
            cboPurchaseUnit.Value = entity.GCPurchaseUnit;
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
            entity.GCPurchaseUnit = cboPurchaseUnit.Value.ToString();
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