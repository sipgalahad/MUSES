using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using System.Data;
using CodeX.Data.Core.Dal;
using CodeX.Common;
using DevExpress.Web.ASPxCallbackPanel;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ClassStudentSubjectMarkList : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.CS_SUBJECT_MARK;
        }

        protected override void InitializeDataControl()
        {
            List<vClassSubject> lstSubject = BusinessLayer.GetvClassSubjectList(string.Format("SchoolClassID = {0} AND SubjectGCClassStudyType = '{1}' AND ParentID IS NULL", AppSession.ClassStudent.SchoolClassID, Constant.ClassStudyType.REGULAR));

            string lstClassSubjectID = string.Join(",", lstSubject.Select(p => p.ClassSubjectID).ToList());
            lstMark = BusinessLayer.GetClassStudentSubjectMarkList(String.Format("ClassSubjectID IN ({0}) AND StudentID = {1} AND PeriodSectionID = {2}", lstClassSubjectID, AppSession.ClassStudent.StudentID, AppSession.ClassStudent.PeriodSectionID));
            grdView.DataSource = lstSubject;
            grdView.DataBind();
        }

        List<ClassStudentSubjectMark> lstMark = null;
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vClassSubject entity = (vClassSubject)e.Row.DataItem;
                ClassStudentSubjectMark studentMark = lstMark.FirstOrDefault(p => p.ClassSubjectID == entity.ClassSubjectID);
                HtmlGenericControl divMarkTheory = (HtmlGenericControl)e.Row.FindControl("divMarkTheory");
                HtmlGenericControl divMarkPractice = (HtmlGenericControl)e.Row.FindControl("divMarkPractice");

                if (studentMark != null)
                {
                    divMarkTheory.InnerHtml = studentMark.TheoryMark.ToString();
                    divMarkPractice.InnerHtml = studentMark.PracticeMark.ToString();
                }
                else
                {
                    divMarkTheory.InnerHtml = "-";
                    divMarkPractice.InnerHtml = "-";
                }

                if (entity.GCLessonType == Constant.LessonType.PRACTICE)
                    divMarkTheory.InnerHtml = "-";
                else if (entity.GCLessonType == Constant.LessonType.THEORY)
                    divMarkPractice.InnerHtml = "-";
            }
        }
    }
}