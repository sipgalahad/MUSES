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
            List<Student> lstUser = BusinessLayer.GetStudentList(string.Format("StudentCode = '{0}' AND IsDeleted = 0", userName));
            if (lstUser.Count > 0)
            {
                Student user = lstUser[0];
                if (user.Password.Trim() == FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha1"))
                {
                    UserLogin userLogin = new UserLogin();
                    userLogin.UserID = user.StudentID;
                    userLogin.UserName = user.StudentCode;
                    userLogin.UserFullName = user.StudentName;
                    Site site = BusinessLayer.GetSiteList("").FirstOrDefault();
                    userLogin.SiteID = site.SiteID;
                    userLogin.SiteName = site.SiteName;

                    AppSession.StudentLogin = userLogin;

                    loginData = string.Format("{0}|{1}|{2}", userName, user.Password, site.SiteID);
                    result = string.Format("success|{0}", user.StudentCode);
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