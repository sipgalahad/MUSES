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
    public partial class CurriculumMeetingPlanEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.CR_CURRICULUM_MEETING_PLAN;
        }

        protected string OnGetParentFilterExpression()
        {
            return string.Format("CurriculumID = {0} AND IsHeader = 1 AND IsDeleted = 0", AppSession.CurriculumID);
        }

        protected string OnGetReferenceFilterExpression()
        {
            return string.Format("CurriculumID = {0} AND IsDeleted = 0", AppSession.CurriculumID);
        }

        protected override void InitializeDataControl()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.CURRICULUM_MEETING_PLAN_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboCurriculumMeetingPlanType, lstSc, "StandardCodeName", "StandardCodeID");

            BindGridView();

            Helper.SetControlEntrySetting(txtCurriculumMeetingPlanName, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtDisplayOrder, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboCurriculumMeetingPlanType, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = string.Format("CurriculumID = {0} AND IsDeleted = 0", AppSession.CurriculumID);
            grdView.DataSource = BusinessLayer.GetvCurriculumMeetingPlanList(filterExpression);
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

        private void ControlToEntity(CurriculumMeetingPlan entity)
        {
            entity.CurriculumMeetingPlanName = txtCurriculumMeetingPlanName.Text;
            entity.GCCurriculumMeetingPlanType = cboCurriculumMeetingPlanType.Value.ToString();
            if (tacParent.Value != "0" && tacParent.Value != "")
                entity.ParentID = Convert.ToInt32(tacParent.Value);
            else
                entity.ParentID = null;
            if (tacReference.Value != "0" && tacReference.Value != "")
                entity.CurriculumSyllabusReferenceID = Convert.ToInt32(tacReference.Value);
            else
                entity.CurriculumSyllabusReferenceID = null;
            entity.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
            entity.IsHeader = chkIsHeader.Checked;
            entity.IsUsingCode = chkIsUsingCode.Checked;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            CurriculumMeetingPlanDao entityDao = new CurriculumMeetingPlanDao(ctx);
            try
            {
                CurriculumMeetingPlan entity = new CurriculumMeetingPlan();
                ControlToEntity(entity);
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
            CurriculumMeetingPlanDao entityDao = new CurriculumMeetingPlanDao(ctx);
            try
            {
                CurriculumMeetingPlan entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
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
                CurriculumMeetingPlan entity = BusinessLayer.GetCurriculumMeetingPlan(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateCurriculumMeetingPlan(entity);
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