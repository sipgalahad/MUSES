using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using DevExpress.Web.ASPxCallbackPanel;
using System.Web.UI.HtmlControls;
using CodeX.Data.Core.Dal;
using CodeX.Muses.Web.Information.Program;
using CodeX.Common;

namespace CodeX.Muses.Web.Information.Program
{
    public partial class TransEmployeeLoanDtCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            String filterExpression = String.Format("TransactionID = {0} ", Convert.ToInt32(param));
            vTransEmployeeLoanHd entityHd = BusinessLayer.GetvTransEmployeeLoanHdList(filterExpression).FirstOrDefault();
            txtHeader.Text = String.Format("{0} - {1}", entityHd.TransactionNo, entityHd.EmployeeName);
            txtTotal.Text = entityHd.TotalAmount.ToString("N");
            hdnPaymentIndex.Value = entityHd.NoOfPayment.ToString();
            hdnTotalAmount.Value = entityHd.TotalAmount.ToString();
            hdnStartPaymentDate.Value = entityHd.StartPaymentDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);

            BindGridView();
        }

        private void BindGridView()
        {
            short indexPayment = 1;
            List<TransEmployeeLoanDt> lstViewDt = new List<TransEmployeeLoanDt>();
            List<TransEmployeeLoanDt> lstTempDt = BusinessLayer.GetTransEmployeeLoanDtList(String.Format("TransactionID = {0} ORDER BY PaymentDate ASC ", Convert.ToInt32(hdnID.Value)));
            foreach (TransEmployeeLoanDt entityDt in lstTempDt)
            {
                entityDt.PaymentIndex = indexPayment;
                lstViewDt.Add(entityDt);
                indexPayment += 1;
            }
            rptView.DataSource = lstViewDt;
            rptView.DataBind();
        }

        protected void rptView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                TransEmployeeLoanDt entity = (TransEmployeeLoanDt)e.Item.DataItem;
                TextBox txtPaymentDate = (TextBox)e.Item.FindControl("txtPaymentDate");
                TextBox txtTransactionAmount = (TextBox)e.Item.FindControl("txtTransactionAmount");

                txtTransactionAmount.Text = entity.TransactionAmount.ToString();
                txtPaymentDate.Text = entity.PaymentDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);

                txtTransactionAmount.Attributes.Add("validationgroup", "mpTrxPopup");
                txtPaymentDate.Attributes.Add("validationgroup", "mpTrxPopup");
                if (entity.IsProcessed)
                {
                    HtmlGenericControl divDelete = (HtmlGenericControl)e.Item.FindControl("divDelete");
                    divDelete.Style.Add("display", "none");
                    txtTransactionAmount.ReadOnly = true;
                    txtPaymentDate.ReadOnly = true;
                }
            }
        }

        protected void cbpPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        #region Process Detail
        protected void cbpProcessPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "generate")
            {
                if (OnGenerateRecordEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else if (param[0] == "save")
            {
                if (OnSaveRecordEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
        private void ControlToEntity(TransEmployeeLoanDt entity)
        {
            
        }

        private bool OnGenerateRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransEmployeeLoanDtDao entityDtDao = new TransEmployeeLoanDtDao(ctx);
            try
            {
                Decimal totalAmount = Convert.ToDecimal(hdnTotalAmount.Value);
                Decimal noOfPayment = Convert.ToDecimal(hdnPaymentIndex.Value);
                Decimal payment = totalAmount / noOfPayment;
                DateTime paymentDate = Helper.GetDatePickerValue(hdnStartPaymentDate.Value);
                
                for (short indexPayment = 1; indexPayment <= noOfPayment; indexPayment++)
                {
                    TransEmployeeLoanDt entityDt = new TransEmployeeLoanDt();
                    entityDt.TransactionID = Convert.ToInt32(hdnID.Value);
                    entityDt.PaymentIndex = indexPayment;
                    entityDt.PaymentDate = paymentDate;
                    entityDt.TransactionAmount = payment;
                    entityDt.IsProcessed = false;
                    entityDtDao.Insert(entityDt);

                    paymentDate = paymentDate.AddMonths(1);
                }
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

        private bool OnSaveRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransEmployeeLoanDtDao entityDtDao = new TransEmployeeLoanDtDao(ctx);
            TransEmployeeLoanHdDao entityHdDao = new TransEmployeeLoanHdDao(ctx);

            try
            {
                List<TransEmployeeLoanDt> lstEntityDt = BusinessLayer.GetTransEmployeeLoanDtList(String.Format("TransactionID = {0}", hdnID.Value),ctx);
                string[] lstSaveValue = hdnSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    short paymentIndex = Convert.ToInt16(temp[0]);
                    DateTime paymentDate = Helper.GetDatePickerValue(temp[1]);
                    Decimal paymentAmount = Convert.ToDecimal(temp[2]);

                    TransEmployeeLoanDt entityDt = lstEntityDt.FirstOrDefault(p => p.PaymentIndex == paymentIndex);
                    if (entityDt == null)
                    {
                        entityDt = new TransEmployeeLoanDt();
                        entityDt.PaymentIndex = paymentIndex;
                        entityDt.PaymentDate = paymentDate;
                        entityDt.TransactionAmount = paymentAmount;
                        entityDt.TransactionID = Convert.ToInt32(hdnID.Value);
                        entityDtDao.Insert(entityDt);
                    }
                    else
                    {
                        entityDt.PaymentDate = paymentDate;
                        entityDt.TransactionAmount = paymentAmount;
                        entityDtDao.Update(entityDt);
                        lstEntityDt.Remove(entityDt);
                    }
                }

                foreach (TransEmployeeLoanDt entityDt in lstEntityDt)
                {
                    entityDtDao.Delete(entityDt.TransactionID, entityDt.PaymentIndex);
                }

                TransEmployeeLoanHd entityHd = entityHdDao.Get(Convert.ToInt32(hdnID.Value));
                entityHd.NoOfPayment = (short)lstSaveValue.Length;
                entityHdDao.Update(entityHd);
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

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                BusinessLayer.DeleteTransEmployeeLoanDt(Convert.ToInt32(hdnID.Value), Convert.ToInt16(hdnPaymentIndex.Value));
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion
    }
}