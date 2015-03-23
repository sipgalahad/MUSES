using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeX.Common;

namespace CodeX.Data.Model
{
    #region AdmissionPaymentDt
    public partial class AdmissionPaymentDt
    {
        public Boolean IsPaymentDateNow
        {
            get
            {
                if (_PaymentDate.ToString("dd-MM-yyyy") == Constant.ConstantDate.DEFAULT_NULL)
                    return true;
                return false;
            }
        }
        public String PaymentDateInDatePickerFormat
        {
            get
            {
                if (_PaymentDate.ToString("dd-MM-yyyy") == Constant.ConstantDate.DEFAULT_NULL)
                    return "";
                return _PaymentDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            }
        }
    }
    #endregion
    #region AdmissionPaymentHd
    public partial class AdmissionPaymentHd
    {
        public String cfRemarks
        {
            get
            {
                return _Remarks.Replace("\n", "<br>");
            }
        }
    }
    #endregion
    #region ARInvoiceHd
    public partial class ARInvoiceHd
    {
        public String DueDateInString
        {
            get { return _DueDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }

        public String ARInvoiceDateInString
        {
            get { return _ARInvoiceDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }

        public Decimal RemainingAmount
        {
            get { return (_TotalClaimedAmount - _TotalPaymentAmount); }
        }
    }
    #endregion
    #region DailyScheduleTypeDt
    public partial class DailyScheduleTypeDt
    {
        public string cfDailyScheduleType
        {
            get { return _GCDailyScheduleType.Split('^')[1]; }
        }
    }
    #endregion
    #region ExamScheduleHd
    public partial class ExamScheduleHd
    {
        public string StartDateInDatePickerFormat
        {
            get { return _StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }
        public string EndDateInDatePickerFormat
        {
            get { return _EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }
    }
    #endregion
    #region FADepreciation
    public partial class FADepreciation
    {
        public String DepreciationDateInString
        {
            get { return _DepreciationDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
        public String DepreciationYear
        {
            get { return _PeriodNo.Substring(0, 4); }
        }
        public String DepreciationPeriodNo
        {
            get { return _PeriodNo.Substring(4, 2); }
        }
    }
    #endregion
    #region Holiday
    public partial class Holiday
    {
        public string DateInString
        {
            get
            {
                if (_IsAnnualHoliday)
                    return new DateTime(DateTime.Now.Year, _HolidayMonth, _HolidayDate).ToString(Constant.FormatString.DATE_FORMAT);
                else
                    return new DateTime((int)_HolidayYear, _HolidayMonth, _HolidayDate).ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
    }
    #endregion
    #region ItemRequestHd
    public partial class ItemRequestHd
    {
        public string TransactionDateInString
        {
            get
            {
                return _TransactionDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
    }
    #endregion
    #region PurchaseRequestHd
    public partial class PurchaseRequestHd
    {
        public string TransactionDateInString
        {
            get
            {
                return _TransactionDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
    }
    #endregion
    #region PurchaseOrderDt
    public partial class PurchaseOrderDt
    {
        public Decimal CustomSubTotal
        {
            get
            {
                Decimal totalAfterDisc1 = (Quantity * UnitPrice) - ((Quantity * UnitPrice) *
               _DiscountPercentage1 / 100);
                Decimal totalAfterDisc2 = totalAfterDisc1 - (_DiscountPercentage2 / 100 * totalAfterDisc1);
                return totalAfterDisc2;
            }
        }
    }
    #endregion
    #region ScholarshipComp
    public partial class ScholarshipComp
    {
        public String cfDiscountAmount
        {
            get
            {
                if (_IsDiscountInPercentage)
                    return string.Format("{0} %", _DiscountAmount);
                return _DiscountAmount.ToString("N");
            }
        }
    }
    #endregion
    #region StudentFee
    public partial class StudentFee
    {
        public String PaymentDateInString
        {
            get { return _PaymentDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
    }
    #endregion
    #region StudentProgressRuleDt
    public partial class StudentProgressRuleDt
    {
        public String cfValue
        {
            get
            {
                string result = "";
                if (_IsFromPassingGrade)
                    result += "{KKM}";
                else
                    result += _FromValue.ToString();
                result += " - ";
                if (_IsToPassingGrade)
                    result += "{KKM}";
                else
                    result += _ToValue.ToString();
                return result;
            }
        }
    }
    #endregion
}
