using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using CodeX.Common;
using System.Net;

namespace CodeX.Data.Model
{
    #region vAdmissionFeeRuleDtCustom
    public partial class vAdmissionFeeRuleDtCustom
    {
        public Decimal TotalPaymentAmount
        {
            get { return _TotalAmount * _NoOfRegistrationPaymentPeriod; }
        }
    }
    #endregion
    #region vAPMovement
    public partial class vAPMovement
    {
        public String MovementDateInString
        {
            get { return _MovementDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
    }
    #endregion
    #region vARInvoiceDt
    public partial class vARInvoiceDt
    {
        public Boolean IsProcessed
        {
            get { return _GCTransactionStatus == Constant.TransactionStatus.PROCESSED; }
        }
        public String DueDateInString 
        {
            get { return _DueDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
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
    }
    #endregion
    #region vARInvoiceHd
    public partial class vARInvoiceHd
    {
        public String ARInvoiceDateInString
        {
            get
            {
                return _ARInvoiceDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }

        public String DueDateInString
        {
            get
            {
                return _DueDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
    }
    #endregion
    #region vARMovement
    public partial class vARMovement
    {
        public String MovementDateInString
        {
            get { return _MovementDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
    }
    #endregion
    #region vARReceivingDt
    public partial class vARReceivingDt
    {
        public String CardNumber4
        {
            get
            {
                string[] temp = _CardNumber.Split('-');
                if (temp.Count() > 3)
                    return temp[3];
                return "";
            }
        }
        public Decimal LineTotal
        {
            get
            {
                return (_PaymentAmount + _CardFeeAmount);
            }
        }
    }
    #endregion
    #region vARReceivingHd
    public partial class vARReceivingHd
    {
        public string ReceivingDateInString
        {
            get { return _ReceivingDate.ToString(Constant.FormatString.DATE_FORMAT); }
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
    #region vClassStudent
    public partial class vClassStudent
    {
        public String StudentImageUrl { get { return Function.GenerateStudentPictureFileName(_PictureFileName, _StudentCode); } }
    }
    #endregion
    #region vClassSubject
    public partial class vClassSubject
    {
        public Boolean IsMainTeacher { get { return _ParentID == 0; } }
    }
    #endregion
    #region vCurriculumClassType
    public partial class vCurriculumClassType
    {
        public Boolean IsExtracurricular
        {
            get
            {
                return _GCClassStudyType == Constant.ClassStudyType.EXTRACURRICULAR;
            }
        }
    }
    #endregion
    #region vCurriculumMarkTypeDt
    public partial class vCurriculumMarkTypeDt
    {
        public String cfCurriculumMarkTypeDtName
        {
            get
            {
                return string.Format("{0} - {1}", _CurriculumMarkTypeName, _CurriculumMarkTypeDtName);
            }
        }
    }
    #endregion
    #region vFADepreciation
    public partial class vFADepreciation
    {
        public String DepreciationDateInString
        {
            get { return _DepreciationDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
    }
    #endregion
    #region vDirectPaymentDt
    public partial class vDirectPaymentDt
    {
        public String CardNumber4
        {
            get
            {
                string[] temp = _CardNumber.Split('-');
                if (temp.Count() > 3)
                    return temp[3];
                return "";
            }
        }
        public Decimal LineTotal
        {
            get { return _PaymentAmount + _CardFeeAmount; }
        }
    }
    #endregion
    #region vDirectPurchaseDt
    public partial class vDirectPurchaseDt
    {
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
        public String PurchaseDateInString
        {
            get { return _PurchaseDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
        public String cfTransactionHeaderInformation
        {
            get
            {
                return String.Format("{0} Supplier : {1}", _DirectPurchaseNo, _BusinessPartnerName);
            }
        }
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
    #region vEmployee
    public partial class vEmployee
    {

        public String HomeAddress
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
    #region vFAItem
    public partial class vFAItem
    {
        public String ProcurementDateInString
        {
            get { return _ProcurementDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
        public String DepreciationStartDateInString 
        {
            get { return _DepreciationStartDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
    }
    #endregion
    #region vFAItemMovement
    public partial class vFAItemMovement
    {
        public String MovementDateInString
        {
            get { return _MovementDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
        public String MovementDateInDatePickerFormat
        {
            get { return _MovementDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }

        private bool _IsAllowEditItem = false;
        public bool IsAllowEditItem
        {
            get { return _IsAllowEditItem; }
            set { _IsAllowEditItem = value; }
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

        public String JournalDateInMonth
        {
            get 
            {
                return _JournalDate.ToString("MMMM yyyy");
            }
        }
    }
    #endregion
    #region vGLTransactionDtCustom
    public partial class vGLTransactionDtCustom
    {
        public string JournalDateInString
        {
            get { return _JournalDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }

        public String AccountForJournalVoucher
        {
            get
            {
                string text = _GLAccountName;
                if (_SubLedgerName != null && _SubLedgerName != "") text += "-" + _SubLedgerName;
                text += " " + _Remarks;
                return text;
            }
        }
    }
    #endregion
    #region vGLTransactionHd
    public partial class vGLTransactionHd
    {
        public string JournalDateInString
        {
            get { return _JournalDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
        public string LastUpdatedDateInString
        {
            get { return _LastUpdatedDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }

        public string JournalDateInMonth
        {
            get { return _JournalDate.ToString("MMMM yyyy"); }
        }

        public Decimal Selisih
        {
            get { return _DebitAmount - _CreditAmount; }
        }
    }
    #endregion
    #region vItemAlternateUnitCustom
    public partial class vItemAlternateUnitCustom
    {
        public String CustomConversion
        {
            get
            {
                return "1.00 " + _AlternateUnit + " = " + ConversionFactor + " " + _ItemUnit;
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
        public String cfTransactionHeaderInformation
        {
            get
            {
                if (_HeaderRemarks != "")
                    return String.Format("{0} Bagian : {1} ({2})", _TransactionNo, _ServiceUnitName, _HeaderRemarks);
                return String.Format("{0} Bagian : {1}", _TransactionNo, _ServiceUnitName);
            }
        }
        public String cfTransactionHeaderInformation2
        {
            get
            {
                if (_HeaderRemarks != "")
                    return String.Format("{0} Keterangan : {1}", _TransactionNo, _HeaderRemarks);
                return String.Format("{0}", _TransactionNo);
            }
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
    #region vJournalTemplateDt
    public partial class vJournalTemplateDt
    {
        public string cfPosition
        {
            get
            {
                if (_Position == "D") return "Debit";
                return "Kredit";
            }
        }
    }
    #endregion
    #region vJournalTemplateHd
    public partial class vJournalTemplateHd
    {
        public String StatusDK
        {
            get
            {
                String result = "";
                if (_TotalDebit < 100 && _TotalKredit < 100) result = String.Format("Debit dan Kredit tidak balance");
                else if (_TotalDebit < 100) result = String.Format("Debit tidak balance");
                else if (_TotalKredit < 100) result = String.Format("Kredit tidak balance");
                return result;
            }
        }
    }
    #endregion
    #region vMarkTypeFormula
    public partial class vMarkTypeFormula
    {
        public string cfFromValue
        {
            get
            {
                if (_FromGCMarkType == Constant.MarkType.OPTION)
                    return _FromMarkTypeDtName;
                return string.Format("{0} - {1}", _MinValue, _MaxValue);
            }
        }
    }
    #endregion
    #region vMarkTypeHd
    public partial class vMarkTypeHd
    {
        public bool IsOption
        {
            get
            {
                return _GCMarkType == Constant.MarkType.OPTION;
            }
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
    #region vPeriodScheduleClassType
    public partial class vPeriodScheduleClassType
    {
        public string StartDateInDatePickerFormat
        {
            get { return _StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }
        public string EndDateInDatePickerFormat
        {
            get { return _EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
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
        public String DateOfBirthInString
        {
            get { return _DateOfBirth.ToString("dd-MMM-yyyy"); }
        }
        public string StudentAge
        {
            get
            {
                string result;
                int ageInYear, ageInMonth, ageInDay = 0;

                ageInYear = Function.GetPatientAgeInYear(DateOfBirth, DateTime.Now);
                ageInMonth = Function.GetPatientAgeInMonth(DateOfBirth, DateTime.Now);
                ageInDay = Function.GetPatientAgeInDay(DateOfBirth, DateTime.Now);

                if (ageInYear > 0)
                    result = string.Format("{0}yr", ageInYear);
                else if (ageInMonth > 0)
                    result = string.Format("{0}mo", ageInMonth);
                else
                    result = string.Format("{0}day", ageInDay);

                return result;
            }
        }
        public String HomeAddress
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
                //if (_PhoneNo2 != "")
                //{
                //    if (result.ToString() != "")
                //        result.Append(" / ");
                //    result.Append(_PhoneNo2);
                //}
                return result.ToString();
            }
        }
        public String ProspectiveStudentImageUrl { get { return Function.GenerateStudentPictureFileName(_PictureFileName, _ProspectiveStudentCode); } }
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
    #region vProspectiveStudentAchievement
    public partial class vProspectiveStudentAchievement
    {
        public string AchievementDateInDatePickerFormat
        {
            get { return _AchievementDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }
        public string AchievementDateInString
        {
            get { return _AchievementDate.ToString(Constant.FormatString.DATE_FORMAT); }
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
        public String CustomSupplierItem
        {
            get
            {
                if (_SupplierItemCode != "")
                {
                    if (_SupplierItemName != "")
                        return string.Format("{0} ({1})", _SupplierItemName, _SupplierItemCode);
                    return _SupplierItemCode;
                }
                return _SupplierItemName;
            }
        }

        public String CustomTotalPurchaseUnit
        {
            get
            {
                return (_Quantity * _ConversionFactor).ToString("#,##0.00") + " " + _BaseUnit;
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
    }
    #endregion
    #region vPurchaseReceiveCredit
    public partial class vPurchaseReceiveCredit
    {
        public Decimal CustomSubTotal
        {
            get
            {
                Decimal subTotal = _TotalNetTransactionAmount - _CNAmount;
                return subTotal;
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
                if (_ItemUnit != _BaseUnit)
                    return "1.00 " + _ItemUnit + " = " + ConversionFactor + " " + _BaseUnit;
                else
                    return string.Empty;
            }
        }

        public Decimal DiscountAmount
        {
            get { return _DiscountAmount1 + _DiscountAmount2; }
        }

        public Boolean isConfirmed
        {
            get
            {
                return _GCItemDetailStatus == "X121^002" ? true : false;
            }
        }
        public String cfTransactionHeaderInformation
        {
            get
            {
                return String.Format("{0} Supplier : {1}", _PurchaseReceiveNo, _SupplierName);
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
        public String CustomQuantityItemUnit
        {
            get
            {
                return string.Format("{0} {1}", _Quantity, _ItemUnit);
            }
        }
        public Decimal DiscountAmount
        {
            get
            {
                return _DiscountAmount1 + _DiscountAmount2;
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
        public String DateOfBirthInString
        {
            get { return _DateOfBirth.ToString("dd-MMM-yyyy"); }
        }
        public String SchoolDateInDatePickerFormat
        {
            get { return _SchoolDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }
        public string StudentAge
        {
            get
            {
                string result;
                int ageInYear, ageInMonth, ageInDay = 0;

                ageInYear = Function.GetPatientAgeInYear(DateOfBirth, DateTime.Now);
                ageInMonth = Function.GetPatientAgeInMonth(DateOfBirth, DateTime.Now);
                ageInDay = Function.GetPatientAgeInDay(DateOfBirth, DateTime.Now);

                if (ageInYear > 0)
                    result = string.Format("{0}yr", ageInYear);
                else if (ageInMonth > 0)
                    result = string.Format("{0}mo", ageInMonth);
                else
                    result = string.Format("{0}day", ageInDay);

                return result;
            }
        }
        public String HomeAddress
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
                //if (_PhoneNo2 != "")
                //{
                //    if (result.ToString() != "")
                //        result.Append(" / ");
                //    result.Append(_PhoneNo2);
                //}
                return result.ToString();
            }
        }
        public String ProspectiveStudentImageUrl { get { return Function.GenerateStudentPictureFileName(_PictureFileName, _ProspectiveStudentCode); } }
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
    #region vRegistrationInvoice
    public partial class vRegistrationInvoice
    {
        public Decimal RemainingAmount
        {
            get { return _TotalClaimedAmount - _TotalPaymentAmount; }
        }
        public String DateOfBirthInString
        {
            get { return _DateOfBirth.ToString("dd-MMM-yyyy"); }
        }
        public string StudentAge
        {
            get
            {
                string result;
                int ageInYear, ageInMonth, ageInDay = 0;

                ageInYear = Function.GetPatientAgeInYear(DateOfBirth, DateTime.Now);
                ageInMonth = Function.GetPatientAgeInMonth(DateOfBirth, DateTime.Now);
                ageInDay = Function.GetPatientAgeInDay(DateOfBirth, DateTime.Now);

                if (ageInYear > 0)
                    result = string.Format("{0}yr", ageInYear);
                else if (ageInMonth > 0)
                    result = string.Format("{0}mo", ageInMonth);
                else
                    result = string.Format("{0}day", ageInDay);

                return result;
            }
        }
        public String HomeAddress
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
                //if (_PhoneNo2 != "")
                //{
                //    if (result.ToString() != "")
                //        result.Append(" / ");
                //    result.Append(_PhoneNo2);
                //}
                return result.ToString();
            }
        }
        public String ProspectiveStudentImageUrl { get { return Function.GenerateStudentPictureFileName(_PictureFileName, _ProspectiveStudentCode); } }
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
    #region vSalesInvoiceDt
    public partial class vSalesInvoiceDt
    {
        public Decimal CustomSubTotal
        {
            get
            {
                return Math.Round(_LineAmount * (100 + _VATPercentage) / 100);
            }
        }

        public Decimal UnitPriceAfterVAT
        {
            get
            {
                return Math.Round(_UnitPrice * (100 + _VATPercentage) / 100);
            }
        }

        public Decimal CustomTotalDiscount
        {
            get
            {
                return (Quantity * UnitPriceAfterVAT * ConversionFactor) - CustomSubTotal;
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

        public String CustomConversion
        {
            get
            {
                return "1.00 " + _ItemUnit + " = " + ConversionFactor + " " + _BaseUnit;
            }
        }
    }
    #endregion
    #region vSalesInvoiceHd
    public partial class vSalesInvoiceHd
    {
        public string SalesInvoiceDateInString
        {
            get
            {
                return _SalesInvoiceDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public decimal FinalDiscountAmount
        {
            get
            {
                return (_TransactionAmount + VATAmount) * _FinalDiscountPercentage / 100;
            }
        }
        public decimal VATAmount
        {
            get
            {
                return _TransactionAmount * _VATPercentage / 100;
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
        public String DateOfBirthInString
        {
            get { return _DateOfBirth.ToString("dd-MMM-yyyy"); }
        }
        public string StudentAge
        {
            get
            {
                string result;
                int ageInYear, ageInMonth, ageInDay = 0;

                ageInYear = Function.GetPatientAgeInYear(DateOfBirth, DateTime.Now);
                ageInMonth = Function.GetPatientAgeInMonth(DateOfBirth, DateTime.Now);
                ageInDay = Function.GetPatientAgeInDay(DateOfBirth, DateTime.Now);

                if (ageInYear > 0)
                    result = string.Format("{0}yr", ageInYear);
                else if (ageInMonth > 0)
                    result = string.Format("{0}mo", ageInMonth);
                else
                    result = string.Format("{0}day", ageInDay);

                return result;
            }
        }
        public String HomeAddress
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
                //if (_PhoneNo2 != "")
                //{
                //    if (result.ToString() != "")
                //        result.Append(" / ");
                //    result.Append(_PhoneNo2);
                //}
                return result.ToString();
            }
        }
        public String StudentImageUrl { get { return Function.GenerateStudentPictureFileName(_PictureFileName, _StudentCode); } }
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
    #region vStudentAchievement
    public partial class vStudentAchievement 
    {
        public string AchievementDateInDatePickerFormat 
        {
            get { return _AchievementDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }
        public string AchievementDateInString
        {
            get { return _AchievementDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
    }
    #endregion
    #region vStudentCoverageTransactionHd
    public partial class vStudentCoverageTransactionHd
    {
        public String TransactionDateInString
        {
            get { return _TransactionDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
    }
    #endregion
    #region vStudentCustom
    public partial class vStudentCustom
    {
        public String StudentImageUrl { get { return Function.GenerateStudentPictureFileName(_PictureFileName, _StudentCode); } }
    }
    #endregion
    #region vStudentFamily
    public partial class vStudentFamily
    {
        public string DateOfBirthInDatePickerFormat
        {
            get { return _DateOfBirth.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }
    }
    #endregion
    #region vStudentFee
    public partial class vStudentFee
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
        public String PaymentPeriod
        {
            get
            {
                if (_GCAdmissionPaymentPeriod == Constant.AdmissionPaymentPeriod.TAHUNAN)
                    return string.Format("{0}", _TransactionYear);
                if (_GCAdmissionPaymentPeriod == Constant.AdmissionPaymentPeriod.BULANAN)
                {
                    DateTime dt = new DateTime(_TransactionYear, _TransactionMonth, 1);
                    return string.Format("{0}", dt.ToString("MMM yyyy"));
                }
                return "";
            }
        }
    }
    #endregion
    #region vStudentFeeDt
    public partial class vStudentFeeDt
    {
        public Boolean IsProcessed
        {
            get { return _ARInvoiceDtID > 0; }
        }
        public Boolean IsClosed
        {
            get { return _GCTransactionStatus == Constant.TransactionStatus.CLOSED; }
        }
        public String DueDateInString
        {
            get { return _DueDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
        public String cfStudentFeeCompTypeName
        {
            get {
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
    }
    #endregion
    #region vStudentScholarshipTransactionHd
    public partial class vStudentScholarshipTransactionHd
    {
        public String TransactionDateInString
        {
            get { return _TransactionDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
    }
    #endregion
    #region vStudentScholarshipTransactionDt
    public partial class vStudentScholarshipTransactionDt
    {
        public String StartingDateInString
        {
            get { return _StartingDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }

        public String StartingDateInYear 
        {
            get { return _StartingDate.ToString("yyyy"); }
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
    #region vTeacher
    public partial class vTeacher
    {

        public String HomeAddress
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
    #region vTeacherAbsence
    public partial class vTeacherAbsence
    {
        public string cfDate
        {
            get
            {
                return string.Format("{0} - {1}", _StartDate.ToString(Constant.FormatString.DATE_FORMAT), _EndDate.ToString(Constant.FormatString.DATE_FORMAT));
            }
        }
        public string cfTime
        {
            get
            {
                if (_IsFullDay)
                    return "Full Day";
                return string.Format("{0} - {1}", _StartTime, _EndTime);
            }
        }
        public string cfAbsenceReason
        {
            get
            {
                if (_GCAbsenceReason == Constant.AbsenceReason.OTHER)
                    return _OtherAbsenceReason;
                return _AbsenceReason;
            }
        }
    }
    #endregion
    #region vTeacherMark
    public partial class vTeacherMark 
    {
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

        public string PeriodNoInMonth 
        {
            get 
            {
                switch(_PeriodNo.Substring(4,2))
                {
                    case "01": return "Januari";
                    case "02": return "Februari";
                    case "03": return "Maret";
                    case "04": return "April";
                    case "05": return "Mei";
                    case "06": return "Juni";
                    case "07": return "Juli";
                    case "08": return "Agustus";
                    case "09": return "September";
                    case "10": return "Oktober";
                    case "11": return "November";
                    case "12": return "Desember";
                    default: return "";
                }
            }
        }
    }
    #endregion
    #region vTeacherSubjectPerSchoolType
    public partial class vTeacherSubjectPerSchoolType 
    {
        public string SubjectIDGCSchoolType
        {
            get
            {
                return string.Format("{0}|{1}", _SubjectID, _GCSchoolType);
            }
        }
    }
    #endregion

    #region Project Management
    #region vActivityHistory
    public partial class vActivityHistory
    {
        public string CustomRemarks
        {
            get { return WebUtility.HtmlEncode(_Remarks).Replace(@"\n", "<br/>"); }
        }

        public string CreatedDateInDatePicker
        {
            get { return _CreatedDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }

        public string CreatedDateInDateTime
        {
            get { return _CreatedDate.ToString(Constant.FormatString.DATE_TIME_FORMAT); }
        }

    }
    #endregion
    #region vBudgetRealizationDt
    public partial class vBudgetRealizationDt
    {
        public bool IsAllowEditItem
        {
            get { return _GCItemDetailStatus != Constant.TransactionStatus.OPEN ? false : true; }
        }
    }
    #endregion
    #region vBudgetRealizationHd
    public partial class vBudgetRealizationHd 
    {
        public String RealizationDateInString 
        {
            get { return _RealizationDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
    }
    #endregion
    #region vBudgetRequestDt
    public partial class vBudgetRequestDt
    {
        public bool IsAllowEditItem
        {
            get { return _GCTransactionStatus != Constant.TransactionStatus.OPEN ? false : true; }
        }

        public decimal CustomRequestAmount 
        {
            get { return _RequestAmount - _RealizationAmount; }
        }
    }
    #endregion
    #region vBudgetRequestHd
    public partial class vBudgetRequestHd
    {
        public String RequestDateInString
        {
            get { return _RequestDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
    }
    #endregion
    #region vProject
    public partial class vProject
    {
        public String StartDateInDatePicker
        {
            get { return _StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }

        public String EndDateInDatePicker
        {
            get { return _EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }
    }
    #endregion
    #region vProjectTask
    public partial class vProjectTask
    {
        public string StartDateInDatePicker
        {
            get { return _StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }

        public string EndDateInDatePicker
        {
            get { return _EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }

        public string ScheduleTaskStartDateInDatePicker
        {
            get { return _ScheduleTaskStartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }

        public string ScheduleTaskEndDateInDatePicker
        {
            get { return _ScheduleTaskEndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }

        public string StartDateInString
        {
            get { return _StartDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }

        public string EndDateInString
        {
            get { return _EndDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }

        public string CustomRemarks
        {
            get { return WebUtility.HtmlEncode(_Remarks).Replace(";", "<br/>"); }
        }
    }
    #endregion
    #region vProjectTaskBudget
    public partial class vProjectTaskBudget
    {
        public string CustomRemarks
        {
            get { return WebUtility.HtmlEncode(_Remarks).Replace(";", "<br/>"); }
        }
    }
    #endregion
    #region vProjectTaskCustom
    public partial class vProjectTaskCustom
    {
        public string StartDateInDatePicker
        {
            get { return _StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }

        public string EndDateInDatePicker
        {
            get { return _EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }

        public string StartDateInString
        {
            get { return _StartDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }

        public string EndDateInString
        {
            get { return _EndDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }

        public string CustomRemarks
        {
            get { return WebUtility.HtmlEncode(_Remarks).Replace(";", "<br/>"); }
        }

        public string CustomAssignName
        {
            get
            {
                String lstString = WebUtility.HtmlEncode(_EmployeeName != "" ? String.Format("{0};{1}", _EmployeeName, _ListAssigneeName) : _ListAssigneeName);
                List<String> lstName = lstString.Split(';').ToList();
                return String.Join("<br/>", lstName.Take(3));
            }
        }
    }
    #endregion
    #region vProjectTaskLog
    public partial class vProjectTaskLog
    {
        public string NoteDateInDatePicker
        {
            get { return _NoteDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }

        public string NoteDateInString
        {
            get { return _NoteDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }

        public string CustomRemarks
        {
            get { return WebUtility.HtmlEncode(_Remarks).Replace(";", "<br/>"); }
        }
    }
    #endregion
    #region vProposedBudgetDt
    public partial class vProposedBudgetDt
    {
        public bool IsAllowEditItem
        {
            get { return _GCItemDetailStatus != Constant.ProjectStatus.OPEN ? false : true; }
        }

        public String RealizationDateInDatePicker
        {
            get 
            {
                if(_RealizationDate != new DateTime(1900, 1, 1))
                {
                    DateTime temp = Convert.ToDateTime(_RealizationDate);
                    return temp.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                }
                return ""; 
            }
        }
    }
    #endregion
    #endregion
}
