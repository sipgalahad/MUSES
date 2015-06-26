using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using DevExpress.Web.ASPxEditors;
using System.Reflection;
using System.Collections;
using CodeX.Common;
using CodeX.Data.Core.Dal;

namespace CodeX.Muses.Web.Accounting.Program
{
    public partial class GLWarehouseProductLineAccountEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Accounting.GL_WAREHOUSE_PRODUCT_LINE_ACCOUNT;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String[] param = Request.QueryString["id"].Split('|');
                hdnID.Value = param[0];
                vGLWarehouseProductLineAccount entity = BusinessLayer.GetvGLWarehouseProductLineAccountList(String.Format("ID = {0}", hdnID.Value))[0];
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }

            txtProductLineCode.Focus();
        }

        protected override void SetControlProperties()
        {
            String filterExpression = String.Format("ParentID IN ('{0}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.ITEM_TYPE);
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(filterExpression);

            Methods.SetComboBoxField(cboGCItemType, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.ITEM_TYPE).ToList(), "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnProductLineID, new ControlEntrySetting(true, false));
            SetControlEntrySetting(txtProductLineCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtProductLineName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(cboGCItemType, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtNotes, new ControlEntrySetting(true, true, false));

            #region Pengaturan Perkiraan untuk Aktiva Tetap
            SetControlEntrySetting(hdnInventoryID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnInventorySearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnInventorySubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtInventoryGLAccountNo, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtInventoryGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblInventorySubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnInventorySubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtInventorySubLedgerCode, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtInventorySubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnCOGSID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnCOGSSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnCOGSSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtCOGSGLAccountNo, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtCOGSGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblCOGSSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnCOGSSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtCOGSSubLedgerCode, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtCOGSSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnConsumption, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnConsumptionSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnConsumptionSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtConsumptionGLAccountNo, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtConsumptionGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblConsumptionSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnConsumptionSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtConsumptionSubLedgerCode, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtConsumptionSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnAdjustmentIN, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAdjustmentINSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAdjustmentINSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAdjustmentINGLAccountNo, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtAdjustmentINGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblAdjustmentINSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnAdjustmentINSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAdjustmentINSubLedgerCode, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtAdjustmentINSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnAdjustmentOUT, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAdjustmentOUTSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAdjustmentOUTSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAdjustmentOUTGLAccountNo, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtAdjustmentOUTGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblAdjustmentOUTSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnAdjustmentOUTSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAdjustmentOUTSubLedgerCode, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtAdjustmentOUTSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnInventoryVATID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnInventoryVATSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnInventoryVATSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtInventoryVATGLAccountNo, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtInventoryVATGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblInventoryVATSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnInventoryVATSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtInventoryVATSubLedgerCode, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtInventoryVATSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnInventoryDiscountID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnInventoryDiscountSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnInventoryDiscountSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtInventoryDiscountGLAccountNo, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtInventoryDiscountGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblInventoryDiscountSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnInventoryDiscountSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtInventoryDiscountSubLedgerCode, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtInventoryDiscountSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnPurchasePriceVariantID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnPurchasePriceVariantSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnPurchasePriceVariantSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtPurchasePriceVariantGLAccountNo, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtPurchasePriceVariantGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblPurchasePriceVariantSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnPurchasePriceVariantSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtPurchasePriceVariantSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtPurchasePriceVariantSubLedgerName, new ControlEntrySetting(false, false, false));
            #endregion
        }

        private void EntityToControl(vGLWarehouseProductLineAccount entity)
        {
            hdnProductLineID.Value = entity.ProductLineID.ToString();
            txtProductLineCode.Text = entity.ProductLineCode;
            txtProductLineName.Text = entity.ProductLineName;
            cboGCItemType.Value = entity.GCItemType;
            txtNotes.Text = entity.Remarks;
            
            #region Pengaturan Perkiraan untuk Aktiva Tetap
            #region Inventory
            hdnInventoryID.Value = entity.Inventory.ToString();
            txtInventoryGLAccountNo.Text = entity.InventoryGLAccountNo;
            txtInventoryGLAccountName.Text = entity.InventoryGLAccountName;

            hdnInventorySubLedgerID.Value = entity.InventorySubLedgerID.ToString();
            hdnInventorySearchDialogTypeName.Value = entity.InventorySearchDialogTypeName;
            hdnInventoryIDFieldName.Value = entity.InventoryIDFieldName;
            hdnInventoryCodeFieldName.Value = entity.InventoryCodeFieldName;
            hdnInventoryDisplayFieldName.Value = entity.InventoryDisplayFieldName;
            hdnInventoryMethodName.Value = entity.InventoryMethodName;
            hdnInventoryFilterExpression.Value = entity.InventoryFilterExpression;

            hdnInventorySubLedger.Value = entity.InventorySubLedger.ToString();
            txtInventorySubLedgerCode.Text = entity.InventorySubLedgerCode;
            txtInventorySubLedgerName.Text = entity.InventorySubLedgerName;
            #endregion

            #region Inventory VAT
            hdnInventoryVATID.Value = entity.InventoryVAT.ToString();
            txtInventoryVATGLAccountNo.Text = entity.InventoryVATGLAccountNo;
            txtInventoryVATGLAccountName.Text = entity.InventoryVATGLAccountName;

            hdnInventoryVATSubLedgerID.Value = entity.InventoryVATSubLedgerID.ToString();
            hdnInventoryVATSearchDialogTypeName.Value = entity.InventoryVATSearchDialogTypeName;
            hdnInventoryVATIDFieldName.Value = entity.InventoryVATIDFieldName;
            hdnInventoryVATCodeFieldName.Value = entity.InventoryVATCodeFieldName;
            hdnInventoryVATDisplayFieldName.Value = entity.InventoryVATDisplayFieldName;
            hdnInventoryVATMethodName.Value = entity.InventoryVATMethodName;
            hdnInventoryVATFilterExpression.Value = entity.InventoryVATFilterExpression;

            hdnInventoryVATSubLedger.Value = entity.InventoryVATSubLedger.ToString();
            txtInventoryVATSubLedgerCode.Text = entity.InventoryVATSubLedgerCode;
            txtInventoryVATSubLedgerName.Text = entity.InventoryVATSubLedgerName;
            #endregion

            #region Inventory Discount
            hdnInventoryDiscountID.Value = entity.InventoryDiscount.ToString();
            txtInventoryDiscountGLAccountNo.Text = entity.InventoryDiscountGLAccountNo;
            txtInventoryDiscountGLAccountName.Text = entity.InventoryDiscountGLAccountName;

            hdnInventoryDiscountSubLedgerID.Value = entity.InventoryDiscountSubLedgerID.ToString();
            hdnInventoryDiscountSearchDialogTypeName.Value = entity.InventoryDiscountSearchDialogTypeName;
            hdnInventoryDiscountIDFieldName.Value = entity.InventoryDiscountIDFieldName;
            hdnInventoryDiscountCodeFieldName.Value = entity.InventoryDiscountCodeFieldName;
            hdnInventoryDiscountDisplayFieldName.Value = entity.InventoryDiscountDisplayFieldName;
            hdnInventoryDiscountMethodName.Value = entity.InventoryDiscountMethodName;
            hdnInventoryDiscountFilterExpression.Value = entity.InventoryDiscountFilterExpression;

            hdnInventoryDiscountSubLedger.Value = entity.InventoryDiscountSubLedger.ToString();
            txtInventoryDiscountSubLedgerCode.Text = entity.InventoryDiscountSubLedgerCode;
            txtInventoryDiscountSubLedgerName.Text = entity.InventoryDiscountSubLedgerName;
            #endregion

            #region COGS
            hdnCOGSID.Value = entity.COGS.ToString();
            txtCOGSGLAccountNo.Text = entity.COGSGLAccountNo;
            txtCOGSGLAccountName.Text = entity.COGSGLAccountName;

            hdnCOGSSubLedgerID.Value = entity.COGSSubLedgerID.ToString();
            hdnCOGSSearchDialogTypeName.Value = entity.COGSSearchDialogTypeName;
            hdnCOGSIDFieldName.Value = entity.COGSIDFieldName;
            hdnCOGSCodeFieldName.Value = entity.COGSCodeFieldName;
            hdnCOGSDisplayFieldName.Value = entity.COGSDisplayFieldName;
            hdnCOGSMethodName.Value = entity.COGSMethodName;
            hdnCOGSFilterExpression.Value = entity.COGSFilterExpression;

            hdnCOGSSubLedger.Value = entity.COGSSubLedger.ToString();
            txtCOGSSubLedgerCode.Text = entity.COGSSubLedgerCode;
            txtCOGSSubLedgerName.Text = entity.COGSSubLedgerName;
            #endregion

            #region Consumption
            hdnConsumption.Value = entity.Consumption.ToString();
            txtConsumptionGLAccountNo.Text = entity.ConsumptionGLAccountNo;
            txtConsumptionGLAccountName.Text = entity.ConsumptionGLAccountName;

            hdnConsumptionSubLedgerID.Value = entity.ConsumptionSubLedgerID.ToString();
            hdnConsumptionSearchDialogTypeName.Value = entity.ConsumptionSearchDialogTypeName;
            hdnConsumptionIDFieldName.Value = entity.ConsumptionIDFieldName;
            hdnConsumptionCodeFieldName.Value = entity.ConsumptionCodeFieldName;
            hdnConsumptionDisplayFieldName.Value = entity.ConsumptionDisplayFieldName;
            hdnConsumptionMethodName.Value = entity.ConsumptionMethodName;
            hdnConsumptionFilterExpression.Value = entity.ConsumptionFilterExpression;

            hdnConsumptionSubLedger.Value = entity.ConsumptionSubLedger.ToString();
            txtConsumptionSubLedgerCode.Text = entity.ConsumptionSubLedgerCode;
            txtConsumptionSubLedgerName.Text = entity.ConsumptionSubLedgerName;
            #endregion

            #region AdjustmentIN
            hdnAdjustmentIN.Value = entity.AdjustmentIN.ToString();
            txtAdjustmentINGLAccountNo.Text = entity.AdjustmentINGLAccountNo;
            txtAdjustmentINGLAccountName.Text = entity.AdjustmentINGLAccountName;

            hdnAdjustmentINSubLedgerID.Value = entity.AdjustmentINSubLedgerID.ToString();
            hdnAdjustmentINSearchDialogTypeName.Value = entity.AdjustmentINSearchDialogTypeName;
            hdnAdjustmentINIDFieldName.Value = entity.AdjustmentINIDFieldName;
            hdnAdjustmentINCodeFieldName.Value = entity.AdjustmentINCodeFieldName;
            hdnAdjustmentINDisplayFieldName.Value = entity.AdjustmentINDisplayFieldName;
            hdnAdjustmentINMethodName.Value = entity.AdjustmentINMethodName;
            hdnAdjustmentINFilterExpression.Value = entity.AdjustmentINFilterExpression;

            hdnAdjustmentINSubLedger.Value = entity.AdjustmentINSubLedger.ToString();
            txtAdjustmentINSubLedgerCode.Text = entity.AdjustmentINSubLedgerCode;
            txtAdjustmentINSubLedgerName.Text = entity.AdjustmentINSubLedgerName;
            #endregion

            #region AdjustmentOUT
            hdnAdjustmentOUT.Value = entity.AdjustmentOUT.ToString();
            txtAdjustmentOUTGLAccountNo.Text = entity.AdjustmentOUTGLAccountNo;
            txtAdjustmentOUTGLAccountName.Text = entity.AdjustmentOUTGLAccountName;

            hdnAdjustmentOUTSubLedgerID.Value = entity.AdjustmentOUTSubLedgerID.ToString();
            hdnAdjustmentOUTSearchDialogTypeName.Value = entity.AdjustmentOUTSearchDialogTypeName;
            hdnAdjustmentOUTIDFieldName.Value = entity.AdjustmentOUTIDFieldName;
            hdnAdjustmentOUTCodeFieldName.Value = entity.AdjustmentOUTCodeFieldName;
            hdnAdjustmentOUTDisplayFieldName.Value = entity.AdjustmentOUTDisplayFieldName;
            hdnAdjustmentOUTMethodName.Value = entity.AdjustmentOUTMethodName;
            hdnAdjustmentOUTFilterExpression.Value = entity.AdjustmentOUTFilterExpression;

            hdnAdjustmentOUTSubLedger.Value = entity.AdjustmentOUTSubLedger.ToString();
            txtAdjustmentOUTSubLedgerCode.Text = entity.AdjustmentOUTSubLedgerCode;
            txtAdjustmentOUTSubLedgerName.Text = entity.AdjustmentOUTSubLedgerName;
            #endregion

            #region PurchasePriceVariant
            hdnPurchasePriceVariantID.Value = entity.PurchasePriceVariant.ToString();
            txtPurchasePriceVariantGLAccountNo.Text = entity.PurchasePriceVariantGLAccountNo;
            txtPurchasePriceVariantGLAccountName.Text = entity.PurchasePriceVariantGLAccountName;
            hdnPurchasePriceVariantSubLedgerID.Value = entity.PurchasePriceVariantSubLedgerID.ToString();
            hdnPurchasePriceVariantSearchDialogTypeName.Value = entity.PurchasePriceVariantSearchDialogTypeName;
            hdnPurchasePriceVariantIDFieldName.Value = entity.PurchasePriceVariantIDFieldName;
            hdnPurchasePriceVariantCodeFieldName.Value = entity.PurchasePriceVariantCodeFieldName;
            hdnPurchasePriceVariantDisplayFieldName.Value = entity.PurchasePriceVariantDisplayFieldName;
            hdnPurchasePriceVariantMethodName.Value = entity.PurchasePriceVariantMethodName;
            hdnPurchasePriceVariantFilterExpression.Value = entity.PurchasePriceVariantFilterExpression;

            hdnPurchasePriceVariantSubLedger.Value = entity.PurchasePriceVariantSubLedger.ToString();
            txtPurchasePriceVariantSubLedgerCode.Text = entity.PurchasePriceVariantSubLedgerCode.ToString();
            txtPurchasePriceVariantSubLedgerName.Text = entity.PurchasePriceVariantSubLedgerName.ToString();
            #endregion
            #endregion
        }

        private void ControlToEntity(GLWarehouseProductLineAccount entity)
        {
            entity.Remarks = txtNotes.Text;
            entity.GCItemType = cboGCItemType.Value.ToString();
            entity.ProductLineID = Convert.ToInt32(hdnProductLineID.Value);

            #region Pengaturan Perkiraan untuk Aktiva Tetap
            #region Inventory
            entity.Inventory = Convert.ToInt32(hdnInventoryID.Value);
            if (hdnInventorySubLedger.Value != "" && hdnInventorySubLedger.Value != "0")
                entity.InventorySubLedger = Convert.ToInt32(hdnInventorySubLedger.Value);
            else
                entity.InventorySubLedger = null;
            #endregion

            #region COGS
            entity.COGS = Convert.ToInt32(hdnCOGSID.Value);
            if (hdnCOGSSubLedger.Value != "" && hdnCOGSSubLedger.Value != "0")
                entity.COGSSubLedger = Convert.ToInt32(hdnCOGSSubLedger.Value);
            else
                entity.COGSSubLedger = null;
            #endregion

            #region Consumption
            entity.Consumption = Convert.ToInt32(hdnConsumption.Value);
            if (hdnConsumptionSubLedger.Value != "" && hdnConsumptionSubLedger.Value != "0")
                entity.ConsumptionSubLedger = Convert.ToInt32(hdnConsumptionSubLedger.Value);
            else
                entity.ConsumptionSubLedger = null;
            #endregion

            #region AdjustmentIN
            entity.AdjustmentIN = Convert.ToInt32(hdnAdjustmentIN.Value);
            if (hdnAdjustmentINSubLedger.Value != "" && hdnAdjustmentINSubLedger.Value != "0")
                entity.AdjustmentINSubLedger = Convert.ToInt32(hdnAdjustmentINSubLedger.Value);
            else
                entity.AdjustmentINSubLedger = null;
            #endregion

            #region AdjustmentOUT
            entity.AdjustmentOUT = Convert.ToInt32(hdnAdjustmentOUT.Value);
            if (hdnAdjustmentOUTSubLedger.Value != "" && hdnAdjustmentOUTSubLedger.Value != "0")
                entity.AdjustmentOUTSubLedger = Convert.ToInt32(hdnAdjustmentOUTSubLedger.Value);
            else
                entity.AdjustmentOUTSubLedger = null;
            #endregion

            #region InventoryVAT
            entity.InventoryVAT = Convert.ToInt32(hdnInventoryVATID.Value);
            if (hdnInventoryVATSubLedger.Value != "" && hdnInventoryVATSubLedger.Value != "0")
                entity.InventoryVATSubLedger = Convert.ToInt32(hdnInventoryVATSubLedger.Value);
            else
                entity.InventoryVATSubLedger = null;
            #endregion

            #region InventoryDiscount
            entity.InventoryDiscount = Convert.ToInt32(hdnInventoryDiscountID.Value);
            if (hdnInventoryDiscountSubLedger.Value != "" && hdnInventoryDiscountSubLedger.Value != "0")
                entity.InventoryDiscountSubLedger = Convert.ToInt32(hdnInventoryDiscountSubLedger.Value);
            else
                entity.InventoryDiscountSubLedger = null;
            #endregion

            #region PurchasePriceVariant
            entity.PurchasePriceVariant = Convert.ToInt32(hdnPurchasePriceVariantID.Value);
            if (hdnPurchasePriceVariantSubLedger.Value != "" && hdnPurchasePriceVariantSubLedger.Value != "0")
                entity.PurchasePriceVariantSubLedger = Convert.ToInt32(hdnPurchasePriceVariantSubLedger.Value);
            else
                entity.PurchasePriceVariantSubLedger = null;
            #endregion
            #endregion
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            GLWarehouseProductLineAccountDao entityDao = new GLWarehouseProductLineAccountDao(ctx);
            bool result = false;
            try
            {
                GLWarehouseProductLineAccount entity = new GLWarehouseProductLineAccount();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetGLWarehouseProductLineAccountMaxID(ctx).ToString();
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
            try
            {
                GLWarehouseProductLineAccount entity = BusinessLayer.GetGLWarehouseProductLineAccount(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateGLWarehouseProductLineAccount(entity);
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