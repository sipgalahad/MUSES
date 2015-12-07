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
    public partial class ItemGroupMasterEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.ITEM_GROUP_MASTER;
        }

        protected override void InitializeDataControl()
        {
            String[] param = Request.QueryString["id"].Split('|');
            if (param[0] == "edit")
            {
                IsAdd = false;
                String ID = param[1];
                hdnID.Value = ID;
                SetControlProperties();
                vItemGroupMaster entity = BusinessLayer.GetvItemGroupMasterList(string.Format("ItemGroupID = {0}", ID))[0];
                EntityToControl(entity);
                hdnGCItemType.Value = entity.GCItemType;
            }
            else
            {
                hdnGCItemType.Value = param[1];
                SetControlProperties();
                IsAdd = true;
            }
            txtItemGroupCode.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.ITEM_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboItemType, lstSc, "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtItemGroupCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtItemGroupName1, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtItemGroupName2, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboItemType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtPrintOrder, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(hdnParentID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtParentCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtParentName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(chkIsHeader, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(vItemGroupMaster entity)
        {
            txtItemGroupCode.Text = entity.ItemGroupCode;
            txtItemGroupName1.Text = entity.ItemGroupName1;
            txtItemGroupName2.Text = entity.ItemGroupName2;
            cboItemType.Value = entity.GCItemType;
            txtPrintOrder.Text = entity.PrintOrder.ToString();
            hdnParentID.Value = entity.ParentID.ToString();
            txtParentCode.Text = entity.ParentCode;
            txtParentName.Text = entity.ParentName;
            chkIsHeader.Checked = entity.IsHeader;
        }

        private void ControlToEntity(ItemGroupMaster entity)
        {
            entity.ItemGroupCode = txtItemGroupCode.Text;
            entity.ItemGroupName1 = txtItemGroupName1.Text;
            entity.ItemGroupName2 = txtItemGroupName2.Text;
            entity.GCItemType = cboItemType.Value.ToString();
            entity.PrintOrder = Convert.ToInt16(txtPrintOrder.Text);
            if (hdnParentID.Value == "" || hdnParentID.Value == "0")
                entity.ParentID = null;
            else
                entity.ParentID = Convert.ToInt32(hdnParentID.Value);
            entity.IsHeader = chkIsHeader.Checked;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("ItemGroupCode = '{0}'", txtItemGroupCode.Text);
            List<ItemGroupMaster> lst = BusinessLayer.GetItemGroupMasterList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Item Group with Code " + txtItemGroupCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("ItemGroupCode = '{0}' AND ItemGroupID != {1}", txtItemGroupCode.Text, hdnID.Value);
            List<ItemGroupMaster> lst = BusinessLayer.GetItemGroupMasterList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Item Group with Code " + txtItemGroupCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            ItemGroupMasterDao entityDao = new ItemGroupMasterDao(ctx);
            SiteItemGroupDao entitySiteItemGroupDao = new SiteItemGroupDao(ctx);
            ItemGroupPlanningDao entityGroupPlanningDao = new ItemGroupPlanningDao(ctx);
            bool result = false;
            try
            {
                ItemGroupMaster entity = new ItemGroupMaster();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                entity.ItemGroupID = BusinessLayer.GetItemGroupMasterMaxID(ctx);

                SiteItemGroup siteItemGroup = new SiteItemGroup();
                siteItemGroup.ItemGroupID = entity.ItemGroupID;
                siteItemGroup.SiteID = AppSession.UserLogin.SiteID;
                siteItemGroup.CreatedBy = AppSession.UserLogin.UserID;
                entitySiteItemGroupDao.Insert(siteItemGroup);

                ItemGroupPlanning ip = new ItemGroupPlanning();
                ip.BusinessPartnerID = null;
                ip.ItemGroupID = entity.ItemGroupID;
                ip.SiteID = AppSession.UserLogin.SiteID;
                ip.CreatedBy = AppSession.UserLogin.UserID;
                entityGroupPlanningDao.Insert(ip);

                retval = entity.ItemGroupID.ToString();
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
                ItemGroupMaster entity = BusinessLayer.GetItemGroupMaster(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateItemGroupMaster(entity);
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