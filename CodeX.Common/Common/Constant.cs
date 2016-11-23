using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeX.Common
{
    public partial class Constant
    {
        public static class GridViewPageSize
        {
            public const int GRID_MASTER = 15;
            public const int GRID_MATRIX = 10;
            public const int GRID_POPUP = 8;
            public const int GRID_POPUP_LIST = 8;
            public const int GRID_PATIENT_LIST = 10;
        }

        public static class DefaultValueEntry
        {
            public const string DATE_NOW = "@DateNow";
            public const string TIME_NOW = "@TimeNow";
        }

        public static class FormatString
        {
            public const string DATE_FORMAT = "dd-MMM-yyyy";
            public const string DATE_PICKER_FORMAT = "dd-MM-yyyy";
            public const string DATE_FORMAT_112 = "yyyyMMdd";
            public const string TIME_FORMAT = "HH:mm";
            public const string DATE_TIME_FORMAT = "dd-MMM-yyyy HH:mm:ss";
            public const string DATE_REPORT_FORMAT = "dd MMMM yyyy";
        }

        public static class ConstantDate
        {
            public const string DEFAULT_NULL = "01-01-1900";
        }

        public static partial class SettingParameter
        {
            public const string DEFAULT_PASSWORD = "CMN0001";
            public const string PERSON_NAME_FORMAT = "CMN0002";
        }

        public static class DBSyncInfoCode
        {
            public const string ITEM = "CP001";
            public const string LOCATION = "CP002";
            public const string ITEM_TRANSACTION = "IM001";
        }

        public static partial class MasterCode
        {
            public const string ITEM = "XXX001";
            public const string SUPPLIER = "XXX002";
            public const string CUSTOMER = "XXX003";
            public const string RENUMERATION_COMP = "XXXHR001";
            public const string RENUMERATION = "XXXHR002";
            public const string HR_DAILY_SCHEDULE = "XXXHR003";
            public const string HR_WEEKLY_SCHEDULE = "XXXHR004";
            public const string ORGANIZATION_DEPARTMENT = "XXXHR005";
            public const string RENUMERATION_COMP_FORMULA = "XXXHR006";
            public const string TEMPLATE_EMPLOYEE_GROUP = "XXXHR007";
            public const string JOB_LEVEL = "XXXHR008";
        }

        #region Standard Code
        public static partial class StandardCode
        {
            public const string MARITAL_STATUS = "0002";
            public const string GENDER = "0003";
            public const string ETHNIC = "0005";
            public const string RELIGION = "0006";
            public const string FAMILY_RELATION = "0063";
            public const string EMPLOYMENT_STATUS = "0066";
            public const string NATIONALITY = "0212";
            public const string PROVINCE = "0347";
            public const string ITEM_TYPE = "X001";
            public const string ITEM_UNIT = "X003";
            public const string CUSTOMER_TYPE = "X004";
            public const string TARIFF_SCHEME = "X005";
            public const string BLOOD_TYPE = "X009";
            public const string OCCUPATION = "X012";
            public const string EDUCATION = "X013";
            public const string SALUTATION = "X014";
            public const string TITLE = "X015";
            public const string SUFFIX = "X016";
            public const string BUSINESS_OBJECT_TYPE = "X017";
            public const string HEALTHCARE_OPERATING_GROUP = "X033";
            public const string RESTRICTION_TYPE = "X038";
            public const string CARD_TYPE = "X102";
            public const string VALUE_TYPE = "X103";
            public const string REPORTING_PERIOD = "X106";
            public const string ADJUSTMENT_REASON = "X107";
            public const string FILTER_PARAMETER_TYPE = "X108";
            public const string ABC_CLASS = "X109";
            public const string TRANSACTION_STATUS = "X121";
            public const string DELETE_REASON = "X129";
            public const string PAYMENT_TYPE = "X034";
            public const string PAYMENT_METHOD = "X035";
            public const string REPORT_TYPE = "X140";
            public const string DATA_SOURCE_TYPE = "X141";
            public const string CARD_PROVIDER = "X142";
            public const string PURCHASE_ORDER_TYPE = "X145";
            public const string FRANCO_REGION = "X146";
            public const string CURRENCY_CODE = "X147";
            public const string CHARGES_TYPE = "X157";
            public const string PURCHASE_RETURN_TYPE = "X161";
            public const string PURCHASE_RETURN_REASON = "X162";
            public const string RETURN_REASON = "X170";
            public const string COENAM_RULE = "X172";
            public const string ADJUSTMENT_TYPE = "X173";
            public const string CONSUMPTION_TYPE = "X174";
            public const string DIRECT_PURCHASE_TYPE = "X175";
            public const string SUPPLIER_CREDIT_NOTE_TYPE = "X176";
            public const string CHECK_COUNT_TYPE = "X177";
            public const string SUPPLIER_PAYMENT_METHOD = "X178";
            public const string GLACCOUNT_TYPE = "X180";
            public const string WRITE_OFF_TYPE = "X182";
            public const string ASSET_SALES_TYPE = "X183";
            public const string SUPPLIER_TYPE = "X186";
            public const string JOURNAL_GROUP = "X188";
            public const string GL_ACCOUNT_PAYABLE_TYPE = "X192";
            public const string EMPLOYEE_OCCUPATION = "X193";
            public const string DEPARTMENT = "X194";
            public const string EMPLOYEE_OCCUPATION_LEVEL = "X195";
            public const string PREFIX_TYPE = "X302";
            public const string BANK_TRANSACTION_TYPE = "X305";
            public const string CLIENT_TYPE = "X306";
            public const string PURCHASE_TYPE = "X307";
            public const string DAY = "X308";
            public const string PURCHASE_METHOD = "X309";
            public const string REORDER_TYPE = "X310";
            public const string DISTRIBUTION_TYPE = "X311";
            public const string RENUMERATION_COMP_TYPE = "X314";
            public const string POSITION_LEVEL = "X315";
            public const string POSITION_TYPE = "X316";
            public const string SCHEDULE_TYPE = "X317";
            public const string RENUMERATION_FORMULA_BASE_TARIFF_TYPE = "X319";
            public const string HR_DAILY_SCHEDULE_TYPE = "X320";
            public const string OVERTIME_REASON = "X322";
            public const string ATTENDANCE_STATUS = "X323";
            public const string ABSENCE = "X324";
            public const string JOB_LEVEL_TYPE = "X325";
            public const string RENUMERATION_COMP_SOURCE = "X326";
        }

        public static class RenumerationCompSource
        {
            public const string JOB_LEVEL = "X326^001";
            public const string POSITION = "X326^002";
            public const string FAMILY_STATUS = "X326^003";
        }

        public static class Attendance 
        {
            public const string SAKIT = "X323^002";
            public const string IZIN = "X323^003";
        }

        public static class EmployeeScheduleType
        {
            public const string FIXED = "X317^001";
            public const string SHIFT = "X317^002";
        }

        public static class RenumerationSheduleType
        {
            public const string FIXED = "X317^001";
            public const string SHIFT = "X317^002";
        }

        public static class RenumerationFormulaBaseTariffType
        {
            public const string RENUMERATION_COMP = "X319^001";
            public const string FIX_AMOUNT = "X319^002";
        }

        public static class PaymentMethod
        {
            public const string CASH = "X035^001";
            public const string CREDIT_CARD = "X035^002";
            public const string DEBIT_CARD = "X035^003";
            public const string BANK_TRANSFER = "X035^004";
            public const string ACCOUNT_RECEIVABLES = "X035^005";
            public const string DOWN_PAYMENT = "X035^006";
            public const string DOWN_PAYMENT_RETURN = "X035^007";
        }

        public static class Gender
        {
            public const string MALE = "0003^M";
            public const string FEMALE = "0003^F";
        }

        public static class PaymentType
        {
            public const string DOWN_PAYMENT = "X034^001";
            public const string SETTLEMENT = "X034^002";
        }

        public static class PrefixType
        {
            public const string FIXED_TYPE = "X302^001";
            public const string N_FIRST_DIGIT = "X302^002";
        }

        public static class FamilyRelation
        {
            public const string FATHER = "0063^001";
            public const string MOTHER = "0063^002";
            public const string KAKAK = "0063^003";
            public const string ADIK = "0063^004";
        }

        public static class Religion
        {
            public const string CATHOLIC = "0006^CAT";
        }

        public static class ReportType
        {
            public const string REPORT = "X140^001";
            public const string FORM = "X140^002";
        }

        public static class PurchaseReturnType
        {
            public const string REPLACEMENT = "X161^001";
            public const string CREDIT_NOTE = "X161^002";
        }

        public static class SupplierPaymentMethod
        {
            public const string TUNAI = "X178^001";
            public const string TRANSFER = "X178^002";
            public const string GIRO = "X178^003";
            public const string CHEQUE = "X178^004";
            public const string CREDIT_CARD = "X178^005";
            public const string DEBIT_CARD = "X178^006";
            public const string KOREKSI_FAKTUR = "X178^007";
        }

        public static class ControlType
        {
            public const string TEXT_BOX = "X103^001";
            public const string COMBO_BOX = "X103^002";
            public const string RADIO_BUTTON = "X103^003";
            public const string CHECK_BOX = "X103^004";
            public const string SEARCH_DIALOG = "X103^005";
            public const string CUSTOM_COMBO_BOX = "X103^006";
        }

        public static class AdjustmentType
        {
            public const string RECEIPTS = "X173^001";
        }

        public static class ItemStatus
        {
            public const string ACTIVE = "X181^001";
            public const string IN_ACTIVE = "X181^999";
        }

        public static class JournalGroup
        {
            public const string PENDAPATAN_PENERIMAAN = "X188^001";
            public const string HUTANG_PIUTANG = "X188^002";
            public const string INVENTORY = "X188^003";
            public const string PHARMACY = "X188^004";
            public const string FIXED_ASSET = "X188^005";
            public const string MEMORIAL = "X188^006";
        }

        public static class TransactionStatus
        {
            public const string OPEN = "X121^001";
            public const string WAIT_FOR_APPROVAL = "X121^002";
            public const string APPROVED = "X121^003";
            public const string CLOSED = "X121^004";
            public const string PROCESSED = "X121^005";
            public const string VOID = "X121^999";
        }

        public static class DistributionStatus
        {
            public const string OPEN = "X160^001";
            public const string WAIT_FOR_APPROVAL = "X160^002";
            public const string ON_DELIVERY = "X160^003";
            public const string RECEIVED = "X160^004";
            public const string VOID = "X160^999";
        }

        public static class FilterParameterType
        {
            public const string COMBO_BOX = "X108^001";
            public const string CHECK_LIST = "X108^002";
            public const string DATE = "X108^003";
            public const string PAST_PERIOD = "X108^004";
            public const string UPCOMING_PERIOD = "X108^005";
            public const string FREE_TEXT = "X108^006";
            public const string SEARCH_DIALOG = "X108^007";
            public const string CUSTOM_COMBO_BOX = "X108^008";
            public const string YEAR_COMBO_BOX = "X108^009";
            public const string TEXT_BOX = "X108^010";
            public const string RANGE = "X108^011";
            public const string CONSTANT = "X108^012";
            public const string SINGLE_DATE = "X108^013";
        }

        public static class DataSourceType
        {
            public const string VIEW = "X141^001";
            public const string STORED_PROCEDURE = "X141^002";
        }

        public static class DeleteReason
        {
            public const string WRONG_ENTRY = "X129^001";
            public const string INACTIVE_RECORD = "X129^002";
            public const string OTHER = "X129^999";
        }
        #endregion

        public static class EventViewer 
        {
            public const int WM_COPYDATA = 0x004A;
            public const int WM_QUIT = 0x0010;
        }
    }
}
