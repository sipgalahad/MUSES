using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Core.Dal;
using System.Data;
using System.Web.UI.HtmlControls;
using CodeX.Common;

namespace CodeX.Ottimo.Web.Inventory.Program
{
    public partial class ItemProductionEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.ITEM_PRODUCTION;
        }

        protected override void InitializeDataControl()
        {
            int count = BusinessLayer.GetLocationUserRowCount(string.Format("UserID = {0} AND IsDeleted = 0", AppSession.UserLogin.UserID));
            if (count > 0)
                hdnRecordFilterExpression.Value = string.Format("FromLocationID IN (SELECT LocationID FROM LocationUser WHERE UserID = {0} AND IsDeleted = 0)", AppSession.UserLogin.UserID);
            else
            {
                count = BusinessLayer.GetLocationUserRoleRowCount(string.Format("RoleID IN (SELECT RoleID FROM UserInRole WHERE UserID = {0} AND SiteID = '{1}') AND IsDeleted = 0", AppSession.UserLogin.UserID, AppSession.UserLogin.SiteID));
                if (count > 0)
                    hdnRecordFilterExpression.Value = string.Format("FromLocationID IN (SELECT LocationID FROM LocationUserRole WHERE RoleID IN (SELECT RoleID FROM UserInRole WHERE UserID = {0} AND SiteID = '{1}') AND IsDeleted = 0)", AppSession.UserLogin.UserID, AppSession.UserLogin.SiteID);
                else
                    hdnRecordFilterExpression.Value = "";
            }
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnProductionID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtProductionNo, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(hdnLocationIDFrom, new ControlEntrySetting(true, false, true,""));
            SetControlEntrySetting(txtLocationCode, new ControlEntrySetting(true, false, true,""));
            SetControlEntrySetting(txtLocationName, new ControlEntrySetting(false, false, true,""));
            SetControlEntrySetting(hdnItemID, new ControlEntrySetting(true, false, true,""));
            SetControlEntrySetting(txtItemCode, new ControlEntrySetting(true, false, true,""));
            SetControlEntrySetting(txtItemName, new ControlEntrySetting(false, false, true,""));
            SetControlEntrySetting(txtQuantity, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(chkIsFixedCost, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtFixedCostAmount, new ControlEntrySetting(true, true, true));
            
            SetControlEntrySetting(txtProductionDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(hdnLocationIDTo, new ControlEntrySetting(true, false, true,""));
            SetControlEntrySetting(txtLocationCodeTo, new ControlEntrySetting(true, false, true,""));
            SetControlEntrySetting(txtLocationNameTo, new ControlEntrySetting(false, false, true,""));
            SetControlEntrySetting(txtBatchNumber, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtReferenceNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtNotes, new ControlEntrySetting(true, true, false));
            
            SetControlEntrySetting(lblLocation, new ControlEntrySetting(true, false));
            SetControlEntrySetting(lblLocationTo, new ControlEntrySetting(true, false));
            SetControlEntrySetting(lblItem, new ControlEntrySetting(true, false));
        }

        #region Filter Expression Search Dialog
        protected string OnGetFilterExpressionFromLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.PRODUCTION_PROCESS);
        }
        protected string OnGetFilterExpressionToLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.PRODUCTION_PROCESS);
        }
        protected string OnGetFilterExpressionItemProduct()
        {
            return string.Format("GCItemType IN ('{0}','{1}','{2}') AND ItemID IN (SELECT ItemID FROM ItemProduct WHERE IsProductionItem = 1) AND IsDeleted = 0", Constant.ItemType.DRUGS, Constant.ItemType.SUPPLIES, Constant.ItemType.LOGISTIC);
        }
        #endregion

        #region Load Entity
        protected string GetFilterExpression()
        {
            return hdnRecordFilterExpression.Value;
        }

        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvItemProductionHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vItemProductionHd entity = BusinessLayer.GetvItemProductionHd(filterExpression, PageIndex, "ProductionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvItemProductionHdRowIndex(filterExpression, keyValue, "ProductionID DESC");
            vItemProductionHd entity = BusinessLayer.GetvItemProductionHd(filterExpression, PageIndex, "ProductionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }
        #endregion

        public override void OnAddRecord()
        {
            hdnIsEditable.Value = "1";
        }

        protected string IsEditable()
        {
            return hdnIsEditable.Value;
        }

        private void EntityToControl(vItemProductionHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
                hdnIsEditable.Value = "0";
            }
            else
                hdnIsEditable.Value = "1";
            hdnProductionID.Value = entity.ProductionID.ToString();
            txtProductionNo.Text = entity.ProductionNo;
            txtProductionDate.Text = entity.ProductionDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            hdnLocationIDFrom.Value = entity.FromLocationID.ToString();
            txtLocationCode.Text = entity.FromLocationCode;
            txtLocationName.Text = entity.FromLocationName;
            hdnLocationIDTo.Value = entity.ToLocationID.ToString();
            txtLocationCodeTo.Text = entity.ToLocationCode;
            txtLocationNameTo.Text = entity.ToLocationName;
            txtNotes.Text = entity.Remarks;
            txtBatchNumber.Text = entity.BatchNumber;
            txtFixedCostAmount.Text = entity.FixedCostAmount.ToString();
            hdnItemID.Value = entity.ItemID.ToString();
            txtItemCode.Text = entity.ItemCode;
            txtItemName.Text = entity.ItemName1;
            txtQuantity.Text = entity.Quantity.ToString();
            txtUnitPrice.Text = entity.UnitPrice.ToString();
            txtQuantity.Text = entity.Quantity.ToString();
            chkIsFixedCost.Checked = (entity.FixedCostAmount != 0);

            BindGridView();
        }

        private void BindGridView()
        {
            string filterExpression = "1 = 0";
            if (hdnItemID.Value != "")
                filterExpression = string.Format("ItemID = {0} AND IsDeleted = 0", hdnItemID.Value);

            Qty = Convert.ToDecimal(txtQuantity.Text);
            List<vItemBOM> lstEntity = BusinessLayer.GetvItemBOMList(filterExpression);
            string lstItemID = "";
            foreach (vItemBOM itemBOM in lstEntity)
            {
                if (lstItemID != "")
                    lstItemID += ",";
                lstItemID += itemBOM.BillOfMaterialID.ToString();
            }
            if (lstItemID != "")
                filterExpression = string.Format("SiteID = '{0}' AND ItemID IN ({1})", AppSession.UserLogin.SiteID, lstItemID);
            lstItemCost = BusinessLayer.GetItemCostList(filterExpression);

            if (lstItemID != "" && hdnLocationIDFrom.Value != "")
                filterExpression = string.Format("LocationID = {0} AND ItemID IN ({1}) AND IsDeleted = 0", hdnLocationIDFrom.Value, lstItemID);
            else
                filterExpression = "1 = 0";
            lstItemBalance = BusinessLayer.GetItemBalanceList(filterExpression);
            
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();   
        }

        private Decimal Qty = 0;
        private List<ItemBalance> lstItemBalance = null;
        private List<ItemCost> lstItemCost = null;
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vItemBOM entity = e.Row.DataItem as vItemBOM;
                TextBox txtQuantity = (TextBox)e.Row.FindControl("txtQuantity");
                TextBox txtCostAmount = (TextBox)e.Row.FindControl("txtCostAmount");
                HtmlInputHidden hdnCostAmount = (HtmlInputHidden)e.Row.FindControl("hdnCostAmount");

                decimal qty = (entity.BOMQuantity / entity.ItemQuantity * Qty);
                txtQuantity.Text = qty.ToString();
                txtQuantity.Attributes.Add("validationgroup", "mpEntry");
                ItemCost itemCost = lstItemCost.FirstOrDefault(p => p.ItemID == entity.BillOfMaterialID);
                decimal costAmount = (itemCost.TotalBurden + itemCost.TotalLabor + itemCost.TotalMaterial + itemCost.TotalOverhead + itemCost.TotalSubContract);
                hdnCostAmount.Value = costAmount.ToString();
                txtCostAmount.Text = (costAmount * qty).ToString();

                ItemBalance itemBalance = lstItemBalance.FirstOrDefault(p => p.ItemID == entity.BillOfMaterialID);
                if (itemBalance != null)
                {
                    HtmlGenericControl divRemainingStock = (HtmlGenericControl)e.Row.FindControl("divRemainingStock");
                    divRemainingStock.InnerHtml = itemBalance.QuantityEND.ToString("N");
                    txtQuantity.CssClass = "txtCurrency txtQuantity min";
                    txtQuantity.Attributes.Add("max", itemBalance.QuantityEND.ToString());
                }
            }
        }

        #region Save & Edit Header
        private void ControlToEntity(ItemProductionHd entityHd)
        {
            entityHd.FromLocationID = Convert.ToInt32(hdnLocationIDFrom.Value);
            entityHd.ToLocationID = Convert.ToInt32(hdnLocationIDTo.Value);
            entityHd.ItemID = Convert.ToInt32(hdnItemID.Value);
            entityHd.Quantity = Convert.ToDecimal(txtQuantity.Text);
            entityHd.ProductionDate = Helper.GetDatePickerValue(txtProductionDate.Text);
            entityHd.Remarks = txtNotes.Text;
            entityHd.BatchNumber = txtBatchNumber.Text;
            entityHd.ReferenceNo = txtReferenceNo.Text;
            entityHd.UnitPrice = Convert.ToDecimal(txtUnitPrice.Text);
            entityHd.FixedCostAmount = Convert.ToDecimal(Request.Form[txtFixedCostAmount.UniqueID]);
        }
        
        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ItemProductionHdDao entityHdDao = new ItemProductionHdDao(ctx);
            ItemProductionDtDao entityDtDao = new ItemProductionDtDao(ctx);
            try
            {
                ItemProductionHd entityHd = new ItemProductionHd();
                ControlToEntity(entityHd);
                entityHd.ProductionNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.PRODUCTION_PROCESS, entityHd.ProductionDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);
                entityHd.ProductionID = BusinessLayer.GetItemProductionHdMaxID(ctx);

                string filterExpression = string.Format("ItemID = {0} AND IsDeleted = 0", hdnItemID.Value);
                List<ItemBOM> lstEntity = BusinessLayer.GetItemBOMList(filterExpression, ctx);
                string lstItemID = "";
                foreach (ItemBOM itemBOM in lstEntity)
                {
                    if (lstItemID != "")
                        lstItemID += ",";
                    lstItemID += itemBOM.BillOfMaterialID.ToString();
                }
                if (lstItemID != "")
                    filterExpression = string.Format("SiteID = '{0}' AND ItemID IN ({1})", AppSession.UserLogin.SiteID, lstItemID);
                lstItemCost = BusinessLayer.GetItemCostList(filterExpression, ctx);

                foreach (ItemBOM itemBOM in lstEntity)
                {
                    ItemProductionDt entityDt = new ItemProductionDt();
                    entityDt.ProductionID = entityHd.ProductionID;
                    entityDt.ItemID = entityHd.ItemID;
                    entityDt.BillOfMaterialID = itemBOM.BillOfMaterialID;
                    entityDt.SequenceNo = itemBOM.SequenceNo;
                    entityDt.ConversionFactor = itemBOM.BOMQuantity / itemBOM.ItemQuantity;
                    entityDt.BOMQuantity = entityHd.Quantity * entityDt.ConversionFactor;

                    ItemCost itemCost = lstItemCost.FirstOrDefault(p => p.ItemID == itemBOM.BillOfMaterialID);
                    entityDt.CostAmount = (itemCost.TotalBurden + itemCost.TotalLabor + itemCost.TotalMaterial + itemCost.TotalOverhead + itemCost.TotalSubContract);
                    entityDtDao.Insert(entityDt);
                }

                retval = entityHd.ProductionID.ToString();
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ItemProductionHdDao entityHdDao = new ItemProductionHdDao(ctx);
            ItemProductionDtDao entityDtDao = new ItemProductionDtDao(ctx);
            try
            {
                ItemProductionHd entityHd = entityHdDao.Get(Convert.ToInt32(hdnProductionID.Value));
                ControlToEntity(entityHd);
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Update(entityHd);

                string filterExpression = string.Format("ProductionID = {0}", entityHd.ProductionID);
                List<ItemProductionDt> lstItemProductionDt = BusinessLayer.GetItemProductionDtList(filterExpression, ctx);
                foreach (ItemProductionDt entityDt in lstItemProductionDt)
                {
                    entityDt.BOMQuantity = entityHd.Quantity * entityDt.ConversionFactor;
                    entityDtDao.Update(entityDt);
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        #region Approve Proposed Void Entity
        protected override bool OnApproveRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ItemProductionHdDao itemHdDao = new ItemProductionHdDao(ctx);
            try
            {
                ItemProductionHd itemHd = itemHdDao.Get(Convert.ToInt32(hdnProductionID.Value));
                itemHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                itemHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                itemHdDao.Update(itemHd);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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

        protected override bool OnProposeRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ItemProductionHdDao itemHdDao = new ItemProductionHdDao(ctx);
            try
            {
                ItemProductionHd itemHd = itemHdDao.Get(Convert.ToInt32(hdnProductionID.Value));
                itemHd.GCTransactionStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                itemHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                itemHdDao.Update(itemHd);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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

        protected override bool OnVoidRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ItemProductionHdDao itemHdDao = new ItemProductionHdDao(ctx);
            try
            {
                ItemProductionHd itemHd = itemHdDao.Get(Convert.ToInt32(hdnProductionID.Value));
                itemHd.GCTransactionStatus = Constant.TransactionStatus.VOID;
                itemHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                itemHdDao.Update(itemHd);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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
        #endregion

        #endregion
    }
}