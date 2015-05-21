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
    public partial class StudentMarkPerIndicatorInformation : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.WS_STUDENT_MARK_PER_INDICATOR;
        }

        protected string OnGetTableViewWidth()
        {
            return hdnTableWidth.Value;
        }

        int tableWidth = 0;
        List<vClassSubjectTaskIndicator> lstClassSubjectTaskIndicator = null;
        List<vClassSubjectTaskIndicator> lstIndicator = null;
        List<vClassStudentSubjectTaskMark> lstStudentMark = null;
        protected override void InitializeDataControl()
        {
            tableWidth = 150;

            lstClassSubjectTaskIndicator = BusinessLayer.GetvClassSubjectTaskIndicatorList(string.Format("ClassSubjectID = {0} AND IsDeleted = 0", AppSession.ClassSubject.ClassSubjectID));
            lstIndicator = (from p in lstClassSubjectTaskIndicator
                                                             select new vClassSubjectTaskIndicator { SubjectIndicatorName = p.SubjectIndicatorName }).GroupBy(p => p.SubjectIndicatorName).Select(p => p.First()).ToList();

            rptHeader2.DataSource = lstIndicator;
            rptHeader2.DataBind();
            rptHeader1.DataSource = lstIndicator;
            rptHeader1.DataBind();

            ClassSubject classSubject = BusinessLayer.GetClassSubject(AppSession.ClassSubject.ClassSubjectID);
            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", classSubject.SchoolClassID));

            lstStudentMark = BusinessLayer.GetvClassStudentSubjectTaskMarkList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID));

            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();

            hdnTableWidth.Value = tableWidth.ToString();
        }

        protected void rptHeader1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTaskIndicator entity = (vClassSubjectTaskIndicator)e.Item.DataItem;
                HtmlTableCell thIndicator = (HtmlTableCell)e.Item.FindControl("thIndicator");
                thIndicator.ColSpan = lstClassSubjectTaskIndicator.Where(p => p.SubjectIndicatorName == entity.SubjectIndicatorName).Count();
            }
        }

        protected void rptHeader2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTaskIndicator entity = (vClassSubjectTaskIndicator)e.Item.DataItem;
                Repeater rptHeader2Dt = (Repeater)e.Item.FindControl("rptHeader2Dt");
                rptHeader2Dt.DataSource = lstClassSubjectTaskIndicator.Where(p => p.SubjectIndicatorName == entity.SubjectIndicatorName).ToList();
                rptHeader2Dt.DataBind();
            }
        }

        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptStudentMark = (Repeater)e.Item.FindControl("rptStudentMark");
                rptStudentMark.DataSource = lstIndicator;
                rptStudentMark.DataBind();
            }
        }

        protected void rptStudentMark_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTaskIndicator entity = (vClassSubjectTaskIndicator)e.Item.DataItem;
                Repeater rptStudentMarkDt = (Repeater)e.Item.FindControl("rptStudentMarkDt");
                rptStudentMarkDt.DataSource = lstClassSubjectTaskIndicator.Where(p => p.SubjectIndicatorName == entity.SubjectIndicatorName).ToList();
                rptStudentMarkDt.DataBind();
            }
        }

        protected void rptStudentMarkDt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTaskIndicator entity = (vClassSubjectTaskIndicator)e.Item.DataItem;
                vClassStudent student = ((RepeaterItem)((RepeaterItem)e.Item.Parent.Parent).Parent.Parent).DataItem as vClassStudent;

                HtmlTableCell tdStudentMark = (HtmlTableCell)e.Item.FindControl("tdStudentMark");
                vClassStudentSubjectTaskMark studentMark = lstStudentMark.FirstOrDefault(p => p.ClassSubjectTaskID == entity.ClassSubjectTaskID && p.StudentID == student.StudentID);
                if (studentMark != null)
                {
                    switch (entity.GCMarkType)
                    {
                        case Constant.SubjectMarkType.NUMBER: tdStudentMark.InnerHtml = studentMark.Mark.ToString(); break;
                        case Constant.SubjectMarkType.OPTION: tdStudentMark.InnerHtml = studentMark.MarkTypeDtName; break;
                        case Constant.SubjectMarkType.TEXT: tdStudentMark.InnerHtml = studentMark.DescriptionMark; break;
                    }
                }
                tableWidth += 90;
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }
    }
}