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
        #endregion
        #region ClassStudentSubjectMark
        public static ClassStudentSubjectMark GetClassStudentSubjectMark(Int32 ClassSubjectID, Int32 StudentID)
        {
            return new ClassStudentSubjectMarkDao().Get(ClassSubjectID, StudentID);
        }
        public static int InsertClassStudentSubjectMark(ClassStudentSubjectMark record)
        {
            return new ClassStudentSubjectMarkDao().Insert(record);
        }
        public static int UpdateClassStudentSubjectMark(ClassStudentSubjectMark record)
        {
            return new ClassStudentSubjectMarkDao().Update(record);
        }
        public static int DeleteClassStudentSubjectMark(Int32 ClassSubjectID, Int32 StudentID)
        {
            return new ClassStudentSubjectMarkDao().Delete(ClassSubjectID, StudentID);
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
        #region GLSetting
        public static GLSetting GetGLSetting(Int32 ID)
        {
            return new GLSettingDao().Get(ID);
        }
        public static int InsertGLSetting(GLSetting record)
        {
            return new GLSettingDao().Insert(record);
        }
        public static int UpdateGLSetting(GLSetting record)
        {
            return new GLSettingDao().Update(record);
        }
        public static int DeleteGLSetting(Int32 ID)
        {
            return new GLSettingDao().Delete(ID);
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
        #region RegistrationFee
        public static RegistrationFee GetRegistrationFee(Int32 RegistrationFeeID)
        {
            return new RegistrationFeeDao().Get(RegistrationFeeID);
        }
        public static int InsertRegistrationFee(RegistrationFee record)
        {
            return new RegistrationFeeDao().Insert(record);
        }
        public static int UpdateRegistrationFee(RegistrationFee record)
        {
            return new RegistrationFeeDao().Update(record);
        }
        public static int DeleteRegistrationFee(Int32 RegistrationFeeID)
        {
            return new RegistrationFeeDao().Delete(RegistrationFeeID);
        }
        public static List<RegistrationFee> GetRegistrationFeeList(string filterExpression)
        {
            List<RegistrationFee> result = new List<RegistrationFee>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RegistrationFee));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RegistrationFee)helper.IDataReaderToObject(reader, new RegistrationFee()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<RegistrationFee> GetRegistrationFeeList(string filterExpression, IDbContext ctx)
        {
            List<RegistrationFee> result = new List<RegistrationFee>();
            try
            {
                DbHelper helper = new DbHelper(typeof(RegistrationFee));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RegistrationFee)helper.IDataReaderToObject(reader, new RegistrationFee()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
        }
        #endregion
        #region RegistrationFeeComp
        public static RegistrationFeeComp GetRegistrationFeeComp(Int32 RegistrationFeeCompID)
        {
            return new RegistrationFeeCompDao().Get(RegistrationFeeCompID);
        }
        public static int InsertRegistrationFeeComp(RegistrationFeeComp record)
        {
            return new RegistrationFeeCompDao().Insert(record);
        }
        public static int UpdateRegistrationFeeComp(RegistrationFeeComp record)
        {
            return new RegistrationFeeCompDao().Update(record);
        }
        public static int DeleteRegistrationFeeComp(Int32 RegistrationFeeCompID)
        {
            return new RegistrationFeeCompDao().Delete(RegistrationFeeCompID);
        }
        public static List<RegistrationFeeComp> GetRegistrationFeeCompList(string filterExpression)
        {
            List<RegistrationFeeComp> result = new List<RegistrationFeeComp>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RegistrationFeeComp));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RegistrationFeeComp)helper.IDataReaderToObject(reader, new RegistrationFeeComp()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<RegistrationFeeComp> GetRegistrationFeeCompList(string filterExpression, IDbContext ctx)
        {
            List<RegistrationFeeComp> result = new List<RegistrationFeeComp>();
            try
            {
                DbHelper helper = new DbHelper(typeof(RegistrationFeeComp));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RegistrationFeeComp)helper.IDataReaderToObject(reader, new RegistrationFeeComp()));
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
        #region SubjectGradeMajor
        public static SubjectGradeMajor GetSubjectGradeMajor(Int32 SubjectID, String GCGrade)
        {
            return new SubjectGradeMajorDao().Get(SubjectID, GCGrade);
        }
        public static int InsertSubjectGradeMajor(SubjectGradeMajor record)
        {
            return new SubjectGradeMajorDao().Insert(record);
        }
        public static int UpdateSubjectGradeMajor(SubjectGradeMajor record)
        {
            return new SubjectGradeMajorDao().Update(record);
        }
        public static int DeleteSubjectGradeMajor(Int32 SubjectID, String GCGrade)
        {
            return new SubjectGradeMajorDao().Delete(SubjectID, GCGrade);
        }
        public static List<SubjectGradeMajor> GetSubjectGradeMajorList(string filterExpression)
        {
            List<SubjectGradeMajor> result = new List<SubjectGradeMajor>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SubjectGradeMajor));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SubjectGradeMajor)helper.IDataReaderToObject(reader, new SubjectGradeMajor()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SubjectMatter
        public static SubjectMatter GetSubjectMatter(Int32 SubjectMatterID)
        {
            return new SubjectMatterDao().Get(SubjectMatterID);
        }
        public static int InsertSubjectMatter(SubjectMatter record)
        {
            return new SubjectMatterDao().Insert(record);
        }
        public static int UpdateSubjectMatter(SubjectMatter record)
        {
            return new SubjectMatterDao().Update(record);
        }
        public static int DeleteSubjectMatter(Int32 SubjectMatterID)
        {
            return new SubjectMatterDao().Delete(SubjectMatterID);
        }
        public static List<SubjectMatter> GetSubjectMatterList(string filterExpression)
        {
            List<SubjectMatter> result = new List<SubjectMatter>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SubjectMatter));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SubjectMatter)helper.IDataReaderToObject(reader, new SubjectMatter()));
            }
            catch (Exception ex)
            {
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
        public static List<Teacher> GetTeacherList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<Teacher> result = new List<Teacher>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Teacher));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
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

        public static Int32 GetTeacherRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Teacher));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTeacherRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Teacher));
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
        public static Int32 GetTeacherMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(Teacher));
                ctx.CommandText = helper.SelectMaxColumn("TeacherID");
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
        #region TeacherSubject
        public static TeacherSubject GetTeacherSubject(Int32 TeacherID, Int32 SubjectID)
        {
            return new TeacherSubjectDao().Get(TeacherID, SubjectID);
        }
        public static int InsertTeacherSubject(TeacherSubject record)
        {
            return new TeacherSubjectDao().Insert(record);
        }
        public static int UpdateTeacherSubject(TeacherSubject record)
        {
            return new TeacherSubjectDao().Update(record);
        }
        public static int DeleteTeacherSubject(Int32 TeacherID, Int32 SubjectID)
        {
            return new TeacherSubjectDao().Delete(TeacherID, SubjectID);
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
        public static List<TeacherSubject> GetTeacherSubjectList(string filterExpression, IDbContext ctx)
        {
            List<TeacherSubject> result = new List<TeacherSubject>();
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
    }
}
