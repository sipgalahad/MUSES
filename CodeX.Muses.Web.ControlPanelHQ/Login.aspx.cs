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
using System.Web.Configuration;

namespace CodeX.Muses.Web.ControlPanelHQ
{
    public partial class Login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hdnSiteID.Value = BusinessLayer.GetSiteList(string.Format("ParentID IS NULL")).FirstOrDefault().SiteID;

                txtUserName.Attributes.Add("validationgroup", "mpLogin");
                txtPassword.Attributes.Add("validationgroup", "mpLogin");
                Helper.AddCssClass(txtUserName, "required");
                Helper.AddCssClass(txtPassword, "required");

                txtUserName.Focus();
            }
        }

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string param = e.Parameter;
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            string result = "";
            string loginData = "";
            string url = "";
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
                    if (user.TeacherID > 0)
                        userLogin.UserFullName = user.TeacherName;
                    else
                        userLogin.UserFullName = user.FullName;
                    userLogin.TeacherID = user.TeacherID;
                    Site site = BusinessLayer.GetSite(hdnSiteID.Value);
                    userLogin.SiteID = site.SiteID;
                    userLogin.SiteName = site.SiteName;
                    List<vUserInRole> lstUserRole = BusinessLayer.GetvUserInRoleList(string.Format("UserID = {0} AND SiteID = '{1}'", userLogin.UserID, userLogin.SiteID));
                    userLogin.IsSysAdmin = (lstUserRole.Where(p => p.RoleID == 1).Count() > 0);
                    url = "~/../ControlPanelHQ/Libs/Program/RemoteLogon.aspx";

                    AppSession.UserLogin = userLogin;

                    loginData = string.Format("{0}|{1}|{2}", userName, user.Password, site.SiteID);

                    List<GetLoginAttributeUserList> lst = BusinessLayer.GetLoginAttributeUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, "");
                    if (lst.Count > 0)
                        result = "success|1";
                    else
                        result = "success|0";
                }
                else
                    result = "fail|UserID And Password Doesn't match";
            }
            else
                result = "fail|User Doesn't exist";
            panel.JSProperties["cpUrl"] = url;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpLoginData"] = loginData;
        }
    }
}