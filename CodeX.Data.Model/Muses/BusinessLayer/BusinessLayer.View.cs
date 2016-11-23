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
        #region vAbsenceProposalEmployee
        public static List<vAbsenceProposalEmployee> GetvAbsenceProposalEmployeeList(string filterExpression)
        {
            List<vAbsenceProposalEmployee> result = new List<vAbsenceProposalEmployee>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vAbsenceProposalEmployee));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vAbsenceProposalEmployee)helper.IDataReaderToObject(reader, new vAbsenceProposalEmployee()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvAbsenceProposalEmployeeRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vAbsenceProposalEmployee));
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
        public static List<vAbsenceProposalEmployee> GetvAbsenceProposalEmployeeList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vAbsenceProposalEmployee> result = new List<vAbsenceProposalEmployee>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vAbsenceProposalEmployee));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vAbsenceProposalEmployee)helper.IDataReaderToObject(reader, new vAbsenceProposalEmployee()));
            }
            catch (Exception ex)
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
        #region vAbsenceProposalHd
        public static List<vAbsenceProposalHd> GetvAbsenceProposalHdList(string filterExpression)
        {
            List<vAbsenceProposalHd> result = new List<vAbsenceProposalHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vAbsenceProposalHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vAbsenceProposalHd)helper.IDataReaderToObject(reader, new vAbsenceProposalHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvAbsenceProposalHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vAbsenceProposalHd));
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
        public static vAbsenceProposalHd GetvAbsenceProposalHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vAbsenceProposalHd> result = new List<vAbsenceProposalHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vAbsenceProposalHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vAbsenceProposalHd)helper.IDataReaderToObject(reader, new vAbsenceProposalHd()));
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
        public static Int32 GetvAbsenceProposalHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vAbsenceProposalHd));
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
        public static List<vAbsenceProposalHd> GetvAbsenceProposalHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vAbsenceProposalHd> result = new List<vAbsenceProposalHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vAbsenceProposalHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vAbsenceProposalHd)helper.IDataReaderToObject(reader, new vAbsenceProposalHd()));
            }
            catch (Exception ex)
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
        public static List<vARInvoiceHd> GetvARInvoiceHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vARInvoiceHd> result = new List<vARInvoiceHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vARInvoiceHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
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
        #region vARInvoiceReceiving
        public static List<vARInvoiceReceiving> GetvARInvoiceReceivingList(string filterExpression)
        {
            List<vARInvoiceReceiving> result = new List<vARInvoiceReceiving>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vARInvoiceReceiving));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vARInvoiceReceiving)helper.IDataReaderToObject(reader, new vARInvoiceReceiving()));
            }
            catch (Exception ex)
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
        public static List<vClassSubject> GetvClassSubjectList(string filterExpression, IDbContext ctx)
        {
            List<vClassSubject> result = new List<vClassSubject>();
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
        #region vClassStudentRegistration
        public static List<vClassStudentRegistration> GetvClassStudentRegistrationList(string filterExpression)
        {
            List<vClassStudentRegistration> result = new List<vClassStudentRegistration>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassStudentRegistration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vClassStudentRegistration)helper.IDataReaderToObject(reader, new vClassStudentRegistration()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvClassStudentRegistrationRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassStudentRegistration));
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
        #region vClassStudentSubjectMark
        public static List<vClassStudentSubjectMark> GetvClassStudentSubjectMarkList(string filterExpression)
        {
            List<vClassStudentSubjectMark> result = new List<vClassStudentSubjectMark>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vClassStudentSubjectMark));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vClassStudentSubjectMark)helper.IDataReaderToObject(reader, new vClassStudentSubjectMark()));
            }
            catch (Exception ex)
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
        #region vCurriculumClassType
        public static List<vCurriculumClassType> GetvCurriculumClassTypeList(string filterExpression)
        {
            List<vCurriculumClassType> result = new List<vCurriculumClassType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCurriculumClassType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vCurriculumClassType)helper.IDataReaderToObject(reader, new vCurriculumClassType()));
            }
            catch (Exception ex)
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
        #region vCurriculumClassTypeExtracurricular
        public static List<vCurriculumClassTypeExtracurricular> GetvCurriculumClassTypeExtracurricularList(string filterExpression)
        {
            List<vCurriculumClassTypeExtracurricular> result = new List<vCurriculumClassTypeExtracurricular>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCurriculumClassTypeExtracurricular));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vCurriculumClassTypeExtracurricular)helper.IDataReaderToObject(reader, new vCurriculumClassTypeExtracurricular()));
            }
            catch (Exception ex)
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
        #region vCurriculumFinalMarkFormulaDt
        public static List<vCurriculumFinalMarkFormulaDt> GetvCurriculumFinalMarkFormulaDtList(string filterExpression)
        {
            List<vCurriculumFinalMarkFormulaDt> result = new List<vCurriculumFinalMarkFormulaDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCurriculumFinalMarkFormulaDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vCurriculumFinalMarkFormulaDt)helper.IDataReaderToObject(reader, new vCurriculumFinalMarkFormulaDt()));
            }
            catch (Exception ex)
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
        #region vCurriculumMajor
        public static List<vCurriculumMajor> GetvCurriculumMajorList(string filterExpression)
        {
            List<vCurriculumMajor> result = new List<vCurriculumMajor>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCurriculumMajor));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vCurriculumMajor)helper.IDataReaderToObject(reader, new vCurriculumMajor()));
            }
            catch (Exception ex)
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
        #region vCurriculumMarkType
        public static List<vCurriculumMarkType> GetvCurriculumMarkTypeList(string filterExpression)
        {
            List<vCurriculumMarkType> result = new List<vCurriculumMarkType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCurriculumMarkType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vCurriculumMarkType)helper.IDataReaderToObject(reader, new vCurriculumMarkType()));
            }
            catch (Exception ex)
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
        #region vCurriculumMarkTypeClassStudyType
        public static List<vCurriculumMarkTypeClassStudyType> GetvCurriculumMarkTypeClassStudyTypeList(string filterExpression)
        {
            List<vCurriculumMarkTypeClassStudyType> result = new List<vCurriculumMarkTypeClassStudyType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCurriculumMarkTypeClassStudyType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vCurriculumMarkTypeClassStudyType)helper.IDataReaderToObject(reader, new vCurriculumMarkTypeClassStudyType()));
            }
            catch (Exception ex)
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
        #region vCurriculumMarkTypeDt
        public static List<vCurriculumMarkTypeDt> GetvCurriculumMarkTypeDtList(string filterExpression)
        {
            List<vCurriculumMarkTypeDt> result = new List<vCurriculumMarkTypeDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCurriculumMarkTypeDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vCurriculumMarkTypeDt)helper.IDataReaderToObject(reader, new vCurriculumMarkTypeDt()));
            }
            catch (Exception ex)
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
        #region vCurriculumMeetingPlan
        public static List<vCurriculumMeetingPlan> GetvCurriculumMeetingPlanList(string filterExpression)
        {
            List<vCurriculumMeetingPlan> result = new List<vCurriculumMeetingPlan>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCurriculumMeetingPlan));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vCurriculumMeetingPlan)helper.IDataReaderToObject(reader, new vCurriculumMeetingPlan()));
            }
            catch (Exception ex)
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
        #region vCurriculumReport
        public static List<vCurriculumReport> GetvCurriculumReportList(string filterExpression)
        {
            List<vCurriculumReport> result = new List<vCurriculumReport>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCurriculumReport));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vCurriculumReport)helper.IDataReaderToObject(reader, new vCurriculumReport()));
            }
            catch (Exception ex)
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
        #region vCurriculumSubject
        public static List<vCurriculumSubject> GetvCurriculumSubjectList(string filterExpression)
        {
            List<vCurriculumSubject> result = new List<vCurriculumSubject>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCurriculumSubject));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vCurriculumSubject)helper.IDataReaderToObject(reader, new vCurriculumSubject()));
            }
            catch (Exception ex)
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
        #region vCurriculumSubjectClassType
        public static List<vCurriculumSubjectClassType> GetvCurriculumSubjectClassTypeList(string filterExpression)
        {
            List<vCurriculumSubjectClassType> result = new List<vCurriculumSubjectClassType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCurriculumSubjectClassType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vCurriculumSubjectClassType)helper.IDataReaderToObject(reader, new vCurriculumSubjectClassType()));
            }
            catch (Exception ex)
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
        #region vCurriculumSubjectMarkType
        public static List<vCurriculumSubjectMarkType> GetvCurriculumSubjectMarkTypeList(string filterExpression)
        {
            List<vCurriculumSubjectMarkType> result = new List<vCurriculumSubjectMarkType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCurriculumSubjectMarkType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vCurriculumSubjectMarkType)helper.IDataReaderToObject(reader, new vCurriculumSubjectMarkType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vCurriculumSubjectMarkType> GetvCurriculumSubjectMarkTypeList(string filterExpression, IDbContext ctx)
        {
            List<vCurriculumSubjectMarkType> result = new List<vCurriculumSubjectMarkType>();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCurriculumSubjectMarkType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vCurriculumSubjectMarkType)helper.IDataReaderToObject(reader, new vCurriculumSubjectMarkType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region vCurriculumSyllabus
        public static List<vCurriculumSyllabus> GetvCurriculumSyllabusList(string filterExpression)
        {
            List<vCurriculumSyllabus> result = new List<vCurriculumSyllabus>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vCurriculumSyllabus));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vCurriculumSyllabus)helper.IDataReaderToObject(reader, new vCurriculumSyllabus()));
            }
            catch (Exception ex)
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
        #region vDirectPurchaseDtQtyOnOrderPerItemPerSiteServiceUnit
        public static List<vDirectPurchaseDtQtyOnOrderPerItemPerSiteServiceUnit> GetvDirectPurchaseDtQtyOnOrderPerItemPerSiteServiceUnitList(string filterExpression)
        {
            List<vDirectPurchaseDtQtyOnOrderPerItemPerSiteServiceUnit> result = new List<vDirectPurchaseDtQtyOnOrderPerItemPerSiteServiceUnit>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vDirectPurchaseDtQtyOnOrderPerItemPerSiteServiceUnit));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vDirectPurchaseDtQtyOnOrderPerItemPerSiteServiceUnit)helper.IDataReaderToObject(reader, new vDirectPurchaseDtQtyOnOrderPerItemPerSiteServiceUnit()));
            }
            catch (Exception ex)
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
        #region vEmployee
        public static List<vEmployee> GetvEmployeeList(string filterExpression)
        {
            List<vEmployee> result = new List<vEmployee>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vEmployee));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vEmployee)helper.IDataReaderToObject(reader, new vEmployee()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vEmployee> GetvEmployeeList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vEmployee> result = new List<vEmployee>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vEmployee));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vEmployee)helper.IDataReaderToObject(reader, new vEmployee()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        public static Int32 GetvEmployeeRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vEmployee));
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
        public static Int32 GetvEmployeeRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vEmployee));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "EmployeeID", keyValue, orderByExpression);
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
        #region vEmployeeRenumeration
        public static List<vEmployeeRenumeration> GetvEmployeeRenumerationList(string filterExpression)
        {
            List<vEmployeeRenumeration> result = new List<vEmployeeRenumeration>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vEmployeeRenumeration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vEmployeeRenumeration)helper.IDataReaderToObject(reader, new vEmployeeRenumeration()));
            }
            catch (Exception ex)
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
        #region vFADepreciation
        public static List<vFADepreciation> GetvFADepreciationList(string filterExpression)
        {
            List<vFADepreciation> result = new List<vFADepreciation>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vFADepreciation));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vFADepreciation)helper.IDataReaderToObject(reader, new vFADepreciation()));
            }
            catch (Exception ex)
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
        #region vGLAPOther
        public static List<vGLAPOther> GetvGLAPOtherList(string filterExpression)
        {
            List<vGLAPOther> result = new List<vGLAPOther>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLAPOther));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGLAPOther)helper.IDataReaderToObject(reader, new vGLAPOther()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vGLAPOther> GetvGLAPOtherList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vGLAPOther> result = new List<vGLAPOther>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLAPOther));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGLAPOther)helper.IDataReaderToObject(reader, new vGLAPOther()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvGLAPOtherRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLAPOther));
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
        public static Int32 GetvGLAPOtherRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLAPOther));
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
                ctx.CommandText = helper.GetRowIndex(filterExpression, "GLSettingCode", keyValue, orderByExpression);
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
        #region vGLTransactionHdPerTransactionCode
        public static List<vGLTransactionHdPerTransactionCode> GetvGLTransactionHdPerTransactionCodeList(string filterExpression)
        {
            List<vGLTransactionHdPerTransactionCode> result = new List<vGLTransactionHdPerTransactionCode>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGLTransactionHdPerTransactionCode));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGLTransactionHdPerTransactionCode)helper.IDataReaderToObject(reader, new vGLTransactionHdPerTransactionCode()));
            }
            catch (Exception ex)
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
        #region vGradePromotionFormulaDt
        public static List<vGradePromotionFormulaDt> GetvGradePromotionFormulaDtList(string filterExpression)
        {
            List<vGradePromotionFormulaDt> result = new List<vGradePromotionFormulaDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vGradePromotionFormulaDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vGradePromotionFormulaDt)helper.IDataReaderToObject(reader, new vGradePromotionFormulaDt()));
            }
            catch (Exception ex)
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
        #region vHRDailyScheduleDt
        public static List<vHRDailyScheduleDt> GetvHRDailyScheduleDtList(string filterExpression)
        {
            List<vHRDailyScheduleDt> result = new List<vHRDailyScheduleDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vHRDailyScheduleDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vHRDailyScheduleDt)helper.IDataReaderToObject(reader, new vHRDailyScheduleDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vHRDailyScheduleDt> GetvHRDailyScheduleDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vHRDailyScheduleDt> result = new List<vHRDailyScheduleDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vHRDailyScheduleDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vHRDailyScheduleDt)helper.IDataReaderToObject(reader, new vHRDailyScheduleDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvHRDailyScheduleDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vHRDailyScheduleDt));
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
        public static Int32 GetvHRDailyScheduleDtRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vHRDailyScheduleDt));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "DailyScheduleDtID", keyValue, orderByExpression);
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
        #region vHRScheduleGroupDate
        public static List<vHRScheduleGroupDate> GetvHRScheduleGroupDateList(string filterExpression)
        {
            List<vHRScheduleGroupDate> result = new List<vHRScheduleGroupDate>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vHRScheduleGroupDate));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vHRScheduleGroupDate)helper.IDataReaderToObject(reader, new vHRScheduleGroupDate()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvHRScheduleGroupDateRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vHRScheduleGroupDate));
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
        public static List<vHRScheduleGroupDate> GetvHRScheduleGroupDateList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vHRScheduleGroupDate> result = new List<vHRScheduleGroupDate>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vHRScheduleGroupDate));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vHRScheduleGroupDate)helper.IDataReaderToObject(reader, new vHRScheduleGroupDate()));
            }
            catch (Exception ex)
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
        #region vHRScheduleGroupEmployee
        public static List<vHRScheduleGroupEmployee> GetvHRScheduleGroupEmployeeList(string filterExpression)
        {
            List<vHRScheduleGroupEmployee> result = new List<vHRScheduleGroupEmployee>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vHRScheduleGroupEmployee));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vHRScheduleGroupEmployee)helper.IDataReaderToObject(reader, new vHRScheduleGroupEmployee()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvHRScheduleGroupEmployeeRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vHRScheduleGroupEmployee));
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
        public static List<vHRScheduleGroupEmployee> GetvHRScheduleGroupEmployeeList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vHRScheduleGroupEmployee> result = new List<vHRScheduleGroupEmployee>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vHRScheduleGroupEmployee));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vHRScheduleGroupEmployee)helper.IDataReaderToObject(reader, new vHRScheduleGroupEmployee()));
            }
            catch (Exception ex)
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
        #region vHRScheduleGroupHd
        public static List<vHRScheduleGroupHd> GetvHRScheduleGroupHdList(string filterExpression)
        {
            List<vHRScheduleGroupHd> result = new List<vHRScheduleGroupHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vHRScheduleGroupHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vHRScheduleGroupHd)helper.IDataReaderToObject(reader, new vHRScheduleGroupHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvHRScheduleGroupHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vHRScheduleGroupHd));
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
        public static vHRScheduleGroupHd GetvHRScheduleGroupHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vHRScheduleGroupHd> result = new List<vHRScheduleGroupHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vHRScheduleGroupHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vHRScheduleGroupHd)helper.IDataReaderToObject(reader, new vHRScheduleGroupHd()));
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
        public static Int32 GetvHRScheduleGroupHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vHRScheduleGroupHd));
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
        public static List<vHRScheduleGroupHd> GetvHRScheduleGroupHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vHRScheduleGroupHd> result = new List<vHRScheduleGroupHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vHRScheduleGroupHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vHRScheduleGroupHd)helper.IDataReaderToObject(reader, new vHRScheduleGroupHd()));
            }
            catch (Exception ex)
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
        public static Decimal GetvItemDistributionDtSumQtyOnOrder(string filterExpression)
        {
            Decimal result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemDistributionDt));
                ctx.CommandText = helper.SelectSumColumn("Quantity * ConversionFactor", filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                if (row == null || row.ItemArray.GetValue(0) is DBNull)
                    result = 0;
                else
                    result = Convert.ToDecimal(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
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
        #region vItemDistributionDtQtyOnOrderPerItemPerToLocation
        public static List<vItemDistributionDtQtyOnOrderPerItemPerToLocation> GetvItemDistributionDtQtyOnOrderPerItemPerToLocationList(string filterExpression)
        {
            List<vItemDistributionDtQtyOnOrderPerItemPerToLocation> result = new List<vItemDistributionDtQtyOnOrderPerItemPerToLocation>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemDistributionDtQtyOnOrderPerItemPerToLocation));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemDistributionDtQtyOnOrderPerItemPerToLocation)helper.IDataReaderToObject(reader, new vItemDistributionDtQtyOnOrderPerItemPerToLocation()));
            }
            catch (Exception ex)
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
        #region vItemGroupPlanning
        public static List<vItemGroupPlanning> GetvItemGroupPlanningList(string filterExpression)
        {
            List<vItemGroupPlanning> result = new List<vItemGroupPlanning>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemGroupPlanning));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemGroupPlanning)helper.IDataReaderToObject(reader, new vItemGroupPlanning()));
            }
            catch (Exception ex)
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
        public static Int32 GetvItemMasterRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemMaster));
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
        public static List<vItemMaster> GetvItemMasterList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vItemMaster> result = new List<vItemMaster>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemMaster));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
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
        #region vItemPlanningCustom
        public static List<vItemPlanningCustom> GetvItemPlanningCustomList(string filterExpression)
        {
            List<vItemPlanningCustom> result = new List<vItemPlanningCustom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemPlanningCustom));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemPlanningCustom)helper.IDataReaderToObject(reader, new vItemPlanningCustom()));
            }
            catch (Exception ex)
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
        public static Decimal GetvItemRequestDtSumQtyOnOrder(string filterExpression)
        {
            Decimal result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemRequestDt));
                ctx.CommandText = helper.SelectSumColumn("Quantity * ConversionFactor", filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                if (row == null || row.ItemArray.GetValue(0) is DBNull)
                    result = 0;
                else
                    result = Convert.ToDecimal(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
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
        #region vItemRequestDtQtyOnOrderPerItemPerFromSiteServiceUnit
        public static List<vItemRequestDtQtyOnOrderPerItemPerFromSiteServiceUnit> GetvItemRequestDtQtyOnOrderPerItemPerFromSiteServiceUnitList(string filterExpression)
        {
            List<vItemRequestDtQtyOnOrderPerItemPerFromSiteServiceUnit> result = new List<vItemRequestDtQtyOnOrderPerItemPerFromSiteServiceUnit>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vItemRequestDtQtyOnOrderPerItemPerFromSiteServiceUnit));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vItemRequestDtQtyOnOrderPerItemPerFromSiteServiceUnit)helper.IDataReaderToObject(reader, new vItemRequestDtQtyOnOrderPerItemPerFromSiteServiceUnit()));
            }
            catch (Exception ex)
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
        #region vJobLevel
        public static List<vJobLevel> GetvJobLevelList(string filterExpression)
        {
            List<vJobLevel> result = new List<vJobLevel>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vJobLevel));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vJobLevel)helper.IDataReaderToObject(reader, new vJobLevel()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vJobLevel> GetvJobLevelList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vJobLevel> result = new List<vJobLevel>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vJobLevel));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vJobLevel)helper.IDataReaderToObject(reader, new vJobLevel()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvJobLevelRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vJobLevel));
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
        public static Int32 GetvJobLevelRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vJobLevel));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "JobLevelID", keyValue, orderByExpression);
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
        #region vJobLevelRenumeration
        public static List<vJobLevelRenumeration> GetvJobLevelRenumerationList(string filterExpression)
        {
            List<vJobLevelRenumeration> result = new List<vJobLevelRenumeration>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vJobLevelRenumeration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vJobLevelRenumeration)helper.IDataReaderToObject(reader, new vJobLevelRenumeration()));
            }
            catch (Exception ex)
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
        #region vLocationItemGroupPath
        public static List<vLocationItemGroupPath> GetvLocationItemGroupPathList(string filterExpression)
        {
            List<vLocationItemGroupPath> result = new List<vLocationItemGroupPath>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vLocationItemGroupPath));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vLocationItemGroupPath)helper.IDataReaderToObject(reader, new vLocationItemGroupPath()));
            }
            catch (Exception ex)
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
        #region vMarkTypeFormula
        public static List<vMarkTypeFormula> GetvMarkTypeFormulaList(string filterExpression)
        {
            List<vMarkTypeFormula> result = new List<vMarkTypeFormula>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vMarkTypeFormula));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vMarkTypeFormula)helper.IDataReaderToObject(reader, new vMarkTypeFormula()));
            }
            catch (Exception ex)
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
        #region vMarkTypeHd
        public static List<vMarkTypeHd> GetvMarkTypeHdList(string filterExpression)
        {
            List<vMarkTypeHd> result = new List<vMarkTypeHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vMarkTypeHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vMarkTypeHd)helper.IDataReaderToObject(reader, new vMarkTypeHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvMarkTypeHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vMarkTypeHd));
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
        public static List<vMarkTypeHd> GetvMarkTypeHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vMarkTypeHd> result = new List<vMarkTypeHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vMarkTypeHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vMarkTypeHd)helper.IDataReaderToObject(reader, new vMarkTypeHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvMarkTypeHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vMarkTypeHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "MarkTypeID", keyValue, orderByExpression);
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
        #region vOrganizationDepartment
        public static List<vOrganizationDepartment> GetvOrganizationDepartmentList(string filterExpression)
        {
            List<vOrganizationDepartment> result = new List<vOrganizationDepartment>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vOrganizationDepartment));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vOrganizationDepartment)helper.IDataReaderToObject(reader, new vOrganizationDepartment()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vOrganizationDepartment> GetvOrganizationDepartmentList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vOrganizationDepartment> result = new List<vOrganizationDepartment>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vOrganizationDepartment));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vOrganizationDepartment)helper.IDataReaderToObject(reader, new vOrganizationDepartment()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvOrganizationDepartmentRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vOrganizationDepartment));
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
        public static Int32 GetvOrganizationDepartmentRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vOrganizationDepartment));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "OrganizationDepartmentID", keyValue, orderByExpression);
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
        #region vOrganizationPosition
        public static List<vOrganizationPosition> GetvOrganizationPositionList(string filterExpression)
        {
            List<vOrganizationPosition> result = new List<vOrganizationPosition>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vOrganizationPosition));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vOrganizationPosition)helper.IDataReaderToObject(reader, new vOrganizationPosition()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vOrganizationPosition> GetvOrganizationPositionList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vOrganizationPosition> result = new List<vOrganizationPosition>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vOrganizationPosition));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vOrganizationPosition)helper.IDataReaderToObject(reader, new vOrganizationPosition()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvOrganizationPositionRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vOrganizationPosition));
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
        public static Int32 GetvOrganizationPositionRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vOrganizationPosition));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "OrganizationPositionID", keyValue, orderByExpression);
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
        #region vOrganizationPositionRenumeration
        public static List<vOrganizationPositionRenumeration> GetvOrganizationPositionRenumerationList(string filterExpression)
        {
            List<vOrganizationPositionRenumeration> result = new List<vOrganizationPositionRenumeration>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vOrganizationPositionRenumeration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vOrganizationPositionRenumeration)helper.IDataReaderToObject(reader, new vOrganizationPositionRenumeration()));
            }
            catch (Exception ex)
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
        #region vOvertimeProposalEmployee
        public static List<vOvertimeProposalEmployee> GetvOvertimeProposalEmployeeList(string filterExpression)
        {
            List<vOvertimeProposalEmployee> result = new List<vOvertimeProposalEmployee>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vOvertimeProposalEmployee));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vOvertimeProposalEmployee)helper.IDataReaderToObject(reader, new vOvertimeProposalEmployee()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvOvertimeProposalEmployeeRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vOvertimeProposalEmployee));
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
        public static List<vOvertimeProposalEmployee> GetvOvertimeProposalEmployeeList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vOvertimeProposalEmployee> result = new List<vOvertimeProposalEmployee>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vOvertimeProposalEmployee));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vOvertimeProposalEmployee)helper.IDataReaderToObject(reader, new vOvertimeProposalEmployee()));
            }
            catch (Exception ex)
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
        #region vOvertimeProposalHd
        public static List<vOvertimeProposalHd> GetvOvertimeProposalHdList(string filterExpression)
        {
            List<vOvertimeProposalHd> result = new List<vOvertimeProposalHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vOvertimeProposalHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vOvertimeProposalHd)helper.IDataReaderToObject(reader, new vOvertimeProposalHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvOvertimeProposalHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vOvertimeProposalHd));
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
        public static vOvertimeProposalHd GetvOvertimeProposalHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vOvertimeProposalHd> result = new List<vOvertimeProposalHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vOvertimeProposalHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vOvertimeProposalHd)helper.IDataReaderToObject(reader, new vOvertimeProposalHd()));
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
        public static Int32 GetvOvertimeProposalHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vOvertimeProposalHd));
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
        public static List<vOvertimeProposalHd> GetvOvertimeProposalHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vOvertimeProposalHd> result = new List<vOvertimeProposalHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vOvertimeProposalHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vOvertimeProposalHd)helper.IDataReaderToObject(reader, new vOvertimeProposalHd()));
            }
            catch (Exception ex)
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
        #region vPeriodClassTypeStudentPerGender
        public static List<vPeriodClassTypeStudentPerGender> GetvPeriodClassTypeStudentPerGenderList(string filterExpression)
        {
            List<vPeriodClassTypeStudentPerGender> result = new List<vPeriodClassTypeStudentPerGender>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPeriodClassTypeStudentPerGender));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPeriodClassTypeStudentPerGender)helper.IDataReaderToObject(reader, new vPeriodClassTypeStudentPerGender()));
            }
            catch (Exception ex)
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
        #region vPeriodClassTypeStudentPerReligion
        public static List<vPeriodClassTypeStudentPerReligion> GetvPeriodClassTypeStudentPerReligionList(string filterExpression)
        {
            List<vPeriodClassTypeStudentPerReligion> result = new List<vPeriodClassTypeStudentPerReligion>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPeriodClassTypeStudentPerReligion));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPeriodClassTypeStudentPerReligion)helper.IDataReaderToObject(reader, new vPeriodClassTypeStudentPerReligion()));
            }
            catch (Exception ex)
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
        #region vPeriodClassTypeSubjectFinalMarkFormulaCustom
        public static List<vPeriodClassTypeSubjectFinalMarkFormulaCustom> GetvPeriodClassTypeSubjectFinalMarkFormulaCustomList(string filterExpression)
        {
            List<vPeriodClassTypeSubjectFinalMarkFormulaCustom> result = new List<vPeriodClassTypeSubjectFinalMarkFormulaCustom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPeriodClassTypeSubjectFinalMarkFormulaCustom));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPeriodClassTypeSubjectFinalMarkFormulaCustom)helper.IDataReaderToObject(reader, new vPeriodClassTypeSubjectFinalMarkFormulaCustom()));
            }
            catch (Exception ex)
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
        #region vPeriodFinalMarkFormula
        public static List<vPeriodFinalMarkFormula> GetvPeriodFinalMarkFormulaList(string filterExpression)
        {
            List<vPeriodFinalMarkFormula> result = new List<vPeriodFinalMarkFormula>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPeriodFinalMarkFormula));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPeriodFinalMarkFormula)helper.IDataReaderToObject(reader, new vPeriodFinalMarkFormula()));
            }
            catch (Exception ex)
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
        #region vPeriodGrade
        public static List<vPeriodGrade> GetvPeriodGradeList(string filterExpression)
        {
            List<vPeriodGrade> result = new List<vPeriodGrade>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPeriodGrade));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPeriodGrade)helper.IDataReaderToObject(reader, new vPeriodGrade()));
            }
            catch (Exception ex)
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
        #region vPeriodGradeClassType
        public static List<vPeriodGradeClassType> GetvPeriodGradeClassTypeList(string filterExpression)
        {
            List<vPeriodGradeClassType> result = new List<vPeriodGradeClassType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPeriodGradeClassType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPeriodGradeClassType)helper.IDataReaderToObject(reader, new vPeriodGradeClassType()));
            }
            catch (Exception ex)
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
        #region vPeriodScheduleClassType
        public static List<vPeriodScheduleClassType> GetvPeriodScheduleClassTypeList(string filterExpression)
        {
            List<vPeriodScheduleClassType> result = new List<vPeriodScheduleClassType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPeriodScheduleClassType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPeriodScheduleClassType)helper.IDataReaderToObject(reader, new vPeriodScheduleClassType()));
            }
            catch (Exception ex)
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
        public static Decimal GetvPurchaseOrderDtSumQtyOnOrder(string filterExpression)
        {
            Decimal result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseOrderDt));
                ctx.CommandText = helper.SelectSumColumn("Quantity * ConversionFactor", filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                if (row == null || row.ItemArray.GetValue(0) is DBNull)
                    result = 0;
                else
                    result = Convert.ToDecimal(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
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
        public static List<vPurchaseOrderDtOutStanding> GetvPurchaseOrderDtOutStandingList(string filterExpression, IDbContext ctx)
        {
            List<vPurchaseOrderDtOutStanding> result = new List<vPurchaseOrderDtOutStanding>();
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
        #region vPurchaseOrderDtQtyOnOrderPerItemPerSiteServiceUnit
        public static List<vPurchaseOrderDtQtyOnOrderPerItemPerSiteServiceUnit> GetvPurchaseOrderDtQtyOnOrderPerItemPerSiteServiceUnitList(string filterExpression)
        {
            List<vPurchaseOrderDtQtyOnOrderPerItemPerSiteServiceUnit> result = new List<vPurchaseOrderDtQtyOnOrderPerItemPerSiteServiceUnit>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseOrderDtQtyOnOrderPerItemPerSiteServiceUnit));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseOrderDtQtyOnOrderPerItemPerSiteServiceUnit)helper.IDataReaderToObject(reader, new vPurchaseOrderDtQtyOnOrderPerItemPerSiteServiceUnit()));
            }
            catch (Exception ex)
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
        #region vPurchaseReceivePOCustom
        public static List<vPurchaseReceivePOCustom> GetvPurchaseReceivePOCustomList(string filterExpression)
        {
            List<vPurchaseReceivePOCustom> result = new List<vPurchaseReceivePOCustom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseReceivePOCustom));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseReceivePOCustom)helper.IDataReaderToObject(reader, new vPurchaseReceivePOCustom()));
            }
            catch (Exception ex)
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
        #region vPurchaseRequestDP
        public static List<vPurchaseRequestDP> GetvPurchaseRequestDPList(string filterExpression)
        {
            List<vPurchaseRequestDP> result = new List<vPurchaseRequestDP>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseRequestDP));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseRequestDP)helper.IDataReaderToObject(reader, new vPurchaseRequestDP()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vPurchaseRequestDP> GetvPurchaseRequestDPList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vPurchaseRequestDP> result = new List<vPurchaseRequestDP>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseRequestDP));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseRequestDP)helper.IDataReaderToObject(reader, new vPurchaseRequestDP()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvPurchaseRequestDPRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseRequestDP));
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
        public static Decimal GetvPurchaseRequestDtSumQtyOnOrder(string filterExpression)
        {
            Decimal result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseRequestDt));
                ctx.CommandText = helper.SelectSumColumn("Quantity * ConversionFactor", filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                if (row == null || row.ItemArray.GetValue(0) is DBNull)
                    result = 0;
                else
                    result = Convert.ToDecimal(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
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
        #region vPurchaseRequestDtQtyOnOrderPerItemPerSiteServiceUnit
        public static List<vPurchaseRequestDtQtyOnOrderPerItemPerSiteServiceUnit> GetvPurchaseRequestDtQtyOnOrderPerItemPerSiteServiceUnitList(string filterExpression)
        {
            List<vPurchaseRequestDtQtyOnOrderPerItemPerSiteServiceUnit> result = new List<vPurchaseRequestDtQtyOnOrderPerItemPerSiteServiceUnit>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vPurchaseRequestDtQtyOnOrderPerItemPerSiteServiceUnit));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vPurchaseRequestDtQtyOnOrderPerItemPerSiteServiceUnit)helper.IDataReaderToObject(reader, new vPurchaseRequestDtQtyOnOrderPerItemPerSiteServiceUnit()));
            }
            catch (Exception ex)
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
        #region vRActivityHistory
        public static List<vRActivityHistory> GetvRActivityHistoryList(string filterExpression)
        {
            List<vRActivityHistory> result = new List<vRActivityHistory>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRActivityHistory));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRActivityHistory)helper.IDataReaderToObject(reader, new vRActivityHistory()));
            }
            catch (Exception ex)
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
        #region vRBudgetRealizationDt
        public static List<vRBudgetRealizationDt> GetvRBudgetRealizationDtList(string filterExpression)
        {
            List<vRBudgetRealizationDt> result = new List<vRBudgetRealizationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRBudgetRealizationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRBudgetRealizationDt)helper.IDataReaderToObject(reader, new vRBudgetRealizationDt()));
            }
            catch (Exception ex)
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
        #region vRBudgetRealizationHd
        public static List<vRBudgetRealizationHd> GetvRBudgetRealizationHdList(string filterExpression)
        {
            List<vRBudgetRealizationHd> result = new List<vRBudgetRealizationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRBudgetRealizationHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRBudgetRealizationHd)helper.IDataReaderToObject(reader, new vRBudgetRealizationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vRBudgetRealizationHd> GetvRBudgetRealizationHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vRBudgetRealizationHd> result = new List<vRBudgetRealizationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRBudgetRealizationHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRBudgetRealizationHd)helper.IDataReaderToObject(reader, new vRBudgetRealizationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvRBudgetRealizationHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRBudgetRealizationHd));
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
        public static Int32 GetvRBudgetRealizationHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRBudgetRealizationHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "BudgetRealizationNo", keyValue, orderByExpression);
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
        public static vRBudgetRealizationHd GetvRBudgetRealizationHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vRBudgetRealizationHd> result = new List<vRBudgetRealizationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRBudgetRealizationHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRBudgetRealizationHd)helper.IDataReaderToObject(reader, new vRBudgetRealizationHd()));
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
        #region vRBudgetRequestHd
        public static List<vRBudgetRequestHd> GetvRBudgetRequestHdList(string filterExpression)
        {
            List<vRBudgetRequestHd> result = new List<vRBudgetRequestHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRBudgetRequestHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRBudgetRequestHd)helper.IDataReaderToObject(reader, new vRBudgetRequestHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vRBudgetRequestHd> GetvRBudgetRequestHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vRBudgetRequestHd> result = new List<vRBudgetRequestHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRBudgetRequestHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRBudgetRequestHd)helper.IDataReaderToObject(reader, new vRBudgetRequestHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvRBudgetRequestHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRBudgetRequestHd));
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
        public static Int32 GetvRBudgetRequestHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRBudgetRequestHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "BudgetRequestNo", keyValue, orderByExpression);
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
        public static vRBudgetRequestHd GetvRBudgetRequestHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vRBudgetRequestHd> result = new List<vRBudgetRequestHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRBudgetRequestHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRBudgetRequestHd)helper.IDataReaderToObject(reader, new vRBudgetRequestHd()));
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
        #region vRenumerationComp
        public static List<vRenumerationComp> GetvRenumerationCompList(string filterExpression)
        {
            List<vRenumerationComp> result = new List<vRenumerationComp>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRenumerationComp));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRenumerationComp)helper.IDataReaderToObject(reader, new vRenumerationComp()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vRenumerationComp> GetvRenumerationCompList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vRenumerationComp> result = new List<vRenumerationComp>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRenumerationComp));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRenumerationComp)helper.IDataReaderToObject(reader, new vRenumerationComp()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvRenumerationCompRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRenumerationComp));
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
        public static Int32 GetvRenumerationCompRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRenumerationComp));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "RenumerationCompID", keyValue, orderByExpression);
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
        #region vRenumerationHd
        public static List<vRenumerationHd> GetvRenumerationHdList(string filterExpression)
        {
            List<vRenumerationHd> result = new List<vRenumerationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRenumerationHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRenumerationHd)helper.IDataReaderToObject(reader, new vRenumerationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vRenumerationHd> GetvRenumerationHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vRenumerationHd> result = new List<vRenumerationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRenumerationHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRenumerationHd)helper.IDataReaderToObject(reader, new vRenumerationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvRenumerationHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRenumerationHd));
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
        public static Int32 GetvRenumerationHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRenumerationHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "RenumerationID", keyValue, orderByExpression);
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
        #region vRoom
        public static List<vRoom> GetvRoomList(string filterExpression)
        {
            List<vRoom> result = new List<vRoom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRoom));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRoom)helper.IDataReaderToObject(reader, new vRoom()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vRoom> GetvRoomList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vRoom> result = new List<vRoom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRoom));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRoom)helper.IDataReaderToObject(reader, new vRoom()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        public static Int32 GetvRoomRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRoom));
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
        public static Int32 GetvRoomRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRoom));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "RoomID", keyValue, orderByExpression);
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
        #region vRoomSite
        public static List<vRoomSite> GetvRoomSiteList(string filterExpression)
        {
            List<vRoomSite> result = new List<vRoomSite>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRoomSite));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRoomSite)helper.IDataReaderToObject(reader, new vRoomSite()));
            }
            catch (Exception ex)
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
        #region vRProject
        public static List<vRProject> GetvRProjectList(string filterExpression)
        {
            List<vRProject> result = new List<vRProject>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProject));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRProject)helper.IDataReaderToObject(reader, new vRProject()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vRProject> GetvRProjectList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vRProject> result = new List<vRProject>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProject));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRProject)helper.IDataReaderToObject(reader, new vRProject()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvRProjectRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProject));
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
        public static Int32 GetvRProjectRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProject));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ProjectID", keyValue, orderByExpression);
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
        #region vRProjectGroup
        public static List<vRProjectGroup> GetvRProjectGroupList(string filterExpression)
        {
            List<vRProjectGroup> result = new List<vRProjectGroup>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectGroup));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRProjectGroup)helper.IDataReaderToObject(reader, new vRProjectGroup()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vRProjectGroup> GetvRProjectGroupList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vRProjectGroup> result = new List<vRProjectGroup>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectGroup));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRProjectGroup)helper.IDataReaderToObject(reader, new vRProjectGroup()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        public static Int32 GetvRProjectGroupRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectGroup));
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
        public static Int32 GetvRProjectGroupRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectGroup));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ProjectGroupID", keyValue, orderByExpression);
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
        #region vRProjectLog
        public static List<vRProjectLog> GetvRProjectLogList(string filterExpression)
        {
            List<vRProjectLog> result = new List<vRProjectLog>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectLog));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRProjectLog)helper.IDataReaderToObject(reader, new vRProjectLog()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vRProjectLog> GetvRProjectLogList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vRProjectLog> result = new List<vRProjectLog>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectLog));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRProjectLog)helper.IDataReaderToObject(reader, new vRProjectLog()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        public static Int32 GetvRProjectLogRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectLog));
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
        public static Int32 GetvRProjectLogRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectLog));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ProjectLogID", keyValue, orderByExpression);
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
        #region vRProjectOrganization
        public static List<vRProjectOrganization> GetvRProjectOrganizationList(string filterExpression)
        {
            List<vRProjectOrganization> result = new List<vRProjectOrganization>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectOrganization));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRProjectOrganization)helper.IDataReaderToObject(reader, new vRProjectOrganization()));
            }
            catch (Exception ex)
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
        #region vRProjectOrganizationMember
        public static List<vRProjectOrganizationMember> GetvRProjectOrganizationMemberList(string filterExpression)
        {
            List<vRProjectOrganizationMember> result = new List<vRProjectOrganizationMember>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectOrganizationMember));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRProjectOrganizationMember)helper.IDataReaderToObject(reader, new vRProjectOrganizationMember()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vRProjectOrganizationMember> GetvRProjectOrganizationMemberList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vRProjectOrganizationMember> result = new List<vRProjectOrganizationMember>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectOrganizationMember));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRProjectOrganizationMember)helper.IDataReaderToObject(reader, new vRProjectOrganizationMember()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvRProjectOrganizationMemberRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectOrganizationMember));
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
        #region vRProjectTask
        public static List<vRProjectTask> GetvRProjectTaskList(string filterExpression)
        {
            List<vRProjectTask> result = new List<vRProjectTask>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectTask));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRProjectTask)helper.IDataReaderToObject(reader, new vRProjectTask()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vRProjectTask> GetvRProjectTaskList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vRProjectTask> result = new List<vRProjectTask>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectTask));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRProjectTask)helper.IDataReaderToObject(reader, new vRProjectTask()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvRProjectTaskRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectTask));
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
        #region vRProjectTaskAssign
        public static List<vRProjectTaskAssign> GetvRProjectTaskAssignList(string filterExpression)
        {
            List<vRProjectTaskAssign> result = new List<vRProjectTaskAssign>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectTaskAssign));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRProjectTaskAssign)helper.IDataReaderToObject(reader, new vRProjectTaskAssign()));
            }
            catch (Exception ex)
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
        #region vRProjectTaskFile
        public static List<vRProjectTaskFile> GetvRProjectTaskFileList(string filterExpression)
        {
            List<vRProjectTaskFile> result = new List<vRProjectTaskFile>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectTaskFile));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRProjectTaskFile)helper.IDataReaderToObject(reader, new vRProjectTaskFile()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vRProjectTaskFile> GetvRProjectTaskFileList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vRProjectTaskFile> result = new List<vRProjectTaskFile>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectTaskFile));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRProjectTaskFile)helper.IDataReaderToObject(reader, new vRProjectTaskFile()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        public static Int32 GetvRProjectTaskFileRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectTaskFile));
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
        #region vRProjectTaskLog
        public static List<vRProjectTaskLog> GetvRProjectTaskLogList(string filterExpression)
        {
            List<vRProjectTaskLog> result = new List<vRProjectTaskLog>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectTaskLog));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRProjectTaskLog)helper.IDataReaderToObject(reader, new vRProjectTaskLog()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vRProjectTaskLog> GetvRProjectTaskLogList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vRProjectTaskLog> result = new List<vRProjectTaskLog>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectTaskLog));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vRProjectTaskLog)helper.IDataReaderToObject(reader, new vRProjectTaskLog()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        public static Int32 GetvRProjectTaskLogRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectTaskLog));
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
        public static Int32 GetvRProjectTaskLogRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vRProjectTaskLog));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ProjectTaskLogID", keyValue, orderByExpression);
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
        #region vSchoolSubject
        public static List<vSchoolSubject> GetvSchoolSubjectList(string filterExpression)
        {
            List<vSchoolSubject> result = new List<vSchoolSubject>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSchoolSubject));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSchoolSubject)helper.IDataReaderToObject(reader, new vSchoolSubject()));
            }
            catch (Exception ex)
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
        #region vServiceUnitLocation
        public static List<vServiceUnitLocation> GetvServiceUnitLocationList(string filterExpression)
        {
            List<vServiceUnitLocation> result = new List<vServiceUnitLocation>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vServiceUnitLocation));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vServiceUnitLocation)helper.IDataReaderToObject(reader, new vServiceUnitLocation()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvServiceUnitLocationRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vServiceUnitLocation));
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
        public static List<vServiceUnitLocation> GetvServiceUnitLocationList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vServiceUnitLocation> result = new List<vServiceUnitLocation>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vServiceUnitLocation));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vServiceUnitLocation)helper.IDataReaderToObject(reader, new vServiceUnitLocation()));
            }
            catch (Exception ex)
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
        #region vServiceUnitLocationCustom
        public static List<vServiceUnitLocationCustom> GetvServiceUnitLocationCustomList(string filterExpression)
        {
            List<vServiceUnitLocationCustom> result = new List<vServiceUnitLocationCustom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vServiceUnitLocationCustom));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vServiceUnitLocationCustom)helper.IDataReaderToObject(reader, new vServiceUnitLocationCustom()));
            }
            catch (Exception ex)
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
        #region vSiteServiceUnit
        public static List<vSiteServiceUnit> GetvSiteServiceUnitList(string filterExpression)
        {
            List<vSiteServiceUnit> result = new List<vSiteServiceUnit>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSiteServiceUnit));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSiteServiceUnit)helper.IDataReaderToObject(reader, new vSiteServiceUnit()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vSiteServiceUnit> GetvSiteServiceUnitList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vSiteServiceUnit> result = new List<vSiteServiceUnit>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSiteServiceUnit));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSiteServiceUnit)helper.IDataReaderToObject(reader, new vSiteServiceUnit()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvSiteServiceUnitRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSiteServiceUnit));
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
        #region vStudentCoverageTransactionDt
        public static List<vStudentCoverageTransactionDt> GetvStudentCoverageTransactionDtList(string filterExpression)
        {
            List<vStudentCoverageTransactionDt> result = new List<vStudentCoverageTransactionDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentCoverageTransactionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentCoverageTransactionDt)helper.IDataReaderToObject(reader, new vStudentCoverageTransactionDt()));
            }
            catch (Exception ex)
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
        public static List<vStudentFee> GetvStudentFeeList(string filterExpression, IDbContext ctx)
        {
            List<vStudentFee> result = new List<vStudentFee>();
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
        #region vStudentFeeStatusPerClassSummary
        public static List<vStudentFeeStatusPerClassSummary> GetvStudentFeeStatusPerClassSummaryList(string filterExpression)
        {
            List<vStudentFeeStatusPerClassSummary> result = new List<vStudentFeeStatusPerClassSummary>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentFeeStatusPerClassSummary));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentFeeStatusPerClassSummary)helper.IDataReaderToObject(reader, new vStudentFeeStatusPerClassSummary()));
            }
            catch (Exception ex)
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
        #region vStudentMoveOut
        public static List<vStudentMoveOut> GetvStudentMoveOutList(string filterExpression)
        {
            List<vStudentMoveOut> result = new List<vStudentMoveOut>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentMoveOut));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentMoveOut)helper.IDataReaderToObject(reader, new vStudentMoveOut()));
            }
            catch (Exception ex)
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
        #region vStudentNote
        public static List<vStudentNote> GetvStudentNoteList(string filterExpression)
        {
            List<vStudentNote> result = new List<vStudentNote>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentNote));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentNote)helper.IDataReaderToObject(reader, new vStudentNote()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvStudentNoteRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentNote));
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
        public static List<vStudentNote> GetvStudentNoteList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vStudentNote> result = new List<vStudentNote>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentNote));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentNote)helper.IDataReaderToObject(reader, new vStudentNote()));
            }
            catch (Exception ex)
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
        #region vStudentScholarshipTransactionDt
        public static List<vStudentScholarshipTransactionDt> GetvStudentScholarshipTransactionDtList(string filterExpression)
        {
            List<vStudentScholarshipTransactionDt> result = new List<vStudentScholarshipTransactionDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentScholarshipTransactionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentScholarshipTransactionDt)helper.IDataReaderToObject(reader, new vStudentScholarshipTransactionDt()));
            }
            catch (Exception ex)
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
        #region vStudentScholarshipTransactionDtCustom
        public static List<vStudentScholarshipTransactionDtCustom> GetvStudentScholarshipTransactionDtCustomList(string filterExpression)
        {
            List<vStudentScholarshipTransactionDtCustom> result = new List<vStudentScholarshipTransactionDtCustom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentScholarshipTransactionDtCustom));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentScholarshipTransactionDtCustom)helper.IDataReaderToObject(reader, new vStudentScholarshipTransactionDtCustom()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vStudentScholarshipTransactionDtCustom> GetvStudentScholarshipTransactionDtCustomList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vStudentScholarshipTransactionDtCustom> result = new List<vStudentScholarshipTransactionDtCustom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentScholarshipTransactionDtCustom));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentScholarshipTransactionDtCustom)helper.IDataReaderToObject(reader, new vStudentScholarshipTransactionDtCustom()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvStudentScholarshipTransactionDtCustomRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentScholarshipTransactionDtCustom));
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
        #region vStudentScholarshipTransactionHd
        public static List<vStudentScholarshipTransactionHd> GetvStudentScholarshipTransactionHdList(string filterExpression)
        {
            List<vStudentScholarshipTransactionHd> result = new List<vStudentScholarshipTransactionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentScholarshipTransactionHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentScholarshipTransactionHd)helper.IDataReaderToObject(reader, new vStudentScholarshipTransactionHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vStudentScholarshipTransactionHd> GetvStudentScholarshipTransactionHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vStudentScholarshipTransactionHd> result = new List<vStudentScholarshipTransactionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentScholarshipTransactionHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentScholarshipTransactionHd)helper.IDataReaderToObject(reader, new vStudentScholarshipTransactionHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvStudentScholarshipTransactionHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentScholarshipTransactionHd));
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
        public static Int32 GetvStudentScholarshipTransactionHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentScholarshipTransactionHd));
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

        public static vStudentScholarshipTransactionHd GetvStudentScholarshipTransactionHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vStudentScholarshipTransactionHd> result = new List<vStudentScholarshipTransactionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentScholarshipTransactionHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentScholarshipTransactionHd)helper.IDataReaderToObject(reader, new vStudentScholarshipTransactionHd()));
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
        #region vStudentUkegUpembSummary
        public static List<vStudentUkegUpembSummary> GetvStudentUkegUpembSummaryList(string filterExpression)
        {
            List<vStudentUkegUpembSummary> result = new List<vStudentUkegUpembSummary>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentUkegUpembSummary));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentUkegUpembSummary)helper.IDataReaderToObject(reader, new vStudentUkegUpembSummary()));
            }
            catch (Exception ex)
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
        #region vStudentUsekSummary
        public static List<vStudentUsekSummary> GetvStudentUsekSummaryList(string filterExpression)
        {
            List<vStudentUsekSummary> result = new List<vStudentUsekSummary>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vStudentUsekSummary));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vStudentUsekSummary)helper.IDataReaderToObject(reader, new vStudentUsekSummary()));
            }
            catch (Exception ex)
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
        #region vSubjectCurriculum
        public static List<vSubjectCurriculum> GetvSubjectCurriculumList(string filterExpression)
        {
            List<vSubjectCurriculum> result = new List<vSubjectCurriculum>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSubjectCurriculum));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSubjectCurriculum)helper.IDataReaderToObject(reader, new vSubjectCurriculum()));
            }
            catch (Exception ex)
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
        #region vSubjectCurriculumMeetingPlan
        public static List<vSubjectCurriculumMeetingPlan> GetvSubjectCurriculumMeetingPlanList(string filterExpression)
        {
            List<vSubjectCurriculumMeetingPlan> result = new List<vSubjectCurriculumMeetingPlan>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSubjectCurriculumMeetingPlan));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSubjectCurriculumMeetingPlan)helper.IDataReaderToObject(reader, new vSubjectCurriculumMeetingPlan()));
            }
            catch (Exception ex)
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
        #region vSubjectCurriculumSyllabus
        public static List<vSubjectCurriculumSyllabus> GetvSubjectCurriculumSyllabusList(string filterExpression)
        {
            List<vSubjectCurriculumSyllabus> result = new List<vSubjectCurriculumSyllabus>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSubjectCurriculumSyllabus));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSubjectCurriculumSyllabus)helper.IDataReaderToObject(reader, new vSubjectCurriculumSyllabus()));
            }
            catch (Exception ex)
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
        #region vSubLedgerDt
        public static List<vSubLedgerDt> GetvSubLedgerDtList(string filterExpression)
        {
            List<vSubLedgerDt> result = new List<vSubLedgerDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSubLedgerDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSubLedgerDt)helper.IDataReaderToObject(reader, new vSubLedgerDt()));
            }
            catch (Exception ex)
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
        #region vTemplateEmployeeGroupDt
        public static List<vTemplateEmployeeGroupDt> GetvTemplateEmployeeGroupDtList(string filterExpression)
        {
            List<vTemplateEmployeeGroupDt> result = new List<vTemplateEmployeeGroupDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTemplateEmployeeGroupDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTemplateEmployeeGroupDt)helper.IDataReaderToObject(reader, new vTemplateEmployeeGroupDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vTemplateEmployeeGroupDt> GetvTemplateEmployeeGroupDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTemplateEmployeeGroupDt> result = new List<vTemplateEmployeeGroupDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTemplateEmployeeGroupDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTemplateEmployeeGroupDt)helper.IDataReaderToObject(reader, new vTemplateEmployeeGroupDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTemplateEmployeeGroupDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTemplateEmployeeGroupDt));
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
        #region vTeacherSubjectPerSchoolType
        public static List<vTeacherSubjectPerSchoolType> GetvTeacherSubjectPerSchoolTypeList(string filterExpression)
        {
            List<vTeacherSubjectPerSchoolType> result = new List<vTeacherSubjectPerSchoolType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherSubjectPerSchoolType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTeacherSubjectPerSchoolType)helper.IDataReaderToObject(reader, new vTeacherSubjectPerSchoolType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vTeacherSubjectPerSchoolType> GetvTeacherSubjectPerSchoolTypeList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTeacherSubjectPerSchoolType> result = new List<vTeacherSubjectPerSchoolType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherSubjectPerSchoolType));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTeacherSubjectPerSchoolType)helper.IDataReaderToObject(reader, new vTeacherSubjectPerSchoolType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTeacherSubjectPerSchoolTypeRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeacherSubjectPerSchoolType));
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
        #region vTransEmployeeLoanHd
        public static List<vTransEmployeeLoanHd> GetvTransEmployeeLoanHdList(string filterExpression)
        {
            List<vTransEmployeeLoanHd> result = new List<vTransEmployeeLoanHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeeLoanHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransEmployeeLoanHd)helper.IDataReaderToObject(reader, new vTransEmployeeLoanHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTransEmployeeLoanHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeeLoanHd));
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
        public static vTransEmployeeLoanHd GetvTransEmployeeLoanHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vTransEmployeeLoanHd> result = new List<vTransEmployeeLoanHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeeLoanHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransEmployeeLoanHd)helper.IDataReaderToObject(reader, new vTransEmployeeLoanHd()));
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
        public static Int32 GetvTransEmployeeLoanHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeeLoanHd));
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
        public static List<vTransEmployeeLoanHd> GetvTransEmployeeLoanHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTransEmployeeLoanHd> result = new List<vTransEmployeeLoanHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeeLoanHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransEmployeeLoanHd)helper.IDataReaderToObject(reader, new vTransEmployeeLoanHd()));
            }
            catch (Exception ex)
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
        #region vTransEmployeePositionDt
        public static List<vTransEmployeePositionDt> GetvTransEmployeePositionDtList(string filterExpression)
        {
            List<vTransEmployeePositionDt> result = new List<vTransEmployeePositionDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeePositionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransEmployeePositionDt)helper.IDataReaderToObject(reader, new vTransEmployeePositionDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTransEmployeePositionDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeePositionDt));
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
        public static List<vTransEmployeePositionDt> GetvTransEmployeePositionDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTransEmployeePositionDt> result = new List<vTransEmployeePositionDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeePositionDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransEmployeePositionDt)helper.IDataReaderToObject(reader, new vTransEmployeePositionDt()));
            }
            catch (Exception ex)
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
        #region vTransEmployeePositionHd
        public static List<vTransEmployeePositionHd> GetvTransEmployeePositionHdList(string filterExpression)
        {
            List<vTransEmployeePositionHd> result = new List<vTransEmployeePositionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeePositionHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransEmployeePositionHd)helper.IDataReaderToObject(reader, new vTransEmployeePositionHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTransEmployeePositionHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeePositionHd));
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
        public static vTransEmployeePositionHd GetvTransEmployeePositionHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vTransEmployeePositionHd> result = new List<vTransEmployeePositionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeePositionHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransEmployeePositionHd)helper.IDataReaderToObject(reader, new vTransEmployeePositionHd()));
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
        public static Int32 GetvTransEmployeePositionHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeePositionHd));
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
        public static List<vTransEmployeePositionHd> GetvTransEmployeePositionHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTransEmployeePositionHd> result = new List<vTransEmployeePositionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeePositionHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransEmployeePositionHd)helper.IDataReaderToObject(reader, new vTransEmployeePositionHd()));
            }
            catch (Exception ex)
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
        #region vTransEmployeePositionRenumeration
        public static List<vTransEmployeePositionRenumeration> GetvTransEmployeePositionRenumerationList(string filterExpression)
        {
            List<vTransEmployeePositionRenumeration> result = new List<vTransEmployeePositionRenumeration>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeePositionRenumeration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransEmployeePositionRenumeration)helper.IDataReaderToObject(reader, new vTransEmployeePositionRenumeration()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTransEmployeePositionRenumerationRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeePositionRenumeration));
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
        public static List<vTransEmployeePositionRenumeration> GetvTransEmployeePositionRenumerationList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTransEmployeePositionRenumeration> result = new List<vTransEmployeePositionRenumeration>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeePositionRenumeration));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransEmployeePositionRenumeration)helper.IDataReaderToObject(reader, new vTransEmployeePositionRenumeration()));
            }
            catch (Exception ex)
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
        #region vTransEmployeePositionRenumerationFormula
        public static List<vTransEmployeePositionRenumerationFormula> GetvTransEmployeePositionRenumerationFormulaList(string filterExpression)
        {
            List<vTransEmployeePositionRenumerationFormula> result = new List<vTransEmployeePositionRenumerationFormula>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeePositionRenumerationFormula));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransEmployeePositionRenumerationFormula)helper.IDataReaderToObject(reader, new vTransEmployeePositionRenumerationFormula()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTransEmployeePositionRenumerationFormulaRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeePositionRenumerationFormula));
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
        public static List<vTransEmployeePositionRenumerationFormula> GetvTransEmployeePositionRenumerationFormulaList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTransEmployeePositionRenumerationFormula> result = new List<vTransEmployeePositionRenumerationFormula>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeePositionRenumerationFormula));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransEmployeePositionRenumerationFormula)helper.IDataReaderToObject(reader, new vTransEmployeePositionRenumerationFormula()));
            }
            catch (Exception ex)
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
        #region vTransEmployeeJobLevelDt
        public static List<vTransEmployeeJobLevelDt> GetvTransEmployeeJobLevelDtList(string filterExpression)
        {
            List<vTransEmployeeJobLevelDt> result = new List<vTransEmployeeJobLevelDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeeJobLevelDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransEmployeeJobLevelDt)helper.IDataReaderToObject(reader, new vTransEmployeeJobLevelDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTransEmployeeJobLevelDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeeJobLevelDt));
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
        public static List<vTransEmployeeJobLevelDt> GetvTransEmployeeJobLevelDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTransEmployeeJobLevelDt> result = new List<vTransEmployeeJobLevelDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeeJobLevelDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransEmployeeJobLevelDt)helper.IDataReaderToObject(reader, new vTransEmployeeJobLevelDt()));
            }
            catch (Exception ex)
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
        #region vTransEmployeeJobLevelHd
        public static List<vTransEmployeeJobLevelHd> GetvTransEmployeeJobLevelHdList(string filterExpression)
        {
            List<vTransEmployeeJobLevelHd> result = new List<vTransEmployeeJobLevelHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeeJobLevelHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransEmployeeJobLevelHd)helper.IDataReaderToObject(reader, new vTransEmployeeJobLevelHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTransEmployeeJobLevelHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeeJobLevelHd));
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
        public static vTransEmployeeJobLevelHd GetvTransEmployeeJobLevelHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vTransEmployeeJobLevelHd> result = new List<vTransEmployeeJobLevelHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeeJobLevelHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransEmployeeJobLevelHd)helper.IDataReaderToObject(reader, new vTransEmployeeJobLevelHd()));
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
        public static Int32 GetvTransEmployeeJobLevelHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeeJobLevelHd));
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
        public static List<vTransEmployeeJobLevelHd> GetvTransEmployeeJobLevelHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTransEmployeeJobLevelHd> result = new List<vTransEmployeeJobLevelHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeeJobLevelHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransEmployeeJobLevelHd)helper.IDataReaderToObject(reader, new vTransEmployeeJobLevelHd()));
            }
            catch (Exception ex)
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
        #region vTransEmployeeJobLevelRenumeration
        public static List<vTransEmployeeJobLevelRenumeration> GetvTransEmployeeJobLevelRenumerationList(string filterExpression)
        {
            List<vTransEmployeeJobLevelRenumeration> result = new List<vTransEmployeeJobLevelRenumeration>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeeJobLevelRenumeration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransEmployeeJobLevelRenumeration)helper.IDataReaderToObject(reader, new vTransEmployeeJobLevelRenumeration()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTransEmployeeJobLevelRenumerationRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeeJobLevelRenumeration));
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
        public static List<vTransEmployeeJobLevelRenumeration> GetvTransEmployeeJobLevelRenumerationList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTransEmployeeJobLevelRenumeration> result = new List<vTransEmployeeJobLevelRenumeration>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeeJobLevelRenumeration));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransEmployeeJobLevelRenumeration)helper.IDataReaderToObject(reader, new vTransEmployeeJobLevelRenumeration()));
            }
            catch (Exception ex)
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
        #region vTransEmployeeJobLevelRenumerationFormula
        public static List<vTransEmployeeJobLevelRenumerationFormula> GetvTransEmployeeJobLevelRenumerationFormulaList(string filterExpression)
        {
            List<vTransEmployeeJobLevelRenumerationFormula> result = new List<vTransEmployeeJobLevelRenumerationFormula>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeeJobLevelRenumerationFormula));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransEmployeeJobLevelRenumerationFormula)helper.IDataReaderToObject(reader, new vTransEmployeeJobLevelRenumerationFormula()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTransEmployeeJobLevelRenumerationFormulaRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeeJobLevelRenumerationFormula));
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
        public static List<vTransEmployeeJobLevelRenumerationFormula> GetvTransEmployeeJobLevelRenumerationFormulaList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTransEmployeeJobLevelRenumerationFormula> result = new List<vTransEmployeeJobLevelRenumerationFormula>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransEmployeeJobLevelRenumerationFormula));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransEmployeeJobLevelRenumerationFormula)helper.IDataReaderToObject(reader, new vTransEmployeeJobLevelRenumerationFormula()));
            }
            catch (Exception ex)
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
        #region vTransJobLevelRenumerationDt
        public static List<vTransJobLevelRenumerationDt> GetvTransJobLevelRenumerationDtList(string filterExpression)
        {
            List<vTransJobLevelRenumerationDt> result = new List<vTransJobLevelRenumerationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransJobLevelRenumerationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransJobLevelRenumerationDt)helper.IDataReaderToObject(reader, new vTransJobLevelRenumerationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTransJobLevelRenumerationDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransJobLevelRenumerationDt));
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
        public static List<vTransJobLevelRenumerationDt> GetvTransJobLevelRenumerationDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTransJobLevelRenumerationDt> result = new List<vTransJobLevelRenumerationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransJobLevelRenumerationDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransJobLevelRenumerationDt)helper.IDataReaderToObject(reader, new vTransJobLevelRenumerationDt()));
            }
            catch (Exception ex)
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
        #region vTransJobLevelRenumerationHd
        public static List<vTransJobLevelRenumerationHd> GetvTransJobLevelRenumerationHdList(string filterExpression)
        {
            List<vTransJobLevelRenumerationHd> result = new List<vTransJobLevelRenumerationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransJobLevelRenumerationHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransJobLevelRenumerationHd)helper.IDataReaderToObject(reader, new vTransJobLevelRenumerationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTransJobLevelRenumerationHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransJobLevelRenumerationHd));
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
        public static vTransJobLevelRenumerationHd GetvTransJobLevelRenumerationHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vTransJobLevelRenumerationHd> result = new List<vTransJobLevelRenumerationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransJobLevelRenumerationHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransJobLevelRenumerationHd)helper.IDataReaderToObject(reader, new vTransJobLevelRenumerationHd()));
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
        public static Int32 GetvTransJobLevelRenumerationHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransJobLevelRenumerationHd));
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
        public static List<vTransJobLevelRenumerationHd> GetvTransJobLevelRenumerationHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTransJobLevelRenumerationHd> result = new List<vTransJobLevelRenumerationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransJobLevelRenumerationHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransJobLevelRenumerationHd)helper.IDataReaderToObject(reader, new vTransJobLevelRenumerationHd()));
            }
            catch (Exception ex)
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
        #region vTransPositionRenumerationDt
        public static List<vTransPositionRenumerationDt> GetvTransPositionRenumerationDtList(string filterExpression)
        {
            List<vTransPositionRenumerationDt> result = new List<vTransPositionRenumerationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransPositionRenumerationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransPositionRenumerationDt)helper.IDataReaderToObject(reader, new vTransPositionRenumerationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTransPositionRenumerationDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransPositionRenumerationDt));
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
        public static List<vTransPositionRenumerationDt> GetvTransPositionRenumerationDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTransPositionRenumerationDt> result = new List<vTransPositionRenumerationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransPositionRenumerationDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransPositionRenumerationDt)helper.IDataReaderToObject(reader, new vTransPositionRenumerationDt()));
            }
            catch (Exception ex)
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
        #region vTransPositionRenumerationHd
        public static List<vTransPositionRenumerationHd> GetvTransPositionRenumerationHdList(string filterExpression)
        {
            List<vTransPositionRenumerationHd> result = new List<vTransPositionRenumerationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransPositionRenumerationHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransPositionRenumerationHd)helper.IDataReaderToObject(reader, new vTransPositionRenumerationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTransPositionRenumerationHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransPositionRenumerationHd));
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
        public static vTransPositionRenumerationHd GetvTransPositionRenumerationHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vTransPositionRenumerationHd> result = new List<vTransPositionRenumerationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransPositionRenumerationHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransPositionRenumerationHd)helper.IDataReaderToObject(reader, new vTransPositionRenumerationHd()));
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
        public static Int32 GetvTransPositionRenumerationHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransPositionRenumerationHd));
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
        public static List<vTransPositionRenumerationHd> GetvTransPositionRenumerationHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTransPositionRenumerationHd> result = new List<vTransPositionRenumerationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransPositionRenumerationHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransPositionRenumerationHd)helper.IDataReaderToObject(reader, new vTransPositionRenumerationHd()));
            }
            catch (Exception ex)
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
        #region vTransRenumerationDt
        public static List<vTransRenumerationDt> GetvTransRenumerationDtList(string filterExpression)
        {
            List<vTransRenumerationDt> result = new List<vTransRenumerationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransRenumerationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransRenumerationDt)helper.IDataReaderToObject(reader, new vTransRenumerationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTransRenumerationDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransRenumerationDt));
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
        public static List<vTransRenumerationDt> GetvTransRenumerationDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTransRenumerationDt> result = new List<vTransRenumerationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransRenumerationDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransRenumerationDt)helper.IDataReaderToObject(reader, new vTransRenumerationDt()));
            }
            catch (Exception ex)
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
        #region vTransRenumerationHd
        public static List<vTransRenumerationHd> GetvTransRenumerationHdList(string filterExpression)
        {
            List<vTransRenumerationHd> result = new List<vTransRenumerationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransRenumerationHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransRenumerationHd)helper.IDataReaderToObject(reader, new vTransRenumerationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTransRenumerationHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransRenumerationHd));
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
        public static vTransRenumerationHd GetvTransRenumerationHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vTransRenumerationHd> result = new List<vTransRenumerationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransRenumerationHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransRenumerationHd)helper.IDataReaderToObject(reader, new vTransRenumerationHd()));
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
        public static Int32 GetvTransRenumerationHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransRenumerationHd));
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
        public static List<vTransRenumerationHd> GetvTransRenumerationHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTransRenumerationHd> result = new List<vTransRenumerationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransRenumerationHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransRenumerationHd)helper.IDataReaderToObject(reader, new vTransRenumerationHd()));
            }
            catch (Exception ex)
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
        #region vTransRenumerationDtFormula
        public static List<vTransRenumerationDtFormula> GetvTransRenumerationDtFormulaList(string filterExpression)
        {
            List<vTransRenumerationDtFormula> result = new List<vTransRenumerationDtFormula>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransRenumerationDtFormula));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransRenumerationDtFormula)helper.IDataReaderToObject(reader, new vTransRenumerationDtFormula()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTransRenumerationDtFormulaRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransRenumerationDtFormula));
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
        public static List<vTransRenumerationDtFormula> GetvTransRenumerationDtFormulaList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTransRenumerationDtFormula> result = new List<vTransRenumerationDtFormula>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransRenumerationDtFormula));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransRenumerationDtFormula)helper.IDataReaderToObject(reader, new vTransRenumerationDtFormula()));
            }
            catch (Exception ex)
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
        #region vTransRenumerationCompFormulaDt
        public static List<vTransRenumerationCompFormulaDt> GetvTransRenumerationCompFormulaDtList(string filterExpression)
        {
            List<vTransRenumerationCompFormulaDt> result = new List<vTransRenumerationCompFormulaDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransRenumerationCompFormulaDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransRenumerationCompFormulaDt)helper.IDataReaderToObject(reader, new vTransRenumerationCompFormulaDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTransRenumerationCompFormulaDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransRenumerationCompFormulaDt));
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
        public static List<vTransRenumerationCompFormulaDt> GetvTransRenumerationCompFormulaDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTransRenumerationCompFormulaDt> result = new List<vTransRenumerationCompFormulaDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransRenumerationCompFormulaDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransRenumerationCompFormulaDt)helper.IDataReaderToObject(reader, new vTransRenumerationCompFormulaDt()));
            }
            catch (Exception ex)
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
        #region vTransRenumerationCompFormulaHd
        public static List<vTransRenumerationCompFormulaHd> GetvTransRenumerationCompFormulaHdList(string filterExpression)
        {
            List<vTransRenumerationCompFormulaHd> result = new List<vTransRenumerationCompFormulaHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransRenumerationCompFormulaHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransRenumerationCompFormulaHd)helper.IDataReaderToObject(reader, new vTransRenumerationCompFormulaHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTransRenumerationCompFormulaHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransRenumerationCompFormulaHd));
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
        public static vTransRenumerationCompFormulaHd GetvTransRenumerationCompFormulaHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vTransRenumerationCompFormulaHd> result = new List<vTransRenumerationCompFormulaHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransRenumerationCompFormulaHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransRenumerationCompFormulaHd)helper.IDataReaderToObject(reader, new vTransRenumerationCompFormulaHd()));
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
        public static Int32 GetvTransRenumerationCompFormulaHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransRenumerationCompFormulaHd));
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
        public static List<vTransRenumerationCompFormulaHd> GetvTransRenumerationCompFormulaHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTransRenumerationCompFormulaHd> result = new List<vTransRenumerationCompFormulaHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransRenumerationCompFormulaHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransRenumerationCompFormulaHd)helper.IDataReaderToObject(reader, new vTransRenumerationCompFormulaHd()));
            }
            catch (Exception ex)
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
        #region vTransTeacherProfileDt
        public static List<vTransTeacherProfileDt> GetvTransTeacherProfileDtList(string filterExpression)
        {
            List<vTransTeacherProfileDt> result = new List<vTransTeacherProfileDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransTeacherProfileDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransTeacherProfileDt)helper.IDataReaderToObject(reader, new vTransTeacherProfileDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTransTeacherProfileDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransTeacherProfileDt));
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
        public static List<vTransTeacherProfileDt> GetvTransTeacherProfileDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTransTeacherProfileDt> result = new List<vTransTeacherProfileDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransTeacherProfileDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransTeacherProfileDt)helper.IDataReaderToObject(reader, new vTransTeacherProfileDt()));
            }
            catch (Exception ex)
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
        #region vTransTeacherProfileDtItem
        public static List<vTransTeacherProfileDtItem> GetvTransTeacherProfileDtItemList(string filterExpression)
        {
            List<vTransTeacherProfileDtItem> result = new List<vTransTeacherProfileDtItem>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransTeacherProfileDtItem));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransTeacherProfileDtItem)helper.IDataReaderToObject(reader, new vTransTeacherProfileDtItem()));
            }
            catch (Exception ex)
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
        #region vTransTeacherProfileHd
        public static List<vTransTeacherProfileHd> GetvTransTeacherProfileHdList(string filterExpression)
        {
            List<vTransTeacherProfileHd> result = new List<vTransTeacherProfileHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransTeacherProfileHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransTeacherProfileHd)helper.IDataReaderToObject(reader, new vTransTeacherProfileHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvTransTeacherProfileHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransTeacherProfileHd));
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
        public static vTransTeacherProfileHd GetvTransTeacherProfileHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vTransTeacherProfileHd> result = new List<vTransTeacherProfileHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransTeacherProfileHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransTeacherProfileHd)helper.IDataReaderToObject(reader, new vTransTeacherProfileHd()));
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
        public static Int32 GetvTransTeacherProfileHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransTeacherProfileHd));
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
        public static List<vTransTeacherProfileHd> GetvTransTeacherProfileHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTransTeacherProfileHd> result = new List<vTransTeacherProfileHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTransTeacherProfileHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTransTeacherProfileHd)helper.IDataReaderToObject(reader, new vTransTeacherProfileHd()));
            }
            catch (Exception ex)
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

        #region Project Management
        #region vActivityHistory
        public static List<vActivityHistory> GetvActivityHistoryList(string filterExpression)
        {
            List<vActivityHistory> result = new List<vActivityHistory>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vActivityHistory));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vActivityHistory)helper.IDataReaderToObject(reader, new vActivityHistory()));
            }
            catch (Exception ex)
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
        #region vBudgetRealizationDt
        public static List<vBudgetRealizationDt> GetvBudgetRealizationDtList(string filterExpression)
        {
            List<vBudgetRealizationDt> result = new List<vBudgetRealizationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vBudgetRealizationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vBudgetRealizationDt)helper.IDataReaderToObject(reader, new vBudgetRealizationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vBudgetRealizationDt> GetvBudgetRealizationDtList(string filterExpression,IDbContext ctx)
        {
            List<vBudgetRealizationDt> result = new List<vBudgetRealizationDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(vBudgetRealizationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vBudgetRealizationDt)helper.IDataReaderToObject(reader, new vBudgetRealizationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<vBudgetRealizationDt> GetvBudgetRealizationDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vBudgetRealizationDt> result = new List<vBudgetRealizationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vBudgetRealizationDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vBudgetRealizationDt)helper.IDataReaderToObject(reader, new vBudgetRealizationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvBudgetRealizationDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vBudgetRealizationDt));
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
        public static Int32 GetvBudgetRealizationDtRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vBudgetRealizationDt));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "BudgetRealizationDtID", keyValue, orderByExpression);
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
        #region vBudgetRealizationHd
        public static List<vBudgetRealizationHd> GetvBudgetRealizationHdList(string filterExpression)
        {
            List<vBudgetRealizationHd> result = new List<vBudgetRealizationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vBudgetRealizationHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vBudgetRealizationHd)helper.IDataReaderToObject(reader, new vBudgetRealizationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvBudgetRealizationHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vBudgetRealizationHd));
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
        public static Int32 GetvBudgetRealizationHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vBudgetRealizationHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "BudgetRealizationNo", keyValue, orderByExpression);
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
        public static vBudgetRealizationHd GetvBudgetRealizationHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vBudgetRealizationHd> result = new List<vBudgetRealizationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vBudgetRealizationHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vBudgetRealizationHd)helper.IDataReaderToObject(reader, new vBudgetRealizationHd()));
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
        #region vBudgetRequestDt
        public static List<vBudgetRequestDt> GetvBudgetRequestDtList(string filterExpression)
        {
            List<vBudgetRequestDt> result = new List<vBudgetRequestDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vBudgetRequestDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vBudgetRequestDt)helper.IDataReaderToObject(reader, new vBudgetRequestDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vBudgetRequestDt> GetvBudgetRequestDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vBudgetRequestDt> result = new List<vBudgetRequestDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vBudgetRequestDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vBudgetRequestDt)helper.IDataReaderToObject(reader, new vBudgetRequestDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvBudgetRequestDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vBudgetRequestDt));
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
        public static Int32 GetvBudgetRequestDtRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vBudgetRequestDt));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "BudgetRequestDtID", keyValue, orderByExpression);
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
        #region vBudgetRequestHd
        public static List<vBudgetRequestHd> GetvBudgetRequestHdList(string filterExpression)
        {
            List<vBudgetRequestHd> result = new List<vBudgetRequestHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vBudgetRequestHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vBudgetRequestHd)helper.IDataReaderToObject(reader, new vBudgetRequestHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvBudgetRequestHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vBudgetRequestHd));
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
        public static Int32 GetvBudgetRequestHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vBudgetRequestHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "BudgetRequestNo", keyValue, orderByExpression);
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
        public static vBudgetRequestHd GetvBudgetRequestHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vBudgetRequestHd> result = new List<vBudgetRequestHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vBudgetRequestHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vBudgetRequestHd)helper.IDataReaderToObject(reader, new vBudgetRequestHd()));
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
        #region vMemberTask
        public static List<vMemberTask> GetvMemberTaskList(string filterExpression)
        {
            List<vMemberTask> result = new List<vMemberTask>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vMemberTask));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vMemberTask)helper.IDataReaderToObject(reader, new vMemberTask()));
            }
            catch (Exception ex)
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
        #region vTeamDt
        public static List<vTeamDt> GetvTeamDtList(string filterExpression)
        {
            List<vTeamDt> result = new List<vTeamDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeamDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTeamDt)helper.IDataReaderToObject(reader, new vTeamDt()));
            }
            catch (Exception ex)
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
        #region vTeamHd
        public static List<vTeamHd> GetvTeamHdList(string filterExpression)
        {
            List<vTeamHd> result = new List<vTeamHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeamHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTeamHd)helper.IDataReaderToObject(reader, new vTeamHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vTeamHd> GetvTeamHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vTeamHd> result = new List<vTeamHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeamHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vTeamHd)helper.IDataReaderToObject(reader, new vTeamHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        public static Int32 GetvTeamHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeamHd));
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
        public static Int32 GetvTeamHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vTeamHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "TeamID", keyValue, orderByExpression);
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
        #region vProject
        public static List<vProject> GetvProjectList(string filterExpression)
        {
            List<vProject> result = new List<vProject>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProject));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProject)helper.IDataReaderToObject(reader, new vProject()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vProject> GetvProjectList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vProject> result = new List<vProject>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProject));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProject)helper.IDataReaderToObject(reader, new vProject()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvProjectRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProject));
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
        public static Int32 GetvProjectRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProject));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ProjectID", keyValue, orderByExpression);
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
        #region vProjectBudgetDt
        public static List<vProjectBudgetDt> GetvProjectBudgetDtList(string filterExpression)
        {
            List<vProjectBudgetDt> result = new List<vProjectBudgetDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectBudgetDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProjectBudgetDt)helper.IDataReaderToObject(reader, new vProjectBudgetDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vProjectBudgetDt> GetvProjectBudgetDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vProjectBudgetDt> result = new List<vProjectBudgetDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectBudgetDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProjectBudgetDt)helper.IDataReaderToObject(reader, new vProjectBudgetDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        public static Int32 GetvProjectBudgetDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectBudgetDt));
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
        public static Int32 GetvProjectBudgetDtRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectBudgetDt));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "BudgetID", keyValue, orderByExpression);
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
        #region vProjectBudgetHd
        public static List<vProjectBudgetHd> GetvProjectBudgetHdList(string filterExpression)
        {
            List<vProjectBudgetHd> result = new List<vProjectBudgetHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectBudgetHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProjectBudgetHd)helper.IDataReaderToObject(reader, new vProjectBudgetHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vProjectBudgetHd> GetvProjectBudgetHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vProjectBudgetHd> result = new List<vProjectBudgetHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectBudgetHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProjectBudgetHd)helper.IDataReaderToObject(reader, new vProjectBudgetHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvProjectBudgetHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectBudgetHd));
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
        public static Int32 GetvProjectBudgetHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectBudgetHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "BudgetID", keyValue, orderByExpression);
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
        #region vProjectTask
        public static List<vProjectTask> GetvProjectTaskList(string filterExpression)
        {
            List<vProjectTask> result = new List<vProjectTask>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectTask));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProjectTask)helper.IDataReaderToObject(reader, new vProjectTask()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vProjectTask> GetvProjectTaskList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vProjectTask> result = new List<vProjectTask>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectTask));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProjectTask)helper.IDataReaderToObject(reader, new vProjectTask()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        public static Int32 GetvProjectTaskRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectTask));
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
        public static Int32 GetvProjectTaskRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectTask));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ProjectTaskID", keyValue, orderByExpression);
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
        #region vProjectTaskBudget
        public static List<vProjectTaskBudget> GetvProjectTaskBudgetList(string filterExpression)
        {
            List<vProjectTaskBudget> result = new List<vProjectTaskBudget>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectTaskBudget));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProjectTaskBudget)helper.IDataReaderToObject(reader, new vProjectTaskBudget()));
            }
            catch (Exception ex)
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
        #region vProjectTaskCustom
        public static List<vProjectTaskCustom> GetvProjectTaskCustomList(string filterExpression)
        {
            List<vProjectTaskCustom> result = new List<vProjectTaskCustom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectTaskCustom));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProjectTaskCustom)helper.IDataReaderToObject(reader, new vProjectTaskCustom()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vProjectTaskCustom> GetvProjectTaskCustomList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vProjectTaskCustom> result = new List<vProjectTaskCustom>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectTaskCustom));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProjectTaskCustom)helper.IDataReaderToObject(reader, new vProjectTaskCustom()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        public static Int32 GetvProjectTaskCustomRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectTaskCustom));
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
        public static Int32 GetvProjectTaskCustomRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectTaskCustom));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ProjectTaskID", keyValue, orderByExpression);
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
        #region vProjectTaskFile
        public static List<vProjectTaskFile> GetvProjectTaskFileList(string filterExpression)
        {
            List<vProjectTaskFile> result = new List<vProjectTaskFile>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectTaskFile));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProjectTaskFile)helper.IDataReaderToObject(reader, new vProjectTaskFile()));
            }
            catch (Exception ex)
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
        #region vProjectTaskLog
        public static List<vProjectTaskLog> GetvProjectTaskLogList(string filterExpression)
        {
            List<vProjectTaskLog> result = new List<vProjectTaskLog>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectTaskLog));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProjectTaskLog)helper.IDataReaderToObject(reader, new vProjectTaskLog()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vProjectTaskLog> GetvProjectTaskLogList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vProjectTaskLog> result = new List<vProjectTaskLog>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectTaskLog));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProjectTaskLog)helper.IDataReaderToObject(reader, new vProjectTaskLog()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        public static Int32 GetvProjectTaskLogRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectTaskLog));
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
        public static Int32 GetvProjectTaskLogRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectTaskLog));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ProjectTaskLogID", keyValue, orderByExpression);
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
        #region vProjectTaskStructure
        public static List<vProjectTaskStructure> GetvProjectTaskStructureList(string filterExpression)
        {
            List<vProjectTaskStructure> result = new List<vProjectTaskStructure>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProjectTaskStructure));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProjectTaskStructure)helper.IDataReaderToObject(reader, new vProjectTaskStructure()));
            }
            catch (Exception ex)
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
        #region vProposedBudgetDt
        public static List<vProposedBudgetDt> GetvProposedBudgetDtList(string filterExpression)
        {
            List<vProposedBudgetDt> result = new List<vProposedBudgetDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProposedBudgetDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProposedBudgetDt)helper.IDataReaderToObject(reader, new vProposedBudgetDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<vProposedBudgetDt> GetvProposedBudgetDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<vProposedBudgetDt> result = new List<vProposedBudgetDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProposedBudgetDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProposedBudgetDt)helper.IDataReaderToObject(reader, new vProposedBudgetDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        public static Int32 GetvProposedBudgetDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProposedBudgetDt));
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
        public static Int32 GetvProposedBudgetDtRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProposedBudgetDt));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ProposedBudgetDtID", keyValue, orderByExpression);
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
        #region vProposedBudgetHd
        public static List<vProposedBudgetHd> GetvProposedBudgetHdList(string filterExpression)
        {
            List<vProposedBudgetHd> result = new List<vProposedBudgetHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProposedBudgetHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProposedBudgetHd)helper.IDataReaderToObject(reader, new vProposedBudgetHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetvProposedBudgetHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProposedBudgetHd));
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
        public static Int32 GetvProposedBudgetHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProposedBudgetHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ProposedBudgetNo", keyValue, orderByExpression);
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
        public static vProposedBudgetHd GetvProposedBudgetHd(string filterExpression, int pageIndex, string orderByExpression = "")
        {
            List<vProposedBudgetHd> result = new List<vProposedBudgetHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vProposedBudgetHd));
                ctx.CommandText = helper.SelectByPageIndex(filterExpression, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vProposedBudgetHd)helper.IDataReaderToObject(reader, new vProposedBudgetHd()));
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
        #endregion
    }
}
