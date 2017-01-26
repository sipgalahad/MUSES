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
using CodeX.Web.Finance.MasterPage;

namespace CodeX.Ottimo.Web.Finance.Program
{
    public partial class APInvoiceSupplierProcessPurchaseReturnCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        private APInvoiceSupplierProcess DetailPage
        {
            get { return (APInvoiceSupplierProcess)Page; }
        }

        public MPSupplierPageTrx MasterPage
        {
            get
            {
                return (MPSupplierPageTrx)DetailPage.Master;
            }
        }

        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');
            hdnPurchaseInvoiceID.Value = temp[0];
            hdnGCItemType.Value = temp[1];
            hdnGCPurchaseType.Value = temp[2];
            BindGridView(1, true, ref PageCount);
        }

        #region Bind Grid
        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string transactionCode = "";
            if (hdnGCPurchaseType.Value == Constant.PurchaseType.CONSIGNMENT)
                transactionCode = Constant.TransactionCode.CONSIGNMENT_RETURN;
            else
                transactionCode = Constant.TransactionCode.PURCHASE_RETURN;
            string filterExpression = string.Format("BusinessPartnerID = {0} AND GCTransactionStatus IN ('{1}') AND TransactionCode = '{2}' AND GCPurchaseReturnType = '{3}' AND PurchaseReturnID NOT IN (SELECT PurchaseReturnID FROM PurchaseInvoiceDt WHERE PurchaseReturnID IS NOT NULL AND IsDeleted = 0)", MasterPage.BusinessPartnerID, Constant.TransactionStatus.APPROVED, transactionCode, Constant.PurchaseReturnType.CREDIT_NOTE);
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvPurchaseReturnHdRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MATRIX);
            }

            lstSelectedMember = hdnSelectedPurchaseReturn.Value.Split(',');
            List<vPurchaseReturnHd> lstEntity = BusinessLayer.GetvPurchaseReturnHdList(filterExpression, Constant.GridViewPageSize.GRID_MATRIX, pageIndex);
            lvwView.DataSource = lstEntity;
            lvwView.DataBind();
        }

        protected void lvwView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                vPurchaseReturnHd entity = e.Item.DataItem as vPurchaseReturnHd;
                CheckBox chkPurchaseReturn = e.Item.FindControl("chkPurchaseReturn") as CheckBox;
                if (lstSelectedMember.Contains(entity.PurchaseReturnID.ToString()))
                    chkPurchaseReturn.Checked = true;
                else
                    chkPurchaseReturn.Checked = false;
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
            string filterExpression = string.Format("PurchaseReturnID IN ({0})", hdnSelectedPurchaseReturn.Value.Substring(1));
            List<vPurchaseReturnHd> lstPurchaseReturn = BusinessLayer.GetvPurchaseReturnHdList(filterExpression);
            foreach (vPurchaseReturnHd purchaseReturn in lstPurchaseReturn)
            {
                PurchaseInvoiceDt entityDt = new PurchaseInvoiceDt();
                entityDt.PurchaseReceiveID = purchaseReturn.PurchaseReceiveID;
                entityDt.PurchaseReturnID = purchaseReturn.PurchaseReturnID;
                entityDt.IsCreditNoteOnly = true;
                if (purchaseReturn.CreditNoteID > 0)
                {
                    entityDt.CreditNoteID = purchaseReturn.CreditNoteID;
                    entityDt.CreditNoteAmount = purchaseReturn.TotalNetTransactionAmount;
                }
                else
                {
                    entityDt.CreditNoteID = null;
                    entityDt.CreditNoteAmount = 0;
                }
                entityDt.ChargesAmount = 0;                
                entityDt.PPH23Amount = 0; 
                entityDt.PPH25Amount = 0;
                entityDt.FinalDiscountAmount = 0;
                entityDt.DownPaymentAmount = 0;
                entityDt.StampAmount = 0;
                entityDt.TransactionAmount = 0;
                entityDt.ReferenceNo = purchaseReturn.ReferenceNo;
                entityDt.ReferenceDate = purchaseReturn.ReferenceDate;
                entityDt.VATAmount = 0;
                entityDt.LineAmount = entityDt.TransactionAmount - entityDt.DiscountAmount - entityDt.FinalDiscountAmount + entityDt.VATAmount + entityDt.PPH23Amount + entityDt.PPH25Amount + entityDt.StampAmount + entityDt.ChargesAmount - entityDt.DownPaymentAmount - entityDt.CreditNoteAmount;
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
        #endregion
    }
}