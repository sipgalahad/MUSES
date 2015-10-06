using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using System.Data;
using CodeX.Data.Core.Dal;
using CodeX.Common;
using DevExpress.Web.ASPxCallbackPanel;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class AdmissionFeeEntry1 : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.PA_ADMISSION_FEE;
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        protected string OnGetRegistrationFilterExpression()
        {
            return string.Format("PeriodAdmissionID = {0} AND GCRegistrationStatus IN ('{1}','{2}')", AppSession.PeriodAdmissionID, Constant.RegistrationStatus.ACCEPTED, Constant.RegistrationStatus.AR_PROCESSED);
        }
        protected string OnGetAdmissionFeeRuleFilterExpression()
        {
            return string.Format("SchoolPeriodID = {0} AND IsDeleted = 0", hdnSchoolPeriodID.Value);
        }
        protected string OnGetRegistrationStatusAccepted()
        {
            return Constant.RegistrationStatus.ACCEPTED;
        }
        protected string OnGetAdmissionFeeRuleFeederFilterExpression()
        {
            return string.Format(" AND (GCFromSchoolType IS NULL OR GCFromSchoolType = '{0}')", Constant.FromSchoolType.FEEDER);
        }
        protected string OnGetAdmissionFeeRuleNonFeederFilterExpression()
        {
            return string.Format(" AND (GCFromSchoolType IS NULL OR GCFromSchoolType = '{0}')", Constant.FromSchoolType.NON_FEEDER);
        }

        protected override void InitializeDataControl()
        {
            PeriodAdmission entity = BusinessLayer.GetPeriodAdmission(AppSession.PeriodAdmissionID);
            hdnSchoolPeriodID.Value = entity.SchoolPeriodID.ToString();

            SchoolPeriod entitySchoolPeriod = BusinessLayer.GetSchoolPeriod(entity.SchoolPeriodID);
            hdnYear.Value = entitySchoolPeriod.StartDate.Year.ToString();
            hdnMonth.Value = entitySchoolPeriod.StartDate.Month.ToString();
            List<AdmissionPaymentHd> lstPayment = BusinessLayer.GetAdmissionPaymentHdList(string.Format("SchoolPeriodID = {0} AND IsDeleted = 0", hdnSchoolPeriodID.Value));
            Methods.SetComboBoxField<AdmissionPaymentHd>(cboPaymentType, lstPayment, "PaymentName", "PaymentID");

            Helper.SetControlEntrySetting(tacRegistration, new ControlEntrySetting(true, true, true), "mpFilterGenerate");
            Helper.SetControlEntrySetting(tacAdmissionFeeRule, new ControlEntrySetting(true, true, true), "mpFilterGenerate");
            Helper.SetControlEntrySetting(cboPaymentType, new ControlEntrySetting(true, true, true), "mpFilterGenerate");
        }

        List<AdmissionPaymentDt> lstPaymentDt = null;
        List<vStudentFee> lstStudentFee = null;
        List<vStudentFeeDt> lstStudentFeeDt = null;
        List<ScholarshipComp> lstScholarshipComp = null;
        List<RegistrationScholarship> lstRegistrationScholarshipFee = null;
        protected void cbpScholarship_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string filterExpression = string.Format("SchoolPeriodID = {0} AND GCScholarshipType = '{1}' AND ScholarshipID IN (SELECT ScholarshipID FROM ScholarshipPeriodAdmission WHERE PeriodAdmissionID = {2})", hdnSchoolPeriodID.Value, Constant.ScholarshipType.ADMISSION, AppSession.PeriodAdmissionID);
            if (hdnIsFeeder.Value == "1")
                filterExpression += string.Format(" AND (GCFromSchoolType IS NULL OR GCFromSchoolType = '{0}')", Constant.FromSchoolType.FEEDER);
            else
                filterExpression += string.Format(" AND (GCFromSchoolType IS NULL OR GCFromSchoolType = '{0}')", Constant.FromSchoolType.NON_FEEDER);
            List<Scholarship> lstScholarship = BusinessLayer.GetScholarshipList(filterExpression);
            lstRegistrationScholarshipFee = BusinessLayer.GetRegistrationScholarshipList(String.Format("RegistrationID = {0}", tacRegistration.Value));

            ASPxCallbackPanel cbpScholarship = (ASPxCallbackPanel)sender;
            GridView grdScholarship = (GridView)cbpScholarship.FindControl("grdScholarship");
            grdScholarship.DataSource = lstScholarship;
            grdScholarship.DataBind();
        }

        protected void grdScholarship_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Scholarship entity = (Scholarship)e.Row.DataItem;
                RegistrationScholarship registrationScholarshipFee = lstRegistrationScholarshipFee.FirstOrDefault(p => p.ScholarshipID == entity.ScholarshipID);
                if (registrationScholarshipFee != null)
                {
                    CheckBox chkIsSelected = (CheckBox)e.Row.FindControl("chkIsSelected");
                    chkIsSelected.Checked = true;
                }
            }
        }

        bool isLoadRegistration = false;
        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] temp = e.Parameter.Split('|');
                isLoadRegistration = temp[1] == "1";

                lstPaymentDt = BusinessLayer.GetAdmissionPaymentDtList(string.Format("PaymentID = {0}", cboPaymentType.Value));
                lstStudentFee = BusinessLayer.GetvStudentFeeList(String.Format("ProspectiveStudentID = {0} AND IsDeleted = 0", hdnProspectiveStudentID.Value));
                lstStudentFeeDt = BusinessLayer.GetvStudentFeeDtList(String.Format("ProspectiveStudentID = {0} AND IsDeleted = 0", hdnProspectiveStudentID.Value));
                if (hdnLstScholarshipID.Value != "")
                    lstScholarshipComp = BusinessLayer.GetScholarshipCompList(string.Format("ScholarshipID IN ({0}) AND DiscountAmount > 0", hdnLstScholarshipID.Value));
                else
                    lstScholarshipComp = new List<ScholarshipComp>();

                List<StudentFeeComp> lstStudentFeeComp = BusinessLayer.GetStudentFeeCompList(String.Format("ProspectiveStudentID = {0} AND IsDeleted = 0", hdnProspectiveStudentID.Value));
                List<vAdmissionFeeRuleDtCustom> lstEntity = BusinessLayer.GetvAdmissionFeeRuleDtCustomList(string.Format("SchoolPeriodID = {0} AND (IsFixedAmount = 1 OR (IsFixedAmount = 0 AND PeriodAdmissionID = {1} AND AdmissionFeeRuleID = {2})) AND IsDeleted = 0", hdnSchoolPeriodID.Value, AppSession.PeriodAdmissionID, tacAdmissionFeeRule.Value));
                foreach (StudentFeeComp studentFeeComp in lstStudentFeeComp)
                {
                    vAdmissionFeeRuleDtCustom entityDtCustom = lstEntity.FirstOrDefault(p => p.StudentFeeCompTypeID == studentFeeComp.StudentFeeCompTypeID);
                    if (entityDtCustom != null)
                        entityDtCustom.TotalAmount = studentFeeComp.TotalAmount;
                }
                rptAdmissionComp.DataSource = lstEntity;
                rptAdmissionComp.DataBind();
            }
        }

        protected void rptAdmissionComp_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vAdmissionFeeRuleDtCustom entity = (vAdmissionFeeRuleDtCustom)e.Item.DataItem;

                if (entity.NoOfRegistrationPaymentPeriod == 0)
                {
                    HtmlGenericControl containerTableFee = (HtmlGenericControl)e.Item.FindControl("containerTableFee");
                    containerTableFee.Style.Add("display", "none");
                }

                Repeater rptViewDt = (Repeater)e.Item.FindControl("rptViewDt");

                ScholarshipComp entityScholarshipComp = lstScholarshipComp.FirstOrDefault(p => p.StudentFeeCompTypeID == entity.StudentFeeCompTypeID);

                List<vStudentFeeDt> lstStudentFeeDt1 = lstStudentFeeDt.Where(p => p.StudentFeeCompTypeID == entity.StudentFeeCompTypeID).ToList();

                List<vStudentFeeDt> lstEntity = new List<vStudentFeeDt>();
                List<AdmissionPaymentDt> lstPaymentDt1 = lstPaymentDt.Where(p => p.AdmissionFeeCompID == entity.AdmissionFeeCompID).ToList();
                short ctr = 1;

                decimal totalDiscountAmount = 0;
                foreach (AdmissionPaymentDt paymentDt in lstPaymentDt1)
                {
                    decimal totalPayment = 0;
                    decimal totalPaymentInPercentage = 0;
                    if (paymentDt.IsPaymentAmountInPercentage)
                    {
                        totalPayment = entity.TotalPaymentAmount * paymentDt.PaymentAmount / 100;
                        totalPaymentInPercentage = paymentDt.PaymentAmount;
                    }
                    else
                    {
                        totalPayment = paymentDt.PaymentAmount;
                        totalPaymentInPercentage = paymentDt.PaymentAmount * 100 / entity.TotalPaymentAmount;
                    }
                    totalPayment = totalPayment / paymentDt.NoOfPayment;
                    totalPaymentInPercentage = totalPaymentInPercentage / paymentDt.NoOfPayment;
                    for (int i = 0; i < paymentDt.NoOfPayment; ++i)
                    {
                        decimal tempDiscountAmount = 0;
                        vStudentFeeDt entityDt = lstStudentFeeDt1.FirstOrDefault(p => p.DisplayOrder == ctr);
                        if (entityDt == null)
                        {
                            entityDt = new vStudentFeeDt();
                            if (paymentDt.PaymentDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT) == Constant.ConstantDate.DEFAULT_NULL)
                                entityDt.DueDate = DateTime.Now;
                            else
                                entityDt.DueDate = paymentDt.PaymentDate;
                            entityDt.LineAmount = totalPayment;
                            entityDt.TransactionAmount = totalPaymentInPercentage;
                            entityDt.DisplayOrder = ctr;
                        }
                        if (!isLoadRegistration && entityScholarshipComp != null)
                        {
                            if (entityScholarshipComp.IsDiscountInPercentage)
                            {
                                if (entityScholarshipComp.NoOfPeriod <= entity.NoOfRegistrationPaymentPeriod)
                                {
                                    Decimal DiscountAmount = entityScholarshipComp.DiscountAmount * entityScholarshipComp.NoOfPeriod / entity.NoOfRegistrationPaymentPeriod;
                                    tempDiscountAmount = DiscountAmount * entityDt.LineAmount / 100;
                                }
                                else
                                    tempDiscountAmount = entityScholarshipComp.DiscountAmount * entityDt.LineAmount / 100;
                            }
                            else
                            {
                                if (entityScholarshipComp.NoOfPeriod <= entity.NoOfRegistrationPaymentPeriod)
                                    tempDiscountAmount = entityScholarshipComp.DiscountAmount * entityScholarshipComp.NoOfPeriod;
                                else
                                {
                                    Decimal DiscountAmount = entityScholarshipComp.DiscountAmount * entityScholarshipComp.NoOfPeriod / entity.NoOfRegistrationPaymentPeriod;
                                    tempDiscountAmount = DiscountAmount;
                                }
                            }
                        }
                        totalDiscountAmount += tempDiscountAmount;
                        entityDt.LineAmount -= tempDiscountAmount;
                        lstEntity.Add(entityDt);
                        ctr++;
                    }
                }
                if (isLoadRegistration)
                {
                    vStudentFee studentFee = lstStudentFee.FirstOrDefault(p => p.StudentFeeCompTypeID == entity.StudentFeeCompTypeID);
                    if (studentFee != null)
                        totalDiscountAmount = studentFee.TotalDiscountAmount;
                }
                TextBox txtTotalDiscountAmount = (TextBox)e.Item.FindControl("txtTotalDiscountAmount");
                txtTotalDiscountAmount.Text = totalDiscountAmount.ToString();
                TextBox txtAdmissionFeeCompTransactionAmount = (TextBox)e.Item.FindControl("txtAdmissionFeeCompTransactionAmount");
                txtAdmissionFeeCompTransactionAmount.Text = (entity.TotalPaymentAmount - totalDiscountAmount).ToString();

                TextBox txtDiscountPercentage = (TextBox)e.Item.FindControl("txtDiscountPercentage");
                if (entity.TotalPaymentAmount != 0)
                    txtDiscountPercentage.Text = (totalDiscountAmount / entity.TotalPaymentAmount * 100).ToString("#,##0.00");
                else
                    txtDiscountPercentage.Text = 0.ToString("#,##0.00");

                rptViewDt.DataSource = lstEntity;
                rptViewDt.DataBind();
            }
        }

        private string RegistrationNo = "";
        private void OnSaveRecord(IDbContext ctx, int registrationID, bool isApproved)
        {
            RegistrationDao entityDao = new RegistrationDao(ctx);
            StudentFeeDao entityFeeDao = new StudentFeeDao(ctx);
            StudentFeeDtDao entityFeeDtDao = new StudentFeeDtDao(ctx);
            StudentFeeCompDao entityFeeCompDao = new StudentFeeCompDao(ctx);
            RegistrationScholarshipDao entityScholarshipDao = new RegistrationScholarshipDao(ctx);
            
            Registration entity = entityDao.Get(registrationID);
            RegistrationNo = entity.RegistrationNo;
            entity.AdmissionFeeRuleID = Convert.ToInt32(tacAdmissionFeeRule.Value);
            entity.PaymentID = Convert.ToInt32(cboPaymentType.Value);
            if (isApproved)
                entity.GCRegistrationStatus = Constant.RegistrationStatus.AR_PROCESSED;
            entity.LastUpdatedBy = AppSession.UserLogin.UserID;
            entityDao.Update(entity);

            List<RegistrationScholarship> lstRegistrationScholarshipFee = BusinessLayer.GetRegistrationScholarshipList(String.Format("RegistrationID = {0}", tacRegistration.Value), ctx);
            if (hdnLstScholarshipID.Value != "")
            {
                string[] lstSaveScholarship = hdnLstScholarshipID.Value.Split(',');
                foreach (string saveValue in lstSaveScholarship)
                {
                    int scholarshipID = Convert.ToInt32(saveValue);
                    RegistrationScholarship registrationScholarshipFee = lstRegistrationScholarshipFee.FirstOrDefault(p => p.ScholarshipID == scholarshipID);
                    if (registrationScholarshipFee == null)
                    {
                        registrationScholarshipFee = new RegistrationScholarship();
                        registrationScholarshipFee.RegistrationID = entity.RegistrationID;
                        registrationScholarshipFee.ScholarshipID = scholarshipID;
                        entityScholarshipDao.Insert(registrationScholarshipFee);
                    }
                    else
                        lstRegistrationScholarshipFee.Remove(registrationScholarshipFee);
                }
            }
            foreach (RegistrationScholarship entityScholarship in lstRegistrationScholarshipFee)
            {
                entityScholarshipDao.Delete(entityScholarship.RegistrationID, entityScholarship.ScholarshipID);
            }

            List<StudentFeeComp> lstStudentFeeComp = BusinessLayer.GetStudentFeeCompList(String.Format("ProspectiveStudentID = {0} AND IsDeleted = 0", hdnProspectiveStudentID.Value), ctx);
            List<StudentFee> lstStudentFee = BusinessLayer.GetStudentFeeList(String.Format("ProspectiveStudentID = {0} AND IsDeleted = 0", hdnProspectiveStudentID.Value), ctx);
            string lstStudentFeeID = string.Join(",", lstStudentFee.Select(p => p.StudentFeeID).ToList());
            List<StudentFeeDt> lstStudentFeeDt = null;
            if (lstStudentFeeID != "")
                lstStudentFeeDt = BusinessLayer.GetStudentFeeDtList(string.Format("StudentFeeID IN ({0})", lstStudentFeeID), ctx);
            else
                lstStudentFeeDt = new List<StudentFeeDt>();

            string[] lstSaveValue = hdnSaveValue.Value.Split('|');
            foreach (string saveValue in lstSaveValue)
            {
                string[] temp = saveValue.Split(';');
                int studentFeeCompTypeID = Convert.ToInt32(temp[0]);
                short noOfPeriod = Convert.ToInt16(temp[1]);
                decimal admissionFeeCompValue = Convert.ToDecimal(temp[2]);
                string GCAdmissionPaymentPeriod = temp[3];
                decimal discountPercentage = Convert.ToDecimal(temp[4]);
                decimal discountAmount = Convert.ToDecimal(temp[5]);
                DateTime dueDate = Helper.GetDatePickerValue(temp[6]);
                StudentFeeComp studentFeeComp = lstStudentFeeComp.FirstOrDefault(p => p.StudentFeeCompTypeID == studentFeeCompTypeID);
                StudentFee studentFee = null;
                List<StudentFeeDt> lstStudentFeeDt1 = null;
                if (studentFeeComp != null)
                {
                    studentFeeComp.NoOfPeriod = noOfPeriod;
                    studentFeeComp.TotalAmount = admissionFeeCompValue;
                    studentFeeComp.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityFeeCompDao.Update(studentFeeComp);

                    lstStudentFeeComp.Remove(studentFeeComp);
                    studentFee = lstStudentFee.FirstOrDefault(p => p.StudentFeeCompID == studentFeeComp.StudentFeeCompID);
                    lstStudentFeeDt1 = lstStudentFeeDt.Where(p => p.StudentFeeID == studentFee.StudentFeeID).ToList();

                    lstStudentFee.Remove(studentFee);
                    studentFee.DueDate = dueDate;
                    studentFee.TransactionAmount = admissionFeeCompValue * noOfPeriod;
                    studentFee.IsDiscountAmountInPercentage = true;
                    studentFee.DiscountAmount = discountPercentage;
                    studentFee.TotalDiscountAmount = discountAmount;
                    studentFee.TotalStudentAmount = studentFee.StudentAmount = studentFee.LineAmount = studentFee.TransactionAmount - studentFee.TotalDiscountAmount;
                    studentFee.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityFeeDao.Update(studentFee);
                }
                else
                {
                    studentFeeComp = new StudentFeeComp();
                    studentFeeComp.SchoolPeriodID = Convert.ToInt32(hdnSchoolPeriodID.Value);
                    studentFeeComp.ProspectiveStudentID = entity.ProspectiveStudentID;
                    studentFeeComp.StudentFeeCompTypeID = studentFeeCompTypeID;
                    studentFeeComp.NoOfPeriod = noOfPeriod;
                    studentFeeComp.TotalAmount = admissionFeeCompValue;
                    studentFeeComp.CreatedBy = AppSession.UserLogin.UserID;
                    entityFeeCompDao.Insert(studentFeeComp);
                    studentFeeComp.StudentFeeCompID = BusinessLayer.GetStudentFeeCompMaxID(ctx);

                    studentFee = new StudentFee();
                    studentFee.SchoolPeriodID = Convert.ToInt32(hdnSchoolPeriodID.Value);
                    studentFee.DisplayOrder = 1;

                    if (GCAdmissionPaymentPeriod == Constant.AdmissionPaymentPeriod.BULANAN)
                    {
                        studentFee.TransactionMonth = Convert.ToInt32(hdnMonth.Value);
                        studentFee.TransactionYear = Convert.ToInt32(hdnYear.Value);
                    }
                    else if (GCAdmissionPaymentPeriod == Constant.AdmissionPaymentPeriod.TAHUNAN)
                        studentFee.TransactionYear = Convert.ToInt32(hdnYear.Value);
                    studentFee.ProspectiveStudentID = entity.ProspectiveStudentID;
                    studentFee.StudentFeeCompID = studentFeeComp.StudentFeeCompID;
                    studentFee.DueDate = dueDate;
                    studentFee.TransactionAmount = admissionFeeCompValue * noOfPeriod;
                    studentFee.IsDiscountAmountInPercentage = true;
                    studentFee.DiscountAmount = discountPercentage;
                    studentFee.TotalDiscountAmount = discountAmount;
                    studentFee.TotalStudentAmount = studentFee.StudentAmount = studentFee.LineAmount = studentFee.TransactionAmount - studentFee.TotalDiscountAmount;
                    studentFee.CreatedBy = AppSession.UserLogin.UserID;
                    entityFeeDao.Insert(studentFee);
                    studentFee.StudentFeeID = BusinessLayer.GetStudentFeeMaxID(ctx);

                    lstStudentFeeDt1 = new List<StudentFeeDt>();
                }

                string[] lstSaveValue1 = temp[7].Split(',');
                short ctr = 1;
                foreach (string saveValue1 in lstSaveValue1)
                {
                    string[] temp1 = saveValue1.Split('^');
                    StudentFeeDt entityFeeDt = lstStudentFeeDt1.FirstOrDefault(p => p.DisplayOrder == ctr);
                    if (entityFeeDt == null)
                    {
                        entityFeeDt = new StudentFeeDt();
                        entityFeeDt.StudentFeeID = studentFee.StudentFeeID;
                        entityFeeDt.DisplayOrder = ctr;
                        entityFeeDt.DueDate = Helper.GetDatePickerValue(temp1[0]);
                        entityFeeDt.TransactionAmount = Convert.ToDecimal(temp1[1]);
                        entityFeeDt.IsTransactionAmountInPercentage = true;
                        entityFeeDt.TotalStudentAmount = entityFeeDt.StudentAmount = entityFeeDt.LineAmount = Convert.ToDecimal(temp1[2]);
                        entityFeeDt.CreatedBy = AppSession.UserLogin.UserID;

                        entityFeeDtDao.Insert(entityFeeDt);
                    }
                    else
                    {
                        entityFeeDt.DueDate = Helper.GetDatePickerValue(temp1[0]);
                        entityFeeDt.TransactionAmount = Convert.ToDecimal(temp1[1]);
                        entityFeeDt.IsTransactionAmountInPercentage = true;
                        entityFeeDt.TotalStudentAmount = entityFeeDt.StudentAmount = entityFeeDt.LineAmount = Convert.ToDecimal(temp1[2]);
                        entityFeeDt.LastUpdatedBy = AppSession.UserLogin.UserID;

                        entityFeeDtDao.Update(entityFeeDt);

                        lstStudentFeeDt.Remove(entityFeeDt);
                    }
                    ctr++;
                }
            }
            foreach (StudentFeeComp entityFeeComp in lstStudentFeeComp)
            {
                entityFeeComp.IsDeleted = true;
                entityFeeComp.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityFeeCompDao.Update(entityFeeComp);
            }
            foreach (StudentFee entityFee in lstStudentFee)
            {
                entityFee.IsDeleted = true;
                entityFee.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityFeeDao.Update(entityFee);
            }
            foreach (StudentFeeDt entityFeeDt in lstStudentFeeDt)
            {
                entityFeeDt.IsDeleted = true;
                entityFeeDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityFeeDtDao.Update(entityFeeDt);
            }
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            try
            {
                int registrationID = Convert.ToInt32(tacRegistration.Value);
                if (type == "save")
                {
                    OnSaveRecord(ctx, registrationID, false);
                }
                else if(type == "void")
                {
                    BusinessLayer.DeleteARProspectiveStudent(AppSession.UserLogin.UserID, registrationID, ctx);
                }
                else
                {
                    ProspectiveStudentDao entityProspectiveStudentDao = new ProspectiveStudentDao(ctx);
                    OnSaveRecord(ctx, registrationID, true);

                    int prospectiveStudentID = Convert.ToInt32(hdnProspectiveStudentID.Value);
                    ProspectiveStudent entityProspectiveStudent = entityProspectiveStudentDao.Get(prospectiveStudentID);
                    if (entityProspectiveStudent.ProspectiveStudentCode == "")
                    {
                        //entityProspectiveStudent.ProspectiveStudentCode = BusinessLayer.GenerateProspectiveStudentCode(AppSession.UserLogin.SiteID, Convert.ToInt32(hdnYear.Value), RegistrationNo, ctx);
                        ctx.CommandType = CommandType.Text;
                        ctx.Command.Parameters.Clear();
                        entityProspectiveStudent.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityProspectiveStudentDao.Update(entityProspectiveStudent);
                    }
                    //BusinessLayer.GenerateARProspectiveStudent(AppSession.UserLogin.UserID, AppSession.UserLogin.SiteID, registrationID, ctx);
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
    }
}