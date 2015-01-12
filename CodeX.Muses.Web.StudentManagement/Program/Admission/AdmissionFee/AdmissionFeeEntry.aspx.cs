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

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            return true;
        }
    }
}