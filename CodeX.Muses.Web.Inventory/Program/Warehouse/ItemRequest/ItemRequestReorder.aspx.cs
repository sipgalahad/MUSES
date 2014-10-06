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

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class ItemRequestReorder : BasePageTrx
    {
        private string[] lstSelectedMember = null;
        private string[] lstQtyItemRequest = null;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.REORDER_ITEM_REQUEST;
        }

        #region Html Getter
        protected string OnGetFilterExpressionFromLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.ITEM_REQUEST);
        }
        protected string OnGetFilterExpressionToLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.ITEM_DISTRIBUTION);
        }
        #endregion

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
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();
            int PageCount = 1;
            int RowCount = 1;

            BindGridView(1, true, ref PageCount, ref RowCount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnLocationIDFrom, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtLocationCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtLocationName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtNotes, new ControlEntrySetting(true, true, false));
            
            SetControlEntrySetting(txtLocationCodeTo, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtLocationNameTo, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtItemOrderDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(txtItemOrderTime, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.TIME_FORMAT)));
            
        }

        #region Load
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vItemBalanceInventory entity = e.Row.DataItem as vItemBalanceInventory;
                TextBox txtItemRequest = e.Row.FindControl("txtItemRequest") as TextBox;
                CheckBox chkIsSelected = (CheckBox)e.Row.FindControl("chkIsSelected");
                Decimal autoQty = (entity.QuantityMAX - entity.QuantityEND - entity.ItemRequestQtyOnOrder);
                if (autoQty < 0) autoQty = 0;
                txtItemRequest.Text = autoQty.ToString("N");
                if (lstSelectedMember.Contains(entity.ID.ToString()))
                {
                    int idx = Array.IndexOf(lstSelectedMember, entity.ID.ToString());
                    chkIsSelected.Checked = true;
                    txtItemRequest.ReadOnly = false;
                    txtItemRequest.Text = lstQtyItemRequest[idx];
                }
            }
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnLocationIDFrom.Value != "")
                filterExpression = string.Format("LocationID = {0} AND QuantityEND <= QuantityMIN AND IsDeleted = 0", hdnLocationIDFrom.Value);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvItemBalanceInventoryRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split('|');
            lstQtyItemRequest = hdnItemRequest.Value.Split('|');
            List<vItemBalanceInventory> lstEntity = BusinessLayer.GetvItemBalanceInventoryList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ItemName1 ASC");
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
        #endregion

        #region Save
        public void SaveItemRequestHd(IDbContext ctx, ref int itemReqID, ref string retval)
        {
            ItemRequestHdDao entityHdDao = new ItemRequestHdDao(ctx);
            ItemRequestHd entityHd = new ItemRequestHd();
            entityHd.FromLocationID = Convert.ToInt32(hdnLocationIDFrom.Value);
            entityHd.ToLocationID = Convert.ToInt32(hdnLocationIDTo.Value);
            entityHd.TransactionDate = Helper.GetDatePickerValue(txtItemOrderDate.Text);
            entityHd.TransactionTime = txtItemOrderTime.Text;
            entityHd.Remarks = txtNotes.Text;
            entityHd.ItemRequestNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.ITEM_REQUEST, entityHd.TransactionDate, ctx);
            retval = entityHd.ItemRequestNo;
            entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
            ctx.CommandType = CommandType.Text;
            ctx.Command.Parameters.Clear();
            entityHd.CreatedBy = AppSession.UserLogin.UserID;
            entityHdDao.Insert(entityHd);
            itemReqID = BusinessLayer.GetItemRequestHdMaxID(ctx);
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage, ref string retval)
        {
            bool result = true;
            String[] paramID = hdnSelectedMember.Value.Substring(1).Split('|');
            String[] paramItemRequest = hdnItemRequest.Value.Substring(1).Split('|');
            IDbContext ctx = DbFactory.Configure(true);
            int itemRequestID = 0;
            ItemRequestDtDao entityItemRequestDtDao = new ItemRequestDtDao(ctx);
            try
            {
                SaveItemRequestHd(ctx, ref itemRequestID, ref retval);
                for (int ct = 0; ct < paramID.Length; ct++)
                {
                    vItemBalance entityItemBalance = BusinessLayer.GetvItemBalanceList(string.Format("ID = {0}",paramID[ct]),ctx)[0];
                    ItemRequestDt entityItemReqDt = new ItemRequestDt();
                    entityItemReqDt.ItemRequestID = itemRequestID;
                    entityItemReqDt.ItemID = entityItemBalance.ItemID;
                    entityItemReqDt.Quantity = Convert.ToDecimal(paramItemRequest[ct]);
                    entityItemReqDt.GCItemUnit = entityItemBalance.GCItemUnit;
                    entityItemReqDt.GCBaseUnit = entityItemBalance.GCItemUnit;
                    entityItemReqDt.ConversionFactor = Convert.ToDecimal("1.00");
                    entityItemReqDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                    entityItemReqDt.CreatedBy = AppSession.UserLogin.UserID;
                    entityItemRequestDtDao.Insert(entityItemReqDt);
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
        #endregion
    }
}