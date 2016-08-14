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

        List<vCurriculumMarkType> lstCurriculumMarkType = null;
        List<vCurriculumMarkType> lstCurriculumMarkTypeDesc = null;
        protected override void InitializeDataControl()
        {
            List<vClassSubject> lstSubject = BusinessLayer.GetvClassSubjectList(string.Format("SchoolClassID = {0} AND SubjectGCClassStudyType = '{1}' AND ParentID IS NULL", AppSession.ClassStudent.SchoolClassID, Constant.ClassStudyType.REGULAR));

            vSchoolClass entitySchoolClass = BusinessLayer.GetvSchoolClassList(String.Format("SchoolClassID = {0}", AppSession.ClassStudent.SchoolClassID)).FirstOrDefault();

            hdnCurriculumID.Value = entitySchoolClass.CurriculumID.ToString();
            lstCurriculumMarkType = BusinessLayer.GetvCurriculumMarkTypeList(string.Format("CurriculumID = {0} AND IsDeleted = 0", entitySchoolClass.CurriculumID));
            lstCurriculumMarkTypeDesc = lstCurriculumMarkType.Where(p => p.IsShowCompetencyDescription).ToList();
            rptHeader2Desc.DataSource = lstCurriculumMarkTypeDesc;
            rptHeader2Desc.DataBind();

            rptHeader2.DataSource = lstCurriculumMarkType;
            rptHeader2.DataBind();

            rptHeader3.DataSource = lstCurriculumMarkType.Where(p => p.PredicateMarkTypeID > 0).ToList();
            rptHeader3.DataBind();

            thMark.ColSpan = tableHeaderColSpan;

            if (lstCurriculumMarkTypeDesc.Count > 0)
                thCompetencyDescription.ColSpan = lstCurriculumMarkTypeDesc.Count;
            else
                thCompetencyDescription.Style.Add("display", "none");

            string lstClassSubjectID = string.Join(",", lstSubject.Select(p => p.ClassSubjectID).ToList());
            lstMark = BusinessLayer.GetvClassStudentSubjectMarkList(String.Format("ClassSubjectID IN ({0}) AND StudentID = {1} AND PeriodSectionID = {2}", lstClassSubjectID, AppSession.ClassStudent.StudentID, AppSession.ClassStudent.PeriodSectionID));
            rptView.DataSource = lstSubject;
            rptView.DataBind();
        }

        int tableHeaderColSpan = 0;
        protected void rptHeader2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vCurriculumMarkType entity = (vCurriculumMarkType)e.Item.DataItem;
                HtmlTableCell thHeader = (HtmlTableCell)e.Item.FindControl("thHeader");
                thHeader.InnerHtml = entity.CurriculumMarkTypeName;

                if (entity.PredicateMarkTypeID > 0)
                {
                    thHeader.ColSpan = 2;
                    tableHeaderColSpan += 2;
                }
                else
                {
                    thHeader.RowSpan = 2;
                    tableHeaderColSpan++;
                }
            }
        }

        #region HTML Getter
        public string GetFilterExpression() 
        {
            PeriodSection ps = BusinessLayer.GetPeriodSection(AppSession.ClassStudent.PeriodSectionID);
            return String.Format("{0}|{1}|{2}|{3}", ps.SchoolPeriodID, AppSession.ClassStudent.PeriodSectionID,AppSession.ClassStudent.SchoolClassID, AppSession.ClassStudent.StudentID);
        }
        #endregion

        List<vClassStudentSubjectMark> lstMark = null;
        protected void rptView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptStudentMark = (Repeater)e.Item.FindControl("rptStudentMark");
                rptStudentMark.DataSource = lstCurriculumMarkType;
                rptStudentMark.DataBind();

                Repeater rptStudentMarkDesc = (Repeater)e.Item.FindControl("rptStudentMarkDesc");
                rptStudentMarkDesc.DataSource = lstCurriculumMarkTypeDesc;
                rptStudentMarkDesc.DataBind();
            }
        }

        protected void rptStudentMark_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubject entityClassSubject = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vClassSubject;
                vCurriculumMarkType markType = (vCurriculumMarkType)e.Item.DataItem;
                vClassStudentSubjectMark studentMark = lstMark.FirstOrDefault(p => p.ClassSubjectID == entityClassSubject.ClassSubjectID && p.CurriculumMarkTypeID == markType.CurriculumMarkTypeID);

                HtmlTableCell tdFinalMark = (HtmlTableCell)e.Item.FindControl("tdFinalMark");
                HtmlTableCell tdPredicateMark = (HtmlTableCell)e.Item.FindControl("tdPredicateMark");
                if (markType.PredicateMarkTypeID > 0)
                {
                    if (studentMark != null)
                        tdPredicateMark.InnerHtml = studentMark.PredicateMarkTypeDtName;
                    else
                        tdPredicateMark.InnerHtml = "-";
                }
                else
                    tdPredicateMark.Style.Add("display", "none");

                if (studentMark != null)
                {
                    switch (markType.FinalGCMarkType)
                    {
                        case Constant.SubjectMarkType.OPTION: tdFinalMark.InnerHtml = studentMark.MarkTypeDtName; break;
                        case Constant.SubjectMarkType.NUMBER: tdFinalMark.InnerHtml = studentMark.Mark.ToString(); break;
                        case Constant.SubjectMarkType.TEXT: tdFinalMark.InnerHtml = studentMark.DescriptionMark; break;
                    }
                    
                }
                else
                    tdFinalMark.InnerHtml = "-";
            }
        }

        protected void rptStudentMarkDesc_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubject entityClassSubject = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vClassSubject;
                vCurriculumMarkType markType = (vCurriculumMarkType)e.Item.DataItem;
                vClassStudentSubjectMark studentMark = lstMark.FirstOrDefault(p => p.ClassSubjectID == entityClassSubject.ClassSubjectID);
                HtmlTableCell tdDescription = (HtmlTableCell)e.Item.FindControl("tdDescription");
                if (studentMark != null)
                    tdDescription.InnerHtml = studentMark.CompetencyDescription;
                else
                    tdDescription.InnerHtml = "-";
            }
        }
    }
}