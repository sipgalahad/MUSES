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
    public partial class StudentProgressRuleDtEntryCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            StudentProgressRuleHd entity = BusinessLayer.GetStudentProgressRuleHd(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.StudentProgressRuleCode, entity.StudentProgressRuleName);

            BindGridView();

            Helper.SetControlEntrySetting(txtDisplayOrder, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtStudentProgressRuleDtName, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtFromValue, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtToValue, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        protected void rptTaskType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                StandardCode obj = (StandardCode)e.Item.DataItem;
                CheckBox chkTaskType = (CheckBox)e.Item.FindControl("chkTaskType");
                chkTaskType.Attributes.Add("tasktypename", obj.StandardCodeName);
                chkTaskType.Attributes.Add("tasktypeid", obj.StandardCodeID);
            }
        }

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetStudentProgressRuleDtList(string.Format("StudentProgressRuleID = {0} ORDER BY DisplayOrder ASC", hdnID.Value));
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

        private void ControlToEntity(StudentProgressRuleDt entity)
        {
            entity.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
            entity.StudentProgressRuleDtName = txtStudentProgressRuleDtName.Text;
            entity.IsFromPassingGrade = chkIsFromPassingGrade.Checked;
            if (entity.IsFromPassingGrade)
                entity.FromValue = -1;
            else
                entity.FromValue = Convert.ToDecimal(Request.Form[txtFromValue.UniqueID]);
            entity.IsToPassingGrade = chkIsToPassingGrade.Checked;
            if (entity.IsToPassingGrade)
                entity.ToValue = -1;
            else
                entity.ToValue = Convert.ToDecimal(Request.Form[txtToValue.UniqueID]);
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                StudentProgressRuleDt entity = new StudentProgressRuleDt();
                ControlToEntity(entity);
                entity.StudentProgressRuleID = Convert.ToInt32(hdnID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertStudentProgressRuleDt(entity);
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
                StudentProgressRuleDt entity = BusinessLayer.GetStudentProgressRuleDt(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateStudentProgressRuleDt(entity);
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
                StudentProgressRuleDt entity = BusinessLayer.GetStudentProgressRuleDt(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateStudentProgressRuleDt(entity);
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