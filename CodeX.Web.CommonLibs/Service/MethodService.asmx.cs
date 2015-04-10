using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Reflection;
using CodeX.Data.Model;
using System.Collections;
using System.Data;
using CodeX.Web.Common;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using CodeX.Web.Common.UI;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using CodeX.Common;
using CodeX.Data.Core.Dal;

namespace CodeX.Web.CommonLibs.Service
{
    /// <summary>
    /// Summary description for MethodService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class MethodService : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public object GetSessionValue(string sessionName)
        {
            return Session[sessionName];
        }

        #region GetItemMasterPurchase
        [WebMethod(EnableSession = true)]
        public object GetItemMasterPurchase(int itemID, int businessPartnerID)
        {
            return BusinessLayer.GetItemMasterPurchaseList(AppSession.UserLogin.SiteID, itemID, businessPartnerID).FirstOrDefault();
        }
        #endregion

        #region GetItemMasterSales
        [WebMethod(EnableSession = true)]
        public object GetItemMasterSales(string itemCode, int studentID, int locationID, int type, DateTime transactionDate, string additionalFilterExpression)
        {
            string filterExpression = string.Format("ItemCode = '{0}'", itemCode);
            if (additionalFilterExpression != "")
                filterExpression += string.Format(" AND {0}", additionalFilterExpression);
            ItemMaster item = BusinessLayer.GetItemMasterList(filterExpression).FirstOrDefault();
            if (item == null)
                return null;
            return BusinessLayer.GetItemMasterSalesList(AppSession.UserLogin.SiteID, item.ItemID, studentID, locationID, type, transactionDate).FirstOrDefault();
        }
        #endregion

        #region GetItemQtyOnOrder
        [WebMethod(EnableSession = true)]
        public object GetItemQtyOnOrder(int itemID, int locationID, int type)
        {
            return BusinessLayer.GetItemQtyOnOrder(itemID, locationID, type).FirstOrDefault();
        }
        #endregion

        #region Get Object
        [WebMethod()]
        public object GetLimitListObject(string methodName, string filterExpression, int pageCount)
        {
            MethodInfo method = typeof(BusinessLayer).GetMethod(methodName, new[] { typeof(string), typeof(int), typeof(int), typeof(string) });
            if (method != null)
            {
                object obj = method.Invoke(null, new object[] { filterExpression, pageCount, 1, "" });
                IList list = (IList)obj;
                return list;
            }
            return GetListObject(methodName, filterExpression);
        }
        [WebMethod()]
        public object GetLimitListObject2(string methodName, string filterExpression, int pageCount, string orderByExpression)
        {
            MethodInfo method = typeof(BusinessLayer).GetMethod(methodName, new[] { typeof(string), typeof(int), typeof(int), typeof(string) });
            if (method != null)
            {
                object obj = method.Invoke(null, new object[] { filterExpression, pageCount, 1, orderByExpression });
                IList list = (IList)obj;
                return list;
            }
            return GetListObject(methodName, filterExpression);
        }

        [WebMethod()]
        public object GetListObject(string methodName, string filterExpression)
        {
            MethodInfo method = typeof(BusinessLayer).GetMethod(methodName, new[] { typeof(string) });
            object obj = method.Invoke(null, new string[] { filterExpression });
            IList list = (IList)obj;
            return list;
        }

        [WebMethod()]
        public object GetObject(string methodName, string filterExpression)
        {
            MethodInfo method = typeof(BusinessLayer).GetMethod(methodName, new[] { typeof(string) });
            object obj = method.Invoke(null, new string[] { filterExpression });
            IList list = (IList)obj;
            if (list.Count > 0)
                return list[0];
            return null;
        }

        [WebMethod()]
        public object GetValue(string methodName, string filterExpression)
        {
            MethodInfo method = typeof(BusinessLayer).GetMethod(methodName, new[] { typeof(string) });
            object obj = method.Invoke(null, new string[] { filterExpression });
            return obj;
        }

        [WebMethod()]
        public object GetObjectValue(string methodName, string filterExpression, string returnField)
        {
            MethodInfo method = typeof(BusinessLayer).GetMethod(methodName, new[] { typeof(string) });
            object tempObj = method.Invoke(null, new string[] { filterExpression });
            IList list = (IList)tempObj;
            if (list.Count > 0)
            {
                object obj = list[0];
                return obj.GetType().GetProperty(returnField).GetValue(obj, null);
            }
            return "";
        }

        [WebMethod(EnableSession = true)]
        public object GetObjectFromSession(string sessionName, string filterBy, object filterValue)
        {
            //Type t = Type.GetType("MyPersonClass");
            //Type listDataType = typeof(List<>).MakeGenericType(t);

            //list = Convert.ChangeType(HttpContext.Current.Session[sessionName], listDataType);



            IList list = (IList)HttpContext.Current.Session[sessionName];
            //Type type = list.GetType().GetGenericArguments()[0];

            //return list.FirstOrDefault(o => o.GetType().GetProperty(filterBy).GetValue(o, null).ToString() == filterValue.ToString());

            foreach (object obj in list)
            {
                object val = obj.GetType().GetProperty(filterBy).GetValue(obj, null);
                if (val.ToString() == filterValue.ToString())
                {
                    //return type.GetProperty(returnField).GetValue(obj, null);
                    return obj;
                }
            }
            //List<list.GetType()> lst = list;
            return null;
        }

        [WebMethod(EnableSession = true)]
        public object GetObjectValueFromSession(string sessionName, string filterBy, object filterValue, string returnField)
        {
            IList list = (IList)HttpContext.Current.Session[sessionName];
            //Type type = list.GetType().GetGenericArguments()[0];

            foreach (object obj in list)
            {
                object val = obj.GetType().GetProperty(filterBy).GetValue(obj, null);
                if (val.ToString() == filterValue.ToString())
                {
                    return obj.GetType().GetProperty(returnField).GetValue(obj, null);
                }
            }

            return null;
        }
        #endregion
        
        #region Get Control
        [WebMethod(EnableSession = true)]
        public string GetControlHtml(string controlLocation, string queryString)
        {
            try
            {
                Page page = new Page();
                BaseViewPopupCtl userControl = (BaseViewPopupCtl)page.LoadControl(controlLocation);
                userControl.InitializeControl(queryString);
                userControl.EnableViewState = false;
                HtmlForm form = new HtmlForm();
                form.Controls.Add(userControl);
                page.Controls.Add(form);

                StringWriter textWriter = new StringWriter();
                HttpContext.Current.Server.Execute(page, textWriter, false);
                return textWriter.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string CleanHtml(string html)
        {
            return Regex.Replace(html, @"<[/]?(form)[^>]*?>", "", RegexOptions.IgnoreCase);
        }
        #endregion

        #region Get Custom Object
        [WebMethod()]
        public object GetCustomObject(string listParentValue, string selectedColumnID, string selectedColumnName, string valueColumn, string valueColumnType, string filterExpression, string orderByExpression, string objectTypeName)
        {
            List<Variable2> lstVariable = new List<Variable2>();
            IDbContext ctx = DbFactory.Configure(true);
            try
            {
                if (filterExpression != "")
                    filterExpression = string.Format(" WHERE {0}", filterExpression);
                string groupBy = string.Format("{0}, {1}", selectedColumnID, selectedColumnName);
                if (listParentValue != "")
                    groupBy += ", " + listParentValue;
                if (selectedColumnID == selectedColumnName)
                    ctx.CommandText = string.Format("WITH cte AS (SELECT {0}, {2} = {3}({2}) FROM {7}{5} GROUP BY {4}) SELECT ID = {0}, Name = {1}, Value = {2} FROM cte ORDER BY {6}", selectedColumnID, selectedColumnName, valueColumn, valueColumnType, groupBy, filterExpression, orderByExpression, objectTypeName);
                else
                    ctx.CommandText = string.Format("WITH cte AS (SELECT {0}, {1}, {2} = {3}({2}) FROM {7}{5} GROUP BY {4}) SELECT ID = {0}, Name = {1}, Value = {2} FROM cte ORDER BY {6}", selectedColumnID, selectedColumnName, valueColumn, valueColumnType, groupBy, filterExpression, orderByExpression, objectTypeName);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        lstVariable.Add(new Variable2 { ID = reader["ID"].ToString(), Name = reader["Name"].ToString(), Value = Convert.ToDecimal(reader["Value"]) });
            }
            finally
            {
                ctx.Close();
            }
            return lstVariable;
        }

        private class Variable2
        {
            public String ID { get; set; }
            public String Name { get; set; }
            public Decimal Value { get; set; }
            public String cfValue { get { return Value.ToString("N2"); } }
        }
        #endregion

        #region Mobile
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetMobileObject(string methodName, string filterExpression)
        {
            MethodInfo method = typeof(BusinessLayer).GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
            object obj = method.Invoke(null, new string[] { filterExpression });
            IList list = (IList)obj;
            //return list[0];
            //return new JavaScriptSerializer().Serialize(list[0]);

            Object returnObj = new { ReturnObj = list[0], Timestamp = DateTime.Now };
            return new JavaScriptSerializer().Serialize(returnObj);
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetMobileListObject(string methodName, string filterExpression)
        {
            MethodInfo method = typeof(BusinessLayer).GetMethod(methodName, new[] { typeof(string), typeof(int), typeof(int), typeof(string) });
            object obj = method.Invoke(null, new object[] { filterExpression, 10, 1, "" });
            IList list = (IList)obj;
            //return list;
            //return new JavaScriptSerializer().Serialize(list);

            Object returnObj = new { ReturnObj = list, Timestamp = DateTime.Now };
            return new JavaScriptSerializer().Serialize(returnObj);
        }
        #endregion
    }
}
