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

        protected string OnGetMenuCode()
        {
            return BasePageContent.OnGetMenuCode();
        }
    }
}