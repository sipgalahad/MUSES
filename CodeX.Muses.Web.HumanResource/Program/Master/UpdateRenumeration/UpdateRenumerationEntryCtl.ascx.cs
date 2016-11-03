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
using System.Web.UI.HtmlControls;

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
            hdnID.Value = param;
            String temp_hdn = string.Format("TransactionDtID = {0}", hdnID.Value);
            vTransRenumerationDt entity = BusinessLayer.GetvTransRenumerationDtList(string.Format("TransactionDtID = {0}", hdnID.Value))[0];
            txtHeaderText.Text = string.Format("{0}", entity.RenumerationCompName);

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

        private void BindGridView()
        {
            List<StandardCode> lstDayType = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.RENUMERATION_COMP_DAY_TYPE));
            lstEntity = BusinessLayer.GetvTransRenumerationDtFormulaList(string.Format("TransactionDtID = {0}", hdnID.Value));
            grdView.DataSource = lstDayType;            
            grdView.DataBind();
        }

        List<vTransRenumerationDtFormula> lstEntity = null;
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                StandardCode entity = (StandardCode)e.Row.DataItem;
                HtmlGenericControl divFormula = (HtmlGenericControl)e.Row.FindControl("divFormula");
                HtmlInputHidden hdnFormulaID = (HtmlInputHidden)e.Row.FindControl("hdnFormulaID");
                HtmlInputHidden hdnFormulaName = (HtmlInputHidden)e.Row.FindControl("hdnFormulaName");
                vTransRenumerationDtFormula entityFormula = lstEntity.FirstOrDefault(p => p.GCDayType == entity.StandardCodeID);
                if (entityFormula != null)
                {
                    divFormula.InnerHtml = entityFormula.FormulaName;
                    hdnFormulaName.Value = entityFormula.FormulaName;
                    hdnFormulaID.Value = entityFormula.FormulaID.ToString();
                }
            }
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
                if (OnSaveRecordEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
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

        private void ControlToEntity(TransRenumerationDtFormula entity)
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

        private bool OnSaveRecordEntityDt(ref string errMessage)
        {
            try
            {
                TransRenumerationDtFormula entity = BusinessLayer.GetTransRenumerationDtFormula(Convert.ToInt32(hdnID.Value), hdnGCDayType.Value);
                if (entity == null)
                {
                    //ControlToEntity(entity);
                    //entity.OrganizationDepartmentID = Convert.ToInt32(hdnID.Value);
                    //entity.CreatedBy = AppSession.UserLogin.UserID;
                    entity = new TransRenumerationDtFormula();
                    entity.FormulaID = Convert.ToInt32(hdnFormulaID.Value);
                    entity.TransactionDtID = Convert.ToInt32(hdnID.Value);
                    entity.GCDayType = hdnGCDayType.Value.ToString();
                    BusinessLayer.InsertTransRenumerationDtFormula(entity);
                }
                else
                {
                    entity.FormulaID = Convert.ToInt32(hdnFormulaID.Value);
                    BusinessLayer.UpdateTransRenumerationDtFormula(entity);
                        
                }
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
                //TransRenumerationDtFormula entity = BusinessLayer.GetTransRenumerationDtFormula(Convert.ToInt32(hdnID.Value), hdnGCDayType.Value);
                //OrganizationPosition entity = BusinessLayer.GetOrganizationPosition(Convert.ToInt32(hdnEntryID.Value));
                //entity.IsDeleted = true;
                //entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                //BusinessLayer.UpdateOrganizationPosition(entity);
                //sentity.FormulaID = Convert.ToInt32(hdnFormulaID.Value);
                BusinessLayer.DeleteTransRenumerationDtFormula(Convert.ToInt32(hdnID.Value), hdnGCDayType.Value);
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