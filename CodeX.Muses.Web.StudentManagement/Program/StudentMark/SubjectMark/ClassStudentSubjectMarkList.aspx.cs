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
            rptView.DataSource = lstSubject;
            rptView.DataBind();
        }

        #region HTML Getter
        public string GetFilterExpression() 
        {
            PeriodSection ps = BusinessLayer.GetPeriodSection(AppSession.ClassStudent.PeriodSectionID);
            return String.Format("{0}|{1}|{2}|{3}", ps.SchoolPeriodID, AppSession.ClassStudent.PeriodSectionID,AppSession.ClassStudent.SchoolClassID, AppSession.ClassStudent.StudentID);
        }
        #endregion

        List<ClassStudentSubjectMark> lstMark = null;
        protected void rptView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubject entity = (vClassSubject)e.Item.DataItem;
                ClassStudentSubjectMark studentMark = lstMark.FirstOrDefault(p => p.ClassSubjectID == entity.ClassSubjectID);
                HtmlGenericControl divMarkTheory = (HtmlGenericControl)e.Item.FindControl("divMarkTheory");
                HtmlGenericControl divMarkPractice = (HtmlGenericControl)e.Item.FindControl("divMarkPractice");
                HtmlGenericControl divAffectiveMark = (HtmlGenericControl)e.Item.FindControl("divAffectiveMark");
                HtmlGenericControl divAffectiveDescription = (HtmlGenericControl)e.Item.FindControl("divAffectiveDescription");
                HtmlGenericControl divProgressDescription = (HtmlGenericControl)e.Item.FindControl("divProgressDescription");

                if (studentMark != null)
                {
                    //divMarkTheory.InnerHtml = studentMark.TheoryMark.ToString();
                    //divMarkPractice.InnerHtml = studentMark.PracticeMark.ToString();
                    //divAffectiveMark.InnerHtml = studentMark.AffectiveMark;
                    //divAffectiveDescription.InnerHtml = studentMark.AffectiveDescription;
                    //divProgressDescription.InnerHtml = studentMark.ProgressDescription;
                }
                else
                {
                    divMarkTheory.InnerHtml = "-";
                    divMarkPractice.InnerHtml = "-";
                    divAffectiveMark.InnerHtml = "-";
                    divAffectiveDescription.InnerHtml = "-";
                    divProgressDescription.InnerHtml = "-";
                }

                if (entity.GCLessonType == Constant.LessonType.PRACTICE)
                    divMarkTheory.InnerHtml = "-";
                else if (entity.GCLessonType == Constant.LessonType.THEORY)
                    divMarkPractice.InnerHtml = "-";
            }
        }
    }
}