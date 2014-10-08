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

namespace CodeX.Muses.Web.Finance.Program
{
    public partial class APInvoiceSupplierProcessCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        private APInvoiceSupplierProcess DetailPage
        {
            get { return (APInvoiceSupplierProcess)Page; }
        }

        public override void InitializeDataControl(string param)
        {
            hdnPurchaseInvoiceID.Value = param;
            BindGridView(1, true, ref PageCount);
        }

        #region Bind Grid
        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = string.Format("BusinessPartnerID = {0} AND GCTransactionStatus IN ('{1}','{2}') AND PurchaseReceiveID NOT IN (SELECT PurchaseReceiveID FROM PurchaseInvoiceDt WHERE PurchaseReceiveID IS NOT NULL AND IsDeleted = 0)", AppSession.BusinessPartnerID, Constant.TransactionStatus.APPROVED, Constant.TransactionStatus.PROCESSED);
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvPurchaseReceiveCreditRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MATRIX);
            }

            lstSelectedMember = hdnSelectedPurchaseReceive.Value.Split(',');
            List<vPurchaseReceiveCredit> lstEntity = BusinessLayer.GetvPurchaseReceiveCreditList(filterExpression, Constant.GridViewPageSize.GRID_MATRIX, pageIndex);
            lvwView.DataSource = lstEntity;
            lvwView.DataBind();
        }

        protected void lvwView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                vPurchaseReceiveCredit entity = e.Item.DataItem as vPurchaseReceiveCredit;
                CheckBox chkPurchaseReceive = e.Item.FindControl("chkPurchaseReceive") as CheckBox;
                if (lstSelectedMember.Contains(entity.PurchaseReceiveID.ToString()))
                    chkPurchaseReceive.Checked = true;
                else
                    chkPurchaseReceive.Checked = false;
            }
        }

        protected void cbpProcessDetail_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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
        #endregion

        #region Save Entity
        private void ControlToEntity(IDbContext ctx, List<PurchaseInvoiceDt> lstEntityDt)
        {
            string filterExpression = string.Format("PurchaseReceiveID IN ({0})", hdnSelectedPurchaseReceive.Value.Substring(1));
            List<vPurchaseReceiveCredit> lstPurchaseReceiveCredit = BusinessLayer.GetvPurchaseReceiveCreditList(filterExpression);
            foreach (vPurchaseReceiveCredit purchaseReceiveCredit in lstPurchaseReceiveCredit)
            {
                PurchaseInvoiceDt entityDt = new PurchaseInvoiceDt();

                entityDt.PurchaseReceiveID = purchaseReceiveCredit.PurchaseReceiveID;
                //List<vPurchaseReceiveDt> lstEntity = BusinessLayer.GetvPurchaseReceiveDtList(string.Format("PurchaseReceiveID = {0}", purchaseReceiveCredit.PurchaseReceiveID), ctx);
                //entityDt.DiscountAmount = lstEntity.Sum(p => p.CustomTotalDiscount);
                entityDt.DiscountAmount = purchaseReceiveCredit.DiscountAmount;
                entityDt.ChargesAmount = purchaseReceiveCredit.ChargesAmount;
                entityDt.CreditNoteAmount = purchaseReceiveCredit.CNAmount;
                entityDt.PPH23Amount = 0; // ini juga perlu dipertanyakan soalnya di receive kan ga ada pph
                entityDt.PPH25Amount = 0;
                entityDt.FinalDiscountAmount = purchaseReceiveCredit.FinalDiscount;
                entityDt.DownPaymentAmount = purchaseReceiveCredit.DownPaymentAmount;
                entityDt.StampAmount = purchaseReceiveCredit.StampAmount;
                entityDt.TransactionAmount = purchaseReceiveCredit.TransactionAmount;
                entityDt.ReferenceNo = purchaseReceiveCredit.ReferenceNo;
                entityDt.ReferenceDate = purchaseReceiveCredit.ReferenceDate;
                entityDt.VATAmount = purchaseReceiveCredit.VATAmount;
                entityDt.LineAmount = entityDt.TransactionAmount - entityDt.DiscountAmount - entityDt.FinalDiscountAmount + entityDt.VATAmount + entityDt.PPH23Amount + entityDt.PPH25Amount + entityDt.StampAmount + entityDt.ChargesAmount - entityDt.CreditNoteAmount;
                lstEntityDt.Add(entityDt);
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseInvoiceDtDao entityDtDao = new PurchaseInvoiceDtDao(ctx);
            int purchaseInvoiceID = Convert.ToInt32(hdnPurchaseInvoiceID.Value);
            try
            {
                DetailPage.SavePurchaseInvoiceHd(ctx, ref purchaseInvoiceID);
                List<PurchaseInvoiceDt> lstEntityDt = new List<PurchaseInvoiceDt>();
                ControlToEntity(ctx, lstEntityDt);

                foreach (PurchaseInvoiceDt entityDt in lstEntityDt)
                {
                    entityDt.PurchaseInvoiceID = purchaseInvoiceID;

                    entityDt.CreatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Insert(entityDt);
                }
                retval = purchaseInvoiceID.ToString();
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