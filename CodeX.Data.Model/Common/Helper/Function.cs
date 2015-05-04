using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Globalization;
using System.Text.RegularExpressions;
using CodeX.Common;
using CodeX.Data.Core.Dal;
using System.Reflection;
using System.IO;
using System.Threading;

namespace CodeX.Data.Model
{
    public partial class Function
    {
        #region Insert Log
        public enum LogType
        {
            Insert = 0, Update = 1, Delete = 2
        }

        public static void InsertLog(object obj, LogType logType)
        {
            Thread thread1 = new Thread(new ThreadStart(() => InsertLogToTextFile(obj, logType)));
            thread1.Start();
        }

        static void InsertLogToTextFile(object obj, LogType logType)
        {
            if (obj is DbDataModel)
            {
                Type type = obj.GetType();

                string physicalPath = AppConfigManager.CDXTableLogFolder + "conf.txt";
                string[] lstConf = System.IO.File.ReadAllLines(physicalPath, Encoding.GetEncoding("windows-1250"));
                string _tableName = GetTableName(type);
                foreach (string conf in lstConf)
                {
                    string[] temp = conf.Split(';');
                    if (temp[0] == _tableName)
                    {
                        physicalPath = string.Format("{0}{1}\\", AppConfigManager.CDXTableLogFolder, _tableName);
                        if (!Directory.Exists(physicalPath))
                            Directory.CreateDirectory(physicalPath);

                        string myFile = string.Format("{0}\\{1}.txt", physicalPath, DateTime.Now.ToString("yyyyMMdd"));

                        string[] listColumn = temp[1].Split('|');
                        string message = "";
                        PropertyInfo[] propInfs = type.GetProperties();
                        foreach (PropertyInfo prop in propInfs)
                        {
                            object[] custAttr = prop.GetCustomAttributes(false);
                            foreach (Attribute attrib in custAttr)
                            {
                                ColumnAttribute schema = attrib as ColumnAttribute;
                                if (schema != null)
                                {
                                    if (listColumn.Contains(schema.Name))
                                    {
                                        object fieldValue = prop.GetValue(obj, null);
                                        if (!schema.IsNullable)
                                            fieldValue = CheckIsNull(fieldValue, prop.PropertyType);

                                        if (message != "")
                                            message += "|";
                                        if (fieldValue != null)
                                            message += fieldValue;
                                        else
                                            message += "NULL";
                                    }
                                }
                            }
                        }
                        message += "|" + ((int)logType).ToString();
                        message += Environment.NewLine;
                        if (!File.Exists(myFile))
                            File.WriteAllText(myFile, message);
                        else
                            File.AppendAllText(myFile, message);
                        break;
                    }
                }
            }
        }

        public object CheckIsNull(object obj)
        {
            if (obj == null) return null;
            Type type = obj.GetType();
            foreach (PropertyInfo prop in type.GetProperties())
            {
                foreach (Attribute attrib in prop.GetCustomAttributes(true))
                {
                    ColumnAttribute schema = attrib as ColumnAttribute;
                    if (schema != null && !schema.IsNullable)
                    {
                        prop.SetValue(obj, CheckIsNull(prop.GetValue(obj, null), prop.PropertyType), null);
                    }
                }
            }
            return obj;
        }


        private static object CheckIsNull(object obj, Type type)
        {
            if (type.FullName.Contains("DateTime"))
            {
                if (obj is DBNull || obj == null)
                    return Convert.ToDateTime("1900-01-01");
                if (Convert.ToDateTime(obj).Year < 1900)
                    return Convert.ToDateTime("1900-01-01");
            }
            else if (obj is DBNull || obj == null)
            {
                if (type.FullName.Contains("String")) return string.Empty;
                if (type.FullName.Contains("Boolean")) return false;
                return null;
            }
            return obj;
        }

        private static string GetTableName(MemberInfo type)
        {
            TableAttribute tableInfo = (TableAttribute)Attribute.GetCustomAttribute(type, typeof(TableAttribute));
            if (tableInfo == null || tableInfo.Name.Equals(""))
            {
                return type.Name;
            }
            else
            {
                return tableInfo.Name;
            }
        }
        #endregion

        #region Common
        /// <summary>
        /// parameter : string time ( yyyyMMdd )
        /// </summary>
        /// <param name="timeIn_yyyyMMdd"></param>
        /// <returns></returns>
        public static DateTime StringToDateTime(string time)
        {
            DateTime theTime = DateTime.ParseExact(time,
                                        "yyyyMMdd",
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.None);
            return theTime;
        }

        public static String ToRoman(int number)
        {
            if (-9999 >= number || number >= 9999)
            {
                throw new ArgumentOutOfRangeException("number");
            }

            if (number == 0)
            {
                return "NUL";
            }

            StringBuilder sb = new StringBuilder(10);

            if (number < 0)
            {
                sb.Append('-');
                number *= -1;
            }

            string[,] table = new string[,] { 
        { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" }, 
        { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" }, 
        { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" },
        { "", "M", "MM", "MMM", "M(V)", "(V)", "(V)M", 
                                          "(V)MM", "(V)MMM", "M(X)" } 
    };

            for (int i = 1000, j = 3; i > 0; i /= 10, j--)
            {
                int digit = number / i;
                sb.Append(table[j, digit]);
                number -= digit * i;
            }

            return sb.ToString();
        }

        public static String GenerateAddress(String _StreetName, String _County, String _District, String _City, String _State)
        {
            StringBuilder result = new StringBuilder();
            if (_StreetName != "")
                result.Append(_StreetName).Append(" ");
            if (_County != "")
                result.Append(_County).Append(" ");
            if (_District != "")
                result.Append(_District).Append(" ");
            if (_City != "")
                result.Append(_City).Append(" ");
            if (_State != "")
                result.Append(_State).Append(" ");
            return result.ToString();
        }

        public static String NumberInWords(Int64 amount, Boolean isMoney = false)
        {
            StringBuilder strbuild;
            if (isMoney)
                strbuild = new StringBuilder("RUPIAH");
            else
                strbuild = new StringBuilder();

            String[] arrBil = { "", "SATU ", "DUA ", "TIGA ", "EMPAT ", "LIMA ", "ENAM ", "TUJUH ", "DELAPAN ", "SEMBILAN ", "SE" };
            String[] arrSatKecil = { "", "PULUH ", "RATUS " };
            String[] arrSatBesar = { "", "RIBU ", "JUTA ", "MILYAR " };
            int ctrKecil = 0;
            int ctrBesar = 0;
            if (amount == 0)
            {
                if (isMoney)
                    return "NOL RUPIAH";
                else
                    return "NOL";
            }
            else
            {
                while (amount > 0)
                {
                    long a = amount % 10;
                    amount /= 10;

                    if (a > 0)
                        strbuild.Insert(0, arrSatKecil[ctrKecil]);

                    if (a == 1 && ctrKecil > 0)
                        strbuild.Insert(0, arrBil[10]);
                    else if (ctrKecil == 0 && amount % 10 == 1 && a > 0)
                    {
                        strbuild.Insert(0, "BELAS ");
                        if (a == 1)
                            a = 10;
                        strbuild.Insert(0, arrBil[a]);
                        amount /= 10;
                        ctrKecil++;
                    }
                    else
                        strbuild.Insert(0, arrBil[a]);

                    ctrKecil++;
                    if (ctrKecil % 3 == 0)
                    {
                        ctrBesar++;
                        ctrKecil = 0;
                        if (amount > 0 && amount % 1000 > 0)
                        {
                            strbuild.Insert(0, arrSatBesar[ctrBesar]);
                        }
                    }

                }
                return strbuild.ToString();
            }


        }

        #region Calculate Patient Age Based on DateOfBirth
        public static int GetPatientAgeInDay(DateTime dateOfBirth, DateTime nowDate)
        {
            int day = GetPatientAge(dateOfBirth, nowDate, 1);
            return day;
        }
        public static int GetPatientAgeInMonth(DateTime dateOfBirth, DateTime nowDate)
        {
            int month = GetPatientAge(dateOfBirth, nowDate, 2);
            return month;
        }
        public static int GetPatientAgeInYear(DateTime dateOfBirth, DateTime nowDate)
        {
            int year = GetPatientAge(dateOfBirth, nowDate, 3);
            return year;
        }
        public static int GetPatientAge(DateTime dateOfBirth, DateTime nowDate, int type)
        {
            var days = nowDate.Day - dateOfBirth.Day;
            if (days < 0)
            {
                var newNow = nowDate.AddMonths(-1);
                days += (int)(nowDate - newNow).TotalDays;
                nowDate = newNow;
            }
            var months = nowDate.Month - dateOfBirth.Month;
            if (months < 0)
            {
                months += 12;
                nowDate = nowDate.AddYears(-1);
            }
            var years = nowDate.Year - dateOfBirth.Year;
            int typo = 0;
            switch (type)
            {
                case 1: typo = days; break;
                case 2: typo = months; break;
                case 3: typo = years; break;
            }
            return typo;
        }

        public static string UrlRoot()
        {
            string url = "";
            if (HttpRuntime.AppDomainAppVirtualPath.Equals("/"))
            {
                string absolutePath = HttpContext.Current.Request.Url.AbsolutePath;
                int count = absolutePath.Split('/').Length - 2;
                for (int i = 0; i < count; i++)
                {
                    url += "../";
                }
                url = url.Substring(0, url.Length - 1);
            }
            else
                url = HttpRuntime.AppDomainAppVirtualPath;

            return url; 
        }
        #endregion
        #endregion
    }
}
