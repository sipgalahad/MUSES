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

namespace CodeX.DesktopTools
{
    public class SyncProcess
    {
        public static bool Sync(SyncService.SyncServiceSoapClient client, string siteID, string syncType)
        {
            if (syncType == Constant.BusinessObjectType.ITEM)
                return SyncItem(client, siteID);
            return false;
        }

        class CResult
        {
            public List<ItemMaster> ListItemMaster { get; set; }
            public List<ItemProduct> ListItemProduct { get; set; }
            public List<ItemTagField> ListItemTagField { get; set; }
            public List<ItemPlanning> ListItemPlanning { get; set; }
            public String TimeStamp { get; set; }
            public Int32 RowCount { get; set; }
        }
        public static bool SyncItem(SyncService.SyncServiceSoapClient client, string siteID)
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

            int rowCountPerPage = 4;
            DBSyncInfo syncInfo = BusinessLayer.GetDBSyncInfo(Constant.BusinessObjectType.ITEM, siteID);

            int rowCount = -1;
            object serviceResult = client.GetItemMasterList(siteID, syncInfo.LastSyncDate, 1, rowCountPerPage, rowCount);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            CResult tempResult = jss.Deserialize<CResult>(serviceResult.ToString());
            List<ItemMaster> lstItemMaster = tempResult.ListItemMaster;
            List<ItemProduct> lstItemProduct = tempResult.ListItemProduct;
            List<ItemTagField> lstItemTagField = tempResult.ListItemTagField;
            List<ItemPlanning> lstItemPlanning = tempResult.ListItemPlanning;

            rowCount = tempResult.RowCount;
            decimal totalPageCount = Math.Ceiling((decimal)rowCount / rowCountPerPage);
            for (int i = 1; i <= totalPageCount; ++i)
            {
                if (i > 1)
                {
                    serviceResult = client.GetItemMasterList(siteID, syncInfo.LastSyncDate, i, rowCountPerPage, rowCount);
                    jss = new JavaScriptSerializer();
                    tempResult = jss.Deserialize<CResult>(serviceResult.ToString());
                    lstItemMaster = tempResult.ListItemMaster;
                    lstItemProduct = tempResult.ListItemProduct;
                    lstItemTagField = tempResult.ListItemTagField;
                    lstItemPlanning = tempResult.ListItemPlanning;
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
                        sqlInsert += string.Format("UPDATE a SET {0} ", GetUpdateObjectFieldName(propInfs, "a", "b"));
                        sqlInsert += string.Format("FROM ItemProduct a INNER JOIN ItemMaster c ON c.ItemID = a.ItemID INNER JOIN [#TempTableItemProduct] b ON c.ConsolidateID = b.ItemID;");
                        sqlInsert += string.Format("INSERT ItemProduct SELECT {0} FROM #TempTableItemProduct a INNER JOIN ItemMaster im ON im.ConsolidateID = a.ItemID WHERE a.ItemID NOT IN (SELECT ConsolidateID FROM ItemMaster im INNER JOIN ItemProduct ip ON ip.ItemID = im.ItemID);", fieldName.Replace("a.ItemID", "im.ItemID"));

                        sqlInsert += string.Format("DROP TABLE #TempTableItemProduct;");
                    }
                    #endregion

                    #region Item Tag Field
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
                        sqlInsert += string.Format("UPDATE a SET {0} ", GetUpdateObjectFieldName(propInfs, "a", "b"));
                        sqlInsert += string.Format("FROM ItemTagField a INNER JOIN ItemMaster c ON c.ItemID = a.ItemID INNER JOIN [#TempTableItemTagField] b ON c.ConsolidateID = b.ItemID;");
                        sqlInsert += string.Format("INSERT ItemTagField SELECT {0} FROM #TempTableItemTagField a INNER JOIN ItemMaster im ON im.ConsolidateID = a.ItemID WHERE a.ItemID NOT IN (SELECT ConsolidateID FROM ItemMaster im INNER JOIN ItemTagField ip ON ip.ItemID = im.ItemID);", fieldName.Replace("a.ItemID", "im.ItemID"));

                        sqlInsert += string.Format("DROP TABLE #TempTableItemTagField;");
                    }
                    #endregion

                    #region Item Planning
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
                        sqlInsert += string.Format("UPDATE a SET {0} ", GetUpdateObjectFieldName(propInfs, "a", "b"));
                        sqlInsert += string.Format("FROM ItemPlanning a INNER JOIN ItemMaster c ON c.ItemID = a.ItemID INNER JOIN [#TempTableItemPlanning] b ON c.ConsolidateID = b.ItemID;");
                        sqlInsert += string.Format("INSERT ItemPlanning SELECT {0} FROM #TempTableItemPlanning a INNER JOIN ItemMaster im ON im.ConsolidateID = a.ItemID WHERE a.ItemID NOT IN (SELECT ConsolidateID FROM ItemMaster im INNER JOIN ItemPlanning ip ON ip.ItemID = im.ItemID);", fieldName.Replace("a.ItemID", "im.ItemID"));

                        sqlInsert += string.Format("DROP TABLE #TempTableItemPlanning;");
                    }
                    #endregion
                    sqlInsert = sqlInsertTempTable + sqlInsert;
                    sqlInsert += string.Format("INSERT SiteItem SELECT '{0}',ItemID,0,{1},GETDATE(),{1},GETDATE() FROM ItemMaster WHERE ItemID NOT IN (SELECT ItemID FROM SiteItem);", siteID, 0);
                    sqlInsert += string.Format("UPDATE DBSyncInfo SET LastSyncDate = '{0}' WHERE GCBusinessObjectType = '{1}' AND SiteID = '{2}';", tempResult.TimeStamp, Constant.BusinessObjectType.ITEM, siteID);

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
            }
            return result;
        }

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
    }
}
