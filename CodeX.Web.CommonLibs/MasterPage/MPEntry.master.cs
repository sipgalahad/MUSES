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

namespace CodeX.Web.CommonLibs.MasterPage
{
    public partial class MPEntry : BaseMP
    {
        private BasePageEntry _basePageEntry;
        private BasePageEntry BasePageEntry
        {
            get
            {
                if (_basePageEntry == null)
                    _basePageEntry = (BasePageEntry)Page;
                return _basePageEntry;
            }
        }

        private GetUserMenuAccess menu;
        protected String GetMenuCaption()
        {
            return menu.MenuCaption;
        }
        protected String GetBreadcrumbs()
        {
            List<GetUserMenuAccess> lstMenu = ((MPMain)((MPBaseContent)Master).Master).ListMenu;
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
            breadcrumb += "<div class='divSeparator'> > </div>";
            breadcrumb += string.Format("<div>{0} Entry</div>", imagesHierarchy.Last().MenuCaption);
            //string breadcrumb = string.Join(" > ", string.Format("<div>{0}</div>", imagesHierarchy.Select(i => i.MenuCaption)));
            return breadcrumb;
        }

        protected string menuCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Form["id"] != null)
                    hdnListID.Value = Request.Form["id"].ToString();
                if (Request.Form["txtSearchView"] != null)
                    hdnListTextSearch.Value = Request.Form["txtSearchView"].ToString();
                if (Request.Form["filterExpression"] != null)
                    hdnListFilterExpression.Value = Request.Form["filterExpression"].ToString();

                hdnIsAdd.Value = BasePageEntry.IsAdd ? "1" : "0";
                menuCode = BasePageEntry.OnGetMenuCode();
                bool IsAllowSaveAndNew, IsAllowSaveAndClose;
                IsAllowSaveAndNew = IsAllowSaveAndClose = true;
                BasePageEntry.SetCRUDMode(ref IsAllowSaveAndNew, ref IsAllowSaveAndClose);
                menu = ((MPMain)((MPBaseContent)Master).Master).ListMenu.FirstOrDefault(p => p.MenuCode == menuCode);
                string CRUDMode = menu.CRUDMode;

                if (!IsAllowSaveAndNew) CRUDMode = CRUDMode.Replace("C", "");
                if (!IsAllowSaveAndClose) CRUDMode = CRUDMode.Replace("U", "").Replace("C", "");

                if (!CRUDMode.Contains("C"))
                    btnMPEntrySaveNew.Style.Add("display", "none");
                if (!CRUDMode.Contains("C") && !CRUDMode.Contains("U"))
                    btnMPEntrySaveClose.Style.Add("display", "none");
            }
        }

        protected void cbpMPEntryContent_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string param = e.Parameter;
            if (param == "refresh")
                BasePageEntry.RefreshControl();

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpParam"] = param;
        }

        protected void cbpMPEntryProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string[] param = e.Parameter.Split('|');
            if (param[0] == "savenew" || param[0] == "saveclose")
            {
                bool isAdd = (param[1] == "1");
                result = param[0] + "|";
                BasePageEntry.OnBtnSaveClick(ref result, isAdd);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
    }
}