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

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class PurchaseBudgetQuickPicksCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;

        private PurchaseBudgetEntry DetailPage
        {
            get { return (PurchaseBudgetEntry)Page; }
        }

        public override void InitializeDataControl(string param)
        {
            hdnParam.Value = param;
            string[] temp = param.Split('|');
            hdnTransactionID.Value = temp[0];
            hdnSiteServiceUnitID.Value = temp[1];
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
            string filterExpression = string.Format("SiteServiceUnitID = {0} AND ItemName1 LIKE '%{1}%' AND GCItemStatus = '{2}' AND IsDeleted = 0", hdnSiteServiceUnitID.Value, hdnFilterItem.Value, Constant.ItemStatus.ACTIVE);
            if (hdnItemGroupDrugLogisticID.Value != "")
                filterExpression += string.Format(" AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroupMaster WHERE DisplayPath LIKE '%/{0}/%')", hdnItemGroupDrugLogisticID.Value);
            return filterExpression;
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vServiceUnitItemLogistic entity = e.Row.DataItem as vServiceUnitItemLogistic;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (lstSelectedMember.Contains(entity.ItemID.ToString()))
                    chkIsSelected.Checked = true;
            }
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (hdnTransactionID.Value != "0" && hdnTransactionID.Value != "")
            {
                List<vPurchaseBudgetDt> lstItemID = BusinessLayer.GetvPurchaseBudgetDtList(string.Format("TransactionID = {0} AND GCItemDetailStatus != '{1}'", hdnTransactionID.Value, Constant.TransactionStatus.VOID));
                string lstSelectedID = "";
                if (lstItemID.Count > 0)
                {
                    foreach (vPurchaseBudgetDt itm in lstItemID)
                        lstSelectedID += "," + itm.ItemID;
                    filterExpression += string.Format(" AND ItemID NOT IN ({0})", lstSelectedID.Substring(1));
                }
            }
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvServiceUnitItemLogisticRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 10);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<vServiceUnitItemLogistic> lstEntity = BusinessLayer.GetvServiceUnitItemLogisticList(filterExpression, 10, pageIndex, "ItemName1 ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseBudgetDtDao entityDtDao = new PurchaseBudgetDtDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                string[] lstSelectedMemberQty = hdnSelectedMemberQty.Value.Split(',');
                string[] lstSelectedMemberTotalAmount = hdnSelectedMemberTotalAmount.Value.Split(',');
                int TransactionID = 0;
                DetailPage.SavePurchaseBudgetHd(ctx, ref TransactionID);

                List<ItemMaster> lstItemMaster = BusinessLayer.GetItemMasterList(string.Format("ItemID IN ({0})", hdnSelectedMember.Value), ctx);
                int ct = 0;
                foreach (String itemID in lstSelectedMember)
                {
                    ItemMaster itemMaster = lstItemMaster.FirstOrDefault(p => p.ItemID == Convert.ToInt32(itemID));

                    PurchaseBudgetDt entityDt = new PurchaseBudgetDt();
                    entityDt.TransactionID = TransactionID;
                    entityDt.ItemID = itemMaster.ItemID;
                    entityDt.Quantity = Convert.ToDecimal(lstSelectedMemberQty[ct]);
                    entityDt.GCItemUnit = itemMaster.GCItemUnit;
                    entityDt.GCBaseUnit = itemMaster.GCItemUnit;
                    entityDt.ConversionFactor = 1;
                    entityDt.BaseQuantity = entityDt.Quantity * entityDt.ConversionFactor;
                    entityDt.Remarks = "";
                    entityDt.TotalAmount = Convert.ToDecimal(lstSelectedMemberTotalAmount[ct]);
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