using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using CodeX.Common;

namespace CodeX.Data.Model
{
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
