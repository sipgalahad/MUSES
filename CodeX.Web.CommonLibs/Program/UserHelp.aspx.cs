using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.Collections;
using DevExpress.Web.ASPxEditors;
using System.Text;
using DevExpress.Web.ASPxCallbackPanel;
using System.IO;
using CodeX.Common;

namespace CodeX.Web.CommonLibs.Program
{
    public partial class UserHelp : BasePage
    {
        string localhostdir = AppConfigManager.CDXUserHelpVirtualDirectory;
        string rootpath = AppConfigManager.CDXUserHelpPhysicalDirectory;
        protected void Page_Load(object sender, EventArgs e)
        {
            //string[] lstDir = Directory.GetDirectories(@"D:\Test\");
            //GetDirectory(0, rootpath);
            PopulateNodes(rootpath, tvwView.Nodes);
        }

        private void PopulateNodes(string path, TreeNodeCollection nodes)
        {
            string[] lstDir = Directory.GetDirectories(path);
            foreach (string test in lstDir)
            {
                string name = test.Remove(0, test.LastIndexOf('\\') + 1);
                TreeNode tn = new TreeNode();
                //tn.Text = string.Format("<div style='font-weight:bold;'>{0}</div>", name);
                tn.Text = name;
                tn.Value = test;
                tn.SelectAction = TreeNodeSelectAction.Expand;
                //divTest.InnerHtml += string.Format("<div style='font-weight:bold;padding-left:{1}0px;'>{0}</div>", name, level);
                //GetDirectory(level + 1, test);
                tn.NavigateUrl = "#";
                nodes.Add(tn);
                tn.PopulateOnDemand = true;
            }
            string[] array1 = Directory.GetFiles(path);
            foreach (string test2 in array1)
            {
                string url = test2.Replace(rootpath, localhostdir).Replace("\\", "/");
                TreeNode tn = new TreeNode();
                tn.Text = Path.GetFileName(test2);
                tn.Value = test2;
                tn.NavigateUrl = url;
                nodes.Add(tn);
                //divTest.InnerHtml += string.Format("<div class='divFile' style='padding-left:{1}0px;' url='{2}'>{0}</div>", Path.GetFileName(test2), level, url);
                tn.PopulateOnDemand = false;
            }
        }

        private void PopulateSubLevel(String path, TreeNode parentNode)
        {
            PopulateNodes(path, parentNode.ChildNodes);
        }

        protected void tvwView_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            PopulateSubLevel(e.Node.Value.ToString(), e.Node);
        }
    }
}