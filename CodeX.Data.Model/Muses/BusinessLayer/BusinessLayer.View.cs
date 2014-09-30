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
        #region vSchoolDailyScheduleDt
        public static List<vSchoolDailyScheduleDt> GetvSchoolDailyScheduleDtList(string filterExpression)
        {
            List<vSchoolDailyScheduleDt> result = new List<vSchoolDailyScheduleDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSchoolDailyScheduleDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSchoolDailyScheduleDt)helper.IDataReaderToObject(reader, new vSchoolDailyScheduleDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region vSchoolPeriodSchedule
        public static List<vSchoolPeriodSchedule> GetvSchoolPeriodScheduleList(string filterExpression)
        {
            List<vSchoolPeriodSchedule> result = new List<vSchoolPeriodSchedule>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSchoolPeriodSchedule));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSchoolPeriodSchedule)helper.IDataReaderToObject(reader, new vSchoolPeriodSchedule()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region vSchoolPeriodSection
        public static List<vSchoolPeriodSection> GetvSchoolPeriodSectionList(string filterExpression)
        {
            List<vSchoolPeriodSection> result = new List<vSchoolPeriodSection>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(vSchoolPeriodSection));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((vSchoolPeriodSection)helper.IDataReaderToObject(reader, new vSchoolPeriodSection()));
            }
            catch (Exception ex)
            {
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
        #endregion
    }
}
