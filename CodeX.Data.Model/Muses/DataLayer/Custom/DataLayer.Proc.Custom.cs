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
}
