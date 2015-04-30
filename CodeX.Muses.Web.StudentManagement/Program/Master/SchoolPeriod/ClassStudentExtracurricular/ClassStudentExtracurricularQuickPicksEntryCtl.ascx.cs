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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ClassStudentExtracurricularQuickPicksEntryCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;

        public override void InitializeDataControl(string param)
        {
            hdnSchoolClassID.Value = param;

            vSchoolClass entity = BusinessLayer.GetvSchoolClassList(string.Format("SchoolClassID = {0}", hdnSchoolClassID.Value)).FirstOrDefault();
            hdnClassTypeID.Value = entity.CurriculumClassTypeID.ToString();
            BindGridView(1, true, ref PageCount);
        }

        private string GetFilterExpression()
        {
            string filterExpression = string.Format("SiteID = '{0}' AND StudentName LIKE '%{1}%' AND IsDeleted = 0 AND GCGrade + '|' + GCMajor IN (SELECT GCGrade + '|' + GCMajor FROM vClassTypeExtracurricular WHERE ExtracurricularClassTypeID = {3}) AND StudentID NOT IN (SELECT StudentID FROM ClassStudent WHERE SchoolClassID = {2})", AppSession.UserLogin.SiteID, hdnFilterItem.Value, hdnSchoolClassID.Value, hdnClassTypeID.Value);
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
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vStudent entity = e.Row.DataItem as vStudent;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (lstSelectedMember.Contains(entity.StudentID.ToString()))
                    chkIsSelected.Checked = true;
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
            bool result = false;
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            try
            {
                int SchoolClassID = Convert.ToInt32(hdnSchoolClassID.Value);
                foreach (String studentID in lstSelectedMember)
                {
                    ClassStudent entity = new ClassStudent();
                    entity.SchoolClassID = SchoolClassID;
                    entity.StudentID = Convert.ToInt32(studentID);
                    entity.GCClassStudentStatus = Constant.ClassStudentStatus.OPEN;
                    entityDao.Insert(entity);
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