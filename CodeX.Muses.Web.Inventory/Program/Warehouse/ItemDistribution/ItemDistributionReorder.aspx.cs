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
using System.Web.UI.HtmlControls;
using CodeX.Common;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class ItemDistributionReorder : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            if (Page.Request.QueryString.Count > 0 && Page.Request.QueryString["type"] == "cs")
                return Constant.MenuCode.Inventory.REORDER_ITEM_DISTRIBUTION_CROSS_SITE;
            return Constant.MenuCode.Inventory.REORDER_ITEM_DISTRIBUTION; 
        }

        #region Html Getter
        protected string OnGetFilterExpressionFromLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.ITEM_DISTRIBUTION);
        }
        protected string OnGetFilterExpressionToLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.ITEM_REQUEST);
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
            SetControlEntrySetting(hdnFromLocationID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtFromLocationCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtFromLocationName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtNotes, new ControlEntrySetting(true, true, false));
            
            SetControlEntrySetting(txtToLocationCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtToLocationName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtItemOrderDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(txtItemOrderTime, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.TIME_FORMAT)));
        }

        #region Load
        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnToLocationID.Value != "" && hdnFromLocationID.Value != "")
                filterExpression = string.Format("LocationID = {0} AND QuantityEND <= QuantityMIN AND IsDeleted = 0 AND ItemID IN (SELECT ItemID FROM ItemBalance WHERE LocationID = {1} AND IsDeleted = 0)", hdnToLocationID.Value, hdnFromLocationID.Value);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvItemBalanceInventoryRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vItemBalanceInventory> lstEntity = BusinessLayer.GetvItemBalanceInventoryList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ItemName1 ASC");
            string lstItemID = string.Join(",", lstEntity.Select(p => p.ItemID));

            filterExpression = "1 = 0";
            if (hdnFromLocationID.Value != "" && lstItemID != "")
                filterExpression = string.Format("LocationID = {0} AND ItemID IN ({1}) AND IsDeleted = 0", hdnFromLocationID.Value, lstItemID);
            lstItemBalanceFromLocation = BusinessLayer.GetItemBalanceList(filterExpression);

            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        List<ItemBalance> lstItemBalanceFromLocation = null;
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vItemBalanceInventory entity = e.Row.DataItem as vItemBalanceInventory;
                HtmlInputText txtItemRequest = e.Row.FindControl("txtItemRequest") as HtmlInputText;

                ItemBalance itemBalanceFromLocation = lstItemBalanceFromLocation.FirstOrDefault(p => p.ItemID == entity.ItemID);
                if (itemBalanceFromLocation != null)
                {
                    decimal qty = (entity.QuantityMAX - entity.QuantityEND);
                    if(qty > itemBalanceFromLocation.QuantityEND)
                        qty = itemBalanceFromLocation.QuantityEND;

                    txtItemRequest.Attributes.Add("max", itemBalanceFromLocation.QuantityEND.ToString());
                    txtItemRequest.Value = (qty - entity.ItemDistributionQtyOnOrder).ToString();

                    HtmlGenericControl divFromLocationQty = (HtmlGenericControl)e.Row.FindControl("divFromLocationQty");
                    divFromLocationQty.InnerHtml = itemBalanceFromLocation.QuantityEND.ToString();
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

        #region Save
        public void SaveItemDistributionHd(IDbContext ctx, ref int distributionID, ref string retval)
        {
            ItemDistributionHdDao entityHdDao = new ItemDistributionHdDao(ctx);
            ItemDistributionHd entityHd = new ItemDistributionHd();
            entityHd.FromLocationID = Convert.ToInt32(hdnFromLocationID.Value);
            entityHd.ToLocationID = Convert.ToInt32(hdnToLocationID.Value);
            entityHd.DeliveryDate = Helper.GetDatePickerValue(txtItemOrderDate.Text);
            entityHd.DeliveryTime = txtItemOrderTime.Text;
            entityHd.DeliveryRemarks = txtNotes.Text;
            entityHd.TransactionCode = Constant.TransactionCode.ITEM_DISTRIBUTION;
            entityHd.DistributionNo = BusinessLayer.GenerateTransactionNo(entityHd.TransactionCode, entityHd.DeliveryDate, ctx);
            retval = entityHd.DistributionNo;
            entityHd.GCDistributionStatus = Constant.DistributionStatus.OPEN;
            ctx.CommandType = CommandType.Text;
            ctx.Command.Parameters.Clear();
            entityHd.CreatedBy = AppSession.UserLogin.UserID;
            entityHdDao.Insert(entityHd);
            distributionID = BusinessLayer.GetItemDistributionHdMaxID(ctx);
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage, ref string retval)
        {
            bool result = true;
            String[] paramID = hdnSelectedMember.Value.Substring(1).Split(',');
            String[] paramItemDistribution = hdnItemDistribution.Value.Substring(1).Split(',');
            IDbContext ctx = DbFactory.Configure(true);
            int distributionID = 0;
            ItemDistributionDtDao entityItemDistributionDtDao = new ItemDistributionDtDao(ctx);
            try
            {
                SaveItemDistributionHd(ctx, ref distributionID, ref retval);
                for (int ct = 0; ct < paramID.Length; ct++)
                {
                    vItemBalance entityItemBalance = BusinessLayer.GetvItemBalanceList(string.Format("ID = {0}",paramID[ct]),ctx)[0];
                    ItemDistributionDt entityItemDistributionDt = new ItemDistributionDt();
                    entityItemDistributionDt.DistributionID = distributionID;
                    entityItemDistributionDt.ItemID = entityItemBalance.ItemID;
                    entityItemDistributionDt.Quantity = Convert.ToDecimal(paramItemDistribution[ct]);
                    entityItemDistributionDt.GCItemUnit = entityItemBalance.GCItemUnit;
                    entityItemDistributionDt.GCBaseUnit = entityItemBalance.GCItemUnit;
                    entityItemDistributionDt.ConversionFactor = Convert.ToDecimal("1.00");
                    entityItemDistributionDt.GCItemDetailStatus = Constant.DistributionStatus.OPEN;
                    entityItemDistributionDt.CreatedBy = AppSession.UserLogin.UserID;
                    entityItemDistributionDtDao.Insert(entityItemDistributionDt);
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