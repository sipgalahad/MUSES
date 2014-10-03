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
using CodeX.Common;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class StockConsumptionApprovalDetail : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        private string[] lstSelectedMember = null;

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.ITEM_CONSUMPTION_APPROVAL;
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
            hdnConsumptionID.Value = Page.Request.QueryString["id"];
            vItemTransactionHd entityItemRequest = BusinessLayer.GetvItemTransactionHdList(String.Format("TransactionID = {0}", Convert.ToInt32(hdnConsumptionID.Value)))[0];
            EntityToControl(entityItemRequest);
        }

        private void EntityToControl(vItemTransactionHd entity)
        {
            hdnConsumptionID.Value = entity.TransactionID.ToString();
            txtConsumptionNo.Text = entity.TransactionNo;
            txtConsumptionDate.Text = entity.TransactionDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            hdnLocationID.Value = entity.FromLocationID.ToString();
            txtLocationCode.Text = entity.FromLocationCode;
            txtLocationName.Text = entity.FromLocationName;

            txtConsumptionType.Text = entity.ConsumptionType;
            txtRemarks.Text = entity.Remarks;
            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vItemTransactionDt entity = e.Row.DataItem as vItemTransactionDt;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (entity.GCItemDetailStatus == Constant.TransactionStatus.APPROVED || lstSelectedMember.Contains(entity.ID.ToString()))
                    chkIsSelected.Checked = true;
            }
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnConsumptionID.Value != "")
                filterExpression = string.Format("TransactionID = {0} AND GCItemDetailStatus != '{1}'", hdnConsumptionID.Value, Constant.TransactionStatus.VOID);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvItemTransactionDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<vItemTransactionDt> lstEntity = BusinessLayer.GetvItemTransactionDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ItemName1 ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();

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

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ItemTransactionDtDao itemDtDao = new ItemTransactionDtDao(ctx);
            try
            {
                string filterExpressionSetDefaultDt = String.Format("TransactionID = {0} AND GCItemDetailStatus != '{1}'", hdnConsumptionID.Value, Constant.TransactionStatus.VOID);
                List<ItemTransactionDt> lstItemTransactionDtSetDefault = BusinessLayer.GetItemTransactionDtList(filterExpressionSetDefaultDt);

                string filterExpressionItemTransactionDt = String.Format("ID IN ({0})", hdnSelectedMember.Value.Substring(1));
                List<ItemTransactionDt> lstItemTransactionDt = BusinessLayer.GetItemTransactionDtList(filterExpressionItemTransactionDt);

                foreach (ItemTransactionDt itemDt in lstItemTransactionDtSetDefault)
                {
                    if (itemDt.GCItemDetailStatus == Constant.TransactionStatus.APPROVED && lstItemTransactionDt.Where(p => p.ID == itemDt.ID).Count() < 1)
                    {
                        itemDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                        itemDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        itemDtDao.Update(itemDt);
                    }
                }

                foreach (ItemTransactionDt itemDt in lstItemTransactionDt)
                {
                    itemDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
                    itemDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    itemDtDao.Update(itemDt);
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
    }
}