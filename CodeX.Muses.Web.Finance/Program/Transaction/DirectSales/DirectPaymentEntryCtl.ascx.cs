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
    public partial class DirectPaymentEntryCtl : BaseEntryPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            SalesInvoiceHd entityHd = BusinessLayer.GetSalesInvoiceHd(Convert.ToInt32(param));
            hdnInvoiceID.Value = entityHd.SalesInvoiceID.ToString();
            txtInvoiceNo.Text = entityHd.SalesInvoiceNo;
            txtInvoiceTotal.Text = entityHd.NetTransactionAmount.ToString();

            hdnCreditCardFeeFilterExpression.Value = string.Format("SiteID = '{0}' AND GCCardType = '[GCCardType]' AND GCCardProvider = '[GCCardProvider]' AND EDCMachineID = [EDCMachineID]", AppSession.UserLogin.SiteID);

            List<EDCMachine> lstEDCMachine = BusinessLayer.GetEDCMachineList("IsDeleted = 0");
            Methods.SetComboBoxField<EDCMachine>(cboEDCMachine, lstEDCMachine, "EDCMachineName", "EDCMachineID");
            cboEDCMachine.SelectedIndex = 0;

            List<Bank> lstBank = BusinessLayer.GetBankList("IsDeleted = 0");
            Methods.SetComboBoxField<Bank>(cboBank, lstBank, "BankName", "BankID");
            cboBank.SelectedIndex = 0;

            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(String.Format("ParentID IN ('{0}','{1}','{2}','{3}') AND StandardCodeID NOT IN ('{4}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.CARD_TYPE, Constant.StandardCode.PAYMENT_METHOD, Constant.StandardCode.PAYMENT_TYPE, Constant.StandardCode.CARD_PROVIDER, Constant.PaymentMethod.BANK_TRANSFER));
            Methods.SetComboBoxField<StandardCode>(cboCardType, lstSc.Where(p => p.ParentID == Constant.StandardCode.CARD_TYPE).ToList(), "StandardCodeName", "StandardCodeID");

            Methods.SetComboBoxField<StandardCode>(cboPaymentMethod, lstSc.Where(p => p.ParentID == Constant.StandardCode.PAYMENT_METHOD && p.StandardCodeID != Constant.PaymentMethod.ACCOUNT_RECEIVABLES).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboPaymentType, lstSc.Where(p => p.ParentID == Constant.StandardCode.PAYMENT_TYPE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboCardProvider, lstSc.Where(p => p.ParentID == Constant.StandardCode.CARD_PROVIDER).ToList(), "StandardCodeName", "StandardCodeID");

            tdARPaymentMethod.InnerHtml = lstSc.FirstOrDefault(p => p.StandardCodeID == Constant.PaymentMethod.ACCOUNT_RECEIVABLES).StandardCodeName;

            cboPaymentMethod.SelectedIndex = 0;
            cboCardType.SelectedIndex = 0;
            //cboPaymentType.Value = Constant.PaymentType.SETTLEMENT;
            //cboPaymentType.ClientEnabled = false;

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

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnPaymentHdID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtPaymentNo, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtPaymentDate, new ControlEntrySetting(true, false, true, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtPaymentTime, new ControlEntrySetting(true, false, true, Constant.DefaultValueEntry.TIME_NOW));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, false, false));
            SetControlEntrySetting(cboPaymentType, new ControlEntrySetting(true, false, true, Constant.PaymentType.SETTLEMENT));

            SetControlEntrySetting(txtInvoiceNo, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtInvoiceTotal, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtPayment, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(txtCashReturnAmount, new ControlEntrySetting(false, false, true));

            SetControlEntrySetting(cboCardType, new ControlEntrySetting(true, false, false));
            SetControlEntrySetting(cboCardProvider, new ControlEntrySetting(true, false, false));
            SetControlEntrySetting(txtCardNumber4, new ControlEntrySetting(true, false, false));
            SetControlEntrySetting(txtHolderName, new ControlEntrySetting(true, false, false));
            SetControlEntrySetting(cboCardDateMonth, new ControlEntrySetting(true, false, false));
            SetControlEntrySetting(cboCardDateYear, new ControlEntrySetting(true, false, false));
        }

        protected void cbpPaymentDt_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
           // BindGrdPaymentDetail();
        }

        #region Save Entity
        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            String[] listParam = hdnInlineEditingData.Value.Split('|');

            IDbContext ctx = DbFactory.Configure(true);
            DirectPaymentHdDao entityHdDao = new DirectPaymentHdDao(ctx);
            DirectPaymentDtDao entityDtDao = new DirectPaymentDtDao(ctx);
            SalesInvoiceHdDao salesInvoiceHdDao = new SalesInvoiceHdDao(ctx);
            SalesInvoiceDtDao salesInvoiceDtDao = new SalesInvoiceDtDao(ctx);
            try
            {
                #region Payment Hd
                DirectPaymentHd entityHd = new DirectPaymentHd();
                entityHd.PaymentDate = Helper.GetDatePickerValue(txtPaymentDate);
                entityHd.PaymentTime = txtPaymentTime.Text;
                entityHd.GCPaymentType = cboPaymentType.Value.ToString();
                entityHd.SalesInvoiceID = Convert.ToInt32(hdnInvoiceID.Value);
                entityHd.TotalPaymentAmount = Convert.ToInt32(hdnTotalPaymentAmount.Value);
                entityHd.TotalFeeAmount = Convert.ToInt32(hdnTotalFeeAmount.Value);
                entityHd.CashReturnAmount = Convert.ToDecimal(Request.Form[txtCashReturnAmount.UniqueID]);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                entityHd.PaymentNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.DIRECT_PAYMENT, entityHd.PaymentDate, ctx);
                entityHd.CreatedBy = entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHdDao.Insert(entityHd);
                entityHd.PaymentID = BusinessLayer.GetDirectPaymentHdMaxID(ctx);
                #endregion

                #region Payment Dt
                foreach (String param in listParam)
                {
                    String[] data = param.Split(';');
                    bool isChanged = data[0] == "1" ? true : false;
                    int ID = Convert.ToInt32(data[1]);
                    if (isChanged || ID > 0)
                    {
                        DirectPaymentDt entityDt = new DirectPaymentDt();
                        entityDt.PaymentID = entityHd.PaymentID;
                        entityDt.GCPaymentMethod = data[2];
                        if (entityDt.GCPaymentMethod != Constant.PaymentMethod.CASH)
                        {
                            if (data[3] != "")
                                entityDt.EDCMachineID = Convert.ToInt32(data[3]);
                            else
                                entityDt.EDCMachineID = null;
                            if (data[5] != "")
                                entityDt.BankID = Convert.ToInt32(data[5]);
                            else
                                entityDt.BankID = null;
                            entityDt.ReferenceNo = data[6];
                            entityDt.GCCardType = data[10];
                            if (data[11] != "")
                                entityDt.CardNumber = string.Format("XXXX-XXXX-XXXX-{0}", data[11]);
                            else
                                entityDt.CardNumber = "";
                            entityDt.CardHolderName = data[12];
                            if (data[13] != "" && data[14] != "")
                                entityDt.CardValidThru = string.Format("{0:00}/{1:00}", data[13].PadLeft(2, '0'), data[14].Substring(2));
                            else
                                entityDt.CardValidThru = "";
                            entityDt.GCCardProvider = data[16];
                        }
                        entityDt.PaymentAmount = Convert.ToDecimal(data[7].Replace(",00", "").Replace(".", ""));
                        entityDt.CardFeeAmount = Convert.ToDecimal(data[8].Replace(",00", "").Replace(".", ""));
                        entityDt.CreatedBy = AppSession.UserLogin.UserID;
                        entityDtDao.Insert(entityDt);
                    }
                }
                
                #endregion

                #region Update Invoice
                SalesInvoiceHd salesInvoiceHd = salesInvoiceHdDao.Get(Convert.ToInt32(hdnInvoiceID.Value));
                salesInvoiceHd.GCTransactionStatus = Constant.TransactionStatus.CLOSED;
                salesInvoiceHd.LastUpdatedDate = DateTime.Now;
                salesInvoiceHdDao.Update(salesInvoiceHd);

                List<SalesInvoiceDt> lstSalesInvoiceDt = BusinessLayer.GetSalesInvoiceDtList(string.Format("SalesInvoiceID = {0}", salesInvoiceHd.SalesInvoiceID), ctx);
                foreach (SalesInvoiceDt salesInvoiceDt in lstSalesInvoiceDt)
                {
                    salesInvoiceDt.GCItemDetailStatus = Constant.TransactionStatus.CLOSED;
                    salesInvoiceDt.LastUpdatedDate = DateTime.Now;
                    salesInvoiceDtDao.Update(salesInvoiceDt);
                }
                #endregion

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                result = false;
                errMessage = ex.Message;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }

            return result;
        }
        #endregion
    }
}