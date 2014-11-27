using System;
using CodeX.Data.Core.Dal;

/***************************************************************************
 * $Archive: $
 * $Workfile: $
 * $Author: $
 * $Date: $
 * $Modtime: $  
 * $Revision: $
 ***************************************************************************/
namespace CodeX.Data.Model
{
    #region GetItemMasterPurchase
    [Serializable]
    [Table(Name = "GetItemMasterPurchase")]
    public class GetItemMasterPurchase
    {
        private Int32 _ItemID;
        private String _ItemCode;
        private String _ItemName1;
        private String _GCItemType;
        private String _ItemUnit;
        private String _PurchaseUnit;
        private Int32 _ItemGroupID;
        private String _ItemGroupCode;
        private String _ItemGroupName1;
        private Decimal _Discount;
        private Decimal _Price;
        private Int32 _BusinessPartnerID;
        private String _BusinessPartnerCode;
        private String _BusinessPartnerName;
        private String _SupplierItemCode;
        private String _SupplierItemName;
        private Decimal _ConversionFactor;

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
        [Column(Name = "ItemUnit", DataType = "String")]
        public String ItemUnit
        {
            get { return _ItemUnit; }
            set { _ItemUnit = value; }
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
        [Column(Name = "Discount", DataType = "Decimal")]
        public Decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }
        [Column(Name = "Price", DataType = "Decimal")]
        public Decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
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
        [Column(Name = "ConversionFactor", DataType = "Decimal")]
        public Decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
    }
    #endregion
    #region GetLocationUserList
    [Serializable]
    [Table(Name = "GetLocationUserList")]
    public class GetLocationUserList
    {
        private Int32 _LocationID;
        private String _LocationCode;
        private String _LocationName;

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
    }
    #endregion
    #region GetLoginAttributeUserList
    [Serializable]
    [Table(Name = "GetLoginAttributeUserList")]
    public class GetLoginAttributeUserList
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
        [Column(Name = "LoginAttributeCaption", DataType = "String")]
        public String LoginAttributeCaption
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
    }
    #endregion
    #region GetReportUserList
    [Serializable]
    [Table(Name = "GetReportUserList")]
    public partial class GetReportUserList
    {
        private Int32 _ReportID;
        private String _ReportCode;
        private String _ReportTitle1;
        private String _ReportTitle2;
        private Int32? _ParentID;
        private Boolean _IsHeader;
        private String _MenuCode;
        private Int16 _DisplayOrder;
        private Boolean _IsSelected;

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
        [Column(Name = "ReportTitle1", DataType = "String")]
        public String ReportTitle1
        {
            get { return _ReportTitle1; }
            set { _ReportTitle1 = value; }
        }
        [Column(Name = "ReportTitle2", DataType = "String")]
        public String ReportTitle2
        {
            get { return _ReportTitle2; }
            set { _ReportTitle2 = value; }
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
        [Column(Name = "MenuCode", DataType = "String")]
        public String MenuCode
        {
            get { return _MenuCode; }
            set { _MenuCode = value; }
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
    #region GetUserMenuAccess
    [Serializable]
    [Table(Name = "GetUserMenuAccess")]
    public class GetUserMenuAccess
    {
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
        private String _CRUDMode;

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
        [Column(Name = "CRUDMode", DataType = "String")]
        public String CRUDMode
        {
            get { return _CRUDMode; }
            set { _CRUDMode = value; }
        }
    }
    #endregion
    #region GetUserMenuList
    [Serializable]
    [Table(Name = "GetUserMenuList")]
    public class GetUserMenuList
    {
        private Int32 _MenuID;
        private String _MenuCode;
        private String _ModuleID;
        private String _MenuCaption;
        private String _SiteID;
        private String _CRUDModeUserSite;
        private Int32 _UserID;
        private Int32 _Level;
        private String _CRUDModeUser;

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

        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }

        /// <summary>
        /// CRUDModeUserRole untuk menyimpan CRUDMode dari UserRole ybs. Jika Mode false, visible akan false.
        /// Untuk Visibility
        /// </summary>
        [Column(Name = "CRUDModeUserSite", DataType = "String")]
        public String CRUDModeUserSite
        {
            get { return _CRUDModeUserSite; }
            set { _CRUDModeUserSite = value; }
        }

        [Column(Name = "UserID", DataType = "Int32")]
        public Int32 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        [Column(Name = "Level", DataType = "Int32")]
        public Int32 Level
        {
            get { return _Level; }
            set { _Level = value; }
        }

        /// <summary>
        /// CRUDModeUser untuk menyimpan CRUDMode dari UserRole ybs
        /// </summary>
        [Column(Name = "CRUDModeUser", DataType = "String")]
        public String CRUDModeUser
        {
            get
            {
                if (_CRUDModeUser.Length > 0)
                    return _CRUDModeUser;
                return "------";
            }
            set { _CRUDModeUser = value; }
        }

        public Boolean CREATE
        {
            get { return CRUDModeUser.Contains("C"); }
            set
            {
                string[] arr = CRUDModeUser.Split('-');
                if (value)
                    arr[0] = "C";
                else
                    arr[0] = "";
                CRUDModeUser = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6] + "-" + arr[7] + "-" + arr[8];
            }
        }
        public Boolean READ
        {
            get { return CRUDModeUser.Contains("R"); }
            set
            {
                string[] arr = CRUDModeUser.Split('-');
                if (value)
                    arr[1] = "R";
                else
                    arr[1] = "";
                CRUDModeUser = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6] + "-" + arr[7] + "-" + arr[8];
            }
        }
        public Boolean UPDATE
        {
            get { return CRUDModeUser.Contains("U"); }
            set
            {
                string[] arr = CRUDModeUser.Split('-');
                if (value)
                    arr[2] = "U";
                else
                    arr[2] = "";
                CRUDModeUser = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6] + "-" + arr[7] + "-" + arr[8];
            }
        }
        public Boolean DELETE
        {
            get { return CRUDModeUser.Contains("D"); }
            set
            {
                string[] arr = CRUDModeUser.Split('-');
                if (value)
                    arr[3] = "D";
                else
                    arr[3] = "";
                CRUDModeUser = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6] + "-" + arr[7] + "-" + arr[8];
            }
        }
        public Boolean EXPORT
        {
            get { return CRUDModeUser.Contains("E"); }
            set
            {
                string[] arr = CRUDModeUser.Split('-');
                if (value)
                    arr[4] = "E";
                else
                    arr[4] = "";
                CRUDModeUser = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6] + "-" + arr[7] + "-" + arr[8];
            }
        }
        public Boolean PROPOSE
        {
            get { return CRUDModeUser.Contains("P"); }
            set
            {
                string[] arr = CRUDModeUser.Split('-');
                if (value)
                    arr[5] = "P";
                else
                    arr[5] = "";
                CRUDModeUser = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6] + "-" + arr[7] + "-" + arr[8];
            }
        }
        public Boolean APPROVE
        {
            get { return CRUDModeUser.Contains("A"); }
            set
            {
                string[] arr = CRUDModeUser.Split('-');
                if (value)
                    arr[6] = "A";
                else
                    arr[6] = "";
                CRUDModeUser = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6] + "-" + arr[7] + "-" + arr[8];
            }
        }
        public Boolean REOPEN
        {
            get { return CRUDModeUser.Contains("O"); }
            set
            {
                string[] arr = CRUDModeUser.Split('-');
                if (value)
                    arr[7] = "O";
                else
                    arr[7] = "";
                CRUDModeUser = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6] + "-" + arr[7] + "-" + arr[8];
            }
        }
        public Boolean SYNC
        {
            get { return CRUDModeUser.Contains("S"); }
            set
            {
                string[] arr = CRUDModeUser.Split('-');
                if (value)
                    arr[8] = "S";
                else
                    arr[8] = "";
                CRUDModeUser = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6] + "-" + arr[7] + "-" + arr[8];
            }
        }
        public Boolean ENABLED
        {
            get { return CRUDModeUser.Contains("R"); }
        }

        public Boolean CVISIBLE
        {
            get { return _CRUDModeUserSite.Contains("C"); }
        }
        public Boolean RVISIBLE
        {
            get { return _CRUDModeUserSite.Contains("R"); }
        }
        public Boolean UVISIBLE
        {
            get { return _CRUDModeUserSite.Contains("U"); }
        }
        public Boolean DVISIBLE
        {
            get { return _CRUDModeUserSite.Contains("D"); }
        }
        public Boolean EVISIBLE
        {
            get { return _CRUDModeUserSite.Contains("E"); }
        }
        public Boolean PVISIBLE
        {
            get { return _CRUDModeUserSite.Contains("P"); }
        }
        public Boolean AVISIBLE
        {
            get { return _CRUDModeUserSite.Contains("A"); }
        }
        public Boolean OVISIBLE
        {
            get { return _CRUDModeUserSite.Contains("O"); }
        }
        public Boolean SVISIBLE
        {
            get { return _CRUDModeUserSite.Contains("S"); }
        }
    }
    #endregion
    #region GetUserRoleMenuList
    [Serializable]
    [Table(Name = "GetUserRoleMenuList")]
    public class GetUserRoleMenuList
    {
        private Int32 _ID;
        private Int32 _MenuID;
        private String _MenuCode;
        private String _ModuleID;
        private String _MenuCaption;
        private String _SiteID;
        private String _CRUDModeMenu;
        private Int32 _RoleID;
        private Int32 _Level;
        private String _CRUDModeUserRole;

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

        [Column(Name = "SiteID", DataType = "String")]
        public String SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }

        [Column(Name = "CRUDModeMenu", DataType = "String")]
        public String CRUDModeMenu
        {
            get { return _CRUDModeMenu; }
            set { _CRUDModeMenu = value; }
        }

        [Column(Name = "RoleID", DataType = "Int32")]
        public Int32 RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }

        [Column(Name = "Level", DataType = "Int32")]
        public Int32 Level
        {
            get { return _Level; }
            set { _Level = value; }
        }

        [Column(Name = "CRUDModeUserRole", DataType = "String")]
        public String CRUDModeUserRole
        {
            get
            {
                if (_CRUDModeUserRole.Length > 0)
                    return _CRUDModeUserRole;
                return "-------";
            }
            set { _CRUDModeUserRole = value; }
        }

        public Boolean CREATE
        {
            get { return CRUDModeUserRole.Contains("C"); }
            set
            {
                string[] arr = CRUDModeUserRole.Split('-');
                if (value)
                    arr[0] = "C";
                else
                    arr[0] = "";
                CRUDModeUserRole = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6] + "-" + arr[7];
            }
        }
        public Boolean READ
        {
            get { return CRUDModeUserRole.Contains("R"); }
            set
            {
                string[] arr = CRUDModeUserRole.Split('-');
                if (value)
                    arr[1] = "R";
                else
                    arr[1] = "";
                CRUDModeUserRole = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6] + "-" + arr[7] + "-" + arr[8];
            }
        }
        public Boolean UPDATE
        {
            get { return CRUDModeUserRole.Contains("U"); }
            set
            {
                string[] arr = CRUDModeUserRole.Split('-');
                if (value)
                    arr[2] = "U";
                else
                    arr[2] = "";
                CRUDModeUserRole = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6] + "-" + arr[7] + "-" + arr[8];
            }
        }
        public Boolean DELETE
        {
            get { return CRUDModeUserRole.Contains("D"); }
            set
            {
                string[] arr = CRUDModeUserRole.Split('-');
                if (value)
                    arr[3] = "D";
                else
                    arr[3] = "";
                CRUDModeUserRole = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6] + "-" + arr[7] + "-" + arr[8];
            }
        }
        public Boolean EXPORT
        {
            get { return CRUDModeUserRole.Contains("E"); }
            set
            {
                string[] arr = CRUDModeUserRole.Split('-');
                if (value)
                    arr[4] = "E";
                else
                    arr[4] = "";
                CRUDModeUserRole = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6] + "-" + arr[7] + "-" + arr[8];
            }
        }
        public Boolean PROPOSE
        {
            get { return CRUDModeUserRole.Contains("P"); }
            set
            {
                string[] arr = CRUDModeUserRole.Split('-');
                if (value)
                    arr[5] = "P";
                else
                    arr[5] = "";
                CRUDModeUserRole = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6] + "-" + arr[7] + "-" + arr[8];
            }
        }
        public Boolean APPROVE
        {
            get { return CRUDModeUserRole.Contains("A"); }
            set
            {
                string[] arr = CRUDModeUserRole.Split('-');
                if (value)
                    arr[6] = "A";
                else
                    arr[6] = "";
                CRUDModeUserRole = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6] + "-" + arr[7] + "-" + arr[8];
            }
        }
        public Boolean REOPEN
        {
            get { return CRUDModeUserRole.Contains("O"); }
            set
            {
                string[] arr = CRUDModeUserRole.Split('-');
                if (value)
                    arr[7] = "O";
                else
                    arr[7] = "";
                CRUDModeUserRole = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6] + "-" + arr[7] + "-" + arr[8];
            }
        }
        public Boolean SYNC
        {
            get { return CRUDModeUserRole.Contains("S"); }
            set
            {
                string[] arr = CRUDModeUserRole.Split('-');
                if (value)
                    arr[7] = "O";
                else
                    arr[7] = "";
                CRUDModeUserRole = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6] + "-" + arr[7] + "-" + arr[8];
            }
        }
        public Boolean ENABLED
        {
            get { return _CRUDModeUserRole.Contains("R"); }
        }

        public Boolean CVISIBLE
        {
            get { return _CRUDModeMenu.Contains("C"); }
        }
        public Boolean RVISIBLE
        {
            get { return _CRUDModeMenu.Contains("R"); }
        }
        public Boolean UVISIBLE
        {
            get { return _CRUDModeMenu.Contains("U"); }
        }
        public Boolean DVISIBLE
        {
            get { return _CRUDModeMenu.Contains("D"); }
        }
        public Boolean EVISIBLE
        {
            get { return _CRUDModeMenu.Contains("E"); }
        }
        public Boolean PVISIBLE
        {
            get { return _CRUDModeMenu.Contains("P"); }
        }
        public Boolean AVISIBLE
        {
            get { return _CRUDModeMenu.Contains("A"); }
        }
        public Boolean OVISIBLE
        {
            get { return _CRUDModeMenu.Contains("O"); }
        }
        public Boolean SVISIBLE
        {
            get { return _CRUDModeMenu.Contains("S"); }
        }
    }
    #endregion

}