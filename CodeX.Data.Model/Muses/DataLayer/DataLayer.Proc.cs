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
        private Decimal _IN_Return;
        private Decimal _IN_Void;
        private Decimal _OUT_Charges;
        private Decimal _OUT_Distribution;
        private Decimal _OUT_Adjustment;
        private Decimal _OUT_Consumption;
        private Decimal _OUT_Void;
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
        [Column(Name = "IN_Return", DataType = "Decimal")]
        public Decimal IN_Return
        {
            get { return _IN_Return; }
            set { _IN_Return = value; }
        }
        [Column(Name = "IN_Void", DataType = "Decimal")]
        public Decimal IN_Void
        {
            get { return _IN_Void; }
            set { _IN_Void = value; }
        }
        [Column(Name = "OUT_Charges", DataType = "Decimal")]
        public Decimal OUT_Charges
        {
            get { return _OUT_Charges; }
            set { _OUT_Charges = value; }
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
        [Column(Name = "OUT_Void", DataType = "Decimal")]
        public Decimal OUT_Void
        {
            get { return _OUT_Void; }
            set { _OUT_Void = value; }
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