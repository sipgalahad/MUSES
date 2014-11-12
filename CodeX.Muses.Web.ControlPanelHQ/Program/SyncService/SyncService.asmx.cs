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
using CodeX.Data.Core.Dal;

namespace CodeX.Muses.Web.ControlPanelHQ.Program
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
        #region Get From Server
        #region Sync Item
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetItemMasterList(Int32 DBSyncInfoID, String siteID, DateTime lastSyncDate, int pageIndex, int rowCountPerPage, int rowCount)
        {
            int maxRow = pageIndex * rowCountPerPage;
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

            string ListItemID = String.Join(",", ListItemMaster.Select(p => p.ItemID));
            if (lastSyncDate.Year > 1900)
            {
                filterExpression = "";
                if (rowCount > maxRow)
                    filterExpression += string.Format("ItemID IN ({0}) AND ", ListItemID);
                filterExpression += string.Format("((ItemID IN (SELECT ItemID FROM SiteItem WHERE SiteID = '{0}' AND IsDeleted = 0 AND CreatedDate > '{1}' OR (LastUpdatedDate IS NOT NULL AND LastUpdatedDate > '{1}'))) OR ", siteID, lastSyncDate);
                filterExpression += string.Format("(ItemID IN (SELECT ItemID FROM SiteItem WHERE SiteID = '{0}' AND IsDeleted = 0) AND (LastUpdatedDate IS NOT NULL AND LastUpdatedDate > '{1}')))", siteID, lastSyncDate);
            }
            else
                filterExpression = string.Format("ItemID IN ({0})", ListItemID);
            
            List<ItemProduct> ListItemProduct = BusinessLayer.GetItemProductList(filterExpression);
            foreach (ItemProduct entity in ListItemProduct)
                entity.OriginalValue = null;

            List<ItemTagField> ListItemTagField = BusinessLayer.GetItemTagFieldList(filterExpression);
            foreach (ItemTagField entity in ListItemTagField)
                entity.OriginalValue = null;

            if (lastSyncDate.Year > 1900)
            {
                filterExpression = "";
                if (rowCount > maxRow)
                    filterExpression += string.Format("ItemID IN ({0}) AND ", ListItemID);
                filterExpression += string.Format("(SiteID = '{0}' AND (CreatedDate > '{1}' OR (LastUpdatedDate IS NOT NULL AND LastUpdatedDate > '{1}')))", siteID, lastSyncDate);
            }
            else
                filterExpression = string.Format("SiteID = '{0}' AND ItemID IN ({1})", siteID, ListItemID);

            List<ItemPlanning> ListItemPlanning = BusinessLayer.GetItemPlanningList(filterExpression);
            foreach (ItemPlanning entity in ListItemPlanning)
                entity.OriginalValue = null;


            if (lastSyncDate.Year > 1900)
            {
                filterExpression = "";
                if (rowCount > maxRow)
                    filterExpression += string.Format("ItemID IN ({0}) AND ", ListItemID);
                filterExpression += string.Format("((ItemID IN (SELECT ItemID FROM SiteItem WHERE SiteID = '{0}' AND IsDeleted = 0 AND CreatedDate > '{1}' OR (LastUpdatedDate IS NOT NULL AND LastUpdatedDate > '{1}'))) OR ", siteID, lastSyncDate);
                filterExpression += string.Format("(ItemID IN (SELECT ItemID FROM SiteItem WHERE SiteID = '{0}' AND IsDeleted = 0) AND (CreatedDate > '{1}' OR (LastUpdatedDate IS NOT NULL AND LastUpdatedDate > '{1}'))))", siteID, lastSyncDate);
            }
            else
                filterExpression = string.Format("ItemID IN ({0})", ListItemID);

            List<ItemAlternateUnit> ListItemAlternateUnit = BusinessLayer.GetItemAlternateUnitList(filterExpression);
            foreach (ItemAlternateUnit entity in ListItemAlternateUnit)
                entity.OriginalValue = null;

            if (maxRow >= rowCount)
            {
                DBSyncInfoDt DBSyncInfo = BusinessLayer.GetDBSyncInfoDt(DBSyncInfoID, siteID);
                DBSyncInfo.LastSyncDate = DateTime.Now;
                BusinessLayer.UpdateDBSyncInfoDt(DBSyncInfo);
            }

            Object returnObj = new { ListItemMaster = ListItemMaster, ListItemProduct = ListItemProduct, ListItemTagField = ListItemTagField, ListItemPlanning = ListItemPlanning, ListItemAlternateUnit = ListItemAlternateUnit, Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), RowCount = rowCount };
            return new JavaScriptSerializer().Serialize(returnObj);
        }
        #endregion

        #region Sync Location
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetLocationList(Int32 DBSyncInfoID, String siteID, DateTime lastSyncDate, int pageIndex, int rowCountPerPage, int rowCount)
        {
            int maxRow = pageIndex * rowCountPerPage;
            string filterExpression = "";
            if (lastSyncDate.Year > 1900)
            {
                if (filterExpression != "")
                    filterExpression += " AND ";
                filterExpression += string.Format("SiteID = '{0}' AND (CreatedDate > '{1}' OR (LastUpdatedDate IS NOT NULL AND LastUpdatedDate > '{1}'))", siteID, lastSyncDate);
            }
            else
                filterExpression = string.Format("SiteID = '{0}'", siteID);
            if (rowCount < 0)
                rowCount = BusinessLayer.GetLocationRowCount(filterExpression);
            List<Location> ListLocation = BusinessLayer.GetLocationList(filterExpression, rowCountPerPage, pageIndex, "");
            foreach (Location entity in ListLocation)
                entity.OriginalValue = null;

            if (maxRow >= rowCount)
            {
                DBSyncInfoDt DBSyncInfo = BusinessLayer.GetDBSyncInfoDt(DBSyncInfoID, siteID);
                DBSyncInfo.LastSyncDate = DateTime.Now;
                BusinessLayer.UpdateDBSyncInfoDt(DBSyncInfo);
            }

            Object returnObj = new { ListLocation = ListLocation, Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), RowCount = rowCount };
            return new JavaScriptSerializer().Serialize(returnObj);
        }
        #endregion
        #endregion

        #region Post From Server
        #region Sync Item Transaction
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool PostItemTransaction(Int32 DBSyncInfoID, String siteID, DateTime lastSyncDate, List<vSyncItemTransactionHd> lstItemTransactionHd, List<vSyncItemTransactionDt> lstItemTransactionDt)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            try
            {
                string sqlInsertTempTable = "";
                string sqlInsert = "";
                string fieldName = "";
                string parameter = "";

                #region Item Transaction Hd
                Type type1 = typeof(vSyncItemTransactionHd);
                PropertyInfo[] propInfs = type1.GetProperties();
                fieldName = GetInsertObjectFieldName(propInfs, "");
                fieldName += ",ConsolidateID";

                if (lstItemTransactionHd.Count > 0)
                {
                    foreach (vSyncItemTransactionHd entity in lstItemTransactionHd)
                    {
                        string insertPerObj = GetInsertObjectValue(propInfs, entity);
                        insertPerObj += string.Format(",{0}", entity.TransactionID);
                        if (parameter != "")
                            parameter += ",";
                        parameter += string.Format("({0})", insertPerObj);
                    }

                    sqlInsertTempTable += "SELECT TOP 0 * INTO #TempTableItemTransactionHd FROM ItemTransactionHd;";
                    sqlInsertTempTable += string.Format("INSERT INTO #TempTableItemTransactionHd ");
                    sqlInsertTempTable += string.Format("({0}) ", fieldName);
                    sqlInsertTempTable += string.Format(" {0} ", "VALUES");
                    sqlInsertTempTable += string.Format("{0};", parameter);
                    sqlInsert += string.Format("INSERT INTO ItemTransactionHd ");
                    sqlInsert += string.Format("({0}) ", fieldName);
                    sqlInsert += string.Format("SELECT {0} FROM #TempTableItemTransactionHd WHERE ConsolidateID NOT IN (SELECT ConsolidateID FROM ItemTransactionHd WHERE ConsolidateID IS NOT NULL);", fieldName);

                    sqlInsert += string.Format("UPDATE a SET {0} ", GetUpdateObjectFieldName(propInfs, "a", "b"));
                    sqlInsert += string.Format("FROM ItemTransactionHd a INNER JOIN [#TempTableItemTransactionHd] b ON a.ConsolidateID = b.ConsolidateID;");
                    sqlInsert += string.Format("DROP TABLE #TempTableItemTransactionHd;");
                }
                #endregion

                #region Item Transaction Dt
                if (lstItemTransactionDt.Count > 0)
                {
                    fieldName = "";
                    parameter = "";
                    Type type2 = typeof(vSyncItemTransactionDt);
                    propInfs = type2.GetProperties();
                    fieldName = GetInsertObjectFieldName(propInfs, "");

                    foreach (vSyncItemTransactionDt entity in lstItemTransactionDt)
                    {
                        string insertPerObj = GetInsertObjectValue(propInfs, entity);
                        if (parameter != "")
                            parameter += ",";
                        parameter += string.Format("({0})", insertPerObj);
                    }

                    sqlInsertTempTable += "SELECT TOP 0 * INTO #TempTableItemTransactionDt FROM ItemTransactionDt;";
                    sqlInsertTempTable += string.Format("INSERT INTO #TempTableItemTransactionDt ");
                    sqlInsertTempTable += string.Format("({0}) ", fieldName);
                    sqlInsertTempTable += string.Format(" {0} ", "VALUES");
                    sqlInsertTempTable += string.Format("{0};", parameter);

                    fieldName = GetInsertObjectFieldName(propInfs, "a.");
                    sqlInsert += string.Format("UPDATE a SET {0} ", GetUpdateObjectFieldName(propInfs, "a", "b").Replace("a.TransactionID = b.TransactionID,", ""));
                    sqlInsert += string.Format("FROM ItemTransactionDt a INNER JOIN ItemTransactionHd c ON c.TransactionID = a.TransactionID INNER JOIN [#TempTableItemTransactionDt] b ON c.ConsolidateID = b.TransactionID;");
                    sqlInsert += string.Format("INSERT ItemTransactionDt SELECT {0} FROM #TempTableItemTransactionDt a INNER JOIN ItemTransactionHd im ON im.ConsolidateID = a.TransactionID WHERE a.TransactionID NOT IN (SELECT ConsolidateID FROM ItemTransactionHd im INNER JOIN ItemTransactionDt ip ON ip.TransactionID = im.TransactionID AND ConsolidateID IS NOT NULL);", fieldName.Replace("a.TransactionID", "im.TransactionID"));

                    sqlInsert += string.Format("DROP TABLE #TempTableItemTransactionDt;");
                }
                #endregion

                sqlInsert += string.Format("UPDATE DBSyncInfoDt SET LastSyncDate = '{0}' WHERE DBSyncInfoID = {1} AND SiteID = '{2}';", DateTime.Now, DBSyncInfoID, siteID);
                ctx.CommandText = sqlInsertTempTable;
                DaoBase.ExecuteNonQuery(ctx);

                ctx.CommandText = sqlInsert;
                DaoBase.ExecuteNonQuery(ctx);

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                //errMessage = ex.Message;
                ctx.RollBackTransaction();
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion

        #region Utility
        private static string GetUpdateObjectFieldName(PropertyInfo[] propInfs, string tableName1, string tableName2)
        {
            string fieldName = "";
            foreach (PropertyInfo prop in propInfs)
            {
                object[] custAttr = prop.GetCustomAttributes(false);
                foreach (Attribute attrib in custAttr)
                {
                    ColumnAttribute schema = attrib as ColumnAttribute;
                    if (schema != null && !schema.IsComputed && !schema.IsPrimaryKey && !schema.IsIdentity && !schema.IsTimeStamp)
                    {
                        if (fieldName != "")
                            fieldName += ",";
                        fieldName += string.Format("{1}.{0} = {2}.{0}", schema.Name, tableName1, tableName2);
                    }
                }
            }
            return fieldName;
        }

        private static string GetInsertObjectFieldName(PropertyInfo[] propInfs, string prefix)
        {
            string fieldName = "";
            foreach (PropertyInfo prop in propInfs)
            {
                object[] custAttr = prop.GetCustomAttributes(false);
                foreach (Attribute attrib in custAttr)
                {
                    ColumnAttribute schema = attrib as ColumnAttribute;
                    if (schema != null && !schema.IsComputed && !schema.IsIdentity && !schema.IsTimeStamp)
                    {
                        if (fieldName != "")
                            fieldName += ",";
                        fieldName += string.Format("{0}{1}", prefix, schema.Name);
                    }
                }
            }
            return fieldName;
        }

        private static string GetInsertObjectValue(PropertyInfo[] propInfs, object entity)
        {
            string insertPerObj = "";
            foreach (PropertyInfo prop in propInfs)
            {
                object[] custAttr = prop.GetCustomAttributes(false);
                foreach (Attribute attrib in custAttr)
                {
                    ColumnAttribute schema = attrib as ColumnAttribute;
                    if (schema != null && !schema.IsComputed && !schema.IsIdentity && !schema.IsTimeStamp)
                    {
                        object fieldValue = prop.GetValue(entity, null);
                        if (!schema.IsNullable)
                            fieldValue = CheckIsNull(fieldValue, prop.PropertyType);

                        if (insertPerObj != "")
                            insertPerObj += ",";
                        if (schema.IsNullable && (fieldValue == "" || fieldValue == null || fieldValue.ToString() == "0"))
                            insertPerObj += "NULL";
                        else
                            insertPerObj += string.Format("'{0}'", fieldValue);
                    }
                }
            }
            return insertPerObj;
        }
        private static object CheckIsNull(object obj, Type type)
        {
            if (type.FullName.Contains("DateTime"))
            {
                if (obj is DBNull || obj == null)
                    return Convert.ToDateTime("1900-01-01");
                if (Convert.ToDateTime(obj).Year < 1900)
                    return Convert.ToDateTime("1900-01-01");
            }
            else if (obj is DBNull || obj == null)
            {
                if (type.FullName.Contains("String")) return string.Empty;
                //if (type.FullName.Contains("Int64")) return 0;
                if (type.FullName.Contains("Boolean")) return false;
                return null;
            }
            return obj;
        }
        #endregion
        #endregion

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
