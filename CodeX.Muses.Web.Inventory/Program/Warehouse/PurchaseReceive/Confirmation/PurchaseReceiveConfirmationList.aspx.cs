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
    public partial class PurchaseReceiveConfirmationList : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.PURCHASE_RECEIVE_CONFIRMED;
        }

        protected string OnGetTransactionStatusApproved()
        {
            return Constant.TransactionStatus.APPROVED;
        }
        public override void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowEdit = IsAllowDelete = false;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<SettingParameter> lstSettingParameter = BusinessLayer.GetSettingParameterList(string.Format("ParameterCode IN ('{0}','{1}')",
                                                                                               Constant.SettingParameter.IS_DISCOUNT_APPLIED_TO_AVERAGE_PRICE,
                                                                                               Constant.SettingParameter.IS_DISCOUNT_APPLIED_TO_UNIT_PRICE));

            hdnIsDiscountAppliedToAveragePrice.Value = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.IS_DISCOUNT_APPLIED_TO_AVERAGE_PRICE).ParameterValue;
            hdnIsDiscountAppliedToUnitPrice.Value = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.IS_DISCOUNT_APPLIED_TO_UNIT_PRICE).ParameterValue;
            List<Variable> lstVariable = new List<Variable>();
            lstVariable.Add(new Variable { Code = "0", Value = "Semua" });
            lstVariable.Add(new Variable { Code = "1", Value = "Belum Diproses" });
            lstVariable.Add(new Variable { Code = "2", Value = "Sudah Diproses" });
            Methods.SetComboBoxField<Variable>(cboViewType, lstVariable, "Value", "Code");
            cboViewType.Value = "1";

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += String.Format("TransactionCode = '{0}'", Constant.TransactionCode.PURCHASE_RECEIVE);
            if (cboViewType.Value.ToString() == "0")
                filterExpression += String.Format(" AND GCTransactionStatus IN ('{0}','{1}')", Constant.TransactionStatus.APPROVED, Constant.TransactionStatus.PROCESSED);
            else if (cboViewType.Value.ToString() == "1")
                filterExpression += String.Format(" AND GCTransactionStatus = '{0}'", Constant.TransactionStatus.APPROVED);
            else
                filterExpression += String.Format(" AND GCTransactionStatus = '{0}'", Constant.TransactionStatus.PROCESSED);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvPurchaseReceiveHdRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vPurchaseReceiveHd> lstEntity = BusinessLayer.GetvPurchaseReceiveHdList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ReceivedDate DESC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vPurchaseReceiveHd entity = e.Row.DataItem as vPurchaseReceiveHd;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (entity.GCTransactionStatus != Constant.TransactionStatus.APPROVED)
                    chkIsSelected.Visible = false;
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

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseReceiveHdDao purchaseHdDao = new PurchaseReceiveHdDao(ctx);
            PurchaseReceiveDtDao purchaseDtDao = new PurchaseReceiveDtDao(ctx);
            ItemPlanningDao itemPlanningDao = new ItemPlanningDao(ctx);
            try
            {
                string filterExpressionPurchaseReceiveHd = String.Format("PurchaseReceiveID IN ({0})", hdnParam.Value);
                List<PurchaseReceiveHd> lstPurchaseReceiveHd = BusinessLayer.GetPurchaseReceiveHdList(filterExpressionPurchaseReceiveHd, ctx);
                foreach (PurchaseReceiveHd purchaseHd in lstPurchaseReceiveHd)
                {
                    purchaseHd.GCTransactionStatus = Constant.TransactionStatus.PROCESSED;
                    purchaseHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    purchaseHdDao.Update(purchaseHd);
                }

                filterExpressionPurchaseReceiveHd = String.Format("PurchaseReceiveID IN ({0}) AND GCItemDetailStatus != '{1}'", hdnParam.Value, Constant.TransactionStatus.VOID);
                List<PurchaseReceiveDt> lstPurchaseReceiveDt = BusinessLayer.GetPurchaseReceiveDtList(filterExpressionPurchaseReceiveHd, ctx);
                foreach (PurchaseReceiveDt purchaseDt in lstPurchaseReceiveDt)
                {
                    purchaseDt.GCItemDetailStatus = Constant.TransactionStatus.PROCESSED;
                    purchaseDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    purchaseDtDao.Update(purchaseDt);
                }

                string lstPurchaseReceiveID = string.Join(",", lstPurchaseReceiveHd.Select(p => p.PurchaseReceiveID).ToList());
                if (lstPurchaseReceiveID != "")
                {
                    String lstItemID = String.Join(",", lstPurchaseReceiveDt.Select(p => p.ItemID).ToList());

                    string filterExpression = String.Format("SiteID = '{0}' AND ItemID IN ({1}) AND IsDeleted = 0", AppSession.UserLogin.SiteID, lstItemID);
                    List<ItemPlanning> lstItemPlanning = BusinessLayer.GetItemPlanningList(filterExpression, ctx);
                    filterExpression = String.Format("SiteID = '{0}' AND ItemID IN ({1}) AND LocationIsDeleted = 0 AND IsDeleted = 0", AppSession.UserLogin.SiteID, lstItemID);
                    List<vItemBalance> lstItemBalance = BusinessLayer.GetvItemBalanceList(filterExpression, ctx);
                    foreach (PurchaseReceiveHd purchaseHd in lstPurchaseReceiveHd)
                    {
                        List<PurchaseReceiveDt> lstPurchaseReceiveDt1 = lstPurchaseReceiveDt.Where(p => p.PurchaseReceiveID == purchaseHd.PurchaseReceiveID).ToList();
                        foreach (PurchaseReceiveDt purchaseDt in lstPurchaseReceiveDt1)
                        {
                            purchaseDt.GCItemDetailStatus = Constant.TransactionStatus.CLOSED;
                            purchaseDt.LastUpdatedBy = AppSession.UserLogin.UserID;

                            ItemPlanning entityItemPlanning = lstItemPlanning.Where(x => x.ItemID == purchaseDt.ItemID).FirstOrDefault();

                            decimal purchaseUnitPrice = purchaseDt.UnitPrice;
                            decimal unitPrice = 0;
                            if (hdnIsDiscountAppliedToAveragePrice.Value == "1")
                            {
                                decimal discountAmount1 = (purchaseUnitPrice * purchaseDt.DiscountPercentage1) / 100;
                                decimal discountAmount2 = ((purchaseUnitPrice - discountAmount1) * purchaseDt.DiscountPercentage2) / 100;
                                purchaseUnitPrice = purchaseUnitPrice - (discountAmount1 + discountAmount2);
                            }

                            unitPrice = purchaseUnitPrice / purchaseDt.ConversionFactor;
                            if (hdnIsDiscountAppliedToUnitPrice.Value == "1")
                            {
                                decimal discountAmount1 = (unitPrice * purchaseDt.DiscountPercentage1) / 100;
                                decimal discountAmount2 = ((unitPrice - discountAmount1) * purchaseDt.DiscountPercentage2) / 100;
                                unitPrice = unitPrice - (discountAmount1 + discountAmount2);
                            }

                            if (entityItemPlanning.LastPurchasePrice < unitPrice)
                            {
                                entityItemPlanning.LastPurchasePrice = unitPrice;
                                entityItemPlanning.PurchaseUnitPrice = purchaseUnitPrice;
                                if (entityItemPlanning.UnitPrice < unitPrice)
                                    entityItemPlanning.UnitPrice = unitPrice;
                            }

                            entityItemPlanning.LastBusinessPartnerID = purchaseHd.BusinessPartnerID;
                            entityItemPlanning.LastPurchaseDiscount = purchaseDt.DiscountPercentage1;

                            decimal qtyEnd = lstItemBalance.Where(p => p.ItemID == purchaseDt.ItemID).Sum(p => p.QuantityEND);
                            decimal tempQty = (qtyEnd + (purchaseDt.Quantity * purchaseDt.ConversionFactor));
                            if (tempQty > 0)
                                entityItemPlanning.AveragePrice = ((entityItemPlanning.AveragePrice * qtyEnd) + (purchaseUnitPrice * purchaseDt.Quantity)) / tempQty;
                            else
                                entityItemPlanning.AveragePrice = 0;

                            entityItemPlanning.LastUpdatedBy = AppSession.UserLogin.UserID;
                            itemPlanningDao.Update(entityItemPlanning);
                            purchaseDtDao.Update(purchaseDt);
                        }
                    }
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