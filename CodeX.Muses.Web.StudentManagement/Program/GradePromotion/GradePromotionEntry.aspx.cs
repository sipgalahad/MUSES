using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;
using System.Web.UI.HtmlControls;
using CodeX.Data.Core.Dal;
using DevExpress.Web.ASPxEditors;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class GradePromotionEntry : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.GRADE_PROMOTION;
        }

        protected string OnGetClassStudyTypeRegular()
        {
            return Constant.ClassStudyType.REGULAR;
        }

        protected string OnGetPeriodSectionFilterExpression()
        {
            return string.Format("GCPeriodSectionStatus != '{0}'", Constant.SchoolPeriodStatus.VOID);
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<SchoolPeriod> lstSchoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SiteID = '{0}' AND GCSchoolPeriodStatus != '{1}'", AppSession.UserLogin.SiteID, Constant.SchoolPeriodStatus.VOID));
            Methods.SetComboBoxField<SchoolPeriod>(cboSchoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
            SchoolPeriod selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);
            if (selectedSchoolPeriod == null)
                cboSchoolPeriod.SelectedIndex = 0;
            else
                cboSchoolPeriod.Value = selectedSchoolPeriod.SchoolPeriodID.ToString();

            List<PeriodSection> lstPeriodSection = BusinessLayer.GetPeriodSectionList(string.Format("'{0}' BETWEEN StartDate AND EndDate", DateTime.Now.ToString("yyyyMMdd")));
            if (lstPeriodSection.Count > 0)
            {
                PeriodSection periodSection = lstPeriodSection.FirstOrDefault();
                tacPeriodSection.Value = periodSection.PeriodSectionID.ToString();
                tacPeriodSection.Text = periodSection.PeriodSectionName;
            }

            SchoolPeriod nextSchoolPeriod = lstSchoolPeriod.FirstOrDefault(p => p.StartDate <= DateTime.Now.AddYears(1) && p.EndDate >= DateTime.Now.AddYears(1));
            if (nextSchoolPeriod != null)
                hdnNextSchoolPeriod.Value = nextSchoolPeriod.SchoolPeriodID.ToString();
            else
                hdnNextSchoolPeriod.Value = cboSchoolPeriod.Value.ToString();

            BindGridView();
        }

        private string GetFilterExpression()
        {
            if (tacSchoolClass.Value == "")
                return "1 = 0";
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("SchoolClassID = {0}", tacSchoolClass.Value);
            return filterExpression;
        }

        List<ClassStudentMark> lstStudentMark = null;
        List<vPeriodClassType> lstPeriodClassType = null;
        private void BindGridView()
        {
            string filterExpression = GetFilterExpression();
            if (tacSchoolClass.Value != "")
                lstStudentMark = BusinessLayer.GetClassStudentMarkList(string.Format("SchoolClassID = {0} AND PeriodSectionID = {1}", tacSchoolClass.Value, tacPeriodSection.Value));

            lstPeriodClassType = BusinessLayer.GetvPeriodClassTypeList(string.Format("SchoolPeriodID = {0} AND GCGrade = '{1}' AND IsDeleted = 0", hdnNextSchoolPeriod.Value, hdnNextGCGrade.Value));

            List<vClassStudent> lstEntity = BusinessLayer.GetvClassStudentList(String.Format("{0} AND GCClassStudentStatus = '{1}'", filterExpression, Constant.ClassStudentStatus.OPEN));
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vClassStudent entity = (vClassStudent)e.Row.DataItem;
                ClassStudentMark studentMark = lstStudentMark.FirstOrDefault(p => p.StudentID == entity.StudentID);
                if (studentMark != null)
                {
                    HtmlGenericControl lblFinalMark = (HtmlGenericControl)e.Row.FindControl("lblFinalMark");
                    lblFinalMark.InnerHtml = studentMark.FinalMark.ToString();
                }

                HtmlGenericControl divNextGrade = (HtmlGenericControl)e.Row.FindControl("divNextGrade");
                divNextGrade.InnerHtml = hdnNextGrade.Value;

                ASPxComboBox cboGCMajor = (ASPxComboBox)e.Row.FindControl("cboGCMajor");
                cboGCMajor.ClientInstanceName = string.Format("cboGCMajor{0}", e.Row.DataItemIndex);
                Methods.SetComboBoxField<vPeriodClassType>(cboGCMajor, lstPeriodClassType, "Major", "GCMajor");

                if (hdnGCMajor.Value != "")
                {
                    cboGCMajor.Value = hdnGCMajor.Value;
                    cboGCMajor.ClientEnabled = false;
                }
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "promote")
            {
                if (OnPromoteEntity(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else 
            {
                if (OnRejectEntity(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool OnPromoteEntity(ref string errMessage) 
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            StudentDao studentDao = new StudentDao(ctx);
            ClassStudentDao classStudentDao = new ClassStudentDao(ctx);
            try
            {
                List<Student> lstStudent = BusinessLayer.GetStudentList(String.Format("StudentID IN ({0})", hdnLstStudentID.Value), ctx);
                List<ClassStudent> lstClassStudent = BusinessLayer.GetClassStudentList(String.Format("SchoolClassID = {0} AND StudentID IN ({1})", tacSchoolClass.Value, hdnLstStudentID.Value), ctx);
                
                string[] lstSaveValue = hdnSelectedValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue) 
                {
                    string[] temp = saveValue.Split(';');

                    Student entity = lstStudent.FirstOrDefault(p => p.StudentID == Convert.ToInt32(temp[0]));
                    entity.GCGrade = hdnNextGCGrade.Value;
                    entity.GCMajor = temp[1];

                    ClassStudent csEntity = lstClassStudent.FirstOrDefault(p => p.StudentID == entity.StudentID);
                    csEntity.GCClassStudentStatus = Constant.ClassStudentStatus.NAIK_KELAS;
                    classStudentDao.Update(csEntity);
                    studentDao.Update(entity);
                }
                ctx.CommitTransaction();
            }
            catch(Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                ctx.RollBackTransaction();
                result = false;
            }
            finally 
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnRejectEntity(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ClassStudentDao classStudentDao = new ClassStudentDao(ctx);
            try
            {
                List<ClassStudent> lstClassStudent = BusinessLayer.GetClassStudentList(String.Format("SchoolClassID = {0} AND StudentID IN ({1})", tacSchoolClass.Value, hdnLstStudentID.Value), ctx);

                foreach (ClassStudent entity in lstClassStudent)
                {
                    entity.GCClassStudentStatus = Constant.ClassStudentStatus.TIDAK_NAIK_KELAS;
                    classStudentDao.Update(entity);
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                ctx.RollBackTransaction();
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
    }
}