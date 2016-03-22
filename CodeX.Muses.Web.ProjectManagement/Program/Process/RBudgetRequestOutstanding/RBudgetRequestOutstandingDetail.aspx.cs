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
    public partial class RBudgetRequestOutstandingDetail : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        private string[] lstSelectedMember = null;
        private string[] lstSaveValue = null;

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.RBUDGET_REQUEST_OUTSTANDING;
        }

        public override void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowEdit = IsAllowDelete = false;
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowAdd = false;
        }

        protected override void InitializeDataControl()
        {
            hdnRequestID.Value = Page.Request.QueryString["id"];
            vRBudgetRequestHd entityBudgetRequest = BusinessLayer.GetvRBudgetRequestHdList(string.Format("BudgetRequestID = {0}", hdnRequestID.Value))[0];

            EntityToControl(entityBudgetRequest);
        }

        private void EntityToControl(vRBudgetRequestHd entity)
        {
            hdnRequestID.Value = entity.BudgetRequestID.ToString();
            txtBudgetRequestNo.Text = entity.BudgetRequestNo;
            txtRequestDate.Text = entity.RequestDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtRequestTime.Text = entity.RequestTime;
            txtProjectName.Text = entity.ProjectName;
            txtProjectTaskGroup.Text = entity.ProjectTaskGroupName;
            txtDueDate.Text = entity.DueDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);

            txtNotes.Text = entity.Remarks;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnRequestID.Value != "")
                filterExpression = string.Format("BudgetRequestID = {0} AND GCItemDetailStatus = '{1}' AND IsDeleted = 0", hdnRequestID.Value, Constant.TransactionStatus.APPROVED);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetRBudgetRequestDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            if (lstFundType == null)
                lstFundType = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PROJECT_FUNDING));

            rptViewHeader.DataSource = lstFundType;
            rptViewHeader.DataBind();

            rptViewHeader2.DataSource = lstFundType;
            rptViewHeader2.DataBind();

            thNotProcessedHeader.ColSpan = thProcessedHeader.ColSpan = lstFundType.Count;

            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            lstSaveValue = hdnLstSaveValue.Value.Split('|');
            List<RBudgetRequestDt> lstEntity = BusinessLayer.GetRBudgetRequestDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "BudgetRequestDtName ASC");
            if (lstEntity.Count > 0)
            {
                string lstBudgetRequestDtID = string.Join(",", lstEntity.Select(p => p.BudgetRequestDtID).ToList());
                lstEntityFund = BusinessLayer.GetRBudgetRequestDtFundList(string.Format("BudgetRequestDtID IN ({0})", lstBudgetRequestDtID));
            }
            else
                lstEntityFund = new List<RBudgetRequestDtFund>();
            string lsItemID = string.Join(",", lstEntity.Select(p => p.BudgetRequestDtID).ToList());
            //if (lsItemID != "")
            //    lstBudgetRequestDtRealizationPerItem = BusinessLayer.GetvBudgetRequestDtRealizationPerItemList(string.Format("ItemID IN ({0})", lsItemID));
            //else
            //    lstBudgetRequestDtRealizationPerItem = new List<vBudgetRequestDtRealizationPerItem>();

            lvwView.DataSource = lstEntity;
            lvwView.DataBind();
        }

        List<RBudgetRequestDtFund> lstEntityFund = null;
        List<StandardCode> lstFundType = null;
        bool isChecked = true;
        string tempSaveValue = "";
        //List<vBudgetRequestDtRealizationPerItem> lstBudgetRequestDtRealizationPerItem = null;
        protected void lvwView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                RBudgetRequestDt entity = e.Item.DataItem as RBudgetRequestDt;
                CheckBox chkIsSelected = (CheckBox)e.Item.FindControl("chkIsSelected");
                if (lstSelectedMember.Contains(entity.BudgetRequestDtID.ToString()))
                {
                    int idx = Array.IndexOf(lstSelectedMember, entity.BudgetRequestDtID.ToString());
                    isChecked = true;
                    chkIsSelected.Checked = true;
                    tempSaveValue = lstSaveValue[idx];
                }
                else
                {
                    isChecked = false;
                    tempSaveValue = "";
                }

                Repeater rptViewItem = e.Item.FindControl("rptViewItem") as Repeater;
                rptViewItem.DataSource = lstFundType;
                rptViewItem.DataBind();
                Repeater rptViewItem2 = e.Item.FindControl("rptViewItem2") as Repeater;
                rptViewItem2.DataSource = lstFundType;
                rptViewItem2.DataBind();
            }
        }

        protected void rptViewItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                StandardCode entity = e.Item.DataItem as StandardCode;
                RBudgetRequestDt entityBudgetRequestDt = ((ListViewItem)e.Item.Parent.Parent).DataItem as RBudgetRequestDt;
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

        protected void rptViewItem2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                StandardCode entity = e.Item.DataItem as StandardCode;
                RBudgetRequestDt entityBudgetRequestDt = ((ListViewItem)e.Item.Parent.Parent).DataItem as RBudgetRequestDt;
                TextBox txtTotalAmount = e.Item.FindControl("txtTotalAmount") as TextBox;

                decimal totalAmount = 0;
                txtTotalAmount.Attributes.Add("GCProjectFundType", entity.StandardCodeID);
                RBudgetRequestDtFund entityFund = lstEntityFund.FirstOrDefault(p => p.BudgetRequestDtID == entityBudgetRequestDt.BudgetRequestDtID && p.GCProjectFundType == entity.StandardCodeID);
                if (entityFund != null)
                    totalAmount = entityFund.TotalAmount;
                else
                    totalAmount = 0;
                if (isChecked)
                {
                    txtTotalAmount.ReadOnly = false;
                    string[] lst = tempSaveValue.Split('%');
                    foreach (string s in lst)
                    {
                        string[] temp = s.Split(';');
                        if (temp[0] == entity.StandardCodeID)
                            totalAmount = Convert.ToDecimal(temp[1]);
                    }
                }
                txtTotalAmount.Text = totalAmount.ToString();
                txtTotalAmount.Attributes.Add("TotalAmount", totalAmount.ToString());

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

        public void SaveBudgetRealizationHd(IDbContext ctx, ref int realizationID, ref string realizationNo)
        {
            RBudgetRealizationHdDao entityHdDao = new RBudgetRealizationHdDao(ctx);
            RBudgetRealizationHd entityHd = new RBudgetRealizationHd();
            entityHd.BudgetRequestID = Convert.ToInt32(hdnRequestID.Value);
            entityHd.RealizationDate = Helper.GetDatePickerValue(txtRequestDate.Text);
            entityHd.RealizationTime = txtRequestTime.Text;
            entityHd.Remarks = string.Format("Realisasi untuk permintaan Nomor {0}", Request.Form[txtBudgetRequestNo.UniqueID]);
            entityHd.BudgetRealizationNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.BUDGET_REALIZATION, entityHd.RealizationDate, ctx);
            entityHd.GCTransactionStatus = Constant.DistributionStatus.ON_DELIVERY;
            ctx.CommandType = CommandType.Text;
            ctx.Command.Parameters.Clear();
            entityHd.CreatedBy = AppSession.UserLogin.UserID;
            realizationID = entityHdDao.Insert(entityHd);
            realizationNo = entityHd.BudgetRealizationNo;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage, ref string retval)
        {
            bool result = true;
            String[] paramID = hdnSelectedMember.Value.Substring(1).Split(',');
            String[] paramSaveValue = hdnLstSaveValue.Value.Substring(1).Split('|');

            string realizationNo = "";

            IDbContext ctx = DbFactory.Configure(true);
            int realizationID = 0;
            RBudgetRealizationDtDao entityBudgetRealizationDao = new RBudgetRealizationDtDao(ctx);
            RBudgetRealizationDtFundDao entityBudgetRealizationFundDao = new RBudgetRealizationDtFundDao(ctx);
            RBudgetRequestDtDao entityBudgetRequestDtDao = new RBudgetRequestDtDao(ctx);
            RBudgetRequestHdDao entityBudgetRequestHdDao = new RBudgetRequestHdDao(ctx);
            try
            {
                if (type == "approve")
                {
                    SaveBudgetRealizationHd(ctx, ref realizationID, ref realizationNo);
                    for (int ct = 0; ct < paramID.Length; ct++)
                    {
                        List<RBudgetRealizationDtFund> lstEntityFund = new List<RBudgetRealizationDtFund>();
                        string[] lstSaveValue = paramSaveValue[ct].Split('%');
                        foreach (String saveValue in lstSaveValue)
                        {
                            string[] temp = saveValue.Split(';');
                            RBudgetRealizationDtFund entityFund = new RBudgetRealizationDtFund();
                            entityFund.GCProjectFundType = temp[0];
                            entityFund.TotalAmount = Convert.ToDecimal(temp[1]);
                            lstEntityFund.Add(entityFund);
                        }

                        RBudgetRequestDt entityItemReqDt = entityBudgetRequestDtDao.Get(Convert.ToInt32(paramID[ct]));
                        RBudgetRealizationDt itemDt = new RBudgetRealizationDt();
                        entityItemReqDt.GCItemDetailStatus = Constant.TransactionStatus.PROCESSED;
                        itemDt.BudgetRealizationID = realizationID;
                        itemDt.BudgetRealizationDtName = entityItemReqDt.BudgetRequestDtName;
                        itemDt.TotalAmount = lstEntityFund.Sum(p => p.TotalAmount);
                        itemDt.GCItemDetailStatus = Constant.DistributionStatus.OPEN;
                        itemDt.CreatedBy = AppSession.UserLogin.UserID;
                        itemDt.BudgetRealizationDtID = entityBudgetRealizationDao.Insert(itemDt);

                        foreach (RBudgetRealizationDtFund entityFund in lstEntityFund)
                        {
                            entityFund.BudgetRealizationDtID = itemDt.BudgetRealizationDtID;
                            entityBudgetRealizationFundDao.Insert(entityFund);
                        }

                        entityItemReqDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityBudgetRequestDtDao.Update(entityItemReqDt);
                    }
                }
                else
                {
                    List<RBudgetRequestDt> lstEntityItemReqDt = BusinessLayer.GetRBudgetRequestDtList(string.Format("BudgetRequestDtID IN ({0})", hdnSelectedMember.Value.Substring(1)));
                    foreach (RBudgetRequestDt itemReq in lstEntityItemReqDt)
                    {
                        itemReq.GCItemDetailStatus = Constant.TransactionStatus.VOID;
                        itemReq.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityBudgetRequestDtDao.Update(itemReq);
                    }
                }

                int count = BusinessLayer.GetRBudgetRequestDtRowCount(string.Format("BudgetRequestID = {0} AND GCItemDetailStatus = '{1}' AND IsDeleted = 0", hdnRequestID.Value, Constant.TransactionStatus.APPROVED), ctx);
                retval = string.Format("{0}|{1}", count, realizationNo);
                if (count == 0)
                {
                    RBudgetRequestHd entityBudgetRequestHd = entityBudgetRequestHdDao.Get(Convert.ToInt32(hdnRequestID.Value));
                    if (type == "approve") entityBudgetRequestHd.GCTransactionStatus = Constant.TransactionStatus.CLOSED;
                    else entityBudgetRequestHd.GCTransactionStatus = Constant.TransactionStatus.VOID;
                    entityBudgetRequestHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityBudgetRequestHdDao.Update(entityBudgetRequestHd);
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
    }
}