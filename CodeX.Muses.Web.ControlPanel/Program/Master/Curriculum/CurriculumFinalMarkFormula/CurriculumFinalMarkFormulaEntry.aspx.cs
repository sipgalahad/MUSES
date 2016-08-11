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
    public partial class CurriculumFinalMarkFormulaEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.CR_CURRICULUM_FINAL_MARK_FORMULA;
        }

        protected string OnGetFinalMarkSourceIndicator()
        {
            return Constant.FinalMarkSource.INDICATOR;
        }

        protected override void InitializeDataControl()
        {
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.FINAL_MARK_SOURCE, Constant.StandardCode.FINAL_MARK_SUMMARY_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboFinalMarkSource, lstStandardCode.Where(p => p.ParentID == Constant.StandardCode.FINAL_MARK_SOURCE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboSummaryType, lstStandardCode.Where(p => p.ParentID == Constant.StandardCode.FINAL_MARK_SUMMARY_TYPE).ToList(), "StandardCodeName", "StandardCodeID");

            List<CurriculumMarkType> lstMarkType = BusinessLayer.GetCurriculumMarkTypeList(string.Format("CurriculumID = {0} AND IsDeleted = 0", AppSession.CurriculumID));
            Methods.SetComboBoxField<CurriculumMarkType>(cboMarkType, lstMarkType, "CurriculumMarkTypeName", "CurriculumMarkTypeID");
            cboMarkType.SelectedIndex = 0;

            BindGridView();

            Helper.SetControlEntrySetting(txtCurriculumFinalMarkFormulaCode, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtCurriculumFinalMarkFormulaName, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboFinalMarkSource, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboSummaryType, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = string.Format("CurriculumMarkTypeID = {0} AND IsDeleted = 0", cboMarkType.Value);
            grdView.DataSource = BusinessLayer.GetCurriculumFinalMarkFormulaHdList(filterExpression);
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

        private void ControlToEntity(CurriculumFinalMarkFormulaHd entity)
        {
            entity.CurriculumFinalMarkFormulaCode = txtCurriculumFinalMarkFormulaCode.Text;
            entity.CurriculumFinalMarkFormulaName = txtCurriculumFinalMarkFormulaName.Text;
            entity.GCFinalMarkSource = cboFinalMarkSource.Value.ToString();
            entity.GCSummaryType = cboSummaryType.Value.ToString();
            entity.IsTotalAverageAllMark = chkIsTotalAverageAllMark.Checked;
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            CurriculumFinalMarkFormulaHdDao entityDao = new CurriculumFinalMarkFormulaHdDao(ctx);
            try
            {
                CurriculumFinalMarkFormulaHd entity = new CurriculumFinalMarkFormulaHd();
                ControlToEntity(entity);
                entity.CurriculumMarkTypeID = Convert.ToInt32(cboMarkType.Value);
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
            CurriculumFinalMarkFormulaHdDao entityDao = new CurriculumFinalMarkFormulaHdDao(ctx);
            try
            {
                CurriculumFinalMarkFormulaHd entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
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
                CurriculumFinalMarkFormulaHd entity = BusinessLayer.GetCurriculumFinalMarkFormulaHd(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateCurriculumFinalMarkFormulaHd(entity);
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