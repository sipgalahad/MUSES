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
        private Int32 StudentID = 0;
        //private String lstClassSubjectUlanganID = "";
        //private String lstClassSubjectTugasID = "";
        List<vClassSubjectTask> lstClassSubjectTask = null;
        List<ClassStudentSubjectTaskMark> lstNilai = null;
        //List<ClassStudentSubjectTaskMark> lstNilaiTugas = null;

        int MaxUlangan = 0;
        int MaxTugas = 0;
        
        public class TempClass 
        {
            Int32 _ClassSubjectID;
            Int32 _ClassSubjectTaskID;
            String _GCTaskType;
            Int32 _DisplayOrder;

            public Int32 ClassSubjectID
            {
                get { return _ClassSubjectID; }
                set { _ClassSubjectID = value; }
            }
            
            public Int32 ClassSubjectTaskID
            {
                get { return _ClassSubjectTaskID; }
                set { _ClassSubjectTaskID = value; }
            }
            
            public String GCTaskType
            {
                get { return _GCTaskType; }
                set { _GCTaskType = value; }
            }
            

            public Int32 DisplayOrder
            {
                get { return _DisplayOrder; }
                set { _DisplayOrder = value; }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        public override void Bind(string filterExpression, string[] param)
        {
            #region Initialization
            StudentID = 1;

            vClassStudent student = BusinessLayer.GetvClassStudentList(String.Format("StudentID = {0} AND GCClassStudyType = '{1}'", StudentID, Constant.ClassStudyType.REGULAR))[0];
            tdStudentName.InnerHtml = student.StudentName;
            tdNIS.InnerHtml = student.StudentCode;
            tdClass.InnerHtml = String.Format("{0} / {1}", student.SchoolClassName, student.PeriodSectionName);
            tdSchoolPeriod.InnerHtml = student.SchoolPeriodName;

            Site site = BusinessLayer.GetSite(AppSession.UserLogin.SiteID);
            tdSchoolName.InnerHtml = site.SiteName;

            List<vClassSubject> lstClassSubject = BusinessLayer.GetvClassSubjectList(String.Format("SchoolClassID = 1 AND SchoolPeriodID = 2 AND IsDeleted = 0"));
            String lstClassSubjectID = String.Join(",", lstClassSubject.Select(x => x.ClassSubjectID));
            lstClassSubjectTask = BusinessLayer.GetvClassSubjectTaskList(String.Format("ClassSubjectID IN ({0})", lstClassSubjectID));

            lstNilai = BusinessLayer.GetClassStudentSubjectTaskMarkList(String.Format("StudentID = {0}", StudentID)).OrderBy(x => x.ClassSubjectTaskID).ToList();
            #endregion

            #region header ulangan
            var temp = lstClassSubjectTask.Where(m => m.GCTaskType == Constant.TaskType.ULANGAN).GroupBy(x => x.ClassSubjectID).Select(s => new { ClassSubjectID = s.Key, Count = s.Count() });
            
            List<String> lstDataHeader = new List<String>();
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
            temp = lstClassSubjectTask.Where(m => m.GCTaskType == Constant.TaskType.TUGAS_KELAS || m.GCTaskType == Constant.TaskType.TUGAS_KELOMPOK || m.GCTaskType == Constant.TaskType.PEKERJAAN_RUMAH).GroupBy(x => x.ClassSubjectID).Select(s => new { ClassSubjectID = s.Key, Count = s.Count() });
            
            MaxTugas = temp.Max(x => x.Count);
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

            rptSubject.DataSource = lstClassSubject;
            rptSubject.DataBind();
        }

        protected void rptSubject_ItemDataBound(object sender, RepeaterItemEventArgs e) 
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                vClassSubject entity = e.Item.DataItem as vClassSubject;

                #region Detail Ulangan
                List<Int32> lstCS = lstClassSubjectTask.Where(m => m.ClassSubjectID == entity.ClassSubjectID && m.GCTaskType == Constant.TaskType.ULANGAN).Select(x => x.ClassSubjectTaskID).ToList();
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
                lstCS = lstClassSubjectTask.Where(m => m.ClassSubjectID == entity.ClassSubjectID && (m.GCTaskType == Constant.TaskType.TUGAS_KELAS || m.GCTaskType == Constant.TaskType.TUGAS_KELOMPOK || m.GCTaskType == Constant.TaskType.PEKERJAAN_RUMAH)).Select(x => x.ClassSubjectTaskID).ToList();
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

                #region UTS
                HtmlTableCell tdDetailUTS = e.Item.FindControl("tdDetailUTS") as HtmlTableCell;
                vClassSubjectTask entityCST = lstClassSubjectTask.FirstOrDefault(x => x.GCTaskType == Constant.TaskType.UTS);
                if (entityCST != null) tdDetailUTS.InnerHtml =  lstNilai.FirstOrDefault(x => x.ClassSubjectTaskID == entityCST.ClassSubjectTaskID).Mark.ToString("N2");
                else tdDetailUTS.InnerHtml = "-";
                #endregion

                #region Final Score
                HtmlTableCell tdFinalScore = e.Item.FindControl("tdFinalScore") as HtmlTableCell;
                tdFinalScore.InnerHtml = "-";
                #endregion
            }
        }
    }
}