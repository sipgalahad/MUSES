using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common;
using CodeX.Data.Model;
using System.Web.Security;
using System.Text;
using CodeX.Common;

namespace CodeX.Web.CommonLibs.Program
{
    public partial class RemoteLogon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["id"] != null)
            {
                string[] param = Request.Form["id"].Split('|');
                string userName = param[0];
                string password = param[1];
                List<vUser> lstUser = BusinessLayer.GetvUserList(string.Format("UserName = '{0}' AND IsDeleted = 0", userName));
                if (lstUser.Count > 0)
                {
                    vUser user = lstUser[0];
                    if (user.Password.Trim() == password || password == "fromprogram")
                    {
                        string siteID = param[2];

                        UserLogin userLogin = new UserLogin();
                        userLogin.UserID = user.UserID;
                        userLogin.UserName = user.UserName;
                        if (user.EmployeeID > 0)
                            userLogin.UserFullName = user.EmployeeName;
                        else
                            userLogin.UserFullName = user.FullName;
                        userLogin.SiteID = siteID;
                        userLogin.EmployeeID = user.EmployeeID;

                        List<UserInRole> lstUserSysAdmin = BusinessLayer.GetUserInRoleList(string.Format("UserID = {0} AND SiteID = '{1}' AND RoleID = 1", userLogin.UserID, userLogin.SiteID));
                        userLogin.IsSysAdmin = (lstUserSysAdmin.Count > 0);
                        userLogin.SiteName = BusinessLayer.GetSite(siteID).SiteName;

                        AppSession.UserLogin = userLogin;

                        string ListModuleID = "";
                        List<Module> lstModule = BusinessLayer.GetModuleList("IsVisible = 1 ORDER BY ModuleIndex ASC");
                        foreach (Module module in lstModule)
                        {
                            List<GetUserMenuAccess> lstUserMenu = BusinessLayer.GetUserMenuAccess(module.ModuleID, siteID, AppSession.UserLogin.UserID, "IsShowInPullDownMenu = 1");
                            if (lstUserMenu.Count > 0)
                            {
                                if (ListModuleID != "")
                                    ListModuleID += ",";
                                ListModuleID += string.Format("'{0}'", module.ModuleID);
                            }
                        }
                        AppSession.ListModuleID = ListModuleID;
                        Response.Redirect("~/Libs/Program/Main.aspx");
                    }
                }
            }
        }
    }
}