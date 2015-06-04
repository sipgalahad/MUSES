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
            String filterExpression = String.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.ITEM_TYPE);
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(filterExpression);
            Methods.SetComboBoxField(cboGCItemType, lstStandardCode, "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnProductLineID, new ControlEntrySetting(true, false));
            SetControlEntrySetting(txtProductLineCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtProductLineName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(cboGCItemType, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtNotes, new ControlEntrySetting(true, true, false));

            #region Pengaturan Perkiraan untuk Aktiva Tetap
            SetControlEntrySetting(hdnGLAccount1ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSearchDialogTypeName1, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSubLedgerID1, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtGLAccount1Code, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtGLAccount1Name, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblSubLedgerDt1, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnSubLedgerDt1ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSubLedgerDt1Code, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtSubLedgerDt1Name, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnGLAccount2ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSearchDialogTypeName2, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSubLedgerID2, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtGLAccount2Code, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtGLAccount2Name, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblSubLedgerDt2, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnSubLedgerDt2ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSubLedgerDt2Code, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtSubLedgerDt2Name, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnGLAccount3ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSearchDialogTypeName3, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSubLedgerID3, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtGLAccount3Code, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtGLAccount3Name, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblSubLedgerDt3, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnSubLedgerDt3ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSubLedgerDt3Code, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtSubLedgerDt3Name, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnGLAccount4ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSearchDialogTypeName4, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSubLedgerID4, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtGLAccount4Code, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtGLAccount4Name, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblSubLedgerDt4, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnSubLedgerDt4ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSubLedgerDt4Code, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtSubLedgerDt4Name, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnGLAccount5ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSearchDialogTypeName5, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSubLedgerID5, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtGLAccount5Code, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtGLAccount5Name, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblSubLedgerDt5, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnSubLedgerDt5ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSubLedgerDt5Code, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtSubLedgerDt5Name, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnPurchasePriceVariantID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnPurchasePriceVariantSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnPurchasePriceVariantSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtPurchasePriceVariantGLAccountNo, new ControlEntrySetting(true, true, false));
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
            #region GL Account 1
            hdnGLAccount1ID.Value = entity.Inventory.ToString();
            txtGLAccount1Code.Text = entity.InventoryGLAccountNo;
            txtGLAccount1Name.Text = entity.InventoryGLAccountName;

            hdnSubLedgerID1.Value = entity.InventorySubLedgerID.ToString();
            hdnSearchDialogTypeName1.Value = entity.InventorySearchDialogTypeName;
            hdnIDFieldName1.Value = entity.InventoryIDFieldName;
            hdnCodeFieldName1.Value = entity.InventoryCodeFieldName;
            hdnDisplayFieldName1.Value = entity.InventoryDisplayFieldName;
            hdnMethodName1.Value = entity.InventoryMethodName;
            hdnFilterExpression1.Value = entity.InventoryFilterExpression;

            hdnSubLedgerDt1ID.Value = entity.InventorySubLedger.ToString();
            txtSubLedgerDt1Code.Text = entity.InventorySubLedgerCode;
            txtSubLedgerDt1Name.Text = entity.InventorySubLedgerName;
            #endregion

            #region GL Account 2
            hdnGLAccount2ID.Value = entity.COGS.ToString();
            txtGLAccount2Code.Text = entity.COGSGLAccountNo;
            txtGLAccount2Name.Text = entity.COGSGLAccountName;

            hdnSubLedgerID2.Value = entity.COGSSubLedgerID.ToString();
            hdnSearchDialogTypeName2.Value = entity.COGSSearchDialogTypeName;
            hdnIDFieldName2.Value = entity.COGSIDFieldName;
            hdnCodeFieldName2.Value = entity.COGSCodeFieldName;
            hdnDisplayFieldName2.Value = entity.COGSDisplayFieldName;
            hdnMethodName2.Value = entity.COGSMethodName;
            hdnFilterExpression2.Value = entity.COGSFilterExpression;

            hdnSubLedgerDt2ID.Value = entity.COGSSubLedger.ToString();
            txtSubLedgerDt2Code.Text = entity.COGSSubLedgerCode;
            txtSubLedgerDt2Name.Text = entity.COGSSubLedgerName;
            #endregion

            #region GL Account 3
            hdnGLAccount3ID.Value = entity.Consumption.ToString();
            txtGLAccount3Code.Text = entity.ConsumptionGLAccountNo;
            txtGLAccount3Name.Text = entity.ConsumptionGLAccountName;

            hdnSubLedgerID3.Value = entity.ConsumptionSubLedgerID.ToString();
            hdnSearchDialogTypeName3.Value = entity.ConsumptionSearchDialogTypeName;
            hdnIDFieldName3.Value = entity.ConsumptionIDFieldName;
            hdnCodeFieldName3.Value = entity.ConsumptionCodeFieldName;
            hdnDisplayFieldName3.Value = entity.ConsumptionDisplayFieldName;
            hdnMethodName3.Value = entity.ConsumptionMethodName;
            hdnFilterExpression3.Value = entity.ConsumptionFilterExpression;

            hdnSubLedgerDt3ID.Value = entity.ConsumptionSubLedger.ToString();
            txtSubLedgerDt3Code.Text = entity.ConsumptionSubLedgerCode;
            txtSubLedgerDt3Name.Text = entity.ConsumptionSubLedgerName;
            #endregion

            #region GL Account 4
            hdnGLAccount4ID.Value = entity.InventoryVAT.ToString();
            txtGLAccount4Code.Text = entity.InventoryVATGLAccountNo;
            txtGLAccount4Name.Text = entity.InventoryVATGLAccountName;

            hdnSubLedgerID4.Value = entity.InventoryVATSubLedgerID.ToString();
            hdnSearchDialogTypeName4.Value = entity.InventoryVATSearchDialogTypeName;
            hdnIDFieldName4.Value = entity.InventoryVATIDFieldName;
            hdnCodeFieldName4.Value = entity.InventoryVATCodeFieldName;
            hdnDisplayFieldName4.Value = entity.InventoryVATDisplayFieldName;
            hdnMethodName4.Value = entity.InventoryVATMethodName;
            hdnFilterExpression4.Value = entity.InventoryVATFilterExpression;

            hdnSubLedgerDt4ID.Value = entity.InventoryVATSubLedger.ToString();
            txtSubLedgerDt4Code.Text = entity.InventoryVATSubLedgerCode;
            txtSubLedgerDt4Name.Text = entity.InventoryVATSubLedgerName;
            #endregion

            #region GL Account 5
            hdnGLAccount5ID.Value = entity.InventoryDiscount.ToString();
            txtGLAccount5Code.Text = entity.InventoryDiscountGLAccountNo;
            txtGLAccount5Name.Text = entity.InventoryDiscountGLAccountName;

            hdnSubLedgerID5.Value = entity.InventoryDiscountSubLedgerID.ToString();
            hdnSearchDialogTypeName5.Value = entity.InventoryDiscountSearchDialogTypeName;
            hdnIDFieldName5.Value = entity.InventoryDiscountIDFieldName;
            hdnCodeFieldName5.Value = entity.InventoryDiscountCodeFieldName;
            hdnDisplayFieldName5.Value = entity.InventoryDiscountDisplayFieldName;
            hdnMethodName5.Value = entity.InventoryDiscountMethodName;
            hdnFilterExpression5.Value = entity.InventoryDiscountFilterExpression;

            hdnSubLedgerDt5ID.Value = entity.InventoryDiscountSubLedger.ToString();
            txtSubLedgerDt5Code.Text = entity.InventoryDiscountSubLedgerCode;
            txtSubLedgerDt5Name.Text = entity.InventoryDiscountSubLedgerName;
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
            #region GL Account 1
            entity.Inventory = Convert.ToInt32(hdnGLAccount1ID.Value);
            if (hdnSubLedgerDt1ID.Value != "" && hdnSubLedgerDt1ID.Value != "0")
                entity.InventorySubLedger = Convert.ToInt32(hdnSubLedgerDt1ID.Value);
            else
                entity.InventorySubLedger = null;
            #endregion

            #region GL Account 2
            entity.COGS = Convert.ToInt32(hdnGLAccount2ID.Value);
            if (hdnSubLedgerDt2ID.Value != "" && hdnSubLedgerDt2ID.Value != "0")
                entity.COGSSubLedger = Convert.ToInt32(hdnSubLedgerDt2ID.Value);
            else
                entity.COGSSubLedger = null;
            #endregion

            #region GL Account 3
            entity.Consumption = Convert.ToInt32(hdnGLAccount3ID.Value);
            if (hdnSubLedgerDt3ID.Value != "" && hdnSubLedgerDt3ID.Value != "0")
                entity.ConsumptionSubLedger = Convert.ToInt32(hdnSubLedgerDt3ID.Value);
            else
                entity.ConsumptionSubLedger = null;
            #endregion

            #region GL Account 4
            entity.InventoryVAT = Convert.ToInt32(hdnGLAccount4ID.Value);
            if (hdnSubLedgerDt4ID.Value != "" && hdnSubLedgerDt4ID.Value != "0")
                entity.InventoryVATSubLedger = Convert.ToInt32(hdnSubLedgerDt4ID.Value);
            else
                entity.InventoryVATSubLedger = null;
            #endregion

            #region GL Account 5
            entity.InventoryDiscount = Convert.ToInt32(hdnGLAccount5ID.Value);
            if (hdnSubLedgerDt5ID.Value != "" && hdnSubLedgerDt5ID.Value != "0")
                entity.InventoryDiscountSubLedger = Convert.ToInt32(hdnSubLedgerDt5ID.Value);
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