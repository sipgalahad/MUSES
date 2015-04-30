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

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class CurriculumClassTypeEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            if (hdnGCClassStudyType.Value == Constant.ClassStudyType.EXTRACURRICULAR)
                return Constant.MenuCode.ControlPanel.CR_CURRICULUM_EXTRACURRICULAR_CLASS_TYPE;
            return Constant.MenuCode.ControlPanel.CR_CURRICULUM_CLASS_TYPE;
        }
        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                if (Page.Request.QueryString["id"] == "ex")
                    hdnGCClassStudyType.Value = Constant.ClassStudyType.EXTRACURRICULAR;
                else
                    hdnGCClassStudyType.Value = Constant.ClassStudyType.REGULAR;
            }
            else
                hdnGCClassStudyType.Value = Constant.ClassStudyType.REGULAR;
            if (hdnGCClassStudyType.Value == Constant.ClassStudyType.EXTRACURRICULAR)
            {
                trGrade.Style.Add("display", "none");
                trMajor.Style.Add("display", "none");
            }
            List<vSchoolGrade> lstGrade = BusinessLayer.GetvSchoolGradeList(string.Format("SiteID = '{0}' ORDER BY DisplayOrder", AppSession.UserLogin.SiteID));
            List<CurriculumMajor> lstMajor = BusinessLayer.GetCurriculumMajorList(string.Format("CurriculumID = {0} AND IsDeleted = 0", AppSession.CurriculumID));
            lstMajor.Insert(0, new CurriculumMajor { CurriculumMajorID = 0, CurriculumMajorName = "" });
            Methods.SetComboBoxField<vSchoolGrade>(cboGrade, lstGrade, "Grade", "GCGrade");
            Methods.SetComboBoxField<CurriculumMajor>(cboMajor, lstMajor, "CurriculumMajorName", "CurriculumMajorID");

            BindGridView();

            Helper.SetControlEntrySetting(txtCurriculumClassTypeCode, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtCurriculumClassTypeName, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboGrade, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = string.Format("CurriculumID = {0} AND GCClassStudyType = '{1}' AND IsDeleted = 0", AppSession.CurriculumID, hdnGCClassStudyType.Value);
            grdView.DataSource = BusinessLayer.GetvCurriculumClassTypeList(filterExpression);
            grdView.DataBind();
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
                if (hdnEntryID.Value.ToString() != "")
                {
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
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

        private void ControlToEntity(CurriculumClassType entity)
        {
            entity.CurriculumClassTypeCode = txtCurriculumClassTypeCode.Text;
            entity.CurriculumClassTypeName = txtCurriculumClassTypeName.Text;
            if (hdnGCClassStudyType.Value == Constant.ClassStudyType.EXTRACURRICULAR)
            {
                entity.GCGrade = null;
                entity.CurriculumMajorID = null;
            }
            else
            {
                entity.GCGrade = cboGrade.Value.ToString();
                if (cboMajor.Value == null || cboMajor.Value.ToString() == "0")
                    entity.CurriculumMajorID = null;
                else
                    entity.CurriculumMajorID = Convert.ToInt32(cboMajor.Value);
            }
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            CurriculumClassTypeDao entityDao = new CurriculumClassTypeDao(ctx);
            try
            {
                CurriculumClassType entity = new CurriculumClassType();
                ControlToEntity(entity);
                entity.GCClassStudyType = hdnGCClassStudyType.Value;
                entity.CurriculumID = AppSession.CurriculumID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
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

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            CurriculumClassTypeDao entityDao = new CurriculumClassTypeDao(ctx);
            try
            {
                CurriculumClassType entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);
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

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                CurriculumClassType entity = BusinessLayer.GetCurriculumClassType(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateCurriculumClassType(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion
    }
}