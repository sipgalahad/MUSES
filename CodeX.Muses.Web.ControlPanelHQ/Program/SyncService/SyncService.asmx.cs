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

namespace CodeX.Muses.Web.ControlPanel.Program
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
        public object GetItemMasterList(String siteID, DateTime lastSyncDate, int pageIndex, int rowCountPerPage, int rowCount)
        {
            string filterExpression = "";
            if (lastSyncDate.Year > 1900)
            {
                if (filterExpression != "")
                    filterExpression += " AND ";
                filterExpression += string.Format("(ItemID IN (SELECT ItemID FROM SiteItem WHERE SiteID = '{0}' AND IsDeleted = 0 AND CreatedDate > '{1}' OR (LastUpdatedDate IS NOT NULL AND LastUpdatedDate > '{1}'))) OR ", siteID, lastSyncDate);
                filterExpression += string.Format("(ItemID IN (SELECT ItemID FROM SiteItem WHERE SiteID = '{0}' AND IsDeleted = 0) AND (CreatedDate > '{1}' OR (LastUpdatedDate IS NOT NULL AND LastUpdatedDate > '{1}')))", siteID, lastSyncDate);
            }
            else
                filterExpression = string.Format("ItemID IN (SELECT ItemID FROM SiteItem WHERE SiteID = '{0}' AND IsDeleted = 0)", siteID);
            if (rowCount < 0)
                rowCount = BusinessLayer.GetItemMasterRowCount(filterExpression);
            List<ItemMaster> ListItemMaster = BusinessLayer.GetItemMasterList(filterExpression, rowCountPerPage, pageIndex, "");
            foreach (ItemMaster entity in ListItemMaster)
                entity.OriginalValue = null;
            

            if (lastSyncDate.Year > 1900)
            {
                if (filterExpression != "")
                    filterExpression += " AND ";
                filterExpression = string.Format("(ItemID IN (SELECT ItemID FROM SiteItem WHERE SiteID = '{0}' AND IsDeleted = 0 AND CreatedDate > '{1}' OR (LastUpdatedDate IS NOT NULL AND LastUpdatedDate > '{1}'))) OR ", siteID, lastSyncDate);
                filterExpression = string.Format("(ItemID IN (SELECT ItemID FROM SiteItem WHERE SiteID = '{0}' AND IsDeleted = 0) AND (LastUpdatedDate IS NOT NULL AND LastUpdatedDate > '{1}'))", siteID, lastSyncDate);
            }
            else
                filterExpression = string.Format("ItemID IN (SELECT ItemID FROM SiteItem WHERE SiteID = '{0}' AND IsDeleted = 0)", siteID);
            if (rowCount > ListItemMaster.Count)
                filterExpression += string.Format(" AND ItemID <= {0}", ListItemMaster.Last().ItemID);
            List<ItemProduct> ListItemProduct = BusinessLayer.GetItemProductList(filterExpression, rowCountPerPage, pageIndex, "");
            foreach (ItemProduct entity in ListItemProduct)
                entity.OriginalValue = null;

            List<ItemTagField> ListItemTagField = BusinessLayer.GetItemTagFieldList(filterExpression, rowCountPerPage, pageIndex, "");
            foreach (ItemTagField entity in ListItemTagField)
                entity.OriginalValue = null;

            if (lastSyncDate.Year > 1900)
            {
                if (filterExpression != "")
                    filterExpression += " AND ";
                filterExpression = string.Format("SiteID = '{0}' AND (CreatedDate > '{1}' OR (LastUpdatedDate IS NOT NULL AND LastUpdatedDate > '{1}'))", siteID, lastSyncDate);
            }
            else
                filterExpression = string.Format("SiteID = '{0}'", siteID);
            if (rowCount > ListItemMaster.Count)
                filterExpression += string.Format(" AND ItemID <= {0}", ListItemMaster.Last().ItemID);

            List<ItemPlanning> ListItemPlanning = BusinessLayer.GetItemPlanningList(filterExpression, rowCountPerPage, pageIndex, "");
            foreach (ItemPlanning entity in ListItemPlanning)
                entity.OriginalValue = null;

            Object returnObj = new { ListItemMaster = ListItemMaster, ListItemProduct = ListItemProduct, ListItemTagField = ListItemTagField, ListItemPlanning = ListItemPlanning, Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), RowCount = rowCount };
            return new JavaScriptSerializer().Serialize(returnObj);
        }



        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string WaitMessage(string siteID)
        {
            return ClientAdapter.Instance.GetMessage(siteID);
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SendMessage(string siteID, string type)
        {
            ClientAdapter.Instance.SendMessage(siteID, type);
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Join(string siteID)
        {
            ClientAdapter.Instance.Join(siteID);
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Fork(string siteID)
        {
            ClientAdapter.Instance.Fork(siteID);
        }
    }
}
