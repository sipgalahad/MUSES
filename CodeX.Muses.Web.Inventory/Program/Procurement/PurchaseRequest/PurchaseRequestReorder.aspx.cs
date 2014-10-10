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
    public partial class PurchaseRequestReorder : BasePageTrx
    {
        private string[] lstSelectedMember = null;
        private string[] lstQtyPurchaseRequest = null;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.REORDER_PURCHASE_REQUEST;
        }

        #region Html Getter
        protected string OnGetFilterExpressionLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.PURCHASE_ORDER);
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
            SetControlEntrySetting(hdnLocationID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtLocationCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtLocationName, new ControlEntrySetting(false, false, true));

            SetControlEntrySetting(txtPurchaseRequestDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(txtPurchaseRequestTime, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.TIME_FORMAT)));
            SetControlEntrySetting(txtNotes, new ControlEntrySetting(true, true, false));
        }

        #region Load
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vItemBalanceInventory entity = e.Row.DataItem as vItemBalanceInventory;
                TextBox txtPurchaseRequest = e.Row.FindControl("txtPurchaseRequest") as TextBox;
                CheckBox chkIsSelected = (CheckBox)e.Row.FindControl("chkIsSelected");
                Decimal autoQty = (entity.QuantityMAX - entity.QuantityEND - entity.PurchaseRequestQtyOnOrder);
                if (autoQty < 0) autoQty = 0;
                txtPurchaseRequest.Text = autoQty.ToString("N");
                if (lstSelectedMember.Contains(entity.ID.ToString()))
                {
                    int idx = Array.IndexOf(lstSelectedMember, entity.ID.ToString());
                    chkIsSelected.Checked = true;
                    txtPurchaseRequest.ReadOnly = false;
                    txtPurchaseRequest.Text = lstQtyPurchaseRequest[idx];
                }
            }
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnLocationID.Value != "")
                filterExpression = string.Format("LocationID = {0} AND QuantityEND <= QuantityMIN AND IsDeleted = 0", hdnLocationID.Value);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvItemBalanceRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split('|');
            lstQtyPurchaseRequest = hdnPurchaseRequest.Value.Split('|');
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
        public void SavePurchaseRequestHd(IDbContext ctx, ref int purchaseReqID, ref string retval)
        {
            PurchaseRequestHdDao entityHdDao = new PurchaseRequestHdDao(ctx);
            PurchaseRequestHd entityHd = new PurchaseRequestHd();
            entityHd.FromLocationID = Convert.ToInt32(hdnLocationID.Value);
            entityHd.TransactionDate = Helper.GetDatePickerValue(txtPurchaseRequestDate.Text);
            entityHd.TransactionTime = txtPurchaseRequestTime.Text;
            entityHd.Remarks = txtNotes.Text;
            entityHd.PurchaseRequestNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.PURCHASE_REQUEST, entityHd.TransactionDate, ctx);
            retval = entityHd.PurchaseRequestNo;
            entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
            ctx.CommandType = CommandType.Text;
            ctx.Command.Parameters.Clear();
            entityHd.CreatedBy = AppSession.UserLogin.UserID;
            entityHdDao.Insert(entityHd);
            purchaseReqID = BusinessLayer.GetPurchaseRequestHdMaxID(ctx);
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage, ref string retval)
        {
            bool result = true;
            String[] paramID = hdnSelectedMember.Value.Substring(1).Split('|');
            String[] paramPurchaseRequest = hdnPurchaseRequest.Value.Substring(1).Split('|');
            IDbContext ctx = DbFactory.Configure(true);
            int purchaseRequestID = 0;
            PurchaseRequestDtDao entityPurchaseRequestDtDao = new PurchaseRequestDtDao(ctx);
            try
            {
                SavePurchaseRequestHd(ctx, ref purchaseRequestID, ref retval);

                string lstID = "";
                foreach (String id in paramID)
                {
                    if (lstID != "")
                        lstID += ",";
                    lstID += id;
                }
                List<vItemBalance> lstEntityItemBalance = BusinessLayer.GetvItemBalanceList(string.Format("ID IN ({0})", lstID), ctx);

                string lstItemID = "";
                foreach (vItemBalance entityItemBalance in lstEntityItemBalance)
                {
                    if (lstItemID != "")
                        lstItemID += ",";
                    lstItemID += entityItemBalance.ItemID.ToString();
                }
                List<ItemPlanning> lstItemPlanning = BusinessLayer.GetItemPlanningList(string.Format("SiteID = '{0}' AND ItemID IN ({0}) AND IsDeleted = 0", AppSession.UserLogin.SiteID, lstItemID), ctx);
                for (int ct = 0; ct < paramID.Length; ct++)
                {
                    vItemBalance entityItemBalance = lstEntityItemBalance.FirstOrDefault(p => p.ID == Convert.ToInt32(paramID[ct]));
                    PurchaseRequestDt entityPurchaseReqDt = new PurchaseRequestDt();
                    entityPurchaseReqDt.PurchaseRequestID = purchaseRequestID;
                    entityPurchaseReqDt.ItemID = entityItemBalance.ItemID;
                    entityPurchaseReqDt.Quantity = Convert.ToDecimal(paramPurchaseRequest[ct]);
                    entityPurchaseReqDt.GCPurchaseUnit = entityItemBalance.GCItemUnit;
                    entityPurchaseReqDt.GCBaseUnit = entityItemBalance.GCItemUnit;
                    entityPurchaseReqDt.ConversionFactor = Convert.ToDecimal("1.00");
                    entityPurchaseReqDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;

                    ItemPlanning itemPlanning = lstItemPlanning.FirstOrDefault(p => p.ItemID == entityItemBalance.ItemID);
                    if (itemPlanning != null)
                        entityPurchaseReqDt.BusinessPartnerID = itemPlanning.BusinessPartnerID;
                    else
                        entityPurchaseReqDt.BusinessPartnerID = null;

                    int businessPartnerID = (entityPurchaseReqDt.BusinessPartnerID == null ? 0 : (int)entityPurchaseReqDt.BusinessPartnerID);                        
                    GetItemMasterPurchase itemPurchase = BusinessLayer.GetItemMasterPurchaseList(AppSession.UserLogin.SiteID, entityItemBalance.ItemID, businessPartnerID, ctx).FirstOrDefault();
                    if (itemPurchase != null)
                    {
                        entityPurchaseReqDt.UnitPrice = itemPurchase.Price;
                        entityPurchaseReqDt.DiscountPercentage = itemPurchase.Discount;
                    }
                    else
                    {
                        entityPurchaseReqDt.UnitPrice = Convert.ToDecimal(0.00);
                        entityPurchaseReqDt.DiscountPercentage = Convert.ToDecimal(0.00);
                    }
                    ctx.CommandType = CommandType.Text;
                    ctx.Command.Parameters.Clear();
                    entityPurchaseReqDt.CreatedBy = AppSession.UserLogin.UserID;
                    entityPurchaseRequestDtDao.Insert(entityPurchaseReqDt);
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