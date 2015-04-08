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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class SubjectCompetencyStandardEntryCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            SubjectMatterHd entity = BusinessLayer.GetSubjectMatterHd(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.SubjectMatterCode, entity.SubjectMatterName);

            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PERIOD_SECTION));
            Methods.SetComboBoxField<StandardCode>(cboGCPeriodSection, lstSc, "StandardCodeName", "StandardCodeID");
            cboGCPeriodSection.SelectedIndex = 0;

            SubjectCompetencyStandardSummary entitySummary = BusinessLayer.GetSubjectCompetencyStandardSummaryList(string.Format("SubjectMatterID = {0} AND GCPeriodSection = '{1}'", entity.SubjectMatterID, cboGCPeriodSection.Value)).FirstOrDefault();
            txtHeaderText2.Text = entitySummary.SummaryName;

            BindGridView();

            Helper.SetControlEntrySetting(txtSubjectCompetencyStandardName, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtDisplayOrder, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false), "mpTrxPopup");
        }

        private void BindGridView()
        {
            string filterExpression = string.Format("SubjectMatterID = {0} AND GCPeriodSection = '{1}' ORDER BY DisplayOrder ASC", hdnID.Value, cboGCPeriodSection.Value);
            grdView.DataSource = BusinessLayer.GetSubjectCompetencyStandardList(filterExpression);
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

        private void ControlToEntity(SubjectCompetencyStandard entity)
        {
            entity.SubjectCompetencyStandardName = txtSubjectCompetencyStandardName.Text;
            entity.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                SubjectCompetencyStandard entity = new SubjectCompetencyStandard();
                ControlToEntity(entity);
                entity.GCPeriodSection = cboGCPeriodSection.Value.ToString();
                entity.SubjectMatterID = Convert.ToInt32(hdnID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertSubjectCompetencyStandard(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            try
            {
                SubjectCompetencyStandard entity = BusinessLayer.GetSubjectCompetencyStandard(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSubjectCompetencyStandard(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                SubjectCompetencyStandard entity = BusinessLayer.GetSubjectCompetencyStandard(Convert.ToInt32(hdnID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSubjectCompetencyStandard(entity);
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