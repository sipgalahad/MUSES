using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using DevExpress.Web.ASPxCallbackPanel;
using System.Web.UI.HtmlControls;
using CodeX.Data.Core.Dal;
using CodeX.Muses.Web.Information.Program;
using CodeX.Common;

namespace CodeX.Muses.Web.Information.Program
{
    public partial class RenumerationDtCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            //param = 1|01-01-2017

            String[] lstParam = param.Split('|');
            if (lstParam.Length == 1)
            {
                RenumerationHd entityHd = BusinessLayer.GetRenumerationHd(Convert.ToInt32(param));
                txtHeader.Text = String.Format("{0} - {1}", entityHd.RenumerationName, entityHd.RenumerationCode);
                if (entityHd.CurrentTransactionID != null)
                    hdnID.Value = entityHd.CurrentTransactionID.ToString();
                else
                    hdnID.Value = "0";
                BindGridView();
            }
            else if (lstParam[0] == "ep")
            {
                OrganizationPosition op = BusinessLayer.GetOrganizationPosition(Convert.ToInt32(lstParam[2]));
                trOrganizationPosition.Style.Remove("display");
                txtorganizationPosition.Text = op.OrganizationPositionName;

                if (lstParam[1] != "0")
                {
                    TransRenumerationHd entityTransHd = BusinessLayer.GetTransRenumerationHd(Convert.ToInt32(lstParam[1]));
                    RenumerationHd entityHd = BusinessLayer.GetRenumerationHd(entityTransHd.RenumerationID);
                    hdnID.Value = entityTransHd.TransactionID.ToString();
                    txtHeader.Text = String.Format("{0} - {1}", entityHd.RenumerationName, entityHd.RenumerationCode);
                }
                else
                {
                    hdnID.Value = "0";
                    txtHeader.Text = "";
                }
                BindGridView();
            }
            else if (lstParam[0] == "ej")
            {
                JobLevel op = BusinessLayer.GetJobLevel(Convert.ToInt32(lstParam[2]));
                trOrganizationPosition.Style.Remove("display");
                txtorganizationPosition.Text = op.JobLevelName;

                if (lstParam[1] != "")
                {
                    TransRenumerationHd entityTransHd = BusinessLayer.GetTransRenumerationHd(Convert.ToInt32(lstParam[1]));
                    RenumerationHd entityHd = BusinessLayer.GetRenumerationHd(entityTransHd.RenumerationID);
                    hdnID.Value = entityTransHd.TransactionID.ToString();
                    txtHeader.Text = String.Format("{0} - {1}", entityHd.RenumerationName, entityHd.RenumerationCode);
                }
                else
                {
                    hdnID.Value = "0";
                    txtHeader.Text = "";
                }
                BindGridView();
            }
            else
            {
                hdnStartEffectiveDate.Value = lstParam[1];
                string filterExpression = String.Format("RenumerationID = {0} AND StartEffectiveDate <= '{1}' AND GCTransactionStatus = '{2}'", lstParam[0], Helper.GetDatePickerValue(lstParam[1]).ToString("yyyyMMdd"), Constant.TransactionStatus.APPROVED);
                RenumerationHd entityHd = BusinessLayer.GetRenumerationHd(Convert.ToInt32(lstParam[0]));
                txtHeader.Text = String.Format("{0} - {1}", entityHd.RenumerationName, entityHd.RenumerationCode);
                TransRenumerationHd entityTransHd = BusinessLayer.GetTransRenumerationHdList(filterExpression, 1, 1, "StartEffectiveDate DESC").FirstOrDefault();
                if (entityTransHd != null)
                    hdnID.Value = entityTransHd.TransactionID.ToString();
                else
                    hdnID.Value = "0";
                BindGridView();
            }
            
        }

        private void BindGridView()
        {
            lstDayType = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.RENUMERATION_COMP_DAY_TYPE));
            rptView.DataSource = BusinessLayer.GetvTransRenumerationDtList(String.Format("TransactionID = {0} AND IsDeleted = 0",Convert.ToInt32(hdnID.Value)));
            rptView.DataBind();
        }

        List<StandardCode> lstDayType = null;
        List<vTransRenumerationDtFormula> lstEntity = null;
        protected void rptView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vTransRenumerationDt entity = (vTransRenumerationDt)e.Item.DataItem;
                Repeater rptFormula = (Repeater)e.Item.FindControl("rptFormula");
                HtmlGenericControl divAmount = (HtmlGenericControl)e.Item.FindControl("divAmount");
                if (entity.IsUseFormula)
                {
                    lstEntity = BusinessLayer.GetvTransRenumerationDtFormulaList(string.Format("TransactionDtID = {0}", entity.TransactionDtID));
                    rptFormula.DataSource = lstDayType;
                    rptFormula.DataBind();
                }
                else
                    divAmount.InnerHtml = entity.Amount.ToString("N");
            }
        }

        protected void rptFormula_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                StandardCode entity = (StandardCode)e.Item.DataItem;
                HtmlGenericControl divFormula = (HtmlGenericControl)e.Item.FindControl("divFormula");
                vTransRenumerationDtFormula entityFormula = lstEntity.FirstOrDefault(p => p.GCDayType == entity.StandardCodeID);
                if (entityFormula != null)
                {
                    if (entityFormula.FormulaRemarks != "")
                        divFormula.InnerHtml = string.Format("{0} ({1})", entityFormula.FormulaName, entityFormula.FormulaRemarks);
                    else
                        divFormula.InnerHtml = entityFormula.FormulaName;
                }
            }
        }

        protected void cbpPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}