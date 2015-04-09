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
            public const string SCHOOL_TYPE = "CP0002";
            public const string DEFAULT_BANK = "FN0003";
            public const string IS_ITEM_DISTRIBUTION_AUTO_RECEIVED = "IM0002";
            public const string MAX_STUDENT = "SM0001";
            public const string HEADMASTER = "SM0004";
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
            public const string ADMISSION_TYPE = "MS015";
            public const string FROM_SCHOOL_TYPE = "MS017";
            public const string SCHOLARSHIP_TYPE = "MS018";
            public const string ACHIEVEMENT_TYPE = "MS019";
            public const string SCHOOL_DAY = "MS020";
            public const string SCORE_GRADE = "MS022";
            public const string BANK_EXPORT_DATA_TYPE = "MS023";
            public const string SUBJECT_MARK_TYPE = "MS025";
            public const string SUBJECT_MARK_OPTION = "MS026";
            public const string LESSON_TYPE = "MS027";
            public const string PERIOD_SECTION = "MS028";
            public const string SUBJECT_MEETING_PLAN_DT_TYPE = "MS029";
            public const string SUBJECT_BASIC_COMPETENCY_DT_TYPE = "MS030";
            public const string SUBJECT_TYPE = "MS031";
        }

        public static class StudentStatus
        {
            public const string ACTIVE = "MS006^001";
        }

        public static class SubjectType
        {
            public const string UMUM = "MS031^001";
            public const string PENJURUSAN = "MS031^002";
        }

        public static class SchoolPeriodStatus
        {
            public const string OPEN = "MS001^001";
            public const string START = "MS001^002";
            public const string END = "MS001^003";
            public const string VOID = "MS001^999";
        }

        public static class ClassStudentStatus
        {
            public const string OPEN = "MS021^001";
            public const string NAIK_KELAS = "MS021^002";
            public const string TIDAK_NAIK_KELAS = "MS021^003";
        }

        public static class SubjectMarkType
        {
            public const string NUMBER = "MS025^001";
            public const string OPTION = "MS025^002";
            public const string TEXT = "MS025^003";
        }

        public static class LessonType
        {
            public const string THEORY = "MS027^001";
            public const string PRACTICE = "MS027^002";
            public const string THEORY_PRACTICE = "MS027^003";
        }

        public static class AdmissionType
        {
            public const string NEW_STUDENT = "MS015^001";
            public const string STUDENT_TRANSFER = "MS015^002";
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

        public static class ClassStudyType
        {
            public const string REGULAR = "MS024^001";
            public const string EXTRACURRICULAR = "MS024^002";
            public const string PERSONALITY = "MS024^003";
        }

        public static class AdmissionPaymentPeriod 
        { 
            public const string SEKALI_BAYAR = "MS014^001";
            public const string BULANAN = "MS014^002";
            public const string TAHUNAN = "MS014^003";
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
            public const string CUSTOMER = "X017^005";
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
                public const string COA_GROUP = "AC010100";
                public const string CHART_OF_ACCOUNT = "AC010200";
                public const string SUB_LEDGER_TYPE = "AC010300";
                public const string SUB_LEDGER = "AC010400";
                public const string JOURNAL_TEMPLATE = "AC010500";
                public const string PRODUCT_LINE = "AC010600";

                public const string GL_SETTING = "AC020100";
                public const string GL_PRODUCT_LINE = "AC020201";
                public const string GL_SUPPLIER_LINE = "AC020202";
                public const string GL_WAREHOUSE_PRODUCT_LINE_ACCOUNT = "AC020310";
                public const string GL_AP_PAYMENT = "AC020321";
                public const string GL_ACCOUNT_PAYABLE = "AC020322";
                public const string GL_AP_REVENUE_SHARING = "AC020323";
                public const string GL_FA_WRITE_OFF = "AC020340";

                public const string COA_BUDGET_YEAR = "AC030100";
                public const string COA_BUDGET_MONTH = "AC030200";

                public const string JOURNAL_ENTRY = "AC050100";
                public const string JOURNAL_LIST = "AC050200";
                public const string INTERFACE_JOURNAL_PROCESS = "AC050300";

                public const string JOURNAL_POSTING = "AC060100";

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
                public const string SCHOOL_MAJOR = "CP010109";
                public const string TEACHER_MARK_TYPE_GROUP = "CP010110";
                public const string TEACHER_MARK_TYPE_ITEM = "CP010111";
                public const string EXTRACURRICULAR_CLASS_TYPE = "CP010112";
                public const string EXTRACURRICULAR_SUBJECT = "CP010113";
                public const string PERSONALITY = "CP010114";
                public const string STUDENT_FINAL_MARK_FORMULA = "CP010115";
                public const string STUDENT_PROGRESS_RULE = "CP010116";

                public const string ITEM_GROUP_MASTER = "CP010201";
                public const string ITEM_PRODUCT = "CP010202";
                public const string MANUFACTURER = "CP010203";
                public const string PRODUCT_BRAND = "CP010204";
                public const string LOCATION = "CP010205";
                public const string LOCATION_PERMISSION = "CP010206";

                public const string COVERAGE_TYPE = "CP010301";
                public const string CUSTOMER = "CP010302";
                public const string CUSTOMER_CONTRACT = "CP010303";
                public const string SUPPLIER = "CP010304";
                public const string TERM = "CP010305";
                public const string BANK = "CP010306";
                public const string EDC_MACHINE = "CP010307";
                public const string CREDIT_CARD_FEE = "CP010308";
                public const string MARKUP_MARGIN = "CP010309";
                public const string STUDENT_FEE_COMP = "CP010310";

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
                public const string MASTER_CODING = "CP020212";
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
                public const string SB_SUBJECT_CLASS_TYPE = "CP99010100";
                public const string SB_SUBJECT_MATTER = "CP99010200";
                public const string SB_SUBJECT_BASIC_COMPETENCY = "CP99010300";
                public const string SB_SUBJECT_MEETING_PLAN = "CP99010400";
            }
            #endregion

            #region Finance
            public static class Finance
            {
                public const string PROSPECTIVE_STUDENT_LIST = "FN040110";
                public const string GENERATE_PROSPECTIVE_STUDENT_UPLOAD_FILE = "FN040111";
                public const string AR_INVOICE_PROSPECTIVE_STUDENT_PROCESS = "FN040112";
                public const string AR_INVOICE_PROSPECTIVE_STUDENT_EDIT = "FN040113";
                public const string AR_INVOICE_PROSPECTIVE_STUDENT_RECEIVE = "FN040114";
                public const string PROSPECTIVE_STUDENT_PAYMENT_METHOD_EDIT = "FN040115";
                public const string GENERATE_AR_INVOICE_PROSPECTIVE_STUDENT = "FN040120";

                public const string STUDENT_LIST = "FN040210";
                public const string GENERATE_STUDENT_UPLOAD_FILE = "FN040211";
                public const string AR_INVOICE_STUDENT_PROCESS = "FN040212";
                public const string AR_INVOICE_STUDENT_EDIT = "FN040213";
                public const string AR_INVOICE_STUDENT_RECEIVE = "FN040214";
                public const string STUDENT_PAYMENT_METHOD_EDIT = "FN040215";
                public const string STUDENT_MONTHLY_FEE_EDIT = "FN040216";
                public const string GENERATE_AR_INVOICE_STUDENT = "FN040220";

                public const string CUSTOMER_LIST = "FN040300";
                public const string AR_INVOICE_CUSTOMER_PROCESS = "FN040301";
                public const string AR_INVOICE_CUSTOMER_EDIT = "FN040302";
                public const string AR_INVOICE_CUSTOMER_RECEIVE = "FN040303";

                public const string GENERATE_UPLOAD_FILE = "FN040400";
                public const string BANK_UPLOADED_FILE = "FN040500";
                public const string STUDENT_COVERAGE_TRANSACTION = "FN040600";

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
                public const string TEACHER_PIC_INFO = "IF010300";
                public const string EXTRACURRICULAR_SCHEDULE_INFO = "IF010400";

                public const string STOCK_DETAIL_INFO = "IF020100";

                public const string AP_SUPPLIER_INFORMATION = "IF030100";
                public const string AR_STUDENT_INFORMATION = "IF030200";
                public const string AR_PROSPECTIVE_STUDENT_INFORMATION = "IF030300";
                public const string AR_CUSTOMER_INFORMATION = "IF030400";
                public const string STUDENT_FEE = "IF030500";

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
                public const string SCHOLARSHIP = "SM010400";

                public const string PERIOD_ADMISSION = "SM020100";

                public const string CLASS_WEEKLY_SCHEDULE = "SM030100";
                public const string SUBJECT_PER_CLASS = "SM030200";
                public const string STUDENT_FINAL_MARK = "SM030300";
                public const string GRADE_PROMOTION = "SM030400";
                public const string STUDENT_SCHOOL_FEE = "SM030500";
                public const string SCHOOL_PERIOD_CLOSING = "SM030600";
                public const string TEACHER_MARK_GROUP = "SM030700";
                public const string EXTRACURRICULAR_WEEKLY_SCHEDULE = "SM030800";
                public const string STUDENT_DAILY_ATTENDANCE = "SM030900";

                public const string SUBJECT = "SM110100";
                public const string TEACHER_PERIOD_CLASS_TYPE_SUBJECT = "SM110200";

                public const string TEACHER_WEEKLY_SCHEDULE = "SM120100";
                public const string TEACHER_CLASS = "SM120200";
                public const string TEACHER_STUDENT_FINAL_MARK = "SM120300"; 

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
                public const string SP_SCHOOL_PERIOD_CLASS_TYPE_PERSONALITY = "SM99010304";
                public const string SP_SCHOOL_CLASS = "SM99010401";
                public const string SP_CLASS_SUBJECT = "SM99010402";
                public const string SP_CLASS_SCHEDULE = "SM99010403";
                public const string SP_CLASS_STUDENT = "SM99010404";
                public const string SP_SCHOOL_PERIOD_CLASS_TYPE_EXTRACURRICULAR = "SM99010501";
                public const string SP_SCHOOL_PERIOD_CLASS_TYPE_SUBJECT_EXTRACURRICULAR = "SM99010502";
                public const string SP_GENERATE_SCHOOL_CLASS_EXTRACURRICULAR = "SM99010503";
                public const string SP_SCHOOL_CLASS_EXTRACURRICULAR = "SM99010601";
                public const string SP_CLASS_SUBJECT_EXTRACURRICULAR = "SM99010602";
                public const string SP_CLASS_SCHEDULE_EXTRACURRICULAR = "SM99010603";
                public const string SP_CLASS_STUDENT_EXTRACURRICULAR = "SM99010604";
                public const string SP_TEACHER_SCHEDULE = "SM99010701";
                public const string SP_ORGANIZATION = "SM99010702";

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
                public const string WS_STUDENT_NOTE = "SM99040104";
                public const string WS_ATTENDANCE_HISTORY = "SM99040201";
                public const string WS_STUDENT_MARK = "SM99040202";
                public const string WS_SUBJECT_MATTER = "SM99040203";
                public const string WS_SUBJECT_MEETING_PLAN = "SM99040204";

                public const string TEACHER_CLASS_SUBJECT_PAGE = "SM99050000";
                public const string TCS_CLASS_TASK = "SM99050101";
                public const string TCS_CLASS_TASK_SUMMARY = "SM99050102";
                public const string TCS_CLASS_ATTENDANCE_SUMMARY = "SM99050103";

                public const string TCS_ATTENDANCE_HISTORY = "SM99050201";
                public const string TCS_STUDENT_MARK = "SM99050202";
                public const string TCS_SUBJECT_MATTER = "SM99050203";

                public const string CLASS_STUDENT_PAGE = "SM99060000";
                public const string CS_SUBJECT_MARK = "SM99060101";
                public const string CS_EXTRACURRICULAR_MARK = "SM99060102";
                public const string CS_PERSONALITY_MARK = "SM99060103";
                public const string CS_CLASS_STUDENT_NOTE = "SM99060104";
                public const string CS_ORGANIZATION_MARK = "SM99060105";
                public const string CS_STUDENT_ATTENDANCE = "SM99060106";

                public const string SUBJECT_PAGE = "SM99070000";
                public const string SB_SUBJECT_MATTER = "SM99070200";
                public const string SB_SUBJECT_BASIC_COMPETENCY = "SM99070300";
                public const string SB_SUBJECT_MEETING_PLAN = "SM99070400";

                public const string SUBJECT_MATTER_PAGE = "SM99080000";
                public const string SBM_SUBJECT_MATTER = "SM99080100";
                public const string SBM_SUBJECT_BASIC_COMPETENCY = "SM99080200";
                public const string SBM_SUBJECT_MEETING_PLAN = "SM99080300";
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

            public const string AR_INVOICE_PROSPECTIVE_STUDENT = "5101";
            public const string AR_RECEIVE_PROSPECTIVE_STUDENT = "5102";
            public const string AR_INVOICE_STUDENT = "5201";
            public const string AR_RECEIVE_STUDENT = "5202";
            public const string AR_INVOICE_CUSTOMER = "5301";
            public const string AR_RECEIVE_CUSTOMER = "5302";
            public const string STUDENT_COVERAGE = "5401";

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

        public class TaskType 
        { 
            public const string UTS = "MS009^001";
            public const string UAS = "MS009^002";
            public const string ULANGAN = "MS009^003";
            public const string PEKERJAAN_RUMAH = "MS009^004";
            public const string TUGAS_KELAS = "MS009^005";
            public const string TUGAS_KELOMPOK = "MS009^006";
        }

        public class AttendanceStatus 
        { 
            public const string HADIR = "MS005^001";
	        public const string SAKIT = "MS005^002";
	        public const string IZIN = "MS005^003";
            public const string ALPA = "MS005^004";
        }

        public class BankExportDataType 
        {
            public const string MANDIRI = "MS023^001";
        }
    }
}
