using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using DevExpress.Web.ASPxCallbackPanel;
using System.Web.UI.HtmlControls;
using CodeX.Data.Core.Dal;
using CodeX.Common;
using System.Data;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class DirectPurchaseConfirmationDtCtl : BaseViewPopupCtl
    {
        protected string GetVATPercentageLabel()
        {
            return hdnVATPercentage.Value;
        }

        public override void InitializeDataControl(string param)
        {
            hdnVATPercentage.Value = BusinessLayer.GetSettingParameter(Constant.SettingParameter.VAT_PERCENTAGE).ParameterValue;

            List<StandardCode> listStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.DIRECT_PURCHASE_TYPE, Constant.StandardCode.ITEM_UNIT));
            Methods.SetComboBoxField<StandardCode>(cboDirectPurchaseType, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.DIRECT_PURCHASE_TYPE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            cboDirectPurchaseType.SelectedIndex = 0;

            hdnDirectPurchaseID.Value = param;

            vDirectPurchaseHd entity = BusinessLayer.GetvDirectPurchaseHdList(string.Format("DirectPurchaseID = {0}", hdnDirectPurchaseID.Value)).FirstOrDefault();
            txtDirectPurchaseNo.Text = entity.DirectPurchaseNo;
            txtDirectPurchaseDate.Text = entity.PurchaseDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtReferenceNo.Text = entity.ReferenceNo;
            if (entity.ReferenceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT) != Constant.ConstantDate.DEFAULT_NULL)
                txtReferenceDate.Text = entity.ReferenceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            else
                txtReferenceDate.Text = "";            
            txtRemarks.Text = entity.Remarks;
            chkPPN.Checked = entity.IsIncludeVAT;
            txtPPN.Text = entity.VATAmount.ToString();
            txtTransactionAmount.Text = entity.TransactionAmount.ToString();
            txtTotalNetTransactionAmount.Text = entity.TotalNetTransactionAmount.ToString();
            txtFinalDiscountPercentage.Text = entity.FinalDiscountPercentage.ToString();
            txtFinalDiscountAmount.Text = entity.FinalDiscountAmount.ToString();
            txtLocationName.Text = entity.LocationName;
            txtServiceUnitName.Text = entity.ServiceUnitName;
            txtToServiceUnitName.Text = entity.ToServiceUnitName;
            cboDirectPurchaseType.Value = entity.GCDirectPurchaseType;
            txtSupplier.Text = entity.BusinessPartnerName;

            BindGridView();
        }

        private void BindGridView()
        {
            string filterExpression = string.Format("DirectPurchaseID = {0} AND GCItemDetailStatus != '{1}'", hdnDirectPurchaseID.Value, Constant.TransactionStatus.VOID);
            List<vDirectPurchaseDt> lstEntity = BusinessLayer.GetvDirectPurchaseDtList(filterExpression);
            lvwView.DataSource = lstEntity;
            lvwView.DataBind();
        }

        protected void cbpPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}