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
using System.Text;
using CodeX.Common;

namespace Codex.Muses.Web.Accounting.Program
{
    public partial class FAItemEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            if (hdnPurchaseReceiveDtID.Value == "")
                return Constant.MenuCode.AssetManagement.FA_ITEM;
            return Constant.MenuCode.AssetManagement.FA_ITEM_FROM_PURCHASE_RECEIVE;
        }

        protected string OnGetFilterExpressionSupplier()
        {
            return string.Format("GCBusinessPartnerType = '{0}' AND IsDeleted = 0", Constant.BusinessObjectType.SUPPLIER);
        }

        protected string OnGetFilterExpressionItem()
        {
            return string.Format("IsFixedAsset = 1 AND IsDeleted = 0");
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                string[] param = Request.QueryString["id"].Split('|');
                if (param[0] == "pr")
                {
                    hdnPurchaseReceiveDtID.Value = param[1];
                    SetControlProperties();
                    IsAdd = true;
                    vPurchaseReceiveDt entity = BusinessLayer.GetvPurchaseReceiveDtList(string.Format("ID = {0}", hdnPurchaseReceiveDtID.Value))[0];
                    EntityToControl(entity);
                    trBusinessPartnerNonMaster.Attributes.Add("style", "display:none");
                }
                else
                {
                    IsAdd = false;
                    String ID = param[0];
                    hdnID.Value = ID;
                    SetControlProperties();
                    vFAItem entity = BusinessLayer.GetvFAItemList(string.Format("FixedAssetID = {0}", ID))[0];
                    vFAItemCOA entityCOA = BusinessLayer.GetvFAItemCOAList(string.Format("FixedAssetID = {0} AND SiteID = '{1}'", ID, AppSession.UserLogin.SiteID))[0];
                    EntityToControl(entity, entityCOA);
                }
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
                trBusinessPartnerNonMaster.Attributes.Add("style", "display:none");
            }
            txtFixedAssetCode.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.ITEM_UNIT));
            lstStandardCode.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });
            Methods.SetComboBoxField<StandardCode>(cboGCProcurementUnit, lstStandardCode, "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            #region Data Aktiva Tetap
            SetControlEntrySetting(txtFixedAssetCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtFixedAssetName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(hdnItemID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtItemCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtItemName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtSerialNumber, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(hdnFAGroupID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtFAGroupCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtFAGroupName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(hdnFALocationID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtFALocationCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtFALocationName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(chkIsBusinessPartnerFromMaster, new ControlEntrySetting(true, true, false, true));
            SetControlEntrySetting(txtBusinessPartnerNameNonMaster, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(hdnBusinessPartnerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtBusinessPartnerCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtBusinessPartnerName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtContractNumber, new ControlEntrySetting(true, true, false));
            #endregion
            
            #region Data Perolehan Aktiva Tetap
            SetControlEntrySetting(hdnPurchaseReceiveID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtProcurementNumber, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtProcurementDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtProcurementAmount, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(txtProcurementQuantity, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(cboGCProcurementUnit, new ControlEntrySetting(true, true, false));
            #endregion

            #region Data Perhitungan Penyusutan Aktiva Tetap
            SetControlEntrySetting(hdnFADepreciationMethodID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtFADepreciationMethodCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtFADepreciationMethodName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtDepreciationStartDate, new ControlEntrySetting(true, true, true, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtDepreciationStartLength, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(txtAssetFinalValue, new ControlEntrySetting(true, true, true, "0"));
            #endregion

            #region Pengaturan Perkiraan untuk Aktiva Tetap
            SetControlEntrySetting(hdnGLAccount1ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSearchDialogTypeName1, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSubLedgerID1, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtGLAccount1Code, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtGLAccount1Name, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblSubLedgerDt1, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnSubLedgerDt1ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSubLedgerDt1Code, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtSubLedgerDt1Name, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnGLAccount2ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSearchDialogTypeName2, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSubLedgerID2, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtGLAccount2Code, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtGLAccount2Name, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblSubLedgerDt2, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnSubLedgerDt2ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSubLedgerDt2Code, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtSubLedgerDt2Name, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnGLAccount3ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSearchDialogTypeName3, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSubLedgerID3, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtGLAccount3Code, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtGLAccount3Name, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblSubLedgerDt3, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnSubLedgerDt3ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSubLedgerDt3Code, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtSubLedgerDt3Name, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnGLAccount4ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSearchDialogTypeName4, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSubLedgerID4, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtGLAccount4Code, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtGLAccount4Name, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblSubLedgerDt4, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnSubLedgerDt4ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSubLedgerDt4Code, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtSubLedgerDt4Name, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnGLAccount5ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSearchDialogTypeName5, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSubLedgerID5, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtGLAccount5Code, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtGLAccount5Name, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblSubLedgerDt5, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnSubLedgerDt5ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSubLedgerDt5Code, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtSubLedgerDt5Name, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnGLAccount6ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSearchDialogTypeName6, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSubLedgerID6, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtGLAccount6Code, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtGLAccount6Name, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblSubLedgerDt6, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnSubLedgerDt6ID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSubLedgerDt6Code, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtSubLedgerDt6Name, new ControlEntrySetting(false, false, false));
            #endregion
        }

        private void EntityToControl(vPurchaseReceiveDt entity)
        {
            hdnPurchaseReceiveID.Value = entity.PurchaseReceiveID.ToString();
            txtProcurementNumber.Text = entity.PurchaseReceiveNo;
            txtProcurementDate.Text = entity.ReceivedDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtProcurementAmount.Text = entity.UnitPrice.ToString();
            txtProcurementQuantity.Text = entity.Quantity.ToString();
            cboGCProcurementUnit.Value = entity.GCItemUnit;

            hdnItemID.Value = entity.ItemID.ToString();
            txtItemCode.Text = entity.ItemCode;
            txtItemName.Text = entity.ItemName1;
            hdnBusinessPartnerID.Value = entity.SupplierID.ToString();
            txtBusinessPartnerCode.Text = entity.SupplierCode;
            txtBusinessPartnerName.Text = entity.SupplierName;
        }

        private void EntityToControl(vFAItem entity, vFAItemCOA entityCOA)
        {
            #region Data Aktiva Tetap
            txtFixedAssetCode.Text = entity.FixedAssetCode;
            txtFixedAssetName.Text = entity.FixedAssetName;
            hdnItemID.Value = entity.ItemID.ToString();
            txtItemCode.Text = entity.ItemCode;
            txtItemName.Text = entity.ItemName1;
            txtSerialNumber.Text = entity.SerialNumber;
            hdnFAGroupID.Value = entity.FAGroupID.ToString();
            txtFAGroupCode.Text = entity.FAGroupCode;
            txtFAGroupName.Text = entity.FAGroupName;
            hdnFALocationID.Value = entity.FALocationID.ToString();
            txtFALocationCode.Text = entity.FALocationCode;
            txtFALocationName.Text = entity.FALocationName;
            txtRemarks.Text = entity.Remarks;
            
            if (entity.BusinessPartnerID > 0)
            {
                chkIsBusinessPartnerFromMaster.Checked = true;
                hdnBusinessPartnerID.Value = entity.BusinessPartnerID.ToString();
                txtBusinessPartnerCode.Text = entity.BusinessPartnerCode;
                txtBusinessPartnerName.Text = entity.BusinessPartnerName;
                trBusinessPartnerNonMaster.Attributes.Add("style", "display:none");
            }
            else if (entity.BusinessPartnerName != "")
            {
                chkIsBusinessPartnerFromMaster.Checked = false;
                txtBusinessPartnerNameNonMaster.Text = entity.BusinessPartnerName;
                trBusinessPartner.Attributes.Add("style", "display:none");
            }
            else
            {
                chkIsBusinessPartnerFromMaster.Checked = true;
                trBusinessPartnerNonMaster.Attributes.Add("style", "display:none");
            }
            txtContractNumber.Text = entity.ContractNumber;
            #endregion

            #region Data Perolehan Aktiva Tetap
            hdnPurchaseReceiveID.Value = entity.PurchaseReceiveID.ToString();
            txtProcurementNumber.Text = entity.ProcurementNumber;
            txtProcurementDate.Text = entity.ProcurementDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtProcurementAmount.Text = entity.ProcurementAmount.ToString();
            txtProcurementQuantity.Text = entity.ProcurementQuantity.ToString();
            cboGCProcurementUnit.Value = entity.GCProcurementUnit;
            #endregion

            #region Data Perhitungan Penyusutan Aktiva Tetap
            hdnFADepreciationMethodID.Value = entity.MethodID.ToString();
            txtFADepreciationMethodCode.Text = entity.MethodCode;
            txtFADepreciationMethodName.Text = entity.MethodName;
            txtDepreciationStartDate.Text = entity.DepreciationStartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtDepreciationStartLength.Text = entity.DepreciationLength.ToString();
            txtAssetFinalValue.Text = entity.AssetFinalValue.ToString();
            #endregion

            #region Pengaturan Perkiraan untuk Aktiva Tetap
            #region GL Account 1
            hdnGLAccount1ID.Value = entityCOA.GLAccount1.ToString();
            txtGLAccount1Code.Text = entityCOA.GLAccount1No;
            txtGLAccount1Name.Text = entityCOA.GLAccount1Name;

            hdnSubLedgerID1.Value = entityCOA.SubLedgerID1.ToString();
            hdnSearchDialogTypeName1.Value = entityCOA.SearchDialogTypeName1;
            hdnIDFieldName1.Value = entityCOA.IDFieldName1;
            hdnCodeFieldName1.Value = entityCOA.CodeFieldName1;
            hdnDisplayFieldName1.Value = entityCOA.DisplayFieldName1;
            hdnMethodName1.Value = entityCOA.MethodName1;
            hdnFilterExpression1.Value = entityCOA.FilterExpression1;

            hdnSubLedgerDt1ID.Value = entityCOA.SubLedger1.ToString();
            txtSubLedgerDt1Code.Text = entityCOA.SubLedger1Code;
            txtSubLedgerDt1Name.Text = entityCOA.SubLedger1Name;
            #endregion

            #region GL Account 2
            hdnGLAccount2ID.Value = entityCOA.GLAccount2.ToString();
            txtGLAccount2Code.Text = entityCOA.GLAccount2No;
            txtGLAccount2Name.Text = entityCOA.GLAccount2Name;

            hdnSubLedgerID2.Value = entityCOA.SubLedgerID2.ToString();
            hdnSearchDialogTypeName2.Value = entityCOA.SearchDialogTypeName2;
            hdnIDFieldName2.Value = entityCOA.IDFieldName2;
            hdnCodeFieldName2.Value = entityCOA.CodeFieldName2;
            hdnDisplayFieldName2.Value = entityCOA.DisplayFieldName2;
            hdnMethodName2.Value = entityCOA.MethodName2;
            hdnFilterExpression2.Value = entityCOA.FilterExpression2;

            hdnSubLedgerDt2ID.Value = entityCOA.SubLedger2.ToString();
            txtSubLedgerDt2Code.Text = entityCOA.SubLedger2Code;
            txtSubLedgerDt2Name.Text = entityCOA.SubLedger2Name;
            #endregion

            #region GL Account 3
            hdnGLAccount3ID.Value = entityCOA.GLAccount3.ToString();
            txtGLAccount3Code.Text = entityCOA.GLAccount3No;
            txtGLAccount3Name.Text = entityCOA.GLAccount3Name;

            hdnSubLedgerID3.Value = entityCOA.SubLedgerID3.ToString();
            hdnSearchDialogTypeName3.Value = entityCOA.SearchDialogTypeName3;
            hdnIDFieldName3.Value = entityCOA.IDFieldName3;
            hdnCodeFieldName3.Value = entityCOA.CodeFieldName3;
            hdnDisplayFieldName3.Value = entityCOA.DisplayFieldName3;
            hdnMethodName3.Value = entityCOA.MethodName3;
            hdnFilterExpression3.Value = entityCOA.FilterExpression3;

            hdnSubLedgerDt3ID.Value = entityCOA.SubLedger3.ToString();
            txtSubLedgerDt3Code.Text = entityCOA.SubLedger3Code;
            txtSubLedgerDt3Name.Text = entityCOA.SubLedger3Name;
            #endregion

            #region GL Account 4
            hdnGLAccount4ID.Value = entityCOA.GLAccount4.ToString();
            txtGLAccount4Code.Text = entityCOA.GLAccount4No;
            txtGLAccount4Name.Text = entityCOA.GLAccount4Name;

            hdnSubLedgerID4.Value = entityCOA.SubLedgerID4.ToString();
            hdnSearchDialogTypeName4.Value = entityCOA.SearchDialogTypeName4;
            hdnIDFieldName4.Value = entityCOA.IDFieldName4;
            hdnCodeFieldName4.Value = entityCOA.CodeFieldName4;
            hdnDisplayFieldName4.Value = entityCOA.DisplayFieldName4;
            hdnMethodName4.Value = entityCOA.MethodName4;
            hdnFilterExpression4.Value = entityCOA.FilterExpression4;

            hdnSubLedgerDt4ID.Value = entityCOA.SubLedger4.ToString();
            txtSubLedgerDt4Code.Text = entityCOA.SubLedger4Code;
            txtSubLedgerDt4Name.Text = entityCOA.SubLedger4Name;
            #endregion

            #region GL Account 5
            hdnGLAccount5ID.Value = entityCOA.GLAccount5.ToString();
            txtGLAccount5Code.Text = entityCOA.GLAccount5No;
            txtGLAccount5Name.Text = entityCOA.GLAccount5Name;

            hdnSubLedgerID5.Value = entityCOA.SubLedgerID5.ToString();
            hdnSearchDialogTypeName5.Value = entityCOA.SearchDialogTypeName5;
            hdnIDFieldName5.Value = entityCOA.IDFieldName5;
            hdnCodeFieldName5.Value = entityCOA.CodeFieldName5;
            hdnDisplayFieldName5.Value = entityCOA.DisplayFieldName5;
            hdnMethodName5.Value = entityCOA.MethodName5;
            hdnFilterExpression5.Value = entityCOA.FilterExpression5;

            hdnSubLedgerDt5ID.Value = entityCOA.SubLedger5.ToString();
            txtSubLedgerDt5Code.Text = entityCOA.SubLedger5Code;
            txtSubLedgerDt5Name.Text = entityCOA.SubLedger5Name;
            #endregion

            #region GL Account 6
            hdnGLAccount6ID.Value = entityCOA.GLAccount6.ToString();
            txtGLAccount6Code.Text = entityCOA.GLAccount6No;
            txtGLAccount6Name.Text = entityCOA.GLAccount6Name;

            hdnSubLedgerID6.Value = entityCOA.SubLedgerID6.ToString();
            hdnSearchDialogTypeName6.Value = entityCOA.SearchDialogTypeName6;
            hdnIDFieldName6.Value = entityCOA.IDFieldName6;
            hdnCodeFieldName6.Value = entityCOA.CodeFieldName6;
            hdnDisplayFieldName6.Value = entityCOA.DisplayFieldName6;
            hdnMethodName6.Value = entityCOA.MethodName6;
            hdnFilterExpression6.Value = entityCOA.FilterExpression6;

            hdnSubLedgerDt6ID.Value = entityCOA.SubLedger6.ToString();
            txtSubLedgerDt6Code.Text = entityCOA.SubLedger6Code;
            txtSubLedgerDt6Name.Text = entityCOA.SubLedger6Name;
            #endregion
            #endregion
        }

        private void ControlToEntity(FAItem entity, FAItemCOA entityCOA)
        {
            #region Data Aktiva Tetap
            entity.FixedAssetName = txtFixedAssetName.Text;
            if (hdnItemID.Value != "" && hdnItemID.Value != "0")
                entity.ItemID = Convert.ToInt32(hdnItemID.Value);
            else
                entity.ItemID = null;
            entity.SerialNumber = txtSerialNumber.Text;
            entity.FAGroupID = Convert.ToInt32(hdnFAGroupID.Value);
            entity.FALocationID = Convert.ToInt32(hdnFALocationID.Value);
            entity.Remarks = txtRemarks.Text;
            if (chkIsBusinessPartnerFromMaster.Checked)
            {
                if (hdnBusinessPartnerID.Value != "" && hdnBusinessPartnerID.Value != "0")
                    entity.BusinessPartnerID = Convert.ToInt32(hdnBusinessPartnerID.Value);
                else
                    entity.BusinessPartnerID = null;
                entity.BusinessPartnerName = "";
            }
            else
            {
                entity.BusinessPartnerID = null;
                entity.BusinessPartnerName = txtBusinessPartnerNameNonMaster.Text;
            }
            entity.ContractNumber = txtContractNumber.Text;
            #endregion

            #region Data Perolehan Aktiva Tetap
            if (hdnPurchaseReceiveID.Value == "0" || hdnPurchaseReceiveID.Value == "")
            {
                entity.ProcurementNumber = txtProcurementNumber.Text;
                entity.ProcurementDate = Helper.GetDatePickerValue(txtProcurementDate.Text);
                entity.ProcurementAmount = Convert.ToDecimal(txtProcurementAmount.Text);
                entity.ProcurementQuantity = Convert.ToDecimal(txtProcurementQuantity.Text);
                if (cboGCProcurementUnit.Value != null && cboGCProcurementUnit.Value.ToString() != "")
                    entity.GCProcurementUnit = cboGCProcurementUnit.Value.ToString();
                else
                    entity.GCProcurementUnit = null;
                entity.PurchaseReceiveID = null;
            }
            else
                entity.PurchaseReceiveID = Convert.ToInt32(hdnPurchaseReceiveID.Value);
            #endregion

            #region Data Perhitungan Penyusutan Aktiva Tetap
            entity.MethodID = Convert.ToInt32(hdnFADepreciationMethodID.Value);
            entity.DepreciationStartDate = Helper.GetDatePickerValue(txtDepreciationStartDate.Text);
            entity.DepreciationLength = Convert.ToInt16(txtDepreciationStartLength.Text);
            entity.AssetFinalValue = Convert.ToDecimal(txtAssetFinalValue.Text);
            #endregion

            #region Pengaturan Perkiraan untuk Aktiva Tetap
            #region GL Account 1
            if (hdnGLAccount1ID.Value != "" && hdnGLAccount1ID.Value != "0")
                entityCOA.GLAccount1 = Convert.ToInt32(hdnGLAccount1ID.Value);
            else
                entityCOA.GLAccount1 = null;
            if (hdnSubLedgerDt1ID.Value != "" && hdnSubLedgerDt1ID.Value != "0")
                entityCOA.SubLedger1 = Convert.ToInt32(hdnSubLedgerDt1ID.Value);
            else
                entityCOA.SubLedger1 = null;
            #endregion

            #region GL Account 2
            if (hdnGLAccount2ID.Value != "" && hdnGLAccount2ID.Value != "0")
                entityCOA.GLAccount2 = Convert.ToInt32(hdnGLAccount2ID.Value);
            else
                entityCOA.GLAccount2 = null;
            if (hdnSubLedgerDt2ID.Value != "" && hdnSubLedgerDt2ID.Value != "0")
                entityCOA.SubLedger2 = Convert.ToInt32(hdnSubLedgerDt2ID.Value);
            else
                entityCOA.SubLedger2 = null;
            #endregion

            #region GL Account 3
            if (hdnGLAccount3ID.Value != "" && hdnGLAccount3ID.Value != "0")
                entityCOA.GLAccount3 = Convert.ToInt32(hdnGLAccount3ID.Value);
            else
                entityCOA.GLAccount3 = null;
            if (hdnSubLedgerDt3ID.Value != "" && hdnSubLedgerDt3ID.Value != "0")
                entityCOA.SubLedger3 = Convert.ToInt32(hdnSubLedgerDt3ID.Value);
            else
                entityCOA.SubLedger3 = null;
            #endregion

            #region GL Account 4
            if (hdnGLAccount4ID.Value != "" && hdnGLAccount4ID.Value != "0")
                entityCOA.GLAccount4 = Convert.ToInt32(hdnGLAccount4ID.Value);
            else
                entityCOA.GLAccount4 = null;
            if (hdnSubLedgerDt4ID.Value != "" && hdnSubLedgerDt4ID.Value != "0")
                entityCOA.SubLedger4 = Convert.ToInt32(hdnSubLedgerDt4ID.Value);
            else
                entityCOA.SubLedger4 = null;
            #endregion

            #region GL Account 5
            if (hdnGLAccount5ID.Value != "" && hdnGLAccount5ID.Value != "0")
                entityCOA.GLAccount5 = Convert.ToInt32(hdnGLAccount5ID.Value);
            else
                entityCOA.GLAccount5 = null;
            if (hdnSubLedgerDt5ID.Value != "" && hdnSubLedgerDt5ID.Value != "0")
                entityCOA.SubLedger5 = Convert.ToInt32(hdnSubLedgerDt5ID.Value);
            else
                entityCOA.SubLedger5 = null;
            #endregion

            #region GL Account 6
            if (hdnGLAccount6ID.Value != "" && hdnGLAccount6ID.Value != "0")
                entityCOA.GLAccount6 = Convert.ToInt32(hdnGLAccount6ID.Value);
            else
                entityCOA.GLAccount6 = null;
            if (hdnSubLedgerDt6ID.Value != "" && hdnSubLedgerDt6ID.Value != "0")
                entityCOA.SubLedger6 = Convert.ToInt32(hdnSubLedgerDt6ID.Value);
            else
                entityCOA.SubLedger6 = null;
            #endregion
            #endregion
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            FAItemDao entityDao = new FAItemDao(ctx);
            FAItemCOADao entityCOADao = new FAItemCOADao(ctx);
            bool result = false;
            try
            {
                FAItem entity = new FAItem();
                FAItemCOA entityCOA = new FAItemCOA();
                ControlToEntity(entity, entityCOA);
                entity.SiteID = entityCOA.SiteID = AppSession.UserLogin.SiteID;
                entity.FixedAssetCode = GenerateFixedAssetCode(ctx, entity);
                entity.GCItemStatus = Constant.ItemStatus.ACTIVE;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);

                entityCOA.CreatedBy = AppSession.UserLogin.UserID;
                entityCOA.FixedAssetID = BusinessLayer.GetFAItemMaxID(ctx);
                entityCOADao.Insert(entityCOA);
                retval = entityCOA.FixedAssetID.ToString();
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
            IDbContext ctx = DbFactory.Configure(true);
            FAItemDao entityDao = new FAItemDao(ctx);
            FAItemCOADao entityCOADao = new FAItemCOADao(ctx);
            bool result = false;
            try
            {
                FAItem entity = entityDao.Get(Convert.ToInt32(hdnID.Value));;
                FAItemCOA entityCOA = entityCOADao.Get(AppSession.UserLogin.SiteID, entity.FixedAssetID);
                ControlToEntity(entity, entityCOA);
                entity.LastUpdatedBy = entityCOA.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);
                entityCOADao.Update(entityCOA);
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

        private String GenerateFixedAssetCode(IDbContext ctx, FAItem entity)
        {
            SiteDao siteDao = new SiteDao(ctx);
            StringBuilder result = new StringBuilder();

            DateTime procurementDate = Helper.GetDatePickerValue(Request.Form[txtProcurementDate.UniqueID]);
            string initial = siteDao.Get(entity.SiteID).Initial;
            result.Append(initial).Append('/').Append(txtFAGroupCode.Text).Append('/').Append(procurementDate.ToString("yyMM")).Append('/');

            FAItem fai = BusinessLayer.GetFAItemList(string.Format("FixedAssetCode LIKE '{0}%'", result.ToString()), 1, 1, "FixedAssetCode DESC", ctx).FirstOrDefault();
            int newNumber = 1;
            if (fai != null)
                newNumber = Convert.ToInt32(fai.FixedAssetCode.Substring(result.ToString().Length)) + 1;
            return string.Format("{0}{1}", result.ToString(), newNumber.ToString().PadLeft(6, '0'));
        }
    }
}