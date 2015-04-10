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
using System.Globalization;
using DevExpress.Web.ASPxEditors;
using System.Data;
using CodeX.Common;

namespace CodeX.Muses.Web.Finance.Program
{
    public partial class DirectPaymentViewCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            SalesInvoiceHd salesInvoiceHd = BusinessLayer.GetSalesInvoiceHd(Convert.ToInt32(param));
            hdnInvoiceID.Value = salesInvoiceHd.SalesInvoiceID.ToString();
            txtInvoiceNo.Text = salesInvoiceHd.SalesInvoiceNo;
            txtInvoiceTotal.Text = salesInvoiceHd.TransactionAmount.ToString();

            vDirectPaymentHd entityHd = BusinessLayer.GetvDirectPaymentHdList(string.Format("SalesInvoiceID = {0}", salesInvoiceHd.SalesInvoiceID)).FirstOrDefault();
            txtPaymentNo.Text = entityHd.PaymentNo;
            txtPaymentDate.Text = entityHd.PaymentDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtPaymentTime.Text = entityHd.PaymentTime;
            txtRemarks.Text = entityHd.Remarks;
            txtPaymentType.Text = entityHd.PaymentType;
            txtPayment.Text = entityHd.TotalPaymentAmount.ToString();
            txtCashReturnAmount.Text = entityHd.CashReturnAmount.ToString();

            List<vDirectPaymentDt> lstDt = BusinessLayer.GetvDirectPaymentDtList(string.Format("PaymentID = {0}", entityHd.PaymentID));
            lvwPaymentDt.DataSource = lstDt;
            lvwPaymentDt.DataBind();

            Decimal patientAmount = lstDt.Select(p => p.PaymentAmount).Sum();
            Decimal cardFeeAmount = lstDt.Select(p => p.CardFeeAmount).Sum();
            tdTotalPatientEdit.InnerHtml = patientAmount.ToString("N");
            tdTotalCardFeeEdit.InnerHtml = cardFeeAmount.ToString("N");
            tdLineTotalEdit.InnerHtml = (patientAmount + cardFeeAmount).ToString("N");

            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(String.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.CARD_TYPE, Constant.StandardCode.CARD_PROVIDER));
            Methods.SetComboBoxField<StandardCode>(cboCardType, lstSc.Where(p => p.ParentID == Constant.StandardCode.CARD_TYPE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboCardProvider, lstSc.Where(p => p.ParentID == Constant.StandardCode.CARD_PROVIDER).ToList(), "StandardCodeName", "StandardCodeID");

            cboCardDateMonth.DataSource = Enumerable.Range(1, 12).Select(a => new
            {
                MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(a),
                MonthNumber = a
            });
            cboCardDateMonth.TextField = "MonthName";
            cboCardDateMonth.ValueField = "MonthNumber";
            cboCardDateMonth.EnableCallbackMode = false;
            cboCardDateMonth.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboCardDateMonth.DropDownStyle = DropDownStyle.DropDownList;
            cboCardDateMonth.DataBind();

            cboCardDateYear.DataSource = Enumerable.Range(DateTime.Now.Year, 10);
            cboCardDateYear.EnableCallbackMode = false;
            cboCardDateYear.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboCardDateYear.DropDownStyle = DropDownStyle.DropDownList;
            cboCardDateYear.DataBind();
        }
    }
}