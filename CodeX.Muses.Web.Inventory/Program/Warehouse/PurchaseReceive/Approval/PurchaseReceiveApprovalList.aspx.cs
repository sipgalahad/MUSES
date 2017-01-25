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
    public partial class PurchaseReceiveApprovalList : BasePageList
    {
        protected int PageCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.PURCHASE_RECEIVE_APPROVAL;
        }

        public override void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowEdit = IsAllowDelete = false;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            BindGridView(1, true, ref PageCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            hdnIsDiscountAppliedToUnitPrice.Value = BusinessLayer.GetSettingParameter(Constant.SettingParameter.IS_DISCOUNT_APPLIED_TO_UNIT_PRICE).ParameterValue;
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += String.Format("TransactionCode = '{0}' AND GCTransactionStatus = '{1}'", Constant.TransactionCode.PURCHASE_RECEIVE, Constant.TransactionStatus.WAIT_FOR_APPROVAL);

            int count = BusinessLayer.GetLocationUserRowCount(string.Format("UserID = {0} AND IsDeleted = 0", AppSession.UserLogin.UserID));

            if (count > 0)
                filterExpression += string.Format(" AND LocationID IN (SELECT LocationID FROM LocationUser WHERE UserID = {0} AND IsDeleted = 0)", AppSession.UserLogin.UserID);
            else
            {
                count = BusinessLayer.GetLocationUserRoleRowCount(string.Format("RoleID IN (SELECT RoleID FROM UserInRole WHERE UserID = {0} AND SiteID = '{1}') AND IsDeleted = 0", AppSession.UserLogin.UserID, AppSession.UserLogin.SiteID));
                if (count > 0)
                    filterExpression += string.Format(" AND LocationID IN (SELECT LocationID FROM LocationUserRole WHERE RoleID IN (SELECT RoleID FROM UserInRole WHERE UserID = {0} AND SiteID = '{1}') AND IsDeleted = 0)", AppSession.UserLogin.UserID, AppSession.UserLogin.SiteID);
            }

            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvPurchaseReceiveHdRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vPurchaseReceiveHd> lstEntity = BusinessLayer.GetvPurchaseReceiveHdList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ReceivedDate DESC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            if (type == "approve")
            {
                bool result = true;
                IDbContext ctx = DbFactory.Configure(true);
                PurchaseOrderHdDao purchaseOrderHdDao = new PurchaseOrderHdDao(ctx);
                PurchaseOrderDtDao purchaseOrderDtDao = new PurchaseOrderDtDao(ctx);
                PurchaseReceiveHdDao purchaseHdDao = new PurchaseReceiveHdDao(ctx);
                PurchaseReceiveDtDao purchaseDtDao = new PurchaseReceiveDtDao(ctx);
                ItemPlanningDao itemPlanningDao = new ItemPlanningDao(ctx);

                try
                {
                    PurchaseReceiveHd entity = purchaseHdDao.Get(Convert.ToInt32(hdnID.Value));
                    if (entity.GCTransactionStatus == Constant.TransactionStatus.OPEN || entity.GCTransactionStatus == Constant.TransactionStatus.WAIT_FOR_APPROVAL)
                    {
                        List<PurchaseReceiveDt> lstPurchaseReceiveDt = BusinessLayer.GetPurchaseReceiveDtList(String.Format("PurchaseReceiveID = {0} AND GCItemDetailStatus != '{1}'", entity.PurchaseReceiveID, Constant.TransactionStatus.VOID), ctx);

                        String lstItemID = String.Join(",", lstPurchaseReceiveDt.Select(p => p.ItemID).ToList());
                        string filterExpression = String.Format("SiteID = '{0}' AND ItemID IN ({1}) AND IsDeleted = 0", AppSession.UserLogin.SiteID, lstItemID);
                        List<ItemPlanning> lstItemPlanning = BusinessLayer.GetItemPlanningList(filterExpression, ctx);

                        List<PurchaseReceiveDt> lstPendingPurchaseReceiveDt = BusinessLayer.GetPurchaseReceiveDtList(String.Format("PurchaseReceiveID != {0} AND ItemID IN ({1}) AND GCItemDetailStatus IN ('{2}','{3}') AND QtyBeforeApproved != 0", entity.PurchaseReceiveID, lstItemID, Constant.TransactionStatus.OPEN, Constant.TransactionStatus.WAIT_FOR_APPROVAL), ctx);
                        if (lstPendingPurchaseReceiveDt.Count > 0)
                        {
                            List<PurchaseReceiveHd> lstRequiredPurchaseReceiveHd = BusinessLayer.GetPurchaseReceiveHdList(String.Format("PurchaseReceiveID IN ({0})", String.Join(",", lstPendingPurchaseReceiveDt.Select(p => p.PurchaseReceiveID).ToList())), ctx);

                            String lstPurchaseReceiveNo = String.Join(",", lstRequiredPurchaseReceiveHd.Select(p => string.Format("<b>{0}</b>", p.PurchaseReceiveNo)).ToList());
                            errMessage = string.Format("Harap Proses Penerimaan Dengan Nomor {0} Terlebih Dahulu", lstPurchaseReceiveNo);
                            result = false;
                        }
                        else
                        {
                            foreach (PurchaseReceiveDt purchaseDt in lstPurchaseReceiveDt)
                            {
                                purchaseDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
                                purchaseDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                                purchaseDtDao.Update(purchaseDt);

                                ItemPlanning entityItemPlanning = lstItemPlanning.Where(x => x.ItemID == purchaseDt.ItemID).FirstOrDefault();
                                decimal purchaseUnitPrice = purchaseDt.UnitPrice;
                                decimal unitPrice = 0;
                                if (hdnIsDiscountAppliedToUnitPrice.Value == "1")
                                {
                                    decimal discountAmount1 = (purchaseUnitPrice * purchaseDt.DiscountPercentage1) / 100;
                                    decimal discountAmount2 = ((purchaseUnitPrice - discountAmount1) * purchaseDt.DiscountPercentage2) / 100;
                                    purchaseUnitPrice = purchaseUnitPrice - (discountAmount1 + discountAmount2);
                                }
                                unitPrice = purchaseUnitPrice / purchaseDt.ConversionFactor;
                                if (entityItemPlanning.LastPurchasePrice < unitPrice)
                                {
                                    entityItemPlanning.LastPurchasePrice = unitPrice;
                                    if (entityItemPlanning.UnitPrice < unitPrice)
                                    {
                                        entityItemPlanning.UnitPrice = unitPrice;
                                        entityItemPlanning.PurchaseUnitPrice = purchaseUnitPrice;
                                    }
                                }
                                if (!entityItemPlanning.ListPendingPurchaseReceiveID.Contains(string.Format("|{0}|", entity.PurchaseReceiveID)))
                                {
                                    entityItemPlanning.ListPendingPurchaseReceiveID += string.Format("|{0}|", entity.PurchaseReceiveID);
                                    if (entityItemPlanning.ListPendingPurchaseReceiveID.Length > 1000)
                                        entityItemPlanning.ListPendingPurchaseReceiveID = entityItemPlanning.ListPendingPurchaseReceiveID.Substring(0, 1000);
                                }
                                entityItemPlanning.LastUpdatedBy = AppSession.UserLogin.UserID;
                                itemPlanningDao.Update(entityItemPlanning);
                            }
                            entity.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                            if (entity.ApprovedDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT) == Constant.ConstantDate.DEFAULT_NULL)
                                entity.ApprovedDate = DateTime.Now;
                            entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                            purchaseHdDao.Update(entity);
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
            else
            {
                bool result = true;
                IDbContext ctx = DbFactory.Configure(true);
                PurchaseReceiveHdDao purchaseHdDao = new PurchaseReceiveHdDao(ctx);
                PurchaseReceiveDtDao purchaseDtDao = new PurchaseReceiveDtDao(ctx);

                try
                {
                    PurchaseReceiveHd entity = purchaseHdDao.Get(Convert.ToInt32(hdnID.Value));
                    if (entity.GCTransactionStatus == Constant.TransactionStatus.APPROVED || entity.GCTransactionStatus == Constant.TransactionStatus.WAIT_FOR_APPROVAL)
                    {
                        List<PurchaseReceiveDt> lstPurchaseReceiveDt = BusinessLayer.GetPurchaseReceiveDtList(String.Format("PurchaseReceiveID = {0} AND GCItemDetailStatus != '{1}'", entity.PurchaseReceiveID, Constant.TransactionStatus.VOID), ctx);

                        List<String> lstRequiredPurchaseReceiveID = new List<String>();
                        String lstItemID = String.Join(",", lstPurchaseReceiveDt.Select(p => p.ItemID).ToList());

                        string filterExpression = String.Format("SiteID = '{0}' AND ItemID IN ({1}) AND IsDeleted = 0", AppSession.UserLogin.SiteID, lstItemID);
                        List<ItemPlanning> lstItemPlanning = BusinessLayer.GetItemPlanningList(filterExpression, ctx);

                        foreach (ItemPlanning itemPlanning in lstItemPlanning)
                        {
                            if (itemPlanning.ListPendingPurchaseReceiveID != "")
                            {
                                string temp = itemPlanning.ListPendingPurchaseReceiveID.Substring(1, itemPlanning.ListPendingPurchaseReceiveID.Length - 2);
                                string[] lstPendingPurchaseReceiveID = temp.Split(new string[] { "||" }, StringSplitOptions.None);
                                string prID = lstPendingPurchaseReceiveID.Last();
                                if (prID != hdnID.Value)
                                {
                                    if (lstRequiredPurchaseReceiveID.Count(p => p == prID) == 0)
                                        lstRequiredPurchaseReceiveID.Add(prID);
                                }
                            }
                        }

                        if (lstRequiredPurchaseReceiveID.Count > 0)
                        {
                            List<PurchaseReceiveHd> lstRequiredPurchaseReceiveHd = BusinessLayer.GetPurchaseReceiveHdList(String.Format("PurchaseReceiveID IN ({0})", String.Join(",", lstRequiredPurchaseReceiveID.Select(p => p).ToList())), ctx);

                            String lstPurchaseReceiveNo = String.Join(",", lstRequiredPurchaseReceiveHd.Select(p => string.Format("<b>{0}</b>", p.PurchaseReceiveNo)).ToList());
                            errMessage = string.Format("Harap Batalkan Penerimaan Dengan Nomor {0} Terlebih Dahulu", lstPurchaseReceiveNo);
                            result = false;
                        }
                        else
                        {
                            entity.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                            entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                            purchaseHdDao.Update(entity);
                            foreach (PurchaseReceiveDt purchaseDt in lstPurchaseReceiveDt)
                            {
                                purchaseDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                                purchaseDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                                purchaseDtDao.Update(purchaseDt);
                            }
                        }
                    }
                    else
                    {
                        result = false;
                        errMessage = "Transaksi Sudah Diproses. Tidak Bisa Dibuka Kembali";
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
}