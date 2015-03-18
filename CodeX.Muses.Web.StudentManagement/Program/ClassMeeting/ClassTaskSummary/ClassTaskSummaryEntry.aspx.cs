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
    public partial class ClassTaskSummaryEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.TCS_CLASS_TASK_SUMMARY;
        }

        protected int OnGetTableViewWidth()
        {
            return 1000 + (lstClassTask.Count * 90);
        }
        protected string OnGetSubjectMarkTypeNumber()
        {
            return Constant.SubjectMarkType.NUMBER;
        }
        protected string OnGetSubjectMarkTypeOption()
        {
            return Constant.SubjectMarkType.OPTION;
        }
        protected string OnGetSubjectMarkTypeText()
        {
            return Constant.SubjectMarkType.TEXT;
        }

        protected string OnGetTransactionStatusApproved()
        {
            return Constant.TransactionStatus.APPROVED;
        }

        List<ClassSubjectTask> lstClassTask = null;
        List<ClassSubjectTask> lstTheory = null;
        List<ClassSubjectTask> lstPractice = null;
        protected override void InitializeDataControl()
        {
            lstClassTask = BusinessLayer.GetClassSubjectTaskList(string.Format("ClassSubjectID = {0} AND IsDeleted = 0", AppSession.ClassSubject.ClassSubjectID));
            lstTheory = lstClassTask.Where(p => p.GCLessonType == Constant.LessonType.THEORY).ToList();
            lstPractice = lstClassTask.Where(p => p.GCLessonType == Constant.LessonType.PRACTICE).ToList();
            rptHeaderTheory.DataSource = lstTheory;
            rptHeaderTheory.DataBind();

            rptHeaderPractice.DataSource = lstPractice;
            rptHeaderPractice.DataBind();

            if (lstTheory.Count < 1)
            {
                thFinalMarkTheory.Style.Add("display", "none");
                thFinalReadonlyMarkTheory.Style.Add("display", "none");
                thMarkTheory.Style.Add("display", "none");
                thTheory.Style.Add("display", "none");
            }
            if (lstPractice.Count < 1)
            {
                thFinalMarkPractice.Style.Add("display", "none");
                thFinalReadonlyMarkPractice.Style.Add("display", "none");
                thMarkPractice.Style.Add("display", "none");
                thPractice.Style.Add("display", "none");
            }

            thTheory.ColSpan = lstTheory.Count + 2;
            thPractice.ColSpan = lstPractice.Count + 2;
            thMarkTheory.ColSpan = lstTheory.Count;
            thMarkPractice.ColSpan = lstPractice.Count;

            vClassSubject entityClassSubject = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID)).FirstOrDefault();
            hdnIsMainTeacher.Value = entityClassSubject.ParentID == 0 ? "1" : "0";
            txtPassingGrade.Text = entityClassSubject.PassingGrade.ToString();
            hdnGCSubjectMarkType.Value = entityClassSubject.GCSubjectMarkType;

            string filterExpression = "";
            if (entityClassSubject.ParentID == 0)
            {
                filterExpression = string.Format("ClassSubjectID = {0} OR ClassSubjectID IN (SELECT ClassSubjectID FROM ClassSubject WHERE ParentID = {0} AND IsDeleted = 0)", AppSession.ClassSubject.ClassSubjectID);
                hdnParentClassSubjectID.Value = entityClassSubject.ClassSubjectID.ToString();
            }
            else
            {
                filterExpression = string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID);
                hdnParentClassSubjectID.Value = entityClassSubject.ParentID.ToString();
            }
            lstStudentMark = BusinessLayer.GetvClassStudentSubjectTaskMarkList(filterExpression);

            filterExpression = string.Format("{0} AND PeriodSectionID = {1}", filterExpression, AppSession.ClassSubject.PeriodSectionID);
            lstStudentFinalMark = BusinessLayer.GetClassStudentSubjectMarkList(filterExpression);

            lstOption = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SUBJECT_MARK_OPTION));
            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", entityClassSubject.SchoolClassID));
            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();

            ClassSubjectSection entitySubjectSection = BusinessLayer.GetClassSubjectSectionList(string.Format("PeriodSectionID = {0} AND ClassSubjectID = {1}", AppSession.ClassSubject.PeriodSectionID, hdnParentClassSubjectID.Value)).FirstOrDefault();
            if (entitySubjectSection == null)
            {
                btnApprove.Style.Add("display", "none");
                btnReopen.Style.Add("display", "none");
            }
            else
            {
                hdnGCTransactionStatus.Value = entitySubjectSection.GCTransactionStatus;
                if (entitySubjectSection.GCTransactionStatus == Constant.TransactionStatus.APPROVED)
                {
                    btnApprove.Style.Add("display", "none");
                    btnSave.Style.Add("display", "none");
                }
                else
                {
                    btnReopen.Style.Add("display", "none");
                }
            }
        }

        List<vClassStudentSubjectTaskMark> lstStudentMark = null;
        List<ClassStudentSubjectMark> lstStudentFinalMark = null;
        List<StandardCode> lstOption = null;
        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassStudent entity = (vClassStudent)e.Item.DataItem;
                ClassStudentSubjectMark studentFinalMark = lstStudentFinalMark.FirstOrDefault(p => p.StudentID == entity.StudentID);
                if (studentFinalMark != null)
                {
                    TextBox txtFinalStudentMarkTheory = (TextBox)e.Item.FindControl("txtFinalStudentMarkTheory");
                    TextBox txtFinalStudentMarkPractice = (TextBox)e.Item.FindControl("txtFinalStudentMarkPractice");
                    TextBox txtAffectiveMark = (TextBox)e.Item.FindControl("txtAffectiveMark");
                    TextBox txtAffectiveDescription = (TextBox)e.Item.FindControl("txtAffectiveDescription");
                    TextBox txtProgressDescription = (TextBox)e.Item.FindControl("txtProgressDescription");
                    txtFinalStudentMarkTheory.Text = studentFinalMark.TheoryMark.ToString();
                    txtFinalStudentMarkPractice.Text = studentFinalMark.PracticeMark.ToString();
                    txtAffectiveMark.Text = studentFinalMark.AffectiveMark;
                    txtAffectiveDescription.Text = studentFinalMark.AffectiveDescription;
                    txtProgressDescription.Text = studentFinalMark.ProgressDescription;
                }

                Repeater rptStudentMarkTheory = (Repeater)e.Item.FindControl("rptStudentMarkTheory");
                rptStudentMarkTheory.DataSource = lstTheory;
                rptStudentMarkTheory.DataBind();
                Repeater rptStudentMarkPractice = (Repeater)e.Item.FindControl("rptStudentMarkPractice");
                rptStudentMarkPractice.DataSource = lstPractice;
                rptStudentMarkPractice.DataBind();

                HtmlTableCell tdTotalStudentMarkTheory = (HtmlTableCell)e.Item.FindControl("tdTotalStudentMarkTheory");
                HtmlTableCell tdFinalStudentMarkTheory = (HtmlTableCell)e.Item.FindControl("tdFinalStudentMarkTheory");

                HtmlTableCell tdTotalStudentMarkPractice = (HtmlTableCell)e.Item.FindControl("tdTotalStudentMarkPractice");
                HtmlTableCell tdFinalStudentMarkPractice = (HtmlTableCell)e.Item.FindControl("tdFinalStudentMarkPractice");

                if (lstTheory.Count < 1)
                {
                    tdTotalStudentMarkTheory.Style.Add("display", "none");
                    tdFinalStudentMarkTheory.Style.Add("display", "none");
                }
                if (lstPractice.Count < 1)
                {
                    tdTotalStudentMarkPractice.Style.Add("display", "none");
                    tdFinalStudentMarkPractice.Style.Add("display", "none");
                }
            }
        }

        private void rptStudentMarkTheory_ItemDataBound(object sender, RepeaterItemEventArgs e, string type)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ClassSubjectTask subjectTask = (ClassSubjectTask)e.Item.DataItem;
                vClassStudent student = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vClassStudent;

                TextBox txtStudentMark = (TextBox)e.Item.FindControl("txtStudentMark");
                ASPxComboBox cboStudentMarkOption = (ASPxComboBox)e.Item.FindControl("cboStudentMarkOption");
                TextBox txtStudentMarkDescription = (TextBox)e.Item.FindControl("txtStudentMarkDescription");
                vClassStudentSubjectTaskMark studentMark = lstStudentMark.FirstOrDefault(p => p.ClassSubjectTaskID == subjectTask.ClassSubjectTaskID && p.StudentID == student.StudentID);

                int parentIndex = ((RepeaterItem)e.Item.Parent.Parent).ItemIndex;
                cboStudentMarkOption.ClientInstanceName = string.Format("cboStudentMarkOption{0}{1}{2}", type, parentIndex.ToString("D2"), e.Item.ItemIndex.ToString("D2"));
                txtStudentMark.Attributes.Add("positiontag", string.Format("{0}{1}", parentIndex.ToString("D2"), e.Item.ItemIndex.ToString("D2")));
                switch (hdnGCSubjectMarkType.Value)
                {
                    case Constant.SubjectMarkType.NUMBER: cboStudentMarkOption.ClientVisible = false; txtStudentMarkDescription.Style.Add("display", "none"); break;
                    case Constant.SubjectMarkType.OPTION:
                        txtStudentMark.Style.Add("display", "none"); txtStudentMarkDescription.Style.Add("display", "none");
                        Methods.SetComboBoxField<StandardCode>(cboStudentMarkOption, lstOption, "StandardCodeName", "StandardCodeID");
                        break;
                    case Constant.SubjectMarkType.TEXT: cboStudentMarkOption.ClientVisible = false; txtStudentMark.Style.Add("display", "none"); break;
                }
                HtmlGenericControl bIsRemedial = (HtmlGenericControl)e.Item.FindControl("bIsRemedial");
                if (studentMark != null)
                {
                    txtStudentMark.Text = studentMark.Mark.ToString();
                    cboStudentMarkOption.Value = studentMark.GCOptionMark;
                    txtStudentMarkDescription.Text = studentMark.DescriptionMark;
                    if (!studentMark.IsRemedial)
                        bIsRemedial.Style.Add("display", "none");
                }
                else
                    bIsRemedial.Style.Add("display", "none");

                bIsRemedial.Attributes.Add("ClassSubjectTaskID", subjectTask.ClassSubjectTaskID.ToString());
            }
        }

        protected void rptStudentMarkTheory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            rptStudentMarkTheory_ItemDataBound(sender, e, "Theory");
        }

        protected void rptStudentMarkPractice_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            rptStudentMarkTheory_ItemDataBound(sender, e, "Practice");
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            if (type == "save")
            {
                bool result = true;
                IDbContext ctx = DbFactory.Configure(true);
                ClassSubjectSectionDao entitySubjectSectionDao = new ClassSubjectSectionDao(ctx);
                ClassSubjectTaskDao entityDtDao = new ClassSubjectTaskDao(ctx);
                ClassStudentSubjectTaskMarkDao entityStudentSubjectTaskMarkDao = new ClassStudentSubjectTaskMarkDao(ctx);
                ClassStudentSubjectMarkDao entityStudentSubjectMarkDao = new ClassStudentSubjectMarkDao(ctx);
                try
                {
                    string[] lstSaveValue = hdnListSaveHeaderValue.Value.Split('|');

                    lstClassTask = BusinessLayer.GetClassSubjectTaskList(string.Format("ClassSubjectID = {0} AND IsDeleted = 0", AppSession.ClassSubject.ClassSubjectID));
                    List<int> lstClassSubjectTaskID = new List<int>();
                    foreach (String saveValue in lstSaveValue)
                    {
                        string[] temp = saveValue.Split(',');
                        int ClassSubjectTaskID = Convert.ToInt32(temp[0]);
                        ClassSubjectTask entityDt = lstClassTask.FirstOrDefault(p => p.ClassSubjectTaskID == ClassSubjectTaskID);
                        short FinalMarkPercentage = Convert.ToInt16(temp[1]);
                        if (FinalMarkPercentage != entityDt.FinalMarkPercentage)
                        {
                            entityDt.FinalMarkPercentage = FinalMarkPercentage;
                            entityDtDao.Update(entityDt);
                        }
                        lstClassSubjectTaskID.Add(ClassSubjectTaskID);
                    }

                    ClassSubjectSection entitySubjectSection = BusinessLayer.GetClassSubjectSectionList(string.Format("PeriodSectionID = {0} AND ClassSubjectID = {1}", AppSession.ClassSubject.PeriodSectionID, hdnParentClassSubjectID.Value), ctx).FirstOrDefault();
                    if (entitySubjectSection == null)
                    {
                        entitySubjectSection = new ClassSubjectSection();
                        entitySubjectSection.ClassSubjectID = Convert.ToInt32(hdnParentClassSubjectID.Value);
                        entitySubjectSection.PeriodSectionID = AppSession.ClassSubject.PeriodSectionID;
                        entitySubjectSection.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                        entitySubjectSectionDao.Insert(entitySubjectSection);
                    }

                    List<ClassStudentSubjectTaskMark> lstStudentMark = BusinessLayer.GetClassStudentSubjectTaskMarkList(string.Format("ClassSubjectTaskID IN ({0})", string.Join(",", lstClassSubjectTaskID.Select(p => p).ToList())), ctx);
                    List<ClassStudentSubjectMark> lstStudentFinalMark = BusinessLayer.GetClassStudentSubjectMarkList(string.Format("PeriodSectionID = {0} AND ClassSubjectID = {1}", AppSession.ClassSubject.PeriodSectionID, hdnParentClassSubjectID.Value), ctx);
                    lstSaveValue = hdnListSaveValue.Value.Split('|');
                    int ClassSubjectID = Convert.ToInt32(hdnParentClassSubjectID.Value);
                    foreach (String saveValue in lstSaveValue)
                    {
                        string[] lstSaveValue1 = saveValue.Split('|');
                        foreach (String saveValue1 in lstSaveValue1)
                        {
                            string[] temp = saveValue.Split('*');
                            int studentID = Convert.ToInt32(temp[0]);
                            decimal finalStudentMarkTheory = -1;
                            if (temp[1] != "-")
                                finalStudentMarkTheory = Convert.ToDecimal(temp[1]);
                            decimal finalStudentMarkPractice = -1;
                            if (temp[1] != "-")
                                finalStudentMarkPractice = Convert.ToDecimal(temp[1]);
                            ClassStudentSubjectMark studentFinalMark = lstStudentFinalMark.FirstOrDefault(p => p.StudentID == studentID);
                            if (studentFinalMark == null)
                            {
                                if (finalStudentMarkTheory > -1 || finalStudentMarkPractice > -1)
                                {
                                    studentFinalMark = new ClassStudentSubjectMark();
                                    studentFinalMark.ClassSubjectID = ClassSubjectID;
                                    studentFinalMark.StudentID = studentID;
                                    studentFinalMark.PeriodSectionID = AppSession.ClassSubject.PeriodSectionID;
                                    studentFinalMark.TheoryMark = finalStudentMarkTheory;
                                    studentFinalMark.PracticeMark = finalStudentMarkPractice;
                                    studentFinalMark.AffectiveMark = temp[3];
                                    studentFinalMark.AffectiveDescription = temp[4];
                                    studentFinalMark.ProgressDescription = temp[5];
                                    entityStudentSubjectMarkDao.Insert(studentFinalMark);
                                }
                            }
                            else
                            {
                                //if (finalStudentMark > -1)
                                //{
                                studentFinalMark.TheoryMark = finalStudentMarkTheory;
                                studentFinalMark.PracticeMark = finalStudentMarkPractice;
                                studentFinalMark.AffectiveMark = temp[3];
                                studentFinalMark.AffectiveDescription = temp[4];
                                studentFinalMark.ProgressDescription = temp[5];
                                entityStudentSubjectMarkDao.Update(studentFinalMark);
                                //}
                                //else
                                //    entityStudentSubjectMarkDao.Delete(ClassSubjectID, studentID, AppSession.ClassSubject.PeriodSectionID);
                            }

                            string[] lstSaveValue2 = temp[6].Split(',');
                            int ctr = 0;
                            foreach (String saveValue2 in lstSaveValue2)
                            {
                                if (saveValue2 != "")
                                {
                                    int ClassSubjectTaskID = lstClassSubjectTaskID[ctr];
                                    ClassStudentSubjectTaskMark studentMark = lstStudentMark.FirstOrDefault(p => p.ClassSubjectTaskID == ClassSubjectTaskID && p.StudentID == studentID);

                                    if (hdnGCSubjectMarkType.Value == Constant.SubjectMarkType.NUMBER)
                                    {
                                        Decimal mark = -1;
                                        if (saveValue2 != "-")
                                            mark = Convert.ToDecimal(saveValue2);
                                        if (studentMark == null)
                                        {
                                            if (mark > -1)
                                            {
                                                studentMark = new ClassStudentSubjectTaskMark();
                                                studentMark.StudentID = studentID;
                                                studentMark.ClassSubjectTaskID = ClassSubjectTaskID;
                                                studentMark.Mark = mark;
                                                entityStudentSubjectTaskMarkDao.Insert(studentMark);
                                            }
                                        }
                                        else if (studentMark.Mark != mark)
                                        {
                                            if (mark > -1)
                                            {
                                                studentMark.Mark = mark;
                                                entityStudentSubjectTaskMarkDao.Update(studentMark);
                                            }
                                            else
                                                entityStudentSubjectTaskMarkDao.Delete(ClassSubjectTaskID, studentID);
                                        }
                                    }
                                    else if (hdnGCSubjectMarkType.Value == Constant.SubjectMarkType.OPTION)
                                    {
                                        if (studentMark == null)
                                        {
                                            if (saveValue2 != "")
                                            {
                                                studentMark = new ClassStudentSubjectTaskMark();
                                                studentMark.StudentID = studentID;
                                                studentMark.ClassSubjectTaskID = ClassSubjectTaskID;
                                                studentMark.GCOptionMark = saveValue2;
                                                entityStudentSubjectTaskMarkDao.Insert(studentMark);
                                            }
                                        }
                                        else if (studentMark.GCOptionMark != saveValue2)
                                        {
                                            if (saveValue2 != "")
                                            {
                                                studentMark.GCOptionMark = saveValue2;
                                                entityStudentSubjectTaskMarkDao.Update(studentMark);
                                            }
                                            else
                                                entityStudentSubjectTaskMarkDao.Delete(ClassSubjectTaskID, studentID);
                                        }
                                    }
                                    else
                                    {
                                        if (studentMark == null)
                                        {
                                            if (saveValue2 != "")
                                            {
                                                studentMark = new ClassStudentSubjectTaskMark();
                                                studentMark.StudentID = studentID;
                                                studentMark.ClassSubjectTaskID = ClassSubjectTaskID;
                                                studentMark.DescriptionMark = saveValue2;
                                                entityStudentSubjectTaskMarkDao.Insert(studentMark);
                                            }
                                        }
                                        else if (studentMark.DescriptionMark != saveValue2)
                                        {
                                            if (saveValue2 != "")
                                            {
                                                studentMark.DescriptionMark = saveValue2;
                                                entityStudentSubjectTaskMarkDao.Update(studentMark);
                                            }
                                            else
                                                entityStudentSubjectTaskMarkDao.Delete(ClassSubjectTaskID, studentID);
                                        }
                                    }
                                }
                                ctr++;
                            }
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
            else if (type == "approve")
            {
                try
                {
                    ClassSubjectSection entity = BusinessLayer.GetClassSubjectSection(Convert.ToInt32(hdnParentClassSubjectID.Value), AppSession.ClassSubject.PeriodSectionID);
                    entity.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                    BusinessLayer.UpdateClassSubjectSection(entity);
                    return true;
                }
                catch (Exception ex)
                {
                    Helper.InsertErrorLog(ex);
                    errMessage = ex.Message;
                    return false;
                }
            }
            else if (type == "reopen")
            {
                try
                {
                    ClassSubjectSection entity = BusinessLayer.GetClassSubjectSection(Convert.ToInt32(hdnParentClassSubjectID.Value), AppSession.ClassSubject.PeriodSectionID);
                    entity.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                    BusinessLayer.UpdateClassSubjectSection(entity);
                    return true;
                }
                catch (Exception ex)
                {
                    Helper.InsertErrorLog(ex);
                    errMessage = ex.Message;
                    return false;
                }
            }
            return false;
        }
    }
}