using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Model;
using System.Web.UI.HtmlControls;
using System.Text;
using CodeX.Common;
using CodeX.Web.Common;
using CodeX.Web.CommonLibs.MasterPage;

namespace CodeX.Web.Finance.MasterPage
{
    public partial class MPSupplierPageTrxVisit : BaseMP
    {
        public List<GetUserMenuAccess> ListMenu = null;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!Page.IsPostBack)
            {
                string parentCode = Constant.MenuCode.Finance.SUPPLIER_LIST;
                string filterExpression = string.Format("ParentCode = '{0}'", parentCode);
                ListMenu = BusinessLayer.GetUserMenuAccess(Constant.Module.FINANCE, AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, filterExpression);
                ((MPBaseDetailPageTrx)Master).SetParentCode(parentCode);
                ((MPBaseDetailPageTrx)Master).SetListMenu(ListMenu);

                ((SupplierBannerDtCtl)ctlBanner).InitializeBanner();
                ((MPBaseDetailPageTrx)Master).SetTitleText(((SupplierBannerDtCtl)ctlBanner).OnGetTitleText());
            }
        }
    }
}