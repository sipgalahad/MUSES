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
        private String _GCAdmissionFeeCompType;
        private String _AdmissionFeeCompType;
        private String _GCAdmissionPaymentPeriod;
        private String _AdmissionPaymentPeriod;
        private Boolean _IsFixedAmount;
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
        [Column(Name = "GCAdmissionFeeCompType", DataType = "String")]
        public String GCAdmissionFeeCompType
        {
            get { return _GCAdmissionFeeCompType; }
            set { _GCAdmissionFeeCompType = value; }
        }
        [Column(Name = "AdmissionFeeCompType", DataType = "String")]
        public String AdmissionFeeCompType
        {
            get { return _AdmissionFeeCompType; }
            set { _AdmissionFeeCompType = value; }
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
        private String _GCAdmissionFeeCompType;
        private String _AdmissionFeeCompType;
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
        [Column(Name = "GCAdmissionFeeCompType", DataType = "String")]
        public String GCAdmissionFeeCompType
        {
            get { return _GCAdmissionFeeCompType; }
            set { _GCAdmissionFeeCompType = value; }
        }
        [Column(Name = "AdmissionFeeCompType", DataType = "String")]
        public String AdmissionFeeCompType
        {
            get { return _AdmissionFeeCompType; }
            set { _AdmissionFeeCompType = value; }
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
        private String _GCTransactionStatus;
        private String _TransactionStatus;
        private Decimal _TransactionAmount;
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
        private Int32 _BankID;

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
        [Column(Name = "BankID", DataType = "Int32")]
        public Int32 BankID
        {
            get { return _BankID; }
            set { _BankID = value; }
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
        private Int32 _StudentID;
        private String _StudentCode;
        private String _StudentName;
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
        private Int32 _SchoolClassID;
        private String _SchoolClassName;
        private Int32 _PeriodClassTypeSubjectID;
        private Int32 _SubjectID;
        private String _SubjectCode;
        private String _SubjectName;
        private Int16 _NoMeetingHoursInWeek;
        private Int32 _ParentID;
        private Int32 _TeacherID;
        private String _TeacherCode;
        private String _TeacherName;
        private Int32 _RoomID;
        private String _RoomName;
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
        private Decimal _Mark;

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
        [Column(Name = "Mark", DataType = "Decimal")]
        public Decimal Mark
        {
            get { return _Mark; }
            set { _Mark = value; }
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
    #region vClassType
    [Serializable]
    [Table(Name = "vClassType")]
    public class vClassType
    {
        private Int32 _ClassTypeID;
        private String _ClassTypeCode;
        private String _ClassTypeName;
        private String _SiteID;
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
        private Decimal _DiscountPercentage1;
        private Decimal _DiscountPercentage2;
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
    #region vGLSetting
    [Serializable]
    [Table(Name = "vGLSetting")]
    public class vGLSetting
    {
        private Int32 _ID;
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

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
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
    }
    #endregion
    #region vJournalTemplateDt
    [Serializable]
    [Table(Name = "vJournalTemplateDt")]
    public class vJournalTemplateDt
    {
        private Int32 _ID;
        private Int32 _TemplateID;
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
        private DateTime _RegistrationStartDate;
        private DateTime _RegistrationEndDate;
        private DateTime _StartDate;
        private DateTime _EndDate;
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
        private String _GCGrade;
        private String _Grade;
        private String _GCMajor;
        private String _Major;
        private Int32 _DailySchedulePackageID;
        private String _DailySchedulePackageName;
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
        private Int32 _TeacherID;
        private String _TeacherCode;
        private String _TeacherName;
        private Int16 _NoMeetingHoursInWeek;
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
        private Decimal _DiscountPercentage2;
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
        [Column(Name = "DiscountPercentage2", DataType = "Decimal")]
        public Decimal DiscountPercentage2
        {
            get { return _DiscountPercentage2; }
            set { _DiscountPercentage2 = value; }
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
        private Decimal _FinalDiscount;
        private Decimal _VATPercentage;
        private Decimal _DownPaymentAmount;
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
        [Column(Name = "DownPaymentAmount", DataType = "Decimal")]
        public Decimal DownPaymentAmount
        {
            get { return _DownPaymentAmount; }
            set { _DownPaymentAmount = value; }
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
        private Decimal _DiscountAmount;
        private Decimal _FinalDiscount;
        private Decimal _StampAmount;
        private Decimal _VATPercentage;
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
        [Column(Name = "DiscountAmount", DataType = "Decimal")]
        public Decimal DiscountAmount
        {
            get { return _DiscountAmount; }
            set { _DiscountAmount = value; }
        }
        [Column(Name = "FinalDiscount", DataType = "Decimal")]
        public Decimal FinalDiscount
        {
            get { return _FinalDiscount; }
            set { _FinalDiscount = value; }
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
        private Decimal _DiscountPercentage2;
        private Boolean _IsBonusItem;
        private Boolean _IsControlExpired;
        private String _GCItemDetailStatus;
        private String _ItemDetailStatus;
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
        private Decimal _DiscountAmount;
        private Decimal _FinalDiscount;
        private String _GCChargesType;
        private String _ChargesType;
        private Decimal _ChargesAmount;
        private Decimal _StampAmount;
        private Decimal _VATPercentage;
        private Decimal _DownPaymentAmount;
        private String _DownPaymentReferenceNo;
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
        [Column(Name = "DiscountAmount", DataType = "Decimal")]
        public Decimal DiscountAmount
        {
            get { return _DiscountAmount; }
            set { _DiscountAmount = value; }
        }
        [Column(Name = "FinalDiscount", DataType = "Decimal")]
        public Decimal FinalDiscount
        {
            get { return _FinalDiscount; }
            set { _FinalDiscount = value; }
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
        private Decimal _DiscountPercentage2;
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
        [Column(Name = "DiscountPercentage2", DataType = "Decimal")]
        public Decimal DiscountPercentage2
        {
            get { return _DiscountPercentage2; }
            set { _DiscountPercentage2 = value; }
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
        private Decimal _FinalMark;
        private Int32 _AdmissionFeeRuleID;
        private String _AdmissionFeeRuleName;
        private Int32 _PaymentID;
        private String _PaymentName;
        private String _Remarks;
        private String _GCRegistrationStatus;
        private String _RegistrationStatus;
        private String _ProspectiveStudentCode;
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
        private String _GCGrade;
        private String _GCMajor;
        private Int32 _RoomID;
        private String _RoomName;
        private Int32 _TeacherID;
        private String _TeacherName;
        private Int16 _MaxStudent;
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
    #region vSubjectGradeMajor
    [Serializable]
    [Table(Name = "vSubjectGradeMajor")]
    public class vSubjectGradeMajor
    {
        private Int32 _SubjectID;
        private String _SubjectCode;
        private String _SubjectName;
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
}
