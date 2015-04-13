using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Common;
using CodeX.Data.Core.Dal;
using CodeX.Web.Common;
using System.Data;

namespace CodeX.Web.CommonLibs.Controls
{
    public partial class WeekPickerCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            DateTime startOfWeek = DateTime.Today.AddDays(-1 * (int)(DateTime.Today.DayOfWeek));
            DateTime endOfWeek = startOfWeek.AddDays(6);
            hdnStartDate.Value = startOfWeek.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            hdnEndDate.Value = endOfWeek.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtDate.Text = string.Format("{0} - {1}", hdnStartDate.Value, hdnEndDate.Value);
        }

        public DateTime GetStartDate()
        {
            return Helper.GetDatePickerValue(hdnStartDate.Value);
        }
        public DateTime GetEndDate()
        {
            return Helper.GetDatePickerValue(hdnEndDate.Value);
        }
    }
}