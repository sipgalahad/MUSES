using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeX.Data.Core.Dal;

namespace CodeX.Data.Model
{
    #region StandardCode
    public partial class StandardCode
    {
        public String cfStandardCodeID
        {
            get
            {
                return _StandardCodeID.Split('^')[1];
            }
        }
    }
    #endregion


    #region MigrationConfigurationDt
    public partial class MigrationConfigurationDt
    {
        public String InputType
        {
            get
            {
                switch (_Type)
                {
                    case "1": return "Text Box";
                    case "2": return "Combo Box";
                    case "3": return "Check Box";
                    case "4": return "Date Edit";
                    default: return "Search Dialog";
                }
            }
        }
    }
    #endregion
}
