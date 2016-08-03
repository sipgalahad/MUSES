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
    public partial class ClassStudentPersonalityMarkList : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.CS_PERSONALITY_MARK;
        }

        List<vStudentNote> lstStudentNote = null;
        List<StandardCode> lstNoteRate = null;
        List<MarkTypeDt> lstOption = null;
        protected override void InitializeDataControl()
        {
            List<vClassSubject> lstSubject = BusinessLayer.GetvClassSubjectList(string.Format("SchoolClassID = {0} AND SubjectGCClassStudyType = '{1}' AND ParentID IS NULL", AppSession.ClassStudent.SchoolClassID, Constant.ClassStudyType.PERSONALITY));
            vSchoolClass sc = BusinessLayer.GetvSchoolClassList(string.Format("SchoolClassID = {0}", AppSession.ClassStudent.SchoolClassID)).FirstOrDefault();

            CurriculumMarkType entityMarkType = BusinessLayer.GetCurriculumMarkTypeList(string.Format("CurriculumID = {0} AND GCStudentMarkGroup = '{1}' AND IsDeleted = 0", sc.CurriculumID, Constant.StudentMarkGroup.AFFECTIVE)).FirstOrDefault();
            hdnMarkTypeID.Value = entityMarkType.PredicateMarkTypeID.ToString();
            hdnCurriculumMarkTypeID.Value = entityMarkType.CurriculumMarkTypeID.ToString();

            lstOption = BusinessLayer.GetMarkTypeDtList(string.Format("MarkTypeID = {0} AND IsDeleted = 0", hdnMarkTypeID.Value));
            lstOption.Insert(0, new MarkTypeDt { MarkTypeDtID = 0, MarkTypeDtName = "" });

            string lstClassSubjectID = string.Join(",", lstSubject.Select(p => p.ClassSubjectID).ToList());
            if (lstClassSubjectID != "")
                lstMark = BusinessLayer.GetClassStudentSubjectMarkList(String.Format("ClassSubjectID IN ({0}) AND StudentID = {1} AND PeriodSectionID = {2}", lstClassSubjectID, AppSession.ClassStudent.StudentID, AppSession.ClassStudent.PeriodSectionID));
            else
                lstMark = new List<ClassStudentSubjectMark>();
            grdView.DataSource = lstSubject;
            grdView.DataBind();

            BindGridView();
        }

        List<ClassStudentSubjectMark> lstMark = null;
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vClassSubject entity = (vClassSubject)e.Row.DataItem;
                ClassStudentSubjectMark studentMark = lstMark.FirstOrDefault(p => p.ClassSubjectID == entity.ClassSubjectID);
                TextBox txtMarkDescription = (TextBox)e.Row.FindControl("txtMarkDescription");
                DropDownList ddlMark = (DropDownList)e.Row.FindControl("ddlMark");
                Methods.SetComboBoxField<MarkTypeDt>(ddlMark, lstOption, "MarkTypeDtName", "MarkTypeDtID");

                if (studentMark != null)
                {
                    txtMarkDescription.Text = studentMark.DescriptionMark;
                    ddlMark.SelectedValue = studentMark.PredicateMarkTypeDtID.ToString();
                }
            }
        }

        private void BindGridView()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.STUDENT_NOTE_CATEGORY, Constant.StandardCode.STUDENT_NOTE_RATE));
            lstNoteRate = lstSc.Where(p => p.ParentID == Constant.StandardCode.STUDENT_NOTE_RATE).ToList();
            List<StandardCode> lstNoteCategory = lstSc.Where(p => p.ParentID == Constant.StandardCode.STUDENT_NOTE_CATEGORY).ToList();

            string filterExpression = string.Format("StudentID = {0} AND PeriodSectionID = {1} AND IsDeleted = 0", AppSession.ClassStudent.StudentID, AppSession.ClassStudent.PeriodSectionID);
            lstStudentNote = BusinessLayer.GetvStudentNoteList(filterExpression);

            rptNoteRateHeader.DataSource = lstNoteRate;
            rptNoteRateHeader.DataBind();

            rptNoteCategory.DataSource = lstNoteCategory;
            rptNoteCategory.DataBind();
        }

        protected void rptNoteCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                StandardCode noteCategory = ((RepeaterItem)e.Item.Parent.Parent).DataItem as StandardCode;

                List<vStudentNote> lstStudentNote1 = lstStudentNote.Where(p => p.GCNoteCategory == noteCategory.StandardCodeID && p.GCNoteRate == noteRate.StandardCodeID).ToList();

                HtmlGenericControl divStudentNoteRateCount = (HtmlGenericControl)e.Item.FindControl("divStudentNoteRateCount");
                if (lstStudentNote1.Count > 0)
                    divStudentNoteRateCount.InnerHtml = lstStudentNote1.Count.ToString();
                else
                    divStudentNoteRateCount.InnerHtml = "-";
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            if (type == "save")
            {
                bool result = true;
                IDbContext ctx = DbFactory.Configure(true);
                ClassStudentSubjectMarkDao entityDao = new ClassStudentSubjectMarkDao(ctx);
                try
                {
                    List<ClassStudentSubjectMark> lstEntity = BusinessLayer.GetClassStudentSubjectMarkList(string.Format("ClassSubjectID IN ({0}) AND StudentID = {1} AND PeriodSectionID = {2}", hdnLstClassSubjectID.Value, AppSession.ClassStudent.StudentID, AppSession.ClassStudent.PeriodSectionID), ctx);
                    string[] lstSaveValue = hdnSaveValue.Value.Split('|');
                    foreach (string saveValue in lstSaveValue)
                    {
                        string[] temp = saveValue.Split(';');
                        int ClassSubjectID = Convert.ToInt32(temp[0]);
                        ClassStudentSubjectMark entity = lstEntity.FirstOrDefault(p => p.ClassSubjectID == ClassSubjectID);
                        if (entity == null)
                        {
                            entity = new ClassStudentSubjectMark();
                            entity.ClassSubjectID = ClassSubjectID;
                            entity.PeriodSectionID = AppSession.ClassStudent.PeriodSectionID;
                            entity.StudentID = AppSession.ClassStudent.StudentID;
                            entity.CurriculumMarkTypeID = Convert.ToInt32(hdnCurriculumMarkTypeID.Value);
                            if (temp[1] != "0")
                                entity.PredicateMarkTypeDtID = Convert.ToInt32(temp[1]);
                            else
                                entity.PredicateMarkTypeDtID = null;
                            entity.DescriptionMark = temp[2];
                            entityDao.Insert(entity);
                        }
                        else
                        {
                            if (temp[1] != "0")
                                entity.PredicateMarkTypeDtID = Convert.ToInt32(temp[1]);
                            else
                                entity.PredicateMarkTypeDtID = null;
                            entity.DescriptionMark = temp[2];
                            entityDao.Update(entity);
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
            return false;
        }
    }
}