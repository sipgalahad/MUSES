using System;
using System.Data;
using System.Data.SqlClient;

namespace CodeX.Data.Core.Dal
{
    public static class DaoBase
    {

        public static int ExecuteNonQuery(IDbContext ctx, bool checkQuery)
        {
            if (checkQuery && ctx.CommandText.Trim().Equals(string.Empty))
            {
                return 0;
            }
            return ExecuteNonQuery(ctx);
        }

        public static int ExecuteNonQuery(IDbContext ctx)
        {
            bool isNullTransaction = false;
            // Selalu pakai transaction untuk mencegah record ditable lain di update bila ada proses yg gagal
            if (ctx.Transaction == null)
            {
                isNullTransaction = true;
                ctx.Command.Transaction = ctx.Command.Connection.BeginTransaction();
            }
            int result;
            try
            {
                object temp = ctx.Command.ExecuteScalar();

                if (IsNumber(temp))
                    result = Convert.ToInt32(temp);
                else
                    result = 0;
                if (isNullTransaction)
                    ctx.Command.Transaction.Commit();
            }
            catch (SqlException ex)
            {
                if (isNullTransaction)
                    ctx.Command.Transaction.Rollback();
                throw new Exception(ex.Message, ex);
            }
            catch (Exception ex)
            {
                if (isNullTransaction)
                    ctx.Command.Transaction.Rollback();
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isNullTransaction)
                    ctx.Command.Transaction = null;
                ctx.Close();
            }
            return result;
        }
        public static bool IsNumber(this object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }

        public static DataSet GetDataSet(IDbContext ctx)
        {
            DataSet ds = new DataSet();
            IDataAdapter da = ctx.DataAdapter;

            // Untuk keperluan error handling & loging
            try
            {
                //Error bila untuk app windows, jadi diabaikan saja
                //HttpContext.Current.Session["_LastSqlException"] = null;
                //HttpContext.Current.Session["_LastSqlCommand"] = ctx.Command;
            }
            catch (Exception) { }


            try
            {
                da.Fill(ds);
            }
            catch (SqlException ex)
            {
                try
                {
                    //Error bila untuk app windows, jadi diabaikan saja
                    //HttpContext.Current.Session["_LastSqlException"] = ex;
                }
                catch (Exception) { }
                
                throw new Exception(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return ds;
        }

        public static DataTable GetDataTable(IDbContext ctx)
        {
            return GetDataSet(ctx).Tables[0];
        }

        public static DataRow GetDataRow(IDbContext ctx)
        {
            DataTable dt = GetDataTable(ctx);
            return (dt.Rows.Count > 0) ? dt.Rows[0] : null;
        }

        public static IDataReader GetDataReader(IDbContext ctx)
        {
            // Untuk keperluan error handling & loging
            try
            {
                //Error bila untuk app windows, jadi diabaikan saja
                //HttpContext.Current.Session["_LastSqlException"] = null;
                //HttpContext.Current.Session["_LastSqlCommand"] = ctx.Command;
            }
            catch (Exception){}


            IDataReader idr;
            try
            {
                idr = ctx.Command.ExecuteReader();
            }
            catch (SqlException ex)
            {
                try
                {
                    //Error bila untuk app windows, jadi diabaikan saja
                    //HttpContext.Current.Session["_LastSqlException"] = ex;
                }
                catch (Exception) { }
                throw new Exception(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Clear();
            }
            return idr;
        }

        public static object ExecuteScalar(IDbContext ctx)
        {

            // Untuk keperluan error handling & loging
            try
            {
                //Error bila untuk app windows, jadi diabaikan saja
                //HttpContext.Current.Session["_LastSqlException"] = null;
                //HttpContext.Current.Session["_LastSqlCommand"] = ctx.Command;
            }
            catch (Exception) { }

            object result;
            try
            {
                result = ctx.Command.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                try
                {
                    //Error bila untuk app windows, jadi diabaikan saja
                    //HttpContext.Current.Session["_LastSqlException"] = ex;
                }
                catch (Exception) { }
                
                throw new Exception(ex.Message, ex);
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