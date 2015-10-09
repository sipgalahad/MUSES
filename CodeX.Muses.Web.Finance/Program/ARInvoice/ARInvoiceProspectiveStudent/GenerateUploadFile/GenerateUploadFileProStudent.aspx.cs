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
namespace CodeX.Muses.Web.Finance.Program
{
    public partial class GenerateUploadFileProStudent : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Finance.GENERATE_PROSPECTIVE_STUDENT_UPLOAD_FILE;
        }

        protected override void InitializeDataControl()
        {
            vRegistration reg = BusinessLayer.GetvRegistrationList(string.Format("ProspectiveStudentID = {0}", AppSession.ProspectiveStudentID)).FirstOrDefault();
            hdnSchoolPeriod.Value = BusinessLayer.GetPeriodAdmission(reg.PeriodAdmissionID).SchoolPeriodID.ToString();

            ARBalance entityARBalance = BusinessLayer.GetARBalanceList(String.Format("ProspectiveStudentID = {0} AND IsDeleted = 0", AppSession.ProspectiveStudentID)).FirstOrDefault();
            if (entityARBalance != null)
            {
                hdnDepositAmount.Value = entityARBalance.DepositAmount.ToString();
                txtDepositAmount.Text = entityARBalance.DepositAmount.ToString();
            }
            else
                hdnDepositAmount.Value = "0";

            List<Bank> lstBank = BusinessLayer.GetBankList(String.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField(cboBank, lstBank, "BankName", "BankID");

            cboMonth.DataSource = Enumerable.Range(1, 12).Select(a => new
            {
                MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(a),
                MonthNumber = a
            });
            cboMonth.TextField = "MonthName";
            cboMonth.ValueField = "MonthNumber";
            cboMonth.EnableCallbackMode = false;
            cboMonth.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboMonth.DropDownStyle = DropDownStyle.DropDownList;
            cboMonth.DataBind();
            cboMonth.Value = DateTime.Now.Month.ToString();

            cboYear.DataSource = Enumerable.Range(DateTime.Now.Year - 99, 100).Reverse();
            cboYear.EnableCallbackMode = false;
            cboYear.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboYear.DropDownStyle = DropDownStyle.DropDownList;
            cboYear.DataBind();
            cboYear.SelectedIndex = 0;

            txtStartDate.Text = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtEndDate.Text = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString(Constant.FormatString.DATE_PICKER_FORMAT);

            BindGridView();

            Helper.SetControlEntrySetting(cboBank, new ControlEntrySetting(true, true, true), "mpEntry");
        }
        public void BindGridView() 
        {
            List<vStudentFeeDt> lstEntity = BusinessLayer.GetvStudentFeeDtList(String.Format("ProspectiveStudentID = {0} AND StudentAmount > 0 AND IsPaid = 0", AppSession.ProspectiveStudentID));
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            IDbContext ctx = DbFactory.Configure(true);
            ProspectiveStudentDao prospectiveStudentDao = new ProspectiveStudentDao(ctx);
            SchoolClassDao schoolClassDao = new SchoolClassDao(ctx);
            ARInvoiceDtDao arInvoiceDtDao = new ARInvoiceDtDao(ctx);
            ARInvoiceHdDao arInvoiceHdDao = new ARInvoiceHdDao(ctx);
            SiteParameterDao siteParameterDao = new SiteParameterDao(ctx);
            SchoolPeriodDao schoolPeriodDao = new SchoolPeriodDao(ctx);
            BankDao bankDao = new BankDao(ctx);
            try
            {
                Bank bank = bankDao.Get(Convert.ToInt32(cboBank.Value));
                ProspectiveStudent prospectiveStudent = prospectiveStudentDao.Get(AppSession.ProspectiveStudentID);
                List<vStudentFeeDt> lstStudentFeeDt = BusinessLayer.GetvStudentFeeDtList(String.Format("StudentFeeDtID IN ({0})", hdnSelectedValue.Value), ctx);
                List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsDeleted = 0 AND IsActive = 1", Constant.StandardCode.SCHOOL_TYPE), ctx);

                List<ARInvoiceHd> lstARInvoiceHd = BusinessLayer.GetARInvoiceHdList(string.Format("ProspectiveStudentID = {0} AND GCTransactionStatus != '{1}'", prospectiveStudent.ProspectiveStudentID, Constant.TransactionStatus.VOID), ctx);
                foreach (ARInvoiceHd arInvoiceHD in lstARInvoiceHd)
                {
                    arInvoiceHD.GCTransactionStatus = Constant.TransactionStatus.VOID;
                    arInvoiceHD.LastUpdatedBy = AppSession.UserLogin.UserID;
                    arInvoiceHdDao.Update(arInvoiceHD);
                }

                String schoolType = siteParameterDao.Get(prospectiveStudent.SiteID, Constant.SiteParameter.SCHOOL_TYPE).ParameterValue;

                #region Insert AR Invoice
                string remarks = "";
                foreach (vStudentFeeDt studentFeeDt in lstStudentFeeDt)
                {
                    if (remarks != "")
                        remarks += ", ";
                    remarks += studentFeeDt.cfStudentFeeCompTypeName;
                }

                DateTime DueDate = new DateTime(Convert.ToInt32(cboYear.Value), Convert.ToInt32(cboMonth.Value), 1).AddMonths(1).AddDays(-1);
                Int32 BankID = Convert.ToInt32(cboBank.Value);

                ARInvoiceHd entityARInvoiceHd = new ARInvoiceHd();
                entityARInvoiceHd.TransactionCode = Constant.TransactionCode.AR_INVOICE_PROSPECTIVE_STUDENT;
                entityARInvoiceHd.ARInvoiceDate = DateTime.Now;
                entityARInvoiceHd.BankID = BankID;
                entityARInvoiceHd.ProspectiveStudentID = AppSession.ProspectiveStudentID;
                entityARInvoiceHd.ARInvoiceNo = BusinessLayer.GenerateTransactionNo(entityARInvoiceHd.TransactionCode, DateTime.Now, ctx);
                ctx.CommandType = System.Data.CommandType.Text;
                entityARInvoiceHd.DueDate = DueDate;
                entityARInvoiceHd.Remarks = remarks;
                entityARInvoiceHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                entityARInvoiceHd.CreatedBy = AppSession.UserLogin.UserID;
                arInvoiceHdDao.Insert(entityARInvoiceHd);
                Int32 ARInvoiceHdID = BusinessLayer.GetARInvoiceHdMaxID(ctx);

                foreach (vStudentFeeDt studentFeeDt in lstStudentFeeDt)
                {
                    ARInvoiceDt entityARInvoiceDt = new ARInvoiceDt();
                    entityARInvoiceDt.ARInvoiceID = ARInvoiceHdID;
                    entityARInvoiceDt.StudentFeeDtID = studentFeeDt.StudentFeeDtID;
                    entityARInvoiceDt.StudentFeeCompTypeID = studentFeeDt.StudentFeeCompTypeID;
                    entityARInvoiceDt.ClaimedAmount = entityARInvoiceDt.TransactionAmount = studentFeeDt.StudentAmount;
                    entityARInvoiceDt.DiscountAmount = 0;
                    entityARInvoiceDt.VarianceAmount = null;
                    entityARInvoiceDt.CreatedBy = AppSession.UserLogin.UserID;
                    arInvoiceDtDao.Insert(entityARInvoiceDt);
                }

                entityARInvoiceHd = arInvoiceHdDao.Get(ARInvoiceHdID);
                entityARInvoiceHd.GCTransactionStatus = Constant.TransactionStatus.PROCESSED;
                entityARInvoiceHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                arInvoiceHdDao.Update(entityARInvoiceHd);
                #endregion

                #region Build Text File
                String txt = string.Empty;
                String format = "";
                SchoolPeriod Period = schoolPeriodDao.Get(Convert.ToInt32(hdnSchoolPeriod.Value));
                List<vAdmissionFeeComp> sfctList = BusinessLayer.GetvAdmissionFeeCompList(String.Format("SchoolPeriodID = {0} AND IsDeleted = 0", hdnSchoolPeriod.Value));
                
                if (bank.GCBankExportDataType.ToString() == Constant.BankExportDataType.MANDIRI)
                {
                    format = @"{NBS}|||IDR|{StudentName}|{Class}|{Unit}|{NA1}{NA2}{NA3}{NA4}{NA5}{NA6}{NA7}{NA8}{NA9}{NA10}{NA11}{NA12}{NA13}{NA14}{NA15}{NA16}{NA17}{NA18}{NA19}{NA20}{NA21}{NA22}{NA23}{NA24}{NA25}|{SchoolPeriod}|{Month}||||||||||||||||||||{StartPeriod}|{EndPeriod}|{Notes1}|{Notes2}|{Notes3}|{Notes4}|{Notes5}|{Notes6}|{Notes7}|{Notes8}|{Notes9}|{Notes10}|{Notes11}|{Notes12}|{Notes13}|{Notes14}|{Notes15}|{Notes16}|{Notes17}|{Notes18}|{Notes19}|{Notes20}|{Notes21}|{Notes22}|{Notes23}|{Notes24}|{Notes25}|~";
                    String nbs = "";

                    #region ProspectiveStudent
                    String tempFormat = format;
                    tempFormat = tempFormat.Replace("{NBS}", prospectiveStudent.ProspectiveStudentCode);
                    nbs = prospectiveStudent.ProspectiveStudentCode;
                    tempFormat = tempFormat.Replace("{Class}", "Baru");
                    if (schoolType != "")
                    {
                        StandardCode sc = lstStandardCode.FirstOrDefault(x => x.StandardCodeID == schoolType);
                        tempFormat = tempFormat.Replace("{Unit}", sc.StandardCodeName);
                    }
                    tempFormat = tempFormat.Replace("{StudentName}", prospectiveStudent.ProspectiveStudentName);
                    tempFormat = tempFormat.Replace("{Month}", cboMonth.Text);
                    tempFormat = tempFormat.Replace("{StartPeriod}", Helper.GetDatePickerValue(txtStartDate.Text).ToString("yyyyMMdd"));
                    tempFormat = tempFormat.Replace("{EndPeriod}", Helper.GetDatePickerValue(txtEndDate.Text).ToString("yyyyMMdd"));
                    tempFormat = tempFormat.Replace("{SchoolPeriod}", String.Format("{0}-{1}", Period.StartDate.Year, Period.EndDate.Year));

                    int count = 1;
                    decimal depositAmount = Convert.ToDecimal(hdnDepositAmount.Value);
                    foreach (vAdmissionFeeComp obj in sfctList)
                    {
                        List<vStudentFeeDt> lstStudentFeeDt1 = lstStudentFeeDt.Where(x => x.StudentFeeCompTypeID == obj.StudentFeeCompTypeID).ToList();
                        string ShortName = obj.ShortName;
                        if (lstStudentFeeDt1.Count > 0)
                        {
                            decimal amount = Convert.ToDecimal(lstStudentFeeDt1.Sum(x => x.StudentAmount));

                            if (depositAmount < amount)
                            {
                                amount = amount - depositAmount;
                                depositAmount = 0;

                                tempFormat = tempFormat.Replace("{Notes" + count + "}", String.Format(@"{0}\{1}\{1}\{2}", count.ToString("00"), ShortName, (int)amount));
                                tempFormat = tempFormat.Replace("{NA" + count + "}", String.Format("{0}{1}", ShortName, Convert.ToInt32(amount / 1000)));
                                count++;
                            }
                            else
                                depositAmount -= amount;
                        }

                    }
                    for (; count < 26; count++)
                    {
                        tempFormat = tempFormat.Replace("{Notes" + count + "}", @"\\\");
                        tempFormat = tempFormat.Replace("{NA" + count + "}", "");
                    }
                    txt += String.Format("{0}\n", tempFormat);
                    #endregion

                    ctx.CommitTransaction();

                    #region Download the Text file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", String.Format("attachment;filename={0}_{1}.txt", nbs, DateTime.Now.ToString("yyyyMMdd")));
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(txt);
                    Response.Flush();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                    #endregion
                }
                else if (bank.GCBankExportDataType == Constant.BankExportDataType.BCA)
                {
                    #region Download BCA File
                    String NBS = "";
                    String StudentName = "";
                    String Class = "";
                    String Usek = "-";
                    String Pemb = "-";
                    String Keg = "-";
                    String Penalty = "-";
                    String Admin = bank.AdministrationAmount.ToString();
                    Decimal totalAmount = 0;

                    // Initialize StringWriter instance.
                    StringWriter stringWriter = new StringWriter();
                    // Put HtmlTextWriter in using block because it needs to call Dispose.
                    using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
                    {
                        writer.RenderBeginTag(HtmlTextWriterTag.Html);
                        writer.RenderBeginTag(HtmlTextWriterTag.Body);
                        writer.AddAttribute(HtmlTextWriterAttribute.Border, "1");
                        writer.RenderBeginTag(HtmlTextWriterTag.Table);

                        #region Table Header
                        writer.RenderBeginTag(HtmlTextWriterTag.Thead);
                        writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                        writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
                        writer.RenderBeginTag(HtmlTextWriterTag.Td); writer.Write("No Pelanggan"); writer.RenderEndTag();
                        writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
                        writer.RenderBeginTag(HtmlTextWriterTag.Td); writer.Write("NAMA SISWA"); writer.RenderEndTag();
                        writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
                        writer.RenderBeginTag(HtmlTextWriterTag.Td); writer.Write("KELAS"); writer.RenderEndTag();
                        writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
                        writer.RenderBeginTag(HtmlTextWriterTag.Td); writer.Write("SPP"); writer.RenderEndTag();
                        writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
                        writer.RenderBeginTag(HtmlTextWriterTag.Td); writer.Write("Pembangunan"); writer.RenderEndTag();
                        writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
                        writer.RenderBeginTag(HtmlTextWriterTag.Td); writer.Write("Kegiatan"); writer.RenderEndTag();
                        writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
                        writer.RenderBeginTag(HtmlTextWriterTag.Td); writer.Write("Denda"); writer.RenderEndTag();
                        writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
                        writer.RenderBeginTag(HtmlTextWriterTag.Td); writer.Write("Admin"); writer.RenderEndTag();
                        writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
                        writer.RenderBeginTag(HtmlTextWriterTag.Td); writer.Write("Total"); writer.RenderEndTag();
                        writer.RenderEndTag();//Tr
                        writer.RenderEndTag();//Thead
                        #endregion

                        #region Prospective Student
                        NBS = prospectiveStudent.ProspectiveStudentCode;
                        Class = "Baru";
                        StudentName = prospectiveStudent.ProspectiveStudentName;

                        decimal depositAmount = Convert.ToDecimal(hdnDepositAmount.Value);
                        foreach (vAdmissionFeeComp obj in sfctList)
                        {
                            List<vStudentFeeDt> lstStudentFeeDt1 = lstStudentFeeDt.Where(x => x.StudentFeeCompTypeID == obj.StudentFeeCompTypeID).ToList();
                            string ShortName = obj.ShortName;
                            if (lstStudentFeeDt1.Count > 0)
                            {
                                decimal amount = Convert.ToDecimal(lstStudentFeeDt1.Sum(x => x.StudentAmount));

                                if (depositAmount < amount)
                                {
                                    amount = amount - depositAmount;
                                    depositAmount = 0;

                                    switch (ShortName)
                                    {
                                        case "Usek": Usek = amount.ToString("N"); break;
                                        case "Pemb": Pemb = amount.ToString("N"); break;
                                        case "Keg": Keg = amount.ToString("N"); break;
                                    }
                                    totalAmount += amount;
                                }
                                else
                                    depositAmount -= amount;
                            }
                        }
                        Penalty = "-";
                        totalAmount += bank.AdministrationAmount;
                        SetHtmlRow(writer, NBS, StudentName, Class, Usek, Pemb, Keg, Penalty, Admin, totalAmount);
                        #endregion

                        writer.RenderEndTag();//Table
                        writer.RenderEndTag();//Body
                        writer.RenderEndTag();//HTML

                        ctx.CommitTransaction();

                        string attachment = string.Format("attachment;filename=\"{0}_{1}.xls\"", NBS, DateTime.Now.ToString("yyyyMMdd"));
                        HttpContext.Current.Response.ClearContent();
                        HttpContext.Current.Response.AddHeader("content-disposition", attachment);
                        HttpContext.Current.Response.ContentType = "application/ms-excel";
                        HttpContext.Current.Response.Write(stringWriter.ToString());
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();
                    }
                    #endregion
                }
                    
                #endregion
                

                
            }
            catch (Exception ex)
            {
                String errMessage = ex.Message;
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
            }
            finally 
            {
                ctx.Close();
            }
        }

        private void SetHtmlRow(HtmlTextWriter writer, String NBS, String StudentName, String Class, String Usek, String Pemb, String Keg, String Penalty, String Admin, Decimal Total)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td); writer.Write(NBS); writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td); writer.Write(StudentName); writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td); writer.Write(Class); writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Align, "Right");
            writer.RenderBeginTag(HtmlTextWriterTag.Td); writer.Write(Usek); writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Align, "Right");
            writer.RenderBeginTag(HtmlTextWriterTag.Td); writer.Write(Pemb); writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Align, "Right");
            writer.RenderBeginTag(HtmlTextWriterTag.Td); writer.Write(Keg); writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Align, "Right");
            writer.RenderBeginTag(HtmlTextWriterTag.Td); writer.Write(Penalty); writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Align, "Right");
            writer.RenderBeginTag(HtmlTextWriterTag.Td); writer.Write(Admin); writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Align, "Right");
            writer.RenderBeginTag(HtmlTextWriterTag.Td); writer.Write(Total.ToString("N")); writer.RenderEndTag();
            writer.RenderEndTag();
        }
    }
}