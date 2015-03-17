using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common;
using CodeX.Common;
using CodeX.Data.Core.Dal;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class StudentFinalMarkDtCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');
            Student student = BusinessLayer.GetStudent(Convert.ToInt32(temp[0]));
            txtHeaderText.Text = student.StudentName;

            List<vClassSubject> lstSubject = BusinessLayer.GetvClassSubjectList(string.Format("SchoolClassID = {0} AND SubjectGCClassStudyType = '{1}' AND ParentID IS NULL", temp[1], Constant.ClassStudyType.REGULAR));

            string lstClassSubjectID = string.Join(",", lstSubject.Select(p => p.ClassSubjectID).ToList());
            lstMark = BusinessLayer.GetClassStudentSubjectMarkList(String.Format("ClassSubjectID IN ({0}) AND StudentID = {1} AND PeriodSectionID = {2}", lstClassSubjectID, temp[0], temp[2]));
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
            }
        }
    }
}