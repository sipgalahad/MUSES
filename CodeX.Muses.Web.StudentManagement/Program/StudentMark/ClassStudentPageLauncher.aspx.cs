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
    public partial class ClassStudentPageLauncher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] temp = Request.QueryString["id"].Split('|');
            ClassStudentModel classStudent = new ClassStudentModel();
            classStudent.SchoolClassID = Convert.ToInt32(temp[0]);
            classStudent.StudentID = Convert.ToInt32(temp[1]);
            classStudent.PeriodSectionID = Convert.ToInt32(temp[2]);
            AppSession.ClassStudent = classStudent;

            string filterExpression = string.Format("ParentCode = '{0}'", Constant.MenuCode.StudentManagement.CLASS_STUDENT_PAGE);
            List<GetUserMenuAccess> lstMenu = BusinessLayer.GetUserMenuAccess(Constant.Module.STUDENT_MANAGEMENT, AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, filterExpression);
            int parentID = (int)lstMenu.Where(p => p.MenuIndex > 0).OrderBy(p => p.MenuIndex).FirstOrDefault().MenuID;

            filterExpression = string.Format("ParentID = {0}", parentID);
            lstMenu = BusinessLayer.GetUserMenuAccess(Constant.Module.STUDENT_MANAGEMENT, AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, filterExpression);
            GetUserMenuAccess menu = lstMenu.OrderBy(p => p.MenuIndex).FirstOrDefault();
            Response.Redirect(Page.ResolveUrl(menu.MenuUrl));
        }
    }
}