using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Collections;
using CodeX.Data.Model;

namespace CodeX.Web.CommonLibs.Service
{
    /// <summary>
    /// Summary description for MethodService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class SyncService : System.Web.Services.WebService
    {

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetItemMasterList(string filterExpression)
        {
            List<ItemMaster> ListItemMaster = BusinessLayer.GetItemMasterList(filterExpression, 10, 1, "");
            List<ItemProduct> ListItemProduct = BusinessLayer.GetItemProductList("", 10, 1, "");

            Object returnObj = new { ListItemMaster = ListItemMaster, ListItemProduct = ListItemProduct, Timestamp = DateTime.Now };
            return new JavaScriptSerializer().Serialize(returnObj);
        }
    }
}
