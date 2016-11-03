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
    public partial class UpdateRenumerationEntryCtl : BaseViewPopupCtl
    {
        protected string OnGetFormulaFilterExpression()
        {
            return string.Format("IsDeleted = 0");
        }

        protected string OnGetEmployeeFilterExpression()
        {
            return string.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }

        public override void InitializeDataControl(string param)
        {
            Repeater rptDayType = (Repeater)ddeDayType.FindControl("rptDayType");
            List<StandardCode> lstDayType = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsDeleted = 0", Constant.StandardCode.RENUMERATION_COMP_DAY_TYPE));
            rptDayType.DataSource = lstDayType;
            rptDayType.DataBind();

            //List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.POSITION_LEVEL, Constant.StandardCode.POSITION_TYPE, Constant.StandardCode.SCHEDULE_TYPE));
            //Methods.SetComboBoxField<StandardCode>(cboGCPositionLevel, lstSc.Where(p => p.ParentID == Constant.StandardCode.POSITION_LEVEL).ToList(), "StandardCodeName", "StandardCodeID");
            //Methods.SetComboBoxField<StandardCode>(cboGCPositionType, lstSc.Where(p => p.ParentID == Constant.StandardCode.POSITION_TYPE).ToList(), "StandardCodeName", "StandardCodeID");
            //Methods.SetComboBoxField<StandardCode>(cboGCScheduleType, lstSc.Where(p => p.ParentID == Constant.StandardCode.SCHEDULE_TYPE).ToList(), "StandardCodeName", "StandardCodeID");
            

            //List<HRWeeklySchedule> lstWs = BusinessLayer.GetHRWeeklyScheduleList(string.Format("IsDeleted = 0"));
            //Methods.SetComboBoxField<HRWeeklySchedule>(cboWeeklyScheduleID, lstWs, "WeeklyScheduleName", "WeeklyScheduleID");




            hdnID.Value = param;
            String temp_hdn = string.Format("TransactionDtID = {0}", hdnID.Value);
            vTransRenumerationDt entity = BusinessLayer.GetvTransRenumerationDtList(string.Format("TransactionDtID = {0}", hdnID.Value))[0];
            //TransRenumerationDt entity = BusinessLayer.GetTransRenumerationDt(Convert.ToInt32(hdnID.Value));
            TransRenumerationHd entityHd = BusinessLayer.GetTransRenumerationHd(Convert.ToInt32(entity.TransactionID));
            txtHeaderText.Text = string.Format("{0} - {1}", entityHd.TransactionNo, entity.RenumerationCompName);

            BindGridView();

            //Helper.SetControlEntrySetting(txtOrganizationPositionName, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            //Helper.SetControlEntrySetting(cboGCPositionLevel, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            //Helper.SetControlEntrySetting(cboGCPositionType, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            //Helper.SetControlEntrySetting(cboGCScheduleType, new ControlEntrySetting(true, true, false), "mpTrxPopup");
            //Helper.SetControlEntrySetting(cboWeeklyScheduleID, new ControlEntrySetting(true, true, false), "mpTrxPopup");
            //Helper.SetControlEntrySetting(tacOrganizationPositionEmployee, new ControlEntrySetting(true, true, false), "mpTrxPopup");

            Helper.SetControlEntrySetting(tacFormulaID, new ControlEntrySetting(true, true, false), "mpTrxPopup");

            //Helper.SetControlEntrySetting(chkIsSchedule, new ControlEntrySetting(true, true, false), "mpTrxPopup");
  
        }

        protected void rptDayType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                StandardCode obj = (StandardCode)e.Item.DataItem;
                CheckBox chkDayType = (CheckBox)e.Item.FindControl("chkDayType");
                chkDayType.Attributes.Add("daytypename", obj.StandardCodeName);
                chkDayType.Attributes.Add("daytypeid", obj.StandardCodeID);
            }
        }

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetvOrganizationPositionList(string.Format("OrganizationDepartmentID = {0} AND IsDeleted = 0 ORDER BY GCPositionLevel ASC", hdnID.Value));
            
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

        private void ControlToEntity(OrganizationPosition entity)
        {
            //entity.OrganizationPositionName = txtOrganizationPositionName.Text;
            //entity.GCPositionLevel = cboGCPositionLevel.Value.ToString();
            ////entity.GCScheduleType = cboGCScheduleType.Value.ToString();
            //entity.GCPositionType = cboGCPositionType.Value.ToString();
            //if (cboGCScheduleType.Value.ToString() != "0" || cboGCScheduleType.Value.ToString() != null)
            //    entity.GCScheduleType = cboGCScheduleType.Value.ToString();
            //else
            //    entity.GCScheduleType = null;
            //if (hdnPICEmployeeID.Value == "0" || hdnPICEmployeeID.Value == null)
            //    entity.PICEmployeeID = null;
            //else
            //    entity.PICEmployeeID = Convert.ToInt32(hdnPICEmployeeID.Value);
            //if (cboWeeklyScheduleID.Value.ToString() != "0" || cboWeeklyScheduleID.Value.ToString() != null)
            //    entity.WeeklyScheduleID = Convert.ToInt32(cboWeeklyScheduleID.Value);
            //else
            //    entity.WeeklyScheduleID = null;
            ////entity.WeeklyScheduleID = Convert.ToInt32(cboWeeklyScheduleID.Value);
            //entity.IsScheduleAllowChanged = chkIsSchedule.Checked;
            
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                OrganizationPosition entity = new OrganizationPosition();
                ControlToEntity(entity);
                entity.OrganizationDepartmentID = Convert.ToInt32(hdnID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertOrganizationPosition(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            try
            {
                OrganizationPosition entity = BusinessLayer.GetOrganizationPosition(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateOrganizationPosition(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                OrganizationPosition entity = BusinessLayer.GetOrganizationPosition(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateOrganizationPosition(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion
    }
}