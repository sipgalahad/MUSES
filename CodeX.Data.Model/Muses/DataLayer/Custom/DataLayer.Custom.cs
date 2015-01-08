using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeX.Common;

namespace CodeX.Data.Model
{
    #region DailyScheduleTypeDt
    public partial class DailyScheduleTypeDt
    {
        public string cfDailyScheduleType
        {
            get { return _GCDailyScheduleType.Split('^')[1]; }
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
}
