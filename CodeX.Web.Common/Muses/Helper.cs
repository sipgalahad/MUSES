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
        public static String GenerateItemCode(IDbContext ctx, String ItemName)
        {
            string itemName2Char = ItemName.Trim().Substring(0, 2).ToUpper();
            ItemMaster im = BusinessLayer.GetItemMasterList(string.Format("ItemCode LIKE '{0}%'", itemName2Char), 1, 1, "ItemCode DESC", ctx).FirstOrDefault();
            int newNumber = 1;
            if (im != null)
                newNumber = Convert.ToInt32(im.ItemCode.Substring(itemName2Char.Length)) + 1;
            return string.Format("{0}{1}", itemName2Char, newNumber.ToString().PadLeft(5, '0'));
        }
        public static string GetModuleID(string moduleName)
        {
            string result = "";
            moduleName = moduleName.ToLower();
            switch (moduleName)
            {
                case "controlpanel": result = "CP"; break;
                case "finance": result = "FN"; break;
                case "inventory": result = "IM"; break;
                case "teacherpage": result = "TP"; break;
                default: result = "SM"; break;
            }
            return result;
        }
    }
}
