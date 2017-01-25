using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Xml.Linq;
using CodeX.Data.Model;

namespace CodeX.Web.Common.UI
{
    public abstract class BasePageContent : BasePage
    {
        public abstract string OnGetMenuCode();
        public virtual string GetCustomLang()
        {
            return "";
        }
        public virtual bool IsReportSelectLanguage()
        {
            return false;
        }
        public virtual bool IsEntryUsePopup()
        {
            return true;
        }

        public virtual void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowEdit = IsAllowDelete = true;
        }
    }
}
