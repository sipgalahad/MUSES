using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Data.Model;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using System.Web.Security;
using DevExpress.Web.ASPxCallbackPanel;

namespace CodeX.Muses.Web.Mobile.Program
{
    public partial class Login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string param = e.Parameter;
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            string result = "";
            string loginData = "";
            string userName = txtUserName.Text;
            string password = txtPassword.Text;
            List<vUser> lstUser = BusinessLayer.GetvUserList(string.Format("UserName = '{0}' AND IsDeleted = 0", userName));
            if (lstUser.Count > 0)
            {
                vUser user = lstUser[0];
                if (user.Password.Trim() == FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha1"))
                {
                    UserLogin userLogin = new UserLogin();
                    userLogin.UserID = user.UserID;
                    userLogin.UserName = user.UserName;
                    userLogin.UserFullName = user.FullName;
                    Site site = BusinessLayer.GetSiteList("").FirstOrDefault();
                    userLogin.SiteID = site.SiteID;
                    userLogin.SiteName = site.SiteName;
                    
                    UserAttribute ua = BusinessLayer.GetUserAttribute(user.UserID);
                    userLogin.EmployeeID = ua.EmployeeID;

                    List<UserInRole> lstUserSysAdmin = BusinessLayer.GetUserInRoleList(string.Format("UserID = {0} AND SiteID = '{1}' AND RoleID = 1", userLogin.UserID, userLogin.SiteID));
                    userLogin.IsSysAdmin = (lstUserSysAdmin.Count > 0);

                    AppSession.UserLogin = userLogin;

                    loginData = string.Format("{0}|{1}|{2}", userName, user.Password, site.SiteID);
                    result = string.Format("success|{0}", user.UserName);
                }
                else
                    result = "fail|UserID And Password Doesn't match";
            }
            else
                result = "fail|User Doesn't exist";
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpLoginData"] = loginData;
        }
    }
}