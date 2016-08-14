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
    public partial class BRaporSemesterGanjilSMP2006Rpt : BaseCustomReportCtl
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
        vSchoolClass sc = null;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        vSite site = null;
        public override void Bind(string filterExpression, string[] param)
        {
            site = BusinessLayer.GetvSiteList(String.Format("SiteID = '{0}'", AppSession.UserLogin.SiteID))[0];

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
            lstClassSubject = BusinessLayer.GetvClassSubjectList(String.Format("SchoolPeriodID = {0} AND SchoolClassID IN ({1}) AND IsDeleted = 0", SchoolPeriodID, lstSchoolClassID));
            lstClassSubjectID = String.Join(",", lstClassSubject.Select(x => x.ClassSubjectID));
            lstOrganizationHd = BusinessLayer.GetOrganizationHdList(string.Format("SchoolPeriodID = {0} AND IsAllStudentAsMember = 1 AND IsDeleted = 0", SchoolPeriodID));

            lstClassSubjectTask = BusinessLayer.GetvClassSubjectTaskList(String.Format("ClassSubjectID IN ({0})", lstClassSubjectID));
            HeadMaster = BusinessLayer.GetSiteParameter(AppSession.UserLogin.SiteID, Constant.SiteParameter.HEADMASTER).ParameterValue;

            sc = BusinessLayer.GetvSchoolClassList(String.Format("SchoolClassID = {0}", SchoolClassID))[0];
            rptStudent.DataSource = lstStudentID;
            rptStudent.DataBind();
            #endregion
        }

        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                StudentID = (Int32)e.Item.DataItem;
                
                #region initialization
                HtmlTableCell tdStudentName = e.Item.FindControl("tdStudentName") as HtmlTableCell;
                HtmlTableCell tdNIS = e.Item.FindControl("tdNIS") as HtmlTableCell;
                HtmlTableCell tdClass = e.Item.FindControl("tdClass") as HtmlTableCell;
                HtmlTableCell tdSchoolPeriod = e.Item.FindControl("tdSchoolPeriod") as HtmlTableCell;
                HtmlTableCell tdSemester = e.Item.FindControl("tdSemester") as HtmlTableCell;
                HtmlTableCell tdSchoolName = e.Item.FindControl("tdSchoolName") as HtmlTableCell;
                HtmlTableCell tdSchoolAddress = e.Item.FindControl("tdSchoolAddress") as HtmlTableCell;

                HtmlTableCell tdSick = e.Item.FindControl("tdSick") as HtmlTableCell;
                HtmlTableCell tdPermit = e.Item.FindControl("tdPermit") as HtmlTableCell;
                HtmlTableCell tdAlpha = e.Item.FindControl("tdAlpha") as HtmlTableCell;
                HtmlTableCell tdHeaderHasil = e.Item.FindControl("tdHeaderHasil") as HtmlTableCell;
                
                Repeater rptSubject = e.Item.FindControl("rptSubject") as Repeater;
                Repeater rptSubjectKompetnsi = e.Item.FindControl("rptSubjectKompetnsi") as Repeater;
                Repeater rptPersonality = e.Item.FindControl("rptPersonality") as Repeater;
                Repeater rptOrganization = e.Item.FindControl("rptOrganization") as Repeater;
                Repeater rptEskul = e.Item.FindControl("rptEskul") as Repeater;
                
                HtmlTableCell tdStudentRemarks = e.Item.FindControl("tdStudentRemarks") as HtmlTableCell;
                #endregion

                ClassStudentMark entityMark = lstClassStudentMark.FirstOrDefault(p => p.StudentID == StudentID);
                if (entityMark != null)
                    tdStudentRemarks.InnerHtml = entityMark.Remarks;


                vClassStudent student = lstClassStudent.FirstOrDefault(x => x.StudentID == StudentID && x.GCClassStudyType == Constant.ClassStudyType.REGULAR);
                tdStudentName.InnerHtml = student.StudentName;
                tdNIS.InnerHtml = string.Format("{0} / {1}", student.StudentCode, student.NationalStudentNo);
                PeriodSection ps = BusinessLayer.GetPeriodSection(PeriodSectionID);
                tdClass.InnerHtml = String.Format("{0}", student.SchoolClassName);
                tdSemester.InnerHtml = ps.PeriodSectionName;
                tdSchoolPeriod.InnerHtml = student.SchoolPeriodName;
                                
                tdSchoolName.InnerHtml = site.SiteName;
                tdSchoolAddress.InnerHtml = site.StreetName.Split(',')[0];

                if (lstClassSubjectID != "")
                {
                    lstNilai = BusinessLayer.GetvClassStudentSubjectMarkList(String.Format("StudentID = {0}", StudentID)).ToList();

                    rptSubject.DataSource = lstClassSubject.Where(x => x.SubjectGCClassStudyType == Constant.ClassStudyType.REGULAR);
                    rptSubject.DataBind();

                    rptSubjectKompetnsi.DataSource = lstClassSubject.Where(x => x.SubjectGCClassStudyType == Constant.ClassStudyType.REGULAR);
                    rptSubjectKompetnsi.DataBind();

                    rptPersonality.DataSource = lstClassSubject.Where(x => x.SubjectGCClassStudyType == Constant.ClassStudyType.PERSONALITY);
                    rptPersonality.DataBind();
                }

                rptEskul.DataSource = lstClassSubject.Where(x => x.SubjectGCClassStudyType == Constant.ClassStudyType.EXTRACURRICULAR);
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

                List<ClassStudentAttendance> csa = BusinessLayer.GetClassStudentAttendanceList(String.Format("SchoolClassID = {0} AND PeriodSectionID = {1} AND StudentID = {2}", SchoolClassID, PeriodSectionID, StudentID));
                if (csa.Count > 0)
                {
                    tdSick.InnerHtml = String.Format("{0}", csa.Where(x => x.GCAttendanceStatus == Constant.AttendanceStatus.SAKIT).Sum(p => p.TotalAttendanceStatus));
                    tdPermit.InnerHtml = String.Format("{0}", csa.Where(x => x.GCAttendanceStatus == Constant.AttendanceStatus.IZIN).Sum(p => p.TotalAttendanceStatus));
                    tdAlpha.InnerHtml = String.Format("{0}", csa.Where(x => x.GCAttendanceStatus == Constant.AttendanceStatus.ALPA).Sum(p => p.TotalAttendanceStatus));
                }
                else
                {
                    List<ClassStudentDailyAttendance> csda = BusinessLayer.GetClassStudentDailyAttendanceList(String.Format("SchoolClassID = {0} AND PeriodSectionID = {1} AND StudentID = {2}", SchoolClassID, PeriodSectionID, StudentID));
                    tdSick.InnerHtml = String.Format("{0}", csda.Where(x => x.GCAttendanceStatus == Constant.AttendanceStatus.SAKIT).Count());
                    tdPermit.InnerHtml = String.Format("{0}", csda.Where(x => x.GCAttendanceStatus == Constant.AttendanceStatus.IZIN).Count());
                    tdAlpha.InnerHtml = String.Format("{0}", csda.Where(x => x.GCAttendanceStatus == Constant.AttendanceStatus.ALPA).Count());
                }
                
                String text = divPageFooter.InnerHtml;
                text = text.Replace("{Date.Now}", DateTime.Now.ToString(Constant.FormatString.DATE_REPORT_FORMAT));
                text = text.Replace("{City}", site.City);
                text = text.Replace("{WaliKelas}", sc.TeacherName);
                text = text.Replace("{Headmaster}", HeadMaster);
                divPageFooter.InnerHtml = text;
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

        protected void rptSubjectKompetnsi_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                vClassSubject entity = e.Item.DataItem as vClassSubject;
                HtmlTableCell tdKompetensi = e.Item.FindControl("tdKompetensi") as HtmlTableCell;
                vClassStudentSubjectMark cs = lstNilai.FirstOrDefault(x => x.ClassSubjectID == entity.ClassSubjectID && x.GCStudentMarkGroup == Constant.StudentMarkGroup.THEORY);
                if (cs != null) tdKompetensi.InnerHtml = cs.CompetencyDescription;
                else tdKompetensi.InnerHtml = "-";
            }
        }

        protected void rptPersonality_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                vClassSubject entity = e.Item.DataItem as vClassSubject;

                List<vClassStudentSubjectMark> lstMark = lstNilai.Where(x => x.ClassSubjectID == entity.ClassSubjectID).ToList();
                vClassStudentSubjectMark mark = lstMark.FirstOrDefault(p => p.GCStudentMarkGroup == Constant.StudentMarkGroup.AFFECTIVE);
                HtmlTableCell tdPersonalityScore = e.Item.FindControl("tdPersonalityScore") as HtmlTableCell;
                if (mark != null)
                    tdPersonalityScore.InnerHtml = mark.PredicateMarkTypeDtName;
                else
                    tdPersonalityScore.InnerHtml = "";
            }
        }

        protected void rptSubject_ItemDataBound(object sender, RepeaterItemEventArgs e) 
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                vClassSubject entity = e.Item.DataItem as vClassSubject;

                List<vClassStudentSubjectMark> lstMark = lstNilai.Where(x => x.ClassSubjectID == entity.ClassSubjectID).ToList();
                HtmlTableCell tdTheory = e.Item.FindControl("tdTheory") as HtmlTableCell;
                HtmlTableCell tdTxtTheory = e.Item.FindControl("tdTxtTheory") as HtmlTableCell;
                HtmlTableCell tdFinalScore = e.Item.FindControl("tdFinalScore") as HtmlTableCell;
                HtmlTableCell tdTxtDescription = e.Item.FindControl("tdTxtDescription") as HtmlTableCell;

                vClassStudentSubjectMark theoryMark = lstMark.FirstOrDefault(p => p.GCStudentMarkGroup == Constant.StudentMarkGroup.THEORY);
                if (theoryMark != null && theoryMark.Mark > 0)
                {
                    tdTheory.InnerHtml = theoryMark.Mark.ToString("N");
                    tdTxtTheory.InnerHtml = Function.NumberInWordsForScore(theoryMark.Mark);
                    if (theoryMark.Mark > entity.PassingGrade)
                        tdTxtDescription.InnerHtml = "Terlampaui";
                    else if (theoryMark.Mark == entity.PassingGrade)
                        tdTxtDescription.InnerHtml = "Tercapai";
                    else
                        tdTxtDescription.InnerHtml = "Tidak Tercapai";
                }
                else
                {
                    tdTheory.InnerHtml = "-";
                    tdTxtTheory.InnerHtml = "-";
                    tdTxtDescription.InnerHtml = "-";
                }
            }
        }
    }
}