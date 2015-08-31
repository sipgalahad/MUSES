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
    public partial class GenerateUploadFile : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Finance.GENERATE_UPLOAD_FILE;
        }
        
        //String lstSiteID = "";
        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<vSite> lstSite = BusinessLayer.GetvSiteList(String.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsHeader = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<vSite>(cboSite, lstSite, "SiteName", "SiteID");
            cboSite.SelectedIndex = 0;
            hdnSiteID.Value = cboSite.Value.ToString();
            hdnSiteName.Value = cboSite.Text;

            //List<Bank> lstBank = BusinessLayer.GetBankList(String.Format("SiteID IN ({0}) AND IsDeleted = 0", lstSiteID));
            //Methods.SetComboBoxField(cboBank, lstBank, "BankName", "BankID");

            DateTime date = DateTime.Now.AddMonths(1);
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
            cboMonth.Value = date.Month.ToString();

            cboYear.DataSource = Enumerable.Range(DateTime.Now.Year - 99, 100).Reverse();
            cboYear.EnableCallbackMode = false;
            cboYear.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboYear.DropDownStyle = DropDownStyle.DropDownList;
            cboYear.DataBind();
            cboYear.Value = date.Year.ToString();

            //txtStartDate.Text = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            //txtEndDate.Text = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString(Constant.FormatString.DATE_PICKER_FORMAT);
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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            IDbContext ctx = DbFactory.Configure(true);
            ARInvoiceDtDao arInvoiceDtDao = new ARInvoiceDtDao(ctx);
            ARInvoiceHdDao arInvoiceHdDao = new ARInvoiceHdDao(ctx);
            BankDao bankDao = new BankDao(ctx);
            try
            {
                Bank bank = bankDao.Get(Convert.ToInt32(tacBank.Value));
                //Build the Text file data.
                String txt = string.Empty;
                String format = "";

                List<SiteParameter> lstSiteParameter = BusinessLayer.GetSiteParameterList(String.Format("SiteID = '{0}' AND ParameterCode = '{1}'", Request.Form[hdnSiteID.UniqueID], Constant.SiteParameter.SCHOOL_TYPE), ctx);
                List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsDeleted = 0 AND IsActive = 1", Constant.StandardCode.SCHOOL_TYPE), ctx);
                List<vARInvoiceDt> lstvInvoiceDt = BusinessLayer.GetvARInvoiceDtList(String.Format("DueDate <= '{0}' AND GCTransactionStatus IN ('{1}','{2}','{3}') AND SiteID = '{4}'", Helper.GetDatePickerValue(txtEndDate.Text), Constant.TransactionStatus.WAIT_FOR_APPROVAL, Constant.TransactionStatus.APPROVED, Constant.TransactionStatus.PROCESSED, Request.Form[hdnSiteID.UniqueID]), ctx);

                String lstARInvoiceDtID = String.Join(",", lstvInvoiceDt.Select(p => p.ARInvoiceDtID).ToList());
                String lstARInvoiceID = String.Join(",", lstvInvoiceDt.Select(p => p.ARInvoiceID).ToList());
                List<ARInvoiceHd> lstInvoiceHd = null;
                List<ARInvoiceDt> lstInvoiceDt = null;
                if (lstARInvoiceDtID != "")
                {
                    lstInvoiceDt = BusinessLayer.GetARInvoiceDtList(string.Format("ARInvoiceDtID IN ({0})", lstARInvoiceDtID), ctx);
                    lstInvoiceHd = BusinessLayer.GetARInvoiceHdList(string.Format("ARInvoiceID IN ({0})", lstARInvoiceID), ctx);

                    foreach (ARInvoiceHd entityARInvoiceHd in lstInvoiceHd)
                    {
                        if (entityARInvoiceHd.GCTransactionStatus == Constant.TransactionStatus.WAIT_FOR_APPROVAL || entityARInvoiceHd.GCTransactionStatus == Constant.TransactionStatus.APPROVED)
                        {
                            entityARInvoiceHd.GCTransactionStatus = Constant.TransactionStatus.PROCESSED;
                            entityARInvoiceHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                            arInvoiceHdDao.Update(entityARInvoiceHd);
                        }
                    }
                }
                else
                    lstInvoiceDt = new List<ARInvoiceDt>();

                string filterExpressionARBalance = "";
                String lstProspectiveStudentID = String.Join(",", lstvInvoiceDt.Where(p => p.StudentID == 0).GroupBy(x => x.ProspectiveStudentID).Where(x => x.Key != 0).Select(x => x.Key));
                List<ProspectiveStudent> lstProspectiveStudent = null;
                if (lstProspectiveStudentID != "")
                {
                    filterExpressionARBalance = String.Format("ProspectiveStudentID IN ({0})", lstProspectiveStudentID);
                    lstProspectiveStudent = BusinessLayer.GetProspectiveStudentList(String.Format("ProspectiveStudentID IN ({0})", lstProspectiveStudentID));
                }

                String lstStudentID = String.Join(",", lstvInvoiceDt.GroupBy(x => x.StudentID).Where(x => x.Key != 0).Select(x => x.Key));
                List<Student> lstStudent = null;
                List<SchoolClass> lstSchoolClass = null;
                if (lstStudentID != "")
                {
                    if (filterExpressionARBalance != "")
                        filterExpressionARBalance += " AND ";
                    filterExpressionARBalance = String.Format("StudentID IN ({0})", lstStudentID);
                    lstStudent = BusinessLayer.GetStudentList(String.Format("StudentID IN ({0})", lstStudentID));
                    String lstSchooClassID = String.Join(",", lstStudent.Where(p => p.SchoolClassID != null).GroupBy(x => x.SchoolClassID).Where(x => x.Key != 0).Select(x => x.Key));
                    if (lstSchooClassID != "")
                        lstSchoolClass = BusinessLayer.GetSchoolClassList(String.Format("SchoolClassID IN ({0})", lstSchooClassID));
                }

                List<ARBalance> lstARBalance = BusinessLayer.GetARBalanceList(filterExpressionARBalance, ctx);
                SchoolPeriod Period = BusinessLayer.GetSchoolPeriodList(String.Format("(StartDate <= '{0}' AND EndDate >= '{0}') AND (StartDate <= '{1}' AND EndDate >= '{1}') AND SiteID = '{2}'", Helper.GetDatePickerValue(txtStartDate.Text), Helper.GetDatePickerValue(txtEndDate.Text), Request.Form[hdnSiteID.UniqueID]), ctx)[0];
                List<vStudentFeeComp> sfctList = BusinessLayer.GetvStudentFeeCompList(String.Format("SchoolPeriodID = {0} AND IsDeleted = 0", Period.SchoolPeriodID), ctx);

                if (bank.GCBankExportDataType == Constant.BankExportDataType.MANDIRI)
                {
                    format = @"{NBS}|||IDR|{StudentName}|{Class}|{Unit}|{NA1}{NA2}{NA3}{NA4}{NA5}{NA6}{NA7}{NA8}{NA9}{NA10}{NA11}{NA12}{NA13}{NA14}{NA15}{NA16}{NA17}{NA18}{NA19}{NA20}{NA21}{NA22}{NA23}{NA24}{NA25}|{SchoolPeriod}|{Month}||||||||||||||||||||{StartPeriod}|{EndPeriod}|{Notes1}|{Notes2}|{Notes3}|{Notes4}|{Notes5}|{Notes6}|{Notes7}|{Notes8}|{Notes9}|{Notes10}|{Notes11}|{Notes12}|{Notes13}|{Notes14}|{Notes15}|{Notes16}|{Notes17}|{Notes18}|{Notes19}|{Notes20}|{Notes21}|{Notes22}|{Notes23}|{Notes24}|{Notes25}|~";

                    #region ProspectiveStudent
                    if (lstProspectiveStudent != null)
                    {
                        foreach (ProspectiveStudent ps in lstProspectiveStudent)
                        {
                            String tempFormat = format;
                            tempFormat = tempFormat.Replace("{NBS}", ps.ProspectiveStudentCode);
                            tempFormat = tempFormat.Replace("{Class}", "Baru");
                            SiteParameter sp = lstSiteParameter.FirstOrDefault(x => x.SiteID == ps.SiteID);
                            if (sp != null)
                            {
                                StandardCode sc = lstStandardCode.FirstOrDefault(x => x.StandardCodeID == sp.ParameterValue);
                                tempFormat = tempFormat.Replace("{Unit}", sc.StandardCodeName);
                            }
                            tempFormat = tempFormat.Replace("{StudentName}", ps.ProspectiveStudentName);
                            tempFormat = tempFormat.Replace("{Month}", cboMonth.Text);
                            tempFormat = tempFormat.Replace("{StartPeriod}", Helper.GetDatePickerValue(txtStartDate.Text).ToString("yyyyMMdd"));
                            tempFormat = tempFormat.Replace("{EndPeriod}", Helper.GetDatePickerValue(txtEndDate.Text).ToString("yyyyMMdd"));
                            tempFormat = tempFormat.Replace("{SchoolPeriod}", String.Format("{0}-{1}", Period.StartDate.Year, Period.EndDate.Year));

                            List<vARInvoiceDt> lstObj = lstvInvoiceDt.Where(x => x.ProspectiveStudentID == ps.ProspectiveStudentID).ToList();
                            int count = 1;
                            decimal depositAmount = 0;
                            ARBalance entityARBalance = lstARBalance.FirstOrDefault(p => p.ProspectiveStudentID == ps.ProspectiveStudentID);
                            if (entityARBalance != null)
                                depositAmount = entityARBalance.DepositAmount;
                            
                            foreach (vStudentFeeComp obj in sfctList.Where(x => x.ProspectiveStudentID  == ps.ProspectiveStudentID))
                            {
                                List<vARInvoiceDt> lstvARInvoiceDt1 = lstObj.Where(x => x.StudentFeeCompTypeID == obj.StudentFeeCompTypeID).ToList();
                                string ShortName = obj.ShortName;
                                if (lstvARInvoiceDt1.Count > 0)
                                {
                                    decimal amount = Convert.ToDecimal(lstvARInvoiceDt1.Sum(x => x.ClaimedAmount));

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
                            txt += String.Format("{0}{1}", tempFormat, Environment.NewLine);
                        }
                    }
                    #endregion

                    #region Student
                    if (lstStudent != null)
                    {
                        foreach (Student s in lstStudent)
                        {
                            String tempFormat = format;
                            tempFormat = tempFormat.Replace("{NBS}", s.VirtualAccountNo);
                            if (s.SchoolClassID != null)
                            {
                                SchoolClass schoolClass = lstSchoolClass.FirstOrDefault(x => x.SchoolClassID == s.SchoolClassID);
                                tempFormat = tempFormat.Replace("{Class}", schoolClass.SchoolClassName);
                            }
                            else
                            {
                                tempFormat = tempFormat.Replace("{Class}", "Siswa");
                            }
                            SiteParameter sp = lstSiteParameter.FirstOrDefault(x => x.SiteID == s.SiteID);
                            if (sp != null)
                            {
                                StandardCode sc = lstStandardCode.FirstOrDefault(x => x.StandardCodeID == sp.ParameterValue);
                                tempFormat = tempFormat.Replace("{Unit}", sc.StandardCodeName);
                            }
                            tempFormat = tempFormat.Replace("{StudentName}", s.StudentName);
                            tempFormat = tempFormat.Replace("{Month}", cboMonth.Text);
                            tempFormat = tempFormat.Replace("{StartPeriod}", Helper.GetDatePickerValue(txtStartDate.Text).ToString("yyyyMMdd"));
                            tempFormat = tempFormat.Replace("{EndPeriod}", Helper.GetDatePickerValue(txtEndDate.Text).ToString("yyyyMMdd"));
                            tempFormat = tempFormat.Replace("{SchoolPeriod}", String.Format("{0}-{1}", Period.StartDate.Year, Period.EndDate.Year));

                            List<vARInvoiceDt> lstObj = lstvInvoiceDt.Where(x => x.StudentID == s.StudentID).ToList();
                            int count = 1;
                            decimal depositAmount = 0;
                            ARBalance entityARBalance = lstARBalance.FirstOrDefault(p => p.StudentID == s.StudentID);
                            if (entityARBalance != null)
                                depositAmount = entityARBalance.DepositAmount;
                            
                            foreach (vStudentFeeComp obj in sfctList.Where(x => x.StudentID == s.StudentID))
                            {
                                List<vARInvoiceDt> lstvARInvoiceDt1 = lstObj.Where(x => x.StudentFeeCompTypeID == obj.StudentFeeCompTypeID).ToList();
                                string ShortName = obj.ShortName;
                                if (lstvARInvoiceDt1.Count > 0)
                                {
                                    decimal amount = Convert.ToDecimal(lstvARInvoiceDt1.Sum(x => x.ClaimedAmount));

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
                            txt += String.Format("{0}{1}", tempFormat, Environment.NewLine);
                        }
                    }
                    #endregion
                    ctx.CommitTransaction();

                    #region Download the Text file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", string.Format("attachment;filename=TagihanSiswa_{0}_{1}.txt", hdnSiteName.Value.Replace(" ",""), DateTime.Now.ToString("yyyyMMdd")));
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
                    List<Site> lstSite = BusinessLayer.GetSiteList(String.Format("SiteID = {0}", Request.Form[hdnSiteID.UniqueID]), ctx);

                    foreach (Site site in lstSite) 
                    {
                        String unit = "";
                        SiteParameter sp = lstSiteParameter.FirstOrDefault(x => x.SiteID == site.SiteID);
                        if (sp != null)
                        {
                            StandardCode sc = lstStandardCode.FirstOrDefault(x => x.StandardCodeID == sp.ParameterValue);
                            unit = sc.StandardCodeName;
                        }

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

                            #region ProspectiveStudent PerUnit
                            if (lstProspectiveStudent != null)
                            {
                                foreach (ProspectiveStudent ps in lstProspectiveStudent.Where(x => x.SiteID == site.SiteID))
                                {
                                    String NBS = "";
                                    String StudentName = "";
                                    String Class = "";
                                    String Usek = "-";
                                    String Pemb = "-";
                                    String Keg = "-";
                                    String Penalty = "-";
                                    String Admin = bank.AdministrationAmount.ToString();
                                    Decimal totalAmount = 0;

                                    NBS = ps.ProspectiveStudentCode;
                                    Class = "Baru";
                                    StudentName = ps.ProspectiveStudentName;

                                    List<vARInvoiceDt> lstObj = lstvInvoiceDt.Where(x => x.ProspectiveStudentID == ps.ProspectiveStudentID).ToList();

                                    decimal depositAmount = 0;
                                    ARBalance entityARBalance = lstARBalance.FirstOrDefault(p => p.ProspectiveStudentID == ps.ProspectiveStudentID);
                                    if (entityARBalance != null)
                                        depositAmount = entityARBalance.DepositAmount;
                                    decimal TotalPenalty = 0;
                                    
                                    foreach (vStudentFeeComp obj in sfctList.Where(x => x.ProspectiveStudentID == ps.ProspectiveStudentID))
                                    {
                                        List<vARInvoiceDt> lstvARInvoiceDt1 = lstObj.Where(x => x.StudentFeeCompTypeID == obj.StudentFeeCompTypeID).ToList();
                                        string ShortName = obj.ShortName;
                                        if (lstvARInvoiceDt1.Count > 0)
                                        {
                                            foreach (vARInvoiceDt x in lstvARInvoiceDt1)
                                            {
                                                TotalPenalty += x.VarianceAmount;
                                            }
                                            decimal amount = Convert.ToDecimal(lstvARInvoiceDt1.Sum(x => x.ClaimedAmount));

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
                                    if (TotalPenalty > 0) Penalty = TotalPenalty.ToString("N");
                                    totalAmount += bank.AdministrationAmount;
                                    SetHtmlRow(writer, NBS, StudentName, Class, Usek, Pemb, Keg, Penalty, Admin, totalAmount);
                                }
                            }
                            #endregion

                            #region Student PerUnit
                            if (lstStudent != null)
                            {
                                foreach (Student s in lstStudent.Where(x => x.SiteID == site.SiteID))
                                {

                                    String NBS = "";
                                    String StudentName = "";
                                    String Class = "";
                                    String Usek = "-";
                                    String Pemb = "-";
                                    String Keg = "-";
                                    String Penalty = "-";
                                    String Admin = bank.AdministrationAmount.ToString();
                                    Decimal totalAmount = 0;

                                    NBS = s.VirtualAccountNo;
                                    if (s.SchoolClassID != null)
                                    {
                                        SchoolClass schoolClass = lstSchoolClass.FirstOrDefault(x => x.SchoolClassID == s.SchoolClassID);
                                        Class = schoolClass.SchoolClassName;
                                    }
                                    else Class = "Siswa";
                                    StudentName = s.StudentName;

                                    List<vARInvoiceDt> lstObj = lstvInvoiceDt.Where(x => x.StudentID == s.StudentID).ToList();

                                    decimal depositAmount = 0;
                                    ARBalance entityARBalance = lstARBalance.FirstOrDefault(p => p.StudentID == s.StudentID);
                                    if (entityARBalance != null) depositAmount = entityARBalance.DepositAmount;
                                    Decimal TotalPenalty = 0;
                                    
                                    foreach (vStudentFeeComp obj in sfctList.Where(x => x.StudentID == s.StudentID))
                                    {
                                        List<vARInvoiceDt> lstvARInvoiceDt1 = lstObj.Where(x => x.StudentFeeCompTypeID == obj.StudentFeeCompTypeID).ToList();
                                        string ShortName = obj.ShortName;

                                        if (lstvARInvoiceDt1.Count > 0)
                                        {
                                            foreach (vARInvoiceDt x in lstvARInvoiceDt1)
                                            {
                                                TotalPenalty += x.VarianceAmount;
                                            }
                                            decimal amount = Convert.ToDecimal(lstvARInvoiceDt1.Sum(x => x.ClaimedAmount));

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

                                    if (TotalPenalty > 0) Penalty = TotalPenalty.ToString("N");
                                    totalAmount += bank.AdministrationAmount;
                                    SetHtmlRow(writer, NBS, StudentName, Class, Usek, Pemb, Keg, Penalty, Admin, totalAmount);
                                }
                            }

                            #endregion

                            writer.RenderEndTag();//Table
                            writer.RenderEndTag();//Body
                            writer.RenderEndTag();//HTML
                            
                            ctx.CommitTransaction();
                            
                            string attachment = string.Format("attachment;filename=\"TagihanSiswa_{0}_{1}.xls\"", unit, DateTime.Now.ToString("yyyyMMdd"));
                            HttpContext.Current.Response.ClearContent();
                            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
                            HttpContext.Current.Response.ContentType = "application/ms-excel";
                            HttpContext.Current.Response.Write(stringWriter.ToString());
                            HttpContext.Current.Response.Flush();
                            HttpContext.Current.Response.End();
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                String errMessage = ex.Message;
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