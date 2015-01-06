using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeX.Data.Model;

namespace CodeX.Web.Common.UI
{
    public abstract class BaseCustomReportCtl : System.Web.UI.UserControl 
    {
        public abstract void Bind(string filterExpression, string[] param);
    }
}
