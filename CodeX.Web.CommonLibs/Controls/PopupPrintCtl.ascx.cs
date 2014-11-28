using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using System.Xml.Linq;
using CodeX.Data.Model;
using CodeX.Common;

namespace CodeX.Web.CommonLibs.Controls
{
    public partial class PopupPrintCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            if (param != "")
            {
                string moduleName = Helper.GetModuleName();
                string ModuleID = Helper.GetModuleID(moduleName);
                List<GetReportUserList> lstReport = BusinessLayer.GetReportUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.ReportType.FORM, ModuleID, param, "");
                rptPrint.DataSource = lstReport;
                rptPrint.DataBind();
            }
        }
    }
}