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
using CodeX.Common;

namespace CodeX.Web.CommonLibs.MasterPage
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string moduleName = Helper.GetModuleName();
                string ModuleID = Helper.GetModuleID(moduleName);
                XDocument xdoc = Helper.LoadXMLFile(this, string.Format("right_panel/{0}.xml", ModuleID));
                if (xdoc != null)
                {
                    string menuCode = BasePageContent.OnGetMenuCode();
                    List<GetReportUserList> lstReport = BusinessLayer.GetReportUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.ReportType.FORM, ModuleID, menuCode, "");
                    if (lstReport.Count > 0)
                        btnMPEntryPrint.Style.Remove("display");
                }
            }
        }

        protected string OnGetMenuCode()
        {
            return BasePageContent.OnGetMenuCode();
        }
    }
}