using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using System.Web.UI.HtmlControls;
using CodeX.Web.Common;
using CodeX.Common;

namespace CodeX.Muses.Web.StudentManagement.Report
{
    public partial class BRaporSemester2013Rpt : BaseCustomReportCtl
    {
        private Int32 SchoolPeriodID = 0;
        private Int32 PeriodSectionID = 0;
        private Int32 SchoolClassID = 0;
        private Int32 StudentID = 0;

        private string HeadMaster = "";
        
        List<vClassSubjectTask> lstClassSubjectTask = null;
        List<vClassStudentSubjectMark> lstNilai = null;
        List<OrganizationHd> lstOrganizationHd = new List<OrganizationHd>();
        List<vOrganizationDt> lstOrganizationDt = new List<vOrganizationDt>();
        List<vOrganizationDtStudent> lstOrganizationDtStudent = new List<vOrganizationDtStudent>();
        List<vClassStudent> lstClassStudent = new List<vClassStudent>();
        List<ClassStudentMark> lstClassStudentMark = null;
        List<vClassSubject> lstClassSubject = null;
        String lstClassSubjectID = "";

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        public override void Bind(string filterExpression, string[] param)
        {
            #region Initialization
            List<Int32> lstStudentID = new List<Int32>();
            SchoolPeriodID = Convert.ToInt32(param[0]);
            PeriodSectionID = Convert.ToInt32(param[1]);
            SchoolClassID = Convert.ToInt32(param[2]);

            if (param.Count() > 3)
            {
                lstStudentID.Add(Convert.ToInt32(param[3]));
                lstOrganizationDt.AddRange(BusinessLayer.GetvOrganizationDtList(String.Format("SchoolPeriodID = {0} AND StudentCoordinatorID = {1}", SchoolPeriodID, param[3])));
                lstOrganizationDtStudent.AddRange(BusinessLayer.GetvOrganizationDtStudentList(String.Format("SchoolPeriodID = {0} AND StudentID = {1}", SchoolPeriodID, param[3])));
                lstClassStudent = BusinessLayer.GetvClassStudentList(String.Format("StudentID = {0} OR (GCClassStudyType = '{1}' AND StudentID = {0})", param[3], Constant.ClassStudyType.EXTRACURRICULAR));
                lstClassStudentMark = BusinessLayer.GetClassStudentMarkList(string.Format("SchoolClassID = {0} AND PeriodSectionID = {1} AND StudentID = {2}", SchoolClassID, PeriodSectionID, param[3]));
            } 
            else 
            {
                lstClassStudent = BusinessLayer.GetvClassStudentList(String.Format("SchoolClassID = {0} OR GCClassStudyType = '{1}'", SchoolClassID, Constant.ClassStudyType.EXTRACURRICULAR));
                lstStudentID.AddRange(lstClassStudent.GroupBy(s => s.StudentID).Select(x => x.Key));
                String lst = String.Join(",", lstStudentID);
                lstOrganizationDt.AddRange(BusinessLayer.GetvOrganizationDtList(String.Format("SchoolPeriodID = {0} AND StudentCoordinatorID IN ({1})", SchoolPeriodID, lst)));
                lstOrganizationDtStudent.AddRange(BusinessLayer.GetvOrganizationDtStudentList(String.Format("SchoolPeriodID = {0} AND StudentID IN ({1})", SchoolPeriodID, lst)));
                lstClassStudentMark = BusinessLayer.GetClassStudentMarkList(string.Format("SchoolClassID = {0} AND PeriodSectionID = {1}", SchoolClassID, PeriodSectionID));
            }
            String lstSchoolClassID = String.Join(",", lstClassStudent.GroupBy(s => s.SchoolClassID).Select(x => x.Key));
            lstClassSubject = BusinessLayer.GetvClassSubjectList(String.Format("SchoolPeriodID = {0} AND SchoolClassID IN ({1}) AND IsDeleted = 0 AND ParentID IS NULL", SchoolPeriodID, lstSchoolClassID));
            lstClassSubjectID = String.Join(",", lstClassSubject.Select(x => x.ClassSubjectID));
            lstOrganizationHd = BusinessLayer.GetOrganizationHdList(string.Format("SchoolPeriodID = {0} AND IsAllStudentAsMember = 1 AND IsDeleted = 0", SchoolPeriodID));

            HeadMaster = BusinessLayer.GetSiteParameter(AppSession.UserLogin.SiteID, Constant.SiteParameter.HEADMASTER).ParameterValue;

            rptStudent.DataSource = lstStudentID;
            rptStudent.DataBind();
            #endregion
        }
        int SubjectRowCount = 0;
        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                StudentID = (Int32)e.Item.DataItem;
                
                #region initialization
                HtmlGenericControl divSchool = e.Item.FindControl("divSchool") as HtmlGenericControl;
                HtmlGenericControl divPersonal = e.Item.FindControl("divPersonal") as HtmlGenericControl;
                HtmlGenericControl divRapor = e.Item.FindControl("divRapor") as HtmlGenericControl;

                HtmlTableCell tdStudentName = e.Item.FindControl("tdStudentName") as HtmlTableCell;
                HtmlTableCell tdNIS = e.Item.FindControl("tdNIS") as HtmlTableCell;
                HtmlTableCell tdClass = e.Item.FindControl("tdClass") as HtmlTableCell;
                HtmlTableCell tdPeriodSection = e.Item.FindControl("tdPeriodSection") as HtmlTableCell;
                HtmlTableCell tdSchoolPeriod = e.Item.FindControl("tdSchoolPeriod") as HtmlTableCell;
                HtmlTableCell tdSchoolName = e.Item.FindControl("tdSchoolName") as HtmlTableCell;
                HtmlTableCell tdSchoolAddress = e.Item.FindControl("tdSchoolAddress") as HtmlTableCell;

                HtmlTableCell tdStudentName1 = e.Item.FindControl("tdStudentName1") as HtmlTableCell;
                HtmlTableCell tdNIS1 = e.Item.FindControl("tdNIS1") as HtmlTableCell;
                HtmlTableCell tdClass1 = e.Item.FindControl("tdClass1") as HtmlTableCell;
                HtmlTableCell tdSchoolPeriod1 = e.Item.FindControl("tdSchoolPeriod1") as HtmlTableCell;
                HtmlTableCell tdSchoolName1 = e.Item.FindControl("tdSchoolName1") as HtmlTableCell;
                HtmlTableCell tdPeriodSection1 = e.Item.FindControl("tdPeriodSection1") as HtmlTableCell;
                HtmlTableCell tdSchoolAddress1 = e.Item.FindControl("tdSchoolAddress1") as HtmlTableCell;

                HtmlTableCell tdSick = e.Item.FindControl("tdSick") as HtmlTableCell;
                HtmlTableCell tdPermit = e.Item.FindControl("tdPermit") as HtmlTableCell;
                HtmlTableCell tdAlpha = e.Item.FindControl("tdAlpha") as HtmlTableCell;
                HtmlTableCell tdHeaderHasil = e.Item.FindControl("tdHeaderHasil") as HtmlTableCell;

                HtmlTableCell tdAchStudentName = e.Item.FindControl("tdAchStudentName") as HtmlTableCell;
                HtmlTableCell tdAchSchoolName  = e.Item.FindControl("tdAchSchoolName") as HtmlTableCell;
                HtmlTableCell tdAchNIS = e.Item.FindControl("tdAchNIS") as HtmlTableCell;

                HtmlTableCell tdFooterDateNow = e.Item.FindControl("tdFooterDateNow") as HtmlTableCell;
                HtmlTableCell tdFooterStudentParent = e.Item.FindControl("tdFooterStudentParent") as HtmlTableCell;
                HtmlTableCell tdFooterWali = e.Item.FindControl("tdFooterWali") as HtmlTableCell;

                HtmlTableCell tdFooterDateNow1 = e.Item.FindControl("tdFooterDateNow1") as HtmlTableCell;
                HtmlTableCell tdFooterStudentParent1 = e.Item.FindControl("tdFooterStudentParent1") as HtmlTableCell;
                HtmlTableCell tdFooterWali1 = e.Item.FindControl("tdFooterWali1") as HtmlTableCell;
                
                Repeater rptCurriculumSubjectGroupName = e.Item.FindControl("rptCurriculumSubjectGroupName") as Repeater;
                Repeater rptCurriculumSubjectGroupName1 = e.Item.FindControl("rptCurriculumSubjectGroupName1") as Repeater;
                Repeater rptPersonality = e.Item.FindControl("rptPersonality") as Repeater;
                Repeater rptOrganization = e.Item.FindControl("rptOrganization") as Repeater;
                Repeater rptEskul = e.Item.FindControl("rptEskul") as Repeater;
                Repeater rptAchievement = e.Item.FindControl("rptAchievement") as Repeater;
                #endregion

                #region Personal Data
                vStudent st = BusinessLayer.GetvStudentList(String.Format("StudentID = {0}",StudentID))[0];
                List<StudentPastStudy> lstSps = BusinessLayer.GetStudentPastStudyList(String.Format("StudentID = {0} AND IsDeleted = 0", StudentID));
                Registration reg = BusinessLayer.GetRegistrationList(String.Format("RegistrationID = {0}", st.RegistrationID))[0];

                String raporHeader = divRapor.InnerHtml;
                raporHeader = raporHeader.Replace("{StudentName}", st.StudentName);
                raporHeader = raporHeader.Replace("{StudentNIS}", String.Format("{0} / {1}", st.StudentCode, st.NationalStudentNo));
                divRapor.InnerHtml = raporHeader;

                String personalText = divPersonal.InnerHtml;
                tdAchStudentName.InnerHtml = st.StudentName;
                tdAchNIS.InnerHtml = String.Format("{0} / {1}", st.StudentCode, st.NationalStudentNo);

                personalText = personalText.Replace("{StudentName}", st.StudentName);
                personalText = personalText.Replace("{NIS}", String.Format("{0} / {1}", st.StudentCode, st.NationalStudentNo));
                personalText = personalText.Replace("{DOB}", String.Format("{0}, {1}",st.CityOfBirth, st.DateOfBirthInString));
                personalText = personalText.Replace("{Gender}", st.Gender);
                personalText = personalText.Replace("{Religion}", st.Religion);
                personalText = personalText.Replace("{Address}", st.StreetName);
                personalText = personalText.Replace("{City}", st.City);
                personalText = personalText.Replace("{PhoneNo}", st.PhoneNo1);
                personalText = personalText.Replace("{Grade}", st.Grade);
                personalText = personalText.Replace("{AcceptedDate}", reg.AcceptedDate.ToString(Constant.FormatString.DATE_FORMAT));
                if (lstSps.Count > 0)
                    personalText = personalText.Replace("{PastSchool}", lstSps[0].SchoolName);
                else
                    personalText = personalText.Replace("{PastSchool}", "-");
                
                List<vStudentFamily> lstFamily = BusinessLayer.GetvStudentFamilyList(String.Format("StudentID = {0} AND GCFamilyRelation IN ('{0}','{1}') ", StudentID,Constant.FamilyRelation.FATHER, Constant.FamilyRelation.MOTHER));
                vStudentFamily father = lstFamily.FirstOrDefault(x => x.GCFamilyRelation == Constant.FamilyRelation.FATHER);
                vStudentFamily mother = lstFamily.FirstOrDefault(x => x.GCFamilyRelation == Constant.FamilyRelation.MOTHER);
                if (father != null)
                {
                    personalText = personalText.Replace("{FatherName}", father.FullName);
                    tdFooterStudentParent1.InnerHtml = tdFooterStudentParent.InnerHtml = father.FullName;
                    personalText = personalText.Replace("{FatherEducationLevel}", father.EducationLevel);
                    personalText = personalText.Replace("{FatherJob}", father.Job);
                    personalText = personalText.Replace("{ParentAddress}", father.HomeStreetName);
                    personalText = personalText.Replace("{ParentCity}", father.HomeCity);
                    personalText = personalText.Replace("{ParentPhoneNo}", father.HomePhoneNo1);
                }
                else
                {
                    personalText = personalText.Replace("{FatherName}", "-");
                    personalText = personalText.Replace("{FatherEducationLevel}", "-");
                    personalText = personalText.Replace("{FatherJob}", "-");
                }

                if (mother != null)
                {
                    personalText = personalText.Replace("{MotherName}", mother.FullName);
                    tdFooterStudentParent1.InnerHtml = tdFooterStudentParent.InnerHtml = mother.FullName;
                    personalText = personalText.Replace("{MotherEducationLevel}", mother.EducationLevel);
                    personalText = personalText.Replace("{MotherJob}", mother.Job);
                    personalText = personalText.Replace("{ParentAddress}", mother.HomeStreetName);
                    personalText = personalText.Replace("{ParentCity}", mother.HomeCity);
                    personalText = personalText.Replace("{ParentPhoneNo}", mother.HomePhoneNo1);
                }
                else
                {
                    personalText = personalText.Replace("{MotherName}", "-");
                    personalText = personalText.Replace("{MotherEducationLevel}", "-");
                    personalText = personalText.Replace("{MotherJob}", "-");
                    personalText = personalText.Replace("{ParentAddress}", "-");
                    personalText = personalText.Replace("{ParentCity}", "-");
                    personalText = personalText.Replace("{ParentPhoneNo}", "-");
                    tdFooterStudentParent1.InnerHtml = tdFooterStudentParent.InnerHtml = ".................................";
                }

                vSite site = BusinessLayer.GetvSiteList(String.Format("SiteID = '{0}'", AppSession.UserLogin.SiteID))[0];
                personalText = personalText.Replace("{FooterDate.Now}", String.Format("{0}, {1}", site.State, DateTime.Now.ToString(Constant.FormatString.DATE_REPORT_FORMAT)));
                tdFooterDateNow1.InnerHtml = tdFooterDateNow.InnerHtml = String.Format("{0}, {1}", site.State, DateTime.Now.ToString(Constant.FormatString.DATE_REPORT_FORMAT));
                personalText = personalText.Replace("{Headmaster}", HeadMaster);

                divPersonal.InnerHtml = personalText;
                #endregion

                vClassStudent student = lstClassStudent.FirstOrDefault(x => x.StudentID == StudentID && x.GCClassStudyType == Constant.ClassStudyType.REGULAR);
                tdStudentName1.InnerHtml = tdStudentName.InnerHtml = student.StudentName;
                tdNIS1.InnerHtml = tdNIS.InnerHtml = student.StudentCode;
                PeriodSection ps = BusinessLayer.GetPeriodSection(PeriodSectionID);
                tdClass1.InnerHtml = tdClass.InnerHtml = String.Format("{0}", student.SchoolClassName);
                tdPeriodSection1.InnerHtml = tdPeriodSection.InnerHtml = String.Format("{0}", ps.PeriodSectionName);
                tdSchoolPeriod1.InnerHtml = tdSchoolPeriod.InnerHtml = student.SchoolPeriodName;

                tdAchSchoolName.InnerHtml = tdSchoolName1.InnerHtml = tdSchoolName.InnerHtml = site.SiteName;
                tdSchoolAddress1.InnerHtml = tdSchoolAddress.InnerHtml = String.Format("{0}<br/>{1}", site.StreetName, site.City);

                #region School Data
                String tempSchool = divSchool.InnerHtml;
                tempSchool = tempSchool.Replace("{SchoolName}", site.SiteName);
                tempSchool = tempSchool.Replace("{SchoolAddress}", String.Format("{0}<br/>Kode Pos : {1} Telepon : {2}", site.StreetName,site.ZipCode, site.PhoneNo1));
                tempSchool = tempSchool.Replace("{SchoolKelurahan}", site.District);
                tempSchool = tempSchool.Replace("{SchoolKecamatan}", site.County);
                tempSchool = tempSchool.Replace("{SchoolCity}", site.City);
                tempSchool = tempSchool.Replace("{SchoolProvince}", site.State);
                divSchool.InnerHtml = tempSchool;
                #endregion

                if (lstClassSubjectID != "")
                {
                    lstClassSubjectTask = BusinessLayer.GetvClassSubjectTaskList(String.Format("ClassSubjectID IN ({0})", lstClassSubjectID));
                    lstNilai = BusinessLayer.GetvClassStudentSubjectMarkList(String.Format("StudentID = {0}", StudentID)).ToList();

                    CurriculumSchoolPeriodSection CSPS = BusinessLayer.GetCurriculumSchoolPeriodSectionList(String.Format("CurriculumSchoolPeriodSectionID = {0}", ps.CurriculumSchoolPeriodSectionID))[0];
                    List<CurriculumSubjectGroup> lstSubjectGroup = BusinessLayer.GetCurriculumSubjectGroupList(String.Format("CurriculumID = {0} AND IsDeleted = 0", CSPS.CurriculumID));
                    rptCurriculumSubjectGroupName.DataSource = lstSubjectGroup;
                    rptCurriculumSubjectGroupName.DataBind();

                    rptCurriculumSubjectGroupName1.DataSource = lstSubjectGroup;
                    rptCurriculumSubjectGroupName1.DataBind();

                    SubjectRowCount = lstClassSubject.Where(x => x.SubjectGCClassStudyType == Constant.ClassStudyType.REGULAR).Count() + lstSubjectGroup.Count();
                    
                    rptPersonality.DataSource = lstClassSubject.Where(x => x.SubjectGCClassStudyType == Constant.ClassStudyType.PERSONALITY);
                    rptPersonality.DataBind();
                }

                List<vClassStudent> lstTempClassStudent = lstClassStudent.Where(x => x.GCClassStudyType == Constant.ClassStudyType.EXTRACURRICULAR && x.StudentID == StudentID).ToList();

                rptEskul.DataSource = lstClassSubject.Where(x => x.SubjectGCClassStudyType == Constant.ClassStudyType.EXTRACURRICULAR && lstTempClassStudent.Select(s => s.SchoolClassID).Contains(x.SchoolClassID));
                rptEskul.DataBind();

                List<Variable> lstOrganization = new List<Variable>();
                foreach (vOrganizationDt organizationDt in lstOrganizationDt.Where(x => x.StudentCoordinatorID == StudentID))
                    lstOrganization.Add(new Variable { Code = organizationDt.OrganizationName, Value = organizationDt.Position });
                foreach (vOrganizationDtStudent organizationDt in lstOrganizationDtStudent.Where(x => x.StudentID == StudentID))
                    lstOrganization.Add(new Variable { Code = organizationDt.OrganizationName, Value = organizationDt.Position });
                foreach (OrganizationHd organizationHd in lstOrganizationHd)
                {
                    if (lstOrganization.Where(p => p.Code == organizationHd.OrganizationName).Count() < 1)
                    {
                        lstOrganization.Add(new Variable { Code = organizationHd.OrganizationName, Value = "Anggota" });
                    }
                }
                rptOrganization.DataSource = lstOrganization;
                rptOrganization.DataBind();

                List<ClassStudentDailyAttendance> csda = BusinessLayer.GetClassStudentDailyAttendanceList(String.Format("SchoolClassID = {0} AND PeriodSectionID = {1} AND StudentID = {2}", SchoolClassID, PeriodSectionID, StudentID));
                tdSick.InnerHtml = String.Format("{0} hari", csda.Where(x => x.GCAttendanceStatus == Constant.AttendanceStatus.SAKIT).Count());
                tdPermit.InnerHtml = String.Format("{0} hari", csda.Where(x => x.GCAttendanceStatus == Constant.AttendanceStatus.IZIN).Count());
                tdAlpha.InnerHtml = String.Format("{0} hari", csda.Where(x => x.GCAttendanceStatus == Constant.AttendanceStatus.ALPA).Count());

                List<StudentAchievement> achievement = BusinessLayer.GetStudentAchievementList(String.Format("IsDeleted = 0 AND StudentID = {0}", StudentID));
                rptAchievement.DataSource = achievement;
                rptAchievement.DataBind();

                //String text = divPageFooter.InnerHtml;
                //text = text.Replace("{Date.Now}", DateTime.Now.ToString(Constant.FormatString.DATE_REPORT_FORMAT));
                //text = text.Replace("{City}", site.City);
                vSchoolClass sc = BusinessLayer.GetvSchoolClassList(String.Format("SchoolClassID = {0}", SchoolClassID))[0];
                tdFooterWali1.InnerHtml = tdFooterWali.InnerHtml = sc.TeacherName;
                //text = text.Replace("{Headmaster}", HeadMaster);
                //divPageFooter.InnerHtml = text;
            }
        }

        protected void rptCurriculumSubjectGroupName_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                CurriculumSubjectGroup entity = e.Item.DataItem as CurriculumSubjectGroup;
                Repeater rptSubject = e.Item.FindControl("rptSubject") as Repeater;

                rptSubject.DataSource = lstClassSubject.Where(x => x.SubjectGCClassStudyType == Constant.ClassStudyType.REGULAR && x.CurriculumSubjectGroupID == entity.CurriculumSubjectGroupID);
                rptSubject.DataBind();
            }
        }
        string remarks;
        protected void rptCurriculumSubjectGroupName1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                CurriculumSubjectGroup entity = e.Item.DataItem as CurriculumSubjectGroup;
                Repeater rptSubjectKompetensi = e.Item.FindControl("rptSubjectKompetensi") as Repeater;

                List<vClassSubject> lstTemp = lstClassSubject.Where(x => x.SubjectGCClassStudyType == Constant.ClassStudyType.PERSONALITY).ToList();
                List<vClassStudentSubjectMark> lstTempNilai = lstNilai.Where(x => lstTemp.Select(s => s.ClassSubjectID).Contains(x.ClassSubjectID)).ToList();
                remarks = String.Format(",", lstTempNilai.Select(x => x.DescriptionMark));
                rptSubjectKompetensi.DataSource = lstClassSubject.Where(x => x.SubjectGCClassStudyType == Constant.ClassStudyType.REGULAR && x.CurriculumSubjectGroupID == entity.CurriculumSubjectGroupID);
                rptSubjectKompetensi.DataBind();
            }
        }

        protected void rptEskul_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                vClassSubject entity = e.Item.DataItem as vClassSubject;
                HtmlTableCell tdEskul = e.Item.FindControl("tdEskul") as HtmlTableCell;
                vClassStudentSubjectMark cs = lstNilai.FirstOrDefault(x => x.ClassSubjectID == entity.ClassSubjectID);
                if (cs != null) tdEskul.InnerHtml = cs.CompetencyDescription;
                else tdEskul.InnerHtml = "-";
            }
        }

        protected void rptSubjectKompetensi_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                vClassSubject entity = e.Item.DataItem as vClassSubject;
                HtmlTableCell tdTeoriKompetensi = e.Item.FindControl("tdTeoriKompetensi") as HtmlTableCell;
                HtmlTableCell tdPraktikKompetensi = e.Item.FindControl("tdPraktikKompetensi") as HtmlTableCell;
                HtmlTableCell tdSikapKompetensi = e.Item.FindControl("tdSikapKompetensi") as HtmlTableCell;

                vClassStudentSubjectMark cs = lstNilai.FirstOrDefault(x => x.ClassSubjectID == entity.ClassSubjectID && x.GCStudentMarkGroup == Constant.StudentMarkGroup.THEORY);
                if (cs != null) tdTeoriKompetensi.InnerHtml = cs.CompetencyDescription;
                else tdTeoriKompetensi.InnerHtml = "-";

                cs = lstNilai.FirstOrDefault(x => x.ClassSubjectID == entity.ClassSubjectID && x.GCStudentMarkGroup == Constant.StudentMarkGroup.PRACTICE);
                if (cs != null) tdPraktikKompetensi.InnerHtml = cs.CompetencyDescription;
                else tdPraktikKompetensi.InnerHtml = "-";

                cs = lstNilai.FirstOrDefault(x => x.ClassSubjectID == entity.ClassSubjectID && x.GCStudentMarkGroup == Constant.StudentMarkGroup.AFFECTIVE);
                if (cs != null) tdSikapKompetensi.InnerHtml = cs.CompetencyDescription;
                else tdSikapKompetensi.InnerHtml = "-";
            }
        }

        protected void rptSubject_ItemDataBound(object sender, RepeaterItemEventArgs e) 
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                vClassSubject entity = e.Item.DataItem as vClassSubject;
                
                List<vClassStudentSubjectMark> lstMark = lstNilai.Where(x => x.ClassSubjectID == entity.ClassSubjectID).ToList();
                HtmlTableCell tdTxtSubjectName = e.Item.FindControl("tdTxtSubjectName") as HtmlTableCell;
                HtmlTableCell tdTheory = e.Item.FindControl("tdTheory") as HtmlTableCell;
                HtmlTableCell tdTxtTheory = e.Item.FindControl("tdTxtTheory") as HtmlTableCell;
                HtmlTableCell tdPractice = e.Item.FindControl("tdPractice") as HtmlTableCell;
                HtmlTableCell tdTxtPractice = e.Item.FindControl("tdTxtPractice") as HtmlTableCell;
                HtmlTableCell tdFinalScore = e.Item.FindControl("tdFinalScore") as HtmlTableCell;
                HtmlTableCell tdAffective = e.Item.FindControl("tdAffective") as HtmlTableCell;
                HtmlTableCell tdAttitude = e.Item.FindControl("tdAttitude") as HtmlTableCell;

                if (e.Item.ItemIndex == 0)
                    tdAttitude.RowSpan = SubjectRowCount;
                else
                    tdAttitude.Style.Add("Display", "none");

                tdTxtSubjectName.InnerHtml = String.Format("{0}<br/>Nama Guru : {1}",entity.SubjectName, entity.TeacherName);

                if (tdAttitude.InnerHtml == "")
                    tdAttitude.InnerHtml = remarks;

                vClassStudentSubjectMark theoryMark = lstMark.FirstOrDefault(p => p.GCStudentMarkGroup == Constant.StudentMarkGroup.THEORY);
                if (theoryMark != null)
                {
                    tdTheory.InnerHtml = theoryMark.MarkTypeDtName;
                    tdTxtTheory.InnerHtml = theoryMark.PredicateMarkTypeDtName;
                }
                else
                {
                    tdTheory.InnerHtml = "-";
                    tdTxtTheory.InnerHtml = "-";
                }
                vClassStudentSubjectMark practiceMark = lstMark.FirstOrDefault(p => p.GCStudentMarkGroup == Constant.StudentMarkGroup.PRACTICE);
                if (practiceMark != null)
                {
                    tdPractice.InnerHtml = practiceMark.MarkTypeDtName;
                    tdTxtPractice.InnerHtml = practiceMark.PredicateMarkTypeDtName;
                }
                else
                {
                    tdPractice.InnerHtml = "-";
                    tdTxtPractice.InnerHtml = "-";
                }

                vClassStudentSubjectMark affectiveMark = lstMark.FirstOrDefault(p => p.GCStudentMarkGroup == Constant.StudentMarkGroup.AFFECTIVE);
                if (affectiveMark != null)
                    tdAffective.InnerHtml = affectiveMark.PredicateMarkTypeDtName;
                else
                    tdAffective.InnerHtml = "-";
            }
        }
    }
}