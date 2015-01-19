using System;
using CodeX.Data.Core.Dal;
using System.Text;
using System.ComponentModel;
using System.IO;

namespace CodeX.Data.Model
{
    #region Common Views
    #region vAddress
    [Serializable]
    [Table(Name = "vAddress")]
    public class vAddress
    {
        private String _AddressID;
        private String _StreetName;
        private String _District;
        private String _City;
        private String _County;
        private String _GCState;
        private String _State;
        private Int32 _ZipCodeID;
        private String _ZipCode;
        private String _PhoneNo1;
        private String _PhoneNo2;
        private String _FaxNo1;
        private String _FaxNo2;
        private Boolean _IsMailingAddress;

        [Column(Name = "AddressID", DataType = "String")]
        public String AddressID
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
        [Column(Name = "PhoneNo1", DataType = "String")]
        public String PhoneNo1
        {
            get { return _PhoneNo1; }
            set { _PhoneNo1 = value; }
        }
        [Column(Name = "PhoneNo2", DataType = "String")]
        public String PhoneNo2
        {
            get { return _PhoneNo2; }
            set { _PhoneNo2 = value; }
        }
        [Column(Name = "FaxNo1", DataType = "String")]
        public String FaxNo1
        {
            get { return _FaxNo1; }
            set { _FaxNo1 = value; }
        }
        [Column(Name = "FaxNo2", DataType = "String")]
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
    #endregion
    #region vDBSyncInfoDt
    [Serializable]
    [Table(Name = "vDBSyncInfoDt")]
    public class vDBSyncInfoDt
    {
        private Int32 _DBSyncInfoID;
        private String _DBSyncInfoCode;
        private String _DBSyncInfoName;
        private String _ModuleID;
        private Int32 _RowCount;
        private String _SiteID;
        private DateTime _LastSyncDate;
        private Boolean _IsDeleted;

        [Column(Name = "DBSyncInfoID", DataType = "Int32")]
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
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "LastSyncDate", DataType = "DateTime")]
        public DateTime LastSyncDate
        {
            get { return _LastSyncDate; }
            set { _LastSyncDate = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vFilterParameter
    [Serializable]
    [Table(Name = "vFilterParameter")]
    public class vFilterParameter
    {
        private Int32 _FilterParameterID;
        private String _FilterParameterCode;
        private String _FilterParameterName;
        private String _ControlName;
        private String _FilterParameterCaption;
        private String _GCFilterParameterType;
        private String _FilterParameterType;
        private String _MethodName;
        private String _FilterExpression;
        private String _ValueFieldName;
        private String _TextFieldName;
        private String _FieldName;
        private Boolean _IsDeleted;

        [Column(Name = "FilterParameterID", DataType = "Int32")]
        public Int32 FilterParameterID
        {
            get { return _FilterParameterID; }
            set { _FilterParameterID = value; }
        }
        [Column(Name = "FilterParameterCode", DataType = "String")]
        public String FilterParameterCode
        {
            get { return _FilterParameterCode; }
            set { _FilterParameterCode = value; }
        }
        [Column(Name = "FilterParameterName", DataType = "String")]
        public String FilterParameterName
        {
            get { return _FilterParameterName; }
            set { _FilterParameterName = value; }
        }
        [Column(Name = "ControlName", DataType = "String")]
        public String ControlName
        {
            get { return _ControlName; }
            set { _ControlName = value; }
        }
        [Column(Name = "FilterParameterCaption", DataType = "String")]
        public String FilterParameterCaption
        {
            get { return _FilterParameterCaption; }
            set { _FilterParameterCaption = value; }
        }
        [Column(Name = "GCFilterParameterType", DataType = "String")]
        public String GCFilterParameterType
        {
            get { return _GCFilterParameterType; }
            set { _GCFilterParameterType = value; }
        }
        [Column(Name = "FilterParameterType", DataType = "String")]
        public String FilterParameterType
        {
            get { return _FilterParameterType; }
            set { _FilterParameterType = value; }
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
        [Column(Name = "ValueFieldName", DataType = "String")]
        public String ValueFieldName
        {
            get { return _ValueFieldName; }
            set { _ValueFieldName = value; }
        }
        [Column(Name = "TextFieldName", DataType = "String")]
        public String TextFieldName
        {
            get { return _TextFieldName; }
            set { _TextFieldName = value; }
        }
        [Column(Name = "FieldName", DataType = "String")]
        public String FieldName
        {
            get { return _FieldName; }
            set { _FieldName = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vMenu
    [Serializable]
    [Table(Name = "vMenu")]
    public class vMenu
    {
        private Int32 _MenuID;
        private String _MenuCode;
        private String _ModuleID;
        private String _MenuCaption;
        private String _MenuUrl;
        private Int16 _MenuLevel;
        private Int16 _MenuIndex;
        private String _MenuTooltip;
        private Int32 _ParentID;
        private String _CRUDMode;
        private String _ImageUrl;
        private Boolean _IsHeader;
        private Int32 _Level;

        [Column(Name = "MenuID", DataType = "Int32")]
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
        [Column(Name = "ModuleID", DataType = "String")]
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
        [Column(Name = "MenuUrl", DataType = "String")]
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
        [Column(Name = "MenuTooltip", DataType = "String")]
        public String MenuTooltip
        {
            get { return _MenuTooltip; }
            set { _MenuTooltip = value; }
        }
        [Column(Name = "ParentID", DataType = "Int32")]
        public Int32 ParentID
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
        [Column(Name = "ImageUrl", DataType = "String")]
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
        [Column(Name = "Level", DataType = "Int32")]
        public Int32 Level
        {
            get { return _Level; }
            set { _Level = value; }
        }
    }
    #endregion
    #region vMenuReport
    [Serializable]
    [Table(Name = "vMenuReport")]
    public class vMenuReport
    {
        private Int32 _MenuID;
        private Int32 _ReportID;
        private String _ReportCode;
        private String _ReportName;
        private Int16 _DisplayOrder;
        private Boolean _IsSelected;

        [Column(Name = "MenuID", DataType = "Int32")]
        public Int32 MenuID
        {
            get { return _MenuID; }
            set { _MenuID = value; }
        }
        [Column(Name = "ReportID", DataType = "Int32")]
        public Int32 ReportID
        {
            get { return _ReportID; }
            set { _ReportID = value; }
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
    #endregion
    #region vReportMaster
    [Serializable]
    [Table(Name = "vReportMaster")]
    public class vReportMaster
    {
        private Int32 _ReportID;
        private String _ModuleID;
        private String _ReportCode;
        private String _ReportName;
        private String _GCReportType;
        private String _ReportType;
        private Boolean _IsHeader;
        private Int32 _ParentID;
        private Int32 _Level;
        private String _Path;

        [Column(Name = "ReportID", DataType = "Int32")]
        public Int32 ReportID
        {
            get { return _ReportID; }
            set { _ReportID = value; }
        }
        [Column(Name = "ModuleID", DataType = "String")]
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
        [Column(Name = "ReportType", DataType = "String")]
        public String ReportType
        {
            get { return _ReportType; }
            set { _ReportType = value; }
        }
        [Column(Name = "IsHeader", DataType = "Boolean")]
        public Boolean IsHeader
        {
            get { return _IsHeader; }
            set { _IsHeader = value; }
        }
        [Column(Name = "ParentID", DataType = "Int32")]
        public Int32 ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        [Column(Name = "Level", DataType = "Int32")]
        public Int32 Level
        {
            get { return _Level; }
            set { _Level = value; }
        }
        [Column(Name = "Path", DataType = "String")]
        public String Path
        {
            get { return _Path; }
            set { _Path = value; }
        }
    }
    #endregion
    #region vReportParameter
    [Serializable]
    [Table(Name = "vReportParameter")]
    public class vReportParameter
    {
        private Int32 _ReportID;
        private String _ReportCode;
        private Int32 _FilterParameterID;
        private String _FilterParameterCode;
        private String _FilterParameterName;
        private String _ControlName;
        private String _FilterParameterCaption;
        private String _GCFilterParameterType;
        private String _MethodName;
        private String _FilterExpression;
        private String _ValueFieldName;
        private String _TextFieldName;
        private String _ClientInstanceName;
        private String _FieldName;
        private Boolean _IsAllowSelectAll;
        private Int16 _DisplayOrder;
        private String _SearchDialogType;
        private String _SearchDialogMethodName;
        private String _SearchDialogFilterExpression;
        private String _SearchDialogIDField;
        private String _SearchDialogCodeField;
        private String _SearchDialogNameField;
        private String _ListText;
        private String _ListValue;
        private Int16 _YearMinusNYear;
        private Int16 _YearPlusNYear;
        private String _TxtCssClass;
        private String _DefaultValue;
        private Boolean _IsDeleted;

        [Column(Name = "ReportID", DataType = "Int32")]
        public Int32 ReportID
        {
            get { return _ReportID; }
            set { _ReportID = value; }
        }
        [Column(Name = "ReportCode", DataType = "String")]
        public String ReportCode
        {
            get { return _ReportCode; }
            set { _ReportCode = value; }
        }
        [Column(Name = "FilterParameterID", DataType = "Int32")]
        public Int32 FilterParameterID
        {
            get { return _FilterParameterID; }
            set { _FilterParameterID = value; }
        }
        [Column(Name = "FilterParameterCode", DataType = "String")]
        public String FilterParameterCode
        {
            get { return _FilterParameterCode; }
            set { _FilterParameterCode = value; }
        }
        [Column(Name = "FilterParameterName", DataType = "String")]
        public String FilterParameterName
        {
            get { return _FilterParameterName; }
            set { _FilterParameterName = value; }
        }
        [Column(Name = "ControlName", DataType = "String")]
        public String ControlName
        {
            get { return _ControlName; }
            set { _ControlName = value; }
        }
        [Column(Name = "FilterParameterCaption", DataType = "String")]
        public String FilterParameterCaption
        {
            get { return _FilterParameterCaption; }
            set { _FilterParameterCaption = value; }
        }
        [Column(Name = "GCFilterParameterType", DataType = "String")]
        public String GCFilterParameterType
        {
            get { return _GCFilterParameterType; }
            set { _GCFilterParameterType = value; }
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
        [Column(Name = "ValueFieldName", DataType = "String")]
        public String ValueFieldName
        {
            get { return _ValueFieldName; }
            set { _ValueFieldName = value; }
        }
        [Column(Name = "TextFieldName", DataType = "String")]
        public String TextFieldName
        {
            get { return _TextFieldName; }
            set { _TextFieldName = value; }
        }
        [Column(Name = "ClientInstanceName", DataType = "String")]
        public String ClientInstanceName
        {
            get { return _ClientInstanceName; }
            set { _ClientInstanceName = value; }
        }
        [Column(Name = "FieldName", DataType = "String")]
        public String FieldName
        {
            get { return _FieldName; }
            set { _FieldName = value; }
        }
        [Column(Name = "IsAllowSelectAll", DataType = "Boolean")]
        public Boolean IsAllowSelectAll
        {
            get { return _IsAllowSelectAll; }
            set { _IsAllowSelectAll = value; }
        }
        [Column(Name = "DisplayOrder", DataType = "Int16")]
        public Int16 DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        [Column(Name = "SearchDialogType", DataType = "String")]
        public String SearchDialogType
        {
            get { return _SearchDialogType; }
            set { _SearchDialogType = value; }
        }
        [Column(Name = "SearchDialogMethodName", DataType = "String")]
        public String SearchDialogMethodName
        {
            get { return _SearchDialogMethodName; }
            set { _SearchDialogMethodName = value; }
        }
        [Column(Name = "SearchDialogFilterExpression", DataType = "String")]
        public String SearchDialogFilterExpression
        {
            get { return _SearchDialogFilterExpression; }
            set { _SearchDialogFilterExpression = value; }
        }
        [Column(Name = "SearchDialogIDField", DataType = "String")]
        public String SearchDialogIDField
        {
            get { return _SearchDialogIDField; }
            set { _SearchDialogIDField = value; }
        }
        [Column(Name = "SearchDialogCodeField", DataType = "String")]
        public String SearchDialogCodeField
        {
            get { return _SearchDialogCodeField; }
            set { _SearchDialogCodeField = value; }
        }
        [Column(Name = "SearchDialogNameField", DataType = "String")]
        public String SearchDialogNameField
        {
            get { return _SearchDialogNameField; }
            set { _SearchDialogNameField = value; }
        }
        [Column(Name = "ListText", DataType = "String")]
        public String ListText
        {
            get { return _ListText; }
            set { _ListText = value; }
        }
        [Column(Name = "ListValue", DataType = "String")]
        public String ListValue
        {
            get { return _ListValue; }
            set { _ListValue = value; }
        }
        [Column(Name = "YearMinusNYear", DataType = "Int16")]
        public Int16 YearMinusNYear
        {
            get { return _YearMinusNYear; }
            set { _YearMinusNYear = value; }
        }
        [Column(Name = "YearPlusNYear", DataType = "Int16")]
        public Int16 YearPlusNYear
        {
            get { return _YearPlusNYear; }
            set { _YearPlusNYear = value; }
        }
        [Column(Name = "TxtCssClass", DataType = "String")]
        public String TxtCssClass
        {
            get { return _TxtCssClass; }
            set { _TxtCssClass = value; }
        }
        [Column(Name = "DefaultValue", DataType = "String")]
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
    }
    #endregion
    #region vSite
    [Serializable]
    [Table(Name = "vSite")]
    public partial class vSite
    {
        private String _SiteID;
        private String _SiteName;
        private String _GCOperatingGroup;
        private String _ParentID;
        private Boolean _IsHeader;
        private String _ShortName;
        private String _Initial;
        private String _LicenseNo;
        private String _AddressID;
        private String _PhoneNo1;
        private String _PhoneNo2;
        private String _FaxNo1;
        private String _FaxNo2;
        private String _StreetName;
        private String _County;
        private String _District;
        private String _City;
        private String _GCState;
        private String _State;
        private Int32 _ZipCodeID;
        private String _ZipCode;
        private Int32 _Level;
        private String _Path;

        [Column(Name = "SiteID", DataType = "String")]
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
        [Column(Name = "GCOperatingGroup", DataType = "String")]
        public String GCOperatingGroup
        {
            get { return _GCOperatingGroup; }
            set { _GCOperatingGroup = value; }
        }
        [Column(Name = "ParentID", DataType = "String")]
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
        [Column(Name = "ShortName", DataType = "String")]
        public String ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }
        [Column(Name = "Initial", DataType = "String")]
        public String Initial
        {
            get { return _Initial; }
            set { _Initial = value; }
        }
        [Column(Name = "LicenseNo", DataType = "String")]
        public String LicenseNo
        {
            get { return _LicenseNo; }
            set { _LicenseNo = value; }
        }
        [Column(Name = "AddressID", DataType = "String")]
        public String AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
        }
        [Column(Name = "PhoneNo1", DataType = "String")]
        public String PhoneNo1
        {
            get { return _PhoneNo1; }
            set { _PhoneNo1 = value; }
        }
        [Column(Name = "PhoneNo2", DataType = "String")]
        public String PhoneNo2
        {
            get { return _PhoneNo2; }
            set { _PhoneNo2 = value; }
        }
        [Column(Name = "FaxNo1", DataType = "String")]
        public String FaxNo1
        {
            get { return _FaxNo1; }
            set { _FaxNo1 = value; }
        }
        [Column(Name = "FaxNo2", DataType = "String")]
        public String FaxNo2
        {
            get { return _FaxNo2; }
            set { _FaxNo2 = value; }
        }
        [Column(Name = "StreetName", DataType = "String")]
        public String StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }
        [Column(Name = "County", DataType = "String")]
        public String County
        {
            get { return _County; }
            set { _County = value; }
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
        [Column(Name = "Level", DataType = "Int32")]
        public Int32 Level
        {
            get { return _Level; }
            set { _Level = value; }
        }
        [Column(Name = "Path", DataType = "String")]
        public String Path
        {
            get { return _Path; }
            set { _Path = value; }
        }
    }
    #endregion
    #region vSiteModule
    [Serializable]
    [Table(Name = "vSiteModule")]
    public class vSiteModule
    {
        private Int32 _SiteModuleID;
        private String _SiteID;
        private String _SiteName;
        private String _ModuleID;
        private String _ModuleName;
        private Boolean _IsDeleted;

        [Column(Name = "SiteModuleID", DataType = "Int32")]
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
        [Column(Name = "SiteName", DataType = "String")]
        public String SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }
        [Column(Name = "ModuleID", DataType = "String")]
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
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vUser
    [Serializable]
    [Table(Name = "vUser")]
    public class vUser
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
        private String _FullName;
        private String _UserImage;
        private Int32 _EmployeeID;
        private String _EmployeeName;
        private Boolean _IsResetPassword;
        private Boolean _IsDeleted;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32 _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "UserID", DataType = "Int32")]
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
        [Column(Name = "MobileAlias", DataType = "String")]
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
        [Column(Name = "MobilePIN", DataType = "String")]
        public String MobilePIN
        {
            get { return _MobilePIN; }
            set { _MobilePIN = value; }
        }
        [Column(Name = "Email", DataType = "String")]
        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        [Column(Name = "LoweredEmail", DataType = "String")]
        public String LoweredEmail
        {
            get { return _LoweredEmail; }
            set { _LoweredEmail = value; }
        }
        [Column(Name = "PasswordQuestion", DataType = "String")]
        public String PasswordQuestion
        {
            get { return _PasswordQuestion; }
            set { _PasswordQuestion = value; }
        }
        [Column(Name = "PasswordAnswer", DataType = "String")]
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
        [Column(Name = "Comment", DataType = "String")]
        public String Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }
        [Column(Name = "FullName", DataType = "String")]
        public String FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
        [Column(Name = "UserImage", DataType = "String")]
        public String UserImage
        {
            get { return _UserImage; }
            set { _UserImage = value; }
        }
        [Column(Name = "EmployeeID", DataType = "Int32")]
        public Int32 EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }
        [Column(Name = "EmployeeName", DataType = "String")]
        public String EmployeeName
        {
            get { return _EmployeeName; }
            set { _EmployeeName = value; }
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
        [Column(Name = "LastUpdatedBy", DataType = "Int32")]
        public Int32 LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime")]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }
    #endregion
    #region vUserInRole
    [Serializable]
    [Table(Name = "vUserInRole")]
    public class vUserInRole
    {
        private Int32 _UserID;
        private String _UserName;
        private String _SiteID;
        private String _SiteName;
        private Int32 _RoleID;
        private String _RoleName;
        private String _DefaultPageUrl;
        private Boolean _IsMainRole;

        [Column(Name = "UserID", DataType = "Int32")]
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
        [Column(Name = "SiteID", DataType = "String")]
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
        [Column(Name = "RoleID", DataType = "Int32")]
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
        [Column(Name = "DefaultPageUrl", DataType = "String")]
        public String DefaultPageUrl
        {
            get { return _DefaultPageUrl; }
            set { _DefaultPageUrl = value; }
        }
        [Column(Name = "IsMainRole", DataType = "Boolean")]
        public Boolean IsMainRole
        {
            get { return _IsMainRole; }
            set { _IsMainRole = value; }
        }
    }
    #endregion
    #region vUserMenu
    [Serializable]
    [Table(Name = "vUserMenu")]
    public class vUserMenu
    {
        private Int32 _ID;
        private Int32 _MenuID;
        private String _ModuleID;
        private String _MenuCode;
        private String _MenuCaption;
        private Int16 _MenuIndex;
        private Int32? _ParentID;
        private String _ParentCode;
        private String _MenuUrl;
        private String _ImageUrl;
        private Int16 _MenuLevel;
        private Boolean _IsVisible;
        private Boolean _IsShowInPullDownMenu;
        private String _SiteID;
        private Int32 _UserID;
        private String _CRUDMode;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
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
        [Column(Name = "ModuleID", DataType = "String")]
        public String ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }
        [Column(Name = "MenuCode", DataType = "String")]
        public String MenuCode
        {
            get { return _MenuCode; }
            set { _MenuCode = value; }
        }
        [Column(Name = "MenuCaption", DataType = "String")]
        public String MenuCaption
        {
            get { return _MenuCaption; }
            set { _MenuCaption = value; }
        }
        [Column(Name = "MenuIndex", DataType = "Int16")]
        public Int16 MenuIndex
        {
            get { return _MenuIndex; }
            set { _MenuIndex = value; }
        }
        [Column(Name = "ParentID", DataType = "Int32")]
        public Int32? ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        [Column(Name = "ParentCode", DataType = "String")]
        public String ParentCode
        {
            get { return _ParentCode; }
            set { _ParentCode = value; }
        }
        [Column(Name = "MenuUrl", DataType = "String")]
        public String MenuUrl
        {
            get { return _MenuUrl; }
            set { _MenuUrl = value; }
        }
        [Column(Name = "ImageUrl", DataType = "String")]
        public String ImageUrl
        {
            get { return _ImageUrl; }
            set { _ImageUrl = value; }
        }
        [Column(Name = "MenuLevel", DataType = "Int16")]
        public Int16 MenuLevel
        {
            get { return _MenuLevel; }
            set { _MenuLevel = value; }
        }
        [Column(Name = "IsVisible", DataType = "Boolean")]
        public Boolean IsVisible
        {
            get { return _IsVisible; }
            set { _IsVisible = value; }
        }
        [Column(Name = "IsShowInPullDownMenu", DataType = "Boolean")]
        public Boolean IsShowInPullDownMenu
        {
            get { return _IsShowInPullDownMenu; }
            set { _IsShowInPullDownMenu = value; }
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
    }
    #endregion
    #region vUserRoleLoginAttribute
    [Serializable]
    [Table(Name = "vUserRoleLoginAttribute")]
    public class vUserRoleLoginAttribute
    {
        private Int32 _RoleID;
        private String _SiteID;
        private Int32 _LoginAttributeID;
        private String _LoginAttributeCode;
        private String _LoginAttributeName;
        private String _SessionName;
        private String _MethodName;
        private String _FilterExpression;
        private String _ValueFieldName;
        private String _TextFieldName;
        private String _DefaultValue;
        private Boolean _IsDeleted;

        [Column(Name = "RoleID", DataType = "Int32")]
        public Int32 RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        [Column(Name = "LoginAttributeID", DataType = "Int32")]
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
        [Column(Name = "FilterExpression", DataType = "String")]
        public String FilterExpression
        {
            get { return _FilterExpression; }
            set { _FilterExpression = value; }
        }
        [Column(Name = "ValueFieldName", DataType = "String")]
        public String ValueFieldName
        {
            get { return _ValueFieldName; }
            set { _ValueFieldName = value; }
        }
        [Column(Name = "TextFieldName", DataType = "String")]
        public String TextFieldName
        {
            get { return _TextFieldName; }
            set { _TextFieldName = value; }
        }
        [Column(Name = "DefaultValue", DataType = "String")]
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
    }
    #endregion
    #region vZipCodes
    [Serializable]
    [Table(Name = "vZipCodes")]
    public class vZipCodes
    {
        private Int32 _ID;
        private String _ZIpCode;
        private String _StreetName;
        private String _District;
        private String _County;
        private String _City;
        private String _GCProvince;
        private String _Province;
        private Decimal _Longitude;
        private Decimal _Latitude;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "ZIpCode", DataType = "String")]
        public String ZIpCode
        {
            get { return _ZIpCode; }
            set { _ZIpCode = value; }
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
        [Column(Name = "Province", DataType = "String")]
        public String Province
        {
            get { return _Province; }
            set { _Province = value; }
        }
        [Column(Name = "Longitude", DataType = "Decimal")]
        public Decimal Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; }
        }
        [Column(Name = "Latitude", DataType = "Decimal")]
        public Decimal Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #endregion
}
