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
            Int32 _ProspectiveStudentID;
            Int32 _StudentID;
            String _NBS;
            String _StudentName;
            String _PaymentDate;
            Boolean _IsProcessed;
            Decimal _Amount;
            String _Status;

            public String cfStudentID
            {
                get { return string.Format("{0}|{1}", _StudentID, _ProspectiveStudentID); }
            }
            public Int32 ProspectiveStudentID
            {
                get { return _ProspectiveStudentID; }
                set { _ProspectiveStudentID = value; }
            }
            public Int32 StudentID
            {
                get { return _StudentID; }
                set { _StudentID = value; }
            }
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
            public String PaymentDate
            {
                get { return _PaymentDate; }
                set { _PaymentDate = value; }
            }
            public Boolean IsProcessed
            {
                get { return _IsProcessed; }
                set { _IsProcessed = value; }
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

        public string GetSiteID() 
        {
            return AppSession.UserLogin.SiteID;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            //List<Site> lstSite = BusinessLayer.GetSiteList(String.Format("ParentID = '{0}' OR SiteID = '{0}'", AppSession.UserLogin.SiteID));
            //String lstSiteID = "";
            //foreach (Site obj in lstSite)
            //{
            //    if (lstSiteID != "") lstSiteID += ',';
            //    lstSiteID += String.Format("'{0}'", obj.SiteID);
            //}

            //List<vSite> lstSite = BusinessLayer.GetvSiteList(String.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsHeader = 0", AppSession.UserLogin.SiteID));
            //Methods.SetComboBoxField<vSite>(cboSite, lstSite, "SiteName", "SiteID");
            //cboSite.SelectedIndex = 0;
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
            grdView.DataSource = lstBankData.OrderBy(p => p.NBS).ToList();
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BankData entity = (BankData)e.Row.DataItem;
                if (!entity.IsProcessed)
                    e.Row.CssClass = "highlighted";
            }
        }

        public void UploadFile(String data, ref string errMessage) 
        {
            IDbContext ctx = DbFactory.Configure(true);
            ARBalanceDao arBalanceDao = new ARBalanceDao(ctx);
            ARInvoiceHdDao arInvoiceHdDao = new ARInvoiceHdDao(ctx);
            ARInvoiceDtDao entityInvoiceDtDao = new ARInvoiceDtDao(ctx);
            ARReceivingHdDao entityReceivingHdDao = new ARReceivingHdDao(ctx);
            ARReceivingDtDao entityReceivingDtDao = new ARReceivingDtDao(ctx);
            ARInvoiceReceivingDao entityIRDao = new ARInvoiceReceivingDao(ctx);
            StudentFeeDtDao entityStudentFeeDtDao = new StudentFeeDtDao(ctx);
            BankDao bankDao = new BankDao(ctx);
            try
            {
                Bank bank = bankDao.Get(Convert.ToInt32(tacBank.Value));
                List<vARInvoiceHd> lstARInvoiceHd = BusinessLayer.GetvARInvoiceHdList(String.Format("GCTransactionStatus IN ('{0}','{1}')", Constant.TransactionStatus.APPROVED, Constant.TransactionStatus.PROCESSED), ctx);
                String lstARInvoiceID = String.Join(",", lstARInvoiceHd.Select(x => x.ARInvoiceID).ToList());
                List<ARInvoiceDt> lstARInvoiceDt = null;
                List<StudentFeeDt> lstStudentFeeDt = null;
                if (lstARInvoiceID != "") 
                {
                    lstARInvoiceDt = BusinessLayer.GetARInvoiceDtList(String.Format("ARInvoiceID IN ({0})", lstARInvoiceID), ctx);
                    String lstStudentFeeDtID = String.Join(",", lstARInvoiceDt.Where(p => p.StudentFeeDtID != null).Select(x => x.StudentFeeDtID).ToList());
                    if (lstStudentFeeDtID != "") lstStudentFeeDt = BusinessLayer.GetStudentFeeDtList(String.Format("StudentFeeDtID IN ({0})", lstStudentFeeDtID), ctx);
                } 

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
                if (bank.GCBankExportDataType == Constant.BankExportDataType.MANDIRI) 
                {
                    #region Upload Mandiri
                    data = data.Replace("\r\n", "|");
                    String[] arrData = data.Split('|').ToArray();
                    for (int i = 6; i < arrData.Count(); )
                    {
                        if (arrData[i].Contains(":86:UBP60"))
                        {
                            BankData entity = new BankData();
                            entity.NBS = arrData[i].Substring(24, 6);
                            String PaymentDate = arrData[i - 1].Substring(4,6);
                            DateTime date = DateTime.ParseExact(PaymentDate, "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                            entity.PaymentDate = date.ToString(Constant.FormatString.DATE_FORMAT);
                            entity.Amount = Convert.ToDecimal(arrData[i - 1].Substring(arrData[i - 1].IndexOf('C') + 1, arrData[i - 1].IndexOf('N') - 1 - arrData[i - 1].IndexOf('C')));

                            Student entityStudent = lstStudent.FirstOrDefault(p => p.VirtualAccountNo == entity.NBS);
                            ProspectiveStudent entityProspectiveStudent = lstProspectiveStudent.FirstOrDefault(p => p.ProspectiveStudentCode == entity.NBS);
                            
                            #region Proses ARReceiving, ARInvoice, ARBalance
                            if (entityStudent != null || entityProspectiveStudent != null)
                            {
                                #region Generate Data
                                string stringDate = arrData[i - 1].Substring(4, 6);
                                DateTime receivingDate = DateTime.ParseExact(stringDate,
                                                "yyMMdd",
                                                CultureInfo.InvariantCulture,
                                                DateTimeStyles.None);
                                ARBalance entityARBalance = null;
                                if (entityStudent != null)
                                {
                                    entityARBalance = lstARBalance.FirstOrDefault(p => p.StudentID == entityStudent.StudentID);
                                    entity.StudentID = entityStudent.StudentID;
                                    entity.ProspectiveStudentID = 0;
                                    entity.StudentName = entityStudent.StudentName;
                                    entity.Status = "Siswa";
                                }
                                else if (entityProspectiveStudent != null)
                                {
                                    entityARBalance = lstARBalance.FirstOrDefault(p => p.ProspectiveStudentID == entityProspectiveStudent.ProspectiveStudentID);
                                    entity.StudentName = entityProspectiveStudent.ProspectiveStudentName;
                                    entity.Status = "Calon Siswa";
                                    entity.StudentID = 0;
                                    entity.ProspectiveStudentID = entityProspectiveStudent.ProspectiveStudentID;
                                }
                                
                                decimal totalAmount = entity.Amount - bank.AdministrationAmount;
                                if (entityARBalance != null)
                                    totalAmount += entityARBalance.DepositAmount;
                                #endregion

                                List<vARInvoiceHd> lstARInvoiceHd1 = lstARInvoiceHd.Where(x => x.VirtualAccount == entity.NBS).ToList();
                                if (lstARInvoiceHd1.Sum(p => p.TotalClaimedAmount - p.TotalPaymentAmount) == totalAmount)
                                {
                                    entity.IsProcessed = true;
                                    #region ARReceiving
                                    ARReceivingHd entityReceivingHd = new ARReceivingHd();
                                    if (entityStudent != null)
                                        entityReceivingHd.StudentID = entityStudent.StudentID;
                                    else
                                        entityReceivingHd.StudentID = null;

                                    if (entityProspectiveStudent != null)
                                        entityReceivingHd.ProspectiveStudentID = entityProspectiveStudent.ProspectiveStudentID;
                                    else
                                        entityReceivingHd.ProspectiveStudentID = null;
                                    entityReceivingHd.ReceivingDate = receivingDate;
                                    entityReceivingHd.TotalInvoiceAmount = entityReceivingHd.TotalReceivingAmount = totalAmount;
                                    entityReceivingHd.TotalFeeAmount = bank.AdministrationAmount;
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
                                    entityDt.BankID = bank.BankID;
                                    entityDt.PaymentAmount = entity.Amount - bank.AdministrationAmount;
                                    entityDt.CardFeeAmount = bank.AdministrationAmount;
                                    entityDt.CreatedBy = AppSession.UserLogin.UserID;
                                    entityReceivingDtDao.Insert(entityDt);
                                    #endregion

                                    #region ARBalance
                                    if (entityARBalance != null && entityARBalance.DepositAmount > 0)
                                    {
                                        ARReceivingDt entityDt2 = new ARReceivingDt();
                                        entityDt2.ARReceivingID = entityReceivingHd.ARReceivingID;
                                        entityDt2.GCARPaymentMethod = Constant.PaymentMethod.DOWN_PAYMENT_RETURN;
                                        if (totalAmount < entityARBalance.DepositAmount)
                                            entityDt2.PaymentAmount = totalAmount;
                                        else
                                            entityDt2.PaymentAmount = entityARBalance.DepositAmount;
                                        entityDt2.CardFeeAmount = 0;
                                        entityDt2.CreatedBy = AppSession.UserLogin.UserID;
                                        entityReceivingDtDao.Insert(entityDt2);

                                        entityARBalance.DepositAmount -= entityDt2.PaymentAmount;
                                        entityARBalance.LastUpdatedBy = AppSession.UserLogin.UserID;
                                        arBalanceDao.Update(entityARBalance);
                                    }
                                    #endregion

                                    #region ARInvoice
                                    decimal totalInvoiceAmount = 0;
                                    foreach (vARInvoiceHd obj in lstARInvoiceHd1)
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
                                            if (studentFeeDt != null)
                                            {
                                                studentFeeDt.IsPaid = true;
                                                studentFeeDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                                                entityStudentFeeDtDao.Update(studentFeeDt);
                                            }

                                            ARInvoiceReceiving ARInvoiceReceivingObj = new ARInvoiceReceiving();
                                            ARInvoiceReceivingObj.ARInvoiceID = arInvoiceHD.ARInvoiceID;
                                            ARInvoiceReceivingObj.ARReceivingID = entityReceivingHd.ARReceivingID;
                                            ARInvoiceReceivingObj.ReceivingAmount = aRInvoiceDt.ClaimedAmount - aRInvoiceDt.PaymentAmount;
                                            ARInvoiceReceivingObj.ARInvoiceDtID = aRInvoiceDt.ARInvoiceDtID;
                                            entityIRDao.Insert(ARInvoiceReceivingObj);

                                            aRInvoiceDt.PaymentAmount = aRInvoiceDt.ClaimedAmount;
                                            aRInvoiceDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                                            entityInvoiceDtDao.Update(aRInvoiceDt);
                                        }
                                    }
                                    #endregion

                                    entityReceivingHd = entityReceivingHdDao.Get(entityReceivingHd.ARReceivingID);
                                    entityReceivingHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                                    entityReceivingHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                                    entityReceivingHdDao.Update(entityReceivingHd);
                                }
                                else
                                    entity.IsProcessed = false;
                            }
                            #endregion

                            lstBankData.Add(entity);
                        }
                        i += 2;
                    }
                    #endregion
                }
                else if (bank.GCBankExportDataType == Constant.BankExportDataType.BCA)
                {
                    #region Upload BCA
                    data = ChangeSpace(data);
                    List<String> arrData = data.Split('|').ToList();
                    arrData.RemoveAll(x => x == "");
                    int count = 1;
                    for (int i = 4; i < arrData.Count(); i++) 
                    { 
                        List<String> tempData = arrData[i].Split('_').ToList();
                        tempData.RemoveAll(x => x == "");
                        if (tempData[0] == count.ToString()) 
                        {
                            BankData entity = new BankData();
                            entity.NBS = tempData[1];
                            entity.Amount = Convert.ToDecimal(tempData[6]);

                            Student entityStudent = lstStudent.FirstOrDefault(p => p.VirtualAccountNo == entity.NBS);
                            ProspectiveStudent entityProspectiveStudent = lstProspectiveStudent.FirstOrDefault(p => p.ProspectiveStudentCode == entity.NBS);

                            #region Proses ARReceiving, ARInvoice, ARBalance
                            if (entityStudent != null || entityProspectiveStudent != null)
                            {
                                ARBalance entityARBalance = null;
                                DateTime receivingDate = Convert.ToDateTime(tempData[7]);  
                                if (entityStudent != null)
                                    entityARBalance = lstARBalance.FirstOrDefault(p => p.StudentID == entityStudent.StudentID);

                                if (entityProspectiveStudent != null)
                                    entityARBalance = lstARBalance.FirstOrDefault(p => p.ProspectiveStudentID == entityProspectiveStudent.ProspectiveStudentID);

                                decimal totalAmount = entity.Amount - bank.AdministrationAmount;
                                if (entityARBalance != null)
                                    totalAmount += entityARBalance.DepositAmount;

                                List<vARInvoiceHd> lstARInvoiceHd1 = lstARInvoiceHd.Where(x => x.VirtualAccount == entity.NBS).ToList();
                                if (lstARInvoiceHd1.Sum(p => p.TotalClaimedAmount - p.TotalPaymentAmount) == totalAmount)
                                {
                                    #region ARReceiving
                                    ARReceivingHd entityReceivingHd = new ARReceivingHd();
                                    if (entityStudent != null)
                                        entityReceivingHd.StudentID = entityStudent.StudentID;
                                    else
                                        entityReceivingHd.StudentID = null;

                                    if (entityProspectiveStudent != null)
                                        entityReceivingHd.ProspectiveStudentID = entityProspectiveStudent.ProspectiveStudentID;
                                    else
                                        entityReceivingHd.ProspectiveStudentID = null;
                                    entityReceivingHd.ReceivingDate = receivingDate;
                                    entityReceivingHd.TotalInvoiceAmount = entityReceivingHd.TotalReceivingAmount = totalAmount;
                                    entityReceivingHd.TotalFeeAmount = bank.AdministrationAmount;
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
                                    entityDt.BankID = bank.BankID;
                                    entityDt.PaymentAmount = totalAmount;
                                    entityDt.CardFeeAmount = bank.AdministrationAmount;
                                    entityDt.CreatedBy = AppSession.UserLogin.UserID;
                                    entityReceivingDtDao.Insert(entityDt);
                                    #endregion

                                    #region ARBalance
                                    if (entityARBalance != null && entityARBalance.DepositAmount > 0)
                                    {
                                        ARReceivingDt entityDt2 = new ARReceivingDt();
                                        entityDt2.ARReceivingID = entityReceivingHd.ARReceivingID;
                                        entityDt2.GCARPaymentMethod = Constant.PaymentMethod.DOWN_PAYMENT_RETURN;
                                        if (totalAmount < entityARBalance.DepositAmount)
                                            entityDt2.PaymentAmount = totalAmount;
                                        else
                                            entityDt2.PaymentAmount = entityARBalance.DepositAmount;
                                        entityDt2.CardFeeAmount = 0;
                                        entityDt2.CreatedBy = AppSession.UserLogin.UserID;
                                        entityReceivingDtDao.Insert(entityDt2);

                                        entityARBalance.DepositAmount -= entityDt2.PaymentAmount;
                                        entityARBalance.LastUpdatedBy = AppSession.UserLogin.UserID;
                                        arBalanceDao.Update(entityARBalance);
                                    }
                                    #endregion

                                    #region ARInvoice
                                    decimal totalInvoiceAmount = 0;
                                    foreach (vARInvoiceHd obj in lstARInvoiceHd1)
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
                                            ARInvoiceReceivingObj.ReceivingAmount = aRInvoiceDt.ClaimedAmount - aRInvoiceDt.PaymentAmount;
                                            ARInvoiceReceivingObj.ARInvoiceDtID = aRInvoiceDt.ARInvoiceDtID;
                                            entityIRDao.Insert(ARInvoiceReceivingObj);

                                            aRInvoiceDt.PaymentAmount = aRInvoiceDt.ClaimedAmount;
                                            aRInvoiceDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                                            entityInvoiceDtDao.Update(aRInvoiceDt);
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
                                    #endregion

                                    entityReceivingHd = entityReceivingHdDao.Get(entityReceivingHd.ARReceivingID);
                                    entityReceivingHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                                    entityReceivingHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                                    entityReceivingHdDao.Update(entityReceivingHd);
                                }
                                else
                                    entity.IsProcessed = false;
                            } 
                            else
                            {
                                entity.StudentName = tempData[3];
                                entity.Status = "-";
                            }
                            #endregion

                            lstBankData.Add(entity);
                            count++;
                        }
                    }
                    
                    #endregion
                }
                
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
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
            string errMessage = "";
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
                    String data = GetDataFromFile();
                    UploadFile(data, ref errMessage);
                    BindGridView(1, true, ref pageCount, ref rowCount);
                    result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpErrorMessage"] = errMessage;
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

        private String ChangeSpace(String Data) 
        {
            //String temp = "";
            Data = Data.Replace("\r\n", "|");
            Char[] tempChar = Data.ToCharArray();
            for (int i = 0; i < tempChar.Count(); i++) 
            {
                if ((i > 0 && (tempChar[i - 1] == ' ' || tempChar[i - 1] == '_') && tempChar[i] == ' ') || (i < tempChar.Count() - 1 && tempChar[i + 1] == ' ' && tempChar[i] == ' ')) 
                {
                    tempChar[i] = '_';
                }
            }
            return new String(tempChar);
        }
    }
}