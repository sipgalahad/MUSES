using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeX.Data.Core.Dal;
using System.Data;
using System.Data.SqlClient;

namespace CodeX.Data.Model
{
    public static partial class BusinessLayer
    {
        #region FillStockTakingDt
        public static void FillStockTakingDt(Int32 StockTakingID, Int32 LocationID, DateTime Date, String Time, int UserID, IDbContext ctx = null)
        {
            bool IsCtxNull = false;
            if (ctx == null)
            {
                IsCtxNull = true;
                ctx = DbFactory.Configure();
            }
            ctx.CommandText = "FillStockTakingDt";
            ctx.CommandType = CommandType.StoredProcedure;
            ctx.Command.Parameters.Add(new SqlParameter("@StockTakingID", StockTakingID));
            ctx.Command.Parameters.Add(new SqlParameter("@LocationID", LocationID));
            ctx.Command.Parameters.Add(new SqlParameter("@Date", Date));
            ctx.Command.Parameters.Add(new SqlParameter("@Time", Time));
            ctx.Command.Parameters.Add(new SqlParameter("@UserID", UserID));

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
        }
        #endregion
        #region GenerateARProspectiveStudent
        public static void GenerateARProspectiveStudent(int UserID, String SiteID, int RegistrationID, IDbContext ctx = null)
        {
            bool IsCtxNull = false;
            if (ctx == null)
            {
                IsCtxNull = true;
                ctx = DbFactory.Configure();
            }
            ctx.CommandText = "GenerateARProspectiveStudent";
            ctx.CommandType = CommandType.StoredProcedure;
            ctx.Command.Parameters.Add(new SqlParameter("@UserID", UserID));
            ctx.Command.Parameters.Add(new SqlParameter("@SiteID", SiteID));
            ctx.Command.Parameters.Add(new SqlParameter("@RegistrationID", RegistrationID));

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
        }
        #endregion
        #region GenerateFADepreciation
        public static void GenerateFADepreciation(int FixedAssetID, int CreatedBy, IDbContext ctx = null)
        {
            bool IsCtxNull = false;
            if (ctx == null)
            {
                IsCtxNull = true;
                ctx = DbFactory.Configure();
            }
            ctx.CommandText = "GenerateFADepreciation";
            ctx.CommandType = CommandType.StoredProcedure;
            ctx.Command.Parameters.Add(new SqlParameter("@FixedAssetID", FixedAssetID));
            ctx.Command.Parameters.Add(new SqlParameter("@CreatedBy", CreatedBy));

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
        }
        #endregion
        #region GetAPSupplierInformation
        public static List<GetAPSupplierInformation> GetAPSupplierInformationList(String MovementDate, Int32 PageIndex, Int32 NumRows)
        {
            List<GetAPSupplierInformation> result = new List<GetAPSupplierInformation>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetAPSupplierInformation));
                ctx.CommandText = "GetAPSupplierInformation";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("MovementDate", MovementDate);
                ctx.Add("PageIndex", PageIndex);
                ctx.Add("NumRows", NumRows);

                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetAPSupplierInformation)helper.IDataReaderToObject(reader, new GetAPSupplierInformation()));
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
        #region GetAPSupplierInformationDt
        public static List<GetAPSupplierInformationDt> GetAPSupplierInformationDtList(String MovementDate, Int32 SupplierID, Int32 Start, Int32 End)
        {
            List<GetAPSupplierInformationDt> result = new List<GetAPSupplierInformationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetAPSupplierInformationDt));
                ctx.CommandText = "GetAPSupplierInformationDt";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("MovementDate", MovementDate);
                ctx.Add("SupplierID", SupplierID);
                ctx.Add("Start", Start);
                ctx.Add("End", End);

                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetAPSupplierInformationDt)helper.IDataReaderToObject(reader, new GetAPSupplierInformationDt()));
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
        #region GetGLBalanceDtPerPeriod
        public static Int32 GetGLBalanceDtPerPeriodRowCount(Int32 GLAccountID, Int32 year, Int32 month, IDbContext ctx)
        {
            List<GetGLBalanceDtPerPeriod> result = new List<GetGLBalanceDtPerPeriod>();
            SqlParameter param = new SqlParameter();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetGLBalanceDtPerPeriod));
                ctx.CommandText = "GetGLBalanceDtPerPeriodRowCount";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("GLAccountID", GLAccountID);
                ctx.Add("JournalYear", year);
                ctx.Add("JournalMonth", month);

                param.ParameterName = "@Result";
                param.SqlDbType = SqlDbType.Int;
                param.Size = 20;
                param.Direction = ParameterDirection.Output;

                ctx.Command.Parameters.Add(param);
                DaoBase.ExecuteNonQuery(ctx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return (Int32)param.Value;
        }
        public static List<GetGLBalanceDtPerPeriod> GetGLBalanceDtPerPeriodList(Int32 GLAccountID, Int32 year, Int32 month, Int32 PageIndex, Int32 NumRows, IDbContext ctx)
        {
            List<GetGLBalanceDtPerPeriod> result = new List<GetGLBalanceDtPerPeriod>();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetGLBalanceDtPerPeriod));
                ctx.CommandText = "GetGLBalanceDtPerPeriod";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("GLAccountID", GLAccountID);
                ctx.Add("JournalYear", year);
                ctx.Add("JournalMonth", month);
                ctx.Add("PageIndex", PageIndex);
                ctx.Add("NumRows", NumRows);
                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetGLBalanceDtPerPeriod)helper.IDataReaderToObject(reader, new GetGLBalanceDtPerPeriod()));
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
        public static List<GetGLBalanceDtPerPeriod> GetGLBalanceDtPerPeriodList(Int32 GLAccountID, Int32 year, Int32 month, Int32 PageIndex, Int32 NumRows)
        {
            IDbContext ctx = DbFactory.Configure();
            return GetGLBalanceDtPerPeriodList(GLAccountID, year, month, PageIndex, NumRows, ctx);
        }
        public static Int32 GetGLBalanceDtPerPeriodRowCount(Int32 GLAccountID, Int32 year, Int32 month)
        {
            IDbContext ctx = DbFactory.Configure();
            return GetGLBalanceDtPerPeriodRowCount(GLAccountID, year, month, ctx);
        }
        #endregion
        #region GetGLBalanceDtPerSubLedger
        public static List<GetGLBalanceDtPerSubLedger> GetGLBalanceDtPerSubLedgerList(Int32 GLAccountID, Int32 SubLedger, Int32 year, Int32 month, Int32 PageIndex, Int32 NumRows)
        {
            List<GetGLBalanceDtPerSubLedger> result = new List<GetGLBalanceDtPerSubLedger>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetGLBalanceDtPerSubLedger));
                ctx.CommandText = "GetGLBalanceDtPerSubLedger";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("GLAccountID", GLAccountID);
                ctx.Add("SubLedger", SubLedger);
                ctx.Add("JournalYear", year);
                ctx.Add("JournalMonth", month);
                ctx.Add("PageIndex", PageIndex);
                ctx.Add("NumRows", NumRows);
                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetGLBalanceDtPerSubLedger)helper.IDataReaderToObject(reader, new GetGLBalanceDtPerSubLedger()));
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
        public static List<GetGLBalanceDtPerSubLedger> GetGLBalanceDtPerSubLedgerList(Int32 GLAccountID, Int32 SubLedger, Int32 year, Int32 month)
        {
            return GetGLBalanceDtPerSubLedgerList(GLAccountID, SubLedger, year, month, 1, 1000);
        }
        public static Int32 GetGLBalanceDtPerSubLedgerRowCount(Int32 GLAccountID, Int32 SubLedger, Int32 year, Int32 month, IDbContext ctx)
        {
            List<GetGLBalanceDtPerSubLedger> result = new List<GetGLBalanceDtPerSubLedger>();
            SqlParameter param = new SqlParameter();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetGLBalanceDtPerSubLedger));
                ctx.CommandText = "GetGLBalanceDtPerSubLedgerRowCount";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("GLAccountID", GLAccountID);
                ctx.Add("SubLedger", SubLedger);
                ctx.Add("JournalYear", year);
                ctx.Add("JournalMonth", month);

                param.ParameterName = "@Result";
                param.SqlDbType = SqlDbType.Int;
                param.Size = 20;
                param.Direction = ParameterDirection.Output;

                ctx.Command.Parameters.Add(param);
                DaoBase.ExecuteNonQuery(ctx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return (Int32)param.Value;
        }
        public static Int32 GetGLBalanceDtPerSubLedgerRowCount(Int32 GLAccountID, Int32 SubLedger, Int32 year, Int32 month)
        {
            IDbContext ctx = DbFactory.Configure();
            return GetGLBalanceDtPerSubLedgerRowCount(GLAccountID, SubLedger, year, month, ctx);
        }
        #endregion
        #region GetGLBalanceDtInformation
        public static List<GetGLBalanceDtInformation> GetGLBalanceDtInformationList(Int32 GLAccountID, Int32 SubLedger, Int32 year, Int32 month, Int32 PageIndex, Int32 NumRows)
        {
            List<GetGLBalanceDtInformation> result = new List<GetGLBalanceDtInformation>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetGLBalanceDtInformation));
                ctx.CommandText = "GetGLBalanceDtInformation";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("GLAccountID", GLAccountID);
                ctx.Add("SubLedger", SubLedger);
                ctx.Add("JournalYear", year);
                ctx.Add("JournalMonth", month);
                ctx.Add("PageIndex", PageIndex);
                ctx.Add("NumRows", NumRows);
                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetGLBalanceDtInformation)helper.IDataReaderToObject(reader, new GetGLBalanceDtInformation()));
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
        public static List<GetGLBalanceDtInformation> GetGLBalanceDtInformationList(Int32 GLAccountID, Int32 SubLedger, Int32 year, Int32 month)
        {
            return GetGLBalanceDtInformationList(GLAccountID, SubLedger, year, month, 1, 1000);
        }
        public static Int32 GetGLBalanceDtInformationRowCount(Int32 GLAccountID, Int32 SubLedger, Int32 year, Int32 month, IDbContext ctx)
        {
            List<GetGLBalanceDtInformation> result = new List<GetGLBalanceDtInformation>();
            SqlParameter param = new SqlParameter();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetGLBalanceDtInformation));
                ctx.CommandText = "GetGLBalanceDtInformationRowCount";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("GLAccountID", GLAccountID);
                ctx.Add("SubLedger", SubLedger);
                ctx.Add("JournalYear", year);
                ctx.Add("JournalMonth", month);

                param.ParameterName = "@Result";
                param.SqlDbType = SqlDbType.Int;
                param.Size = 20;
                param.Direction = ParameterDirection.Output;

                ctx.Command.Parameters.Add(param);
                DaoBase.ExecuteNonQuery(ctx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return (Int32)param.Value;
        }
        public static Int32 GetGLBalanceDtInformationRowCount(Int32 GLAccountID, Int32 SubLedger, Int32 year, Int32 month)
        {
            IDbContext ctx = DbFactory.Configure();
            return GetGLBalanceDtInformationRowCount(GLAccountID, SubLedger, year, month, ctx);
        }
        #endregion
        #region GetGLBalancePerGLAccount
        public static List<GetGLBalancePerGLAccount> GetGLBalancePerGLAccountList(Int32 GLAccountID, Int32 year, Int32 month, Int32 PageIndex, Int32 NumRows, IDbContext ctx)
        {
            List<GetGLBalancePerGLAccount> result = new List<GetGLBalancePerGLAccount>();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetGLBalancePerGLAccount));
                ctx.CommandText = "GetGLBalancePerGLAccount";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("GLAccountID", GLAccountID);
                ctx.Add("JournalYear", year);
                ctx.Add("JournalMonth", month);
                ctx.Add("PageIndex", PageIndex);
                ctx.Add("NumRows", NumRows);
                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetGLBalancePerGLAccount)helper.IDataReaderToObject(reader, new GetGLBalancePerGLAccount()));
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
        public static List<GetGLBalancePerGLAccount> GetGLBalancePerGLAccountList(Int32 GLAccountID, Int32 year, Int32 month, Int32 PageIndex, Int32 NumRows)
        {
            IDbContext ctx = DbFactory.Configure();
            return GetGLBalancePerGLAccountList(GLAccountID, year, month, PageIndex, NumRows, ctx);
        }
        public static Int32 GetGLBalancePerGLAccountRowCount(Int32 GLAccountID, Int32 year, Int32 month, IDbContext ctx)
        {
            List<GetGLBalancePerGLAccount> result = new List<GetGLBalancePerGLAccount>();
            SqlParameter param = new SqlParameter();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetGLBalancePerGLAccount));
                ctx.CommandText = "GetGLBalancePerGLAccountRowCount";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("GLAccountID", GLAccountID);
                ctx.Add("JournalYear", year);
                ctx.Add("JournalMonth", month);

                param.ParameterName = "@Result";
                param.SqlDbType = SqlDbType.Int;
                param.Size = 20;
                param.Direction = ParameterDirection.Output;

                ctx.Command.Parameters.Add(param);
                DaoBase.ExecuteNonQuery(ctx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return (Int32)param.Value;
        }
        public static Int32 GetGLBalancePerGLAccountRowCount(Int32 GLAccountID, Int32 year, Int32 month)
        {
            IDbContext ctx = DbFactory.Configure();
            return GetGLBalancePerGLAccountRowCount(GLAccountID, year, month, ctx);
        }
        #endregion
        #region GetGLBalancePerPeriod
        public static Int32 GetGLBalancePerPeriodRowCount(string siteID, Int32 year, Int32 month, Boolean IsDetailOnly, IDbContext ctx)
        {
            List<GetGLBalancePerPeriod> result = new List<GetGLBalancePerPeriod>();
            SqlParameter param = new SqlParameter();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetGLBalancePerPeriod));
                ctx.CommandText = "GetGLBalancePerPeriodRowCount";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("SiteID", siteID);
                ctx.Add("JournalYear", year);
                ctx.Add("JournalMonth", month);
                ctx.Add("IsDetailOnly", IsDetailOnly);

                param.ParameterName = "@Result";
                param.SqlDbType = SqlDbType.Int;
                param.Size = 20;
                param.Direction = ParameterDirection.Output;

                ctx.Command.Parameters.Add(param);
                DaoBase.ExecuteNonQuery(ctx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return (Int32)param.Value;
        }
        public static List<GetGLBalancePerPeriod> GetGLBalancePerPeriodList(String siteID, Int32 year, Int32 month, Boolean IsDetailOnly, Int32 PageIndex, Int32 NumRows, IDbContext ctx)
        {
            List<GetGLBalancePerPeriod> result = new List<GetGLBalancePerPeriod>();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetGLBalancePerPeriod));
                ctx.CommandText = "GetGLBalancePerPeriod";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("SiteID", siteID);
                ctx.Add("JournalYear", year);
                ctx.Add("JournalMonth", month);
                ctx.Add("IsDetailOnly", IsDetailOnly);
                ctx.Add("PageIndex", PageIndex);
                ctx.Add("NumRows", NumRows);
                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetGLBalancePerPeriod)helper.IDataReaderToObject(reader, new GetGLBalancePerPeriod()));
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
        public static List<GetGLBalancePerPeriod> GetGLBalancePerPeriodList(string siteID, Int32 year, Int32 month, Boolean IsDetailOnly, Int32 PageIndex, Int32 NumRows)
        {
            IDbContext ctx = DbFactory.Configure();
            return GetGLBalancePerPeriodList(siteID, year, month, IsDetailOnly, PageIndex, NumRows, ctx);
        }
        public static Int32 GetGLBalancePerPeriodRowCount(string siteID, Int32 year, Int32 month, Boolean IsDetailOnly)
        {
            IDbContext ctx = DbFactory.Configure();
            return GetGLBalancePerPeriodRowCount(siteID, year, month, IsDetailOnly, ctx);
        }
        #endregion
        #region GetGLBalanceProfitLossPerPeriodPerLevel
        public static List<GetGLBalanceProfitLossPerPeriodPerLevel> GetGLBalanceProfitLossPerPeriodPerLevelList(String HealthcareID, Int32 JournalYear, Int32 JournalMonth, Int32 AccountLevel, Int32 PageIndex, Int32 NumRows, IDbContext ctx)
        {
            List<GetGLBalanceProfitLossPerPeriodPerLevel> result = new List<GetGLBalanceProfitLossPerPeriodPerLevel>();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetGLBalanceProfitLossPerPeriodPerLevel));
                ctx.CommandText = "GetGLBalanceProfitLossPerPeriodPerLevel";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("SiteID", HealthcareID);
                ctx.Add("JournalYear", JournalYear);
                ctx.Add("JournalMonth", JournalMonth);
                ctx.Add("AccountLevel", AccountLevel);
                ctx.Add("PageIndex", PageIndex);
                ctx.Add("NumRows", NumRows);
                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetGLBalanceProfitLossPerPeriodPerLevel)helper.IDataReaderToObject(reader, new GetGLBalanceProfitLossPerPeriodPerLevel()));
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
        public static List<GetGLBalanceProfitLossPerPeriodPerLevel> GetGLBalanceProfitLossPerPeriodPerLevelList(String HealthcareID, Int32 JournalYear, Int32 JournalMonth, Int32 AccountLevel, Int32 PageIndex = 1, Int32 NumRows = 5000)
        {
            IDbContext ctx = DbFactory.Configure();
            return GetGLBalanceProfitLossPerPeriodPerLevelList(HealthcareID, JournalYear, JournalMonth, AccountLevel, PageIndex, NumRows, ctx);
        }
        #endregion
        #region GetItemMovementPerPeriodeDetail
        public static List<GetItemMovementPerPeriodeDetail> GetItemMovementPerPeriodeDetail(string movementDate, int locationID, string itemName, Int32 PageIndex, Int32 NumRows)
        {
            List<GetItemMovementPerPeriodeDetail> result = new List<GetItemMovementPerPeriodeDetail>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetItemMovementPerPeriodeDetail));
                ctx.CommandText = "GetItemMovementPerPeriodeDetail";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("MovementDate", movementDate);
                ctx.Add("LocationID", locationID);
                ctx.Add("ItemName", itemName);
                ctx.Add("PageIndex", PageIndex);
                ctx.Add("NumRows", NumRows);

                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetItemMovementPerPeriodeDetail)helper.IDataReaderToObject(reader, new GetItemMovementPerPeriodeDetail()));
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
        #region GetItemQtyOnOrder
        public static List<GetItemQtyOnOrder> GetItemQtyOnOrder(int itemID, int locationID, int type)
        {
            List<GetItemQtyOnOrder> result = new List<GetItemQtyOnOrder>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetItemQtyOnOrder));
                ctx.CommandText = "GetItemQtyOnOrder";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("ItemID", itemID);
                ctx.Add("LocationID", locationID);
                ctx.Add("Type", type);
                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetItemQtyOnOrder)helper.IDataReaderToObject(reader, new GetItemQtyOnOrder()));
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
    }
}
