using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;

namespace CodeX.Muses.Web.Accounting.Program
{
    public partial class JournalTemplateDtEntryCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnTemplateID.Value = param;
            JournalTemplateHd entity = BusinessLayer.GetJournalTemplateHd(Convert.ToInt32(hdnTemplateID.Value));
            txtTemplateCode.Text = entity.TemplateCode;
            txtTemplateName.Text = entity.TemplateName;
            
            List<Variable> lstPosition = new List<Variable>();
            lstPosition.Add(new Variable { Code = "D", Value = GetLabel("Debit") });
            lstPosition.Add(new Variable { Code = "K", Value = GetLabel("Kredit") });
            Methods.SetRadioButtonListField<Variable>(rblPosition, lstPosition, "Value", "Code");

            BindGridView();

            txtGLAccountCode.Attributes.Add("validationgroup", "mpEntryPopup");
            txtSubLedgerDtCode.Attributes.Add("validationgroup", "mpEntryPopup");
            txtAmountPercentage.Attributes.Add("validationgroup", "mpEntryPopup");
            txtDisplayOrder.Attributes.Add("validationgroup", "mpEntryPopup");
        }

        private void BindGridView()
        {
            string filterExpression = string.Format("TemplateID = {0} AND IsDeleted = 0 ORDER BY DisplayOrder", hdnTemplateID.Value);
            List<vJournalTemplateDt> lstEntity = BusinessLayer.GetvJournalTemplateDtList(filterExpression);
            grdViewD.DataSource = lstEntity.Where(p => p.Position == "D").ToList();
            grdViewD.DataBind();

            grdViewK.DataSource = lstEntity.Where(p => p.Position == "K").ToList();
            grdViewK.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                    e.Row.Cells[i].Text = GetLabel(e.Row.Cells[i].Text);
            }
            
        }

        protected void cbpEntryPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            LoadWords();
            string[] param = e.Parameter.Split('|');

            string result = param[0] + "|";
            string errMessage = "";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    if (OnSaveEditRecord(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecord(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                if (OnDeleteRecord(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            BindGridView();
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity(JournalTemplateDt entity)
        {
            entity.GLAccountID = Convert.ToInt32(hdnGLAccountID.Value);
            if (hdnSubLedgerDtID.Value != "" && hdnSubLedgerDtID.Value != "0")
                entity.SubLedgerID = Convert.ToInt32(hdnSubLedgerDtID.Value);
            else
                entity.SubLedgerID = null;
            entity.AmountPercentage = Convert.ToDecimal(txtAmountPercentage.Text);
            entity.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
            entity.Position = Request.Form[rblPosition.UniqueID];
        }

        private bool OnSaveAddRecord(ref string errMessage)
        {
            try
            {
                JournalTemplateDt entity = new JournalTemplateDt();
                ControlToEntity(entity);
                entity.TemplateID = Convert.ToInt32(hdnTemplateID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertJournalTemplateDt(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnSaveEditRecord(ref string errMessage)
        {
            try
            {
                JournalTemplateDt entity = BusinessLayer.GetJournalTemplateDt(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateJournalTemplateDt(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnDeleteRecord(ref string errMessage)
        {
            try
            {
                JournalTemplateDt entity = BusinessLayer.GetJournalTemplateDt(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertJournalTemplateDt(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
    }
}