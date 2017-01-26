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
    public partial class ItemDistributionQuickPicksCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;

        private ItemDistributionEntry DetailPage
        {
            get { return (ItemDistributionEntry)Page; }
        }

        protected string OnGetFilterExpressionItemGroup()
        {
            return string.Format("GCItemType = '{0}' AND IsDeleted = 0", Constant.ItemType.PRODUCT);
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
            hdnLocationID.Value = temp[1];
            hdnLstFilterFromLocationItemGroup.Value = temp[2];
            hdnLstFilterToLocationItemGroup.Value = temp[3];
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
                filterExpression += string.Format("GCItemType = '{0}' AND GCItemStatus = '{1}' AND ItemName1 LIKE '%{2}%' AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE DisplayPath LIKE '%/{3}/%') AND IsDeleted = 0", Constant.ItemType.PRODUCT, Constant.ItemStatus.ACTIVE, hdnFilterItem.Value, hdnItemGroupID.Value);
            else
            {
                filterExpression += string.Format("GCItemType = '{0}' AND GCItemStatus = '{1}' AND ItemName1 LIKE '%{2}%' AND IsDeleted = 0", Constant.ItemType.PRODUCT, Constant.ItemStatus.ACTIVE, hdnFilterItem.Value);
                if (hdnLstFilterFromLocationItemGroup.Value != "")
                    filterExpression += string.Format(" AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE {0})", hdnLstFilterFromLocationItemGroup.Value);
                if (hdnLstFilterToLocationItemGroup.Value != "")
                    filterExpression += string.Format(" AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE {0})", hdnLstFilterToLocationItemGroup.Value);
            }
            return filterExpression;
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vItemProduct entity = e.Row.DataItem as vItemProduct;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                HtmlGenericControl divStock = e.Row.FindControl("divStock") as HtmlGenericControl;
                HtmlInputHidden hdnDistributionUnit = e.Row.FindControl("hdnDistributionUnit") as HtmlInputHidden;
                HtmlInputHidden hdnConversionFactor = e.Row.FindControl("hdnConversionFactor") as HtmlInputHidden;
                if (lstSelectedMember.Contains(entity.ItemID.ToString()))
                    chkIsSelected.Checked = true;

                vItemPlanningCustom itemPlanning = lstItemPlanning.FirstOrDefault(p => p.ItemID == entity.ItemID);
                divStock.InnerHtml = lstItemBalance.Where(p => p.ItemID == entity.ItemID).Sum(p => p.QuantityEND).ToString();
                hdnDistributionUnit.Value = itemPlanning.DistributionUnit;
                hdnConversionFactor.Value = itemPlanning.cfDistributionUnitConversion;
            }
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (hdnTransactionID.Value != "0" && hdnTransactionID.Value != "")
            {
                List<vItemDistributionDt> lstItemDistributionID = BusinessLayer.GetvItemDistributionDtList(string.Format("DistributionID = {0} AND GCItemDetailStatus != '{1}' AND IsDeleted = 0", hdnTransactionID.Value, Constant.TransactionStatus.VOID));
                string lstSelectedID = string.Join(",", lstItemDistributionID.Select(p => p.ItemID).ToList());
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
            if (hdnLocationID.Value != "" && lstItemID != "")
                lstItemBalance = BusinessLayer.GetItemBalanceList(string.Format("LocationID = {0} AND ItemID IN ({1}) AND IsDeleted = 0", hdnLocationID.Value, lstItemID));
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
            ItemDistributionDtDao entityDtDao = new ItemDistributionDtDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                string[] lstSelectedMemberQty = hdnSelectedMemberQty.Value.Split(',');
                int TransactionID = 0;
                DetailPage.SaveItemDistributionHd(ctx, ref TransactionID);

                List<ItemMaster> lstItemMaster = BusinessLayer.GetItemMasterList(string.Format("ItemID IN ({0})", hdnSelectedMember.Value), ctx); 
                lstItemPlanning = BusinessLayer.GetvItemPlanningCustomList(string.Format("SiteID = '{0}' AND ItemID IN ({1}) AND IsDeleted = 0", AppSession.UserLogin.SiteID, hdnSelectedMember.Value), ctx);

                int ct = 0;
                foreach (String itemID in lstSelectedMember)
                {
                    ItemMaster itemMaster = lstItemMaster.FirstOrDefault(p => p.ItemID == Convert.ToInt32(itemID));
                    vItemPlanningCustom itemPlanning = lstItemPlanning.FirstOrDefault(p => p.ItemID == Convert.ToInt32(itemID));

                    ItemDistributionDt entityDt = new ItemDistributionDt();
                    entityDt.DistributionID = TransactionID;
                    entityDt.ItemID = itemMaster.ItemID;
                    entityDt.Quantity = Convert.ToDecimal(lstSelectedMemberQty[ct]);
                    entityDt.GCItemUnit = itemPlanning.GCDistributionUnit;
                    entityDt.GCBaseUnit = itemMaster.GCItemUnit;
                    entityDt.ConversionFactor = itemPlanning.DistributionUnitConversionFactor;
                    entityDt.Remarks = "";
                    entityDt.GCItemDetailStatus = Constant.DistributionStatus.OPEN;
                    entityDt.CreatedBy = AppSession.UserLogin.UserID;
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