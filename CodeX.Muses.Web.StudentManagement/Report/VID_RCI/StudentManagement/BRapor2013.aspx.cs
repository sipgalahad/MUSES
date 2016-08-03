using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Data.Model;
using CodeX.Web.Common;
using CodeX.Common;
using System.Web.UI.HtmlControls;

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
            string param = Request.QueryString["id"];
            string[] temp = param.Split('|');
            String StudentID = temp[0];
            String ClassID = temp[1];
            String PeriodSectionID = temp[2];
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
            tdHeaderSchoolAddress1.InnerHtml = site.StreetName;
            tdHeaderStudentName1.InnerHtml = st.StudentName;
            tdHeaderStudentCode1.InnerHtml = String.Format("{0} / {1}", st.StudentCode, st.NationalStudentNo);
            //tdHeaderSchoolPeriod1.InnerHtml = sc.PeriodSectionName;


            tdHeaderSchoolPeriod2.InnerHtml = sc.SchoolPeriodName;
            tdHeaderSchoolAddress2.InnerHtml = site.StreetName;
            tdHeaderStudentName2.InnerHtml = st.StudentName;
            tdHeaderStudentCode2.InnerHtml = String.Format("{0} / {1}", st.StudentCode, st.NationalStudentNo);

            List<vClassSubject> lstSubject = BusinessLayer.GetvClassSubjectList(string.Format("SchoolClassID = {0} AND IsDeleted = 0", ClassID));
            List<vClassSubject> lstSubjectPersonality = lstSubject.Where(p => p.SubjectGCClassStudyType == Constant.ClassStudyType.PERSONALITY).ToList();

            string lstClassSubjectID = string.Join(",", lstSubject.Select(p => p.ClassSubjectID).ToList());
            if (lstClassSubjectID != "")
                lstMark = BusinessLayer.GetvClassStudentSubjectMarkList(String.Format("ClassSubjectID IN ({0}) AND StudentID = {1} AND PeriodSectionID = {2}", lstClassSubjectID, StudentID, PeriodSectionID));
            else
                lstMark = new List<vClassStudentSubjectMark>();

            rptPersonality.DataSource = lstSubjectPersonality;
            rptPersonality.DataBind();
        }

        List<vClassStudentSubjectMark> lstMark = null;
        protected void rptPersonality_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubject entity = (vClassSubject)e.Item.DataItem;
                vClassStudentSubjectMark entityMark = lstMark.FirstOrDefault(p => p.ClassSubjectID == entity.ClassSubjectID);

                HtmlGenericControl divPredicate = (HtmlGenericControl)e.Item.FindControl("divPredicate");
                HtmlGenericControl divRemarks = (HtmlGenericControl)e.Item.FindControl("divRemarks");
                if (entityMark != null)
                {
                    divPredicate.InnerHtml = entityMark.PredicateMarkTypeDtName;
                    divRemarks.InnerHtml = entityMark.DescriptionMark;
                }
            }
        }
    }
}