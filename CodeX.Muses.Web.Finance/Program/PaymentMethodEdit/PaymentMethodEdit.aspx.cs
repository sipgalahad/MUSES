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
using CodeX.Common;
using DevExpress.Web.ASPxEditors;
using System.Globalization;
using CodeX.Data.Core.Dal;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Web.UI.HtmlControls;
namespace CodeX.Muses.Web.Finance.Program
{
    public partial class PaymentMethodEdit : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Finance.PAYMENT_METHOD_EDIT;
        }
        
        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            
        }

        #region HTML Getter
        public String OnGetStudentFilterExpression() 
        {
            return String.Format("IsDeleted = 0 AND SiteID = '{0}'", AppSession.UserLogin.SiteID);
        }
        #endregion

        public class TempClass
        {
            Int32 _StudentFeeCompID;
            String _DueDate;
            Decimal _PaymentAmount;

            public Int32 StudentFeeCompID
            {
                get { return _StudentFeeCompID; }
                set { _StudentFeeCompID = value; }
            }
            public String DueDate
            {
                get { return _DueDate; }
                set { _DueDate = value; }
            }
            public Decimal PaymentAmount
            {
                get { return _PaymentAmount; }
                set { _PaymentAmount = value; }
            }

        }

        public override void SetFilterParameter(ref string[] fieldListText, ref string[] fieldListValue)
        {
            //fieldListText = new string[] { "Business Partner Code", "Business Partner Name" };
            //fieldListValue = new string[] { "BusinessPartnerCode", "BusinessPartnerName" };
        }

        private string GetFilterExpression()
        {
            string filterExpression = String.Format("StudentID = {0} AND GCAdmissionPaymentPeriod IN ('{1}','{2}') AND IsDeleted = 0", tacStudent.Value, Constant.AdmissionPaymentPeriod.TAHUNAN, Constant.AdmissionPaymentPeriod.SEKALI_BAYAR);
            return filterExpression;
        }

        List<vStudentFee> lstStudentFee = null;
        public void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            String filterExpression = GetFilterExpression();
            List<vStudentFeeComp> lstStudentFeeComp = BusinessLayer.GetvStudentFeeCompList(filterExpression);
            String StudentFeeCompID = String.Join(",", lstStudentFeeComp.Select(x => x.StudentFeeCompID));
            lstStudentFee = BusinessLayer.GetvStudentFeeList(String.Format("StudentFeeCompID IN ({0})", StudentFeeCompID));
            rptStudentFeeComp.DataSource = lstStudentFeeComp;
            rptStudentFeeComp.DataBind();
        }

        protected void rptStudentFeeComp_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vStudentFeeComp entity = e.Item.DataItem as vStudentFeeComp;
                
                Repeater rptStudentFee = (Repeater)e.Item.FindControl("rptStudentFee");
                rptStudentFee.DataSource = lstStudentFee.Where(x => x.StudentFeeCompID == entity.StudentFeeCompID && x.IsDeleted == false && (x.GCTransactionStatus != Constant.TransactionStatus.CLOSED && x.GCTransactionStatus != Constant.TransactionStatus.VOID));
                rptStudentFee.DataBind();

                HtmlInputHidden hdnTotalAmount = e.Item.FindControl("hdnTotalAmount") as HtmlInputHidden;
                hdnTotalAmount.Attributes.Add("class", String.Format("hdnTotalAmount{0}", entity.StudentFeeCompID));
                Decimal totalAmount = entity.TotalAmount - lstStudentFee.Where(x => x.StudentFeeCompID == entity.StudentFeeCompID && x.IsDeleted == false && x.GCTransactionStatus == Constant.TransactionStatus.CLOSED).Sum(x => x.LineAmount);
                hdnTotalAmount.Value = totalAmount.ToString();

                HtmlTableCell tdTotalAmount = e.Item.FindControl("tdTotalAmount") as HtmlTableCell;
                tdTotalAmount.InnerHtml = totalAmount.ToString("N");
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount, ref rowCount);
                    result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }


        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (OnSaveEditRecord(ref errMessage))
                result += "success";
            else
                result += string.Format("fail|{0}", errMessage);

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
        
        private Boolean OnSaveEditRecord(ref String errMessage) 
        {

            #region intialization
            List<String> lstData = hdnParam.Value.Split('|').ToList();
            List<TempClass> lstTempClass = new List<TempClass>();
            foreach (String data in lstData)
            {
                if (data != "") 
                {
                    TempClass temp = new TempClass();
                    String[] arr = data.Split(';');
                    temp.StudentFeeCompID = Convert.ToInt32(arr[0]);
                    temp.DueDate = arr[1];
                    temp.PaymentAmount = Convert.ToDecimal(arr[2]);
                    lstTempClass.Add(temp);
                }
            }

            String filterExpression = GetFilterExpression();
            List<vStudentFeeComp> lstStudentFeeComp = BusinessLayer.GetvStudentFeeCompList(filterExpression);
            String StudentFeeCompID = String.Join(",", lstStudentFeeComp.Select(x => x.StudentFeeCompID));
            lstStudentFee = BusinessLayer.GetvStudentFeeList(String.Format("StudentFeeCompID IN ({0}) AND IsDeleted = 0 AND GCTransactionStatus IN ('{1}','{2}','{3}')", StudentFeeCompID, Constant.TransactionStatus.APPROVED, Constant.TransactionStatus.WAIT_FOR_APPROVAL, Constant.TransactionStatus.PROCESSED));

            List<Int32> lstStudentFeeCompID = lstStudentFeeComp.GroupBy(x => x.StudentFeeCompID).Select(x => x.Key).ToList();
            String studentFeeID = String.Join(",", lstStudentFee.Select(x => x.StudentFeeID));

            List<ARInvoiceDt> lstARInvoiceDt = BusinessLayer.GetARInvoiceDtList(String.Format("StudentFeeID IN ({0})", studentFeeID));
            String arInvoiceID = String.Join(",", lstARInvoiceDt.Select(x => x.ARInvoiceID));
            List<ARInvoiceHd> lstARInvoiceHd = BusinessLayer.GetARInvoiceHdList(String.Format("ARInvoiceID IN ({0})", arInvoiceID));

            List<StudentFeeCompType> lstSfct = BusinessLayer.GetStudentFeeCompTypeList(String.Format("SiteID = '{0}' AND IsDeleted = 0",AppSession.UserLogin.SiteID));
            List<Bank> lstBankID = BusinessLayer.GetBankList(String.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID));
            Int32 BankID = lstBankID.FirstOrDefault().BankID;
            #endregion

            Boolean status = true;
            IDbContext ctx = DbFactory.Configure(true);
            StudentFeeDao studentFeeDao = new StudentFeeDao(ctx);
            ARInvoiceDtDao ardtDao = new ARInvoiceDtDao(ctx);
            ARInvoiceHdDao arhdDao = new ARInvoiceHdDao(ctx);

            try
            {
                foreach (Int32 studentFeeCompID in lstStudentFeeCompID)
                {
                    Int32 countEdit = lstTempClass.Select(x => x.StudentFeeCompID == studentFeeCompID).Count();
                    Int32 countSF = lstStudentFee.Select(x => x.StudentFeeCompID == studentFeeCompID).Count();
                    List<vStudentFee> lstSf = lstStudentFee.Where(x => x.StudentFeeCompID == studentFeeCompID).ToList();
                    List<TempClass> lstTc = lstTempClass.Where(x => x.StudentFeeCompID == studentFeeCompID).ToList();

                    if (countEdit > countSF)
                    {
                        Int32 SchoolPeriodID = 0;
                        Int32? StudentFeeCompTypeID = 0;
                        //Update
                        Int32 count = 0;
                        Int32 DisplayOrder = 0;
                        foreach (vStudentFee obj in lstSf)
                        {
                            StudentFee sf = studentFeeDao.Get(obj.StudentFeeID);
                            SchoolPeriodID = sf.SchoolPeriodID;
                            sf.TotalPaymentAmount = lstTc[count].PaymentAmount;
                            sf.LineAmount = sf.TotalPaymentAmount - sf.TotalDiscountAmount;
                            DisplayOrder = sf.DisplayOrder;
                            studentFeeDao.Update(sf);

                            ARInvoiceDt ardt = lstARInvoiceDt.FirstOrDefault(x=> x.StudentFeeID == obj.StudentFeeID);
                            StudentFeeCompTypeID = ardt.StudentFeeCompTypeID;
                            ardt.TransactionAmount = lstTc[count].PaymentAmount;
                            ardt.ClaimedAmount = ardt.TransactionAmount - ardt.DiscountAmount;
                            ardtDao.Update(ardt);

                            count++;
                        }

                        for (int i = 0; i < countEdit - countSF; i++)
                        {
                            StudentFee sf = new StudentFee();
                            sf.StudentID = Convert.ToInt32(tacStudent.Value);
                            sf.SchoolPeriodID = SchoolPeriodID;
                            sf.StudentFeeCompID = studentFeeCompID;
                            sf.DisplayOrder = Convert.ToInt16(++DisplayOrder);
                            sf.PaymentDate = Helper.GetDatePickerValue(lstTc[count].DueDate);
                            sf.TotalPaymentAmount = lstTc[count].PaymentAmount;
                            sf.LineAmount = sf.TotalPaymentAmount - sf.TotalDiscountAmount;
                            sf.IsDeleted = false;
                            sf.CreatedBy = AppSession.UserLogin.UserID;
                            sf.CreatedDate = DateTime.Now;
                            studentFeeDao.Insert(sf);
                            Int32 StudentFeeID = BusinessLayer.GetStudentFeeMaxID(ctx);

                            ARInvoiceHd arhd = new ARInvoiceHd();
                            arhd.ARInvoiceNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.AR_INVOICE_STUDENT, DateTime.Now,ctx);
                            ctx.CommandType = System.Data.CommandType.Text;
                            arhd.BankID = BankID;
                            arhd.ARInvoiceDate = DateTime.Now;
                            arhd.StudentID = Convert.ToInt32(tacStudent.Value);
                            DateTime DueDate = Helper.GetDatePickerValue(lstTc[count].DueDate);
                            arhd.DueDate = DueDate;
                            arhd.Remarks = String.Format("Tagihan {0} {1} {2}",lstSfct.FirstOrDefault(x => x.StudentFeeCompTypeID == StudentFeeCompTypeID).StudentFeeCompTypeName,
                                            DueDate.ToString("MMMM", CultureInfo.InvariantCulture), DueDate.Year);
                            arhd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                            arhd.CreatedBy = AppSession.UserLogin.UserID;
                            arhdDao.Insert(arhd);
                            Int32 ARInvoiceHdID = BusinessLayer.GetARInvoiceHdMaxID(ctx);
                            
                            ARInvoiceDt ardt = new ARInvoiceDt();
                            ardt.ARInvoiceID = ARInvoiceHdID;
                            ardt.StudentFeeID = StudentFeeID;
                            ardt.StudentFeeCompTypeID = StudentFeeCompTypeID;
                            ardt.TransactionAmount = lstTc[count].PaymentAmount;
                            ardt.DiscountAmount = 0;
                            ardt.ClaimedAmount = ardt.TransactionAmount - ardt.DiscountAmount;
                            ardt.VarianceAmount = null;
                            ardt.CreatedBy = AppSession.UserLogin.UserID;
                            ardtDao.Insert(ardt);

                            count++;
                        }
                        
                    }
                    else if (countEdit < countSF)
                    {
                        //Update
                        Int32 count = 0;
                        foreach (vStudentFee obj in lstSf)
                        {
                            if (count < countEdit)
                            {
                                StudentFee sf = studentFeeDao.Get(obj.StudentFeeID);
                                sf.TotalPaymentAmount = lstTc[count].PaymentAmount;
                                sf.LineAmount = sf.TotalPaymentAmount - sf.TotalDiscountAmount;
                                studentFeeDao.Update(sf);

                                ARInvoiceDt ardt = lstARInvoiceDt.FirstOrDefault(x => x.StudentFeeID == obj.StudentFeeID);
                                ardt.TransactionAmount = lstTc[count].PaymentAmount;
                                ardt.ClaimedAmount = ardt.TransactionAmount - ardt.DiscountAmount;
                                ardtDao.Update(ardt);
                            }
                            else 
                            {
                                StudentFee sf = studentFeeDao.Get(obj.StudentFeeID);
                                sf.IsDeleted = true;
                                studentFeeDao.Update(sf);

                                ARInvoiceDt ardt = lstARInvoiceDt.FirstOrDefault(x => x.StudentFeeID == obj.StudentFeeID);
                                ARInvoiceHd arhd = lstARInvoiceHd.FirstOrDefault(x => x.ARInvoiceID == ardt.ARInvoiceID);
                                arhd.GCTransactionStatus = Constant.TransactionStatus.VOID;
                                arhd.LastUpdatedBy = AppSession.UserLogin.UserID;
                                arhdDao.Update(arhd);
                                ardtDao.Delete(ardt.ARInvoiceDtID);
                            }
                            count++;
                        }
                    }
                }
                
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                ctx.RollBackTransaction();
                status = false;
            }
            finally
            {
                ctx.Close();
            }
            return status;
        }
    }
}