using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeX.Data.Core.Dal;
using System.Data;

namespace CodeX.Data.Model
{
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
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(DailyScheduleTypeDt record)
        {
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
    #region Student
    [Serializable]
    [Table(Name = "Student")]
    public class Student : DbDataModel
    {
        private Int32 _StudentID;
        private String _StudentCode;
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
}
