using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using CodeX.Common;

namespace CodeX.Data.Model
{
    #region GetAPSupplierInformationDt
    public partial class GetAPSupplierInformationDt
    {
        public String PurchaseReceivedDateInString
        {
            get
            {
                if (_PurchaseReceivedDate.ToString("dd-MM-yyyy") == Constant.ConstantDate.DEFAULT_NULL)
                    return "-";
                return _PurchaseReceivedDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public String PurchaseInvoiceDateInString
        {
            get
            {
                if (_PurchaseInvoiceDate.ToString("dd-MM-yyyy") == Constant.ConstantDate.DEFAULT_NULL)
                    return "-";
                return _PurchaseInvoiceDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
    }
    #endregion
    #region GetARCustomerInformationDt
    public partial class GetARCustomerInformationDt
    {
        public String ARInvoiceDateInString
        {
            get
            {
                if (_ARInvoiceDate.ToString("dd-MM-yyyy") == Constant.ConstantDate.DEFAULT_NULL)
                    return "-";
                return _ARInvoiceDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
    }
    #endregion
    #region GetARProspectiveStudentInformationDt
    public partial class GetARProspectiveStudentInformationDt
    {
        public String ARInvoiceDateInString
        {
            get
            {
                if (_ARInvoiceDate.ToString("dd-MM-yyyy") == Constant.ConstantDate.DEFAULT_NULL)
                    return "-";
                return _ARInvoiceDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
    }
    #endregion
    #region GetARStudentInformationDt
    public partial class GetARStudentInformationDt
    {
        public String ARInvoiceDateInString
        {
            get
            {
                if (_ARInvoiceDate.ToString("dd-MM-yyyy") == Constant.ConstantDate.DEFAULT_NULL)
                    return "-";
                return _ARInvoiceDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
    }
    #endregion
    #region GetFixedAssetValue
    public partial class GetFixedAssetValue
    {
        public String ProcurementDateInString
        {
            get { return _ProcurementDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
        public String DepreciationStartDateInString 
        {
            get { return _DepreciationStartDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }

        public String DepreciationDateInString
        {
            get { return _DepreciationDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
        public Int32 Umur 
        {
            get { return _DepreciationLength * 12; }
        }
    }
    #endregion
    #region GetGLBalanceDtPerSubLedger
    public partial class GetGLBalanceDtPerSubLedger
    {
        public String JournalDateInString
        {
            get { return _JournalDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
    }
    #endregion
    #region GetGLBalanceDtInformation
    public partial class GetGLBalanceDtInformation
    {
        public String JournalDateInString
        {
            get { return _JournalDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
    }
    #endregion
    #region GetGLBalancePerGLAccount
    public partial class GetGLBalancePerGLAccount
    {
        public String JournalDateInString
        {
            get { return _JournalDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
    }
    #endregion
    #region GetGLBalancePerPeriodForTBalance
    public partial class GetGLBalancePerPeriodForTBalance
    {
        public String AdditionalClassName { get; set; }
    }
    #endregion
    #region GetGLBalanceProfitLossPerPeriodPerLevel
    public partial class GetGLBalanceProfitLossPerPeriodPerLevel
    {
        public Decimal cfBalanceEND
        {
            get { return _cfBalanceENDLastMonth + _cfProfitLoss; }
        }
        public Decimal BalanceEND
        {
            get { return _BalanceENDLastMonth + _ProfitLoss; }
        }
    }
    #endregion
    #region GetItemMovementPerPeriodeDetail
    public partial class GetItemMovementPerPeriodeDetail
    {
        public Decimal QuantityEND
        {
            get
            {
                return _IN_QuantityBEGIN + _IN_PurchaseReceive + _IN_Distribution + _IN_Adjustment - _OUT_Adjustment - _OUT_Distribution - _OUT_Consumption;
            }
        }
    }
    #endregion
    #region GetItemUsagePurchaseRequestROP
    public partial class GetItemUsagePurchaseRequestROP
    {
        public Decimal AvgQuantityOut
        {
            get
            {
                if (_NDaysBackward != 0)
                    return _QuantityOut / _NDaysBackward;
                return 0;
            }
        }
        public Decimal QtyOrder
        {
            get
            {
                return Math.Ceiling(AvgQuantityOut * _NDaysForward);
            }
        }
    }
    #endregion
    #region GetStudentReceiveSummaryDt
    public partial class GetStudentReceiveSummaryDt
    {
        public String cfStudentFeeCompTypeName
        {
            get
            {
                if (_GCAdmissionPaymentPeriod == Constant.AdmissionPaymentPeriod.TAHUNAN)
                    return string.Format("{0} {1}", _StudentFeeCompTypeName, _TransactionYear);
                if (_GCAdmissionPaymentPeriod == Constant.AdmissionPaymentPeriod.BULANAN)
                {
                    DateTime dt = new DateTime(_TransactionYear, _TransactionMonth, 1);
                    return string.Format("{0} {1}", _StudentFeeCompTypeName, dt.ToString("MMM yyyy"));
                }
                return _StudentFeeCompTypeName;
            }
        }
        public String cfVirtualAccountNo
        {
            get
            {
                if (_StudentID == 0)
                    return _ProspectiveStudentCode;
                return _VirtualAccountNo;
            }
        }
        public String cfStudentName
        {
            get
            {
                if (_StudentID == 0)
                    return _ProspectiveStudentName;
                return _StudentName;
            }
        }
    }
    #endregion
}
