using System;
using CodeX.Data.Core.Dal;

/***************************************************************************
 * $Archive: $
 * $Workfile: $
 * $Author: $
 * $Date: $
 * $Modtime: $  
 * $Revision: $
 ***************************************************************************/
namespace CodeX.Data.Model
{
    #region GetAPSupplierInformation
    [Serializable]
    [Table(Name = "GetAPSupplierInformation")]
    public partial class GetAPSupplierInformation
    {
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private Decimal _BalanceBegin;
        private Decimal _BalanceIN;
        private Decimal _BalanceOUT;
        private Decimal _Days_0_30;
        private Decimal _Days_30_60;
        private Decimal _Days_60_90;
        private Decimal _Days_90;
        private Decimal _BalanceEND;

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
        [Column(Name = "BalanceBegin", DataType = "Decimal")]
        public Decimal BalanceBegin
        {
            get { return _BalanceBegin; }
            set { _BalanceBegin = value; }
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
        [Column(Name = "Days_0_30", DataType = "Decimal")]
        public Decimal Days_0_30
        {
            get { return _Days_0_30; }
            set { _Days_0_30 = value; }
        }
        [Column(Name = "Days_30_60", DataType = "Decimal")]
        public Decimal Days_30_60
        {
            get { return _Days_30_60; }
            set { _Days_30_60 = value; }
        }
        [Column(Name = "Days_60_90", DataType = "Decimal")]
        public Decimal Days_60_90
        {
            get { return _Days_60_90; }
            set { _Days_60_90 = value; }
        }
        [Column(Name = "Days_90", DataType = "Decimal")]
        public Decimal Days_90
        {
            get { return _Days_90; }
            set { _Days_90 = value; }
        }
        [Column(Name = "BalanceEND", DataType = "Decimal")]
        public Decimal BalanceEND
        {
            get { return _BalanceEND; }
            set { _BalanceEND = value; }
        }
    }
    #endregion
    #region GetAPSupplierInformationDt
    [Serializable]
    [Table(Name = "GetAPSupplierInformationDt")]
    public partial class GetAPSupplierInformationDt
    {
        private DateTime _PurchaseReceivedDate;
        private DateTime _PurchaseInvoiceDate;
        private String _PurchaseInvoiceNo;
        private Int32 _PurchaseReceiveID;
        private String _PurchaseReceiveNo;
        private Int32 _SupplierID;
        private String _SupplierCode;
        private String _SupplierName;
        private Decimal _InvoiceAmount;
        private Decimal _PaymentAmount;

        [Column(Name = "PurchaseReceivedDate", DataType = "DateTime")]
        public DateTime PurchaseReceivedDate
        {
            get { return _PurchaseReceivedDate; }
            set { _PurchaseReceivedDate = value; }
        }
        [Column(Name = "PurchaseInvoiceDate", DataType = "DateTime")]
        public DateTime PurchaseInvoiceDate
        {
            get { return _PurchaseInvoiceDate; }
            set { _PurchaseInvoiceDate = value; }
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
        [Column(Name = "InvoiceAmount", DataType = "Decimal")]
        public Decimal InvoiceAmount
        {
            get { return _InvoiceAmount; }
            set { _InvoiceAmount = value; }
        }
        [Column(Name = "PaymentAmount", DataType = "Decimal")]
        public Decimal PaymentAmount
        {
            get { return _PaymentAmount; }
            set { _PaymentAmount = value; }
        }
    }
    #endregion
    #region GetARCustomerInformation
    [Serializable]
    [Table(Name = "GetARCustomerInformation")]
    public partial class GetARCustomerInformation
    {
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private Decimal _BalanceBegin;
        private Decimal _BalanceIN;
        private Decimal _BalanceOUT;
        private Decimal _Days_0_30;
        private Decimal _Days_30_60;
        private Decimal _Days_60_90;
        private Decimal _Days_90;
        private Decimal _BalanceEND;

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
        [Column(Name = "BalanceBegin", DataType = "Decimal")]
        public Decimal BalanceBegin
        {
            get { return _BalanceBegin; }
            set { _BalanceBegin = value; }
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
        [Column(Name = "Days_0_30", DataType = "Decimal")]
        public Decimal Days_0_30
        {
            get { return _Days_0_30; }
            set { _Days_0_30 = value; }
        }
        [Column(Name = "Days_30_60", DataType = "Decimal")]
        public Decimal Days_30_60
        {
            get { return _Days_30_60; }
            set { _Days_30_60 = value; }
        }
        [Column(Name = "Days_60_90", DataType = "Decimal")]
        public Decimal Days_60_90
        {
            get { return _Days_60_90; }
            set { _Days_60_90 = value; }
        }
        [Column(Name = "Days_90", DataType = "Decimal")]
        public Decimal Days_90
        {
            get { return _Days_90; }
            set { _Days_90 = value; }
        }
        [Column(Name = "BalanceEND", DataType = "Decimal")]
        public Decimal BalanceEND
        {
            get { return _BalanceEND; }
            set { _BalanceEND = value; }
        }
    }
    #endregion
    #region GetARCustomerInformationDt
    [Serializable]
    [Table(Name = "GetARCustomerInformationDt")]
    public partial class GetARCustomerInformationDt
    {
        private DateTime _ARInvoiceDate;
        private String _ARInvoiceNo;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private Decimal _InvoiceAmount;
        private Decimal _PaymentAmount;

        [Column(Name = "ARInvoiceDate", DataType = "DateTime")]
        public DateTime ARInvoiceDate
        {
            get { return _ARInvoiceDate; }
            set { _ARInvoiceDate = value; }
        }
        [Column(Name = "ARInvoiceNo", DataType = "String")]
        public String ARInvoiceNo
        {
            get { return _ARInvoiceNo; }
            set { _ARInvoiceNo = value; }
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
        [Column(Name = "InvoiceAmount", DataType = "Decimal")]
        public Decimal InvoiceAmount
        {
            get { return _InvoiceAmount; }
            set { _InvoiceAmount = value; }
        }
        [Column(Name = "PaymentAmount", DataType = "Decimal")]
        public Decimal PaymentAmount
        {
            get { return _PaymentAmount; }
            set { _PaymentAmount = value; }
        }
    }
    #endregion
    #region GetARProspectiveStudentInformation
    [Serializable]
    [Table(Name = "GetARProspectiveStudentInformation")]
    public partial class GetARProspectiveStudentInformation
    {
        private Int32 _ProspectiveStudentID;
        private String _ProspectiveStudentCode;
        private String _ProspectiveStudentName;
        private Decimal _BalanceBegin;
        private Decimal _BalanceIN;
        private Decimal _BalanceOUT;
        private Decimal _Days_0_30;
        private Decimal _Days_30_60;
        private Decimal _Days_60_90;
        private Decimal _Days_90;
        private Decimal _BalanceEND;

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
        [Column(Name = "BalanceBegin", DataType = "Decimal")]
        public Decimal BalanceBegin
        {
            get { return _BalanceBegin; }
            set { _BalanceBegin = value; }
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
        [Column(Name = "Days_0_30", DataType = "Decimal")]
        public Decimal Days_0_30
        {
            get { return _Days_0_30; }
            set { _Days_0_30 = value; }
        }
        [Column(Name = "Days_30_60", DataType = "Decimal")]
        public Decimal Days_30_60
        {
            get { return _Days_30_60; }
            set { _Days_30_60 = value; }
        }
        [Column(Name = "Days_60_90", DataType = "Decimal")]
        public Decimal Days_60_90
        {
            get { return _Days_60_90; }
            set { _Days_60_90 = value; }
        }
        [Column(Name = "Days_90", DataType = "Decimal")]
        public Decimal Days_90
        {
            get { return _Days_90; }
            set { _Days_90 = value; }
        }
        [Column(Name = "BalanceEND", DataType = "Decimal")]
        public Decimal BalanceEND
        {
            get { return _BalanceEND; }
            set { _BalanceEND = value; }
        }
    }
    #endregion
    #region GetARProspectiveStudentInformationDt
    [Serializable]
    [Table(Name = "GetARProspectiveStudentInformationDt")]
    public partial class GetARProspectiveStudentInformationDt
    {
        private DateTime _ARInvoiceDate;
        private String _ARInvoiceNo;
        private Int32 _ProspectiveStudentID;
        private String _ProspectiveStudentCode;
        private String _ProspectiveStudentName;
        private Decimal _InvoiceAmount;
        private Decimal _PaymentAmount;

        [Column(Name = "ARInvoiceDate", DataType = "DateTime")]
        public DateTime ARInvoiceDate
        {
            get { return _ARInvoiceDate; }
            set { _ARInvoiceDate = value; }
        }
        [Column(Name = "ARInvoiceNo", DataType = "String")]
        public String ARInvoiceNo
        {
            get { return _ARInvoiceNo; }
            set { _ARInvoiceNo = value; }
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
        [Column(Name = "InvoiceAmount", DataType = "Decimal")]
        public Decimal InvoiceAmount
        {
            get { return _InvoiceAmount; }
            set { _InvoiceAmount = value; }
        }
        [Column(Name = "PaymentAmount", DataType = "Decimal")]
        public Decimal PaymentAmount
        {
            get { return _PaymentAmount; }
            set { _PaymentAmount = value; }
        }
    }
    #endregion
    #region GetARStudentInformation
    [Serializable]
    [Table(Name = "GetARStudentInformation")]
    public partial class GetARStudentInformation
    {
        private Int32 _StudentID;
        private String _StudentCode;
        private String _StudentName;
        private Decimal _BalanceBegin;
        private Decimal _BalanceIN;
        private Decimal _BalanceOUT;
        private Decimal _Days_0_30;
        private Decimal _Days_30_60;
        private Decimal _Days_60_90;
        private Decimal _Days_90;
        private Decimal _BalanceEND;

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
        [Column(Name = "BalanceBegin", DataType = "Decimal")]
        public Decimal BalanceBegin
        {
            get { return _BalanceBegin; }
            set { _BalanceBegin = value; }
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
        [Column(Name = "Days_0_30", DataType = "Decimal")]
        public Decimal Days_0_30
        {
            get { return _Days_0_30; }
            set { _Days_0_30 = value; }
        }
        [Column(Name = "Days_30_60", DataType = "Decimal")]
        public Decimal Days_30_60
        {
            get { return _Days_30_60; }
            set { _Days_30_60 = value; }
        }
        [Column(Name = "Days_60_90", DataType = "Decimal")]
        public Decimal Days_60_90
        {
            get { return _Days_60_90; }
            set { _Days_60_90 = value; }
        }
        [Column(Name = "Days_90", DataType = "Decimal")]
        public Decimal Days_90
        {
            get { return _Days_90; }
            set { _Days_90 = value; }
        }
        [Column(Name = "BalanceEND", DataType = "Decimal")]
        public Decimal BalanceEND
        {
            get { return _BalanceEND; }
            set { _BalanceEND = value; }
        }
    }
    #endregion
    #region GetARStudentInformationDt
    [Serializable]
    [Table(Name = "GetARStudentInformationDt")]
    public partial class GetARStudentInformationDt
    {
        private DateTime _ARInvoiceDate;
        private String _ARInvoiceNo;
        private Int32 _StudentID;
        private String _StudentCode;
        private String _StudentName;
        private Decimal _InvoiceAmount;
        private Decimal _PaymentAmount;

        [Column(Name = "ARInvoiceDate", DataType = "DateTime")]
        public DateTime ARInvoiceDate
        {
            get { return _ARInvoiceDate; }
            set { _ARInvoiceDate = value; }
        }
        [Column(Name = "ARInvoiceNo", DataType = "String")]
        public String ARInvoiceNo
        {
            get { return _ARInvoiceNo; }
            set { _ARInvoiceNo = value; }
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
        [Column(Name = "InvoiceAmount", DataType = "Decimal")]
        public Decimal InvoiceAmount
        {
            get { return _InvoiceAmount; }
            set { _InvoiceAmount = value; }
        }
        [Column(Name = "PaymentAmount", DataType = "Decimal")]
        public Decimal PaymentAmount
        {
            get { return _PaymentAmount; }
            set { _PaymentAmount = value; }
        }
    }
    #endregion
    #region GetGLBalanceDtInformation
    public partial class GetGLBalanceDtInformation
    {
        private Int32 _TransactionDtID;
        private String _JournalNo;
        private DateTime _JournalDate;
        private String _Remarks;
        private Decimal _DEBITAmount;
        private Decimal _CREDITAmount;
        private Decimal _BalanceEND;

        [Column(Name = "TransactionDtID", DataType = "Int32")]
        public Int32 TransactionDtID
        {
            get { return _TransactionDtID; }
            set { _TransactionDtID = value; }
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
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "DEBITAmount", DataType = "Decimal")]
        public Decimal DEBITAmount
        {
            get { return _DEBITAmount; }
            set { _DEBITAmount = value; }
        }
        [Column(Name = "CREDITAmount", DataType = "Decimal")]
        public Decimal CREDITAmount
        {
            get { return _CREDITAmount; }
            set { _CREDITAmount = value; }
        }
        [Column(Name = "BalanceEND", DataType = "Decimal")]
        public Decimal BalanceEND
        {
            get { return _BalanceEND; }
            set { _BalanceEND = value; }
        }
    }
    #endregion
    #region GetGLBalanceDtPerPeriod
    public class GetGLBalanceDtPerPeriod
    {
        private Int32 _SubLedgerDtID;
        private String _SubLedgerDtCode;
        private String _SubLedgerDtName;
        private Decimal _BalanceBEGIN;
        private Decimal _BalanceCREDIT;
        private Decimal _BalanceDEBIT;
        private Decimal _BalanceEND;

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
        [Column(Name = "BalanceBEGIN", DataType = "Decimal")]
        public Decimal BalanceBEGIN
        {
            get { return _BalanceBEGIN; }
            set { _BalanceBEGIN = value; }
        }
        [Column(Name = "BalanceCREDIT", DataType = "Decimal")]
        public Decimal BalanceCREDIT
        {
            get { return _BalanceCREDIT; }
            set { _BalanceCREDIT = value; }
        }
        [Column(Name = "BalanceDEBIT", DataType = "Decimal")]
        public Decimal BalanceDEBIT
        {
            get { return _BalanceDEBIT; }
            set { _BalanceDEBIT = value; }
        }
        [Column(Name = "BalanceEND", DataType = "Decimal")]
        public Decimal BalanceEND
        {
            get { return _BalanceEND; }
            set { _BalanceEND = value; }
        }
    }
    #endregion
    #region GetGLBalanceDtPerSubLedger
    public partial class GetGLBalanceDtPerSubLedger
    {
        private Int32 _TransactionDtID;
        private String _JournalNo;
        private DateTime _JournalDate;
        private String _Remarks;
        private Decimal _DebitAmount;
        private Decimal _CreditAmount;
        private Decimal _BalanceEND;

        [Column(Name = "TransactionDtID", DataType = "Int32")]
        public Int32 TransactionDtID
        {
            get { return _TransactionDtID; }
            set { _TransactionDtID = value; }
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
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
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
        [Column(Name = "BalanceEND", DataType = "Decimal")]
        public Decimal BalanceEND
        {
            get { return _BalanceEND; }
            set { _BalanceEND = value; }
        }


    }
    #endregion
    #region GetGLBalancePerGLAccount
    public partial class GetGLBalancePerGLAccount
    {
        private Int32 _TransactionDtID;
        private String _JournalNo;
        private DateTime _JournalDate;
        private String _Remarks;
        private Decimal _DEBITAmount;
        private Decimal _CREDITAmount;
        private Decimal _BalanceEND;

        [Column(Name = "TransactionDtID", DataType = "Int32")]
        public Int32 TransactionDtID
        {
            get { return _TransactionDtID; }
            set { _TransactionDtID = value; }
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
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "DEBITAmount", DataType = "Decimal")]
        public Decimal DEBITAmount
        {
            get { return _DEBITAmount; }
            set { _DEBITAmount = value; }
        }
        [Column(Name = "CREDITAmount", DataType = "Decimal")]
        public Decimal CREDITAmount
        {
            get { return _CREDITAmount; }
            set { _CREDITAmount = value; }
        }
        [Column(Name = "BalanceEND", DataType = "Decimal")]
        public Decimal BalanceEND
        {
            get { return _BalanceEND; }
            set { _BalanceEND = value; }
        }
    }
    #endregion
    #region GetGLBalancePerPeriod
    public class GetGLBalancePerPeriod
    {
        private Int32 _GLAccountID;
        private String _GLAccountNo;
        private String _GLAccountName;
        private String _Position;
        private Decimal _BalanceBEGIN;
        private Decimal _BalanceDEBIT;
        private Decimal _BalanceCREDIT;
        private Decimal _BalanceEND;
        private Int32 _Level;
        private Boolean _IsHeader;

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
        [Column(Name = "Position", DataType = "String")]
        public String Position
        {
            get { return _Position; }
            set { _Position = value; }
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
        [Column(Name = "Level", DataType = "Int32")]
        public Int32 Level
        {
            get { return _Level; }
            set { _Level = value; }
        }
        [Column(Name = "IsHeader", DataType = "Boolean")]
        public Boolean IsHeader
        {
            get { return _IsHeader; }
            set { _IsHeader = value; }
        }
    }
    #endregion
    #region GetGLBalanceProfitLossPerPeriodPerLevel
    public partial class GetGLBalanceProfitLossPerPeriodPerLevel
    {
        private Int32 _GLAccountID;
        private String _GLAccountNo;
        private String _GLAccountName;
        private String _GCGLAccountType;
        private String _GLAccountType;
        private String _Position;
        private Boolean _IsHeader;
        private Int32 _Level;
        private Decimal _BalanceBEGIN;
        private Decimal _ProfitLoss;
        private Decimal _BalanceENDLastMonth;
        private Decimal _cfBalanceBEGIN;
        private Decimal _cfProfitLoss;
        private Decimal _cfBalanceENDLastMonth;
        private Decimal _BudgetAmount;
        private Int32 _TotalRow;
        
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
        [Column(Name = "Level", DataType = "Int32")]
        public Int32 Level
        {
            get { return _Level; }
            set { _Level = value; }
        }
        [Column(Name = "BalanceBEGIN", DataType = "Decimal")]
        public Decimal BalanceBEGIN
        {
            get { return _BalanceBEGIN; }
            set { _BalanceBEGIN = value; }
        }
        [Column(Name = "ProfitLoss", DataType = "Decimal")]
        public Decimal ProfitLoss
        {
            get { return _ProfitLoss; }
            set { _ProfitLoss = value; }
        }
        [Column(Name = "BalanceENDLastMonth", DataType = "Decimal")]
        public Decimal BalanceENDLastMonth
        {
            get { return _BalanceENDLastMonth; }
            set { _BalanceENDLastMonth = value; }
        }
        [Column(Name = "cfBalanceBEGIN", DataType = "Decimal")]
        public Decimal cfBalanceBEGIN
        {
            get { return _cfBalanceBEGIN; }
            set { _cfBalanceBEGIN = value; }
        }
        [Column(Name = "cfProfitLoss", DataType = "Decimal")]
        public Decimal cfProfitLoss
        {
            get { return _cfProfitLoss; }
            set { _cfProfitLoss = value; }
        }
        [Column(Name = "cfBalanceENDLastMonth", DataType = "Decimal")]
        public Decimal cfBalanceENDLastMonth
        {
            get { return _cfBalanceENDLastMonth; }
            set { _cfBalanceENDLastMonth = value; }
        }
        [Column(Name = "BudgetAmount", DataType = "Decimal")]
        public Decimal BudgetAmount
        {
            get { return _BudgetAmount; }
            set { _BudgetAmount = value; }
        }
        [Column(Name = "TotalRow", DataType = "Int32")]
        public Int32 TotalRow
        {
            get { return _TotalRow; }
            set { _TotalRow = value; }
        }
    }
    #endregion
    #region GetItemMasterSales
    [Serializable]
    [Table(Name = "GetItemMasterSales")]
    public class GetItemMasterSales
    {
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private String _GCItemType;
        private String _GCItemUnit;
        private String _GCPurchaseUnit;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Decimal _Discount;
        private Decimal _Price;
        private Int32 _StudentID;
        private String _StudentCode;
        private String _StudentName;
        private Int32 _QtyOnOrder;

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
        [Column(Name = "GCPurchaseUnit", DataType = "String")]
        public String GCPurchaseUnit
        {
            get { return _GCPurchaseUnit; }
            set { _GCPurchaseUnit = value; }
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
        [Column(Name = "Discount", DataType = "Decimal")]
        public Decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }
        [Column(Name = "Price", DataType = "Decimal")]
        public Decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
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
        [Column(Name = "QtyOnOrder", DataType = "Int32")]
        public Int32 QtyOnOrder
        {
            get { return _QtyOnOrder; }
            set { _QtyOnOrder = value; }
        }
    }
    #endregion
    #region GetItemMovementPerPeriodeDetail
    [Serializable]
    [Table(Name = "GetItemMovementPerPeriodeDetail")]
    public partial class GetItemMovementPerPeriodeDetail
    {
        private int _ItemID;
        private String _ItemName1;
        private String _ItemUnit;
        private Decimal _IN_QuantityBEGIN;
        private Decimal _IN_PurchaseReceive;
        private Decimal _IN_Distribution;
        private Decimal _IN_Adjustment;
        private Decimal _IN_PriceChanged;
        private Decimal _OUT_Distribution;
        private Decimal _OUT_Adjustment;
        private Decimal _OUT_Consumption;
        private Decimal _OUT_Pemusnahan;
        private Decimal _OUT_PriceChanged;
        private bool _IsDeleted;

        [Column(Name = "ItemID", DataType = "int")]
        public int ItemID
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
        [Column(Name = "IN_QuantityBEGIN", DataType = "Decimal")]
        public Decimal IN_QuantityBEGIN
        {
            get { return _IN_QuantityBEGIN; }
            set { _IN_QuantityBEGIN = value; }
        }
        [Column(Name = "IN_PurchaseReceive", DataType = "Decimal")]
        public Decimal IN_PurchaseReceive
        {
            get { return _IN_PurchaseReceive; }
            set { _IN_PurchaseReceive = value; }
        }
        [Column(Name = "IN_Distribution", DataType = "Decimal")]
        public Decimal IN_Distribution
        {
            get { return _IN_Distribution; }
            set { _IN_Distribution = value; }
        }
        [Column(Name = "IN_Adjustment", DataType = "Decimal")]
        public Decimal IN_Adjustment
        {
            get { return _IN_Adjustment; }
            set { _IN_Adjustment = value; }
        }
        [Column(Name = "IN_PriceChanged", DataType = "Decimal")]
        public Decimal IN_PriceChanged
        {
            get { return _IN_PriceChanged; }
            set { _IN_PriceChanged = value; }
        }
        [Column(Name = "OUT_Distribution", DataType = "Decimal")]
        public Decimal OUT_Distribution
        {
            get { return _OUT_Distribution; }
            set { _OUT_Distribution = value; }
        }
        [Column(Name = "OUT_Adjustment", DataType = "Decimal")]
        public Decimal OUT_Adjustment
        {
            get { return _OUT_Adjustment; }
            set { _OUT_Adjustment = value; }
        }
        [Column(Name = "OUT_Consumption", DataType = "Decimal")]
        public Decimal OUT_Consumption
        {
            get { return _OUT_Consumption; }
            set { _OUT_Consumption = value; }
        }
        [Column(Name = "OUT_Pemusnahan", DataType = "Decimal")]
        public Decimal OUT_Pemusnahan
        {
            get { return _OUT_Pemusnahan; }
            set { _OUT_Pemusnahan = value; }
        }
        [Column(Name = "OUT_PriceChanged", DataType = "Decimal")]
        public Decimal OUT_PriceChanged
        {
            get { return _OUT_PriceChanged; }
            set { _OUT_PriceChanged = value; }
        }
        [Column(Name = "IsDeleted", DataType = "bool")]
        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region GetItemQtyOnOrder
    [Serializable]
    [Table(Name = "GetItemQtyOnOrder")]
    public class GetItemQtyOnOrder
    {
        private Int32 _QtyOnOrder;

        [Column(Name = "QtyOnOrder", DataType = "Int32")]
        public Int32 QtyOnOrder
        {
            get { return _QtyOnOrder; }
            set { _QtyOnOrder = value; }
        }
    }
    #endregion
}