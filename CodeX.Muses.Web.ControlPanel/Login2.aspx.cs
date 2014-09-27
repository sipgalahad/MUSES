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

namespace CodeX.Muses.Web.ControlPanel
{
    public partial class Login2 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (AppSession.UserLogin == null)
                    pnlUserLoginInformation.Style.Add("display", "none");
                else
                {
                    BindCboSelectUserRole();
                    lblUserLoginInfo.InnerHtml = AppSession.UserLogin.UserFullName;
                    loginContainerLoginInfo.Style.Add("display", "none");
                }

                txtUserName.Attributes.Add("validationgroup", "mpLogin");
                txtPassword.Attributes.Add("validationgroup", "mpLogin");
                Helper.AddCssClass(txtUserName, "required");
                Helper.AddCssClass(txtPassword, "required");

                SetRptModule(ddlSite.SelectedValue);

                txtUserName.Focus();
            }
            //FormsAuthentication.HashPasswordForStoringInConfigFile(
            //Response.Redirect(Page.ResolveUrl("~/../Outpatient/Program/Registration.aspx"));
        }

        private void SetRptModule(string siteID)
        {
            List<Module> apps = BusinessLayer.GetModuleList("IsVisible = 1 ORDER BY ModuleIndex ASC");
            List<LoginModule> lstModule = null;
            if (AppSession.UserLogin == null)
            {
                lstModule = (from p in apps
                             select new LoginModule { ModuleID = p.ModuleID, ImageUrl = ResolveUrl(p.DisabledImageUrl), ModuleName = p.ModuleName, Link = p.DefaultUrl }).ToList();
            }
            else
            {
                lstModule = (from p in apps
                             select new LoginModule { ModuleID = p.ModuleID, ImageUrl = p.ImageUrl, DisabledImageUrl = p.DisabledImageUrl, ModuleName = p.ModuleName, Link = p.DefaultUrl }).ToList();

                //List<GetUserMenuAccess> lstUserMenu = BusinessLayer.GetUserMenuAccess("", SiteID, AppSession.UserLogin.UserID, "IsShowInPullDownMenu = 1");
                foreach (LoginModule module in lstModule)
                {
                    List<GetUserMenuAccess> lstUserMenu = BusinessLayer.GetUserMenuAccess(module.ModuleID, siteID, AppSession.UserLogin.UserID, "IsShowInPullDownMenu = 1");
                    //GetUserMenuAccess userMenu = lstUserMenu.FirstOrDefault(p => p.ModuleID == module.ModuleID);
                    if (lstUserMenu.Count > 0)
                    {
                        module.ImageUrl = ResolveUrl(module.ImageUrl);
                        module.CssClass = "enabled";
                    }
                    else
                        module.ImageUrl = ResolveUrl(module.DisabledImageUrl);
                }
            }

            rptModule.DataSource = lstModule;
            rptModule.DataBind();
        }

        protected void cbpRptModule_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            SetRptModule(e.Parameter);
        }

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string param = e.Parameter;
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpParam"] = param;
            if (param == "login")
            {
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
                        loginData = string.Format("{0}|{1}", userName, user.Password);

                        UserLogin userLogin = new UserLogin();
                        userLogin.UserID = user.UserID;
                        userLogin.UserName = user.UserName;
                        userLogin.UserFullName = user.FullName;

                        AppSession.UserLogin = userLogin;
                        result = string.Format("success|{0}", userLogin.UserFullName);
                    }
                    else
                        result = "fail|UserID And Password Doesn't match";
                }
                else
                    result = "fail|User Doesn't exist";
                panel.JSProperties["cpResult"] = result;
                panel.JSProperties["cpLoginData"] = loginData;
            }
            else // Get Data Login
            {
                User user = BusinessLayer.GetUser(AppSession.UserLogin.UserID);
                string loginData = string.Format("{0}|{1}", user.UserName, user.Password);
                panel.JSProperties["cpLoginData"] = loginData;
                panel.JSProperties["cpLink"] = param.Split('|')[1];
                panel.JSProperties["cpModuleID"] = param.Split('|')[2];
            }
        }

        protected void cbpSelectUserRole_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindCboSelectUserRole();
        }

        private void BindCboSelectUserRole()
        {
            List<vUserInRole> lstUserInRole = BusinessLayer.GetvUserInRoleList(string.Format("UserID = {0}", AppSession.UserLogin.UserID));
            List<Site> lstSite = (from p in lstUserInRole
                                              select new Site { SiteID = p.SiteID, SiteName = p.SiteName }).GroupBy(p => p.SiteID).Select(p => p.First()).ToList();
            Methods.SetComboBoxField<Site>(ddlSite, lstSite, "SiteName", "SiteID");
            ddlSite.SelectedIndex = 0;
        }

        protected class LoginModule
        {
            private string _CssClass = "disabled";
            public string CssClass
            {
                get { return _CssClass; }
                set { _CssClass = value; }
            }
            public string ModuleID { get; set; }
            public string DisabledImageUrl { get; set; }
            public string ModuleName { get; set; }
            public string Link { get; set; }
            public string ImageUrl { get; set; }
        }


        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            AppSession.ClearSession();
            HttpContext.Current.Response.Redirect("~/Login.aspx", true);
        }
    }
}