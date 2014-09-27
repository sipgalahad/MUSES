using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.Configuration;

namespace CodeX.Data.Model
{
    #region vSite
    public partial class vSite
    {
        public String Address
        {
            get
            {
                return Function.GenerateAddress(_StreetName, _County, _District, _City, _State);
            }
        }
        public String AddressLine2
        {
            get
            {
                string result = String.Format("{0} {1} {2} {3} {4}", _District, _County, _City, _State, _ZipCode);
                return result.Replace("  ", " ").TrimStart(new char[] { ' ' }).TrimEnd(new char[] { ',', ' ' });
            }
        }
    }
    #endregion
}