using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Common;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common;
using System.Data;
using CodeX.Data.Core.Dal;
using CodeX.Data.Model;
using CodeX.Muses.Web.Accounting.Program;

namespace CodeX.Web.Accounting.Program
{
    public partial class GLSupplierLineDtEntryCtl : BaseEntryPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            IsAdd = true;
            string[] temp = param.Split('|');
            hdnSupplierLineID.Value = temp[0];
            hdnGCPurchaseType.Value = temp[1];

            vSupplierLineDt supplierLineDt = BusinessLayer.GetvSupplierLineDtList(string.Format("SupplierLineID = {0} AND SiteID = '{1}' AND GCPurchaseType = '{2}'", hdnSupplierLineID.Value, AppSession.UserLogin.SiteID, hdnGCPurchaseType.Value)).FirstOrDefault();
            if (supplierLineDt != null)
            {
                IsAdd = false;
                EntityToControl(supplierLineDt);
            }
            else
                IsAdd = true;

            SupplierLine supplierLine = BusinessLayer.GetSupplierLine(Convert.ToInt32(hdnSupplierLineID.Value));
            txtSupplierLineCode.Text = supplierLine.SupplierLineCode;
            txtSupplierLineName.Text = supplierLine.SupplierLineName;
            txtPurchaseType.Text = BusinessLayer.GetStandardCode(hdnGCPurchaseType.Value).StandardCodeName;
            //txtTemplateCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            #region Pengaturan Perkiraan
            SetControlEntrySetting(hdnAPID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAPSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAPSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAPGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtAPGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblAPSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnAPSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAPSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtAPSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnAPInProcessID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAPInProcessSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAPInProcessSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAPInProcessGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtAPInProcessGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblAPInProcessSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnAPInProcessSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAPInProcessSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtAPInProcessSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnAPDiscountID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAPDiscountSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAPDiscountSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAPDiscountGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtAPDiscountGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblAPDiscountSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnAPDiscountSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAPDiscountSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtAPDiscountSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnAPStampID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAPStampSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAPStampSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAPStampGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtAPStampGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblAPStampSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnAPStampSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAPStampSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtAPStampSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnAPDownPaymentID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAPDownPaymentSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAPDownPaymentSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAPDownPaymentGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtAPDownPaymentGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblAPDownPaymentSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnAPDownPaymentSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAPDownPaymentSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtAPDownPaymentSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnAPChargeID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAPChargeSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAPChargeSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAPChargeGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtAPChargeGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblAPChargeSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnAPChargeSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAPChargeSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtAPChargeSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnARPurchaseReturnID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnARPurchaseReturnSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnARPurchaseReturnSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtARPurchaseReturnGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtARPurchaseReturnGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblARPurchaseReturnSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnARPurchaseReturnSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtARPurchaseReturnSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtARPurchaseReturnSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnARCreditNoteID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnARCreditNoteSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnARCreditNoteSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtARCreditNoteGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtARCreditNoteGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblARCreditNoteSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnARCreditNoteSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtARCreditNoteSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtARCreditNoteSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnAPVarianceID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAPVarianceSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnAPVarianceSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAPVarianceGLAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtAPVarianceGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblAPVarianceSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnAPVarianceSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtAPVarianceSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtAPVarianceSubLedgerName, new ControlEntrySetting(false, false, false));
            #endregion
        }

        private void EntityToControl(vSupplierLineDt entityDt)
        {
            #region Pengaturan Perkiraan
            #region AP
            hdnAPID.Value = entityDt.AP.ToString();
            txtAPGLAccountNo.Text = entityDt.APGLAccountNo;
            txtAPGLAccountName.Text = entityDt.APGLAccountName;
            hdnAPSubLedgerID.Value = entityDt.APSubLedgerID.ToString();
            hdnAPSearchDialogTypeName.Value = entityDt.APSearchDialogTypeName;
            hdnAPIDFieldName.Value = entityDt.APIDFieldName;
            hdnAPCodeFieldName.Value = entityDt.APCodeFieldName;
            hdnAPDisplayFieldName.Value = entityDt.APDisplayFieldName;
            hdnAPMethodName.Value = entityDt.APMethodName;
            hdnAPFilterExpression.Value = entityDt.APFilterExpression;

            hdnAPSubLedger.Value = entityDt.APSubLedger.ToString();
            txtAPSubLedgerCode.Text = entityDt.APSubLedgerCode.ToString();
            txtAPSubLedgerName.Text = entityDt.APSubLedgerName.ToString();
            #endregion

            #region APInProcess
            hdnAPInProcessID.Value = entityDt.APInProcess.ToString();
            txtAPInProcessGLAccountNo.Text = entityDt.APInProcessGLAccountNo;
            txtAPInProcessGLAccountName.Text = entityDt.APInProcessGLAccountName;
            hdnAPInProcessSubLedgerID.Value = entityDt.APInProcessSubLedgerID.ToString();
            hdnAPInProcessSearchDialogTypeName.Value = entityDt.APInProcessSearchDialogTypeName;
            hdnAPInProcessIDFieldName.Value = entityDt.APInProcessIDFieldName;
            hdnAPInProcessCodeFieldName.Value = entityDt.APInProcessCodeFieldName;
            hdnAPInProcessDisplayFieldName.Value = entityDt.APInProcessDisplayFieldName;
            hdnAPInProcessMethodName.Value = entityDt.APInProcessMethodName;
            hdnAPInProcessFilterExpression.Value = entityDt.APInProcessFilterExpression;

            hdnAPInProcessSubLedger.Value = entityDt.APInProcessSubLedger.ToString();
            txtAPInProcessSubLedgerCode.Text = entityDt.APInProcessSubLedgerCode.ToString();
            txtAPInProcessSubLedgerName.Text = entityDt.APInProcessSubLedgerName.ToString();
            #endregion

            #region APDiscount
            hdnAPDiscountID.Value = entityDt.APDiscount.ToString();
            txtAPDiscountGLAccountNo.Text = entityDt.APDiscountGLAccountNo;
            txtAPDiscountGLAccountName.Text = entityDt.APDiscountGLAccountName;
            hdnAPDiscountSubLedgerID.Value = entityDt.APDiscountSubLedgerID.ToString();
            hdnAPDiscountSearchDialogTypeName.Value = entityDt.APDiscountSearchDialogTypeName;
            hdnAPDiscountIDFieldName.Value = entityDt.APDiscountIDFieldName;
            hdnAPDiscountCodeFieldName.Value = entityDt.APDiscountCodeFieldName;
            hdnAPDiscountDisplayFieldName.Value = entityDt.APDiscountDisplayFieldName;
            hdnAPDiscountMethodName.Value = entityDt.APDiscountMethodName;
            hdnAPDiscountFilterExpression.Value = entityDt.APDiscountFilterExpression;

            hdnAPDiscountSubLedger.Value = entityDt.APDiscountSubLedger.ToString();
            txtAPDiscountSubLedgerCode.Text = entityDt.APDiscountSubLedgerCode.ToString();
            txtAPDiscountSubLedgerName.Text = entityDt.APDiscountSubLedgerName.ToString();
            #endregion

            #region APStamp
            hdnAPStampID.Value = entityDt.APStamp.ToString();
            txtAPStampGLAccountNo.Text = entityDt.APStampGLAccountNo;
            txtAPStampGLAccountName.Text = entityDt.APStampGLAccountName;
            hdnAPStampSubLedgerID.Value = entityDt.APStampSubLedgerID.ToString();
            hdnAPStampSearchDialogTypeName.Value = entityDt.APStampSearchDialogTypeName;
            hdnAPStampIDFieldName.Value = entityDt.APStampIDFieldName;
            hdnAPStampCodeFieldName.Value = entityDt.APStampCodeFieldName;
            hdnAPStampDisplayFieldName.Value = entityDt.APStampDisplayFieldName;
            hdnAPStampMethodName.Value = entityDt.APStampMethodName;
            hdnAPStampFilterExpression.Value = entityDt.APStampFilterExpression;

            hdnAPStampSubLedger.Value = entityDt.APStampSubLedger.ToString();
            txtAPStampSubLedgerCode.Text = entityDt.APStampSubLedgerCode.ToString();
            txtAPStampSubLedgerName.Text = entityDt.APStampSubLedgerName.ToString();
            #endregion

            #region APDownPayment
            hdnAPDownPaymentID.Value = entityDt.APDownPayment.ToString();
            txtAPDownPaymentGLAccountNo.Text = entityDt.APDownPaymentGLAccountNo;
            txtAPDownPaymentGLAccountName.Text = entityDt.APDownPaymentGLAccountName;
            hdnAPDownPaymentSubLedgerID.Value = entityDt.APDownPaymentSubLedgerID.ToString();
            hdnAPDownPaymentSearchDialogTypeName.Value = entityDt.APDownPaymentSearchDialogTypeName;
            hdnAPDownPaymentIDFieldName.Value = entityDt.APDownPaymentIDFieldName;
            hdnAPDownPaymentCodeFieldName.Value = entityDt.APDownPaymentCodeFieldName;
            hdnAPDownPaymentDisplayFieldName.Value = entityDt.APDownPaymentDisplayFieldName;
            hdnAPDownPaymentMethodName.Value = entityDt.APDownPaymentMethodName;
            hdnAPDownPaymentFilterExpression.Value = entityDt.APDownPaymentFilterExpression;

            hdnAPDownPaymentSubLedger.Value = entityDt.APDownPaymentSubLedger.ToString();
            txtAPDownPaymentSubLedgerCode.Text = entityDt.APDownPaymentSubLedgerCode.ToString();
            txtAPDownPaymentSubLedgerName.Text = entityDt.APDownPaymentSubLedgerName.ToString();
            #endregion

            #region APCharge
            hdnAPChargeID.Value = entityDt.APCharge.ToString();
            txtAPChargeGLAccountNo.Text = entityDt.APChargeGLAccountNo;
            txtAPChargeGLAccountName.Text = entityDt.APChargeGLAccountName;
            hdnAPChargeSubLedgerID.Value = entityDt.APChargeSubLedgerID.ToString();
            hdnAPChargeSearchDialogTypeName.Value = entityDt.APChargeSearchDialogTypeName;
            hdnAPChargeIDFieldName.Value = entityDt.APChargeIDFieldName;
            hdnAPChargeCodeFieldName.Value = entityDt.APChargeCodeFieldName;
            hdnAPChargeDisplayFieldName.Value = entityDt.APChargeDisplayFieldName;
            hdnAPChargeMethodName.Value = entityDt.APChargeMethodName;
            hdnAPChargeFilterExpression.Value = entityDt.APChargeFilterExpression;

            hdnAPChargeSubLedger.Value = entityDt.APChargeSubLedger.ToString();
            txtAPChargeSubLedgerCode.Text = entityDt.APChargeSubLedgerCode.ToString();
            txtAPChargeSubLedgerName.Text = entityDt.APChargeSubLedgerName.ToString();
            #endregion

            #region ARPurchaseReturn
            hdnARPurchaseReturnID.Value = entityDt.ARPurchaseReturn.ToString();
            txtARPurchaseReturnGLAccountNo.Text = entityDt.ARPurchaseReturnGLAccountNo;
            txtARPurchaseReturnGLAccountName.Text = entityDt.ARPurchaseReturnGLAccountName;
            hdnARPurchaseReturnSubLedgerID.Value = entityDt.ARPurchaseReturnSubLedgerID.ToString();
            hdnARPurchaseReturnSearchDialogTypeName.Value = entityDt.ARPurchaseReturnSearchDialogTypeName;
            hdnARPurchaseReturnIDFieldName.Value = entityDt.ARPurchaseReturnIDFieldName;
            hdnARPurchaseReturnCodeFieldName.Value = entityDt.ARPurchaseReturnCodeFieldName;
            hdnARPurchaseReturnDisplayFieldName.Value = entityDt.ARPurchaseReturnDisplayFieldName;
            hdnARPurchaseReturnMethodName.Value = entityDt.ARPurchaseReturnMethodName;
            hdnARPurchaseReturnFilterExpression.Value = entityDt.ARPurchaseReturnFilterExpression;

            hdnARPurchaseReturnSubLedger.Value = entityDt.ARPurchaseReturnSubLedger.ToString();
            txtARPurchaseReturnSubLedgerCode.Text = entityDt.ARPurchaseReturnSubLedgerCode.ToString();
            txtARPurchaseReturnSubLedgerName.Text = entityDt.ARPurchaseReturnSubLedgerName.ToString();
            #endregion

            #region ARCreditNote
            hdnARCreditNoteID.Value = entityDt.ARCreditNote.ToString();
            txtARCreditNoteGLAccountNo.Text = entityDt.ARCreditNoteGLAccountNo;
            txtARCreditNoteGLAccountName.Text = entityDt.ARCreditNoteGLAccountName;
            hdnARCreditNoteSubLedgerID.Value = entityDt.ARCreditNoteSubLedgerID.ToString();
            hdnARCreditNoteSearchDialogTypeName.Value = entityDt.ARCreditNoteSearchDialogTypeName;
            hdnARCreditNoteIDFieldName.Value = entityDt.ARCreditNoteIDFieldName;
            hdnARCreditNoteCodeFieldName.Value = entityDt.ARCreditNoteCodeFieldName;
            hdnARCreditNoteDisplayFieldName.Value = entityDt.ARCreditNoteDisplayFieldName;
            hdnARCreditNoteMethodName.Value = entityDt.ARCreditNoteMethodName;
            hdnARCreditNoteFilterExpression.Value = entityDt.ARCreditNoteFilterExpression;

            hdnARCreditNoteSubLedger.Value = entityDt.ARCreditNoteSubLedger.ToString();
            txtARCreditNoteSubLedgerCode.Text = entityDt.ARCreditNoteSubLedgerCode.ToString();
            txtARCreditNoteSubLedgerName.Text = entityDt.ARCreditNoteSubLedgerName.ToString();
            #endregion

            #region APVariance
            hdnAPVarianceID.Value = entityDt.APVariance.ToString();
            txtAPVarianceGLAccountNo.Text = entityDt.APVarianceGLAccountNo;
            txtAPVarianceGLAccountName.Text = entityDt.APVarianceGLAccountName;
            hdnAPVarianceSubLedgerID.Value = entityDt.APVarianceSubLedgerID.ToString();
            hdnAPVarianceSearchDialogTypeName.Value = entityDt.APVarianceSearchDialogTypeName;
            hdnAPVarianceIDFieldName.Value = entityDt.APVarianceIDFieldName;
            hdnAPVarianceCodeFieldName.Value = entityDt.APVarianceCodeFieldName;
            hdnAPVarianceDisplayFieldName.Value = entityDt.APVarianceDisplayFieldName;
            hdnAPVarianceMethodName.Value = entityDt.APVarianceMethodName;
            hdnAPVarianceFilterExpression.Value = entityDt.APVarianceFilterExpression;

            hdnAPVarianceSubLedger.Value = entityDt.APVarianceSubLedger.ToString();
            txtAPVarianceSubLedgerCode.Text = entityDt.APVarianceSubLedgerCode.ToString();
            txtAPVarianceSubLedgerName.Text = entityDt.APVarianceSubLedgerName.ToString();
            #endregion
            #endregion
        }

        private void ControlToEntity(SupplierLineDt entityDt)
        {
            #region Pengaturan Perkiraan
            #region AP
            if (hdnAPID.Value != "" && hdnAPID.Value != "0")
                entityDt.AP = Convert.ToInt32(hdnAPID.Value);
            else
                entityDt.AP = null;
            if (hdnAPSubLedger.Value != "" && hdnAPSubLedger.Value != "0")
                entityDt.APSubLedger = Convert.ToInt32(hdnAPSubLedger.Value);
            else
                entityDt.APSubLedger = null;
            #endregion

            #region APInProcess
            if (hdnAPInProcessID.Value != "" && hdnAPInProcessID.Value != "0")
                entityDt.APInProcess = Convert.ToInt32(hdnAPInProcessID.Value);
            else
                entityDt.APInProcess = null;
            if (hdnAPInProcessSubLedger.Value != "" && hdnAPInProcessSubLedger.Value != "0")
                entityDt.APInProcessSubLedger = Convert.ToInt32(hdnAPInProcessSubLedger.Value);
            else
                entityDt.APInProcessSubLedger = null;
            #endregion

            #region APDiscount
            if (hdnAPDiscountID.Value != "" && hdnAPDiscountID.Value != "0")
                entityDt.APDiscount = Convert.ToInt32(hdnAPDiscountID.Value);
            else
                entityDt.APDiscount = null;
            if (hdnAPDiscountSubLedger.Value != "" && hdnAPDiscountSubLedger.Value != "0")
                entityDt.APDiscountSubLedger = Convert.ToInt32(hdnAPDiscountSubLedger.Value);
            else
                entityDt.APDiscountSubLedger = null;
            #endregion

            #region APStamp
            if (hdnAPStampID.Value != "" && hdnAPStampID.Value != "0")
                entityDt.APStamp = Convert.ToInt32(hdnAPStampID.Value);
            else
                entityDt.APStamp = null;
            if (hdnAPStampSubLedger.Value != "" && hdnAPStampSubLedger.Value != "0")
                entityDt.APStampSubLedger = Convert.ToInt32(hdnAPStampSubLedger.Value);
            else
                entityDt.APStampSubLedger = null;
            #endregion

            #region APDownPayment
            if (hdnAPDownPaymentID.Value != "" && hdnAPDownPaymentID.Value != "0")
                entityDt.APDownPayment = Convert.ToInt32(hdnAPDownPaymentID.Value);
            else
                entityDt.APDownPayment = null;
            if (hdnAPDownPaymentSubLedger.Value != "" && hdnAPDownPaymentSubLedger.Value != "0")
                entityDt.APDownPaymentSubLedger = Convert.ToInt32(hdnAPDownPaymentSubLedger.Value);
            else
                entityDt.APDownPaymentSubLedger = null;
            #endregion

            #region APCharge
            if (hdnAPChargeID.Value != "" && hdnAPChargeID.Value != "0")
                entityDt.APCharge = Convert.ToInt32(hdnAPChargeID.Value);
            else
                entityDt.APCharge = null;
            if (hdnAPChargeSubLedger.Value != "" && hdnAPChargeSubLedger.Value != "0")
                entityDt.APChargeSubLedger = Convert.ToInt32(hdnAPChargeSubLedger.Value);
            else
                entityDt.APChargeSubLedger = null;
            #endregion

            #region ARPurchaseReturn
            if (hdnARPurchaseReturnID.Value != "" && hdnARPurchaseReturnID.Value != "0")
                entityDt.ARPurchaseReturn = Convert.ToInt32(hdnARPurchaseReturnID.Value);
            else
                entityDt.ARPurchaseReturn = null;
            if (hdnARPurchaseReturnSubLedger.Value != "" && hdnARPurchaseReturnSubLedger.Value != "0")
                entityDt.ARPurchaseReturnSubLedger = Convert.ToInt32(hdnARPurchaseReturnSubLedger.Value);
            else
                entityDt.ARPurchaseReturnSubLedger = null;
            #endregion

            #region ARCreditNote
            if (hdnARCreditNoteID.Value != "" && hdnARCreditNoteID.Value != "0")
                entityDt.ARCreditNote = Convert.ToInt32(hdnARCreditNoteID.Value);
            else
                entityDt.ARCreditNote = null;
            if (hdnARCreditNoteSubLedger.Value != "" && hdnARCreditNoteSubLedger.Value != "0")
                entityDt.ARCreditNoteSubLedger = Convert.ToInt32(hdnARCreditNoteSubLedger.Value);
            else
                entityDt.ARCreditNoteSubLedger = null;
            #endregion

            #region APVariance
            if (hdnAPVarianceID.Value != "" && hdnAPVarianceID.Value != "0")
                entityDt.APVariance = Convert.ToInt32(hdnAPVarianceID.Value);
            else
                entityDt.APVariance = null;
            if (hdnAPVarianceSubLedger.Value != "" && hdnAPVarianceSubLedger.Value != "0")
                entityDt.APVarianceSubLedger = Convert.ToInt32(hdnAPVarianceSubLedger.Value);
            else
                entityDt.APVarianceSubLedger = null;
            #endregion
            #endregion
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            SupplierLineDtDao supplierLineDtDao = new SupplierLineDtDao(ctx);
            bool result = false;
            try
            {
                SupplierLineDt entityDt = new SupplierLineDt();
                ControlToEntity(entityDt);
                entityDt.SiteID = AppSession.UserLogin.SiteID;
                entityDt.GCPurchaseType = hdnGCPurchaseType.Value;
                entityDt.CreatedBy = AppSession.UserLogin.UserID;
                supplierLineDtDao.Insert(entityDt);
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

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            SupplierLineDtDao supplierLineDtDao = new SupplierLineDtDao(ctx);
            bool result = false;
            try
            {
                SupplierLineDt entityDt = supplierLineDtDao.Get(Convert.ToInt32(hdnSupplierLineID.Value), AppSession.UserLogin.SiteID, hdnGCPurchaseType.Value);
                ControlToEntity(entityDt);
                entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                supplierLineDtDao.Update(entityDt);
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
    }
}