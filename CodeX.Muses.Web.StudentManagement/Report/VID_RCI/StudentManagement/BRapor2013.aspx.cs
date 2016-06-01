using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Data.Model;
using CodeX.Web.Common;

namespace CodeX.Muses.Web.StudentManagement.Report.VID_RCI.StudentManagement
{
    public partial class BRapor2013 : System.Web.UI.Page
    {
        protected String ResolveUrl1(string url)
        {
            return CurrentDomain() + ResolveUrl(url);
        }
        private string CurrentDomain()
        {
            string currDomain = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host;
            if (Request.Url.Port != 80 && Request.Url.Port != 443)
                currDomain += (":" + Request.Url.Port);
            return currDomain;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            String StudentID = Request.QueryString["id"];
            vStudent st = BusinessLayer.GetvStudentList(String.Format("StudentID = {0}", StudentID))[0];

            vSite site = BusinessLayer.GetvSiteList(String.Format("SiteID = '{0}'", st.SiteID))[0];
            tdSchoolName.InnerHtml = site.SiteName;
            tdSchoolAddress.InnerHtml = String.Format("{0}<br/>Kode Pos : {1} Telepon : {2}", site.StreetName, site.ZipCode, site.PhoneNo1);
            tdSchoolKelurahan.InnerHtml = site.District;
            tdSchoolKecamatan.InnerHtml = site.County;
            tdSchoolCity.InnerHtml = site.City;
            tdSchoolProvince.InnerHtml = site.State;

            divStudentName.InnerHtml = st.StudentName;
            divStudentNIS.InnerHtml = string.Format("{0} / {1}", st.StudentCode, st.NationalStudentNo);

            tdHeaderSchoolName1.InnerHtml = site.SiteName;
            tdHeaderSchoolClassName1.InnerHtml = st.SchoolClassName;
            tdHeaderSchoolAddress1.InnerHtml = "";

            vSchoolClass sc = BusinessLayer.GetvSchoolClassList(string.Format("SchoolClassID = {0}", st.SchoolClassID)).FirstOrDefault();
            tdHeaderSchoolPeriod1.InnerHtml = sc.SchoolPeriodName;
            tdHeaderStudentName1.InnerHtml = st.StudentName;
            tdHeaderStudentCode1.InnerHtml = String.Format("{0} / {1}", st.StudentCode, st.NationalStudentNo);
            //tdHeaderSchoolPeriod1.InnerHtml = sc.PeriodSectionName;
        }
    }
}