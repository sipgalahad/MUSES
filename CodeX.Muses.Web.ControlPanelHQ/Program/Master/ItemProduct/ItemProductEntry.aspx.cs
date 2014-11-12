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
using System.Web.UI.HtmlControls;
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class ItemProductEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanelHQ.ITEM_PRODUCT;
        }

        protected override void InitializeDataControl()
        {
            String GCItemType = Constant.ItemType.PRODUCT;
            hdnGCItemType.Value = GCItemType;

            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                vItemProduct entity = BusinessLayer.GetvItemProductList(string.Format("ItemID = {0}", ID))[0];
                ItemTagField entityTagField = BusinessLayer.GetItemTagField(Convert.ToInt32(ID));
               
                SetControlProperties();
                EntityToControl(entity, entityTagField);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            ctlEntityCode.InitializeMasterCodingControl(Constant.MasterCode.ITEM);
            ctlEntityCode.SetControlVisibility(IsAdd);
            ctlEntityCode.SetFocus();
        }

        public override void OnAddRecord()
        {
            ctlEntityCode.SetControlVisibility(true);
        }

        protected override void SetControlProperties()
        {
            List<MarginMarkupHd> lstMarginMarkupHd = BusinessLayer.GetMarginMarkupHdList("IsDeleted = 0");
            lstMarginMarkupHd.Insert(0, new MarginMarkupHd { MarkupID = 0, MarkupName = "" });
            Methods.SetComboBoxField<MarginMarkupHd>(cboMarkup, lstMarginMarkupHd, "MarkupName", "MarkupID");

            List<RestrictionHd> lstRestrictionHd = BusinessLayer.GetRestrictionHdList("IsDeleted = 0");
            lstRestrictionHd.Insert(0, new RestrictionHd { RestrictionID = 0, RestrictionName = "" });
            Methods.SetComboBoxField<RestrictionHd>(cboTransactionRestriction, lstRestrictionHd, "RestrictionName", "RestrictionID");

            string filterExpression = string.Format("ParentID IN ('{0}','{1}') AND IsDeleted = 0", Constant.StandardCode.ITEM_UNIT, Constant.StandardCode.ABC_CLASS);

            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(filterExpression);
            lstStandardCode.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });

            Methods.SetComboBoxField<StandardCode>(cboItemUnit, lstStandardCode.Where(sc => sc.ParentID == Constant.StandardCode.ITEM_UNIT).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetRadioButtonListField<StandardCode>(rblABCClass, lstStandardCode.Where(sc => sc.ParentID == Constant.StandardCode.ABC_CLASS).ToList(), "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            #region Item Information
            //SetControlEntrySetting(txtItemCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtItemName1, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtItemName2, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(hdnItemGroupID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtItemGroupCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtItemGroupName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(hdnProductBrandID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtProductBrandCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtProductBrandName, new ControlEntrySetting(false, false, false));
            #endregion

            #region Finance Information
            SetControlEntrySetting(hdnProductLineID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtProductLineCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtProductLineName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtSuggestedPrice, new ControlEntrySetting(true, true, true, 0));
            SetControlEntrySetting(cboMarkup, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtMargin, new ControlEntrySetting(true, true, true, 0));
            SetControlEntrySetting(chkIsUsingStandardPrice, new ControlEntrySetting(true, true, false));
            #endregion

            #region Inventory Information
            SetControlEntrySetting(rblABCClass, new ControlEntrySetting(true, true, false, "X109^001"));
            SetControlEntrySetting(cboItemUnit, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtCountInterval, new ControlEntrySetting(true, true, true, 0));
            SetControlEntrySetting(cboTransactionRestriction, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(chkIsBatchControl, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(chkIsInventoryItem, new ControlEntrySetting(true, true, false, true));
            SetControlEntrySetting(chkIsProductionItem, new ControlEntrySetting(true, true, false));
            #endregion

            #region Other Information
            SetControlEntrySetting(txtNotes, new ControlEntrySetting(true, true, false));
            #endregion
        }

        protected override void OnReInitControl()
        {
            #region Custom Attribute
            foreach (RepeaterItem item in rptCustomAttribute.Items)
            {
                TextBox txt = (TextBox)item.FindControl("txtTagField");
                txt.Text = "";
            }
            #endregion
        }

        private void EntityToControl(vItemProduct entity, ItemTagField entityTagField)
        {
            #region Item Information
            ctlEntityCode.SetText(entity.ItemCode);
            txtItemName1.Text = entity.ItemName1;
            txtItemName2.Text = entity.ItemName2;
            hdnItemGroupID.Value = entity.ItemGroupID.ToString();
            txtItemGroupCode.Text = entity.ItemGroupCode;
            txtItemGroupName.Text = entity.ItemGroupName1;
            hdnProductBrandID.Value = entity.ProductBrandID.ToString();
            txtProductBrandCode.Text = entity.ProductBrandCode;
            txtProductBrandName.Text = entity.ProductBrandName;
            #endregion

            #region Finance Information
            hdnProductLineID.Value = entity.ProductLineID.ToString();
            txtProductLineCode.Text = entity.ProductLineCode;
            txtProductLineName.Text = entity.ProductLineName;
            txtSuggestedPrice.Text = entity.HETAmount.ToString();
            cboMarkup.Value = entity.MarkupID.ToString();
            txtMargin.Text = entity.MarginPercentage.ToString();
            chkIsUsingStandardPrice.Checked = entity.IsUsingStandardPrice;
            #endregion

            #region Inventory Information
            rblABCClass.SelectedValue = entity.GCABCClass;
            cboItemUnit.Value = entity.GCItemUnit;
            txtCountInterval.Text = entity.CycleCountInterval.ToString();
            cboTransactionRestriction.Value = entity.RestrictionID.ToString();
            chkIsBatchControl.Checked = entity.IsControlExpired;
            chkIsInventoryItem.Checked = entity.IsInventoryItem;
            chkIsProductionItem.Checked = entity.IsProductionItem;
            chkIsControlExpired.Checked = entity.IsControlExpired;
            #endregion

            #region Other Information
            txtNotes.Text = entity.Remarks;
            #endregion

            #region Custom Attribute
            foreach (RepeaterItem item in rptCustomAttribute.Items)
            {
                TextBox txt = (TextBox)item.FindControl("txtTagField");
                HtmlInputHidden hdn = (HtmlInputHidden)item.FindControl("hdnTagFieldCode");
                txt.Text = entityTagField.GetType().GetProperty("TagField" + hdn.Value).GetValue(entityTagField, null).ToString();
            }
            #endregion
        }

        private void ControlToEntity(ItemMaster entity, ItemProduct entityProduct, ItemTagField entityTagField)
        {
            #region Item Information
            entity.ItemName1 = txtItemName1.Text;
            entity.ItemName2 = txtItemName2.Text;
            entity.ItemGroupID = Convert.ToInt32(hdnItemGroupID.Value);
            if (hdnProductBrandID.Value == "" || hdnProductBrandID.Value == "0")
                entityProduct.ProductBrandID = null;
            else
                entityProduct.ProductBrandID = Convert.ToInt32(hdnProductBrandID.Value);
            #endregion

            #region Finance Information
            if (string.IsNullOrEmpty(txtProductLineCode.Text))
                entity.ProductLineID = null;
            else
                entity.ProductLineID = Convert.ToInt32(hdnProductLineID.Value);

            if (cboMarkup.Value != null && cboMarkup.Value.ToString() != "0")
                entityProduct.MarkupID = Convert.ToInt32(cboMarkup.Value);
            else
                entityProduct.MarkupID = null;
            entityProduct.IsUsingStandardPrice = chkIsUsingStandardPrice.Checked;
            entityProduct.MarginPercentage = Convert.ToDecimal(txtMargin.Text);
            #endregion

            #region Inventory Information
            entityProduct.GCABCClass = rblABCClass.SelectedValue;
            entity.GCItemUnit = cboItemUnit.Value.ToString();
            entityProduct.CycleCountInterval = Convert.ToDecimal(txtCountInterval.Text);
            if (cboTransactionRestriction.Value != null && cboTransactionRestriction.Value.ToString() != "0")
                entityProduct.RestrictionID = Convert.ToInt32(cboTransactionRestriction.Value);
            else
                entityProduct.RestrictionID = null;

            entityProduct.IsControlExpired = chkIsBatchControl.Checked;
            entityProduct.IsInventoryItem = chkIsInventoryItem.Checked;
            entityProduct.IsProductionItem = chkIsProductionItem.Checked;
            entityProduct.IsControlExpired = chkIsControlExpired.Checked;
            #endregion

            #region Other Information
            txtNotes.Text = entity.Remarks;
            #endregion

            #region Custom Attribute
            foreach (RepeaterItem item in rptCustomAttribute.Items)
            {
                TextBox txt = (TextBox)item.FindControl("txtTagField");
                HtmlInputHidden hdn = (HtmlInputHidden)item.FindControl("hdnTagFieldCode");
                entityTagField.GetType().GetProperty("TagField" + hdn.Value).SetValue(entityTagField, txt.Text, null);
            }
            #endregion
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ItemMasterDao entityDao = new ItemMasterDao(ctx);
            ItemProductDao entityProductDao = new ItemProductDao(ctx);
            ItemTagFieldDao entityTagFieldDao = new ItemTagFieldDao(ctx);
            //ItemCostDao entityCostDao = new ItemCostDao(ctx);
            //ItemPlanningDao entityPlanningDao = new ItemPlanningDao(ctx);
            try
            {
                ItemMaster entity = new ItemMaster();
                ItemProduct entityProduct = new ItemProduct();
                ItemTagField entityTagField = new ItemTagField();
                ControlToEntity(entity, entityProduct, entityTagField);
                entity.ItemCode = ctlEntityCode.GetCode(entity.ItemName1, ctx);
                entity.GCItemType = hdnGCItemType.Value;

                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);

                entityProduct.ItemID = BusinessLayer.GetItemMasterMaxID(ctx);
                entityProductDao.Insert(entityProduct);

                entityTagField.ItemID = entityProduct.ItemID;
                entityTagFieldDao.Insert(entityTagField);

                //List<Site> lstSite = BusinessLayer.GetSiteList("", ctx);
                //foreach (Site healthcare in lstSite)
                //{
                //    ItemCost ic = new ItemCost();
                //    ic.ItemID = entityProduct.ItemID;
                //    ic.SiteID = healthcare.SiteID;
                //    ic.CreatedBy = AppSession.UserLogin.UserID;
                //    entityCostDao.Insert(ic);

                //    ItemPlanning ip = new ItemPlanning();
                //    ip.BusinessPartnerID = null;
                //    ip.ItemID = entityProduct.ItemID;
                //    ip.SiteID = healthcare.SiteID;
                //    ip.CreatedBy = AppSession.UserLogin.UserID;
                //    entityPlanningDao.Insert(ip);
                //}
                retval = entityProduct.ItemID.ToString();
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ItemMasterDao entityDao = new ItemMasterDao(ctx);
            ItemProductDao entityProductDao = new ItemProductDao(ctx);
            ItemTagFieldDao entityTagFieldDao = new ItemTagFieldDao(ctx);
            try
            {
                Int32 ItemID = Convert.ToInt32(hdnID.Value);
                ItemMaster entity = entityDao.Get(ItemID);
                ItemProduct entityProduct = entityProductDao.Get(ItemID);
                ItemTagField entityTagField = entityTagFieldDao.Get(ItemID);
                ControlToEntity(entity, entityProduct, entityTagField);
                entity.ItemCode = ctlEntityCode.GetCode(entity.ItemName1, ctx);

                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityProduct.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityTagField.LastUpdatedBy = AppSession.UserLogin.UserID;

                entityDao.Update(entity);
                entityProductDao.Update(entityProduct);
                entityTagFieldDao.Update(entityTagField);

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (pnlCustomAttribute.Visible)
            {
                List<Variable> ListCustomAttribute = initListCustomAttribute();
                if (ListCustomAttribute.Count == 0)
                    pnlCustomAttribute.Visible = false;
                else
                {
                    rptCustomAttribute.DataSource = ListCustomAttribute;
                    rptCustomAttribute.DataBind();
                }
            }
        }

        private List<Variable> initListCustomAttribute()
        {
            List<Variable> ListCustomAttribute = new List<Variable>();
            TagField tagField = BusinessLayer.GetTagField(Constant.BusinessObjectType.ITEM);
            if (tagField != null)
            {
                if (tagField.TagField1 != "") { ListCustomAttribute.Add(new Variable { Code = "1", Value = tagField.TagField1 }); }
                if (tagField.TagField2 != "") { ListCustomAttribute.Add(new Variable { Code = "2", Value = tagField.TagField2 }); }
                if (tagField.TagField3 != "") { ListCustomAttribute.Add(new Variable { Code = "3", Value = tagField.TagField3 }); }
                if (tagField.TagField4 != "") { ListCustomAttribute.Add(new Variable { Code = "4", Value = tagField.TagField4 }); }
                if (tagField.TagField5 != "") { ListCustomAttribute.Add(new Variable { Code = "5", Value = tagField.TagField5 }); }
                if (tagField.TagField6 != "") { ListCustomAttribute.Add(new Variable { Code = "6", Value = tagField.TagField6 }); }
                if (tagField.TagField7 != "") { ListCustomAttribute.Add(new Variable { Code = "7", Value = tagField.TagField7 }); }
                if (tagField.TagField8 != "") { ListCustomAttribute.Add(new Variable { Code = "8", Value = tagField.TagField8 }); }
                if (tagField.TagField9 != "") { ListCustomAttribute.Add(new Variable { Code = "9", Value = tagField.TagField9 }); }
                if (tagField.TagField10 != "") { ListCustomAttribute.Add(new Variable { Code = "10", Value = tagField.TagField10 }); }
                if (tagField.TagField11 != "") { ListCustomAttribute.Add(new Variable { Code = "11", Value = tagField.TagField11 }); }
                if (tagField.TagField12 != "") { ListCustomAttribute.Add(new Variable { Code = "12", Value = tagField.TagField12 }); }
                if (tagField.TagField13 != "") { ListCustomAttribute.Add(new Variable { Code = "13", Value = tagField.TagField13 }); }
                if (tagField.TagField14 != "") { ListCustomAttribute.Add(new Variable { Code = "14", Value = tagField.TagField14 }); }
                if (tagField.TagField15 != "") { ListCustomAttribute.Add(new Variable { Code = "15", Value = tagField.TagField15 }); }
                if (tagField.TagField16 != "") { ListCustomAttribute.Add(new Variable { Code = "16", Value = tagField.TagField16 }); }
                if (tagField.TagField17 != "") { ListCustomAttribute.Add(new Variable { Code = "17", Value = tagField.TagField17 }); }
                if (tagField.TagField18 != "") { ListCustomAttribute.Add(new Variable { Code = "18", Value = tagField.TagField18 }); }
                if (tagField.TagField19 != "") { ListCustomAttribute.Add(new Variable { Code = "19", Value = tagField.TagField19 }); }
                if (tagField.TagField20 != "") { ListCustomAttribute.Add(new Variable { Code = "20", Value = tagField.TagField20 }); }
            }
            return ListCustomAttribute;
        }
    }
}