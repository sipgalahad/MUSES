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
    public partial class StudentNoteInformation : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.WS_STUDENT_NOTE_INFORMATION;
        }

        List<vStudentNote> lstStudentNote = null;
        List<StandardCode> lstNoteRate = null;
        protected override void InitializeDataControl()
        {
            vClassSubject entityClassSubject = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID)).FirstOrDefault();
            txtPassingGrade.Text = entityClassSubject.PassingGrade.ToString();
            if (entityClassSubject.ParentID == 0)
                hdnParentClassSubjectID.Value = entityClassSubject.ClassSubjectID.ToString();
            else
                hdnParentClassSubjectID.Value = entityClassSubject.ParentID.ToString();
            hdnSubjectID.Value = entityClassSubject.SubjectID.ToString();
            hdnSchoolPeriodID.Value = entityClassSubject.SchoolPeriodID.ToString();

            List<StandardCode> lstNoteCategory = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.STUDENT_NOTE_CATEGORY));
            lstNoteCategory.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "-- Semua --" });
            Methods.SetComboBoxField<StandardCode>(cboNoteCategory, lstNoteCategory, "StandardCodeName", "StandardCodeID");
            cboNoteCategory.SelectedIndex = 0;

            BindGridView();
        }

        private void BindGridView()
        {
            lstNoteRate = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.STUDENT_NOTE_RATE));

            string filterExpression = string.Format("ClassSubjectID = {0} AND PeriodSectionID = {1} AND IsDeleted = 0", AppSession.ClassSubject.ClassSubjectID, AppSession.ClassSubject.PeriodSectionID);
            if (cboNoteCategory.Value != null && cboNoteCategory.Value.ToString() != "")
                filterExpression += string.Format(" AND GCNoteCategory = '{0}'", cboNoteCategory.Value);
            lstStudentNote = BusinessLayer.GetvStudentNoteList(filterExpression);

            rptNoteRateHeader.DataSource = lstNoteRate;
            rptNoteRateHeader.DataBind();

            ClassSubject classSubject = BusinessLayer.GetClassSubject(AppSession.ClassSubject.ClassSubjectID);
            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", classSubject.SchoolClassID));
            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();

        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }
        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptNoteRate = (Repeater)e.Item.FindControl("rptNoteRate");
                rptNoteRate.DataSource = lstNoteRate;
                rptNoteRate.DataBind();
            }
        }

        protected void rptNoteRate_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                StandardCode noteRate = (StandardCode)e.Item.DataItem;
                vClassStudent student = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vClassStudent;

                List<vStudentNote> lstStudentNote1 = lstStudentNote.Where(p => p.StudentID == student.StudentID && p.GCNoteRate == noteRate.StandardCodeID).ToList();

                HtmlGenericControl divStudentNoteRateCount = (HtmlGenericControl)e.Item.FindControl("divStudentNoteRateCount");
                if (lstStudentNote1.Count > 0)
                    divStudentNoteRateCount.InnerHtml = lstStudentNote1.Count.ToString();
                else
                    divStudentNoteRateCount.InnerHtml = "-";
            }
        }
    }
}