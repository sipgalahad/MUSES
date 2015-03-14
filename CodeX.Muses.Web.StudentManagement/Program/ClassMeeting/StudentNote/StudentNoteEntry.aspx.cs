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
            lstStudentNote = BusinessLayer.GetStudentNoteList(filterExpression);

            ClassSubject classSubject = BusinessLayer.GetClassSubject(AppSession.ClassSubject.ClassSubjectID);
            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", classSubject.SchoolClassID));
            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();
        }

        protected void cbpMeetingDetail_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        List<StudentNote> lstStudentNote = null;
        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassStudent entity = (vClassStudent)e.Item.DataItem;
                StudentNote studentMark = lstStudentNote.FirstOrDefault(p => p.StudentID == entity.StudentID);
                if (studentMark != null)
                {
                    TextBox txtStudentNote = (TextBox)e.Item.FindControl("txtStudentNote");
                    txtStudentNote.Text = studentMark.Remarks;
                }
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            StudentNoteDao entityDtDao = new StudentNoteDao(ctx);
            try
            {
                string[] lstSaveValue = hdnListSaveValue.Value.Split('|');

                List<StudentNote> lstStudentMark = BusinessLayer.GetStudentNoteList(string.Format("ClassMeetingID = {0}", AppSession.ClassSubject.ClassMeetingID), ctx);
                foreach (String saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(',');
                    int studentID = Convert.ToInt32(temp[0]);
                    StudentNote entityDt = lstStudentMark.FirstOrDefault(p => p.StudentID == studentID);
                    if (entityDt == null)
                    {
                        if (temp[1] != "")
                        {
                            entityDt = new StudentNote();
                            entityDt.ClassMeetingID = AppSession.ClassSubject.ClassMeetingID;
                            entityDt.ClassSubjectID = AppSession.ClassSubject.ClassSubjectID;
                            entityDt.PeriodSectionID = AppSession.ClassSubject.PeriodSectionID;
                            entityDt.NoteDate = DateTime.Now;
                            entityDt.NoteTime = DateTime.Now.ToString(Constant.FormatString.TIME_FORMAT);
                            entityDt.StudentID = studentID;
                            entityDt.Remarks = temp[1];
                            entityDtDao.Insert(entityDt);
                        }
                    }
                    else
                    {
                        entityDt.Remarks = temp[1];
                        entityDtDao.Update(entityDt);
                    }
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                result = false;
                errMessage = ex.Message;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
    }
}