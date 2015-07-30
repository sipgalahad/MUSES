using System;
using System.Data;
using CodeX.Data.Core.Dal;
namespace CodeX.Data.Model
{
    #region Standard Tables
    #region Address
    [Serializable]
    [Table(Name = "Address")]
    public class Address : DbDataModel
    {
        private String _AddressID;
        private String _GCAddressType;
        private String _StreetName;
        private String _District;
        private String _City;
        private String _County;
        private String _GCState;
        private Int32? _ZipCode;
        private String _PhoneNo1;
        private String _PhoneNo2;
        private String _FaxNo1;
        private String _FaxNo2;
        private Boolean _IsMailingAddress;

        [Column(Name = "AddressID", DataType = "String", IsPrimaryKey = true)]
        public String AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
        }
        [Column(Name = "GCAddressType", DataType = "String")]
        public String GCAddressType
        {
            get { return _GCAddressType; }
            set { _GCAddressType = value; }
        }
        [Column(Name = "StreetName", DataType = "String")]
        public String StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }
        [Column(Name = "District", DataType = "String", IsNullable = true)]
        public String District
        {
            get { return _District; }
            set { _District = value; }
        }
        [Column(Name = "City", DataType = "String", IsNullable = true)]
        public String City
        {
            get { return _City; }
            set { _City = value; }
        }
        [Column(Name = "County", DataType = "String", IsNullable = true)]
        public String County
        {
            get { return _County; }
            set { _County = value; }
        }
        [Column(Name = "GCState", DataType = "String", IsNullable = true)]
        public String GCState
        {
            get { return _GCState; }
            set { _GCState = value; }
        }
        [Column(Name = "ZipCode", DataType = "Int32", IsNullable = true)]
        public Int32? ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }
        [Column(Name = "PhoneNo1", DataType = "String", IsNullable = true)]
        public String PhoneNo1
        {
            get { return _PhoneNo1; }
            set { _PhoneNo1 = value; }
        }
        [Column(Name = "PhoneNo2", DataType = "String", IsNullable = true)]
        public String PhoneNo2
        {
            get { return _PhoneNo2; }
            set { _PhoneNo2 = value; }
        }
        [Column(Name = "FaxNo1", DataType = "String", IsNullable = true)]
        public String FaxNo1
        {
            get { return _FaxNo1; }
            set { _FaxNo1 = value; }
        }
        [Column(Name = "FaxNo2", DataType = "String", IsNullable = true)]
        public String FaxNo2
        {
            get { return _FaxNo2; }
            set { _FaxNo2 = value; }
        }
        [Column(Name = "IsMailingAddress", DataType = "Boolean")]
        public Boolean IsMailingAddress
        {
            get { return _IsMailingAddress; }
            set { _IsMailingAddress = value; }
        }
    }

    public class AddressDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Address));
        private bool _isAuditLog = false;
        private const string p_AddressID = "@p_AddressID";
        public AddressDao() { }
        public AddressDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Address Get(String AddressID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_AddressID, AddressID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Address)_helper.DataRowToObject(row, new Address());
        }
        public int Insert(Address record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Address record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(String AddressID)
        {
            Address record;
            if (_ctx.Transaction == null)
                record = new AddressDao().Get(AddressID);
            else
                record = Get(AddressID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region DBSyncInfoDt
    [Serializable]
    [Table(Name = "DBSyncInfoDt")]
    public class DBSyncInfoDt : DbDataModel
    {
        private Int32 _DBSyncInfoID;
        private String _SiteID;
        private DateTime _LastSyncDate;

        [Column(Name = "DBSyncInfoID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 DBSyncInfoID
        {
            get { return _DBSyncInfoID; }
            set { _DBSyncInfoID = value; }
        }
        [Column(Name = "SiteID", DataType = "String", IsPrimaryKey = true)]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "LastSyncDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastSyncDate
        {
            get { return _LastSyncDate; }
            set { _LastSyncDate = value; }
        }
    }

    public class DBSyncInfoDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(DBSyncInfoDt));
        private bool _isAuditLog = false;
        private const string p_DBSyncInfoID = "@p_DBSyncInfoID";
        private const string p_SiteID = "@p_SiteID";
        public DBSyncInfoDtDao() { }
        public DBSyncInfoDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public DBSyncInfoDt Get(Int32 DBSyncInfoID, String SiteID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_DBSyncInfoID, DBSyncInfoID);
            _ctx.Add(p_SiteID, SiteID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (DBSyncInfoDt)_helper.DataRowToObject(row, new DBSyncInfoDt());
        }
        public int Insert(DBSyncInfoDt record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(DBSyncInfoDt record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 DBSyncInfoID, String SiteID)
        {
            DBSyncInfoDt record;
            if (_ctx.Transaction == null)
                record = new DBSyncInfoDtDao().Get(DBSyncInfoID, SiteID);
            else
                record = Get(DBSyncInfoID, SiteID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region DBSyncInfoHd
    [Serializable]
    [Table(Name = "DBSyncInfoHd")]
    public class DBSyncInfoHd : DbDataModel
    {
        private Int32 _DBSyncInfoID;
        private String _DBSyncInfoCode;
        private String _DBSyncInfoName;
        private String _ModuleID;
        private Int32 _RowCount;
        private Boolean _IsDeleted;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "DBSyncInfoID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 DBSyncInfoID
        {
            get { return _DBSyncInfoID; }
            set { _DBSyncInfoID = value; }
        }
        [Column(Name = "DBSyncInfoCode", DataType = "String")]
        public String DBSyncInfoCode
        {
            get { return _DBSyncInfoCode; }
            set { _DBSyncInfoCode = value; }
        }
        [Column(Name = "DBSyncInfoName", DataType = "String")]
        public String DBSyncInfoName
        {
            get { return _DBSyncInfoName; }
            set { _DBSyncInfoName = value; }
        }
        [Column(Name = "ModuleID", DataType = "String")]
        public String ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }
        [Column(Name = "RowCount", DataType = "Int32")]
        public Int32 RowCount
        {
            get { return _RowCount; }
            set { _RowCount = value; }
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

    public class DBSyncInfoHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(DBSyncInfoHd));
        private bool _isAuditLog = false;
        private const string p_DBSyncInfoID = "@p_DBSyncInfoID";
        public DBSyncInfoHdDao() { }
        public DBSyncInfoHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public DBSyncInfoHd Get(Int32 DBSyncInfoID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_DBSyncInfoID, DBSyncInfoID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (DBSyncInfoHd)_helper.DataRowToObject(row, new DBSyncInfoHd());
        }
        public int Insert(DBSyncInfoHd record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(DBSyncInfoHd record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 DBSyncInfoID)
        {
            DBSyncInfoHd record;
            if (_ctx.Transaction == null)
                record = new DBSyncInfoHdDao().Get(DBSyncInfoID);
            else
                record = Get(DBSyncInfoID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region LocationUser
    [Serializable]
    [Table(Name = "LocationUser")]
    public class LocationUser : DbDataModel
    {
        private Int32 _ID;
        private Int32 _LocationID;
        private Int32 _UserID;
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
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "UserID", DataType = "Int32")]
        public Int32 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
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

    public class LocationUserDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(LocationUser));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public LocationUserDao() { }
        public LocationUserDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public LocationUser Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (LocationUser)_helper.DataRowToObject(row, new LocationUser());
        }
        public int Insert(LocationUser record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(LocationUser record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            LocationUser record;
            if (_ctx.Transaction == null)
                record = new LocationUserDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region LocationUserRole
    [Serializable]
    [Table(Name = "LocationUserRole")]
    public class LocationUserRole : DbDataModel
    {
        private Int32 _ID;
        private Int32 _LocationID;
        private Int32 _RoleID;
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
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        [Column(Name = "RoleID", DataType = "Int32")]
        public Int32 RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
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

    public class LocationUserRoleDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(LocationUserRole));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public LocationUserRoleDao() { }
        public LocationUserRoleDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public LocationUserRole Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (LocationUserRole)_helper.DataRowToObject(row, new LocationUserRole());
        }
        public int Insert(LocationUserRole record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(LocationUserRole record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            LocationUserRole record;
            if (_ctx.Transaction == null)
                record = new LocationUserRoleDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region LoginAttribute
    [Serializable]
    [Table(Name = "LoginAttribute")]
    public class LoginAttribute : DbDataModel
    {
        private Int32 _LoginAttributeID;
        private String _LoginAttributeCode;
        private String _LoginAttributeName;
        private String _LoginAttributeCaption;
        private String _SessionName;
        private String _MethodName;
        private String _FilterExpression;
        private String _ValueFieldName;
        private String _TextFieldName;
        private String _DefaultValue;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "LoginAttributeID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 LoginAttributeID
        {
            get { return _LoginAttributeID; }
            set { _LoginAttributeID = value; }
        }
        [Column(Name = "LoginAttributeCode", DataType = "String")]
        public String LoginAttributeCode
        {
            get { return _LoginAttributeCode; }
            set { _LoginAttributeCode = value; }
        }
        [Column(Name = "LoginAttributeName", DataType = "String")]
        public String LoginAttributeName
        {
            get { return _LoginAttributeName; }
            set { _LoginAttributeName = value; }
        }
        [Column(Name = "LoginAttributeCaption", DataType = "String")]
        public String LoginAttributeCaption
        {
            get { return _LoginAttributeCaption; }
            set { _LoginAttributeCaption = value; }
        }
        [Column(Name = "SessionName", DataType = "String")]
        public String SessionName
        {
            get { return _SessionName; }
            set { _SessionName = value; }
        }
        [Column(Name = "MethodName", DataType = "String")]
        public String MethodName
        {
            get { return _MethodName; }
            set { _MethodName = value; }
        }
        [Column(Name = "FilterExpression", DataType = "String", IsNullable = true)]
        public String FilterExpression
        {
            get { return _FilterExpression; }
            set { _FilterExpression = value; }
        }
        [Column(Name = "ValueFieldName", DataType = "String", IsNullable = true)]
        public String ValueFieldName
        {
            get { return _ValueFieldName; }
            set { _ValueFieldName = value; }
        }
        [Column(Name = "TextFieldName", DataType = "String", IsNullable = true)]
        public String TextFieldName
        {
            get { return _TextFieldName; }
            set { _TextFieldName = value; }
        }
        [Column(Name = "DefaultValue", DataType = "String", IsNullable = true)]
        public String DefaultValue
        {
            get { return _DefaultValue; }
            set { _DefaultValue = value; }
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

    public class LoginAttributeDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(LoginAttribute));
        private bool _isAuditLog = false;
        private const string p_LoginAttributeID = "@p_LoginAttributeID";
        public LoginAttributeDao() { }
        public LoginAttributeDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public LoginAttribute Get(Int32 LoginAttributeID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_LoginAttributeID, LoginAttributeID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (LoginAttribute)_helper.DataRowToObject(row, new LoginAttribute());
        }
        public int Insert(LoginAttribute record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(LoginAttribute record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 LoginAttributeID)
        {
            LoginAttribute record;
            if (_ctx.Transaction == null)
                record = new LoginAttributeDao().Get(LoginAttributeID);
            else
                record = Get(LoginAttributeID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region MenuClientType
    [Serializable]
    [Table(Name = "MenuClientType")]
    public class MenuClientType : DbDataModel
    {
        private Int32 _MenuID;
        private String _GCClientType;

        [Column(Name = "MenuID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 MenuID
        {
            get { return _MenuID; }
            set { _MenuID = value; }
        }
        [Column(Name = "GCClientType", DataType = "String", IsPrimaryKey = true)]
        public String GCClientType
        {
            get { return _GCClientType; }
            set { _GCClientType = value; }
        }
    }

    public class MenuClientTypeDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(MenuClientType));
        private bool _isAuditLog = false;
        private const string p_GCClientType = "@p_GCClientType";
        private const string p_MenuID = "@p_MenuID";
        public MenuClientTypeDao() { }
        public MenuClientTypeDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public MenuClientType Get(Int32 MenuID, String GCClientType)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_GCClientType, GCClientType);
            _ctx.Add(p_MenuID, MenuID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (MenuClientType)_helper.DataRowToObject(row, new MenuClientType());
        }
        public int Insert(MenuClientType record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(MenuClientType record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 MenuID, String GCClientType)
        {
            MenuClientType record;
            if (_ctx.Transaction == null)
                record = new MenuClientTypeDao().Get(MenuID, GCClientType);
            else
                record = Get(MenuID, GCClientType);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region MenuMaster
    [Serializable]
    [Table(Name = "Menu")]
    public class MenuMaster : DbDataModel
    {
        private Int32 _MenuID;
        private String _MenuCode;
        private String _ModuleID;
        private String _MenuCaption;
        private String _MenuUrl;
        private Int16 _MenuLevel;
        private Int16 _MenuIndex;
        private String _MenuTooltip;
        private Int32? _ParentID;
        private String _CRUDMode;
        private String _ImageUrl;
        private Boolean _IsHeader;
        private Boolean _IsShowInPullDownMenu;
        private Boolean _IsVisible;
        private Boolean _IsBeginGroup;
        private String _HelpLinkIDForList;
        private String _HelpLinkIDForEntry;
        private String _Remarks;
        private Boolean _IsActive;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "MenuID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 MenuID
        {
            get { return _MenuID; }
            set { _MenuID = value; }
        }
        [Column(Name = "MenuCode", DataType = "String")]
        public String MenuCode
        {
            get { return _MenuCode; }
            set { _MenuCode = value; }
        }
        [Column(Name = "ModuleID", DataType = "String", IsNullable = true)]
        public String ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }
        [Column(Name = "MenuCaption", DataType = "String")]
        public String MenuCaption
        {
            get { return _MenuCaption; }
            set { _MenuCaption = value; }
        }
        [Column(Name = "MenuUrl", DataType = "String", IsNullable = true)]
        public String MenuUrl
        {
            get { return _MenuUrl; }
            set { _MenuUrl = value; }
        }
        [Column(Name = "MenuLevel", DataType = "Int16")]
        public Int16 MenuLevel
        {
            get { return _MenuLevel; }
            set { _MenuLevel = value; }
        }
        [Column(Name = "MenuIndex", DataType = "Int16")]
        public Int16 MenuIndex
        {
            get { return _MenuIndex; }
            set { _MenuIndex = value; }
        }
        [Column(Name = "MenuTooltip", DataType = "String", IsNullable = true)]
        public String MenuTooltip
        {
            get { return _MenuTooltip; }
            set { _MenuTooltip = value; }
        }
        [Column(Name = "ParentID", DataType = "Int32", IsNullable = true)]
        public Int32? ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        [Column(Name = "CRUDMode", DataType = "String")]
        public String CRUDMode
        {
            get { return _CRUDMode; }
            set { _CRUDMode = value; }
        }
        [Column(Name = "ImageUrl", DataType = "String", IsNullable = true)]
        public String ImageUrl
        {
            get { return _ImageUrl; }
            set { _ImageUrl = value; }
        }
        [Column(Name = "IsHeader", DataType = "Boolean")]
        public Boolean IsHeader
        {
            get { return _IsHeader; }
            set { _IsHeader = value; }
        }
        [Column(Name = "IsShowInPullDownMenu", DataType = "Boolean")]
        public Boolean IsShowInPullDownMenu
        {
            get { return _IsShowInPullDownMenu; }
            set { _IsShowInPullDownMenu = value; }
        }
        [Column(Name = "IsVisible", DataType = "Boolean")]
        public Boolean IsVisible
        {
            get { return _IsVisible; }
            set { _IsVisible = value; }
        }
        [Column(Name = "IsBeginGroup", DataType = "Boolean")]
        public Boolean IsBeginGroup
        {
            get { return _IsBeginGroup; }
            set { _IsBeginGroup = value; }
        }
        [Column(Name = "HelpLinkIDForList", DataType = "String", IsNullable = true)]
        public String HelpLinkIDForList
        {
            get { return _HelpLinkIDForList; }
            set { _HelpLinkIDForList = value; }
        }
        [Column(Name = "HelpLinkIDForEntry", DataType = "String", IsNullable = true)]
        public String HelpLinkIDForEntry
        {
            get { return _HelpLinkIDForEntry; }
            set { _HelpLinkIDForEntry = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsActive", DataType = "Boolean")]
        public Boolean IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
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

    public class MenuMasterDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(MenuMaster));
        private bool _isAuditLog = false;
        private const string p_MenuID = "@p_MenuID";
        public MenuMasterDao() { }
        public MenuMasterDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public MenuMaster Get(Int32 MenuID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_MenuID, MenuID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (MenuMaster)_helper.DataRowToObject(row, new MenuMaster());
        }
        public int Insert(MenuMaster record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(MenuMaster record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 MenuID)
        {
            MenuMaster record;
            if (_ctx.Transaction == null)
                record = new MenuMasterDao().Get(MenuID);
            else
                record = Get(MenuID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region MenuReport
    [Serializable]
    [Table(Name = "MenuReport")]
    public class MenuReport : DbDataModel
    {
        private Int32 _MenuID;
        private Int32 _ReportID;
        private Int16 _DisplayOrder;
        private Boolean _IsSelected;

        [Column(Name = "MenuID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 MenuID
        {
            get { return _MenuID; }
            set { _MenuID = value; }
        }
        [Column(Name = "ReportID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 ReportID
        {
            get { return _ReportID; }
            set { _ReportID = value; }
        }
        [Column(Name = "DisplayOrder", DataType = "Int16")]
        public Int16 DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        [Column(Name = "IsSelected", DataType = "Boolean")]
        public Boolean IsSelected
        {
            get { return _IsSelected; }
            set { _IsSelected = value; }
        }
    }

    public class MenuReportDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(MenuReport));
        private bool _isAuditLog = false;
        private const string p_MenuID = "@p_MenuID";
        private const string p_ReportID = "@p_ReportID";
        public MenuReportDao() { }
        public MenuReportDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public MenuReport Get(Int32 MenuID, Int32 ReportID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_MenuID, MenuID);
            _ctx.Add(p_ReportID, ReportID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (MenuReport)_helper.DataRowToObject(row, new MenuReport());
        }
        public int Insert(MenuReport record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(MenuReport record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 MenuID, Int32 ReportID)
        {
            MenuReport record;
            if (_ctx.Transaction == null)
                record = new MenuReportDao().Get(MenuID, ReportID);
            else
                record = Get(MenuID, ReportID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Module
    [Serializable]
    [Table(Name = "Module")]
    public class Module : DbDataModel
    {
        private String _ModuleID;
        private String _ModuleName;
        private String _ModuleShortName;
        private Int16 _ModuleIndex;
        private String _ImageUrl;
        private String _DisabledImageUrl;
        private String _DefaultUrl;
        private String _Description;
        private String _BackgroundColor;
        private Boolean _IsVisible;
        private Int32 _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ModuleID", DataType = "String", IsPrimaryKey = true)]
        public String ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }
        [Column(Name = "ModuleName", DataType = "String")]
        public String ModuleName
        {
            get { return _ModuleName; }
            set { _ModuleName = value; }
        }
        [Column(Name = "ModuleShortName", DataType = "String")]
        public String ModuleShortName
        {
            get { return _ModuleShortName; }
            set { _ModuleShortName = value; }
        }
        [Column(Name = "ModuleIndex", DataType = "Int16")]
        public Int16 ModuleIndex
        {
            get { return _ModuleIndex; }
            set { _ModuleIndex = value; }
        }
        [Column(Name = "ImageUrl", DataType = "String", IsNullable = true)]
        public String ImageUrl
        {
            get { return _ImageUrl; }
            set { _ImageUrl = value; }
        }
        [Column(Name = "DisabledImageUrl", DataType = "String", IsNullable = true)]
        public String DisabledImageUrl
        {
            get { return _DisabledImageUrl; }
            set { _DisabledImageUrl = value; }
        }
        [Column(Name = "DefaultUrl", DataType = "String", IsNullable = true)]
        public String DefaultUrl
        {
            get { return _DefaultUrl; }
            set { _DefaultUrl = value; }
        }
        [Column(Name = "Description", DataType = "String", IsNullable = true)]
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        [Column(Name = "BackgroundColor", DataType = "String", IsNullable = true)]
        public String BackgroundColor
        {
            get { return _BackgroundColor; }
            set { _BackgroundColor = value; }
        }
        [Column(Name = "IsVisible", DataType = "Boolean")]
        public Boolean IsVisible
        {
            get { return _IsVisible; }
            set { _IsVisible = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32 LastUpdatedBy
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

    public class ModuleDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Module));
        private bool _isAuditLog = false;
        private const string p_ModuleID = "@p_ModuleID";
        public ModuleDao() { }
        public ModuleDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Module Get(String ModuleID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ModuleID, ModuleID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Module)_helper.DataRowToObject(row, new Module());
        }
        public int Insert(Module record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Module record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(String ModuleID)
        {
            Module record;
            if (_ctx.Transaction == null)
                record = new ModuleDao().Get(ModuleID);
            else
                record = Get(ModuleID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region MasterCoding
    [Serializable]
    [Table(Name = "MasterCoding")]
    public class MasterCoding : DbDataModel
    {
        private String _MasterCode;
        private String _MasterName;
        private String _GCPrefixType;
        private String _DefaultPrefix;
        private Int16 _PrefixLength;
        private Boolean _IsBySite;
        private Int16 _CounterDigit;
        private Boolean _IsAllowChangeInitial;
        private Boolean _IsEditable;
        private String _TableName;
        private String _CodeFieldName;
        private String _NameFieldName;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "MasterCode", DataType = "String", IsPrimaryKey = true)]
        public String MasterCode
        {
            get { return _MasterCode; }
            set { _MasterCode = value; }
        }
        [Column(Name = "MasterName", DataType = "String")]
        public String MasterName
        {
            get { return _MasterName; }
            set { _MasterName = value; }
        }
        [Column(Name = "GCPrefixType", DataType = "String")]
        public String GCPrefixType
        {
            get { return _GCPrefixType; }
            set { _GCPrefixType = value; }
        }
        [Column(Name = "DefaultPrefix", DataType = "String")]
        public String DefaultPrefix
        {
            get { return _DefaultPrefix; }
            set { _DefaultPrefix = value; }
        }
        [Column(Name = "PrefixLength", DataType = "Int16")]
        public Int16 PrefixLength
        {
            get { return _PrefixLength; }
            set { _PrefixLength = value; }
        }
        [Column(Name = "IsBySite", DataType = "Boolean")]
        public Boolean IsBySite
        {
            get { return _IsBySite; }
            set { _IsBySite = value; }
        }
        [Column(Name = "CounterDigit", DataType = "Int16")]
        public Int16 CounterDigit
        {
            get { return _CounterDigit; }
            set { _CounterDigit = value; }
        }
        [Column(Name = "IsAllowChangeInitial", DataType = "Boolean")]
        public Boolean IsAllowChangeInitial
        {
            get { return _IsAllowChangeInitial; }
            set { _IsAllowChangeInitial = value; }
        }
        [Column(Name = "IsEditable", DataType = "Boolean")]
        public Boolean IsEditable
        {
            get { return _IsEditable; }
            set { _IsEditable = value; }
        }
        [Column(Name = "TableName", DataType = "String")]
        public String TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        [Column(Name = "CodeFieldName", DataType = "String")]
        public String CodeFieldName
        {
            get { return _CodeFieldName; }
            set { _CodeFieldName = value; }
        }
        [Column(Name = "NameFieldName", DataType = "String")]
        public String NameFieldName
        {
            get { return _NameFieldName; }
            set { _NameFieldName = value; }
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

    public class MasterCodingDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(MasterCoding));
        private bool _isAuditLog = false;
        private const string p_MasterCode = "@p_MasterCode";
        public MasterCodingDao() { }
        public MasterCodingDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public MasterCoding Get(String MasterCode)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_MasterCode, MasterCode);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (MasterCoding)_helper.DataRowToObject(row, new MasterCoding());
        }
        public int Insert(MasterCoding record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(MasterCoding record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(String MasterCode)
        {
            MasterCoding record;
            if (_ctx.Transaction == null)
                record = new MasterCodingDao().Get(MasterCode);
            else
                record = Get(MasterCode);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PivotSettingDt
    [Serializable]
    [Table(Name = "PivotSettingDt")]
    public class PivotSettingDt : DbDataModel
    {
        private Int32 _ID;
        private Int32 _PivotSettingID;
        private String _ColumnID;
        private String _ColumnName;
        private String _DisplayText;
        private Int32 _ColumnWidth;
        private String _DefaultOrderType;
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
        [Column(Name = "PivotSettingID", DataType = "Int32")]
        public Int32 PivotSettingID
        {
            get { return _PivotSettingID; }
            set { _PivotSettingID = value; }
        }
        [Column(Name = "ColumnID", DataType = "String")]
        public String ColumnID
        {
            get { return _ColumnID; }
            set { _ColumnID = value; }
        }
        [Column(Name = "ColumnName", DataType = "String")]
        public String ColumnName
        {
            get { return _ColumnName; }
            set { _ColumnName = value; }
        }
        [Column(Name = "DisplayText", DataType = "String")]
        public String DisplayText
        {
            get { return _DisplayText; }
            set { _DisplayText = value; }
        }
        [Column(Name = "ColumnWidth", DataType = "Int32")]
        public Int32 ColumnWidth
        {
            get { return _ColumnWidth; }
            set { _ColumnWidth = value; }
        }
        [Column(Name = "DefaultOrderType", DataType = "String")]
        public String DefaultOrderType
        {
            get { return _DefaultOrderType; }
            set { _DefaultOrderType = value; }
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

    public class PivotSettingDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PivotSettingDt));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public PivotSettingDtDao() { }
        public PivotSettingDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PivotSettingDt Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PivotSettingDt)_helper.DataRowToObject(row, new PivotSettingDt());
        }
        public int Insert(PivotSettingDt record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PivotSettingDt record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            PivotSettingDt record;
            if (_ctx.Transaction == null)
                record = new PivotSettingDtDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PivotSettingHd
    [Serializable]
    [Table(Name = "PivotSettingHd")]
    public class PivotSettingHd : DbDataModel
    {
        private Int32 _PivotSettingID;
        private String _PivotSettingCode;
        private String _PivotSettingName;
        private String _ValueFieldColumn;
        private String _ValueFieldType;
        private String _ObjectTypeName;
        private Boolean _IsDeleted;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "PivotSettingID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 PivotSettingID
        {
            get { return _PivotSettingID; }
            set { _PivotSettingID = value; }
        }
        [Column(Name = "PivotSettingCode", DataType = "String")]
        public String PivotSettingCode
        {
            get { return _PivotSettingCode; }
            set { _PivotSettingCode = value; }
        }
        [Column(Name = "PivotSettingName", DataType = "String")]
        public String PivotSettingName
        {
            get { return _PivotSettingName; }
            set { _PivotSettingName = value; }
        }
        [Column(Name = "ValueFieldColumn", DataType = "String")]
        public String ValueFieldColumn
        {
            get { return _ValueFieldColumn; }
            set { _ValueFieldColumn = value; }
        }
        [Column(Name = "ValueFieldType", DataType = "String")]
        public String ValueFieldType
        {
            get { return _ValueFieldType; }
            set { _ValueFieldType = value; }
        }
        [Column(Name = "ObjectTypeName", DataType = "String")]
        public String ObjectTypeName
        {
            get { return _ObjectTypeName; }
            set { _ObjectTypeName = value; }
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

    public class PivotSettingHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PivotSettingHd));
        private bool _isAuditLog = false;
        private const string p_PivotSettingID = "@p_PivotSettingID";
        public PivotSettingHdDao() { }
        public PivotSettingHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PivotSettingHd Get(Int32 PivotSettingID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_PivotSettingID, PivotSettingID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PivotSettingHd)_helper.DataRowToObject(row, new PivotSettingHd());
        }
        public int Insert(PivotSettingHd record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PivotSettingHd record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 PivotSettingID)
        {
            PivotSettingHd record;
            if (_ctx.Transaction == null)
                record = new PivotSettingHdDao().Get(PivotSettingID);
            else
                record = Get(PivotSettingID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ReportMaster
    [Serializable]
    [Table(Name = "ReportMaster")]
    public class ReportMaster : DbDataModel
    {
        private Int32 _ReportID;
        private String _ModuleID;
        private String _ReportCode;
        private String _ReportName;
        private String _GCReportType;
        private String _ReportUrl;
        private Int32? _ParentID;
        private Boolean _IsHeader;
        private Int16 _DisplayOrder;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ReportID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ReportID
        {
            get { return _ReportID; }
            set { _ReportID = value; }
        }
        [Column(Name = "ModuleID", DataType = "String", IsNullable = true)]
        public String ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }
        [Column(Name = "ReportCode", DataType = "String")]
        public String ReportCode
        {
            get { return _ReportCode; }
            set { _ReportCode = value; }
        }
        [Column(Name = "ReportName", DataType = "String")]
        public String ReportName
        {
            get { return _ReportName; }
            set { _ReportName = value; }
        }
        [Column(Name = "GCReportType", DataType = "String")]
        public String GCReportType
        {
            get { return _GCReportType; }
            set { _GCReportType = value; }
        }
        [Column(Name = "ReportUrl", DataType = "String")]
        public String ReportUrl
        {
            get { return _ReportUrl; }
            set { _ReportUrl = value; }
        }
        [Column(Name = "ParentID", DataType = "Int32", IsNullable = true)]
        public Int32? ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        [Column(Name = "IsHeader", DataType = "Boolean")]
        public Boolean IsHeader
        {
            get { return _IsHeader; }
            set { _IsHeader = value; }
        }
        [Column(Name = "DisplayOrder", DataType = "Int16")]
        public Int16 DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
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

    public class ReportMasterDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ReportMaster));
        private bool _isAuditLog = false;
        private const string p_ReportID = "@p_ReportID";
        public ReportMasterDao() { }
        public ReportMasterDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ReportMaster Get(Int32 ReportID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ReportID, ReportID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ReportMaster)_helper.DataRowToObject(row, new ReportMaster());
        }
        public int Insert(ReportMaster record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ReportMaster record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ReportID)
        {
            ReportMaster record;
            if (_ctx.Transaction == null)
                record = new ReportMasterDao().Get(ReportID);
            else
                record = Get(ReportID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region SettingParameter
    [Serializable]
    [Table(Name = "SettingParameter")]
    public class SettingParameter : DbDataModel
    {
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

    public class SettingParameterDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(SettingParameter));
        private bool _isAuditLog = false;
        private const string p_ParameterCode = "@p_ParameterCode";
        public SettingParameterDao() { }
        public SettingParameterDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public SettingParameter Get(String ParameterCode)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ParameterCode, ParameterCode);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (SettingParameter)_helper.DataRowToObject(row, new SettingParameter());
        }
        public int Insert(SettingParameter record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(SettingParameter record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(String ParameterCode)
        {
            SettingParameter record;
            if (_ctx.Transaction == null)
                record = new SettingParameterDao().Get(ParameterCode);
            else
                record = Get(ParameterCode);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Site
    [Serializable]
    [Table(Name = "Site")]
    public class Site : DbDataModel
    {
        private String _SiteID;
        private String _SiteName;
        private String _ParentID;
        private Boolean _IsHeader;
        private String _GCOperatingGroup;
        private String _ShortName;
        private String _Initial;
        private Int32 _ReferenceID;
        private String _LicenseNo;
        private String _AddressID;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "SiteID", DataType = "String", IsPrimaryKey = true)]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "SiteName", DataType = "String")]
        public String SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }
        [Column(Name = "ParentID", DataType = "String", IsNullable = true)]
        public String ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        [Column(Name = "IsHeader", DataType = "Boolean")]
        public Boolean IsHeader
        {
            get { return _IsHeader; }
            set { _IsHeader = value; }
        }
        [Column(Name = "GCOperatingGroup", DataType = "String")]
        public String GCOperatingGroup
        {
            get { return _GCOperatingGroup; }
            set { _GCOperatingGroup = value; }
        }
        [Column(Name = "ShortName", DataType = "String")]
        public String ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }
        [Column(Name = "Initial", DataType = "String", IsNullable = true)]
        public String Initial
        {
            get { return _Initial; }
            set { _Initial = value; }
        }
        [Column(Name = "ReferenceID", DataType = "Int32")]
        public Int32 ReferenceID
        {
            get { return _ReferenceID; }
            set { _ReferenceID = value; }
        }
        [Column(Name = "LicenseNo", DataType = "String", IsNullable = true)]
        public String LicenseNo
        {
            get { return _LicenseNo; }
            set { _LicenseNo = value; }
        }
        [Column(Name = "AddressID", DataType = "String", IsNullable = true)]
        public String AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
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

    public class SiteDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Site));
        private bool _isAuditLog = false;
        private const string p_SiteID = "@p_SiteID";
        public SiteDao() { }
        public SiteDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Site Get(String SiteID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_SiteID, SiteID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Site)_helper.DataRowToObject(row, new Site());
        }
        public int Insert(Site record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Site record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(String SiteID)
        {
            Site record;
            if (_ctx.Transaction == null)
                record = new SiteDao().Get(SiteID);
            else
                record = Get(SiteID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region SiteModule
    [Serializable]
    [Table(Name = "SiteModule")]
    public class SiteModule : DbDataModel
    {
        private Int32 _SiteModuleID;
        private String _SiteID;
        private String _ModuleID;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "SiteModuleID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 SiteModuleID
        {
            get { return _SiteModuleID; }
            set { _SiteModuleID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "ModuleID", DataType = "String")]
        public String ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
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

    public class SiteModuleDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(SiteModule));
        private bool _isAuditLog = false;
        private const string p_SiteModuleID = "@p_SiteModuleID";
        public SiteModuleDao() { }
        public SiteModuleDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public SiteModule Get(Int32 SiteModuleID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_SiteModuleID, SiteModuleID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (SiteModule)_helper.DataRowToObject(row, new SiteModule());
        }
        public int Insert(SiteModule record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(SiteModule record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 SiteModuleID)
        {
            SiteModule record;
            if (_ctx.Transaction == null)
                record = new SiteModuleDao().Get(SiteModuleID);
            else
                record = Get(SiteModuleID);
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
        private String _ListText;
        private String _ListValue;
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
        [Column(Name = "ListText", DataType = "String", IsNullable = true)]
        public String ListText
        {
            get { return _ListText; }
            set { _ListText = value; }
        }
        [Column(Name = "ListValue", DataType = "String", IsNullable = true)]
        public String ListValue
        {
            get { return _ListValue; }
            set { _ListValue = value; }
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
    #region StandardCode
    [Serializable]
    [Table(Name = "StandardCode")]
    public partial class StandardCode : DbDataModel
    {
        private String _StandardCodeID;
        private String _StandardCodeName;
        private String _TagProperty;
        private String _ParentID;
        private Boolean _IsHeader;
        private Boolean _IsDefault;
        private Boolean _IsEditableByUser;
        private Boolean _IsActive;
        private String _Notes;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "StandardCodeID", DataType = "String", IsPrimaryKey = true)]
        public String StandardCodeID
        {
            get { return _StandardCodeID; }
            set { _StandardCodeID = value; }
        }
        [Column(Name = "StandardCodeName", DataType = "String")]
        public String StandardCodeName
        {
            get { return _StandardCodeName; }
            set { _StandardCodeName = value; }
        }
        [Column(Name = "TagProperty", DataType = "String", IsNullable = true)]
        public String TagProperty
        {
            get { return _TagProperty; }
            set { _TagProperty = value; }
        }
        [Column(Name = "ParentID", DataType = "String", IsNullable = true)]
        public String ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        [Column(Name = "IsHeader", DataType = "Boolean")]
        public Boolean IsHeader
        {
            get { return _IsHeader; }
            set { _IsHeader = value; }
        }
        [Column(Name = "IsDefault", DataType = "Boolean", IsNullable = true)]
        public Boolean IsDefault
        {
            get { return _IsDefault; }
            set { _IsDefault = value; }
        }
        [Column(Name = "IsEditableByUser", DataType = "Boolean")]
        public Boolean IsEditableByUser
        {
            get { return _IsEditableByUser; }
            set { _IsEditableByUser = value; }
        }
        [Column(Name = "IsActive", DataType = "Boolean")]
        public Boolean IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        [Column(Name = "Notes", DataType = "String", IsNullable = true)]
        public String Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
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

    public class StandardCodeDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(StandardCode));
        private bool _isAuditLog = false;
        private const string p_StandardCodeID = "@p_StandardCodeID";
        public StandardCodeDao() { }
        public StandardCodeDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public StandardCode Get(String StandardCodeID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_StandardCodeID, StandardCodeID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (StandardCode)_helper.DataRowToObject(row, new StandardCode());
        }
        public int Insert(StandardCode record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(StandardCode record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(String StandardCodeID)
        {
            StandardCode record;
            if (_ctx.Transaction == null)
                record = new StandardCodeDao().Get(StandardCodeID);
            else
                record = Get(StandardCodeID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region TagField
    [Serializable]
    [Table(Name = "TagField")]
    public class TagField : DbDataModel
    {
        private String _GCBusinessObjectType;
        private String _TagField1;
        private String _TagField2;
        private String _TagField3;
        private String _TagField4;
        private String _TagField5;
        private String _TagField6;
        private String _TagField7;
        private String _TagField8;
        private String _TagField9;
        private String _TagField10;
        private String _TagField11;
        private String _TagField12;
        private String _TagField13;
        private String _TagField14;
        private String _TagField15;
        private String _TagField16;
        private String _TagField17;
        private String _TagField18;
        private String _TagField19;
        private String _TagField20;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "GCBusinessObjectType", DataType = "String", IsPrimaryKey = true)]
        public String GCBusinessObjectType
        {
            get { return _GCBusinessObjectType; }
            set { _GCBusinessObjectType = value; }
        }
        [Column(Name = "TagField1", DataType = "String", IsNullable = true)]
        public String TagField1
        {
            get { return _TagField1; }
            set { _TagField1 = value; }
        }
        [Column(Name = "TagField2", DataType = "String", IsNullable = true)]
        public String TagField2
        {
            get { return _TagField2; }
            set { _TagField2 = value; }
        }
        [Column(Name = "TagField3", DataType = "String", IsNullable = true)]
        public String TagField3
        {
            get { return _TagField3; }
            set { _TagField3 = value; }
        }
        [Column(Name = "TagField4", DataType = "String", IsNullable = true)]
        public String TagField4
        {
            get { return _TagField4; }
            set { _TagField4 = value; }
        }
        [Column(Name = "TagField5", DataType = "String", IsNullable = true)]
        public String TagField5
        {
            get { return _TagField5; }
            set { _TagField5 = value; }
        }
        [Column(Name = "TagField6", DataType = "String", IsNullable = true)]
        public String TagField6
        {
            get { return _TagField6; }
            set { _TagField6 = value; }
        }
        [Column(Name = "TagField7", DataType = "String", IsNullable = true)]
        public String TagField7
        {
            get { return _TagField7; }
            set { _TagField7 = value; }
        }
        [Column(Name = "TagField8", DataType = "String", IsNullable = true)]
        public String TagField8
        {
            get { return _TagField8; }
            set { _TagField8 = value; }
        }
        [Column(Name = "TagField9", DataType = "String", IsNullable = true)]
        public String TagField9
        {
            get { return _TagField9; }
            set { _TagField9 = value; }
        }
        [Column(Name = "TagField10", DataType = "String", IsNullable = true)]
        public String TagField10
        {
            get { return _TagField10; }
            set { _TagField10 = value; }
        }
        [Column(Name = "TagField11", DataType = "String", IsNullable = true)]
        public String TagField11
        {
            get { return _TagField11; }
            set { _TagField11 = value; }
        }
        [Column(Name = "TagField12", DataType = "String", IsNullable = true)]
        public String TagField12
        {
            get { return _TagField12; }
            set { _TagField12 = value; }
        }
        [Column(Name = "TagField13", DataType = "String", IsNullable = true)]
        public String TagField13
        {
            get { return _TagField13; }
            set { _TagField13 = value; }
        }
        [Column(Name = "TagField14", DataType = "String", IsNullable = true)]
        public String TagField14
        {
            get { return _TagField14; }
            set { _TagField14 = value; }
        }
        [Column(Name = "TagField15", DataType = "String", IsNullable = true)]
        public String TagField15
        {
            get { return _TagField15; }
            set { _TagField15 = value; }
        }
        [Column(Name = "TagField16", DataType = "String", IsNullable = true)]
        public String TagField16
        {
            get { return _TagField16; }
            set { _TagField16 = value; }
        }
        [Column(Name = "TagField17", DataType = "String", IsNullable = true)]
        public String TagField17
        {
            get { return _TagField17; }
            set { _TagField17 = value; }
        }
        [Column(Name = "TagField18", DataType = "String", IsNullable = true)]
        public String TagField18
        {
            get { return _TagField18; }
            set { _TagField18 = value; }
        }
        [Column(Name = "TagField19", DataType = "String", IsNullable = true)]
        public String TagField19
        {
            get { return _TagField19; }
            set { _TagField19 = value; }
        }
        [Column(Name = "TagField20", DataType = "String", IsNullable = true)]
        public String TagField20
        {
            get { return _TagField20; }
            set { _TagField20 = value; }
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

    public class TagFieldDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(TagField));
        private bool _isAuditLog = false;
        private const string p_GCBusinessObjectType = "@p_GCBusinessObjectType";
        public TagFieldDao() { }
        public TagFieldDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public TagField Get(String GCBusinessObjectType)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_GCBusinessObjectType, GCBusinessObjectType);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (TagField)_helper.DataRowToObject(row, new TagField());
        }
        public int Insert(TagField record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(TagField record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(String GCBusinessObjectType)
        {
            TagField record;
            if (_ctx.Transaction == null)
                record = new TagFieldDao().Get(GCBusinessObjectType);
            else
                record = Get(GCBusinessObjectType);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region TransactionType
    [Serializable]
    [Table(Name = "TransactionType")]
    public class TransactionType : DbDataModel
    {
        private String _TransactionCode;
        private String _TransactionName;
        private String _TransactionInitial;
        private Boolean _IsByDepartment;
        private Boolean _IsNeedApproval;
        private Boolean _IsInventoryTransaction;
        private String _NumberingMethod;
        private Int16 _CounterDigit;
        private String _TableName;
        private String _FieldName1;
        private String _FieldName2;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "TransactionCode", DataType = "String", IsPrimaryKey = true)]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
        [Column(Name = "TransactionName", DataType = "String")]
        public String TransactionName
        {
            get { return _TransactionName; }
            set { _TransactionName = value; }
        }
        [Column(Name = "TransactionInitial", DataType = "String")]
        public String TransactionInitial
        {
            get { return _TransactionInitial; }
            set { _TransactionInitial = value; }
        }
        [Column(Name = "IsByDepartment", DataType = "Boolean")]
        public Boolean IsByDepartment
        {
            get { return _IsByDepartment; }
            set { _IsByDepartment = value; }
        }
        [Column(Name = "IsNeedApproval", DataType = "Boolean")]
        public Boolean IsNeedApproval
        {
            get { return _IsNeedApproval; }
            set { _IsNeedApproval = value; }
        }
        [Column(Name = "IsInventoryTransaction", DataType = "Boolean")]
        public Boolean IsInventoryTransaction
        {
            get { return _IsInventoryTransaction; }
            set { _IsInventoryTransaction = value; }
        }
        [Column(Name = "NumberingMethod", DataType = "String")]
        public String NumberingMethod
        {
            get { return _NumberingMethod; }
            set { _NumberingMethod = value; }
        }
        [Column(Name = "CounterDigit", DataType = "Int16")]
        public Int16 CounterDigit
        {
            get { return _CounterDigit; }
            set { _CounterDigit = value; }
        }
        [Column(Name = "TableName", DataType = "String")]
        public String TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        [Column(Name = "FieldName1", DataType = "String")]
        public String FieldName1
        {
            get { return _FieldName1; }
            set { _FieldName1 = value; }
        }
        [Column(Name = "FieldName2", DataType = "String")]
        public String FieldName2
        {
            get { return _FieldName2; }
            set { _FieldName2 = value; }
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

    public class TransactionTypeDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(TransactionType));
        private bool _isAuditLog = false;
        private const string p_TransactionCode = "@p_TransactionCode";
        public TransactionTypeDao() { }
        public TransactionTypeDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public TransactionType Get(String TransactionCode)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_TransactionCode, TransactionCode);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (TransactionType)_helper.DataRowToObject(row, new TransactionType());
        }
        public int Insert(TransactionType record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(TransactionType record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(String TransactionCode)
        {
            TransactionType record;
            if (_ctx.Transaction == null)
                record = new TransactionTypeDao().Get(TransactionCode);
            else
                record = Get(TransactionCode);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region User
    [Serializable]
    [Table(Name = "[User]")]
    public class User : DbDataModel
    {
        private Int32 _UserID;
        private String _UserName;
        private String _LoweredUserName;
        private String _Password;
        private String _MobileAlias;
        private Boolean _IsAnonymous;
        private DateTime _LastActivityDate;
        private String _MobilePIN;
        private String _Email;
        private String _LoweredEmail;
        private String _PasswordQuestion;
        private String _PasswordAnswer;
        private Boolean _IsApproved;
        private Boolean _IsLockedOut;
        private DateTime _LastLoginDate;
        private DateTime _LastPasswordChangedDate;
        private DateTime _LastLockoutDate;
        private String _Comment;

        [Column(Name = "UserID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        [Column(Name = "UserName", DataType = "String")]
        public String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        [Column(Name = "LoweredUserName", DataType = "String")]
        public String LoweredUserName
        {
            get { return _LoweredUserName; }
            set { _LoweredUserName = value; }
        }
        [Column(Name = "Password", DataType = "String")]
        public String Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        [Column(Name = "MobileAlias", DataType = "String", IsNullable = true)]
        public String MobileAlias
        {
            get { return _MobileAlias; }
            set { _MobileAlias = value; }
        }
        [Column(Name = "IsAnonymous", DataType = "Boolean")]
        public Boolean IsAnonymous
        {
            get { return _IsAnonymous; }
            set { _IsAnonymous = value; }
        }
        [Column(Name = "LastActivityDate", DataType = "DateTime")]
        public DateTime LastActivityDate
        {
            get { return _LastActivityDate; }
            set { _LastActivityDate = value; }
        }
        [Column(Name = "MobilePIN", DataType = "String", IsNullable = true)]
        public String MobilePIN
        {
            get { return _MobilePIN; }
            set { _MobilePIN = value; }
        }
        [Column(Name = "Email", DataType = "String", IsNullable = true)]
        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        [Column(Name = "LoweredEmail", DataType = "String", IsNullable = true)]
        public String LoweredEmail
        {
            get { return _LoweredEmail; }
            set { _LoweredEmail = value; }
        }
        [Column(Name = "PasswordQuestion", DataType = "String", IsNullable = true)]
        public String PasswordQuestion
        {
            get { return _PasswordQuestion; }
            set { _PasswordQuestion = value; }
        }
        [Column(Name = "PasswordAnswer", DataType = "String", IsNullable = true)]
        public String PasswordAnswer
        {
            get { return _PasswordAnswer; }
            set { _PasswordAnswer = value; }
        }
        [Column(Name = "IsApproved", DataType = "Boolean")]
        public Boolean IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }
        [Column(Name = "IsLockedOut", DataType = "Boolean")]
        public Boolean IsLockedOut
        {
            get { return _IsLockedOut; }
            set { _IsLockedOut = value; }
        }
        [Column(Name = "LastLoginDate", DataType = "DateTime")]
        public DateTime LastLoginDate
        {
            get { return _LastLoginDate; }
            set { _LastLoginDate = value; }
        }
        [Column(Name = "LastPasswordChangedDate", DataType = "DateTime")]
        public DateTime LastPasswordChangedDate
        {
            get { return _LastPasswordChangedDate; }
            set { _LastPasswordChangedDate = value; }
        }
        [Column(Name = "LastLockoutDate", DataType = "DateTime")]
        public DateTime LastLockoutDate
        {
            get { return _LastLockoutDate; }
            set { _LastLockoutDate = value; }
        }
        [Column(Name = "Comment", DataType = "String", IsNullable = true)]
        public String Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }
    }

    public class UserDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(User));
        private bool _isAuditLog = false;
        private const string p_UserID = "@p_UserID";
        public UserDao() { }
        public UserDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public User Get(Int32 UserID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_UserID, UserID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (User)_helper.DataRowToObject(row, new User());
        }
        public int Insert(User record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(User record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 UserID)
        {
            User record;
            if (_ctx.Transaction == null)
                record = new UserDao().Get(UserID);
            else
                record = Get(UserID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region UserAttribute
    [Serializable]
    [Table(Name = "UserAttribute")]
    public class UserAttribute : DbDataModel
    {
        private Int32 _UserID;
        private String _FullName;
        private String _UserImage;
        private Int32? _EmployeeID;
        private Boolean _IsResetPassword;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "UserID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        [Column(Name = "FullName", DataType = "String", IsNullable = true)]
        public String FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
        [Column(Name = "UserImage", DataType = "String", IsNullable = true)]
        public String UserImage
        {
            get { return _UserImage; }
            set { _UserImage = value; }
        }
        [Column(Name = "EmployeeID", DataType = "Int32", IsNullable = true)]
        public Int32? EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }
        [Column(Name = "IsResetPassword", DataType = "Boolean")]
        public Boolean IsResetPassword
        {
            get { return _IsResetPassword; }
            set { _IsResetPassword = value; }
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

    public class UserAttributeDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(UserAttribute));
        private bool _isAuditLog = false;
        private const string p_UserID = "@p_UserID";
        public UserAttributeDao() { }
        public UserAttributeDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public UserAttribute Get(Int32 UserID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_UserID, UserID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (UserAttribute)_helper.DataRowToObject(row, new UserAttribute());
        }
        public int Insert(UserAttribute record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(UserAttribute record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 UserID)
        {
            UserAttribute record;
            if (_ctx.Transaction == null)
                record = new UserAttributeDao().Get(UserID);
            else
                record = Get(UserID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region UserInRole
    [Serializable]
    [Table(Name = "UserInRole")]
    public class UserInRole : DbDataModel
    {
        private Int32 _UserID;
        private String _SiteID;
        private Int32 _RoleID;
        private Boolean _IsMainRole;

        [Column(Name = "UserID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        [Column(Name = "SiteID", DataType = "String", IsPrimaryKey = true)]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "RoleID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        [Column(Name = "IsMainRole", DataType = "Boolean")]
        public Boolean IsMainRole
        {
            get { return _IsMainRole; }
            set { _IsMainRole = value; }
        }
    }

    public class UserInRoleDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(UserInRole));
        private bool _isAuditLog = false;
        private const string p_RoleID = "@p_RoleID";
        private const string p_SiteID = "@p_SiteID";
        private const string p_UserID = "@p_UserID";
        public UserInRoleDao() { }
        public UserInRoleDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public UserInRole Get(Int32 UserID, String SiteID, Int32 RoleID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_RoleID, RoleID);
            _ctx.Add(p_SiteID, SiteID);
            _ctx.Add(p_UserID, UserID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (UserInRole)_helper.DataRowToObject(row, new UserInRole());
        }
        public int Insert(UserInRole record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(UserInRole record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 UserID, String SiteID, Int32 RoleID)
        {
            UserInRole record;
            if (_ctx.Transaction == null)
                record = new UserInRoleDao().Get(UserID, SiteID, RoleID);
            else
                record = Get(UserID, SiteID, RoleID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region UserLoginAttribute
    [Serializable]
    [Table(Name = "UserLoginAttribute")]
    public class UserLoginAttribute : DbDataModel
    {
        private Int32 _UserID;
        private String _SiteID;
        private Int32 _LoginAttributeID;

        [Column(Name = "UserID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        [Column(Name = "SiteID", DataType = "String", IsPrimaryKey = true)]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "LoginAttributeID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 LoginAttributeID
        {
            get { return _LoginAttributeID; }
            set { _LoginAttributeID = value; }
        }
    }

    public class UserLoginAttributeDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(UserLoginAttribute));
        private bool _isAuditLog = false;
        private const string p_LoginAttributeID = "@p_LoginAttributeID";
        private const string p_SiteID = "@p_SiteID";
        private const string p_UserID = "@p_UserID";
        public UserLoginAttributeDao() { }
        public UserLoginAttributeDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public UserLoginAttribute Get(Int32 UserID, String SiteID, Int32 LoginAttributeID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_LoginAttributeID, LoginAttributeID);
            _ctx.Add(p_SiteID, SiteID);
            _ctx.Add(p_UserID, UserID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (UserLoginAttribute)_helper.DataRowToObject(row, new UserLoginAttribute());
        }
        public int Insert(UserLoginAttribute record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(UserLoginAttribute record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 UserID, String SiteID, Int32 LoginAttributeID)
        {
            UserLoginAttribute record;
            if (_ctx.Transaction == null)
                record = new UserLoginAttributeDao().Get(UserID, SiteID, LoginAttributeID);
            else
                record = Get(UserID, SiteID, LoginAttributeID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region UserMenu
    [Serializable]
    [Table(Name = "UserMenu")]
    public class UserMenu : DbDataModel
    {
        private Int32 _ID;
        private Int32 _MenuID;
        private String _SiteID;
        private Int32 _UserID;
        private String _CRUDMode;
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
        [Column(Name = "MenuID", DataType = "Int32")]
        public Int32 MenuID
        {
            get { return _MenuID; }
            set { _MenuID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "UserID", DataType = "Int32")]
        public Int32 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        [Column(Name = "CRUDMode", DataType = "String")]
        public String CRUDMode
        {
            get { return _CRUDMode; }
            set { _CRUDMode = value; }
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

    public class UserMenuDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(UserMenu));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public UserMenuDao() { }
        public UserMenuDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public UserMenu Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (UserMenu)_helper.DataRowToObject(row, new UserMenu());
        }
        public int Insert(UserMenu record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(UserMenu record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            UserMenu record;
            if (_ctx.Transaction == null)
                record = new UserMenuDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region UserReport
    [Serializable]
    [Table(Name = "UserReport")]
    public class UserReport : DbDataModel
    {
        private Int32 _UserID;
        private String _SiteID;
        private Int32 _ReportID;

        [Column(Name = "UserID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        [Column(Name = "SiteID", DataType = "String", IsPrimaryKey = true)]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "ReportID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 ReportID
        {
            get { return _ReportID; }
            set { _ReportID = value; }
        }
    }

    public class UserReportDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(UserReport));
        private bool _isAuditLog = false;
        private const string p_ReportID = "@p_ReportID";
        private const string p_SiteID = "@p_SiteID";
        private const string p_UserID = "@p_UserID";
        public UserReportDao() { }
        public UserReportDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public UserReport Get(Int32 UserID, String SiteID, Int32 ReportID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ReportID, ReportID);
            _ctx.Add(p_SiteID, SiteID);
            _ctx.Add(p_UserID, UserID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (UserReport)_helper.DataRowToObject(row, new UserReport());
        }
        public int Insert(UserReport record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(UserReport record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 UserID, String SiteID, Int32 ReportID)
        {
            UserReport record;
            if (_ctx.Transaction == null)
                record = new UserReportDao().Get(UserID, SiteID, ReportID);
            else
                record = Get(UserID, SiteID, ReportID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region UserRole
    [Serializable]
    [Table(Name = "UserRole")]
    public class UserRole : DbDataModel
    {
        private Int32 _RoleID;
        private String _RoleName;
        private String _LoweredRoleName;
        private String _Description;
        private String _DefaultPageUrl;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "RoleID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        [Column(Name = "RoleName", DataType = "String")]
        public String RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }
        [Column(Name = "LoweredRoleName", DataType = "String")]
        public String LoweredRoleName
        {
            get { return _LoweredRoleName; }
            set { _LoweredRoleName = value; }
        }
        [Column(Name = "Description", DataType = "String", IsNullable = true)]
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        [Column(Name = "DefaultPageUrl", DataType = "String")]
        public String DefaultPageUrl
        {
            get { return _DefaultPageUrl; }
            set { _DefaultPageUrl = value; }
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


    public class UserRoleDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(UserRole));
        private bool _isAuditLog = false;
        private const string p_RoleID = "@p_RoleID";
        public UserRoleDao() { }
        public UserRoleDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public UserRole Get(Int32 RoleID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_RoleID, RoleID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (UserRole)_helper.DataRowToObject(row, new UserRole());
        }
        public int Insert(UserRole record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(UserRole record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 RoleID)
        {
            UserRole record;
            if (_ctx.Transaction == null)
                record = new UserRoleDao().Get(RoleID);
            else
                record = Get(RoleID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region UserRoleLoginAttribute
    [Serializable]
    [Table(Name = "UserRoleLoginAttribute")]
    public class UserRoleLoginAttribute : DbDataModel
    {
        private Int32 _RoleID;
        private String _SiteID;
        private Int32 _LoginAttributeID;

        [Column(Name = "RoleID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        [Column(Name = "SiteID", DataType = "String", IsPrimaryKey = true)]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "LoginAttributeID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 LoginAttributeID
        {
            get { return _LoginAttributeID; }
            set { _LoginAttributeID = value; }
        }
    }

    public class UserRoleLoginAttributeDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(UserRoleLoginAttribute));
        private bool _isAuditLog = false;
        private const string p_LoginAttributeID = "@p_LoginAttributeID";
        private const string p_RoleID = "@p_RoleID";
        private const string p_SiteID = "@p_SiteID";
        public UserRoleLoginAttributeDao() { }
        public UserRoleLoginAttributeDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public UserRoleLoginAttribute Get(Int32 RoleID, String SiteID, Int32 LoginAttributeID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_LoginAttributeID, LoginAttributeID);
            _ctx.Add(p_RoleID, RoleID);
            _ctx.Add(p_SiteID, SiteID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (UserRoleLoginAttribute)_helper.DataRowToObject(row, new UserRoleLoginAttribute());
        }
        public int Insert(UserRoleLoginAttribute record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(UserRoleLoginAttribute record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 RoleID, String SiteID, Int32 LoginAttributeID)
        {
            UserRoleLoginAttribute record;
            if (_ctx.Transaction == null)
                record = new UserRoleLoginAttributeDao().Get(RoleID, SiteID, LoginAttributeID);
            else
                record = Get(RoleID, SiteID, LoginAttributeID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region UserRoleMenu
    [Serializable]
    [Table(Name = "UserRoleMenu")]
    public class UserRoleMenu : DbDataModel
    {
        private Int32 _ID;
        private Int32 _MenuID;
        private Int32 _RoleID;
        private String _CRUDMode;
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
        [Column(Name = "MenuID", DataType = "Int32")]
        public Int32 MenuID
        {
            get { return _MenuID; }
            set { _MenuID = value; }
        }
        [Column(Name = "RoleID", DataType = "Int32")]
        public Int32 RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        [Column(Name = "CRUDMode", DataType = "String")]
        public String CRUDMode
        {
            get { return _CRUDMode; }
            set { _CRUDMode = value; }
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

    public class UserRoleMenuDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(UserRoleMenu));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public UserRoleMenuDao() { }
        public UserRoleMenuDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public UserRoleMenu Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (UserRoleMenu)_helper.DataRowToObject(row, new UserRoleMenu());
        }
        public int Insert(UserRoleMenu record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(UserRoleMenu record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            UserRoleMenu record;
            if (_ctx.Transaction == null)
                record = new UserRoleMenuDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region UserRoleReport
    [Serializable]
    [Table(Name = "UserRoleReport")]
    public class UserRoleReport : DbDataModel
    {
        private Int32 _RoleID;
        private String _SiteID;
        private Int32 _ReportID;

        [Column(Name = "RoleID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        [Column(Name = "SiteID", DataType = "String", IsPrimaryKey = true)]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "ReportID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 ReportID
        {
            get { return _ReportID; }
            set { _ReportID = value; }
        }
    }

    public class UserRoleReportDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(UserRoleReport));
        private bool _isAuditLog = false;
        private const string p_ReportID = "@p_ReportID";
        private const string p_RoleID = "@p_RoleID";
        private const string p_SiteID = "@p_SiteID";
        public UserRoleReportDao() { }
        public UserRoleReportDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public UserRoleReport Get(Int32 RoleID, String SiteID, Int32 ReportID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ReportID, ReportID);
            _ctx.Add(p_RoleID, RoleID);
            _ctx.Add(p_SiteID, SiteID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (UserRoleReport)_helper.DataRowToObject(row, new UserRoleReport());
        }
        public int Insert(UserRoleReport record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(UserRoleReport record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 RoleID, String SiteID, Int32 ReportID)
        {
            UserRoleReport record;
            if (_ctx.Transaction == null)
                record = new UserRoleReportDao().Get(RoleID, SiteID, ReportID);
            else
                record = Get(RoleID, SiteID, ReportID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region UserTagField
    [Serializable]
    [Table(Name = "UserTagField")]
    public class UserTagField : DbDataModel
    {
        private Int32 _UserID;
        private String _TagField1;
        private String _TagField2;
        private String _TagField3;
        private String _TagField4;
        private String _TagField5;
        private String _TagField6;
        private String _TagField7;
        private String _TagField8;
        private String _TagField9;
        private String _TagField10;
        private String _TagField11;
        private String _TagField12;
        private String _TagField13;
        private String _TagField14;
        private String _TagField15;
        private String _TagField16;
        private String _TagField17;
        private String _TagField18;
        private String _TagField19;
        private String _TagField20;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "UserID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        [Column(Name = "TagField1", DataType = "String", IsNullable = true)]
        public String TagField1
        {
            get { return _TagField1; }
            set { _TagField1 = value; }
        }
        [Column(Name = "TagField2", DataType = "String", IsNullable = true)]
        public String TagField2
        {
            get { return _TagField2; }
            set { _TagField2 = value; }
        }
        [Column(Name = "TagField3", DataType = "String", IsNullable = true)]
        public String TagField3
        {
            get { return _TagField3; }
            set { _TagField3 = value; }
        }
        [Column(Name = "TagField4", DataType = "String", IsNullable = true)]
        public String TagField4
        {
            get { return _TagField4; }
            set { _TagField4 = value; }
        }
        [Column(Name = "TagField5", DataType = "String", IsNullable = true)]
        public String TagField5
        {
            get { return _TagField5; }
            set { _TagField5 = value; }
        }
        [Column(Name = "TagField6", DataType = "String", IsNullable = true)]
        public String TagField6
        {
            get { return _TagField6; }
            set { _TagField6 = value; }
        }
        [Column(Name = "TagField7", DataType = "String", IsNullable = true)]
        public String TagField7
        {
            get { return _TagField7; }
            set { _TagField7 = value; }
        }
        [Column(Name = "TagField8", DataType = "String", IsNullable = true)]
        public String TagField8
        {
            get { return _TagField8; }
            set { _TagField8 = value; }
        }
        [Column(Name = "TagField9", DataType = "String", IsNullable = true)]
        public String TagField9
        {
            get { return _TagField9; }
            set { _TagField9 = value; }
        }
        [Column(Name = "TagField10", DataType = "String", IsNullable = true)]
        public String TagField10
        {
            get { return _TagField10; }
            set { _TagField10 = value; }
        }
        [Column(Name = "TagField11", DataType = "String", IsNullable = true)]
        public String TagField11
        {
            get { return _TagField11; }
            set { _TagField11 = value; }
        }
        [Column(Name = "TagField12", DataType = "String", IsNullable = true)]
        public String TagField12
        {
            get { return _TagField12; }
            set { _TagField12 = value; }
        }
        [Column(Name = "TagField13", DataType = "String", IsNullable = true)]
        public String TagField13
        {
            get { return _TagField13; }
            set { _TagField13 = value; }
        }
        [Column(Name = "TagField14", DataType = "String", IsNullable = true)]
        public String TagField14
        {
            get { return _TagField14; }
            set { _TagField14 = value; }
        }
        [Column(Name = "TagField15", DataType = "String", IsNullable = true)]
        public String TagField15
        {
            get { return _TagField15; }
            set { _TagField15 = value; }
        }
        [Column(Name = "TagField16", DataType = "String", IsNullable = true)]
        public String TagField16
        {
            get { return _TagField16; }
            set { _TagField16 = value; }
        }
        [Column(Name = "TagField17", DataType = "String", IsNullable = true)]
        public String TagField17
        {
            get { return _TagField17; }
            set { _TagField17 = value; }
        }
        [Column(Name = "TagField18", DataType = "String", IsNullable = true)]
        public String TagField18
        {
            get { return _TagField18; }
            set { _TagField18 = value; }
        }
        [Column(Name = "TagField19", DataType = "String", IsNullable = true)]
        public String TagField19
        {
            get { return _TagField19; }
            set { _TagField19 = value; }
        }
        [Column(Name = "TagField20", DataType = "String", IsNullable = true)]
        public String TagField20
        {
            get { return _TagField20; }
            set { _TagField20 = value; }
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

    public class UserTagFieldDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(UserTagField));
        private bool _isAuditLog = false;
        private const string p_UserID = "@p_UserID";
        public UserTagFieldDao() { }
        public UserTagFieldDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public UserTagField Get(Int32 UserID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_UserID, UserID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (UserTagField)_helper.DataRowToObject(row, new UserTagField());
        }
        public int Insert(UserTagField record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(UserTagField record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 UserID)
        {
            UserTagField record;
            if (_ctx.Transaction == null)
                record = new UserTagFieldDao().Get(UserID);
            else
                record = Get(UserID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ZipCodes
    [Serializable]
    [Table(Name = "ZipCodes")]
    public class ZipCodes : DbDataModel
    {
        private Int32 _ID;
        private String _ZipCode;
        private String _StreetName;
        private String _District;
        private String _County;
        private String _City;
        private String _GCProvince;
        private Decimal _Longitude;
        private Decimal _Latitude;
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
        [Column(Name = "ZipCode", DataType = "String")]
        public String ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
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
        [Column(Name = "County", DataType = "String")]
        public String County
        {
            get { return _County; }
            set { _County = value; }
        }
        [Column(Name = "City", DataType = "String")]
        public String City
        {
            get { return _City; }
            set { _City = value; }
        }
        [Column(Name = "GCProvince", DataType = "String")]
        public String GCProvince
        {
            get { return _GCProvince; }
            set { _GCProvince = value; }
        }
        [Column(Name = "Longitude", DataType = "Decimal", IsNullable = true)]
        public Decimal Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; }
        }
        [Column(Name = "Latitude", DataType = "Decimal", IsNullable = true)]
        public Decimal Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean", IsNullable = true)]
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

    public class ZipCodesDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ZipCodes));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public ZipCodesDao() { }
        public ZipCodesDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ZipCodes Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ZipCodes)_helper.DataRowToObject(row, new ZipCodes());
        }
        public int Insert(ZipCodes record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ZipCodes record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            ZipCodes record;
            if (_ctx.Transaction == null)
                record = new ZipCodesDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #endregion


    #region Tools
    #region MigrationConfigurationDt
    [Serializable]
    [Table(Name = "MigrationConfigurationDt")]
    public partial class MigrationConfigurationDt : DbDataModel
    {
        private Int32 _ID;
        private Int32 _HeaderID;
        private String _TableName;
        private String _LinkColumn;
        private String _ColumnName;
        private String _ColumnCaption;
        private Boolean _IsVisible;
        private String _FromColumn;
        private String _DefaultValue;
        private Boolean _IsRequired;
        private String _Type;
        private String _MethodName;
        private String _ValueField;
        private String _TextField;
        private String _FilterExpression;
        private String _ValueChecked;
        private String _ValueUnchecked;
        private Boolean _OtherValue;
        private String _FormatDate;
        private String _SearchDialogType;
        private String _SearchDialogMethodName;
        private String _SearchDialogFilterExpression;
        private String _SearchDialogIDField;
        private String _SearchDialogCodeField;
        private String _SearchDialogNameField;
        private String _IDColumn;
        private String _FormatCode;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "HeaderID", DataType = "Int32")]
        public Int32 HeaderID
        {
            get { return _HeaderID; }
            set { _HeaderID = value; }
        }
        [Column(Name = "TableName", DataType = "String")]
        public String TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        [Column(Name = "LinkColumn", DataType = "String", IsNullable = true)]
        public String LinkColumn
        {
            get { return _LinkColumn; }
            set { _LinkColumn = value; }
        }
        [Column(Name = "ColumnName", DataType = "String")]
        public String ColumnName
        {
            get { return _ColumnName; }
            set { _ColumnName = value; }
        }
        [Column(Name = "ColumnCaption", DataType = "String", IsNullable = true)]
        public String ColumnCaption
        {
            get { return _ColumnCaption; }
            set { _ColumnCaption = value; }
        }
        [Column(Name = "IsVisible", DataType = "Boolean")]
        public Boolean IsVisible
        {
            get { return _IsVisible; }
            set { _IsVisible = value; }
        }
        [Column(Name = "FromColumn", DataType = "String", IsNullable = true)]
        public String FromColumn
        {
            get { return _FromColumn; }
            set { _FromColumn = value; }
        }
        [Column(Name = "DefaultValue", DataType = "String", IsNullable = true)]
        public String DefaultValue
        {
            get { return _DefaultValue; }
            set { _DefaultValue = value; }
        }
        [Column(Name = "IsRequired", DataType = "Boolean")]
        public Boolean IsRequired
        {
            get { return _IsRequired; }
            set { _IsRequired = value; }
        }
        [Column(Name = "Type", DataType = "String")]
        public String Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        [Column(Name = "MethodName", DataType = "String", IsNullable = true)]
        public String MethodName
        {
            get { return _MethodName; }
            set { _MethodName = value; }
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
        [Column(Name = "FilterExpression", DataType = "String", IsNullable = true)]
        public String FilterExpression
        {
            get { return _FilterExpression; }
            set { _FilterExpression = value; }
        }
        [Column(Name = "ValueChecked", DataType = "String", IsNullable = true)]
        public String ValueChecked
        {
            get { return _ValueChecked; }
            set { _ValueChecked = value; }
        }
        [Column(Name = "ValueUnchecked", DataType = "String", IsNullable = true)]
        public String ValueUnchecked
        {
            get { return _ValueUnchecked; }
            set { _ValueUnchecked = value; }
        }
        [Column(Name = "OtherValue", DataType = "Boolean", IsNullable = true)]
        public Boolean OtherValue
        {
            get { return _OtherValue; }
            set { _OtherValue = value; }
        }
        [Column(Name = "FormatDate", DataType = "String", IsNullable = true)]
        public String FormatDate
        {
            get { return _FormatDate; }
            set { _FormatDate = value; }
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
        [Column(Name = "IDColumn", DataType = "String", IsNullable = true)]
        public String IDColumn
        {
            get { return _IDColumn; }
            set { _IDColumn = value; }
        }
        [Column(Name = "FormatCode", DataType = "String", IsNullable = true)]
        public String FormatCode
        {
            get { return _FormatCode; }
            set { _FormatCode = value; }
        }
    }

    public class MigrationConfigurationDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(MigrationConfigurationDt));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public MigrationConfigurationDtDao() { }
        public MigrationConfigurationDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public MigrationConfigurationDt Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (MigrationConfigurationDt)_helper.DataRowToObject(row, new MigrationConfigurationDt());
        }
        public int Insert(MigrationConfigurationDt record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(MigrationConfigurationDt record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            MigrationConfigurationDt record;
            if (_ctx.Transaction == null)
                record = new MigrationConfigurationDtDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region MigrationConfigurationHd
    [Serializable]
    [Table(Name = "MigrationConfigurationHd")]
    public class MigrationConfigurationHd : DbDataModel
    {
        private Int32 _ID;
        private String _FromTable;
        private String _ToTable;
        private String _GridColumns;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "FromTable", DataType = "String")]
        public String FromTable
        {
            get { return _FromTable; }
            set { _FromTable = value; }
        }
        [Column(Name = "ToTable", DataType = "String")]
        public String ToTable
        {
            get { return _ToTable; }
            set { _ToTable = value; }
        }
        [Column(Name = "GridColumns", DataType = "String", IsNullable = true)]
        public String GridColumns
        {
            get { return _GridColumns; }
            set { _GridColumns = value; }
        }
    }

    public class MigrationConfigurationHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(MigrationConfigurationHd));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public MigrationConfigurationHdDao() { }
        public MigrationConfigurationHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public MigrationConfigurationHd Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (MigrationConfigurationHd)_helper.DataRowToObject(row, new MigrationConfigurationHd());
        }
        public int Insert(MigrationConfigurationHd record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(MigrationConfigurationHd record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            MigrationConfigurationHd record;
            if (_ctx.Transaction == null)
                record = new MigrationConfigurationHdDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region MigrationConfigurationTableLink
    [Serializable]
    [Table(Name = "MigrationConfigurationTableLink")]
    public class MigrationConfigurationTableLink : DbDataModel
    {
        private Int32 _HeaderID;
        private String _TableName;
        private String _ColumnName;
        private String _LinkTableName;
        private String _LinkTableColumn;
        private Boolean _IsOneToMany;
        private String _RepeaterTable;
        private String _RepeaterFilterExpression;
        private String _RepeaterIDValue;
        private String _RepeaterLabelValue;
        private String _DtColumnID;
        private String _DtColumnValue;

        [Column(Name = "HeaderID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 HeaderID
        {
            get { return _HeaderID; }
            set { _HeaderID = value; }
        }
        [Column(Name = "TableName", DataType = "String", IsPrimaryKey = true)]
        public String TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        [Column(Name = "ColumnName", DataType = "String", IsPrimaryKey = true)]
        public String ColumnName
        {
            get { return _ColumnName; }
            set { _ColumnName = value; }
        }
        [Column(Name = "LinkTableName", DataType = "String")]
        public String LinkTableName
        {
            get { return _LinkTableName; }
            set { _LinkTableName = value; }
        }
        [Column(Name = "LinkTableColumn", DataType = "String")]
        public String LinkTableColumn
        {
            get { return _LinkTableColumn; }
            set { _LinkTableColumn = value; }
        }
        [Column(Name = "IsOneToMany", DataType = "Boolean")]
        public Boolean IsOneToMany
        {
            get { return _IsOneToMany; }
            set { _IsOneToMany = value; }
        }
        [Column(Name = "RepeaterTable", DataType = "String", IsNullable = true)]
        public String RepeaterTable
        {
            get { return _RepeaterTable; }
            set { _RepeaterTable = value; }
        }
        [Column(Name = "RepeaterFilterExpression", DataType = "String", IsNullable = true)]
        public String RepeaterFilterExpression
        {
            get { return _RepeaterFilterExpression; }
            set { _RepeaterFilterExpression = value; }
        }
        [Column(Name = "RepeaterIDValue", DataType = "String", IsNullable = true)]
        public String RepeaterIDValue
        {
            get { return _RepeaterIDValue; }
            set { _RepeaterIDValue = value; }
        }
        [Column(Name = "RepeaterLabelValue", DataType = "String", IsNullable = true)]
        public String RepeaterLabelValue
        {
            get { return _RepeaterLabelValue; }
            set { _RepeaterLabelValue = value; }
        }
        [Column(Name = "DtColumnID", DataType = "String", IsNullable = true)]
        public String DtColumnID
        {
            get { return _DtColumnID; }
            set { _DtColumnID = value; }
        }
        [Column(Name = "DtColumnValue", DataType = "String", IsNullable = true)]
        public String DtColumnValue
        {
            get { return _DtColumnValue; }
            set { _DtColumnValue = value; }
        }
    }

    public class MigrationConfigurationTableLinkDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(MigrationConfigurationTableLink));
        private bool _isAuditLog = false;
        private const string p_ColumnName = "@p_ColumnName";
        private const string p_HeaderID = "@p_HeaderID";
        private const string p_TableName = "@p_TableName";
        public MigrationConfigurationTableLinkDao() { }
        public MigrationConfigurationTableLinkDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public MigrationConfigurationTableLink Get(Int32 HeaderID, String TableName, String ColumnName)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ColumnName, ColumnName);
            _ctx.Add(p_HeaderID, HeaderID);
            _ctx.Add(p_TableName, TableName);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (MigrationConfigurationTableLink)_helper.DataRowToObject(row, new MigrationConfigurationTableLink());
        }
        public int Insert(MigrationConfigurationTableLink record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(MigrationConfigurationTableLink record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 HeaderID, String TableName, String ColumnName)
        {
            MigrationConfigurationTableLink record;
            if (_ctx.Transaction == null)
                record = new MigrationConfigurationTableLinkDao().Get(HeaderID, TableName, ColumnName);
            else
                record = Get(HeaderID, TableName, ColumnName);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PersonNameConfiguration
    [Serializable]
    [Table(Name = "PersonNameConfiguration")]
    public class PersonNameConfiguration : DbDataModel
    {
        private Int32 _ID;
        private String _TableName;
        private String _FirstNameColumn;
        private String _MiddleNameColumn;
        private String _LastNameColumn;
        private String _TitleColumn;
        private String _SuffixColumn;
        private String _FullNameColumn;
        private String _NameColumn;
        private String _IDColumn;
        private Boolean _IsActive;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "TableName", DataType = "String")]
        public String TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        [Column(Name = "FirstNameColumn", DataType = "String", IsNullable = true)]
        public String FirstNameColumn
        {
            get { return _FirstNameColumn; }
            set { _FirstNameColumn = value; }
        }
        [Column(Name = "MiddleNameColumn", DataType = "String", IsNullable = true)]
        public String MiddleNameColumn
        {
            get { return _MiddleNameColumn; }
            set { _MiddleNameColumn = value; }
        }
        [Column(Name = "LastNameColumn", DataType = "String", IsNullable = true)]
        public String LastNameColumn
        {
            get { return _LastNameColumn; }
            set { _LastNameColumn = value; }
        }
        [Column(Name = "TitleColumn", DataType = "String", IsNullable = true)]
        public String TitleColumn
        {
            get { return _TitleColumn; }
            set { _TitleColumn = value; }
        }
        [Column(Name = "SuffixColumn", DataType = "String", IsNullable = true)]
        public String SuffixColumn
        {
            get { return _SuffixColumn; }
            set { _SuffixColumn = value; }
        }
        [Column(Name = "FullNameColumn", DataType = "String", IsNullable = true)]
        public String FullNameColumn
        {
            get { return _FullNameColumn; }
            set { _FullNameColumn = value; }
        }
        [Column(Name = "NameColumn", DataType = "String", IsNullable = true)]
        public String NameColumn
        {
            get { return _NameColumn; }
            set { _NameColumn = value; }
        }
        [Column(Name = "IDColumn", DataType = "String", IsNullable = true)]
        public String IDColumn
        {
            get { return _IDColumn; }
            set { _IDColumn = value; }
        }
        [Column(Name = "IsActive", DataType = "Boolean")]
        public Boolean IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
    }

    public class PersonNameConfigurationDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PersonNameConfiguration));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public PersonNameConfigurationDao() { }
        public PersonNameConfigurationDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PersonNameConfiguration Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PersonNameConfiguration)_helper.DataRowToObject(row, new PersonNameConfiguration());
        }
        public int Insert(PersonNameConfiguration record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PersonNameConfiguration record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            PersonNameConfiguration record;
            if (_ctx.Transaction == null)
                record = new PersonNameConfigurationDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region RestoreDataConfiguration
    [Serializable]
    [Table(Name = "RestoreDataConfiguration")]
    public class RestoreDataConfiguration : DbDataModel
    {
        private Int32 _ID;
        private String _TableName;
        private String _TableAlias;
        private String _FilterExpression;
        private String _GridColumns;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "TableName", DataType = "String")]
        public String TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        [Column(Name = "TableAlias", DataType = "String")]
        public String TableAlias
        {
            get { return _TableAlias; }
            set { _TableAlias = value; }
        }
        [Column(Name = "FilterExpression", DataType = "String", IsNullable = true)]
        public String FilterExpression
        {
            get { return _FilterExpression; }
            set { _FilterExpression = value; }
        }
        [Column(Name = "GridColumns", DataType = "String")]
        public String GridColumns
        {
            get { return _GridColumns; }
            set { _GridColumns = value; }
        }
    }

    public class RestoreDataConfigurationDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(RestoreDataConfiguration));
        private bool _isAuditLog = false;
        public RestoreDataConfigurationDao() { }
        public RestoreDataConfigurationDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public RestoreDataConfiguration Get()
        {
            _ctx.CommandText = _helper.GetRecord();
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (RestoreDataConfiguration)_helper.DataRowToObject(row, new RestoreDataConfiguration());
        }
        public int Insert(RestoreDataConfiguration record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(RestoreDataConfiguration record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete()
        {
            RestoreDataConfiguration record;
            if (_ctx.Transaction == null)
                record = new RestoreDataConfigurationDao().Get();
            else
                record = Get();
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region SysColumns
    [Serializable]
    [Table(Name = "Sys.columns")]
    public class SysColumns : DbDataModel
    {
        private String _Name;
        private Int32 _UserTypeID;
        private Boolean _IsNullable;
        private Boolean _IsIdentity;

        [Column(Name = "name", DataType = "String")]
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        [Column(Name = "user_type_id", DataType = "String")]
        public Int32 UserTypeID
        {
            get { return _UserTypeID; }
            set { _UserTypeID = value; }
        }

        [Column(Name = "is_nullable", DataType = "Boolean")]
        public Boolean IsNullable
        {
            get { return _IsNullable; }
            set { _IsNullable = value; }
        }

        [Column(Name = "is_identity", DataType = "Boolean")]
        public Boolean IsIdentity
        {
            get { return _IsIdentity; }
            set { _IsIdentity = value; }
        }

        public String Type
        {
            get
            {
                switch (_UserTypeID)
                {
                    case 48: return "Int16";
                    case 56: return "Int32";
                    case 104: return "Boolean";
                    case 61:
                    case 40: return "DateTime";
                    case 62: return "Double";
                    case 108: 
                    case 60: return "Decimal";
                    case 52: return "Int16";
                    case 127: return "Int64";
                }
                return "String";
            }
        }
    }
    #endregion
    #region SysObjects
    [Serializable]
    [Table(Name = "Sys.objects")]
    public class SysObjects : DbDataModel
    {
        private String _Name;
        private Int32 _ObjectID;

        [Column(Name = "name", DataType = "String")]
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        [Column(Name = "object_id", DataType = "Int32")]
        public Int32 ObjectID
        {
            get { return _ObjectID; }
            set { _ObjectID = value; }
        }
    }
    #endregion
    #endregion
}
