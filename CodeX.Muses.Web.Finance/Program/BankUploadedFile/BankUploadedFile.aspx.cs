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
            ARInvoiceHdDao arInvoiceHdDao = new ARInvoiceHdDao(ctx);
            List<vARInvoiceHd> lstARInvoiceHd = BusinessLayer.GetvARInvoiceHdList(String.Format("GCTransactionStatus = '{0}'", Constant.TransactionStatus.PROCESSED), ctx);
            String studentID = String.Join(",",lstARInvoiceHd.Where(s => s.StudentID != 0).Select(x => x.StudentID).ToList());
            List<Student> lstStudent = null;
            if(studentID != "")lstStudent = BusinessLayer.GetStudentList(String.Format("StudentID IN ({0})", studentID));

            String prospectiveStudentID = String.Join(",", lstARInvoiceHd.Where(s => s.ProspectiveStudentID != 0).Select(x => x.ProspectiveStudentID).ToList());
            List<ProspectiveStudent> lstProspectiveStudent = null;
            if(prospectiveStudentID  != "") lstProspectiveStudent = BusinessLayer.GetProspectiveStudentList(String.Format("ProspectiveStudentID IN ({0})", prospectiveStudentID));
            try
            {
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

                        foreach (vARInvoiceHd obj in lstARInvoiceHd.Where(x => x.VirtualAccount == entity.NBS).ToList())
                        {
                            ARInvoiceHd arInvoiceHD = arInvoiceHdDao.Get(obj.ARInvoiceID);
                            arInvoiceHD.GCTransactionStatus = Constant.TransactionStatus.CLOSED;
                            arInvoiceHD.LastUpdatedBy = AppSession.UserLogin.UserID;
                            if (arInvoiceHD.StudentID != null && arInvoiceHD.StudentID != 0)
                            {
                                String studentName = lstStudent.FirstOrDefault(x => x.StudentID == arInvoiceHD.StudentID).StudentName;
                                entity.StudentName = studentName;
                                entity.Status = "Siswa";
                            }
                            else 
                            {
                                String studentName = lstProspectiveStudent.FirstOrDefault(x => x.ProspectiveStudentID == arInvoiceHD.ProspectiveStudentID).ProspectiveStudentName;
                                entity.StudentName = studentName;
                                entity.Status = "Calon Siswa";
                            }
                            //arInvoiceHdDao.Update(arInvoiceHD);
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