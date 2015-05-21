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

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class GradePromotionFormulaDtEntryCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            String GCSchoolType = BusinessLayer.GetSiteParameter(AppSession.UserLogin.SiteID, Constant.SiteParameter.SCHOOL_TYPE).ParameterValue;

            GradePromotionFormulaHd entity = BusinessLayer.GetGradePromotionFormulaHd(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.GradePromotionFormulaCode, entity.GradePromotionFormulaName);

            List<CurriculumSchoolPeriodSection> lstPeriodSection = BusinessLayer.GetCurriculumSchoolPeriodSectionList(string.Format("CurriculumID = {0} AND IsDeleted = 0", AppSession.CurriculumID));
            Methods.SetComboBoxField<CurriculumSchoolPeriodSection>(cboCurriculumSchoolPeriodSection, lstPeriodSection, "CurriculumSchoolPeriodSectionName", "CurriculumSchoolPeriodSectionID");

            List<vSchoolGrade> lstGrade = BusinessLayer.GetvSchoolGradeList(string.Format("GCSchoolType = '{0}'", GCSchoolType));
            lstGrade.Insert(0, new vSchoolGrade { GCGrade = "", Grade = "" });
            Methods.SetComboBoxField<vSchoolGrade>(cboGCGrade, lstGrade, "Grade", "GCGrade");

            BindGridView();

            Helper.SetControlEntrySetting(txtDisplayOrder, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(cboGCGrade, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(cboCurriculumSchoolPeriodSection, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtGradePromotionFormulaDtName, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetvGradePromotionFormulaDtList(string.Format("GradePromotionFormulaID = {0} ORDER BY DisplayOrder ASC", hdnID.Value));
            grdView.DataBind();
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        #region Process Detail
        protected void cbpProcessPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        private void ControlToEntity(GradePromotionFormulaDt entity)
        {
            entity.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
            entity.IsCurrentGrade = chkIsCurrentGrade.Checked;
            if (cboGCGrade.Value != null && cboGCGrade.Value.ToString() != "")
                entity.GCGrade = cboGCGrade.Value.ToString();
            else
                entity.GCGrade = null;
            entity.CurriculumSchoolPeriodSectionID = Convert.ToInt32(cboCurriculumSchoolPeriodSection.Value);
            entity.FinalMarkPercentage = Convert.ToDecimal(txtFinalMarkPercentage.Text);
            entity.GradePromotionFormulaDtName = txtGradePromotionFormulaDtName.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            GradePromotionFormulaDtDao entityDao = new GradePromotionFormulaDtDao(ctx);
            try
            {
                GradePromotionFormulaDt entity = new GradePromotionFormulaDt();
                ControlToEntity(entity);
                entity.GradePromotionFormulaID = Convert.ToInt32(hdnID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
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
            GradePromotionFormulaDtDao entityDao = new GradePromotionFormulaDtDao(ctx);
            try
            {
                GradePromotionFormulaDt entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
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
                GradePromotionFormulaDt entity = BusinessLayer.GetGradePromotionFormulaDt(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateGradePromotionFormulaDt(entity);
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