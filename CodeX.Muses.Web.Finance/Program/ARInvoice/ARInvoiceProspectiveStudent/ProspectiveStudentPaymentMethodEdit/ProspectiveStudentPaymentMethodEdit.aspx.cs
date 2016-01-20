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
using CodeX.Common;
using DevExpress.Web.ASPxEditors;
using System.Globalization;
using CodeX.Data.Core.Dal;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Web.UI.HtmlControls;
namespace CodeX.Muses.Web.Finance.Program
{
    public partial class ProspectiveStudentPaymentMethodEdit : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Finance.PROSPECTIVE_STUDENT_PAYMENT_METHOD_EDIT;
        }

        protected override void InitializeDataControl()
        {
            BindGridView();
        }

        #region HTML Getter
        public String OnGetStudentFilterExpression() 
        {
            return String.Format("IsDeleted = 0 AND SiteID = '{0}'", AppSession.UserLogin.SiteID);
        }
        public String OnGetSchoolPeriodFilterExpression() 
        {
            return String.Format("SiteID = '{0}' AND GCSchoolPeriodStatus = '{1}'",AppSession.UserLogin.SiteID, Constant.SchoolPeriodStatus.START);
        }
        #endregion

        private string GetFilterExpression()
        {
            string filterExpression = String.Format("ProspectiveStudentID = {0} AND GCAdmissionPaymentPeriod IN ('{1}','{2}','{3}') AND IsDeleted = 0", AppSession.ProspectiveStudentID, Constant.AdmissionPaymentPeriod.TAHUNAN, Constant.AdmissionPaymentPeriod.SEKALI_BAYAR, Constant.AdmissionPaymentPeriod.BULANAN);
            return filterExpression;
        }

        List<vCustomer> lstCustomer = null;
        List<vStudentFeeDt> lstStudentFeeDt = null;
        public void BindGridView()
        {
            lstCustomer = BusinessLayer.GetvCustomerList(string.Format("IsDeleted = 0"));
            lstCustomer.Insert(0, new vCustomer { BusinessPartnerID = 0, BusinessPartnerName = "" });

            String filterExpression = GetFilterExpression();
            List<vStudentFee> lstStudentFee = BusinessLayer.GetvStudentFeeList(filterExpression);
            String lstStudentFeeID = String.Join(",", lstStudentFee.Select(x => x.StudentFeeID));
            if (lstStudentFeeID != "")
            {
                lstStudentFeeDt = BusinessLayer.GetvStudentFeeDtList(String.Format("StudentFeeID IN ({0}) AND PayerAmount = 0 AND IsDeleted = 0", lstStudentFeeID));
                rptStudentFeeComp.DataSource = lstStudentFee;
                rptStudentFeeComp.DataBind();
            }
        }

        protected void rptStudentFeeComp_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vStudentFee entity = e.Item.DataItem as vStudentFee;
                List<vStudentFeeDt> lstTemp = lstStudentFeeDt.Where(x => x.StudentFeeID == entity.StudentFeeID && x.GCTransactionStatus != Constant.TransactionStatus.CLOSED).ToList();
                Repeater rptStudentFee = (Repeater)e.Item.FindControl("rptStudentFee");
                rptStudentFee.DataSource = lstTemp;
                rptStudentFee.DataBind();

                if (lstTemp.Count() > 0)
                {
                    decimal paymentAmount = lstStudentFeeDt.Where(x => x.StudentFeeID == entity.StudentFeeID && x.GCTransactionStatus == Constant.TransactionStatus.CLOSED).Sum(x => x.StudentAmount);
                    Decimal totalAmount = entity.LineAmount - paymentAmount;

                    TextBox txtTotalAmount = e.Item.FindControl("txtTotalAmount") as TextBox;
                    TextBox txtTotalPaymentAmount = e.Item.FindControl("txtTotalPaymentAmount") as TextBox;
                    TextBox txtRemainingAmount = e.Item.FindControl("txtRemainingAmount") as TextBox;
                    DropDownList ddlCustomer = e.Item.FindControl("ddlCustomer") as DropDownList;
                    TextBox txtPayerAmount = e.Item.FindControl("txtPayerAmount") as TextBox;
                    txtRemainingAmount.Attributes.Add("class", String.Format("txtRemainingAmount{0} txtRemainingAmount txtCurrency", entity.StudentFeeID));
                    txtTotalAmount.Text = entity.LineAmount.ToString();
                    txtTotalPaymentAmount.Text = paymentAmount.ToString();
                    txtRemainingAmount.Text = totalAmount.ToString();
                    Methods.SetComboBoxField<vCustomer>(ddlCustomer, lstCustomer, "BusinessPartnerName", "BusinessPartnerID");
                    ddlCustomer.SelectedValue = entity.BusinessPartnerID.ToString();
                    txtPayerAmount.Text = entity.PayerAmount.ToString();
                }
                else
                {
                    HtmlTableRow trDataHeader = e.Item.FindControl("trDataHeader") as HtmlTableRow;
                    HtmlTableRow trDataHeader1 = e.Item.FindControl("trDataHeader1") as HtmlTableRow;
                    HtmlTableRow trDataHeader2 = e.Item.FindControl("trDataHeader2") as HtmlTableRow;
                    HtmlTableRow trDataHeader3 = e.Item.FindControl("trDataHeader3") as HtmlTableRow;
                    HtmlTableRow trDataHeader4 = e.Item.FindControl("trDataHeader4") as HtmlTableRow;
                    trDataHeader.Style.Add("display", "none");
                    trDataHeader1.Style.Add("display", "none");
                    trDataHeader2.Style.Add("display", "none");
                    trDataHeader3.Style.Add("display", "none");
                    trDataHeader4.Style.Add("display", "none");

                    HtmlTableRow trDataDetail = e.Item.FindControl("trDataDetail") as HtmlTableRow;
                    trDataDetail.Style.Add("display", "none");
                }
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ARInvoiceHdDao arInvoiceHdDao = new ARInvoiceHdDao(ctx);
            StudentFeeDao studentFeeDao = new StudentFeeDao(ctx);
            StudentFeeDtDao studentFeeDtDao = new StudentFeeDtDao(ctx);
            try
            {
                List<ARInvoiceHd> lstARInvoiceHd = BusinessLayer.GetARInvoiceHdList(string.Format("ProspectiveStudentID = {0} AND GCTransactionStatus NOT IN ('{1}','{2}') AND TotalPaymentAmount = 0", AppSession.ProspectiveStudentID, Constant.TransactionStatus.CLOSED, Constant.TransactionStatus.VOID), ctx);
                foreach (ARInvoiceHd arInvoiceHD in lstARInvoiceHd)
                {
                    if (BusinessLayer.GetARInvoiceDtRowCount(string.Format("ARInvoiceID = {0} AND StudentFeeDtID IS NOT NULL AND IsDeleted = 0", arInvoiceHD.ARInvoiceID), ctx) > 0)
                    {
                        arInvoiceHD.GCTransactionStatus = Constant.TransactionStatus.VOID;
                        arInvoiceHD.LastUpdatedBy = AppSession.UserLogin.UserID;
                        arInvoiceHdDao.Update(arInvoiceHD);
                    }
                }

                List<StudentFee> lstStudentFee = BusinessLayer.GetStudentFeeList(string.Format("StudentFeeID IN ({0})", hdnLstStudentFeeID.Value), ctx);
                List<StudentFeeDt> lstStudentFeeDt = BusinessLayer.GetStudentFeeDtList(string.Format("StudentFeeID IN ({0}) AND IsDeleted = 0", hdnLstStudentFeeID.Value), ctx);
                string[] lstSaveValue = hdnSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    int studentFeeID = Convert.ToInt32(temp[0]);
                    decimal totalAmount = Convert.ToDecimal(temp[1]);
                    int customerID = Convert.ToInt32(temp[2]);
                    decimal payerAmount = Convert.ToDecimal(temp[3]);
                    string[] lstSaveValue1 = temp[4].Split('^');

                    StudentFee entityStudentFee = lstStudentFee.FirstOrDefault(p => p.StudentFeeID == studentFeeID);
                    if (customerID == 0)
                        entityStudentFee.BusinessPartnerID = null;
                    else
                        entityStudentFee.BusinessPartnerID = customerID;
                    entityStudentFee.TransactionAmount = totalAmount;
                    entityStudentFee.PayerAmount = payerAmount;
                    entityStudentFee.TotalStudentAmount = entityStudentFee.StudentAmount = totalAmount - payerAmount;
                    entityStudentFee.LineAmount = entityStudentFee.StudentAmount + entityStudentFee.PayerAmount;
                    entityStudentFee.LastUpdatedBy = AppSession.UserLogin.UserID;
                    studentFeeDao.Update(entityStudentFee);

                    short ctr = 1;
                    foreach (string saveValue1 in lstSaveValue1)
                    {
                        string[] temp1 = saveValue1.Split(',');
                        int studentFeeDtID = Convert.ToInt32(temp1[0]);
                        DateTime dueDate = Helper.GetDatePickerValue(temp1[1]);
                        decimal transactionAmount = Convert.ToDecimal(temp1[2]);

                        StudentFeeDt entityDt = lstStudentFeeDt.FirstOrDefault(x => x.StudentFeeDtID == studentFeeDtID);
                        if (entityDt == null)
                        {
                            entityDt = new StudentFeeDt();
                            entityDt.StudentFeeID = studentFeeID;
                            entityDt.DisplayOrder = ctr;
                            entityDt.DueDate = dueDate;
                            entityDt.IsTransactionAmountInPercentage = false;
                            entityDt.LineAmount = entityDt.StudentAmount = entityDt.TotalStudentAmount = entityDt.TransactionAmount = transactionAmount;
                            entityDt.IsPaid = false;
                            entityDt.CreatedBy = AppSession.UserLogin.UserID;
                            studentFeeDtDao.Insert(entityDt);
                        }
                        else
                        {
                            entityDt.DisplayOrder = ctr;
                            entityDt.DueDate = dueDate;
                            entityDt.IsTransactionAmountInPercentage = false;
                            entityDt.LineAmount = entityDt.StudentAmount = entityDt.TotalStudentAmount = entityDt.TransactionAmount = transactionAmount;
                            entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            studentFeeDtDao.Update(entityDt);

                            lstStudentFeeDt.Remove(entityDt);
                        }
                        ctr++;
                    }
                    StudentFeeDt entityPayerDt = lstStudentFeeDt.FirstOrDefault(x => x.StudentFeeID == studentFeeID && x.PayerAmount > 0);
                    if (entityPayerDt != null)
                    {
                        if (entityStudentFee.TransactionMonth != null)
                        {
                            DateTime dt = new DateTime((int)entityStudentFee.TransactionYear, (int)entityStudentFee.TransactionMonth, 1);
                            entityPayerDt.DueDate = dt;
                        }
                        else
                            entityPayerDt.DueDate = entityStudentFee.DueDate;
                        entityPayerDt.IsTransactionAmountInPercentage = false;
                        entityPayerDt.LineAmount = entityStudentFee.PayerAmount;
                        entityPayerDt.TotalStudentAmount = 0;
                        entityPayerDt.LineAmount = entityPayerDt.TransactionAmount = entityPayerDt.PayerAmount = entityStudentFee.PayerAmount;
                        if (entityStudentFee.PayerAmount == 0)
                            entityStudentFee.IsDeleted = true;
                        entityPayerDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        studentFeeDtDao.Update(entityPayerDt);
                        lstStudentFeeDt.Remove(entityPayerDt);
                    }
                    else
                    {
                        if (entityStudentFee.BusinessPartnerID != null && entityStudentFee.PayerAmount > 0)
                        {
                            entityPayerDt = new StudentFeeDt();
                            entityPayerDt.StudentFeeID = entityStudentFee.StudentFeeID;
                            entityPayerDt.DisplayOrder = 1;
                            if (entityStudentFee.TransactionMonth != null)
                            {
                                DateTime dt = new DateTime((int)entityStudentFee.TransactionYear, (int)entityStudentFee.TransactionMonth, 1);
                                entityPayerDt.DueDate = dt;
                            }
                            else
                                entityPayerDt.DueDate = entityStudentFee.DueDate;
                            entityPayerDt.IsTransactionAmountInPercentage = false;
                            entityPayerDt.TotalStudentAmount = 0;
                            entityPayerDt.LineAmount = entityPayerDt.TransactionAmount = entityPayerDt.PayerAmount = entityStudentFee.PayerAmount;
                            entityPayerDt.CreatedBy = AppSession.UserLogin.UserID;
                            studentFeeDtDao.Insert(entityPayerDt);
                        }
                    }

                }

                foreach (StudentFeeDt entityDt in lstStudentFeeDt)
                {
                    entityDt.IsDeleted = true;
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    studentFeeDtDao.Update(entityDt);
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                ctx.RollBackTransaction();
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
    }
}