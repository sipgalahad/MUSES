using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using System.Web.UI.HtmlControls;
using CodeX.Data.Core.Dal;
using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxEditors;
using CodeX.Common;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class PurchaseReturnEntryPicksCtl : BaseEntryPopupCtl
    {
        private PurchaseReturnEntry DetailPage
        {
            get { return (PurchaseReturnEntry)Page; }
        }

        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');
            hdnPurchaseReceiveID.Value = temp[0];
            hdnPurchaseReturnID.Value = temp[1];

            BindGridView();
        }

        #region Bind Grid
        List<StandardCode> lstPurchaseReturnReason = null;
        private void BindGridView()
        {
            lstPurchaseReturnReason = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PURCHASE_RETURN_REASON));
            string filterExpression = string.Format("PurchaseReceiveID = {0} AND GCItemDetailStatus != '{1}'", hdnPurchaseReceiveID.Value, Constant.TransactionStatus.VOID);
            //if (hdnPurchaseReturnID.Value != "0" && hdnPurchaseReturnID.Value != "")
            //    filterExpression += string.Format(" AND ItemID NOT IN (SELECT ItemID FROM PurchaseReturnDt WHERE PurchaseReturnID = {0})", hdnPurchaseReceiveID.Value);
            List<vPurchaseReceiveDt> lstEntityDt = BusinessLayer.GetvPurchaseReceiveDtList(filterExpression);

            //List<vPurchaseReturnDt> lstPurchaseReturnDt = BusinessLayer.GetvPurchaseReturnDtList(filterExpression);

            lvwView.DataSource = lstEntityDt;
            lvwView.DataBind();
        }

        #endregion

        protected void lvwView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                vPurchaseReceiveDt entity = (vPurchaseReceiveDt)e.Item.DataItem;
                TextBox txtQtyRetur = (TextBox)e.Item.FindControl("txtQtyRetur");
                ASPxComboBox cboPurchaseReturnReason = (ASPxComboBox)e.Item.FindControl("cboPurchaseReturnReason");
                cboPurchaseReturnReason.ClientInstanceName = string.Format("cboPurchaseReturnReason{0}", e.Item.DataItemIndex);
                Methods.SetComboBoxField<StandardCode>(cboPurchaseReturnReason, lstPurchaseReturnReason, "StandardCodeName", "StandardCodeID");
                
                txtQtyRetur.Attributes.Add("max", entity.Quantity.ToString());
                Helper.SetControlEntrySetting(txtQtyRetur, new ControlEntrySetting(true, true, true), "mpEntryPopup");
                Helper.SetControlEntrySetting(cboPurchaseReturnReason, new ControlEntrySetting(true, true, true), "mpEntryPopup");
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseReceiveHdDao entityHdDao = new PurchaseReceiveHdDao(ctx);
            PurchaseReturnDtDao entityDtDao = new PurchaseReturnDtDao(ctx);
            try
            {
                int PRID = 0;
                string purchaseReturnNo = "";
                DetailPage.SavePurchaseReturnHd(ctx, ref PRID, ref purchaseReturnNo);
                
                string[] lstSelectedItem = hdnSelectedItem.Value.Split(',');
                string[] lstSelectedQty = hdnSelectedQtyRetur.Value.Split(',');
                string[] lstSelectedReturnReason = hdnSelectedReturnReason.Value.Split(',');
                List<PurchaseReceiveDt> lstPurchaseReceiveDt = BusinessLayer.GetPurchaseReceiveDtList(string.Format("ID IN ({0})", hdnSelectedItem.Value), ctx);
                for (int i = 0; i < lstSelectedItem.Length; ++i)
                {
                    PurchaseReceiveDt directPurchaseDt = lstPurchaseReceiveDt.FirstOrDefault(p => p.ID == Convert.ToInt32(lstSelectedItem[i]));
                    PurchaseReturnDt entityDt = new PurchaseReturnDt();
                    entityDt.ItemID = directPurchaseDt.ItemID;
                    entityDt.ItemName1 = directPurchaseDt.ItemName1;
                    entityDt.Quantity = Convert.ToDecimal(lstSelectedQty[i]);
                    entityDt.UnitPrice = directPurchaseDt.UnitPrice;
                    entityDt.ConversionFactor = directPurchaseDt.ConversionFactor;
                    entityDt.DiscountPercentage1 = directPurchaseDt.DiscountPercentage1;
                    entityDt.DiscountAmount1 = entityDt.Quantity * entityDt.UnitPrice * entityDt.DiscountPercentage1 / 100;
                    entityDt.DiscountPercentage2 = directPurchaseDt.DiscountPercentage2;
                    entityDt.DiscountAmount2 = ((entityDt.Quantity * entityDt.UnitPrice) - entityDt.DiscountAmount1) * entityDt.DiscountPercentage2 / 100;
                    entityDt.LineAmount = entityDt.Quantity * entityDt.UnitPrice - (entityDt.DiscountAmount1 + entityDt.DiscountAmount2);
                    entityDt.GCPurchaseReturnReason = lstSelectedReturnReason[i];
                    entityDt.GCBaseUnit = directPurchaseDt.GCBaseUnit;
                    entityDt.GCItemUnit = directPurchaseDt.GCItemUnit;
                    entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                    entityDt.PurchaseReturnID = PRID;
                    entityDt.CreatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Insert(entityDt);
                }

                PurchaseReceiveHd entity = entityHdDao.Get(Convert.ToInt32(hdnPurchaseReceiveID.Value));
                entity.IsHasPurchaseReturn = true;
                entity.PurchaseReturnID = Convert.ToInt32(PRID);
                entityHdDao.Update(entity);
                retval = purchaseReturnNo;
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