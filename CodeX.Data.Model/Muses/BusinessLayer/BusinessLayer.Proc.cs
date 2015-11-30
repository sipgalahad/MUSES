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
        #region DeleteARProspectiveStudent
        public static void DeleteARProspectiveStudent(int UserID, int RegistrationID, IDbContext ctx = null)
        {
            bool IsCtxNull = false;
            if (ctx == null)
            {
                IsCtxNull = true;
                ctx = DbFactory.Configure();
            }
            ctx.CommandText = "DeleteARProspectiveStudent";
            ctx.CommandType = CommandType.StoredProcedure;
            ctx.Command.Parameters.Add(new SqlParameter("@UserID", UserID));
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
        #region GenerateARInvoiceProspectiveStudent
        public static void GenerateARInvoiceProspectiveStudent(String lstProspectiveStudent, String SiteID, int Month, int Year, int UserID, IDbContext ctx = null)
        {
            bool IsCtxNull = false;
            if (ctx == null)
            {
                IsCtxNull = true;
                ctx = DbFactory.Configure();
            }
            ctx.CommandText = "GenerateARInvoiceProspectiveStudent";
            ctx.CommandType = CommandType.StoredProcedure;
            ctx.Command.Parameters.Add(new SqlParameter("@lstProspectiveStudent", lstProspectiveStudent));
            ctx.Command.Parameters.Add(new SqlParameter("@SiteID", SiteID));
            ctx.Command.Parameters.Add(new SqlParameter("@Month", Month));
            ctx.Command.Parameters.Add(new SqlParameter("@Year", Year));
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
        #region GenerateARInvoiceStudent
        public static void GenerateARInvoiceStudent(String lstStudent, String SiteID, int Month, int Year, int UserID, IDbContext ctx = null)
        {
            bool IsCtxNull = false;
            if (ctx == null)
            {
                IsCtxNull = true;
                ctx = DbFactory.Configure();
            }
            ctx.CommandText = "GenerateARInvoiceStudent";
            ctx.CommandType = CommandType.StoredProcedure;
            ctx.Command.Parameters.Add(new SqlParameter("@lstStudent", lstStudent));
            ctx.Command.Parameters.Add(new SqlParameter("@SiteID", SiteID));
            ctx.Command.Parameters.Add(new SqlParameter("@Month", Month));
            ctx.Command.Parameters.Add(new SqlParameter("@Year", Year));
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
        #region GenerateEmployeeCode
        public static string GenerateEmployeeCode(String GCDepartment, DateTime HiredDate, IDbContext ctx = null)
        {
            bool IsCtxNull = false;
            if (ctx == null)
            {
                IsCtxNull = true;
                ctx = DbFactory.Configure();
            }
            ctx.CommandText = "GenerateEmployeeCode";
            ctx.CommandType = CommandType.StoredProcedure;
            ctx.Command.Parameters.Add(new SqlParameter("@GCDepartment", GCDepartment));
            ctx.Command.Parameters.Add(new SqlParameter("@HiredDate", HiredDate));

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
        #region GenerateProspectiveStudentCode
        public static string GenerateProspectiveStudentCode(String SiteID, int Year, String RegistrationNo, IDbContext ctx = null)
        {
            bool IsCtxNull = false;
            if (ctx == null)
            {
                IsCtxNull = true;
                ctx = DbFactory.Configure();
            }
            ctx.CommandText = "GenerateProspectiveStudentCode";
            ctx.CommandType = CommandType.StoredProcedure;
            ctx.Command.Parameters.Add(new SqlParameter("@SiteID", SiteID));
            ctx.Command.Parameters.Add(new SqlParameter("@Year", Year));
            ctx.Command.Parameters.Add(new SqlParameter("@RegistrationNo", RegistrationNo));

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
        #region GenerateRegistrationNo
        public static string GenerateRegistrationNo(Int32 SchoolPeriodID, IDbContext ctx = null)
        {
            bool IsCtxNull = false;
            if (ctx == null)
            {
                IsCtxNull = true;
                ctx = DbFactory.Configure();
            }
            ctx.CommandText = "GenerateRegistrationNo";
            ctx.CommandType = CommandType.StoredProcedure;
            ctx.Command.Parameters.Add(new SqlParameter("@SchoolPeriodID", SchoolPeriodID));

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
        #region GetARCustomerInformation
        public static List<GetARCustomerInformation> GetARCustomerInformation(String MovementDate, Int32 PageIndex, Int32 NumRows)
        {
            List<GetARCustomerInformation> result = new List<GetARCustomerInformation>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetARCustomerInformation));
                ctx.CommandText = "GetARCustomerInformation";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("MovementDate", MovementDate);
                ctx.Add("PageIndex", PageIndex);
                ctx.Add("NumRows", NumRows);

                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetARCustomerInformation)helper.IDataReaderToObject(reader, new GetARCustomerInformation()));
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
        #region GetARCustomerInformationDt
        public static List<GetARCustomerInformationDt> GetARCustomerInformationDtList(String MovementDate, Int32 BusinessPartnerID, Int32 Start, Int32 End)
        {
            List<GetARCustomerInformationDt> result = new List<GetARCustomerInformationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetARCustomerInformationDt));
                ctx.CommandText = "GetARCustomerInformationDt";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("MovementDate", MovementDate);
                ctx.Add("BusinessPartnerID", BusinessPartnerID);
                ctx.Add("Start", Start);
                ctx.Add("End", End);

                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetARCustomerInformationDt)helper.IDataReaderToObject(reader, new GetARCustomerInformationDt()));
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
        #region GetARProspectiveStudentInformation
        public static List<GetARProspectiveStudentInformation> GetARProspectiveStudentInformation(String MovementDate, Int32 PageIndex, Int32 NumRows)
        {
            List<GetARProspectiveStudentInformation> result = new List<GetARProspectiveStudentInformation>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetARProspectiveStudentInformation));
                ctx.CommandText = "GetARProspectiveStudentInformation";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("MovementDate", MovementDate);
                ctx.Add("PageIndex", PageIndex);
                ctx.Add("NumRows", NumRows);

                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetARProspectiveStudentInformation)helper.IDataReaderToObject(reader, new GetARProspectiveStudentInformation()));
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
        #region GetARProspectiveStudentInformationDt
        public static List<GetARProspectiveStudentInformationDt> GetARProspectiveStudentInformationDtList(String MovementDate, Int32 ProspectiveStudentID, Int32 Start, Int32 End)
        {
            List<GetARProspectiveStudentInformationDt> result = new List<GetARProspectiveStudentInformationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetARProspectiveStudentInformationDt));
                ctx.CommandText = "GetARProspectiveStudentInformationDt";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("MovementDate", MovementDate);
                ctx.Add("ProspectiveStudentID", ProspectiveStudentID);
                ctx.Add("Start", Start);
                ctx.Add("End", End);

                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetARProspectiveStudentInformationDt)helper.IDataReaderToObject(reader, new GetARProspectiveStudentInformationDt()));
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
        #region GetARStudentInformation
        public static List<GetARStudentInformation> GetARStudentInformation(String MovementDate, Int32 PageIndex, Int32 NumRows)
        {
            List<GetARStudentInformation> result = new List<GetARStudentInformation>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetARStudentInformation));
                ctx.CommandText = "GetARStudentInformation";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("MovementDate", MovementDate);
                ctx.Add("PageIndex", PageIndex);
                ctx.Add("NumRows", NumRows);

                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetARStudentInformation)helper.IDataReaderToObject(reader, new GetARStudentInformation()));
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
        #region GetARStudentInformation
        public static List<GetARStudentInformation> GetARStudentInformation(String MovementDate, String LstStudentID)
        {
            List<GetARStudentInformation> result = new List<GetARStudentInformation>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetARStudentInformation));
                ctx.CommandText = "GetARStudentInformation2";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("MovementDate", MovementDate);
                ctx.Add("LstStudentID", LstStudentID);

                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetARStudentInformation)helper.IDataReaderToObject(reader, new GetARStudentInformation()));
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
        #region GetARStudentInformationDt
        public static List<GetARStudentInformationDt> GetARStudentInformationDtList(String MovementDate, Int32 StudentID, Int32 Start, Int32 End)
        {
            List<GetARStudentInformationDt> result = new List<GetARStudentInformationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetARStudentInformationDt));
                ctx.CommandText = "GetARStudentInformationDt";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("MovementDate", MovementDate);
                ctx.Add("StudentID", StudentID);
                ctx.Add("Start", Start);
                ctx.Add("End", End);

                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetARStudentInformationDt)helper.IDataReaderToObject(reader, new GetARStudentInformationDt()));
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
        #region GetGLBalancePerPeriodForTBalance
        public static List<GetGLBalancePerPeriodForTBalance> GetGLBalancePerPeriodForTBalance(String SiteID, Int32 year, Int32 month)
        {
            List<GetGLBalancePerPeriodForTBalance> result = new List<GetGLBalancePerPeriodForTBalance>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetGLBalancePerPeriodForTBalance));
                ctx.CommandText = "GetGLBalancePerPeriodForTBalance";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("SiteID", SiteID);
                ctx.Add("JournalYear", year);
                ctx.Add("JournalMonth", month);
                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetGLBalancePerPeriodForTBalance)helper.IDataReaderToObject(reader, new GetGLBalancePerPeriodForTBalance()));
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
        #region GetGLBalancePerLevelCompare
        public static List<GetGLBalancePerLevelCompare> GetGLBalancePerLevelCompareList(string siteID, Int32 year, Int32 month, Int32 year2, Int32 month2, Boolean AccountLevel, IDbContext ctx)
        {
            List<GetGLBalancePerLevelCompare> result = new List<GetGLBalancePerLevelCompare>();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetGLBalancePerLevelCompare));
                ctx.CommandText = "GetGLBalancePerLevelCompare";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("SiteID", siteID);
                ctx.Add("JournalYear", year);
                ctx.Add("JournalMonth", month);
                ctx.Add("JournalYear2", year2);
                ctx.Add("JournalMonth2", month2);
                ctx.Add("AccountLevel", AccountLevel);
                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetGLBalancePerLevelCompare)helper.IDataReaderToObject(reader, new GetGLBalancePerLevelCompare()));
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
        public static List<GetGLBalanceProfitLossPerPeriodPerLevel> GetGLBalanceProfitLossPerPeriodPerLevelList(String SiteID, Int32 JournalYear, Int32 JournalMonth, Int32 AccountLevel, Int32 PageIndex, Int32 NumRows, IDbContext ctx)
        {
            List<GetGLBalanceProfitLossPerPeriodPerLevel> result = new List<GetGLBalanceProfitLossPerPeriodPerLevel>();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetGLBalanceProfitLossPerPeriodPerLevel));
                ctx.CommandText = "GetGLBalanceProfitLossPerPeriodPerLevel";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("SiteID", SiteID);
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
        public static List<GetGLBalanceProfitLossPerPeriodPerLevel> GetGLBalanceProfitLossPerPeriodPerLevelList(String SiteID, Int32 JournalYear, Int32 JournalMonth, Int32 AccountLevel, Int32 PageIndex = 1, Int32 NumRows = 5000)
        {
            IDbContext ctx = DbFactory.Configure();
            return GetGLBalanceProfitLossPerPeriodPerLevelList(SiteID, JournalYear, JournalMonth, AccountLevel, PageIndex, NumRows, ctx);
        }
        #endregion
        #region GetItemMasterSales
        public static List<GetItemMasterSales> GetItemMasterSalesList(string siteID, int itemID, int studentID, int locationID, int type, DateTime transactionDate, IDbContext ctx)
        {
            List<GetItemMasterSales> result = new List<GetItemMasterSales>();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetItemMasterSales));
                ctx.CommandText = "GetItemMasterSales";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("p_SiteID", siteID);
                ctx.Add("p_ItemID", itemID);
                ctx.Add("p_StudentID", studentID);
                ctx.Add("p_LocationID", locationID);
                ctx.Add("p_Type", type);
                ctx.Add("p_TransactionDate", transactionDate);
                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetItemMasterSales)helper.IDataReaderToObject(reader, new GetItemMasterSales()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }

        public static List<GetItemMasterSales> GetItemMasterSalesList(string siteID, int itemID, int studentID, int locationID, int type, DateTime transactionDate)
        {
            List<GetItemMasterSales> result = new List<GetItemMasterSales>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetItemMasterSales));
                ctx.CommandText = "GetItemMasterSales";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("p_SiteID", siteID);
                ctx.Add("p_ItemID", itemID);
                ctx.Add("p_StudentID", studentID);
                ctx.Add("p_LocationID", locationID);
                ctx.Add("p_Type", type);
                ctx.Add("p_TransactionDate", transactionDate);
                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetItemMasterSales)helper.IDataReaderToObject(reader, new GetItemMasterSales()));
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
        #region GetItemUsagePurchaseRequestROP
        public static List<GetItemUsagePurchaseRequestROP> GetItemUsagePurchaseRequestROP(String LstLocationID, String LstItemID)
        {
            List<GetItemUsagePurchaseRequestROP> result = new List<GetItemUsagePurchaseRequestROP>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetItemUsagePurchaseRequestROP));
                ctx.CommandText = "GetItemUsagePurchaseRequestROP";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("LstLocationID", LstLocationID);
                ctx.Add("LstItemID", LstItemID);

                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetItemUsagePurchaseRequestROP)helper.IDataReaderToObject(reader, new GetItemUsagePurchaseRequestROP()));
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
        #region GetStudentReceiveSummary
        public static List<GetStudentReceiveSummary> GetStudentReceiveSummary(String SiteID, Int32 year, Int32 month)
        {
            List<GetStudentReceiveSummary> result = new List<GetStudentReceiveSummary>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetStudentReceiveSummary));
                ctx.CommandText = "GetStudentReceiveSummary";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("SiteID", SiteID);
                ctx.Add("Month", month);
                ctx.Add("Year", year);
                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetStudentReceiveSummary)helper.IDataReaderToObject(reader, new GetStudentReceiveSummary()));
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
        #region GetStudentReceiveSummaryDt
        public static List<GetStudentReceiveSummaryDt> GetStudentReceiveSummaryDt(String SiteID, Int32 year, Int32 month, String type, Int32 studentFeeCompTypeID)
        {
            List<GetStudentReceiveSummaryDt> result = new List<GetStudentReceiveSummaryDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetStudentReceiveSummaryDt));
                ctx.CommandText = "GetStudentReceiveSummaryDt";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("SiteID", SiteID);
                ctx.Add("Month", month);
                ctx.Add("Year", year);
                ctx.Add("Type", type);
                ctx.Add("StudentFeeCompTypeID", studentFeeCompTypeID);
                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetStudentReceiveSummaryDt)helper.IDataReaderToObject(reader, new GetStudentReceiveSummaryDt()));
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
        #region GetStudentRevenue
        public static List<GetStudentRevenue> GetStudentRevenue(String SiteID, Int32 year, Int32 month)
        {
            List<GetStudentRevenue> result = new List<GetStudentRevenue>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetStudentRevenue));
                ctx.CommandText = "GetStudentRevenue";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("SiteID", SiteID);
                ctx.Add("Month", month);
                ctx.Add("Year", year);
                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetStudentRevenue)helper.IDataReaderToObject(reader, new GetStudentRevenue()));
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
        #region PostingJournal
        public static bool PostingJournal(String SiteID, String PeriodNo, Int32 CreatedBy, IDbContext ctx = null)
        {
            bool IsCtxNull = false;
            if (ctx == null)
            {
                IsCtxNull = true;
                ctx = DbFactory.Configure();
            }
            ctx.CommandText = "PostingJournal";
            ctx.CommandType = CommandType.StoredProcedure;
            ctx.Command.Parameters.Add(new SqlParameter("@SiteID", SiteID));
            ctx.Command.Parameters.Add(new SqlParameter("@PeriodNo", PeriodNo));
            ctx.Command.Parameters.Add(new SqlParameter("@CreatedBy", CreatedBy));
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@Result";
            param.SqlDbType = SqlDbType.Bit;
            param.Size = 1;
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
            return (bool)param.Value;
        }
        #endregion
        #region ProcessInterfaceJournal
        public static string ProcessInterfaceJournal(String SiteID, String JournalDate, String TransactionCode, int UserID, IDbContext ctx = null)
        {
            bool IsCtxNull = false;
            if (ctx == null)
            {
                IsCtxNull = true;
                ctx = DbFactory.Configure();
            }
            ctx.CommandText = "ProcessInterfaceJournal";
            ctx.CommandType = CommandType.StoredProcedure;
            ctx.Command.Parameters.Add(new SqlParameter("@SiteID", SiteID));
            ctx.Command.Parameters.Add(new SqlParameter("@JournalDate", JournalDate));
            ctx.Command.Parameters.Add(new SqlParameter("@TransactionCode", TransactionCode));
            ctx.Command.Parameters.Add(new SqlParameter("@UserID", UserID));

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@Result";
            param.SqlDbType = SqlDbType.VarChar;
            param.Size = 1000;
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
        #region ProcessProspectiveStudentAcceptance
        public static void ProcessProspectiveStudentAcceptance(String lstRegistration, DateTime acceptedDate, String SiteID, int UserID, IDbContext ctx = null)
        {
            bool IsCtxNull = false;
            if (ctx == null)
            {
                IsCtxNull = true;
                ctx = DbFactory.Configure();
            }
            ctx.CommandText = "ProcessProspectiveStudentAcceptance";
            ctx.CommandType = CommandType.StoredProcedure;
            ctx.Command.Parameters.Add(new SqlParameter("@lstRegistration", lstRegistration));
            ctx.Command.Parameters.Add(new SqlParameter("@AcceptedDate", acceptedDate));
            ctx.Command.Parameters.Add(new SqlParameter("@SiteID", SiteID));
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
        #region ProcessReRegistrationStudent
        public static void ProcessReRegistrationStudent(String lstStudentID, int NextSchoolPeriodID, int UserID, IDbContext ctx = null)
        {
            bool IsCtxNull = false;
            if (ctx == null)
            {
                IsCtxNull = true;
                ctx = DbFactory.Configure();
            }
            ctx.CommandText = "ProcessReRegistrationStudent";
            ctx.CommandType = CommandType.StoredProcedure;
            ctx.Command.Parameters.Add(new SqlParameter("@lstStudentID", lstStudentID));
            ctx.Command.Parameters.Add(new SqlParameter("@NextSchoolPeriodID", NextSchoolPeriodID));
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
    }
}
