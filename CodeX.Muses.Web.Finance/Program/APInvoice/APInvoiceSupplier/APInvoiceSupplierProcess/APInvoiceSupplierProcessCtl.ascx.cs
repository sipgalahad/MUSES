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

namespace CodeX.Muses.Web.Finance.Program
{
    public partial class APInvoiceSupplierProcessCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        private string[] lstSelectedMemberPurchaseReturn = null;
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
                transactionCode = Constant.TransactionCode.CONSIGNMENT_RECEIVE;
            else
                transactionCode = Constant.TransactionCode.PURCHASE_RECEIVE;
            string filterExpression = string.Format("BusinessPartnerID = {0} AND GCTransactionStatus IN ('{1}') AND GCItemType = '{2}' AND TransactionCode = '{3}' AND PurchaseReceiveID NOT IN (SELECT PurchaseReceiveID FROM PurchaseInvoiceDt WHERE PurchaseReceiveID IS NOT NULL AND IsDeleted = 0)", MasterPage.BusinessPartnerID, Constant.TransactionStatus.PROCESSED, hdnGCItemType.Value, transactionCode);
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvPurchaseReceiveCreditRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MATRIX);
            }

            lstSelectedMember = hdnSelectedPurchaseReceive.Value.Split(',');
            lstSelectedMemberPurchaseReturn = hdnSelectedIncludePurchaseReturn.Value.Split(',');
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
                CheckBox chkIsIncludePurchaseReturn = e.Item.FindControl("chkIsIncludePurchaseReturn") as CheckBox;
                if (lstSelectedMemberPurchaseReturn.Contains(entity.PurchaseReceiveID.ToString()))
                    chkIsIncludePurchaseReturn.Checked = true;
                else
                    chkIsIncludePurchaseReturn.Checked = false;
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
        private void ControlToEntity(IDbContext ctx, List<PurchaseInvoiceDt> lstEntityDt, List<PurchaseInvoiceDtCreditNote> lstEntityDtCreditNote)
        {
            lstSelectedMemberPurchaseReturn = hdnSelectedIncludePurchaseReturn.Value.Split(',');
            string filterExpression = string.Format("PurchaseReceiveID IN ({0})", hdnSelectedPurchaseReceive.Value.Substring(1));
            List<vPurchaseReceiveCredit> lstPurchaseReceiveCredit = BusinessLayer.GetvPurchaseReceiveCreditList(filterExpression);
            foreach (vPurchaseReceiveCredit purchaseReceiveCredit in lstPurchaseReceiveCredit)
            {
                bool isIncludePurchaseReturn = lstSelectedMemberPurchaseReturn.Contains(purchaseReceiveCredit.PurchaseReceiveID.ToString());
                PurchaseInvoiceDt entityDt = new PurchaseInvoiceDt();
                entityDt.PurchaseReceiveID = purchaseReceiveCredit.PurchaseReceiveID;
                entityDt.PurchaseReturnID = null;
                entityDt.CreditNoteID = null;
                entityDt.IsCreditNoteOnly = false;
                if (purchaseReceiveCredit.CreditNoteID != "")
                {
                    if (isIncludePurchaseReturn)
                    {
                        string[] lstCreditNoteID = purchaseReceiveCredit.CreditNoteID.Split(',');
                        foreach (string creditNoteID in lstCreditNoteID)
                        {
                            PurchaseInvoiceDtCreditNote entityDtCreditNote = new PurchaseInvoiceDtCreditNote();
                            entityDtCreditNote.PurchaseReceiveID = purchaseReceiveCredit.PurchaseReceiveID;
                            entityDtCreditNote.CreditNoteID = Convert.ToInt32(creditNoteID);
                            lstEntityDtCreditNote.Add(entityDtCreditNote);
                        }
                        entityDt.CreditNoteAmount = purchaseReceiveCredit.CNAmount;
                    }
                    else
                        entityDt.CreditNoteAmount = 0;
                }
                else
                    entityDt.CreditNoteAmount = 0;
                entityDt.ChargesAmount = purchaseReceiveCredit.ChargesAmount;
                entityDt.PPH23Amount = 0; 
                entityDt.PPH25Amount = 0;
                entityDt.FinalDiscountAmount = purchaseReceiveCredit.FinalDiscountAmount;
                entityDt.DownPaymentAmount = purchaseReceiveCredit.DownPaymentAmount;
                entityDt.StampAmount = purchaseReceiveCredit.StampAmount;
                entityDt.TransactionAmount = purchaseReceiveCredit.TransactionAmount;
                entityDt.ReferenceNo = purchaseReceiveCredit.ReferenceNo;
                entityDt.ReferenceDate = purchaseReceiveCredit.ReferenceDate;
                entityDt.VATAmount = purchaseReceiveCredit.VATAmount;
                entityDt.LineAmount = entityDt.TransactionAmount - entityDt.DiscountAmount - entityDt.FinalDiscountAmount + entityDt.VATAmount + entityDt.PPH23Amount + entityDt.PPH25Amount + entityDt.StampAmount + entityDt.ChargesAmount - entityDt.DownPaymentAmount - entityDt.CreditNoteAmount;
                lstEntityDt.Add(entityDt);
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseInvoiceDtDao entityDtDao = new PurchaseInvoiceDtDao(ctx);
            PurchaseInvoiceDtCreditNoteDao entityDtCreditNoteDao = new PurchaseInvoiceDtCreditNoteDao(ctx);
            int purchaseInvoiceID = Convert.ToInt32(hdnPurchaseInvoiceID.Value);
            try
            {
                DetailPage.SavePurchaseInvoiceHd(ctx, ref purchaseInvoiceID);
                List<PurchaseInvoiceDt> lstEntityDt = new List<PurchaseInvoiceDt>();
                List<PurchaseInvoiceDtCreditNote> lstEntityDtCreditNote = new List<PurchaseInvoiceDtCreditNote>();
                ControlToEntity(ctx, lstEntityDt, lstEntityDtCreditNote);

                foreach (PurchaseInvoiceDt entityDt in lstEntityDt)
                {
                    entityDt.PurchaseInvoiceID = purchaseInvoiceID;

                    entityDt.CreatedBy = AppSession.UserLogin.UserID;
                    entityDt.ID = entityDtDao.Insert(entityDt);

                    List<PurchaseInvoiceDtCreditNote> lstEntityDtCreditNote1 = lstEntityDtCreditNote.Where(p => p.PurchaseReceiveID == entityDt.PurchaseReceiveID).ToList();
                    foreach (PurchaseInvoiceDtCreditNote entityDtCreditNote in lstEntityDtCreditNote1)
                    {
                        entityDtCreditNote.PurchaseInvoiceDtID = entityDt.ID;
                        entityDtCreditNoteDao.Insert(entityDtCreditNote);
                    }

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