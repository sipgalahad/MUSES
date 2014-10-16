using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using CodeX.Common;

namespace CodeX.Data.Model
{
    #region vAPMovement
    public partial class vAPMovement
    {
        public String MovementDateInString
        {
            get { return _MovementDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
    }
    #endregion
    #region vChartOfAccount
    public partial class vChartOfAccount
    {
        public String cfIsHeader
        {
            get
            {
                if (_IsHeader) return "I";
                return "A";
            }
        }
    }
    #endregion
    #region vDirectPurchaseDt
    public partial class vDirectPurchaseDt
    {
        public Decimal CustomSubTotal
        {
            get
            {
                Decimal total = _Quantity * _UnitPrice;
                total = total - (total * _DiscountPercentage / 100);
                return total;
            }
        }

        public Decimal CustomDiscount
        {
            get
            {
                return (_Quantity * _UnitPrice) * _DiscountPercentage / 100;
            }
        }

        public Boolean IsAllowEditItem
        {
            get
            {
                return (_GCItemDetailStatus == Constant.TransactionStatus.OPEN);
            }
        }

        public String CustomConversion
        {
            get
            {
                return "1.00 " + _ItemUnit + " = " + ConversionFactor + " " + _BaseUnit;
            }
        }

        public String CustomItemUnit
        {
            get
            {
                return _Quantity + " " + _ItemUnit;
            }
        }

        public String CustomUnitPrice
        {
            get
            {
                return UnitPrice.ToString("N") + " / " + _ItemUnit;
            }
        }

        public Decimal CustomTotalDiscount
        {
            get
            {
                return (Quantity * UnitPrice * ConversionFactor) - CustomSubTotal;
            }
        }
        //public String CustomQtyRemaining
        //{
        //    get
        //    {
        //        return string.Format("{0:N}", (_Quantity - _ReceivedQuantity));
        //    }
        //}
    }
    #endregion
    #region vDirectPurchaseHd
    public partial class vDirectPurchaseHd
    {
        public string PurchaseDateInString
        {
            get
            {
                return _PurchaseDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }

        public string ReferenceDateInString
        {
            get
            {
                return _ReferenceDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
    }
    #endregion
    #region vDirectPurchaseReturnDt
    public partial class vDirectPurchaseReturnDt
    {
        public String CustomConversion
        {
            get
            {
                return "1.00 " + _ItemUnit + " = " + ConversionFactor + " " + _BaseUnit;
            }
        }
        public Decimal CustomDiscount
        {
            get
            {
                return (_Quantity * _UnitPrice) * _DiscountPercentage1 / 100;
            }
        }

        public Decimal CustomSubTotal
        {
            get
            {
                // Decimal totalAfterDisc1 = (Quantity * UnitPrice * ConversionFactor) - ((Quantity * UnitPrice * ConversionFactor) *
                //_DiscountPercentage1 / 100);
                Decimal totalAfterDisc1 = (Quantity * UnitPrice) - ((Quantity * UnitPrice) *
               _DiscountPercentage1 / 100);
                Decimal totalAfterDisc2 = totalAfterDisc1 - (_DiscountPercentage2 / 100 * totalAfterDisc1);
                return totalAfterDisc2;
            }
        }

        public Decimal CustomLineAmount
        {
            get
            {
                return _Quantity * _UnitPrice;
            }
        }

        public Decimal CustomTotalDiscount
        {
            get
            {
                //return (Quantity * UnitPrice * ConversionFactor) - CustomSubTotal;
                return (Quantity * UnitPrice) - CustomSubTotal;
            }
        }

        public Boolean IsAllowEditItem
        {
            get
            {
                return (_GCItemDetailStatus == Constant.TransactionStatus.OPEN);
            }
        }

        public String CustomUnitPrice
        {
            get
            {
                return UnitPrice.ToString("N") + " / " + _ItemUnit;
            }
        }
    }
    #endregion
    #region vDirectPurchaseReturnHd
    public partial class vDirectPurchaseReturnHd
    {
        public string ReturnDateInString
        {
            get
            {
                return _ReturnDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }

        public string ReferenceDateInString
        {
            get
            {
                return _ReferenceDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
    }
    #endregion
    #region vGLTransactionDt
    public partial class vGLTransactionDt
    {
        public Boolean IsAllowEditItem
        {
            get
            {
                return (_GCItemDetailStatus == Constant.TransactionStatus.OPEN);
            }
        }
    }
    #endregion
    #region vItemBalance
    public partial class vItemBalance
    {
        public String CustomMinimum
        {
            get { return string.Format("{0:N} {1}", QuantityMIN, _ItemUnit); }
        }

        public String CustomMaximum
        {
            get { return string.Format("{0:N} {1}", QuantityMAX, _ItemUnit); }
        }
        public String CustomEndingBalance
        {
            get { return string.Format("{0:N} {1}", QuantityEND, _ItemUnit); }
        }
    }
    #endregion
    #region vItemBalanceInventory
    public partial class vItemBalanceInventory
    {
        public String CustomMinimum
        {
            get { return string.Format("{0:N} {1}", QuantityMIN, _ItemUnit); }
        }

        public String CustomMaximum
        {
            get { return string.Format("{0:N} {1}", QuantityMAX, _ItemUnit); }
        }
        public String CustomEndingBalance
        {
            get { return string.Format("{0:N} {1}", QuantityEND, _ItemUnit); }
        }
        public String CustomQtyOnOrderItemRequest
        {
            get { return string.Format("{0:N} {1}", ItemRequestQtyOnOrder, _ItemUnit); }
        }
        public String CustomQtyOnOrderPurchaseRequest
        {
            get { return string.Format("{0:N} {1}", PurchaseRequestQtyOnOrder, _ItemUnit); }
        }
        public String CustomQtyOnOrderPurchaseOrder
        {
            get { return string.Format("{0:N} {1}", PurchaseOrderQtyOnOrder, _ItemUnit); }
        }
        public String CustomQtyOnOrderItemDistribution
        {
            get { return string.Format("{0:N} {1}", ItemDistributionQtyOnOrder, _ItemUnit); }
        }
    }
    #endregion
    #region vItemCost
    public partial class vItemCost
    {
        public String cfTotalMaterial
        {
            get { return String.Format("{0:N2}", _TotalMaterial); }
        }

        public String cfTotalLabor
        {
            get { return String.Format("{0:N2}", _TotalLabor); }
        }

        public String cfTotalOverhead
        {
            get { return String.Format("{0:N2}", _TotalOverhead); }
        }

        public String cfTotalSubContract
        {
            get { return String.Format("{0:N2}", _TotalSubContract); }
        }

        public String cfTotalBurden
        {
            get { return String.Format("{0:N2}", _TotalBurden); }
        }
    }
    #endregion
    #region vItemDistributionDt
    public partial class vItemDistributionDt
    {
        public String DeliveryDateInString
        {
            get { return _DeliveryDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }

        public Boolean IsAllowEditItem
        {
            get
            {
                return (_GCItemDetailStatus == Constant.DistributionStatus.OPEN);
            }
        }

        public String CustomItemUnit
        {
            get
            {
                return _Quantity + " " + _ItemUnit;
            }
        }

        public String CustomConversion
        {
            get
            {
                return "1.00 " + _ItemUnit + " = " + ConversionFactor + " " + _BaseUnit;
            }
        }

        public decimal CustomTotal
        {
            get
            {
                return (_Quantity * _ConversionFactor);
            }
        }

        public String CustomItemDistribution
        {
            get
            {
                return string.Format("{0:N} {1}", CustomTotal, _BaseUnit);
            }
        }
    }
    #endregion
    #region vItemDistributionHd
    public partial class vItemDistributionHd
    {
        public string DeliveryDateInString
        {
            get
            {
                return _DeliveryDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }

        public string DeliveryDateTimeInString
        {
            get
            {
                return _DeliveryDate.ToString(Constant.FormatString.DATE_FORMAT) + " " + _DeliveryTime;
            }
        }
    }
    #endregion
    #region vItemMovement
    public partial class vItemMovement
    {
        public string MovementDateInString
        {
            get
            {
                return _MovementDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public string SupplierName
        {
            get
            {
                return DetailDesc.Split(new String[] { " PO:", " :" }, StringSplitOptions.None)[0];
            }
        }
    }
    #endregion
    #region vItemRequestDt
    public partial class vItemRequestDt
    {
        public Boolean IsAllowEditItem
        {
            get
            {
                return (_GCItemDetailStatus == Constant.TransactionStatus.OPEN);
            }
        }
        public String CustomEndingBalance
        {
            get
            {
                if (_EndingBalance == null) return 0 + " " + _BaseUnit;
                else return _EndingBalance + " " + _BaseUnit;
            }
        }

        public String CustomItemUnit
        {
            get
            {
                return _Quantity + " " + _ItemUnit;
            }
        }

        public String CustomConversion
        {
            get
            {
                return "1.00 " + _ItemUnit + " = " + ConversionFactor + " " + _BaseUnit;
            }
        }

        public decimal CustomTotal
        {
            get
            {
                return (_Quantity * _ConversionFactor);
            }
        }

        public String CustomItemRequest
        {
            get
            {
                return string.Format("{0:N} {1}", CustomTotal, _BaseUnit);
            }
        }
    }
    #endregion
    #region vItemRequestHd
    public partial class vItemRequestHd
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
    #region vItemTransactionDt
    public partial class vItemTransactionDt
    {
        public Boolean IsAllowEditItem
        {
            get
            {
                return (_GCItemDetailStatus == Constant.TransactionStatus.OPEN);
            }
        }
        public String Conversion
        {
            get { return string.Format("1 {0} = {1} {2}", _BaseUnit, _ConversionFactor, _ItemUnit); }
        }
        public String CustomItemUnit
        {
            get
            {
                return _Quantity + " " + _ItemUnit;
            }
        }
        public String TransactionDateInString
        {
            get { return _TransactionDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
    }
    #endregion
    #region vItemTransactionHd
    public partial class vItemTransactionHd
    {
        public String TransactionDateInString
        {
            get { return _TransactionDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
    }
    #endregion
    #region vPeriodAdmission
    public partial class vPeriodAdmission
    {
        public string StartDateInString
        {
            get
            {
                return _StartDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public string EndDateInString
        {
            get
            {
                return _EndDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public string RegistrationStartDateInString
        {
            get
            {
                return _RegistrationStartDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public string RegistrationEndDateInString
        {
            get
            {
                return _RegistrationEndDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public string StartDateInDatePickerFormat
        {
            get
            {
                return _StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            }
        }
        public string EndDateInDatePickerFormat
        {
            get
            {
                return _EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            }
        }
        public string RegistrationStartDateInDatePickerFormat
        {
            get
            {
                return _RegistrationStartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            }
        }
        public string RegistrationEndDateInDatePickerFormat
        {
            get
            {
                return _RegistrationEndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            }
        }
    }
    #endregion
    #region vPeriodClassType
    public partial class vPeriodClassType
    {
        public bool IsAllowEditItem
        {
            get
            {
                return _CreatedClass < 1;
            }
        }
    }
    #endregion
    #region vPeriodSchedule
    public partial class vPeriodSchedule
    {
        public string StartDateInString
        {
            get
            {
                return _StartDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public string EndDateInString
        {
            get
            {
                return _EndDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public string StartDateInDatePickerFormat
        {
            get
            {
                return _StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            }
        }
        public string EndDateInDatePickerFormat
        {
            get
            {
                return _EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            }
        }
        public string cfGCPeriodScheduleType
        {
            get { return _GCPeriodScheduleType.Split('^')[1]; }
        }
    }
    #endregion
    #region vPeriodSection
    public partial class vPeriodSection
    {
        public string StartDateInString
        {
            get
            {
                return _StartDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public string EndDateInString
        {
            get
            {
                return _EndDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public string StartDateInDatePickerFormat
        {
            get
            {
                return _StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            }
        }
        public string EndDateInDatePickerFormat
        {
            get
            {
                return _EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            }
        }
    }
    #endregion
    #region vProspectiveStudent
    public partial class vProspectiveStudent
    {
        public int AgeInYear
        {
            get
            {
                return Function.GetPatientAgeInYear(_DateOfBirth, DateTime.Now);
            }
        }
        public int AgeInMonth
        {
            get
            {
                return Function.GetPatientAgeInMonth(_DateOfBirth, DateTime.Now);
            }
        }
        public int AgeInDay
        {
            get
            {
                return Function.GetPatientAgeInDay(_DateOfBirth, DateTime.Now);
            }
        }
    }
    #endregion
    #region vProspectiveStudentFamily
    public partial class vProspectiveStudentFamily
    {
        public string DateOfBirthInDatePickerFormat
        {
            get { return _DateOfBirth.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }
    }
    #endregion
    #region vPurchaseInvoiceDt
    public partial class vPurchaseInvoiceDt
    {
        public String ReferenceDateInString
        {
            get { return _ReferenceDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }

        public String PurchaseInvoiceDateInString
        {
            get { return _PurchaseInvoiceDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }

        public String PaymentDueDateInString
        {
            get { return _PaymentDueDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
    }
    #endregion
    #region vPurchaseInvoiceHd
    public partial class vPurchaseInvoiceHd
    {
        public string PInvoiceDateInString
        {
            get
            {
                return _PurchaseInvoiceDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }

        public string DueDateInString
        {
            get
            {
                return _DueDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public Decimal CustomSisaHutang
        {
            get
            {
                Decimal sisa = _TotalNetTransactionAmount - _PaymentAmount;
                return sisa;
            }
        }
        public int CustomUmur
        {
            get
            {
                return Function.GetPatientAgeInDay(_DueDate, DateTime.Today);
            }
        }
    }
    #endregion
    #region vPurchaseInvoiceHdPayment
    public partial class vPurchaseInvoiceHdPayment
    {
        public string DueDateInString
        {
            get
            {
                return _DueDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }

        //public Decimal CustomTotalHutang
        //{
        //    get
        //    {
        //        //masih belum dihitung PPH
        //        Decimal total1 = (_TotalTransactionAmount - (_TotalDownPaymentAmount + TotalCreditNoteAmount + _FinalDiscount)) * ((100 + _VATPercentage) / 100);
        //        Decimal total = total1 - _StampAmount - _ChargesAmount;
        //        return total;
        //    }
        //}

        public Decimal CustomTotalHutang
        {
            get
            {
                //masih belum dihitung PPH
                Decimal FinalDiscount = (_FinalDiscount / 100) * _TotalTransactionAmount;
                Decimal total1 = (_TotalTransactionAmount - (_TotalDownPaymentAmount + TotalCreditNoteAmount + FinalDiscount));
                Decimal pph = (_PPHPercentage / 100) * total1;
                Decimal total2 = total1 * ((100 + _VATPercentage) / 100);
                Decimal total = total2 - pph - _StampAmount - _ChargesAmount;
                return total;
            }
        }

        public Decimal CustomSisaHutang
        {
            get
            {
                Decimal sisa = CustomTotalHutang - _PaymentAmount;
                return sisa;
            }
        }

        public Decimal VATAmount
        {
            get
            {
                //masih belum dihitung PPH
                Decimal FinalDiscount = (_FinalDiscount / 100) * _TotalTransactionAmount;
                Decimal total1 = (_TotalTransactionAmount - (_TotalDownPaymentAmount + TotalCreditNoteAmount + FinalDiscount));
                Decimal vat = (_VATPercentage / 100) * total1;
                return vat;
            }
        }

        public Decimal PPHAmount
        {
            get
            {
                //masih belum dihitung PPH
                Decimal FinalDiscount = (_FinalDiscount / 100) * _TotalTransactionAmount;
                Decimal total1 = (_TotalTransactionAmount - (_TotalDownPaymentAmount + TotalCreditNoteAmount + FinalDiscount));
                Decimal pph = (_PPHPercentage / 100) * total1;
                return pph;
            }
        }
    }
    #endregion
    #region vPurchaseOrderDt
    public partial class vPurchaseOrderDt
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

        public Decimal CustomTotal
        {
            get
            {
                return _Quantity * _ConversionFactor;
            }
        }

        public Boolean IsAllowEditItem
        {
            get
            {
                return (_GCItemDetailStatus == Constant.TransactionStatus.OPEN);
            }
        }

        public String CustomConversion
        {
            get
            {
                return "1.00 " + _PurchaseUnit + " = " + ConversionFactor + " " + _BaseUnit;
            }
        }

        public String CustomPurchaseUnit
        {
            get
            {
                return _Quantity + " " + _PurchaseUnit;
            }
        }

        public String CustomUnitPrice
        {
            get
            {
                return UnitPrice.ToString("N2") + " / " + _PurchaseUnit;
            }
        }

        public String CustomQtyRemaining
        {
            get
            {
                return string.Format("{0:N}", (_Quantity - _ReceivedQuantity));
            }
        }

        public String OrderDateInString
        {
            get { return _OrderDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }

        public Boolean IsReceived
        {
            get { return _ReceivedInformation != "" ? true : false; }
        }
    }
    #endregion
    #region vPurchaseOrderDtOutStanding
    public partial class vPurchaseOrderDtOutStanding
    {
        public Decimal CustomSubTotal
        {
            get
            {
                Decimal totalAfterDisc1 = (Quantity * UnitPrice * ConversionFactor) - ((Quantity * UnitPrice * ConversionFactor) *
               _DiscountPercentage1 / 100);
                Decimal totalAfterDisc2 = totalAfterDisc1 - (_DiscountPercentage2 / 100 * totalAfterDisc1);
                return totalAfterDisc2;
            }
        }

        public Decimal CustomTotalDiscount
        {
            get
            {
                return (Quantity * UnitPrice * ConversionFactor) - CustomSubTotal;
            }
        }
        public Boolean IsAllowEditItem
        {
            get
            {
                return (_GCItemDetailStatus == Constant.TransactionStatus.OPEN);
            }
        }

        public String CustomUnitPrice
        {
            get
            {
                return UnitPrice.ToString("N2") + " / " + _BaseUnit;
            }
        }
    }
    #endregion
    #region vPurchaseOrderHd
    public partial class vPurchaseOrderHd
    {
        public string OrderDateInString
        {
            get
            {
                return _OrderDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }

        public string DeliveryDateInString
        {
            get
            {
                return _DeliveryDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }

        public string ExpiredDateInString
        {
            get
            {
                return _POExpiredDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }

        public Decimal cfTransactionAmount
        {
            get
            {
                decimal finalDisc = (_TransactionAmount * _FinalDiscount / 100);
                decimal PPN = (_VATPercentage / 100) * (_TransactionAmount - finalDisc);
                decimal total = _TransactionAmount - finalDisc + PPN - _DownPaymentAmount;
                return total;
            }
        }
    }
    #endregion
    #region vPurchaseReceiveCredit
    public partial class vPurchaseReceiveCredit
    {
        public Decimal CustomTotal
        {
            get
            {
                Decimal totalReceive = ((_TransactionAmount - _FinalDiscount) * ((100 + _VATPercentage) / 100)) - _ChargesAmount + _StampAmount;
                return totalReceive;
            }
        }
        public Decimal CustomSubTotal
        {
            get
            {
                Decimal subTotal = CustomTotal - _DownPaymentAmount - _CNAmount;
                return subTotal;
            }
        }
        public Decimal VATAmount
        {
            get
            {
                Decimal VATAmount = ((_TransactionAmount - _FinalDiscount) * (_VATPercentage / 100));
                return VATAmount;
            }
        }
    }
    #endregion
    #region vPurchaseReceiveDt
    public partial class vPurchaseReceiveDt
    {
        public string ReceivedDateInString
        {
            get { return _ReceivedDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }

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

        public Decimal CustomTotalDiscount
        {
            get
            {
                return (Quantity * UnitPrice) - CustomSubTotal;
            }
        }
        public Boolean IsAllowEditItem
        {
            get
            {
                return (_GCItemDetailStatus == Constant.TransactionStatus.OPEN);
            }
        }

        public String CustomUnitPrice
        {
            get
            {
                return UnitPrice.ToString("N2") + " / " + _ItemUnit;
            }
        }

        public String CustomConversion
        {
            get
            {
                if (_ItemUnit != _BaseUnit)
                    return "1.00 " + _ItemUnit + " = " + ConversionFactor + " " + _BaseUnit;
                else
                    return string.Empty;
            }
        }

        public Decimal DiscountAmount1
        {
            get { return (_UnitPrice * Quantity) * DiscountPercentage1 / 100; }
        }

        public Decimal DiscountAmount2
        {
            get { return ((_UnitPrice * Quantity) - DiscountAmount1) * DiscountPercentage2 / 100; }
        }

        public Boolean isConfirmed
        {
            get
            {
                return _GCItemDetailStatus == "X121^002" ? true : false;
            }
        }
    }
    #endregion
    #region vPurchaseReceiveHd
    public partial class vPurchaseReceiveHd
    {
        public string ReceivedDateInString
        {
            get
            {
                return _ReceivedDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public decimal NetTransactionAmount
        {
            get
            {
                return _TransactionAmount - _DiscountAmount;
            }
        }

        public string PaymentDueDateInString
        {
            get
            {
                return _PaymentDueDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
    }
    #endregion
    #region vPurchaseReplacementDt
    public partial class vPurchaseReplacementDt
    {
        public Boolean IsAllowEditItem
        {
            get
            {
                return (_GCItemDetailStatus == Constant.TransactionStatus.OPEN);
            }
        }

        public String CustomQuantityItemUnit
        {
            get
            {
                return string.Format("{0} {1}", _Quantity, _ItemUnit);
            }
        }
        public String CustomFromQuantityItemUnit
        {
            get
            {
                return string.Format("{0} {1}", _FromQuantity, _FromItemUnit);
            }
        }
        public String CustomConversion
        {
            get
            {
                return "1.00 " + _ItemUnit + " = " + ConversionFactor + " " + _BaseUnit;
            }
        }
    }
    #endregion
    #region vPurchaseReplacementHd
    public partial class vPurchaseReplacementHd
    {
        public string ReplacementDateInString
        {
            get
            {
                return _ReplacementDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }

        public string RefferenceDateInString
        {
            get
            {
                return _ReferenceDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
    }
    #endregion
    #region vPurchaseRequestDt
    public partial class vPurchaseRequestDt
    {
        public Boolean IsAllowEditItem
        {
            get
            {
                return (_GCItemDetailStatus == Constant.TransactionStatus.OPEN);
            }
        }

        public Boolean IsApproved
        {
            get { return _GCItemDetailStatus == Constant.TransactionStatus.APPROVED; }
        }

        public String CustomEndingBalance
        {
            get
            {
                return _EndingBalance / _ConversionFactor + " " + _PurchaseUnit;
            }
        }
        public String CustomConversion
        {
            get
            {
                return "1.00 " + _PurchaseUnit + " = " + ConversionFactor + " " + _BaseUnit;
            }
        }

        public String CustomPurchaseUnit
        {
            get
            {
                return _Quantity + " " + _PurchaseUnit;
            }
        }

        public String CustomUnitPrice
        {
            get
            {
                return UnitPrice.ToString("N2") + " / " + _PurchaseUnit;
            }
        }

        public decimal CustomTotal
        {
            get
            {
                return _Quantity * _ConversionFactor;
            }
        }

        public decimal CustomTotalPrice
        {
            get
            {
                return _Quantity * _UnitPrice;
            }
        }

        public String CustomPurchaseRequest
        {
            get
            {
                return string.Format("{0:N} {1}", CustomTotal, _BaseUnit);
            }
        }

        public string TransactionDateInString
        {
            get
            {
                return _TransactionDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }

        public string ItemNameCatalog
        {
            get
            {
                if (_SupplierItemName != "")
                    return string.Format("{0} / {1}", _ItemName1, _SupplierItemName);
                return _ItemName1;
            }
        }
    }
    #endregion
    #region vPurchaseRequestDtOutstanding
    public partial class vPurchaseRequestDtOutstanding
    {
        public String cfSupplierItem
        {
            get
            {
                if (_SupplierItemName != "" && _SupplierItemCode != "")
                    return string.Format("{0} ({1})", _SupplierItemName, _SupplierItemCode);
                if (_SupplierItemName != "")
                    return _SupplierItemName;
                return _SupplierItemCode;
            }
        }
        public String CustomQtyOnOrder
        {
            get
            {
                return string.Format("{0:N} {1}", _QtyOnOrder, _BaseUnit);
            }
        }
        public Boolean IsAllowEditItem
        {
            get
            {
                return (_GCItemDetailStatus == Constant.TransactionStatus.OPEN);
            }
        }
        public String CustomEndingBalance
        {
            get
            {
                if (_QuantityEND == null) return 0 + " " + _BaseUnit;
                else return _QuantityEND + " " + _BaseUnit;
            }
        }
        public String CustomConversion
        {
            get
            {
                if (!_PurchaseUnit.Equals(_BaseUnit))
                    return "1.00 " + _PurchaseUnit + " = " + ConversionFactor + " " + _BaseUnit;
                else
                    return string.Empty;
            }
        }

        public String CustomPurchaseUnit
        {
            get
            {
                return _Quantity + " " + _PurchaseUnit;
            }
        }

        public String CustomUnitPrice
        {
            get
            {
                return UnitPrice + " / " + _BaseUnit;
            }
        }

        public decimal CustomTotal
        {
            get
            {
                return _Quantity * _ConversionFactor;
            }
        }

        public decimal CustomTotalPrice
        {
            get
            {
                return _Quantity * _UnitPrice;
            }
        }

        public String CustomPurchaseRequest
        {
            get
            {
                return string.Format("{0:N} {1}", CustomTotal, _BaseUnit);
            }
        }
    }
    #endregion
    #region vPurchaseRequestHd
    public partial class vPurchaseRequestHd
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
    #region vPurchaseReturnDt
    public partial class vPurchaseReturnDt
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

        public Decimal CustomTotalDiscount
        {
            get
            {
                return (Quantity * UnitPrice) - CustomSubTotal;
            }
        }

        public Decimal Discount
        {
            get
            {
                return Price * _DiscountPercentage1 / 100;
            }
        }
        public Decimal Price
        {
            get
            {
                return (Quantity * UnitPrice * ConversionFactor);
            }
        }

        public Boolean IsAllowEditItem
        {
            get
            {
                return (_GCItemDetailStatus == Constant.TransactionStatus.OPEN);
            }
        }

        public String CustomUnitPrice
        {
            get
            {
                return UnitPrice.ToString("N2") + " / " + _ItemUnit;
            }
        }

        public String CustomQuantityItemUnit
        {
            get
            {
                return string.Format("{0} {1}", _Quantity, _ItemUnit);
            }
        }

        public String ReturnDateInString
        {
            get { return _ReturnDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
    }
    #endregion
    #region vPurchaseReturnHd
    public partial class vPurchaseReturnHd
    {
        public string ReturnDateInString
        {
            get
            {
                return _ReturnDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }

        public string RefferenceDateInString
        {
            get
            {
                return _ReferenceDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
    }
    #endregion
    #region vRegistration
    public partial class vRegistration
    {
        public int AgeInYear
        {
            get
            {
                return Function.GetPatientAgeInYear(_DateOfBirth, DateTime.Now);
            }
        }
        public int AgeInMonth
        {
            get
            {
                return Function.GetPatientAgeInMonth(_DateOfBirth, DateTime.Now);
            }
        }
        public int AgeInDay
        {
            get
            {
                return Function.GetPatientAgeInDay(_DateOfBirth, DateTime.Now);
            }
        }
    }
    #endregion
    #region vStockTakingHd
    public partial class vStockTakingHd
    {
        public string FormDateInString
        {
            get
            {
                return _FormDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
    }
    #endregion
    #region vStudent
    public partial class vStudent
    {
        public int AgeInYear
        {
            get
            {
                return Function.GetPatientAgeInYear(_DateOfBirth, DateTime.Now);
            }
        }
        public int AgeInMonth
        {
            get
            {
                return Function.GetPatientAgeInMonth(_DateOfBirth, DateTime.Now);
            }
        }
        public int AgeInDay
        {
            get
            {
                return Function.GetPatientAgeInDay(_DateOfBirth, DateTime.Now);
            }
        }
    }
    #endregion
    #region vSupplier
    public partial class vSupplier
    {
        public String Address
        {
            get
            {
                return Function.GenerateAddress(_StreetName, _County, _District, _City, _State);
            }
        }
        public String cfPhoneNo
        {
            get
            {
                StringBuilder result = new StringBuilder();

                if (_PhoneNo1 != "")
                    result.Append(_PhoneNo1);
                if (_PhoneNo2 != "")
                {
                    if (result.ToString() != "")
                        result.Append(" / ");
                    result.Append(_PhoneNo2);
                }
                return result.ToString();
            }
        }
    }
    #endregion
    #region vSupplierCreditNote
    public partial class vSupplierCreditNote
    {
        public string CreditNoteDateInString
        {
            get
            {
                return _CreditNoteDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }

        public Decimal VATAmount
        {
            get { return _CNAmount * _VATPercentage / 100; }
        }

        public String TotalInString
        {
            get { return Function.NumberInWords(Convert.ToInt32(_CNAmount + VATAmount), true); }
        }
    }
    #endregion
    #region vSupplierPaymentHd
    public partial class vSupplierPaymentHd
    {
        public string PaymentDateInString
        {
            get
            {
                return _PaymentDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public Decimal PaymentAmountHd
        {
            get
            {
                List<SupplierPaymentDt> lst = BusinessLayer.GetSupplierPaymentDtList(string.Format("SupplierPaymentID = {0}", _SupplierPaymentID));
                return lst.Sum(p => p.PaymentAmount);
            }
        }
    }
    #endregion
}
