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
        private Decimal _OUT_Distribution;
        private Decimal _OUT_Adjustment;
        private Decimal _OUT_Consumption;
        private Decimal _OUT_Pemusnahan;
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