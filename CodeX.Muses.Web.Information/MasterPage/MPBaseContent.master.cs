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

namespace CodeX.Muses.Web.Information.MasterPage
{
    public partial class MPBaseContent : BaseMP
    {
        private BasePageList _basePageList;
        private BasePageList BasePageList
        {
            get
            {
                if (_basePageList == null)
                    _basePageList = (BasePageList)Page;
                return _basePageList;
            }
        }
        protected string menuCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                menuCode = OnGetMenuCode();
                menu = ((MPMain)Master).ListMenu.FirstOrDefault(p => p.MenuCode == menuCode);
                string CRUDMode = menu.CRUDMode;

                hdnMenuCaption.Value = menu.MenuCaption;

                foreach (Control c in ulMPListToolbar.Controls)
                {
                    if (c is HtmlControl && ((HtmlControl)c).TagName.ToLower() == "li")
                    {
                        HtmlGenericControl li = c as HtmlGenericControl;
                        SetToolbarButtonVisibility(li, CRUDMode);
                    }
                    else if (c is ContentPlaceHolder)
                    {
                        foreach (Control c2 in c.Controls)
                        {
                            if (c2 is HtmlControl && ((HtmlControl)c2).TagName.ToLower() == "li")
                            {
                                HtmlGenericControl li = c2 as HtmlGenericControl;
                                SetToolbarButtonVisibility(li, CRUDMode);
                            }
                        }
                    }
                }
            }
        }

        private void SetToolbarButtonVisibility(HtmlGenericControl li, string CRUDMode)
        {
            if (li.Attributes["CRUDMode"] != null)
            {
                string liCRUDMode = li.Attributes["CRUDMode"];
                if (!CRUDMode.Contains(liCRUDMode))
                {
                    li.Style.Add("display", "none");
                    li.Attributes.Add("isallow", "0");
                }
                else
                    li.Attributes.Add("isallow", "1");
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
            return BasePageList.OnGetMenuCode();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Helper.ExportExcel(hdnMenuCaption.Value, hdnMenuCaption.Value, BasePageList.OnGetExportControl(), this);
        }
    }
}