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
    public partial class PerformanceIndicatorEntryCtl : BaseViewPopupCtl
    {
        protected string OnGetEmployeeFilterExpression()
        {
            return string.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }

        #region Html Getter
        protected string OnGetGCScheduleTypeFromComponent()
        {
            return Constant.RenumerationSheduleType.FIXED;
        }
        #endregion

        public override void InitializeDataControl(string param)
        {
            
            hdnID.Value = param;
            vPerformanceIndicatorHd entity = BusinessLayer.GetvPerformanceIndicatorHdList(String.Format("PerformanceIndicatorID = {0} ", Convert.ToInt32(hdnID.Value))).FirstOrDefault();
            txtHeaderText.Text = String.Format("{0}", entity.PerformanceIndicatorName);
            txtKeterangan.Text = string.Format("{0} - {1}", entity.IndicatorMarkPeriod, entity.IndicatorMarkType);

            BindGridView();

            Helper.SetControlEntrySetting(txtPerformanceIndicatorDtName, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtDisplayOrder, new ControlEntrySetting(true, true, true), "mpTrxPopup");
  
        }

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetPerformanceIndicatorDtList(string.Format("PerformanceIndicatorID = {0} AND IsDeleted = 0 ", hdnID.Value));
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

        private void ControlToEntity(PerformanceIndicatorDt entity)
        {
            entity.PerformanceIndicatorDtName = txtPerformanceIndicatorDtName.Text;
            entity.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
            
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                PerformanceIndicatorDt entity = new PerformanceIndicatorDt();
                ControlToEntity(entity);
                entity.PerformanceIndicatorDtID = Convert.ToInt32(hdnID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertPerformanceIndicatorDt(entity);
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
                PerformanceIndicatorDt entity = BusinessLayer.GetPerformanceIndicatorDt(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdatePerformanceIndicatorDt(entity);
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
                PerformanceIndicatorDt entity = BusinessLayer.GetPerformanceIndicatorDt(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdatePerformanceIndicatorDt(entity);
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