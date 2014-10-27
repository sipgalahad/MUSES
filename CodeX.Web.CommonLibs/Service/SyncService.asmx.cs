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
        public object GetItemMasterList(String siteID, DateTime lastSyncDate, string filterExpression)
        {
            if (lastSyncDate.Year > 1900)
            {
                if (filterExpression != "")
                    filterExpression += " AND ";
                filterExpression = string.Format("(ItemID IN (SELECT ItemID FROM SiteItem WHERE SiteID = '{0}' AND IsDeleted = 0 AND CreatedDate > '{0}' OR (LastUpdatedDate IS NOT NULL AND LastUpdatedDate > '{0}'))) OR ", siteID, lastSyncDate);
                filterExpression = string.Format("(ItemID IN (SELECT ItemID FROM SiteItem WHERE SiteID = '{0}' AND IsDeleted = 0) AND CreatedDate > '{1}' OR (LastUpdatedDate IS NOT NULL AND LastUpdatedDate > '{1}'))", siteID, lastSyncDate);
            }
            else
                filterExpression = string.Format("ItemID IN (SELECT ItemID FROM SiteItem WHERE SiteID = '{0}' AND IsDeleted = 0)", siteID);

            List<ItemMaster> ListItemMaster = BusinessLayer.GetItemMasterList(filterExpression, 10, 1, "");


            if (lastSyncDate.Year > 1900)
            {
                if (filterExpression != "")
                    filterExpression += " AND ";
                filterExpression = string.Format("(ItemID IN (SELECT ItemID FROM SiteItem WHERE SiteID = '{0}' AND IsDeleted = 0 AND CreatedDate > '{0}' OR (LastUpdatedDate IS NOT NULL AND LastUpdatedDate > '{0}'))) OR ", siteID, lastSyncDate);
                filterExpression = string.Format("(ItemID IN (SELECT ItemID FROM SiteItem WHERE SiteID = '{0}' AND IsDeleted = 0) AND (LastUpdatedDate IS NOT NULL AND LastUpdatedDate > '{1}'))", siteID, lastSyncDate);
            }
            else
                filterExpression = string.Format("ItemID IN (SELECT ItemID FROM SiteItem WHERE SiteID = '{0}' AND IsDeleted = 0)", siteID);

            List<ItemProduct> ListItemProduct = BusinessLayer.GetItemProductList(filterExpression, 10, 1, "");

            Object returnObj = new { ListItemMaster = ListItemMaster, ListItemProduct = ListItemProduct, Timestamp = DateTime.Now };
            return new JavaScriptSerializer().Serialize(returnObj);
        }
    }
}
