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
using CodeX.Common;

namespace CodeX.Web.CommonLibs.MasterPage
{
    public partial class MPBase : System.Web.UI.MasterPage
    {
        protected string OnGetTitle()
        {
            return "VIDA";
        }
        protected string OnGetAppIcon()
        {
            return "../Images/AppIcon/general/icon.ico?1";
        }
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
                cssCodex.Href = cssCodex.Href.Replace("[Themes]", config.Themes);
                cssJquery.Href = cssJquery.Href.Replace("[Themes]", config.Themes);
                AddIcon(OnGetAppIcon());
            }
        }

        private void AddIcon(string href)
        {
            //HtmlHead head = (HtmlHead)Page.Header;
            HtmlLink link = new HtmlLink();
            link.Attributes.Add("href", href);
            link.Attributes.Add("type", "image/gif");
            link.Attributes.Add("rel", "icon");
            //head.Controls.Add(link);
            divStyle.Controls.Add(link);
        }

        private void AddLink(string href)
        {
            //HtmlHead head = (HtmlHead)Page.Header;
            HtmlLink link = new HtmlLink();
            link.Attributes.Add("href", href);
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            //head.Controls.Add(link);
            divStyle.Controls.Add(link);
        }
    }
}