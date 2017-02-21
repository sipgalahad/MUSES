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
    #region GetARStudentPerDate
    public partial class GetARStudentPerDate
    {
        public Decimal Total
        {
            get
            {
                return _Col1 + _Col2 + _Col3 + _ColPse2;
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
        public String cfClassHeader
        {
            get
            {
                if (_IsHeader) return "tdEntityHeader";
                return "tdEntityDetail";
            }
        }
    }
    #endregion
    #region GetGLBalanceProfitLossPerPeriodPerSite
    public partial class GetGLBalanceProfitLossPerPeriodPerSite
    {
        public Decimal cfCol1 { get { if (_AccountLevel == 1) return _Col1; return 0; } }
        public Decimal cfCol2 { get { if (_AccountLevel == 1) return _Col2; return 0; } }
        public Decimal cfCol3 { get { if (_AccountLevel == 1) return _Col3; return 0; } }
        public Decimal cfCol4 { get { if (_AccountLevel == 1) return _Col4; return 0; } }
        public Decimal cfCol5 { get { if (_AccountLevel == 1) return _Col5; return 0; } }
        public Decimal cfCol6 { get { if (_AccountLevel == 1) return _Col6; return 0; } }
        public Decimal cfCol7 { get { if (_AccountLevel == 1) return _Col7; return 0; } }
        public Decimal cfCol8 { get { if (_AccountLevel == 1) return _Col8; return 0; } }
        public Decimal cfCol9 { get { if (_AccountLevel == 1) return _Col9; return 0; } }
        public Decimal cfCol10 { get { if (_AccountLevel == 1) return _Col10; return 0; } }
        public Decimal cfCol11 { get { if (_AccountLevel == 1) return _Col11; return 0; } }
        public Decimal cfColSU9 { get { if (_AccountLevel == 1) return _ColSU9; return 0; } }
        public Decimal cfColSU10 { get { if (_AccountLevel == 1) return _ColSU10; return 0; } }
        public Decimal cfColSU11 { get { if (_AccountLevel == 1) return _ColSU11; return 0; } }
        public Decimal cfColSU12 { get { if (_AccountLevel == 1) return _ColSU12; return 0; } }
        public Decimal cfColSU13 { get { if (_AccountLevel == 1) return _ColSU13; return 0; } }
        public Decimal cfColSU14 { get { if (_AccountLevel == 1) return _ColSU14; return 0; } }
        public Decimal cfColSU15 { get { if (_AccountLevel == 1) return _ColSU15; return 0; } }


        public String cfClassHeader
        {
            get
            {
                if (_IsHeader) return "tdEntityHeader";
                return "tdEntityDetail";
            }
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
    #region GetItemUsageItemRequestROPList
    public partial class GetItemUsageItemRequestROPList
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
    #region GetItemUsagePurchaseRequestROPList
    public partial class GetItemUsagePurchaseRequestROPList
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
