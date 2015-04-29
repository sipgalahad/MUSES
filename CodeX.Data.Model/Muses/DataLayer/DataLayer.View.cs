using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeX.Data.Core.Dal;

namespace CodeX.Data.Model
{
    #region vAdmissionFeeComp
    [Serializable]
    [Table(Name = "vAdmissionFeeComp")]
    public class vAdmissionFeeComp
    {
        private Int32 _AdmissionFeeCompID;
        private Int32 _SchoolPeriodID;
        private Int32 _StudentFeeCompTypeID;
        private String _StudentFeeCompTypeName;
        private String _ShortName;
        private String _GCAdmissionPaymentPeriod;
        private String _AdmissionPaymentPeriod;
        private Boolean _IsFixedAmount;
        private Int16 _PenaltyPercentage;
        private Decimal _TotalAmount;
        private Int16 _NoOfRegistrationPaymentPeriod;
        private Boolean _IsDeleted;

        [Column(Name = "AdmissionFeeCompID", DataType = "Int32")]
        public Int32 AdmissionFeeCompID
        {
            get { return _AdmissionFeeCompID; }
            set { _AdmissionFeeCompID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "StudentFeeCompTypeID", DataType = "Int32")]
        public Int32 StudentFeeCompTypeID
        {
            get { return _StudentFeeCompTypeID; }
            set { _StudentFeeCompTypeID = value; }
        }
        [Column(Name = "StudentFeeCompTypeName", DataType = "String")]
        public String StudentFeeCompTypeName
        {
            get { return _StudentFeeCompTypeName; }
            set { _StudentFeeCompTypeName = value; }
        }
        [Column(Name = "ShortName", DataType = "String")]
        public String ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }
        [Column(Name = "GCAdmissionPaymentPeriod", DataType = "String")]
        public String GCAdmissionPaymentPeriod
        {
            get { return _GCAdmissionPaymentPeriod; }
            set { _GCAdmissionPaymentPeriod = value; }
        }
        [Column(Name = "AdmissionPaymentPeriod", DataType = "String")]
        public String AdmissionPaymentPeriod
        {
            get { return _AdmissionPaymentPeriod; }
            set { _AdmissionPaymentPeriod = value; }
        }
        [Column(Name = "IsFixedAmount", DataType = "Boolean")]
        public Boolean IsFixedAmount
        {
            get { return _IsFixedAmount; }
            set { _IsFixedAmount = value; }
        }
        [Column(Name = "PenaltyPercentage", DataType = "Int16")]
        public Int16 PenaltyPercentage
        {
            get { return _PenaltyPercentage; }
            set { _PenaltyPercentage = value; }
        }
        [Column(Name = "TotalAmount", DataType = "Decimal")]
        public Decimal TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
        [Column(Name = "NoOfRegistrationPaymentPeriod", DataType = "Int16")]
        public Int16 NoOfRegistrationPaymentPeriod
        {
            get { return _NoOfRegistrationPaymentPeriod; }
            set { _NoOfRegistrationPaymentPeriod = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vAdmissionFeeRuleDtCustom
    [Serializable]
    [Table(Name = "vAdmissionFeeRuleDtCustom")]
    public partial class vAdmissionFeeRuleDtCustom
    {
        private Int32 _AdmissionFeeCompID;
        private Int32 _SchoolPeriodID;
        private Int32 _StudentFeeCompTypeID;
        private String _StudentFeeCompTypeName;
        private String _GCAdmissionPaymentPeriod;
        private String _AdmissionPaymentPeriod;
        private Boolean _IsFixedAmount;
        private Decimal _TotalAmount;
        private Int16 _NoOfRegistrationPaymentPeriod;
        private Int32 _AdmissionFeeRuleID;
        private Int32 _PeriodAdmissionID;
        private Boolean _IsDeleted;

        [Column(Name = "AdmissionFeeCompID", DataType = "Int32")]
        public Int32 AdmissionFeeCompID
        {
            get { return _AdmissionFeeCompID; }
            set { _AdmissionFeeCompID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "StudentFeeCompTypeID", DataType = "Int32")]
        public Int32 StudentFeeCompTypeID
        {
            get { return _StudentFeeCompTypeID; }
            set { _StudentFeeCompTypeID = value; }
        }
        [Column(Name = "StudentFeeCompTypeName", DataType = "String")]
        public String StudentFeeCompTypeName
        {
            get { return _StudentFeeCompTypeName; }
            set { _StudentFeeCompTypeName = value; }
        }
        [Column(Name = "GCAdmissionPaymentPeriod", DataType = "String")]
        public String GCAdmissionPaymentPeriod
        {
            get { return _GCAdmissionPaymentPeriod; }
            set { _GCAdmissionPaymentPeriod = value; }
        }
        [Column(Name = "AdmissionPaymentPeriod", DataType = "String")]
        public String AdmissionPaymentPeriod
        {
            get { return _AdmissionPaymentPeriod; }
            set { _AdmissionPaymentPeriod = value; }
        }
        [Column(Name = "IsFixedAmount", DataType = "Boolean")]
        public Boolean IsFixedAmount
        {
            get { return _IsFixedAmount; }
            set { _IsFixedAmount = value; }
        }
        [Column(Name = "TotalAmount", DataType = "Decimal")]
        public Decimal TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
        [Column(Name = "NoOfRegistrationPaymentPeriod", DataType = "Int16")]
        public Int16 NoOfRegistrationPaymentPeriod
        {
            get { return _NoOfRegistrationPaymentPeriod; }
            set { _NoOfRegistrationPaymentPeriod = value; }
        }
        [Column(Name = "AdmissionFeeRuleID", DataType = "Int32")]
        public Int32 AdmissionFeeRuleID
        {
            get { return _AdmissionFeeRuleID; }
            set { _AdmissionFeeRuleID = value; }
        }
        [Column(Name = "PeriodAdmissionID", DataType = "Int32")]
        public Int32 PeriodAdmissionID
        {
            get { return _PeriodAdmissionID; }
            set { _PeriodAdmissionID = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vAdmissionFeeRuleHd
    [Serializable]
    [Table(Name = "vAdmissionFeeRuleHd")]
    public class vAdmissionFeeRuleHd
    {
        private Int32 _AdmissionFeeRuleID;
        private Int32 _SchoolPeriodID;
        private String _AdmissionFeeRuleName;
        private String _GCFromSchoolType;
        private String _FromSchoolType;
        private Boolean _IsDeleted;

        [Column(Name = "AdmissionFeeRuleID", DataType = "Int32")]
        public Int32 AdmissionFeeRuleID
        {
            get { return _AdmissionFeeRuleID; }
            set { _AdmissionFeeRuleID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "AdmissionFeeRuleName", DataType = "String")]
        public String AdmissionFeeRuleName
        {
            get { return _AdmissionFeeRuleName; }
            set { _AdmissionFeeRuleName = value; }
        }
        [Column(Name = "GCFromSchoolType", DataType = "String")]
        public String GCFromSchoolType
        {
            get { return _GCFromSchoolType; }
            set { _GCFromSchoolType = value; }
        }
        [Column(Name = "FromSchoolType", DataType = "String")]
        public String FromSchoolType
        {
            get { return _FromSchoolType; }
            set { _FromSchoolType = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vAPMovement
    [Serializable]
    [Table(Name = "vAPMovement")]
    public partial class vAPMovement
    {
        private Int32 _MovementID;
        private DateTime _MovementDate;
        private String _TransactionCode;
        private Int32 _TransactionID;
        private String _TransactionNo;
        private Int32 _TransactionDtID;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private String _TransactionDescription;
        private String _DetailDesc;
        private Decimal _BalanceBEGIN;
        private Decimal _BalanceIN;
        private Decimal _BalanceOUT;
        private Decimal _BalanceEND;
        private Int32 _CreatedBy;
        private String _CreatedByName;
        private DateTime _CreatedDate;
        private Int32 _LastUpdatedBy;
        private String _LastUpdatedByName;
        private DateTime _LastUpdatedDate;

        [Column(Name = "MovementID", DataType = "Int32")]
        public Int32 MovementID
        {
            get { return _MovementID; }
            set { _MovementID = value; }
        }
        [Column(Name = "MovementDate", DataType = "DateTime")]
        public DateTime MovementDate
        {
            get { return _MovementDate; }
            set { _MovementDate = value; }
        }
        [Column(Name = "TransactionCode", DataType = "String")]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
        [Column(Name = "TransactionID", DataType = "Int32")]
        public Int32 TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }
        [Column(Name = "TransactionNo", DataType = "String")]
        public String TransactionNo
        {
            get { return _TransactionNo; }
            set { _TransactionNo = value; }
        }
        [Column(Name = "TransactionDtID", DataType = "Int32")]
        public Int32 TransactionDtID
        {
            get { return _TransactionDtID; }
            set { _TransactionDtID = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "TransactionDescription", DataType = "String")]
        public String TransactionDescription
        {
            get { return _TransactionDescription; }
            set { _TransactionDescription = value; }
        }
        [Column(Name = "DetailDesc", DataType = "String")]
        public String DetailDesc
        {
            get { return _DetailDesc; }
            set { _DetailDesc = value; }
        }
        [Column(Name = "BalanceBEGIN", DataType = "Decimal")]
        public Decimal BalanceBEGIN
        {
            get { return _BalanceBEGIN; }
            set { _BalanceBEGIN = value; }
        }
        [Column(Name = "BalanceIN", DataType = "Decimal")]
        public Decimal BalanceIN
        {
            get { return _BalanceIN; }
            set { _BalanceIN = value; }
        }
        [Column(Name = "BalanceOUT", DataType = "Decimal")]
        public Decimal BalanceOUT
        {
            get { return _BalanceOUT; }
            set { _BalanceOUT = value; }
        }
        [Column(Name = "BalanceEND", DataType = "Decimal")]
        public Decimal BalanceEND
        {
            get { return _BalanceEND; }
            set { _BalanceEND = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedByName", DataType = "String")]
        public String CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32")]
        public Int32 LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedByName", DataType = "String")]
        public String LastUpdatedByName
        {
            get { return _LastUpdatedByName; }
            set { _LastUpdatedByName = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime")]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }
    #endregion
    #region vARInvoiceDt
    [Serializable]
    [Table(Name = "vARInvoiceDt")]
    public partial class vARInvoiceDt
    {
        private Int32 _ARInvoiceDtID;
        private Int32 _ARInvoiceID;
        private String _ARInvoiceNo;
        private DateTime _ARInvoiceDate;
        private DateTime _DueDate;
        private Int32 _StudentFeeDtID;
        private Int32 _StudentFeeCompTypeID;
        private String _StudentFeeCompTypeName;
        private Int32 _TransactionMonth;
        private Int32 _TransactionYear;
        private String _GCAdmissionPaymentPeriod;
        private String _SFCTShortName;
        private String _GCTransactionStatus;
        private String _TransactionStatus;
        private Decimal _TransactionAmount;
        private Decimal _PenaltyAmount;
        private Decimal _LineAmount;
        private Decimal _ClaimedAmount;
        private Decimal _DiscountAmount;
        private Decimal _VarianceAmount;
        private String _ReferenceNo;
        private Int32 _StudentID;
        private String _StudentCode;
        private String _StudentName;
        private Int32 _ProspectiveStudentID;
        private String _ProspectiveStudentCode;
        private String _ProspectiveStudentName;
        private Int32 _PayedStudentID;
        private String _PayedStudentCode;
        private String _PayedStudentName;
        private Int32 _BankID;
        private Boolean _IsTransferred;
        private String _Remarks;

        [Column(Name = "ARInvoiceDtID", DataType = "Int32")]
        public Int32 ARInvoiceDtID
        {
            get { return _ARInvoiceDtID; }
            set { _ARInvoiceDtID = value; }
        }
        [Column(Name = "ARInvoiceID", DataType = "Int32")]
        public Int32 ARInvoiceID
        {
            get { return _ARInvoiceID; }
            set { _ARInvoiceID = value; }
        }
        [Column(Name = "ARInvoiceNo", DataType = "String")]
        public String ARInvoiceNo
        {
            get { return _ARInvoiceNo; }
            set { _ARInvoiceNo = value; }
        }
        [Column(Name = "ARInvoiceDate", DataType = "DateTime")]
        public DateTime ARInvoiceDate
        {
            get { return _ARInvoiceDate; }
            set { _ARInvoiceDate = value; }
        }
        [Column(Name = "DueDate", DataType = "DateTime")]
        public DateTime DueDate
        {
            get { return _DueDate; }
            set { _DueDate = value; }
        }
        [Column(Name = "StudentFeeDtID", DataType = "Int32")]
        public Int32 StudentFeeDtID
        {
            get { return _StudentFeeDtID; }
            set { _StudentFeeDtID = value; }
        }
        [Column(Name = "StudentFeeCompTypeID", DataType = "Int32")]
        public Int32 StudentFeeCompTypeID
        {
            get { return _StudentFeeCompTypeID; }
            set { _StudentFeeCompTypeID = value; }
        }
        [Column(Name = "StudentFeeCompTypeName", DataType = "String")]
        public String StudentFeeCompTypeName
        {
            get { return _StudentFeeCompTypeName; }
            set { _StudentFeeCompTypeName = value; }
        }
        [Column(Name = "TransactionMonth", DataType = "Int32")]
        public Int32 TransactionMonth
        {
            get { return _TransactionMonth; }
            set { _TransactionMonth = value; }
        }
        [Column(Name = "TransactionYear", DataType = "Int32")]
        public Int32 TransactionYear
        {
            get { return _TransactionYear; }
            set { _TransactionYear = value; }
        }
        [Column(Name = "GCAdmissionPaymentPeriod", DataType = "String")]
        public String GCAdmissionPaymentPeriod
        {
            get { return _GCAdmissionPaymentPeriod; }
            set { _GCAdmissionPaymentPeriod = value; }
        }
        [Column(Name = "SFCTShortName", DataType = "String")]
        public String SFCTShortName
        {
            get { return _SFCTShortName; }
            set { _SFCTShortName = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatus", DataType = "String")]
        public String TransactionStatus
        {
            get { return _TransactionStatus; }
            set { _TransactionStatus = value; }
        }
        [Column(Name = "TransactionAmount", DataType = "Decimal")]
        public Decimal TransactionAmount
        {
            get { return _TransactionAmount; }
            set { _TransactionAmount = value; }
        }
        [Column(Name = "PenaltyAmount", DataType = "Decimal")]
        public Decimal PenaltyAmount
        {
            get { return _PenaltyAmount; }
            set { _PenaltyAmount = value; }
        }
        [Column(Name = "LineAmount", DataType = "Decimal")]
        public Decimal LineAmount
        {
            get { return _LineAmount; }
            set { _LineAmount = value; }
        }
        [Column(Name = "ClaimedAmount", DataType = "Decimal")]
        public Decimal ClaimedAmount
        {
            get { return _ClaimedAmount; }
            set { _ClaimedAmount = value; }
        }
        [Column(Name = "DiscountAmount", DataType = "Decimal")]
        public Decimal DiscountAmount
        {
            get { return _DiscountAmount; }
            set { _DiscountAmount = value; }
        }
        [Column(Name = "VarianceAmount", DataType = "Decimal")]
        public Decimal VarianceAmount
        {
            get { return _VarianceAmount; }
            set { _VarianceAmount = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String")]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "StudentCode", DataType = "String")]
        public String StudentCode
        {
            get { return _StudentCode; }
            set { _StudentCode = value; }
        }
        [Column(Name = "StudentName", DataType = "String")]
        public String StudentName
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }
        [Column(Name = "ProspectiveStudentID", DataType = "Int32")]
        public Int32 ProspectiveStudentID
        {
            get { return _ProspectiveStudentID; }
            set { _ProspectiveStudentID = value; }
        }
        [Column(Name = "ProspectiveStudentCode", DataType = "String")]
        public String ProspectiveStudentCode
        {
            get { return _ProspectiveStudentCode; }
            set { _ProspectiveStudentCode = value; }
        }
        [Column(Name = "ProspectiveStudentName", DataType = "String")]
        public String ProspectiveStudentName
        {
            get { return _ProspectiveStudentName; }
            set { _ProspectiveStudentName = value; }
        }
        [Column(Name = "PayedStudentID", DataType = "Int32")]
        public Int32 PayedStudentID
        {
            get { return _PayedStudentID; }
            set { _PayedStudentID = value; }
        }
        [Column(Name = "PayedStudentCode", DataType = "String")]
        public String PayedStudentCode
        {
            get { return _PayedStudentCode; }
            set { _PayedStudentCode = value; }
        }
        [Column(Name = "PayedStudentName", DataType = "String")]
        public String PayedStudentName
        {
            get { return _PayedStudentName; }
            set { _PayedStudentName = value; }
        }
        [Column(Name = "BankID", DataType = "Int32")]
        public Int32 BankID
        {
            get { return _BankID; }
            set { _BankID = value; }
        }
        [Column(Name = "IsTransferred", DataType = "Boolean")]
        public Boolean IsTransferred
        {
            get { return _IsTransferred; }
            set { _IsTransferred = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
    }
    #endregion
    #region vARInvoiceHd
    [Serializable]
    [Table(Name = "vARInvoiceHd")]
    public partial class vARInvoiceHd
    {
        private Int32 _ARInvoiceID;
        private String _ARInvoiceNo;
        private DateTime _ARInvoiceDate;
        private Int32 _BankID;
        private DateTime _DueDate;
        private Decimal _TotalPaymentAmount;
        private Int32 _StudentID;
        private String _StudentCode;
        private String _StudentName;
        private Int32 _ProspectiveStudentID;
        private String _ProspectiveStudentCode;
        private String _ProspectiveStudentName;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private String _VirtualAccount;
        private String _GCTransactionStatus;
        private String _TransactionStatus;
        private String _TransactionStatusWatermark;
        private Decimal _TotalTransactionAmount;
        private Decimal _TotalClaimedAmount;
        private Decimal _TotalDiscountAmount;
        private Decimal _TotalVarianceAmount;
        private Int32 _TermID;
        private String _Remarks;

        [Column(Name = "ARInvoiceID", DataType = "Int32")]
        public Int32 ARInvoiceID
        {
            get { return _ARInvoiceID; }
            set { _ARInvoiceID = value; }
        }
        [Column(Name = "ARInvoiceNo", DataType = "String")]
        public String ARInvoiceNo
        {
            get { return _ARInvoiceNo; }
            set { _ARInvoiceNo = value; }
        }
        [Column(Name = "ARInvoiceDate", DataType = "DateTime")]
        public DateTime ARInvoiceDate
        {
            get { return _ARInvoiceDate; }
            set { _ARInvoiceDate = value; }
        }
        [Column(Name = "BankID", DataType = "Int32")]
        public Int32 BankID
        {
            get { return _BankID; }
            set { _BankID = value; }
        }
        [Column(Name = "DueDate", DataType = "DateTime")]
        public DateTime DueDate
        {
            get { return _DueDate; }
            set { _DueDate = value; }
        }
        [Column(Name = "TotalPaymentAmount", DataType = "Decimal")]
        public Decimal TotalPaymentAmount
        {
            get { return _TotalPaymentAmount; }
            set { _TotalPaymentAmount = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "StudentCode", DataType = "String")]
        public String StudentCode
        {
            get { return _StudentCode; }
            set { _StudentCode = value; }
        }
        [Column(Name = "StudentName", DataType = "String")]
        public String StudentName
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }
        [Column(Name = "ProspectiveStudentID", DataType = "Int32")]
        public Int32 ProspectiveStudentID
        {
            get { return _ProspectiveStudentID; }
            set { _ProspectiveStudentID = value; }
        }
        [Column(Name = "ProspectiveStudentCode", DataType = "String")]
        public String ProspectiveStudentCode
        {
            get { return _ProspectiveStudentCode; }
            set { _ProspectiveStudentCode = value; }
        }
        [Column(Name = "ProspectiveStudentName", DataType = "String")]
        public String ProspectiveStudentName
        {
            get { return _ProspectiveStudentName; }
            set { _ProspectiveStudentName = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "VirtualAccount", DataType = "String")]
        public String VirtualAccount
        {
            get { return _VirtualAccount; }
            set { _VirtualAccount = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatus", DataType = "String")]
        public String TransactionStatus
        {
            get { return _TransactionStatus; }
            set { _TransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
        [Column(Name = "TotalTransactionAmount", DataType = "Decimal")]
        public Decimal TotalTransactionAmount
        {
            get { return _TotalTransactionAmount; }
            set { _TotalTransactionAmount = value; }
        }
        [Column(Name = "TotalClaimedAmount", DataType = "Decimal")]
        public Decimal TotalClaimedAmount
        {
            get { return _TotalClaimedAmount; }
            set { _TotalClaimedAmount = value; }
        }
        [Column(Name = "TotalDiscountAmount", DataType = "Decimal")]
        public Decimal TotalDiscountAmount
        {
            get { return _TotalDiscountAmount; }
            set { _TotalDiscountAmount = value; }
        }
        [Column(Name = "TotalVarianceAmount", DataType = "Decimal")]
        public Decimal TotalVarianceAmount
        {
            get { return _TotalVarianceAmount; }
            set { _TotalVarianceAmount = value; }
        }
        [Column(Name = "TermID", DataType = "Int32")]
        public Int32 TermID
        {
            get { return _TermID; }
            set { _TermID = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
    }
    #endregion
    #region vARMovement
    [Serializable]
    [Table(Name = "vARMovement")]
    public partial class vARMovement
    {
        private Int32 _MovementID;
        private DateTime _MovementDate;
        private String _TransactionCode;
        private Int32 _TransactionID;
        private String _TransactionNo;
        private Int32 _TransactionDtID;
        private Int32 _StudentID;
        private String _StudentCode;
        private String _StudentName;
        private Int32 _ProspectiveStudentID;
        private String _ProspectiveStudentCode;
        private String _ProspectiveStudentName;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private String _TransactionDescription;
        private String _DetailDesc;
        private Decimal _BalanceBEGIN;
        private Decimal _BalanceIN;
        private Decimal _BalanceOUT;
        private Decimal _BalanceEND;
        private Int32 _CreatedBy;
        private String _CreatedByName;
        private DateTime _CreatedDate;
        private Int32 _LastUpdatedBy;
        private String _LastUpdatedByName;
        private DateTime _LastUpdatedDate;

        [Column(Name = "MovementID", DataType = "Int32")]
        public Int32 MovementID
        {
            get { return _MovementID; }
            set { _MovementID = value; }
        }
        [Column(Name = "MovementDate", DataType = "DateTime")]
        public DateTime MovementDate
        {
            get { return _MovementDate; }
            set { _MovementDate = value; }
        }
        [Column(Name = "TransactionCode", DataType = "String")]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
        [Column(Name = "TransactionID", DataType = "Int32")]
        public Int32 TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }
        [Column(Name = "TransactionNo", DataType = "String")]
        public String TransactionNo
        {
            get { return _TransactionNo; }
            set { _TransactionNo = value; }
        }
        [Column(Name = "TransactionDtID", DataType = "Int32")]
        public Int32 TransactionDtID
        {
            get { return _TransactionDtID; }
            set { _TransactionDtID = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "StudentCode", DataType = "String")]
        public String StudentCode
        {
            get { return _StudentCode; }
            set { _StudentCode = value; }
        }
        [Column(Name = "StudentName", DataType = "String")]
        public String StudentName
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }
        [Column(Name = "ProspectiveStudentID", DataType = "Int32")]
        public Int32 ProspectiveStudentID
        {
            get { return _ProspectiveStudentID; }
            set { _ProspectiveStudentID = value; }
        }
        [Column(Name = "ProspectiveStudentCode", DataType = "String")]
        public String ProspectiveStudentCode
        {
            get { return _ProspectiveStudentCode; }
            set { _ProspectiveStudentCode = value; }
        }
        [Column(Name = "ProspectiveStudentName", DataType = "String")]
        public String ProspectiveStudentName
        {
            get { return _ProspectiveStudentName; }
            set { _ProspectiveStudentName = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "TransactionDescription", DataType = "String")]
        public String TransactionDescription
        {
            get { return _TransactionDescription; }
            set { _TransactionDescription = value; }
        }
        [Column(Name = "DetailDesc", DataType = "String")]
        public String DetailDesc
        {
            get { return _DetailDesc; }
            set { _DetailDesc = value; }
        }
        [Column(Name = "BalanceBEGIN", DataType = "Decimal")]
        public Decimal BalanceBEGIN
        {
            get { return _BalanceBEGIN; }
            set { _BalanceBEGIN = value; }
        }
        [Column(Name = "BalanceIN", DataType = "Decimal")]
        public Decimal BalanceIN
        {
            get { return _BalanceIN; }
            set { _BalanceIN = value; }
        }
        [Column(Name = "BalanceOUT", DataType = "Decimal")]
        public Decimal BalanceOUT
        {
            get { return _BalanceOUT; }
            set { _BalanceOUT = value; }
        }
        [Column(Name = "BalanceEND", DataType = "Decimal")]
        public Decimal BalanceEND
        {
            get { return _BalanceEND; }
            set { _BalanceEND = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedByName", DataType = "String")]
        public String CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32")]
        public Int32 LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedByName", DataType = "String")]
        public String LastUpdatedByName
        {
            get { return _LastUpdatedByName; }
            set { _LastUpdatedByName = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime")]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }
    #endregion
    #region vARReceivingDt
    [Serializable]
    [Table(Name = "vARReceivingDt")]
    public partial class vARReceivingDt
    {
        private Int32 _ARReceivingDetailID;
        private Int32 _ARReceivingID;
        private String _GCARPaymentMethod;
        private String _ARPaymentMethod;
        private Int32 _EDCMachineID;
        private String _EDCMachineName;
        private String _GCCardType;
        private String _GCCardProvider;
        private String _CardNumber;
        private String _CardHolderName;
        private String _CardValidThru;
        private Int32 _BankID;
        private String _BankName;
        private String _ReferenceNo;
        private DateTime _ReferenceDate;
        private Decimal _PaymentAmount;
        private Decimal _CardFeeAmount;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "ARReceivingDetailID", DataType = "Int32")]
        public Int32 ARReceivingDetailID
        {
            get { return _ARReceivingDetailID; }
            set { _ARReceivingDetailID = value; }
        }
        [Column(Name = "ARReceivingID", DataType = "Int32")]
        public Int32 ARReceivingID
        {
            get { return _ARReceivingID; }
            set { _ARReceivingID = value; }
        }
        [Column(Name = "GCARPaymentMethod", DataType = "String")]
        public String GCARPaymentMethod
        {
            get { return _GCARPaymentMethod; }
            set { _GCARPaymentMethod = value; }
        }
        [Column(Name = "ARPaymentMethod", DataType = "String")]
        public String ARPaymentMethod
        {
            get { return _ARPaymentMethod; }
            set { _ARPaymentMethod = value; }
        }
        [Column(Name = "EDCMachineID", DataType = "Int32")]
        public Int32 EDCMachineID
        {
            get { return _EDCMachineID; }
            set { _EDCMachineID = value; }
        }
        [Column(Name = "EDCMachineName", DataType = "String")]
        public String EDCMachineName
        {
            get { return _EDCMachineName; }
            set { _EDCMachineName = value; }
        }
        [Column(Name = "GCCardType", DataType = "String")]
        public String GCCardType
        {
            get { return _GCCardType; }
            set { _GCCardType = value; }
        }
        [Column(Name = "GCCardProvider", DataType = "String")]
        public String GCCardProvider
        {
            get { return _GCCardProvider; }
            set { _GCCardProvider = value; }
        }
        [Column(Name = "CardNumber", DataType = "String")]
        public String CardNumber
        {
            get { return _CardNumber; }
            set { _CardNumber = value; }
        }
        [Column(Name = "CardHolderName", DataType = "String")]
        public String CardHolderName
        {
            get { return _CardHolderName; }
            set { _CardHolderName = value; }
        }
        [Column(Name = "CardValidThru", DataType = "String")]
        public String CardValidThru
        {
            get { return _CardValidThru; }
            set { _CardValidThru = value; }
        }
        [Column(Name = "BankID", DataType = "Int32")]
        public Int32 BankID
        {
            get { return _BankID; }
            set { _BankID = value; }
        }
        [Column(Name = "BankName", DataType = "String")]
        public String BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String")]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "ReferenceDate", DataType = "DateTime")]
        public DateTime ReferenceDate
        {
            get { return _ReferenceDate; }
            set { _ReferenceDate = value; }
        }
        [Column(Name = "PaymentAmount", DataType = "Decimal")]
        public Decimal PaymentAmount
        {
            get { return _PaymentAmount; }
            set { _PaymentAmount = value; }
        }
        [Column(Name = "CardFeeAmount", DataType = "Decimal")]
        public Decimal CardFeeAmount
        {
            get { return _CardFeeAmount; }
            set { _CardFeeAmount = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vARReceivingHd
    [Serializable]
    [Table(Name = "vARReceivingHd")]
    public partial class vARReceivingHd
    {
        private Int32 _ARReceivingID;
        private String _ARReceivingNo;
        private DateTime _ReceivingDate;
        private Int32 _StudentID;
        private String _StudentCode;
        private String _StudentName;
        private Int32 _ProspectiveStudentID;
        private String _ProspectiveStudentCode;
        private String _ProspectiveStudentName;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private Decimal _TotalReceivingAmount;
        private Decimal _TotalFeeAmount;
        private Decimal _TotalInvoiceAmount;
        private Decimal _CashBackAmount;
        private String _Remarks;
        private String _GCTransactionStatus;
        private Boolean _IsDeleted;
        private String _InvoiceNo;
        private String _TransactionStatusWatermark;

        [Column(Name = "ARReceivingID", DataType = "Int32")]
        public Int32 ARReceivingID
        {
            get { return _ARReceivingID; }
            set { _ARReceivingID = value; }
        }
        [Column(Name = "ARReceivingNo", DataType = "String")]
        public String ARReceivingNo
        {
            get { return _ARReceivingNo; }
            set { _ARReceivingNo = value; }
        }
        [Column(Name = "ReceivingDate", DataType = "DateTime")]
        public DateTime ReceivingDate
        {
            get { return _ReceivingDate; }
            set { _ReceivingDate = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "StudentCode", DataType = "String")]
        public String StudentCode
        {
            get { return _StudentCode; }
            set { _StudentCode = value; }
        }
        [Column(Name = "StudentName", DataType = "String")]
        public String StudentName
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }
        [Column(Name = "ProspectiveStudentID", DataType = "Int32")]
        public Int32 ProspectiveStudentID
        {
            get { return _ProspectiveStudentID; }
            set { _ProspectiveStudentID = value; }
        }
        [Column(Name = "ProspectiveStudentCode", DataType = "String")]
        public String ProspectiveStudentCode
        {
            get { return _ProspectiveStudentCode; }
            set { _ProspectiveStudentCode = value; }
        }
        [Column(Name = "ProspectiveStudentName", DataType = "String")]
        public String ProspectiveStudentName
        {
            get { return _ProspectiveStudentName; }
            set { _ProspectiveStudentName = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "TotalReceivingAmount", DataType = "Decimal")]
        public Decimal TotalReceivingAmount
        {
            get { return _TotalReceivingAmount; }
            set { _TotalReceivingAmount = value; }
        }
        [Column(Name = "TotalFeeAmount", DataType = "Decimal")]
        public Decimal TotalFeeAmount
        {
            get { return _TotalFeeAmount; }
            set { _TotalFeeAmount = value; }
        }
        [Column(Name = "TotalInvoiceAmount", DataType = "Decimal")]
        public Decimal TotalInvoiceAmount
        {
            get { return _TotalInvoiceAmount; }
            set { _TotalInvoiceAmount = value; }
        }
        [Column(Name = "CashBackAmount", DataType = "Decimal")]
        public Decimal CashBackAmount
        {
            get { return _CashBackAmount; }
            set { _CashBackAmount = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "InvoiceNo", DataType = "String")]
        public String InvoiceNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
    }
    #endregion
    #region vBank
    [Serializable]
    [Table(Name = "vBank")]
    public class vBank
    {
        private Int32 _BankID;
        private String _BankCode;
        private String _BankName;
        private String _BankAccountNo;
        private String _BankAccountName;
        private String _SiteID;
        private String _SiteName;
        private Decimal _AdministrationAmount;
        private Boolean _IsDeleted;

        [Column(Name = "BankID", DataType = "Int32")]
        public Int32 BankID
        {
            get { return _BankID; }
            set { _BankID = value; }
        }
        [Column(Name = "BankCode", DataType = "String")]
        public String BankCode
        {
            get { return _BankCode; }
            set { _BankCode = value; }
        }
        [Column(Name = "BankName", DataType = "String")]
        public String BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }
        [Column(Name = "BankAccountNo", DataType = "String")]
        public String BankAccountNo
        {
            get { return _BankAccountNo; }
            set { _BankAccountNo = value; }
        }
        [Column(Name = "BankAccountName", DataType = "String")]
        public String BankAccountName
        {
            get { return _BankAccountName; }
            set { _BankAccountName = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "SiteName", DataType = "String")]
        public String SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }
        [Column(Name = "AdministrationAmount", DataType = "Decimal")]
        public Decimal AdministrationAmount
        {
            get { return _AdministrationAmount; }
            set { _AdministrationAmount = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vChartOfAccount
    [Serializable]
    [Table(Name = "vChartOfAccount")]
    public partial class vChartOfAccount
    {
        private Int32 _GLAccountID;
        private String _SiteID;
        private String _GLAccountNo;
        private String _GLAccountName;
        private Int32 _ParentGLAccount;
        private String _ParentGLAccountNo;
        private String _ParentGLAccountName;
        private String _GCGLAccountType;
        private String _GLAccountType;
        private Int32 _COAGroupID;
        private String _COAGroupCode;
        private String _COAGroupName;
        private Int32 _SubLedgerID;
        private String _SubLedgerCode;
        private String _SubLedgerName;
        private Int32 _SubLedgerTypeID;
        private String _SubLedgerTypeCode;
        private String _SubLedgerTypeName;
        private String _MethodName;
        private String _FilterExpression;
        private String _IDFieldName;
        private String _CodeFieldName;
        private String _DisplayFieldName;
        private String _SearchDialogTypeName;
        private String _TableName;
        private String _Position;
        private Boolean _IsHeader;
        private Int16 _AccountLevel;
        private Boolean _IsUsingDocumentControl;
        private Int32 _Level;
        private String _Path;
        private Boolean _IsDeleted;

        [Column(Name = "GLAccountID", DataType = "Int32")]
        public Int32 GLAccountID
        {
            get { return _GLAccountID; }
            set { _GLAccountID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "GLAccountNo", DataType = "String")]
        public String GLAccountNo
        {
            get { return _GLAccountNo; }
            set { _GLAccountNo = value; }
        }
        [Column(Name = "GLAccountName", DataType = "String")]
        public String GLAccountName
        {
            get { return _GLAccountName; }
            set { _GLAccountName = value; }
        }
        [Column(Name = "ParentGLAccount", DataType = "Int32")]
        public Int32 ParentGLAccount
        {
            get { return _ParentGLAccount; }
            set { _ParentGLAccount = value; }
        }
        [Column(Name = "ParentGLAccountNo", DataType = "String")]
        public String ParentGLAccountNo
        {
            get { return _ParentGLAccountNo; }
            set { _ParentGLAccountNo = value; }
        }
        [Column(Name = "ParentGLAccountName", DataType = "String")]
        public String ParentGLAccountName
        {
            get { return _ParentGLAccountName; }
            set { _ParentGLAccountName = value; }
        }
        [Column(Name = "GCGLAccountType", DataType = "String")]
        public String GCGLAccountType
        {
            get { return _GCGLAccountType; }
            set { _GCGLAccountType = value; }
        }
        [Column(Name = "GLAccountType", DataType = "String")]
        public String GLAccountType
        {
            get { return _GLAccountType; }
            set { _GLAccountType = value; }
        }
        [Column(Name = "COAGroupID", DataType = "Int32")]
        public Int32 COAGroupID
        {
            get { return _COAGroupID; }
            set { _COAGroupID = value; }
        }
        [Column(Name = "COAGroupCode", DataType = "String")]
        public String COAGroupCode
        {
            get { return _COAGroupCode; }
            set { _COAGroupCode = value; }
        }
        [Column(Name = "COAGroupName", DataType = "String")]
        public String COAGroupName
        {
            get { return _COAGroupName; }
            set { _COAGroupName = value; }
        }
        [Column(Name = "SubLedgerID", DataType = "Int32")]
        public Int32 SubLedgerID
        {
            get { return _SubLedgerID; }
            set { _SubLedgerID = value; }
        }
        [Column(Name = "SubLedgerCode", DataType = "String")]
        public String SubLedgerCode
        {
            get { return _SubLedgerCode; }
            set { _SubLedgerCode = value; }
        }
        [Column(Name = "SubLedgerName", DataType = "String")]
        public String SubLedgerName
        {
            get { return _SubLedgerName; }
            set { _SubLedgerName = value; }
        }
        [Column(Name = "SubLedgerTypeID", DataType = "Int32")]
        public Int32 SubLedgerTypeID
        {
            get { return _SubLedgerTypeID; }
            set { _SubLedgerTypeID = value; }
        }
        [Column(Name = "SubLedgerTypeCode", DataType = "String")]
        public String SubLedgerTypeCode
        {
            get { return _SubLedgerTypeCode; }
            set { _SubLedgerTypeCode = value; }
        }
        [Column(Name = "SubLedgerTypeName", DataType = "String")]
        public String SubLedgerTypeName
        {
            get { return _SubLedgerTypeName; }
            set { _SubLedgerTypeName = value; }
        }
        [Column(Name = "MethodName", DataType = "String")]
        public String MethodName
        {
            get { return _MethodName; }
            set { _MethodName = value; }
        }
        [Column(Name = "FilterExpression", DataType = "String")]
        public String FilterExpression
        {
            get { return _FilterExpression; }
            set { _FilterExpression = value; }
        }
        [Column(Name = "IDFieldName", DataType = "String")]
        public String IDFieldName
        {
            get { return _IDFieldName; }
            set { _IDFieldName = value; }
        }
        [Column(Name = "CodeFieldName", DataType = "String")]
        public String CodeFieldName
        {
            get { return _CodeFieldName; }
            set { _CodeFieldName = value; }
        }
        [Column(Name = "DisplayFieldName", DataType = "String")]
        public String DisplayFieldName
        {
            get { return _DisplayFieldName; }
            set { _DisplayFieldName = value; }
        }
        [Column(Name = "SearchDialogTypeName", DataType = "String")]
        public String SearchDialogTypeName
        {
            get { return _SearchDialogTypeName; }
            set { _SearchDialogTypeName = value; }
        }
        [Column(Name = "TableName", DataType = "String")]
        public String TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        [Column(Name = "Position", DataType = "String")]
        public String Position
        {
            get { return _Position; }
            set { _Position = value; }
        }
        [Column(Name = "IsHeader", DataType = "Boolean")]
        public Boolean IsHeader
        {
            get { return _IsHeader; }
            set { _IsHeader = value; }
        }
        [Column(Name = "AccountLevel", DataType = "Int16")]
        public Int16 AccountLevel
        {
            get { return _AccountLevel; }
            set { _AccountLevel = value; }
        }
        [Column(Name = "IsUsingDocumentControl", DataType = "Boolean")]
        public Boolean IsUsingDocumentControl
        {
            get { return _IsUsingDocumentControl; }
            set { _IsUsingDocumentControl = value; }
        }
        [Column(Name = "Level", DataType = "Int32")]
        public Int32 Level
        {
            get { return _Level; }
            set { _Level = value; }
        }
        [Column(Name = "Path", DataType = "String")]
        public String Path
        {
            get { return _Path; }
            set { _Path = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vClassMeeting
    [Serializable]
    [Table(Name = "vClassMeeting")]
    public class vClassMeeting
    {
        private Int32 _ClassMeetingID;
        private Int32 _ClassSubjectID;
        private Int32 _PeriodSectionID;
        private DateTime _MeetingDate;
        private String _StartTime;
        private String _EndTime;
        private Int32 _RoomID;
        private String _RoomName;
        private Int32 _TeacherID;
        private String _TeacherName;
        private String _Remarks;
        private String _NextMeetingRemarks;
        private Int32 _SubjectMeetingPlanHdID;
        private Int16 _MeetingNo;
        private Boolean _IsDeleted;

        [Column(Name = "ClassMeetingID", DataType = "Int32")]
        public Int32 ClassMeetingID
        {
            get { return _ClassMeetingID; }
            set { _ClassMeetingID = value; }
        }
        [Column(Name = "ClassSubjectID", DataType = "Int32")]
        public Int32 ClassSubjectID
        {
            get { return _ClassSubjectID; }
            set { _ClassSubjectID = value; }
        }
        [Column(Name = "PeriodSectionID", DataType = "Int32")]
        public Int32 PeriodSectionID
        {
            get { return _PeriodSectionID; }
            set { _PeriodSectionID = value; }
        }
        [Column(Name = "MeetingDate", DataType = "DateTime")]
        public DateTime MeetingDate
        {
            get { return _MeetingDate; }
            set { _MeetingDate = value; }
        }
        [Column(Name = "StartTime", DataType = "String")]
        public String StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        [Column(Name = "EndTime", DataType = "String")]
        public String EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        [Column(Name = "RoomID", DataType = "Int32")]
        public Int32 RoomID
        {
            get { return _RoomID; }
            set { _RoomID = value; }
        }
        [Column(Name = "RoomName", DataType = "String")]
        public String RoomName
        {
            get { return _RoomName; }
            set { _RoomName = value; }
        }
        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
        }
        [Column(Name = "TeacherName", DataType = "String")]
        public String TeacherName
        {
            get { return _TeacherName; }
            set { _TeacherName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "NextMeetingRemarks", DataType = "String")]
        public String NextMeetingRemarks
        {
            get { return _NextMeetingRemarks; }
            set { _NextMeetingRemarks = value; }
        }
        [Column(Name = "SubjectMeetingPlanHdID", DataType = "Int32")]
        public Int32 SubjectMeetingPlanHdID
        {
            get { return _SubjectMeetingPlanHdID; }
            set { _SubjectMeetingPlanHdID = value; }
        }
        [Column(Name = "MeetingNo", DataType = "Int16")]
        public Int16 MeetingNo
        {
            get { return _MeetingNo; }
            set { _MeetingNo = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vClassMeetingAttendance
    [Serializable]
    [Table(Name = "vClassMeetingAttendance")]
    public class vClassMeetingAttendance
    {
        private Int32 _ClassMeetingID;
        private Int32 _ClassSubjectID;
        private Int32 _StudentID;
        private String _GCAttendanceStatus;
        private String _AttendanceStatus;

        [Column(Name = "ClassMeetingID", DataType = "Int32")]
        public Int32 ClassMeetingID
        {
            get { return _ClassMeetingID; }
            set { _ClassMeetingID = value; }
        }
        [Column(Name = "ClassSubjectID", DataType = "Int32")]
        public Int32 ClassSubjectID
        {
            get { return _ClassSubjectID; }
            set { _ClassSubjectID = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "GCAttendanceStatus", DataType = "String")]
        public String GCAttendanceStatus
        {
            get { return _GCAttendanceStatus; }
            set { _GCAttendanceStatus = value; }
        }
        [Column(Name = "AttendanceStatus", DataType = "String")]
        public String AttendanceStatus
        {
            get { return _AttendanceStatus; }
            set { _AttendanceStatus = value; }
        }
    }
    #endregion
    #region vClassMeetingIndicator
    [Serializable]
    [Table(Name = "vClassMeetingIndicator")]
    public class vClassMeetingIndicator
    {
        private Int32 _ClassMeetingID;
        private Int32 _SubjectIndicatorID;
        private String _SubjectIndicatorName;

        [Column(Name = "ClassMeetingID", DataType = "Int32")]
        public Int32 ClassMeetingID
        {
            get { return _ClassMeetingID; }
            set { _ClassMeetingID = value; }
        }
        [Column(Name = "SubjectIndicatorID", DataType = "Int32")]
        public Int32 SubjectIndicatorID
        {
            get { return _SubjectIndicatorID; }
            set { _SubjectIndicatorID = value; }
        }
        [Column(Name = "SubjectIndicatorName", DataType = "String")]
        public String SubjectIndicatorName
        {
            get { return _SubjectIndicatorName; }
            set { _SubjectIndicatorName = value; }
        }
    }
    #endregion
    #region vClassSchedule
    [Serializable]
    [Table(Name = "vClassSchedule")]
    public class vClassSchedule
    {
        private Int32 _ClassScheduleID;
        private Int32 _SchoolPeriodID;
        private Int32 _ClassSubjectID;
        private Int32 _SchoolClassID;
        private String _SchoolClassName;
        private Int32 _SubjectID;
        private String _SubjectName;
        private String _GCClassStudyType;
        private Int16 _DayNumber;
        private Int16 _HoursIndex;
        private String _StartTime;
        private String _EndTime;
        private Int32 _RoomID;
        private String _RoomName;
        private Int32 _TeacherID;
        private String _TeacherName;
        private Boolean _IsDeleted;

        [Column(Name = "ClassScheduleID", DataType = "Int32")]
        public Int32 ClassScheduleID
        {
            get { return _ClassScheduleID; }
            set { _ClassScheduleID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "ClassSubjectID", DataType = "Int32")]
        public Int32 ClassSubjectID
        {
            get { return _ClassSubjectID; }
            set { _ClassSubjectID = value; }
        }
        [Column(Name = "SchoolClassID", DataType = "Int32")]
        public Int32 SchoolClassID
        {
            get { return _SchoolClassID; }
            set { _SchoolClassID = value; }
        }
        [Column(Name = "SchoolClassName", DataType = "String")]
        public String SchoolClassName
        {
            get { return _SchoolClassName; }
            set { _SchoolClassName = value; }
        }
        [Column(Name = "SubjectID", DataType = "Int32")]
        public Int32 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
        [Column(Name = "SubjectName", DataType = "String")]
        public String SubjectName
        {
            get { return _SubjectName; }
            set { _SubjectName = value; }
        }
        [Column(Name = "GCClassStudyType", DataType = "String")]
        public String GCClassStudyType
        {
            get { return _GCClassStudyType; }
            set { _GCClassStudyType = value; }
        }
        [Column(Name = "DayNumber", DataType = "Int16")]
        public Int16 DayNumber
        {
            get { return _DayNumber; }
            set { _DayNumber = value; }
        }
        [Column(Name = "HoursIndex", DataType = "Int16")]
        public Int16 HoursIndex
        {
            get { return _HoursIndex; }
            set { _HoursIndex = value; }
        }
        [Column(Name = "StartTime", DataType = "String")]
        public String StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        [Column(Name = "EndTime", DataType = "String")]
        public String EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        [Column(Name = "RoomID", DataType = "Int32")]
        public Int32 RoomID
        {
            get { return _RoomID; }
            set { _RoomID = value; }
        }
        [Column(Name = "RoomName", DataType = "String")]
        public String RoomName
        {
            get { return _RoomName; }
            set { _RoomName = value; }
        }
        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
        }
        [Column(Name = "TeacherName", DataType = "String")]
        public String TeacherName
        {
            get { return _TeacherName; }
            set { _TeacherName = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vClassStudent
    [Serializable]
    [Table(Name = "vClassStudent")]
    public partial class vClassStudent
    {
        private Int32 _SchoolClassID;
        private Int32 _StudentSchoolClassID;
        private String _SchoolClassName;
        private String _GCClassStudyType;
        private String _ClassStudyType;
        private String _GCGrade;
        private String _Grade;
        private String _GCMajor;
        private String _Major;
        private String _SchoolPeriodName;
        private String _PeriodSectionName;
        private Int32 _StudentID;
        private String _StudentCode;
        private String _StudentName;
        private String _PreferredName;
        private String _GCGender;
        private String _PictureFileName;
        private String _GCClassStudentStatus;
        private String _ClassStudentStatus;

        [Column(Name = "SchoolClassID", DataType = "Int32")]
        public Int32 SchoolClassID
        {
            get { return _SchoolClassID; }
            set { _SchoolClassID = value; }
        }
        [Column(Name = "StudentSchoolClassID", DataType = "Int32")]
        public Int32 StudentSchoolClassID
        {
            get { return _StudentSchoolClassID; }
            set { _StudentSchoolClassID = value; }
        }
        [Column(Name = "SchoolClassName", DataType = "String")]
        public String SchoolClassName
        {
            get { return _SchoolClassName; }
            set { _SchoolClassName = value; }
        }
        [Column(Name = "GCClassStudyType", DataType = "String")]
        public String GCClassStudyType
        {
            get { return _GCClassStudyType; }
            set { _GCClassStudyType = value; }
        }
        [Column(Name = "ClassStudyType", DataType = "String")]
        public String ClassStudyType
        {
            get { return _ClassStudyType; }
            set { _ClassStudyType = value; }
        }
        [Column(Name = "GCGrade", DataType = "String")]
        public String GCGrade
        {
            get { return _GCGrade; }
            set { _GCGrade = value; }
        }
        [Column(Name = "Grade", DataType = "String")]
        public String Grade
        {
            get { return _Grade; }
            set { _Grade = value; }
        }
        [Column(Name = "GCMajor", DataType = "String")]
        public String GCMajor
        {
            get { return _GCMajor; }
            set { _GCMajor = value; }
        }
        [Column(Name = "Major", DataType = "String")]
        public String Major
        {
            get { return _Major; }
            set { _Major = value; }
        }
        [Column(Name = "SchoolPeriodName", DataType = "String")]
        public String SchoolPeriodName
        {
            get { return _SchoolPeriodName; }
            set { _SchoolPeriodName = value; }
        }
        [Column(Name = "PeriodSectionName", DataType = "String")]
        public String PeriodSectionName
        {
            get { return _PeriodSectionName; }
            set { _PeriodSectionName = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "StudentCode", DataType = "String")]
        public String StudentCode
        {
            get { return _StudentCode; }
            set { _StudentCode = value; }
        }
        [Column(Name = "StudentName", DataType = "String")]
        public String StudentName
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }
        [Column(Name = "PreferredName", DataType = "String")]
        public String PreferredName
        {
            get { return _PreferredName; }
            set { _PreferredName = value; }
        }
        [Column(Name = "GCGender", DataType = "String")]
        public String GCGender
        {
            get { return _GCGender; }
            set { _GCGender = value; }
        }
        [Column(Name = "PictureFileName", DataType = "String")]
        public String PictureFileName
        {
            get { return _PictureFileName; }
            set { _PictureFileName = value; }
        }
        [Column(Name = "GCClassStudentStatus", DataType = "String")]
        public String GCClassStudentStatus
        {
            get { return _GCClassStudentStatus; }
            set { _GCClassStudentStatus = value; }
        }
        [Column(Name = "ClassStudentStatus", DataType = "String")]
        public String ClassStudentStatus
        {
            get { return _ClassStudentStatus; }
            set { _ClassStudentStatus = value; }
        }
    }
    #endregion
    #region vClassSubject
    [Serializable]
    [Table(Name = "vClassSubject")]
    public partial class vClassSubject
    {
        private Int32 _ClassSubjectID;
        private Int32 _SchoolPeriodID;
        private Int32 _PeriodSectionID;
        private Int32 _SchoolClassID;
        private String _SchoolClassName;
        private Int32 _PeriodClassTypeSubjectID;
        private Int32 _SubjectID;
        private String _SubjectCode;
        private String _SubjectName;
        private String _GCSubjectMarkType;
        private String _SubjectGCClassStudyType;
        private String _GCLessonType;
        private Int16 _NoMeetingHoursInWeek;
        private Int32 _ParentID;
        private Int32 _TeacherID;
        private String _TeacherCode;
        private String _TeacherName;
        private Int32 _RoomID;
        private String _RoomName;
        private Int16 _PassingGrade;
        private Int32 _SubjectMatterID;
        private String _GCClassStudyType;
        private String _GCSubjectType;
        private String _SubjectType;
        private Int32 _StudentProgressRuleID;
        private Boolean _IsDeleted;

        [Column(Name = "ClassSubjectID", DataType = "Int32")]
        public Int32 ClassSubjectID
        {
            get { return _ClassSubjectID; }
            set { _ClassSubjectID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "PeriodSectionID", DataType = "Int32")]
        public Int32 PeriodSectionID
        {
            get { return _PeriodSectionID; }
            set { _PeriodSectionID = value; }
        }
        [Column(Name = "SchoolClassID", DataType = "Int32")]
        public Int32 SchoolClassID
        {
            get { return _SchoolClassID; }
            set { _SchoolClassID = value; }
        }
        [Column(Name = "SchoolClassName", DataType = "String")]
        public String SchoolClassName
        {
            get { return _SchoolClassName; }
            set { _SchoolClassName = value; }
        }
        [Column(Name = "PeriodClassTypeSubjectID", DataType = "Int32")]
        public Int32 PeriodClassTypeSubjectID
        {
            get { return _PeriodClassTypeSubjectID; }
            set { _PeriodClassTypeSubjectID = value; }
        }
        [Column(Name = "SubjectID", DataType = "Int32")]
        public Int32 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
        [Column(Name = "SubjectCode", DataType = "String")]
        public String SubjectCode
        {
            get { return _SubjectCode; }
            set { _SubjectCode = value; }
        }
        [Column(Name = "SubjectName", DataType = "String")]
        public String SubjectName
        {
            get { return _SubjectName; }
            set { _SubjectName = value; }
        }
        [Column(Name = "GCSubjectMarkType", DataType = "String")]
        public String GCSubjectMarkType
        {
            get { return _GCSubjectMarkType; }
            set { _GCSubjectMarkType = value; }
        }
        [Column(Name = "SubjectGCClassStudyType", DataType = "String")]
        public String SubjectGCClassStudyType
        {
            get { return _SubjectGCClassStudyType; }
            set { _SubjectGCClassStudyType = value; }
        }
        [Column(Name = "GCLessonType", DataType = "String")]
        public String GCLessonType
        {
            get { return _GCLessonType; }
            set { _GCLessonType = value; }
        }
        [Column(Name = "NoMeetingHoursInWeek", DataType = "Int16")]
        public Int16 NoMeetingHoursInWeek
        {
            get { return _NoMeetingHoursInWeek; }
            set { _NoMeetingHoursInWeek = value; }
        }
        [Column(Name = "ParentID", DataType = "Int32")]
        public Int32 ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
        }
        [Column(Name = "TeacherCode", DataType = "String")]
        public String TeacherCode
        {
            get { return _TeacherCode; }
            set { _TeacherCode = value; }
        }
        [Column(Name = "TeacherName", DataType = "String")]
        public String TeacherName
        {
            get { return _TeacherName; }
            set { _TeacherName = value; }
        }
        [Column(Name = "RoomID", DataType = "Int32")]
        public Int32 RoomID
        {
            get { return _RoomID; }
            set { _RoomID = value; }
        }
        [Column(Name = "RoomName", DataType = "String")]
        public String RoomName
        {
            get { return _RoomName; }
            set { _RoomName = value; }
        }
        [Column(Name = "PassingGrade", DataType = "Int16")]
        public Int16 PassingGrade
        {
            get { return _PassingGrade; }
            set { _PassingGrade = value; }
        }
        [Column(Name = "SubjectMatterID", DataType = "Int32")]
        public Int32 SubjectMatterID
        {
            get { return _SubjectMatterID; }
            set { _SubjectMatterID = value; }
        }
        [Column(Name = "GCClassStudyType", DataType = "String")]
        public String GCClassStudyType
        {
            get { return _GCClassStudyType; }
            set { _GCClassStudyType = value; }
        }
        [Column(Name = "GCSubjectType", DataType = "String")]
        public String GCSubjectType
        {
            get { return _GCSubjectType; }
            set { _GCSubjectType = value; }
        }
        [Column(Name = "SubjectType", DataType = "String")]
        public String SubjectType
        {
            get { return _SubjectType; }
            set { _SubjectType = value; }
        }
        [Column(Name = "StudentProgressRuleID", DataType = "Int32")]
        public Int32 StudentProgressRuleID
        {
            get { return _StudentProgressRuleID; }
            set { _StudentProgressRuleID = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vClassSubjectCustom
    [Serializable]
    [Table(Name = "vClassSubjectCustom")]
    public class vClassSubjectCustom
    {
        private Int32 _SchoolClassID;
        private String _SchoolClassName;
        private Int32 _SchoolPeriodID;
        private Int32 _PeriodClassTypeSubjectID;
        private Int32 _SubjectID;
        private String _SubjectCode;
        private String _SubjectName;
        private String _GCClassStudyType;
        private String _TeacherName;

        [Column(Name = "SchoolClassID", DataType = "Int32")]
        public Int32 SchoolClassID
        {
            get { return _SchoolClassID; }
            set { _SchoolClassID = value; }
        }
        [Column(Name = "SchoolClassName", DataType = "String")]
        public String SchoolClassName
        {
            get { return _SchoolClassName; }
            set { _SchoolClassName = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "PeriodClassTypeSubjectID", DataType = "Int32")]
        public Int32 PeriodClassTypeSubjectID
        {
            get { return _PeriodClassTypeSubjectID; }
            set { _PeriodClassTypeSubjectID = value; }
        }
        [Column(Name = "SubjectID", DataType = "Int32")]
        public Int32 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
        [Column(Name = "SubjectCode", DataType = "String")]
        public String SubjectCode
        {
            get { return _SubjectCode; }
            set { _SubjectCode = value; }
        }
        [Column(Name = "SubjectName", DataType = "String")]
        public String SubjectName
        {
            get { return _SubjectName; }
            set { _SubjectName = value; }
        }
        [Column(Name = "GCClassStudyType", DataType = "String")]
        public String GCClassStudyType
        {
            get { return _GCClassStudyType; }
            set { _GCClassStudyType = value; }
        }
        [Column(Name = "TeacherName", DataType = "String")]
        public String TeacherName
        {
            get { return _TeacherName; }
            set { _TeacherName = value; }
        }
    }
    #endregion
    #region vClassStudentSubjectTaskMark
    [Serializable]
    [Table(Name = "vClassStudentSubjectTaskMark")]
    public class vClassStudentSubjectTaskMark
    {
        private Int32 _ClassSubjectTaskID;
        private Int32 _ClassSubjectID;
        private Int32 _StudentID;
        private Boolean _IsRemedial;
        private Decimal _Mark;
        private Int32 _StudentProgressRuleDtID;
        private String _StudentProgressRuleDtName;
        private String _DescriptionMark;

        [Column(Name = "ClassSubjectTaskID", DataType = "Int32")]
        public Int32 ClassSubjectTaskID
        {
            get { return _ClassSubjectTaskID; }
            set { _ClassSubjectTaskID = value; }
        }
        [Column(Name = "ClassSubjectID", DataType = "Int32")]
        public Int32 ClassSubjectID
        {
            get { return _ClassSubjectID; }
            set { _ClassSubjectID = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "IsRemedial", DataType = "Boolean")]
        public Boolean IsRemedial
        {
            get { return _IsRemedial; }
            set { _IsRemedial = value; }
        }
        [Column(Name = "Mark", DataType = "Decimal")]
        public Decimal Mark
        {
            get { return _Mark; }
            set { _Mark = value; }
        }
        [Column(Name = "StudentProgressRuleDtID", DataType = "Int32")]
        public Int32 StudentProgressRuleDtID
        {
            get { return _StudentProgressRuleDtID; }
            set { _StudentProgressRuleDtID = value; }
        }
        [Column(Name = "StudentProgressRuleDtName", DataType = "String")]
        public String StudentProgressRuleDtName
        {
            get { return _StudentProgressRuleDtName; }
            set { _StudentProgressRuleDtName = value; }
        }
        [Column(Name = "DescriptionMark", DataType = "String")]
        public String DescriptionMark
        {
            get { return _DescriptionMark; }
            set { _DescriptionMark = value; }
        }
    }
    #endregion
    #region vClassStudentSubjectTaskRemedialMark
    [Serializable]
    [Table(Name = "vClassStudentSubjectTaskRemedialMark")]
    public class vClassStudentSubjectTaskRemedialMark
    {
        private Int32 _ClassSubjectTaskRemedialID;
        private Int32 _ClassSubjectTaskID;
        private Int16 _DisplayOrder;
        private DateTime _TaskDate;
        private String _Remarks;
        private Int32 _StudentID;
        private Decimal _Mark;
        private String _GCOptionMark;
        private String _DescriptionMark;

        [Column(Name = "ClassSubjectTaskRemedialID", DataType = "Int32")]
        public Int32 ClassSubjectTaskRemedialID
        {
            get { return _ClassSubjectTaskRemedialID; }
            set { _ClassSubjectTaskRemedialID = value; }
        }
        [Column(Name = "ClassSubjectTaskID", DataType = "Int32")]
        public Int32 ClassSubjectTaskID
        {
            get { return _ClassSubjectTaskID; }
            set { _ClassSubjectTaskID = value; }
        }
        [Column(Name = "DisplayOrder", DataType = "Int16")]
        public Int16 DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        [Column(Name = "TaskDate", DataType = "DateTime")]
        public DateTime TaskDate
        {
            get { return _TaskDate; }
            set { _TaskDate = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "Mark", DataType = "Decimal")]
        public Decimal Mark
        {
            get { return _Mark; }
            set { _Mark = value; }
        }
        [Column(Name = "GCOptionMark", DataType = "String")]
        public String GCOptionMark
        {
            get { return _GCOptionMark; }
            set { _GCOptionMark = value; }
        }
        [Column(Name = "DescriptionMark", DataType = "String")]
        public String DescriptionMark
        {
            get { return _DescriptionMark; }
            set { _DescriptionMark = value; }
        }
    }
    #endregion
    #region vClassSubjectTask
    [Serializable]
    [Table(Name = "vClassSubjectTask")]
    public class vClassSubjectTask
    {
        private Int32 _ClassSubjectTaskID;
        private Int32 _ClassSubjectID;
        private Int32 _PeriodSectionID;
        private String _ClassTaskCode;
        private String _GCTaskType;
        private String _TaskType;
        private String _GCLessonType;
        private String _LessonType;
        private Int16 _FinalMarkPercentage;
        private DateTime _TaskDate;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private String _StartTime;
        private String _EndTime;
        private String _Topic;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "ClassSubjectTaskID", DataType = "Int32")]
        public Int32 ClassSubjectTaskID
        {
            get { return _ClassSubjectTaskID; }
            set { _ClassSubjectTaskID = value; }
        }
        [Column(Name = "ClassSubjectID", DataType = "Int32")]
        public Int32 ClassSubjectID
        {
            get { return _ClassSubjectID; }
            set { _ClassSubjectID = value; }
        }
        [Column(Name = "PeriodSectionID", DataType = "Int32")]
        public Int32 PeriodSectionID
        {
            get { return _PeriodSectionID; }
            set { _PeriodSectionID = value; }
        }
        [Column(Name = "ClassTaskCode", DataType = "String")]
        public String ClassTaskCode
        {
            get { return _ClassTaskCode; }
            set { _ClassTaskCode = value; }
        }
        [Column(Name = "GCTaskType", DataType = "String")]
        public String GCTaskType
        {
            get { return _GCTaskType; }
            set { _GCTaskType = value; }
        }
        [Column(Name = "TaskType", DataType = "String")]
        public String TaskType
        {
            get { return _TaskType; }
            set { _TaskType = value; }
        }
        [Column(Name = "GCLessonType", DataType = "String")]
        public String GCLessonType
        {
            get { return _GCLessonType; }
            set { _GCLessonType = value; }
        }
        [Column(Name = "LessonType", DataType = "String")]
        public String LessonType
        {
            get { return _LessonType; }
            set { _LessonType = value; }
        }
        [Column(Name = "FinalMarkPercentage", DataType = "Int16")]
        public Int16 FinalMarkPercentage
        {
            get { return _FinalMarkPercentage; }
            set { _FinalMarkPercentage = value; }
        }
        [Column(Name = "TaskDate", DataType = "DateTime")]
        public DateTime TaskDate
        {
            get { return _TaskDate; }
            set { _TaskDate = value; }
        }
        [Column(Name = "StartDate", DataType = "DateTime")]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Column(Name = "EndDate", DataType = "DateTime")]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        [Column(Name = "StartTime", DataType = "String")]
        public String StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        [Column(Name = "EndTime", DataType = "String")]
        public String EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        [Column(Name = "Topic", DataType = "String")]
        public String Topic
        {
            get { return _Topic; }
            set { _Topic = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vClassSubjectTaskCustom
    [Serializable]
    [Table(Name = "vClassSubjectTaskCustom")]
    public class vClassSubjectTaskCustom
    {
        private Int32 _ClassSubjectTaskID;
        private Int32 _ClassSubjectID;
        private Int32 _PeriodSectionID;
        private String _ClassTaskCode;
        private String _GCTaskType;
        private Int32 _TheoryFinalMarkFormulaDtID;
        private String _TheoryFinalMarkFormulaDtName;
        private Int16 _TheoryDisplayOrder;
        private Decimal _TheoryFinalMarkPercentage;
        private Int32 _PracticeFinalMarkFormulaDtID;
        private String _PracticeFinalMarkFormulaDtName;
        private Int16 _PracticeDisplayOrder;
        private Decimal _PracticeFinalMarkPercentage;
        private String _TaskType;
        private String _GCLessonType;
        private String _LessonType;
        private Int16 _FinalMarkPercentage;
        private DateTime _TaskDate;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private String _StartTime;
        private String _EndTime;
        private String _Topic;
        private String _Remarks;
        private Int32 _TheoryFinalMarkFormulaID;
        private Int32 _PracticeFinalMarkFormulaID;
        private Boolean _IsDeleted;

        [Column(Name = "ClassSubjectTaskID", DataType = "Int32")]
        public Int32 ClassSubjectTaskID
        {
            get { return _ClassSubjectTaskID; }
            set { _ClassSubjectTaskID = value; }
        }
        [Column(Name = "ClassSubjectID", DataType = "Int32")]
        public Int32 ClassSubjectID
        {
            get { return _ClassSubjectID; }
            set { _ClassSubjectID = value; }
        }
        [Column(Name = "PeriodSectionID", DataType = "Int32")]
        public Int32 PeriodSectionID
        {
            get { return _PeriodSectionID; }
            set { _PeriodSectionID = value; }
        }
        [Column(Name = "ClassTaskCode", DataType = "String")]
        public String ClassTaskCode
        {
            get { return _ClassTaskCode; }
            set { _ClassTaskCode = value; }
        }
        [Column(Name = "GCTaskType", DataType = "String")]
        public String GCTaskType
        {
            get { return _GCTaskType; }
            set { _GCTaskType = value; }
        }
        [Column(Name = "TheoryFinalMarkFormulaDtID", DataType = "Int32")]
        public Int32 TheoryFinalMarkFormulaDtID
        {
            get { return _TheoryFinalMarkFormulaDtID; }
            set { _TheoryFinalMarkFormulaDtID = value; }
        }
        [Column(Name = "TheoryFinalMarkFormulaDtName", DataType = "String")]
        public String TheoryFinalMarkFormulaDtName
        {
            get { return _TheoryFinalMarkFormulaDtName; }
            set { _TheoryFinalMarkFormulaDtName = value; }
        }
        [Column(Name = "TheoryDisplayOrder", DataType = "Int16")]
        public Int16 TheoryDisplayOrder
        {
            get { return _TheoryDisplayOrder; }
            set { _TheoryDisplayOrder = value; }
        }
        [Column(Name = "TheoryFinalMarkPercentage", DataType = "Decimal")]
        public Decimal TheoryFinalMarkPercentage
        {
            get { return _TheoryFinalMarkPercentage; }
            set { _TheoryFinalMarkPercentage = value; }
        }
        [Column(Name = "PracticeFinalMarkFormulaDtID", DataType = "Int32")]
        public Int32 PracticeFinalMarkFormulaDtID
        {
            get { return _PracticeFinalMarkFormulaDtID; }
            set { _PracticeFinalMarkFormulaDtID = value; }
        }
        [Column(Name = "PracticeFinalMarkFormulaDtName", DataType = "String")]
        public String PracticeFinalMarkFormulaDtName
        {
            get { return _PracticeFinalMarkFormulaDtName; }
            set { _PracticeFinalMarkFormulaDtName = value; }
        }
        [Column(Name = "PracticeDisplayOrder", DataType = "Int16")]
        public Int16 PracticeDisplayOrder
        {
            get { return _PracticeDisplayOrder; }
            set { _PracticeDisplayOrder = value; }
        }
        [Column(Name = "PracticeFinalMarkPercentage", DataType = "Decimal")]
        public Decimal PracticeFinalMarkPercentage
        {
            get { return _PracticeFinalMarkPercentage; }
            set { _PracticeFinalMarkPercentage = value; }
        }
        [Column(Name = "TaskType", DataType = "String")]
        public String TaskType
        {
            get { return _TaskType; }
            set { _TaskType = value; }
        }
        [Column(Name = "GCLessonType", DataType = "String")]
        public String GCLessonType
        {
            get { return _GCLessonType; }
            set { _GCLessonType = value; }
        }
        [Column(Name = "LessonType", DataType = "String")]
        public String LessonType
        {
            get { return _LessonType; }
            set { _LessonType = value; }
        }
        [Column(Name = "FinalMarkPercentage", DataType = "Int16")]
        public Int16 FinalMarkPercentage
        {
            get { return _FinalMarkPercentage; }
            set { _FinalMarkPercentage = value; }
        }
        [Column(Name = "TaskDate", DataType = "DateTime")]
        public DateTime TaskDate
        {
            get { return _TaskDate; }
            set { _TaskDate = value; }
        }
        [Column(Name = "StartDate", DataType = "DateTime")]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Column(Name = "EndDate", DataType = "DateTime")]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        [Column(Name = "StartTime", DataType = "String")]
        public String StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        [Column(Name = "EndTime", DataType = "String")]
        public String EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        [Column(Name = "Topic", DataType = "String")]
        public String Topic
        {
            get { return _Topic; }
            set { _Topic = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "TheoryFinalMarkFormulaID", DataType = "Int32")]
        public Int32 TheoryFinalMarkFormulaID
        {
            get { return _TheoryFinalMarkFormulaID; }
            set { _TheoryFinalMarkFormulaID = value; }
        }
        [Column(Name = "PracticeFinalMarkFormulaID", DataType = "Int32")]
        public Int32 PracticeFinalMarkFormulaID
        {
            get { return _PracticeFinalMarkFormulaID; }
            set { _PracticeFinalMarkFormulaID = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vClassSubjectTaskIndicator
    [Serializable]
    [Table(Name = "vClassSubjectTaskIndicator")]
    public class vClassSubjectTaskIndicator
    {
        private Int32 _ClassSubjectTaskID;
        private Int32 _SubjectIndicatorID;
        private String _SubjectIndicatorName;

        [Column(Name = "ClassSubjectTaskID", DataType = "Int32")]
        public Int32 ClassSubjectTaskID
        {
            get { return _ClassSubjectTaskID; }
            set { _ClassSubjectTaskID = value; }
        }
        [Column(Name = "SubjectIndicatorID", DataType = "Int32")]
        public Int32 SubjectIndicatorID
        {
            get { return _SubjectIndicatorID; }
            set { _SubjectIndicatorID = value; }
        }
        [Column(Name = "SubjectIndicatorName", DataType = "String")]
        public String SubjectIndicatorName
        {
            get { return _SubjectIndicatorName; }
            set { _SubjectIndicatorName = value; }
        }
    }
    #endregion
    #region vClassType
    [Serializable]
    [Table(Name = "vClassType")]
    public class vClassType
    {
        private Int32 _ClassTypeID;
        private String _ClassTypeCode;
        private String _ClassTypeName;
        private String _SiteID;
        private String _GCClassStudyType;
        private String _GCGrade;
        private String _Grade;
        private String _GCMajor;
        private String _Major;
        private Boolean _IsDeleted;

        [Column(Name = "ClassTypeID", DataType = "Int32")]
        public Int32 ClassTypeID
        {
            get { return _ClassTypeID; }
            set { _ClassTypeID = value; }
        }
        [Column(Name = "ClassTypeCode", DataType = "String")]
        public String ClassTypeCode
        {
            get { return _ClassTypeCode; }
            set { _ClassTypeCode = value; }
        }
        [Column(Name = "ClassTypeName", DataType = "String")]
        public String ClassTypeName
        {
            get { return _ClassTypeName; }
            set { _ClassTypeName = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "GCClassStudyType", DataType = "String")]
        public String GCClassStudyType
        {
            get { return _GCClassStudyType; }
            set { _GCClassStudyType = value; }
        }
        [Column(Name = "GCGrade", DataType = "String")]
        public String GCGrade
        {
            get { return _GCGrade; }
            set { _GCGrade = value; }
        }
        [Column(Name = "Grade", DataType = "String")]
        public String Grade
        {
            get { return _Grade; }
            set { _Grade = value; }
        }
        [Column(Name = "GCMajor", DataType = "String")]
        public String GCMajor
        {
            get { return _GCMajor; }
            set { _GCMajor = value; }
        }
        [Column(Name = "Major", DataType = "String")]
        public String Major
        {
            get { return _Major; }
            set { _Major = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vClassTypeExtracurricular
    [Serializable]
    [Table(Name = "vClassTypeExtracurricular")]
    public class vClassTypeExtracurricular
    {
        private Int32 _ClassTypeID;
        private String _ClassTypeCode;
        private String _ClassTypeName;
        private String _GCGrade;
        private String _GCMajor;
        private Int32 _ExtracurricularClassTypeID;

        [Column(Name = "ClassTypeID", DataType = "Int32")]
        public Int32 ClassTypeID
        {
            get { return _ClassTypeID; }
            set { _ClassTypeID = value; }
        }
        [Column(Name = "ClassTypeCode", DataType = "String")]
        public String ClassTypeCode
        {
            get { return _ClassTypeCode; }
            set { _ClassTypeCode = value; }
        }
        [Column(Name = "ClassTypeName", DataType = "String")]
        public String ClassTypeName
        {
            get { return _ClassTypeName; }
            set { _ClassTypeName = value; }
        }
        [Column(Name = "GCGrade", DataType = "String")]
        public String GCGrade
        {
            get { return _GCGrade; }
            set { _GCGrade = value; }
        }
        [Column(Name = "GCMajor", DataType = "String")]
        public String GCMajor
        {
            get { return _GCMajor; }
            set { _GCMajor = value; }
        }
        [Column(Name = "ExtracurricularClassTypeID", DataType = "Int32")]
        public Int32 ExtracurricularClassTypeID
        {
            get { return _ExtracurricularClassTypeID; }
            set { _ExtracurricularClassTypeID = value; }
        }
    }
    #endregion
    #region vCOAGroup
    [Serializable]
    [Table(Name = "vCOAGroup")]
    public class vCOAGroup
    {
        private Int32 _COAGroupID;
        private String _GCCOAType;
        private String _COAType;
        private String _COAGroupCode;
        private String _COAGroupName;
        private Boolean _IsHeader;
        private Int32 _ParentID;
        private String _ParentCode;
        private String _ParentName;
        private Int16 _PrintOrder;
        private Boolean _IsDeleted;
        private Int32 _Level;
        private String _DisplayPath;

        [Column(Name = "COAGroupID", DataType = "Int32")]
        public Int32 COAGroupID
        {
            get { return _COAGroupID; }
            set { _COAGroupID = value; }
        }
        [Column(Name = "GCCOAType", DataType = "String")]
        public String GCCOAType
        {
            get { return _GCCOAType; }
            set { _GCCOAType = value; }
        }
        [Column(Name = "COAType", DataType = "String")]
        public String COAType
        {
            get { return _COAType; }
            set { _COAType = value; }
        }
        [Column(Name = "COAGroupCode", DataType = "String")]
        public String COAGroupCode
        {
            get { return _COAGroupCode; }
            set { _COAGroupCode = value; }
        }
        [Column(Name = "COAGroupName", DataType = "String")]
        public String COAGroupName
        {
            get { return _COAGroupName; }
            set { _COAGroupName = value; }
        }
        [Column(Name = "IsHeader", DataType = "Boolean")]
        public Boolean IsHeader
        {
            get { return _IsHeader; }
            set { _IsHeader = value; }
        }
        [Column(Name = "ParentID", DataType = "Int32")]
        public Int32 ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        [Column(Name = "ParentCode", DataType = "String")]
        public String ParentCode
        {
            get { return _ParentCode; }
            set { _ParentCode = value; }
        }
        [Column(Name = "ParentName", DataType = "String")]
        public String ParentName
        {
            get { return _ParentName; }
            set { _ParentName = value; }
        }
        [Column(Name = "PrintOrder", DataType = "Int16")]
        public Int16 PrintOrder
        {
            get { return _PrintOrder; }
            set { _PrintOrder = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "Level", DataType = "Int32")]
        public Int32 Level
        {
            get { return _Level; }
            set { _Level = value; }
        }
        [Column(Name = "DisplayPath", DataType = "String")]
        public String DisplayPath
        {
            get { return _DisplayPath; }
            set { _DisplayPath = value; }
        }
    }
    #endregion
    #region vCoverageTypeDt
    [Serializable]
    [Table(Name = "vCoverageTypeDt")]
    public class vCoverageTypeDt
    {
        private Int32 _CoverageTypeDtID;
        private Int32 _CoverageTypeID;
        private String _ListClassTypeID;
        private String _ListClassTypeName;
        private String _CoverageTypeDtName;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "CoverageTypeDtID", DataType = "Int32")]
        public Int32 CoverageTypeDtID
        {
            get { return _CoverageTypeDtID; }
            set { _CoverageTypeDtID = value; }
        }
        [Column(Name = "CoverageTypeID", DataType = "Int32")]
        public Int32 CoverageTypeID
        {
            get { return _CoverageTypeID; }
            set { _CoverageTypeID = value; }
        }
        [Column(Name = "ListClassTypeID", DataType = "String")]
        public String ListClassTypeID
        {
            get { return _ListClassTypeID; }
            set { _ListClassTypeID = value; }
        }
        [Column(Name = "ListClassTypeName", DataType = "String")]
        public String ListClassTypeName
        {
            get { return _ListClassTypeName; }
            set { _ListClassTypeName = value; }
        }
        [Column(Name = "CoverageTypeDtName", DataType = "String")]
        public String CoverageTypeDtName
        {
            get { return _CoverageTypeDtName; }
            set { _CoverageTypeDtName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vCreditCard
    [Serializable]
    [Table(Name = "vCreditCard")]
    public class vCreditCard
    {
        private Int32 _CreditCardID;
        private String _SiteID;
        private String _SiteName;
        private String _GCCardType;
        private String _CardType;
        private String _GCCardProvider;
        private String _CardProvider;
        private Int32 _EDCMachineID;
        private String _EDCMachineCode;
        private String _EDCMachineName;
        private Decimal _CreditCardFee;
        private Boolean _IsDeleted;

        [Column(Name = "CreditCardID", DataType = "Int32")]
        public Int32 CreditCardID
        {
            get { return _CreditCardID; }
            set { _CreditCardID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "SiteName", DataType = "String")]
        public String SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }
        [Column(Name = "GCCardType", DataType = "String")]
        public String GCCardType
        {
            get { return _GCCardType; }
            set { _GCCardType = value; }
        }
        [Column(Name = "CardType", DataType = "String")]
        public String CardType
        {
            get { return _CardType; }
            set { _CardType = value; }
        }
        [Column(Name = "GCCardProvider", DataType = "String")]
        public String GCCardProvider
        {
            get { return _GCCardProvider; }
            set { _GCCardProvider = value; }
        }
        [Column(Name = "CardProvider", DataType = "String")]
        public String CardProvider
        {
            get { return _CardProvider; }
            set { _CardProvider = value; }
        }
        [Column(Name = "EDCMachineID", DataType = "Int32")]
        public Int32 EDCMachineID
        {
            get { return _EDCMachineID; }
            set { _EDCMachineID = value; }
        }
        [Column(Name = "EDCMachineCode", DataType = "String")]
        public String EDCMachineCode
        {
            get { return _EDCMachineCode; }
            set { _EDCMachineCode = value; }
        }
        [Column(Name = "EDCMachineName", DataType = "String")]
        public String EDCMachineName
        {
            get { return _EDCMachineName; }
            set { _EDCMachineName = value; }
        }
        [Column(Name = "CreditCardFee", DataType = "Decimal")]
        public Decimal CreditCardFee
        {
            get { return _CreditCardFee; }
            set { _CreditCardFee = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vCurriculumClassType
    [Serializable]
    [Table(Name = "vCurriculumClassType")]
    public class vCurriculumClassType
    {
        private Int32 _CurriculumClassTypeID;
        private String _CurriculumClassTypeCode;
        private String _CurriculumClassTypeName;
        private Int32 _CurriculumID;
        private String _GCClassStudyType;
        private String _GCGrade;
        private String _Grade;
        private Int32 _CurriculumMajorID;
        private String _CurriculumMajorName;
        private String _GCMajor;
        private String _Major;
        private Boolean _IsDeleted;

        [Column(Name = "CurriculumClassTypeID", DataType = "Int32")]
        public Int32 CurriculumClassTypeID
        {
            get { return _CurriculumClassTypeID; }
            set { _CurriculumClassTypeID = value; }
        }
        [Column(Name = "CurriculumClassTypeCode", DataType = "String")]
        public String CurriculumClassTypeCode
        {
            get { return _CurriculumClassTypeCode; }
            set { _CurriculumClassTypeCode = value; }
        }
        [Column(Name = "CurriculumClassTypeName", DataType = "String")]
        public String CurriculumClassTypeName
        {
            get { return _CurriculumClassTypeName; }
            set { _CurriculumClassTypeName = value; }
        }
        [Column(Name = "CurriculumID", DataType = "Int32")]
        public Int32 CurriculumID
        {
            get { return _CurriculumID; }
            set { _CurriculumID = value; }
        }
        [Column(Name = "GCClassStudyType", DataType = "String")]
        public String GCClassStudyType
        {
            get { return _GCClassStudyType; }
            set { _GCClassStudyType = value; }
        }
        [Column(Name = "GCGrade", DataType = "String")]
        public String GCGrade
        {
            get { return _GCGrade; }
            set { _GCGrade = value; }
        }
        [Column(Name = "Grade", DataType = "String")]
        public String Grade
        {
            get { return _Grade; }
            set { _Grade = value; }
        }
        [Column(Name = "CurriculumMajorID", DataType = "Int32")]
        public Int32 CurriculumMajorID
        {
            get { return _CurriculumMajorID; }
            set { _CurriculumMajorID = value; }
        }
        [Column(Name = "CurriculumMajorName", DataType = "String")]
        public String CurriculumMajorName
        {
            get { return _CurriculumMajorName; }
            set { _CurriculumMajorName = value; }
        }
        [Column(Name = "GCMajor", DataType = "String")]
        public String GCMajor
        {
            get { return _GCMajor; }
            set { _GCMajor = value; }
        }
        [Column(Name = "Major", DataType = "String")]
        public String Major
        {
            get { return _Major; }
            set { _Major = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vCurriculumMajor
    [Serializable]
    [Table(Name = "vCurriculumMajor")]
    public class vCurriculumMajor
    {
        private Int32 _CurriculumMajorID;
        private Int32 _CurriculumID;
        private String _GCMajor;
        private String _Major;
        private String _CurriculumMajorName;
        private Boolean _IsDeleted;

        [Column(Name = "CurriculumMajorID", DataType = "Int32")]
        public Int32 CurriculumMajorID
        {
            get { return _CurriculumMajorID; }
            set { _CurriculumMajorID = value; }
        }
        [Column(Name = "CurriculumID", DataType = "Int32")]
        public Int32 CurriculumID
        {
            get { return _CurriculumID; }
            set { _CurriculumID = value; }
        }
        [Column(Name = "GCMajor", DataType = "String")]
        public String GCMajor
        {
            get { return _GCMajor; }
            set { _GCMajor = value; }
        }
        [Column(Name = "Major", DataType = "String")]
        public String Major
        {
            get { return _Major; }
            set { _Major = value; }
        }
        [Column(Name = "CurriculumMajorName", DataType = "String")]
        public String CurriculumMajorName
        {
            get { return _CurriculumMajorName; }
            set { _CurriculumMajorName = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vCurriculumMarkType
    [Serializable]
    [Table(Name = "vCurriculumMarkType")]
    public class vCurriculumMarkType
    {
        private Int32 _CurriculumMarkTypeID;
        private Int32 _CurriculumID;
        private String _CurriculumMarkTypeName;
        private String _GCTaskMarkType;
        private String _TaskMarkType;
        private String _GCFinalMarkType;
        private String _FinalMarkType;
        private Boolean _IsDeleted;

        [Column(Name = "CurriculumMarkTypeID", DataType = "Int32")]
        public Int32 CurriculumMarkTypeID
        {
            get { return _CurriculumMarkTypeID; }
            set { _CurriculumMarkTypeID = value; }
        }
        [Column(Name = "CurriculumID", DataType = "Int32")]
        public Int32 CurriculumID
        {
            get { return _CurriculumID; }
            set { _CurriculumID = value; }
        }
        [Column(Name = "CurriculumMarkTypeName", DataType = "String")]
        public String CurriculumMarkTypeName
        {
            get { return _CurriculumMarkTypeName; }
            set { _CurriculumMarkTypeName = value; }
        }
        [Column(Name = "GCTaskMarkType", DataType = "String")]
        public String GCTaskMarkType
        {
            get { return _GCTaskMarkType; }
            set { _GCTaskMarkType = value; }
        }
        [Column(Name = "TaskMarkType", DataType = "String")]
        public String TaskMarkType
        {
            get { return _TaskMarkType; }
            set { _TaskMarkType = value; }
        }
        [Column(Name = "GCFinalMarkType", DataType = "String")]
        public String GCFinalMarkType
        {
            get { return _GCFinalMarkType; }
            set { _GCFinalMarkType = value; }
        }
        [Column(Name = "FinalMarkType", DataType = "String")]
        public String FinalMarkType
        {
            get { return _FinalMarkType; }
            set { _FinalMarkType = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vCustomer
    [Serializable]
    [Table(Name = "vCustomer")]
    public class vCustomer
    {
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private String _GCCustomerType;
        private String _CustomerType;
        private Int32 _CustomerBillTo;
        private String _CustomerBillToCode;
        private String _CustomerBillToName;
        private Decimal _CreditLimit;
        private Decimal _CreditBalance;
        private Boolean _IsDummy;
        private Boolean _IsCreditHold;
        private Boolean _IsHasContract;
        private Boolean _IsUsingDunningLetter;
        private Boolean _IsDeleted;

        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "GCCustomerType", DataType = "String")]
        public String GCCustomerType
        {
            get { return _GCCustomerType; }
            set { _GCCustomerType = value; }
        }
        [Column(Name = "CustomerType", DataType = "String")]
        public String CustomerType
        {
            get { return _CustomerType; }
            set { _CustomerType = value; }
        }
        [Column(Name = "CustomerBillTo", DataType = "Int32")]
        public Int32 CustomerBillTo
        {
            get { return _CustomerBillTo; }
            set { _CustomerBillTo = value; }
        }
        [Column(Name = "CustomerBillToCode", DataType = "String")]
        public String CustomerBillToCode
        {
            get { return _CustomerBillToCode; }
            set { _CustomerBillToCode = value; }
        }
        [Column(Name = "CustomerBillToName", DataType = "String")]
        public String CustomerBillToName
        {
            get { return _CustomerBillToName; }
            set { _CustomerBillToName = value; }
        }
        [Column(Name = "CreditLimit", DataType = "Decimal")]
        public Decimal CreditLimit
        {
            get { return _CreditLimit; }
            set { _CreditLimit = value; }
        }
        [Column(Name = "CreditBalance", DataType = "Decimal")]
        public Decimal CreditBalance
        {
            get { return _CreditBalance; }
            set { _CreditBalance = value; }
        }
        [Column(Name = "IsDummy", DataType = "Boolean")]
        public Boolean IsDummy
        {
            get { return _IsDummy; }
            set { _IsDummy = value; }
        }
        [Column(Name = "IsCreditHold", DataType = "Boolean")]
        public Boolean IsCreditHold
        {
            get { return _IsCreditHold; }
            set { _IsCreditHold = value; }
        }
        [Column(Name = "IsHasContract", DataType = "Boolean")]
        public Boolean IsHasContract
        {
            get { return _IsHasContract; }
            set { _IsHasContract = value; }
        }
        [Column(Name = "IsUsingDunningLetter", DataType = "Boolean")]
        public Boolean IsUsingDunningLetter
        {
            get { return _IsUsingDunningLetter; }
            set { _IsUsingDunningLetter = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vCustomerContractMemberCustom
    [Serializable]
    [Table(Name = "vCustomerContractMemberCustom")]
    public class vCustomerContractMemberCustom
    {
        private Int32 _ContractID;
        private Int32 _CoverageTypeID;
        private String _CoverageTypeName;
        private String _ListStudentID;
        private String _ListStudentName;

        [Column(Name = "ContractID", DataType = "Int32")]
        public Int32 ContractID
        {
            get { return _ContractID; }
            set { _ContractID = value; }
        }
        [Column(Name = "CoverageTypeID", DataType = "Int32")]
        public Int32 CoverageTypeID
        {
            get { return _CoverageTypeID; }
            set { _CoverageTypeID = value; }
        }
        [Column(Name = "CoverageTypeName", DataType = "String")]
        public String CoverageTypeName
        {
            get { return _CoverageTypeName; }
            set { _CoverageTypeName = value; }
        }
        [Column(Name = "ListStudentID", DataType = "String")]
        public String ListStudentID
        {
            get { return _ListStudentID; }
            set { _ListStudentID = value; }
        }
        [Column(Name = "ListStudentName", DataType = "String")]
        public String ListStudentName
        {
            get { return _ListStudentName; }
            set { _ListStudentName = value; }
        }
    }
    #endregion
    #region vDailyScheduleTypeDt
    [Serializable]
    [Table(Name = "vDailyScheduleTypeDt")]
    public class vDailyScheduleTypeDt
    {
        private Int32 _DailyScheduleTypeDtID;
        private Int32 _DailyScheduleTypeID;
        private Int16 _HoursIndex;
        private String _StartTime;
        private String _EndTime;
        private String _GCDailyScheduleType;
        private String _DailyScheduleType;

        [Column(Name = "DailyScheduleTypeDtID", DataType = "Int32")]
        public Int32 DailyScheduleTypeDtID
        {
            get { return _DailyScheduleTypeDtID; }
            set { _DailyScheduleTypeDtID = value; }
        }
        [Column(Name = "DailyScheduleTypeID", DataType = "Int32")]
        public Int32 DailyScheduleTypeID
        {
            get { return _DailyScheduleTypeID; }
            set { _DailyScheduleTypeID = value; }
        }
        [Column(Name = "HoursIndex", DataType = "Int16")]
        public Int16 HoursIndex
        {
            get { return _HoursIndex; }
            set { _HoursIndex = value; }
        }
        [Column(Name = "StartTime", DataType = "String")]
        public String StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        [Column(Name = "EndTime", DataType = "String")]
        public String EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        [Column(Name = "GCDailyScheduleType", DataType = "String")]
        public String GCDailyScheduleType
        {
            get { return _GCDailyScheduleType; }
            set { _GCDailyScheduleType = value; }
        }
        [Column(Name = "DailyScheduleType", DataType = "String")]
        public String DailyScheduleType
        {
            get { return _DailyScheduleType; }
            set { _DailyScheduleType = value; }
        }
    }
    #endregion
    #region vDirectPaymentDt
    [Serializable]
    [Table(Name = "vDirectPaymentDt")]
    public partial class vDirectPaymentDt
    {
        private Int32 _PaymentDetailID;
        private Int32 _PaymentID;
        private String _PaymentNo;
        private DateTime _PaymentDate;
        private String _PaymentTime;
        private String _GCPaymentMethod;
        private String _PaymentMethod;
        private Int32 _EDCMachineID;
        private String _EDCMachineName;
        private String _GCCardType;
        private String _CardType;
        private String _GCCardProvider;
        private String _CardProvider;
        private String _CardNumber;
        private String _CardHolderName;
        private String _CardValidThru;
        private Int32 _BankID;
        private String _BankName;
        private String _ReferenceNo;
        private Decimal _PaymentAmount;
        private Decimal _TotalPaymentAmount;
        private Decimal _CardFeeAmount;
        private Decimal _TotalFeeAmount;
        private Boolean _IsDeleted;
        private String _Cashier;

        [Column(Name = "PaymentDetailID", DataType = "Int32")]
        public Int32 PaymentDetailID
        {
            get { return _PaymentDetailID; }
            set { _PaymentDetailID = value; }
        }
        [Column(Name = "PaymentID", DataType = "Int32")]
        public Int32 PaymentID
        {
            get { return _PaymentID; }
            set { _PaymentID = value; }
        }
        [Column(Name = "PaymentNo", DataType = "String")]
        public String PaymentNo
        {
            get { return _PaymentNo; }
            set { _PaymentNo = value; }
        }
        [Column(Name = "PaymentDate", DataType = "DateTime")]
        public DateTime PaymentDate
        {
            get { return _PaymentDate; }
            set { _PaymentDate = value; }
        }
        [Column(Name = "PaymentTime", DataType = "String")]
        public String PaymentTime
        {
            get { return _PaymentTime; }
            set { _PaymentTime = value; }
        }
        [Column(Name = "GCPaymentMethod", DataType = "String")]
        public String GCPaymentMethod
        {
            get { return _GCPaymentMethod; }
            set { _GCPaymentMethod = value; }
        }
        [Column(Name = "PaymentMethod", DataType = "String")]
        public String PaymentMethod
        {
            get { return _PaymentMethod; }
            set { _PaymentMethod = value; }
        }
        [Column(Name = "EDCMachineID", DataType = "Int32")]
        public Int32 EDCMachineID
        {
            get { return _EDCMachineID; }
            set { _EDCMachineID = value; }
        }
        [Column(Name = "EDCMachineName", DataType = "String")]
        public String EDCMachineName
        {
            get { return _EDCMachineName; }
            set { _EDCMachineName = value; }
        }
        [Column(Name = "GCCardType", DataType = "String")]
        public String GCCardType
        {
            get { return _GCCardType; }
            set { _GCCardType = value; }
        }
        [Column(Name = "CardType", DataType = "String")]
        public String CardType
        {
            get { return _CardType; }
            set { _CardType = value; }
        }
        [Column(Name = "GCCardProvider", DataType = "String")]
        public String GCCardProvider
        {
            get { return _GCCardProvider; }
            set { _GCCardProvider = value; }
        }
        [Column(Name = "CardProvider", DataType = "String")]
        public String CardProvider
        {
            get { return _CardProvider; }
            set { _CardProvider = value; }
        }
        [Column(Name = "CardNumber", DataType = "String")]
        public String CardNumber
        {
            get { return _CardNumber; }
            set { _CardNumber = value; }
        }
        [Column(Name = "CardHolderName", DataType = "String")]
        public String CardHolderName
        {
            get { return _CardHolderName; }
            set { _CardHolderName = value; }
        }
        [Column(Name = "CardValidThru", DataType = "String")]
        public String CardValidThru
        {
            get { return _CardValidThru; }
            set { _CardValidThru = value; }
        }
        [Column(Name = "BankID", DataType = "Int32")]
        public Int32 BankID
        {
            get { return _BankID; }
            set { _BankID = value; }
        }
        [Column(Name = "BankName", DataType = "String")]
        public String BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String")]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "PaymentAmount", DataType = "Decimal")]
        public Decimal PaymentAmount
        {
            get { return _PaymentAmount; }
            set { _PaymentAmount = value; }
        }
        [Column(Name = "TotalPaymentAmount", DataType = "Decimal")]
        public Decimal TotalPaymentAmount
        {
            get { return _TotalPaymentAmount; }
            set { _TotalPaymentAmount = value; }
        }
        [Column(Name = "CardFeeAmount", DataType = "Decimal")]
        public Decimal CardFeeAmount
        {
            get { return _CardFeeAmount; }
            set { _CardFeeAmount = value; }
        }
        [Column(Name = "TotalFeeAmount", DataType = "Decimal")]
        public Decimal TotalFeeAmount
        {
            get { return _TotalFeeAmount; }
            set { _TotalFeeAmount = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "Cashier", DataType = "String")]
        public String Cashier
        {
            get { return _Cashier; }
            set { _Cashier = value; }
        }
    }
    #endregion
    #region vDirectPaymentHd
    [Serializable]
    [Table(Name = "vDirectPaymentHd")]
    public class vDirectPaymentHd
    {
        private Int32 _PaymentID;
        private String _PaymentNo;
        private Int32 _SalesInvoiceID;
        private DateTime _PaymentDate;
        private String _PaymentTime;
        private String _GCPaymentType;
        private String _PaymentType;
        private Decimal _TotalPaymentAmount;
        private Decimal _TotalFeeAmount;
        private Decimal _CashReturnAmount;
        private String _GCTransactionStatus;
        private Int32 _PaymentReceiptID;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "PaymentID", DataType = "Int32")]
        public Int32 PaymentID
        {
            get { return _PaymentID; }
            set { _PaymentID = value; }
        }
        [Column(Name = "PaymentNo", DataType = "String")]
        public String PaymentNo
        {
            get { return _PaymentNo; }
            set { _PaymentNo = value; }
        }
        [Column(Name = "SalesInvoiceID", DataType = "Int32")]
        public Int32 SalesInvoiceID
        {
            get { return _SalesInvoiceID; }
            set { _SalesInvoiceID = value; }
        }
        [Column(Name = "PaymentDate", DataType = "DateTime")]
        public DateTime PaymentDate
        {
            get { return _PaymentDate; }
            set { _PaymentDate = value; }
        }
        [Column(Name = "PaymentTime", DataType = "String")]
        public String PaymentTime
        {
            get { return _PaymentTime; }
            set { _PaymentTime = value; }
        }
        [Column(Name = "GCPaymentType", DataType = "String")]
        public String GCPaymentType
        {
            get { return _GCPaymentType; }
            set { _GCPaymentType = value; }
        }
        [Column(Name = "PaymentType", DataType = "String")]
        public String PaymentType
        {
            get { return _PaymentType; }
            set { _PaymentType = value; }
        }
        [Column(Name = "TotalPaymentAmount", DataType = "Decimal")]
        public Decimal TotalPaymentAmount
        {
            get { return _TotalPaymentAmount; }
            set { _TotalPaymentAmount = value; }
        }
        [Column(Name = "TotalFeeAmount", DataType = "Decimal")]
        public Decimal TotalFeeAmount
        {
            get { return _TotalFeeAmount; }
            set { _TotalFeeAmount = value; }
        }
        [Column(Name = "CashReturnAmount", DataType = "Decimal")]
        public Decimal CashReturnAmount
        {
            get { return _CashReturnAmount; }
            set { _CashReturnAmount = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "PaymentReceiptID", DataType = "Int32")]
        public Int32 PaymentReceiptID
        {
            get { return _PaymentReceiptID; }
            set { _PaymentReceiptID = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vDirectPurchaseDt
    [Serializable]
    [Table(Name = "vDirectPurchaseDt")]
    public partial class vDirectPurchaseDt
    {
        private Int32 _ID;
        private Int32 _DirectPurchaseID;
        private String _DirectPurchaseNo;
        private DateTime _PurchaseDate;
        private Int32 _LocationID;
        private String _LocationCode;
        private String _LocationName;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private Decimal _VATPercentage;
        private Decimal _TotalTransactionAmount;
        private String _CreatedByUserName;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Decimal _Quantity;
        private String _GCItemUnit;
        private String _ItemUnit;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private Decimal _ConversionFactor;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage;
        private Decimal _DiscountAmount;
        private Decimal _LineAmount;
        private String _GCItemDetailStatus;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "DirectPurchaseID", DataType = "Int32")]
        public Int32 DirectPurchaseID
        {
            get { return _DirectPurchaseID; }
            set { _DirectPurchaseID = value; }
        }
        [Column(Name = "DirectPurchaseNo", DataType = "String")]
        public String DirectPurchaseNo
        {
            get { return _DirectPurchaseNo; }
            set { _DirectPurchaseNo = value; }
        }
        [Column(Name = "PurchaseDate", DataType = "DateTime")]
        public DateTime PurchaseDate
        {
            get { return _PurchaseDate; }
            set { _PurchaseDate = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "LocationCode", DataType = "String")]
        public String LocationCode
        {
            get { return _LocationCode; }
            set { _LocationCode = value; }
        }
        [Column(Name = "LocationName", DataType = "String")]
        public String LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "VATPercentage", DataType = "Decimal")]
        public Decimal VATPercentage
        {
            get { return _VATPercentage; }
            set { _VATPercentage = value; }
        }
        [Column(Name = "TotalTransactionAmount", DataType = "Decimal")]
        public Decimal TotalTransactionAmount
        {
            get { return _TotalTransactionAmount; }
            set { _TotalTransactionAmount = value; }
        }
        [Column(Name = "CreatedByUserName", DataType = "String")]
        public String CreatedByUserName
        {
            get { return _CreatedByUserName; }
            set { _CreatedByUserName = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "UnitPrice", DataType = "Decimal")]
        public Decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        [Column(Name = "DiscountPercentage", DataType = "Decimal")]
        public Decimal DiscountPercentage
        {
            get { return _DiscountPercentage; }
            set { _DiscountPercentage = value; }
        }
        [Column(Name = "DiscountAmount", DataType = "Decimal")]
        public Decimal DiscountAmount
        {
            get { return _DiscountAmount; }
            set { _DiscountAmount = value; }
        }
        [Column(Name = "LineAmount", DataType = "Decimal")]
        public Decimal LineAmount
        {
            get { return _LineAmount; }
            set { _LineAmount = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
    }
    #endregion
    #region vDirectPurchaseHd
    [Serializable]
    [Table(Name = "vDirectPurchaseHd")]
    public partial class vDirectPurchaseHd
    {
        private Int32 _DirectPurchaseID;
        private String _DirectPurchaseNo;
        private DateTime _PurchaseDate;
        private Int32 _LocationID;
        private String _LocationCode;
        private String _LocationName;
        private Int32 _LocationItemGroupID;
        private Boolean _IsHasPurchaseReturn;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private String _GCDirectPurchaseType;
        private String _ReferenceNo;
        private DateTime _ReferenceDate;
        private Boolean _IsIncludeVAT;
        private Decimal _TransactionAmount;
        private Decimal _VATPercentage;
        private Decimal _VATAmount;
        private Decimal _FinalDiscountAmount;
        private Decimal _TotalNetTransactionAmount;
        private String _Remarks;
        private String _GCTransactionStatus;
        private String _TransactionStatusWatermark;
        private Int32 _CreatedBy;
        private String _CreatedByUserName;

        [Column(Name = "DirectPurchaseID", DataType = "Int32")]
        public Int32 DirectPurchaseID
        {
            get { return _DirectPurchaseID; }
            set { _DirectPurchaseID = value; }
        }
        [Column(Name = "DirectPurchaseNo", DataType = "String")]
        public String DirectPurchaseNo
        {
            get { return _DirectPurchaseNo; }
            set { _DirectPurchaseNo = value; }
        }
        [Column(Name = "PurchaseDate", DataType = "DateTime")]
        public DateTime PurchaseDate
        {
            get { return _PurchaseDate; }
            set { _PurchaseDate = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "LocationCode", DataType = "String")]
        public String LocationCode
        {
            get { return _LocationCode; }
            set { _LocationCode = value; }
        }
        [Column(Name = "LocationName", DataType = "String")]
        public String LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }
        [Column(Name = "LocationItemGroupID", DataType = "Int32")]
        public Int32 LocationItemGroupID
        {
            get { return _LocationItemGroupID; }
            set { _LocationItemGroupID = value; }
        }
        [Column(Name = "IsHasPurchaseReturn", DataType = "Boolean")]
        public Boolean IsHasPurchaseReturn
        {
            get { return _IsHasPurchaseReturn; }
            set { _IsHasPurchaseReturn = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "GCDirectPurchaseType", DataType = "String")]
        public String GCDirectPurchaseType
        {
            get { return _GCDirectPurchaseType; }
            set { _GCDirectPurchaseType = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String")]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "ReferenceDate", DataType = "DateTime")]
        public DateTime ReferenceDate
        {
            get { return _ReferenceDate; }
            set { _ReferenceDate = value; }
        }
        [Column(Name = "IsIncludeVAT", DataType = "Boolean")]
        public Boolean IsIncludeVAT
        {
            get { return _IsIncludeVAT; }
            set { _IsIncludeVAT = value; }
        }
        [Column(Name = "TransactionAmount", DataType = "Decimal")]
        public Decimal TransactionAmount
        {
            get { return _TransactionAmount; }
            set { _TransactionAmount = value; }
        }
        [Column(Name = "VATPercentage", DataType = "Decimal")]
        public Decimal VATPercentage
        {
            get { return _VATPercentage; }
            set { _VATPercentage = value; }
        }
        [Column(Name = "VATAmount", DataType = "Decimal")]
        public Decimal VATAmount
        {
            get { return _VATAmount; }
            set { _VATAmount = value; }
        }
        [Column(Name = "FinalDiscountAmount", DataType = "Decimal")]
        public Decimal FinalDiscountAmount
        {
            get { return _FinalDiscountAmount; }
            set { _FinalDiscountAmount = value; }
        }
        [Column(Name = "TotalNetTransactionAmount", DataType = "Decimal")]
        public Decimal TotalNetTransactionAmount
        {
            get { return _TotalNetTransactionAmount; }
            set { _TotalNetTransactionAmount = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedByUserName", DataType = "String")]
        public String CreatedByUserName
        {
            get { return _CreatedByUserName; }
            set { _CreatedByUserName = value; }
        }
    }
    #endregion
    #region vDirectPurchaseReturnDt
    [Serializable]
    [Table(Name = "vDirectPurchaseReturnDt")]
    public partial class vDirectPurchaseReturnDt
    {
        private Int32 _ID;
        private Int32 _DirectPurchaseReturnID;
        private String _DirectPurchaseReturnNo;
        private DateTime _ReturnDate;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _SupplierName;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Decimal _ReceivedQuantity;
        private String _ReceivedItemUnit;
        private Decimal _Quantity;
        private String _ItemUnit;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private Decimal _ConversionFactor;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage;
        private Decimal _DiscountAmount;
        private Decimal _LineAmount;
        private String _GCPurchaseReturnReason;
        private String _PurchaseReturnReason;
        private Int32 _SupplierID;
        private String _GCTransactionStatus;
        private String _GCItemUnit;
        private String _GCItemDetailStatus;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "DirectPurchaseReturnID", DataType = "Int32")]
        public Int32 DirectPurchaseReturnID
        {
            get { return _DirectPurchaseReturnID; }
            set { _DirectPurchaseReturnID = value; }
        }
        [Column(Name = "DirectPurchaseReturnNo", DataType = "String")]
        public String DirectPurchaseReturnNo
        {
            get { return _DirectPurchaseReturnNo; }
            set { _DirectPurchaseReturnNo = value; }
        }
        [Column(Name = "ReturnDate", DataType = "DateTime")]
        public DateTime ReturnDate
        {
            get { return _ReturnDate; }
            set { _ReturnDate = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "SupplierName", DataType = "String")]
        public String SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "ReceivedQuantity", DataType = "Decimal")]
        public Decimal ReceivedQuantity
        {
            get { return _ReceivedQuantity; }
            set { _ReceivedQuantity = value; }
        }
        [Column(Name = "ReceivedItemUnit", DataType = "String")]
        public String ReceivedItemUnit
        {
            get { return _ReceivedItemUnit; }
            set { _ReceivedItemUnit = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "UnitPrice", DataType = "Decimal")]
        public Decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        [Column(Name = "DiscountPercentage", DataType = "Decimal")]
        public Decimal DiscountPercentage
        {
            get { return _DiscountPercentage; }
            set { _DiscountPercentage = value; }
        }
        [Column(Name = "DiscountAmount", DataType = "Decimal")]
        public Decimal DiscountAmount
        {
            get { return _DiscountAmount; }
            set { _DiscountAmount = value; }
        }
        [Column(Name = "LineAmount", DataType = "Decimal")]
        public Decimal LineAmount
        {
            get { return _LineAmount; }
            set { _LineAmount = value; }
        }
        [Column(Name = "GCPurchaseReturnReason", DataType = "String")]
        public String GCPurchaseReturnReason
        {
            get { return _GCPurchaseReturnReason; }
            set { _GCPurchaseReturnReason = value; }
        }
        [Column(Name = "PurchaseReturnReason", DataType = "String")]
        public String PurchaseReturnReason
        {
            get { return _PurchaseReturnReason; }
            set { _PurchaseReturnReason = value; }
        }
        [Column(Name = "SupplierID", DataType = "Int32")]
        public Int32 SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
    }
    #endregion
    #region vDirectPurchaseReturnHd
    [Serializable]
    [Table(Name = "vDirectPurchaseReturnHd")]
    public partial class vDirectPurchaseReturnHd
    {
        private Int32 _DirectPurchaseReturnID;
        private DateTime _ReturnDate;
        private String _DirectPurchaseReturnNo;
        private Int32 _DirectPurchaseID;
        private String _DirectPurchaseNo;
        private Int32 _LocationID;
        private String _LocationCode;
        private String _LocationName;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _SupplierName;
        private String _GCDirectPurchaseReturnType;
        private String _PurchaseReturnType;
        private String _ReferenceNo;
        private DateTime _ReferenceDate;
        private Boolean _IsIncludeVAT;
        private Decimal _TransactionAmount;
        private Decimal _VATPercentage;
        private Decimal _VATAmount;
        private Decimal _FinalDiscountAmount;
        private Decimal _TotalNetTransactionAmount;
        private String _Remarks;
        private String _GCTransactionStatus;
        private String _TransactionStatusWatermark;
        private Int32 _NumberOfItems;
        private Int32 _NumberOfApprovedItems;

        [Column(Name = "DirectPurchaseReturnID", DataType = "Int32")]
        public Int32 DirectPurchaseReturnID
        {
            get { return _DirectPurchaseReturnID; }
            set { _DirectPurchaseReturnID = value; }
        }
        [Column(Name = "ReturnDate", DataType = "DateTime")]
        public DateTime ReturnDate
        {
            get { return _ReturnDate; }
            set { _ReturnDate = value; }
        }
        [Column(Name = "DirectPurchaseReturnNo", DataType = "String")]
        public String DirectPurchaseReturnNo
        {
            get { return _DirectPurchaseReturnNo; }
            set { _DirectPurchaseReturnNo = value; }
        }
        [Column(Name = "DirectPurchaseID", DataType = "Int32")]
        public Int32 DirectPurchaseID
        {
            get { return _DirectPurchaseID; }
            set { _DirectPurchaseID = value; }
        }
        [Column(Name = "DirectPurchaseNo", DataType = "String")]
        public String DirectPurchaseNo
        {
            get { return _DirectPurchaseNo; }
            set { _DirectPurchaseNo = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "LocationCode", DataType = "String")]
        public String LocationCode
        {
            get { return _LocationCode; }
            set { _LocationCode = value; }
        }
        [Column(Name = "LocationName", DataType = "String")]
        public String LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "SupplierName", DataType = "String")]
        public String SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }
        [Column(Name = "GCDirectPurchaseReturnType", DataType = "String")]
        public String GCDirectPurchaseReturnType
        {
            get { return _GCDirectPurchaseReturnType; }
            set { _GCDirectPurchaseReturnType = value; }
        }
        [Column(Name = "PurchaseReturnType", DataType = "String")]
        public String PurchaseReturnType
        {
            get { return _PurchaseReturnType; }
            set { _PurchaseReturnType = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String")]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "ReferenceDate", DataType = "DateTime")]
        public DateTime ReferenceDate
        {
            get { return _ReferenceDate; }
            set { _ReferenceDate = value; }
        }
        [Column(Name = "IsIncludeVAT", DataType = "Boolean")]
        public Boolean IsIncludeVAT
        {
            get { return _IsIncludeVAT; }
            set { _IsIncludeVAT = value; }
        }
        [Column(Name = "TransactionAmount", DataType = "Decimal")]
        public Decimal TransactionAmount
        {
            get { return _TransactionAmount; }
            set { _TransactionAmount = value; }
        }
        [Column(Name = "VATPercentage", DataType = "Decimal")]
        public Decimal VATPercentage
        {
            get { return _VATPercentage; }
            set { _VATPercentage = value; }
        }
        [Column(Name = "VATAmount", DataType = "Decimal")]
        public Decimal VATAmount
        {
            get { return _VATAmount; }
            set { _VATAmount = value; }
        }
        [Column(Name = "FinalDiscountAmount", DataType = "Decimal")]
        public Decimal FinalDiscountAmount
        {
            get { return _FinalDiscountAmount; }
            set { _FinalDiscountAmount = value; }
        }
        [Column(Name = "TotalNetTransactionAmount", DataType = "Decimal")]
        public Decimal TotalNetTransactionAmount
        {
            get { return _TotalNetTransactionAmount; }
            set { _TotalNetTransactionAmount = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
        [Column(Name = "NumberOfItems", DataType = "Int32")]
        public Int32 NumberOfItems
        {
            get { return _NumberOfItems; }
            set { _NumberOfItems = value; }
        }
        [Column(Name = "NumberOfApprovedItems", DataType = "Int32")]
        public Int32 NumberOfApprovedItems
        {
            get { return _NumberOfApprovedItems; }
            set { _NumberOfApprovedItems = value; }
        }
    }
    #endregion
    #region vEDCMachine
    [Serializable]
    [Table(Name = "vEDCMachine")]
    public class vEDCMachine
    {
        private Int32 _EDCMachineID;
        private String _EDCMachineCode;
        private String _EDCMachineName;
        private String _GCCardProvider;
        private String _CardProvider;
        private Boolean _IsDeleted;

        [Column(Name = "EDCMachineID", DataType = "Int32")]
        public Int32 EDCMachineID
        {
            get { return _EDCMachineID; }
            set { _EDCMachineID = value; }
        }
        [Column(Name = "EDCMachineCode", DataType = "String")]
        public String EDCMachineCode
        {
            get { return _EDCMachineCode; }
            set { _EDCMachineCode = value; }
        }
        [Column(Name = "EDCMachineName", DataType = "String")]
        public String EDCMachineName
        {
            get { return _EDCMachineName; }
            set { _EDCMachineName = value; }
        }
        [Column(Name = "GCCardProvider", DataType = "String")]
        public String GCCardProvider
        {
            get { return _GCCardProvider; }
            set { _GCCardProvider = value; }
        }
        [Column(Name = "CardProvider", DataType = "String")]
        public String CardProvider
        {
            get { return _CardProvider; }
            set { _CardProvider = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vExamClassSchedule
    [Serializable]
    [Table(Name = "vExamClassSchedule")]
    public class vExamClassSchedule
    {
        private Int32 _ExamScheduleDtID;
        private Int32 _SchoolClassID;
        private Int32 _RoomID;
        private String _RoomName;
        private Int32 _EmployeeID;
        private String _EmployeeName;
        private Boolean _IsDeleted;

        [Column(Name = "ExamScheduleDtID", DataType = "Int32")]
        public Int32 ExamScheduleDtID
        {
            get { return _ExamScheduleDtID; }
            set { _ExamScheduleDtID = value; }
        }
        [Column(Name = "SchoolClassID", DataType = "Int32")]
        public Int32 SchoolClassID
        {
            get { return _SchoolClassID; }
            set { _SchoolClassID = value; }
        }
        [Column(Name = "RoomID", DataType = "Int32")]
        public Int32 RoomID
        {
            get { return _RoomID; }
            set { _RoomID = value; }
        }
        [Column(Name = "RoomName", DataType = "String")]
        public String RoomName
        {
            get { return _RoomName; }
            set { _RoomName = value; }
        }
        [Column(Name = "EmployeeID", DataType = "Int32")]
        public Int32 EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }
        [Column(Name = "EmployeeName", DataType = "String")]
        public String EmployeeName
        {
            get { return _EmployeeName; }
            set { _EmployeeName = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vExamScheduleDt
    [Serializable]
    [Table(Name = "vExamScheduleDt")]
    public class vExamScheduleDt
    {
        private Int32 _ExamScheduleDtID;
        private Int32 _ExamScheduleID;
        private Int32 _SubjectID;
        private String _SubjectName;
        private DateTime _ExamDate;
        private Int16 _DayNumber;
        private Int16 _HoursIndex;
        private String _StartTime;
        private String _EndTime;
        private Boolean _IsDeleted;

        [Column(Name = "ExamScheduleDtID", DataType = "Int32")]
        public Int32 ExamScheduleDtID
        {
            get { return _ExamScheduleDtID; }
            set { _ExamScheduleDtID = value; }
        }
        [Column(Name = "ExamScheduleID", DataType = "Int32")]
        public Int32 ExamScheduleID
        {
            get { return _ExamScheduleID; }
            set { _ExamScheduleID = value; }
        }
        [Column(Name = "SubjectID", DataType = "Int32")]
        public Int32 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
        [Column(Name = "SubjectName", DataType = "String")]
        public String SubjectName
        {
            get { return _SubjectName; }
            set { _SubjectName = value; }
        }
        [Column(Name = "ExamDate", DataType = "DateTime")]
        public DateTime ExamDate
        {
            get { return _ExamDate; }
            set { _ExamDate = value; }
        }
        [Column(Name = "DayNumber", DataType = "Int16")]
        public Int16 DayNumber
        {
            get { return _DayNumber; }
            set { _DayNumber = value; }
        }
        [Column(Name = "HoursIndex", DataType = "Int16")]
        public Int16 HoursIndex
        {
            get { return _HoursIndex; }
            set { _HoursIndex = value; }
        }
        [Column(Name = "StartTime", DataType = "String")]
        public String StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        [Column(Name = "EndTime", DataType = "String")]
        public String EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vExamScheduleHd
    [Serializable]
    [Table(Name = "vExamScheduleHd")]
    public class vExamScheduleHd
    {
        private Int32 _ExamScheduleID;
        private Int32 _PeriodSectionID;
        private Int32 _PeriodClassTypeID;
        private String _ClassTypeCode;
        private String _ClassTypeName;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private Int32 _ExamSchedulePackageID;
        private String _GCExaminationType;
        private String _GCTransactionStatus;

        [Column(Name = "ExamScheduleID", DataType = "Int32")]
        public Int32 ExamScheduleID
        {
            get { return _ExamScheduleID; }
            set { _ExamScheduleID = value; }
        }
        [Column(Name = "PeriodSectionID", DataType = "Int32")]
        public Int32 PeriodSectionID
        {
            get { return _PeriodSectionID; }
            set { _PeriodSectionID = value; }
        }
        [Column(Name = "PeriodClassTypeID", DataType = "Int32")]
        public Int32 PeriodClassTypeID
        {
            get { return _PeriodClassTypeID; }
            set { _PeriodClassTypeID = value; }
        }
        [Column(Name = "ClassTypeCode", DataType = "String")]
        public String ClassTypeCode
        {
            get { return _ClassTypeCode; }
            set { _ClassTypeCode = value; }
        }
        [Column(Name = "ClassTypeName", DataType = "String")]
        public String ClassTypeName
        {
            get { return _ClassTypeName; }
            set { _ClassTypeName = value; }
        }
        [Column(Name = "StartDate", DataType = "DateTime")]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Column(Name = "EndDate", DataType = "DateTime")]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        [Column(Name = "ExamSchedulePackageID", DataType = "Int32")]
        public Int32 ExamSchedulePackageID
        {
            get { return _ExamSchedulePackageID; }
            set { _ExamSchedulePackageID = value; }
        }
        [Column(Name = "GCExaminationType", DataType = "String")]
        public String GCExaminationType
        {
            get { return _GCExaminationType; }
            set { _GCExaminationType = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
    }
    #endregion
    #region vFADepreciation
    [Serializable]
    [Table(Name = "vFADepreciation")]
    public partial class vFADepreciation
    {
        private Int32 _FADepreciationID;
        private Int32 _FixedAssetID;
        private String _FixedAssetCode;
        private String _FixedAssetName;
        private String _PeriodNo;
        private DateTime _DepreciationDate;
        private Decimal _ProcurementAmount;
        private Decimal _AssetValue;
        private Decimal _DepreciationAmount;
        private Decimal _TotalDepreciationAmount;
        private Int32 _GLJournalID;

        [Column(Name = "FADepreciationID", DataType = "Int32")]
        public Int32 FADepreciationID
        {
            get { return _FADepreciationID; }
            set { _FADepreciationID = value; }
        }
        [Column(Name = "FixedAssetID", DataType = "Int32")]
        public Int32 FixedAssetID
        {
            get { return _FixedAssetID; }
            set { _FixedAssetID = value; }
        }
        [Column(Name = "FixedAssetCode", DataType = "String")]
        public String FixedAssetCode
        {
            get { return _FixedAssetCode; }
            set { _FixedAssetCode = value; }
        }
        [Column(Name = "FixedAssetName", DataType = "String")]
        public String FixedAssetName
        {
            get { return _FixedAssetName; }
            set { _FixedAssetName = value; }
        }
        [Column(Name = "PeriodNo", DataType = "String")]
        public String PeriodNo
        {
            get { return _PeriodNo; }
            set { _PeriodNo = value; }
        }
        [Column(Name = "DepreciationDate", DataType = "DateTime")]
        public DateTime DepreciationDate
        {
            get { return _DepreciationDate; }
            set { _DepreciationDate = value; }
        }
        [Column(Name = "ProcurementAmount", DataType = "Decimal")]
        public Decimal ProcurementAmount
        {
            get { return _ProcurementAmount; }
            set { _ProcurementAmount = value; }
        }
        [Column(Name = "AssetValue", DataType = "Decimal")]
        public Decimal AssetValue
        {
            get { return _AssetValue; }
            set { _AssetValue = value; }
        }
        [Column(Name = "DepreciationAmount", DataType = "Decimal")]
        public Decimal DepreciationAmount
        {
            get { return _DepreciationAmount; }
            set { _DepreciationAmount = value; }
        }
        [Column(Name = "TotalDepreciationAmount", DataType = "Decimal")]
        public Decimal TotalDepreciationAmount
        {
            get { return _TotalDepreciationAmount; }
            set { _TotalDepreciationAmount = value; }
        }
        [Column(Name = "GLJournalID", DataType = "Int32")]
        public Int32 GLJournalID
        {
            get { return _GLJournalID; }
            set { _GLJournalID = value; }
        }
    }
    #endregion
    #region vFAGroup
    [Serializable]
    [Table(Name = "vFAGroup")]
    public class vFAGroup
    {
        private Int32 _FAGroupID;
        private String _FAGroupCode;
        private String _FAGroupName;
        private Int32 _MethodID;
        private String _MethodCode;
        private String _MethodName;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "FAGroupID", DataType = "Int32")]
        public Int32 FAGroupID
        {
            get { return _FAGroupID; }
            set { _FAGroupID = value; }
        }
        [Column(Name = "FAGroupCode", DataType = "String")]
        public String FAGroupCode
        {
            get { return _FAGroupCode; }
            set { _FAGroupCode = value; }
        }
        [Column(Name = "FAGroupName", DataType = "String")]
        public String FAGroupName
        {
            get { return _FAGroupName; }
            set { _FAGroupName = value; }
        }
        [Column(Name = "MethodID", DataType = "Int32")]
        public Int32 MethodID
        {
            get { return _MethodID; }
            set { _MethodID = value; }
        }
        [Column(Name = "MethodCode", DataType = "String")]
        public String MethodCode
        {
            get { return _MethodCode; }
            set { _MethodCode = value; }
        }
        [Column(Name = "MethodName", DataType = "String")]
        public String MethodName
        {
            get { return _MethodName; }
            set { _MethodName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vFAGroupCOA
    [Serializable]
    [Table(Name = "vFAGroupCOA")]
    public class vFAGroupCOA
    {
        private String _SiteID;
        private Int32 _FAGroupID;
        private Int32 _GLAccount1;
        private String _GLAccount1No;
        private String _GLAccount1Name;
        private Int32 _GLAccount2;
        private String _GLAccount2No;
        private String _GLAccount2Name;
        private Int32 _GLAccount3;
        private String _GLAccount3No;
        private String _GLAccount3Name;
        private Int32 _GLAccount4;
        private String _GLAccount4No;
        private String _GLAccount4Name;
        private Int32 _GLAccount5;
        private String _GLAccount5No;
        private String _GLAccount5Name;
        private Int32 _GLAccount6;
        private String _GLAccount6No;
        private String _GLAccount6Name;
        private Int32 _SubLedger1;
        private String _SubLedger1Name;
        private String _SubLedger1Code;
        private Int32 _SubLedgerID1;
        private Int32 _SubLedgerTypeID1;
        private String _MethodName1;
        private String _FilterExpression1;
        private String _IDFieldName1;
        private String _CodeFieldName1;
        private String _DisplayFieldName1;
        private String _SearchDialogTypeName1;
        private String _TableName1;
        private Int32 _SubLedger2;
        private String _SubLedger2Name;
        private String _SubLedger2Code;
        private Int32 _SubLedgerID2;
        private Int32 _SubLedgerTypeID2;
        private String _MethodName2;
        private String _FilterExpression2;
        private String _IDFieldName2;
        private String _CodeFieldName2;
        private String _DisplayFieldName2;
        private String _SearchDialogTypeName2;
        private Int32 _SubLedger3;
        private String _SubLedger3Name;
        private String _SubLedger3Code;
        private Int32 _SubLedgerID3;
        private Int32 _SubLedgerTypeID3;
        private String _MethodName3;
        private String _FilterExpression3;
        private String _IDFieldName3;
        private String _CodeFieldName3;
        private String _DisplayFieldName3;
        private String _SearchDialogTypeName3;
        private Int32 _SubLedger4;
        private String _SubLedger4Name;
        private String _SubLedger4Code;
        private Int32 _SubLedgerID4;
        private Int32 _SubLedgerTypeID4;
        private String _MethodName4;
        private String _FilterExpression4;
        private String _IDFieldName4;
        private String _CodeFieldName4;
        private String _DisplayFieldName4;
        private String _SearchDialogTypeName4;
        private Int32 _SubLedger5;
        private String _SubLedger5Name;
        private String _SubLedger5Code;
        private Int32 _SubLedgerID5;
        private Int32 _SubLedgerTypeID5;
        private String _MethodName5;
        private String _FilterExpression5;
        private String _IDFieldName5;
        private String _CodeFieldName5;
        private String _DisplayFieldName5;
        private String _SearchDialogTypeName5;
        private Int32 _SubLedger6;
        private String _SubLedger6Name;
        private String _SubLedger6Code;
        private Int32 _SubLedgerID6;
        private Int32 _SubLedgerTypeID6;
        private String _MethodName6;
        private String _FilterExpression6;
        private String _IDFieldName6;
        private String _CodeFieldName6;
        private String _DisplayFieldName6;
        private String _SearchDialogTypeName6;

        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "FAGroupID", DataType = "Int32")]
        public Int32 FAGroupID
        {
            get { return _FAGroupID; }
            set { _FAGroupID = value; }
        }
        [Column(Name = "GLAccount1", DataType = "Int32")]
        public Int32 GLAccount1
        {
            get { return _GLAccount1; }
            set { _GLAccount1 = value; }
        }
        [Column(Name = "GLAccount1No", DataType = "String")]
        public String GLAccount1No
        {
            get { return _GLAccount1No; }
            set { _GLAccount1No = value; }
        }
        [Column(Name = "GLAccount1Name", DataType = "String")]
        public String GLAccount1Name
        {
            get { return _GLAccount1Name; }
            set { _GLAccount1Name = value; }
        }
        [Column(Name = "GLAccount2", DataType = "Int32")]
        public Int32 GLAccount2
        {
            get { return _GLAccount2; }
            set { _GLAccount2 = value; }
        }
        [Column(Name = "GLAccount2No", DataType = "String")]
        public String GLAccount2No
        {
            get { return _GLAccount2No; }
            set { _GLAccount2No = value; }
        }
        [Column(Name = "GLAccount2Name", DataType = "String")]
        public String GLAccount2Name
        {
            get { return _GLAccount2Name; }
            set { _GLAccount2Name = value; }
        }
        [Column(Name = "GLAccount3", DataType = "Int32")]
        public Int32 GLAccount3
        {
            get { return _GLAccount3; }
            set { _GLAccount3 = value; }
        }
        [Column(Name = "GLAccount3No", DataType = "String")]
        public String GLAccount3No
        {
            get { return _GLAccount3No; }
            set { _GLAccount3No = value; }
        }
        [Column(Name = "GLAccount3Name", DataType = "String")]
        public String GLAccount3Name
        {
            get { return _GLAccount3Name; }
            set { _GLAccount3Name = value; }
        }
        [Column(Name = "GLAccount4", DataType = "Int32")]
        public Int32 GLAccount4
        {
            get { return _GLAccount4; }
            set { _GLAccount4 = value; }
        }
        [Column(Name = "GLAccount4No", DataType = "String")]
        public String GLAccount4No
        {
            get { return _GLAccount4No; }
            set { _GLAccount4No = value; }
        }
        [Column(Name = "GLAccount4Name", DataType = "String")]
        public String GLAccount4Name
        {
            get { return _GLAccount4Name; }
            set { _GLAccount4Name = value; }
        }
        [Column(Name = "GLAccount5", DataType = "Int32")]
        public Int32 GLAccount5
        {
            get { return _GLAccount5; }
            set { _GLAccount5 = value; }
        }
        [Column(Name = "GLAccount5No", DataType = "String")]
        public String GLAccount5No
        {
            get { return _GLAccount5No; }
            set { _GLAccount5No = value; }
        }
        [Column(Name = "GLAccount5Name", DataType = "String")]
        public String GLAccount5Name
        {
            get { return _GLAccount5Name; }
            set { _GLAccount5Name = value; }
        }
        [Column(Name = "GLAccount6", DataType = "Int32")]
        public Int32 GLAccount6
        {
            get { return _GLAccount6; }
            set { _GLAccount6 = value; }
        }
        [Column(Name = "GLAccount6No", DataType = "String")]
        public String GLAccount6No
        {
            get { return _GLAccount6No; }
            set { _GLAccount6No = value; }
        }
        [Column(Name = "GLAccount6Name", DataType = "String")]
        public String GLAccount6Name
        {
            get { return _GLAccount6Name; }
            set { _GLAccount6Name = value; }
        }
        [Column(Name = "SubLedger1", DataType = "Int32")]
        public Int32 SubLedger1
        {
            get { return _SubLedger1; }
            set { _SubLedger1 = value; }
        }
        [Column(Name = "SubLedger1Name", DataType = "String")]
        public String SubLedger1Name
        {
            get { return _SubLedger1Name; }
            set { _SubLedger1Name = value; }
        }
        [Column(Name = "SubLedger1Code", DataType = "String")]
        public String SubLedger1Code
        {
            get { return _SubLedger1Code; }
            set { _SubLedger1Code = value; }
        }
        [Column(Name = "SubLedgerID1", DataType = "Int32")]
        public Int32 SubLedgerID1
        {
            get { return _SubLedgerID1; }
            set { _SubLedgerID1 = value; }
        }
        [Column(Name = "SubLedgerTypeID1", DataType = "Int32")]
        public Int32 SubLedgerTypeID1
        {
            get { return _SubLedgerTypeID1; }
            set { _SubLedgerTypeID1 = value; }
        }
        [Column(Name = "MethodName1", DataType = "String")]
        public String MethodName1
        {
            get { return _MethodName1; }
            set { _MethodName1 = value; }
        }
        [Column(Name = "FilterExpression1", DataType = "String")]
        public String FilterExpression1
        {
            get { return _FilterExpression1; }
            set { _FilterExpression1 = value; }
        }
        [Column(Name = "IDFieldName1", DataType = "String")]
        public String IDFieldName1
        {
            get { return _IDFieldName1; }
            set { _IDFieldName1 = value; }
        }
        [Column(Name = "CodeFieldName1", DataType = "String")]
        public String CodeFieldName1
        {
            get { return _CodeFieldName1; }
            set { _CodeFieldName1 = value; }
        }
        [Column(Name = "DisplayFieldName1", DataType = "String")]
        public String DisplayFieldName1
        {
            get { return _DisplayFieldName1; }
            set { _DisplayFieldName1 = value; }
        }
        [Column(Name = "SearchDialogTypeName1", DataType = "String")]
        public String SearchDialogTypeName1
        {
            get { return _SearchDialogTypeName1; }
            set { _SearchDialogTypeName1 = value; }
        }
        [Column(Name = "TableName1", DataType = "String")]
        public String TableName1
        {
            get { return _TableName1; }
            set { _TableName1 = value; }
        }
        [Column(Name = "SubLedger2", DataType = "Int32")]
        public Int32 SubLedger2
        {
            get { return _SubLedger2; }
            set { _SubLedger2 = value; }
        }
        [Column(Name = "SubLedger2Name", DataType = "String")]
        public String SubLedger2Name
        {
            get { return _SubLedger2Name; }
            set { _SubLedger2Name = value; }
        }
        [Column(Name = "SubLedger2Code", DataType = "String")]
        public String SubLedger2Code
        {
            get { return _SubLedger2Code; }
            set { _SubLedger2Code = value; }
        }
        [Column(Name = "SubLedgerID2", DataType = "Int32")]
        public Int32 SubLedgerID2
        {
            get { return _SubLedgerID2; }
            set { _SubLedgerID2 = value; }
        }
        [Column(Name = "SubLedgerTypeID2", DataType = "Int32")]
        public Int32 SubLedgerTypeID2
        {
            get { return _SubLedgerTypeID2; }
            set { _SubLedgerTypeID2 = value; }
        }
        [Column(Name = "MethodName2", DataType = "String")]
        public String MethodName2
        {
            get { return _MethodName2; }
            set { _MethodName2 = value; }
        }
        [Column(Name = "FilterExpression2", DataType = "String")]
        public String FilterExpression2
        {
            get { return _FilterExpression2; }
            set { _FilterExpression2 = value; }
        }
        [Column(Name = "IDFieldName2", DataType = "String")]
        public String IDFieldName2
        {
            get { return _IDFieldName2; }
            set { _IDFieldName2 = value; }
        }
        [Column(Name = "CodeFieldName2", DataType = "String")]
        public String CodeFieldName2
        {
            get { return _CodeFieldName2; }
            set { _CodeFieldName2 = value; }
        }
        [Column(Name = "DisplayFieldName2", DataType = "String")]
        public String DisplayFieldName2
        {
            get { return _DisplayFieldName2; }
            set { _DisplayFieldName2 = value; }
        }
        [Column(Name = "SearchDialogTypeName2", DataType = "String")]
        public String SearchDialogTypeName2
        {
            get { return _SearchDialogTypeName2; }
            set { _SearchDialogTypeName2 = value; }
        }
        [Column(Name = "SubLedger3", DataType = "Int32")]
        public Int32 SubLedger3
        {
            get { return _SubLedger3; }
            set { _SubLedger3 = value; }
        }
        [Column(Name = "SubLedger3Name", DataType = "String")]
        public String SubLedger3Name
        {
            get { return _SubLedger3Name; }
            set { _SubLedger3Name = value; }
        }
        [Column(Name = "SubLedger3Code", DataType = "String")]
        public String SubLedger3Code
        {
            get { return _SubLedger3Code; }
            set { _SubLedger3Code = value; }
        }
        [Column(Name = "SubLedgerID3", DataType = "Int32")]
        public Int32 SubLedgerID3
        {
            get { return _SubLedgerID3; }
            set { _SubLedgerID3 = value; }
        }
        [Column(Name = "SubLedgerTypeID3", DataType = "Int32")]
        public Int32 SubLedgerTypeID3
        {
            get { return _SubLedgerTypeID3; }
            set { _SubLedgerTypeID3 = value; }
        }
        [Column(Name = "MethodName3", DataType = "String")]
        public String MethodName3
        {
            get { return _MethodName3; }
            set { _MethodName3 = value; }
        }
        [Column(Name = "FilterExpression3", DataType = "String")]
        public String FilterExpression3
        {
            get { return _FilterExpression3; }
            set { _FilterExpression3 = value; }
        }
        [Column(Name = "IDFieldName3", DataType = "String")]
        public String IDFieldName3
        {
            get { return _IDFieldName3; }
            set { _IDFieldName3 = value; }
        }
        [Column(Name = "CodeFieldName3", DataType = "String")]
        public String CodeFieldName3
        {
            get { return _CodeFieldName3; }
            set { _CodeFieldName3 = value; }
        }
        [Column(Name = "DisplayFieldName3", DataType = "String")]
        public String DisplayFieldName3
        {
            get { return _DisplayFieldName3; }
            set { _DisplayFieldName3 = value; }
        }
        [Column(Name = "SearchDialogTypeName3", DataType = "String")]
        public String SearchDialogTypeName3
        {
            get { return _SearchDialogTypeName3; }
            set { _SearchDialogTypeName3 = value; }
        }
        [Column(Name = "SubLedger4", DataType = "Int32")]
        public Int32 SubLedger4
        {
            get { return _SubLedger4; }
            set { _SubLedger4 = value; }
        }
        [Column(Name = "SubLedger4Name", DataType = "String")]
        public String SubLedger4Name
        {
            get { return _SubLedger4Name; }
            set { _SubLedger4Name = value; }
        }
        [Column(Name = "SubLedger4Code", DataType = "String")]
        public String SubLedger4Code
        {
            get { return _SubLedger4Code; }
            set { _SubLedger4Code = value; }
        }
        [Column(Name = "SubLedgerID4", DataType = "Int32")]
        public Int32 SubLedgerID4
        {
            get { return _SubLedgerID4; }
            set { _SubLedgerID4 = value; }
        }
        [Column(Name = "SubLedgerTypeID4", DataType = "Int32")]
        public Int32 SubLedgerTypeID4
        {
            get { return _SubLedgerTypeID4; }
            set { _SubLedgerTypeID4 = value; }
        }
        [Column(Name = "MethodName4", DataType = "String")]
        public String MethodName4
        {
            get { return _MethodName4; }
            set { _MethodName4 = value; }
        }
        [Column(Name = "FilterExpression4", DataType = "String")]
        public String FilterExpression4
        {
            get { return _FilterExpression4; }
            set { _FilterExpression4 = value; }
        }
        [Column(Name = "IDFieldName4", DataType = "String")]
        public String IDFieldName4
        {
            get { return _IDFieldName4; }
            set { _IDFieldName4 = value; }
        }
        [Column(Name = "CodeFieldName4", DataType = "String")]
        public String CodeFieldName4
        {
            get { return _CodeFieldName4; }
            set { _CodeFieldName4 = value; }
        }
        [Column(Name = "DisplayFieldName4", DataType = "String")]
        public String DisplayFieldName4
        {
            get { return _DisplayFieldName4; }
            set { _DisplayFieldName4 = value; }
        }
        [Column(Name = "SearchDialogTypeName4", DataType = "String")]
        public String SearchDialogTypeName4
        {
            get { return _SearchDialogTypeName4; }
            set { _SearchDialogTypeName4 = value; }
        }
        [Column(Name = "SubLedger5", DataType = "Int32")]
        public Int32 SubLedger5
        {
            get { return _SubLedger5; }
            set { _SubLedger5 = value; }
        }
        [Column(Name = "SubLedger5Name", DataType = "String")]
        public String SubLedger5Name
        {
            get { return _SubLedger5Name; }
            set { _SubLedger5Name = value; }
        }
        [Column(Name = "SubLedger5Code", DataType = "String")]
        public String SubLedger5Code
        {
            get { return _SubLedger5Code; }
            set { _SubLedger5Code = value; }
        }
        [Column(Name = "SubLedgerID5", DataType = "Int32")]
        public Int32 SubLedgerID5
        {
            get { return _SubLedgerID5; }
            set { _SubLedgerID5 = value; }
        }
        [Column(Name = "SubLedgerTypeID5", DataType = "Int32")]
        public Int32 SubLedgerTypeID5
        {
            get { return _SubLedgerTypeID5; }
            set { _SubLedgerTypeID5 = value; }
        }
        [Column(Name = "MethodName5", DataType = "String")]
        public String MethodName5
        {
            get { return _MethodName5; }
            set { _MethodName5 = value; }
        }
        [Column(Name = "FilterExpression5", DataType = "String")]
        public String FilterExpression5
        {
            get { return _FilterExpression5; }
            set { _FilterExpression5 = value; }
        }
        [Column(Name = "IDFieldName5", DataType = "String")]
        public String IDFieldName5
        {
            get { return _IDFieldName5; }
            set { _IDFieldName5 = value; }
        }
        [Column(Name = "CodeFieldName5", DataType = "String")]
        public String CodeFieldName5
        {
            get { return _CodeFieldName5; }
            set { _CodeFieldName5 = value; }
        }
        [Column(Name = "DisplayFieldName5", DataType = "String")]
        public String DisplayFieldName5
        {
            get { return _DisplayFieldName5; }
            set { _DisplayFieldName5 = value; }
        }
        [Column(Name = "SearchDialogTypeName5", DataType = "String")]
        public String SearchDialogTypeName5
        {
            get { return _SearchDialogTypeName5; }
            set { _SearchDialogTypeName5 = value; }
        }
        [Column(Name = "SubLedger6", DataType = "Int32")]
        public Int32 SubLedger6
        {
            get { return _SubLedger6; }
            set { _SubLedger6 = value; }
        }
        [Column(Name = "SubLedger6Name", DataType = "String")]
        public String SubLedger6Name
        {
            get { return _SubLedger6Name; }
            set { _SubLedger6Name = value; }
        }
        [Column(Name = "SubLedger6Code", DataType = "String")]
        public String SubLedger6Code
        {
            get { return _SubLedger6Code; }
            set { _SubLedger6Code = value; }
        }
        [Column(Name = "SubLedgerID6", DataType = "Int32")]
        public Int32 SubLedgerID6
        {
            get { return _SubLedgerID6; }
            set { _SubLedgerID6 = value; }
        }
        [Column(Name = "SubLedgerTypeID6", DataType = "Int32")]
        public Int32 SubLedgerTypeID6
        {
            get { return _SubLedgerTypeID6; }
            set { _SubLedgerTypeID6 = value; }
        }
        [Column(Name = "MethodName6", DataType = "String")]
        public String MethodName6
        {
            get { return _MethodName6; }
            set { _MethodName6 = value; }
        }
        [Column(Name = "FilterExpression6", DataType = "String")]
        public String FilterExpression6
        {
            get { return _FilterExpression6; }
            set { _FilterExpression6 = value; }
        }
        [Column(Name = "IDFieldName6", DataType = "String")]
        public String IDFieldName6
        {
            get { return _IDFieldName6; }
            set { _IDFieldName6 = value; }
        }
        [Column(Name = "CodeFieldName6", DataType = "String")]
        public String CodeFieldName6
        {
            get { return _CodeFieldName6; }
            set { _CodeFieldName6 = value; }
        }
        [Column(Name = "DisplayFieldName6", DataType = "String")]
        public String DisplayFieldName6
        {
            get { return _DisplayFieldName6; }
            set { _DisplayFieldName6 = value; }
        }
        [Column(Name = "SearchDialogTypeName6", DataType = "String")]
        public String SearchDialogTypeName6
        {
            get { return _SearchDialogTypeName6; }
            set { _SearchDialogTypeName6 = value; }
        }
    }
    #endregion
    #region vFAItem
    [Serializable]
    [Table(Name = "vFAItem")]
    public partial class vFAItem
    {
        private Int32 _FixedAssetID;
        private String _FixedAssetCode;
        private String _FixedAssetName;
        private String _SiteID;
        private Int32 _FAGroupID;
        private String _FAGroupCode;
        private String _FAGroupName;
        private Int32 _FALocationID;
        private String _FALocationCode;
        private String _FALocationName;
        private Int32 _MethodID;
        private String _MethodCode;
        private String _MethodName;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private String _SerialNumber;
        private Boolean _IsContractItem;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private String _ContractNumber;
        private Int32 _PurchaseReceiveID;
        private String _ProcurementNumber;
        private DateTime _ProcurementDate;
        private Decimal _ProcurementAmount;
        private Decimal _ProcurementQuantity;
        private String _GCProcurementUnit;
        private String _ProcurementUnit;
        private DateTime _DepreciationStartDate;
        private Int16 _DepreciationLength;
        private Decimal _AssetFinalValue;
        private String _Remarks;
        private String _GCItemStatus;
        private Boolean _IsDeleted;

        [Column(Name = "FixedAssetID", DataType = "Int32")]
        public Int32 FixedAssetID
        {
            get { return _FixedAssetID; }
            set { _FixedAssetID = value; }
        }
        [Column(Name = "FixedAssetCode", DataType = "String")]
        public String FixedAssetCode
        {
            get { return _FixedAssetCode; }
            set { _FixedAssetCode = value; }
        }
        [Column(Name = "FixedAssetName", DataType = "String")]
        public String FixedAssetName
        {
            get { return _FixedAssetName; }
            set { _FixedAssetName = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "FAGroupID", DataType = "Int32")]
        public Int32 FAGroupID
        {
            get { return _FAGroupID; }
            set { _FAGroupID = value; }
        }
        [Column(Name = "FAGroupCode", DataType = "String")]
        public String FAGroupCode
        {
            get { return _FAGroupCode; }
            set { _FAGroupCode = value; }
        }
        [Column(Name = "FAGroupName", DataType = "String")]
        public String FAGroupName
        {
            get { return _FAGroupName; }
            set { _FAGroupName = value; }
        }
        [Column(Name = "FALocationID", DataType = "Int32")]
        public Int32 FALocationID
        {
            get { return _FALocationID; }
            set { _FALocationID = value; }
        }
        [Column(Name = "FALocationCode", DataType = "String")]
        public String FALocationCode
        {
            get { return _FALocationCode; }
            set { _FALocationCode = value; }
        }
        [Column(Name = "FALocationName", DataType = "String")]
        public String FALocationName
        {
            get { return _FALocationName; }
            set { _FALocationName = value; }
        }
        [Column(Name = "MethodID", DataType = "Int32")]
        public Int32 MethodID
        {
            get { return _MethodID; }
            set { _MethodID = value; }
        }
        [Column(Name = "MethodCode", DataType = "String")]
        public String MethodCode
        {
            get { return _MethodCode; }
            set { _MethodCode = value; }
        }
        [Column(Name = "MethodName", DataType = "String")]
        public String MethodName
        {
            get { return _MethodName; }
            set { _MethodName = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "SerialNumber", DataType = "String")]
        public String SerialNumber
        {
            get { return _SerialNumber; }
            set { _SerialNumber = value; }
        }
        [Column(Name = "IsContractItem", DataType = "Boolean")]
        public Boolean IsContractItem
        {
            get { return _IsContractItem; }
            set { _IsContractItem = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "ContractNumber", DataType = "String")]
        public String ContractNumber
        {
            get { return _ContractNumber; }
            set { _ContractNumber = value; }
        }
        [Column(Name = "PurchaseReceiveID", DataType = "Int32")]
        public Int32 PurchaseReceiveID
        {
            get { return _PurchaseReceiveID; }
            set { _PurchaseReceiveID = value; }
        }
        [Column(Name = "ProcurementNumber", DataType = "String")]
        public String ProcurementNumber
        {
            get { return _ProcurementNumber; }
            set { _ProcurementNumber = value; }
        }
        [Column(Name = "ProcurementDate", DataType = "DateTime")]
        public DateTime ProcurementDate
        {
            get { return _ProcurementDate; }
            set { _ProcurementDate = value; }
        }
        [Column(Name = "ProcurementAmount", DataType = "Decimal")]
        public Decimal ProcurementAmount
        {
            get { return _ProcurementAmount; }
            set { _ProcurementAmount = value; }
        }
        [Column(Name = "ProcurementQuantity", DataType = "Decimal")]
        public Decimal ProcurementQuantity
        {
            get { return _ProcurementQuantity; }
            set { _ProcurementQuantity = value; }
        }
        [Column(Name = "GCProcurementUnit", DataType = "String")]
        public String GCProcurementUnit
        {
            get { return _GCProcurementUnit; }
            set { _GCProcurementUnit = value; }
        }
        [Column(Name = "ProcurementUnit", DataType = "String")]
        public String ProcurementUnit
        {
            get { return _ProcurementUnit; }
            set { _ProcurementUnit = value; }
        }
        [Column(Name = "DepreciationStartDate", DataType = "DateTime")]
        public DateTime DepreciationStartDate
        {
            get { return _DepreciationStartDate; }
            set { _DepreciationStartDate = value; }
        }
        [Column(Name = "DepreciationLength", DataType = "Int16")]
        public Int16 DepreciationLength
        {
            get { return _DepreciationLength; }
            set { _DepreciationLength = value; }
        }
        [Column(Name = "AssetFinalValue", DataType = "Decimal")]
        public Decimal AssetFinalValue
        {
            get { return _AssetFinalValue; }
            set { _AssetFinalValue = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCItemStatus", DataType = "String")]
        public String GCItemStatus
        {
            get { return _GCItemStatus; }
            set { _GCItemStatus = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vFAItemCOA
    [Serializable]
    [Table(Name = "vFAItemCOA")]
    public class vFAItemCOA
    {
        private String _SiteID;
        private Int32 _FixedAssetID;
        private Int32 _GLAccount1;
        private String _GLAccount1No;
        private String _GLAccount1Name;
        private Int32 _SubLedgerID1;
        private String _SearchDialogTypeName1;
        private String _IDFieldName1;
        private String _CodeFieldName1;
        private String _DisplayFieldName1;
        private String _MethodName1;
        private String _FilterExpression1;
        private Int32 _GLAccount2;
        private String _GLAccount2No;
        private String _GLAccount2Name;
        private Int32 _SubLedgerID2;
        private String _SearchDialogTypeName2;
        private String _IDFieldName2;
        private String _CodeFieldName2;
        private String _DisplayFieldName2;
        private String _MethodName2;
        private String _FilterExpression2;
        private Int32 _GLAccount3;
        private String _GLAccount3No;
        private String _GLAccount3Name;
        private Int32 _SubLedgerID3;
        private String _SearchDialogTypeName3;
        private String _IDFieldName3;
        private String _CodeFieldName3;
        private String _DisplayFieldName3;
        private String _MethodName3;
        private String _FilterExpression3;
        private Int32 _GLAccount4;
        private String _GLAccount4No;
        private String _GLAccount4Name;
        private Int32 _SubLedgerID4;
        private String _SearchDialogTypeName4;
        private String _IDFieldName4;
        private String _CodeFieldName4;
        private String _DisplayFieldName4;
        private String _MethodName4;
        private String _FilterExpression4;
        private Int32 _GLAccount5;
        private String _GLAccount5No;
        private String _GLAccount5Name;
        private Int32 _SubLedgerID5;
        private String _SearchDialogTypeName5;
        private String _IDFieldName5;
        private String _CodeFieldName5;
        private String _DisplayFieldName5;
        private String _MethodName5;
        private String _FilterExpression5;
        private Int32 _GLAccount6;
        private String _GLAccount6No;
        private String _GLAccount6Name;
        private Int32 _SubLedgerID6;
        private String _SearchDialogTypeName6;
        private String _IDFieldName6;
        private String _CodeFieldName6;
        private String _DisplayFieldName6;
        private String _MethodName6;
        private String _FilterExpression6;
        private Int32 _SubLedger1;
        private String _SubLedger1Code;
        private String _SubLedger1Name;
        private Int32 _SubLedger2;
        private String _SubLedger2Code;
        private String _SubLedger2Name;
        private Int32 _SubLedger3;
        private String _SubLedger3Code;
        private String _SubLedger3Name;
        private Int32 _SubLedger4;
        private String _SubLedger4Code;
        private String _SubLedger4Name;
        private Int32 _SubLedger5;
        private String _SubLedger5Code;
        private String _SubLedger5Name;
        private Int32 _SubLedger6;
        private String _SubLedger6Code;
        private String _SubLedger6Name;

        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "FixedAssetID", DataType = "Int32")]
        public Int32 FixedAssetID
        {
            get { return _FixedAssetID; }
            set { _FixedAssetID = value; }
        }
        [Column(Name = "GLAccount1", DataType = "Int32")]
        public Int32 GLAccount1
        {
            get { return _GLAccount1; }
            set { _GLAccount1 = value; }
        }
        [Column(Name = "GLAccount1No", DataType = "String")]
        public String GLAccount1No
        {
            get { return _GLAccount1No; }
            set { _GLAccount1No = value; }
        }
        [Column(Name = "GLAccount1Name", DataType = "String")]
        public String GLAccount1Name
        {
            get { return _GLAccount1Name; }
            set { _GLAccount1Name = value; }
        }
        [Column(Name = "SubLedgerID1", DataType = "Int32")]
        public Int32 SubLedgerID1
        {
            get { return _SubLedgerID1; }
            set { _SubLedgerID1 = value; }
        }
        [Column(Name = "SearchDialogTypeName1", DataType = "String")]
        public String SearchDialogTypeName1
        {
            get { return _SearchDialogTypeName1; }
            set { _SearchDialogTypeName1 = value; }
        }
        [Column(Name = "IDFieldName1", DataType = "String")]
        public String IDFieldName1
        {
            get { return _IDFieldName1; }
            set { _IDFieldName1 = value; }
        }
        [Column(Name = "CodeFieldName1", DataType = "String")]
        public String CodeFieldName1
        {
            get { return _CodeFieldName1; }
            set { _CodeFieldName1 = value; }
        }
        [Column(Name = "DisplayFieldName1", DataType = "String")]
        public String DisplayFieldName1
        {
            get { return _DisplayFieldName1; }
            set { _DisplayFieldName1 = value; }
        }
        [Column(Name = "MethodName1", DataType = "String")]
        public String MethodName1
        {
            get { return _MethodName1; }
            set { _MethodName1 = value; }
        }
        [Column(Name = "FilterExpression1", DataType = "String")]
        public String FilterExpression1
        {
            get { return _FilterExpression1; }
            set { _FilterExpression1 = value; }
        }
        [Column(Name = "GLAccount2", DataType = "Int32")]
        public Int32 GLAccount2
        {
            get { return _GLAccount2; }
            set { _GLAccount2 = value; }
        }
        [Column(Name = "GLAccount2No", DataType = "String")]
        public String GLAccount2No
        {
            get { return _GLAccount2No; }
            set { _GLAccount2No = value; }
        }
        [Column(Name = "GLAccount2Name", DataType = "String")]
        public String GLAccount2Name
        {
            get { return _GLAccount2Name; }
            set { _GLAccount2Name = value; }
        }
        [Column(Name = "SubLedgerID2", DataType = "Int32")]
        public Int32 SubLedgerID2
        {
            get { return _SubLedgerID2; }
            set { _SubLedgerID2 = value; }
        }
        [Column(Name = "SearchDialogTypeName2", DataType = "String")]
        public String SearchDialogTypeName2
        {
            get { return _SearchDialogTypeName2; }
            set { _SearchDialogTypeName2 = value; }
        }
        [Column(Name = "IDFieldName2", DataType = "String")]
        public String IDFieldName2
        {
            get { return _IDFieldName2; }
            set { _IDFieldName2 = value; }
        }
        [Column(Name = "CodeFieldName2", DataType = "String")]
        public String CodeFieldName2
        {
            get { return _CodeFieldName2; }
            set { _CodeFieldName2 = value; }
        }
        [Column(Name = "DisplayFieldName2", DataType = "String")]
        public String DisplayFieldName2
        {
            get { return _DisplayFieldName2; }
            set { _DisplayFieldName2 = value; }
        }
        [Column(Name = "MethodName2", DataType = "String")]
        public String MethodName2
        {
            get { return _MethodName2; }
            set { _MethodName2 = value; }
        }
        [Column(Name = "FilterExpression2", DataType = "String")]
        public String FilterExpression2
        {
            get { return _FilterExpression2; }
            set { _FilterExpression2 = value; }
        }
        [Column(Name = "GLAccount3", DataType = "Int32")]
        public Int32 GLAccount3
        {
            get { return _GLAccount3; }
            set { _GLAccount3 = value; }
        }
        [Column(Name = "GLAccount3No", DataType = "String")]
        public String GLAccount3No
        {
            get { return _GLAccount3No; }
            set { _GLAccount3No = value; }
        }
        [Column(Name = "GLAccount3Name", DataType = "String")]
        public String GLAccount3Name
        {
            get { return _GLAccount3Name; }
            set { _GLAccount3Name = value; }
        }
        [Column(Name = "SubLedgerID3", DataType = "Int32")]
        public Int32 SubLedgerID3
        {
            get { return _SubLedgerID3; }
            set { _SubLedgerID3 = value; }
        }
        [Column(Name = "SearchDialogTypeName3", DataType = "String")]
        public String SearchDialogTypeName3
        {
            get { return _SearchDialogTypeName3; }
            set { _SearchDialogTypeName3 = value; }
        }
        [Column(Name = "IDFieldName3", DataType = "String")]
        public String IDFieldName3
        {
            get { return _IDFieldName3; }
            set { _IDFieldName3 = value; }
        }
        [Column(Name = "CodeFieldName3", DataType = "String")]
        public String CodeFieldName3
        {
            get { return _CodeFieldName3; }
            set { _CodeFieldName3 = value; }
        }
        [Column(Name = "DisplayFieldName3", DataType = "String")]
        public String DisplayFieldName3
        {
            get { return _DisplayFieldName3; }
            set { _DisplayFieldName3 = value; }
        }
        [Column(Name = "MethodName3", DataType = "String")]
        public String MethodName3
        {
            get { return _MethodName3; }
            set { _MethodName3 = value; }
        }
        [Column(Name = "FilterExpression3", DataType = "String")]
        public String FilterExpression3
        {
            get { return _FilterExpression3; }
            set { _FilterExpression3 = value; }
        }
        [Column(Name = "GLAccount4", DataType = "Int32")]
        public Int32 GLAccount4
        {
            get { return _GLAccount4; }
            set { _GLAccount4 = value; }
        }
        [Column(Name = "GLAccount4No", DataType = "String")]
        public String GLAccount4No
        {
            get { return _GLAccount4No; }
            set { _GLAccount4No = value; }
        }
        [Column(Name = "GLAccount4Name", DataType = "String")]
        public String GLAccount4Name
        {
            get { return _GLAccount4Name; }
            set { _GLAccount4Name = value; }
        }
        [Column(Name = "SubLedgerID4", DataType = "Int32")]
        public Int32 SubLedgerID4
        {
            get { return _SubLedgerID4; }
            set { _SubLedgerID4 = value; }
        }
        [Column(Name = "SearchDialogTypeName4", DataType = "String")]
        public String SearchDialogTypeName4
        {
            get { return _SearchDialogTypeName4; }
            set { _SearchDialogTypeName4 = value; }
        }
        [Column(Name = "IDFieldName4", DataType = "String")]
        public String IDFieldName4
        {
            get { return _IDFieldName4; }
            set { _IDFieldName4 = value; }
        }
        [Column(Name = "CodeFieldName4", DataType = "String")]
        public String CodeFieldName4
        {
            get { return _CodeFieldName4; }
            set { _CodeFieldName4 = value; }
        }
        [Column(Name = "DisplayFieldName4", DataType = "String")]
        public String DisplayFieldName4
        {
            get { return _DisplayFieldName4; }
            set { _DisplayFieldName4 = value; }
        }
        [Column(Name = "MethodName4", DataType = "String")]
        public String MethodName4
        {
            get { return _MethodName4; }
            set { _MethodName4 = value; }
        }
        [Column(Name = "FilterExpression4", DataType = "String")]
        public String FilterExpression4
        {
            get { return _FilterExpression4; }
            set { _FilterExpression4 = value; }
        }
        [Column(Name = "GLAccount5", DataType = "Int32")]
        public Int32 GLAccount5
        {
            get { return _GLAccount5; }
            set { _GLAccount5 = value; }
        }
        [Column(Name = "GLAccount5No", DataType = "String")]
        public String GLAccount5No
        {
            get { return _GLAccount5No; }
            set { _GLAccount5No = value; }
        }
        [Column(Name = "GLAccount5Name", DataType = "String")]
        public String GLAccount5Name
        {
            get { return _GLAccount5Name; }
            set { _GLAccount5Name = value; }
        }
        [Column(Name = "SubLedgerID5", DataType = "Int32")]
        public Int32 SubLedgerID5
        {
            get { return _SubLedgerID5; }
            set { _SubLedgerID5 = value; }
        }
        [Column(Name = "SearchDialogTypeName5", DataType = "String")]
        public String SearchDialogTypeName5
        {
            get { return _SearchDialogTypeName5; }
            set { _SearchDialogTypeName5 = value; }
        }
        [Column(Name = "IDFieldName5", DataType = "String")]
        public String IDFieldName5
        {
            get { return _IDFieldName5; }
            set { _IDFieldName5 = value; }
        }
        [Column(Name = "CodeFieldName5", DataType = "String")]
        public String CodeFieldName5
        {
            get { return _CodeFieldName5; }
            set { _CodeFieldName5 = value; }
        }
        [Column(Name = "DisplayFieldName5", DataType = "String")]
        public String DisplayFieldName5
        {
            get { return _DisplayFieldName5; }
            set { _DisplayFieldName5 = value; }
        }
        [Column(Name = "MethodName5", DataType = "String")]
        public String MethodName5
        {
            get { return _MethodName5; }
            set { _MethodName5 = value; }
        }
        [Column(Name = "FilterExpression5", DataType = "String")]
        public String FilterExpression5
        {
            get { return _FilterExpression5; }
            set { _FilterExpression5 = value; }
        }
        [Column(Name = "GLAccount6", DataType = "Int32")]
        public Int32 GLAccount6
        {
            get { return _GLAccount6; }
            set { _GLAccount6 = value; }
        }
        [Column(Name = "GLAccount6No", DataType = "String")]
        public String GLAccount6No
        {
            get { return _GLAccount6No; }
            set { _GLAccount6No = value; }
        }
        [Column(Name = "GLAccount6Name", DataType = "String")]
        public String GLAccount6Name
        {
            get { return _GLAccount6Name; }
            set { _GLAccount6Name = value; }
        }
        [Column(Name = "SubLedgerID6", DataType = "Int32")]
        public Int32 SubLedgerID6
        {
            get { return _SubLedgerID6; }
            set { _SubLedgerID6 = value; }
        }
        [Column(Name = "SearchDialogTypeName6", DataType = "String")]
        public String SearchDialogTypeName6
        {
            get { return _SearchDialogTypeName6; }
            set { _SearchDialogTypeName6 = value; }
        }
        [Column(Name = "IDFieldName6", DataType = "String")]
        public String IDFieldName6
        {
            get { return _IDFieldName6; }
            set { _IDFieldName6 = value; }
        }
        [Column(Name = "CodeFieldName6", DataType = "String")]
        public String CodeFieldName6
        {
            get { return _CodeFieldName6; }
            set { _CodeFieldName6 = value; }
        }
        [Column(Name = "DisplayFieldName6", DataType = "String")]
        public String DisplayFieldName6
        {
            get { return _DisplayFieldName6; }
            set { _DisplayFieldName6 = value; }
        }
        [Column(Name = "MethodName6", DataType = "String")]
        public String MethodName6
        {
            get { return _MethodName6; }
            set { _MethodName6 = value; }
        }
        [Column(Name = "FilterExpression6", DataType = "String")]
        public String FilterExpression6
        {
            get { return _FilterExpression6; }
            set { _FilterExpression6 = value; }
        }
        [Column(Name = "SubLedger1", DataType = "Int32")]
        public Int32 SubLedger1
        {
            get { return _SubLedger1; }
            set { _SubLedger1 = value; }
        }
        [Column(Name = "SubLedger1Code", DataType = "String")]
        public String SubLedger1Code
        {
            get { return _SubLedger1Code; }
            set { _SubLedger1Code = value; }
        }
        [Column(Name = "SubLedger1Name", DataType = "String")]
        public String SubLedger1Name
        {
            get { return _SubLedger1Name; }
            set { _SubLedger1Name = value; }
        }
        [Column(Name = "SubLedger2", DataType = "Int32")]
        public Int32 SubLedger2
        {
            get { return _SubLedger2; }
            set { _SubLedger2 = value; }
        }
        [Column(Name = "SubLedger2Code", DataType = "String")]
        public String SubLedger2Code
        {
            get { return _SubLedger2Code; }
            set { _SubLedger2Code = value; }
        }
        [Column(Name = "SubLedger2Name", DataType = "String")]
        public String SubLedger2Name
        {
            get { return _SubLedger2Name; }
            set { _SubLedger2Name = value; }
        }
        [Column(Name = "SubLedger3", DataType = "Int32")]
        public Int32 SubLedger3
        {
            get { return _SubLedger3; }
            set { _SubLedger3 = value; }
        }
        [Column(Name = "SubLedger3Code", DataType = "String")]
        public String SubLedger3Code
        {
            get { return _SubLedger3Code; }
            set { _SubLedger3Code = value; }
        }
        [Column(Name = "SubLedger3Name", DataType = "String")]
        public String SubLedger3Name
        {
            get { return _SubLedger3Name; }
            set { _SubLedger3Name = value; }
        }
        [Column(Name = "SubLedger4", DataType = "Int32")]
        public Int32 SubLedger4
        {
            get { return _SubLedger4; }
            set { _SubLedger4 = value; }
        }
        [Column(Name = "SubLedger4Code", DataType = "String")]
        public String SubLedger4Code
        {
            get { return _SubLedger4Code; }
            set { _SubLedger4Code = value; }
        }
        [Column(Name = "SubLedger4Name", DataType = "String")]
        public String SubLedger4Name
        {
            get { return _SubLedger4Name; }
            set { _SubLedger4Name = value; }
        }
        [Column(Name = "SubLedger5", DataType = "Int32")]
        public Int32 SubLedger5
        {
            get { return _SubLedger5; }
            set { _SubLedger5 = value; }
        }
        [Column(Name = "SubLedger5Code", DataType = "String")]
        public String SubLedger5Code
        {
            get { return _SubLedger5Code; }
            set { _SubLedger5Code = value; }
        }
        [Column(Name = "SubLedger5Name", DataType = "String")]
        public String SubLedger5Name
        {
            get { return _SubLedger5Name; }
            set { _SubLedger5Name = value; }
        }
        [Column(Name = "SubLedger6", DataType = "Int32")]
        public Int32 SubLedger6
        {
            get { return _SubLedger6; }
            set { _SubLedger6 = value; }
        }
        [Column(Name = "SubLedger6Code", DataType = "String")]
        public String SubLedger6Code
        {
            get { return _SubLedger6Code; }
            set { _SubLedger6Code = value; }
        }
        [Column(Name = "SubLedger6Name", DataType = "String")]
        public String SubLedger6Name
        {
            get { return _SubLedger6Name; }
            set { _SubLedger6Name = value; }
        }
    }
    #endregion
    #region vFAItemMovement
    [Serializable]
    [Table(Name = "vFAItemMovement")]
    public partial class vFAItemMovement
    {
        private Int32 _MovementID;
        private String _MovementNo;
        private DateTime _MovementDate;
        private Int32 _FixedAssetID;
        private Int32 _FromFALocationID;
        private String _FromLocationCode;
        private String _FromLocationName;
        private Int32 _ToFALocationID;
        private String _ToLocationCode;
        private String _ToLocationName;
        private String _Remarks;
        private String _GCTransactionStatus;
        private Int32 _CreatedBy;
        private String _CreatedByName;
        private DateTime _CreatedDate;
        private String _LastUpdatedByName;
        private DateTime _LastUpdatedDate;

        [Column(Name = "MovementID", DataType = "Int32")]
        public Int32 MovementID
        {
            get { return _MovementID; }
            set { _MovementID = value; }
        }
        [Column(Name = "MovementNo", DataType = "String")]
        public String MovementNo
        {
            get { return _MovementNo; }
            set { _MovementNo = value; }
        }
        [Column(Name = "MovementDate", DataType = "DateTime")]
        public DateTime MovementDate
        {
            get { return _MovementDate; }
            set { _MovementDate = value; }
        }
        [Column(Name = "FixedAssetID", DataType = "Int32")]
        public Int32 FixedAssetID
        {
            get { return _FixedAssetID; }
            set { _FixedAssetID = value; }
        }
        [Column(Name = "FromFALocationID", DataType = "Int32")]
        public Int32 FromFALocationID
        {
            get { return _FromFALocationID; }
            set { _FromFALocationID = value; }
        }
        [Column(Name = "FromLocationCode", DataType = "String")]
        public String FromLocationCode
        {
            get { return _FromLocationCode; }
            set { _FromLocationCode = value; }
        }
        [Column(Name = "FromLocationName", DataType = "String")]
        public String FromLocationName
        {
            get { return _FromLocationName; }
            set { _FromLocationName = value; }
        }
        [Column(Name = "ToFALocationID", DataType = "Int32")]
        public Int32 ToFALocationID
        {
            get { return _ToFALocationID; }
            set { _ToFALocationID = value; }
        }
        [Column(Name = "ToLocationCode", DataType = "String")]
        public String ToLocationCode
        {
            get { return _ToLocationCode; }
            set { _ToLocationCode = value; }
        }
        [Column(Name = "ToLocationName", DataType = "String")]
        public String ToLocationName
        {
            get { return _ToLocationName; }
            set { _ToLocationName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedByName", DataType = "String")]
        public String CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedByName", DataType = "String")]
        public String LastUpdatedByName
        {
            get { return _LastUpdatedByName; }
            set { _LastUpdatedByName = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime")]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }
    #endregion
    #region vFAWriteOff
    [Serializable]
    [Table(Name = "vFAWriteOff")]
    public class vFAWriteOff
    {
        private Int32 _FAWriteOffID;
        private String _FAWriteOffNo;
        private DateTime _FAWriteOffDate;
        private Int32 _FixedAssetID;
        private String _FixedAssetCode;
        private String _FixedAssetName;
        private String _GCAssetWriteOffType;
        private String _AssetWriteOffType;
        private String _GCAssetSalesType;
        private String _AssetSalesType;
        private Decimal _AssetValue;
        private Decimal _WriteOffAmount;
        private String _Remarks;
        private String _GCTransactionStatus;

        [Column(Name = "FAWriteOffID", DataType = "Int32")]
        public Int32 FAWriteOffID
        {
            get { return _FAWriteOffID; }
            set { _FAWriteOffID = value; }
        }
        [Column(Name = "FAWriteOffNo", DataType = "String")]
        public String FAWriteOffNo
        {
            get { return _FAWriteOffNo; }
            set { _FAWriteOffNo = value; }
        }
        [Column(Name = "FAWriteOffDate", DataType = "DateTime")]
        public DateTime FAWriteOffDate
        {
            get { return _FAWriteOffDate; }
            set { _FAWriteOffDate = value; }
        }
        [Column(Name = "FixedAssetID", DataType = "Int32")]
        public Int32 FixedAssetID
        {
            get { return _FixedAssetID; }
            set { _FixedAssetID = value; }
        }
        [Column(Name = "FixedAssetCode", DataType = "String")]
        public String FixedAssetCode
        {
            get { return _FixedAssetCode; }
            set { _FixedAssetCode = value; }
        }
        [Column(Name = "FixedAssetName", DataType = "String")]
        public String FixedAssetName
        {
            get { return _FixedAssetName; }
            set { _FixedAssetName = value; }
        }
        [Column(Name = "GCAssetWriteOffType", DataType = "String")]
        public String GCAssetWriteOffType
        {
            get { return _GCAssetWriteOffType; }
            set { _GCAssetWriteOffType = value; }
        }
        [Column(Name = "AssetWriteOffType", DataType = "String")]
        public String AssetWriteOffType
        {
            get { return _AssetWriteOffType; }
            set { _AssetWriteOffType = value; }
        }

        [Column(Name = "GCAssetSalesType", DataType = "String")]
        public String GCAssetSalesType
        {
            get { return _GCAssetSalesType; }
            set { _GCAssetSalesType = value; }
        }
        [Column(Name = "AssetSalesType", DataType = "String")]
        public String AssetSalesType
        {
            get { return _AssetSalesType; }
            set { _AssetSalesType = value; }
        }

        [Column(Name = "AssetValue", DataType = "Decimal")]
        public Decimal AssetValue
        {
            get { return _AssetValue; }
            set { _AssetValue = value; }
        }
        [Column(Name = "WriteOffAmount", DataType = "Decimal")]
        public Decimal WriteOffAmount
        {
            get { return _WriteOffAmount; }
            set { _WriteOffAmount = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
    }
    #endregion
    #region vGLAccountPayable
    [Serializable]
    [Table(Name = "vGLAccountPayable")]
    public class vGLAccountPayable
    {
        private Int32 _ID;
        private String _GCAccountPayableType;
        private String _AccountPayableType;
        private String _GCItemType;
        private String _ItemType;
        private Int32 _GLAccount;
        private String _GLAccountNo;
        private String _GLAccountName;
        private Int32 _SubLedgerID;
        private String _SearchDialogTypeName;
        private String _IDFieldName;
        private String _CodeFieldName;
        private String _DisplayFieldName;
        private String _MethodName;
        private String _FilterExpression;
        private Int32 _SubLedger;
        private String _SubLedgerCode;
        private String _SubLedgerName;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "GCAccountPayableType", DataType = "String")]
        public String GCAccountPayableType
        {
            get { return _GCAccountPayableType; }
            set { _GCAccountPayableType = value; }
        }
        [Column(Name = "AccountPayableType", DataType = "String")]
        public String AccountPayableType
        {
            get { return _AccountPayableType; }
            set { _AccountPayableType = value; }
        }
        [Column(Name = "GCItemType", DataType = "String")]
        public String GCItemType
        {
            get { return _GCItemType; }
            set { _GCItemType = value; }
        }
        [Column(Name = "ItemType", DataType = "String")]
        public String ItemType
        {
            get { return _ItemType; }
            set { _ItemType = value; }
        }
        [Column(Name = "GLAccount", DataType = "Int32")]
        public Int32 GLAccount
        {
            get { return _GLAccount; }
            set { _GLAccount = value; }
        }
        [Column(Name = "GLAccountNo", DataType = "String")]
        public String GLAccountNo
        {
            get { return _GLAccountNo; }
            set { _GLAccountNo = value; }
        }
        [Column(Name = "GLAccountName", DataType = "String")]
        public String GLAccountName
        {
            get { return _GLAccountName; }
            set { _GLAccountName = value; }
        }
        [Column(Name = "SubLedgerID", DataType = "Int32")]
        public Int32 SubLedgerID
        {
            get { return _SubLedgerID; }
            set { _SubLedgerID = value; }
        }
        [Column(Name = "SearchDialogTypeName", DataType = "String")]
        public String SearchDialogTypeName
        {
            get { return _SearchDialogTypeName; }
            set { _SearchDialogTypeName = value; }
        }
        [Column(Name = "IDFieldName", DataType = "String")]
        public String IDFieldName
        {
            get { return _IDFieldName; }
            set { _IDFieldName = value; }
        }
        [Column(Name = "CodeFieldName", DataType = "String")]
        public String CodeFieldName
        {
            get { return _CodeFieldName; }
            set { _CodeFieldName = value; }
        }
        [Column(Name = "DisplayFieldName", DataType = "String")]
        public String DisplayFieldName
        {
            get { return _DisplayFieldName; }
            set { _DisplayFieldName = value; }
        }
        [Column(Name = "MethodName", DataType = "String")]
        public String MethodName
        {
            get { return _MethodName; }
            set { _MethodName = value; }
        }
        [Column(Name = "FilterExpression", DataType = "String")]
        public String FilterExpression
        {
            get { return _FilterExpression; }
            set { _FilterExpression = value; }
        }
        [Column(Name = "SubLedger", DataType = "Int32")]
        public Int32 SubLedger
        {
            get { return _SubLedger; }
            set { _SubLedger = value; }
        }
        [Column(Name = "SubLedgerCode", DataType = "String")]
        public String SubLedgerCode
        {
            get { return _SubLedgerCode; }
            set { _SubLedgerCode = value; }
        }
        [Column(Name = "SubLedgerName", DataType = "String")]
        public String SubLedgerName
        {
            get { return _SubLedgerName; }
            set { _SubLedgerName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vGLAPPayment
    [Serializable]
    [Table(Name = "vGLAPPayment")]
    public class vGLAPPayment
    {
        private Int32 _ID;
        private String _GCSupplierPaymentMethod;
        private String _SupplierPaymentMethod;
        private Int32 _BankID;
        private String _BankCode;
        private String _BankName;
        private Int32 _GLAccount;
        private String _GLAccountNo;
        private String _GLAccountName;
        private Int32 _SubLedgerID;
        private String _SearchDialogTypeName;
        private String _IDFieldName;
        private String _CodeFieldName;
        private String _DisplayFieldName;
        private String _MethodName;
        private String _FilterExpression;
        private Int32 _SubLedger;
        private String _SubLedgerCode;
        private String _SubLedgerName;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "GCSupplierPaymentMethod", DataType = "String")]
        public String GCSupplierPaymentMethod
        {
            get { return _GCSupplierPaymentMethod; }
            set { _GCSupplierPaymentMethod = value; }
        }
        [Column(Name = "SupplierPaymentMethod", DataType = "String")]
        public String SupplierPaymentMethod
        {
            get { return _SupplierPaymentMethod; }
            set { _SupplierPaymentMethod = value; }
        }
        [Column(Name = "BankID", DataType = "Int32")]
        public Int32 BankID
        {
            get { return _BankID; }
            set { _BankID = value; }
        }
        [Column(Name = "BankCode", DataType = "String")]
        public String BankCode
        {
            get { return _BankCode; }
            set { _BankCode = value; }
        }
        [Column(Name = "BankName", DataType = "String")]
        public String BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }
        [Column(Name = "GLAccount", DataType = "Int32")]
        public Int32 GLAccount
        {
            get { return _GLAccount; }
            set { _GLAccount = value; }
        }
        [Column(Name = "GLAccountNo", DataType = "String")]
        public String GLAccountNo
        {
            get { return _GLAccountNo; }
            set { _GLAccountNo = value; }
        }
        [Column(Name = "GLAccountName", DataType = "String")]
        public String GLAccountName
        {
            get { return _GLAccountName; }
            set { _GLAccountName = value; }
        }
        [Column(Name = "SubLedgerID", DataType = "Int32")]
        public Int32 SubLedgerID
        {
            get { return _SubLedgerID; }
            set { _SubLedgerID = value; }
        }
        [Column(Name = "SearchDialogTypeName", DataType = "String")]
        public String SearchDialogTypeName
        {
            get { return _SearchDialogTypeName; }
            set { _SearchDialogTypeName = value; }
        }
        [Column(Name = "IDFieldName", DataType = "String")]
        public String IDFieldName
        {
            get { return _IDFieldName; }
            set { _IDFieldName = value; }
        }
        [Column(Name = "CodeFieldName", DataType = "String")]
        public String CodeFieldName
        {
            get { return _CodeFieldName; }
            set { _CodeFieldName = value; }
        }
        [Column(Name = "DisplayFieldName", DataType = "String")]
        public String DisplayFieldName
        {
            get { return _DisplayFieldName; }
            set { _DisplayFieldName = value; }
        }
        [Column(Name = "MethodName", DataType = "String")]
        public String MethodName
        {
            get { return _MethodName; }
            set { _MethodName = value; }
        }
        [Column(Name = "FilterExpression", DataType = "String")]
        public String FilterExpression
        {
            get { return _FilterExpression; }
            set { _FilterExpression = value; }
        }
        [Column(Name = "SubLedger", DataType = "Int32")]
        public Int32 SubLedger
        {
            get { return _SubLedger; }
            set { _SubLedger = value; }
        }
        [Column(Name = "SubLedgerCode", DataType = "String")]
        public String SubLedgerCode
        {
            get { return _SubLedgerCode; }
            set { _SubLedgerCode = value; }
        }
        [Column(Name = "SubLedgerName", DataType = "String")]
        public String SubLedgerName
        {
            get { return _SubLedgerName; }
            set { _SubLedgerName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vGLBalanceDtDocument
    [Serializable]
    [Table(Name = "vGLBalanceDtDocument")]
    public class vGLBalanceDtDocument
    {
        private Int32 _ID;
        private Int32 _GLAccount;
        private String _GLAccountNo;
        private String _GLAccountName;
        private Int32 _SubLedger;
        private String _SubLedgerCode;
        private String _SubLedgerName;
        private String _ReferenceNo;
        private Decimal _BalanceBEGIN;
        private Decimal _BalanceDEBIT;
        private Decimal _BalanceCREDIT;
        private Decimal _BalanceEND;
        private Boolean _IsDeleted;
        private Int32 _CreatedBy;
        private String _CreatedByName;
        private DateTime _CreatedDate;
        private Int32 _LastUpdatedBy;
        private String _LastUpdatedByName;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "GLAccount", DataType = "Int32")]
        public Int32 GLAccount
        {
            get { return _GLAccount; }
            set { _GLAccount = value; }
        }
        [Column(Name = "GLAccountNo", DataType = "String")]
        public String GLAccountNo
        {
            get { return _GLAccountNo; }
            set { _GLAccountNo = value; }
        }
        [Column(Name = "GLAccountName", DataType = "String")]
        public String GLAccountName
        {
            get { return _GLAccountName; }
            set { _GLAccountName = value; }
        }
        [Column(Name = "SubLedger", DataType = "Int32")]
        public Int32 SubLedger
        {
            get { return _SubLedger; }
            set { _SubLedger = value; }
        }
        [Column(Name = "SubLedgerCode", DataType = "String")]
        public String SubLedgerCode
        {
            get { return _SubLedgerCode; }
            set { _SubLedgerCode = value; }
        }
        [Column(Name = "SubLedgerName", DataType = "String")]
        public String SubLedgerName
        {
            get { return _SubLedgerName; }
            set { _SubLedgerName = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String")]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "BalanceBEGIN", DataType = "Decimal")]
        public Decimal BalanceBEGIN
        {
            get { return _BalanceBEGIN; }
            set { _BalanceBEGIN = value; }
        }
        [Column(Name = "BalanceDEBIT", DataType = "Decimal")]
        public Decimal BalanceDEBIT
        {
            get { return _BalanceDEBIT; }
            set { _BalanceDEBIT = value; }
        }
        [Column(Name = "BalanceCREDIT", DataType = "Decimal")]
        public Decimal BalanceCREDIT
        {
            get { return _BalanceCREDIT; }
            set { _BalanceCREDIT = value; }
        }
        [Column(Name = "BalanceEND", DataType = "Decimal")]
        public Decimal BalanceEND
        {
            get { return _BalanceEND; }
            set { _BalanceEND = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedByName", DataType = "String")]
        public String CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32")]
        public Int32 LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedByName", DataType = "String")]
        public String LastUpdatedByName
        {
            get { return _LastUpdatedByName; }
            set { _LastUpdatedByName = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime")]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }
    #endregion
    #region vGLBalancePerPeriodNo
    [Serializable]
    [Table(Name = "vGLBalancePerPeriodNo")]
    public class vGLBalancePerPeriodNo
    {
        private String _PeriodNo;
        private Decimal _BalanceBEGIN;
        private Decimal _BalanceDEBIT;
        private Decimal _BalanceCREDIT;
        private Decimal _BalanceEND;

        [Column(Name = "PeriodNo", DataType = "String")]
        public String PeriodNo
        {
            get { return _PeriodNo; }
            set { _PeriodNo = value; }
        }
        [Column(Name = "BalanceBEGIN", DataType = "Decimal")]
        public Decimal BalanceBEGIN
        {
            get { return _BalanceBEGIN; }
            set { _BalanceBEGIN = value; }
        }
        [Column(Name = "BalanceDEBIT", DataType = "Decimal")]
        public Decimal BalanceDEBIT
        {
            get { return _BalanceDEBIT; }
            set { _BalanceDEBIT = value; }
        }
        [Column(Name = "BalanceCREDIT", DataType = "Decimal")]
        public Decimal BalanceCREDIT
        {
            get { return _BalanceCREDIT; }
            set { _BalanceCREDIT = value; }
        }
        [Column(Name = "BalanceEND", DataType = "Decimal")]
        public Decimal BalanceEND
        {
            get { return _BalanceEND; }
            set { _BalanceEND = value; }
        }
    }
    #endregion
    #region vGLFAWriteOffAccount
    [Serializable]
    [Table(Name = "vGLFAWriteOffAccount")]
    public class vGLFAWriteOffAccount
    {
        private Int32 _ID;
        private Int32 _FAGroupID;
        private String _FAGroupCode;
        private String _FAGroupName;
        private String _GCWriteOffType;
        private String _WriteOffType;
        private String _GCAssetSalesType;
        private String _AssetSalesType;
        private Int32 _BankID;
        private String _BankCode;
        private String _BankName;
        private Int32 _GLAccount;
        private String _GLAccountNo;
        private String _GLAccountName;
        private Int32 _SubLedgerID;
        private String _SearchDialogTypeName;
        private String _IDFieldName;
        private String _CodeFieldName;
        private String _DisplayFieldName;
        private String _MethodName;
        private String _FilterExpression;
        private Int32 _SubLedger;
        private String _SubLedgerCode;
        private String _SubLedgerName;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "FAGroupID", DataType = "Int32")]
        public Int32 FAGroupID
        {
            get { return _FAGroupID; }
            set { _FAGroupID = value; }
        }
        [Column(Name = "FAGroupCode", DataType = "String")]
        public String FAGroupCode
        {
            get { return _FAGroupCode; }
            set { _FAGroupCode = value; }
        }
        [Column(Name = "FAGroupName", DataType = "String")]
        public String FAGroupName
        {
            get { return _FAGroupName; }
            set { _FAGroupName = value; }
        }
        [Column(Name = "GCWriteOffType", DataType = "String")]
        public String GCWriteOffType
        {
            get { return _GCWriteOffType; }
            set { _GCWriteOffType = value; }
        }
        [Column(Name = "WriteOffType", DataType = "String")]
        public String WriteOffType
        {
            get { return _WriteOffType; }
            set { _WriteOffType = value; }
        }
        [Column(Name = "GCAssetSalesType", DataType = "String")]
        public String GCAssetSalesType
        {
            get { return _GCAssetSalesType; }
            set { _GCAssetSalesType = value; }
        }
        [Column(Name = "AssetSalesType", DataType = "String")]
        public String AssetSalesType
        {
            get { return _AssetSalesType; }
            set { _AssetSalesType = value; }
        }
        [Column(Name = "BankID", DataType = "Int32")]
        public Int32 BankID
        {
            get { return _BankID; }
            set { _BankID = value; }
        }
        [Column(Name = "BankCode", DataType = "String")]
        public String BankCode
        {
            get { return _BankCode; }
            set { _BankCode = value; }
        }
        [Column(Name = "BankName", DataType = "String")]
        public String BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }
        [Column(Name = "GLAccount", DataType = "Int32")]
        public Int32 GLAccount
        {
            get { return _GLAccount; }
            set { _GLAccount = value; }
        }
        [Column(Name = "GLAccountNo", DataType = "String")]
        public String GLAccountNo
        {
            get { return _GLAccountNo; }
            set { _GLAccountNo = value; }
        }
        [Column(Name = "GLAccountName", DataType = "String")]
        public String GLAccountName
        {
            get { return _GLAccountName; }
            set { _GLAccountName = value; }
        }
        [Column(Name = "SubLedgerID", DataType = "Int32")]
        public Int32 SubLedgerID
        {
            get { return _SubLedgerID; }
            set { _SubLedgerID = value; }
        }
        [Column(Name = "SearchDialogTypeName", DataType = "String")]
        public String SearchDialogTypeName
        {
            get { return _SearchDialogTypeName; }
            set { _SearchDialogTypeName = value; }
        }
        [Column(Name = "IDFieldName", DataType = "String")]
        public String IDFieldName
        {
            get { return _IDFieldName; }
            set { _IDFieldName = value; }
        }
        [Column(Name = "CodeFieldName", DataType = "String")]
        public String CodeFieldName
        {
            get { return _CodeFieldName; }
            set { _CodeFieldName = value; }
        }
        [Column(Name = "DisplayFieldName", DataType = "String")]
        public String DisplayFieldName
        {
            get { return _DisplayFieldName; }
            set { _DisplayFieldName = value; }
        }
        [Column(Name = "MethodName", DataType = "String")]
        public String MethodName
        {
            get { return _MethodName; }
            set { _MethodName = value; }
        }
        [Column(Name = "FilterExpression", DataType = "String")]
        public String FilterExpression
        {
            get { return _FilterExpression; }
            set { _FilterExpression = value; }
        }
        [Column(Name = "SubLedger", DataType = "Int32")]
        public Int32 SubLedger
        {
            get { return _SubLedger; }
            set { _SubLedger = value; }
        }
        [Column(Name = "SubLedgerCode", DataType = "String")]
        public String SubLedgerCode
        {
            get { return _SubLedgerCode; }
            set { _SubLedgerCode = value; }
        }
        [Column(Name = "SubLedgerName", DataType = "String")]
        public String SubLedgerName
        {
            get { return _SubLedgerName; }
            set { _SubLedgerName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vGLSetting
    [Serializable]
    [Table(Name = "vGLSetting")]
    public class vGLSetting
    {
        private String _SiteID;
        private String _GLSettingCode;
        private String _GLSettingName;
        private Int32 _GLAccount;
        private String _GLAccountNo;
        private String _GLAccountName;
        private Int32 _SubLedgerID;
        private String _SearchDialogTypeName;
        private String _IDFieldName;
        private String _CodeFieldName;
        private String _DisplayFieldName;
        private String _MethodName;
        private String _FilterExpression;
        private Int32 _SubLedger;
        private String _SubLedgerCode;
        private String _SubLedgerName;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "GLSettingCode", DataType = "String")]
        public String GLSettingCode
        {
            get { return _GLSettingCode; }
            set { _GLSettingCode = value; }
        }
        [Column(Name = "GLSettingName", DataType = "String")]
        public String GLSettingName
        {
            get { return _GLSettingName; }
            set { _GLSettingName = value; }
        }
        [Column(Name = "GLAccount", DataType = "Int32")]
        public Int32 GLAccount
        {
            get { return _GLAccount; }
            set { _GLAccount = value; }
        }
        [Column(Name = "GLAccountNo", DataType = "String")]
        public String GLAccountNo
        {
            get { return _GLAccountNo; }
            set { _GLAccountNo = value; }
        }
        [Column(Name = "GLAccountName", DataType = "String")]
        public String GLAccountName
        {
            get { return _GLAccountName; }
            set { _GLAccountName = value; }
        }
        [Column(Name = "SubLedgerID", DataType = "Int32")]
        public Int32 SubLedgerID
        {
            get { return _SubLedgerID; }
            set { _SubLedgerID = value; }
        }
        [Column(Name = "SearchDialogTypeName", DataType = "String")]
        public String SearchDialogTypeName
        {
            get { return _SearchDialogTypeName; }
            set { _SearchDialogTypeName = value; }
        }
        [Column(Name = "IDFieldName", DataType = "String")]
        public String IDFieldName
        {
            get { return _IDFieldName; }
            set { _IDFieldName = value; }
        }
        [Column(Name = "CodeFieldName", DataType = "String")]
        public String CodeFieldName
        {
            get { return _CodeFieldName; }
            set { _CodeFieldName = value; }
        }
        [Column(Name = "DisplayFieldName", DataType = "String")]
        public String DisplayFieldName
        {
            get { return _DisplayFieldName; }
            set { _DisplayFieldName = value; }
        }
        [Column(Name = "MethodName", DataType = "String")]
        public String MethodName
        {
            get { return _MethodName; }
            set { _MethodName = value; }
        }
        [Column(Name = "FilterExpression", DataType = "String")]
        public String FilterExpression
        {
            get { return _FilterExpression; }
            set { _FilterExpression = value; }
        }
        [Column(Name = "SubLedger", DataType = "Int32")]
        public Int32 SubLedger
        {
            get { return _SubLedger; }
            set { _SubLedger = value; }
        }
        [Column(Name = "SubLedgerCode", DataType = "String")]
        public String SubLedgerCode
        {
            get { return _SubLedgerCode; }
            set { _SubLedgerCode = value; }
        }
        [Column(Name = "SubLedgerName", DataType = "String")]
        public String SubLedgerName
        {
            get { return _SubLedgerName; }
            set { _SubLedgerName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vGLTransactionDt
    [Serializable]
    [Table(Name = "vGLTransactionDt")]
    public partial class vGLTransactionDt
    {
        private Int32 _TransactionDtID;
        private Int32 _GLTransactionID;
        private String _GCJournalGroup;
        private String _TransactionCode;
        private String _TransactionName;
        private String _JournalNo;
        private DateTime _JournalDate;
        private Int32 _GLAccount;
        private String _GLAccountNo;
        private String _GLAccountName;
        private Int32 _SubLedgerID;
        private String _SearchDialogTypeName;
        private String _IDFieldName;
        private String _CodeFieldName;
        private String _DisplayFieldName;
        private String _MethodName;
        private String _FilterExpression;
        private Int32 _SubLedger;
        private String _SubLedgerCode;
        private String _SubLedgerName;
        private String _Position;
        private Decimal _DebitAmount;
        private Decimal _CreditAmount;
        private String _ReferenceNo;
        private Decimal _BalanceEND;
        private Int16 _DisplayOrder;
        private String _Remarks;
        private String _GCItemDetailStatus;
        private Boolean _IsDeleted;
        private Int32 _LastUpdatedBy;
        private String _LastUpdatedByUserName;
        private DateTime _LastUpdatedDate;

        [Column(Name = "TransactionDtID", DataType = "Int32")]
        public Int32 TransactionDtID
        {
            get { return _TransactionDtID; }
            set { _TransactionDtID = value; }
        }
        [Column(Name = "GLTransactionID", DataType = "Int32")]
        public Int32 GLTransactionID
        {
            get { return _GLTransactionID; }
            set { _GLTransactionID = value; }
        }
        [Column(Name = "GCJournalGroup", DataType = "String")]
        public String GCJournalGroup
        {
            get { return _GCJournalGroup; }
            set { _GCJournalGroup = value; }
        }
        [Column(Name = "TransactionCode", DataType = "String")]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
        [Column(Name = "TransactionName", DataType = "String")]
        public String TransactionName
        {
            get { return _TransactionName; }
            set { _TransactionName = value; }
        }
        [Column(Name = "JournalNo", DataType = "String")]
        public String JournalNo
        {
            get { return _JournalNo; }
            set { _JournalNo = value; }
        }
        [Column(Name = "JournalDate", DataType = "DateTime")]
        public DateTime JournalDate
        {
            get { return _JournalDate; }
            set { _JournalDate = value; }
        }
        [Column(Name = "GLAccount", DataType = "Int32")]
        public Int32 GLAccount
        {
            get { return _GLAccount; }
            set { _GLAccount = value; }
        }
        [Column(Name = "GLAccountNo", DataType = "String")]
        public String GLAccountNo
        {
            get { return _GLAccountNo; }
            set { _GLAccountNo = value; }
        }
        [Column(Name = "GLAccountName", DataType = "String")]
        public String GLAccountName
        {
            get { return _GLAccountName; }
            set { _GLAccountName = value; }
        }
        [Column(Name = "SubLedgerID", DataType = "Int32")]
        public Int32 SubLedgerID
        {
            get { return _SubLedgerID; }
            set { _SubLedgerID = value; }
        }
        [Column(Name = "SearchDialogTypeName", DataType = "String")]
        public String SearchDialogTypeName
        {
            get { return _SearchDialogTypeName; }
            set { _SearchDialogTypeName = value; }
        }
        [Column(Name = "IDFieldName", DataType = "String")]
        public String IDFieldName
        {
            get { return _IDFieldName; }
            set { _IDFieldName = value; }
        }
        [Column(Name = "CodeFieldName", DataType = "String")]
        public String CodeFieldName
        {
            get { return _CodeFieldName; }
            set { _CodeFieldName = value; }
        }
        [Column(Name = "DisplayFieldName", DataType = "String")]
        public String DisplayFieldName
        {
            get { return _DisplayFieldName; }
            set { _DisplayFieldName = value; }
        }
        [Column(Name = "MethodName", DataType = "String")]
        public String MethodName
        {
            get { return _MethodName; }
            set { _MethodName = value; }
        }
        [Column(Name = "FilterExpression", DataType = "String")]
        public String FilterExpression
        {
            get { return _FilterExpression; }
            set { _FilterExpression = value; }
        }
        [Column(Name = "SubLedger", DataType = "Int32")]
        public Int32 SubLedger
        {
            get { return _SubLedger; }
            set { _SubLedger = value; }
        }
        [Column(Name = "SubLedgerCode", DataType = "String")]
        public String SubLedgerCode
        {
            get { return _SubLedgerCode; }
            set { _SubLedgerCode = value; }
        }
        [Column(Name = "SubLedgerName", DataType = "String")]
        public String SubLedgerName
        {
            get { return _SubLedgerName; }
            set { _SubLedgerName = value; }
        }
        [Column(Name = "Position", DataType = "String")]
        public String Position
        {
            get { return _Position; }
            set { _Position = value; }
        }
        [Column(Name = "DebitAmount", DataType = "Decimal")]
        public Decimal DebitAmount
        {
            get { return _DebitAmount; }
            set { _DebitAmount = value; }
        }
        [Column(Name = "CreditAmount", DataType = "Decimal")]
        public Decimal CreditAmount
        {
            get { return _CreditAmount; }
            set { _CreditAmount = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String")]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "BalanceEND", DataType = "Decimal")]
        public Decimal BalanceEND
        {
            get { return _BalanceEND; }
            set { _BalanceEND = value; }
        }
        [Column(Name = "DisplayOrder", DataType = "Int16")]
        public Int16 DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32")]
        public Int32 LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedByUserName", DataType = "String")]
        public String LastUpdatedByUserName
        {
            get { return _LastUpdatedByUserName; }
            set { _LastUpdatedByUserName = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime")]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }
    #endregion
    #region vGLTransactionDtCustom
    [Serializable]
    [Table(Name = "vGLTransactionDtCustom")]
    public partial class vGLTransactionDtCustom
    {
        private Int32 _TransactionDtID;
        private Int32 _GLTransactionID;
        private DateTime _JournalDate;
        private String _TransactionCode;
        private String _JournalNo;
        private String _GLAccountNo;
        private String _GLAccountName;
        private String _SubLedgerCode;
        private String _SubLedgerName;
        private String _Position;
        private Decimal _DebitAmount;
        private Decimal _CreditAmount;
        private String _ReferenceNo;
        private Int16 _DisplayOrder;
        private String _Remarks;
        private String _GCItemDetailStatus;
        private String _ItemDetailStatus;
        private String _GCTransactionStatus;
        private String _TransactionStatus;
        private Boolean _IsDeleted;
        private Int32 _CreatedBy;
        private String _CreatedByName;
        private DateTime _CreatedDate;
        private Int32 _LastUpdatedBy;
        private String _LastUpdatedByName;
        private DateTime _LastUpdatedDate;

        [Column(Name = "TransactionDtID", DataType = "Int32")]
        public Int32 TransactionDtID
        {
            get { return _TransactionDtID; }
            set { _TransactionDtID = value; }
        }
        [Column(Name = "GLTransactionID", DataType = "Int32")]
        public Int32 GLTransactionID
        {
            get { return _GLTransactionID; }
            set { _GLTransactionID = value; }
        }
        [Column(Name = "TransactionCode", DataType = "String")]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
        [Column(Name = "JournalDate", DataType = "DateTime")]
        public DateTime JournalDate
        {
            get { return _JournalDate; }
            set { _JournalDate = value; }
        }
        [Column(Name = "JournalNo", DataType = "String")]
        public String JournalNo
        {
            get { return _JournalNo; }
            set { _JournalNo = value; }
        }
        [Column(Name = "GLAccountNo", DataType = "String")]
        public String GLAccountNo
        {
            get { return _GLAccountNo; }
            set { _GLAccountNo = value; }
        }
        [Column(Name = "GLAccountName", DataType = "String")]
        public String GLAccountName
        {
            get { return _GLAccountName; }
            set { _GLAccountName = value; }
        }
        [Column(Name = "SubLedgerCode", DataType = "String")]
        public String SubLedgerCode
        {
            get { return _SubLedgerCode; }
            set { _SubLedgerCode = value; }
        }
        [Column(Name = "SubLedgerName", DataType = "String")]
        public String SubLedgerName
        {
            get { return _SubLedgerName; }
            set { _SubLedgerName = value; }
        }
        [Column(Name = "Position", DataType = "String")]
        public String Position
        {
            get { return _Position; }
            set { _Position = value; }
        }
        [Column(Name = "DebitAmount", DataType = "Decimal")]
        public Decimal DebitAmount
        {
            get { return _DebitAmount; }
            set { _DebitAmount = value; }
        }
        [Column(Name = "CreditAmount", DataType = "Decimal")]
        public Decimal CreditAmount
        {
            get { return _CreditAmount; }
            set { _CreditAmount = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String")]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "DisplayOrder", DataType = "Int16")]
        public Int16 DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
        [Column(Name = "ItemDetailStatus", DataType = "String")]
        public String ItemDetailStatus
        {
            get { return _ItemDetailStatus; }
            set { _ItemDetailStatus = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatus", DataType = "String")]
        public String TransactionStatus
        {
            get { return _TransactionStatus; }
            set { _TransactionStatus = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedByName", DataType = "String")]
        public String CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32")]
        public Int32 LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedByName", DataType = "String")]
        public String LastUpdatedByName
        {
            get { return _LastUpdatedByName; }
            set { _LastUpdatedByName = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime")]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }
    #endregion
    #region vGLTransactionHd
    [Serializable]
    [Table(Name = "vGLTransactionHd")]
    public partial class vGLTransactionHd
    {
        private Int32 _GLTransactionID;
        private String _GCJournalGroup;
        private String _JournalGroup;
        private String _TransactionCode;
        private String _TransactionName;
        private String _JournalNo;
        private DateTime _JournalDate;
        private Decimal _DebitAmount;
        private Decimal _CreditAmount;
        private String _Remarks;
        private Boolean _IsGeneratedBySystem;
        private String _GCTransactionStatus;
        private String _TransactionStatus;
        private String _TransactionStatusWatermark;
        private String _GCVoidReason;
        private String _VoidReason;
        private Int32 _CreatedBy;
        private String _CreatedByName;
        private DateTime _CreatedDate;
        private Int32 _LastUpdatedBy;
        private String _LastUpdatedByName;
        private DateTime _LastUpdatedDate;

        [Column(Name = "GLTransactionID", DataType = "Int32")]
        public Int32 GLTransactionID
        {
            get { return _GLTransactionID; }
            set { _GLTransactionID = value; }
        }
        [Column(Name = "GCJournalGroup", DataType = "String")]
        public String GCJournalGroup
        {
            get { return _GCJournalGroup; }
            set { _GCJournalGroup = value; }
        }
        [Column(Name = "JournalGroup", DataType = "String")]
        public String JournalGroup
        {
            get { return _JournalGroup; }
            set { _JournalGroup = value; }
        }
        [Column(Name = "TransactionCode", DataType = "String")]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
        [Column(Name = "TransactionName", DataType = "String")]
        public String TransactionName
        {
            get { return _TransactionName; }
            set { _TransactionName = value; }
        }
        [Column(Name = "JournalNo", DataType = "String")]
        public String JournalNo
        {
            get { return _JournalNo; }
            set { _JournalNo = value; }
        }
        [Column(Name = "JournalDate", DataType = "DateTime")]
        public DateTime JournalDate
        {
            get { return _JournalDate; }
            set { _JournalDate = value; }
        }
        [Column(Name = "DebitAmount", DataType = "Decimal")]
        public Decimal DebitAmount
        {
            get { return _DebitAmount; }
            set { _DebitAmount = value; }
        }
        [Column(Name = "CreditAmount", DataType = "Decimal")]
        public Decimal CreditAmount
        {
            get { return _CreditAmount; }
            set { _CreditAmount = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsGeneratedBySystem", DataType = "Boolean")]
        public Boolean IsGeneratedBySystem
        {
            get { return _IsGeneratedBySystem; }
            set { _IsGeneratedBySystem = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatus", DataType = "String")]
        public String TransactionStatus
        {
            get { return _TransactionStatus; }
            set { _TransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
        [Column(Name = "GCVoidReason", DataType = "String")]
        public String GCVoidReason
        {
            get { return _GCVoidReason; }
            set { _GCVoidReason = value; }
        }
        [Column(Name = "VoidReason", DataType = "String")]
        public String VoidReason
        {
            get { return _VoidReason; }
            set { _VoidReason = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedByName", DataType = "String")]
        public String CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32")]
        public Int32 LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedByName", DataType = "String")]
        public String LastUpdatedByName
        {
            get { return _LastUpdatedByName; }
            set { _LastUpdatedByName = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime")]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }
    #endregion
    #region vGLWarehouseProductLineAccount
    [Serializable]
    [Table(Name = "vGLWarehouseProductLineAccount")]
    public class vGLWarehouseProductLineAccount
    {
        private Int32 _ID;
        private String _GCItemType;
        private String _ItemType;
        private Int32 _ProductLineID;
        private String _ProductLineCode;
        private String _ProductLineName;
        private Int32 _Inventory;
        private String _InventoryGLAccountNo;
        private String _InventoryGLAccountName;
        private Int32 _InventorySubLedgerID;
        private String _InventorySearchDialogTypeName;
        private String _InventoryIDFieldName;
        private String _InventoryCodeFieldName;
        private String _InventoryDisplayFieldName;
        private String _InventoryMethodName;
        private String _InventoryFilterExpression;
        private Int32 _InventorySubLedger;
        private String _InventorySubLedgerCode;
        private String _InventorySubLedgerName;
        private Int32 _InventoryVAT;
        private String _InventoryVATGLAccountNo;
        private String _InventoryVATGLAccountName;
        private Int32 _InventoryVATSubLedgerID;
        private String _InventoryVATSearchDialogTypeName;
        private String _InventoryVATIDFieldName;
        private String _InventoryVATCodeFieldName;
        private String _InventoryVATDisplayFieldName;
        private String _InventoryVATMethodName;
        private String _InventoryVATFilterExpression;
        private Int32 _InventoryVATSubLedger;
        private String _InventoryVATSubLedgerCode;
        private String _InventoryVATSubLedgerName;
        private Int32 _InventoryDiscount;
        private String _InventoryDiscountGLAccountNo;
        private String _InventoryDiscountGLAccountName;
        private Int32 _InventoryDiscountSubLedgerID;
        private String _InventoryDiscountSearchDialogTypeName;
        private String _InventoryDiscountIDFieldName;
        private String _InventoryDiscountCodeFieldName;
        private String _InventoryDiscountDisplayFieldName;
        private String _InventoryDiscountMethodName;
        private String _InventoryDiscountFilterExpression;
        private Int32 _InventoryDiscountSubLedger;
        private String _InventoryDiscountSubLedgerCode;
        private String _InventoryDiscountSubLedgerName;
        private Int32 _COGS;
        private String _COGSGLAccountNo;
        private String _COGSGLAccountName;
        private Int32 _COGSSubLedgerID;
        private String _COGSSearchDialogTypeName;
        private String _COGSIDFieldName;
        private String _COGSCodeFieldName;
        private String _COGSDisplayFieldName;
        private String _COGSMethodName;
        private String _COGSFilterExpression;
        private Int32 _COGSSubLedger;
        private String _COGSSubLedgerCode;
        private String _COGSSubLedgerName;
        private Int32 _Consumption;
        private String _ConsumptionGLAccountNo;
        private String _ConsumptionGLAccountName;
        private Int32 _ConsumptionSubLedgerID;
        private String _ConsumptionSearchDialogTypeName;
        private String _ConsumptionIDFieldName;
        private String _ConsumptionCodeFieldName;
        private String _ConsumptionDisplayFieldName;
        private String _ConsumptionMethodName;
        private String _ConsumptionFilterExpression;
        private Int32 _ConsumptionSubLedger;
        private String _ConsumptionSubLedgerCode;
        private String _ConsumptionSubLedgerName;
        private Int32 _PurchasePriceVariant;
        private String _PurchasePriceVariantGLAccountNo;
        private String _PurchasePriceVariantGLAccountName;
        private Int32 _PurchasePriceVariantSubLedgerID;
        private String _PurchasePriceVariantSearchDialogTypeName;
        private String _PurchasePriceVariantIDFieldName;
        private String _PurchasePriceVariantCodeFieldName;
        private String _PurchasePriceVariantDisplayFieldName;
        private String _PurchasePriceVariantMethodName;
        private String _PurchasePriceVariantFilterExpression;
        private Int32 _PurchasePriceVariantSubLedger;
        private String _PurchasePriceVariantSubLedgerCode;
        private String _PurchasePriceVariantSubLedgerName;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "GCItemType", DataType = "String")]
        public String GCItemType
        {
            get { return _GCItemType; }
            set { _GCItemType = value; }
        }
        [Column(Name = "ItemType", DataType = "String")]
        public String ItemType
        {
            get { return _ItemType; }
            set { _ItemType = value; }
        }
        [Column(Name = "ProductLineID", DataType = "Int32")]
        public Int32 ProductLineID
        {
            get { return _ProductLineID; }
            set { _ProductLineID = value; }
        }
        [Column(Name = "ProductLineCode", DataType = "String")]
        public String ProductLineCode
        {
            get { return _ProductLineCode; }
            set { _ProductLineCode = value; }
        }
        [Column(Name = "ProductLineName", DataType = "String")]
        public String ProductLineName
        {
            get { return _ProductLineName; }
            set { _ProductLineName = value; }
        }
        [Column(Name = "Inventory", DataType = "Int32")]
        public Int32 Inventory
        {
            get { return _Inventory; }
            set { _Inventory = value; }
        }
        [Column(Name = "InventoryGLAccountNo", DataType = "String")]
        public String InventoryGLAccountNo
        {
            get { return _InventoryGLAccountNo; }
            set { _InventoryGLAccountNo = value; }
        }
        [Column(Name = "InventoryGLAccountName", DataType = "String")]
        public String InventoryGLAccountName
        {
            get { return _InventoryGLAccountName; }
            set { _InventoryGLAccountName = value; }
        }
        [Column(Name = "InventorySubLedgerID", DataType = "Int32")]
        public Int32 InventorySubLedgerID
        {
            get { return _InventorySubLedgerID; }
            set { _InventorySubLedgerID = value; }
        }
        [Column(Name = "InventorySearchDialogTypeName", DataType = "String")]
        public String InventorySearchDialogTypeName
        {
            get { return _InventorySearchDialogTypeName; }
            set { _InventorySearchDialogTypeName = value; }
        }
        [Column(Name = "InventoryIDFieldName", DataType = "String")]
        public String InventoryIDFieldName
        {
            get { return _InventoryIDFieldName; }
            set { _InventoryIDFieldName = value; }
        }
        [Column(Name = "InventoryCodeFieldName", DataType = "String")]
        public String InventoryCodeFieldName
        {
            get { return _InventoryCodeFieldName; }
            set { _InventoryCodeFieldName = value; }
        }
        [Column(Name = "InventoryDisplayFieldName", DataType = "String")]
        public String InventoryDisplayFieldName
        {
            get { return _InventoryDisplayFieldName; }
            set { _InventoryDisplayFieldName = value; }
        }
        [Column(Name = "InventoryMethodName", DataType = "String")]
        public String InventoryMethodName
        {
            get { return _InventoryMethodName; }
            set { _InventoryMethodName = value; }
        }
        [Column(Name = "InventoryFilterExpression", DataType = "String")]
        public String InventoryFilterExpression
        {
            get { return _InventoryFilterExpression; }
            set { _InventoryFilterExpression = value; }
        }
        [Column(Name = "InventorySubLedger", DataType = "Int32")]
        public Int32 InventorySubLedger
        {
            get { return _InventorySubLedger; }
            set { _InventorySubLedger = value; }
        }
        [Column(Name = "InventorySubLedgerCode", DataType = "String")]
        public String InventorySubLedgerCode
        {
            get { return _InventorySubLedgerCode; }
            set { _InventorySubLedgerCode = value; }
        }
        [Column(Name = "InventorySubLedgerName", DataType = "String")]
        public String InventorySubLedgerName
        {
            get { return _InventorySubLedgerName; }
            set { _InventorySubLedgerName = value; }
        }
        [Column(Name = "InventoryVAT", DataType = "Int32")]
        public Int32 InventoryVAT
        {
            get { return _InventoryVAT; }
            set { _InventoryVAT = value; }
        }
        [Column(Name = "InventoryVATGLAccountNo", DataType = "String")]
        public String InventoryVATGLAccountNo
        {
            get { return _InventoryVATGLAccountNo; }
            set { _InventoryVATGLAccountNo = value; }
        }
        [Column(Name = "InventoryVATGLAccountName", DataType = "String")]
        public String InventoryVATGLAccountName
        {
            get { return _InventoryVATGLAccountName; }
            set { _InventoryVATGLAccountName = value; }
        }
        [Column(Name = "InventoryVATSubLedgerID", DataType = "Int32")]
        public Int32 InventoryVATSubLedgerID
        {
            get { return _InventoryVATSubLedgerID; }
            set { _InventoryVATSubLedgerID = value; }
        }
        [Column(Name = "InventoryVATSearchDialogTypeName", DataType = "String")]
        public String InventoryVATSearchDialogTypeName
        {
            get { return _InventoryVATSearchDialogTypeName; }
            set { _InventoryVATSearchDialogTypeName = value; }
        }
        [Column(Name = "InventoryVATIDFieldName", DataType = "String")]
        public String InventoryVATIDFieldName
        {
            get { return _InventoryVATIDFieldName; }
            set { _InventoryVATIDFieldName = value; }
        }
        [Column(Name = "InventoryVATCodeFieldName", DataType = "String")]
        public String InventoryVATCodeFieldName
        {
            get { return _InventoryVATCodeFieldName; }
            set { _InventoryVATCodeFieldName = value; }
        }
        [Column(Name = "InventoryVATDisplayFieldName", DataType = "String")]
        public String InventoryVATDisplayFieldName
        {
            get { return _InventoryVATDisplayFieldName; }
            set { _InventoryVATDisplayFieldName = value; }
        }
        [Column(Name = "InventoryVATMethodName", DataType = "String")]
        public String InventoryVATMethodName
        {
            get { return _InventoryVATMethodName; }
            set { _InventoryVATMethodName = value; }
        }
        [Column(Name = "InventoryVATFilterExpression", DataType = "String")]
        public String InventoryVATFilterExpression
        {
            get { return _InventoryVATFilterExpression; }
            set { _InventoryVATFilterExpression = value; }
        }
        [Column(Name = "InventoryVATSubLedger", DataType = "Int32")]
        public Int32 InventoryVATSubLedger
        {
            get { return _InventoryVATSubLedger; }
            set { _InventoryVATSubLedger = value; }
        }
        [Column(Name = "InventoryVATSubLedgerCode", DataType = "String")]
        public String InventoryVATSubLedgerCode
        {
            get { return _InventoryVATSubLedgerCode; }
            set { _InventoryVATSubLedgerCode = value; }
        }
        [Column(Name = "InventoryVATSubLedgerName", DataType = "String")]
        public String InventoryVATSubLedgerName
        {
            get { return _InventoryVATSubLedgerName; }
            set { _InventoryVATSubLedgerName = value; }
        }
        [Column(Name = "InventoryDiscount", DataType = "Int32")]
        public Int32 InventoryDiscount
        {
            get { return _InventoryDiscount; }
            set { _InventoryDiscount = value; }
        }
        [Column(Name = "InventoryDiscountGLAccountNo", DataType = "String")]
        public String InventoryDiscountGLAccountNo
        {
            get { return _InventoryDiscountGLAccountNo; }
            set { _InventoryDiscountGLAccountNo = value; }
        }
        [Column(Name = "InventoryDiscountGLAccountName", DataType = "String")]
        public String InventoryDiscountGLAccountName
        {
            get { return _InventoryDiscountGLAccountName; }
            set { _InventoryDiscountGLAccountName = value; }
        }
        [Column(Name = "InventoryDiscountSubLedgerID", DataType = "Int32")]
        public Int32 InventoryDiscountSubLedgerID
        {
            get { return _InventoryDiscountSubLedgerID; }
            set { _InventoryDiscountSubLedgerID = value; }
        }
        [Column(Name = "InventoryDiscountSearchDialogTypeName", DataType = "String")]
        public String InventoryDiscountSearchDialogTypeName
        {
            get { return _InventoryDiscountSearchDialogTypeName; }
            set { _InventoryDiscountSearchDialogTypeName = value; }
        }
        [Column(Name = "InventoryDiscountIDFieldName", DataType = "String")]
        public String InventoryDiscountIDFieldName
        {
            get { return _InventoryDiscountIDFieldName; }
            set { _InventoryDiscountIDFieldName = value; }
        }
        [Column(Name = "InventoryDiscountCodeFieldName", DataType = "String")]
        public String InventoryDiscountCodeFieldName
        {
            get { return _InventoryDiscountCodeFieldName; }
            set { _InventoryDiscountCodeFieldName = value; }
        }
        [Column(Name = "InventoryDiscountDisplayFieldName", DataType = "String")]
        public String InventoryDiscountDisplayFieldName
        {
            get { return _InventoryDiscountDisplayFieldName; }
            set { _InventoryDiscountDisplayFieldName = value; }
        }
        [Column(Name = "InventoryDiscountMethodName", DataType = "String")]
        public String InventoryDiscountMethodName
        {
            get { return _InventoryDiscountMethodName; }
            set { _InventoryDiscountMethodName = value; }
        }
        [Column(Name = "InventoryDiscountFilterExpression", DataType = "String")]
        public String InventoryDiscountFilterExpression
        {
            get { return _InventoryDiscountFilterExpression; }
            set { _InventoryDiscountFilterExpression = value; }
        }
        [Column(Name = "InventoryDiscountSubLedger", DataType = "Int32")]
        public Int32 InventoryDiscountSubLedger
        {
            get { return _InventoryDiscountSubLedger; }
            set { _InventoryDiscountSubLedger = value; }
        }
        [Column(Name = "InventoryDiscountSubLedgerCode", DataType = "String")]
        public String InventoryDiscountSubLedgerCode
        {
            get { return _InventoryDiscountSubLedgerCode; }
            set { _InventoryDiscountSubLedgerCode = value; }
        }
        [Column(Name = "InventoryDiscountSubLedgerName", DataType = "String")]
        public String InventoryDiscountSubLedgerName
        {
            get { return _InventoryDiscountSubLedgerName; }
            set { _InventoryDiscountSubLedgerName = value; }
        }
        [Column(Name = "COGS", DataType = "Int32")]
        public Int32 COGS
        {
            get { return _COGS; }
            set { _COGS = value; }
        }
        [Column(Name = "COGSGLAccountNo", DataType = "String")]
        public String COGSGLAccountNo
        {
            get { return _COGSGLAccountNo; }
            set { _COGSGLAccountNo = value; }
        }
        [Column(Name = "COGSGLAccountName", DataType = "String")]
        public String COGSGLAccountName
        {
            get { return _COGSGLAccountName; }
            set { _COGSGLAccountName = value; }
        }
        [Column(Name = "COGSSubLedgerID", DataType = "Int32")]
        public Int32 COGSSubLedgerID
        {
            get { return _COGSSubLedgerID; }
            set { _COGSSubLedgerID = value; }
        }
        [Column(Name = "COGSSearchDialogTypeName", DataType = "String")]
        public String COGSSearchDialogTypeName
        {
            get { return _COGSSearchDialogTypeName; }
            set { _COGSSearchDialogTypeName = value; }
        }
        [Column(Name = "COGSIDFieldName", DataType = "String")]
        public String COGSIDFieldName
        {
            get { return _COGSIDFieldName; }
            set { _COGSIDFieldName = value; }
        }
        [Column(Name = "COGSCodeFieldName", DataType = "String")]
        public String COGSCodeFieldName
        {
            get { return _COGSCodeFieldName; }
            set { _COGSCodeFieldName = value; }
        }
        [Column(Name = "COGSDisplayFieldName", DataType = "String")]
        public String COGSDisplayFieldName
        {
            get { return _COGSDisplayFieldName; }
            set { _COGSDisplayFieldName = value; }
        }
        [Column(Name = "COGSMethodName", DataType = "String")]
        public String COGSMethodName
        {
            get { return _COGSMethodName; }
            set { _COGSMethodName = value; }
        }
        [Column(Name = "COGSFilterExpression", DataType = "String")]
        public String COGSFilterExpression
        {
            get { return _COGSFilterExpression; }
            set { _COGSFilterExpression = value; }
        }
        [Column(Name = "COGSSubLedger", DataType = "Int32")]
        public Int32 COGSSubLedger
        {
            get { return _COGSSubLedger; }
            set { _COGSSubLedger = value; }
        }
        [Column(Name = "COGSSubLedgerCode", DataType = "String")]
        public String COGSSubLedgerCode
        {
            get { return _COGSSubLedgerCode; }
            set { _COGSSubLedgerCode = value; }
        }
        [Column(Name = "COGSSubLedgerName", DataType = "String")]
        public String COGSSubLedgerName
        {
            get { return _COGSSubLedgerName; }
            set { _COGSSubLedgerName = value; }
        }
        [Column(Name = "Consumption", DataType = "Int32")]
        public Int32 Consumption
        {
            get { return _Consumption; }
            set { _Consumption = value; }
        }
        [Column(Name = "ConsumptionGLAccountNo", DataType = "String")]
        public String ConsumptionGLAccountNo
        {
            get { return _ConsumptionGLAccountNo; }
            set { _ConsumptionGLAccountNo = value; }
        }
        [Column(Name = "ConsumptionGLAccountName", DataType = "String")]
        public String ConsumptionGLAccountName
        {
            get { return _ConsumptionGLAccountName; }
            set { _ConsumptionGLAccountName = value; }
        }
        [Column(Name = "ConsumptionSubLedgerID", DataType = "Int32")]
        public Int32 ConsumptionSubLedgerID
        {
            get { return _ConsumptionSubLedgerID; }
            set { _ConsumptionSubLedgerID = value; }
        }
        [Column(Name = "ConsumptionSearchDialogTypeName", DataType = "String")]
        public String ConsumptionSearchDialogTypeName
        {
            get { return _ConsumptionSearchDialogTypeName; }
            set { _ConsumptionSearchDialogTypeName = value; }
        }
        [Column(Name = "ConsumptionIDFieldName", DataType = "String")]
        public String ConsumptionIDFieldName
        {
            get { return _ConsumptionIDFieldName; }
            set { _ConsumptionIDFieldName = value; }
        }
        [Column(Name = "ConsumptionCodeFieldName", DataType = "String")]
        public String ConsumptionCodeFieldName
        {
            get { return _ConsumptionCodeFieldName; }
            set { _ConsumptionCodeFieldName = value; }
        }
        [Column(Name = "ConsumptionDisplayFieldName", DataType = "String")]
        public String ConsumptionDisplayFieldName
        {
            get { return _ConsumptionDisplayFieldName; }
            set { _ConsumptionDisplayFieldName = value; }
        }
        [Column(Name = "ConsumptionMethodName", DataType = "String")]
        public String ConsumptionMethodName
        {
            get { return _ConsumptionMethodName; }
            set { _ConsumptionMethodName = value; }
        }
        [Column(Name = "ConsumptionFilterExpression", DataType = "String")]
        public String ConsumptionFilterExpression
        {
            get { return _ConsumptionFilterExpression; }
            set { _ConsumptionFilterExpression = value; }
        }
        [Column(Name = "ConsumptionSubLedger", DataType = "Int32")]
        public Int32 ConsumptionSubLedger
        {
            get { return _ConsumptionSubLedger; }
            set { _ConsumptionSubLedger = value; }
        }
        [Column(Name = "ConsumptionSubLedgerCode", DataType = "String")]
        public String ConsumptionSubLedgerCode
        {
            get { return _ConsumptionSubLedgerCode; }
            set { _ConsumptionSubLedgerCode = value; }
        }
        [Column(Name = "ConsumptionSubLedgerName", DataType = "String")]
        public String ConsumptionSubLedgerName
        {
            get { return _ConsumptionSubLedgerName; }
            set { _ConsumptionSubLedgerName = value; }
        }
        [Column(Name = "PurchasePriceVariant", DataType = "Int32")]
        public Int32 PurchasePriceVariant
        {
            get { return _PurchasePriceVariant; }
            set { _PurchasePriceVariant = value; }
        }
        [Column(Name = "PurchasePriceVariantGLAccountNo", DataType = "String")]
        public String PurchasePriceVariantGLAccountNo
        {
            get { return _PurchasePriceVariantGLAccountNo; }
            set { _PurchasePriceVariantGLAccountNo = value; }
        }
        [Column(Name = "PurchasePriceVariantGLAccountName", DataType = "String")]
        public String PurchasePriceVariantGLAccountName
        {
            get { return _PurchasePriceVariantGLAccountName; }
            set { _PurchasePriceVariantGLAccountName = value; }
        }
        [Column(Name = "PurchasePriceVariantSubLedgerID", DataType = "Int32")]
        public Int32 PurchasePriceVariantSubLedgerID
        {
            get { return _PurchasePriceVariantSubLedgerID; }
            set { _PurchasePriceVariantSubLedgerID = value; }
        }
        [Column(Name = "PurchasePriceVariantSearchDialogTypeName", DataType = "String")]
        public String PurchasePriceVariantSearchDialogTypeName
        {
            get { return _PurchasePriceVariantSearchDialogTypeName; }
            set { _PurchasePriceVariantSearchDialogTypeName = value; }
        }
        [Column(Name = "PurchasePriceVariantIDFieldName", DataType = "String")]
        public String PurchasePriceVariantIDFieldName
        {
            get { return _PurchasePriceVariantIDFieldName; }
            set { _PurchasePriceVariantIDFieldName = value; }
        }
        [Column(Name = "PurchasePriceVariantCodeFieldName", DataType = "String")]
        public String PurchasePriceVariantCodeFieldName
        {
            get { return _PurchasePriceVariantCodeFieldName; }
            set { _PurchasePriceVariantCodeFieldName = value; }
        }
        [Column(Name = "PurchasePriceVariantDisplayFieldName", DataType = "String")]
        public String PurchasePriceVariantDisplayFieldName
        {
            get { return _PurchasePriceVariantDisplayFieldName; }
            set { _PurchasePriceVariantDisplayFieldName = value; }
        }
        [Column(Name = "PurchasePriceVariantMethodName", DataType = "String")]
        public String PurchasePriceVariantMethodName
        {
            get { return _PurchasePriceVariantMethodName; }
            set { _PurchasePriceVariantMethodName = value; }
        }
        [Column(Name = "PurchasePriceVariantFilterExpression", DataType = "String")]
        public String PurchasePriceVariantFilterExpression
        {
            get { return _PurchasePriceVariantFilterExpression; }
            set { _PurchasePriceVariantFilterExpression = value; }
        }
        [Column(Name = "PurchasePriceVariantSubLedger", DataType = "Int32")]
        public Int32 PurchasePriceVariantSubLedger
        {
            get { return _PurchasePriceVariantSubLedger; }
            set { _PurchasePriceVariantSubLedger = value; }
        }
        [Column(Name = "PurchasePriceVariantSubLedgerCode", DataType = "String")]
        public String PurchasePriceVariantSubLedgerCode
        {
            get { return _PurchasePriceVariantSubLedgerCode; }
            set { _PurchasePriceVariantSubLedgerCode = value; }
        }
        [Column(Name = "PurchasePriceVariantSubLedgerName", DataType = "String")]
        public String PurchasePriceVariantSubLedgerName
        {
            get { return _PurchasePriceVariantSubLedgerName; }
            set { _PurchasePriceVariantSubLedgerName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vGradePromotionFormulaDt
    [Serializable]
    [Table(Name = "vGradePromotionFormulaDt")]
    public class vGradePromotionFormulaDt
    {
        private Int32 _GradePromotionFormulaDtID;
        private Int32 _GradePromotionFormulaID;
        private String _GradePromotionFormulaDtName;
        private Boolean _IsCurrentGrade;
        private String _GCGrade;
        private String _Grade;
        private String _GCPeriodSection;
        private String _PeriodSection;
        private Int16 _DisplayOrder;
        private Decimal _FinalMarkPercentage;
        private Boolean _IsDeleted;

        [Column(Name = "GradePromotionFormulaDtID", DataType = "Int32")]
        public Int32 GradePromotionFormulaDtID
        {
            get { return _GradePromotionFormulaDtID; }
            set { _GradePromotionFormulaDtID = value; }
        }
        [Column(Name = "GradePromotionFormulaID", DataType = "Int32")]
        public Int32 GradePromotionFormulaID
        {
            get { return _GradePromotionFormulaID; }
            set { _GradePromotionFormulaID = value; }
        }
        [Column(Name = "GradePromotionFormulaDtName", DataType = "String")]
        public String GradePromotionFormulaDtName
        {
            get { return _GradePromotionFormulaDtName; }
            set { _GradePromotionFormulaDtName = value; }
        }
        [Column(Name = "IsCurrentGrade", DataType = "Boolean")]
        public Boolean IsCurrentGrade
        {
            get { return _IsCurrentGrade; }
            set { _IsCurrentGrade = value; }
        }
        [Column(Name = "GCGrade", DataType = "String")]
        public String GCGrade
        {
            get { return _GCGrade; }
            set { _GCGrade = value; }
        }
        [Column(Name = "Grade", DataType = "String")]
        public String Grade
        {
            get { return _Grade; }
            set { _Grade = value; }
        }
        [Column(Name = "GCPeriodSection", DataType = "String")]
        public String GCPeriodSection
        {
            get { return _GCPeriodSection; }
            set { _GCPeriodSection = value; }
        }
        [Column(Name = "PeriodSection", DataType = "String")]
        public String PeriodSection
        {
            get { return _PeriodSection; }
            set { _PeriodSection = value; }
        }
        [Column(Name = "DisplayOrder", DataType = "Int16")]
        public Int16 DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        [Column(Name = "FinalMarkPercentage", DataType = "Decimal")]
        public Decimal FinalMarkPercentage
        {
            get { return _FinalMarkPercentage; }
            set { _FinalMarkPercentage = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vInterfaceJournalSettingDtCustom
    [Serializable]
    [Table(Name = "vInterfaceJournalSettingDtCustom")]
    public class vInterfaceJournalSettingDtCustom
    {
        private String _TransactionCode;
        private String _SiteID;
        private String _TypeCode;
        private String _MenuCode;
        private String _DataSource;
        private String _TypeName;
        private String _Position;

        [Column(Name = "TransactionCode", DataType = "String")]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "TypeCode", DataType = "String")]
        public String TypeCode
        {
            get { return _TypeCode; }
            set { _TypeCode = value; }
        }
        [Column(Name = "MenuCode", DataType = "String")]
        public String MenuCode
        {
            get { return _MenuCode; }
            set { _MenuCode = value; }
        }
        [Column(Name = "DataSource", DataType = "String")]
        public String DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }
        [Column(Name = "TypeName", DataType = "String")]
        public String TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }
        [Column(Name = "Position", DataType = "String")]
        public String Position
        {
            get { return _Position; }
            set { _Position = value; }
        }
    }
    #endregion
    #region vItemAlternateUnit
    [Serializable]
    [Table(Name = "vItemAlternateUnit")]
    public partial class vItemAlternateUnit
    {
        private Int32 _ID;
        private Int32 _ItemID;
        private String _GCAlternateUnit;
        private String _AlternateUnit;
        private Decimal _ConversionFactor;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "GCAlternateUnit", DataType = "String")]
        public String GCAlternateUnit
        {
            get { return _GCAlternateUnit; }
            set { _GCAlternateUnit = value; }
        }
        [Column(Name = "AlternateUnit", DataType = "String")]
        public String AlternateUnit
        {
            get { return _AlternateUnit; }
            set { _AlternateUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vItemAlternateUnitCustom
    [Serializable]
    [Table(Name = "vItemAlternateUnitCustom")]
    public partial class vItemAlternateUnitCustom
    {
        private Int32 _ItemID;
        private String _GCAlternateUnit;
        private String _AlternateUnit;
        private Decimal _ConversionFactor;
        private String _GCItemUnit;
        private String _ItemUnit;

        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "GCAlternateUnit", DataType = "String")]
        public String GCAlternateUnit
        {
            get { return _GCAlternateUnit; }
            set { _GCAlternateUnit = value; }
        }
        [Column(Name = "AlternateUnit", DataType = "String")]
        public String AlternateUnit
        {
            get { return _AlternateUnit; }
            set { _AlternateUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
    }
    #endregion
    #region vItemBalance
    [Serializable]
    [Table(Name = "vItemBalance")]
    public partial class vItemBalance
    {
        private Int32 _ID;
        private Int32 _LocationID;
        private String _SiteID;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private String _GCItemType;
        private String _GCItemUnit;
        private String _ItemUnit;
        private String _GCPurchaseUnit;
        private Boolean _IsControlExpired;
        private String _PurchaseUnit;
        private Int32 _ItemGroupID;
        private String _GCReorderType;
        private Decimal _QuantityMIN;
        private Decimal _QuantityMAX;
        private Decimal _QuantityBEGIN;
        private Decimal _QuantityIN;
        private Decimal _QuantityOUT;
        private Decimal _QuantityEND;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "GCItemType", DataType = "String")]
        public String GCItemType
        {
            get { return _GCItemType; }
            set { _GCItemType = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "GCPurchaseUnit", DataType = "String")]
        public String GCPurchaseUnit
        {
            get { return _GCPurchaseUnit; }
            set { _GCPurchaseUnit = value; }
        }
        [Column(Name = "IsControlExpired", DataType = "Boolean")]
        public Boolean IsControlExpired
        {
            get { return _IsControlExpired; }
            set { _IsControlExpired = value; }
        }
        [Column(Name = "PurchaseUnit", DataType = "String")]
        public String PurchaseUnit
        {
            get { return _PurchaseUnit; }
            set { _PurchaseUnit = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "GCReorderType", DataType = "String")]
        public String GCReorderType
        {
            get { return _GCReorderType; }
            set { _GCReorderType = value; }
        }
        [Column(Name = "QuantityMIN", DataType = "Decimal")]
        public Decimal QuantityMIN
        {
            get { return _QuantityMIN; }
            set { _QuantityMIN = value; }
        }
        [Column(Name = "QuantityMAX", DataType = "Decimal")]
        public Decimal QuantityMAX
        {
            get { return _QuantityMAX; }
            set { _QuantityMAX = value; }
        }
        [Column(Name = "QuantityBEGIN", DataType = "Decimal")]
        public Decimal QuantityBEGIN
        {
            get { return _QuantityBEGIN; }
            set { _QuantityBEGIN = value; }
        }
        [Column(Name = "QuantityIN", DataType = "Decimal")]
        public Decimal QuantityIN
        {
            get { return _QuantityIN; }
            set { _QuantityIN = value; }
        }
        [Column(Name = "QuantityOUT", DataType = "Decimal")]
        public Decimal QuantityOUT
        {
            get { return _QuantityOUT; }
            set { _QuantityOUT = value; }
        }
        [Column(Name = "QuantityEND", DataType = "Decimal")]
        public Decimal QuantityEND
        {
            get { return _QuantityEND; }
            set { _QuantityEND = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vItemBalanceInventory
    [Serializable]
    [Table(Name = "vItemBalanceInventory")]
    public partial class vItemBalanceInventory
    {
        private Int32 _ID;
        private Int32 _LocationID;
        private String _SiteID;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private String _GCItemType;
        private String _GCItemUnit;
        private String _ItemUnit;
        private String _GCPurchaseUnit;
        private Boolean _IsControlExpired;
        private String _PurchaseUnit;
        private Int32 _ItemGroupID;
        private String _GCReorderType;
        private Decimal _QuantityMIN;
        private Decimal _QuantityMAX;
        private Decimal _QuantityBEGIN;
        private Decimal _QuantityIN;
        private Decimal _QuantityOUT;
        private Decimal _QuantityEND;
        private Boolean _IsDeleted;
        private Decimal _ItemRequestQtyOnOrder;
        private Decimal _PurchaseRequestQtyOnOrder;
        private Decimal _PurchaseOrderQtyOnOrder;
        private Decimal _ItemDistributionQtyOnOrder;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "GCItemType", DataType = "String")]
        public String GCItemType
        {
            get { return _GCItemType; }
            set { _GCItemType = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "GCPurchaseUnit", DataType = "String")]
        public String GCPurchaseUnit
        {
            get { return _GCPurchaseUnit; }
            set { _GCPurchaseUnit = value; }
        }
        [Column(Name = "IsControlExpired", DataType = "Boolean")]
        public Boolean IsControlExpired
        {
            get { return _IsControlExpired; }
            set { _IsControlExpired = value; }
        }
        [Column(Name = "PurchaseUnit", DataType = "String")]
        public String PurchaseUnit
        {
            get { return _PurchaseUnit; }
            set { _PurchaseUnit = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "GCReorderType", DataType = "String")]
        public String GCReorderType
        {
            get { return _GCReorderType; }
            set { _GCReorderType = value; }
        }
        [Column(Name = "QuantityMIN", DataType = "Decimal")]
        public Decimal QuantityMIN
        {
            get { return _QuantityMIN; }
            set { _QuantityMIN = value; }
        }
        [Column(Name = "QuantityMAX", DataType = "Decimal")]
        public Decimal QuantityMAX
        {
            get { return _QuantityMAX; }
            set { _QuantityMAX = value; }
        }
        [Column(Name = "QuantityBEGIN", DataType = "Decimal")]
        public Decimal QuantityBEGIN
        {
            get { return _QuantityBEGIN; }
            set { _QuantityBEGIN = value; }
        }
        [Column(Name = "QuantityIN", DataType = "Decimal")]
        public Decimal QuantityIN
        {
            get { return _QuantityIN; }
            set { _QuantityIN = value; }
        }
        [Column(Name = "QuantityOUT", DataType = "Decimal")]
        public Decimal QuantityOUT
        {
            get { return _QuantityOUT; }
            set { _QuantityOUT = value; }
        }
        [Column(Name = "QuantityEND", DataType = "Decimal")]
        public Decimal QuantityEND
        {
            get { return _QuantityEND; }
            set { _QuantityEND = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "ItemRequestQtyOnOrder", DataType = "Decimal")]
        public Decimal ItemRequestQtyOnOrder
        {
            get { return _ItemRequestQtyOnOrder; }
            set { _ItemRequestQtyOnOrder = value; }
        }
        [Column(Name = "PurchaseRequestQtyOnOrder", DataType = "Decimal")]
        public Decimal PurchaseRequestQtyOnOrder
        {
            get { return _PurchaseRequestQtyOnOrder; }
            set { _PurchaseRequestQtyOnOrder = value; }
        }
        [Column(Name = "PurchaseOrderQtyOnOrder", DataType = "Decimal")]
        public Decimal PurchaseOrderQtyOnOrder
        {
            get { return _PurchaseOrderQtyOnOrder; }
            set { _PurchaseOrderQtyOnOrder = value; }
        }
        [Column(Name = "ItemDistributionQtyOnOrder", DataType = "Decimal")]
        public Decimal ItemDistributionQtyOnOrder
        {
            get { return _ItemDistributionQtyOnOrder; }
            set { _ItemDistributionQtyOnOrder = value; }
        }
    }
    #endregion
    #region vItemCost
    [Serializable]
    [Table(Name = "vItemCost")]
    public partial class vItemCost
    {
        private Int32 _ItemCostID;
        private Int32 _ItemID;
        private String _ItemName1;
        private String _ItemName2;
        private String _SiteID;
        private String _SiteName;
        private Decimal _PreviousMaterial;
        private Decimal _CurrentMaterial;
        private Decimal _TotalMaterial;
        private Decimal _PreviousLabor;
        private Decimal _CurrentLabor;
        private Decimal _TotalLabor;
        private Decimal _PreviousOverhead;
        private Decimal _CurrentOverhead;
        private Decimal _TotalOverhead;
        private Decimal _PreviousSubContract;
        private Decimal _CurrentSubContract;
        private Decimal _TotalSubContract;
        private Decimal _PreviousBurden;
        private Decimal _CurrentBurden;
        private Decimal _TotalBurden;
        private Boolean _IsDeleted;

        [Column(Name = "ItemCostID", DataType = "Int32")]
        public Int32 ItemCostID
        {
            get { return _ItemCostID; }
            set { _ItemCostID = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemName2", DataType = "String")]
        public String ItemName2
        {
            get { return _ItemName2; }
            set { _ItemName2 = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "SiteName", DataType = "String")]
        public String SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }
        [Column(Name = "PreviousMaterial", DataType = "Decimal")]
        public Decimal PreviousMaterial
        {
            get { return _PreviousMaterial; }
            set { _PreviousMaterial = value; }
        }
        [Column(Name = "CurrentMaterial", DataType = "Decimal")]
        public Decimal CurrentMaterial
        {
            get { return _CurrentMaterial; }
            set { _CurrentMaterial = value; }
        }
        [Column(Name = "TotalMaterial", DataType = "Decimal")]
        public Decimal TotalMaterial
        {
            get { return _TotalMaterial; }
            set { _TotalMaterial = value; }
        }
        [Column(Name = "PreviousLabor", DataType = "Decimal")]
        public Decimal PreviousLabor
        {
            get { return _PreviousLabor; }
            set { _PreviousLabor = value; }
        }
        [Column(Name = "CurrentLabor", DataType = "Decimal")]
        public Decimal CurrentLabor
        {
            get { return _CurrentLabor; }
            set { _CurrentLabor = value; }
        }
        [Column(Name = "TotalLabor", DataType = "Decimal")]
        public Decimal TotalLabor
        {
            get { return _TotalLabor; }
            set { _TotalLabor = value; }
        }
        [Column(Name = "PreviousOverhead", DataType = "Decimal")]
        public Decimal PreviousOverhead
        {
            get { return _PreviousOverhead; }
            set { _PreviousOverhead = value; }
        }
        [Column(Name = "CurrentOverhead", DataType = "Decimal")]
        public Decimal CurrentOverhead
        {
            get { return _CurrentOverhead; }
            set { _CurrentOverhead = value; }
        }
        [Column(Name = "TotalOverhead", DataType = "Decimal")]
        public Decimal TotalOverhead
        {
            get { return _TotalOverhead; }
            set { _TotalOverhead = value; }
        }
        [Column(Name = "PreviousSubContract", DataType = "Decimal")]
        public Decimal PreviousSubContract
        {
            get { return _PreviousSubContract; }
            set { _PreviousSubContract = value; }
        }
        [Column(Name = "CurrentSubContract", DataType = "Decimal")]
        public Decimal CurrentSubContract
        {
            get { return _CurrentSubContract; }
            set { _CurrentSubContract = value; }
        }
        [Column(Name = "TotalSubContract", DataType = "Decimal")]
        public Decimal TotalSubContract
        {
            get { return _TotalSubContract; }
            set { _TotalSubContract = value; }
        }
        [Column(Name = "PreviousBurden", DataType = "Decimal")]
        public Decimal PreviousBurden
        {
            get { return _PreviousBurden; }
            set { _PreviousBurden = value; }
        }
        [Column(Name = "CurrentBurden", DataType = "Decimal")]
        public Decimal CurrentBurden
        {
            get { return _CurrentBurden; }
            set { _CurrentBurden = value; }
        }
        [Column(Name = "TotalBurden", DataType = "Decimal")]
        public Decimal TotalBurden
        {
            get { return _TotalBurden; }
            set { _TotalBurden = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vItemDistributionDt
    [Serializable]
    [Table(Name = "vItemDistributionDt")]
    public partial class vItemDistributionDt
    {
        private Int32 _ID;
        private String _DistributionNo;
        private Int32 _DistributionID;
        private String _GCDistributionStatus;
        private String _DistributionStatus;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private String _ItemName2;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private DateTime _DeliveryDate;
        private Int32 _FromLocationID;
        private String _FromLocationName;
        private Int32 _ToLocationID;
        private String _ToLocationName;
        private Decimal _Quantity;
        private String _GCItemUnit;
        private String _ItemUnit;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private Decimal _ConversionFactor;
        private String _GCItemDetailStatus;
        private Boolean _isDeleted;
        private String _LastUpdateByName;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "DistributionNo", DataType = "String")]
        public String DistributionNo
        {
            get { return _DistributionNo; }
            set { _DistributionNo = value; }
        }
        [Column(Name = "DistributionID", DataType = "Int32")]
        public Int32 DistributionID
        {
            get { return _DistributionID; }
            set { _DistributionID = value; }
        }
        [Column(Name = "GCDistributionStatus", DataType = "String")]
        public String GCDistributionStatus
        {
            get { return _GCDistributionStatus; }
            set { _GCDistributionStatus = value; }
        }
        [Column(Name = "DistributionStatus", DataType = "String")]
        public String DistributionStatus
        {
            get { return _DistributionStatus; }
            set { _DistributionStatus = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemName2", DataType = "String")]
        public String ItemName2
        {
            get { return _ItemName2; }
            set { _ItemName2 = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "DeliveryDate", DataType = "DateTime")]
        public DateTime DeliveryDate
        {
            get { return _DeliveryDate; }
            set { _DeliveryDate = value; }
        }
        [Column(Name = "FromLocationID", DataType = "Int32")]
        public Int32 FromLocationID
        {
            get { return _FromLocationID; }
            set { _FromLocationID = value; }
        }
        [Column(Name = "FromLocationName", DataType = "String")]
        public String FromLocationName
        {
            get { return _FromLocationName; }
            set { _FromLocationName = value; }
        }
        [Column(Name = "ToLocationID", DataType = "Int32")]
        public Int32 ToLocationID
        {
            get { return _ToLocationID; }
            set { _ToLocationID = value; }
        }
        [Column(Name = "ToLocationName", DataType = "String")]
        public String ToLocationName
        {
            get { return _ToLocationName; }
            set { _ToLocationName = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
        [Column(Name = "isDeleted", DataType = "Boolean")]
        public Boolean isDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }
        [Column(Name = "LastUpdateByName", DataType = "String")]
        public String LastUpdateByName
        {
            get { return _LastUpdateByName; }
            set { _LastUpdateByName = value; }
        }

    }
    #endregion
    #region vItemDistributionHd
    [Serializable]
    [Table(Name = "vItemDistributionHd")]
    public partial class vItemDistributionHd
    {
        private Int32 _DistributionID;
        private String _DistributionNo;
        private Int32 _ItemRequestID;
        private DateTime _DeliveryDate;
        private String _DeliveryTime;
        private Int32 _FromLocationID;
        private String _FromLocationCode;
        private String _FromLocationName;
        private Int32 _FromLocationItemGroupID;
        private Int32 _ToLocationID;
        private String _ToLocationCode;
        private String _ToLocationName;
        private String _DeliveredBy;
        private String _GCDistributionStatus;
        private String _DistributionStatus;
        private String _DistributionStatusWatermark;
        private Boolean _isGeneratedBySystem;
        private String _DeliveryRemarks;
        private Int32 _NumberOfItems;
        private Int32 _NumberOfApprovedItems;

        [Column(Name = "DistributionID", DataType = "Int32")]
        public Int32 DistributionID
        {
            get { return _DistributionID; }
            set { _DistributionID = value; }
        }
        [Column(Name = "DistributionNo", DataType = "String")]
        public String DistributionNo
        {
            get { return _DistributionNo; }
            set { _DistributionNo = value; }
        }
        [Column(Name = "ItemRequestID", DataType = "Int32")]
        public Int32 ItemRequestID
        {
            get { return _ItemRequestID; }
            set { _ItemRequestID = value; }
        }
        [Column(Name = "DeliveryDate", DataType = "DateTime")]
        public DateTime DeliveryDate
        {
            get { return _DeliveryDate; }
            set { _DeliveryDate = value; }
        }
        [Column(Name = "DeliveryTime", DataType = "String")]
        public String DeliveryTime
        {
            get { return _DeliveryTime; }
            set { _DeliveryTime = value; }
        }
        [Column(Name = "FromLocationID", DataType = "Int32")]
        public Int32 FromLocationID
        {
            get { return _FromLocationID; }
            set { _FromLocationID = value; }
        }
        [Column(Name = "FromLocationCode", DataType = "String")]
        public String FromLocationCode
        {
            get { return _FromLocationCode; }
            set { _FromLocationCode = value; }
        }
        [Column(Name = "FromLocationName", DataType = "String")]
        public String FromLocationName
        {
            get { return _FromLocationName; }
            set { _FromLocationName = value; }
        }
        [Column(Name = "FromLocationItemGroupID", DataType = "Int32")]
        public Int32 FromLocationItemGroupID
        {
            get { return _FromLocationItemGroupID; }
            set { _FromLocationItemGroupID = value; }
        }
        [Column(Name = "ToLocationID", DataType = "Int32")]
        public Int32 ToLocationID
        {
            get { return _ToLocationID; }
            set { _ToLocationID = value; }
        }
        [Column(Name = "ToLocationCode", DataType = "String")]
        public String ToLocationCode
        {
            get { return _ToLocationCode; }
            set { _ToLocationCode = value; }
        }
        [Column(Name = "ToLocationName", DataType = "String")]
        public String ToLocationName
        {
            get { return _ToLocationName; }
            set { _ToLocationName = value; }
        }
        [Column(Name = "DeliveredBy", DataType = "String")]
        public String DeliveredBy
        {
            get { return _DeliveredBy; }
            set { _DeliveredBy = value; }
        }
        [Column(Name = "GCDistributionStatus", DataType = "String")]
        public String GCDistributionStatus
        {
            get { return _GCDistributionStatus; }
            set { _GCDistributionStatus = value; }
        }
        [Column(Name = "DistributionStatus", DataType = "String")]
        public String DistributionStatus
        {
            get { return _DistributionStatus; }
            set { _DistributionStatus = value; }
        }
        [Column(Name = "DistributionStatusWatermark", DataType = "String")]
        public String DistributionStatusWatermark
        {
            get { return _DistributionStatusWatermark; }
            set { _DistributionStatusWatermark = value; }
        }
        [Column(Name = "isGeneratedBySystem", DataType = "Boolean")]
        public Boolean isGeneratedBySystem
        {
            get { return _isGeneratedBySystem; }
            set { _isGeneratedBySystem = value; }
        }
        [Column(Name = "DeliveryRemarks", DataType = "String")]
        public String DeliveryRemarks
        {
            get { return _DeliveryRemarks; }
            set { _DeliveryRemarks = value; }
        }
        [Column(Name = "NumberOfItems", DataType = "Int32")]
        public Int32 NumberOfItems
        {
            get { return _NumberOfItems; }
            set { _NumberOfItems = value; }
        }
        [Column(Name = "NumberOfApprovedItems", DataType = "Int32")]
        public Int32 NumberOfApprovedItems
        {
            get { return _NumberOfApprovedItems; }
            set { _NumberOfApprovedItems = value; }
        }
    }
    #endregion
    #region vItemGroupMaster
    [Serializable]
    [Table(Name = "vItemGroupMaster")]
    public class vItemGroupMaster
    {
        private Int32 _ItemGroupID;
        private String _GCItemType;
        private String _ItemType;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private String _ItemGroupName2;
        private Boolean _IsHeader;
        private Int32 _ParentID;
        private String _ParentCode;
        private String _ParentName;
        private Int16 _PrintOrder;
        private Boolean _IsDeleted;
        private Int32 _Level;
        private String _DisplayPath;

        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "GCItemType", DataType = "String")]
        public String GCItemType
        {
            get { return _GCItemType; }
            set { _GCItemType = value; }
        }
        [Column(Name = "ItemType", DataType = "String")]
        public String ItemType
        {
            get { return _ItemType; }
            set { _ItemType = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "ItemGroupName2", DataType = "String")]
        public String ItemGroupName2
        {
            get { return _ItemGroupName2; }
            set { _ItemGroupName2 = value; }
        }
        [Column(Name = "IsHeader", DataType = "Boolean")]
        public Boolean IsHeader
        {
            get { return _IsHeader; }
            set { _IsHeader = value; }
        }
        [Column(Name = "ParentID", DataType = "Int32")]
        public Int32 ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        [Column(Name = "ParentCode", DataType = "String")]
        public String ParentCode
        {
            get { return _ParentCode; }
            set { _ParentCode = value; }
        }
        [Column(Name = "ParentName", DataType = "String")]
        public String ParentName
        {
            get { return _ParentName; }
            set { _ParentName = value; }
        }
        [Column(Name = "PrintOrder", DataType = "Int16")]
        public Int16 PrintOrder
        {
            get { return _PrintOrder; }
            set { _PrintOrder = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "Level", DataType = "Int32")]
        public Int32 Level
        {
            get { return _Level; }
            set { _Level = value; }
        }
        [Column(Name = "DisplayPath", DataType = "String")]
        public String DisplayPath
        {
            get { return _DisplayPath; }
            set { _DisplayPath = value; }
        }
    }
    #endregion
    #region vItemMaster
    [Serializable]
    [Table(Name = "vItemMaster")]
    public class vItemMaster
    {
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private String _ItemName2;
        private String _GCItemType;
        private String _ItemType;
        private String _GCItemStatus;
        private String _GCItemUnit;
        private String _ItemUnit;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Int32 _ProductLineID;
        private String _ProductLineCode;
        private String _ProductLineName;
        private String _Remarks;
        private Boolean _IsIncludeInAdminCalculation;
        private Boolean _IsDeleted;

        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemName2", DataType = "String")]
        public String ItemName2
        {
            get { return _ItemName2; }
            set { _ItemName2 = value; }
        }
        [Column(Name = "GCItemType", DataType = "String")]
        public String GCItemType
        {
            get { return _GCItemType; }
            set { _GCItemType = value; }
        }
        [Column(Name = "ItemType", DataType = "String")]
        public String ItemType
        {
            get { return _ItemType; }
            set { _ItemType = value; }
        }
        [Column(Name = "GCItemStatus", DataType = "String")]
        public String GCItemStatus
        {
            get { return _GCItemStatus; }
            set { _GCItemStatus = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "ProductLineID", DataType = "Int32")]
        public Int32 ProductLineID
        {
            get { return _ProductLineID; }
            set { _ProductLineID = value; }
        }
        [Column(Name = "ProductLineCode", DataType = "String")]
        public String ProductLineCode
        {
            get { return _ProductLineCode; }
            set { _ProductLineCode = value; }
        }
        [Column(Name = "ProductLineName", DataType = "String")]
        public String ProductLineName
        {
            get { return _ProductLineName; }
            set { _ProductLineName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsIncludeInAdminCalculation", DataType = "Boolean")]
        public Boolean IsIncludeInAdminCalculation
        {
            get { return _IsIncludeInAdminCalculation; }
            set { _IsIncludeInAdminCalculation = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vItemMovement
    [Serializable]
    [Table(Name = "vItemMovement")]
    public partial class vItemMovement
    {
        private Int32 _MovementID;
        private DateTime _MovementDate;
        private Int32 _LocationID;
        private String _TransactionCode;
        private String _TransactionNo;
        private Int32 _TransactionID;
        private Int32 _TransactionDtID;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private String _TransactionDescription;
        private String _DetailDesc;
        private Decimal _QuantityBEGIN;
        private Decimal _QuantityIN;
        private Decimal _QuantityOUT;
        private Decimal _QuantityEND;
        private String _GCItemUnit;
        private String _ItemUnit;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private Decimal _ConversionFactor;
        private Decimal _SalesPrice;
        private Decimal _PurchasePrice;
        private Decimal _CostAmount;
        private Int32 _CreatedBy;
        private String _CreatedByUserName;

        [Column(Name = "MovementID", DataType = "Int32")]
        public Int32 MovementID
        {
            get { return _MovementID; }
            set { _MovementID = value; }
        }
        [Column(Name = "MovementDate", DataType = "DateTime")]
        public DateTime MovementDate
        {
            get { return _MovementDate; }
            set { _MovementDate = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "TransactionCode", DataType = "String")]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
        [Column(Name = "TransactionNo", DataType = "String")]
        public String TransactionNo
        {
            get { return _TransactionNo; }
            set { _TransactionNo = value; }
        }
        [Column(Name = "TransactionID", DataType = "Int32")]
        public Int32 TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }
        [Column(Name = "TransactionDtID", DataType = "Int32")]
        public Int32 TransactionDtID
        {
            get { return _TransactionDtID; }
            set { _TransactionDtID = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "TransactionDescription", DataType = "String")]
        public String TransactionDescription
        {
            get { return _TransactionDescription; }
            set { _TransactionDescription = value; }
        }
        [Column(Name = "DetailDesc", DataType = "String")]
        public String DetailDesc
        {
            get { return _DetailDesc; }
            set { _DetailDesc = value; }
        }
        [Column(Name = "QuantityBEGIN", DataType = "Decimal")]
        public Decimal QuantityBEGIN
        {
            get { return _QuantityBEGIN; }
            set { _QuantityBEGIN = value; }
        }
        [Column(Name = "QuantityIN", DataType = "Decimal")]
        public Decimal QuantityIN
        {
            get { return _QuantityIN; }
            set { _QuantityIN = value; }
        }
        [Column(Name = "QuantityOUT", DataType = "Decimal")]
        public Decimal QuantityOUT
        {
            get { return _QuantityOUT; }
            set { _QuantityOUT = value; }
        }
        [Column(Name = "QuantityEND", DataType = "Decimal")]
        public Decimal QuantityEND
        {
            get { return _QuantityEND; }
            set { _QuantityEND = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "SalesPrice", DataType = "Decimal")]
        public Decimal SalesPrice
        {
            get { return _SalesPrice; }
            set { _SalesPrice = value; }
        }
        [Column(Name = "PurchasePrice", DataType = "Decimal")]
        public Decimal PurchasePrice
        {
            get { return _PurchasePrice; }
            set { _PurchasePrice = value; }
        }
        [Column(Name = "CostAmount", DataType = "Decimal")]
        public Decimal CostAmount
        {
            get { return _CostAmount; }
            set { _CostAmount = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedByUserName", DataType = "String")]
        public String CreatedByUserName
        {
            get { return _CreatedByUserName; }
            set { _CreatedByUserName = value; }
        }
    }
    #endregion
    #region vItemPlanning
    [Serializable]
    [Table(Name = "vItemPlanning")]
    public class vItemPlanning
    {
        private Int32 _ID;
        private String _SiteID;
        private String _SiteName;
        private Int32 _ItemID;
        private String _ItemName1;
        private String _GCItemUnit;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private Decimal _AveragePrice;
        private Int16 _LeadTime;
        private Int16 _SafetyTime;
        private Decimal _SafetyStock;
        private String _GCPurchaseUnit;
        private Decimal _MinOrderQty;
        private Decimal _MaxOrderQty;
        private Decimal _ToleranceQty;
        private Int16 _TimeFence;
        private Decimal _UnitPrice;
        private Decimal _PurchaseUnitPrice;
        private Int32 _LastBusinessPartnerID;
        private Decimal _LastPurchasePrice;
        private Decimal _LastPurchaseDiscount;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "SiteName", DataType = "String")]
        public String SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "AveragePrice", DataType = "Decimal")]
        public Decimal AveragePrice
        {
            get { return _AveragePrice; }
            set { _AveragePrice = value; }
        }
        [Column(Name = "LeadTime", DataType = "Int16")]
        public Int16 LeadTime
        {
            get { return _LeadTime; }
            set { _LeadTime = value; }
        }
        [Column(Name = "SafetyTime", DataType = "Int16")]
        public Int16 SafetyTime
        {
            get { return _SafetyTime; }
            set { _SafetyTime = value; }
        }
        [Column(Name = "SafetyStock", DataType = "Decimal")]
        public Decimal SafetyStock
        {
            get { return _SafetyStock; }
            set { _SafetyStock = value; }
        }
        [Column(Name = "GCPurchaseUnit", DataType = "String")]
        public String GCPurchaseUnit
        {
            get { return _GCPurchaseUnit; }
            set { _GCPurchaseUnit = value; }
        }
        [Column(Name = "MinOrderQty", DataType = "Decimal")]
        public Decimal MinOrderQty
        {
            get { return _MinOrderQty; }
            set { _MinOrderQty = value; }
        }
        [Column(Name = "MaxOrderQty", DataType = "Decimal")]
        public Decimal MaxOrderQty
        {
            get { return _MaxOrderQty; }
            set { _MaxOrderQty = value; }
        }
        [Column(Name = "ToleranceQty", DataType = "Decimal")]
        public Decimal ToleranceQty
        {
            get { return _ToleranceQty; }
            set { _ToleranceQty = value; }
        }
        [Column(Name = "TimeFence", DataType = "Int16")]
        public Int16 TimeFence
        {
            get { return _TimeFence; }
            set { _TimeFence = value; }
        }
        [Column(Name = "UnitPrice", DataType = "Decimal")]
        public Decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        [Column(Name = "PurchaseUnitPrice", DataType = "Decimal")]
        public Decimal PurchaseUnitPrice
        {
            get { return _PurchaseUnitPrice; }
            set { _PurchaseUnitPrice = value; }
        }

        [Column(Name = "LastBusinessPartnerID", DataType = "Int32")]
        public Int32 LastBusinessPartnerID
        {
            get { return _LastBusinessPartnerID; }
            set { _LastBusinessPartnerID = value; }
        }
        [Column(Name = "LastPurchasePrice", DataType = "Decimal")]
        public Decimal LastPurchasePrice
        {
            get { return _LastPurchasePrice; }
            set { _LastPurchasePrice = value; }
        }
        [Column(Name = "LastPurchaseDiscount", DataType = "Decimal")]
        public Decimal LastPurchaseDiscount
        {
            get { return _LastPurchaseDiscount; }
            set { _LastPurchaseDiscount = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vItemProduct
    [Serializable]
    [Table(Name = "vItemProduct")]
    public class vItemProduct
    {
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private String _ItemName2;
        private String _GCItemType;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private String _ItemGroupName2;
        private Int32 _ProductLineID;
        private String _ProductLineCode;
        private String _ProductLineName;
        private String _GCItemUnit;
        private String _ItemUnit;
        private Int32 _ProductBrandID;
        private String _ProductBrandCode;
        private String _ProductBrandName;
        private Int32 _ManufacturerID;
        private String _ManufacturerCode;
        private String _ManufacturerName;
        private Int32 _RestrictionID;
        private Int32 _MarkupID;
        private Decimal _MarginPercentage;
        private Boolean _IsInventoryItem;
        private Boolean _IsControlExpired;
        private Boolean _IsProductionItem;
        private Boolean _IsUsingStandardPrice;
        private String _GCABCClass;
        private String _ABCClass;
        private Decimal _CycleCountInterval;
        private Decimal _HETAmount;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemName2", DataType = "String")]
        public String ItemName2
        {
            get { return _ItemName2; }
            set { _ItemName2 = value; }
        }
        [Column(Name = "GCItemType", DataType = "String")]
        public String GCItemType
        {
            get { return _GCItemType; }
            set { _GCItemType = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "ItemGroupName2", DataType = "String")]
        public String ItemGroupName2
        {
            get { return _ItemGroupName2; }
            set { _ItemGroupName2 = value; }
        }
        [Column(Name = "ProductLineID", DataType = "Int32")]
        public Int32 ProductLineID
        {
            get { return _ProductLineID; }
            set { _ProductLineID = value; }
        }
        [Column(Name = "ProductLineCode", DataType = "String")]
        public String ProductLineCode
        {
            get { return _ProductLineCode; }
            set { _ProductLineCode = value; }
        }
        [Column(Name = "ProductLineName", DataType = "String")]
        public String ProductLineName
        {
            get { return _ProductLineName; }
            set { _ProductLineName = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "ProductBrandID", DataType = "Int32")]
        public Int32 ProductBrandID
        {
            get { return _ProductBrandID; }
            set { _ProductBrandID = value; }
        }
        [Column(Name = "ProductBrandCode", DataType = "String")]
        public String ProductBrandCode
        {
            get { return _ProductBrandCode; }
            set { _ProductBrandCode = value; }
        }
        [Column(Name = "ProductBrandName", DataType = "String")]
        public String ProductBrandName
        {
            get { return _ProductBrandName; }
            set { _ProductBrandName = value; }
        }
        [Column(Name = "ManufacturerID", DataType = "Int32")]
        public Int32 ManufacturerID
        {
            get { return _ManufacturerID; }
            set { _ManufacturerID = value; }
        }
        [Column(Name = "ManufacturerCode", DataType = "String")]
        public String ManufacturerCode
        {
            get { return _ManufacturerCode; }
            set { _ManufacturerCode = value; }
        }
        [Column(Name = "ManufacturerName", DataType = "String")]
        public String ManufacturerName
        {
            get { return _ManufacturerName; }
            set { _ManufacturerName = value; }
        }
        [Column(Name = "RestrictionID", DataType = "Int32")]
        public Int32 RestrictionID
        {
            get { return _RestrictionID; }
            set { _RestrictionID = value; }
        }
        [Column(Name = "MarkupID", DataType = "Int32")]
        public Int32 MarkupID
        {
            get { return _MarkupID; }
            set { _MarkupID = value; }
        }
        [Column(Name = "MarginPercentage", DataType = "Decimal")]
        public Decimal MarginPercentage
        {
            get { return _MarginPercentage; }
            set { _MarginPercentage = value; }
        }
        [Column(Name = "IsInventoryItem", DataType = "Boolean")]
        public Boolean IsInventoryItem
        {
            get { return _IsInventoryItem; }
            set { _IsInventoryItem = value; }
        }
        [Column(Name = "IsControlExpired", DataType = "Boolean")]
        public Boolean IsControlExpired
        {
            get { return _IsControlExpired; }
            set { _IsControlExpired = value; }
        }
        [Column(Name = "IsProductionItem", DataType = "Boolean")]
        public Boolean IsProductionItem
        {
            get { return _IsProductionItem; }
            set { _IsProductionItem = value; }
        }
        [Column(Name = "IsUsingStandardPrice", DataType = "Boolean")]
        public Boolean IsUsingStandardPrice
        {
            get { return _IsUsingStandardPrice; }
            set { _IsUsingStandardPrice = value; }
        }
        [Column(Name = "GCABCClass", DataType = "String")]
        public String GCABCClass
        {
            get { return _GCABCClass; }
            set { _GCABCClass = value; }
        }
        [Column(Name = "ABCClass", DataType = "String")]
        public String ABCClass
        {
            get { return _ABCClass; }
            set { _ABCClass = value; }
        }
        [Column(Name = "CycleCountInterval", DataType = "Decimal")]
        public Decimal CycleCountInterval
        {
            get { return _CycleCountInterval; }
            set { _CycleCountInterval = value; }
        }
        [Column(Name = "HETAmount", DataType = "Decimal")]
        public Decimal HETAmount
        {
            get { return _HETAmount; }
            set { _HETAmount = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vItemRequestDt
    [Serializable]
    [Table(Name = "vItemRequestDt")]
    public partial class vItemRequestDt
    {
        private Int32 _ID;
        private Int32 _ItemRequestID;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private String _ItemName2;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Decimal _Quantity;
        private String _GCItemUnit;
        private String _ItemUnit;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private Decimal _ConversionFactor;
        private Decimal _DistributionQty;
        private Decimal _ConsumptionQty;
        private Decimal _PurchaseRequestQty;
        private Decimal _PurchaseRequestReceivedQty;
        private String _GCItemDetailStatus;
        private Int32 _ToLocationID;
        private Decimal _EndingBalance;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "ItemRequestID", DataType = "Int32")]
        public Int32 ItemRequestID
        {
            get { return _ItemRequestID; }
            set { _ItemRequestID = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemName2", DataType = "String")]
        public String ItemName2
        {
            get { return _ItemName2; }
            set { _ItemName2 = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "DistributionQty", DataType = "Decimal")]
        public Decimal DistributionQty
        {
            get { return _DistributionQty; }
            set { _DistributionQty = value; }
        }
        [Column(Name = "ConsumptionQty", DataType = "Decimal")]
        public Decimal ConsumptionQty
        {
            get { return _ConsumptionQty; }
            set { _ConsumptionQty = value; }
        }
        [Column(Name = "PurchaseRequestQty", DataType = "Decimal")]
        public Decimal PurchaseRequestQty
        {
            get { return _PurchaseRequestQty; }
            set { _PurchaseRequestQty = value; }
        }
        [Column(Name = "PurchaseRequestReceivedQty", DataType = "Decimal")]
        public Decimal PurchaseRequestReceivedQty
        {
            get { return _PurchaseRequestReceivedQty; }
            set { _PurchaseRequestReceivedQty = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
        [Column(Name = "ToLocationID", DataType = "Int32")]
        public Int32 ToLocationID
        {
            get { return _ToLocationID; }
            set { _ToLocationID = value; }
        }
        [Column(Name = "EndingBalance", DataType = "Decimal")]
        public Decimal EndingBalance
        {
            get { return _EndingBalance; }
            set { _EndingBalance = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vItemRequestDtRealizationPerItem
    [Serializable]
    [Table(Name = "vItemRequestDtRealizationPerItem")]
    public class vItemRequestDtRealizationPerItem
    {
        private Int32 _ItemID;
        private Decimal _ItemRequestQuantity;
        private Decimal _PurchaseRequestOrderQty;
        private Decimal _PurchaseRequestReceivedQty;

        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemRequestQuantity", DataType = "Decimal")]
        public Decimal ItemRequestQuantity
        {
            get { return _ItemRequestQuantity; }
            set { _ItemRequestQuantity = value; }
        }
        [Column(Name = "PurchaseRequestOrderQty", DataType = "Decimal")]
        public Decimal PurchaseRequestOrderQty
        {
            get { return _PurchaseRequestOrderQty; }
            set { _PurchaseRequestOrderQty = value; }
        }
        [Column(Name = "PurchaseRequestReceivedQty", DataType = "Decimal")]
        public Decimal PurchaseRequestReceivedQty
        {
            get { return _PurchaseRequestReceivedQty; }
            set { _PurchaseRequestReceivedQty = value; }
        }
    }
    #endregion
    #region vItemRequestDtRealizationPerItemPerOrder
    [Serializable]
    [Table(Name = "vItemRequestDtRealizationPerItemPerOrder")]
    public class vItemRequestDtRealizationPerItemPerOrder
    {
        private Int32 _ItemRequestID;
        private String _ItemRequestNo;
        private Int32 _ItemID;
        private String _ItemUnit;
        private Decimal _ItemRequestQuantity;
        private Decimal _PurchaseRequestOrderQty;
        private Decimal _PurchaseRequestReceivedQty;

        [Column(Name = "ItemRequestID", DataType = "Int32")]
        public Int32 ItemRequestID
        {
            get { return _ItemRequestID; }
            set { _ItemRequestID = value; }
        }
        [Column(Name = "ItemRequestNo", DataType = "String")]
        public String ItemRequestNo
        {
            get { return _ItemRequestNo; }
            set { _ItemRequestNo = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "ItemRequestQuantity", DataType = "Decimal")]
        public Decimal ItemRequestQuantity
        {
            get { return _ItemRequestQuantity; }
            set { _ItemRequestQuantity = value; }
        }
        [Column(Name = "PurchaseRequestOrderQty", DataType = "Decimal")]
        public Decimal PurchaseRequestOrderQty
        {
            get { return _PurchaseRequestOrderQty; }
            set { _PurchaseRequestOrderQty = value; }
        }
        [Column(Name = "PurchaseRequestReceivedQty", DataType = "Decimal")]
        public Decimal PurchaseRequestReceivedQty
        {
            get { return _PurchaseRequestReceivedQty; }
            set { _PurchaseRequestReceivedQty = value; }
        }
    }
    #endregion
    #region vItemRequestHd
    [Serializable]
    [Table(Name = "vItemRequestHd")]
    public partial class vItemRequestHd
    {
        private Int32 _ItemRequestID;
        private DateTime _TransactionDate;
        private String _TransactionTime;
        private String _ItemRequestNo;
        private Int32 _FromLocationID;
        private String _FromLocationCode;
        private String _FromLocationName;
        private Int32 _FromLocationItemGroupID;
        private Int32 _ToLocationID;
        private String _ToLocationCode;
        private String _ToLocationName;
        private Int32 _ToLocationItemGroupID;
        private String _Remarks;
        private String _GCTransactionStatus;
        private String _TransactionStatusWatermark;
        private Int32 _NumberOfItems;
        private Int32 _NumberOfApprovedItems;
        private String _CreatedByName;

        [Column(Name = "ItemRequestID", DataType = "Int32")]
        public Int32 ItemRequestID
        {
            get { return _ItemRequestID; }
            set { _ItemRequestID = value; }
        }
        [Column(Name = "TransactionDate", DataType = "DateTime")]
        public DateTime TransactionDate
        {
            get { return _TransactionDate; }
            set { _TransactionDate = value; }
        }
        [Column(Name = "TransactionTime", DataType = "String")]
        public String TransactionTime
        {
            get { return _TransactionTime; }
            set { _TransactionTime = value; }
        }
        [Column(Name = "ItemRequestNo", DataType = "String")]
        public String ItemRequestNo
        {
            get { return _ItemRequestNo; }
            set { _ItemRequestNo = value; }
        }
        [Column(Name = "FromLocationID", DataType = "Int32")]
        public Int32 FromLocationID
        {
            get { return _FromLocationID; }
            set { _FromLocationID = value; }
        }
        [Column(Name = "FromLocationCode", DataType = "String")]
        public String FromLocationCode
        {
            get { return _FromLocationCode; }
            set { _FromLocationCode = value; }
        }
        [Column(Name = "FromLocationName", DataType = "String")]
        public String FromLocationName
        {
            get { return _FromLocationName; }
            set { _FromLocationName = value; }
        }
        [Column(Name = "FromLocationItemGroupID", DataType = "Int32")]
        public Int32 FromLocationItemGroupID
        {
            get { return _FromLocationItemGroupID; }
            set { _FromLocationItemGroupID = value; }
        }
        [Column(Name = "ToLocationID", DataType = "Int32")]
        public Int32 ToLocationID
        {
            get { return _ToLocationID; }
            set { _ToLocationID = value; }
        }
        [Column(Name = "ToLocationCode", DataType = "String")]
        public String ToLocationCode
        {
            get { return _ToLocationCode; }
            set { _ToLocationCode = value; }
        }
        [Column(Name = "ToLocationName", DataType = "String")]
        public String ToLocationName
        {
            get { return _ToLocationName; }
            set { _ToLocationName = value; }
        }
        [Column(Name = "ToLocationItemGroupID", DataType = "Int32")]
        public Int32 ToLocationItemGroupID
        {
            get { return _ToLocationItemGroupID; }
            set { _ToLocationItemGroupID = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
        [Column(Name = "NumberOfItems", DataType = "Int32")]
        public Int32 NumberOfItems
        {
            get { return _NumberOfItems; }
            set { _NumberOfItems = value; }
        }
        [Column(Name = "NumberOfApprovedItems", DataType = "Int32")]
        public Int32 NumberOfApprovedItems
        {
            get { return _NumberOfApprovedItems; }
            set { _NumberOfApprovedItems = value; }
        }
        [Column(Name = "CreatedByName", DataType = "String")]
        public String CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }
    }
    #endregion
    #region vItemTariff
    [Serializable]
    [Table(Name = "vItemTariff")]
    public class vItemTariff
    {
        private Int32 _ItemID;
        private DateTime _StartingDate;
        private String _ItemName1;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private String _GCTariffScheme;
        private String _GCItemType;
        private Decimal _Tariff;

        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "StartingDate", DataType = "DateTime")]
        public DateTime StartingDate
        {
            get { return _StartingDate; }
            set { _StartingDate = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "GCTariffScheme", DataType = "String")]
        public String GCTariffScheme
        {
            get { return _GCTariffScheme; }
            set { _GCTariffScheme = value; }
        }
        [Column(Name = "GCItemType", DataType = "String")]
        public String GCItemType
        {
            get { return _GCItemType; }
            set { _GCItemType = value; }
        }
        [Column(Name = "Tariff", DataType = "Decimal")]
        public Decimal Tariff
        {
            get { return _Tariff; }
            set { _Tariff = value; }
        }
    }
    #endregion
    #region vItemTariffCustom
    [Serializable]
    [Table(Name = "vItemTariffCustom")]
    public class vItemTariffCustom
    {
        private Int32 _ItemID;
        private String _ItemName1;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private String _GCTariffScheme;
        private String _GCItemType;
        private Decimal _Tariff;

        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "GCTariffScheme", DataType = "String")]
        public String GCTariffScheme
        {
            get { return _GCTariffScheme; }
            set { _GCTariffScheme = value; }
        }
        [Column(Name = "GCItemType", DataType = "String")]
        public String GCItemType
        {
            get { return _GCItemType; }
            set { _GCItemType = value; }
        }
        [Column(Name = "Tariff", DataType = "Decimal")]
        public Decimal Tariff
        {
            get { return _Tariff; }
            set { _Tariff = value; }
        }
    }
    #endregion
    #region vItemTransactionDt
    [Serializable]
    [Table(Name = "vItemTransactionDt")]
    public partial class vItemTransactionDt
    {
        private Int32 _ID;
        private Int32 _TransactionID;
        private String _TransactionCode;
        private String _TransactionNo;
        private DateTime _TransactionDate;
        private Int32 _FromLocationID;
        private String _FromLocationCode;
        private String _FromLocationName;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private Decimal _Quantity;
        private String _GCItemUnit;
        private String _ItemUnit;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private Decimal _ConversionFactor;
        private Decimal _BaseQuantity;
        private Decimal _CostAmount;
        private Boolean _IsControlExpired;
        private String _GCAdjustmentReason;
        private String _AdjustmentReason;
        private String _Remarks;
        private String _GCItemDetailStatus;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "TransactionID", DataType = "Int32")]
        public Int32 TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }
        [Column(Name = "TransactionCode", DataType = "String")]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
        [Column(Name = "TransactionNo", DataType = "String")]
        public String TransactionNo
        {
            get { return _TransactionNo; }
            set { _TransactionNo = value; }
        }
        [Column(Name = "TransactionDate", DataType = "DateTime")]
        public DateTime TransactionDate
        {
            get { return _TransactionDate; }
            set { _TransactionDate = value; }
        }
        [Column(Name = "FromLocationID", DataType = "Int32")]
        public Int32 FromLocationID
        {
            get { return _FromLocationID; }
            set { _FromLocationID = value; }
        }
        [Column(Name = "FromLocationCode", DataType = "String")]
        public String FromLocationCode
        {
            get { return _FromLocationCode; }
            set { _FromLocationCode = value; }
        }
        [Column(Name = "FromLocationName", DataType = "String")]
        public String FromLocationName
        {
            get { return _FromLocationName; }
            set { _FromLocationName = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "BaseQuantity", DataType = "Decimal")]
        public Decimal BaseQuantity
        {
            get { return _BaseQuantity; }
            set { _BaseQuantity = value; }
        }
        [Column(Name = "CostAmount", DataType = "Decimal")]
        public Decimal CostAmount
        {
            get { return _CostAmount; }
            set { _CostAmount = value; }
        }
        [Column(Name = "IsControlExpired", DataType = "Boolean")]
        public Boolean IsControlExpired
        {
            get { return _IsControlExpired; }
            set { _IsControlExpired = value; }
        }
        [Column(Name = "GCAdjustmentReason", DataType = "String")]
        public String GCAdjustmentReason
        {
            get { return _GCAdjustmentReason; }
            set { _GCAdjustmentReason = value; }
        }
        [Column(Name = "AdjustmentReason", DataType = "String")]
        public String AdjustmentReason
        {
            get { return _AdjustmentReason; }
            set { _AdjustmentReason = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
    }
    #endregion
    #region vItemTransactionHd
    [Serializable]
    [Table(Name = "vItemTransactionHd")]
    public partial class vItemTransactionHd
    {
        private Int32 _TransactionID;
        private String _TransactionCode;
        private DateTime _TransactionDate;
        private String _TransactionNo;
        private Int32 _FromLocationID;
        private String _FromLocationCode;
        private String _FromLocationName;
        private Int32 _ToLocationID;
        private String _ToLocationCode;
        private String _ToLocationName;
        private String _GCAdjustmentType;
        private String _AdjustmentType;
        private String _GCConsumptionType;
        private String _ConsumptionType;
        private Boolean _IsBySystem;
        private String _Remarks;
        private String _GCTransactionStatus;
        private String _TransactionStatusWatermark;
        private Int32 _NumberOfItems;
        private Int32 _NumberOfApprovedItems;
        private String _CreatedByName;

        [Column(Name = "TransactionID", DataType = "Int32")]
        public Int32 TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }
        [Column(Name = "TransactionCode", DataType = "String")]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
        [Column(Name = "TransactionDate", DataType = "DateTime")]
        public DateTime TransactionDate
        {
            get { return _TransactionDate; }
            set { _TransactionDate = value; }
        }
        [Column(Name = "TransactionNo", DataType = "String")]
        public String TransactionNo
        {
            get { return _TransactionNo; }
            set { _TransactionNo = value; }
        }
        [Column(Name = "FromLocationID", DataType = "Int32")]
        public Int32 FromLocationID
        {
            get { return _FromLocationID; }
            set { _FromLocationID = value; }
        }
        [Column(Name = "FromLocationCode", DataType = "String")]
        public String FromLocationCode
        {
            get { return _FromLocationCode; }
            set { _FromLocationCode = value; }
        }
        [Column(Name = "FromLocationName", DataType = "String")]
        public String FromLocationName
        {
            get { return _FromLocationName; }
            set { _FromLocationName = value; }
        }
        [Column(Name = "ToLocationID", DataType = "Int32")]
        public Int32 ToLocationID
        {
            get { return _ToLocationID; }
            set { _ToLocationID = value; }
        }
        [Column(Name = "ToLocationCode", DataType = "String")]
        public String ToLocationCode
        {
            get { return _ToLocationCode; }
            set { _ToLocationCode = value; }
        }
        [Column(Name = "ToLocationName", DataType = "String")]
        public String ToLocationName
        {
            get { return _ToLocationName; }
            set { _ToLocationName = value; }
        }
        [Column(Name = "GCAdjustmentType", DataType = "String")]
        public String GCAdjustmentType
        {
            get { return _GCAdjustmentType; }
            set { _GCAdjustmentType = value; }
        }
        [Column(Name = "AdjustmentType", DataType = "String")]
        public String AdjustmentType
        {
            get { return _AdjustmentType; }
            set { _AdjustmentType = value; }
        }
        [Column(Name = "GCConsumptionType", DataType = "String")]
        public String GCConsumptionType
        {
            get { return _GCConsumptionType; }
            set { _GCConsumptionType = value; }
        }
        [Column(Name = "ConsumptionType", DataType = "String")]
        public String ConsumptionType
        {
            get { return _ConsumptionType; }
            set { _ConsumptionType = value; }
        }
        [Column(Name = "IsBySystem", DataType = "Boolean")]
        public Boolean IsBySystem
        {
            get { return _IsBySystem; }
            set { _IsBySystem = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
        [Column(Name = "NumberOfItems", DataType = "Int32")]
        public Int32 NumberOfItems
        {
            get { return _NumberOfItems; }
            set { _NumberOfItems = value; }
        }
        [Column(Name = "NumberOfApprovedItems", DataType = "Int32")]
        public Int32 NumberOfApprovedItems
        {
            get { return _NumberOfApprovedItems; }
            set { _NumberOfApprovedItems = value; }
        }
        [Column(Name = "CreatedByName", DataType = "String")]
        public String CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }
    }
    #endregion
    #region vJournalTemplateDt
    [Serializable]
    [Table(Name = "vJournalTemplateDt")]
    public partial class vJournalTemplateDt
    {
        private Int32 _ID;
        private Int32 _TemplateID;
        private String _TemplateCode;
        private String _TemplateName;
        private Int32 _GLAccountID;
        private String _GLAccountNo;
        private String _GLAccountName;
        private Int32 _SubLedgerID;
        private String _SearchDialogTypeName;
        private String _IDFieldName;
        private String _CodeFieldName;
        private String _DisplayFieldName;
        private String _MethodName;
        private String _FilterExpression;
        private Int32 _SubLedgerDtID;
        private String _SubLedgerDtCode;
        private String _SubLedgerDtName;
        private Decimal _AmountPercentage;
        private String _Position;
        private Int16 _DisplayOrder;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "TemplateID", DataType = "Int32")]
        public Int32 TemplateID
        {
            get { return _TemplateID; }
            set { _TemplateID = value; }
        }
        [Column(Name = "TemplateCode", DataType = "String")]
        public String TemplateCode
        {
            get { return _TemplateCode; }
            set { _TemplateCode = value; }
        }
        [Column(Name = "TemplateName", DataType = "String")]
        public String TemplateName
        {
            get { return _TemplateName; }
            set { _TemplateName = value; }
        }
        [Column(Name = "GLAccountID", DataType = "Int32")]
        public Int32 GLAccountID
        {
            get { return _GLAccountID; }
            set { _GLAccountID = value; }
        }
        [Column(Name = "GLAccountNo", DataType = "String")]
        public String GLAccountNo
        {
            get { return _GLAccountNo; }
            set { _GLAccountNo = value; }
        }
        [Column(Name = "GLAccountName", DataType = "String")]
        public String GLAccountName
        {
            get { return _GLAccountName; }
            set { _GLAccountName = value; }
        }
        [Column(Name = "SubLedgerID", DataType = "Int32")]
        public Int32 SubLedgerID
        {
            get { return _SubLedgerID; }
            set { _SubLedgerID = value; }
        }
        [Column(Name = "SearchDialogTypeName", DataType = "String")]
        public String SearchDialogTypeName
        {
            get { return _SearchDialogTypeName; }
            set { _SearchDialogTypeName = value; }
        }
        [Column(Name = "IDFieldName", DataType = "String")]
        public String IDFieldName
        {
            get { return _IDFieldName; }
            set { _IDFieldName = value; }
        }
        [Column(Name = "CodeFieldName", DataType = "String")]
        public String CodeFieldName
        {
            get { return _CodeFieldName; }
            set { _CodeFieldName = value; }
        }
        [Column(Name = "DisplayFieldName", DataType = "String")]
        public String DisplayFieldName
        {
            get { return _DisplayFieldName; }
            set { _DisplayFieldName = value; }
        }
        [Column(Name = "MethodName", DataType = "String")]
        public String MethodName
        {
            get { return _MethodName; }
            set { _MethodName = value; }
        }
        [Column(Name = "FilterExpression", DataType = "String")]
        public String FilterExpression
        {
            get { return _FilterExpression; }
            set { _FilterExpression = value; }
        }
        [Column(Name = "SubLedgerDtID", DataType = "Int32")]
        public Int32 SubLedgerDtID
        {
            get { return _SubLedgerDtID; }
            set { _SubLedgerDtID = value; }
        }
        [Column(Name = "SubLedgerDtCode", DataType = "String")]
        public String SubLedgerDtCode
        {
            get { return _SubLedgerDtCode; }
            set { _SubLedgerDtCode = value; }
        }
        [Column(Name = "SubLedgerDtName", DataType = "String")]
        public String SubLedgerDtName
        {
            get { return _SubLedgerDtName; }
            set { _SubLedgerDtName = value; }
        }
        [Column(Name = "AmountPercentage", DataType = "Decimal")]
        public Decimal AmountPercentage
        {
            get { return _AmountPercentage; }
            set { _AmountPercentage = value; }
        }
        [Column(Name = "Position", DataType = "String")]
        public String Position
        {
            get { return _Position; }
            set { _Position = value; }
        }
        [Column(Name = "DisplayOrder", DataType = "Int16")]
        public Int16 DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vJournalTemplateHd
    [Serializable]
    [Table(Name = "vJournalTemplateHd")]
    public partial class vJournalTemplateHd
    {
        private Int32 _TemplateID;
        private String _TemplateCode;
        private String _TemplateName;
        private Decimal _TotalDebit;
        private Decimal _TotalKredit;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "TemplateID", DataType = "Int32")]
        public Int32 TemplateID
        {
            get { return _TemplateID; }
            set { _TemplateID = value; }
        }
        [Column(Name = "TemplateCode", DataType = "String")]
        public String TemplateCode
        {
            get { return _TemplateCode; }
            set { _TemplateCode = value; }
        }
        [Column(Name = "TemplateName", DataType = "String")]
        public String TemplateName
        {
            get { return _TemplateName; }
            set { _TemplateName = value; }
        }
        [Column(Name = "TotalDebit", DataType = "Decimal")]
        public Decimal TotalDebit
        {
            get { return _TotalDebit; }
            set { _TotalDebit = value; }
        }
        [Column(Name = "TotalKredit", DataType = "Decimal")]
        public Decimal TotalKredit
        {
            get { return _TotalKredit; }
            set { _TotalKredit = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vLocation
    [Serializable]
    [Table(Name = "vLocation")]
    public class vLocation
    {
        private Int32 _LocationID;
        private String _SiteID;
        private String _LocationCode;
        private String _LocationName;
        private String _ShortName;
        private Int32 _ParentID;
        private String _ParentCode;
        private String _ParentName;
        private String _GCItemType;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Int32 _RestrictionID;
        private String _RestrictionCode;
        private String _RestrictionName;
        private Boolean _IsHeader;
        private Boolean _IsHasChildren;
        private Boolean _IsAvailable;
        private Boolean _IsNettable;
        private Boolean _IsAllowOverIssued;
        private Boolean _IsHoldForTransaction;
        private Boolean _IsDeleted;
        private Int32 _Level;

        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "LocationCode", DataType = "String")]
        public String LocationCode
        {
            get { return _LocationCode; }
            set { _LocationCode = value; }
        }
        [Column(Name = "LocationName", DataType = "String")]
        public String LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }
        [Column(Name = "ShortName", DataType = "String")]
        public String ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }
        [Column(Name = "ParentID", DataType = "Int32")]
        public Int32 ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        [Column(Name = "ParentCode", DataType = "String")]
        public String ParentCode
        {
            get { return _ParentCode; }
            set { _ParentCode = value; }
        }
        [Column(Name = "ParentName", DataType = "String")]
        public String ParentName
        {
            get { return _ParentName; }
            set { _ParentName = value; }
        }
        [Column(Name = "GCItemType", DataType = "String")]
        public String GCItemType
        {
            get { return _GCItemType; }
            set { _GCItemType = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "RestrictionID", DataType = "Int32")]
        public Int32 RestrictionID
        {
            get { return _RestrictionID; }
            set { _RestrictionID = value; }
        }
        [Column(Name = "RestrictionCode", DataType = "String")]
        public String RestrictionCode
        {
            get { return _RestrictionCode; }
            set { _RestrictionCode = value; }
        }
        [Column(Name = "RestrictionName", DataType = "String")]
        public String RestrictionName
        {
            get { return _RestrictionName; }
            set { _RestrictionName = value; }
        }
        [Column(Name = "IsHeader", DataType = "Boolean")]
        public Boolean IsHeader
        {
            get { return _IsHeader; }
            set { _IsHeader = value; }
        }
        [Column(Name = "IsHasChildren", DataType = "Boolean")]
        public Boolean IsHasChildren
        {
            get { return _IsHasChildren; }
            set { _IsHasChildren = value; }
        }
        [Column(Name = "IsAvailable", DataType = "Boolean")]
        public Boolean IsAvailable
        {
            get { return _IsAvailable; }
            set { _IsAvailable = value; }
        }
        [Column(Name = "IsNettable", DataType = "Boolean")]
        public Boolean IsNettable
        {
            get { return _IsNettable; }
            set { _IsNettable = value; }
        }
        [Column(Name = "IsAllowOverIssued", DataType = "Boolean")]
        public Boolean IsAllowOverIssued
        {
            get { return _IsAllowOverIssued; }
            set { _IsAllowOverIssued = value; }
        }
        [Column(Name = "IsHoldForTransaction", DataType = "Boolean")]
        public Boolean IsHoldForTransaction
        {
            get { return _IsHoldForTransaction; }
            set { _IsHoldForTransaction = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "Level", DataType = "Int32")]
        public Int32 Level
        {
            get { return _Level; }
            set { _Level = value; }
        }
    }
    #endregion
    #region vOrganizationDt
    [Serializable]
    [Table(Name = "vOrganizationDt")]
    public class vOrganizationDt
    {
        private Int32 _OrganizationDtID;
        private Int32 _OrganizationID;
        private String _OrganizationName;
        private Int32 _SchoolPeriodID;
        private String _Position;
        private Int16 _DisplayOrder;
        private Int32 _StudentCoordinatorID;
        private String _StudentCoordinatorCode;
        private String _StudentCoordinatorName;
        private String _ListStudentID;
        private String _ListStudentName;
        private Boolean _IsDeleted;

        [Column(Name = "OrganizationDtID", DataType = "Int32")]
        public Int32 OrganizationDtID
        {
            get { return _OrganizationDtID; }
            set { _OrganizationDtID = value; }
        }
        [Column(Name = "OrganizationID", DataType = "Int32")]
        public Int32 OrganizationID
        {
            get { return _OrganizationID; }
            set { _OrganizationID = value; }
        }
        [Column(Name = "OrganizationName", DataType = "String")]
        public String OrganizationName
        {
            get { return _OrganizationName; }
            set { _OrganizationName = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "Position", DataType = "String")]
        public String Position
        {
            get { return _Position; }
            set { _Position = value; }
        }
        [Column(Name = "DisplayOrder", DataType = "Int16")]
        public Int16 DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        [Column(Name = "StudentCoordinatorID", DataType = "Int32")]
        public Int32 StudentCoordinatorID
        {
            get { return _StudentCoordinatorID; }
            set { _StudentCoordinatorID = value; }
        }
        [Column(Name = "StudentCoordinatorCode", DataType = "String")]
        public String StudentCoordinatorCode
        {
            get { return _StudentCoordinatorCode; }
            set { _StudentCoordinatorCode = value; }
        }
        [Column(Name = "StudentCoordinatorName", DataType = "String")]
        public String StudentCoordinatorName
        {
            get { return _StudentCoordinatorName; }
            set { _StudentCoordinatorName = value; }
        }
        [Column(Name = "ListStudentID", DataType = "String")]
        public String ListStudentID
        {
            get { return _ListStudentID; }
            set { _ListStudentID = value; }
        }
        [Column(Name = "ListStudentName", DataType = "String")]
        public String ListStudentName
        {
            get { return _ListStudentName; }
            set { _ListStudentName = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vOrganizationDtStudent
    [Serializable]
    [Table(Name = "vOrganizationDtStudent")]
    public class vOrganizationDtStudent
    {
        private Int32 _OrganizationDtID;
        private Int32 _OrganizationID;
        private String _OrganizationName;
        private Int32 _SchoolPeriodID;
        private String _Position;
        private Int32 _StudentID;

        [Column(Name = "OrganizationDtID", DataType = "Int32")]
        public Int32 OrganizationDtID
        {
            get { return _OrganizationDtID; }
            set { _OrganizationDtID = value; }
        }
        [Column(Name = "OrganizationID", DataType = "Int32")]
        public Int32 OrganizationID
        {
            get { return _OrganizationID; }
            set { _OrganizationID = value; }
        }
        [Column(Name = "OrganizationName", DataType = "String")]
        public String OrganizationName
        {
            get { return _OrganizationName; }
            set { _OrganizationName = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "Position", DataType = "String")]
        public String Position
        {
            get { return _Position; }
            set { _Position = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
    }
    #endregion
    #region vPeriodAdmission
    [Serializable]
    [Table(Name = "vPeriodAdmission")]
    public partial class vPeriodAdmission
    {
        private Int32 _PeriodAdmissionID;
        private String _PeriodAdmissionCode;
        private String _PeriodAdmissionName;
        private String _Initial;
        private Int32 _SchoolPeriodID;
        private String _SchoolPeriodName;
        private DateTime _RegistrationStartDate;
        private DateTime _RegistrationEndDate;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private String _GCPeriodAdmissionType;
        private String _GCPeriodAdmissionStatus;
        private String _PeriodAdmissionStatus;
        private String _Remarks;

        [Column(Name = "PeriodAdmissionID", DataType = "Int32")]
        public Int32 PeriodAdmissionID
        {
            get { return _PeriodAdmissionID; }
            set { _PeriodAdmissionID = value; }
        }
        [Column(Name = "PeriodAdmissionCode", DataType = "String")]
        public String PeriodAdmissionCode
        {
            get { return _PeriodAdmissionCode; }
            set { _PeriodAdmissionCode = value; }
        }
        [Column(Name = "PeriodAdmissionName", DataType = "String")]
        public String PeriodAdmissionName
        {
            get { return _PeriodAdmissionName; }
            set { _PeriodAdmissionName = value; }
        }
        [Column(Name = "Initial", DataType = "String")]
        public String Initial
        {
            get { return _Initial; }
            set { _Initial = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "SchoolPeriodName", DataType = "String")]
        public String SchoolPeriodName
        {
            get { return _SchoolPeriodName; }
            set { _SchoolPeriodName = value; }
        }
        [Column(Name = "RegistrationStartDate", DataType = "DateTime")]
        public DateTime RegistrationStartDate
        {
            get { return _RegistrationStartDate; }
            set { _RegistrationStartDate = value; }
        }
        [Column(Name = "RegistrationEndDate", DataType = "DateTime")]
        public DateTime RegistrationEndDate
        {
            get { return _RegistrationEndDate; }
            set { _RegistrationEndDate = value; }
        }
        [Column(Name = "StartDate", DataType = "DateTime")]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Column(Name = "EndDate", DataType = "DateTime")]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        [Column(Name = "GCPeriodAdmissionType", DataType = "String")]
        public String GCPeriodAdmissionType
        {
            get { return _GCPeriodAdmissionType; }
            set { _GCPeriodAdmissionType = value; }
        }
        [Column(Name = "GCPeriodAdmissionStatus", DataType = "String")]
        public String GCPeriodAdmissionStatus
        {
            get { return _GCPeriodAdmissionStatus; }
            set { _GCPeriodAdmissionStatus = value; }
        }
        [Column(Name = "PeriodAdmissionStatus", DataType = "String")]
        public String PeriodAdmissionStatus
        {
            get { return _PeriodAdmissionStatus; }
            set { _PeriodAdmissionStatus = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
    }
    #endregion
    #region vPeriodClassType
    [Serializable]
    [Table(Name = "vPeriodClassType")]
    public partial class vPeriodClassType
    {
        private Int32 _PeriodClassTypeID;
        private Int32 _SchoolPeriodID;
        private String _SchoolPeriodName;
        private Int32 _PeriodSectionID;
        private String _PeriodSectionName;
        private Int32 _ClassTypeID;
        private String _ClassTypeCode;
        private String _ClassTypeName;
        private String _GCClassStudyType;
        private String _GCGrade;
        private String _Grade;
        private String _GCMajor;
        private String _Major;
        private Int32 _DailySchedulePackageID;
        private String _DailySchedulePackageName;
        private Int32 _TheoryFinalMarkFormulaID;
        private Int32 _PracticeFinalMarkFormulaID;
        private Int32 _GradePromotionFormulaID;
        private Int16 _NoOfClass;
        private Int32 _CreatedClass;
        private Boolean _IsDeleted;

        [Column(Name = "PeriodClassTypeID", DataType = "Int32")]
        public Int32 PeriodClassTypeID
        {
            get { return _PeriodClassTypeID; }
            set { _PeriodClassTypeID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "SchoolPeriodName", DataType = "String")]
        public String SchoolPeriodName
        {
            get { return _SchoolPeriodName; }
            set { _SchoolPeriodName = value; }
        }
        [Column(Name = "PeriodSectionID", DataType = "Int32")]
        public Int32 PeriodSectionID
        {
            get { return _PeriodSectionID; }
            set { _PeriodSectionID = value; }
        }
        [Column(Name = "PeriodSectionName", DataType = "String")]
        public String PeriodSectionName
        {
            get { return _PeriodSectionName; }
            set { _PeriodSectionName = value; }
        }
        [Column(Name = "ClassTypeID", DataType = "Int32")]
        public Int32 ClassTypeID
        {
            get { return _ClassTypeID; }
            set { _ClassTypeID = value; }
        }
        [Column(Name = "ClassTypeCode", DataType = "String")]
        public String ClassTypeCode
        {
            get { return _ClassTypeCode; }
            set { _ClassTypeCode = value; }
        }
        [Column(Name = "ClassTypeName", DataType = "String")]
        public String ClassTypeName
        {
            get { return _ClassTypeName; }
            set { _ClassTypeName = value; }
        }
        [Column(Name = "GCClassStudyType", DataType = "String")]
        public String GCClassStudyType
        {
            get { return _GCClassStudyType; }
            set { _GCClassStudyType = value; }
        }
        [Column(Name = "GCGrade", DataType = "String")]
        public String GCGrade
        {
            get { return _GCGrade; }
            set { _GCGrade = value; }
        }
        [Column(Name = "Grade", DataType = "String")]
        public String Grade
        {
            get { return _Grade; }
            set { _Grade = value; }
        }
        [Column(Name = "GCMajor", DataType = "String")]
        public String GCMajor
        {
            get { return _GCMajor; }
            set { _GCMajor = value; }
        }
        [Column(Name = "Major", DataType = "String")]
        public String Major
        {
            get { return _Major; }
            set { _Major = value; }
        }
        [Column(Name = "DailySchedulePackageID", DataType = "Int32")]
        public Int32 DailySchedulePackageID
        {
            get { return _DailySchedulePackageID; }
            set { _DailySchedulePackageID = value; }
        }
        [Column(Name = "DailySchedulePackageName", DataType = "String")]
        public String DailySchedulePackageName
        {
            get { return _DailySchedulePackageName; }
            set { _DailySchedulePackageName = value; }
        }
        [Column(Name = "TheoryFinalMarkFormulaID", DataType = "Int32")]
        public Int32 TheoryFinalMarkFormulaID
        {
            get { return _TheoryFinalMarkFormulaID; }
            set { _TheoryFinalMarkFormulaID = value; }
        }
        [Column(Name = "PracticeFinalMarkFormulaID", DataType = "Int32")]
        public Int32 PracticeFinalMarkFormulaID
        {
            get { return _PracticeFinalMarkFormulaID; }
            set { _PracticeFinalMarkFormulaID = value; }
        }
        [Column(Name = "GradePromotionFormulaID", DataType = "Int32")]
        public Int32 GradePromotionFormulaID
        {
            get { return _GradePromotionFormulaID; }
            set { _GradePromotionFormulaID = value; }
        }
        [Column(Name = "NoOfClass", DataType = "Int16")]
        public Int16 NoOfClass
        {
            get { return _NoOfClass; }
            set { _NoOfClass = value; }
        }
        [Column(Name = "CreatedClass", DataType = "Int32")]
        public Int32 CreatedClass
        {
            get { return _CreatedClass; }
            set { _CreatedClass = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vPeriodClassTypeSubject
    [Serializable]
    [Table(Name = "vPeriodClassTypeSubject")]
    public class vPeriodClassTypeSubject
    {
        private Int32 _PeriodClassTypeSubjectID;
        private Int32 _PeriodClassTypeID;
        private Int32 _SchoolPeriodID;
        private String _SchoolPeriodName;
        private Int32 _PeriodSectionID;
        private String _PeriodSectionName;
        private Int32 _ClassTypeID;
        private String _ClassTypeName;
        private Int32 _SubjectID;
        private String _SubjectCode;
        private String _SubjectName;
        private String _GCClassStudyType;
        private Int32 _SubjectMatterID;
        private String _SubjectMatterCode;
        private String _SubjectMatterName;
        private String _GCSubjectType;
        private String _SubjectType;
        private Int32 _TeacherID;
        private String _TeacherCode;
        private String _TeacherName;
        private Int16 _NoMeetingHoursInWeek;
        private Int16 _PassingGrade;
        private Int32 _TheoryFinalMarkFormulaID;
        private Int32 _PracticeFinalMarkFormulaID;
        private Boolean _IsEditable;
        private Boolean _IsDeleted;

        [Column(Name = "PeriodClassTypeSubjectID", DataType = "Int32")]
        public Int32 PeriodClassTypeSubjectID
        {
            get { return _PeriodClassTypeSubjectID; }
            set { _PeriodClassTypeSubjectID = value; }
        }
        [Column(Name = "PeriodClassTypeID", DataType = "Int32")]
        public Int32 PeriodClassTypeID
        {
            get { return _PeriodClassTypeID; }
            set { _PeriodClassTypeID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "SchoolPeriodName", DataType = "String")]
        public String SchoolPeriodName
        {
            get { return _SchoolPeriodName; }
            set { _SchoolPeriodName = value; }
        }
        [Column(Name = "PeriodSectionID", DataType = "Int32")]
        public Int32 PeriodSectionID
        {
            get { return _PeriodSectionID; }
            set { _PeriodSectionID = value; }
        }
        [Column(Name = "PeriodSectionName", DataType = "String")]
        public String PeriodSectionName
        {
            get { return _PeriodSectionName; }
            set { _PeriodSectionName = value; }
        }
        [Column(Name = "ClassTypeID", DataType = "Int32")]
        public Int32 ClassTypeID
        {
            get { return _ClassTypeID; }
            set { _ClassTypeID = value; }
        }
        [Column(Name = "ClassTypeName", DataType = "String")]
        public String ClassTypeName
        {
            get { return _ClassTypeName; }
            set { _ClassTypeName = value; }
        }
        [Column(Name = "SubjectID", DataType = "Int32")]
        public Int32 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
        [Column(Name = "SubjectCode", DataType = "String")]
        public String SubjectCode
        {
            get { return _SubjectCode; }
            set { _SubjectCode = value; }
        }
        [Column(Name = "SubjectName", DataType = "String")]
        public String SubjectName
        {
            get { return _SubjectName; }
            set { _SubjectName = value; }
        }
        [Column(Name = "GCClassStudyType", DataType = "String")]
        public String GCClassStudyType
        {
            get { return _GCClassStudyType; }
            set { _GCClassStudyType = value; }
        }
        [Column(Name = "SubjectMatterID", DataType = "Int32")]
        public Int32 SubjectMatterID
        {
            get { return _SubjectMatterID; }
            set { _SubjectMatterID = value; }
        }
        [Column(Name = "SubjectMatterCode", DataType = "String")]
        public String SubjectMatterCode
        {
            get { return _SubjectMatterCode; }
            set { _SubjectMatterCode = value; }
        }
        [Column(Name = "SubjectMatterName", DataType = "String")]
        public String SubjectMatterName
        {
            get { return _SubjectMatterName; }
            set { _SubjectMatterName = value; }
        }
        [Column(Name = "GCSubjectType", DataType = "String")]
        public String GCSubjectType
        {
            get { return _GCSubjectType; }
            set { _GCSubjectType = value; }
        }
        [Column(Name = "SubjectType", DataType = "String")]
        public String SubjectType
        {
            get { return _SubjectType; }
            set { _SubjectType = value; }
        }
        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
        }
        [Column(Name = "TeacherCode", DataType = "String")]
        public String TeacherCode
        {
            get { return _TeacherCode; }
            set { _TeacherCode = value; }
        }
        [Column(Name = "TeacherName", DataType = "String")]
        public String TeacherName
        {
            get { return _TeacherName; }
            set { _TeacherName = value; }
        }
        [Column(Name = "NoMeetingHoursInWeek", DataType = "Int16")]
        public Int16 NoMeetingHoursInWeek
        {
            get { return _NoMeetingHoursInWeek; }
            set { _NoMeetingHoursInWeek = value; }
        }
        [Column(Name = "PassingGrade", DataType = "Int16")]
        public Int16 PassingGrade
        {
            get { return _PassingGrade; }
            set { _PassingGrade = value; }
        }
        [Column(Name = "TheoryFinalMarkFormulaID", DataType = "Int32")]
        public Int32 TheoryFinalMarkFormulaID
        {
            get { return _TheoryFinalMarkFormulaID; }
            set { _TheoryFinalMarkFormulaID = value; }
        }
        [Column(Name = "PracticeFinalMarkFormulaID", DataType = "Int32")]
        public Int32 PracticeFinalMarkFormulaID
        {
            get { return _PracticeFinalMarkFormulaID; }
            set { _PracticeFinalMarkFormulaID = value; }
        }
        [Column(Name = "IsEditable", DataType = "Boolean")]
        public Boolean IsEditable
        {
            get { return _IsEditable; }
            set { _IsEditable = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vPeriodSchedule
    [Serializable]
    [Table(Name = "vPeriodSchedule")]
    public partial class vPeriodSchedule
    {
        private Int32 _PeriodScheduleID;
        private String _PeriodScheduleCode;
        private String _PeriodScheduleName;
        private Int32 _SchoolPeriodID;
        private String _GCPeriodScheduleType;
        private String _PeriodScheduleType;
        private String _GCTaskType;
        private String _TaskType;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "PeriodScheduleID", DataType = "Int32")]
        public Int32 PeriodScheduleID
        {
            get { return _PeriodScheduleID; }
            set { _PeriodScheduleID = value; }
        }
        [Column(Name = "PeriodScheduleCode", DataType = "String")]
        public String PeriodScheduleCode
        {
            get { return _PeriodScheduleCode; }
            set { _PeriodScheduleCode = value; }
        }
        [Column(Name = "PeriodScheduleName", DataType = "String")]
        public String PeriodScheduleName
        {
            get { return _PeriodScheduleName; }
            set { _PeriodScheduleName = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "GCPeriodScheduleType", DataType = "String")]
        public String GCPeriodScheduleType
        {
            get { return _GCPeriodScheduleType; }
            set { _GCPeriodScheduleType = value; }
        }
        [Column(Name = "PeriodScheduleType", DataType = "String")]
        public String PeriodScheduleType
        {
            get { return _PeriodScheduleType; }
            set { _PeriodScheduleType = value; }
        }
        [Column(Name = "GCTaskType", DataType = "String")]
        public String GCTaskType
        {
            get { return _GCTaskType; }
            set { _GCTaskType = value; }
        }
        [Column(Name = "TaskType", DataType = "String")]
        public String TaskType
        {
            get { return _TaskType; }
            set { _TaskType = value; }
        }
        [Column(Name = "StartDate", DataType = "DateTime")]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Column(Name = "EndDate", DataType = "DateTime")]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vPeriodSection
    [Serializable]
    [Table(Name = "vPeriodSection")]
    public partial class vPeriodSection
    {
        private Int32 _PeriodSectionID;
        private String _PeriodSectionCode;
        private String _GCPeriodSection;
        private String _PeriodSection;
        private String _PeriodSectionName;
        private Int32 _SchoolPeriodID;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private String _GCPeriodSectionStatus;
        private String _PeriodSectionStatus;
        private String _Remarks;

        [Column(Name = "PeriodSectionID", DataType = "Int32")]
        public Int32 PeriodSectionID
        {
            get { return _PeriodSectionID; }
            set { _PeriodSectionID = value; }
        }
        [Column(Name = "PeriodSectionCode", DataType = "String")]
        public String PeriodSectionCode
        {
            get { return _PeriodSectionCode; }
            set { _PeriodSectionCode = value; }
        }
        [Column(Name = "GCPeriodSection", DataType = "String")]
        public String GCPeriodSection
        {
            get { return _GCPeriodSection; }
            set { _GCPeriodSection = value; }
        }
        [Column(Name = "PeriodSection", DataType = "String")]
        public String PeriodSection
        {
            get { return _PeriodSection; }
            set { _PeriodSection = value; }
        }
        [Column(Name = "PeriodSectionName", DataType = "String")]
        public String PeriodSectionName
        {
            get { return _PeriodSectionName; }
            set { _PeriodSectionName = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "StartDate", DataType = "DateTime")]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Column(Name = "EndDate", DataType = "DateTime")]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        [Column(Name = "GCPeriodSectionStatus", DataType = "String")]
        public String GCPeriodSectionStatus
        {
            get { return _GCPeriodSectionStatus; }
            set { _GCPeriodSectionStatus = value; }
        }
        [Column(Name = "PeriodSectionStatus", DataType = "String")]
        public String PeriodSectionStatus
        {
            get { return _PeriodSectionStatus; }
            set { _PeriodSectionStatus = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
    }
    #endregion
    #region vProductBrand
    [Serializable]
    [Table(Name = "vProductBrand")]
    public class vProductBrand
    {
        private Int32 _ProductBrandID;
        private String _ProductBrandCode;
        private String _ProductBrandName;
        private Int32 _ManufacturerID;
        private String _ManufacturerCode;
        private String _ManufacturerName;
        private Boolean _IsDeleted;

        [Column(Name = "ProductBrandID", DataType = "Int32")]
        public Int32 ProductBrandID
        {
            get { return _ProductBrandID; }
            set { _ProductBrandID = value; }
        }
        [Column(Name = "ProductBrandCode", DataType = "String")]
        public String ProductBrandCode
        {
            get { return _ProductBrandCode; }
            set { _ProductBrandCode = value; }
        }
        [Column(Name = "ProductBrandName", DataType = "String")]
        public String ProductBrandName
        {
            get { return _ProductBrandName; }
            set { _ProductBrandName = value; }
        }
        [Column(Name = "ManufacturerID", DataType = "Int32")]
        public Int32 ManufacturerID
        {
            get { return _ManufacturerID; }
            set { _ManufacturerID = value; }
        }
        [Column(Name = "ManufacturerCode", DataType = "String")]
        public String ManufacturerCode
        {
            get { return _ManufacturerCode; }
            set { _ManufacturerCode = value; }
        }
        [Column(Name = "ManufacturerName", DataType = "String")]
        public String ManufacturerName
        {
            get { return _ManufacturerName; }
            set { _ManufacturerName = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vProductLine
    [Serializable]
    [Table(Name = "vProductLine")]
    public class vProductLine
    {
        private Int32 _ProductLineID;
        private String _ProductLineCode;
        private String _ProductLineName;
        private String _GCItemType;
        private String _ItemType;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "ProductLineID", DataType = "Int32")]
        public Int32 ProductLineID
        {
            get { return _ProductLineID; }
            set { _ProductLineID = value; }
        }
        [Column(Name = "ProductLineCode", DataType = "String")]
        public String ProductLineCode
        {
            get { return _ProductLineCode; }
            set { _ProductLineCode = value; }
        }
        [Column(Name = "ProductLineName", DataType = "String")]
        public String ProductLineName
        {
            get { return _ProductLineName; }
            set { _ProductLineName = value; }
        }
        [Column(Name = "GCItemType", DataType = "String")]
        public String GCItemType
        {
            get { return _GCItemType; }
            set { _GCItemType = value; }
        }
        [Column(Name = "ItemType", DataType = "String")]
        public String ItemType
        {
            get { return _ItemType; }
            set { _ItemType = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vProductLineDt
    [Serializable]
    [Table(Name = "vProductLineDt")]
    public class vProductLineDt
    {
        private Int32 _ProductLineID;
        private String _SiteID;
        private Int32 _Inventory;
        private String _InventoryGLAccountNo;
        private String _InventoryGLAccountName;
        private Int32 _InventorySubLedgerID;
        private String _InventorySearchDialogTypeName;
        private String _InventoryIDFieldName;
        private String _InventoryCodeFieldName;
        private String _InventoryDisplayFieldName;
        private String _InventoryMethodName;
        private String _InventoryFilterExpression;
        private Int32 _InventorySubLedger;
        private String _InventorySubLedgerCode;
        private String _InventorySubLedgerName;
        private Int32 _InventoryVAT;
        private String _InventoryVATGLAccountNo;
        private String _InventoryVATGLAccountName;
        private Int32 _InventoryVATSubLedgerID;
        private String _InventoryVATSearchDialogTypeName;
        private String _InventoryVATIDFieldName;
        private String _InventoryVATCodeFieldName;
        private String _InventoryVATDisplayFieldName;
        private String _InventoryVATMethodName;
        private String _InventoryVATFilterExpression;
        private Int32 _InventoryVATSubLedger;
        private String _InventoryVATSubLedgerCode;
        private String _InventoryVATSubLedgerName;
        private Int32 _InventoryDiscount;
        private String _InventoryDiscountGLAccountNo;
        private String _InventoryDiscountGLAccountName;
        private Int32 _InventoryDiscountSubLedgerID;
        private String _InventoryDiscountSearchDialogTypeName;
        private String _InventoryDiscountIDFieldName;
        private String _InventoryDiscountCodeFieldName;
        private String _InventoryDiscountDisplayFieldName;
        private String _InventoryDiscountMethodName;
        private String _InventoryDiscountFilterExpression;
        private Int32 _InventoryDiscountSubLedger;
        private String _InventoryDiscountSubLedgerCode;
        private String _InventoryDiscountSubLedgerName;
        private Int32 _COGS;
        private String _COGSGLAccountNo;
        private String _COGSGLAccountName;
        private Int32 _COGSSubLedgerID;
        private String _COGSSearchDialogTypeName;
        private String _COGSIDFieldName;
        private String _COGSCodeFieldName;
        private String _COGSDisplayFieldName;
        private String _COGSMethodName;
        private String _COGSFilterExpression;
        private Int32 _COGSSubLedger;
        private String _COGSSubLedgerCode;
        private String _COGSSubLedgerName;
        private Int32 _Purchase;
        private String _PurchaseGLAccountNo;
        private String _PurchaseGLAccountName;
        private Int32 _PurchaseSubLedgerID;
        private String _PurchaseSearchDialogTypeName;
        private String _PurchaseIDFieldName;
        private String _PurchaseCodeFieldName;
        private String _PurchaseDisplayFieldName;
        private String _PurchaseMethodName;
        private String _PurchaseFilterExpression;
        private Int32 _PurchaseSubLedger;
        private String _PurchaseSubLedgerCode;
        private String _PurchaseSubLedgerName;
        private Int32 _PurchaseReturn;
        private String _PurchaseReturnGLAccountNo;
        private String _PurchaseReturnGLAccountName;
        private Int32 _PurchaseReturnSubLedgerID;
        private String _PurchaseReturnSearchDialogTypeName;
        private String _PurchaseReturnIDFieldName;
        private String _PurchaseReturnCodeFieldName;
        private String _PurchaseReturnDisplayFieldName;
        private String _PurchaseReturnMethodName;
        private String _PurchaseReturnFilterExpression;
        private Int32 _PurchaseReturnSubLedger;
        private String _PurchaseReturnSubLedgerCode;
        private String _PurchaseReturnSubLedgerName;
        private Int32 _PurchaseDiscount;
        private String _PurchaseDiscountGLAccountNo;
        private String _PurchaseDiscountGLAccountName;
        private Int32 _PurchaseDiscountSubLedgerID;
        private String _PurchaseDiscountSearchDialogTypeName;
        private String _PurchaseDiscountIDFieldName;
        private String _PurchaseDiscountCodeFieldName;
        private String _PurchaseDiscountDisplayFieldName;
        private String _PurchaseDiscountMethodName;
        private String _PurchaseDiscountFilterExpression;
        private Int32 _PurchaseDiscountSubLedger;
        private String _PurchaseDiscountSubLedgerCode;
        private String _PurchaseDiscountSubLedgerName;
        private Int32 _PurchasePriceVariant;
        private String _PurchasePriceVariantGLAccountNo;
        private String _PurchasePriceVariantGLAccountName;
        private Int32 _PurchasePriceVariantSubLedgerID;
        private String _PurchasePriceVariantSearchDialogTypeName;
        private String _PurchasePriceVariantIDFieldName;
        private String _PurchasePriceVariantCodeFieldName;
        private String _PurchasePriceVariantDisplayFieldName;
        private String _PurchasePriceVariantMethodName;
        private String _PurchasePriceVariantFilterExpression;
        private Int32 _PurchasePriceVariantSubLedger;
        private String _PurchasePriceVariantSubLedgerCode;
        private String _PurchasePriceVariantSubLedgerName;
        private Int32 _Sales;
        private String _SalesGLAccountNo;
        private String _SalesGLAccountName;
        private Int32 _SalesSubLedgerID;
        private String _SalesSearchDialogTypeName;
        private String _SalesIDFieldName;
        private String _SalesCodeFieldName;
        private String _SalesDisplayFieldName;
        private String _SalesMethodName;
        private String _SalesFilterExpression;
        private Int32 _SalesSubLedger;
        private String _SalesSubLedgerCode;
        private String _SalesSubLedgerName;
        private Int32 _SalesReturn;
        private String _SalesReturnGLAccountNo;
        private String _SalesReturnGLAccountName;
        private Int32 _SalesReturnSubLedgerID;
        private String _SalesReturnSearchDialogTypeName;
        private String _SalesReturnIDFieldName;
        private String _SalesReturnCodeFieldName;
        private String _SalesReturnDisplayFieldName;
        private String _SalesReturnMethodName;
        private String _SalesReturnFilterExpression;
        private Int32 _SalesReturnSubLedger;
        private String _SalesReturnSubLedgerCode;
        private String _SalesReturnSubLedgerName;
        private Int32 _SalesDiscount;
        private String _SalesDiscountGLAccountNo;
        private String _SalesDiscountGLAccountName;
        private Int32 _SalesDiscountSubLedgerID;
        private String _SalesDiscountSearchDialogTypeName;
        private String _SalesDiscountIDFieldName;
        private String _SalesDiscountCodeFieldName;
        private String _SalesDiscountDisplayFieldName;
        private String _SalesDiscountMethodName;
        private String _SalesDiscountFilterExpression;
        private Int32 _SalesDiscountSubLedger;
        private String _SalesDiscountSubLedgerCode;
        private String _SalesDiscountSubLedgerName;
        private Int32 _MaterialRevenue;
        private String _MaterialRevenueGLAccountNo;
        private String _MaterialRevenueGLAccountName;
        private Int32 _MaterialRevenueSubLedgerID;
        private String _MaterialRevenueSearchDialogTypeName;
        private String _MaterialRevenueIDFieldName;
        private String _MaterialRevenueCodeFieldName;
        private String _MaterialRevenueDisplayFieldName;
        private String _MaterialRevenueMethodName;
        private String _MaterialRevenueFilterExpression;
        private Int32 _MaterialRevenueSubLedger;
        private String _MaterialRevenueSubLedgerCode;
        private String _MaterialRevenueSubLedgerName;
        private Int32 _Consumption;
        private String _ConsumptionGLAccountNo;
        private String _ConsumptionGLAccountName;
        private Int32 _ConsumptionSubLedgerID;
        private String _ConsumptionSearchDialogTypeName;
        private String _ConsumptionIDFieldName;
        private String _ConsumptionCodeFieldName;
        private String _ConsumptionDisplayFieldName;
        private String _ConsumptionMethodName;
        private String _ConsumptionFilterExpression;
        private Int32 _ConsumptionSubLedger;
        private String _ConsumptionSubLedgerCode;
        private String _ConsumptionSubLedgerName;
        private Int32 _AdjustmentIN;
        private String _AdjustmentINGLAccountNo;
        private String _AdjustmentINGLAccountName;
        private Int32 _AdjustmentINSubLedgerID;
        private String _AdjustmentINSearchDialogTypeName;
        private String _AdjustmentINIDFieldName;
        private String _AdjustmentINCodeFieldName;
        private String _AdjustmentINDisplayFieldName;
        private String _AdjustmentINMethodName;
        private String _AdjustmentINFilterExpression;
        private Int32 _AdjustmentINSubLedger;
        private String _AdjustmentINSubLedgerCode;
        private String _AdjustmentINSubLedgerName;
        private Int32 _AdjustmentOUT;
        private String _AdjustmentOUTGLAccountNo;
        private String _AdjustmentOUTGLAccountName;
        private Int32 _AdjustmentOUTSubLedgerID;
        private String _AdjustmentOUTSearchDialogTypeName;
        private String _AdjustmentOUTIDFieldName;
        private String _AdjustmentOUTCodeFieldName;
        private String _AdjustmentOUTDisplayFieldName;
        private String _AdjustmentOUTMethodName;
        private String _AdjustmentOUTFilterExpression;
        private Int32 _AdjustmentOUTSubLedger;
        private String _AdjustmentOUTSubLedgerCode;
        private String _AdjustmentOUTSubLedgerName;
        private String _Remarks;

        [Column(Name = "ProductLineID", DataType = "Int32")]
        public Int32 ProductLineID
        {
            get { return _ProductLineID; }
            set { _ProductLineID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "Inventory", DataType = "Int32")]
        public Int32 Inventory
        {
            get { return _Inventory; }
            set { _Inventory = value; }
        }
        [Column(Name = "InventoryGLAccountNo", DataType = "String")]
        public String InventoryGLAccountNo
        {
            get { return _InventoryGLAccountNo; }
            set { _InventoryGLAccountNo = value; }
        }
        [Column(Name = "InventoryGLAccountName", DataType = "String")]
        public String InventoryGLAccountName
        {
            get { return _InventoryGLAccountName; }
            set { _InventoryGLAccountName = value; }
        }
        [Column(Name = "InventorySubLedgerID", DataType = "Int32")]
        public Int32 InventorySubLedgerID
        {
            get { return _InventorySubLedgerID; }
            set { _InventorySubLedgerID = value; }
        }
        [Column(Name = "InventorySearchDialogTypeName", DataType = "String")]
        public String InventorySearchDialogTypeName
        {
            get { return _InventorySearchDialogTypeName; }
            set { _InventorySearchDialogTypeName = value; }
        }
        [Column(Name = "InventoryIDFieldName", DataType = "String")]
        public String InventoryIDFieldName
        {
            get { return _InventoryIDFieldName; }
            set { _InventoryIDFieldName = value; }
        }
        [Column(Name = "InventoryCodeFieldName", DataType = "String")]
        public String InventoryCodeFieldName
        {
            get { return _InventoryCodeFieldName; }
            set { _InventoryCodeFieldName = value; }
        }
        [Column(Name = "InventoryDisplayFieldName", DataType = "String")]
        public String InventoryDisplayFieldName
        {
            get { return _InventoryDisplayFieldName; }
            set { _InventoryDisplayFieldName = value; }
        }
        [Column(Name = "InventoryMethodName", DataType = "String")]
        public String InventoryMethodName
        {
            get { return _InventoryMethodName; }
            set { _InventoryMethodName = value; }
        }
        [Column(Name = "InventoryFilterExpression", DataType = "String")]
        public String InventoryFilterExpression
        {
            get { return _InventoryFilterExpression; }
            set { _InventoryFilterExpression = value; }
        }
        [Column(Name = "InventorySubLedger", DataType = "Int32")]
        public Int32 InventorySubLedger
        {
            get { return _InventorySubLedger; }
            set { _InventorySubLedger = value; }
        }
        [Column(Name = "InventorySubLedgerCode", DataType = "String")]
        public String InventorySubLedgerCode
        {
            get { return _InventorySubLedgerCode; }
            set { _InventorySubLedgerCode = value; }
        }
        [Column(Name = "InventorySubLedgerName", DataType = "String")]
        public String InventorySubLedgerName
        {
            get { return _InventorySubLedgerName; }
            set { _InventorySubLedgerName = value; }
        }
        [Column(Name = "InventoryVAT", DataType = "Int32")]
        public Int32 InventoryVAT
        {
            get { return _InventoryVAT; }
            set { _InventoryVAT = value; }
        }
        [Column(Name = "InventoryVATGLAccountNo", DataType = "String")]
        public String InventoryVATGLAccountNo
        {
            get { return _InventoryVATGLAccountNo; }
            set { _InventoryVATGLAccountNo = value; }
        }
        [Column(Name = "InventoryVATGLAccountName", DataType = "String")]
        public String InventoryVATGLAccountName
        {
            get { return _InventoryVATGLAccountName; }
            set { _InventoryVATGLAccountName = value; }
        }
        [Column(Name = "InventoryVATSubLedgerID", DataType = "Int32")]
        public Int32 InventoryVATSubLedgerID
        {
            get { return _InventoryVATSubLedgerID; }
            set { _InventoryVATSubLedgerID = value; }
        }
        [Column(Name = "InventoryVATSearchDialogTypeName", DataType = "String")]
        public String InventoryVATSearchDialogTypeName
        {
            get { return _InventoryVATSearchDialogTypeName; }
            set { _InventoryVATSearchDialogTypeName = value; }
        }
        [Column(Name = "InventoryVATIDFieldName", DataType = "String")]
        public String InventoryVATIDFieldName
        {
            get { return _InventoryVATIDFieldName; }
            set { _InventoryVATIDFieldName = value; }
        }
        [Column(Name = "InventoryVATCodeFieldName", DataType = "String")]
        public String InventoryVATCodeFieldName
        {
            get { return _InventoryVATCodeFieldName; }
            set { _InventoryVATCodeFieldName = value; }
        }
        [Column(Name = "InventoryVATDisplayFieldName", DataType = "String")]
        public String InventoryVATDisplayFieldName
        {
            get { return _InventoryVATDisplayFieldName; }
            set { _InventoryVATDisplayFieldName = value; }
        }
        [Column(Name = "InventoryVATMethodName", DataType = "String")]
        public String InventoryVATMethodName
        {
            get { return _InventoryVATMethodName; }
            set { _InventoryVATMethodName = value; }
        }
        [Column(Name = "InventoryVATFilterExpression", DataType = "String")]
        public String InventoryVATFilterExpression
        {
            get { return _InventoryVATFilterExpression; }
            set { _InventoryVATFilterExpression = value; }
        }
        [Column(Name = "InventoryVATSubLedger", DataType = "Int32")]
        public Int32 InventoryVATSubLedger
        {
            get { return _InventoryVATSubLedger; }
            set { _InventoryVATSubLedger = value; }
        }
        [Column(Name = "InventoryVATSubLedgerCode", DataType = "String")]
        public String InventoryVATSubLedgerCode
        {
            get { return _InventoryVATSubLedgerCode; }
            set { _InventoryVATSubLedgerCode = value; }
        }
        [Column(Name = "InventoryVATSubLedgerName", DataType = "String")]
        public String InventoryVATSubLedgerName
        {
            get { return _InventoryVATSubLedgerName; }
            set { _InventoryVATSubLedgerName = value; }
        }
        [Column(Name = "InventoryDiscount", DataType = "Int32")]
        public Int32 InventoryDiscount
        {
            get { return _InventoryDiscount; }
            set { _InventoryDiscount = value; }
        }
        [Column(Name = "InventoryDiscountGLAccountNo", DataType = "String")]
        public String InventoryDiscountGLAccountNo
        {
            get { return _InventoryDiscountGLAccountNo; }
            set { _InventoryDiscountGLAccountNo = value; }
        }
        [Column(Name = "InventoryDiscountGLAccountName", DataType = "String")]
        public String InventoryDiscountGLAccountName
        {
            get { return _InventoryDiscountGLAccountName; }
            set { _InventoryDiscountGLAccountName = value; }
        }
        [Column(Name = "InventoryDiscountSubLedgerID", DataType = "Int32")]
        public Int32 InventoryDiscountSubLedgerID
        {
            get { return _InventoryDiscountSubLedgerID; }
            set { _InventoryDiscountSubLedgerID = value; }
        }
        [Column(Name = "InventoryDiscountSearchDialogTypeName", DataType = "String")]
        public String InventoryDiscountSearchDialogTypeName
        {
            get { return _InventoryDiscountSearchDialogTypeName; }
            set { _InventoryDiscountSearchDialogTypeName = value; }
        }
        [Column(Name = "InventoryDiscountIDFieldName", DataType = "String")]
        public String InventoryDiscountIDFieldName
        {
            get { return _InventoryDiscountIDFieldName; }
            set { _InventoryDiscountIDFieldName = value; }
        }
        [Column(Name = "InventoryDiscountCodeFieldName", DataType = "String")]
        public String InventoryDiscountCodeFieldName
        {
            get { return _InventoryDiscountCodeFieldName; }
            set { _InventoryDiscountCodeFieldName = value; }
        }
        [Column(Name = "InventoryDiscountDisplayFieldName", DataType = "String")]
        public String InventoryDiscountDisplayFieldName
        {
            get { return _InventoryDiscountDisplayFieldName; }
            set { _InventoryDiscountDisplayFieldName = value; }
        }
        [Column(Name = "InventoryDiscountMethodName", DataType = "String")]
        public String InventoryDiscountMethodName
        {
            get { return _InventoryDiscountMethodName; }
            set { _InventoryDiscountMethodName = value; }
        }
        [Column(Name = "InventoryDiscountFilterExpression", DataType = "String")]
        public String InventoryDiscountFilterExpression
        {
            get { return _InventoryDiscountFilterExpression; }
            set { _InventoryDiscountFilterExpression = value; }
        }
        [Column(Name = "InventoryDiscountSubLedger", DataType = "Int32")]
        public Int32 InventoryDiscountSubLedger
        {
            get { return _InventoryDiscountSubLedger; }
            set { _InventoryDiscountSubLedger = value; }
        }
        [Column(Name = "InventoryDiscountSubLedgerCode", DataType = "String")]
        public String InventoryDiscountSubLedgerCode
        {
            get { return _InventoryDiscountSubLedgerCode; }
            set { _InventoryDiscountSubLedgerCode = value; }
        }
        [Column(Name = "InventoryDiscountSubLedgerName", DataType = "String")]
        public String InventoryDiscountSubLedgerName
        {
            get { return _InventoryDiscountSubLedgerName; }
            set { _InventoryDiscountSubLedgerName = value; }
        }
        [Column(Name = "COGS", DataType = "Int32")]
        public Int32 COGS
        {
            get { return _COGS; }
            set { _COGS = value; }
        }
        [Column(Name = "COGSGLAccountNo", DataType = "String")]
        public String COGSGLAccountNo
        {
            get { return _COGSGLAccountNo; }
            set { _COGSGLAccountNo = value; }
        }
        [Column(Name = "COGSGLAccountName", DataType = "String")]
        public String COGSGLAccountName
        {
            get { return _COGSGLAccountName; }
            set { _COGSGLAccountName = value; }
        }
        [Column(Name = "COGSSubLedgerID", DataType = "Int32")]
        public Int32 COGSSubLedgerID
        {
            get { return _COGSSubLedgerID; }
            set { _COGSSubLedgerID = value; }
        }
        [Column(Name = "COGSSearchDialogTypeName", DataType = "String")]
        public String COGSSearchDialogTypeName
        {
            get { return _COGSSearchDialogTypeName; }
            set { _COGSSearchDialogTypeName = value; }
        }
        [Column(Name = "COGSIDFieldName", DataType = "String")]
        public String COGSIDFieldName
        {
            get { return _COGSIDFieldName; }
            set { _COGSIDFieldName = value; }
        }
        [Column(Name = "COGSCodeFieldName", DataType = "String")]
        public String COGSCodeFieldName
        {
            get { return _COGSCodeFieldName; }
            set { _COGSCodeFieldName = value; }
        }
        [Column(Name = "COGSDisplayFieldName", DataType = "String")]
        public String COGSDisplayFieldName
        {
            get { return _COGSDisplayFieldName; }
            set { _COGSDisplayFieldName = value; }
        }
        [Column(Name = "COGSMethodName", DataType = "String")]
        public String COGSMethodName
        {
            get { return _COGSMethodName; }
            set { _COGSMethodName = value; }
        }
        [Column(Name = "COGSFilterExpression", DataType = "String")]
        public String COGSFilterExpression
        {
            get { return _COGSFilterExpression; }
            set { _COGSFilterExpression = value; }
        }
        [Column(Name = "COGSSubLedger", DataType = "Int32")]
        public Int32 COGSSubLedger
        {
            get { return _COGSSubLedger; }
            set { _COGSSubLedger = value; }
        }
        [Column(Name = "COGSSubLedgerCode", DataType = "String")]
        public String COGSSubLedgerCode
        {
            get { return _COGSSubLedgerCode; }
            set { _COGSSubLedgerCode = value; }
        }
        [Column(Name = "COGSSubLedgerName", DataType = "String")]
        public String COGSSubLedgerName
        {
            get { return _COGSSubLedgerName; }
            set { _COGSSubLedgerName = value; }
        }
        [Column(Name = "Purchase", DataType = "Int32")]
        public Int32 Purchase
        {
            get { return _Purchase; }
            set { _Purchase = value; }
        }
        [Column(Name = "PurchaseGLAccountNo", DataType = "String")]
        public String PurchaseGLAccountNo
        {
            get { return _PurchaseGLAccountNo; }
            set { _PurchaseGLAccountNo = value; }
        }
        [Column(Name = "PurchaseGLAccountName", DataType = "String")]
        public String PurchaseGLAccountName
        {
            get { return _PurchaseGLAccountName; }
            set { _PurchaseGLAccountName = value; }
        }
        [Column(Name = "PurchaseSubLedgerID", DataType = "Int32")]
        public Int32 PurchaseSubLedgerID
        {
            get { return _PurchaseSubLedgerID; }
            set { _PurchaseSubLedgerID = value; }
        }
        [Column(Name = "PurchaseSearchDialogTypeName", DataType = "String")]
        public String PurchaseSearchDialogTypeName
        {
            get { return _PurchaseSearchDialogTypeName; }
            set { _PurchaseSearchDialogTypeName = value; }
        }
        [Column(Name = "PurchaseIDFieldName", DataType = "String")]
        public String PurchaseIDFieldName
        {
            get { return _PurchaseIDFieldName; }
            set { _PurchaseIDFieldName = value; }
        }
        [Column(Name = "PurchaseCodeFieldName", DataType = "String")]
        public String PurchaseCodeFieldName
        {
            get { return _PurchaseCodeFieldName; }
            set { _PurchaseCodeFieldName = value; }
        }
        [Column(Name = "PurchaseDisplayFieldName", DataType = "String")]
        public String PurchaseDisplayFieldName
        {
            get { return _PurchaseDisplayFieldName; }
            set { _PurchaseDisplayFieldName = value; }
        }
        [Column(Name = "PurchaseMethodName", DataType = "String")]
        public String PurchaseMethodName
        {
            get { return _PurchaseMethodName; }
            set { _PurchaseMethodName = value; }
        }
        [Column(Name = "PurchaseFilterExpression", DataType = "String")]
        public String PurchaseFilterExpression
        {
            get { return _PurchaseFilterExpression; }
            set { _PurchaseFilterExpression = value; }
        }
        [Column(Name = "PurchaseSubLedger", DataType = "Int32")]
        public Int32 PurchaseSubLedger
        {
            get { return _PurchaseSubLedger; }
            set { _PurchaseSubLedger = value; }
        }
        [Column(Name = "PurchaseSubLedgerCode", DataType = "String")]
        public String PurchaseSubLedgerCode
        {
            get { return _PurchaseSubLedgerCode; }
            set { _PurchaseSubLedgerCode = value; }
        }
        [Column(Name = "PurchaseSubLedgerName", DataType = "String")]
        public String PurchaseSubLedgerName
        {
            get { return _PurchaseSubLedgerName; }
            set { _PurchaseSubLedgerName = value; }
        }
        [Column(Name = "PurchaseReturn", DataType = "Int32")]
        public Int32 PurchaseReturn
        {
            get { return _PurchaseReturn; }
            set { _PurchaseReturn = value; }
        }
        [Column(Name = "PurchaseReturnGLAccountNo", DataType = "String")]
        public String PurchaseReturnGLAccountNo
        {
            get { return _PurchaseReturnGLAccountNo; }
            set { _PurchaseReturnGLAccountNo = value; }
        }
        [Column(Name = "PurchaseReturnGLAccountName", DataType = "String")]
        public String PurchaseReturnGLAccountName
        {
            get { return _PurchaseReturnGLAccountName; }
            set { _PurchaseReturnGLAccountName = value; }
        }
        [Column(Name = "PurchaseReturnSubLedgerID", DataType = "Int32")]
        public Int32 PurchaseReturnSubLedgerID
        {
            get { return _PurchaseReturnSubLedgerID; }
            set { _PurchaseReturnSubLedgerID = value; }
        }
        [Column(Name = "PurchaseReturnSearchDialogTypeName", DataType = "String")]
        public String PurchaseReturnSearchDialogTypeName
        {
            get { return _PurchaseReturnSearchDialogTypeName; }
            set { _PurchaseReturnSearchDialogTypeName = value; }
        }
        [Column(Name = "PurchaseReturnIDFieldName", DataType = "String")]
        public String PurchaseReturnIDFieldName
        {
            get { return _PurchaseReturnIDFieldName; }
            set { _PurchaseReturnIDFieldName = value; }
        }
        [Column(Name = "PurchaseReturnCodeFieldName", DataType = "String")]
        public String PurchaseReturnCodeFieldName
        {
            get { return _PurchaseReturnCodeFieldName; }
            set { _PurchaseReturnCodeFieldName = value; }
        }
        [Column(Name = "PurchaseReturnDisplayFieldName", DataType = "String")]
        public String PurchaseReturnDisplayFieldName
        {
            get { return _PurchaseReturnDisplayFieldName; }
            set { _PurchaseReturnDisplayFieldName = value; }
        }
        [Column(Name = "PurchaseReturnMethodName", DataType = "String")]
        public String PurchaseReturnMethodName
        {
            get { return _PurchaseReturnMethodName; }
            set { _PurchaseReturnMethodName = value; }
        }
        [Column(Name = "PurchaseReturnFilterExpression", DataType = "String")]
        public String PurchaseReturnFilterExpression
        {
            get { return _PurchaseReturnFilterExpression; }
            set { _PurchaseReturnFilterExpression = value; }
        }
        [Column(Name = "PurchaseReturnSubLedger", DataType = "Int32")]
        public Int32 PurchaseReturnSubLedger
        {
            get { return _PurchaseReturnSubLedger; }
            set { _PurchaseReturnSubLedger = value; }
        }
        [Column(Name = "PurchaseReturnSubLedgerCode", DataType = "String")]
        public String PurchaseReturnSubLedgerCode
        {
            get { return _PurchaseReturnSubLedgerCode; }
            set { _PurchaseReturnSubLedgerCode = value; }
        }
        [Column(Name = "PurchaseReturnSubLedgerName", DataType = "String")]
        public String PurchaseReturnSubLedgerName
        {
            get { return _PurchaseReturnSubLedgerName; }
            set { _PurchaseReturnSubLedgerName = value; }
        }
        [Column(Name = "PurchaseDiscount", DataType = "Int32")]
        public Int32 PurchaseDiscount
        {
            get { return _PurchaseDiscount; }
            set { _PurchaseDiscount = value; }
        }
        [Column(Name = "PurchaseDiscountGLAccountNo", DataType = "String")]
        public String PurchaseDiscountGLAccountNo
        {
            get { return _PurchaseDiscountGLAccountNo; }
            set { _PurchaseDiscountGLAccountNo = value; }
        }
        [Column(Name = "PurchaseDiscountGLAccountName", DataType = "String")]
        public String PurchaseDiscountGLAccountName
        {
            get { return _PurchaseDiscountGLAccountName; }
            set { _PurchaseDiscountGLAccountName = value; }
        }
        [Column(Name = "PurchaseDiscountSubLedgerID", DataType = "Int32")]
        public Int32 PurchaseDiscountSubLedgerID
        {
            get { return _PurchaseDiscountSubLedgerID; }
            set { _PurchaseDiscountSubLedgerID = value; }
        }
        [Column(Name = "PurchaseDiscountSearchDialogTypeName", DataType = "String")]
        public String PurchaseDiscountSearchDialogTypeName
        {
            get { return _PurchaseDiscountSearchDialogTypeName; }
            set { _PurchaseDiscountSearchDialogTypeName = value; }
        }
        [Column(Name = "PurchaseDiscountIDFieldName", DataType = "String")]
        public String PurchaseDiscountIDFieldName
        {
            get { return _PurchaseDiscountIDFieldName; }
            set { _PurchaseDiscountIDFieldName = value; }
        }
        [Column(Name = "PurchaseDiscountCodeFieldName", DataType = "String")]
        public String PurchaseDiscountCodeFieldName
        {
            get { return _PurchaseDiscountCodeFieldName; }
            set { _PurchaseDiscountCodeFieldName = value; }
        }
        [Column(Name = "PurchaseDiscountDisplayFieldName", DataType = "String")]
        public String PurchaseDiscountDisplayFieldName
        {
            get { return _PurchaseDiscountDisplayFieldName; }
            set { _PurchaseDiscountDisplayFieldName = value; }
        }
        [Column(Name = "PurchaseDiscountMethodName", DataType = "String")]
        public String PurchaseDiscountMethodName
        {
            get { return _PurchaseDiscountMethodName; }
            set { _PurchaseDiscountMethodName = value; }
        }
        [Column(Name = "PurchaseDiscountFilterExpression", DataType = "String")]
        public String PurchaseDiscountFilterExpression
        {
            get { return _PurchaseDiscountFilterExpression; }
            set { _PurchaseDiscountFilterExpression = value; }
        }
        [Column(Name = "PurchaseDiscountSubLedger", DataType = "Int32")]
        public Int32 PurchaseDiscountSubLedger
        {
            get { return _PurchaseDiscountSubLedger; }
            set { _PurchaseDiscountSubLedger = value; }
        }
        [Column(Name = "PurchaseDiscountSubLedgerCode", DataType = "String")]
        public String PurchaseDiscountSubLedgerCode
        {
            get { return _PurchaseDiscountSubLedgerCode; }
            set { _PurchaseDiscountSubLedgerCode = value; }
        }
        [Column(Name = "PurchaseDiscountSubLedgerName", DataType = "String")]
        public String PurchaseDiscountSubLedgerName
        {
            get { return _PurchaseDiscountSubLedgerName; }
            set { _PurchaseDiscountSubLedgerName = value; }
        }
        [Column(Name = "PurchasePriceVariant", DataType = "Int32")]
        public Int32 PurchasePriceVariant
        {
            get { return _PurchasePriceVariant; }
            set { _PurchasePriceVariant = value; }
        }
        [Column(Name = "PurchasePriceVariantGLAccountNo", DataType = "String")]
        public String PurchasePriceVariantGLAccountNo
        {
            get { return _PurchasePriceVariantGLAccountNo; }
            set { _PurchasePriceVariantGLAccountNo = value; }
        }
        [Column(Name = "PurchasePriceVariantGLAccountName", DataType = "String")]
        public String PurchasePriceVariantGLAccountName
        {
            get { return _PurchasePriceVariantGLAccountName; }
            set { _PurchasePriceVariantGLAccountName = value; }
        }
        [Column(Name = "PurchasePriceVariantSubLedgerID", DataType = "Int32")]
        public Int32 PurchasePriceVariantSubLedgerID
        {
            get { return _PurchasePriceVariantSubLedgerID; }
            set { _PurchasePriceVariantSubLedgerID = value; }
        }
        [Column(Name = "PurchasePriceVariantSearchDialogTypeName", DataType = "String")]
        public String PurchasePriceVariantSearchDialogTypeName
        {
            get { return _PurchasePriceVariantSearchDialogTypeName; }
            set { _PurchasePriceVariantSearchDialogTypeName = value; }
        }
        [Column(Name = "PurchasePriceVariantIDFieldName", DataType = "String")]
        public String PurchasePriceVariantIDFieldName
        {
            get { return _PurchasePriceVariantIDFieldName; }
            set { _PurchasePriceVariantIDFieldName = value; }
        }
        [Column(Name = "PurchasePriceVariantCodeFieldName", DataType = "String")]
        public String PurchasePriceVariantCodeFieldName
        {
            get { return _PurchasePriceVariantCodeFieldName; }
            set { _PurchasePriceVariantCodeFieldName = value; }
        }
        [Column(Name = "PurchasePriceVariantDisplayFieldName", DataType = "String")]
        public String PurchasePriceVariantDisplayFieldName
        {
            get { return _PurchasePriceVariantDisplayFieldName; }
            set { _PurchasePriceVariantDisplayFieldName = value; }
        }
        [Column(Name = "PurchasePriceVariantMethodName", DataType = "String")]
        public String PurchasePriceVariantMethodName
        {
            get { return _PurchasePriceVariantMethodName; }
            set { _PurchasePriceVariantMethodName = value; }
        }
        [Column(Name = "PurchasePriceVariantFilterExpression", DataType = "String")]
        public String PurchasePriceVariantFilterExpression
        {
            get { return _PurchasePriceVariantFilterExpression; }
            set { _PurchasePriceVariantFilterExpression = value; }
        }
        [Column(Name = "PurchasePriceVariantSubLedger", DataType = "Int32")]
        public Int32 PurchasePriceVariantSubLedger
        {
            get { return _PurchasePriceVariantSubLedger; }
            set { _PurchasePriceVariantSubLedger = value; }
        }
        [Column(Name = "PurchasePriceVariantSubLedgerCode", DataType = "String")]
        public String PurchasePriceVariantSubLedgerCode
        {
            get { return _PurchasePriceVariantSubLedgerCode; }
            set { _PurchasePriceVariantSubLedgerCode = value; }
        }
        [Column(Name = "PurchasePriceVariantSubLedgerName", DataType = "String")]
        public String PurchasePriceVariantSubLedgerName
        {
            get { return _PurchasePriceVariantSubLedgerName; }
            set { _PurchasePriceVariantSubLedgerName = value; }
        }
        [Column(Name = "Sales", DataType = "Int32")]
        public Int32 Sales
        {
            get { return _Sales; }
            set { _Sales = value; }
        }
        [Column(Name = "SalesGLAccountNo", DataType = "String")]
        public String SalesGLAccountNo
        {
            get { return _SalesGLAccountNo; }
            set { _SalesGLAccountNo = value; }
        }
        [Column(Name = "SalesGLAccountName", DataType = "String")]
        public String SalesGLAccountName
        {
            get { return _SalesGLAccountName; }
            set { _SalesGLAccountName = value; }
        }
        [Column(Name = "SalesSubLedgerID", DataType = "Int32")]
        public Int32 SalesSubLedgerID
        {
            get { return _SalesSubLedgerID; }
            set { _SalesSubLedgerID = value; }
        }
        [Column(Name = "SalesSearchDialogTypeName", DataType = "String")]
        public String SalesSearchDialogTypeName
        {
            get { return _SalesSearchDialogTypeName; }
            set { _SalesSearchDialogTypeName = value; }
        }
        [Column(Name = "SalesIDFieldName", DataType = "String")]
        public String SalesIDFieldName
        {
            get { return _SalesIDFieldName; }
            set { _SalesIDFieldName = value; }
        }
        [Column(Name = "SalesCodeFieldName", DataType = "String")]
        public String SalesCodeFieldName
        {
            get { return _SalesCodeFieldName; }
            set { _SalesCodeFieldName = value; }
        }
        [Column(Name = "SalesDisplayFieldName", DataType = "String")]
        public String SalesDisplayFieldName
        {
            get { return _SalesDisplayFieldName; }
            set { _SalesDisplayFieldName = value; }
        }
        [Column(Name = "SalesMethodName", DataType = "String")]
        public String SalesMethodName
        {
            get { return _SalesMethodName; }
            set { _SalesMethodName = value; }
        }
        [Column(Name = "SalesFilterExpression", DataType = "String")]
        public String SalesFilterExpression
        {
            get { return _SalesFilterExpression; }
            set { _SalesFilterExpression = value; }
        }
        [Column(Name = "SalesSubLedger", DataType = "Int32")]
        public Int32 SalesSubLedger
        {
            get { return _SalesSubLedger; }
            set { _SalesSubLedger = value; }
        }
        [Column(Name = "SalesSubLedgerCode", DataType = "String")]
        public String SalesSubLedgerCode
        {
            get { return _SalesSubLedgerCode; }
            set { _SalesSubLedgerCode = value; }
        }
        [Column(Name = "SalesSubLedgerName", DataType = "String")]
        public String SalesSubLedgerName
        {
            get { return _SalesSubLedgerName; }
            set { _SalesSubLedgerName = value; }
        }
        [Column(Name = "SalesReturn", DataType = "Int32")]
        public Int32 SalesReturn
        {
            get { return _SalesReturn; }
            set { _SalesReturn = value; }
        }
        [Column(Name = "SalesReturnGLAccountNo", DataType = "String")]
        public String SalesReturnGLAccountNo
        {
            get { return _SalesReturnGLAccountNo; }
            set { _SalesReturnGLAccountNo = value; }
        }
        [Column(Name = "SalesReturnGLAccountName", DataType = "String")]
        public String SalesReturnGLAccountName
        {
            get { return _SalesReturnGLAccountName; }
            set { _SalesReturnGLAccountName = value; }
        }
        [Column(Name = "SalesReturnSubLedgerID", DataType = "Int32")]
        public Int32 SalesReturnSubLedgerID
        {
            get { return _SalesReturnSubLedgerID; }
            set { _SalesReturnSubLedgerID = value; }
        }
        [Column(Name = "SalesReturnSearchDialogTypeName", DataType = "String")]
        public String SalesReturnSearchDialogTypeName
        {
            get { return _SalesReturnSearchDialogTypeName; }
            set { _SalesReturnSearchDialogTypeName = value; }
        }
        [Column(Name = "SalesReturnIDFieldName", DataType = "String")]
        public String SalesReturnIDFieldName
        {
            get { return _SalesReturnIDFieldName; }
            set { _SalesReturnIDFieldName = value; }
        }
        [Column(Name = "SalesReturnCodeFieldName", DataType = "String")]
        public String SalesReturnCodeFieldName
        {
            get { return _SalesReturnCodeFieldName; }
            set { _SalesReturnCodeFieldName = value; }
        }
        [Column(Name = "SalesReturnDisplayFieldName", DataType = "String")]
        public String SalesReturnDisplayFieldName
        {
            get { return _SalesReturnDisplayFieldName; }
            set { _SalesReturnDisplayFieldName = value; }
        }
        [Column(Name = "SalesReturnMethodName", DataType = "String")]
        public String SalesReturnMethodName
        {
            get { return _SalesReturnMethodName; }
            set { _SalesReturnMethodName = value; }
        }
        [Column(Name = "SalesReturnFilterExpression", DataType = "String")]
        public String SalesReturnFilterExpression
        {
            get { return _SalesReturnFilterExpression; }
            set { _SalesReturnFilterExpression = value; }
        }
        [Column(Name = "SalesReturnSubLedger", DataType = "Int32")]
        public Int32 SalesReturnSubLedger
        {
            get { return _SalesReturnSubLedger; }
            set { _SalesReturnSubLedger = value; }
        }
        [Column(Name = "SalesReturnSubLedgerCode", DataType = "String")]
        public String SalesReturnSubLedgerCode
        {
            get { return _SalesReturnSubLedgerCode; }
            set { _SalesReturnSubLedgerCode = value; }
        }
        [Column(Name = "SalesReturnSubLedgerName", DataType = "String")]
        public String SalesReturnSubLedgerName
        {
            get { return _SalesReturnSubLedgerName; }
            set { _SalesReturnSubLedgerName = value; }
        }
        [Column(Name = "SalesDiscount", DataType = "Int32")]
        public Int32 SalesDiscount
        {
            get { return _SalesDiscount; }
            set { _SalesDiscount = value; }
        }
        [Column(Name = "SalesDiscountGLAccountNo", DataType = "String")]
        public String SalesDiscountGLAccountNo
        {
            get { return _SalesDiscountGLAccountNo; }
            set { _SalesDiscountGLAccountNo = value; }
        }
        [Column(Name = "SalesDiscountGLAccountName", DataType = "String")]
        public String SalesDiscountGLAccountName
        {
            get { return _SalesDiscountGLAccountName; }
            set { _SalesDiscountGLAccountName = value; }
        }
        [Column(Name = "SalesDiscountSubLedgerID", DataType = "Int32")]
        public Int32 SalesDiscountSubLedgerID
        {
            get { return _SalesDiscountSubLedgerID; }
            set { _SalesDiscountSubLedgerID = value; }
        }
        [Column(Name = "SalesDiscountSearchDialogTypeName", DataType = "String")]
        public String SalesDiscountSearchDialogTypeName
        {
            get { return _SalesDiscountSearchDialogTypeName; }
            set { _SalesDiscountSearchDialogTypeName = value; }
        }
        [Column(Name = "SalesDiscountIDFieldName", DataType = "String")]
        public String SalesDiscountIDFieldName
        {
            get { return _SalesDiscountIDFieldName; }
            set { _SalesDiscountIDFieldName = value; }
        }
        [Column(Name = "SalesDiscountCodeFieldName", DataType = "String")]
        public String SalesDiscountCodeFieldName
        {
            get { return _SalesDiscountCodeFieldName; }
            set { _SalesDiscountCodeFieldName = value; }
        }
        [Column(Name = "SalesDiscountDisplayFieldName", DataType = "String")]
        public String SalesDiscountDisplayFieldName
        {
            get { return _SalesDiscountDisplayFieldName; }
            set { _SalesDiscountDisplayFieldName = value; }
        }
        [Column(Name = "SalesDiscountMethodName", DataType = "String")]
        public String SalesDiscountMethodName
        {
            get { return _SalesDiscountMethodName; }
            set { _SalesDiscountMethodName = value; }
        }
        [Column(Name = "SalesDiscountFilterExpression", DataType = "String")]
        public String SalesDiscountFilterExpression
        {
            get { return _SalesDiscountFilterExpression; }
            set { _SalesDiscountFilterExpression = value; }
        }
        [Column(Name = "SalesDiscountSubLedger", DataType = "Int32")]
        public Int32 SalesDiscountSubLedger
        {
            get { return _SalesDiscountSubLedger; }
            set { _SalesDiscountSubLedger = value; }
        }
        [Column(Name = "SalesDiscountSubLedgerCode", DataType = "String")]
        public String SalesDiscountSubLedgerCode
        {
            get { return _SalesDiscountSubLedgerCode; }
            set { _SalesDiscountSubLedgerCode = value; }
        }
        [Column(Name = "SalesDiscountSubLedgerName", DataType = "String")]
        public String SalesDiscountSubLedgerName
        {
            get { return _SalesDiscountSubLedgerName; }
            set { _SalesDiscountSubLedgerName = value; }
        }
        [Column(Name = "MaterialRevenue", DataType = "Int32")]
        public Int32 MaterialRevenue
        {
            get { return _MaterialRevenue; }
            set { _MaterialRevenue = value; }
        }
        [Column(Name = "MaterialRevenueGLAccountNo", DataType = "String")]
        public String MaterialRevenueGLAccountNo
        {
            get { return _MaterialRevenueGLAccountNo; }
            set { _MaterialRevenueGLAccountNo = value; }
        }
        [Column(Name = "MaterialRevenueGLAccountName", DataType = "String")]
        public String MaterialRevenueGLAccountName
        {
            get { return _MaterialRevenueGLAccountName; }
            set { _MaterialRevenueGLAccountName = value; }
        }
        [Column(Name = "MaterialRevenueSubLedgerID", DataType = "Int32")]
        public Int32 MaterialRevenueSubLedgerID
        {
            get { return _MaterialRevenueSubLedgerID; }
            set { _MaterialRevenueSubLedgerID = value; }
        }
        [Column(Name = "MaterialRevenueSearchDialogTypeName", DataType = "String")]
        public String MaterialRevenueSearchDialogTypeName
        {
            get { return _MaterialRevenueSearchDialogTypeName; }
            set { _MaterialRevenueSearchDialogTypeName = value; }
        }
        [Column(Name = "MaterialRevenueIDFieldName", DataType = "String")]
        public String MaterialRevenueIDFieldName
        {
            get { return _MaterialRevenueIDFieldName; }
            set { _MaterialRevenueIDFieldName = value; }
        }
        [Column(Name = "MaterialRevenueCodeFieldName", DataType = "String")]
        public String MaterialRevenueCodeFieldName
        {
            get { return _MaterialRevenueCodeFieldName; }
            set { _MaterialRevenueCodeFieldName = value; }
        }
        [Column(Name = "MaterialRevenueDisplayFieldName", DataType = "String")]
        public String MaterialRevenueDisplayFieldName
        {
            get { return _MaterialRevenueDisplayFieldName; }
            set { _MaterialRevenueDisplayFieldName = value; }
        }
        [Column(Name = "MaterialRevenueMethodName", DataType = "String")]
        public String MaterialRevenueMethodName
        {
            get { return _MaterialRevenueMethodName; }
            set { _MaterialRevenueMethodName = value; }
        }
        [Column(Name = "MaterialRevenueFilterExpression", DataType = "String")]
        public String MaterialRevenueFilterExpression
        {
            get { return _MaterialRevenueFilterExpression; }
            set { _MaterialRevenueFilterExpression = value; }
        }
        [Column(Name = "MaterialRevenueSubLedger", DataType = "Int32")]
        public Int32 MaterialRevenueSubLedger
        {
            get { return _MaterialRevenueSubLedger; }
            set { _MaterialRevenueSubLedger = value; }
        }
        [Column(Name = "MaterialRevenueSubLedgerCode", DataType = "String")]
        public String MaterialRevenueSubLedgerCode
        {
            get { return _MaterialRevenueSubLedgerCode; }
            set { _MaterialRevenueSubLedgerCode = value; }
        }
        [Column(Name = "MaterialRevenueSubLedgerName", DataType = "String")]
        public String MaterialRevenueSubLedgerName
        {
            get { return _MaterialRevenueSubLedgerName; }
            set { _MaterialRevenueSubLedgerName = value; }
        }
        [Column(Name = "Consumption", DataType = "Int32")]
        public Int32 Consumption
        {
            get { return _Consumption; }
            set { _Consumption = value; }
        }
        [Column(Name = "ConsumptionGLAccountNo", DataType = "String")]
        public String ConsumptionGLAccountNo
        {
            get { return _ConsumptionGLAccountNo; }
            set { _ConsumptionGLAccountNo = value; }
        }
        [Column(Name = "ConsumptionGLAccountName", DataType = "String")]
        public String ConsumptionGLAccountName
        {
            get { return _ConsumptionGLAccountName; }
            set { _ConsumptionGLAccountName = value; }
        }
        [Column(Name = "ConsumptionSubLedgerID", DataType = "Int32")]
        public Int32 ConsumptionSubLedgerID
        {
            get { return _ConsumptionSubLedgerID; }
            set { _ConsumptionSubLedgerID = value; }
        }
        [Column(Name = "ConsumptionSearchDialogTypeName", DataType = "String")]
        public String ConsumptionSearchDialogTypeName
        {
            get { return _ConsumptionSearchDialogTypeName; }
            set { _ConsumptionSearchDialogTypeName = value; }
        }
        [Column(Name = "ConsumptionIDFieldName", DataType = "String")]
        public String ConsumptionIDFieldName
        {
            get { return _ConsumptionIDFieldName; }
            set { _ConsumptionIDFieldName = value; }
        }
        [Column(Name = "ConsumptionCodeFieldName", DataType = "String")]
        public String ConsumptionCodeFieldName
        {
            get { return _ConsumptionCodeFieldName; }
            set { _ConsumptionCodeFieldName = value; }
        }
        [Column(Name = "ConsumptionDisplayFieldName", DataType = "String")]
        public String ConsumptionDisplayFieldName
        {
            get { return _ConsumptionDisplayFieldName; }
            set { _ConsumptionDisplayFieldName = value; }
        }
        [Column(Name = "ConsumptionMethodName", DataType = "String")]
        public String ConsumptionMethodName
        {
            get { return _ConsumptionMethodName; }
            set { _ConsumptionMethodName = value; }
        }
        [Column(Name = "ConsumptionFilterExpression", DataType = "String")]
        public String ConsumptionFilterExpression
        {
            get { return _ConsumptionFilterExpression; }
            set { _ConsumptionFilterExpression = value; }
        }
        [Column(Name = "ConsumptionSubLedger", DataType = "Int32")]
        public Int32 ConsumptionSubLedger
        {
            get { return _ConsumptionSubLedger; }
            set { _ConsumptionSubLedger = value; }
        }
        [Column(Name = "ConsumptionSubLedgerCode", DataType = "String")]
        public String ConsumptionSubLedgerCode
        {
            get { return _ConsumptionSubLedgerCode; }
            set { _ConsumptionSubLedgerCode = value; }
        }
        [Column(Name = "ConsumptionSubLedgerName", DataType = "String")]
        public String ConsumptionSubLedgerName
        {
            get { return _ConsumptionSubLedgerName; }
            set { _ConsumptionSubLedgerName = value; }
        }
        [Column(Name = "AdjustmentIN", DataType = "Int32")]
        public Int32 AdjustmentIN
        {
            get { return _AdjustmentIN; }
            set { _AdjustmentIN = value; }
        }
        [Column(Name = "AdjustmentINGLAccountNo", DataType = "String")]
        public String AdjustmentINGLAccountNo
        {
            get { return _AdjustmentINGLAccountNo; }
            set { _AdjustmentINGLAccountNo = value; }
        }
        [Column(Name = "AdjustmentINGLAccountName", DataType = "String")]
        public String AdjustmentINGLAccountName
        {
            get { return _AdjustmentINGLAccountName; }
            set { _AdjustmentINGLAccountName = value; }
        }
        [Column(Name = "AdjustmentINSubLedgerID", DataType = "Int32")]
        public Int32 AdjustmentINSubLedgerID
        {
            get { return _AdjustmentINSubLedgerID; }
            set { _AdjustmentINSubLedgerID = value; }
        }
        [Column(Name = "AdjustmentINSearchDialogTypeName", DataType = "String")]
        public String AdjustmentINSearchDialogTypeName
        {
            get { return _AdjustmentINSearchDialogTypeName; }
            set { _AdjustmentINSearchDialogTypeName = value; }
        }
        [Column(Name = "AdjustmentINIDFieldName", DataType = "String")]
        public String AdjustmentINIDFieldName
        {
            get { return _AdjustmentINIDFieldName; }
            set { _AdjustmentINIDFieldName = value; }
        }
        [Column(Name = "AdjustmentINCodeFieldName", DataType = "String")]
        public String AdjustmentINCodeFieldName
        {
            get { return _AdjustmentINCodeFieldName; }
            set { _AdjustmentINCodeFieldName = value; }
        }
        [Column(Name = "AdjustmentINDisplayFieldName", DataType = "String")]
        public String AdjustmentINDisplayFieldName
        {
            get { return _AdjustmentINDisplayFieldName; }
            set { _AdjustmentINDisplayFieldName = value; }
        }
        [Column(Name = "AdjustmentINMethodName", DataType = "String")]
        public String AdjustmentINMethodName
        {
            get { return _AdjustmentINMethodName; }
            set { _AdjustmentINMethodName = value; }
        }
        [Column(Name = "AdjustmentINFilterExpression", DataType = "String")]
        public String AdjustmentINFilterExpression
        {
            get { return _AdjustmentINFilterExpression; }
            set { _AdjustmentINFilterExpression = value; }
        }
        [Column(Name = "AdjustmentINSubLedger", DataType = "Int32")]
        public Int32 AdjustmentINSubLedger
        {
            get { return _AdjustmentINSubLedger; }
            set { _AdjustmentINSubLedger = value; }
        }
        [Column(Name = "AdjustmentINSubLedgerCode", DataType = "String")]
        public String AdjustmentINSubLedgerCode
        {
            get { return _AdjustmentINSubLedgerCode; }
            set { _AdjustmentINSubLedgerCode = value; }
        }
        [Column(Name = "AdjustmentINSubLedgerName", DataType = "String")]
        public String AdjustmentINSubLedgerName
        {
            get { return _AdjustmentINSubLedgerName; }
            set { _AdjustmentINSubLedgerName = value; }
        }
        [Column(Name = "AdjustmentOUT", DataType = "Int32")]
        public Int32 AdjustmentOUT
        {
            get { return _AdjustmentOUT; }
            set { _AdjustmentOUT = value; }
        }
        [Column(Name = "AdjustmentOUTGLAccountNo", DataType = "String")]
        public String AdjustmentOUTGLAccountNo
        {
            get { return _AdjustmentOUTGLAccountNo; }
            set { _AdjustmentOUTGLAccountNo = value; }
        }
        [Column(Name = "AdjustmentOUTGLAccountName", DataType = "String")]
        public String AdjustmentOUTGLAccountName
        {
            get { return _AdjustmentOUTGLAccountName; }
            set { _AdjustmentOUTGLAccountName = value; }
        }
        [Column(Name = "AdjustmentOUTSubLedgerID", DataType = "Int32")]
        public Int32 AdjustmentOUTSubLedgerID
        {
            get { return _AdjustmentOUTSubLedgerID; }
            set { _AdjustmentOUTSubLedgerID = value; }
        }
        [Column(Name = "AdjustmentOUTSearchDialogTypeName", DataType = "String")]
        public String AdjustmentOUTSearchDialogTypeName
        {
            get { return _AdjustmentOUTSearchDialogTypeName; }
            set { _AdjustmentOUTSearchDialogTypeName = value; }
        }
        [Column(Name = "AdjustmentOUTIDFieldName", DataType = "String")]
        public String AdjustmentOUTIDFieldName
        {
            get { return _AdjustmentOUTIDFieldName; }
            set { _AdjustmentOUTIDFieldName = value; }
        }
        [Column(Name = "AdjustmentOUTCodeFieldName", DataType = "String")]
        public String AdjustmentOUTCodeFieldName
        {
            get { return _AdjustmentOUTCodeFieldName; }
            set { _AdjustmentOUTCodeFieldName = value; }
        }
        [Column(Name = "AdjustmentOUTDisplayFieldName", DataType = "String")]
        public String AdjustmentOUTDisplayFieldName
        {
            get { return _AdjustmentOUTDisplayFieldName; }
            set { _AdjustmentOUTDisplayFieldName = value; }
        }
        [Column(Name = "AdjustmentOUTMethodName", DataType = "String")]
        public String AdjustmentOUTMethodName
        {
            get { return _AdjustmentOUTMethodName; }
            set { _AdjustmentOUTMethodName = value; }
        }
        [Column(Name = "AdjustmentOUTFilterExpression", DataType = "String")]
        public String AdjustmentOUTFilterExpression
        {
            get { return _AdjustmentOUTFilterExpression; }
            set { _AdjustmentOUTFilterExpression = value; }
        }
        [Column(Name = "AdjustmentOUTSubLedger", DataType = "Int32")]
        public Int32 AdjustmentOUTSubLedger
        {
            get { return _AdjustmentOUTSubLedger; }
            set { _AdjustmentOUTSubLedger = value; }
        }
        [Column(Name = "AdjustmentOUTSubLedgerCode", DataType = "String")]
        public String AdjustmentOUTSubLedgerCode
        {
            get { return _AdjustmentOUTSubLedgerCode; }
            set { _AdjustmentOUTSubLedgerCode = value; }
        }
        [Column(Name = "AdjustmentOUTSubLedgerName", DataType = "String")]
        public String AdjustmentOUTSubLedgerName
        {
            get { return _AdjustmentOUTSubLedgerName; }
            set { _AdjustmentOUTSubLedgerName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
    }
    #endregion
    #region vProspectiveStudent
    [Serializable]
    [Table(Name = "vProspectiveStudent")]
    public partial class vProspectiveStudent
    {
        private Int32 _ProspectiveStudentID;
        private String _ProspectiveStudentCode;
        private Int32 _PeriodAdmissionID;
        private String _SiteID;
        private String _GCSalutation;
        private String _GCSuffix;
        private String _GCTitle;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _ProspectiveStudentName;
        private String _Name;
        private String _PreferredName;
        private String _CityOfBirth;
        private DateTime _DateOfBirth;
        private String _GCGender;
        private String _GCNationality;
        private String _GCReligion;
        private String _PlaceOfBaptism;
        private DateTime _DateOfBaptism;
        private Boolean _IsFeeder;
        private String _AddressID;
        private String _StreetName;
        private String _District;
        private String _City;
        private String _County;
        private String _GCState;
        private String _State;
        private Int32 _ZipCodeID;
        private String _ZipCode;
        private String _EmailAddress1;
        private String _EmailAddress2;
        private String _MobilePhoneNo1;
        private String _MobilePhoneNo2;
        private String _PhoneNo1;
        private String _PictureFileName;
        private String _GCBloodType;
        private String _GCLanguage;
        private Decimal _HomeDistance;
        private String _MedicalHistory;

        [Column(Name = "ProspectiveStudentID", DataType = "Int32")]
        public Int32 ProspectiveStudentID
        {
            get { return _ProspectiveStudentID; }
            set { _ProspectiveStudentID = value; }
        }
        [Column(Name = "ProspectiveStudentCode", DataType = "String")]
        public String ProspectiveStudentCode
        {
            get { return _ProspectiveStudentCode; }
            set { _ProspectiveStudentCode = value; }
        }
        [Column(Name = "PeriodAdmissionID", DataType = "Int32")]
        public Int32 PeriodAdmissionID
        {
            get { return _PeriodAdmissionID; }
            set { _PeriodAdmissionID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "GCSalutation", DataType = "String")]
        public String GCSalutation
        {
            get { return _GCSalutation; }
            set { _GCSalutation = value; }
        }
        [Column(Name = "GCSuffix", DataType = "String")]
        public String GCSuffix
        {
            get { return _GCSuffix; }
            set { _GCSuffix = value; }
        }
        [Column(Name = "GCTitle", DataType = "String")]
        public String GCTitle
        {
            get { return _GCTitle; }
            set { _GCTitle = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "ProspectiveStudentName", DataType = "String")]
        public String ProspectiveStudentName
        {
            get { return _ProspectiveStudentName; }
            set { _ProspectiveStudentName = value; }
        }
        [Column(Name = "Name", DataType = "String")]
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        [Column(Name = "PreferredName", DataType = "String")]
        public String PreferredName
        {
            get { return _PreferredName; }
            set { _PreferredName = value; }
        }
        [Column(Name = "CityOfBirth", DataType = "String")]
        public String CityOfBirth
        {
            get { return _CityOfBirth; }
            set { _CityOfBirth = value; }
        }
        [Column(Name = "DateOfBirth", DataType = "DateTime")]
        public DateTime DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }
        [Column(Name = "GCGender", DataType = "String")]
        public String GCGender
        {
            get { return _GCGender; }
            set { _GCGender = value; }
        }
        [Column(Name = "GCNationality", DataType = "String")]
        public String GCNationality
        {
            get { return _GCNationality; }
            set { _GCNationality = value; }
        }
        [Column(Name = "GCReligion", DataType = "String")]
        public String GCReligion
        {
            get { return _GCReligion; }
            set { _GCReligion = value; }
        }
        [Column(Name = "PlaceOfBaptism", DataType = "String")]
        public String PlaceOfBaptism
        {
            get { return _PlaceOfBaptism; }
            set { _PlaceOfBaptism = value; }
        }
        [Column(Name = "DateOfBaptism", DataType = "DateTime")]
        public DateTime DateOfBaptism
        {
            get { return _DateOfBaptism; }
            set { _DateOfBaptism = value; }
        }
        [Column(Name = "IsFeeder", DataType = "Boolean")]
        public Boolean IsFeeder
        {
            get { return _IsFeeder; }
            set { _IsFeeder = value; }
        }
        [Column(Name = "AddressID", DataType = "String")]
        public String AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
        }
        [Column(Name = "StreetName", DataType = "String")]
        public String StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }
        [Column(Name = "District", DataType = "String")]
        public String District
        {
            get { return _District; }
            set { _District = value; }
        }
        [Column(Name = "City", DataType = "String")]
        public String City
        {
            get { return _City; }
            set { _City = value; }
        }
        [Column(Name = "County", DataType = "String")]
        public String County
        {
            get { return _County; }
            set { _County = value; }
        }
        [Column(Name = "GCState", DataType = "String")]
        public String GCState
        {
            get { return _GCState; }
            set { _GCState = value; }
        }
        [Column(Name = "State", DataType = "String")]
        public String State
        {
            get { return _State; }
            set { _State = value; }
        }
        [Column(Name = "ZipCodeID", DataType = "Int32")]
        public Int32 ZipCodeID
        {
            get { return _ZipCodeID; }
            set { _ZipCodeID = value; }
        }
        [Column(Name = "ZipCode", DataType = "String")]
        public String ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }
        [Column(Name = "EmailAddress1", DataType = "String")]
        public String EmailAddress1
        {
            get { return _EmailAddress1; }
            set { _EmailAddress1 = value; }
        }
        [Column(Name = "EmailAddress2", DataType = "String")]
        public String EmailAddress2
        {
            get { return _EmailAddress2; }
            set { _EmailAddress2 = value; }
        }
        [Column(Name = "MobilePhoneNo1", DataType = "String")]
        public String MobilePhoneNo1
        {
            get { return _MobilePhoneNo1; }
            set { _MobilePhoneNo1 = value; }
        }
        [Column(Name = "MobilePhoneNo2", DataType = "String")]
        public String MobilePhoneNo2
        {
            get { return _MobilePhoneNo2; }
            set { _MobilePhoneNo2 = value; }
        }
        [Column(Name = "PhoneNo1", DataType = "String")]
        public String PhoneNo1
        {
            get { return _PhoneNo1; }
            set { _PhoneNo1 = value; }
        }
        [Column(Name = "PictureFileName", DataType = "String")]
        public String PictureFileName
        {
            get { return _PictureFileName; }
            set { _PictureFileName = value; }
        }
        [Column(Name = "GCBloodType", DataType = "String")]
        public String GCBloodType
        {
            get { return _GCBloodType; }
            set { _GCBloodType = value; }
        }
        [Column(Name = "GCLanguage", DataType = "String")]
        public String GCLanguage
        {
            get { return _GCLanguage; }
            set { _GCLanguage = value; }
        }
        [Column(Name = "HomeDistance", DataType = "Decimal")]
        public Decimal HomeDistance
        {
            get { return _HomeDistance; }
            set { _HomeDistance = value; }
        }
        [Column(Name = "MedicalHistory", DataType = "String")]
        public String MedicalHistory
        {
            get { return _MedicalHistory; }
            set { _MedicalHistory = value; }
        }
    }
    #endregion
    #region vProspectiveStudentAchievement
    [Serializable]
    [Table(Name = "vProspectiveStudentAchievement")]
    public partial class vProspectiveStudentAchievement
    {
        private Int32 _ProspectiveStudentAchievementID;
        private String _ProspectiveStudentCode;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _ProspectiveStudentName;
        private String _Name;
        private Int32 _ProspectiveStudentID;
        private DateTime _AchievementDate;
        private String _GCAchievementType;
        private String _AchievementType;
        private String _AchievementName;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "ProspectiveStudentAchievementID", DataType = "Int32")]
        public Int32 ProspectiveStudentAchievementID
        {
            get { return _ProspectiveStudentAchievementID; }
            set { _ProspectiveStudentAchievementID = value; }
        }
        [Column(Name = "ProspectiveStudentCode", DataType = "String")]
        public String ProspectiveStudentCode
        {
            get { return _ProspectiveStudentCode; }
            set { _ProspectiveStudentCode = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "ProspectiveStudentName", DataType = "String")]
        public String ProspectiveStudentName
        {
            get { return _ProspectiveStudentName; }
            set { _ProspectiveStudentName = value; }
        }
        [Column(Name = "Name", DataType = "String")]
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        [Column(Name = "ProspectiveStudentID", DataType = "Int32")]
        public Int32 ProspectiveStudentID
        {
            get { return _ProspectiveStudentID; }
            set { _ProspectiveStudentID = value; }
        }
        [Column(Name = "AchievementDate", DataType = "DateTime")]
        public DateTime AchievementDate
        {
            get { return _AchievementDate; }
            set { _AchievementDate = value; }
        }
        [Column(Name = "GCAchievementType", DataType = "String")]
        public String GCAchievementType
        {
            get { return _GCAchievementType; }
            set { _GCAchievementType = value; }
        }
        [Column(Name = "AchievementType", DataType = "String")]
        public String AchievementType
        {
            get { return _AchievementType; }
            set { _AchievementType = value; }
        }
        [Column(Name = "AchievementName", DataType = "String")]
        public String AchievementName
        {
            get { return _AchievementName; }
            set { _AchievementName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vProspectiveStudentFamily
    [Serializable]
    [Table(Name = "vProspectiveStudentFamily")]
    public partial class vProspectiveStudentFamily
    {
        private Int32 _FamilyID;
        private Int32 _ProspectiveStudentID;
        private String _GCFamilyRelation;
        private String _FamilyRelation;
        private String _GCSalutation;
        private String _GCTitle;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _FullName;
        private String _FamilyName;
        private String _CityOfBirth;
        private DateTime _DateOfBirth;
        private String _GCSuffix;
        private String _GCReligion;
        private String _GCNationality;
        private String _GCEducationLevel;
        private String _CompanyName;
        private String _GCJob;
        private String _Occupation;
        private Decimal _Salary;
        private String _OfficeAddressID;
        private String _OfficeStreetName;
        private String _OfficeDistrict;
        private String _OfficeCity;
        private String _OfficeCounty;
        private String _OfficeGCState;
        private String _OfficeState;
        private Int32 _OfficeZipCodeID;
        private String _OfficeZipCode;
        private String _OfficePhoneNo1;
        private String _EmailAddress;
        private String _MobilePhoneNo1;
        private String _MobilePhoneNo2;
        private Boolean _IsDeleted;

        [Column(Name = "FamilyID", DataType = "Int32")]
        public Int32 FamilyID
        {
            get { return _FamilyID; }
            set { _FamilyID = value; }
        }
        [Column(Name = "ProspectiveStudentID", DataType = "Int32")]
        public Int32 ProspectiveStudentID
        {
            get { return _ProspectiveStudentID; }
            set { _ProspectiveStudentID = value; }
        }
        [Column(Name = "GCFamilyRelation", DataType = "String")]
        public String GCFamilyRelation
        {
            get { return _GCFamilyRelation; }
            set { _GCFamilyRelation = value; }
        }
        [Column(Name = "FamilyRelation", DataType = "String")]
        public String FamilyRelation
        {
            get { return _FamilyRelation; }
            set { _FamilyRelation = value; }
        }
        [Column(Name = "GCSalutation", DataType = "String")]
        public String GCSalutation
        {
            get { return _GCSalutation; }
            set { _GCSalutation = value; }
        }
        [Column(Name = "GCTitle", DataType = "String")]
        public String GCTitle
        {
            get { return _GCTitle; }
            set { _GCTitle = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "FullName", DataType = "String")]
        public String FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
        [Column(Name = "FamilyName", DataType = "String")]
        public String FamilyName
        {
            get { return _FamilyName; }
            set { _FamilyName = value; }
        }
        [Column(Name = "CityOfBirth", DataType = "String")]
        public String CityOfBirth
        {
            get { return _CityOfBirth; }
            set { _CityOfBirth = value; }
        }
        [Column(Name = "DateOfBirth", DataType = "DateTime")]
        public DateTime DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }
        [Column(Name = "GCSuffix", DataType = "String")]
        public String GCSuffix
        {
            get { return _GCSuffix; }
            set { _GCSuffix = value; }
        }
        [Column(Name = "GCReligion", DataType = "String")]
        public String GCReligion
        {
            get { return _GCReligion; }
            set { _GCReligion = value; }
        }
        [Column(Name = "GCNationality", DataType = "String")]
        public String GCNationality
        {
            get { return _GCNationality; }
            set { _GCNationality = value; }
        }
        [Column(Name = "GCEducationLevel", DataType = "String")]
        public String GCEducationLevel
        {
            get { return _GCEducationLevel; }
            set { _GCEducationLevel = value; }
        }
        [Column(Name = "CompanyName", DataType = "String")]
        public String CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        [Column(Name = "GCJob", DataType = "String")]
        public String GCJob
        {
            get { return _GCJob; }
            set { _GCJob = value; }
        }
        [Column(Name = "Occupation", DataType = "String")]
        public String Occupation
        {
            get { return _Occupation; }
            set { _Occupation = value; }
        }
        [Column(Name = "Salary", DataType = "Decimal")]
        public Decimal Salary
        {
            get { return _Salary; }
            set { _Salary = value; }
        }
        [Column(Name = "OfficeAddressID", DataType = "String")]
        public String OfficeAddressID
        {
            get { return _OfficeAddressID; }
            set { _OfficeAddressID = value; }
        }
        [Column(Name = "OfficeStreetName", DataType = "String")]
        public String OfficeStreetName
        {
            get { return _OfficeStreetName; }
            set { _OfficeStreetName = value; }
        }
        [Column(Name = "OfficeDistrict", DataType = "String")]
        public String OfficeDistrict
        {
            get { return _OfficeDistrict; }
            set { _OfficeDistrict = value; }
        }
        [Column(Name = "OfficeCity", DataType = "String")]
        public String OfficeCity
        {
            get { return _OfficeCity; }
            set { _OfficeCity = value; }
        }
        [Column(Name = "OfficeCounty", DataType = "String")]
        public String OfficeCounty
        {
            get { return _OfficeCounty; }
            set { _OfficeCounty = value; }
        }
        [Column(Name = "OfficeGCState", DataType = "String")]
        public String OfficeGCState
        {
            get { return _OfficeGCState; }
            set { _OfficeGCState = value; }
        }
        [Column(Name = "OfficeState", DataType = "String")]
        public String OfficeState
        {
            get { return _OfficeState; }
            set { _OfficeState = value; }
        }
        [Column(Name = "OfficeZipCodeID", DataType = "Int32")]
        public Int32 OfficeZipCodeID
        {
            get { return _OfficeZipCodeID; }
            set { _OfficeZipCodeID = value; }
        }
        [Column(Name = "OfficeZipCode", DataType = "String")]
        public String OfficeZipCode
        {
            get { return _OfficeZipCode; }
            set { _OfficeZipCode = value; }
        }
        [Column(Name = "OfficePhoneNo1", DataType = "String")]
        public String OfficePhoneNo1
        {
            get { return _OfficePhoneNo1; }
            set { _OfficePhoneNo1 = value; }
        }
        [Column(Name = "EmailAddress", DataType = "String")]
        public String EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        [Column(Name = "MobilePhoneNo1", DataType = "String")]
        public String MobilePhoneNo1
        {
            get { return _MobilePhoneNo1; }
            set { _MobilePhoneNo1 = value; }
        }
        [Column(Name = "MobilePhoneNo2", DataType = "String")]
        public String MobilePhoneNo2
        {
            get { return _MobilePhoneNo2; }
            set { _MobilePhoneNo2 = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vProspectiveStudentFolder
    [Serializable]
    [Table(Name = "vProspectiveStudentFolder")]
    public class vProspectiveStudentFolder
    {
        private String _SiteID;
        private String _SiteName;
        private Int32 _FormID;
        private String _FormCode;
        private String _FormName;
        private Boolean _IsDeleted;

        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "SiteName", DataType = "String")]
        public String SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }
        [Column(Name = "FormID", DataType = "Int32")]
        public Int32 FormID
        {
            get { return _FormID; }
            set { _FormID = value; }
        }
        [Column(Name = "FormCode", DataType = "String")]
        public String FormCode
        {
            get { return _FormCode; }
            set { _FormCode = value; }
        }
        [Column(Name = "FormName", DataType = "String")]
        public String FormName
        {
            get { return _FormName; }
            set { _FormName = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vProspectiveStudentForm
    [Serializable]
    [Table(Name = "vProspectiveStudentForm")]
    public class vProspectiveStudentForm
    {
        private Int32 _FormID;
        private String _SiteID;
        private String _FormCode;
        private String _FormName;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "FormID", DataType = "Int32")]
        public Int32 FormID
        {
            get { return _FormID; }
            set { _FormID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "FormCode", DataType = "String")]
        public String FormCode
        {
            get { return _FormCode; }
            set { _FormCode = value; }
        }
        [Column(Name = "FormName", DataType = "String")]
        public String FormName
        {
            get { return _FormName; }
            set { _FormName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vProspectiveStudentPastStudy
    [Serializable]
    [Table(Name = "vProspectiveStudentPastStudy")]
    public class vProspectiveStudentPastStudy
    {
        private Int32 _ProspectiveStudentPastStudyID;
        private Int32 _ProspectiveStudentID;
        private Int32 _StartYear;
        private Int32 _EndYear;
        private String _GCSchoolType;
        private String _SchoolType;
        private String _SchoolName;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "ProspectiveStudentPastStudyID", DataType = "Int32")]
        public Int32 ProspectiveStudentPastStudyID
        {
            get { return _ProspectiveStudentPastStudyID; }
            set { _ProspectiveStudentPastStudyID = value; }
        }
        [Column(Name = "ProspectiveStudentID", DataType = "Int32")]
        public Int32 ProspectiveStudentID
        {
            get { return _ProspectiveStudentID; }
            set { _ProspectiveStudentID = value; }
        }
        [Column(Name = "StartYear", DataType = "Int32")]
        public Int32 StartYear
        {
            get { return _StartYear; }
            set { _StartYear = value; }
        }
        [Column(Name = "EndYear", DataType = "Int32")]
        public Int32 EndYear
        {
            get { return _EndYear; }
            set { _EndYear = value; }
        }
        [Column(Name = "GCSchoolType", DataType = "String")]
        public String GCSchoolType
        {
            get { return _GCSchoolType; }
            set { _GCSchoolType = value; }
        }
        [Column(Name = "SchoolType", DataType = "String")]
        public String SchoolType
        {
            get { return _SchoolType; }
            set { _SchoolType = value; }
        }
        [Column(Name = "SchoolName", DataType = "String")]
        public String SchoolName
        {
            get { return _SchoolName; }
            set { _SchoolName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vPurchaseOrderDt
    [Serializable]
    [Table(Name = "vPurchaseOrderDt")]
    public partial class vPurchaseOrderDt
    {
        private Int32 _ID;
        private Int32 _PurchaseOrderID;
        private String _PurchaseOrderNo;
        private DateTime _OrderDate;
        private Int32 _SupplierID;
        private String _SupplierCode;
        private String _SupplierName;
        private Int32 _LocationID;
        private String _GCTransactionStatus;
        private String _TransactionStatus;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Decimal _Quantity;
        private String _SupplierItemName;
        private String _SupplierItemCode;
        private String _GCPurchaseUnit;
        private String _PurchaseUnit;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private Decimal _ConversionFactor;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage1;
        private Decimal _DiscountAmount1;
        private Decimal _DiscountPercentage2;
        private Decimal _DiscountAmount2;
        private String _GCItemDetailStatus;
        private String _ReceivedInformation;
        private Decimal _ReceivedQuantity;
        private Decimal _LineAmount;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "PurchaseOrderID", DataType = "Int32")]
        public Int32 PurchaseOrderID
        {
            get { return _PurchaseOrderID; }
            set { _PurchaseOrderID = value; }
        }
        [Column(Name = "PurchaseOrderNo", DataType = "String")]
        public String PurchaseOrderNo
        {
            get { return _PurchaseOrderNo; }
            set { _PurchaseOrderNo = value; }
        }
        [Column(Name = "OrderDate", DataType = "DateTime")]
        public DateTime OrderDate
        {
            get { return _OrderDate; }
            set { _OrderDate = value; }
        }
        [Column(Name = "SupplierID", DataType = "Int32")]
        public Int32 SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
        }
        [Column(Name = "SupplierCode", DataType = "String")]
        public String SupplierCode
        {
            get { return _SupplierCode; }
            set { _SupplierCode = value; }
        }
        [Column(Name = "SupplierName", DataType = "String")]
        public String SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatus", DataType = "String")]
        public String TransactionStatus
        {
            get { return _TransactionStatus; }
            set { _TransactionStatus = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "SupplierItemName", DataType = "String")]
        public String SupplierItemName
        {
            get { return _SupplierItemName; }
            set { _SupplierItemName = value; }
        }
        [Column(Name = "SupplierItemCode", DataType = "String")]
        public String SupplierItemCode
        {
            get { return _SupplierItemCode; }
            set { _SupplierItemCode = value; }
        }
        [Column(Name = "GCPurchaseUnit", DataType = "String")]
        public String GCPurchaseUnit
        {
            get { return _GCPurchaseUnit; }
            set { _GCPurchaseUnit = value; }
        }
        [Column(Name = "PurchaseUnit", DataType = "String")]
        public String PurchaseUnit
        {
            get { return _PurchaseUnit; }
            set { _PurchaseUnit = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "UnitPrice", DataType = "Decimal")]
        public Decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        [Column(Name = "DiscountPercentage1", DataType = "Decimal")]
        public Decimal DiscountPercentage1
        {
            get { return _DiscountPercentage1; }
            set { _DiscountPercentage1 = value; }
        }
        [Column(Name = "DiscountAmount1", DataType = "Decimal")]
        public Decimal DiscountAmount1
        {
            get { return _DiscountAmount1; }
            set { _DiscountAmount1 = value; }
        }
        [Column(Name = "DiscountPercentage2", DataType = "Decimal")]
        public Decimal DiscountPercentage2
        {
            get { return _DiscountPercentage2; }
            set { _DiscountPercentage2 = value; }
        }
        [Column(Name = "DiscountAmount2", DataType = "Decimal")]
        public Decimal DiscountAmount2
        {
            get { return _DiscountAmount2; }
            set { _DiscountAmount2 = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
        [Column(Name = "ReceivedInformation", DataType = "String")]
        public String ReceivedInformation
        {
            get { return _ReceivedInformation; }
            set { _ReceivedInformation = value; }
        }
        [Column(Name = "ReceivedQuantity", DataType = "Decimal")]
        public Decimal ReceivedQuantity
        {
            get { return _ReceivedQuantity; }
            set { _ReceivedQuantity = value; }
        }
        [Column(Name = "LineAmount", DataType = "Decimal")]
        public Decimal LineAmount
        {
            get { return _LineAmount; }
            set { _LineAmount = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vPurchaseOrderDtOutStanding
    [Serializable]
    [Table(Name = "vPurchaseOrderDtOutStanding")]
    public partial class vPurchaseOrderDtOutStanding
    {
        private Int32 _ID;
        private Int32 _PurchaseReceiveID;
        private Int32 _PurchaseOrderID;
        private String _PurchaseOrderNo;
        private Int32 _ItemID;
        private String _ItemName1;
        private String _ItemCode;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Decimal _OrderQuantity;
        private String _OrderPurchaseUnit;
        private Decimal _Quantity;
        private String _SupplierItemName;
        private String _SupplierItemCode;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private String _GCItemUnit;
        private String _ItemUnit;
        private Decimal _ConversionFactor;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage1;
        private Decimal _DiscountPercentage2;
        private Boolean _IsBonusItem;
        private Boolean _IsControlExpired;
        private String _GCItemDetailStatus;
        private Int32 _CreatedBy;
        private String _UserName;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "PurchaseReceiveID", DataType = "Int32")]
        public Int32 PurchaseReceiveID
        {
            get { return _PurchaseReceiveID; }
            set { _PurchaseReceiveID = value; }
        }
        [Column(Name = "PurchaseOrderID", DataType = "Int32")]
        public Int32 PurchaseOrderID
        {
            get { return _PurchaseOrderID; }
            set { _PurchaseOrderID = value; }
        }
        [Column(Name = "PurchaseOrderNo", DataType = "String")]
        public String PurchaseOrderNo
        {
            get { return _PurchaseOrderNo; }
            set { _PurchaseOrderNo = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "OrderQuantity", DataType = "Decimal")]
        public Decimal OrderQuantity
        {
            get { return _OrderQuantity; }
            set { _OrderQuantity = value; }
        }
        [Column(Name = "OrderPurchaseUnit", DataType = "String")]
        public String OrderPurchaseUnit
        {
            get { return _OrderPurchaseUnit; }
            set { _OrderPurchaseUnit = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "SupplierItemName", DataType = "String")]
        public String SupplierItemName
        {
            get { return _SupplierItemName; }
            set { _SupplierItemName = value; }
        }
        [Column(Name = "SupplierItemCode", DataType = "String")]
        public String SupplierItemCode
        {
            get { return _SupplierItemCode; }
            set { _SupplierItemCode = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "UnitPrice", DataType = "Decimal")]
        public Decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        [Column(Name = "DiscountPercentage1", DataType = "Decimal")]
        public Decimal DiscountPercentage1
        {
            get { return _DiscountPercentage1; }
            set { _DiscountPercentage1 = value; }
        }
        [Column(Name = "DiscountPercentage2", DataType = "Decimal")]
        public Decimal DiscountPercentage2
        {
            get { return _DiscountPercentage2; }
            set { _DiscountPercentage2 = value; }
        }
        [Column(Name = "IsBonusItem", DataType = "Boolean")]
        public Boolean IsBonusItem
        {
            get { return _IsBonusItem; }
            set { _IsBonusItem = value; }
        }
        [Column(Name = "IsControlExpired", DataType = "Boolean")]
        public Boolean IsControlExpired
        {
            get { return _IsControlExpired; }
            set { _IsControlExpired = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "UserName", DataType = "String")]
        public String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
    }
    #endregion
    #region vPurchaseOrderHd
    [Serializable]
    [Table(Name = "vPurchaseOrderHd")]
    public partial class vPurchaseOrderHd
    {
        private Int32 _PurchaseOrderID;
        private String _TransactionCode;
        private Int32 _LocationID;
        private String _LocationCode;
        private String _LocationName;
        private Int32 _LocationItemGroupID;
        private DateTime _OrderDate;
        private String _PurchaseOrderNo;
        private DateTime _DeliveryDate;
        private DateTime _POExpiredDate;
        private String _GCPurchaseOrderType;
        private String _PurchaseOrderType;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private Int32 _TermID;
        private String _TermName;
        private String _GCFrancoRegion;
        private String _FrancoRegion;
        private String _GCCurrencyCode;
        private String _CurrencyCode;
        private Decimal _CurrencyRate;
        private Boolean _IsIncludeVAT;
        private Decimal _TransactionAmount;
        private Decimal _TotalDtAmount;
        private Decimal _FinalDiscountPercentage;
        private Decimal _FinalDiscountAmount;
        private Decimal _VATPercentage;
        private Decimal _VATAmount;
        private Decimal _DownPaymentAmount;
        private Decimal _TotalNetTransactionAmount;
        private String _PaymentRemarks;
        private String _Remarks;
        private String _GCTransactionStatus;
        private String _TransactionStatus;
        private String _TransactionStatusWatermark;
        private Int32 _NumberOfItems;
        private Int32 _NumberOfApprovedItems;
        private String _CreatedByName;

        [Column(Name = "PurchaseOrderID", DataType = "Int32")]
        public Int32 PurchaseOrderID
        {
            get { return _PurchaseOrderID; }
            set { _PurchaseOrderID = value; }
        }
        [Column(Name = "TransactionCode", DataType = "String")]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "LocationCode", DataType = "String")]
        public String LocationCode
        {
            get { return _LocationCode; }
            set { _LocationCode = value; }
        }
        [Column(Name = "LocationName", DataType = "String")]
        public String LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }
        [Column(Name = "LocationItemGroupID", DataType = "Int32")]
        public Int32 LocationItemGroupID
        {
            get { return _LocationItemGroupID; }
            set { _LocationItemGroupID = value; }
        }
        [Column(Name = "OrderDate", DataType = "DateTime")]
        public DateTime OrderDate
        {
            get { return _OrderDate; }
            set { _OrderDate = value; }
        }
        [Column(Name = "PurchaseOrderNo", DataType = "String")]
        public String PurchaseOrderNo
        {
            get { return _PurchaseOrderNo; }
            set { _PurchaseOrderNo = value; }
        }
        [Column(Name = "DeliveryDate", DataType = "DateTime")]
        public DateTime DeliveryDate
        {
            get { return _DeliveryDate; }
            set { _DeliveryDate = value; }
        }
        [Column(Name = "POExpiredDate", DataType = "DateTime")]
        public DateTime POExpiredDate
        {
            get { return _POExpiredDate; }
            set { _POExpiredDate = value; }
        }
        [Column(Name = "GCPurchaseOrderType", DataType = "String")]
        public String GCPurchaseOrderType
        {
            get { return _GCPurchaseOrderType; }
            set { _GCPurchaseOrderType = value; }
        }
        [Column(Name = "PurchaseOrderType", DataType = "String")]
        public String PurchaseOrderType
        {
            get { return _PurchaseOrderType; }
            set { _PurchaseOrderType = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "TermID", DataType = "Int32")]
        public Int32 TermID
        {
            get { return _TermID; }
            set { _TermID = value; }
        }
        [Column(Name = "TermName", DataType = "String")]
        public String TermName
        {
            get { return _TermName; }
            set { _TermName = value; }
        }
        [Column(Name = "GCFrancoRegion", DataType = "String")]
        public String GCFrancoRegion
        {
            get { return _GCFrancoRegion; }
            set { _GCFrancoRegion = value; }
        }
        [Column(Name = "FrancoRegion", DataType = "String")]
        public String FrancoRegion
        {
            get { return _FrancoRegion; }
            set { _FrancoRegion = value; }
        }
        [Column(Name = "GCCurrencyCode", DataType = "String")]
        public String GCCurrencyCode
        {
            get { return _GCCurrencyCode; }
            set { _GCCurrencyCode = value; }
        }
        [Column(Name = "CurrencyCode", DataType = "String")]
        public String CurrencyCode
        {
            get { return _CurrencyCode; }
            set { _CurrencyCode = value; }
        }
        [Column(Name = "CurrencyRate", DataType = "Decimal")]
        public Decimal CurrencyRate
        {
            get { return _CurrencyRate; }
            set { _CurrencyRate = value; }
        }
        [Column(Name = "IsIncludeVAT", DataType = "Boolean")]
        public Boolean IsIncludeVAT
        {
            get { return _IsIncludeVAT; }
            set { _IsIncludeVAT = value; }
        }
        [Column(Name = "TransactionAmount", DataType = "Decimal")]
        public Decimal TransactionAmount
        {
            get { return _TransactionAmount; }
            set { _TransactionAmount = value; }
        }
        [Column(Name = "TotalDtAmount", DataType = "Decimal")]
        public Decimal TotalDtAmount
        {
            get { return _TotalDtAmount; }
            set { _TotalDtAmount = value; }
        }
        [Column(Name = "FinalDiscountPercentage", DataType = "Decimal")]
        public Decimal FinalDiscountPercentage
        {
            get { return _FinalDiscountPercentage; }
            set { _FinalDiscountPercentage = value; }
        }
        [Column(Name = "FinalDiscountAmount", DataType = "Decimal")]
        public Decimal FinalDiscountAmount
        {
            get { return _FinalDiscountAmount; }
            set { _FinalDiscountAmount = value; }
        }
        [Column(Name = "VATPercentage", DataType = "Decimal")]
        public Decimal VATPercentage
        {
            get { return _VATPercentage; }
            set { _VATPercentage = value; }
        }
        [Column(Name = "VATAmount", DataType = "Decimal")]
        public Decimal VATAmount
        {
            get { return _VATAmount; }
            set { _VATAmount = value; }
        }
        [Column(Name = "DownPaymentAmount", DataType = "Decimal")]
        public Decimal DownPaymentAmount
        {
            get { return _DownPaymentAmount; }
            set { _DownPaymentAmount = value; }
        }
        [Column(Name = "TotalNetTransactionAmount", DataType = "Decimal")]
        public Decimal TotalNetTransactionAmount
        {
            get { return _TotalNetTransactionAmount; }
            set { _TotalNetTransactionAmount = value; }
        }
        [Column(Name = "PaymentRemarks", DataType = "String")]
        public String PaymentRemarks
        {
            get { return _PaymentRemarks; }
            set { _PaymentRemarks = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatus", DataType = "String")]
        public String TransactionStatus
        {
            get { return _TransactionStatus; }
            set { _TransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
        [Column(Name = "NumberOfItems", DataType = "Int32")]
        public Int32 NumberOfItems
        {
            get { return _NumberOfItems; }
            set { _NumberOfItems = value; }
        }
        [Column(Name = "NumberOfApprovedItems", DataType = "Int32")]
        public Int32 NumberOfApprovedItems
        {
            get { return _NumberOfApprovedItems; }
            set { _NumberOfApprovedItems = value; }
        }
        [Column(Name = "CreatedByName", DataType = "String")]
        public String CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }
    }
    #endregion
    #region vPurchaseInvoiceDt
    [Serializable]
    [Table(Name = "vPurchaseInvoiceDt")]
    public partial class vPurchaseInvoiceDt
    {
        private Int32 _ID;
        private Int32 _PurchaseInvoiceID;
        private String _PurchaseInvoiceNo;
        private Int32 _PurchaseReceiveID;
        private String _PurchaseReceiveNo;
        private DateTime _PurchaseInvoiceDate;
        private DateTime _ReceivedDate;
        private DateTime _PaymentDueDate;
        private String _ReferenceNo;
        private DateTime _ReferenceDate;
        private String _BusinessPartnerName;
        private Decimal _TransactionAmount;
        private Decimal _DiscountAmount;
        private Decimal _FinalDiscountAmount;
        private Decimal _VATAmount;
        private Decimal _ChargesAmount;
        private Decimal _PPH23Amount;
        private Decimal _PPH25Amount;
        private Decimal _StampAmount;
        private Decimal _CreditNoteAmount;
        private Decimal _DownPaymentAmount;
        private String _GCTransactionStatus;
        private String _TransactionStatus;
        private Int32 _PurchaseReturnID;
        private Decimal _LineAmount;
        private Boolean _IsHasCreditNote;
        private Boolean _IsDeleted;
        private String _CreatedByUserName;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "PurchaseInvoiceID", DataType = "Int32")]
        public Int32 PurchaseInvoiceID
        {
            get { return _PurchaseInvoiceID; }
            set { _PurchaseInvoiceID = value; }
        }
        [Column(Name = "PurchaseInvoiceNo", DataType = "String")]
        public String PurchaseInvoiceNo
        {
            get { return _PurchaseInvoiceNo; }
            set { _PurchaseInvoiceNo = value; }
        }
        [Column(Name = "PurchaseReceiveID", DataType = "Int32")]
        public Int32 PurchaseReceiveID
        {
            get { return _PurchaseReceiveID; }
            set { _PurchaseReceiveID = value; }
        }
        [Column(Name = "PurchaseReceiveNo", DataType = "String")]
        public String PurchaseReceiveNo
        {
            get { return _PurchaseReceiveNo; }
            set { _PurchaseReceiveNo = value; }
        }
        [Column(Name = "PurchaseInvoiceDate", DataType = "DateTime")]
        public DateTime PurchaseInvoiceDate
        {
            get { return _PurchaseInvoiceDate; }
            set { _PurchaseInvoiceDate = value; }
        }
        [Column(Name = "ReceivedDate", DataType = "DateTime")]
        public DateTime ReceivedDate
        {
            get { return _ReceivedDate; }
            set { _ReceivedDate = value; }
        }
        [Column(Name = "PaymentDueDate", DataType = "DateTime")]
        public DateTime PaymentDueDate
        {
            get { return _PaymentDueDate; }
            set { _PaymentDueDate = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String")]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "ReferenceDate", DataType = "DateTime")]
        public DateTime ReferenceDate
        {
            get { return _ReferenceDate; }
            set { _ReferenceDate = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "TransactionAmount", DataType = "Decimal")]
        public Decimal TransactionAmount
        {
            get { return _TransactionAmount; }
            set { _TransactionAmount = value; }
        }
        [Column(Name = "DiscountAmount", DataType = "Decimal")]
        public Decimal DiscountAmount
        {
            get { return _DiscountAmount; }
            set { _DiscountAmount = value; }
        }
        [Column(Name = "FinalDiscountAmount", DataType = "Decimal")]
        public Decimal FinalDiscountAmount
        {
            get { return _FinalDiscountAmount; }
            set { _FinalDiscountAmount = value; }
        }
        [Column(Name = "VATAmount", DataType = "Decimal")]
        public Decimal VATAmount
        {
            get { return _VATAmount; }
            set { _VATAmount = value; }
        }
        [Column(Name = "ChargesAmount", DataType = "Decimal")]
        public Decimal ChargesAmount
        {
            get { return _ChargesAmount; }
            set { _ChargesAmount = value; }
        }
        [Column(Name = "PPH23Amount", DataType = "Decimal")]
        public Decimal PPH23Amount
        {
            get { return _PPH23Amount; }
            set { _PPH23Amount = value; }
        }
        [Column(Name = "PPH25Amount", DataType = "Decimal")]
        public Decimal PPH25Amount
        {
            get { return _PPH25Amount; }
            set { _PPH25Amount = value; }
        }
        [Column(Name = "StampAmount", DataType = "Decimal")]
        public Decimal StampAmount
        {
            get { return _StampAmount; }
            set { _StampAmount = value; }
        }
        [Column(Name = "CreditNoteAmount", DataType = "Decimal")]
        public Decimal CreditNoteAmount
        {
            get { return _CreditNoteAmount; }
            set { _CreditNoteAmount = value; }
        }
        [Column(Name = "DownPaymentAmount", DataType = "Decimal")]
        public Decimal DownPaymentAmount
        {
            get { return _DownPaymentAmount; }
            set { _DownPaymentAmount = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatus", DataType = "String")]
        public String TransactionStatus
        {
            get { return _TransactionStatus; }
            set { _TransactionStatus = value; }
        }
        [Column(Name = "PurchaseReturnID", DataType = "Int32")]
        public Int32 PurchaseReturnID
        {
            get { return _PurchaseReturnID; }
            set { _PurchaseReturnID = value; }
        }
        [Column(Name = "LineAmount", DataType = "Decimal")]
        public Decimal LineAmount
        {
            get { return _LineAmount; }
            set { _LineAmount = value; }
        }
        [Column(Name = "IsHasCreditNote", DataType = "Boolean")]
        public Boolean IsHasCreditNote
        {
            get { return _IsHasCreditNote; }
            set { _IsHasCreditNote = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedByUserName", DataType = "String")]
        public String CreatedByUserName
        {
            get { return _CreatedByUserName; }
            set { _CreatedByUserName = value; }
        }
    }
    #endregion
    #region vPurchaseInvoiceHd
    [Serializable]
    [Table(Name = "vPurchaseInvoiceHd")]
    public partial class vPurchaseInvoiceHd : DbDataModel
    {
        private Int32 _PurchaseInvoiceID;
        private DateTime _PurchaseInvoiceDate;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private String _PurchaseInvoiceNo;
        private String _SupplierInvoiceNo;
        private DateTime _SupplierInvoiceDate;
        private String _TaxInvoiceNo;
        private DateTime _TaxInvoiceDate;
        private DateTime _DueDate;
        private String _GCCurrencyCode;
        private String _CurrencyCode;
        private Decimal _CurrencyRate;
        private Decimal _TotalTransactionAmount;
        private Decimal _TotalDownPaymentAmount;
        private Decimal _TotalCreditNoteAmount;
        private Decimal _FinalDiscount;
        private Decimal _VATPercentage;
        private Decimal _PPHPercentage;
        private Decimal _ChargesAmount;
        private Decimal _StampAmount;
        private Decimal _TotalNetTransactionAmount;
        private Int16 _NumberOfPayment;
        private Decimal _PaymentAmount;
        private String _Remarks;
        private String _GCTransactionStatus;
        private String _TransactionStatus;
        private String _TransactionStatusWatermark;
        private Boolean _IsVerified;
        private Int32? _VerifiedBy;
        private DateTime _VerifiedDate;

        [Column(Name = "PurchaseInvoiceID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 PurchaseInvoiceID
        {
            get { return _PurchaseInvoiceID; }
            set { _PurchaseInvoiceID = value; }
        }
        [Column(Name = "PurchaseInvoiceDate", DataType = "DateTime")]
        public DateTime PurchaseInvoiceDate
        {
            get { return _PurchaseInvoiceDate; }
            set { _PurchaseInvoiceDate = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "PurchaseInvoiceNo", DataType = "String")]
        public String PurchaseInvoiceNo
        {
            get { return _PurchaseInvoiceNo; }
            set { _PurchaseInvoiceNo = value; }
        }
        [Column(Name = "SupplierInvoiceNo", DataType = "String")]
        public String SupplierInvoiceNo
        {
            get { return _SupplierInvoiceNo; }
            set { _SupplierInvoiceNo = value; }
        }
        [Column(Name = "SupplierInvoiceDate", DataType = "DateTime")]
        public DateTime SupplierInvoiceDate
        {
            get { return _SupplierInvoiceDate; }
            set { _SupplierInvoiceDate = value; }
        }
        [Column(Name = "TaxInvoiceNo", DataType = "String", IsNullable = true)]
        public String TaxInvoiceNo
        {
            get { return _TaxInvoiceNo; }
            set { _TaxInvoiceNo = value; }
        }
        [Column(Name = "TaxInvoiceDate", DataType = "DateTime", IsNullable = true)]
        public DateTime TaxInvoiceDate
        {
            get { return _TaxInvoiceDate; }
            set { _TaxInvoiceDate = value; }
        }
        [Column(Name = "DueDate", DataType = "DateTime")]
        public DateTime DueDate
        {
            get { return _DueDate; }
            set { _DueDate = value; }
        }
        [Column(Name = "GCCurrencyCode", DataType = "String")]
        public String GCCurrencyCode
        {
            get { return _GCCurrencyCode; }
            set { _GCCurrencyCode = value; }
        }
        [Column(Name = "CurrencyCode", DataType = "String")]
        public String CurrencyCode
        {
            get { return _CurrencyCode; }
            set { _CurrencyCode = value; }
        }
        [Column(Name = "CurrencyRate", DataType = "Decimal")]
        public Decimal CurrencyRate
        {
            get { return _CurrencyRate; }
            set { _CurrencyRate = value; }
        }
        [Column(Name = "TotalTransactionAmount", DataType = "Decimal")]
        public Decimal TotalTransactionAmount
        {
            get { return _TotalTransactionAmount; }
            set { _TotalTransactionAmount = value; }
        }
        [Column(Name = "TotalDownPaymentAmount", DataType = "Decimal")]
        public Decimal TotalDownPaymentAmount
        {
            get { return _TotalDownPaymentAmount; }
            set { _TotalDownPaymentAmount = value; }
        }
        [Column(Name = "TotalCreditNoteAmount", DataType = "Decimal")]
        public Decimal TotalCreditNoteAmount
        {
            get { return _TotalCreditNoteAmount; }
            set { _TotalCreditNoteAmount = value; }
        }
        [Column(Name = "FinalDiscount", DataType = "Decimal")]
        public Decimal FinalDiscount
        {
            get { return _FinalDiscount; }
            set { _FinalDiscount = value; }
        }
        [Column(Name = "VATPercentage", DataType = "Decimal")]
        public Decimal VATPercentage
        {
            get { return _VATPercentage; }
            set { _VATPercentage = value; }
        }
        [Column(Name = "PPHPercentage", DataType = "Decimal")]
        public Decimal PPHPercentage
        {
            get { return _PPHPercentage; }
            set { _PPHPercentage = value; }
        }
        [Column(Name = "ChargesAmount", DataType = "Decimal")]
        public Decimal ChargesAmount
        {
            get { return _ChargesAmount; }
            set { _ChargesAmount = value; }
        }
        [Column(Name = "StampAmount", DataType = "Decimal")]
        public Decimal StampAmount
        {
            get { return _StampAmount; }
            set { _StampAmount = value; }
        }
        [Column(Name = "TotalNetTransactionAmount", DataType = "Decimal")]
        public Decimal TotalNetTransactionAmount
        {
            get { return _TotalNetTransactionAmount; }
            set { _TotalNetTransactionAmount = value; }
        }
        [Column(Name = "NumberOfPayment", DataType = "Int16")]
        public Int16 NumberOfPayment
        {
            get { return _NumberOfPayment; }
            set { _NumberOfPayment = value; }
        }
        [Column(Name = "PaymentAmount", DataType = "Decimal")]
        public Decimal PaymentAmount
        {
            get { return _PaymentAmount; }
            set { _PaymentAmount = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatus", DataType = "String")]
        public String TransactionStatus
        {
            get { return _TransactionStatus; }
            set { _TransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
        [Column(Name = "IsVerified", DataType = "Boolean")]
        public Boolean IsVerified
        {
            get { return _IsVerified; }
            set { _IsVerified = value; }
        }
        [Column(Name = "VerifiedBy", DataType = "Int32", IsNullable = true)]
        public Int32? VerifiedBy
        {
            get { return _VerifiedBy; }
            set { _VerifiedBy = value; }
        }
        [Column(Name = "VerifiedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime VerifiedDate
        {
            get { return _VerifiedDate; }
            set { _VerifiedDate = value; }
        }
    }
    #endregion
    #region vPurchaseInvoiceHdPayment
    [Serializable]
    [Table(Name = "vPurchaseInvoiceHdPayment")]
    public partial class vPurchaseInvoiceHdPayment : DbDataModel
    {
        private Int32 _PurchaseInvoiceID;
        private Int32 _SupplierPaymentID;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private String _PurchaseInvoiceNo;
        private DateTime _DueDate;
        private DateTime _PurchaseInvoiceDate;
        private Decimal _TotalTransactionAmount;
        private Decimal _TotalDownPaymentAmount;
        private Decimal _TotalCreditNoteAmount;
        private Decimal _FinalDiscount;
        private Decimal _VATPercentage;
        private Decimal _PPHPercentage;
        private Decimal _ChargesAmount;
        private Decimal _StampAmount;
        private Decimal _TotalNetTransactionAmount;
        private Int16 _NumberOfPayment;
        private Decimal _PaymentAmount;
        private Boolean _IsVerified;

        [Column(Name = "PurchaseInvoiceID", DataType = "Int32")]
        public Int32 PurchaseInvoiceID
        {
            get { return _PurchaseInvoiceID; }
            set { _PurchaseInvoiceID = value; }
        }
        [Column(Name = "SupplierPaymentID", DataType = "Int32")]
        public Int32 SupplierPaymentID
        {
            get { return _SupplierPaymentID; }
            set { _SupplierPaymentID = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "PurchaseInvoiceNo", DataType = "String")]
        public String PurchaseInvoiceNo
        {
            get { return _PurchaseInvoiceNo; }
            set { _PurchaseInvoiceNo = value; }
        }
        [Column(Name = "DueDate", DataType = "DateTime")]
        public DateTime DueDate
        {
            get { return _DueDate; }
            set { _DueDate = value; }
        }
        [Column(Name = "PurchaseInvoiceDate", DataType = "DateTime")]
        public DateTime PurchaseInvoiceDate
        {
            get { return _PurchaseInvoiceDate; }
            set { _PurchaseInvoiceDate = value; }
        }
        [Column(Name = "TotalTransactionAmount", DataType = "Decimal")]
        public Decimal TotalTransactionAmount
        {
            get { return _TotalTransactionAmount; }
            set { _TotalTransactionAmount = value; }
        }
        [Column(Name = "TotalDownPaymentAmount", DataType = "Decimal")]
        public Decimal TotalDownPaymentAmount
        {
            get { return _TotalDownPaymentAmount; }
            set { _TotalDownPaymentAmount = value; }
        }
        [Column(Name = "TotalCreditNoteAmount", DataType = "Decimal")]
        public Decimal TotalCreditNoteAmount
        {
            get { return _TotalCreditNoteAmount; }
            set { _TotalCreditNoteAmount = value; }
        }
        [Column(Name = "FinalDiscount", DataType = "Decimal")]
        public Decimal FinalDiscount
        {
            get { return _FinalDiscount; }
            set { _FinalDiscount = value; }
        }
        [Column(Name = "VATPercentage", DataType = "Decimal")]
        public Decimal VATPercentage
        {
            get { return _VATPercentage; }
            set { _VATPercentage = value; }
        }
        [Column(Name = "PPHPercentage", DataType = "Decimal")]
        public Decimal PPHPercentage
        {
            get { return _PPHPercentage; }
            set { _PPHPercentage = value; }
        }
        [Column(Name = "ChargesAmount", DataType = "Decimal")]
        public Decimal ChargesAmount
        {
            get { return _ChargesAmount; }
            set { _ChargesAmount = value; }
        }
        [Column(Name = "StampAmount", DataType = "Decimal")]
        public Decimal StampAmount
        {
            get { return _StampAmount; }
            set { _StampAmount = value; }
        }
        [Column(Name = "TotalNetTransactionAmount", DataType = "Decimal")]
        public Decimal TotalNetTransactionAmount
        {
            get { return _TotalNetTransactionAmount; }
            set { _TotalNetTransactionAmount = value; }
        }
        [Column(Name = "NumberOfPayment", DataType = "Int16")]
        public Int16 DownPaymentAmount
        {
            get { return _NumberOfPayment; }
            set { _NumberOfPayment = value; }
        }
        [Column(Name = "PaymentAmount", DataType = "Decimal")]
        public Decimal PaymentAmount
        {
            get { return _PaymentAmount; }
            set { _PaymentAmount = value; }
        }
        [Column(Name = "IsVerified", DataType = "Boolean")]
        public Boolean IsVerified
        {
            get { return _IsVerified; }
            set { _IsVerified = value; }
        }
    }
    #endregion
    #region vPurchaseReceiveCredit
    [Serializable]
    [Table(Name = "vPurchaseReceiveCredit")]
    public partial class vPurchaseReceiveCredit
    {
        private Int32 _PurchaseReceiveID;
        private Int32 _BusinessPartnerID;
        private String _SupplierCode;
        private String _SupplierName;
        private String _PurchaseReceiveNo;
        private DateTime _ReceivedDate;
        private Decimal _TransactionAmount;
        private Decimal _DownPaymentAmount;
        private Decimal _ChargesAmount;
        private Decimal _FinalDiscountPercentage;
        private Decimal _FinalDiscountAmount;
        private Decimal _StampAmount;
        private Decimal _VATPercentage;
        private Decimal _VATAmount;
        private Decimal _TotalNetTransactionAmount;
        private DateTime _ReferenceDate;
        private String _ReferenceNo;
        private String _GCTransactionStatus;
        private Decimal _CNAmount;
        private DateTime _PaymentDueDate;
        private String _CreatedByName;

        [Column(Name = "PurchaseReceiveID", DataType = "Int32")]
        public Int32 PurchaseReceiveID
        {
            get { return _PurchaseReceiveID; }
            set { _PurchaseReceiveID = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "SupplierCode", DataType = "String")]
        public String SupplierCode
        {
            get { return _SupplierCode; }
            set { _SupplierCode = value; }
        }
        [Column(Name = "SupplierName", DataType = "String")]
        public String SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }
        [Column(Name = "PurchaseReceiveNo", DataType = "String")]
        public String PurchaseReceiveNo
        {
            get { return _PurchaseReceiveNo; }
            set { _PurchaseReceiveNo = value; }
        }
        [Column(Name = "ReceivedDate", DataType = "DateTime")]
        public DateTime ReceivedDate
        {
            get { return _ReceivedDate; }
            set { _ReceivedDate = value; }
        }
        [Column(Name = "TransactionAmount", DataType = "Decimal")]
        public Decimal TransactionAmount
        {
            get { return _TransactionAmount; }
            set { _TransactionAmount = value; }
        }
        [Column(Name = "DownPaymentAmount", DataType = "Decimal")]
        public Decimal DownPaymentAmount
        {
            get { return _DownPaymentAmount; }
            set { _DownPaymentAmount = value; }
        }
        [Column(Name = "ChargesAmount", DataType = "Decimal")]
        public Decimal ChargesAmount
        {
            get { return _ChargesAmount; }
            set { _ChargesAmount = value; }
        }
        [Column(Name = "FinalDiscountPercentage", DataType = "Decimal")]
        public Decimal FinalDiscountPercentage
        {
            get { return _FinalDiscountPercentage; }
            set { _FinalDiscountPercentage = value; }
        }
        [Column(Name = "FinalDiscountAmount", DataType = "Decimal")]
        public Decimal FinalDiscountAmount
        {
            get { return _FinalDiscountAmount; }
            set { _FinalDiscountAmount = value; }
        }
        [Column(Name = "StampAmount", DataType = "Decimal")]
        public Decimal StampAmount
        {
            get { return _StampAmount; }
            set { _StampAmount = value; }
        }
        [Column(Name = "VATPercentage", DataType = "Decimal")]
        public Decimal VATPercentage
        {
            get { return _VATPercentage; }
            set { _VATPercentage = value; }
        }
        [Column(Name = "VATAmount", DataType = "Decimal")]
        public Decimal VATAmount
        {
            get { return _VATAmount; }
            set { _VATAmount = value; }
        }
        [Column(Name = "TotalNetTransactionAmount", DataType = "Decimal")]
        public Decimal TotalNetTransactionAmount
        {
            get { return _TotalNetTransactionAmount; }
            set { _TotalNetTransactionAmount = value; }
        }
        [Column(Name = "ReferenceDate", DataType = "DateTime")]
        public DateTime ReferenceDate
        {
            get { return _ReferenceDate; }
            set { _ReferenceDate = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String")]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "CNAmount", DataType = "Decimal")]
        public Decimal CNAmount
        {
            get { return _CNAmount; }
            set { _CNAmount = value; }
        }
        [Column(Name = "PaymentDueDate", DataType = "DateTime")]
        public DateTime PaymentDueDate
        {
            get { return _PaymentDueDate; }
            set { _PaymentDueDate = value; }
        }
        [Column(Name = "CreatedByName", DataType = "String")]
        public String CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }
    }
    #endregion
    #region vPurchaseReceiveDt
    [Serializable]
    [Table(Name = "vPurchaseReceiveDt")]
    public partial class vPurchaseReceiveDt
    {
        private Int32 _ID;
        private Int32 _PurchaseReceiveID;
        private String _PurchaseReceiveNo;
        private DateTime _ReceivedDate;
        private Int32 _PurchaseOrderID;
        private String _PurchaseOrderNo;
        private Int32 _ItemID;
        private String _ItemName1;
        private String _ItemCode;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Int32 _SupplierID;
        private Int32 _LocationID;
        private String _LocationName;
        private Decimal _OrderQuantity;
        private String _OrderPurchaseUnit;
        private Decimal _Quantity;
        private String _SupplierCode;
        private String _SupplierName;
        private String _SupplierItemName;
        private String _SupplierItemCode;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private String _GCItemUnit;
        private String _ItemUnit;
        private Decimal _ConversionFactor;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage1;
        private Decimal _DiscountAmount1;
        private Decimal _DiscountPercentage2;
        private Decimal _DiscountAmount2;
        private Decimal _LineAmount;
        private Boolean _IsBonusItem;
        private Boolean _IsControlExpired;
        private String _GCItemDetailStatus;
        private Decimal _VATPercentage;
        private Decimal _StampAmount;
        private Decimal _ChargesAmount;
        private String _ItemDetailStatus;
        private Int32 _CreatedBy;
        private String _UserName;
        private DateTime _CreatedDate;
        private Int32 _LastUpdatedBy;
        private String _LastUpdatedByName;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "PurchaseReceiveID", DataType = "Int32")]
        public Int32 PurchaseReceiveID
        {
            get { return _PurchaseReceiveID; }
            set { _PurchaseReceiveID = value; }
        }
        [Column(Name = "PurchaseReceiveNo", DataType = "String")]
        public String PurchaseReceiveNo
        {
            get { return _PurchaseReceiveNo; }
            set { _PurchaseReceiveNo = value; }
        }
        [Column(Name = "ReceivedDate", DataType = "DateTime")]
        public DateTime ReceivedDate
        {
            get { return _ReceivedDate; }
            set { _ReceivedDate = value; }
        }
        [Column(Name = "PurchaseOrderID", DataType = "Int32")]
        public Int32 PurchaseOrderID
        {
            get { return _PurchaseOrderID; }
            set { _PurchaseOrderID = value; }
        }
        [Column(Name = "PurchaseOrderNo", DataType = "String")]
        public String PurchaseOrderNo
        {
            get { return _PurchaseOrderNo; }
            set { _PurchaseOrderNo = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "SupplierID", DataType = "Int32")]
        public Int32 SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "LocationName", DataType = "String")]
        public String LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }
        [Column(Name = "OrderQuantity", DataType = "Decimal")]
        public Decimal OrderQuantity
        {
            get { return _OrderQuantity; }
            set { _OrderQuantity = value; }
        }
        [Column(Name = "OrderPurchaseUnit", DataType = "String")]
        public String OrderPurchaseUnit
        {
            get { return _OrderPurchaseUnit; }
            set { _OrderPurchaseUnit = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "SupplierCode", DataType = "String")]
        public String SupplierCode
        {
            get { return _SupplierCode; }
            set { _SupplierCode = value; }
        }
        [Column(Name = "SupplierName", DataType = "String")]
        public String SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }
        [Column(Name = "SupplierItemName", DataType = "String")]
        public String SupplierItemName
        {
            get { return _SupplierItemName; }
            set { _SupplierItemName = value; }
        }
        [Column(Name = "SupplierItemCode", DataType = "String")]
        public String SupplierItemCode
        {
            get { return _SupplierItemCode; }
            set { _SupplierItemCode = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "UnitPrice", DataType = "Decimal")]
        public Decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        [Column(Name = "DiscountPercentage1", DataType = "Decimal")]
        public Decimal DiscountPercentage1
        {
            get { return _DiscountPercentage1; }
            set { _DiscountPercentage1 = value; }
        }
        [Column(Name = "DiscountAmount1", DataType = "Decimal")]
        public Decimal DiscountAmount1
        {
            get { return _DiscountAmount1; }
            set { _DiscountAmount1 = value; }
        }
        [Column(Name = "DiscountPercentage2", DataType = "Decimal")]
        public Decimal DiscountPercentage2
        {
            get { return _DiscountPercentage2; }
            set { _DiscountPercentage2 = value; }
        }
        [Column(Name = "DiscountAmount2", DataType = "Decimal")]
        public Decimal DiscountAmount2
        {
            get { return _DiscountAmount2; }
            set { _DiscountAmount2 = value; }
        }
        [Column(Name = "LineAmount", DataType = "Decimal")]
        public Decimal LineAmount
        {
            get { return _LineAmount; }
            set { _LineAmount = value; }
        }
        [Column(Name = "IsBonusItem", DataType = "Boolean")]
        public Boolean IsBonusItem
        {
            get { return _IsBonusItem; }
            set { _IsBonusItem = value; }
        }
        [Column(Name = "IsControlExpired", DataType = "Boolean")]
        public Boolean IsControlExpired
        {
            get { return _IsControlExpired; }
            set { _IsControlExpired = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
        [Column(Name = "VATPercentage", DataType = "Decimal")]
        public Decimal VATPercentage
        {
            get { return _VATPercentage; }
            set { _VATPercentage = value; }
        }
        [Column(Name = "StampAmount", DataType = "Decimal")]
        public Decimal StampAmount
        {
            get { return _StampAmount; }
            set { _StampAmount = value; }
        }
        [Column(Name = "ChargesAmount", DataType = "Decimal")]
        public Decimal ChargesAmount
        {
            get { return _ChargesAmount; }
            set { _ChargesAmount = value; }
        }
        [Column(Name = "ItemDetailStatus", DataType = "String")]
        public String ItemDetailStatus
        {
            get { return _ItemDetailStatus; }
            set { _ItemDetailStatus = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "UserName", DataType = "String")]
        public String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32")]
        public Int32 LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedByName", DataType = "String")]
        public String LastUpdatedByName
        {
            get { return _LastUpdatedByName; }
            set { _LastUpdatedByName = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime")]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }
    #endregion
    #region vPurchaseReceiveDtFixedAsset
    [Serializable]
    [Table(Name = "vPurchaseReceiveDtFixedAsset")]
    public class vPurchaseReceiveDtFixedAsset : vPurchaseReceiveDt
    {
    }
    #endregion
    #region vPurchaseReceiveHd
    [Serializable]
    [Table(Name = "vPurchaseReceiveHd")]
    public partial class vPurchaseReceiveHd
    {
        private Int32 _PurchaseReceiveID;
        private String _TransactionCode;
        private String _PurchaseReceiveNo;
        private DateTime _ReceivedDate;
        private String _ReceivedTime;
        private Int32 _LocationID;
        private String _LocationCode;
        private String _LocationName;
        private Int32 _SupplierID;
        private String _SupplierCode;
        private String _SupplierName;
        private Int32 _TermID;
        private String _TermName;
        private String _ReferenceNo;
        private DateTime _ReferenceDate;
        private String _GCCurrencyCode;
        private String _CurrencyCode;
        private Decimal _CurrencyRate;
        private Boolean _IsIncludeVAT;
        private Decimal _TransactionAmount;
        private Decimal _FinalDiscountPercentage;
        private Decimal _FinalDiscountAmount;
        private String _GCChargesType;
        private String _ChargesType;
        private Decimal _ChargesAmount;
        private Decimal _StampAmount;
        private Decimal _VATPercentage;
        private Decimal _VATAmount;
        private Decimal _DownPaymentAmount;
        private String _DownPaymentReferenceNo;
        private Decimal _TotalNetTransactionAmount;
        private String _ReceivedBy;
        private String _Remarks;
        private DateTime _PaymentDueDate;
        private String _GCTransactionStatus;
        private Boolean _IsHasPurchaseReturn;
        private Int32 _PurchaseReturnID;
        private String _TransactionStatusWatermark;
        private String _CreatedByName;

        [Column(Name = "PurchaseReceiveID", DataType = "Int32")]
        public Int32 PurchaseReceiveID
        {
            get { return _PurchaseReceiveID; }
            set { _PurchaseReceiveID = value; }
        }
        [Column(Name = "TransactionCode", DataType = "String")]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
        [Column(Name = "PurchaseReceiveNo", DataType = "String")]
        public String PurchaseReceiveNo
        {
            get { return _PurchaseReceiveNo; }
            set { _PurchaseReceiveNo = value; }
        }
        [Column(Name = "ReceivedDate", DataType = "DateTime")]
        public DateTime ReceivedDate
        {
            get { return _ReceivedDate; }
            set { _ReceivedDate = value; }
        }
        [Column(Name = "ReceivedTime", DataType = "String")]
        public String ReceivedTime
        {
            get { return _ReceivedTime; }
            set { _ReceivedTime = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "LocationCode", DataType = "String")]
        public String LocationCode
        {
            get { return _LocationCode; }
            set { _LocationCode = value; }
        }
        [Column(Name = "LocationName", DataType = "String")]
        public String LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }
        [Column(Name = "SupplierID", DataType = "Int32")]
        public Int32 SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
        }
        [Column(Name = "SupplierCode", DataType = "String")]
        public String SupplierCode
        {
            get { return _SupplierCode; }
            set { _SupplierCode = value; }
        }
        [Column(Name = "SupplierName", DataType = "String")]
        public String SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }
        [Column(Name = "TermID", DataType = "Int32")]
        public Int32 TermID
        {
            get { return _TermID; }
            set { _TermID = value; }
        }
        [Column(Name = "TermName", DataType = "String")]
        public String TermName
        {
            get { return _TermName; }
            set { _TermName = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String")]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "ReferenceDate", DataType = "DateTime")]
        public DateTime ReferenceDate
        {
            get { return _ReferenceDate; }
            set { _ReferenceDate = value; }
        }
        [Column(Name = "GCCurrencyCode", DataType = "String")]
        public String GCCurrencyCode
        {
            get { return _GCCurrencyCode; }
            set { _GCCurrencyCode = value; }
        }
        [Column(Name = "CurrencyCode", DataType = "String")]
        public String CurrencyCode
        {
            get { return _CurrencyCode; }
            set { _CurrencyCode = value; }
        }
        [Column(Name = "CurrencyRate", DataType = "Decimal")]
        public Decimal CurrencyRate
        {
            get { return _CurrencyRate; }
            set { _CurrencyRate = value; }
        }
        [Column(Name = "IsIncludeVAT", DataType = "Boolean")]
        public Boolean IsIncludeVAT
        {
            get { return _IsIncludeVAT; }
            set { _IsIncludeVAT = value; }
        }
        [Column(Name = "TransactionAmount", DataType = "Decimal")]
        public Decimal TransactionAmount
        {
            get { return _TransactionAmount; }
            set { _TransactionAmount = value; }
        }
        [Column(Name = "FinalDiscountPercentage", DataType = "Decimal")]
        public Decimal FinalDiscountPercentage
        {
            get { return _FinalDiscountPercentage; }
            set { _FinalDiscountPercentage = value; }
        }
        [Column(Name = "FinalDiscountAmount", DataType = "Decimal")]
        public Decimal FinalDiscountAmount
        {
            get { return _FinalDiscountAmount; }
            set { _FinalDiscountAmount = value; }
        }
        [Column(Name = "GCChargesType", DataType = "String")]
        public String GCChargesType
        {
            get { return _GCChargesType; }
            set { _GCChargesType = value; }
        }
        [Column(Name = "ChargesType", DataType = "String")]
        public String ChargesType
        {
            get { return _ChargesType; }
            set { _ChargesType = value; }
        }
        [Column(Name = "ChargesAmount", DataType = "Decimal")]
        public Decimal ChargesAmount
        {
            get { return _ChargesAmount; }
            set { _ChargesAmount = value; }
        }
        [Column(Name = "StampAmount", DataType = "Decimal")]
        public Decimal StampAmount
        {
            get { return _StampAmount; }
            set { _StampAmount = value; }
        }
        [Column(Name = "VATPercentage", DataType = "Decimal")]
        public Decimal VATPercentage
        {
            get { return _VATPercentage; }
            set { _VATPercentage = value; }
        }
        [Column(Name = "VATAmount", DataType = "Decimal")]
        public Decimal VATAmount
        {
            get { return _VATAmount; }
            set { _VATAmount = value; }
        }
        [Column(Name = "DownPaymentAmount", DataType = "Decimal")]
        public Decimal DownPaymentAmount
        {
            get { return _DownPaymentAmount; }
            set { _DownPaymentAmount = value; }
        }
        [Column(Name = "DownPaymentReferenceNo", DataType = "String")]
        public String DownPaymentReferenceNo
        {
            get { return _DownPaymentReferenceNo; }
            set { _DownPaymentReferenceNo = value; }
        }
        [Column(Name = "TotalNetTransactionAmount", DataType = "Decimal")]
        public Decimal TotalNetTransactionAmount
        {
            get { return _TotalNetTransactionAmount; }
            set { _TotalNetTransactionAmount = value; }
        }
        [Column(Name = "ReceivedBy", DataType = "String")]
        public String ReceivedBy
        {
            get { return _ReceivedBy; }
            set { _ReceivedBy = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "PaymentDueDate", DataType = "DateTime")]
        public DateTime PaymentDueDate
        {
            get { return _PaymentDueDate; }
            set { _PaymentDueDate = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "IsHasPurchaseReturn", DataType = "Boolean")]
        public Boolean IsHasPurchaseReturn
        {
            get { return _IsHasPurchaseReturn; }
            set { _IsHasPurchaseReturn = value; }
        }
        [Column(Name = "PurchaseReturnID", DataType = "Int32")]
        public Int32 PurchaseReturnID
        {
            get { return _PurchaseReturnID; }
            set { _PurchaseReturnID = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
        [Column(Name = "CreatedByName", DataType = "String")]
        public String CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }
    }
    #endregion
    #region vPurchaseReplacementDt
    [Serializable]
    [Table(Name = "vPurchaseReplacementDt")]
    public partial class vPurchaseReplacementDt
    {
        private Int32 _ID;
        private Int32 _PurchaseReplacementID;
        private Int32 _FromItemID;
        private String _FromItemCode;
        private String _FromItemName1;
        private Decimal _FromQuantity;
        private String _FromBaseUnit;
        private String _FromItemUnit;
        private Decimal _FromConversionFactor;
        private Decimal _FromUnitPrice;
        private Int32 _ToItemID;
        private String _ToItemCode;
        private String _ToItemName1;
        private Decimal _Quantity;
        private String _GCItemUnit;
        private String _ItemUnit;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private Decimal _ConversionFactor;
        private Boolean _IsControlExpired;
        private String _GCItemDetailStatus;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "PurchaseReplacementID", DataType = "Int32")]
        public Int32 PurchaseReplacementID
        {
            get { return _PurchaseReplacementID; }
            set { _PurchaseReplacementID = value; }
        }
        [Column(Name = "FromItemID", DataType = "Int32")]
        public Int32 FromItemID
        {
            get { return _FromItemID; }
            set { _FromItemID = value; }
        }
        [Column(Name = "FromItemCode", DataType = "String")]
        public String FromItemCode
        {
            get { return _FromItemCode; }
            set { _FromItemCode = value; }
        }
        [Column(Name = "FromItemName1", DataType = "String")]
        public String FromItemName1
        {
            get { return _FromItemName1; }
            set { _FromItemName1 = value; }
        }
        [Column(Name = "FromQuantity", DataType = "Decimal")]
        public Decimal FromQuantity
        {
            get { return _FromQuantity; }
            set { _FromQuantity = value; }
        }
        [Column(Name = "FromBaseUnit", DataType = "String")]
        public String FromBaseUnit
        {
            get { return _FromBaseUnit; }
            set { _FromBaseUnit = value; }
        }
        [Column(Name = "FromItemUnit", DataType = "String")]
        public String FromItemUnit
        {
            get { return _FromItemUnit; }
            set { _FromItemUnit = value; }
        }
        [Column(Name = "FromConversionFactor", DataType = "Decimal")]
        public Decimal FromConversionFactor
        {
            get { return _FromConversionFactor; }
            set { _FromConversionFactor = value; }
        }
        [Column(Name = "FromUnitPrice", DataType = "Decimal")]
        public Decimal FromUnitPrice
        {
            get { return _FromUnitPrice; }
            set { _FromUnitPrice = value; }
        }
        [Column(Name = "ToItemID", DataType = "Int32")]
        public Int32 ToItemID
        {
            get { return _ToItemID; }
            set { _ToItemID = value; }
        }
        [Column(Name = "ToItemCode", DataType = "String")]
        public String ToItemCode
        {
            get { return _ToItemCode; }
            set { _ToItemCode = value; }
        }
        [Column(Name = "ToItemName1", DataType = "String")]
        public String ToItemName1
        {
            get { return _ToItemName1; }
            set { _ToItemName1 = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "IsControlExpired", DataType = "Boolean")]
        public Boolean IsControlExpired
        {
            get { return _IsControlExpired; }
            set { _IsControlExpired = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
    }
    #endregion
    #region vPurchaseReplacementHd
    [Serializable]
    [Table(Name = "vPurchaseReplacementHd")]
    public partial class vPurchaseReplacementHd
    {
        private Int32 _PurchaseReplacementID;
        private String _PurchaseReplacementNo;
        private DateTime _ReplacementDate;
        private Int32 _PurchaseReturnID;
        private String _PurchaseReturnNo;
        private Int32 _LocationID;
        private String _LocationCode;
        private String _LocationName;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private String _ReferenceNo;
        private DateTime _ReferenceDate;
        private String _Remarks;
        private String _GCTransactionStatus;
        private String _TransactionStatusWatermark;

        [Column(Name = "PurchaseReplacementID", DataType = "Int32")]
        public Int32 PurchaseReplacementID
        {
            get { return _PurchaseReplacementID; }
            set { _PurchaseReplacementID = value; }
        }
        [Column(Name = "PurchaseReplacementNo", DataType = "String")]
        public String PurchaseReplacementNo
        {
            get { return _PurchaseReplacementNo; }
            set { _PurchaseReplacementNo = value; }
        }
        [Column(Name = "ReplacementDate", DataType = "DateTime")]
        public DateTime ReplacementDate
        {
            get { return _ReplacementDate; }
            set { _ReplacementDate = value; }
        }
        [Column(Name = "PurchaseReturnID", DataType = "Int32")]
        public Int32 PurchaseReturnID
        {
            get { return _PurchaseReturnID; }
            set { _PurchaseReturnID = value; }
        }
        [Column(Name = "PurchaseReturnNo", DataType = "String")]
        public String PurchaseReturnNo
        {
            get { return _PurchaseReturnNo; }
            set { _PurchaseReturnNo = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "LocationCode", DataType = "String")]
        public String LocationCode
        {
            get { return _LocationCode; }
            set { _LocationCode = value; }
        }
        [Column(Name = "LocationName", DataType = "String")]
        public String LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String")]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "ReferenceDate", DataType = "DateTime")]
        public DateTime ReferenceDate
        {
            get { return _ReferenceDate; }
            set { _ReferenceDate = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
    }
    #endregion
    #region vPurchaseRequestDt
    [Serializable]
    [Table(Name = "vPurchaseRequestDt")]
    public partial class vPurchaseRequestDt
    {
        private Int32 _ID;
        private Int32 _PurchaseRequestID;
        private String _PurchaseRequestNo;
        private DateTime _TransactionDate;
        private Int32 _FromLocationID;
        private String _FromLocationCode;
        private String _FromLocationName;
        private Int32 _ItemId;
        private String _ItemCode;
        private String _ItemName1;
        private String _ItemName2;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private Decimal _Quantity;
        private Decimal _POQuantity;
        private String _GCPurchaseUnit;
        private String _PurchaseUnit;
        private Decimal _ConversionFactor;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private String _SupplierItemCode;
        private String _SupplierItemName;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage;
        private String _GCItemDetailStatus;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32 _ItemGroupId;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Decimal _EndingBalance;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "PurchaseRequestID", DataType = "Int32")]
        public Int32 PurchaseRequestID
        {
            get { return _PurchaseRequestID; }
            set { _PurchaseRequestID = value; }
        }
        [Column(Name = "PurchaseRequestNo", DataType = "String")]
        public String PurchaseRequestNo
        {
            get { return _PurchaseRequestNo; }
            set { _PurchaseRequestNo = value; }
        }
        [Column(Name = "TransactionDate", DataType = "DateTime")]
        public DateTime TransactionDate
        {
            get { return _TransactionDate; }
            set { _TransactionDate = value; }
        }
        [Column(Name = "FromLocationID", DataType = "Int32")]
        public Int32 FromLocationID
        {
            get { return _FromLocationID; }
            set { _FromLocationID = value; }
        }
        [Column(Name = "FromLocationCode", DataType = "String")]
        public String FromLocationCode
        {
            get { return _FromLocationCode; }
            set { _FromLocationCode = value; }
        }
        [Column(Name = "FromLocationName", DataType = "String")]
        public String FromLocationName
        {
            get { return _FromLocationName; }
            set { _FromLocationName = value; }
        }
        [Column(Name = "ItemId", DataType = "Int32")]
        public Int32 ItemId
        {
            get { return _ItemId; }
            set { _ItemId = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemName2", DataType = "String")]
        public String ItemName2
        {
            get { return _ItemName2; }
            set { _ItemName2 = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "POQuantity", DataType = "Decimal")]
        public Decimal POQuantity
        {
            get { return _POQuantity; }
            set { _POQuantity = value; }
        }
        [Column(Name = "GCPurchaseUnit", DataType = "String")]
        public String GCPurchaseUnit
        {
            get { return _GCPurchaseUnit; }
            set { _GCPurchaseUnit = value; }
        }
        [Column(Name = "PurchaseUnit", DataType = "String")]
        public String PurchaseUnit
        {
            get { return _PurchaseUnit; }
            set { _PurchaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "SupplierItemCode", DataType = "String")]
        public String SupplierItemCode
        {
            get { return _SupplierItemCode; }
            set { _SupplierItemCode = value; }
        }
        [Column(Name = "SupplierItemName", DataType = "String")]
        public String SupplierItemName
        {
            get { return _SupplierItemName; }
            set { _SupplierItemName = value; }
        }
        [Column(Name = "UnitPrice", DataType = "Decimal")]
        public Decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        [Column(Name = "DiscountPercentage", DataType = "Decimal")]
        public Decimal DiscountPercentage
        {
            get { return _DiscountPercentage; }
            set { _DiscountPercentage = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "ItemGroupId", DataType = "Int32")]
        public Int32 ItemGroupId
        {
            get { return _ItemGroupId; }
            set { _ItemGroupId = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "EndingBalance", DataType = "Decimal")]
        public Decimal EndingBalance
        {
            get { return _EndingBalance; }
            set { _EndingBalance = value; }
        }
    }
    #endregion
    #region vPurchaseRequestDtOutstanding
    [Serializable]
    [Table(Name = "vPurchaseRequestDtOutstanding")]
    public partial class vPurchaseRequestDtOutstanding
    {
        private Int32 _ID;
        private String _PurchaseRequestNo;
        private Int32 _FromLocationID;
        private String _GCTransactionStatus;
        private Int32 _PurchaseRequestID;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private Decimal _Quantity;
        private String _GCPurchaseUnit;
        private String _PurchaseUnit;
        private Decimal _ConversionFactor;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private String _SupplierItemCode;
        private String _SupplierItemName;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage;
        private String _GCItemDetailStatus;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Decimal _QtyOnOrder;
        private Decimal _QuantityEND;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "PurchaseRequestNo", DataType = "String")]
        public String PurchaseRequestNo
        {
            get { return _PurchaseRequestNo; }
            set { _PurchaseRequestNo = value; }
        }
        [Column(Name = "FromLocationID", DataType = "Int32")]
        public Int32 FromLocationID
        {
            get { return _FromLocationID; }
            set { _FromLocationID = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "PurchaseRequestID", DataType = "Int32")]
        public Int32 PurchaseRequestID
        {
            get { return _PurchaseRequestID; }
            set { _PurchaseRequestID = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "GCPurchaseUnit", DataType = "String")]
        public String GCPurchaseUnit
        {
            get { return _GCPurchaseUnit; }
            set { _GCPurchaseUnit = value; }
        }
        [Column(Name = "PurchaseUnit", DataType = "String")]
        public String PurchaseUnit
        {
            get { return _PurchaseUnit; }
            set { _PurchaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "SupplierItemCode", DataType = "String")]
        public String SupplierItemCode
        {
            get { return _SupplierItemCode; }
            set { _SupplierItemCode = value; }
        }
        [Column(Name = "SupplierItemName", DataType = "String")]
        public String SupplierItemName
        {
            get { return _SupplierItemName; }
            set { _SupplierItemName = value; }
        }
        [Column(Name = "UnitPrice", DataType = "Decimal")]
        public Decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        [Column(Name = "DiscountPercentage", DataType = "Decimal")]
        public Decimal DiscountPercentage
        {
            get { return _DiscountPercentage; }
            set { _DiscountPercentage = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "QtyOnOrder", DataType = "Decimal")]
        public Decimal QtyOnOrder
        {
            get { return _QtyOnOrder; }
            set { _QtyOnOrder = value; }
        }
        [Column(Name = "QuantityEND", DataType = "Decimal")]
        public Decimal QuantityEND
        {
            get { return _QuantityEND; }
            set { _QuantityEND = value; }
        }
    }
    #endregion
    #region vPurchaseRequestHd
    [Serializable]
    [Table(Name = "vPurchaseRequestHd")]
    public partial class vPurchaseRequestHd
    {
        private Int32 _PurchaseRequestID;
        private DateTime _TransactionDate;
        private String _TransactionTime;
        private String _PurchaseRequestNo;
        private Int32 _ItemRequestID;
        private Int32 _FromLocationID;
        private String _LocationCode;
        private String _LocationName;
        private Int32 _LocationItemGroupID;
        private String _Remarks;
        private String _GCTransactionStatus;
        private String _TransactionStatusWatermark;
        private Int32 _NumberOfItems;
        private Int32 _NumberOfApprovedItems;
        private String _CreatedByName;
        private String _ApprovedByName;

        [Column(Name = "PurchaseRequestID", DataType = "Int32")]
        public Int32 PurchaseRequestID
        {
            get { return _PurchaseRequestID; }
            set { _PurchaseRequestID = value; }
        }
        [Column(Name = "TransactionDate", DataType = "DateTime")]
        public DateTime TransactionDate
        {
            get { return _TransactionDate; }
            set { _TransactionDate = value; }
        }
        [Column(Name = "TransactionTime", DataType = "String")]
        public String TransactionTime
        {
            get { return _TransactionTime; }
            set { _TransactionTime = value; }
        }
        [Column(Name = "PurchaseRequestNo", DataType = "String")]
        public String PurchaseRequestNo
        {
            get { return _PurchaseRequestNo; }
            set { _PurchaseRequestNo = value; }
        }
        [Column(Name = "ItemRequestID", DataType = "Int32")]
        public Int32 ItemRequestID
        {
            get { return _ItemRequestID; }
            set { _ItemRequestID = value; }
        }
        [Column(Name = "FromLocationID", DataType = "Int32")]
        public Int32 FromLocationID
        {
            get { return _FromLocationID; }
            set { _FromLocationID = value; }
        }
        [Column(Name = "LocationCode", DataType = "String")]
        public String LocationCode
        {
            get { return _LocationCode; }
            set { _LocationCode = value; }
        }
        [Column(Name = "LocationName", DataType = "String")]
        public String LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }
        [Column(Name = "LocationItemGroupID", DataType = "Int32")]
        public Int32 LocationItemGroupID
        {
            get { return _LocationItemGroupID; }
            set { _LocationItemGroupID = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
        [Column(Name = "NumberOfItems", DataType = "Int32")]
        public Int32 NumberOfItems
        {
            get { return _NumberOfItems; }
            set { _NumberOfItems = value; }
        }
        [Column(Name = "NumberOfApprovedItems", DataType = "Int32")]
        public Int32 NumberOfApprovedItems
        {
            get { return _NumberOfApprovedItems; }
            set { _NumberOfApprovedItems = value; }
        }
        [Column(Name = "CreatedByName", DataType = "String")]
        public String CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }
        [Column(Name = "ApprovedByName", DataType = "String")]
        public String ApprovedByName
        {
            get { return _ApprovedByName; }
            set { _ApprovedByName = value; }
        }
    }
    #endregion
    #region vPurchaseRequestPO
    [Serializable]
    [Table(Name = "vPurchaseRequestPO")]
    public class vPurchaseRequestPO
    {
        private Int32 _ID;
        private Int32 _PurchaseRequestID;
        private String _PurchaseRequestNo;
        private Int32 _ItemID;
        private String _ItemName1;
        private String _ItemUnit;
        private Int32 _PurchaseOrderID;
        private Decimal _OrderQuantity;
        private Decimal _ReceivedQuantity;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "PurchaseRequestID", DataType = "Int32")]
        public Int32 PurchaseRequestID
        {
            get { return _PurchaseRequestID; }
            set { _PurchaseRequestID = value; }
        }
        [Column(Name = "PurchaseRequestNo", DataType = "String")]
        public String PurchaseRequestNo
        {
            get { return _PurchaseRequestNo; }
            set { _PurchaseRequestNo = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "PurchaseOrderID", DataType = "Int32")]
        public Int32 PurchaseOrderID
        {
            get { return _PurchaseOrderID; }
            set { _PurchaseOrderID = value; }
        }
        [Column(Name = "OrderQuantity", DataType = "Decimal")]
        public Decimal OrderQuantity
        {
            get { return _OrderQuantity; }
            set { _OrderQuantity = value; }
        }
        [Column(Name = "ReceivedQuantity", DataType = "Decimal")]
        public Decimal ReceivedQuantity
        {
            get { return _ReceivedQuantity; }
            set { _ReceivedQuantity = value; }
        }
    }
    #endregion
    #region vPurchaseReturnDt
    [Serializable]
    [Table(Name = "vPurchaseReturnDt")]
    public partial class vPurchaseReturnDt
    {
        private Int32 _ID;
        private Int32 _PurchaseReturnID;
        private String _PurchaseReturnNo;
        private DateTime _ReturnDate;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Decimal _ReceivedQuantity;
        private String _ReceivedItemUnit;
        private Decimal _ReceivedConversionFactor;
        private Decimal _Quantity;
        private String _ItemUnit;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private Decimal _ConversionFactor;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage1;
        private Decimal _DiscountAmount1;
        private Decimal _DiscountPercentage2;
        private Decimal _DiscountAmount2;
        private Decimal _LineAmount;
        private String _GCPurchaseReturnReason;
        private String _PurchaseReturnReason;
        private Int32 _SupplierID;
        private String _SupplierCode;
        private String _SupplierName;
        private String _GCTransactionStatus;
        private String _GCItemUnit;
        private String _GCItemDetailStatus;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "PurchaseReturnID", DataType = "Int32")]
        public Int32 PurchaseReturnID
        {
            get { return _PurchaseReturnID; }
            set { _PurchaseReturnID = value; }
        }
        [Column(Name = "PurchaseReturnNo", DataType = "String")]
        public String PurchaseReturnNo
        {
            get { return _PurchaseReturnNo; }
            set { _PurchaseReturnNo = value; }
        }
        [Column(Name = "ReturnDate", DataType = "DateTime")]
        public DateTime ReturnDate
        {
            get { return _ReturnDate; }
            set { _ReturnDate = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "ReceivedQuantity", DataType = "Decimal")]
        public Decimal ReceivedQuantity
        {
            get { return _ReceivedQuantity; }
            set { _ReceivedQuantity = value; }
        }
        [Column(Name = "ReceivedItemUnit", DataType = "String")]
        public String ReceivedItemUnit
        {
            get { return _ReceivedItemUnit; }
            set { _ReceivedItemUnit = value; }
        }
        [Column(Name = "ReceivedConversionFactor", DataType = "Decimal")]
        public Decimal ReceivedConversionFactor
        {
            get { return _ReceivedConversionFactor; }
            set { _ReceivedConversionFactor = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "UnitPrice", DataType = "Decimal")]
        public Decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        [Column(Name = "DiscountPercentage1", DataType = "Decimal")]
        public Decimal DiscountPercentage1
        {
            get { return _DiscountPercentage1; }
            set { _DiscountPercentage1 = value; }
        }
        [Column(Name = "DiscountAmount1", DataType = "Decimal")]
        public Decimal DiscountAmount1
        {
            get { return _DiscountAmount1; }
            set { _DiscountAmount1 = value; }
        }
        [Column(Name = "DiscountPercentage2", DataType = "Decimal")]
        public Decimal DiscountPercentage2
        {
            get { return _DiscountPercentage2; }
            set { _DiscountPercentage2 = value; }
        }
        [Column(Name = "DiscountAmount2", DataType = "Decimal")]
        public Decimal DiscountAmount2
        {
            get { return _DiscountAmount2; }
            set { _DiscountAmount2 = value; }
        }
        [Column(Name = "LineAmount", DataType = "Decimal")]
        public Decimal LineAmount
        {
            get { return _LineAmount; }
            set { _LineAmount = value; }
        }
        [Column(Name = "GCPurchaseReturnReason", DataType = "String")]
        public String GCPurchaseReturnReason
        {
            get { return _GCPurchaseReturnReason; }
            set { _GCPurchaseReturnReason = value; }
        }
        [Column(Name = "PurchaseReturnReason", DataType = "String")]
        public String PurchaseReturnReason
        {
            get { return _PurchaseReturnReason; }
            set { _PurchaseReturnReason = value; }
        }
        [Column(Name = "SupplierID", DataType = "Int32")]
        public Int32 SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
        }
        [Column(Name = "SupplierCode", DataType = "String")]
        public String SupplierCode
        {
            get { return _SupplierCode; }
            set { _SupplierCode = value; }
        }
        [Column(Name = "SupplierName", DataType = "String")]
        public String SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
    }
    #endregion
    #region vPurchaseReturnHd
    [Serializable]
    [Table(Name = "vPurchaseReturnHd")]
    public partial class vPurchaseReturnHd
    {
        private Int32 _PurchaseReturnID;
        private String _TransactionCode;
        private DateTime _ReturnDate;
        private String _PurchaseReturnNo;
        private Int32 _PurchaseReceiveID;
        private String _PurchaseReceiveNo;
        private Int32 _LocationID;
        private String _LocationCode;
        private String _LocationName;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _SupplierName;
        private String _GCPurchaseReturnType;
        private String _PurchaseReturnType;
        private String _ReferenceNo;
        private DateTime _ReferenceDate;
        private Boolean _IsIncludeVAT;
        private Decimal _TransactionAmount;
        private Decimal _VATPercentage;
        private Decimal _VATAmount;
        private Decimal _TotalNetTransactionAmount;
        private Boolean _IsAutoUpdateStock;
        private String _Remarks;
        private String _GCTransactionStatus;
        private String _TransactionStatusWatermark;
        private Int32 _NumberOfItems;
        private Int32 _NumberOfApprovedItems;

        [Column(Name = "PurchaseReturnID", DataType = "Int32")]
        public Int32 PurchaseReturnID
        {
            get { return _PurchaseReturnID; }
            set { _PurchaseReturnID = value; }
        }
        [Column(Name = "TransactionCode", DataType = "String")]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
        [Column(Name = "ReturnDate", DataType = "DateTime")]
        public DateTime ReturnDate
        {
            get { return _ReturnDate; }
            set { _ReturnDate = value; }
        }
        [Column(Name = "PurchaseReturnNo", DataType = "String")]
        public String PurchaseReturnNo
        {
            get { return _PurchaseReturnNo; }
            set { _PurchaseReturnNo = value; }
        }
        [Column(Name = "PurchaseReceiveID", DataType = "Int32")]
        public Int32 PurchaseReceiveID
        {
            get { return _PurchaseReceiveID; }
            set { _PurchaseReceiveID = value; }
        }
        [Column(Name = "PurchaseReceiveNo", DataType = "String")]
        public String PurchaseReceiveNo
        {
            get { return _PurchaseReceiveNo; }
            set { _PurchaseReceiveNo = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "LocationCode", DataType = "String")]
        public String LocationCode
        {
            get { return _LocationCode; }
            set { _LocationCode = value; }
        }
        [Column(Name = "LocationName", DataType = "String")]
        public String LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "SupplierName", DataType = "String")]
        public String SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }
        [Column(Name = "GCPurchaseReturnType", DataType = "String")]
        public String GCPurchaseReturnType
        {
            get { return _GCPurchaseReturnType; }
            set { _GCPurchaseReturnType = value; }
        }
        [Column(Name = "PurchaseReturnType", DataType = "String")]
        public String PurchaseReturnType
        {
            get { return _PurchaseReturnType; }
            set { _PurchaseReturnType = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String")]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "ReferenceDate", DataType = "DateTime")]
        public DateTime ReferenceDate
        {
            get { return _ReferenceDate; }
            set { _ReferenceDate = value; }
        }
        [Column(Name = "IsIncludeVAT", DataType = "Boolean")]
        public Boolean IsIncludeVAT
        {
            get { return _IsIncludeVAT; }
            set { _IsIncludeVAT = value; }
        }
        [Column(Name = "TransactionAmount", DataType = "Decimal")]
        public Decimal TransactionAmount
        {
            get { return _TransactionAmount; }
            set { _TransactionAmount = value; }
        }
        [Column(Name = "VATPercentage", DataType = "Decimal")]
        public Decimal VATPercentage
        {
            get { return _VATPercentage; }
            set { _VATPercentage = value; }
        }
        [Column(Name = "VATAmount", DataType = "Decimal")]
        public Decimal VATAmount
        {
            get { return _VATAmount; }
            set { _VATAmount = value; }
        }
        [Column(Name = "TotalNetTransactionAmount", DataType = "Decimal")]
        public Decimal TotalNetTransactionAmount
        {
            get { return _TotalNetTransactionAmount; }
            set { _TotalNetTransactionAmount = value; }
        }
        [Column(Name = "IsAutoUpdateStock", DataType = "Boolean")]
        public Boolean IsAutoUpdateStock
        {
            get { return _IsAutoUpdateStock; }
            set { _IsAutoUpdateStock = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
        [Column(Name = "NumberOfItems", DataType = "Int32")]
        public Int32 NumberOfItems
        {
            get { return _NumberOfItems; }
            set { _NumberOfItems = value; }
        }
        [Column(Name = "NumberOfApprovedItems", DataType = "Int32")]
        public Int32 NumberOfApprovedItems
        {
            get { return _NumberOfApprovedItems; }
            set { _NumberOfApprovedItems = value; }
        }
    }
    #endregion
    #region vRegistration
    [Serializable]
    [Table(Name = "vRegistration")]
    public partial class vRegistration
    {
        private Int32 _RegistrationID;
        private String _RegistrationNo;
        private DateTime _RegistrationDate;
        private String _RegistrationTime;
        private Int32 _PeriodAdmissionID;
        private Int32 _ProspectiveStudentID;
        private String _GCRegistrationType;
        private String _GCInformationSource;
        private String _GCGrade;
        private String _Grade;
        private String _GCMajor;
        private String _Major;
        private DateTime _SchoolDate;
        private Decimal _FinalMark;
        private Int32 _AdmissionFeeRuleID;
        private String _AdmissionFeeRuleName;
        private Int32 _PaymentID;
        private String _PaymentName;
        private String _Remarks;
        private String _GCRegistrationStatus;
        private String _RegistrationStatus;
        private String _ProspectiveStudentCode;
        private String _NationalStudentNo;
        private String _SiteID;
        private String _GCSalutation;
        private String _GCSuffix;
        private String _GCTitle;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _ProspectiveStudentName;
        private String _Name;
        private String _PreferredName;
        private String _CityOfBirth;
        private DateTime _DateOfBirth;
        private String _GCGender;
        private String _GCNationality;
        private String _GCReligion;
        private String _PlaceOfBaptism;
        private DateTime _DateOfBaptism;
        private Boolean _IsFeeder;
        private String _AddressID;
        private String _StreetName;
        private String _District;
        private String _City;
        private String _County;
        private String _GCState;
        private String _State;
        private Int32 _ZipCodeID;
        private String _ZipCode;
        private String _EmailAddress1;
        private String _EmailAddress2;
        private String _MobilePhoneNo1;
        private String _MobilePhoneNo2;
        private String _PhoneNo1;
        private String _PictureFileName;

        [Column(Name = "RegistrationID", DataType = "Int32")]
        public Int32 RegistrationID
        {
            get { return _RegistrationID; }
            set { _RegistrationID = value; }
        }
        [Column(Name = "RegistrationNo", DataType = "String")]
        public String RegistrationNo
        {
            get { return _RegistrationNo; }
            set { _RegistrationNo = value; }
        }
        [Column(Name = "RegistrationDate", DataType = "DateTime")]
        public DateTime RegistrationDate
        {
            get { return _RegistrationDate; }
            set { _RegistrationDate = value; }
        }
        [Column(Name = "RegistrationTime", DataType = "String")]
        public String RegistrationTime
        {
            get { return _RegistrationTime; }
            set { _RegistrationTime = value; }
        }
        [Column(Name = "PeriodAdmissionID", DataType = "Int32")]
        public Int32 PeriodAdmissionID
        {
            get { return _PeriodAdmissionID; }
            set { _PeriodAdmissionID = value; }
        }
        [Column(Name = "ProspectiveStudentID", DataType = "Int32")]
        public Int32 ProspectiveStudentID
        {
            get { return _ProspectiveStudentID; }
            set { _ProspectiveStudentID = value; }
        }
        [Column(Name = "GCRegistrationType", DataType = "String")]
        public String GCRegistrationType
        {
            get { return _GCRegistrationType; }
            set { _GCRegistrationType = value; }
        }
        [Column(Name = "GCInformationSource", DataType = "String")]
        public String GCInformationSource
        {
            get { return _GCInformationSource; }
            set { _GCInformationSource = value; }
        }
        [Column(Name = "GCGrade", DataType = "String")]
        public String GCGrade
        {
            get { return _GCGrade; }
            set { _GCGrade = value; }
        }
        [Column(Name = "Grade", DataType = "String")]
        public String Grade
        {
            get { return _Grade; }
            set { _Grade = value; }
        }
        [Column(Name = "GCMajor", DataType = "String")]
        public String GCMajor
        {
            get { return _GCMajor; }
            set { _GCMajor = value; }
        }
        [Column(Name = "Major", DataType = "String")]
        public String Major
        {
            get { return _Major; }
            set { _Major = value; }
        }
        [Column(Name = "SchoolDate", DataType = "DateTime")]
        public DateTime SchoolDate
        {
            get { return _SchoolDate; }
            set { _SchoolDate = value; }
        }
        [Column(Name = "FinalMark", DataType = "Decimal")]
        public Decimal FinalMark
        {
            get { return _FinalMark; }
            set { _FinalMark = value; }
        }
        [Column(Name = "AdmissionFeeRuleID", DataType = "Int32")]
        public Int32 AdmissionFeeRuleID
        {
            get { return _AdmissionFeeRuleID; }
            set { _AdmissionFeeRuleID = value; }
        }
        [Column(Name = "AdmissionFeeRuleName", DataType = "String")]
        public String AdmissionFeeRuleName
        {
            get { return _AdmissionFeeRuleName; }
            set { _AdmissionFeeRuleName = value; }
        }
        [Column(Name = "PaymentID", DataType = "Int32")]
        public Int32 PaymentID
        {
            get { return _PaymentID; }
            set { _PaymentID = value; }
        }
        [Column(Name = "PaymentName", DataType = "String")]
        public String PaymentName
        {
            get { return _PaymentName; }
            set { _PaymentName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCRegistrationStatus", DataType = "String")]
        public String GCRegistrationStatus
        {
            get { return _GCRegistrationStatus; }
            set { _GCRegistrationStatus = value; }
        }
        [Column(Name = "RegistrationStatus", DataType = "String")]
        public String RegistrationStatus
        {
            get { return _RegistrationStatus; }
            set { _RegistrationStatus = value; }
        }
        [Column(Name = "ProspectiveStudentCode", DataType = "String")]
        public String ProspectiveStudentCode
        {
            get { return _ProspectiveStudentCode; }
            set { _ProspectiveStudentCode = value; }
        }
        [Column(Name = "NationalStudentNo", DataType = "String")]
        public String NationalStudentNo
        {
            get { return _NationalStudentNo; }
            set { _NationalStudentNo = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "GCSalutation", DataType = "String")]
        public String GCSalutation
        {
            get { return _GCSalutation; }
            set { _GCSalutation = value; }
        }
        [Column(Name = "GCSuffix", DataType = "String")]
        public String GCSuffix
        {
            get { return _GCSuffix; }
            set { _GCSuffix = value; }
        }
        [Column(Name = "GCTitle", DataType = "String")]
        public String GCTitle
        {
            get { return _GCTitle; }
            set { _GCTitle = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "ProspectiveStudentName", DataType = "String")]
        public String ProspectiveStudentName
        {
            get { return _ProspectiveStudentName; }
            set { _ProspectiveStudentName = value; }
        }
        [Column(Name = "Name", DataType = "String")]
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        [Column(Name = "PreferredName", DataType = "String")]
        public String PreferredName
        {
            get { return _PreferredName; }
            set { _PreferredName = value; }
        }
        [Column(Name = "CityOfBirth", DataType = "String")]
        public String CityOfBirth
        {
            get { return _CityOfBirth; }
            set { _CityOfBirth = value; }
        }
        [Column(Name = "DateOfBirth", DataType = "DateTime")]
        public DateTime DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }
        [Column(Name = "GCGender", DataType = "String")]
        public String GCGender
        {
            get { return _GCGender; }
            set { _GCGender = value; }
        }
        [Column(Name = "GCNationality", DataType = "String")]
        public String GCNationality
        {
            get { return _GCNationality; }
            set { _GCNationality = value; }
        }
        [Column(Name = "GCReligion", DataType = "String")]
        public String GCReligion
        {
            get { return _GCReligion; }
            set { _GCReligion = value; }
        }
        [Column(Name = "PlaceOfBaptism", DataType = "String")]
        public String PlaceOfBaptism
        {
            get { return _PlaceOfBaptism; }
            set { _PlaceOfBaptism = value; }
        }
        [Column(Name = "DateOfBaptism", DataType = "DateTime")]
        public DateTime DateOfBaptism
        {
            get { return _DateOfBaptism; }
            set { _DateOfBaptism = value; }
        }
        [Column(Name = "IsFeeder", DataType = "Boolean")]
        public Boolean IsFeeder
        {
            get { return _IsFeeder; }
            set { _IsFeeder = value; }
        }
        [Column(Name = "AddressID", DataType = "String")]
        public String AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
        }
        [Column(Name = "StreetName", DataType = "String")]
        public String StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }
        [Column(Name = "District", DataType = "String")]
        public String District
        {
            get { return _District; }
            set { _District = value; }
        }
        [Column(Name = "City", DataType = "String")]
        public String City
        {
            get { return _City; }
            set { _City = value; }
        }
        [Column(Name = "County", DataType = "String")]
        public String County
        {
            get { return _County; }
            set { _County = value; }
        }
        [Column(Name = "GCState", DataType = "String")]
        public String GCState
        {
            get { return _GCState; }
            set { _GCState = value; }
        }
        [Column(Name = "State", DataType = "String")]
        public String State
        {
            get { return _State; }
            set { _State = value; }
        }
        [Column(Name = "ZipCodeID", DataType = "Int32")]
        public Int32 ZipCodeID
        {
            get { return _ZipCodeID; }
            set { _ZipCodeID = value; }
        }
        [Column(Name = "ZipCode", DataType = "String")]
        public String ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }
        [Column(Name = "EmailAddress1", DataType = "String")]
        public String EmailAddress1
        {
            get { return _EmailAddress1; }
            set { _EmailAddress1 = value; }
        }
        [Column(Name = "EmailAddress2", DataType = "String")]
        public String EmailAddress2
        {
            get { return _EmailAddress2; }
            set { _EmailAddress2 = value; }
        }
        [Column(Name = "MobilePhoneNo1", DataType = "String")]
        public String MobilePhoneNo1
        {
            get { return _MobilePhoneNo1; }
            set { _MobilePhoneNo1 = value; }
        }
        [Column(Name = "MobilePhoneNo2", DataType = "String")]
        public String MobilePhoneNo2
        {
            get { return _MobilePhoneNo2; }
            set { _MobilePhoneNo2 = value; }
        }
        [Column(Name = "PhoneNo1", DataType = "String")]
        public String PhoneNo1
        {
            get { return _PhoneNo1; }
            set { _PhoneNo1 = value; }
        }
        [Column(Name = "PictureFileName", DataType = "String")]
        public String PictureFileName
        {
            get { return _PictureFileName; }
            set { _PictureFileName = value; }
        }
    }
    #endregion
    #region vRegistrationInvoice
    [Serializable]
    [Table(Name = "vRegistrationInvoice")]
    public partial class vRegistrationInvoice
    {
        private Int32 _RegistrationID;
        private String _RegistrationNo;
        private DateTime _RegistrationDate;
        private String _RegistrationTime;
        private Int32 _PeriodAdmissionID;
        private Int32 _ProspectiveStudentID;
        private String _GCRegistrationType;
        private String _GCInformationSource;
        private String _GCGrade;
        private String _Grade;
        private String _GCMajor;
        private String _Major;
        private DateTime _SchoolDate;
        private Decimal _FinalMark;
        private Int32 _AdmissionFeeRuleID;
        private String _AdmissionFeeRuleName;
        private Int32 _PaymentID;
        private String _PaymentName;
        private String _Remarks;
        private String _GCRegistrationStatus;
        private String _RegistrationStatus;
        private String _ProspectiveStudentCode;
        private String _NationalStudentNo;
        private String _SiteID;
        private String _GCSalutation;
        private String _GCSuffix;
        private String _GCTitle;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _ProspectiveStudentName;
        private String _Name;
        private String _PreferredName;
        private String _CityOfBirth;
        private DateTime _DateOfBirth;
        private String _GCGender;
        private String _GCNationality;
        private String _GCReligion;
        private String _PlaceOfBaptism;
        private DateTime _DateOfBaptism;
        private Boolean _IsFeeder;
        private String _AddressID;
        private String _StreetName;
        private String _District;
        private String _City;
        private String _County;
        private String _GCState;
        private String _State;
        private Int32 _ZipCodeID;
        private String _ZipCode;
        private String _EmailAddress1;
        private String _EmailAddress2;
        private String _MobilePhoneNo1;
        private String _MobilePhoneNo2;
        private String _PhoneNo1;
        private String _PictureFileName;
        private Decimal _TotalClaimedAmount;
        private Decimal _TotalPaymentAmount;

        [Column(Name = "RegistrationID", DataType = "Int32")]
        public Int32 RegistrationID
        {
            get { return _RegistrationID; }
            set { _RegistrationID = value; }
        }
        [Column(Name = "RegistrationNo", DataType = "String")]
        public String RegistrationNo
        {
            get { return _RegistrationNo; }
            set { _RegistrationNo = value; }
        }
        [Column(Name = "RegistrationDate", DataType = "DateTime")]
        public DateTime RegistrationDate
        {
            get { return _RegistrationDate; }
            set { _RegistrationDate = value; }
        }
        [Column(Name = "RegistrationTime", DataType = "String")]
        public String RegistrationTime
        {
            get { return _RegistrationTime; }
            set { _RegistrationTime = value; }
        }
        [Column(Name = "PeriodAdmissionID", DataType = "Int32")]
        public Int32 PeriodAdmissionID
        {
            get { return _PeriodAdmissionID; }
            set { _PeriodAdmissionID = value; }
        }
        [Column(Name = "ProspectiveStudentID", DataType = "Int32")]
        public Int32 ProspectiveStudentID
        {
            get { return _ProspectiveStudentID; }
            set { _ProspectiveStudentID = value; }
        }
        [Column(Name = "GCRegistrationType", DataType = "String")]
        public String GCRegistrationType
        {
            get { return _GCRegistrationType; }
            set { _GCRegistrationType = value; }
        }
        [Column(Name = "GCInformationSource", DataType = "String")]
        public String GCInformationSource
        {
            get { return _GCInformationSource; }
            set { _GCInformationSource = value; }
        }
        [Column(Name = "GCGrade", DataType = "String")]
        public String GCGrade
        {
            get { return _GCGrade; }
            set { _GCGrade = value; }
        }
        [Column(Name = "Grade", DataType = "String")]
        public String Grade
        {
            get { return _Grade; }
            set { _Grade = value; }
        }
        [Column(Name = "GCMajor", DataType = "String")]
        public String GCMajor
        {
            get { return _GCMajor; }
            set { _GCMajor = value; }
        }
        [Column(Name = "Major", DataType = "String")]
        public String Major
        {
            get { return _Major; }
            set { _Major = value; }
        }
        [Column(Name = "SchoolDate", DataType = "DateTime")]
        public DateTime SchoolDate
        {
            get { return _SchoolDate; }
            set { _SchoolDate = value; }
        }
        [Column(Name = "FinalMark", DataType = "Decimal")]
        public Decimal FinalMark
        {
            get { return _FinalMark; }
            set { _FinalMark = value; }
        }
        [Column(Name = "AdmissionFeeRuleID", DataType = "Int32")]
        public Int32 AdmissionFeeRuleID
        {
            get { return _AdmissionFeeRuleID; }
            set { _AdmissionFeeRuleID = value; }
        }
        [Column(Name = "AdmissionFeeRuleName", DataType = "String")]
        public String AdmissionFeeRuleName
        {
            get { return _AdmissionFeeRuleName; }
            set { _AdmissionFeeRuleName = value; }
        }
        [Column(Name = "PaymentID", DataType = "Int32")]
        public Int32 PaymentID
        {
            get { return _PaymentID; }
            set { _PaymentID = value; }
        }
        [Column(Name = "PaymentName", DataType = "String")]
        public String PaymentName
        {
            get { return _PaymentName; }
            set { _PaymentName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCRegistrationStatus", DataType = "String")]
        public String GCRegistrationStatus
        {
            get { return _GCRegistrationStatus; }
            set { _GCRegistrationStatus = value; }
        }
        [Column(Name = "RegistrationStatus", DataType = "String")]
        public String RegistrationStatus
        {
            get { return _RegistrationStatus; }
            set { _RegistrationStatus = value; }
        }
        [Column(Name = "ProspectiveStudentCode", DataType = "String")]
        public String ProspectiveStudentCode
        {
            get { return _ProspectiveStudentCode; }
            set { _ProspectiveStudentCode = value; }
        }
        [Column(Name = "NationalStudentNo", DataType = "String")]
        public String NationalStudentNo
        {
            get { return _NationalStudentNo; }
            set { _NationalStudentNo = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "GCSalutation", DataType = "String")]
        public String GCSalutation
        {
            get { return _GCSalutation; }
            set { _GCSalutation = value; }
        }
        [Column(Name = "GCSuffix", DataType = "String")]
        public String GCSuffix
        {
            get { return _GCSuffix; }
            set { _GCSuffix = value; }
        }
        [Column(Name = "GCTitle", DataType = "String")]
        public String GCTitle
        {
            get { return _GCTitle; }
            set { _GCTitle = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "ProspectiveStudentName", DataType = "String")]
        public String ProspectiveStudentName
        {
            get { return _ProspectiveStudentName; }
            set { _ProspectiveStudentName = value; }
        }
        [Column(Name = "Name", DataType = "String")]
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        [Column(Name = "PreferredName", DataType = "String")]
        public String PreferredName
        {
            get { return _PreferredName; }
            set { _PreferredName = value; }
        }
        [Column(Name = "CityOfBirth", DataType = "String")]
        public String CityOfBirth
        {
            get { return _CityOfBirth; }
            set { _CityOfBirth = value; }
        }
        [Column(Name = "DateOfBirth", DataType = "DateTime")]
        public DateTime DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }
        [Column(Name = "GCGender", DataType = "String")]
        public String GCGender
        {
            get { return _GCGender; }
            set { _GCGender = value; }
        }
        [Column(Name = "GCNationality", DataType = "String")]
        public String GCNationality
        {
            get { return _GCNationality; }
            set { _GCNationality = value; }
        }
        [Column(Name = "GCReligion", DataType = "String")]
        public String GCReligion
        {
            get { return _GCReligion; }
            set { _GCReligion = value; }
        }
        [Column(Name = "PlaceOfBaptism", DataType = "String")]
        public String PlaceOfBaptism
        {
            get { return _PlaceOfBaptism; }
            set { _PlaceOfBaptism = value; }
        }
        [Column(Name = "DateOfBaptism", DataType = "DateTime")]
        public DateTime DateOfBaptism
        {
            get { return _DateOfBaptism; }
            set { _DateOfBaptism = value; }
        }
        [Column(Name = "IsFeeder", DataType = "Boolean")]
        public Boolean IsFeeder
        {
            get { return _IsFeeder; }
            set { _IsFeeder = value; }
        }
        [Column(Name = "AddressID", DataType = "String")]
        public String AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
        }
        [Column(Name = "StreetName", DataType = "String")]
        public String StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }
        [Column(Name = "District", DataType = "String")]
        public String District
        {
            get { return _District; }
            set { _District = value; }
        }
        [Column(Name = "City", DataType = "String")]
        public String City
        {
            get { return _City; }
            set { _City = value; }
        }
        [Column(Name = "County", DataType = "String")]
        public String County
        {
            get { return _County; }
            set { _County = value; }
        }
        [Column(Name = "GCState", DataType = "String")]
        public String GCState
        {
            get { return _GCState; }
            set { _GCState = value; }
        }
        [Column(Name = "State", DataType = "String")]
        public String State
        {
            get { return _State; }
            set { _State = value; }
        }
        [Column(Name = "ZipCodeID", DataType = "Int32")]
        public Int32 ZipCodeID
        {
            get { return _ZipCodeID; }
            set { _ZipCodeID = value; }
        }
        [Column(Name = "ZipCode", DataType = "String")]
        public String ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }
        [Column(Name = "EmailAddress1", DataType = "String")]
        public String EmailAddress1
        {
            get { return _EmailAddress1; }
            set { _EmailAddress1 = value; }
        }
        [Column(Name = "EmailAddress2", DataType = "String")]
        public String EmailAddress2
        {
            get { return _EmailAddress2; }
            set { _EmailAddress2 = value; }
        }
        [Column(Name = "MobilePhoneNo1", DataType = "String")]
        public String MobilePhoneNo1
        {
            get { return _MobilePhoneNo1; }
            set { _MobilePhoneNo1 = value; }
        }
        [Column(Name = "MobilePhoneNo2", DataType = "String")]
        public String MobilePhoneNo2
        {
            get { return _MobilePhoneNo2; }
            set { _MobilePhoneNo2 = value; }
        }
        [Column(Name = "PhoneNo1", DataType = "String")]
        public String PhoneNo1
        {
            get { return _PhoneNo1; }
            set { _PhoneNo1 = value; }
        }
        [Column(Name = "PictureFileName", DataType = "String")]
        public String PictureFileName
        {
            get { return _PictureFileName; }
            set { _PictureFileName = value; }
        }
        [Column(Name = "TotalClaimedAmount", DataType = "Decimal")]
        public Decimal TotalClaimedAmount
        {
            get { return _TotalClaimedAmount; }
            set { _TotalClaimedAmount = value; }
        }
        [Column(Name = "TotalPaymentAmount", DataType = "Decimal")]
        public Decimal TotalPaymentAmount
        {
            get { return _TotalPaymentAmount; }
            set { _TotalPaymentAmount = value; }
        }
    }
    #endregion
    #region vRestrictionDt
    [Serializable]
    [Table(Name = "vRestrictionDt")]
    public class vRestrictionDt
    {
        private Int32 _RestrictionID;
        private String _TransactionCode;
        private String _TransactionName;

        [Column(Name = "RestrictionID", DataType = "Int32")]
        public Int32 RestrictionID
        {
            get { return _RestrictionID; }
            set { _RestrictionID = value; }
        }
        [Column(Name = "TransactionCode", DataType = "String")]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
        [Column(Name = "TransactionName", DataType = "String")]
        public String TransactionName
        {
            get { return _TransactionName; }
            set { _TransactionName = value; }
        }
    }
    #endregion
    #region vSalesInvoiceDt
    [Serializable]
    [Table(Name = "vSalesInvoiceDt")]
    public partial class vSalesInvoiceDt
    {
        private Int32 _TransactionDtID;
        private Int32 _SalesInvoiceID;
        private String _SalesInvoiceNo;
        private DateTime _SalesInvoiceDate;
        private String _StudentName;
        private Int32 _ItemID;
        private String _ItemName1;
        private String _ItemCode;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Decimal _Quantity;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private String _GCItemUnit;
        private String _ItemUnit;
        private Decimal _ConversionFactor;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage1;
        private Decimal _DiscountPercentage2;
        private Decimal _DiscountPercentage3;
        private Decimal _LineAmount;
        private Boolean _IsBonusItem;
        private Boolean _IsControlBatchNumber;
        private String _GCTransactionStatus;
        private String _GCItemDetailStatus;
        private String _ItemDtProductionCode;
        private Decimal _ItemDtQuantity;
        private Decimal _VATPercentage;
        private Int32 _CreatedBy;
        private String _CreatedByName;

        [Column(Name = "TransactionDtID", DataType = "Int32")]
        public Int32 TransactionDtID
        {
            get { return _TransactionDtID; }
            set { _TransactionDtID = value; }
        }
        [Column(Name = "SalesInvoiceID", DataType = "Int32")]
        public Int32 SalesInvoiceID
        {
            get { return _SalesInvoiceID; }
            set { _SalesInvoiceID = value; }
        }
        [Column(Name = "SalesInvoiceNo", DataType = "String")]
        public String SalesInvoiceNo
        {
            get { return _SalesInvoiceNo; }
            set { _SalesInvoiceNo = value; }
        }
        [Column(Name = "SalesInvoiceDate", DataType = "DateTime")]
        public DateTime SalesInvoiceDate
        {
            get { return _SalesInvoiceDate; }
            set { _SalesInvoiceDate = value; }
        }
        [Column(Name = "StudentName", DataType = "String")]
        public String StudentName
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "UnitPrice", DataType = "Decimal")]
        public Decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        [Column(Name = "DiscountPercentage1", DataType = "Decimal")]
        public Decimal DiscountPercentage1
        {
            get { return _DiscountPercentage1; }
            set { _DiscountPercentage1 = value; }
        }
        [Column(Name = "DiscountPercentage2", DataType = "Decimal")]
        public Decimal DiscountPercentage2
        {
            get { return _DiscountPercentage2; }
            set { _DiscountPercentage2 = value; }
        }
        [Column(Name = "DiscountPercentage3", DataType = "Decimal")]
        public Decimal DiscountPercentage3
        {
            get { return _DiscountPercentage3; }
            set { _DiscountPercentage3 = value; }
        }
        [Column(Name = "LineAmount", DataType = "Decimal")]
        public Decimal LineAmount
        {
            get { return _LineAmount; }
            set { _LineAmount = value; }
        }
        [Column(Name = "IsBonusItem", DataType = "Boolean")]
        public Boolean IsBonusItem
        {
            get { return _IsBonusItem; }
            set { _IsBonusItem = value; }
        }
        [Column(Name = "IsControlBatchNumber", DataType = "Boolean")]
        public Boolean IsControlBatchNumber
        {
            get { return _IsControlBatchNumber; }
            set { _IsControlBatchNumber = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
        [Column(Name = "ItemDtProductionCode", DataType = "String")]
        public String ItemDtProductionCode
        {
            get { return _ItemDtProductionCode; }
            set { _ItemDtProductionCode = value; }
        }
        [Column(Name = "ItemDtQuantity", DataType = "Decimal")]
        public Decimal ItemDtQuantity
        {
            get { return _ItemDtQuantity; }
            set { _ItemDtQuantity = value; }
        }
        [Column(Name = "VATPercentage", DataType = "Decimal")]
        public Decimal VATPercentage
        {
            get { return _VATPercentage; }
            set { _VATPercentage = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedByName", DataType = "String")]
        public String CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }
    }
    #endregion
    #region vSalesInvoiceHd
    [Serializable]
    [Table(Name = "vSalesInvoiceHd")]
    public partial class vSalesInvoiceHd
    {
        private Int32 _SalesInvoiceID;
        private String _TransactionCode;
        private String _SalesInvoiceNo;
        private DateTime _SalesInvoiceDate;
        private Int32 _LocationID;
        private String _LocationCode;
        private String _LocationName;
        private Int32 _StudentID;
        private String _StudentCode;
        private String _StudentName;
        private Int32 _TermID;
        private String _TermName;
        private String _GCFrancoRegion;
        private String _FrancoRegion;
        private String _GCCurrencyCode;
        private String _CurrencyCode;
        private Decimal _CurrencyRate;
        private Boolean _IsIncludeVAT;
        private Decimal _TransactionAmount;
        private Decimal _FinalDiscountPercentage;
        private Decimal _VATPercentage;
        private Decimal _NetTransactionAmount;
        private String _Remarks;
        private String _GCTransactionStatus;
        private String _TransactionStatusWatermark;
        private String _CreatedByName;
        private String _ApprovedByName;
        private Int32 _NumberOfItems;

        [Column(Name = "SalesInvoiceID", DataType = "Int32")]
        public Int32 SalesInvoiceID
        {
            get { return _SalesInvoiceID; }
            set { _SalesInvoiceID = value; }
        }
        [Column(Name = "TransactionCode", DataType = "String")]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
        [Column(Name = "SalesInvoiceNo", DataType = "String")]
        public String SalesInvoiceNo
        {
            get { return _SalesInvoiceNo; }
            set { _SalesInvoiceNo = value; }
        }
        [Column(Name = "SalesInvoiceDate", DataType = "DateTime")]
        public DateTime SalesInvoiceDate
        {
            get { return _SalesInvoiceDate; }
            set { _SalesInvoiceDate = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "LocationCode", DataType = "String")]
        public String LocationCode
        {
            get { return _LocationCode; }
            set { _LocationCode = value; }
        }
        [Column(Name = "LocationName", DataType = "String")]
        public String LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "StudentCode", DataType = "String")]
        public String StudentCode
        {
            get { return _StudentCode; }
            set { _StudentCode = value; }
        }
        [Column(Name = "StudentName", DataType = "String")]
        public String StudentName
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }
        [Column(Name = "TermID", DataType = "Int32")]
        public Int32 TermID
        {
            get { return _TermID; }
            set { _TermID = value; }
        }
        [Column(Name = "TermName", DataType = "String")]
        public String TermName
        {
            get { return _TermName; }
            set { _TermName = value; }
        }
        [Column(Name = "GCFrancoRegion", DataType = "String")]
        public String GCFrancoRegion
        {
            get { return _GCFrancoRegion; }
            set { _GCFrancoRegion = value; }
        }
        [Column(Name = "FrancoRegion", DataType = "String")]
        public String FrancoRegion
        {
            get { return _FrancoRegion; }
            set { _FrancoRegion = value; }
        }
        [Column(Name = "GCCurrencyCode", DataType = "String")]
        public String GCCurrencyCode
        {
            get { return _GCCurrencyCode; }
            set { _GCCurrencyCode = value; }
        }
        [Column(Name = "CurrencyCode", DataType = "String")]
        public String CurrencyCode
        {
            get { return _CurrencyCode; }
            set { _CurrencyCode = value; }
        }
        [Column(Name = "CurrencyRate", DataType = "Decimal")]
        public Decimal CurrencyRate
        {
            get { return _CurrencyRate; }
            set { _CurrencyRate = value; }
        }
        [Column(Name = "IsIncludeVAT", DataType = "Boolean")]
        public Boolean IsIncludeVAT
        {
            get { return _IsIncludeVAT; }
            set { _IsIncludeVAT = value; }
        }
        [Column(Name = "TransactionAmount", DataType = "Decimal")]
        public Decimal TransactionAmount
        {
            get { return _TransactionAmount; }
            set { _TransactionAmount = value; }
        }
        [Column(Name = "FinalDiscountPercentage", DataType = "Decimal")]
        public Decimal FinalDiscountPercentage
        {
            get { return _FinalDiscountPercentage; }
            set { _FinalDiscountPercentage = value; }
        }
        [Column(Name = "VATPercentage", DataType = "Decimal")]
        public Decimal VATPercentage
        {
            get { return _VATPercentage; }
            set { _VATPercentage = value; }
        }
        [Column(Name = "NetTransactionAmount", DataType = "Decimal")]
        public Decimal NetTransactionAmount
        {
            get { return _NetTransactionAmount; }
            set { _NetTransactionAmount = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
        [Column(Name = "CreatedByName", DataType = "String")]
        public String CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }
        [Column(Name = "ApprovedByName", DataType = "String")]
        public String ApprovedByName
        {
            get { return _ApprovedByName; }
            set { _ApprovedByName = value; }
        }
        [Column(Name = "NumberOfItems", DataType = "Int32")]
        public Int32 NumberOfItems
        {
            get { return _NumberOfItems; }
            set { _NumberOfItems = value; }
        }
    }
    #endregion
    #region vScholarship
    [Serializable]
    [Table(Name = "vScholarship")]
    public class vScholarship
    {
        private Int32 _ScholarshipID;
        private String _SiteID;
        private Int32 _SchoolPeriodID;
        private String _GCScholarshipType;
        private String _ScholarshipType;
        private String _ScholarshipName;
        private String _GCFromSchoolType;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "ScholarshipID", DataType = "Int32")]
        public Int32 ScholarshipID
        {
            get { return _ScholarshipID; }
            set { _ScholarshipID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "GCScholarshipType", DataType = "String")]
        public String GCScholarshipType
        {
            get { return _GCScholarshipType; }
            set { _GCScholarshipType = value; }
        }
        [Column(Name = "ScholarshipType", DataType = "String")]
        public String ScholarshipType
        {
            get { return _ScholarshipType; }
            set { _ScholarshipType = value; }
        }
        [Column(Name = "ScholarshipName", DataType = "String")]
        public String ScholarshipName
        {
            get { return _ScholarshipName; }
            set { _ScholarshipName = value; }
        }
        [Column(Name = "GCFromSchoolType", DataType = "String")]
        public String GCFromSchoolType
        {
            get { return _GCFromSchoolType; }
            set { _GCFromSchoolType = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vSchoolClass
    [Serializable]
    [Table(Name = "vSchoolClass")]
    public class vSchoolClass
    {
        private Int32 _SchoolClassID;
        private String _SchoolClassCode;
        private String _SchoolClassName;
        private Int32 _DailySchedulePackageID;
        private Int32 _PeriodClassTypeID;
        private Int32 _SchoolPeriodID;
        private String _SchoolPeriodName;
        private Int32 _PeriodSectionID;
        private String _PeriodSectionName;
        private Int32 _ClassTypeID;
        private String _ClassTypeName;
        private String _GCClassStudyType;
        private String _GCGrade;
        private String _GCMajor;
        private Int32 _RoomID;
        private String _RoomName;
        private Int32 _TeacherID;
        private String _TeacherName;
        private Int16 _MaxStudent;
        private String _NextGCGrade;
        private String _NextGrade;
        private Int32 _GradePromotionFormulaID;
        private Boolean _IsDeleted;

        [Column(Name = "SchoolClassID", DataType = "Int32")]
        public Int32 SchoolClassID
        {
            get { return _SchoolClassID; }
            set { _SchoolClassID = value; }
        }
        [Column(Name = "SchoolClassCode", DataType = "String")]
        public String SchoolClassCode
        {
            get { return _SchoolClassCode; }
            set { _SchoolClassCode = value; }
        }
        [Column(Name = "SchoolClassName", DataType = "String")]
        public String SchoolClassName
        {
            get { return _SchoolClassName; }
            set { _SchoolClassName = value; }
        }
        [Column(Name = "DailySchedulePackageID", DataType = "Int32")]
        public Int32 DailySchedulePackageID
        {
            get { return _DailySchedulePackageID; }
            set { _DailySchedulePackageID = value; }
        }
        [Column(Name = "PeriodClassTypeID", DataType = "Int32")]
        public Int32 PeriodClassTypeID
        {
            get { return _PeriodClassTypeID; }
            set { _PeriodClassTypeID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "SchoolPeriodName", DataType = "String")]
        public String SchoolPeriodName
        {
            get { return _SchoolPeriodName; }
            set { _SchoolPeriodName = value; }
        }
        [Column(Name = "PeriodSectionID", DataType = "Int32")]
        public Int32 PeriodSectionID
        {
            get { return _PeriodSectionID; }
            set { _PeriodSectionID = value; }
        }
        [Column(Name = "PeriodSectionName", DataType = "String")]
        public String PeriodSectionName
        {
            get { return _PeriodSectionName; }
            set { _PeriodSectionName = value; }
        }
        [Column(Name = "ClassTypeID", DataType = "Int32")]
        public Int32 ClassTypeID
        {
            get { return _ClassTypeID; }
            set { _ClassTypeID = value; }
        }
        [Column(Name = "ClassTypeName", DataType = "String")]
        public String ClassTypeName
        {
            get { return _ClassTypeName; }
            set { _ClassTypeName = value; }
        }
        [Column(Name = "GCClassStudyType", DataType = "String")]
        public String GCClassStudyType
        {
            get { return _GCClassStudyType; }
            set { _GCClassStudyType = value; }
        }
        [Column(Name = "GCGrade", DataType = "String")]
        public String GCGrade
        {
            get { return _GCGrade; }
            set { _GCGrade = value; }
        }
        [Column(Name = "GCMajor", DataType = "String")]
        public String GCMajor
        {
            get { return _GCMajor; }
            set { _GCMajor = value; }
        }
        [Column(Name = "RoomID", DataType = "Int32")]
        public Int32 RoomID
        {
            get { return _RoomID; }
            set { _RoomID = value; }
        }
        [Column(Name = "RoomName", DataType = "String")]
        public String RoomName
        {
            get { return _RoomName; }
            set { _RoomName = value; }
        }
        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
        }
        [Column(Name = "TeacherName", DataType = "String")]
        public String TeacherName
        {
            get { return _TeacherName; }
            set { _TeacherName = value; }
        }
        [Column(Name = "MaxStudent", DataType = "Int16")]
        public Int16 MaxStudent
        {
            get { return _MaxStudent; }
            set { _MaxStudent = value; }
        }
        [Column(Name = "NextGCGrade", DataType = "String")]
        public String NextGCGrade
        {
            get { return _NextGCGrade; }
            set { _NextGCGrade = value; }
        }
        [Column(Name = "NextGrade", DataType = "String")]
        public String NextGrade
        {
            get { return _NextGrade; }
            set { _NextGrade = value; }
        }
        [Column(Name = "GradePromotionFormulaID", DataType = "Int32")]
        public Int32 GradePromotionFormulaID
        {
            get { return _GradePromotionFormulaID; }
            set { _GradePromotionFormulaID = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vSchoolGrade
    [Serializable]
    [Table(Name = "vSchoolGrade")]
    public class vSchoolGrade
    {
        private String _SiteID;
        private String _GCGrade;
        private String _Grade;
        private Int16 _DisplayOrder;
        private Boolean _IsAllowRegistration;
        private Boolean _IsNeedNationalStudentNo;

        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "GCGrade", DataType = "String")]
        public String GCGrade
        {
            get { return _GCGrade; }
            set { _GCGrade = value; }
        }
        [Column(Name = "Grade", DataType = "String")]
        public String Grade
        {
            get { return _Grade; }
            set { _Grade = value; }
        }
        [Column(Name = "DisplayOrder", DataType = "Int16")]
        public Int16 DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        [Column(Name = "IsAllowRegistration", DataType = "Boolean")]
        public Boolean IsAllowRegistration
        {
            get { return _IsAllowRegistration; }
            set { _IsAllowRegistration = value; }
        }
        [Column(Name = "IsNeedNationalStudentNo", DataType = "Boolean")]
        public Boolean IsNeedNationalStudentNo
        {
            get { return _IsNeedNationalStudentNo; }
            set { _IsNeedNationalStudentNo = value; }
        }
    }
    #endregion
    #region vSchoolMajor
    [Serializable]
    [Table(Name = "vSchoolMajor")]
    public class vSchoolMajor
    {
        private String _SiteID;
        private String _GCMajor;
        private String _Major;

        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "GCMajor", DataType = "String")]
        public String GCMajor
        {
            get { return _GCMajor; }
            set { _GCMajor = value; }
        }
        [Column(Name = "Major", DataType = "String")]
        public String Major
        {
            get { return _Major; }
            set { _Major = value; }
        }
    }
    #endregion
    #region vSchoolPeriod
    [Serializable]
    [Table(Name = "vSchoolPeriod")]
    public class vSchoolPeriod
    {
        private Int32 _SchoolPeriodID;
        private String _SchoolPeriodCode;
        private String _SchoolPeriodName;
        private String _SiteID;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private Int32 _DailySchedulePackageID;
        private Int32 _ExamSchedulePackageID;
        private Int32 _GradePromotionFormulaID;
        private String _GCSchoolPeriodStatus;
        private String _SchoolPeriodStatus;
        private String _Remarks;

        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "SchoolPeriodCode", DataType = "String")]
        public String SchoolPeriodCode
        {
            get { return _SchoolPeriodCode; }
            set { _SchoolPeriodCode = value; }
        }
        [Column(Name = "SchoolPeriodName", DataType = "String")]
        public String SchoolPeriodName
        {
            get { return _SchoolPeriodName; }
            set { _SchoolPeriodName = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "StartDate", DataType = "DateTime")]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Column(Name = "EndDate", DataType = "DateTime")]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        [Column(Name = "DailySchedulePackageID", DataType = "Int32")]
        public Int32 DailySchedulePackageID
        {
            get { return _DailySchedulePackageID; }
            set { _DailySchedulePackageID = value; }
        }
        [Column(Name = "ExamSchedulePackageID", DataType = "Int32")]
        public Int32 ExamSchedulePackageID
        {
            get { return _ExamSchedulePackageID; }
            set { _ExamSchedulePackageID = value; }
        }
        [Column(Name = "GradePromotionFormulaID", DataType = "Int32")]
        public Int32 GradePromotionFormulaID
        {
            get { return _GradePromotionFormulaID; }
            set { _GradePromotionFormulaID = value; }
        }
        [Column(Name = "GCSchoolPeriodStatus", DataType = "String")]
        public String GCSchoolPeriodStatus
        {
            get { return _GCSchoolPeriodStatus; }
            set { _GCSchoolPeriodStatus = value; }
        }
        [Column(Name = "SchoolPeriodStatus", DataType = "String")]
        public String SchoolPeriodStatus
        {
            get { return _SchoolPeriodStatus; }
            set { _SchoolPeriodStatus = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
    }
    #endregion
    #region vSiteItem
    [Serializable]
    [Table(Name = "vSiteItem")]
    public class vSiteItem
    {
        private Int32 _SiteItemID;
        private String _SiteID;
        private String _SiteName;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private Boolean _IsDeleted;

        [Column(Name = "SiteItemID", DataType = "Int32")]
        public Int32 SiteItemID
        {
            get { return _SiteItemID; }
            set { _SiteItemID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "SiteName", DataType = "String")]
        public String SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vStockTakingDt
    [Serializable]
    [Table(Name = "vStockTakingDt")]
    public class vStockTakingDt
    {
        private Int32 _StockTakingID;
        private String _StockTakingNo;
        private DateTime _FormDate;
        private Int32 _LocationID;
        private String _LocationCode;
        private String _LocationName;
        private String _GCTransactionStatus;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private Int32 _MovementID;
        private DateTime _StartDate;
        private String _StartTime;
        private DateTime _EndDate;
        private String _EndTime;
        private Decimal _QuantityBSO;
        private Decimal _QuantityAdjustment;
        private Decimal _QuantityEND;
        private String _GCItemUnit;
        private String _ItemUnit;
        private String _GCPurchaseUnit;
        private String _PurchaseUnit;
        private Decimal _ConversionFactor;
        private String _GCCheckCountType;
        private String _CheckCountType;
        private String _GCItemDetailStatus;
        private Boolean _IsControlExpired;

        [Column(Name = "StockTakingID", DataType = "Int32")]
        public Int32 StockTakingID
        {
            get { return _StockTakingID; }
            set { _StockTakingID = value; }
        }
        [Column(Name = "StockTakingNo", DataType = "String")]
        public String StockTakingNo
        {
            get { return _StockTakingNo; }
            set { _StockTakingNo = value; }
        }
        [Column(Name = "FormDate", DataType = "DateTime")]
        public DateTime FormDate
        {
            get { return _FormDate; }
            set { _FormDate = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "LocationCode", DataType = "String")]
        public String LocationCode
        {
            get { return _LocationCode; }
            set { _LocationCode = value; }
        }
        [Column(Name = "LocationName", DataType = "String")]
        public String LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "MovementID", DataType = "Int32")]
        public Int32 MovementID
        {
            get { return _MovementID; }
            set { _MovementID = value; }
        }
        [Column(Name = "StartDate", DataType = "DateTime")]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Column(Name = "StartTime", DataType = "String")]
        public String StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        [Column(Name = "EndDate", DataType = "DateTime")]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        [Column(Name = "EndTime", DataType = "String")]
        public String EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        [Column(Name = "QuantityBSO", DataType = "Decimal")]
        public Decimal QuantityBSO
        {
            get { return _QuantityBSO; }
            set { _QuantityBSO = value; }
        }
        [Column(Name = "QuantityAdjustment", DataType = "Decimal")]
        public Decimal QuantityAdjustment
        {
            get { return _QuantityAdjustment; }
            set { _QuantityAdjustment = value; }
        }
        [Column(Name = "QuantityEND", DataType = "Decimal")]
        public Decimal QuantityEND
        {
            get { return _QuantityEND; }
            set { _QuantityEND = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "GCPurchaseUnit", DataType = "String")]
        public String GCPurchaseUnit
        {
            get { return _GCPurchaseUnit; }
            set { _GCPurchaseUnit = value; }
        }
        [Column(Name = "PurchaseUnit", DataType = "String")]
        public String PurchaseUnit
        {
            get { return _PurchaseUnit; }
            set { _PurchaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "GCCheckCountType", DataType = "String")]
        public String GCCheckCountType
        {
            get { return _GCCheckCountType; }
            set { _GCCheckCountType = value; }
        }
        [Column(Name = "CheckCountType", DataType = "String")]
        public String CheckCountType
        {
            get { return _CheckCountType; }
            set { _CheckCountType = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
        [Column(Name = "IsControlExpired", DataType = "Boolean")]
        public Boolean IsControlExpired
        {
            get { return _IsControlExpired; }
            set { _IsControlExpired = value; }
        }
    }
    #endregion
    #region vStockTakingHd
    [Serializable]
    [Table(Name = "vStockTakingHd")]
    public partial class vStockTakingHd
    {
        private Int32 _StockTakingID;
        private String _StockTakingNo;
        private DateTime _FormDate;
        private Int32 _LocationID;
        private String _LocationCode;
        private String _LocationName;
        private String _Remarks;
        private String _GCTransactionStatus;
        private String _TransactionStatusWatermark;
        private String _GCVoidReason;
        private String _VoidReason;

        [Column(Name = "StockTakingID", DataType = "Int32")]
        public Int32 StockTakingID
        {
            get { return _StockTakingID; }
            set { _StockTakingID = value; }
        }
        [Column(Name = "StockTakingNo", DataType = "String")]
        public String StockTakingNo
        {
            get { return _StockTakingNo; }
            set { _StockTakingNo = value; }
        }
        [Column(Name = "FormDate", DataType = "DateTime")]
        public DateTime FormDate
        {
            get { return _FormDate; }
            set { _FormDate = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "LocationCode", DataType = "String")]
        public String LocationCode
        {
            get { return _LocationCode; }
            set { _LocationCode = value; }
        }
        [Column(Name = "LocationName", DataType = "String")]
        public String LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
        [Column(Name = "GCVoidReason", DataType = "String")]
        public String GCVoidReason
        {
            get { return _GCVoidReason; }
            set { _GCVoidReason = value; }
        }
        [Column(Name = "VoidReason", DataType = "String")]
        public String VoidReason
        {
            get { return _VoidReason; }
            set { _VoidReason = value; }
        }
    }
    #endregion
    #region vStudent
    [Serializable]
    [Table(Name = "vStudent")]
    public partial class vStudent
    {
        private Int32 _StudentID;
        private String _StudentCode;
        private String _NationalStudentNo;
        private String _VirtualAccountNo;
        private String _SiteID;
        private String _GCSalutation;
        private String _GCSuffix;
        private String _GCStudentStatus;
        private String _GCTitle;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _StudentName;
        private String _Name;
        private String _PreferredName;
        private String _CityOfBirth;
        private DateTime _DateOfBirth;
        private String _GCGender;
        private String _GCNationality;
        private String _GCReligion;
        private String _PlaceOfBaptism;
        private DateTime _DateOfBaptism;
        private String _GCGrade;
        private String _GCMajor;
        private Int32 _SchoolClassID;
        private String _SchoolClassName;
        private Boolean _IsFeeder;
        private String _AddressID;
        private String _StreetName;
        private String _District;
        private String _City;
        private String _County;
        private String _GCState;
        private String _State;
        private Int32 _ZipCodeID;
        private String _ZipCode;
        private String _EmailAddress1;
        private String _EmailAddress2;
        private String _MobilePhoneNo1;
        private String _MobilePhoneNo2;
        private String _PhoneNo1;
        private String _PictureFileName;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "StudentCode", DataType = "String")]
        public String StudentCode
        {
            get { return _StudentCode; }
            set { _StudentCode = value; }
        }
        [Column(Name = "NationalStudentNo", DataType = "String")]
        public String NationalStudentNo
        {
            get { return _NationalStudentNo; }
            set { _NationalStudentNo = value; }
        }
        [Column(Name = "VirtualAccountNo", DataType = "String")]
        public String VirtualAccountNo
        {
            get { return _VirtualAccountNo; }
            set { _VirtualAccountNo = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "GCSalutation", DataType = "String")]
        public String GCSalutation
        {
            get { return _GCSalutation; }
            set { _GCSalutation = value; }
        }
        [Column(Name = "GCSuffix", DataType = "String")]
        public String GCSuffix
        {
            get { return _GCSuffix; }
            set { _GCSuffix = value; }
        }
        [Column(Name = "GCStudentStatus", DataType = "String")]
        public String GCStudentStatus
        {
            get { return _GCStudentStatus; }
            set { _GCStudentStatus = value; }
        }
        [Column(Name = "GCTitle", DataType = "String")]
        public String GCTitle
        {
            get { return _GCTitle; }
            set { _GCTitle = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "StudentName", DataType = "String")]
        public String StudentName
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }
        [Column(Name = "Name", DataType = "String")]
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        [Column(Name = "PreferredName", DataType = "String")]
        public String PreferredName
        {
            get { return _PreferredName; }
            set { _PreferredName = value; }
        }
        [Column(Name = "CityOfBirth", DataType = "String")]
        public String CityOfBirth
        {
            get { return _CityOfBirth; }
            set { _CityOfBirth = value; }
        }
        [Column(Name = "DateOfBirth", DataType = "DateTime")]
        public DateTime DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }
        [Column(Name = "GCGender", DataType = "String")]
        public String GCGender
        {
            get { return _GCGender; }
            set { _GCGender = value; }
        }
        [Column(Name = "GCNationality", DataType = "String")]
        public String GCNationality
        {
            get { return _GCNationality; }
            set { _GCNationality = value; }
        }
        [Column(Name = "GCReligion", DataType = "String")]
        public String GCReligion
        {
            get { return _GCReligion; }
            set { _GCReligion = value; }
        }
        [Column(Name = "PlaceOfBaptism", DataType = "String")]
        public String PlaceOfBaptism
        {
            get { return _PlaceOfBaptism; }
            set { _PlaceOfBaptism = value; }
        }
        [Column(Name = "DateOfBaptism", DataType = "DateTime")]
        public DateTime DateOfBaptism
        {
            get { return _DateOfBaptism; }
            set { _DateOfBaptism = value; }
        }
        [Column(Name = "GCGrade", DataType = "String")]
        public String GCGrade
        {
            get { return _GCGrade; }
            set { _GCGrade = value; }
        }
        [Column(Name = "GCMajor", DataType = "String")]
        public String GCMajor
        {
            get { return _GCMajor; }
            set { _GCMajor = value; }
        }
        [Column(Name = "SchoolClassID", DataType = "Int32")]
        public Int32 SchoolClassID
        {
            get { return _SchoolClassID; }
            set { _SchoolClassID = value; }
        }
        [Column(Name = "SchoolClassName", DataType = "String")]
        public String SchoolClassName
        {
            get { return _SchoolClassName; }
            set { _SchoolClassName = value; }
        }
        [Column(Name = "IsFeeder", DataType = "Boolean")]
        public Boolean IsFeeder
        {
            get { return _IsFeeder; }
            set { _IsFeeder = value; }
        }
        [Column(Name = "AddressID", DataType = "String")]
        public String AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
        }
        [Column(Name = "StreetName", DataType = "String")]
        public String StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }
        [Column(Name = "District", DataType = "String")]
        public String District
        {
            get { return _District; }
            set { _District = value; }
        }
        [Column(Name = "City", DataType = "String")]
        public String City
        {
            get { return _City; }
            set { _City = value; }
        }
        [Column(Name = "County", DataType = "String")]
        public String County
        {
            get { return _County; }
            set { _County = value; }
        }
        [Column(Name = "GCState", DataType = "String")]
        public String GCState
        {
            get { return _GCState; }
            set { _GCState = value; }
        }
        [Column(Name = "State", DataType = "String")]
        public String State
        {
            get { return _State; }
            set { _State = value; }
        }
        [Column(Name = "ZipCodeID", DataType = "Int32")]
        public Int32 ZipCodeID
        {
            get { return _ZipCodeID; }
            set { _ZipCodeID = value; }
        }
        [Column(Name = "ZipCode", DataType = "String")]
        public String ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }
        [Column(Name = "EmailAddress1", DataType = "String")]
        public String EmailAddress1
        {
            get { return _EmailAddress1; }
            set { _EmailAddress1 = value; }
        }
        [Column(Name = "EmailAddress2", DataType = "String")]
        public String EmailAddress2
        {
            get { return _EmailAddress2; }
            set { _EmailAddress2 = value; }
        }
        [Column(Name = "MobilePhoneNo1", DataType = "String")]
        public String MobilePhoneNo1
        {
            get { return _MobilePhoneNo1; }
            set { _MobilePhoneNo1 = value; }
        }
        [Column(Name = "MobilePhoneNo2", DataType = "String")]
        public String MobilePhoneNo2
        {
            get { return _MobilePhoneNo2; }
            set { _MobilePhoneNo2 = value; }
        }
        [Column(Name = "PhoneNo1", DataType = "String")]
        public String PhoneNo1
        {
            get { return _PhoneNo1; }
            set { _PhoneNo1 = value; }
        }
        [Column(Name = "PictureFileName", DataType = "String")]
        public String PictureFileName
        {
            get { return _PictureFileName; }
            set { _PictureFileName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vStudentAchievement
    [Serializable]
    [Table(Name = "vStudentAchievement")]
    public partial class vStudentAchievement
    {
        private Int32 _StudentAchievementID;
        private String _StudentCode;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _StudentName;
        private String _Name;
        private Int32 _StudentID;
        private DateTime _AchievementDate;
        private String _GCAchievementType;
        private String _AchievementType;
        private String _AchievementName;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "StudentAchievementID", DataType = "Int32")]
        public Int32 StudentAchievementID
        {
            get { return _StudentAchievementID; }
            set { _StudentAchievementID = value; }
        }
        [Column(Name = "StudentCode", DataType = "String")]
        public String StudentCode
        {
            get { return _StudentCode; }
            set { _StudentCode = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "StudentName", DataType = "String")]
        public String StudentName
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }
        [Column(Name = "Name", DataType = "String")]
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "AchievementDate", DataType = "DateTime")]
        public DateTime AchievementDate
        {
            get { return _AchievementDate; }
            set { _AchievementDate = value; }
        }
        [Column(Name = "GCAchievementType", DataType = "String")]
        public String GCAchievementType
        {
            get { return _GCAchievementType; }
            set { _GCAchievementType = value; }
        }
        [Column(Name = "AchievementType", DataType = "String")]
        public String AchievementType
        {
            get { return _AchievementType; }
            set { _AchievementType = value; }
        }
        [Column(Name = "AchievementName", DataType = "String")]
        public String AchievementName
        {
            get { return _AchievementName; }
            set { _AchievementName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vStudentCoverageTransactionDtCustom
    [Serializable]
    [Table(Name = "vStudentCoverageTransactionDtCustom")]
    public class vStudentCoverageTransactionDtCustom
    {
        private Int32 _TransactionID;
        private Int32 _CoverageTypeID;
        private String _CoverageTypeName;
        private String _ListStudentID;
        private String _ListStudentName;

        [Column(Name = "TransactionID", DataType = "Int32")]
        public Int32 TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }
        [Column(Name = "CoverageTypeID", DataType = "Int32")]
        public Int32 CoverageTypeID
        {
            get { return _CoverageTypeID; }
            set { _CoverageTypeID = value; }
        }
        [Column(Name = "CoverageTypeName", DataType = "String")]
        public String CoverageTypeName
        {
            get { return _CoverageTypeName; }
            set { _CoverageTypeName = value; }
        }
        [Column(Name = "ListStudentID", DataType = "String")]
        public String ListStudentID
        {
            get { return _ListStudentID; }
            set { _ListStudentID = value; }
        }
        [Column(Name = "ListStudentName", DataType = "String")]
        public String ListStudentName
        {
            get { return _ListStudentName; }
            set { _ListStudentName = value; }
        }
    }
    #endregion
    #region vStudentCoverageTransactionHd
    [Serializable]
    [Table(Name = "vStudentCoverageTransactionHd")]
    public partial class vStudentCoverageTransactionHd
    {
        private Int32 _TransactionID;
        private String _TransactionCode;
        private DateTime _TransactionDate;
        private String _TransactionNo;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private DateTime _StartingDate;
        private String _ReferenceNo;
        private String _Remarks;
        private String _GCTransactionStatus;
        private String _TransactionStatusWatermark;

        [Column(Name = "TransactionID", DataType = "Int32")]
        public Int32 TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }
        [Column(Name = "TransactionCode", DataType = "String")]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
        [Column(Name = "TransactionDate", DataType = "DateTime")]
        public DateTime TransactionDate
        {
            get { return _TransactionDate; }
            set { _TransactionDate = value; }
        }
        [Column(Name = "TransactionNo", DataType = "String")]
        public String TransactionNo
        {
            get { return _TransactionNo; }
            set { _TransactionNo = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "StartingDate", DataType = "DateTime")]
        public DateTime StartingDate
        {
            get { return _StartingDate; }
            set { _StartingDate = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String")]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
    }
    #endregion
    #region vStudentCustom
    [Serializable]
    [Table(Name = "vStudentCustom")]
    public partial class vStudentCustom
    {
        private Int32 _StudentID;
        private Int32 _SchoolClassID;
        private String _StudentCode;
        private String _StudentName;
        private String _GCGender;
        private String _PictureFileName;
        private String _GCClassStudentStatus;
        private String _ClassStudentStatus;

        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "SchoolClassID", DataType = "Int32")]
        public Int32 SchoolClassID
        {
            get { return _SchoolClassID; }
            set { _SchoolClassID = value; }
        }
        [Column(Name = "StudentCode", DataType = "String")]
        public String StudentCode
        {
            get { return _StudentCode; }
            set { _StudentCode = value; }
        }
        [Column(Name = "StudentName", DataType = "String")]
        public String StudentName
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }
        [Column(Name = "GCGender", DataType = "String")]
        public String GCGender
        {
            get { return _GCGender; }
            set { _GCGender = value; }
        }
        [Column(Name = "PictureFileName", DataType = "String")]
        public String PictureFileName
        {
            get { return _PictureFileName; }
            set { _PictureFileName = value; }
        }
        [Column(Name = "GCClassStudentStatus", DataType = "String")]
        public String GCClassStudentStatus
        {
            get { return _GCClassStudentStatus; }
            set { _GCClassStudentStatus = value; }
        }
        [Column(Name = "ClassStudentStatus", DataType = "String")]
        public String ClassStudentStatus
        {
            get { return _ClassStudentStatus; }
            set { _ClassStudentStatus = value; }
        }
    }
    #endregion
    #region vStudentFamily
    [Serializable]
    [Table(Name = "vStudentFamily")]
    public partial class vStudentFamily
    {
        private Int32 _FamilyID;
        private Int32 _StudentID;
        private String _GCFamilyRelation;
        private String _FamilyRelation;
        private String _GCSalutation;
        private String _GCTitle;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _FullName;
        private String _FamilyName;
        private String _CityOfBirth;
        private DateTime _DateOfBirth;
        private String _GCSuffix;
        private String _GCReligion;
        private String _GCNationality;
        private String _GCEducationLevel;
        private String _CompanyName;
        private String _GCJob;
        private String _Occupation;
        private Decimal _Salary;
        private String _OfficeAddressID;
        private String _OfficeStreetName;
        private String _OfficeDistrict;
        private String _OfficeCity;
        private String _OfficeCounty;
        private String _OfficeGCState;
        private String _OfficeState;
        private Int32 _OfficeZipCodeID;
        private String _OfficeZipCode;
        private String _OfficePhoneNo1;
        private String _EmailAddress;
        private String _MobilePhoneNo1;
        private String _MobilePhoneNo2;
        private Boolean _IsDeleted;

        [Column(Name = "FamilyID", DataType = "Int32")]
        public Int32 FamilyID
        {
            get { return _FamilyID; }
            set { _FamilyID = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "GCFamilyRelation", DataType = "String")]
        public String GCFamilyRelation
        {
            get { return _GCFamilyRelation; }
            set { _GCFamilyRelation = value; }
        }
        [Column(Name = "FamilyRelation", DataType = "String")]
        public String FamilyRelation
        {
            get { return _FamilyRelation; }
            set { _FamilyRelation = value; }
        }
        [Column(Name = "GCSalutation", DataType = "String")]
        public String GCSalutation
        {
            get { return _GCSalutation; }
            set { _GCSalutation = value; }
        }
        [Column(Name = "GCTitle", DataType = "String")]
        public String GCTitle
        {
            get { return _GCTitle; }
            set { _GCTitle = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "FullName", DataType = "String")]
        public String FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
        [Column(Name = "FamilyName", DataType = "String")]
        public String FamilyName
        {
            get { return _FamilyName; }
            set { _FamilyName = value; }
        }
        [Column(Name = "CityOfBirth", DataType = "String")]
        public String CityOfBirth
        {
            get { return _CityOfBirth; }
            set { _CityOfBirth = value; }
        }
        [Column(Name = "DateOfBirth", DataType = "DateTime")]
        public DateTime DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }
        [Column(Name = "GCSuffix", DataType = "String")]
        public String GCSuffix
        {
            get { return _GCSuffix; }
            set { _GCSuffix = value; }
        }
        [Column(Name = "GCReligion", DataType = "String")]
        public String GCReligion
        {
            get { return _GCReligion; }
            set { _GCReligion = value; }
        }
        [Column(Name = "GCNationality", DataType = "String")]
        public String GCNationality
        {
            get { return _GCNationality; }
            set { _GCNationality = value; }
        }
        [Column(Name = "GCEducationLevel", DataType = "String")]
        public String GCEducationLevel
        {
            get { return _GCEducationLevel; }
            set { _GCEducationLevel = value; }
        }
        [Column(Name = "CompanyName", DataType = "String")]
        public String CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        [Column(Name = "GCJob", DataType = "String")]
        public String GCJob
        {
            get { return _GCJob; }
            set { _GCJob = value; }
        }
        [Column(Name = "Occupation", DataType = "String")]
        public String Occupation
        {
            get { return _Occupation; }
            set { _Occupation = value; }
        }
        [Column(Name = "Salary", DataType = "Decimal")]
        public Decimal Salary
        {
            get { return _Salary; }
            set { _Salary = value; }
        }
        [Column(Name = "OfficeAddressID", DataType = "String")]
        public String OfficeAddressID
        {
            get { return _OfficeAddressID; }
            set { _OfficeAddressID = value; }
        }
        [Column(Name = "OfficeStreetName", DataType = "String")]
        public String OfficeStreetName
        {
            get { return _OfficeStreetName; }
            set { _OfficeStreetName = value; }
        }
        [Column(Name = "OfficeDistrict", DataType = "String")]
        public String OfficeDistrict
        {
            get { return _OfficeDistrict; }
            set { _OfficeDistrict = value; }
        }
        [Column(Name = "OfficeCity", DataType = "String")]
        public String OfficeCity
        {
            get { return _OfficeCity; }
            set { _OfficeCity = value; }
        }
        [Column(Name = "OfficeCounty", DataType = "String")]
        public String OfficeCounty
        {
            get { return _OfficeCounty; }
            set { _OfficeCounty = value; }
        }
        [Column(Name = "OfficeGCState", DataType = "String")]
        public String OfficeGCState
        {
            get { return _OfficeGCState; }
            set { _OfficeGCState = value; }
        }
        [Column(Name = "OfficeState", DataType = "String")]
        public String OfficeState
        {
            get { return _OfficeState; }
            set { _OfficeState = value; }
        }
        [Column(Name = "OfficeZipCodeID", DataType = "Int32")]
        public Int32 OfficeZipCodeID
        {
            get { return _OfficeZipCodeID; }
            set { _OfficeZipCodeID = value; }
        }
        [Column(Name = "OfficeZipCode", DataType = "String")]
        public String OfficeZipCode
        {
            get { return _OfficeZipCode; }
            set { _OfficeZipCode = value; }
        }
        [Column(Name = "OfficePhoneNo1", DataType = "String")]
        public String OfficePhoneNo1
        {
            get { return _OfficePhoneNo1; }
            set { _OfficePhoneNo1 = value; }
        }
        [Column(Name = "EmailAddress", DataType = "String")]
        public String EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        [Column(Name = "MobilePhoneNo1", DataType = "String")]
        public String MobilePhoneNo1
        {
            get { return _MobilePhoneNo1; }
            set { _MobilePhoneNo1 = value; }
        }
        [Column(Name = "MobilePhoneNo2", DataType = "String")]
        public String MobilePhoneNo2
        {
            get { return _MobilePhoneNo2; }
            set { _MobilePhoneNo2 = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vStudentFee
    [Serializable]
    [Table(Name = "vStudentFee")]
    public partial class vStudentFee
    {
        private Int32 _StudentFeeID;
        private Int32 _StudentID;
        private String _StudentCode;
        private String _StudentName;
        private Int32 _ProspectiveStudentID;
        private Int32 _BusinessPartnerID;
        private Int32 _SchoolPeriodID;
        private Int32 _StudentFeeCompID;
        private Int32 _StudentFeeCompTypeID;
        private String _StudentFeeCompTypeName;
        private String _GCAdmissionPaymentPeriod;
        private Int16 _DisplayOrder;
        private DateTime _DueDate;
        private Int32 _TransactionMonth;
        private Int32 _TransactionYear;
        private Decimal _TransactionAmount;
        private Boolean _IsDiscountAmountInPercentage;
        private Decimal _DiscountAmount;
        private Decimal _TotalDiscountAmount;
        private Decimal _StudentAmount;
        private Decimal _StudentPenaltyAmount;
        private Boolean _IsStudentPenaltyAmountInPercentage;
        private Decimal _TotalStudentPenaltyAmount;
        private Decimal _TotalStudentAmount;
        private Decimal _PayerAmount;
        private Decimal _LineAmount;
        private Boolean _IsPaid;
        private Boolean _IsDeleted;

        [Column(Name = "StudentFeeID", DataType = "Int32")]
        public Int32 StudentFeeID
        {
            get { return _StudentFeeID; }
            set { _StudentFeeID = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "StudentCode", DataType = "String")]
        public String StudentCode
        {
            get { return _StudentCode; }
            set { _StudentCode = value; }
        }
        [Column(Name = "StudentName", DataType = "String")]
        public String StudentName
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }
        [Column(Name = "ProspectiveStudentID", DataType = "Int32")]
        public Int32 ProspectiveStudentID
        {
            get { return _ProspectiveStudentID; }
            set { _ProspectiveStudentID = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "StudentFeeCompID", DataType = "Int32")]
        public Int32 StudentFeeCompID
        {
            get { return _StudentFeeCompID; }
            set { _StudentFeeCompID = value; }
        }
        [Column(Name = "StudentFeeCompTypeID", DataType = "Int32")]
        public Int32 StudentFeeCompTypeID
        {
            get { return _StudentFeeCompTypeID; }
            set { _StudentFeeCompTypeID = value; }
        }
        [Column(Name = "StudentFeeCompTypeName", DataType = "String")]
        public String StudentFeeCompTypeName
        {
            get { return _StudentFeeCompTypeName; }
            set { _StudentFeeCompTypeName = value; }
        }
        [Column(Name = "GCAdmissionPaymentPeriod", DataType = "String")]
        public String GCAdmissionPaymentPeriod
        {
            get { return _GCAdmissionPaymentPeriod; }
            set { _GCAdmissionPaymentPeriod = value; }
        }
        [Column(Name = "DisplayOrder", DataType = "Int16")]
        public Int16 DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        [Column(Name = "DueDate", DataType = "DateTime")]
        public DateTime DueDate
        {
            get { return _DueDate; }
            set { _DueDate = value; }
        }
        [Column(Name = "TransactionMonth", DataType = "Int32")]
        public Int32 TransactionMonth
        {
            get { return _TransactionMonth; }
            set { _TransactionMonth = value; }
        }
        [Column(Name = "TransactionYear", DataType = "Int32")]
        public Int32 TransactionYear
        {
            get { return _TransactionYear; }
            set { _TransactionYear = value; }
        }
        [Column(Name = "TransactionAmount", DataType = "Decimal")]
        public Decimal TransactionAmount
        {
            get { return _TransactionAmount; }
            set { _TransactionAmount = value; }
        }
        [Column(Name = "IsDiscountAmountInPercentage", DataType = "Boolean")]
        public Boolean IsDiscountAmountInPercentage
        {
            get { return _IsDiscountAmountInPercentage; }
            set { _IsDiscountAmountInPercentage = value; }
        }
        [Column(Name = "DiscountAmount", DataType = "Decimal")]
        public Decimal DiscountAmount
        {
            get { return _DiscountAmount; }
            set { _DiscountAmount = value; }
        }
        [Column(Name = "TotalDiscountAmount", DataType = "Decimal")]
        public Decimal TotalDiscountAmount
        {
            get { return _TotalDiscountAmount; }
            set { _TotalDiscountAmount = value; }
        }
        [Column(Name = "StudentAmount", DataType = "Decimal")]
        public Decimal StudentAmount
        {
            get { return _StudentAmount; }
            set { _StudentAmount = value; }
        }
        [Column(Name = "StudentPenaltyAmount", DataType = "Decimal")]
        public Decimal StudentPenaltyAmount
        {
            get { return _StudentPenaltyAmount; }
            set { _StudentPenaltyAmount = value; }
        }
        [Column(Name = "IsStudentPenaltyAmountInPercentage", DataType = "Boolean")]
        public Boolean IsStudentPenaltyAmountInPercentage
        {
            get { return _IsStudentPenaltyAmountInPercentage; }
            set { _IsStudentPenaltyAmountInPercentage = value; }
        }
        [Column(Name = "TotalStudentPenaltyAmount", DataType = "Decimal")]
        public Decimal TotalStudentPenaltyAmount
        {
            get { return _TotalStudentPenaltyAmount; }
            set { _TotalStudentPenaltyAmount = value; }
        }
        [Column(Name = "TotalStudentAmount", DataType = "Decimal")]
        public Decimal TotalStudentAmount
        {
            get { return _TotalStudentAmount; }
            set { _TotalStudentAmount = value; }
        }
        [Column(Name = "PayerAmount", DataType = "Decimal")]
        public Decimal PayerAmount
        {
            get { return _PayerAmount; }
            set { _PayerAmount = value; }
        }
        [Column(Name = "LineAmount", DataType = "Decimal")]
        public Decimal LineAmount
        {
            get { return _LineAmount; }
            set { _LineAmount = value; }
        }
        [Column(Name = "IsPaid", DataType = "Boolean")]
        public Boolean IsPaid
        {
            get { return _IsPaid; }
            set { _IsPaid = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vStudentFeeComp
    [Serializable]
    [Table(Name = "vStudentFeeComp")]
    public partial class vStudentFeeComp
    {
        private Int32 _StudentFeeCompID;
        private Int32 _StudentID;
        private Int32 _ProspectiveStudentID;
        private Int32 _SchoolPeriodID;
        private Int32 _StudentFeeCompTypeID;
        private String _StudentFeeCompTypeName;
        private String _GCAdmissionPaymentPeriod;
        private String _AdmissionPaymentPeriod;
        private Int16 _PenaltyPercentage;
        private Decimal _TotalAmount;
        private Boolean _IsDeleted;

        [Column(Name = "StudentFeeCompID", DataType = "Int32")]
        public Int32 StudentFeeCompID
        {
            get { return _StudentFeeCompID; }
            set { _StudentFeeCompID = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "ProspectiveStudentID", DataType = "Int32")]
        public Int32 ProspectiveStudentID
        {
            get { return _ProspectiveStudentID; }
            set { _ProspectiveStudentID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "StudentFeeCompTypeID", DataType = "Int32")]
        public Int32 StudentFeeCompTypeID
        {
            get { return _StudentFeeCompTypeID; }
            set { _StudentFeeCompTypeID = value; }
        }
        [Column(Name = "StudentFeeCompTypeName", DataType = "String")]
        public String StudentFeeCompTypeName
        {
            get { return _StudentFeeCompTypeName; }
            set { _StudentFeeCompTypeName = value; }
        }
        [Column(Name = "GCAdmissionPaymentPeriod", DataType = "String")]
        public String GCAdmissionPaymentPeriod
        {
            get { return _GCAdmissionPaymentPeriod; }
            set { _GCAdmissionPaymentPeriod = value; }
        }
        [Column(Name = "AdmissionPaymentPeriod", DataType = "String")]
        public String AdmissionPaymentPeriod
        {
            get { return _AdmissionPaymentPeriod; }
            set { _AdmissionPaymentPeriod = value; }
        }
        [Column(Name = "PenaltyPercentage", DataType = "Int16")]
        public Int16 PenaltyPercentage
        {
            get { return _PenaltyPercentage; }
            set { _PenaltyPercentage = value; }
        }
        [Column(Name = "TotalAmount", DataType = "Decimal")]
        public Decimal TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vStudentFeeCompType
    [Serializable]
    [Table(Name = "vStudentFeeCompType")]
    public class vStudentFeeCompType
    {
        private Int32 _StudentFeeCompTypeID;
        private String _SiteID;
        private String _StudentFeeCompTypeName;
        private String _GCAdmissionPaymentPeriod;
        private String _AdmissionPaymentPeriod;
        private Boolean _IsDeleted;

        [Column(Name = "StudentFeeCompTypeID", DataType = "Int32")]
        public Int32 StudentFeeCompTypeID
        {
            get { return _StudentFeeCompTypeID; }
            set { _StudentFeeCompTypeID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "StudentFeeCompTypeName", DataType = "String")]
        public String StudentFeeCompTypeName
        {
            get { return _StudentFeeCompTypeName; }
            set { _StudentFeeCompTypeName = value; }
        }
        [Column(Name = "GCAdmissionPaymentPeriod", DataType = "String")]
        public String GCAdmissionPaymentPeriod
        {
            get { return _GCAdmissionPaymentPeriod; }
            set { _GCAdmissionPaymentPeriod = value; }
        }
        [Column(Name = "AdmissionPaymentPeriod", DataType = "String")]
        public String AdmissionPaymentPeriod
        {
            get { return _AdmissionPaymentPeriod; }
            set { _AdmissionPaymentPeriod = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vStudentFeeDt
    [Serializable]
    [Table(Name = "vStudentFeeDt")]
    public partial class vStudentFeeDt
    {
        private Int32 _StudentFeeDtID;
        private Int32 _StudentFeeID;
        private Int32 _StudentID;
        private Int32 _ProspectiveStudentID;
        private Int32 _SchoolPeriodID;
        private Int32 _StudentFeeCompID;
        private Int32 _StudentFeeCompTypeID;
        private String _StudentFeeCompTypeName;
        private String _GCAdmissionPaymentPeriod;
        private Int32 _TransactionMonth;
        private Int32 _TransactionYear;
        private Int16 _DisplayOrder;
        private DateTime _DueDate;
        private Boolean _IsTransactionAmountInPercentage;
        private Decimal _TransactionAmount;
        private Decimal _StudentAmount;
        private Decimal _TotalStudentPenaltyAmount;
        private Decimal _TotalStudentAmount;
        private Decimal _PayerAmount;
        private Decimal _LineAmount;
        private Boolean _IsTransferred;
        private Int32 _ARInvoiceDtID;
        private Boolean _IsPaid;
        private Boolean _IsDeleted;
        private String _GCTransactionStatus;

        [Column(Name = "StudentFeeDtID", DataType = "Int32")]
        public Int32 StudentFeeDtID
        {
            get { return _StudentFeeDtID; }
            set { _StudentFeeDtID = value; }
        }
        [Column(Name = "StudentFeeID", DataType = "Int32")]
        public Int32 StudentFeeID
        {
            get { return _StudentFeeID; }
            set { _StudentFeeID = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "ProspectiveStudentID", DataType = "Int32")]
        public Int32 ProspectiveStudentID
        {
            get { return _ProspectiveStudentID; }
            set { _ProspectiveStudentID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "StudentFeeCompID", DataType = "Int32")]
        public Int32 StudentFeeCompID
        {
            get { return _StudentFeeCompID; }
            set { _StudentFeeCompID = value; }
        }
        [Column(Name = "StudentFeeCompTypeID", DataType = "Int32")]
        public Int32 StudentFeeCompTypeID
        {
            get { return _StudentFeeCompTypeID; }
            set { _StudentFeeCompTypeID = value; }
        }
        [Column(Name = "StudentFeeCompTypeName", DataType = "String")]
        public String StudentFeeCompTypeName
        {
            get { return _StudentFeeCompTypeName; }
            set { _StudentFeeCompTypeName = value; }
        }
        [Column(Name = "GCAdmissionPaymentPeriod", DataType = "String")]
        public String GCAdmissionPaymentPeriod
        {
            get { return _GCAdmissionPaymentPeriod; }
            set { _GCAdmissionPaymentPeriod = value; }
        }
        [Column(Name = "TransactionMonth", DataType = "Int32")]
        public Int32 TransactionMonth
        {
            get { return _TransactionMonth; }
            set { _TransactionMonth = value; }
        }
        [Column(Name = "TransactionYear", DataType = "Int32")]
        public Int32 TransactionYear
        {
            get { return _TransactionYear; }
            set { _TransactionYear = value; }
        }
        [Column(Name = "DisplayOrder", DataType = "Int16")]
        public Int16 DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        [Column(Name = "DueDate", DataType = "DateTime")]
        public DateTime DueDate
        {
            get { return _DueDate; }
            set { _DueDate = value; }
        }
        [Column(Name = "IsTransactionAmountInPercentage", DataType = "Boolean")]
        public Boolean IsTransactionAmountInPercentage
        {
            get { return _IsTransactionAmountInPercentage; }
            set { _IsTransactionAmountInPercentage = value; }
        }
        [Column(Name = "TransactionAmount", DataType = "Decimal")]
        public Decimal TransactionAmount
        {
            get { return _TransactionAmount; }
            set { _TransactionAmount = value; }
        }
        [Column(Name = "StudentAmount", DataType = "Decimal")]
        public Decimal StudentAmount
        {
            get { return _StudentAmount; }
            set { _StudentAmount = value; }
        }
        [Column(Name = "TotalStudentPenaltyAmount", DataType = "Decimal")]
        public Decimal TotalStudentPenaltyAmount
        {
            get { return _TotalStudentPenaltyAmount; }
            set { _TotalStudentPenaltyAmount = value; }
        }
        [Column(Name = "TotalStudentAmount", DataType = "Decimal")]
        public Decimal TotalStudentAmount
        {
            get { return _TotalStudentAmount; }
            set { _TotalStudentAmount = value; }
        }
        [Column(Name = "PayerAmount", DataType = "Decimal")]
        public Decimal PayerAmount
        {
            get { return _PayerAmount; }
            set { _PayerAmount = value; }
        }
        [Column(Name = "LineAmount", DataType = "Decimal")]
        public Decimal LineAmount
        {
            get { return _LineAmount; }
            set { _LineAmount = value; }
        }
        [Column(Name = "IsTransferred", DataType = "Boolean")]
        public Boolean IsTransferred
        {
            get { return _IsTransferred; }
            set { _IsTransferred = value; }
        }
        [Column(Name = "ARInvoiceDtID", DataType = "Int32")]
        public Int32 ARInvoiceDtID
        {
            get { return _ARInvoiceDtID; }
            set { _ARInvoiceDtID = value; }
        }
        [Column(Name = "IsPaid", DataType = "Boolean")]
        public Boolean IsPaid
        {
            get { return _IsPaid; }
            set { _IsPaid = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
    }
    #endregion
    #region vStudentFinalMarkFormulaDt
    [Serializable]
    [Table(Name = "vStudentFinalMarkFormulaDt")]
    public class vStudentFinalMarkFormulaDt
    {
        private Int32 _StudentFinalMarkFormulaDtID;
        private Int32 _StudentFinalMarkFormulaID;
        private String _StudentFinalMarkFormulaDtName;
        private Int16 _DisplayOrder;
        private Decimal _FinalMarkPercentage;
        private String _ListGCTaskType;
        private String _ListTaskType;
        private Boolean _IsDeleted;

        [Column(Name = "StudentFinalMarkFormulaDtID", DataType = "Int32")]
        public Int32 StudentFinalMarkFormulaDtID
        {
            get { return _StudentFinalMarkFormulaDtID; }
            set { _StudentFinalMarkFormulaDtID = value; }
        }
        [Column(Name = "StudentFinalMarkFormulaID", DataType = "Int32")]
        public Int32 StudentFinalMarkFormulaID
        {
            get { return _StudentFinalMarkFormulaID; }
            set { _StudentFinalMarkFormulaID = value; }
        }
        [Column(Name = "StudentFinalMarkFormulaDtName", DataType = "String")]
        public String StudentFinalMarkFormulaDtName
        {
            get { return _StudentFinalMarkFormulaDtName; }
            set { _StudentFinalMarkFormulaDtName = value; }
        }
        [Column(Name = "DisplayOrder", DataType = "Int16")]
        public Int16 DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        [Column(Name = "FinalMarkPercentage", DataType = "Decimal")]
        public Decimal FinalMarkPercentage
        {
            get { return _FinalMarkPercentage; }
            set { _FinalMarkPercentage = value; }
        }
        [Column(Name = "ListGCTaskType", DataType = "String")]
        public String ListGCTaskType
        {
            get { return _ListGCTaskType; }
            set { _ListGCTaskType = value; }
        }
        [Column(Name = "ListTaskType", DataType = "String")]
        public String ListTaskType
        {
            get { return _ListTaskType; }
            set { _ListTaskType = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vStudentPastStudy
    [Serializable]
    [Table(Name = "vStudentPastStudy")]
    public class vStudentPastStudy
    {
        private Int32 _StudentPastStudyID;
        private Int32 _StudentID;
        private Int32 _StartYear;
        private Int32 _EndYear;
        private String _GCSchoolType;
        private String _SchoolType;
        private String _SchoolName;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "StudentPastStudyID", DataType = "Int32")]
        public Int32 StudentPastStudyID
        {
            get { return _StudentPastStudyID; }
            set { _StudentPastStudyID = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "StartYear", DataType = "Int32")]
        public Int32 StartYear
        {
            get { return _StartYear; }
            set { _StartYear = value; }
        }
        [Column(Name = "EndYear", DataType = "Int32")]
        public Int32 EndYear
        {
            get { return _EndYear; }
            set { _EndYear = value; }
        }
        [Column(Name = "GCSchoolType", DataType = "String")]
        public String GCSchoolType
        {
            get { return _GCSchoolType; }
            set { _GCSchoolType = value; }
        }
        [Column(Name = "SchoolType", DataType = "String")]
        public String SchoolType
        {
            get { return _SchoolType; }
            set { _SchoolType = value; }
        }
        [Column(Name = "SchoolName", DataType = "String")]
        public String SchoolName
        {
            get { return _SchoolName; }
            set { _SchoolName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vSubjectBasicCompetency
    [Serializable]
    [Table(Name = "vSubjectBasicCompetency")]
    public class vSubjectBasicCompetency
    {
        private Int32 _SubjectBasicCompetencyID;
        private Int32 _SubjectCompetencyStandardID;
        private String _SubjectCompetencyStandardName;
        private String _SubjectBasicCompetencyName;
        private String _StudySource;
        private Boolean _IsDeleted;

        [Column(Name = "SubjectBasicCompetencyID", DataType = "Int32")]
        public Int32 SubjectBasicCompetencyID
        {
            get { return _SubjectBasicCompetencyID; }
            set { _SubjectBasicCompetencyID = value; }
        }
        [Column(Name = "SubjectCompetencyStandardID", DataType = "Int32")]
        public Int32 SubjectCompetencyStandardID
        {
            get { return _SubjectCompetencyStandardID; }
            set { _SubjectCompetencyStandardID = value; }
        }
        [Column(Name = "SubjectCompetencyStandardName", DataType = "String")]
        public String SubjectCompetencyStandardName
        {
            get { return _SubjectCompetencyStandardName; }
            set { _SubjectCompetencyStandardName = value; }
        }
        [Column(Name = "SubjectBasicCompetencyName", DataType = "String")]
        public String SubjectBasicCompetencyName
        {
            get { return _SubjectBasicCompetencyName; }
            set { _SubjectBasicCompetencyName = value; }
        }
        [Column(Name = "StudySource", DataType = "String")]
        public String StudySource
        {
            get { return _StudySource; }
            set { _StudySource = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vSubjectClassType
    [Serializable]
    [Table(Name = "vSubjectClassType")]
    public class vSubjectClassType
    {
        private Int32 _SubjectID;
        private String _SubjectCode;
        private String _SubjectName;
        private Int32 _ClassTypeID;
        private String _ClassTypeCode;
        private String _ClassTypeName;
        private String _GCGrade;
        private String _Grade;
        private String _GCMajor;
        private String _Major;
        private Boolean _IsDeleted;

        [Column(Name = "SubjectID", DataType = "Int32")]
        public Int32 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
        [Column(Name = "SubjectCode", DataType = "String")]
        public String SubjectCode
        {
            get { return _SubjectCode; }
            set { _SubjectCode = value; }
        }
        [Column(Name = "SubjectName", DataType = "String")]
        public String SubjectName
        {
            get { return _SubjectName; }
            set { _SubjectName = value; }
        }
        [Column(Name = "ClassTypeID", DataType = "Int32")]
        public Int32 ClassTypeID
        {
            get { return _ClassTypeID; }
            set { _ClassTypeID = value; }
        }
        [Column(Name = "ClassTypeCode", DataType = "String")]
        public String ClassTypeCode
        {
            get { return _ClassTypeCode; }
            set { _ClassTypeCode = value; }
        }
        [Column(Name = "ClassTypeName", DataType = "String")]
        public String ClassTypeName
        {
            get { return _ClassTypeName; }
            set { _ClassTypeName = value; }
        }
        [Column(Name = "GCGrade", DataType = "String")]
        public String GCGrade
        {
            get { return _GCGrade; }
            set { _GCGrade = value; }
        }
        [Column(Name = "Grade", DataType = "String")]
        public String Grade
        {
            get { return _Grade; }
            set { _Grade = value; }
        }
        [Column(Name = "GCMajor", DataType = "String")]
        public String GCMajor
        {
            get { return _GCMajor; }
            set { _GCMajor = value; }
        }
        [Column(Name = "Major", DataType = "String")]
        public String Major
        {
            get { return _Major; }
            set { _Major = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vSubjectCurriculumSyllabus
    [Serializable]
    [Table(Name = "vSubjectCurriculumSyllabus")]
    public class vSubjectCurriculumSyllabus
    {
        private Int32 _SubjectCurriculumSyllabusID;
        private Int32 _SubjectCurriculumID;
        private Int32 _SubjectID;
        private Int32 _CurriculumSyllabusID;
        private Boolean _IsHeader;
        private String _SubjectCurriculumSyllabusName;
        private Int32 _ReferenceID;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "SubjectCurriculumSyllabusID", DataType = "Int32")]
        public Int32 SubjectCurriculumSyllabusID
        {
            get { return _SubjectCurriculumSyllabusID; }
            set { _SubjectCurriculumSyllabusID = value; }
        }
        [Column(Name = "SubjectCurriculumID", DataType = "Int32")]
        public Int32 SubjectCurriculumID
        {
            get { return _SubjectCurriculumID; }
            set { _SubjectCurriculumID = value; }
        }
        [Column(Name = "SubjectID", DataType = "Int32")]
        public Int32 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
        [Column(Name = "CurriculumSyllabusID", DataType = "Int32")]
        public Int32 CurriculumSyllabusID
        {
            get { return _CurriculumSyllabusID; }
            set { _CurriculumSyllabusID = value; }
        }
        [Column(Name = "IsHeader", DataType = "Boolean")]
        public Boolean IsHeader
        {
            get { return _IsHeader; }
            set { _IsHeader = value; }
        }
        [Column(Name = "SubjectCurriculumSyllabusName", DataType = "String")]
        public String SubjectCurriculumSyllabusName
        {
            get { return _SubjectCurriculumSyllabusName; }
            set { _SubjectCurriculumSyllabusName = value; }
        }
        [Column(Name = "ReferenceID", DataType = "Int32")]
        public Int32 ReferenceID
        {
            get { return _ReferenceID; }
            set { _ReferenceID = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vSubjectIndicator
    [Serializable]
    [Table(Name = "vSubjectIndicator")]
    public partial class vSubjectIndicator
    {
        private Int32 _SubjectIndicatorID;
        private Int32 _SubjectBasicCompetencyID;
        private Int32 _SubjectMatterID;
        private String _SubjectBasicCompetencyName;
        private String _SubjectIndicatorName;
        private Int16 _DisplayOrder;
        private Boolean _IsDeleted;

        [Column(Name = "SubjectIndicatorID", DataType = "Int32")]
        public Int32 SubjectIndicatorID
        {
            get { return _SubjectIndicatorID; }
            set { _SubjectIndicatorID = value; }
        }
        [Column(Name = "SubjectBasicCompetencyID", DataType = "Int32")]
        public Int32 SubjectBasicCompetencyID
        {
            get { return _SubjectBasicCompetencyID; }
            set { _SubjectBasicCompetencyID = value; }
        }
        [Column(Name = "SubjectMatterID", DataType = "Int32")]
        public Int32 SubjectMatterID
        {
            get { return _SubjectMatterID; }
            set { _SubjectMatterID = value; }
        }
        [Column(Name = "SubjectBasicCompetencyName", DataType = "String")]
        public String SubjectBasicCompetencyName
        {
            get { return _SubjectBasicCompetencyName; }
            set { _SubjectBasicCompetencyName = value; }
        }
        [Column(Name = "SubjectIndicatorName", DataType = "String")]
        public String SubjectIndicatorName
        {
            get { return _SubjectIndicatorName; }
            set { _SubjectIndicatorName = value; }
        }
        [Column(Name = "DisplayOrder", DataType = "Int16")]
        public Int16 DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vSubjectMatterHd
    [Serializable]
    [Table(Name = "vSubjectMatterHd")]
    public class vSubjectMatterHd
    {
        private Int32 _SubjectMatterID;
        private String _SubjectMatterCode;
        private String _SubjectMatterName;
        private Int32 _SubjectID;
        private String _ListClassTypeID;
        private String _ListClassTypeName;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "SubjectMatterID", DataType = "Int32")]
        public Int32 SubjectMatterID
        {
            get { return _SubjectMatterID; }
            set { _SubjectMatterID = value; }
        }
        [Column(Name = "SubjectMatterCode", DataType = "String")]
        public String SubjectMatterCode
        {
            get { return _SubjectMatterCode; }
            set { _SubjectMatterCode = value; }
        }
        [Column(Name = "SubjectMatterName", DataType = "String")]
        public String SubjectMatterName
        {
            get { return _SubjectMatterName; }
            set { _SubjectMatterName = value; }
        }
        [Column(Name = "SubjectID", DataType = "Int32")]
        public Int32 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
        [Column(Name = "ListClassTypeID", DataType = "String")]
        public String ListClassTypeID
        {
            get { return _ListClassTypeID; }
            set { _ListClassTypeID = value; }
        }
        [Column(Name = "ListClassTypeName", DataType = "String")]
        public String ListClassTypeName
        {
            get { return _ListClassTypeName; }
            set { _ListClassTypeName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vSubjectMeetingPlanHd
    [Serializable]
    [Table(Name = "vSubjectMeetingPlanHd")]
    public class vSubjectMeetingPlanHd
    {
        private Int32 _SubjectMeetingPlanHdID;
        private Int32 _SubjectMatterID;
        private String _GCPeriodSection;
        private String _PeriodSection;
        private Int16 _MeetingNo;
        private Int32 _SubjectCompetencyStandardID;
        private String _SubjectCompetencyStandardName;
        private String _ListSubjectBasicCompetencyID;
        private String _ListSubjectBasicCompetencyName;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "SubjectMeetingPlanHdID", DataType = "Int32")]
        public Int32 SubjectMeetingPlanHdID
        {
            get { return _SubjectMeetingPlanHdID; }
            set { _SubjectMeetingPlanHdID = value; }
        }
        [Column(Name = "SubjectMatterID", DataType = "Int32")]
        public Int32 SubjectMatterID
        {
            get { return _SubjectMatterID; }
            set { _SubjectMatterID = value; }
        }
        [Column(Name = "GCPeriodSection", DataType = "String")]
        public String GCPeriodSection
        {
            get { return _GCPeriodSection; }
            set { _GCPeriodSection = value; }
        }
        [Column(Name = "PeriodSection", DataType = "String")]
        public String PeriodSection
        {
            get { return _PeriodSection; }
            set { _PeriodSection = value; }
        }
        [Column(Name = "MeetingNo", DataType = "Int16")]
        public Int16 MeetingNo
        {
            get { return _MeetingNo; }
            set { _MeetingNo = value; }
        }
        [Column(Name = "SubjectCompetencyStandardID", DataType = "Int32")]
        public Int32 SubjectCompetencyStandardID
        {
            get { return _SubjectCompetencyStandardID; }
            set { _SubjectCompetencyStandardID = value; }
        }
        [Column(Name = "SubjectCompetencyStandardName", DataType = "String")]
        public String SubjectCompetencyStandardName
        {
            get { return _SubjectCompetencyStandardName; }
            set { _SubjectCompetencyStandardName = value; }
        }
        [Column(Name = "ListSubjectBasicCompetencyID", DataType = "String")]
        public String ListSubjectBasicCompetencyID
        {
            get { return _ListSubjectBasicCompetencyID; }
            set { _ListSubjectBasicCompetencyID = value; }
        }
        [Column(Name = "ListSubjectBasicCompetencyName", DataType = "String")]
        public String ListSubjectBasicCompetencyName
        {
            get { return _ListSubjectBasicCompetencyName; }
            set { _ListSubjectBasicCompetencyName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vSubjectMeetingPlanIndicator
    [Serializable]
    [Table(Name = "vSubjectMeetingPlanIndicator")]
    public class vSubjectMeetingPlanIndicator
    {
        private Int32 _SubjectMeetingPlanID;
        private Int32 _SubjectIndicatorID;
        private String _SubjectIndicatorName;

        [Column(Name = "SubjectMeetingPlanID", DataType = "Int32")]
        public Int32 SubjectMeetingPlanID
        {
            get { return _SubjectMeetingPlanID; }
            set { _SubjectMeetingPlanID = value; }
        }
        [Column(Name = "SubjectIndicatorID", DataType = "Int32")]
        public Int32 SubjectIndicatorID
        {
            get { return _SubjectIndicatorID; }
            set { _SubjectIndicatorID = value; }
        }
        [Column(Name = "SubjectIndicatorName", DataType = "String")]
        public String SubjectIndicatorName
        {
            get { return _SubjectIndicatorName; }
            set { _SubjectIndicatorName = value; }
        }
    }
    #endregion
    #region vSubLedgerDt
    [Serializable]
    [Table(Name = "vSubLedgerDt")]
    public class vSubLedgerDt
    {
        private Int32 _SubLedgerDtID;
        private Int32 _SubLedgerID;
        private String _SubLedgerDtCode;
        private String _SubLedgerDtName;
        private String _SubLedgerCode;
        private String _SubLedgerName;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "SubLedgerDtID", DataType = "Int32")]
        public Int32 SubLedgerDtID
        {
            get { return _SubLedgerDtID; }
            set { _SubLedgerDtID = value; }
        }
        [Column(Name = "SubLedgerID", DataType = "Int32")]
        public Int32 SubLedgerID
        {
            get { return _SubLedgerID; }
            set { _SubLedgerID = value; }
        }
        [Column(Name = "SubLedgerDtCode", DataType = "String")]
        public String SubLedgerDtCode
        {
            get { return _SubLedgerDtCode; }
            set { _SubLedgerDtCode = value; }
        }
        [Column(Name = "SubLedgerDtName", DataType = "String")]
        public String SubLedgerDtName
        {
            get { return _SubLedgerDtName; }
            set { _SubLedgerDtName = value; }
        }
        [Column(Name = "SubLedgerCode", DataType = "String")]
        public String SubLedgerCode
        {
            get { return _SubLedgerCode; }
            set { _SubLedgerCode = value; }
        }
        [Column(Name = "SubLedgerName", DataType = "String")]
        public String SubLedgerName
        {
            get { return _SubLedgerName; }
            set { _SubLedgerName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vSubLedgerHd
    [Serializable]
    [Table(Name = "vSubLedgerHd")]
    public class vSubLedgerHd
    {
        private Int32 _SubLedgerID;
        private String _SubLedgerCode;
        private String _SubLedgerName;
        private Int32 _SubLedgerTypeID;
        private String _SubLedgerTypeCode;
        private String _SubLedgerTypeName;
        private String _MethodName;
        private String _FilterExpression;
        private String _IDFieldName;
        private String _CodeFieldName;
        private String _DisplayFieldName;
        private String _SearchDialogTypeName;
        private String _TableName;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "SubLedgerID", DataType = "Int32")]
        public Int32 SubLedgerID
        {
            get { return _SubLedgerID; }
            set { _SubLedgerID = value; }
        }
        [Column(Name = "SubLedgerCode", DataType = "String")]
        public String SubLedgerCode
        {
            get { return _SubLedgerCode; }
            set { _SubLedgerCode = value; }
        }
        [Column(Name = "SubLedgerName", DataType = "String")]
        public String SubLedgerName
        {
            get { return _SubLedgerName; }
            set { _SubLedgerName = value; }
        }
        [Column(Name = "SubLedgerTypeID", DataType = "Int32")]
        public Int32 SubLedgerTypeID
        {
            get { return _SubLedgerTypeID; }
            set { _SubLedgerTypeID = value; }
        }
        [Column(Name = "SubLedgerTypeCode", DataType = "String")]
        public String SubLedgerTypeCode
        {
            get { return _SubLedgerTypeCode; }
            set { _SubLedgerTypeCode = value; }
        }
        [Column(Name = "SubLedgerTypeName", DataType = "String")]
        public String SubLedgerTypeName
        {
            get { return _SubLedgerTypeName; }
            set { _SubLedgerTypeName = value; }
        }
        [Column(Name = "MethodName", DataType = "String")]
        public String MethodName
        {
            get { return _MethodName; }
            set { _MethodName = value; }
        }
        [Column(Name = "FilterExpression", DataType = "String")]
        public String FilterExpression
        {
            get { return _FilterExpression; }
            set { _FilterExpression = value; }
        }
        [Column(Name = "IDFieldName", DataType = "String")]
        public String IDFieldName
        {
            get { return _IDFieldName; }
            set { _IDFieldName = value; }
        }
        [Column(Name = "CodeFieldName", DataType = "String")]
        public String CodeFieldName
        {
            get { return _CodeFieldName; }
            set { _CodeFieldName = value; }
        }
        [Column(Name = "DisplayFieldName", DataType = "String")]
        public String DisplayFieldName
        {
            get { return _DisplayFieldName; }
            set { _DisplayFieldName = value; }
        }
        [Column(Name = "SearchDialogTypeName", DataType = "String")]
        public String SearchDialogTypeName
        {
            get { return _SearchDialogTypeName; }
            set { _SearchDialogTypeName = value; }
        }
        [Column(Name = "TableName", DataType = "String")]
        public String TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vSupplier
    [Serializable]
    [Table(Name = "vSupplier")]
    public partial class vSupplier
    {
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private String _GCSupplierType;
        private Int32 _SupplierLineID;
        private String _SupplierLineCode;
        private String _SupplierLineName;
        private String _ShortName;
        private Int32 _TermID;
        private String _ContactPerson;
        private Decimal _MaxPOAmount;
        private Decimal _MinPOAmount;
        private Int16 _LeadTime;
        private Boolean _IsLogisticSupplier;
        private Boolean _IsPharmacySupplier;
        private Boolean _IsPaymentHold;
        private String _AddressID;
        private String _PhoneNo1;
        private String _PhoneNo2;
        private String _StreetName;
        private String _County;
        private String _District;
        private String _City;
        private String _GCState;
        private String _State;

        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "GCSupplierType", DataType = "String")]
        public String GCSupplierType
        {
            get { return _GCSupplierType; }
            set { _GCSupplierType = value; }
        }
        [Column(Name = "SupplierLineID", DataType = "Int32")]
        public Int32 SupplierLineID
        {
            get { return _SupplierLineID; }
            set { _SupplierLineID = value; }
        }
        [Column(Name = "SupplierLineCode", DataType = "String")]
        public String SupplierLineCode
        {
            get { return _SupplierLineCode; }
            set { _SupplierLineCode = value; }
        }
        [Column(Name = "SupplierLineName", DataType = "String")]
        public String SupplierLineName
        {
            get { return _SupplierLineName; }
            set { _SupplierLineName = value; }
        }
        [Column(Name = "ShortName", DataType = "String")]
        public String ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }
        [Column(Name = "TermID", DataType = "Int32")]
        public Int32 TermID
        {
            get { return _TermID; }
            set { _TermID = value; }
        }
        [Column(Name = "ContactPerson", DataType = "String")]
        public String ContactPerson
        {
            get { return _ContactPerson; }
            set { _ContactPerson = value; }
        }
        [Column(Name = "MaxPOAmount", DataType = "Decimal")]
        public Decimal MaxPOAmount
        {
            get { return _MaxPOAmount; }
            set { _MaxPOAmount = value; }
        }
        [Column(Name = "MinPOAmount", DataType = "Decimal")]
        public Decimal MinPOAmount
        {
            get { return _MinPOAmount; }
            set { _MinPOAmount = value; }
        }
        [Column(Name = "LeadTime", DataType = "Int16")]
        public Int16 LeadTime
        {
            get { return _LeadTime; }
            set { _LeadTime = value; }
        }
        [Column(Name = "IsLogisticSupplier", DataType = "Boolean")]
        public Boolean IsLogisticSupplier
        {
            get { return _IsLogisticSupplier; }
            set { _IsLogisticSupplier = value; }
        }
        [Column(Name = "IsPharmacySupplier", DataType = "Boolean")]
        public Boolean IsPharmacySupplier
        {
            get { return _IsPharmacySupplier; }
            set { _IsPharmacySupplier = value; }
        }
        [Column(Name = "IsPaymentHold", DataType = "Boolean")]
        public Boolean IsPaymentHold
        {
            get { return _IsPaymentHold; }
            set { _IsPaymentHold = value; }
        }
        [Column(Name = "AddressID", DataType = "String")]
        public String AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
        }
        [Column(Name = "PhoneNo1", DataType = "String")]
        public String PhoneNo1
        {
            get { return _PhoneNo1; }
            set { _PhoneNo1 = value; }
        }
        [Column(Name = "PhoneNo2", DataType = "String")]
        public String PhoneNo2
        {
            get { return _PhoneNo2; }
            set { _PhoneNo2 = value; }
        }
        [Column(Name = "StreetName", DataType = "String")]
        public String StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }
        [Column(Name = "County", DataType = "String")]
        public String County
        {
            get { return _County; }
            set { _County = value; }
        }
        [Column(Name = "District", DataType = "String")]
        public String District
        {
            get { return _District; }
            set { _District = value; }
        }
        [Column(Name = "City", DataType = "String")]
        public String City
        {
            get { return _City; }
            set { _City = value; }
        }
        [Column(Name = "GCState", DataType = "String")]
        public String GCState
        {
            get { return _GCState; }
            set { _GCState = value; }
        }
        [Column(Name = "State", DataType = "String")]
        public String State
        {
            get { return _State; }
            set { _State = value; }
        }
    }
    #endregion
    #region vSupplierCreditNote
    [Serializable]
    [Table(Name = "vSupplierCreditNote")]
    public partial class vSupplierCreditNote
    {
        private Int32 _CreditNoteID;
        private String _CreditNoteNo;
        private DateTime _CreditNoteDate;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private Int32 _PurchaseReturnID;
        private String _PurchaseReturnNo;
        private String _GCCreditNoteType;
        private String _CreditNoteType;
        private Decimal _CNAmount;
        private Boolean _IsIncludeVAT;
        private Decimal _VATPercentage;
        private String _Remarks;
        private Int32 _PurchaseInvoiceID;
        private String _GCTransactionStatus;
        private String _TransactionStatusWatermark;

        [Column(Name = "CreditNoteID", DataType = "Int32")]
        public Int32 CreditNoteID
        {
            get { return _CreditNoteID; }
            set { _CreditNoteID = value; }
        }
        [Column(Name = "CreditNoteNo", DataType = "String")]
        public String CreditNoteNo
        {
            get { return _CreditNoteNo; }
            set { _CreditNoteNo = value; }
        }
        [Column(Name = "CreditNoteDate", DataType = "DateTime")]
        public DateTime CreditNoteDate
        {
            get { return _CreditNoteDate; }
            set { _CreditNoteDate = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "PurchaseReturnID", DataType = "Int32")]
        public Int32 PurchaseReturnID
        {
            get { return _PurchaseReturnID; }
            set { _PurchaseReturnID = value; }
        }
        [Column(Name = "PurchaseReturnNo", DataType = "String")]
        public String PurchaseReturnNo
        {
            get { return _PurchaseReturnNo; }
            set { _PurchaseReturnNo = value; }
        }
        [Column(Name = "GCCreditNoteType", DataType = "String")]
        public String GCCreditNoteType
        {
            get { return _GCCreditNoteType; }
            set { _GCCreditNoteType = value; }
        }
        [Column(Name = "CreditNoteType", DataType = "String")]
        public String CreditNoteType
        {
            get { return _CreditNoteType; }
            set { _CreditNoteType = value; }
        }
        [Column(Name = "CNAmount", DataType = "Decimal")]
        public Decimal CNAmount
        {
            get { return _CNAmount; }
            set { _CNAmount = value; }
        }
        [Column(Name = "IsIncludeVAT", DataType = "Boolean")]
        public Boolean IsIncludeVAT
        {
            get { return _IsIncludeVAT; }
            set { _IsIncludeVAT = value; }
        }
        [Column(Name = "VATPercentage", DataType = "Decimal")]
        public Decimal VATPercentage
        {
            get { return _VATPercentage; }
            set { _VATPercentage = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "PurchaseInvoiceID", DataType = "Int32")]
        public Int32 PurchaseInvoiceID
        {
            get { return _PurchaseInvoiceID; }
            set { _PurchaseInvoiceID = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
    }
    #endregion
    #region vSupplierItem
    [Serializable]
    [Table(Name = "vSupplierItem")]
    public class vSupplierItem
    {
        private Int32 _ID;
        private Int32 _BusinessPartnerID;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private String _ItemName2;
        private String _SupplierItemCode;
        private String _SupplierItemName;
        private Decimal _Price;
        private Decimal _DiscountPercentage;
        private Int16 _LeadTime;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemName2", DataType = "String")]
        public String ItemName2
        {
            get { return _ItemName2; }
            set { _ItemName2 = value; }
        }
        [Column(Name = "SupplierItemCode", DataType = "String")]
        public String SupplierItemCode
        {
            get { return _SupplierItemCode; }
            set { _SupplierItemCode = value; }
        }
        [Column(Name = "SupplierItemName", DataType = "String")]
        public String SupplierItemName
        {
            get { return _SupplierItemName; }
            set { _SupplierItemName = value; }
        }
        [Column(Name = "Price", DataType = "Decimal")]
        public Decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        [Column(Name = "DiscountPercentage", DataType = "Decimal")]
        public Decimal DiscountPercentage
        {
            get { return _DiscountPercentage; }
            set { _DiscountPercentage = value; }
        }
        [Column(Name = "LeadTime", DataType = "Int16")]
        public Int16 LeadTime
        {
            get { return _LeadTime; }
            set { _LeadTime = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vSupplierItemPlaning
    [Serializable]
    [Table(Name = "vSupplierItemPlaning")]
    public class vSupplierItemPlaning
    {
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private Boolean _IsDeleted;
        private String _GCItemType;
        private String _GCItemUnit;
        private Int32? _BusinessPartnerID;
        private String _BusinessPartnerName;
        private String _BusinessPartnerCode;
        private Decimal _UnitPrice;
        private Decimal _Discount;
        private String _SupplierItemCode;
        private String _SupplierItemName;

        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "GCItemType", DataType = "String")]
        public String GCItemType
        {
            get { return _GCItemType; }
            set { _GCItemType = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32? BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "UnitPrice", DataType = "Decimal")]
        public Decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        [Column(Name = "Discount", DataType = "Decimal")]
        public Decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }
        [Column(Name = "SupplierItemCode", DataType = "String")]
        public String SupplierItemCode
        {
            get { return _SupplierItemCode; }
            set { _SupplierItemCode = value; }
        }
        [Column(Name = "SupplierItemName", DataType = "String")]
        public String SupplierItemName
        {
            get { return _SupplierItemName; }
            set { _SupplierItemName = value; }
        }
    }
    #endregion
    #region vSupplierLineDt
    [Serializable]
    [Table(Name = "vSupplierLineDt")]
    public class vSupplierLineDt
    {
        private Int32 _SupplierLineID;
        private String _SiteID;
        private Int32 _AP;
        private String _APGLAccountNo;
        private String _APGLAccountName;
        private Int32 _APSubLedgerID;
        private String _APSearchDialogTypeName;
        private String _APIDFieldName;
        private String _APCodeFieldName;
        private String _APDisplayFieldName;
        private String _APMethodName;
        private String _APFilterExpression;
        private Int32 _APSubLedger;
        private String _APSubLedgerCode;
        private String _APSubLedgerName;
        private Int32 _APInProcess;
        private String _APInProcessGLAccountNo;
        private String _APInProcessGLAccountName;
        private Int32 _APInProcessSubLedgerID;
        private String _APInProcessSearchDialogTypeName;
        private String _APInProcessIDFieldName;
        private String _APInProcessCodeFieldName;
        private String _APInProcessDisplayFieldName;
        private String _APInProcessMethodName;
        private String _APInProcessFilterExpression;
        private Int32 _APInProcessSubLedger;
        private String _APInProcessSubLedgerCode;
        private String _APInProcessSubLedgerName;
        private Int32 _APDiscount;
        private String _APDiscountGLAccountNo;
        private String _APDiscountGLAccountName;
        private Int32 _APDiscountSubLedgerID;
        private String _APDiscountSearchDialogTypeName;
        private String _APDiscountIDFieldName;
        private String _APDiscountCodeFieldName;
        private String _APDiscountDisplayFieldName;
        private String _APDiscountMethodName;
        private String _APDiscountFilterExpression;
        private Int32 _APDiscountSubLedger;
        private String _APDiscountSubLedgerCode;
        private String _APDiscountSubLedgerName;
        private Int32 _APStamp;
        private String _APStampGLAccountNo;
        private String _APStampGLAccountName;
        private Int32 _APStampSubLedgerID;
        private String _APStampSearchDialogTypeName;
        private String _APStampIDFieldName;
        private String _APStampCodeFieldName;
        private String _APStampDisplayFieldName;
        private String _APStampMethodName;
        private String _APStampFilterExpression;
        private Int32 _APStampSubLedger;
        private String _APStampSubLedgerCode;
        private String _APStampSubLedgerName;
        private Int32 _APDownPayment;
        private String _APDownPaymentGLAccountNo;
        private String _APDownPaymentGLAccountName;
        private Int32 _APDownPaymentSubLedgerID;
        private String _APDownPaymentSearchDialogTypeName;
        private String _APDownPaymentIDFieldName;
        private String _APDownPaymentCodeFieldName;
        private String _APDownPaymentDisplayFieldName;
        private String _APDownPaymentMethodName;
        private String _APDownPaymentFilterExpression;
        private Int32 _APDownPaymentSubLedger;
        private String _APDownPaymentSubLedgerCode;
        private String _APDownPaymentSubLedgerName;
        private Int32 _APCharge;
        private String _APChargeGLAccountNo;
        private String _APChargeGLAccountName;
        private Int32 _APChargeSubLedgerID;
        private String _APChargeSearchDialogTypeName;
        private String _APChargeIDFieldName;
        private String _APChargeCodeFieldName;
        private String _APChargeDisplayFieldName;
        private String _APChargeMethodName;
        private String _APChargeFilterExpression;
        private Int32 _APChargeSubLedger;
        private String _APChargeSubLedgerCode;
        private String _APChargeSubLedgerName;
        private Int32 _ARPurchaseReturn;
        private String _ARPurchaseReturnGLAccountNo;
        private String _ARPurchaseReturnGLAccountName;
        private Int32 _ARPurchaseReturnSubLedgerID;
        private String _ARPurchaseReturnSearchDialogTypeName;
        private String _ARPurchaseReturnIDFieldName;
        private String _ARPurchaseReturnCodeFieldName;
        private String _ARPurchaseReturnDisplayFieldName;
        private String _ARPurchaseReturnMethodName;
        private String _ARPurchaseReturnFilterExpression;
        private Int32 _ARPurchaseReturnSubLedger;
        private String _ARPurchaseReturnSubLedgerCode;
        private String _ARPurchaseReturnSubLedgerName;
        private Int32 _ARCreditNote;
        private String _ARCreditNoteGLAccountNo;
        private String _ARCreditNoteGLAccountName;
        private Int32 _ARCreditNoteSubLedgerID;
        private String _ARCreditNoteSearchDialogTypeName;
        private String _ARCreditNoteIDFieldName;
        private String _ARCreditNoteCodeFieldName;
        private String _ARCreditNoteDisplayFieldName;
        private String _ARCreditNoteMethodName;
        private String _ARCreditNoteFilterExpression;
        private Int32 _ARCreditNoteSubLedger;
        private String _ARCreditNoteSubLedgerCode;
        private String _ARCreditNoteSubLedgerName;

        [Column(Name = "SupplierLineID", DataType = "Int32")]
        public Int32 SupplierLineID
        {
            get { return _SupplierLineID; }
            set { _SupplierLineID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "AP", DataType = "Int32")]
        public Int32 AP
        {
            get { return _AP; }
            set { _AP = value; }
        }
        [Column(Name = "APGLAccountNo", DataType = "String")]
        public String APGLAccountNo
        {
            get { return _APGLAccountNo; }
            set { _APGLAccountNo = value; }
        }
        [Column(Name = "APGLAccountName", DataType = "String")]
        public String APGLAccountName
        {
            get { return _APGLAccountName; }
            set { _APGLAccountName = value; }
        }
        [Column(Name = "APSubLedgerID", DataType = "Int32")]
        public Int32 APSubLedgerID
        {
            get { return _APSubLedgerID; }
            set { _APSubLedgerID = value; }
        }
        [Column(Name = "APSearchDialogTypeName", DataType = "String")]
        public String APSearchDialogTypeName
        {
            get { return _APSearchDialogTypeName; }
            set { _APSearchDialogTypeName = value; }
        }
        [Column(Name = "APIDFieldName", DataType = "String")]
        public String APIDFieldName
        {
            get { return _APIDFieldName; }
            set { _APIDFieldName = value; }
        }
        [Column(Name = "APCodeFieldName", DataType = "String")]
        public String APCodeFieldName
        {
            get { return _APCodeFieldName; }
            set { _APCodeFieldName = value; }
        }
        [Column(Name = "APDisplayFieldName", DataType = "String")]
        public String APDisplayFieldName
        {
            get { return _APDisplayFieldName; }
            set { _APDisplayFieldName = value; }
        }
        [Column(Name = "APMethodName", DataType = "String")]
        public String APMethodName
        {
            get { return _APMethodName; }
            set { _APMethodName = value; }
        }
        [Column(Name = "APFilterExpression", DataType = "String")]
        public String APFilterExpression
        {
            get { return _APFilterExpression; }
            set { _APFilterExpression = value; }
        }
        [Column(Name = "APSubLedger", DataType = "Int32")]
        public Int32 APSubLedger
        {
            get { return _APSubLedger; }
            set { _APSubLedger = value; }
        }
        [Column(Name = "APSubLedgerCode", DataType = "String")]
        public String APSubLedgerCode
        {
            get { return _APSubLedgerCode; }
            set { _APSubLedgerCode = value; }
        }
        [Column(Name = "APSubLedgerName", DataType = "String")]
        public String APSubLedgerName
        {
            get { return _APSubLedgerName; }
            set { _APSubLedgerName = value; }
        }
        [Column(Name = "APInProcess", DataType = "Int32")]
        public Int32 APInProcess
        {
            get { return _APInProcess; }
            set { _APInProcess = value; }
        }
        [Column(Name = "APInProcessGLAccountNo", DataType = "String")]
        public String APInProcessGLAccountNo
        {
            get { return _APInProcessGLAccountNo; }
            set { _APInProcessGLAccountNo = value; }
        }
        [Column(Name = "APInProcessGLAccountName", DataType = "String")]
        public String APInProcessGLAccountName
        {
            get { return _APInProcessGLAccountName; }
            set { _APInProcessGLAccountName = value; }
        }
        [Column(Name = "APInProcessSubLedgerID", DataType = "Int32")]
        public Int32 APInProcessSubLedgerID
        {
            get { return _APInProcessSubLedgerID; }
            set { _APInProcessSubLedgerID = value; }
        }
        [Column(Name = "APInProcessSearchDialogTypeName", DataType = "String")]
        public String APInProcessSearchDialogTypeName
        {
            get { return _APInProcessSearchDialogTypeName; }
            set { _APInProcessSearchDialogTypeName = value; }
        }
        [Column(Name = "APInProcessIDFieldName", DataType = "String")]
        public String APInProcessIDFieldName
        {
            get { return _APInProcessIDFieldName; }
            set { _APInProcessIDFieldName = value; }
        }
        [Column(Name = "APInProcessCodeFieldName", DataType = "String")]
        public String APInProcessCodeFieldName
        {
            get { return _APInProcessCodeFieldName; }
            set { _APInProcessCodeFieldName = value; }
        }
        [Column(Name = "APInProcessDisplayFieldName", DataType = "String")]
        public String APInProcessDisplayFieldName
        {
            get { return _APInProcessDisplayFieldName; }
            set { _APInProcessDisplayFieldName = value; }
        }
        [Column(Name = "APInProcessMethodName", DataType = "String")]
        public String APInProcessMethodName
        {
            get { return _APInProcessMethodName; }
            set { _APInProcessMethodName = value; }
        }
        [Column(Name = "APInProcessFilterExpression", DataType = "String")]
        public String APInProcessFilterExpression
        {
            get { return _APInProcessFilterExpression; }
            set { _APInProcessFilterExpression = value; }
        }
        [Column(Name = "APInProcessSubLedger", DataType = "Int32")]
        public Int32 APInProcessSubLedger
        {
            get { return _APInProcessSubLedger; }
            set { _APInProcessSubLedger = value; }
        }
        [Column(Name = "APInProcessSubLedgerCode", DataType = "String")]
        public String APInProcessSubLedgerCode
        {
            get { return _APInProcessSubLedgerCode; }
            set { _APInProcessSubLedgerCode = value; }
        }
        [Column(Name = "APInProcessSubLedgerName", DataType = "String")]
        public String APInProcessSubLedgerName
        {
            get { return _APInProcessSubLedgerName; }
            set { _APInProcessSubLedgerName = value; }
        }
        [Column(Name = "APDiscount", DataType = "Int32")]
        public Int32 APDiscount
        {
            get { return _APDiscount; }
            set { _APDiscount = value; }
        }
        [Column(Name = "APDiscountGLAccountNo", DataType = "String")]
        public String APDiscountGLAccountNo
        {
            get { return _APDiscountGLAccountNo; }
            set { _APDiscountGLAccountNo = value; }
        }
        [Column(Name = "APDiscountGLAccountName", DataType = "String")]
        public String APDiscountGLAccountName
        {
            get { return _APDiscountGLAccountName; }
            set { _APDiscountGLAccountName = value; }
        }
        [Column(Name = "APDiscountSubLedgerID", DataType = "Int32")]
        public Int32 APDiscountSubLedgerID
        {
            get { return _APDiscountSubLedgerID; }
            set { _APDiscountSubLedgerID = value; }
        }
        [Column(Name = "APDiscountSearchDialogTypeName", DataType = "String")]
        public String APDiscountSearchDialogTypeName
        {
            get { return _APDiscountSearchDialogTypeName; }
            set { _APDiscountSearchDialogTypeName = value; }
        }
        [Column(Name = "APDiscountIDFieldName", DataType = "String")]
        public String APDiscountIDFieldName
        {
            get { return _APDiscountIDFieldName; }
            set { _APDiscountIDFieldName = value; }
        }
        [Column(Name = "APDiscountCodeFieldName", DataType = "String")]
        public String APDiscountCodeFieldName
        {
            get { return _APDiscountCodeFieldName; }
            set { _APDiscountCodeFieldName = value; }
        }
        [Column(Name = "APDiscountDisplayFieldName", DataType = "String")]
        public String APDiscountDisplayFieldName
        {
            get { return _APDiscountDisplayFieldName; }
            set { _APDiscountDisplayFieldName = value; }
        }
        [Column(Name = "APDiscountMethodName", DataType = "String")]
        public String APDiscountMethodName
        {
            get { return _APDiscountMethodName; }
            set { _APDiscountMethodName = value; }
        }
        [Column(Name = "APDiscountFilterExpression", DataType = "String")]
        public String APDiscountFilterExpression
        {
            get { return _APDiscountFilterExpression; }
            set { _APDiscountFilterExpression = value; }
        }
        [Column(Name = "APDiscountSubLedger", DataType = "Int32")]
        public Int32 APDiscountSubLedger
        {
            get { return _APDiscountSubLedger; }
            set { _APDiscountSubLedger = value; }
        }
        [Column(Name = "APDiscountSubLedgerCode", DataType = "String")]
        public String APDiscountSubLedgerCode
        {
            get { return _APDiscountSubLedgerCode; }
            set { _APDiscountSubLedgerCode = value; }
        }
        [Column(Name = "APDiscountSubLedgerName", DataType = "String")]
        public String APDiscountSubLedgerName
        {
            get { return _APDiscountSubLedgerName; }
            set { _APDiscountSubLedgerName = value; }
        }
        [Column(Name = "APStamp", DataType = "Int32")]
        public Int32 APStamp
        {
            get { return _APStamp; }
            set { _APStamp = value; }
        }
        [Column(Name = "APStampGLAccountNo", DataType = "String")]
        public String APStampGLAccountNo
        {
            get { return _APStampGLAccountNo; }
            set { _APStampGLAccountNo = value; }
        }
        [Column(Name = "APStampGLAccountName", DataType = "String")]
        public String APStampGLAccountName
        {
            get { return _APStampGLAccountName; }
            set { _APStampGLAccountName = value; }
        }
        [Column(Name = "APStampSubLedgerID", DataType = "Int32")]
        public Int32 APStampSubLedgerID
        {
            get { return _APStampSubLedgerID; }
            set { _APStampSubLedgerID = value; }
        }
        [Column(Name = "APStampSearchDialogTypeName", DataType = "String")]
        public String APStampSearchDialogTypeName
        {
            get { return _APStampSearchDialogTypeName; }
            set { _APStampSearchDialogTypeName = value; }
        }
        [Column(Name = "APStampIDFieldName", DataType = "String")]
        public String APStampIDFieldName
        {
            get { return _APStampIDFieldName; }
            set { _APStampIDFieldName = value; }
        }
        [Column(Name = "APStampCodeFieldName", DataType = "String")]
        public String APStampCodeFieldName
        {
            get { return _APStampCodeFieldName; }
            set { _APStampCodeFieldName = value; }
        }
        [Column(Name = "APStampDisplayFieldName", DataType = "String")]
        public String APStampDisplayFieldName
        {
            get { return _APStampDisplayFieldName; }
            set { _APStampDisplayFieldName = value; }
        }
        [Column(Name = "APStampMethodName", DataType = "String")]
        public String APStampMethodName
        {
            get { return _APStampMethodName; }
            set { _APStampMethodName = value; }
        }
        [Column(Name = "APStampFilterExpression", DataType = "String")]
        public String APStampFilterExpression
        {
            get { return _APStampFilterExpression; }
            set { _APStampFilterExpression = value; }
        }
        [Column(Name = "APStampSubLedger", DataType = "Int32")]
        public Int32 APStampSubLedger
        {
            get { return _APStampSubLedger; }
            set { _APStampSubLedger = value; }
        }
        [Column(Name = "APStampSubLedgerCode", DataType = "String")]
        public String APStampSubLedgerCode
        {
            get { return _APStampSubLedgerCode; }
            set { _APStampSubLedgerCode = value; }
        }
        [Column(Name = "APStampSubLedgerName", DataType = "String")]
        public String APStampSubLedgerName
        {
            get { return _APStampSubLedgerName; }
            set { _APStampSubLedgerName = value; }
        }
        [Column(Name = "APDownPayment", DataType = "Int32")]
        public Int32 APDownPayment
        {
            get { return _APDownPayment; }
            set { _APDownPayment = value; }
        }
        [Column(Name = "APDownPaymentGLAccountNo", DataType = "String")]
        public String APDownPaymentGLAccountNo
        {
            get { return _APDownPaymentGLAccountNo; }
            set { _APDownPaymentGLAccountNo = value; }
        }
        [Column(Name = "APDownPaymentGLAccountName", DataType = "String")]
        public String APDownPaymentGLAccountName
        {
            get { return _APDownPaymentGLAccountName; }
            set { _APDownPaymentGLAccountName = value; }
        }
        [Column(Name = "APDownPaymentSubLedgerID", DataType = "Int32")]
        public Int32 APDownPaymentSubLedgerID
        {
            get { return _APDownPaymentSubLedgerID; }
            set { _APDownPaymentSubLedgerID = value; }
        }
        [Column(Name = "APDownPaymentSearchDialogTypeName", DataType = "String")]
        public String APDownPaymentSearchDialogTypeName
        {
            get { return _APDownPaymentSearchDialogTypeName; }
            set { _APDownPaymentSearchDialogTypeName = value; }
        }
        [Column(Name = "APDownPaymentIDFieldName", DataType = "String")]
        public String APDownPaymentIDFieldName
        {
            get { return _APDownPaymentIDFieldName; }
            set { _APDownPaymentIDFieldName = value; }
        }
        [Column(Name = "APDownPaymentCodeFieldName", DataType = "String")]
        public String APDownPaymentCodeFieldName
        {
            get { return _APDownPaymentCodeFieldName; }
            set { _APDownPaymentCodeFieldName = value; }
        }
        [Column(Name = "APDownPaymentDisplayFieldName", DataType = "String")]
        public String APDownPaymentDisplayFieldName
        {
            get { return _APDownPaymentDisplayFieldName; }
            set { _APDownPaymentDisplayFieldName = value; }
        }
        [Column(Name = "APDownPaymentMethodName", DataType = "String")]
        public String APDownPaymentMethodName
        {
            get { return _APDownPaymentMethodName; }
            set { _APDownPaymentMethodName = value; }
        }
        [Column(Name = "APDownPaymentFilterExpression", DataType = "String")]
        public String APDownPaymentFilterExpression
        {
            get { return _APDownPaymentFilterExpression; }
            set { _APDownPaymentFilterExpression = value; }
        }
        [Column(Name = "APDownPaymentSubLedger", DataType = "Int32")]
        public Int32 APDownPaymentSubLedger
        {
            get { return _APDownPaymentSubLedger; }
            set { _APDownPaymentSubLedger = value; }
        }
        [Column(Name = "APDownPaymentSubLedgerCode", DataType = "String")]
        public String APDownPaymentSubLedgerCode
        {
            get { return _APDownPaymentSubLedgerCode; }
            set { _APDownPaymentSubLedgerCode = value; }
        }
        [Column(Name = "APDownPaymentSubLedgerName", DataType = "String")]
        public String APDownPaymentSubLedgerName
        {
            get { return _APDownPaymentSubLedgerName; }
            set { _APDownPaymentSubLedgerName = value; }
        }
        [Column(Name = "APCharge", DataType = "Int32")]
        public Int32 APCharge
        {
            get { return _APCharge; }
            set { _APCharge = value; }
        }
        [Column(Name = "APChargeGLAccountNo", DataType = "String")]
        public String APChargeGLAccountNo
        {
            get { return _APChargeGLAccountNo; }
            set { _APChargeGLAccountNo = value; }
        }
        [Column(Name = "APChargeGLAccountName", DataType = "String")]
        public String APChargeGLAccountName
        {
            get { return _APChargeGLAccountName; }
            set { _APChargeGLAccountName = value; }
        }
        [Column(Name = "APChargeSubLedgerID", DataType = "Int32")]
        public Int32 APChargeSubLedgerID
        {
            get { return _APChargeSubLedgerID; }
            set { _APChargeSubLedgerID = value; }
        }
        [Column(Name = "APChargeSearchDialogTypeName", DataType = "String")]
        public String APChargeSearchDialogTypeName
        {
            get { return _APChargeSearchDialogTypeName; }
            set { _APChargeSearchDialogTypeName = value; }
        }
        [Column(Name = "APChargeIDFieldName", DataType = "String")]
        public String APChargeIDFieldName
        {
            get { return _APChargeIDFieldName; }
            set { _APChargeIDFieldName = value; }
        }
        [Column(Name = "APChargeCodeFieldName", DataType = "String")]
        public String APChargeCodeFieldName
        {
            get { return _APChargeCodeFieldName; }
            set { _APChargeCodeFieldName = value; }
        }
        [Column(Name = "APChargeDisplayFieldName", DataType = "String")]
        public String APChargeDisplayFieldName
        {
            get { return _APChargeDisplayFieldName; }
            set { _APChargeDisplayFieldName = value; }
        }
        [Column(Name = "APChargeMethodName", DataType = "String")]
        public String APChargeMethodName
        {
            get { return _APChargeMethodName; }
            set { _APChargeMethodName = value; }
        }
        [Column(Name = "APChargeFilterExpression", DataType = "String")]
        public String APChargeFilterExpression
        {
            get { return _APChargeFilterExpression; }
            set { _APChargeFilterExpression = value; }
        }
        [Column(Name = "APChargeSubLedger", DataType = "Int32")]
        public Int32 APChargeSubLedger
        {
            get { return _APChargeSubLedger; }
            set { _APChargeSubLedger = value; }
        }
        [Column(Name = "APChargeSubLedgerCode", DataType = "String")]
        public String APChargeSubLedgerCode
        {
            get { return _APChargeSubLedgerCode; }
            set { _APChargeSubLedgerCode = value; }
        }
        [Column(Name = "APChargeSubLedgerName", DataType = "String")]
        public String APChargeSubLedgerName
        {
            get { return _APChargeSubLedgerName; }
            set { _APChargeSubLedgerName = value; }
        }
        [Column(Name = "ARPurchaseReturn", DataType = "Int32")]
        public Int32 ARPurchaseReturn
        {
            get { return _ARPurchaseReturn; }
            set { _ARPurchaseReturn = value; }
        }
        [Column(Name = "ARPurchaseReturnGLAccountNo", DataType = "String")]
        public String ARPurchaseReturnGLAccountNo
        {
            get { return _ARPurchaseReturnGLAccountNo; }
            set { _ARPurchaseReturnGLAccountNo = value; }
        }
        [Column(Name = "ARPurchaseReturnGLAccountName", DataType = "String")]
        public String ARPurchaseReturnGLAccountName
        {
            get { return _ARPurchaseReturnGLAccountName; }
            set { _ARPurchaseReturnGLAccountName = value; }
        }
        [Column(Name = "ARPurchaseReturnSubLedgerID", DataType = "Int32")]
        public Int32 ARPurchaseReturnSubLedgerID
        {
            get { return _ARPurchaseReturnSubLedgerID; }
            set { _ARPurchaseReturnSubLedgerID = value; }
        }
        [Column(Name = "ARPurchaseReturnSearchDialogTypeName", DataType = "String")]
        public String ARPurchaseReturnSearchDialogTypeName
        {
            get { return _ARPurchaseReturnSearchDialogTypeName; }
            set { _ARPurchaseReturnSearchDialogTypeName = value; }
        }
        [Column(Name = "ARPurchaseReturnIDFieldName", DataType = "String")]
        public String ARPurchaseReturnIDFieldName
        {
            get { return _ARPurchaseReturnIDFieldName; }
            set { _ARPurchaseReturnIDFieldName = value; }
        }
        [Column(Name = "ARPurchaseReturnCodeFieldName", DataType = "String")]
        public String ARPurchaseReturnCodeFieldName
        {
            get { return _ARPurchaseReturnCodeFieldName; }
            set { _ARPurchaseReturnCodeFieldName = value; }
        }
        [Column(Name = "ARPurchaseReturnDisplayFieldName", DataType = "String")]
        public String ARPurchaseReturnDisplayFieldName
        {
            get { return _ARPurchaseReturnDisplayFieldName; }
            set { _ARPurchaseReturnDisplayFieldName = value; }
        }
        [Column(Name = "ARPurchaseReturnMethodName", DataType = "String")]
        public String ARPurchaseReturnMethodName
        {
            get { return _ARPurchaseReturnMethodName; }
            set { _ARPurchaseReturnMethodName = value; }
        }
        [Column(Name = "ARPurchaseReturnFilterExpression", DataType = "String")]
        public String ARPurchaseReturnFilterExpression
        {
            get { return _ARPurchaseReturnFilterExpression; }
            set { _ARPurchaseReturnFilterExpression = value; }
        }
        [Column(Name = "ARPurchaseReturnSubLedger", DataType = "Int32")]
        public Int32 ARPurchaseReturnSubLedger
        {
            get { return _ARPurchaseReturnSubLedger; }
            set { _ARPurchaseReturnSubLedger = value; }
        }
        [Column(Name = "ARPurchaseReturnSubLedgerCode", DataType = "String")]
        public String ARPurchaseReturnSubLedgerCode
        {
            get { return _ARPurchaseReturnSubLedgerCode; }
            set { _ARPurchaseReturnSubLedgerCode = value; }
        }
        [Column(Name = "ARPurchaseReturnSubLedgerName", DataType = "String")]
        public String ARPurchaseReturnSubLedgerName
        {
            get { return _ARPurchaseReturnSubLedgerName; }
            set { _ARPurchaseReturnSubLedgerName = value; }
        }
        [Column(Name = "ARCreditNote", DataType = "Int32")]
        public Int32 ARCreditNote
        {
            get { return _ARCreditNote; }
            set { _ARCreditNote = value; }
        }
        [Column(Name = "ARCreditNoteGLAccountNo", DataType = "String")]
        public String ARCreditNoteGLAccountNo
        {
            get { return _ARCreditNoteGLAccountNo; }
            set { _ARCreditNoteGLAccountNo = value; }
        }
        [Column(Name = "ARCreditNoteGLAccountName", DataType = "String")]
        public String ARCreditNoteGLAccountName
        {
            get { return _ARCreditNoteGLAccountName; }
            set { _ARCreditNoteGLAccountName = value; }
        }
        [Column(Name = "ARCreditNoteSubLedgerID", DataType = "Int32")]
        public Int32 ARCreditNoteSubLedgerID
        {
            get { return _ARCreditNoteSubLedgerID; }
            set { _ARCreditNoteSubLedgerID = value; }
        }
        [Column(Name = "ARCreditNoteSearchDialogTypeName", DataType = "String")]
        public String ARCreditNoteSearchDialogTypeName
        {
            get { return _ARCreditNoteSearchDialogTypeName; }
            set { _ARCreditNoteSearchDialogTypeName = value; }
        }
        [Column(Name = "ARCreditNoteIDFieldName", DataType = "String")]
        public String ARCreditNoteIDFieldName
        {
            get { return _ARCreditNoteIDFieldName; }
            set { _ARCreditNoteIDFieldName = value; }
        }
        [Column(Name = "ARCreditNoteCodeFieldName", DataType = "String")]
        public String ARCreditNoteCodeFieldName
        {
            get { return _ARCreditNoteCodeFieldName; }
            set { _ARCreditNoteCodeFieldName = value; }
        }
        [Column(Name = "ARCreditNoteDisplayFieldName", DataType = "String")]
        public String ARCreditNoteDisplayFieldName
        {
            get { return _ARCreditNoteDisplayFieldName; }
            set { _ARCreditNoteDisplayFieldName = value; }
        }
        [Column(Name = "ARCreditNoteMethodName", DataType = "String")]
        public String ARCreditNoteMethodName
        {
            get { return _ARCreditNoteMethodName; }
            set { _ARCreditNoteMethodName = value; }
        }
        [Column(Name = "ARCreditNoteFilterExpression", DataType = "String")]
        public String ARCreditNoteFilterExpression
        {
            get { return _ARCreditNoteFilterExpression; }
            set { _ARCreditNoteFilterExpression = value; }
        }
        [Column(Name = "ARCreditNoteSubLedger", DataType = "Int32")]
        public Int32 ARCreditNoteSubLedger
        {
            get { return _ARCreditNoteSubLedger; }
            set { _ARCreditNoteSubLedger = value; }
        }
        [Column(Name = "ARCreditNoteSubLedgerCode", DataType = "String")]
        public String ARCreditNoteSubLedgerCode
        {
            get { return _ARCreditNoteSubLedgerCode; }
            set { _ARCreditNoteSubLedgerCode = value; }
        }
        [Column(Name = "ARCreditNoteSubLedgerName", DataType = "String")]
        public String ARCreditNoteSubLedgerName
        {
            get { return _ARCreditNoteSubLedgerName; }
            set { _ARCreditNoteSubLedgerName = value; }
        }
    }
    #endregion
    #region vSupplierPaymentHd
    [Serializable]
    [Table(Name = "vSupplierPaymentHd")]
    public partial class vSupplierPaymentHd : DbDataModel
    {
        private Int32 _SupplierPaymentID;
        private String _SupplierPaymentNo;
        private DateTime _PaymentDate;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private String _ReferenceNo;
        private DateTime _ReferenceDate;
        private String _GCCurrencyCode;
        private String _CurrencyCode;
        private Decimal _CurrencyRate;
        private String _GCSupplierPaymentMethod;
        private String _PaymentMethod;
        private Int32? _BankID;
        private String _BankReferenceNo;
        private String _Remarks;
        private String _GCTransactionStatus;
        private String _TransactionStatusWatermark;

        [Column(Name = "SupplierPaymentID", DataType = "Int32")]
        public Int32 SupplierPaymentID
        {
            get { return _SupplierPaymentID; }
            set { _SupplierPaymentID = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "SupplierPaymentNo", DataType = "String")]
        public String SupplierPaymentNo
        {
            get { return _SupplierPaymentNo; }
            set { _SupplierPaymentNo = value; }
        }
        [Column(Name = "PaymentDate", DataType = "DateTime")]
        public DateTime PaymentDate
        {
            get { return _PaymentDate; }
            set { _PaymentDate = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String", IsNullable = true)]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "ReferenceDate", DataType = "DateTime", IsNullable = true)]
        public DateTime ReferenceDate
        {
            get { return _ReferenceDate; }
            set { _ReferenceDate = value; }
        }
        [Column(Name = "GCCurrencyCode", DataType = "String")]
        public String GCCurrencyCode
        {
            get { return _GCCurrencyCode; }
            set { _GCCurrencyCode = value; }
        }
        [Column(Name = "CurrencyCode", DataType = "String")]
        public String CurrencyCode
        {
            get { return _CurrencyCode; }
            set { _CurrencyCode = value; }
        }
        [Column(Name = "CurrencyRate", DataType = "Decimal")]
        public Decimal CurrencyRate
        {
            get { return _CurrencyRate; }
            set { _CurrencyRate = value; }
        }
        [Column(Name = "GCSupplierPaymentMethod", DataType = "String")]
        public String GCSupplierPaymentMethod
        {
            get { return _GCSupplierPaymentMethod; }
            set { _GCSupplierPaymentMethod = value; }
        }
        [Column(Name = "PaymentMethod", DataType = "String")]
        public String PaymentMethod
        {
            get { return _PaymentMethod; }
            set { _PaymentMethod = value; }
        }
        [Column(Name = "BankID", DataType = "Int32", IsNullable = true)]
        public Int32? BankID
        {
            get { return _BankID; }
            set { _BankID = value; }
        }
        [Column(Name = "BankReferenceNo", DataType = "String", IsNullable = true)]
        public String BankReferenceNo
        {
            get { return _BankReferenceNo; }
            set { _BankReferenceNo = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
    }
    #endregion
    #region vSyncItemTransactionDt
    [Serializable]
    [Table(Name = "vSyncItemTransactionDt")]
    public class vSyncItemTransactionDt
    {
        private Int32 _ID;
        private Int32 _TransactionID;
        private Int32 _ItemID;
        private Decimal _Quantity;
        private String _GCItemUnit;
        private String _GCBaseUnit;
        private Decimal _ConversionFactor;
        private Decimal _BaseQuantity;
        private Decimal _CostAmount;
        private Boolean _IsControlExpired;
        private String _GCAdjustmentReason;
        private String _Remarks;
        private String _GCItemDetailStatus;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "TransactionID", DataType = "Int32")]
        public Int32 TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "BaseQuantity", DataType = "Decimal")]
        public Decimal BaseQuantity
        {
            get { return _BaseQuantity; }
            set { _BaseQuantity = value; }
        }
        [Column(Name = "CostAmount", DataType = "Decimal")]
        public Decimal CostAmount
        {
            get { return _CostAmount; }
            set { _CostAmount = value; }
        }
        [Column(Name = "IsControlExpired", DataType = "Boolean")]
        public Boolean IsControlExpired
        {
            get { return _IsControlExpired; }
            set { _IsControlExpired = value; }
        }
        [Column(Name = "GCAdjustmentReason", DataType = "String", IsNullable = true)]
        public String GCAdjustmentReason
        {
            get { return _GCAdjustmentReason; }
            set { _GCAdjustmentReason = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }
    #endregion
    #region vSyncItemTransactionHd
    [Serializable]
    [Table(Name = "vSyncItemTransactionHd")]
    public class vSyncItemTransactionHd
    {
        private Int32 _TransactionID;
        private String _TransactionCode;
        private DateTime _TransactionDate;
        private String _TransactionNo;
        private Int32 _FromLocationID;
        private Int32? _ToLocationID;
        private String _GCAdjustmentType;
        private String _GCConsumptionType;
        private Boolean _IsBySystem;
        private String _ReferenceNo;
        private String _Remarks;
        private String _GCTransactionStatus;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "TransactionID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }
        [Column(Name = "TransactionCode", DataType = "String")]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
        [Column(Name = "TransactionDate", DataType = "DateTime")]
        public DateTime TransactionDate
        {
            get { return _TransactionDate; }
            set { _TransactionDate = value; }
        }
        [Column(Name = "TransactionNo", DataType = "String")]
        public String TransactionNo
        {
            get { return _TransactionNo; }
            set { _TransactionNo = value; }
        }
        [Column(Name = "FromLocationID", DataType = "Int32")]
        public Int32 FromLocationID
        {
            get { return _FromLocationID; }
            set { _FromLocationID = value; }
        }
        [Column(Name = "ToLocationID", DataType = "Int32", IsNullable = true)]
        public Int32? ToLocationID
        {
            get { return _ToLocationID; }
            set { _ToLocationID = value; }
        }
        [Column(Name = "GCAdjustmentType", DataType = "String", IsNullable = true)]
        public String GCAdjustmentType
        {
            get { return _GCAdjustmentType; }
            set { _GCAdjustmentType = value; }
        }
        [Column(Name = "GCConsumptionType", DataType = "String", IsNullable = true)]
        public String GCConsumptionType
        {
            get { return _GCConsumptionType; }
            set { _GCConsumptionType = value; }
        }
        [Column(Name = "IsBySystem", DataType = "Boolean")]
        public Boolean IsBySystem
        {
            get { return _IsBySystem; }
            set { _IsBySystem = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String", IsNullable = true)]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }
    #endregion
    #region vTariffBookDt
    [Serializable]
    [Table(Name = "vTariffBookDt")]
    public class vTariffBookDt
    {
        private Int32 _BookID;
        private String _GCTariffScheme;
        private String _GCItemType;
        private Int32 _ItemID;
        private Int32 _ItemGroupID;
        private Decimal _SuggestedTariff;
        private Decimal _BaseTariff;
        private Decimal _ApprovedBaseTariff;
        private Decimal _ProposedTariff;
        private Decimal _ApprovedTariff;
        private Boolean _IsApproved;
        private String _Notes;

        [Column(Name = "BookID", DataType = "Int32")]
        public Int32 BookID
        {
            get { return _BookID; }
            set { _BookID = value; }
        }
        [Column(Name = "GCTariffScheme", DataType = "String")]
        public String GCTariffScheme
        {
            get { return _GCTariffScheme; }
            set { _GCTariffScheme = value; }
        }
        [Column(Name = "GCItemType", DataType = "String")]
        public String GCItemType
        {
            get { return _GCItemType; }
            set { _GCItemType = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "SuggestedTariff", DataType = "Decimal")]
        public Decimal SuggestedTariff
        {
            get { return _SuggestedTariff; }
            set { _SuggestedTariff = value; }
        }
        [Column(Name = "BaseTariff", DataType = "Decimal")]
        public Decimal BaseTariff
        {
            get { return _BaseTariff; }
            set { _BaseTariff = value; }
        }
        [Column(Name = "ApprovedBaseTariff", DataType = "Decimal")]
        public Decimal ApprovedBaseTariff
        {
            get { return _ApprovedBaseTariff; }
            set { _ApprovedBaseTariff = value; }
        }
        [Column(Name = "ProposedTariff", DataType = "Decimal")]
        public Decimal ProposedTariff
        {
            get { return _ProposedTariff; }
            set { _ProposedTariff = value; }
        }
        [Column(Name = "ApprovedTariff", DataType = "Decimal")]
        public Decimal ApprovedTariff
        {
            get { return _ApprovedTariff; }
            set { _ApprovedTariff = value; }
        }
        [Column(Name = "IsApproved", DataType = "Boolean")]
        public Boolean IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }
        [Column(Name = "Notes", DataType = "String")]
        public String Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
        }
    }
    #endregion
    #region vTariffBookHd
    [Serializable]
    [Table(Name = "vTariffBookHd")]
    public partial class vTariffBookHd
    {
        private Int32 _BookID;
        private String _SiteID;
        private String _SiteName;
        private String _DocumentNo;
        private Int16 _RevisionNo;
        private DateTime _DocumentDate;
        private String _GCTariffScheme;
        private String _TariffScheme;
        private String _GCTransactionStatus;
        private String _TransactionStatus;
        private String _GCItemType;
        private String _ItemType;
        private Int32 _PreparedBy;
        private DateTime _ApprovedDate;
        private Int32 _ApprovedBy;
        private DateTime _StartingDate;
        private Boolean _IsIncludeVAT;
        private String _DocumentSummary;
        private String _Notes;
        private Int32 _NumberOfItems;
        private Int32 _NumberOfApprovedItems;
        private Boolean _IsDeleted;

        [Column(Name = "BookID", DataType = "Int32")]
        public Int32 BookID
        {
            get { return _BookID; }
            set { _BookID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "SiteName", DataType = "String")]
        public String SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }
        [Column(Name = "DocumentNo", DataType = "String")]
        public String DocumentNo
        {
            get { return _DocumentNo; }
            set { _DocumentNo = value; }
        }
        [Column(Name = "RevisionNo", DataType = "Int16")]
        public Int16 RevisionNo
        {
            get { return _RevisionNo; }
            set { _RevisionNo = value; }
        }
        [Column(Name = "DocumentDate", DataType = "DateTime")]
        public DateTime DocumentDate
        {
            get { return _DocumentDate; }
            set { _DocumentDate = value; }
        }
        [Column(Name = "GCTariffScheme", DataType = "String")]
        public String GCTariffScheme
        {
            get { return _GCTariffScheme; }
            set { _GCTariffScheme = value; }
        }
        [Column(Name = "TariffScheme", DataType = "String")]
        public String TariffScheme
        {
            get { return _TariffScheme; }
            set { _TariffScheme = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatus", DataType = "String")]
        public String TransactionStatus
        {
            get { return _TransactionStatus; }
            set { _TransactionStatus = value; }
        }
        [Column(Name = "GCItemType", DataType = "String")]
        public String GCItemType
        {
            get { return _GCItemType; }
            set { _GCItemType = value; }
        }
        [Column(Name = "ItemType", DataType = "String")]
        public String ItemType
        {
            get { return _ItemType; }
            set { _ItemType = value; }
        }
        [Column(Name = "PreparedBy", DataType = "Int32")]
        public Int32 PreparedBy
        {
            get { return _PreparedBy; }
            set { _PreparedBy = value; }
        }
        [Column(Name = "ApprovedDate", DataType = "DateTime")]
        public DateTime ApprovedDate
        {
            get { return _ApprovedDate; }
            set { _ApprovedDate = value; }
        }
        [Column(Name = "ApprovedBy", DataType = "Int32")]
        public Int32 ApprovedBy
        {
            get { return _ApprovedBy; }
            set { _ApprovedBy = value; }
        }
        [Column(Name = "StartingDate", DataType = "DateTime")]
        public DateTime StartingDate
        {
            get { return _StartingDate; }
            set { _StartingDate = value; }
        }
        [Column(Name = "IsIncludeVAT", DataType = "Boolean")]
        public Boolean IsIncludeVAT
        {
            get { return _IsIncludeVAT; }
            set { _IsIncludeVAT = value; }
        }
        [Column(Name = "DocumentSummary", DataType = "String")]
        public String DocumentSummary
        {
            get { return _DocumentSummary; }
            set { _DocumentSummary = value; }
        }
        [Column(Name = "Notes", DataType = "String")]
        public String Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
        }
        [Column(Name = "NumberOfItems", DataType = "Int32")]
        public Int32 NumberOfItems
        {
            get { return _NumberOfItems; }
            set { _NumberOfItems = value; }
        }
        [Column(Name = "NumberOfApprovedItems", DataType = "Int32")]
        public Int32 NumberOfApprovedItems
        {
            get { return _NumberOfApprovedItems; }
            set { _NumberOfApprovedItems = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vTeacher
    [Serializable]
    [Table(Name = "vTeacher")]
    public partial class vTeacher
    {
        private Int32 _TeacherID;
        private String _TeacherCode;
        private String _SiteID;
        private String _GCSalutation;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _TeacherName;
        private String _Initial;
        private String _GCGender;
        private String _GCTitle;
        private String _GCSuffix;
        private String _GCEmployeeType;
        private String _CityOfBirth;
        private DateTime _DateOfBirth;
        private String _GCDepartment;
        private String _GCOccupation;
        private String _GCOccupationLevel;
        private String _AddressID;
        private String _StreetName;
        private String _District;
        private String _City;
        private String _County;
        private String _GCState;
        private String _State;
        private Int32 _ZipCodeID;
        private String _ZipCode;
        private String _PhoneNo1;
        private String _PhoneNo2;
        private String _EmailAddress1;
        private String _EmailAddress2;
        private String _MobilePhoneNo1;
        private String _MobilePhoneNo2;
        private String _OfficeExtensionNo;
        private String _VATRegistrationNo;
        private DateTime _HiredDate;
        private DateTime _TerminatedDate;
        private String _PictureFileName;
        private String _GCEmployeeStatus;
        private Int32 _RoomID;
        private String _RoomCode;
        private String _RoomName;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
        }
        [Column(Name = "TeacherCode", DataType = "String")]
        public String TeacherCode
        {
            get { return _TeacherCode; }
            set { _TeacherCode = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "GCSalutation", DataType = "String")]
        public String GCSalutation
        {
            get { return _GCSalutation; }
            set { _GCSalutation = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "TeacherName", DataType = "String")]
        public String TeacherName
        {
            get { return _TeacherName; }
            set { _TeacherName = value; }
        }
        [Column(Name = "Initial", DataType = "String")]
        public String Initial
        {
            get { return _Initial; }
            set { _Initial = value; }
        }
        [Column(Name = "GCGender", DataType = "String")]
        public String GCGender
        {
            get { return _GCGender; }
            set { _GCGender = value; }
        }
        [Column(Name = "GCTitle", DataType = "String")]
        public String GCTitle
        {
            get { return _GCTitle; }
            set { _GCTitle = value; }
        }
        [Column(Name = "GCSuffix", DataType = "String")]
        public String GCSuffix
        {
            get { return _GCSuffix; }
            set { _GCSuffix = value; }
        }
        [Column(Name = "GCEmployeeType", DataType = "String")]
        public String GCEmployeeType
        {
            get { return _GCEmployeeType; }
            set { _GCEmployeeType = value; }
        }
        [Column(Name = "CityOfBirth", DataType = "String")]
        public String CityOfBirth
        {
            get { return _CityOfBirth; }
            set { _CityOfBirth = value; }
        }
        [Column(Name = "DateOfBirth", DataType = "DateTime")]
        public DateTime DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }
        [Column(Name = "GCDepartment", DataType = "String")]
        public String GCDepartment
        {
            get { return _GCDepartment; }
            set { _GCDepartment = value; }
        }
        [Column(Name = "GCOccupation", DataType = "String")]
        public String GCOccupation
        {
            get { return _GCOccupation; }
            set { _GCOccupation = value; }
        }
        [Column(Name = "GCOccupationLevel", DataType = "String")]
        public String GCOccupationLevel
        {
            get { return _GCOccupationLevel; }
            set { _GCOccupationLevel = value; }
        }
        [Column(Name = "AddressID", DataType = "String")]
        public String AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
        }
        [Column(Name = "StreetName", DataType = "String")]
        public String StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }
        [Column(Name = "District", DataType = "String")]
        public String District
        {
            get { return _District; }
            set { _District = value; }
        }
        [Column(Name = "City", DataType = "String")]
        public String City
        {
            get { return _City; }
            set { _City = value; }
        }
        [Column(Name = "County", DataType = "String")]
        public String County
        {
            get { return _County; }
            set { _County = value; }
        }
        [Column(Name = "GCState", DataType = "String")]
        public String GCState
        {
            get { return _GCState; }
            set { _GCState = value; }
        }
        [Column(Name = "State", DataType = "String")]
        public String State
        {
            get { return _State; }
            set { _State = value; }
        }
        [Column(Name = "ZipCodeID", DataType = "Int32")]
        public Int32 ZipCodeID
        {
            get { return _ZipCodeID; }
            set { _ZipCodeID = value; }
        }
        [Column(Name = "ZipCode", DataType = "String")]
        public String ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }
        [Column(Name = "PhoneNo1", DataType = "String")]
        public String PhoneNo1
        {
            get { return _PhoneNo1; }
            set { _PhoneNo1 = value; }
        }
        [Column(Name = "PhoneNo2", DataType = "String")]
        public String PhoneNo2
        {
            get { return _PhoneNo2; }
            set { _PhoneNo2 = value; }
        }
        [Column(Name = "EmailAddress1", DataType = "String")]
        public String EmailAddress1
        {
            get { return _EmailAddress1; }
            set { _EmailAddress1 = value; }
        }
        [Column(Name = "EmailAddress2", DataType = "String")]
        public String EmailAddress2
        {
            get { return _EmailAddress2; }
            set { _EmailAddress2 = value; }
        }
        [Column(Name = "MobilePhoneNo1", DataType = "String")]
        public String MobilePhoneNo1
        {
            get { return _MobilePhoneNo1; }
            set { _MobilePhoneNo1 = value; }
        }
        [Column(Name = "MobilePhoneNo2", DataType = "String")]
        public String MobilePhoneNo2
        {
            get { return _MobilePhoneNo2; }
            set { _MobilePhoneNo2 = value; }
        }
        [Column(Name = "OfficeExtensionNo", DataType = "String")]
        public String OfficeExtensionNo
        {
            get { return _OfficeExtensionNo; }
            set { _OfficeExtensionNo = value; }
        }
        [Column(Name = "VATRegistrationNo", DataType = "String")]
        public String VATRegistrationNo
        {
            get { return _VATRegistrationNo; }
            set { _VATRegistrationNo = value; }
        }
        [Column(Name = "HiredDate", DataType = "DateTime")]
        public DateTime HiredDate
        {
            get { return _HiredDate; }
            set { _HiredDate = value; }
        }
        [Column(Name = "TerminatedDate", DataType = "DateTime")]
        public DateTime TerminatedDate
        {
            get { return _TerminatedDate; }
            set { _TerminatedDate = value; }
        }
        [Column(Name = "PictureFileName", DataType = "String")]
        public String PictureFileName
        {
            get { return _PictureFileName; }
            set { _PictureFileName = value; }
        }
        [Column(Name = "GCEmployeeStatus", DataType = "String")]
        public String GCEmployeeStatus
        {
            get { return _GCEmployeeStatus; }
            set { _GCEmployeeStatus = value; }
        }
        [Column(Name = "RoomID", DataType = "Int32")]
        public Int32 RoomID
        {
            get { return _RoomID; }
            set { _RoomID = value; }
        }
        [Column(Name = "RoomCode", DataType = "String")]
        public String RoomCode
        {
            get { return _RoomCode; }
            set { _RoomCode = value; }
        }
        [Column(Name = "RoomName", DataType = "String")]
        public String RoomName
        {
            get { return _RoomName; }
            set { _RoomName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vTeacherAbsence
    [Serializable]
    [Table(Name = "vTeacherAbsence")]
    public partial class vTeacherAbsence
    {
        private Int32 _TeacherAbsenceID;
        private Int32 _TeacherID;
        private String _TeacherCode;
        private String _TeacherName;
        private Int32 _SchoolPeriodID;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private String _StartTime;
        private String _EndTime;
        private Boolean _IsFullDay;
        private String _GCAbsenceReason;
        private String _AbsenceReason;
        private String _OtherAbsenceReason;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "TeacherAbsenceID", DataType = "Int32")]
        public Int32 TeacherAbsenceID
        {
            get { return _TeacherAbsenceID; }
            set { _TeacherAbsenceID = value; }
        }
        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
        }
        [Column(Name = "TeacherCode", DataType = "String")]
        public String TeacherCode
        {
            get { return _TeacherCode; }
            set { _TeacherCode = value; }
        }
        [Column(Name = "TeacherName", DataType = "String")]
        public String TeacherName
        {
            get { return _TeacherName; }
            set { _TeacherName = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "StartDate", DataType = "DateTime")]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Column(Name = "EndDate", DataType = "DateTime")]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        [Column(Name = "StartTime", DataType = "String")]
        public String StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        [Column(Name = "EndTime", DataType = "String")]
        public String EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        [Column(Name = "IsFullDay", DataType = "Boolean")]
        public Boolean IsFullDay
        {
            get { return _IsFullDay; }
            set { _IsFullDay = value; }
        }
        [Column(Name = "GCAbsenceReason", DataType = "String")]
        public String GCAbsenceReason
        {
            get { return _GCAbsenceReason; }
            set { _GCAbsenceReason = value; }
        }
        [Column(Name = "AbsenceReason", DataType = "String")]
        public String AbsenceReason
        {
            get { return _AbsenceReason; }
            set { _AbsenceReason = value; }
        }
        [Column(Name = "OtherAbsenceReason", DataType = "String")]
        public String OtherAbsenceReason
        {
            get { return _OtherAbsenceReason; }
            set { _OtherAbsenceReason = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vTeacherClassSubject
    [Serializable]
    [Table(Name = "vTeacherClassSubject")]
    public class vTeacherClassSubject
    {
        private Int32 _ClassSubjectID;
        private Int32 _SchoolPeriodID;
        private Int32 _TeacherID;
        private Int32 _SchoolClassID;
        private String _SchoolClassName;
        private Int32 _SubjectID;
        private String _SubjectName;
        private Int16 _NoMeetingHoursInWeek;

        [Column(Name = "ClassSubjectID", DataType = "Int32")]
        public Int32 ClassSubjectID
        {
            get { return _ClassSubjectID; }
            set { _ClassSubjectID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
        }
        [Column(Name = "SchoolClassID", DataType = "Int32")]
        public Int32 SchoolClassID
        {
            get { return _SchoolClassID; }
            set { _SchoolClassID = value; }
        }
        [Column(Name = "SchoolClassName", DataType = "String")]
        public String SchoolClassName
        {
            get { return _SchoolClassName; }
            set { _SchoolClassName = value; }
        }
        [Column(Name = "SubjectID", DataType = "Int32")]
        public Int32 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
        [Column(Name = "SubjectName", DataType = "String")]
        public String SubjectName
        {
            get { return _SubjectName; }
            set { _SubjectName = value; }
        }
        [Column(Name = "NoMeetingHoursInWeek", DataType = "Int16")]
        public Int16 NoMeetingHoursInWeek
        {
            get { return _NoMeetingHoursInWeek; }
            set { _NoMeetingHoursInWeek = value; }
        }
    }
    #endregion
    #region vTeacherMark
    [Serializable]
    [Table(Name = "vTeacherMark")]
    public partial class vTeacherMark
    {
        private Int32 _TeacherMarkID;
        private Int32 _SchoolPeriodID;
        private String _SchoolPeriodName;
        private Int32 _PeriodSectionID;
        private String _PeriodSectionName;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private String _PeriodNo;
        private Int32 _FinalMark;
        private String _FinalMarkInString;
        private Boolean _IsDeleted;

        [Column(Name = "TeacherMarkID", DataType = "Int32")]
        public Int32 TeacherMarkID
        {
            get { return _TeacherMarkID; }
            set { _TeacherMarkID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "SchoolPeriodName", DataType = "String")]
        public String SchoolPeriodName
        {
            get { return _SchoolPeriodName; }
            set { _SchoolPeriodName = value; }
        }
        [Column(Name = "PeriodSectionID", DataType = "Int32")]
        public Int32 PeriodSectionID
        {
            get { return _PeriodSectionID; }
            set { _PeriodSectionID = value; }
        }
        [Column(Name = "PeriodSectionName", DataType = "String")]
        public String PeriodSectionName
        {
            get { return _PeriodSectionName; }
            set { _PeriodSectionName = value; }
        }
        [Column(Name = "StartDate", DataType = "DateTime")]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Column(Name = "EndDate", DataType = "DateTime")]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        [Column(Name = "PeriodNo", DataType = "String")]
        public String PeriodNo
        {
            get { return _PeriodNo; }
            set { _PeriodNo = value; }
        }
        [Column(Name = "FinalMark", DataType = "Int32")]
        public Int32 FinalMark
        {
            get { return _FinalMark; }
            set { _FinalMark = value; }
        }
        [Column(Name = "FinalMarkInString", DataType = "String")]
        public String FinalMarkInString
        {
            get { return _FinalMarkInString; }
            set { _FinalMarkInString = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vTeacherMarkGroup
    [Serializable]
    [Table(Name = "vTeacherMarkGroup")]
    public class vTeacherMarkGroup
    {
        private Int32 _TeacherMarkGroupID;
        private Int32 _TeacherMarkID;
        private Int32 _SchoolPeriodID;
        private String _PeriodNo;
        private Int32 _TeacherMarkTypeGroupID;
        private String _TeacherMarkTypeGroupName;
        private Int32 _FinalMarkPercentage;
        private Int32 _Mark;
        private String _MarkInString;
        private Boolean _IsDeleted;

        [Column(Name = "TeacherMarkGroupID", DataType = "Int32")]
        public Int32 TeacherMarkGroupID
        {
            get { return _TeacherMarkGroupID; }
            set { _TeacherMarkGroupID = value; }
        }
        [Column(Name = "TeacherMarkID", DataType = "Int32")]
        public Int32 TeacherMarkID
        {
            get { return _TeacherMarkID; }
            set { _TeacherMarkID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "PeriodNo", DataType = "String")]
        public String PeriodNo
        {
            get { return _PeriodNo; }
            set { _PeriodNo = value; }
        }
        [Column(Name = "TeacherMarkTypeGroupID", DataType = "Int32")]
        public Int32 TeacherMarkTypeGroupID
        {
            get { return _TeacherMarkTypeGroupID; }
            set { _TeacherMarkTypeGroupID = value; }
        }
        [Column(Name = "TeacherMarkTypeGroupName", DataType = "String")]
        public String TeacherMarkTypeGroupName
        {
            get { return _TeacherMarkTypeGroupName; }
            set { _TeacherMarkTypeGroupName = value; }
        }
        [Column(Name = "FinalMarkPercentage", DataType = "Int32")]
        public Int32 FinalMarkPercentage
        {
            get { return _FinalMarkPercentage; }
            set { _FinalMarkPercentage = value; }
        }
        [Column(Name = "Mark", DataType = "Int32")]
        public Int32 Mark
        {
            get { return _Mark; }
            set { _Mark = value; }
        }
        [Column(Name = "MarkInString", DataType = "String")]
        public String MarkInString
        {
            get { return _MarkInString; }
            set { _MarkInString = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vTeacherMarkItem
    [Serializable]
    [Table(Name = "vTeacherMarkItem")]
    public class vTeacherMarkItem
    {
        private Int32 _TeacherMarkItemID;
        private Int32 _TeacherMarkGroupID;
        private Int32 _TeacherMarkTypeItemID;
        private String _TeacherMarkTypeItemName;
        private Int32 _FinalMarkPercentage;
        private Int32 _Mark;
        private String _MarkInString;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "TeacherMarkItemID", DataType = "Int32")]
        public Int32 TeacherMarkItemID
        {
            get { return _TeacherMarkItemID; }
            set { _TeacherMarkItemID = value; }
        }
        [Column(Name = "TeacherMarkGroupID", DataType = "Int32")]
        public Int32 TeacherMarkGroupID
        {
            get { return _TeacherMarkGroupID; }
            set { _TeacherMarkGroupID = value; }
        }
        [Column(Name = "TeacherMarkTypeItemID", DataType = "Int32")]
        public Int32 TeacherMarkTypeItemID
        {
            get { return _TeacherMarkTypeItemID; }
            set { _TeacherMarkTypeItemID = value; }
        }
        [Column(Name = "TeacherMarkTypeItemName", DataType = "String")]
        public String TeacherMarkTypeItemName
        {
            get { return _TeacherMarkTypeItemName; }
            set { _TeacherMarkTypeItemName = value; }
        }
        [Column(Name = "FinalMarkPercentage", DataType = "Int32")]
        public Int32 FinalMarkPercentage
        {
            get { return _FinalMarkPercentage; }
            set { _FinalMarkPercentage = value; }
        }
        [Column(Name = "Mark", DataType = "Int32")]
        public Int32 Mark
        {
            get { return _Mark; }
            set { _Mark = value; }
        }
        [Column(Name = "MarkInString", DataType = "String")]
        public String MarkInString
        {
            get { return _MarkInString; }
            set { _MarkInString = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vTeacherMarkTypeGroup
    [Serializable]
    [Table(Name = "vTeacherMarkTypeGroup")]
    public class vTeacherMarkTypeGroup
    {
        private Int32 _TeacherMarkTypeGroupID;
        private String _SiteID;
        private String _SiteName;
        private String _TeacherMarkTypeGroupName;
        private Int32 _FinalMarkPercentage;
        private Boolean _IsDeleted;

        [Column(Name = "TeacherMarkTypeGroupID", DataType = "Int32")]
        public Int32 TeacherMarkTypeGroupID
        {
            get { return _TeacherMarkTypeGroupID; }
            set { _TeacherMarkTypeGroupID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "SiteName", DataType = "String")]
        public String SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }
        [Column(Name = "TeacherMarkTypeGroupName", DataType = "Int32")]
        public String TeacherMarkTypeGroupName
        {
            get { return _TeacherMarkTypeGroupName; }
            set { _TeacherMarkTypeGroupName = value; }
        }
        [Column(Name = "FinalMarkPercentage", DataType = "Int32")]
        public Int32 FinalMarkPercentage
        {
            get { return _FinalMarkPercentage; }
            set { _FinalMarkPercentage = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vTeacherMarkTypeItem
    [Serializable]
    [Table(Name = "vTeacherMarkTypeItem")]
    public class vTeacherMarkTypeItem
    {
        private Int32 _TeacherMarkTypeItemID;
        private Int32 _TeacherMarkTypeGroupID;
        private String _TeacherMarkTypeGroupName;
        private String _TeacherMarkTypeItemName;
        private Int32 _FinalMarkPercentage;
        private Boolean _IsDeleted;

        [Column(Name = "TeacherMarkTypeItemID", DataType = "Int32")]
        public Int32 TeacherMarkTypeItemID
        {
            get { return _TeacherMarkTypeItemID; }
            set { _TeacherMarkTypeItemID = value; }
        }
        [Column(Name = "TeacherMarkTypeGroupID", DataType = "Int32")]
        public Int32 TeacherMarkTypeGroupID
        {
            get { return _TeacherMarkTypeGroupID; }
            set { _TeacherMarkTypeGroupID = value; }
        }
        [Column(Name = "TeacherMarkTypeGroupName", DataType = "Int32")]
        public String TeacherMarkTypeGroupName
        {
            get { return _TeacherMarkTypeGroupName; }
            set { _TeacherMarkTypeGroupName = value; }
        }
        [Column(Name = "TeacherMarkTypeItemName", DataType = "Int32")]
        public String TeacherMarkTypeItemName
        {
            get { return _TeacherMarkTypeItemName; }
            set { _TeacherMarkTypeItemName = value; }
        }
        [Column(Name = "FinalMarkPercentage", DataType = "Int32")]
        public Int32 FinalMarkPercentage
        {
            get { return _FinalMarkPercentage; }
            set { _FinalMarkPercentage = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vTeacherSchedule
    [Serializable]
    [Table(Name = "vTeacherSchedule")]
    public class vTeacherSchedule
    {
        private Int32 _TeacherScheduleID;
        private Int32 _SchoolPeriodID;
        private Int32 _TeacherID;
        private String _TeacherName;
        private Int16 _DayNumber;
        private Int16 _HoursIndex;
        private Boolean _IsDeleted;

        [Column(Name = "TeacherScheduleID", DataType = "Int32")]
        public Int32 TeacherScheduleID
        {
            get { return _TeacherScheduleID; }
            set { _TeacherScheduleID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
        }
        [Column(Name = "TeacherName", DataType = "String")]
        public String TeacherName
        {
            get { return _TeacherName; }
            set { _TeacherName = value; }
        }
        [Column(Name = "DayNumber", DataType = "Int16")]
        public Int16 DayNumber
        {
            get { return _DayNumber; }
            set { _DayNumber = value; }
        }
        [Column(Name = "HoursIndex", DataType = "Int16")]
        public Int16 HoursIndex
        {
            get { return _HoursIndex; }
            set { _HoursIndex = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vTeacherSubject
    [Serializable]
    [Table(Name = "vTeacherSubject")]
    public class vTeacherSubject
    {
        private Int32 _TeacherID;
        private String _TeacherCode;
        private String _TeacherName;
        private Int32 _SubjectID;
        private String _SubjectCode;
        private String _SubjectName;

        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
        }
        [Column(Name = "TeacherCode", DataType = "String")]
        public String TeacherCode
        {
            get { return _TeacherCode; }
            set { _TeacherCode = value; }
        }
        [Column(Name = "TeacherName", DataType = "String")]
        public String TeacherName
        {
            get { return _TeacherName; }
            set { _TeacherName = value; }
        }
        [Column(Name = "SubjectID", DataType = "Int32")]
        public Int32 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
        [Column(Name = "SubjectCode", DataType = "String")]
        public String SubjectCode
        {
            get { return _SubjectCode; }
            set { _SubjectCode = value; }
        }
        [Column(Name = "SubjectName", DataType = "String")]
        public String SubjectName
        {
            get { return _SubjectName; }
            set { _SubjectName = value; }
        }
    }
    #endregion
    #region vTeacherSubstitution
    [Serializable]
    [Table(Name = "vTeacherSubstitution")]
    public class vTeacherSubstitution
    {
        private Int32 _TeacherSubstitutionID;
        private Int32 _TeacherAbsenceID;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private Int32 _ClassScheduleID;
        private Int32 _RoomID;
        private String _RoomCode;
        private String _RoomName;
        private Int32 _SchoolClassID;
        private String _SchoolClassCode;
        private String _SchoolClassName;
        private Int32 _ClassSubjectID;
        private Int32 _SubjectID;
        private String _SubjectName;
        private Int16 _DayNumber;
        private Int16 _HoursIndex;
        private DateTime _SchoolDate;
        private Int32 _TeacherID;
        private String _TeacherCode;
        private String _TeacherName;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "TeacherSubstitutionID", DataType = "Int32")]
        public Int32 TeacherSubstitutionID
        {
            get { return _TeacherSubstitutionID; }
            set { _TeacherSubstitutionID = value; }
        }
        [Column(Name = "TeacherAbsenceID", DataType = "Int32")]
        public Int32 TeacherAbsenceID
        {
            get { return _TeacherAbsenceID; }
            set { _TeacherAbsenceID = value; }
        }
        [Column(Name = "StartDate", DataType = "DateTime")]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Column(Name = "EndDate", DataType = "DateTime")]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        [Column(Name = "ClassScheduleID", DataType = "Int32")]
        public Int32 ClassScheduleID
        {
            get { return _ClassScheduleID; }
            set { _ClassScheduleID = value; }
        }
        [Column(Name = "RoomID", DataType = "Int32")]
        public Int32 RoomID
        {
            get { return _RoomID; }
            set { _RoomID = value; }
        }
        [Column(Name = "RoomCode", DataType = "String")]
        public String RoomCode
        {
            get { return _RoomCode; }
            set { _RoomCode = value; }
        }
        [Column(Name = "RoomName", DataType = "String")]
        public String RoomName
        {
            get { return _RoomName; }
            set { _RoomName = value; }
        }
        [Column(Name = "SchoolClassID", DataType = "Int32")]
        public Int32 SchoolClassID
        {
            get { return _SchoolClassID; }
            set { _SchoolClassID = value; }
        }
        [Column(Name = "SchoolClassCode", DataType = "String")]
        public String SchoolClassCode
        {
            get { return _SchoolClassCode; }
            set { _SchoolClassCode = value; }
        }
        [Column(Name = "SchoolClassName", DataType = "String")]
        public String SchoolClassName
        {
            get { return _SchoolClassName; }
            set { _SchoolClassName = value; }
        }
        [Column(Name = "ClassSubjectID", DataType = "Int32")]
        public Int32 ClassSubjectID
        {
            get { return _ClassSubjectID; }
            set { _ClassSubjectID = value; }
        }
        [Column(Name = "SubjectID", DataType = "Int32")]
        public Int32 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
        [Column(Name = "SubjectName", DataType = "String")]
        public String SubjectName
        {
            get { return _SubjectName; }
            set { _SubjectName = value; }
        }
        [Column(Name = "DayNumber", DataType = "Int16")]
        public Int16 DayNumber
        {
            get { return _DayNumber; }
            set { _DayNumber = value; }
        }
        [Column(Name = "HoursIndex", DataType = "Int16")]
        public Int16 HoursIndex
        {
            get { return _HoursIndex; }
            set { _HoursIndex = value; }
        }
        [Column(Name = "SchoolDate", DataType = "DateTime")]
        public DateTime SchoolDate
        {
            get { return _SchoolDate; }
            set { _SchoolDate = value; }
        }
        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
        }
        [Column(Name = "TeacherCode", DataType = "String")]
        public String TeacherCode
        {
            get { return _TeacherCode; }
            set { _TeacherCode = value; }
        }
        [Column(Name = "TeacherName", DataType = "String")]
        public String TeacherName
        {
            get { return _TeacherName; }
            set { _TeacherName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
}
