using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Model;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CodeX.Web.Common;

namespace CodeX.Muses.Web.Mobile.MasterPage
{
    public partial class MPBaseContent : BaseMP
    {
        private BasePageContent _basePageContent;
        private BasePageContent BasePageContent
        {
            get
            {
                if (_basePageContent == null)
                    _basePageContent = (BasePageContent)Page;
                return _basePageContent;
            }
        }
        protected string menuCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                menuCode = OnGetMenuCode();
                menu = ((MPMain)Master).ListMenu.FirstOrDefault(p => p.MenuCode == menuCode);
            }
        }
        private GetUserMenuAccess menu;
        protected String GetMenuCaption()
        {
            return menu.MenuCaption;
        }
        protected String GetBreadcrumbs()
        {
            List<GetUserMenuAccess> lstMenu = ((MPMain)Master).ListMenu;
            StringBuilder result = new StringBuilder();
            List<GetUserMenuAccess> imagesHierarchy = new List<GetUserMenuAccess>();

            GetUserMenuAccess currMenu = lstMenu.FirstOrDefault(p => p.MenuCode == menuCode);
            while (currMenu != null)
            {
                imagesHierarchy.Insert(0, currMenu);
                currMenu = lstMenu.FirstOrDefault(p => p.MenuID == currMenu.ParentID);
            }

            string breadcrumb = "";
            foreach (GetUserMenuAccess menu in imagesHierarchy)
            {
                if (breadcrumb != "")
                    breadcrumb += "<div class='divSeparator'> > </div>";
                breadcrumb += string.Format("<div>{0}</div>", menu.MenuCaption);
            }
            //string breadcrumb = string.Join(" > ", string.Format("<div>{0}</div>", imagesHierarchy.Select(i => i.MenuCaption)));
            return breadcrumb;
        }

        protected string OnGetMenuCode()
        {
            return BasePageContent.OnGetMenuCode();
        }
    }
}