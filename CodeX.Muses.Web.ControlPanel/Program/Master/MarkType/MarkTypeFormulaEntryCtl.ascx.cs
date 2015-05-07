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
using CodeX.Data.Core.Dal;
using System.Globalization;
using DevExpress.Web.ASPxEditors;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class MarkTypeFormulaEntryCtl : BaseViewPopupCtl
    {
        public String GetMarkTypeNumber()
        {
            return Constant.MarkType.NUMBER;
        }

        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            MarkTypeHd entity = BusinessLayer.GetMarkTypeHd(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0} ({1})", entity.MarkTypeCode, entity.MarkTypeName);

            List<MarkTypeHd> lstFromMarkType = BusinessLayer.GetMarkTypeHdList(string.Format("MarkTypeID != {0} AND IsDeleted = 0", hdnID.Value));
            Methods.SetComboBoxField<MarkTypeHd>(cboFromMarkType, lstFromMarkType, "MarkTypeName", "cfMarkTypeID");
            cboFromMarkType.SelectedIndex = 0;

            BindGridView();

            Helper.SetControlEntrySetting(tacFromMarkTypeDt, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(tacToMarkTypeDt, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtMaxValue, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtMinValue, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetvMarkTypeFormulaList(string.Format("MarkTypeID = {0} AND FromMarkTypeID = {1} AND IsDeleted = 0", hdnID.Value, cboFromMarkType.Value.ToString().Split('|')[0]));
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

        private void ControlToEntity(MarkTypeFormula entity)
        {
            if (hdnGCMarkType.Value == Constant.MarkType.NUMBER)
            {
                entity.MinValue = Convert.ToDecimal(txtMinValue.Text);
                entity.MaxValue = Convert.ToDecimal(txtMaxValue.Text);
                entity.FromMarkTypeDtID = null;
            }
            else
            {
                entity.FromMarkTypeDtID = Convert.ToInt32(hdnFromMarkTypeDtID.Value);
                entity.MinValue = 0;
                entity.MaxValue = 0;
            }
            entity.ToMarkTypeDtID = Convert.ToInt32(hdnToMarkTypeDtID.Value);
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            MarkTypeFormulaDao entityDao = new MarkTypeFormulaDao(ctx);
            try
            {
                MarkTypeFormula entity = new MarkTypeFormula();
                ControlToEntity(entity);
                entity.MarkTypeID = Convert.ToInt32(hdnID.Value);
                entity.FromMarkTypeID = Convert.ToInt32(cboFromMarkType.Value.ToString().Split('|')[0]);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            MarkTypeFormulaDao entityDao = new MarkTypeFormulaDao(ctx);
            try
            {
                MarkTypeFormula entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                MarkTypeFormula entity = BusinessLayer.GetMarkTypeFormula(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateMarkTypeFormula(entity);
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