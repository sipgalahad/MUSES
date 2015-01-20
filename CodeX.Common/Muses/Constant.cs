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
            public const string ACCOUNTING = "AC";
            public const string ASSET_MANAGEMENT = "AM";
            public const string CONTROL_PANEL = "CP";
            public const string CONTROL_PANEL_HQ = "CPHQ";
            public const string FINANCE = "FN";
            public const string INFORMATION = "IF";
            public const string INVENTORY = "IM";
            public const string STUDENT_MANAGEMENT = "SM";
        }
        #endregion

        #region Site Parameter
        public static class SiteParameter
        {
            public const string IP_ADDRESS_SYNC = "CP0001";
            public const string IS_ITEM_DISTRIBUTION_AUTO_RECEIVED = "IM0002";
            public const string MAX_STUDENT = "SM0001";
        }
        #endregion

        #region Standard Code
        public static partial class StandardCode
        {
            public const string SCHOOL_PERIOD_STATUS = "MS001";
            public const string REGISTRATION_TYPE = "MS002";
            public const string SCHOOL_GRADE = "MS003";
            public const string SCHOOL_MAJOR = "MS004";
            public const string STUDENT_ATTENDANCE = "MS005";
            public const string STUDENT_STATUS = "MS006";
            public const string SCHOOL_PERIOD_SCHEDULE_TYPE = "MS007";
            public const string SCHOOL_DAILY_SCHEDULE_TYPE = "MS008";
            public const string TASK_TYPE = "MS009";
            public const string SCHOOL_TYPE = "MS010";
            public const string REGISTRATION_STATUS = "MS011";
            public const string LANGUAGE = "MS012";
            public const string INFORMATION_SOURCE = "MS013";
            public const string ADMISSION_PAYMENT_PERIOD = "MS014";
            public const string ADMISSION_FEE_COMP_TYPE = "MS015";
            public const string FROM_SCHOOL_TYPE = "MS017";
            public const string ACHIEVEMENT_TYPE = "MS019";
            public const string SCHOOL_DAY = "MS020";
        }

        public static class SchoolPeriodStatus
        {
            public const string OPEN = "MS001^001";
            public const string START = "MS001^002";
            public const string END = "MS001^003";
            public const string VOID = "MS001^999";
        }

        public static class SchoolDailyScheduleType
        {
            public const string KBM = "MS008^001";
        }

        public static class PeriodScheduleType
        {
            public const string KBM = "MS007^001";
            public const string INTERNAL_EXAM = "MS007^002";
        }

        public static class SchoolType
        {
            public const string UMUM = "MS016^001";
            public const string KATOLIK = "MS016^002";
        }

        public static class ScholarshipType
        {
            public const string ADMISSION = "MS018^001";
        }

        public static class RegistrationStatus
        {
            public const string OPEN = "MS011^001";
            public const string ACCEPTED = "MS011^002";
            public const string REJECTED = "MS011^003";
            public const string AR_PROCESSED = "MS011^004";
            public const string PAID = "MS011^005";
            public const string SETTLED = "MS011^006";
            public const string CLOSED = "MS011^007";
            public const string VOID = "MS011^999";
        }

        public static class FromSchoolType
        {
            public const string FEEDER = "MS017^001";
            public const string NON_FEEDER = "MS017^002";
        }

        public static class ItemType
        {
            public const string PRODUCT = "X001^001";
        }

        public static class EmployeeType
        {
            public const string TEACHER = "X196^001";
        }

        public static class BusinessObjectType
        {
            public const string STUDENT = "X017^001";
            public const string ITEM = "X017^002";
            public const string USER = "X017^004";
            public const string SUPPLIER = "X017^004";
        }

        public static class AddressType
        {
            public const string SITE = "X301^001";
            public const string BUSINESS_PARTNER = "X301^002";
            public const string STUDENT = "X301^003";
            public const string PROSPECTIVE_STUDENT = "X301^004";
            public const string STUDENT_FAMILY = "X301^005";
            public const string PROSPECTIVE_STUDENT_FAMILY = "X301^006";
            public const string EMPLOYEE = "X301^007";
        }
        #endregion

        #region Menu Code
        public static class MenuCode
        {
            #region Module HQ
            #region ControlPanelHQ
            public static class ControlPanelHQ
            {
                public const string PROSPECTIVE_STUDENT_FORM = "CPHQ010107";
                public const string ITEM_GROUP_MASTER = "CPHQ010201";
                public const string ITEM_PRODUCT = "CPHQ010202";
                public const string LOCATION = "CPHQ010103";
                public const string LOCATION_ITEM = "CPHQ010104";

                public const string SUPPLIER = "CPHQ010201";
                public const string CUSTOMER = "CPHQ010202";

                public const string SITE_INFORMATION = "CPHQ020101";
                public const string USER_ROLES = "CPHQ020301";
                public const string USER_ACCOUNTS = "CPHQ020302";

                public const string SYNC_PROCESS = "CPHQ080100";

                public const string SITE_PAGE = "CPHQ99010000";
                public const string ST_SITE_MODULE = "CPHQ99010101";
                public const string ST_SITE_ITEM_GROUP = "CPHQ99010201";
                public const string ST_SITE_ITEM = "CPHQ99010202";
                public const string ST_SITE_SUPPLIER = "CPHQ99010301";
                public const string ST_SITE_COA = "CPHQ99010401";
            }
            #endregion
            #endregion

            #region Module Site
            #region Accounting
            public static class Accounting
            {
                public const string CHART_OF_ACCOUNT = "AC010100";
                public const string SUB_LEDGER_TYPE = "AC010200";
                public const string SUB_LEDGER = "AC010300";
                public const string JOURNAL_TEMPLATE = "AC010400";
                public const string PRODUCT_LINE = "AC010500";

                public const string GL_SETTING = "AC020100";

                public const string COA_BUDGET_YEAR = "AC030100";
                public const string COA_BUDGET_MONTH = "AC030200";

                public const string JOURNAL_ENTRY = "AC050100";
                public const string JOURNAL_LIST = "AC050200";
                public const string PROFIT_LOSS_INFORMATION = "AC080500";
            }
            #endregion

            #region AssetManagement
            public static class AssetManagement
            {
                public const string FA_DEPRECIATION_METHOD = "AM010100";
                public const string FA_GROUP = "AM010200";
                public const string FA_LOCATION = "AM010300";
                public const string FA_ITEM = "AM010400";
                public const string FA_ITEM_FROM_PURCHASE_RECEIVE = "AM010500";

                public const string FA_ITEM_LIST = "AM020100";
                public const string FA_ITEM_MOVEMENT = "AM020101";
                public const string FA_WRITE_OFF = "AM020102";
                public const string FA_VOID_WRITE_OFF = "AM020200";
            }
            #endregion

            #region ControlPanel
            public static class ControlPanel
            {
                public const string TEACHER = "CP010101";
                public const string CLASS_TYPE = "CP010102";
                public const string ROOM = "CP010103";
                public const string SUBJECT = "CP010104";
                public const string SCHOOL_DAILY_SCHEDULE_TYPE = "CP010105";
                public const string SCHOOL_DAILY_SCHEDULE_PACKAGE = "CP010106";
                public const string PROSPECTIVE_STUDENT_FORM = "CP010107";
                public const string SCHOOL_GRADE = "CP010108";

                public const string ITEM_GROUP_MASTER = "CP010201";
                public const string ITEM_PRODUCT = "CP010202";
                public const string MANUFACTURER = "CP010203";
                public const string PRODUCT_BRAND = "CP010204";
                public const string LOCATION = "CP010205";
                public const string LOCATION_PERMISSION = "CP010206";

                public const string SUPPLIER = "CP010301";
                public const string TERM = "CP010302";
                public const string BANK = "CP010303";
                public const string EDC_MACHINE = "CP010304";
                public const string CREDIT_CARD_FEE = "CP010305";
                public const string MARKUP_MARGIN = "CP010306";

                public const string HOLIDAY = "CP010901";

                public const string SITE_INFORMATION = "CP020101";
                public const string MODULE_MANAGEMENT = "CP020201";
                public const string MENU_MANAGEMENT = "CP020202";
                public const string CUSTOM_ATTRIBUTE = "CP020203";
                public const string TRANSACTION_NUMBERING = "CP020204";
                public const string STANDARD_CODE = "CP020205";
                public const string SETTING_PARAMETER = "CP020206";
                public const string SITE_PARAMETER = "CP020207";
                public const string ZIPCODES = "CP020208";
                public const string FILTER_PARAMETER = "CP020209";
                public const string REPORT_CONFIGURATION = "CP020210";
                public const string LOGIN_ATTRIBUTE = "CP020211";
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

                public const string SUBJECT_PAGE = "CP99010000";
                public const string SB_SUBJECT_GRADE_MAJOR = "CP99010100";
                public const string SB_SUBJECT_MATTER = "CP99010200";
            }
            #endregion

            #region Finance
            public static class Finance
            {
                public const string PROSPECTIVE_STUDENT_LIST = "FN040100";
                public const string AR_INVOICE_PROSPECTIVE_STUDENT_PROCESS = "FN040101";
                public const string AR_INVOICE_PROSPECTIVE_STUDENT_EDIT = "FN040102";
                public const string AR_INVOICE_PROSPECTIVE_STUDENT_RECEIVE = "FN040103";

                public const string STUDENT_LIST = "FN040200";
                public const string AR_INVOICE_STUDENT_PROCESS = "FN040201";
                public const string AR_INVOICE_STUDENT_EDIT = "FN040202";
                public const string AR_INVOICE_STUDENT_RECEIVE = "FN040203";

                public const string SUPPLIER_LIST = "FN050200";
                public const string AP_INVOICE_SUPPLIER_PROCESS = "FN050201";
                public const string AP_INVOICE_SUPPLIER_VERIFICATION = "FN050202";
                public const string AP_INVOICE_SUPPLIER_PAYMENT = "FN050203";

            }
            #endregion

            #region Information
            public static class Information
            {
                public const string TEACHER_SCHEDULE_INFO = "IF010100";
                public const string CLASS_SCHEDULE_INFO = "IF010200";

                public const string STOCK_DETAIL_INFO = "IF020100";

                public const string AP_SUPPLIER_INFORMATION = "IF030100";

                public const string UNBALANCE_JOURNAL = "IF040100";
                public const string BALANCE_INFORMATION = "IF040200";
                public const string BALANCE_INFORMATION_SUB_ACCOUNT = "IF040300";
                public const string BALANCE_INFORMATION_PER_ACCOUNT = "IF040400";
                public const string LABA_RUGI_INFORMATION = "IF040500";
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
                public const string EXAM_SCHEDULE = "SM010300";

                public const string PERIOD_ADMISSION = "SM020100";

                public const string TEACHER_WEEKLY_SCHEDULE = "SM030100";
                public const string TEACHER_CLASS = "SM030200";

                public const string CLASS_WEEKLY_SCHEDULE = "SM040100";
                public const string SUBJECT_PER_CLASS = "SM040200";     

                public const string SCHOOL_PERIOD_PAGE = "SM99010000";
                public const string SP_SCHOOL_PERIOD_SCHEDULE = "SM99010101";
                public const string SP_SCHOOL_PERIOD_SECTION = "SM99010102";
                public const string SP_SCHOOL_PERIOD_ADMISSION = "SM99010201";
                public const string SP_ADMISSION_SELECTION = "SM99010202";
                public const string SP_ADMISSION_FEE_COMP = "SM99010203";
                public const string SP_ADMISSION_FEE_RULE = "SM99010204";
                public const string SP_ADMISSION_PAYMENT = "SM99010205";
                public const string SP_ADMISSION_SCHOLARSHIP = "SM99010206";
                public const string SP_SCHOOL_PERIOD_CLASS_TYPE = "SM99010301";
                public const string SP_SCHOOL_PERIOD_CLASS_TYPE_SUBJECT = "SM99010302";
                public const string SP_GENERATE_SCHOOL_CLASS = "SM99010303";
                public const string SP_SCHOOL_CLASS = "SM99010401";
                public const string SP_CLASS_SUBJECT = "SM99010402";
                public const string SP_CLASS_SCHEDULE = "SM99010403";
                public const string SP_CLASS_STUDENT = "SM99010404";

                public const string STUDENT_PAGE = "SM99020000";
                public const string ST_STUDENT_PAST_STUDY = "SM99020101";
                public const string ST_STUDENT_PARENT = "SM99020102";
                public const string ST_STUDENT_FAMILY = "SM99020103";
                public const string ST_CHANGE_STUDENT_PHOTO = "SM99020104";
                public const string ST_ACHIEVEMENT = "SM99020105";
                public const string ST_FINAL_MARK_CHART = "SM99020201";
                public const string ST_SUBJECT_CHART = "SM99020202";
                public const string ST_STUDENT_HISTORY = "SM99020203";

                public const string PERIOD_ADMISSION_PAGE = "SM99030000";
                public const string PA_PROSPECTIVE_STUDENT = "SM99030101";
                public const string PA_PROSPECTIVE_STUDENT_FORM_STATUS = "SM99030102";
                public const string PA_PROSPECTIVE_STUDENT_MARK = "SM99030201";
                public const string PA_PROSPECTIVE_STUDENT_RESULT = "SM99030202";
                public const string PA_ADMISSION_FEE = "SM99030301";
                public const string PA_GENERATE_AR_PROSPECTIVE_STUDENT = "SM99030302";
                public const string PROSPECTIVE_STUDENT_ACCEPTANCE = "SM99030401";

                public const string CLASS_MEETING_PAGE = "SM99040000";
                public const string WS_CLASS_MEETING = "SM99040101";
                public const string WS_CLASS_ATTENDANCE = "SM99040102";
                public const string WS_CLASS_TASK = "SM99040103";
                public const string WS_ATTENDANCE_HISTORY = "SM99040201";
                public const string WS_STUDENT_MARK = "SM99040202";
                public const string WS_SUBJECT_MATTER = "SM99040203";

                public const string TEACHER_CLASS_SUBJECT_PAGE = "SM99050000";
                public const string TCS_CLASS_TASK = "SM99050101";
                public const string TCS_CLASS_TASK_SUMMARY = "SM99050102";
                public const string TCS_CLASS_ATTENDANCE_SUMMARY = "SM99050103";

                public const string TCS_ATTENDANCE_HISTORY = "SM99050201";
                public const string TCS_STUDENT_MARK = "SM99050202";
                public const string TCS_SUBJECT_MATTER = "SM99050203";
            }
            #endregion
            #endregion
        }
        #endregion

        #region TransactionCode
        public static class TransactionCode
        {
            public const string REGISTRATION = "1101";
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

            public const string AR_INVOICE_PROSPECTIVE_STUDENT = "5102";
            public const string AR_INVOICE_STUDENT = "5103";
            public const string AR_RECEIVE_PROSPECTIVE_STUDENT = "5104";
            public const string AR_RECEIVE_STUDENT = "5105";

            public const string PURCHASE_INVOICE = "6101";
            public const string SUPPLIER_PAYMENT_VERIFICATION = "6102";

            public const string FIXED_ASSET_ITEM_MOVEMENT = "7101";
            public const string FIXED_ASSET_WRITE_OFF = "7102";

            public const string JOURNAL_MEMORIAL = "7201";
            public const string JOURNAL = "72%";
            public const string JOURNAL_MEMORIAL_IKHTISAR = "7299";
        }
        #endregion

        #region Setting Parameter
        public static partial class SettingParameter
        {
            public const string SCHOOL_TYPE = "MSSCP0001";

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
