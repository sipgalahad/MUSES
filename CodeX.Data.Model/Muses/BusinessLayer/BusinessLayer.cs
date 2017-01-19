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
        #region AbsenceProposalDate
        public static AbsenceProposalDate GetAbsenceProposalDate(Int32 TransactionDtID)
        {
            return new AbsenceProposalDateDao().Get(TransactionDtID);
        }
        public static int InsertAbsenceProposalDate(AbsenceProposalDate record)
        {
            return new AbsenceProposalDateDao().Insert(record);
        }
        public static int UpdateAbsenceProposalDate(AbsenceProposalDate record)
        {
            return new AbsenceProposalDateDao().Update(record);
        }
        public static int DeleteAbsenceProposalDate(Int32 TransactionDtID)
        {
            return new AbsenceProposalDateDao().Delete(TransactionDtID);
        }
        public static List<AbsenceProposalDate> GetAbsenceProposalDateList(string filterExpression)
        {
            List<AbsenceProposalDate> result = new List<AbsenceProposalDate>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(AbsenceProposalDate));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((AbsenceProposalDate)helper.IDataReaderToObject(reader, new AbsenceProposalDate()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<AbsenceProposalDate> GetAbsenceProposalDateList(string filterExpression, IDbContext ctx)
        {
            List<AbsenceProposalDate> result = new List<AbsenceProposalDate>();
            try
            {
                DbHelper helper = new DbHelper(typeof(AbsenceProposalDate));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((AbsenceProposalDate)helper.IDataReaderToObject(reader, new AbsenceProposalDate()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetAbsenceProposalDateMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(AbsenceProposalDate));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetAbsenceProposalDateRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(AbsenceProposalDate));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<AbsenceProposalDate> GetAbsenceProposalDateList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<AbsenceProposalDate> result = new List<AbsenceProposalDate>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(AbsenceProposalDate));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((AbsenceProposalDate)helper.IDataReaderToObject(reader, new AbsenceProposalDate()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region AbsenceProposalEmployee
        public static AbsenceProposalEmployee GetAbsenceProposalEmployee(Int32 TransactionID, Int32 EmployeeID)
        {
            return new AbsenceProposalEmployeeDao().Get(TransactionID, EmployeeID);
        }
        public static int InsertAbsenceProposalEmployee(AbsenceProposalEmployee record)
        {
            return new AbsenceProposalEmployeeDao().Insert(record);
        }
        public static int UpdateAbsenceProposalEmployee(AbsenceProposalEmployee record)
        {
            return new AbsenceProposalEmployeeDao().Update(record);
        }
        public static int DeleteAbsenceProposalEmployee(Int32 TransactionID, Int32 EmployeeID)
        {
            return new AbsenceProposalEmployeeDao().Delete(TransactionID, EmployeeID);
        }
        public static List<AbsenceProposalEmployee> GetAbsenceProposalEmployeeList(string filterExpression)
        {
            List<AbsenceProposalEmployee> result = new List<AbsenceProposalEmployee>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(AbsenceProposalEmployee));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((AbsenceProposalEmployee)helper.IDataReaderToObject(reader, new AbsenceProposalEmployee()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<AbsenceProposalEmployee> GetAbsenceProposalEmployeeList(string filterExpression, IDbContext ctx)
        {
            List<AbsenceProposalEmployee> result = new List<AbsenceProposalEmployee>();
            try
            {
                DbHelper helper = new DbHelper(typeof(AbsenceProposalEmployee));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((AbsenceProposalEmployee)helper.IDataReaderToObject(reader, new AbsenceProposalEmployee()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetAbsenceProposalEmployeeMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(AbsenceProposalEmployee));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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
        #region AbsenceProposalHd
        public static AbsenceProposalHd GetAbsenceProposalHd(Int32 TransactionID)
        {
            return new AbsenceProposalHdDao().Get(TransactionID);
        }
        public static int InsertAbsenceProposalHd(AbsenceProposalHd record)
        {
            return new AbsenceProposalHdDao().Insert(record);
        }
        public static int UpdateAbsenceProposalHd(AbsenceProposalHd record)
        {
            return new AbsenceProposalHdDao().Update(record);
        }
        public static int DeleteAbsenceProposalHd(Int32 TransactionID)
        {
            return new AbsenceProposalHdDao().Delete(TransactionID);
        }
        public static List<AbsenceProposalHd> GetAbsenceProposalHdList(string filterExpression)
        {
            List<AbsenceProposalHd> result = new List<AbsenceProposalHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(AbsenceProposalHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((AbsenceProposalHd)helper.IDataReaderToObject(reader, new AbsenceProposalHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<AbsenceProposalHd> GetAbsenceProposalHdList(string filterExpression, IDbContext ctx)
        {
            List<AbsenceProposalHd> result = new List<AbsenceProposalHd>();
            try
            {
                DbHelper helper = new DbHelper(typeof(AbsenceProposalHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((AbsenceProposalHd)helper.IDataReaderToObject(reader, new AbsenceProposalHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetAbsenceProposalHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(AbsenceProposalHd));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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
        #region AdmissionFeeComp
        public static AdmissionFeeComp GetAdmissionFeeComp(Int32 AdmissionFeeCompID)
        {
            return new AdmissionFeeCompDao().Get(AdmissionFeeCompID);
        }
        public static int InsertAdmissionFeeComp(AdmissionFeeComp record)
        {
            return new AdmissionFeeCompDao().Insert(record);
        }
        public static int UpdateAdmissionFeeComp(AdmissionFeeComp record)
        {
            return new AdmissionFeeCompDao().Update(record);
        }
        public static int DeleteAdmissionFeeComp(Int32 AdmissionFeeCompID)
        {
            return new AdmissionFeeCompDao().Delete(AdmissionFeeCompID);
        }
        public static List<AdmissionFeeComp> GetAdmissionFeeCompList(string filterExpression)
        {
            List<AdmissionFeeComp> result = new List<AdmissionFeeComp>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(AdmissionFeeComp));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((AdmissionFeeComp)helper.IDataReaderToObject(reader, new AdmissionFeeComp()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region AdmissionFeeRuleDt
        public static AdmissionFeeRuleDt GetAdmissionFeeRuleDt(Int32 AdmissionFeeRuleID, Int32 PeriodAdmissionID, Int32 AdmissionFeeCompID)
        {
            return new AdmissionFeeRuleDtDao().Get(AdmissionFeeRuleID, PeriodAdmissionID, AdmissionFeeCompID);
        }
        public static int InsertAdmissionFeeRuleDt(AdmissionFeeRuleDt record)
        {
            return new AdmissionFeeRuleDtDao().Insert(record);
        }
        public static int UpdateAdmissionFeeRuleDt(AdmissionFeeRuleDt record)
        {
            return new AdmissionFeeRuleDtDao().Update(record);
        }
        public static int DeleteAdmissionFeeRuleDt(Int32 AdmissionFeeRuleID, Int32 PeriodAdmissionID, Int32 AdmissionFeeCompID)
        {
            return new AdmissionFeeRuleDtDao().Delete(AdmissionFeeRuleID, PeriodAdmissionID, AdmissionFeeCompID);
        }
        public static List<AdmissionFeeRuleDt> GetAdmissionFeeRuleDtList(string filterExpression)
        {
            List<AdmissionFeeRuleDt> result = new List<AdmissionFeeRuleDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(AdmissionFeeRuleDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((AdmissionFeeRuleDt)helper.IDataReaderToObject(reader, new AdmissionFeeRuleDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<AdmissionFeeRuleDt> GetAdmissionFeeRuleDtList(string filterExpression, IDbContext ctx)
        {
            List<AdmissionFeeRuleDt> result = new List<AdmissionFeeRuleDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(AdmissionFeeRuleDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((AdmissionFeeRuleDt)helper.IDataReaderToObject(reader, new AdmissionFeeRuleDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region AdmissionFeeRuleHd
        public static AdmissionFeeRuleHd GetAdmissionFeeRuleHd(Int32 AdmissionFeeRuleID)
        {
            return new AdmissionFeeRuleHdDao().Get(AdmissionFeeRuleID);
        }
        public static int InsertAdmissionFeeRuleHd(AdmissionFeeRuleHd record)
        {
            return new AdmissionFeeRuleHdDao().Insert(record);
        }
        public static int UpdateAdmissionFeeRuleHd(AdmissionFeeRuleHd record)
        {
            return new AdmissionFeeRuleHdDao().Update(record);
        }
        public static int DeleteAdmissionFeeRuleHd(Int32 AdmissionFeeRuleID)
        {
            return new AdmissionFeeRuleHdDao().Delete(AdmissionFeeRuleID);
        }
        public static List<AdmissionFeeRuleHd> GetAdmissionFeeRuleHdList(string filterExpression)
        {
            List<AdmissionFeeRuleHd> result = new List<AdmissionFeeRuleHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(AdmissionFeeRuleHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((AdmissionFeeRuleHd)helper.IDataReaderToObject(reader, new AdmissionFeeRuleHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetAdmissionFeeRuleHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(AdmissionFeeRuleHd));
                ctx.CommandText = helper.SelectMaxColumn("AdmissionFeeRuleID");
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
        #region AdmissionPaymentDt
        public static AdmissionPaymentDt GetAdmissionPaymentDt(Int32 PaymentID, Int32 AdmissionFeeCompID, Int16 DisplayOrder)
        {
            return new AdmissionPaymentDtDao().Get(PaymentID, AdmissionFeeCompID, DisplayOrder);
        }
        public static int InsertAdmissionPaymentDt(AdmissionPaymentDt record)
        {
            return new AdmissionPaymentDtDao().Insert(record);
        }
        public static int UpdateAdmissionPaymentDt(AdmissionPaymentDt record)
        {
            return new AdmissionPaymentDtDao().Update(record);
        }
        public static int DeleteAdmissionPaymentDt(Int32 PaymentID, Int32 AdmissionFeeCompID, Int16 DisplayOrder)
        {
            return new AdmissionPaymentDtDao().Delete(PaymentID, AdmissionFeeCompID, DisplayOrder);
        }
        public static List<AdmissionPaymentDt> GetAdmissionPaymentDtList(string filterExpression)
        {
            List<AdmissionPaymentDt> result = new List<AdmissionPaymentDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(AdmissionPaymentDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((AdmissionPaymentDt)helper.IDataReaderToObject(reader, new AdmissionPaymentDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<AdmissionPaymentDt> GetAdmissionPaymentDtList(string filterExpression, IDbContext ctx)
        {
            List<AdmissionPaymentDt> result = new List<AdmissionPaymentDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(AdmissionPaymentDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((AdmissionPaymentDt)helper.IDataReaderToObject(reader, new AdmissionPaymentDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region AdmissionPaymentHd
        public static AdmissionPaymentHd GetAdmissionPaymentHd(Int32 PaymentID)
        {
            return new AdmissionPaymentHdDao().Get(PaymentID);
        }
        public static int InsertAdmissionPaymentHd(AdmissionPaymentHd record)
        {
            return new AdmissionPaymentHdDao().Insert(record);
        }
        public static int UpdateAdmissionPaymentHd(AdmissionPaymentHd record)
        {
            return new AdmissionPaymentHdDao().Update(record);
        }
        public static int DeleteAdmissionPaymentHd(Int32 PaymentID)
        {
            return new AdmissionPaymentHdDao().Delete(PaymentID);
        }
        public static List<AdmissionPaymentHd> GetAdmissionPaymentHdList(string filterExpression)
        {
            List<AdmissionPaymentHd> result = new List<AdmissionPaymentHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(AdmissionPaymentHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((AdmissionPaymentHd)helper.IDataReaderToObject(reader, new AdmissionPaymentHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetAdmissionPaymentHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(AdmissionPaymentHd));
                ctx.CommandText = helper.SelectMaxColumn("PaymentID");
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
        #region AdmissionSelection
        public static AdmissionSelection GetAdmissionSelection(Int32 AdmissionSelectionID)
        {
            return new AdmissionSelectionDao().Get(AdmissionSelectionID);
        }
        public static int InsertAdmissionSelection(AdmissionSelection record)
        {
            return new AdmissionSelectionDao().Insert(record);
        }
        public static int UpdateAdmissionSelection(AdmissionSelection record)
        {
            return new AdmissionSelectionDao().Update(record);
        }
        public static int DeleteAdmissionSelection(Int32 AdmissionSelectionID)
        {
            return new AdmissionSelectionDao().Delete(AdmissionSelectionID);
        }
        public static List<AdmissionSelection> GetAdmissionSelectionList(string filterExpression)
        {
            List<AdmissionSelection> result = new List<AdmissionSelection>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(AdmissionSelection));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((AdmissionSelection)helper.IDataReaderToObject(reader, new AdmissionSelection()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ARBalance
        public static ARBalance GetARBalance(Int32 ID)
        {
            return new ARBalanceDao().Get(ID);
        }
        public static int InsertARBalance(ARBalance record)
        {
            return new ARBalanceDao().Insert(record);
        }
        public static int UpdateARBalance(ARBalance record)
        {
            return new ARBalanceDao().Update(record);
        }
        public static int DeleteARBalance(Int32 ID)
        {
            return new ARBalanceDao().Delete(ID);
        }
        public static List<ARBalance> GetARBalanceList(string filterExpression)
        {
            List<ARBalance> result = new List<ARBalance>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ARBalance));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ARBalance)helper.IDataReaderToObject(reader, new ARBalance()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ARBalance> GetARBalanceList(string filterExpression, IDbContext ctx)
        {
            List<ARBalance> result = new List<ARBalance>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ARBalance));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ARBalance)helper.IDataReaderToObject(reader, new ARBalance()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ARInvoiceDt
        public static ARInvoiceDt GetARInvoiceDt(Int32 ARInvoiceDtID)
        {
            return new ARInvoiceDtDao().Get(ARInvoiceDtID);
        }
        public static int InsertARInvoiceDt(ARInvoiceDt record)
        {
            return new ARInvoiceDtDao().Insert(record);
        }
        public static int UpdateARInvoiceDt(ARInvoiceDt record)
        {
            return new ARInvoiceDtDao().Update(record);
        }
        public static int DeleteARInvoiceDt(Int32 ARInvoiceDtID)
        {
            return new ARInvoiceDtDao().Delete(ARInvoiceDtID);
        }
        public static List<ARInvoiceDt> GetARInvoiceDtList(string filterExpression)
        {
            List<ARInvoiceDt> result = new List<ARInvoiceDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ARInvoiceDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ARInvoiceDt)helper.IDataReaderToObject(reader, new ARInvoiceDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ARInvoiceDt> GetARInvoiceDtList(string filterExpression, IDbContext ctx)
        {
            List<ARInvoiceDt> result = new List<ARInvoiceDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ARInvoiceDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ARInvoiceDt)helper.IDataReaderToObject(reader, new ARInvoiceDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetARInvoiceDtRowCount(string filterExpression, IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ARInvoiceDt));
                ctx.CommandText = helper.GetRowCount(filterExpression);
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
        #region ARInvoiceHd
        public static ARInvoiceHd GetARInvoiceHd(Int32 ARInvoiceID)
        {
            return new ARInvoiceHdDao().Get(ARInvoiceID);
        }
        public static int InsertARInvoiceHd(ARInvoiceHd record)
        {
            return new ARInvoiceHdDao().Insert(record);
        }
        public static int UpdateARInvoiceHd(ARInvoiceHd record)
        {
            return new ARInvoiceHdDao().Update(record);
        }
        public static int DeleteARInvoiceHd(Int32 ARInvoiceID)
        {
            return new ARInvoiceHdDao().Delete(ARInvoiceID);
        }
        public static List<ARInvoiceHd> GetARInvoiceHdList(string filterExpression)
        {
            List<ARInvoiceHd> result = new List<ARInvoiceHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ARInvoiceHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ARInvoiceHd)helper.IDataReaderToObject(reader, new ARInvoiceHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ARInvoiceHd> GetARInvoiceHdList(string filterExpression, IDbContext ctx)
        {
            List<ARInvoiceHd> result = new List<ARInvoiceHd>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ARInvoiceHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ARInvoiceHd)helper.IDataReaderToObject(reader, new ARInvoiceHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetARInvoiceHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ARInvoiceHd));
                ctx.CommandText = helper.SelectMaxColumn("ARInvoiceID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetARInvoiceHdRowCount(string filterExpression, IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ARInvoiceHd));
                ctx.CommandText = helper.GetRowCount(filterExpression);
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
        #region ARInvoiceReceiving
        public static ARInvoiceReceiving GetARInvoiceReceiving(Int32 ARReceivingID, Int32 ARInvoiceID, Int32 ARInvoiceDtID)
        {
            return new ARInvoiceReceivingDao().Get(ARReceivingID, ARInvoiceID, ARInvoiceDtID);
        }
        public static int InsertARInvoiceReceiving(ARInvoiceReceiving record)
        {
            return new ARInvoiceReceivingDao().Insert(record);
        }
        public static int UpdateARInvoiceReceiving(ARInvoiceReceiving record)
        {
            return new ARInvoiceReceivingDao().Update(record);
        }
        public static int DeleteARInvoiceReceiving(Int32 ARReceivingID, Int32 ARInvoiceID, Int32 ARInvoiceDtID)
        {
            return new ARInvoiceReceivingDao().Delete(ARReceivingID, ARInvoiceID, ARInvoiceDtID);
        }
        public static List<ARInvoiceReceiving> GetARInvoiceReceivingList(string filterExpression)
        {
            List<ARInvoiceReceiving> result = new List<ARInvoiceReceiving>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ARInvoiceReceiving));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ARInvoiceReceiving)helper.IDataReaderToObject(reader, new ARInvoiceReceiving()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ARInvoiceReceiving> GetARInvoiceReceivingList(string filterExpression, IDbContext ctx)
        {
            List<ARInvoiceReceiving> result = new List<ARInvoiceReceiving>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ARInvoiceReceiving));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ARInvoiceReceiving)helper.IDataReaderToObject(reader, new ARInvoiceReceiving()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ARReceivingDt
        public static ARReceivingDt GetARReceivingDt(Int32 ARReceivingDetailID)
        {
            return new ARReceivingDtDao().Get(ARReceivingDetailID);
        }
        public static int InsertARReceivingDt(ARReceivingDt record)
        {
            return new ARReceivingDtDao().Insert(record);
        }
        public static int UpdateARReceivingDt(ARReceivingDt record)
        {
            return new ARReceivingDtDao().Update(record);
        }
        public static int DeleteARReceivingDt(Int32 ARReceivingDetailID)
        {
            return new ARReceivingDtDao().Delete(ARReceivingDetailID);
        }
        public static List<ARReceivingDt> GetARReceivingDtList(string filterExpression)
        {
            List<ARReceivingDt> result = new List<ARReceivingDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ARReceivingDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ARReceivingDt)helper.IDataReaderToObject(reader, new ARReceivingDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ARReceivingDt> GetARReceivingDtList(string filterExpression, IDbContext ctx)
        {
            List<ARReceivingDt> result = new List<ARReceivingDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ARReceivingDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ARReceivingDt)helper.IDataReaderToObject(reader, new ARReceivingDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ARReceivingHd
        public static ARReceivingHd GetARReceivingHd(Int32 ARReceivingID)
        {
            return new ARReceivingHdDao().Get(ARReceivingID);
        }
        public static int InsertARReceivingHd(ARReceivingHd record)
        {
            return new ARReceivingHdDao().Insert(record);
        }
        public static int UpdateARReceivingHd(ARReceivingHd record)
        {
            return new ARReceivingHdDao().Update(record);
        }
        public static int DeleteARReceivingHd(Int32 ARReceivingID)
        {
            return new ARReceivingHdDao().Delete(ARReceivingID);
        }
        public static List<ARReceivingHd> GetARReceivingHdList(string filterExpression)
        {
            List<ARReceivingHd> result = new List<ARReceivingHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ARReceivingHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ARReceivingHd)helper.IDataReaderToObject(reader, new ARReceivingHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ARReceivingHd> GetARReceivingHdList(string filterExpression, IDbContext ctx)
        {
            List<ARReceivingHd> result = new List<ARReceivingHd>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ARReceivingHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ARReceivingHd)helper.IDataReaderToObject(reader, new ARReceivingHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetARReceivingHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ARReceivingHd));
                ctx.CommandText = helper.SelectMaxColumn("ARReceivingID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetARReceivingHdRowCount(string filterExpression, IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ARReceivingHd));
                ctx.CommandText = helper.GetRowCount(filterExpression);
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
        #region Bank
        public static Bank GetBank(Int32 BankID)
        {
            return new BankDao().Get(BankID);
        }
        public static int InsertBank(Bank record)
        {
            return new BankDao().Insert(record);
        }
        public static int UpdateBank(Bank record)
        {
            return new BankDao().Update(record);
        }
        public static int DeleteBank(Int32 BankID)
        {
            return new BankDao().Delete(BankID);
        }
        public static List<Bank> GetBankList(string filterExpression)
        {
            List<Bank> result = new List<Bank>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Bank));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Bank)helper.IDataReaderToObject(reader, new Bank()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetBankMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(Bank));
                ctx.CommandText = helper.SelectMaxColumn("BankID");
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
        #region BusinessPartners
        public static BusinessPartners GetBusinessPartners(Int32 BusinessPartnerID)
        {
            return new BusinessPartnersDao().Get(BusinessPartnerID);
        }
        public static int InsertBusinessPartners(BusinessPartners record)
        {
            return new BusinessPartnersDao().Insert(record);
        }
        public static int UpdateBusinessPartners(BusinessPartners record)
        {
            return new BusinessPartnersDao().Update(record);
        }
        public static int DeleteBusinessPartners(Int32 BusinessPartnerID)
        {
            return new BusinessPartnersDao().Delete(BusinessPartnerID);
        }
        public static List<BusinessPartners> GetBusinessPartnersList(string filterExpression)
        {
            List<BusinessPartners> result = new List<BusinessPartners>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(BusinessPartners));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((BusinessPartners)helper.IDataReaderToObject(reader, new BusinessPartners()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<BusinessPartners> GetBusinessPartnersList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<BusinessPartners> result = new List<BusinessPartners>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(BusinessPartners));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((BusinessPartners)helper.IDataReaderToObject(reader, new BusinessPartners()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetBusinessPartnersRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(BusinessPartners));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetBusinessPartnersRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(BusinessPartners));
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
        public static Int32 GetBusinessPartnersMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(BusinessPartners));
                ctx.CommandText = helper.SelectMaxColumn("BusinessPartnerID");
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
        #region BusinessPartnerTagField
        public static BusinessPartnerTagField GetBusinessPartnerTagField(Int32 BusinessPartnerID)
        {
            return new BusinessPartnerTagFieldDao().Get(BusinessPartnerID);
        }
        public static int InsertBusinessPartnerTagField(BusinessPartnerTagField record)
        {
            return new BusinessPartnerTagFieldDao().Insert(record);
        }
        public static int UpdateBusinessPartnerTagField(BusinessPartnerTagField record)
        {
            return new BusinessPartnerTagFieldDao().Update(record);
        }
        public static int DeleteBusinessPartnerTagField(Int32 BusinessPartnerID)
        {
            return new BusinessPartnerTagFieldDao().Delete(BusinessPartnerID);
        }
        public static List<BusinessPartnerTagField> GetBusinessPartnerTagFieldList(string filterExpression)
        {
            List<BusinessPartnerTagField> result = new List<BusinessPartnerTagField>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(BusinessPartnerTagField));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((BusinessPartnerTagField)helper.IDataReaderToObject(reader, new BusinessPartnerTagField()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ChartOfAccount
        public static ChartOfAccount GetChartOfAccount(Int32 GLAccountID)
        {
            return new ChartOfAccountDao().Get(GLAccountID);
        }
        public static int InsertChartOfAccount(ChartOfAccount record)
        {
            return new ChartOfAccountDao().Insert(record);
        }
        public static int UpdateChartOfAccount(ChartOfAccount record)
        {
            return new ChartOfAccountDao().Update(record);
        }
        public static int DeleteChartOfAccount(Int32 GLAccountID)
        {
            return new ChartOfAccountDao().Delete(GLAccountID);
        }
        public static List<ChartOfAccount> GetChartOfAccountList(string filterExpression)
        {
            List<ChartOfAccount> result = new List<ChartOfAccount>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ChartOfAccount));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ChartOfAccount)helper.IDataReaderToObject(reader, new ChartOfAccount()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ChartOfAccount> GetChartOfAccountList(string filterExpression, IDbContext ctx)
        {
            List<ChartOfAccount> result = new List<ChartOfAccount>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ChartOfAccount));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ChartOfAccount)helper.IDataReaderToObject(reader, new ChartOfAccount()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetChartOfAccountMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ChartOfAccount));
                ctx.CommandText = helper.SelectMaxColumn("GLAccountID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<ChartOfAccount> GetChartOfAccountList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<ChartOfAccount> result = new List<ChartOfAccount>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ChartOfAccount));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ChartOfAccount)helper.IDataReaderToObject(reader, new ChartOfAccount()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ClassMeeting
        public static ClassMeeting GetClassMeeting(Int32 ClassMeetingID)
        {
            return new ClassMeetingDao().Get(ClassMeetingID);
        }
        public static int InsertClassMeeting(ClassMeeting record)
        {
            return new ClassMeetingDao().Insert(record);
        }
        public static int UpdateClassMeeting(ClassMeeting record)
        {
            return new ClassMeetingDao().Update(record);
        }
        public static int DeleteClassMeeting(Int32 ClassMeetingID)
        {
            return new ClassMeetingDao().Delete(ClassMeetingID);
        }
        public static List<ClassMeeting> GetClassMeetingList(string filterExpression)
        {
            List<ClassMeeting> result = new List<ClassMeeting>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassMeeting));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassMeeting)helper.IDataReaderToObject(reader, new ClassMeeting()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetClassMeetingMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassMeeting));
                ctx.CommandText = helper.SelectMaxColumn("ClassMeetingID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetClassMeetingRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassMeeting));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ClassMeeting> GetClassMeetingList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<ClassMeeting> result = new List<ClassMeeting>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassMeeting));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassMeeting)helper.IDataReaderToObject(reader, new ClassMeeting()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ClassMeetingAttendance
        public static ClassMeetingAttendance GetClassMeetingAttendance(Int32 ClassMeetingID, Int32 StudentID)
        {
            return new ClassMeetingAttendanceDao().Get(ClassMeetingID, StudentID);
        }
        public static int InsertClassMeetingAttendance(ClassMeetingAttendance record)
        {
            return new ClassMeetingAttendanceDao().Insert(record);
        }
        public static int UpdateClassMeetingAttendance(ClassMeetingAttendance record)
        {
            return new ClassMeetingAttendanceDao().Update(record);
        }
        public static int DeleteClassMeetingAttendance(Int32 ClassMeetingID, Int32 StudentID)
        {
            return new ClassMeetingAttendanceDao().Delete(ClassMeetingID, StudentID);
        }
        public static List<ClassMeetingAttendance> GetClassMeetingAttendanceList(string filterExpression)
        {
            List<ClassMeetingAttendance> result = new List<ClassMeetingAttendance>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassMeetingAttendance));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassMeetingAttendance)helper.IDataReaderToObject(reader, new ClassMeetingAttendance()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ClassMeetingAttendance> GetClassMeetingAttendanceList(string filterExpression, IDbContext ctx)
        {
            List<ClassMeetingAttendance> result = new List<ClassMeetingAttendance>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassMeetingAttendance));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassMeetingAttendance)helper.IDataReaderToObject(reader, new ClassMeetingAttendance()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ClassMeetingIndicator
        public static ClassMeetingIndicator GetClassMeetingIndicator(Int32 ClassMeetingIndicatorID)
        {
            return new ClassMeetingIndicatorDao().Get(ClassMeetingIndicatorID);
        }
        public static int InsertClassMeetingIndicator(ClassMeetingIndicator record)
        {
            return new ClassMeetingIndicatorDao().Insert(record);
        }
        public static int UpdateClassMeetingIndicator(ClassMeetingIndicator record)
        {
            return new ClassMeetingIndicatorDao().Update(record);
        }
        public static int DeleteClassMeetingIndicator(Int32 ClassMeetingIndicatorID)
        {
            return new ClassMeetingIndicatorDao().Delete(ClassMeetingIndicatorID);
        }
        public static List<ClassMeetingIndicator> GetClassMeetingIndicatorList(string filterExpression)
        {
            List<ClassMeetingIndicator> result = new List<ClassMeetingIndicator>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassMeetingIndicator));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassMeetingIndicator)helper.IDataReaderToObject(reader, new ClassMeetingIndicator()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ClassMeetingIndicator> GetClassMeetingIndicatorList(string filterExpression, IDbContext ctx)
        {
            List<ClassMeetingIndicator> result = new List<ClassMeetingIndicator>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassMeetingIndicator));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassMeetingIndicator)helper.IDataReaderToObject(reader, new ClassMeetingIndicator()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ClassSchedule
        public static ClassSchedule GetClassSchedule(Int32 ClassScheduleID)
        {
            return new ClassScheduleDao().Get(ClassScheduleID);
        }
        public static int InsertClassSchedule(ClassSchedule record)
        {
            return new ClassScheduleDao().Insert(record);
        }
        public static int UpdateClassSchedule(ClassSchedule record)
        {
            return new ClassScheduleDao().Update(record);
        }
        public static int DeleteClassSchedule(Int32 ClassScheduleID)
        {
            return new ClassScheduleDao().Delete(ClassScheduleID);
        }
        public static List<ClassSchedule> GetClassScheduleList(string filterExpression)
        {
            List<ClassSchedule> result = new List<ClassSchedule>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassSchedule));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassSchedule)helper.IDataReaderToObject(reader, new ClassSchedule()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ClassSchedule> GetClassScheduleList(string filterExpression, IDbContext ctx)
        {
            List<ClassSchedule> result = new List<ClassSchedule>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassSchedule));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassSchedule)helper.IDataReaderToObject(reader, new ClassSchedule()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ClassStudent
        public static ClassStudent GetClassStudent(Int32 SchoolClassID, Int32 StudentID)
        {
            return new ClassStudentDao().Get(SchoolClassID, StudentID);
        }
        public static int InsertClassStudent(ClassStudent record)
        {
            return new ClassStudentDao().Insert(record);
        }
        public static int UpdateClassStudent(ClassStudent record)
        {
            return new ClassStudentDao().Update(record);
        }
        public static int DeleteClassStudent(Int32 SchoolClassID, Int32 StudentID)
        {
            return new ClassStudentDao().Delete(SchoolClassID, StudentID);
        }
        public static List<ClassStudent> GetClassStudentList(string filterExpression)
        {
            List<ClassStudent> result = new List<ClassStudent>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassStudent));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassStudent)helper.IDataReaderToObject(reader, new ClassStudent()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ClassStudent> GetClassStudentList(string filterExpression, IDbContext ctx)
        {
            List<ClassStudent> result = new List<ClassStudent>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassStudent));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassStudent)helper.IDataReaderToObject(reader, new ClassStudent()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ClassStudentAttendance
        public static ClassStudentAttendance GetClassStudentAttendance(Int32 SchoolClassID, Int32 PeriodSectionID, Int32 StudentID, String GCAttendanceStatus)
        {
            return new ClassStudentAttendanceDao().Get(SchoolClassID, PeriodSectionID, StudentID, GCAttendanceStatus);
        }
        public static int InsertClassStudentAttendance(ClassStudentAttendance record)
        {
            return new ClassStudentAttendanceDao().Insert(record);
        }
        public static int UpdateClassStudentAttendance(ClassStudentAttendance record)
        {
            return new ClassStudentAttendanceDao().Update(record);
        }
        public static int DeleteClassStudentAttendance(Int32 SchoolClassID, Int32 PeriodSectionID, Int32 StudentID, String GCAttendanceStatus)
        {
            return new ClassStudentAttendanceDao().Delete(SchoolClassID, PeriodSectionID, StudentID, GCAttendanceStatus);
        }
        public static List<ClassStudentAttendance> GetClassStudentAttendanceList(string filterExpression)
        {
            List<ClassStudentAttendance> result = new List<ClassStudentAttendance>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassStudentAttendance));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassStudentAttendance)helper.IDataReaderToObject(reader, new ClassStudentAttendance()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ClassStudentAttendance> GetClassStudentAttendanceList(string filterExpression, IDbContext ctx)
        {
            List<ClassStudentAttendance> result = new List<ClassStudentAttendance>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassStudentAttendance));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassStudentAttendance)helper.IDataReaderToObject(reader, new ClassStudentAttendance()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ClassStudentDailyAttendance
        public static ClassStudentDailyAttendance GetClassStudentDailyAttendance(Int32 SchoolClassID, Int32 PeriodSectionID, Int32 StudentID, DateTime SchoolDate)
        {
            return new ClassStudentDailyAttendanceDao().Get(SchoolClassID, PeriodSectionID, StudentID, SchoolDate);
        }
        public static int InsertClassStudentDailyAttendance(ClassStudentDailyAttendance record)
        {
            return new ClassStudentDailyAttendanceDao().Insert(record);
        }
        public static int UpdateClassStudentDailyAttendance(ClassStudentDailyAttendance record)
        {
            return new ClassStudentDailyAttendanceDao().Update(record);
        }
        public static int DeleteClassStudentDailyAttendance(Int32 SchoolClassID, Int32 PeriodSectionID, Int32 StudentID, DateTime SchoolDate)
        {
            return new ClassStudentDailyAttendanceDao().Delete(SchoolClassID, PeriodSectionID, StudentID, SchoolDate);
        }
        public static List<ClassStudentDailyAttendance> GetClassStudentDailyAttendanceList(string filterExpression)
        {
            List<ClassStudentDailyAttendance> result = new List<ClassStudentDailyAttendance>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassStudentDailyAttendance));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassStudentDailyAttendance)helper.IDataReaderToObject(reader, new ClassStudentDailyAttendance()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ClassStudentDailyAttendance> GetClassStudentDailyAttendanceList(string filterExpression, IDbContext ctx)
        {
            List<ClassStudentDailyAttendance> result = new List<ClassStudentDailyAttendance>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassStudentDailyAttendance));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassStudentDailyAttendance)helper.IDataReaderToObject(reader, new ClassStudentDailyAttendance()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ClassStudentMark
        public static ClassStudentMark GetClassStudentMark(Int32 SchoolClassID, Int32 PeriodSectionID, Int32 StudentID)
        {
            return new ClassStudentMarkDao().Get(SchoolClassID, PeriodSectionID, StudentID);
        }
        public static int InsertClassStudentMark(ClassStudentMark record)
        {
            return new ClassStudentMarkDao().Insert(record);
        }
        public static int UpdateClassStudentMark(ClassStudentMark record)
        {
            return new ClassStudentMarkDao().Update(record);
        }
        public static int DeleteClassStudentMark(Int32 SchoolClassID, Int32 PeriodSectionID, Int32 StudentID)
        {
            return new ClassStudentMarkDao().Delete(SchoolClassID, PeriodSectionID, StudentID);
        }
        public static List<ClassStudentMark> GetClassStudentMarkList(string filterExpression)
        {
            List<ClassStudentMark> result = new List<ClassStudentMark>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassStudentMark));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassStudentMark)helper.IDataReaderToObject(reader, new ClassStudentMark()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ClassStudentMark> GetClassStudentMarkList(string filterExpression, IDbContext ctx)
        {
            List<ClassStudentMark> result = new List<ClassStudentMark>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassStudentMark));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassStudentMark)helper.IDataReaderToObject(reader, new ClassStudentMark()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ClassStudentSubjectIndicatorMark
        public static ClassStudentSubjectIndicatorMark GetClassStudentSubjectIndicatorMark(Int32 ClassSubjectID, Int32 StudentID, Int32 PeriodSectionID, Int32 CurriculumMarkTypeID, String SubjectIndicatorName)
        {
            return new ClassStudentSubjectIndicatorMarkDao().Get(ClassSubjectID, StudentID, PeriodSectionID, CurriculumMarkTypeID, SubjectIndicatorName);
        }
        public static int InsertClassStudentSubjectIndicatorMark(ClassStudentSubjectIndicatorMark record)
        {
            return new ClassStudentSubjectIndicatorMarkDao().Insert(record);
        }
        public static int UpdateClassStudentSubjectIndicatorMark(ClassStudentSubjectIndicatorMark record)
        {
            return new ClassStudentSubjectIndicatorMarkDao().Update(record);
        }
        public static int DeleteClassStudentSubjectIndicatorMark(Int32 ClassSubjectID, Int32 StudentID, Int32 PeriodSectionID, Int32 CurriculumMarkTypeID, String SubjectIndicatorName)
        {
            return new ClassStudentSubjectIndicatorMarkDao().Delete(ClassSubjectID, StudentID, PeriodSectionID, CurriculumMarkTypeID, SubjectIndicatorName);
        }
        public static List<ClassStudentSubjectIndicatorMark> GetClassStudentSubjectIndicatorMarkList(string filterExpression)
        {
            List<ClassStudentSubjectIndicatorMark> result = new List<ClassStudentSubjectIndicatorMark>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassStudentSubjectIndicatorMark));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassStudentSubjectIndicatorMark)helper.IDataReaderToObject(reader, new ClassStudentSubjectIndicatorMark()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ClassStudentSubjectIndicatorMark> GetClassStudentSubjectIndicatorMarkList(string filterExpression, IDbContext ctx)
        {
            List<ClassStudentSubjectIndicatorMark> result = new List<ClassStudentSubjectIndicatorMark>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassStudentSubjectIndicatorMark));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassStudentSubjectIndicatorMark)helper.IDataReaderToObject(reader, new ClassStudentSubjectIndicatorMark()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ClassStudentSubjectMark
        public static ClassStudentSubjectMark GetClassStudentSubjectMark(Int32 ClassSubjectID, Int32 StudentID, Int32 PeriodSectionID, Int32 CurriculumMarkTypeID)
        {
            return new ClassStudentSubjectMarkDao().Get(ClassSubjectID, StudentID, PeriodSectionID, CurriculumMarkTypeID);
        }
        public static int InsertClassStudentSubjectMark(ClassStudentSubjectMark record)
        {
            return new ClassStudentSubjectMarkDao().Insert(record);
        }
        public static int UpdateClassStudentSubjectMark(ClassStudentSubjectMark record)
        {
            return new ClassStudentSubjectMarkDao().Update(record);
        }
        public static int DeleteClassStudentSubjectMark(Int32 ClassSubjectID, Int32 StudentID, Int32 PeriodSectionID, Int32 CurriculumMarkTypeID)
        {
            return new ClassStudentSubjectMarkDao().Delete(ClassSubjectID, StudentID, PeriodSectionID, CurriculumMarkTypeID);
        }
        public static List<ClassStudentSubjectMark> GetClassStudentSubjectMarkList(string filterExpression)
        {
            List<ClassStudentSubjectMark> result = new List<ClassStudentSubjectMark>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassStudentSubjectMark));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassStudentSubjectMark)helper.IDataReaderToObject(reader, new ClassStudentSubjectMark()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ClassStudentSubjectMark> GetClassStudentSubjectMarkList(string filterExpression, IDbContext ctx)
        {
            List<ClassStudentSubjectMark> result = new List<ClassStudentSubjectMark>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassStudentSubjectMark));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassStudentSubjectMark)helper.IDataReaderToObject(reader, new ClassStudentSubjectMark()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ClassStudentSubjectTaskGroupMark
        public static ClassStudentSubjectTaskGroupMark GetClassStudentSubjectTaskGroupMark(Int32 ClassSubjectID, Int32 PeriodSectionID, Int32 CurriculumFinalMarkFormulaDtID, Int32 StudentID)
        {
            return new ClassStudentSubjectTaskGroupMarkDao().Get(ClassSubjectID, PeriodSectionID, CurriculumFinalMarkFormulaDtID, StudentID);
        }
        public static int InsertClassStudentSubjectTaskGroupMark(ClassStudentSubjectTaskGroupMark record)
        {
            return new ClassStudentSubjectTaskGroupMarkDao().Insert(record);
        }
        public static int UpdateClassStudentSubjectTaskGroupMark(ClassStudentSubjectTaskGroupMark record)
        {
            return new ClassStudentSubjectTaskGroupMarkDao().Update(record);
        }
        public static int DeleteClassStudentSubjectTaskGroupMark(Int32 ClassSubjectID, Int32 PeriodSectionID, Int32 CurriculumFinalMarkFormulaDtID, Int32 StudentID)
        {
            return new ClassStudentSubjectTaskGroupMarkDao().Delete(ClassSubjectID, PeriodSectionID, CurriculumFinalMarkFormulaDtID, StudentID);
        }
        public static List<ClassStudentSubjectTaskGroupMark> GetClassStudentSubjectTaskGroupMarkList(string filterExpression)
        {
            List<ClassStudentSubjectTaskGroupMark> result = new List<ClassStudentSubjectTaskGroupMark>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassStudentSubjectTaskGroupMark));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassStudentSubjectTaskGroupMark)helper.IDataReaderToObject(reader, new ClassStudentSubjectTaskGroupMark()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ClassStudentSubjectTaskGroupMark> GetClassStudentSubjectTaskGroupMarkList(string filterExpression, IDbContext ctx)
        {
            List<ClassStudentSubjectTaskGroupMark> result = new List<ClassStudentSubjectTaskGroupMark>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassStudentSubjectTaskGroupMark));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassStudentSubjectTaskGroupMark)helper.IDataReaderToObject(reader, new ClassStudentSubjectTaskGroupMark()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ClassStudentSubjectTaskMark
        public static ClassStudentSubjectTaskMark GetClassStudentSubjectTaskMark(Int32 ClassSubjectTaskID, Int32 StudentID)
        {
            return new ClassStudentSubjectTaskMarkDao().Get(ClassSubjectTaskID, StudentID);
        }
        public static int InsertClassStudentSubjectTaskMark(ClassStudentSubjectTaskMark record)
        {
            return new ClassStudentSubjectTaskMarkDao().Insert(record);
        }
        public static int UpdateClassStudentSubjectTaskMark(ClassStudentSubjectTaskMark record)
        {
            return new ClassStudentSubjectTaskMarkDao().Update(record);
        }
        public static int DeleteClassStudentSubjectTaskMark(Int32 ClassSubjectTaskID, Int32 StudentID)
        {
            return new ClassStudentSubjectTaskMarkDao().Delete(ClassSubjectTaskID, StudentID);
        }
        public static List<ClassStudentSubjectTaskMark> GetClassStudentSubjectTaskMarkList(string filterExpression)
        {
            List<ClassStudentSubjectTaskMark> result = new List<ClassStudentSubjectTaskMark>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassStudentSubjectTaskMark));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassStudentSubjectTaskMark)helper.IDataReaderToObject(reader, new ClassStudentSubjectTaskMark()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ClassStudentSubjectTaskMark> GetClassStudentSubjectTaskMarkList(string filterExpression, IDbContext ctx)
        {
            List<ClassStudentSubjectTaskMark> result = new List<ClassStudentSubjectTaskMark>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassStudentSubjectTaskMark));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassStudentSubjectTaskMark)helper.IDataReaderToObject(reader, new ClassStudentSubjectTaskMark()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ClassStudentSubjectTaskRemedialMark
        public static ClassStudentSubjectTaskRemedialMark GetClassStudentSubjectTaskRemedialMark(Int32 ClassSubjectTaskID, Int32 StudentID)
        {
            return new ClassStudentSubjectTaskRemedialMarkDao().Get(ClassSubjectTaskID, StudentID);
        }
        public static int InsertClassStudentSubjectTaskRemedialMark(ClassStudentSubjectTaskRemedialMark record)
        {
            return new ClassStudentSubjectTaskRemedialMarkDao().Insert(record);
        }
        public static int UpdateClassStudentSubjectTaskRemedialMark(ClassStudentSubjectTaskRemedialMark record)
        {
            return new ClassStudentSubjectTaskRemedialMarkDao().Update(record);
        }
        public static int DeleteClassStudentSubjectTaskRemedialMark(Int32 ClassSubjectTaskID, Int32 StudentID)
        {
            return new ClassStudentSubjectTaskRemedialMarkDao().Delete(ClassSubjectTaskID, StudentID);
        }
        public static List<ClassStudentSubjectTaskRemedialMark> GetClassStudentSubjectTaskRemedialMarkList(string filterExpression)
        {
            List<ClassStudentSubjectTaskRemedialMark> result = new List<ClassStudentSubjectTaskRemedialMark>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassStudentSubjectTaskRemedialMark));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassStudentSubjectTaskRemedialMark)helper.IDataReaderToObject(reader, new ClassStudentSubjectTaskRemedialMark()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ClassStudentSubjectTaskRemedialMark> GetClassStudentSubjectTaskRemedialMarkList(string filterExpression, IDbContext ctx)
        {
            List<ClassStudentSubjectTaskRemedialMark> result = new List<ClassStudentSubjectTaskRemedialMark>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassStudentSubjectTaskRemedialMark));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassStudentSubjectTaskRemedialMark)helper.IDataReaderToObject(reader, new ClassStudentSubjectTaskRemedialMark()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ClassSubject
        public static ClassSubject GetClassSubject(Int32 ClassSubjectID)
        {
            return new ClassSubjectDao().Get(ClassSubjectID);
        }
        public static int InsertClassSubject(ClassSubject record)
        {
            return new ClassSubjectDao().Insert(record);
        }
        public static int UpdateClassSubject(ClassSubject record)
        {
            return new ClassSubjectDao().Update(record);
        }
        public static int DeleteClassSubject(Int32 ClassSubjectID)
        {
            return new ClassSubjectDao().Delete(ClassSubjectID);
        }
        public static List<ClassSubject> GetClassSubjectList(string filterExpression)
        {
            List<ClassSubject> result = new List<ClassSubject>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassSubject));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassSubject)helper.IDataReaderToObject(reader, new ClassSubject()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ClassSubject> GetClassSubjectList(string filterExpression, IDbContext ctx)
        {
            List<ClassSubject> result = new List<ClassSubject>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassSubject));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassSubject)helper.IDataReaderToObject(reader, new ClassSubject()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetClassSubjectMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassSubject));
                ctx.CommandText = helper.SelectMaxColumn("ClassSubjectID");
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
        #region ClassSubjectSection
        public static ClassSubjectSection GetClassSubjectSection(Int32 ClassSubjectID, Int32 PeriodSectionID)
        {
            return new ClassSubjectSectionDao().Get(ClassSubjectID, PeriodSectionID);
        }
        public static int InsertClassSubjectSection(ClassSubjectSection record)
        {
            return new ClassSubjectSectionDao().Insert(record);
        }
        public static int UpdateClassSubjectSection(ClassSubjectSection record)
        {
            return new ClassSubjectSectionDao().Update(record);
        }
        public static int DeleteClassSubjectSection(Int32 ClassSubjectID, Int32 PeriodSectionID)
        {
            return new ClassSubjectSectionDao().Delete(ClassSubjectID, PeriodSectionID);
        }
        public static List<ClassSubjectSection> GetClassSubjectSectionList(string filterExpression)
        {
            List<ClassSubjectSection> result = new List<ClassSubjectSection>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassSubjectSection));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassSubjectSection)helper.IDataReaderToObject(reader, new ClassSubjectSection()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ClassSubjectSection> GetClassSubjectSectionList(string filterExpression, IDbContext ctx)
        {
            List<ClassSubjectSection> result = new List<ClassSubjectSection>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassSubjectSection));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassSubjectSection)helper.IDataReaderToObject(reader, new ClassSubjectSection()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ClassSubjectTask
        public static ClassSubjectTask GetClassSubjectTask(Int32 ClassSubjectTaskID)
        {
            return new ClassSubjectTaskDao().Get(ClassSubjectTaskID);
        }
        public static int InsertClassSubjectTask(ClassSubjectTask record)
        {
            return new ClassSubjectTaskDao().Insert(record);
        }
        public static int UpdateClassSubjectTask(ClassSubjectTask record)
        {
            return new ClassSubjectTaskDao().Update(record);
        }
        public static int DeleteClassSubjectTask(Int32 ClassSubjectTaskID)
        {
            return new ClassSubjectTaskDao().Delete(ClassSubjectTaskID);
        }
        public static List<ClassSubjectTask> GetClassSubjectTaskList(string filterExpression)
        {
            List<ClassSubjectTask> result = new List<ClassSubjectTask>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassSubjectTask));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassSubjectTask)helper.IDataReaderToObject(reader, new ClassSubjectTask()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ClassSubjectTask> GetClassSubjectTaskList(string filterExpression, IDbContext ctx)
        {
            List<ClassSubjectTask> result = new List<ClassSubjectTask>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassSubjectTask));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassSubjectTask)helper.IDataReaderToObject(reader, new ClassSubjectTask()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetClassSubjectTaskMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassSubjectTask));
                ctx.CommandText = helper.SelectMaxColumn("ClassSubjectTaskID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static String GetClassSubjectTaskMaxCode(IDbContext ctx, string filterExpression)
        {
            String result = "";
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassSubjectTask));
                ctx.CommandText = helper.SelectMaxColumn("ClassTaskCode", filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                if (row != null)
                    result = row.ItemArray.GetValue(0).ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ClassSubjectTaskIndicator
        public static ClassSubjectTaskIndicator GetClassSubjectTaskIndicator(Int32 ClassSubjectTaskIndicatorID)
        {
            return new ClassSubjectTaskIndicatorDao().Get(ClassSubjectTaskIndicatorID);
        }
        public static int InsertClassSubjectTaskIndicator(ClassSubjectTaskIndicator record)
        {
            return new ClassSubjectTaskIndicatorDao().Insert(record);
        }
        public static int UpdateClassSubjectTaskIndicator(ClassSubjectTaskIndicator record)
        {
            return new ClassSubjectTaskIndicatorDao().Update(record);
        }
        public static int DeleteClassSubjectTaskIndicator(Int32 ClassSubjectTaskIndicatorID)
        {
            return new ClassSubjectTaskIndicatorDao().Delete(ClassSubjectTaskIndicatorID);
        }
        public static List<ClassSubjectTaskIndicator> GetClassSubjectTaskIndicatorList(string filterExpression)
        {
            List<ClassSubjectTaskIndicator> result = new List<ClassSubjectTaskIndicator>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassSubjectTaskIndicator));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassSubjectTaskIndicator)helper.IDataReaderToObject(reader, new ClassSubjectTaskIndicator()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ClassSubjectTaskIndicator> GetClassSubjectTaskIndicatorList(string filterExpression, IDbContext ctx)
        {
            List<ClassSubjectTaskIndicator> result = new List<ClassSubjectTaskIndicator>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassSubjectTaskIndicator));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassSubjectTaskIndicator)helper.IDataReaderToObject(reader, new ClassSubjectTaskIndicator()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ClassSubjectTaskRemedial
        public static ClassSubjectTaskRemedial GetClassSubjectTaskRemedial(Int32 ClassSubjectTaskRemedialID)
        {
            return new ClassSubjectTaskRemedialDao().Get(ClassSubjectTaskRemedialID);
        }
        public static int InsertClassSubjectTaskRemedial(ClassSubjectTaskRemedial record)
        {
            return new ClassSubjectTaskRemedialDao().Insert(record);
        }
        public static int UpdateClassSubjectTaskRemedial(ClassSubjectTaskRemedial record)
        {
            return new ClassSubjectTaskRemedialDao().Update(record);
        }
        public static int DeleteClassSubjectTaskRemedial(Int32 ClassSubjectTaskRemedialID)
        {
            return new ClassSubjectTaskRemedialDao().Delete(ClassSubjectTaskRemedialID);
        }
        public static List<ClassSubjectTaskRemedial> GetClassSubjectTaskRemedialList(string filterExpression)
        {
            List<ClassSubjectTaskRemedial> result = new List<ClassSubjectTaskRemedial>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassSubjectTaskRemedial));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassSubjectTaskRemedial)helper.IDataReaderToObject(reader, new ClassSubjectTaskRemedial()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ClassType
        public static ClassType GetClassType(Int32 ClassTypeID)
        {
            return new ClassTypeDao().Get(ClassTypeID);
        }
        public static int InsertClassType(ClassType record)
        {
            return new ClassTypeDao().Insert(record);
        }
        public static int UpdateClassType(ClassType record)
        {
            return new ClassTypeDao().Update(record);
        }
        public static int DeleteClassType(Int32 ClassTypeID)
        {
            return new ClassTypeDao().Delete(ClassTypeID);
        }
        public static List<ClassType> GetClassTypeList(string filterExpression)
        {
            List<ClassType> result = new List<ClassType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassType)helper.IDataReaderToObject(reader, new ClassType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetClassTypeRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassType));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ClassType> GetClassTypeList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<ClassType> result = new List<ClassType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassType));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ClassType)helper.IDataReaderToObject(reader, new ClassType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetClassTypeRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassType));
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
        public static Int32 GetClassTypeMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ClassType));
                ctx.CommandText = helper.SelectMaxColumn("ClassTypeID");
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
        #region COABudget
        public static COABudget GetCOABudget(Int32 ID)
        {
            return new COABudgetDao().Get(ID);
        }
        public static int InsertCOABudget(COABudget record)
        {
            return new COABudgetDao().Insert(record);
        }
        public static int UpdateCOABudget(COABudget record)
        {
            return new COABudgetDao().Update(record);
        }
        public static int DeleteCOABudget(Int32 ID)
        {
            return new COABudgetDao().Delete(ID);
        }
        public static List<COABudget> GetCOABudgetList(string filterExpression)
        {
            List<COABudget> result = new List<COABudget>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(COABudget));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((COABudget)helper.IDataReaderToObject(reader, new COABudget()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<COABudget> GetCOABudgetList(string filterExpression, IDbContext ctx)
        {
            List<COABudget> result = new List<COABudget>();
            try
            {
                DbHelper helper = new DbHelper(typeof(COABudget));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((COABudget)helper.IDataReaderToObject(reader, new COABudget()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region COAGroup
        public static COAGroup GetCOAGroup(Int32 COAGroupID)
        {
            return new COAGroupDao().Get(COAGroupID);
        }
        public static int InsertCOAGroup(COAGroup record)
        {
            return new COAGroupDao().Insert(record);
        }
        public static int UpdateCOAGroup(COAGroup record)
        {
            return new COAGroupDao().Update(record);
        }
        public static int DeleteCOAGroup(Int32 COAGroupID)
        {
            return new COAGroupDao().Delete(COAGroupID);
        }
        public static List<COAGroup> GetCOAGroupList(string filterExpression)
        {
            List<COAGroup> result = new List<COAGroup>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(COAGroup));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((COAGroup)helper.IDataReaderToObject(reader, new COAGroup()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetCOAGroupMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(COAGroup));
                ctx.CommandText = helper.SelectMaxColumn("COAGroupID");
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
        #region CoverageType
        public static CoverageType GetCoverageType(Int32 CoverageTypeID)
        {
            return new CoverageTypeDao().Get(CoverageTypeID);
        }
        public static int InsertCoverageType(CoverageType record)
        {
            return new CoverageTypeDao().Insert(record);
        }
        public static int UpdateCoverageType(CoverageType record)
        {
            return new CoverageTypeDao().Update(record);
        }
        public static int DeleteCoverageType(Int32 CoverageTypeID)
        {
            return new CoverageTypeDao().Delete(CoverageTypeID);
        }
        public static List<CoverageType> GetCoverageTypeList(string filterExpression)
        {
            List<CoverageType> result = new List<CoverageType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CoverageType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CoverageType)helper.IDataReaderToObject(reader, new CoverageType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<CoverageType> GetCoverageTypeList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<CoverageType> result = new List<CoverageType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CoverageType));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CoverageType)helper.IDataReaderToObject(reader, new CoverageType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetCoverageTypeRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CoverageType));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetCoverageTypeRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CoverageType));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "CoverageTypeID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetCoverageTypeMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(CoverageType));
                ctx.CommandText = helper.SelectMaxColumn("CoverageTypeID");
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
        #region CoverageTypeDt
        public static CoverageTypeDt GetCoverageTypeDt(Int32 CoverageTypeDtID)
        {
            return new CoverageTypeDtDao().Get(CoverageTypeDtID);
        }
        public static int InsertCoverageTypeDt(CoverageTypeDt record)
        {
            return new CoverageTypeDtDao().Insert(record);
        }
        public static int UpdateCoverageTypeDt(CoverageTypeDt record)
        {
            return new CoverageTypeDtDao().Update(record);
        }
        public static int DeleteCoverageTypeDt(Int32 CoverageTypeDtID)
        {
            return new CoverageTypeDtDao().Delete(CoverageTypeDtID);
        }
        public static List<CoverageTypeDt> GetCoverageTypeDtList(string filterExpression)
        {
            List<CoverageTypeDt> result = new List<CoverageTypeDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CoverageTypeDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CoverageTypeDt)helper.IDataReaderToObject(reader, new CoverageTypeDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetCoverageTypeDtMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(CoverageTypeDt));
                ctx.CommandText = helper.SelectMaxColumn("CoverageTypeDtID");
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
        #region CoverageTypeDtClassType
        public static CoverageTypeDtClassType GetCoverageTypeDtClassType(Int32 CoverageTypeDtID, Int32 ClassTypeID)
        {
            return new CoverageTypeDtClassTypeDao().Get(CoverageTypeDtID, ClassTypeID);
        }
        public static int InsertCoverageTypeDtClassType(CoverageTypeDtClassType record)
        {
            return new CoverageTypeDtClassTypeDao().Insert(record);
        }
        public static int UpdateCoverageTypeDtClassType(CoverageTypeDtClassType record)
        {
            return new CoverageTypeDtClassTypeDao().Update(record);
        }
        public static int DeleteCoverageTypeDtClassType(Int32 CoverageTypeDtID, Int32 ClassTypeID)
        {
            return new CoverageTypeDtClassTypeDao().Delete(CoverageTypeDtID, ClassTypeID);
        }
        public static List<CoverageTypeDtClassType> GetCoverageTypeDtClassTypeList(string filterExpression)
        {
            List<CoverageTypeDtClassType> result = new List<CoverageTypeDtClassType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CoverageTypeDtClassType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CoverageTypeDtClassType)helper.IDataReaderToObject(reader, new CoverageTypeDtClassType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<CoverageTypeDtClassType> GetCoverageTypeDtClassTypeList(string filterExpression, IDbContext ctx)
        {
            List<CoverageTypeDtClassType> result = new List<CoverageTypeDtClassType>();
            try
            {
                DbHelper helper = new DbHelper(typeof(CoverageTypeDtClassType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CoverageTypeDtClassType)helper.IDataReaderToObject(reader, new CoverageTypeDtClassType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region CoverageTypeDtComp
        public static CoverageTypeDtComp GetCoverageTypeDtComp(Int32 CoverageTypeDtID, Int32 StudentFeeCompTypeID)
        {
            return new CoverageTypeDtCompDao().Get(CoverageTypeDtID, StudentFeeCompTypeID);
        }
        public static int InsertCoverageTypeDtComp(CoverageTypeDtComp record)
        {
            return new CoverageTypeDtCompDao().Insert(record);
        }
        public static int UpdateCoverageTypeDtComp(CoverageTypeDtComp record)
        {
            return new CoverageTypeDtCompDao().Update(record);
        }
        public static int DeleteCoverageTypeDtComp(Int32 CoverageTypeDtID, Int32 StudentFeeCompTypeID)
        {
            return new CoverageTypeDtCompDao().Delete(CoverageTypeDtID, StudentFeeCompTypeID);
        }
        public static List<CoverageTypeDtComp> GetCoverageTypeDtCompList(string filterExpression)
        {
            List<CoverageTypeDtComp> result = new List<CoverageTypeDtComp>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CoverageTypeDtComp));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CoverageTypeDtComp)helper.IDataReaderToObject(reader, new CoverageTypeDtComp()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<CoverageTypeDtComp> GetCoverageTypeDtCompList(string filterExpression, IDbContext ctx)
        {
            List<CoverageTypeDtComp> result = new List<CoverageTypeDtComp>();
            try
            {
                DbHelper helper = new DbHelper(typeof(CoverageTypeDtComp));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CoverageTypeDtComp)helper.IDataReaderToObject(reader, new CoverageTypeDtComp()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region CreditCard
        public static CreditCard GetCreditCard(Int32 CreditCardID)
        {
            return new CreditCardDao().Get(CreditCardID);
        }
        public static int InsertCreditCard(CreditCard record)
        {
            return new CreditCardDao().Insert(record);
        }
        public static int UpdateCreditCard(CreditCard record)
        {
            return new CreditCardDao().Update(record);
        }
        public static int DeleteCreditCard(Int32 CreditCardID)
        {
            return new CreditCardDao().Delete(CreditCardID);
        }
        public static List<CreditCard> GetCreditCardList(string filterExpression)
        {
            List<CreditCard> result = new List<CreditCard>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CreditCard));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CreditCard)helper.IDataReaderToObject(reader, new CreditCard()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetCreditCardMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(CreditCard));
                ctx.CommandText = helper.SelectMaxColumn("CreditCardID");
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
        #region Curriculum
        public static Curriculum GetCurriculum(Int32 CurriculumID)
        {
            return new CurriculumDao().Get(CurriculumID);
        }
        public static int InsertCurriculum(Curriculum record)
        {
            return new CurriculumDao().Insert(record);
        }
        public static int UpdateCurriculum(Curriculum record)
        {
            return new CurriculumDao().Update(record);
        }
        public static int DeleteCurriculum(Int32 CurriculumID)
        {
            return new CurriculumDao().Delete(CurriculumID);
        }
        public static List<Curriculum> GetCurriculumList(string filterExpression)
        {
            List<Curriculum> result = new List<Curriculum>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Curriculum));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Curriculum)helper.IDataReaderToObject(reader, new Curriculum()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<Curriculum> GetCurriculumList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<Curriculum> result = new List<Curriculum>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Curriculum));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Curriculum)helper.IDataReaderToObject(reader, new Curriculum()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetCurriculumRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Curriculum));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetCurriculumRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Curriculum));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "CurriculumID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetCurriculumMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(Curriculum));
                ctx.CommandText = helper.SelectMaxColumn("CurriculumID");
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
        #region CurriculumClassType
        public static CurriculumClassType GetCurriculumClassType(Int32 CurriculumClassTypeID)
        {
            return new CurriculumClassTypeDao().Get(CurriculumClassTypeID);
        }
        public static int InsertCurriculumClassType(CurriculumClassType record)
        {
            return new CurriculumClassTypeDao().Insert(record);
        }
        public static int UpdateCurriculumClassType(CurriculumClassType record)
        {
            return new CurriculumClassTypeDao().Update(record);
        }
        public static int DeleteCurriculumClassType(Int32 CurriculumClassTypeID)
        {
            return new CurriculumClassTypeDao().Delete(CurriculumClassTypeID);
        }
        public static List<CurriculumClassType> GetCurriculumClassTypeList(string filterExpression)
        {
            List<CurriculumClassType> result = new List<CurriculumClassType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumClassType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumClassType)helper.IDataReaderToObject(reader, new CurriculumClassType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<CurriculumClassType> GetCurriculumClassTypeList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<CurriculumClassType> result = new List<CurriculumClassType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumClassType));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumClassType)helper.IDataReaderToObject(reader, new CurriculumClassType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetCurriculumClassTypeRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumClassType));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region CurriculumClassTypeExtracurricular
        public static CurriculumClassTypeExtracurricular GetCurriculumClassTypeExtracurricular(Int32 CurriculumClassTypeID, Int32 ExtracurricularCurriculumClassTypeID)
        {
            return new CurriculumClassTypeExtracurricularDao().Get(CurriculumClassTypeID, ExtracurricularCurriculumClassTypeID);
        }
        public static int InsertCurriculumClassTypeExtracurricular(CurriculumClassTypeExtracurricular record)
        {
            return new CurriculumClassTypeExtracurricularDao().Insert(record);
        }
        public static int UpdateCurriculumClassTypeExtracurricular(CurriculumClassTypeExtracurricular record)
        {
            return new CurriculumClassTypeExtracurricularDao().Update(record);
        }
        public static int DeleteCurriculumClassTypeExtracurricular(Int32 CurriculumClassTypeID, Int32 ExtracurricularCurriculumClassTypeID)
        {
            return new CurriculumClassTypeExtracurricularDao().Delete(CurriculumClassTypeID, ExtracurricularCurriculumClassTypeID);
        }
        public static List<CurriculumClassTypeExtracurricular> GetCurriculumClassTypeExtracurricularList(string filterExpression)
        {
            List<CurriculumClassTypeExtracurricular> result = new List<CurriculumClassTypeExtracurricular>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumClassTypeExtracurricular));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumClassTypeExtracurricular)helper.IDataReaderToObject(reader, new CurriculumClassTypeExtracurricular()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<CurriculumClassTypeExtracurricular> GetCurriculumClassTypeExtracurricularList(string filterExpression, IDbContext ctx)
        {
            List<CurriculumClassTypeExtracurricular> result = new List<CurriculumClassTypeExtracurricular>();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumClassTypeExtracurricular));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumClassTypeExtracurricular)helper.IDataReaderToObject(reader, new CurriculumClassTypeExtracurricular()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region CurriculumFinalMarkFormulaDt
        public static CurriculumFinalMarkFormulaDt GetCurriculumFinalMarkFormulaDt(Int32 CurriculumFinalMarkFormulaDtID)
        {
            return new CurriculumFinalMarkFormulaDtDao().Get(CurriculumFinalMarkFormulaDtID);
        }
        public static int InsertCurriculumFinalMarkFormulaDt(CurriculumFinalMarkFormulaDt record)
        {
            return new CurriculumFinalMarkFormulaDtDao().Insert(record);
        }
        public static int UpdateCurriculumFinalMarkFormulaDt(CurriculumFinalMarkFormulaDt record)
        {
            return new CurriculumFinalMarkFormulaDtDao().Update(record);
        }
        public static int DeleteCurriculumFinalMarkFormulaDt(Int32 CurriculumFinalMarkFormulaDtID)
        {
            return new CurriculumFinalMarkFormulaDtDao().Delete(CurriculumFinalMarkFormulaDtID);
        }
        public static List<CurriculumFinalMarkFormulaDt> GetCurriculumFinalMarkFormulaDtList(string filterExpression)
        {
            List<CurriculumFinalMarkFormulaDt> result = new List<CurriculumFinalMarkFormulaDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumFinalMarkFormulaDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumFinalMarkFormulaDt)helper.IDataReaderToObject(reader, new CurriculumFinalMarkFormulaDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetCurriculumFinalMarkFormulaDtMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumFinalMarkFormulaDt));
                ctx.CommandText = helper.SelectMaxColumn("CurriculumFinalMarkFormulaDtID");
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
        #region CurriculumFinalMarkFormulaDtMarkType
        public static CurriculumFinalMarkFormulaDtMarkType GetCurriculumFinalMarkFormulaDtMarkType(Int32 CurriculumFinalMarkFormulaDtID, Int32 CurriculumMarkTypeDtID)
        {
            return new CurriculumFinalMarkFormulaDtMarkTypeDao().Get(CurriculumFinalMarkFormulaDtID, CurriculumMarkTypeDtID);
        }
        public static int InsertCurriculumFinalMarkFormulaDtMarkType(CurriculumFinalMarkFormulaDtMarkType record)
        {
            return new CurriculumFinalMarkFormulaDtMarkTypeDao().Insert(record);
        }
        public static int UpdateCurriculumFinalMarkFormulaDtMarkType(CurriculumFinalMarkFormulaDtMarkType record)
        {
            return new CurriculumFinalMarkFormulaDtMarkTypeDao().Update(record);
        }
        public static int DeleteCurriculumFinalMarkFormulaDtMarkType(Int32 CurriculumFinalMarkFormulaDtID, Int32 CurriculumMarkTypeDtID)
        {
            return new CurriculumFinalMarkFormulaDtMarkTypeDao().Delete(CurriculumFinalMarkFormulaDtID, CurriculumMarkTypeDtID);
        }
        public static List<CurriculumFinalMarkFormulaDtMarkType> GetCurriculumFinalMarkFormulaDtMarkTypeList(string filterExpression)
        {
            List<CurriculumFinalMarkFormulaDtMarkType> result = new List<CurriculumFinalMarkFormulaDtMarkType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumFinalMarkFormulaDtMarkType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumFinalMarkFormulaDtMarkType)helper.IDataReaderToObject(reader, new CurriculumFinalMarkFormulaDtMarkType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<CurriculumFinalMarkFormulaDtMarkType> GetCurriculumFinalMarkFormulaDtMarkTypeList(string filterExpression, IDbContext ctx)
        {
            List<CurriculumFinalMarkFormulaDtMarkType> result = new List<CurriculumFinalMarkFormulaDtMarkType>();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumFinalMarkFormulaDtMarkType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumFinalMarkFormulaDtMarkType)helper.IDataReaderToObject(reader, new CurriculumFinalMarkFormulaDtMarkType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region CurriculumFinalMarkFormulaHd
        public static CurriculumFinalMarkFormulaHd GetCurriculumFinalMarkFormulaHd(Int32 CurriculumFinalMarkFormulaID)
        {
            return new CurriculumFinalMarkFormulaHdDao().Get(CurriculumFinalMarkFormulaID);
        }
        public static int InsertCurriculumFinalMarkFormulaHd(CurriculumFinalMarkFormulaHd record)
        {
            return new CurriculumFinalMarkFormulaHdDao().Insert(record);
        }
        public static int UpdateCurriculumFinalMarkFormulaHd(CurriculumFinalMarkFormulaHd record)
        {
            return new CurriculumFinalMarkFormulaHdDao().Update(record);
        }
        public static int DeleteCurriculumFinalMarkFormulaHd(Int32 CurriculumFinalMarkFormulaID)
        {
            return new CurriculumFinalMarkFormulaHdDao().Delete(CurriculumFinalMarkFormulaID);
        }
        public static List<CurriculumFinalMarkFormulaHd> GetCurriculumFinalMarkFormulaHdList(string filterExpression)
        {
            List<CurriculumFinalMarkFormulaHd> result = new List<CurriculumFinalMarkFormulaHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumFinalMarkFormulaHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumFinalMarkFormulaHd)helper.IDataReaderToObject(reader, new CurriculumFinalMarkFormulaHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region CurriculumMajor
        public static CurriculumMajor GetCurriculumMajor(Int32 CurriculumMajorID)
        {
            return new CurriculumMajorDao().Get(CurriculumMajorID);
        }
        public static int InsertCurriculumMajor(CurriculumMajor record)
        {
            return new CurriculumMajorDao().Insert(record);
        }
        public static int UpdateCurriculumMajor(CurriculumMajor record)
        {
            return new CurriculumMajorDao().Update(record);
        }
        public static int DeleteCurriculumMajor(Int32 CurriculumMajorID)
        {
            return new CurriculumMajorDao().Delete(CurriculumMajorID);
        }
        public static List<CurriculumMajor> GetCurriculumMajorList(string filterExpression)
        {
            List<CurriculumMajor> result = new List<CurriculumMajor>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumMajor));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumMajor)helper.IDataReaderToObject(reader, new CurriculumMajor()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region CurriculumMarkType
        public static CurriculumMarkType GetCurriculumMarkType(Int32 CurriculumMarkTypeID)
        {
            return new CurriculumMarkTypeDao().Get(CurriculumMarkTypeID);
        }
        public static int InsertCurriculumMarkType(CurriculumMarkType record)
        {
            return new CurriculumMarkTypeDao().Insert(record);
        }
        public static int UpdateCurriculumMarkType(CurriculumMarkType record)
        {
            return new CurriculumMarkTypeDao().Update(record);
        }
        public static int DeleteCurriculumMarkType(Int32 CurriculumMarkTypeID)
        {
            return new CurriculumMarkTypeDao().Delete(CurriculumMarkTypeID);
        }
        public static List<CurriculumMarkType> GetCurriculumMarkTypeList(string filterExpression)
        {
            List<CurriculumMarkType> result = new List<CurriculumMarkType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumMarkType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumMarkType)helper.IDataReaderToObject(reader, new CurriculumMarkType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetCurriculumMarkTypeMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumMarkType));
                ctx.CommandText = helper.SelectMaxColumn("CurriculumMarkTypeID");
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
        #region CurriculumMarkTypeClassStudyType
        public static CurriculumMarkTypeClassStudyType GetCurriculumMarkTypeClassStudyType(Int32 CurriculumMarkTypeID, String GCClassStudyType)
        {
            return new CurriculumMarkTypeClassStudyTypeDao().Get(CurriculumMarkTypeID, GCClassStudyType);
        }
        public static int InsertCurriculumMarkTypeClassStudyType(CurriculumMarkTypeClassStudyType record)
        {
            return new CurriculumMarkTypeClassStudyTypeDao().Insert(record);
        }
        public static int UpdateCurriculumMarkTypeClassStudyType(CurriculumMarkTypeClassStudyType record)
        {
            return new CurriculumMarkTypeClassStudyTypeDao().Update(record);
        }
        public static int DeleteCurriculumMarkTypeClassStudyType(Int32 CurriculumMarkTypeID, String GCClassStudyType)
        {
            return new CurriculumMarkTypeClassStudyTypeDao().Delete(CurriculumMarkTypeID, GCClassStudyType);
        }
        public static List<CurriculumMarkTypeClassStudyType> GetCurriculumMarkTypeClassStudyTypeList(string filterExpression)
        {
            List<CurriculumMarkTypeClassStudyType> result = new List<CurriculumMarkTypeClassStudyType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumMarkTypeClassStudyType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumMarkTypeClassStudyType)helper.IDataReaderToObject(reader, new CurriculumMarkTypeClassStudyType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<CurriculumMarkTypeClassStudyType> GetCurriculumMarkTypeClassStudyTypeList(string filterExpression, IDbContext ctx)
        {
            List<CurriculumMarkTypeClassStudyType> result = new List<CurriculumMarkTypeClassStudyType>();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumMarkTypeClassStudyType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumMarkTypeClassStudyType)helper.IDataReaderToObject(reader, new CurriculumMarkTypeClassStudyType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region CurriculumMarkTypeDt
        public static CurriculumMarkTypeDt GetCurriculumMarkTypeDt(Int32 CurriculumMarkTypeDtID)
        {
            return new CurriculumMarkTypeDtDao().Get(CurriculumMarkTypeDtID);
        }
        public static int InsertCurriculumMarkTypeDt(CurriculumMarkTypeDt record)
        {
            return new CurriculumMarkTypeDtDao().Insert(record);
        }
        public static int UpdateCurriculumMarkTypeDt(CurriculumMarkTypeDt record)
        {
            return new CurriculumMarkTypeDtDao().Update(record);
        }
        public static int DeleteCurriculumMarkTypeDt(Int32 CurriculumMarkTypeDtID)
        {
            return new CurriculumMarkTypeDtDao().Delete(CurriculumMarkTypeDtID);
        }
        public static List<CurriculumMarkTypeDt> GetCurriculumMarkTypeDtList(string filterExpression)
        {
            List<CurriculumMarkTypeDt> result = new List<CurriculumMarkTypeDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumMarkTypeDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumMarkTypeDt)helper.IDataReaderToObject(reader, new CurriculumMarkTypeDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region CurriculumMeetingPlan
        public static CurriculumMeetingPlan GetCurriculumMeetingPlan(Int32 CurriculumMeetingPlanID)
        {
            return new CurriculumMeetingPlanDao().Get(CurriculumMeetingPlanID);
        }
        public static int InsertCurriculumMeetingPlan(CurriculumMeetingPlan record)
        {
            return new CurriculumMeetingPlanDao().Insert(record);
        }
        public static int UpdateCurriculumMeetingPlan(CurriculumMeetingPlan record)
        {
            return new CurriculumMeetingPlanDao().Update(record);
        }
        public static int DeleteCurriculumMeetingPlan(Int32 CurriculumMeetingPlanID)
        {
            return new CurriculumMeetingPlanDao().Delete(CurriculumMeetingPlanID);
        }
        public static List<CurriculumMeetingPlan> GetCurriculumMeetingPlanList(string filterExpression)
        {
            List<CurriculumMeetingPlan> result = new List<CurriculumMeetingPlan>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumMeetingPlan));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumMeetingPlan)helper.IDataReaderToObject(reader, new CurriculumMeetingPlan()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region CurriculumReportDt
        public static CurriculumReportDt GetCurriculumReportDt(Int32 CurriculumReportDtID)
        {
            return new CurriculumReportDtDao().Get(CurriculumReportDtID);
        }
        public static int InsertCurriculumReportDt(CurriculumReportDt record)
        {
            return new CurriculumReportDtDao().Insert(record);
        }
        public static int UpdateCurriculumReportDt(CurriculumReportDt record)
        {
            return new CurriculumReportDtDao().Update(record);
        }
        public static int DeleteCurriculumReportDt(Int32 CurriculumReportDtID)
        {
            return new CurriculumReportDtDao().Delete(CurriculumReportDtID);
        }
        public static List<CurriculumReportDt> GetCurriculumReportDtList(string filterExpression)
        {
            List<CurriculumReportDt> result = new List<CurriculumReportDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumReportDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumReportDt)helper.IDataReaderToObject(reader, new CurriculumReportDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region CurriculumReportDtSubject
        public static CurriculumReportDtSubject GetCurriculumReportDtSubject(Int32 CurriculumReportDtID, Int32 CurriculumSubjectID)
        {
            return new CurriculumReportDtSubjectDao().Get(CurriculumReportDtID, CurriculumSubjectID);
        }
        public static int InsertCurriculumReportDtSubject(CurriculumReportDtSubject record)
        {
            return new CurriculumReportDtSubjectDao().Insert(record);
        }
        public static int UpdateCurriculumReportDtSubject(CurriculumReportDtSubject record)
        {
            return new CurriculumReportDtSubjectDao().Update(record);
        }
        public static int DeleteCurriculumReportDtSubject(Int32 CurriculumReportDtID, Int32 CurriculumSubjectID)
        {
            return new CurriculumReportDtSubjectDao().Delete(CurriculumReportDtID, CurriculumSubjectID);
        }
        public static List<CurriculumReportDtSubject> GetCurriculumReportDtSubjectList(string filterExpression)
        {
            List<CurriculumReportDtSubject> result = new List<CurriculumReportDtSubject>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumReportDtSubject));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumReportDtSubject)helper.IDataReaderToObject(reader, new CurriculumReportDtSubject()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region CurriculumSchoolPeriodSection
        public static CurriculumSchoolPeriodSection GetCurriculumSchoolPeriodSection(Int32 CurriculumSchoolPeriodSectionID)
        {
            return new CurriculumSchoolPeriodSectionDao().Get(CurriculumSchoolPeriodSectionID);
        }
        public static int InsertCurriculumSchoolPeriodSection(CurriculumSchoolPeriodSection record)
        {
            return new CurriculumSchoolPeriodSectionDao().Insert(record);
        }
        public static int UpdateCurriculumSchoolPeriodSection(CurriculumSchoolPeriodSection record)
        {
            return new CurriculumSchoolPeriodSectionDao().Update(record);
        }
        public static int DeleteCurriculumSchoolPeriodSection(Int32 CurriculumSchoolPeriodSectionID)
        {
            return new CurriculumSchoolPeriodSectionDao().Delete(CurriculumSchoolPeriodSectionID);
        }
        public static List<CurriculumSchoolPeriodSection> GetCurriculumSchoolPeriodSectionList(string filterExpression)
        {
            List<CurriculumSchoolPeriodSection> result = new List<CurriculumSchoolPeriodSection>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumSchoolPeriodSection));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumSchoolPeriodSection)helper.IDataReaderToObject(reader, new CurriculumSchoolPeriodSection()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region CurriculumSubject
        public static CurriculumSubject GetCurriculumSubject(Int32 CurriculumSubjectID)
        {
            return new CurriculumSubjectDao().Get(CurriculumSubjectID);
        }
        public static int InsertCurriculumSubject(CurriculumSubject record)
        {
            return new CurriculumSubjectDao().Insert(record);
        }
        public static int UpdateCurriculumSubject(CurriculumSubject record)
        {
            return new CurriculumSubjectDao().Update(record);
        }
        public static int DeleteCurriculumSubject(Int32 CurriculumSubjectID)
        {
            return new CurriculumSubjectDao().Delete(CurriculumSubjectID);
        }
        public static List<CurriculumSubject> GetCurriculumSubjectList(string filterExpression)
        {
            List<CurriculumSubject> result = new List<CurriculumSubject>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumSubject));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumSubject)helper.IDataReaderToObject(reader, new CurriculumSubject()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetCurriculumSubjectMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumSubject));
                ctx.CommandText = helper.SelectMaxColumn("CurriculumSubjectID");
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
        #region CurriculumSubjectClassType
        public static CurriculumSubjectClassType GetCurriculumSubjectClassType(Int32 CurriculumSubjectID, Int32 CurriculumClassTypeID)
        {
            return new CurriculumSubjectClassTypeDao().Get(CurriculumSubjectID, CurriculumClassTypeID);
        }
        public static int InsertCurriculumSubjectClassType(CurriculumSubjectClassType record)
        {
            return new CurriculumSubjectClassTypeDao().Insert(record);
        }
        public static int UpdateCurriculumSubjectClassType(CurriculumSubjectClassType record)
        {
            return new CurriculumSubjectClassTypeDao().Update(record);
        }
        public static int DeleteCurriculumSubjectClassType(Int32 CurriculumSubjectID, Int32 CurriculumClassTypeID)
        {
            return new CurriculumSubjectClassTypeDao().Delete(CurriculumSubjectID, CurriculumClassTypeID);
        }
        public static List<CurriculumSubjectClassType> GetCurriculumSubjectClassTypeList(string filterExpression)
        {
            List<CurriculumSubjectClassType> result = new List<CurriculumSubjectClassType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumSubjectClassType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumSubjectClassType)helper.IDataReaderToObject(reader, new CurriculumSubjectClassType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<CurriculumSubjectClassType> GetCurriculumSubjectClassTypeList(string filterExpression, IDbContext ctx)
        {
            List<CurriculumSubjectClassType> result = new List<CurriculumSubjectClassType>();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumSubjectClassType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumSubjectClassType)helper.IDataReaderToObject(reader, new CurriculumSubjectClassType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region CurriculumSubjectGroup
        public static CurriculumSubjectGroup GetCurriculumSubjectGroup(Int32 CurriculumSubjectGroupID)
        {
            return new CurriculumSubjectGroupDao().Get(CurriculumSubjectGroupID);
        }
        public static int InsertCurriculumSubjectGroup(CurriculumSubjectGroup record)
        {
            return new CurriculumSubjectGroupDao().Insert(record);
        }
        public static int UpdateCurriculumSubjectGroup(CurriculumSubjectGroup record)
        {
            return new CurriculumSubjectGroupDao().Update(record);
        }
        public static int DeleteCurriculumSubjectGroup(Int32 CurriculumSubjectGroupID)
        {
            return new CurriculumSubjectGroupDao().Delete(CurriculumSubjectGroupID);
        }
        public static List<CurriculumSubjectGroup> GetCurriculumSubjectGroupList(string filterExpression)
        {
            List<CurriculumSubjectGroup> result = new List<CurriculumSubjectGroup>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumSubjectGroup));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumSubjectGroup)helper.IDataReaderToObject(reader, new CurriculumSubjectGroup()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region CurriculumSubjectMarkType
        public static CurriculumSubjectMarkType GetCurriculumSubjectMarkType(Int32 CurriculumSubjectID, Int32 CurriculumMarkTypeID)
        {
            return new CurriculumSubjectMarkTypeDao().Get(CurriculumSubjectID, CurriculumMarkTypeID);
        }
        public static int InsertCurriculumSubjectMarkType(CurriculumSubjectMarkType record)
        {
            return new CurriculumSubjectMarkTypeDao().Insert(record);
        }
        public static int UpdateCurriculumSubjectMarkType(CurriculumSubjectMarkType record)
        {
            return new CurriculumSubjectMarkTypeDao().Update(record);
        }
        public static int DeleteCurriculumSubjectMarkType(Int32 CurriculumSubjectID, Int32 CurriculumMarkTypeID)
        {
            return new CurriculumSubjectMarkTypeDao().Delete(CurriculumSubjectID, CurriculumMarkTypeID);
        }
        public static List<CurriculumSubjectMarkType> GetCurriculumSubjectMarkTypeList(string filterExpression)
        {
            List<CurriculumSubjectMarkType> result = new List<CurriculumSubjectMarkType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumSubjectMarkType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumSubjectMarkType)helper.IDataReaderToObject(reader, new CurriculumSubjectMarkType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<CurriculumSubjectMarkType> GetCurriculumSubjectMarkTypeList(string filterExpression, IDbContext ctx)
        {
            List<CurriculumSubjectMarkType> result = new List<CurriculumSubjectMarkType>();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumSubjectMarkType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumSubjectMarkType)helper.IDataReaderToObject(reader, new CurriculumSubjectMarkType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region CurriculumSyllabus
        public static CurriculumSyllabus GetCurriculumSyllabus(Int32 CurriculumSyllabusID)
        {
            return new CurriculumSyllabusDao().Get(CurriculumSyllabusID);
        }
        public static int InsertCurriculumSyllabus(CurriculumSyllabus record)
        {
            return new CurriculumSyllabusDao().Insert(record);
        }
        public static int UpdateCurriculumSyllabus(CurriculumSyllabus record)
        {
            return new CurriculumSyllabusDao().Update(record);
        }
        public static int DeleteCurriculumSyllabus(Int32 CurriculumSyllabusID)
        {
            return new CurriculumSyllabusDao().Delete(CurriculumSyllabusID);
        }
        public static List<CurriculumSyllabus> GetCurriculumSyllabusList(string filterExpression)
        {
            List<CurriculumSyllabus> result = new List<CurriculumSyllabus>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CurriculumSyllabus));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CurriculumSyllabus)helper.IDataReaderToObject(reader, new CurriculumSyllabus()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Customer
        public static Customer GetCustomer(Int32 BusinessPartnerID)
        {
            return new CustomerDao().Get(BusinessPartnerID);
        }
        public static int InsertCustomer(Customer record)
        {
            return new CustomerDao().Insert(record);
        }
        public static int UpdateCustomer(Customer record)
        {
            return new CustomerDao().Update(record);
        }
        public static int DeleteCustomer(Int32 BusinessPartnerID)
        {
            return new CustomerDao().Delete(BusinessPartnerID);
        }
        public static List<Customer> GetCustomerList(string filterExpression)
        {
            List<Customer> result = new List<Customer>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Customer));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Customer)helper.IDataReaderToObject(reader, new Customer()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region CustomerContract
        public static CustomerContract GetCustomerContract(Int32 ContractID)
        {
            return new CustomerContractDao().Get(ContractID);
        }
        public static int InsertCustomerContract(CustomerContract record)
        {
            return new CustomerContractDao().Insert(record);
        }
        public static int UpdateCustomerContract(CustomerContract record)
        {
            return new CustomerContractDao().Update(record);
        }
        public static int DeleteCustomerContract(Int32 ContractID)
        {
            return new CustomerContractDao().Delete(ContractID);
        }
        public static List<CustomerContract> GetCustomerContractList(string filterExpression)
        {
            List<CustomerContract> result = new List<CustomerContract>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CustomerContract));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CustomerContract)helper.IDataReaderToObject(reader, new CustomerContract()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<CustomerContract> GetCustomerContractList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<CustomerContract> result = new List<CustomerContract>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CustomerContract));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CustomerContract)helper.IDataReaderToObject(reader, new CustomerContract()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetCustomerContractRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CustomerContract));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetCustomerContractRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CustomerContract));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ContractID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetCustomerContractMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(CustomerContract));
                ctx.CommandText = helper.SelectMaxColumn("ContractID");
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
        #region CustomerContractMember
        public static CustomerContractMember GetCustomerContractMember(Int32 ContractID, Int32 CoverageTypeID, Int32 StudentID)
        {
            return new CustomerContractMemberDao().Get(ContractID, CoverageTypeID, StudentID);
        }
        public static int InsertCustomerContractMember(CustomerContractMember record)
        {
            return new CustomerContractMemberDao().Insert(record);
        }
        public static int UpdateCustomerContractMember(CustomerContractMember record)
        {
            return new CustomerContractMemberDao().Update(record);
        }
        public static int DeleteCustomerContractMember(Int32 ContractID, Int32 CoverageTypeID, Int32 StudentID)
        {
            return new CustomerContractMemberDao().Delete(ContractID, CoverageTypeID, StudentID);
        }
        public static List<CustomerContractMember> GetCustomerContractMemberList(string filterExpression)
        {
            List<CustomerContractMember> result = new List<CustomerContractMember>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CustomerContractMember));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CustomerContractMember)helper.IDataReaderToObject(reader, new CustomerContractMember()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<CustomerContractMember> GetCustomerContractMemberList(string filterExpression, IDbContext ctx)
        {
            List<CustomerContractMember> result = new List<CustomerContractMember>();
            try
            {
                DbHelper helper = new DbHelper(typeof(CustomerContractMember));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CustomerContractMember)helper.IDataReaderToObject(reader, new CustomerContractMember()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region DailySchedule
        public static DailySchedule GetDailySchedule(Int32 DailyScheduleID)
        {
            return new DailyScheduleDao().Get(DailyScheduleID);
        }
        public static int InsertDailySchedule(DailySchedule record)
        {
            return new DailyScheduleDao().Insert(record);
        }
        public static int UpdateDailySchedule(DailySchedule record)
        {
            return new DailyScheduleDao().Update(record);
        }
        public static int DeleteDailySchedule(Int32 DailyScheduleID)
        {
            return new DailyScheduleDao().Delete(DailyScheduleID);
        }
        public static List<DailySchedule> GetDailyScheduleList(string filterExpression)
        {
            List<DailySchedule> result = new List<DailySchedule>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(DailySchedule));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((DailySchedule)helper.IDataReaderToObject(reader, new DailySchedule()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region DailySchedulePackage
        public static DailySchedulePackage GetDailySchedulePackage(Int32 DailySchedulePackageID)
        {
            return new DailySchedulePackageDao().Get(DailySchedulePackageID);
        }
        public static int InsertDailySchedulePackage(DailySchedulePackage record)
        {
            return new DailySchedulePackageDao().Insert(record);
        }
        public static int UpdateDailySchedulePackage(DailySchedulePackage record)
        {
            return new DailySchedulePackageDao().Update(record);
        }
        public static int DeleteDailySchedulePackage(Int32 DailySchedulePackageID)
        {
            return new DailySchedulePackageDao().Delete(DailySchedulePackageID);
        }
        public static List<DailySchedulePackage> GetDailySchedulePackageList(string filterExpression)
        {
            List<DailySchedulePackage> result = new List<DailySchedulePackage>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(DailySchedulePackage));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((DailySchedulePackage)helper.IDataReaderToObject(reader, new DailySchedulePackage()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetDailySchedulePackageRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(DailySchedulePackage));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<DailySchedulePackage> GetDailySchedulePackageList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<DailySchedulePackage> result = new List<DailySchedulePackage>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(DailySchedulePackage));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((DailySchedulePackage)helper.IDataReaderToObject(reader, new DailySchedulePackage()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetDailySchedulePackageRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(DailySchedulePackage));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "DailySchedulePackageID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetDailySchedulePackageMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(DailySchedulePackage));
                ctx.CommandText = helper.SelectMaxColumn("DailySchedulePackageID");
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
        #region DailyScheduleTypeDt
        public static DailyScheduleTypeDt GetDailyScheduleTypeDt(Int32 DailyScheduleTypeDtID)
        {
            return new DailyScheduleTypeDtDao().Get(DailyScheduleTypeDtID);
        }
        public static int InsertDailyScheduleTypeDt(DailyScheduleTypeDt record)
        {
            return new DailyScheduleTypeDtDao().Insert(record);
        }
        public static int UpdateDailyScheduleTypeDt(DailyScheduleTypeDt record)
        {
            return new DailyScheduleTypeDtDao().Update(record);
        }
        public static int DeleteDailyScheduleTypeDt(Int32 DailyScheduleTypeDtID)
        {
            return new DailyScheduleTypeDtDao().Delete(DailyScheduleTypeDtID);
        }
        public static List<DailyScheduleTypeDt> GetDailyScheduleTypeDtList(string filterExpression)
        {
            List<DailyScheduleTypeDt> result = new List<DailyScheduleTypeDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(DailyScheduleTypeDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((DailyScheduleTypeDt)helper.IDataReaderToObject(reader, new DailyScheduleTypeDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region DailyScheduleTypeHd
        public static DailyScheduleTypeHd GetDailyScheduleTypeHd(Int32 DailyScheduleTypeHdID)
        {
            return new DailyScheduleTypeHdDao().Get(DailyScheduleTypeHdID);
        }
        public static int InsertDailyScheduleTypeHd(DailyScheduleTypeHd record)
        {
            return new DailyScheduleTypeHdDao().Insert(record);
        }
        public static int UpdateDailyScheduleTypeHd(DailyScheduleTypeHd record)
        {
            return new DailyScheduleTypeHdDao().Update(record);
        }
        public static int DeleteDailyScheduleTypeHd(Int32 DailyScheduleTypeHdID)
        {
            return new DailyScheduleTypeHdDao().Delete(DailyScheduleTypeHdID);
        }
        public static List<DailyScheduleTypeHd> GetDailyScheduleTypeHdList(string filterExpression)
        {
            List<DailyScheduleTypeHd> result = new List<DailyScheduleTypeHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(DailyScheduleTypeHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((DailyScheduleTypeHd)helper.IDataReaderToObject(reader, new DailyScheduleTypeHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetDailyScheduleTypeHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(DailyScheduleTypeHd));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<DailyScheduleTypeHd> GetDailyScheduleTypeHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<DailyScheduleTypeHd> result = new List<DailyScheduleTypeHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(DailyScheduleTypeHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((DailyScheduleTypeHd)helper.IDataReaderToObject(reader, new DailyScheduleTypeHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetDailyScheduleTypeHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(DailyScheduleTypeHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "DailyScheduleTypeID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetDailyScheduleTypeHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(DailyScheduleTypeHd));
                ctx.CommandText = helper.SelectMaxColumn("DailyScheduleTypeID");
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
        #region Department
        public static Department GetDepartment(String DepartmentID)
        {
            return new DepartmentDao().Get(DepartmentID);
        }
        public static int InsertDepartment(Department record)
        {
            return new DepartmentDao().Insert(record);
        }
        public static int UpdateDepartment(Department record)
        {
            return new DepartmentDao().Update(record);
        }
        public static int DeleteDepartment(String DepartmentID)
        {
            return new DepartmentDao().Delete(DepartmentID);
        }
        public static List<Department> GetDepartmentList(string filterExpression)
        {
            List<Department> result = new List<Department>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Department));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Department)helper.IDataReaderToObject(reader, new Department()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<Department> GetDepartmentList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<Department> result = new List<Department>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Department));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Department)helper.IDataReaderToObject(reader, new Department()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetDepartmentRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Department));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetDepartmentRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Department));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "DepartmentID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region DirectPaymentDt
        public static DirectPaymentDt GetDirectPaymentDt(Int32 PaymentDetailID)
        {
            return new DirectPaymentDtDao().Get(PaymentDetailID);
        }
        public static int InsertDirectPaymentDt(DirectPaymentDt record)
        {
            return new DirectPaymentDtDao().Insert(record);
        }
        public static int UpdateDirectPaymentDt(DirectPaymentDt record)
        {
            return new DirectPaymentDtDao().Update(record);
        }
        public static int DeleteDirectPaymentDt(Int32 PaymentDetailID)
        {
            return new DirectPaymentDtDao().Delete(PaymentDetailID);
        }
        public static List<DirectPaymentDt> GetDirectPaymentDtList(string filterExpression)
        {
            List<DirectPaymentDt> result = new List<DirectPaymentDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(DirectPaymentDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((DirectPaymentDt)helper.IDataReaderToObject(reader, new DirectPaymentDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<DirectPaymentDt> GetDirectPaymentDtList(string filterExpression, IDbContext ctx)
        {
            List<DirectPaymentDt> result = new List<DirectPaymentDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(DirectPaymentDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((DirectPaymentDt)helper.IDataReaderToObject(reader, new DirectPaymentDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region DirectPaymentHd
        public static DirectPaymentHd GetDirectPaymentHd(Int32 PaymentID)
        {
            return new DirectPaymentHdDao().Get(PaymentID);
        }
        public static int InsertDirectPaymentHd(DirectPaymentHd record)
        {
            return new DirectPaymentHdDao().Insert(record);
        }
        public static int UpdateDirectPaymentHd(DirectPaymentHd record)
        {
            return new DirectPaymentHdDao().Update(record);
        }
        public static int DeleteDirectPaymentHd(Int32 PaymentID)
        {
            return new DirectPaymentHdDao().Delete(PaymentID);
        }
        public static List<DirectPaymentHd> GetDirectPaymentHdList(string filterExpression)
        {
            List<DirectPaymentHd> result = new List<DirectPaymentHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(DirectPaymentHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((DirectPaymentHd)helper.IDataReaderToObject(reader, new DirectPaymentHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<DirectPaymentHd> GetDirectPaymentHdList(string filterExpression, IDbContext ctx)
        {
            List<DirectPaymentHd> result = new List<DirectPaymentHd>();
            try
            {
                DbHelper helper = new DbHelper(typeof(DirectPaymentHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((DirectPaymentHd)helper.IDataReaderToObject(reader, new DirectPaymentHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetDirectPaymentHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(DirectPaymentHd));
                ctx.CommandText = helper.SelectMaxColumn("PaymentID");
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
        #region DirectPurchaseDt
        public static DirectPurchaseDt GetDirectPurchaseDt(Int32 ID)
        {
            return new DirectPurchaseDtDao().Get(ID);
        }
        public static int InsertDirectPurchaseDt(DirectPurchaseDt record)
        {
            return new DirectPurchaseDtDao().Insert(record);
        }
        public static int UpdateDirectPurchaseDt(DirectPurchaseDt record)
        {
            return new DirectPurchaseDtDao().Update(record);
        }
        public static int DeleteDirectPurchaseDt(Int32 ID)
        {
            return new DirectPurchaseDtDao().Delete(ID);
        }
        public static List<DirectPurchaseDt> GetDirectPurchaseDtList(string filterExpression)
        {
            List<DirectPurchaseDt> result = new List<DirectPurchaseDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(DirectPurchaseDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((DirectPurchaseDt)helper.IDataReaderToObject(reader, new DirectPurchaseDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        public static List<DirectPurchaseDt> GetDirectPurchaseDtList(string filterExpression, IDbContext ctx)
        {
            List<DirectPurchaseDt> result = new List<DirectPurchaseDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(DirectPurchaseDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((DirectPurchaseDt)helper.IDataReaderToObject(reader, new DirectPurchaseDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetDirectPurchaseDtRowCount(string filterExpression, IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(DirectPurchaseDt));
                ctx.CommandText = helper.GetRowCount(filterExpression);
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
        #region DirectPurchaseHd
        public static DirectPurchaseHd GetDirectPurchaseHd(Int32 DirectPurchaseID)
        {
            return new DirectPurchaseHdDao().Get(DirectPurchaseID);
        }
        public static int InsertDirectPurchaseHd(DirectPurchaseHd record)
        {
            return new DirectPurchaseHdDao().Insert(record);
        }
        public static int UpdateDirectPurchaseHd(DirectPurchaseHd record)
        {
            return new DirectPurchaseHdDao().Update(record);
        }
        public static int DeleteDirectPurchaseHd(Int32 DirectPurchaseID)
        {
            return new DirectPurchaseHdDao().Delete(DirectPurchaseID);
        }
        public static List<DirectPurchaseHd> GetDirectPurchaseHdList(string filterExpression)
        {
            List<DirectPurchaseHd> result = new List<DirectPurchaseHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(DirectPurchaseHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((DirectPurchaseHd)helper.IDataReaderToObject(reader, new DirectPurchaseHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<DirectPurchaseHd> GetDirectPurchaseHdList(string filterExpression, IDbContext ctx)
        {
            List<DirectPurchaseHd> result = new List<DirectPurchaseHd>();
            try
            {
                DbHelper helper = new DbHelper(typeof(DirectPurchaseHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((DirectPurchaseHd)helper.IDataReaderToObject(reader, new DirectPurchaseHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetDirectPurchaseHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(DirectPurchaseHd));
                ctx.CommandText = helper.SelectMaxColumn("DirectPurchaseID");
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
        #region DirectPurchaseReturnDt
        public static DirectPurchaseReturnDt GetDirectPurchaseReturnDt(Int32 ID)
        {
            return new DirectPurchaseReturnDtDao().Get(ID);
        }
        public static int InsertDirectPurchaseReturnDt(DirectPurchaseReturnDt record)
        {
            return new DirectPurchaseReturnDtDao().Insert(record);
        }
        public static int UpdateDirectPurchaseReturnDt(DirectPurchaseReturnDt record)
        {
            return new DirectPurchaseReturnDtDao().Update(record);
        }
        public static int DeleteDirectPurchaseReturnDt(Int32 ID)
        {
            return new DirectPurchaseReturnDtDao().Delete(ID);
        }
        public static List<DirectPurchaseReturnDt> GetDirectPurchaseReturnDtList(string filterExpression)
        {
            List<DirectPurchaseReturnDt> result = new List<DirectPurchaseReturnDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(DirectPurchaseReturnDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((DirectPurchaseReturnDt)helper.IDataReaderToObject(reader, new DirectPurchaseReturnDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<DirectPurchaseReturnDt> GetDirectPurchaseReturnDtList(string filterExpression, IDbContext ctx)
        {
            List<DirectPurchaseReturnDt> result = new List<DirectPurchaseReturnDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(DirectPurchaseReturnDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((DirectPurchaseReturnDt)helper.IDataReaderToObject(reader, new DirectPurchaseReturnDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region DirectPurchaseReturnHd
        public static DirectPurchaseReturnHd GetDirectPurchaseReturnHd(Int32 DirectPurchaseReturnID)
        {
            return new DirectPurchaseReturnHdDao().Get(DirectPurchaseReturnID);
        }
        public static int InsertDirectPurchaseReturnHd(DirectPurchaseReturnHd record)
        {
            return new DirectPurchaseReturnHdDao().Insert(record);
        }
        public static int UpdateDirectPurchaseReturnHd(DirectPurchaseReturnHd record)
        {
            return new DirectPurchaseReturnHdDao().Update(record);
        }
        public static int DeleteDirectPurchaseReturnHd(Int32 DirectPurchaseReturnID)
        {
            return new DirectPurchaseReturnHdDao().Delete(DirectPurchaseReturnID);
        }
        public static List<DirectPurchaseReturnHd> GetDirectPurchaseReturnHdList(string filterExpression)
        {
            List<DirectPurchaseReturnHd> result = new List<DirectPurchaseReturnHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(DirectPurchaseReturnHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((DirectPurchaseReturnHd)helper.IDataReaderToObject(reader, new DirectPurchaseReturnHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetDirectPurchaseReturnHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(DirectPurchaseReturnHd));
                ctx.CommandText = helper.SelectMaxColumn("DirectPurchaseReturnID");
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
        #region EDCMachine
        public static EDCMachine GetEDCMachine(Int32 EDCMachineID)
        {
            return new EDCMachineDao().Get(EDCMachineID);
        }
        public static int InsertEDCMachine(EDCMachine record)
        {
            return new EDCMachineDao().Insert(record);
        }
        public static int UpdateEDCMachine(EDCMachine record)
        {
            return new EDCMachineDao().Update(record);
        }
        public static int DeleteEDCMachine(Int32 EDCMachineID)
        {
            return new EDCMachineDao().Delete(EDCMachineID);
        }
        public static List<EDCMachine> GetEDCMachineList(string filterExpression)
        {
            List<EDCMachine> result = new List<EDCMachine>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(EDCMachine));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((EDCMachine)helper.IDataReaderToObject(reader, new EDCMachine()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetEDCMachineRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(EDCMachine));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "EDCMachineID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetEDCMachineMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(EDCMachine));
                ctx.CommandText = helper.SelectMaxColumn("EDCMachineID");
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
        #region Employee
        public static Employee GetEmployee(Int32 EmployeeID)
        {
            return new EmployeeDao().Get(EmployeeID);
        }
        public static int InsertEmployee(Employee record)
        {
            return new EmployeeDao().Insert(record);
        }
        public static int UpdateEmployee(Employee record)
        {
            return new EmployeeDao().Update(record);
        }
        public static int DeleteEmployee(Int32 EmployeeID)
        {
            return new EmployeeDao().Delete(EmployeeID);
        }
        public static List<Employee> GetEmployeeList(string filterExpression)
        {
            List<Employee> result = new List<Employee>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Employee));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Employee)helper.IDataReaderToObject(reader, new Employee()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetEmployeeMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(Employee));
                ctx.CommandText = helper.SelectMaxColumn("EmployeeID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<Employee> GetEmployeeList(string filterExpression, IDbContext ctx)
        {
            List<Employee> result = new List<Employee>();
            try
            {
                DbHelper helper = new DbHelper(typeof(Employee));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Employee)helper.IDataReaderToObject(reader, new Employee()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<Employee> GetEmployeeList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<Employee> result = new List<Employee>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Employee));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Employee)helper.IDataReaderToObject(reader, new Employee()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region EmployeeAttendanceSummary
        public static EmployeeAttendanceSummary GetEmployeeAttendanceSummary(Int32 AttendanceSummaryID)
        {
            return new EmployeeAttendanceSummaryDao().Get(AttendanceSummaryID);
        }
        public static int InsertEmployeeAttendanceSummary(EmployeeAttendanceSummary record)
        {
            return new EmployeeAttendanceSummaryDao().Insert(record);
        }
        public static int UpdateEmployeeAttendanceSummary(EmployeeAttendanceSummary record)
        {
            return new EmployeeAttendanceSummaryDao().Update(record);
        }
        public static int DeleteEmployeeAttendanceSummary(Int32 AttendanceSummaryID)
        {
            return new EmployeeAttendanceSummaryDao().Delete(AttendanceSummaryID);
        }
        public static List<EmployeeAttendanceSummary> GetEmployeeAttendanceSummaryList(string filterExpression)
        {
            List<EmployeeAttendanceSummary> result = new List<EmployeeAttendanceSummary>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(EmployeeAttendanceSummary));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((EmployeeAttendanceSummary)helper.IDataReaderToObject(reader, new EmployeeAttendanceSummary()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region EmployeeDailyAttendance
        public static EmployeeDailyAttendance GetEmployeeDailyAttendance(Int32 EmployeeID, DateTime ScheduleDate, String ScheduleStartTime)
        {
            return new EmployeeDailyAttendanceDao().Get(EmployeeID, ScheduleDate, ScheduleStartTime);
        }
        public static int InsertEmployeeDailyAttendance(EmployeeDailyAttendance record)
        {
            return new EmployeeDailyAttendanceDao().Insert(record);
        }
        public static int UpdateEmployeeDailyAttendance(EmployeeDailyAttendance record)
        {
            return new EmployeeDailyAttendanceDao().Update(record);
        }
        public static int DeleteEmployeeDailyAttendance(Int32 EmployeeID, DateTime ScheduleDate, String ScheduleStartTime)
        {
            return new EmployeeDailyAttendanceDao().Delete(EmployeeID, ScheduleDate, ScheduleStartTime);
        }
        public static List<EmployeeDailyAttendance> GetEmployeeDailyAttendanceList(string filterExpression)
        {
            List<EmployeeDailyAttendance> result = new List<EmployeeDailyAttendance>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(EmployeeDailyAttendance));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((EmployeeDailyAttendance)helper.IDataReaderToObject(reader, new EmployeeDailyAttendance()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<EmployeeDailyAttendance> GetEmployeeDailyAttendanceList(string filterExpression, IDbContext ctx)
        {
            List<EmployeeDailyAttendance> result = new List<EmployeeDailyAttendance>();
            try
            {
                DbHelper helper = new DbHelper(typeof(EmployeeDailyAttendance));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((EmployeeDailyAttendance)helper.IDataReaderToObject(reader, new EmployeeDailyAttendance()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetEmployeeDailyAttendanceMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(EmployeeDailyAttendance));
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
        #region EmployeeDailyAttendanceRenumeration
        public static EmployeeDailyAttendanceRenumeration GetEmployeeDailyAttendanceRenumeration(Int32 EmployeeID, DateTime ScheduleDate, String ScheduleStartTime, Int32 RenumerationCompID)
        {
            return new EmployeeDailyAttendanceRenumerationDao().Get(EmployeeID, ScheduleDate, ScheduleStartTime, RenumerationCompID);
        }
        public static int InsertEmployeeDailyAttendanceRenumeration(EmployeeDailyAttendanceRenumeration record)
        {
            return new EmployeeDailyAttendanceRenumerationDao().Insert(record);
        }
        public static int UpdateEmployeeDailyAttendanceRenumeration(EmployeeDailyAttendanceRenumeration record)
        {
            return new EmployeeDailyAttendanceRenumerationDao().Update(record);
        }
        public static int DeleteEmployeeDailyAttendanceRenumeration(Int32 EmployeeID, DateTime ScheduleDate, String ScheduleStartTime, Int32 RenumerationCompID)
        {
            return new EmployeeDailyAttendanceRenumerationDao().Delete(EmployeeID, ScheduleDate, ScheduleStartTime, RenumerationCompID);
        }
        public static List<EmployeeDailyAttendanceRenumeration> GetEmployeeDailyAttendanceRenumerationList(string filterExpression)
        {
            List<EmployeeDailyAttendanceRenumeration> result = new List<EmployeeDailyAttendanceRenumeration>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(EmployeeDailyAttendanceRenumeration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((EmployeeDailyAttendanceRenumeration)helper.IDataReaderToObject(reader, new EmployeeDailyAttendanceRenumeration()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<EmployeeDailyAttendanceRenumeration> GetEmployeeDailyAttendanceRenumerationList(string filterExpression, IDbContext ctx)
        {
            List<EmployeeDailyAttendanceRenumeration> result = new List<EmployeeDailyAttendanceRenumeration>();
            try
            {
                DbHelper helper = new DbHelper(typeof(EmployeeDailyAttendanceRenumeration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((EmployeeDailyAttendanceRenumeration)helper.IDataReaderToObject(reader, new EmployeeDailyAttendanceRenumeration()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetEmployeeDailyAttendanceRenumerationMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(EmployeeDailyAttendanceRenumeration));
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
        #region EmployeeFingerprintLog
        public static EmployeeFingerprintLog GetEmployeeFingerprintLog(Int32 EmployeeID, DateTime LogDateTime)
        {
            return new EmployeeFingerprintLogDao().Get(EmployeeID, LogDateTime);
        }
        public static int InsertEmployeeFingerprintLog(EmployeeFingerprintLog record)
        {
            return new EmployeeFingerprintLogDao().Insert(record);
        }
        public static int UpdateEmployeeFingerprintLog(EmployeeFingerprintLog record)
        {
            return new EmployeeFingerprintLogDao().Update(record);
        }
        public static int DeleteEmployeeFingerprintLog(Int32 EmployeeID, DateTime LogDateTime)
        {
            return new EmployeeFingerprintLogDao().Delete(EmployeeID, LogDateTime);
        }
        public static List<EmployeeFingerprintLog> GetEmployeeFingerprintLogList(string filterExpression)
        {
            List<EmployeeFingerprintLog> result = new List<EmployeeFingerprintLog>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(EmployeeFingerprintLog));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((EmployeeFingerprintLog)helper.IDataReaderToObject(reader, new EmployeeFingerprintLog()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<EmployeeFingerprintLog> GetEmployeeFingerprintLogList(string filterExpression, IDbContext ctx)
        {
            List<EmployeeFingerprintLog> result = new List<EmployeeFingerprintLog>();
            try
            {
                DbHelper helper = new DbHelper(typeof(EmployeeFingerprintLog));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((EmployeeFingerprintLog)helper.IDataReaderToObject(reader, new EmployeeFingerprintLog()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetEmployeeFingerprintLogMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(EmployeeFingerprintLog));
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
        #region ExamClassSchedule
        public static ExamClassSchedule GetExamClassSchedule(Int32 ExamScheduleDtID, Int32 SchoolClassID)
        {
            return new ExamClassScheduleDao().Get(ExamScheduleDtID, SchoolClassID);
        }
        public static int InsertExamClassSchedule(ExamClassSchedule record)
        {
            return new ExamClassScheduleDao().Insert(record);
        }
        public static int UpdateExamClassSchedule(ExamClassSchedule record)
        {
            return new ExamClassScheduleDao().Update(record);
        }
        public static int DeleteExamClassSchedule(Int32 ExamScheduleDtID, Int32 SchoolClassID)
        {
            return new ExamClassScheduleDao().Delete(ExamScheduleDtID, SchoolClassID);
        }
        public static List<ExamClassSchedule> GetExamClassScheduleList(string filterExpression)
        {
            List<ExamClassSchedule> result = new List<ExamClassSchedule>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ExamClassSchedule));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ExamClassSchedule)helper.IDataReaderToObject(reader, new ExamClassSchedule()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ExamScheduleDt
        public static ExamScheduleDt GetExamScheduleDt(Int32 ExamScheduleDtID)
        {
            return new ExamScheduleDtDao().Get(ExamScheduleDtID);
        }
        public static int InsertExamScheduleDt(ExamScheduleDt record)
        {
            return new ExamScheduleDtDao().Insert(record);
        }
        public static int UpdateExamScheduleDt(ExamScheduleDt record)
        {
            return new ExamScheduleDtDao().Update(record);
        }
        public static int DeleteExamScheduleDt(Int32 ExamScheduleDtID)
        {
            return new ExamScheduleDtDao().Delete(ExamScheduleDtID);
        }
        public static List<ExamScheduleDt> GetExamScheduleDtList(string filterExpression)
        {
            List<ExamScheduleDt> result = new List<ExamScheduleDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ExamScheduleDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ExamScheduleDt)helper.IDataReaderToObject(reader, new ExamScheduleDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ExamScheduleDt> GetExamScheduleDtList(string filterExpression, IDbContext ctx)
        {
            List<ExamScheduleDt> result = new List<ExamScheduleDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ExamScheduleDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ExamScheduleDt)helper.IDataReaderToObject(reader, new ExamScheduleDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ExamScheduleHd
        public static ExamScheduleHd GetExamScheduleHd(Int32 ExamScheduleID)
        {
            return new ExamScheduleHdDao().Get(ExamScheduleID);
        }
        public static int InsertExamScheduleHd(ExamScheduleHd record)
        {
            return new ExamScheduleHdDao().Insert(record);
        }
        public static int UpdateExamScheduleHd(ExamScheduleHd record)
        {
            return new ExamScheduleHdDao().Update(record);
        }
        public static int DeleteExamScheduleHd(Int32 ExamScheduleID)
        {
            return new ExamScheduleHdDao().Delete(ExamScheduleID);
        }
        public static List<ExamScheduleHd> GetExamScheduleHdList(string filterExpression)
        {
            List<ExamScheduleHd> result = new List<ExamScheduleHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ExamScheduleHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ExamScheduleHd)helper.IDataReaderToObject(reader, new ExamScheduleHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetExamScheduleHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ExamScheduleHd));
                ctx.CommandText = helper.SelectMaxColumn("ExamScheduleID");
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
        #region FADepreciation
        public static FADepreciation GetFADepreciation(Int32 FADepreciationID)
        {
            return new FADepreciationDao().Get(FADepreciationID);
        }
        public static int InsertFADepreciation(FADepreciation record)
        {
            return new FADepreciationDao().Insert(record);
        }
        public static int UpdateFADepreciation(FADepreciation record)
        {
            return new FADepreciationDao().Update(record);
        }
        public static int DeleteFADepreciation(Int32 FADepreciationID)
        {
            return new FADepreciationDao().Delete(FADepreciationID);
        }
        public static List<FADepreciation> GetFADepreciationList(string filterExpression)
        {
            IDbContext ctx = DbFactory.Configure();
            return GetFADepreciationList(filterExpression, ctx);
        }
        public static List<FADepreciation> GetFADepreciationList(string filterExpression, IDbContext ctx)
        {
            List<FADepreciation> result = new List<FADepreciation>();
            try
            {
                DbHelper helper = new DbHelper(typeof(FADepreciation));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((FADepreciation)helper.IDataReaderToObject(reader, new FADepreciation()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<FADepreciation> GetFADepreciationList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<FADepreciation> result = new List<FADepreciation>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(FADepreciation));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((FADepreciation)helper.IDataReaderToObject(reader, new FADepreciation()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetFADepreciationRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(FADepreciation));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetFADepreciationRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(FADepreciation));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "FADepreciationID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region FADepreciationMethod
        public static FADepreciationMethod GetFADepreciationMethod(Int32 MethodID)
        {
            return new FADepreciationMethodDao().Get(MethodID);
        }
        public static int InsertFADepreciationMethod(FADepreciationMethod record)
        {
            return new FADepreciationMethodDao().Insert(record);
        }
        public static int UpdateFADepreciationMethod(FADepreciationMethod record)
        {
            return new FADepreciationMethodDao().Update(record);
        }
        public static int DeleteFADepreciationMethod(Int32 MethodID)
        {
            return new FADepreciationMethodDao().Delete(MethodID);
        }
        public static List<FADepreciationMethod> GetFADepreciationMethodList(string filterExpression)
        {
            List<FADepreciationMethod> result = new List<FADepreciationMethod>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(FADepreciationMethod));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((FADepreciationMethod)helper.IDataReaderToObject(reader, new FADepreciationMethod()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<FADepreciationMethod> GetFADepreciationMethodList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<FADepreciationMethod> result = new List<FADepreciationMethod>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(FADepreciationMethod));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((FADepreciationMethod)helper.IDataReaderToObject(reader, new FADepreciationMethod()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetFADepreciationMethodRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(FADepreciationMethod));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetFADepreciationMethodRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(FADepreciationMethod));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "MethodID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetFADepreciationMethodMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(FADepreciationMethod));
                ctx.CommandText = helper.SelectMaxColumn("MethodID");
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
        #region FAGroup
        public static FAGroup GetFAGroup(Int32 FAGroupID)
        {
            return new FAGroupDao().Get(FAGroupID);
        }
        public static int InsertFAGroup(FAGroup record)
        {
            return new FAGroupDao().Insert(record);
        }
        public static int UpdateFAGroup(FAGroup record)
        {
            return new FAGroupDao().Update(record);
        }
        public static int DeleteFAGroup(Int32 FAGroupID)
        {
            return new FAGroupDao().Delete(FAGroupID);
        }
        public static List<FAGroup> GetFAGroupList(string filterExpression)
        {
            List<FAGroup> result = new List<FAGroup>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(FAGroup));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((FAGroup)helper.IDataReaderToObject(reader, new FAGroup()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        public static Int32 GetFAGroupMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(FAGroup));
                ctx.CommandText = helper.SelectMaxColumn("FAGroupID");
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
        #region FAGroupCOA
        public static FAGroupCOA GetFAGroupCOA(String SiteID, Int32 FAGroupID)
        {
            return new FAGroupCOADao().Get(SiteID, FAGroupID);
        }
        public static int InsertFAGroupCOA(FAGroupCOA record)
        {
            return new FAGroupCOADao().Insert(record);
        }
        public static int UpdateFAGroupCOA(FAGroupCOA record)
        {
            return new FAGroupCOADao().Update(record);
        }
        public static int DeleteFAGroupCOA(String SiteID, Int32 FAGroupID)
        {
            return new FAGroupCOADao().Delete(SiteID, FAGroupID);
        }
        public static List<FAGroupCOA> GetFAGroupCOAList(string filterExpression)
        {
            List<FAGroupCOA> result = new List<FAGroupCOA>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(FAGroupCOA));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((FAGroupCOA)helper.IDataReaderToObject(reader, new FAGroupCOA()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region FAItem
        public static FAItem GetFAItem(Int32 FixedAssetID)
        {
            return new FAItemDao().Get(FixedAssetID);
        }
        public static int InsertFAItem(FAItem record)
        {
            return new FAItemDao().Insert(record);
        }
        public static int UpdateFAItem(FAItem record)
        {
            return new FAItemDao().Update(record);
        }
        public static int DeleteFAItem(Int32 FixedAssetID)
        {
            return new FAItemDao().Delete(FixedAssetID);
        }
        public static List<FAItem> GetFAItemList(string filterExpression)
        {
            List<FAItem> result = new List<FAItem>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(FAItem));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((FAItem)helper.IDataReaderToObject(reader, new FAItem()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetFAItemMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(FAItem));
                ctx.CommandText = helper.SelectMaxColumn("FixedAssetID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<FAItem> GetFAItemList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "", IDbContext ctx = null)
        {
            List<FAItem> result = new List<FAItem>();
            bool IsCtxNull = false;
            if (ctx == null)
            {
                IsCtxNull = true;
                ctx = DbFactory.Configure();
            }
            try
            {
                DbHelper helper = new DbHelper(typeof(FAItem));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((FAItem)helper.IDataReaderToObject(reader, new FAItem()));
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
            return result;
        }
        #endregion
        #region FAItemCOA
        public static FAItemCOA GetFAItemCOA(String SiteID, Int32 FixedAssetID)
        {
            return new FAItemCOADao().Get(SiteID, FixedAssetID);
        }
        public static int InsertFAItemCOA(FAItemCOA record)
        {
            return new FAItemCOADao().Insert(record);
        }
        public static int UpdateFAItemCOA(FAItemCOA record)
        {
            return new FAItemCOADao().Update(record);
        }
        public static int DeleteFAItemCOA(String SiteID, Int32 FixedAssetID)
        {
            return new FAItemCOADao().Delete(SiteID, FixedAssetID);
        }
        public static List<FAItemCOA> GetFAItemCOAList(string filterExpression)
        {
            List<FAItemCOA> result = new List<FAItemCOA>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(FAItemCOA));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((FAItemCOA)helper.IDataReaderToObject(reader, new FAItemCOA()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region FAItemMovement
        public static FAItemMovement GetFAItemMovement(Int32 MovementID)
        {
            return new FAItemMovementDao().Get(MovementID);
        }
        public static int InsertFAItemMovement(FAItemMovement record)
        {
            return new FAItemMovementDao().Insert(record);
        }
        public static int UpdateFAItemMovement(FAItemMovement record)
        {
            return new FAItemMovementDao().Update(record);
        }
        public static int DeleteFAItemMovement(Int32 MovementID)
        {
            return new FAItemMovementDao().Delete(MovementID);
        }
        public static List<FAItemMovement> GetFAItemMovementList(string filterExpression)
        {
            List<FAItemMovement> result = new List<FAItemMovement>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(FAItemMovement));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((FAItemMovement)helper.IDataReaderToObject(reader, new FAItemMovement()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<FAItemMovement> GetFAItemMovementList(string filterExpression, IDbContext ctx)
        {
            List<FAItemMovement> result = new List<FAItemMovement>();
            try
            {
                DbHelper helper = new DbHelper(typeof(FAItemMovement));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((FAItemMovement)helper.IDataReaderToObject(reader, new FAItemMovement()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region FALocation
        public static FALocation GetFALocation(Int32 FALocationID)
        {
            return new FALocationDao().Get(FALocationID);
        }
        public static int InsertFALocation(FALocation record)
        {
            return new FALocationDao().Insert(record);
        }
        public static int UpdateFALocation(FALocation record)
        {
            return new FALocationDao().Update(record);
        }
        public static int DeleteFALocation(Int32 FALocationID)
        {
            return new FALocationDao().Delete(FALocationID);
        }
        public static List<FALocation> GetFALocationList(string filterExpression)
        {
            List<FALocation> result = new List<FALocation>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(FALocation));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((FALocation)helper.IDataReaderToObject(reader, new FALocation()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<FALocation> GetFALocationList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<FALocation> result = new List<FALocation>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(FALocation));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((FALocation)helper.IDataReaderToObject(reader, new FALocation()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetFALocationRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(FALocation));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetFALocationRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(FALocation));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "FALocationID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetFALocationMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(FALocation));
                ctx.CommandText = helper.SelectMaxColumn("FALocationID");
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
        #region FAWriteOff
        public static FAWriteOff GetFAWriteOff(Int32 FAWriteOffID)
        {
            return new FAWriteOffDao().Get(FAWriteOffID);
        }
        public static int InsertFAWriteOff(FAWriteOff record)
        {
            return new FAWriteOffDao().Insert(record);
        }
        public static int UpdateFAWriteOff(FAWriteOff record)
        {
            return new FAWriteOffDao().Update(record);
        }
        public static int DeleteFAWriteOff(Int32 FAWriteOffID)
        {
            return new FAWriteOffDao().Delete(FAWriteOffID);
        }
        public static List<FAWriteOff> GetFAWriteOffList(string filterExpression)
        {
            List<FAWriteOff> result = new List<FAWriteOff>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(FAWriteOff));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((FAWriteOff)helper.IDataReaderToObject(reader, new FAWriteOff()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<FAWriteOff> GetFAWriteOffList(string filterExpression, IDbContext ctx)
        {
            List<FAWriteOff> result = new List<FAWriteOff>();
            try
            {
                DbHelper helper = new DbHelper(typeof(FAWriteOff));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((FAWriteOff)helper.IDataReaderToObject(reader, new FAWriteOff()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region FamilyStatus
        public static FamilyStatus GetFamilyStatus(Int32 FamilyStatusID)
        {
            return new FamilyStatusDao().Get(FamilyStatusID);
        }
        public static int InsertFamilyStatus(FamilyStatus record)
        {
            return new FamilyStatusDao().Insert(record);
        }
        public static int UpdateFamilyStatus(FamilyStatus record)
        {
            return new FamilyStatusDao().Update(record);
        }
        public static int DeleteFamilyStatus(Int32 FamilyStatusID)
        {
            return new FamilyStatusDao().Delete(FamilyStatusID);
        }
        public static List<FamilyStatus> GetFamilyStatusList(string filterExpression)
        {
            List<FamilyStatus> result = new List<FamilyStatus>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(FamilyStatus));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((FamilyStatus)helper.IDataReaderToObject(reader, new FamilyStatus()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<FamilyStatus> GetFamilyStatusList(string filterExpression, IDbContext ctx)
        {
            List<FamilyStatus> result = new List<FamilyStatus>();
            try
            {
                DbHelper helper = new DbHelper(typeof(FamilyStatus));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((FamilyStatus)helper.IDataReaderToObject(reader, new FamilyStatus()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region GLAccountPayable
        public static GLAccountPayable GetGLAccountPayable(Int32 ID)
        {
            return new GLAccountPayableDao().Get(ID);
        }
        public static int InsertGLAccountPayable(GLAccountPayable record)
        {
            return new GLAccountPayableDao().Insert(record);
        }
        public static int UpdateGLAccountPayable(GLAccountPayable record)
        {
            return new GLAccountPayableDao().Update(record);
        }
        public static int DeleteGLAccountPayable(Int32 ID)
        {
            return new GLAccountPayableDao().Delete(ID);
        }
        public static List<GLAccountPayable> GetGLAccountPayableList(string filterExpression)
        {
            List<GLAccountPayable> result = new List<GLAccountPayable>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GLAccountPayable));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GLAccountPayable)helper.IDataReaderToObject(reader, new GLAccountPayable()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetGLAccountPayableMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(GLAccountPayable));
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
        #region GLAccountPayableDt
        public static GLAccountPayableDt GetGLAccountPayableDt(Int32 ID, Int32 BusinessPartnerID)
        {
            return new GLAccountPayableDtDao().Get(ID, BusinessPartnerID);
        }
        public static int InsertGLAccountPayableDt(GLAccountPayableDt record)
        {
            return new GLAccountPayableDtDao().Insert(record);
        }
        public static int UpdateGLAccountPayableDt(GLAccountPayableDt record)
        {
            return new GLAccountPayableDtDao().Update(record);
        }
        public static int DeleteGLAccountPayableDt(Int32 ID, Int32 BusinessPartnerID)
        {
            return new GLAccountPayableDtDao().Delete(ID, BusinessPartnerID);
        }
        public static List<GLAccountPayableDt> GetGLAccountPayableDtList(string filterExpression)
        {
            List<GLAccountPayableDt> result = new List<GLAccountPayableDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GLAccountPayableDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GLAccountPayableDt)helper.IDataReaderToObject(reader, new GLAccountPayableDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region GLAPOther
        public static GLAPOther GetGLAPOther(Int32 ID)
        {
            return new GLAPOtherDao().Get(ID);
        }
        public static int InsertGLAPOther(GLAPOther record)
        {
            return new GLAPOtherDao().Insert(record);
        }
        public static int UpdateGLAPOther(GLAPOther record)
        {
            return new GLAPOtherDao().Update(record);
        }
        public static int DeleteGLAPOther(Int32 ID)
        {
            return new GLAPOtherDao().Delete(ID);
        }
        public static List<GLAPOther> GetGLAPOtherList(string filterExpression)
        {
            List<GLAPOther> result = new List<GLAPOther>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GLAPOther));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GLAPOther)helper.IDataReaderToObject(reader, new GLAPOther()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetGLAPOtherMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(GLAPOther));
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
        #region GLAPPayment
        public static GLAPPayment GetGLAPPayment(Int32 ID)
        {
            return new GLAPPaymentDao().Get(ID);
        }
        public static int InsertGLAPPayment(GLAPPayment record)
        {
            return new GLAPPaymentDao().Insert(record);
        }
        public static int UpdateGLAPPayment(GLAPPayment record)
        {
            return new GLAPPaymentDao().Update(record);
        }
        public static int DeleteGLAPPayment(Int32 ID)
        {
            return new GLAPPaymentDao().Delete(ID);
        }
        public static List<GLAPPayment> GetGLAPPaymentList(string filterExpression)
        {
            List<GLAPPayment> result = new List<GLAPPayment>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GLAPPayment));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GLAPPayment)helper.IDataReaderToObject(reader, new GLAPPayment()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetGLAPPaymentMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(GLAPPayment));
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
        #region GLBalanceDtDocument
        public static GLBalanceDtDocument GetGLBalanceDtDocument(Int32 ID)
        {
            return new GLBalanceDtDocumentDao().Get(ID);
        }
        public static int InsertGLBalanceDtDocument(GLBalanceDtDocument record)
        {
            return new GLBalanceDtDocumentDao().Insert(record);
        }
        public static int UpdateGLBalanceDtDocument(GLBalanceDtDocument record)
        {
            return new GLBalanceDtDocumentDao().Update(record);
        }
        public static int DeleteGLBalanceDtDocument(Int32 ID)
        {
            return new GLBalanceDtDocumentDao().Delete(ID);
        }
        public static List<GLBalanceDtDocument> GetGLBalanceDtDocumentList(string filterExpression)
        {
            List<GLBalanceDtDocument> result = new List<GLBalanceDtDocument>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GLBalanceDtDocument));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GLBalanceDtDocument)helper.IDataReaderToObject(reader, new GLBalanceDtDocument()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<GLBalanceDtDocument> GetGLBalanceDtDocumentList(string filterExpression, IDbContext ctx)
        {
            List<GLBalanceDtDocument> result = new List<GLBalanceDtDocument>();
            try
            {
                DbHelper helper = new DbHelper(typeof(GLBalanceDtDocument));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GLBalanceDtDocument)helper.IDataReaderToObject(reader, new GLBalanceDtDocument()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region GLFAWriteOffAccount
        public static GLFAWriteOffAccount GetGLFAWriteOffAccount(Int32 ID)
        {
            return new GLFAWriteOffAccountDao().Get(ID);
        }
        public static int InsertGLFAWriteOffAccount(GLFAWriteOffAccount record)
        {
            return new GLFAWriteOffAccountDao().Insert(record);
        }
        public static int UpdateGLFAWriteOffAccount(GLFAWriteOffAccount record)
        {
            return new GLFAWriteOffAccountDao().Update(record);
        }
        public static int DeleteGLFAWriteOffAccount(Int32 ID)
        {
            return new GLFAWriteOffAccountDao().Delete(ID);
        }
        public static List<GLFAWriteOffAccount> GetGLFAWriteOffAccountList(string filterExpression)
        {
            List<GLFAWriteOffAccount> result = new List<GLFAWriteOffAccount>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GLFAWriteOffAccount));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GLFAWriteOffAccount)helper.IDataReaderToObject(reader, new GLFAWriteOffAccount()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetGLFAWriteOffAccountMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(GLFAWriteOffAccount));
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
        #region GLSetting
        public static GLSetting GetGLSetting(String SiteID, String GLSettingCode)
        {
            return new GLSettingDao().Get(SiteID, GLSettingCode);
        }
        public static int InsertGLSetting(GLSetting record)
        {
            return new GLSettingDao().Insert(record);
        }
        public static int UpdateGLSetting(GLSetting record)
        {
            return new GLSettingDao().Update(record);
        }
        public static int DeleteGLSetting(String SiteID, String GLSettingCode)
        {
            return new GLSettingDao().Delete(SiteID, GLSettingCode);
        }
        public static List<GLSetting> GetGLSettingList(string filterExpression)
        {
            List<GLSetting> result = new List<GLSetting>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GLSetting));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GLSetting)helper.IDataReaderToObject(reader, new GLSetting()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region GLTransactionDt
        public static GLTransactionDt GetGLTransactionDt(Int32 TransactionDtID)
        {
            return new GLTransactionDtDao().Get(TransactionDtID);
        }
        public static int InsertGLTransactionDt(GLTransactionDt record)
        {
            return new GLTransactionDtDao().Insert(record);
        }
        public static int UpdateGLTransactionDt(GLTransactionDt record)
        {
            return new GLTransactionDtDao().Update(record);
        }
        public static int DeleteGLTransactionDt(Int32 TransactionDtID)
        {
            return new GLTransactionDtDao().Delete(TransactionDtID);
        }
        public static List<GLTransactionDt> GetGLTransactionDtList(string filterExpression)
        {
            List<GLTransactionDt> result = new List<GLTransactionDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GLTransactionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GLTransactionDt)helper.IDataReaderToObject(reader, new GLTransactionDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<GLTransactionDt> GetGLTransactionDtList(string filterExpression, IDbContext ctx)
        {
            List<GLTransactionDt> result = new List<GLTransactionDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(GLTransactionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GLTransactionDt)helper.IDataReaderToObject(reader, new GLTransactionDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<GLTransactionDt> GetGLTransactionDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<GLTransactionDt> result = new List<GLTransactionDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GLTransactionDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GLTransactionDt)helper.IDataReaderToObject(reader, new GLTransactionDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetGLTransactionDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GLTransactionDt));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetGLTransactionDtRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GLTransactionDt));
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
        #region GLTransactionHd
        public static GLTransactionHd GetGLTransactionHd(Int32 GLTransactionID)
        {
            return new GLTransactionHdDao().Get(GLTransactionID);
        }
        public static int InsertGLTransactionHd(GLTransactionHd record)
        {
            return new GLTransactionHdDao().Insert(record);
        }
        public static int UpdateGLTransactionHd(GLTransactionHd record)
        {
            return new GLTransactionHdDao().Update(record);
        }
        public static int DeleteGLTransactionHd(Int32 GLTransactionID)
        {
            return new GLTransactionHdDao().Delete(GLTransactionID);
        }
        public static List<GLTransactionHd> GetGLTransactionHdList(string filterExpression)
        {
            List<GLTransactionHd> result = new List<GLTransactionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GLTransactionHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GLTransactionHd)helper.IDataReaderToObject(reader, new GLTransactionHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetGLTransactionMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(GLTransactionHd));
                ctx.CommandText = helper.SelectMaxColumn("GLTransactionID");
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
        #region GLWarehouseProductLineAccount
        public static GLWarehouseProductLineAccount GetGLWarehouseProductLineAccount(Int32 ID)
        {
            return new GLWarehouseProductLineAccountDao().Get(ID);
        }
        public static int InsertGLWarehouseProductLineAccount(GLWarehouseProductLineAccount record)
        {
            return new GLWarehouseProductLineAccountDao().Insert(record);
        }
        public static int UpdateGLWarehouseProductLineAccount(GLWarehouseProductLineAccount record)
        {
            return new GLWarehouseProductLineAccountDao().Update(record);
        }
        public static int DeleteGLWarehouseProductLineAccount(Int32 ID)
        {
            return new GLWarehouseProductLineAccountDao().Delete(ID);
        }
        public static List<GLWarehouseProductLineAccount> GetGLWarehouseProductLineAccountList(string filterExpression)
        {
            List<GLWarehouseProductLineAccount> result = new List<GLWarehouseProductLineAccount>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GLWarehouseProductLineAccount));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GLWarehouseProductLineAccount)helper.IDataReaderToObject(reader, new GLWarehouseProductLineAccount()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetGLWarehouseProductLineAccountMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(GLWarehouseProductLineAccount));
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
        #region GLWarehouseProductLineAccountDt
        public static GLWarehouseProductLineAccountDt GetGLWarehouseProductLineAccountDt(Int32 ID, Int32 LocationID)
        {
            return new GLWarehouseProductLineAccountDtDao().Get(ID, LocationID);
        }
        public static int InsertGLWarehouseProductLineAccountDt(GLWarehouseProductLineAccountDt record)
        {
            return new GLWarehouseProductLineAccountDtDao().Insert(record);
        }
        public static int UpdateGLWarehouseProductLineAccountDt(GLWarehouseProductLineAccountDt record)
        {
            return new GLWarehouseProductLineAccountDtDao().Update(record);
        }
        public static int DeleteGLWarehouseProductLineAccountDt(Int32 ID, Int32 LocationID)
        {
            return new GLWarehouseProductLineAccountDtDao().Delete(ID, LocationID);
        }
        public static List<GLWarehouseProductLineAccountDt> GetGLWarehouseProductLineAccountDtList(string filterExpression)
        {
            List<GLWarehouseProductLineAccountDt> result = new List<GLWarehouseProductLineAccountDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GLWarehouseProductLineAccountDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GLWarehouseProductLineAccountDt)helper.IDataReaderToObject(reader, new GLWarehouseProductLineAccountDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region GradePromotionFormulaDt
        public static GradePromotionFormulaDt GetGradePromotionFormulaDt(Int32 GradePromotionFormulaDtID)
        {
            return new GradePromotionFormulaDtDao().Get(GradePromotionFormulaDtID);
        }
        public static int InsertGradePromotionFormulaDt(GradePromotionFormulaDt record)
        {
            return new GradePromotionFormulaDtDao().Insert(record);
        }
        public static int UpdateGradePromotionFormulaDt(GradePromotionFormulaDt record)
        {
            return new GradePromotionFormulaDtDao().Update(record);
        }
        public static int DeleteGradePromotionFormulaDt(Int32 GradePromotionFormulaDtID)
        {
            return new GradePromotionFormulaDtDao().Delete(GradePromotionFormulaDtID);
        }
        public static List<GradePromotionFormulaDt> GetGradePromotionFormulaDtList(string filterExpression)
        {
            List<GradePromotionFormulaDt> result = new List<GradePromotionFormulaDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GradePromotionFormulaDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GradePromotionFormulaDt)helper.IDataReaderToObject(reader, new GradePromotionFormulaDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region GradePromotionFormulaHd
        public static GradePromotionFormulaHd GetGradePromotionFormulaHd(Int32 GradePromotionFormulaID)
        {
            return new GradePromotionFormulaHdDao().Get(GradePromotionFormulaID);
        }
        public static int InsertGradePromotionFormulaHd(GradePromotionFormulaHd record)
        {
            return new GradePromotionFormulaHdDao().Insert(record);
        }
        public static int UpdateGradePromotionFormulaHd(GradePromotionFormulaHd record)
        {
            return new GradePromotionFormulaHdDao().Update(record);
        }
        public static int DeleteGradePromotionFormulaHd(Int32 GradePromotionFormulaID)
        {
            return new GradePromotionFormulaHdDao().Delete(GradePromotionFormulaID);
        }
        public static List<GradePromotionFormulaHd> GetGradePromotionFormulaHdList(string filterExpression)
        {
            List<GradePromotionFormulaHd> result = new List<GradePromotionFormulaHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GradePromotionFormulaHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GradePromotionFormulaHd)helper.IDataReaderToObject(reader, new GradePromotionFormulaHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<GradePromotionFormulaHd> GetGradePromotionFormulaHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<GradePromotionFormulaHd> result = new List<GradePromotionFormulaHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GradePromotionFormulaHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GradePromotionFormulaHd)helper.IDataReaderToObject(reader, new GradePromotionFormulaHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetGradePromotionFormulaHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GradePromotionFormulaHd));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetGradePromotionFormulaHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GradePromotionFormulaHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "GradePromotionFormulaID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetGradePromotionFormulaHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(GradePromotionFormulaHd));
                ctx.CommandText = helper.SelectMaxColumn("GradePromotionFormulaID");
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
        #region Holiday
        public static Holiday GetHoliday(Int32 ID)
        {
            return new HolidayDao().Get(ID);
        }
        public static int InsertHoliday(Holiday record)
        {
            return new HolidayDao().Insert(record);
        }
        public static int UpdateHoliday(Holiday record)
        {
            return new HolidayDao().Update(record);
        }
        public static int DeleteHoliday(Int32 ID)
        {
            return new HolidayDao().Delete(ID);
        }
        public static List<Holiday> GetHolidayList(string filterExpression)
        {
            List<Holiday> result = new List<Holiday>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Holiday));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Holiday)helper.IDataReaderToObject(reader, new Holiday()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<Holiday> GetHolidayList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<Holiday> result = new List<Holiday>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Holiday));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Holiday)helper.IDataReaderToObject(reader, new Holiday()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetHolidayRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Holiday));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetHolidayRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Holiday));
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
        public static Int32 GetHolidayMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(Holiday));
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
        #region HRDailyScheduleHd
        public static HRDailyScheduleHd GetHRDailyScheduleHd(Int32 DailyScheduleID)
        {
            return new HRDailyScheduleHdDao().Get(DailyScheduleID);
        }
        public static int InsertHRDailyScheduleHd(HRDailyScheduleHd record)
        {
            return new HRDailyScheduleHdDao().Insert(record);
        }
        public static int UpdateHRDailyScheduleHd(HRDailyScheduleHd record)
        {
            return new HRDailyScheduleHdDao().Update(record);
        }
        public static int DeleteHRDailyScheduleHd(Int32 DailyScheduleID)
        {
            return new HRDailyScheduleHdDao().Delete(DailyScheduleID);
        }
        public static List<HRDailyScheduleHd> GetHRDailyScheduleHdList(string filterExpression)
        {
            List<HRDailyScheduleHd> result = new List<HRDailyScheduleHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(HRDailyScheduleHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((HRDailyScheduleHd)helper.IDataReaderToObject(reader, new HRDailyScheduleHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<HRDailyScheduleHd> GetHRDailyScheduleHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<HRDailyScheduleHd> result = new List<HRDailyScheduleHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(HRDailyScheduleHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((HRDailyScheduleHd)helper.IDataReaderToObject(reader, new HRDailyScheduleHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetHRDailyScheduleHdCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(HRDailyScheduleHd));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetHRDailyScheduleHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(HRDailyScheduleHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "DailyScheduleID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region HRScheduleGroupDate
        public static HRScheduleGroupDate GetHRScheduleGroupDate(Int32 TransactionDtID)
        {
            return new HRScheduleGroupDateDao().Get(TransactionDtID);
        }
        public static int InsertHRScheduleGroupDate(HRScheduleGroupDate record)
        {
            return new HRScheduleGroupDateDao().Insert(record);
        }
        public static int UpdateHRScheduleGroupDate(HRScheduleGroupDate record)
        {
            return new HRScheduleGroupDateDao().Update(record);
        }
        public static int DeleteHRScheduleGroupDate(Int32 TransactionDtID)
        {
            return new HRScheduleGroupDateDao().Delete(TransactionDtID);
        }
        public static List<HRScheduleGroupDate> GetHRScheduleGroupDateList(string filterExpression)
        {
            List<HRScheduleGroupDate> result = new List<HRScheduleGroupDate>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(HRScheduleGroupDate));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((HRScheduleGroupDate)helper.IDataReaderToObject(reader, new HRScheduleGroupDate()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<HRScheduleGroupDate> GetHRScheduleGroupDateList(string filterExpression, IDbContext ctx)
        {
            List<HRScheduleGroupDate> result = new List<HRScheduleGroupDate>();
            try
            {
                DbHelper helper = new DbHelper(typeof(HRScheduleGroupDate));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((HRScheduleGroupDate)helper.IDataReaderToObject(reader, new HRScheduleGroupDate()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetHRScheduleGroupDateMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(HRScheduleGroupDate));
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
        #region HRScheduleGroupEmployee
        public static HRScheduleGroupEmployee GetHRScheduleGroupEmployee(Int32 TransactionID, Int32 EmployeeID)
        {
            return new HRScheduleGroupEmployeeDao().Get(TransactionID, EmployeeID);
        }
        public static int InsertHRScheduleGroupEmployee(HRScheduleGroupEmployee record)
        {
            return new HRScheduleGroupEmployeeDao().Insert(record);
        }
        public static int UpdateHRScheduleGroupEmployee(HRScheduleGroupEmployee record)
        {
            return new HRScheduleGroupEmployeeDao().Update(record);
        }
        public static int DeleteHRScheduleGroupEmployee(Int32 TransactionID, Int32 EmployeeID)
        {
            return new HRScheduleGroupEmployeeDao().Delete(TransactionID, EmployeeID);
        }
        public static List<HRScheduleGroupEmployee> GetHRScheduleGroupEmployeeList(string filterExpression)
        {
            List<HRScheduleGroupEmployee> result = new List<HRScheduleGroupEmployee>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(HRScheduleGroupEmployee));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((HRScheduleGroupEmployee)helper.IDataReaderToObject(reader, new HRScheduleGroupEmployee()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<HRScheduleGroupEmployee> GetHRScheduleGroupEmployeeList(string filterExpression, IDbContext ctx)
        {
            List<HRScheduleGroupEmployee> result = new List<HRScheduleGroupEmployee>();
            try
            {
                DbHelper helper = new DbHelper(typeof(HRScheduleGroupEmployee));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((HRScheduleGroupEmployee)helper.IDataReaderToObject(reader, new HRScheduleGroupEmployee()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetHRScheduleGroupEmployeeMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(HRScheduleGroupEmployee));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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
        #region HRScheduleGroupHd
        public static HRScheduleGroupHd GetHRScheduleGroupHd(Int32 TransactionID)
        {
            return new HRScheduleGroupHdDao().Get(TransactionID);
        }
        public static int InsertHRScheduleGroupHd(HRScheduleGroupHd record)
        {
            return new HRScheduleGroupHdDao().Insert(record);
        }
        public static int UpdateHRScheduleGroupHd(HRScheduleGroupHd record)
        {
            return new HRScheduleGroupHdDao().Update(record);
        }
        public static int DeleteHRScheduleGroupHd(Int32 TransactionID)
        {
            return new HRScheduleGroupHdDao().Delete(TransactionID);
        }
        public static List<HRScheduleGroupHd> GetHRScheduleGroupHdList(string filterExpression)
        {
            List<HRScheduleGroupHd> result = new List<HRScheduleGroupHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(HRScheduleGroupHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((HRScheduleGroupHd)helper.IDataReaderToObject(reader, new HRScheduleGroupHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<HRScheduleGroupHd> GetHRScheduleGroupHdList(string filterExpression, IDbContext ctx)
        {
            List<HRScheduleGroupHd> result = new List<HRScheduleGroupHd>();
            try
            {
                DbHelper helper = new DbHelper(typeof(HRScheduleGroupHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((HRScheduleGroupHd)helper.IDataReaderToObject(reader, new HRScheduleGroupHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetHRScheduleGroupHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(HRScheduleGroupHd));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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
        #region HRDailyScheduleDt
        public static HRDailyScheduleDt GetHRDailyScheduleDt(Int32 DailyScheduleDtID)
        {
            return new HRDailyScheduleDtDao().Get(DailyScheduleDtID);
        }
        public static int InsertHRDailyScheduleDt(HRDailyScheduleDt record)
        {
            return new HRDailyScheduleDtDao().Insert(record);
        }
        public static int UpdateHRDailyScheduleDt(HRDailyScheduleDt record)
        {
            return new HRDailyScheduleDtDao().Update(record);
        }
        public static int DeleteHRDailyScheduleDt(Int32 DailyScheduleDtID)
        {
            return new HRDailyScheduleDtDao().Delete(DailyScheduleDtID);
        }
        public static List<HRDailyScheduleDt> GetHRDailyScheduleDtList(string filterExpression)
        {
            List<HRDailyScheduleDt> result = new List<HRDailyScheduleDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(HRDailyScheduleDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((HRDailyScheduleDt)helper.IDataReaderToObject(reader, new HRDailyScheduleDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region HRWeeklySchedule
        public static HRWeeklySchedule GetHRWeeklySchedule(Int32 WeeklyScheduleID)
        {
            return new HRWeeklyScheduleDao().Get(WeeklyScheduleID);
        }
        public static int InsertHRWeeklySchedule(HRWeeklySchedule record)
        {
            return new HRWeeklyScheduleDao().Insert(record);
        }
        public static int UpdateHRWeeklySchedule(HRWeeklySchedule record)
        {
            return new HRWeeklyScheduleDao().Update(record);
        }
        public static int DeleteHRWeeklySchedule(Int32 WeeklyScheduleID)
        {
            return new HRWeeklyScheduleDao().Delete(WeeklyScheduleID);
        }
        public static List<HRWeeklySchedule> GetHRWeeklyScheduleList(string filterExpression)
        {
            List<HRWeeklySchedule> result = new List<HRWeeklySchedule>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(HRWeeklySchedule));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((HRWeeklySchedule)helper.IDataReaderToObject(reader, new HRWeeklySchedule()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<HRWeeklySchedule> GetHRWeeklyScheduleList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<HRWeeklySchedule> result = new List<HRWeeklySchedule>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(HRWeeklySchedule));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((HRWeeklySchedule)helper.IDataReaderToObject(reader, new HRWeeklySchedule()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetHRWeeklyScheduleCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(HRWeeklySchedule));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetHRWeeklyScheduleRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(HRWeeklySchedule));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "WeeklyScheduleID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ItemAlternateUnit
        public static ItemAlternateUnit GetItemAlternateUnit(Int32 ID)
        {
            return new ItemAlternateUnitDao().Get(ID);
        }
        public static int InsertItemAlternateUnit(ItemAlternateUnit record)
        {
            return new ItemAlternateUnitDao().Insert(record);
        }
        public static int UpdateItemAlternateUnit(ItemAlternateUnit record)
        {
            return new ItemAlternateUnitDao().Update(record);
        }
        public static int DeleteItemAlternateUnit(Int32 ID)
        {
            return new ItemAlternateUnitDao().Delete(ID);
        }
        public static List<ItemAlternateUnit> GetItemAlternateUnitList(string filterExpression, IDbContext ctx)
        {
            List<ItemAlternateUnit> result = new List<ItemAlternateUnit>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemAlternateUnit));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemAlternateUnit)helper.IDataReaderToObject(reader, new ItemAlternateUnit()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<ItemAlternateUnit> GetItemAlternateUnitList(string filterExpression)
        {
            List<ItemAlternateUnit> result = new List<ItemAlternateUnit>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemAlternateUnit));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemAlternateUnit)helper.IDataReaderToObject(reader, new ItemAlternateUnit()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ItemAlternateUnit> GetItemAlternateUnitList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<ItemAlternateUnit> result = new List<ItemAlternateUnit>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemAlternateUnit));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemAlternateUnit)helper.IDataReaderToObject(reader, new ItemAlternateUnit()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ItemBalance
        public static ItemBalance GetItemBalance(Int32 ID)
        {
            return new ItemBalanceDao().Get(ID);
        }
        public static int InsertItemBalance(ItemBalance record)
        {
            return new ItemBalanceDao().Insert(record);
        }
        public static int UpdateItemBalance(ItemBalance record)
        {
            return new ItemBalanceDao().Update(record);
        }
        public static int DeleteItemBalance(Int32 ID)
        {
            return new ItemBalanceDao().Delete(ID);
        }
        public static List<ItemBalance> GetItemBalanceList(string filterExpression)
        {
            List<ItemBalance> result = new List<ItemBalance>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemBalance));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemBalance)helper.IDataReaderToObject(reader, new ItemBalance()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ItemBalance> GetItemBalanceList(string filterExpression, IDbContext ctx)
        {
            List<ItemBalance> result = new List<ItemBalance>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemBalance));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemBalance)helper.IDataReaderToObject(reader, new ItemBalance()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Decimal GetItemBalanceSumQuantityEND(string filterExpression)
        {
            Decimal result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemBalance));
                ctx.CommandText = helper.SelectSumColumn("QuantityEND", filterExpression);
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
        #region ItemDistributionDt
        public static ItemDistributionDt GetItemDistributionDt(Int32 ID)
        {
            return new ItemDistributionDtDao().Get(ID);
        }
        public static int InsertItemDistributionDt(ItemDistributionDt record)
        {
            return new ItemDistributionDtDao().Insert(record);
        }
        public static int UpdateItemDistributionDt(ItemDistributionDt record)
        {
            return new ItemDistributionDtDao().Update(record);
        }
        public static int DeleteItemDistributionDt(Int32 ID)
        {
            return new ItemDistributionDtDao().Delete(ID);
        }
        public static List<ItemDistributionDt> GetItemDistributionDtList(string filterExpression)
        {
            List<ItemDistributionDt> result = new List<ItemDistributionDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemDistributionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemDistributionDt)helper.IDataReaderToObject(reader, new ItemDistributionDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ItemDistributionDt> GetItemDistributionDtList(string filterExpression, IDbContext ctx)
        {
            List<ItemDistributionDt> result = new List<ItemDistributionDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemDistributionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemDistributionDt)helper.IDataReaderToObject(reader, new ItemDistributionDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ItemDistributionHd
        public static ItemDistributionHd GetItemDistributionHd(Int32 DistributionID)
        {
            return new ItemDistributionHdDao().Get(DistributionID);
        }
        public static int InsertItemDistributionHd(ItemDistributionHd record)
        {
            return new ItemDistributionHdDao().Insert(record);
        }
        public static int UpdateItemDistributionHd(ItemDistributionHd record)
        {
            return new ItemDistributionHdDao().Update(record);
        }
        public static int DeleteItemDistributionHd(Int32 DistributionID)
        {
            return new ItemDistributionHdDao().Delete(DistributionID);
        }
        public static List<ItemDistributionHd> GetItemDistributionHdList(string filterExpression)
        {
            List<ItemDistributionHd> result = new List<ItemDistributionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemDistributionHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemDistributionHd)helper.IDataReaderToObject(reader, new ItemDistributionHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetItemDistributionHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemDistributionHd));
                ctx.CommandText = helper.SelectMaxColumn("DistributionID");
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
        #region ItemCost
        public static ItemCost GetItemCost(Int32 ItemCostID)
        {
            return new ItemCostDao().Get(ItemCostID);
        }
        public static int InsertItemCost(ItemCost record)
        {
            return new ItemCostDao().Insert(record);
        }
        public static int UpdateItemCost(ItemCost record)
        {
            return new ItemCostDao().Update(record);
        }
        public static int DeleteItemCost(Int32 ItemCostID)
        {
            return new ItemCostDao().Delete(ItemCostID);
        }
        public static List<ItemCost> GetItemCostList(string filterExpression)
        {
            List<ItemCost> result = new List<ItemCost>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemCost));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemCost)helper.IDataReaderToObject(reader, new ItemCost()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ItemCost> GetItemCostList(string filterExpression, IDbContext ctx)
        {
            List<ItemCost> result = new List<ItemCost>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemCost));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemCost)helper.IDataReaderToObject(reader, new ItemCost()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ItemGroupMaster
        public static ItemGroupMaster GetItemGroupMaster(Int32 ItemGroupID)
        {
            return new ItemGroupMasterDao().Get(ItemGroupID);
        }
        public static int InsertItemGroupMaster(ItemGroupMaster record)
        {
            return new ItemGroupMasterDao().Insert(record);
        }
        public static int UpdateItemGroupMaster(ItemGroupMaster record)
        {
            return new ItemGroupMasterDao().Update(record);
        }
        public static int DeleteItemGroupMaster(Int32 ItemGroupID)
        {
            return new ItemGroupMasterDao().Delete(ItemGroupID);
        }
        public static List<ItemGroupMaster> GetItemGroupMasterList(string filterExpression)
        {
            List<ItemGroupMaster> result = new List<ItemGroupMaster>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemGroupMaster));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemGroupMaster)helper.IDataReaderToObject(reader, new ItemGroupMaster()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetItemGroupMasterMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemGroupMaster));
                ctx.CommandText = helper.SelectMaxColumn("ItemGroupID");
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
        #region ItemGroupPlanning
        public static ItemGroupPlanning GetItemGroupPlanning(String SiteID, Int32 ItemGroupID)
        {
            return new ItemGroupPlanningDao().Get(SiteID, ItemGroupID);
        }
        public static int InsertItemGroupPlanning(ItemGroupPlanning record)
        {
            return new ItemGroupPlanningDao().Insert(record);
        }
        public static int UpdateItemGroupPlanning(ItemGroupPlanning record)
        {
            return new ItemGroupPlanningDao().Update(record);
        }
        public static int DeleteItemGroupPlanning(String SiteID, Int32 ItemGroupID)
        {
            return new ItemGroupPlanningDao().Delete(SiteID, ItemGroupID);
        }
        public static List<ItemGroupPlanning> GetItemGroupPlanningList(string filterExpression)
        {
            List<ItemGroupPlanning> result = new List<ItemGroupPlanning>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemGroupPlanning));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemGroupPlanning)helper.IDataReaderToObject(reader, new ItemGroupPlanning()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ItemMaster
        public static ItemMaster GetItemMaster(Int32 ItemID)
        {
            return new ItemMasterDao().Get(ItemID);
        }
        public static int InsertItemMaster(ItemMaster record)
        {
            return new ItemMasterDao().Insert(record);
        }
        public static int UpdateItemMaster(ItemMaster record)
        {
            return new ItemMasterDao().Update(record);
        }
        public static int DeleteItemMaster(Int32 ItemID)
        {
            return new ItemMasterDao().Delete(ItemID);
        }
        public static List<ItemMaster> GetItemMasterList(string filterExpression, IDbContext ctx)
        {
            List<ItemMaster> result = new List<ItemMaster>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemMaster));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemMaster)helper.IDataReaderToObject(reader, new ItemMaster()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<ItemMaster> GetItemMasterList(string filterExpression)
        {
            List<ItemMaster> result = new List<ItemMaster>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemMaster));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemMaster)helper.IDataReaderToObject(reader, new ItemMaster()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ItemMaster> GetItemMasterList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<ItemMaster> result = new List<ItemMaster>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemMaster));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemMaster)helper.IDataReaderToObject(reader, new ItemMaster()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ItemMaster> GetItemMasterList(string filterExpression, int numRows, int pageIndex, string orderByExpression, IDbContext ctx)
        {
            List<ItemMaster> result = new List<ItemMaster>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemMaster));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemMaster)helper.IDataReaderToObject(reader, new ItemMaster()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetItemMasterRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemMaster));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetItemMasterRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemMaster));
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
        public static Int32 GetItemMasterMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemMaster));
                ctx.CommandText = helper.SelectMaxColumn("ItemID");
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
        #region ItemPlanning
        public static ItemPlanning GetItemPlanning(Int32 ID)
        {
            return new ItemPlanningDao().Get(ID);
        }
        public static int InsertItemPlanning(ItemPlanning record)
        {
            return new ItemPlanningDao().Insert(record);
        }
        public static int UpdateItemPlanning(ItemPlanning record)
        {
            return new ItemPlanningDao().Update(record);
        }
        public static int DeleteItemPlanning(Int32 ID)
        {
            return new ItemPlanningDao().Delete(ID);
        }
        public static List<ItemPlanning> GetItemPlanningList(string filterExpression)
        {
            List<ItemPlanning> result = new List<ItemPlanning>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemPlanning));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemPlanning)helper.IDataReaderToObject(reader, new ItemPlanning()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ItemPlanning> GetItemPlanningList(string filterExpression, IDbContext ctx)
        {
            List<ItemPlanning> result = new List<ItemPlanning>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemPlanning));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemPlanning)helper.IDataReaderToObject(reader, new ItemPlanning()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<ItemPlanning> GetItemPlanningList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<ItemPlanning> result = new List<ItemPlanning>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemPlanning));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemPlanning)helper.IDataReaderToObject(reader, new ItemPlanning()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ItemProduct
        public static ItemProduct GetItemProduct(Int32 ItemID)
        {
            return new ItemProductDao().Get(ItemID);
        }
        public static int InsertItemProduct(ItemProduct record)
        {
            return new ItemProductDao().Insert(record);
        }
        public static int UpdateItemProduct(ItemProduct record)
        {
            return new ItemProductDao().Update(record);
        }
        public static int DeleteItemProduct(Int32 ItemID)
        {
            return new ItemProductDao().Delete(ItemID);
        }
        public static List<ItemProduct> GetItemProductList(string filterExpression)
        {
            List<ItemProduct> result = new List<ItemProduct>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemProduct));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemProduct)helper.IDataReaderToObject(reader, new ItemProduct()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ItemProduct> GetItemProductList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<ItemProduct> result = new List<ItemProduct>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemProduct));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemProduct)helper.IDataReaderToObject(reader, new ItemProduct()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ItemRequestHd
        public static ItemRequestHd GetItemRequestHd(Int32 ItemRequestID)
        {
            return new ItemRequestHdDao().Get(ItemRequestID);
        }
        public static int InsertItemRequestHd(ItemRequestHd record)
        {
            return new ItemRequestHdDao().Insert(record);
        }
        public static int UpdateItemRequestHd(ItemRequestHd record)
        {
            return new ItemRequestHdDao().Update(record);
        }
        public static int DeleteItemRequestHd(Int32 ItemRequestID)
        {
            return new ItemRequestHdDao().Delete(ItemRequestID);
        }
        public static List<ItemRequestHd> GetItemRequestHdList(string filterExpression)
        {
            List<ItemRequestHd> result = new List<ItemRequestHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemRequestHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemRequestHd)helper.IDataReaderToObject(reader, new ItemRequestHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetItemRequestHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemRequestHd));
                ctx.CommandText = helper.SelectMaxColumn("ItemRequestID");
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
        #region ItemRequestDt
        public static ItemRequestDt GetItemRequestDt(Int32 ID)
        {
            return new ItemRequestDtDao().Get(ID);
        }
        public static int InsertItemRequestDt(ItemRequestDt record)
        {
            return new ItemRequestDtDao().Insert(record);
        }
        public static int UpdateItemRequestDt(ItemRequestDt record)
        {
            return new ItemRequestDtDao().Update(record);
        }
        public static int DeleteItemRequestDt(Int32 ID)
        {
            return new ItemRequestDtDao().Delete(ID);
        }
        public static List<ItemRequestDt> GetItemRequestDtList(string filterExpression)
        {
            List<ItemRequestDt> result = new List<ItemRequestDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemRequestDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemRequestDt)helper.IDataReaderToObject(reader, new ItemRequestDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ItemRequestDt> GetItemRequestDtList(string filterExpression, IDbContext ctx)
        {
            List<ItemRequestDt> result = new List<ItemRequestDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemRequestDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemRequestDt)helper.IDataReaderToObject(reader, new ItemRequestDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetItemRequestDtRowCount(string filterExpression, IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemRequestDt));
                ctx.CommandText = helper.GetRowCount(filterExpression);
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
        #region ItemTagField
        public static ItemTagField GetItemTagField(Int32 ItemID)
        {
            return new ItemTagFieldDao().Get(ItemID);
        }
        public static int InsertItemTagField(ItemTagField record)
        {
            return new ItemTagFieldDao().Insert(record);
        }
        public static int UpdateItemTagField(ItemTagField record)
        {
            return new ItemTagFieldDao().Update(record);
        }
        public static int DeleteItemTagField(Int32 ItemID)
        {
            return new ItemTagFieldDao().Delete(ItemID);
        }
        public static List<ItemTagField> GetItemTagFieldList(string filterExpression)
        {
            List<ItemTagField> result = new List<ItemTagField>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemTagField));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemTagField)helper.IDataReaderToObject(reader, new ItemTagField()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ItemTagField> GetItemTagFieldList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<ItemTagField> result = new List<ItemTagField>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemTagField));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemTagField)helper.IDataReaderToObject(reader, new ItemTagField()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ItemTariff
        public static ItemTariff GetItemTariff(Int32 ID)
        {
            return new ItemTariffDao().Get(ID);
        }
        public static int InsertItemTariff(ItemTariff record)
        {
            return new ItemTariffDao().Insert(record);
        }
        public static int UpdateItemTariff(ItemTariff record)
        {
            return new ItemTariffDao().Update(record);
        }
        public static int DeleteItemTariff(Int32 ID)
        {
            return new ItemTariffDao().Delete(ID);
        }
        public static List<ItemTariff> GetItemTariffList(string filterExpression)
        {
            List<ItemTariff> result = new List<ItemTariff>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemTariff));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemTariff)helper.IDataReaderToObject(reader, new ItemTariff()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetItemTariffMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemTariff));
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
        #region ItemTransactionDt
        public static ItemTransactionDt GetItemTransactionDt(Int32 ID)
        {
            return new ItemTransactionDtDao().Get(ID);
        }
        public static int InsertItemTransactionDt(ItemTransactionDt record)
        {
            return new ItemTransactionDtDao().Insert(record);
        }
        public static int UpdateItemTransactionDt(ItemTransactionDt record)
        {
            return new ItemTransactionDtDao().Update(record);
        }
        public static int DeleteItemTransactionDt(Int32 ID)
        {
            return new ItemTransactionDtDao().Delete(ID);
        }
        public static List<ItemTransactionDt> GetItemTransactionDtList(string filterExpression, IDbContext ctx)
        {
            List<ItemTransactionDt> result = new List<ItemTransactionDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemTransactionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemTransactionDt)helper.IDataReaderToObject(reader, new ItemTransactionDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<ItemTransactionDt> GetItemTransactionDtList(string filterExpression)
        {
            List<ItemTransactionDt> result = new List<ItemTransactionDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemTransactionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemTransactionDt)helper.IDataReaderToObject(reader, new ItemTransactionDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ItemTransactionHd
        public static ItemTransactionHd GetItemTransactionHd(Int32 TransactionID)
        {
            return new ItemTransactionHdDao().Get(TransactionID);
        }
        public static int InsertItemTransactionHd(ItemTransactionHd record)
        {
            return new ItemTransactionHdDao().Insert(record);
        }
        public static int UpdateItemTransactionHd(ItemTransactionHd record)
        {
            return new ItemTransactionHdDao().Update(record);
        }
        public static int DeleteItemTransactionHd(Int32 TransactionID)
        {
            return new ItemTransactionHdDao().Delete(TransactionID);
        }
        public static List<ItemTransactionHd> GetItemTransactionHdList(string filterExpression, IDbContext ctx)
        {
            List<ItemTransactionHd> result = new List<ItemTransactionHd>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemTransactionHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemTransactionHd)helper.IDataReaderToObject(reader, new ItemTransactionHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<ItemTransactionHd> GetItemTransactionHdList(string filterExpression)
        {
            List<ItemTransactionHd> result = new List<ItemTransactionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemTransactionHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ItemTransactionHd)helper.IDataReaderToObject(reader, new ItemTransactionHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetItemTransactionHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemTransactionHd));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetItemTransactionHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ItemTransactionHd));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region JobLevel
        public static JobLevel GetJobLevel(Int32 JobLevelID)
        {
            return new JobLevelDao().Get(JobLevelID);
        }
        public static int InsertJobLevel(JobLevel record)
        {
            return new JobLevelDao().Insert(record);
        }
        public static int UpdateJobLevel(JobLevel record)
        {
            return new JobLevelDao().Update(record);
        }
        public static int DeleteJobLevel(Int32 JobLevelID)
        {
            return new JobLevelDao().Delete(JobLevelID);
        }
        public static List<JobLevel> GetJobLevelList(string filterExpression)
        {
            List<JobLevel> result = new List<JobLevel>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(JobLevel));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((JobLevel)helper.IDataReaderToObject(reader, new JobLevel()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<JobLevel> GetJobLevelList(string filterExpression, IDbContext ctx)
        {
            List<JobLevel> result = new List<JobLevel>();
            try
            {
                DbHelper helper = new DbHelper(typeof(JobLevel));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((JobLevel)helper.IDataReaderToObject(reader, new JobLevel()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region JobLevelPosition
        public static JobLevelPosition GetJobLevelPosition(Int32 JobLevelPositionID)
        {
            return new JobLevelPositionDao().Get(JobLevelPositionID);
        }
        public static int InsertJobLevelPosition(JobLevelPosition record)
        {
            return new JobLevelPositionDao().Insert(record);
        }
        public static int UpdateJobLevelPosition(JobLevelPosition record)
        {
            return new JobLevelPositionDao().Update(record);
        }
        public static int DeleteJobLevelPosition(Int32 JobLevelPositionID)
        {
            return new JobLevelPositionDao().Delete(JobLevelPositionID);
        }
        public static List<JobLevelPosition> GetJobLevelPositionList(string filterExpression)
        {
            List<JobLevelPosition> result = new List<JobLevelPosition>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(JobLevelPosition));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((JobLevelPosition)helper.IDataReaderToObject(reader, new JobLevelPosition()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<JobLevelPosition> GetJobLevelPositionList(string filterExpression, IDbContext ctx)
        {
            List<JobLevelPosition> result = new List<JobLevelPosition>();
            try
            {
                DbHelper helper = new DbHelper(typeof(JobLevelPosition));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((JobLevelPosition)helper.IDataReaderToObject(reader, new JobLevelPosition()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region JobLevelPerformanceIndicator
        public static JobLevelPerformanceIndicator GetJobLevelPerformanceIndicator(Int32 JobLevelPerformanceIndicatorID)
        {
            return new JobLevelPerformanceIndicatorDao().Get(JobLevelPerformanceIndicatorID);
        }
        public static int InsertJobLevelPerformanceIndicator(JobLevelPerformanceIndicator record)
        {
            return new JobLevelPerformanceIndicatorDao().Insert(record);
        }
        public static int UpdateJobLevelPerformanceIndicator(JobLevelPerformanceIndicator record)
        {
            return new JobLevelPerformanceIndicatorDao().Update(record);
        }
        public static int DeleteJobLevelPerformanceIndicator(Int32 JobLevelPerformanceIndicatorID)
        {
            return new JobLevelPerformanceIndicatorDao().Delete(JobLevelPerformanceIndicatorID);
        }
        public static List<JobLevelPerformanceIndicator> GetJobLevelPerformanceIndicatorList(string filterExpression)
        {
            List<JobLevelPerformanceIndicator> result = new List<JobLevelPerformanceIndicator>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(JobLevelPerformanceIndicator));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((JobLevelPerformanceIndicator)helper.IDataReaderToObject(reader, new JobLevelPerformanceIndicator()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<JobLevelPerformanceIndicator> GetJobLevelPerformanceIndicatorList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<JobLevelPerformanceIndicator> result = new List<JobLevelPerformanceIndicator>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(JobLevelPerformanceIndicator));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((JobLevelPerformanceIndicator)helper.IDataReaderToObject(reader, new JobLevelPerformanceIndicator()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetJobLevelPerformanceIndicatorRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(JobLevelPerformanceIndicator));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetJobLevelPerformanceIndicatorRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(JobLevelPerformanceIndicator));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "JobLevelPerformanceIndicatorID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetJobLevelPerformanceIndicatorMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(JobLevelPerformanceIndicator));
                ctx.CommandText = helper.SelectMaxColumn("JobLevelPerformanceIndicatorID");
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
        #region JobLevelPerformanceIndicatorDt
        public static JobLevelPerformanceIndicatorDt GetJobLevelPerformanceIndicatorDt(Int32 JobLevelPerformanceIndicatorID, Int32 JobLevelID)
        {
            return new JobLevelPerformanceIndicatorDtDao().Get(JobLevelPerformanceIndicatorID, JobLevelID);
        }
        public static int InsertJobLevelPerformanceIndicatorDt(JobLevelPerformanceIndicatorDt record)
        {
            return new JobLevelPerformanceIndicatorDtDao().Insert(record);
        }
        public static int UpdateJobLevelPerformanceIndicatorDt(JobLevelPerformanceIndicatorDt record)
        {
            return new JobLevelPerformanceIndicatorDtDao().Update(record);
        }
        public static int DeleteJobLevelPerformanceIndicatorDt(Int32 JobLevelPerformanceIndicatorID, Int32 JobLevelID)
        {
            return new JobLevelPerformanceIndicatorDtDao().Delete(JobLevelPerformanceIndicatorID, JobLevelID);
        }
        public static List<JobLevelPerformanceIndicatorDt> GetJobLevelPerformanceIndicatorDtList(string filterExpression)
        {
            List<JobLevelPerformanceIndicatorDt> result = new List<JobLevelPerformanceIndicatorDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(JobLevelPerformanceIndicatorDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((JobLevelPerformanceIndicatorDt)helper.IDataReaderToObject(reader, new JobLevelPerformanceIndicatorDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region JobLevelPerformanceIndicatorDtIndicator
        public static JobLevelPerformanceIndicatorDtIndicator GetJobLevelPerformanceIndicatorDtIndicator(Int32 JobLevelPerformanceIndicatorID, Int32 PerformanceIndicatorDtID)
        {
            return new JobLevelPerformanceIndicatorDtIndicatorDao().Get(JobLevelPerformanceIndicatorID, PerformanceIndicatorDtID);
        }
        public static int InsertJobLevelPerformanceIndicatorDtIndicator(JobLevelPerformanceIndicatorDtIndicator record)
        {
            return new JobLevelPerformanceIndicatorDtIndicatorDao().Insert(record);
        }
        public static int UpdateJobLevelPerformanceIndicatorDtIndicator(JobLevelPerformanceIndicatorDtIndicator record)
        {
            return new JobLevelPerformanceIndicatorDtIndicatorDao().Update(record);
        }
        public static int DeleteJobLevelPerformanceIndicatorDtIndicator(Int32 JobLevelPerformanceIndicatorID, Int32 PerformanceIndicatorDtID)
        {
            return new JobLevelPerformanceIndicatorDtIndicatorDao().Delete(JobLevelPerformanceIndicatorID, PerformanceIndicatorDtID);
        }
        public static List<JobLevelPerformanceIndicatorDtIndicator> GetJobLevelPerformanceIndicatorDtIndicatorList(string filterExpression)
        {
            List<JobLevelPerformanceIndicatorDtIndicator> result = new List<JobLevelPerformanceIndicatorDtIndicator>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(JobLevelPerformanceIndicatorDtIndicator));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((JobLevelPerformanceIndicatorDtIndicator)helper.IDataReaderToObject(reader, new JobLevelPerformanceIndicatorDtIndicator()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region JobLevelWorkYears
        public static JobLevelWorkYears GetJobLevelWorkYears(Int32 JobLevelWorkYearsID)
        {
            return new JobLevelWorkYearsDao().Get(JobLevelWorkYearsID);
        }
        public static int InsertJobLevelWorkYears(JobLevelWorkYears record)
        {
            return new JobLevelWorkYearsDao().Insert(record);
        }
        public static int UpdateJobLevelWorkYears(JobLevelWorkYears record)
        {
            return new JobLevelWorkYearsDao().Update(record);
        }
        public static int DeleteJobLevelWorkYears(Int32 JobLevelWorkYearsID)
        {
            return new JobLevelWorkYearsDao().Delete(JobLevelWorkYearsID);
        }
        public static List<JobLevelWorkYears> GetJobLevelWorkYearsList(string filterExpression)
        {
            List<JobLevelWorkYears> result = new List<JobLevelWorkYears>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(JobLevelWorkYears));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((JobLevelWorkYears)helper.IDataReaderToObject(reader, new JobLevelWorkYears()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<JobLevelWorkYears> GetJobLevelWorkYearsList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<JobLevelWorkYears> result = new List<JobLevelWorkYears>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(JobLevelWorkYears));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((JobLevelWorkYears)helper.IDataReaderToObject(reader, new JobLevelWorkYears()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetJobLevelWorkYearsRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(JobLevelWorkYears));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetJobLevelWorkYearsRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(JobLevelWorkYears));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "JobLevelWorkYearsID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetJobLevelWorkYearsMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(JobLevelWorkYears));
                ctx.CommandText = helper.SelectMaxColumn("JobLevelWorkYearsID");
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
        #region JobLevelWorkYearsDt
        public static JobLevelWorkYearsDt GetJobLevelWorkYearsDt(Int32 JobLevelWorkYearsID, Int32 JobLevelID)
        {
            return new JobLevelWorkYearsDtDao().Get(JobLevelWorkYearsID, JobLevelID);
        }
        public static int InsertJobLevelWorkYearsDt(JobLevelWorkYearsDt record)
        {
            return new JobLevelWorkYearsDtDao().Insert(record);
        }
        public static int UpdateJobLevelWorkYearsDt(JobLevelWorkYearsDt record)
        {
            return new JobLevelWorkYearsDtDao().Update(record);
        }
        public static int DeleteJobLevelWorkYearsDt(Int32 JobLevelWorkYearsID, Int32 JobLevelID)
        {
            return new JobLevelWorkYearsDtDao().Delete(JobLevelWorkYearsID, JobLevelID);
        }
        public static List<JobLevelWorkYearsDt> GetJobLevelWorkYearsDtList(string filterExpression)
        {
            List<JobLevelWorkYearsDt> result = new List<JobLevelWorkYearsDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(JobLevelWorkYearsDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((JobLevelWorkYearsDt)helper.IDataReaderToObject(reader, new JobLevelWorkYearsDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region JournalTemplateDt
        public static JournalTemplateDt GetJournalTemplateDt(Int32 ID)
        {
            return new JournalTemplateDtDao().Get(ID);
        }
        public static int InsertJournalTemplateDt(JournalTemplateDt record)
        {
            return new JournalTemplateDtDao().Insert(record);
        }
        public static int UpdateJournalTemplateDt(JournalTemplateDt record)
        {
            return new JournalTemplateDtDao().Update(record);
        }
        public static int DeleteJournalTemplateDt(Int32 ID)
        {
            return new JournalTemplateDtDao().Delete(ID);
        }
        public static List<JournalTemplateDt> GetJournalTemplateDtList(string filterExpression)
        {
            List<JournalTemplateDt> result = new List<JournalTemplateDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(JournalTemplateDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((JournalTemplateDt)helper.IDataReaderToObject(reader, new JournalTemplateDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region JournalTemplateHd
        public static JournalTemplateHd GetJournalTemplateHd(Int32 TemplateID)
        {
            return new JournalTemplateHdDao().Get(TemplateID);
        }
        public static int InsertJournalTemplateHd(JournalTemplateHd record)
        {
            return new JournalTemplateHdDao().Insert(record);
        }
        public static int UpdateJournalTemplateHd(JournalTemplateHd record)
        {
            return new JournalTemplateHdDao().Update(record);
        }
        public static int DeleteJournalTemplateHd(Int32 TemplateID)
        {
            return new JournalTemplateHdDao().Delete(TemplateID);
        }
        public static List<JournalTemplateHd> GetJournalTemplateHdList(string filterExpression)
        {
            List<JournalTemplateHd> result = new List<JournalTemplateHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(JournalTemplateHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((JournalTemplateHd)helper.IDataReaderToObject(reader, new JournalTemplateHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<JournalTemplateHd> GetJournalTemplateHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<JournalTemplateHd> result = new List<JournalTemplateHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(JournalTemplateHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((JournalTemplateHd)helper.IDataReaderToObject(reader, new JournalTemplateHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetJournalTemplateHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(JournalTemplateHd));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetJournalTemplateHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(JournalTemplateHd));
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
        public static Int32 GetJournalTemplateHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(JournalTemplateHd));
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
        #region Location
        public static Location GetLocation(Int32 LocationID)
        {
            return new LocationDao().Get(LocationID);
        }
        public static int InsertLocation(Location record)
        {
            return new LocationDao().Insert(record);
        }
        public static int UpdateLocation(Location record)
        {
            return new LocationDao().Update(record);
        }
        public static int DeleteLocation(Int32 LocationID)
        {
            return new LocationDao().Delete(LocationID);
        }
        public static List<Location> GetLocationList(string filterExpression)
        {
            List<Location> result = new List<Location>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Location));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Location)helper.IDataReaderToObject(reader, new Location()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<Location> GetLocationList(string filterExpression, int numRows, int pageIndex, string orderByExpression)
        {
            List<Location> result = new List<Location>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Location));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Location)helper.IDataReaderToObject(reader, new Location()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetLocationRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Location));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetLocationMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(Location));
                ctx.CommandText = helper.SelectMaxColumn("LocationID");
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
        #region LocationItemGroup
        public static LocationItemGroup GetLocationItemGroup(Int32 LocationID, Int32 ItemGroupID)
        {
            return new LocationItemGroupDao().Get(LocationID, ItemGroupID);
        }
        public static int InsertLocationItemGroup(LocationItemGroup record)
        {
            return new LocationItemGroupDao().Insert(record);
        }
        public static int UpdateLocationItemGroup(LocationItemGroup record)
        {
            return new LocationItemGroupDao().Update(record);
        }
        public static int DeleteLocationItemGroup(Int32 LocationID, Int32 ItemGroupID)
        {
            return new LocationItemGroupDao().Delete(LocationID, ItemGroupID);
        }
        public static List<LocationItemGroup> GetLocationItemGroupList(string filterExpression)
        {
            List<LocationItemGroup> result = new List<LocationItemGroup>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(LocationItemGroup));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((LocationItemGroup)helper.IDataReaderToObject(reader, new LocationItemGroup()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<LocationItemGroup> GetLocationItemGroupList(string filterExpression, IDbContext ctx)
        {
            List<LocationItemGroup> result = new List<LocationItemGroup>();
            try
            {
                DbHelper helper = new DbHelper(typeof(LocationItemGroup));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((LocationItemGroup)helper.IDataReaderToObject(reader, new LocationItemGroup()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region Manufacturer
        public static Manufacturer GetManufacturer(Int32 ManufacturerID)
        {
            return new ManufacturerDao().Get(ManufacturerID);
        }
        public static int InsertManufacturer(Manufacturer record)
        {
            return new ManufacturerDao().Insert(record);
        }
        public static int UpdateManufacturer(Manufacturer record)
        {
            return new ManufacturerDao().Update(record);
        }
        public static int DeleteManufacturer(Int32 ManufacturerID)
        {
            return new ManufacturerDao().Delete(ManufacturerID);
        }
        public static List<Manufacturer> GetManufacturerList(string filterExpression)
        {
            List<Manufacturer> result = new List<Manufacturer>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Manufacturer));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Manufacturer)helper.IDataReaderToObject(reader, new Manufacturer()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<Manufacturer> GetManufacturerList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<Manufacturer> result = new List<Manufacturer>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Manufacturer));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Manufacturer)helper.IDataReaderToObject(reader, new Manufacturer()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetManufacturerRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Manufacturer));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetManufacturerRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Manufacturer));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ManufacturerID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetManufacturerMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(Manufacturer));
                ctx.CommandText = helper.SelectMaxColumn("ManufacturerID");
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
        #region MarginMarkupDt
        public static MarginMarkupDt GetMarginMarkupDt(Int32 MarkupID, Int16 SequenceNo)
        {
            return new MarginMarkupDtDao().Get(MarkupID, SequenceNo);
        }
        public static int InsertMarginMarkupDt(MarginMarkupDt record)
        {
            return new MarginMarkupDtDao().Insert(record);
        }
        public static int UpdateMarginMarkupDt(MarginMarkupDt record)
        {
            return new MarginMarkupDtDao().Update(record);
        }
        public static int DeleteMarginMarkupDt(Int32 MarkupID, Int16 SequenceNo)
        {
            return new MarginMarkupDtDao().Delete(MarkupID, SequenceNo);
        }
        public static List<MarginMarkupDt> GetMarginMarkupDtList(string filterExpression)
        {
            List<MarginMarkupDt> result = new List<MarginMarkupDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MarginMarkupDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MarginMarkupDt)helper.IDataReaderToObject(reader, new MarginMarkupDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int16 GetMarginMarkupDtMaxSequenceNo(string filterExpression)
        {
            Int16 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MarginMarkupDt));
                ctx.CommandText = helper.SelectMaxColumn("SequenceNo", filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                if (row != null)
                    result = Convert.ToInt16(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region MarginMarkupHd
        public static MarginMarkupHd GetMarginMarkupHd(Int32 MarkupID)
        {
            return new MarginMarkupHdDao().Get(MarkupID);
        }
        public static int InsertMarginMarkupHd(MarginMarkupHd record)
        {
            return new MarginMarkupHdDao().Insert(record);
        }
        public static int UpdateMarginMarkupHd(MarginMarkupHd record)
        {
            return new MarginMarkupHdDao().Update(record);
        }
        public static int DeleteMarginMarkupHd(Int32 MarkupID)
        {
            return new MarginMarkupHdDao().Delete(MarkupID);
        }
        public static List<MarginMarkupHd> GetMarginMarkupHdList(string filterExpression)
        {
            List<MarginMarkupHd> result = new List<MarginMarkupHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MarginMarkupHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MarginMarkupHd)helper.IDataReaderToObject(reader, new MarginMarkupHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<MarginMarkupHd> GetMarginMarkupHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<MarginMarkupHd> result = new List<MarginMarkupHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MarginMarkupHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MarginMarkupHd)helper.IDataReaderToObject(reader, new MarginMarkupHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetMarginMarkupHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MarginMarkupHd));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetMarginMarkupHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MarginMarkupHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "MarkupID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetMarginMarkupHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(MarginMarkupHd));
                ctx.CommandText = helper.SelectMaxColumn("MarkupID");
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
        #region MarkTypeDt
        public static MarkTypeDt GetMarkTypeDt(Int32 MarkTypeDtID)
        {
            return new MarkTypeDtDao().Get(MarkTypeDtID);
        }
        public static int InsertMarkTypeDt(MarkTypeDt record)
        {
            return new MarkTypeDtDao().Insert(record);
        }
        public static int UpdateMarkTypeDt(MarkTypeDt record)
        {
            return new MarkTypeDtDao().Update(record);
        }
        public static int DeleteMarkTypeDt(Int32 MarkTypeDtID)
        {
            return new MarkTypeDtDao().Delete(MarkTypeDtID);
        }
        public static List<MarkTypeDt> GetMarkTypeDtList(string filterExpression)
        {
            List<MarkTypeDt> result = new List<MarkTypeDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MarkTypeDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MarkTypeDt)helper.IDataReaderToObject(reader, new MarkTypeDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<MarkTypeDt> GetMarkTypeDtList(string filterExpression, IDbContext ctx)
        {
            List<MarkTypeDt> result = new List<MarkTypeDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(MarkTypeDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MarkTypeDt)helper.IDataReaderToObject(reader, new MarkTypeDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region MarkTypeFormula
        public static MarkTypeFormula GetMarkTypeFormula(Int32 MarkTypeFormulaID)
        {
            return new MarkTypeFormulaDao().Get(MarkTypeFormulaID);
        }
        public static int InsertMarkTypeFormula(MarkTypeFormula record)
        {
            return new MarkTypeFormulaDao().Insert(record);
        }
        public static int UpdateMarkTypeFormula(MarkTypeFormula record)
        {
            return new MarkTypeFormulaDao().Update(record);
        }
        public static int DeleteMarkTypeFormula(Int32 MarkTypeFormulaID)
        {
            return new MarkTypeFormulaDao().Delete(MarkTypeFormulaID);
        }
        public static List<MarkTypeFormula> GetMarkTypeFormulaList(string filterExpression)
        {
            List<MarkTypeFormula> result = new List<MarkTypeFormula>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MarkTypeFormula));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MarkTypeFormula)helper.IDataReaderToObject(reader, new MarkTypeFormula()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region MarkTypeHd
        public static MarkTypeHd GetMarkTypeHd(Int32 MarkTypeID)
        {
            return new MarkTypeHdDao().Get(MarkTypeID);
        }
        public static int InsertMarkTypeHd(MarkTypeHd record)
        {
            return new MarkTypeHdDao().Insert(record);
        }
        public static int UpdateMarkTypeHd(MarkTypeHd record)
        {
            return new MarkTypeHdDao().Update(record);
        }
        public static int DeleteMarkTypeHd(Int32 MarkTypeID)
        {
            return new MarkTypeHdDao().Delete(MarkTypeID);
        }
        public static List<MarkTypeHd> GetMarkTypeHdList(string filterExpression)
        {
            List<MarkTypeHd> result = new List<MarkTypeHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MarkTypeHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MarkTypeHd)helper.IDataReaderToObject(reader, new MarkTypeHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetMarkTypeHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(MarkTypeHd));
                ctx.CommandText = helper.SelectMaxColumn("MarkTypeID");
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
        #region OrganizationDepartment
        public static OrganizationDepartment GetOrganizationDepartment(Int32 OrganizationDepartmentID)
        {
            return new OrganizationDepartmentDao().Get(OrganizationDepartmentID);
        }
        public static int InsertOrganizationDepartment(OrganizationDepartment record)
        {
            return new OrganizationDepartmentDao().Insert(record);
        }
        public static int UpdateOrganizationDepartment(OrganizationDepartment record)
        {
            return new OrganizationDepartmentDao().Update(record);
        }
        public static int DeleteOrganizationDepartment(Int32 OrganizationDepartmentID)
        {
            return new OrganizationDepartmentDao().Delete(OrganizationDepartmentID);
        }
        public static List<OrganizationDepartment> GetOrganizationDepartmentList(string filterExpression)
        {
            List<OrganizationDepartment> result = new List<OrganizationDepartment>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(OrganizationDepartment));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((OrganizationDepartment)helper.IDataReaderToObject(reader, new OrganizationDepartment()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region OrganizationPosition
        public static OrganizationPosition GetOrganizationPosition(Int32 OrganizationPositionID)
        {
            return new OrganizationPositionDao().Get(OrganizationPositionID);
        }
        public static int InsertOrganizationPosition(OrganizationPosition record)
        {
            return new OrganizationPositionDao().Insert(record);
        }
        public static int UpdateOrganizationPosition(OrganizationPosition record)
        {
            return new OrganizationPositionDao().Update(record);
        }
        public static int DeleteOrganizationPosition(Int32 OrganizationPositionID)
        {
            return new OrganizationPositionDao().Delete(OrganizationPositionID);
        }
        public static List<OrganizationPosition> GetOrganizationPositionList(string filterExpression)
        {
            List<OrganizationPosition> result = new List<OrganizationPosition>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(OrganizationPosition));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((OrganizationPosition)helper.IDataReaderToObject(reader, new OrganizationPosition()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<OrganizationPosition> GetOrganizationPositionList(string filterExpression, IDbContext ctx)
        {
            List<OrganizationPosition> result = new List<OrganizationPosition>();
            try
            {
                DbHelper helper = new DbHelper(typeof(OrganizationPosition));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((OrganizationPosition)helper.IDataReaderToObject(reader, new OrganizationPosition()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region OrganizationDt
        public static OrganizationDt GetOrganizationDt(Int32 OrganizationDtID)
        {
            return new OrganizationDtDao().Get(OrganizationDtID);
        }
        public static int InsertOrganizationDt(OrganizationDt record)
        {
            return new OrganizationDtDao().Insert(record);
        }
        public static int UpdateOrganizationDt(OrganizationDt record)
        {
            return new OrganizationDtDao().Update(record);
        }
        public static int DeleteOrganizationDt(Int32 OrganizationDtID)
        {
            return new OrganizationDtDao().Delete(OrganizationDtID);
        }
        public static List<OrganizationDt> GetOrganizationDtList(string filterExpression)
        {
            List<OrganizationDt> result = new List<OrganizationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(OrganizationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((OrganizationDt)helper.IDataReaderToObject(reader, new OrganizationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetOrganizationDtMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(OrganizationDt));
                ctx.CommandText = helper.SelectMaxColumn("OrganizationDtID");
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
        #region OrganizationDtStudent
        public static OrganizationDtStudent GetOrganizationDtStudent(Int32 OrganizationDtID, Int32 StudentID)
        {
            return new OrganizationDtStudentDao().Get(OrganizationDtID, StudentID);
        }
        public static int InsertOrganizationDtStudent(OrganizationDtStudent record)
        {
            return new OrganizationDtStudentDao().Insert(record);
        }
        public static int UpdateOrganizationDtStudent(OrganizationDtStudent record)
        {
            return new OrganizationDtStudentDao().Update(record);
        }
        public static int DeleteOrganizationDtStudent(Int32 OrganizationDtID, Int32 StudentID)
        {
            return new OrganizationDtStudentDao().Delete(OrganizationDtID, StudentID);
        }
        public static List<OrganizationDtStudent> GetOrganizationDtStudentList(string filterExpression)
        {
            List<OrganizationDtStudent> result = new List<OrganizationDtStudent>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(OrganizationDtStudent));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((OrganizationDtStudent)helper.IDataReaderToObject(reader, new OrganizationDtStudent()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<OrganizationDtStudent> GetOrganizationDtStudentList(string filterExpression, IDbContext ctx)
        {
            List<OrganizationDtStudent> result = new List<OrganizationDtStudent>();
            try
            {
                DbHelper helper = new DbHelper(typeof(OrganizationDtStudent));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((OrganizationDtStudent)helper.IDataReaderToObject(reader, new OrganizationDtStudent()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region OrganizationHd
        public static OrganizationHd GetOrganizationHd(Int32 OrganizationID)
        {
            return new OrganizationHdDao().Get(OrganizationID);
        }
        public static int InsertOrganizationHd(OrganizationHd record)
        {
            return new OrganizationHdDao().Insert(record);
        }
        public static int UpdateOrganizationHd(OrganizationHd record)
        {
            return new OrganizationHdDao().Update(record);
        }
        public static int DeleteOrganizationHd(Int32 OrganizationID)
        {
            return new OrganizationHdDao().Delete(OrganizationID);
        }
        public static List<OrganizationHd> GetOrganizationHdList(string filterExpression)
        {
            List<OrganizationHd> result = new List<OrganizationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(OrganizationHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((OrganizationHd)helper.IDataReaderToObject(reader, new OrganizationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region OvertimeProposalDate
        public static OvertimeProposalDate GetOvertimeProposalDate(Int32 TransactionDtID)
        {
            return new OvertimeProposalDateDao().Get(TransactionDtID);
        }
        public static int InsertOvertimeProposalDate(OvertimeProposalDate record)
        {
            return new OvertimeProposalDateDao().Insert(record);
        }
        public static int UpdateOvertimeProposalDate(OvertimeProposalDate record)
        {
            return new OvertimeProposalDateDao().Update(record);
        }
        public static int DeleteOvertimeProposalDate(Int32 TransactionDtID)
        {
            return new OvertimeProposalDateDao().Delete(TransactionDtID);
        }
        public static List<OvertimeProposalDate> GetOvertimeProposalDateList(string filterExpression)
        {
            List<OvertimeProposalDate> result = new List<OvertimeProposalDate>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(OvertimeProposalDate));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((OvertimeProposalDate)helper.IDataReaderToObject(reader, new OvertimeProposalDate()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<OvertimeProposalDate> GetOvertimeProposalDateList(string filterExpression, IDbContext ctx)
        {
            List<OvertimeProposalDate> result = new List<OvertimeProposalDate>();
            try
            {
                DbHelper helper = new DbHelper(typeof(OvertimeProposalDate));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((OvertimeProposalDate)helper.IDataReaderToObject(reader, new OvertimeProposalDate()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetOvertimeProposalDateMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(OvertimeProposalDate));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetOvertimeProposalDateRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(OvertimeProposalDate));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<OvertimeProposalDate> GetOvertimeProposalDateList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<OvertimeProposalDate> result = new List<OvertimeProposalDate>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(OvertimeProposalDate));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((OvertimeProposalDate)helper.IDataReaderToObject(reader, new OvertimeProposalDate()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region OvertimeProposalEmployee
        public static OvertimeProposalEmployee GetOvertimeProposalEmployee(Int32 TransactionID, Int32 EmployeeID)
        {
            return new OvertimeProposalEmployeeDao().Get(TransactionID, EmployeeID);
        }
        public static int InsertOvertimeProposalEmployee(OvertimeProposalEmployee record)
        {
            return new OvertimeProposalEmployeeDao().Insert(record);
        }
        public static int UpdateOvertimeProposalEmployee(OvertimeProposalEmployee record)
        {
            return new OvertimeProposalEmployeeDao().Update(record);
        }
        public static int DeleteOvertimeProposalEmployee(Int32 TransactionID, Int32 EmployeeID)
        {
            return new OvertimeProposalEmployeeDao().Delete(TransactionID, EmployeeID);
        }
        public static List<OvertimeProposalEmployee> GetOvertimeProposalEmployeeList(string filterExpression)
        {
            List<OvertimeProposalEmployee> result = new List<OvertimeProposalEmployee>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(OvertimeProposalEmployee));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((OvertimeProposalEmployee)helper.IDataReaderToObject(reader, new OvertimeProposalEmployee()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<OvertimeProposalEmployee> GetOvertimeProposalEmployeeList(string filterExpression, IDbContext ctx)
        {
            List<OvertimeProposalEmployee> result = new List<OvertimeProposalEmployee>();
            try
            {
                DbHelper helper = new DbHelper(typeof(OvertimeProposalEmployee));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((OvertimeProposalEmployee)helper.IDataReaderToObject(reader, new OvertimeProposalEmployee()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetOvertimeProposalEmployeeMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(OvertimeProposalEmployee));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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
        #region OvertimeProposalHd
        public static OvertimeProposalHd GetOvertimeProposalHd(Int32 TransactionID)
        {
            return new OvertimeProposalHdDao().Get(TransactionID);
        }
        public static int InsertOvertimeProposalHd(OvertimeProposalHd record)
        {
            return new OvertimeProposalHdDao().Insert(record);
        }
        public static int UpdateOvertimeProposalHd(OvertimeProposalHd record)
        {
            return new OvertimeProposalHdDao().Update(record);
        }
        public static int DeleteOvertimeProposalHd(Int32 TransactionID)
        {
            return new OvertimeProposalHdDao().Delete(TransactionID);
        }
        public static List<OvertimeProposalHd> GetOvertimeProposalHdList(string filterExpression)
        {
            List<OvertimeProposalHd> result = new List<OvertimeProposalHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(OvertimeProposalHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((OvertimeProposalHd)helper.IDataReaderToObject(reader, new OvertimeProposalHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<OvertimeProposalHd> GetOvertimeProposalHdList(string filterExpression, IDbContext ctx)
        {
            List<OvertimeProposalHd> result = new List<OvertimeProposalHd>();
            try
            {
                DbHelper helper = new DbHelper(typeof(OvertimeProposalHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((OvertimeProposalHd)helper.IDataReaderToObject(reader, new OvertimeProposalHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetOvertimeProposalHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(OvertimeProposalHd));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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
        #region PerformanceIndicatorDt
        public static PerformanceIndicatorDt GetPerformanceIndicatorDt(Int32 PerformanceIndicatorDtID)
        {
            return new PerformanceIndicatorDtDao().Get(PerformanceIndicatorDtID);
        }
        public static int InsertPerformanceIndicatorDt(PerformanceIndicatorDt record)
        {
            return new PerformanceIndicatorDtDao().Insert(record);
        }
        public static int UpdatePerformanceIndicatorDt(PerformanceIndicatorDt record)
        {
            return new PerformanceIndicatorDtDao().Update(record);
        }
        public static int DeletePerformanceIndicatorDt(Int32 PerformanceIndicatorDtID)
        {
            return new PerformanceIndicatorDtDao().Delete(PerformanceIndicatorDtID);
        }
        public static List<PerformanceIndicatorDt> GetPerformanceIndicatorDtList(string filterExpression)
        {
            List<PerformanceIndicatorDt> result = new List<PerformanceIndicatorDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PerformanceIndicatorDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PerformanceIndicatorDt)helper.IDataReaderToObject(reader, new PerformanceIndicatorDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region PerformanceIndicatorHd
        public static PerformanceIndicatorHd GetPerformanceIndicatorHd(Int32 PerformanceIndicatorID)
        {
            return new PerformanceIndicatorHdDao().Get(PerformanceIndicatorID);
        }
        public static int InsertPerformanceIndicatorHd(PerformanceIndicatorHd record)
        {
            return new PerformanceIndicatorHdDao().Insert(record);
        }
        public static int UpdatePerformanceIndicatorHd(PerformanceIndicatorHd record)
        {
            return new PerformanceIndicatorHdDao().Update(record);
        }
        public static int DeletePerformanceIndicatorHd(Int32 PerformanceIndicatorID)
        {
            return new PerformanceIndicatorHdDao().Delete(PerformanceIndicatorID);
        }
        public static List<PerformanceIndicatorHd> GetPerformanceIndicatorHdList(string filterExpression)
        {
            List<PerformanceIndicatorHd> result = new List<PerformanceIndicatorHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PerformanceIndicatorHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PerformanceIndicatorHd)helper.IDataReaderToObject(reader, new PerformanceIndicatorHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<PerformanceIndicatorHd> GetPerformanceIndicatorHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<PerformanceIndicatorHd> result = new List<PerformanceIndicatorHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PerformanceIndicatorHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PerformanceIndicatorHd)helper.IDataReaderToObject(reader, new PerformanceIndicatorHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetPerformanceIndicatorHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PerformanceIndicatorHd));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetPerformanceIndicatorHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PerformanceIndicatorHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "PerformanceIndicatorID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetPerformanceIndicatorHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(PerformanceIndicatorHd));
                ctx.CommandText = helper.SelectMaxColumn("PerformanceIndicatorID");
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
        #region PeriodAdmission
        public static PeriodAdmission GetPeriodAdmission(Int32 PeriodAdmissionID)
        {
            return new PeriodAdmissionDao().Get(PeriodAdmissionID);
        }
        public static int InsertPeriodAdmission(PeriodAdmission record)
        {
            return new PeriodAdmissionDao().Insert(record);
        }
        public static int UpdatePeriodAdmission(PeriodAdmission record)
        {
            return new PeriodAdmissionDao().Update(record);
        }
        public static int DeletePeriodAdmission(Int32 PeriodAdmissionID)
        {
            return new PeriodAdmissionDao().Delete(PeriodAdmissionID);
        }
        public static List<PeriodAdmission> GetPeriodAdmissionList(string filterExpression)
        {
            List<PeriodAdmission> result = new List<PeriodAdmission>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodAdmission));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PeriodAdmission)helper.IDataReaderToObject(reader, new PeriodAdmission()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetPeriodAdmissionMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodAdmission));
                ctx.CommandText = helper.SelectMaxColumn("PeriodAdmissionID");
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
        #region PeriodClassType
        public static PeriodClassType GetPeriodClassType(Int32 PeriodClassTypeID)
        {
            return new PeriodClassTypeDao().Get(PeriodClassTypeID);
        }
        public static int InsertPeriodClassType(PeriodClassType record)
        {
            return new PeriodClassTypeDao().Insert(record);
        }
        public static int UpdatePeriodClassType(PeriodClassType record)
        {
            return new PeriodClassTypeDao().Update(record);
        }
        public static int DeletePeriodClassType(Int32 PeriodClassTypeID)
        {
            return new PeriodClassTypeDao().Delete(PeriodClassTypeID);
        }
        public static List<PeriodClassType> GetPeriodClassTypeList(string filterExpression)
        {
            List<PeriodClassType> result = new List<PeriodClassType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodClassType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PeriodClassType)helper.IDataReaderToObject(reader, new PeriodClassType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetPeriodClassTypeMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodClassType));
                ctx.CommandText = helper.SelectMaxColumn("PeriodClassTypeID");
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
        #region PeriodClassTypeFinalMarkFormula
        public static PeriodClassTypeFinalMarkFormula GetPeriodClassTypeFinalMarkFormula(Int32 PeriodClassTypeID, Int32 CurriculumMarkTypeID)
        {
            return new PeriodClassTypeFinalMarkFormulaDao().Get(PeriodClassTypeID, CurriculumMarkTypeID);
        }
        public static int InsertPeriodClassTypeFinalMarkFormula(PeriodClassTypeFinalMarkFormula record)
        {
            return new PeriodClassTypeFinalMarkFormulaDao().Insert(record);
        }
        public static int UpdatePeriodClassTypeFinalMarkFormula(PeriodClassTypeFinalMarkFormula record)
        {
            return new PeriodClassTypeFinalMarkFormulaDao().Update(record);
        }
        public static int DeletePeriodClassTypeFinalMarkFormula(Int32 PeriodClassTypeID, Int32 CurriculumMarkTypeID)
        {
            return new PeriodClassTypeFinalMarkFormulaDao().Delete(PeriodClassTypeID, CurriculumMarkTypeID);
        }
        public static List<PeriodClassTypeFinalMarkFormula> GetPeriodClassTypeFinalMarkFormulaList(string filterExpression)
        {
            List<PeriodClassTypeFinalMarkFormula> result = new List<PeriodClassTypeFinalMarkFormula>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodClassTypeFinalMarkFormula));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PeriodClassTypeFinalMarkFormula)helper.IDataReaderToObject(reader, new PeriodClassTypeFinalMarkFormula()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<PeriodClassTypeFinalMarkFormula> GetPeriodClassTypeFinalMarkFormulaList(string filterExpression, IDbContext ctx)
        {
            List<PeriodClassTypeFinalMarkFormula> result = new List<PeriodClassTypeFinalMarkFormula>();
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodClassTypeFinalMarkFormula));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PeriodClassTypeFinalMarkFormula)helper.IDataReaderToObject(reader, new PeriodClassTypeFinalMarkFormula()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region PeriodClassTypeSubject
        public static PeriodClassTypeSubject GetPeriodClassTypeSubject(Int32 PeriodClassTypeSubjectID)
        {
            return new PeriodClassTypeSubjectDao().Get(PeriodClassTypeSubjectID);
        }
        public static int InsertPeriodClassTypeSubject(PeriodClassTypeSubject record)
        {
            return new PeriodClassTypeSubjectDao().Insert(record);
        }
        public static int UpdatePeriodClassTypeSubject(PeriodClassTypeSubject record)
        {
            return new PeriodClassTypeSubjectDao().Update(record);
        }
        public static int DeletePeriodClassTypeSubject(Int32 PeriodClassTypeSubjectID)
        {
            return new PeriodClassTypeSubjectDao().Delete(PeriodClassTypeSubjectID);
        }
        public static List<PeriodClassTypeSubject> GetPeriodClassTypeSubjectList(string filterExpression)
        {
            List<PeriodClassTypeSubject> result = new List<PeriodClassTypeSubject>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodClassTypeSubject));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PeriodClassTypeSubject)helper.IDataReaderToObject(reader, new PeriodClassTypeSubject()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetPeriodClassTypeSubjectMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodClassTypeSubject));
                ctx.CommandText = helper.SelectMaxColumn("PeriodClassTypeSubjectID");
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
        #region PeriodClassTypeSubjectFinalMarkFormula
        public static PeriodClassTypeSubjectFinalMarkFormula GetPeriodClassTypeSubjectFinalMarkFormula(Int32 PeriodClassTypeSubjectID, Int32 CurriculumMarkTypeID)
        {
            return new PeriodClassTypeSubjectFinalMarkFormulaDao().Get(PeriodClassTypeSubjectID, CurriculumMarkTypeID);
        }
        public static int InsertPeriodClassTypeSubjectFinalMarkFormula(PeriodClassTypeSubjectFinalMarkFormula record)
        {
            return new PeriodClassTypeSubjectFinalMarkFormulaDao().Insert(record);
        }
        public static int UpdatePeriodClassTypeSubjectFinalMarkFormula(PeriodClassTypeSubjectFinalMarkFormula record)
        {
            return new PeriodClassTypeSubjectFinalMarkFormulaDao().Update(record);
        }
        public static int DeletePeriodClassTypeSubjectFinalMarkFormula(Int32 PeriodClassTypeSubjectID, Int32 CurriculumMarkTypeID)
        {
            return new PeriodClassTypeSubjectFinalMarkFormulaDao().Delete(PeriodClassTypeSubjectID, CurriculumMarkTypeID);
        }
        public static List<PeriodClassTypeSubjectFinalMarkFormula> GetPeriodClassTypeSubjectFinalMarkFormulaList(string filterExpression)
        {
            List<PeriodClassTypeSubjectFinalMarkFormula> result = new List<PeriodClassTypeSubjectFinalMarkFormula>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodClassTypeSubjectFinalMarkFormula));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PeriodClassTypeSubjectFinalMarkFormula)helper.IDataReaderToObject(reader, new PeriodClassTypeSubjectFinalMarkFormula()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<PeriodClassTypeSubjectFinalMarkFormula> GetPeriodClassTypeSubjectFinalMarkFormulaList(string filterExpression, IDbContext ctx)
        {
            List<PeriodClassTypeSubjectFinalMarkFormula> result = new List<PeriodClassTypeSubjectFinalMarkFormula>();
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodClassTypeSubjectFinalMarkFormula));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PeriodClassTypeSubjectFinalMarkFormula)helper.IDataReaderToObject(reader, new PeriodClassTypeSubjectFinalMarkFormula()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region PeriodClassTypeSubjectIndicator
        public static PeriodClassTypeSubjectIndicator GetPeriodClassTypeSubjectIndicator(Int32 PeriodClassTypeSubjectID, Int32 SubjectIndicatorID)
        {
            return new PeriodClassTypeSubjectIndicatorDao().Get(PeriodClassTypeSubjectID, SubjectIndicatorID);
        }
        public static int InsertPeriodClassTypeSubjectIndicator(PeriodClassTypeSubjectIndicator record)
        {
            return new PeriodClassTypeSubjectIndicatorDao().Insert(record);
        }
        public static int UpdatePeriodClassTypeSubjectIndicator(PeriodClassTypeSubjectIndicator record)
        {
            return new PeriodClassTypeSubjectIndicatorDao().Update(record);
        }
        public static int DeletePeriodClassTypeSubjectIndicator(Int32 PeriodClassTypeSubjectID, Int32 SubjectIndicatorID)
        {
            return new PeriodClassTypeSubjectIndicatorDao().Delete(PeriodClassTypeSubjectID, SubjectIndicatorID);
        }
        public static List<PeriodClassTypeSubjectIndicator> GetPeriodClassTypeSubjectIndicatorList(string filterExpression)
        {
            List<PeriodClassTypeSubjectIndicator> result = new List<PeriodClassTypeSubjectIndicator>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodClassTypeSubjectIndicator));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PeriodClassTypeSubjectIndicator)helper.IDataReaderToObject(reader, new PeriodClassTypeSubjectIndicator()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<PeriodClassTypeSubjectIndicator> GetPeriodClassTypeSubjectIndicatorList(string filterExpression, IDbContext ctx)
        {
            List<PeriodClassTypeSubjectIndicator> result = new List<PeriodClassTypeSubjectIndicator>();
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodClassTypeSubjectIndicator));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PeriodClassTypeSubjectIndicator)helper.IDataReaderToObject(reader, new PeriodClassTypeSubjectIndicator()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }

        public static Int32 GetPeriodClassTypeSubjectIndicatorRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodClassTypeSubjectIndicator));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region PeriodFinalMarkFormula
        public static PeriodFinalMarkFormula GetPeriodFinalMarkFormula(Int32 SchoolPeriodID, Int32 CurriculumMarkTypeID)
        {
            return new PeriodFinalMarkFormulaDao().Get(SchoolPeriodID, CurriculumMarkTypeID);
        }
        public static int InsertPeriodFinalMarkFormula(PeriodFinalMarkFormula record)
        {
            return new PeriodFinalMarkFormulaDao().Insert(record);
        }
        public static int UpdatePeriodFinalMarkFormula(PeriodFinalMarkFormula record)
        {
            return new PeriodFinalMarkFormulaDao().Update(record);
        }
        public static int DeletePeriodFinalMarkFormula(Int32 SchoolPeriodID, Int32 CurriculumMarkTypeID)
        {
            return new PeriodFinalMarkFormulaDao().Delete(SchoolPeriodID, CurriculumMarkTypeID);
        }
        public static List<PeriodFinalMarkFormula> GetPeriodFinalMarkFormulaList(string filterExpression)
        {
            List<PeriodFinalMarkFormula> result = new List<PeriodFinalMarkFormula>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodFinalMarkFormula));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PeriodFinalMarkFormula)helper.IDataReaderToObject(reader, new PeriodFinalMarkFormula()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<PeriodFinalMarkFormula> GetPeriodFinalMarkFormulaList(string filterExpression, IDbContext ctx)
        {
            List<PeriodFinalMarkFormula> result = new List<PeriodFinalMarkFormula>();
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodFinalMarkFormula));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PeriodFinalMarkFormula)helper.IDataReaderToObject(reader, new PeriodFinalMarkFormula()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region PeriodGrade
        public static PeriodGrade GetPeriodGrade(Int32 SchoolPeriodID, String GCGrade)
        {
            return new PeriodGradeDao().Get(SchoolPeriodID, GCGrade);
        }
        public static int InsertPeriodGrade(PeriodGrade record)
        {
            return new PeriodGradeDao().Insert(record);
        }
        public static int UpdatePeriodGrade(PeriodGrade record)
        {
            return new PeriodGradeDao().Update(record);
        }
        public static int DeletePeriodGrade(Int32 SchoolPeriodID, String GCGrade)
        {
            return new PeriodGradeDao().Delete(SchoolPeriodID, GCGrade);
        }
        public static List<PeriodGrade> GetPeriodGradeList(string filterExpression)
        {
            List<PeriodGrade> result = new List<PeriodGrade>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodGrade));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PeriodGrade)helper.IDataReaderToObject(reader, new PeriodGrade()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region PeriodSchedule
        public static PeriodSchedule GetPeriodSchedule(Int32 PeriodScheduleID)
        {
            return new PeriodScheduleDao().Get(PeriodScheduleID);
        }
        public static int InsertPeriodSchedule(PeriodSchedule record)
        {
            return new PeriodScheduleDao().Insert(record);
        }
        public static int UpdatePeriodSchedule(PeriodSchedule record)
        {
            return new PeriodScheduleDao().Update(record);
        }
        public static int DeletePeriodSchedule(Int32 PeriodScheduleID)
        {
            return new PeriodScheduleDao().Delete(PeriodScheduleID);
        }
        public static List<PeriodSchedule> GetPeriodScheduleList(string filterExpression)
        {
            List<PeriodSchedule> result = new List<PeriodSchedule>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodSchedule));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PeriodSchedule)helper.IDataReaderToObject(reader, new PeriodSchedule()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetPeriodScheduleMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodSchedule));
                ctx.CommandText = helper.SelectMaxColumn("PeriodScheduleID");
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
        #region PeriodScheduleClassType
        public static PeriodScheduleClassType GetPeriodScheduleClassType(Int32 PeriodScheduleID, Int32 PeriodClassTypeID)
        {
            return new PeriodScheduleClassTypeDao().Get(PeriodScheduleID, PeriodClassTypeID);
        }
        public static int InsertPeriodScheduleClassType(PeriodScheduleClassType record)
        {
            return new PeriodScheduleClassTypeDao().Insert(record);
        }
        public static int UpdatePeriodScheduleClassType(PeriodScheduleClassType record)
        {
            return new PeriodScheduleClassTypeDao().Update(record);
        }
        public static int DeletePeriodScheduleClassType(Int32 PeriodScheduleID, Int32 PeriodClassTypeID)
        {
            return new PeriodScheduleClassTypeDao().Delete(PeriodScheduleID, PeriodClassTypeID);
        }
        public static List<PeriodScheduleClassType> GetPeriodScheduleClassTypeList(string filterExpression)
        {
            List<PeriodScheduleClassType> result = new List<PeriodScheduleClassType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodScheduleClassType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PeriodScheduleClassType)helper.IDataReaderToObject(reader, new PeriodScheduleClassType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<PeriodScheduleClassType> GetPeriodScheduleClassTypeList(string filterExpression, IDbContext ctx)
        {
            List<PeriodScheduleClassType> result = new List<PeriodScheduleClassType>();
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodScheduleClassType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PeriodScheduleClassType)helper.IDataReaderToObject(reader, new PeriodScheduleClassType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region PeriodSection
        public static PeriodSection GetPeriodSection(Int32 PeriodSectionID)
        {
            return new PeriodSectionDao().Get(PeriodSectionID);
        }
        public static int InsertPeriodSection(PeriodSection record)
        {
            return new PeriodSectionDao().Insert(record);
        }
        public static int UpdatePeriodSection(PeriodSection record)
        {
            return new PeriodSectionDao().Update(record);
        }
        public static int DeletePeriodSection(Int32 PeriodSectionID)
        {
            return new PeriodSectionDao().Delete(PeriodSectionID);
        }
        public static List<PeriodSection> GetPeriodSectionList(string filterExpression)
        {
            List<PeriodSection> result = new List<PeriodSection>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodSection));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PeriodSection)helper.IDataReaderToObject(reader, new PeriodSection()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<PeriodSection> GetPeriodSectionList(string filterExpression,IDbContext ctx)
        {
            List<PeriodSection> result = new List<PeriodSection>();
            try
            {
                DbHelper helper = new DbHelper(typeof(PeriodSection));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PeriodSection)helper.IDataReaderToObject(reader, new PeriodSection()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region PersonalityType
        public static PersonalityType GetPersonalityType(Int32 PersonalityTypeID)
        {
            return new PersonalityTypeDao().Get(PersonalityTypeID);
        }
        public static int InsertPersonalityType(PersonalityType record)
        {
            return new PersonalityTypeDao().Insert(record);
        }
        public static int UpdatePersonalityType(PersonalityType record)
        {
            return new PersonalityTypeDao().Update(record);
        }
        public static int DeletePersonalityType(Int32 PersonalityTypeID)
        {
            return new PersonalityTypeDao().Delete(PersonalityTypeID);
        }
        public static List<PersonalityType> GetPersonalityTypeList(string filterExpression)
        {
            List<PersonalityType> result = new List<PersonalityType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PersonalityType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PersonalityType)helper.IDataReaderToObject(reader, new PersonalityType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ProductBrand
        public static ProductBrand GetProductBrand(Int32 ProductBrandID)
        {
            return new ProductBrandDao().Get(ProductBrandID);
        }
        public static int InsertProductBrand(ProductBrand record)
        {
            return new ProductBrandDao().Insert(record);
        }
        public static int UpdateProductBrand(ProductBrand record)
        {
            return new ProductBrandDao().Update(record);
        }
        public static int DeleteProductBrand(Int32 ProductBrandID)
        {
            return new ProductBrandDao().Delete(ProductBrandID);
        }
        public static List<ProductBrand> GetProductBrandList(string filterExpression)
        {
            List<ProductBrand> result = new List<ProductBrand>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProductBrand));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProductBrand)helper.IDataReaderToObject(reader, new ProductBrand()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ProductBrand> GetProductBrandList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<ProductBrand> result = new List<ProductBrand>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProductBrand));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProductBrand)helper.IDataReaderToObject(reader, new ProductBrand()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        public static Int32 GetProductBrandRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProductBrand));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetProductBrandRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProductBrand));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ProductBrandID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetProductBrandMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ProductBrand));
                ctx.CommandText = helper.SelectMaxColumn("ProductBrandID");
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
        #region ProductLine
        public static ProductLine GetProductLine(Int32 ProductLineID)
        {
            return new ProductLineDao().Get(ProductLineID);
        }
        public static int InsertProductLine(ProductLine record)
        {
            return new ProductLineDao().Insert(record);
        }
        public static int UpdateProductLine(ProductLine record)
        {
            return new ProductLineDao().Update(record);
        }
        public static int DeleteProductLine(Int32 ProductLineID)
        {
            return new ProductLineDao().Delete(ProductLineID);
        }
        public static List<ProductLine> GetProductLineList(string filterExpression)
        {
            List<ProductLine> result = new List<ProductLine>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProductLine));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProductLine)helper.IDataReaderToObject(reader, new ProductLine()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ProductLine> GetProductLineList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<ProductLine> result = new List<ProductLine>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProductLine));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProductLine)helper.IDataReaderToObject(reader, new ProductLine()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetProductLineRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProductLine));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetProductLineRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProductLine));
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
        public static Int32 GetProductLineMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ProductLine));
                ctx.CommandText = helper.SelectMaxColumn("ProductLineID");
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
        #region ProductLineDt
        public static ProductLineDt GetProductLineDt(Int32 ProductLineID, String SiteID)
        {
            return new ProductLineDtDao().Get(ProductLineID, SiteID);
        }
        public static int InsertProductLineDt(ProductLineDt record)
        {
            return new ProductLineDtDao().Insert(record);
        }
        public static int UpdateProductLineDt(ProductLineDt record)
        {
            return new ProductLineDtDao().Update(record);
        }
        public static int DeleteProductLineDt(Int32 ProductLineID, String SiteID)
        {
            return new ProductLineDtDao().Delete(ProductLineID, SiteID);
        }
        public static List<ProductLineDt> GetProductLineDtList(string filterExpression)
        {
            List<ProductLineDt> result = new List<ProductLineDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProductLineDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProductLineDt)helper.IDataReaderToObject(reader, new ProductLineDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ProspectiveStudent
        public static ProspectiveStudent GetProspectiveStudent(Int32 ProspectiveStudentID)
        {
            return new ProspectiveStudentDao().Get(ProspectiveStudentID);
        }
        public static int InsertProspectiveStudent(ProspectiveStudent record)
        {
            return new ProspectiveStudentDao().Insert(record);
        }
        public static int UpdateProspectiveStudent(ProspectiveStudent record)
        {
            return new ProspectiveStudentDao().Update(record);
        }
        public static int DeleteProspectiveStudent(Int32 ProspectiveStudentID)
        {
            return new ProspectiveStudentDao().Delete(ProspectiveStudentID);
        }
        public static List<ProspectiveStudent> GetProspectiveStudentList(string filterExpression)
        {
            List<ProspectiveStudent> result = new List<ProspectiveStudent>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProspectiveStudent));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProspectiveStudent)helper.IDataReaderToObject(reader, new ProspectiveStudent()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ProspectiveStudent> GetProspectiveStudentList(string filterExpression, IDbContext ctx)
        {
            List<ProspectiveStudent> result = new List<ProspectiveStudent>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProspectiveStudent));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProspectiveStudent)helper.IDataReaderToObject(reader, new ProspectiveStudent()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetProspectiveStudentRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProspectiveStudent));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetProspectiveStudentMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ProspectiveStudent));
                ctx.CommandText = helper.SelectMaxColumn("ProspectiveStudentID");
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
        #region ProspectiveStudentAchievement
        public static ProspectiveStudentAchievement GetProspectiveStudentAchievement(Int32 ProspectiveStudentAchievementID)
        {
            return new ProspectiveStudentAchievementDao().Get(ProspectiveStudentAchievementID);
        }
        public static int InsertProspectiveStudentAchievement(ProspectiveStudentAchievement record)
        {
            return new ProspectiveStudentAchievementDao().Insert(record);
        }
        public static int UpdateProspectiveStudentAchievement(ProspectiveStudentAchievement record)
        {
            return new ProspectiveStudentAchievementDao().Update(record);
        }
        public static int DeleteProspectiveStudentAchievement(Int32 ProspectiveStudentAchievementID)
        {
            return new ProspectiveStudentAchievementDao().Delete(ProspectiveStudentAchievementID);
        }
        public static List<ProspectiveStudentAchievement> GetProspectiveStudentAchievementList(string filterExpression)
        {
            List<ProspectiveStudentAchievement> result = new List<ProspectiveStudentAchievement>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProspectiveStudentAchievement));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProspectiveStudentAchievement)helper.IDataReaderToObject(reader, new ProspectiveStudentAchievement()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ProspectiveStudentFolder
        public static ProspectiveStudentFolder GetProspectiveStudentFolder(String SiteID, Int32 FormID)
        {
            return new ProspectiveStudentFolderDao().Get(SiteID, FormID);
        }
        public static int InsertProspectiveStudentFolder(ProspectiveStudentFolder record)
        {
            return new ProspectiveStudentFolderDao().Insert(record);
        }
        public static int UpdateProspectiveStudentFolder(ProspectiveStudentFolder record)
        {
            return new ProspectiveStudentFolderDao().Update(record);
        }
        public static int DeleteProspectiveStudentFolder(String SiteID, Int32 FormID)
        {
            return new ProspectiveStudentFolderDao().Delete(SiteID, FormID);
        }
        public static List<ProspectiveStudentFolder> GetProspectiveStudentFolderList(string filterExpression)
        {
            List<ProspectiveStudentFolder> result = new List<ProspectiveStudentFolder>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProspectiveStudentFolder));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProspectiveStudentFolder)helper.IDataReaderToObject(reader, new ProspectiveStudentFolder()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ProspectiveStudentFolder> GetProspectiveStudentFolderList(string filterExpression, IDbContext ctx)
        {
            List<ProspectiveStudentFolder> result = new List<ProspectiveStudentFolder>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProspectiveStudentFolder));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProspectiveStudentFolder)helper.IDataReaderToObject(reader, new ProspectiveStudentFolder()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<ProspectiveStudentForm> GetProspectiveStudentFormList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<ProspectiveStudentForm> result = new List<ProspectiveStudentForm>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProspectiveStudentForm));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProspectiveStudentForm)helper.IDataReaderToObject(reader, new ProspectiveStudentForm()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetProspectiveStudentFormRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProspectiveStudentForm));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetProspectiveStudentFormRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProspectiveStudentForm));
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
        #region ProspectiveStudentFolderStatus
        public static ProspectiveStudentFolderStatus GetProspectiveStudentFolderStatus(Int32 ProspectiveStudentID, Int32 FormID)
        {
            return new ProspectiveStudentFolderStatusDao().Get(ProspectiveStudentID, FormID);
        }
        public static int InsertProspectiveStudentFolderStatus(ProspectiveStudentFolderStatus record)
        {
            return new ProspectiveStudentFolderStatusDao().Insert(record);
        }
        public static int UpdateProspectiveStudentFolderStatus(ProspectiveStudentFolderStatus record)
        {
            return new ProspectiveStudentFolderStatusDao().Update(record);
        }
        public static int DeleteProspectiveStudentFolderStatus(Int32 ProspectiveStudentID, Int32 FormID)
        {
            return new ProspectiveStudentFolderStatusDao().Delete(ProspectiveStudentID, FormID);
        }
        public static List<ProspectiveStudentFolderStatus> GetProspectiveStudentFolderStatusList(string filterExpression)
        {
            List<ProspectiveStudentFolderStatus> result = new List<ProspectiveStudentFolderStatus>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProspectiveStudentFolderStatus));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProspectiveStudentFolderStatus)helper.IDataReaderToObject(reader, new ProspectiveStudentFolderStatus()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ProspectiveStudentFolderStatus> GetProspectiveStudentFolderStatusList(string filterExpression, IDbContext ctx)
        {
            List<ProspectiveStudentFolderStatus> result = new List<ProspectiveStudentFolderStatus>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProspectiveStudentFolderStatus));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProspectiveStudentFolderStatus)helper.IDataReaderToObject(reader, new ProspectiveStudentFolderStatus()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ProspectiveStudentForm
        public static ProspectiveStudentForm GetProspectiveStudentForm(Int32 FormID)
        {
            return new ProspectiveStudentFormDao().Get(FormID);
        }
        public static int InsertProspectiveStudentForm(ProspectiveStudentForm record)
        {
            return new ProspectiveStudentFormDao().Insert(record);
        }
        public static int UpdateProspectiveStudentForm(ProspectiveStudentForm record)
        {
            return new ProspectiveStudentFormDao().Update(record);
        }
        public static int DeleteProspectiveStudentForm(Int32 FormID)
        {
            return new ProspectiveStudentFormDao().Delete(FormID);
        }
        public static List<ProspectiveStudentForm> GetProspectiveStudentFormList(string filterExpression)
        {
            List<ProspectiveStudentForm> result = new List<ProspectiveStudentForm>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProspectiveStudentForm));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProspectiveStudentForm)helper.IDataReaderToObject(reader, new ProspectiveStudentForm()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetProspectiveStudentFormMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ProspectiveStudentForm));
                ctx.CommandText = helper.SelectMaxColumn("FormID");
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
        #region ProspectiveStudentFamily
        public static ProspectiveStudentFamily GetProspectiveStudentFamily(Int32 FamilyID)
        {
            return new ProspectiveStudentFamilyDao().Get(FamilyID);
        }
        public static int InsertProspectiveStudentFamily(ProspectiveStudentFamily record)
        {
            return new ProspectiveStudentFamilyDao().Insert(record);
        }
        public static int UpdateProspectiveStudentFamily(ProspectiveStudentFamily record)
        {
            return new ProspectiveStudentFamilyDao().Update(record);
        }
        public static int DeleteProspectiveStudentFamily(Int32 FamilyID)
        {
            return new ProspectiveStudentFamilyDao().Delete(FamilyID);
        }
        public static List<ProspectiveStudentFamily> GetProspectiveStudentFamilyList(string filterExpression)
        {
            List<ProspectiveStudentFamily> result = new List<ProspectiveStudentFamily>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProspectiveStudentFamily));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProspectiveStudentFamily)helper.IDataReaderToObject(reader, new ProspectiveStudentFamily()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ProspectiveStudentFamily> GetProspectiveStudentFamilyList(string filterExpression, IDbContext ctx)
        {
            List<ProspectiveStudentFamily> result = new List<ProspectiveStudentFamily>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProspectiveStudentFamily));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProspectiveStudentFamily)helper.IDataReaderToObject(reader, new ProspectiveStudentFamily()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetProspectiveStudentFamilyMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ProspectiveStudentFamily));
                ctx.CommandText = helper.SelectMaxColumn("FamilyID");
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
        #region ProspectiveStudentPastStudy
        public static ProspectiveStudentPastStudy GetProspectiveStudentPastStudy(Int32 ProspectiveStudentPastStudyID)
        {
            return new ProspectiveStudentPastStudyDao().Get(ProspectiveStudentPastStudyID);
        }
        public static int InsertProspectiveStudentPastStudy(ProspectiveStudentPastStudy record)
        {
            return new ProspectiveStudentPastStudyDao().Insert(record);
        }
        public static int UpdateProspectiveStudentPastStudy(ProspectiveStudentPastStudy record)
        {
            return new ProspectiveStudentPastStudyDao().Update(record);
        }
        public static int DeleteProspectiveStudentPastStudy(Int32 ProspectiveStudentPastStudyID)
        {
            return new ProspectiveStudentPastStudyDao().Delete(ProspectiveStudentPastStudyID);
        }
        public static List<ProspectiveStudentPastStudy> GetProspectiveStudentPastStudyList(string filterExpression)
        {
            List<ProspectiveStudentPastStudy> result = new List<ProspectiveStudentPastStudy>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProspectiveStudentPastStudy));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProspectiveStudentPastStudy)helper.IDataReaderToObject(reader, new ProspectiveStudentPastStudy()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region PurchaseInvoiceDt
        public static PurchaseInvoiceDt GetPurchaseInvoiceDt(Int32 ID)
        {
            return new PurchaseInvoiceDtDao().Get(ID);
        }
        public static int InsertPurchaseInvoiceDt(PurchaseInvoiceDt record)
        {
            return new PurchaseInvoiceDtDao().Insert(record);
        }
        public static int UpdatePurchaseInvoiceDt(PurchaseInvoiceDt record)
        {
            return new PurchaseInvoiceDtDao().Update(record);
        }
        public static int DeletePurchaseInvoiceDt(Int32 ID)
        {
            return new PurchaseInvoiceDtDao().Delete(ID);
        }
        public static List<PurchaseInvoiceDt> GetPurchaseInvoiceDtList(string filterExpression)
        {
            List<PurchaseInvoiceDt> result = new List<PurchaseInvoiceDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseInvoiceDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseInvoiceDt)helper.IDataReaderToObject(reader, new PurchaseInvoiceDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<PurchaseInvoiceDt> GetPurchaseInvoiceDtList(string filterExpression, IDbContext ctx)
        {
            List<PurchaseInvoiceDt> result = new List<PurchaseInvoiceDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseInvoiceDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseInvoiceDt)helper.IDataReaderToObject(reader, new PurchaseInvoiceDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetPurchaseInvoiceDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseInvoiceDt));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region PurchaseInvoiceDtPayment
        public static PurchaseInvoiceDtPayment GetPurchaseInvoiceDtPayment(Int32 ID)
        {
            return new PurchaseInvoiceDtPaymentDao().Get(ID);
        }
        public static int InsertPurchaseInvoiceDtPayment(PurchaseInvoiceDtPayment record)
        {
            return new PurchaseInvoiceDtPaymentDao().Insert(record);
        }
        public static int UpdatePurchaseInvoiceDtPayment(PurchaseInvoiceDtPayment record)
        {
            return new PurchaseInvoiceDtPaymentDao().Update(record);
        }
        public static int DeletePurchaseInvoiceDtPayment(Int32 ID)
        {
            return new PurchaseInvoiceDtPaymentDao().Delete(ID);
        }
        public static List<PurchaseInvoiceDtPayment> GetPurchaseInvoiceDtPaymentList(string filterExpression)
        {
            List<PurchaseInvoiceDtPayment> result = new List<PurchaseInvoiceDtPayment>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseInvoiceDtPayment));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseInvoiceDtPayment)helper.IDataReaderToObject(reader, new PurchaseInvoiceDtPayment()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region PurchaseInvoiceHd
        public static PurchaseInvoiceHd GetPurchaseInvoiceHd(Int32 PurchaseInvoiceID)
        {
            return new PurchaseInvoiceHdDao().Get(PurchaseInvoiceID);
        }
        public static int InsertPurchaseInvoiceHd(PurchaseInvoiceHd record)
        {
            return new PurchaseInvoiceHdDao().Insert(record);
        }
        public static int UpdatePurchaseInvoiceHd(PurchaseInvoiceHd record)
        {
            return new PurchaseInvoiceHdDao().Update(record);
        }
        public static int DeletePurchaseInvoiceHd(Int32 PurchaseInvoiceID)
        {
            return new PurchaseInvoiceHdDao().Delete(PurchaseInvoiceID);
        }
        public static List<PurchaseInvoiceHd> GetPurchaseInvoiceHdList(string filterExpression)
        {
            IDbContext ctx = DbFactory.Configure();
            return GetPurchaseInvoiceHdList(filterExpression, ctx); ;
        }
        public static List<PurchaseInvoiceHd> GetPurchaseInvoiceHdList(string filterExpression, IDbContext ctx)
        {
            List<PurchaseInvoiceHd> result = new List<PurchaseInvoiceHd>();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseInvoiceHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseInvoiceHd)helper.IDataReaderToObject(reader, new PurchaseInvoiceHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetPurchaseInvoiceHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseInvoiceHd));
                ctx.CommandText = helper.SelectMaxColumn("PurchaseInvoiceID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }

        public static Int32 GetPurchaseInvoiceHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseInvoiceHd));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region PurchaseInvoiceHdPayment
        public static PurchaseInvoiceHdPayment GetPurchaseInvoiceHdPayment(Int32 ID)
        {
            return new PurchaseInvoiceHdPaymentDao().Get(ID);
        }
        public static int InsertPurchaseInvoiceHdPayment(PurchaseInvoiceHdPayment record)
        {
            return new PurchaseInvoiceHdPaymentDao().Insert(record);
        }
        public static int UpdatePurchaseInvoiceHdPayment(PurchaseInvoiceHdPayment record)
        {
            return new PurchaseInvoiceHdPaymentDao().Update(record);
        }
        public static int DeletePurchaseInvoiceHdPayment(Int32 ID)
        {
            return new PurchaseInvoiceHdPaymentDao().Delete(ID);
        }
        public static List<PurchaseInvoiceHdPayment> GetPurchaseInvoiceHdPaymentList(string filterExpression)
        {
            List<PurchaseInvoiceHdPayment> result = new List<PurchaseInvoiceHdPayment>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseInvoiceHdPayment));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseInvoiceHdPayment)helper.IDataReaderToObject(reader, new PurchaseInvoiceHdPayment()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region PurchaseOrderDt
        public static PurchaseOrderDt GetPurchaseOrderDt(Int32 ID)
        {
            return new PurchaseOrderDtDao().Get(ID);
        }
        public static int InsertPurchaseOrderDt(PurchaseOrderDt record)
        {
            return new PurchaseOrderDtDao().Insert(record);
        }
        public static int UpdatePurchaseOrderDt(PurchaseOrderDt record)
        {
            return new PurchaseOrderDtDao().Update(record);
        }
        public static int DeletePurchaseOrderDt(Int32 ID)
        {
            return new PurchaseOrderDtDao().Delete(ID);
        }
        public static List<PurchaseOrderDt> GetPurchaseOrderDtList(string filterExpression)
        {
            List<PurchaseOrderDt> result = new List<PurchaseOrderDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseOrderDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseOrderDt)helper.IDataReaderToObject(reader, new PurchaseOrderDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<PurchaseOrderDt> GetPurchaseOrderDtList(string filterExpression, IDbContext ctx)
        {
            List<PurchaseOrderDt> result = new List<PurchaseOrderDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseOrderDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseOrderDt)helper.IDataReaderToObject(reader, new PurchaseOrderDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetPurchaseOrderDtRowCount(string filterExpression, IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseOrderDt));
                ctx.CommandText = helper.GetRowCount(filterExpression);
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
        #region PurchaseOrderHd
        public static PurchaseOrderHd GetPurchaseOrderHd(Int32 PurchaseOrderID)
        {
            return new PurchaseOrderHdDao().Get(PurchaseOrderID);
        }
        public static int InsertPurchaseOrderHd(PurchaseOrderHd record)
        {
            return new PurchaseOrderHdDao().Insert(record);
        }
        public static int UpdatePurchaseOrderHd(PurchaseOrderHd record)
        {
            return new PurchaseOrderHdDao().Update(record);
        }
        public static int DeletePurchaseOrderHd(Int32 PurchaseOrderID)
        {
            return new PurchaseOrderHdDao().Delete(PurchaseOrderID);
        }
        public static List<PurchaseOrderHd> GetPurchaseOrderHdList(string filterExpression)
        {
            List<PurchaseOrderHd> result = new List<PurchaseOrderHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseOrderHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseOrderHd)helper.IDataReaderToObject(reader, new PurchaseOrderHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<PurchaseOrderHd> GetPurchaseOrderHdList(string filterExpression, IDbContext ctx)
        {
            List<PurchaseOrderHd> result = new List<PurchaseOrderHd>();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseOrderHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseOrderHd)helper.IDataReaderToObject(reader, new PurchaseOrderHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetPurchaseOrderHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseOrderHd));
                ctx.CommandText = helper.SelectMaxColumn("PurchaseOrderID");
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
        #region PurchaseReceiveDt
        public static PurchaseReceiveDt GetPurchaseReceiveDt(Int32 ID)
        {
            return new PurchaseReceiveDtDao().Get(ID);
        }
        public static int InsertPurchaseReceiveDt(PurchaseReceiveDt record)
        {
            return new PurchaseReceiveDtDao().Insert(record);
        }
        public static int UpdatePurchaseReceiveDt(PurchaseReceiveDt record)
        {
            return new PurchaseReceiveDtDao().Update(record);
        }
        public static int DeletePurchaseReceiveDt(Int32 ID)
        {
            return new PurchaseReceiveDtDao().Delete(ID);
        }
        public static List<PurchaseReceiveDt> GetPurchaseReceiveDtList(string filterExpression)
        {
            List<PurchaseReceiveDt> result = new List<PurchaseReceiveDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseReceiveDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseReceiveDt)helper.IDataReaderToObject(reader, new PurchaseReceiveDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<PurchaseReceiveDt> GetPurchaseReceiveDtList(string filterExpression, IDbContext ctx)
        {
            List<PurchaseReceiveDt> result = new List<PurchaseReceiveDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseReceiveDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseReceiveDt)helper.IDataReaderToObject(reader, new PurchaseReceiveDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetPurchaseReceiveDtMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseReceiveDt));
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
        #region PurchaseReceiveDtExpired
        public static PurchaseReceiveDtExpired GetPurchaseReceiveDtExpired(Int32 ID, String BatchNumber)
        {
            return new PurchaseReceiveDtExpiredDao().Get(ID, BatchNumber);
        }
        public static int InsertPurchaseReceiveDtExpired(PurchaseReceiveDtExpired record)
        {
            return new PurchaseReceiveDtExpiredDao().Insert(record);
        }
        public static int UpdatePurchaseReceiveDtExpired(PurchaseReceiveDtExpired record)
        {
            return new PurchaseReceiveDtExpiredDao().Update(record);
        }
        public static int DeletePurchaseReceiveDtExpired(Int32 ID, String BatchNumber)
        {
            return new PurchaseReceiveDtExpiredDao().Delete(ID, BatchNumber);
        }
        public static List<PurchaseReceiveDtExpired> GetPurchaseReceiveDtExpiredList(string filterExpression)
        {
            List<PurchaseReceiveDtExpired> result = new List<PurchaseReceiveDtExpired>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseReceiveDtExpired));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseReceiveDtExpired)helper.IDataReaderToObject(reader, new PurchaseReceiveDtExpired()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region PurchaseReceiveHd
        public static PurchaseReceiveHd GetPurchaseReceiveHd(Int32 PurchaseReceiveID)
        {
            return new PurchaseReceiveHdDao().Get(PurchaseReceiveID);
        }
        public static int InsertPurchaseReceiveHd(PurchaseReceiveHd record)
        {
            return new PurchaseReceiveHdDao().Insert(record);
        }
        public static int UpdatePurchaseReceiveHd(PurchaseReceiveHd record)
        {
            return new PurchaseReceiveHdDao().Update(record);
        }
        public static int DeletePurchaseReceiveHd(Int32 PurchaseReceiveID)
        {
            return new PurchaseReceiveHdDao().Delete(PurchaseReceiveID);
        }
        public static List<PurchaseReceiveHd> GetPurchaseReceiveHdList(string filterExpression)
        {
            List<PurchaseReceiveHd> result = new List<PurchaseReceiveHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseReceiveHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseReceiveHd)helper.IDataReaderToObject(reader, new PurchaseReceiveHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<PurchaseReceiveHd> GetPurchaseReceiveHdList(string filterExpression, IDbContext ctx)
        {
            List<PurchaseReceiveHd> result = new List<PurchaseReceiveHd>();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseReceiveHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseReceiveHd)helper.IDataReaderToObject(reader, new PurchaseReceiveHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetPurchaseReceiveHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseReceiveHd));
                ctx.CommandText = helper.SelectMaxColumn("PurchaseReceiveID");
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
        #region PurchaseReceivePO
        public static PurchaseReceivePO GetPurchaseReceivePO(Int32 ID)
        {
            return new PurchaseReceivePODao().Get(ID);
        }
        public static int InsertPurchaseReceivePO(PurchaseReceivePO record)
        {
            return new PurchaseReceivePODao().Insert(record);
        }
        public static int UpdatePurchaseReceivePO(PurchaseReceivePO record)
        {
            return new PurchaseReceivePODao().Update(record);
        }
        public static int DeletePurchaseReceivePO(Int32 ID)
        {
            return new PurchaseReceivePODao().Delete(ID);
        }
        public static List<PurchaseReceivePO> GetPurchaseReceivePOList(string filterExpression)
        {
            List<PurchaseReceivePO> result = new List<PurchaseReceivePO>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseReceivePO));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseReceivePO)helper.IDataReaderToObject(reader, new PurchaseReceivePO()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region PurchaseReplacementDt
        public static PurchaseReplacementDt GetPurchaseReplacementDt(Int32 ID)
        {
            return new PurchaseReplacementDtDao().Get(ID);
        }
        public static int InsertPurchaseReplacementDt(PurchaseReplacementDt record)
        {
            return new PurchaseReplacementDtDao().Insert(record);
        }
        public static int UpdatePurchaseReplacementDt(PurchaseReplacementDt record)
        {
            return new PurchaseReplacementDtDao().Update(record);
        }
        public static int DeletePurchaseReplacementDt(Int32 ID)
        {
            return new PurchaseReplacementDtDao().Delete(ID);
        }
        public static List<PurchaseReplacementDt> GetPurchaseReplacementDtList(string filterExpression)
        {
            List<PurchaseReplacementDt> result = new List<PurchaseReplacementDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseReplacementDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseReplacementDt)helper.IDataReaderToObject(reader, new PurchaseReplacementDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region PurchaseReplacementHd
        public static PurchaseReplacementHd GetPurchaseReplacementHd(Int32 PurchaseReplacementID)
        {
            return new PurchaseReplacementHdDao().Get(PurchaseReplacementID);
        }
        public static int InsertPurchaseReplacementHd(PurchaseReplacementHd record)
        {
            return new PurchaseReplacementHdDao().Insert(record);
        }
        public static int UpdatePurchaseReplacementHd(PurchaseReplacementHd record)
        {
            return new PurchaseReplacementHdDao().Update(record);
        }
        public static int DeletePurchaseReplacementHd(Int32 PurchaseReplacementID)
        {
            return new PurchaseReplacementHdDao().Delete(PurchaseReplacementID);
        }
        public static List<PurchaseReplacementHd> GetPurchaseReplacementHdList(string filterExpression)
        {
            List<PurchaseReplacementHd> result = new List<PurchaseReplacementHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseReplacementHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseReplacementHd)helper.IDataReaderToObject(reader, new PurchaseReplacementHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetPurchaseReplacementHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseReplacementHd));
                ctx.CommandText = helper.SelectMaxColumn("PurchaseReplacementID");
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
        #region PurchaseRequestDP
        public static PurchaseRequestDP GetPurchaseRequestDP(Int32 ID)
        {
            return new PurchaseRequestDPDao().Get(ID);
        }
        public static int InsertPurchaseRequestDP(PurchaseRequestDP record)
        {
            return new PurchaseRequestDPDao().Insert(record);
        }
        public static int UpdatePurchaseRequestDP(PurchaseRequestDP record)
        {
            return new PurchaseRequestDPDao().Update(record);
        }
        public static int DeletePurchaseRequestDP(Int32 ID)
        {
            return new PurchaseRequestDPDao().Delete(ID);
        }
        public static List<PurchaseRequestDP> GetPurchaseRequestDPList(string filterExpression)
        {
            List<PurchaseRequestDP> result = new List<PurchaseRequestDP>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseRequestDP));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseRequestDP)helper.IDataReaderToObject(reader, new PurchaseRequestDP()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region PurchaseRequestDt
        public static PurchaseRequestDt GetPurchaseRequestDt(Int32 ID)
        {
            return new PurchaseRequestDtDao().Get(ID);
        }
        public static int InsertPurchaseRequestDt(PurchaseRequestDt record)
        {
            return new PurchaseRequestDtDao().Insert(record);
        }
        public static int UpdatePurchaseRequestDt(PurchaseRequestDt record)
        {
            return new PurchaseRequestDtDao().Update(record);
        }
        public static int DeletePurchaseRequestDt(Int32 ID)
        {
            return new PurchaseRequestDtDao().Delete(ID);
        }
        public static List<PurchaseRequestDt> GetPurchaseRequestDtList(string filterExpression)
        {
            List<PurchaseRequestDt> result = new List<PurchaseRequestDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseRequestDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseRequestDt)helper.IDataReaderToObject(reader, new PurchaseRequestDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        public static List<PurchaseRequestDt> GetPurchaseRequestDtList(string filterExpression, IDbContext ctx)
        {
            List<PurchaseRequestDt> result = new List<PurchaseRequestDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseRequestDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseRequestDt)helper.IDataReaderToObject(reader, new PurchaseRequestDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetPurchaseRequestDtRowCount(string filterExpression, IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseRequestDt));
                ctx.CommandText = helper.GetRowCount(filterExpression);
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
        #region PurchaseRequestHd
        public static PurchaseRequestHd GetPurchaseRequestHd(Int32 PurchaseRequestID)
        {
            return new PurchaseRequestHdDao().Get(PurchaseRequestID);
        }
        public static int InsertPurchaseRequestHd(PurchaseRequestHd record)
        {
            return new PurchaseRequestHdDao().Insert(record);
        }
        public static int UpdatePurchaseRequestHd(PurchaseRequestHd record)
        {
            return new PurchaseRequestHdDao().Update(record);
        }
        public static int DeletePurchaseRequestHd(Int32 PurchaseRequestID)
        {
            return new PurchaseRequestHdDao().Delete(PurchaseRequestID);
        }
        public static List<PurchaseRequestHd> GetPurchaseRequestHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<PurchaseRequestHd> result = new List<PurchaseRequestHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseRequestHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseRequestHd)helper.IDataReaderToObject(reader, new PurchaseRequestHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<PurchaseRequestHd> GetPurchaseRequestHdList(string filterExpression)
        {
            List<PurchaseRequestHd> result = new List<PurchaseRequestHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseRequestHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseRequestHd)helper.IDataReaderToObject(reader, new PurchaseRequestHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetPurchaseRequestHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseRequestHd));
                ctx.CommandText = helper.SelectMaxColumn("PurchaseRequestID");
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
        #region PurchaseRequestPO
        public static PurchaseRequestPO GetPurchaseRequestPO(Int32 ID)
        {
            return new PurchaseRequestPODao().Get(ID);
        }
        public static int InsertPurchaseRequestPO(PurchaseRequestPO record)
        {
            return new PurchaseRequestPODao().Insert(record);
        }
        public static int UpdatePurchaseRequestPO(PurchaseRequestPO record)
        {
            return new PurchaseRequestPODao().Update(record);
        }
        public static int DeletePurchaseRequestPO(Int32 ID)
        {
            return new PurchaseRequestPODao().Delete(ID);
        }
        public static List<PurchaseRequestPO> GetPurchaseRequestPOList(string filterExpression)
        {
            List<PurchaseRequestPO> result = new List<PurchaseRequestPO>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseRequestPO));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseRequestPO)helper.IDataReaderToObject(reader, new PurchaseRequestPO()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<PurchaseRequestPO> GetPurchaseRequestPOList(string filterExpression, IDbContext ctx)
        {
            List<PurchaseRequestPO> result = new List<PurchaseRequestPO>();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseRequestPO));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseRequestPO)helper.IDataReaderToObject(reader, new PurchaseRequestPO()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region PurchaseReturnDt
        public static PurchaseReturnDt GetPurchaseReturnDt(Int32 ID)
        {
            return new PurchaseReturnDtDao().Get(ID);
        }
        public static int InsertPurchaseReturnDt(PurchaseReturnDt record)
        {
            return new PurchaseReturnDtDao().Insert(record);
        }
        public static int UpdatePurchaseReturnDt(PurchaseReturnDt record)
        {
            return new PurchaseReturnDtDao().Update(record);
        }
        public static int DeletePurchaseReturnDt(Int32 ID)
        {
            return new PurchaseReturnDtDao().Delete(ID);
        }
        public static List<PurchaseReturnDt> GetPurchaseReturnDtList(string filterExpression)
        {
            List<PurchaseReturnDt> result = new List<PurchaseReturnDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseReturnDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseReturnDt)helper.IDataReaderToObject(reader, new PurchaseReturnDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<PurchaseReturnDt> GetPurchaseReturnDtList(string filterExpression, IDbContext ctx)
        {
            List<PurchaseReturnDt> result = new List<PurchaseReturnDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseReturnDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseReturnDt)helper.IDataReaderToObject(reader, new PurchaseReturnDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region PurchaseReturnHd
        public static PurchaseReturnHd GetPurchaseReturnHd(Int32 PurchaseReturnID)
        {
            return new PurchaseReturnHdDao().Get(PurchaseReturnID);
        }
        public static int InsertPurchaseReturnHd(PurchaseReturnHd record)
        {
            return new PurchaseReturnHdDao().Insert(record);
        }
        public static int UpdatePurchaseReturnHd(PurchaseReturnHd record)
        {
            return new PurchaseReturnHdDao().Update(record);
        }
        public static int DeletePurchaseReturnHd(Int32 PurchaseReturnID)
        {
            return new PurchaseReturnHdDao().Delete(PurchaseReturnID);
        }
        public static List<PurchaseReturnHd> GetPurchaseReturnHdList(string filterExpression)
        {
            List<PurchaseReturnHd> result = new List<PurchaseReturnHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseReturnHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((PurchaseReturnHd)helper.IDataReaderToObject(reader, new PurchaseReturnHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetPurchaseReturnHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(PurchaseReturnHd));
                ctx.CommandText = helper.SelectMaxColumn("PurchaseReturnID");
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
        #region RBudgetRealizationDt
        public static RBudgetRealizationDt GetRBudgetRealizationDt(Int32 BudgetRealizationDtID)
        {
            return new RBudgetRealizationDtDao().Get(BudgetRealizationDtID);
        }
        public static int InsertRBudgetRealizationDt(RBudgetRealizationDt record)
        {
            return new RBudgetRealizationDtDao().Insert(record);
        }
        public static int UpdateRBudgetRealizationDt(RBudgetRealizationDt record)
        {
            return new RBudgetRealizationDtDao().Update(record);
        }
        public static int DeleteRBudgetRealizationDt(Int32 BudgetRealizationDtID)
        {
            return new RBudgetRealizationDtDao().Delete(BudgetRealizationDtID);
        }
        public static List<RBudgetRealizationDt> GetRBudgetRealizationDtList(string filterExpression)
        {
            List<RBudgetRealizationDt> result = new List<RBudgetRealizationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RBudgetRealizationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RBudgetRealizationDt)helper.IDataReaderToObject(reader, new RBudgetRealizationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<RBudgetRealizationDt> GetRBudgetRealizationDtList(string filterExpression, IDbContext ctx)
        {
            List<RBudgetRealizationDt> result = new List<RBudgetRealizationDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(RBudgetRealizationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RBudgetRealizationDt)helper.IDataReaderToObject(reader, new RBudgetRealizationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<RBudgetRealizationDt> GetRBudgetRealizationDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<RBudgetRealizationDt> result = new List<RBudgetRealizationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RBudgetRealizationDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RBudgetRealizationDt)helper.IDataReaderToObject(reader, new RBudgetRealizationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRBudgetRealizationDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RBudgetRealizationDt));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region RBudgetRealizationDtFund
        public static RBudgetRealizationDtFund GetRBudgetRealizationDtFund(Int32 BudgetRealizationDtID, String GCProjectFundType)
        {
            return new RBudgetRealizationDtFundDao().Get(BudgetRealizationDtID, GCProjectFundType);
        }
        public static int InsertRBudgetRealizationDtFund(RBudgetRealizationDtFund record)
        {
            return new RBudgetRealizationDtFundDao().Insert(record);
        }
        public static int UpdateRBudgetRealizationDtFund(RBudgetRealizationDtFund record)
        {
            return new RBudgetRealizationDtFundDao().Update(record);
        }
        public static int DeleteRBudgetRealizationDtFund(Int32 BudgetRealizationDtID, String GCProjectFundType)
        {
            return new RBudgetRealizationDtFundDao().Delete(BudgetRealizationDtID, GCProjectFundType);
        }
        public static List<RBudgetRealizationDtFund> GetRBudgetRealizationDtFundList(string filterExpression)
        {
            List<RBudgetRealizationDtFund> result = new List<RBudgetRealizationDtFund>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RBudgetRealizationDtFund));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RBudgetRealizationDtFund)helper.IDataReaderToObject(reader, new RBudgetRealizationDtFund()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region RBudgetRealizationHd
        public static RBudgetRealizationHd GetRBudgetRealizationHd(Int32 BudgetRealizationID)
        {
            return new RBudgetRealizationHdDao().Get(BudgetRealizationID);
        }
        public static int InsertRBudgetRealizationHd(RBudgetRealizationHd record)
        {
            return new RBudgetRealizationHdDao().Insert(record);
        }
        public static int UpdateRBudgetRealizationHd(RBudgetRealizationHd record)
        {
            return new RBudgetRealizationHdDao().Update(record);
        }
        public static int DeleteRBudgetRealizationHd(Int32 BudgetRealizationID)
        {
            return new RBudgetRealizationHdDao().Delete(BudgetRealizationID);
        }
        public static List<RBudgetRealizationHd> GetRBudgetRealizationHdList(string filterExpression)
        {
            List<RBudgetRealizationHd> result = new List<RBudgetRealizationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RBudgetRealizationHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RBudgetRealizationHd)helper.IDataReaderToObject(reader, new RBudgetRealizationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<RBudgetRealizationHd> GetRBudgetRealizationHdList(string filterExpression, IDbContext ctx)
        {
            List<RBudgetRealizationHd> result = new List<RBudgetRealizationHd>();
            try
            {
                DbHelper helper = new DbHelper(typeof(RBudgetRealizationHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RBudgetRealizationHd)helper.IDataReaderToObject(reader, new RBudgetRealizationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region RBudgetRequestDt
        public static RBudgetRequestDt GetRBudgetRequestDt(Int32 BudgetRequestDtID)
        {
            return new RBudgetRequestDtDao().Get(BudgetRequestDtID);
        }
        public static int InsertRBudgetRequestDt(RBudgetRequestDt record)
        {
            return new RBudgetRequestDtDao().Insert(record);
        }
        public static int UpdateRBudgetRequestDt(RBudgetRequestDt record)
        {
            return new RBudgetRequestDtDao().Update(record);
        }
        public static int DeleteRBudgetRequestDt(Int32 BudgetRequestDtID)
        {
            return new RBudgetRequestDtDao().Delete(BudgetRequestDtID);
        }
        public static List<RBudgetRequestDt> GetRBudgetRequestDtList(string filterExpression)
        {
            List<RBudgetRequestDt> result = new List<RBudgetRequestDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RBudgetRequestDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RBudgetRequestDt)helper.IDataReaderToObject(reader, new RBudgetRequestDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<RBudgetRequestDt> GetRBudgetRequestDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<RBudgetRequestDt> result = new List<RBudgetRequestDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RBudgetRequestDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RBudgetRequestDt)helper.IDataReaderToObject(reader, new RBudgetRequestDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRBudgetRequestDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RBudgetRequestDt));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRBudgetRequestDtRowCount(string filterExpression, IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(RBudgetRequestDt));
                ctx.CommandText = helper.GetRowCount(filterExpression);
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
        #region RBudgetRequestDtFund
        public static RBudgetRequestDtFund GetRBudgetRequestDtFund(Int32 BudgetRequestDtID, String GCProjectFundType)
        {
            return new RBudgetRequestDtFundDao().Get(BudgetRequestDtID, GCProjectFundType);
        }
        public static int InsertRBudgetRequestDtFund(RBudgetRequestDtFund record)
        {
            return new RBudgetRequestDtFundDao().Insert(record);
        }
        public static int UpdateRBudgetRequestDtFund(RBudgetRequestDtFund record)
        {
            return new RBudgetRequestDtFundDao().Update(record);
        }
        public static int DeleteRBudgetRequestDtFund(Int32 BudgetRequestDtID, String GCProjectFundType)
        {
            return new RBudgetRequestDtFundDao().Delete(BudgetRequestDtID, GCProjectFundType);
        }
        public static List<RBudgetRequestDtFund> GetRBudgetRequestDtFundList(string filterExpression)
        {
            List<RBudgetRequestDtFund> result = new List<RBudgetRequestDtFund>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RBudgetRequestDtFund));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RBudgetRequestDtFund)helper.IDataReaderToObject(reader, new RBudgetRequestDtFund()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<RBudgetRequestDtFund> GetRBudgetRequestDtFundList(string filterExpression, IDbContext ctx)
        {
            List<RBudgetRequestDtFund> result = new List<RBudgetRequestDtFund>();
            try
            {
                DbHelper helper = new DbHelper(typeof(RBudgetRequestDtFund));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RBudgetRequestDtFund)helper.IDataReaderToObject(reader, new RBudgetRequestDtFund()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region RBudgetRequestHd
        public static RBudgetRequestHd GetRBudgetRequestHd(Int32 BudgetRequestID)
        {
            return new RBudgetRequestHdDao().Get(BudgetRequestID);
        }
        public static int InsertRBudgetRequestHd(RBudgetRequestHd record)
        {
            return new RBudgetRequestHdDao().Insert(record);
        }
        public static int UpdateRBudgetRequestHd(RBudgetRequestHd record)
        {
            return new RBudgetRequestHdDao().Update(record);
        }
        public static int DeleteRBudgetRequestHd(Int32 BudgetRequestID)
        {
            return new RBudgetRequestHdDao().Delete(BudgetRequestID);
        }
        public static List<RBudgetRequestHd> GetRBudgetRequestHdList(string filterExpression)
        {
            List<RBudgetRequestHd> result = new List<RBudgetRequestHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RBudgetRequestHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RBudgetRequestHd)helper.IDataReaderToObject(reader, new RBudgetRequestHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRBudgetRequestHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(RBudgetRequestHd));
                ctx.CommandText = helper.SelectMaxColumn("BudgetRequestID");
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
        #region Registration
        public static Registration GetRegistration(Int32 RegistrationID)
        {
            return new RegistrationDao().Get(RegistrationID);
        }
        public static int InsertRegistration(Registration record)
        {
            return new RegistrationDao().Insert(record);
        }
        public static int UpdateRegistration(Registration record)
        {
            return new RegistrationDao().Update(record);
        }
        public static int DeleteRegistration(Int32 RegistrationID)
        {
            return new RegistrationDao().Delete(RegistrationID);
        }
        public static List<Registration> GetRegistrationList(string filterExpression)
        {
            List<Registration> result = new List<Registration>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Registration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Registration)helper.IDataReaderToObject(reader, new Registration()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<Registration> GetRegistrationList(string filterExpression, IDbContext ctx)
        {
            List<Registration> result = new List<Registration>();            
            try
            {
                DbHelper helper = new DbHelper(typeof(Registration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Registration)helper.IDataReaderToObject(reader, new Registration()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetRegistrationMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(Registration));
                ctx.CommandText = helper.SelectMaxColumn("RegistrationID");
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
        #region RegistrationMark
        public static RegistrationMark GetRegistrationMark(Int32 PeriodAdmissionID, Int32 AdmissionSelectionID, Int32 RegistrationID)
        {
            return new RegistrationMarkDao().Get(PeriodAdmissionID, AdmissionSelectionID, RegistrationID);
        }
        public static int InsertRegistrationMark(RegistrationMark record)
        {
            return new RegistrationMarkDao().Insert(record);
        }
        public static int UpdateRegistrationMark(RegistrationMark record)
        {
            return new RegistrationMarkDao().Update(record);
        }
        public static int DeleteRegistrationMark(Int32 PeriodAdmissionID, Int32 AdmissionSelectionID, Int32 RegistrationID)
        {
            return new RegistrationMarkDao().Delete(PeriodAdmissionID, AdmissionSelectionID, RegistrationID);
        }
        public static List<RegistrationMark> GetRegistrationMarkList(string filterExpression)
        {
            List<RegistrationMark> result = new List<RegistrationMark>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RegistrationMark));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RegistrationMark)helper.IDataReaderToObject(reader, new RegistrationMark()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<RegistrationMark> GetRegistrationMarkList(string filterExpression, IDbContext ctx)
        {
            List<RegistrationMark> result = new List<RegistrationMark>();
            try
            {
                DbHelper helper = new DbHelper(typeof(RegistrationMark));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RegistrationMark)helper.IDataReaderToObject(reader, new RegistrationMark()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region RegistrationScholarship
        public static RegistrationScholarship GetRegistrationScholarship(Int32 RegistrationID, Int32 ScholarshipID)
        {
            return new RegistrationScholarshipDao().Get(RegistrationID, ScholarshipID);
        }
        public static int InsertRegistrationScholarship(RegistrationScholarship record)
        {
            return new RegistrationScholarshipDao().Insert(record);
        }
        public static int UpdateRegistrationScholarship(RegistrationScholarship record)
        {
            return new RegistrationScholarshipDao().Update(record);
        }
        public static int DeleteRegistrationScholarship(Int32 RegistrationID, Int32 ScholarshipID)
        {
            return new RegistrationScholarshipDao().Delete(RegistrationID, ScholarshipID);
        }
        public static List<RegistrationScholarship> GetRegistrationScholarshipList(string filterExpression)
        {
            List<RegistrationScholarship> result = new List<RegistrationScholarship>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RegistrationScholarship));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RegistrationScholarship)helper.IDataReaderToObject(reader, new RegistrationScholarship()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<RegistrationScholarship> GetRegistrationScholarshipList(string filterExpression, IDbContext ctx)
        {
            List<RegistrationScholarship> result = new List<RegistrationScholarship>();
            try
            {
                DbHelper helper = new DbHelper(typeof(RegistrationScholarship));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RegistrationScholarship)helper.IDataReaderToObject(reader, new RegistrationScholarship()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region RevenuePeriod
        public static RevenuePeriod GetRevenuePeriod(Int32 RevenuePeriodID)
        {
            return new RevenuePeriodDao().Get(RevenuePeriodID);
        }
        public static int InsertRevenuePeriod(RevenuePeriod record)
        {
            return new RevenuePeriodDao().Insert(record);
        }
        public static int UpdateRevenuePeriod(RevenuePeriod record)
        {
            return new RevenuePeriodDao().Update(record);
        }
        public static int DeleteRevenuePeriod(Int32 RevenuePeriodID)
        {
            return new RevenuePeriodDao().Delete(RevenuePeriodID);
        }
        public static List<RevenuePeriod> GetRevenuePeriodList(string filterExpression)
        {
            List<RevenuePeriod> result = new List<RevenuePeriod>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RevenuePeriod));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RevenuePeriod)helper.IDataReaderToObject(reader, new RevenuePeriod()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<RevenuePeriod> GetRevenuePeriodList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<RevenuePeriod> result = new List<RevenuePeriod>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RevenuePeriod));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RevenuePeriod)helper.IDataReaderToObject(reader, new RevenuePeriod()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRevenuePeriodRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RevenuePeriod));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRevenuePeriodRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RevenuePeriod));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "RevenuePeriodID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region RenumerationComp
        public static RenumerationComp GetRenumerationComp(Int32 RenumerationCompID)
        {
            return new RenumerationCompDao().Get(RenumerationCompID);
        }
        public static int InsertRenumerationComp(RenumerationComp record)
        {
            return new RenumerationCompDao().Insert(record);
        }
        public static int UpdateRenumerationComp(RenumerationComp record)
        {
            return new RenumerationCompDao().Update(record);
        }
        public static int DeleteRenumerationComp(Int32 RenumerationCompID)
        {
            return new RenumerationCompDao().Delete(RenumerationCompID);
        }
        public static List<RenumerationComp> GetRenumerationCompList(string filterExpression)
        {
            List<RenumerationComp> result = new List<RenumerationComp>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RenumerationComp));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RenumerationComp)helper.IDataReaderToObject(reader, new RenumerationComp()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region RenumerationHd
        public static RenumerationHd GetRenumerationHd(Int32 RenumerationID)
        {
            return new RenumerationHdDao().Get(RenumerationID);
        }
        public static int InsertRenumerationHd(RenumerationHd record)
        {
            return new RenumerationHdDao().Insert(record);
        }
        public static int UpdateRenumerationHd(RenumerationHd record)
        {
            return new RenumerationHdDao().Update(record);
        }
        public static int DeleteRenumerationHd(Int32 RenumerationID)
        {
            return new RenumerationHdDao().Delete(RenumerationID);
        }
        public static List<RenumerationHd> GetRenumerationHdList(string filterExpression)
        {
            List<RenumerationHd> result = new List<RenumerationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RenumerationHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RenumerationHd)helper.IDataReaderToObject(reader, new RenumerationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<RenumerationHd> GetRenumerationHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<RenumerationHd> result = new List<RenumerationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RenumerationHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RenumerationHd)helper.IDataReaderToObject(reader, new RenumerationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRenumerationHdCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RenumerationHd));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRenumerationHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RenumerationHd));
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
        #region RenumerationCompFormulaHd
        public static RenumerationCompFormulaHd GetRenumerationCompFormulaHd(Int32 FormulaID)
        {
            return new RenumerationCompFormulaHdDao().Get(FormulaID);
        }
        public static int InsertRenumerationCompFormulaHd(RenumerationCompFormulaHd record)
        {
            return new RenumerationCompFormulaHdDao().Insert(record);
        }
        public static int UpdateRenumerationCompFormulaHd(RenumerationCompFormulaHd record)
        {
            return new RenumerationCompFormulaHdDao().Update(record);
        }
        public static int DeleteRenumerationCompFormulaHd(Int32 FormulaID)
        {
            return new RenumerationCompFormulaHdDao().Delete(FormulaID);
        }
        public static List<RenumerationCompFormulaHd> GetRenumerationCompFormulaHdList(string filterExpression)
        {
            List<RenumerationCompFormulaHd> result = new List<RenumerationCompFormulaHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RenumerationCompFormulaHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RenumerationCompFormulaHd)helper.IDataReaderToObject(reader, new RenumerationCompFormulaHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<RenumerationCompFormulaHd> GetRenumerationCompFormulaHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<RenumerationCompFormulaHd> result = new List<RenumerationCompFormulaHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RenumerationCompFormulaHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RenumerationCompFormulaHd)helper.IDataReaderToObject(reader, new RenumerationCompFormulaHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRenumerationCompFormulaHdCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RenumerationCompFormulaHd));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRenumerationCompFormulaHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RenumerationCompFormulaHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "FormulaID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region RestrictionDt
        public static RestrictionDt GetRestrictionDt(Int32 RestrictionID, String TransactionCode)
        {
            return new RestrictionDtDao().Get(RestrictionID, TransactionCode);
        }
        public static int InsertRestrictionDt(RestrictionDt record)
        {
            return new RestrictionDtDao().Insert(record);
        }
        public static int UpdateRestrictionDt(RestrictionDt record)
        {
            return new RestrictionDtDao().Update(record);
        }
        public static int DeleteRestrictionDt(Int32 RestrictionID, String TransactionCode)
        {
            return new RestrictionDtDao().Delete(RestrictionID, TransactionCode);
        }
        public static List<RestrictionDt> GetRestrictionDtList(string filterExpression)
        {
            List<RestrictionDt> result = new List<RestrictionDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RestrictionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RestrictionDt)helper.IDataReaderToObject(reader, new RestrictionDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<RestrictionDt> GetRestrictionDtList(string filterExpression, IDbContext ctx)
        {
            List<RestrictionDt> result = new List<RestrictionDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(RestrictionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RestrictionDt)helper.IDataReaderToObject(reader, new RestrictionDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region RestrictionHd
        public static RestrictionHd GetRestrictionHd(Int32 RestrictionID)
        {
            return new RestrictionHdDao().Get(RestrictionID);
        }
        public static int InsertRestrictionHd(RestrictionHd record)
        {
            return new RestrictionHdDao().Insert(record);
        }
        public static int UpdateRestrictionHd(RestrictionHd record)
        {
            return new RestrictionHdDao().Update(record);
        }
        public static int DeleteRestrictionHd(Int32 RestrictionID)
        {
            return new RestrictionHdDao().Delete(RestrictionID);
        }
        public static List<RestrictionHd> GetRestrictionHdList(string filterExpression)
        {
            List<RestrictionHd> result = new List<RestrictionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RestrictionHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RestrictionHd)helper.IDataReaderToObject(reader, new RestrictionHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<RestrictionHd> GetRestrictionHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<RestrictionHd> result = new List<RestrictionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RestrictionHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RestrictionHd)helper.IDataReaderToObject(reader, new RestrictionHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRestrictionHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RestrictionHd));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRestrictionHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RestrictionHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "RestrictionID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRestrictionHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(RestrictionHd));
                ctx.CommandText = helper.SelectMaxColumn("RestrictionID");
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
        #region RProject
        public static RProject GetRProject(Int32 ProjectID)
        {
            return new RProjectDao().Get(ProjectID);
        }
        public static int InsertRProject(RProject record)
        {
            return new RProjectDao().Insert(record);
        }
        public static int UpdateRProject(RProject record)
        {
            return new RProjectDao().Update(record);
        }
        public static int DeleteRProject(Int32 ProjectID)
        {
            return new RProjectDao().Delete(ProjectID);
        }
        public static List<RProject> GetRProjectList(string filterExpression)
        {
            List<RProject> result = new List<RProject>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RProject));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RProject)helper.IDataReaderToObject(reader, new RProject()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRProjectMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(RProject));
                ctx.CommandText = helper.SelectMaxColumn("ProjectID");
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
        #region RProjectGroup
        public static RProjectGroup GetRProjectGroup(Int32 ProjectGroupID)
        {
            return new RProjectGroupDao().Get(ProjectGroupID);
        }
        public static int InsertRProjectGroup(RProjectGroup record)
        {
            return new RProjectGroupDao().Insert(record);
        }
        public static int UpdateRProjectGroup(RProjectGroup record)
        {
            return new RProjectGroupDao().Update(record);
        }
        public static int DeleteRProjectGroup(Int32 ProjectGroupID)
        {
            return new RProjectGroupDao().Delete(ProjectGroupID);
        }
        public static List<RProjectGroup> GetRProjectGroupList(string filterExpression)
        {
            List<RProjectGroup> result = new List<RProjectGroup>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RProjectGroup));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RProjectGroup)helper.IDataReaderToObject(reader, new RProjectGroup()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRProjectGroupMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(RProjectGroup));
                ctx.CommandText = helper.SelectMaxColumn("ProjectGroupID");
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
        #region RProjectLog
        public static RProjectLog GetRProjectLog(Int32 ProjectLogID)
        {
            return new RProjectLogDao().Get(ProjectLogID);
        }
        public static int InsertRProjectLog(RProjectLog record)
        {
            return new RProjectLogDao().Insert(record);
        }
        public static int UpdateRProjectLog(RProjectLog record)
        {
            return new RProjectLogDao().Update(record);
        }
        public static int DeleteRProjectLog(Int32 ProjectLogID)
        {
            return new RProjectLogDao().Delete(ProjectLogID);
        }
        public static List<RProjectLog> GetRProjectLogList(string filterExpression)
        {
            List<RProjectLog> result = new List<RProjectLog>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RProjectLog));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RProjectLog)helper.IDataReaderToObject(reader, new RProjectLog()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region RProjectOrganization
        public static RProjectOrganization GetRProjectOrganization(Int32 ProjectOrganizationID)
        {
            return new RProjectOrganizationDao().Get(ProjectOrganizationID);
        }
        public static int InsertRProjectOrganization(RProjectOrganization record)
        {
            return new RProjectOrganizationDao().Insert(record);
        }
        public static int UpdateRProjectOrganization(RProjectOrganization record)
        {
            return new RProjectOrganizationDao().Update(record);
        }
        public static int DeleteRProjectOrganization(Int32 ProjectOrganizationID)
        {
            return new RProjectOrganizationDao().Delete(ProjectOrganizationID);
        }
        public static List<RProjectOrganization> GetRProjectOrganizationList(string filterExpression)
        {
            List<RProjectOrganization> result = new List<RProjectOrganization>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RProjectOrganization));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RProjectOrganization)helper.IDataReaderToObject(reader, new RProjectOrganization()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRProjectOrganizationMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(RProjectOrganization));
                ctx.CommandText = helper.SelectMaxColumn("ProjectOrganizationID");
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
        #region RProjectOrganizationMember
        public static RProjectOrganizationMember GetRProjectOrganizationMember(Int32 ProjectOrganizationID, Int32 EmployeeID)
        {
            return new RProjectOrganizationMemberDao().Get(ProjectOrganizationID, EmployeeID);
        }
        public static int InsertRProjectOrganizationMember(RProjectOrganizationMember record)
        {
            return new RProjectOrganizationMemberDao().Insert(record);
        }
        public static int UpdateRProjectOrganizationMember(RProjectOrganizationMember record)
        {
            return new RProjectOrganizationMemberDao().Update(record);
        }
        public static int DeleteRProjectOrganizationMember(Int32 ProjectOrganizationID, Int32 EmployeeID)
        {
            return new RProjectOrganizationMemberDao().Delete(ProjectOrganizationID, EmployeeID);
        }
        public static List<RProjectOrganizationMember> GetRProjectOrganizationMemberList(string filterExpression)
        {
            List<RProjectOrganizationMember> result = new List<RProjectOrganizationMember>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RProjectOrganizationMember));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RProjectOrganizationMember)helper.IDataReaderToObject(reader, new RProjectOrganizationMember()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<RProjectOrganizationMember> GetRProjectOrganizationMemberList(string filterExpression, IDbContext ctx)
        {
            List<RProjectOrganizationMember> result = new List<RProjectOrganizationMember>();
            try
            {
                DbHelper helper = new DbHelper(typeof(RProjectOrganizationMember));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RProjectOrganizationMember)helper.IDataReaderToObject(reader, new RProjectOrganizationMember()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region RProjectTask
        public static RProjectTask GetRProjectTask(Int32 ProjectTaskID)
        {
            return new RProjectTaskDao().Get(ProjectTaskID);
        }
        public static int InsertRProjectTask(RProjectTask record)
        {
            return new RProjectTaskDao().Insert(record);
        }
        public static int UpdateRProjectTask(RProjectTask record)
        {
            return new RProjectTaskDao().Update(record);
        }
        public static int DeleteRProjectTask(Int32 ProjectTaskID)
        {
            return new RProjectTaskDao().Delete(ProjectTaskID);
        }
        public static List<RProjectTask> GetRProjectTaskList(string filterExpression)
        {
            List<RProjectTask> result = new List<RProjectTask>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RProjectTask));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RProjectTask)helper.IDataReaderToObject(reader, new RProjectTask()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<RProjectTask> GetRProjectTaskList(string filterExpression, IDbContext ctx)
        {
            List<RProjectTask> result = new List<RProjectTask>();
            try
            {
                DbHelper helper = new DbHelper(typeof(RProjectTask));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RProjectTask)helper.IDataReaderToObject(reader, new RProjectTask()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetRProjectTaskMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(RProjectTask));
                ctx.CommandText = helper.SelectMaxColumn("ProjectTaskID");
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
        #region RProjectTaskAssign
        public static RProjectTaskAssign GetRProjectTaskAssign(Int32 ProjectTaskAssignID)
        {
            return new RProjectTaskAssignDao().Get(ProjectTaskAssignID);
        }
        public static int InsertRProjectTaskAssign(RProjectTaskAssign record)
        {
            return new RProjectTaskAssignDao().Insert(record);
        }
        public static int UpdateRProjectTaskAssign(RProjectTaskAssign record)
        {
            return new RProjectTaskAssignDao().Update(record);
        }
        public static int DeleteRProjectTaskAssign(Int32 ProjectTaskAssignID)
        {
            return new RProjectTaskAssignDao().Delete(ProjectTaskAssignID);
        }
        public static List<RProjectTaskAssign> GetRProjectTaskAssignList(string filterExpression)
        {
            List<RProjectTaskAssign> result = new List<RProjectTaskAssign>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RProjectTaskAssign));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RProjectTaskAssign)helper.IDataReaderToObject(reader, new RProjectTaskAssign()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<RProjectTaskAssign> GetRProjectTaskAssignList(string filterExpression, IDbContext ctx)
        {
            List<RProjectTaskAssign> result = new List<RProjectTaskAssign>();
            try
            {
                DbHelper helper = new DbHelper(typeof(RProjectTaskAssign));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RProjectTaskAssign)helper.IDataReaderToObject(reader, new RProjectTaskAssign()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region RProjectTaskFile
        public static RProjectTaskFile GetRProjectTaskFile(Int32 ProjectTaskFileID)
        {
            return new RProjectTaskFileDao().Get(ProjectTaskFileID);
        }
        public static int InsertRProjectTaskFile(RProjectTaskFile record)
        {
            return new RProjectTaskFileDao().Insert(record);
        }
        public static int UpdateRProjectTaskFile(RProjectTaskFile record)
        {
            return new RProjectTaskFileDao().Update(record);
        }
        public static int DeleteRProjectTaskFile(Int32 ProjectTaskFileID)
        {
            return new RProjectTaskFileDao().Delete(ProjectTaskFileID);
        }
        public static List<RProjectTaskFile> GetRProjectTaskFileList(string filterExpression)
        {
            List<RProjectTaskFile> result = new List<RProjectTaskFile>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RProjectTaskFile));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RProjectTaskFile)helper.IDataReaderToObject(reader, new RProjectTaskFile()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region RProjectTaskGroup
        public static RProjectTaskGroup GetRProjectTaskGroup(Int32 ProjectTaskGroupID)
        {
            return new RProjectTaskGroupDao().Get(ProjectTaskGroupID);
        }
        public static int InsertRProjectTaskGroup(RProjectTaskGroup record)
        {
            return new RProjectTaskGroupDao().Insert(record);
        }
        public static int UpdateRProjectTaskGroup(RProjectTaskGroup record)
        {
            return new RProjectTaskGroupDao().Update(record);
        }
        public static int DeleteRProjectTaskGroup(Int32 ProjectTaskGroupID)
        {
            return new RProjectTaskGroupDao().Delete(ProjectTaskGroupID);
        }
        public static List<RProjectTaskGroup> GetRProjectTaskGroupList(string filterExpression)
        {
            List<RProjectTaskGroup> result = new List<RProjectTaskGroup>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RProjectTaskGroup));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RProjectTaskGroup)helper.IDataReaderToObject(reader, new RProjectTaskGroup()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRProjectTaskGroupMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(RProjectTaskGroup));
                ctx.CommandText = helper.SelectMaxColumn("ProjectTaskGroupID");
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
        #region RProjectTaskLog
        public static RProjectTaskLog GetRProjectTaskLog(Int32 ProjectTaskLogID)
        {
            return new RProjectTaskLogDao().Get(ProjectTaskLogID);
        }
        public static int InsertRProjectTaskLog(RProjectTaskLog record)
        {
            return new RProjectTaskLogDao().Insert(record);
        }
        public static int UpdateRProjectTaskLog(RProjectTaskLog record)
        {
            return new RProjectTaskLogDao().Update(record);
        }
        public static int DeleteRProjectTaskLog(Int32 ProjectTaskLogID)
        {
            return new RProjectTaskLogDao().Delete(ProjectTaskLogID);
        }
        public static List<RProjectTaskLog> GetRProjectTaskLogList(string filterExpression)
        {
            List<RProjectTaskLog> result = new List<RProjectTaskLog>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RProjectTaskLog));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RProjectTaskLog)helper.IDataReaderToObject(reader, new RProjectTaskLog()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Room
        public static Room GetRoom(Int32 RoomID)
        {
            return new RoomDao().Get(RoomID);
        }
        public static int InsertRoom(Room record)
        {
            return new RoomDao().Insert(record);
        }
        public static int UpdateRoom(Room record)
        {
            return new RoomDao().Update(record);
        }
        public static int DeleteRoom(Int32 RoomID)
        {
            return new RoomDao().Delete(RoomID);
        }
        public static List<Room> GetRoomList(string filterExpression)
        {
            List<Room> result = new List<Room>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Room));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Room)helper.IDataReaderToObject(reader, new Room()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRoomRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Room));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<Room> GetRoomList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<Room> result = new List<Room>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Room));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Room)helper.IDataReaderToObject(reader, new Room()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRoomRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Room));
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
        public static Int32 GetRoomMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(Room));
                ctx.CommandText = helper.SelectMaxColumn("RoomID");
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
        #region RoomSite
        public static RoomSite GetRoomSite(Int32 RoomID, String SiteID)
        {
            return new RoomSiteDao().Get(RoomID, SiteID);
        }
        public static int InsertRoomSite(RoomSite record)
        {
            return new RoomSiteDao().Insert(record);
        }
        public static int UpdateRoomSite(RoomSite record)
        {
            return new RoomSiteDao().Update(record);
        }
        public static int DeleteRoomSite(Int32 RoomID, String SiteID)
        {
            return new RoomSiteDao().Delete(RoomID, SiteID);
        }
        public static List<RoomSite> GetRoomSiteList(string filterExpression)
        {
            List<RoomSite> result = new List<RoomSite>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RoomSite));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RoomSite)helper.IDataReaderToObject(reader, new RoomSite()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<RoomSite> GetRoomSiteList(string filterExpression, IDbContext ctx)
        {
            List<RoomSite> result = new List<RoomSite>();
            try
            {
                DbHelper helper = new DbHelper(typeof(RoomSite));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RoomSite)helper.IDataReaderToObject(reader, new RoomSite()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region SalesInvoiceDt
        public static SalesInvoiceDt GetSalesInvoiceDt(Int32 TransactionDtID)
        {
            return new SalesInvoiceDtDao().Get(TransactionDtID);
        }
        public static int InsertSalesInvoiceDt(SalesInvoiceDt record)
        {
            return new SalesInvoiceDtDao().Insert(record);
        }
        public static int UpdateSalesInvoiceDt(SalesInvoiceDt record)
        {
            return new SalesInvoiceDtDao().Update(record);
        }
        public static int DeleteSalesInvoiceDt(Int32 TransactionDtID)
        {
            return new SalesInvoiceDtDao().Delete(TransactionDtID);
        }
        public static List<SalesInvoiceDt> GetSalesInvoiceDtList(string filterExpression)
        {
            List<SalesInvoiceDt> result = new List<SalesInvoiceDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SalesInvoiceDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SalesInvoiceDt)helper.IDataReaderToObject(reader, new SalesInvoiceDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<SalesInvoiceDt> GetSalesInvoiceDtList(string filterExpression, IDbContext ctx)
        {
            List<SalesInvoiceDt> result = new List<SalesInvoiceDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(SalesInvoiceDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SalesInvoiceDt)helper.IDataReaderToObject(reader, new SalesInvoiceDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SalesInvoiceHd
        public static SalesInvoiceHd GetSalesInvoiceHd(Int32 SalesInvoiceID)
        {
            return new SalesInvoiceHdDao().Get(SalesInvoiceID);
        }
        public static int InsertSalesInvoiceHd(SalesInvoiceHd record)
        {
            return new SalesInvoiceHdDao().Insert(record);
        }
        public static int UpdateSalesInvoiceHd(SalesInvoiceHd record)
        {
            return new SalesInvoiceHdDao().Update(record);
        }
        public static int DeleteSalesInvoiceHd(Int32 SalesInvoiceID)
        {
            return new SalesInvoiceHdDao().Delete(SalesInvoiceID);
        }
        public static List<SalesInvoiceHd> GetSalesInvoiceHdList(string filterExpression)
        {
            List<SalesInvoiceHd> result = new List<SalesInvoiceHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SalesInvoiceHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SalesInvoiceHd)helper.IDataReaderToObject(reader, new SalesInvoiceHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<SalesInvoiceHd> GetSalesInvoiceHdList(string filterExpression, IDbContext ctx)
        {
            List<SalesInvoiceHd> result = new List<SalesInvoiceHd>();
            try
            {
                DbHelper helper = new DbHelper(typeof(SalesInvoiceHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SalesInvoiceHd)helper.IDataReaderToObject(reader, new SalesInvoiceHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetSalesInvoiceHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(SalesInvoiceHd));
                ctx.CommandText = helper.SelectMaxColumn("SalesInvoiceID");
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
        #region Scholarship
        public static Scholarship GetScholarship(Int32 ScholarshipID)
        {
            return new ScholarshipDao().Get(ScholarshipID);
        }
        public static int InsertScholarship(Scholarship record)
        {
            return new ScholarshipDao().Insert(record);
        }
        public static int UpdateScholarship(Scholarship record)
        {
            return new ScholarshipDao().Update(record);
        }
        public static int DeleteScholarship(Int32 ScholarshipID)
        {
            return new ScholarshipDao().Delete(ScholarshipID);
        }
        public static List<Scholarship> GetScholarshipList(string filterExpression)
        {
            List<Scholarship> result = new List<Scholarship>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Scholarship));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Scholarship)helper.IDataReaderToObject(reader, new Scholarship()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetScholarshipMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(Scholarship));
                ctx.CommandText = helper.SelectMaxColumn("ScholarshipID");
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
        #region ScholarshipComp
        public static ScholarshipComp GetScholarshipComp(Int32 ScholarshipID, Int32 AdmissionFeeCompID)
        {
            return new ScholarshipCompDao().Get(ScholarshipID, AdmissionFeeCompID);
        }
        public static int InsertScholarshipComp(ScholarshipComp record)
        {
            return new ScholarshipCompDao().Insert(record);
        }
        public static int UpdateScholarshipComp(ScholarshipComp record)
        {
            return new ScholarshipCompDao().Update(record);
        }
        public static int DeleteScholarshipComp(Int32 ScholarshipID, Int32 AdmissionFeeCompID)
        {
            return new ScholarshipCompDao().Delete(ScholarshipID, AdmissionFeeCompID);
        }
        public static List<ScholarshipComp> GetScholarshipCompList(string filterExpression)
        {
            List<ScholarshipComp> result = new List<ScholarshipComp>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ScholarshipComp));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ScholarshipComp)helper.IDataReaderToObject(reader, new ScholarshipComp()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ScholarshipComp> GetScholarshipCompList(string filterExpression, IDbContext ctx)
        {
            List<ScholarshipComp> result = new List<ScholarshipComp>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ScholarshipComp));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ScholarshipComp)helper.IDataReaderToObject(reader, new ScholarshipComp()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ScholarshipPeriodAdmission
        public static ScholarshipPeriodAdmission GetScholarshipPeriodAdmission(Int32 ScholarshipID, Int32 PeriodAdmissionID)
        {
            return new ScholarshipPeriodAdmissionDao().Get(ScholarshipID, PeriodAdmissionID);
        }
        public static int InsertScholarshipPeriodAdmission(ScholarshipPeriodAdmission record)
        {
            return new ScholarshipPeriodAdmissionDao().Insert(record);
        }
        public static int UpdateScholarshipPeriodAdmission(ScholarshipPeriodAdmission record)
        {
            return new ScholarshipPeriodAdmissionDao().Update(record);
        }
        public static int DeleteScholarshipPeriodAdmission(Int32 ScholarshipID, Int32 PeriodAdmissionID)
        {
            return new ScholarshipPeriodAdmissionDao().Delete(ScholarshipID, PeriodAdmissionID);
        }
        public static List<ScholarshipPeriodAdmission> GetScholarshipPeriodAdmissionList(string filterExpression)
        {
            List<ScholarshipPeriodAdmission> result = new List<ScholarshipPeriodAdmission>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ScholarshipPeriodAdmission));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ScholarshipPeriodAdmission)helper.IDataReaderToObject(reader, new ScholarshipPeriodAdmission()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ScholarshipPeriodAdmission> GetScholarshipPeriodAdmissionList(string filterExpression, IDbContext ctx)
        {
            List<ScholarshipPeriodAdmission> result = new List<ScholarshipPeriodAdmission>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ScholarshipPeriodAdmission));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ScholarshipPeriodAdmission)helper.IDataReaderToObject(reader, new ScholarshipPeriodAdmission()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region SchoolClass
        public static SchoolClass GetSchoolClass(Int32 SchoolClassID)
        {
            return new SchoolClassDao().Get(SchoolClassID);
        }
        public static int InsertSchoolClass(SchoolClass record)
        {
            return new SchoolClassDao().Insert(record);
        }
        public static int UpdateSchoolClass(SchoolClass record)
        {
            return new SchoolClassDao().Update(record);
        }
        public static int DeleteSchoolClass(Int32 SchoolClassID)
        {
            return new SchoolClassDao().Delete(SchoolClassID);
        }
        public static List<SchoolClass> GetSchoolClassList(string filterExpression)
        {
            List<SchoolClass> result = new List<SchoolClass>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolClass));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SchoolClass)helper.IDataReaderToObject(reader, new SchoolClass()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<SchoolClass> GetSchoolClassList(string filterExpression, IDbContext ctx)
        {
            List<SchoolClass> result = new List<SchoolClass>();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolClass));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SchoolClass)helper.IDataReaderToObject(reader, new SchoolClass()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetSchoolClassRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolClass));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SchoolGrade
        public static SchoolGrade GetSchoolGrade(String GCSchoolType, String GCGrade)
        {
            return new SchoolGradeDao().Get(GCSchoolType, GCGrade);
        }
        public static int InsertSchoolGrade(SchoolGrade record)
        {
            return new SchoolGradeDao().Insert(record);
        }
        public static int UpdateSchoolGrade(SchoolGrade record)
        {
            return new SchoolGradeDao().Update(record);
        }
        public static int DeleteSchoolGrade(String GCSchoolType, String GCGrade)
        {
            return new SchoolGradeDao().Delete(GCSchoolType, GCGrade);
        }
        public static List<SchoolGrade> GetSchoolGradeList(string filterExpression)
        {
            List<SchoolGrade> result = new List<SchoolGrade>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolGrade));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SchoolGrade)helper.IDataReaderToObject(reader, new SchoolGrade()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SchoolMajor
        public static SchoolMajor GetSchoolMajor(String GCSchoolType, String GCMajor)
        {
            return new SchoolMajorDao().Get(GCSchoolType, GCMajor);
        }
        public static int InsertSchoolMajor(SchoolMajor record)
        {
            return new SchoolMajorDao().Insert(record);
        }
        public static int UpdateSchoolMajor(SchoolMajor record)
        {
            return new SchoolMajorDao().Update(record);
        }
        public static int DeleteSchoolMajor(String GCSchoolType, String GCMajor)
        {
            return new SchoolMajorDao().Delete(GCSchoolType, GCMajor);
        }
        public static List<SchoolMajor> GetSchoolMajorList(string filterExpression)
        {
            List<SchoolMajor> result = new List<SchoolMajor>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolMajor));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SchoolMajor)helper.IDataReaderToObject(reader, new SchoolMajor()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SchoolPeriod
        public static SchoolPeriod GetSchoolPeriod(Int32 SchoolPeriodID)
        {
            return new SchoolPeriodDao().Get(SchoolPeriodID);
        }
        public static int InsertSchoolPeriod(SchoolPeriod record)
        {
            return new SchoolPeriodDao().Insert(record);
        }
        public static int UpdateSchoolPeriod(SchoolPeriod record)
        {
            return new SchoolPeriodDao().Update(record);
        }
        public static int DeleteSchoolPeriod(Int32 SchoolPeriodID)
        {
            return new SchoolPeriodDao().Delete(SchoolPeriodID);
        }
        public static List<SchoolPeriod> GetSchoolPeriodList(string filterExpression)
        {
            List<SchoolPeriod> result = new List<SchoolPeriod>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolPeriod));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SchoolPeriod)helper.IDataReaderToObject(reader, new SchoolPeriod()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<SchoolPeriod> GetSchoolPeriodList(string filterExpression, IDbContext ctx)
        {
            List<SchoolPeriod> result = new List<SchoolPeriod>();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolPeriod));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SchoolPeriod)helper.IDataReaderToObject(reader, new SchoolPeriod()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetSchoolPeriodRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolPeriod));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<SchoolPeriod> GetSchoolPeriodList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<SchoolPeriod> result = new List<SchoolPeriod>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolPeriod));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SchoolPeriod)helper.IDataReaderToObject(reader, new SchoolPeriod()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSchoolPeriodRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolPeriod));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "SchoolPeriodID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSchoolPeriodMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolPeriod));
                ctx.CommandText = helper.SelectMaxColumn("SchoolPeriodID");
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
        #region SchoolSubject
        public static SchoolSubject GetSchoolSubject(String GCSchoolType, Int32 SubjectID)
        {
            return new SchoolSubjectDao().Get(GCSchoolType, SubjectID);
        }
        public static int InsertSchoolSubject(SchoolSubject record)
        {
            return new SchoolSubjectDao().Insert(record);
        }
        public static int UpdateSchoolSubject(SchoolSubject record)
        {
            return new SchoolSubjectDao().Update(record);
        }
        public static int DeleteSchoolSubject(String GCSchoolType, Int32 SubjectID)
        {
            return new SchoolSubjectDao().Delete(GCSchoolType, SubjectID);
        }
        public static List<SchoolSubject> GetSchoolSubjectList(string filterExpression)
        {
            List<SchoolSubject> result = new List<SchoolSubject>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolSubject));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SchoolSubject)helper.IDataReaderToObject(reader, new SchoolSubject()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SchoolTypeTeacherProfileGroup
        public static SchoolTypeTeacherProfileGroup GetSchoolTypeTeacherProfileGroup(Int32 GCSchoolType, Int32 TeacherProfileGroupID)
        {
            return new SchoolTypeTeacherProfileGroupDao().Get(GCSchoolType, TeacherProfileGroupID);
        }
        public static int InsertSchoolTypeTeacherProfileGroup(SchoolTypeTeacherProfileGroup record)
        {
            return new SchoolTypeTeacherProfileGroupDao().Insert(record);
        }
        public static int UpdateSchoolTypeTeacherProfileGroup(SchoolTypeTeacherProfileGroup record)
        {
            return new SchoolTypeTeacherProfileGroupDao().Update(record);
        }
        public static int DeleteSchoolTypeTeacherProfileGroup(Int32 GCSchoolType, Int32 TeacherProfileGroupID)
        {
            return new SchoolTypeTeacherProfileGroupDao().Delete(GCSchoolType, TeacherProfileGroupID);
        }
        public static List<SchoolTypeTeacherProfileGroup> GetSchoolTypeTeacherProfileGroupList(string filterExpression)
        {
            List<SchoolTypeTeacherProfileGroup> result = new List<SchoolTypeTeacherProfileGroup>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolTypeTeacherProfileGroup));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SchoolTypeTeacherProfileGroup)helper.IDataReaderToObject(reader, new SchoolTypeTeacherProfileGroup()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ServiceUnitLocation
        public static ServiceUnitLocation GetServiceUnitLocation(Int32 SiteServiceUnitID, Int32 LocationID)
        {
            return new ServiceUnitLocationDao().Get(SiteServiceUnitID, LocationID);
        }
        public static int InsertServiceUnitLocation(ServiceUnitLocation record)
        {
            return new ServiceUnitLocationDao().Insert(record);
        }
        public static int UpdateServiceUnitLocation(ServiceUnitLocation record)
        {
            return new ServiceUnitLocationDao().Update(record);
        }
        public static int DeleteServiceUnitLocation(Int32 SiteServiceUnitID, Int32 LocationID)
        {
            return new ServiceUnitLocationDao().Delete(SiteServiceUnitID, LocationID);
        }
        public static List<ServiceUnitLocation> GetServiceUnitLocationList(string filterExpression)
        {
            List<ServiceUnitLocation> result = new List<ServiceUnitLocation>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ServiceUnitLocation));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ServiceUnitLocation)helper.IDataReaderToObject(reader, new ServiceUnitLocation()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ServiceUnitLocation> GetServiceUnitLocationList(string filterExpression, IDbContext ctx)
        {
            List<ServiceUnitLocation> result = new List<ServiceUnitLocation>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ServiceUnitLocation));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ServiceUnitLocation)helper.IDataReaderToObject(reader, new ServiceUnitLocation()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region ServiceUnitMaster
        public static ServiceUnitMaster GetServiceUnitMaster(Int32 ServiceUnitID)
        {
            return new ServiceUnitMasterDao().Get(ServiceUnitID);
        }
        public static int InsertServiceUnitMaster(ServiceUnitMaster record)
        {
            return new ServiceUnitMasterDao().Insert(record);
        }
        public static int UpdateServiceUnitMaster(ServiceUnitMaster record)
        {
            return new ServiceUnitMasterDao().Update(record);
        }
        public static int DeleteServiceUnitMaster(Int32 ServiceUnitID)
        {
            return new ServiceUnitMasterDao().Delete(ServiceUnitID);
        }
        public static List<ServiceUnitMaster> GetServiceUnitMasterList(string filterExpression)
        {
            List<ServiceUnitMaster> result = new List<ServiceUnitMaster>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ServiceUnitMaster));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ServiceUnitMaster)helper.IDataReaderToObject(reader, new ServiceUnitMaster()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetServiceUnitMasterRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ServiceUnitMaster));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ServiceUnitMaster> GetServiceUnitMasterList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<ServiceUnitMaster> result = new List<ServiceUnitMaster>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ServiceUnitMaster));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ServiceUnitMaster)helper.IDataReaderToObject(reader, new ServiceUnitMaster()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetServiceUnitMasterRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ServiceUnitMaster));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ServiceUnitID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetServiceUnitMasterMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ServiceUnitMaster));
                ctx.CommandText = helper.SelectMaxColumn("ServiceUnitID");
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
        #region SiteBusinessPartner
        public static SiteBusinessPartner GetSiteBusinessPartner(Int32 SiteBusinessPartnerID)
        {
            return new SiteBusinessPartnerDao().Get(SiteBusinessPartnerID);
        }
        public static int InsertSiteBusinessPartner(SiteBusinessPartner record)
        {
            return new SiteBusinessPartnerDao().Insert(record);
        }
        public static int UpdateSiteBusinessPartner(SiteBusinessPartner record)
        {
            return new SiteBusinessPartnerDao().Update(record);
        }
        public static int DeleteSiteBusinessPartner(Int32 SiteBusinessPartnerID)
        {
            return new SiteBusinessPartnerDao().Delete(SiteBusinessPartnerID);
        }
        public static List<SiteBusinessPartner> GetSiteBusinessPartnerList(string filterExpression)
        {
            List<SiteBusinessPartner> result = new List<SiteBusinessPartner>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SiteBusinessPartner));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SiteBusinessPartner)helper.IDataReaderToObject(reader, new SiteBusinessPartner()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SiteItem
        public static SiteItem GetSiteItem(Int32 SiteItemID)
        {
            return new SiteItemDao().Get(SiteItemID);
        }
        public static int InsertSiteItem(SiteItem record)
        {
            return new SiteItemDao().Insert(record);
        }
        public static int UpdateSiteItem(SiteItem record)
        {
            return new SiteItemDao().Update(record);
        }
        public static int DeleteSiteItem(Int32 SiteItemID)
        {
            return new SiteItemDao().Delete(SiteItemID);
        }
        public static List<SiteItem> GetSiteItemList(string filterExpression)
        {
            List<SiteItem> result = new List<SiteItem>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SiteItem));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SiteItem)helper.IDataReaderToObject(reader, new SiteItem()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<SiteItem> GetSiteItemList(string filterExpression, IDbContext ctx)
        {
            List<SiteItem> result = new List<SiteItem>();
            try
            {
                DbHelper helper = new DbHelper(typeof(SiteItem));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SiteItem)helper.IDataReaderToObject(reader, new SiteItem()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region SiteItemGroup
        public static SiteItemGroup GetSiteItemGroup(Int32 SiteItemGroupID)
        {
            return new SiteItemGroupDao().Get(SiteItemGroupID);
        }
        public static int InsertSiteItemGroup(SiteItemGroup record)
        {
            return new SiteItemGroupDao().Insert(record);
        }
        public static int UpdateSiteItemGroup(SiteItemGroup record)
        {
            return new SiteItemGroupDao().Update(record);
        }
        public static int DeleteSiteItemGroup(Int32 SiteItemGroupID)
        {
            return new SiteItemGroupDao().Delete(SiteItemGroupID);
        }
        public static List<SiteItemGroup> GetSiteItemGroupList(string filterExpression)
        {
            List<SiteItemGroup> result = new List<SiteItemGroup>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SiteItemGroup));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SiteItemGroup)helper.IDataReaderToObject(reader, new SiteItemGroup()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SiteServiceUnit
        public static SiteServiceUnit GetSiteServiceUnit(Int32 SiteServiceUnitID)
        {
            return new SiteServiceUnitDao().Get(SiteServiceUnitID);
        }
        public static int InsertSiteServiceUnit(SiteServiceUnit record)
        {
            return new SiteServiceUnitDao().Insert(record);
        }
        public static int UpdateSiteServiceUnit(SiteServiceUnit record)
        {
            return new SiteServiceUnitDao().Update(record);
        }
        public static int DeleteSiteServiceUnit(Int32 SiteServiceUnitID)
        {
            return new SiteServiceUnitDao().Delete(SiteServiceUnitID);
        }
        public static List<SiteServiceUnit> GetSiteServiceUnitList(string filterExpression)
        {
            List<SiteServiceUnit> result = new List<SiteServiceUnit>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SiteServiceUnit));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SiteServiceUnit)helper.IDataReaderToObject(reader, new SiteServiceUnit()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region StockTakingDt
        public static StockTakingDt GetStockTakingDt(Int32 StockTakingID, Int32 ItemID)
        {
            return new StockTakingDtDao().Get(StockTakingID, ItemID);
        }
        public static int InsertStockTakingDt(StockTakingDt record)
        {
            return new StockTakingDtDao().Insert(record);
        }
        public static int UpdateStockTakingDt(StockTakingDt record)
        {
            return new StockTakingDtDao().Update(record);
        }
        public static int DeleteStockTakingDt(Int32 StockTakingID, Int32 ItemID)
        {
            return new StockTakingDtDao().Delete(StockTakingID, ItemID);
        }
        public static List<StockTakingDt> GetStockTakingDtList(string filterExpression, IDbContext ctx)
        {
            List<StockTakingDt> result = new List<StockTakingDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(StockTakingDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StockTakingDt)helper.IDataReaderToObject(reader, new StockTakingDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<StockTakingDt> GetStockTakingDtList(string filterExpression)
        {
            List<StockTakingDt> result = new List<StockTakingDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StockTakingDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StockTakingDt)helper.IDataReaderToObject(reader, new StockTakingDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region StockTakingDtExpired
        public static StockTakingDtExpired GetStockTakingDtExpired(Int32 StockTakingID, Int32 ItemID, String BatchNumber)
        {
            return new StockTakingDtExpiredDao().Get(StockTakingID, ItemID, BatchNumber);
        }
        public static int InsertStockTakingDtExpired(StockTakingDtExpired record)
        {
            return new StockTakingDtExpiredDao().Insert(record);
        }
        public static int UpdateStockTakingDtExpired(StockTakingDtExpired record)
        {
            return new StockTakingDtExpiredDao().Update(record);
        }
        public static int DeleteStockTakingDtExpired(Int32 StockTakingID, Int32 ItemID, String BatchNumber)
        {
            return new StockTakingDtExpiredDao().Delete(StockTakingID, ItemID, BatchNumber);
        }

        public static List<StockTakingDtExpired> GetStockTakingDtExpiredList(string filterExpression, IDbContext ctx)
        {
            List<StockTakingDtExpired> result = new List<StockTakingDtExpired>();
            try
            {
                DbHelper helper = new DbHelper(typeof(StockTakingDtExpired));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StockTakingDtExpired)helper.IDataReaderToObject(reader, new StockTakingDtExpired()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }

        public static List<StockTakingDtExpired> GetStockTakingDtExpiredList(string filterExpression)
        {
            List<StockTakingDtExpired> result = new List<StockTakingDtExpired>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StockTakingDtExpired));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StockTakingDtExpired)helper.IDataReaderToObject(reader, new StockTakingDtExpired()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region StockTakingHd
        public static StockTakingHd GetStockTakingHd(Int32 StockTakingID)
        {
            return new StockTakingHdDao().Get(StockTakingID);
        }
        public static int InsertStockTakingHd(StockTakingHd record)
        {
            return new StockTakingHdDao().Insert(record);
        }
        public static int UpdateStockTakingHd(StockTakingHd record)
        {
            return new StockTakingHdDao().Update(record);
        }
        public static int DeleteStockTakingHd(Int32 StockTakingID)
        {
            return new StockTakingHdDao().Delete(StockTakingID);
        }
        public static List<StockTakingHd> GetStockTakingHdList(string filterExpression)
        {
            List<StockTakingHd> result = new List<StockTakingHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StockTakingHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StockTakingHd)helper.IDataReaderToObject(reader, new StockTakingHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetStockTakingHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(StockTakingHd));
                ctx.CommandText = helper.SelectMaxColumn("StockTakingID");
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
        #region Student
        public static Student GetStudent(Int32 StudentID)
        {
            return new StudentDao().Get(StudentID);
        }
        public static int InsertStudent(Student record)
        {
            return new StudentDao().Insert(record);
        }
        public static int UpdateStudent(Student record)
        {
            return new StudentDao().Update(record);
        }
        public static int DeleteStudent(Int32 StudentID)
        {
            return new StudentDao().Delete(StudentID);
        }
        public static List<Student> GetStudentList(string filterExpression)
        {
            List<Student> result = new List<Student>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Student));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Student)helper.IDataReaderToObject(reader, new Student()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<Student> GetStudentList(string filterExpression, IDbContext ctx)
        {
            List<Student> result = new List<Student>();
            //IDbContext ctx = DbFactory.Configure()
            try
            {
                DbHelper helper = new DbHelper(typeof(Student));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Student)helper.IDataReaderToObject(reader, new Student()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            //finally
            //{
            //    ctx.Close();
            //}
            return result;
        }
        public static List<Student> GetStudentList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<Student> result = new List<Student>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Student));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Student)helper.IDataReaderToObject(reader, new Student()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetStudentRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Student));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetStudentRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Student));
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
        public static Int32 GetStudentMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(Student));
                ctx.CommandText = helper.SelectMaxColumn("StudentID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<Int32> GetStudentIDList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            String columnName = "StudentID";
            List<Int32> result = new List<Int32>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Student));
                ctx.CommandText = helper.SelectColumn(columnName, filterExpression, numRows, pageIndex, orderByExpression);
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
        #endregion
        #region StudentAchievement
        public static StudentAchievement GetStudentAchievement(Int32 StudentAchievementID)
        {
            return new StudentAchievementDao().Get(StudentAchievementID);
        }
        public static int InsertStudentAchievement(StudentAchievement record)
        {
            return new StudentAchievementDao().Insert(record);
        }
        public static int UpdateStudentAchievement(StudentAchievement record)
        {
            return new StudentAchievementDao().Update(record);
        }
        public static int DeleteStudentAchievement(Int32 StudentAchievementID)
        {
            return new StudentAchievementDao().Delete(StudentAchievementID);
        }
        public static List<StudentAchievement> GetStudentAchievementList(string filterExpression)
        {
            List<StudentAchievement> result = new List<StudentAchievement>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentAchievement));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentAchievement)helper.IDataReaderToObject(reader, new StudentAchievement()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region StudentAttribute
        public static StudentAttribute GetStudentAttribute(Int32 StudentAttributeID)
        {
            return new StudentAttributeDao().Get(StudentAttributeID);
        }
        public static int InsertStudentAttribute(StudentAttribute record)
        {
            return new StudentAttributeDao().Insert(record);
        }
        public static int UpdateStudentAttribute(StudentAttribute record)
        {
            return new StudentAttributeDao().Update(record);
        }
        public static int DeleteStudentAttribute(Int32 StudentAttributeID)
        {
            return new StudentAttributeDao().Delete(StudentAttributeID);
        }
        public static List<StudentAttribute> GetStudentAttributeList(string filterExpression)
        {
            List<StudentAttribute> result = new List<StudentAttribute>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentAttribute));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentAttribute)helper.IDataReaderToObject(reader, new StudentAttribute()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region StudentCoverageTransactionDt
        public static StudentCoverageTransactionDt GetStudentCoverageTransactionDt(Int32 ID)
        {
            return new StudentCoverageTransactionDtDao().Get(ID);
        }
        public static int InsertStudentCoverageTransactionDt(StudentCoverageTransactionDt record)
        {
            return new StudentCoverageTransactionDtDao().Insert(record);
        }
        public static int UpdateStudentCoverageTransactionDt(StudentCoverageTransactionDt record)
        {
            return new StudentCoverageTransactionDtDao().Update(record);
        }
        public static int DeleteStudentCoverageTransactionDt(Int32 ID)
        {
            return new StudentCoverageTransactionDtDao().Delete(ID);
        }
        public static List<StudentCoverageTransactionDt> GetStudentCoverageTransactionDtList(string filterExpression)
        {
            List<StudentCoverageTransactionDt> result = new List<StudentCoverageTransactionDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentCoverageTransactionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentCoverageTransactionDt)helper.IDataReaderToObject(reader, new StudentCoverageTransactionDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<StudentCoverageTransactionDt> GetStudentCoverageTransactionDtList(string filterExpression, IDbContext ctx)
        {
            List<StudentCoverageTransactionDt> result = new List<StudentCoverageTransactionDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentCoverageTransactionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentCoverageTransactionDt)helper.IDataReaderToObject(reader, new StudentCoverageTransactionDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region StudentCoverageTransactionHd
        public static StudentCoverageTransactionHd GetStudentCoverageTransactionHd(Int32 TransactionID)
        {
            return new StudentCoverageTransactionHdDao().Get(TransactionID);
        }
        public static int InsertStudentCoverageTransactionHd(StudentCoverageTransactionHd record)
        {
            return new StudentCoverageTransactionHdDao().Insert(record);
        }
        public static int UpdateStudentCoverageTransactionHd(StudentCoverageTransactionHd record)
        {
            return new StudentCoverageTransactionHdDao().Update(record);
        }
        public static int DeleteStudentCoverageTransactionHd(Int32 TransactionID)
        {
            return new StudentCoverageTransactionHdDao().Delete(TransactionID);
        }
        public static List<StudentCoverageTransactionHd> GetStudentCoverageTransactionHdList(string filterExpression)
        {
            List<StudentCoverageTransactionHd> result = new List<StudentCoverageTransactionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentCoverageTransactionHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentCoverageTransactionHd)helper.IDataReaderToObject(reader, new StudentCoverageTransactionHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetStudentCoverageTransactionHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentCoverageTransactionHd));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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
        #region StudentFamily
        public static StudentFamily GetStudentFamily(Int32 FamilyID)
        {
            return new StudentFamilyDao().Get(FamilyID);
        }
        public static int InsertStudentFamily(StudentFamily record)
        {
            return new StudentFamilyDao().Insert(record);
        }
        public static int UpdateStudentFamily(StudentFamily record)
        {
            return new StudentFamilyDao().Update(record);
        }
        public static int DeleteStudentFamily(Int32 FamilyID)
        {
            return new StudentFamilyDao().Delete(FamilyID);
        }
        public static List<StudentFamily> GetStudentFamilyList(string filterExpression)
        {
            List<StudentFamily> result = new List<StudentFamily>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentFamily));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentFamily)helper.IDataReaderToObject(reader, new StudentFamily()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<StudentFamily> GetStudentFamilyList(string filterExpression, IDbContext ctx)
        {
            List<StudentFamily> result = new List<StudentFamily>();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentFamily));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentFamily)helper.IDataReaderToObject(reader, new StudentFamily()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetStudentFamilyMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentFamily));
                ctx.CommandText = helper.SelectMaxColumn("FamilyID");
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
        #region StudentFee
        public static StudentFee GetStudentFee(Int32 StudentFeeID)
        {
            return new StudentFeeDao().Get(StudentFeeID);
        }
        public static int InsertStudentFee(StudentFee record)
        {
            return new StudentFeeDao().Insert(record);
        }
        public static int UpdateStudentFee(StudentFee record)
        {
            return new StudentFeeDao().Update(record);
        }
        public static int DeleteStudentFee(Int32 StudentFeeID)
        {
            return new StudentFeeDao().Delete(StudentFeeID);
        }
        public static List<StudentFee> GetStudentFeeList(string filterExpression)
        {
            List<StudentFee> result = new List<StudentFee>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentFee));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentFee)helper.IDataReaderToObject(reader, new StudentFee()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<StudentFee> GetStudentFeeList(string filterExpression, IDbContext ctx)
        {
            List<StudentFee> result = new List<StudentFee>();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentFee));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentFee)helper.IDataReaderToObject(reader, new StudentFee()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetStudentFeeMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentFee));
                ctx.CommandText = helper.SelectMaxColumn("StudentFeeID");
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
        #region StudentFeeComp
        public static StudentFeeComp GetStudentFeeComp(Int32 StudentFeeCompID)
        {
            return new StudentFeeCompDao().Get(StudentFeeCompID);
        }
        public static int InsertStudentFeeComp(StudentFeeComp record)
        {
            return new StudentFeeCompDao().Insert(record);
        }
        public static int UpdateStudentFeeComp(StudentFeeComp record)
        {
            return new StudentFeeCompDao().Update(record);
        }
        public static int DeleteStudentFeeComp(Int32 StudentFeeCompID)
        {
            return new StudentFeeCompDao().Delete(StudentFeeCompID);
        }
        public static List<StudentFeeComp> GetStudentFeeCompList(string filterExpression)
        {
            List<StudentFeeComp> result = new List<StudentFeeComp>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentFeeComp));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentFeeComp)helper.IDataReaderToObject(reader, new StudentFeeComp()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<StudentFeeComp> GetStudentFeeCompList(string filterExpression, IDbContext ctx)
        {
            List<StudentFeeComp> result = new List<StudentFeeComp>();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentFeeComp));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentFeeComp)helper.IDataReaderToObject(reader, new StudentFeeComp()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetStudentFeeCompMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentFeeComp));
                ctx.CommandText = helper.SelectMaxColumn("StudentFeeCompID");
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
        #region StudentFeeCompType
        public static StudentFeeCompType GetStudentFeeCompType(Int32 StudentFeeCompTypeID)
        {
            return new StudentFeeCompTypeDao().Get(StudentFeeCompTypeID);
        }
        public static int InsertStudentFeeCompType(StudentFeeCompType record)
        {
            return new StudentFeeCompTypeDao().Insert(record);
        }
        public static int UpdateStudentFeeCompType(StudentFeeCompType record)
        {
            return new StudentFeeCompTypeDao().Update(record);
        }
        public static int DeleteStudentFeeCompType(Int32 StudentFeeCompTypeID)
        {
            return new StudentFeeCompTypeDao().Delete(StudentFeeCompTypeID);
        }
        public static List<StudentFeeCompType> GetStudentFeeCompTypeList(string filterExpression)
        {
            List<StudentFeeCompType> result = new List<StudentFeeCompType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentFeeCompType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentFeeCompType)helper.IDataReaderToObject(reader, new StudentFeeCompType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetStudentFeeCompTypeMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentFeeCompType));
                ctx.CommandText = helper.SelectMaxColumn("StudentFeeCompTypeID");
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
        #region StudentFeeCompTypeDueDate
        public static StudentFeeCompTypeDueDate GetStudentFeeCompTypeDueDate(Int32 StudentFeeCompTypeDueDateID)
        {
            return new StudentFeeCompTypeDueDateDao().Get(StudentFeeCompTypeDueDateID);
        }
        public static int InsertStudentFeeCompTypeDueDate(StudentFeeCompTypeDueDate record)
        {
            return new StudentFeeCompTypeDueDateDao().Insert(record);
        }
        public static int UpdateStudentFeeCompTypeDueDate(StudentFeeCompTypeDueDate record)
        {
            return new StudentFeeCompTypeDueDateDao().Update(record);
        }
        public static int DeleteStudentFeeCompTypeDueDate(Int32 StudentFeeCompTypeDueDateID)
        {
            return new StudentFeeCompTypeDueDateDao().Delete(StudentFeeCompTypeDueDateID);
        }
        public static List<StudentFeeCompTypeDueDate> GetStudentFeeCompTypeDueDateList(string filterExpression)
        {
            List<StudentFeeCompTypeDueDate> result = new List<StudentFeeCompTypeDueDate>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentFeeCompTypeDueDate));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentFeeCompTypeDueDate)helper.IDataReaderToObject(reader, new StudentFeeCompTypeDueDate()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region StudentFeeCompTypePayment
        public static StudentFeeCompTypePayment GetStudentFeeCompTypePayment(Int32 StudentFeeCompTypePaymentID)
        {
            return new StudentFeeCompTypePaymentDao().Get(StudentFeeCompTypePaymentID);
        }
        public static int InsertStudentFeeCompTypePayment(StudentFeeCompTypePayment record)
        {
            return new StudentFeeCompTypePaymentDao().Insert(record);
        }
        public static int UpdateStudentFeeCompTypePayment(StudentFeeCompTypePayment record)
        {
            return new StudentFeeCompTypePaymentDao().Update(record);
        }
        public static int DeleteStudentFeeCompTypePayment(Int32 StudentFeeCompTypePaymentID)
        {
            return new StudentFeeCompTypePaymentDao().Delete(StudentFeeCompTypePaymentID);
        }
        public static List<StudentFeeCompTypePayment> GetStudentFeeCompTypePaymentList(string filterExpression)
        {
            List<StudentFeeCompTypePayment> result = new List<StudentFeeCompTypePayment>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentFeeCompTypePayment));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentFeeCompTypePayment)helper.IDataReaderToObject(reader, new StudentFeeCompTypePayment()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region StudentFeeDt
        public static StudentFeeDt GetStudentFeeDt(Int32 StudentFeeDtID)
        {
            return new StudentFeeDtDao().Get(StudentFeeDtID);
        }
        public static int InsertStudentFeeDt(StudentFeeDt record)
        {
            return new StudentFeeDtDao().Insert(record);
        }
        public static int UpdateStudentFeeDt(StudentFeeDt record)
        {
            return new StudentFeeDtDao().Update(record);
        }
        public static int DeleteStudentFeeDt(Int32 StudentFeeDtID)
        {
            return new StudentFeeDtDao().Delete(StudentFeeDtID);
        }
        public static List<StudentFeeDt> GetStudentFeeDtList(string filterExpression)
        {
            List<StudentFeeDt> result = new List<StudentFeeDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentFeeDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentFeeDt)helper.IDataReaderToObject(reader, new StudentFeeDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<StudentFeeDt> GetStudentFeeDtList(string filterExpression, IDbContext ctx)
        {
            List<StudentFeeDt> result = new List<StudentFeeDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentFeeDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentFeeDt)helper.IDataReaderToObject(reader, new StudentFeeDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region StudentMoveOut
        public static StudentMoveOut GetStudentMoveOut(Int32 StudentMoveOutID)
        {
            return new StudentMoveOutDao().Get(StudentMoveOutID);
        }
        public static int InsertStudentMoveOut(StudentMoveOut record)
        {
            return new StudentMoveOutDao().Insert(record);
        }
        public static int UpdateStudentMoveOut(StudentMoveOut record)
        {
            return new StudentMoveOutDao().Update(record);
        }
        public static int DeleteStudentMoveOut(Int32 StudentMoveOutID)
        {
            return new StudentMoveOutDao().Delete(StudentMoveOutID);
        }
        public static List<StudentMoveOut> GetStudentMoveOutList(string filterExpression)
        {
            List<StudentMoveOut> result = new List<StudentMoveOut>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentMoveOut));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentMoveOut)helper.IDataReaderToObject(reader, new StudentMoveOut()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region StudentNote
        public static StudentNote GetStudentNote(Int32 StudentNoteID)
        {
            return new StudentNoteDao().Get(StudentNoteID);
        }
        public static int InsertStudentNote(StudentNote record)
        {
            return new StudentNoteDao().Insert(record);
        }
        public static int UpdateStudentNote(StudentNote record)
        {
            return new StudentNoteDao().Update(record);
        }
        public static int DeleteStudentNote(Int32 StudentNoteID)
        {
            return new StudentNoteDao().Delete(StudentNoteID);
        }
        public static List<StudentNote> GetStudentNoteList(string filterExpression)
        {
            List<StudentNote> result = new List<StudentNote>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentNote));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentNote)helper.IDataReaderToObject(reader, new StudentNote()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<StudentNote> GetStudentNoteList(string filterExpression, IDbContext ctx)
        {
            List<StudentNote> result = new List<StudentNote>();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentNote));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentNote)helper.IDataReaderToObject(reader, new StudentNote()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetStudentNoteRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentNote));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<StudentNote> GetStudentNoteList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<StudentNote> result = new List<StudentNote>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentNote));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentNote)helper.IDataReaderToObject(reader, new StudentNote()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region StudentPastStudy
        public static StudentPastStudy GetStudentPastStudy(Int32 StudentPastStudyID)
        {
            return new StudentPastStudyDao().Get(StudentPastStudyID);
        }
        public static int InsertStudentPastStudy(StudentPastStudy record)
        {
            return new StudentPastStudyDao().Insert(record);
        }
        public static int UpdateStudentPastStudy(StudentPastStudy record)
        {
            return new StudentPastStudyDao().Update(record);
        }
        public static int DeleteStudentPastStudy(Int32 StudentPastStudyID)
        {
            return new StudentPastStudyDao().Delete(StudentPastStudyID);
        }
        public static List<StudentPastStudy> GetStudentPastStudyList(string filterExpression)
        {
            List<StudentPastStudy> result = new List<StudentPastStudy>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentPastStudy));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentPastStudy)helper.IDataReaderToObject(reader, new StudentPastStudy()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region StudentScholarshipTransactionDt
        public static StudentScholarshipTransactionDt GetStudentScholarshipTransactionDt(Int32 ID)
        {
            return new StudentScholarshipTransactionDtDao().Get(ID);
        }
        public static int InsertStudentScholarshipTransactionDt(StudentScholarshipTransactionDt record)
        {
            return new StudentScholarshipTransactionDtDao().Insert(record);
        }
        public static int UpdateStudentScholarshipTransactionDt(StudentScholarshipTransactionDt record)
        {
            return new StudentScholarshipTransactionDtDao().Update(record);
        }
        public static int DeleteStudentScholarshipTransactionDt(Int32 ID)
        {
            return new StudentScholarshipTransactionDtDao().Delete(ID);
        }
        public static List<StudentScholarshipTransactionDt> GetStudentScholarshipTransactionDtList(string filterExpression)
        {
            List<StudentScholarshipTransactionDt> result = new List<StudentScholarshipTransactionDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentScholarshipTransactionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentScholarshipTransactionDt)helper.IDataReaderToObject(reader, new StudentScholarshipTransactionDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<StudentScholarshipTransactionDt> GetStudentScholarshipTransactionDtList(string filterExpression, IDbContext ctx)
        {
            List<StudentScholarshipTransactionDt> result = new List<StudentScholarshipTransactionDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentScholarshipTransactionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentScholarshipTransactionDt)helper.IDataReaderToObject(reader, new StudentScholarshipTransactionDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region StudentScholarshipTransactionHd
        public static StudentScholarshipTransactionHd GetStudentScholarshipTransactionHd(Int32 TransactionID)
        {
            return new StudentScholarshipTransactionHdDao().Get(TransactionID);
        }
        public static int InsertStudentScholarshipTransactionHd(StudentScholarshipTransactionHd record)
        {
            return new StudentScholarshipTransactionHdDao().Insert(record);
        }
        public static int UpdateStudentScholarshipTransactionHd(StudentScholarshipTransactionHd record)
        {
            return new StudentScholarshipTransactionHdDao().Update(record);
        }
        public static int DeleteStudentScholarshipTransactionHd(Int32 TransactionID)
        {
            return new StudentScholarshipTransactionHdDao().Delete(TransactionID);
        }
        public static List<StudentScholarshipTransactionHd> GetStudentScholarshipTransactionHdList(string filterExpression)
        {
            List<StudentScholarshipTransactionHd> result = new List<StudentScholarshipTransactionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentScholarshipTransactionHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StudentScholarshipTransactionHd)helper.IDataReaderToObject(reader, new StudentScholarshipTransactionHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetStudentScholarshipTransactionHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(StudentScholarshipTransactionHd));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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
        #region Subject
        public static Subject GetSubject(Int32 SubjectID)
        {
            return new SubjectDao().Get(SubjectID);
        }
        public static int InsertSubject(Subject record)
        {
            return new SubjectDao().Insert(record);
        }
        public static int UpdateSubject(Subject record)
        {
            return new SubjectDao().Update(record);
        }
        public static int DeleteSubject(Int32 SubjectID)
        {
            return new SubjectDao().Delete(SubjectID);
        }
        public static List<Subject> GetSubjectList(string filterExpression)
        {
            List<Subject> result = new List<Subject>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Subject));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Subject)helper.IDataReaderToObject(reader, new Subject()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSubjectRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Subject));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<Subject> GetSubjectList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<Subject> result = new List<Subject>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Subject));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Subject)helper.IDataReaderToObject(reader, new Subject()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSubjectRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Subject));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "SubjectID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSubjectMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(Subject));
                ctx.CommandText = helper.SelectMaxColumn("SubjectID");
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
        #region SubjectCurriculum
        public static SubjectCurriculum GetSubjectCurriculum(Int32 SubjectCurriculumID)
        {
            return new SubjectCurriculumDao().Get(SubjectCurriculumID);
        }
        public static int InsertSubjectCurriculum(SubjectCurriculum record)
        {
            return new SubjectCurriculumDao().Insert(record);
        }
        public static int UpdateSubjectCurriculum(SubjectCurriculum record)
        {
            return new SubjectCurriculumDao().Update(record);
        }
        public static int DeleteSubjectCurriculum(Int32 SubjectCurriculumID)
        {
            return new SubjectCurriculumDao().Delete(SubjectCurriculumID);
        }
        public static List<SubjectCurriculum> GetSubjectCurriculumList(string filterExpression)
        {
            List<SubjectCurriculum> result = new List<SubjectCurriculum>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SubjectCurriculum));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SubjectCurriculum)helper.IDataReaderToObject(reader, new SubjectCurriculum()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSubjectCurriculumMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(SubjectCurriculum));
                ctx.CommandText = helper.SelectMaxColumn("SubjectCurriculumID");
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
        #region SubjectCurriculumClassType
        public static SubjectCurriculumClassType GetSubjectCurriculumClassType(Int32 SubjectCurriculumID, Int32 ClassTypeID)
        {
            return new SubjectCurriculumClassTypeDao().Get(SubjectCurriculumID, ClassTypeID);
        }
        public static int InsertSubjectCurriculumClassType(SubjectCurriculumClassType record)
        {
            return new SubjectCurriculumClassTypeDao().Insert(record);
        }
        public static int UpdateSubjectCurriculumClassType(SubjectCurriculumClassType record)
        {
            return new SubjectCurriculumClassTypeDao().Update(record);
        }
        public static int DeleteSubjectCurriculumClassType(Int32 SubjectCurriculumID, Int32 ClassTypeID)
        {
            return new SubjectCurriculumClassTypeDao().Delete(SubjectCurriculumID, ClassTypeID);
        }
        public static List<SubjectCurriculumClassType> GetSubjectCurriculumClassTypeList(string filterExpression)
        {
            List<SubjectCurriculumClassType> result = new List<SubjectCurriculumClassType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SubjectCurriculumClassType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SubjectCurriculumClassType)helper.IDataReaderToObject(reader, new SubjectCurriculumClassType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<SubjectCurriculumClassType> GetSubjectCurriculumClassTypeList(string filterExpression, IDbContext ctx)
        {
            List<SubjectCurriculumClassType> result = new List<SubjectCurriculumClassType>();
            try
            {
                DbHelper helper = new DbHelper(typeof(SubjectCurriculumClassType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SubjectCurriculumClassType)helper.IDataReaderToObject(reader, new SubjectCurriculumClassType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region SubjectCurriculumFinalMarkDescription
        public static SubjectCurriculumFinalMarkDescription GetSubjectCurriculumFinalMarkDescription(Int32 SubjectCurriculumFinalMarkDescriptionID)
        {
            return new SubjectCurriculumFinalMarkDescriptionDao().Get(SubjectCurriculumFinalMarkDescriptionID);
        }
        public static int InsertSubjectCurriculumFinalMarkDescription(SubjectCurriculumFinalMarkDescription record)
        {
            return new SubjectCurriculumFinalMarkDescriptionDao().Insert(record);
        }
        public static int UpdateSubjectCurriculumFinalMarkDescription(SubjectCurriculumFinalMarkDescription record)
        {
            return new SubjectCurriculumFinalMarkDescriptionDao().Update(record);
        }
        public static int DeleteSubjectCurriculumFinalMarkDescription(Int32 SubjectCurriculumFinalMarkDescriptionID)
        {
            return new SubjectCurriculumFinalMarkDescriptionDao().Delete(SubjectCurriculumFinalMarkDescriptionID);
        }
        public static List<SubjectCurriculumFinalMarkDescription> GetSubjectCurriculumFinalMarkDescriptionList(string filterExpression)
        {
            List<SubjectCurriculumFinalMarkDescription> result = new List<SubjectCurriculumFinalMarkDescription>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SubjectCurriculumFinalMarkDescription));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SubjectCurriculumFinalMarkDescription)helper.IDataReaderToObject(reader, new SubjectCurriculumFinalMarkDescription()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<SubjectCurriculumFinalMarkDescription> GetSubjectCurriculumFinalMarkDescriptionList(string filterExpression, IDbContext ctx)
        {
            List<SubjectCurriculumFinalMarkDescription> result = new List<SubjectCurriculumFinalMarkDescription>();
            try
            {
                DbHelper helper = new DbHelper(typeof(SubjectCurriculumFinalMarkDescription));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SubjectCurriculumFinalMarkDescription)helper.IDataReaderToObject(reader, new SubjectCurriculumFinalMarkDescription()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region SubjectCurriculumMeetingPlan
        public static SubjectCurriculumMeetingPlan GetSubjectCurriculumMeetingPlan(Int32 SubjectCurriculumMeetingPlanID)
        {
            return new SubjectCurriculumMeetingPlanDao().Get(SubjectCurriculumMeetingPlanID);
        }
        public static int InsertSubjectCurriculumMeetingPlan(SubjectCurriculumMeetingPlan record)
        {
            return new SubjectCurriculumMeetingPlanDao().Insert(record);
        }
        public static int UpdateSubjectCurriculumMeetingPlan(SubjectCurriculumMeetingPlan record)
        {
            return new SubjectCurriculumMeetingPlanDao().Update(record);
        }
        public static int DeleteSubjectCurriculumMeetingPlan(Int32 SubjectCurriculumMeetingPlanID)
        {
            return new SubjectCurriculumMeetingPlanDao().Delete(SubjectCurriculumMeetingPlanID);
        }
        public static List<SubjectCurriculumMeetingPlan> GetSubjectCurriculumMeetingPlanList(string filterExpression)
        {
            List<SubjectCurriculumMeetingPlan> result = new List<SubjectCurriculumMeetingPlan>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SubjectCurriculumMeetingPlan));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SubjectCurriculumMeetingPlan)helper.IDataReaderToObject(reader, new SubjectCurriculumMeetingPlan()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SubjectCurriculumSyllabus
        public static SubjectCurriculumSyllabus GetSubjectCurriculumSyllabus(Int32 SubjectCurriculumSyllabusID)
        {
            return new SubjectCurriculumSyllabusDao().Get(SubjectCurriculumSyllabusID);
        }
        public static int InsertSubjectCurriculumSyllabus(SubjectCurriculumSyllabus record)
        {
            return new SubjectCurriculumSyllabusDao().Insert(record);
        }
        public static int UpdateSubjectCurriculumSyllabus(SubjectCurriculumSyllabus record)
        {
            return new SubjectCurriculumSyllabusDao().Update(record);
        }
        public static int DeleteSubjectCurriculumSyllabus(Int32 SubjectCurriculumSyllabusID)
        {
            return new SubjectCurriculumSyllabusDao().Delete(SubjectCurriculumSyllabusID);
        }
        public static List<SubjectCurriculumSyllabus> GetSubjectCurriculumSyllabusList(string filterExpression)
        {
            List<SubjectCurriculumSyllabus> result = new List<SubjectCurriculumSyllabus>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SubjectCurriculumSyllabus));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SubjectCurriculumSyllabus)helper.IDataReaderToObject(reader, new SubjectCurriculumSyllabus()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SubLedgerDt
        public static SubLedgerDt GetSubLedgerDt(Int32 SubLedgerDtID)
        {
            return new SubLedgerDtDao().Get(SubLedgerDtID);
        }
        public static int InsertSubLedgerDt(SubLedgerDt record)
        {
            return new SubLedgerDtDao().Insert(record);
        }
        public static int UpdateSubLedgerDt(SubLedgerDt record)
        {
            return new SubLedgerDtDao().Update(record);
        }
        public static int DeleteSubLedgerDt(Int32 SubLedgerDtID)
        {
            return new SubLedgerDtDao().Delete(SubLedgerDtID);
        }
        public static List<SubLedgerDt> GetSubLedgerDtList(string filterExpression)
        {
            List<SubLedgerDt> result = new List<SubLedgerDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SubLedgerDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SubLedgerDt)helper.IDataReaderToObject(reader, new SubLedgerDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<SubLedgerDt> GetSubLedgerDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<SubLedgerDt> result = new List<SubLedgerDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SubLedgerDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SubLedgerDt)helper.IDataReaderToObject(reader, new SubLedgerDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSubLedgerDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SubLedgerDt));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SubLedgerHd
        public static SubLedgerHd GetSubLedgerHd(Int32 SubLedgerID)
        {
            return new SubLedgerHdDao().Get(SubLedgerID);
        }
        public static int InsertSubLedgerHd(SubLedgerHd record)
        {
            return new SubLedgerHdDao().Insert(record);
        }
        public static int UpdateSubLedgerHd(SubLedgerHd record)
        {
            return new SubLedgerHdDao().Update(record);
        }
        public static int DeleteSubLedgerHd(Int32 SubLedgerID)
        {
            return new SubLedgerHdDao().Delete(SubLedgerID);
        }
        public static List<SubLedgerHd> GetSubLedgerHdList(string filterExpression)
        {
            List<SubLedgerHd> result = new List<SubLedgerHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SubLedgerHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SubLedgerHd)helper.IDataReaderToObject(reader, new SubLedgerHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSubLedgerHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(SubLedgerHd));
                ctx.CommandText = helper.SelectMaxColumn("SubLedgerID");
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
        #region SubLedgerType
        public static SubLedgerType GetSubLedgerType(Int32 SubLedgerTypeID)
        {
            return new SubLedgerTypeDao().Get(SubLedgerTypeID);
        }
        public static int InsertSubLedgerType(SubLedgerType record)
        {
            return new SubLedgerTypeDao().Insert(record);
        }
        public static int UpdateSubLedgerType(SubLedgerType record)
        {
            return new SubLedgerTypeDao().Update(record);
        }
        public static int DeleteSubLedgerType(Int32 SubLedgerTypeID)
        {
            return new SubLedgerTypeDao().Delete(SubLedgerTypeID);
        }
        public static List<SubLedgerType> GetSubLedgerTypeList(string filterExpression)
        {
            List<SubLedgerType> result = new List<SubLedgerType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SubLedgerType));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SubLedgerType)helper.IDataReaderToObject(reader, new SubLedgerType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSubLedgerTypeRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SubLedgerType));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<SubLedgerType> GetSubLedgerTypeList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<SubLedgerType> result = new List<SubLedgerType>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SubLedgerType));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SubLedgerType)helper.IDataReaderToObject(reader, new SubLedgerType()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSubLedgerTypeRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SubLedgerType));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "SubLedgerTypeID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Supplier
        public static Supplier GetSupplier(Int32 BusinessPartnerID)
        {
            return new SupplierDao().Get(BusinessPartnerID);
        }
        public static int InsertSupplier(Supplier record)
        {
            return new SupplierDao().Insert(record);
        }
        public static int UpdateSupplier(Supplier record)
        {
            return new SupplierDao().Update(record);
        }
        public static int DeleteSupplier(Int32 BusinessPartnerID)
        {
            return new SupplierDao().Delete(BusinessPartnerID);
        }
        public static List<Supplier> GetSupplierList(string filterExpression)
        {
            List<Supplier> result = new List<Supplier>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Supplier));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Supplier)helper.IDataReaderToObject(reader, new Supplier()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SupplierCreditNote
        public static SupplierCreditNote GetSupplierCreditNote(Int32 CreditNoteID)
        {
            return new SupplierCreditNoteDao().Get(CreditNoteID);
        }
        public static int InsertSupplierCreditNote(SupplierCreditNote record)
        {
            return new SupplierCreditNoteDao().Insert(record);
        }
        public static int UpdateSupplierCreditNote(SupplierCreditNote record)
        {
            return new SupplierCreditNoteDao().Update(record);
        }
        public static int DeleteSupplierCreditNote(Int32 CreditNoteID)
        {
            return new SupplierCreditNoteDao().Delete(CreditNoteID);
        }
        public static List<SupplierCreditNote> GetSupplierCreditNoteList(string filterExpression)
        {
            List<SupplierCreditNote> result = new List<SupplierCreditNote>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SupplierCreditNote));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SupplierCreditNote)helper.IDataReaderToObject(reader, new SupplierCreditNote()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSupplierCreditNoteMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(SupplierCreditNote));
                ctx.CommandText = helper.SelectMaxColumn("CreditNoteID");
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
        #region SupplierItem
        public static SupplierItem GetSupplierItem(Int32 ID)
        {
            return new SupplierItemDao().Get(ID);
        }
        public static int InsertSupplierItem(SupplierItem record)
        {
            return new SupplierItemDao().Insert(record);
        }
        public static int UpdateSupplierItem(SupplierItem record)
        {
            return new SupplierItemDao().Update(record);
        }
        public static int DeleteSupplierItem(Int32 ID)
        {
            return new SupplierItemDao().Delete(ID);
        }
        public static List<SupplierItem> GetSupplierItemList(string filterExpression)
        {
            List<SupplierItem> result = new List<SupplierItem>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SupplierItem));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SupplierItem)helper.IDataReaderToObject(reader, new SupplierItem()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SupplierLine
        public static SupplierLine GetSupplierLine(Int32 SupplierLineID)
        {
            return new SupplierLineDao().Get(SupplierLineID);
        }
        public static int InsertSupplierLine(SupplierLine record)
        {
            return new SupplierLineDao().Insert(record);
        }
        public static int UpdateSupplierLine(SupplierLine record)
        {
            return new SupplierLineDao().Update(record);
        }
        public static int DeleteSupplierLine(Int32 SupplierLineID)
        {
            return new SupplierLineDao().Delete(SupplierLineID);
        }
        public static List<SupplierLine> GetSupplierLineList(string filterExpression)
        {
            List<SupplierLine> result = new List<SupplierLine>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SupplierLine));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SupplierLine)helper.IDataReaderToObject(reader, new SupplierLine()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<SupplierLine> GetSupplierLineList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<SupplierLine> result = new List<SupplierLine>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SupplierLine));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SupplierLine)helper.IDataReaderToObject(reader, new SupplierLine()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSupplierLineRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SupplierLine));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSupplierLineRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SupplierLine));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "SupplierLineID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSupplierLineMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(SupplierLine));
                ctx.CommandText = helper.SelectMaxColumn("SupplierLineID");
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
        #region SupplierLineDt
        public static SupplierLineDt GetSupplierLineDt(Int32 SupplierLineID, String SiteID, String GCPurchaseType)
        {
            return new SupplierLineDtDao().Get(SupplierLineID, SiteID, GCPurchaseType);
        }
        public static int InsertSupplierLineDt(SupplierLineDt record)
        {
            return new SupplierLineDtDao().Insert(record);
        }
        public static int UpdateSupplierLineDt(SupplierLineDt record)
        {
            return new SupplierLineDtDao().Update(record);
        }
        public static int DeleteSupplierLineDt(Int32 SupplierLineID, String SiteID, String GCPurchaseType)
        {
            return new SupplierLineDtDao().Delete(SupplierLineID, SiteID, GCPurchaseType);
        }
        public static List<SupplierLineDt> GetSupplierLineDtList(string filterExpression)
        {
            List<SupplierLineDt> result = new List<SupplierLineDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SupplierLineDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SupplierLineDt)helper.IDataReaderToObject(reader, new SupplierLineDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SupplierPaymentDt
        public static SupplierPaymentDt GetSupplierPaymentDt(Int32 SupplierPaymentID, Int32 PurchaseInvoiceID)
        {
            return new SupplierPaymentDtDao().Get(SupplierPaymentID, PurchaseInvoiceID);
        }
        public static int InsertSupplierPaymentDt(SupplierPaymentDt record)
        {
            return new SupplierPaymentDtDao().Insert(record);
        }
        public static int UpdateSupplierPaymentDt(SupplierPaymentDt record)
        {
            return new SupplierPaymentDtDao().Update(record);
        }
        public static int DeleteSupplierPaymentDt(Int32 SupplierPaymentID, Int32 PurchaseInvoiceID)
        {
            return new SupplierPaymentDtDao().Delete(SupplierPaymentID, PurchaseInvoiceID);
        }
        public static List<SupplierPaymentDt> GetSupplierPaymentDtList(string filterExpression)
        {
            List<SupplierPaymentDt> result = new List<SupplierPaymentDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SupplierPaymentDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SupplierPaymentDt)helper.IDataReaderToObject(reader, new SupplierPaymentDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<SupplierPaymentDt> GetSupplierPaymentDtList(string filterExpression, IDbContext ctx)
        {
            List<SupplierPaymentDt> result = new List<SupplierPaymentDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(SupplierPaymentDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SupplierPaymentDt)helper.IDataReaderToObject(reader, new SupplierPaymentDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetSupplierPaymentDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SupplierPaymentDt));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SupplierPaymentHd
        public static SupplierPaymentHd GetSupplierPaymentHd(Int32 SupplierPaymentID)
        {
            return new SupplierPaymentHdDao().Get(SupplierPaymentID);
        }
        public static int InsertSupplierPaymentHd(SupplierPaymentHd record)
        {
            return new SupplierPaymentHdDao().Insert(record);
        }
        public static int UpdateSupplierPaymentHd(SupplierPaymentHd record)
        {
            return new SupplierPaymentHdDao().Update(record);
        }
        public static int DeleteSupplierPaymentHd(Int32 SupplierPaymentID)
        {
            return new SupplierPaymentHdDao().Delete(SupplierPaymentID);
        }
        public static List<SupplierPaymentHd> GetSupplierPaymentHdList(string filterExpression)
        {
            List<SupplierPaymentHd> result = new List<SupplierPaymentHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SupplierPaymentHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SupplierPaymentHd)helper.IDataReaderToObject(reader, new SupplierPaymentHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSupplierPaymentHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(SupplierPaymentHd));
                ctx.CommandText = helper.SelectMaxColumn("SupplierPaymentID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }

        public static Int32 GetSupplierPaymentHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SupplierPaymentHd));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region TariffBookDt
        public static TariffBookDt GetTariffBookDt(Int32 BookID, Int32 ItemID, Int32 ClassID)
        {
            return new TariffBookDtDao().Get(BookID, ItemID, ClassID);
        }
        public static int InsertTariffBookDt(TariffBookDt record)
        {
            return new TariffBookDtDao().Insert(record);
        }
        public static int UpdateTariffBookDt(TariffBookDt record)
        {
            return new TariffBookDtDao().Update(record);
        }
        public static int DeleteTariffBookDt(Int32 BookID, Int32 ItemID, Int32 ClassID)
        {
            return new TariffBookDtDao().Delete(BookID, ItemID, ClassID);
        }
        public static List<TariffBookDt> GetTariffBookDtList(string filterExpression)
        {
            IDbContext ctx = DbFactory.Configure();
            return GetTariffBookDtList(filterExpression, ctx);
        }
        public static List<TariffBookDt> GetTariffBookDtList(string filterExpression, IDbContext ctx)
        {
            List<TariffBookDt> result = new List<TariffBookDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TariffBookDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TariffBookDt)helper.IDataReaderToObject(reader, new TariffBookDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region TariffBookHd
        public static TariffBookHd GetTariffBookHd(Int32 BookID)
        {
            return new TariffBookHdDao().Get(BookID);
        }
        public static int InsertTariffBookHd(TariffBookHd record)
        {
            return new TariffBookHdDao().Insert(record);
        }
        public static int UpdateTariffBookHd(TariffBookHd record)
        {
            return new TariffBookHdDao().Update(record);
        }
        public static int DeleteTariffBookHd(Int32 BookID)
        {
            return new TariffBookHdDao().Delete(BookID);
        }
        public static List<TariffBookHd> GetTariffBookHdList(string filterExpression)
        {
            IDbContext ctx = DbFactory.Configure();
            return GetTariffBookHdList(filterExpression, ctx);
        }
        public static List<TariffBookHd> GetTariffBookHdList(string filterExpression, IDbContext ctx)
        {
            List<TariffBookHd> result = new List<TariffBookHd>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TariffBookHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TariffBookHd)helper.IDataReaderToObject(reader, new TariffBookHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTariffBookHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TariffBookHd));
                ctx.CommandText = helper.SelectMaxColumn("BookID");
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
        #region Teacher
        public static Teacher GetTeacher(Int32 TeacherID)
        {
            return new TeacherDao().Get(TeacherID);
        }
        public static int InsertTeacher(Teacher record)
        {
            return new TeacherDao().Insert(record);
        }
        public static int UpdateTeacher(Teacher record)
        {
            return new TeacherDao().Update(record);
        }
        public static int DeleteTeacher(Int32 TeacherID)
        {
            return new TeacherDao().Delete(TeacherID);
        }
        public static List<Teacher> GetTeacherList(string filterExpression)
        {
            List<Teacher> result = new List<Teacher>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Teacher));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Teacher)helper.IDataReaderToObject(reader, new Teacher()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region TeacherAbsence
        public static TeacherAbsence GetTeacherAbsence(Int32 TeacherAbsenceID)
        {
            return new TeacherAbsenceDao().Get(TeacherAbsenceID);
        }
        public static int InsertTeacherAbsence(TeacherAbsence record)
        {
            return new TeacherAbsenceDao().Insert(record);
        }
        public static int UpdateTeacherAbsence(TeacherAbsence record)
        {
            return new TeacherAbsenceDao().Update(record);
        }
        public static int DeleteTeacherAbsence(Int32 TeacherAbsenceID)
        {
            return new TeacherAbsenceDao().Delete(TeacherAbsenceID);
        }
        public static List<TeacherAbsence> GetTeacherAbsenceList(string filterExpression)
        {
            List<TeacherAbsence> result = new List<TeacherAbsence>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherAbsence));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeacherAbsence)helper.IDataReaderToObject(reader, new TeacherAbsence()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTeacherAbsenceMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherAbsence));
                ctx.CommandText = helper.SelectMaxColumn("TeacherAbsenceID");
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
        #region TeacherMark
        public static TeacherMark GetTeacherMark(Int32 TeacherMarkID)
        {
            return new TeacherMarkDao().Get(TeacherMarkID);
        }
        public static int InsertTeacherMark(TeacherMark record)
        {
            return new TeacherMarkDao().Insert(record);
        }
        public static int UpdateTeacherMark(TeacherMark record)
        {
            return new TeacherMarkDao().Update(record);
        }
        public static int DeleteTeacherMark(Int32 TeacherMarkID)
        {
            return new TeacherMarkDao().Delete(TeacherMarkID);
        }
        public static List<TeacherMark> GetTeacherMarkList(string filterExpression)
        {
            List<TeacherMark> result = new List<TeacherMark>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherMark));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeacherMark)helper.IDataReaderToObject(reader, new TeacherMark()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TeacherMark> GetTeacherMarkList(string filterExpression, IDbContext ctx)
        {
            List<TeacherMark> result = new List<TeacherMark>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherMark));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeacherMark)helper.IDataReaderToObject(reader, new TeacherMark()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTeacherMarkMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherMark));
                ctx.CommandText = helper.SelectMaxColumn("TeacherMarkID");
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
        #region TeacherMarkGroup
        public static TeacherMarkGroup GetTeacherMarkGroup(Int32 TeacherMarkGroupID)
        {
            return new TeacherMarkGroupDao().Get(TeacherMarkGroupID);
        }
        public static int InsertTeacherMarkGroup(TeacherMarkGroup record)
        {
            return new TeacherMarkGroupDao().Insert(record);
        }
        public static int UpdateTeacherMarkGroup(TeacherMarkGroup record)
        {
            return new TeacherMarkGroupDao().Update(record);
        }
        public static int DeleteTeacherMarkGroup(Int32 TeacherMarkGroupID)
        {
            return new TeacherMarkGroupDao().Delete(TeacherMarkGroupID);
        }
        public static List<TeacherMarkGroup> GetTeacherMarkGroupList(string filterExpression)
        {
            List<TeacherMarkGroup> result = new List<TeacherMarkGroup>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherMarkGroup));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeacherMarkGroup)helper.IDataReaderToObject(reader, new TeacherMarkGroup()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TeacherMarkGroup> GetTeacherMarkGroupList(string filterExpression, IDbContext ctx)
        {
            List<TeacherMarkGroup> result = new List<TeacherMarkGroup>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherMarkGroup));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeacherMarkGroup)helper.IDataReaderToObject(reader, new TeacherMarkGroup()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTeacherMarkGroupMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherMarkGroup));
                ctx.CommandText = helper.SelectMaxColumn("TeacherMarkGroupID");
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
        #region TeacherMarkItem
        public static TeacherMarkItem GetTeacherMarkItem(Int32 TeacherMarkItemID)
        {
            return new TeacherMarkItemDao().Get(TeacherMarkItemID);
        }
        public static int InsertTeacherMarkItem(TeacherMarkItem record)
        {
            return new TeacherMarkItemDao().Insert(record);
        }
        public static int UpdateTeacherMarkItem(TeacherMarkItem record)
        {
            return new TeacherMarkItemDao().Update(record);
        }
        public static int DeleteTeacherMarkItem(Int32 TeacherMarkItemID)
        {
            return new TeacherMarkItemDao().Delete(TeacherMarkItemID);
        }
        public static List<TeacherMarkItem> GetTeacherMarkItemList(string filterExpression)
        {
            List<TeacherMarkItem> result = new List<TeacherMarkItem>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherMarkItem));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeacherMarkItem)helper.IDataReaderToObject(reader, new TeacherMarkItem()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TeacherMarkItem> GetTeacherMarkItemList(string filterExpression, IDbContext ctx)
        {
            List<TeacherMarkItem> result = new List<TeacherMarkItem>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherMarkItem));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeacherMarkItem)helper.IDataReaderToObject(reader, new TeacherMarkItem()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region TeacherMarkTypeDimension
        public static TeacherMarkTypeDimension GetTeacherMarkTypeDimension(Int32 TeacherMarkTypeDimensionID)
        {
            return new TeacherMarkTypeDimensionDao().Get(TeacherMarkTypeDimensionID);
        }
        public static int InsertTeacherMarkTypeDimension(TeacherMarkTypeDimension record)
        {
            return new TeacherMarkTypeDimensionDao().Insert(record);
        }
        public static int UpdateTeacherMarkTypeDimension(TeacherMarkTypeDimension record)
        {
            return new TeacherMarkTypeDimensionDao().Update(record);
        }
        public static int DeleteTeacherMarkTypeDimension(Int32 TeacherMarkTypeDimensionID)
        {
            return new TeacherMarkTypeDimensionDao().Delete(TeacherMarkTypeDimensionID);
        }
        public static List<TeacherMarkTypeDimension> GetTeacherMarkTypeDimensionList(string filterExpression)
        {
            List<TeacherMarkTypeDimension> result = new List<TeacherMarkTypeDimension>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherMarkTypeDimension));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeacherMarkTypeDimension)helper.IDataReaderToObject(reader, new TeacherMarkTypeDimension()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region TeacherMarkTypeGroup
        public static TeacherMarkTypeGroup GetTeacherMarkTypeGroup(Int32 TeacherMarkTypeGroupID)
        {
            return new TeacherMarkTypeGroupDao().Get(TeacherMarkTypeGroupID);
        }
        public static int InsertTeacherMarkTypeGroup(TeacherMarkTypeGroup record)
        {
            return new TeacherMarkTypeGroupDao().Insert(record);
        }
        public static int UpdateTeacherMarkTypeGroup(TeacherMarkTypeGroup record)
        {
            return new TeacherMarkTypeGroupDao().Update(record);
        }
        public static int DeleteTeacherMarkTypeGroup(Int32 TeacherMarkTypeGroupID)
        {
            return new TeacherMarkTypeGroupDao().Delete(TeacherMarkTypeGroupID);
        }
        public static List<TeacherMarkTypeGroup> GetTeacherMarkTypeGroupList(string filterExpression)
        {
            List<TeacherMarkTypeGroup> result = new List<TeacherMarkTypeGroup>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherMarkTypeGroup));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeacherMarkTypeGroup)helper.IDataReaderToObject(reader, new TeacherMarkTypeGroup()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TeacherMarkTypeGroup> GetTeacherMarkTypeGroupList(string filterExpression,IDbContext ctx)
        {
            List<TeacherMarkTypeGroup> result = new List<TeacherMarkTypeGroup>();
            
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherMarkTypeGroup));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeacherMarkTypeGroup)helper.IDataReaderToObject(reader, new TeacherMarkTypeGroup()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTeacherMarkTypeGroupMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherMarkTypeGroup));
                ctx.CommandText = helper.SelectMaxColumn("TeacherMarkTypeGroup");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static List<TeacherMarkTypeGroup> GetTeacherMarkTypeGroupList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<TeacherMarkTypeGroup> result = new List<TeacherMarkTypeGroup>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherMarkTypeGroup));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeacherMarkTypeGroup)helper.IDataReaderToObject(reader, new TeacherMarkTypeGroup()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        public static Int32 GetTeacherMarkTypeGroupRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherMarkTypeGroup));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTeacherMarkTypeGroupRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherMarkTypeGroup));
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
        #region TeacherMarkTypeItem
        public static TeacherMarkTypeItem GetTeacherMarkTypeItem(Int32 TeacherMarkTypeItemID)
        {
            return new TeacherMarkTypeItemDao().Get(TeacherMarkTypeItemID);
        }
        public static int InsertTeacherMarkTypeItem(TeacherMarkTypeItem record)
        {
            return new TeacherMarkTypeItemDao().Insert(record);
        }
        public static int UpdateTeacherMarkTypeItem(TeacherMarkTypeItem record)
        {
            return new TeacherMarkTypeItemDao().Update(record);
        }
        public static int DeleteTeacherMarkTypeItem(Int32 TeacherMarkTypeItemID)
        {
            return new TeacherMarkTypeItemDao().Delete(TeacherMarkTypeItemID);
        }
        public static List<TeacherMarkTypeItem> GetTeacherMarkTypeItemList(string filterExpression)
        {
            List<TeacherMarkTypeItem> result = new List<TeacherMarkTypeItem>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherMarkTypeItem));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeacherMarkTypeItem)helper.IDataReaderToObject(reader, new TeacherMarkTypeItem()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TeacherMarkTypeItem> GetTeacherMarkTypeItemList(string filterExpression, IDbContext ctx)
        {
            List<TeacherMarkTypeItem> result = new List<TeacherMarkTypeItem>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherMarkTypeItem));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeacherMarkTypeItem)helper.IDataReaderToObject(reader, new TeacherMarkTypeItem()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region TeacherProfileGroup
        public static TeacherProfileGroup GetTeacherProfileGroup(Int32 TeacherProfileGroupID)
        {
            return new TeacherProfileGroupDao().Get(TeacherProfileGroupID);
        }
        public static int InsertTeacherProfileGroup(TeacherProfileGroup record)
        {
            return new TeacherProfileGroupDao().Insert(record);
        }
        public static int UpdateTeacherProfileGroup(TeacherProfileGroup record)
        {
            return new TeacherProfileGroupDao().Update(record);
        }
        public static int DeleteTeacherProfileGroup(Int32 TeacherProfileGroupID)
        {
            return new TeacherProfileGroupDao().Delete(TeacherProfileGroupID);
        }
        public static List<TeacherProfileGroup> GetTeacherProfileGroupList(string filterExpression)
        {
            List<TeacherProfileGroup> result = new List<TeacherProfileGroup>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherProfileGroup));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeacherProfileGroup)helper.IDataReaderToObject(reader, new TeacherProfileGroup()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region TeacherProfileItem
        public static TeacherProfileItem GetTeacherProfileItem(Int32 TeacherProfileItemID)
        {
            return new TeacherProfileItemDao().Get(TeacherProfileItemID);
        }
        public static int InsertTeacherProfileItem(TeacherProfileItem record)
        {
            return new TeacherProfileItemDao().Insert(record);
        }
        public static int UpdateTeacherProfileItem(TeacherProfileItem record)
        {
            return new TeacherProfileItemDao().Update(record);
        }
        public static int DeleteTeacherProfileItem(Int32 TeacherProfileItemID)
        {
            return new TeacherProfileItemDao().Delete(TeacherProfileItemID);
        }
        public static List<TeacherProfileItem> GetTeacherProfileItemList(string filterExpression)
        {
            List<TeacherProfileItem> result = new List<TeacherProfileItem>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherProfileItem));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeacherProfileItem)helper.IDataReaderToObject(reader, new TeacherProfileItem()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TeacherProfileItem> GetTeacherProfileItemList(string filterExpression,IDbContext ctx)
        {
            List<TeacherProfileItem> result = new List<TeacherProfileItem>();
            
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherProfileItem));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeacherProfileItem)helper.IDataReaderToObject(reader, new TeacherProfileItem()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region TeacherSchedule
        public static TeacherSchedule GetTeacherSchedule(Int32 TeacherScheduleID)
        {
            return new TeacherScheduleDao().Get(TeacherScheduleID);
        }
        public static int InsertTeacherSchedule(TeacherSchedule record)
        {
            return new TeacherScheduleDao().Insert(record);
        }
        public static int UpdateTeacherSchedule(TeacherSchedule record)
        {
            return new TeacherScheduleDao().Update(record);
        }
        public static int DeleteTeacherSchedule(Int32 TeacherScheduleID)
        {
            return new TeacherScheduleDao().Delete(TeacherScheduleID);
        }
        public static List<TeacherSchedule> GetTeacherScheduleList(string filterExpression)
        {
            List<TeacherSchedule> result = new List<TeacherSchedule>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherSchedule));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeacherSchedule)helper.IDataReaderToObject(reader, new TeacherSchedule()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TeacherSchedule> GetTeacherScheduleList(string filterExpression, IDbContext ctx)
        {
            List<TeacherSchedule> result = new List<TeacherSchedule>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherSchedule));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeacherSchedule)helper.IDataReaderToObject(reader, new TeacherSchedule()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region TeacherSubject
        public static TeacherSubject GetTeacherSubject(Int32 TeacherID, Int32 SubjectID, String SiteID)
        {
            return new TeacherSubjectDao().Get(TeacherID, SubjectID, SiteID);
        }
        public static int InsertTeacherSubject(TeacherSubject record)
        {
            return new TeacherSubjectDao().Insert(record);
        }
        public static int UpdateTeacherSubject(TeacherSubject record)
        {
            return new TeacherSubjectDao().Update(record);
        }
        public static int DeleteTeacherSubject(Int32 TeacherID, Int32 SubjectID, String SiteID)
        {
            return new TeacherSubjectDao().Delete(TeacherID, SubjectID, SiteID);
        }
        public static List<TeacherSubject> GetTeacherSubjectList(string filterExpression)
        {
            List<TeacherSubject> result = new List<TeacherSubject>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherSubject));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeacherSubject)helper.IDataReaderToObject(reader, new TeacherSubject()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region TeacherSubstitution
        public static TeacherSubstitution GetTeacherSubstitution(Int32 TeacherSubstitutionID)
        {
            return new TeacherSubstitutionDao().Get(TeacherSubstitutionID);
        }
        public static int InsertTeacherSubstitution(TeacherSubstitution record)
        {
            return new TeacherSubstitutionDao().Insert(record);
        }
        public static int UpdateTeacherSubstitution(TeacherSubstitution record)
        {
            return new TeacherSubstitutionDao().Update(record);
        }
        public static int DeleteTeacherSubstitution(Int32 TeacherSubstitutionID)
        {
            return new TeacherSubstitutionDao().Delete(TeacherSubstitutionID);
        }
        public static List<TeacherSubstitution> GetTeacherSubstitutionList(string filterExpression)
        {
            List<TeacherSubstitution> result = new List<TeacherSubstitution>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherSubstitution));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeacherSubstitution)helper.IDataReaderToObject(reader, new TeacherSubstitution()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TeacherSubstitution> GetTeacherSubstitutionList(string filterExpression, IDbContext ctx)
        {
            List<TeacherSubstitution> result = new List<TeacherSubstitution>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeacherSubstitution));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeacherSubstitution)helper.IDataReaderToObject(reader, new TeacherSubstitution()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region TemplateText
        public static TemplateText GetTemplateText(Int32 TemplateID)
        {
            return new TemplateTextDao().Get(TemplateID);
        }
        public static int InsertTemplateText(TemplateText record)
        {
            return new TemplateTextDao().Insert(record);
        }
        public static int UpdateTemplateText(TemplateText record)
        {
            return new TemplateTextDao().Update(record);
        }
        public static int DeleteTemplateText(Int32 TemplateID)
        {
            return new TemplateTextDao().Delete(TemplateID);
        }
        public static List<TemplateText> GetTemplateTextList(string filterExpression)
        {
            List<TemplateText> result = new List<TemplateText>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TemplateText));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TemplateText)helper.IDataReaderToObject(reader, new TemplateText()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region TemplateEmployeeGroupDt
        public static TemplateEmployeeGroupDt GetTemplateEmployeeGroupDt(Int32 TemplateID, Int32 EmployeeID)
        {
            return new TemplateEmployeeGroupDtDao().Get(TemplateID, EmployeeID);
        }
        public static int InsertTemplateEmployeeGroupDt(TemplateEmployeeGroupDt record)
        {
            return new TemplateEmployeeGroupDtDao().Insert(record);
        }
        public static int UpdateTemplateEmployeeGroupDt(TemplateEmployeeGroupDt record)
        {
            return new TemplateEmployeeGroupDtDao().Update(record);
        }
        public static int DeleteTemplateEmployeeGroupDt(Int32 TemplateID, Int32 EmployeeID)
        {
            return new TemplateEmployeeGroupDtDao().Delete(TemplateID, EmployeeID);
        }
        public static List<TemplateEmployeeGroupDt> GetTemplateEmployeeGroupDtList(string filterExpression)
        {
            List<TemplateEmployeeGroupDt> result = new List<TemplateEmployeeGroupDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TemplateEmployeeGroupDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TemplateEmployeeGroupDt)helper.IDataReaderToObject(reader, new TemplateEmployeeGroupDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TemplateEmployeeGroupDt> GetTemplateEmployeeGroupDtList(string filterExpression, IDbContext ctx)
        {
            List<TemplateEmployeeGroupDt> result = new List<TemplateEmployeeGroupDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TemplateEmployeeGroupDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TemplateEmployeeGroupDt)helper.IDataReaderToObject(reader, new TemplateEmployeeGroupDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region TemplateEmployeeGroupHd
        public static TemplateEmployeeGroupHd GetTemplateEmployeeGroupHd(Int32 TemplateID)
        {
            return new TemplateEmployeeGroupHdDao().Get(TemplateID);
        }
        public static int InsertTemplateEmployeeGroupHd(TemplateEmployeeGroupHd record)
        {
            return new TemplateEmployeeGroupHdDao().Insert(record);
        }
        public static int UpdateTemplateEmployeeGroupHd(TemplateEmployeeGroupHd record)
        {
            return new TemplateEmployeeGroupHdDao().Update(record);
        }
        public static int DeleteTemplateEmployeeGroupHd(Int32 TemplateID)
        {
            return new TemplateEmployeeGroupHdDao().Delete(TemplateID);
        }
        public static List<TemplateEmployeeGroupHd> GetTemplateEmployeeGroupHdList(string filterExpression)
        {
            List<TemplateEmployeeGroupHd> result = new List<TemplateEmployeeGroupHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TemplateEmployeeGroupHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TemplateEmployeeGroupHd)helper.IDataReaderToObject(reader, new TemplateEmployeeGroupHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TemplateEmployeeGroupHd> GetTemplateEmployeeGroupHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<TemplateEmployeeGroupHd> result = new List<TemplateEmployeeGroupHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TemplateEmployeeGroupHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TemplateEmployeeGroupHd)helper.IDataReaderToObject(reader, new TemplateEmployeeGroupHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTemplateEmployeeGroupHdCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TemplateEmployeeGroupHd));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTemplateEmployeeGroupHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TemplateEmployeeGroupHd));
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
        #endregion
        #region Term
        public static Term GetTerm(Int32 TermID)
        {
            return new TermDao().Get(TermID);
        }
        public static int InsertTerm(Term record)
        {
            return new TermDao().Insert(record);
        }
        public static int UpdateTerm(Term record)
        {
            return new TermDao().Update(record);
        }
        public static int DeleteTerm(Int32 TermID)
        {
            return new TermDao().Delete(TermID);
        }
        public static List<Term> GetTermList(string filterExpression)
        {
            List<Term> result = new List<Term>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Term));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Term)helper.IDataReaderToObject(reader, new Term()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<Term> GetTermList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<Term> result = new List<Term>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Term));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Term)helper.IDataReaderToObject(reader, new Term()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTermRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Term));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTermRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Term));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "TermID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTermMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(Term));
                ctx.CommandText = helper.SelectMaxColumn("TermID");
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
        #region TransEmployeeLoanHd
        public static TransEmployeeLoanHd GetTransEmployeeLoanHd(Int32 TransactionID)
        {
            return new TransEmployeeLoanHdDao().Get(TransactionID);
        }
        public static int InsertTransEmployeeLoanHd(TransEmployeeLoanHd record)
        {
            return new TransEmployeeLoanHdDao().Insert(record);
        }
        public static int UpdateTransEmployeeLoanHd(TransEmployeeLoanHd record)
        {
            return new TransEmployeeLoanHdDao().Update(record);
        }
        public static int DeleteTransEmployeeLoanHd(Int32 TransactionID)
        {
            return new TransEmployeeLoanHdDao().Delete(TransactionID);
        }
        public static List<TransEmployeeLoanHd> GetTransEmployeeLoanHdList(string filterExpression)
        {
            List<TransEmployeeLoanHd> result = new List<TransEmployeeLoanHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeLoanHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeLoanHd)helper.IDataReaderToObject(reader, new TransEmployeeLoanHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TransEmployeeLoanHd> GetTransEmployeeLoanHdList(string filterExpression, IDbContext ctx)
        {
            List<TransEmployeeLoanHd> result = new List<TransEmployeeLoanHd>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeLoanHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeLoanHd)helper.IDataReaderToObject(reader, new TransEmployeeLoanHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTransEmployeeLoanHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeLoanHd));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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
        #region TransEmployeeLoanDt
        public static TransEmployeeLoanDt GetTransEmployeeLoanDt(Int32 TransactionID, Int16 PaymentIndex)
        {
            return new TransEmployeeLoanDtDao().Get(TransactionID, PaymentIndex);
        }
        public static int InsertTransEmployeeLoanDt(TransEmployeeLoanDt record)
        {
            return new TransEmployeeLoanDtDao().Insert(record);
        }
        public static int UpdateTransEmployeeLoanDt(TransEmployeeLoanDt record)
        {
            return new TransEmployeeLoanDtDao().Update(record);
        }
        public static int DeleteTransEmployeeLoanDt(Int32 TransactionID, Int16 PaymentIndex)
        {
            return new TransEmployeeLoanDtDao().Delete(TransactionID, PaymentIndex);
        }
        public static List<TransEmployeeLoanDt> GetTransEmployeeLoanDtList(string filterExpression)
        {
            List<TransEmployeeLoanDt> result = new List<TransEmployeeLoanDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeLoanDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeLoanDt)helper.IDataReaderToObject(reader, new TransEmployeeLoanDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TransEmployeeLoanDt> GetTransEmployeeLoanDtList(string filterExpression, IDbContext ctx)
        {
            List<TransEmployeeLoanDt> result = new List<TransEmployeeLoanDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeLoanDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeLoanDt)helper.IDataReaderToObject(reader, new TransEmployeeLoanDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTransEmployeeLoanDtMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeLoanDt));
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
        #region TransEmployeePositionDt
        public static TransEmployeePositionDt GetTransEmployeePositionDt(Int32 TransactionID, Int32 EmployeeID)
        {
            return new TransEmployeePositionDtDao().Get(TransactionID, EmployeeID);
        }
        public static int InsertTransEmployeePositionDt(TransEmployeePositionDt record)
        {
            return new TransEmployeePositionDtDao().Insert(record);
        }
        public static int UpdateTransEmployeePositionDt(TransEmployeePositionDt record)
        {
            return new TransEmployeePositionDtDao().Update(record);
        }
        public static int DeleteTransEmployeePositionDt(Int32 TransactionID, Int32 EmployeeID)
        {
            return new TransEmployeePositionDtDao().Delete(TransactionID, EmployeeID);
        }
        public static List<TransEmployeePositionDt> GetTransEmployeePositionDtList(string filterExpression)
        {
            List<TransEmployeePositionDt> result = new List<TransEmployeePositionDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeePositionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeePositionDt)helper.IDataReaderToObject(reader, new TransEmployeePositionDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TransEmployeePositionDt> GetTransEmployeePositionDtList(string filterExpression, IDbContext ctx)
        {
            List<TransEmployeePositionDt> result = new List<TransEmployeePositionDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeePositionDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeePositionDt)helper.IDataReaderToObject(reader, new TransEmployeePositionDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTransEmployeePositionDtMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeePositionDt));
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
        #region TransEmployeePositionHd
        public static TransEmployeePositionHd GetTransEmployeePositionHd(Int32 TransactionID)
        {
            return new TransEmployeePositionHdDao().Get(TransactionID);
        }
        public static int InsertTransEmployeePositionHd(TransEmployeePositionHd record)
        {
            return new TransEmployeePositionHdDao().Insert(record);
        }
        public static int UpdateTransEmployeePositionHd(TransEmployeePositionHd record)
        {
            return new TransEmployeePositionHdDao().Update(record);
        }
        public static int DeleteTransEmployeePositionHd(Int32 TransactionID)
        {
            return new TransEmployeePositionHdDao().Delete(TransactionID);
        }
        public static List<TransEmployeePositionHd> GetTransEmployeePositionHdList(string filterExpression)
        {
            List<TransEmployeePositionHd> result = new List<TransEmployeePositionHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeePositionHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeePositionHd)helper.IDataReaderToObject(reader, new TransEmployeePositionHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTransEmployeePositionHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeePositionHd));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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
        #region TransEmployeeFamilyStatusRenumeration
        public static TransEmployeeFamilyStatusRenumeration GetTransEmployeeFamilyStatusRenumeration(Int32 TransactionDtID)
        {
            return new TransEmployeeFamilyStatusRenumerationDao().Get(TransactionDtID);
        }
        public static int InsertTransEmployeeFamilyStatusRenumeration(TransEmployeeFamilyStatusRenumeration record)
        {
            return new TransEmployeeFamilyStatusRenumerationDao().Insert(record);
        }
        public static int UpdateTransEmployeeFamilyStatusRenumeration(TransEmployeeFamilyStatusRenumeration record)
        {
            return new TransEmployeeFamilyStatusRenumerationDao().Update(record);
        }
        public static int DeleteTransEmployeeFamilyStatusRenumeration(Int32 TransactionDtID)
        {
            return new TransEmployeeFamilyStatusRenumerationDao().Delete(TransactionDtID);
        }
        public static List<TransEmployeeFamilyStatusRenumeration> GetTransEmployeeFamilyStatusRenumerationList(string filterExpression)
        {
            List<TransEmployeeFamilyStatusRenumeration> result = new List<TransEmployeeFamilyStatusRenumeration>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeFamilyStatusRenumeration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeFamilyStatusRenumeration)helper.IDataReaderToObject(reader, new TransEmployeeFamilyStatusRenumeration()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TransEmployeeFamilyStatusRenumeration> GetTransEmployeeFamilyStatusRenumerationList(string filterExpression, IDbContext ctx)
        {
            List<TransEmployeeFamilyStatusRenumeration> result = new List<TransEmployeeFamilyStatusRenumeration>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeFamilyStatusRenumeration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeFamilyStatusRenumeration)helper.IDataReaderToObject(reader, new TransEmployeeFamilyStatusRenumeration()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTransEmployeeFamilyStatusRenumerationMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeFamilyStatusRenumeration));
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
        #region TransEmployeeFamilyStatusDt
        public static TransEmployeeFamilyStatusDt GetTransEmployeeFamilyStatusDt(Int32 TransactionID, Int32 EmployeeID)
        {
            return new TransEmployeeFamilyStatusDtDao().Get(TransactionID, EmployeeID);
        }
        public static int InsertTransEmployeeFamilyStatusDt(TransEmployeeFamilyStatusDt record)
        {
            return new TransEmployeeFamilyStatusDtDao().Insert(record);
        }
        public static int UpdateTransEmployeeFamilyStatusDt(TransEmployeeFamilyStatusDt record)
        {
            return new TransEmployeeFamilyStatusDtDao().Update(record);
        }
        public static int DeleteTransEmployeeFamilyStatusDt(Int32 TransactionID, Int32 EmployeeID)
        {
            return new TransEmployeeFamilyStatusDtDao().Delete(TransactionID, EmployeeID);
        }
        public static List<TransEmployeeFamilyStatusDt> GetTransEmployeeFamilyStatusDtList(string filterExpression)
        {
            List<TransEmployeeFamilyStatusDt> result = new List<TransEmployeeFamilyStatusDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeFamilyStatusDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeFamilyStatusDt)helper.IDataReaderToObject(reader, new TransEmployeeFamilyStatusDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TransEmployeeFamilyStatusDt> GetTransEmployeeFamilyStatusDtList(string filterExpression, IDbContext ctx)
        {
            List<TransEmployeeFamilyStatusDt> result = new List<TransEmployeeFamilyStatusDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeFamilyStatusDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeFamilyStatusDt)helper.IDataReaderToObject(reader, new TransEmployeeFamilyStatusDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTransEmployeeFamilyStatusDtMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeFamilyStatusDt));
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
        #region TransEmployeeFamilyStatusHd
        public static TransEmployeeFamilyStatusHd GetTransEmployeeFamilyStatusHd(Int32 TransactionID)
        {
            return new TransEmployeeFamilyStatusHdDao().Get(TransactionID);
        }
        public static int InsertTransEmployeeFamilyStatusHd(TransEmployeeFamilyStatusHd record)
        {
            return new TransEmployeeFamilyStatusHdDao().Insert(record);
        }
        public static int UpdateTransEmployeeFamilyStatusHd(TransEmployeeFamilyStatusHd record)
        {
            return new TransEmployeeFamilyStatusHdDao().Update(record);
        }
        public static int DeleteTransEmployeeFamilyStatusHd(Int32 TransactionID)
        {
            return new TransEmployeeFamilyStatusHdDao().Delete(TransactionID);
        }
        public static List<TransEmployeeFamilyStatusHd> GetTransEmployeeFamilyStatusHdList(string filterExpression)
        {
            List<TransEmployeeFamilyStatusHd> result = new List<TransEmployeeFamilyStatusHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeFamilyStatusHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeFamilyStatusHd)helper.IDataReaderToObject(reader, new TransEmployeeFamilyStatusHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTransEmployeeFamilyStatusHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeFamilyStatusHd));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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
        #region TransEmployeeFamilyStatusRenumerationFormula
        public static TransEmployeeFamilyStatusRenumerationFormula GetTransEmployeeFamilyStatusRenumerationFormula(Int32 TransactionDtID, String GCDayType)
        {
            return new TransEmployeeFamilyStatusRenumerationFormulaDao().Get(TransactionDtID, GCDayType);
        }
        public static int InsertTransEmployeeFamilyStatusRenumerationFormula(TransEmployeeFamilyStatusRenumerationFormula record)
        {
            return new TransEmployeeFamilyStatusRenumerationFormulaDao().Insert(record);
        }
        public static int UpdateTransEmployeeFamilyStatusRenumerationFormula(TransEmployeeFamilyStatusRenumerationFormula record)
        {
            return new TransEmployeeFamilyStatusRenumerationFormulaDao().Update(record);
        }
        public static int DeleteTransEmployeeFamilyStatusRenumerationFormula(Int32 TransactionDtID, String GCDayType)
        {
            return new TransEmployeeFamilyStatusRenumerationFormulaDao().Delete(TransactionDtID, GCDayType);
        }
        public static List<TransEmployeeFamilyStatusRenumerationFormula> GetTransEmployeeFamilyStatusRenumerationFormulaList(string filterExpression)
        {
            List<TransEmployeeFamilyStatusRenumerationFormula> result = new List<TransEmployeeFamilyStatusRenumerationFormula>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeFamilyStatusRenumerationFormula));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeFamilyStatusRenumerationFormula)helper.IDataReaderToObject(reader, new TransEmployeeFamilyStatusRenumerationFormula()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region TransFamilyStatusRenumerationDt
        public static TransFamilyStatusRenumerationDt GetTransFamilyStatusRenumerationDt(Int32 TransactionID, Int32 FamilyStatusID)
        {
            return new TransFamilyStatusRenumerationDtDao().Get(TransactionID, FamilyStatusID);
        }
        public static int InsertTransFamilyStatusRenumerationDt(TransFamilyStatusRenumerationDt record)
        {
            return new TransFamilyStatusRenumerationDtDao().Insert(record);
        }
        public static int UpdateTransFamilyStatusRenumerationDt(TransFamilyStatusRenumerationDt record)
        {
            return new TransFamilyStatusRenumerationDtDao().Update(record);
        }
        public static int DeleteTransFamilyStatusRenumerationDt(Int32 TransactionID, Int32 FamilyStatusID)
        {
            return new TransFamilyStatusRenumerationDtDao().Delete(TransactionID, FamilyStatusID);
        }
        public static List<TransFamilyStatusRenumerationDt> GetTransFamilyStatusRenumerationDtList(string filterExpression)
        {
            List<TransFamilyStatusRenumerationDt> result = new List<TransFamilyStatusRenumerationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransFamilyStatusRenumerationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransFamilyStatusRenumerationDt)helper.IDataReaderToObject(reader, new TransFamilyStatusRenumerationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TransFamilyStatusRenumerationDt> GetTransFamilyStatusRenumerationDtList(string filterExpression, IDbContext ctx)
        {
            List<TransFamilyStatusRenumerationDt> result = new List<TransFamilyStatusRenumerationDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransFamilyStatusRenumerationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransFamilyStatusRenumerationDt)helper.IDataReaderToObject(reader, new TransFamilyStatusRenumerationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTransFamilyStatusRenumerationDtMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransFamilyStatusRenumerationDt));
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
        #region TransFamilyStatusRenumerationHd
        public static TransFamilyStatusRenumerationHd GetTransFamilyStatusRenumerationHd(Int32 TransactionID)
        {
            return new TransFamilyStatusRenumerationHdDao().Get(TransactionID);
        }
        public static int InsertTransFamilyStatusRenumerationHd(TransFamilyStatusRenumerationHd record)
        {
            return new TransFamilyStatusRenumerationHdDao().Insert(record);
        }
        public static int UpdateTransFamilyStatusRenumerationHd(TransFamilyStatusRenumerationHd record)
        {
            return new TransFamilyStatusRenumerationHdDao().Update(record);
        }
        public static int DeleteTransFamilyStatusRenumerationHd(Int32 TransactionID)
        {
            return new TransFamilyStatusRenumerationHdDao().Delete(TransactionID);
        }
        public static List<TransFamilyStatusRenumerationHd> GetTransFamilyStatusRenumerationHdList(string filterExpression)
        {
            List<TransFamilyStatusRenumerationHd> result = new List<TransFamilyStatusRenumerationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransFamilyStatusRenumerationHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransFamilyStatusRenumerationHd)helper.IDataReaderToObject(reader, new TransFamilyStatusRenumerationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTransFamilyStatusRenumerationHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransFamilyStatusRenumerationHd));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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
        #region TransPositionRenumerationDt
        public static TransPositionRenumerationDt GetTransPositionRenumerationDt(Int32 TransactionID, Int32 OrganizationPositionID)
        {
            return new TransPositionRenumerationDtDao().Get(TransactionID, OrganizationPositionID);
        }
        public static int InsertTransPositionRenumerationDt(TransPositionRenumerationDt record)
        {
            return new TransPositionRenumerationDtDao().Insert(record);
        }
        public static int UpdateTransPositionRenumerationDt(TransPositionRenumerationDt record)
        {
            return new TransPositionRenumerationDtDao().Update(record);
        }
        public static int DeleteTransPositionRenumerationDt(Int32 TransactionID, Int32 OrganizationPositionID)
        {
            return new TransPositionRenumerationDtDao().Delete(TransactionID, OrganizationPositionID);
        }
        public static List<TransPositionRenumerationDt> GetTransPositionRenumerationDtList(string filterExpression)
        {
            List<TransPositionRenumerationDt> result = new List<TransPositionRenumerationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransPositionRenumerationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransPositionRenumerationDt)helper.IDataReaderToObject(reader, new TransPositionRenumerationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TransPositionRenumerationDt> GetTransPositionRenumerationDtList(string filterExpression, IDbContext ctx)
        {
            List<TransPositionRenumerationDt> result = new List<TransPositionRenumerationDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransPositionRenumerationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransPositionRenumerationDt)helper.IDataReaderToObject(reader, new TransPositionRenumerationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTransTransRenumerationDtMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransPositionRenumerationDt));
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
        #region TransPositionRenumerationHd
        public static TransPositionRenumerationHd GetTransPositionRenumerationHd(Int32 TransactionID)
        {
            return new TransPositionRenumerationHdDao().Get(TransactionID);
        }
        public static int InsertTransPositionRenumerationHd(TransPositionRenumerationHd record)
        {
            return new TransPositionRenumerationHdDao().Insert(record);
        }
        public static int UpdateTransPositionRenumerationHd(TransPositionRenumerationHd record)
        {
            return new TransPositionRenumerationHdDao().Update(record);
        }
        public static int DeleteTransPositionRenumerationHd(Int32 TransactionID)
        {
            return new TransPositionRenumerationHdDao().Delete(TransactionID);
        }
        public static List<TransPositionRenumerationHd> GetTransPositionRenumerationHdList(string filterExpression)
        {
            List<TransPositionRenumerationHd> result = new List<TransPositionRenumerationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransPositionRenumerationHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransPositionRenumerationHd)helper.IDataReaderToObject(reader, new TransPositionRenumerationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTransPositionRenumerationHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransPositionRenumerationHd));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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
        #region TransRenumerationDt
        public static TransRenumerationDt GetTransRenumerationDt(Int32 TransactionDtID)
        {
            return new TransRenumerationDtDao().Get(TransactionDtID);
        }
        public static int InsertTransRenumerationDt(TransRenumerationDt record)
        {
            return new TransRenumerationDtDao().Insert(record);
        }
        public static int UpdateTransRenumerationDt(TransRenumerationDt record)
        {
            return new TransRenumerationDtDao().Update(record);
        }
        public static int DeleteTransRenumerationDt(Int32 TransactionDtID)
        {
            return new TransRenumerationDtDao().Delete(TransactionDtID);
        }
        public static List<TransRenumerationDt> GetTransRenumerationDtList(string filterExpression)
        {
            List<TransRenumerationDt> result = new List<TransRenumerationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransRenumerationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransRenumerationDt)helper.IDataReaderToObject(reader, new TransRenumerationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TransRenumerationDt> GetTransRenumerationDtList(string filterExpression, IDbContext ctx)
        {
            List<TransRenumerationDt> result = new List<TransRenumerationDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransRenumerationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransRenumerationDt)helper.IDataReaderToObject(reader, new TransRenumerationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTransRenumerationDtMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransRenumerationDt));
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
        #region TransRenumerationHd
        public static TransRenumerationHd GetTransRenumerationHd(Int32 TransactionID)
        {
            return new TransRenumerationHdDao().Get(TransactionID);
        }
        public static int InsertTransRenumerationHd(TransRenumerationHd record)
        {
            return new TransRenumerationHdDao().Insert(record);
        }
        public static int UpdateTransRenumerationHd(TransRenumerationHd record)
        {
            return new TransRenumerationHdDao().Update(record);
        }
        public static int DeleteTransRenumerationHd(Int32 TransactionID)
        {
            return new TransRenumerationHdDao().Delete(TransactionID);
        }
        public static List<TransRenumerationHd> GetTransRenumerationHdList(string filterExpression)
        {
            List<TransRenumerationHd> result = new List<TransRenumerationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransRenumerationHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransRenumerationHd)helper.IDataReaderToObject(reader, new TransRenumerationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TransRenumerationHd> GetTransRenumerationHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<TransRenumerationHd> result = new List<TransRenumerationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransRenumerationHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransRenumerationHd)helper.IDataReaderToObject(reader, new TransRenumerationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTransRenumerationHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransRenumerationHd));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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
        #region TransEmployeePositionRenumeration
        public static TransEmployeePositionRenumeration GetTransEmployeePositionRenumeration(Int32 TransactionDtID)
        {
            return new TransEmployeePositionRenumerationDao().Get(TransactionDtID);
        }
        public static int InsertTransEmployeePositionRenumeration(TransEmployeePositionRenumeration record)
        {
            return new TransEmployeePositionRenumerationDao().Insert(record);
        }
        public static int UpdateTransEmployeePositionRenumeration(TransEmployeePositionRenumeration record)
        {
            return new TransEmployeePositionRenumerationDao().Update(record);
        }
        public static int DeleteTransEmployeePositionRenumeration(Int32 TransactionDtID)
        {
            return new TransEmployeePositionRenumerationDao().Delete(TransactionDtID);
        }
        public static List<TransEmployeePositionRenumeration> GetTransEmployeePositionRenumerationList(string filterExpression)
        {
            List<TransEmployeePositionRenumeration> result = new List<TransEmployeePositionRenumeration>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeePositionRenumeration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeePositionRenumeration)helper.IDataReaderToObject(reader, new TransEmployeePositionRenumeration()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TransEmployeePositionRenumeration> GetTransEmployeePositionRenumerationList(string filterExpression, IDbContext ctx)
        {
            List<TransEmployeePositionRenumeration> result = new List<TransEmployeePositionRenumeration>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeePositionRenumeration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeePositionRenumeration)helper.IDataReaderToObject(reader, new TransEmployeePositionRenumeration()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTransEmployeePositionRenumerationMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeePositionRenumeration));
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
        #region TransEmployeePositionRenumerationFormula
        public static TransEmployeePositionRenumerationFormula GetTransEmployeePositionRenumerationFormula(Int32 TransactionDtID, String GCDayType)
        {
            return new TransEmployeePositionRenumerationFormulaDao().Get(TransactionDtID, GCDayType);
        }
        public static int InsertTransEmployeePositionRenumerationFormula(TransEmployeePositionRenumerationFormula record)
        {
            return new TransEmployeePositionRenumerationFormulaDao().Insert(record);
        }
        public static int UpdateTransEmployeePositionRenumerationFormula(TransEmployeePositionRenumerationFormula record)
        {
            return new TransEmployeePositionRenumerationFormulaDao().Update(record);
        }
        public static int DeleteTransEmployeePositionRenumerationFormula(Int32 TransactionDtID, String GCDayType)
        {
            return new TransEmployeePositionRenumerationFormulaDao().Delete(TransactionDtID, GCDayType);
        }
        public static List<TransEmployeePositionRenumerationFormula> GetTransEmployeePositionRenumerationFormulaList(string filterExpression)
        {
            List<TransEmployeePositionRenumerationFormula> result = new List<TransEmployeePositionRenumerationFormula>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeePositionRenumerationFormula));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeePositionRenumerationFormula)helper.IDataReaderToObject(reader, new TransEmployeePositionRenumerationFormula()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region TransEmployeeJobLevelDt
        public static TransEmployeeJobLevelDt GetTransEmployeeJobLevelDt(Int32 TransactionID, Int32 EmployeeID)
        {
            return new TransEmployeeJobLevelDtDao().Get(TransactionID, EmployeeID);
        }
        public static int InsertTransEmployeeJobLevelDt(TransEmployeeJobLevelDt record)
        {
            return new TransEmployeeJobLevelDtDao().Insert(record);
        }
        public static int UpdateTransEmployeeJobLevelDt(TransEmployeeJobLevelDt record)
        {
            return new TransEmployeeJobLevelDtDao().Update(record);
        }
        public static int DeleteTransEmployeeJobLevelDt(Int32 TransactionID, Int32 EmployeeID)
        {
            return new TransEmployeeJobLevelDtDao().Delete(TransactionID, EmployeeID);
        }
        public static List<TransEmployeeJobLevelDt> GetTransEmployeeJobLevelDtList(string filterExpression)
        {
            List<TransEmployeeJobLevelDt> result = new List<TransEmployeeJobLevelDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeJobLevelDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeJobLevelDt)helper.IDataReaderToObject(reader, new TransEmployeeJobLevelDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
         
            return result;
        }
        public static List<TransEmployeeJobLevelDt> GetTransEmployeeJobLevelDtList(string filterExpression, IDbContext ctx)
        {
            List<TransEmployeeJobLevelDt> result = new List<TransEmployeeJobLevelDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeJobLevelDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeJobLevelDt)helper.IDataReaderToObject(reader, new TransEmployeeJobLevelDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTransEmployeeJobLevelDtMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeJobLevelDt));
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
        #region TransEmployeeJobLevelHd
        public static TransEmployeeJobLevelHd GetTransEmployeeJobLevelHd(Int32 TransactionID)
        {
            return new TransEmployeeJobLevelHdDao().Get(TransactionID);
        }
        public static int InsertTransEmployeeJobLevelHd(TransEmployeeJobLevelHd record)
        {
            return new TransEmployeeJobLevelHdDao().Insert(record);
        }
        public static int UpdateTransEmployeeJobLevelHd(TransEmployeeJobLevelHd record)
        {
            return new TransEmployeeJobLevelHdDao().Update(record);
        }
        public static int DeleteTransEmployeeJobLevelHd(Int32 TransactionID)
        {
            return new TransEmployeeJobLevelHdDao().Delete(TransactionID);
        }
        public static List<TransEmployeeJobLevelHd> GetTransEmployeeJobLevelHdList(string filterExpression)
        {
            List<TransEmployeeJobLevelHd> result = new List<TransEmployeeJobLevelHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeJobLevelHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeJobLevelHd)helper.IDataReaderToObject(reader, new TransEmployeeJobLevelHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTransEmployeeJobLevelHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeJobLevelHd));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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
        #region TransEmployeeJobLevelRenumeration
        public static TransEmployeeJobLevelRenumeration GetTransEmployeeJobLevelRenumeration(Int32 TransactionDtID)
        {
            return new TransEmployeeJobLevelRenumerationDao().Get(TransactionDtID);
        }
        public static int InsertTransEmployeeJobLevelRenumeration(TransEmployeeJobLevelRenumeration record)
        {
            return new TransEmployeeJobLevelRenumerationDao().Insert(record);
        }
        public static int UpdateTransEmployeeJobLevelRenumeration(TransEmployeeJobLevelRenumeration record)
        {
            return new TransEmployeeJobLevelRenumerationDao().Update(record);
        }
        public static int DeleteTransEmployeeJobLevelRenumeration(Int32 TransactionDtID)
        {
            return new TransEmployeeJobLevelRenumerationDao().Delete(TransactionDtID);
        }
        public static List<TransEmployeeJobLevelRenumeration> GetTransEmployeeJobLevelRenumerationList(string filterExpression)
        {
            List<TransEmployeeJobLevelRenumeration> result = new List<TransEmployeeJobLevelRenumeration>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeJobLevelRenumeration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeJobLevelRenumeration)helper.IDataReaderToObject(reader, new TransEmployeeJobLevelRenumeration()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TransEmployeeJobLevelRenumeration> GetTransEmployeeJobLevelRenumerationList(string filterExpression, IDbContext ctx)
        {
            List<TransEmployeeJobLevelRenumeration> result = new List<TransEmployeeJobLevelRenumeration>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeJobLevelRenumeration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeJobLevelRenumeration)helper.IDataReaderToObject(reader, new TransEmployeeJobLevelRenumeration()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTransEmployeeJobLevelRenumerationMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeJobLevelRenumeration));
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
        #region TransEmployeeJobLevelRenumerationFormula
        public static TransEmployeeJobLevelRenumerationFormula GetTransEmployeeJobLevelRenumerationFormula(Int32 TransactionDtID, String GCDayType)
        {
            return new TransEmployeeJobLevelRenumerationFormulaDao().Get(TransactionDtID, GCDayType);
        }
        public static int InsertTransEmployeeJobLevelRenumerationFormula(TransEmployeeJobLevelRenumerationFormula record)
        {
            return new TransEmployeeJobLevelRenumerationFormulaDao().Insert(record);
        }
        public static int UpdateTransEmployeeJobLevelRenumerationFormula(TransEmployeeJobLevelRenumerationFormula record)
        {
            return new TransEmployeeJobLevelRenumerationFormulaDao().Update(record);
        }
        public static int DeleteTransEmployeeJobLevelRenumerationFormula(Int32 TransactionDtID, String GCDayType)
        {
            return new TransEmployeeJobLevelRenumerationFormulaDao().Delete(TransactionDtID, GCDayType);
        }
        public static List<TransEmployeeJobLevelRenumerationFormula> GetTransEmployeeJobLevelRenumerationFormulaList(string filterExpression)
        {
            List<TransEmployeeJobLevelRenumerationFormula> result = new List<TransEmployeeJobLevelRenumerationFormula>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeJobLevelRenumerationFormula));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeJobLevelRenumerationFormula)helper.IDataReaderToObject(reader, new TransEmployeeJobLevelRenumerationFormula()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region TransEmployeeRenumerationDt
        public static TransEmployeeRenumerationDt GetTransEmployeeRenumerationDt(Int32 TransactionID, Int32 EmployeeID)
        {
            return new TransEmployeeRenumerationDtDao().Get(TransactionID, EmployeeID);
        }
        public static int InsertTransEmployeeRenumerationDt(TransEmployeeRenumerationDt record)
        {
            return new TransEmployeeRenumerationDtDao().Insert(record);
        }
        public static int UpdateTransEmployeeRenumerationDt(TransEmployeeRenumerationDt record)
        {
            return new TransEmployeeRenumerationDtDao().Update(record);
        }
        public static int DeleteTransEmployeeRenumerationDt(Int32 TransactionID, Int32 EmployeeID)
        {
            return new TransEmployeeRenumerationDtDao().Delete(TransactionID, EmployeeID);
        }
        public static List<TransEmployeeRenumerationDt> GetTransEmployeeRenumerationDtList(string filterExpression)
        {
            List<TransEmployeeRenumerationDt> result = new List<TransEmployeeRenumerationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeRenumerationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeRenumerationDt)helper.IDataReaderToObject(reader, new TransEmployeeRenumerationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TransEmployeeRenumerationDt> GetTransEmployeeRenumerationDttList(string filterExpression, IDbContext ctx)
        {
            List<TransEmployeeRenumerationDt> result = new List<TransEmployeeRenumerationDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeRenumerationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeRenumerationDt)helper.IDataReaderToObject(reader, new TransEmployeeRenumerationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTransEmployeeRenumerationDtMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeRenumerationDt));
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
        #region TransEmployeeRenumerationHd
        public static TransEmployeeRenumerationHd GetTransEmployeeRenumerationHd(Int32 TransactionID)
        {
            return new TransEmployeeRenumerationHdDao().Get(TransactionID);
        }
        public static int InsertTransEmployeeRenumerationHd(TransEmployeeRenumerationHd record)
        {
            return new TransEmployeeRenumerationHdDao().Insert(record);
        }
        public static int UpdateTransEmployeeRenumerationHd(TransEmployeeRenumerationHd record)
        {
            return new TransEmployeeRenumerationHdDao().Update(record);
        }
        public static int DeleteTransEmployeeRenumerationHd(Int32 TransactionID)
        {
            return new TransEmployeeRenumerationHdDao().Delete(TransactionID);
        }
        public static List<TransEmployeeRenumerationHd> GetTransEmployeeRenumerationHdList(string filterExpression)
        {
            List<TransEmployeeRenumerationHd> result = new List<TransEmployeeRenumerationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeRenumerationHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeRenumerationHd)helper.IDataReaderToObject(reader, new TransEmployeeRenumerationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTransEmployeeRenumerationHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeRenumerationHd));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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
        #region TransEmployeeSiteDt
        public static TransEmployeeSiteDt GetTransEmployeeSiteDt(Int32 TransactionID, Int32 EmployeeID)
        {
            return new TransEmployeeSiteDtDao().Get(TransactionID, EmployeeID);
        }
        public static int InsertTransEmployeeSiteDt(TransEmployeeSiteDt record)
        {
            return new TransEmployeeSiteDtDao().Insert(record);
        }
        public static int UpdateTransEmployeeSiteDt(TransEmployeeSiteDt record)
        {
            return new TransEmployeeSiteDtDao().Update(record);
        }
        public static int DeleteTransEmployeeSiteDt(Int32 TransactionID, Int32 EmployeeID)
        {
            return new TransEmployeeSiteDtDao().Delete(TransactionID, EmployeeID);
        }
        public static List<TransEmployeeSiteDt> GetTransEmployeeSiteDtList(string filterExpression)
        {
            List<TransEmployeeSiteDt> result = new List<TransEmployeeSiteDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeSiteDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeSiteDt)helper.IDataReaderToObject(reader, new TransEmployeeSiteDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TransEmployeeSiteDt> GetTransEmployeeSiteDtList(string filterExpression, IDbContext ctx)
        {
            List<TransEmployeeSiteDt> result = new List<TransEmployeeSiteDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeSiteDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeSiteDt)helper.IDataReaderToObject(reader, new TransEmployeeSiteDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTransEmployeeSiteDtMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeSiteDt));
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
        #region TransEmployeeSiteHd
        public static TransEmployeeSiteHd GetTransEmployeeSiteHd(Int32 TransactionID)
        {
            return new TransEmployeeSiteHdDao().Get(TransactionID);
        }
        public static int InsertTransEmployeeSiteHd(TransEmployeeSiteHd record)
        {
            return new TransEmployeeSiteHdDao().Insert(record);
        }
        public static int UpdateTransEmployeeSiteHd(TransEmployeeSiteHd record)
        {
            return new TransEmployeeSiteHdDao().Update(record);
        }
        public static int DeleteTransEmployeeSiteHd(Int32 TransactionID)
        {
            return new TransEmployeeSiteHdDao().Delete(TransactionID);
        }
        public static List<TransEmployeeSiteHd> GetTransEmployeeSiteHdList(string filterExpression)
        {
            List<TransEmployeeSiteHd> result = new List<TransEmployeeSiteHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeSiteHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeSiteHd)helper.IDataReaderToObject(reader, new TransEmployeeSiteHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTransEmployeeSiteHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeSiteHd));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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
        #region TransJobLevelRenumerationDt
        public static TransJobLevelRenumerationDt GetTransJobLevelRenumerationDt(Int32 TransactionID, Int32 JobLevelID)
        {
            return new TransJobLevelRenumerationDtDao().Get(TransactionID, JobLevelID);
        }
        public static int InsertTransJobLevelRenumerationDt(TransJobLevelRenumerationDt record)
        {
            return new TransJobLevelRenumerationDtDao().Insert(record);
        }
        public static int UpdateTransJobLevelRenumerationDt(TransJobLevelRenumerationDt record)
        {
            return new TransJobLevelRenumerationDtDao().Update(record);
        }
        public static int DeleteTransJobLevelRenumerationDt(Int32 TransactionID, Int32 JobLevelID)
        {
            return new TransJobLevelRenumerationDtDao().Delete(TransactionID, JobLevelID);
        }
        public static List<TransJobLevelRenumerationDt> GetTransJobLevelRenumerationDtList(string filterExpression)
        {
            List<TransJobLevelRenumerationDt> result = new List<TransJobLevelRenumerationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransJobLevelRenumerationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransJobLevelRenumerationDt)helper.IDataReaderToObject(reader, new TransJobLevelRenumerationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TransJobLevelRenumerationDt> GetTransJobLevelRenumerationDtList(string filterExpression, IDbContext ctx)
        {
            List<TransJobLevelRenumerationDt> result = new List<TransJobLevelRenumerationDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransJobLevelRenumerationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransJobLevelRenumerationDt)helper.IDataReaderToObject(reader, new TransJobLevelRenumerationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTransJobLevelRenumerationDtMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransJobLevelRenumerationDt));
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
        #region TransJobLevelRenumerationHd
        public static TransJobLevelRenumerationHd GetTransJobLevelRenumerationHd(Int32 TransactionID)
        {
            return new TransJobLevelRenumerationHdDao().Get(TransactionID);
        }
        public static int InsertTransJobLevelRenumerationHd(TransJobLevelRenumerationHd record)
        {
            return new TransJobLevelRenumerationHdDao().Insert(record);
        }
        public static int UpdateTransJobLevelRenumerationHd(TransJobLevelRenumerationHd record)
        {
            return new TransJobLevelRenumerationHdDao().Update(record);
        }
        public static int DeleteTransJobLevelRenumerationHd(Int32 TransactionID)
        {
            return new TransJobLevelRenumerationHdDao().Delete(TransactionID);
        }
        public static List<TransJobLevelRenumerationHd> GetTransJobLevelRenumerationHdList(string filterExpression)
        {
            List<TransJobLevelRenumerationHd> result = new List<TransJobLevelRenumerationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransJobLevelRenumerationHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransJobLevelRenumerationHd)helper.IDataReaderToObject(reader, new TransJobLevelRenumerationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTransJobLevelRenumerationHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransJobLevelRenumerationHd));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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
        #region TransJobLevelPositionRenumerationDt
        public static TransJobLevelPositionRenumerationDt GetTransJobLevelPositionRenumerationDt(Int32 TransactionID, Int32 JobLevelPositionID)
        {
            return new TransJobLevelPositionRenumerationDtDao().Get(TransactionID, JobLevelPositionID);
        }
        public static int InsertTransJobLevelPositionRenumerationDt(TransJobLevelPositionRenumerationDt record)
        {
            return new TransJobLevelPositionRenumerationDtDao().Insert(record);
        }
        public static int UpdateTransJobLevelPositionRenumerationDt(TransJobLevelPositionRenumerationDt record)
        {
            return new TransJobLevelPositionRenumerationDtDao().Update(record);
        }
        public static int DeleteTransJobLevelPositionRenumerationDt(Int32 TransactionID, Int32 JobLevelPositionID)
        {
            return new TransJobLevelPositionRenumerationDtDao().Delete(TransactionID, JobLevelPositionID);
        }
        public static List<TransJobLevelPositionRenumerationDt> GetTransJobLevelPositionRenumerationDtList(string filterExpression)
        {
            List<TransJobLevelPositionRenumerationDt> result = new List<TransJobLevelPositionRenumerationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransJobLevelPositionRenumerationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransJobLevelPositionRenumerationDt)helper.IDataReaderToObject(reader, new TransJobLevelPositionRenumerationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TransJobLevelPositionRenumerationDt> GetTransJobLevelPositionRenumerationDtList(string filterExpression, IDbContext ctx)
        {
            List<TransJobLevelPositionRenumerationDt> result = new List<TransJobLevelPositionRenumerationDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransJobLevelPositionRenumerationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransJobLevelPositionRenumerationDt)helper.IDataReaderToObject(reader, new TransJobLevelPositionRenumerationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTransJobLevelPositionRenumerationDtMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransJobLevelPositionRenumerationDt));
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
        #region TransJobLevelPositionRenumerationHd
        public static TransJobLevelPositionRenumerationHd GetTransJobLevelPositionRenumerationHd(Int32 TransactionID)
        {
            return new TransJobLevelPositionRenumerationHdDao().Get(TransactionID);
        }
        public static int InsertTransJobLevelPositionRenumerationHd(TransJobLevelPositionRenumerationHd record)
        {
            return new TransJobLevelPositionRenumerationHdDao().Insert(record);
        }
        public static int UpdateTransJobLevelPositionRenumerationHd(TransJobLevelPositionRenumerationHd record)
        {
            return new TransJobLevelPositionRenumerationHdDao().Update(record);
        }
        public static int DeleteTransJobLevelPositionRenumerationHd(Int32 TransactionID)
        {
            return new TransJobLevelPositionRenumerationHdDao().Delete(TransactionID);
        }
        public static List<TransJobLevelPositionRenumerationHd> GetTransJobLevelPositionRenumerationHdList(string filterExpression)
        {
            List<TransJobLevelPositionRenumerationHd> result = new List<TransJobLevelPositionRenumerationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransJobLevelPositionRenumerationHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransJobLevelPositionRenumerationHd)helper.IDataReaderToObject(reader, new TransJobLevelPositionRenumerationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTransJobLevelPositionRenumerationHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransJobLevelPositionRenumerationHd));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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
        #region TransRenumerationDtFormula
        public static TransRenumerationDtFormula GetTransRenumerationDtFormula(Int32 TransactionDtID, String GCDayType)
        {
            return new TransRenumerationDtFormulaDao().Get(TransactionDtID, GCDayType);
        }
        public static int InsertTransRenumerationDtFormula(TransRenumerationDtFormula record)
        {
            return new TransRenumerationDtFormulaDao().Insert(record);
        }
        public static int UpdateTransRenumerationDtFormula(TransRenumerationDtFormula record)
        {
            return new TransRenumerationDtFormulaDao().Update(record);
        }
        public static int DeleteTransRenumerationDtFormula(Int32 TransactionDtID, String GCDayType)
        {
            return new TransRenumerationDtFormulaDao().Delete(TransactionDtID, GCDayType);
        }
        public static List<TransRenumerationDtFormula> GetTransRenumerationDtFormulaList(string filterExpression)
        {
            List<TransRenumerationDtFormula> result = new List<TransRenumerationDtFormula>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransRenumerationDtFormula));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransRenumerationDtFormula)helper.IDataReaderToObject(reader, new TransRenumerationDtFormula()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region TransRenumerationCompFormulaHd
        public static TransRenumerationCompFormulaHd GetTransRenumerationCompFormulaHd(Int32 TransactionID)
        {
            return new TransRenumerationCompFormulaHdDao().Get(TransactionID);
        }
        public static int InsertTransRenumerationCompFormulaHd(TransRenumerationCompFormulaHd record)
        {
            return new TransRenumerationCompFormulaHdDao().Insert(record);
        }
        public static int UpdateTransRenumerationCompFormulaHd(TransRenumerationCompFormulaHd record)
        {
            return new TransRenumerationCompFormulaHdDao().Update(record);
        }
        public static int DeleteTransRenumerationCompFormulaHd(Int32 TransactionID)
        {
            return new TransRenumerationCompFormulaHdDao().Delete(TransactionID);
        }
        public static List<TransRenumerationCompFormulaHd> GetTransRenumerationCompFormulaHdList(string filterExpression)
        {
            List<TransRenumerationCompFormulaHd> result = new List<TransRenumerationCompFormulaHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransRenumerationCompFormulaHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransRenumerationCompFormulaHd)helper.IDataReaderToObject(reader, new TransRenumerationCompFormulaHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTransRenumerationCompFormulaHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransRenumerationCompFormulaHd));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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
        #region TransRenumerationCompFormulaDt
        public static TransRenumerationCompFormulaDt GetTransRenumerationCompFormulaDt(Int32 TransactionDtID)
        {
            return new TransRenumerationCompFormulaDtDao().Get(TransactionDtID);
        }
        public static int InsertTransRenumerationCompFormulaDt(TransRenumerationCompFormulaDt record)
        {
            return new TransRenumerationCompFormulaDtDao().Insert(record);
        }
        public static int UpdateTransRenumerationCompFormulaDt(TransRenumerationCompFormulaDt record)
        {
            return new TransRenumerationCompFormulaDtDao().Update(record);
        }
        public static int DeleteTransRenumerationCompFormulaDt(Int32 TransactionDtID)
        {
            return new TransRenumerationCompFormulaDtDao().Delete(TransactionDtID);
        }
        public static List<TransRenumerationCompFormulaDt> GetTransRenumerationCompFormulaDtList(string filterExpression)
        {
            List<TransRenumerationCompFormulaDt> result = new List<TransRenumerationCompFormulaDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransRenumerationCompFormulaDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransRenumerationCompFormulaDt)helper.IDataReaderToObject(reader, new TransRenumerationCompFormulaDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TransRenumerationCompFormulaDt> GetTransRenumerationCompFormulaDtList(string filterExpression, IDbContext ctx)
        {
            List<TransRenumerationCompFormulaDt> result = new List<TransRenumerationCompFormulaDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransRenumerationCompFormulaDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransRenumerationCompFormulaDt)helper.IDataReaderToObject(reader, new TransRenumerationCompFormulaDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTransRenumerationCompFormulaDtMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransRenumerationCompFormulaDt));
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
        #region TransRenumerationCompFormulaDtHour
        public static TransRenumerationCompFormulaDtHour GetTransRenumerationCompFormulaDtHour(Int32 TransactionDtID, Int16 FromHoursIndex, Int16 ToHoursIndex)
        {
            return new TransRenumerationCompFormulaDtHourDao().Get(TransactionDtID, FromHoursIndex, ToHoursIndex);
        }
        public static int InsertTransRenumerationCompFormulaDtHour(TransRenumerationCompFormulaDtHour record)
        {
            return new TransRenumerationCompFormulaDtHourDao().Insert(record);
        }
        public static int UpdateTransRenumerationCompFormulaDtHour(TransRenumerationCompFormulaDtHour record)
        {
            return new TransRenumerationCompFormulaDtHourDao().Update(record);
        }
        public static int DeleteTransRenumerationCompFormulaDtHour(Int32 TransactionDtID, Int16 FromHoursIndex, Int16 ToHoursIndex)
        {
            return new TransRenumerationCompFormulaDtHourDao().Delete(TransactionDtID, FromHoursIndex, ToHoursIndex);
        }
        public static List<TransRenumerationCompFormulaDtHour> GetTransRenumerationCompFormulaDtHourList(string filterExpression)
        {
            List<TransRenumerationCompFormulaDtHour> result = new List<TransRenumerationCompFormulaDtHour>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransRenumerationCompFormulaDtHour));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransRenumerationCompFormulaDtHour)helper.IDataReaderToObject(reader, new TransRenumerationCompFormulaDtHour()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TransRenumerationCompFormulaDtHour> GetTransRenumerationCompFormulaDtHourList(string filterExpression, IDbContext ctx)
        {
            List<TransRenumerationCompFormulaDtHour> result = new List<TransRenumerationCompFormulaDtHour>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransRenumerationCompFormulaDtHour));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransRenumerationCompFormulaDtHour)helper.IDataReaderToObject(reader, new TransRenumerationCompFormulaDtHour()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTransRenumerationCompFormulaDtHourMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransRenumerationCompFormulaDtHour));
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
        #region TransEmployeeRevenueDt
        public static TransEmployeeRevenueDt GetTransEmployeeRevenueDt(Int32 TransactionDtID)
        {
            return new TransEmployeeRevenueDtDao().Get(TransactionDtID);
        }
        public static int InsertTransEmployeeRevenueDt(TransEmployeeRevenueDt record)
        {
            return new TransEmployeeRevenueDtDao().Insert(record);
        }
        public static int UpdateTransEmployeeRevenueDt(TransEmployeeRevenueDt record)
        {
            return new TransEmployeeRevenueDtDao().Update(record);
        }
        public static int DeleteTransEmployeeRevenueDt(Int32 TransactionDtID)
        {
            return new TransEmployeeRevenueDtDao().Delete(TransactionDtID);
        }
        public static List<TransEmployeeRevenueDt> GetTransEmployeeRevenueDtList(string filterExpression)
        {
            List<TransEmployeeRevenueDt> result = new List<TransEmployeeRevenueDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeRevenueDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeRevenueDt)helper.IDataReaderToObject(reader, new TransEmployeeRevenueDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TransEmployeeRevenueDt> GetTransEmployeeRevenueDtList(string filterExpression, IDbContext ctx)
        {
            List<TransEmployeeRevenueDt> result = new List<TransEmployeeRevenueDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeRevenueDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeRevenueDt)helper.IDataReaderToObject(reader, new TransEmployeeRevenueDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTransEmployeeRevenueDtMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeRevenueDt));
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
        #region TransEmployeeRevenueHd
        public static TransEmployeeRevenueHd GetTransEmployeeRevenueHd(Int32 TransactionID)
        {
            return new TransEmployeeRevenueHdDao().Get(TransactionID);
        }
        public static int InsertTransEmployeeRevenueHd(TransEmployeeRevenueHd record)
        {
            return new TransEmployeeRevenueHdDao().Insert(record);
        }
        public static int UpdateTransEmployeeRevenueHd(TransEmployeeRevenueHd record)
        {
            return new TransEmployeeRevenueHdDao().Update(record);
        }
        public static int DeleteTransEmployeeRevenueHd(Int32 TransactionID)
        {
            return new TransEmployeeRevenueHdDao().Delete(TransactionID);
        }
        public static List<TransEmployeeRevenueHd> GetTransEmployeeRevenueHdList(string filterExpression)
        {
            List<TransEmployeeRevenueHd> result = new List<TransEmployeeRevenueHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeRevenueHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeRevenueHd)helper.IDataReaderToObject(reader, new TransEmployeeRevenueHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TransEmployeeRevenueHd> GetTransEmployeeRevenueHdList(string filterExpression, IDbContext ctx)
        {
            List<TransEmployeeRevenueHd> result = new List<TransEmployeeRevenueHd>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeRevenueHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransEmployeeRevenueHd)helper.IDataReaderToObject(reader, new TransEmployeeRevenueHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTransEmployeeRevenueHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransEmployeeRevenueHd));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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
        #region TransTeacherProfileDt
        public static TransTeacherProfileDt GetTransTeacherProfileDt(Int32 ID)
        {
            return new TransTeacherProfileDtDao().Get(ID);
        }
        public static int InsertTransTeacherProfileDt(TransTeacherProfileDt record)
        {
            return new TransTeacherProfileDtDao().Insert(record);
        }
        public static int UpdateTransTeacherProfileDt(TransTeacherProfileDt record)
        {
            return new TransTeacherProfileDtDao().Update(record);
        }
        public static int DeleteTransTeacherProfileDt(Int32 ID)
        {
            return new TransTeacherProfileDtDao().Delete(ID);
        }
        public static List<TransTeacherProfileDt> GetTransTeacherProfileDtList(string filterExpression)
        {
            List<TransTeacherProfileDt> result = new List<TransTeacherProfileDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransTeacherProfileDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransTeacherProfileDt)helper.IDataReaderToObject(reader, new TransTeacherProfileDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TransTeacherProfileDt> GetTransTeacherProfileDtList(string filterExpression,IDbContext ctx)
        {
            List<TransTeacherProfileDt> result = new List<TransTeacherProfileDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransTeacherProfileDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransTeacherProfileDt)helper.IDataReaderToObject(reader, new TransTeacherProfileDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTransTeacherProfileDtMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransTeacherProfileDt));
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
        #region TransTeacherProfileDtItem
        public static TransTeacherProfileDtItem GetTransTeacherProfileDtItem(Int32 TransTeacherProfileDtID, Int32 TeacherProfileItemID)
        {
            return new TransTeacherProfileDtItemDao().Get(TransTeacherProfileDtID, TeacherProfileItemID);
        }
        public static int InsertTransTeacherProfileDtItem(TransTeacherProfileDtItem record)
        {
            return new TransTeacherProfileDtItemDao().Insert(record);
        }
        public static int UpdateTransTeacherProfileDtItem(TransTeacherProfileDtItem record)
        {
            return new TransTeacherProfileDtItemDao().Update(record);
        }
        public static int DeleteTransTeacherProfileDtItem(Int32 TransTeacherProfileDtID, Int32 TeacherProfileItemID)
        {
            return new TransTeacherProfileDtItemDao().Delete(TransTeacherProfileDtID, TeacherProfileItemID);
        }
        public static List<TransTeacherProfileDtItem> GetTransTeacherProfileDtItemList(string filterExpression)
        {
            List<TransTeacherProfileDtItem> result = new List<TransTeacherProfileDtItem>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransTeacherProfileDtItem));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransTeacherProfileDtItem)helper.IDataReaderToObject(reader, new TransTeacherProfileDtItem()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region TransTeacherProfileHd
        public static TransTeacherProfileHd GetTransTeacherProfileHd(Int32 TransactionID)
        {
            return new TransTeacherProfileHdDao().Get(TransactionID);
        }
        public static int InsertTransTeacherProfileHd(TransTeacherProfileHd record)
        {
            return new TransTeacherProfileHdDao().Insert(record);
        }
        public static int UpdateTransTeacherProfileHd(TransTeacherProfileHd record)
        {
            return new TransTeacherProfileHdDao().Update(record);
        }
        public static int DeleteTransTeacherProfileHd(Int32 TransactionID)
        {
            return new TransTeacherProfileHdDao().Delete(TransactionID);
        }
        public static List<TransTeacherProfileHd> GetTransTeacherProfileHdList(string filterExpression)
        {
            List<TransTeacherProfileHd> result = new List<TransTeacherProfileHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TransTeacherProfileHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TransTeacherProfileHd)helper.IDataReaderToObject(reader, new TransTeacherProfileHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTransTeacherProfileHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TransTeacherProfileHd));
                ctx.CommandText = helper.SelectMaxColumn("TransactionID");
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

        #region Project Management
        #region ActivityHistory
        public static ActivityHistory GetActivityHistory(Int32 HistoryID)
        {
            return new ActivityHistoryDao().Get(HistoryID);
        }
        public static int InsertActivityHistory(ActivityHistory record)
        {
            return new ActivityHistoryDao().Insert(record);
        }
        public static int UpdateActivityHistory(ActivityHistory record)
        {
            return new ActivityHistoryDao().Update(record);
        }
        public static int DeleteActivityHistory(Int32 HistoryID)
        {
            return new ActivityHistoryDao().Delete(HistoryID);
        }
        public static List<ActivityHistory> GetActivityHistoryList(string filterExpression)
        {
            List<ActivityHistory> result = new List<ActivityHistory>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ActivityHistory));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ActivityHistory)helper.IDataReaderToObject(reader, new ActivityHistory()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region BudgetRealizationDt
        public static BudgetRealizationDt GetBudgetRealizationDt(Int32 BudgetRealizationDtID)
        {
            return new BudgetRealizationDtDao().Get(BudgetRealizationDtID);
        }
        public static int InsertBudgetRealizationDt(BudgetRealizationDt record)
        {
            return new BudgetRealizationDtDao().Insert(record);
        }
        public static int UpdateBudgetRealizationDt(BudgetRealizationDt record)
        {
            return new BudgetRealizationDtDao().Update(record);
        }
        public static int DeleteBudgetRealizationDt(Int32 BudgetRealizationDtID)
        {
            return new BudgetRealizationDtDao().Delete(BudgetRealizationDtID);
        }
        public static List<BudgetRealizationDt> GetBudgetRealizationDtList(string filterExpression)
        {
            List<BudgetRealizationDt> result = new List<BudgetRealizationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(BudgetRealizationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((BudgetRealizationDt)helper.IDataReaderToObject(reader, new BudgetRealizationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<BudgetRealizationDt> GetBudgetRealizationDtList(string filterExpression, IDbContext ctx)
        {
            List<BudgetRealizationDt> result = new List<BudgetRealizationDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(BudgetRealizationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((BudgetRealizationDt)helper.IDataReaderToObject(reader, new BudgetRealizationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region BudgetRealizationHd
        public static BudgetRealizationHd GetBudgetRealizationHd(Int32 BudgetRealizationID)
        {
            return new BudgetRealizationHdDao().Get(BudgetRealizationID);
        }
        public static int InsertBudgetRealizationHd(BudgetRealizationHd record)
        {
            return new BudgetRealizationHdDao().Insert(record);
        }
        public static int UpdateBudgetRealizationHd(BudgetRealizationHd record)
        {
            return new BudgetRealizationHdDao().Update(record);
        }
        public static int DeleteBudgetRealizationHd(Int32 BudgetRealizationID)
        {
            return new BudgetRealizationHdDao().Delete(BudgetRealizationID);
        }
        public static List<BudgetRealizationHd> GetBudgetRealizationHdList(string filterExpression)
        {
            List<BudgetRealizationHd> result = new List<BudgetRealizationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(BudgetRealizationHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((BudgetRealizationHd)helper.IDataReaderToObject(reader, new BudgetRealizationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetBudgetRealizationHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(BudgetRealizationHd));
                ctx.CommandText = helper.SelectMaxColumn("BudgetRealizationID");
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
        #region BudgetRequestDt
        public static BudgetRequestDt GetBudgetRequestDt(Int32 BudgetRequestDtID)
        {
            return new BudgetRequestDtDao().Get(BudgetRequestDtID);
        }
        public static int InsertBudgetRequestDt(BudgetRequestDt record)
        {
            return new BudgetRequestDtDao().Insert(record);
        }
        public static int UpdateBudgetRequestDt(BudgetRequestDt record)
        {
            return new BudgetRequestDtDao().Update(record);
        }
        public static int DeleteBudgetRequestDt(Int32 BudgetRequestDtID)
        {
            return new BudgetRequestDtDao().Delete(BudgetRequestDtID);
        }
        public static List<BudgetRequestDt> GetBudgetRequestDtList(string filterExpression)
        {
            List<BudgetRequestDt> result = new List<BudgetRequestDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(BudgetRequestDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((BudgetRequestDt)helper.IDataReaderToObject(reader, new BudgetRequestDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region BudgetRequestHd
        public static BudgetRequestHd GetBudgetRequestHd(Int32 BudgetRequestID)
        {
            return new BudgetRequestHdDao().Get(BudgetRequestID);
        }
        public static int InsertBudgetRequestHd(BudgetRequestHd record)
        {
            return new BudgetRequestHdDao().Insert(record);
        }
        public static int UpdateBudgetRequestHd(BudgetRequestHd record)
        {
            return new BudgetRequestHdDao().Update(record);
        }
        public static int DeleteBudgetRequestHd(Int32 BudgetRequestID)
        {
            return new BudgetRequestHdDao().Delete(BudgetRequestID);
        }
        public static List<BudgetRequestHd> GetBudgetRequestHdList(string filterExpression)
        {
            List<BudgetRequestHd> result = new List<BudgetRequestHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(BudgetRequestHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((BudgetRequestHd)helper.IDataReaderToObject(reader, new BudgetRequestHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetBudgetRequestHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(BudgetRequestHd));
                ctx.CommandText = helper.SelectMaxColumn("BudgetRequestID");
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
        #region MemberTask
        public static MemberTask GetMemberTask(Int32 ProjectTaskID, Int32 AssigneeID)
        {
            return new MemberTaskDao().Get(ProjectTaskID, AssigneeID);
        }
        public static int InsertMemberTask(MemberTask record)
        {
            return new MemberTaskDao().Insert(record);
        }
        public static int UpdateMemberTask(MemberTask record)
        {
            return new MemberTaskDao().Update(record);
        }
        public static int DeleteMemberTask(Int32 ProjectTaskID, Int32 AssigneeID)
        {
            return new MemberTaskDao().Delete(ProjectTaskID, AssigneeID);
        }
        public static List<MemberTask> GetMemberTaskList(string filterExpression)
        {
            List<MemberTask> result = new List<MemberTask>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MemberTask));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MemberTask)helper.IDataReaderToObject(reader, new MemberTask()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region TeamDt
        public static TeamDt GetTeamDt(Int32 TeamDtID)
        {
            return new TeamDtDao().Get(TeamDtID);
        }
        public static int InsertTeamDt(TeamDt record)
        {
            return new TeamDtDao().Insert(record);
        }
        public static int UpdateTeamDt(TeamDt record)
        {
            return new TeamDtDao().Update(record);
        }
        public static int DeleteTeamDt(Int32 TeamDtID)
        {
            return new TeamDtDao().Delete(TeamDtID);
        }
        public static List<TeamDt> GetTeamDtList(string filterExpression)
        {
            List<TeamDt> result = new List<TeamDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeamDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeamDt)helper.IDataReaderToObject(reader, new TeamDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TeamDt> GetTeamDtList(string filterExpression,IDbContext ctx)
        {
            List<TeamDt> result = new List<TeamDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeamDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeamDt)helper.IDataReaderToObject(reader, new TeamDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetTeamDtMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TeamDt));
                ctx.CommandText = helper.SelectMaxColumn("TeamDtID");
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
        #region TeamDtMember
        public static TeamDtMember GetTeamDtMember(Int32 TeamDtID, Int32 EmployeeID)
        {
            return new TeamDtMemberDao().Get(TeamDtID, EmployeeID);
        }
        public static int InsertTeamDtMember(TeamDtMember record)
        {
            return new TeamDtMemberDao().Insert(record);
        }
        public static int UpdateTeamDtMember(TeamDtMember record)
        {
            return new TeamDtMemberDao().Update(record);
        }
        public static int DeleteTeamDtMember(Int32 TeamDtID, Int32 EmployeeID)
        {
            return new TeamDtMemberDao().Delete(TeamDtID, EmployeeID);
        }
        public static List<TeamDtMember> GetTeamDtMemberList(string filterExpression)
        {
            List<TeamDtMember> result = new List<TeamDtMember>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeamDtMember));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeamDtMember)helper.IDataReaderToObject(reader, new TeamDtMember()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<TeamDtMember> GetTeamDtMemberList(string filterExpression, IDbContext ctx)
        {
            List<TeamDtMember> result = new List<TeamDtMember>();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeamDtMember));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeamDtMember)helper.IDataReaderToObject(reader, new TeamDtMember()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region TeamHd
        public static TeamHd GetTeamHd(Int32 TeamID)
        {
            return new TeamHdDao().Get(TeamID);
        }
        public static int InsertTeamHd(TeamHd record)
        {
            return new TeamHdDao().Insert(record);
        }
        public static int UpdateTeamHd(TeamHd record)
        {
            return new TeamHdDao().Update(record);
        }
        public static int DeleteTeamHd(Int32 TeamID)
        {
            return new TeamHdDao().Delete(TeamID);
        }
        public static List<TeamHd> GetTeamHdList(string filterExpression)
        {
            List<TeamHd> result = new List<TeamHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TeamHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TeamHd)helper.IDataReaderToObject(reader, new TeamHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTeamHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(TeamHd));
                ctx.CommandText = helper.SelectMaxColumn("TeamID");
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
        #region Project
        public static Project GetProject(Int32 ProjectID)
        {
            return new ProjectDao().Get(ProjectID);
        }
        public static int InsertProject(Project record)
        {
            return new ProjectDao().Insert(record);
        }
        public static int UpdateProject(Project record)
        {
            return new ProjectDao().Update(record);
        }
        public static int DeleteProject(Int32 ProjectID)
        {
            return new ProjectDao().Delete(ProjectID);
        }
        public static List<Project> GetProjectList(string filterExpression)
        {
            List<Project> result = new List<Project>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Project));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Project)helper.IDataReaderToObject(reader, new Project()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetProjectMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(Project));
                ctx.CommandText = helper.SelectMaxColumn("ProjectID");
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
        #region ProjectBudgetDt
        public static ProjectBudgetDt GetProjectBudgetDt(Int32 BudgetDtID)
        {
            return new ProjectBudgetDtDao().Get(BudgetDtID);
        }
        public static int InsertProjectBudgetDt(ProjectBudgetDt record)
        {
            return new ProjectBudgetDtDao().Insert(record);
        }
        public static int UpdateProjectBudgetDt(ProjectBudgetDt record)
        {
            return new ProjectBudgetDtDao().Update(record);
        }
        public static int DeleteProjectBudgetDt(Int32 BudgetDtID)
        {
            return new ProjectBudgetDtDao().Delete(BudgetDtID);
        }
        public static List<ProjectBudgetDt> GetProjectBudgetDtList(string filterExpression)
        {
            List<ProjectBudgetDt> result = new List<ProjectBudgetDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProjectBudgetDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProjectBudgetDt)helper.IDataReaderToObject(reader, new ProjectBudgetDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ProjectBudgetFund
        public static ProjectBudgetFund GetProjectBudgetFund(Int32 BudgetFundID)
        {
            return new ProjectBudgetFundDao().Get(BudgetFundID);
        }
        public static int InsertProjectBudgetFund(ProjectBudgetFund record)
        {
            return new ProjectBudgetFundDao().Insert(record);
        }
        public static int UpdateProjectBudgetFund(ProjectBudgetFund record)
        {
            return new ProjectBudgetFundDao().Update(record);
        }
        public static int DeleteProjectBudgetFund(Int32 BudgetFundID)
        {
            return new ProjectBudgetFundDao().Delete(BudgetFundID);
        }
        public static List<ProjectBudgetFund> GetProjectBudgetFundList(string filterExpression)
        {
            List<ProjectBudgetFund> result = new List<ProjectBudgetFund>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProjectBudgetFund));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProjectBudgetFund)helper.IDataReaderToObject(reader, new ProjectBudgetFund()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ProjectBudgetHd
        public static ProjectBudgetHd GetProjectBudgetHd(Int32 BudgetID)
        {
            return new ProjectBudgetHdDao().Get(BudgetID);
        }
        public static int InsertProjectBudgetHd(ProjectBudgetHd record)
        {
            return new ProjectBudgetHdDao().Insert(record);
        }
        public static int UpdateProjectBudgetHd(ProjectBudgetHd record)
        {
            return new ProjectBudgetHdDao().Update(record);
        }
        public static int DeleteProjectBudgetHd(Int32 BudgetID)
        {
            return new ProjectBudgetHdDao().Delete(BudgetID);
        }
        public static List<ProjectBudgetHd> GetProjectBudgetHdList(string filterExpression)
        {
            List<ProjectBudgetHd> result = new List<ProjectBudgetHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProjectBudgetHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProjectBudgetHd)helper.IDataReaderToObject(reader, new ProjectBudgetHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ProjectScheduledTask
        public static ProjectScheduledTask GetProjectScheduledTask(Int32 ScheduledTaskID)
        {
            return new ProjectScheduledTaskDao().Get(ScheduledTaskID);
        }
        public static int InsertProjectScheduledTask(ProjectScheduledTask record)
        {
            return new ProjectScheduledTaskDao().Insert(record);
        }
        public static int UpdateProjectScheduledTask(ProjectScheduledTask record)
        {
            return new ProjectScheduledTaskDao().Update(record);
        }
        public static int DeleteProjectScheduledTask(Int32 ScheduledTaskID)
        {
            return new ProjectScheduledTaskDao().Delete(ScheduledTaskID);
        }
        public static List<ProjectScheduledTask> GetProjectScheduledTaskList(string filterExpression)
        {
            List<ProjectScheduledTask> result = new List<ProjectScheduledTask>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProjectScheduledTask));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProjectScheduledTask)helper.IDataReaderToObject(reader, new ProjectScheduledTask()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetProjectScheduledTaskMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ProjectScheduledTask));
                ctx.CommandText = helper.SelectMaxColumn("ScheduledTaskID");
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
        #region ProjectTeam
        public static ProjectTeam GetProjectTeam(Int32 ProjectID, Int32 TeamID)
        {
            return new ProjectTeamDao().Get(ProjectID, TeamID);
        }
        public static int InsertProjectTeam(ProjectTeam record)
        {
            return new ProjectTeamDao().Insert(record);
        }
        public static int UpdateProjectTeam(ProjectTeam record)
        {
            return new ProjectTeamDao().Update(record);
        }
        public static int DeleteProjectTeam(Int32 ProjectID, Int32 TeamID)
        {
            return new ProjectTeamDao().Delete(ProjectID, TeamID);
        }
        public static List<ProjectTeam> GetProjectTeamList(string filterExpression)
        {
            List<ProjectTeam> result = new List<ProjectTeam>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProjectTeam));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProjectTeam)helper.IDataReaderToObject(reader, new ProjectTeam()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ProjectTask
        public static ProjectTask GetProjectTask(Int32 ProjectTaskID)
        {
            return new ProjectTaskDao().Get(ProjectTaskID);
        }
        public static int InsertProjectTask(ProjectTask record)
        {
            return new ProjectTaskDao().Insert(record);
        }
        public static int UpdateProjectTask(ProjectTask record)
        {
            return new ProjectTaskDao().Update(record);
        }
        public static int DeleteProjectTask(Int32 ProjectTaskID)
        {
            return new ProjectTaskDao().Delete(ProjectTaskID);
        }
        public static List<ProjectTask> GetProjectTaskList(string filterExpression)
        {
            List<ProjectTask> result = new List<ProjectTask>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProjectTask));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProjectTask)helper.IDataReaderToObject(reader, new ProjectTask()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetProjectTaskMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ProjectTask));
                ctx.CommandText = helper.SelectMaxColumn("ProjectTaskID");
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
        #region ProjectTaskBudget
        public static ProjectTaskBudget GetProjectTaskBudget(Int32 BudgetID, Int32 ProjectTaskID)
        {
            return new ProjectTaskBudgetDao().Get(BudgetID, ProjectTaskID);
        }
        public static int InsertProjectTaskBudget(ProjectTaskBudget record)
        {
            return new ProjectTaskBudgetDao().Insert(record);
        }
        public static int UpdateProjectTaskBudget(ProjectTaskBudget record)
        {
            return new ProjectTaskBudgetDao().Update(record);
        }
        public static int DeleteProjectTaskBudget(Int32 BudgetID, Int32 ProjectTaskID)
        {
            return new ProjectTaskBudgetDao().Delete(BudgetID, ProjectTaskID);
        }
        public static List<ProjectTaskBudget> GetProjectTaskBudgetList(string filterExpression)
        {
            List<ProjectTaskBudget> result = new List<ProjectTaskBudget>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProjectTaskBudget));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProjectTaskBudget)helper.IDataReaderToObject(reader, new ProjectTaskBudget()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ProjectTaskFile
        public static ProjectTaskFile GetProjectTaskFile(Int32 FileID)
        {
            return new ProjectTaskFileDao().Get(FileID);
        }
        public static int InsertProjectTaskFile(ProjectTaskFile record)
        {
            return new ProjectTaskFileDao().Insert(record);
        }
        public static int UpdateProjectTaskFile(ProjectTaskFile record)
        {
            return new ProjectTaskFileDao().Update(record);
        }
        public static int DeleteProjectTaskFile(Int32 FileID)
        {
            return new ProjectTaskFileDao().Delete(FileID);
        }
        public static List<ProjectTaskFile> GetProjectTaskFileList(string filterExpression)
        {
            List<ProjectTaskFile> result = new List<ProjectTaskFile>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProjectTaskFile));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProjectTaskFile)helper.IDataReaderToObject(reader, new ProjectTaskFile()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ProjectTaskLog
        public static ProjectTaskLog GetProjectTaskLog(Int32 ProjectTaskLogID)
        {
            return new ProjectTaskLogDao().Get(ProjectTaskLogID);
        }
        public static int InsertProjectTaskLog(ProjectTaskLog record)
        {
            return new ProjectTaskLogDao().Insert(record);
        }
        public static int UpdateProjectTaskLog(ProjectTaskLog record)
        {
            return new ProjectTaskLogDao().Update(record);
        }
        public static int DeleteProjectTaskLog(Int32 ProjectTaskLogID)
        {
            return new ProjectTaskLogDao().Delete(ProjectTaskLogID);
        }
        public static List<ProjectTaskLog> GetProjectTaskLogList(string filterExpression)
        {
            List<ProjectTaskLog> result = new List<ProjectTaskLog>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProjectTaskLog));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProjectTaskLog)helper.IDataReaderToObject(reader, new ProjectTaskLog()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ProjectTaskStructure
        public static ProjectTaskStructure GetProjectTaskStructure(Int32 ProjectTaskID, Int32 PrevProjectTaskID)
        {
            return new ProjectTaskStructureDao().Get(ProjectTaskID, PrevProjectTaskID);
        }
        public static int InsertProjectTaskStructure(ProjectTaskStructure record)
        {
            return new ProjectTaskStructureDao().Insert(record);
        }
        public static int UpdateProjectTaskStructure(ProjectTaskStructure record)
        {
            return new ProjectTaskStructureDao().Update(record);
        }
        public static int DeleteProjectTaskStructure(Int32 ProjectTaskID, Int32 PrevProjectTaskID)
        {
            return new ProjectTaskStructureDao().Delete(ProjectTaskID, PrevProjectTaskID);
        }
        public static List<ProjectTaskStructure> GetProjectTaskStructureList(string filterExpression)
        {
            List<ProjectTaskStructure> result = new List<ProjectTaskStructure>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProjectTaskStructure));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProjectTaskStructure)helper.IDataReaderToObject(reader, new ProjectTaskStructure()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ProposedBudgetDt
        public static ProposedBudgetDt GetProposedBudgetDt(Int32 ProposedBudgetDtID)
        {
            return new ProposedBudgetDtDao().Get(ProposedBudgetDtID);
        }
        public static int InsertProposedBudgetDt(ProposedBudgetDt record)
        {
            return new ProposedBudgetDtDao().Insert(record);
        }
        public static int UpdateProposedBudgetDt(ProposedBudgetDt record)
        {
            return new ProposedBudgetDtDao().Update(record);
        }
        public static int DeleteProposedBudgetDt(Int32 ProposedBudgetDtID)
        {
            return new ProposedBudgetDtDao().Delete(ProposedBudgetDtID);
        }
        public static List<ProposedBudgetDt> GetProposedBudgetDtList(string filterExpression)
        {
            List<ProposedBudgetDt> result = new List<ProposedBudgetDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProposedBudgetDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProposedBudgetDt)helper.IDataReaderToObject(reader, new ProposedBudgetDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ProposedBudgetDt> GetProposedBudgetDtList(string filterExpression, IDbContext ctx)
        {
            List<ProposedBudgetDt> result = new List<ProposedBudgetDt>();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProposedBudgetDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProposedBudgetDt)helper.IDataReaderToObject(reader, new ProposedBudgetDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        public static Int32 GetProposedBudgetDtMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ProposedBudgetDt));
                ctx.CommandText = helper.SelectMaxColumn("ProposedBudgetDtID");
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
        #region ProposedBudgetDtFund
        public static ProposedBudgetDtFund GetProposedBudgetDtFund(Int32 ProposedBudgetFundID)
        {
            return new ProposedBudgetDtFundDao().Get(ProposedBudgetFundID);
        }
        public static int InsertProposedBudgetDtFund(ProposedBudgetDtFund record)
        {
            return new ProposedBudgetDtFundDao().Insert(record);
        }
        public static int UpdateProposedBudgetDtFund(ProposedBudgetDtFund record)
        {
            return new ProposedBudgetDtFundDao().Update(record);
        }
        public static int DeleteProposedBudgetDtFund(Int32 ProposedBudgetFundID)
        {
            return new ProposedBudgetDtFundDao().Delete(ProposedBudgetFundID);
        }
        public static List<ProposedBudgetDtFund> GetProposedBudgetDtFundList(string filterExpression)
        {
            List<ProposedBudgetDtFund> result = new List<ProposedBudgetDtFund>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProposedBudgetDtFund));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProposedBudgetDtFund)helper.IDataReaderToObject(reader, new ProposedBudgetDtFund()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ProposedBudgetHd
        public static ProposedBudgetHd GetProposedBudgetHd(Int32 ProposedBudgetID)
        {
            return new ProposedBudgetHdDao().Get(ProposedBudgetID);
        }
        public static int InsertProposedBudgetHd(ProposedBudgetHd record)
        {
            return new ProposedBudgetHdDao().Insert(record);
        }
        public static int UpdateProposedBudgetHd(ProposedBudgetHd record)
        {
            return new ProposedBudgetHdDao().Update(record);
        }
        public static int DeleteProposedBudgetHd(Int32 ProposedBudgetID)
        {
            return new ProposedBudgetHdDao().Delete(ProposedBudgetID);
        }
        public static List<ProposedBudgetHd> GetProposedBudgetHdList(string filterExpression)
        {
            List<ProposedBudgetHd> result = new List<ProposedBudgetHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ProposedBudgetHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ProposedBudgetHd)helper.IDataReaderToObject(reader, new ProposedBudgetHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetProposedBudgetHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(ProposedBudgetHd));
                ctx.CommandText = helper.SelectMaxColumn("ProposedBudgetID");
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
    }
}
