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
using CodeX.Common;

namespace CodeX.Muses.Web.Mobile.MasterPage
{
    public partial class MPMain : BaseMP
    {
        public List<MenuMaster> ListMenu { get { return lstMenu; } }
        protected List<MenuMaster> lstMenu = null;
        private string MenuCode = "";
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!Page.IsPostBack)
            {
                if (AppSession.StudentLogin == null)
                    Response.Redirect("~/Login.aspx");

                MenuCode = ((BasePageContent)Page).OnGetMenuCode();
                hdnLoginData.Value = string.Format("{0}|{1}|{2}", AppSession.StudentLogin.UserName, "fromprogram", AppSession.StudentLogin.SiteID);

                lstMenu = BusinessLayer.GetMenuMasterList(string.Format("ModuleID = '{0}' AND IsShowInPullDownMenu = 1 AND IsVisible = 1", Constant.Module.MOBILE));
                //string ModuleID = Constant.Module.MOBILE;
                //lstMenu = BusinessLayer.GetUserMenuAccess(ModuleID, AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, "IsShowInPullDownMenu = 1 AND IsVisible = 1");
                //lstMenu = BusinessLayer.GetMenuList(string.Format("ModuleID = '{0}'", ModuleID));
                
            }
        }

        protected void rptMenuLevel1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                MenuMaster entity = (MenuMaster)e.Item.DataItem;
                Repeater rptMenuLevel2 = (Repeater)e.Item.FindControl("rptMenuLevel2");
                rptMenuLevel2.DataSource = lstMenu.Where(p => p.ParentID == entity.MenuID).ToList();
                rptMenuLevel2.DataBind();
            }
        }

        protected void rptMenuLevel2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                MenuMaster entity = (MenuMaster)e.Item.DataItem;
                HtmlGenericControl ulLinkMenu = e.Item.FindControl("ulLinkMenu") as HtmlGenericControl;
                if (entity.MenuCode == MenuCode)
                    ulLinkMenu.Attributes.Add("class", "ulLinkMenu selected");
                else
                    ulLinkMenu.Attributes.Add("class", "ulLinkMenu");
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

        protected string GetHospitalName()
        {
            return AppSession.StudentLogin.SiteName;
        }

        protected string GetUserInfo()
        {
            return string.Format("{0}", AppSession.StudentLogin.UserFullName);
        }

        protected void cbpCloseWindow_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            AppSession.ClearSession();
        }
    }
}