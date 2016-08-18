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
    public partial class BRaporMidSemesterSMP2006Rpt : BaseCustomReportCtl
    {
        
        private Int32 SchoolPeriodID = 0;
        private Int32 PeriodSectionID = 0;
        private Int32 SchoolClassID = 0;
        private Int32 StudentID = 0;
        
        List<vClassSubjectTask> lstClassSubjectTask = null;
        List<ClassStudentSubjectTaskMark> lstNilai = null;
        List<vClassStudentSubjectMark> lstNilai2 = null;
        List<ClassStudentSubjectMark> lstStudentSubjectMark = null;
        List<vClassSubject> lstClassSubject = null;
        String lstClassSubjectID = "";

        int MaxUlangan = 0;
        int MaxTugas = 0;
        vSite site = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        public override void Bind(string filterExpression, string[] param)
        {
            site = BusinessLayer.GetvSiteList(String.Format("SiteID = '{0}'", AppSession.UserLogin.SiteID))[0];
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
            lstClassSubject = BusinessLayer.GetvClassSubjectList(String.Format("SchoolPeriodID = {0} AND SchoolClassID = {1} AND SubjectGCClassStudyType IN ('{2}','{3}') AND IsDeleted = 0", SchoolPeriodID, SchoolClassID, Constant.ClassStudyType.REGULAR, Constant.ClassStudyType.PERSONALITY));
            lstClassSubjectID = String.Join(",", lstClassSubject.Select(x => x.ClassSubjectID));
            lstClassSubjectTask = BusinessLayer.GetvClassSubjectTaskList(String.Format("ClassSubjectID IN ({0}) AND IsIncludeInMidSemesterRapor = 1", lstClassSubjectID));

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
                HtmlTableCell tdSemester = e.Item.FindControl("tdSemester") as HtmlTableCell;
                HtmlTableCell tdSchoolName = e.Item.FindControl("tdSchoolName") as HtmlTableCell;
                HtmlTableCell tdSchoolAddress = e.Item.FindControl("tdSchoolAddress") as HtmlTableCell;

                HtmlTableCell tdSick = e.Item.FindControl("tdSick") as HtmlTableCell;
                HtmlTableCell tdPermit = e.Item.FindControl("tdPermit") as HtmlTableCell;
                HtmlTableCell tdAlpha = e.Item.FindControl("tdAlpha") as HtmlTableCell;


                HtmlTableCell tdHeaderTugas = e.Item.FindControl("tdHeaderTugas") as HtmlTableCell;
                HtmlTableCell tdHeaderUlangan = e.Item.FindControl("tdHeaderUlangan") as HtmlTableCell;
                Repeater rptUlanganHeader = e.Item.FindControl("rptUlanganHeader") as Repeater;
                Repeater rptTugasHeader = e.Item.FindControl("rptTugasHeader") as Repeater;
                Repeater rptSubject = e.Item.FindControl("rptSubject") as Repeater;
                Repeater rptPersonality = e.Item.FindControl("rptPersonality") as Repeater;

                vClassStudent student = BusinessLayer.GetvClassStudentList(String.Format("StudentID = {0} AND GCClassStudyType = '{1}'", StudentID, Constant.ClassStudyType.REGULAR))[0];
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
                    lstNilai2 = BusinessLayer.GetvClassStudentSubjectMarkList(String.Format("StudentID = {0}", StudentID)).ToList();

                    rptPersonality.DataSource = lstClassSubject.Where(x => x.SubjectGCClassStudyType == Constant.ClassStudyType.PERSONALITY);
                    rptPersonality.DataBind();

                    lstNilai = BusinessLayer.GetClassStudentSubjectTaskMarkList(String.Format("StudentID = {0}", StudentID)).OrderBy(x => x.ClassSubjectTaskID).ToList();
                    lstStudentSubjectMark = BusinessLayer.GetClassStudentSubjectMarkList(String.Format("StudentID = {0}", StudentID)).ToList();
                    #region header ulangan
                    var temp = lstClassSubjectTask.Where(m => m.CurriculumMarkTypeDtID == 21).GroupBy(x => x.ClassSubjectID).Select(s => new { ClassSubjectID = s.Key, Count = s.Count() });

                    List<String> lstDataHeader = new List<String>();
                    if (temp.Count() > 0)
                        MaxUlangan = temp.Max(x => x.Count);
                    for (int i = 0; i < MaxUlangan; i++)
                    {
                        lstDataHeader.Add(String.Format("{0}", i + 1));
                    }
                    //lstDataHeader.Add("Rata-Rata");
                    //tdUlangan.ColSpan = MaxUlangan + 1;
                    tdHeaderUlangan.ColSpan = MaxUlangan;

                    rptUlanganHeader.DataSource = lstDataHeader;
                    rptUlanganHeader.DataBind();
                    #endregion

                    #region header Tugas
                    temp = lstClassSubjectTask.Where(m => m.IsExam == false && m.CurriculumMarkTypeDtID == 22).GroupBy(x => x.ClassSubjectID).Select(s => new { ClassSubjectID = s.Key, Count = s.Count() });

                    if (temp.Count() > 0)
                        MaxTugas = temp.Max(x => x.Count);
                    else
                        MaxTugas = 1;
                    lstDataHeader.Clear();
                    for (int i = 0; i < MaxTugas; i++)
                    {
                        lstDataHeader.Add(String.Format("{0}", i + 1));
                    }
                    //lstDataHeader.Add("Rata-Rata");
                    //tdTugas.ColSpan = MaxTugas + 1;
                    tdHeaderTugas.ColSpan = MaxTugas;

                    rptTugasHeader.DataSource = lstDataHeader;
                    rptTugasHeader.DataBind();
                    #endregion

                    //tdHeaderNilai.ColSpan = MaxTugas + MaxUlangan + 1;
                    //tdHeaderHasil.ColSpan = tdHeaderNilai.ColSpan + 1;
                    rptSubject.DataSource = lstClassSubject.Where(p => p.SubjectGCClassStudyType == Constant.ClassStudyType.REGULAR).ToList();
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

        protected void rptPersonality_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                vClassSubject entity = e.Item.DataItem as vClassSubject;

                List<vClassStudentSubjectMark> lstMark = lstNilai2.Where(x => x.ClassSubjectID == entity.ClassSubjectID).ToList();
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

                decimal totalNilai = 0;
                int jmlhNilai = 0;

                #region Detail Ulangan
                List<Int32> lstCS = lstClassSubjectTask.Where(m => m.ClassSubjectID == entity.ClassSubjectID && m.CurriculumMarkTypeDtID == 21).Select(x => x.ClassSubjectTaskID).ToList();
                List<String> lstDetailUlangan = new List<String>();
                if (lstCS.Count() > 0)
                {
                    foreach (Int32 obj in lstCS)
                    {
                        ClassStudentSubjectTaskMark cssEntity = lstNilai.FirstOrDefault(x => x.ClassSubjectTaskID == obj);
                        if (cssEntity != null)
                        {
                            lstDetailUlangan.Add(cssEntity.Mark.ToString("N"));
                            totalNilai += cssEntity.Mark;
                            jmlhNilai++;
                        }
                        else { lstDetailUlangan.Add("-"); }
                    }
                }
                //Decimal average = 0;
                //if(lstDetailUlangan.Count > 0) average = lstDetailUlangan.Average(x => Convert.ToDecimal(x));
                if (lstDetailUlangan.Count < MaxUlangan) for (int i = lstDetailUlangan.Count; i < MaxUlangan; i++) lstDetailUlangan.Add("-");
                //if (average != 0) lstDetailUlangan.Add(average.ToString("N"));
                //else lstDetailUlangan.Add("-");

                Repeater rptUlanganDetail = (Repeater)e.Item.FindControl("rptUlanganDetail");
                rptUlanganDetail.DataSource = lstDetailUlangan;
                rptUlanganDetail.DataBind();
                #endregion

                #region Detail Tugas
                //lstClassSubjectTugasID = String.Join(",", lstClassSubjectTask.Where(m => m.ClassSubjectID == entity.ClassSubjectID && (m.GCTaskType == Constant.TaskType.TUGAS_KELAS || m.GCTaskType == Constant.TaskType.TUGAS_KELOMPOK || m.GCTaskType == Constant.TaskType.PEKERJAAN_RUMAH)).Select(x => x.ClassSubjectTaskID));
                lstCS.Clear();
                lstCS = lstClassSubjectTask.Where(m => m.ClassSubjectID == entity.ClassSubjectID && (m.IsExam == false && m.CurriculumMarkTypeDtID == 22)).Select(x => x.ClassSubjectTaskID).ToList();
                List<String> lstDetailTugas = new List<String>();

                if (lstCS.Count() > 0)
                {
                    foreach (Int32 obj in lstCS)
                    {
                        ClassStudentSubjectTaskMark cssEntity = lstNilai.FirstOrDefault(x => x.ClassSubjectTaskID == obj);
                        if (cssEntity != null)
                        {
                            lstDetailTugas.Add(cssEntity.Mark.ToString("N"));
                            totalNilai += cssEntity.Mark;
                            jmlhNilai++;
                        }
                        else { lstDetailTugas.Add("-"); }
                    }
                }
                //average = 0;
                //if (lstDetailTugas.Count > 0) average = lstDetailTugas.Average(x => Convert.ToDecimal(x));
                if (lstDetailTugas.Count < MaxTugas) for (int i = lstDetailTugas.Count; i < MaxTugas; i++) lstDetailTugas.Add("-");
                //if (average != 0) lstDetailTugas.Add(average.ToString("N"));
                //else lstDetailTugas.Add("-");

                Repeater rptTugasDetail = (Repeater)e.Item.FindControl("rptTugasDetail");
                rptTugasDetail.DataSource = lstDetailTugas;
                rptTugasDetail.DataBind();
                #endregion

                #region UTS
                HtmlTableCell tdDetailUTS = e.Item.FindControl("tdDetailUTS") as HtmlTableCell;
                vClassSubjectTask entityCST = lstClassSubjectTask.FirstOrDefault(x => x.CurriculumMarkTypeDtID == 23);
                if (entityCST != null)
                {
                    decimal mark = lstNilai.FirstOrDefault(x => x.ClassSubjectTaskID == entityCST.ClassSubjectTaskID).Mark;
                    tdDetailUTS.InnerHtml = mark.ToString("N2");
                    totalNilai += mark;
                    jmlhNilai++;
                }
                else tdDetailUTS.InnerHtml = "-";
                #endregion

                HtmlTableCell tdDetailAverage = e.Item.FindControl("tdDetailAverage") as HtmlTableCell;
                if (jmlhNilai > 0)
                    tdDetailAverage.InnerHtml = (totalNilai / jmlhNilai).ToString("N0");
                else
                    tdDetailAverage.InnerHtml = "-";
            }
        }
    }
}