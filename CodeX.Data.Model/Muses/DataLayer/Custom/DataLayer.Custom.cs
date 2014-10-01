using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeX.Common;

namespace CodeX.Data.Model
{
    #region DailyScheduleTypeDt
    public partial class DailyScheduleTypeDt
    {
        public string cfDailyScheduleType
        {
            get { return _GCDailyScheduleType.Split('^')[1]; }
        }
    }
    #endregion
}
