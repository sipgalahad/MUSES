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
            public const string PROJECT_MANAGEMENT = "PM";
            public const string STUDENT_MANAGEMENT = "SM";
            public const string MOBILE = "MB";
            public const string REPORT = "RP";
        }
        #endregion

        #region Site Parameter
        public static class SiteParameter
        {
            public const string DEFAULT_INTERFACE_JOURNAL_START_DATE = "AC0003";
            public const string IP_ADDRESS_SYNC = "CP0001";
            public const string SCHOOL_TYPE = "CP0002";
            public const string FINANCE_MANAGER = "FN0001";
            public const string DEFAULT_BANK = "FN0003";
            public const string STUDENT_BILL_PRINT_MARGIN = "FN0004";
            public const string IS_ITEM_DISTRIBUTION_AUTO_RECEIVED = "IM0002";
            public const string MAX_STUDENT = "SM0001";
            public const string HEADMASTER = "SM0004"; 
        }
        #endregion

        public static partial class MasterCode
        {
            public const string PROJECT_GROUP = "VID001";
            public const string PROJECT = "VID002";
        }

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
            public const string STUDENT_MOVE_OUT_REASON = "MS026";
            public const string STUDENT_MARK_GROUP = "MS027";
            public const string PERIOD_SECTION = "MS028";
            public const string SUBJECT_MEETING_PLAN_DT_TYPE = "MS029";
            public const string SUBJECT_BASIC_COMPETENCY_DT_TYPE = "MS030";
            public const string COMPETENCY_DESCRIPTION_TYPE = "MS031";
            public const string ABSENCE_REASON = "MS032";
            public const string CURRICULUM_SYLLABUS_TYPE = "MS033";
            public const string CURRICULUM_MEETING_PLAN_TYPE = "MS034";
            public const string TRANSPORTATION = "MS036";
            public const string BODY_CONDITION = "MS037";
            public const string LIVING_WITH = "MS038";
            public const string HANGOUT = "MS039";
            public const string APPETITE = "MS040";
            public const string ORPHANS_STATUS = "MS041";
            public const string RELATIONSHIP_WITH_FAMILY = "MS042";
            public const string URINATE_STATUS = "MS043";
            public const string STUDENT_TYPE = "MS044";
            public const string FINAL_MARK_SOURCE = "MS045";
            public const string FINAL_MARK_SUMMARY_TYPE = "MS046";
            public const string STUDENT_NOTE_CATEGORY = "MS047";
            public const string STUDENT_NOTE_RATE = "MS048";
            public const string SUBJECT_INDICATOR_TYPE = "MS049";

            public const string PROJECT_TASK_STATUS = "DT001";
            public const string PROJECT_TASK_PRIORITY = "DT002";
            public const string PROJECT_STATUS = "DT003";
            public const string PROJECT_TASK_TYPE = "DT004";
            public const string SCHEDULE_TASK_TYPE = "DT005";
            public const string PROJECT_FUNDING = "DT006";
            public const string BUDGET_TYPE = "DT007";
            public const string DUE_DATE_TYPE = "DT008";

            public const string RENUMERATION_COMP_DAY_TYPE = "X321";

        }
        public static class FinalMarkSummaryType
        {
            public const string AVERAGE = "MS046^001";
            public const string MAX = "MS046^002";
        }

        public static class FinalMarkSource
        {
            public const string MARK_TYPE = "MS045^001";
            public const string INDICATOR = "MS045^002";
        }

        public static class TeacherProfileMarkType 
        {
            public const string NUMBER = "MS035^001";
            public const string TEXT = "MS035^002";
        }

        public static class StudentStatus
        {
            public const string ACTIVE = "MS006^001";
            public const string DROP_OUT = "MS006^002";
        }

        public static class PurchaseType
        {
            public const string NON_CONSIGNMENT = "X307^001";
            public const string CONSIGNMENT = "X307^002";
        }

        public static class PurchaseMethod
        {
            public const string PURCHASE_ORDER = "X309^001";
            public const string DIRECT_PURCHASE = "X309^002";
        }

        public static class ReorderType
        {
            public const string STATIC = "X310^001";
            public const string DYNAMIC = "X310^002";
        }

        public static class DistributionType
        {
            public const string DISTRIBUTION = "X311^001";
            public const string CONSUMPTION = "X311^002";
        }

        public static class AchievementType
        {
            public const string AKADEMIS = "MS019^001";
            public const string KESENIAN = "MS019^002";
            public const string OLAHRAGA = "MS019^003";
        }

        public static class DueDateType
        {
            public const string NO_DUE_DATE = "DT008^001";
            public const string RANGE = "DT008^002";
            public const string DUE_DATE_END_DATE = "DT008^003";
        }

        public static class AbsenceReason
        {
            public const string OTHER = "MS032^999";
        }

        public static class CurriculumReportType
        {
            public const string RAPOR = "MS050^001";
        }

        public static class SchoolTypeName 
        { 
            public const string TK = "MS010^001";
            public const string SD = "MS010^002";
            public const string SMP = "MS010^003";
            public const string SMA = "MS010^004";
            public const string UNIVERSITAS = "MS010^005";
            public const string NON_FORMAL = "MS010^009";
        }

        public static class StudentMoveOutReason
        {
            public const string OTHER = "MS026^999";
        }

        public static class CompetencyDescriptionType
        {
            public const string SEMESTER = "MS031^001";
            public const string TASK_INDICATOR = "MS031^002";
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

        public static class StudentMarkGroup
        {
            public const string THEORY = "MS027^001";
            public const string PRACTICE = "MS027^002";
            public const string AFFECTIVE = "MS027^003";
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

        public static class CurriculumSyllabusType
        {
            public const string INDICATOR = "MS033^001";
            public const string STANDARD_CODE = "MS033^002";
            public const string MAIN_COMPETENCY = "MS033^003";
            public const string INDICATOR_DT = "MS033^004";
            public const string OTHER = "MS034^999";
        }

        public static class CurriculumMeetingPlanType
        {
            public const string INDICATOR = "MS034^001";
            public const string MEETING = "MS034^002";
            public const string OTHER = "MS034^999";
        }

        public static class ClassStudyType
        {
            public const string REGULAR = "MS024^001";
            public const string EXTRACURRICULAR = "MS024^002";
            public const string PERSONALITY = "MS024^003";
        }

        public static class MarkType
        {
            public const string NUMBER = "MS025^001";
            public const string OPTION = "MS025^002";
            public const string DESCRIPTION = "MS025^003";
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
            public const string OTHER = "X196^999";
        }

        public static class BusinessObjectType
        {
            public const string STUDENT = "X017^001";
            public const string ITEM = "X017^002";
            public const string USER = "X017^003";
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
            public const string STUDENT_FAMILY_OFFICE = "X301^007";
            public const string PROSPECTIVE_STUDENT_FAMILY_OFFICE = "X301^008";
        }

        public static class EmployeeStatus
        {
            public const string FULL_TIME_EMPLOYED = "0066^001";
            public const string PART_TIME_EMPLOYED = "0066^002";
            public const string CONTRACT = "0066^003";
        }

        public static class ProjectTaskStatus
        {
            public const string OPEN = "DT001^001";
            public const string IN_PROGRESS = "DT001^002";
            public const string NEED_CONFIRMATION = "DT001^003";
            public const string CLOSED = "DT001^004";
            public const string VOID = "DT001^999";
        }

        public static class ProjectTaskPriority
        {
            public const string LOW = "DT002^001";
            public const string MEDIUM = "DT002^002";
            public const string HIGH = "DT002^003";
        }

        public static class ProjectStatus
        {
            public const string OPEN = "DT003^001";
            public const string PROPOSED = "DT003^002";
            public const string APPROVED = "DT003^003";
            public const string IN_PROGRESS = "DT003^004";
            public const string COMPLETE = "DT003^005";
            public const string CANCELED = "DT003^999";
        }

        public static class ProjectTaskType
        {
            public const string SCHEDULED = "DT004^001";
            public const string FLOATING_TASK = "DT004^002";
        }

        public static class ProjectScheduledTaskType
        {
            public const string RANGED_TIME = "DT005^001";
            public const string ALL_TIME = "DT005^002";
        }

        public static class TemplateGroup
        {
            public const string EMAIL = "X112^001";
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
                public const string TREASURY_BOOK = "AC010800";

                public const string GL_SETTING = "AC020100";
                public const string GL_PRODUCT_LINE = "AC020201";
                public const string GL_SUPPLIER_LINE = "AC020202";
                public const string GL_AP_OTHER = "AC020203";
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
                public const string TREASURY_ENTRY = "AC050400";

                public const string JOURNAL_POSTING = "AC060100";

                //public const string PROFIT_LOSS_INFORMATION = "AC080500";

                public const string REPORT = "AC090000";
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
                public const string SCHOOL_UNIT = "CP010101";
                public const string ROOM = "CP010102";
                public const string SCHOOL_DAILY_SCHEDULE_TYPE = "CP010103";
                public const string SCHOOL_DAILY_SCHEDULE_PACKAGE = "CP010104";
                public const string TEACHER = "CP010105";
                public const string EMPLOYEE = "CP010106";
                public const string TEACHER_MARK_TYPE_GROUP = "CP010107";
                public const string DEPARTMENT = "CP010108";
                public const string SERVICE_UNIT = "CP010109";

                public const string SUBJECT = "CP010201";
                public const string EXTRACURRICULAR_SUBJECT = "CP010202";
                public const string PERSONALITY = "CP010203";
                public const string MARK_TYPE = "CP010204";
                public const string CURRICULUM = "CP010205";
                public const string PROSPECTIVE_STUDENT_FORM = "CP010206";

                public const string ITEM_GROUP_MASTER = "CP010301";
                public const string ITEM_PRODUCT = "CP010302";
                public const string MANUFACTURER = "CP010303";
                public const string PRODUCT_BRAND = "CP010304";
                public const string LOCATION = "CP010305";
                public const string LOCATION_PERMISSION = "CP010306";
                public const string LOCATION_ITEM = "CP010307";
                public const string ITEM_UNIT = "CP010308";

                public const string COVERAGE_TYPE = "CP010401";
                public const string CUSTOMER = "CP010402";
                public const string CUSTOMER_CONTRACT = "CP010403";
                public const string SUPPLIER = "CP010404";
                public const string TERM = "CP010405";
                public const string BANK = "CP010406";
                public const string EDC_MACHINE = "CP010407";
                public const string CREDIT_CARD_FEE = "CP010408";
                public const string MARKUP_MARGIN = "CP010409";
                public const string STUDENT_FEE_COMP = "CP010410";

                public const string RENUMERATION_COMP = "CP010501";
                public const string RENUMERATION = "CP010502";
                public const string HR_DAILY_SCHEDULE = "CP010503";
                public const string HR_WEEKLY_SCHEDULE = "CP010504";
                public const string ORGANIZATION_DEPARTMENT = "CP010505";
                public const string RENUMERATION_COMP_FORMULA = "CP010506";
                public const string TEMPLATE_EMPLOYEE_GROUP = "CP010507";
                public const string JOB_LEVEL = "CP010508";
                public const string FAMILY_STATUS = "CP010509";
                public const string REVENUE_PERIOD = "CP010510";
                public const string JOB_LEVEL_POSITION = "CP010511";
                public const string PERFORMANCE_INDICATOR = "CP010512";
                public const string JOB_LEVEL_WORK_YEARS = "CP010513";
                public const string JOB_LEVEL_PERFORMANCE_INDICATOR = "CP010514";
                public const string EMPLOYEE_TYPE = "CP010515";

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
                public const string SB_SUBJECT_CURRICULUM = "CP99010100";
                public const string SB_SUBJECT_CURRICULUM_SYLLABUS = "CP99010200";
                public const string SB_SUBJECT_CURRICULUM_MEETING_PLAN = "CP99010300";

                public const string CURRICULUM_PAGE = "CP99020000";
                public const string CR_CURRICULUM_SCHOOL_PERIOD_SECTION = "CP99020101";
                public const string CR_CURRICULUM_MAJOR = "CP99020102";
                public const string CR_CURRICULUM_CLASS_TYPE = "CP99020103";
                public const string CR_CURRICULUM_EXTRACURRICULAR_CLASS_TYPE = "CP99020104";
                public const string CR_CURRICULUM_MARK_TYPE = "CP99020201";
                public const string CR_CURRICULUM_FINAL_MARK_FORMULA = "CP99020202";
                public const string CR_GRADE_PROMOTION_FORMULA = "CP99020203";
                public const string CR_CURRICULUM_SYLLABUS = "CP99020301";
                public const string CR_CURRICULUM_MEETING_PLAN = "CP99020302";
                public const string CR_CURRICULUM_SUBJECT_GROUP = "CP99020401";
                public const string CR_CURRICULUM_SUBJECT = "CP99020402";
                public const string CR_CURRICULUM_EXTRACURRICULAR = "CP99020403";
                public const string CR_CURRICULUM_PERSONALITY = "CP99020404";

                public const string SCHOOL_TYPE_PAGE = "CP99030000";
                public const string ST_SCHOOL_GRADE = "CP99030101";
                public const string ST_SCHOOL_MAJOR = "CP99030102";
                public const string ST_CLASS_TYPE = "CP99030103";
                public const string ST_EXTRACURRICULAR_CLASS_TYPE = "CP99030104";
                public const string ST_SUBJECT = "CP99030201";
                public const string ST_EXTRACURRICULAR_SUBJECT = "CP99030202";
                public const string ST_PERSONALITY_SUBJECT = "CP99030203";

                public const string SITE_SERVICE_UNIT_PAGE = "CP99040000";
                public const string SSU_LOCATION = "CP99040100";
                public const string SSU_ITEM_LOGISTIC = "CP99040200";
            }
            #endregion

            #region Finance
            public static class Finance
            {
                public const string CREATE_TARIFF = "FN030100";

                public const string PROSPECTIVE_STUDENT_LIST = "FN040100";
                public const string STUDENT_LIST = "FN040200";
                public const string CUSTOMER_LIST = "FN040300";

                public const string SUPPLIER_LIST = "FN050200";
                public const string AP_INVOICE_SUPPLIER_PROCESS = "FN050201";
                public const string AP_INVOICE_SUPPLIER_VERIFICATION = "FN050202";
                public const string AP_INVOICE_SUPPLIER_PAYMENT = "FN050203";

                public const string DIRECT_SALES = "FN060100";
                public const string STUDENT_COVERAGE_TRANSACTION = "FN060200";
                public const string STUDENT_SCHOLARSHIP_TRANSACTION = "FN060300";

                public const string GENERATE_AR_INVOICE_PROSPECTIVE_STUDENT = "FN070100";
                public const string GENERATE_AR_INVOICE_STUDENT = "FN070200";
                public const string GENERATE_UPLOAD_FILE = "FN070300";
                public const string BANK_UPLOADED_FILE = "FN070400";
                public const string STUDENT_FEE_PENALTY = "FN070500";
                public const string DIRECT_SALES_VOID = "FN070600";
                public const string BUDGET_REALIZATION = "FN070700";
                public const string STUDENT_REREGISTRATION = "FN070800";

                public const string REPORT = "FN090000";

                public const string PROSPECTIVE_STUDENT_PAGE = "FN99010000";
                public const string AR_INVOICE_PROSPECTIVE_STUDENT_PROCESS = "FN99010101";
                public const string AR_INVOICE_PROSPECTIVE_STUDENT_EDIT = "FN99010102";
                public const string AR_INVOICE_PROSPECTIVE_STUDENT_RECEIVE = "FN99010103";
                public const string GENERATE_PROSPECTIVE_STUDENT_UPLOAD_FILE = "FN99010201";
                public const string PROSPECTIVE_STUDENT_PAYMENT_METHOD_EDIT = "FN99010202";

                public const string STUDENT_PAGE = "FN99020000";
                public const string AR_INVOICE_STUDENT_PROCESS = "FN99020101";
                public const string AR_INVOICE_STUDENT_EDIT = "FN99020102";
                public const string AR_INVOICE_STUDENT_RECEIVE = "FN99020103";
                public const string GENERATE_STUDENT_UPLOAD_FILE = "FN99020201";
                public const string STUDENT_PAYMENT_METHOD_EDIT = "FN99020202";
                public const string STUDENT_MONTHLY_FEE_EDIT = "FN99020203";
                public const string CHANGE_STUDENT_FEE_CUSTOMER = "FN99020204";

                public const string CUSTOMER_PAGE = "FN99030000";
                public const string AR_INVOICE_CUSTOMER_PROCESS = "FN99030101";
                public const string AR_INVOICE_CUSTOMER_EDIT = "FN99030102";
                public const string AR_INVOICE_CUSTOMER_RECEIVE = "FN99030103";
            }
            #endregion

            #region Human Resources
            public static class HumanResources
            {
                public const string UPDATE_RENUMERATION = "HR010101";
                public const string UPDATE_RENUMERATION_COMP_FORMULA = "HR010102";
                
                public const string UPDATE_RENUMERATION_JOB_LEVEL = "HR010201";
                public const string UPDATE_RENUMERATION_POSITION = "HR010202";
                public const string UPDATE_RENUMERATION_FAMILY_STATUS = "HR010203";
                public const string UPDATE_RENUMERATION_JOB_LEVEL_POSITION = "HR010204";
                public const string UPDATE_JOB_LEVEL_WORKS_YEARS_RENUMERATION = "HR010205";
                public const string UPDATE_JOB_LEVEL_PERFORMANCE_INDICATOR_RENUMERATION = "HR010206";

                public const string UPDATE_EMPLOYEE_JOB_LEVEL = "HR010301";
                public const string UPDATE_EMPLOYEE_POSITION = "HR010302";
                public const string UPDATE_EMPLOYEE_FAMILY_STATUS = "HR010303";
                public const string UPDATE_EMPLOYEE_SITE = "HR010304";
                public const string HR_SCHEDULE_GRUP_HD = "HR010305";
                public const string UPDATE_EMPLOYEE_RENUMERATION = "HR010306";


                public const string OVERTIME_PROPOSAL = "HR020100";
                public const string ABSENCE_PROPOSAL = "HR020200";
                public const string EMPLOYEE_LOAN = "HR020300";
                public const string EMPLOYEE_DAILY_ATTENDANCE = "HR020400";
                public const string EMPLOYEE_REVENUE = "HR020500";
                public const string EMPLOYEE_PERFORMANCE_INDICATOR = "HR020600";
            }
            #endregion

            #region Information
            public static class Information
            {
                public const string TEACHER_SCHEDULE_INFO = "IF010100";
                public const string CLASS_SCHEDULE_INFO = "IF010200";
                public const string TEACHER_PIC_INFO = "IF010300";
                public const string EXTRACURRICULAR_SCHEDULE_INFO = "IF010400";
                public const string STUDENT_STATISTIC_INFO = "IF010500";
                public const string STUDENT_MARK_PER_TEACHER_INFO = "IF010600";
                public const string STUDENT_MARK_PER_CLASS_INFO = "IF010700";

                public const string STOCK_DETAIL_INFO = "IF020100";

                public const string AP_SUPPLIER_INFORMATION = "IF030100";
                public const string AR_STUDENT_INFORMATION = "IF030201";
                public const string AR_PROSPECTIVE_STUDENT_INFORMATION = "IF030202";
                public const string AR_CUSTOMER_INFORMATION = "IF030203";
                public const string TARIFF_INFORMATION = "IF030300";
                public const string STUDENT_FEE = "IF030401";                
                public const string STUDENT_REVENUE_USEK_INFO = "IF030402";
                public const string STUDENT_PAYMENT_SUMMARY = "IF030403";
                public const string STUDENT_BILL_INFORMATION = "IF030404";
                public const string STUDENT_PAYMENT_INFORMATION = "IF030405";
                public const string PROSPECTIVE_STUDENT_PAYMENT_INFORMATION = "IF030406";
                public const string CUSTOMER_PAYMENT_INFORMATION = "IF030407";
                public const string STUDENT_REVENUE_INFO = "IF030408";
                public const string STUDENT_PAYMENT_SUMMARY_INFO = "IF030409";
                public const string STUDENT_FEE_STATUS_SUMMARY = "IF030410";
                public const string STUDENT_COVERAGE_INFO = "IF030501";
                public const string STUDENT_SCHOLARSHIP_INFO = "IF030502";

                public const string UNBALANCE_JOURNAL = "IF040100";
                public const string BALANCE_INFORMATION = "IF040200";
                public const string BALANCE_INFORMATION_SUB_ACCOUNT = "IF040300";
                public const string BALANCE_INFORMATION_PER_ACCOUNT = "IF040400";
                public const string LABA_RUGI_INFORMATION = "IF040500";

                public const string POSITION_RENUMERATION_INFORMATION = "IF050100";
                public const string EMPLOYEE_RENUMERATION_INFORMATION = "IF050200";
                public const string JOB_LEVEL_RENUMERATION_INFORMATION = "IF050400";
                public const string FAMILY_STATUS_RENUMERATION_INFORMATION = "IF050500";
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
                public const string ITEM_DISTRIBUTION_CONFIRMED = "IM030203";
                public const string PURCHASE_RECEIVE = "IM030301";
                public const string PURCHASE_RETURN = "IM030302";
                public const string CREDIT_NOTE = "IM030303";
                public const string PURCHASE_REPLACEMENT = "IM030304";
                public const string ITEM_ADJUSTMENT = "IM030502";
                public const string ITEM_CONSUMPTION = "IM030503";
                public const string ITEM_PRODUCTION = "IM030504";
                public const string STOCK_TAKING = "IM030505";

                public const string CONSIGNMENT_ORDER = "IM040101";
                public const string APPROVED_CONSIGNMENT_ORDER = "IM040102";
                public const string CONSIGNMENT_RECEIVE = "IM040200";
                public const string CONSIGNMENT_RETURN = "IM040300";

                public const string REORDER_ITEM_REQUEST_CROSS_SITE = "IM050101";
                public const string ITEM_REQUEST_CROSS_SITE = "IM050102";
                public const string APPROVED_ITEM_REQUEST_CROSS_SITE = "IM050103";
                public const string REORDER_ITEM_DISTRIBUTION_CROSS_SITE = "IM050201";
                public const string ITEM_DISTRIBUTION_CROSS_SITE = "IM050202";
                public const string ITEM_DISTRIBUTION_CONFIRMED_CROSS_SITE = "IM050203";

                public const string PURCHASE_BUDGET = "IM060100";
                public const string PURCHASE_BUDGET_APPROVAL = "IM060200";

                public const string ITEM_REQUEST_APPROVAL = "IM070101";
                public const string ITEM_DISTRIBUTION_APPROVAL = "IM070102";
                public const string ITEM_ADJUSTMENT_APPROVAL = "IM070103";
                public const string ITEM_CONSUMPTION_APPROVAL = "IM070104";
                public const string PURCHASE_RECEIVE_CONFIRMED = "IM070105";
                public const string PURCHASE_RECEIVE_APPROVAL = "IM070106";
                public const string PURCHASE_RETURN_APPROVAL = "IM070107";
                public const string PURCHASE_RECEIVE_VOID = "IM070108";
                public const string PURCHASE_RETURN_VOID = "IM070109";

                public const string PURCHASE_REQUEST_APPROVAL = "IM070201";
                public const string PURCHASE_ORDER_APPROVAL = "IM070202";
                public const string DIRECT_PURCHASE_CONFIRMED = "IM070203";

                public const string CONSIGNMENT_ORDER_APPROVAL = "IM070301";
                public const string CONSIGNMENT_RECEIVE_CONFIRMED = "IM070302";
                public const string CONSIGNMENT_RECEIVE_APPROVAL = "IM070303";
                public const string CONSIGNMENT_RETURN_APPROVAL = "IM070304";

                public const string ITEM_REQUEST_CROSS_SITE_APPROVAL = "IM070401";
                public const string ITEM_DISTRIBUTION_CROSS_SITE_APPROVAL = "IM070402";

                public const string REPORT = "IM090000";
            }
            #endregion

            #region Mobile
            public static class Mobile
            {
                public const string STUDENT_CLASS_INFO = "MB010100";
                public const string STUDENT_BILL_INFO = "MB010200";
            }
            #endregion

            #region Project Management
            public static class ProjectManagement
            {
                public const string PROJECT = "PM010100";
                public const string BUDGET = "PM010200";
                public const string RPROJECT_GROUP = "PM010300";
                public const string RPROJECT = "PM010400";

                public const string PROJECT_TASK = "PM020100";
                public const string TO_DO_LIST = "PM020200";
                public const string PROJECT_MANAGEMENT = "PM020300";
                public const string TO_DO_LIST_IN_CALENDAR = "PM020400";
                public const string BUDGET_MANAGEMENT = "PM020500";
                public const string RPROJECT_PAGE_LIST = "PM020600";
                public const string MY_RPROJECT_PAGE_LIST = "PM020700";
                public const string RBUDGET_REQUEST_OUTSTANDING = "PM020800";
                public const string MY_RPROJECT_SUMMARY_LIST = "PM020900";

                public const string RPROJECT_PAGE = "PM99030000";
                public const string RPROJECT_STATUS = "PM99030101";
                public const string RTIMELINE = "PM99030102";
                public const string RPROJECT_EVALUATION = "PM99030103";
                public const string RPROJECT_TASK_FILE = "PM99030104";
                public const string RBUDGET_REQUEST = "PM99030201";
                public const string RITEM_REQUEST = "PM99030202";
                public const string RBUDGET_REQUEST_CONFIRMATION = "PM99030203";
                public const string RBUDGET_REQUEST_REALIZATION_INFORMATION = "PM99030901";
                public const string RITEM_REQUEST_REALIZATION_INFORMATION = "PM99030902";

                public const string PROJECT_MANAGEMENT_PAGE = "PM99010000";
                public const string PROJECT_MANAGEMENT_DETAIL = "PM99010100";
                public const string PROJECT_TASK_DETAIL = "PM99010101";
                public const string TIMELINE = "PM99010102";
                public const string PROJECT_EVALUATION = "PM99010103";
                
                public const string BUDGET_REQUEST = "PM99010107";
                public const string ITEM_REQUEST = "PM99010108";
                
                public const string BUDGET_MANAGEMENT_PAGE = "PM99020000";
                public const string PROPOSED_BUDGET = "PM99020101";
                public const string LIST_PROPOSED_BUDGET = "PM99020102";
                public const string PROJECT_BUDGET_INFORMATION = "PM99020103";
                public const string USE_OF_BUDGET = "PM99020104";
            }
            #endregion

            #region StudentManagement
            public static class StudentManagement
            {
                public const string STUDENT = "SM010100";
                public const string SCHOOL_PERIOD = "SM010200";
                public const string SCHOLARSHIP = "SM010300";

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
                public const string TEACHER_ABSENCE = "SM031000";
                public const string TSB_TEACHER_SUBSTITUTION = "SM031001";
                public const string TSB_TEACHER_SUBSTITUTION_PER_DATE = "SM031002";
                public const string STUDENT_MARK_LEDGER = "SM031100";
                public const string STUDENT_MOVE_OUT = "SM031200";
                public const string SCHOOL_CLASS = "SM031300";

                public const string TEACHER_PROFILE = "SM040100";

                public const string SUBJECT = "SM110100";
                public const string TEACHER_PERIOD_CLASS_TYPE_SUBJECT = "SM110200";

                public const string TEACHER_WEEKLY_SCHEDULE = "SM120100";
                public const string TEACHER_CLASS = "SM120200";
                public const string TEACHER_STUDENT_FINAL_MARK = "SM120300";
                public const string TEACHER_SCHOOL_CLASS = "SM120400";

                public const string SCHOOL_PERIOD_PAGE = "SM99010000";
                public const string SP_SCHOOL_PERIOD_SCHEDULE = "SM99010101";
                public const string SP_SCHOOL_PERIOD_SECTION = "SM99010102";
                public const string SP_EXAM_SCHEDULE = "SM99010103";
                public const string SP_SCHOOL_PERIOD_ADMISSION = "SM99010201";
                public const string SP_ADMISSION_SELECTION = "SM99010202";
                public const string SP_ADMISSION_FEE_COMP = "SM99010203";
                public const string SP_ADMISSION_FEE_RULE = "SM99010204";
                public const string SP_ADMISSION_PAYMENT = "SM99010205";
                public const string SP_ADMISSION_SCHOLARSHIP = "SM99010206";
                public const string SP_SCHOOL_PERIOD_GRADE = "SM99010301";
                public const string SP_SCHOOL_PERIOD_CLASS_TYPE = "SM99010302";
                public const string SP_SCHOOL_PERIOD_CLASS_TYPE_SUBJECT = "SM99010303";
                public const string SP_GENERATE_SCHOOL_CLASS = "SM99010304";
                public const string SP_SCHOOL_PERIOD_CLASS_TYPE_PERSONALITY = "SM99010305";
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
                public const string ST_STUDENT_TRUSTEE = "SM99020103";
                public const string ST_STUDENT_FAMILY = "SM99020104";
                public const string ST_CHANGE_STUDENT_PHOTO = "SM99020105";
                public const string ST_ACHIEVEMENT = "SM99020106";
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
                public const string WS_SUBJECT_INDICATOR = "SM99040105";
                public const string WS_ATTENDANCE_HISTORY = "SM99040201";
                public const string WS_STUDENT_MARK = "SM99040202";
                public const string WS_SUBJECT_CURRICULUM_SYLLABUS = "SM99040203";
                public const string WS_SUBJECT_CURRICULUM_MEETING_PLAN = "SM99040204";
                public const string WS_STUDENT_MARK_PER_INDICATOR = "SM99040205";
                public const string WS_CLASS_TASK_PER_INDICATOR = "SM99040206";
                public const string WS_STUDENT_NOTE_INFORMATION = "SM99040207";
                public const string WS_STUDENT_MARK_PER_INDICATOR_ALL = "SM99040208";
                public const string WS_CLASS_ATTENDANCE_SUMMARY = "SM99040209";

                public const string TEACHER_CLASS_SUBJECT_PAGE = "SM99050000";
                public const string TCS_CLASS_TASK = "SM99050101";
                public const string TCS_CLASS_TASK_SUMMARY = "SM99050102";
                public const string TSC_STUDENT_MARK_PER_INDICATOR = "SM99050103";
                public const string TCS_SUBJECT_INDICATOR = "SM99050104";

                public const string TCS_ATTENDANCE_HISTORY = "SM99050201";
                public const string TCS_STUDENT_MARK = "SM99050202";
                public const string TCS_STUDENT_MARK_PER_INDICATOR = "SM99050203";
                public const string TCS_STUDENT_MARK_PER_INDICATOR_ALL = "SM99050204";
                public const string TCS_CLASS_ATTENDANCE_SUMMARY = "SM99050205";

                public const string CLASS_STUDENT_PAGE = "SM99060000";
                public const string CS_SUBJECT_MARK = "SM99060101";
                public const string CS_EXTRACURRICULAR_MARK = "SM99060102";
                public const string CS_PERSONALITY_MARK = "SM99060103";
                public const string CS_CLASS_STUDENT_NOTE = "SM99060104";
                public const string CS_ORGANIZATION_MARK = "SM99060105";
                public const string CS_STUDENT_ATTENDANCE = "SM99060106";

                public const string SUBJECT_PAGE = "SM99070000";
                public const string SB_SUBJECT_CURRICULUM = "SM99070200";
                public const string SB_SUBJECT_CURRICULUM_SYLLABUS = "SM99070300";
                public const string SB_SUBJECT_CURRICULUM_MEETING_PLAN = "SM99070400";

                public const string SUBJECT_CURRICULUM_PAGE = "SM99080000";
                public const string SBM_SUBJECT_CURRICULUM = "SM99080100";
                public const string SBM_SUBJECT_CURRICULUM_SYLLABUS = "SM99080200";
                public const string SBM_SUBJECT_CURRICULUM_MEETING_PLAN = "SM99080300";

                public const string SCHOOL_CLASS_PAGE = "SM99090000";
                public const string SC_STUDENT_MARK = "SM99090101";
                public const string SC_STUDENT_MARK_PER_INDICATOR = "SM99090102";
                public const string SC_STUDENT_MARK_PER_INDICATOR_ALL = "SM99090103";

                public const string TEACHER_SCHOOL_CLASS_PAGE = "SM99100000";
                public const string MTSC_STUDENT_MARK = "SM99100101";
                public const string MTSC_STUDENT_MARK_PER_INDICATOR = "SM99100102";
                public const string MTSC_STUDENT_MARK_PER_INDICATOR_ALL = "SM99100103";
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
            public const string CONSIGNMENT_ORDER = "4213";
            public const string CONSIGNMENT_RECEIVE = "4214";
            public const string CONSIGNMENT_RETURN = "4215";
            public const string ITEM_REQUEST_CROSS_SITE = "4301";
            public const string ITEM_DISTRIBUTION_CROSS_SITE = "4302";
            public const string PURCHASE_BUDGET = "4701";

            public const string AR_INVOICE_PROSPECTIVE_STUDENT = "5101";
            public const string AR_RECEIVE_PROSPECTIVE_STUDENT = "5102";
            public const string AR_INVOICE_STUDENT = "5201";
            public const string AR_RECEIVE_STUDENT = "5202";
            public const string AR_INVOICE_CUSTOMER = "5301";
            public const string AR_RECEIVE_CUSTOMER = "5302";
            public const string STUDENT_COVERAGE = "5401";
            public const string STUDENT_SCHOLARSHIP = "5402";
            public const string DIRECT_SALES = "5501";
            public const string DIRECT_PAYMENT = "5502";

            public const string PURCHASE_INVOICE = "6101";
            public const string SUPPLIER_PAYMENT_VERIFICATION = "6102";

            public const string FIXED_ASSET_ITEM_MOVEMENT = "7101";
            public const string FIXED_ASSET_WRITE_OFF = "7102";

            public const string JOURNAL_MEMORIAL = "7201";
            public const string JOURNAL = "72%";
            public const string JOURNAL_MEMORIAL_CASH_OUT = "7282";
            public const string JOURNAL_MEMORIAL_CASH_IN = "7283";
            public const string JOURNAL_MEMORIAL_BANK_OUT = "7284";
            public const string JOURNAL_MEMORIAL_BANK_IN = "7285";
            public const string JOURNAL_MEMORIAL_IKHTISAR = "7299";
            public const string TREASURY = "7301";

            public const string PROPOSED_BUDGET = "8101";
            public const string BUDGET_REQUEST = "8102";
            public const string BUDGET_REALIZATION = "8103";

            public const string RENUMERATION = "8201";
            public const string POSITION_RENUMERATION = "8202";
            public const string EMPLOYEE_POSITION = "8203";
            public const string RENUMERATION_COMP_FORMULA = "8204";
            public const string HR_SCHEDULE_GRUP = "8205";
            public const string OVERTIME_PROPOSAL = "8206";
            public const string ABSENCE_PROPOSAL = "8207";
            public const string RENUMERATION_JOB_LEVEL = "8208";
            public const string EMPLOYEE_JOB_LEVEL = "8209";
            public const string EMPLOYEE_LOAN = "8210";
            public const string RENUMERATION_FAMILY_STATUS = "8211";
            public const string EMPLOYEE_FAMILY_STATUS = "8212";
            public const string EMPLOYEE_REVENUE = "8213";
            public const string EMPLOYEE_RENUMERATION = "8214";
            public const string EMPLOYEE_SITE = "8215";
            public const string JOB_LEVEL_POSITION_RENUMERATION = "8216";
            public const string JOB_LEVEL_WORK_YEARS = "8217";
            public const string JOB_LEVEL_PERFORMANCE_INDICATOR = "8217";


            public const string TEACHER_PROFILE = "9101";
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
            public const string IS_VAT_APPLIED_TO_AVERAGE_PRICE = "MSSIM0007";
            public const string IS_PURCHASE_RECEIVE_ALLOW_MULTI_PURCHASE_ORDER = "MSSIM0008";
            public const string IS_ALLOW_REOPEN_OUTSTANDING_PO = "MSSIM0009";

            public const string NON_MASTER_SUPPLIER = "MSSFN0003";
            public const string NON_MASTER_ITEM = "MSSFN0004";
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

        public class EmployeeAttendanceStatus
        {
            public const string HADIR = "X323^001";
            public const string SAKIT = "X323^002";
            public const string IZIN = "X323^003";
            public const string ALPA = "X323^004";
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
            public const string BCA = "MS023^002";
        }

        public class BudgetType 
        { 
            public const string ANGGARAN = "DT007^001";
            public const string SARANA = "DT007^002";
        }
    }
}
