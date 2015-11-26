using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common;
using CodeX.Data.Core.Dal;
using System.Data;
using CodeX.Common;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class PurchaseRequestQuickPicksCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;

        private PurchaseRequestEntry DetailPage
        {
            get { return (PurchaseRequestEntry)Page; }
        }
        protected string OnGetFilterExpressionItemProduct()
        {
            return string.Format("GCItemType = '{0}' AND IsDeleted = 0", Constant.ItemType.PRODUCT);
        }

        public override void InitializeDataControl(string param)
        {
            hdnParam.Value = param;
            string[] temp = param.Split('|');
            hdnTransactionID.Value = temp[0];
            hdnLstLocationID.Value = temp[1];
            hdnLstFilterLocationItemGroup.Value = temp[2];
            BindGridView(1, true, ref PageCount);
        }

        protected void cbpPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        private string GetFilterExpression()
        {
            string filterExpression = "";

            if (hdnItemGroupID.Value != "")
                filterExpression += string.Format("GCItemType = '{0}' AND ItemName1 LIKE '%{1}%' AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE DisplayPath LIKE '%/{2}/%') AND IsDeleted = 0", Constant.ItemType.PRODUCT, hdnFilterItem.Value, hdnItemGroupID.Value);
            else if (hdnLstFilterLocationItemGroup.Value == "")
                filterExpression += string.Format("GCItemType = '{0}' AND ItemName1 LIKE '%{1}%' AND IsDeleted = 0", Constant.ItemType.PRODUCT, hdnFilterItem.Value);
            else
                filterExpression += string.Format("GCItemType = '{0}' AND ItemName1 LIKE '%{1}%' AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE {2}) AND IsDeleted = 0", Constant.ItemType.PRODUCT, hdnFilterItem.Value, hdnLstFilterLocationItemGroup.Value);

            return filterExpression;
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vItemProduct entity = e.Row.DataItem as vItemProduct;
                vItemPlanningCustom itemPlanning = lstItemPlanning.FirstOrDefault(p => p.ItemID == entity.ItemID);
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                HtmlGenericControl divStock = e.Row.FindControl("divStock") as HtmlGenericControl;
                HtmlInputHidden hdnPurchaseUnit = e.Row.FindControl("hdnPurchaseUnit") as HtmlInputHidden;
                HtmlInputHidden hdnConversionFactor = e.Row.FindControl("hdnConversionFactor") as HtmlInputHidden;
                if (lstSelectedMember.Contains(entity.ItemID.ToString()))
                    chkIsSelected.Checked = true;

                divStock.InnerHtml = lstItemBalance.Where(p => p.ItemID == entity.ItemID).Sum(p => p.QuantityEND).ToString();
                hdnPurchaseUnit.Value = itemPlanning.PurchaseUnit;
                hdnConversionFactor.Value = itemPlanning.cfConversion;
            }
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (hdnTransactionID.Value != "0" && hdnTransactionID.Value != "")
            {
                List<PurchaseRequestDt> lstPurchaseRequestDt = BusinessLayer.GetPurchaseRequestDtList(string.Format("PurchaseRequestID = {0} AND GCItemDetailStatus != '{1}' AND IsDeleted = 0", hdnTransactionID.Value, Constant.TransactionStatus.VOID));
                string lstSelectedID = string.Join(",", lstPurchaseRequestDt.Select(p => p.ItemID).ToList());
                if (lstSelectedID != "")
                    filterExpression += string.Format(" AND ItemID NOT IN ({0})", lstSelectedID);
            }
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvItemProductRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 10);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<vItemProduct> lstEntity = BusinessLayer.GetvItemProductList(filterExpression, 10, pageIndex, "ItemName1 ASC");

            string lstItemID = string.Join(",", lstEntity.Select(p => p.ItemID).ToList());
            if (hdnLstLocationID.Value != "" && lstItemID != "")
                lstItemBalance = BusinessLayer.GetItemBalanceList(string.Format("LocationID IN ({0}) AND ItemID IN ({1}) AND IsDeleted = 0", hdnLstLocationID.Value, lstItemID));
            else
                lstItemBalance = new List<ItemBalance>();
            if (lstItemID != "")
                lstItemPlanning = BusinessLayer.GetvItemPlanningCustomList(string.Format("SiteID = '{0}' AND ItemID IN ({1}) AND IsDeleted = 0", AppSession.UserLogin.SiteID, lstItemID));
            else
                lstItemPlanning = new List<vItemPlanningCustom>();
            
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        List<ItemBalance> lstItemBalance = null;
        List<vItemPlanningCustom> lstItemPlanning = null;
        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseRequestDtDao entityDtDao = new PurchaseRequestDtDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                string[] lstSelectedMemberQty = hdnSelectedMemberQty.Value.Split(',');
                int TransactionID = 0;
                DetailPage.SavePurchaseRequestHd(ctx, ref TransactionID);

                int? businessPartnerID = null;
                if (hdnSupplierID.Value != "")
                    businessPartnerID = Convert.ToInt32(hdnSupplierID.Value);
                List<ItemMaster> lstItemMaster = BusinessLayer.GetItemMasterList(string.Format("ItemID IN ({0})", hdnSelectedMember.Value), ctx);
                int ct = 0;
                foreach (String itemID in lstSelectedMember)
                {
                    ItemMaster itemMaster = lstItemMaster.FirstOrDefault(p => p.ItemID == Convert.ToInt32(itemID));

                    PurchaseRequestDt entityDt = new PurchaseRequestDt();
                    entityDt.PurchaseRequestID = TransactionID;
                    entityDt.ItemID = itemMaster.ItemID;
                    entityDt.Quantity = Convert.ToDecimal(lstSelectedMemberQty[ct]);
                    entityDt.GCBaseUnit = itemMaster.GCItemUnit;
                    entityDt.BusinessPartnerID = businessPartnerID;
                    entityDt.Remarks = "";
                    entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                    entityDt.CreatedBy = AppSession.UserLogin.UserID;

                    int tempBusinessPartnerID = 0;
                    if (businessPartnerID != null)
                        tempBusinessPartnerID = (int)businessPartnerID;
                    GetItemMasterPurchase itemMasterPurchase = BusinessLayer.GetItemMasterPurchaseList(AppSession.UserLogin.SiteID, itemMaster.ItemID, tempBusinessPartnerID, ctx).FirstOrDefault();
                    ctx.CommandType = CommandType.Text;
                    ctx.Command.Parameters.Clear();
                    if (itemMasterPurchase != null)
                    {
                        entityDt.UnitPrice = itemMasterPurchase.Price;
                        entityDt.GCPurchaseUnit = itemMasterPurchase.PurchaseUnit;
                        entityDt.ConversionFactor = itemMasterPurchase.ConversionFactor;
                        entityDt.DiscountPercentage = itemMasterPurchase.Discount;
                    }
                    else
                    {
                        entityDt.UnitPrice = 0;
                        entityDt.DiscountPercentage = 0;
                        entityDt.GCPurchaseUnit = itemMaster.GCItemUnit;
                        entityDt.ConversionFactor = 1;
                    }
                    entityDtDao.Insert(entityDt);
                    ct++;
                }
                retval = TransactionID.ToString();
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                result = false;
                errMessage = ex.Message;
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