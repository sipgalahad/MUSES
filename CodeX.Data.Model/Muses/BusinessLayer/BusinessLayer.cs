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
        #region SiteParameter
        public static SiteParameter GetSiteParameter(String SiteID, String ParameterCode)
        {
            return new SiteParameterDao().Get(SiteID, ParameterCode);
        }
        public static int InsertSiteParameter(SiteParameter record)
        {
            return new SiteParameterDao().Insert(record);
        }
        public static int UpdateSiteParameter(SiteParameter record)
        {
            return new SiteParameterDao().Update(record);
        }
        public static int DeleteSiteParameter(String SiteID, String ParameterCode)
        {
            return new SiteParameterDao().Delete(SiteID, ParameterCode);
        }
        public static List<SiteParameter> GetSiteParameterList(string filterExpression)
        {
            List<SiteParameter> result = new List<SiteParameter>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SiteParameter));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SiteParameter)helper.IDataReaderToObject(reader, new SiteParameter()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<SiteParameter> GetSiteParameterList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<SiteParameter> result = new List<SiteParameter>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SiteParameter));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SiteParameter)helper.IDataReaderToObject(reader, new SiteParameter()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSiteParameterRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SiteParameter));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSiteParameterRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SiteParameter));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ParameterCode", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
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
