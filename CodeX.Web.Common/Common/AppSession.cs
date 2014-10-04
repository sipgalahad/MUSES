using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CodeX.Data.Model;

namespace CodeX.Web.Common
{
    public class AppSession
    {
        public static void SetSessionValue(string sessionName, string value)
        {
            sessionName = string.Format("_lgnAttr{0}", sessionName);
            if (HttpContext.Current.Request.Cookies["Muses"] != null)
            {
                HttpCookie myCookie = HttpContext.Current.Request.Cookies["Muses"];
                myCookie.Values[sessionName] = value;
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
            HttpContext.Current.Session[sessionName] = value;
        }
        public static string GetSessionValue(string sessionName)
        {
            sessionName = string.Format("_lgnAttr{0}", sessionName);
            if (HttpContext.Current.Session[sessionName] == null)
            {
                if (HttpContext.Current.Request.Cookies["Muses"] != null)
                {
                    if (HttpContext.Current.Request.Cookies["Muses"][sessionName] != null)
                    {
                        string value = HttpContext.Current.Request.Cookies["Muses"][sessionName];
                        HttpContext.Current.Session[sessionName] = value;
                        return value;
                    }
                }
                return null;
            }
            return HttpContext.Current.Session[sessionName].ToString();
        }

        public static ClassSubjectModel ClassSubject
        {
            get
            {
                if (HttpContext.Current.Session["_ClassSubject"] == null)
                {
                    if (HttpContext.Current.Request.Cookies["Muses"] != null)
                    {
                        if (HttpContext.Current.Request.Cookies["Muses"]["_ClassSubject"] != null)
                        {
                            string[] temp = HttpContext.Current.Request.Cookies["Muses"]["_ClassSubject"].Split('|');
                            ClassSubjectModel entity = new ClassSubjectModel();
                            entity.ClassSubjectID = Convert.ToInt32(temp[0]);
                            entity.ClassScheduleID = Convert.ToInt32(temp[1]);
                            entity.ClassMeetingID = Convert.ToInt32(temp[2]);
                            HttpContext.Current.Session["_ClassSubject"] = entity;
                            return entity;
                        }
                    }
                    return null;
                }
                return ((ClassSubjectModel)(HttpContext.Current.Session["_ClassSubject"]));
            }
            set
            {
                if (HttpContext.Current.Request.Cookies["Muses"] != null)
                {
                    HttpCookie myCookie = HttpContext.Current.Request.Cookies["Muses"];
                    myCookie.Values["_ClassSubject"] = string.Format("{0}|{1}|{2}", value.ClassSubjectID, value.ClassScheduleID, value.ClassMeetingID);
                    HttpContext.Current.Response.Cookies.Add(myCookie);
                }

                HttpContext.Current.Session["_ClassSubject"] = value;
            }
        }

        public static UserLogin UserLogin
        {
            get
            {
                if (HttpContext.Current.Session["_UserName"] == null)
                {
                    if (HttpContext.Current.Request.Cookies["Muses"] != null)
                    {
                        if (HttpContext.Current.Request.Cookies["Muses"]["_UserName"] != null)
                        {
                            string[] temp = HttpContext.Current.Request.Cookies["Muses"]["_UserName"].Split('|');
                            string userID = temp[0];
                            string siteID = temp[1];
                            vUser user = BusinessLayer.GetvUserList(string.Format("UserID = {0} AND IsDeleted = 0", userID)).FirstOrDefault();
                            if (user == null)
                                return null;
                            UserLogin userLogin = new UserLogin();
                            userLogin.UserID = user.UserID;
                            userLogin.UserName = user.UserName;
                            userLogin.SiteID = siteID;
                            if (user.TeacherID > 0)
                                userLogin.UserFullName = user.TeacherName;
                            else
                                userLogin.UserFullName = user.FullName;
                            userLogin.TeacherID = user.TeacherID;

                            List<UserInRole> lstUserSysAdmin = BusinessLayer.GetUserInRoleList(string.Format("UserID = {0} AND SiteID = '{1}' AND RoleID = 1", userLogin.UserID, userLogin.SiteID));
                            userLogin.IsSysAdmin = (lstUserSysAdmin.Count > 0);
                            if (siteID != "")
                                userLogin.SiteName = BusinessLayer.GetSite(siteID).SiteName;

                            HttpContext.Current.Session["_UserName"] = userLogin;
                            return userLogin;
                        }
                    }
                    return null;
                }
                return ((UserLogin)(HttpContext.Current.Session["_UserName"]));
            }
            set
            {
                if (HttpContext.Current.Request.Cookies["Muses"] == null || HttpContext.Current.Request.Cookies["Muses"]["_UserName"] == null)
                {
                    HttpCookie userLoginCookie = new HttpCookie("Muses");
                    userLoginCookie["_UserName"] = string.Format("{0}|{1}", value.UserID, value.SiteID);
                    userLoginCookie.Expires = DateTime.Now.AddDays(1d);
                    HttpContext.Current.Response.Cookies.Add(userLoginCookie);
                }
                else
                {
                    HttpContext.Current.Request.Cookies["Muses"]["_UserName"] = string.Format("{0}|{1}", value.UserID, value.SiteID);
                    HttpContext.Current.Request.Cookies["Muses"].Expires = DateTime.Now.AddDays(1d);
                }
                HttpContext.Current.Session["_UserName"] = value;
            }
        }

        public static void ClearSession()
        {
            if (HttpContext.Current.Request.Cookies["Muses"] != null)
            {
                HttpCookie myCookie = new HttpCookie("Muses");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }

            HttpContext.Current.Session.Clear();
        }

        public static Int32 SchoolPeriodID
        {
            get
            {
                if (HttpContext.Current.Session["_SchoolPeriodID"] == null)
                {
                    if (HttpContext.Current.Request.Cookies["Muses"] != null)
                    {
                        if (HttpContext.Current.Request.Cookies["Muses"]["_SchoolPeriodID"] != null)
                        {
                            int value = Convert.ToInt32(HttpContext.Current.Request.Cookies["Muses"]["_SchoolPeriodID"]);
                            HttpContext.Current.Session["_SchoolPeriodID"] = value;
                            return value;
                        }
                    }
                    return 0;
                }
                return ((Int32)(HttpContext.Current.Session["_SchoolPeriodID"]));
            }
            set
            {
                if (HttpContext.Current.Request.Cookies["Muses"] != null)
                {
                    HttpCookie myCookie = HttpContext.Current.Request.Cookies["Muses"];
                    myCookie.Values["_SchoolPeriodID"] = value.ToString();
                    HttpContext.Current.Response.Cookies.Add(myCookie);
                }
                HttpContext.Current.Session["_SchoolPeriodID"] = value;
            }
        }
    }
}
