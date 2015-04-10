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

namespace CodeX.Muses.Web.Finance.Program
{
    public partial class DirectSalesEntryDtCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnSalesInvoiceID.Value = param;
            vSalesInvoiceHd entity = BusinessLayer.GetvSalesInvoiceHdList(String.Format("SalesInvoiceID = {0}",param))[0];
            EntityToControl(entity);
            BindGridView();
        }

        private void EntityToControl(vSalesInvoiceHd entity)
        {
            txtSalesInvoiceNo.Text = entity.SalesInvoiceNo;
            txtSalesUnitDate.Text = entity.SalesInvoiceDate.ToString(Constant.FormatString.DATE_FORMAT);
            txtStudentCode.Text = entity.StudentCode;
            txtStudentName.Text = entity.StudentName;
            chkPPN.Checked = entity.IsIncludeVAT;
            txtLocationCode.Text = entity.LocationCode;
            txtLocationName.Text = entity.LocationName;
            txtTerm.Text = entity.TermName;
            txtFrancoRegion.Text = entity.FrancoRegion;
            txtCurrency.Text = entity.CurrencyCode;
            txtKurs.Text = entity.CurrencyRate.ToString();

            txtNotes.Text = entity.Remarks;
            txtTransactionAmount.Text = entity.TransactionAmount.ToString();
            Decimal vatAmount = entity.TransactionAmount * (entity.VATPercentage / 100);
            txtPPNPercentage.Text = entity.VATPercentage.ToString();
            txtPPN.Text = vatAmount.ToString();
            txtTransactionAmountAfterVAT.Text = (vatAmount + entity.TransactionAmount).ToString();
            txtFinalDiscount.Text = entity.FinalDiscountAmount.ToString();
            txtFinalDiscountInPercentage.Text = entity.FinalDiscountPercentage.ToString();
            txtTransactionAmountSaldo.Text = entity.NetTransactionAmount.ToString();
        }

        private void BindGridView()
        {
            string filterExpression = string.Format("SalesInvoiceID = {0} AND GCItemDetailStatus != '{1}'", hdnSalesInvoiceID.Value, Constant.TransactionStatus.VOID);
            List<vSalesInvoiceDt> lstEntity = BusinessLayer.GetvSalesInvoiceDtList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}