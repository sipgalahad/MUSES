using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeX.Data.Core.Dal;
using System.Data;

namespace CodeX.Data.Model
{
    #region BusinessPartners
    [Serializable]
    [Table(Name = "BusinessPartners")]
    public class BusinessPartners : DbDataModel
    {
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private String _ShortName;
        private String _GCBusinessPartnerType;
        private String _ContactPerson;
        private String _SiteID;
        private Int64? _AddressID;
        private Boolean _IsTaxable;
        private String _VATRegistrationNo;
        private Int32? _TermID;
        private Boolean _IsBlackList;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "BusinessPartnerID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "ShortName", DataType = "String", IsNullable = true)]
        public String ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }
        [Column(Name = "GCBusinessPartnerType", DataType = "String")]
        public String GCBusinessPartnerType
        {
            get { return _GCBusinessPartnerType; }
            set { _GCBusinessPartnerType = value; }
        }
        [Column(Name = "ContactPerson", DataType = "String", IsNullable = true)]
        public String ContactPerson
        {
            get { return _ContactPerson; }
            set { _ContactPerson = value; }
        }
        [Column(Name = "SiteID", DataType = "String", IsNullable = true)]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "AddressID", DataType = "Int64", IsNullable = true)]
        public Int64? AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
        }
        [Column(Name = "IsTaxable", DataType = "Boolean", IsNullable = true)]
        public Boolean IsTaxable
        {
            get { return _IsTaxable; }
            set { _IsTaxable = value; }
        }
        [Column(Name = "VATRegistrationNo", DataType = "String", IsNullable = true)]
        public String VATRegistrationNo
        {
            get { return _VATRegistrationNo; }
            set { _VATRegistrationNo = value; }
        }
        [Column(Name = "TermID", DataType = "Int32", IsNullable = true)]
        public Int32? TermID
        {
            get { return _TermID; }
            set { _TermID = value; }
        }
        [Column(Name = "IsBlackList", DataType = "Boolean", IsNullable = true)]
        public Boolean IsBlackList
        {
            get { return _IsBlackList; }
            set { _IsBlackList = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class BusinessPartnersDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(BusinessPartners));
        private bool _isAuditLog = false;
        private const string p_BusinessPartnerID = "@p_BusinessPartnerID";
        public BusinessPartnersDao() { }
        public BusinessPartnersDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public BusinessPartners Get(Int32 BusinessPartnerID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_BusinessPartnerID, BusinessPartnerID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (BusinessPartners)_helper.DataRowToObject(row, new BusinessPartners());
        }
        public int Insert(BusinessPartners record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(BusinessPartners record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 BusinessPartnerID)
        {
            BusinessPartners record;
            if (_ctx.Transaction == null)
                record = new BusinessPartnersDao().Get(BusinessPartnerID);
            else
                record = Get(BusinessPartnerID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ClassMeeting
    [Serializable]
    [Table(Name = "ClassMeeting")]
    public class ClassMeeting : DbDataModel
    {
        private Int32 _ClassMeetingID;
        private Int32 _ClassSubjectID;
        private DateTime _MeetingDate;
        private String _StartTime;
        private String _EndTime;
        private Int32 _RoomID;
        private Int32 _TeacherID;
        private String _Remarks;
        private String _NextMeetingRemarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ClassMeetingID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "NextMeetingRemarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class ClassMeetingDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ClassMeeting));
        private bool _isAuditLog = false;
        private const string p_ClassMeetingID = "@p_ClassMeetingID";
        public ClassMeetingDao() { }
        public ClassMeetingDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ClassMeeting Get(Int32 ClassMeetingID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ClassMeetingID, ClassMeetingID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ClassMeeting)_helper.DataRowToObject(row, new ClassMeeting());
        }
        public int Insert(ClassMeeting record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ClassMeeting record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ClassMeetingID)
        {
            ClassMeeting record;
            if (_ctx.Transaction == null)
                record = new ClassMeetingDao().Get(ClassMeetingID);
            else
                record = Get(ClassMeetingID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ClassSchedule
    [Serializable]
    [Table(Name = "ClassSchedule")]
    public class ClassSchedule : DbDataModel
    {
        private Int32 _ClassScheduleID;
        private Int32 _SchoolClassID;
        private Int32 _ClassSubjectID;
        private Int16 _DayNumber;
        private Int16 _HoursIndex;
        private Int32 _RoomID;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ClassScheduleID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ClassScheduleID
        {
            get { return _ClassScheduleID; }
            set { _ClassScheduleID = value; }
        }
        [Column(Name = "SchoolClassID", DataType = "Int32")]
        public Int32 SchoolClassID
        {
            get { return _SchoolClassID; }
            set { _SchoolClassID = value; }
        }
        [Column(Name = "ClassSubjectID", DataType = "Int32")]
        public Int32 ClassSubjectID
        {
            get { return _ClassSubjectID; }
            set { _ClassSubjectID = value; }
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
        [Column(Name = "RoomID", DataType = "Int32")]
        public Int32 RoomID
        {
            get { return _RoomID; }
            set { _RoomID = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class ClassScheduleDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ClassSchedule));
        private bool _isAuditLog = false;
        private const string p_ClassScheduleID = "@p_ClassScheduleID";
        public ClassScheduleDao() { }
        public ClassScheduleDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ClassSchedule Get(Int32 ClassScheduleID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ClassScheduleID, ClassScheduleID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ClassSchedule)_helper.DataRowToObject(row, new ClassSchedule());
        }
        public int Insert(ClassSchedule record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ClassSchedule record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ClassScheduleID)
        {
            ClassSchedule record;
            if (_ctx.Transaction == null)
                record = new ClassScheduleDao().Get(ClassScheduleID);
            else
                record = Get(ClassScheduleID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ClassStudent
    [Serializable]
    [Table(Name = "ClassStudent")]
    public class ClassStudent : DbDataModel
    {
        private Int32 _SchoolClassID;
        private Int32 _StudentID;

        [Column(Name = "SchoolClassID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 SchoolClassID
        {
            get { return _SchoolClassID; }
            set { _SchoolClassID = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
    }

    public class ClassStudentDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ClassStudent));
        private bool _isAuditLog = false;
        private const string p_SchoolClassID = "@p_SchoolClassID";
        private const string p_StudentID = "@p_StudentID";
        public ClassStudentDao() { }
        public ClassStudentDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ClassStudent Get(Int32 SchoolClassID, Int32 StudentID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_SchoolClassID, SchoolClassID);
            _ctx.Add(p_StudentID, StudentID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ClassStudent)_helper.DataRowToObject(row, new ClassStudent());
        }
        public int Insert(ClassStudent record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ClassStudent record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 SchoolClassID, Int32 StudentID)
        {
            ClassStudent record;
            if (_ctx.Transaction == null)
                record = new ClassStudentDao().Get(SchoolClassID, StudentID);
            else
                record = Get(SchoolClassID, StudentID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ClassSubject
    [Serializable]
    [Table(Name = "ClassSubject")]
    public class ClassSubject : DbDataModel
    {
        private Int32 _ClassSubjectID;
        private Int32 _SchoolClassID;
        private Int32 _PeriodClassTypeSubjectID;
        private Int32 _TeacherID;
        private Int16 _NoMeetingHoursInWeek;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ClassSubjectID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "PeriodClassTypeSubjectID", DataType = "Int32")]
        public Int32 PeriodClassTypeSubjectID
        {
            get { return _PeriodClassTypeSubjectID; }
            set { _PeriodClassTypeSubjectID = value; }
        }
        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class ClassSubjectDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ClassSubject));
        private bool _isAuditLog = false;
        private const string p_ClassSubjectID = "@p_ClassSubjectID";
        public ClassSubjectDao() { }
        public ClassSubjectDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ClassSubject Get(Int32 ClassSubjectID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ClassSubjectID, ClassSubjectID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ClassSubject)_helper.DataRowToObject(row, new ClassSubject());
        }
        public int Insert(ClassSubject record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ClassSubject record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ClassSubjectID)
        {
            ClassSubject record;
            if (_ctx.Transaction == null)
                record = new ClassSubjectDao().Get(ClassSubjectID);
            else
                record = Get(ClassSubjectID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region DailySchedule
    [Serializable]
    [Table(Name = "DailySchedule")]
    public class DailySchedule : DbDataModel
    {
        private Int32 _DailyScheduleID;
        private Int32 _SchoolPeriodID;
        private Int16 _DayNumber;
        private Int32 _DailyScheduleTypeID;

        [Column(Name = "DailyScheduleID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 DailyScheduleID
        {
            get { return _DailyScheduleID; }
            set { _DailyScheduleID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "DayNumber", DataType = "Int16")]
        public Int16 DayNumber
        {
            get { return _DayNumber; }
            set { _DayNumber = value; }
        }
        [Column(Name = "DailyScheduleTypeID", DataType = "Int32")]
        public Int32 DailyScheduleTypeID
        {
            get { return _DailyScheduleTypeID; }
            set { _DailyScheduleTypeID = value; }
        }
    }

    public class DailyScheduleDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(DailySchedule));
        private bool _isAuditLog = false;
        private const string p_DailyScheduleID = "@p_DailyScheduleID";
        public DailyScheduleDao() { }
        public DailyScheduleDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public DailySchedule Get(Int32 DailyScheduleID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_DailyScheduleID, DailyScheduleID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (DailySchedule)_helper.DataRowToObject(row, new DailySchedule());
        }
        public int Insert(DailySchedule record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(DailySchedule record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 DailyScheduleID)
        {
            DailySchedule record;
            if (_ctx.Transaction == null)
                record = new DailyScheduleDao().Get(DailyScheduleID);
            else
                record = Get(DailyScheduleID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region DailySchedulePackage
    [Serializable]
    [Table(Name = "DailySchedulePackage")]
    public class DailySchedulePackage : DbDataModel
    {
        private Int32 _DailySchedulePackageID;
        private String _DailySchedulePackageCode;
        private String _DailySchedulePackageName;
        private String _SiteID;
        private Int32? _DailyScheduleTypeID1;
        private Int32? _DailyScheduleTypeID2;
        private Int32? _DailyScheduleTypeID3;
        private Int32? _DailyScheduleTypeID4;
        private Int32? _DailyScheduleTypeID5;
        private Int32? _DailyScheduleTypeID6;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "DailySchedulePackageID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 DailySchedulePackageID
        {
            get { return _DailySchedulePackageID; }
            set { _DailySchedulePackageID = value; }
        }
        [Column(Name = "DailySchedulePackageCode", DataType = "String")]
        public String DailySchedulePackageCode
        {
            get { return _DailySchedulePackageCode; }
            set { _DailySchedulePackageCode = value; }
        }
        [Column(Name = "DailySchedulePackageName", DataType = "String")]
        public String DailySchedulePackageName
        {
            get { return _DailySchedulePackageName; }
            set { _DailySchedulePackageName = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "DailyScheduleTypeID1", DataType = "Int32", IsNullable = true)]
        public Int32? DailyScheduleTypeID1
        {
            get { return _DailyScheduleTypeID1; }
            set { _DailyScheduleTypeID1 = value; }
        }
        [Column(Name = "DailyScheduleTypeID2", DataType = "Int32", IsNullable = true)]
        public Int32? DailyScheduleTypeID2
        {
            get { return _DailyScheduleTypeID2; }
            set { _DailyScheduleTypeID2 = value; }
        }
        [Column(Name = "DailyScheduleTypeID3", DataType = "Int32", IsNullable = true)]
        public Int32? DailyScheduleTypeID3
        {
            get { return _DailyScheduleTypeID3; }
            set { _DailyScheduleTypeID3 = value; }
        }
        [Column(Name = "DailyScheduleTypeID4", DataType = "Int32", IsNullable = true)]
        public Int32? DailyScheduleTypeID4
        {
            get { return _DailyScheduleTypeID4; }
            set { _DailyScheduleTypeID4 = value; }
        }
        [Column(Name = "DailyScheduleTypeID5", DataType = "Int32", IsNullable = true)]
        public Int32? DailyScheduleTypeID5
        {
            get { return _DailyScheduleTypeID5; }
            set { _DailyScheduleTypeID5 = value; }
        }
        [Column(Name = "DailyScheduleTypeID6", DataType = "Int32", IsNullable = true)]
        public Int32? DailyScheduleTypeID6
        {
            get { return _DailyScheduleTypeID6; }
            set { _DailyScheduleTypeID6 = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class DailySchedulePackageDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(DailySchedulePackage));
        private bool _isAuditLog = false;
        private const string p_DailySchedulePackageID = "@p_DailySchedulePackageID";
        public DailySchedulePackageDao() { }
        public DailySchedulePackageDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public DailySchedulePackage Get(Int32 DailySchedulePackageID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_DailySchedulePackageID, DailySchedulePackageID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (DailySchedulePackage)_helper.DataRowToObject(row, new DailySchedulePackage());
        }
        public int Insert(DailySchedulePackage record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(DailySchedulePackage record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 DailySchedulePackageID)
        {
            DailySchedulePackage record;
            if (_ctx.Transaction == null)
                record = new DailySchedulePackageDao().Get(DailySchedulePackageID);
            else
                record = Get(DailySchedulePackageID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region DailyScheduleTypeDt
    [Serializable]
    [Table(Name = "DailyScheduleTypeDt")]
    public partial class DailyScheduleTypeDt : DbDataModel
    {
        private Int32 _DailyScheduleTypeDtID;
        private Int32? _DailyScheduleTypeID;
        private Int16 _HoursIndex;
        private String _StartTime;
        private String _EndTime;
        private String _GCDailyScheduleType;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "DailyScheduleTypeDtID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 DailyScheduleTypeDtID
        {
            get { return _DailyScheduleTypeDtID; }
            set { _DailyScheduleTypeDtID = value; }
        }
        [Column(Name = "DailyScheduleTypeID", DataType = "Int32", IsNullable = true)]
        public Int32? DailyScheduleTypeID
        {
            get { return _DailyScheduleTypeID; }
            set { _DailyScheduleTypeID = value; }
        }
        [Column(Name = "HoursIndex", DataType = "Int16", IsNullable = true)]
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
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class DailyScheduleTypeDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(DailyScheduleTypeDt));
        private bool _isAuditLog = false;
        private const string p_DailyScheduleTypeDtID = "@p_DailyScheduleTypeDtID";
        public DailyScheduleTypeDtDao() { }
        public DailyScheduleTypeDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public DailyScheduleTypeDt Get(Int32 DailyScheduleTypeDtID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_DailyScheduleTypeDtID, DailyScheduleTypeDtID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (DailyScheduleTypeDt)_helper.DataRowToObject(row, new DailyScheduleTypeDt());
        }
        public int Insert(DailyScheduleTypeDt record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(DailyScheduleTypeDt record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 DailyScheduleTypeDtID)
        {
            DailyScheduleTypeDt record;
            if (_ctx.Transaction == null)
                record = new DailyScheduleTypeDtDao().Get(DailyScheduleTypeDtID);
            else
                record = Get(DailyScheduleTypeDtID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region DailyScheduleTypeHd
    [Serializable]
    [Table(Name = "DailyScheduleTypeHd")]
    public class DailyScheduleTypeHd : DbDataModel
    {
        private Int32 _DailyScheduleTypeID;
        private String _DailyScheduleTypeCode;
        private String _DailyScheduleTypeName;
        private String _SiteID;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "DailyScheduleTypeID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 DailyScheduleTypeID
        {
            get { return _DailyScheduleTypeID; }
            set { _DailyScheduleTypeID = value; }
        }
        [Column(Name = "DailyScheduleTypeCode", DataType = "String")]
        public String DailyScheduleTypeCode
        {
            get { return _DailyScheduleTypeCode; }
            set { _DailyScheduleTypeCode = value; }
        }
        [Column(Name = "DailyScheduleTypeName", DataType = "String")]
        public String DailyScheduleTypeName
        {
            get { return _DailyScheduleTypeName; }
            set { _DailyScheduleTypeName = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class DailyScheduleTypeHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(DailyScheduleTypeHd));
        private bool _isAuditLog = false;
        private const string p_DailyScheduleTypeID = "@p_DailyScheduleTypeID";
        public DailyScheduleTypeHdDao() { }
        public DailyScheduleTypeHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public DailyScheduleTypeHd Get(Int32 DailyScheduleTypeID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_DailyScheduleTypeID, DailyScheduleTypeID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (DailyScheduleTypeHd)_helper.DataRowToObject(row, new DailyScheduleTypeHd());
        }
        public int Insert(DailyScheduleTypeHd record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(DailyScheduleTypeHd record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 DailyScheduleTypeID)
        {
            DailyScheduleTypeHd record;
            if (_ctx.Transaction == null)
                record = new DailyScheduleTypeHdDao().Get(DailyScheduleTypeID);
            else
                record = Get(DailyScheduleTypeID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region DirectPurchaseDt
    [Serializable]
    [Table(Name = "DirectPurchaseDt")]
    public partial class DirectPurchaseDt : DbDataModel
    {
        private Int32 _ID;
        private Int32 _DirectPurchaseID;
        private Int32 _ItemID;
        private Decimal _Quantity;
        private String _GCItemUnit;
        private String _GCBaseUnit;
        private Decimal _ConversionFactor;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage;
        private Boolean _IsControlExpired;
        private String _GCItemDetailStatus;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
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
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
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
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class DirectPurchaseDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(DirectPurchaseDt));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public DirectPurchaseDtDao() { }
        public DirectPurchaseDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public DirectPurchaseDt Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (DirectPurchaseDt)_helper.DataRowToObject(row, new DirectPurchaseDt());
        }
        public int Insert(DirectPurchaseDt record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(DirectPurchaseDt record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            DirectPurchaseDt record;
            if (_ctx.Transaction == null)
                record = new DirectPurchaseDtDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region DirectPurchaseHd
    [Serializable]
    [Table(Name = "DirectPurchaseHd")]
    public class DirectPurchaseHd : DbDataModel
    {
        private Int32 _DirectPurchaseID;
        private String _DirectPurchaseNo;
        private DateTime _PurchaseDate;
        private Int32 _LocationID;
        private Int32 _BusinessPartnerID;
        private String _GCDirectPurchaseType;
        private String _ReferenceNo;
        private DateTime _ReferenceDate;
        private Boolean _IsIncludeVAT;
        private Decimal _TransactionAmount;
        private Decimal _VATPercentage;
        private String _Remarks;
        private String _GCTransactionStatus;
        private Boolean _IsHasPurchaseReturn;
        private Int32? _DirectPurchaseReturnID;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "DirectPurchaseID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "GCDirectPurchaseType", DataType = "String")]
        public String GCDirectPurchaseType
        {
            get { return _GCDirectPurchaseType; }
            set { _GCDirectPurchaseType = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String", IsNullable = true)]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "ReferenceDate", DataType = "DateTime", IsNullable = true)]
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
        [Column(Name = "IsHasPurchaseReturn", DataType = "Boolean")]
        public Boolean IsHasPurchaseReturn
        {
            get { return _IsHasPurchaseReturn; }
            set { _IsHasPurchaseReturn = value; }
        }
        [Column(Name = "DirectPurchaseReturnID", DataType = "Int32", IsNullable = true)]
        public Int32? DirectPurchaseReturnID
        {
            get { return _DirectPurchaseReturnID; }
            set { _DirectPurchaseReturnID = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class DirectPurchaseHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(DirectPurchaseHd));
        private bool _isAuditLog = false;
        private const string p_DirectPurchaseID = "@p_DirectPurchaseID";
        public DirectPurchaseHdDao() { }
        public DirectPurchaseHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public DirectPurchaseHd Get(Int32 DirectPurchaseID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_DirectPurchaseID, DirectPurchaseID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (DirectPurchaseHd)_helper.DataRowToObject(row, new DirectPurchaseHd());
        }
        public int Insert(DirectPurchaseHd record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(DirectPurchaseHd record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 DirectPurchaseID)
        {
            DirectPurchaseHd record;
            if (_ctx.Transaction == null)
                record = new DirectPurchaseHdDao().Get(DirectPurchaseID);
            else
                record = Get(DirectPurchaseID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region DirectPurchaseReturnDt
    [Serializable]
    [Table(Name = "DirectPurchaseReturnDt")]
    public class DirectPurchaseReturnDt : DbDataModel
    {
        private Int32 _ID;
        private Int32 _DirectPurchaseReturnID;
        private Int32 _ItemID;
        private Decimal _Quantity;
        private String _GCItemUnit;
        private String _GCBaseUnit;
        private Decimal _ConversionFactor;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage1;
        private Decimal _DiscountPercentage2;
        private Boolean _IsControlExpired;
        private String _GCPurchaseReturnReason;
        private String _PurchaseReturnReason;
        private String _GCItemDetailStatus;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
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
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
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
        [Column(Name = "IsControlExpired", DataType = "Boolean")]
        public Boolean IsControlExpired
        {
            get { return _IsControlExpired; }
            set { _IsControlExpired = value; }
        }
        [Column(Name = "GCPurchaseReturnReason", DataType = "String")]
        public String GCPurchaseReturnReason
        {
            get { return _GCPurchaseReturnReason; }
            set { _GCPurchaseReturnReason = value; }
        }
        [Column(Name = "PurchaseReturnReason", DataType = "String", IsNullable = true)]
        public String PurchaseReturnReason
        {
            get { return _PurchaseReturnReason; }
            set { _PurchaseReturnReason = value; }
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
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class DirectPurchaseReturnDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(DirectPurchaseReturnDt));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public DirectPurchaseReturnDtDao() { }
        public DirectPurchaseReturnDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public DirectPurchaseReturnDt Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (DirectPurchaseReturnDt)_helper.DataRowToObject(row, new DirectPurchaseReturnDt());
        }
        public int Insert(DirectPurchaseReturnDt record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(DirectPurchaseReturnDt record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            DirectPurchaseReturnDt record;
            if (_ctx.Transaction == null)
                record = new DirectPurchaseReturnDtDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region DirectPurchaseReturnHd
    [Serializable]
    [Table(Name = "DirectPurchaseReturnHd")]
    public class DirectPurchaseReturnHd : DbDataModel
    {
        private Int32 _DirectPurchaseReturnID;
        private DateTime _ReturnDate;
        private String _DirectPurchaseReturnNo;
        private Int32 _DirectPurchaseID;
        private Int32 _LocationID;
        private Int32 _BusinessPartnerID;
        private String _GCDirectPurchaseReturnType;
        private String _ReferenceNo;
        private DateTime _ReferenceDate;
        private Boolean _IsIncludeVAT;
        private Decimal _TransactionAmount;
        private Decimal _VATPercentage;
        private String _Remarks;
        private String _GCTransactionStatus;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "DirectPurchaseReturnID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "GCDirectPurchaseReturnType", DataType = "String")]
        public String GCDirectPurchaseReturnType
        {
            get { return _GCDirectPurchaseReturnType; }
            set { _GCDirectPurchaseReturnType = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String", IsNullable = true)]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "ReferenceDate", DataType = "DateTime", IsNullable = true)]
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
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class DirectPurchaseReturnHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(DirectPurchaseReturnHd));
        private bool _isAuditLog = false;
        private const string p_DirectPurchaseReturnID = "@p_DirectPurchaseReturnID";
        public DirectPurchaseReturnHdDao() { }
        public DirectPurchaseReturnHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public DirectPurchaseReturnHd Get(Int32 DirectPurchaseReturnID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_DirectPurchaseReturnID, DirectPurchaseReturnID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (DirectPurchaseReturnHd)_helper.DataRowToObject(row, new DirectPurchaseReturnHd());
        }
        public int Insert(DirectPurchaseReturnHd record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(DirectPurchaseReturnHd record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 DirectPurchaseReturnID)
        {
            DirectPurchaseReturnHd record;
            if (_ctx.Transaction == null)
                record = new DirectPurchaseReturnHdDao().Get(DirectPurchaseReturnID);
            else
                record = Get(DirectPurchaseReturnID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ItemBalance
    [Serializable]
    [Table(Name = "ItemBalance")]
    public class ItemBalance : DbDataModel
    {
        private Int32 _ID;
        private Int32 _LocationID;
        private Int32 _ItemID;
        private String _GCReorderType;
        private Decimal _QuantityBEGIN;
        private Decimal _QuantityIN;
        private Decimal _QuantityOUT;
        private Decimal _QuantityEND;
        private Decimal _QuantityMIN;
        private Decimal _QuantityMAX;
        private Boolean _IsDeleted;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "GCReorderType", DataType = "String")]
        public String GCReorderType
        {
            get { return _GCReorderType; }
            set { _GCReorderType = value; }
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
        [Column(Name = "QuantityEND", DataType = "Decimal", IsNullable = true, IsComputed = true)]
        public Decimal QuantityEND
        {
            get { return _QuantityEND; }
            set { _QuantityEND = value; }
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
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class ItemBalanceDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ItemBalance));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public ItemBalanceDao() { }
        public ItemBalanceDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ItemBalance Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ItemBalance)_helper.DataRowToObject(row, new ItemBalance());
        }
        public int Insert(ItemBalance record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ItemBalance record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            ItemBalance record;
            if (_ctx.Transaction == null)
                record = new ItemBalanceDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ItemDistributionDt
    [Serializable]
    [Table(Name = "ItemDistributionDt")]
    public class ItemDistributionDt : DbDataModel
    {
        private Int32 _ID;
        private Int32 _DistributionID;
        private Int32 _ItemID;
        private Decimal _Quantity;
        private String _GCItemUnit;
        private String _GCBaseUnit;
        private Decimal _ConversionFactor;
        private Boolean _IsControlExpired;
        private String _Remarks;
        private String _GCItemDetailStatus;
        private Boolean _IsDeleted;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
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
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "IsControlExpired", DataType = "Boolean")]
        public Boolean IsControlExpired
        {
            get { return _IsControlExpired; }
            set { _IsControlExpired = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class ItemDistributionDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ItemDistributionDt));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public ItemDistributionDtDao() { }
        public ItemDistributionDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ItemDistributionDt Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ItemDistributionDt)_helper.DataRowToObject(row, new ItemDistributionDt());
        }
        public int Insert(ItemDistributionDt record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ItemDistributionDt record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            ItemDistributionDt record;
            if (_ctx.Transaction == null)
                record = new ItemDistributionDtDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ItemDistributionHd
    [Serializable]
    [Table(Name = "ItemDistributionHd")]
    public class ItemDistributionHd : DbDataModel
    {
        private Int32 _DistributionID;
        private String _DistributionNo;
        private Int32? _ItemRequestID;
        private Int32 _FromLocationID;
        private Int32 _ToLocationID;
        private DateTime _DeliveryDate;
        private String _DeliveryTime;
        private String _DeliveredBy;
        private DateTime _ReceivedDate;
        private String _ReceivedTime;
        private String _ReceivedBy;
        private String _GCDistributionStatus;
        private String _DeliveryRemarks;
        private String _ReceivedRemarks;
        private Boolean _IsGeneratedBySystem;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "DistributionID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "ItemRequestID", DataType = "Int32", IsNullable = true)]
        public Int32? ItemRequestID
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
        [Column(Name = "ToLocationID", DataType = "Int32")]
        public Int32 ToLocationID
        {
            get { return _ToLocationID; }
            set { _ToLocationID = value; }
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
        [Column(Name = "DeliveredBy", DataType = "String")]
        public String DeliveredBy
        {
            get { return _DeliveredBy; }
            set { _DeliveredBy = value; }
        }
        [Column(Name = "ReceivedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime ReceivedDate
        {
            get { return _ReceivedDate; }
            set { _ReceivedDate = value; }
        }
        [Column(Name = "ReceivedTime", DataType = "String", IsNullable = true)]
        public String ReceivedTime
        {
            get { return _ReceivedTime; }
            set { _ReceivedTime = value; }
        }
        [Column(Name = "ReceivedBy", DataType = "String", IsNullable = true)]
        public String ReceivedBy
        {
            get { return _ReceivedBy; }
            set { _ReceivedBy = value; }
        }
        [Column(Name = "GCDistributionStatus", DataType = "String", IsNullable = true)]
        public String GCDistributionStatus
        {
            get { return _GCDistributionStatus; }
            set { _GCDistributionStatus = value; }
        }
        [Column(Name = "DeliveryRemarks", DataType = "String", IsNullable = true)]
        public String DeliveryRemarks
        {
            get { return _DeliveryRemarks; }
            set { _DeliveryRemarks = value; }
        }
        [Column(Name = "ReceivedRemarks", DataType = "String", IsNullable = true)]
        public String ReceivedRemarks
        {
            get { return _ReceivedRemarks; }
            set { _ReceivedRemarks = value; }
        }
        [Column(Name = "IsGeneratedBySystem", DataType = "Boolean")]
        public Boolean IsGeneratedBySystem
        {
            get { return _IsGeneratedBySystem; }
            set { _IsGeneratedBySystem = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class ItemDistributionHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ItemDistributionHd));
        private bool _isAuditLog = false;
        private const string p_DistributionID = "@p_DistributionID";
        public ItemDistributionHdDao() { }
        public ItemDistributionHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ItemDistributionHd Get(Int32 DistributionID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_DistributionID, DistributionID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ItemDistributionHd)_helper.DataRowToObject(row, new ItemDistributionHd());
        }
        public int Insert(ItemDistributionHd record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ItemDistributionHd record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 DistributionID)
        {
            ItemDistributionHd record;
            if (_ctx.Transaction == null)
                record = new ItemDistributionHdDao().Get(DistributionID);
            else
                record = Get(DistributionID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ItemMaster
    [Serializable]
    [Table(Name = "ItemMaster")]
    public class ItemMaster : DbDataModel
    {
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private String _ItemName2;
        private String _GCItemType;
        private String _GCItemStatus;
        private String _GCItemUnit;
        private Int32 _ItemGroupID;
        private Int32? _ProductLineID;
        private String _Remarks;
        private Boolean _IsIncludeInAdminCalculation;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ItemID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "ItemName2", DataType = "String", IsNullable = true)]
        public String ItemName2
        {
            get { return _ItemName2; }
            set { _ItemName2 = value; }
        }
        [Column(Name = "GCItemType", DataType = "String")]
        public String GCItemType
        {
            get { return _GCItemType; }
            set { _GCItemType = value; }
        }
        [Column(Name = "GCItemStatus", DataType = "String", IsNullable = true)]
        public String GCItemStatus
        {
            get { return _GCItemStatus; }
            set { _GCItemStatus = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String", IsNullable = true)]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32")]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "ProductLineID", DataType = "Int32?", IsNullable = true)]
        public Int32? ProductLineID
        {
            get { return _ProductLineID; }
            set { _ProductLineID = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsIncludeInAdminCalculation", DataType = "Boolean", IsNullable = true)]
        public Boolean IsIncludeInAdminCalculation
        {
            get { return _IsIncludeInAdminCalculation; }
            set { _IsIncludeInAdminCalculation = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class ItemMasterDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ItemMaster));
        private bool _isAuditLog = false;
        private const string p_ItemID = "@p_ItemID";
        public ItemMasterDao() { }
        public ItemMasterDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ItemMaster Get(Int32 ItemID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ItemID, ItemID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ItemMaster)_helper.DataRowToObject(row, new ItemMaster());
        }
        public int Insert(ItemMaster record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ItemMaster record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ItemID)
        {
            ItemMaster record;
            if (_ctx.Transaction == null)
                record = new ItemMasterDao().Get(ItemID);
            else
                record = Get(ItemID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ItemPlanning
    [Serializable]
    [Table(Name = "ItemPlanning")]
    public class ItemPlanning : DbDataModel
    {
        private Int32 _ID;
        private String _SiteID;
        private Int32 _ItemID;
        private Int32? _BusinessPartnerID;
        private Decimal _AveragePrice;
        private Byte? _LeadTime;
        private Byte? _SafetyTime;
        private Decimal _SafetyStock;
        private String _GCPurchaseUnit;
        private Decimal _MinOrderQty;
        private Decimal _MaxOrderQty;
        private Boolean _IsUsingDynamicROP;
        private Decimal _ToleranceQty;
        private Byte? _TimeFence;
        private Decimal _PurchaseUnitPrice;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage;
        private Int32? _LastBusinessPartnerID;
        private Decimal _LastPurchasePrice;
        private Decimal _LastPurchaseDiscount;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
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
        [Column(Name = "BusinessPartnerID", DataType = "Int32", IsNullable = true)]
        public Int32? BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "AveragePrice", DataType = "Decimal", IsNullable = true)]
        public Decimal AveragePrice
        {
            get { return _AveragePrice; }
            set { _AveragePrice = value; }
        }
        [Column(Name = "LeadTime", DataType = "Byte", IsNullable = true)]
        public Byte? LeadTime
        {
            get { return _LeadTime; }
            set { _LeadTime = value; }
        }
        [Column(Name = "SafetyTime", DataType = "Byte", IsNullable = true)]
        public Byte? SafetyTime
        {
            get { return _SafetyTime; }
            set { _SafetyTime = value; }
        }
        [Column(Name = "SafetyStock", DataType = "Decimal", IsNullable = true)]
        public Decimal SafetyStock
        {
            get { return _SafetyStock; }
            set { _SafetyStock = value; }
        }
        [Column(Name = "GCPurchaseUnit", DataType = "String", IsNullable = true)]
        public String GCPurchaseUnit
        {
            get { return _GCPurchaseUnit; }
            set { _GCPurchaseUnit = value; }
        }
        [Column(Name = "MinOrderQty", DataType = "Decimal", IsNullable = true)]
        public Decimal MinOrderQty
        {
            get { return _MinOrderQty; }
            set { _MinOrderQty = value; }
        }
        [Column(Name = "MaxOrderQty", DataType = "Decimal", IsNullable = true)]
        public Decimal MaxOrderQty
        {
            get { return _MaxOrderQty; }
            set { _MaxOrderQty = value; }
        }
        [Column(Name = "IsUsingDynamicROP", DataType = "Boolean")]
        public Boolean IsUsingDynamicROP
        {
            get { return _IsUsingDynamicROP; }
            set { _IsUsingDynamicROP = value; }
        }
        [Column(Name = "ToleranceQty", DataType = "Decimal", IsNullable = true)]
        public Decimal ToleranceQty
        {
            get { return _ToleranceQty; }
            set { _ToleranceQty = value; }
        }
        [Column(Name = "TimeFence", DataType = "Byte", IsNullable = true)]
        public Byte? TimeFence
        {
            get { return _TimeFence; }
            set { _TimeFence = value; }
        }
        [Column(Name = "PurchaseUnitPrice", DataType = "Decimal", IsNullable = true)]
        public Decimal PurchaseUnitPrice
        {
            get { return _PurchaseUnitPrice; }
            set { _PurchaseUnitPrice = value; }
        }
        [Column(Name = "UnitPrice", DataType = "Decimal", IsNullable = true)]
        public Decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        [Column(Name = "DiscountPercentage", DataType = "Decimal", IsNullable = true)]
        public Decimal DiscountPercentage
        {
            get { return _DiscountPercentage; }
            set { _DiscountPercentage = value; }
        }
        [Column(Name = "LastBusinessPartnerID", DataType = "Int32", IsNullable = true)]
        public Int32? LastBusinessPartnerID
        {
            get { return _LastBusinessPartnerID; }
            set { _LastBusinessPartnerID = value; }
        }
        [Column(Name = "LastPurchasePrice", DataType = "Decimal", IsNullable = true)]
        public Decimal LastPurchasePrice
        {
            get { return _LastPurchasePrice; }
            set { _LastPurchasePrice = value; }
        }
        [Column(Name = "LastPurchaseDiscount", DataType = "Decimal", IsNullable = true)]
        public Decimal LastPurchaseDiscount
        {
            get { return _LastPurchaseDiscount; }
            set { _LastPurchaseDiscount = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class ItemPlanningDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ItemPlanning));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public ItemPlanningDao() { }
        public ItemPlanningDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ItemPlanning Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ItemPlanning)_helper.DataRowToObject(row, new ItemPlanning());
        }
        public int Insert(ItemPlanning record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ItemPlanning record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            ItemPlanning record;
            if (_ctx.Transaction == null)
                record = new ItemPlanningDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ItemRequestDt
    [Serializable]
    [Table(Name = "ItemRequestDt")]
    public class ItemRequestDt : DbDataModel
    {
        private Int32 _ID;
        private Int32 _ItemRequestID;
        private Int32 _ItemID;
        private Decimal _Quantity;
        private String _GCItemUnit;
        private String _GCBaseUnit;
        private Decimal _ConversionFactor;
        private Decimal _DistributionQty;
        private Decimal _ConsumptionQty;
        private Decimal _PurchaseRequestQty;
        private String _GCItemDetailStatus;
        private Boolean _IsDeleted;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
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
        [Column(Name = "GCItemDetailStatus", DataType = "String")]
        public String GCItemDetailStatus
        {
            get { return _GCItemDetailStatus; }
            set { _GCItemDetailStatus = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class ItemRequestDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ItemRequestDt));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public ItemRequestDtDao() { }
        public ItemRequestDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ItemRequestDt Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ItemRequestDt)_helper.DataRowToObject(row, new ItemRequestDt());
        }
        public int Insert(ItemRequestDt record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ItemRequestDt record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            ItemRequestDt record;
            if (_ctx.Transaction == null)
                record = new ItemRequestDtDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ItemRequestHd
    [Serializable]
    [Table(Name = "ItemRequestHd")]
    public partial class ItemRequestHd : DbDataModel
    {
        private Int32 _ItemRequestID;
        private DateTime _TransactionDate;
        private String _TransactionTime;
        private String _ItemRequestNo;
        private Int32 _FromLocationID;
        private Int32 _ToLocationID;
        private String _Remarks;
        private String _GCTransactionStatus;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ItemRequestID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "ToLocationID", DataType = "Int32")]
        public Int32 ToLocationID
        {
            get { return _ToLocationID; }
            set { _ToLocationID = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class ItemRequestHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ItemRequestHd));
        private bool _isAuditLog = false;
        private const string p_ItemRequestID = "@p_ItemRequestID";
        public ItemRequestHdDao() { }
        public ItemRequestHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ItemRequestHd Get(Int32 ItemRequestID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ItemRequestID, ItemRequestID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ItemRequestHd)_helper.DataRowToObject(row, new ItemRequestHd());
        }
        public int Insert(ItemRequestHd record)
        {
            record.CreatedDate = record.LastUpdatedDate = DateTime.Now;
            record.LastUpdatedBy = record.CreatedBy;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ItemRequestHd record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ItemRequestID)
        {
            ItemRequestHd record;
            if (_ctx.Transaction == null)
                record = new ItemRequestHdDao().Get(ItemRequestID);
            else
                record = Get(ItemRequestID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ItemTransactionDt
    [Serializable]
    [Table(Name = "ItemTransactionDt")]
    public class ItemTransactionDt : DbDataModel
    {
        private Int32 _ID;
        private Int32 _TransactionID;
        private Int32 _ItemID;
        private Decimal _Quantity;
        private String _GCItemUnit;
        private String _GCBaseUnit;
        private Decimal _ConversionFactor;
        private Decimal _BaseQuantity;
        private Decimal _CostAmount;
        private Boolean _IsControlExpired;
        private String _GCAdjustmentReason;
        private String _Remarks;
        private String _GCItemDetailStatus;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
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
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
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
        [Column(Name = "GCAdjustmentReason", DataType = "String", IsNullable = true)]
        public String GCAdjustmentReason
        {
            get { return _GCAdjustmentReason; }
            set { _GCAdjustmentReason = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class ItemTransactionDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ItemTransactionDt));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public ItemTransactionDtDao() { }
        public ItemTransactionDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ItemTransactionDt Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ItemTransactionDt)_helper.DataRowToObject(row, new ItemTransactionDt());
        }
        public int Insert(ItemTransactionDt record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ItemTransactionDt record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            ItemTransactionDt record;
            if (_ctx.Transaction == null)
                record = new ItemTransactionDtDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ItemTransactionHd
    [Serializable]
    [Table(Name = "ItemTransactionHd")]
    public class ItemTransactionHd : DbDataModel
    {
        private Int32 _TransactionID;
        private String _TransactionCode;
        private DateTime _TransactionDate;
        private String _TransactionNo;
        private Int32 _FromLocationID;
        private Int32? _ToLocationID;
        private String _GCAdjustmentType;
        private String _GCConsumptionType;
        private Boolean _IsBySystem;
        private String _ReferenceNo;
        private String _Remarks;
        private String _GCTransactionStatus;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "TransactionID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "ToLocationID", DataType = "Int32", IsNullable = true)]
        public Int32? ToLocationID
        {
            get { return _ToLocationID; }
            set { _ToLocationID = value; }
        }
        [Column(Name = "GCAdjustmentType", DataType = "String", IsNullable = true)]
        public String GCAdjustmentType
        {
            get { return _GCAdjustmentType; }
            set { _GCAdjustmentType = value; }
        }
        [Column(Name = "GCConsumptionType", DataType = "String", IsNullable = true)]
        public String GCConsumptionType
        {
            get { return _GCConsumptionType; }
            set { _GCConsumptionType = value; }
        }
        [Column(Name = "IsBySystem", DataType = "Boolean")]
        public Boolean IsBySystem
        {
            get { return _IsBySystem; }
            set { _IsBySystem = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String", IsNullable = true)]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class ItemTransactionHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ItemTransactionHd));
        private bool _isAuditLog = false;
        private const string p_TransactionID = "@p_TransactionID";
        public ItemTransactionHdDao() { }
        public ItemTransactionHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ItemTransactionHd Get(Int32 TransactionID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_TransactionID, TransactionID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ItemTransactionHd)_helper.DataRowToObject(row, new ItemTransactionHd());
        }
        public int Insert(ItemTransactionHd record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ItemTransactionHd record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 TransactionID)
        {
            ItemTransactionHd record;
            if (_ctx.Transaction == null)
                record = new ItemTransactionHdDao().Get(TransactionID);
            else
                record = Get(TransactionID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Location
    [Serializable]
    [Table(Name = "Location")]
    public class Location : DbDataModel
    {
        private Int32 _LocationID;
        private String _SiteID;
        private String _LocationCode;
        private String _LocationName;
        private String _ShortName;
        private Int32? _ParentID;
        private Int32? _ItemGroupID;
        private Int32? _RestrictionID;
        private Boolean _IsHeader;
        private Boolean _IsAvailable;
        private Boolean _IsNettable;
        private Boolean _IsAllowOverIssued;
        private Boolean _IsHoldForTransaction;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "LocationID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "ShortName", DataType = "String")]
        public String ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }
        [Column(Name = "ParentID", DataType = "Int32", IsNullable = true)]
        public Int32? ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        [Column(Name = "ItemGroupID", DataType = "Int32", IsNullable = true)]
        public Int32? ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "RestrictionID", DataType = "Int32", IsNullable = true)]
        public Int32? RestrictionID
        {
            get { return _RestrictionID; }
            set { _RestrictionID = value; }
        }
        [Column(Name = "IsHeader", DataType = "Boolean")]
        public Boolean IsHeader
        {
            get { return _IsHeader; }
            set { _IsHeader = value; }
        }
        [Column(Name = "IsAvailable", DataType = "Boolean", IsNullable = true)]
        public Boolean IsAvailable
        {
            get { return _IsAvailable; }
            set { _IsAvailable = value; }
        }
        [Column(Name = "IsNettable", DataType = "Boolean", IsNullable = true)]
        public Boolean IsNettable
        {
            get { return _IsNettable; }
            set { _IsNettable = value; }
        }
        [Column(Name = "IsAllowOverIssued", DataType = "Boolean", IsNullable = true)]
        public Boolean IsAllowOverIssued
        {
            get { return _IsAllowOverIssued; }
            set { _IsAllowOverIssued = value; }
        }
        [Column(Name = "IsHoldForTransaction", DataType = "Boolean")]
        public Boolean IsHoldForTransaction
        {
            get { return _IsHoldForTransaction; }
            set { _IsHoldForTransaction = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class LocationDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Location));
        private bool _isAuditLog = false;
        private const string p_LocationID = "@p_LocationID";
        public LocationDao() { }
        public LocationDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Location Get(Int32 LocationID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_LocationID, LocationID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Location)_helper.DataRowToObject(row, new Location());
        }
        public int Insert(Location record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Location record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 LocationID)
        {
            Location record;
            if (_ctx.Transaction == null)
                record = new LocationDao().Get(LocationID);
            else
                record = Get(LocationID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PeriodAdmission
    [Serializable]
    [Table(Name = "PeriodAdmission")]
    public class PeriodAdmission : DbDataModel
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
        private String _Remarks;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "PeriodAdmissionID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class PeriodAdmissionDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PeriodAdmission));
        private bool _isAuditLog = false;
        private const string p_PeriodAdmissionID = "@p_PeriodAdmissionID";
        public PeriodAdmissionDao() { }
        public PeriodAdmissionDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PeriodAdmission Get(Int32 PeriodAdmissionID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_PeriodAdmissionID, PeriodAdmissionID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PeriodAdmission)_helper.DataRowToObject(row, new PeriodAdmission());
        }
        public int Insert(PeriodAdmission record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PeriodAdmission record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 PeriodAdmissionID)
        {
            PeriodAdmission record;
            if (_ctx.Transaction == null)
                record = new PeriodAdmissionDao().Get(PeriodAdmissionID);
            else
                record = Get(PeriodAdmissionID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PeriodClassType
    [Serializable]
    [Table(Name = "PeriodClassType")]
    public class PeriodClassType : DbDataModel
    {
        private Int32 _PeriodClassTypeID;
        private Int32 _SchoolPeriodID;
        private Int32? _PeriodSectionID;
        private Int32 _ClassTypeID;
        private Int32 _DailySchedulePackageID;
        private Int16 _NoOfClass;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "PeriodClassTypeID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "PeriodSectionID", DataType = "Int32", IsNullable = true)]
        public Int32? PeriodSectionID
        {
            get { return _PeriodSectionID; }
            set { _PeriodSectionID = value; }
        }
        [Column(Name = "ClassTypeID", DataType = "Int32")]
        public Int32 ClassTypeID
        {
            get { return _ClassTypeID; }
            set { _ClassTypeID = value; }
        }
        [Column(Name = "DailySchedulePackageID", DataType = "Int32")]
        public Int32 DailySchedulePackageID
        {
            get { return _DailySchedulePackageID; }
            set { _DailySchedulePackageID = value; }
        }
        [Column(Name = "NoOfClass", DataType = "Int16")]
        public Int16 NoOfClass
        {
            get { return _NoOfClass; }
            set { _NoOfClass = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class PeriodClassTypeDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PeriodClassType));
        private bool _isAuditLog = false;
        private const string p_PeriodClassTypeID = "@p_PeriodClassTypeID";
        public PeriodClassTypeDao() { }
        public PeriodClassTypeDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PeriodClassType Get(Int32 PeriodClassTypeID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_PeriodClassTypeID, PeriodClassTypeID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PeriodClassType)_helper.DataRowToObject(row, new PeriodClassType());
        }
        public int Insert(PeriodClassType record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PeriodClassType record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 PeriodClassTypeID)
        {
            PeriodClassType record;
            if (_ctx.Transaction == null)
                record = new PeriodClassTypeDao().Get(PeriodClassTypeID);
            else
                record = Get(PeriodClassTypeID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PeriodClassTypeSubject
    [Serializable]
    [Table(Name = "PeriodClassTypeSubject")]
    public class PeriodClassTypeSubject : DbDataModel
    {
        private Int32 _PeriodClassTypeSubjectID;
        private Int32 _PeriodClassTypeID;
        private Int32 _SubjectID;
        private Int32 _TeacherID;
        private Int16 _NoMeetingHoursInWeek;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "PeriodClassTypeSubjectID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "SubjectID", DataType = "Int32")]
        public Int32 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class PeriodClassTypeSubjectDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PeriodClassTypeSubject));
        private bool _isAuditLog = false;
        private const string p_PeriodClassTypeSubjectID = "@p_PeriodClassTypeSubjectID";
        public PeriodClassTypeSubjectDao() { }
        public PeriodClassTypeSubjectDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PeriodClassTypeSubject Get(Int32 PeriodClassTypeSubjectID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_PeriodClassTypeSubjectID, PeriodClassTypeSubjectID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PeriodClassTypeSubject)_helper.DataRowToObject(row, new PeriodClassTypeSubject());
        }
        public int Insert(PeriodClassTypeSubject record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PeriodClassTypeSubject record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 PeriodClassTypeSubjectID)
        {
            PeriodClassTypeSubject record;
            if (_ctx.Transaction == null)
                record = new PeriodClassTypeSubjectDao().Get(PeriodClassTypeSubjectID);
            else
                record = Get(PeriodClassTypeSubjectID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PeriodSchedule
    [Serializable]
    [Table(Name = "PeriodSchedule")]
    public class PeriodSchedule : DbDataModel
    {
        private Int32 _PeriodScheduleID;
        private String _PeriodScheduleCode;
        private String _PeriodScheduleName;
        private Int32 _SchoolPeriodID;
        private String _GCPeriodScheduleType;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "PeriodScheduleID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class PeriodScheduleDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PeriodSchedule));
        private bool _isAuditLog = false;
        private const string p_PeriodScheduleID = "@p_PeriodScheduleID";
        public PeriodScheduleDao() { }
        public PeriodScheduleDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PeriodSchedule Get(Int32 PeriodScheduleID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_PeriodScheduleID, PeriodScheduleID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PeriodSchedule)_helper.DataRowToObject(row, new PeriodSchedule());
        }
        public int Insert(PeriodSchedule record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PeriodSchedule record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 PeriodScheduleID)
        {
            PeriodSchedule record;
            if (_ctx.Transaction == null)
                record = new PeriodScheduleDao().Get(PeriodScheduleID);
            else
                record = Get(PeriodScheduleID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PeriodSection
    [Serializable]
    [Table(Name = "PeriodSection")]
    public class PeriodSection : DbDataModel
    {
        private Int32 _PeriodSectionID;
        private String _PeriodSectionCode;
        private String _PeriodSectionName;
        private Int32 _SchoolPeriodID;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private String _GCPeriodSectionStatus;
        private String _Remarks;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "PeriodSectionID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class PeriodSectionDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PeriodSection));
        private bool _isAuditLog = false;
        private const string p_PeriodSectionID = "@p_PeriodSectionID";
        public PeriodSectionDao() { }
        public PeriodSectionDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PeriodSection Get(Int32 PeriodSectionID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_PeriodSectionID, PeriodSectionID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PeriodSection)_helper.DataRowToObject(row, new PeriodSection());
        }
        public int Insert(PeriodSection record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PeriodSection record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 PeriodSectionID)
        {
            PeriodSection record;
            if (_ctx.Transaction == null)
                record = new PeriodSectionDao().Get(PeriodSectionID);
            else
                record = Get(PeriodSectionID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PurchaseOrderDt
    [Serializable]
    [Table(Name = "PurchaseOrderDt")]
    public partial class PurchaseOrderDt : DbDataModel
    {
        private Int32 _ID;
        private Int32 _PurchaseOrderID;
        private Int32 _ItemID;
        private Int32? _PurchaseRequestID;
        private Decimal _Quantity;
        private String _GCPurchaseUnit;
        private String _GCBaseUnit;
        private Decimal _ConversionFactor;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage1;
        private Decimal _DiscountPercentage2;
        private Boolean _IsBonusItem;
        private Decimal _LineAmount;
        private String _Remarks;
        private String _GCItemDetailStatus;
        private String _ReceivedInformation;
        private Decimal _ReceivedQuantity;
        private Boolean _IsDeleted;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "PurchaseRequestID", DataType = "Int32", IsNullable = true)]
        public Int32? PurchaseRequestID
        {
            get { return _PurchaseRequestID; }
            set { _PurchaseRequestID = value; }
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
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
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
        [Column(Name = "LineAmount", DataType = "Decimal")]
        public Decimal LineAmount
        {
            get { return _LineAmount; }
            set { _LineAmount = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "ReceivedInformation", DataType = "String", IsNullable = true)]
        public String ReceivedInformation
        {
            get { return _ReceivedInformation; }
            set { _ReceivedInformation = value; }
        }
        [Column(Name = "ReceivedQuantity", DataType = "Decimal", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class PurchaseOrderDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PurchaseOrderDt));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public PurchaseOrderDtDao() { }
        public PurchaseOrderDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PurchaseOrderDt Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PurchaseOrderDt)_helper.DataRowToObject(row, new PurchaseOrderDt());
        }
        public int Insert(PurchaseOrderDt record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PurchaseOrderDt record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            PurchaseOrderDt record;
            if (_ctx.Transaction == null)
                record = new PurchaseOrderDtDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PurchaseOrderHd
    [Serializable]
    [Table(Name = "PurchaseOrderHd")]
    public class PurchaseOrderHd : DbDataModel
    {
        private Int32 _PurchaseOrderID;
        private DateTime _OrderDate;
        private String _PurchaseOrderNo;
        private Int32? _LocationID;
        private DateTime _DeliveryDate;
        private DateTime _POExpiredDate;
        private String _GCPurchaseOrderType;
        private Int32? _BusinessPartnerID;
        private Int32 _TermID;
        private String _GCFrancoRegion;
        private String _GCCurrencyCode;
        private Decimal _CurrencyRate;
        private Boolean _IsIncludeVAT;
        private Decimal _TransactionAmount;
        private Decimal _FinalDiscount;
        private Decimal _VATPercentage;
        private Decimal _DownPaymentAmount;
        private String _PaymentRemarks;
        private String _Remarks;
        private String _GCTransactionStatus;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "PurchaseOrderID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 PurchaseOrderID
        {
            get { return _PurchaseOrderID; }
            set { _PurchaseOrderID = value; }
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
        [Column(Name = "LocationID", DataType = "Int32", IsNullable = true)]
        public Int32? LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
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
        [Column(Name = "BusinessPartnerID", DataType = "Int32", IsNullable = true)]
        public Int32? BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "TermID", DataType = "Int32")]
        public Int32 TermID
        {
            get { return _TermID; }
            set { _TermID = value; }
        }
        [Column(Name = "GCFrancoRegion", DataType = "String")]
        public String GCFrancoRegion
        {
            get { return _GCFrancoRegion; }
            set { _GCFrancoRegion = value; }
        }
        [Column(Name = "GCCurrencyCode", DataType = "String")]
        public String GCCurrencyCode
        {
            get { return _GCCurrencyCode; }
            set { _GCCurrencyCode = value; }
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
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class PurchaseOrderHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PurchaseOrderHd));
        private bool _isAuditLog = false;
        private const string p_PurchaseOrderID = "@p_PurchaseOrderID";
        public PurchaseOrderHdDao() { }
        public PurchaseOrderHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PurchaseOrderHd Get(Int32 PurchaseOrderID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_PurchaseOrderID, PurchaseOrderID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PurchaseOrderHd)_helper.DataRowToObject(row, new PurchaseOrderHd());
        }
        public int Insert(PurchaseOrderHd record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PurchaseOrderHd record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 PurchaseOrderID)
        {
            PurchaseOrderHd record;
            if (_ctx.Transaction == null)
                record = new PurchaseOrderHdDao().Get(PurchaseOrderID);
            else
                record = Get(PurchaseOrderID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PurchaseRequestDt
    [Serializable]
    [Table(Name = "PurchaseRequestDt")]
    public class PurchaseRequestDt : DbDataModel
    {
        private Int32 _ID;
        private Int32 _PurchaseRequestID;
        private Int32 _ItemID;
        private Decimal _Quantity;
        private String _GCPurchaseUnit;
        private String _GCBaseUnit;
        private Decimal _ConversionFactor;
        private Int32? _BusinessPartnerID;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage;
        private String _GCItemDetailStatus;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
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
        [Column(Name = "GCBaseUnit", DataType = "String")]
        public String GCBaseUnit
        {
            get { return _GCBaseUnit; }
            set { _GCBaseUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32", IsNullable = true)]
        public Int32? BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
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
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class PurchaseRequestDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PurchaseRequestDt));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public PurchaseRequestDtDao() { }
        public PurchaseRequestDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PurchaseRequestDt Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PurchaseRequestDt)_helper.DataRowToObject(row, new PurchaseRequestDt());
        }
        public int Insert(PurchaseRequestDt record)
        {
            record.CreatedDate = record.LastUpdatedDate = DateTime.Now;
            record.LastUpdatedBy = record.CreatedBy;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PurchaseRequestDt record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            PurchaseRequestDt record;
            if (_ctx.Transaction == null)
                record = new PurchaseRequestDtDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PurchaseRequestHd
    [Serializable]
    [Table(Name = "PurchaseRequestHd")]
    public partial class PurchaseRequestHd : DbDataModel
    {
        private Int32 _PurchaseRequestID;
        private DateTime _TransactionDate;
        private String _TransactionTime;
        private String _PurchaseRequestNo;
        private Int32? _ItemRequestID;
        private Int32 _FromLocationID;
        private Int32? _ToLocationID;
        private String _Remarks;
        private String _GCTransactionStatus;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "PurchaseRequestID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "ItemRequestID", DataType = "Int32", IsNullable = true)]
        public Int32? ItemRequestID
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
        [Column(Name = "ToLocationID", DataType = "Int32", IsNullable = true)]
        public Int32? ToLocationID
        {
            get { return _ToLocationID; }
            set { _ToLocationID = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class PurchaseRequestHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PurchaseRequestHd));
        private bool _isAuditLog = false;
        private const string p_PurchaseRequestID = "@p_PurchaseRequestID";
        public PurchaseRequestHdDao() { }
        public PurchaseRequestHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PurchaseRequestHd Get(Int32 PurchaseRequestID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_PurchaseRequestID, PurchaseRequestID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PurchaseRequestHd)_helper.DataRowToObject(row, new PurchaseRequestHd());
        }
        public int Insert(PurchaseRequestHd record)
        {
            record.CreatedDate = record.LastUpdatedDate = DateTime.Now;
            record.LastUpdatedBy = record.CreatedBy;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PurchaseRequestHd record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 PurchaseRequestID)
        {
            PurchaseRequestHd record;
            if (_ctx.Transaction == null)
                record = new PurchaseRequestHdDao().Get(PurchaseRequestID);
            else
                record = Get(PurchaseRequestID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PurchaseRequestPO
    [Serializable]
    [Table(Name = "PurchaseRequestPO")]
    public class PurchaseRequestPO : DbDataModel
    {
        private Int32 _ID;
        private Int32 _PurchaseRequestID;
        private Int32 _ItemID;
        private Int32 _PurchaseOrderID;
        private Decimal _OrderQuantity;
        private Decimal _ReceivedQuantity;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
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

    public class PurchaseRequestPODao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PurchaseRequestPO));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public PurchaseRequestPODao() { }
        public PurchaseRequestPODao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PurchaseRequestPO Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PurchaseRequestPO)_helper.DataRowToObject(row, new PurchaseRequestPO());
        }
        public int Insert(PurchaseRequestPO record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PurchaseRequestPO record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            PurchaseRequestPO record;
            if (_ctx.Transaction == null)
                record = new PurchaseRequestPODao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region RestrictionDt
    [Serializable]
    [Table(Name = "RestrictionDt")]
    public class RestrictionDt : DbDataModel
    {
        private Int32 _RestrictionID;
        private String _TransactionCode;

        [Column(Name = "RestrictionID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 RestrictionID
        {
            get { return _RestrictionID; }
            set { _RestrictionID = value; }
        }
        [Column(Name = "TransactionCode", DataType = "String", IsPrimaryKey = true)]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
    }

    public class RestrictionDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(RestrictionDt));
        private bool _isAuditLog = false;
        private const string p_RestrictionID = "@p_RestrictionID";
        private const string p_TransactionCode = "@p_TransactionCode";
        public RestrictionDtDao() { }
        public RestrictionDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public RestrictionDt Get(Int32 RestrictionID, String TransactionCode)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_RestrictionID, RestrictionID);
            _ctx.Add(p_TransactionCode, TransactionCode);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (RestrictionDt)_helper.DataRowToObject(row, new RestrictionDt());
        }
        public int Insert(RestrictionDt record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(RestrictionDt record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 RestrictionID, String TransactionCode)
        {
            RestrictionDt record;
            if (_ctx.Transaction == null)
                record = new RestrictionDtDao().Get(RestrictionID, TransactionCode);
            else
                record = Get(RestrictionID, TransactionCode);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region RestrictionHd
    [Serializable]
    [Table(Name = "RestrictionHd")]
    public class RestrictionHd : DbDataModel
    {
        private Int32 _RestrictionID;
        private String _RestrictionCode;
        private String _RestrictionName;
        private String _GCRestrictionType;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "RestrictionID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 RestrictionID
        {
            get { return _RestrictionID; }
            set { _RestrictionID = value; }
        }
        [Column(Name = "RestrictionCode", DataType = "String")]
        public String RestrictionCode
        {
            get { return _RestrictionCode; }
            set { _RestrictionCode = value; }
        }
        [Column(Name = "RestrictionName", DataType = "String")]
        public String RestrictionName
        {
            get { return _RestrictionName; }
            set { _RestrictionName = value; }
        }
        [Column(Name = "GCRestrictionType", DataType = "String")]
        public String GCRestrictionType
        {
            get { return _GCRestrictionType; }
            set { _GCRestrictionType = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class RestrictionHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(RestrictionHd));
        private bool _isAuditLog = false;
        private const string p_RestrictionID = "@p_RestrictionID";
        public RestrictionHdDao() { }
        public RestrictionHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public RestrictionHd Get(Int32 RestrictionID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_RestrictionID, RestrictionID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (RestrictionHd)_helper.DataRowToObject(row, new RestrictionHd());
        }
        public int Insert(RestrictionHd record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(RestrictionHd record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 RestrictionID)
        {
            RestrictionHd record;
            if (_ctx.Transaction == null)
                record = new RestrictionHdDao().Get(RestrictionID);
            else
                record = Get(RestrictionID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Room
    [Serializable]
    [Table(Name = "Room")]
    public class Room : DbDataModel
    {
        private Int32 _RoomID;
        private String _RoomCode;
        private String _RoomName;
        private String _SiteID;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "RoomID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 RoomID
        {
            get { return _RoomID; }
            set { _RoomID = value; }
        }
        [Column(Name = "RoomCode", DataType = "String")]
        public String RoomCode
        {
            get { return _RoomCode; }
            set { _RoomCode = value; }
        }
        [Column(Name = "RoomName", DataType = "String")]
        public String RoomName
        {
            get { return _RoomName; }
            set { _RoomName = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class RoomDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Room));
        private bool _isAuditLog = false;
        private const string p_RoomID = "@p_RoomID";
        public RoomDao() { }
        public RoomDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Room Get(Int32 RoomID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_RoomID, RoomID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Room)_helper.DataRowToObject(row, new Room());
        }
        public int Insert(Room record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Room record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 RoomID)
        {
            Room record;
            if (_ctx.Transaction == null)
                record = new RoomDao().Get(RoomID);
            else
                record = Get(RoomID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region SchoolClass
    [Serializable]
    [Table(Name = "SchoolClass")]
    public class SchoolClass : DbDataModel
    {
        private Int32 _SchoolClassID;
        private String _SchoolClassCode;
        private String _SchoolClassName;
        private Int32 _PeriodClassTypeID;
        private Int32 _RoomID;
        private Int32 _TeacherID;
        private Int16 _MaxStudent;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "SchoolClassID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "PeriodClassTypeID", DataType = "Int32")]
        public Int32 PeriodClassTypeID
        {
            get { return _PeriodClassTypeID; }
            set { _PeriodClassTypeID = value; }
        }
        [Column(Name = "RoomID", DataType = "Int32")]
        public Int32 RoomID
        {
            get { return _RoomID; }
            set { _RoomID = value; }
        }
        [Column(Name = "TeacherID", DataType = "Int32")]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class SchoolClassDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(SchoolClass));
        private bool _isAuditLog = false;
        private const string p_SchoolClassID = "@p_SchoolClassID";
        public SchoolClassDao() { }
        public SchoolClassDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public SchoolClass Get(Int32 SchoolClassID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_SchoolClassID, SchoolClassID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (SchoolClass)_helper.DataRowToObject(row, new SchoolClass());
        }
        public int Insert(SchoolClass record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(SchoolClass record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 SchoolClassID)
        {
            SchoolClass record;
            if (_ctx.Transaction == null)
                record = new SchoolClassDao().Get(SchoolClassID);
            else
                record = Get(SchoolClassID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region SchoolPeriod
    [Serializable]
    [Table(Name = "SchoolPeriod")]
    public class SchoolPeriod : DbDataModel
    {
        private Int32 _SchoolPeriodID;
        private String _SchoolPeriodCode;
        private String _SchoolPeriodName;
        private String _SiteID;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private Int32 _DailySchedulePackageID;
        private String _GCSchoolPeriodStatus;
        private String _Remarks;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "SchoolPeriodID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "SchoolPeriodCode", DataType = "String")]
        public String SchoolPeriodCode
        {
            get { return _SchoolPeriodCode; }
            set { _SchoolPeriodCode = value; }
        }
        [Column(Name = "SchoolPeriodName", DataType = "String")]
        public String SchoolPeriodName
        {
            get { return _SchoolPeriodName; }
            set { _SchoolPeriodName = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
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
        [Column(Name = "DailySchedulePackageID", DataType = "Int32")]
        public Int32 DailySchedulePackageID
        {
            get { return _DailySchedulePackageID; }
            set { _DailySchedulePackageID = value; }
        }
        [Column(Name = "GCSchoolPeriodStatus", DataType = "String")]
        public String GCSchoolPeriodStatus
        {
            get { return _GCSchoolPeriodStatus; }
            set { _GCSchoolPeriodStatus = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class SchoolPeriodDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(SchoolPeriod));
        private bool _isAuditLog = false;
        private const string p_SchoolPeriodID = "@p_SchoolPeriodID";
        public SchoolPeriodDao() { }
        public SchoolPeriodDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public SchoolPeriod Get(Int32 SchoolPeriodID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_SchoolPeriodID, SchoolPeriodID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (SchoolPeriod)_helper.DataRowToObject(row, new SchoolPeriod());
        }
        public int Insert(SchoolPeriod record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(SchoolPeriod record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 SchoolPeriodID)
        {
            SchoolPeriod record;
            if (_ctx.Transaction == null)
                record = new SchoolPeriodDao().Get(SchoolPeriodID);
            else
                record = Get(SchoolPeriodID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region SiteParameter
    [Serializable]
    [Table(Name = "SiteParameter")]
    public class SiteParameter : DbDataModel
    {
        private String _SiteID;
        private String _ParameterCode;
        private String _ModuleID;
        private String _ParameterName;
        private String _GCParameterValueType;
        private String _TableName;
        private String _FilterExpression;
        private String _ValueField;
        private String _TextField;
        private String _SearchDialogType;
        private String _SearchDialogMethodName;
        private String _SearchDialogFilterExpression;
        private String _SearchDialogIDField;
        private String _SearchDialogCodeField;
        private String _SearchDialogNameField;
        private String _ParameterValue;
        private String _Notes;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "SiteID", DataType = "String", IsPrimaryKey = true)]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "ParameterCode", DataType = "String", IsPrimaryKey = true)]
        public String ParameterCode
        {
            get { return _ParameterCode; }
            set { _ParameterCode = value; }
        }
        [Column(Name = "ModuleID", DataType = "String")]
        public String ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }
        [Column(Name = "ParameterName", DataType = "String")]
        public String ParameterName
        {
            get { return _ParameterName; }
            set { _ParameterName = value; }
        }
        [Column(Name = "GCParameterValueType", DataType = "String")]
        public String GCParameterValueType
        {
            get { return _GCParameterValueType; }
            set { _GCParameterValueType = value; }
        }
        [Column(Name = "TableName", DataType = "String", IsNullable = true)]
        public String TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        [Column(Name = "FilterExpression", DataType = "String", IsNullable = true)]
        public String FilterExpression
        {
            get { return _FilterExpression; }
            set { _FilterExpression = value; }
        }
        [Column(Name = "ValueField", DataType = "String", IsNullable = true)]
        public String ValueField
        {
            get { return _ValueField; }
            set { _ValueField = value; }
        }
        [Column(Name = "TextField", DataType = "String", IsNullable = true)]
        public String TextField
        {
            get { return _TextField; }
            set { _TextField = value; }
        }
        [Column(Name = "SearchDialogType", DataType = "String", IsNullable = true)]
        public String SearchDialogType
        {
            get { return _SearchDialogType; }
            set { _SearchDialogType = value; }
        }
        [Column(Name = "SearchDialogMethodName", DataType = "String", IsNullable = true)]
        public String SearchDialogMethodName
        {
            get { return _SearchDialogMethodName; }
            set { _SearchDialogMethodName = value; }
        }
        [Column(Name = "SearchDialogFilterExpression", DataType = "String", IsNullable = true)]
        public String SearchDialogFilterExpression
        {
            get { return _SearchDialogFilterExpression; }
            set { _SearchDialogFilterExpression = value; }
        }
        [Column(Name = "SearchDialogIDField", DataType = "String", IsNullable = true)]
        public String SearchDialogIDField
        {
            get { return _SearchDialogIDField; }
            set { _SearchDialogIDField = value; }
        }
        [Column(Name = "SearchDialogCodeField", DataType = "String", IsNullable = true)]
        public String SearchDialogCodeField
        {
            get { return _SearchDialogCodeField; }
            set { _SearchDialogCodeField = value; }
        }
        [Column(Name = "SearchDialogNameField", DataType = "String", IsNullable = true)]
        public String SearchDialogNameField
        {
            get { return _SearchDialogNameField; }
            set { _SearchDialogNameField = value; }
        }
        [Column(Name = "ParameterValue", DataType = "String", IsNullable = true)]
        public String ParameterValue
        {
            get { return _ParameterValue; }
            set { _ParameterValue = value; }
        }
        [Column(Name = "Notes", DataType = "String", IsNullable = true)]
        public String Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class SiteParameterDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(SiteParameter));
        private bool _isAuditLog = false;
        private const string p_SiteID = "@p_SiteID";
        private const string p_ParameterCode = "@p_ParameterCode";
        public SiteParameterDao() { }
        public SiteParameterDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public SiteParameter Get(String SiteID, String ParameterCode)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_SiteID, SiteID);
            _ctx.Add(p_ParameterCode, ParameterCode);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (SiteParameter)_helper.DataRowToObject(row, new SiteParameter());
        }
        public int Insert(SiteParameter record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(SiteParameter record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(String SiteID, String ParameterCode)
        {
            SiteParameter record;
            if (_ctx.Transaction == null)
                record = new SiteParameterDao().Get(SiteID, ParameterCode);
            else
                record = Get(SiteID, ParameterCode);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Student
    [Serializable]
    [Table(Name = "Student")]
    public class Student : DbDataModel
    {
        private Int32 _StudentID;
        private String _StudentCode;
        private String _SiteID;
        private String _GCSalutation;
        private String _GCStudentStatus;
        private String _GCTitle;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _StudentName;
        private String _Name;
        private String _GCSuffix;
        private String _PreferredName;
        private String _CityOfBirth;
        private DateTime _DateOfBirth;
        private String _GCGender;
        private String _GCNationality;
        private String _GCGrade;
        private String _GCMajor;
        private Int32? _SchoolClassID;
        private Int32 _AddressID;
        private String _EmailAddress1;
        private String _EmailAddress2;
        private String _MobilePhoneNo1;
        private String _MobilePhoneNo2;
        private String _PictureFileName;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "StudentID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "GCSalutation", DataType = "String", IsNullable = true)]
        public String GCSalutation
        {
            get { return _GCSalutation; }
            set { _GCSalutation = value; }
        }
        [Column(Name = "GCStudentStatus", DataType = "String")]
        public String GCStudentStatus
        {
            get { return _GCStudentStatus; }
            set { _GCStudentStatus = value; }
        }
        [Column(Name = "GCTitle", DataType = "String", IsNullable = true)]
        public String GCTitle
        {
            get { return _GCTitle; }
            set { _GCTitle = value; }
        }
        [Column(Name = "FirstName", DataType = "String", IsNullable = true)]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String", IsNullable = true)]
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
        [Column(Name = "StudentName", DataType = "String", IsNullable = true)]
        public String StudentName
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }
        [Column(Name = "Name", DataType = "String", IsNullable = true)]
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        [Column(Name = "GCSuffix", DataType = "String", IsNullable = true)]
        public String GCSuffix
        {
            get { return _GCSuffix; }
            set { _GCSuffix = value; }
        }
        [Column(Name = "PreferredName", DataType = "String")]
        public String PreferredName
        {
            get { return _PreferredName; }
            set { _PreferredName = value; }
        }
        [Column(Name = "CityOfBirth", DataType = "String", IsNullable = true)]
        public String CityOfBirth
        {
            get { return _CityOfBirth; }
            set { _CityOfBirth = value; }
        }
        [Column(Name = "DateOfBirth", DataType = "DateTime", IsNullable = true)]
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
        [Column(Name = "GCNationality", DataType = "String", IsNullable = true)]
        public String GCNationality
        {
            get { return _GCNationality; }
            set { _GCNationality = value; }
        }
        [Column(Name = "GCGrade", DataType = "String", IsNullable = true)]
        public String GCGrade
        {
            get { return _GCGrade; }
            set { _GCGrade = value; }
        }
        [Column(Name = "GCMajor", DataType = "String", IsNullable = true)]
        public String GCMajor
        {
            get { return _GCMajor; }
            set { _GCMajor = value; }
        }
        [Column(Name = "SchoolClassID", DataType = "Int32", IsNullable = true)]
        public Int32? SchoolClassID
        {
            get { return _SchoolClassID; }
            set { _SchoolClassID = value; }
        }
        [Column(Name = "AddressID", DataType = "Int32")]
        public Int32 AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
        }
        [Column(Name = "EmailAddress1", DataType = "String", IsNullable = true)]
        public String EmailAddress1
        {
            get { return _EmailAddress1; }
            set { _EmailAddress1 = value; }
        }
        [Column(Name = "EmailAddress2", DataType = "String", IsNullable = true)]
        public String EmailAddress2
        {
            get { return _EmailAddress2; }
            set { _EmailAddress2 = value; }
        }
        [Column(Name = "MobilePhoneNo1", DataType = "String", IsNullable = true)]
        public String MobilePhoneNo1
        {
            get { return _MobilePhoneNo1; }
            set { _MobilePhoneNo1 = value; }
        }
        [Column(Name = "MobilePhoneNo2", DataType = "String", IsNullable = true)]
        public String MobilePhoneNo2
        {
            get { return _MobilePhoneNo2; }
            set { _MobilePhoneNo2 = value; }
        }
        [Column(Name = "PictureFileName", DataType = "String", IsNullable = true)]
        public String PictureFileName
        {
            get { return _PictureFileName; }
            set { _PictureFileName = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class StudentDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Student));
        private bool _isAuditLog = false;
        private const string p_StudentID = "@p_StudentID";
        public StudentDao() { }
        public StudentDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Student Get(Int32 StudentID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_StudentID, StudentID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Student)_helper.DataRowToObject(row, new Student());
        }
        public int Insert(Student record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Student record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 StudentID)
        {
            Student record;
            if (_ctx.Transaction == null)
                record = new StudentDao().Get(StudentID);
            else
                record = Get(StudentID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Subject
    [Serializable]
    [Table(Name = "Subject")]
    public class Subject : DbDataModel
    {
        private Int32 _SubjectID;
        private String _SubjectCode;
        private String _SubjectName;
        private String _SiteID;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "SubjectID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class SubjectDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Subject));
        private bool _isAuditLog = false;
        private const string p_SubjectID = "@p_SubjectID";
        public SubjectDao() { }
        public SubjectDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Subject Get(Int32 SubjectID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_SubjectID, SubjectID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Subject)_helper.DataRowToObject(row, new Subject());
        }
        public int Insert(Subject record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Subject record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 SubjectID)
        {
            Subject record;
            if (_ctx.Transaction == null)
                record = new SubjectDao().Get(SubjectID);
            else
                record = Get(SubjectID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region SubjectGradeMajor
    [Serializable]
    [Table(Name = "SubjectGradeMajor")]
    public class SubjectGradeMajor : DbDataModel
    {
        private Int32 _SubjectID;
        private String _GCGrade;
        private String _GCMajor;

        [Column(Name = "SubjectID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
        [Column(Name = "GCGrade", DataType = "String", IsPrimaryKey = true)]
        public String GCGrade
        {
            get { return _GCGrade; }
            set { _GCGrade = value; }
        }
        [Column(Name = "GCMajor", DataType = "String", IsNullable = true)]
        public String GCMajor
        {
            get { return _GCMajor; }
            set { _GCMajor = value; }
        }
    }

    public class SubjectGradeMajorDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(SubjectGradeMajor));
        private bool _isAuditLog = false;
        private const string p_GCGrade = "@p_GCGrade";
        private const string p_SubjectID = "@p_SubjectID";
        public SubjectGradeMajorDao() { }
        public SubjectGradeMajorDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public SubjectGradeMajor Get(Int32 SubjectID, String GCGrade)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_GCGrade, GCGrade);
            _ctx.Add(p_SubjectID, SubjectID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (SubjectGradeMajor)_helper.DataRowToObject(row, new SubjectGradeMajor());
        }
        public int Insert(SubjectGradeMajor record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(SubjectGradeMajor record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 SubjectID, String GCGrade)
        {
            SubjectGradeMajor record;
            if (_ctx.Transaction == null)
                record = new SubjectGradeMajorDao().Get(SubjectID, GCGrade);
            else
                record = Get(SubjectID, GCGrade);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Teacher
    [Serializable]
    [Table(Name = "Teacher")]
    public class Teacher : DbDataModel
    {
        private Int32 _TeacherID;
        private String _TeacherCode;
        private String _GCSalutation;
        private String _GCTitle;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _GCSuffix;
        private String _TeacherName;
        private String _PreferredName;
        private String _SiteID;
        private Int32? _RoomID;
        private String _EmailAddress;
        private String _MobilePhone1;
        private String _MobilePhone2;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "TeacherID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "GCSalutation", DataType = "String", IsNullable = true)]
        public String GCSalutation
        {
            get { return _GCSalutation; }
            set { _GCSalutation = value; }
        }
        [Column(Name = "GCTitle", DataType = "String", IsNullable = true)]
        public String GCTitle
        {
            get { return _GCTitle; }
            set { _GCTitle = value; }
        }
        [Column(Name = "FirstName", DataType = "String", IsNullable = true)]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String", IsNullable = true)]
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
        [Column(Name = "GCSuffix", DataType = "String", IsNullable = true)]
        public String GCSuffix
        {
            get { return _GCSuffix; }
            set { _GCSuffix = value; }
        }
        [Column(Name = "TeacherName", DataType = "String")]
        public String TeacherName
        {
            get { return _TeacherName; }
            set { _TeacherName = value; }
        }
        [Column(Name = "PreferredName", DataType = "String", IsNullable = true)]
        public String PreferredName
        {
            get { return _PreferredName; }
            set { _PreferredName = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "RoomID", DataType = "Int32", IsNullable = true)]
        public Int32? RoomID
        {
            get { return _RoomID; }
            set { _RoomID = value; }
        }
        [Column(Name = "EmailAddress", DataType = "String", IsNullable = true)]
        public String EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        [Column(Name = "MobilePhone1", DataType = "String", IsNullable = true)]
        public String MobilePhone1
        {
            get { return _MobilePhone1; }
            set { _MobilePhone1 = value; }
        }
        [Column(Name = "MobilePhone2", DataType = "String", IsNullable = true)]
        public String MobilePhone2
        {
            get { return _MobilePhone2; }
            set { _MobilePhone2 = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class TeacherDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Teacher));
        private bool _isAuditLog = false;
        private const string p_TeacherID = "@p_TeacherID";
        public TeacherDao() { }
        public TeacherDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Teacher Get(Int32 TeacherID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_TeacherID, TeacherID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Teacher)_helper.DataRowToObject(row, new Teacher());
        }
        public int Insert(Teacher record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Teacher record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 TeacherID)
        {
            Teacher record;
            if (_ctx.Transaction == null)
                record = new TeacherDao().Get(TeacherID);
            else
                record = Get(TeacherID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Term
    [Serializable]
    [Table(Name = "Term")]
    public class Term : DbDataModel
    {
        private Int32 _TermID;
        private String _TermCode;
        private String _TermName;
        private Int16 _TermDay;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "TermID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 TermID
        {
            get { return _TermID; }
            set { _TermID = value; }
        }
        [Column(Name = "TermCode", DataType = "String")]
        public String TermCode
        {
            get { return _TermCode; }
            set { _TermCode = value; }
        }
        [Column(Name = "TermName", DataType = "String")]
        public String TermName
        {
            get { return _TermName; }
            set { _TermName = value; }
        }
        [Column(Name = "TermDay", DataType = "Int16")]
        public Int16 TermDay
        {
            get { return _TermDay; }
            set { _TermDay = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class TermDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Term));
        private bool _isAuditLog = false;
        private const string p_TermID = "@p_TermID";
        public TermDao() { }
        public TermDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Term Get(Int32 TermID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_TermID, TermID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Term)_helper.DataRowToObject(row, new Term());
        }
        public int Insert(Term record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Term record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 TermID)
        {
            Term record;
            if (_ctx.Transaction == null)
                record = new TermDao().Get(TermID);
            else
                record = Get(TermID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
}
