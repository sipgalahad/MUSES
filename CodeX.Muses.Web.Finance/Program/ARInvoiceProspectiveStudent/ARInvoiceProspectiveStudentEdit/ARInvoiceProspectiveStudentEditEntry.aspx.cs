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
using System.Web.UI.HtmlControls;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Web.Finance.Program
{
    public partial class ARInvoiceProspectiveStudentEditEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Finance.AR_INVOICE_PROSPECTIVE_STUDENT_EDIT;
        }
        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowAdd = IsAllowSave = false;
        }

        protected string OnGetCustomerFilterExpression()
        {
            return string.Format("IsDeleted = 0");
        }

        protected override void InitializeDataControl()
        {
            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            decimal tempTransactionAmount = -1, tempClaimedAmount = -1, tempDiscountAmount = -1;
            BindGridView(1, true, ref PageCount, ref RowCount, ref tempTransactionAmount, ref tempDiscountAmount, ref tempClaimedAmount);

            IsLoadFirstRecord = (OnGetRowCount() > 0);
        }

        protected string IsEditable()
        {
            return hdnIsEditable.Value;
        }

        #region Load Entity
        public override void OnAddRecord()
        {
            hdnIsEditable.Value = "1";
        }

        protected string OnGetARInvoiceFilterExpression()
        {
            return string.Format("ProspectiveStudentID = {0} AND GCTransactionStatus NOT IN ('{1}','{2}') ", AppSession.ProspectiveStudentID, Constant.TransactionStatus.CLOSED, Constant.TransactionStatus.VOID);
        }

        protected string GetFilterExpression()
        {
            return OnGetARInvoiceFilterExpression();
        }

        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvARInvoiceHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vARInvoiceHd entity = BusinessLayer.GetvARInvoiceHdList(filterExpression, PageIndex, " ARInvoiceID DESC")[0];
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvARInvoiceHdRowIndex(filterExpression, keyValue, "ARInvoiceID DESC");
            vARInvoiceHd entity = BusinessLayer.GetvARInvoiceHdList(filterExpression, PageIndex, "ARInvoiceID DESC")[0];
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vARInvoiceHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
                hdnIsEditable.Value = "0";
            }
            else
                hdnIsEditable.Value = "1";
            txtARInvoiceNo.Text = entity.ARInvoiceNo;
            txtInvoiceDate.Text = entity.ARInvoiceDateInString;
            txtDueDate.Text = entity.DueDateInString;
            txtRemarks.Text = entity.Remarks;
            hdnARInvoiceID.Value = entity.ARInvoiceID.ToString();
            txtTotalTransaction.Text = entity.TotalTransactionAmount.ToString();
            txtTotalClaimed.Text = entity.TotalClaimedAmount.ToString();
            txtTotalDiscount.Text = entity.TotalDiscountAmount.ToString();

            decimal tempTransactionAmount = -1;
            decimal tempDiscountAmount = -1;
            decimal tempClaimedAmount = -1;
            BindGridView(1, true, ref PageCount, ref RowCount, ref tempTransactionAmount, ref tempDiscountAmount, ref tempClaimedAmount);
            hdnPageCount.Value = PageCount.ToString();
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount, ref decimal transactionAmount, ref decimal discountAmount, ref decimal claimedAmount)
        {
            String filterExpression = "1 = 0";
            if (hdnARInvoiceID.Value != "")
            {
                filterExpression = string.Format("ARInvoiceID = {0}", hdnARInvoiceID.Value);
                if (transactionAmount > -1)
                {
                    ARInvoiceHd entity = BusinessLayer.GetARInvoiceHd(Convert.ToInt32(hdnARInvoiceID.Value));
                    transactionAmount = entity.TotalTransactionAmount;
                    discountAmount = entity.TotalDiscountAmount;
                    claimedAmount = entity.TotalClaimedAmount;
                }
            }

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvARInvoiceDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vARInvoiceDt> lstInvoiceDt = BusinessLayer.GetvARInvoiceDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ARInvoiceID ASC");
            lvwView.DataSource = lstInvoiceDt;
            lvwView.DataBind();
        }

        protected void lvwView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                vARInvoiceDt entity = e.Item.DataItem as vARInvoiceDt;
                TextBox txtClaimedAmount = e.Item.FindControl("txtClaimedAmount") as TextBox;
                TextBox txtDiscountAmount = e.Item.FindControl("txtDiscountAmount") as TextBox;

                txtClaimedAmount.Text = entity.ClaimedAmount.ToString();
                txtDiscountAmount.Text = entity.DiscountAmount.ToString();

                //HtmlGenericControl ctl = e.Item.FindControl("varianceAmountDiv") as HtmlGenericControl;
                //if (entity.VarianceAmount < 0)
                //    ctl.Attributes.Add("class", "lblNegativeAmount");
                //else
                //    ctl.Attributes.Add("class", "");
            }
        }
        #endregion

        #region Header
        protected override bool OnVoidRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ARInvoiceHdDao entityHdDao = new ARInvoiceHdDao(ctx);
            ARInvoiceDtDao entityDtDao = new ARInvoiceDtDao(ctx);
            try
            {
                ARInvoiceHd entity = entityHdDao.Get(Convert.ToInt32(hdnARInvoiceID.Value));
                entity.GCTransactionStatus = Constant.TransactionStatus.VOID;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Update(entity);

                List<ARInvoiceDt> lstARInvoiceDt = BusinessLayer.GetARInvoiceDtList(string.Format("ARInvoiceID = {0}", hdnARInvoiceID.Value), ctx);
                foreach (ARInvoiceDt entityDt in lstARInvoiceDt)
                    entityDtDao.Delete(entityDt.ARInvoiceDtID); 
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override bool OnApproveRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ARInvoiceHdDao entityHdDao = new ARInvoiceHdDao(ctx);
            try
            {
                ARInvoiceHd entity = entityHdDao.Get(Convert.ToInt32(hdnARInvoiceID.Value));
                entity.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Update(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override bool OnReopenRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ARInvoiceHdDao entityHdDao = new ARInvoiceHdDao(ctx);
            try
            {
                ARInvoiceHd entity = entityHdDao.Get(Convert.ToInt32(hdnARInvoiceID.Value));
                entity.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Update(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                decimal tempTransactionAmount = -1;
                decimal tempClaimedAmount = -1;
                decimal tempDiscountAmount = -1;
                if (OnProcessRecord(ref errMessage, Convert.ToDecimal(param[1]), ref tempTransactionAmount, ref tempDiscountAmount, ref tempClaimedAmount))
                    result += string.Format("success|{0}|{1}|{2}", tempTransactionAmount, tempDiscountAmount, tempClaimedAmount);
                else
                    result += "fail|" + errMessage;
            }
            else if (param[0] == "delete")
            {
                if (OnDeleteRecord(ref errMessage))
                    result += "success";
                else
                    result += "fail|" + errMessage;
            }
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool OnDeleteRecord(ref string errMessage)
        {
            bool result = true;
            try
            {
                int ARInvoiceDtID = Convert.ToInt32(hdnARInvoiceDtID.Value);
                BusinessLayer.DeleteARInvoiceDt(ARInvoiceDtID);
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
            }

            return result;
        }

        private bool OnProcessRecord(ref string errMessage, Decimal discountAmountSave, ref decimal transactionAmount, ref decimal discountAmount, ref decimal claimedAmount)
        {
            bool result = true;
            try
            {
                int ARInvoiceDtID = Convert.ToInt32(hdnARInvoiceDtID.Value);
                ARInvoiceDt entityDt = BusinessLayer.GetARInvoiceDt(ARInvoiceDtID);
                entityDt.DiscountAmount = discountAmountSave;
                entityDt.ClaimedAmount = entityDt.TransactionAmount - entityDt.DiscountAmount;
                entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateARInvoiceDt(entityDt);

                ARInvoiceHd entity = BusinessLayer.GetARInvoiceHd(Convert.ToInt32(hdnARInvoiceID.Value));
                transactionAmount = entity.TotalTransactionAmount;
                discountAmount = entity.TotalDiscountAmount;
                claimedAmount = entity.TotalClaimedAmount;
            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
            }

            return result;
        }
        #endregion

        #region Callback
        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            decimal transactionAmount = 0, claimedAmount = 0, varianceAmount = 0;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    transactionAmount = -1;
                    claimedAmount = -1;
                    varianceAmount = -1;
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount, ref transactionAmount, ref claimedAmount, ref varianceAmount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount, ref rowCount, ref transactionAmount, ref claimedAmount, ref varianceAmount);
                    result = string.Format("refresh|{0}|{1}|{2}|{3}|{4}", pageCount, rowCount, transactionAmount, claimedAmount, varianceAmount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
        #endregion
    }
}