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
        
        String lstSiteID = "";
        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<Site> lstSite = BusinessLayer.GetSiteList(String.Format("ParentID = '{0}' OR SiteID = '{0}'", AppSession.UserLogin.SiteID));
            
            foreach(Site obj in lstSite)
            {
                if(lstSiteID != "") lstSiteID += ',';
                lstSiteID += String.Format("'{0}'", obj.SiteID);
            }

            List<Bank> lstBank = BusinessLayer.GetBankList(String.Format("SiteID IN ({0}) AND IsDeleted = 0", lstSiteID));
            Methods.SetComboBoxField(cboBank, lstBank, "BankName", "GCBankExportDataType");

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
            //cboMonth.Value = DateTime.Now.Month.ToString();

            cboYear.DataSource = Enumerable.Range(DateTime.Now.Year - 99, 100).Reverse();
            cboYear.EnableCallbackMode = false;
            cboYear.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboYear.DropDownStyle = DropDownStyle.DropDownList;
            cboYear.DataBind();
            //cboYear.SelectedIndex = 0;

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
            try
            {
                //Build the Text file data.
                String txt = string.Empty;
                String format = "";

                List<SiteParameter> lstSiteParameter = BusinessLayer.GetSiteParameterList(String.Format("SiteID IN ({0}) AND ParameterCode = '{1}'", lstSiteID, Constant.SiteParameter.SCHOOL_TYPE));
                List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsDeleted = 0 AND IsActive = 1", Constant.StandardCode.SCHOOL_TYPE));
                List<vARInvoiceDt> lstInvoiceDt = BusinessLayer.GetvARInvoiceDtList(String.Format("DueDate <= '{0}' AND GCTransactionStatus IN ('{1}','{2}')", Helper.GetDatePickerValue(txtEndDate.Text), Constant.TransactionStatus.PROCESSED, Constant.TransactionStatus.APPROVED));

                String ProspectiveStudentID = String.Join(",", lstInvoiceDt.GroupBy(x => x.ProspectiveStudentID).Where(x => x.Key != 0).Select(x => x.Key));
                List<ProspectiveStudent> lstPS = null;
                if (ProspectiveStudentID != "") lstPS = BusinessLayer.GetProspectiveStudentList(String.Format("ProspectiveStudentID IN ({0})", ProspectiveStudentID));

                String StudentID = String.Join(",", lstInvoiceDt.GroupBy(x => x.StudentID).Where(x => x.Key != 0).Select(x => x.Key));
                List<Student> lstStudent = null;
                List<SchoolClass> lstSchoolClass = null;
                if (StudentID != "")
                {
                    lstStudent = BusinessLayer.GetStudentList(String.Format("StudentID IN ({0})", StudentID));
                    String lstSchooClassID = String.Join(",", lstStudent.GroupBy(x => x.SchoolClassID).Where(x => x.Key != 0).Select(x => x.Key));
                    if (lstSchooClassID != "")
                        lstSchoolClass = BusinessLayer.GetSchoolClassList(String.Format("SchoolClassID IN ({0})", lstSchooClassID));
                }

                SchoolPeriod Period = BusinessLayer.GetSchoolPeriodList(String.Format("(StartDate <= '{0}' AND EndDate >= '{0}') AND (StartDate <= '{1}' AND EndDate >= '{1}')", Helper.GetDatePickerValue(txtStartDate.Text), Helper.GetDatePickerValue(txtEndDate.Text)))[0];

                List<vAdmissionFeeComp> sfctList = BusinessLayer.GetvAdmissionFeeCompList(String.Format("SchoolPeriodID = {0} AND IsDeleted = 0", Period.SchoolPeriodID));

                if (cboBank.Value.ToString() == Constant.BankExportDataType.MANDIRI)
                    format = @"{NBS}|||IDR|{StudentName}|{Class}|{Unit}|{NA1}{NA2}{NA3}{NA4}{NA5}{NA6}{NA7}{NA8}{NA9}{NA10}{NA11}{NA12}{NA13}{NA14}{NA15}{NA16}{NA17}{NA18}{NA19}{NA20}{NA21}{NA22}{NA23}{NA24}{NA25}|{SchoolPeriod}|{Month}||||||||||||||||||||{StartPeriod}|{EndPeriod}|{Notes1}|{Notes2}|{Notes3}|{Notes4}|{Notes5}|{Notes6}|{Notes7}|{Notes8}|{Notes9}|{Notes10}|{Notes11}|{Notes12}|{Notes13}|{Notes14}|{Notes15}|{Notes16}|{Notes17}|{Notes18}|{Notes19}|{Notes20}|{Notes21}|{Notes22}|{Notes23}|{Notes24}|{Notes25}|~";

                #region ProspectiveStudent
                if (lstPS != null)
                {
                    foreach (ProspectiveStudent ps in lstPS)
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

                        List<vARInvoiceDt> lstObj = lstInvoiceDt.Where(x => x.ProspectiveStudentID == ps.ProspectiveStudentID).ToList();
                        int count = 1;
                        foreach (vAdmissionFeeComp obj in sfctList)
                        {
                            List<vARInvoiceDt> entity = lstObj.Where(x => x.AdmissionFeeCompID == obj.AdmissionFeeCompID).ToList();
                            string ShortName = obj.ShortName;
                            if (entity.Count > 0)
                            {
                                foreach (vARInvoiceDt x in entity)
                                {
                                    if (cboMonth.Value.ToString() != x.DueDate.Month.ToString())
                                    {
                                        ARInvoiceDt arinvoicedt = arInvoiceDtDao.Get(x.ARInvoiceDtID);
                                        arinvoicedt.ClaimedAmount = x.ClaimedAmount = (x.TransactionAmount - x.DiscountAmount) * (100 + obj.PenaltyPercentage) / 100;
                                        arinvoicedt.LastUpdatedBy = AppSession.UserLogin.UserID;
                                        arInvoiceDtDao.Update(arinvoicedt);
                                    }
                                }

                                tempFormat = tempFormat.Replace("{Notes" + count + "}", String.Format(@"{0}\{1}\{1}\{2}", count.ToString("00"), ShortName, Convert.ToInt32(entity.Sum(x => x.ClaimedAmount))));
                                tempFormat = tempFormat.Replace("{NA" + count + "}", String.Format("{0}{1}", ShortName, Convert.ToInt32(entity.Sum(x => x.ClaimedAmount) / 1000)));
                            }
                            else
                            {
                                tempFormat = tempFormat.Replace("{Notes" + count + "}", @"\\\");
                                tempFormat = tempFormat.Replace("{NA" + count + "}", "");
                            }
                            count++;
                        }
                        for (; count < 26; count++)
                        {
                            tempFormat = tempFormat.Replace("{Notes" + count + "}", @"\\\");
                            tempFormat = tempFormat.Replace("{NA" + count + "}", "");
                        }
                        txt += String.Format("{0}\n", tempFormat);
                    }
                }
                #endregion

                #region Student
                if (lstStudent != null)
                {
                    foreach (Student s in lstStudent)
                    {
                        String tempFormat = format;
                        tempFormat = tempFormat.Replace("{NBS}", s.StudentCode);
                        SchoolClass schoolClass = lstSchoolClass.FirstOrDefault(x => x.SchoolClassID == s.SchoolClassID);
                        tempFormat = tempFormat.Replace("{Class}", schoolClass.SchoolClassName);
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

                        List<vARInvoiceDt> lstObj = lstInvoiceDt.Where(x => x.StudentID == s.StudentID).ToList();
                        int count = 1;
                        foreach (vAdmissionFeeComp obj in sfctList)
                        {
                            List<vARInvoiceDt> entity = lstObj.Where(x => x.AdmissionFeeCompID == obj.AdmissionFeeCompID).ToList();
                            string ShortName = obj.ShortName;
                            if (entity.Count > 0)
                            {
                                foreach (vARInvoiceDt x in entity)
                                {
                                    if (cboMonth.Value.ToString() != x.DueDate.Month.ToString())
                                    {
                                        ARInvoiceDt arinvoicedt = arInvoiceDtDao.Get(x.ARInvoiceDtID);
                                        arinvoicedt.ClaimedAmount = x.ClaimedAmount = (x.TransactionAmount - x.DiscountAmount) * (100 + obj.PenaltyPercentage) / 100;
                                        arinvoicedt.LastUpdatedBy = AppSession.UserLogin.UserID;
                                        arInvoiceDtDao.Update(arinvoicedt);
                                    }
                                }

                                tempFormat = tempFormat.Replace("{Notes" + count + "}", String.Format(@"{0}\{1}\{1}\{2}", count.ToString("00"), ShortName, Convert.ToInt32(entity.Sum(x => x.ClaimedAmount))));
                                tempFormat = tempFormat.Replace("{NA" + count + "}", String.Format("{0}{1}", ShortName, Convert.ToInt32(entity.Sum(x => x.ClaimedAmount) / 1000)));
                            }
                            else
                            {
                                tempFormat = tempFormat.Replace("{Notes" + count + "}", @"\\\");
                                tempFormat = tempFormat.Replace("{NA" + count + "}", "");
                            }
                        }
                        for (; count < 26; count++)
                        {
                            tempFormat = tempFormat.Replace("{Notes" + count + "}", @"\\\");
                            tempFormat = tempFormat.Replace("{NA" + count + "}", "");
                        }
                        txt += String.Format("{0}\n", tempFormat);
                    }
                }
                #endregion

                ctx.CommitTransaction();

                #region Download the Text file.
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=FileName.txt");
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.Output.Write(txt);
                Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
                #endregion
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
    }
}