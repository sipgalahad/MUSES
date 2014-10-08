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
