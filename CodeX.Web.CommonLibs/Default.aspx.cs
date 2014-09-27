using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using System.Web.Security;
using CodeX.Web.Common;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;

namespace CodeX.Web.CommonLibs
{
    public partial class Default : BasePage
    {
        protected string moduleName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            moduleName = Helper.GetModuleName();
        }
    }
}