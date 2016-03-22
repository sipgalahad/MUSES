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
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class RBudgetRequestEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.RBUDGET_REQUEST;
        }

        List<StandardCode> lstFundType = null;
        protected override void InitializeDataControl()
        {
            lstFundType = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PROJECT_FUNDING));
            rptFundHeader.DataSource = lstFundType;
            rptFundHeader.DataBind();

            rptFundItem.DataSource = lstFundType;
            rptFundItem.DataBind();

            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();
            hdnRecordFilterExpression.Value = String.Format("ProjectID = {0}", AppSession.ProjectID);

            string filterExpression = string.Format("ProjectID = {0} AND IsDeleted = 0", AppSession.ProjectID);
            if (AppSession.IsMyProject)
            {
                vRProjectOrganizationMember entityOrganizationMember = BusinessLayer.GetvRProjectOrganizationMemberList(string.Format("ProjectID = {0} AND EmployeeID = {1}", AppSession.ProjectID, AppSession.UserLogin.EmployeeID)).FirstOrDefault();
                hdnMyProjectOrganizationID.Value = entityOrganizationMember.ProjectOrganizationID.ToString();
                filterExpression += string.Format(" AND ProjectTaskGroupID IN (SELECT ProjectTaskGroupID FROM vRProjectTaskAssign WHERE DisplayPath LIKE '%/{0}/%')", hdnMyProjectOrganizationID.Value);
            }
            List<RProjectTaskGroup> lstEntity = BusinessLayer.GetRProjectTaskGroupList(filterExpression);
            Methods.SetComboBoxField(cboProjectTaskGroup, lstEntity, "ProjectTaskGroupName", "ProjectTaskGroupID");
            cboProjectTaskGroup.SelectedIndex = 0;
            
            BindGridView(1, true, ref PageCount, ref RowCount);
            Helper.SetControlEntrySetting(txtBudgetRequestDtName, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtTotalAmount, new ControlEntrySetting(false, false, true), "mpTrx");
        }

        protected void rptFundItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                StandardCode entity = (StandardCode)e.Item.DataItem;
                TextBox txtFundItem = e.Item.FindControl("txtFundItem") as TextBox;
                txtFundItem.Attributes.Add("GCProjectFundType", entity.StandardCodeID);
            }
        }

        #region Filter Expression Search Dialog
        protected string OnGetProjectBudgetFilterExpression()
        {
            return string.Format("ProjectID = {0} AND ItemID IS NULL AND BudgetDtID NOT IN (SELECT BudgetDtID FROM RBudgetRequestDt WHERE GCItemDetailStatus = '{1}')", AppSession.ProjectID, Constant.TransactionStatus.OPEN);
        }
        #endregion

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnRequestID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtBudgetRequestNo, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtRequestDate, new ControlEntrySetting(true, true, true, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtRequestTime, new ControlEntrySetting(true, true, true, Constant.DefaultValueEntry.TIME_NOW));
            SetControlEntrySetting(cboProjectTaskGroup, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtNotes, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtDueDate, new ControlEntrySetting(true, true, true, Constant.DefaultValueEntry.DATE_NOW));
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
            return BusinessLayer.GetvRBudgetRequestHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vRBudgetRequestHd entity = BusinessLayer.GetvRBudgetRequestHd(filterExpression, PageIndex, "BudgetRequestNo DESC");
            if(entity != null) EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvRBudgetRequestHdRowIndex(filterExpression, keyValue, "BudgetRequestNo DESC");
            vRBudgetRequestHd entity = BusinessLayer.GetvRBudgetRequestHd(filterExpression, PageIndex, "BudgetRequestNo DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vRBudgetRequestHd entity, ref bool isShowWatermark, ref string watermarkText)
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
            cboProjectTaskGroup.Value = entity.ProjectTaskGroupID.ToString();
            txtBudgetRequestNo.Text = entity.BudgetRequestNo;
            txtRequestDate.Text = entity.RequestDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtRequestTime.Text = entity.RequestTime;
            txtDueDate.Text = entity.DueDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
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
                rowCount = BusinessLayer.GetRBudgetRequestDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            if (lstFundType == null)
                lstFundType = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PROJECT_FUNDING));

            List<RBudgetRequestDt> lstEntity = BusinessLayer.GetRBudgetRequestDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "BudgetRequestDtName ASC");
            if (lstEntity.Count > 0)
            {
                string lstBudgetRequestDtID = string.Join(",", lstEntity.Select(p => p.BudgetRequestDtID).ToList());
                lstEntityFund = BusinessLayer.GetRBudgetRequestDtFundList(string.Format("BudgetRequestDtID IN ({0})", lstBudgetRequestDtID));
            }
            else
                lstEntityFund = new List<RBudgetRequestDtFund>();

            rptViewHeader.DataSource = lstFundType;
            rptViewHeader.DataBind();

            thContainerAmount.ColSpan = lstFundType.Count;

            rptView.DataSource = lstEntity;
            rptView.DataBind();
        }

        List<RBudgetRequestDtFund> lstEntityFund = null;
        protected void rptView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptViewItem = e.Item.FindControl("rptViewItem") as Repeater;
                rptViewItem.DataSource = lstFundType;
                rptViewItem.DataBind();
            }
        }

        protected void rptViewItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                StandardCode entity = e.Item.DataItem as StandardCode;
                RBudgetRequestDt entityBudgetRequestDt = ((RepeaterItem)e.Item.Parent.Parent).DataItem as RBudgetRequestDt;
                HtmlTableCell tdTotalAmount = e.Item.FindControl("tdTotalAmount") as HtmlTableCell;

                decimal totalAmount = 0;
                tdTotalAmount.Attributes.Add("GCProjectFundType", entity.StandardCodeID);
                RBudgetRequestDtFund entityFund = lstEntityFund.FirstOrDefault(p => p.BudgetRequestDtID == entityBudgetRequestDt.BudgetRequestDtID && p.GCProjectFundType == entity.StandardCodeID);
                if (entityFund != null)
                    totalAmount = entityFund.TotalAmount;
                else
                    totalAmount = 0;
                tdTotalAmount.InnerHtml = totalAmount.ToString("N");
                tdTotalAmount.Attributes.Add("TotalAmount", totalAmount.ToString());
            }
        }
        #endregion

        #region Save Header
        public void SaveRBudgetRequestHd(IDbContext ctx, ref int OrderID)
        {
            RBudgetRequestHdDao entityHdDao = new RBudgetRequestHdDao(ctx);
            if (hdnRequestID.Value == "0")
            {
                RBudgetRequestHd entityHd = new RBudgetRequestHd();
                entityHd.RequestDate = Helper.GetDatePickerValue(txtRequestDate.Text);
                entityHd.RequestTime = txtRequestTime.Text;
                entityHd.DueDate = Helper.GetDatePickerValue(txtDueDate.Text);
                entityHd.Remarks = txtNotes.Text;
                entityHd.ProjectTaskGroupID = Convert.ToInt32(cboProjectTaskGroup.Value);
                entityHd.ProjectID = AppSession.ProjectID;
                entityHd.BudgetRequestNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.BUDGET_REQUEST, entityHd.RequestDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();

                entityHd.CreatedBy = AppSession.UserLogin.UserID;

                entityHdDao.Insert(entityHd);

                OrderID = BusinessLayer.GetRBudgetRequestHdMaxID(ctx);
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
                SaveRBudgetRequestHd(ctx, ref OrderID);
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
                RBudgetRequestHd entity = BusinessLayer.GetRBudgetRequestHd(Convert.ToInt32(hdnRequestID.Value));
                entity.Remarks = txtNotes.Text;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateRBudgetRequestHd(entity);
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
            RBudgetRequestHdDao budgetHdDao = new RBudgetRequestHdDao(ctx);
            RBudgetRequestDtDao budgetDtDao = new RBudgetRequestDtDao(ctx);
            try
            {
                RBudgetRequestHd budgetRequestHd = budgetHdDao.Get(Convert.ToInt32(hdnRequestID.Value));
                budgetRequestHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                budgetRequestHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                budgetHdDao.Update(budgetRequestHd);

                string filterExpressionPurchaseRequestHd = String.Format("BudgetRequestID = {0} AND IsDeleted = 0", budgetRequestHd.BudgetRequestID);
                List<RBudgetRequestDt> lstRBudgetRequestDt = BusinessLayer.GetRBudgetRequestDtList(filterExpressionPurchaseRequestHd);
                foreach (RBudgetRequestDt budgetDt in lstRBudgetRequestDt)
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
            RBudgetRequestHdDao budgetHdDao = new RBudgetRequestHdDao(ctx);
            RBudgetRequestDtDao budgetDtDao = new RBudgetRequestDtDao(ctx);
            try
            {
                RBudgetRequestHd budgetRequestHd = budgetHdDao.Get(Convert.ToInt32(hdnRequestID.Value));
                budgetRequestHd.GCTransactionStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                budgetRequestHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                budgetHdDao.Update(budgetRequestHd);

                string filterExpressionPurchaseRequestHd = String.Format("BudgetRequestID = {0} AND IsDeleted = 0", budgetRequestHd.BudgetRequestID);
                List<RBudgetRequestDt> lstRBudgetRequestDt = BusinessLayer.GetRBudgetRequestDtList(filterExpressionPurchaseRequestHd);
                foreach (RBudgetRequestDt budgetDt in lstRBudgetRequestDt)
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
            RBudgetRequestHdDao budgetHdDao = new RBudgetRequestHdDao(ctx);
            RBudgetRequestDtDao budgetDtDao = new RBudgetRequestDtDao(ctx);
            try
            {
                RBudgetRequestHd budgetRequestHd = budgetHdDao.Get(Convert.ToInt32(hdnRequestID.Value));
                budgetRequestHd.GCTransactionStatus = Constant.TransactionStatus.VOID;
                budgetRequestHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                budgetHdDao.Update(budgetRequestHd);

                string filterExpressionRBudgetRequestHd = String.Format("BudgetRequestID = {0} AND IsDeleted = 0", budgetRequestHd.BudgetRequestID);
                List<RBudgetRequestDt> lstRBudgetRequestDt = BusinessLayer.GetRBudgetRequestDtList(filterExpressionRBudgetRequestHd);
                foreach (RBudgetRequestDt budgetDt in lstRBudgetRequestDt)
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

        private void ControlToEntity(RBudgetRequestDt entityDt)
        {
            entityDt.BudgetRequestDtName = txtBudgetRequestDtName.Text;
            entityDt.TotalAmount = Convert.ToDecimal(Request.Form[txtTotalAmount.UniqueID]);
            entityDt.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int OrderID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            RBudgetRequestDtDao entityDtDao = new RBudgetRequestDtDao(ctx);
            RBudgetRequestDtFundDao entityFundDao = new RBudgetRequestDtFundDao(ctx);
            try
            {
                SaveRBudgetRequestHd(ctx, ref OrderID);
                RBudgetRequestDt entityDt = new RBudgetRequestDt();
                ControlToEntity(entityDt);
                entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                entityDt.BudgetRequestID = OrderID;
                entityDt.CreatedBy = AppSession.UserLogin.UserID;
                entityDt.BudgetRequestDtID = entityDtDao.Insert(entityDt);

                String[] lstSaveValue = hdnLstSaveFund.Value.Split('|');
                foreach (String saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    RBudgetRequestDtFund obj = new RBudgetRequestDtFund();
                    obj.BudgetRequestDtID = entityDt.BudgetRequestDtID;
                    obj.GCProjectFundType = temp[0];
                    obj.TotalAmount = Convert.ToDecimal(temp[1]);
                    entityFundDao.Insert(obj);
                }

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
            RBudgetRequestDtDao entityDtDao = new RBudgetRequestDtDao(ctx);
            RBudgetRequestDtFundDao entityFundDao = new RBudgetRequestDtFundDao(ctx);
            try
            {
                RBudgetRequestDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entityDt);
                entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDtDao.Update(entityDt);

                List<RBudgetRequestDtFund> lstFund = BusinessLayer.GetRBudgetRequestDtFundList(String.Format("BudgetRequestDtID = {0}", entityDt.BudgetRequestDtID), ctx);
                String[] lstSaveValue = hdnLstSaveFund.Value.Split('|');
                foreach (String saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    RBudgetRequestDtFund obj = lstFund.FirstOrDefault(p => p.GCProjectFundType == temp[0]);
                    if (obj == null)
                    {
                        obj = new RBudgetRequestDtFund();
                        obj.BudgetRequestDtID = entityDt.BudgetRequestDtID;
                        obj.GCProjectFundType = temp[0];
                        obj.TotalAmount = Convert.ToDecimal(temp[1]);
                        entityFundDao.Insert(obj);
                    }
                    else
                    {
                        obj.TotalAmount = Convert.ToDecimal(temp[1]);
                        entityFundDao.Update(obj);
                        lstFund.Remove(obj);
                    }
                }
                foreach (RBudgetRequestDtFund obj in lstFund)
                {
                    entityFundDao.Delete(obj.BudgetRequestDtID, obj.GCProjectFundType);
                }
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
            RBudgetRequestDtDao entityDtDao = new RBudgetRequestDtDao(ctx);
            try
            {
                RBudgetRequestDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
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