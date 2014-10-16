using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using CodeX.Common;

namespace CodeX.Data.Model
{
    #region GetReportUserList
    public partial class GetReportUserList
    {
        public string cfReportTitle
        {
            get
            {
                if (_ReportTitle2 != "")
                    return String.Format("{0} {1}", _ReportTitle1, _ReportTitle2);
                return _ReportTitle1;
            }
        }
    }
    #endregion
}
