using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using CodeX.Data.Core.Dal;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;

namespace CodeX.Muses.Web.Finance.Program
{
    public partial class DirectSalesVoidList : BasePageList
    {
        protected int PageCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Finance.DIRECT_SALES_VOID;
        }

        public override void SetFilterParameter(ref string[] fieldListText, ref string[] fieldListValue)
        {
            fieldListText = new string[] { "No. Pembelian" };
            fieldListValue = new string[] { "SalesInvoiceNo" };
        }

        public override void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowEdit = IsAllowDelete = false;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            BindGridView(1, true, ref PageCount);
        }

        private string GetFilterExpression() 
        {
            string filterExpression = String.Format("GCTransactionStatus = '{0}' AND TransactionCode = '{1}'", Constant.TransactionStatus.CLOSED, Constant.TransactionCode.DIRECT_SALES);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += GetFilterExpression();

            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvSalesInvoiceHdRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vSalesInvoiceHd> lstEntity = BusinessLayer.GetvSalesInvoiceHdList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "SalesInvoiceDate DESC");
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
            bool result = true;
            if (type == "decline")
            {
                IDbContext ctx = DbFactory.Configure(true);
                DirectPaymentHdDao dphdDao = new DirectPaymentHdDao(ctx);
                DirectPaymentDtDao dpdtDao = new DirectPaymentDtDao(ctx);
                SalesInvoiceHdDao entityDao = new SalesInvoiceHdDao(ctx);
                SalesInvoiceDtDao entityDtDao = new SalesInvoiceDtDao(ctx);
                try
                {
                    string[] listParam = hdnParam.Value.Split(',');
                    foreach (string param in listParam)
                    {
                        int ItemRequestID = Convert.ToInt32(param);
                        SalesInvoiceHd entity = entityDao.Get(ItemRequestID);
                        List<SalesInvoiceDt> entityDt = BusinessLayer.GetSalesInvoiceDtList(String.Format("SalesInvoiceID = {0} AND GCItemDetailStatus != '{1}'", entity.SalesInvoiceID, Constant.TransactionStatus.VOID), ctx);
                        DirectPaymentHd paymentHD = BusinessLayer.GetDirectPaymentHdList(String.Format("SalesInvoiceID = {0} AND GCTransactionStatus != '{1}'", entity.SalesInvoiceID, Constant.TransactionStatus.VOID), ctx)[0];
                        List<DirectPaymentDt> lstPaymentDt = BusinessLayer.GetDirectPaymentDtList(String.Format("PaymentID = {0} AND IsDeleted = 0", paymentHD.PaymentID), ctx);

                        foreach (DirectPaymentDt obj in lstPaymentDt)
                        {
                            obj.IsDeleted = true;
                            obj.LastUpdatedBy = AppSession.UserLogin.UserID;
                            dpdtDao.Update(obj);
                        }

                        paymentHD.GCTransactionStatus = Constant.TransactionStatus.VOID;
                        paymentHD.LastUpdatedBy = AppSession.UserLogin.UserID;
                        dphdDao.Update(paymentHD);

                        foreach (SalesInvoiceDt obj in entityDt)
                        {
                            obj.GCItemDetailStatus = Constant.TransactionStatus.VOID;
                            obj.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityDtDao.Update(obj);
                        }

                        entity.GCTransactionStatus = Constant.TransactionStatus.VOID;
                        entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDao.Update(entity);
                    }
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
            }
            return result;
        }
    }
}