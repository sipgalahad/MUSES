using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using System.Web.UI.HtmlControls;
using CodeX.Common;
using System.Xml.Linq;

namespace CodeX.Web.CommonLibs.MasterPage
{
    public partial class MPBaseDetailPage : BaseMP
    {
        protected string GetUrlReferrer()
        {
            return "";
        }
        public List<GetUserMenuAccess> ListMenu = null;
        public string menuCode = null;
        private string parentCode = null;


        protected string OnGetMenuCode()
        {
            return menuCode;
        }
        public void SetTitleText(string title)
        {
            tdPatientName.InnerHtml = h3Title.InnerHtml = title;
        }
        public void SetListMenu(List<GetUserMenuAccess> lstMenu)
        {
            ListMenu = lstMenu;
        }

        public void SetParentCode(string parentCode)
        {
            this.parentCode = parentCode;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Page.IsPostBack)
            {
                if (AppSession.UserLogin == null)
                    Response.Redirect("~/../ControlPanel/Login.aspx");


                //imgCloseLeftPane.Src = ResolveUrl("~/Libs/Images/Icon/close_pane.png");
                menuCode = ((BasePageContent)Page).OnGetMenuCode();
                if (parentCode != "")
                {
                    rptMenuHeader.DataSource = ListMenu.Where(p => p.ParentCode == parentCode && p.IsVisible).OrderBy(p => p.MenuIndex).ToList();
                    rptMenuHeader.DataBind();

                    GetUserMenuAccess selectedMenu = ListMenu.FirstOrDefault(p => p.MenuCode == menuCode);
                    if (selectedMenu.ParentCode != parentCode)
                    {
                        rptMenuDetail.DataSource = ListMenu.Where(p => p.ParentID == selectedMenu.ParentID && p.IsVisible).OrderBy(p => p.MenuIndex).ToList();
                        rptMenuDetail.DataBind();
                        divBorderBottomMenuLevel1.Style.Add("display", "none");
                    }
                }
                else
                {
                    rptMenuHeader.DataSource = ListMenu;
                    rptMenuHeader.DataBind();
                }
                string moduleName = Helper.GetModuleName();
                string ModuleID = Helper.GetModuleID(moduleName);
                List<GetReportUserList> lstReport = BusinessLayer.GetReportUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.ReportType.FORM, ModuleID, menuCode, "");
                if (lstReport.Count > 0)
                    btnMPEntryPrint.Style.Remove("display");
            }
        }

        protected void rptMenuHeader_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                GetUserMenuAccess obj = (GetUserMenuAccess)e.Item.DataItem;
                HtmlGenericControl ulLinkModule = e.Item.FindControl("ulLinkModule") as HtmlGenericControl;

                List<GetUserMenuAccess> lstMn = ListMenu.Where(p => p.ParentID == obj.MenuID && p.IsVisible).OrderBy(p => p.MenuIndex).ToList();
                if (lstMn.Count > 0)
                {
                    ulLinkModule.Attributes.Add("url", lstMn[0].MenuUrl);

                    IEnumerable<GetUserMenuAccess> mn = ListMenu.Where(p => p.ParentID == obj.MenuID && p.MenuCode == menuCode && p.IsVisible);
                    if (mn.Count() > 0)
                        ulLinkModule.Attributes.Add("class", "ulLinkModule selected");
                    else
                        ulLinkModule.Attributes.Add("class", "ulLinkModule");
                }
                else
                {
                    ulLinkModule.Attributes.Add("url", obj.MenuUrl);
                    if (obj.MenuCode == menuCode)
                        ulLinkModule.Attributes.Add("class", "ulLinkModule selected");
                    else
                        ulLinkModule.Attributes.Add("class", "ulLinkModule");
                }
            }
        }

        protected void rptMenuDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                GetUserMenuAccess obj = (GetUserMenuAccess)e.Item.DataItem;
                HtmlGenericControl liCaption = (HtmlGenericControl)e.Item.FindControl("liCaption");
                if (obj.MenuCode == menuCode)
                    liCaption.Attributes.Add("class", "selected");
            }
        }
    }
}