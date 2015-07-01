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
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        protected bool IsEditable = true;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.PROPOSED_BUDGET;
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = true;
        }

        protected override void InitializeDataControl()
        {
            List<StandardCode> lstFundType = BusinessLayer.GetStandardCodeList(String.Format("IsDeleted = 0 AND IsActive = 1 AND ParentID IN ('{0}','{1}')", Constant.StandardCode.PROJECT_FUNDING, Constant.StandardCode.BUDGET_TYPE));
            rptFundHeader.DataSource = lstFundType.Where(x => x.ParentID == Constant.StandardCode.PROJECT_FUNDING).ToList();
            rptFundHeader.DataBind();

            rptFundItem.DataSource = lstFundType.Where(x => x.ParentID == Constant.StandardCode.PROJECT_FUNDING).ToList();
            rptFundItem.DataBind();

            Methods.SetComboBoxField(cboBudgetType,lstFundType.Where(x => x.ParentID == Constant.StandardCode.BUDGET_TYPE).ToList(),"StandardCodeName","StandardCodeID");
            cboBudgetType.SelectedIndex = 0;

            txtProposedBudgetDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);

            hdnEmployeeCoordinatorID.Value = AppSession.UserLogin.EmployeeID.ToString();
            RowCountPerPage = Constant.GridViewPageSize.GRID_MATRIX;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
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
        protected string OnGetProposedBudgetHdFilterExpression() 
        {
            return String.Format("ProjectID = '{0}'", AppSession.ProjectID);
        }
        #endregion

        #region Bind Grid View
        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = "";
            vProposedBudgetHd entity = BusinessLayer.GetvProposedBudgetHd(filterExpression, PageIndex, "ProposedBudgetID DESC");
            hdnID.Value = entity.ProposedBudgetID.ToString();
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = "";//String.Format("ProposedBudgetNo = '{0}'", keyValue);
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
                IsEditable = false;
            }
            txtProposedBudgetNo.Text = entity.ProposedBudgetNo;
            txtProposedBudgetDate.Text = entity.ProposedBudgetDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtRemarks.Text = entity.Remarks;
            tacTeamDt.Text = entity.Position;
            tacTeamDt.Value = entity.TeamDtID.ToString();
            hdnTeamDtID.Value = entity.TeamDtID.ToString();
            txtTotalProjectBudget.Text = entity.TotalAmount.ToString("N");
            BindGridView(1, true, ref PageCount, ref RowCount);
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

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            String filterExpression = OnGetFilterExpression();

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvProposedBudgetDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MATRIX);
            }
            
            List<StandardCode> lstFundType = BusinessLayer.GetStandardCodeList(String.Format("IsDeleted = 0 AND IsActive = 1 AND ParentID = '{0}'", Constant.StandardCode.PROJECT_FUNDING));
            rptViewHeader.DataSource = lstFundType.OrderBy(x => x.StandardCodeID);
            rptViewHeader.DataBind();

            List<vProposedBudgetDt> lstEntity = BusinessLayer.GetvProposedBudgetDtList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();

            txtTotalProjectBudget.Text = lstEntity.Sum(x => x.TotalAmount).ToString("N");
        }

        protected void grdView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vProposedBudgetDt entity = e.Item.DataItem as vProposedBudgetDt;
                Repeater rptViewItem = e.Item.FindControl("rptViewItem") as Repeater;
                String[] lst = entity.ListFund.Split('|');
                rptViewItem.DataSource = lst;
                rptViewItem.DataBind();
            }

            if (grdView.Items.Count > 0)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    HtmlTableRow trEmpty = (HtmlTableRow)e.Item.FindControl("trEmpty");
                    trEmpty.Style.Add("Display", "none");
                }
            }
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
        #endregion

        #region Process Detail
        private void ControlToEntity(ProposedBudgetHd entity) 
        {
            entity.TeamDtID = Convert.ToInt32(hdnTeamDtID.Value);
            entity.Remarks = txtRemarks.Text;
            entity.TotalAmount = Convert.ToDecimal(Request.Form[txtTotalProjectBudget.UniqueID]);
            entity.ProposedBudgetDate = Helper.GetDatePickerValue(txtProposedBudgetDate.Text);
        }

        private void ControlToEntity(ProposedBudgetDt entity) 
        {
            entity.ProposedBudgetCode = txtProposedBudgetCode.Text;
            entity.ProposedBudgetName = txtProposedBudgetName.Text;
            if (txtRealizationDate.Text != "")
                entity.RealizationDate = Helper.GetDatePickerValue(txtRealizationDate.Text);
            else
                entity.RealizationDate = null;
            entity.TotalAmount = Convert.ToDecimal(Request.Form[txtTotalLineAmount.UniqueID]);
            entity.Remarks = txtEntryRemarks.Text;
        }

        public void SaveHeader(IDbContext ctx, ref string retval)
        {
            if (retval == "")
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
                
                int ProposedBudgetID = BusinessLayer.GetProposedBudgetHdMaxID(ctx);
                retval = ProposedBudgetID.ToString();
            }
        }

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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
                if (OnDeleteRecordEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        public bool OnSaveEditRecordEntityDt(ref string errMessage) 
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ProposedBudgetDtDao entityDtDao = new ProposedBudgetDtDao(ctx);
            ProposedBudgetDtFundDao fundDao = new ProposedBudgetDtFundDao(ctx);

            try
            {
                ProposedBudgetDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entityDt);
                entityDtDao.Update(entityDt);

                List<ProposedBudgetDtFund> lstFund = BusinessLayer.GetProposedBudgetDtFundList(String.Format("ProposedBudgetDtID = {0}", entityDt.ProposedBudgetDtID));
                String[] data = hdnLstFundItem.Value.Split('|');
                int count = 0;

                foreach (ProposedBudgetDtFund obj in lstFund)
                {
                    obj.Amount = Convert.ToDecimal(data[count]);
                    obj.LastUpdatedBy = AppSession.UserLogin.UserID;
                    fundDao.Update(obj);
                    count++;
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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

        public bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ProposedBudgetDtDao entityDtDao = new ProposedBudgetDtDao(ctx);
            ProposedBudgetDtFundDao fundDao = new ProposedBudgetDtFundDao(ctx);

            try
            {
                String retval = hdnID.Value;
                SaveHeader(ctx, ref retval);
                int ProposedBudgetID = Convert.ToInt32(retval);
                
                ProposedBudgetDt entityDt = new ProposedBudgetDt();
                ControlToEntity(entityDt);
                entityDt.ProposedBudgetID = ProposedBudgetID;
                entityDtDao.Insert(entityDt);

                int ProposedBudgetDtID = BusinessLayer.GetProposedBudgetDtMaxID(ctx);
                List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(String.Format("IsDeleted = 0 AND IsActive = 1 AND ParentID = '{0}'", Constant.StandardCode.PROJECT_FUNDING));
                String[] data = hdnLstFundItem.Value.Split('|');
                int count = 0;

                foreach (StandardCode obj in lstStandardCode) 
                {
                    ProposedBudgetDtFund item = new ProposedBudgetDtFund();
                    item.ProposedBudgetDtID = ProposedBudgetDtID;
                    item.GCProjectFundType = obj.StandardCodeID;
                    item.Amount = Convert.ToDecimal(data[count]);
                    item.CreatedBy = AppSession.UserLogin.UserID;
                    fundDao.Insert(item);
                    count++;
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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

        public bool OnDeleteRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            try
            {
                ProposedBudgetDt entity = BusinessLayer.GetProposedBudgetDt(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateProposedBudgetDt(entity);
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            
            try
            {
                SaveHeader(ctx, ref retval);
                ctx.CommitTransaction();
            }
            catch (Exception ex) 
            {
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
                    entityDt.IsDeleted = true;
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Update(entityDt);
                }
                entityDao.Update(entity);

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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
            try
            {
                ProposedBudgetHd entity = BusinessLayer.GetProposedBudgetHd(Convert.ToInt32(hdnID.Value));
                entity.GCProposedBudgetStatus = Constant.ProjectStatus.PROPOSED;
                BusinessLayer.UpdateProposedBudgetHd(entity);
            }
            catch (Exception ex) 
            {
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }
        #endregion

    }
}