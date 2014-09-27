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


                //imgCloseLeftPane.Src = ResolveUrl("~/Libs/Images/Icon/left.png");
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
                XDocument xdoc = Helper.LoadXMLFile(this, string.Format("right_panel/{0}.xml", ModuleID));
                if (xdoc != null)
                {
                    var lstQuickMenu = (from pg in xdoc.Descendants("page").Where(p => p.Attribute("menucode").Value == menuCode)
                                        select new
                                        {
                                            Tasks = (from qm in pg.Descendants("task")
                                                     select new
                                                     {
                                                         ID = qm.Attribute("id") == null ? "" : qm.Attribute("id").Value,
                                                         Code = qm.Attribute("code").Value,
                                                         Title = qm.Attribute("title").Value,
                                                         Description = qm.Attribute("description").Value,
                                                         Url = qm.Attribute("url").Value,
                                                         Width = qm.Attribute("width") == null ? "950" : qm.Attribute("width").Value,
                                                         Height = qm.Attribute("height") == null ? "600" : qm.Attribute("height").Value
                                                         //Url = Page.ResolveUrl(qm.Attribute("url").Value)
                                                     }),
                                            Information = (from qm in pg.Descendants("information")
                                                           select new
                                                           {
                                                               ID = qm.Attribute("id") == null ? "" : qm.Attribute("id").Value,
                                                               Code = qm.Attribute("code").Value,
                                                               Title = qm.Attribute("title").Value,
                                                               Description = qm.Attribute("description").Value,
                                                               Url = qm.Attribute("url").Value,
                                                               Width = qm.Attribute("width") == null ? "950" : qm.Attribute("width").Value,
                                                               Height = qm.Attribute("height") == null ? "600" : qm.Attribute("height").Value
                                                               //Url = Page.ResolveUrl(qm.Attribute("url").Value)
                                                           }),
                                            Print = (from qm in pg.Descendants("print")
                                                     select new
                                                     {
                                                         Title = qm.Attribute("title").Value,
                                                         ReportCode = qm.Attribute("reportcode").Value
                                                     })

                                        }).FirstOrDefault();
                    if (lstQuickMenu != null)
                    {
                        //if (lstQuickMenu.Tasks.Count() > 0)
                        //{
                        //    rptTasks.DataSource = lstQuickMenu.Tasks;
                        //    rptTasks.DataBind();
                        //}
                        if (lstQuickMenu.Information.Count() > 0)
                            btnMPEntryInfo.Style.Remove("display");
                        if (lstQuickMenu.Print.Count() > 0)
                            btnMPEntryPrint.Style.Remove("display");
                    }
                }
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