using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CodeX.DesktopTools.Properties;
using System.Diagnostics;
using System.Threading;
using CodeX.Data.Model;
using CodeX.Common;
using CodeX.Data.Core.Dal;
using System.Reflection;
using System.Web.Script.Serialization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace CodeX.DesktopTools
{
    public class SyncProcess
    {
        public static bool Sync(SyncService.SyncServiceSoapClient client, string siteID, string syncType)
        {
            vDBSyncInfoDt syncInfo = BusinessLayer.GetvDBSyncInfoDtList(string.Format("DBSyncInfoCode = '{0}' AND SiteID = '{1}'", syncType, siteID))[0];
            if (syncType == Constant.DBSyncInfoCode.ITEM)
                return SyncItem(client, siteID, syncInfo);
            else if (syncType == Constant.DBSyncInfoCode.ITEM_TRANSACTION)
                return SyncItemTransaction(client, siteID, syncInfo);
            return false;
        }

        #region Post To Server
        #region Item Transaction
        public static bool SyncItemTransaction(SyncService.SyncServiceSoapClient client, string siteID, vDBSyncInfoDt syncInfo)
        {
            string filterExpression = "";
            if (syncInfo.LastSyncDate.Year > 1900)
                filterExpression = string.Format("CreatedDate > '{0}' OR (LastUpdatedDate IS NOT NULL AND LastUpdatedDate > '{0}')", syncInfo.LastSyncDate);
            else
                filterExpression = "";
            int rowCount = BusinessLayer.GetItemTransactionHdRowCount(filterExpression);

            decimal totalPageCount = Math.Ceiling((decimal)rowCount /  syncInfo.RowCount);
            for (int i = 1; i <= totalPageCount; ++i)
            {
                List<vSyncItemTransactionHd> lstItemTransactionHd = BusinessLayer.GetvSyncItemTransactionHdList(filterExpression, syncInfo.RowCount, i);

                string filterExpressionDt = string.Format("TransactionID IN ({0})", String.Join(",", lstItemTransactionHd.Select(p => p.TransactionID).ToList()));
                List<vSyncItemTransactionDt> lstItemTransactionDt = BusinessLayer.GetvSyncItemTransactionDtList(filterExpression);
                SyncService.ArrayOfVSyncItemTransactionHd lstHd = new SyncService.ArrayOfVSyncItemTransactionHd();
                SyncService.ArrayOfVSyncItemTransactionDt lstDt = new SyncService.ArrayOfVSyncItemTransactionDt();

                foreach (vSyncItemTransactionHd entityHd in lstItemTransactionHd)
                {
                    SyncService.vSyncItemTransactionHd entityHdNew = new SyncService.vSyncItemTransactionHd();
                    CopyObject(entityHd, ref entityHdNew);
                    lstHd.Add(entityHdNew);
                }
                foreach (vSyncItemTransactionDt entityDt in lstItemTransactionDt)
                {
                    SyncService.vSyncItemTransactionDt entityDtNew = new SyncService.vSyncItemTransactionDt();
                    CopyObject(entityDt, ref entityDtNew);
                    lstDt.Add(entityDtNew);
                }

                client.PostItemTransaction(siteID, syncInfo.LastSyncDate, lstHd, lstDt);
            }
            DBSyncInfoDt syncInfoDt = BusinessLayer.GetDBSyncInfoDt(syncInfo.DBSyncInfoID, siteID);
            syncInfoDt.LastSyncDate = DateTime.Now;
            BusinessLayer.UpdateDBSyncInfoDt(syncInfoDt);
            return true;
        }
        #endregion

        #region Utility
        public static void CopyObject<T>(object sourceObject, ref T destObject)
        {
            //	If either the source, or destination is null, return
            if (sourceObject == null || destObject == null)
                return;

            //	Get the type of each object
            Type sourceType = sourceObject.GetType();
            Type targetType = destObject.GetType();

            //	Loop through the source properties
            foreach (PropertyInfo p in sourceType.GetProperties())
            {
                //	Get the matching property in the destination object
                PropertyInfo targetObj = targetType.GetProperty(p.Name);
                //	If there is none, skip
                if (targetObj == null)
                    continue;

                //	Set the value in the destination
                targetObj.SetValue(destObject, p.GetValue(sourceObject, null), null);
            }
        }
        #endregion
        #endregion

        #region Get From Server
        #region Item
        class CResultItem
        {
            public List<ItemMaster> ListItemMaster { get; set; }
            public List<ItemProduct> ListItemProduct { get; set; }
            public List<ItemTagField> ListItemTagField { get; set; }
            public List<ItemPlanning> ListItemPlanning { get; set; }
            public List<ItemAlternateUnit> ListItemAlternateUnit { get; set; }
            public String TimeStamp { get; set; }
            public Int32 RowCount { get; set; }
        }
        public static bool SyncItem(SyncService.SyncServiceSoapClient client, string siteID, vDBSyncInfoDt syncInfo)
        {
            bool result = true;

            //object serviceResult = client.GetMobileListObject("GetItemMasterList", "");
            //JavaScriptSerializer jss = new JavaScriptSerializer();
            //var tempResult = (IDictionary<string, object>)jss.DeserializeObject(serviceResult.ToString());
            //Object[] lstObj = (Object[])tempResult["ReturnObj"];
            //foreach (Object obj in lstObj)
            //{
            //    var temp = (IDictionary<string, object>)obj;
            //    string test = temp["ItemID"].ToString();
            //    IEnumerable<string> keys = temp.Select(x => x.Key);
            //}

            
            int rowCountPerPage = syncInfo.RowCount;

            int rowCount = -1;
            object serviceResult = client.GetItemMasterList(siteID, syncInfo.LastSyncDate, 1, rowCountPerPage, rowCount);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            CResultItem tempResult = jss.Deserialize<CResultItem>(serviceResult.ToString());
            List<ItemMaster> lstItemMaster = tempResult.ListItemMaster;
            List<ItemProduct> lstItemProduct = tempResult.ListItemProduct;
            List<ItemTagField> lstItemTagField = tempResult.ListItemTagField;
            List<ItemPlanning> lstItemPlanning = tempResult.ListItemPlanning;
            List<ItemAlternateUnit> lstItemAlternateUnit = tempResult.ListItemAlternateUnit;

            rowCount = tempResult.RowCount;
            decimal totalPageCount = Math.Ceiling((decimal)rowCount / rowCountPerPage);

            if (totalPageCount == 0)
            {
                if (lstItemPlanning.Count > 0 || lstItemAlternateUnit.Count > 0)
                    totalPageCount = 1;
            }
            for (int i = 1; i <= totalPageCount; ++i)
            {
                EventViewerHelper.SendMessageToEventViewer(Constant.DBSyncInfoCode.ITEM, "Sync", i.ToString(), "Waiting");
            }
            
            for (int i = 1; i <= totalPageCount; ++i)
            {
                EventViewerHelper.SendMessageToEventViewer(Constant.DBSyncInfoCode.ITEM, "Sync", i.ToString(), "Get Server Data");
                if (i > 1)
                {
                    serviceResult = client.GetItemMasterList(siteID, syncInfo.LastSyncDate, i, rowCountPerPage, rowCount);
                    jss = new JavaScriptSerializer();
                    tempResult = jss.Deserialize<CResultItem>(serviceResult.ToString());
                    lstItemMaster = tempResult.ListItemMaster;
                    lstItemProduct = tempResult.ListItemProduct;
                    lstItemTagField = tempResult.ListItemTagField;
                    lstItemPlanning = tempResult.ListItemPlanning;
                    lstItemAlternateUnit = tempResult.ListItemAlternateUnit;
                }

                IDbContext ctx = DbFactory.Configure(true);
                try
                {
                    string sqlInsertTempTable = "";
                    string sqlInsert = "";
                    string fieldName = "";
                    string parameter = "";

                    #region Item Master
                    Type type1 = typeof(ItemMaster);
                    PropertyInfo[] propInfs = type1.GetProperties();
                    fieldName = GetInsertObjectFieldName(propInfs, "");
                    fieldName += ",ConsolidateID";

                    EventViewerHelper.SendMessageToEventViewer(Constant.DBSyncInfoCode.ITEM, "Sync", i.ToString(), "Sync Item Master");
                    if (lstItemMaster.Count > 0)
                    {
                        foreach (ItemMaster entity in lstItemMaster)
                        {
                            string insertPerObj = GetInsertObjectValue(propInfs, entity);
                            insertPerObj += string.Format(",{0}", entity.ItemID);
                            if (parameter != "")
                                parameter += ",";
                            parameter += string.Format("({0})", insertPerObj);
                        }

                        sqlInsertTempTable += "SELECT TOP 0 * INTO #TempTableItemMaster FROM ItemMaster;";
                        sqlInsertTempTable += string.Format("INSERT INTO #TempTableItemMaster ");
                        sqlInsertTempTable += string.Format("({0}) ", fieldName);
                        sqlInsertTempTable += string.Format(" {0} ", "VALUES");
                        sqlInsertTempTable += string.Format("{0};", parameter);
                        sqlInsert += string.Format("INSERT INTO ItemMaster ");
                        sqlInsert += string.Format("({0}) ", fieldName);
                        sqlInsert += string.Format("SELECT {0} FROM #TempTableItemMaster WHERE ConsolidateID NOT IN (SELECT ConsolidateID FROM ItemMaster);", fieldName);

                        sqlInsert += string.Format("UPDATE a SET {0} ", GetUpdateObjectFieldName(propInfs, "a", "b"));
                        sqlInsert += string.Format("FROM ItemMaster a INNER JOIN [#TempTableItemMaster] b ON a.ConsolidateID = b.ConsolidateID;");
                        sqlInsert += string.Format("DROP TABLE #TempTableItemMaster;");
                    }
                    #endregion

                    #region Item Product
                    EventViewerHelper.SendMessageToEventViewer(Constant.DBSyncInfoCode.ITEM, "Sync", i.ToString(), "Sync Item Product");
                    if (lstItemProduct.Count > 0)
                    {
                        fieldName = "";
                        parameter = "";
                        Type type2 = typeof(ItemProduct);
                        propInfs = type2.GetProperties();
                        fieldName = GetInsertObjectFieldName(propInfs, "");

                        foreach (ItemProduct entity in lstItemProduct)
                        {
                            string insertPerObj = GetInsertObjectValue(propInfs, entity);
                            if (parameter != "")
                                parameter += ",";
                            parameter += string.Format("({0})", insertPerObj);
                        }

                        sqlInsertTempTable += "SELECT TOP 0 * INTO #TempTableItemProduct FROM ItemProduct;";
                        sqlInsertTempTable += string.Format("INSERT INTO #TempTableItemProduct ");
                        sqlInsertTempTable += string.Format("({0}) ", fieldName);
                        sqlInsertTempTable += string.Format(" {0} ", "VALUES");
                        sqlInsertTempTable += string.Format("{0};", parameter);

                        fieldName = GetInsertObjectFieldName(propInfs, "a.");
                        sqlInsert += string.Format("UPDATE a SET {0} ", GetUpdateObjectFieldName(propInfs, "a", "b").Replace("a.ItemID = b.ItemID,", ""));
                        sqlInsert += string.Format("FROM ItemProduct a INNER JOIN ItemMaster c ON c.ItemID = a.ItemID INNER JOIN [#TempTableItemProduct] b ON c.ConsolidateID = b.ItemID;");
                        sqlInsert += string.Format("INSERT ItemProduct SELECT {0} FROM #TempTableItemProduct a INNER JOIN ItemMaster im ON im.ConsolidateID = a.ItemID WHERE a.ItemID NOT IN (SELECT ConsolidateID FROM ItemMaster im INNER JOIN ItemProduct ip ON ip.ItemID = im.ItemID);", fieldName.Replace("a.ItemID", "im.ItemID"));

                        sqlInsert += string.Format("DROP TABLE #TempTableItemProduct;");
                    }
                    #endregion

                    #region Item Tag Field
                    EventViewerHelper.SendMessageToEventViewer(Constant.DBSyncInfoCode.ITEM, "Sync", i.ToString(), "Sync Item Tag Field");
                    if (lstItemTagField.Count > 0)
                    {
                        fieldName = "";
                        parameter = "";
                        Type type2 = typeof(ItemTagField);
                        propInfs = type2.GetProperties();
                        fieldName = GetInsertObjectFieldName(propInfs, "");

                        foreach (ItemTagField entity in lstItemTagField)
                        {
                            string insertPerObj = GetInsertObjectValue(propInfs, entity);
                            if (parameter != "")
                                parameter += ",";
                            parameter += string.Format("({0})", insertPerObj);
                        }

                        sqlInsertTempTable += "SELECT TOP 0 * INTO #TempTableItemTagField FROM ItemTagField;";
                        sqlInsertTempTable += string.Format("INSERT INTO #TempTableItemTagField ");
                        sqlInsertTempTable += string.Format("({0}) ", fieldName);
                        sqlInsertTempTable += string.Format(" {0} ", "VALUES");
                        sqlInsertTempTable += string.Format("{0};", parameter);

                        fieldName = GetInsertObjectFieldName(propInfs, "a.");
                        sqlInsert += string.Format("UPDATE a SET {0} ", GetUpdateObjectFieldName(propInfs, "a", "b").Replace("a.ItemID = b.ItemID,", ""));
                        sqlInsert += string.Format("FROM ItemTagField a INNER JOIN ItemMaster c ON c.ItemID = a.ItemID INNER JOIN [#TempTableItemTagField] b ON c.ConsolidateID = b.ItemID;");
                        sqlInsert += string.Format("INSERT ItemTagField SELECT {0} FROM #TempTableItemTagField a INNER JOIN ItemMaster im ON im.ConsolidateID = a.ItemID WHERE a.ItemID NOT IN (SELECT ConsolidateID FROM ItemMaster im INNER JOIN ItemTagField ip ON ip.ItemID = im.ItemID);", fieldName.Replace("a.ItemID", "im.ItemID"));

                        sqlInsert += string.Format("DROP TABLE #TempTableItemTagField;");
                    }
                    #endregion

                    #region Item Planning
                    EventViewerHelper.SendMessageToEventViewer(Constant.DBSyncInfoCode.ITEM, "Sync", i.ToString(), "Sync Item Tag Planning");
                    if (lstItemPlanning.Count > 0)
                    {
                        fieldName = "";
                        parameter = "";
                        Type type2 = typeof(ItemPlanning);
                        propInfs = type2.GetProperties();
                        fieldName = GetInsertObjectFieldName(propInfs, "");

                        foreach (ItemPlanning entity in lstItemPlanning)
                        {
                            string insertPerObj = GetInsertObjectValue(propInfs, entity);
                            if (parameter != "")
                                parameter += ",";
                            parameter += string.Format("({0})", insertPerObj);
                        }

                        sqlInsertTempTable += "SELECT TOP 0 * INTO #TempTableItemPlanning FROM ItemPlanning;";
                        sqlInsertTempTable += string.Format("INSERT INTO #TempTableItemPlanning ");
                        sqlInsertTempTable += string.Format("({0}) ", fieldName);
                        sqlInsertTempTable += string.Format(" {0} ", "VALUES");
                        sqlInsertTempTable += string.Format("{0};", parameter);

                        fieldName = GetInsertObjectFieldName(propInfs, "a.");
                        sqlInsert += string.Format("UPDATE a SET {0} ", GetUpdateObjectFieldName(propInfs, "a", "b").Replace("a.ItemID = b.ItemID,", ""));
                        sqlInsert += string.Format("FROM ItemPlanning a INNER JOIN ItemMaster c ON c.ItemID = a.ItemID INNER JOIN [#TempTableItemPlanning] b ON c.ConsolidateID = b.ItemID;");
                        sqlInsert += string.Format("INSERT ItemPlanning SELECT {0} FROM #TempTableItemPlanning a INNER JOIN ItemMaster im ON im.ConsolidateID = a.ItemID WHERE a.ItemID NOT IN (SELECT ConsolidateID FROM ItemMaster im INNER JOIN ItemPlanning ip ON ip.ItemID = im.ItemID);", fieldName.Replace("a.ItemID", "im.ItemID"));

                        sqlInsert += string.Format("DROP TABLE #TempTableItemPlanning;");
                    }
                    #endregion

                    #region Item Alternate Unit
                    EventViewerHelper.SendMessageToEventViewer(Constant.DBSyncInfoCode.ITEM, "Sync", i.ToString(), "Sync Item Alternate Unit");
                    if (lstItemAlternateUnit.Count > 0)
                    {
                        fieldName = "";
                        parameter = "";
                        Type type2 = typeof(ItemAlternateUnit);
                        propInfs = type2.GetProperties();
                        fieldName = GetInsertObjectFieldName(propInfs, "a.");
                        fieldName += ",a.ConsolidateID";

                        foreach (ItemAlternateUnit entity in lstItemAlternateUnit)
                        {
                            string insertPerObj = GetInsertObjectValue(propInfs, entity);
                            insertPerObj += string.Format(",{0}", entity.ID);
                            if (parameter != "")
                                parameter += ",";
                            parameter += string.Format("({0})", insertPerObj);
                        }

                        sqlInsertTempTable += "SELECT TOP 0 * INTO #TempTableItemAlternateUnit FROM ItemAlternateUnit;";
                        sqlInsertTempTable += string.Format("INSERT INTO #TempTableItemAlternateUnit ");
                        sqlInsertTempTable += string.Format("({0}) ", fieldName);
                        sqlInsertTempTable += string.Format(" {0} ", "VALUES");
                        sqlInsertTempTable += string.Format("{0};", parameter);

                        sqlInsert += string.Format("UPDATE a SET {0} ", GetUpdateObjectFieldName(propInfs, "a", "b").Replace("a.ItemID = b.ItemID,", ""));
                        sqlInsert += string.Format("FROM ItemAlternateUnit a INNER JOIN [#TempTableItemAlternateUnit] b ON a.ConsolidateID = b.ConsolidateID;");
                        sqlInsert += string.Format("INSERT INTO ItemAlternateUnit ");
                        sqlInsert += string.Format("({0}) ", fieldName);
                        sqlInsert += string.Format("SELECT {0} FROM #TempTableItemAlternateUnit a INNER JOIN ItemMaster im ON im.ConsolidateID = a.ItemID WHERE a.ConsolidateID NOT IN (SELECT ConsolidateID FROM ItemAlternateUnit);", fieldName.Replace("a.ItemID", "im.ItemID"));
                        //sqlInsert += string.Format("INSERT ItemProduct 
                        //SELECT {0} FROM #TempTableItemProduct a INNER JOIN ItemMaster im ON im.ConsolidateID = a.ItemID WHERE a.ItemID NOT IN (SELECT ConsolidateID FROM ItemMaster im INNER JOIN ItemProduct ip ON ip.ItemID = im.ItemID);", fieldName.Replace("a.ItemID", "im.ItemID"));
                        
                        sqlInsert += string.Format("DROP TABLE #TempTableItemAlternateUnit;");
                    }
                    #endregion

                    EventViewerHelper.SendMessageToEventViewer(Constant.DBSyncInfoCode.ITEM, "Sync", i.ToString(), "Insert Into Temp Table");
                    ctx.CommandText = sqlInsertTempTable;
                    DaoBase.ExecuteNonQuery(ctx);

                    sqlInsert += string.Format("INSERT SiteItem SELECT '{0}',ItemID,0,{1},GETDATE(),{1},GETDATE() FROM ItemMaster WHERE ItemID NOT IN (SELECT ItemID FROM SiteItem);", siteID, 0);
                    sqlInsert += string.Format("UPDATE DBSyncInfoDt SET LastSyncDate = '{0}' WHERE DBSyncInfoID = {1} AND SiteID = '{2}';", tempResult.TimeStamp, syncInfo.DBSyncInfoID, siteID);

                    EventViewerHelper.SendMessageToEventViewer(Constant.DBSyncInfoCode.ITEM, "Sync", i.ToString(), "Update Table");
                    ctx.CommandText = sqlInsert;
                    DaoBase.ExecuteNonQuery(ctx);

                    EventViewerHelper.SendMessageToEventViewer(Constant.DBSyncInfoCode.ITEM, "Sync", i.ToString(), "Done");
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
                        if (schema.IsNullable && (fieldValue == "" || fieldValue == null))
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
    }
}
