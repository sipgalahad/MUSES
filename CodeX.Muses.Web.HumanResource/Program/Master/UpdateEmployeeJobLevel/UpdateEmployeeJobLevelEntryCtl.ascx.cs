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
    public partial class UpdateEmployeeJobLevelEntryCtl : BaseViewPopupCtl
    {
        protected string OnGetFormulaFilterExpression()
        {
            return string.Format("IsDeleted = 0");
        }

       
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            String temp_hdn = string.Format("TransactionDtID = {0}", hdnID.Value);
            vTransEmployeeJobLevelRenumeration entity = BusinessLayer.GetvTransEmployeeJobLevelRenumerationList(string.Format("TransactionDtID = {0}", hdnID.Value))[0];
            txtHeaderText.Text = string.Format("{0}", entity.RenumerationCompName);

            BindGridView();

            Helper.SetControlEntrySetting(tacFormulaID, new ControlEntrySetting(true, true, false), "mpTrxPopup");
          }

        private void BindGridView()
        {
            List<StandardCode> lstDayType = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.RENUMERATION_COMP_DAY_TYPE));
            lstEntity = BusinessLayer.GetvTransEmployeeJobLevelRenumerationFormulaList(string.Format("TransactionDtID = {0}", hdnID.Value));
            grdView.DataSource = lstDayType;            
            grdView.DataBind();
        }

        List<vTransEmployeeJobLevelRenumerationFormula> lstEntity = null;
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                StandardCode entity = (StandardCode)e.Row.DataItem;
                HtmlGenericControl divFormula = (HtmlGenericControl)e.Row.FindControl("divFormula");
                HtmlInputHidden hdnFormulaID = (HtmlInputHidden)e.Row.FindControl("hdnFormulaID");
                HtmlInputHidden hdnFormulaName = (HtmlInputHidden)e.Row.FindControl("hdnFormulaName");
                vTransEmployeeJobLevelRenumerationFormula entityFormula = lstEntity.FirstOrDefault(p => p.GCDayType == entity.StandardCodeID);
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

        private void ControlToEntity(TransEmployeeJobLevelRenumerationFormula entity)
        {
        }

        private bool OnSaveRecordEntityDt(ref string errMessage)
        {
            try
            {
                TransEmployeeJobLevelRenumerationFormula entity = BusinessLayer.GetTransEmployeeJobLevelRenumerationFormula(Convert.ToInt32(hdnID.Value), hdnGCDayType.Value);
                if (entity == null)
                {
                    entity = new TransEmployeeJobLevelRenumerationFormula();
                    entity.FormulaID = Convert.ToInt32(hdnFormulaID.Value);
                    entity.TransactionDtID = Convert.ToInt32(hdnID.Value);
                    entity.GCDayType = hdnGCDayType.Value.ToString();
                    BusinessLayer.InsertTransEmployeeJobLevelRenumerationFormula(entity);
                }
                else
                {
                    entity.FormulaID = Convert.ToInt32(hdnFormulaID.Value);
                    BusinessLayer.UpdateTransEmployeeJobLevelRenumerationFormula(entity);
                        
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
                BusinessLayer.DeleteTransEmployeeJobLevelRenumerationFormula(Convert.ToInt32(hdnID.Value), hdnGCDayType.Value);
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