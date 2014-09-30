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
            public const string STUDENT_MANAGEMENT = "SM";
        }
        #endregion

        #region Standard Code
        public static class StandardCode
        {
            public const string SCHOOL_PERIOD_STATUS = "MS001";
            public const string SCHOOL_GRADE = "MS003";
            public const string SCHOOL_MAJOR = "MS004";
            public const string STUDENT_STATUS = "MS006";
            public const string SCHOOL_PERIOD_SCHEDULE_TYPE = "MS007";
            public const string SCHOOL_DAILY_SCHEDULE_TYPE = "MS008";


            public const string MARITAL_STATUS = "0002";
            public const string GENDER = "0003";
            public const string ETHNIC = "0005";
            public const string RELIGION = "0006";
            public const string ADMISSION_TYPE = "0007";
            public const string ADMISSION_SOURCE = "0023";
            public const string ADMISSION_CONDITION = "0043";
            public const string FAMILY_RELATION = "0063";
            public const string EMPLOYMENT_STATUS = "0066";
            public const string BED_STATUS = "0116";
            public const string ALLERGEN_TYPE = "0127";
            public const string ALLERGY_SEVERITY = "0128";
            public const string MEDICATION_ROUTE_HL7 = "0162";
            public const string NATIONALITY = "0212";
            public const string PATIENT_OUTCOME = "0241";
            public const string DOCUMENT_TYPE = "0270";
            public const string PROVINCE = "0347";
            public const string ITEM_TYPE = "X001";
            public const string LABORATORY_RESULT_TYPE = "X002";
            public const string ITEM_UNIT = "X003";
            public const string CUSTOMER_TYPE = "X004";
            public const string TARIFF_SCHEME = "X005";
            public const string AGE_UNIT = "X008";
            public const string BLOOD_TYPE = "X009";
            public const string OCCUPATION = "X012";
            public const string EDUCATION = "X013";
            public const string SALUTATION = "X014";
            public const string TITLE = "X015";
            public const string SUFFIX = "X016";
            public const string BUSINESS_OBJECT_TYPE = "X017";
            public const string HEALTHCARE_PROFESSIONAL_TYPE = "X019";
            public const string REGISTRATION_STATUS = "X020";
            public const string DIAGNOSIS_TYPE = "X029";
            public const string MEDICATION_ROUTE = "X030";
            public const string DIFFERENTIAL_DIAGNOSIS_STATUS = "X031";
            public const string HEALTHCARE_OPERATING_GROUP = "X033";
            public const string PAYMENT_TYPE = "X034";
            public const string PAYMENT_METHOD = "X035";
            public const string RESTRICTION_TYPE = "X038";
            public const string TOOTH = "X044";
            public const string TOOTH_PROBLEM = "X045";
            public const string TOOTH_STATUS = "X046";
            public const string TOOTH_SURFACES = "X047";
            public const string RL_CLASS = "X048";
            public const string DISCHARGE_ROUTINE = "X052";
            public const string VACCINATION_ROUTE = "X059";
            public const string DIAGNOSTIC_RESULT_INTERPRETATION = "X062";
            public const string ONSET = "X064";
            public const string QUALITY = "X065";
            public const string SEVERITY = "X066";
            public const string PATIENT_CATEGORY = "X067";
            public const string COURSE_TIMING = "X068";
            public const string EXACERBATED = "X069";
            public const string RELIEVED_BY = "X070";
            public const string LABORATORY_UNIT = "X072";
            public const string TRIAGE = "X079";
            public const string VACCINATION_GROUP = "X080";
            public const string PARAMEDIC_ROLE = "X084";
            public const string IDENTITY_NUMBERY_TYPE = "X097";
            public const string REVIEW_OF_SYSTEM = "X098";
            public const string CARD_TYPE = "X102";
            public const string VALUE_TYPE = "X103";
            public const string LABORATORY_TEST_CATEGORY = "X104";
            public const string REFERRAL = "X105";
            public const string REFERRER_GROUP = "X105";
            public const string REPORTING_PERIOD = "X106";
            public const string ADJUSTMENT_REASON = "X107";
            public const string FILTER_PARAMETER_TYPE = "X108";
            public const string ABC_CLASS = "X109";
            public const string MEDICAL_FILE_STATUS = "X111";
            public const string TEMPLATE_TEXT_GROUP = "X112";
            public const string OBJECTIVE_DATA_SOURCE = "X113";
            public const string BODY_DIAGRAM_GROUP = "X114";
            public const string BODY_DIAGRAM_SYMBOL = "X115";
            public const string ALLERGY_INFORMATION_SOURCE = "X116";
            public const string TRANSACTION_STATUS = "X121";
            public const string DRUG_FORM = "X122";
            public const string DRUG_CLASSIFICATION = "X123";
            public const string PREGNANCY_CATEGORY = "X124";
            public const string TO_BE_PERFORMED = "X125";
            public const string PATIENT_TRANSFER_TYPE = "X127";
            public const string DELETE_REASON = "X129";
            public const string DOSING_FREQUENCY = "X130";
            public const string BODY_PART_SYMPTOM_CHECKER = "X135";
            public const string DISCONTINUE_MEDICATION_REASON = "X136";
            public const string REFILL_INSTRUCTION = "X138";
            public const string PATIENT_INSTRUCTION_GROUP = "X139";
            public const string REPORT_TYPE = "X140";
            public const string DATA_SOURCE_TYPE = "X141";
            public const string CARD_PROVIDER = "X142";
            public const string MEDICAL_FOLDER_TYPE = "X144";
            public const string PATIENT_VISIT_NOTES = "X011";
            public const string PURCHASE_ORDER_TYPE = "X145";
            public const string FRANCO_REGION = "X146";
            public const string CURRENCY_CODE = "X147";
            public const string BORN_CONDITION = "X148";
            public const string BIRTH_METHOD = "X149";
            public const string BIRTH_COMPLICATION_TYPE = "X150";
            public const string BIRTH_COD = "X151";
            public const string CAESAR_METHOD = "X152";
            public const string TWIN_SINGLE = "X153";
            public const string BORN_AT = "X154";
            public const string DISCOUNT_REASON = "X155";
            public const string VISIT_REASON = "X156";
            public const string CHARGES_TYPE = "X157";
            public const string REVENUE_SHARING_FORMULA_TYPE = "X158";
            public const string REVENUE_SHARING_COMPONENT = "X159";
            public const string PURCHASE_RETURN_TYPE = "X161";
            public const string PURCHASE_RETURN_REASON = "X162";
            public const string PATIENT_ATD_STATUS = "X163";
            public const string REVENUE_SHARING_ADJUSTMENT_GROUP = "X166";
            public const string REVENUE_SHARING_ADJUSTMENT_TYPE = "X167";
            public const string CASHIER_GROUP = "X169";
            public const string RETURN_REASON = "X170";
            public const string COENAM_RULE = "X172";
            public const string ADJUSTMENT_TYPE = "X173";
            public const string CONSUMPTION_TYPE = "X174";
            public const string DIRECT_PURCHASE_TYPE = "X175";
            public const string SUPPLIER_CREDIT_NOTE_TYPE = "X176";
            public const string CHECK_COUNT_TYPE = "X177";
            public const string SUPPLIER_PAYMENT_METHOD = "X178";
            public const string PRESCRIPTION_RETURN_TYPE = "X179";
        }

        public static class SchoolPeriodStatus
        {
            public const string OPEN = "MS001^001";
            public const string START = "MS001^002";
            public const string END = "MS001^003";
            public const string VOID = "MS001^999";
        }

        public static class ControlType
        {
            public const string TEXT_BOX = "X103^001";
            public const string COMBO_BOX = "X103^002";
            public const string RADIO_BUTTON = "X103^003";
            public const string CHECK_BOX = "X103^004";
            public const string SEARCH_DIALOG = "X103^005";
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

            #region StudentManagement
            public static class StudentManagement
            {
                public const string STUDENT = "SM010100";
                public const string SCHOOL_PERIOD = "SM010200";

                public const string SCHOOL_PERIOD_PAGE = "SM99010000";
                public const string SP_SCHOOL_PERIOD_SCHEDULE = "SM99010101";
                public const string SP_SCHOOL_PERIOD_SECTION = "SM99010102";
                public const string SP_SCHOOL_DAILY_SCHEDULE = "SM99010103";
            }
            #endregion
        }
        #endregion      
    }
}
