using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeX.Data.Core.Dal;
using System.Data;

namespace CodeX.Data.Model
{
    #region AdmissionSelection
    [Serializable]
    [Table(Name = "AdmissionSelection")]
    public class AdmissionSelection : DbDataModel
    {
        private Int32 _AdmissionSelectionID;
        private Int32 _SchoolPeriodID;
        private Int32? _PeriodAdmissionID;
        private String _SelectionName;
        private Int16 _DisplayOrder;
        private Int16 _FinalMarkPercentage;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "AdmissionSelectionID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 AdmissionSelectionID
        {
            get { return _AdmissionSelectionID; }
            set { _AdmissionSelectionID = value; }
        }
        [Column(Name = "SchoolPeriodID", DataType = "Int32")]
        public Int32 SchoolPeriodID
        {
            get { return _SchoolPeriodID; }
            set { _SchoolPeriodID = value; }
        }
        [Column(Name = "PeriodAdmissionID", DataType = "Int32", IsNullable = true)]
        public Int32? PeriodAdmissionID
        {
            get { return _PeriodAdmissionID; }
            set { _PeriodAdmissionID = value; }
        }
        [Column(Name = "SelectionName", DataType = "String")]
        public String SelectionName
        {
            get { return _SelectionName; }
            set { _SelectionName = value; }
        }
        [Column(Name = "DisplayOrder", DataType = "Int16")]
        public Int16 DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        [Column(Name = "FinalMarkPercentage", DataType = "Int16")]
        public Int16 FinalMarkPercentage
        {
            get { return _FinalMarkPercentage; }
            set { _FinalMarkPercentage = value; }
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

    public class AdmissionSelectionDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(AdmissionSelection));
        private bool _isAuditLog = false;
        private const string p_AdmissionSelectionID = "@p_AdmissionSelectionID";
        public AdmissionSelectionDao() { }
        public AdmissionSelectionDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public AdmissionSelection Get(Int32 AdmissionSelectionID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_AdmissionSelectionID, AdmissionSelectionID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (AdmissionSelection)_helper.DataRowToObject(row, new AdmissionSelection());
        }
        public int Insert(AdmissionSelection record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(AdmissionSelection record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 AdmissionSelectionID)
        {
            AdmissionSelection record;
            if (_ctx.Transaction == null)
                record = new AdmissionSelectionDao().Get(AdmissionSelectionID);
            else
                record = Get(AdmissionSelectionID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Bank
    [Serializable]
    [Table(Name = "Bank")]
    public class Bank : DbDataModel
    {
        private Int32 _BankID;
        private String _BankCode;
        private String _BankName;
        private String _BankAccountNo;
        private String _BankAccountName;
        private String _SiteID;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "BankID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 BankID
        {
            get { return _BankID; }
            set { _BankID = value; }
        }
        [Column(Name = "BankCode", DataType = "String")]
        public String BankCode
        {
            get { return _BankCode; }
            set { _BankCode = value; }
        }
        [Column(Name = "BankName", DataType = "String")]
        public String BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }
        [Column(Name = "BankAccountNo", DataType = "String")]
        public String BankAccountNo
        {
            get { return _BankAccountNo; }
            set { _BankAccountNo = value; }
        }
        [Column(Name = "BankAccountName", DataType = "String")]
        public String BankAccountName
        {
            get { return _BankAccountName; }
            set { _BankAccountName = value; }
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

    public class BankDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Bank));
        private bool _isAuditLog = false;
        private const string p_BankID = "@p_BankID";
        public BankDao() { }
        public BankDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Bank Get(Int32 BankID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_BankID, BankID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Bank)_helper.DataRowToObject(row, new Bank());
        }
        public int Insert(Bank record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Bank record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 BankID)
        {
            Bank record;
            if (_ctx.Transaction == null)
                record = new BankDao().Get(BankID);
            else
                record = Get(BankID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
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
    #region BusinessPartnerTagField
    [Serializable]
    [Table(Name = "BusinessPartnerTagField")]
    public class BusinessPartnerTagField : DbDataModel
    {
        private Int32 _BusinessPartnerID;
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

        [Column(Name = "BusinessPartnerID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
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

    public class BusinessPartnerTagFieldDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(BusinessPartnerTagField));
        private bool _isAuditLog = false;
        private const string p_BusinessPartnerID = "@p_BusinessPartnerID";
        public BusinessPartnerTagFieldDao() { }
        public BusinessPartnerTagFieldDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public BusinessPartnerTagField Get(Int32 BusinessPartnerID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_BusinessPartnerID, BusinessPartnerID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (BusinessPartnerTagField)_helper.DataRowToObject(row, new BusinessPartnerTagField());
        }
        public int Insert(BusinessPartnerTagField record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(BusinessPartnerTagField record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 BusinessPartnerID)
        {
            BusinessPartnerTagField record;
            if (_ctx.Transaction == null)
                record = new BusinessPartnerTagFieldDao().Get(BusinessPartnerID);
            else
                record = Get(BusinessPartnerID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ChartOfAccount
    [Serializable]
    [Table(Name = "ChartOfAccount")]
    public class ChartOfAccount : DbDataModel
    {
        private Int32 _GLAccountID;
        private String _SiteID;
        private String _GLAccountNo;
        private String _GLAccountName;
        private Int32? _ParentGLAccount;
        private String _GCGLAccountType;
        private Int32? _SubLedgerID;
        private String _Position;
        private Boolean _IsHeader;
        private Int16 _AccountLevel;
        private Boolean _IsUsingDocumentControl;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "GLAccountID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 GLAccountID
        {
            get { return _GLAccountID; }
            set { _GLAccountID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "GLAccountNo", DataType = "String")]
        public String GLAccountNo
        {
            get { return _GLAccountNo; }
            set { _GLAccountNo = value; }
        }
        [Column(Name = "GLAccountName", DataType = "String")]
        public String GLAccountName
        {
            get { return _GLAccountName; }
            set { _GLAccountName = value; }
        }
        [Column(Name = "ParentGLAccount", DataType = "Int32", IsNullable = true)]
        public Int32? ParentGLAccount
        {
            get { return _ParentGLAccount; }
            set { _ParentGLAccount = value; }
        }
        [Column(Name = "GCGLAccountType", DataType = "String", IsNullable = true)]
        public String GCGLAccountType
        {
            get { return _GCGLAccountType; }
            set { _GCGLAccountType = value; }
        }
        [Column(Name = "SubLedgerID", DataType = "Int32", IsNullable = true)]
        public Int32? SubLedgerID
        {
            get { return _SubLedgerID; }
            set { _SubLedgerID = value; }
        }
        [Column(Name = "Position", DataType = "String")]
        public String Position
        {
            get { return _Position; }
            set { _Position = value; }
        }
        [Column(Name = "IsHeader", DataType = "Boolean")]
        public Boolean IsHeader
        {
            get { return _IsHeader; }
            set { _IsHeader = value; }
        }
        [Column(Name = "AccountLevel", DataType = "Int16")]
        public Int16 AccountLevel
        {
            get { return _AccountLevel; }
            set { _AccountLevel = value; }
        }
        [Column(Name = "IsUsingDocumentControl", DataType = "Boolean")]
        public Boolean IsUsingDocumentControl
        {
            get { return _IsUsingDocumentControl; }
            set { _IsUsingDocumentControl = value; }
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

    public class ChartOfAccountDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ChartOfAccount));
        private bool _isAuditLog = false;
        private const string p_GLAccountID = "@p_GLAccountID";
        public ChartOfAccountDao() { }
        public ChartOfAccountDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ChartOfAccount Get(Int32 GLAccountID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_GLAccountID, GLAccountID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ChartOfAccount)_helper.DataRowToObject(row, new ChartOfAccount());
        }
        public int Insert(ChartOfAccount record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ChartOfAccount record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 GLAccountID)
        {
            ChartOfAccount record;
            if (_ctx.Transaction == null)
                record = new ChartOfAccountDao().Get(GLAccountID);
            else
                record = Get(GLAccountID);
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
    #region ClassMeetingAttendance
    [Serializable]
    [Table(Name = "ClassMeetingAttendance")]
    public class ClassMeetingAttendance : DbDataModel
    {
        private Int32 _ClassMeetingID;
        private Int32 _StudentID;
        private String _GCAttendanceStatus;

        [Column(Name = "ClassMeetingID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 ClassMeetingID
        {
            get { return _ClassMeetingID; }
            set { _ClassMeetingID = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "GCAttendanceStatus", DataType = "String")]
        public String GCAttendanceStatus
        {
            get { return _GCAttendanceStatus; }
            set { _GCAttendanceStatus = value; }
        }
    }

    public class ClassMeetingAttendanceDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ClassMeetingAttendance));
        private bool _isAuditLog = false;
        private const string p_ClassMeetingID = "@p_ClassMeetingID";
        private const string p_StudentID = "@p_StudentID";
        public ClassMeetingAttendanceDao() { }
        public ClassMeetingAttendanceDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ClassMeetingAttendance Get(Int32 ClassMeetingID, Int32 StudentID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ClassMeetingID, ClassMeetingID);
            _ctx.Add(p_StudentID, StudentID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ClassMeetingAttendance)_helper.DataRowToObject(row, new ClassMeetingAttendance());
        }
        public int Insert(ClassMeetingAttendance record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ClassMeetingAttendance record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ClassMeetingID, Int32 StudentID)
        {
            ClassMeetingAttendance record;
            if (_ctx.Transaction == null)
                record = new ClassMeetingAttendanceDao().Get(ClassMeetingID, StudentID);
            else
                record = Get(ClassMeetingID, StudentID);
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
    #region ClassStudentSubjectMark
    [Serializable]
    [Table(Name = "ClassStudentSubjectMark")]
    public class ClassStudentSubjectMark : DbDataModel
    {
        private Int32 _ClassSubjectTaskID;
        private Int32 _StudentID;
        private Decimal _Mark;

        [Column(Name = "ClassSubjectTaskID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 ClassSubjectTaskID
        {
            get { return _ClassSubjectTaskID; }
            set { _ClassSubjectTaskID = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "Mark", DataType = "Decimal")]
        public Decimal Mark
        {
            get { return _Mark; }
            set { _Mark = value; }
        }
    }

    public class ClassStudentSubjectMarkDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ClassStudentSubjectMark));
        private bool _isAuditLog = false;
        private const string p_ClassSubjectTaskID = "@p_ClassSubjectTaskID";
        private const string p_StudentID = "@p_StudentID";
        public ClassStudentSubjectMarkDao() { }
        public ClassStudentSubjectMarkDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ClassStudentSubjectMark Get(Int32 ClassSubjectTaskID, Int32 StudentID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ClassSubjectTaskID, ClassSubjectTaskID);
            _ctx.Add(p_StudentID, StudentID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ClassStudentSubjectMark)_helper.DataRowToObject(row, new ClassStudentSubjectMark());
        }
        public int Insert(ClassStudentSubjectMark record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ClassStudentSubjectMark record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ClassSubjectTaskID, Int32 StudentID)
        {
            ClassStudentSubjectMark record;
            if (_ctx.Transaction == null)
                record = new ClassStudentSubjectMarkDao().Get(ClassSubjectTaskID, StudentID);
            else
                record = Get(ClassSubjectTaskID, StudentID);
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
    #region ClassSubjectTask
    [Serializable]
    [Table(Name = "ClassSubjectTask")]
    public class ClassSubjectTask : DbDataModel
    {
        private Int32 _ClassSubjectTaskID;
        private Int32 _ClassSubjectID;
        private String _ClassTaskCode;
        private String _GCTaskType;
        private Int16 _FinalMarkPercentage;
        private DateTime _TaskDate;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private String _StartTime;
        private String _EndTime;
        private String _Topic;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ClassSubjectTaskID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "ClassTaskCode", DataType = "String")]
        public String ClassTaskCode
        {
            get { return _ClassTaskCode; }
            set { _ClassTaskCode = value; }
        }
        [Column(Name = "GCTaskType", DataType = "String")]
        public String GCTaskType
        {
            get { return _GCTaskType; }
            set { _GCTaskType = value; }
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

    public class ClassSubjectTaskDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ClassSubjectTask));
        private bool _isAuditLog = false;
        private const string p_ClassSubjectTaskID = "@p_ClassSubjectTaskID";
        public ClassSubjectTaskDao() { }
        public ClassSubjectTaskDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ClassSubjectTask Get(Int32 ClassSubjectTaskID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ClassSubjectTaskID, ClassSubjectTaskID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ClassSubjectTask)_helper.DataRowToObject(row, new ClassSubjectTask());
        }
        public int Insert(ClassSubjectTask record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ClassSubjectTask record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ClassSubjectTaskID)
        {
            ClassSubjectTask record;
            if (_ctx.Transaction == null)
                record = new ClassSubjectTaskDao().Get(ClassSubjectTaskID);
            else
                record = Get(ClassSubjectTaskID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ClassType
    [Serializable]
    [Table(Name = "ClassType")]
    public class ClassType : DbDataModel
    {
        private Int32 _ClassTypeID;
        private String _ClassTypeCode;
        private String _ClassTypeName;
        private String _SiteID;
        private String _GCGrade;
        private String _GCMajor;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ClassTypeID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "GCMajor", DataType = "String", IsNullable = true)]
        public String GCMajor
        {
            get { return _GCMajor; }
            set { _GCMajor = value; }
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

    public class ClassTypeDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ClassType));
        private bool _isAuditLog = false;
        private const string p_ClassTypeID = "@p_ClassTypeID";
        public ClassTypeDao() { }
        public ClassTypeDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ClassType Get(Int32 ClassTypeID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ClassTypeID, ClassTypeID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ClassType)_helper.DataRowToObject(row, new ClassType());
        }
        public int Insert(ClassType record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ClassType record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ClassTypeID)
        {
            ClassType record;
            if (_ctx.Transaction == null)
                record = new ClassTypeDao().Get(ClassTypeID);
            else
                record = Get(ClassTypeID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region COABudget
    [Serializable]
    [Table(Name = "COABudget")]
    public class COABudget : DbDataModel
    {
        private Int32 _ID;
        private String _PeriodNo;
        private Int32 _GLAccount;
        private Decimal _BudgjetAmount;
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
        [Column(Name = "PeriodNo", DataType = "String")]
        public String PeriodNo
        {
            get { return _PeriodNo; }
            set { _PeriodNo = value; }
        }
        [Column(Name = "GLAccount", DataType = "Int32")]
        public Int32 GLAccount
        {
            get { return _GLAccount; }
            set { _GLAccount = value; }
        }
        [Column(Name = "BudgjetAmount", DataType = "Decimal")]
        public Decimal BudgjetAmount
        {
            get { return _BudgjetAmount; }
            set { _BudgjetAmount = value; }
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

    public class COABudgetDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(COABudget));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public COABudgetDao() { }
        public COABudgetDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public COABudget Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (COABudget)_helper.DataRowToObject(row, new COABudget());
        }
        public int Insert(COABudget record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(COABudget record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            COABudget record;
            if (_ctx.Transaction == null)
                record = new COABudgetDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region CreditCard
    [Serializable]
    [Table(Name = "CreditCard")]
    public class CreditCard : DbDataModel
    {
        private Int32 _CreditCardID;
        private String _SiteID;
        private String _GCCardType;
        private String _GCCardProvider;
        private Int32 _EDCMachineID;
        private Decimal _CreditCardFee;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "CreditCardID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 CreditCardID
        {
            get { return _CreditCardID; }
            set { _CreditCardID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "GCCardType", DataType = "String")]
        public String GCCardType
        {
            get { return _GCCardType; }
            set { _GCCardType = value; }
        }
        [Column(Name = "GCCardProvider", DataType = "String")]
        public String GCCardProvider
        {
            get { return _GCCardProvider; }
            set { _GCCardProvider = value; }
        }
        [Column(Name = "EDCMachineID", DataType = "Int32")]
        public Int32 EDCMachineID
        {
            get { return _EDCMachineID; }
            set { _EDCMachineID = value; }
        }
        [Column(Name = "CreditCardFee", DataType = "Decimal")]
        public Decimal CreditCardFee
        {
            get { return _CreditCardFee; }
            set { _CreditCardFee = value; }
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

    public class CreditCardDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(CreditCard));
        private bool _isAuditLog = false;
        private const string p_CreditCardID = "@p_CreditCardID";
        public CreditCardDao() { }
        public CreditCardDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public CreditCard Get(Int32 CreditCardID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_CreditCardID, CreditCardID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (CreditCard)_helper.DataRowToObject(row, new CreditCard());
        }
        public int Insert(CreditCard record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(CreditCard record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 CreditCardID)
        {
            CreditCard record;
            if (_ctx.Transaction == null)
                record = new CreditCardDao().Get(CreditCardID);
            else
                record = Get(CreditCardID);
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
    #region EDCMachine
    [Serializable]
    [Table(Name = "EDCMachine")]
    public class EDCMachine : DbDataModel
    {
        private Int32 _EDCMachineID;
        private String _EDCMachineCode;
        private String _EDCMachineName;
        private String _GCCardProvider;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "EDCMachineID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 EDCMachineID
        {
            get { return _EDCMachineID; }
            set { _EDCMachineID = value; }
        }
        [Column(Name = "EDCMachineCode", DataType = "String")]
        public String EDCMachineCode
        {
            get { return _EDCMachineCode; }
            set { _EDCMachineCode = value; }
        }
        [Column(Name = "EDCMachineName", DataType = "String")]
        public String EDCMachineName
        {
            get { return _EDCMachineName; }
            set { _EDCMachineName = value; }
        }
        [Column(Name = "GCCardProvider", DataType = "String")]
        public String GCCardProvider
        {
            get { return _GCCardProvider; }
            set { _GCCardProvider = value; }
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

    public class EDCMachineDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(EDCMachine));
        private bool _isAuditLog = false;
        private const string p_EDCMachineID = "@p_EDCMachineID";
        public EDCMachineDao() { }
        public EDCMachineDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public EDCMachine Get(Int32 EDCMachineID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_EDCMachineID, EDCMachineID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (EDCMachine)_helper.DataRowToObject(row, new EDCMachine());
        }
        public int Insert(EDCMachine record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(EDCMachine record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 EDCMachineID)
        {
            EDCMachine record;
            if (_ctx.Transaction == null)
                record = new EDCMachineDao().Get(EDCMachineID);
            else
                record = Get(EDCMachineID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region FADepreciation
    [Serializable]
    [Table(Name = "FADepreciation")]
    public partial class FADepreciation : DbDataModel
    {
        private Int32 _FADepreciationID;
        private Int32 _FixedAssetID;
        private String _PeriodNo;
        private DateTime _DepreciationDate;
        private Decimal _AssetValue;
        private Decimal _DepreciationAmount;
        private Decimal _TotalDepreciationAmount;
        private Int32? _GLJournalID;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "FADepreciationID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 FADepreciationID
        {
            get { return _FADepreciationID; }
            set { _FADepreciationID = value; }
        }
        [Column(Name = "FixedAssetID", DataType = "Int32")]
        public Int32 FixedAssetID
        {
            get { return _FixedAssetID; }
            set { _FixedAssetID = value; }
        }
        [Column(Name = "PeriodNo", DataType = "String")]
        public String PeriodNo
        {
            get { return _PeriodNo; }
            set { _PeriodNo = value; }
        }
        [Column(Name = "DepreciationDate", DataType = "DateTime")]
        public DateTime DepreciationDate
        {
            get { return _DepreciationDate; }
            set { _DepreciationDate = value; }
        }
        [Column(Name = "AssetValue", DataType = "Decimal")]
        public Decimal AssetValue
        {
            get { return _AssetValue; }
            set { _AssetValue = value; }
        }
        [Column(Name = "DepreciationAmount", DataType = "Decimal")]
        public Decimal DepreciationAmount
        {
            get { return _DepreciationAmount; }
            set { _DepreciationAmount = value; }
        }
        [Column(Name = "TotalDepreciationAmount", DataType = "Decimal")]
        public Decimal TotalDepreciationAmount
        {
            get { return _TotalDepreciationAmount; }
            set { _TotalDepreciationAmount = value; }
        }
        [Column(Name = "GLJournalID", DataType = "Int32", IsNullable = true)]
        public Int32? GLJournalID
        {
            get { return _GLJournalID; }
            set { _GLJournalID = value; }
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

    public class FADepreciationDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(FADepreciation));
        private bool _isAuditLog = false;
        private const string p_FADepreciationID = "@p_FADepreciationID";
        public FADepreciationDao() { }
        public FADepreciationDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public FADepreciation Get(Int32 FADepreciationID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_FADepreciationID, FADepreciationID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (FADepreciation)_helper.DataRowToObject(row, new FADepreciation());
        }
        public int Insert(FADepreciation record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(FADepreciation record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 FADepreciationID)
        {
            FADepreciation record;
            if (_ctx.Transaction == null)
                record = new FADepreciationDao().Get(FADepreciationID);
            else
                record = Get(FADepreciationID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region FADepreciationMethod
    [Serializable]
    [Table(Name = "FADepreciationMethod")]
    public class FADepreciationMethod : DbDataModel
    {
        private Int32 _MethodID;
        private String _MethodCode;
        private String _MethodName;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "MethodID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 MethodID
        {
            get { return _MethodID; }
            set { _MethodID = value; }
        }
        [Column(Name = "MethodCode", DataType = "String")]
        public String MethodCode
        {
            get { return _MethodCode; }
            set { _MethodCode = value; }
        }
        [Column(Name = "MethodName", DataType = "String")]
        public String MethodName
        {
            get { return _MethodName; }
            set { _MethodName = value; }
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

    public class FADepreciationMethodDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(FADepreciationMethod));
        private bool _isAuditLog = false;
        private const string p_MethodID = "@p_MethodID";
        public FADepreciationMethodDao() { }
        public FADepreciationMethodDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public FADepreciationMethod Get(Int32 MethodID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_MethodID, MethodID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (FADepreciationMethod)_helper.DataRowToObject(row, new FADepreciationMethod());
        }
        public int Insert(FADepreciationMethod record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(FADepreciationMethod record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 MethodID)
        {
            FADepreciationMethod record;
            if (_ctx.Transaction == null)
                record = new FADepreciationMethodDao().Get(MethodID);
            else
                record = Get(MethodID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region FAGroup
    [Serializable]
    [Table(Name = "FAGroup")]
    public class FAGroup : DbDataModel
    {
        private Int32 _FAGroupID;
        private String _FAGroupCode;
        private String _FAGroupName;
        private Int32 _MethodID;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "FAGroupID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 FAGroupID
        {
            get { return _FAGroupID; }
            set { _FAGroupID = value; }
        }
        [Column(Name = "FAGroupCode", DataType = "String")]
        public String FAGroupCode
        {
            get { return _FAGroupCode; }
            set { _FAGroupCode = value; }
        }
        [Column(Name = "FAGroupName", DataType = "String")]
        public String FAGroupName
        {
            get { return _FAGroupName; }
            set { _FAGroupName = value; }
        }
        [Column(Name = "MethodID", DataType = "Int32")]
        public Int32 MethodID
        {
            get { return _MethodID; }
            set { _MethodID = value; }
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

    public class FAGroupDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(FAGroup));
        private bool _isAuditLog = false;
        private const string p_FAGroupID = "@p_FAGroupID";
        public FAGroupDao() { }
        public FAGroupDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public FAGroup Get(Int32 FAGroupID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_FAGroupID, FAGroupID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (FAGroup)_helper.DataRowToObject(row, new FAGroup());
        }
        public int Insert(FAGroup record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(FAGroup record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 FAGroupID)
        {
            FAGroup record;
            if (_ctx.Transaction == null)
                record = new FAGroupDao().Get(FAGroupID);
            else
                record = Get(FAGroupID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region FAGroupCOA
    [Serializable]
    [Table(Name = "FAGroupCOA")]
    public class FAGroupCOA : DbDataModel
    {
        private String _SiteID;
        private Int32 _FAGroupID;
        private Int32? _GLAccount1;
        private Int32? _GLAccount2;
        private Int32? _GLAccount3;
        private Int32? _GLAccount4;
        private Int32? _GLAccount5;
        private Int32? _GLAccount6;
        private Int32? _SubLedger1;
        private Int32? _SubLedger2;
        private Int32? _SubLedger3;
        private Int32? _SubLedger4;
        private Int32? _SubLedger5;
        private Int32? _SubLedger6;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "SiteID", DataType = "String", IsPrimaryKey = true)]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "FAGroupID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 FAGroupID
        {
            get { return _FAGroupID; }
            set { _FAGroupID = value; }
        }
        [Column(Name = "GLAccount1", DataType = "Int32", IsNullable = true)]
        public Int32? GLAccount1
        {
            get { return _GLAccount1; }
            set { _GLAccount1 = value; }
        }
        [Column(Name = "GLAccount2", DataType = "Int32", IsNullable = true)]
        public Int32? GLAccount2
        {
            get { return _GLAccount2; }
            set { _GLAccount2 = value; }
        }
        [Column(Name = "GLAccount3", DataType = "Int32", IsNullable = true)]
        public Int32? GLAccount3
        {
            get { return _GLAccount3; }
            set { _GLAccount3 = value; }
        }
        [Column(Name = "GLAccount4", DataType = "Int32", IsNullable = true)]
        public Int32? GLAccount4
        {
            get { return _GLAccount4; }
            set { _GLAccount4 = value; }
        }
        [Column(Name = "GLAccount5", DataType = "Int32", IsNullable = true)]
        public Int32? GLAccount5
        {
            get { return _GLAccount5; }
            set { _GLAccount5 = value; }
        }
        [Column(Name = "GLAccount6", DataType = "Int32", IsNullable = true)]
        public Int32? GLAccount6
        {
            get { return _GLAccount6; }
            set { _GLAccount6 = value; }
        }
        [Column(Name = "SubLedger1", DataType = "Int32", IsNullable = true)]
        public Int32? SubLedger1
        {
            get { return _SubLedger1; }
            set { _SubLedger1 = value; }
        }
        [Column(Name = "SubLedger2", DataType = "Int32", IsNullable = true)]
        public Int32? SubLedger2
        {
            get { return _SubLedger2; }
            set { _SubLedger2 = value; }
        }
        [Column(Name = "SubLedger3", DataType = "Int32", IsNullable = true)]
        public Int32? SubLedger3
        {
            get { return _SubLedger3; }
            set { _SubLedger3 = value; }
        }
        [Column(Name = "SubLedger4", DataType = "Int32", IsNullable = true)]
        public Int32? SubLedger4
        {
            get { return _SubLedger4; }
            set { _SubLedger4 = value; }
        }
        [Column(Name = "SubLedger5", DataType = "Int32", IsNullable = true)]
        public Int32? SubLedger5
        {
            get { return _SubLedger5; }
            set { _SubLedger5 = value; }
        }
        [Column(Name = "SubLedger6", DataType = "Int32", IsNullable = true)]
        public Int32? SubLedger6
        {
            get { return _SubLedger6; }
            set { _SubLedger6 = value; }
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

    public class FAGroupCOADao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(FAGroupCOA));
        private bool _isAuditLog = false;
        private const string p_FAGroupID = "@p_FAGroupID";
        private const string p_SiteID = "@p_SiteID";
        public FAGroupCOADao() { }
        public FAGroupCOADao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public FAGroupCOA Get(String SiteID, Int32 FAGroupID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_FAGroupID, FAGroupID);
            _ctx.Add(p_SiteID, SiteID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (FAGroupCOA)_helper.DataRowToObject(row, new FAGroupCOA());
        }
        public int Insert(FAGroupCOA record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(FAGroupCOA record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(String SiteID, Int32 FAGroupID)
        {
            FAGroupCOA record;
            if (_ctx.Transaction == null)
                record = new FAGroupCOADao().Get(SiteID, FAGroupID);
            else
                record = Get(SiteID, FAGroupID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region FAItem
    [Serializable]
    [Table(Name = "FAItem")]
    public class FAItem : DbDataModel
    {
        private Int32 _FixedAssetID;
        private String _FixedAssetCode;
        private String _FixedAssetName;
        private String _SiteID;
        private Int32 _FAGroupID;
        private Int32 _FALocationID;
        private Int32 _MethodID;
        private Int32? _ItemID;
        private String _SerialNumber;
        private Boolean _IsContractItem;
        private Int32? _BusinessPartnerID;
        private String _BusinessPartnerName;
        private String _ContractNumber;
        private Int32? _PurchaseReceiveID;
        private String _ProcurementNumber;
        private DateTime _ProcurementDate;
        private Decimal _ProcurementAmount;
        private Decimal _ProcurementQuantity;
        private String _GCProcurementUnit;
        private DateTime _DepreciationStartDate;
        private Int16 _DepreciationLength;
        private Decimal _AssetFinalValue;
        private String _Remarks;
        private String _GCItemStatus;
        private Boolean _IsDeleted;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "FixedAssetID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 FixedAssetID
        {
            get { return _FixedAssetID; }
            set { _FixedAssetID = value; }
        }
        [Column(Name = "FixedAssetCode", DataType = "String")]
        public String FixedAssetCode
        {
            get { return _FixedAssetCode; }
            set { _FixedAssetCode = value; }
        }
        [Column(Name = "FixedAssetName", DataType = "String")]
        public String FixedAssetName
        {
            get { return _FixedAssetName; }
            set { _FixedAssetName = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "FAGroupID", DataType = "Int32")]
        public Int32 FAGroupID
        {
            get { return _FAGroupID; }
            set { _FAGroupID = value; }
        }
        [Column(Name = "FALocationID", DataType = "Int32")]
        public Int32 FALocationID
        {
            get { return _FALocationID; }
            set { _FALocationID = value; }
        }
        [Column(Name = "MethodID", DataType = "Int32")]
        public Int32 MethodID
        {
            get { return _MethodID; }
            set { _MethodID = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32", IsNullable = true)]
        public Int32? ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "SerialNumber", DataType = "String", IsNullable = true)]
        public String SerialNumber
        {
            get { return _SerialNumber; }
            set { _SerialNumber = value; }
        }
        [Column(Name = "IsContractItem", DataType = "Boolean")]
        public Boolean IsContractItem
        {
            get { return _IsContractItem; }
            set { _IsContractItem = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32", IsNullable = true)]
        public Int32? BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "BusinessPartnerName", DataType = "String", IsNullable = true)]
        public String BusinessPartnerName
        {
            get { return _BusinessPartnerName; }
            set { _BusinessPartnerName = value; }
        }
        [Column(Name = "ContractNumber", DataType = "String", IsNullable = true)]
        public String ContractNumber
        {
            get { return _ContractNumber; }
            set { _ContractNumber = value; }
        }
        [Column(Name = "PurchaseReceiveID", DataType = "Int32", IsNullable = true)]
        public Int32? PurchaseReceiveID
        {
            get { return _PurchaseReceiveID; }
            set { _PurchaseReceiveID = value; }
        }
        [Column(Name = "ProcurementNumber", DataType = "String", IsNullable = true)]
        public String ProcurementNumber
        {
            get { return _ProcurementNumber; }
            set { _ProcurementNumber = value; }
        }
        [Column(Name = "ProcurementDate", DataType = "DateTime", IsNullable = true)]
        public DateTime ProcurementDate
        {
            get { return _ProcurementDate; }
            set { _ProcurementDate = value; }
        }
        [Column(Name = "ProcurementAmount", DataType = "Decimal", IsNullable = true)]
        public Decimal ProcurementAmount
        {
            get { return _ProcurementAmount; }
            set { _ProcurementAmount = value; }
        }
        [Column(Name = "ProcurementQuantity", DataType = "Decimal", IsNullable = true)]
        public Decimal ProcurementQuantity
        {
            get { return _ProcurementQuantity; }
            set { _ProcurementQuantity = value; }
        }
        [Column(Name = "GCProcurementUnit", DataType = "String", IsNullable = true)]
        public String GCProcurementUnit
        {
            get { return _GCProcurementUnit; }
            set { _GCProcurementUnit = value; }
        }
        [Column(Name = "DepreciationStartDate", DataType = "DateTime", IsNullable = true)]
        public DateTime DepreciationStartDate
        {
            get { return _DepreciationStartDate; }
            set { _DepreciationStartDate = value; }
        }
        [Column(Name = "DepreciationLength", DataType = "Int16")]
        public Int16 DepreciationLength
        {
            get { return _DepreciationLength; }
            set { _DepreciationLength = value; }
        }
        [Column(Name = "AssetFinalValue", DataType = "Decimal")]
        public Decimal AssetFinalValue
        {
            get { return _AssetFinalValue; }
            set { _AssetFinalValue = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCItemStatus", DataType = "String")]
        public String GCItemStatus
        {
            get { return _GCItemStatus; }
            set { _GCItemStatus = value; }
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

    public class FAItemDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(FAItem));
        private bool _isAuditLog = false;
        private const string p_FixedAssetID = "@p_FixedAssetID";
        public FAItemDao() { }
        public FAItemDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public FAItem Get(Int32 FixedAssetID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_FixedAssetID, FixedAssetID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (FAItem)_helper.DataRowToObject(row, new FAItem());
        }
        public int Insert(FAItem record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(FAItem record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 FixedAssetID)
        {
            FAItem record;
            if (_ctx.Transaction == null)
                record = new FAItemDao().Get(FixedAssetID);
            else
                record = Get(FixedAssetID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region FAItemCOA
    [Serializable]
    [Table(Name = "FAItemCOA")]
    public class FAItemCOA : DbDataModel
    {
        private String _SiteID;
        private Int32 _FixedAssetID;
        private Int32? _GLAccount1;
        private Int32? _GLAccount2;
        private Int32? _GLAccount3;
        private Int32? _GLAccount4;
        private Int32? _GLAccount5;
        private Int32? _GLAccount6;
        private Int32? _SubLedger1;
        private Int32? _SubLedger2;
        private Int32? _SubLedger3;
        private Int32? _SubLedger4;
        private Int32? _SubLedger5;
        private Int32? _SubLedger6;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "SiteID", DataType = "String", IsPrimaryKey = true)]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "FixedAssetID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 FixedAssetID
        {
            get { return _FixedAssetID; }
            set { _FixedAssetID = value; }
        }
        [Column(Name = "GLAccount1", DataType = "Int32", IsNullable = true)]
        public Int32? GLAccount1
        {
            get { return _GLAccount1; }
            set { _GLAccount1 = value; }
        }
        [Column(Name = "GLAccount2", DataType = "Int32", IsNullable = true)]
        public Int32? GLAccount2
        {
            get { return _GLAccount2; }
            set { _GLAccount2 = value; }
        }
        [Column(Name = "GLAccount3", DataType = "Int32", IsNullable = true)]
        public Int32? GLAccount3
        {
            get { return _GLAccount3; }
            set { _GLAccount3 = value; }
        }
        [Column(Name = "GLAccount4", DataType = "Int32", IsNullable = true)]
        public Int32? GLAccount4
        {
            get { return _GLAccount4; }
            set { _GLAccount4 = value; }
        }
        [Column(Name = "GLAccount5", DataType = "Int32", IsNullable = true)]
        public Int32? GLAccount5
        {
            get { return _GLAccount5; }
            set { _GLAccount5 = value; }
        }
        [Column(Name = "GLAccount6", DataType = "Int32", IsNullable = true)]
        public Int32? GLAccount6
        {
            get { return _GLAccount6; }
            set { _GLAccount6 = value; }
        }
        [Column(Name = "SubLedger1", DataType = "Int32", IsNullable = true)]
        public Int32? SubLedger1
        {
            get { return _SubLedger1; }
            set { _SubLedger1 = value; }
        }
        [Column(Name = "SubLedger2", DataType = "Int32", IsNullable = true)]
        public Int32? SubLedger2
        {
            get { return _SubLedger2; }
            set { _SubLedger2 = value; }
        }
        [Column(Name = "SubLedger3", DataType = "Int32", IsNullable = true)]
        public Int32? SubLedger3
        {
            get { return _SubLedger3; }
            set { _SubLedger3 = value; }
        }
        [Column(Name = "SubLedger4", DataType = "Int32", IsNullable = true)]
        public Int32? SubLedger4
        {
            get { return _SubLedger4; }
            set { _SubLedger4 = value; }
        }
        [Column(Name = "SubLedger5", DataType = "Int32", IsNullable = true)]
        public Int32? SubLedger5
        {
            get { return _SubLedger5; }
            set { _SubLedger5 = value; }
        }
        [Column(Name = "SubLedger6", DataType = "Int32", IsNullable = true)]
        public Int32? SubLedger6
        {
            get { return _SubLedger6; }
            set { _SubLedger6 = value; }
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

    public class FAItemCOADao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(FAItemCOA));
        private bool _isAuditLog = false;
        private const string p_FixedAssetID = "@p_FixedAssetID";
        private const string p_SiteID = "@p_SiteID";
        public FAItemCOADao() { }
        public FAItemCOADao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public FAItemCOA Get(String SiteID, Int32 FixedAssetID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_FixedAssetID, FixedAssetID);
            _ctx.Add(p_SiteID, SiteID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (FAItemCOA)_helper.DataRowToObject(row, new FAItemCOA());
        }
        public int Insert(FAItemCOA record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(FAItemCOA record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(String SiteID, Int32 FixedAssetID)
        {
            FAItemCOA record;
            if (_ctx.Transaction == null)
                record = new FAItemCOADao().Get(SiteID, FixedAssetID);
            else
                record = Get(SiteID, FixedAssetID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region FAItemMovement
    [Serializable]
    [Table(Name = "FAItemMovement")]
    public class FAItemMovement : DbDataModel
    {
        private Int32 _MovementID;
        private String _MovementNo;
        private DateTime _MovementDate;
        private Int32 _FixedAssetID;
        private Int32 _FromFALocationID;
        private Int32 _ToFALocationID;
        private String _Remarks;
        private String _GCTransactionStatus;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "MovementID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 MovementID
        {
            get { return _MovementID; }
            set { _MovementID = value; }
        }
        [Column(Name = "MovementNo", DataType = "String")]
        public String MovementNo
        {
            get { return _MovementNo; }
            set { _MovementNo = value; }
        }
        [Column(Name = "MovementDate", DataType = "DateTime")]
        public DateTime MovementDate
        {
            get { return _MovementDate; }
            set { _MovementDate = value; }
        }
        [Column(Name = "FixedAssetID", DataType = "Int32")]
        public Int32 FixedAssetID
        {
            get { return _FixedAssetID; }
            set { _FixedAssetID = value; }
        }
        [Column(Name = "FromFALocationID", DataType = "Int32")]
        public Int32 FromFALocationID
        {
            get { return _FromFALocationID; }
            set { _FromFALocationID = value; }
        }
        [Column(Name = "ToFALocationID", DataType = "Int32")]
        public Int32 ToFALocationID
        {
            get { return _ToFALocationID; }
            set { _ToFALocationID = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String", IsNullable = true)]
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

    public class FAItemMovementDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(FAItemMovement));
        private bool _isAuditLog = false;
        private const string p_MovementID = "@p_MovementID";
        public FAItemMovementDao() { }
        public FAItemMovementDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public FAItemMovement Get(Int32 MovementID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_MovementID, MovementID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (FAItemMovement)_helper.DataRowToObject(row, new FAItemMovement());
        }
        public int Insert(FAItemMovement record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(FAItemMovement record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 MovementID)
        {
            FAItemMovement record;
            if (_ctx.Transaction == null)
                record = new FAItemMovementDao().Get(MovementID);
            else
                record = Get(MovementID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region FALocation
    [Serializable]
    [Table(Name = "FALocation")]
    public class FALocation : DbDataModel
    {
        private Int32 _FALocationID;
        private String _SiteID;
        private String _FALocationCode;
        private String _FALocationName;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "FALocationID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 FALocationID
        {
            get { return _FALocationID; }
            set { _FALocationID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "FALocationCode", DataType = "String")]
        public String FALocationCode
        {
            get { return _FALocationCode; }
            set { _FALocationCode = value; }
        }
        [Column(Name = "FALocationName", DataType = "String")]
        public String FALocationName
        {
            get { return _FALocationName; }
            set { _FALocationName = value; }
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

    public class FALocationDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(FALocation));
        private bool _isAuditLog = false;
        private const string p_FALocationID = "@p_FALocationID";
        public FALocationDao() { }
        public FALocationDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public FALocation Get(Int32 FALocationID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_FALocationID, FALocationID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (FALocation)_helper.DataRowToObject(row, new FALocation());
        }
        public int Insert(FALocation record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(FALocation record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 FALocationID)
        {
            FALocation record;
            if (_ctx.Transaction == null)
                record = new FALocationDao().Get(FALocationID);
            else
                record = Get(FALocationID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region FAWriteOff
    [Serializable]
    [Table(Name = "FAWriteOff")]
    public class FAWriteOff : DbDataModel
    {
        private Int32 _FAWriteOffID;
        private String _FAWriteOffNo;
        private DateTime _FAWriteOffDate;
        private Int32 _FixedAssetID;
        private String _GCAssetWriteOffType;
        private String _GCAssetSalesType;
        private Decimal _AssetValue;
        private Decimal _WriteOffAmount;
        private String _Remarks;
        private String _GCTransactionStatus;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "FAWriteOffID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 FAWriteOffID
        {
            get { return _FAWriteOffID; }
            set { _FAWriteOffID = value; }
        }
        [Column(Name = "FAWriteOffNo", DataType = "String")]
        public String FAWriteOffNo
        {
            get { return _FAWriteOffNo; }
            set { _FAWriteOffNo = value; }
        }
        [Column(Name = "FAWriteOffDate", DataType = "DateTime")]
        public DateTime FAWriteOffDate
        {
            get { return _FAWriteOffDate; }
            set { _FAWriteOffDate = value; }
        }
        [Column(Name = "FixedAssetID", DataType = "Int32")]
        public Int32 FixedAssetID
        {
            get { return _FixedAssetID; }
            set { _FixedAssetID = value; }
        }
        [Column(Name = "GCAssetWriteOffType", DataType = "String")]
        public String GCAssetWriteOffType
        {
            get { return _GCAssetWriteOffType; }
            set { _GCAssetWriteOffType = value; }
        }
        [Column(Name = "GCAssetSalesType", DataType = "String", IsNullable = true)]
        public String GCAssetSalesType
        {
            get { return _GCAssetSalesType; }
            set { _GCAssetSalesType = value; }
        }
        [Column(Name = "AssetValue", DataType = "Decimal")]
        public Decimal AssetValue
        {
            get { return _AssetValue; }
            set { _AssetValue = value; }
        }
        [Column(Name = "WriteOffAmount", DataType = "Decimal")]
        public Decimal WriteOffAmount
        {
            get { return _WriteOffAmount; }
            set { _WriteOffAmount = value; }
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

    public class FAWriteOffDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(FAWriteOff));
        private bool _isAuditLog = false;
        private const string p_FAWriteOffID = "@p_FAWriteOffID";
        public FAWriteOffDao() { }
        public FAWriteOffDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public FAWriteOff Get(Int32 FAWriteOffID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_FAWriteOffID, FAWriteOffID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (FAWriteOff)_helper.DataRowToObject(row, new FAWriteOff());
        }
        public int Insert(FAWriteOff record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(FAWriteOff record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 FAWriteOffID)
        {
            FAWriteOff record;
            if (_ctx.Transaction == null)
                record = new FAWriteOffDao().Get(FAWriteOffID);
            else
                record = Get(FAWriteOffID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region GLSetting
    [Serializable]
    [Table(Name = "GLSetting")]
    public class GLSetting : DbDataModel
    {
        private Int32 _ID;
        private String _GLSettingCode;
        private String _GLSettingName;
        private Int32 _GLAccount;
        private Int32? _SubLedger;
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
        [Column(Name = "GLSettingCode", DataType = "String")]
        public String GLSettingCode
        {
            get { return _GLSettingCode; }
            set { _GLSettingCode = value; }
        }
        [Column(Name = "GLSettingName", DataType = "String")]
        public String GLSettingName
        {
            get { return _GLSettingName; }
            set { _GLSettingName = value; }
        }
        [Column(Name = "GLAccount", DataType = "Int32")]
        public Int32 GLAccount
        {
            get { return _GLAccount; }
            set { _GLAccount = value; }
        }
        [Column(Name = "SubLedger", DataType = "Int32", IsNullable = true)]
        public Int32? SubLedger
        {
            get { return _SubLedger; }
            set { _SubLedger = value; }
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

    public class GLSettingDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(GLSetting));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public GLSettingDao() { }
        public GLSettingDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public GLSetting Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (GLSetting)_helper.DataRowToObject(row, new GLSetting());
        }
        public int Insert(GLSetting record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(GLSetting record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            GLSetting record;
            if (_ctx.Transaction == null)
                record = new GLSettingDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region GLTransactionDt
    [Serializable]
    [Table(Name = "GLTransactionDt")]
    public class GLTransactionDt : DbDataModel
    {
        private Int32 _TransactionDtID;
        private Int32 _GLTransactionID;
        private Int32 _GLAccount;
        private Int32? _SubLedger;
        private String _Position;
        private Decimal _DebitAmount;
        private Decimal _CreditAmount;
        private String _ReferenceNo;
        private Int16 _DisplayOrder;
        private String _Remarks;
        private String _GCItemDetailStatus;
        private Boolean _IsDeleted;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "TransactionDtID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 TransactionDtID
        {
            get { return _TransactionDtID; }
            set { _TransactionDtID = value; }
        }
        [Column(Name = "GLTransactionID", DataType = "Int32")]
        public Int32 GLTransactionID
        {
            get { return _GLTransactionID; }
            set { _GLTransactionID = value; }
        }
        [Column(Name = "GLAccount", DataType = "Int32")]
        public Int32 GLAccount
        {
            get { return _GLAccount; }
            set { _GLAccount = value; }
        }
        [Column(Name = "SubLedger", DataType = "Int32", IsNullable = true)]
        public Int32? SubLedger
        {
            get { return _SubLedger; }
            set { _SubLedger = value; }
        }
        [Column(Name = "Position", DataType = "String")]
        public String Position
        {
            get { return _Position; }
            set { _Position = value; }
        }
        [Column(Name = "DebitAmount", DataType = "Decimal", IsNullable = true)]
        public Decimal DebitAmount
        {
            get { return _DebitAmount; }
            set { _DebitAmount = value; }
        }
        [Column(Name = "CreditAmount", DataType = "Decimal", IsNullable = true)]
        public Decimal CreditAmount
        {
            get { return _CreditAmount; }
            set { _CreditAmount = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String", IsNullable = true)]
        public String ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        [Column(Name = "DisplayOrder", DataType = "Int16")]
        public Int16 DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
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

    public class GLTransactionDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(GLTransactionDt));
        private bool _isAuditLog = false;
        private const string p_TransactionDtID = "@p_TransactionDtID";
        public GLTransactionDtDao() { }
        public GLTransactionDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public GLTransactionDt Get(Int32 TransactionDtID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_TransactionDtID, TransactionDtID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (GLTransactionDt)_helper.DataRowToObject(row, new GLTransactionDt());
        }
        public int Insert(GLTransactionDt record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(GLTransactionDt record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 TransactionDtID)
        {
            GLTransactionDt record;
            if (_ctx.Transaction == null)
                record = new GLTransactionDtDao().Get(TransactionDtID);
            else
                record = Get(TransactionDtID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region GLTransactionHd
    [Serializable]
    [Table(Name = "GLTransactionHd")]
    public class GLTransactionHd : DbDataModel
    {
        private Int32 _GLTransactionID;
        private String _GCJournalGroup;
        private String _TransactionCode;
        private String _JournalNo;
        private DateTime _JournalDate;
        private Decimal _DebitAmount;
        private Decimal _CreditAmount;
        private String _Remarks;
        private Boolean _IsGeneratedBySystem;
        private String _GCTransactionStatus;
        private String _GCVoidReason;
        private String _VoidReason;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "GLTransactionID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 GLTransactionID
        {
            get { return _GLTransactionID; }
            set { _GLTransactionID = value; }
        }
        [Column(Name = "GCJournalGroup", DataType = "String")]
        public String GCJournalGroup
        {
            get { return _GCJournalGroup; }
            set { _GCJournalGroup = value; }
        }
        [Column(Name = "TransactionCode", DataType = "String")]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }
        [Column(Name = "JournalNo", DataType = "String")]
        public String JournalNo
        {
            get { return _JournalNo; }
            set { _JournalNo = value; }
        }
        [Column(Name = "JournalDate", DataType = "DateTime")]
        public DateTime JournalDate
        {
            get { return _JournalDate; }
            set { _JournalDate = value; }
        }
        [Column(Name = "DebitAmount", DataType = "Decimal")]
        public Decimal DebitAmount
        {
            get { return _DebitAmount; }
            set { _DebitAmount = value; }
        }
        [Column(Name = "CreditAmount", DataType = "Decimal")]
        public Decimal CreditAmount
        {
            get { return _CreditAmount; }
            set { _CreditAmount = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsGeneratedBySystem", DataType = "Boolean")]
        public Boolean IsGeneratedBySystem
        {
            get { return _IsGeneratedBySystem; }
            set { _IsGeneratedBySystem = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "GCVoidReason", DataType = "String", IsNullable = true)]
        public String GCVoidReason
        {
            get { return _GCVoidReason; }
            set { _GCVoidReason = value; }
        }
        [Column(Name = "VoidReason", DataType = "String", IsNullable = true)]
        public String VoidReason
        {
            get { return _VoidReason; }
            set { _VoidReason = value; }
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

    public class GLTransactionHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(GLTransactionHd));
        private bool _isAuditLog = false;
        private const string p_GLTransactionID = "@p_GLTransactionID";
        public GLTransactionHdDao() { }
        public GLTransactionHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public GLTransactionHd Get(Int32 GLTransactionID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_GLTransactionID, GLTransactionID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (GLTransactionHd)_helper.DataRowToObject(row, new GLTransactionHd());
        }
        public int Insert(GLTransactionHd record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(GLTransactionHd record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 GLTransactionID)
        {
            GLTransactionHd record;
            if (_ctx.Transaction == null)
                record = new GLTransactionHdDao().Get(GLTransactionID);
            else
                record = Get(GLTransactionID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Holiday
    [Serializable]
    [Table(Name = "Holiday")]
    public partial class Holiday : DbDataModel
    {
        private Int32 _ID;
        private Int16 _HolidayDate;
        private Int16 _HolidayMonth;
        private Int16? _HolidayYear;
        private String _HolidayName;
        private Boolean _IsAnnualHoliday;
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
        [Column(Name = "HolidayDate", DataType = "Int16")]
        public Int16 HolidayDate
        {
            get { return _HolidayDate; }
            set { _HolidayDate = value; }
        }
        [Column(Name = "HolidayMonth", DataType = "Int16")]
        public Int16 HolidayMonth
        {
            get { return _HolidayMonth; }
            set { _HolidayMonth = value; }
        }
        [Column(Name = "HolidayYear", DataType = "Int16", IsNullable = true)]
        public Int16? HolidayYear
        {
            get { return _HolidayYear; }
            set { _HolidayYear = value; }
        }
        [Column(Name = "HolidayName", DataType = "String")]
        public String HolidayName
        {
            get { return _HolidayName; }
            set { _HolidayName = value; }
        }
        [Column(Name = "IsAnnualHoliday", DataType = "Boolean")]
        public Boolean IsAnnualHoliday
        {
            get { return _IsAnnualHoliday; }
            set { _IsAnnualHoliday = value; }
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

    public class HolidayDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Holiday));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public HolidayDao() { }
        public HolidayDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Holiday Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Holiday)_helper.DataRowToObject(row, new Holiday());
        }
        public int Insert(Holiday record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Holiday record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            Holiday record;
            if (_ctx.Transaction == null)
                record = new HolidayDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ItemAlternateUnit
    [Serializable]
    [Table(Name = "ItemAlternateUnit")]
    public class ItemAlternateUnit : DbDataModel
    {
        private Int32 _ID;
        private Int32 _ItemID;
        private String _GCAlternateUnit;
        private Decimal _ConversionFactor;
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
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "GCAlternateUnit", DataType = "String")]
        public String GCAlternateUnit
        {
            get { return _GCAlternateUnit; }
            set { _GCAlternateUnit = value; }
        }
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
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

    public class ItemAlternateUnitDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ItemAlternateUnit));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public ItemAlternateUnitDao() { }
        public ItemAlternateUnitDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ItemAlternateUnit Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ItemAlternateUnit)_helper.DataRowToObject(row, new ItemAlternateUnit());
        }
        public int Insert(ItemAlternateUnit record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ItemAlternateUnit record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            ItemAlternateUnit record;
            if (_ctx.Transaction == null)
                record = new ItemAlternateUnitDao().Get(ID);
            else
                record = Get(ID);
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
    #region ItemCost
    [Serializable]
    [Table(Name = "ItemCost")]
    public class ItemCost : DbDataModel
    {
        private Int32 _ItemCostID;
        private Int32 _ItemID;
        private String _SiteID;
        private Decimal _PreviousMaterial;
        private Decimal _CurrentMaterial;
        private Decimal _TotalMaterial;
        private Decimal _PreviousLabor;
        private Decimal _CurrentLabor;
        private Decimal _TotalLabor;
        private Decimal _PreviousOverhead;
        private Decimal _CurrentOverhead;
        private Decimal _TotalOverhead;
        private Decimal _PreviousSubContract;
        private Decimal _CurrentSubContract;
        private Decimal _TotalSubContract;
        private Decimal _PreviousBurden;
        private Decimal _CurrentBurden;
        private Decimal _TotalBurden;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ItemCostID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ItemCostID
        {
            get { return _ItemCostID; }
            set { _ItemCostID = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "PreviousMaterial", DataType = "Decimal")]
        public Decimal PreviousMaterial
        {
            get { return _PreviousMaterial; }
            set { _PreviousMaterial = value; }
        }
        [Column(Name = "CurrentMaterial", DataType = "Decimal")]
        public Decimal CurrentMaterial
        {
            get { return _CurrentMaterial; }
            set { _CurrentMaterial = value; }
        }
        [Column(Name = "TotalMaterial", DataType = "Decimal", IsNullable = true, IsComputed = true)]
        public Decimal TotalMaterial
        {
            get { return _TotalMaterial; }
            set { _TotalMaterial = value; }
        }
        [Column(Name = "PreviousLabor", DataType = "Decimal")]
        public Decimal PreviousLabor
        {
            get { return _PreviousLabor; }
            set { _PreviousLabor = value; }
        }
        [Column(Name = "CurrentLabor", DataType = "Decimal")]
        public Decimal CurrentLabor
        {
            get { return _CurrentLabor; }
            set { _CurrentLabor = value; }
        }
        [Column(Name = "TotalLabor", DataType = "Decimal", IsNullable = true, IsComputed = true)]
        public Decimal TotalLabor
        {
            get { return _TotalLabor; }
            set { _TotalLabor = value; }
        }
        [Column(Name = "PreviousOverhead", DataType = "Decimal")]
        public Decimal PreviousOverhead
        {
            get { return _PreviousOverhead; }
            set { _PreviousOverhead = value; }
        }
        [Column(Name = "CurrentOverhead", DataType = "Decimal")]
        public Decimal CurrentOverhead
        {
            get { return _CurrentOverhead; }
            set { _CurrentOverhead = value; }
        }
        [Column(Name = "TotalOverhead", DataType = "Decimal", IsNullable = true, IsComputed = true)]
        public Decimal TotalOverhead
        {
            get { return _TotalOverhead; }
            set { _TotalOverhead = value; }
        }
        [Column(Name = "PreviousSubContract", DataType = "Decimal")]
        public Decimal PreviousSubContract
        {
            get { return _PreviousSubContract; }
            set { _PreviousSubContract = value; }
        }
        [Column(Name = "CurrentSubContract", DataType = "Decimal")]
        public Decimal CurrentSubContract
        {
            get { return _CurrentSubContract; }
            set { _CurrentSubContract = value; }
        }
        [Column(Name = "TotalSubContract", DataType = "Decimal", IsNullable = true, IsComputed = true)]
        public Decimal TotalSubContract
        {
            get { return _TotalSubContract; }
            set { _TotalSubContract = value; }
        }
        [Column(Name = "PreviousBurden", DataType = "Decimal")]
        public Decimal PreviousBurden
        {
            get { return _PreviousBurden; }
            set { _PreviousBurden = value; }
        }
        [Column(Name = "CurrentBurden", DataType = "Decimal")]
        public Decimal CurrentBurden
        {
            get { return _CurrentBurden; }
            set { _CurrentBurden = value; }
        }
        [Column(Name = "TotalBurden", DataType = "Decimal", IsNullable = true, IsComputed = true)]
        public Decimal TotalBurden
        {
            get { return _TotalBurden; }
            set { _TotalBurden = value; }
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

    public class ItemCostDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ItemCost));
        private bool _isAuditLog = false;
        private const string p_ItemCostID = "@p_ItemCostID";
        public ItemCostDao() { }
        public ItemCostDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ItemCost Get(Int32 ItemCostID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ItemCostID, ItemCostID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ItemCost)_helper.DataRowToObject(row, new ItemCost());
        }
        public int Insert(ItemCost record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ItemCost record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ItemCostID)
        {
            ItemCost record;
            if (_ctx.Transaction == null)
                record = new ItemCostDao().Get(ItemCostID);
            else
                record = Get(ItemCostID);
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
    #region ItemGroupMaster
    [Serializable]
    [Table(Name = "ItemGroupMaster")]
    public class ItemGroupMaster : DbDataModel
    {
        private Int32 _ItemGroupID;
        private String _GCItemType;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private String _ItemGroupName2;
        private Boolean _IsHeader;
        private Int32? _ParentID;
        private Int16 _PrintOrder;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ItemGroupID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ItemGroupID
        {
            get { return _ItemGroupID; }
            set { _ItemGroupID = value; }
        }
        [Column(Name = "GCItemType", DataType = "String")]
        public String GCItemType
        {
            get { return _GCItemType; }
            set { _GCItemType = value; }
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
        [Column(Name = "ItemGroupName2", DataType = "String", IsNullable = true)]
        public String ItemGroupName2
        {
            get { return _ItemGroupName2; }
            set { _ItemGroupName2 = value; }
        }
        [Column(Name = "IsHeader", DataType = "Boolean", IsNullable = true)]
        public Boolean IsHeader
        {
            get { return _IsHeader; }
            set { _IsHeader = value; }
        }
        [Column(Name = "ParentID", DataType = "Int32", IsNullable = true)]
        public Int32? ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        [Column(Name = "PrintOrder", DataType = "Int16")]
        public Int16 PrintOrder
        {
            get { return _PrintOrder; }
            set { _PrintOrder = value; }
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

    public class ItemGroupMasterDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ItemGroupMaster));
        private bool _isAuditLog = false;
        private const string p_ItemGroupID = "@p_ItemGroupID";
        public ItemGroupMasterDao() { }
        public ItemGroupMasterDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ItemGroupMaster Get(Int32 ItemGroupID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ItemGroupID, ItemGroupID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ItemGroupMaster)_helper.DataRowToObject(row, new ItemGroupMaster());
        }
        public int Insert(ItemGroupMaster record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ItemGroupMaster record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ItemGroupID)
        {
            ItemGroupMaster record;
            if (_ctx.Transaction == null)
                record = new ItemGroupMasterDao().Get(ItemGroupID);
            else
                record = Get(ItemGroupID);
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
    #region ItemProduct
    [Serializable]
    [Table(Name = "ItemProduct")]
    public class ItemProduct : DbDataModel
    {
        private Int32 _ItemID;
        private Int32? _ProductBrandID;
        private Int32? _RestrictionID;
        private Int32? _MarkupID;
        private Decimal _MarginPercentage;
        private Boolean _IsInventoryItem;
        private Boolean _IsControlExpired;
        private Boolean _IsProductionItem;
        private Boolean _IsUsingStandardPrice;
        private String _GCABCClass;
        private Decimal _CycleCountInterval;
        private Decimal _HETAmount;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ItemID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "ProductBrandID", DataType = "Int32", IsNullable = true)]
        public Int32? ProductBrandID
        {
            get { return _ProductBrandID; }
            set { _ProductBrandID = value; }
        }
        [Column(Name = "RestrictionID", DataType = "Int32", IsNullable = true)]
        public Int32? RestrictionID
        {
            get { return _RestrictionID; }
            set { _RestrictionID = value; }
        }
        [Column(Name = "MarkupID", DataType = "Int32", IsNullable = true)]
        public Int32? MarkupID
        {
            get { return _MarkupID; }
            set { _MarkupID = value; }
        }
        [Column(Name = "MarginPercentage", DataType = "Decimal")]
        public Decimal MarginPercentage
        {
            get { return _MarginPercentage; }
            set { _MarginPercentage = value; }
        }
        [Column(Name = "IsInventoryItem", DataType = "Boolean", IsNullable = true)]
        public Boolean IsInventoryItem
        {
            get { return _IsInventoryItem; }
            set { _IsInventoryItem = value; }
        }
        [Column(Name = "IsControlExpired", DataType = "Boolean", IsNullable = true)]
        public Boolean IsControlExpired
        {
            get { return _IsControlExpired; }
            set { _IsControlExpired = value; }
        }
        [Column(Name = "IsProductionItem", DataType = "Boolean", IsNullable = true)]
        public Boolean IsProductionItem
        {
            get { return _IsProductionItem; }
            set { _IsProductionItem = value; }
        }
        [Column(Name = "IsUsingStandardPrice", DataType = "Boolean")]
        public Boolean IsUsingStandardPrice
        {
            get { return _IsUsingStandardPrice; }
            set { _IsUsingStandardPrice = value; }
        }
        [Column(Name = "GCABCClass", DataType = "String", IsNullable = true)]
        public String GCABCClass
        {
            get { return _GCABCClass; }
            set { _GCABCClass = value; }
        }
        [Column(Name = "CycleCountInterval", DataType = "Decimal", IsNullable = true)]
        public Decimal CycleCountInterval
        {
            get { return _CycleCountInterval; }
            set { _CycleCountInterval = value; }
        }
        [Column(Name = "HETAmount", DataType = "Decimal", IsNullable = true)]
        public Decimal HETAmount
        {
            get { return _HETAmount; }
            set { _HETAmount = value; }
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

    public class ItemProductDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ItemProduct));
        private bool _isAuditLog = false;
        private const string p_ItemID = "@p_ItemID";
        public ItemProductDao() { }
        public ItemProductDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ItemProduct Get(Int32 ItemID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ItemID, ItemID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ItemProduct)_helper.DataRowToObject(row, new ItemProduct());
        }
        public int Insert(ItemProduct record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ItemProduct record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ItemID)
        {
            ItemProduct record;
            if (_ctx.Transaction == null)
                record = new ItemProductDao().Get(ItemID);
            else
                record = Get(ItemID);
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
    #region ItemTagField
    [Serializable]
    [Table(Name = "ItemTagField")]
    public class ItemTagField : DbDataModel
    {
        private Int32 _ItemID;
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

        [Column(Name = "ItemID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
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

    public class ItemTagFieldDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ItemTagField));
        private bool _isAuditLog = false;
        private const string p_ItemID = "@p_ItemID";
        public ItemTagFieldDao() { }
        public ItemTagFieldDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ItemTagField Get(Int32 ItemID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ItemID, ItemID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ItemTagField)_helper.DataRowToObject(row, new ItemTagField());
        }
        public int Insert(ItemTagField record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ItemTagField record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ItemID)
        {
            ItemTagField record;
            if (_ctx.Transaction == null)
                record = new ItemTagFieldDao().Get(ItemID);
            else
                record = Get(ItemID);
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
    #region JournalTemplateDt
    [Serializable]
    [Table(Name = "JournalTemplateDt")]
    public class JournalTemplateDt : DbDataModel
    {
        private Int32 _ID;
        private Int32 _TemplateID;
        private Int32 _GLAccountID;
        private Int32? _SubLedgerID;
        private Decimal _AmountPercentage;
        private String _Position;
        private Int16 _DisplayOrder;
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
        [Column(Name = "TemplateID", DataType = "Int32")]
        public Int32 TemplateID
        {
            get { return _TemplateID; }
            set { _TemplateID = value; }
        }
        [Column(Name = "GLAccountID", DataType = "Int32")]
        public Int32 GLAccountID
        {
            get { return _GLAccountID; }
            set { _GLAccountID = value; }
        }
        [Column(Name = "SubLedgerID", DataType = "Int32", IsNullable = true)]
        public Int32? SubLedgerID
        {
            get { return _SubLedgerID; }
            set { _SubLedgerID = value; }
        }
        [Column(Name = "AmountPercentage", DataType = "Decimal")]
        public Decimal AmountPercentage
        {
            get { return _AmountPercentage; }
            set { _AmountPercentage = value; }
        }
        [Column(Name = "Position", DataType = "String")]
        public String Position
        {
            get { return _Position; }
            set { _Position = value; }
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

    public class JournalTemplateDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(JournalTemplateDt));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public JournalTemplateDtDao() { }
        public JournalTemplateDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public JournalTemplateDt Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (JournalTemplateDt)_helper.DataRowToObject(row, new JournalTemplateDt());
        }
        public int Insert(JournalTemplateDt record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(JournalTemplateDt record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            JournalTemplateDt record;
            if (_ctx.Transaction == null)
                record = new JournalTemplateDtDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region JournalTemplateHd
    [Serializable]
    [Table(Name = "JournalTemplateHd")]
    public class JournalTemplateHd : DbDataModel
    {
        private Int32 _TemplateID;
        private String _TemplateCode;
        private String _TemplateName;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "TemplateID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 TemplateID
        {
            get { return _TemplateID; }
            set { _TemplateID = value; }
        }
        [Column(Name = "TemplateCode", DataType = "String")]
        public String TemplateCode
        {
            get { return _TemplateCode; }
            set { _TemplateCode = value; }
        }
        [Column(Name = "TemplateName", DataType = "String")]
        public String TemplateName
        {
            get { return _TemplateName; }
            set { _TemplateName = value; }
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

    public class JournalTemplateHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(JournalTemplateHd));
        private bool _isAuditLog = false;
        private const string p_TemplateID = "@p_TemplateID";
        public JournalTemplateHdDao() { }
        public JournalTemplateHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public JournalTemplateHd Get(Int32 TemplateID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_TemplateID, TemplateID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (JournalTemplateHd)_helper.DataRowToObject(row, new JournalTemplateHd());
        }
        public int Insert(JournalTemplateHd record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(JournalTemplateHd record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 TemplateID)
        {
            JournalTemplateHd record;
            if (_ctx.Transaction == null)
                record = new JournalTemplateHdDao().Get(TemplateID);
            else
                record = Get(TemplateID);
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
    #region Manufacturer
    [Serializable]
    [Table(Name = "Manufacturer")]
    public class Manufacturer : DbDataModel
    {
        private Int32 _ManufacturerID;
        private String _ManufacturerCode;
        private String _ManufacturerName;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ManufacturerID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ManufacturerID
        {
            get { return _ManufacturerID; }
            set { _ManufacturerID = value; }
        }
        [Column(Name = "ManufacturerCode", DataType = "String")]
        public String ManufacturerCode
        {
            get { return _ManufacturerCode; }
            set { _ManufacturerCode = value; }
        }
        [Column(Name = "ManufacturerName", DataType = "String")]
        public String ManufacturerName
        {
            get { return _ManufacturerName; }
            set { _ManufacturerName = value; }
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

    public class ManufacturerDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Manufacturer));
        private bool _isAuditLog = false;
        private const string p_ManufacturerID = "@p_ManufacturerID";
        public ManufacturerDao() { }
        public ManufacturerDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Manufacturer Get(Int32 ManufacturerID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ManufacturerID, ManufacturerID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Manufacturer)_helper.DataRowToObject(row, new Manufacturer());
        }
        public int Insert(Manufacturer record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Manufacturer record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ManufacturerID)
        {
            Manufacturer record;
            if (_ctx.Transaction == null)
                record = new ManufacturerDao().Get(ManufacturerID);
            else
                record = Get(ManufacturerID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region MarginMarkupDt
    [Serializable]
    [Table(Name = "MarginMarkupDt")]
    public class MarginMarkupDt : DbDataModel
    {
        private Int32 _MarkupID;
        private Int16 _SequenceNo;
        private Decimal _StartingValue;
        private Decimal _EndingValue;
        private Decimal _MarkupAmount;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "MarkupID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 MarkupID
        {
            get { return _MarkupID; }
            set { _MarkupID = value; }
        }
        [Column(Name = "SequenceNo", DataType = "Int16", IsPrimaryKey = true)]
        public Int16 SequenceNo
        {
            get { return _SequenceNo; }
            set { _SequenceNo = value; }
        }
        [Column(Name = "StartingValue", DataType = "Decimal")]
        public Decimal StartingValue
        {
            get { return _StartingValue; }
            set { _StartingValue = value; }
        }
        [Column(Name = "EndingValue", DataType = "Decimal")]
        public Decimal EndingValue
        {
            get { return _EndingValue; }
            set { _EndingValue = value; }
        }
        [Column(Name = "MarkupAmount", DataType = "Decimal")]
        public Decimal MarkupAmount
        {
            get { return _MarkupAmount; }
            set { _MarkupAmount = value; }
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

    public class MarginMarkupDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(MarginMarkupDt));
        private bool _isAuditLog = false;
        private const string p_MarkupID = "@p_MarkupID";
        private const string p_SequenceNo = "@p_SequenceNo";
        public MarginMarkupDtDao() { }
        public MarginMarkupDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public MarginMarkupDt Get(Int32 MarkupID, Int16 SequenceNo)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_MarkupID, MarkupID);
            _ctx.Add(p_SequenceNo, SequenceNo);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (MarginMarkupDt)_helper.DataRowToObject(row, new MarginMarkupDt());
        }
        public int Insert(MarginMarkupDt record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(MarginMarkupDt record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 MarkupID, Int16 SequenceNo)
        {
            MarginMarkupDt record;
            if (_ctx.Transaction == null)
                record = new MarginMarkupDtDao().Get(MarkupID, SequenceNo);
            else
                record = Get(MarkupID, SequenceNo);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region MarginMarkupHd
    [Serializable]
    [Table(Name = "MarginMarkupHd")]
    public class MarginMarkupHd : DbDataModel
    {
        private Int32 _MarkupID;
        private String _MarkupCode;
        private String _MarkupName;
        private Boolean _IsMarkupInPercentage;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "MarkupID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 MarkupID
        {
            get { return _MarkupID; }
            set { _MarkupID = value; }
        }
        [Column(Name = "MarkupCode", DataType = "String")]
        public String MarkupCode
        {
            get { return _MarkupCode; }
            set { _MarkupCode = value; }
        }
        [Column(Name = "MarkupName", DataType = "String")]
        public String MarkupName
        {
            get { return _MarkupName; }
            set { _MarkupName = value; }
        }
        [Column(Name = "IsMarkupInPercentage", DataType = "Boolean")]
        public Boolean IsMarkupInPercentage
        {
            get { return _IsMarkupInPercentage; }
            set { _IsMarkupInPercentage = value; }
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

    public class MarginMarkupHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(MarginMarkupHd));
        private bool _isAuditLog = false;
        private const string p_MarkupID = "@p_MarkupID";
        public MarginMarkupHdDao() { }
        public MarginMarkupHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public MarginMarkupHd Get(Int32 MarkupID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_MarkupID, MarkupID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (MarginMarkupHd)_helper.DataRowToObject(row, new MarginMarkupHd());
        }
        public int Insert(MarginMarkupHd record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(MarginMarkupHd record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 MarkupID)
        {
            MarginMarkupHd record;
            if (_ctx.Transaction == null)
                record = new MarginMarkupHdDao().Get(MarkupID);
            else
                record = Get(MarkupID);
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
        private String _Initial;
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
        [Column(Name = "Initial", DataType = "String", IsNullable = true)]
        public String Initial
        {
            get { return _Initial; }
            set { _Initial = value; }
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
    #region ProductBrand
    [Serializable]
    [Table(Name = "ProductBrand")]
    public class ProductBrand : DbDataModel
    {
        private Int32 _ProductBrandID;
        private String _ProductBrandCode;
        private String _ProductBrandName;
        private Int32 _ManufacturerID;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ProductBrandID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ProductBrandID
        {
            get { return _ProductBrandID; }
            set { _ProductBrandID = value; }
        }
        [Column(Name = "ProductBrandCode", DataType = "String")]
        public String ProductBrandCode
        {
            get { return _ProductBrandCode; }
            set { _ProductBrandCode = value; }
        }
        [Column(Name = "ProductBrandName", DataType = "String")]
        public String ProductBrandName
        {
            get { return _ProductBrandName; }
            set { _ProductBrandName = value; }
        }
        [Column(Name = "ManufacturerID", DataType = "Int32")]
        public Int32 ManufacturerID
        {
            get { return _ManufacturerID; }
            set { _ManufacturerID = value; }
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

    public class ProductBrandDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ProductBrand));
        private bool _isAuditLog = false;
        private const string p_ProductBrandID = "@p_ProductBrandID";
        public ProductBrandDao() { }
        public ProductBrandDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ProductBrand Get(Int32 ProductBrandID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ProductBrandID, ProductBrandID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ProductBrand)_helper.DataRowToObject(row, new ProductBrand());
        }
        public int Insert(ProductBrand record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ProductBrand record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ProductBrandID)
        {
            ProductBrand record;
            if (_ctx.Transaction == null)
                record = new ProductBrandDao().Get(ProductBrandID);
            else
                record = Get(ProductBrandID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ProspectiveStudent
    [Serializable]
    [Table(Name = "ProspectiveStudent")]
    public class ProspectiveStudent : DbDataModel
    {
        private Int32 _ProspectiveStudentID;
        private String _ProspectiveStudentCode;
        private String _SiteID;
        private String _GCSalutation;
        private String _GCProspectiveStudentStatus;
        private String _GCTitle;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _ProspectiveStudentName;
        private String _Name;
        private String _GCSuffix;
        private String _PreferredName;
        private String _CityOfBirth;
        private DateTime _DateOfBirth;
        private String _GCGender;
        private String _GCNationality;
        private String _GCReligion;
        private Int32? _PeriodAdmissionID;
        private Int32 _AddressID;
        private String _EmailAddress1;
        private String _EmailAddress2;
        private String _MobilePhoneNo1;
        private String _MobilePhoneNo2;
        private String _PictureFileName;
        private String _GCBloodType;
        private String _GCLanguage;
        private Decimal _HomeDistance;
        private String _MedicalHistory;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ProspectiveStudentID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ProspectiveStudentID
        {
            get { return _ProspectiveStudentID; }
            set { _ProspectiveStudentID = value; }
        }
        [Column(Name = "ProspectiveStudentCode", DataType = "String")]
        public String ProspectiveStudentCode
        {
            get { return _ProspectiveStudentCode; }
            set { _ProspectiveStudentCode = value; }
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
        [Column(Name = "GCProspectiveStudentStatus", DataType = "String")]
        public String GCProspectiveStudentStatus
        {
            get { return _GCProspectiveStudentStatus; }
            set { _GCProspectiveStudentStatus = value; }
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
        [Column(Name = "ProspectiveStudentName", DataType = "String", IsNullable = true)]
        public String ProspectiveStudentName
        {
            get { return _ProspectiveStudentName; }
            set { _ProspectiveStudentName = value; }
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
        [Column(Name = "GCReligion", DataType = "String", IsNullable = true)]
        public String GCReligion
        {
            get { return _GCReligion; }
            set { _GCReligion = value; }
        }
        [Column(Name = "PeriodAdmissionID", DataType = "Int32", IsNullable = true)]
        public Int32? PeriodAdmissionID
        {
            get { return _PeriodAdmissionID; }
            set { _PeriodAdmissionID = value; }
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
        [Column(Name = "GCBloodType", DataType = "String", IsNullable = true)]
        public String GCBloodType
        {
            get { return _GCBloodType; }
            set { _GCBloodType = value; }
        }
        [Column(Name = "GCLanguage", DataType = "String", IsNullable = true)]
        public String GCLanguage
        {
            get { return _GCLanguage; }
            set { _GCLanguage = value; }
        }
        [Column(Name = "HomeDistance", DataType = "Decimal", IsNullable = true)]
        public Decimal HomeDistance
        {
            get { return _HomeDistance; }
            set { _HomeDistance = value; }
        }
        [Column(Name = "MedicalHistory", DataType = "String", IsNullable = true)]
        public String MedicalHistory
        {
            get { return _MedicalHistory; }
            set { _MedicalHistory = value; }
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

    public class ProspectiveStudentDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ProspectiveStudent));
        private bool _isAuditLog = false;
        private const string p_ProspectiveStudentID = "@p_ProspectiveStudentID";
        public ProspectiveStudentDao() { }
        public ProspectiveStudentDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ProspectiveStudent Get(Int32 ProspectiveStudentID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ProspectiveStudentID, ProspectiveStudentID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ProspectiveStudent)_helper.DataRowToObject(row, new ProspectiveStudent());
        }
        public int Insert(ProspectiveStudent record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ProspectiveStudent record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ProspectiveStudentID)
        {
            ProspectiveStudent record;
            if (_ctx.Transaction == null)
                record = new ProspectiveStudentDao().Get(ProspectiveStudentID);
            else
                record = Get(ProspectiveStudentID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ProspectiveStudentFamily
    [Serializable]
    [Table(Name = "ProspectiveStudentFamily")]
    public class ProspectiveStudentFamily : DbDataModel
    {
        private Int32 _FamilyID;
        private Int32 _ProspectiveStudentID;
        private String _GCFamilyRelation;
        private String _GCSalutation;
        private String _GCTitle;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _FullName;
        private String _FamilyName;
        private String _CityOfBirth;
        private DateTime _DateOfBirth;
        private String _GCSuffix;
        private String _GCReligion;
        private String _GCNationality;
        private String _GCEducationLevel;
        private String _CompanyName;
        private String _GCJob;
        private String _Occupation;
        private Decimal _Salary;
        private Int32? _OfficeAddressID;
        private String _EmailAddress;
        private String _MobilePhoneNo1;
        private String _MobilePhoneNo2;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "FamilyID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 FamilyID
        {
            get { return _FamilyID; }
            set { _FamilyID = value; }
        }
        [Column(Name = "ProspectiveStudentID", DataType = "Int32")]
        public Int32 ProspectiveStudentID
        {
            get { return _ProspectiveStudentID; }
            set { _ProspectiveStudentID = value; }
        }
        [Column(Name = "GCFamilyRelation", DataType = "String")]
        public String GCFamilyRelation
        {
            get { return _GCFamilyRelation; }
            set { _GCFamilyRelation = value; }
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
        [Column(Name = "FullName", DataType = "String", IsNullable = true)]
        public String FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
        [Column(Name = "FamilyName", DataType = "String", IsNullable = true)]
        public String FamilyName
        {
            get { return _FamilyName; }
            set { _FamilyName = value; }
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
        [Column(Name = "GCSuffix", DataType = "String", IsNullable = true)]
        public String GCSuffix
        {
            get { return _GCSuffix; }
            set { _GCSuffix = value; }
        }
        [Column(Name = "GCReligion", DataType = "String", IsNullable = true)]
        public String GCReligion
        {
            get { return _GCReligion; }
            set { _GCReligion = value; }
        }
        [Column(Name = "GCNationality", DataType = "String", IsNullable = true)]
        public String GCNationality
        {
            get { return _GCNationality; }
            set { _GCNationality = value; }
        }
        [Column(Name = "GCEducationLevel", DataType = "String", IsNullable = true)]
        public String GCEducationLevel
        {
            get { return _GCEducationLevel; }
            set { _GCEducationLevel = value; }
        }
        [Column(Name = "CompanyName", DataType = "String", IsNullable = true)]
        public String CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        [Column(Name = "GCJob", DataType = "String", IsNullable = true)]
        public String GCJob
        {
            get { return _GCJob; }
            set { _GCJob = value; }
        }
        [Column(Name = "Occupation", DataType = "String", IsNullable = true)]
        public String Occupation
        {
            get { return _Occupation; }
            set { _Occupation = value; }
        }
        [Column(Name = "Salary", DataType = "Decimal", IsNullable = true)]
        public Decimal Salary
        {
            get { return _Salary; }
            set { _Salary = value; }
        }
        [Column(Name = "OfficeAddressID", DataType = "Int32", IsNullable = true)]
        public Int32? OfficeAddressID
        {
            get { return _OfficeAddressID; }
            set { _OfficeAddressID = value; }
        }
        [Column(Name = "EmailAddress", DataType = "String", IsNullable = true)]
        public String EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
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

    public class ProspectiveStudentFamilyDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ProspectiveStudentFamily));
        private bool _isAuditLog = false;
        private const string p_FamilyID = "@p_FamilyID";
        public ProspectiveStudentFamilyDao() { }
        public ProspectiveStudentFamilyDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ProspectiveStudentFamily Get(Int32 FamilyID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_FamilyID, FamilyID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ProspectiveStudentFamily)_helper.DataRowToObject(row, new ProspectiveStudentFamily());
        }
        public int Insert(ProspectiveStudentFamily record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ProspectiveStudentFamily record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 FamilyID)
        {
            ProspectiveStudentFamily record;
            if (_ctx.Transaction == null)
                record = new ProspectiveStudentFamilyDao().Get(FamilyID);
            else
                record = Get(FamilyID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PurchaseInvoiceDt
    [Serializable]
    [Table(Name = "PurchaseInvoiceDt")]
    public class PurchaseInvoiceDt : DbDataModel
    {
        private Int32 _ID;
        private Int32 _PurchaseInvoiceID;
        private Int32? _PurchaseReceiveID;
        private String _ReferenceNo;
        private DateTime _ReferenceDate;
        private Decimal _TransactionAmount;
        private Decimal _DiscountAmount;
        private Decimal _FinalDiscountAmount;
        private Decimal _VATAmount;
        private Decimal _PPH23Amount;
        private Decimal _PPH25Amount;
        private Decimal _ChargesAmount;
        private Decimal _StampAmount;
        private Decimal _CreditNoteAmount;
        private Decimal _DownPaymentAmount;
        private Decimal _LineAmount;
        private Decimal _PaymentAmount;
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
        [Column(Name = "PurchaseInvoiceID", DataType = "Int32")]
        public Int32 PurchaseInvoiceID
        {
            get { return _PurchaseInvoiceID; }
            set { _PurchaseInvoiceID = value; }
        }
        [Column(Name = "PurchaseReceiveID", DataType = "Int32", IsNullable = true)]
        public Int32? PurchaseReceiveID
        {
            get { return _PurchaseReceiveID; }
            set { _PurchaseReceiveID = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String")]
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
        [Column(Name = "FinalDiscountAmount", DataType = "Decimal")]
        public Decimal FinalDiscountAmount
        {
            get { return _FinalDiscountAmount; }
            set { _FinalDiscountAmount = value; }
        }
        [Column(Name = "VATAmount", DataType = "Decimal")]
        public Decimal VATAmount
        {
            get { return _VATAmount; }
            set { _VATAmount = value; }
        }
        [Column(Name = "PPH23Amount", DataType = "Decimal")]
        public Decimal PPH23Amount
        {
            get { return _PPH23Amount; }
            set { _PPH23Amount = value; }
        }
        [Column(Name = "PPH25Amount", DataType = "Decimal")]
        public Decimal PPH25Amount
        {
            get { return _PPH25Amount; }
            set { _PPH25Amount = value; }
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
        [Column(Name = "CreditNoteAmount", DataType = "Decimal")]
        public Decimal CreditNoteAmount
        {
            get { return _CreditNoteAmount; }
            set { _CreditNoteAmount = value; }
        }
        [Column(Name = "DownPaymentAmount", DataType = "Decimal")]
        public Decimal DownPaymentAmount
        {
            get { return _DownPaymentAmount; }
            set { _DownPaymentAmount = value; }
        }
        [Column(Name = "LineAmount", DataType = "Decimal")]
        public Decimal LineAmount
        {
            get { return _LineAmount; }
            set { _LineAmount = value; }
        }
        [Column(Name = "PaymentAmount", DataType = "Decimal")]
        public Decimal PaymentAmount
        {
            get { return _PaymentAmount; }
            set { _PaymentAmount = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32? CreatedBy
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

    public class PurchaseInvoiceDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PurchaseInvoiceDt));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public PurchaseInvoiceDtDao() { }
        public PurchaseInvoiceDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PurchaseInvoiceDt Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PurchaseInvoiceDt)_helper.DataRowToObject(row, new PurchaseInvoiceDt());
        }
        public int Insert(PurchaseInvoiceDt record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PurchaseInvoiceDt record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            PurchaseInvoiceDt record;
            if (_ctx.Transaction == null)
                record = new PurchaseInvoiceDtDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PurchaseInvoiceDtPayment
    [Serializable]
    [Table(Name = "PurchaseInvoiceDtPayment")]
    public class PurchaseInvoiceDtPayment : DbDataModel
    {
        private Int32 _ID;
        private Int32 _PurchaseInvoiceDtID;
        private Int32 _SupplierPaymentID;
        private DateTime _PaymentDate;
        private Decimal _PaymentAmount;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "PurchaseInvoiceDtID", DataType = "Int32")]
        public Int32 PurchaseInvoiceDtID
        {
            get { return _PurchaseInvoiceDtID; }
            set { _PurchaseInvoiceDtID = value; }
        }
        [Column(Name = "SupplierPaymentID", DataType = "Int32")]
        public Int32 SupplierPaymentID
        {
            get { return _SupplierPaymentID; }
            set { _SupplierPaymentID = value; }
        }
        [Column(Name = "PaymentDate", DataType = "DateTime")]
        public DateTime PaymentDate
        {
            get { return _PaymentDate; }
            set { _PaymentDate = value; }
        }
        [Column(Name = "PaymentAmount", DataType = "Decimal")]
        public Decimal PaymentAmount
        {
            get { return _PaymentAmount; }
            set { _PaymentAmount = value; }
        }
    }

    public class PurchaseInvoiceDtPaymentDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PurchaseInvoiceDtPayment));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public PurchaseInvoiceDtPaymentDao() { }
        public PurchaseInvoiceDtPaymentDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PurchaseInvoiceDtPayment Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PurchaseInvoiceDtPayment)_helper.DataRowToObject(row, new PurchaseInvoiceDtPayment());
        }
        public int Insert(PurchaseInvoiceDtPayment record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PurchaseInvoiceDtPayment record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            PurchaseInvoiceDtPayment record;
            if (_ctx.Transaction == null)
                record = new PurchaseInvoiceDtPaymentDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PurchaseInvoiceHd
    [Serializable]
    [Table(Name = "PurchaseInvoiceHd")]
    public partial class PurchaseInvoiceHd : DbDataModel
    {
        private Int32 _PurchaseInvoiceID;
        private DateTime _PurchaseInvoiceDate;
        private Int32 _BusinessPartnerID;
        private String _PurchaseInvoiceNo;
        private String _SupplierInvoiceNo;
        private DateTime _SupplierInvoiceDate;
        private String _TaxInvoiceNo;
        private DateTime _TaxInvoiceDate;
        private DateTime _DueDate;
        private String _GCCurrencyCode;
        private Decimal _CurrencyRate;
        private Decimal _TotalTransactionAmount;
        private Decimal _TotalDownPaymentAmount;
        private Decimal _TotalCreditNoteAmount;
        private Decimal _FinalDiscount;
        private Decimal _VATPercentage;
        private Decimal _PPHPercentage;
        private String _GCChargesType;
        private Decimal _ChargesAmount;
        private Decimal _StampAmount;
        private Decimal _TotalNetTransactionAmount;
        private Int16 _NumberOfPayment;
        private Decimal _PaymentAmount;
        private String _Remarks;
        private Boolean _IsVerified;
        private Int32? _VerifiedBy;
        private DateTime _VerifiedDate;
        private String _GCTransactionStatus;
        private String _GCVoidReason;
        private String _VoidReason;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "PurchaseInvoiceID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 PurchaseInvoiceID
        {
            get { return _PurchaseInvoiceID; }
            set { _PurchaseInvoiceID = value; }
        }
        [Column(Name = "PurchaseInvoiceDate", DataType = "DateTime")]
        public DateTime PurchaseInvoiceDate
        {
            get { return _PurchaseInvoiceDate; }
            set { _PurchaseInvoiceDate = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "PurchaseInvoiceNo", DataType = "String")]
        public String PurchaseInvoiceNo
        {
            get { return _PurchaseInvoiceNo; }
            set { _PurchaseInvoiceNo = value; }
        }
        [Column(Name = "SupplierInvoiceNo", DataType = "String")]
        public String SupplierInvoiceNo
        {
            get { return _SupplierInvoiceNo; }
            set { _SupplierInvoiceNo = value; }
        }
        [Column(Name = "SupplierInvoiceDate", DataType = "DateTime")]
        public DateTime SupplierInvoiceDate
        {
            get { return _SupplierInvoiceDate; }
            set { _SupplierInvoiceDate = value; }
        }
        [Column(Name = "TaxInvoiceNo", DataType = "String", IsNullable = true)]
        public String TaxInvoiceNo
        {
            get { return _TaxInvoiceNo; }
            set { _TaxInvoiceNo = value; }
        }
        [Column(Name = "TaxInvoiceDate", DataType = "DateTime", IsNullable = true)]
        public DateTime TaxInvoiceDate
        {
            get { return _TaxInvoiceDate; }
            set { _TaxInvoiceDate = value; }
        }
        [Column(Name = "DueDate", DataType = "DateTime")]
        public DateTime DueDate
        {
            get { return _DueDate; }
            set { _DueDate = value; }
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
        [Column(Name = "TotalTransactionAmount", DataType = "Decimal")]
        public Decimal TotalTransactionAmount
        {
            get { return _TotalTransactionAmount; }
            set { _TotalTransactionAmount = value; }
        }
        [Column(Name = "TotalDownPaymentAmount", DataType = "Decimal")]
        public Decimal TotalDownPaymentAmount
        {
            get { return _TotalDownPaymentAmount; }
            set { _TotalDownPaymentAmount = value; }
        }
        [Column(Name = "TotalCreditNoteAmount", DataType = "Decimal")]
        public Decimal TotalCreditNoteAmount
        {
            get { return _TotalCreditNoteAmount; }
            set { _TotalCreditNoteAmount = value; }
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
        [Column(Name = "PPHPercentage", DataType = "Decimal")]
        public Decimal PPHPercentage
        {
            get { return _PPHPercentage; }
            set { _PPHPercentage = value; }
        }
        [Column(Name = "GCChargesType", DataType = "String", IsNullable = true)]
        public String GCChargesType
        {
            get { return _GCChargesType; }
            set { _GCChargesType = value; }
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
        [Column(Name = "TotalNetTransactionAmount", DataType = "Decimal")]
        public Decimal TotalNetTransactionAmount
        {
            get { return _TotalNetTransactionAmount; }
            set { _TotalNetTransactionAmount = value; }
        }
        [Column(Name = "NumberOfPayment", DataType = "Int16")]
        public Int16 NumberOfPayment
        {
            get { return _NumberOfPayment; }
            set { _NumberOfPayment = value; }
        }
        [Column(Name = "PaymentAmount", DataType = "Decimal")]
        public Decimal PaymentAmount
        {
            get { return _PaymentAmount; }
            set { _PaymentAmount = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsVerified", DataType = "Boolean")]
        public Boolean IsVerified
        {
            get { return _IsVerified; }
            set { _IsVerified = value; }
        }
        [Column(Name = "VerifiedBy", DataType = "Int32", IsNullable = true)]
        public Int32? VerifiedBy
        {
            get { return _VerifiedBy; }
            set { _VerifiedBy = value; }
        }
        [Column(Name = "VerifiedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime VerifiedDate
        {
            get { return _VerifiedDate; }
            set { _VerifiedDate = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String")]
        public String GCTransactionStatus
        {
            get { return _GCTransactionStatus; }
            set { _GCTransactionStatus = value; }
        }
        [Column(Name = "GCVoidReason", DataType = "String", IsNullable = true)]
        public String GCVoidReason
        {
            get { return _GCVoidReason; }
            set { _GCVoidReason = value; }
        }
        [Column(Name = "VoidReason", DataType = "String", IsNullable = true)]
        public String VoidReason
        {
            get { return _VoidReason; }
            set { _VoidReason = value; }
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

    public class PurchaseInvoiceHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PurchaseInvoiceHd));
        private bool _isAuditLog = false;
        private const string p_PurchaseInvoiceID = "@p_PurchaseInvoiceID";
        public PurchaseInvoiceHdDao() { }
        public PurchaseInvoiceHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PurchaseInvoiceHd Get(Int32 PurchaseInvoiceID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_PurchaseInvoiceID, PurchaseInvoiceID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PurchaseInvoiceHd)_helper.DataRowToObject(row, new PurchaseInvoiceHd());
        }
        public int Insert(PurchaseInvoiceHd record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PurchaseInvoiceHd record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 PurchaseInvoiceID)
        {
            PurchaseInvoiceHd record;
            if (_ctx.Transaction == null)
                record = new PurchaseInvoiceHdDao().Get(PurchaseInvoiceID);
            else
                record = Get(PurchaseInvoiceID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PurchaseInvoiceHdPayment
    [Serializable]
    [Table(Name = "PurchaseInvoiceHdPayment")]
    public class PurchaseInvoiceHdPayment : DbDataModel
    {
        private Int32 _ID;
        private Int32 _PurchaseInvoiceID;
        private Int32 _SupplierPaymentID;
        private DateTime _PaymentDate;
        private Decimal _PaymentAmount;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "PurchaseInvoiceID", DataType = "Int32")]
        public Int32 PurchaseInvoiceID
        {
            get { return _PurchaseInvoiceID; }
            set { _PurchaseInvoiceID = value; }
        }
        [Column(Name = "SupplierPaymentID", DataType = "Int32")]
        public Int32 SupplierPaymentID
        {
            get { return _SupplierPaymentID; }
            set { _SupplierPaymentID = value; }
        }
        [Column(Name = "PaymentDate", DataType = "DateTime")]
        public DateTime PaymentDate
        {
            get { return _PaymentDate; }
            set { _PaymentDate = value; }
        }
        [Column(Name = "PaymentAmount", DataType = "Decimal")]
        public Decimal PaymentAmount
        {
            get { return _PaymentAmount; }
            set { _PaymentAmount = value; }
        }
    }

    public class PurchaseInvoiceHdPaymentDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PurchaseInvoiceHdPayment));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public PurchaseInvoiceHdPaymentDao() { }
        public PurchaseInvoiceHdPaymentDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PurchaseInvoiceHdPayment Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PurchaseInvoiceHdPayment)_helper.DataRowToObject(row, new PurchaseInvoiceHdPayment());
        }
        public int Insert(PurchaseInvoiceHdPayment record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PurchaseInvoiceHdPayment record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            PurchaseInvoiceHdPayment record;
            if (_ctx.Transaction == null)
                record = new PurchaseInvoiceHdPaymentDao().Get(ID);
            else
                record = Get(ID);
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
    #region PurchaseReceiveDt
    [Serializable]
    [Table(Name = "PurchaseReceiveDt")]
    public class PurchaseReceiveDt : DbDataModel
    {
        private Int32 _ID;
        private Int32 _PurchaseReceiveID;
        private Int32? _PurchaseOrderID;
        private Int32 _ItemID;
        private Decimal _Quantity;
        private String _GCItemUnit;
        private String _GCBaseUnit;
        private Decimal _ConversionFactor;
        private Decimal _UnitPrice;
        private Decimal _DiscountPercentage1;
        private Decimal _DiscountPercentage2;
        private Boolean _IsBonusItem;
        private Boolean _IsControlExpired;
        private String _GCItemDetailStatus;
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
        [Column(Name = "PurchaseReceiveID", DataType = "Int32")]
        public Int32 PurchaseReceiveID
        {
            get { return _PurchaseReceiveID; }
            set { _PurchaseReceiveID = value; }
        }
        [Column(Name = "PurchaseOrderID", DataType = "Int32", IsNullable = true)]
        public Int32? PurchaseOrderID
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

    public class PurchaseReceiveDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PurchaseReceiveDt));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public PurchaseReceiveDtDao() { }
        public PurchaseReceiveDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PurchaseReceiveDt Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PurchaseReceiveDt)_helper.DataRowToObject(row, new PurchaseReceiveDt());
        }
        public int Insert(PurchaseReceiveDt record)
        {
            record.CreatedDate = record.LastUpdatedDate = DateTime.Now;
            record.LastUpdatedBy = record.CreatedBy;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PurchaseReceiveDt record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            PurchaseReceiveDt record;
            if (_ctx.Transaction == null)
                record = new PurchaseReceiveDtDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PurchaseReceiveDtExpired
    [Serializable]
    [Table(Name = "PurchaseReceiveDtExpired")]
    public partial class PurchaseReceiveDtExpired : DbDataModel
    {
        private Int32 _ID;
        private String _BatchNumber;
        private DateTime _ExpiredDate;
        private Decimal _Quantity;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "BatchNumber", DataType = "String", IsPrimaryKey = true)]
        public String BatchNumber
        {
            get { return _BatchNumber; }
            set { _BatchNumber = value; }
        }
        [Column(Name = "ExpiredDate", DataType = "DateTime")]
        public DateTime ExpiredDate
        {
            get { return _ExpiredDate; }
            set { _ExpiredDate = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
    }

    public class PurchaseReceiveDtExpiredDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PurchaseReceiveDtExpired));
        private bool _isAuditLog = false;
        private const string p_BatchNumber = "@p_BatchNumber";
        private const string p_ID = "@p_ID";
        public PurchaseReceiveDtExpiredDao() { }
        public PurchaseReceiveDtExpiredDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PurchaseReceiveDtExpired Get(Int32 ID, String BatchNumber)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_BatchNumber, BatchNumber);
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PurchaseReceiveDtExpired)_helper.DataRowToObject(row, new PurchaseReceiveDtExpired());
        }
        public int Insert(PurchaseReceiveDtExpired record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PurchaseReceiveDtExpired record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID, String BatchNumber)
        {
            PurchaseReceiveDtExpired record;
            if (_ctx.Transaction == null)
                record = new PurchaseReceiveDtExpiredDao().Get(ID, BatchNumber);
            else
                record = Get(ID, BatchNumber);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PurchaseReceiveHd
    [Serializable]
    [Table(Name = "PurchaseReceiveHd")]
    public class PurchaseReceiveHd : DbDataModel
    {
        private Int32 _PurchaseReceiveID;
        private DateTime _ReceivedDate;
        private String _ReceivedTime;
        private String _PurchaseReceiveNo;
        private Int32 _LocationID;
        private Int32 _BusinessPartnerID;
        private Int32 _TermID;
        private String _ReferenceNo;
        private DateTime _ReferenceDate;
        private DateTime _PaymentDueDate;
        private String _GCCurrencyCode;
        private Decimal _CurrencyRate;
        private Boolean _IsIncludeVAT;
        private Decimal _TransactionAmount;
        private Decimal _DiscountAmount;
        private Decimal _FinalDiscount;
        private String _GCChargesType;
        private Decimal _ChargesAmount;
        private Decimal _StampAmount;
        private Decimal _VATPercentage;
        private Decimal _DownPaymentAmount;
        private String _DownPaymentReferenceNo;
        private Decimal _NetTransactionAmount;
        private String _ReceivedBy;
        private String _Remarks;
        private String _GCTransactionStatus;
        private Boolean _IsHasPurchaseReturn;
        private Int32? _PurchaseReturnID;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "PurchaseReceiveID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 PurchaseReceiveID
        {
            get { return _PurchaseReceiveID; }
            set { _PurchaseReceiveID = value; }
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
        [Column(Name = "PurchaseReceiveNo", DataType = "String")]
        public String PurchaseReceiveNo
        {
            get { return _PurchaseReceiveNo; }
            set { _PurchaseReceiveNo = value; }
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
        [Column(Name = "TermID", DataType = "Int32")]
        public Int32 TermID
        {
            get { return _TermID; }
            set { _TermID = value; }
        }
        [Column(Name = "ReferenceNo", DataType = "String")]
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
        [Column(Name = "PaymentDueDate", DataType = "DateTime")]
        public DateTime PaymentDueDate
        {
            get { return _PaymentDueDate; }
            set { _PaymentDueDate = value; }
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
        [Column(Name = "DownPaymentReferenceNo", DataType = "String", IsNullable = true)]
        public String DownPaymentReferenceNo
        {
            get { return _DownPaymentReferenceNo; }
            set { _DownPaymentReferenceNo = value; }
        }
        [Column(Name = "NetTransactionAmount", DataType = "Decimal")]
        public Decimal NetTransactionAmount
        {
            get { return _NetTransactionAmount; }
            set { _NetTransactionAmount = value; }
        }
        [Column(Name = "ReceivedBy", DataType = "String", IsNullable = true)]
        public String ReceivedBy
        {
            get { return _ReceivedBy; }
            set { _ReceivedBy = value; }
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
        [Column(Name = "IsHasPurchaseReturn", DataType = "Boolean")]
        public Boolean IsHasPurchaseReturn
        {
            get { return _IsHasPurchaseReturn; }
            set { _IsHasPurchaseReturn = value; }
        }
        [Column(Name = "PurchaseReturnID", DataType = "Int32", IsNullable = true)]
        public Int32? PurchaseReturnID
        {
            get { return _PurchaseReturnID; }
            set { _PurchaseReturnID = value; }
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

    public class PurchaseReceiveHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PurchaseReceiveHd));
        private bool _isAuditLog = false;
        private const string p_PurchaseReceiveID = "@p_PurchaseReceiveID";
        public PurchaseReceiveHdDao() { }
        public PurchaseReceiveHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PurchaseReceiveHd Get(Int32 PurchaseReceiveID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_PurchaseReceiveID, PurchaseReceiveID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PurchaseReceiveHd)_helper.DataRowToObject(row, new PurchaseReceiveHd());
        }
        public int Insert(PurchaseReceiveHd record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PurchaseReceiveHd record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 PurchaseReceiveID)
        {
            PurchaseReceiveHd record;
            if (_ctx.Transaction == null)
                record = new PurchaseReceiveHdDao().Get(PurchaseReceiveID);
            else
                record = Get(PurchaseReceiveID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PurchaseReplacementDt
    [Serializable]
    [Table(Name = "PurchaseReplacementDt")]
    public class PurchaseReplacementDt : DbDataModel
    {
        private Int32 _ID;
        private Int32 _PurchaseReplacementID;
        private Int32 _FromItemID;
        private Int32 _ToItemID;
        private Decimal _Quantity;
        private String _GCItemUnit;
        private String _GCBaseUnit;
        private Decimal _ConversionFactor;
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
        [Column(Name = "PurchaseReplacementID", DataType = "Int32")]
        public Int32 PurchaseReplacementID
        {
            get { return _PurchaseReplacementID; }
            set { _PurchaseReplacementID = value; }
        }
        [Column(Name = "FromItemID", DataType = "Int32")]
        public Int32 FromItemID
        {
            get { return _FromItemID; }
            set { _FromItemID = value; }
        }
        [Column(Name = "ToItemID", DataType = "Int32")]
        public Int32 ToItemID
        {
            get { return _ToItemID; }
            set { _ToItemID = value; }
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

    public class PurchaseReplacementDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PurchaseReplacementDt));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public PurchaseReplacementDtDao() { }
        public PurchaseReplacementDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PurchaseReplacementDt Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PurchaseReplacementDt)_helper.DataRowToObject(row, new PurchaseReplacementDt());
        }
        public int Insert(PurchaseReplacementDt record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PurchaseReplacementDt record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            PurchaseReplacementDt record;
            if (_ctx.Transaction == null)
                record = new PurchaseReplacementDtDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PurchaseReplacementHd
    [Serializable]
    [Table(Name = "PurchaseReplacementHd")]
    public class PurchaseReplacementHd : DbDataModel
    {
        private Int32 _PurchaseReplacementID;
        private String _PurchaseReplacementNo;
        private DateTime _ReplacementDate;
        private Int32 _PurchaseReturnID;
        private Int32 _LocationID;
        private Int32 _BusinessPartnerID;
        private String _ReferenceNo;
        private DateTime _ReferenceDate;
        private String _Remarks;
        private String _GCTransactionStatus;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "PurchaseReplacementID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 PurchaseReplacementID
        {
            get { return _PurchaseReplacementID; }
            set { _PurchaseReplacementID = value; }
        }
        [Column(Name = "PurchaseReplacementNo", DataType = "String")]
        public String PurchaseReplacementNo
        {
            get { return _PurchaseReplacementNo; }
            set { _PurchaseReplacementNo = value; }
        }
        [Column(Name = "ReplacementDate", DataType = "DateTime")]
        public DateTime ReplacementDate
        {
            get { return _ReplacementDate; }
            set { _ReplacementDate = value; }
        }
        [Column(Name = "PurchaseReturnID", DataType = "Int32")]
        public Int32 PurchaseReturnID
        {
            get { return _PurchaseReturnID; }
            set { _PurchaseReturnID = value; }
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
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCTransactionStatus", DataType = "String", IsNullable = true)]
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

    public class PurchaseReplacementHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PurchaseReplacementHd));
        private bool _isAuditLog = false;
        private const string p_PurchaseReplacementID = "@p_PurchaseReplacementID";
        public PurchaseReplacementHdDao() { }
        public PurchaseReplacementHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PurchaseReplacementHd Get(Int32 PurchaseReplacementID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_PurchaseReplacementID, PurchaseReplacementID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PurchaseReplacementHd)_helper.DataRowToObject(row, new PurchaseReplacementHd());
        }
        public int Insert(PurchaseReplacementHd record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PurchaseReplacementHd record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 PurchaseReplacementID)
        {
            PurchaseReplacementHd record;
            if (_ctx.Transaction == null)
                record = new PurchaseReplacementHdDao().Get(PurchaseReplacementID);
            else
                record = Get(PurchaseReplacementID);
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
    #region PurchaseReturnDt
    [Serializable]
    [Table(Name = "PurchaseReturnDt")]
    public class PurchaseReturnDt : DbDataModel
    {
        private Int32 _ID;
        private Int32 _PurchaseReturnID;
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
        [Column(Name = "PurchaseReturnID", DataType = "Int32")]
        public Int32 PurchaseReturnID
        {
            get { return _PurchaseReturnID; }
            set { _PurchaseReturnID = value; }
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

    public class PurchaseReturnDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PurchaseReturnDt));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public PurchaseReturnDtDao() { }
        public PurchaseReturnDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PurchaseReturnDt Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PurchaseReturnDt)_helper.DataRowToObject(row, new PurchaseReturnDt());
        }
        public int Insert(PurchaseReturnDt record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PurchaseReturnDt record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            PurchaseReturnDt record;
            if (_ctx.Transaction == null)
                record = new PurchaseReturnDtDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region PurchaseReturnHd
    [Serializable]
    [Table(Name = "PurchaseReturnHd")]
    public class PurchaseReturnHd : DbDataModel
    {
        private Int32 _PurchaseReturnID;
        private DateTime _ReturnDate;
        private String _PurchaseReturnNo;
        private Int32 _PurchaseReceiveID;
        private Int32 _LocationID;
        private Int32 _BusinessPartnerID;
        private String _GCPurchaseReturnType;
        private String _ReferenceNo;
        private DateTime _ReferenceDate;
        private Boolean _IsIncludeVAT;
        private Decimal _TransactionAmount;
        private Decimal _VATPercentage;
        private Boolean _IsAutoUpdateStock;
        private String _Remarks;
        private String _GCTransactionStatus;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "PurchaseReturnID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 PurchaseReturnID
        {
            get { return _PurchaseReturnID; }
            set { _PurchaseReturnID = value; }
        }
        [Column(Name = "ReturnDate", DataType = "DateTime")]
        public DateTime ReturnDate
        {
            get { return _ReturnDate; }
            set { _ReturnDate = value; }
        }
        [Column(Name = "PurchaseReturnNo", DataType = "String")]
        public String PurchaseReturnNo
        {
            get { return _PurchaseReturnNo; }
            set { _PurchaseReturnNo = value; }
        }
        [Column(Name = "PurchaseReceiveID", DataType = "Int32")]
        public Int32 PurchaseReceiveID
        {
            get { return _PurchaseReceiveID; }
            set { _PurchaseReceiveID = value; }
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
        [Column(Name = "GCPurchaseReturnType", DataType = "String")]
        public String GCPurchaseReturnType
        {
            get { return _GCPurchaseReturnType; }
            set { _GCPurchaseReturnType = value; }
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
        [Column(Name = "IsAutoUpdateStock", DataType = "Boolean")]
        public Boolean IsAutoUpdateStock
        {
            get { return _IsAutoUpdateStock; }
            set { _IsAutoUpdateStock = value; }
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

    public class PurchaseReturnHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(PurchaseReturnHd));
        private bool _isAuditLog = false;
        private const string p_PurchaseReturnID = "@p_PurchaseReturnID";
        public PurchaseReturnHdDao() { }
        public PurchaseReturnHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public PurchaseReturnHd Get(Int32 PurchaseReturnID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_PurchaseReturnID, PurchaseReturnID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (PurchaseReturnHd)_helper.DataRowToObject(row, new PurchaseReturnHd());
        }
        public int Insert(PurchaseReturnHd record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(PurchaseReturnHd record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 PurchaseReturnID)
        {
            PurchaseReturnHd record;
            if (_ctx.Transaction == null)
                record = new PurchaseReturnHdDao().Get(PurchaseReturnID);
            else
                record = Get(PurchaseReturnID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Registration
    [Serializable]
    [Table(Name = "Registration")]
    public class Registration : DbDataModel
    {
        private Int32 _RegistrationID;
        private String _RegistrationNo;
        private DateTime _RegistrationDate;
        private String _RegistrationTime;
        private Int32? _PeriodAdmissionID;
        private Int32 _ProspectiveStudentID;
        private String _GCRegistrationType;
        private String _GCInformationSource;
        private Decimal _FinalMark;
        private String _Remarks;
        private String _GCRegistrationStatus;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "RegistrationID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 RegistrationID
        {
            get { return _RegistrationID; }
            set { _RegistrationID = value; }
        }
        [Column(Name = "RegistrationNo", DataType = "String")]
        public String RegistrationNo
        {
            get { return _RegistrationNo; }
            set { _RegistrationNo = value; }
        }
        [Column(Name = "RegistrationDate", DataType = "DateTime")]
        public DateTime RegistrationDate
        {
            get { return _RegistrationDate; }
            set { _RegistrationDate = value; }
        }
        [Column(Name = "RegistrationTime", DataType = "String")]
        public String RegistrationTime
        {
            get { return _RegistrationTime; }
            set { _RegistrationTime = value; }
        }
        [Column(Name = "PeriodAdmissionID", DataType = "Int32", IsNullable = true)]
        public Int32? PeriodAdmissionID
        {
            get { return _PeriodAdmissionID; }
            set { _PeriodAdmissionID = value; }
        }
        [Column(Name = "ProspectiveStudentID", DataType = "Int32")]
        public Int32 ProspectiveStudentID
        {
            get { return _ProspectiveStudentID; }
            set { _ProspectiveStudentID = value; }
        }
        [Column(Name = "GCRegistrationType", DataType = "String")]
        public String GCRegistrationType
        {
            get { return _GCRegistrationType; }
            set { _GCRegistrationType = value; }
        }
        [Column(Name = "GCInformationSource", DataType = "String")]
        public String GCInformationSource
        {
            get { return _GCInformationSource; }
            set { _GCInformationSource = value; }
        }
        [Column(Name = "FinalMark", DataType = "Decimal")]
        public Decimal FinalMark
        {
            get { return _FinalMark; }
            set { _FinalMark = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCRegistrationStatus", DataType = "String")]
        public String GCRegistrationStatus
        {
            get { return _GCRegistrationStatus; }
            set { _GCRegistrationStatus = value; }
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

    public class RegistrationDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Registration));
        private bool _isAuditLog = false;
        private const string p_RegistrationID = "@p_RegistrationID";
        public RegistrationDao() { }
        public RegistrationDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Registration Get(Int32 RegistrationID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_RegistrationID, RegistrationID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Registration)_helper.DataRowToObject(row, new Registration());
        }
        public int Insert(Registration record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Registration record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 RegistrationID)
        {
            Registration record;
            if (_ctx.Transaction == null)
                record = new RegistrationDao().Get(RegistrationID);
            else
                record = Get(RegistrationID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region RegistrationMark
    [Serializable]
    [Table(Name = "RegistrationMark")]
    public class RegistrationMark : DbDataModel
    {
        private Int32 _PeriodAdmissionID;
        private Int32 _AdmissionSelectionID;
        private Int32 _RegistrationID;
        private Decimal _Mark;

        [Column(Name = "PeriodAdmissionID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 PeriodAdmissionID
        {
            get { return _PeriodAdmissionID; }
            set { _PeriodAdmissionID = value; }
        }
        [Column(Name = "AdmissionSelectionID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 AdmissionSelectionID
        {
            get { return _AdmissionSelectionID; }
            set { _AdmissionSelectionID = value; }
        }
        [Column(Name = "RegistrationID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 RegistrationID
        {
            get { return _RegistrationID; }
            set { _RegistrationID = value; }
        }
        [Column(Name = "Mark", DataType = "Decimal")]
        public Decimal Mark
        {
            get { return _Mark; }
            set { _Mark = value; }
        }
    }

    public class RegistrationMarkDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(RegistrationMark));
        private bool _isAuditLog = false;
        private const string p_AdmissionSelectionID = "@p_AdmissionSelectionID";
        private const string p_PeriodAdmissionID = "@p_PeriodAdmissionID";
        private const string p_RegistrationID = "@p_RegistrationID";
        public RegistrationMarkDao() { }
        public RegistrationMarkDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public RegistrationMark Get(Int32 PeriodAdmissionID, Int32 AdmissionSelectionID, Int32 RegistrationID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_AdmissionSelectionID, AdmissionSelectionID);
            _ctx.Add(p_PeriodAdmissionID, PeriodAdmissionID);
            _ctx.Add(p_RegistrationID, RegistrationID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (RegistrationMark)_helper.DataRowToObject(row, new RegistrationMark());
        }
        public int Insert(RegistrationMark record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(RegistrationMark record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 PeriodAdmissionID, Int32 AdmissionSelectionID, Int32 RegistrationID)
        {
            RegistrationMark record;
            if (_ctx.Transaction == null)
                record = new RegistrationMarkDao().Get(PeriodAdmissionID, AdmissionSelectionID, RegistrationID);
            else
                record = Get(PeriodAdmissionID, AdmissionSelectionID, RegistrationID);
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
    #region StockTakingDt
    [Serializable]
    [Table(Name = "StockTakingDt")]
    public class StockTakingDt : DbDataModel
    {
        private Int32 _StockTakingID;
        private Int32 _ItemID;
        private Int32 _MovementID;
        private DateTime _StartDate;
        private String _StartTime;
        private DateTime _EndDate;
        private String _EndTime;
        private Decimal _QuantityBSO;
        private Decimal _QuantityAdjustment;
        private Decimal _QuantityEND;
        private String _GCItemUnit;
        private String _GCCheckCountType;
        private String _GCItemDetailStatus;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "StockTakingID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 StockTakingID
        {
            get { return _StockTakingID; }
            set { _StockTakingID = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "MovementID", DataType = "Int32")]
        public Int32 MovementID
        {
            get { return _MovementID; }
            set { _MovementID = value; }
        }
        [Column(Name = "StartDate", DataType = "DateTime", IsNullable = true)]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Column(Name = "StartTime", DataType = "String", IsNullable = true)]
        public String StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        [Column(Name = "EndDate", DataType = "DateTime", IsNullable = true)]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        [Column(Name = "EndTime", DataType = "String", IsNullable = true)]
        public String EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        [Column(Name = "QuantityBSO", DataType = "Decimal")]
        public Decimal QuantityBSO
        {
            get { return _QuantityBSO; }
            set { _QuantityBSO = value; }
        }
        [Column(Name = "QuantityAdjustment", DataType = "Decimal", IsNullable = true)]
        public Decimal QuantityAdjustment
        {
            get { return _QuantityAdjustment; }
            set { _QuantityAdjustment = value; }
        }
        [Column(Name = "QuantityEND", DataType = "Decimal", IsNullable = true)]
        public Decimal QuantityEND
        {
            get { return _QuantityEND; }
            set { _QuantityEND = value; }
        }
        [Column(Name = "GCItemUnit", DataType = "String")]
        public String GCItemUnit
        {
            get { return _GCItemUnit; }
            set { _GCItemUnit = value; }
        }
        [Column(Name = "GCCheckCountType", DataType = "String", IsNullable = true)]
        public String GCCheckCountType
        {
            get { return _GCCheckCountType; }
            set { _GCCheckCountType = value; }
        }
        [Column(Name = "GCItemDetailStatus", DataType = "String", IsNullable = true)]
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

    public class StockTakingDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(StockTakingDt));
        private bool _isAuditLog = false;
        private const string p_ItemID = "@p_ItemID";
        private const string p_StockTakingID = "@p_StockTakingID";
        public StockTakingDtDao() { }
        public StockTakingDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public StockTakingDt Get(Int32 StockTakingID, Int32 ItemID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ItemID, ItemID);
            _ctx.Add(p_StockTakingID, StockTakingID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (StockTakingDt)_helper.DataRowToObject(row, new StockTakingDt());
        }
        public int Insert(StockTakingDt record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(StockTakingDt record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 StockTakingID, Int32 ItemID)
        {
            StockTakingDt record;
            if (_ctx.Transaction == null)
                record = new StockTakingDtDao().Get(StockTakingID, ItemID);
            else
                record = Get(StockTakingID, ItemID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region StockTakingDtExpired
    [Serializable]
    [Table(Name = "StockTakingDtExpired")]
    public partial class StockTakingDtExpired : DbDataModel
    {
        private Int32 _StockTakingID;
        private Int32 _ItemID;
        private String _BatchNumber;
        private DateTime _ExpiredDate;
        private Decimal _Quantity;

        [Column(Name = "StockTakingID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 StockTakingID
        {
            get { return _StockTakingID; }
            set { _StockTakingID = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "BatchNumber", DataType = "String", IsPrimaryKey = true)]
        public String BatchNumber
        {
            get { return _BatchNumber; }
            set { _BatchNumber = value; }
        }
        [Column(Name = "ExpiredDate", DataType = "DateTime")]
        public DateTime ExpiredDate
        {
            get { return _ExpiredDate; }
            set { _ExpiredDate = value; }
        }
        [Column(Name = "Quantity", DataType = "Decimal")]
        public Decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
    }

    public class StockTakingDtExpiredDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(StockTakingDtExpired));
        private bool _isAuditLog = false;
        private const string p_BatchNumber = "@p_BatchNumber";
        private const string p_ItemID = "@p_ItemID";
        private const string p_StockTakingID = "@p_StockTakingID";
        public StockTakingDtExpiredDao() { }
        public StockTakingDtExpiredDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public StockTakingDtExpired Get(Int32 StockTakingID, Int32 ItemID, String BatchNumber)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_BatchNumber, BatchNumber);
            _ctx.Add(p_ItemID, ItemID);
            _ctx.Add(p_StockTakingID, StockTakingID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (StockTakingDtExpired)_helper.DataRowToObject(row, new StockTakingDtExpired());
        }
        public int Insert(StockTakingDtExpired record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(StockTakingDtExpired record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 StockTakingID, Int32 ItemID, String BatchNumber)
        {
            StockTakingDtExpired record;
            if (_ctx.Transaction == null)
                record = new StockTakingDtExpiredDao().Get(StockTakingID, ItemID, BatchNumber);
            else
                record = Get(StockTakingID, ItemID, BatchNumber);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region StockTakingHd
    [Serializable]
    [Table(Name = "StockTakingHd")]
    public class StockTakingHd : DbDataModel
    {
        private Int32 _StockTakingID;
        private String _StockTakingNo;
        private DateTime _FormDate;
        private Int32 _LocationID;
        private String _Remarks;
        private String _GCTransactionStatus;
        private String _GCVoidReason;
        private String _VoidReason;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "StockTakingID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 StockTakingID
        {
            get { return _StockTakingID; }
            set { _StockTakingID = value; }
        }
        [Column(Name = "StockTakingNo", DataType = "String")]
        public String StockTakingNo
        {
            get { return _StockTakingNo; }
            set { _StockTakingNo = value; }
        }
        [Column(Name = "FormDate", DataType = "DateTime")]
        public DateTime FormDate
        {
            get { return _FormDate; }
            set { _FormDate = value; }
        }
        [Column(Name = "LocationID", DataType = "Int32")]
        public Int32 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
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
        [Column(Name = "GCVoidReason", DataType = "String", IsNullable = true)]
        public String GCVoidReason
        {
            get { return _GCVoidReason; }
            set { _GCVoidReason = value; }
        }
        [Column(Name = "VoidReason", DataType = "String", IsNullable = true)]
        public String VoidReason
        {
            get { return _VoidReason; }
            set { _VoidReason = value; }
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

    public class StockTakingHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(StockTakingHd));
        private bool _isAuditLog = false;
        private const string p_StockTakingID = "@p_StockTakingID";
        public StockTakingHdDao() { }
        public StockTakingHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public StockTakingHd Get(Int32 StockTakingID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_StockTakingID, StockTakingID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (StockTakingHd)_helper.DataRowToObject(row, new StockTakingHd());
        }
        public int Insert(StockTakingHd record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(StockTakingHd record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 StockTakingID)
        {
            StockTakingHd record;
            if (_ctx.Transaction == null)
                record = new StockTakingHdDao().Get(StockTakingID);
            else
                record = Get(StockTakingID);
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
        private String _GCReligion;
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
        [Column(Name = "GCReligion", DataType = "String", IsNullable = true)]
        public String GCReligion
        {
            get { return _GCReligion; }
            set { _GCReligion = value; }
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
    #region StudentFamily
    [Serializable]
    [Table(Name = "StudentFamily")]
    public class StudentFamily : DbDataModel
    {
        private Int32 _FamilyID;
        private Int32 _StudentID;
        private String _GCFamilyRelation;
        private String _GCSalutation;
        private String _GCTitle;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _FullName;
        private String _FamilyName;
        private String _CityOfBirth;
        private DateTime _DateOfBirth;
        private String _GCSuffix;
        private String _GCReligion;
        private String _GCNationality;
        private String _GCEducationLevel;
        private String _CompanyName;
        private String _GCJob;
        private String _Occupation;
        private Decimal _Salary;
        private Int32? _OfficeAddressID;
        private String _EmailAddress;
        private String _MobilePhoneNo1;
        private String _MobilePhoneNo2;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "FamilyID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 FamilyID
        {
            get { return _FamilyID; }
            set { _FamilyID = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "GCFamilyRelation", DataType = "String")]
        public String GCFamilyRelation
        {
            get { return _GCFamilyRelation; }
            set { _GCFamilyRelation = value; }
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
        [Column(Name = "FullName", DataType = "String", IsNullable = true)]
        public String FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
        [Column(Name = "FamilyName", DataType = "String", IsNullable = true)]
        public String FamilyName
        {
            get { return _FamilyName; }
            set { _FamilyName = value; }
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
        [Column(Name = "GCSuffix", DataType = "String", IsNullable = true)]
        public String GCSuffix
        {
            get { return _GCSuffix; }
            set { _GCSuffix = value; }
        }
        [Column(Name = "GCReligion", DataType = "String", IsNullable = true)]
        public String GCReligion
        {
            get { return _GCReligion; }
            set { _GCReligion = value; }
        }
        [Column(Name = "GCNationality", DataType = "String", IsNullable = true)]
        public String GCNationality
        {
            get { return _GCNationality; }
            set { _GCNationality = value; }
        }
        [Column(Name = "GCEducationLevel", DataType = "String", IsNullable = true)]
        public String GCEducationLevel
        {
            get { return _GCEducationLevel; }
            set { _GCEducationLevel = value; }
        }
        [Column(Name = "CompanyName", DataType = "String", IsNullable = true)]
        public String CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        [Column(Name = "GCJob", DataType = "String", IsNullable = true)]
        public String GCJob
        {
            get { return _GCJob; }
            set { _GCJob = value; }
        }
        [Column(Name = "Occupation", DataType = "String", IsNullable = true)]
        public String Occupation
        {
            get { return _Occupation; }
            set { _Occupation = value; }
        }
        [Column(Name = "Salary", DataType = "Decimal", IsNullable = true)]
        public Decimal Salary
        {
            get { return _Salary; }
            set { _Salary = value; }
        }
        [Column(Name = "OfficeAddressID", DataType = "Int32", IsNullable = true)]
        public Int32? OfficeAddressID
        {
            get { return _OfficeAddressID; }
            set { _OfficeAddressID = value; }
        }
        [Column(Name = "EmailAddress", DataType = "String", IsNullable = true)]
        public String EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
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

    public class StudentFamilyDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(StudentFamily));
        private bool _isAuditLog = false;
        private const string p_FamilyID = "@p_FamilyID";
        public StudentFamilyDao() { }
        public StudentFamilyDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public StudentFamily Get(Int32 FamilyID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_FamilyID, FamilyID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (StudentFamily)_helper.DataRowToObject(row, new StudentFamily());
        }
        public int Insert(StudentFamily record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(StudentFamily record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 FamilyID)
        {
            StudentFamily record;
            if (_ctx.Transaction == null)
                record = new StudentFamilyDao().Get(FamilyID);
            else
                record = Get(FamilyID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region StudentPastStudy
    [Serializable]
    [Table(Name = "StudentPastStudy")]
    public class StudentPastStudy : DbDataModel
    {
        private Int32 _StudentPastStudyID;
        private Int32 _StudentID;
        private Int32 _StartYear;
        private Int32 _EndYear;
        private String _GCSchoolType;
        private String _SchoolName;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "StudentPastStudyID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 StudentPastStudyID
        {
            get { return _StudentPastStudyID; }
            set { _StudentPastStudyID = value; }
        }
        [Column(Name = "StudentID", DataType = "Int32")]
        public Int32 StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }
        [Column(Name = "StartYear", DataType = "Int32")]
        public Int32 StartYear
        {
            get { return _StartYear; }
            set { _StartYear = value; }
        }
        [Column(Name = "EndYear", DataType = "Int32")]
        public Int32 EndYear
        {
            get { return _EndYear; }
            set { _EndYear = value; }
        }
        [Column(Name = "GCSchoolType", DataType = "String")]
        public String GCSchoolType
        {
            get { return _GCSchoolType; }
            set { _GCSchoolType = value; }
        }
        [Column(Name = "SchoolName", DataType = "String")]
        public String SchoolName
        {
            get { return _SchoolName; }
            set { _SchoolName = value; }
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

    public class StudentPastStudyDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(StudentPastStudy));
        private bool _isAuditLog = false;
        private const string p_StudentPastStudyID = "@p_StudentPastStudyID";
        public StudentPastStudyDao() { }
        public StudentPastStudyDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public StudentPastStudy Get(Int32 StudentPastStudyID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_StudentPastStudyID, StudentPastStudyID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (StudentPastStudy)_helper.DataRowToObject(row, new StudentPastStudy());
        }
        public int Insert(StudentPastStudy record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(StudentPastStudy record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 StudentPastStudyID)
        {
            StudentPastStudy record;
            if (_ctx.Transaction == null)
                record = new StudentPastStudyDao().Get(StudentPastStudyID);
            else
                record = Get(StudentPastStudyID);
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
    #region SubjectMatter
    [Serializable]
    [Table(Name = "SubjectMatter")]
    public class SubjectMatter : DbDataModel
    {
        private Int32 _SubjectMatterID;
        private Int32 _SubjectID;
        private String _GCGrade;
        private String _GCMajor;
        private Int16 _MeetingNo;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "SubjectMatterID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 SubjectMatterID
        {
            get { return _SubjectMatterID; }
            set { _SubjectMatterID = value; }
        }
        [Column(Name = "SubjectID", DataType = "Int32")]
        public Int32 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
        [Column(Name = "GCGrade", DataType = "String")]
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
        [Column(Name = "MeetingNo", DataType = "Int16")]
        public Int16 MeetingNo
        {
            get { return _MeetingNo; }
            set { _MeetingNo = value; }
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

    public class SubjectMatterDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(SubjectMatter));
        private bool _isAuditLog = false;
        private const string p_SubjectMatterID = "@p_SubjectMatterID";
        public SubjectMatterDao() { }
        public SubjectMatterDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public SubjectMatter Get(Int32 SubjectMatterID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_SubjectMatterID, SubjectMatterID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (SubjectMatter)_helper.DataRowToObject(row, new SubjectMatter());
        }
        public int Insert(SubjectMatter record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(SubjectMatter record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 SubjectMatterID)
        {
            SubjectMatter record;
            if (_ctx.Transaction == null)
                record = new SubjectMatterDao().Get(SubjectMatterID);
            else
                record = Get(SubjectMatterID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region SubLedgerType
    [Serializable]
    [Table(Name = "SubLedgerType")]
    public class SubLedgerType : DbDataModel
    {
        private Int32 _SubLedgerTypeID;
        private String _SubLedgerTypeCode;
        private String _SubLedgerTypeName;
        private String _MethodName;
        private String _FilterExpression;
        private String _IDFieldName;
        private String _CodeFieldName;
        private String _DisplayFieldName;
        private String _SearchDialogTypeName;
        private String _TableName;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "SubLedgerTypeID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 SubLedgerTypeID
        {
            get { return _SubLedgerTypeID; }
            set { _SubLedgerTypeID = value; }
        }
        [Column(Name = "SubLedgerTypeCode", DataType = "String")]
        public String SubLedgerTypeCode
        {
            get { return _SubLedgerTypeCode; }
            set { _SubLedgerTypeCode = value; }
        }
        [Column(Name = "SubLedgerTypeName", DataType = "String")]
        public String SubLedgerTypeName
        {
            get { return _SubLedgerTypeName; }
            set { _SubLedgerTypeName = value; }
        }
        [Column(Name = "MethodName", DataType = "String")]
        public String MethodName
        {
            get { return _MethodName; }
            set { _MethodName = value; }
        }
        [Column(Name = "FilterExpression", DataType = "String")]
        public String FilterExpression
        {
            get { return _FilterExpression; }
            set { _FilterExpression = value; }
        }
        [Column(Name = "IDFieldName", DataType = "String", IsNullable = true)]
        public String IDFieldName
        {
            get { return _IDFieldName; }
            set { _IDFieldName = value; }
        }
        [Column(Name = "CodeFieldName", DataType = "String", IsNullable = true)]
        public String CodeFieldName
        {
            get { return _CodeFieldName; }
            set { _CodeFieldName = value; }
        }
        [Column(Name = "DisplayFieldName", DataType = "String", IsNullable = true)]
        public String DisplayFieldName
        {
            get { return _DisplayFieldName; }
            set { _DisplayFieldName = value; }
        }
        [Column(Name = "SearchDialogTypeName", DataType = "String", IsNullable = true)]
        public String SearchDialogTypeName
        {
            get { return _SearchDialogTypeName; }
            set { _SearchDialogTypeName = value; }
        }
        [Column(Name = "TableName", DataType = "String", IsNullable = true)]
        public String TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
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

    public class SubLedgerTypeDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(SubLedgerType));
        private bool _isAuditLog = false;
        private const string p_SubLedgerTypeID = "@p_SubLedgerTypeID";
        public SubLedgerTypeDao() { }
        public SubLedgerTypeDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public SubLedgerType Get(Int32 SubLedgerTypeID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_SubLedgerTypeID, SubLedgerTypeID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (SubLedgerType)_helper.DataRowToObject(row, new SubLedgerType());
        }
        public int Insert(SubLedgerType record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(SubLedgerType record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 SubLedgerTypeID)
        {
            SubLedgerType record;
            if (_ctx.Transaction == null)
                record = new SubLedgerTypeDao().Get(SubLedgerTypeID);
            else
                record = Get(SubLedgerTypeID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region SubLedgerDt
    [Serializable]
    [Table(Name = "SubLedgerDt")]
    public class SubLedgerDt : DbDataModel
    {
        private Int32 _SubLedgerDtID;
        private Int32 _SubLedgerID;
        private String _SubLedgerDtCode;
        private String _SubLedgerDtName;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "SubLedgerDtID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 SubLedgerDtID
        {
            get { return _SubLedgerDtID; }
            set { _SubLedgerDtID = value; }
        }
        [Column(Name = "SubLedgerID", DataType = "Int32")]
        public Int32 SubLedgerID
        {
            get { return _SubLedgerID; }
            set { _SubLedgerID = value; }
        }
        [Column(Name = "SubLedgerDtCode", DataType = "String")]
        public String SubLedgerDtCode
        {
            get { return _SubLedgerDtCode; }
            set { _SubLedgerDtCode = value; }
        }
        [Column(Name = "SubLedgerDtName", DataType = "String")]
        public String SubLedgerDtName
        {
            get { return _SubLedgerDtName; }
            set { _SubLedgerDtName = value; }
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

    public class SubLedgerDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(SubLedgerDt));
        private bool _isAuditLog = false;
        private const string p_SubLedgerDtID = "@p_SubLedgerDtID";
        public SubLedgerDtDao() { }
        public SubLedgerDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public SubLedgerDt Get(Int32 SubLedgerDtID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_SubLedgerDtID, SubLedgerDtID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (SubLedgerDt)_helper.DataRowToObject(row, new SubLedgerDt());
        }
        public int Insert(SubLedgerDt record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(SubLedgerDt record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 SubLedgerDtID)
        {
            SubLedgerDt record;
            if (_ctx.Transaction == null)
                record = new SubLedgerDtDao().Get(SubLedgerDtID);
            else
                record = Get(SubLedgerDtID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region SubLedgerHd
    [Serializable]
    [Table(Name = "SubLedgerHd")]
    public class SubLedgerHd : DbDataModel
    {
        private Int32 _SubLedgerID;
        private String _SubLedgerCode;
        private String _SubLedgerName;
        private Int32 _SubLedgerTypeID;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "SubLedgerID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 SubLedgerID
        {
            get { return _SubLedgerID; }
            set { _SubLedgerID = value; }
        }
        [Column(Name = "SubLedgerCode", DataType = "String")]
        public String SubLedgerCode
        {
            get { return _SubLedgerCode; }
            set { _SubLedgerCode = value; }
        }
        [Column(Name = "SubLedgerName", DataType = "String")]
        public String SubLedgerName
        {
            get { return _SubLedgerName; }
            set { _SubLedgerName = value; }
        }
        [Column(Name = "SubLedgerTypeID", DataType = "Int32")]
        public Int32 SubLedgerTypeID
        {
            get { return _SubLedgerTypeID; }
            set { _SubLedgerTypeID = value; }
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

    public class SubLedgerHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(SubLedgerHd));
        private bool _isAuditLog = false;
        private const string p_SubLedgerID = "@p_SubLedgerID";
        public SubLedgerHdDao() { }
        public SubLedgerHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public SubLedgerHd Get(Int32 SubLedgerID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_SubLedgerID, SubLedgerID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (SubLedgerHd)_helper.DataRowToObject(row, new SubLedgerHd());
        }
        public int Insert(SubLedgerHd record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(SubLedgerHd record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 SubLedgerID)
        {
            SubLedgerHd record;
            if (_ctx.Transaction == null)
                record = new SubLedgerHdDao().Get(SubLedgerID);
            else
                record = Get(SubLedgerID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Supplier
    [Serializable]
    [Table(Name = "Supplier")]
    public class Supplier : DbDataModel
    {
        private Int32 _BusinessPartnerID;
        private Decimal _MaxPOAmount;
        private Boolean _IsPaymentHold;
        private Int16 _LeadTime;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "BusinessPartnerID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "MaxPOAmount", DataType = "Decimal")]
        public Decimal MaxPOAmount
        {
            get { return _MaxPOAmount; }
            set { _MaxPOAmount = value; }
        }
        [Column(Name = "IsPaymentHold", DataType = "Boolean", IsNullable = true)]
        public Boolean IsPaymentHold
        {
            get { return _IsPaymentHold; }
            set { _IsPaymentHold = value; }
        }
        [Column(Name = "LeadTime", DataType = "Int16", IsNullable = true)]
        public Int16 LeadTime
        {
            get { return _LeadTime; }
            set { _LeadTime = value; }
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

    public class SupplierDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Supplier));
        private bool _isAuditLog = false;
        private const string p_BusinessPartnerID = "@p_BusinessPartnerID";
        public SupplierDao() { }
        public SupplierDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Supplier Get(Int32 BusinessPartnerID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_BusinessPartnerID, BusinessPartnerID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Supplier)_helper.DataRowToObject(row, new Supplier());
        }
        public int Insert(Supplier record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Supplier record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 BusinessPartnerID)
        {
            Supplier record;
            if (_ctx.Transaction == null)
                record = new SupplierDao().Get(BusinessPartnerID);
            else
                record = Get(BusinessPartnerID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region SupplierCreditNote
    [Serializable]
    [Table(Name = "SupplierCreditNote")]
    public class SupplierCreditNote : DbDataModel
    {
        private Int32 _CreditNoteID;
        private String _CreditNoteNo;
        private DateTime _CreditNoteDate;
        private Int32 _BusinessPartnerID;
        private Int32 _PurchaseReturnID;
        private String _GCCreditNoteType;
        private Decimal _CNAmount;
        private Boolean _IsIncludeVAT;
        private Decimal _VATPercentage;
        private String _Remarks;
        private Int32? _PurchaseInvoiceID;
        private String _GCTransactionStatus;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "CreditNoteID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 CreditNoteID
        {
            get { return _CreditNoteID; }
            set { _CreditNoteID = value; }
        }
        [Column(Name = "CreditNoteNo", DataType = "String")]
        public String CreditNoteNo
        {
            get { return _CreditNoteNo; }
            set { _CreditNoteNo = value; }
        }
        [Column(Name = "CreditNoteDate", DataType = "DateTime")]
        public DateTime CreditNoteDate
        {
            get { return _CreditNoteDate; }
            set { _CreditNoteDate = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
        }
        [Column(Name = "PurchaseReturnID", DataType = "Int32")]
        public Int32 PurchaseReturnID
        {
            get { return _PurchaseReturnID; }
            set { _PurchaseReturnID = value; }
        }
        [Column(Name = "GCCreditNoteType", DataType = "String")]
        public String GCCreditNoteType
        {
            get { return _GCCreditNoteType; }
            set { _GCCreditNoteType = value; }
        }
        [Column(Name = "CNAmount", DataType = "Decimal")]
        public Decimal CNAmount
        {
            get { return _CNAmount; }
            set { _CNAmount = value; }
        }
        [Column(Name = "IsIncludeVAT", DataType = "Boolean")]
        public Boolean IsIncludeVAT
        {
            get { return _IsIncludeVAT; }
            set { _IsIncludeVAT = value; }
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
        [Column(Name = "PurchaseInvoiceID", DataType = "Int32", IsNullable = true)]
        public Int32? PurchaseInvoiceID
        {
            get { return _PurchaseInvoiceID; }
            set { _PurchaseInvoiceID = value; }
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

    public class SupplierCreditNoteDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(SupplierCreditNote));
        private bool _isAuditLog = false;
        private const string p_CreditNoteID = "@p_CreditNoteID";
        public SupplierCreditNoteDao() { }
        public SupplierCreditNoteDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public SupplierCreditNote Get(Int32 CreditNoteID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_CreditNoteID, CreditNoteID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (SupplierCreditNote)_helper.DataRowToObject(row, new SupplierCreditNote());
        }
        public int Insert(SupplierCreditNote record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(SupplierCreditNote record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 CreditNoteID)
        {
            SupplierCreditNote record;
            if (_ctx.Transaction == null)
                record = new SupplierCreditNoteDao().Get(CreditNoteID);
            else
                record = Get(CreditNoteID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region SupplierItem
    [Serializable]
    [Table(Name = "SupplierItem")]
    public partial class SupplierItem : DbDataModel
    {
        private Int32 _ID;
        private Int32 _BusinessPartnerID;
        private Int32 _ItemID;
        private String _SupplierItemCode;
        private String _SupplierItemName;
        private Int16 _LeadTime;
        private Decimal _Price;
        private Decimal _DiscountPercentage;
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
        [Column(Name = "LeadTime", DataType = "Int16", IsNullable = true)]
        public Int16 LeadTime
        {
            get { return _LeadTime; }
            set { _LeadTime = value; }
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

    public class SupplierItemDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(SupplierItem));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public SupplierItemDao() { }
        public SupplierItemDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public SupplierItem Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (SupplierItem)_helper.DataRowToObject(row, new SupplierItem());
        }
        public int Insert(SupplierItem record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(SupplierItem record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            SupplierItem record;
            if (_ctx.Transaction == null)
                record = new SupplierItemDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region SupplierPaymentDt
    [Serializable]
    [Table(Name = "SupplierPaymentDt")]
    public class SupplierPaymentDt : DbDataModel
    {
        private Int32 _SupplierPaymentID;
        private Int32 _PurchaseInvoiceID;
        private Decimal _PaymentAmount;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "SupplierPaymentID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 SupplierPaymentID
        {
            get { return _SupplierPaymentID; }
            set { _SupplierPaymentID = value; }
        }
        [Column(Name = "PurchaseInvoiceID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 PurchaseInvoiceID
        {
            get { return _PurchaseInvoiceID; }
            set { _PurchaseInvoiceID = value; }
        }
        [Column(Name = "PaymentAmount", DataType = "Decimal")]
        public Decimal PaymentAmount
        {
            get { return _PaymentAmount; }
            set { _PaymentAmount = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32? CreatedBy
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

    public class SupplierPaymentDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(SupplierPaymentDt));
        private bool _isAuditLog = false;
        private const string p_SupplierPaymentID = "@p_SupplierPaymentID";
        private const string p_PurchaseInvoiceID = "@p_PurchaseInvoiceID";
        public SupplierPaymentDtDao() { }
        public SupplierPaymentDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public SupplierPaymentDt Get(Int32 SupplierPaymentID, Int32 PurchaseInvoiceID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_SupplierPaymentID, SupplierPaymentID);
            _ctx.Add(p_PurchaseInvoiceID, PurchaseInvoiceID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (SupplierPaymentDt)_helper.DataRowToObject(row, new SupplierPaymentDt());
        }
        public int Insert(SupplierPaymentDt record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(SupplierPaymentDt record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 SupplierPaymentID, Int32 PurchaseInvoiceID)
        {
            SupplierPaymentDt record;
            if (_ctx.Transaction == null)
                record = new SupplierPaymentDtDao().Get(SupplierPaymentID, PurchaseInvoiceID);
            else
                record = Get(SupplierPaymentID, PurchaseInvoiceID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region SupplierPaymentHd
    [Serializable]
    [Table(Name = "SupplierPaymentHd")]
    public class SupplierPaymentHd : DbDataModel
    {
        private Int32 _SupplierPaymentID;
        private String _SupplierPaymentNo;
        private DateTime _PaymentDate;
        private Int32 _BusinessPartnerID;
        private String _ReferenceNo;
        private DateTime _ReferenceDate;
        private String _GCCurrencyCode;
        private Decimal _CurrencyRate;
        private String _GCSupplierPaymentMethod;
        private Int32? _BankID;
        private String _BankReferenceNo;
        private String _Remarks;
        private String _GCTransactionStatus;
        private String _GCVoidReason;
        private String _VoidReason;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "SupplierPaymentID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 SupplierPaymentID
        {
            get { return _SupplierPaymentID; }
            set { _SupplierPaymentID = value; }
        }
        [Column(Name = "SupplierPaymentNo", DataType = "String")]
        public String SupplierPaymentNo
        {
            get { return _SupplierPaymentNo; }
            set { _SupplierPaymentNo = value; }
        }
        [Column(Name = "PaymentDate", DataType = "DateTime")]
        public DateTime PaymentDate
        {
            get { return _PaymentDate; }
            set { _PaymentDate = value; }
        }
        [Column(Name = "BusinessPartnerID", DataType = "Int32")]
        public Int32 BusinessPartnerID
        {
            get { return _BusinessPartnerID; }
            set { _BusinessPartnerID = value; }
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
        [Column(Name = "GCSupplierPaymentMethod", DataType = "String")]
        public String GCSupplierPaymentMethod
        {
            get { return _GCSupplierPaymentMethod; }
            set { _GCSupplierPaymentMethod = value; }
        }
        [Column(Name = "BankID", DataType = "Int32", IsNullable = true)]
        public Int32? BankID
        {
            get { return _BankID; }
            set { _BankID = value; }
        }
        [Column(Name = "BankReferenceNo", DataType = "String", IsNullable = true)]
        public String BankReferenceNo
        {
            get { return _BankReferenceNo; }
            set { _BankReferenceNo = value; }
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
        [Column(Name = "GCVoidReason", DataType = "String", IsNullable = true)]
        public String GCVoidReason
        {
            get { return _GCVoidReason; }
            set { _GCVoidReason = value; }
        }
        [Column(Name = "VoidReason", DataType = "String", IsNullable = true)]
        public String VoidReason
        {
            get { return _VoidReason; }
            set { _VoidReason = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32? CreatedBy
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

    public class SupplierPaymentHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(SupplierPaymentHd));
        private bool _isAuditLog = false;
        private const string p_SupplierPaymentID = "@p_SupplierPaymentID";
        public SupplierPaymentHdDao() { }
        public SupplierPaymentHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public SupplierPaymentHd Get(Int32 SupplierPaymentID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_SupplierPaymentID, SupplierPaymentID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (SupplierPaymentHd)_helper.DataRowToObject(row, new SupplierPaymentHd());
        }
        public int Insert(SupplierPaymentHd record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(SupplierPaymentHd record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 SupplierPaymentID)
        {
            SupplierPaymentHd record;
            if (_ctx.Transaction == null)
                record = new SupplierPaymentHdDao().Get(SupplierPaymentID);
            else
                record = Get(SupplierPaymentID);
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
    #region TeacherSubject
    [Serializable]
    [Table(Name = "TeacherSubject")]
    public class TeacherSubject : DbDataModel
    {
        private Int32 _TeacherID;
        private Int32 _SubjectID;

        [Column(Name = "TeacherID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 TeacherID
        {
            get { return _TeacherID; }
            set { _TeacherID = value; }
        }
        [Column(Name = "SubjectID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
    }

    public class TeacherSubjectDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(TeacherSubject));
        private bool _isAuditLog = false;
        private const string p_SubjectID = "@p_SubjectID";
        private const string p_TeacherID = "@p_TeacherID";
        public TeacherSubjectDao() { }
        public TeacherSubjectDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public TeacherSubject Get(Int32 TeacherID, Int32 SubjectID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_SubjectID, SubjectID);
            _ctx.Add(p_TeacherID, TeacherID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (TeacherSubject)_helper.DataRowToObject(row, new TeacherSubject());
        }
        public int Insert(TeacherSubject record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(TeacherSubject record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 TeacherID, Int32 SubjectID)
        {
            TeacherSubject record;
            if (_ctx.Transaction == null)
                record = new TeacherSubjectDao().Get(TeacherID, SubjectID);
            else
                record = Get(TeacherID, SubjectID);
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
