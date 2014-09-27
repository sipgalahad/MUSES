using System;
using System.Collections.Generic;
using System.Data;
using CodeX.Data.Core.Dal;
using System.Linq;

namespace CodeX.Data.Model
{
    public static partial class BusinessLayer
    {
        #region GetLocationUserAccessList
        public static List<GetLocationUserList> GetLocationUserAccessList(string param)
        {
            string[] par = param.Split(';');
            string siteID = par[0];
            string userID = par[1];
            string transactionCode = par[2];
            string filterExpression = par[3];
            return GetLocationUserList(siteID, Convert.ToInt32(userID), transactionCode, filterExpression);
        }
        #endregion
    }
}
