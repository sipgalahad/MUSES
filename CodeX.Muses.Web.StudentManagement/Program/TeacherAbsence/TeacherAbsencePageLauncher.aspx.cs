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
    public partial class TeacherAbsencePageLauncher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AppSession.TeacherAbsenceID = Convert.ToInt32(Request.QueryString["id"]);

            string filterExpression = string.Format("ParentCode = '{0}'", Constant.MenuCode.StudentManagement.TEACHER_ABSENCE);
            List<GetUserMenuAccess> lstMenu = BusinessLayer.GetUserMenuAccess(Constant.Module.STUDENT_MANAGEMENT, AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, filterExpression);
            GetUserMenuAccess menu = lstMenu.OrderBy(p => p.MenuIndex).FirstOrDefault();
            Response.Redirect(Page.ResolveUrl(menu.MenuUrl));
        }
    }
}