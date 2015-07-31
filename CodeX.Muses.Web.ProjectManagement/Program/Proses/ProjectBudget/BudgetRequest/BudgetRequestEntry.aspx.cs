using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Core.Dal;
using System.Data;
using CodeX.Common;

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class BudgetRequestEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.BUDGET_REQUEST;
        }

        protected override void InitializeDataControl()
        {
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();
            hdnRecordFilterExpression.Value = String.Format("ProjectID = {0}", AppSession.ProjectID);
            List<vTeamDt> lstTeamDt = null;
            if(AppSession.UserLogin.EmployeeID != 0)
                lstTeamDt = BusinessLayer.GetvTeamDtList(String.Format("ProjectID = '{0}' AND IsDeleted = 0 AND (EmployeeCoordinatorID = {1} OR ListEmployeeID1 LIKE '%;{1};%')", AppSession.ProjectID, AppSession.UserLogin.EmployeeID));
            else
                lstTeamDt = BusinessLayer.GetvTeamDtList(String.Format("ProjectID = '{0}' AND IsDeleted = 0", AppSession.ProjectID));
            Methods.SetComboBoxField(cboTeamDt, lstTeamDt, "Position", "TeamDtID");
            cboTeamDt.SelectedIndex = 0;
            txtRequestDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            
            BindGridView(1, true, ref PageCount, ref RowCount);
            Helper.SetControlEntrySetting(tacProjectBudget, new ControlEntrySetting(true, false, true), "mpTrx");
            Helper.SetControlEntrySetting(txtRequestAmount, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        #region Filter Expression Search Dialog
        protected string OnGetProjectBudgetFilterExpression()
        {
            return string.Format("ProjectID = {0} AND ItemID IS NULL AND BudgetID NOT IN (SELECT BudgetID FROM BudgetRequestDt WHERE GCItemDetailStatus = '{1}')", AppSession.ProjectID, Constant.TransactionStatus.OPEN);
        }
        #endregion

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnRequestID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtBudgetRequestNo, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtRequestDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(cboTeamDt, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtNotes, new ControlEntrySetting(true, true, false));
        }

        #region Load Entity
        public override void OnAddRecord()
        {
            hdnPageCount.Value = "0";
            hdnRowCount.Value = "0";
            hdnIsEditable.Value = "1";
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
            return BusinessLayer.GetvBudgetRequestHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vBudgetRequestHd entity = BusinessLayer.GetvBudgetRequestHd(filterExpression, PageIndex, "BudgetRequestNo DESC");
            if(entity != null) EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvBudgetRequestHdRowIndex(filterExpression, keyValue, "BudgetRequestNo DESC");
            vBudgetRequestHd entity = BusinessLayer.GetvBudgetRequestHd(filterExpression, PageIndex, "BudgetRequestNo DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vBudgetRequestHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
                hdnIsEditable.Value = "0";
            }
            else
                hdnIsEditable.Value = "1";

            hdnRequestID.Value = entity.BudgetRequestID.ToString();
            cboTeamDt.Text = entity.Position;
            cboTeamDt.Value = entity.TeamDtID.ToString();
            txtBudgetRequestNo.Text = entity.BudgetRequestNo;
            txtRequestDate.Text = entity.RequestDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtNotes.Text = entity.Remarks;

            BindGridView(1, true, ref PageCount, ref RowCount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnRequestID.Value != "")
                filterExpression = string.Format("BudgetRequestID = {0} AND IsDeleted = 0", hdnRequestID.Value);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvBudgetRequestDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vBudgetRequestDt> lstEntity = BusinessLayer.GetvBudgetRequestDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "BudgetName ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
        #endregion

        #region Save Header
        public void SaveBudgetRequestHd(IDbContext ctx, ref int OrderID)
        {
            BudgetRequestHdDao entityHdDao = new BudgetRequestHdDao(ctx);
            if (hdnRequestID.Value == "0")
            {
                BudgetRequestHd entityHd = new BudgetRequestHd();
                entityHd.RequestDate = Helper.GetDatePickerValue(txtRequestDate.Text);
                entityHd.Remarks = txtNotes.Text;
                entityHd.TeamDtID = Convert.ToInt32(cboTeamDt.Value);
                entityHd.ProjectID = AppSession.ProjectID;
                entityHd.BudgetRequestNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.BUDGET_REQUEST, entityHd.RequestDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();

                entityHd.CreatedBy = AppSession.UserLogin.UserID;

                entityHdDao.Insert(entityHd);

                OrderID = BusinessLayer.GetBudgetRequestHdMaxID(ctx);
            }
            else
            {
                OrderID = Convert.ToInt32(hdnRequestID.Value);
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            try
            {
                int OrderID = 0;
                SaveBudgetRequestHd(ctx, ref OrderID);
                retval = OrderID.ToString();
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            try
            {
                BudgetRequestHd entity = BusinessLayer.GetBudgetRequestHd(Convert.ToInt32(hdnRequestID.Value));
                entity.Remarks = txtNotes.Text;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateBudgetRequestHd(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        protected override bool OnApproveRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            BudgetRequestHdDao budgetHdDao = new BudgetRequestHdDao(ctx);
            BudgetRequestDtDao budgetDtDao = new BudgetRequestDtDao(ctx);
            try
            {
                BudgetRequestHd budgetRequestHd = budgetHdDao.Get(Convert.ToInt32(hdnRequestID.Value));
                budgetRequestHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                budgetRequestHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                budgetHdDao.Update(budgetRequestHd);

                string filterExpressionPurchaseRequestHd = String.Format("BudgetRequestID = {0} AND IsDeleted = 0", budgetRequestHd.BudgetRequestID);
                List<BudgetRequestDt> lstBudgetRequestDt = BusinessLayer.GetBudgetRequestDtList(filterExpressionPurchaseRequestHd);
                foreach (BudgetRequestDt budgetDt in lstBudgetRequestDt)
                {
                    budgetDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
                    budgetDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    budgetDtDao.Update(budgetDt);
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

        protected override bool OnProposeRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            BudgetRequestHdDao budgetHdDao = new BudgetRequestHdDao(ctx);
            BudgetRequestDtDao budgetDtDao = new BudgetRequestDtDao(ctx);
            try
            {
                BudgetRequestHd budgetRequestHd = budgetHdDao.Get(Convert.ToInt32(hdnRequestID.Value));
                budgetRequestHd.GCTransactionStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                budgetRequestHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                budgetHdDao.Update(budgetRequestHd);

                string filterExpressionPurchaseRequestHd = String.Format("BudgetRequestID = {0} AND IsDeleted = 0", budgetRequestHd.BudgetRequestID);
                List<BudgetRequestDt> lstBudgetRequestDt = BusinessLayer.GetBudgetRequestDtList(filterExpressionPurchaseRequestHd);
                foreach (BudgetRequestDt budgetDt in lstBudgetRequestDt)
                {
                    budgetDt.GCItemDetailStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                    budgetDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    budgetDtDao.Update(budgetDt);
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

        protected override bool OnVoidRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            BudgetRequestHdDao budgetHdDao = new BudgetRequestHdDao(ctx);
            BudgetRequestDtDao budgetDtDao = new BudgetRequestDtDao(ctx);
            try
            {
                BudgetRequestHd budgetRequestHd = budgetHdDao.Get(Convert.ToInt32(hdnRequestID.Value));
                budgetRequestHd.GCTransactionStatus = Constant.TransactionStatus.VOID;
                budgetRequestHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                budgetHdDao.Update(budgetRequestHd);

                string filterExpressionBudgetRequestHd = String.Format("BudgetRequestID = {0} AND IsDeleted = 0", budgetRequestHd.BudgetRequestID);
                List<BudgetRequestDt> lstBudgetRequestDt = BusinessLayer.GetBudgetRequestDtList(filterExpressionBudgetRequestHd);
                foreach (BudgetRequestDt budgetDt in lstBudgetRequestDt)
                {
                    budgetDt.GCItemDetailStatus = Constant.TransactionStatus.VOID;
                    budgetDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    budgetDtDao.Update(budgetDt);
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

        #region CallBack Trigger
        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount, ref rowCount);
                    result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
        #endregion

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            int OrderID = 0;
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    OrderID = Convert.ToInt32(hdnRequestID.Value);
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage, ref OrderID))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                OrderID = Convert.ToInt32(hdnRequestID.Value);
                if (OnDeleteEntityDt(ref errMessage, OrderID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpOrderID"] = OrderID.ToString();
        }

        private void ControlToEntity(BudgetRequestDt entityDt)
        {
            entityDt.BudgetID = Convert.ToInt32(hdnBudgetID.Value);
            entityDt.RequestAmount = Convert.ToDecimal(txtRequestAmount.Text);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int OrderID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            BudgetRequestDtDao entityDtDao = new BudgetRequestDtDao(ctx);
            try
            {
                SaveBudgetRequestHd(ctx, ref OrderID);
                BudgetRequestDt entityDt = new BudgetRequestDt();
                ControlToEntity(entityDt);
                entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                entityDt.BudgetRequestID = OrderID;
                entityDt.CreatedBy = AppSession.UserLogin.UserID;
                entityDtDao.Insert(entityDt);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                result = false;
                errMessage = ex.Message;
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
            BudgetRequestDtDao entityDtDao = new BudgetRequestDtDao(ctx);
            try
            {
                BudgetRequestDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entityDt);
                entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDtDao.Update(entityDt);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                result = false;
                errMessage = ex.Message;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnDeleteEntityDt(ref string errMessage, int ID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            BudgetRequestDtDao entityDtDao = new BudgetRequestDtDao(ctx);
            try
            {
                BudgetRequestDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                entityDt.IsDeleted = true;
                entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDtDao.Update(entityDt);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
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