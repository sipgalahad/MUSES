using System;
using System.Collections.Generic;
using System.Data;
using CodeX.Data.Core.Dal;
using System.Linq;

namespace CodeX.Data.Model
{
    public static partial class BusinessLayer
    {
        #region Common Views
        #region Address
        public static Address GetAddress(Int32 AddressID)
        {
            return new AddressDao().Get(AddressID);
        }
        public static int InsertAddress(Address record)
        {
            return new AddressDao().Insert(record);
        }
        public static int UpdateAddress(Address record)
        {
            return new AddressDao().Update(record);
        }
        public static int DeleteAddress(Int32 AddressID)
        {
            return new AddressDao().Delete(AddressID);
        }
        public static List<Address> GetAddressList(string filterExpression)
        {
            List<Address> result = new List<Address>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Address));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Address)helper.IDataReaderToObject(reader, new Address()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetAddressMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(Address));
                ctx.CommandText = helper.SelectMaxColumn("AddressID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region FilterParameter
        public static FilterParameter GetFilterParameter(Int32 FilterParameterID)
        {
            return new FilterParameterDao().Get(FilterParameterID);
        }
        public static int InsertFilterParameter(FilterParameter record)
        {
            return new FilterParameterDao().Insert(record);
        }
        public static int UpdateFilterParameter(FilterParameter record)
        {
            return new FilterParameterDao().Update(record);
        }
        public static int DeleteFilterParameter(Int32 FilterParameterID)
        {
            return new FilterParameterDao().Delete(FilterParameterID);
        }
        public static List<FilterParameter> GetFilterParameterList(string filterExpression)
        {
            List<FilterParameter> result = new List<FilterParameter>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(FilterParameter));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((FilterParameter)helper.IDataReaderToObject(reader, new FilterParameter()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetFilterParameterMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(FilterParameter));
                ctx.CommandText = helper.SelectMaxColumn("FilterParameterID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region LocationUser
        public static LocationUser GetLocationUser(Int32 ID)
        {
            return new LocationUserDao().Get(ID);
        }
        public static int InsertLocationUser(LocationUser record)
        {
            return new LocationUserDao().Insert(record);
        }
        public static int UpdateLocationUser(LocationUser record)
        {
            return new LocationUserDao().Update(record);
        }
        public static int DeleteLocationUser(Int32 ID)
        {
            return new LocationUserDao().Delete(ID);
        }
        public static List<LocationUser> GetLocationUserList(string filterExpression)
        {
            List<LocationUser> result = new List<LocationUser>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(LocationUser));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((LocationUser)helper.IDataReaderToObject(reader, new LocationUser()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<LocationUser> GetLocationUserList(string filterExpression, IDbContext ctx)
        {
            List<LocationUser> result = new List<LocationUser>();
            try
            {
                DbHelper helper = new DbHelper(typeof(LocationUser));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((LocationUser)helper.IDataReaderToObject(reader, new LocationUser()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<Int32> GetLocationUserLocationIDList(string filterExpression)
        {
            String columnName = "LocationID";
            List<Int32> result = new List<Int32>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(LocationUser));
                ctx.CommandText = helper.SelectColumn(columnName, filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add(Convert.ToInt32(reader[columnName]));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetLocationUserRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(LocationUser));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region LocationUserRole
        public static LocationUserRole GetLocationUserRole(Int32 ID)
        {
            return new LocationUserRoleDao().Get(ID);
        }
        public static int InsertLocationUserRole(LocationUserRole record)
        {
            return new LocationUserRoleDao().Insert(record);
        }
        public static int UpdateLocationUserRole(LocationUserRole record)
        {
            return new LocationUserRoleDao().Update(record);
        }
        public static int DeleteLocationUserRole(Int32 ID)
        {
            return new LocationUserRoleDao().Delete(ID);
        }
        public static List<LocationUserRole> GetLocationUserRoleList(string filterExpression, IDbContext ctx)
        {
            List<LocationUserRole> result = new List<LocationUserRole>();
            try
            {
                DbHelper helper = new DbHelper(typeof(LocationUserRole));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((LocationUserRole)helper.IDataReaderToObject(reader, new LocationUserRole()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<LocationUserRole> GetLocationUserRoleList(string filterExpression)
        {
            List<LocationUserRole> result = new List<LocationUserRole>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(LocationUserRole));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((LocationUserRole)helper.IDataReaderToObject(reader, new LocationUserRole()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<Int32> GetLocationUserRoleLocationIDList(string filterExpression)
        {
            String columnName = "LocationID";
            List<Int32> result = new List<Int32>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(LocationUserRole));
                ctx.CommandText = helper.SelectColumn(columnName, filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add(Convert.ToInt32(reader[columnName]));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetLocationUserRoleRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(LocationUserRole));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region LoginAttribute
        public static LoginAttribute GetLoginAttribute(Int32 LoginAttributeID)
        {
            return new LoginAttributeDao().Get(LoginAttributeID);
        }
        public static int InsertLoginAttribute(LoginAttribute record)
        {
            return new LoginAttributeDao().Insert(record);
        }
        public static int UpdateLoginAttribute(LoginAttribute record)
        {
            return new LoginAttributeDao().Update(record);
        }
        public static int DeleteLoginAttribute(Int32 LoginAttributeID)
        {
            return new LoginAttributeDao().Delete(LoginAttributeID);
        }
        public static List<LoginAttribute> GetLoginAttributeList(string filterExpression)
        {
            List<LoginAttribute> result = new List<LoginAttribute>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(LoginAttribute));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((LoginAttribute)helper.IDataReaderToObject(reader, new LoginAttribute()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetLoginAttributeRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(LoginAttribute));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<LoginAttribute> GetLoginAttributeList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<LoginAttribute> result = new List<LoginAttribute>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(LoginAttribute));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((LoginAttribute)helper.IDataReaderToObject(reader, new LoginAttribute()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetLoginAttributeRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(LoginAttribute));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "LoginAttributeID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetLoginAttributeMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(LoginAttribute));
                ctx.CommandText = helper.SelectMaxColumn("LoginAttributeID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region MenuMaster
        public static MenuMaster GetMenuMaster(Int32 MenuID)
        {
            return new MenuMasterDao().Get(MenuID);
        }
        public static int InsertMenuMaster(MenuMaster record)
        {
            return new MenuMasterDao().Insert(record);
        }
        public static int UpdateMenuMaster(MenuMaster record)
        {
            return new MenuMasterDao().Update(record);
        }
        public static int DeleteMenuMaster(Int32 MenuID)
        {
            return new MenuMasterDao().Delete(MenuID);
        }
        public static List<MenuMaster> GetMenuMasterList(string filterExpression)
        {
            List<MenuMaster> result = new List<MenuMaster>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MenuMaster));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MenuMaster)helper.IDataReaderToObject(reader, new MenuMaster()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetMenuMasterMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(MenuMaster));
                ctx.CommandText = helper.SelectMaxColumn("MenuID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region Module
        public static Module GetModule(String ModuleID)
        {
            return new ModuleDao().Get(ModuleID);
        }
        public static int InsertModule(Module record)
        {
            return new ModuleDao().Insert(record);
        }
        public static int UpdateModule(Module record)
        {
            return new ModuleDao().Update(record);
        }
        public static int DeleteModule(String ModuleID)
        {
            return new ModuleDao().Delete(ModuleID);
        }
        public static List<Module> GetModuleList(string filterExpression)
        {
            List<Module> result = new List<Module>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Module));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Module)helper.IDataReaderToObject(reader, new Module()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<Module> GetModuleList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<Module> result = new List<Module>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Module));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Module)helper.IDataReaderToObject(reader, new Module()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetModuleRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Module));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetModuleRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Module));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ModuleID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region PivotSettingDt
        public static PivotSettingDt GetPivotSettingDt(Int32 ID)
        {
            return new PivotSettingDtDao().Get(ID);
        }
        public static int InsertPivotSettingDt(PivotSettingDt record)
        {
            return new PivotSettingDtDao().Insert(record);
        }
        public static int UpdatePivotSettingDt(PivotSettingDt record)
        {
            return new PivotSettingDtDao().Update(record);
        }
        public static int DeletePivotSettingDt(Int32 ID)
        {
            return new PivotSettingDtDao().Delete(ID);
        }
        public static List<PivotSettingDt> GetPivotSettingDtList(string filterExpression)
        {
            List<PivotSettingDt> result = new List<PivotSettingDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PivotSettingDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PivotSettingDt)helper.IDataReaderToObject(reader, new PivotSettingDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region PivotSettingHd
        public static PivotSettingHd GetPivotSettingHd(Int32 PivotSettingID)
        {
            return new PivotSettingHdDao().Get(PivotSettingID);
        }
        public static int InsertPivotSettingHd(PivotSettingHd record)
        {
            return new PivotSettingHdDao().Insert(record);
        }
        public static int UpdatePivotSettingHd(PivotSettingHd record)
        {
            return new PivotSettingHdDao().Update(record);
        }
        public static int DeletePivotSettingHd(Int32 PivotSettingID)
        {
            return new PivotSettingHdDao().Delete(PivotSettingID);
        }
        public static List<PivotSettingHd> GetPivotSettingHdList(string filterExpression)
        {
            List<PivotSettingHd> result = new List<PivotSettingHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PivotSettingHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PivotSettingHd)helper.IDataReaderToObject(reader, new PivotSettingHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ReportMaster
        public static ReportMaster GetReportMaster(Int32 ReportID)
        {
            return new ReportMasterDao().Get(ReportID);
        }
        public static int InsertReportMaster(ReportMaster record)
        {
            return new ReportMasterDao().Insert(record);
        }
        public static int UpdateReportMaster(ReportMaster record)
        {
            return new ReportMasterDao().Update(record);
        }
        public static int DeleteReportMaster(Int32 ReportID)
        {
            return new ReportMasterDao().Delete(ReportID);
        }
        public static List<ReportMaster> GetReportMasterList(string filterExpression)
        {
            List<ReportMaster> result = new List<ReportMaster>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ReportMaster));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ReportMaster)helper.IDataReaderToObject(reader, new ReportMaster()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetReportMasterMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ReportMaster));
                ctx.CommandText = helper.SelectMaxColumn("ReportID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ReportParameter
        public static ReportParameter GetReportParameter(Int32 ReportID, Int32 FilterParameterID)
        {
            return new ReportParameterDao().Get(ReportID, FilterParameterID);
        }
        public static int InsertReportParameter(ReportParameter record)
        {
            return new ReportParameterDao().Insert(record);
        }
        public static int UpdateReportParameter(ReportParameter record)
        {
            return new ReportParameterDao().Update(record);
        }
        public static int DeleteReportParameter(Int32 ReportID, Int32 FilterParameterID)
        {
            return new ReportParameterDao().Delete(ReportID, FilterParameterID);
        }
        public static List<ReportParameter> GetReportParameterList(string filterExpression)
        {
            List<ReportParameter> result = new List<ReportParameter>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ReportParameter));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ReportParameter)helper.IDataReaderToObject(reader, new ReportParameter()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SettingParameter
        public static SettingParameter GetSettingParameter(String ParameterCode)
        {
            return new SettingParameterDao().Get(ParameterCode);
        }
        public static int InsertSettingParameter(SettingParameter record)
        {
            return new SettingParameterDao().Insert(record);
        }
        public static int UpdateSettingParameter(SettingParameter record)
        {
            return new SettingParameterDao().Update(record);
        }
        public static int DeleteSettingParameter(String ParameterCode)
        {
            return new SettingParameterDao().Delete(ParameterCode);
        }
        public static List<SettingParameter> GetSettingParameterList(string filterExpression)
        {
            List<SettingParameter> result = new List<SettingParameter>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SettingParameter));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SettingParameter)helper.IDataReaderToObject(reader, new SettingParameter()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<SettingParameter> GetSettingParameterList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<SettingParameter> result = new List<SettingParameter>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SettingParameter));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SettingParameter)helper.IDataReaderToObject(reader, new SettingParameter()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSettingParameterRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SettingParameter));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSettingParameterRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SettingParameter));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ParameterCode", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Site
        public static Site GetSite(String SiteID)
        {
            return new SiteDao().Get(SiteID);
        }
        public static int InsertSite(Site record)
        {
            return new SiteDao().Insert(record);
        }
        public static int UpdateSite(Site record)
        {
            return new SiteDao().Update(record);
        }
        public static int DeleteSite(String SiteID)
        {
            return new SiteDao().Delete(SiteID);
        }
        public static List<Site> GetSiteList(string filterExpression, IDbContext ctx)
        {
            List<Site> result = new List<Site>();
            try
            {
                DbHelper helper = new DbHelper(typeof(Site));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Site)helper.IDataReaderToObject(reader, new Site()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<Site> GetSiteList(string filterExpression)
        {
            List<Site> result = new List<Site>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Site));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Site)helper.IDataReaderToObject(reader, new Site()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<Site> GetSiteList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<Site> result = new List<Site>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Site));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Site)helper.IDataReaderToObject(reader, new Site()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSiteRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Site));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSiteRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Site));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "SiteID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region StandardCode
        public static StandardCode GetStandardCode(String StandardCodeID)
        {
            return new StandardCodeDao().Get(StandardCodeID);
        }
        public static int InsertStandardCode(StandardCode record)
        {
            return new StandardCodeDao().Insert(record);
        }
        public static int UpdateStandardCode(StandardCode record)
        {
            return new StandardCodeDao().Update(record);
        }
        public static int DeleteStandardCode(String StandardCodeID)
        {
            return new StandardCodeDao().Delete(StandardCodeID);
        }
        public static List<StandardCode> GetStandardCodeList(string filterExpression, IDbContext ctx)
        {
            List<StandardCode> result = new List<StandardCode>();
            try
            {
                DbHelper helper = new DbHelper(typeof(StandardCode));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StandardCode)helper.IDataReaderToObject(reader, new StandardCode()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<StandardCode> GetStandardCodeList(string filterExpression)
        {
            List<StandardCode> result = new List<StandardCode>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StandardCode));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StandardCode)helper.IDataReaderToObject(reader, new StandardCode()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<StandardCode> GetStandardCodeList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<StandardCode> result = new List<StandardCode>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StandardCode));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StandardCode)helper.IDataReaderToObject(reader, new StandardCode()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetStandardCodeRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StandardCode));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetStandardCodeRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StandardCode));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "StandardCodeID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region TagField
        public static TagField GetTagField(String GCBusinessObjectType)
        {
            return new TagFieldDao().Get(GCBusinessObjectType);
        }
        public static int InsertTagField(TagField record)
        {
            return new TagFieldDao().Insert(record);
        }
        public static int UpdateTagField(TagField record)
        {
            return new TagFieldDao().Update(record);
        }
        public static int DeleteTagField(String GCBusinessObjectType)
        {
            return new TagFieldDao().Delete(GCBusinessObjectType);
        }
        public static List<TagField> GetTagFieldList(string filterExpression)
        {
            List<TagField> result = new List<TagField>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TagField));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TagField)helper.IDataReaderToObject(reader, new TagField()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region TransactionType
        public static TransactionType GetTransactionType(String TransactionCode)
        {
            return new TransactionTypeDao().Get(TransactionCode);
        }
        public static int InsertTransactionType(TransactionType record)
        {
            return new TransactionTypeDao().Insert(record);
        }
        public static int UpdateTransactionType(TransactionType record)
        {
            return new TransactionTypeDao().Update(record);
        }
        public static int DeleteTransactionType(String TransactionCode)
        {
            return new TransactionTypeDao().Delete(TransactionCode);
        }
        public static List<TransactionType> GetTransactionTypeList(string filterExpression)
        {
            List<TransactionType> result = new List<TransactionType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransactionType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransactionType)helper.IDataReaderToObject(reader, new TransactionType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TransactionType> GetTransactionTypeList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<TransactionType> result = new List<TransactionType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransactionType));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransactionType)helper.IDataReaderToObject(reader, new TransactionType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTransactionTypeRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransactionType));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTransactionTypeRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransactionType));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "TransactionCode", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region User
        public static User GetUser(Int32 UserID)
        {
            return new UserDao().Get(UserID);
        }
        public static int InsertUser(User record)
        {
            return new UserDao().Insert(record);
        }
        public static int UpdateUser(User record)
        {
            return new UserDao().Update(record);
        }
        public static int DeleteUser(Int32 UserID)
        {
            return new UserDao().Delete(UserID);
        }
        public static List<User> GetUserList(string filterExpression)
        {
            List<User> result = new List<User>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(User));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((User)helper.IDataReaderToObject(reader, new User()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<User> GetUserList(string filterExpression, IDbContext ctx)
        {
            List<User> result = new List<User>();
            try
            {
                DbHelper helper = new DbHelper(typeof(User));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((User)helper.IDataReaderToObject(reader, new User()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetUserMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(User));
                ctx.CommandText = helper.SelectMaxColumn("UserID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region UserAttribute
        public static UserAttribute GetUserAttribute(Int32 UserID)
        {
            return new UserAttributeDao().Get(UserID);
        }
        public static int InsertUserAttribute(UserAttribute record)
        {
            return new UserAttributeDao().Insert(record);
        }
        public static int UpdateUserAttribute(UserAttribute record)
        {
            return new UserAttributeDao().Update(record);
        }
        public static int DeleteUserAttribute(Int32 UserID)
        {
            return new UserAttributeDao().Delete(UserID);
        }
        public static List<UserAttribute> GetUserAttributeList(string filterExpression)
        {
            List<UserAttribute> result = new List<UserAttribute>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(UserAttribute));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((UserAttribute)helper.IDataReaderToObject(reader, new UserAttribute()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region UserInRole
        public static UserInRole GetUserInRole(Int32 UserID, String SiteID, Int32 RoleID)
        {
            return new UserInRoleDao().Get(UserID, SiteID, RoleID);
        }
        public static int InsertUserInRole(UserInRole record)
        {
            return new UserInRoleDao().Insert(record);
        }
        public static int UpdateUserInRole(UserInRole record)
        {
            return new UserInRoleDao().Update(record);
        }
        public static int DeleteUserInRole(Int32 UserID, String SiteID, Int32 RoleID)
        {
            return new UserInRoleDao().Delete(UserID, SiteID, RoleID);
        }
        public static List<UserInRole> GetUserInRoleList(string filterExpression)
        {
            List<UserInRole> result = new List<UserInRole>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(UserInRole));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((UserInRole)helper.IDataReaderToObject(reader, new UserInRole()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region UserLoginAttribute
        public static UserLoginAttribute GetUserLoginAttribute(Int32 UserID, String SiteID, Int32 LoginAttributeID)
        {
            return new UserLoginAttributeDao().Get(UserID, SiteID, LoginAttributeID);
        }
        public static int InsertUserLoginAttribute(UserLoginAttribute record)
        {
            return new UserLoginAttributeDao().Insert(record);
        }
        public static int UpdateUserLoginAttribute(UserLoginAttribute record)
        {
            return new UserLoginAttributeDao().Update(record);
        }
        public static int DeleteUserLoginAttribute(Int32 UserID, String SiteID, Int32 LoginAttributeID)
        {
            return new UserLoginAttributeDao().Delete(UserID, SiteID, LoginAttributeID);
        }
        public static List<UserLoginAttribute> GetUserLoginAttributeList(string filterExpression)
        {
            List<UserLoginAttribute> result = new List<UserLoginAttribute>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(UserLoginAttribute));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((UserLoginAttribute)helper.IDataReaderToObject(reader, new UserLoginAttribute()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region UserMenu
        public static UserMenu GetUserMenu(Int32 ID)
        {
            return new UserMenuDao().Get(ID);
        }
        public static int InsertUserMenu(UserMenu record)
        {
            return new UserMenuDao().Insert(record);
        }
        public static int UpdateUserMenu(UserMenu record)
        {
            return new UserMenuDao().Update(record);
        }
        public static int DeleteUserMenu(Int32 ID)
        {
            return new UserMenuDao().Delete(ID);
        }
        public static List<UserMenu> GetUserMenuList(string filterExpression)
        {
            List<UserMenu> result = new List<UserMenu>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(UserMenu));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((UserMenu)helper.IDataReaderToObject(reader, new UserMenu()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<UserMenu> GetUserMenuList(string filterExpression, IDbContext ctx)
        {
            List<UserMenu> result = new List<UserMenu>();
            try
            {
                DbHelper helper = new DbHelper(typeof(UserMenu));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((UserMenu)helper.IDataReaderToObject(reader, new UserMenu()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region UserRole
        public static UserRole GetUserRole(Int32 RoleID)
        {
            return new UserRoleDao().Get(RoleID);
        }
        public static int InsertUserRole(UserRole record)
        {
            return new UserRoleDao().Insert(record);
        }
        public static int UpdateUserRole(UserRole record)
        {
            return new UserRoleDao().Update(record);
        }
        public static int DeleteUserRole(Int32 RoleID)
        {
            return new UserRoleDao().Delete(RoleID);
        }
        public static List<UserRole> GetUserRoleList(string filterExpression)
        {
            List<UserRole> result = new List<UserRole>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(UserRole));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((UserRole)helper.IDataReaderToObject(reader, new UserRole()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<UserRole> GetUserRoleList(string filterExpression, IDbContext ctx)
        {
            List<UserRole> result = new List<UserRole>();
            try
            {
                DbHelper helper = new DbHelper(typeof(UserRole));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((UserRole)helper.IDataReaderToObject(reader, new UserRole()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<UserRole> GetUserRoleList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<UserRole> result = new List<UserRole>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(UserRole));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((UserRole)helper.IDataReaderToObject(reader, new UserRole()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetUserRoleRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(UserRole));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetUserRoleRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(UserRole));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "RoleID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetUserRoleMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(UserRole));
                ctx.CommandText = helper.SelectMaxColumn("RoleID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region UserRoleLoginAttribute
        public static UserRoleLoginAttribute GetUserRoleLoginAttribute(Int32 RoleID, String SiteID, Int32 LoginAttributeID)
        {
            return new UserRoleLoginAttributeDao().Get(RoleID, SiteID, LoginAttributeID);
        }
        public static int InsertUserRoleLoginAttribute(UserRoleLoginAttribute record)
        {
            return new UserRoleLoginAttributeDao().Insert(record);
        }
        public static int UpdateUserRoleLoginAttribute(UserRoleLoginAttribute record)
        {
            return new UserRoleLoginAttributeDao().Update(record);
        }
        public static int DeleteUserRoleLoginAttribute(Int32 RoleID, String SiteID, Int32 LoginAttributeID)
        {
            return new UserRoleLoginAttributeDao().Delete(RoleID, SiteID, LoginAttributeID);
        }
        public static List<UserRoleLoginAttribute> GetUserRoleLoginAttributeList(string filterExpression)
        {
            List<UserRoleLoginAttribute> result = new List<UserRoleLoginAttribute>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(UserRoleLoginAttribute));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((UserRoleLoginAttribute)helper.IDataReaderToObject(reader, new UserRoleLoginAttribute()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region UserRoleMenu
        public static UserRoleMenu GetUserRoleMenu(Int32 ID)
        {
            return new UserRoleMenuDao().Get(ID);
        }
        public static int InsertUserRoleMenu(UserRoleMenu record)
        {
            return new UserRoleMenuDao().Insert(record);
        }
        public static int UpdateUserRoleMenu(UserRoleMenu record)
        {
            return new UserRoleMenuDao().Update(record);
        }
        public static int DeleteUserRoleMenu(Int32 ID)
        {
            return new UserRoleMenuDao().Delete(ID);
        }
        public static List<UserRoleMenu> GetUserRoleMenuList(string filterExpression, IDbContext ctx)
        {
            List<UserRoleMenu> result = new List<UserRoleMenu>();
            try
            {
                DbHelper helper = new DbHelper(typeof(UserRoleMenu));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((UserRoleMenu)helper.IDataReaderToObject(reader, new UserRoleMenu()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<UserRoleMenu> GetUserRoleMenuList(string filterExpression)
        {
            List<UserRoleMenu> result = new List<UserRoleMenu>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(UserRoleMenu));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((UserRoleMenu)helper.IDataReaderToObject(reader, new UserRoleMenu()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region UserTagField
        public static UserTagField GetUserTagField(Int32 UserID)
        {
            return new UserTagFieldDao().Get(UserID);
        }
        public static int InsertUserTagField(UserTagField record)
        {
            return new UserTagFieldDao().Insert(record);
        }
        public static int UpdateUserTagField(UserTagField record)
        {
            return new UserTagFieldDao().Update(record);
        }
        public static int DeleteUserTagField(Int32 UserID)
        {
            return new UserTagFieldDao().Delete(UserID);
        }
        public static List<UserTagField> GetUserTagFieldList(string filterExpression)
        {
            List<UserTagField> result = new List<UserTagField>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(UserTagField));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((UserTagField)helper.IDataReaderToObject(reader, new UserTagField()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ZipCodes
        public static ZipCodes GetZipCodes(Int32 ID)
        {
            return new ZipCodesDao().Get(ID);
        }
        public static int InsertZipCodes(ZipCodes record)
        {
            return new ZipCodesDao().Insert(record);
        }
        public static int UpdateZipCodes(ZipCodes record)
        {
            return new ZipCodesDao().Update(record);
        }
        public static int DeleteZipCodes(Int32 ID)
        {
            return new ZipCodesDao().Delete(ID);
        }
        public static List<ZipCodes> GetZipCodesList(string filterExpression)
        {
            List<ZipCodes> result = new List<ZipCodes>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ZipCodes));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ZipCodes)helper.IDataReaderToObject(reader, new ZipCodes()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ZipCodes> GetZipCodesList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<ZipCodes> result = new List<ZipCodes>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ZipCodes));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ZipCodes)helper.IDataReaderToObject(reader, new ZipCodes()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetZipCodesRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ZipCodes));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetZipCodesRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ZipCodes));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetZipCodesMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ZipCodes));
                ctx.CommandText = helper.SelectMaxColumn("ID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #endregion


        #region Tools
        #region MigrationConfigurationDt
        public static MigrationConfigurationDt GetMigrationConfigurationDt(Int32 ID)
        {
            return new MigrationConfigurationDtDao().Get(ID);
        }
        public static int InsertMigrationConfigurationDt(MigrationConfigurationDt record)
        {
            return new MigrationConfigurationDtDao().Insert(record);
        }
        public static int UpdateMigrationConfigurationDt(MigrationConfigurationDt record)
        {
            return new MigrationConfigurationDtDao().Update(record);
        }
        public static int DeleteMigrationConfigurationDt(Int32 ID)
        {
            return new MigrationConfigurationDtDao().Delete(ID);
        }
        public static List<MigrationConfigurationDt> GetMigrationConfigurationDtList(string filterExpression)
        {
            List<MigrationConfigurationDt> result = new List<MigrationConfigurationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MigrationConfigurationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MigrationConfigurationDt)helper.IDataReaderToObject(reader, new MigrationConfigurationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<MigrationConfigurationDt> GetMigrationConfigurationDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<MigrationConfigurationDt> result = new List<MigrationConfigurationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MigrationConfigurationDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MigrationConfigurationDt)helper.IDataReaderToObject(reader, new MigrationConfigurationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetMigrationConfigurationDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MigrationConfigurationDt));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetMigrationConfigurationDtRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MigrationConfigurationDt));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetMigrationConfigurationDtMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(MigrationConfigurationDt));
                ctx.CommandText = helper.SelectMaxColumn("ID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region MigrationConfigurationHd
        public static MigrationConfigurationHd GetMigrationConfigurationHd(Int32 ID)
        {
            return new MigrationConfigurationHdDao().Get(ID);
        }
        public static int InsertMigrationConfigurationHd(MigrationConfigurationHd record)
        {
            return new MigrationConfigurationHdDao().Insert(record);
        }
        public static int UpdateMigrationConfigurationHd(MigrationConfigurationHd record)
        {
            return new MigrationConfigurationHdDao().Update(record);
        }
        public static int DeleteMigrationConfigurationHd(Int32 ID)
        {
            return new MigrationConfigurationHdDao().Delete(ID);
        }
        public static List<MigrationConfigurationHd> GetMigrationConfigurationHdList(string filterExpression)
        {
            List<MigrationConfigurationHd> result = new List<MigrationConfigurationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MigrationConfigurationHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MigrationConfigurationHd)helper.IDataReaderToObject(reader, new MigrationConfigurationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<MigrationConfigurationHd> GetMigrationConfigurationHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<MigrationConfigurationHd> result = new List<MigrationConfigurationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MigrationConfigurationHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MigrationConfigurationHd)helper.IDataReaderToObject(reader, new MigrationConfigurationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetMigrationConfigurationHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MigrationConfigurationHd));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetMigrationConfigurationHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MigrationConfigurationHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetMigrationConfigurationHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(MigrationConfigurationHd));
                ctx.CommandText = helper.SelectMaxColumn("ID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region MigrationConfigurationTableLink
        public static MigrationConfigurationTableLink GetMigrationConfigurationTableLink(Int32 HeaderID, String TableName, String ColumnName)
        {
            return new MigrationConfigurationTableLinkDao().Get(HeaderID, TableName, ColumnName);
        }
        public static int InsertMigrationConfigurationTableLink(MigrationConfigurationTableLink record)
        {
            return new MigrationConfigurationTableLinkDao().Insert(record);
        }
        public static int UpdateMigrationConfigurationTableLink(MigrationConfigurationTableLink record)
        {
            return new MigrationConfigurationTableLinkDao().Update(record);
        }
        public static int DeleteMigrationConfigurationTableLink(Int32 HeaderID, String TableName, String ColumnName)
        {
            return new MigrationConfigurationTableLinkDao().Delete(HeaderID, TableName, ColumnName);
        }
        public static List<MigrationConfigurationTableLink> GetMigrationConfigurationTableLinkList(string filterExpression)
        {
            List<MigrationConfigurationTableLink> result = new List<MigrationConfigurationTableLink>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MigrationConfigurationTableLink));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MigrationConfigurationTableLink)helper.IDataReaderToObject(reader, new MigrationConfigurationTableLink()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region PersonNameConfiguration
        public static PersonNameConfiguration GetPersonNameConfiguration(Int32 ID)
        {
            return new PersonNameConfigurationDao().Get(ID);
        }
        public static int InsertPersonNameConfiguration(PersonNameConfiguration record)
        {
            return new PersonNameConfigurationDao().Insert(record);
        }
        public static int UpdatePersonNameConfiguration(PersonNameConfiguration record)
        {
            return new PersonNameConfigurationDao().Update(record);
        }
        public static int DeletePersonNameConfiguration(Int32 ID)
        {
            return new PersonNameConfigurationDao().Delete(ID);
        }
        public static List<PersonNameConfiguration> GetPersonNameConfigurationList(string filterExpression)
        {
            List<PersonNameConfiguration> result = new List<PersonNameConfiguration>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PersonNameConfiguration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PersonNameConfiguration)helper.IDataReaderToObject(reader, new PersonNameConfiguration()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region RestoreDataConfiguration
        public static RestoreDataConfiguration GetRestoreDataConfiguration()
        {
            return new RestoreDataConfigurationDao().Get();
        }
        public static int InsertRestoreDataConfiguration(RestoreDataConfiguration record)
        {
            return new RestoreDataConfigurationDao().Insert(record);
        }
        public static int UpdateRestoreDataConfiguration(RestoreDataConfiguration record)
        {
            return new RestoreDataConfigurationDao().Update(record);
        }
        public static int DeleteRestoreDataConfiguration()
        {
            return new RestoreDataConfigurationDao().Delete();
        }
        public static List<RestoreDataConfiguration> GetRestoreDataConfigurationList(string filterExpression)
        {
            List<RestoreDataConfiguration> result = new List<RestoreDataConfiguration>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RestoreDataConfiguration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RestoreDataConfiguration)helper.IDataReaderToObject(reader, new RestoreDataConfiguration()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<RestoreDataConfiguration> GetRestoreDataConfigurationList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<RestoreDataConfiguration> result = new List<RestoreDataConfiguration>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RestoreDataConfiguration));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RestoreDataConfiguration)helper.IDataReaderToObject(reader, new RestoreDataConfiguration()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRestoreDataConfigurationRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RestoreDataConfiguration));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRestoreDataConfigurationMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(RestoreDataConfiguration));
                ctx.CommandText = helper.SelectMaxColumn("ID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetRestoreDataConfigurationRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RestoreDataConfiguration));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SysColumns
        public static List<SysColumns> GetSysColumnsList(string filterExpression)
        {
            List<SysColumns> result = new List<SysColumns>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SysColumns));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SysColumns)helper.IDataReaderToObject(reader, new SysColumns()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<String> GetSysColumnsPKList(string tableName)
        {
            List<String> result = new List<String>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SysColumns));
                ctx.CommandText = string.Format("SELECT column_name FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1 AND table_name = '{0}'", tableName);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add(reader[0].ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        #endregion
        #region SysObjects
        public static List<SysObjects> GetSysObjectsList(string filterExpression)
        {
            List<SysObjects> result = new List<SysObjects>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SysObjects));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SysObjects)helper.IDataReaderToObject(reader, new SysObjects()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        #endregion
        #endregion
    }
}
