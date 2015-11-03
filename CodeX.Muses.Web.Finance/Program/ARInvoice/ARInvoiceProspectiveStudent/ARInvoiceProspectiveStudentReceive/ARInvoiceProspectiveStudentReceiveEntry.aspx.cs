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
using System.Globalization;
using DevExpress.Web.ASPxEditors;
using CodeX.Data.Core.Dal;
using System.Data;
using CodeX.Common;

namespace CodeX.Muses.Web.Finance.Program
{
    public partial class ARInvoiceProspectiveStudentReceiveEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Finance.AR_INVOICE_PROSPECTIVE_STUDENT_RECEIVE;
        }
        
        protected string OnGetCustomerFilterExpression()
        {
            return string.Format("IsDeleted = 0");
        }

        protected override void InitializeDataControl()
        {
            InitializeControlProperties();
        }
        private void InitializeControlProperties()
        {
            hdnCreditCardFeeFilterExpression.Value = string.Format("SiteID = '{0}' AND GCCardType = '[GCCardType]' AND GCCardProvider = '[GCCardProvider]' AND EDCMachineID = [EDCMachineID]", AppSession.UserLogin.SiteID);
            
            List<EDCMachine> lstEDCMachine = BusinessLayer.GetEDCMachineList("IsDeleted = 0");
            Methods.SetComboBoxField<EDCMachine>(cboEDCMachine, lstEDCMachine, "EDCMachineName", "EDCMachineID");
            cboEDCMachine.SelectedIndex = 0;

            List<Bank> lstBank = BusinessLayer.GetBankList("IsDeleted = 0");
            Methods.SetComboBoxField<Bank>(cboBank, lstBank, "BankName", "BankID");
            cboBank.SelectedIndex = 0;

            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(String.Format("ParentID IN ('{0}','{1}','{2}','{3}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.CARD_TYPE, Constant.StandardCode.PAYMENT_METHOD, Constant.StandardCode.PAYMENT_TYPE, Constant.StandardCode.CARD_PROVIDER));
            Methods.SetComboBoxField<StandardCode>(cboCardType, lstSc.Where(p => p.ParentID == Constant.StandardCode.CARD_TYPE).ToList(), "StandardCodeName", "StandardCodeID");

            Methods.SetComboBoxField<StandardCode>(cboPaymentMethod, lstSc.Where(p => p.ParentID == Constant.StandardCode.PAYMENT_METHOD && p.StandardCodeID != Constant.PaymentMethod.ACCOUNT_RECEIVABLES && p.StandardCodeID != Constant.PaymentMethod.DOWN_PAYMENT).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboCardProvider, lstSc.Where(p => p.ParentID == Constant.StandardCode.CARD_PROVIDER).ToList(), "StandardCodeName", "StandardCodeID");

            tdARPaymentMethod.InnerHtml = lstSc.FirstOrDefault(p => p.StandardCodeID == Constant.PaymentMethod.ACCOUNT_RECEIVABLES).StandardCodeName;

            cboPaymentMethod.SelectedIndex = 0;
            cboCardType.SelectedIndex = 0;

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

            OnAddRecord();

        }
        protected override void SetControlProperties()
        {
            ListView lvwARInvoice = (ListView) ddeInvoiceNo.FindControl("lvwInvoice");
            string filter = string.Format("ProspectiveStudentID = {0} AND (GCTransactionStatus = '{1}' OR GCTransactionStatus = '{2}')", AppSession.ProspectiveStudentID, Constant.TransactionStatus.APPROVED, Constant.TransactionStatus.PROCESSED);
            List<ARInvoiceHd> lst = BusinessLayer.GetARInvoiceHdList(filter);
            
            lvwARInvoice.DataSource = lst;
            lvwARInvoice.DataBind();

            Helper.SetControlEntrySetting(cboCardType, new ControlEntrySetting(true, true, true), "vgCardInformation");
            Helper.SetControlEntrySetting(cboCardProvider, new ControlEntrySetting(true, true, true), "vgCardInformation");
            Helper.SetControlEntrySetting(txtCardNumber4, new ControlEntrySetting(true, true, true), "vgCardInformation");
            Helper.SetControlEntrySetting(txtHolderName, new ControlEntrySetting(true, true, true), "vgCardInformation");
            Helper.SetControlEntrySetting(cboCardDateMonth, new ControlEntrySetting(true, true, true), "vgCardInformation");
            Helper.SetControlEntrySetting(cboCardDateYear, new ControlEntrySetting(true, true, true), "vgCardInformation");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnARReceivingID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtARReceivingNo, new ControlEntrySetting(true, false, false));
            SetControlEntrySetting(txtReceivingDate, new ControlEntrySetting(true, false, true, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, false, false));

            SetControlEntrySetting(txtInvoiceNo, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(ddeInvoiceNo, new ControlEntrySetting(true, false, false));
            SetControlEntrySetting(txtRemainingTotal, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(txtPaymentAmount, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtCashbackAmount, new ControlEntrySetting(false, false, true));

            SetControlEntrySetting(cboCardType, new ControlEntrySetting(true, false, false));
            SetControlEntrySetting(cboCardProvider, new ControlEntrySetting(true, false, false));
            SetControlEntrySetting(txtCardNumber4, new ControlEntrySetting(true, false, false));
            SetControlEntrySetting(txtHolderName, new ControlEntrySetting(true, false, false));
            SetControlEntrySetting(cboCardDateMonth, new ControlEntrySetting(true, false, false));
            SetControlEntrySetting(cboCardDateYear, new ControlEntrySetting(true, false, false));
        }
        
        #region Load Entity
        private void BindGrdARReceivingDetail()
        {
            List<vARReceivingDt> lstDt = BusinessLayer.GetvARReceivingDtList(string.Format("ARReceivingID = {0}",hdnARReceivingID.Value));
            lvwARReceivingDt.DataSource = lstDt;
            lvwARReceivingDt.DataBind();

            decimal paymentAmount = lstDt.Select(p => p.PaymentAmount).Sum();
            decimal cardFeeAmount = lstDt.Select(p => p.CardFeeAmount).Sum();

            tdTotalPaymentEdit.InnerHtml = paymentAmount.ToString("N");
            tdTotalCardFeeEdit.InnerHtml = cardFeeAmount.ToString("N");
            tdLineTotalEdit.InnerHtml = (paymentAmount + cardFeeAmount).ToString("N");
            hdnTotalPaymentAmount.Value = paymentAmount.ToString();

        }
        protected void cbpARReceivingDt_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGrdARReceivingDetail();
        }

        public string GetFilterExpression()
        {
            string filterExpression = string.Format("ProspectiveStudentID = {0}", AppSession.ProspectiveStudentID);
            return filterExpression;
        }
        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvARReceivingHdRowCount(filterExpression);
        }
        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vARReceivingHd entity = BusinessLayer.GetvARReceivingHd(filterExpression, PageIndex, "ARReceivingID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }
        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvARReceivingHdRowIndex(filterExpression, keyValue, "ARReceivingID DESC");
            vARReceivingHd entity = BusinessLayer.GetvARReceivingHd(filterExpression, PageIndex, "ARReceivingID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }
        private void EntityToControl(vARReceivingHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus == Constant.TransactionStatus.VOID)
            {
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
            }
            hdnARReceivingID.Value = entity.ARReceivingID.ToString();
            txtARReceivingNo.Text = entity.ARReceivingNo;
            txtReceivingDate.Text = entity.ReceivingDateInString;// ReceivingDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtInvoiceNo.Text = entity.InvoiceNo;
            txtRemarks.Text = entity.Remarks;
            txtRemainingTotal.Text = entity.TotalInvoiceAmount.ToString();
            txtPaymentAmount.Text = entity.TotalReceivingAmount.ToString();
            txtCashbackAmount.Text = entity.CashBackAmount.ToString();

            BindGrdARReceivingDetail();

        }
        #endregion

        #region Save
        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            String[] listParam = hdnInlineEditingData.Value.Split('|');

            IDbContext ctx = DbFactory.Configure(true);

            ARInvoiceHdDao entityInvoiceHdDao = new ARInvoiceHdDao(ctx);
            ARInvoiceDtDao entityInvoiceDtDao = new ARInvoiceDtDao(ctx);
            ARReceivingHdDao entityReceivingHdDao = new ARReceivingHdDao(ctx);
            ARReceivingDtDao entityReceivingDtDao = new ARReceivingDtDao(ctx);
            ARInvoiceReceivingDao entityIRDao = new ARInvoiceReceivingDao(ctx);
            RegistrationDao entityRegDao = new RegistrationDao(ctx);
            StudentFeeDtDao entityStudentFeeDtDao = new StudentFeeDtDao(ctx);
            try
            {
                #region ARReceivingHD
                ARReceivingHd entityReceivingHd = new ARReceivingHd();
                List<ARInvoiceHd> lstARInvoiceHd = BusinessLayer.GetARInvoiceHdList(string.Format("ARInvoiceID IN ({0})", hdnListInvoiceID.Value));
                decimal totalInvoice = 0;

                entityReceivingHd.ProspectiveStudentID = AppSession.ProspectiveStudentID;
                entityReceivingHd.StudentID = null;
                entityReceivingHd.ReceivingDate = Helper.GetDatePickerValue(txtReceivingDate);
                entityReceivingHd.TotalReceivingAmount = Convert.ToDecimal(hdnTotalPaymentAmount.Value);
                entityReceivingHd.TotalFeeAmount = Convert.ToDecimal(hdnTotalFeeAmount.Value);
                entityReceivingHd.CashBackAmount = Convert.ToDecimal(hdnCashbackAmount.Value);
                //entityReceivingHd.TotalInvoiceAmount = Convert.ToDecimal(hdnTotalTransactionAmount.Value);
                entityReceivingHd.Remarks = txtRemarks.Text;
                entityReceivingHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                entityReceivingHd.ARReceivingNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.AR_RECEIVE_PROSPECTIVE_STUDENT, entityReceivingHd.ReceivingDate, ctx);
                entityReceivingHd.CreatedBy = entityReceivingHd.LastUpdatedBy = AppSession.UserLogin.UserID;

                foreach (ARInvoiceHd arinvoicehd in lstARInvoiceHd)
                {
                    totalInvoice += arinvoicehd.RemainingAmount;
                    if (totalInvoice > entityReceivingHd.TotalReceivingAmount)
                        break;
                }
                entityReceivingHd.TotalInvoiceAmount = totalInvoice;

                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityReceivingHdDao.Insert(entityReceivingHd);
                entityReceivingHd.ARReceivingID = BusinessLayer.GetARReceivingHdMaxID(ctx);
                #endregion
                
                #region ARReceivingDt
                foreach(String param in listParam){
                    String[] data = param.Split(';');
                    bool isChanged = data[0] == "1" ? true : false;
                    int ID = Convert.ToInt32(data[1]);
                    if(isChanged || ID > 0)
                    {
                        ARReceivingDt entityDt = new ARReceivingDt();
                        entityDt.ARReceivingID = entityReceivingHd.ARReceivingID;
                        entityDt.GCARPaymentMethod = data[2];
                        if(entityDt.GCARPaymentMethod != Constant.PaymentMethod.CASH)
                        {
                            if(data[3] != "")
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
                        entityReceivingDtDao.Insert(entityDt);
                    }
                }
                #endregion

                #region Update InvoiceReceive
                if (hdnListInvoiceID.Value != "")
                {
                    List<ARInvoiceHd> lstARInvoiceHD = BusinessLayer.GetARInvoiceHdList(string.Format("ARInvoiceID IN ({0})", hdnListInvoiceID.Value), ctx);
                    List<ARInvoiceDt> lstARInvoiceDt = BusinessLayer.GetARInvoiceDtList(string.Format("ARInvoiceID IN ({0})", hdnListInvoiceID.Value), ctx);
                    String lstStudentFeeDtID = String.Join(",", lstARInvoiceDt.Select(x => x.StudentFeeDtID).ToList());
                    List<StudentFeeDt> lstStudentFeeDt = BusinessLayer.GetStudentFeeDtList(String.Format("StudentFeeDtID IN ({0})", lstStudentFeeDtID), ctx);
                    decimal totalPaymentAmount = entityReceivingHd.TotalReceivingAmount;
                    foreach (ARInvoiceHd ARInvoiceHdobj in lstARInvoiceHD)
                    {
                        List<ARInvoiceDt> lstARInvoiceDt1 = lstARInvoiceDt.Where(p => p.ARInvoiceID == ARInvoiceHdobj.ARInvoiceID).ToList();
                        foreach (ARInvoiceDt aRInvoiceDt in lstARInvoiceDt1)
                        {
                            StudentFeeDt studentFeeDt = lstStudentFeeDt.FirstOrDefault(p => p.StudentFeeDtID == aRInvoiceDt.StudentFeeDtID);
                            studentFeeDt.IsPaid = true;
                            studentFeeDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityStudentFeeDtDao.Update(studentFeeDt);

                            ARInvoiceReceiving ARInvoiceReceivingObj = new ARInvoiceReceiving();
                            ARInvoiceReceivingObj.ARInvoiceID = ARInvoiceHdobj.ARInvoiceID;
                            ARInvoiceReceivingObj.ARReceivingID = entityReceivingHd.ARReceivingID;
                            ARInvoiceReceivingObj.ARInvoiceDtID = aRInvoiceDt.ARInvoiceDtID;

                            decimal remainingAmount = (aRInvoiceDt.ClaimedAmount - aRInvoiceDt.PaymentAmount);
                            if (remainingAmount < totalPaymentAmount)
                            {
                                ARInvoiceReceivingObj.ReceivingAmount = remainingAmount;
                                aRInvoiceDt.PaymentAmount = aRInvoiceDt.ClaimedAmount;
                                ARInvoiceHdobj.TotalPaymentAmount += remainingAmount;
                                totalPaymentAmount -= remainingAmount;
                            }
                            else
                            {
                                ARInvoiceReceivingObj.ReceivingAmount = totalPaymentAmount;
                                aRInvoiceDt.PaymentAmount += totalPaymentAmount;
                                ARInvoiceHdobj.TotalPaymentAmount += totalPaymentAmount;
                                totalPaymentAmount = 0;
                            }
                            aRInvoiceDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityInvoiceDtDao.Update(aRInvoiceDt);
                            entityIRDao.Insert(ARInvoiceReceivingObj);
                        }
                        if (ARInvoiceHdobj.RemainingAmount == 0)
                            ARInvoiceHdobj.GCTransactionStatus = Constant.TransactionStatus.CLOSED;
                        else
                            ARInvoiceHdobj.GCTransactionStatus = Constant.TransactionStatus.PROCESSED;
                        ARInvoiceHdobj.LastUpdatedBy = AppSession.UserLogin.UserID;
                        ARInvoiceHdobj.LastUpdatedDate = DateTime.Now;
                        entityInvoiceHdDao.Update(ARInvoiceHdobj);
                    }
                }
                #endregion

                entityReceivingHd = entityReceivingHdDao.Get(entityReceivingHd.ARReceivingID);
                entityReceivingHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                entityReceivingHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityReceivingHdDao.Update(entityReceivingHd);

                //Registration entityReg = BusinessLayer.GetRegistrationList(string.Format("ProspectiveStudentID = {0}", AppSession.ProspectiveStudentID)).FirstOrDefault();
                //int rowCount = BusinessLayer.GetARInvoiceHdRowCount(string.Format("ProspectiveStudentID = {0} AND GCTransactionStatus != '{1}' AND TotalClaimedAmount != TotalPaymentAmount", AppSession.ProspectiveStudentID, Constant.TransactionStatus.VOID), ctx);
                //if (rowCount < 1)
                //{
                //    entityReg.GCRegistrationStatus = Constant.RegistrationStatus.SETTLED;
                //    entityReg.LastUpdatedBy = AppSession.UserLogin.UserID;
                //    entityRegDao.Update(entityReg);
                //}
                //else if (entityReg.GCRegistrationStatus == Constant.RegistrationStatus.AR_PROCESSED)
                //{
                //    entityReg.GCRegistrationStatus = Constant.RegistrationStatus.PAID;
                //    entityReg.LastUpdatedBy = AppSession.UserLogin.UserID;
                //    entityRegDao.Update(entityReg);
                //}
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

        #region Void
        protected override bool OnVoidRecord(ref string errMessage)
        {
            bool result = true;

            IDbContext ctx = DbFactory.Configure(true);
            ARReceivingHdDao entityARRHdDao = new ARReceivingHdDao(ctx);
            ARInvoiceHdDao entityARIHdDao = new ARInvoiceHdDao(ctx);
            ARInvoiceDtDao entityARIDtDao = new ARInvoiceDtDao(ctx);
            ARInvoiceReceivingDao entityIRDao = new ARInvoiceReceivingDao(ctx);
            RegistrationDao entityRegDao = new RegistrationDao(ctx);
            StudentFeeDtDao entityStudentFeeDtDao = new StudentFeeDtDao(ctx);
            try
            {
                ARReceivingHd entityARR = entityARRHdDao.Get(Convert.ToInt32(hdnARReceivingID.Value));
                entityARR.GCTransactionStatus = Constant.TransactionStatus.VOID;
                entityARR.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityARRHdDao.Update(entityARR);

                List<ARInvoiceReceiving> lstARIR = BusinessLayer.GetARInvoiceReceivingList(string.Format("ARReceivingID = {0}", hdnARReceivingID.Value), ctx);
                string lstARInvoiceID = string.Join(",", lstARIR.Select(p => p.ARInvoiceID).ToList());
                List<ARInvoiceHd> lstARInvoice = BusinessLayer.GetARInvoiceHdList(string.Format("ARInvoiceID IN ({0})", lstARInvoiceID), ctx);
                foreach (ARInvoiceHd enARI in lstARInvoice)
                {
                    List<ARInvoiceReceiving> lstARIR1 = lstARIR.Where(p => p.ARInvoiceID == enARI.ARInvoiceID).ToList();
                    string lstARInvoiceDtID = string.Join(",", lstARIR1.Select(p => p.ARInvoiceDtID).ToList());
                    List<ARInvoiceDt> lstARInvoiceDt = BusinessLayer.GetARInvoiceDtList(string.Format("ARInvoiceDtID IN ({0})", lstARInvoiceDtID), ctx);
                    String lstStudentFeeDtID = String.Join(",", lstARInvoiceDt.Select(x => x.StudentFeeDtID).ToList());
                    List<StudentFeeDt> lstStudentFeeDt = BusinessLayer.GetStudentFeeDtList(String.Format("StudentFeeDtID IN ({0})", lstStudentFeeDtID), ctx);

                    foreach (ARInvoiceDt aRInvoiceDt in lstARInvoiceDt)
                    {
                        aRInvoiceDt.PaymentAmount -= lstARIR1.FirstOrDefault(p => p.ARInvoiceDtID == aRInvoiceDt.ARInvoiceDtID).ReceivingAmount;
                        aRInvoiceDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityARIDtDao.Update(aRInvoiceDt);

                        StudentFeeDt studentFeeDt = lstStudentFeeDt.FirstOrDefault(p => p.StudentFeeDtID == aRInvoiceDt.StudentFeeDtID);
                        if (aRInvoiceDt.PaymentAmount == 0)
                        {
                            studentFeeDt.IsPaid = false;
                            studentFeeDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityStudentFeeDtDao.Update(studentFeeDt);
                        }
                    }

                    enARI.TotalPaymentAmount -= lstARIR.Where(p => p.ARInvoiceID == enARI.ARInvoiceID).Sum(p => p.ReceivingAmount);
                    enARI.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                    enARI.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityARIHdDao.Update(enARI);
                }

                int rowCount = BusinessLayer.GetARReceivingHdRowCount(string.Format("ProspectiveStudentID = {0} AND GCTransactionStatus != '{1}'", AppSession.ProspectiveStudentID, Constant.TransactionStatus.VOID), ctx);
                if (rowCount < 1)
                {
                    Registration entityReg = BusinessLayer.GetRegistrationList(string.Format("ProspectiveStudentID = {0}", AppSession.ProspectiveStudentID)).FirstOrDefault();
                    entityReg.GCRegistrationStatus = Constant.RegistrationStatus.AR_PROCESSED;
                    entityReg.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityRegDao.Update(entityReg);
                }

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
        #endregion

    }
}