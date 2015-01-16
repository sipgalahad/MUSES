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
    public partial class AdmissionFeeCompEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SP_ADMISSION_FEE_COMP;
        }
        protected override void InitializeDataControl()
        {
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.ADMISSION_PAYMENT_PERIOD, Constant.StandardCode.ADMISSION_FEE_COMP_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboAdmissionPaymentPeriod, lstStandardCode.Where(p => p.ParentID == Constant.StandardCode.ADMISSION_PAYMENT_PERIOD).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboAdmissionFeeCompType, lstStandardCode.Where(p => p.ParentID == Constant.StandardCode.ADMISSION_FEE_COMP_TYPE).ToList(), "StandardCodeName", "StandardCodeID");

            BindGridView();

            Helper.SetControlEntrySetting(cboAdmissionFeeCompType, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboAdmissionPaymentPeriod, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(chkIsFixedAmount, new ControlEntrySetting(true, true, false), "mpTrx");
            Helper.SetControlEntrySetting(txtTotalAmount, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = string.Format("SchoolPeriodID = {0} AND IsDeleted = 0", AppSession.SchoolPeriodID);
            List<vAdmissionFeeComp> lstEntity = BusinessLayer.GetvAdmissionFeeCompList(filterExpression);
            grdView.DataSource = lstEntity;
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

        private void ControlToEntity(AdmissionFeeComp entity)
        {
            entity.GCAdmissionFeeCompType = cboAdmissionFeeCompType.Value.ToString();
            entity.GCAdmissionPaymentPeriod = cboAdmissionPaymentPeriod.Value.ToString();
            entity.IsFixedAmount = chkIsFixedAmount.Checked;
            entity.TotalAmount = Convert.ToDecimal(txtTotalAmount.Text);
            entity.NoOfRegistrationPaymentPeriod = Convert.ToInt16(txtNoOfRegistrationPaymentPeriod.Text);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                AdmissionFeeComp entity = new AdmissionFeeComp();
                ControlToEntity(entity);
                entity.SchoolPeriodID = AppSession.SchoolPeriodID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertAdmissionFeeComp(entity);
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
                AdmissionFeeComp entity = BusinessLayer.GetAdmissionFeeComp(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateAdmissionFeeComp(entity);
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
                AdmissionFeeComp entity = BusinessLayer.GetAdmissionFeeComp(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedDate = DateTime.Now;
                BusinessLayer.UpdateAdmissionFeeComp(entity);
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