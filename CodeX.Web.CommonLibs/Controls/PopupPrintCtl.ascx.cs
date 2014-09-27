using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using System.Xml.Linq;

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

                XDocument xdoc = Helper.LoadXMLFile(this, string.Format("right_panel/{0}.xml", ModuleID));
                if (xdoc != null)
                {
                    var lstQuickMenu = (from pg in xdoc.Descendants("page").Where(p => p.Attribute("menucode").Value == param)
                                        select new
                                        {
                                            Print = (from qm in pg.Descendants("print")
                                                     select new
                                                     {
                                                         Title = qm.Attribute("title").Value,
                                                         IsDisplayPrintCount = qm.Attribute("isDisplayPrintCount") == null ? "0" : qm.Attribute("isDisplayPrintCount").Value,
                                                         ReportCode = qm.Attribute("reportcode").Value
                                                     })

                                        }).FirstOrDefault();
                    if (lstQuickMenu != null)
                    {
                        if (lstQuickMenu.Print.Count() > 0)
                        {
                            rptPrint.DataSource = lstQuickMenu.Print;
                            rptPrint.DataBind();
                        }
                    }
                }
            }
        }
    }
}