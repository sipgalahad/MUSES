using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Data.Model;
using CodeX.Web.Common;
using CodeX.Common;

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class ProjectManagementPageLauncher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AppSession.ProjectID = Convert.ToInt32(Request.QueryString["id"]);

            string filterExpression = string.Format("ParentCode = '{0}'", Constant.MenuCode.ProjectManagement.PROJECT_MANAGEMENT_PAGE);
            List<GetUserMenuAccess> lstMenu = BusinessLayer.GetUserMenuAccess(Constant.Module.PROJECT_MANAGEMENT, AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, filterExpression);
            int parentID = (int)lstMenu.Where(p => p.MenuIndex > 0).OrderBy(p => p.MenuIndex).FirstOrDefault().MenuID;

            filterExpression = string.Format("ParentID = {0}", parentID);
            lstMenu = BusinessLayer.GetUserMenuAccess(Constant.Module.PROJECT_MANAGEMENT, AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, filterExpression);
            GetUserMenuAccess menu = lstMenu.OrderBy(p => p.MenuIndex).FirstOrDefault();
            Response.Redirect(Page.ResolveUrl(menu.MenuUrl));
        }
    }
}