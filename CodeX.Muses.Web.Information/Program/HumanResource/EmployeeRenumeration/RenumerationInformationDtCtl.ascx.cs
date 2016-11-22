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
    public partial class RenumerationInformationDtCtl : BaseViewPopupCtl
    {
        protected string OnGetFormulaFilterExpression()
        {
            return string.Format("IsDeleted = 0");
        }

       
        public override void InitializeDataControl(string param)
        {
            
            String[] lstParam = param.Split('|');
            hdnID.Value = lstParam[3];
            hdnTempEntity.Value = lstParam[0];
            if (lstParam[0] == "emp")
            {
                vEmployeeRenumeration entity = BusinessLayer.GetvEmployeeRenumerationList(string.Format(" EmployeeID = {0} AND RenumerationCompID = {1} ", lstParam[1], lstParam[2]))[0];
                txtHeaderText.Text = string.Format("{0} - {1}", entity.EmployeeName, entity.RenumerationCompName);
            }
            else if (lstParam[0] == "jl")
            {
                vJobLevelRenumeration entity = BusinessLayer.GetvJobLevelRenumerationList(string.Format(" JobLevelID = {0} AND RenumerationCompID = {1} ", lstParam[1], lstParam[2]))[0];
                txtHeaderText.Text = string.Format("{0} - {1}", entity.JobLevelName, entity.RenumerationCompName);
            }
            else 
            {
                vOrganizationPositionRenumeration entity = BusinessLayer.GetvOrganizationPositionRenumerationList(string.Format(" OrganizationPositionID = {0} AND RenumerationCompID = {1} ", lstParam[1], lstParam[2]))[0];
                txtHeaderText.Text = string.Format("{0} - {1}", entity.OrganizationPositionName, entity.RenumerationCompName);
            }
            BindGridView();

            Helper.SetControlEntrySetting(tacFormulaID, new ControlEntrySetting(true, true, false), "mpTrxPopup");
          }

        private void BindGridView()
        {
            List<StandardCode> lstDayType = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.RENUMERATION_COMP_DAY_TYPE));
            
            lstEntityRenumDtFormula = BusinessLayer.GetvTransRenumerationDtFormulaList(string.Format("TransactionDtID = {0}", hdnID.Value));
            
            grdView.DataSource = lstDayType;            
            grdView.DataBind();
        }

        List<vTransRenumerationDtFormula> lstEntityRenumDtFormula = null;
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                StandardCode entity = (StandardCode)e.Row.DataItem;
                HtmlGenericControl divFormula = (HtmlGenericControl)e.Row.FindControl("divFormula");
                HtmlInputHidden hdnFormulaID = (HtmlInputHidden)e.Row.FindControl("hdnFormulaID");
                HtmlInputHidden hdnFormulaName = (HtmlInputHidden)e.Row.FindControl("hdnFormulaName");
                vTransRenumerationDtFormula entityFormula = lstEntityRenumDtFormula.FirstOrDefault(p => p.GCDayType == entity.StandardCodeID);
                
                if (entityFormula != null)
                {
                    if (entityFormula.FormulaRemarks != "")
                        divFormula.InnerHtml = string.Format("{0} ({1})", entityFormula.FormulaName, entityFormula.FormulaRemarks);
                    else
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
            
        }

        private bool OnSaveRecordEntityDt(ref string errMessage)
        {
            try
            {
                TransRenumerationDtFormula entity = BusinessLayer.GetTransRenumerationDtFormula(Convert.ToInt32(hdnID.Value), hdnGCDayType.Value);
                if (entity == null)
                {
                    
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