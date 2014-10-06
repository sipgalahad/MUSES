using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeX.Data.Core.Dal;

namespace CodeX.Data.Model
{
    #region vClassMeeting
    [Serializable]
    [Table(Name = "vClassMeeting")]
    public class vClassMeeting
    {
        private Int32 _ClassMeetingID;
        private Int32 _ClassSubjectID;
        private DateTime _MeetingDate;
        private String _StartTime;
        private String _EndTime;
        private Int32 _RoomID;
        private String _RoomName;
        private Int32 _TeacherID;
        private String _TeacherName;
        private String _Remarks;
        private String _NextMeetingRemarks;
        private Boolean _IsDeleted;

        [Column(Name = "ClassMeetingID", DataType = "Int32")]
        public Int32 ClassMeetingID
        {
            get { return _ClassMeetingID; }
            set { _ClassMeetingID = value; }
        }
        [Column(Name = "ClassSubjectID", DataType = "Int32")]
        public Int32 ClassSubjectID
        {
            get { return _ClassSubjectID; }
            set { _ClassSubjectID = value; }
        }
        [Column(Name = "MeetingDate", DataType = "DateTime")]
        public DateTime MeetingDate
        {
            get { return _MeetingDate; }
            set { _MeetingDate = value; }
        }
        [Column(Name = "StartTime", DataType = "String")]
        public String StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        [Column(Name = "EndTime", DataType = "String")]
        public String EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        [Column(Name = "RoomID", DataType = "Int32")]
        public Int32 RoomID
        {
            get { return _RoomID; }
            set { _RoomID = value; }
        }
        [Column(Name = "RoomName", DataType = "String")]
        public String RoomName
        {
            get { return _RoomName; }
            set { _RoomName = value; }
        }
        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
        }
        [Column(Name = "TeacherName", DataType = "String")]
        public String TeacherName
        {
            get { return _TeacherName; }
            set { _TeacherName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "NextMeetingRemarks", DataType = "String")]
        public String NextMeetingRemarks
        {
            get { return _NextMeetingRemarks; }
            set { _NextMeetingRemarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vClassSchedule
    [Serializable]
    [Table(Name = "vClassSchedule")]
    public class vClassSchedule
    {
        private Int32 _ClassScheduleID;
        private Int32 _SchoolPeriodID;
        private Int32 _ClassSubjectID;
        private Int32 _SchoolClassID;
        private String _SchoolClassName;
        private Int32 _SubjectID;
        private String _SubjectName;
        private Int16 _DayNumber;
        private Int16 _HoursIndex;
        private String _StartTime;
        private String _EndTime;
        private Int32 _RoomID;
        private String _RoomName;
        private Int32 _TeacherID;
        private String _TeacherName;
        private Boolean _IsDeleted;

        [Column(Name = "ClassScheduleID", DataType = "Int32")]
        public Int32 ClassScheduleID
        {
            get { return _ClassScheduleID; }
            set { _ClassScheduleID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "ClassSubjectID", DataType = "Int32")]
        public Int32 ClassSubjectID
        {
            get { return _ClassSubjectID; }
            set { _ClassSubjectID = value; }
        }
        [Column(Name = "SchoolClassID", DataType = "Int32")]
        public Int32 SchoolClassID
        {
            get { return _SchoolClassID; }
            set { _SchoolClassID = value; }
        }
        [Column(Name = "SchoolClassName", DataType = "String")]
        public String SchoolClassName
        {
            get { return _SchoolClassName; }
            set { _SchoolClassName = value; }
        }
        [Column(Name = "SubjectID", DataType = "Int32")]
        public Int32 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
        [Column(Name = "SubjectName", DataType = "String")]
        public String SubjectName
        {
            get { return _SubjectName; }
            set { _SubjectName = value; }
        }
        [Column(Name = "DayNumber", DataType = "Int16")]
        public Int16 DayNumber
        {
            get { return _DayNumber; }
            set { _DayNumber = value; }
        }
        [Column(Name = "HoursIndex", DataType = "Int16")]
        public Int16 HoursIndex
        {
            get { return _HoursIndex; }
            set { _HoursIndex = value; }
        }
        [Column(Name = "StartTime", DataType = "String")]
        public String StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        [Column(Name = "EndTime", DataType = "String")]
        public String EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        [Column(Name = "RoomID", DataType = "Int32")]
        public Int32 RoomID
        {
            get { return _RoomID; }
            set { _RoomID = value; }
        }
        [Column(Name = "RoomName", DataType = "String")]
        public String RoomName
        {
            get { return _RoomName; }
            set { _RoomName = value; }
        }
        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
        }
        [Column(Name = "TeacherName", DataType = "String")]
        public String TeacherName
        {
            get { return _TeacherName; }
            set { _TeacherName = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vClassStudent
    [Serializable]
    [Table(Name = "vClassStudent")]
    public class vClassStudent
    {
        private Int32 _SchoolClassID;
        private Int32 _StudentID;
        private String _StudentName;

        [Column(Name = "SchoolClassID", DataType = "Int32")]
        public Int32 SchoolClassID
        {
            get { return _SchoolClassID; }
            set { _SchoolClassID = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "StudentName", DataType = "String")]
        public String StudentName
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }
    }
    #endregion
    #region vClassSubject
    [Serializable]
    [Table(Name = "vClassSubject")]
    public class vClassSubject
    {
        private Int32 _ClassSubjectID;
        private Int32 _SchoolClassID;
        private String _SchoolClassName;
        private Int32 _PeriodClassTypeSubjectID;
        private Int32 _SubjectID;
        private String _SubjectCode;
        private String _SubjectName;
        private Int16 _NoMeetingHoursInWeek;
        private Int32 _TeacherID;
        private String _TeacherCode;
        private String _TeacherName;
        private Int32 _RoomID;
        private String _RoomName;
        private Boolean _IsDeleted;

        [Column(Name = "ClassSubjectID", DataType = "Int32")]
        public Int32 ClassSubjectID
        {
            get { return _ClassSubjectID; }
            set { _ClassSubjectID = value; }
        }
        [Column(Name = "SchoolClassID", DataType = "Int32")]
        public Int32 SchoolClassID
        {
            get { return _SchoolClassID; }
            set { _SchoolClassID = value; }
        }
        [Column(Name = "SchoolClassName", DataType = "String")]
        public String SchoolClassName
        {
            get { return _SchoolClassName; }
            set { _SchoolClassName = value; }
        }
        [Column(Name = "PeriodClassTypeSubjectID", DataType = "Int32")]
        public Int32 PeriodClassTypeSubjectID
        {
            get { return _PeriodClassTypeSubjectID; }
            set { _PeriodClassTypeSubjectID = value; }
        }
        [Column(Name = "SubjectID", DataType = "Int32")]
        public Int32 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
        [Column(Name = "SubjectCode", DataType = "String")]
        public String SubjectCode
        {
            get { return _SubjectCode; }
            set { _SubjectCode = value; }
        }
        [Column(Name = "SubjectName", DataType = "String")]
        public String SubjectName
        {
            get { return _SubjectName; }
            set { _SubjectName = value; }
        }
        [Column(Name = "NoMeetingHoursInWeek", DataType = "Int16")]
        public Int16 NoMeetingHoursInWeek
        {
            get { return _NoMeetingHoursInWeek; }
            set { _NoMeetingHoursInWeek = value; }
        }
        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
        }
        [Column(Name = "TeacherCode", DataType = "String")]
        public String TeacherCode
        {
            get { return _TeacherCode; }
            set { _TeacherCode = value; }
        }
        [Column(Name = "TeacherName", DataType = "String")]
        public String TeacherName
        {
            get { return _TeacherName; }
            set { _TeacherName = value; }
        }
        [Column(Name = "RoomID", DataType = "Int32")]
        public Int32 RoomID
        {
            get { return _RoomID; }
            set { _RoomID = value; }
        }
        [Column(Name = "RoomName", DataType = "String")]
        public String RoomName
        {
            get { return _RoomName; }
            set { _RoomName = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vClassSubjectCustom
    [Serializable]
    [Table(Name = "vClassSubjectCustom")]
    public class vClassSubjectCustom
    {
        private Int32 _SchoolClassID;
        private String _SchoolClassName;
        private Int32 _SchoolPeriodID;
        private Int32 _PeriodClassTypeSubjectID;
        private Int32 _SubjectID;
        private String _SubjectCode;
        private String _SubjectName;
        private String _TeacherName;

        [Column(Name = "SchoolClassID", DataType = "Int32")]
        public Int32 SchoolClassID
        {
            get { return _SchoolClassID; }
            set { _SchoolClassID = value; }
        }
        [Column(Name = "SchoolClassName", DataType = "String")]
        public String SchoolClassName
        {
            get { return _SchoolClassName; }
            set { _SchoolClassName = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "PeriodClassTypeSubjectID", DataType = "Int32")]
        public Int32 PeriodClassTypeSubjectID
        {
            get { return _PeriodClassTypeSubjectID; }
            set { _PeriodClassTypeSubjectID = value; }
        }
        [Column(Name = "SubjectID", DataType = "Int32")]
        public Int32 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
        [Column(Name = "SubjectCode", DataType = "String")]
        public String SubjectCode
        {
            get { return _SubjectCode; }
            set { _SubjectCode = value; }
        }
        [Column(Name = "SubjectName", DataType = "String")]
        public String SubjectName
        {
            get { return _SubjectName; }
            set { _SubjectName = value; }
        }
        [Column(Name = "TeacherName", DataType = "String")]
        public String TeacherName
        {
            get { return _TeacherName; }
            set { _TeacherName = value; }
        }
    }
    #endregion
    #region vClassSubjectTask
    [Serializable]
    [Table(Name = "vClassSubjectTask")]
    public class vClassSubjectTask
    {
        private Int32 _ClassSubjectTaskID;
        private Int32 _ClassSubjectID;
        private String _GCTaskType;
        private String _TaskType;
        private Int16 _FinalMarkPercentage;
        private DateTime _TaskDate;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private String _StartTime;
        private String _EndTime;
        private String _Topic;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "ClassSubjectTaskID", DataType = "Int32")]
        public Int32 ClassSubjectTaskID
        {
            get { return _ClassSubjectTaskID; }
            set { _ClassSubjectTaskID = value; }
        }
        [Column(Name = "ClassSubjectID", DataType = "Int32")]
        public Int32 ClassSubjectID
        {
            get { return _ClassSubjectID; }
            set { _ClassSubjectID = value; }
        }
        [Column(Name = "GCTaskType", DataType = "String")]
        public String GCTaskType
        {
            get { return _GCTaskType; }
            set { _GCTaskType = value; }
        }
        [Column(Name = "TaskType", DataType = "String")]
        public String TaskType
        {
            get { return _TaskType; }
            set { _TaskType = value; }
        }
        [Column(Name = "FinalMarkPercentage", DataType = "Int16")]
        public Int16 FinalMarkPercentage
        {
            get { return _FinalMarkPercentage; }
            set { _FinalMarkPercentage = value; }
        }
        [Column(Name = "TaskDate", DataType = "DateTime")]
        public DateTime TaskDate
        {
            get { return _TaskDate; }
            set { _TaskDate = value; }
        }
        [Column(Name = "StartDate", DataType = "DateTime")]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Column(Name = "EndDate", DataType = "DateTime")]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        [Column(Name = "StartTime", DataType = "String")]
        public String StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        [Column(Name = "EndTime", DataType = "String")]
        public String EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        [Column(Name = "Topic", DataType = "String")]
        public String Topic
        {
            get { return _Topic; }
            set { _Topic = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vClassType
    [Serializable]
    [Table(Name = "vClassType")]
    public class vClassType
    {
        private Int32 _ClassTypeID;
        private String _ClassTypeCode;
        private String _ClassTypeName;
        private String _SiteID;
        private String _GCGrade;
        private String _Grade;
        private String _GCMajor;
        private String _Major;
        private Boolean _IsDeleted;

        [Column(Name = "ClassTypeID", DataType = "Int32")]
        public Int32 ClassTypeID
        {
            get { return _ClassTypeID; }
            set { _ClassTypeID = value; }
        }
        [Column(Name = "ClassTypeCode", DataType = "String")]
        public String ClassTypeCode
        {
            get { return _ClassTypeCode; }
            set { _ClassTypeCode = value; }
        }
        [Column(Name = "ClassTypeName", DataType = "String")]
        public String ClassTypeName
        {
            get { return _ClassTypeName; }
            set { _ClassTypeName = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "GCGrade", DataType = "String")]
        public String GCGrade
        {
            get { return _GCGrade; }
            set { _GCGrade = value; }
        }
        [Column(Name = "Grade", DataType = "String")]
        public String Grade
        {
            get { return _Grade; }
            set { _Grade = value; }
        }
        [Column(Name = "GCMajor", DataType = "String")]
        public String GCMajor
        {
            get { return _GCMajor; }
            set { _GCMajor = value; }
        }
        [Column(Name = "Major", DataType = "String")]
        public String Major
        {
            get { return _Major; }
            set { _Major = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vDailyScheduleTypeDt
    [Serializable]
    [Table(Name = "vDailyScheduleTypeDt")]
    public class vDailyScheduleTypeDt
    {
        private Int32 _DailyScheduleTypeDtID;
        private Int32 _DailyScheduleTypeID;
        private Int16 _HoursIndex;
        private String _StartTime;
        private String _EndTime;
        private String _GCDailyScheduleType;
        private String _DailyScheduleType;

        [Column(Name = "DailyScheduleTypeDtID", DataType = "Int32")]
        public Int32 DailyScheduleTypeDtID
        {
            get { return _DailyScheduleTypeDtID; }
            set { _DailyScheduleTypeDtID = value; }
        }
        [Column(Name = "DailyScheduleTypeID", DataType = "Int32")]
        public Int32 DailyScheduleTypeID
        {
            get { return _DailyScheduleTypeID; }
            set { _DailyScheduleTypeID = value; }
        }
        [Column(Name = "HoursIndex", DataType = "Int16")]
        public Int16 HoursIndex
        {
            get { return _HoursIndex; }
            set { _HoursIndex = value; }
        }
        [Column(Name = "StartTime", DataType = "String")]
        public String StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        [Column(Name = "EndTime", DataType = "String")]
        public String EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        [Column(Name = "GCDailyScheduleType", DataType = "String")]
        public String GCDailyScheduleType
        {
            get { return _GCDailyScheduleType; }
            set { _GCDailyScheduleType = value; }
        }
        [Column(Name = "DailyScheduleType", DataType = "String")]
        public String DailyScheduleType
        {
            get { return _DailyScheduleType; }
            set { _DailyScheduleType = value; }
        }
    }
    #endregion
    #region vDirectPurchaseDt
    [Serializable]
    [Table(Name = "vDirectPurchaseDt")]
    public partial class vDirectPurchaseDt
    {
        private Int32 _ID;
        private Int32 _DirectPurchaseID;
        private String _DirectPurchaseNo;
        private DateTime _PurchaseDate;
        private Int32 _LocationID;
        private String _LocationCode;
        private String _LocationName;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private Decimal _VATPercentage;
        private Decimal _TotalTransactionAmount;
        private String _CreatedByUserName;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Decimal _Quantity;
        private String _GCItemUnit;
        private String _ItemUnit;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private Decimal _ConversionFactor;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage;
        private String _GCItemDetailStatus;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "DirectPurchaseID", DataType = "Int32")]
        public Int32 DirectPurchaseID
        {
            get { return _DirectPurchaseID; }
            set { _DirectPurchaseID = value; }
        }
        [Column(Name = "DirectPurchaseNo", DataType = "String")]
        public String DirectPurchaseNo
        {
            get { return _DirectPurchaseNo; }
            set { _DirectPurchaseNo = value; }
        }
        [Column(Name = "PurchaseDate", DataType = "DateTime")]
        public DateTime PurchaseDate
        {
            get { return _PurchaseDate; }
            set { _PurchaseDate = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "LocationCode", DataType = "String")]
        public String LocationCode
        {
            get { return _LocationCode; }
            set { _LocationCode = value; }
        }
        [Column(Name = "LocationName", DataType = "String")]
        public String LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "VATPercentage", DataType = "Decimal")]
        public Decimal VATPercentage
        {
            get { return _VATPercentage; }
            set { _VATPercentage = value; }
        }
        [Column(Name = "TotalTransactionAmount", DataType = "Decimal")]
        public Decimal TotalTransactionAmount
        {
            get { return _TotalTransactionAmount; }
            set { _TotalTransactionAmount = value; }
        }
        [Column(Name = "CreatedByUserName", DataType = "String")]
        public String CreatedByUserName
        {
            get { return _CreatedByUserName; }
            set { _CreatedByUserName = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "UnitPrice", DataType = "Decimal")]
        public Decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        [Column(Name = "DiscountPercentage", DataType = "Decimal")]
        public Decimal DiscountPercentage
        {
            get { return _DiscountPercentage; }
            set { _DiscountPercentage = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
    }
    #endregion
    #region vDirectPurchaseHd
    [Serializable]
    [Table(Name = "vDirectPurchaseHd")]
    public partial class vDirectPurchaseHd
    {
        private Int32 _DirectPurchaseID;
        private String _DirectPurchaseNo;
        private DateTime _PurchaseDate;
        private Int32 _LocationID;
        private String _LocationCode;
        private String _LocationName;
        private Int32 _LocationItemGroupID;
        private Boolean _IsHasPurchaseReturn;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private String _GCDirectPurchaseType;
        private String _ReferenceNo;
        private DateTime _ReferenceDate;
        private Boolean _IsIncludeVAT;
        private Decimal _TransactionAmount;
        private Decimal _VATPercentage;
        private String _Remarks;
        private String _GCTransactionStatus;
        private String _TransactionStatusWatermark;
        private Int32 _CreatedBy;
        private String _CreatedByUserName;

        [Column(Name = "DirectPurchaseID", DataType = "Int32")]
        public Int32 DirectPurchaseID
        {
            get { return _DirectPurchaseID; }
            set { _DirectPurchaseID = value; }
        }
        [Column(Name = "DirectPurchaseNo", DataType = "String")]
        public String DirectPurchaseNo
        {
            get { return _DirectPurchaseNo; }
            set { _DirectPurchaseNo = value; }
        }
        [Column(Name = "PurchaseDate", DataType = "DateTime")]
        public DateTime PurchaseDate
        {
            get { return _PurchaseDate; }
            set { _PurchaseDate = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "LocationCode", DataType = "String")]
        public String LocationCode
        {
            get { return _LocationCode; }
            set { _LocationCode = value; }
        }
        [Column(Name = "LocationName", DataType = "String")]
        public String LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }
        [Column(Name = "LocationItemGroupID", DataType = "Int32")]
        public Int32 LocationItemGroupID
        {
            get { return _LocationItemGroupID; }
            set { _LocationItemGroupID = value; }
        }
        [Column(Name = "IsHasPurchaseReturn", DataType = "Boolean")]
        public Boolean IsHasPurchaseReturn
        {
            get { return _IsHasPurchaseReturn; }
            set { _IsHasPurchaseReturn = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "GCDirectPurchaseType", DataType = "String")]
        public String GCDirectPurchaseType
        {
            get { return _GCDirectPurchaseType; }
            set { _GCDirectPurchaseType = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String")]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "ReferenceDate", DataType = "DateTime")]
        public DateTime ReferenceDate
        {
            get { return _ReferenceDate; }
            set { _ReferenceDate = value; }
        }
        [Column(Name = "IsIncludeVAT", DataType = "Boolean")]
        public Boolean IsIncludeVAT
        {
            get { return _IsIncludeVAT; }
            set { _IsIncludeVAT = value; }
        }
        [Column(Name = "TransactionAmount", DataType = "Decimal")]
        public Decimal TransactionAmount
        {
            get { return _TransactionAmount; }
            set { _TransactionAmount = value; }
        }
        [Column(Name = "VATPercentage", DataType = "Decimal")]
        public Decimal VATPercentage
        {
            get { return _VATPercentage; }
            set { _VATPercentage = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedByUserName", DataType = "String")]
        public String CreatedByUserName
        {
            get { return _CreatedByUserName; }
            set { _CreatedByUserName = value; }
        }
    }
    #endregion
    #region vDirectPurchaseReturnDt
    [Serializable]
    [Table(Name = "vDirectPurchaseReturnDt")]
    public partial class vDirectPurchaseReturnDt
    {
        private Int32 _ID;
        private Int32 _DirectPurchaseReturnID;
        private String _DirectPurchaseReturnNo;
        private DateTime _ReturnDate;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _SupplierName;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Decimal _ReceivedQuantity;
        private String _ReceivedItemUnit;
        private Decimal _Quantity;
        private String _ItemUnit;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private Decimal _ConversionFactor;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage1;
        private Decimal _DiscountPercentage2;
        private String _GCPurchaseReturnReason;
        private String _PurchaseReturnReason;
        private Int32 _SupplierID;
        private String _GCTransactionStatus;
        private String _GCItemUnit;
        private String _GCItemDetailStatus;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "DirectPurchaseReturnID", DataType = "Int32")]
        public Int32 DirectPurchaseReturnID
        {
            get { return _DirectPurchaseReturnID; }
            set { _DirectPurchaseReturnID = value; }
        }
        [Column(Name = "DirectPurchaseReturnNo", DataType = "String")]
        public String DirectPurchaseReturnNo
        {
            get { return _DirectPurchaseReturnNo; }
            set { _DirectPurchaseReturnNo = value; }
        }
        [Column(Name = "ReturnDate", DataType = "DateTime")]
        public DateTime ReturnDate
        {
            get { return _ReturnDate; }
            set { _ReturnDate = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "SupplierName", DataType = "String")]
        public String SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "ReceivedQuantity", DataType = "Decimal")]
        public Decimal ReceivedQuantity
        {
            get { return _ReceivedQuantity; }
            set { _ReceivedQuantity = value; }
        }
        [Column(Name = "ReceivedItemUnit", DataType = "String")]
        public String ReceivedItemUnit
        {
            get { return _ReceivedItemUnit; }
            set { _ReceivedItemUnit = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "UnitPrice", DataType = "Decimal")]
        public Decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        [Column(Name = "DiscountPercentage1", DataType = "Decimal")]
        public Decimal DiscountPercentage1
        {
            get { return _DiscountPercentage1; }
            set { _DiscountPercentage1 = value; }
        }
        [Column(Name = "DiscountPercentage2", DataType = "Decimal")]
        public Decimal DiscountPercentage2
        {
            get { return _DiscountPercentage2; }
            set { _DiscountPercentage2 = value; }
        }
        [Column(Name = "GCPurchaseReturnReason", DataType = "String")]
        public String GCPurchaseReturnReason
        {
            get { return _GCPurchaseReturnReason; }
            set { _GCPurchaseReturnReason = value; }
        }
        [Column(Name = "PurchaseReturnReason", DataType = "String")]
        public String PurchaseReturnReason
        {
            get { return _PurchaseReturnReason; }
            set { _PurchaseReturnReason = value; }
        }
        [Column(Name = "SupplierID", DataType = "Int32")]
        public Int32 SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
    }
    #endregion
    #region vDirectPurchaseReturnHd
    [Serializable]
    [Table(Name = "vDirectPurchaseReturnHd")]
    public partial class vDirectPurchaseReturnHd
    {
        private Int32 _DirectPurchaseReturnID;
        private DateTime _ReturnDate;
        private String _DirectPurchaseReturnNo;
        private Int32 _DirectPurchaseID;
        private String _DirectPurchaseNo;
        private Int32 _LocationID;
        private String _LocationCode;
        private String _LocationName;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _SupplierName;
        private String _GCDirectPurchaseReturnType;
        private String _PurchaseReturnType;
        private String _ReferenceNo;
        private DateTime _ReferenceDate;
        private Boolean _IsIncludeVAT;
        private Decimal _TransactionAmount;
        private Decimal _VATPercentage;
        private String _Remarks;
        private String _GCTransactionStatus;
        private String _TransactionStatusWatermark;
        private Int32 _NumberOfItems;
        private Int32 _NumberOfApprovedItems;

        [Column(Name = "DirectPurchaseReturnID", DataType = "Int32")]
        public Int32 DirectPurchaseReturnID
        {
            get { return _DirectPurchaseReturnID; }
            set { _DirectPurchaseReturnID = value; }
        }
        [Column(Name = "ReturnDate", DataType = "DateTime")]
        public DateTime ReturnDate
        {
            get { return _ReturnDate; }
            set { _ReturnDate = value; }
        }
        [Column(Name = "DirectPurchaseReturnNo", DataType = "String")]
        public String DirectPurchaseReturnNo
        {
            get { return _DirectPurchaseReturnNo; }
            set { _DirectPurchaseReturnNo = value; }
        }
        [Column(Name = "DirectPurchaseID", DataType = "Int32")]
        public Int32 DirectPurchaseID
        {
            get { return _DirectPurchaseID; }
            set { _DirectPurchaseID = value; }
        }
        [Column(Name = "DirectPurchaseNo", DataType = "String")]
        public String DirectPurchaseNo
        {
            get { return _DirectPurchaseNo; }
            set { _DirectPurchaseNo = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "LocationCode", DataType = "String")]
        public String LocationCode
        {
            get { return _LocationCode; }
            set { _LocationCode = value; }
        }
        [Column(Name = "LocationName", DataType = "String")]
        public String LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "SupplierName", DataType = "String")]
        public String SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }
        [Column(Name = "GCDirectPurchaseReturnType", DataType = "String")]
        public String GCDirectPurchaseReturnType
        {
            get { return _GCDirectPurchaseReturnType; }
            set { _GCDirectPurchaseReturnType = value; }
        }
        [Column(Name = "PurchaseReturnType", DataType = "String")]
        public String PurchaseReturnType
        {
            get { return _PurchaseReturnType; }
            set { _PurchaseReturnType = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String")]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "ReferenceDate", DataType = "DateTime")]
        public DateTime ReferenceDate
        {
            get { return _ReferenceDate; }
            set { _ReferenceDate = value; }
        }
        [Column(Name = "IsIncludeVAT", DataType = "Boolean")]
        public Boolean IsIncludeVAT
        {
            get { return _IsIncludeVAT; }
            set { _IsIncludeVAT = value; }
        }
        [Column(Name = "TransactionAmount", DataType = "Decimal")]
        public Decimal TransactionAmount
        {
            get { return _TransactionAmount; }
            set { _TransactionAmount = value; }
        }
        [Column(Name = "VATPercentage", DataType = "Decimal")]
        public Decimal VATPercentage
        {
            get { return _VATPercentage; }
            set { _VATPercentage = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
        [Column(Name = "NumberOfItems", DataType = "Int32")]
        public Int32 NumberOfItems
        {
            get { return _NumberOfItems; }
            set { _NumberOfItems = value; }
        }
        [Column(Name = "NumberOfApprovedItems", DataType = "Int32")]
        public Int32 NumberOfApprovedItems
        {
            get { return _NumberOfApprovedItems; }
            set { _NumberOfApprovedItems = value; }
        }
    }
    #endregion
    #region vItemBalance
    [Serializable]
    [Table(Name = "vItemBalance")]
    public partial class vItemBalance
    {
        private Int32 _ID;
        private Int32 _LocationID;
        private String _SiteID;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private String _GCItemType;
        private String _GCItemUnit;
        private String _ItemUnit;
        private String _GCPurchaseUnit;
        private Boolean _IsControlExpired;
        private String _PurchaseUnit;
        private Int32 _ItemGroupID;
        private String _GCReorderType;
        private Decimal _QuantityMIN;
        private Decimal _QuantityMAX;
        private Decimal _QuantityBEGIN;
        private Decimal _QuantityIN;
        private Decimal _QuantityOUT;
        private Decimal _QuantityEND;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "GCItemType", DataType = "String")]
        public String GCItemType
        {
            get { return _GCItemType; }
            set { _GCItemType = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "GCPurchaseUnit", DataType = "String")]
        public String GCPurchaseUnit
        {
            get { return _GCPurchaseUnit; }
            set { _GCPurchaseUnit = value; }
        }
        [Column(Name = "IsControlExpired", DataType = "Boolean")]
        public Boolean IsControlExpired
        {
            get { return _IsControlExpired; }
            set { _IsControlExpired = value; }
        }
        [Column(Name = "PurchaseUnit", DataType = "String")]
        public String PurchaseUnit
        {
            get { return _PurchaseUnit; }
            set { _PurchaseUnit = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "GCReorderType", DataType = "String")]
        public String GCReorderType
        {
            get { return _GCReorderType; }
            set { _GCReorderType = value; }
        }
        [Column(Name = "QuantityMIN", DataType = "Decimal")]
        public Decimal QuantityMIN
        {
            get { return _QuantityMIN; }
            set { _QuantityMIN = value; }
        }
        [Column(Name = "QuantityMAX", DataType = "Decimal")]
        public Decimal QuantityMAX
        {
            get { return _QuantityMAX; }
            set { _QuantityMAX = value; }
        }
        [Column(Name = "QuantityBEGIN", DataType = "Decimal")]
        public Decimal QuantityBEGIN
        {
            get { return _QuantityBEGIN; }
            set { _QuantityBEGIN = value; }
        }
        [Column(Name = "QuantityIN", DataType = "Decimal")]
        public Decimal QuantityIN
        {
            get { return _QuantityIN; }
            set { _QuantityIN = value; }
        }
        [Column(Name = "QuantityOUT", DataType = "Decimal")]
        public Decimal QuantityOUT
        {
            get { return _QuantityOUT; }
            set { _QuantityOUT = value; }
        }
        [Column(Name = "QuantityEND", DataType = "Decimal")]
        public Decimal QuantityEND
        {
            get { return _QuantityEND; }
            set { _QuantityEND = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vItemBalanceInventory
    [Serializable]
    [Table(Name = "vItemBalanceInventory")]
    public partial class vItemBalanceInventory
    {
        private Int32 _ID;
        private Int32 _LocationID;
        private String _SiteID;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private String _GCItemType;
        private String _GCItemUnit;
        private String _ItemUnit;
        private String _GCPurchaseUnit;
        private Boolean _IsControlExpired;
        private String _PurchaseUnit;
        private Int32 _ItemGroupID;
        private String _GCReorderType;
        private Decimal _QuantityMIN;
        private Decimal _QuantityMAX;
        private Decimal _QuantityBEGIN;
        private Decimal _QuantityIN;
        private Decimal _QuantityOUT;
        private Decimal _QuantityEND;
        private Boolean _IsDeleted;
        private Decimal _ItemRequestQtyOnOrder;
        private Decimal _PurchaseRequestQtyOnOrder;
        private Decimal _PurchaseOrderQtyOnOrder;
        private Decimal _ItemDistributionQtyOnOrder;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "GCItemType", DataType = "String")]
        public String GCItemType
        {
            get { return _GCItemType; }
            set { _GCItemType = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "GCPurchaseUnit", DataType = "String")]
        public String GCPurchaseUnit
        {
            get { return _GCPurchaseUnit; }
            set { _GCPurchaseUnit = value; }
        }
        [Column(Name = "IsControlExpired", DataType = "Boolean")]
        public Boolean IsControlExpired
        {
            get { return _IsControlExpired; }
            set { _IsControlExpired = value; }
        }
        [Column(Name = "PurchaseUnit", DataType = "String")]
        public String PurchaseUnit
        {
            get { return _PurchaseUnit; }
            set { _PurchaseUnit = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "GCReorderType", DataType = "String")]
        public String GCReorderType
        {
            get { return _GCReorderType; }
            set { _GCReorderType = value; }
        }
        [Column(Name = "QuantityMIN", DataType = "Decimal")]
        public Decimal QuantityMIN
        {
            get { return _QuantityMIN; }
            set { _QuantityMIN = value; }
        }
        [Column(Name = "QuantityMAX", DataType = "Decimal")]
        public Decimal QuantityMAX
        {
            get { return _QuantityMAX; }
            set { _QuantityMAX = value; }
        }
        [Column(Name = "QuantityBEGIN", DataType = "Decimal")]
        public Decimal QuantityBEGIN
        {
            get { return _QuantityBEGIN; }
            set { _QuantityBEGIN = value; }
        }
        [Column(Name = "QuantityIN", DataType = "Decimal")]
        public Decimal QuantityIN
        {
            get { return _QuantityIN; }
            set { _QuantityIN = value; }
        }
        [Column(Name = "QuantityOUT", DataType = "Decimal")]
        public Decimal QuantityOUT
        {
            get { return _QuantityOUT; }
            set { _QuantityOUT = value; }
        }
        [Column(Name = "QuantityEND", DataType = "Decimal")]
        public Decimal QuantityEND
        {
            get { return _QuantityEND; }
            set { _QuantityEND = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "ItemRequestQtyOnOrder", DataType = "Decimal")]
        public Decimal ItemRequestQtyOnOrder
        {
            get { return _ItemRequestQtyOnOrder; }
            set { _ItemRequestQtyOnOrder = value; }
        }
        [Column(Name = "PurchaseRequestQtyOnOrder", DataType = "Decimal")]
        public Decimal PurchaseRequestQtyOnOrder
        {
            get { return _PurchaseRequestQtyOnOrder; }
            set { _PurchaseRequestQtyOnOrder = value; }
        }
        [Column(Name = "PurchaseOrderQtyOnOrder", DataType = "Decimal")]
        public Decimal PurchaseOrderQtyOnOrder
        {
            get { return _PurchaseOrderQtyOnOrder; }
            set { _PurchaseOrderQtyOnOrder = value; }
        }
        [Column(Name = "ItemDistributionQtyOnOrder", DataType = "Decimal")]
        public Decimal ItemDistributionQtyOnOrder
        {
            get { return _ItemDistributionQtyOnOrder; }
            set { _ItemDistributionQtyOnOrder = value; }
        }
    }
    #endregion
    #region vItemDistributionDt
    [Serializable]
    [Table(Name = "vItemDistributionDt")]
    public partial class vItemDistributionDt
    {
        private Int32 _ID;
        private String _DistributionNo;
        private Int32 _DistributionID;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private String _ItemName2;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private DateTime _DeliveryDate;
        private Int32 _FromLocationID;
        private String _FromLocationName;
        private Int32 _ToLocationID;
        private String _ToLocationName;
        private Decimal _Quantity;
        private String _GCItemUnit;
        private String _ItemUnit;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private Decimal _ConversionFactor;
        private String _GCItemDetailStatus;
        private Boolean _isDeleted;
        private String _LastUpdateByName;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "DistributionNo", DataType = "String")]
        public String DistributionNo
        {
            get { return _DistributionNo; }
            set { _DistributionNo = value; }
        }
        [Column(Name = "DistributionID", DataType = "Int32")]
        public Int32 DistributionID
        {
            get { return _DistributionID; }
            set { _DistributionID = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemName2", DataType = "String")]
        public String ItemName2
        {
            get { return _ItemName2; }
            set { _ItemName2 = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "DeliveryDate", DataType = "DateTime")]
        public DateTime DeliveryDate
        {
            get { return _DeliveryDate; }
            set { _DeliveryDate = value; }
        }
        [Column(Name = "FromLocationID", DataType = "Int32")]
        public Int32 FromLocationID
        {
            get { return _FromLocationID; }
            set { _FromLocationID = value; }
        }
        [Column(Name = "FromLocationName", DataType = "String")]
        public String FromLocationName
        {
            get { return _FromLocationName; }
            set { _FromLocationName = value; }
        }
        [Column(Name = "ToLocationID", DataType = "Int32")]
        public Int32 ToLocationID
        {
            get { return _ToLocationID; }
            set { _ToLocationID = value; }
        }
        [Column(Name = "ToLocationName", DataType = "String")]
        public String ToLocationName
        {
            get { return _ToLocationName; }
            set { _ToLocationName = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
        [Column(Name = "isDeleted", DataType = "Boolean")]
        public Boolean isDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }
        [Column(Name = "LastUpdateByName", DataType = "String")]
        public String LastUpdateByName
        {
            get { return _LastUpdateByName; }
            set { _LastUpdateByName = value; }
        }

    }
    #endregion
    #region vItemDistributionHd
    [Serializable]
    [Table(Name = "vItemDistributionHd")]
    public partial class vItemDistributionHd
    {
        private Int32 _DistributionID;
        private String _DistributionNo;
        private Int32 _ItemRequestID;
        private DateTime _DeliveryDate;
        private String _DeliveryTime;
        private Int32 _FromLocationID;
        private String _FromLocationCode;
        private String _FromLocationName;
        private Int32 _FromLocationItemGroupID;
        private Int32 _ToLocationID;
        private String _ToLocationCode;
        private String _ToLocationName;
        private String _DeliveredBy;
        private String _GCDistributionStatus;
        private String _DistributionStatus;
        private String _DistributionStatusWatermark;
        private Boolean _isGeneratedBySystem;
        private String _DeliveryRemarks;
        private Int32 _NumberOfItems;
        private Int32 _NumberOfApprovedItems;

        [Column(Name = "DistributionID", DataType = "Int32")]
        public Int32 DistributionID
        {
            get { return _DistributionID; }
            set { _DistributionID = value; }
        }
        [Column(Name = "DistributionNo", DataType = "String")]
        public String DistributionNo
        {
            get { return _DistributionNo; }
            set { _DistributionNo = value; }
        }
        [Column(Name = "ItemRequestID", DataType = "Int32")]
        public Int32 ItemRequestID
        {
            get { return _ItemRequestID; }
            set { _ItemRequestID = value; }
        }
        [Column(Name = "DeliveryDate", DataType = "DateTime")]
        public DateTime DeliveryDate
        {
            get { return _DeliveryDate; }
            set { _DeliveryDate = value; }
        }
        [Column(Name = "DeliveryTime", DataType = "String")]
        public String DeliveryTime
        {
            get { return _DeliveryTime; }
            set { _DeliveryTime = value; }
        }
        [Column(Name = "FromLocationID", DataType = "Int32")]
        public Int32 FromLocationID
        {
            get { return _FromLocationID; }
            set { _FromLocationID = value; }
        }
        [Column(Name = "FromLocationCode", DataType = "String")]
        public String FromLocationCode
        {
            get { return _FromLocationCode; }
            set { _FromLocationCode = value; }
        }
        [Column(Name = "FromLocationName", DataType = "String")]
        public String FromLocationName
        {
            get { return _FromLocationName; }
            set { _FromLocationName = value; }
        }
        [Column(Name = "FromLocationItemGroupID", DataType = "Int32")]
        public Int32 FromLocationItemGroupID
        {
            get { return _FromLocationItemGroupID; }
            set { _FromLocationItemGroupID = value; }
        }
        [Column(Name = "ToLocationID", DataType = "Int32")]
        public Int32 ToLocationID
        {
            get { return _ToLocationID; }
            set { _ToLocationID = value; }
        }
        [Column(Name = "ToLocationCode", DataType = "String")]
        public String ToLocationCode
        {
            get { return _ToLocationCode; }
            set { _ToLocationCode = value; }
        }
        [Column(Name = "ToLocationName", DataType = "String")]
        public String ToLocationName
        {
            get { return _ToLocationName; }
            set { _ToLocationName = value; }
        }
        [Column(Name = "DeliveredBy", DataType = "String")]
        public String DeliveredBy
        {
            get { return _DeliveredBy; }
            set { _DeliveredBy = value; }
        }
        [Column(Name = "GCDistributionStatus", DataType = "String")]
        public String GCDistributionStatus
        {
            get { return _GCDistributionStatus; }
            set { _GCDistributionStatus = value; }
        }
        [Column(Name = "DistributionStatus", DataType = "String")]
        public String DistributionStatus
        {
            get { return _DistributionStatus; }
            set { _DistributionStatus = value; }
        }
        [Column(Name = "DistributionStatusWatermark", DataType = "String")]
        public String DistributionStatusWatermark
        {
            get { return _DistributionStatusWatermark; }
            set { _DistributionStatusWatermark = value; }
        }
        [Column(Name = "isGeneratedBySystem", DataType = "Boolean")]
        public Boolean isGeneratedBySystem
        {
            get { return _isGeneratedBySystem; }
            set { _isGeneratedBySystem = value; }
        }
        [Column(Name = "DeliveryRemarks", DataType = "String")]
        public String DeliveryRemarks
        {
            get { return _DeliveryRemarks; }
            set { _DeliveryRemarks = value; }
        }
        [Column(Name = "NumberOfItems", DataType = "Int32")]
        public Int32 NumberOfItems
        {
            get { return _NumberOfItems; }
            set { _NumberOfItems = value; }
        }
        [Column(Name = "NumberOfApprovedItems", DataType = "Int32")]
        public Int32 NumberOfApprovedItems
        {
            get { return _NumberOfApprovedItems; }
            set { _NumberOfApprovedItems = value; }
        }
    }
    #endregion
    #region vItemRequestDt
    [Serializable]
    [Table(Name = "vItemRequestDt")]
    public partial class vItemRequestDt
    {
        private Int32 _ID;
        private Int32 _ItemRequestID;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private String _ItemName2;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Decimal _Quantity;
        private String _GCItemUnit;
        private String _ItemUnit;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private Decimal _ConversionFactor;
        private Decimal _DistributionQty;
        private Decimal _ConsumptionQty;
        private Decimal _PurchaseRequestQty;
        private Decimal _PurchaseRequestReceivedQty;
        private String _GCItemDetailStatus;
        private Int32 _ToLocationID;
        private Decimal _EndingBalance;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "ItemRequestID", DataType = "Int32")]
        public Int32 ItemRequestID
        {
            get { return _ItemRequestID; }
            set { _ItemRequestID = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemName2", DataType = "String")]
        public String ItemName2
        {
            get { return _ItemName2; }
            set { _ItemName2 = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "DistributionQty", DataType = "Decimal")]
        public Decimal DistributionQty
        {
            get { return _DistributionQty; }
            set { _DistributionQty = value; }
        }
        [Column(Name = "ConsumptionQty", DataType = "Decimal")]
        public Decimal ConsumptionQty
        {
            get { return _ConsumptionQty; }
            set { _ConsumptionQty = value; }
        }
        [Column(Name = "PurchaseRequestQty", DataType = "Decimal")]
        public Decimal PurchaseRequestQty
        {
            get { return _PurchaseRequestQty; }
            set { _PurchaseRequestQty = value; }
        }
        [Column(Name = "PurchaseRequestReceivedQty", DataType = "Decimal")]
        public Decimal PurchaseRequestReceivedQty
        {
            get { return _PurchaseRequestReceivedQty; }
            set { _PurchaseRequestReceivedQty = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
        [Column(Name = "ToLocationID", DataType = "Int32")]
        public Int32 ToLocationID
        {
            get { return _ToLocationID; }
            set { _ToLocationID = value; }
        }
        [Column(Name = "EndingBalance", DataType = "Decimal")]
        public Decimal EndingBalance
        {
            get { return _EndingBalance; }
            set { _EndingBalance = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vItemRequestDtRealizationPerItem
    [Serializable]
    [Table(Name = "vItemRequestDtRealizationPerItem")]
    public class vItemRequestDtRealizationPerItem
    {
        private Int32 _ItemID;
        private Decimal _ItemRequestQuantity;
        private Decimal _PurchaseRequestOrderQty;
        private Decimal _PurchaseRequestReceivedQty;

        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemRequestQuantity", DataType = "Decimal")]
        public Decimal ItemRequestQuantity
        {
            get { return _ItemRequestQuantity; }
            set { _ItemRequestQuantity = value; }
        }
        [Column(Name = "PurchaseRequestOrderQty", DataType = "Decimal")]
        public Decimal PurchaseRequestOrderQty
        {
            get { return _PurchaseRequestOrderQty; }
            set { _PurchaseRequestOrderQty = value; }
        }
        [Column(Name = "PurchaseRequestReceivedQty", DataType = "Decimal")]
        public Decimal PurchaseRequestReceivedQty
        {
            get { return _PurchaseRequestReceivedQty; }
            set { _PurchaseRequestReceivedQty = value; }
        }
    }
    #endregion
    #region vItemRequestDtRealizationPerItemPerOrder
    [Serializable]
    [Table(Name = "vItemRequestDtRealizationPerItemPerOrder")]
    public class vItemRequestDtRealizationPerItemPerOrder
    {
        private Int32 _ItemRequestID;
        private String _ItemRequestNo;
        private Int32 _ItemID;
        private String _ItemUnit;
        private Decimal _ItemRequestQuantity;
        private Decimal _PurchaseRequestOrderQty;
        private Decimal _PurchaseRequestReceivedQty;

        [Column(Name = "ItemRequestID", DataType = "Int32")]
        public Int32 ItemRequestID
        {
            get { return _ItemRequestID; }
            set { _ItemRequestID = value; }
        }
        [Column(Name = "ItemRequestNo", DataType = "String")]
        public String ItemRequestNo
        {
            get { return _ItemRequestNo; }
            set { _ItemRequestNo = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "ItemRequestQuantity", DataType = "Decimal")]
        public Decimal ItemRequestQuantity
        {
            get { return _ItemRequestQuantity; }
            set { _ItemRequestQuantity = value; }
        }
        [Column(Name = "PurchaseRequestOrderQty", DataType = "Decimal")]
        public Decimal PurchaseRequestOrderQty
        {
            get { return _PurchaseRequestOrderQty; }
            set { _PurchaseRequestOrderQty = value; }
        }
        [Column(Name = "PurchaseRequestReceivedQty", DataType = "Decimal")]
        public Decimal PurchaseRequestReceivedQty
        {
            get { return _PurchaseRequestReceivedQty; }
            set { _PurchaseRequestReceivedQty = value; }
        }
    }
    #endregion
    #region vItemRequestHd
    [Serializable]
    [Table(Name = "vItemRequestHd")]
    public partial class vItemRequestHd
    {
        private Int32 _ItemRequestID;
        private DateTime _TransactionDate;
        private String _TransactionTime;
        private String _ItemRequestNo;
        private Int32 _FromLocationID;
        private String _FromLocationCode;
        private String _FromLocationName;
        private Int32 _FromLocationItemGroupID;
        private Int32 _ToLocationID;
        private String _ToLocationCode;
        private String _ToLocationName;
        private Int32 _ToLocationItemGroupID;
        private String _Remarks;
        private String _GCTransactionStatus;
        private String _TransactionStatusWatermark;
        private Int32 _NumberOfItems;
        private Int32 _NumberOfApprovedItems;
        private String _CreatedByName;

        [Column(Name = "ItemRequestID", DataType = "Int32")]
        public Int32 ItemRequestID
        {
            get { return _ItemRequestID; }
            set { _ItemRequestID = value; }
        }
        [Column(Name = "TransactionDate", DataType = "DateTime")]
        public DateTime TransactionDate
        {
            get { return _TransactionDate; }
            set { _TransactionDate = value; }
        }
        [Column(Name = "TransactionTime", DataType = "String")]
        public String TransactionTime
        {
            get { return _TransactionTime; }
            set { _TransactionTime = value; }
        }
        [Column(Name = "ItemRequestNo", DataType = "String")]
        public String ItemRequestNo
        {
            get { return _ItemRequestNo; }
            set { _ItemRequestNo = value; }
        }
        [Column(Name = "FromLocationID", DataType = "Int32")]
        public Int32 FromLocationID
        {
            get { return _FromLocationID; }
            set { _FromLocationID = value; }
        }
        [Column(Name = "FromLocationCode", DataType = "String")]
        public String FromLocationCode
        {
            get { return _FromLocationCode; }
            set { _FromLocationCode = value; }
        }
        [Column(Name = "FromLocationName", DataType = "String")]
        public String FromLocationName
        {
            get { return _FromLocationName; }
            set { _FromLocationName = value; }
        }
        [Column(Name = "FromLocationItemGroupID", DataType = "Int32")]
        public Int32 FromLocationItemGroupID
        {
            get { return _FromLocationItemGroupID; }
            set { _FromLocationItemGroupID = value; }
        }
        [Column(Name = "ToLocationID", DataType = "Int32")]
        public Int32 ToLocationID
        {
            get { return _ToLocationID; }
            set { _ToLocationID = value; }
        }
        [Column(Name = "ToLocationCode", DataType = "String")]
        public String ToLocationCode
        {
            get { return _ToLocationCode; }
            set { _ToLocationCode = value; }
        }
        [Column(Name = "ToLocationName", DataType = "String")]
        public String ToLocationName
        {
            get { return _ToLocationName; }
            set { _ToLocationName = value; }
        }
        [Column(Name = "ToLocationItemGroupID", DataType = "Int32")]
        public Int32 ToLocationItemGroupID
        {
            get { return _ToLocationItemGroupID; }
            set { _ToLocationItemGroupID = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
        [Column(Name = "NumberOfItems", DataType = "Int32")]
        public Int32 NumberOfItems
        {
            get { return _NumberOfItems; }
            set { _NumberOfItems = value; }
        }
        [Column(Name = "NumberOfApprovedItems", DataType = "Int32")]
        public Int32 NumberOfApprovedItems
        {
            get { return _NumberOfApprovedItems; }
            set { _NumberOfApprovedItems = value; }
        }
        [Column(Name = "CreatedByName", DataType = "String")]
        public String CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }
    }
    #endregion
    #region vItemTransactionDt
    [Serializable]
    [Table(Name = "vItemTransactionDt")]
    public partial class vItemTransactionDt
    {
        private Int32 _ID;
        private Int32 _TransactionID;
        private String _TransactionCode;
        private String _TransactionNo;
        private DateTime _TransactionDate;
        private Int32 _FromLocationID;
        private String _FromLocationCode;
        private String _FromLocationName;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private Decimal _Quantity;
        private String _GCItemUnit;
        private String _ItemUnit;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private Decimal _ConversionFactor;
        private Decimal _BaseQuantity;
        private Decimal _CostAmount;
        private Boolean _IsControlExpired;
        private String _GCAdjustmentReason;
        private String _AdjustmentReason;
        private String _Remarks;
        private String _GCItemDetailStatus;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "TransactionID", DataType = "Int32")]
        public Int32 TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }
        [Column(Name = "TransactionCode", DataType = "String")]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
        [Column(Name = "TransactionNo", DataType = "String")]
        public String TransactionNo
        {
            get { return _TransactionNo; }
            set { _TransactionNo = value; }
        }
        [Column(Name = "TransactionDate", DataType = "DateTime")]
        public DateTime TransactionDate
        {
            get { return _TransactionDate; }
            set { _TransactionDate = value; }
        }
        [Column(Name = "FromLocationID", DataType = "Int32")]
        public Int32 FromLocationID
        {
            get { return _FromLocationID; }
            set { _FromLocationID = value; }
        }
        [Column(Name = "FromLocationCode", DataType = "String")]
        public String FromLocationCode
        {
            get { return _FromLocationCode; }
            set { _FromLocationCode = value; }
        }
        [Column(Name = "FromLocationName", DataType = "String")]
        public String FromLocationName
        {
            get { return _FromLocationName; }
            set { _FromLocationName = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "BaseQuantity", DataType = "Decimal")]
        public Decimal BaseQuantity
        {
            get { return _BaseQuantity; }
            set { _BaseQuantity = value; }
        }
        [Column(Name = "CostAmount", DataType = "Decimal")]
        public Decimal CostAmount
        {
            get { return _CostAmount; }
            set { _CostAmount = value; }
        }
        [Column(Name = "IsControlExpired", DataType = "Boolean")]
        public Boolean IsControlExpired
        {
            get { return _IsControlExpired; }
            set { _IsControlExpired = value; }
        }
        [Column(Name = "GCAdjustmentReason", DataType = "String")]
        public String GCAdjustmentReason
        {
            get { return _GCAdjustmentReason; }
            set { _GCAdjustmentReason = value; }
        }
        [Column(Name = "AdjustmentReason", DataType = "String")]
        public String AdjustmentReason
        {
            get { return _AdjustmentReason; }
            set { _AdjustmentReason = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
    }
    #endregion
    #region vItemTransactionHd
    [Serializable]
    [Table(Name = "vItemTransactionHd")]
    public partial class vItemTransactionHd
    {
        private Int32 _TransactionID;
        private String _TransactionCode;
        private DateTime _TransactionDate;
        private String _TransactionNo;
        private Int32 _FromLocationID;
        private String _FromLocationCode;
        private String _FromLocationName;
        private Int32 _ToLocationID;
        private String _ToLocationCode;
        private String _ToLocationName;
        private String _GCAdjustmentType;
        private String _AdjustmentType;
        private String _GCConsumptionType;
        private String _ConsumptionType;
        private Boolean _IsBySystem;
        private String _Remarks;
        private String _GCTransactionStatus;
        private String _TransactionStatusWatermark;
        private Int32 _NumberOfItems;
        private Int32 _NumberOfApprovedItems;

        [Column(Name = "TransactionID", DataType = "Int32")]
        public Int32 TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }
        [Column(Name = "TransactionCode", DataType = "String")]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
        [Column(Name = "TransactionDate", DataType = "DateTime")]
        public DateTime TransactionDate
        {
            get { return _TransactionDate; }
            set { _TransactionDate = value; }
        }
        [Column(Name = "TransactionNo", DataType = "String")]
        public String TransactionNo
        {
            get { return _TransactionNo; }
            set { _TransactionNo = value; }
        }
        [Column(Name = "FromLocationID", DataType = "Int32")]
        public Int32 FromLocationID
        {
            get { return _FromLocationID; }
            set { _FromLocationID = value; }
        }
        [Column(Name = "FromLocationCode", DataType = "String")]
        public String FromLocationCode
        {
            get { return _FromLocationCode; }
            set { _FromLocationCode = value; }
        }
        [Column(Name = "FromLocationName", DataType = "String")]
        public String FromLocationName
        {
            get { return _FromLocationName; }
            set { _FromLocationName = value; }
        }
        [Column(Name = "ToLocationID", DataType = "Int32")]
        public Int32 ToLocationID
        {
            get { return _ToLocationID; }
            set { _ToLocationID = value; }
        }
        [Column(Name = "ToLocationCode", DataType = "String")]
        public String ToLocationCode
        {
            get { return _ToLocationCode; }
            set { _ToLocationCode = value; }
        }
        [Column(Name = "ToLocationName", DataType = "String")]
        public String ToLocationName
        {
            get { return _ToLocationName; }
            set { _ToLocationName = value; }
        }
        [Column(Name = "GCAdjustmentType", DataType = "String")]
        public String GCAdjustmentType
        {
            get { return _GCAdjustmentType; }
            set { _GCAdjustmentType = value; }
        }
        [Column(Name = "AdjustmentType", DataType = "String")]
        public String AdjustmentType
        {
            get { return _AdjustmentType; }
            set { _AdjustmentType = value; }
        }
        [Column(Name = "GCConsumptionType", DataType = "String")]
        public String GCConsumptionType
        {
            get { return _GCConsumptionType; }
            set { _GCConsumptionType = value; }
        }
        [Column(Name = "ConsumptionType", DataType = "String")]
        public String ConsumptionType
        {
            get { return _ConsumptionType; }
            set { _ConsumptionType = value; }
        }
        [Column(Name = "IsBySystem", DataType = "Boolean")]
        public Boolean IsBySystem
        {
            get { return _IsBySystem; }
            set { _IsBySystem = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
        [Column(Name = "NumberOfItems", DataType = "Int32")]
        public Int32 NumberOfItems
        {
            get { return _NumberOfItems; }
            set { _NumberOfItems = value; }
        }
        [Column(Name = "NumberOfApprovedItems", DataType = "Int32")]
        public Int32 NumberOfApprovedItems
        {
            get { return _NumberOfApprovedItems; }
            set { _NumberOfApprovedItems = value; }
        }
    }
    #endregion
    #region vPeriodAdmission
    [Serializable]
    [Table(Name = "vPeriodAdmission")]
    public partial class vPeriodAdmission
    {
        private Int32 _PeriodAdmissionID;
        private String _PeriodAdmissionCode;
        private String _PeriodAdmissionName;
        private Int32 _SchoolPeriodID;
        private DateTime _RegistrationStartDate;
        private DateTime _RegistrationEndDate;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private String _GCPeriodAdmissionStatus;
        private String _PeriodAdmissionStatus;
        private String _Remarks;

        [Column(Name = "PeriodAdmissionID", DataType = "Int32")]
        public Int32 PeriodAdmissionID
        {
            get { return _PeriodAdmissionID; }
            set { _PeriodAdmissionID = value; }
        }
        [Column(Name = "PeriodAdmissionCode", DataType = "String")]
        public String PeriodAdmissionCode
        {
            get { return _PeriodAdmissionCode; }
            set { _PeriodAdmissionCode = value; }
        }
        [Column(Name = "PeriodAdmissionName", DataType = "String")]
        public String PeriodAdmissionName
        {
            get { return _PeriodAdmissionName; }
            set { _PeriodAdmissionName = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "RegistrationStartDate", DataType = "DateTime")]
        public DateTime RegistrationStartDate
        {
            get { return _RegistrationStartDate; }
            set { _RegistrationStartDate = value; }
        }
        [Column(Name = "RegistrationEndDate", DataType = "DateTime")]
        public DateTime RegistrationEndDate
        {
            get { return _RegistrationEndDate; }
            set { _RegistrationEndDate = value; }
        }
        [Column(Name = "StartDate", DataType = "DateTime")]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Column(Name = "EndDate", DataType = "DateTime")]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        [Column(Name = "GCPeriodAdmissionStatus", DataType = "String")]
        public String GCPeriodAdmissionStatus
        {
            get { return _GCPeriodAdmissionStatus; }
            set { _GCPeriodAdmissionStatus = value; }
        }
        [Column(Name = "PeriodAdmissionStatus", DataType = "String")]
        public String PeriodAdmissionStatus
        {
            get { return _PeriodAdmissionStatus; }
            set { _PeriodAdmissionStatus = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
    }
    #endregion
    #region vPeriodClassType
    [Serializable]
    [Table(Name = "vPeriodClassType")]
    public partial class vPeriodClassType
    {
        private Int32 _PeriodClassTypeID;
        private Int32 _SchoolPeriodID;
        private String _SchoolPeriodName;
        private Int32 _PeriodSectionID;
        private String _PeriodSectionName;
        private Int32 _ClassTypeID;
        private String _ClassTypeName;
        private String _GCGrade;
        private String _Grade;
        private String _GCMajor;
        private String _Major;
        private Int32 _DailySchedulePackageID;
        private String _DailySchedulePackageName;
        private Int16 _NoOfClass;
        private Int32 _CreatedClass;
        private Boolean _IsDeleted;

        [Column(Name = "PeriodClassTypeID", DataType = "Int32")]
        public Int32 PeriodClassTypeID
        {
            get { return _PeriodClassTypeID; }
            set { _PeriodClassTypeID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "SchoolPeriodName", DataType = "String")]
        public String SchoolPeriodName
        {
            get { return _SchoolPeriodName; }
            set { _SchoolPeriodName = value; }
        }
        [Column(Name = "PeriodSectionID", DataType = "Int32")]
        public Int32 PeriodSectionID
        {
            get { return _PeriodSectionID; }
            set { _PeriodSectionID = value; }
        }
        [Column(Name = "PeriodSectionName", DataType = "String")]
        public String PeriodSectionName
        {
            get { return _PeriodSectionName; }
            set { _PeriodSectionName = value; }
        }
        [Column(Name = "ClassTypeID", DataType = "Int32")]
        public Int32 ClassTypeID
        {
            get { return _ClassTypeID; }
            set { _ClassTypeID = value; }
        }
        [Column(Name = "ClassTypeName", DataType = "String")]
        public String ClassTypeName
        {
            get { return _ClassTypeName; }
            set { _ClassTypeName = value; }
        }
        [Column(Name = "GCGrade", DataType = "String")]
        public String GCGrade
        {
            get { return _GCGrade; }
            set { _GCGrade = value; }
        }
        [Column(Name = "Grade", DataType = "String")]
        public String Grade
        {
            get { return _Grade; }
            set { _Grade = value; }
        }
        [Column(Name = "GCMajor", DataType = "String")]
        public String GCMajor
        {
            get { return _GCMajor; }
            set { _GCMajor = value; }
        }
        [Column(Name = "Major", DataType = "String")]
        public String Major
        {
            get { return _Major; }
            set { _Major = value; }
        }
        [Column(Name = "DailySchedulePackageID", DataType = "Int32")]
        public Int32 DailySchedulePackageID
        {
            get { return _DailySchedulePackageID; }
            set { _DailySchedulePackageID = value; }
        }
        [Column(Name = "DailySchedulePackageName", DataType = "String")]
        public String DailySchedulePackageName
        {
            get { return _DailySchedulePackageName; }
            set { _DailySchedulePackageName = value; }
        }
        [Column(Name = "NoOfClass", DataType = "Int16")]
        public Int16 NoOfClass
        {
            get { return _NoOfClass; }
            set { _NoOfClass = value; }
        }
        [Column(Name = "CreatedClass", DataType = "Int32")]
        public Int32 CreatedClass
        {
            get { return _CreatedClass; }
            set { _CreatedClass = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vPeriodClassTypeSubject
    [Serializable]
    [Table(Name = "vPeriodClassTypeSubject")]
    public class vPeriodClassTypeSubject
    {
        private Int32 _PeriodClassTypeSubjectID;
        private Int32 _PeriodClassTypeID;
        private Int32 _SchoolPeriodID;
        private String _SchoolPeriodName;
        private Int32 _PeriodSectionID;
        private String _PeriodSectionName;
        private Int32 _ClassTypeID;
        private String _ClassTypeName;
        private Int32 _SubjectID;
        private String _SubjectCode;
        private String _SubjectName;
        private Int32 _TeacherID;
        private String _TeacherCode;
        private String _TeacherName;
        private Int16 _NoMeetingHoursInWeek;
        private Boolean _IsDeleted;

        [Column(Name = "PeriodClassTypeSubjectID", DataType = "Int32")]
        public Int32 PeriodClassTypeSubjectID
        {
            get { return _PeriodClassTypeSubjectID; }
            set { _PeriodClassTypeSubjectID = value; }
        }
        [Column(Name = "PeriodClassTypeID", DataType = "Int32")]
        public Int32 PeriodClassTypeID
        {
            get { return _PeriodClassTypeID; }
            set { _PeriodClassTypeID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "SchoolPeriodName", DataType = "String")]
        public String SchoolPeriodName
        {
            get { return _SchoolPeriodName; }
            set { _SchoolPeriodName = value; }
        }
        [Column(Name = "PeriodSectionID", DataType = "Int32")]
        public Int32 PeriodSectionID
        {
            get { return _PeriodSectionID; }
            set { _PeriodSectionID = value; }
        }
        [Column(Name = "PeriodSectionName", DataType = "String")]
        public String PeriodSectionName
        {
            get { return _PeriodSectionName; }
            set { _PeriodSectionName = value; }
        }
        [Column(Name = "ClassTypeID", DataType = "Int32")]
        public Int32 ClassTypeID
        {
            get { return _ClassTypeID; }
            set { _ClassTypeID = value; }
        }
        [Column(Name = "ClassTypeName", DataType = "String")]
        public String ClassTypeName
        {
            get { return _ClassTypeName; }
            set { _ClassTypeName = value; }
        }
        [Column(Name = "SubjectID", DataType = "Int32")]
        public Int32 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
        [Column(Name = "SubjectCode", DataType = "String")]
        public String SubjectCode
        {
            get { return _SubjectCode; }
            set { _SubjectCode = value; }
        }
        [Column(Name = "SubjectName", DataType = "String")]
        public String SubjectName
        {
            get { return _SubjectName; }
            set { _SubjectName = value; }
        }
        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
        }
        [Column(Name = "TeacherCode", DataType = "String")]
        public String TeacherCode
        {
            get { return _TeacherCode; }
            set { _TeacherCode = value; }
        }
        [Column(Name = "TeacherName", DataType = "String")]
        public String TeacherName
        {
            get { return _TeacherName; }
            set { _TeacherName = value; }
        }
        [Column(Name = "NoMeetingHoursInWeek", DataType = "Int16")]
        public Int16 NoMeetingHoursInWeek
        {
            get { return _NoMeetingHoursInWeek; }
            set { _NoMeetingHoursInWeek = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vPeriodSchedule
    [Serializable]
    [Table(Name = "vPeriodSchedule")]
    public partial class vPeriodSchedule
    {
        private Int32 _PeriodScheduleID;
        private String _PeriodScheduleCode;
        private String _PeriodScheduleName;
        private Int32 _SchoolPeriodID;
        private String _GCPeriodScheduleType;
        private String _PeriodScheduleType;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "PeriodScheduleID", DataType = "Int32")]
        public Int32 PeriodScheduleID
        {
            get { return _PeriodScheduleID; }
            set { _PeriodScheduleID = value; }
        }
        [Column(Name = "PeriodScheduleCode", DataType = "String")]
        public String PeriodScheduleCode
        {
            get { return _PeriodScheduleCode; }
            set { _PeriodScheduleCode = value; }
        }
        [Column(Name = "PeriodScheduleName", DataType = "String")]
        public String PeriodScheduleName
        {
            get { return _PeriodScheduleName; }
            set { _PeriodScheduleName = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "GCPeriodScheduleType", DataType = "String")]
        public String GCPeriodScheduleType
        {
            get { return _GCPeriodScheduleType; }
            set { _GCPeriodScheduleType = value; }
        }
        [Column(Name = "PeriodScheduleType", DataType = "String")]
        public String PeriodScheduleType
        {
            get { return _PeriodScheduleType; }
            set { _PeriodScheduleType = value; }
        }
        [Column(Name = "StartDate", DataType = "DateTime")]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Column(Name = "EndDate", DataType = "DateTime")]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vPeriodSection
    [Serializable]
    [Table(Name = "vPeriodSection")]
    public partial class vPeriodSection
    {
        private Int32 _PeriodSectionID;
        private String _PeriodSectionCode;
        private String _PeriodSectionName;
        private Int32 _SchoolPeriodID;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private String _GCPeriodSectionStatus;
        private String _PeriodSectionStatus;
        private String _Remarks;

        [Column(Name = "PeriodSectionID", DataType = "Int32")]
        public Int32 PeriodSectionID
        {
            get { return _PeriodSectionID; }
            set { _PeriodSectionID = value; }
        }
        [Column(Name = "PeriodSectionCode", DataType = "String")]
        public String PeriodSectionCode
        {
            get { return _PeriodSectionCode; }
            set { _PeriodSectionCode = value; }
        }
        [Column(Name = "PeriodSectionName", DataType = "String")]
        public String PeriodSectionName
        {
            get { return _PeriodSectionName; }
            set { _PeriodSectionName = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "StartDate", DataType = "DateTime")]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Column(Name = "EndDate", DataType = "DateTime")]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        [Column(Name = "GCPeriodSectionStatus", DataType = "String")]
        public String GCPeriodSectionStatus
        {
            get { return _GCPeriodSectionStatus; }
            set { _GCPeriodSectionStatus = value; }
        }
        [Column(Name = "PeriodSectionStatus", DataType = "String")]
        public String PeriodSectionStatus
        {
            get { return _PeriodSectionStatus; }
            set { _PeriodSectionStatus = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
    }
    #endregion
    #region vPurchaseOrderDt
    [Serializable]
    [Table(Name = "vPurchaseOrderDt")]
    public partial class vPurchaseOrderDt
    {
        private Int32 _ID;
        private Int32 _PurchaseOrderID;
        private String _PurchaseOrderNo;
        private DateTime _OrderDate;
        private Int32 _SupplierID;
        private String _SupplierCode;
        private String _SupplierName;
        private String _GCTransactionStatus;
        private String _TransactionStatus;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Decimal _Quantity;
        private String _SupplierItemName;
        private String _SupplierItemCode;
        private String _GCPurchaseUnit;
        private String _PurchaseUnit;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private Decimal _ConversionFactor;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage1;
        private Decimal _DiscountPercentage2;
        private String _GCItemDetailStatus;
        private String _ReceivedInformation;
        private Decimal _ReceivedQuantity;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "PurchaseOrderID", DataType = "Int32")]
        public Int32 PurchaseOrderID
        {
            get { return _PurchaseOrderID; }
            set { _PurchaseOrderID = value; }
        }
        [Column(Name = "PurchaseOrderNo", DataType = "String")]
        public String PurchaseOrderNo
        {
            get { return _PurchaseOrderNo; }
            set { _PurchaseOrderNo = value; }
        }
        [Column(Name = "OrderDate", DataType = "DateTime")]
        public DateTime OrderDate
        {
            get { return _OrderDate; }
            set { _OrderDate = value; }
        }
        [Column(Name = "SupplierID", DataType = "Int32")]
        public Int32 SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
        }
        [Column(Name = "SupplierCode", DataType = "String")]
        public String SupplierCode
        {
            get { return _SupplierCode; }
            set { _SupplierCode = value; }
        }
        [Column(Name = "SupplierName", DataType = "String")]
        public String SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatus", DataType = "String")]
        public String TransactionStatus
        {
            get { return _TransactionStatus; }
            set { _TransactionStatus = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "SupplierItemName", DataType = "String")]
        public String SupplierItemName
        {
            get { return _SupplierItemName; }
            set { _SupplierItemName = value; }
        }
        [Column(Name = "SupplierItemCode", DataType = "String")]
        public String SupplierItemCode
        {
            get { return _SupplierItemCode; }
            set { _SupplierItemCode = value; }
        }
        [Column(Name = "GCPurchaseUnit", DataType = "String")]
        public String GCPurchaseUnit
        {
            get { return _GCPurchaseUnit; }
            set { _GCPurchaseUnit = value; }
        }
        [Column(Name = "PurchaseUnit", DataType = "String")]
        public String PurchaseUnit
        {
            get { return _PurchaseUnit; }
            set { _PurchaseUnit = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "UnitPrice", DataType = "Decimal")]
        public Decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        [Column(Name = "DiscountPercentage1", DataType = "Decimal")]
        public Decimal DiscountPercentage1
        {
            get { return _DiscountPercentage1; }
            set { _DiscountPercentage1 = value; }
        }
        [Column(Name = "DiscountPercentage2", DataType = "Decimal")]
        public Decimal DiscountPercentage2
        {
            get { return _DiscountPercentage2; }
            set { _DiscountPercentage2 = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
        [Column(Name = "ReceivedInformation", DataType = "String")]
        public String ReceivedInformation
        {
            get { return _ReceivedInformation; }
            set { _ReceivedInformation = value; }
        }
        [Column(Name = "ReceivedQuantity", DataType = "Decimal")]
        public Decimal ReceivedQuantity
        {
            get { return _ReceivedQuantity; }
            set { _ReceivedQuantity = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vPurchaseOrderDtOutStanding
    [Serializable]
    [Table(Name = "vPurchaseOrderDtOutStanding")]
    public partial class vPurchaseOrderDtOutStanding
    {
        private Int32 _ID;
        private Int32 _PurchaseReceiveID;
        private Int32 _PurchaseOrderID;
        private String _PurchaseOrderNo;
        private Int32 _ItemID;
        private String _ItemName1;
        private String _ItemCode;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Decimal _OrderQuantity;
        private String _OrderPurchaseUnit;
        private Decimal _Quantity;
        private String _SupplierItemName;
        private String _SupplierItemCode;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private String _GCItemUnit;
        private String _ItemUnit;
        private Decimal _ConversionFactor;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage1;
        private Decimal _DiscountPercentage2;
        private Boolean _IsBonusItem;
        private Boolean _IsControlExpired;
        private String _GCItemDetailStatus;
        private Int32 _CreatedBy;
        private String _UserName;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "PurchaseReceiveID", DataType = "Int32")]
        public Int32 PurchaseReceiveID
        {
            get { return _PurchaseReceiveID; }
            set { _PurchaseReceiveID = value; }
        }
        [Column(Name = "PurchaseOrderID", DataType = "Int32")]
        public Int32 PurchaseOrderID
        {
            get { return _PurchaseOrderID; }
            set { _PurchaseOrderID = value; }
        }
        [Column(Name = "PurchaseOrderNo", DataType = "String")]
        public String PurchaseOrderNo
        {
            get { return _PurchaseOrderNo; }
            set { _PurchaseOrderNo = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "OrderQuantity", DataType = "Decimal")]
        public Decimal OrderQuantity
        {
            get { return _OrderQuantity; }
            set { _OrderQuantity = value; }
        }
        [Column(Name = "OrderPurchaseUnit", DataType = "String")]
        public String OrderPurchaseUnit
        {
            get { return _OrderPurchaseUnit; }
            set { _OrderPurchaseUnit = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "SupplierItemName", DataType = "String")]
        public String SupplierItemName
        {
            get { return _SupplierItemName; }
            set { _SupplierItemName = value; }
        }
        [Column(Name = "SupplierItemCode", DataType = "String")]
        public String SupplierItemCode
        {
            get { return _SupplierItemCode; }
            set { _SupplierItemCode = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "UnitPrice", DataType = "Decimal")]
        public Decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        [Column(Name = "DiscountPercentage1", DataType = "Decimal")]
        public Decimal DiscountPercentage1
        {
            get { return _DiscountPercentage1; }
            set { _DiscountPercentage1 = value; }
        }
        [Column(Name = "DiscountPercentage2", DataType = "Decimal")]
        public Decimal DiscountPercentage2
        {
            get { return _DiscountPercentage2; }
            set { _DiscountPercentage2 = value; }
        }
        [Column(Name = "IsBonusItem", DataType = "Boolean")]
        public Boolean IsBonusItem
        {
            get { return _IsBonusItem; }
            set { _IsBonusItem = value; }
        }
        [Column(Name = "IsControlExpired", DataType = "Boolean")]
        public Boolean IsControlExpired
        {
            get { return _IsControlExpired; }
            set { _IsControlExpired = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "UserName", DataType = "String")]
        public String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
    }
    #endregion
    #region vPurchaseOrderHd
    [Serializable]
    [Table(Name = "vPurchaseOrderHd")]
    public partial class vPurchaseOrderHd
    {
        private Int32 _PurchaseOrderID;
        private Int32 _LocationID;
        private String _LocationCode;
        private String _LocationName;
        private Int32 _LocationItemGroupID;
        private DateTime _OrderDate;
        private String _PurchaseOrderNo;
        private DateTime _DeliveryDate;
        private DateTime _POExpiredDate;
        private String _GCPurchaseOrderType;
        private String _PurchaseOrderType;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private Int32 _TermID;
        private String _TermName;
        private String _GCFrancoRegion;
        private String _FrancoRegion;
        private String _GCCurrencyCode;
        private String _CurrencyCode;
        private Decimal _CurrencyRate;
        private Boolean _IsIncludeVAT;
        private Decimal _TransactionAmount;
        private Decimal _TotalDtAmount;
        private Decimal _FinalDiscount;
        private Decimal _VATPercentage;
        private Decimal _DownPaymentAmount;
        private String _PaymentRemarks;
        private String _Remarks;
        private String _GCTransactionStatus;
        private String _TransactionStatus;
        private String _TransactionStatusWatermark;
        private Int32 _NumberOfItems;
        private Int32 _NumberOfApprovedItems;
        private String _CreatedByName;

        [Column(Name = "PurchaseOrderID", DataType = "Int32")]
        public Int32 PurchaseOrderID
        {
            get { return _PurchaseOrderID; }
            set { _PurchaseOrderID = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "LocationCode", DataType = "String")]
        public String LocationCode
        {
            get { return _LocationCode; }
            set { _LocationCode = value; }
        }
        [Column(Name = "LocationName", DataType = "String")]
        public String LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }
        [Column(Name = "LocationItemGroupID", DataType = "Int32")]
        public Int32 LocationItemGroupID
        {
            get { return _LocationItemGroupID; }
            set { _LocationItemGroupID = value; }
        }
        [Column(Name = "OrderDate", DataType = "DateTime")]
        public DateTime OrderDate
        {
            get { return _OrderDate; }
            set { _OrderDate = value; }
        }
        [Column(Name = "PurchaseOrderNo", DataType = "String")]
        public String PurchaseOrderNo
        {
            get { return _PurchaseOrderNo; }
            set { _PurchaseOrderNo = value; }
        }
        [Column(Name = "DeliveryDate", DataType = "DateTime")]
        public DateTime DeliveryDate
        {
            get { return _DeliveryDate; }
            set { _DeliveryDate = value; }
        }
        [Column(Name = "POExpiredDate", DataType = "DateTime")]
        public DateTime POExpiredDate
        {
            get { return _POExpiredDate; }
            set { _POExpiredDate = value; }
        }
        [Column(Name = "GCPurchaseOrderType", DataType = "String")]
        public String GCPurchaseOrderType
        {
            get { return _GCPurchaseOrderType; }
            set { _GCPurchaseOrderType = value; }
        }
        [Column(Name = "PurchaseOrderType", DataType = "String")]
        public String PurchaseOrderType
        {
            get { return _PurchaseOrderType; }
            set { _PurchaseOrderType = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "TermID", DataType = "Int32")]
        public Int32 TermID
        {
            get { return _TermID; }
            set { _TermID = value; }
        }
        [Column(Name = "TermName", DataType = "String")]
        public String TermName
        {
            get { return _TermName; }
            set { _TermName = value; }
        }
        [Column(Name = "GCFrancoRegion", DataType = "String")]
        public String GCFrancoRegion
        {
            get { return _GCFrancoRegion; }
            set { _GCFrancoRegion = value; }
        }
        [Column(Name = "FrancoRegion", DataType = "String")]
        public String FrancoRegion
        {
            get { return _FrancoRegion; }
            set { _FrancoRegion = value; }
        }
        [Column(Name = "GCCurrencyCode", DataType = "String")]
        public String GCCurrencyCode
        {
            get { return _GCCurrencyCode; }
            set { _GCCurrencyCode = value; }
        }
        [Column(Name = "CurrencyCode", DataType = "String")]
        public String CurrencyCode
        {
            get { return _CurrencyCode; }
            set { _CurrencyCode = value; }
        }
        [Column(Name = "CurrencyRate", DataType = "Decimal")]
        public Decimal CurrencyRate
        {
            get { return _CurrencyRate; }
            set { _CurrencyRate = value; }
        }
        [Column(Name = "IsIncludeVAT", DataType = "Boolean")]
        public Boolean IsIncludeVAT
        {
            get { return _IsIncludeVAT; }
            set { _IsIncludeVAT = value; }
        }
        [Column(Name = "TransactionAmount", DataType = "Decimal")]
        public Decimal TransactionAmount
        {
            get { return _TransactionAmount; }
            set { _TransactionAmount = value; }
        }
        [Column(Name = "TotalDtAmount", DataType = "Decimal")]
        public Decimal TotalDtAmount
        {
            get { return _TotalDtAmount; }
            set { _TotalDtAmount = value; }
        }
        [Column(Name = "FinalDiscount", DataType = "Decimal")]
        public Decimal FinalDiscount
        {
            get { return _FinalDiscount; }
            set { _FinalDiscount = value; }
        }
        [Column(Name = "VATPercentage", DataType = "Decimal")]
        public Decimal VATPercentage
        {
            get { return _VATPercentage; }
            set { _VATPercentage = value; }
        }
        [Column(Name = "DownPaymentAmount", DataType = "Decimal")]
        public Decimal DownPaymentAmount
        {
            get { return _DownPaymentAmount; }
            set { _DownPaymentAmount = value; }
        }
        [Column(Name = "PaymentRemarks", DataType = "String")]
        public String PaymentRemarks
        {
            get { return _PaymentRemarks; }
            set { _PaymentRemarks = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatus", DataType = "String")]
        public String TransactionStatus
        {
            get { return _TransactionStatus; }
            set { _TransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
        [Column(Name = "NumberOfItems", DataType = "Int32")]
        public Int32 NumberOfItems
        {
            get { return _NumberOfItems; }
            set { _NumberOfItems = value; }
        }
        [Column(Name = "NumberOfApprovedItems", DataType = "Int32")]
        public Int32 NumberOfApprovedItems
        {
            get { return _NumberOfApprovedItems; }
            set { _NumberOfApprovedItems = value; }
        }
        [Column(Name = "CreatedByName", DataType = "String")]
        public String CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }
    }
    #endregion
    #region vPurchaseReceiveDt
    [Serializable]
    [Table(Name = "vPurchaseReceiveDt")]
    public partial class vPurchaseReceiveDt
    {
        private Int32 _ID;
        private Int32 _PurchaseReceiveID;
        private String _PurchaseReceiveNo;
        private DateTime _ReceivedDate;
        private Int32 _PurchaseOrderID;
        private String _PurchaseOrderNo;
        private Int32 _ItemID;
        private String _ItemName1;
        private String _ItemCode;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Int32 _SupplierID;
        private Int32 _LocationID;
        private String _LocationName;
        private Decimal _OrderQuantity;
        private String _OrderPurchaseUnit;
        private Decimal _Quantity;
        private String _SupplierName;
        private String _SupplierItemName;
        private String _SupplierItemCode;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private String _GCItemUnit;
        private String _ItemUnit;
        private Decimal _ConversionFactor;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage1;
        private Decimal _DiscountPercentage2;
        private Boolean _IsBonusItem;
        private Boolean _IsControlExpired;
        private String _GCItemDetailStatus;
        private String _ItemDetailStatus;
        private Int32 _CreatedBy;
        private String _UserName;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "PurchaseReceiveID", DataType = "Int32")]
        public Int32 PurchaseReceiveID
        {
            get { return _PurchaseReceiveID; }
            set { _PurchaseReceiveID = value; }
        }
        [Column(Name = "PurchaseReceiveNo", DataType = "String")]
        public String PurchaseReceiveNo
        {
            get { return _PurchaseReceiveNo; }
            set { _PurchaseReceiveNo = value; }
        }
        [Column(Name = "ReceivedDate", DataType = "DateTime")]
        public DateTime ReceivedDate
        {
            get { return _ReceivedDate; }
            set { _ReceivedDate = value; }
        }
        [Column(Name = "PurchaseOrderID", DataType = "Int32")]
        public Int32 PurchaseOrderID
        {
            get { return _PurchaseOrderID; }
            set { _PurchaseOrderID = value; }
        }
        [Column(Name = "PurchaseOrderNo", DataType = "String")]
        public String PurchaseOrderNo
        {
            get { return _PurchaseOrderNo; }
            set { _PurchaseOrderNo = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "SupplierID", DataType = "Int32")]
        public Int32 SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "LocationName", DataType = "String")]
        public String LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }
        [Column(Name = "OrderQuantity", DataType = "Decimal")]
        public Decimal OrderQuantity
        {
            get { return _OrderQuantity; }
            set { _OrderQuantity = value; }
        }
        [Column(Name = "OrderPurchaseUnit", DataType = "String")]
        public String OrderPurchaseUnit
        {
            get { return _OrderPurchaseUnit; }
            set { _OrderPurchaseUnit = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "SupplierName", DataType = "String")]
        public String SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }
        [Column(Name = "SupplierItemName", DataType = "String")]
        public String SupplierItemName
        {
            get { return _SupplierItemName; }
            set { _SupplierItemName = value; }
        }
        [Column(Name = "SupplierItemCode", DataType = "String")]
        public String SupplierItemCode
        {
            get { return _SupplierItemCode; }
            set { _SupplierItemCode = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "UnitPrice", DataType = "Decimal")]
        public Decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        [Column(Name = "DiscountPercentage1", DataType = "Decimal")]
        public Decimal DiscountPercentage1
        {
            get { return _DiscountPercentage1; }
            set { _DiscountPercentage1 = value; }
        }
        [Column(Name = "DiscountPercentage2", DataType = "Decimal")]
        public Decimal DiscountPercentage2
        {
            get { return _DiscountPercentage2; }
            set { _DiscountPercentage2 = value; }
        }
        [Column(Name = "IsBonusItem", DataType = "Boolean")]
        public Boolean IsBonusItem
        {
            get { return _IsBonusItem; }
            set { _IsBonusItem = value; }
        }
        [Column(Name = "IsControlExpired", DataType = "Boolean")]
        public Boolean IsControlExpired
        {
            get { return _IsControlExpired; }
            set { _IsControlExpired = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
        [Column(Name = "ItemDetailStatus", DataType = "String")]
        public String ItemDetailStatus
        {
            get { return _ItemDetailStatus; }
            set { _ItemDetailStatus = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "UserName", DataType = "String")]
        public String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
    }
    #endregion
    #region vPurchaseReceiveHd
    [Serializable]
    [Table(Name = "vPurchaseReceiveHd")]
    public partial class vPurchaseReceiveHd
    {
        private Int32 _PurchaseReceiveID;
        private String _PurchaseReceiveNo;
        private DateTime _ReceivedDate;
        private String _ReceivedTime;
        private Int32 _LocationID;
        private String _LocationCode;
        private String _LocationName;
        private Int32 _SupplierID;
        private String _SupplierCode;
        private String _SupplierName;
        private Int32 _TermID;
        private String _TermName;
        private String _ReferenceNo;
        private DateTime _ReferenceDate;
        private String _GCCurrencyCode;
        private String _CurrencyCode;
        private Decimal _CurrencyRate;
        private Boolean _IsIncludeVAT;
        private Decimal _TransactionAmount;
        private Decimal _DiscountAmount;
        private Decimal _FinalDiscount;
        private String _GCChargesType;
        private String _ChargesType;
        private Decimal _ChargesAmount;
        private Decimal _StampAmount;
        private Decimal _VATPercentage;
        private Decimal _DownPaymentAmount;
        private String _DownPaymentReferenceNo;
        private String _ReceivedBy;
        private String _Remarks;
        private DateTime _PaymentDueDate;
        private String _GCTransactionStatus;
        private Boolean _IsHasPurchaseReturn;
        private Int32 _PurchaseReturnID;
        private String _TransactionStatusWatermark;
        private String _CreatedByName;

        [Column(Name = "PurchaseReceiveID", DataType = "Int32")]
        public Int32 PurchaseReceiveID
        {
            get { return _PurchaseReceiveID; }
            set { _PurchaseReceiveID = value; }
        }
        [Column(Name = "PurchaseReceiveNo", DataType = "String")]
        public String PurchaseReceiveNo
        {
            get { return _PurchaseReceiveNo; }
            set { _PurchaseReceiveNo = value; }
        }
        [Column(Name = "ReceivedDate", DataType = "DateTime")]
        public DateTime ReceivedDate
        {
            get { return _ReceivedDate; }
            set { _ReceivedDate = value; }
        }
        [Column(Name = "ReceivedTime", DataType = "String")]
        public String ReceivedTime
        {
            get { return _ReceivedTime; }
            set { _ReceivedTime = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "LocationCode", DataType = "String")]
        public String LocationCode
        {
            get { return _LocationCode; }
            set { _LocationCode = value; }
        }
        [Column(Name = "LocationName", DataType = "String")]
        public String LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }
        [Column(Name = "SupplierID", DataType = "Int32")]
        public Int32 SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
        }
        [Column(Name = "SupplierCode", DataType = "String")]
        public String SupplierCode
        {
            get { return _SupplierCode; }
            set { _SupplierCode = value; }
        }
        [Column(Name = "SupplierName", DataType = "String")]
        public String SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }
        [Column(Name = "TermID", DataType = "Int32")]
        public Int32 TermID
        {
            get { return _TermID; }
            set { _TermID = value; }
        }
        [Column(Name = "TermName", DataType = "String")]
        public String TermName
        {
            get { return _TermName; }
            set { _TermName = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String")]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "ReferenceDate", DataType = "DateTime")]
        public DateTime ReferenceDate
        {
            get { return _ReferenceDate; }
            set { _ReferenceDate = value; }
        }
        [Column(Name = "GCCurrencyCode", DataType = "String")]
        public String GCCurrencyCode
        {
            get { return _GCCurrencyCode; }
            set { _GCCurrencyCode = value; }
        }
        [Column(Name = "CurrencyCode", DataType = "String")]
        public String CurrencyCode
        {
            get { return _CurrencyCode; }
            set { _CurrencyCode = value; }
        }
        [Column(Name = "CurrencyRate", DataType = "Decimal")]
        public Decimal CurrencyRate
        {
            get { return _CurrencyRate; }
            set { _CurrencyRate = value; }
        }
        [Column(Name = "IsIncludeVAT", DataType = "Boolean")]
        public Boolean IsIncludeVAT
        {
            get { return _IsIncludeVAT; }
            set { _IsIncludeVAT = value; }
        }
        [Column(Name = "TransactionAmount", DataType = "Decimal")]
        public Decimal TransactionAmount
        {
            get { return _TransactionAmount; }
            set { _TransactionAmount = value; }
        }
        [Column(Name = "DiscountAmount", DataType = "Decimal")]
        public Decimal DiscountAmount
        {
            get { return _DiscountAmount; }
            set { _DiscountAmount = value; }
        }
        [Column(Name = "FinalDiscount", DataType = "Decimal")]
        public Decimal FinalDiscount
        {
            get { return _FinalDiscount; }
            set { _FinalDiscount = value; }
        }
        [Column(Name = "GCChargesType", DataType = "String")]
        public String GCChargesType
        {
            get { return _GCChargesType; }
            set { _GCChargesType = value; }
        }
        [Column(Name = "ChargesType", DataType = "String")]
        public String ChargesType
        {
            get { return _ChargesType; }
            set { _ChargesType = value; }
        }
        [Column(Name = "ChargesAmount", DataType = "Decimal")]
        public Decimal ChargesAmount
        {
            get { return _ChargesAmount; }
            set { _ChargesAmount = value; }
        }
        [Column(Name = "StampAmount", DataType = "Decimal")]
        public Decimal StampAmount
        {
            get { return _StampAmount; }
            set { _StampAmount = value; }
        }
        [Column(Name = "VATPercentage", DataType = "Decimal")]
        public Decimal VATPercentage
        {
            get { return _VATPercentage; }
            set { _VATPercentage = value; }
        }
        [Column(Name = "DownPaymentAmount", DataType = "Decimal")]
        public Decimal DownPaymentAmount
        {
            get { return _DownPaymentAmount; }
            set { _DownPaymentAmount = value; }
        }
        [Column(Name = "DownPaymentReferenceNo", DataType = "String")]
        public String DownPaymentReferenceNo
        {
            get { return _DownPaymentReferenceNo; }
            set { _DownPaymentReferenceNo = value; }
        }
        [Column(Name = "ReceivedBy", DataType = "String")]
        public String ReceivedBy
        {
            get { return _ReceivedBy; }
            set { _ReceivedBy = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "PaymentDueDate", DataType = "DateTime")]
        public DateTime PaymentDueDate
        {
            get { return _PaymentDueDate; }
            set { _PaymentDueDate = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "IsHasPurchaseReturn", DataType = "Boolean")]
        public Boolean IsHasPurchaseReturn
        {
            get { return _IsHasPurchaseReturn; }
            set { _IsHasPurchaseReturn = value; }
        }
        [Column(Name = "PurchaseReturnID", DataType = "Int32")]
        public Int32 PurchaseReturnID
        {
            get { return _PurchaseReturnID; }
            set { _PurchaseReturnID = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
        [Column(Name = "CreatedByName", DataType = "String")]
        public String CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }
    }
    #endregion
    #region vPurchaseRequestDt
    [Serializable]
    [Table(Name = "vPurchaseRequestDt")]
    public partial class vPurchaseRequestDt
    {
        private Int32 _ID;
        private Int32 _PurchaseRequestID;
        private String _PurchaseRequestNo;
        private DateTime _TransactionDate;
        private Int32 _FromLocationID;
        private String _FromLocationCode;
        private String _FromLocationName;
        private Int32 _ItemId;
        private String _ItemCode;
        private String _ItemName1;
        private String _ItemName2;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private Decimal _Quantity;
        private Decimal _POQuantity;
        private String _GCPurchaseUnit;
        private String _PurchaseUnit;
        private Decimal _ConversionFactor;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private String _SupplierItemCode;
        private String _SupplierItemName;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage;
        private String _GCItemDetailStatus;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32 _ItemGroupId;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Decimal _EndingBalance;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "PurchaseRequestID", DataType = "Int32")]
        public Int32 PurchaseRequestID
        {
            get { return _PurchaseRequestID; }
            set { _PurchaseRequestID = value; }
        }
        [Column(Name = "PurchaseRequestNo", DataType = "String")]
        public String PurchaseRequestNo
        {
            get { return _PurchaseRequestNo; }
            set { _PurchaseRequestNo = value; }
        }
        [Column(Name = "TransactionDate", DataType = "DateTime")]
        public DateTime TransactionDate
        {
            get { return _TransactionDate; }
            set { _TransactionDate = value; }
        }
        [Column(Name = "FromLocationID", DataType = "Int32")]
        public Int32 FromLocationID
        {
            get { return _FromLocationID; }
            set { _FromLocationID = value; }
        }
        [Column(Name = "FromLocationCode", DataType = "String")]
        public String FromLocationCode
        {
            get { return _FromLocationCode; }
            set { _FromLocationCode = value; }
        }
        [Column(Name = "FromLocationName", DataType = "String")]
        public String FromLocationName
        {
            get { return _FromLocationName; }
            set { _FromLocationName = value; }
        }
        [Column(Name = "ItemId", DataType = "Int32")]
        public Int32 ItemId
        {
            get { return _ItemId; }
            set { _ItemId = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemName2", DataType = "String")]
        public String ItemName2
        {
            get { return _ItemName2; }
            set { _ItemName2 = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "POQuantity", DataType = "Decimal")]
        public Decimal POQuantity
        {
            get { return _POQuantity; }
            set { _POQuantity = value; }
        }
        [Column(Name = "GCPurchaseUnit", DataType = "String")]
        public String GCPurchaseUnit
        {
            get { return _GCPurchaseUnit; }
            set { _GCPurchaseUnit = value; }
        }
        [Column(Name = "PurchaseUnit", DataType = "String")]
        public String PurchaseUnit
        {
            get { return _PurchaseUnit; }
            set { _PurchaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "SupplierItemCode", DataType = "String")]
        public String SupplierItemCode
        {
            get { return _SupplierItemCode; }
            set { _SupplierItemCode = value; }
        }
        [Column(Name = "SupplierItemName", DataType = "String")]
        public String SupplierItemName
        {
            get { return _SupplierItemName; }
            set { _SupplierItemName = value; }
        }
        [Column(Name = "UnitPrice", DataType = "Decimal")]
        public Decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        [Column(Name = "DiscountPercentage", DataType = "Decimal")]
        public Decimal DiscountPercentage
        {
            get { return _DiscountPercentage; }
            set { _DiscountPercentage = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "ItemGroupId", DataType = "Int32")]
        public Int32 ItemGroupId
        {
            get { return _ItemGroupId; }
            set { _ItemGroupId = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "EndingBalance", DataType = "Decimal")]
        public Decimal EndingBalance
        {
            get { return _EndingBalance; }
            set { _EndingBalance = value; }
        }
    }
    #endregion
    #region vPurchaseRequestDtOutstanding
    [Serializable]
    [Table(Name = "vPurchaseRequestDtOutstanding")]
    public partial class vPurchaseRequestDtOutstanding
    {
        private Int32 _ID;
        private String _PurchaseRequestNo;
        private Int32 _FromLocationID;
        private String _GCTransactionStatus;
        private Int32 _PurchaseRequestID;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private String _GCBaseUnit;
        private String _BaseUnit;
        private Decimal _Quantity;
        private String _GCPurchaseUnit;
        private String _PurchaseUnit;
        private Decimal _ConversionFactor;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private String _SupplierItemCode;
        private String _SupplierItemName;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage;
        private String _GCItemDetailStatus;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Decimal _QtyOnOrder;
        private Decimal _QuantityEND;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "PurchaseRequestNo", DataType = "String")]
        public String PurchaseRequestNo
        {
            get { return _PurchaseRequestNo; }
            set { _PurchaseRequestNo = value; }
        }
        [Column(Name = "FromLocationID", DataType = "Int32")]
        public Int32 FromLocationID
        {
            get { return _FromLocationID; }
            set { _FromLocationID = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "PurchaseRequestID", DataType = "Int32")]
        public Int32 PurchaseRequestID
        {
            get { return _PurchaseRequestID; }
            set { _PurchaseRequestID = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "BaseUnit", DataType = "String")]
        public String BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        [Column(Name = "GCPurchaseUnit", DataType = "String")]
        public String GCPurchaseUnit
        {
            get { return _GCPurchaseUnit; }
            set { _GCPurchaseUnit = value; }
        }
        [Column(Name = "PurchaseUnit", DataType = "String")]
        public String PurchaseUnit
        {
            get { return _PurchaseUnit; }
            set { _PurchaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "SupplierItemCode", DataType = "String")]
        public String SupplierItemCode
        {
            get { return _SupplierItemCode; }
            set { _SupplierItemCode = value; }
        }
        [Column(Name = "SupplierItemName", DataType = "String")]
        public String SupplierItemName
        {
            get { return _SupplierItemName; }
            set { _SupplierItemName = value; }
        }
        [Column(Name = "UnitPrice", DataType = "Decimal")]
        public Decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        [Column(Name = "DiscountPercentage", DataType = "Decimal")]
        public Decimal DiscountPercentage
        {
            get { return _DiscountPercentage; }
            set { _DiscountPercentage = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ItemGroupCode", DataType = "String")]
        public String ItemGroupCode
        {
            get { return _ItemGroupCode; }
            set { _ItemGroupCode = value; }
        }
        [Column(Name = "ItemGroupName1", DataType = "String")]
        public String ItemGroupName1
        {
            get { return _ItemGroupName1; }
            set { _ItemGroupName1 = value; }
        }
        [Column(Name = "QtyOnOrder", DataType = "Decimal")]
        public Decimal QtyOnOrder
        {
            get { return _QtyOnOrder; }
            set { _QtyOnOrder = value; }
        }
        [Column(Name = "QuantityEND", DataType = "Decimal")]
        public Decimal QuantityEND
        {
            get { return _QuantityEND; }
            set { _QuantityEND = value; }
        }
    }
    #endregion
    #region vPurchaseRequestHd
    [Serializable]
    [Table(Name = "vPurchaseRequestHd")]
    public partial class vPurchaseRequestHd
    {
        private Int32 _PurchaseRequestID;
        private DateTime _TransactionDate;
        private String _TransactionTime;
        private String _PurchaseRequestNo;
        private Int32 _ItemRequestID;
        private Int32 _FromLocationID;
        private String _LocationCode;
        private String _LocationName;
        private Int32 _LocationItemGroupID;
        private String _Remarks;
        private String _GCTransactionStatus;
        private String _TransactionStatusWatermark;
        private Int32 _NumberOfItems;
        private Int32 _NumberOfApprovedItems;
        private String _CreatedByName;
        private String _ApprovedByName;

        [Column(Name = "PurchaseRequestID", DataType = "Int32")]
        public Int32 PurchaseRequestID
        {
            get { return _PurchaseRequestID; }
            set { _PurchaseRequestID = value; }
        }
        [Column(Name = "TransactionDate", DataType = "DateTime")]
        public DateTime TransactionDate
        {
            get { return _TransactionDate; }
            set { _TransactionDate = value; }
        }
        [Column(Name = "TransactionTime", DataType = "String")]
        public String TransactionTime
        {
            get { return _TransactionTime; }
            set { _TransactionTime = value; }
        }
        [Column(Name = "PurchaseRequestNo", DataType = "String")]
        public String PurchaseRequestNo
        {
            get { return _PurchaseRequestNo; }
            set { _PurchaseRequestNo = value; }
        }
        [Column(Name = "ItemRequestID", DataType = "Int32")]
        public Int32 ItemRequestID
        {
            get { return _ItemRequestID; }
            set { _ItemRequestID = value; }
        }
        [Column(Name = "FromLocationID", DataType = "Int32")]
        public Int32 FromLocationID
        {
            get { return _FromLocationID; }
            set { _FromLocationID = value; }
        }
        [Column(Name = "LocationCode", DataType = "String")]
        public String LocationCode
        {
            get { return _LocationCode; }
            set { _LocationCode = value; }
        }
        [Column(Name = "LocationName", DataType = "String")]
        public String LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }
        [Column(Name = "LocationItemGroupID", DataType = "Int32")]
        public Int32 LocationItemGroupID
        {
            get { return _LocationItemGroupID; }
            set { _LocationItemGroupID = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "TransactionStatusWatermark", DataType = "String")]
        public String TransactionStatusWatermark
        {
            get { return _TransactionStatusWatermark; }
            set { _TransactionStatusWatermark = value; }
        }
        [Column(Name = "NumberOfItems", DataType = "Int32")]
        public Int32 NumberOfItems
        {
            get { return _NumberOfItems; }
            set { _NumberOfItems = value; }
        }
        [Column(Name = "NumberOfApprovedItems", DataType = "Int32")]
        public Int32 NumberOfApprovedItems
        {
            get { return _NumberOfApprovedItems; }
            set { _NumberOfApprovedItems = value; }
        }
        [Column(Name = "CreatedByName", DataType = "String")]
        public String CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }
        [Column(Name = "ApprovedByName", DataType = "String")]
        public String ApprovedByName
        {
            get { return _ApprovedByName; }
            set { _ApprovedByName = value; }
        }
    }
    #endregion
    #region vPurchaseRequestPO
    [Serializable]
    [Table(Name = "vPurchaseRequestPO")]
    public class vPurchaseRequestPO
    {
        private Int32 _ID;
        private Int32 _PurchaseRequestID;
        private String _PurchaseRequestNo;
        private Int32 _ItemID;
        private String _ItemName1;
        private String _ItemUnit;
        private Int32 _PurchaseOrderID;
        private Decimal _OrderQuantity;
        private Decimal _ReceivedQuantity;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "PurchaseRequestID", DataType = "Int32")]
        public Int32 PurchaseRequestID
        {
            get { return _PurchaseRequestID; }
            set { _PurchaseRequestID = value; }
        }
        [Column(Name = "PurchaseRequestNo", DataType = "String")]
        public String PurchaseRequestNo
        {
            get { return _PurchaseRequestNo; }
            set { _PurchaseRequestNo = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
        }
        [Column(Name = "PurchaseOrderID", DataType = "Int32")]
        public Int32 PurchaseOrderID
        {
            get { return _PurchaseOrderID; }
            set { _PurchaseOrderID = value; }
        }
        [Column(Name = "OrderQuantity", DataType = "Decimal")]
        public Decimal OrderQuantity
        {
            get { return _OrderQuantity; }
            set { _OrderQuantity = value; }
        }
        [Column(Name = "ReceivedQuantity", DataType = "Decimal")]
        public Decimal ReceivedQuantity
        {
            get { return _ReceivedQuantity; }
            set { _ReceivedQuantity = value; }
        }
    }
    #endregion
    #region vSchoolClass
    [Serializable]
    [Table(Name = "vSchoolClass")]
    public class vSchoolClass
    {
        private Int32 _SchoolClassID;
        private String _SchoolClassCode;
        private String _SchoolClassName;
        private Int32 _DailySchedulePackageID;
        private Int32 _PeriodClassTypeID;
        private Int32 _SchoolPeriodID;
        private String _SchoolPeriodName;
        private Int32 _PeriodSectionID;
        private String _PeriodSectionName;
        private Int32 _ClassTypeID;
        private String _ClassTypeName;
        private Int32 _RoomID;
        private String _RoomName;
        private Int32 _TeacherID;
        private String _TeacherName;
        private Int16 _MaxStudent;
        private Boolean _IsDeleted;

        [Column(Name = "SchoolClassID", DataType = "Int32")]
        public Int32 SchoolClassID
        {
            get { return _SchoolClassID; }
            set { _SchoolClassID = value; }
        }
        [Column(Name = "SchoolClassCode", DataType = "String")]
        public String SchoolClassCode
        {
            get { return _SchoolClassCode; }
            set { _SchoolClassCode = value; }
        }
        [Column(Name = "SchoolClassName", DataType = "String")]
        public String SchoolClassName
        {
            get { return _SchoolClassName; }
            set { _SchoolClassName = value; }
        }
        [Column(Name = "DailySchedulePackageID", DataType = "Int32")]
        public Int32 DailySchedulePackageID
        {
            get { return _DailySchedulePackageID; }
            set { _DailySchedulePackageID = value; }
        }
        [Column(Name = "PeriodClassTypeID", DataType = "Int32")]
        public Int32 PeriodClassTypeID
        {
            get { return _PeriodClassTypeID; }
            set { _PeriodClassTypeID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "SchoolPeriodName", DataType = "String")]
        public String SchoolPeriodName
        {
            get { return _SchoolPeriodName; }
            set { _SchoolPeriodName = value; }
        }
        [Column(Name = "PeriodSectionID", DataType = "Int32")]
        public Int32 PeriodSectionID
        {
            get { return _PeriodSectionID; }
            set { _PeriodSectionID = value; }
        }
        [Column(Name = "PeriodSectionName", DataType = "String")]
        public String PeriodSectionName
        {
            get { return _PeriodSectionName; }
            set { _PeriodSectionName = value; }
        }
        [Column(Name = "ClassTypeID", DataType = "Int32")]
        public Int32 ClassTypeID
        {
            get { return _ClassTypeID; }
            set { _ClassTypeID = value; }
        }
        [Column(Name = "ClassTypeName", DataType = "String")]
        public String ClassTypeName
        {
            get { return _ClassTypeName; }
            set { _ClassTypeName = value; }
        }
        [Column(Name = "RoomID", DataType = "Int32")]
        public Int32 RoomID
        {
            get { return _RoomID; }
            set { _RoomID = value; }
        }
        [Column(Name = "RoomName", DataType = "String")]
        public String RoomName
        {
            get { return _RoomName; }
            set { _RoomName = value; }
        }
        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
        }
        [Column(Name = "TeacherName", DataType = "String")]
        public String TeacherName
        {
            get { return _TeacherName; }
            set { _TeacherName = value; }
        }
        [Column(Name = "MaxStudent", DataType = "Int16")]
        public Int16 MaxStudent
        {
            get { return _MaxStudent; }
            set { _MaxStudent = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vStudent
    [Serializable]
    [Table(Name = "vStudent")]
    public partial class vStudent
    {
        private Int32 _StudentID;
        private String _StudentCode;
        private String _SiteID;
        private String _GCSalutation;
        private String _GCSuffix;
        private String _GCStudentStatus;
        private String _GCTitle;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _StudentName;
        private String _Name;
        private String _PreferredName;
        private String _CityOfBirth;
        private DateTime _DateOfBirth;
        private String _GCGender;
        private String _GCNationality;
        private String _GCGrade;
        private String _GCMajor;
        private Int32 _SchoolClassID;
        private String _SchoolClassName;
        private Int32 _AddressID;
        private String _StreetName;
        private String _District;
        private String _City;
        private String _County;
        private String _GCState;
        private String _State;
        private Int32 _ZipCodeID;
        private String _ZipCode;
        private String _EmailAddress1;
        private String _EmailAddress2;
        private String _MobilePhoneNo1;
        private String _MobilePhoneNo2;
        private String _PictureFileName;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "StudentCode", DataType = "String")]
        public String StudentCode
        {
            get { return _StudentCode; }
            set { _StudentCode = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "GCSalutation", DataType = "String")]
        public String GCSalutation
        {
            get { return _GCSalutation; }
            set { _GCSalutation = value; }
        }
        [Column(Name = "GCSuffix", DataType = "String")]
        public String GCSuffix
        {
            get { return _GCSuffix; }
            set { _GCSuffix = value; }
        }
        [Column(Name = "GCStudentStatus", DataType = "String")]
        public String GCStudentStatus
        {
            get { return _GCStudentStatus; }
            set { _GCStudentStatus = value; }
        }
        [Column(Name = "GCTitle", DataType = "String")]
        public String GCTitle
        {
            get { return _GCTitle; }
            set { _GCTitle = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "StudentName", DataType = "String")]
        public String StudentName
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }
        [Column(Name = "Name", DataType = "String")]
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        [Column(Name = "PreferredName", DataType = "String")]
        public String PreferredName
        {
            get { return _PreferredName; }
            set { _PreferredName = value; }
        }
        [Column(Name = "CityOfBirth", DataType = "String")]
        public String CityOfBirth
        {
            get { return _CityOfBirth; }
            set { _CityOfBirth = value; }
        }
        [Column(Name = "DateOfBirth", DataType = "DateTime")]
        public DateTime DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }
        [Column(Name = "GCGender", DataType = "String")]
        public String GCGender
        {
            get { return _GCGender; }
            set { _GCGender = value; }
        }
        [Column(Name = "GCNationality", DataType = "String")]
        public String GCNationality
        {
            get { return _GCNationality; }
            set { _GCNationality = value; }
        }
        [Column(Name = "GCGrade", DataType = "String")]
        public String GCGrade
        {
            get { return _GCGrade; }
            set { _GCGrade = value; }
        }
        [Column(Name = "GCMajor", DataType = "String")]
        public String GCMajor
        {
            get { return _GCMajor; }
            set { _GCMajor = value; }
        }
        [Column(Name = "SchoolClassID", DataType = "Int32")]
        public Int32 SchoolClassID
        {
            get { return _SchoolClassID; }
            set { _SchoolClassID = value; }
        }
        [Column(Name = "SchoolClassName", DataType = "String")]
        public String SchoolClassName
        {
            get { return _SchoolClassName; }
            set { _SchoolClassName = value; }
        }
        [Column(Name = "AddressID", DataType = "Int32")]
        public Int32 AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
        }
        [Column(Name = "StreetName", DataType = "String")]
        public String StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }
        [Column(Name = "District", DataType = "String")]
        public String District
        {
            get { return _District; }
            set { _District = value; }
        }
        [Column(Name = "City", DataType = "String")]
        public String City
        {
            get { return _City; }
            set { _City = value; }
        }
        [Column(Name = "County", DataType = "String")]
        public String County
        {
            get { return _County; }
            set { _County = value; }
        }
        [Column(Name = "GCState", DataType = "String")]
        public String GCState
        {
            get { return _GCState; }
            set { _GCState = value; }
        }
        [Column(Name = "State", DataType = "String")]
        public String State
        {
            get { return _State; }
            set { _State = value; }
        }
        [Column(Name = "ZipCodeID", DataType = "Int32")]
        public Int32 ZipCodeID
        {
            get { return _ZipCodeID; }
            set { _ZipCodeID = value; }
        }
        [Column(Name = "ZipCode", DataType = "String")]
        public String ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }
        [Column(Name = "EmailAddress1", DataType = "String")]
        public String EmailAddress1
        {
            get { return _EmailAddress1; }
            set { _EmailAddress1 = value; }
        }
        [Column(Name = "EmailAddress2", DataType = "String")]
        public String EmailAddress2
        {
            get { return _EmailAddress2; }
            set { _EmailAddress2 = value; }
        }
        [Column(Name = "MobilePhoneNo1", DataType = "String")]
        public String MobilePhoneNo1
        {
            get { return _MobilePhoneNo1; }
            set { _MobilePhoneNo1 = value; }
        }
        [Column(Name = "MobilePhoneNo2", DataType = "String")]
        public String MobilePhoneNo2
        {
            get { return _MobilePhoneNo2; }
            set { _MobilePhoneNo2 = value; }
        }
        [Column(Name = "PictureFileName", DataType = "String")]
        public String PictureFileName
        {
            get { return _PictureFileName; }
            set { _PictureFileName = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vSubjectGradeMajor
    [Serializable]
    [Table(Name = "vSubjectGradeMajor")]
    public class vSubjectGradeMajor
    {
        private Int32 _SubjectID;
        private String _SubjectCode;
        private String _SubjectName;
        private String _GCGrade;
        private String _Grade;
        private String _GCMajor;
        private String _Major;
        private Boolean _IsDeleted;

        [Column(Name = "SubjectID", DataType = "Int32")]
        public Int32 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
        [Column(Name = "SubjectCode", DataType = "String")]
        public String SubjectCode
        {
            get { return _SubjectCode; }
            set { _SubjectCode = value; }
        }
        [Column(Name = "SubjectName", DataType = "String")]
        public String SubjectName
        {
            get { return _SubjectName; }
            set { _SubjectName = value; }
        }
        [Column(Name = "GCGrade", DataType = "String")]
        public String GCGrade
        {
            get { return _GCGrade; }
            set { _GCGrade = value; }
        }
        [Column(Name = "Grade", DataType = "String")]
        public String Grade
        {
            get { return _Grade; }
            set { _Grade = value; }
        }
        [Column(Name = "GCMajor", DataType = "String")]
        public String GCMajor
        {
            get { return _GCMajor; }
            set { _GCMajor = value; }
        }
        [Column(Name = "Major", DataType = "String")]
        public String Major
        {
            get { return _Major; }
            set { _Major = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vSupplierItem
    [Serializable]
    [Table(Name = "vSupplierItem")]
    public class vSupplierItem
    {
        private Int32 _ID;
        private Int32 _BusinessPartnerID;
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private String _ItemName2;
        private String _SupplierItemCode;
        private String _SupplierItemName;
        private Decimal _Price;
        private Decimal _DiscountPercentage;
        private Int16 _LeadTime;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "ItemName2", DataType = "String")]
        public String ItemName2
        {
            get { return _ItemName2; }
            set { _ItemName2 = value; }
        }
        [Column(Name = "SupplierItemCode", DataType = "String")]
        public String SupplierItemCode
        {
            get { return _SupplierItemCode; }
            set { _SupplierItemCode = value; }
        }
        [Column(Name = "SupplierItemName", DataType = "String")]
        public String SupplierItemName
        {
            get { return _SupplierItemName; }
            set { _SupplierItemName = value; }
        }
        [Column(Name = "Price", DataType = "Decimal")]
        public Decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        [Column(Name = "DiscountPercentage", DataType = "Decimal")]
        public Decimal DiscountPercentage
        {
            get { return _DiscountPercentage; }
            set { _DiscountPercentage = value; }
        }
        [Column(Name = "LeadTime", DataType = "Int16")]
        public Int16 LeadTime
        {
            get { return _LeadTime; }
            set { _LeadTime = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vSupplierItemPlaning
    [Serializable]
    [Table(Name = "vSupplierItemPlaning")]
    public class vSupplierItemPlaning
    {
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private Boolean _IsDeleted;
        private String _GCItemType;
        private String _GCItemUnit;
        private Int32? _BusinessPartnerID;
        private String _BusinessPartnerName;
        private String _BusinessPartnerCode;
        private Decimal _UnitPrice;
        private Decimal _Discount;
        private String _SupplierItemCode;
        private String _SupplierItemName;

        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ItemCode", DataType = "String")]
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        [Column(Name = "ItemName1", DataType = "String")]
        public String ItemName1
        {
            get { return _ItemName1; }
            set { _ItemName1 = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "GCItemType", DataType = "String")]
        public String GCItemType
        {
            get { return _GCItemType; }
            set { _GCItemType = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32? BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String")]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "BusinessPartnerCode", DataType = "String")]
        public String BusinessPartnerCode
        {
            get { return _BusinessPartnerCode; }
            set { _BusinessPartnerCode = value; }
        }
        [Column(Name = "UnitPrice", DataType = "Decimal")]
        public Decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        [Column(Name = "Discount", DataType = "Decimal")]
        public Decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }
        [Column(Name = "SupplierItemCode", DataType = "String")]
        public String SupplierItemCode
        {
            get { return _SupplierItemCode; }
            set { _SupplierItemCode = value; }
        }
        [Column(Name = "SupplierItemName", DataType = "String")]
        public String SupplierItemName
        {
            get { return _SupplierItemName; }
            set { _SupplierItemName = value; }
        }
    }
    #endregion
    #region vTeacher
    [Serializable]
    [Table(Name = "vTeacher")]
    public class vTeacher
    {
        private Int32 _TeacherID;
        private String _TeacherCode;
        private String _GCSalutation;
        private String _GCTitle;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _GCSuffix;
        private String _EmailAddress;
        private String _MobilePhone1;
        private String _MobilePhone2;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
        }
        [Column(Name = "TeacherCode", DataType = "String")]
        public String TeacherCode
        {
            get { return _TeacherCode; }
            set { _TeacherCode = value; }
        }
        [Column(Name = "GCSalutation", DataType = "String")]
        public String GCSalutation
        {
            get { return _GCSalutation; }
            set { _GCSalutation = value; }
        }
        [Column(Name = "GCTitle", DataType = "String")]
        public String GCTitle
        {
            get { return _GCTitle; }
            set { _GCTitle = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "GCSuffix", DataType = "String")]
        public String GCSuffix
        {
            get { return _GCSuffix; }
            set { _GCSuffix = value; }
        }
        [Column(Name = "EmailAddress", DataType = "String")]
        public String EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        [Column(Name = "MobilePhone1", DataType = "String")]
        public String MobilePhone1
        {
            get { return _MobilePhone1; }
            set { _MobilePhone1 = value; }
        }
        [Column(Name = "MobilePhone2", DataType = "String")]
        public String MobilePhone2
        {
            get { return _MobilePhone2; }
            set { _MobilePhone2 = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vTeacherSubject
    [Serializable]
    [Table(Name = "vTeacherSubject")]
    public class vTeacherSubject
    {
        private Int32 _TeacherID;
        private String _TeacherCode;
        private String _TeacherName;
        private Int32 _SubjectID;
        private String _SubjectCode;
        private String _SubjectName;

        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
        }
        [Column(Name = "TeacherCode", DataType = "String")]
        public String TeacherCode
        {
            get { return _TeacherCode; }
            set { _TeacherCode = value; }
        }
        [Column(Name = "TeacherName", DataType = "String")]
        public String TeacherName
        {
            get { return _TeacherName; }
            set { _TeacherName = value; }
        }
        [Column(Name = "SubjectID", DataType = "Int32")]
        public Int32 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
        [Column(Name = "SubjectCode", DataType = "String")]
        public String SubjectCode
        {
            get { return _SubjectCode; }
            set { _SubjectCode = value; }
        }
        [Column(Name = "SubjectName", DataType = "String")]
        public String SubjectName
        {
            get { return _SubjectName; }
            set { _SubjectName = value; }
        }
    }
    #endregion
}
