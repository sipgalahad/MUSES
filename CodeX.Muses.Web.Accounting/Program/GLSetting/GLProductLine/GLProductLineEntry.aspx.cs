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
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Muses.Web.Accounting.Program
{
    public partial class GLProductLineEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Accounting.GL_PRODUCT_LINE;
        }

        protected override void InitializeDataControl()
        {
            String[] param = Request.QueryString["id"].Split('|');
            if (param[0] == "edit")
            {
                IsAdd = false;
                String ID = param[1];
                hdnID.Value = ID;
                ProductLine entity = BusinessLayer.GetProductLineList(String.Format("ProductLineID = {0}", hdnID.Value))[0];
                vProductLineDt entityDt = BusinessLayer.GetvProductLineDtList(String.Format("ProductLineID = {0}", entity.ProductLineID))[0];

                SetControlProperties();
                EntityToControl(entity, entityDt);
                hdnGCItemType.Value = entity.GCItemType;
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
                hdnGCItemType.Value = param[1];
            }

            txtProductLineCode.Focus();
        }

        protected override void SetControlProperties()
        {
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtProductLineCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtProductLineName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));

            #region Pengaturan Perkiraan
            SetControlEntrySetting(hdnInventoryID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnInventorySearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnInventorySubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtInventoryGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtInventoryGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblInventorySubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnInventorySubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtInventorySubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtInventorySubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnInventoryVATID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnInventoryVATSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnInventoryVATSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtInventoryVATGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtInventoryVATGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblInventoryVATSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnInventoryVATSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtInventoryVATSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtInventoryVATSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnCOGSID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnCOGSSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnCOGSSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtCOGSGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtCOGSGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblCOGSSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnCOGSSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtCOGSSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtCOGSSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnPurchaseID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnPurchaseSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnPurchaseSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtPurchaseGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtPurchaseGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblPurchaseSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnPurchaseSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtPurchaseSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtPurchaseSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnPurchaseReturnID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnPurchaseReturnSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnPurchaseReturnSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtPurchaseReturnGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtPurchaseReturnGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblPurchaseReturnSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnPurchaseReturnSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtPurchaseReturnSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtPurchaseReturnSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnPurchaseDiscount, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnPurchaseDiscountSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnPurchaseDiscountSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtPurchaseDiscountGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtPurchaseDiscountGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblPurchaseDiscountSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnPurchaseDiscountSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtPurchaseDiscountSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtPurchaseDiscountSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnPurchasePriceVariant, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnPurchasePriceVariantSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnPurchasePriceVariantSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtPurchasePriceVariantGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtPurchasePriceVariantGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblPurchasePriceVariantSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnPurchasePriceVariantSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtPurchasePriceVariantSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtPurchasePriceVariantSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnSales, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSalesSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSalesSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSalesGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtSalesGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblSalesSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnSalesSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSalesSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtSalesSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnSalesReturn, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSalesReturnSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSalesReturnSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSalesReturnGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtSalesReturnGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblSalesReturnSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnSalesReturnSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSalesReturnSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtSalesReturnSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnSalesDiscount, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSalesDiscountSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSalesDiscountSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSalesDiscountGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtSalesDiscountGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblSalesDiscountSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnSalesDiscountSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSalesDiscountSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtSalesDiscountSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnMaterialRevenue, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnMaterialRevenueSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnMaterialRevenueSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtMaterialRevenueGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtMaterialRevenueGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblMaterialRevenueSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnMaterialRevenueSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtMaterialRevenueSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtMaterialRevenueSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnConsumption, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnConsumptionSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnConsumptionSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtConsumptionGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtConsumptionGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblConsumptionSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnConsumptionSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtConsumptionSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtConsumptionSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnAdjustmentIN, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAdjustmentINSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAdjustmentINSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAdjustmentINGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtAdjustmentINGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblAdjustmentINSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnAdjustmentINSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAdjustmentINSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtAdjustmentINSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnAdjustmentOUT, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAdjustmentOUTSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAdjustmentOUTSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAdjustmentOUTGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtAdjustmentOUTGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblAdjustmentOUTSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnAdjustmentOUTSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAdjustmentOUTSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtAdjustmentOUTSubLedgerName, new ControlEntrySetting(false, false, false));
            #endregion
        }

        private void EntityToControl(ProductLine entity, vProductLineDt entityDt)
        {
            txtProductLineCode.Text = entity.ProductLineCode;
            txtProductLineName.Text = entity.ProductLineName;
            txtRemarks.Text = entity.Remarks;

            #region Pengaturan Perkiraan
            #region Inventory
            hdnInventoryID.Value = entityDt.Inventory.ToString();
            txtInventoryGLAccountNo.Text = entityDt.InventoryGLAccountNo;
            txtInventoryGLAccountName.Text = entityDt.InventoryGLAccountName;
            hdnInventorySubLedgerID.Value = entityDt.InventorySubLedgerID.ToString();
            hdnInventorySearchDialogTypeName.Value = entityDt.InventorySearchDialogTypeName;
            hdnInventoryIDFieldName.Value = entityDt.InventoryIDFieldName;
            hdnInventoryCodeFieldName.Value = entityDt.InventoryCodeFieldName;
            hdnInventoryDisplayFieldName.Value = entityDt.InventoryDisplayFieldName;
            hdnInventoryMethodName.Value = entityDt.InventoryMethodName;
            hdnInventoryFilterExpression.Value = entityDt.InventoryFilterExpression;

            hdnInventorySubLedger.Value = entityDt.InventorySubLedger.ToString();
            txtInventorySubLedgerCode.Text = entityDt.InventorySubLedgerCode.ToString();
            txtInventorySubLedgerName.Text = entityDt.InventorySubLedgerName.ToString();
            #endregion

            #region InventoryVAT
            hdnInventoryVATID.Value = entityDt.InventoryVAT.ToString();
            txtInventoryVATGLAccountNo.Text = entityDt.InventoryVATGLAccountNo;
            txtInventoryVATGLAccountName.Text = entityDt.InventoryVATGLAccountName;
            hdnInventoryVATSubLedgerID.Value = entityDt.InventoryVATSubLedgerID.ToString();
            hdnInventoryVATSearchDialogTypeName.Value = entityDt.InventoryVATSearchDialogTypeName;
            hdnInventoryVATIDFieldName.Value = entityDt.InventoryVATIDFieldName;
            hdnInventoryVATCodeFieldName.Value = entityDt.InventoryVATCodeFieldName;
            hdnInventoryVATDisplayFieldName.Value = entityDt.InventoryVATDisplayFieldName;
            hdnInventoryVATMethodName.Value = entityDt.InventoryVATMethodName;
            hdnInventoryVATFilterExpression.Value = entityDt.InventoryVATFilterExpression;

            hdnInventoryVATSubLedger.Value = entityDt.InventoryVATSubLedger.ToString();
            txtInventoryVATSubLedgerCode.Text = entityDt.InventoryVATSubLedgerCode.ToString();
            txtInventoryVATSubLedgerName.Text = entityDt.InventoryVATSubLedgerName.ToString();
            #endregion

            #region COGS
            hdnPurchaseID.Value = entityDt.Purchase.ToString();
            txtPurchaseGLAccountNo.Text = entityDt.PurchaseGLAccountNo;
            txtPurchaseGLAccountName.Text = entityDt.PurchaseGLAccountName;
            hdnPurchaseSubLedgerID.Value = entityDt.PurchaseSubLedgerID.ToString();
            hdnPurchaseSearchDialogTypeName.Value = entityDt.PurchaseSearchDialogTypeName;
            hdnPurchaseIDFieldName.Value = entityDt.PurchaseIDFieldName;
            hdnPurchaseCodeFieldName.Value = entityDt.PurchaseCodeFieldName;
            hdnPurchaseDisplayFieldName.Value = entityDt.PurchaseDisplayFieldName;
            hdnPurchaseMethodName.Value = entityDt.PurchaseMethodName;
            hdnPurchaseFilterExpression.Value = entityDt.PurchaseFilterExpression;

            hdnPurchaseSubLedger.Value = entityDt.PurchaseSubLedger.ToString();
            txtPurchaseSubLedgerCode.Text = entityDt.PurchaseSubLedgerCode.ToString();
            txtPurchaseSubLedgerName.Text = entityDt.PurchaseSubLedgerName.ToString();
            #endregion

            #region Purchase
            hdnPurchaseID.Value = entityDt.Purchase.ToString();
            txtPurchaseGLAccountNo.Text = entityDt.PurchaseGLAccountNo;
            txtPurchaseGLAccountName.Text = entityDt.PurchaseGLAccountName;
            hdnPurchaseSubLedgerID.Value = entityDt.PurchaseSubLedgerID.ToString();
            hdnPurchaseSearchDialogTypeName.Value = entityDt.PurchaseSearchDialogTypeName;
            hdnPurchaseIDFieldName.Value = entityDt.PurchaseIDFieldName;
            hdnPurchaseCodeFieldName.Value = entityDt.PurchaseCodeFieldName;
            hdnPurchaseDisplayFieldName.Value = entityDt.PurchaseDisplayFieldName;
            hdnPurchaseMethodName.Value = entityDt.PurchaseMethodName;
            hdnPurchaseFilterExpression.Value = entityDt.PurchaseFilterExpression;

            hdnPurchaseSubLedger.Value = entityDt.PurchaseSubLedger.ToString();
            txtPurchaseSubLedgerCode.Text = entityDt.PurchaseSubLedgerCode.ToString();
            txtPurchaseSubLedgerName.Text = entityDt.PurchaseSubLedgerName.ToString();
            #endregion

            #region PurchaseReturn
            hdnPurchaseReturnID.Value = entityDt.PurchaseReturn.ToString();
            txtPurchaseReturnGLAccountNo.Text = entityDt.PurchaseReturnGLAccountNo;
            txtPurchaseReturnGLAccountName.Text = entityDt.PurchaseReturnGLAccountName;
            hdnPurchaseReturnSubLedgerID.Value = entityDt.PurchaseReturnSubLedgerID.ToString();
            hdnPurchaseReturnSearchDialogTypeName.Value = entityDt.PurchaseReturnSearchDialogTypeName;
            hdnPurchaseReturnIDFieldName.Value = entityDt.PurchaseReturnIDFieldName;
            hdnPurchaseReturnCodeFieldName.Value = entityDt.PurchaseReturnCodeFieldName;
            hdnPurchaseReturnDisplayFieldName.Value = entityDt.PurchaseReturnDisplayFieldName;
            hdnPurchaseReturnMethodName.Value = entityDt.PurchaseReturnMethodName;
            hdnPurchaseReturnFilterExpression.Value = entityDt.PurchaseReturnFilterExpression;

            hdnPurchaseReturnSubLedger.Value = entityDt.PurchaseReturnSubLedger.ToString();
            txtPurchaseReturnSubLedgerCode.Text = entityDt.PurchaseReturnSubLedgerCode.ToString();
            txtPurchaseReturnSubLedgerName.Text = entityDt.PurchaseReturnSubLedgerName.ToString();
            #endregion

            #region PurchaseDiscount
            hdnPurchaseDiscount.Value = entityDt.PurchaseDiscount.ToString();
            txtPurchaseDiscountGLAccountNo.Text = entityDt.PurchaseDiscountGLAccountNo;
            txtPurchaseDiscountGLAccountName.Text = entityDt.PurchaseDiscountGLAccountName;
            hdnPurchaseDiscountSubLedgerID.Value = entityDt.PurchaseDiscountSubLedgerID.ToString();
            hdnPurchaseDiscountSearchDialogTypeName.Value = entityDt.PurchaseDiscountSearchDialogTypeName;
            hdnPurchaseDiscountIDFieldName.Value = entityDt.PurchaseDiscountIDFieldName;
            hdnPurchaseDiscountCodeFieldName.Value = entityDt.PurchaseDiscountCodeFieldName;
            hdnPurchaseDiscountDisplayFieldName.Value = entityDt.PurchaseDiscountDisplayFieldName;
            hdnPurchaseDiscountMethodName.Value = entityDt.PurchaseDiscountMethodName;
            hdnPurchaseDiscountFilterExpression.Value = entityDt.PurchaseDiscountFilterExpression;

            hdnPurchaseDiscountSubLedger.Value = entityDt.PurchaseDiscountSubLedger.ToString();
            txtPurchaseDiscountSubLedgerCode.Text = entityDt.PurchaseDiscountSubLedgerCode.ToString();
            txtPurchaseDiscountSubLedgerName.Text = entityDt.PurchaseDiscountSubLedgerName.ToString();
            #endregion

            #region PurchasePriceVariant
            hdnPurchasePriceVariant.Value = entityDt.PurchasePriceVariant.ToString();
            txtPurchasePriceVariantGLAccountNo.Text = entityDt.PurchasePriceVariantGLAccountNo;
            txtPurchasePriceVariantGLAccountName.Text = entityDt.PurchasePriceVariantGLAccountName;
            hdnPurchasePriceVariantSubLedgerID.Value = entityDt.PurchasePriceVariantSubLedgerID.ToString();
            hdnPurchasePriceVariantSearchDialogTypeName.Value = entityDt.PurchasePriceVariantSearchDialogTypeName;
            hdnPurchasePriceVariantIDFieldName.Value = entityDt.PurchasePriceVariantIDFieldName;
            hdnPurchasePriceVariantCodeFieldName.Value = entityDt.PurchasePriceVariantCodeFieldName;
            hdnPurchasePriceVariantDisplayFieldName.Value = entityDt.PurchasePriceVariantDisplayFieldName;
            hdnPurchasePriceVariantMethodName.Value = entityDt.PurchasePriceVariantMethodName;
            hdnPurchasePriceVariantFilterExpression.Value = entityDt.PurchasePriceVariantFilterExpression;

            hdnPurchasePriceVariantSubLedger.Value = entityDt.PurchasePriceVariantSubLedger.ToString();
            txtPurchasePriceVariantSubLedgerCode.Text = entityDt.PurchasePriceVariantSubLedgerCode.ToString();
            txtPurchasePriceVariantSubLedgerName.Text = entityDt.PurchasePriceVariantSubLedgerName.ToString();
            #endregion

            #region Sales
            hdnSales.Value = entityDt.Sales.ToString();
            txtSalesGLAccountNo.Text = entityDt.SalesGLAccountNo;
            txtSalesGLAccountName.Text = entityDt.SalesGLAccountName;
            hdnSalesSubLedgerID.Value = entityDt.SalesSubLedgerID.ToString();
            hdnSalesSearchDialogTypeName.Value = entityDt.SalesSearchDialogTypeName;
            hdnSalesIDFieldName.Value = entityDt.SalesIDFieldName;
            hdnSalesCodeFieldName.Value = entityDt.SalesCodeFieldName;
            hdnSalesDisplayFieldName.Value = entityDt.SalesDisplayFieldName;
            hdnSalesMethodName.Value = entityDt.SalesMethodName;
            hdnSalesFilterExpression.Value = entityDt.SalesFilterExpression;

            hdnSalesSubLedger.Value = entityDt.SalesSubLedger.ToString();
            txtSalesSubLedgerCode.Text = entityDt.SalesSubLedgerCode.ToString();
            txtSalesSubLedgerName.Text = entityDt.SalesSubLedgerName.ToString();
            #endregion

            #region SalesReturn
            hdnSalesReturn.Value = entityDt.SalesReturn.ToString();
            txtSalesReturnGLAccountNo.Text = entityDt.SalesReturnGLAccountNo;
            txtSalesReturnGLAccountName.Text = entityDt.SalesReturnGLAccountName;
            hdnSalesReturnSubLedgerID.Value = entityDt.SalesReturnSubLedgerID.ToString();
            hdnSalesReturnSearchDialogTypeName.Value = entityDt.SalesReturnSearchDialogTypeName;
            hdnSalesReturnIDFieldName.Value = entityDt.SalesReturnIDFieldName;
            hdnSalesReturnCodeFieldName.Value = entityDt.SalesReturnCodeFieldName;
            hdnSalesReturnDisplayFieldName.Value = entityDt.SalesReturnDisplayFieldName;
            hdnSalesReturnMethodName.Value = entityDt.SalesReturnMethodName;
            hdnSalesReturnFilterExpression.Value = entityDt.SalesReturnFilterExpression;

            hdnSalesReturnSubLedger.Value = entityDt.SalesReturnSubLedger.ToString();
            txtSalesReturnSubLedgerCode.Text = entityDt.SalesReturnSubLedgerCode.ToString();
            txtSalesReturnSubLedgerName.Text = entityDt.SalesReturnSubLedgerName.ToString();
            #endregion

            #region SalesDiscount
            hdnSalesDiscount.Value = entityDt.SalesDiscount.ToString();
            txtSalesDiscountGLAccountNo.Text = entityDt.SalesDiscountGLAccountNo;
            txtSalesDiscountGLAccountName.Text = entityDt.SalesDiscountGLAccountName;
            hdnSalesDiscountSubLedgerID.Value = entityDt.SalesDiscountSubLedgerID.ToString();
            hdnSalesDiscountSearchDialogTypeName.Value = entityDt.SalesDiscountSearchDialogTypeName;
            hdnSalesDiscountIDFieldName.Value = entityDt.SalesDiscountIDFieldName;
            hdnSalesDiscountCodeFieldName.Value = entityDt.SalesDiscountCodeFieldName;
            hdnSalesDiscountDisplayFieldName.Value = entityDt.SalesDiscountDisplayFieldName;
            hdnSalesDiscountMethodName.Value = entityDt.SalesDiscountMethodName;
            hdnSalesDiscountFilterExpression.Value = entityDt.SalesDiscountFilterExpression;

            hdnSalesDiscountSubLedger.Value = entityDt.SalesDiscountSubLedger.ToString();
            txtSalesDiscountSubLedgerCode.Text = entityDt.SalesDiscountSubLedgerCode.ToString();
            txtSalesDiscountSubLedgerName.Text = entityDt.SalesDiscountSubLedgerName.ToString();
            #endregion

            #region MaterialRevenue
            hdnMaterialRevenue.Value = entityDt.MaterialRevenue.ToString();
            txtMaterialRevenueGLAccountNo.Text = entityDt.MaterialRevenueGLAccountNo;
            txtMaterialRevenueGLAccountName.Text = entityDt.MaterialRevenueGLAccountName;
            hdnMaterialRevenueSubLedgerID.Value = entityDt.MaterialRevenueSubLedgerID.ToString();
            hdnMaterialRevenueSearchDialogTypeName.Value = entityDt.MaterialRevenueSearchDialogTypeName;
            hdnMaterialRevenueIDFieldName.Value = entityDt.MaterialRevenueIDFieldName;
            hdnMaterialRevenueCodeFieldName.Value = entityDt.MaterialRevenueCodeFieldName;
            hdnMaterialRevenueDisplayFieldName.Value = entityDt.MaterialRevenueDisplayFieldName;
            hdnMaterialRevenueMethodName.Value = entityDt.MaterialRevenueMethodName;
            hdnMaterialRevenueFilterExpression.Value = entityDt.MaterialRevenueFilterExpression;

            hdnMaterialRevenueSubLedger.Value = entityDt.MaterialRevenueSubLedger.ToString();
            txtMaterialRevenueSubLedgerCode.Text = entityDt.MaterialRevenueSubLedgerCode.ToString();
            txtMaterialRevenueSubLedgerName.Text = entityDt.MaterialRevenueSubLedgerName.ToString();
            #endregion

            #region Consumption
            hdnConsumption.Value = entityDt.Consumption.ToString();
            txtConsumptionGLAccountNo.Text = entityDt.ConsumptionGLAccountNo;
            txtConsumptionGLAccountName.Text = entityDt.ConsumptionGLAccountName;
            hdnConsumptionSubLedgerID.Value = entityDt.ConsumptionSubLedgerID.ToString();
            hdnConsumptionSearchDialogTypeName.Value = entityDt.ConsumptionSearchDialogTypeName;
            hdnConsumptionIDFieldName.Value = entityDt.ConsumptionIDFieldName;
            hdnConsumptionCodeFieldName.Value = entityDt.ConsumptionCodeFieldName;
            hdnConsumptionDisplayFieldName.Value = entityDt.ConsumptionDisplayFieldName;
            hdnConsumptionMethodName.Value = entityDt.ConsumptionMethodName;
            hdnConsumptionFilterExpression.Value = entityDt.ConsumptionFilterExpression;

            hdnConsumptionSubLedger.Value = entityDt.ConsumptionSubLedger.ToString();
            txtConsumptionSubLedgerCode.Text = entityDt.ConsumptionSubLedgerCode.ToString();
            txtConsumptionSubLedgerName.Text = entityDt.ConsumptionSubLedgerName.ToString();
            #endregion

            #region AdjustmentIN
            hdnAdjustmentIN.Value = entityDt.AdjustmentIN.ToString();
            txtAdjustmentINGLAccountNo.Text = entityDt.AdjustmentINGLAccountNo;
            txtAdjustmentINGLAccountName.Text = entityDt.AdjustmentINGLAccountName;
            hdnAdjustmentINSubLedgerID.Value = entityDt.AdjustmentINSubLedgerID.ToString();
            hdnAdjustmentINSearchDialogTypeName.Value = entityDt.AdjustmentINSearchDialogTypeName;
            hdnAdjustmentINIDFieldName.Value = entityDt.AdjustmentINIDFieldName;
            hdnAdjustmentINCodeFieldName.Value = entityDt.AdjustmentINCodeFieldName;
            hdnAdjustmentINDisplayFieldName.Value = entityDt.AdjustmentINDisplayFieldName;
            hdnAdjustmentINMethodName.Value = entityDt.AdjustmentINMethodName;
            hdnAdjustmentINFilterExpression.Value = entityDt.AdjustmentINFilterExpression;

            hdnAdjustmentINSubLedger.Value = entityDt.AdjustmentINSubLedger.ToString();
            txtAdjustmentINSubLedgerCode.Text = entityDt.AdjustmentINSubLedgerCode.ToString();
            txtAdjustmentINSubLedgerName.Text = entityDt.AdjustmentINSubLedgerName.ToString();
            #endregion

            #region AdjustmentOUT
            hdnAdjustmentOUT.Value = entityDt.AdjustmentOUT.ToString();
            txtAdjustmentOUTGLAccountNo.Text = entityDt.AdjustmentOUTGLAccountNo;
            txtAdjustmentOUTGLAccountName.Text = entityDt.AdjustmentOUTGLAccountName;
            hdnAdjustmentOUTSubLedgerID.Value = entityDt.AdjustmentOUTSubLedgerID.ToString();
            hdnAdjustmentOUTSearchDialogTypeName.Value = entityDt.AdjustmentOUTSearchDialogTypeName;
            hdnAdjustmentOUTIDFieldName.Value = entityDt.AdjustmentOUTIDFieldName;
            hdnAdjustmentOUTCodeFieldName.Value = entityDt.AdjustmentOUTCodeFieldName;
            hdnAdjustmentOUTDisplayFieldName.Value = entityDt.AdjustmentOUTDisplayFieldName;
            hdnAdjustmentOUTMethodName.Value = entityDt.AdjustmentOUTMethodName;
            hdnAdjustmentOUTFilterExpression.Value = entityDt.AdjustmentOUTFilterExpression;

            hdnAdjustmentOUTSubLedger.Value = entityDt.AdjustmentOUTSubLedger.ToString();
            txtAdjustmentOUTSubLedgerCode.Text = entityDt.AdjustmentOUTSubLedgerCode.ToString();
            txtAdjustmentOUTSubLedgerName.Text = entityDt.AdjustmentOUTSubLedgerName.ToString();
            #endregion
            #endregion
        }

        private void ControlToEntity(ProductLine entity, ProductLineDt entityDt)
        {
            entity.ProductLineCode = txtProductLineCode.Text;
            entity.ProductLineName = txtProductLineName.Text;
            entity.Remarks = txtRemarks.Text;

            #region Pengaturan Perkiraan
            #region Inventory
            if (hdnInventoryID.Value != "" && hdnInventoryID.Value != "0")
                entityDt.Inventory = Convert.ToInt32(hdnInventoryID.Value);
            else
                entityDt.Inventory = null;
            if (hdnInventorySubLedger.Value != "" && hdnInventorySubLedger.Value != "0")
                entityDt.InventorySubLedger= Convert.ToInt32(hdnInventorySubLedger.Value);
            else
                entityDt.InventorySubLedger = null;
            #endregion

            #region InventoryVAT
            if (hdnInventoryVATID.Value != "" && hdnInventoryVATID.Value != "0")
                entityDt.InventoryVAT = Convert.ToInt32(hdnInventoryVATID.Value);
            else
                entityDt.InventoryVAT = null;
            if (hdnInventoryVATSubLedger.Value != "" && hdnInventoryVATSubLedger.Value != "0")
                entityDt.InventoryVATSubLedger = Convert.ToInt32(hdnInventoryVATSubLedger.Value);
            else
                entityDt.InventoryVATSubLedger = null;
            #endregion

            #region COGS
            if (hdnCOGSID.Value != "" && hdnCOGSID.Value != "0")
                entityDt.COGS = Convert.ToInt32(hdnCOGSID.Value);
            else
                entityDt.COGS = null;
            if (hdnCOGSSubLedger.Value != "" && hdnCOGSSubLedger.Value != "0")
                entityDt.COGSSubLedger= Convert.ToInt32(hdnCOGSSubLedger.Value);
            else
                entityDt.COGSSubLedger = null;
            #endregion

            #region Purchase
            if (hdnPurchaseID.Value != "" && hdnPurchaseID.Value != "0")
                entityDt.Purchase = Convert.ToInt32(hdnPurchaseID.Value);
            else
                entityDt.Purchase = null;
            if (hdnPurchaseSubLedger.Value != "" && hdnPurchaseSubLedger.Value != "0")
                entityDt.PurchaseSubLedger= Convert.ToInt32(hdnPurchaseSubLedger.Value);
            else
                entityDt.PurchaseSubLedger = null;
            #endregion

            #region PurchaseReturn
            if (hdnPurchaseReturnID.Value != "" && hdnPurchaseReturnID.Value != "0")
                entityDt.PurchaseReturn = Convert.ToInt32(hdnPurchaseReturnID.Value);
            else
                entityDt.PurchaseReturn = null;
            if (hdnPurchaseReturnSubLedger.Value != "" && hdnPurchaseReturnSubLedger.Value != "0")
                entityDt.PurchaseReturnSubLedger= Convert.ToInt32(hdnPurchaseReturnSubLedger.Value);
            else
                entityDt.PurchaseReturnSubLedger = null;
            #endregion

            #region PurchaseDiscount
            if (hdnPurchaseDiscount.Value != "" && hdnPurchaseDiscount.Value != "0")
                entityDt.PurchaseDiscount = Convert.ToInt32(hdnPurchaseDiscount.Value);
            else
                entityDt.PurchaseDiscount = null;
            if (hdnPurchaseDiscountSubLedger.Value != "" && hdnPurchaseDiscountSubLedger.Value != "0")
                entityDt.PurchaseDiscountSubLedger= Convert.ToInt32(hdnPurchaseDiscountSubLedger.Value);
            else
                entityDt.PurchaseDiscountSubLedger = null;
            #endregion

            #region PurchasePriceVariant
            if (hdnPurchasePriceVariant.Value != "" && hdnPurchasePriceVariant.Value != "0")
                entityDt.PurchasePriceVariant = Convert.ToInt32(hdnPurchasePriceVariant.Value);
            else
                entityDt.PurchasePriceVariant = null;
            if (hdnPurchasePriceVariantSubLedger.Value != "" && hdnPurchasePriceVariantSubLedger.Value != "0")
                entityDt.PurchasePriceVariantSubLedger= Convert.ToInt32(hdnPurchasePriceVariantSubLedger.Value);
            else
                entityDt.PurchasePriceVariantSubLedger = null;
            #endregion

            #region Sales
            if (hdnSales.Value != "" && hdnSales.Value != "0")
                entityDt.Sales = Convert.ToInt32(hdnSales.Value);
            else
                entityDt.Sales = null;
            if (hdnSalesSubLedger.Value != "" && hdnSalesSubLedger.Value != "0")
                entityDt.SalesSubLedger= Convert.ToInt32(hdnSalesSubLedger.Value);
            else
                entityDt.SalesSubLedger = null;
            #endregion
            
            #region SalesReturn
            if (hdnSalesReturn.Value != "" && hdnSalesReturn.Value != "0")
                entityDt.SalesReturn = Convert.ToInt32(hdnSalesReturn.Value);
            else
                entityDt.SalesReturn = null;
            if (hdnSalesReturnSubLedger.Value != "" && hdnSalesReturnSubLedger.Value != "0")
                entityDt.SalesReturnSubLedger= Convert.ToInt32(hdnSalesReturnSubLedger.Value);
            else
                entityDt.SalesReturnSubLedger = null;
            #endregion
            
            #region SalesDiscount
            if (hdnSalesDiscount.Value != "" && hdnSalesDiscount.Value != "0")
                entityDt.SalesDiscount = Convert.ToInt32(hdnSalesDiscount.Value);
            else
                entityDt.SalesDiscount = null;
            if (hdnSalesDiscountSubLedger.Value != "" && hdnSalesDiscountSubLedger.Value != "0")
                entityDt.SalesDiscountSubLedger= Convert.ToInt32(hdnSalesDiscountSubLedger.Value);
            else
                entityDt.SalesDiscountSubLedger = null;
            #endregion

            #region MaterialRevenue
            if (hdnMaterialRevenue.Value != "" && hdnMaterialRevenue.Value != "0")
                entityDt.MaterialRevenue = Convert.ToInt32(hdnMaterialRevenue.Value);
            else
                entityDt.MaterialRevenue = null;
            if (hdnMaterialRevenueSubLedger.Value != "" && hdnMaterialRevenueSubLedger.Value != "0")
                entityDt.MaterialRevenueSubLedger= Convert.ToInt32(hdnMaterialRevenueSubLedger.Value);
            else
                entityDt.MaterialRevenueSubLedger = null;
            #endregion

            #region Consumption
            if (hdnConsumption.Value != "" && hdnConsumption.Value != "0")
                entityDt.Consumption = Convert.ToInt32(hdnConsumption.Value);
            else
                entityDt.Consumption = null;
            if (hdnConsumptionSubLedger.Value != "" && hdnConsumptionSubLedger.Value != "0")
                entityDt.ConsumptionSubLedger= Convert.ToInt32(hdnConsumptionSubLedger.Value);
            else
                entityDt.ConsumptionSubLedger = null;
            #endregion

            #region AdjustmentIN
            if (hdnAdjustmentIN.Value != "" && hdnAdjustmentIN.Value != "0")
                entityDt.AdjustmentIN = Convert.ToInt32(hdnAdjustmentIN.Value);
            else
                entityDt.AdjustmentIN = null;
            if (hdnAdjustmentINSubLedger.Value != "" && hdnAdjustmentINSubLedger.Value != "0")
                entityDt.AdjustmentINSubLedger= Convert.ToInt32(hdnAdjustmentINSubLedger.Value);
            else
                entityDt.AdjustmentINSubLedger = null;
            #endregion

            #region AdjustmentOUT
            if (hdnAdjustmentOUT.Value != "" && hdnAdjustmentOUT.Value != "0")
                entityDt.AdjustmentOUT = Convert.ToInt32(hdnAdjustmentOUT.Value);
            else
                entityDt.AdjustmentOUT = null;
            if (hdnAdjustmentOUTSubLedger.Value != "" && hdnAdjustmentOUTSubLedger.Value != "0")
                entityDt.AdjustmentOUTSubLedger= Convert.ToInt32(hdnAdjustmentOUTSubLedger.Value);
            else
                entityDt.AdjustmentOUTSubLedger = null;
            #endregion
            #endregion
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            ProductLineDao productLineDao = new ProductLineDao(ctx);
            ProductLineDtDao productLineDtDao = new ProductLineDtDao(ctx);
            bool result = true;
            try
            {
                ProductLine entity = new ProductLine();
                ProductLineDt entityDt = new ProductLineDt();
                ControlToEntity(entity, entityDt);

                entityDt.SiteID = AppSession.UserLogin.SiteID;
                entity.GCItemType = hdnGCItemType.Value;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                productLineDao.Insert(entity);
                entityDt.ProductLineID = BusinessLayer.GetProductLineMaxID(ctx);
                productLineDtDao.Insert(entityDt);

                retval = entityDt.ProductLineID.ToString();
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
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

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            ProductLineDao productLineDao = new ProductLineDao(ctx);
            ProductLineDtDao productLineDtDao = new ProductLineDtDao(ctx);
            bool result = true;
            try
            {
                ProductLine entity = productLineDao.Get(Convert.ToInt32(hdnID.Value));
                ProductLineDt entityDt = productLineDtDao.Get(entity.ProductLineID, AppSession.UserLogin.SiteID);
                ControlToEntity(entity, entityDt);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;

                productLineDao.Update(entity);
                productLineDtDao.Update(entityDt);
                
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                ctx.RollBackTransaction();
                result = false;
            }
            finally 
            {
                ctx.Close();
            }
            return result;
        }
    }
}