using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common;
using CodeX.Data.Model;
using System.Web.UI.HtmlControls;
using CodeX.Web.Common.UI;

namespace CodeX.Web.CommonLibs.MasterPage
{
    public partial class MPMain : BaseMP
    {
        protected string moduleName = "";
        public List<GetUserMenuAccess> ListMenu { get { return lstMenu; } }
        protected List<GetUserMenuAccess> lstMenu = null;

        private string ModuleID = "";
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!Page.IsPostBack)
            {
                moduleName = Helper.GetModuleName();
                if (AppSession.UserLogin == null)
                    Response.Redirect(GetLoginUrl());

                hdnLoginData.Value = string.Format("{0}|{1}|{2}", AppSession.UserLogin.UserName, "fromprogram", AppSession.UserLogin.SiteID);

                ModuleID = Helper.GetModuleID(moduleName);
                lstMenu = BusinessLayer.GetUserMenuAccess(ModuleID, AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, "IsShowInPullDownMenu = 1 AND IsVisible = 1");
                //lstMenu = BusinessLayer.GetMenuList(string.Format("ModuleID = '{0}'", ModuleID));
                rptMenu.DataSource = lstMenu.Where(p => p.ParentID == null).OrderBy(p => p.MenuIndex).ToList();
                rptMenu.DataBind();

                imgOpenModule.Src = ResolveUrl("~/Libs/Images/Icon/module.png");
                imgCloseLeftPane.Src = ResolveUrl("~/Libs/Images/Icon/close_pane.png");

                List<Module> lstModule = BusinessLayer.GetModuleList(string.Format("ModuleID IN ({0}) ORDER BY ModuleIndex", AppSession.ListModuleID));
                rptModule.DataSource = lstModule;
                rptModule.DataBind();

                divSiteName.InnerHtml = AppSession.UserLogin.SiteName;
            }
        }

        protected string GetLoginUrl()
        {
            if (moduleName.Contains("HQ"))
                return "~/../ControlPanelHQ/Login.aspx";
            else
                return string.Format("~/../ControlPanel/Login.aspx?id={0}", AppSession.UserLogin.SiteID);
        }

        protected void rptModule_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Module entity = (Module)e.Item.DataItem;
                HtmlImage imgModule = e.Item.FindControl("imgModule") as HtmlImage;
                HtmlGenericControl ulLinkModule = e.Item.FindControl("ulLinkModule") as HtmlGenericControl;
                imgModule.Src = ResolveUrl(entity.ImageUrl);
                if (entity.ModuleID == ModuleID)
                    ulLinkModule.Attributes.Add("class", "ulLinkModule selected");
                else
                    ulLinkModule.Attributes.Add("class", "ulLinkModule");
                ulLinkModule.Attributes.Add("url", entity.DefaultUrl);
            }
        }

        protected string GetMainPageUrl()
        {
            return ResolveUrl("~/Libs/Program/Main.aspx");
        }

        protected string GetResolveUrl(string url)
        {
            if (url == "#")
                return "#";
            return ResolveUrl(url);
        }

        protected void rptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                GetUserMenuAccess obj = (GetUserMenuAccess)e.Item.DataItem;

                Repeater rptMenuLevel2 = (Repeater)e.Item.FindControl("rptMenuLevel2");

                List<GetUserMenuAccess> lst = GetMenuChild(obj.MenuID);
                if (lst.Count > 0)
                {
                    rptMenuLevel2.DataSource = lst;
                    rptMenuLevel2.DataBind();
                }
            }
        }

        protected void rptMenuLevel2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                GetUserMenuAccess obj = (GetUserMenuAccess)e.Item.DataItem;

                Repeater rptMenuLevel3 = (Repeater)e.Item.FindControl("rptMenuLevel3");

                List<GetUserMenuAccess> lst = GetMenuChild(obj.MenuID);
                if (lst.Count > 0)
                {
                    rptMenuLevel3.DataSource = lst;
                    rptMenuLevel3.DataBind();
                }
            }
        }

        protected void rptMenuLevel3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                GetUserMenuAccess obj = (GetUserMenuAccess)e.Item.DataItem;

                Repeater rptMenuLevel4 = (Repeater)e.Item.FindControl("rptMenuLevel4");

                List<GetUserMenuAccess> lst = GetMenuChild(obj.MenuID);
                if (lst.Count > 0)
                {
                    rptMenuLevel4.DataSource = lst;
                    rptMenuLevel4.DataBind();
                }
            }
        }

        protected List<GetUserMenuAccess> GetMenuChild(Int32 ParentID)
        {
            return lstMenu.Where(p => p.ParentID == ParentID).OrderBy(p => p.MenuIndex).ToList();
        }

        protected string GetUserInfo()
        {
            return string.Format("{0}", AppSession.UserLogin.UserFullName);
        }

        protected void cbpCloseWindow_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            AppSession.ClearSession();
        }
    }
}