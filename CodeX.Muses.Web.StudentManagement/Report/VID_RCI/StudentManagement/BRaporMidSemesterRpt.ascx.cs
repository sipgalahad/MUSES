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
    public partial class BRaporMidSemesterRpt : BaseCustomReportCtl
    {
        
        private Int32 SchoolPeriodID = 0;
        private Int32 PeriodSectionID = 0;
        private Int32 SchoolClassID = 0;
        private Int32 StudentID = 0;
        
        List<vClassSubjectTask> lstClassSubjectTask = null;
        List<ClassStudentSubjectTaskMark> lstNilai = null;
        List<ClassStudentSubjectMark> lstStudentSubjectMark = null;
        List<vClassSubject> lstClassSubject = null;
        String lstClassSubjectID = "";

        int MaxUlangan = 0;
        int MaxTugas = 0;
        int MaxPsikomotorik = 0;
        
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
            lstClassSubject = BusinessLayer.GetvClassSubjectList(String.Format("SchoolPeriodID = {0} AND SchoolClassID = {1} AND SubjectGCClassStudyType = '{2}' AND IsDeleted = 0", SchoolPeriodID, SchoolClassID, Constant.ClassStudyType.REGULAR));
            lstClassSubjectID = String.Join(",", lstClassSubject.Select(x => x.ClassSubjectID));
            lstClassSubjectTask = BusinessLayer.GetvClassSubjectTaskList(String.Format("ClassSubjectID IN ({0})", lstClassSubjectID));

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
                Repeater rptUlanganHeader = e.Item.FindControl("rptUlanganHeader") as Repeater;
                Repeater rptTugasHeader = e.Item.FindControl("rptTugasHeader") as Repeater;
                Repeater rptPsikomotorikHeader = e.Item.FindControl("rptPsikomotorikHeader") as Repeater;
                Repeater rptSubject = e.Item.FindControl("rptSubject") as Repeater;

                vClassStudent student = BusinessLayer.GetvClassStudentList(String.Format("StudentID = {0} AND GCClassStudyType = '{1}'", StudentID, Constant.ClassStudyType.REGULAR))[0];
                tdStudentName.InnerHtml = student.StudentName;
                tdNIS.InnerHtml = student.StudentCode;
                PeriodSection ps = BusinessLayer.GetPeriodSection(PeriodSectionID);
                tdClass.InnerHtml = String.Format("{0} / {1}", student.SchoolClassName, ps.PeriodSectionName);
                tdSchoolPeriod.InnerHtml = student.SchoolPeriodName;

                vSite site = BusinessLayer.GetvSiteList(String.Format("SiteID = '{0}'", AppSession.UserLogin.SiteID))[0];
                tdSchoolName.InnerHtml = site.SiteName;

                if (lstClassSubjectID != "")
                {
                    lstNilai = BusinessLayer.GetClassStudentSubjectTaskMarkList(String.Format("StudentID = {0}", StudentID)).OrderBy(x => x.ClassSubjectTaskID).ToList();
                    lstStudentSubjectMark = BusinessLayer.GetClassStudentSubjectMarkList(String.Format("StudentID = {0}", StudentID)).ToList();
                    #region header ulangan
                    var temp = lstClassSubjectTask.Where(m => m.CurriculumMarkTypeDtID == 3 && m.CurriculumMarkTypeCode == "SMA_2006_TEORI").GroupBy(x => x.ClassSubjectID).Select(s => new { ClassSubjectID = s.Key, Count = s.Count() });

                    List<String> lstDataHeader = new List<String>();
                    if (temp.Count() > 0)
                        MaxUlangan = temp.Max(x => x.Count);
                    for (int i = 0; i < MaxUlangan; i++)
                    {
                        lstDataHeader.Add(String.Format("{0}", i + 1));
                    }
                    //lstDataHeader.Add("Rata-Rata");
                    //tdUlangan.ColSpan = MaxUlangan + 1;
                    tdUlangan.ColSpan = MaxUlangan;

                    rptUlanganHeader.DataSource = lstDataHeader;
                    rptUlanganHeader.DataBind();
                    #endregion

                    #region header Tugas
                    temp = lstClassSubjectTask.Where(m => m.IsExam == false && m.CurriculumMarkTypeDtID != 3 && m.CurriculumMarkTypeCode == "SMA_2006_TEORI").GroupBy(x => x.ClassSubjectID).Select(s => new { ClassSubjectID = s.Key, Count = s.Count() });

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
                    tdTugas.ColSpan = MaxTugas;

                    rptTugasHeader.DataSource = lstDataHeader;
                    rptTugasHeader.DataBind();
                    #endregion

                    #region header Psikomotorik
                    temp = lstClassSubjectTask.Where(m => m.CurriculumMarkTypeCode == "SMA_2006_PRAKTIK").GroupBy(x => x.ClassSubjectID).Select(s => new { ClassSubjectID = s.Key, Count = s.Count() });
                    if (temp.Count() > 0) MaxPsikomotorik = temp.Max(x => x.Count);
                    lstDataHeader.Clear();
                    for (int i = 0; i < MaxPsikomotorik; i++)
                    {
                        lstDataHeader.Add(String.Format("{0}", i + 1));
                    }
                    if (MaxPsikomotorik != 0)
                        tdPsikomotorik.ColSpan = MaxPsikomotorik;
                    else tdPsikomotorik.Style.Add("display", "none");

                    rptPsikomotorikHeader.DataSource = lstDataHeader;
                    rptPsikomotorikHeader.DataBind();
                    #endregion

                    tdHeaderNilai.ColSpan = MaxTugas + MaxUlangan + MaxPsikomotorik + 1;
                    tdHeaderHasil.ColSpan = tdHeaderNilai.ColSpan + 1;
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

                #region Detail Ulangan
                List<Int32> lstCS = lstClassSubjectTask.Where(m => m.ClassSubjectID == entity.ClassSubjectID && m.CurriculumMarkTypeDtID == 3).Select(x => x.ClassSubjectTaskID).ToList();
                List<String> lstDetailUlangan = new List<String>();
                if (lstCS.Count() > 0)
                {
                    foreach (Int32 obj in lstCS)
                    {
                        ClassStudentSubjectTaskMark cssEntity = lstNilai.FirstOrDefault(x => x.ClassSubjectTaskID == obj);
                        if (cssEntity != null) lstDetailUlangan.Add(cssEntity.Mark.ToString("N"));
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
                lstCS = lstClassSubjectTask.Where(m => m.ClassSubjectID == entity.ClassSubjectID && (m.IsExam == false && m.CurriculumMarkTypeDtID != 3) && m.CurriculumMarkTypeCode == "SMA_2006_TEORI").Select(x => x.ClassSubjectTaskID).ToList();
                List<String> lstDetailTugas = new List<String>();

                if (lstCS.Count() > 0)
                {
                    foreach (Int32 obj in lstCS)
                    {
                        ClassStudentSubjectTaskMark cssEntity = lstNilai.FirstOrDefault(x => x.ClassSubjectTaskID == obj);
                        if (cssEntity != null) lstDetailTugas.Add(cssEntity.Mark.ToString("N"));
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

                #region Detail Psikomotorik
                lstCS.Clear();
                lstCS = lstClassSubjectTask.Where(m => m.ClassSubjectID == entity.ClassSubjectID && m.CurriculumMarkTypeCode == "SMA_2006_PRAKTIK").Select(x => x.ClassSubjectTaskID).ToList();
                List<String> lstDetailPsikomtorik = new List<String>();

                if (lstCS.Count() > 0)
                {
                    foreach (Int32 obj in lstCS)
                    {
                        ClassStudentSubjectTaskMark cssEntity = lstNilai.FirstOrDefault(x => x.ClassSubjectTaskID == obj);
                        if (cssEntity != null) lstDetailPsikomtorik.Add(cssEntity.Mark.ToString("N"));
                        else { lstDetailPsikomtorik.Add("-"); }
                    }
                }
                if (lstDetailPsikomtorik.Count < MaxPsikomotorik) for (int i = lstDetailPsikomtorik.Count; i < MaxPsikomotorik; i++) lstDetailPsikomtorik.Add("-");

                Repeater rptPsikomotorikDetail = (Repeater)e.Item.FindControl("rptPsikomotorikDetail");
                rptPsikomotorikDetail.DataSource = lstDetailPsikomtorik;
                rptPsikomotorikDetail.DataBind();
                #endregion

                #region UTS
                HtmlTableCell tdDetailUTS = e.Item.FindControl("tdDetailUTS") as HtmlTableCell;
                vClassSubjectTask entityCST = lstClassSubjectTask.FirstOrDefault(x => x.CurriculumMarkTypeDtID == 1);
                if (entityCST != null) tdDetailUTS.InnerHtml = lstNilai.FirstOrDefault(x => x.ClassSubjectTaskID == entityCST.ClassSubjectTaskID).Mark.ToString("N2");
                else tdDetailUTS.InnerHtml = "-";
                #endregion

                #region Affective
                HtmlTableCell tdDetailSikap = e.Item.FindControl("tdDetailSikap") as HtmlTableCell;
                ClassStudentSubjectMark ssm = lstStudentSubjectMark.FirstOrDefault(x => x.ClassSubjectID == entity.ClassSubjectID);
                //if (ssm != null) tdDetailSikap.InnerHtml = ssm.AffectiveMark;
                //else tdDetailSikap.InnerHtml = "-";
                #endregion
            }
        }
    }
}