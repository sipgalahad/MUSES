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
    public partial class ItemRequestQuickPicksCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;

        private ItemRequestEntry DetailPage
        {
            get { return (ItemRequestEntry)Page; }
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
                filterExpression += string.Format("GCItemType = '{0}' AND ItemName1 LIKE '%{1}%' AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE DisplayPath LIKE '%/{2}/%') AND IsDeleted = 0", Constant.ItemType.PRODUCT, hdnFilterItem.Value, hdnItemGroupID.Value);
            else
            {
                filterExpression += string.Format("GCItemType = '{0}' AND ItemName1 LIKE '%{1}%' AND IsDeleted = 0", Constant.ItemType.PRODUCT, hdnFilterItem.Value);
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
                if (lstSelectedMember.Contains(entity.ItemID.ToString()))
                    chkIsSelected.Checked = true;

                divStock.InnerHtml = lstItemBalance.Where(p => p.ItemID == entity.ItemID).Sum(p => p.QuantityEND).ToString();
            }
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (hdnTransactionID.Value != "0" && hdnTransactionID.Value != "")
            {
                List<vItemRequestDt> lstItemRequestID = BusinessLayer.GetvItemRequestDtList(string.Format("ItemRequestID = {0} AND GCItemDetailStatus != '{1}'", hdnTransactionID.Value, Constant.TransactionStatus.VOID));
                string lstSelectedID = string.Join(",", lstItemRequestID.Select(p => p.ItemID).ToList());
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
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        List<ItemBalance> lstItemBalance = null;
        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ItemRequestDtDao entityDtDao = new ItemRequestDtDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                string[] lstSelectedMemberQty = hdnSelectedMemberQty.Value.Split(',');
                int TransactionID = 0;
                DetailPage.SaveItemRequestHd(ctx, ref TransactionID);

                List<ItemMaster> lstItemMaster = BusinessLayer.GetItemMasterList(string.Format("ItemID IN ({0})", hdnSelectedMember.Value), ctx);
                int ct = 0;
                foreach (String itemID in lstSelectedMember)
                {
                    ItemMaster itemMaster = lstItemMaster.FirstOrDefault(p => p.ItemID == Convert.ToInt32(itemID));

                    ItemRequestDt entityDt = new ItemRequestDt();
                    entityDt.ItemRequestID = TransactionID;
                    entityDt.ItemID = itemMaster.ItemID;
                    entityDt.Quantity = Convert.ToDecimal(lstSelectedMemberQty[ct]);
                    entityDt.GCItemUnit = itemMaster.GCItemUnit;
                    entityDt.GCBaseUnit = itemMaster.GCItemUnit;
                    entityDt.ConversionFactor = 1;                    
                    entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
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