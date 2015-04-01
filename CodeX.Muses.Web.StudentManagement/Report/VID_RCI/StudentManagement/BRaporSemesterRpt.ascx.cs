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
    public partial class BRaporSemesterRpt : BaseCustomReportCtl
    {
        
        private Int32 SchoolPeriodID = 0;
        private Int32 PeriodSectionID = 0;
        private Int32 SchoolClassID = 0;
        private Int32 StudentID = 0;
        
        List<vClassSubjectTask> lstClassSubjectTask = null;
        List<ClassStudentSubjectMark> lstNilai = null;

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

            if (param.Count() > 3) lstStudentID.Add(Convert.ToInt32(param[3]));
            else 
            {
                List<vClassStudent> lstSchoolClass = BusinessLayer.GetvClassStudentList(String.Format("SchoolClassID = {0} AND GCClassStudyType = '{1}'", SchoolClassID, Constant.ClassStudyType.REGULAR));
                lstStudentID.AddRange(lstSchoolClass.Select(x => x.StudentID));
            } 

            rptStudent.DataSource = lstStudentID;
            rptStudent.DataBind();
            #endregion
        }

        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                StudentID = (Int32)e.Item.DataItem;

                HtmlTableCell tdStudentName = e.Item.FindControl("tdStudentName") as HtmlTableCell;
                HtmlTableCell tdNIS = e.Item.FindControl("tdNIS") as HtmlTableCell;
                HtmlTableCell tdClass = e.Item.FindControl("tdClass") as HtmlTableCell;
                HtmlTableCell tdSchoolPeriod = e.Item.FindControl("tdSchoolPeriod") as HtmlTableCell;
                HtmlTableCell tdSchoolName = e.Item.FindControl("tdSchoolName") as HtmlTableCell;
                HtmlTableCell tdUlangan = e.Item.FindControl("tdUlangan") as HtmlTableCell;
                HtmlTableCell tdTugas = e.Item.FindControl("tdTugas") as HtmlTableCell;
                HtmlTableCell tdPsikomotorik = e.Item.FindControl("tdPsikomotorik") as HtmlTableCell;
                HtmlTableCell tdHeaderNilai = e.Item.FindControl("tdHeaderNilai") as HtmlTableCell;
                HtmlTableCell tdSick = e.Item.FindControl("tdSick") as HtmlTableCell;
                HtmlTableCell tdPermit = e.Item.FindControl("tdPermit") as HtmlTableCell;
                HtmlTableCell tdAlpha = e.Item.FindControl("tdAlpha") as HtmlTableCell;
                HtmlTableCell tdHeaderHasil = e.Item.FindControl("tdHeaderHasil") as HtmlTableCell;
                Repeater rptSubject = e.Item.FindControl("rptSubject") as Repeater;

                vClassStudent student = BusinessLayer.GetvClassStudentList(String.Format("StudentID = {0} AND GCClassStudyType = '{1}'", StudentID, Constant.ClassStudyType.REGULAR))[0];
                tdStudentName.InnerHtml = student.StudentName;
                tdNIS.InnerHtml = student.StudentCode;
                PeriodSection ps = BusinessLayer.GetPeriodSection(PeriodSectionID);
                tdClass.InnerHtml = String.Format("{0} / {1}", student.SchoolClassName, ps.PeriodSectionName);
                tdSchoolPeriod.InnerHtml = student.SchoolPeriodName;

                vSite site = BusinessLayer.GetvSiteList(String.Format("SiteID = '{0}'", AppSession.UserLogin.SiteID))[0];
                tdSchoolName.InnerHtml = site.SiteName;

                List<vClassSubject> lstClassSubject = BusinessLayer.GetvClassSubjectList(String.Format("SchoolPeriodID = {0} AND PeriodSectionID = {1} AND SchoolClassID = {2} AND SubjectGCClassStudyType = '{3}' AND IsDeleted = 0", SchoolPeriodID, PeriodSectionID, SchoolClassID, Constant.ClassStudyType.REGULAR));
                String lstClassSubjectID = String.Join(",", lstClassSubject.Select(x => x.ClassSubjectID));
                if (lstClassSubjectID != "")
                {
                    lstClassSubjectTask = BusinessLayer.GetvClassSubjectTaskList(String.Format("ClassSubjectID IN ({0})", lstClassSubjectID));
                    lstNilai = BusinessLayer.GetClassStudentSubjectMarkList(String.Format("StudentID = {0}", StudentID)).ToList();
                    
                    rptSubject.DataSource = lstClassSubject;
                    rptSubject.DataBind();
                }

                List<ClassStudentDailyAttendance> csda = BusinessLayer.GetClassStudentDailyAttendanceList(String.Format("SchoolClassID = {0} AND PeriodSectionID = {1} AND StudentID = {2}", SchoolClassID, PeriodSectionID, StudentID));
                tdSick.InnerHtml = String.Format("{0} hari", csda.Where(x => x.GCAttendanceStatus == Constant.AttendanceStatus.SAKIT).Count());
                tdPermit.InnerHtml = String.Format("{0} hari", csda.Where(x => x.GCAttendanceStatus == Constant.AttendanceStatus.IZIN).Count());
                tdAlpha.InnerHtml = String.Format("{0} hari", csda.Where(x => x.GCAttendanceStatus == Constant.AttendanceStatus.ALPA).Count());
                
                String text = divPageFooter.InnerHtml;
                text = text.Replace("{Date.Now}", DateTime.Now.ToString(Constant.FormatString.DATE_REPORT_FORMAT));
                text = text.Replace("{City}", site.City);
                vSchoolClass sc = BusinessLayer.GetvSchoolClassList(String.Format("SchoolClassID = {0}", SchoolClassID))[0];
                text = text.Replace("{WaliKelas}", sc.TeacherName);
                divPageFooter.InnerHtml = text;
            }
        }

        protected void rptSubject_ItemDataBound(object sender, RepeaterItemEventArgs e) 
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                vClassSubject entity = e.Item.DataItem as vClassSubject;

                ClassStudentSubjectMark cs = lstNilai.FirstOrDefault(x => x.ClassSubjectID == entity.ClassSubjectID);
                HtmlTableCell tdTheory = e.Item.FindControl("tdTheory") as HtmlTableCell;
                HtmlTableCell tdPractice = e.Item.FindControl("tdPractice") as HtmlTableCell;
                HtmlTableCell tdFinalScore = e.Item.FindControl("tdFinalScore") as HtmlTableCell;
                HtmlTableCell tdAffective = e.Item.FindControl("tdAffective") as HtmlTableCell;

                if (cs != null)
                {
                    tdTheory.InnerHtml = cs.TheoryMark > 0 ? cs.TheoryMark.ToString("N") : "-";
                    tdPractice.InnerHtml = cs.PracticeMark > 0 ? cs.PracticeMark.ToString("N") : "-";
                    tdFinalScore.InnerHtml = "-";//cs.TheoryMark > 0 ? cs.TheoryMark.ToString("N") : "-"; ;
                    tdAffective.InnerHtml = cs.AffectiveMark != null ? cs.AffectiveMark : "-";
                }
                else 
                {
                    tdTheory.InnerHtml = "-";
                    tdPractice.InnerHtml = "-";
                    tdFinalScore.InnerHtml = "-";
                    tdAffective.InnerHtml = "-";
                }
            }
        }
    }
}