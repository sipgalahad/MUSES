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
    public partial class AdmissionFeeEntry : BasePageTrx
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

            hdnYear.Value = entity.StartDate.Year.ToString();
            List<AdmissionPaymentHd> lstPayment = BusinessLayer.GetAdmissionPaymentHdList(string.Format("SchoolPeriodID = {0} AND IsDeleted = 0", hdnSchoolPeriodID.Value));
            Methods.SetComboBoxField<AdmissionPaymentHd>(cboPaymentType, lstPayment, "PaymentName", "PaymentID");

            Helper.SetControlEntrySetting(tacRegistration, new ControlEntrySetting(true, true, true), "mpFilterGenerate");
            Helper.SetControlEntrySetting(tacAdmissionFeeRule, new ControlEntrySetting(true, true, true), "mpFilterGenerate");
            Helper.SetControlEntrySetting(cboPaymentType, new ControlEntrySetting(true, true, true), "mpFilterGenerate");
        }

        List<AdmissionPaymentDt> lstPaymentDt = null;
        List<vStudentFee> lstStudentFee = null;
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
                lstStudentFee = BusinessLayer.GetvStudentFeeList(String.Format("RegistrationID = {0} AND IsDeleted = 0", tacRegistration.Value));
                if (hdnLstScholarshipID.Value != "")
                    lstScholarshipComp = BusinessLayer.GetScholarshipCompList(string.Format("ScholarshipID IN ({0}) AND DiscountAmount > 0", hdnLstScholarshipID.Value));
                else
                    lstScholarshipComp = new List<ScholarshipComp>();

                List<StudentFeeComp> lstStudentFeeComp = BusinessLayer.GetStudentFeeCompList(String.Format("RegistrationID = {0} AND IsDeleted = 0", tacRegistration.Value));
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

                List<vStudentFee> lstStudentFee1 = lstStudentFee.Where(p => p.StudentFeeCompTypeID == entity.StudentFeeCompTypeID).ToList();

                List<vStudentFee> lstEntity = new List<vStudentFee>();
                List<AdmissionPaymentDt> lstPaymentDt1 = lstPaymentDt.Where(p => p.AdmissionFeeCompID == entity.AdmissionFeeCompID).ToList();
                short ctr = 1;
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
                        vStudentFee entityDt = lstStudentFee1.FirstOrDefault(p => p.DisplayOrder == ctr);
                        if (entityDt == null)
                        {
                            entityDt = new vStudentFee();
                            if (paymentDt.PaymentDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT) == Constant.ConstantDate.DEFAULT_NULL)
                                entityDt.PaymentDate = DateTime.Now;
                            else
                                entityDt.PaymentDate = paymentDt.PaymentDate;
                            entityDt.TotalPaymentAmount = totalPayment;
                            entityDt.PaymentAmount = totalPaymentInPercentage;
                            entityDt.DisplayOrder = ctr;
                        }
                        if (!isLoadRegistration && entityScholarshipComp != null)
                        {
                            if (entityScholarshipComp.IsDiscountInPercentage)
                            {
                                if (entityScholarshipComp.NoOfPeriod <= entity.NoOfRegistrationPaymentPeriod)
                                {
                                    Decimal DiscountAmount = entityScholarshipComp.DiscountAmount * entityScholarshipComp.NoOfPeriod / entity.NoOfRegistrationPaymentPeriod;
                                    entityDt.TotalDiscountAmount = DiscountAmount * entityDt.TotalPaymentAmount / 100;
                                    entityDt.DiscountAmount = DiscountAmount;
                                }
                                else
                                {
                                    entityDt.TotalDiscountAmount = entityScholarshipComp.DiscountAmount * entityDt.TotalPaymentAmount / 100;
                                    entityDt.DiscountAmount = entityScholarshipComp.DiscountAmount;
                                }
                            }
                            else
                            {
                                if (entityScholarshipComp.NoOfPeriod <= entity.NoOfRegistrationPaymentPeriod)
                                {
                                    entityDt.TotalDiscountAmount = entityScholarshipComp.DiscountAmount * entityScholarshipComp.NoOfPeriod;
                                    entityDt.DiscountAmount = entityScholarshipComp.DiscountAmount * entityScholarshipComp.NoOfPeriod * 100 / entityDt.TotalPaymentAmount;
                                }
                                else
                                {
                                    Decimal DiscountAmount = entityScholarshipComp.DiscountAmount * entityScholarshipComp.NoOfPeriod / entity.NoOfRegistrationPaymentPeriod;
                                    entityDt.TotalDiscountAmount = DiscountAmount;
                                    entityDt.DiscountAmount = DiscountAmount * 100 / entityDt.TotalPaymentAmount;
                                }
                            }
                        }
                        entityDt.LineAmount = entityDt.TotalPaymentAmount - entityDt.TotalDiscountAmount;
                        lstEntity.Add(entityDt);
                        ctr++;
                    }
                }
                rptViewDt.DataSource = lstEntity;
                rptViewDt.DataBind();
            }
        }

        private void OnSaveRecord(IDbContext ctx, int registrationID)
        {
            RegistrationDao entityDao = new RegistrationDao(ctx);
            StudentFeeDao entityFeeDao = new StudentFeeDao(ctx);
            StudentFeeCompDao entityFeeCompDao = new StudentFeeCompDao(ctx);
            RegistrationScholarshipDao entityScholarshipDao = new RegistrationScholarshipDao(ctx);
            
            Registration entity = entityDao.Get(registrationID);
            entity.AdmissionFeeRuleID = Convert.ToInt32(tacAdmissionFeeRule.Value);
            entity.PaymentID = Convert.ToInt32(cboPaymentType.Value);
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
                }
            }
            foreach (RegistrationScholarship entityScholarship in lstRegistrationScholarshipFee)
            {
                entityScholarshipDao.Delete(entityScholarship.RegistrationID, entityScholarship.ScholarshipID);
            }

            List<StudentFeeComp> lstStudentFeeComp = BusinessLayer.GetStudentFeeCompList(String.Format("RegistrationID = {0} AND IsDeleted = 0", tacRegistration.Value), ctx);
            List<StudentFee> lstStudentFee = BusinessLayer.GetStudentFeeList(String.Format("RegistrationID = {0} AND IsDeleted = 0", tacRegistration.Value), ctx);
            string[] lstSaveValue = hdnSaveValue.Value.Split('|');
            foreach (string saveValue in lstSaveValue)
            {
                string[] temp = saveValue.Split(';');
                int studentFeeCompTypeID = Convert.ToInt32(temp[0]);
                short noOfPeriod = Convert.ToInt16(temp[1]);
                decimal admissionFeeCompValue = Convert.ToDecimal(temp[2]);
                StudentFeeComp studentFeeComp = lstStudentFeeComp.FirstOrDefault(p => p.StudentFeeCompTypeID == studentFeeCompTypeID);
                List<StudentFee> lstStudentFee1 = null;
                if (studentFeeComp != null)
                {
                    studentFeeComp.NoOfPeriod = noOfPeriod;
                    studentFeeComp.TotalAmount = admissionFeeCompValue;
                    studentFeeComp.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityFeeCompDao.Update(studentFeeComp);

                    lstStudentFeeComp.Remove(studentFeeComp);
                    lstStudentFee1 = lstStudentFee.Where(p => p.StudentFeeCompID == studentFeeComp.StudentFeeCompID).ToList();
                }
                else
                {
                    studentFeeComp = new StudentFeeComp();
                    studentFeeComp.SchoolPeriodID = Convert.ToInt32(hdnSchoolPeriodID.Value);
                    studentFeeComp.RegistrationID = entity.RegistrationID;
                    studentFeeComp.StudentFeeCompTypeID = studentFeeCompTypeID;
                    studentFeeComp.NoOfPeriod = noOfPeriod;
                    studentFeeComp.TotalAmount = admissionFeeCompValue;
                    studentFeeComp.CreatedBy = AppSession.UserLogin.UserID;
                    entityFeeCompDao.Insert(studentFeeComp);
                    studentFeeComp.StudentFeeCompID = BusinessLayer.GetStudentFeeCompMaxID(ctx);

                    lstStudentFee1 = new List<StudentFee>();
                }

                string[] lstSaveValue1 = temp[3].Split(',');
                short ctr = 1;
                foreach (string saveValue1 in lstSaveValue1)
                {
                    string[] temp1 = saveValue1.Split('^');
                    StudentFee entityFee = lstStudentFee1.FirstOrDefault(p => p.DisplayOrder == ctr);
                    if (entityFee == null)
                    {
                        entityFee = new StudentFee();
                        entityFee.SchoolPeriodID = Convert.ToInt32(hdnSchoolPeriodID.Value);
                        entityFee.StudentFeeCompID = studentFeeComp.StudentFeeCompID;
                        entityFee.RegistrationID = entity.RegistrationID;
                        entityFee.DisplayOrder = ctr;
                        entityFee.PaymentDate = Helper.GetDatePickerValue(temp1[0]);
                        entityFee.PaymentAmount = Convert.ToDecimal(temp1[1]);
                        entityFee.IsPaymentAmountInPercentage = true;
                        entityFee.TotalPaymentAmount = Convert.ToDecimal(temp1[2]);
                        entityFee.DiscountAmount = Convert.ToDecimal(temp1[3]);
                        entityFee.IsDiscountAmountInPercentage = true;
                        entityFee.TotalDiscountAmount = Convert.ToDecimal(temp1[4]);
                        entityFee.LineAmount = Convert.ToDecimal(temp1[5]);
                        entityFee.CreatedBy = AppSession.UserLogin.UserID;

                        entityFeeDao.Insert(entityFee);
                    }
                    else
                    {
                        entityFee.PaymentDate = Helper.GetDatePickerValue(temp1[0]);
                        entityFee.PaymentAmount = Convert.ToDecimal(temp1[1]);
                        entityFee.IsPaymentAmountInPercentage = true;
                        entityFee.TotalPaymentAmount = Convert.ToDecimal(temp1[2]);
                        entityFee.DiscountAmount = Convert.ToDecimal(temp1[3]);
                        entityFee.IsDiscountAmountInPercentage = true;
                        entityFee.TotalDiscountAmount = Convert.ToDecimal(temp1[4]);
                        entityFee.LineAmount = Convert.ToDecimal(temp1[5]);
                        entityFee.LastUpdatedBy = AppSession.UserLogin.UserID;

                        entityFeeDao.Update(entityFee);

                        lstStudentFee.Remove(entityFee);
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
                    OnSaveRecord(ctx, registrationID);
                }
                else if(type == "void")
                {
                    BusinessLayer.DeleteARProspectiveStudent(AppSession.UserLogin.UserID, registrationID, ctx);
                }
                else
                {
                    ProspectiveStudentDao entityProspectiveStudentDao = new ProspectiveStudentDao(ctx);
                    OnSaveRecord(ctx, registrationID);

                    int prospectiveStudentID = Convert.ToInt32(hdnProspectiveStudentID.Value);
                    ProspectiveStudent entityProspectiveStudent = entityProspectiveStudentDao.Get(prospectiveStudentID);
                    if (entityProspectiveStudent.ProspectiveStudentCode == "")
                    {
                        entityProspectiveStudent.ProspectiveStudentCode = BusinessLayer.GenerateProspectiveStudentCode(AppSession.UserLogin.SiteID, Convert.ToInt32(hdnYear.Value), ctx);
                        ctx.CommandType = CommandType.Text;
                        ctx.Command.Parameters.Clear();
                        entityProspectiveStudent.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityProspectiveStudentDao.Update(entityProspectiveStudent);
                    }
                    BusinessLayer.GenerateARProspectiveStudent(AppSession.UserLogin.UserID, AppSession.UserLogin.SiteID, registrationID, ctx);
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