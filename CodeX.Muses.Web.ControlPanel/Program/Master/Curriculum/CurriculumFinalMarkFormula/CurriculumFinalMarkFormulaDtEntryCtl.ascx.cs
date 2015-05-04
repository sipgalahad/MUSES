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

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class CurriculumFinalMarkFormulaDtEntryCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            CurriculumFinalMarkFormulaHd entity = BusinessLayer.GetCurriculumFinalMarkFormulaHd(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.CurriculumFinalMarkFormulaCode, entity.CurriculumFinalMarkFormulaName);

            Repeater rptMarkType = (Repeater)ddeMarkType.FindControl("rptMarkType");
            List<CurriculumMarkTypeDt> lstMarkType = BusinessLayer.GetCurriculumMarkTypeDtList(string.Format("CurriculumMarkTypeID = {0} AND IsDeleted = 0", entity.CurriculumMarkTypeID));
            rptMarkType.DataSource = lstMarkType;
            rptMarkType.DataBind();

            BindGridView();

            Helper.SetControlEntrySetting(txtDisplayOrder, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtCurriculumFinalMarkFormulaDtName, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        protected void rptMarkType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CurriculumMarkTypeDt obj = (CurriculumMarkTypeDt)e.Item.DataItem;
                CheckBox chkMarkType = (CheckBox)e.Item.FindControl("chkMarkType");
                chkMarkType.Attributes.Add("marktypename", obj.CurriculumMarkTypeDtName);
                chkMarkType.Attributes.Add("marktypeid", obj.CurriculumMarkTypeDtID.ToString());
            }
        }

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetvCurriculumFinalMarkFormulaDtList(string.Format("CurriculumFinalMarkFormulaID = {0} ORDER BY DisplayOrder ASC", hdnID.Value));
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

        private void ControlToEntity(CurriculumFinalMarkFormulaDt entity)
        {
            entity.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
            entity.FinalMarkPercentage = Convert.ToDecimal(txtFinalMarkPercentage.Text);
            entity.CurriculumFinalMarkFormulaDtName = txtCurriculumFinalMarkFormulaDtName.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            CurriculumFinalMarkFormulaDtDao entityDao = new CurriculumFinalMarkFormulaDtDao(ctx);
            CurriculumFinalMarkFormulaDtMarkTypeDao entityDtDao = new CurriculumFinalMarkFormulaDtMarkTypeDao(ctx);
            try
            {
                CurriculumFinalMarkFormulaDt entity = new CurriculumFinalMarkFormulaDt();
                ControlToEntity(entity);
                entity.CurriculumFinalMarkFormulaID = Convert.ToInt32(hdnID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                entity.CurriculumFinalMarkFormulaDtID = BusinessLayer.GetCurriculumFinalMarkFormulaDtMaxID(ctx);

                string[] lstMarkTypeID = hdnLstMarkTypeID.Value.Split(',');
                foreach (string markTypeID in lstMarkTypeID)
                {
                    CurriculumFinalMarkFormulaDtMarkType entityDt = new CurriculumFinalMarkFormulaDtMarkType();
                    entityDt.CurriculumFinalMarkFormulaDtID = entity.CurriculumFinalMarkFormulaDtID;
                    entityDt.CurriculumMarkTypeDtID = Convert.ToInt32(markTypeID);
                    entityDtDao.Insert(entityDt);
                }

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
            CurriculumFinalMarkFormulaDtDao entityDao = new CurriculumFinalMarkFormulaDtDao(ctx);
            CurriculumFinalMarkFormulaDtMarkTypeDao entityDtDao = new CurriculumFinalMarkFormulaDtMarkTypeDao(ctx);
            try
            {
                CurriculumFinalMarkFormulaDt entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

                List<CurriculumFinalMarkFormulaDtMarkType> lstEntityDt = BusinessLayer.GetCurriculumFinalMarkFormulaDtMarkTypeList(string.Format("CurriculumFinalMarkFormulaDtID = {0}", entity.CurriculumFinalMarkFormulaDtID), ctx);
                string[] lstMarkTypeID = hdnLstMarkTypeID.Value.Split(',');
                foreach (string markTypeID in lstMarkTypeID)
                {
                    CurriculumFinalMarkFormulaDtMarkType entityDt = lstEntityDt.FirstOrDefault(p => p.CurriculumMarkTypeDtID == Convert.ToInt32(markTypeID));
                    if (entityDt == null)
                    {
                        entityDt = new CurriculumFinalMarkFormulaDtMarkType();
                        entityDt.CurriculumFinalMarkFormulaDtID = entity.CurriculumFinalMarkFormulaDtID;
                        entityDt.CurriculumMarkTypeDtID = Convert.ToInt32(markTypeID);
                        entityDtDao.Insert(entityDt);
                    }
                    else
                        lstEntityDt.Remove(entityDt);
                }

                foreach (CurriculumFinalMarkFormulaDtMarkType entityDt in lstEntityDt)
                {
                    entityDtDao.Delete(entityDt.CurriculumFinalMarkFormulaDtID, entityDt.CurriculumMarkTypeDtID);
                }

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
                CurriculumFinalMarkFormulaDt entity = BusinessLayer.GetCurriculumFinalMarkFormulaDt(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateCurriculumFinalMarkFormulaDt(entity);
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