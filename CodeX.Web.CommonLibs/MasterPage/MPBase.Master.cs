using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using CodeX.Web.Common;
using System.Web.UI.HtmlControls;
using CodeX.Data.Model;

namespace CodeX.Web.CommonLibs.MasterPage
{
    public partial class MPBase : System.Web.UI.MasterPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!Page.IsPostBack)
            {
                XDocument xdoc = Helper.LoadXMLFile(this, "config.xml");
                var config = (from pg in xdoc.Descendants("page")
                              select new
                              {
                                  Themes = pg.Attribute("themes").Value
                              }).FirstOrDefault();
                //string themes = AppConfigManager.QISThemes;
                AddLink(string.Format("../Styles/{0}/codex.css", config.Themes));
                AddLink(string.Format("../Styles/{0}/jquery/jquery.ui.theme.css", config.Themes));
            }
        }

        private void AddLink(string href)
        {
            HtmlHead head = (HtmlHead)Page.Header;
            HtmlLink link = new HtmlLink();
            link.Attributes.Add("href", href);
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            head.Controls.Add(link);
        }
    }
}