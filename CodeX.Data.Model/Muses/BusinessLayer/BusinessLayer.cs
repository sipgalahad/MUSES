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
        #region SchoolDailySchedule
        public static SchoolDailySchedule GetSchoolDailySchedule(Int32 SchoolDailyScheduleID)
        {
            return new SchoolDailyScheduleDao().Get(SchoolDailyScheduleID);
        }
        public static int InsertSchoolDailySchedule(SchoolDailySchedule record)
        {
            return new SchoolDailyScheduleDao().Insert(record);
        }
        public static int UpdateSchoolDailySchedule(SchoolDailySchedule record)
        {
            return new SchoolDailyScheduleDao().Update(record);
        }
        public static int DeleteSchoolDailySchedule(Int32 SchoolDailyScheduleID)
        {
            return new SchoolDailyScheduleDao().Delete(SchoolDailyScheduleID);
        }
        public static List<SchoolDailySchedule> GetSchoolDailyScheduleList(string filterExpression)
        {
            List<SchoolDailySchedule> result = new List<SchoolDailySchedule>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolDailySchedule));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SchoolDailySchedule)helper.IDataReaderToObject(reader, new SchoolDailySchedule()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SchoolDailySchedulePackage
        public static SchoolDailySchedulePackage GetSchoolDailySchedulePackage(Int32 SchoolDailySchedulePackageID)
        {
            return new SchoolDailySchedulePackageDao().Get(SchoolDailySchedulePackageID);
        }
        public static int InsertSchoolDailySchedulePackage(SchoolDailySchedulePackage record)
        {
            return new SchoolDailySchedulePackageDao().Insert(record);
        }
        public static int UpdateSchoolDailySchedulePackage(SchoolDailySchedulePackage record)
        {
            return new SchoolDailySchedulePackageDao().Update(record);
        }
        public static int DeleteSchoolDailySchedulePackage(Int32 SchoolDailySchedulePackageID)
        {
            return new SchoolDailySchedulePackageDao().Delete(SchoolDailySchedulePackageID);
        }
        public static List<SchoolDailySchedulePackage> GetSchoolDailySchedulePackageList(string filterExpression)
        {
            List<SchoolDailySchedulePackage> result = new List<SchoolDailySchedulePackage>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolDailySchedulePackage));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SchoolDailySchedulePackage)helper.IDataReaderToObject(reader, new SchoolDailySchedulePackage()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSchoolDailySchedulePackageRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolDailySchedulePackage));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<SchoolDailySchedulePackage> GetSchoolDailySchedulePackageList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<SchoolDailySchedulePackage> result = new List<SchoolDailySchedulePackage>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolDailySchedulePackage));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SchoolDailySchedulePackage)helper.IDataReaderToObject(reader, new SchoolDailySchedulePackage()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSchoolDailySchedulePackageRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolDailySchedulePackage));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "SchoolDailySchedulePackageID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSchoolDailySchedulePackageMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolDailySchedulePackage));
                ctx.CommandText = helper.SelectMaxColumn("SchoolDailySchedulePackageID");
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
        #region SchoolDailyScheduleTypeDt
        public static SchoolDailyScheduleTypeDt GetSchoolDailyScheduleTypeDt(Int32 SchoolDailyScheduleTypeDtID)
        {
            return new SchoolDailyScheduleTypeDtDao().Get(SchoolDailyScheduleTypeDtID);
        }
        public static int InsertSchoolDailyScheduleTypeDt(SchoolDailyScheduleTypeDt record)
        {
            return new SchoolDailyScheduleTypeDtDao().Insert(record);
        }
        public static int UpdateSchoolDailyScheduleTypeDt(SchoolDailyScheduleTypeDt record)
        {
            return new SchoolDailyScheduleTypeDtDao().Update(record);
        }
        public static int DeleteSchoolDailyScheduleTypeDt(Int32 SchoolDailyScheduleTypeDtID)
        {
            return new SchoolDailyScheduleTypeDtDao().Delete(SchoolDailyScheduleTypeDtID);
        }
        public static List<SchoolDailyScheduleTypeDt> GetSchoolDailyScheduleTypeDtList(string filterExpression)
        {
            List<SchoolDailyScheduleTypeDt> result = new List<SchoolDailyScheduleTypeDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolDailyScheduleTypeDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SchoolDailyScheduleTypeDt)helper.IDataReaderToObject(reader, new SchoolDailyScheduleTypeDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SchoolDailyScheduleTypeHd
        public static SchoolDailyScheduleTypeHd GetSchoolDailyScheduleTypeHd(Int32 SchoolDailyScheduleTypeHdID)
        {
            return new SchoolDailyScheduleTypeHdDao().Get(SchoolDailyScheduleTypeHdID);
        }
        public static int InsertSchoolDailyScheduleTypeHd(SchoolDailyScheduleTypeHd record)
        {
            return new SchoolDailyScheduleTypeHdDao().Insert(record);
        }
        public static int UpdateSchoolDailyScheduleTypeHd(SchoolDailyScheduleTypeHd record)
        {
            return new SchoolDailyScheduleTypeHdDao().Update(record);
        }
        public static int DeleteSchoolDailyScheduleTypeHd(Int32 SchoolDailyScheduleTypeHdID)
        {
            return new SchoolDailyScheduleTypeHdDao().Delete(SchoolDailyScheduleTypeHdID);
        }
        public static List<SchoolDailyScheduleTypeHd> GetSchoolDailyScheduleTypeHdList(string filterExpression)
        {
            List<SchoolDailyScheduleTypeHd> result = new List<SchoolDailyScheduleTypeHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolDailyScheduleTypeHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SchoolDailyScheduleTypeHd)helper.IDataReaderToObject(reader, new SchoolDailyScheduleTypeHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSchoolDailyScheduleTypeHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolDailyScheduleTypeHd));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<SchoolDailyScheduleTypeHd> GetSchoolDailyScheduleTypeHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<SchoolDailyScheduleTypeHd> result = new List<SchoolDailyScheduleTypeHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolDailyScheduleTypeHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SchoolDailyScheduleTypeHd)helper.IDataReaderToObject(reader, new SchoolDailyScheduleTypeHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSchoolDailyScheduleTypeHdRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolDailyScheduleTypeHd));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "SchoolDailyScheduleTypeID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSchoolDailyScheduleTypeHdMaxID(IDbContext ctx)
        {
            Int32 result = 0;
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolDailyScheduleTypeHd));
                ctx.CommandText = helper.SelectMaxColumn("SchoolDailyScheduleTypeID");
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
        #region SchoolPeriodSchedule
        public static SchoolPeriodSchedule GetSchoolPeriodSchedule(Int32 SchoolPeriodScheduleID)
        {
            return new SchoolPeriodScheduleDao().Get(SchoolPeriodScheduleID);
        }
        public static int InsertSchoolPeriodSchedule(SchoolPeriodSchedule record)
        {
            return new SchoolPeriodScheduleDao().Insert(record);
        }
        public static int UpdateSchoolPeriodSchedule(SchoolPeriodSchedule record)
        {
            return new SchoolPeriodScheduleDao().Update(record);
        }
        public static int DeleteSchoolPeriodSchedule(Int32 SchoolPeriodScheduleID)
        {
            return new SchoolPeriodScheduleDao().Delete(SchoolPeriodScheduleID);
        }
        public static List<SchoolPeriodSchedule> GetSchoolPeriodScheduleList(string filterExpression)
        {
            List<SchoolPeriodSchedule> result = new List<SchoolPeriodSchedule>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolPeriodSchedule));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SchoolPeriodSchedule)helper.IDataReaderToObject(reader, new SchoolPeriodSchedule()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SchoolPeriodSection
        public static SchoolPeriodSection GetSchoolPeriodSection(Int32 SchoolPeriodSectionID)
        {
            return new SchoolPeriodSectionDao().Get(SchoolPeriodSectionID);
        }
        public static int InsertSchoolPeriodSection(SchoolPeriodSection record)
        {
            return new SchoolPeriodSectionDao().Insert(record);
        }
        public static int UpdateSchoolPeriodSection(SchoolPeriodSection record)
        {
            return new SchoolPeriodSectionDao().Update(record);
        }
        public static int DeleteSchoolPeriodSection(Int32 SchoolPeriodSectionID)
        {
            return new SchoolPeriodSectionDao().Delete(SchoolPeriodSectionID);
        }
        public static List<SchoolPeriodSection> GetSchoolPeriodSectionList(string filterExpression)
        {
            List<SchoolPeriodSection> result = new List<SchoolPeriodSection>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SchoolPeriodSection));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SchoolPeriodSection)helper.IDataReaderToObject(reader, new SchoolPeriodSection()));
            }
            catch (Exception ex)
            {
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
    }
}
