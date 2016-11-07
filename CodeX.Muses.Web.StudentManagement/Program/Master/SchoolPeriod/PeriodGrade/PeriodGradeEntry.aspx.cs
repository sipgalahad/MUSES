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
using DevExpress.Web.ASPxEditors;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class PeriodGradeEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SP_SCHOOL_PERIOD_GRADE;
        }
        protected override void InitializeDataControl()
        {
            SchoolPeriod entitySchoolPeriod = BusinessLayer.GetSchoolPeriod(AppSession.SchoolPeriodID);
            hdnGCSchoolType.Value = BusinessLayer.GetSiteParameter(entitySchoolPeriod.SiteID, Constant.SiteParameter.SCHOOL_TYPE).ParameterValue;
            List<Curriculum> lstCurriculum = BusinessLayer.GetCurriculumList(string.Format("GCSchoolType = '{0}' AND IsDeleted = 0", hdnGCSchoolType.Value));
            Methods.SetComboBoxField<Curriculum>(cboCurriculum, lstCurriculum, "CurriculumName", "CurriculumID");

            BindGridView();

            Helper.SetControlEntrySetting(cboCurriculum, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            List<vSchoolGrade> lstGrade = BusinessLayer.GetvSchoolGradeList(string.Format("GCSchoolType = '{0}' ORDER BY DisplayOrder", hdnGCSchoolType.Value));
            lstEntity = BusinessLayer.GetvPeriodGradeList(string.Format("SchoolPeriodID = {0}", AppSession.SchoolPeriodID));
            grdView.DataSource = lstGrade;
            grdView.DataBind();
        }

        List<vPeriodGrade> lstEntity = null;
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vSchoolGrade entity = (vSchoolGrade)e.Row.DataItem;
                HtmlGenericControl divCurriculum = (HtmlGenericControl)e.Row.FindControl("divCurriculum");
                HtmlInputHidden hdnCurriculumID = (HtmlInputHidden)e.Row.FindControl("hdnCurriculumID");
                HtmlInputHidden hdnCurriculumName = (HtmlInputHidden)e.Row.FindControl("hdnCurriculumName");
                vPeriodGrade entityCurriculum = lstEntity.FirstOrDefault(p => p.GCGrade == entity.GCGrade);
                if (entityCurriculum != null)
                {
                    divCurriculum.InnerHtml = entityCurriculum.CurriculumName;
                    hdnCurriculumName.Value = entityCurriculum.CurriculumName;
                    hdnCurriculumID.Value = entityCurriculum.CurriculumID.ToString();
                }
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (OnSaveRecordEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else if (param[0] == "delete")
            {
                if (OnDeleteEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool OnSaveRecordEntityDt(ref string errMessage)
        {
            try
            {
                PeriodGrade entity = BusinessLayer.GetPeriodGrade(AppSession.SchoolPeriodID, hdnGCGrade.Value);
                if (entity == null)
                {
                    entity = new PeriodGrade();
                    entity.CurriculumID = Convert.ToInt32(cboCurriculum.Value);
                    entity.SchoolPeriodID = AppSession.SchoolPeriodID;
                    entity.GCGrade = hdnGCGrade.Value;
                    BusinessLayer.InsertPeriodGrade(entity);
                }
                else
                {
                    entity.CurriculumID = Convert.ToInt32(cboCurriculum.Value);
                    BusinessLayer.UpdatePeriodGrade(entity);
                }
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                BusinessLayer.DeletePeriodGrade(AppSession.SchoolPeriodID, hdnGCGrade.Value);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion
    }
}