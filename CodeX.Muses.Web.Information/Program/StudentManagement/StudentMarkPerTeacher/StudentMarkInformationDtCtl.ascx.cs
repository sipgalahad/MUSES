using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using DevExpress.Web.ASPxCallbackPanel;
using System.Web.UI.HtmlControls;
using CodeX.Data.Core.Dal;
using CodeX.Muses.Web.Information.Program;
using CodeX.Common;

namespace CodeX.Muses.Web.Information.Program
{
    public partial class StudentMarkInformationDtCtl : BaseViewPopupCtl
    {
        List<vClassSubjectTask> lstClassTask = null;
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');
            hdnPeriodSection.Value = temp[0];
            hdnClassSubjectID.Value = temp[1];
            BindGridView();
        }

        private void BindGridView()
        {
            lstClassTask = BusinessLayer.GetvClassSubjectTaskList(string.Format("ClassSubjectID = {0} AND PeriodSectionID = {1} AND IsDeleted = 0", hdnClassSubjectID.Value, hdnPeriodSection.Value));
            rptHeader.DataSource = lstClassTask;
            rptHeader.DataBind();

            thMark.ColSpan = lstClassTask.Count;

            lstStudentMark = BusinessLayer.GetvClassStudentSubjectTaskMarkList(string.Format("ClassSubjectID = {0} AND PeriodSectionID = {1}", hdnClassSubjectID.Value, hdnPeriodSection.Value));

            vClassSubject classSubject = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID = {0}", hdnClassSubjectID.Value)).FirstOrDefault();
            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", classSubject.SchoolClassID));
            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();
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
    }
}