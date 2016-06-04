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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class StudentNoteEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.WS_STUDENT_NOTE;
        }
        protected override void InitializeDataControl()
        {
            BindGridView();
        }

        private void BindGridView()
        {
            string filterExpression = string.Format("ClassMeetingID = {0}", AppSession.ClassSubject.ClassMeetingID);
            lstStudentNote = BusinessLayer.GetvStudentNoteList(filterExpression);

            ClassSubject classSubject = BusinessLayer.GetClassSubject(AppSession.ClassSubject.ClassSubjectID);
            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", classSubject.SchoolClassID));
            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        List<vStudentNote> lstStudentNote = null;
        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassStudent entity = (vClassStudent)e.Item.DataItem;
                List<vStudentNote> lstStudentNote1 = lstStudentNote.Where(p => p.StudentID == entity.StudentID).ToList();
                if (lstStudentNote1.Count > 0)
                {
                    TextBox txtStudentNote = (TextBox)e.Item.FindControl("txtStudentNote");
                    txtStudentNote.Text = String.Join(";", lstStudentNote1.Select(p => string.Format("({0}-{1}) {2}", p.NoteCategory, p.NoteRateInitial, p.Remarks)).ToList());
                }
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }
    }
}