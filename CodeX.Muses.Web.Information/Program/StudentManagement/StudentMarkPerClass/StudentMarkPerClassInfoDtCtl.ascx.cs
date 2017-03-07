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
    public partial class StudentMarkPerClassInfoDtCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int CurrPage = 1;
        private StudentMarkPerClassInfo DetailPage
        {
            get { return (StudentMarkPerClassInfo)Page; }
        }

        public override void InitializeDataControl(string param)
        {
            String[] lstParam = param.Split('|');
            hdnSchoolClassID.Value = lstParam[0];
            DateTime dateFrom = DetailPage.OnGetDateFrom();
            DateTime dateTo = DetailPage.OnGetDateTo();

            SchoolClass entity = BusinessLayer.GetSchoolClass(Convert.ToInt32(hdnSchoolClassID.Value));
            txtHeaderText3.Text = entity.SchoolClassName;

            lstStudentMark = BusinessLayer.GetvClassStudentSubjectTaskMarkList(string.Format("SchoolClassID = {0} AND TaskDate BETWEEN '{1}' AND '{2}' AND Mark < PassingGrade", hdnSchoolClassID.Value, dateFrom.ToString("yyyyMMdd"), dateFrom.ToString("yyyyMMdd")));

            string lstTaskID = string.Join(",", lstStudentMark.Select(p => p.ClassSubjectTaskID).ToList());
            if (lstTaskID != "")
                lstClassTask = BusinessLayer.GetvClassSubjectTaskList(string.Format("ClassSubjectTaskID IN ({0}) AND IsDeleted = 0", lstTaskID));
            else
                lstClassTask = new List<vClassSubjectTask>();
            rptHeader.DataSource = lstClassTask;
            rptHeader.DataBind();

            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0} AND StudentID IN (SELECT StudentID FROM vClassStudentSubjectTaskMark WHERE SchoolClassID = {0} AND TaskDate BETWEEN '{1}' AND '{2}' AND Mark < PassingGrade)", entity.SchoolClassID, dateFrom.ToString("yyyyMMdd"), dateFrom.ToString("yyyyMMdd")));
            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();
        }

        List<vClassSubjectTask> lstClassTask = null;
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
                    if (entity.Mark < entity.PassingGrade)
                        divStudentMark.InnerHtml = entity.Mark.ToString();
                }
            }
        }
    }
}