using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CodeX.Data.Core.Dal;

namespace CodeX.Data.Model
{
    public static partial class BusinessLayer
    {
        #region GenerateTransactionNo
        public static string GenerateTransactionNo(string transactionCode, DateTime transactionDate)
        {
            return GenerateTransactionNo(transactionCode, transactionDate, "", null);
        }
        public static string GenerateTransactionNo(string transactionCode, DateTime transactionDate, String transactionInitial = "", IDbContext ctx = null)
        {
            return GenerateTransactionNo(transactionCode, transactionDate, ctx, transactionInitial);
        }
        public static string GenerateTransactionNo(string transactionCode, DateTime transactionDate, IDbContext ctx = null, String transactionInitial = "")
        {
            bool IsCtxNull = false;
            if (ctx == null)
            {
                IsCtxNull = true;
                ctx = DbFactory.Configure();
            }
            ctx.CommandText = "GenerateTransactionNo";
            ctx.CommandType = CommandType.StoredProcedure;
            ctx.Command.Parameters.Add(new SqlParameter("@TransactionCode", transactionCode));
            ctx.Command.Parameters.Add(new SqlParameter("@TransactionDate", transactionDate));
            ctx.Command.Parameters.Add(new SqlParameter("@TransactionInitial", transactionInitial));
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@Result";
            param.SqlDbType = SqlDbType.VarChar;
            param.Size = 20;
            param.Direction = ParameterDirection.Output;

            ctx.Command.Parameters.Add(param);

            try
            {
                DaoBase.ExecuteNonQuery(ctx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (IsCtxNull)
                    ctx.Close();
            }

            return (string)param.Value;
        }
        #endregion
        #region GetItemMasterPurchase
        public static List<GetItemMasterPurchase> GetItemMasterPurchaseList(string siteID, int itemID, int businessPartnerID)
        {
            List<GetItemMasterPurchase> result = new List<GetItemMasterPurchase>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetItemMasterPurchase));
                ctx.CommandText = "GetItemMasterPurchase";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("p_SiteID", siteID);
                ctx.Add("p_ItemID", itemID);
                ctx.Add("p_BusinessPartnerID", businessPartnerID);
                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetItemMasterPurchase)helper.IDataReaderToObject(reader, new GetItemMasterPurchase()));
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
        public static List<GetItemMasterPurchase> GetItemMasterPurchaseList(string siteID, int itemID, int businessPartnerID, IDbContext ctx)
        {
            List<GetItemMasterPurchase> result = new List<GetItemMasterPurchase>();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetItemMasterPurchase));
                ctx.CommandText = "GetItemMasterPurchase";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("p_SiteID", siteID);
                ctx.Add("p_ItemID", itemID);
                ctx.Add("p_BusinessPartnerID", businessPartnerID);
                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetItemMasterPurchase)helper.IDataReaderToObject(reader, new GetItemMasterPurchase()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region GetLocationUserList
        public static List<GetLocationUserList> GetLocationUserList(string siteID, int userID, string transactionCode, string filterExpression)
        {
            List<GetLocationUserList> result = new List<GetLocationUserList>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetLocationUserList));
                ctx.CommandText = "GetLocationUserList";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("p_SiteID", siteID);
                ctx.Add("p_UserID", userID);
                ctx.Add("p_TransactionCode", transactionCode);
                ctx.Add("p_AdditionalFilterExpression", filterExpression);

                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetLocationUserList)helper.IDataReaderToObject(reader, new GetLocationUserList()));
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
        #region GetLoginAttributeUserList
        public static List<GetLoginAttributeUserList> GetLoginAttributeUserList(string siteID, int userID, string filterExpression)
        {
            List<GetLoginAttributeUserList> result = new List<GetLoginAttributeUserList>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetLoginAttributeUserList));
                ctx.CommandText = "GetLoginAttributeUserList";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("p_SiteID", siteID);
                ctx.Add("p_UserID", userID);
                ctx.Add("p_AdditionalFilterExpression", filterExpression);

                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetLoginAttributeUserList)helper.IDataReaderToObject(reader, new GetLoginAttributeUserList()));
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
        #region GetReportUserList
        public static List<GetReportUserList> GetReportUserList(string siteID, int userID, string menuCode, string filterExpression)
        {
            List<GetReportUserList> result = new List<GetReportUserList>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetReportUserList));
                ctx.CommandText = "GetReportUserList";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("p_SiteID", siteID);
                ctx.Add("p_UserID", userID);
                ctx.Add("p_MenuCode", menuCode);
                ctx.Add("p_AdditionalFilterExpression", filterExpression);

                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetReportUserList)helper.IDataReaderToObject(reader, new GetReportUserList()));
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
        #region GetUserMenuAccess
        public static List<GetUserMenuAccess> GetUserMenuAccess(String moduleID, String SiteID, int userID, string additionalFilterExpression)
        {
            List<GetUserMenuAccess> result = new List<GetUserMenuAccess>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetUserMenuAccess));
                ctx.CommandText = "GetUserMenuAccess";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("p_ModuleID", moduleID);
                ctx.Add("p_SiteID", SiteID);
                ctx.Add("p_UserID", userID);
                ctx.Add("p_AdditionalFilterExpression", additionalFilterExpression);
                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetUserMenuAccess)helper.IDataReaderToObject(reader, new GetUserMenuAccess()));
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
        #region GetUserMenuList
        public static List<GetUserMenuList> GetUserMenuList(String moduleID, String SiteID, Int32 userID, String loginSiteID, Int32 loginUserID)
        {
            List<GetUserMenuList> result = new List<GetUserMenuList>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetUserMenuList));
                ctx.CommandText = "GetUserMenuList";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("p_ModuleID", moduleID);
                ctx.Add("p_SiteID", SiteID);
                ctx.Add("p_UserID", userID);
                ctx.Add("p_LoginSiteID", loginSiteID);
                ctx.Add("p_LoginUserID", loginUserID);
                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetUserMenuList)helper.IDataReaderToObject(reader, new GetUserMenuList()));
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
        #region GetUserRoleMenuList
        public static List<GetUserRoleMenuList> GetUserRoleMenuList(String moduleID, String SiteID, Int32 roleID, String loginSiteID, Int32 loginUserID)
        {
            List<GetUserRoleMenuList> result = new List<GetUserRoleMenuList>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetUserRoleMenuList));
                ctx.CommandText = "GetUserRoleMenuList";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("p_ModuleID", moduleID);
                ctx.Add("p_SiteID", SiteID);
                ctx.Add("p_RoleID", roleID);
                ctx.Add("p_LoginSiteID", loginSiteID);
                ctx.Add("p_LoginUserID", loginUserID);
                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetUserRoleMenuList)helper.IDataReaderToObject(reader, new GetUserRoleMenuList()));
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

        public static List<dynamic> GetDataReport(string procedureName, List<Variable> lstVariable)
        {
            var result = new List<dynamic>();
            IDbContext ctx = DbFactory.Configure();
            string typeName = string.Format("CodeX.Data.Model.{0}", procedureName);
            try
            {
                DbHelper helper = new DbHelper(Type.GetType(typeName));
                ctx.CommandText = procedureName;
                ctx.CommandType = CommandType.StoredProcedure;
                ctx.Clear();
                //Add Parameter
                foreach (Variable variable in lstVariable)
                {
                    ctx.Add(variable.Code, variable.Value);
                }
                //Get DataReader
                //result = DaoBase.GetDataTable(ctx);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add(helper.IDataReaderToObject(reader, Activator.CreateInstance(Type.GetType(typeName))));
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
    }
}