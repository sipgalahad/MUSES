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
    public partial class TemplateEmployeeGroupEntryCtl : BaseViewPopupCtl
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
            TemplateEmployeeGroupHd entity = BusinessLayer.GetTemplateEmployeeGroupHd(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.TemplateCode, entity.TemplateName);

            BindGridView();

            
            Helper.SetControlEntrySetting(tacEmployeeID, new ControlEntrySetting(true, true, true), "mpTrxPopup");
           
  
        }

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetvTemplateEmployeeGroupDtList(string.Format("TemplateID = {0} ORDER BY TemplateID ASC", hdnID.Value));
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

        private void ControlToEntity(TemplateEmployeeGroupDt entity)
        {
            entity.EmployeeID = Convert.ToInt32(hdnEmployeeID.Value);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                TemplateEmployeeGroupDt entity = new TemplateEmployeeGroupDt();
                ControlToEntity(entity);
                entity.TemplateID = Convert.ToInt32(hdnID.Value);
                BusinessLayer.InsertTemplateEmployeeGroupDt(entity);
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
                TemplateEmployeeGroupDt entity = BusinessLayer.GetTemplateEmployeeGroupDt(Convert.ToInt32(hdnEntryID.Value), Convert.ToInt32(hdnEmployeeID.Value));
                ControlToEntity(entity);
                //entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateTemplateEmployeeGroupDt(entity);
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
                BusinessLayer.DeleteTemplateEmployeeGroupDt(Convert.ToInt32(hdnID.Value), Convert.ToInt32(hdnEmployeeIDDelete.Value));
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