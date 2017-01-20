using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeX.Data.Core.Dal;
using System.Data;

namespace CodeX.Data.Model
{
    public static partial class BusinessLayer
    {
        #region GetServiceUnitUserAccessList
        public static List<GetServiceUnitUserList> GetServiceUnitUserAccessList(string param)
        {
            string[] par = param.Split(';');
            string siteID = par[0];
            string userID = par[1];
            string filterExpression = par[2];
            return GetServiceUnitUserList(siteID, Convert.ToInt32(userID), filterExpression);
        }
        #endregion
    }
}
