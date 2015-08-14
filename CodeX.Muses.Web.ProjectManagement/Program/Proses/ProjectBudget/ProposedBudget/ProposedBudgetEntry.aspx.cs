using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Data.Model;
using CodeX.Web.Common;
using CodeX.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxEditors;
using System.Net;
using CodeX.Data.Core.Dal;
using System.Data;

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class ProposedBudgetEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.PROPOSED_BUDGET;
        }

        protected override void InitializeDataControl()
        {
            hdnRecordFilterExpression.Value = String.Format("ProjectID = {0}", AppSession.ProjectID);

            //Helper.SetControlEntrySetting(txtProposedBudgetCode, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            //Helper.SetControlEntrySetting(txtProposedBudgetName, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            //Helper.SetControlEntrySetting(tacItem, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            //Helper.SetControlEntrySetting(txtItemQuantity, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            //Helper.SetControlEntrySetting(cboItemUnit, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            //Helper.SetControlEntrySetting(txtRealizationDate, new ControlEntrySetting(true, true, false), "mpTrxPopup");
            //Helper.SetControlEntrySetting(txtEntryRemarks, new ControlEntrySetting(true, true, false), "mpTrxPopup");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtProposedBudgetNo, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtProposedBudgetDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(hdnTeamDtID, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(tacTeamDt, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        protected void rptFundItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                TextBox txtFundItem = e.Item.FindControl("txtFundItem") as TextBox;
                txtFundItem.CssClass = String.Format("txtCurrency txtFund txtFund_{0}", e.Item.ItemIndex);
                txtFundItem.Text = "0.00";
            }
        }

        #region HTML Get FilterExpression
        protected string OnGetTeamDtFilterExpression()
        {
            string filterExpression = "";
            filterExpression = String.Format("ProjectID = {0} AND IsDeleted = 0", AppSession.ProjectID);
            return filterExpression;
        }
        protected string OnGetFilterExpressionItemProduct()
        {
            return string.Format("GCItemType = '{0}' AND IsDeleted = 0", Constant.ItemType.PRODUCT);
        }
        protected string OnGetProposedBudgetHdFilterExpression() 
        {
            return String.Format("ProjectID = '{0}'", AppSession.ProjectID);
        }
        #endregion

        #region Bind Grid View
        public override void OnAddRecord()
        {
            //hdnPageCount.Value = "0";
            //hdnRowCount.Value = "0";
            //hdnIsEditable.Value = "1";
        }

        protected string IsEditable()
        {
            return hdnIsEditable.Value;
        }

        protected string GetFilterExpression()
        {
            return hdnRecordFilterExpression.Value;
        }

        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvProposedBudgetHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vProposedBudgetHd entity = BusinessLayer.GetvProposedBudgetHd(filterExpression, PageIndex, "ProposedBudgetID DESC");
            hdnID.Value = entity.ProposedBudgetID.ToString();
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvProposedBudgetHdRowIndex(filterExpression, keyValue, "ProposedBudgetID DESC");
            vProposedBudgetHd entity = BusinessLayer.GetvProposedBudgetHd(filterExpression, PageIndex, "ProposedBudgetID DESC");
            hdnID.Value = entity.ProposedBudgetID.ToString();
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vProposedBudgetHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCProposedBudgetStatus != Constant.ProjectStatus.OPEN)
            {
                isShowWatermark = true;
                watermarkText = entity.ProposedBudgetStatusWatermark;
                hdnIsEditable.Value = "0";
            }
            else
                hdnIsEditable.Value = "1";

            txtProposedBudgetNo.Text = entity.ProposedBudgetNo;
            txtProposedBudgetDate.Text = entity.ProposedDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtRemarks.Text = entity.Remarks;
            tacTeamDt.Text = entity.Position;
            tacTeamDt.Value = entity.TeamDtID.ToString();
            hdnTeamDtID.Value = entity.TeamDtID.ToString();
            txtTotalProjectBudget.Text = entity.TotalAmount.ToString("N");
            
            ((BudgetCtl)ctlBudget).InitializeTransactionControl(entity);
            ((InfrastructureBudgetCtl)ctlInfrastructure).InitializeTransactionControl(entity);
        }

        private String OnGetFilterExpression() 
        {
            String filterExpression = "IsDeleted = 0";
            if (hdnID.Value != "")
                filterExpression += String.Format(" AND ProposedBudgetID = {0}", hdnID.Value);
            else
                filterExpression += String.Format(" AND ProposedBudgetID = 0");
            return filterExpression;
        }
        #endregion

        #region Process Detail
        private void ControlToEntity(ProposedBudgetHd entity) 
        {
            entity.TeamDtID = Convert.ToInt32(hdnTeamDtID.Value);
            entity.Remarks = txtRemarks.Text;
            entity.TotalAmount = Convert.ToDecimal(Request.Form[txtTotalProjectBudget.UniqueID]);
            entity.ProposedDate = Helper.GetDatePickerValue(txtProposedBudgetDate.Text);
        }

        public void SaveHeader(IDbContext ctx, ref Int32 OrderID)
        {
            if (hdnID.Value == "" || hdnID.Value == "0")
            {
                ProposedBudgetHdDao entityHdDao = new ProposedBudgetHdDao(ctx);

                ProposedBudgetHd entity = new ProposedBudgetHd();
                ControlToEntity(entity);
                entity.ProposedBudgetNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.PROPOSED_BUDGET, Helper.GetDatePickerValue(txtProposedBudgetDate.Text));
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entity.ProjectID = AppSession.ProjectID;
                entity.GCProposedBudgetStatus = Constant.ProjectStatus.OPEN;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entity);

                OrderID = BusinessLayer.GetProposedBudgetHdMaxID(ctx);
            }
            else
            {
                OrderID = Convert.ToInt32(hdnID.Value);
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            
            try
            {
                int OrderID = 0;
                SaveHeader(ctx, ref OrderID);
                retval = OrderID.ToString();
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

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            try
            {
                ProposedBudgetHd entity = BusinessLayer.GetProposedBudgetHd(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateProposedBudgetHd(entity);

                retval = hdnID.Value;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }

        protected override bool OnVoidRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ProposedBudgetHdDao entityDao = new ProposedBudgetHdDao(ctx);
            ProposedBudgetDtDao entityDtDao = new ProposedBudgetDtDao(ctx);

            try
            {
                ProposedBudgetHd entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
                entity.GCProposedBudgetStatus = Constant.ProjectStatus.CANCELED;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                String filterExpression = String.Format("ProposedBudgetID = {0}", entity.ProposedBudgetID);
                List<ProposedBudgetDt> lstEntityDt = BusinessLayer.GetProposedBudgetDtList(filterExpression, ctx);
                foreach (ProposedBudgetDt entityDt in lstEntityDt) 
                {
                    entityDt.GCItemDetailStatus = Constant.ProjectStatus.CANCELED;
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Update(entityDt);
                }
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

        protected override bool OnProposeRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ProposedBudgetHdDao entityHdDao = new ProposedBudgetHdDao(ctx);
            ProposedBudgetDtDao entityDtDao = new ProposedBudgetDtDao(ctx);
            try
            {
                ProposedBudgetHd entity = entityHdDao.Get(Convert.ToInt32(hdnID.Value));
                entity.GCProposedBudgetStatus = Constant.ProjectStatus.PROPOSED;
                entityHdDao.Update(entity);

                List<ProposedBudgetDt> lstDt = BusinessLayer.GetProposedBudgetDtList(String.Format("ProposedBudgetID = {0} AND IsDeleted = 0", hdnID.Value), ctx);
                foreach (ProposedBudgetDt entityDt in lstDt) 
                {
                    entityDt.GCItemDetailStatus = Constant.ProjectStatus.PROPOSED;
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Update(entityDt);
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

        protected override bool OnReopenRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ProposedBudgetHdDao entityHdDao = new ProposedBudgetHdDao(ctx);
            ProposedBudgetDtDao entityDtDao = new ProposedBudgetDtDao(ctx);
            try
            {
                ProposedBudgetHd entity = entityHdDao.Get(Convert.ToInt32(hdnID.Value));
                entity.GCProposedBudgetStatus = Constant.ProjectStatus.OPEN;
                entityHdDao.Update(entity);

                List<ProposedBudgetDt> lstDt = BusinessLayer.GetProposedBudgetDtList(String.Format("ProposedBudgetID = {0} AND IsDeleted = 0", hdnID.Value), ctx);
                foreach (ProposedBudgetDt entityDt in lstDt)
                {
                    entityDt.GCItemDetailStatus = Constant.ProjectStatus.OPEN;
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Update(entityDt);
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
        #endregion

    }
}