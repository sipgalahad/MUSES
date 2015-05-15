using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Data.Model;
using CodeX.Web.Common;
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class SubjectPageLauncher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] temp = Request.QueryString["id"].Split('|');
            SubjectModel subject = new SubjectModel();
            subject.SubjectID = Convert.ToInt32(temp[0]);
            subject.GCSchoolType = temp[1];
            AppSession.Subject = subject;

            string filterExpression = string.Format("ParentCode = '{0}'", Constant.MenuCode.ControlPanel.SUBJECT_PAGE);
            List<GetUserMenuAccess> lstMenu = BusinessLayer.GetUserMenuAccess(Constant.Module.CONTROL_PANEL, AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, filterExpression);
            GetUserMenuAccess menu = lstMenu.OrderBy(p => p.MenuIndex).FirstOrDefault();
            Response.Redirect(Page.ResolveUrl(menu.MenuUrl));
        }
    }
}