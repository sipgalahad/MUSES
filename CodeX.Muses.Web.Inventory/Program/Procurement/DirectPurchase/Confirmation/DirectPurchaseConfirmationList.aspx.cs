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
using System.Data;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class DirectPurchaseConfirmationList : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.DIRECT_PURCHASE_CONFIRMED;
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
            List<SettingParameter> lstSettingParameter = BusinessLayer.GetSettingParameterList(string.Format("ParameterCode IN ('{0}')",
                                                                                               Constant.SettingParameter.IS_VAT_APPLIED_TO_AVERAGE_PRICE));

            hdnIsVATAppliedToAveragePrice.Value = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.IS_VAT_APPLIED_TO_AVERAGE_PRICE).ParameterValue;
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
            string filterExpression = "";
            if (cboViewType.Value != null && cboViewType.Value.ToString() == "0")
                filterExpression = String.Format("GCTransactionStatus IN ('{0}','{1}')", Constant.TransactionStatus.APPROVED, Constant.TransactionStatus.CLOSED);
            else if (cboViewType.Value != null && cboViewType.Value.ToString() == "1")
                filterExpression = String.Format("GCTransactionStatus = '{0}'", Constant.TransactionStatus.APPROVED);
            else
                filterExpression = String.Format("GCTransactionStatus = '{0}'", Constant.TransactionStatus.CLOSED);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvDirectPurchaseHdRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vDirectPurchaseHd> lstEntity = BusinessLayer.GetvDirectPurchaseHdList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "PurchaseDate DESC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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
            DirectPurchaseHdDao purchaseHdDao = new DirectPurchaseHdDao(ctx);
            DirectPurchaseDtDao purchaseDtDao = new DirectPurchaseDtDao(ctx);
            ItemPlanningDao itemPlanningDao = new ItemPlanningDao(ctx);
            try
            {
                DirectPurchaseHd purchaseReceiveHd = purchaseHdDao.Get(Convert.ToInt32(hdnID.Value));
                List<DirectPurchaseDt> lstDirectPurchaseDt = BusinessLayer.GetDirectPurchaseDtList(String.Format("DirectPurchaseID = {0} AND GCItemDetailStatus != '{1}'", hdnID.Value, Constant.TransactionStatus.VOID), ctx);

                List<String> lstRequiredPurchaseReceiveID = new List<String>();
                String lstItemID = String.Join(",", lstDirectPurchaseDt.Select(p => p.ItemID).ToList());

                string filterExpression = String.Format("SiteID = '{0}' AND ItemID IN ({1}) AND IsDeleted = 0", AppSession.UserLogin.SiteID, lstItemID);
                List<ItemPlanning> lstItemPlanning = BusinessLayer.GetItemPlanningList(filterExpression, ctx);
                List<ItemMaster> lstItemMaster = BusinessLayer.GetItemMasterList(string.Format("ItemID IN ({0})", lstItemID), ctx);
                String lstProductLineID = String.Join(",", lstItemMaster.Select(p => p.ProductLineID).ToList());
                List<ProductLine> lstProductLine = BusinessLayer.GetProductLineList(string.Format("ProductLineID IN (SELECT ProductLineID FROM ItemProduct WHERE ItemID IN ({0}) AND ProductLineID IS NOT NULL)", lstItemID), ctx);

                string tempID = string.Format("D{0}", hdnID.Value);
                foreach (ItemPlanning itemPlanning in lstItemPlanning)
                {
                    if (itemPlanning.ListPendingPurchaseReceiveID != "")
                    {
                        string temp = itemPlanning.ListPendingPurchaseReceiveID.Substring(1, itemPlanning.ListPendingPurchaseReceiveID.Length - 2);
                        string[] lstPendingDirectPurchaseID = temp.Split(new string[] { "||" }, StringSplitOptions.None);
                        if (lstPendingDirectPurchaseID[0] != tempID)
                        {
                            if (lstRequiredPurchaseReceiveID.Count(p => p == lstPendingDirectPurchaseID[0]) == 0)
                                lstRequiredPurchaseReceiveID.Add(lstPendingDirectPurchaseID[0]);
                        }
                    }
                }
                if (lstRequiredPurchaseReceiveID.Count > 0)
                {
                    string lstPurchaseReceiveID = String.Join(",", lstRequiredPurchaseReceiveID.Where(p => !p.Contains("D")).Select(p => p).ToList());
                    String lstPurchaseReceiveNo = "";
                    if (lstPurchaseReceiveID != "")
                    {
                        List<PurchaseReceiveHd> lstRequiredPurchaseReceiveHd = BusinessLayer.GetPurchaseReceiveHdList(String.Format("PurchaseReceiveID IN ({0})", lstPurchaseReceiveID), ctx);
                        lstPurchaseReceiveNo = String.Join(",", lstRequiredPurchaseReceiveHd.Select(p => string.Format("<b>{0}</b>", p.PurchaseReceiveNo)).ToList());
                    }

                    string lstDirectPurchaseID = String.Join(",", lstRequiredPurchaseReceiveID.Where(p => p.Contains("D")).Select(p => p.Substring(1)).ToList());
                    String lstDirectPurchaseNo = "";
                    if (lstDirectPurchaseID != "")
                    {
                        List<DirectPurchaseHd> lstRequiredDirectPurchaseHd = BusinessLayer.GetDirectPurchaseHdList(String.Format("DirectPurchaseID IN ({0})", lstDirectPurchaseID), ctx);
                        lstDirectPurchaseNo = String.Join(",", lstRequiredDirectPurchaseHd.Select(p => string.Format("<b>{0}</b>", p.DirectPurchaseNo)).ToList());
                    }
                    if (lstPurchaseReceiveNo != "")
                    {
                        errMessage = string.Format("Harap Proses Penerimaan Dengan Nomor {0} Terlebih Dahulu", lstPurchaseReceiveNo);
                        if (lstDirectPurchaseNo != "")
                            errMessage += "<br/>";
                    }
                    if (lstDirectPurchaseNo != "")
                        errMessage += string.Format("Harap Pembelian Tunai Dengan Nomor {0} Terlebih Dahulu", lstDirectPurchaseNo);
                    result = false;
                }
                else
                {
                    purchaseReceiveHd.GCTransactionStatus = Constant.TransactionStatus.CLOSED;
                    purchaseReceiveHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    purchaseHdDao.Update(purchaseReceiveHd);

                    foreach (DirectPurchaseDt purchaseDt in lstDirectPurchaseDt)
                    {
                        decimal qtyEnd = purchaseDt.QtyBeforeApproved;
                        purchaseDt.QtyBeforeApproved = 0;
                        purchaseDt.GCItemDetailStatus = Constant.TransactionStatus.CLOSED;
                        purchaseDt.LastUpdatedBy = AppSession.UserLogin.UserID;

                        ItemPlanning entityItemPlanning = lstItemPlanning.Where(x => x.ItemID == purchaseDt.ItemID).FirstOrDefault();
                        ItemMaster entityItemMaster = lstItemMaster.Where(x => x.ItemID == purchaseDt.ItemID).FirstOrDefault();

                        bool isVATAppliedToAveragePrice = hdnIsVATAppliedToAveragePrice.Value == "1";
                        if (entityItemMaster.ProductLineID != null)
                        {
                            ProductLine productLine = lstProductLine.Where(x => x.ProductLineID == entityItemMaster.ProductLineID).FirstOrDefault();
                            isVATAppliedToAveragePrice = productLine.IsIncludeVAT;
                        }

                        decimal qtyPurchase = (purchaseDt.Quantity * purchaseDt.ConversionFactor);
                        decimal purchasePrice = purchaseDt.LineAmount;
                        if (isVATAppliedToAveragePrice)
                        {
                            if (purchaseReceiveHd.IsIncludeVAT)
                                purchasePrice = purchasePrice * (100 + purchaseReceiveHd.VATPercentage) / 100;
                        }
                        if ((qtyEnd + qtyPurchase) > 0)
                            entityItemPlanning.AveragePrice = ((entityItemPlanning.AveragePrice * qtyEnd) + (purchasePrice)) / (qtyEnd + qtyPurchase);
                        else
                            entityItemPlanning.AveragePrice = 0;
                        entityItemPlanning.ListPendingPurchaseReceiveID = entityItemPlanning.ListPendingPurchaseReceiveID.Replace(string.Format("|D{0}|", purchaseDt.DirectPurchaseID), "");
                        entityItemPlanning.LastUpdatedBy = AppSession.UserLogin.UserID;
                        itemPlanningDao.Update(entityItemPlanning);
                        purchaseDtDao.Update(purchaseDt);
                        BusinessLayer.UpdateChargesCostAmountDirectPurchase(purchaseReceiveHd.DirectPurchaseID, purchaseDt.ItemID, entityItemPlanning.AveragePrice, true, ctx);
                        ctx.CommandType = CommandType.Text;
                        ctx.Command.Parameters.Clear();
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