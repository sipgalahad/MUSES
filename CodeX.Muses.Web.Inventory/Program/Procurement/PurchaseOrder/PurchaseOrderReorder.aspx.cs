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
    public partial class PurchaseOrderReorder : BasePageTrx
    {
        private string[] lstSelectedMember = null;
        private string[] lstQtyPurchaseOrder = null;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.REORDER_PURCHASE_ORDER;
        }

        #region Html Getter
        protected string OnGetFilterExpressionLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.PURCHASE_ORDER);
        }
        protected string OnGetFilterExpressionSupplier()
        {
            return string.Format("GCBusinessPartnerType = '{0}' AND IsDeleted = 0", Constant.BusinessObjectType.SUPPLIER);
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
            List<GetLocationUserList> lstLocation = BusinessLayer.GetLocationUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.PURCHASE_ORDER, "");
            if (lstLocation.Count == 1)
            {
                trLocation.Style.Add("display", "none");
                hdnLocationID.Value = lstLocation[0].LocationID.ToString();
                txtLocationCode.Text = lstLocation[0].LocationCode;
                txtLocationName.Text = lstLocation[0].LocationName;
            }
            SetControlProperties();

            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();
            int PageCount = 1;
            int RowCount = 1;

            BindGridView(1, true, ref PageCount, ref RowCount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> listStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}','{2}') AND IsDeleted = 0", Constant.StandardCode.PURCHASE_ORDER_TYPE, Constant.StandardCode.FRANCO_REGION, Constant.StandardCode.CURRENCY_CODE));
            List<Term> listTerm = BusinessLayer.GetTermList(string.Format("isDeleted = 0"));
            Methods.SetComboBoxField<StandardCode>(cboPurchaseOrderType, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.PURCHASE_ORDER_TYPE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboFrancoRegion, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.FRANCO_REGION).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboCurrency, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.CURRENCY_CODE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<Term>(cboTerm, listTerm, "TermName", "TermID");
            cboPurchaseOrderType.SelectedIndex = 0;
            cboFrancoRegion.SelectedIndex = 0;
            cboCurrency.SelectedIndex = 0;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtItemOrderDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(txtItemOrderDeliveryDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(txtItemOrderExpiredDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(hdnSupplierID, new ControlEntrySetting(true, true, true,"0"));
            SetControlEntrySetting(txtSupplierCode, new ControlEntrySetting(true, true, true));
            
            SetControlEntrySetting(cboPurchaseOrderType, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(cboTerm, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(cboFrancoRegion, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(cboCurrency, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtKurs, new ControlEntrySetting(true, true, true, "1.00"));
        }

        #region Load
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vItemBalanceInventory entity = e.Row.DataItem as vItemBalanceInventory;
                TextBox txtPurchaseOrder = e.Row.FindControl("txtPurchaseOrder") as TextBox;
                CheckBox chkIsSelected = (CheckBox)e.Row.FindControl("chkIsSelected");
                Decimal autoQty = (entity.QuantityMAX - entity.QuantityEND -entity.PurchaseOrderQtyOnOrder);
                if(autoQty < 0)autoQty = 0;
                txtPurchaseOrder.Text = autoQty.ToString("N");
                if (lstSelectedMember.Contains(entity.ID.ToString()))
                {
                    int idx = Array.IndexOf(lstSelectedMember, entity.ID.ToString());
                    chkIsSelected.Checked = true;
                    txtPurchaseOrder.ReadOnly = false;
                    txtPurchaseOrder.Text = lstQtyPurchaseOrder[idx];
                }
            }
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = string.Format("LocationID = {0} AND QuantityEND <= QuantityMIN AND IsDeleted = 0", hdnLocationID.Value);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvItemBalanceRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            lstQtyPurchaseOrder = hdnPurchaseOrder.Value.Split(',');

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
        public void SavePurchaseOrderHd(IDbContext ctx, ref int purchaseOrderID, ref string retval, decimal Total)
        {
            PurchaseOrderHdDao entityHdDao = new PurchaseOrderHdDao(ctx);
            PurchaseOrderHd entityHd = new PurchaseOrderHd();
            entityHd.OrderDate = Helper.GetDatePickerValue(txtItemOrderDate.Text);
            entityHd.DeliveryDate = Helper.GetDatePickerValue(txtItemOrderDeliveryDate.Text);
            entityHd.POExpiredDate = Helper.GetDatePickerValue(txtItemOrderExpiredDate.Text);
            entityHd.GCPurchaseOrderType = cboPurchaseOrderType.Value.ToString();
            entityHd.TermID = Convert.ToInt32(cboTerm.Value.ToString());
            entityHd.BusinessPartnerID = Convert.ToInt32(hdnSupplierID.Value);
            entityHd.LocationID = Convert.ToInt32(hdnLocationID.Value);
            entityHd.GCFrancoRegion = cboFrancoRegion.Value.ToString();
            entityHd.GCCurrencyCode = cboCurrency.Value.ToString();
            entityHd.CurrencyRate = Convert.ToDecimal(txtKurs.Text);
            entityHd.IsIncludeVAT = false;
            entityHd.TransactionAmount = Total;
            entityHd.PurchaseOrderNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.PURCHASE_ORDER, entityHd.OrderDate, ctx);
            entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
            entityHd.FinalDiscount = Convert.ToDecimal(0.00);
            //entityHd.TaxAmount = Convert.ToDecimal(0.00);
            entityHd.DownPaymentAmount = Convert.ToDecimal(0.00);
            ctx.CommandType = CommandType.Text;
            ctx.Command.Parameters.Clear();
            entityHd.CreatedBy = AppSession.UserLogin.UserID;
            entityHdDao.Insert(entityHd);
            purchaseOrderID = BusinessLayer.GetPurchaseOrderHdMaxID(ctx);
            retval = entityHd.PurchaseOrderNo;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage, ref string retval)
        {
            bool result = true;
            String[] paramID = hdnSelectedMember.Value.Substring(1).Split(',');
            String[] paramPurchaseOrder = hdnPurchaseOrder.Value.Substring(1).Split(',');
            IDbContext ctx = DbFactory.Configure(true);
            int purchaseOrderID = 0;
            PurchaseOrderDtDao entityPurchaseOrderDtDao = new PurchaseOrderDtDao(ctx);
            try
            {
                List<PurchaseOrderDt> lstPurchaseOrderDt = new List<PurchaseOrderDt>();
                for (int ct = 0; ct < paramID.Length; ct++)
                {
                    vItemBalance entityItemBalance = BusinessLayer.GetvItemBalanceList(string.Format("ID = {0}", paramID[ct]), ctx)[0];
                    PurchaseOrderDt entityPurchaseOrderDt = new PurchaseOrderDt();
                    entityPurchaseOrderDt.ItemID = entityItemBalance.ItemID;
                    entityPurchaseOrderDt.Quantity = Convert.ToDecimal(paramPurchaseOrder[ct]);
                    entityPurchaseOrderDt.GCPurchaseUnit = entityItemBalance.GCItemUnit;
                    entityPurchaseOrderDt.GCBaseUnit = entityItemBalance.GCItemUnit;
                    entityPurchaseOrderDt.ConversionFactor = Convert.ToDecimal("1.00");
                    entityPurchaseOrderDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;

                    GetItemMasterPurchase itemPurchase = BusinessLayer.GetItemMasterPurchaseList(AppSession.UserLogin.SiteID, entityItemBalance.ItemID, Convert.ToInt32(hdnSupplierID.Value), ctx).FirstOrDefault();
                    if (itemPurchase != null)
                    {
                        entityPurchaseOrderDt.UnitPrice = itemPurchase.Price;
                        entityPurchaseOrderDt.DiscountPercentage1 = itemPurchase.Discount;
                        //entityPurchaseOrderDt.GCPurchaseUnit = itemPurchase.PurchaseUnit;
                        //entityPurchaseOrderDt.ConversionFactor = itemPurchase.ConversionFactor;
                        //entityPurchaseOrderDt.UnitPrice = itemPurchase.Price * itemPurchase.ConversionFactor;
                    }
                    ctx.CommandType = CommandType.Text;
                    ctx.Command.Parameters.Clear();

                    entityPurchaseOrderDt.CreatedBy = AppSession.UserLogin.UserID;
                    lstPurchaseOrderDt.Add(entityPurchaseOrderDt);
                }
                SavePurchaseOrderHd(ctx, ref purchaseOrderID, ref retval, lstPurchaseOrderDt.Sum(p => p.CustomSubTotal));
                foreach (PurchaseOrderDt entityPurchaseOrderDt in lstPurchaseOrderDt)
                {
                    entityPurchaseOrderDt.PurchaseOrderID = purchaseOrderID;
                    entityPurchaseOrderDtDao.Insert(entityPurchaseOrderDt);
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