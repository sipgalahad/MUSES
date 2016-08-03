using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common;
using CodeX.Common;
using CodeX.Data.Core.Dal;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ClassStudentQuickPicksEntryCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;

        public override void InitializeDataControl(string param)
        {
            hdnSchoolClassID.Value = param;

            vSchoolClass entity = BusinessLayer.GetvSchoolClassList(string.Format("SchoolClassID = {0}", hdnSchoolClassID.Value)).FirstOrDefault();
            hdnGCGrade.Value = entity.GCGrade;
            hdnGCMajor.Value = entity.GCMajor;
            hdnSiteID.Value = BusinessLayer.GetSchoolPeriod(AppSession.SchoolPeriodID).SiteID;
            BindGridView(1, true, ref PageCount);
        }

        private string GetFilterExpression()
        {
            string filterExpression  ="";
            if (hdnGCMajor.Value != "")
                filterExpression = string.Format("SiteID = '{0}' AND StudentName LIKE '%{1}%' AND IsDeleted = 0 AND GCGrade = '{3}' AND GCMajor = '{4}' AND StudentID NOT IN (SELECT StudentID FROM ClassStudent WHERE SchoolClassID = {2})", hdnSiteID.Value, hdnFilterItem.Value, hdnSchoolClassID.Value, hdnGCGrade.Value, hdnGCMajor.Value);
            else
                filterExpression = string.Format("SiteID = '{0}' AND StudentName LIKE '%{1}%' AND IsDeleted = 0 AND GCGrade = '{3}' AND GCMajor IS NULL AND StudentID NOT IN (SELECT StudentID FROM ClassStudent WHERE SchoolClassID = {2})", hdnSiteID.Value, hdnFilterItem.Value, hdnSchoolClassID.Value, hdnGCGrade.Value);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvStudentRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 10);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<vStudent> lstEntity = BusinessLayer.GetvStudentList(filterExpression, 10, pageIndex, "StudentName ASC");
            string lstStudentID = string.Join(",", lstEntity.Select(p => p.StudentID).ToList());
            if (lstStudentID != "")
                lstClassStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolPeriodID = {0} AND StudentID IN ({1})", AppSession.SchoolPeriodID, lstStudentID));
            else
                lstClassStudent = new List<vClassStudent>();
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        List<vClassStudent> lstClassStudent = null;
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vStudent entity = e.Row.DataItem as vStudent;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                HtmlGenericControl divSchoolClassName = e.Row.FindControl("divSchoolClassName") as HtmlGenericControl;
                if (lstSelectedMember.Contains(entity.StudentID.ToString()))
                    chkIsSelected.Checked = true;

                vClassStudent classStudent = lstClassStudent.FirstOrDefault(p => p.StudentID == entity.StudentID);
                if (classStudent != null)
                    divSchoolClassName.InnerHtml = classStudent.SchoolClassName;
            }
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            ClassStudentDao entityDao = new ClassStudentDao(ctx);
            StudentDao entityStudentDao = new StudentDao(ctx);
            bool result = false;
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            try
            {
                int SchoolClassID = Convert.ToInt32(hdnSchoolClassID.Value);

                if (hdnSelectedMember.Value != "")
                {
                    List<ClassStudent> lstClassStudent = BusinessLayer.GetClassStudentList(string.Format("SchoolClassID IN (SELECT SchoolClassID FROM vSchoolClass WHERE SchoolPeriodID = {0}) AND StudentID IN ({1})", AppSession.SchoolPeriodID, hdnSelectedMember.Value), ctx);
                    foreach (ClassStudent classStudent in lstClassStudent)
                    {
                        entityDao.Delete(classStudent.SchoolClassID, classStudent.StudentID);
                    }

                    foreach (String studentID in lstSelectedMember)
                    {
                        ClassStudent entity = new ClassStudent();
                        entity.SchoolClassID = SchoolClassID;
                        entity.StudentID = Convert.ToInt32(studentID);
                        entity.GCClassStudentStatus = Constant.ClassStudentStatus.OPEN;
                        entityDao.Insert(entity);

                        Student student = entityStudentDao.Get(entity.StudentID);
                        student.SchoolClassID = entity.SchoolClassID;
                        student.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityStudentDao.Update(student);
                    }
                }
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
    }
}