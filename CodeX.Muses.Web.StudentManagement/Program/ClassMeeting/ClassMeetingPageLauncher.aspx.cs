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
    public partial class ClassMeetingPageLauncher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] temp = Request.QueryString["id"].Split('|');
            if (temp[0] == "tcs")
            {
                ClassSubjectModel classSubject = new ClassSubjectModel();
                classSubject.PeriodSectionID = Convert.ToInt32(temp[1]);
                classSubject.ClassSubjectID = Convert.ToInt32(temp[2]);
                classSubject.ClassScheduleID = 0;
                classSubject.ClassMeetingID = 0;
                classSubject.IsMarkEntry = true;
                vPeriodSection entityPeriodSection = BusinessLayer.GetvPeriodSectionList(string.Format("PeriodSectionID = {0}", classSubject.PeriodSectionID)).FirstOrDefault();
                classSubject.CurriculumID = entityPeriodSection.CurriculumID;
                classSubject.GCPeriodSection = entityPeriodSection.GCPeriodSection;

                AppSession.ClassSubject = classSubject;

                string filterExpression = string.Format("ParentCode = '{0}'", Constant.MenuCode.StudentManagement.TEACHER_CLASS_SUBJECT_PAGE);
                List<GetUserMenuAccess> lstMenu = BusinessLayer.GetUserMenuAccess(Constant.Module.STUDENT_MANAGEMENT, AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, filterExpression);
                int parentID = (int)lstMenu.Where(p => p.MenuIndex > 0).OrderBy(p => p.MenuIndex).FirstOrDefault().MenuID;

                filterExpression = string.Format("ParentID = {0}", parentID);
                lstMenu = BusinessLayer.GetUserMenuAccess(Constant.Module.STUDENT_MANAGEMENT, AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, filterExpression);
                GetUserMenuAccess menu = lstMenu.OrderBy(p => p.MenuIndex).FirstOrDefault();
                Response.Redirect(Page.ResolveUrl(menu.MenuUrl));
            }
            else
            {
                ClassSubjectModel classSubject = new ClassSubjectModel();
                classSubject.PeriodSectionID = Convert.ToInt32(temp[0]);
                classSubject.ClassSubjectID = Convert.ToInt32(temp[1]);
                classSubject.ClassScheduleID = Convert.ToInt32(temp[2]);
                classSubject.ClassMeetingID = Convert.ToInt32(temp[3]);
                classSubject.IsMarkEntry = false;
                vPeriodSection entityPeriodSection = BusinessLayer.GetvPeriodSectionList(string.Format("PeriodSectionID = {0}", classSubject.PeriodSectionID)).FirstOrDefault();
                classSubject.CurriculumID = entityPeriodSection.CurriculumID;
                classSubject.GCPeriodSection = entityPeriodSection.GCPeriodSection;
                AppSession.ClassSubject = classSubject;

                string filterExpression = string.Format("ParentCode = '{0}'", Constant.MenuCode.StudentManagement.CLASS_MEETING_PAGE);
                List<GetUserMenuAccess> lstMenu = BusinessLayer.GetUserMenuAccess(Constant.Module.STUDENT_MANAGEMENT, AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, filterExpression);
                int parentID = (int)lstMenu.Where(p => p.MenuIndex > 0).OrderBy(p => p.MenuIndex).FirstOrDefault().MenuID;

                filterExpression = string.Format("ParentID = {0}", parentID);
                lstMenu = BusinessLayer.GetUserMenuAccess(Constant.Module.STUDENT_MANAGEMENT, AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, filterExpression);
                GetUserMenuAccess menu = lstMenu.OrderBy(p => p.MenuIndex).FirstOrDefault();
                Response.Redirect(Page.ResolveUrl(menu.MenuUrl));
            }
        }
    }
}