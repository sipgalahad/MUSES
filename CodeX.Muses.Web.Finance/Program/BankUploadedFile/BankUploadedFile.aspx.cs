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
namespace CodeX.Muses.Web.Finance.Program
{
    public partial class BankUploadedFile : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        List<BankData> lstBankData = new List<BankData>();
        public class BankData
        {
            String _NBS;
            String _StudentName;
            Decimal _Amount;
            String _Status;

            public String NBS
            {
                get { return _NBS; }
                set { _NBS = value; }
            }
            public String StudentName
            {
                get { return _StudentName; }
                set { _StudentName = value; }
            }
            public Decimal Amount
            {
                get { return _Amount; }
                set { _Amount = value; }
            }
            public String Status
            {
                get { return _Status; }
                set { _Status = value; }
            }
        }
        
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Finance.BANK_UPLOADED_FILE;
        }
        
        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            
        }

        public override void SetFilterParameter(ref string[] fieldListText, ref string[] fieldListValue)
        {
            //fieldListText = new string[] { "Business Partner Code", "Business Partner Name" };
            //fieldListValue = new string[] { "BusinessPartnerCode", "BusinessPartnerName" };
        }

        private string GetFilterExpression()
        {
            string filterExpression = "";
            return filterExpression;
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            //if (FileUploadControl.HasFile)
            //{
            //    try
            //    {
            //        string filename = Path.GetFileName(FileUploadControl.FileName);
            //        FileUploadControl.SaveAs(Server.MapPath("~/") + filename);
            //        StatusLabel.Text = "Upload status: File uploaded!";
            //    }
            //    catch (Exception ex)
            //    {
            //        StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
            //    }
            //}
        }

        public void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            String data = GetDataFromFile();
            UploadFile(data);
            grdView.DataSource = lstBankData;
            grdView.DataBind();
        }

        public void UploadFile(String data) 
        {
            IDbContext ctx = DbFactory.Configure(true);
            ARBalanceDao arBalanceDao = new ARBalanceDao(ctx);
            ARInvoiceHdDao arInvoiceHdDao = new ARInvoiceHdDao(ctx);
            ARInvoiceDtDao entityInvoiceDtDao = new ARInvoiceDtDao(ctx);
            ARReceivingHdDao entityReceivingHdDao = new ARReceivingHdDao(ctx);
            ARReceivingDtDao entityReceivingDtDao = new ARReceivingDtDao(ctx);
            ARInvoiceReceivingDao entityIRDao = new ARInvoiceReceivingDao(ctx);
            StudentFeeDtDao entityStudentFeeDtDao = new StudentFeeDtDao(ctx);
            try
            {
                List<vARInvoiceHd> lstARInvoiceHd = BusinessLayer.GetvARInvoiceHdList(String.Format("GCTransactionStatus = '{0}'", Constant.TransactionStatus.PROCESSED), ctx);
                String lstARInvoiceID = String.Join(",", lstARInvoiceHd.Select(x => x.ARInvoiceID).ToList());
                List<ARInvoiceDt> lstARInvoiceDt = BusinessLayer.GetARInvoiceDtList(String.Format("ARInvoiceID IN ({0})", lstARInvoiceID), ctx);

                String lstStudentFeeDtID = String.Join(",", lstARInvoiceDt.Select(x => x.StudentFeeDtID).ToList());
                List<StudentFeeDt> lstStudentFeeDt = BusinessLayer.GetStudentFeeDtList(String.Format("StudentFeeDtID IN ({0})", lstStudentFeeDtID), ctx);

                string filterExpressionARBalance = "";
                String lstStudentID = String.Join(",", lstARInvoiceHd.Where(s => s.StudentID != 0).Select(x => x.StudentID).ToList());
                List<Student> lstStudent = null;
                if (lstStudentID != "")
                {
                    filterExpressionARBalance = String.Format("StudentID IN ({0})", lstStudentID);
                    lstStudent = BusinessLayer.GetStudentList(String.Format("StudentID IN ({0})", lstStudentID), ctx);
                }
                else
                    lstStudent = new List<Student>();

                String lstProspectiveStudentID = String.Join(",", lstARInvoiceHd.Where(s => s.StudentID == 0 && s.ProspectiveStudentID != 0).Select(x => x.ProspectiveStudentID).ToList());
                List<ProspectiveStudent> lstProspectiveStudent = null;
                if (lstProspectiveStudentID != "")
                {
                    if (filterExpressionARBalance != "")
                        filterExpressionARBalance += " AND ";
                    filterExpressionARBalance = String.Format("ProspectiveStudentID IN ({0})", lstProspectiveStudentID);
                    lstProspectiveStudent = BusinessLayer.GetProspectiveStudentList(String.Format("ProspectiveStudentID IN ({0})", lstProspectiveStudentID), ctx);
                }
                else
                    lstProspectiveStudent = new List<ProspectiveStudent>();

                List<ARBalance> lstARBalance = BusinessLayer.GetARBalanceList(filterExpressionARBalance, ctx);
                //String data = txtUploadedData.Text;
                data = data.Replace("\r\n", "|");
                String[] arrData = data.Split('|').ToArray();
                for (int i = 6; i < arrData.Count(); )
                {
                    if (arrData[i].Contains(":86:UBP60"))
                    {
                        BankData entity = new BankData();
                        entity.NBS = arrData[i].Substring(24, 6);
                        //entity.StudentName = "test";
                        entity.Amount = Convert.ToDecimal(arrData[i - 1].Substring(arrData[i - 1].IndexOf('C') + 1, arrData[i - 1].IndexOf('N') - 1 - arrData[i - 1].IndexOf('C')));

                        Student entityStudent = lstStudent.FirstOrDefault(p => p.VirtualAccountNo == entity.NBS);
                        ProspectiveStudent entityProspectiveStudent = lstProspectiveStudent.FirstOrDefault(p => p.ProspectiveStudentCode == entity.NBS);

                        if (entityStudent != null || entityProspectiveStudent != null)
                        {
                            string stringDate = arrData[i - 1].Substring(4, 6);
                            DateTime receivingDate = DateTime.ParseExact(stringDate,
                                            "yyMMdd",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None);
                            ARBalance entityARBalance = null;
                            ARReceivingHd entityReceivingHd = new ARReceivingHd();
                            if (entityStudent != null)
                            {
                                entityReceivingHd.StudentID = entityStudent.StudentID;
                                entityARBalance = lstARBalance.FirstOrDefault(p => p.StudentID == entityStudent.StudentID);
                            }
                            else
                                entityReceivingHd.StudentID = null;

                            if (entityProspectiveStudent != null)
                            {
                                entityReceivingHd.ProspectiveStudentID = entityProspectiveStudent.ProspectiveStudentID;
                                entityARBalance = lstARBalance.FirstOrDefault(p => p.ProspectiveStudentID == entityProspectiveStudent.ProspectiveStudentID);
                            }
                            else
                                entityReceivingHd.ProspectiveStudentID = null;
                            entityReceivingHd.ReceivingDate = receivingDate;

                            decimal totalAmount = entity.Amount;
                            if (entityARBalance != null)
                                totalAmount += entityARBalance.DepositAmount;

                            entityReceivingHd.TotalInvoiceAmount = entityReceivingHd.TotalReceivingAmount = totalAmount;
                            entityReceivingHd.TotalFeeAmount = 0;
                            entityReceivingHd.CashBackAmount = 0;
                            entityReceivingHd.Remarks = "";
                            entityReceivingHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                            if (entityStudent != null)
                                entityReceivingHd.ARReceivingNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.AR_RECEIVE_STUDENT, entityReceivingHd.ReceivingDate, ctx);
                            else
                                entityReceivingHd.ARReceivingNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.AR_RECEIVE_PROSPECTIVE_STUDENT, entityReceivingHd.ReceivingDate, ctx);
                            entityReceivingHd.CreatedBy = entityReceivingHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                            ctx.CommandType = CommandType.Text;
                            ctx.Command.Parameters.Clear();
                            entityReceivingHdDao.Insert(entityReceivingHd);
                            entityReceivingHd.ARReceivingID = BusinessLayer.GetARReceivingHdMaxID(ctx);

                            ARReceivingDt entityDt = new ARReceivingDt();
                            entityDt.ARReceivingID = entityReceivingHd.ARReceivingID;
                            entityDt.GCARPaymentMethod = Constant.PaymentMethod.BANK_TRANSFER;
                            entityDt.PaymentAmount = entity.Amount;
                            entityDt.CardFeeAmount = 0;
                            entityDt.CreatedBy = AppSession.UserLogin.UserID;
                            entityReceivingDtDao.Insert(entityDt);

                            decimal totalInvoiceAmount = 0;
                            foreach (vARInvoiceHd obj in lstARInvoiceHd.Where(x => x.VirtualAccount == entity.NBS).ToList())
                            {
                                ARInvoiceHd arInvoiceHD = arInvoiceHdDao.Get(obj.ARInvoiceID);
                                arInvoiceHD.TotalPaymentAmount = arInvoiceHD.TotalClaimedAmount;
                                totalInvoiceAmount += arInvoiceHD.TotalPaymentAmount;
                                arInvoiceHD.GCTransactionStatus = Constant.TransactionStatus.CLOSED;
                                arInvoiceHD.LastUpdatedBy = AppSession.UserLogin.UserID;
                                arInvoiceHdDao.Update(arInvoiceHD);

                                List<ARInvoiceDt> lstARInvoiceDt1 = lstARInvoiceDt.Where(p => p.ARInvoiceID == arInvoiceHD.ARInvoiceID).ToList();
                                foreach (ARInvoiceDt aRInvoiceDt in lstARInvoiceDt1)
                                {
                                    StudentFeeDt studentFeeDt = lstStudentFeeDt.FirstOrDefault(p => p.StudentFeeDtID == aRInvoiceDt.StudentFeeDtID);
                                    studentFeeDt.IsPaid = true;
                                    studentFeeDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                                    entityStudentFeeDtDao.Update(studentFeeDt);

                                    ARInvoiceReceiving ARInvoiceReceivingObj = new ARInvoiceReceiving();
                                    ARInvoiceReceivingObj.ARInvoiceID = arInvoiceHD.ARInvoiceID;
                                    ARInvoiceReceivingObj.ARReceivingID = entityReceivingHd.ARReceivingID;
                                    ARInvoiceReceivingObj.ReceivingAmount = aRInvoiceDt.ClaimedAmount;
                                    ARInvoiceReceivingObj.ARInvoiceDtID = aRInvoiceDt.ARInvoiceDtID;
                                    entityIRDao.Insert(ARInvoiceReceivingObj);
                                }


                                if (arInvoiceHD.StudentID != null && arInvoiceHD.StudentID != 0)
                                {
                                    entity.StudentName = entityStudent.StudentName;
                                    entity.Status = "Siswa";
                                }
                                else
                                {
                                    entity.StudentName = entityProspectiveStudent.ProspectiveStudentName;
                                    entity.Status = "Calon Siswa";
                                }
                            }

                            if (entityARBalance != null)
                            {
                                ARReceivingDt entityDt2 = new ARReceivingDt();
                                entityDt2.ARReceivingID = entityReceivingHd.ARReceivingID;
                                entityDt2.GCARPaymentMethod = Constant.PaymentMethod.DOWN_PAYMENT_RETURN;
                                if (totalInvoiceAmount < entityARBalance.DepositAmount)
                                    entityDt2.PaymentAmount = totalInvoiceAmount;
                                else
                                    entityDt2.PaymentAmount = entityARBalance.DepositAmount;
                                entityDt2.CardFeeAmount = 0;
                                entityDt2.CreatedBy = AppSession.UserLogin.UserID;
                                entityReceivingDtDao.Insert(entityDt2);

                                entityARBalance.DepositAmount -= entityDt2.PaymentAmount;
                                entityARBalance.LastUpdatedBy = AppSession.UserLogin.UserID;
                                arBalanceDao.Update(entityARBalance);
                            }

                            entityReceivingHd = entityReceivingHdDao.Get(entityReceivingHd.ARReceivingID);
                            entityReceivingHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                            entityReceivingHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityReceivingHdDao.Update(entityReceivingHd);
                        }
                        lstBankData.Add(entity);
                    }
                    i += 2;
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                String errMessage = ex.Message;
                ctx.RollBackTransaction();
            }
            finally 
            {
                ctx.Close();
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

        public String GetDataFromFile() 
        {
            string imageData = hdnUploadedFile1.Value;
            if (imageData != "")
            {
                string[] parts = Regex.Split(imageData, ",").Skip(1).ToArray();
                imageData = String.Join(",", parts);
            }

            byte[] data = Convert.FromBase64String(imageData);
            var stream = new StreamReader(new MemoryStream(data));
            string text = stream.ReadToEnd();
            return text;
        }

        protected void cbpPopupProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string imageData = hdnUploadedFile1.Value;
            if (imageData != "")
            {
                string[] parts = Regex.Split(imageData, ",").Skip(1).ToArray();
                imageData = String.Join(",", parts);
            }

            byte[] data = Convert.FromBase64String(imageData);
            var stream = new StreamReader(new MemoryStream(data));
            string text = stream.ReadToEnd();
            UploadFile(text);
        }
    }
}