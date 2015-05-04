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

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class CurriculumMarkTypeDtEntryCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            CurriculumMarkType entity = BusinessLayer.GetCurriculumMarkType(Convert.ToInt32(hdnID.Value));
            txtCurriculumMarkTypeName.Text = entity.CurriculumMarkTypeName;

            BindGridView();

            Helper.SetControlEntrySetting(txtCurriculumMarkTypeDtName, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        private void BindGridView()
        {
            string filterExpression = string.Format("CurriculumMarkTypeID = {0} AND IsDeleted = 0", hdnID.Value);
            grdView.DataSource = BusinessLayer.GetCurriculumMarkTypeDtList(filterExpression);
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

        private void ControlToEntity(CurriculumMarkTypeDt entity)
        {
            entity.CurriculumMarkTypeDtName = txtCurriculumMarkTypeDtName.Text;
            entity.IsExam = chkIsExam.Checked;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                CurriculumMarkTypeDt entity = new CurriculumMarkTypeDt();
                ControlToEntity(entity);
                entity.CurriculumMarkTypeID = Convert.ToInt32(hdnID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertCurriculumMarkTypeDt(entity);
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
                CurriculumMarkTypeDt entity = BusinessLayer.GetCurriculumMarkTypeDt(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateCurriculumMarkTypeDt(entity);
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
                CurriculumMarkTypeDt entity = BusinessLayer.GetCurriculumMarkTypeDt(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateCurriculumMarkTypeDt(entity);
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