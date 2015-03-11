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
                List<vARInvoiceHd> lstInvoiceHd = BusinessLayer.GetvARInvoiceHdList(String.Format("DueDate BETWEEN '{0}' AND '{1}'", Helper.GetDatePickerValue(txtStartDate.Text), Helper.GetDatePickerValue(txtEndDate.Text)));
                String ProspectiveStudentID = String.Join(",", lstInvoiceHd.GroupBy(x => x.ProspectiveStudentID).Where(x => x.Key != 0).Select(x => x.Key));
                
                List<ProspectiveStudent> lstPS = BusinessLayer.GetProspectiveStudentList(String.Format("ProspectiveStudentID IN ({0})", ProspectiveStudentID));
                //List<Registration> lstReg = BusinessLayer.GetRegistrationList(String.Format("ProspectiveStudentID IN ({0})", ProspectiveStudentID));
                //String RegistrationID = String.Join(",", lstReg.GroupBy(x => x.RegistrationID).Select(x => x.Key));

                String StudentID = String.Join(",", lstInvoiceHd.GroupBy(x => x.StudentID).Where(x => x.Key != 0).Select(x => x.Key));
                List<Student> lstStudent = BusinessLayer.GetStudentList(String.Format("StudentID IN ({0})", StudentID));
                //RegistrationID += "," + String.Join(",", lstStudent.GroupBy(x => x.RegistrationID).Select(x => x.Key));
                
                //List<vRegistrationFeeComp> lstRFC = BusinessLayer.GetvRegistrationFeeCompList(String.Format("RegistrationID IN ({0})", RegistrationID));
                SchoolPeriod Period = BusinessLayer.GetSchoolPeriodList(String.Format("(StartDate <= '{0}' AND EndDate >= '{0}') AND (StartDate <= '{1}' AND EndDate >= '{1}')", Helper.GetDatePickerValue(txtStartDate.Text), Helper.GetDatePickerValue(txtEndDate.Text)))[0];

                List<StudentFeeCompType> sfctList = BusinessLayer.GetStudentFeeCompTypeList(String.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID));

                if (cboBank.Value.ToString() == Constant.BankExportDataType.MANDIRI)
                    format = @"{NBS}|||IDR|{StudentName}|{Class}|{Unit}|{NAUsek}{NAKeg}{NAPemb}|{SchoolPeriod}|{Month}||||||||||||||||||||{StartPeriod}|{EndPeriod}|{NotesUsek}|{NotesKeg}|{NotesPemb}|\\\|\\\|\\\|\\\|\\\|\\\|\\\|\\\|\\\|\\\|\\\|\\\|\\\|\\\|\\\|\\\|\\\|\\\|\\\|\\\|\\\|\\\|~";
                
                foreach (ProspectiveStudent ps in lstPS) 
                {
                    String tempFormat = format;
                    tempFormat = tempFormat.Replace("{NBS}", ps.ProspectiveStudentCode);
                    tempFormat = tempFormat.Replace("{StudentName}", ps.ProspectiveStudentName);
                    tempFormat = tempFormat.Replace("{Month}", cboMonth.Text);
                    tempFormat = tempFormat.Replace("{StartPeriod}", Helper.GetDatePickerValue(txtStartDate.Text).ToString("yyyyMMdd"));
                    tempFormat = tempFormat.Replace("{EndPeriod}", Helper.GetDatePickerValue(txtEndDate.Text).ToString("yyyyMMdd"));
                    tempFormat = tempFormat.Replace("{SchoolPeriod}", String.Format("{0}-{1}", Period.StartDate.Year, Period.EndDate.Year));

                    List<vARInvoiceHd> lstObj = lstInvoiceHd.Where(x => x.ProspectiveStudentID == ps.ProspectiveStudentID).ToList();

                    foreach (StudentFeeCompType obj in sfctList) 
                    {
                        vARInvoiceHd entity = lstObj.FirstOrDefault(x => x.StudentFeeCompTypeID == obj.StudentFeeCompTypeID);
                        string ShortName = obj.ShortName;
                        if (entity != null)
                        {
                            tempFormat = tempFormat.Replace("{Notes" + ShortName + "}", String.Format(@"{0}\{0}\{1}", ShortName, Convert.ToInt32(entity.TotalClaimedAmount)));
                            tempFormat = tempFormat.Replace("{NA" + ShortName + "}", String.Format("{0}{1}", ShortName, Convert.ToInt32(entity.TotalClaimedAmount / 1000)));
                        }
                        else 
                        {
                            tempFormat = tempFormat.Replace("{Notes" + ShortName + "}", @"\\\");
                            tempFormat = tempFormat.Replace("{NA" + ShortName + "}", "");
                        }
                    }
                    
                    txt += String.Format("{0}\n", tempFormat);
                }

                foreach (Student s in lstStudent) 
                {
                    String tempFormat = format;
                    tempFormat = tempFormat.Replace("{NBS}", s.StudentCode);
                    tempFormat = tempFormat.Replace("{StudentName}", s.StudentName);
                    tempFormat = tempFormat.Replace("{Month}", cboMonth.Text);
                    tempFormat = tempFormat.Replace("{StartPeriod}", Helper.GetDatePickerValue(txtStartDate.Text).ToString("yyyyMMdd"));
                    tempFormat = tempFormat.Replace("{EndPeriod}", Helper.GetDatePickerValue(txtEndDate.Text).ToString("yyyyMMdd"));
                    tempFormat = tempFormat.Replace("{SchoolPeriod}", String.Format("{0}-{1}", Period.StartDate.Year, Period.EndDate.Year));

                    List<vARInvoiceHd> lstObj = lstInvoiceHd.Where(x => x.StudentID == s.StudentID).ToList();
                    
                    foreach (StudentFeeCompType obj in sfctList)
                    {
                        vARInvoiceHd entity = lstObj.FirstOrDefault(x => x.StudentFeeCompTypeID == obj.StudentFeeCompTypeID);
                        string ShortName = obj.ShortName;
                        if (entity != null)
                        {
                            tempFormat = tempFormat.Replace("{Notes" + ShortName + "}", String.Format(@"{0}\{0}\{1}", ShortName, Convert.ToInt32(entity.TotalClaimedAmount)));
                            tempFormat = tempFormat.Replace("{NA" + ShortName + "}", String.Format("{0}{1}", ShortName, Convert.ToInt32(entity.TotalClaimedAmount / 1000)));
                        }
                        else
                        {
                            tempFormat = tempFormat.Replace("{Notes" + ShortName + "}", @"\\\");
                            tempFormat = tempFormat.Replace("{NA" + ShortName + "}", "");
                        }
                    }

                    txt += String.Format("{0}\n", tempFormat);
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