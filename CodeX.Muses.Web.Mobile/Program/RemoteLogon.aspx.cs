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

namespace CodeX.Muses.Web.Mobile.Program
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
                        userLogin.UserFullName = user.FullName;
                        userLogin.SiteID = siteID;

                        List<UserInRole> lstUserSysAdmin = BusinessLayer.GetUserInRoleList(string.Format("UserID = {0} AND SiteID = '{1}' AND RoleID = 1", userLogin.UserID, userLogin.SiteID));
                        userLogin.IsSysAdmin = (lstUserSysAdmin.Count > 0);
                        userLogin.SiteName = BusinessLayer.GetSite(siteID).SiteName;

                        AppSession.UserLogin = userLogin;

                        Response.Redirect("~/Program/VisitList.aspx");
                    }
                }
            }
        }
    }
}