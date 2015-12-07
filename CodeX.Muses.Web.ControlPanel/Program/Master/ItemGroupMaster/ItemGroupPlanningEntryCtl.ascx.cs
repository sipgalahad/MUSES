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
    public partial class ItemGroupPlanningEntryCtl : BaseEntryPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            IsAdd = false;

            string[] temp = param.Split('|');

            hdnSiteID.Value = temp[0];
            hdnItemGroupID.Value = temp[1];

            vItemGroupPlanning entity = BusinessLayer.GetvItemGroupPlanningList(string.Format("SiteID = '{0}' AND ItemGroupID = {1}", hdnSiteID.Value, hdnItemGroupID.Value))[0];
            txtItemName.Text = entity.ItemGroupName1;
            txtSiteName.Text = entity.SiteName;

            SetControlProperties();
            EntityToControl(entity);
        }

        private void SetControlProperties()
        {
            List<StandardCode> lstPurchaseMethod = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PURCHASE_METHOD));
            Methods.SetComboBoxField<StandardCode>(cboPurchaseMethod, lstPurchaseMethod, "StandardCodeName", "StandardCodeID");
            cboPurchaseMethod.SelectedIndex = 0;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(cboPurchaseMethod, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtNDaysBackward, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(txtNDaysForward, new ControlEntrySetting(true, true, true, "0"));
        }

        private void EntityToControl(vItemGroupPlanning entity)
        {
            cboPurchaseMethod.Value = entity.GCPurchaseMethod;
            txtNDaysBackward.Text = entity.NDaysBackward.ToString();
            txtNDaysForward.Text = entity.NDaysForward.ToString();
        }

        private void ControlToEntity(ItemGroupPlanning entity)
        {
            entity.GCPurchaseMethod = cboPurchaseMethod.Value.ToString();
            entity.NDaysBackward = Convert.ToInt32(txtNDaysBackward.Text);
            entity.NDaysForward = Convert.ToInt32(txtNDaysForward.Text);
        }

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            try
            {
                ItemGroupPlanning entity = BusinessLayer.GetItemGroupPlanning(hdnSiteID.Value, Convert.ToInt32(hdnItemGroupID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateItemGroupPlanning(entity);
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