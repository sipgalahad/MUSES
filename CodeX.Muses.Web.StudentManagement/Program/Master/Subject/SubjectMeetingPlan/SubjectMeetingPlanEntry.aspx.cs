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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class SubjectMeetingPlanEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            if (AppSession.SubjectMatterID > 0)
                return Constant.MenuCode.StudentManagement.SBM_SUBJECT_MEETING_PLAN;
            return Constant.MenuCode.StudentManagement.SB_SUBJECT_MEETING_PLAN;
        }

        protected string OnGetSubjectMatterHdFilterExpression()
        {
            return string.Format("SubjectID = {0} AND IsDeleted = 0", AppSession.SubjectID);
        }

        protected override void InitializeDataControl()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PERIOD_SECTION));
            Methods.SetComboBoxField<StandardCode>(cboGCPeriodSection, lstSc, "StandardCodeName", "StandardCodeID");
            cboGCPeriodSection.SelectedIndex = 0;

            if (AppSession.SubjectMatterID > 0)
            {
                SubjectMatterHd entityHd = BusinessLayer.GetSubjectMatterHd(AppSession.SubjectMatterID);
                tacSubjectMatterHd.Value = entityHd.SubjectMatterID.ToString();
                tacSubjectMatterHd.Text = entityHd.SubjectMatterName;
                tacSubjectMatterHd.Readonly = true;
            }

            BindGridView();

            Helper.SetControlEntrySetting(tacSubjectMatterHd, new ControlEntrySetting(true, true, true), "mpFilter");
            Helper.SetControlEntrySetting(cboGCPeriodSection, new ControlEntrySetting(true, true, true), "mpFilter");

            Helper.SetControlEntrySetting(txtMeetingNo, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(cboCompetencyStandard, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        protected void cboCompetencyStandard_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string filterExpression = "1 = 0";
            if (tacSubjectMatterHd.Value != "")
                filterExpression = string.Format("SubjectMatterID = {0} AND IsDeleted = 0", tacSubjectMatterHd.Value);
            List<SubjectCompetencyStandard> lstCompetencyStandard = BusinessLayer.GetSubjectCompetencyStandardList(filterExpression);
            Methods.SetComboBoxField<SubjectCompetencyStandard>(cboCompetencyStandard, lstCompetencyStandard, "SubjectCompetencyStandardName", "SubjectCompetencyStandardID");
            cboCompetencyStandard.SelectedIndex = 0;
        }


        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        protected void cbpBasicCompetency_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string filterExpression = "1 = 0";
            if (cboCompetencyStandard.Value != null)
                filterExpression = string.Format("SubjectCompetencyStandardID = {0} AND IsDeleted = 0", cboCompetencyStandard.Value);
            List<vSubjectBasicCompetency> lstBasicCompetency = BusinessLayer.GetvSubjectBasicCompetencyList(filterExpression);

            ASPxCallbackPanel cbpBasicCompetency = (ASPxCallbackPanel)ddeBasicCompetency.FindControl("cbpBasicCompetency");
            GridView grdBasicCompetency = (GridView)cbpBasicCompetency.FindControl("grdBasicCompetency");
            grdBasicCompetency.DataSource = lstBasicCompetency;
            grdBasicCompetency.DataBind();
        }

        protected void grdBasicCompetency_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vSubjectBasicCompetency entity = (vSubjectBasicCompetency)e.Row.DataItem;
                CheckBox chkBasicCompetency = (CheckBox)e.Row.FindControl("chkBasicCompetency");
                chkBasicCompetency.Attributes.Add("id", entity.SubjectBasicCompetencyID.ToString());
                chkBasicCompetency.Attributes.Add("name", entity.SubjectBasicCompetencyName);
            }
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = "1 = 0";
            if (tacSubjectMatterHd.Value != "")
                filterExpression = string.Format("SubjectMatterID = {0} AND GCPeriodSection = '{1}' AND IsDeleted = 0 ORDER BY MeetingNo ASC", tacSubjectMatterHd.Value, cboGCPeriodSection.Value);
            grdView.DataSource = BusinessLayer.GetvSubjectMeetingPlanHdList(filterExpression);
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

        private void ControlToEntity(SubjectMeetingPlanHd entity)
        {
            entity.MeetingNo = Convert.ToInt16(txtMeetingNo.Text);
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SubjectMeetingPlanHdDao entityDao = new SubjectMeetingPlanHdDao(ctx);
            SubjectMeetingPlanBasicCompetencyDao entityBasicCompDao = new SubjectMeetingPlanBasicCompetencyDao(ctx);
            try
            {
                SubjectMeetingPlanHd entity = new SubjectMeetingPlanHd();
                ControlToEntity(entity);
                entity.GCPeriodSection = cboGCPeriodSection.Value.ToString();
                entity.SubjectMatterID = Convert.ToInt32(tacSubjectMatterHd.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);

                entity.SubjectMeetingPlanHdID = BusinessLayer.GetSubjectMeetingPlanHdMaxID(ctx);

                string[] lstBasicCompetencyID = hdnLstBasicCompetencyID.Value.Split(',');
                foreach (string basicCompetencyID in lstBasicCompetencyID)
                {
                    SubjectMeetingPlanBasicCompetency entityDt = new SubjectMeetingPlanBasicCompetency();
                    entityDt.SubjectMeetingPlanID = entity.SubjectMeetingPlanHdID;
                    entityDt.BasicCompetencyID = Convert.ToInt32(basicCompetencyID);
                    entityBasicCompDao.Insert(entityDt);
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

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SubjectMeetingPlanHdDao entityDao = new SubjectMeetingPlanHdDao(ctx);
            SubjectMeetingPlanBasicCompetencyDao entityBasicCompDao = new SubjectMeetingPlanBasicCompetencyDao(ctx);
            try
            {
                SubjectMeetingPlanHd entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

                List<SubjectMeetingPlanBasicCompetency> lstEntityDt = BusinessLayer.GetSubjectMeetingPlanBasicCompetencyList(string.Format("SubjectMeetingPlanID = {0}", entity.SubjectMeetingPlanHdID), ctx);
                if (hdnLstBasicCompetencyID.Value != "")
                {
                    string[] lstBasicCompetencyID = hdnLstBasicCompetencyID.Value.Split(',');
                    foreach (string basicCompetencyID in lstBasicCompetencyID)
                    {
                        SubjectMeetingPlanBasicCompetency entityDt = lstEntityDt.FirstOrDefault(p => p.BasicCompetencyID.ToString() == basicCompetencyID);
                        if (entityDt == null)
                        {
                            entityDt = new SubjectMeetingPlanBasicCompetency();
                            entityDt.SubjectMeetingPlanID = entity.SubjectMeetingPlanHdID;
                            entityDt.BasicCompetencyID = Convert.ToInt32(basicCompetencyID);
                            entityBasicCompDao.Insert(entityDt);
                        }
                        else
                            lstEntityDt.Remove(entityDt);
                    }
                }
                foreach (SubjectMeetingPlanBasicCompetency entityDt in lstEntityDt)
                {
                    entityBasicCompDao.Delete(entityDt.SubjectMeetingPlanID, entityDt.BasicCompetencyID);
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

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                SubjectMeetingPlanHd entity = BusinessLayer.GetSubjectMeetingPlanHd(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSubjectMeetingPlanHd(entity);
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