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
        List<MarkTypeDt> lstMarkTypeDt = null;
        List<MarkTypeFormula> lstMarkTypeFormula = null;

        class CMarkTypeColSpan
        {
            public int CurriculumMarkTypeID { get; set; }
            public int ColSpan { get; set; }
        }
        List<CMarkTypeColSpan> lstMarkTypeColSpan = null;
        List<vCurriculumSubjectMarkType> lstCurriculumMarkType = null;
        protected override void InitializeDataControl()
        {
            tableWidth = 150;

            vClassSubject entityClassSubject = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID)).FirstOrDefault();
            txtPassingGrade.Text = entityClassSubject.PassingGrade.ToString();
            if (entityClassSubject.ParentID == 0)
                hdnParentClassSubjectID.Value = entityClassSubject.ClassSubjectID.ToString();
            else
                hdnParentClassSubjectID.Value = entityClassSubject.ParentID.ToString();
            hdnSubjectID.Value = entityClassSubject.SubjectID.ToString();
            lstMarkTypeColSpan = new List<CMarkTypeColSpan>();
            lstCurriculumMarkType = BusinessLayer.GetvCurriculumSubjectMarkTypeList(string.Format("CurriculumID = {0} AND SubjectID = {1} AND IsDeleted = 0", AppSession.ClassSubject.CurriculumID, entityClassSubject.SubjectID));
            string markTypeFormulaFilterExpression = "";
            foreach (vCurriculumSubjectMarkType curriculumMarkType in lstCurriculumMarkType)
            {
                lstMarkTypeColSpan.Add(new CMarkTypeColSpan { CurriculumMarkTypeID = curriculumMarkType.CurriculumMarkTypeID, ColSpan = 0 });
                if (curriculumMarkType.GCCompetencyDescriptionType != Constant.CompetencyDescriptionType.SEMESTER)
                {
                    if (curriculumMarkType.CompetencyMarkTypeID > 0)
                    {
                        if (markTypeFormulaFilterExpression != "")
                            markTypeFormulaFilterExpression += " OR ";
                        markTypeFormulaFilterExpression += string.Format("(MarkTypeID = {0} AND FromMarkTypeID = {1})", curriculumMarkType.CompetencyMarkTypeID, curriculumMarkType.TaskMarkTypeID);
                    }
                }
            }
            if (markTypeFormulaFilterExpression != "")
                lstMarkTypeFormula = BusinessLayer.GetMarkTypeFormulaList(string.Format("({0}) AND IsDeleted = 0", markTypeFormulaFilterExpression));
            else
                lstMarkTypeFormula = new List<MarkTypeFormula>();
            string lstMarkTypeCompetencyID = string.Join(",", lstCurriculumMarkType.Select(p => p.CompetencyMarkTypeID).ToList());
            lstMarkTypeDt = BusinessLayer.GetMarkTypeDtList(string.Format("MarkTypeID IN ({0}) AND IsDeleted = 0", lstMarkTypeCompetencyID));
            hdnLstMarkTypeDt.Value = string.Join("|", lstMarkTypeDt.Select(p => string.Format("{0};{1}", p.MarkTypeDtID, p.Remarks)));

            lstClassSubjectTaskIndicator = BusinessLayer.GetvClassSubjectTaskIndicatorList(string.Format("ClassSubjectID = {0} AND IsDeleted = 0", AppSession.ClassSubject.ClassSubjectID));
            lstIndicator = (from p in lstClassSubjectTaskIndicator
                            select new vClassSubjectTaskIndicator { SubjectIndicatorName = p.SubjectIndicatorName, CurriculumMarkTypeID = p.CurriculumMarkTypeID }).GroupBy(p => new { p.SubjectIndicatorName, p.CurriculumMarkTypeID }).Select(p => p.First()).ToList();

            rptHeader3.DataSource = lstCurriculumMarkType;
            rptHeader3.DataBind();
            rptHeader2.DataSource = lstCurriculumMarkType;
            rptHeader2.DataBind();
            rptHeader1.DataSource = lstCurriculumMarkType;
            rptHeader1.DataBind();
            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", entityClassSubject.SchoolClassID));

            lstStudentMark = BusinessLayer.GetvClassStudentSubjectTaskMarkList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID));

            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();

            hdnTableWidth.Value = tableWidth.ToString();
        }

        protected void rptHeader1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vCurriculumSubjectMarkType entity = (vCurriculumSubjectMarkType)e.Item.DataItem;
                CMarkTypeColSpan markTypeColSpan = lstMarkTypeColSpan.FirstOrDefault(p => p.CurriculumMarkTypeID == entity.CurriculumMarkTypeID);
                HtmlTableCell thMarkType = (HtmlTableCell)e.Item.FindControl("thMarkType");
                if (markTypeColSpan.ColSpan > 0)
                    thMarkType.ColSpan = markTypeColSpan.ColSpan;
                else
                    thMarkType.Style.Add("display", "none");
            }
        }

        protected void rptHeader2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vCurriculumSubjectMarkType entity = (vCurriculumSubjectMarkType)e.Item.DataItem;
                Repeater rptHeader2Dt = (Repeater)e.Item.FindControl("rptHeader2Dt");
                rptHeader2Dt.DataSource = lstIndicator.Where(p => p.CurriculumMarkTypeID == entity.CurriculumMarkTypeID).ToList();
                rptHeader2Dt.DataBind();
            }
        }

        protected void rptHeader2Dt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTaskIndicator entity = (vClassSubjectTaskIndicator)e.Item.DataItem;
                vCurriculumSubjectMarkType markType = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vCurriculumSubjectMarkType;
                HtmlTableCell thIndicator = (HtmlTableCell)e.Item.FindControl("thIndicator");
                int subjectCount = lstClassSubjectTaskIndicator.Where(p => p.SubjectIndicatorName == entity.SubjectIndicatorName && p.CurriculumMarkTypeID == markType.CurriculumMarkTypeID).Count();
                thIndicator.ColSpan = subjectCount;
                tableWidth += (80 * subjectCount);

                CMarkTypeColSpan markTypeColSpan = lstMarkTypeColSpan.FirstOrDefault(p => p.CurriculumMarkTypeID == markType.CurriculumMarkTypeID);
                markTypeColSpan.ColSpan += subjectCount;
            }
        }

        protected void rptHeader3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vCurriculumSubjectMarkType entity = (vCurriculumSubjectMarkType)e.Item.DataItem;
                Repeater rptHeader3Dt = (Repeater)e.Item.FindControl("rptHeader3Dt");
                rptHeader3Dt.DataSource = lstIndicator.Where(p => p.CurriculumMarkTypeID == entity.CurriculumMarkTypeID).ToList();
                rptHeader3Dt.DataBind();
            }
        }

        protected void rptHeader3Dt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTaskIndicator entity = (vClassSubjectTaskIndicator)e.Item.DataItem;
                vCurriculumSubjectMarkType markType = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vCurriculumSubjectMarkType;
                Repeater rptHeader3Dt2 = (Repeater)e.Item.FindControl("rptHeader3Dt2");
                List<vClassSubjectTaskIndicator> lstIndicatorTemp = lstClassSubjectTaskIndicator.Where(p => p.SubjectIndicatorName == entity.SubjectIndicatorName && p.CurriculumMarkTypeID == markType.CurriculumMarkTypeID).ToList();
                rptHeader3Dt2.DataSource = lstIndicatorTemp;
                rptHeader3Dt2.DataBind();
            }
        }

        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptStudentMarkType = (Repeater)e.Item.FindControl("rptStudentMarkType");
                rptStudentMarkType.DataSource = lstCurriculumMarkType;
                rptStudentMarkType.DataBind();
            }
        }

        protected void rptStudentMarkType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vCurriculumSubjectMarkType entity = (vCurriculumSubjectMarkType)e.Item.DataItem;
                Repeater rptStudentMark = (Repeater)e.Item.FindControl("rptStudentMark");
                rptStudentMark.DataSource = lstIndicator.Where(p => p.CurriculumMarkTypeID == entity.CurriculumMarkTypeID).ToList();
                rptStudentMark.DataBind();
            }
        }

        protected void rptStudentMark_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTaskIndicator entity = (vClassSubjectTaskIndicator)e.Item.DataItem;
                vCurriculumSubjectMarkType markType = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vCurriculumSubjectMarkType;
                vClassStudent student = ((RepeaterItem)((RepeaterItem)e.Item.Parent.Parent).Parent.Parent).DataItem as vClassStudent;

                Repeater rptStudentMarkDt = (Repeater)e.Item.FindControl("rptStudentMarkDt");
                List<vClassSubjectTaskIndicator> lstSubjectIndicator = lstClassSubjectTaskIndicator.Where(p => p.SubjectIndicatorName == entity.SubjectIndicatorName && p.CurriculumMarkTypeID == markType.CurriculumMarkTypeID).ToList();
                rptStudentMarkDt.DataSource = lstSubjectIndicator;
                rptStudentMarkDt.DataBind();
            }
        }

        protected void rptStudentMarkDt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubjectTaskIndicator entity = (vClassSubjectTaskIndicator)e.Item.DataItem;
                vClassStudent student = ((RepeaterItem)((RepeaterItem)((RepeaterItem)e.Item.Parent.Parent).Parent.Parent).Parent.Parent).DataItem as vClassStudent;

                HtmlTableCell tdStudentMark = (HtmlTableCell)e.Item.FindControl("tdStudentMark");
                vClassStudentSubjectTaskMark studentMark = lstStudentMark.FirstOrDefault(p => p.ClassSubjectTaskID == entity.ClassSubjectTaskID && p.StudentID == student.StudentID);
                if (studentMark != null)
                {
                    switch (entity.GCMarkType)
                    {
                        case Constant.SubjectMarkType.NUMBER:
                            tdStudentMark.InnerHtml = studentMark.Mark.ToString();
                            if (studentMark.Mark < Convert.ToDecimal(txtPassingGrade.Text))
                                tdStudentMark.Style.Add("color", "Red");
                            break;
                        case Constant.SubjectMarkType.OPTION: tdStudentMark.InnerHtml = studentMark.MarkTypeDtName; break;
                        case Constant.SubjectMarkType.TEXT: tdStudentMark.InnerHtml = studentMark.DescriptionMark; break;
                    }
                }
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }
    }
}