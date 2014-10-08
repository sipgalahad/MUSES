using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeX.Common
{
    public partial class Constant
    {
        #region Session
        public static class Session
        {
        }
        #endregion

        #region Module
        public static class Module
        {
            public const string CONTROL_PANEL = "CP";
            public const string FINANCE = "FN";
            public const string INFORMATION = "IF";
            public const string INVENTORY = "IM";
            public const string STUDENT_MANAGEMENT = "SM";
            public const string TEACHER_PAGE = "TP";
        }
        #endregion

        #region Site Parameter
        public static class SiteParameter
        {
            public const string IS_ITEM_DISTRIBUTION_AUTO_RECEIVED = "IM0002";
        }
        #endregion

        #region Standard Code
        public static class StandardCode
        {
            public const string SCHOOL_PERIOD_STATUS = "MS001";
            public const string SCHOOL_GRADE = "MS003";
            public const string SCHOOL_MAJOR = "MS004";
            public const string STUDENT_ATTENDANCE = "MS005";
            public const string STUDENT_STATUS = "MS006";
            public const string SCHOOL_PERIOD_SCHEDULE_TYPE = "MS007";
            public const string SCHOOL_DAILY_SCHEDULE_TYPE = "MS008";
            public const string TASK_TYPE = "MS009";

            public const string MARITAL_STATUS = "0002";
            public const string GENDER = "0003";
            public const string ETHNIC = "0005";
            public const string RELIGION = "0006";
            public const string NATIONALITY = "0212";
            public const string PROVINCE = "0347";
            public const string ITEM_TYPE = "X001";
            public const string ITEM_UNIT = "X003";
            public const string OCCUPATION = "X012";
            public const string EDUCATION = "X013";
            public const string SALUTATION = "X014";
            public const string TITLE = "X015";
            public const string SUFFIX = "X016";
            public const string BUSINESS_OBJECT_TYPE = "X017";
            public const string HEALTHCARE_OPERATING_GROUP = "X033";
            public const string RESTRICTION_TYPE = "X038";
            public const string VALUE_TYPE = "X103";
            public const string REPORTING_PERIOD = "X106";
            public const string ADJUSTMENT_REASON = "X107";
            public const string FILTER_PARAMETER_TYPE = "X108";
            public const string TRANSACTION_STATUS = "X121";
            public const string DELETE_REASON = "X129";
            public const string REPORT_TYPE = "X140";
            public const string DATA_SOURCE_TYPE = "X141";
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
        }

        public static class SchoolPeriodStatus
        {
            public const string OPEN = "MS001^001";
            public const string START = "MS001^002";
            public const string END = "MS001^003";
            public const string VOID = "MS001^999";
        }

        public static class PurchaseReturnType
        {
            public const string REPLACEMENT = "X161^001";
            public const string CREDIT_NOTE = "X161^002";
        }

        public static class ItemType
        {
            public const string PRODUCT = "X001^001";
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
        }

        public static class AdjustmentType
        {
            public const string RECEIPTS = "X173^001";
        }

        public static class CustomerType
        {
            public const string PERSONAL = "X004^999";
        }

        public static class BusinessObjectType
        {
            public const string PATIENT = "X017^001";
            public const string CUSTOMER = "X017^002";
            public const string SUPPLIER = "X017^003";
            public const string ITEM = "X017^004";
            public const string USER = "X017^005";
            public const string REFERRER = "X017^006";
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

        #region Menu Code
        public static class MenuCode
        {
            #region ControlPanel
            public static class ControlPanel
            {
                public const string TEACHER = "CP010101";
                public const string CLASS_TYPE = "CP010102";
                public const string ROOM = "CP010103";
                public const string SUBJECT = "CP010104";
                public const string SCHOOL_DAILY_SCHEDULE_TYPE = "CP010105";
                public const string SCHOOL_DAILY_SCHEDULE_PACKAGE = "CP010106";

                public const string SITE_INFORMATION = "CP020101";
                public const string MODULE_MANAGEMENT = "CP020201";
                public const string MENU_MANAGEMENT = "CP020202";
                public const string CUSTOM_ATTRIBUTE = "CP020203";
                public const string TRANSACTION_NUMBERING = "CP020204";
                public const string STANDARD_CODE = "CP020205";
                public const string SETTING_PARAMETER = "CP020206";
                public const string ZIPCODES = "CP020207";
                public const string FILTER_PARAMETER = "CP020208";
                public const string REPORT_CONFIGURATION = "CP020209";
                public const string LOGIN_ATTRIBUTE = "CP020210";
                public const string USER_ROLES = "CP020301";
                public const string USER_ACCOUNTS = "CP020302";

                public const string RECOVER_DELETED_RECORD_CONFIGURATION = "CP090101";
                public const string RECOVER_DELETED_RECORD_TOOL = "CP090102";
                public const string DATA_MIGRATION_CONFIGURATION = "CP090201";
                public const string DATA_MIGRATION_TOOL = "CP090202";
                public const string PERSON_NAME_CONFIGURATION = "CP090301";
                public const string PERSON_NAME_TOOL = "CP090302";
                public const string VIEW_ERROR_LOG = "CP090400";

                public const string REPORT = "CP090000";
            }
            #endregion

            #region Finance
            public static class Finance
            {
                public const string SUPPLIER_LIST = "FN050200";
                public const string AP_INVOICE_SUPPLIER_PROCESS = "FN050201";
                public const string AP_INVOICE_SUPPLIER_VERIFICATION = "FN050202";
                public const string AP_INVOICE_SUPPLIER_PAYMENT = "FN050203";

            }
            #endregion

            #region Information
            public static class Information
            {
                public const string STOCK_DETAIL_INFO = "IF020100";

                public const string AP_SUPPLIER_INFORMATION = "IF030100";
            }
            #endregion

            #region Inventory
            public static class Inventory
            {
                public const string REORDER_PURCHASE_REQUEST = "IM020101";
                public const string PURCHASE_REQUEST = "IM020102";
                public const string APPROVED_PURCHASE_REQUEST = "IM020103";
                public const string REORDER_PURCHASE_ORDER = "IM020201";
                public const string PURCHASE_ORDER = "IM020202";
                public const string APPROVED_PURCHASE_ORDER = "IM020203";
                public const string DIRECT_PURCHASE = "IM020301";
                public const string DIRECT_PURCHASE_RETURN = "IM020302";

                public const string REORDER_ITEM_REQUEST = "IM030101";
                public const string ITEM_REQUEST = "IM030102";
                public const string APPROVED_ITEM_REQUEST = "IM030103";
                public const string REORDER_ITEM_DISTRIBUTION = "IM030201";
                public const string ITEM_DISTRIBUTION = "IM030202";
                public const string PURCHASE_RECEIVE = "IM030301";
                public const string PURCHASE_RETURN = "IM030302";
                public const string CREDIT_NOTE = "IM030303";
                public const string PURCHASE_REPLACEMENT = "IM030304";
                public const string ITEM_DISTRIBUTION_CONFIRMED = "IM030203";
                public const string ITEM_ADJUSTMENT = "IM030502";
                public const string ITEM_CONSUMPTION = "IM030503";
                public const string ITEM_PRODUCTION = "IM030504";
                public const string STOCK_TAKING = "IM030505";

                public const string ITEM_REQUEST_APPROVAL = "IM040100";
                public const string PURCHASE_REQUEST_APPROVAL = "IM040200";
                public const string PURCHASE_ORDER_APPROVAL = "IM040300";
                public const string PURCHASE_RECEIVE_APPROVAL = "IM040400";
                public const string ITEM_DISTRIBUTION_APPROVAL = "IM040500";
                public const string PURCHASE_RECEIVE_CONFIRMED = "IM040600";
                public const string PURCHASE_RETURN_APPROVAL = "IM040700";
                public const string ITEM_ADJUSTMENT_APPROVAL = "IM040800";
                public const string ITEM_CONSUMPTION_APPROVAL = "IM040900";
                public const string PURCHASE_RECEIVE_VOID = "IM041000";

                public const string REPORT = "IM090000";
            }
            #endregion

            #region StudentManagement
            public static class StudentManagement
            {
                public const string STUDENT = "SM010100";
                public const string SCHOOL_PERIOD = "SM010200";

                public const string SCHOOL_PERIOD_PAGE = "SM99010000";
                public const string SP_SCHOOL_PERIOD_SCHEDULE = "SM99010101";
                public const string SP_SCHOOL_PERIOD_SECTION = "SM99010102";
                public const string SP_SCHOOL_PERIOD_ADMISSION = "SM99010103";

                public const string SP_SCHOOL_PERIOD_CLASS_TYPE = "SM99010201";
                public const string SP_SCHOOL_PERIOD_CLASS_TYPE_SUBJECT = "SM99010202";

                public const string SP_SCHOOL_CLASS = "SM99010301";
                public const string SP_CLASS_SUBJECT = "SM99010302";
                public const string SP_CLASS_SCHEDULE = "SM99010303";
                public const string SP_CLASS_STUDENT = "SM99010304";
            }
            #endregion

            #region TeacherPage
            public static class TeacherPage
            {
                public const string WEEKLY_SCHEDULE = "TP010100";

                public const string CLASS_MEETING_PAGE = "TP99010000";
                public const string WS_CLASS_MEETING = "TP99010101";
                public const string WS_CLASS_ATTENDANCE = "TP99010102";
                public const string WS_CLASS_TASK = "TP99010103";

                public const string WS_MEETING_HISTORY = "TP99010201";
                public const string WS_ATTENDANCE_HISTORY = "TP99010202";
            }
        }
        #endregion

        public static class TransactionCode
        {
            public const string ITEM_REQUEST = "4104";
            public const string ITEM_CONSUMPTION = "4105";
            public const string ITEM_ADJUSTMENT = "4106";
            public const string PURCHASE_REQUEST = "4201";
            public const string PURCHASE_ORDER = "4202";
            public const string PURCHASE_RECEIVE = "4203";
            public const string ITEM_DISTRIBUTION = "4204";
            public const string PURCHASE_RETURN = "4205";
            public const string DIRECT_PURCHASE = "4206";
            public const string DIRECT_PURCHASE_RETURN = "4207";
            public const string SUPPLIER_CREDIT_NOTE = "4208";
            public const string PRODUCTION_PROCESS = "4209";
            public const string STOCK_TAKING = "4210";
            public const string PURCHASE_RETURN_REPLACEMENT = "4211";

            public const string AR_INVOICE_PATIENT = "5102";
            public const string AR_INVOICE_PAYER = "5103";
            public const string AR_RECEIVE_PATIENT = "5104";
            public const string AR_RECEIVE_PAYER = "5105";

            public const string PURCHASE_INVOICE = "6101";
            public const string SUPPLIER_PAYMENT_VERIFICATION = "6102";
        }
        #endregion

        #region Setting Parameter
        public static partial class SettingParameter
        {
            public const string DEFAULT_MARKUP_MARGIN = "MSSFN0001";
            public const string VAT_PERCENTAGE = "MSSFN0002";

            public const string IS_CONFIRM_PURCHASE_RECEIVE = "MSSIM0002";
            public const string DEFAULT_CYCLE_COUNT_TYPE = "MSSIM0003";
            public const string RANGE_EXPIRED_DATE = "MSSIM0004";
            public const string IS_DISCOUNT_APPLIED_TO_AVERAGE_PRICE = "MSSIM0005";
            public const string IS_DISCOUNT_APPLIED_TO_UNIT_PRICE = "MSSIM0006";
        }
        #endregion
    }
}
