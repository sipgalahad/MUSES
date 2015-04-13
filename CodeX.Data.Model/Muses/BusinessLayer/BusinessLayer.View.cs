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
        #region vAdmissionFeeComp
        public static List<vAdmissionFeeComp> GetvAdmissionFeeCompList(string filterExpression)
        {
            List<vAdmissionFeeComp> result = new List<vAdmissionFeeComp>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vAdmissionFeeComp));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vAdmissionFeeComp)helper.IDataReaderToObject(reader, new vAdmissionFeeComp()));
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
        public static List<vAdmissionFeeComp> GetvAdmissionFeeCompList(string filterExpression, IDbContext ctx)
        {
            List<vAdmissionFeeComp> result = new List<vAdmissionFeeComp>();
            try
            {
                DbHelper helper = new DbHelper(typeof(vAdmissionFeeComp));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vAdmissionFeeComp)helper.IDataReaderToObject(reader, new vAdmissionFeeComp()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region vAdmissionFeeRuleDtCustom
        public static List<vAdmissionFeeRuleDtCustom> GetvAdmissionFeeRuleDtCustomList(string filterExpression)
        {
            List<vAdmissionFeeRuleDtCustom> result = new List<vAdmissionFeeRuleDtCustom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vAdmissionFeeRuleDtCustom));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vAdmissionFeeRuleDtCustom)helper.IDataReaderToObject(reader, new vAdmissionFeeRuleDtCustom()));
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
        #region vAdmissionFeeRuleHd
        public static List<vAdmissionFeeRuleHd> GetvAdmissionFeeRuleHdList(string filterExpression)
        {
            List<vAdmissionFeeRuleHd> result = new List<vAdmissionFeeRuleHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vAdmissionFeeRuleHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vAdmissionFeeRuleHd)helper.IDataReaderToObject(reader, new vAdmissionFeeRuleHd()));
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
        #region vAPMovement
        public static List<vAPMovement> GetvAPMovementList(string filterExpression)
        {
            List<vAPMovement> result = new List<vAPMovement>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vAPMovement));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vAPMovement)helper.IDataReaderToObject(reader, new vAPMovement()));
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
        public static List<vAPMovement> GetvAPMovementList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vAPMovement> result = new List<vAPMovement>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vAPMovement));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vAPMovement)helper.IDataReaderToObject(reader, new vAPMovement()));
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
        public static Int32 GetvAPMovementRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vAPMovement));
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
        #region vARInvoiceDt
        public static List<vARInvoiceDt> GetvARInvoiceDtList(string filterExpression)
        {
            List<vARInvoiceDt> result = new List<vARInvoiceDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vARInvoiceDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vARInvoiceDt)helper.IDataReaderToObject(reader, new vARInvoiceDt()));
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
        public static List<vARInvoiceDt> GetvARInvoiceDtList(string filterExpression, IDbContext ctx)
        {
            List<vARInvoiceDt> result = new List<vARInvoiceDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(vARInvoiceDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vARInvoiceDt)helper.IDataReaderToObject(reader, new vARInvoiceDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<vARInvoiceDt> GetvARInvoiceDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vARInvoiceDt> result = new List<vARInvoiceDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vARInvoiceDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vARInvoiceDt)helper.IDataReaderToObject(reader, new vARInvoiceDt()));
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
        public static Int32 GetvARInvoiceDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vARInvoiceDt));
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
        #region vARInvoiceHd
        public static List<vARInvoiceHd> GetvARInvoiceHdList(string filterExpression)
        {
            List<vARInvoiceHd> result = new List<vARInvoiceHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vARInvoiceHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vARInvoiceHd)helper.IDataReaderToObject(reader, new vARInvoiceHd()));
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
        public static List<vARInvoiceHd> GetvARInvoiceHdList(string filterExpression,IDbContext ctx)
        {
            List<vARInvoiceHd> result = new List<vARInvoiceHd>();
            try
            {
                DbHelper helper = new DbHelper(typeof(vARInvoiceHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vARInvoiceHd)helper.IDataReaderToObject(reader, new vARInvoiceHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<vARInvoiceHd> GetvARInvoiceHdList(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vARInvoiceHd> result = new List<vARInvoiceHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vARInvoiceHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vARInvoiceHd)helper.IDataReaderToObject(reader, new vARInvoiceHd()));
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
                return result;
            return null;
        }
        public static Int32 GetvARInvoiceHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vARInvoiceHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ARInvoiceNo", keyValue, orderByExpression);
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
        public static Int32 GetvARInvoiceHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vARInvoiceHd));
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
        #region vARMovement
        public static List<vARMovement> GetvARMovementList(string filterExpression)
        {
            List<vARMovement> result = new List<vARMovement>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vARMovement));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vARMovement)helper.IDataReaderToObject(reader, new vARMovement()));
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
        public static List<vARMovement> GetvARMovementList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vARMovement> result = new List<vARMovement>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vARMovement));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vARMovement)helper.IDataReaderToObject(reader, new vARMovement()));
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
        public static Int32 GetvARMovementRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vARMovement));
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
        #region vARReceivingDt
        public static List<vARReceivingDt> GetvARReceivingDtList(string filterExpression)
        {
            List<vARReceivingDt> result = new List<vARReceivingDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vARReceivingDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vARReceivingDt)helper.IDataReaderToObject(reader, new vARReceivingDt()));
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
        #region vARReceivingHd
        public static List<vARReceivingHd> GetvARReceivingHdList(string filterExpression)
        {
            List<vARReceivingHd> result = new List<vARReceivingHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vARReceivingHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vARReceivingHd)helper.IDataReaderToObject(reader, new vARReceivingHd()));
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
        public static vARReceivingHd GetvARReceivingHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vARReceivingHd> result = new List<vARReceivingHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vARReceivingHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vARReceivingHd)helper.IDataReaderToObject(reader, new vARReceivingHd()));
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
        public static Int32 GetvARReceivingHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vARReceivingHd));
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
        public static Int32 GetvARReceivingHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vARReceivingHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ARReceivingNo", keyValue, orderByExpression);
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
        public static List<vARReceivingHd> GetvARReceivingHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vARReceivingHd> result = new List<vARReceivingHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vARReceivingHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vARReceivingHd)helper.IDataReaderToObject(reader, new vARReceivingHd()));
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
        #region vBank
        public static List<vBank> GetvBankList(string filterExpression)
        {
            List<vBank> result = new List<vBank>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vBank));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vBank)helper.IDataReaderToObject(reader, new vBank()));
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
        public static List<vBank> GetvBankList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vBank> result = new List<vBank>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vBank));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vBank)helper.IDataReaderToObject(reader, new vBank()));
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
        public static Int32 GetvBankRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vBank));
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
        public static Int32 GetvBankRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vBank));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "BankID", keyValue, orderByExpression);
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
        #region vChartOfAccount
        public static List<vChartOfAccount> GetvChartOfAccountList(string filterExpression)
        {
            List<vChartOfAccount> result = new List<vChartOfAccount>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vChartOfAccount));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vChartOfAccount)helper.IDataReaderToObject(reader, new vChartOfAccount()));
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
        public static List<vChartOfAccount> GetvChartOfAccountList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vChartOfAccount> result = new List<vChartOfAccount>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vChartOfAccount));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vChartOfAccount)helper.IDataReaderToObject(reader, new vChartOfAccount()));
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
        public static Int32 GetvChartOfAccountRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vChartOfAccount));
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
        public static Int32 GetvChartOfAccountRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vChartOfAccount));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "GLAccountID", keyValue, orderByExpression);
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
        #region vClassMeetingAttendance
        public static List<vClassMeetingAttendance> GetvClassMeetingAttendanceList(string filterExpression)
        {
            List<vClassMeetingAttendance> result = new List<vClassMeetingAttendance>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassMeetingAttendance));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vClassMeetingAttendance)helper.IDataReaderToObject(reader, new vClassMeetingAttendance()));
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
        #region vClassMeetingIndicator
        public static List<vClassMeetingIndicator> GetvClassMeetingIndicatorList(string filterExpression)
        {
            List<vClassMeetingIndicator> result = new List<vClassMeetingIndicator>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassMeetingIndicator));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vClassMeetingIndicator)helper.IDataReaderToObject(reader, new vClassMeetingIndicator()));
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
        #region vClassStudentSubjectTaskMark
        public static List<vClassStudentSubjectTaskMark> GetvClassStudentSubjectTaskMarkList(string filterExpression)
        {
            List<vClassStudentSubjectTaskMark> result = new List<vClassStudentSubjectTaskMark>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassStudentSubjectTaskMark));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vClassStudentSubjectTaskMark)helper.IDataReaderToObject(reader, new vClassStudentSubjectTaskMark()));
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
        #region vClassStudentSubjectTaskRemedialMark
        public static List<vClassStudentSubjectTaskRemedialMark> GetvClassStudentSubjectTaskRemedialMarkList(string filterExpression)
        {
            List<vClassStudentSubjectTaskRemedialMark> result = new List<vClassStudentSubjectTaskRemedialMark>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassStudentSubjectTaskRemedialMark));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vClassStudentSubjectTaskRemedialMark)helper.IDataReaderToObject(reader, new vClassStudentSubjectTaskRemedialMark()));
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
        #region vClassSubjectTask
        public static List<vClassSubjectTask> GetvClassSubjectTaskList(string filterExpression)
        {
            List<vClassSubjectTask> result = new List<vClassSubjectTask>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassSubjectTask));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vClassSubjectTask)helper.IDataReaderToObject(reader, new vClassSubjectTask()));
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
        public static List<vClassSubjectTask> GetvClassSubjectTaskList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vClassSubjectTask> result = new List<vClassSubjectTask>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassSubjectTask));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vClassSubjectTask)helper.IDataReaderToObject(reader, new vClassSubjectTask()));
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
        public static Int32 GetvClassSubjectTaskRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassSubjectTask));
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
        #region vClassSubjectTaskCustom
        public static List<vClassSubjectTaskCustom> GetvClassSubjectTaskCustomList(string filterExpression)
        {
            List<vClassSubjectTaskCustom> result = new List<vClassSubjectTaskCustom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassSubjectTaskCustom));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vClassSubjectTaskCustom)helper.IDataReaderToObject(reader, new vClassSubjectTaskCustom()));
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
        #region vClassSubjectTaskIndicator
        public static List<vClassSubjectTaskIndicator> GetvClassSubjectTaskIndicatorList(string filterExpression)
        {
            List<vClassSubjectTaskIndicator> result = new List<vClassSubjectTaskIndicator>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassSubjectTaskIndicator));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vClassSubjectTaskIndicator)helper.IDataReaderToObject(reader, new vClassSubjectTaskIndicator()));
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
        #region vClassType
        public static List<vClassType> GetvClassTypeList(string filterExpression)
        {
            List<vClassType> result = new List<vClassType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vClassType)helper.IDataReaderToObject(reader, new vClassType()));
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
        public static Int32 GetvClassTypeRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassType));
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
        public static List<vClassType> GetvClassTypeList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vClassType> result = new List<vClassType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassType));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vClassType)helper.IDataReaderToObject(reader, new vClassType()));
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
        public static Int32 GetvClassTypeRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassType));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ClassTypeID", keyValue, orderByExpression);
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
        #region vClassTypeExtracurricular
        public static List<vClassTypeExtracurricular> GetvClassTypeExtracurricularList(string filterExpression)
        {
            List<vClassTypeExtracurricular> result = new List<vClassTypeExtracurricular>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassTypeExtracurricular));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vClassTypeExtracurricular)helper.IDataReaderToObject(reader, new vClassTypeExtracurricular()));
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
        #region vCOAGroup
        public static List<vCOAGroup> GetvCOAGroupList(string filterExpression)
        {
            List<vCOAGroup> result = new List<vCOAGroup>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCOAGroup));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vCOAGroup)helper.IDataReaderToObject(reader, new vCOAGroup()));
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
        public static List<vCOAGroup> GetvCOAGroupList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vCOAGroup> result = new List<vCOAGroup>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCOAGroup));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vCOAGroup)helper.IDataReaderToObject(reader, new vCOAGroup()));
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

        public static Int32 GetvCOAGroupRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCOAGroup));
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
        public static Int32 GetvCOAGroupRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCOAGroup));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "COAGroupID", keyValue, orderByExpression);
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
        #region vCoverageTypeDt
        public static List<vCoverageTypeDt> GetvCoverageTypeDtList(string filterExpression)
        {
            List<vCoverageTypeDt> result = new List<vCoverageTypeDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCoverageTypeDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vCoverageTypeDt)helper.IDataReaderToObject(reader, new vCoverageTypeDt()));
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
        #region vCreditCard
        public static List<vCreditCard> GetvCreditCardList(string filterExpression)
        {
            List<vCreditCard> result = new List<vCreditCard>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCreditCard));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vCreditCard)helper.IDataReaderToObject(reader, new vCreditCard()));
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
        public static List<vCreditCard> GetvCreditCardList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vCreditCard> result = new List<vCreditCard>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCreditCard));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vCreditCard)helper.IDataReaderToObject(reader, new vCreditCard()));
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
        public static Int32 GetvCreditCardRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCreditCard));
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
        public static Int32 GetvCreditCardRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCreditCard));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "CreditCardID", keyValue, orderByExpression);
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
        #region vCustomer
        public static List<vCustomer> GetvCustomerList(string filterExpression)
        {
            List<vCustomer> result = new List<vCustomer>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCustomer));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vCustomer)helper.IDataReaderToObject(reader, new vCustomer()));
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
        #region vCustomerContractMemberCustom
        public static List<vCustomerContractMemberCustom> GetvCustomerContractMemberCustomList(string filterExpression)
        {
            List<vCustomerContractMemberCustom> result = new List<vCustomerContractMemberCustom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCustomerContractMemberCustom));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vCustomerContractMemberCustom)helper.IDataReaderToObject(reader, new vCustomerContractMemberCustom()));
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
        #region vDirectPaymentDt
        public static List<vDirectPaymentDt> GetvDirectPaymentDtList(string filterExpression)
        {
            List<vDirectPaymentDt> result = new List<vDirectPaymentDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vDirectPaymentDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vDirectPaymentDt)helper.IDataReaderToObject(reader, new vDirectPaymentDt()));
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
        #region vDirectPaymentHd
        public static List<vDirectPaymentHd> GetvDirectPaymentHdList(string filterExpression)
        {
            List<vDirectPaymentHd> result = new List<vDirectPaymentHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vDirectPaymentHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vDirectPaymentHd)helper.IDataReaderToObject(reader, new vDirectPaymentHd()));
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
        #region vEDCMachine
        public static List<vEDCMachine> GetvEDCMachineList(string filterExpression)
        {
            List<vEDCMachine> result = new List<vEDCMachine>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vEDCMachine));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vEDCMachine)helper.IDataReaderToObject(reader, new vEDCMachine()));
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
        public static List<vEDCMachine> GetvEDCMachineList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vEDCMachine> result = new List<vEDCMachine>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vEDCMachine));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vEDCMachine)helper.IDataReaderToObject(reader, new vEDCMachine()));
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
        public static Int32 GetvEDCMachineRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vEDCMachine));
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
        #region vExamClassSchedule
        public static List<vExamClassSchedule> GetvExamClassScheduleList(string filterExpression)
        {
            List<vExamClassSchedule> result = new List<vExamClassSchedule>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vExamClassSchedule));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vExamClassSchedule)helper.IDataReaderToObject(reader, new vExamClassSchedule()));
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
        #region vExamScheduleDt
        public static List<vExamScheduleDt> GetvExamScheduleDtList(string filterExpression)
        {
            List<vExamScheduleDt> result = new List<vExamScheduleDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vExamScheduleDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vExamScheduleDt)helper.IDataReaderToObject(reader, new vExamScheduleDt()));
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
        #region vExamScheduleHd
        public static List<vExamScheduleHd> GetvExamScheduleHdList(string filterExpression)
        {
            List<vExamScheduleHd> result = new List<vExamScheduleHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vExamScheduleHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vExamScheduleHd)helper.IDataReaderToObject(reader, new vExamScheduleHd()));
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
        #region vFAGroup
        public static List<vFAGroup> GetvFAGroupList(string filterExpression)
        {
            List<vFAGroup> result = new List<vFAGroup>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vFAGroup));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vFAGroup)helper.IDataReaderToObject(reader, new vFAGroup()));
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
        public static List<vFAGroup> GetvFAGroupList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vFAGroup> result = new List<vFAGroup>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vFAGroup));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vFAGroup)helper.IDataReaderToObject(reader, new vFAGroup()));
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
        public static Int32 GetvFAGroupRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vFAGroup));
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
        public static Int32 GetvFAGroupRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vFAGroup));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "FAGroupID", keyValue, orderByExpression);
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
        #region vFAGroupCOA
        public static List<vFAGroupCOA> GetvFAGroupCOAList(string filterExpression)
        {
            List<vFAGroupCOA> result = new List<vFAGroupCOA>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vFAGroupCOA));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vFAGroupCOA)helper.IDataReaderToObject(reader, new vFAGroupCOA()));
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
        #region vFAItem
        public static List<vFAItem> GetvFAItemList(string filterExpression)
        {
            List<vFAItem> result = new List<vFAItem>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vFAItem));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vFAItem)helper.IDataReaderToObject(reader, new vFAItem()));
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
        public static List<vFAItem> GetvFAItemList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vFAItem> result = new List<vFAItem>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vFAItem));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vFAItem)helper.IDataReaderToObject(reader, new vFAItem()));
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
        public static Int32 GetvFAItemRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vFAItem));
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
        public static Int32 GetvFAItemRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vFAItem));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "FixedAssetID", keyValue, orderByExpression);
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
        #region vFAItemCOA
        public static List<vFAItemCOA> GetvFAItemCOAList(string filterExpression)
        {
            List<vFAItemCOA> result = new List<vFAItemCOA>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vFAItemCOA));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vFAItemCOA)helper.IDataReaderToObject(reader, new vFAItemCOA()));
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
        #region vFAItemMovement
        public static List<vFAItemMovement> GetvFAItemMovementList(string filterExpression)
        {
            List<vFAItemMovement> result = new List<vFAItemMovement>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vFAItemMovement));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vFAItemMovement)helper.IDataReaderToObject(reader, new vFAItemMovement()));
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
        public static List<vFAItemMovement> GetvFAItemMovementList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vFAItemMovement> result = new List<vFAItemMovement>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vFAItemMovement));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vFAItemMovement)helper.IDataReaderToObject(reader, new vFAItemMovement()));
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
        public static Int32 GetvFAItemMovementRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vFAItemMovement));
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
        public static Int32 GetvFAItemMovementRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vFAItemMovement));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "MovementID", keyValue, orderByExpression);
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
        #region vFAWriteOff
        public static List<vFAWriteOff> GetvFAWriteOffList(string filterExpression)
        {
            List<vFAWriteOff> result = new List<vFAWriteOff>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vFAWriteOff));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vFAWriteOff)helper.IDataReaderToObject(reader, new vFAWriteOff()));
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
        #region vGLAccountPayable
        public static List<vGLAccountPayable> GetvGLAccountPayableList(string filterExpression)
        {
            List<vGLAccountPayable> result = new List<vGLAccountPayable>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLAccountPayable));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGLAccountPayable)helper.IDataReaderToObject(reader, new vGLAccountPayable()));
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
        public static List<vGLAccountPayable> GetvGLAccountPayableList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vGLAccountPayable> result = new List<vGLAccountPayable>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLAccountPayable));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGLAccountPayable)helper.IDataReaderToObject(reader, new vGLAccountPayable()));
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
        public static Int32 GetvGLAccountPayableRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLAccountPayable));
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
        public static Int32 GetvGLAccountPayableRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLAccountPayable));
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
        #region vGLAPPayment
        public static List<vGLAPPayment> GetvGLAPPaymentList(string filterExpression)
        {
            List<vGLAPPayment> result = new List<vGLAPPayment>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLAPPayment));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGLAPPayment)helper.IDataReaderToObject(reader, new vGLAPPayment()));
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
        public static List<vGLAPPayment> GetvGLAPPaymentList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vGLAPPayment> result = new List<vGLAPPayment>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLAPPayment));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGLAPPayment)helper.IDataReaderToObject(reader, new vGLAPPayment()));
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
        public static Int32 GetvGLAPPaymentRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLAPPayment));
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
        public static Int32 GetvGLAPPaymentRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLAPPayment));
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
        #region vGLBalanceDtDocument
        public static List<vGLBalanceDtDocument> GetvGLBalanceDtDocumentList(string filterExpression)
        {
            List<vGLBalanceDtDocument> result = new List<vGLBalanceDtDocument>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLBalanceDtDocument));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGLBalanceDtDocument)helper.IDataReaderToObject(reader, new vGLBalanceDtDocument()));
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
        #region vGLBalancePerPeriodNo
        public static List<vGLBalancePerPeriodNo> GetvGLBalancePerPeriodNoList(string filterExpression)
        {
            List<vGLBalancePerPeriodNo> result = new List<vGLBalancePerPeriodNo>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLBalancePerPeriodNo));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGLBalancePerPeriodNo)helper.IDataReaderToObject(reader, new vGLBalancePerPeriodNo()));
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
        #region vGLFAWriteOffAccount
        public static List<vGLFAWriteOffAccount> GetvGLFAWriteOffAccountList(string filterExpression)
        {
            List<vGLFAWriteOffAccount> result = new List<vGLFAWriteOffAccount>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLFAWriteOffAccount));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGLFAWriteOffAccount)helper.IDataReaderToObject(reader, new vGLFAWriteOffAccount()));
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
        public static List<vGLFAWriteOffAccount> GetvGLFAWriteOffAccountList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vGLFAWriteOffAccount> result = new List<vGLFAWriteOffAccount>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLFAWriteOffAccount));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGLFAWriteOffAccount)helper.IDataReaderToObject(reader, new vGLFAWriteOffAccount()));
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
        public static Int32 GetvGLFAWriteOffAccountRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLFAWriteOffAccount));
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
        public static Int32 GetvGLFAWriteOffAccountRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLFAWriteOffAccount));
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
        #region vGLSetting
        public static List<vGLSetting> GetvGLSettingList(string filterExpression)
        {
            List<vGLSetting> result = new List<vGLSetting>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLSetting));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGLSetting)helper.IDataReaderToObject(reader, new vGLSetting()));
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
        public static Int32 GetvGLSettingRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLSetting));
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
        public static List<vGLSetting> GetvGLSettingList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vGLSetting> result = new List<vGLSetting>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLSetting));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGLSetting)helper.IDataReaderToObject(reader, new vGLSetting()));
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
        public static Int32 GetvGLSettingRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLSetting));
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
        #region vGLTransactionDt
        public static List<vGLTransactionDt> GetvGLTransactionDtList(string filterExpression)
        {
            List<vGLTransactionDt> result = new List<vGLTransactionDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLTransactionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGLTransactionDt)helper.IDataReaderToObject(reader, new vGLTransactionDt()));
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
        public static List<vGLTransactionDt> GetvGLTransactionDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vGLTransactionDt> result = new List<vGLTransactionDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLTransactionDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGLTransactionDt)helper.IDataReaderToObject(reader, new vGLTransactionDt()));
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
        public static Int32 GetvGLTransactionDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLTransactionDt));
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
        public static Int32 GetvGLTransactionDtRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLTransactionDt));
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
        #region vGLTransactionDtCustom
        public static List<vGLTransactionDtCustom> GetvGLTransactionDtCustomList(string filterExpression)
        {
            List<vGLTransactionDtCustom> result = new List<vGLTransactionDtCustom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLTransactionDtCustom));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGLTransactionDtCustom)helper.IDataReaderToObject(reader, new vGLTransactionDtCustom()));
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
        #region vGLTransactionHd
        public static List<vGLTransactionHd> GetvGLTransactionHdList(string filterExpression)
        {
            List<vGLTransactionHd> result = new List<vGLTransactionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLTransactionHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGLTransactionHd)helper.IDataReaderToObject(reader, new vGLTransactionHd()));
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
        public static List<vGLTransactionHd> GetvGLTransactionHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vGLTransactionHd> result = new List<vGLTransactionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLTransactionHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGLTransactionHd)helper.IDataReaderToObject(reader, new vGLTransactionHd()));
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
        public static Int32 GetvGLTransactionHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLTransactionHd));
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
        public static Int32 GetvGLTransactionHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLTransactionHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "JournalNo", keyValue, orderByExpression);
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

        public static vGLTransactionHd GetvGLTransactionHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vGLTransactionHd> result = new List<vGLTransactionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLTransactionHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGLTransactionHd)helper.IDataReaderToObject(reader, new vGLTransactionHd()));
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
        #region vGLWarehouseProductLineAccount
        public static List<vGLWarehouseProductLineAccount> GetvGLWarehouseProductLineAccountList(string filterExpression)
        {
            List<vGLWarehouseProductLineAccount> result = new List<vGLWarehouseProductLineAccount>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLWarehouseProductLineAccount));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGLWarehouseProductLineAccount)helper.IDataReaderToObject(reader, new vGLWarehouseProductLineAccount()));
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
        public static List<vGLWarehouseProductLineAccount> GetvGLWarehouseProductLineAccountList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vGLWarehouseProductLineAccount> result = new List<vGLWarehouseProductLineAccount>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLWarehouseProductLineAccount));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGLWarehouseProductLineAccount)helper.IDataReaderToObject(reader, new vGLWarehouseProductLineAccount()));
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
        public static Int32 GetvGLWarehouseProductLineAccountRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLWarehouseProductLineAccount));
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
        public static Int32 GetvGLWarehouseProductLineAccountRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLWarehouseProductLineAccount));
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
        #region vInterfaceJournalSettingDtCustom
        public static List<vInterfaceJournalSettingDtCustom> GetvInterfaceJournalSettingDtCustomList(string filterExpression)
        {
            List<vInterfaceJournalSettingDtCustom> result = new List<vInterfaceJournalSettingDtCustom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vInterfaceJournalSettingDtCustom));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vInterfaceJournalSettingDtCustom)helper.IDataReaderToObject(reader, new vInterfaceJournalSettingDtCustom()));
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
        public static List<vInterfaceJournalSettingDtCustom> GetvInterfaceJournalSettingDtCustomList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vInterfaceJournalSettingDtCustom> result = new List<vInterfaceJournalSettingDtCustom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vInterfaceJournalSettingDtCustom));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vInterfaceJournalSettingDtCustom)helper.IDataReaderToObject(reader, new vInterfaceJournalSettingDtCustom()));
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
        public static Int32 GetvInterfaceJournalSettingDtCustomRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vInterfaceJournalSettingDtCustom));
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
        #region vItemAlternateUnit
        public static List<vItemAlternateUnit> GetvItemAlternateUnitList(string filterExpression)
        {
            List<vItemAlternateUnit> result = new List<vItemAlternateUnit>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemAlternateUnit));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemAlternateUnit)helper.IDataReaderToObject(reader, new vItemAlternateUnit()));
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
        #region vItemAlternateUnitCustom
        public static List<vItemAlternateUnitCustom> GetvItemAlternateUnitCustomList(string filterExpression)
        {
            List<vItemAlternateUnitCustom> result = new List<vItemAlternateUnitCustom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemAlternateUnitCustom));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemAlternateUnitCustom)helper.IDataReaderToObject(reader, new vItemAlternateUnitCustom()));
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
        #region vItemCost
        public static List<vItemCost> GetvItemCostList(string filterExpression)
        {
            List<vItemCost> result = new List<vItemCost>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemCost));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemCost)helper.IDataReaderToObject(reader, new vItemCost()));
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
        #region vItemGroupMaster
        public static List<vItemGroupMaster> GetvItemGroupMasterList(string filterExpression)
        {
            List<vItemGroupMaster> result = new List<vItemGroupMaster>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemGroupMaster));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemGroupMaster)helper.IDataReaderToObject(reader, new vItemGroupMaster()));
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
        public static List<vItemGroupMaster> GetvItemGroupMasterList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vItemGroupMaster> result = new List<vItemGroupMaster>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemGroupMaster));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemGroupMaster)helper.IDataReaderToObject(reader, new vItemGroupMaster()));
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

        public static Int32 GetvItemGroupMasterRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemGroupMaster));
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
        public static Int32 GetvItemGroupMasterRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemGroupMaster));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ItemGroupID", keyValue, orderByExpression);
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
        #region vItemMaster
        public static List<vItemMaster> GetvItemMasterList(string filterExpression)
        {
            List<vItemMaster> result = new List<vItemMaster>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemMaster));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemMaster)helper.IDataReaderToObject(reader, new vItemMaster()));
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
        #region vItemMovement
        public static List<vItemMovement> GetvItemMovementList(string filterExpression)
        {
            List<vItemMovement> result = new List<vItemMovement>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemMovement));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemMovement)helper.IDataReaderToObject(reader, new vItemMovement()));
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
        public static List<vItemMovement> GetvItemMovementList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vItemMovement> result = new List<vItemMovement>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemMovement));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemMovement)helper.IDataReaderToObject(reader, new vItemMovement()));
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
        public static Int32 GetvItemMovementRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemMovement));
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
        #region vItemPlanning
        public static List<vItemPlanning> GetvItemPlanningList(string filterExpression)
        {
            List<vItemPlanning> result = new List<vItemPlanning>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemPlanning));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemPlanning)helper.IDataReaderToObject(reader, new vItemPlanning()));
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
        #region vItemProduct
        public static List<vItemProduct> GetvItemProductList(string filterExpression)
        {
            List<vItemProduct> result = new List<vItemProduct>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemProduct));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemProduct)helper.IDataReaderToObject(reader, new vItemProduct()));
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
        public static List<vItemProduct> GetvItemProductList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vItemProduct> result = new List<vItemProduct>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemProduct));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemProduct)helper.IDataReaderToObject(reader, new vItemProduct()));
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
        public static Int32 GetvItemProductRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemProduct));
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
        public static Int32 GetvItemProductRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemProduct));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ItemID", keyValue, orderByExpression);
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
        #region vItemTariff
        public static List<vItemTariff> GetvItemTariffList(string filterExpression)
        {
            List<vItemTariff> result = new List<vItemTariff>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemTariff));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemTariff)helper.IDataReaderToObject(reader, new vItemTariff()));
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
        public static List<vItemTariff> GetvItemTariffList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vItemTariff> result = new List<vItemTariff>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemTariff));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemTariff)helper.IDataReaderToObject(reader, new vItemTariff()));
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
        public static Int32 GetvItemTariffRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemTariff));
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
        public static Int32 GetvItemTariffRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemTariff));
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
        #region vItemTariffCustom
        public static List<vItemTariffCustom> GetvItemTariffCustomList(string filterExpression)
        {
            List<vItemTariffCustom> result = new List<vItemTariffCustom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemTariffCustom));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemTariffCustom)helper.IDataReaderToObject(reader, new vItemTariffCustom()));
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
        #region vJournalTemplateDt
        public static List<vJournalTemplateDt> GetvJournalTemplateDtList(string filterExpression)
        {
            List<vJournalTemplateDt> result = new List<vJournalTemplateDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vJournalTemplateDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vJournalTemplateDt)helper.IDataReaderToObject(reader, new vJournalTemplateDt()));
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
        #region vJournalTemplateHd
        public static List<vJournalTemplateHd> GetvJournalTemplateHdList(string filterExpression)
        {
            List<vJournalTemplateHd> result = new List<vJournalTemplateHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vJournalTemplateHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vJournalTemplateHd)helper.IDataReaderToObject(reader, new vJournalTemplateHd()));
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
        public static List<vJournalTemplateHd> GetvJournalTemplateHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vJournalTemplateHd> result = new List<vJournalTemplateHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vJournalTemplateHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vJournalTemplateHd)helper.IDataReaderToObject(reader, new vJournalTemplateHd()));
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
        public static Int32 GetvJournalTemplateHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vJournalTemplateHd));
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
        public static Int32 GetvJournalTemplateHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vJournalTemplateHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "TemplateID", keyValue, orderByExpression);
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
        public static Int32 GetvJournalTemplateHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(vJournalTemplateHd));
                ctx.CommandText = helper.SelectMaxColumn("TemplateID");
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
        #region vLocation
        public static List<vLocation> GetvLocationList(string filterExpression)
        {
            List<vLocation> result = new List<vLocation>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vLocation));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vLocation)helper.IDataReaderToObject(reader, new vLocation()));
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
        public static List<vLocation> GetvLocationList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vLocation> result = new List<vLocation>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vLocation));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vLocation)helper.IDataReaderToObject(reader, new vLocation()));
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

        public static Int32 GetvLocationRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vLocation));
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
        public static Int32 GetvLocationRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vLocation));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "LocationID", keyValue, orderByExpression);
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
        #region vOrganizationDt
        public static List<vOrganizationDt> GetvOrganizationDtList(string filterExpression)
        {
            List<vOrganizationDt> result = new List<vOrganizationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vOrganizationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vOrganizationDt)helper.IDataReaderToObject(reader, new vOrganizationDt()));
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
        #region vOrganizationDtStudent
        public static List<vOrganizationDtStudent> GetvOrganizationDtStudentList(string filterExpression)
        {
            List<vOrganizationDtStudent> result = new List<vOrganizationDtStudent>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vOrganizationDtStudent));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vOrganizationDtStudent)helper.IDataReaderToObject(reader, new vOrganizationDtStudent()));
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
        public static Int32 GetvPeriodAdmissionRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPeriodAdmission));
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
        public static List<vPeriodAdmission> GetvPeriodAdmissionList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vPeriodAdmission> result = new List<vPeriodAdmission>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPeriodAdmission));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
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
        public static Int32 GetvPeriodAdmissionRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPeriodAdmission));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "PeriodAdmissionID", keyValue, orderByExpression);
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
        #region vProductBrand
        public static List<vProductBrand> GetvProductBrandList(string filterExpression)
        {
            List<vProductBrand> result = new List<vProductBrand>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProductBrand));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProductBrand)helper.IDataReaderToObject(reader, new vProductBrand()));
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
        #region vProductLine
        public static List<vProductLine> GetvProductLineList(string filterExpression)
        {
            List<vProductLine> result = new List<vProductLine>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProductLine));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProductLine)helper.IDataReaderToObject(reader, new vProductLine()));
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
        public static List<vProductLine> GetvProductLineList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vProductLine> result = new List<vProductLine>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProductLine));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProductLine)helper.IDataReaderToObject(reader, new vProductLine()));
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
        public static Int32 GetvProductLineRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProductLine));
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
        public static Int32 GetvProductLineRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProductLine));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ProductLineID", keyValue, orderByExpression);
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
        #region vProductLineDt
        public static List<vProductLineDt> GetvProductLineDtList(string filterExpression)
        {
            List<vProductLineDt> result = new List<vProductLineDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProductLineDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProductLineDt)helper.IDataReaderToObject(reader, new vProductLineDt()));
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
        #region vProspectiveStudent
        public static List<vProspectiveStudent> GetvProspectiveStudentList(string filterExpression)
        {
            List<vProspectiveStudent> result = new List<vProspectiveStudent>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProspectiveStudent));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProspectiveStudent)helper.IDataReaderToObject(reader, new vProspectiveStudent()));
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
        public static List<vProspectiveStudent> GetvProspectiveStudentList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vProspectiveStudent> result = new List<vProspectiveStudent>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProspectiveStudent));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProspectiveStudent)helper.IDataReaderToObject(reader, new vProspectiveStudent()));
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
        public static Int32 GetvProspectiveStudentRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProspectiveStudent));
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
        public static Int32 GetvProspectiveStudentRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProspectiveStudent));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ProspectiveStudentID", keyValue, orderByExpression);
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
        #region vProspectiveStudentAchievement
        public static List<vProspectiveStudentAchievement> GetvProspectiveStudentAchievementList(string filterExpression)
        {
            List<vProspectiveStudentAchievement> result = new List<vProspectiveStudentAchievement>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProspectiveStudentAchievement));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProspectiveStudentAchievement)helper.IDataReaderToObject(reader, new vProspectiveStudentAchievement()));
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
        #region vProspectiveStudentFamily
        public static List<vProspectiveStudentFamily> GetvProspectiveStudentFamilyList(string filterExpression)
        {
            List<vProspectiveStudentFamily> result = new List<vProspectiveStudentFamily>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProspectiveStudentFamily));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProspectiveStudentFamily)helper.IDataReaderToObject(reader, new vProspectiveStudentFamily()));
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
        public static List<vProspectiveStudentFamily> GetvProspectiveStudentFamilyList(string filterExpression, IDbContext ctx)
        {
            List<vProspectiveStudentFamily> result = new List<vProspectiveStudentFamily>();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProspectiveStudentFamily));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProspectiveStudentFamily)helper.IDataReaderToObject(reader, new vProspectiveStudentFamily()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region vProspectiveStudentFolder
        public static List<vProspectiveStudentFolder> GetvProspectiveStudentFolderList(string filterExpression)
        {
            List<vProspectiveStudentFolder> result = new List<vProspectiveStudentFolder>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProspectiveStudentFolder));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProspectiveStudentFolder)helper.IDataReaderToObject(reader, new vProspectiveStudentFolder()));
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
        #region vProspectiveStudentForm
        public static List<vProspectiveStudentForm> GetvProspectiveStudentFormList(string filterExpression)
        {
            List<vProspectiveStudentForm> result = new List<vProspectiveStudentForm>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProspectiveStudentForm));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProspectiveStudentForm)helper.IDataReaderToObject(reader, new vProspectiveStudentForm()));
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
        public static List<vProspectiveStudentForm> GetvProspectiveStudentFormList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vProspectiveStudentForm> result = new List<vProspectiveStudentForm>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProspectiveStudentForm));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProspectiveStudentForm)helper.IDataReaderToObject(reader, new vProspectiveStudentForm()));
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
        public static Int32 GetvProspectiveStudentFormRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProspectiveStudentForm));
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
        public static Int32 GetvProspectiveStudentFormRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProspectiveStudentForm));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "FormID", keyValue, orderByExpression);
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
        #region vProspectiveStudentPastStudy
        public static List<vProspectiveStudentPastStudy> GetvProspectiveStudentPastStudyList(string filterExpression)
        {
            List<vProspectiveStudentPastStudy> result = new List<vProspectiveStudentPastStudy>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProspectiveStudentPastStudy));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProspectiveStudentPastStudy)helper.IDataReaderToObject(reader, new vProspectiveStudentPastStudy()));
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
        #region vPurchaseInvoiceDt
        public static List<vPurchaseInvoiceDt> GetvPurchaseInvoiceDtList(string filterExpression)
        {
            List<vPurchaseInvoiceDt> result = new List<vPurchaseInvoiceDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseInvoiceDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseInvoiceDt)helper.IDataReaderToObject(reader, new vPurchaseInvoiceDt()));
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
        public static List<vPurchaseInvoiceDt> GetvPurchaseInvoiceDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseInvoiceDt> result = new List<vPurchaseInvoiceDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseInvoiceDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseInvoiceDt)helper.IDataReaderToObject(reader, new vPurchaseInvoiceDt()));
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

        public static Int32 GetvPurchaseInvoiceDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseInvoiceDt));
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

        public static vPurchaseInvoiceDt GetvPurchaseInvoiceDt(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseInvoiceDt> result = new List<vPurchaseInvoiceDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseInvoiceDt));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseInvoiceDt)helper.IDataReaderToObject(reader, new vPurchaseInvoiceDt()));
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
        #region vPurchaseInvoiceHd
        public static List<vPurchaseInvoiceHd> GetvPurchaseInvoiceHdList(string filterExpression)
        {
            List<vPurchaseInvoiceHd> result = new List<vPurchaseInvoiceHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseInvoiceHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseInvoiceHd)helper.IDataReaderToObject(reader, new vPurchaseInvoiceHd()));
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
        public static List<vPurchaseInvoiceHd> GetvPurchaseInvoiceHdList(string filterExpression, IDbContext ctx)
        {
            List<vPurchaseInvoiceHd> result = new List<vPurchaseInvoiceHd>();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseInvoiceHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseInvoiceHd)helper.IDataReaderToObject(reader, new vPurchaseInvoiceHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<vPurchaseInvoiceHd> GetvPurchaseInvoiceHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseInvoiceHd> result = new List<vPurchaseInvoiceHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseInvoiceHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseInvoiceHd)helper.IDataReaderToObject(reader, new vPurchaseInvoiceHd()));
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

        public static Int32 GetvPurchaseInvoiceHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseInvoiceHd));
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

        public static Int32 GetvPurchaseInvoiceHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseInvoiceHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "PurchaseInvoiceNo", keyValue, orderByExpression);
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

        public static vPurchaseInvoiceHd GetvPurchaseInvoiceHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseInvoiceHd> result = new List<vPurchaseInvoiceHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseInvoiceHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseInvoiceHd)helper.IDataReaderToObject(reader, new vPurchaseInvoiceHd()));
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
        #region vPurchaseInvoiceHdPayment
        public static List<vPurchaseInvoiceHdPayment> GetvPurchaseInvoiceHdPaymentList(string filterExpression)
        {
            List<vPurchaseInvoiceHdPayment> result = new List<vPurchaseInvoiceHdPayment>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseInvoiceHdPayment));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseInvoiceHdPayment)helper.IDataReaderToObject(reader, new vPurchaseInvoiceHdPayment()));
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
        public static List<vPurchaseInvoiceHdPayment> GetvPurchaseInvoiceHdPaymentList(string filterExpression, IDbContext ctx)
        {
            List<vPurchaseInvoiceHdPayment> result = new List<vPurchaseInvoiceHdPayment>();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseInvoiceHdPayment));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseInvoiceHdPayment)helper.IDataReaderToObject(reader, new vPurchaseInvoiceHdPayment()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<vPurchaseInvoiceHdPayment> GetvPurchaseInvoiceHdPaymentList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseInvoiceHdPayment> result = new List<vPurchaseInvoiceHdPayment>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseInvoiceHdPayment));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseInvoiceHdPayment)helper.IDataReaderToObject(reader, new vPurchaseInvoiceHdPayment()));
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

        public static Int32 GetvPurchaseInvoiceHdPaymentRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseInvoiceHdPayment));
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

        public static Int32 GetvPurchaseInvoiceHdPaymentRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseInvoiceHdPayment));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "PurchaseInvoiceNo", keyValue, orderByExpression);
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

        public static vPurchaseInvoiceHdPayment GetvPurchaseInvoiceHdPayment(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseInvoiceHdPayment> result = new List<vPurchaseInvoiceHdPayment>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseInvoiceHdPayment));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseInvoiceHdPayment)helper.IDataReaderToObject(reader, new vPurchaseInvoiceHdPayment()));
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
        #region vPurchaseReceiveCredit
        public static List<vPurchaseReceiveCredit> GetvPurchaseReceiveCreditList(string filterExpression)
        {
            List<vPurchaseReceiveCredit> result = new List<vPurchaseReceiveCredit>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReceiveCredit));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReceiveCredit)helper.IDataReaderToObject(reader, new vPurchaseReceiveCredit()));
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

        public static List<vPurchaseReceiveCredit> GetvPurchaseReceiveCreditList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseReceiveCredit> result = new List<vPurchaseReceiveCredit>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReceiveCredit));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReceiveCredit)helper.IDataReaderToObject(reader, new vPurchaseReceiveCredit()));
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

        public static Int32 GetvPurchaseReceiveCreditRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReceiveCredit));
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

        public static vPurchaseReceiveCredit GetvPurchaseReceiveCredit(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseReceiveCredit> result = new List<vPurchaseReceiveCredit>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReceiveCredit));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReceiveCredit)helper.IDataReaderToObject(reader, new vPurchaseReceiveCredit()));
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
        #region vPurchaseReceiveDtFixedAsset
        public static List<vPurchaseReceiveDtFixedAsset> GetvPurchaseReceiveDtFixedAssetList(string filterExpression)
        {
            List<vPurchaseReceiveDtFixedAsset> result = new List<vPurchaseReceiveDtFixedAsset>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReceiveDtFixedAsset));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReceiveDtFixedAsset)helper.IDataReaderToObject(reader, new vPurchaseReceiveDtFixedAsset()));
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
        public static List<vPurchaseReceiveDtFixedAsset> GetvPurchaseReceiveDtFixedAssetList(string filterExpression, IDbContext ctx)
        {
            List<vPurchaseReceiveDtFixedAsset> result = new List<vPurchaseReceiveDtFixedAsset>();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReceiveDtFixedAsset));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReceiveDtFixedAsset)helper.IDataReaderToObject(reader, new vPurchaseReceiveDtFixedAsset()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetvPurchaseReceiveDtFixedAssetRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReceiveDtFixedAsset));
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
        public static List<vPurchaseReceiveDtFixedAsset> GetvPurchaseReceiveDtFixedAssetList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseReceiveDtFixedAsset> result = new List<vPurchaseReceiveDtFixedAsset>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReceiveDtFixedAsset));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReceiveDtFixedAsset)helper.IDataReaderToObject(reader, new vPurchaseReceiveDtFixedAsset()));
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
        #region vPurchaseReplacementDt
        public static List<vPurchaseReplacementDt> GetvPurchaseReplacementDtList(string filterExpression)
        {
            List<vPurchaseReplacementDt> result = new List<vPurchaseReplacementDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReplacementDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReplacementDt)helper.IDataReaderToObject(reader, new vPurchaseReplacementDt()));
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
        public static Int32 GetvPurchaseReplacementDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReplacementDt));
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
        public static List<vPurchaseReplacementDt> GetvPurchaseReplacementDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseReplacementDt> result = new List<vPurchaseReplacementDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReplacementDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReplacementDt)helper.IDataReaderToObject(reader, new vPurchaseReplacementDt()));
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
        #region vPurchaseReplacementHd
        public static List<vPurchaseReplacementHd> GetvPurchaseReplacementHdList(string filterExpression)
        {
            List<vPurchaseReplacementHd> result = new List<vPurchaseReplacementHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReplacementHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReplacementHd)helper.IDataReaderToObject(reader, new vPurchaseReplacementHd()));
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

        public static List<vPurchaseReplacementHd> GetvPurchaseReplacementHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseReplacementHd> result = new List<vPurchaseReplacementHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReplacementHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReplacementHd)helper.IDataReaderToObject(reader, new vPurchaseReplacementHd()));
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

        public static Int32 GetvPurchaseReplacementHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReplacementHd));
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

        public static Int32 GetvPurchaseReplacementHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReplacementHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "PurchaseReplacementNo", keyValue, orderByExpression);
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

        public static vPurchaseReplacementHd GetvPurchaseReplacementHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseReplacementHd> result = new List<vPurchaseReplacementHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReplacementHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReplacementHd)helper.IDataReaderToObject(reader, new vPurchaseReplacementHd()));
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
        #region vPurchaseReturnDt
        public static List<vPurchaseReturnDt> GetvPurchaseReturnDtList(string filterExpression)
        {
            List<vPurchaseReturnDt> result = new List<vPurchaseReturnDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReturnDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReturnDt)helper.IDataReaderToObject(reader, new vPurchaseReturnDt()));
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
        public static Int32 GetvPurchaseReturnDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReturnDt));
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
        public static List<vPurchaseReturnDt> GetvPurchaseReturnDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseReturnDt> result = new List<vPurchaseReturnDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReturnDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReturnDt)helper.IDataReaderToObject(reader, new vPurchaseReturnDt()));
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
        #region vPurchaseReturnHd
        public static List<vPurchaseReturnHd> GetvPurchaseReturnHdList(string filterExpression)
        {
            List<vPurchaseReturnHd> result = new List<vPurchaseReturnHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReturnHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReturnHd)helper.IDataReaderToObject(reader, new vPurchaseReturnHd()));
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

        public static List<vPurchaseReturnHd> GetvPurchaseReturnHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseReturnHd> result = new List<vPurchaseReturnHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReturnHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReturnHd)helper.IDataReaderToObject(reader, new vPurchaseReturnHd()));
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

        public static Int32 GetvPurchaseReturnHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReturnHd));
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

        public static Int32 GetvPurchaseReturnHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReturnHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "PurchaseReturnNo", keyValue, orderByExpression);
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

        public static vPurchaseReturnHd GetvPurchaseReturnHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseReturnHd> result = new List<vPurchaseReturnHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReturnHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReturnHd)helper.IDataReaderToObject(reader, new vPurchaseReturnHd()));
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
        #region vRegistration
        public static List<vRegistration> GetvRegistrationList(string filterExpression)
        {
            List<vRegistration> result = new List<vRegistration>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRegistration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRegistration)helper.IDataReaderToObject(reader, new vRegistration()));
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
        public static List<vRegistration> GetvRegistrationList(string filterExpression, IDbContext ctx)
        {
            List<vRegistration> result = new List<vRegistration>();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRegistration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRegistration)helper.IDataReaderToObject(reader, new vRegistration()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetvRegistrationRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRegistration));
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
        public static List<vRegistration> GetvRegistrationList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vRegistration> result = new List<vRegistration>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRegistration));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRegistration)helper.IDataReaderToObject(reader, new vRegistration()));
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
        #region vRegistrationInvoice
        public static List<vRegistrationInvoice> GetvRegistrationInvoiceList(string filterExpression)
        {
            List<vRegistrationInvoice> result = new List<vRegistrationInvoice>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRegistrationInvoice));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRegistrationInvoice)helper.IDataReaderToObject(reader, new vRegistrationInvoice()));
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
        #region vRestrictionDt
        public static List<vRestrictionDt> GetvRestrictionDtList(string filterExpression)
        {
            List<vRestrictionDt> result = new List<vRestrictionDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRestrictionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRestrictionDt)helper.IDataReaderToObject(reader, new vRestrictionDt()));
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
        #region vSalesInvoiceDt
        public static List<vSalesInvoiceDt> GetvSalesInvoiceDtList(string filterExpression)
        {
            List<vSalesInvoiceDt> result = new List<vSalesInvoiceDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSalesInvoiceDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSalesInvoiceDt)helper.IDataReaderToObject(reader, new vSalesInvoiceDt()));
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

        public static Int32 GetvSalesInvoiceDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSalesInvoiceDt));
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
        public static List<vSalesInvoiceDt> GetvSalesInvoiceDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vSalesInvoiceDt> result = new List<vSalesInvoiceDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSalesInvoiceDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSalesInvoiceDt)helper.IDataReaderToObject(reader, new vSalesInvoiceDt()));
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
        #region vSalesInvoiceHd
        public static List<vSalesInvoiceHd> GetvSalesInvoiceHdList(string filterExpression)
        {
            List<vSalesInvoiceHd> result = new List<vSalesInvoiceHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSalesInvoiceHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSalesInvoiceHd)helper.IDataReaderToObject(reader, new vSalesInvoiceHd()));
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

        public static List<vSalesInvoiceHd> GetvSalesInvoiceHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vSalesInvoiceHd> result = new List<vSalesInvoiceHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSalesInvoiceHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSalesInvoiceHd)helper.IDataReaderToObject(reader, new vSalesInvoiceHd()));
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

        public static Int32 GetvSalesInvoiceHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSalesInvoiceHd));
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

        public static Int32 GetvSalesInvoiceHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSalesInvoiceHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "SalesInvoiceNo", keyValue, orderByExpression);
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

        public static vSalesInvoiceHd GetvSalesInvoiceHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vSalesInvoiceHd> result = new List<vSalesInvoiceHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSalesInvoiceHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSalesInvoiceHd)helper.IDataReaderToObject(reader, new vSalesInvoiceHd()));
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
        #region vScholarship
        public static List<vScholarship> GetvScholarshipList(string filterExpression)
        {
            List<vScholarship> result = new List<vScholarship>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vScholarship));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vScholarship)helper.IDataReaderToObject(reader, new vScholarship()));
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
        #region vSchoolGrade
        public static List<vSchoolGrade> GetvSchoolGradeList(string filterExpression)
        {
            List<vSchoolGrade> result = new List<vSchoolGrade>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSchoolGrade));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSchoolGrade)helper.IDataReaderToObject(reader, new vSchoolGrade()));
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
        public static List<vSchoolGrade> GetvSchoolGradeList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vSchoolGrade> result = new List<vSchoolGrade>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSchoolGrade));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSchoolGrade)helper.IDataReaderToObject(reader, new vSchoolGrade()));
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
        public static Int32 GetvSchoolGradeRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSchoolGrade));
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
        public static Int32 GetvSchoolGradeRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSchoolGrade));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "GCGrade", keyValue, orderByExpression);
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
        #region vSchoolMajor
        public static List<vSchoolMajor> GetvSchoolMajorList(string filterExpression)
        {
            List<vSchoolMajor> result = new List<vSchoolMajor>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSchoolMajor));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSchoolMajor)helper.IDataReaderToObject(reader, new vSchoolMajor()));
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
        public static List<vSchoolMajor> GetvSchoolMajorList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vSchoolMajor> result = new List<vSchoolMajor>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSchoolMajor));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSchoolMajor)helper.IDataReaderToObject(reader, new vSchoolMajor()));
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
        public static Int32 GetvSchoolMajorRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSchoolMajor));
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
        public static Int32 GetvSchoolMajorRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSchoolMajor));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "GCMajor", keyValue, orderByExpression);
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
        #region vSchoolPeriod
        public static List<vSchoolPeriod> GetvSchoolPeriodList(string filterExpression)
        {
            List<vSchoolPeriod> result = new List<vSchoolPeriod>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSchoolPeriod));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSchoolPeriod)helper.IDataReaderToObject(reader, new vSchoolPeriod()));
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
        #region vSiteItem
        public static List<vSiteItem> GetvSiteItemList(string filterExpression)
        {
            List<vSiteItem> result = new List<vSiteItem>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSiteItem));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSiteItem)helper.IDataReaderToObject(reader, new vSiteItem()));
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
        public static List<vSiteItem> GetvSiteItemList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vSiteItem> result = new List<vSiteItem>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSiteItem));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSiteItem)helper.IDataReaderToObject(reader, new vSiteItem()));
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
        public static Int32 GetvSiteItemRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSiteItem));
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
        #region vStockTakingDt
        public static List<vStockTakingDt> GetvStockTakingDtList(string filterExpression)
        {
            List<vStockTakingDt> result = new List<vStockTakingDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStockTakingDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStockTakingDt)helper.IDataReaderToObject(reader, new vStockTakingDt()));
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
        public static List<vStockTakingDt> GetvStockTakingDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vStockTakingDt> result = new List<vStockTakingDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStockTakingDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStockTakingDt)helper.IDataReaderToObject(reader, new vStockTakingDt()));
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
        public static Int32 GetvStockTakingDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStockTakingDt));
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
        #region vStockTakingHd
        public static List<vStockTakingHd> GetvStockTakingHdList(string filterExpression)
        {
            List<vStockTakingHd> result = new List<vStockTakingHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStockTakingHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStockTakingHd)helper.IDataReaderToObject(reader, new vStockTakingHd()));
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

        public static List<vStockTakingHd> GetvStockTakingHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vStockTakingHd> result = new List<vStockTakingHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStockTakingHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStockTakingHd)helper.IDataReaderToObject(reader, new vStockTakingHd()));
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

        public static Int32 GetvStockTakingHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStockTakingHd));
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

        public static Int32 GetvStockTakingHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStockTakingHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "StockTakingNo", keyValue, orderByExpression);
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

        public static vStockTakingHd GetvStockTakingHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vStockTakingHd> result = new List<vStockTakingHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStockTakingHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStockTakingHd)helper.IDataReaderToObject(reader, new vStockTakingHd()));
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
                ctx.CommandText = helper.GetRowIndex(filterExpression, "StudentID", keyValue, orderByExpression);
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
        #region vStudentAchievement
        public static List<vStudentAchievement> GetvStudentAchievementList(string filterExpression)
        {
            List<vStudentAchievement> result = new List<vStudentAchievement>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentAchievement));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentAchievement)helper.IDataReaderToObject(reader, new vStudentAchievement()));
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
        #region vStudentCoverageTransactionDtCustom
        public static List<vStudentCoverageTransactionDtCustom> GetvStudentCoverageTransactionDtCustomList(string filterExpression)
        {
            List<vStudentCoverageTransactionDtCustom> result = new List<vStudentCoverageTransactionDtCustom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentCoverageTransactionDtCustom));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentCoverageTransactionDtCustom)helper.IDataReaderToObject(reader, new vStudentCoverageTransactionDtCustom()));
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
        public static List<vStudentCoverageTransactionDtCustom> GetvStudentCoverageTransactionDtCustomList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vStudentCoverageTransactionDtCustom> result = new List<vStudentCoverageTransactionDtCustom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentCoverageTransactionDtCustom));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentCoverageTransactionDtCustom)helper.IDataReaderToObject(reader, new vStudentCoverageTransactionDtCustom()));
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
        public static Int32 GetvStudentCoverageTransactionDtCustomRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentCoverageTransactionDtCustom));
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
        #region vStudentCoverageTransactionHd
        public static List<vStudentCoverageTransactionHd> GetvStudentCoverageTransactionHdList(string filterExpression)
        {
            List<vStudentCoverageTransactionHd> result = new List<vStudentCoverageTransactionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentCoverageTransactionHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentCoverageTransactionHd)helper.IDataReaderToObject(reader, new vStudentCoverageTransactionHd()));
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
        public static List<vStudentCoverageTransactionHd> GetvStudentCoverageTransactionHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vStudentCoverageTransactionHd> result = new List<vStudentCoverageTransactionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentCoverageTransactionHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentCoverageTransactionHd)helper.IDataReaderToObject(reader, new vStudentCoverageTransactionHd()));
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
        public static Int32 GetvStudentCoverageTransactionHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentCoverageTransactionHd));
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
        public static Int32 GetvStudentCoverageTransactionHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentCoverageTransactionHd));
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

        public static vStudentCoverageTransactionHd GetvStudentCoverageTransactionHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vStudentCoverageTransactionHd> result = new List<vStudentCoverageTransactionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentCoverageTransactionHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentCoverageTransactionHd)helper.IDataReaderToObject(reader, new vStudentCoverageTransactionHd()));
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
        #region vStudentCustom
        public static List<vStudentCustom> GetvStudentCustomList(string filterExpression)
        {
            List<vStudentCustom> result = new List<vStudentCustom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentCustom));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentCustom)helper.IDataReaderToObject(reader, new vStudentCustom()));
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
        #region vStudentFamily
        public static List<vStudentFamily> GetvStudentFamilyList(string filterExpression)
        {
            List<vStudentFamily> result = new List<vStudentFamily>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentFamily));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentFamily)helper.IDataReaderToObject(reader, new vStudentFamily()));
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
        #region vStudentFee
        public static List<vStudentFee> GetvStudentFeeList(string filterExpression)
        {
            List<vStudentFee> result = new List<vStudentFee>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentFee));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentFee)helper.IDataReaderToObject(reader, new vStudentFee()));
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
        #region vStudentFeeComp
        public static List<vStudentFeeComp> GetvStudentFeeCompList(string filterExpression)
        {
            List<vStudentFeeComp> result = new List<vStudentFeeComp>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentFeeComp));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentFeeComp)helper.IDataReaderToObject(reader, new vStudentFeeComp()));
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
        public static List<vStudentFeeComp> GetvStudentFeeCompList(string filterExpression, IDbContext ctx)
        {
            List<vStudentFeeComp> result = new List<vStudentFeeComp>();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentFeeComp));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentFeeComp)helper.IDataReaderToObject(reader, new vStudentFeeComp()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region vStudentFeeCompType
        public static List<vStudentFeeCompType> GetvStudentFeeCompTypeList(string filterExpression)
        {
            List<vStudentFeeCompType> result = new List<vStudentFeeCompType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentFeeCompType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentFeeCompType)helper.IDataReaderToObject(reader, new vStudentFeeCompType()));
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
        public static List<vStudentFeeCompType> GetvStudentFeeCompTypeList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vStudentFeeCompType> result = new List<vStudentFeeCompType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentFeeCompType));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentFeeCompType)helper.IDataReaderToObject(reader, new vStudentFeeCompType()));
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
        public static Int32 GetvStudentFeeCompTypeRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentFeeCompType));
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
        public static Int32 GetvStudentFeeCompTypeRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentFeeCompType));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "StudentFeeCompTypeID", keyValue, orderByExpression);
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
        #region vStudentFeeDt
        public static List<vStudentFeeDt> GetvStudentFeeDtList(string filterExpression)
        {
            List<vStudentFeeDt> result = new List<vStudentFeeDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentFeeDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentFeeDt)helper.IDataReaderToObject(reader, new vStudentFeeDt()));
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
        public static List<vStudentFeeDt> GetvStudentFeeDtList(string filterExpression, IDbContext ctx)
        {
            List<vStudentFeeDt> result = new List<vStudentFeeDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentFeeDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentFeeDt)helper.IDataReaderToObject(reader, new vStudentFeeDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<vStudentFeeDt> GetvStudentFeeDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vStudentFeeDt> result = new List<vStudentFeeDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentFeeDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentFeeDt)helper.IDataReaderToObject(reader, new vStudentFeeDt()));
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
        public static Int32 GetvStudentFeeDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentFeeDt));
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
        #region vStudentFinalMarkFormulaDt
        public static List<vStudentFinalMarkFormulaDt> GetvStudentFinalMarkFormulaDtList(string filterExpression)
        {
            List<vStudentFinalMarkFormulaDt> result = new List<vStudentFinalMarkFormulaDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentFinalMarkFormulaDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentFinalMarkFormulaDt)helper.IDataReaderToObject(reader, new vStudentFinalMarkFormulaDt()));
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
        #region vStudentPastStudy
        public static List<vStudentPastStudy> GetvStudentPastStudyList(string filterExpression)
        {
            List<vStudentPastStudy> result = new List<vStudentPastStudy>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentPastStudy));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentPastStudy)helper.IDataReaderToObject(reader, new vStudentPastStudy()));
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
        #region vSubjectBasicCompetency
        public static List<vSubjectBasicCompetency> GetvSubjectBasicCompetencyList(string filterExpression)
        {
            List<vSubjectBasicCompetency> result = new List<vSubjectBasicCompetency>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSubjectBasicCompetency));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSubjectBasicCompetency)helper.IDataReaderToObject(reader, new vSubjectBasicCompetency()));
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
        #region vSubjectClassType
        public static List<vSubjectClassType> GetvSubjectClassTypeList(string filterExpression)
        {
            List<vSubjectClassType> result = new List<vSubjectClassType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSubjectClassType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSubjectClassType)helper.IDataReaderToObject(reader, new vSubjectClassType()));
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
        #region vSubjectIndicator
        public static List<vSubjectIndicator> GetvSubjectIndicatorList(string filterExpression)
        {
            List<vSubjectIndicator> result = new List<vSubjectIndicator>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSubjectIndicator));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSubjectIndicator)helper.IDataReaderToObject(reader, new vSubjectIndicator()));
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
        public static List<vSubjectIndicator> GetvSubjectIndicatorList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vSubjectIndicator> result = new List<vSubjectIndicator>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSubjectIndicator));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSubjectIndicator)helper.IDataReaderToObject(reader, new vSubjectIndicator()));
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
        public static Int32 GetvSubjectIndicatorRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSubjectIndicator));
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
        #region vSubjectMatterHd
        public static List<vSubjectMatterHd> GetvSubjectMatterHdList(string filterExpression)
        {
            List<vSubjectMatterHd> result = new List<vSubjectMatterHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSubjectMatterHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSubjectMatterHd)helper.IDataReaderToObject(reader, new vSubjectMatterHd()));
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
        #region vSubjectMeetingPlanHd
        public static List<vSubjectMeetingPlanHd> GetvSubjectMeetingPlanHdList(string filterExpression)
        {
            List<vSubjectMeetingPlanHd> result = new List<vSubjectMeetingPlanHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSubjectMeetingPlanHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSubjectMeetingPlanHd)helper.IDataReaderToObject(reader, new vSubjectMeetingPlanHd()));
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
        #region vSubjectMeetingPlanIndicator
        public static List<vSubjectMeetingPlanIndicator> GetvSubjectMeetingPlanIndicatorList(string filterExpression)
        {
            List<vSubjectMeetingPlanIndicator> result = new List<vSubjectMeetingPlanIndicator>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSubjectMeetingPlanIndicator));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSubjectMeetingPlanIndicator)helper.IDataReaderToObject(reader, new vSubjectMeetingPlanIndicator()));
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
        #region vSubLedgerHd
        public static List<vSubLedgerHd> GetvSubLedgerHdList(string filterExpression)
        {
            List<vSubLedgerHd> result = new List<vSubLedgerHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSubLedgerHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSubLedgerHd)helper.IDataReaderToObject(reader, new vSubLedgerHd()));
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
        public static List<vSubLedgerHd> GetvSubLedgerHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vSubLedgerHd> result = new List<vSubLedgerHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSubLedgerHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSubLedgerHd)helper.IDataReaderToObject(reader, new vSubLedgerHd()));
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
        public static Int32 GetvSubLedgerHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSubLedgerHd));
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
        public static Int32 GetvSubLedgerHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSubLedgerHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "SubLedgerID", keyValue, orderByExpression);
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
        #region vSupplier
        public static List<vSupplier> GetvSupplierList(string filterExpression)
        {
            List<vSupplier> result = new List<vSupplier>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSupplier));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSupplier)helper.IDataReaderToObject(reader, new vSupplier()));
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
        public static List<vSupplier> GetvSupplierList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vSupplier> result = new List<vSupplier>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSupplier));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSupplier)helper.IDataReaderToObject(reader, new vSupplier()));
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
        public static Int32 GetvSupplierRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSupplier));
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
        public static Int32 GetvSupplierRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSupplier));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "BusinessPartnerID", keyValue, orderByExpression);
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
        #region vSupplierCreditNote
        public static List<vSupplierCreditNote> GetvSupplierCreditNoteList(string filterExpression)
        {
            List<vSupplierCreditNote> result = new List<vSupplierCreditNote>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSupplierCreditNote));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSupplierCreditNote)helper.IDataReaderToObject(reader, new vSupplierCreditNote()));
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

        public static List<vSupplierCreditNote> GetvSupplierCreditNoteList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vSupplierCreditNote> result = new List<vSupplierCreditNote>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSupplierCreditNote));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSupplierCreditNote)helper.IDataReaderToObject(reader, new vSupplierCreditNote()));
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

        public static Int32 GetvSupplierCreditNoteRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSupplierCreditNote));
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

        public static Int32 GetvSupplierCreditNoteRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSupplierCreditNote));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "CreditNoteNo", keyValue, orderByExpression);
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

        public static vSupplierCreditNote GetvSupplierCreditNote(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vSupplierCreditNote> result = new List<vSupplierCreditNote>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSupplierCreditNote));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSupplierCreditNote)helper.IDataReaderToObject(reader, new vSupplierCreditNote()));
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
        #region vSupplierLineDt
        public static List<vSupplierLineDt> GetvSupplierLineDtList(string filterExpression)
        {
            List<vSupplierLineDt> result = new List<vSupplierLineDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSupplierLineDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSupplierLineDt)helper.IDataReaderToObject(reader, new vSupplierLineDt()));
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
        #region vSupplierPaymentHd
        public static List<vSupplierPaymentHd> GetvSupplierPaymentHdList(string filterExpression)
        {
            List<vSupplierPaymentHd> result = new List<vSupplierPaymentHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSupplierPaymentHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSupplierPaymentHd)helper.IDataReaderToObject(reader, new vSupplierPaymentHd()));
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
        public static List<vSupplierPaymentHd> GetvSupplierPaymentHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vSupplierPaymentHd> result = new List<vSupplierPaymentHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSupplierPaymentHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSupplierPaymentHd)helper.IDataReaderToObject(reader, new vSupplierPaymentHd()));
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

        public static Int32 GetvSupplierPaymentHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSupplierPaymentHd));
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

        public static Int32 GetvSupplierPaymentHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSupplierPaymentHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "SupplierPaymentNo", keyValue, orderByExpression);
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

        public static vSupplierPaymentHd GetvSupplierPaymentHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vSupplierPaymentHd> result = new List<vSupplierPaymentHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSupplierPaymentHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSupplierPaymentHd)helper.IDataReaderToObject(reader, new vSupplierPaymentHd()));
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
        #region vSyncItemTransactionDt
        public static List<vSyncItemTransactionDt> GetvSyncItemTransactionDtList(string filterExpression)
        {
            List<vSyncItemTransactionDt> result = new List<vSyncItemTransactionDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSyncItemTransactionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSyncItemTransactionDt)helper.IDataReaderToObject(reader, new vSyncItemTransactionDt()));
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
        public static List<vSyncItemTransactionDt> GetvSyncItemTransactionDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vSyncItemTransactionDt> result = new List<vSyncItemTransactionDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSyncItemTransactionDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSyncItemTransactionDt)helper.IDataReaderToObject(reader, new vSyncItemTransactionDt()));
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
        #region vSyncItemTransactionHd
        public static List<vSyncItemTransactionHd> GetvSyncItemTransactionHdList(string filterExpression)
        {
            List<vSyncItemTransactionHd> result = new List<vSyncItemTransactionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSyncItemTransactionHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSyncItemTransactionHd)helper.IDataReaderToObject(reader, new vSyncItemTransactionHd()));
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
        public static List<vSyncItemTransactionHd> GetvSyncItemTransactionHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vSyncItemTransactionHd> result = new List<vSyncItemTransactionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSyncItemTransactionHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSyncItemTransactionHd)helper.IDataReaderToObject(reader, new vSyncItemTransactionHd()));
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
        #region vTariffBookDt
        public static List<vTariffBookDt> GetvTariffBookDtList(string filterExpression)
        {
            List<vTariffBookDt> result = new List<vTariffBookDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTariffBookDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTariffBookDt)helper.IDataReaderToObject(reader, new vTariffBookDt()));
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
        #region vTariffBookHd
        public static List<vTariffBookHd> GetvTariffBookHdList(string filterExpression)
        {
            List<vTariffBookHd> result = new List<vTariffBookHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTariffBookHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTariffBookHd)helper.IDataReaderToObject(reader, new vTariffBookHd()));
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
        public static Int32 GetvTariffBookHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTariffBookHd));
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
        public static List<vTariffBookHd> GetvTariffBookHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTariffBookHd> result = new List<vTariffBookHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTariffBookHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTariffBookHd)helper.IDataReaderToObject(reader, new vTariffBookHd()));
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
        public static Int32 GetvTariffBookHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTariffBookHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "BookID", keyValue, orderByExpression);
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
        public static vTariffBookHd GetvTariffBookHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vTariffBookHd> result = new List<vTariffBookHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTariffBookHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTariffBookHd)helper.IDataReaderToObject(reader, new vTariffBookHd()));
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
        public static List<vTeacher> GetvTeacherList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTeacher> result = new List<vTeacher>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacher));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
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

        public static Int32 GetvTeacherRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacher));
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
        public static Int32 GetvTeacherRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacher));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "TeacherID", keyValue, orderByExpression);
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
        #region vTeacherAbsence
        public static List<vTeacherAbsence> GetvTeacherAbsenceList(string filterExpression)
        {
            List<vTeacherAbsence> result = new List<vTeacherAbsence>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherAbsence));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTeacherAbsence)helper.IDataReaderToObject(reader, new vTeacherAbsence()));
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
        public static List<vTeacherAbsence> GetvTeacherAbsenceList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTeacherAbsence> result = new List<vTeacherAbsence>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherAbsence));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTeacherAbsence)helper.IDataReaderToObject(reader, new vTeacherAbsence()));
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

        public static Int32 GetvTeacherAbsenceRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherAbsence));
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
        public static Int32 GetvTeacherAbsenceRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherAbsence));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "TeacherMarkID", keyValue, orderByExpression);
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
        #region vTeacherClassSubject
        public static List<vTeacherClassSubject> GetvTeacherClassSubjectList(string filterExpression)
        {
            List<vTeacherClassSubject> result = new List<vTeacherClassSubject>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherClassSubject));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTeacherClassSubject)helper.IDataReaderToObject(reader, new vTeacherClassSubject()));
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
        #region vTeacherMark
        public static List<vTeacherMark> GetvTeacherMarkList(string filterExpression)
        {
            List<vTeacherMark> result = new List<vTeacherMark>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherMark));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTeacherMark)helper.IDataReaderToObject(reader, new vTeacherMark()));
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
        public static List<vTeacherMark> GetvTeacherMarkList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTeacherMark> result = new List<vTeacherMark>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherMark));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTeacherMark)helper.IDataReaderToObject(reader, new vTeacherMark()));
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

        public static Int32 GetvTeacherMarkRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherMark));
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
        public static Int32 GetvTeacherMarkRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherMark));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "TeacherMarkID", keyValue, orderByExpression);
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
        #region vTeacherMarkGroup
        public static List<vTeacherMarkGroup> GetvTeacherMarkGroupList(string filterExpression)
        {
            List<vTeacherMarkGroup> result = new List<vTeacherMarkGroup>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherMarkGroup));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTeacherMarkGroup)helper.IDataReaderToObject(reader, new vTeacherMarkGroup()));
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
        #region vTeacherMarkItem
        public static List<vTeacherMarkItem> GetvTeacherMarkItemList(string filterExpression)
        {
            List<vTeacherMarkItem> result = new List<vTeacherMarkItem>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherMarkItem));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTeacherMarkItem)helper.IDataReaderToObject(reader, new vTeacherMarkItem()));
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
        #region vTeacherMarkTypeGroup
        public static List<vTeacherMarkTypeGroup> GetvTeacherMarkTypeGroupList(string filterExpression)
        {
            List<vTeacherMarkTypeGroup> result = new List<vTeacherMarkTypeGroup>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherMarkTypeGroup));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTeacherMarkTypeGroup)helper.IDataReaderToObject(reader, new vTeacherMarkTypeGroup()));
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
        public static List<vTeacherMarkTypeGroup> GetvTeacherMarkTypeGroupList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTeacherMarkTypeGroup> result = new List<vTeacherMarkTypeGroup>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherMarkTypeGroup));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTeacherMarkTypeGroup)helper.IDataReaderToObject(reader, new vTeacherMarkTypeGroup()));
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

        public static Int32 GetvTeacherMarkTypeGroupRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherMarkTypeGroup));
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
        public static Int32 GetvTeacherMarkTypeGroupRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherMarkTypeGroup));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "TeacherMarkTypeGroupID", keyValue, orderByExpression);
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
        #region vTeacherMarkTypeItem
        public static List<vTeacherMarkTypeItem> GetvTeacherMarkTypeItemList(string filterExpression)
        {
            List<vTeacherMarkTypeItem> result = new List<vTeacherMarkTypeItem>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherMarkTypeItem));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTeacherMarkTypeItem)helper.IDataReaderToObject(reader, new vTeacherMarkTypeItem()));
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
        public static List<vTeacherMarkTypeItem> GetvTeacherMarkTypeItemList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTeacherMarkTypeItem> result = new List<vTeacherMarkTypeItem>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherMarkTypeItem));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTeacherMarkTypeItem)helper.IDataReaderToObject(reader, new vTeacherMarkTypeItem()));
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

        public static Int32 GetvTeacherMarkTypeItemRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherMarkTypeItem));
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
        public static Int32 GetvTeacherMarkTypeItemRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherMarkTypeItem));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "TeacherMarkTypeItemID", keyValue, orderByExpression);
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
        #region vTeacherSchedule
        public static List<vTeacherSchedule> GetvTeacherScheduleList(string filterExpression)
        {
            List<vTeacherSchedule> result = new List<vTeacherSchedule>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherSchedule));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTeacherSchedule)helper.IDataReaderToObject(reader, new vTeacherSchedule()));
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
        #region vTeacherSubstitution
        public static List<vTeacherSubstitution> GetvTeacherSubstitutionList(string filterExpression)
        {
            List<vTeacherSubstitution> result = new List<vTeacherSubstitution>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherSubstitution));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTeacherSubstitution)helper.IDataReaderToObject(reader, new vTeacherSubstitution()));
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
