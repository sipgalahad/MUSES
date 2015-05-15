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
            return Constant.MenuCode.ControlPanel.CR_CURRICULUM_CLASS_TYPE;
        }
        protected override void InitializeDataControl()
        {
            hdnGCClassStudyType.Value = Constant.ClassStudyType.REGULAR;
            Curriculum entityCurriculum = BusinessLayer.GetCurriculum(AppSession.CurriculumID);
            List<vSchoolGrade> lstGrade = BusinessLayer.GetvSchoolGradeList(string.Format("GCSchoolType = '{0}' ORDER BY DisplayOrder", entityCurriculum.GCSchoolType));
            List<vCurriculumMajor> lstMajor = BusinessLayer.GetvCurriculumMajorList(string.Format("CurriculumID = {0} AND IsDeleted = 0", AppSession.CurriculumID));
            lstMajor.Insert(0, new vCurriculumMajor { CurriculumMajorID = 0, CurriculumMajorName = "" });
            Methods.SetComboBoxField<vSchoolGrade>(cboGrade, lstGrade, "Grade", "GCGrade");
            Methods.SetComboBoxField<vCurriculumMajor>(cboMajor, lstMajor, "CurriculumMajorName", "CurriculumMajorID");

            List<ClassType> lstClassType = BusinessLayer.GetClassTypeList(string.Format("GCSchoolType = '{0}' AND GCClassStudyType = '{1}' AND IsDeleted = 0 ORDER BY ClassTypeCode", entityCurriculum.GCSchoolType, Constant.ClassStudyType.REGULAR));
            Methods.SetComboBoxField<ClassType>(cboClassType, lstClassType, "ClassTypeName", "ClassTypeID");

            hdnLstCurriculumMajor.Value = string.Join("|", lstMajor.Select(p => string.Format("{0};{1}", p.CurriculumMajorID, p.GCMajor)));
            hdnLstClassType.Value = string.Join("|", lstClassType.Select(p => string.Format("{0};{1};{2}", p.ClassTypeID, p.GCGrade, p.GCMajor)));

            BindGridView();

            Helper.SetControlEntrySetting(txtCurriculumClassTypeCode, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtCurriculumClassTypeName, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboClassType, new ControlEntrySetting(true, true, true), "mpTrx");
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
            entity.ClassTypeID = Convert.ToInt32(cboClassType.Value);
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