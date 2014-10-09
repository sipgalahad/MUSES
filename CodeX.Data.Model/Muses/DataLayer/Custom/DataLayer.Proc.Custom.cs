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
    #region GetItemMovementPerPeriodeDetail
    public partial class GetItemMovementPerPeriodeDetail
    {
        public Decimal QuantityEND
        {
            get
            {
                return _IN_QuantityBEGIN + _IN_PurchaseReceive + _IN_Distribution + _IN_Adjustment + _IN_Void + _IN_Return - _OUT_Adjustment - _OUT_Distribution - _OUT_Consumption - _OUT_Charges - _OUT_Void;
            }
        }
    }
    #endregion    
}
