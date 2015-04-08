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
        #region Utility
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
        #endregion

        #region ClassStudentModel
        public static ClassStudentModel ClassStudent
        {
            get
            {
                if (HttpContext.Current.Session["_ClassStudent"] == null)
                {
                    if (HttpContext.Current.Request.Cookies["Muses"] != null)
                    {
                        if (HttpContext.Current.Request.Cookies["Muses"]["_ClassStudent"] != null)
                        {
                            string[] temp = HttpContext.Current.Request.Cookies["Muses"]["_ClassStudent"].Split('|');
                            ClassStudentModel entity = new ClassStudentModel();
                            entity.SchoolClassID = Convert.ToInt32(temp[0]);
                            entity.StudentID = Convert.ToInt32(temp[1]);
                            entity.PeriodSectionID = Convert.ToInt32(temp[2]);
                            HttpContext.Current.Session["_ClassStudent"] = entity;
                            return entity;
                        }
                    }
                    return null;
                }
                return ((ClassStudentModel)(HttpContext.Current.Session["_ClassStudent"]));
            }
            set
            {
                if (HttpContext.Current.Request.Cookies["Muses"] != null)
                {
                    HttpCookie myCookie = HttpContext.Current.Request.Cookies["Muses"];
                    myCookie.Values["_ClassStudent"] = string.Format("{0}|{1}|{2}", value.SchoolClassID, value.StudentID, value.PeriodSectionID);
                    HttpContext.Current.Response.Cookies.Add(myCookie);
                }

                HttpContext.Current.Session["_ClassStudent"] = value;
            }
        }
        #endregion

        #region ClassSubject
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
                            entity.PeriodSectionID = Convert.ToInt32(temp[0]);
                            entity.ClassSubjectID = Convert.ToInt32(temp[1]);
                            entity.ClassScheduleID = Convert.ToInt32(temp[2]);
                            entity.ClassMeetingID = Convert.ToInt32(temp[3]);
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
                    myCookie.Values["_ClassSubject"] = string.Format("{0}|{1}|{2}|{3}", value.PeriodSectionID, value.ClassSubjectID, value.ClassScheduleID, value.ClassMeetingID);
                    HttpContext.Current.Response.Cookies.Add(myCookie);
                }

                HttpContext.Current.Session["_ClassSubject"] = value;
            }
        }
        #endregion
        
        #region UserLogin
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
                            if (user.EmployeeID > 0)
                                userLogin.UserFullName = user.EmployeeName;
                            else
                                userLogin.UserFullName = user.FullName;
                            userLogin.EmployeeID = user.EmployeeID;

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
        #endregion

        #region FixedAssetID
        public static Int32 FixedAssetID
        {
            get
            {
                if (HttpContext.Current.Session["_FixedAssetID"] == null)
                {
                    if (HttpContext.Current.Request.Cookies["Muses"] != null)
                    {
                        if (HttpContext.Current.Request.Cookies["Muses"]["_FixedAssetID"] != null)
                        {
                            int value = Convert.ToInt32(HttpContext.Current.Request.Cookies["Muses"]["_FixedAssetID"]);
                            HttpContext.Current.Session["_FixedAssetID"] = value;
                            return value;
                        }
                    }
                    return 0;
                }
                return ((Int32)(HttpContext.Current.Session["_FixedAssetID"]));
            }
            set
            {
                if (HttpContext.Current.Request.Cookies["Muses"] != null)
                {
                    HttpCookie myCookie = HttpContext.Current.Request.Cookies["Muses"];
                    myCookie.Values["_FixedAssetID"] = value.ToString();
                    HttpContext.Current.Response.Cookies.Add(myCookie);
                }
                HttpContext.Current.Session["_FixedAssetID"] = value;
            }
        }
        #endregion

        #region ProspectiveStudentID
        public static Int32 ProspectiveStudentID
        {
            get
            {
                if (HttpContext.Current.Session["_ProspectiveStudentID"] == null)
                {
                    if (HttpContext.Current.Request.Cookies["Muses"] != null)
                    {
                        if (HttpContext.Current.Request.Cookies["Muses"]["_ProspectiveStudentID"] != null)
                        {
                            int value = Convert.ToInt32(HttpContext.Current.Request.Cookies["Muses"]["_ProspectiveStudentID"]);
                            HttpContext.Current.Session["_ProspectiveStudentID"] = value;
                            return value;
                        }
                    }
                    return 0;
                }
                return ((Int32)(HttpContext.Current.Session["_ProspectiveStudentID"]));
            }
            set
            {
                if (HttpContext.Current.Request.Cookies["Muses"] != null)
                {
                    HttpCookie myCookie = HttpContext.Current.Request.Cookies["Muses"];
                    myCookie.Values["_ProspectiveStudentID"] = value.ToString();
                    HttpContext.Current.Response.Cookies.Add(myCookie);
                }
                HttpContext.Current.Session["_ProspectiveStudentID"] = value;
            }
        }
        #endregion

        #region SchoolPeriodID
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
        #endregion

        #region StudentID
        public static Int32 StudentID
        {
            get
            {
                if (HttpContext.Current.Session["_StudentID"] == null)
                {
                    if (HttpContext.Current.Request.Cookies["Muses"] != null)
                    {
                        if (HttpContext.Current.Request.Cookies["Muses"]["_StudentID"] != null)
                        {
                            int value = Convert.ToInt32(HttpContext.Current.Request.Cookies["Muses"]["_StudentID"]);
                            HttpContext.Current.Session["_StudentID"] = value;
                            return value;
                        }
                    }
                    return 0;
                }
                return ((Int32)(HttpContext.Current.Session["_StudentID"]));
            }
            set
            {
                if (HttpContext.Current.Request.Cookies["Muses"] != null)
                {
                    HttpCookie myCookie = HttpContext.Current.Request.Cookies["Muses"];
                    myCookie.Values["_StudentID"] = value.ToString();
                    HttpContext.Current.Response.Cookies.Add(myCookie);
                }
                HttpContext.Current.Session["_StudentID"] = value;
            }
        }
        #endregion

        #region SubjectID
        public static Int32 SubjectID
        {
            get
            {
                if (HttpContext.Current.Session["_SubjectID"] == null)
                {
                    if (HttpContext.Current.Request.Cookies["Muses"] != null)
                    {
                        if (HttpContext.Current.Request.Cookies["Muses"]["_SubjectID"] != null)
                        {
                            int value = Convert.ToInt32(HttpContext.Current.Request.Cookies["Muses"]["_SubjectID"]);
                            HttpContext.Current.Session["_SubjectID"] = value;
                            return value;
                        }
                    }
                    return 0;
                }
                return ((Int32)(HttpContext.Current.Session["_SubjectID"]));
            }
            set
            {
                if (HttpContext.Current.Request.Cookies["Muses"] != null)
                {
                    HttpCookie myCookie = HttpContext.Current.Request.Cookies["Muses"];
                    myCookie.Values["_SubjectID"] = value.ToString();
                    HttpContext.Current.Response.Cookies.Add(myCookie);
                }
                HttpContext.Current.Session["_SubjectID"] = value;
            }
        }
        #endregion

        #region SubjectMatterID
        public static Int32 SubjectMatterID
        {
            get
            {
                if (HttpContext.Current.Session["_SubjectMatterID"] == null)
                {
                    if (HttpContext.Current.Request.Cookies["Muses"] != null)
                    {
                        if (HttpContext.Current.Request.Cookies["Muses"]["_SubjectMatterID"] != null)
                        {
                            int value = Convert.ToInt32(HttpContext.Current.Request.Cookies["Muses"]["_SubjectMatterID"]);
                            HttpContext.Current.Session["_SubjectMatterID"] = value;
                            return value;
                        }
                    }
                    return 0;
                }
                return ((Int32)(HttpContext.Current.Session["_SubjectMatterID"]));
            }
            set
            {
                if (HttpContext.Current.Request.Cookies["Muses"] != null)
                {
                    HttpCookie myCookie = HttpContext.Current.Request.Cookies["Muses"];
                    myCookie.Values["_SubjectMatterID"] = value.ToString();
                    HttpContext.Current.Response.Cookies.Add(myCookie);
                }
                HttpContext.Current.Session["_SubjectMatterID"] = value;
            }
        }
        #endregion

        #region ListModuleID
        public static String ListModuleID
        {
            get
            {
                if (HttpContext.Current.Session["_ListModuleID"] == null)
                {
                    if (HttpContext.Current.Request.Cookies["Muses"] != null)
                    {
                        if (HttpContext.Current.Request.Cookies["Muses"]["_ListModuleID"] != null)
                        {
                            String value = HttpContext.Current.Request.Cookies["Muses"]["_ListModuleID"];
                            HttpContext.Current.Session["_ListModuleID"] = value;
                            return value;
                        }
                    }
                    return "";
                }
                return HttpContext.Current.Session["_ListModuleID"].ToString();
            }
            set
            {
                if (HttpContext.Current.Request.Cookies["Muses"] != null)
                {
                    HttpCookie myCookie = HttpContext.Current.Request.Cookies["Muses"];
                    myCookie.Values["_ListModuleID"] = value.ToString();
                    HttpContext.Current.Response.Cookies.Add(myCookie);
                }
                HttpContext.Current.Session["_ListModuleID"] = value;
            }
        }
        #endregion

        #region BusinessPartnerID
        public static Int32 BusinessPartnerID
        {
            get
            {
                if (HttpContext.Current.Session["_BusinessPartnerID"] == null)
                {
                    if (HttpContext.Current.Request.Cookies["Muses"] != null)
                    {
                        if (HttpContext.Current.Request.Cookies["Muses"]["_BusinessPartnerID"] != null)
                        {
                            int value = Convert.ToInt32(HttpContext.Current.Request.Cookies["Muses"]["_BusinessPartnerID"]);
                            HttpContext.Current.Session["_BusinessPartnerID"] = value;
                            return value;
                        }
                    }
                    return 0;
                }
                return ((Int32)(HttpContext.Current.Session["_BusinessPartnerID"]));
            }
            set
            {
                if (HttpContext.Current.Request.Cookies["Muses"] != null)
                {
                    HttpCookie myCookie = HttpContext.Current.Request.Cookies["Muses"];
                    myCookie.Values["_BusinessPartnerID"] = value.ToString();
                    HttpContext.Current.Response.Cookies.Add(myCookie);
                }
                HttpContext.Current.Session["_BusinessPartnerID"] = value;
            }
        }
        #endregion

        #region PeriodAdmissionID
        public static Int32 PeriodAdmissionID
        {
            get
            {
                if (HttpContext.Current.Session["_PeriodAdmissionID"] == null)
                {
                    if (HttpContext.Current.Request.Cookies["Muses"] != null)
                    {
                        if (HttpContext.Current.Request.Cookies["Muses"]["_PeriodAdmissionID"] != null)
                        {
                            int value = Convert.ToInt32(HttpContext.Current.Request.Cookies["Muses"]["_PeriodAdmissionID"]);
                            HttpContext.Current.Session["_PeriodAdmissionID"] = value;
                            return value;
                        }
                    }
                    return 0;
                }
                return ((Int32)(HttpContext.Current.Session["_PeriodAdmissionID"]));
            }
            set
            {
                if (HttpContext.Current.Request.Cookies["Muses"] != null)
                {
                    HttpCookie myCookie = HttpContext.Current.Request.Cookies["Muses"];
                    myCookie.Values["_PeriodAdmissionID"] = value.ToString();
                    HttpContext.Current.Response.Cookies.Add(myCookie);
                }
                HttpContext.Current.Session["_PeriodAdmissionID"] = value;
            }
        }
        #endregion

        #region SiteID
        public static String SiteID
        {
            get
            {
                if (HttpContext.Current.Session["_SiteID"] == null)
                {
                    if (HttpContext.Current.Request.Cookies["Muses"] != null)
                    {
                        if (HttpContext.Current.Request.Cookies["Muses"]["_SiteID"] != null)
                        {
                            String value = HttpContext.Current.Request.Cookies["Muses"]["_SiteID"].ToString();
                            HttpContext.Current.Session["_SiteID"] = value;
                            return value;
                        }
                    }
                    return "";
                }
                return HttpContext.Current.Session["_SiteID"].ToString();
            }
            set
            {
                if (HttpContext.Current.Request.Cookies["Muses"] != null)
                {
                    HttpCookie myCookie = HttpContext.Current.Request.Cookies["Muses"];
                    myCookie.Values["_SiteID"] = value;
                    HttpContext.Current.Response.Cookies.Add(myCookie);
                }
                HttpContext.Current.Session["_SiteID"] = value;
            }
        }
        #endregion
    }
}
