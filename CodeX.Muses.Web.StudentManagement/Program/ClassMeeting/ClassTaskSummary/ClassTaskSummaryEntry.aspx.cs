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
    public partial class ClassTaskSummaryEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.TCS_CLASS_TASK_SUMMARY;
        }

        protected int OnGetTableViewWidth()
        {
            return 480 + (lstClassTask.Count * 90);
        }

        List<ClassSubjectTask> lstClassTask = null;
        protected override void InitializeDataControl()
        {
            lstClassTask = BusinessLayer.GetClassSubjectTaskList(string.Format("ClassSubjectID = {0} AND IsDeleted = 0", AppSession.ClassSubject.ClassSubjectID));
            rptHeader.DataSource = lstClassTask;
            rptHeader.DataBind();

            thMark.ColSpan = lstClassTask.Count;

            lstStudentMark = BusinessLayer.GetvClassStudentSubjectMarkList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID));

            ClassSubject classSubject = BusinessLayer.GetClassSubject(AppSession.ClassSubject.ClassSubjectID);
            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", classSubject.SchoolClassID));
            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();
        }

        List<vClassStudentSubjectMark> lstStudentMark = null;
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
                ClassSubjectTask subjectTask = (ClassSubjectTask)e.Item.DataItem;
                vClassStudent student = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vClassStudent;

                vClassStudentSubjectMark entity = lstStudentMark.FirstOrDefault(p => p.ClassSubjectTaskID == subjectTask.ClassSubjectTaskID && p.StudentID == student.StudentID);
                if (entity != null)
                {
                    TextBox txtStudentMark = (TextBox)e.Item.FindControl("txtStudentMark");
                    txtStudentMark.Text = entity.Mark.ToString();
                }
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ClassSubjectTaskDao entityDtDao = new ClassSubjectTaskDao(ctx);
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

                List<ClassStudentSubjectMark> lstStudentMark = BusinessLayer.GetClassStudentSubjectMarkList(string.Format("ClassSubjectTaskID IN ({0})", string.Join(",", lstClassSubjectTaskID.Select(p => p).ToList())), ctx);
                lstSaveValue = hdnListSaveValue.Value.Split('|');
                foreach (String saveValue in lstSaveValue)
                {
                    string[] lstSaveValue1 = saveValue.Split('|');
                    foreach (String saveValue1 in lstSaveValue1)
                    {
                        string[] temp = saveValue.Split('^');
                        int studentID = Convert.ToInt32(temp[0]);
                        string[] lstSaveValue2 = temp[1].Split(',');
                        int ctr = 0;
                        foreach (String saveValue2 in lstSaveValue2)
                        {
                            if (saveValue2 != "")
                            {
                                int ClassSubjectTaskID = lstClassSubjectTaskID[ctr];
                                ClassStudentSubjectMark studentMark = lstStudentMark.FirstOrDefault(p => p.ClassSubjectTaskID == ClassSubjectTaskID && p.StudentID == studentID);
                                
                                Decimal mark = Convert.ToDecimal(saveValue2);
                                if (studentMark == null)
                                {
                                    studentMark = new ClassStudentSubjectMark();
                                    studentMark.StudentID = studentID;
                                    studentMark.ClassSubjectTaskID = ClassSubjectTaskID;
                                    studentMark.Mark = mark;
                                    entityStudentSubjectMarkDao.Insert(studentMark);
                                }
                                else if (studentMark.Mark != mark)
                                {
                                    studentMark.Mark = mark;
                                    entityStudentSubjectMarkDao.Update(studentMark);
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
    }
}