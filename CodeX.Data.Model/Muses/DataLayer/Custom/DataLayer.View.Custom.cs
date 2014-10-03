using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using CodeX.Common;

namespace CodeX.Data.Model
{
    #region vItemBalance
    public partial class vItemBalance
    {
        public String CustomMinimum
        {
            get { return string.Format("{0:N} {1}", QuantityMIN, _ItemUnit); }
        }

        public String CustomMaximum
        {
            get { return string.Format("{0:N} {1}", QuantityMAX, _ItemUnit); }
        }
        public String CustomEndingBalance
        {
            get { return string.Format("{0:N} {1}", QuantityEND, _ItemUnit); }
        }
    }
    #endregion
    #region vItemTransactionDt
    public partial class vItemTransactionDt
    {
        public Boolean IsAllowEditItem
        {
            get
            {
                return (_GCItemDetailStatus == Constant.TransactionStatus.OPEN);
            }
        }
        public String Conversion
        {
            get { return string.Format("1 {0} = {1} {2}", _BaseUnit, _ConversionFactor, _ItemUnit); }
        }
        public String CustomItemUnit
        {
            get
            {
                return _Quantity + " " + _ItemUnit;
            }
        }
        public String TransactionDateInString
        {
            get { return _TransactionDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
    }
    #endregion
    #region vItemTransactionHd
    public partial class vItemTransactionHd
    {
        public String TransactionDateInString
        {
            get { return _TransactionDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
    }
    #endregion
    #region vPeriodAdmission
    public partial class vPeriodAdmission
    {
        public string StartDateInString
        {
            get
            {
                return _StartDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public string EndDateInString
        {
            get
            {
                return _EndDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public string RegistrationStartDateInString
        {
            get
            {
                return _RegistrationStartDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public string RegistrationEndDateInString
        {
            get
            {
                return _RegistrationEndDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public string StartDateInDatePickerFormat
        {
            get
            {
                return _StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            }
        }
        public string EndDateInDatePickerFormat
        {
            get
            {
                return _EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            }
        }
    }
    #endregion
    #region vPeriodClassType
    public partial class vPeriodClassType
    {
        public bool IsAllowEditItem
        {
            get
            {
                return _CreatedClass < 1;
            }
        }
    }
    #endregion
    #region vPeriodSchedule
    public partial class vPeriodSchedule
    {
        public string StartDateInString
        {
            get
            {
                return _StartDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public string EndDateInString
        {
            get
            {
                return _EndDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public string StartDateInDatePickerFormat
        {
            get
            {
                return _StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            }
        }
        public string EndDateInDatePickerFormat
        {
            get
            {
                return _EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            }
        }
    }
    #endregion
    #region vPeriodSection
    public partial class vPeriodSection
    {
        public string StartDateInString
        {
            get
            {
                return _StartDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public string EndDateInString
        {
            get
            {
                return _EndDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public string StartDateInDatePickerFormat
        {
            get
            {
                return _StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            }
        }
        public string EndDateInDatePickerFormat
        {
            get
            {
                return _EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            }
        }
    }
    #endregion
    #region vStudent
    public partial class vStudent
    {
        public int AgeInYear
        {
            get
            {
                return Function.GetPatientAgeInYear(_DateOfBirth, DateTime.Now);
            }
        }
        public int AgeInMonth
        {
            get
            {
                return Function.GetPatientAgeInMonth(_DateOfBirth, DateTime.Now);
            }
        }
        public int AgeInDay
        {
            get
            {
                return Function.GetPatientAgeInDay(_DateOfBirth, DateTime.Now);
            }
        }
    }
    #endregion
}
