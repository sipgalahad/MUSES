using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeX.Common
{
    public partial class Constant
    {
        public static class GridViewPageSize
        {
            public const int GRID_MASTER = 15;
            public const int GRID_MATRIX = 10;
            public const int GRID_POPUP = 8;
            public const int GRID_POPUP_LIST = 8;
            public const int GRID_PATIENT_LIST = 10;
        }

        public static class DefaultValueEntry
        {
            public const string DATE_NOW = "@DateNow";
            public const string TIME_NOW = "@TimeNow";
        }

        public static class FormatString
        {
            public const string DATE_FORMAT = "dd-MMM-yyyy";
            public const string DATE_PICKER_FORMAT = "dd-MM-yyyy";
            public const string DATE_FORMAT_112 = "yyyyMMdd";
            public const string TIME_FORMAT = "HH:mm";
            public const string DATE_TIME_FORMAT = "dd-MMM-yyyy HH:mm:ss";
        }

        public static class ConstantDate
        {
            public const string DEFAULT_NULL = "01-01-1900";
        }

        public static partial class SettingParameter
        {
            public const string DEFAULT_PASSWORD = "CMN0001";
            public const string PERSON_NAME_FORMAT = "CMN0002";
        }
    }
}
