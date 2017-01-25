using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Data.Model;
using CodeX.Web.Common;
using CodeX.Common;
using System.Text;

namespace CodeX.Muses.Web.Finance.Program
{
    public partial class SupplierPageLauncher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string filterExpression = string.Format("ParentCode = '{0}'", Constant.MenuCode.Finance.SUPPLIER_LIST);
            List<GetUserMenuAccess> lstMenu = BusinessLayer.GetUserMenuAccess(Constant.Module.FINANCE, AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, filterExpression);
            GetUserMenuAccess menu = lstMenu.OrderBy(p => p.MenuIndex).FirstOrDefault();

            Response.Clear();

            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
            sb.AppendFormat("<form name='form' action='{0}' method='post'>", Page.ResolveUrl(menu.MenuUrl));
            sb.AppendFormat("<input type='hidden' name='postsessionid' value='{0}'>", Request.QueryString["id"]);
            // Other params go here
            sb.Append("</form>");
            sb.Append("</body>");
            sb.Append("</html>");

            Response.Write(sb.ToString());

            Response.End();

            //Response.Redirect(Page.ResolveUrl(menu.MenuUrl));
        }
    }
}