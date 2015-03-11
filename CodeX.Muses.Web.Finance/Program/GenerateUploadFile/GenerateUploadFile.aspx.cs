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

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<Bank> lstBank = BusinessLayer.GetBankList(String.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID));
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
            cboMonth.Value = DateTime.Now.Month.ToString();

            cboYear.DataSource = Enumerable.Range(DateTime.Now.Year - 99, 100).Reverse();
            cboYear.EnableCallbackMode = false;
            cboYear.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboYear.DropDownStyle = DropDownStyle.DropDownList;
            cboYear.DataBind();
            cboYear.SelectedIndex = 0;
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
            try
            {
                //Build the Text file data.
                String txt = string.Empty;
                String format = "";
                List<vARInvoiceDt> lstInvoiceDt = BusinessLayer.GetvARInvoiceDtList(String.Format("DueDate BETWEEN '{0}' AND '{1}'", Helper.GetDatePickerValue(txtStartDate.Text), Helper.GetDatePickerValue(txtEndDate.Text)));
                
                String ProspectiveStudentID = String.Join(",", lstInvoiceDt.GroupBy(x => x.ProspectiveStudentID).Where(x => x.Key != 0).Select(x => x.Key));
                List<ProspectiveStudent> lstPS = null;
                if(ProspectiveStudentID != "") lstPS = BusinessLayer.GetProspectiveStudentList(String.Format("ProspectiveStudentID IN ({0})", ProspectiveStudentID));
                
                String StudentID = String.Join(",", lstInvoiceDt.GroupBy(x => x.StudentID).Where(x => x.Key != 0).Select(x => x.Key));
                List<Student> lstStudent = null;
                if(StudentID  != "") lstStudent = BusinessLayer.GetStudentList(String.Format("StudentID IN ({0})", StudentID));
                
                SchoolPeriod Period = BusinessLayer.GetSchoolPeriodList(String.Format("(StartDate <= '{0}' AND EndDate >= '{0}') AND (StartDate <= '{1}' AND EndDate >= '{1}')", Helper.GetDatePickerValue(txtStartDate.Text), Helper.GetDatePickerValue(txtEndDate.Text)))[0];

                List<vAdmissionFeeComp> sfctList = BusinessLayer.GetvAdmissionFeeCompList(String.Format("SchoolPeriodID = {0} AND IsDeleted = 0", Period.SchoolPeriodID));

                if (cboBank.Value.ToString() == Constant.BankExportDataType.MANDIRI)
                    format = @"{NBS}|||IDR|{StudentName}|{Class}|{Unit}|{NA1}{NA2}{NA3}{NA4}{NA5}{NA6}{NA7}{NA8}{NA9}{NA10}{NA11}{NA12}{NA13}{NA14}{NA15}{NA16}{NA17}{NA18}{NA19}{NA20}{NA21}{NA22}{NA23}{NA24}{NA25}|{SchoolPeriod}|{Month}||||||||||||||||||||{StartPeriod}|{EndPeriod}|{Notes1}|{Notes2}|{Notes3}|{Notes4}|{Notes5}|{Notes6}|{Notes7}|{Notes8}|{Notes9}|{Notes10}|{Notes11}|{Notes12}|{Notes13}|{Notes14}|{Notes15}|{Notes16}|{Notes17}|{Notes18}|{Notes19}|{Notes20}|{Notes21}|{Notes22}|{Notes23}|{Notes24}|{Notes25}|~";
                
                if (lstPS != null)
                {
                    foreach (ProspectiveStudent ps in lstPS)
                    {
                        String tempFormat = format;
                        tempFormat = tempFormat.Replace("{NBS}", ps.ProspectiveStudentCode);
                        tempFormat = tempFormat.Replace("{StudentName}", ps.ProspectiveStudentName);
                        tempFormat = tempFormat.Replace("{Month}", cboMonth.Text);
                        tempFormat = tempFormat.Replace("{StartPeriod}", Helper.GetDatePickerValue(txtStartDate.Text).ToString("yyyyMMdd"));
                        tempFormat = tempFormat.Replace("{EndPeriod}", Helper.GetDatePickerValue(txtEndDate.Text).ToString("yyyyMMdd"));
                        tempFormat = tempFormat.Replace("{SchoolPeriod}", String.Format("{0}-{1}", Period.StartDate.Year, Period.EndDate.Year));

                        List<vARInvoiceDt> lstObj = lstInvoiceDt.Where(x => x.ProspectiveStudentID == ps.ProspectiveStudentID).ToList();
                        int count = 1;
                        foreach (vAdmissionFeeComp obj in sfctList)
                        {
                            vARInvoiceDt entity = lstObj.FirstOrDefault(x => x.AdmissionFeeCompID == obj.AdmissionFeeCompID);
                            string ShortName = obj.ShortName;
                            if (entity != null)
                            {
                                tempFormat = tempFormat.Replace("{Notes" + count + "}", String.Format(@"{0}\{1}\{1}\{2}",count.ToString("00"), ShortName, Convert.ToInt32(entity.ClaimedAmount)));
                                tempFormat = tempFormat.Replace("{NA" + count + "}", String.Format("{0}{1}", ShortName, Convert.ToInt32(entity.ClaimedAmount / 1000)));
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

                if (lstStudent != null)
                {
                    foreach (Student s in lstStudent)
                    {
                        String tempFormat = format;
                        tempFormat = tempFormat.Replace("{NBS}", s.StudentCode);
                        tempFormat = tempFormat.Replace("{StudentName}", s.StudentName);
                        tempFormat = tempFormat.Replace("{Month}", cboMonth.Text);
                        tempFormat = tempFormat.Replace("{StartPeriod}", Helper.GetDatePickerValue(txtStartDate.Text).ToString("yyyyMMdd"));
                        tempFormat = tempFormat.Replace("{EndPeriod}", Helper.GetDatePickerValue(txtEndDate.Text).ToString("yyyyMMdd"));
                        tempFormat = tempFormat.Replace("{SchoolPeriod}", String.Format("{0}-{1}", Period.StartDate.Year, Period.EndDate.Year));

                        List<vARInvoiceDt> lstObj = lstInvoiceDt.Where(x => x.StudentID == s.StudentID).ToList();
                        int count = 1;
                        foreach (vAdmissionFeeComp obj in sfctList)
                        {
                            vARInvoiceDt entity = lstObj.FirstOrDefault(x => x.AdmissionFeeCompID == obj.AdmissionFeeCompID);
                            string ShortName = obj.ShortName;
                            if (entity != null)
                            {
                                tempFormat = tempFormat.Replace("{Notes" + count + "}", String.Format(@"{0}\{1}\{1}\{2}",count.ToString("00"), ShortName, Convert.ToInt32(entity.ClaimedAmount)));
                                tempFormat = tempFormat.Replace("{NA" + count + "}", String.Format("{0}{1}", ShortName, Convert.ToInt32(entity.ClaimedAmount / 1000)));
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
                //Download the Text file.
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
            }
            catch (Exception ex) 
            {
                String errMessage = ex.Message;
            }
        }
    }
}