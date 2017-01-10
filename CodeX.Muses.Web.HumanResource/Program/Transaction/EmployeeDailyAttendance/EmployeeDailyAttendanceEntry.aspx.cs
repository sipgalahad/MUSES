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
using System.Globalization;
using CodeX.Data.Core.Dal;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.HumanResource.Program
{
    public partial class EmployeeDailyAttendanceEntry : BasePageList
    {
        protected int PageCount = 0;
        protected int RowCount = 0;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;   

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.HumanResources.EMPLOYEE_DAILY_ATTENDANCE;
        }

        protected string OnGetEmployeeFilterExpression()
        {
            return string.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }
        
        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            txtDate.Text = DateTime.Now.AddDays(-1).ToString(Constant.FormatString.DATE_PICKER_FORMAT);

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            String date = Helper.GetDatePickerValue(txtDate).ToString("yyyyMMdd");
            string filterExpression = String.Format("ScheduleDate  = '{0}'", date);
    
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvEmployeeDailyAttendanceRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            
            lstAttendanceStatus = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0 ", Constant.StandardCode.ATTENDANCE_STATUS));
            List<vEmployeeDailyAttendance> lstEmployeeDailyAttendance = BusinessLayer.GetvEmployeeDailyAttendanceList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "EmployeeName ASC");
            rptView.DataSource = lstEmployeeDailyAttendance;
            rptView.DataBind();
        }

        protected void rptView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vEmployeeDailyAttendance entity = (vEmployeeDailyAttendance)e.Item.DataItem;
                DropDownList ddlAttendanceStatus = (DropDownList)e.Item.FindControl("ddlAttendanceStatus");
                Methods.SetComboBoxField<StandardCode>(ddlAttendanceStatus, lstAttendanceStatus, "StandardCodeName", "StandardCodeID");
                ddlAttendanceStatus.SelectedValue = entity.GCAttendanceStatus;
            }
        }

        List<StandardCode> lstAttendanceStatus = null;
        

        public void UploadFile(String data, ref string errMessage) 
        {
            //bool result = true;
            String filterExpression = OnGetEmployeeFilterExpression();
            IDbContext ctx = DbFactory.Configure(true);
            HRDailyScheduleHdDao entityDailyScheduleHdDao = new HRDailyScheduleHdDao(ctx);
            EmployeeFingerprintLogDao entityFingerPrintDao = new EmployeeFingerprintLogDao(ctx);
            EmployeeDailyAttendanceDao employeeDailyAttendanceDao = new EmployeeDailyAttendanceDao(ctx);
            EmployeeDailyAttendanceRenumerationDao employeeDailyAttendanceRenumerationDao = new EmployeeDailyAttendanceRenumerationDao(ctx);
            try
            {
                
                DateTime date = Helper.GetDatePickerValue(txtDate);

                string[] splitRow = new string[] { "\r\n" };
                string[] lstDataRaw = data.Split(splitRow, StringSplitOptions.RemoveEmptyEntries);
                
                List<Variable> lstData = new List<Variable>();

                foreach (string dataFingerPrint in lstDataRaw)
                {
                    string[] temp = dataFingerPrint.Split('\t');
                    lstData.Add(new Variable { Code = temp[0].Trim(), Value = temp[1] });
                }

                //string lstEmployeeCode = string.Join(",", lstData.Select(p => string.Format("'{0}'", p.Code)).ToList());
                List<vEmployee> lstEmployee = BusinessLayer.GetvEmployeeList(string.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsDeleted = 0", AppSession.UserLogin.SiteID));

                List<Variable> lstSchedule = new List<Variable>();
                foreach (vEmployee tempEmployee in lstEmployee)
                {
                    lstSchedule.Add(new Variable { Code = tempEmployee.WeeklyScheduleID.ToString(), Value = tempEmployee.CurrentTransScheduleID.ToString() });
                }
                
                String strWeeklyJoin = String.Join(",", lstSchedule.Select(p => String.Format("'{0}'", p.Code)).Distinct().ToList());
                List<HRWeeklySchedule> lstWeeklySchedule = BusinessLayer.GetHRWeeklyScheduleList(String.Format("WeeklyScheduleID  IN ({0}) ", strWeeklyJoin));
                
                String dateTemp = Helper.GetDatePickerValue(Request.Form[txtDate.UniqueID]).ToString("yyyy-MM-dd");
                List<vAbsenceProposalEmployeeDate> lstAbsenceProposal = BusinessLayer.GetvAbsenceProposalEmployeeDateList(String.Format(" StartDate <= '{0}' AND EndDate >= '{0}' AND GCTransactionStatus = '{1}' ", dateTemp, Constant.TransactionStatus.APPROVED));
                
                
               
                List<EmployeeFingerprintLog> lstTempEmployeeFinger = new List<EmployeeFingerprintLog>();
                foreach (Variable data1 in lstData)
                {
                    vEmployee emp = lstEmployee.FirstOrDefault(p => p.EmployeeCode == data1.Code);
                    if (emp != null)
                    {
                        //lstTempDataLog.Add(new Variable { Code = emp.EmployeeID.ToString(), Value = data1.Value);
                        EmployeeFingerprintLog entityFingerPrint = new EmployeeFingerprintLog();
                        entityFingerPrint.EmployeeID = Convert.ToInt32(emp.EmployeeID);
                        entityFingerPrint.LogDateTime = Convert.ToDateTime(data1.Value);
                        entityFingerPrintDao.Insert(entityFingerPrint);
                        lstTempEmployeeFinger.Add(entityFingerPrint);
                    }
                }

                int dayOfWeek = 0;

                foreach (vEmployee employee in lstEmployee)
                {
                    dayOfWeek = (int)date.DayOfWeek;
                    EmployeeFingerprintLog employeeFingerPrint = lstTempEmployeeFinger.FirstOrDefault(p => p.EmployeeID == employee.EmployeeID);
                    HRWeeklySchedule weeklySchedule = lstWeeklySchedule.FirstOrDefault(p => p.WeeklyScheduleID == employee.WeeklyScheduleID);
                    //HRWeeklySchedule weeklySchedule = lstWeeklySchedule.FirstOrDefault(p => p.WeeklyScheduleID == employee.WeeklyScheduleID && employee.EmployeeID == employeeFingerPrint.EmployeeID);
                    int? WeeklyScheduleID = null;
                    WeeklyScheduleID = employee.WeeklyScheduleID;

                    int? DailyScheduleID = null;
                    if (WeeklyScheduleID != 0)
                    {
                        switch (dayOfWeek)
                        {
                            case 1: DailyScheduleID = weeklySchedule.DailyScheduleID1; break;
                            case 2: DailyScheduleID = weeklySchedule.DailyScheduleID2; break;
                            case 3: DailyScheduleID = weeklySchedule.DailyScheduleID3; break;
                            case 4: DailyScheduleID = weeklySchedule.DailyScheduleID4; break;
                            case 5: DailyScheduleID = weeklySchedule.DailyScheduleID5; break;
                            case 6: DailyScheduleID = weeklySchedule.DailyScheduleID6; break;
                            case 7: DailyScheduleID = weeklySchedule.DailyScheduleID7; break;
                        }
                    }

                    if (DailyScheduleID != null && employeeFingerPrint == null)
                    {
                        EmployeeDailyAttendance eda = new EmployeeDailyAttendance();
                        eda.EmployeeID = employee.EmployeeID;
                        eda.ScheduleDate = date;

                        HRDailyScheduleHd daily = entityDailyScheduleHdDao.Get((int)DailyScheduleID);
                        eda.ScheduleStartTime = daily.FromHour;
                        eda.ScheduleEndTime = daily.ToHour;
                        eda.ScheduleNoOfWorkTimeHour = daily.NoOfWorkHours;
                        //eda.NoOfWorkTimeHour = daily.NoOfWorkHours;
                        eda.DailyRenumerationMultiplyBy = 1;
                        eda.GCAttendanceStatus = Constant.EmployeeAttendanceStatus.ALPA;

                        employeeDailyAttendanceDao.Insert(eda);
                    }
                    else if (employeeFingerPrint != null && employeeFingerPrint.LogDateTime.Date == date.Date)
                    {
                        EmployeeDailyAttendance eda = new EmployeeDailyAttendance();
                        eda.EmployeeID = employee.EmployeeID;
                        eda.ScheduleDate = date;
                        eda.ScheduleStartTime = "";
                        eda.ScheduleEndTime = "";
                        eda.DailyRenumerationMultiplyBy = 1;
                        
                    // Kalo ada di jadwal
                        if (DailyScheduleID != null)
                        {
                            HRDailyScheduleHd daily = entityDailyScheduleHdDao.Get((int)DailyScheduleID);
                            // Isi Attendance (schedule start time dan end time nya)
                           
                            //eda.EmployeeID = employee.EmployeeID;
                            eda.ScheduleStartTime = daily.FromHour;
                            eda.ScheduleEndTime = daily.ToHour;
                            eda.ScheduleNoOfWorkTimeHour = daily.NoOfWorkHours;
                            eda.NoOfWorkTimeHour = daily.NoOfWorkHours;

                            List<EmployeeFingerprintLog> lstFingerPrint = lstTempEmployeeFinger.Where(p => p.EmployeeID == employee.EmployeeID).ToList();
                            if (lstFingerPrint.Count > 0)
                            {
                                // Hadir => set status hadir, lalu cek dari lstFingerprint utk realisasi jam masuk dan keluar
                                eda.GCAttendanceStatus = Constant.EmployeeAttendanceStatus.HADIR;
                                EmployeeFingerprintLog arrive = lstFingerPrint.Where(p => String.Compare(p.LogDateTime.ToString("HH:mm"), daily.StartGraceTimeArrive) >= 0 && String.Compare(p.LogDateTime.ToString("HH:mm"), daily.EndGraceTimeArrive) <= 0).OrderBy(p => p.LogDateTime).FirstOrDefault();
                                if (arrive != null)
                                    eda.StartTime = arrive.LogDateTime.ToString("HH:mm");
                                EmployeeFingerprintLog depart = lstFingerPrint.Where(p => String.Compare(p.LogDateTime.ToString("HH:mm"), daily.StartGraceTimeDepart) >= 0 && String.Compare(p.LogDateTime.ToString("HH:mm"), daily.ToGraceTimeDepart) <= 0).OrderByDescending(p => p.LogDateTime).FirstOrDefault();
                                if (depart != null)
                                    eda.EndTime = depart.LogDateTime.ToString("HH:mm");

                                List<vOvertimeProposalEmployeeDate> lstOvertimeProposal = BusinessLayer.GetvOvertimeProposalEmployeeDateList(String.Format("OvertimeDate = '{0}' AND GCTransactionStatus = '{1}'", Helper.GetDatePickerValue(Request.Form[txtDate.UniqueID]), Constant.TransactionStatus.APPROVED));
                                vOvertimeProposalEmployeeDate tempOvertimeProposal = lstOvertimeProposal.FirstOrDefault(p => p.EmployeeID == employee.EmployeeID);
                                if (tempOvertimeProposal != null)
                                {
                                    EmployeeFingerprintLog departOvertime = lstFingerPrint.Where(p => String.Compare(p.LogDateTime.ToString("HH:mm"), tempOvertimeProposal.StartTime) >= 0 && String.Compare(p.LogDateTime.ToString("HH:mm"), tempOvertimeProposal.EndTime) <= 0).OrderByDescending(p => p.LogDateTime).FirstOrDefault();
                                    eda.OvertimeProposalStartTime = tempOvertimeProposal.StartTime;
                                    eda.OvertimeProposalEndTime = tempOvertimeProposal.EndTime;
                                    eda.OvertimeProposalTotalHour = tempOvertimeProposal.TotalHours.ToString();
                                    if(departOvertime != null)
                                        eda.EndTime = departOvertime.LogDateTime.ToString("HH:mm");

                                    EmployeeDailyAttendanceRenumeration edar = new EmployeeDailyAttendanceRenumeration();
                                    vEmployeeRenumeration employeeRenumeration = BusinessLayer.GetvEmployeeRenumerationList(String.Format("EmployeeID = {0} AND GCRenumerationCompType = '{1}' ", employee.EmployeeID, Constant.RenumerationCompType.OVERTIME), ctx).FirstOrDefault();
                                    edar.EmployeeID = eda.EmployeeID;
                                    edar.ScheduleDate = date;
                                    edar.ScheduleStartTime = eda.OvertimeProposalStartTime;
                                    edar.RenumerationCompID = employeeRenumeration.RenumerationCompID;
                                    Decimal tempTotal = GetTotalAmount(employee.EmployeeID, employeeRenumeration.FromRenumerationCompID, tempOvertimeProposal.TotalHours, ctx);
                                    edar.TotalAmount = tempTotal;

                                    employeeDailyAttendanceRenumerationDao.Insert(edar);
                                }
                            }
                            else
                            {
                                // Tidak hadir (cek ke absence, kalo ada statusnya dari situ, kalo ga ada alpha)
                                vAbsenceProposalEmployeeDate tempAbsenceProposal = lstAbsenceProposal.FirstOrDefault(p => p.EmployeeID == employee.EmployeeID);
                                if (tempAbsenceProposal == null)
                                    eda.GCAttendanceStatus = Constant.EmployeeAttendanceStatus.ALPA;
                                else
                                    eda.GCAttendanceStatus = tempAbsenceProposal.GCAttendanceStatus;
                            }
                        }

                        // Kalo ga ada di jadwal, cek ke finger print. kalo ada cek lembur. Kalo ga ada ga usah diapa2in
                        else
                        {
                        }

                        employeeDailyAttendanceDao.Insert(eda);
                    }
                }
                
               
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                //result = false;
                errMessage = ex.Message;
                ctx.RollBackTransaction();
                
            }
            finally
            {
                ctx.Close();
            }
            //return result;
            //List<Variable> lstData = new List<Variable>();
            //lstData.Add(new Variable { Code = "10920123", Value = "2016-12" });
            //lstData.Add(new Variable { Code = "10920124", Value = "2016-12" });
            //string lstEmployeeCode = string.Join(",", lstData.Select(p => string.Format("'{0}'", p.Code)).ToList());
            //'10920123','10920124'
            //List<Employee> lstEmployee = BusinessLayer.GetEmployeeList(string.Format("EmployeeCode IN ({0})", lstEmployeeCode));

            // Insert ke fingerprintlog

            // Loop Semua Employee
            // Get Jadwal
            // Ambil Jam masuk dan jam keluar dari fingerprint
            // masukin ke daily attendence
            
            // Ambil Proposal Lembur
            // Ambil Proposal Ketidakhadiran. Kalo tidak ada di fingerprint dan ada jdwl, default alpha. kecuali ada di proposal
        }

        

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            String data = GetDataFromFile();

            //int adjustmentID = 0;
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                //adjustmentID = Convert.ToInt32(hdnTransactionID.Value);
                if (OnSaveEditRecordEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            else if (param[0] == "saveFile")
            {
                UploadFile(data, ref errMessage);
            }
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpErrorMessage"] = errMessage;
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            EmployeeDailyAttendanceDao entityDao = new EmployeeDailyAttendanceDao(ctx);
            EmployeeDailyAttendanceRenumerationDao employeeDailyAttendanceRenumerationDao = new EmployeeDailyAttendanceRenumerationDao(ctx);
            DateTime date = Helper.GetDatePickerValue(txtDate);
            try
            {
                EmployeeDailyAttendance entity = entityDao.Get(Convert.ToInt32(hdnID.Value), date, hdnScheduleStart.Value.ToString());

                //entity.ScheduleStartTime = hdnScheduleStart.Value.ToString();
                entity.ScheduleEndTime = hdnScheduleEnd.Value.ToString();
                entity.StartTime = hdnStartTime.Value.ToString();
                entity.EndTime = hdnEndTime.Value.ToString();
                entity.NoOfWorkTimeHour = Convert.ToDecimal(hdnNoOfWorkTimeHour.Value);
                entity.DailyRenumerationMultiplyBy = Convert.ToDecimal(hdnDailyRenumerationMultiplyBy.Value);
                entity.OvertimeProposalStartTime = hdnOvertimeProposalStartTime.Value.ToString();
                entity.OvertimeProposalEndTime = hdnOvertimeProposalEndTime.Value.ToString();
                entity.OvertimeProposalTotalHour = hdnOvertimeProposalTotalHour.Value.ToString();
                entity.NoOfOvertimeHour = Convert.ToDecimal(hdnNoOfOvertimeHour.Value.ToString());
                entity.GCAttendanceStatus = hdnStatus.Value.ToString();
                entityDao.Update(entity);


                if (Convert.ToDecimal(hdnNoOfOvertimeHour.Value.ToString()) != 0 && hdnNoOfOvertimeHour.Value.ToString() != null)
                {
                    vEmployeeRenumeration employeeRenumeration = BusinessLayer.GetvEmployeeRenumerationList(String.Format("EmployeeID = {0} AND GCRenumerationCompType = '{1}' ", entity.EmployeeID, Constant.RenumerationCompType.OVERTIME), ctx).FirstOrDefault();
                    EmployeeDailyAttendanceRenumeration entityEmployeeDaily = employeeDailyAttendanceRenumerationDao.Get(Convert.ToInt32(hdnID.Value), date, hdnOvertimeProposalStartTime.Value.ToString(), employeeRenumeration.RenumerationCompID);
                    entityEmployeeDaily.TotalAmount = GetTotalAmount(entityEmployeeDaily.EmployeeID, employeeRenumeration.FromRenumerationCompID, Convert.ToDecimal(hdnNoOfOvertimeHour.Value), ctx);
                    employeeDailyAttendanceRenumerationDao.Update(entityEmployeeDaily);
                }
                else 
                {
                    BusinessLayer.DeleteEmployeeDailyAttendanceRenumeration(Convert.ToInt32(hdnID.Value), date, hdnOvertimeProposalStartTime.Value.ToString(), 1);
                }
          
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

        public Decimal GetTotalAmount(int employeeId, int renumerationCompId, decimal totalHour, IDbContext ctx)
        {
            vEmployeeRenumeration employeeRenumeration = BusinessLayer.GetvEmployeeRenumerationList(String.Format("EmployeeID = {0} AND RenumerationCompID = {1} ", employeeId, renumerationCompId),ctx).FirstOrDefault();
            decimal totalAmount = totalHour * (employeeRenumeration.Amount / 72);

            return totalAmount;
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
    }
}