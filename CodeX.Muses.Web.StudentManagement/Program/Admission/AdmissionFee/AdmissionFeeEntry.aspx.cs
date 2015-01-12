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
            return string.Format("PeriodAdmissionID = {0} AND GCRegistrationStatus = '{1}'", AppSession.PeriodAdmissionID, Constant.RegistrationStatus.ACCEPTED);
        }
        protected string OnGetAdmissionFeeRuleFilterExpression()
        {
            return string.Format("SchoolPeriodID = {0} AND IsDeleted = 0", hdnSchoolPeriodID.Value);
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
            hdnSchoolPeriodID.Value = BusinessLayer.GetPeriodAdmission(AppSession.PeriodAdmissionID).SchoolPeriodID.ToString();
            List<AdmissionPaymentHd> lstPayment = BusinessLayer.GetAdmissionPaymentHdList(string.Format("SchoolPeriodID = {0} AND IsDeleted = 0", hdnSchoolPeriodID.Value));
            Methods.SetComboBoxField<AdmissionPaymentHd>(cboPaymentType, lstPayment, "PaymentName", "PaymentID");
        }

        List<AdmissionPaymentDt> lstPaymentDt = null;
        List<RegistrationFee> lstRegistrationFee = null;
        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            lstPaymentDt = BusinessLayer.GetAdmissionPaymentDtList(string.Format("PaymentID = {0}", cboPaymentType.Value));
            lstRegistrationFee = BusinessLayer.GetRegistrationFeeList(String.Format("RegistrationID = {0} AND IsDeleted = 0", tacRegistration.Value));

            List<vAdmissionFeeRuleDtCustom> lstEntity = BusinessLayer.GetvAdmissionFeeRuleDtCustomList(string.Format("SchoolPeriodID = {0} AND (IsFixedAmount = 1 OR (IsFixedAmount = 0 AND PeriodAdmissionID = {1} AND AdmissionFeeRuleID = {2})) AND IsDeleted = 0", hdnSchoolPeriodID.Value, AppSession.PeriodAdmissionID, tacAdmissionFeeRule.Value));
            rptAdmissionComp.DataSource = lstEntity;
            rptAdmissionComp.DataBind();
        }

        protected void rptAdmissionComp_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vAdmissionFeeRuleDtCustom entity = (vAdmissionFeeRuleDtCustom)e.Item.DataItem;
                Repeater rptViewDt = (Repeater)e.Item.FindControl("rptViewDt");

                List<RegistrationFee> lstRegistrationFee1 = lstRegistrationFee.Where(p => p.AdmissionFeeCompID == entity.AdmissionFeeCompID).ToList();

                List<RegistrationFee> lstEntity = new List<RegistrationFee>();
                List<AdmissionPaymentDt> lstPaymentDt1 = lstPaymentDt.Where(p => p.AdmissionFeeCompID == entity.AdmissionFeeCompID).ToList();
                short ctr = 1;
                foreach (AdmissionPaymentDt paymentDt in lstPaymentDt1)
                {
                    decimal totalPayment = 0;
                    if (paymentDt.IsPaymentAmountInPercentage)
                        totalPayment = entity.TotalAmount * paymentDt.PaymentAmount / 100;
                    else
                        totalPayment = paymentDt.PaymentAmount;
                    totalPayment = totalPayment / paymentDt.NoOfPayment;
                    for (int i = 0; i < paymentDt.NoOfPayment; ++i)
                    {
                        RegistrationFee entityDt = lstRegistrationFee1.FirstOrDefault(p => p.DisplayOrder == ctr);
                        if (entityDt == null)
                        {
                            entityDt = new RegistrationFee();
                            if (paymentDt.PaymentDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT) == Constant.ConstantDate.DEFAULT_NULL)
                                entityDt.PaymentDate = DateTime.Now;
                            else
                                entityDt.PaymentDate = paymentDt.PaymentDate;
                            entityDt.TotalPaymentAmount = totalPayment;
                            entityDt.LineAmount = entityDt.TotalPaymentAmount - entityDt.TotalDiscountAmount;
                            entityDt.DisplayOrder = ctr;
                        }
                        lstEntity.Add(entityDt);
                        ctr++;
                    }
                }
                rptViewDt.DataSource = lstEntity;
                rptViewDt.DataBind();
            }
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            RegistrationDao entityDao = new RegistrationDao(ctx);
            RegistrationFeeDao entityFeeDao = new RegistrationFeeDao(ctx);
            try
            {
                Registration entity = entityDao.Get(Convert.ToInt32(tacRegistration.Value));
                entity.AdmissionFeeRuleID = Convert.ToInt32(tacAdmissionFeeRule.Value);
                entity.PaymentID = Convert.ToInt32(cboPaymentType.Value);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

                List<RegistrationFee> lstRegistrationFee = BusinessLayer.GetRegistrationFeeList(String.Format("RegistrationID = {0} AND IsDeleted = 0", tacRegistration.Value), ctx);
                string[] lstSaveValue = hdnSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    int admissionFeeCompID = Convert.ToInt32(temp[0]);
                    List<RegistrationFee> lstRegistrationFee1 = lstRegistrationFee.Where(p => p.AdmissionFeeCompID == admissionFeeCompID).ToList();

                    string[] lstSaveValue1 = temp[1].Split(',');
                    short ctr = 1;
                    foreach (string saveValue1 in lstSaveValue1)
                    {
                        string[] temp1 = saveValue1.Split('^');
                        RegistrationFee entityFee = lstRegistrationFee1.FirstOrDefault(p => p.DisplayOrder == ctr);
                        if (entityFee == null)
                        {
                            entityFee = new RegistrationFee();
                            entityFee.RegistrationID = entity.RegistrationID;
                            entityFee.AdmissionFeeCompID = admissionFeeCompID;
                            entityFee.DisplayOrder = ctr;
                            entityFee.PaymentDate = Helper.GetDatePickerValue(temp1[0]);
                            entityFee.TotalPaymentAmount = Convert.ToDecimal(temp1[1]);
                            entityFee.TotalDiscountAmount = Convert.ToDecimal(temp1[2]);
                            entityFee.LineAmount = Convert.ToDecimal(temp1[3]);
                            entityFee.CreatedBy = AppSession.UserLogin.UserID;

                            entityFeeDao.Insert(entityFee);
                        }
                        else
                        {
                            entityFee.PaymentDate = Helper.GetDatePickerValue(temp1[0]);
                            entityFee.TotalPaymentAmount = Convert.ToDecimal(temp1[1]);
                            entityFee.TotalDiscountAmount = Convert.ToDecimal(temp1[2]);
                            entityFee.LineAmount = Convert.ToDecimal(temp1[3]);
                            entityFee.LastUpdatedBy = AppSession.UserLogin.UserID;

                            entityFeeDao.Update(entityFee);

                            lstRegistrationFee.Remove(entityFee);
                        }
                        ctr++;
                    }
                }

                foreach (RegistrationFee entityFee in lstRegistrationFee)
                {
                    entityFee.IsDeleted = true;
                    entityFee.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityFeeDao.Update(entityFee);
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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