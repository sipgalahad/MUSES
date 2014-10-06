using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeX.Data.Core.Dal;
using System.Data;

namespace CodeX.Data.Model
{
    public static partial class BusinessLayer
    {
        #region vClassMeeting
        public static List<vClassMeeting> GetvClassMeetingList(string filterExpression)
        {
            List<vClassMeeting> result = new List<vClassMeeting>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassMeeting));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vClassMeeting)helper.IDataReaderToObject(reader, new vClassMeeting()));
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
        public static List<vClassMeeting> GetvClassMeetingList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vClassMeeting> result = new List<vClassMeeting>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassMeeting));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vClassMeeting)helper.IDataReaderToObject(reader, new vClassMeeting()));
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
        public static Int32 GetvClassMeetingRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassMeeting));
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
        #region vClassSchedule
        public static List<vClassSchedule> GetvClassScheduleList(string filterExpression)
        {
            List<vClassSchedule> result = new List<vClassSchedule>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassSchedule));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vClassSchedule)helper.IDataReaderToObject(reader, new vClassSchedule()));
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
        #region vClassStudent
        public static List<vClassStudent> GetvClassStudentList(string filterExpression)
        {
            List<vClassStudent> result = new List<vClassStudent>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassStudent));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vClassStudent)helper.IDataReaderToObject(reader, new vClassStudent()));
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
        #region vClassSubject
        public static List<vClassSubject> GetvClassSubjectList(string filterExpression)
        {
            List<vClassSubject> result = new List<vClassSubject>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassSubject));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vClassSubject)helper.IDataReaderToObject(reader, new vClassSubject()));
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
        #region vClassSubjectCustom
        public static List<vClassSubjectCustom> GetvClassSubjectCustomList(string filterExpression)
        {
            List<vClassSubjectCustom> result = new List<vClassSubjectCustom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassSubjectCustom));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vClassSubjectCustom)helper.IDataReaderToObject(reader, new vClassSubjectCustom()));
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
        #region vDailyScheduleTypeDt
        public static List<vDailyScheduleTypeDt> GetvDailyScheduleTypeDtList(string filterExpression)
        {
            List<vDailyScheduleTypeDt> result = new List<vDailyScheduleTypeDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vDailyScheduleTypeDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vDailyScheduleTypeDt)helper.IDataReaderToObject(reader, new vDailyScheduleTypeDt()));
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
        #region vDirectPurchaseDt
        public static List<vDirectPurchaseDt> GetvDirectPurchaseDtList(string filterExpression)
        {
            List<vDirectPurchaseDt> result = new List<vDirectPurchaseDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vDirectPurchaseDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vDirectPurchaseDt)helper.IDataReaderToObject(reader, new vDirectPurchaseDt()));
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
        public static List<vDirectPurchaseDt> GetvDirectPurchaseDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vDirectPurchaseDt> result = new List<vDirectPurchaseDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vDirectPurchaseDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vDirectPurchaseDt)helper.IDataReaderToObject(reader, new vDirectPurchaseDt()));
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

        public static Int32 GetvDirectPurchaseDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vDirectPurchaseDt));
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
        #region vDirectPurchaseHd
        public static List<vDirectPurchaseHd> GetvDirectPurchaseHdList(string filterExpression)
        {
            List<vDirectPurchaseHd> result = new List<vDirectPurchaseHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vDirectPurchaseHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vDirectPurchaseHd)helper.IDataReaderToObject(reader, new vDirectPurchaseHd()));
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
        public static List<vDirectPurchaseHd> GetvDirectPurchaseHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vDirectPurchaseHd> result = new List<vDirectPurchaseHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vDirectPurchaseHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vDirectPurchaseHd)helper.IDataReaderToObject(reader, new vDirectPurchaseHd()));
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

        public static Int32 GetvDirectPurchaseHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vDirectPurchaseHd));
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

        public static Int32 GetvDirectPurchaseHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vDirectPurchaseHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "DirectPurchaseNo", keyValue, orderByExpression);
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

        public static vDirectPurchaseHd GetvDirectPurchaseHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vDirectPurchaseHd> result = new List<vDirectPurchaseHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vDirectPurchaseHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vDirectPurchaseHd)helper.IDataReaderToObject(reader, new vDirectPurchaseHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            if (result.Count > 0)
                return result[0];
            return null;
        }
        #endregion
        #region vDirectPurchaseReturnDt
        public static List<vDirectPurchaseReturnDt> GetvDirectPurchaseReturnDtList(string filterExpression)
        {
            List<vDirectPurchaseReturnDt> result = new List<vDirectPurchaseReturnDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vDirectPurchaseReturnDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vDirectPurchaseReturnDt)helper.IDataReaderToObject(reader, new vDirectPurchaseReturnDt()));
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
        public static Int32 GetvDirectPurchaseReturnDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vDirectPurchaseReturnDt));
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
        public static List<vDirectPurchaseReturnDt> GetvDirectPurchaseReturnDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vDirectPurchaseReturnDt> result = new List<vDirectPurchaseReturnDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vDirectPurchaseReturnDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vDirectPurchaseReturnDt)helper.IDataReaderToObject(reader, new vDirectPurchaseReturnDt()));
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
        #region vDirectPurchaseReturnHd
        public static List<vDirectPurchaseReturnHd> GetvDirectPurchaseReturnHdList(string filterExpression)
        {
            List<vDirectPurchaseReturnHd> result = new List<vDirectPurchaseReturnHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vDirectPurchaseReturnHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vDirectPurchaseReturnHd)helper.IDataReaderToObject(reader, new vDirectPurchaseReturnHd()));
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

        public static List<vDirectPurchaseReturnHd> GetvDirectPurchaseReturnHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vDirectPurchaseReturnHd> result = new List<vDirectPurchaseReturnHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vDirectPurchaseReturnHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vDirectPurchaseReturnHd)helper.IDataReaderToObject(reader, new vDirectPurchaseReturnHd()));
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

        public static Int32 GetvDirectPurchaseReturnHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vDirectPurchaseReturnHd));
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

        public static Int32 GetvDirectPurchaseReturnHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vDirectPurchaseReturnHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "DirectPurchaseReturnNo", keyValue, orderByExpression);
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

        public static vDirectPurchaseReturnHd GetvDirectPurchaseReturnHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vDirectPurchaseReturnHd> result = new List<vDirectPurchaseReturnHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vDirectPurchaseReturnHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vDirectPurchaseReturnHd)helper.IDataReaderToObject(reader, new vDirectPurchaseReturnHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            if (result.Count > 0)
                return result[0];
            return null;
        }
        #endregion
        #region vItemBalance
        public static List<vItemBalance> GetvItemBalanceList(string filterExpression, IDbContext ctx)
        {
            List<vItemBalance> result = new List<vItemBalance>();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemBalance));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemBalance)helper.IDataReaderToObject(reader, new vItemBalance()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }

        public static List<vItemBalance> GetvItemBalanceList(string filterExpression)
        {
            List<vItemBalance> result = new List<vItemBalance>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemBalance));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemBalance)helper.IDataReaderToObject(reader, new vItemBalance()));
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

        public static List<vItemBalance> GetvItemBalanceList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vItemBalance> result = new List<vItemBalance>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemBalance));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemBalance)helper.IDataReaderToObject(reader, new vItemBalance()));
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
        public static Int32 GetvItemBalanceRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemBalance));
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
        public static Int32 GetvItemBalanceRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemBalance));
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
        #region vItemBalanceInventory
        public static List<vItemBalanceInventory> GetvItemBalanceInventoryList(string filterExpression, IDbContext ctx)
        {
            List<vItemBalanceInventory> result = new List<vItemBalanceInventory>();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemBalanceInventory));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemBalanceInventory)helper.IDataReaderToObject(reader, new vItemBalanceInventory()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }

        public static List<vItemBalanceInventory> GetvItemBalanceInventoryList(string filterExpression)
        {
            List<vItemBalanceInventory> result = new List<vItemBalanceInventory>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemBalanceInventory));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemBalanceInventory)helper.IDataReaderToObject(reader, new vItemBalanceInventory()));
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

        public static List<vItemBalanceInventory> GetvItemBalanceInventoryList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vItemBalanceInventory> result = new List<vItemBalanceInventory>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemBalanceInventory));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemBalanceInventory)helper.IDataReaderToObject(reader, new vItemBalanceInventory()));
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
        public static Int32 GetvItemBalanceInventoryRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemBalanceInventory));
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
        public static Int32 GetvItemBalanceInventoryRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemBalanceInventory));
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
        #region vItemDistributionDt
        public static List<vItemDistributionDt> GetvItemDistributionDtList(string filterExpression)
        {
            List<vItemDistributionDt> result = new List<vItemDistributionDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemDistributionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemDistributionDt)helper.IDataReaderToObject(reader, new vItemDistributionDt()));
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
        public static Int32 GetvItemDistributionDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemDistributionDt));
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

        public static List<vItemDistributionDt> GetvItemDistributionDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vItemDistributionDt> result = new List<vItemDistributionDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemDistributionDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemDistributionDt)helper.IDataReaderToObject(reader, new vItemDistributionDt()));
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
        #region vItemDistributionHd
        public static List<vItemDistributionHd> GetvItemDistributionHdList(string filterExpression)
        {
            List<vItemDistributionHd> result = new List<vItemDistributionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemDistributionHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemDistributionHd)helper.IDataReaderToObject(reader, new vItemDistributionHd()));
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

        public static Int32 GetvItemDistributionHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemDistributionHd));
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

        public static vItemDistributionHd GetvItemDistributionHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vItemDistributionHd> result = new List<vItemDistributionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemDistributionHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemDistributionHd)helper.IDataReaderToObject(reader, new vItemDistributionHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            if (result.Count > 0)
                return result[0];
            return null;
        }
        public static Int32 GetvItemDistributionHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemDistributionHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "DistributionNo", keyValue, orderByExpression);
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

        public static List<vItemDistributionHd> GetvItemDistributionHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vItemDistributionHd> result = new List<vItemDistributionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemDistributionHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemDistributionHd)helper.IDataReaderToObject(reader, new vItemDistributionHd()));
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
        #region vItemRequestDt
        public static List<vItemRequestDt> GetvItemRequestDtList(string filterExpression)
        {
            List<vItemRequestDt> result = new List<vItemRequestDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemRequestDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemRequestDt)helper.IDataReaderToObject(reader, new vItemRequestDt()));
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

        public static Int32 GetvItemRequestDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemRequestDt));
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

        public static List<vItemRequestDt> GetvItemRequestDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vItemRequestDt> result = new List<vItemRequestDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemRequestDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemRequestDt)helper.IDataReaderToObject(reader, new vItemRequestDt()));
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
        #region vItemRequestDtRealizationPerItem
        public static List<vItemRequestDtRealizationPerItem> GetvItemRequestDtRealizationPerItemList(string filterExpression)
        {
            List<vItemRequestDtRealizationPerItem> result = new List<vItemRequestDtRealizationPerItem>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemRequestDtRealizationPerItem));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemRequestDtRealizationPerItem)helper.IDataReaderToObject(reader, new vItemRequestDtRealizationPerItem()));
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
        #region vItemRequestDtRealizationPerItemPerOrder
        public static List<vItemRequestDtRealizationPerItemPerOrder> GetvItemRequestDtRealizationPerItemPerOrderList(string filterExpression)
        {
            List<vItemRequestDtRealizationPerItemPerOrder> result = new List<vItemRequestDtRealizationPerItemPerOrder>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemRequestDtRealizationPerItemPerOrder));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemRequestDtRealizationPerItemPerOrder)helper.IDataReaderToObject(reader, new vItemRequestDtRealizationPerItemPerOrder()));
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
        #region vItemRequestHd
        public static List<vItemRequestHd> GetvItemRequestHdList(string filterExpression)
        {
            List<vItemRequestHd> result = new List<vItemRequestHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemRequestHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemRequestHd)helper.IDataReaderToObject(reader, new vItemRequestHd()));
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
        public static Int32 GetvItemRequestHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemRequestHd));
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

        public static vItemRequestHd GetvItemRequestHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vItemRequestHd> result = new List<vItemRequestHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemRequestHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemRequestHd)helper.IDataReaderToObject(reader, new vItemRequestHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            if (result.Count > 0)
                return result[0];
            return null;
        }
        public static Int32 GetvItemRequestHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemRequestHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ItemRequestNo", keyValue, orderByExpression);
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

        public static List<vItemRequestHd> GetvItemRequestHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vItemRequestHd> result = new List<vItemRequestHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemRequestHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemRequestHd)helper.IDataReaderToObject(reader, new vItemRequestHd()));
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
        #region vItemTransactionDt
        public static List<vItemTransactionDt> GetvItemTransactionDtList(string filterExpression)
        {
            List<vItemTransactionDt> result = new List<vItemTransactionDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemTransactionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemTransactionDt)helper.IDataReaderToObject(reader, new vItemTransactionDt()));
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
        public static List<vItemTransactionDt> GetvItemTransactionDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vItemTransactionDt> result = new List<vItemTransactionDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemTransactionDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemTransactionDt)helper.IDataReaderToObject(reader, new vItemTransactionDt()));
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

        public static Int32 GetvItemTransactionDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemTransactionDt));
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
        public static Int32 GetvItemTransactionDtRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemTransactionDt));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "TransactionDtID", keyValue, orderByExpression);
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
        #region vItemTransactionHd
        public static List<vItemTransactionHd> GetvItemTransactionHdList(string filterExpression)
        {
            List<vItemTransactionHd> result = new List<vItemTransactionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemTransactionHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemTransactionHd)helper.IDataReaderToObject(reader, new vItemTransactionHd()));
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
        public static List<vItemTransactionHd> GetvItemTransactionHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vItemTransactionHd> result = new List<vItemTransactionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemTransactionHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemTransactionHd)helper.IDataReaderToObject(reader, new vItemTransactionHd()));
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
        public static Int32 GetvItemTransactionHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemTransactionHd));
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
        public static Int32 GetvItemTransactionHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemTransactionHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "TransactionNo", keyValue, orderByExpression);
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

        public static vItemTransactionHd GetvItemTransactionHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vItemTransactionHd> result = new List<vItemTransactionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemTransactionHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemTransactionHd)helper.IDataReaderToObject(reader, new vItemTransactionHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            if (result.Count > 0)
                return result[0];
            return null;
        }
        #endregion
        #region vPeriodAdmission
        public static List<vPeriodAdmission> GetvPeriodAdmissionList(string filterExpression)
        {
            List<vPeriodAdmission> result = new List<vPeriodAdmission>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPeriodAdmission));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPeriodAdmission)helper.IDataReaderToObject(reader, new vPeriodAdmission()));
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
        #region vPeriodClassType
        public static List<vPeriodClassType> GetvPeriodClassTypeList(string filterExpression)
        {
            List<vPeriodClassType> result = new List<vPeriodClassType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPeriodClassType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPeriodClassType)helper.IDataReaderToObject(reader, new vPeriodClassType()));
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
        #region vPeriodClassTypeSubject
        public static List<vPeriodClassTypeSubject> GetvPeriodClassTypeSubjectList(string filterExpression)
        {
            List<vPeriodClassTypeSubject> result = new List<vPeriodClassTypeSubject>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPeriodClassTypeSubject));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPeriodClassTypeSubject)helper.IDataReaderToObject(reader, new vPeriodClassTypeSubject()));
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
        #region vPeriodSchedule
        public static List<vPeriodSchedule> GetvPeriodScheduleList(string filterExpression)
        {
            List<vPeriodSchedule> result = new List<vPeriodSchedule>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPeriodSchedule));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPeriodSchedule)helper.IDataReaderToObject(reader, new vPeriodSchedule()));
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
        #region vPeriodSection
        public static List<vPeriodSection> GetvPeriodSectionList(string filterExpression)
        {
            List<vPeriodSection> result = new List<vPeriodSection>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPeriodSection));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPeriodSection)helper.IDataReaderToObject(reader, new vPeriodSection()));
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
        #region vPurchaseOrderDt
        public static List<vPurchaseOrderDt> GetvPurchaseOrderDtList(string filterExpression)
        {
            List<vPurchaseOrderDt> result = new List<vPurchaseOrderDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseOrderDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseOrderDt)helper.IDataReaderToObject(reader, new vPurchaseOrderDt()));
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

        public static Int32 GetvPurchaseOrderDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseOrderDt));
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
        public static List<vPurchaseOrderDt> GetvPurchaseOrderDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseOrderDt> result = new List<vPurchaseOrderDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseOrderDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseOrderDt)helper.IDataReaderToObject(reader, new vPurchaseOrderDt()));
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
        #region vPurchaseOrderDtOutStanding
        public static List<vPurchaseOrderDtOutStanding> GetvPurchaseOrderDtOutStandingList(string filterExpression)
        {
            List<vPurchaseOrderDtOutStanding> result = new List<vPurchaseOrderDtOutStanding>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseOrderDtOutStanding));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseOrderDtOutStanding)helper.IDataReaderToObject(reader, new vPurchaseOrderDtOutStanding()));
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
        public static Int32 GetvPurchaseOrderDtOutStandingRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseOrderDtOutStanding));
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
        public static List<vPurchaseOrderDtOutStanding> GetvPurchaseOrderDtOutStandingList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseOrderDtOutStanding> result = new List<vPurchaseOrderDtOutStanding>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseOrderDtOutStanding));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseOrderDtOutStanding)helper.IDataReaderToObject(reader, new vPurchaseOrderDtOutStanding()));
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
        #region vPurchaseOrderHd
        public static List<vPurchaseOrderHd> GetvPurchaseOrderHdList(string filterExpression)
        {
            List<vPurchaseOrderHd> result = new List<vPurchaseOrderHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseOrderHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseOrderHd)helper.IDataReaderToObject(reader, new vPurchaseOrderHd()));
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
        public static List<vPurchaseOrderHd> GetvPurchaseOrderHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseOrderHd> result = new List<vPurchaseOrderHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseOrderHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseOrderHd)helper.IDataReaderToObject(reader, new vPurchaseOrderHd()));
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

        public static Int32 GetvPurchaseOrderHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseOrderHd));
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

        public static Int32 GetvPurchaseOrderHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseOrderHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "PurchaseOrderNo", keyValue, orderByExpression);
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

        public static vPurchaseOrderHd GetvPurchaseOrderHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseOrderHd> result = new List<vPurchaseOrderHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseOrderHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseOrderHd)helper.IDataReaderToObject(reader, new vPurchaseOrderHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            if (result.Count > 0)
                return result[0];
            return null;
        }
        #endregion
        #region vPurchaseReceiveDt
        public static List<vPurchaseReceiveDt> GetvPurchaseReceiveDtList(string filterExpression)
        {
            List<vPurchaseReceiveDt> result = new List<vPurchaseReceiveDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReceiveDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReceiveDt)helper.IDataReaderToObject(reader, new vPurchaseReceiveDt()));
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
        public static List<vPurchaseReceiveDt> GetvPurchaseReceiveDtList(string filterExpression, IDbContext ctx)
        {
            List<vPurchaseReceiveDt> result = new List<vPurchaseReceiveDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReceiveDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReceiveDt)helper.IDataReaderToObject(reader, new vPurchaseReceiveDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetvPurchaseReceiveDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReceiveDt));
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
        public static List<vPurchaseReceiveDt> GetvPurchaseReceiveDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseReceiveDt> result = new List<vPurchaseReceiveDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReceiveDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReceiveDt)helper.IDataReaderToObject(reader, new vPurchaseReceiveDt()));
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
        #region vPurchaseReceiveHd
        public static List<vPurchaseReceiveHd> GetvPurchaseReceiveHdList(string filterExpression)
        {
            List<vPurchaseReceiveHd> result = new List<vPurchaseReceiveHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReceiveHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReceiveHd)helper.IDataReaderToObject(reader, new vPurchaseReceiveHd()));
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

        public static List<vPurchaseReceiveHd> GetvPurchaseReceiveHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseReceiveHd> result = new List<vPurchaseReceiveHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReceiveHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReceiveHd)helper.IDataReaderToObject(reader, new vPurchaseReceiveHd()));
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

        public static Int32 GetvPurchaseReceiveHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReceiveHd));
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

        public static Int32 GetvPurchaseReceiveHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReceiveHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "PurchaseReceiveNo", keyValue, orderByExpression);
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

        public static vPurchaseReceiveHd GetvPurchaseReceiveHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseReceiveHd> result = new List<vPurchaseReceiveHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReceiveHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReceiveHd)helper.IDataReaderToObject(reader, new vPurchaseReceiveHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            if (result.Count > 0)
                return result[0];
            return null;
        }
        #endregion
        #region vPurchaseRequestDt
        public static List<vPurchaseRequestDt> GetvPurchaseRequestDtList(string filterExpression)
        {
            List<vPurchaseRequestDt> result = new List<vPurchaseRequestDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseRequestDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseRequestDt)helper.IDataReaderToObject(reader, new vPurchaseRequestDt()));
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
        public static Int32 GetvPurchaseRequestDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseRequestDt));
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
        public static List<vPurchaseRequestDt> GetvPurchaseRequestDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseRequestDt> result = new List<vPurchaseRequestDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseRequestDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseRequestDt)helper.IDataReaderToObject(reader, new vPurchaseRequestDt()));
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
        #region vPurchaseRequestDtOutstanding
        public static List<vPurchaseRequestDtOutstanding> GetvPurchaseRequestDtOutstandingList(string filterExpression)
        {
            List<vPurchaseRequestDtOutstanding> result = new List<vPurchaseRequestDtOutstanding>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseRequestDtOutstanding));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseRequestDtOutstanding)helper.IDataReaderToObject(reader, new vPurchaseRequestDtOutstanding()));
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
        public static Int32 GetvPurchaseRequestDtOutstandingRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseRequestDtOutstanding));
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
        public static List<vPurchaseRequestDtOutstanding> GetvPurchaseRequestDtOutstandingList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseRequestDtOutstanding> result = new List<vPurchaseRequestDtOutstanding>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseRequestDtOutstanding));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseRequestDtOutstanding)helper.IDataReaderToObject(reader, new vPurchaseRequestDtOutstanding()));
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
        #region vPurchaseRequestHd
        public static List<vPurchaseRequestHd> GetvPurchaseRequestHdList(string filterExpression)
        {
            List<vPurchaseRequestHd> result = new List<vPurchaseRequestHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseRequestHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseRequestHd)helper.IDataReaderToObject(reader, new vPurchaseRequestHd()));
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

        public static List<vPurchaseRequestHd> GetvPurchaseRequestHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseRequestHd> result = new List<vPurchaseRequestHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseRequestHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseRequestHd)helper.IDataReaderToObject(reader, new vPurchaseRequestHd()));
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

        public static Int32 GetvPurchaseRequestHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseRequestHd));
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

        public static Int32 GetvPurchaseRequestHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseRequestHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "PurchaseRequestNo", keyValue, orderByExpression);
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

        public static vPurchaseRequestHd GetvPurchaseRequestHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseRequestHd> result = new List<vPurchaseRequestHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseRequestHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseRequestHd)helper.IDataReaderToObject(reader, new vPurchaseRequestHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            if (result.Count > 0)
                return result[0];
            return null;
        }
        #endregion
        #region vPurchaseRequestPO
        public static List<vPurchaseRequestPO> GetvPurchaseRequestPOList(string filterExpression)
        {
            List<vPurchaseRequestPO> result = new List<vPurchaseRequestPO>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseRequestPO));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseRequestPO)helper.IDataReaderToObject(reader, new vPurchaseRequestPO()));
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
        public static List<vPurchaseRequestPO> GetvPurchaseRequestPOList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseRequestPO> result = new List<vPurchaseRequestPO>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseRequestPO));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseRequestPO)helper.IDataReaderToObject(reader, new vPurchaseRequestPO()));
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
        public static Int32 GetvPurchaseRequestPORowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseRequestPO));
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
        #region vSchoolClass
        public static List<vSchoolClass> GetvSchoolClassList(string filterExpression)
        {
            List<vSchoolClass> result = new List<vSchoolClass>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSchoolClass));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSchoolClass)helper.IDataReaderToObject(reader, new vSchoolClass()));
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
        #region vStudent
        public static List<vStudent> GetvStudentList(string filterExpression)
        {
            List<vStudent> result = new List<vStudent>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudent));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudent)helper.IDataReaderToObject(reader, new vStudent()));
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
        public static Int32 GetvStudentRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudent));
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
        public static List<vStudent> GetvStudentList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vStudent> result = new List<vStudent>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudent));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudent)helper.IDataReaderToObject(reader, new vStudent()));
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
        public static Int32 GetvStudentRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudent));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ReportID", keyValue, orderByExpression);
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
        #region vSubjectGradeMajor
        public static List<vSubjectGradeMajor> GetvSubjectGradeMajorList(string filterExpression)
        {
            List<vSubjectGradeMajor> result = new List<vSubjectGradeMajor>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSubjectGradeMajor));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSubjectGradeMajor)helper.IDataReaderToObject(reader, new vSubjectGradeMajor()));
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
        #region vSupplierItem
        public static List<vSupplierItem> GetvSupplierItemList(string filterExpression)
        {
            List<vSupplierItem> result = new List<vSupplierItem>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSupplierItem));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSupplierItem)helper.IDataReaderToObject(reader, new vSupplierItem()));
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
        #region vSupplierItemPlaning
        public static List<vSupplierItemPlaning> GetvSupplierItemPlaningList(string filterExpression)
        {
            List<vSupplierItemPlaning> result = new List<vSupplierItemPlaning>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSupplierItemPlaning));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSupplierItemPlaning)helper.IDataReaderToObject(reader, new vSupplierItemPlaning()));
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

        public static List<vSupplierItemPlaning> GetvSupplierItemPlaningList(string filterExpression, IDbContext ctx)
        {
            List<vSupplierItemPlaning> result = new List<vSupplierItemPlaning>();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSupplierItemPlaning));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSupplierItemPlaning)helper.IDataReaderToObject(reader, new vSupplierItemPlaning()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region vTeacher
        public static List<vTeacher> GetvTeacherList(string filterExpression)
        {
            List<vTeacher> result = new List<vTeacher>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacher));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTeacher)helper.IDataReaderToObject(reader, new vTeacher()));
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
        #region vTeacherSubject
        public static List<vTeacherSubject> GetvTeacherSubjectList(string filterExpression)
        {
            List<vTeacherSubject> result = new List<vTeacherSubject>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherSubject));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTeacherSubject)helper.IDataReaderToObject(reader, new vTeacherSubject()));
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
        public static List<vTeacherSubject> GetvTeacherSubjectList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTeacherSubject> result = new List<vTeacherSubject>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherSubject));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTeacherSubject)helper.IDataReaderToObject(reader, new vTeacherSubject()));
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
        public static Int32 GetvTeacherSubjectRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherSubject));
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
    }
}
