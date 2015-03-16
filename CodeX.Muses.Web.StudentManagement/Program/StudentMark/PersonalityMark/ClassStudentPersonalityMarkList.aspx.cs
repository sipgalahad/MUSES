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

        protected override void InitializeDataControl()
        {
            List<vClassSubject> lstSubject = BusinessLayer.GetvClassSubjectList(string.Format("SchoolClassID = {0} AND SubjectGCClassStudyType = '{1}' AND ParentID IS NULL", AppSession.ClassStudent.SchoolClassID, Constant.ClassStudyType.PERSONALITY));

            string lstClassSubjectID = string.Join(",", lstSubject.Select(p => p.ClassSubjectID).ToList());
            lstMark = BusinessLayer.GetClassStudentSubjectMarkList(String.Format("ClassSubjectID IN ({0}) AND StudentID = {1} AND PeriodSectionID = {2}", lstClassSubjectID, AppSession.ClassStudent.StudentID, AppSession.ClassStudent.PeriodSectionID));
            grdView.DataSource = lstSubject;
            grdView.DataBind();
        }

        List<ClassStudentSubjectMark> lstMark = null;
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vClassSubject entity = (vClassSubject)e.Row.DataItem;
                ClassStudentSubjectMark studentMark = lstMark.FirstOrDefault(p => p.ClassSubjectID == entity.ClassSubjectID);
                TextBox txtMarkDescription = (TextBox)e.Row.FindControl("txtMarkDescription");
                if (studentMark != null)
                    txtMarkDescription.Text = studentMark.AffectiveDescription;
            }
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
                            entity.AffectiveDescription = temp[1];
                            entityDao.Insert(entity);
                        }
                        else
                        {
                            entity.AffectiveDescription = temp[1];
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