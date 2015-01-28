using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class StudentFeeCompEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.STUDENT_FEE_COMP;
        }

        public String GetAdmissionPaymentPeriodMonth() 
        {
            return Constant.AdmissionPaymentPeriod.BULANAN;
        }

        public String GetAdmissionPaymentPeriodYear()
        {
            return Constant.AdmissionPaymentPeriod.TAHUNAN;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                StudentFeeCompType entity = BusinessLayer.GetStudentFeeCompType(Convert.ToInt32(ID));
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            txtStudentFeeCompTypeName.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsDeleted = 0 AND IsActive = 1", Constant.StandardCode.ADMISSION_PAYMENT_PERIOD));
            Methods.SetComboBoxField<StandardCode>(cboAdmissionPaymentPeriod, lstStandardCode, "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtStudentFeeCompTypeName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboAdmissionPaymentPeriod, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtDay, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtMonth, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(StudentFeeCompType entity)
        {
            txtStudentFeeCompTypeName.Text = entity.StudentFeeCompTypeName;
            cboAdmissionPaymentPeriod.Value = entity.GCAdmissionPaymentPeriod;
            if (entity.PaymentDate != null) txtDay.Text = entity.PaymentDate.ToString();
            else txtDay.Text = "";
            if (entity.PaymentMonth != null) txtMonth.Text = entity.PaymentMonth.ToString();
            else txtMonth.Text = "";
        }

        private void ControlToEntity(StudentFeeCompType entity)
        {
            entity.SiteID = AppSession.UserLogin.SiteID;
            entity.StudentFeeCompTypeName = txtStudentFeeCompTypeName.Text;
            entity.GCAdmissionPaymentPeriod = cboAdmissionPaymentPeriod.Value.ToString();
            if (txtDay.Text != "") entity.PaymentDate = Convert.ToInt32(txtDay.Text);
            else entity.PaymentDate = null;
            if (txtMonth.Text != "" ) entity.PaymentMonth = Convert.ToInt32(txtMonth.Text);
            else entity.PaymentMonth = null;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            //string FilterExpression = string.Format("BankCode = '{0}'", txtBankCode.Text);
            //List<Bank> lst = BusinessLayer.GetBankList(FilterExpression);

            //if (lst.Count > 0)
            //    errMessage = " Bank With Code " + txtBankCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            //string FilterExpression = string.Format("BankCode = '{0}' AND BankID != {1}", txtBankCode.Text, hdnID.Value);
            //List<Bank> lst = BusinessLayer.GetBankList(FilterExpression);

            //if (lst.Count > 0)
            //    errMessage = " Bank With Code " + txtBankCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            StudentFeeCompTypeDao entityDao = new StudentFeeCompTypeDao(ctx);
            bool result = false;
            try
            {
                StudentFeeCompType entity = new StudentFeeCompType();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetStudentFeeCompTypeMaxID(ctx).ToString();
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            try
            {
                StudentFeeCompType entity = BusinessLayer.GetStudentFeeCompType(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateStudentFeeCompType(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
    }
}