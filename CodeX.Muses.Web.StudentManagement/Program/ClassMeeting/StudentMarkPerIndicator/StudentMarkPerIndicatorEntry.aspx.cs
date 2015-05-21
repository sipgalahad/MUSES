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
using DevExpress.Web.ASPxEditors;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class StudentMarkPerIndicatorEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.TSC_STUDENT_MARK_PER_INDICATOR;
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
        protected override void InitializeDataControl()
        {
            tableWidth = 150;

            vClassSubject entityClassSubject = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID)).FirstOrDefault();
            txtPassingGrade.Text = entityClassSubject.PassingGrade.ToString();

            List<vCurriculumSubjectMarkType> lstCurriculumMarkType = BusinessLayer.GetvCurriculumSubjectMarkTypeList(string.Format("CurriculumID = {0} AND SubjectID = {1} AND IsDeleted = 0", AppSession.ClassSubject.CurriculumID, entityClassSubject.SubjectID));
            string markTypeFormulaFilterExpression = "";
            foreach (vCurriculumSubjectMarkType curriculumMarkType in lstCurriculumMarkType)
            {
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
            {
                lstMarkTypeFormula = BusinessLayer.GetMarkTypeFormulaList(string.Format("({0}) AND IsDeleted = 0", markTypeFormulaFilterExpression));
                string sMarkTypeFormula = "";
                foreach (MarkTypeFormula markTypeFormula in lstMarkTypeFormula)
                {
                    if (sMarkTypeFormula != "")
                        sMarkTypeFormula += "|";
                    sMarkTypeFormula += string.Format("{0};{1};{2};{3};{4};{5}", markTypeFormula.MarkTypeID, markTypeFormula.FromMarkTypeID, markTypeFormula.MinValue, markTypeFormula.MaxValue, markTypeFormula.FromMarkTypeDtID, markTypeFormula.ToMarkTypeDtID);
                }
                //hdnListMarkTypeFormula.Value = sMarkTypeFormula;
            }
            else
                lstMarkTypeFormula = new List<MarkTypeFormula>();
            string lstMarkTypeCompetencyID = string.Join(",", lstCurriculumMarkType.Select(p => p.CompetencyMarkTypeID).ToList());
            lstMarkTypeDt = BusinessLayer.GetMarkTypeDtList(string.Format("MarkTypeID IN ({0}) AND IsDeleted = 0", lstMarkTypeCompetencyID));

            lstClassSubjectTaskIndicator = BusinessLayer.GetvClassSubjectTaskIndicatorList(string.Format("ClassSubjectID = {0} AND IsDeleted = 0", AppSession.ClassSubject.ClassSubjectID));
            lstIndicator = (from p in lstClassSubjectTaskIndicator
                                                             select new vClassSubjectTaskIndicator { SubjectIndicatorName = p.SubjectIndicatorName }).GroupBy(p => p.SubjectIndicatorName).Select(p => p.First()).ToList();

            rptHeader2.DataSource = lstIndicator;
            rptHeader2.DataBind();
            rptHeader1.DataSource = lstIndicator;
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
                vClassSubjectTaskIndicator entity = (vClassSubjectTaskIndicator)e.Item.DataItem;
                HtmlTableCell thIndicator = (HtmlTableCell)e.Item.FindControl("thIndicator");
                int subjectCount = lstClassSubjectTaskIndicator.Where(p => p.SubjectIndicatorName == entity.SubjectIndicatorName).Count();
                thIndicator.ColSpan = subjectCount + 2;
                tableWidth += (80 * subjectCount) + (2 * 80);
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
                vClassStudent student = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vClassStudent;

                HtmlTableCell tdStudentAvgMark = (HtmlTableCell)e.Item.FindControl("tdStudentAvgMark");
                
                Repeater rptStudentMarkDt = (Repeater)e.Item.FindControl("rptStudentMarkDt");
                List<vClassSubjectTaskIndicator> lstSubjectIndicator = lstClassSubjectTaskIndicator.Where(p => p.SubjectIndicatorName == entity.SubjectIndicatorName).ToList();
                rptStudentMarkDt.DataSource = lstSubjectIndicator;
                rptStudentMarkDt.DataBind();

                List<int> lstClassSubjectTaskID = new List<int>();
                foreach (vClassSubjectTaskIndicator subjectIndicator in lstSubjectIndicator)
                {
                    lstClassSubjectTaskID.Add(subjectIndicator.ClassSubjectTaskID);
                }
                var selected = from u in lstStudentMark
                               where lstClassSubjectTaskID.Contains(u.ClassSubjectTaskID) && u.StudentID == student.StudentID
                               select u;
                decimal avgMark = (selected.Sum(p => p.Mark) / lstSubjectIndicator.Count());
                tdStudentAvgMark.InnerHtml = avgMark.ToString();

                ASPxComboBox cboCompetencyMarkType = (ASPxComboBox)e.Item.FindControl("cboCompetencyMarkType");
                Methods.SetComboBoxField<MarkTypeDt>(cboCompetencyMarkType, lstMarkTypeDt, "MarkTypeDtName", "MarkTypeDtID");
                MarkTypeFormula formula = lstMarkTypeFormula.FirstOrDefault(p => p.MaxValue >= avgMark && p.MinValue <= avgMark);
                if (formula != null)
                    cboCompetencyMarkType.Value = formula.ToMarkTypeDtID.ToString();
                //cboCompetencyMarkType.ClientSideEvents.ValueChanged = "function(s,e){ onCboCompetencyMarkTypeValueChanged(s, " + parentIndex + "," + e.Item.ItemIndex + ",'" + student.PreferredName + "'); }";

                //TextBox txtCompetencyDescription = (TextBox)e.Item.FindControl("txtCompetencyDescription");
                //cboCompetencyMarkType.ClientInstanceName = string.Format("cboCompetencyMarkType{0}{1}", parentIndex.ToString("D2"), e.Item.ItemIndex.ToString("D2"));
                //txtCompetencyDescription.Attributes.Add("positiontag", string.Format("{0}{1}", parentIndex.ToString("D2"), e.Item.ItemIndex.ToString("D2")));
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