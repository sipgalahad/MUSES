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
    public partial class PopupInfoCtl : BaseViewPopupCtl
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
                                                           })

                                        }).FirstOrDefault();
                    if (lstQuickMenu != null)
                    {
                        if (lstQuickMenu.Information.Count() > 0)
                        {
                            rptInformation.DataSource = lstQuickMenu.Information;
                            rptInformation.DataBind();
                        }
                    }
                }
            }
        }
    }
}