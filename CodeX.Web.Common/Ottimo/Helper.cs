using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Globalization;
using DevExpress.Web.ASPxEditors;
using System.IO;
using CodeX.Data.Model;
using System.Data;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Web.Common
{
    public partial class Helper
    {
        public static string GetModuleID(string moduleName)
        {
            string result = "";
            moduleName = moduleName.ToLower();
            switch (moduleName)
            {
                case "controlpanel": result = "CP"; break;
                case "inventory": result = "IM"; break;
                case "teacherpage": result = "TP"; break;
                default: result = "SM"; break;
            }
            return result;
        }
    }
}
