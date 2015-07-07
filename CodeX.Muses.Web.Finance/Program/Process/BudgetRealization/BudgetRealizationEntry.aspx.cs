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
using CodeX.Common;
using DevExpress.Web.ASPxEditors;
using System.Globalization;
using CodeX.Data.Core.Dal;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
namespace CodeX.Muses.Web.Finance.Program
{
    public partial class BudgetRealizationEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Finance.BUDGET_REALIZATION;
        }
        
        protected override void InitializeDataControl()
        {
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();
            String ProjectFilterExpression = "";
            if (AppSession.UserLogin.EmployeeID != null && AppSession.UserLogin.EmployeeID != 0)
            {
                ProjectFilterExpression = String.Format("GCProjectStatus NOT IN ('{0}','{1}') AND " +
                                          "ProjectID IN (SELECT ProjectID FROM vTeamDt WHERE EmployeeCoordinatorID = '{2}' OR ListEmployeeID1 LIKE '%;{2};%')", Constant.ProjectStatus.CANCELED, Constant.ProjectStatus.COMPLETE, AppSession.UserLogin.EmployeeID);
            }
            else
            {
                ProjectFilterExpression = String.Format("GCProjectStatus NOT IN ('{0}','{1}')", Constant.ProjectStatus.CANCELED, Constant.ProjectStatus.COMPLETE);
            }

            List<Project> lstProject = BusinessLayer.GetProjectList(ProjectFilterExpression);
            Methods.SetComboBoxField(cboProject, lstProject, "ProjectName", "ProjectID");
            cboProject.SelectedIndex = 0;
            txtRealizationDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);

            BindGridView(1, true, ref PageCount, ref RowCount);
            Helper.SetControlEntrySetting(tacProjectBudget, new ControlEntrySetting(true, false, true), "mpTrx");
            Helper.SetControlEntrySetting(txtRealizationAmount, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        #region HTML Getter
        public String OnGetProjectBudgetFilterExpression() 
        {
            return String.Format("SELECT BudgetRequestID FROM BudgetRequestHd WHERE GCTransactionStatus IN ('{0}','{1}')", Constant.TransactionStatus.APPROVED, Constant.TransactionStatus.PROCESSED);
        }
        #endregion

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtBudgetRealizationNo, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtRealizationDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(cboProject, new ControlEntrySetting(true, false, true));
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

        public string GetFilterExpression()
        {
            string filterExpression = String.Format("");
            return filterExpression;
        }

        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvBudgetRealizationHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vBudgetRealizationHd entity = BusinessLayer.GetvBudgetRealizationHd(filterExpression, PageIndex, "BudgetRealizationNo DESC");
            if (entity != null) EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvBudgetRealizationHdRowIndex(filterExpression, keyValue, "BudgetRealizationNo DESC");
            vBudgetRealizationHd entity = BusinessLayer.GetvBudgetRealizationHd(filterExpression, PageIndex, "BudgetRealizationNo DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

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

        private void EntityToControl(vBudgetRealizationHd entity, ref bool isShowWatermark, ref string watermarkText) 
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
                hdnIsEditable.Value = "0";
            }
            else
                hdnIsEditable.Value = "1";

            hdnID.Value = entity.BudgetRealizationID.ToString();
            cboProject.Text = entity.ProjectName;
            cboProject.Value = entity.ProjectID.ToString();
            txtBudgetRealizationNo.Text = entity.BudgetRealizationNo;
            txtRealizationDate.Text = entity.RealizationDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtNotes.Text = entity.Remarks;

            BindGridView(1, true, ref PageCount, ref RowCount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
        }

        public void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnID.Value != "")
                filterExpression = String.Format("BudgetRealizationID = {0} AND IsDeleted = 0", hdnID.Value);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvBudgetRealizationDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            List<vBudgetRealizationDt> lstBudgetRealizationDt = BusinessLayer.GetvBudgetRealizationDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "BudgetName ASC");
            grdView.DataSource = lstBudgetRealizationDt;
            grdView.DataBind();
        }
        #endregion

        #region Save Header
        public void SaveBudgetRealizationHd(IDbContext ctx, ref int OrderID)
        {
            BudgetRealizationHdDao entityHdDao = new BudgetRealizationHdDao(ctx);
            if (hdnID.Value == "0")
            {
                BudgetRealizationHd entityHd = new BudgetRealizationHd();
                entityHd.ProjectID = Convert.ToInt32(cboProject.Value);
                entityHd.RealizationDate = Helper.GetDatePickerValue(txtRealizationDate.Text);
                entityHd.Remarks = txtNotes.Text;
                entityHd.BudgetRealizationNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.BUDGET_REALIZATION, entityHd.RealizationDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();

                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);
                OrderID = BusinessLayer.GetBudgetRealizationHdMaxID(ctx);
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
                SaveBudgetRealizationHd(ctx, ref OrderID);
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
                BudgetRealizationHd entity = BusinessLayer.GetBudgetRealizationHd(Convert.ToInt32(hdnID.Value));
                entity.Remarks = txtNotes.Text;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateBudgetRealizationHd(entity);
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
            BudgetRealizationHdDao budgetHdDao = new BudgetRealizationHdDao(ctx);
            BudgetRealizationDtDao budgetDtDao = new BudgetRealizationDtDao(ctx);
            BudgetRequestHdDao bRequestDao = new BudgetRequestHdDao(ctx);
            ProjectBudgetDao pBudgetDao = new ProjectBudgetDao(ctx);

            try
            {
                BudgetRealizationHd budgetRequestHd = budgetHdDao.Get(Convert.ToInt32(hdnID.Value));
                budgetRequestHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                budgetRequestHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                budgetHdDao.Update(budgetRequestHd);

                string filterExpressionPurchaseRequestHd = String.Format("BudgetRealizationID = {0} AND IsDeleted = 0", budgetRequestHd.BudgetRealizationID);
                List<vBudgetRealizationDt> lst = BusinessLayer.GetvBudgetRealizationDtList(filterExpressionPurchaseRequestHd);
                List<BudgetRealizationDt> lstBudgetRequestDt = BusinessLayer.GetBudgetRealizationDtList(filterExpressionPurchaseRequestHd);
                foreach (BudgetRealizationDt budgetDt in lstBudgetRequestDt)
                {
                    budgetDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
                    budgetDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    budgetDtDao.Update(budgetDt);
                    
                    vBudgetRealizationDt temp = lst.FirstOrDefault(x => x.BudgetRealizationDtID == budgetDt.BudgetRealizationDtID);
                    ProjectBudget pBudget = pBudgetDao.Get(temp.BudgetID);
                    pBudget.RealizationAmount += budgetDt.RealizationAmount;
                    pBudgetDao.Update(pBudget);
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

        #region Save Detail
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
                    OrderID = Convert.ToInt32(hdnID.Value);
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
                OrderID = Convert.ToInt32(hdnID.Value);
                if (OnDeleteEntityDt(ref errMessage, OrderID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpOrderID"] = OrderID.ToString();
        }

        private void ControlToEntity(BudgetRealizationDt entityDt)
        {
            entityDt.BudgetRequestDtID = Convert.ToInt32(hdnBudgetID.Value);
            entityDt.RealizationAmount = Convert.ToDecimal(txtRealizationAmount.Text);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int OrderID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            BudgetRealizationDtDao entityDtDao = new BudgetRealizationDtDao(ctx);
            try
            {
                SaveBudgetRealizationHd(ctx, ref OrderID);
                BudgetRealizationDt entityDt = new BudgetRealizationDt();
                ControlToEntity(entityDt);
                entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                entityDt.BudgetRealizationID = OrderID;
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
            BudgetRealizationDtDao entityDtDao = new BudgetRealizationDtDao(ctx);
            try
            {
                BudgetRealizationDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
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
            BudgetRealizationDtDao entityDtDao = new BudgetRealizationDtDao(ctx);
            try
            {
                BudgetRealizationDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
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