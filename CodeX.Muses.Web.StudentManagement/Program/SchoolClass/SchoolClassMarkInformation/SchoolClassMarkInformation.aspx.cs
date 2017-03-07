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
    public partial class SchoolClassMarkInformation : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            string id = Request.QueryString["id"];
            if (id == "cs")
                return Constant.MenuCode.StudentManagement.SC_STUDENT_MARK;
            return Constant.MenuCode.StudentManagement.MTSC_STUDENT_MARK;
        }

        List<vClassSubjectTask> lstClassTask = null;
        protected override void InitializeDataControl()
        {
            List<vClassSubject> lstClassSubject = BusinessLayer.GetvClassSubjectList(string.Format("SchoolClassID = {0} AND SubjectGCClassStudyType = '{1}' AND IsDeleted = 0", AppSession.SchoolClass.SchoolClassID, Constant.ClassStudyType.REGULAR));
            Methods.SetComboBoxField<vClassSubject>(cboSubject, lstClassSubject, "SubjectName", "ClassSubjectID");

            BindGridView();
        }

        private void BindGridView()
        {
            if (cboSubject.Value != null)
            {
                lstClassTask = BusinessLayer.GetvClassSubjectTaskList(string.Format("ClassSubjectID = {0} AND PeriodSectionID = {1} AND IsDeleted = 0", cboSubject.Value, AppSession.SchoolClass.PeriodSectionID));
                rptHeader.DataSource = lstClassTask;
                rptHeader.DataBind();

                thMark.ColSpan = lstClassTask.Count;

                lstStudentMark = BusinessLayer.GetvClassStudentSubjectTaskMarkList(string.Format("ClassSubjectID = {0} AND PeriodSectionID = {1}", cboSubject.Value, AppSession.SchoolClass.PeriodSectionID));

                List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", AppSession.SchoolClass.SchoolClassID));
                rptStudent.DataSource = lstStudent;
                rptStudent.DataBind();
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        List<vClassStudentSubjectTaskMark> lstStudentMark = null;
        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptStudentAttendance = (Repeater)e.Item.FindControl("rptStudentAttendance");
                rptStudentAttendance.DataSource = lstClassTask;
                rptStudentAttendance.DataBind();
            }
        }

        protected void rptStudentAttendance_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTask subjectTask = (vClassSubjectTask)e.Item.DataItem;
                vClassStudent student = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vClassStudent;

                vClassStudentSubjectTaskMark entity = lstStudentMark.FirstOrDefault(p => p.ClassSubjectTaskID == subjectTask.ClassSubjectTaskID && p.StudentID == student.StudentID);
                if (entity != null)
                {
                    HtmlGenericControl divStudentMark = (HtmlGenericControl)e.Item.FindControl("divStudentMark");
                    switch (subjectTask.GCMarkType)
                    {
                        case Constant.SubjectMarkType.NUMBER: divStudentMark.InnerHtml = entity.Mark.ToString(); break;
                        case Constant.SubjectMarkType.OPTION: divStudentMark.InnerHtml = entity.MarkTypeDtName; break;
                        case Constant.SubjectMarkType.TEXT: divStudentMark.InnerHtml = entity.DescriptionMark; break;
                    }
                }
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        public override Control OnGetExportControl()
        {
            if (cboSubject.Value != null)
            {
                lstClassTask = BusinessLayer.GetvClassSubjectTaskList(string.Format("ClassSubjectID = {0} AND IsDeleted = 0", Request.Form[hdnSubject.UniqueID]));
                rptHeader2.DataSource = lstClassTask;
                rptHeader2.DataBind();

                thMark2.ColSpan = lstClassTask.Count;

                lstStudentMark = BusinessLayer.GetvClassStudentSubjectTaskMarkList(string.Format("ClassSubjectID = {0}", Request.Form[hdnSubject.UniqueID]));

                List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", AppSession.SchoolClass.SchoolClassID));
                rptStudent2.DataSource = lstStudent;
                rptStudent2.DataBind();
                HtmlGenericControl div = new HtmlGenericControl("DIV");
                div.Controls.Add(pnlPrint);
                return div;
            }
            return null;
        }
    }
}