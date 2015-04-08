using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Data.Model;
using CodeX.Web.Common;
using CodeX.Common;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class SubjectPageLauncher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] temp = Request.QueryString["id"].Split('|');
            AppSession.SubjectID = Convert.ToInt32(temp[0]);
            string filterExpression = "";
            if (temp.Count() > 1)
            {
                AppSession.SubjectMatterID = Convert.ToInt32(temp[1]);
                filterExpression = string.Format("ParentCode = '{0}'", Constant.MenuCode.StudentManagement.SUBJECT_MATTER_PAGE);
            }
            else
            {
                AppSession.SubjectMatterID = 0;
                filterExpression = string.Format("ParentCode = '{0}'", Constant.MenuCode.StudentManagement.SUBJECT_PAGE);
            }
            
            List<GetUserMenuAccess> lstMenu = BusinessLayer.GetUserMenuAccess(Constant.Module.STUDENT_MANAGEMENT, AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, filterExpression);
            GetUserMenuAccess menu = lstMenu.OrderBy(p => p.MenuIndex).FirstOrDefault();
            Response.Redirect(Page.ResolveUrl(menu.MenuUrl));
        }
    }
}