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

namespace CodeX.Muses.Web.TeacherPage.Program
{
    public partial class ClassTaskEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 5;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.TeacherPage.WS_CLASS_TASK;
        }
        protected override void InitializeDataControl()
        {
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvClassSubjectTaskRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, RowCountPerPage);
            }

            List<vClassSubjectTask> lstEntity = BusinessLayer.GetvClassSubjectTaskList(filterExpression, RowCountPerPage, pageIndex, "TaskDate DESC");
            rptMeetingView.DataSource = lstEntity;
            rptMeetingView.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount, ref rowCount);
                    result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        protected void cbpMeetingDetail_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string filterExpression = "1 = 0";
            if (hdnClassSubjectTaskID.Value != "")
                filterExpression = string.Format("ClassSubjectTaskID = {0}", hdnClassSubjectTaskID.Value);
            lstStudentMark = BusinessLayer.GetClassStudentSubjectMarkList(filterExpression);

            ClassSubject classSubject = BusinessLayer.GetClassSubject(AppSession.ClassSubject.ClassSubjectID);
            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", classSubject.SchoolClassID));
            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();
        }

        List<ClassStudentSubjectMark> lstStudentMark = null;
        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassStudent entity = (vClassStudent)e.Item.DataItem;
                ClassStudentSubjectMark studentMark = lstStudentMark.FirstOrDefault(p => p.StudentID == entity.StudentID);
                if (studentMark != null)
                {
                    TextBox txtStudentMark = (TextBox)e.Item.FindControl("txtStudentMark");
                    txtStudentMark.Text = studentMark.Mark.ToString();
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
            ClassStudentSubjectMarkDao entityDtDao = new ClassStudentSubjectMarkDao(ctx);
            try
            {
                string[] lstSaveValue = hdnListSaveValue.Value.Split('|');

                List<ClassStudentSubjectMark> lstStudentMark = BusinessLayer.GetClassStudentSubjectMarkList(string.Format("ClassSubjectTaskID = {0}", hdnClassSubjectTaskID.Value), ctx);
                foreach (String saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(',');
                    int studentID = Convert.ToInt32(temp[0]);
                    ClassStudentSubjectMark entityDt = lstStudentMark.FirstOrDefault(p => p.StudentID == studentID);
                    if (temp[1] != "")
                    {
                        Decimal mark = Convert.ToDecimal(temp[1]);
                        if (entityDt == null)
                        {
                            entityDt = new ClassStudentSubjectMark();
                            entityDt.ClassSubjectTaskID = Convert.ToInt32(hdnClassSubjectTaskID.Value);
                            entityDt.StudentID = studentID;
                            entityDt.Mark = mark;
                            entityDtDao.Insert(entityDt);
                        }
                        else
                        {
                            entityDt.Mark = mark;
                            entityDtDao.Update(entityDt);
                        }
                    }
                    else if(entityDt != null)
                    {
                        entityDtDao.Delete(entityDt.ClassSubjectTaskID, entityDt.StudentID);
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