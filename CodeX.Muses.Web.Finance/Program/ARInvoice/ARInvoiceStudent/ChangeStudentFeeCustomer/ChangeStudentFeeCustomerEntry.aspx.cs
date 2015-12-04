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
    public partial class ChangeStudentFeeCustomerEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Finance.CHANGE_STUDENT_FEE_CUSTOMER;
        }

        protected override void InitializeDataControl()
        {
            hdnSiteID.Value = BusinessLayer.GetStudent(AppSession.StudentID).SiteID;

            lstCustomer = BusinessLayer.GetvCustomerList(string.Format("IsDeleted = 0"));
            hdnLstCustomer.Value = string.Join("|", lstCustomer.Select(p => String.Format("{0};{1}", p.BusinessPartnerID, p.IsGeneratePayerAmount ? "1" : "0")));
        }

        #region HTML Getter
        public String OnGetSchoolPeriodFilterExpression() 
        {
            return String.Format("SiteID = '{0}' AND GCSchoolPeriodStatus = '{1}'", hdnSiteID.Value, Constant.SchoolPeriodStatus.START);
        }
        #endregion

        private string GetFilterExpression()
        {
            string filterExpression = String.Format("StudentID = {0} AND GCAdmissionPaymentPeriod = '{1}' AND IsDeleted = 0 AND SchoolPeriodID = {2}", AppSession.StudentID, Constant.AdmissionPaymentPeriod.BULANAN, hdnSchoolPeriodID.Value);
            return filterExpression;
        }

        List<vStudentFee> lstStudentFee = null;
        List<vCustomer> lstCustomer = null;
        public void BindGridView()
        {
            String filterExpression = GetFilterExpression();

            if (lstCustomer == null)
                lstCustomer = BusinessLayer.GetvCustomerList(string.Format("IsDeleted = 0"));
            lstCustomer.Insert(0, new vCustomer { BusinessPartnerID = 0, BusinessPartnerName = "" });
            List<vStudentFeeComp> lstStudentFeeComp = BusinessLayer.GetvStudentFeeCompList(filterExpression);
            String lstStudentFeeCompID = String.Join(",", lstStudentFeeComp.Select(x => x.StudentFeeCompID));
            if (lstStudentFeeCompID != "")
            {
                lstStudentFee = BusinessLayer.GetvStudentFeeList(string.Format("StudentFeeCompID IN ({0}) AND IsPaid = 1 AND IsDeleted = 0", lstStudentFeeCompID));
                rptStudentFeeComp.DataSource = lstStudentFeeComp;
                rptStudentFeeComp.DataBind();
            }
        }

        protected void rptStudentFeeComp_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vStudentFeeComp entity = e.Item.DataItem as vStudentFeeComp;
                List<vStudentFee> lstTemp = lstStudentFee.Where(x => x.StudentFeeCompID == entity.StudentFeeCompID).OrderBy(p => p.DisplayOrder).ToList();
                Repeater rptStudentFee = (Repeater)e.Item.FindControl("rptStudentFee");
                rptStudentFee.DataSource = lstTemp;
                rptStudentFee.DataBind();

                if (lstTemp.Count() > 0)
                {
                    TextBox txtTotalAmount = e.Item.FindControl("txtTotalAmount") as TextBox;
                    txtTotalAmount.Text = entity.TotalAmount.ToString();
                }
            }
        }

        protected void rptStudentFee_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vStudentFee entity = e.Item.DataItem as vStudentFee;
                DropDownList ddlBusinessPartner = (DropDownList)e.Item.FindControl("ddlBusinessPartner");
                Methods.SetComboBoxField<vCustomer>(ddlBusinessPartner, lstCustomer, "BusinessPartnerName", "BusinessPartnerID");
                ddlBusinessPartner.SelectedValue = entity.BusinessPartnerID.ToString();
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected override bool  OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ARInvoiceHdDao arInvoiceHdDao = new ARInvoiceHdDao(ctx);
            StudentFeeCompDao studentFeeCompDao = new StudentFeeCompDao(ctx);
            StudentFeeDao studentFeeDao = new StudentFeeDao(ctx);
            StudentFeeDtDao studentFeeDtDao = new StudentFeeDtDao(ctx);
            try
            {
                List<ARInvoiceHd> lstARInvoiceHd = BusinessLayer.GetARInvoiceHdList(string.Format("StudentID = {0} AND GCTransactionStatus NOT IN ('{1}','{2}') AND TotalPaymentAmount = 0", AppSession.StudentID, Constant.TransactionStatus.CLOSED, Constant.TransactionStatus.VOID), ctx);
                foreach (ARInvoiceHd arInvoiceHD in lstARInvoiceHd)
                {
                    if (BusinessLayer.GetARInvoiceDtRowCount(string.Format("ARInvoiceID = {0} AND StudentFeeDtID IS NOT NULL AND IsDeleted = 0", arInvoiceHD.ARInvoiceID), ctx) > 0)
                    {
                        arInvoiceHD.GCTransactionStatus = Constant.TransactionStatus.VOID;
                        arInvoiceHD.LastUpdatedBy = AppSession.UserLogin.UserID;
                        arInvoiceHdDao.Update(arInvoiceHD);
                    }
                }

                List<StudentFeeComp> lstStudentFeeComp = BusinessLayer.GetStudentFeeCompList(string.Format("StudentFeeCompID IN ({0})", hdnLstStudentFeeCompID.Value), ctx);
                List<StudentFee> lstStudentFee = BusinessLayer.GetStudentFeeList(string.Format("StudentFeeID IN ({0})", hdnLstStudentFeeID.Value), ctx);
                List<StudentFeeDt> lstStudentFeeDt = BusinessLayer.GetStudentFeeDtList(string.Format("StudentFeeID IN ({0}) AND IsDeleted = 0", hdnLstStudentFeeID.Value), ctx);
                string[] lstSaveValue = hdnSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    int studentFeeCompID = Convert.ToInt32(temp[0]);
                    decimal totalAmount = Convert.ToDecimal(temp[1]);
                    string[] lstSaveValue1 = temp[2].Split('^');

                    StudentFeeComp entityStudentFeeComp = lstStudentFeeComp.FirstOrDefault(p => p.StudentFeeCompID == studentFeeCompID);
                    entityStudentFeeComp.TotalAmount = totalAmount;
                    entityStudentFeeComp.LastUpdatedBy = AppSession.UserLogin.UserID;
                    studentFeeCompDao.Update(entityStudentFeeComp);

                    foreach (string saveValue1 in lstSaveValue1)
                    {
                        string[] temp1 = saveValue1.Split(',');
                        int studentFeeID = Convert.ToInt32(temp1[0]);
                        DateTime dueDate = Helper.GetDatePickerValue(temp1[1]);
                        decimal amount = Convert.ToDecimal(temp1[2]);
                        decimal discountAmount = Convert.ToDecimal(temp1[3]);
                        decimal totalDiscountAmount = Convert.ToDecimal(temp1[4]);
                        int businessPartnerID = Convert.ToInt32(temp1[5]);
                        decimal studentAmount = Convert.ToDecimal(temp1[6]);
                        decimal payerAmount = Convert.ToDecimal(temp1[7]);
                        bool isGeneratePayerAmount = temp1[8] == "1";

                        if (businessPartnerID == 0)
                        {
                            StudentFee entityFee = lstStudentFee.FirstOrDefault(x => x.StudentFeeID == studentFeeID);
                            entityFee.DueDate = dueDate;
                            entityFee.BusinessPartnerID = businessPartnerID;
                            if (entityFee.BusinessPartnerID == 0)
                                entityFee.BusinessPartnerID = null;
                            entityFee.TransactionAmount = amount;
                            entityFee.IsDiscountAmountInPercentage = true;
                            entityFee.DiscountAmount = discountAmount;
                            entityFee.TotalDiscountAmount = totalDiscountAmount;
                            entityFee.TotalStudentAmount = entityFee.LineAmount = entityFee.TransactionAmount - entityFee.TotalDiscountAmount;

                            StudentFeeDt entityPayerDt = lstStudentFeeDt.FirstOrDefault(x => x.StudentFeeID == studentFeeID && x.PayerAmount > 0);
                            if (entityPayerDt != null)
                            {
                                entityPayerDt.DueDate = dueDate;
                                entityPayerDt.IsTransactionAmountInPercentage = false;
                                entityPayerDt.TotalStudentAmount = 0;
                                entityPayerDt.PayerAmount = 0;
                                if (entityFee.IsGeneratePayerAmount)
                                    entityPayerDt.LineAmount = entityPayerDt.TransactionAmount = entityPayerDt.StudentAmount = entityPayerDt.TotalStudentAmount = entityFee.PayerAmount;
                                else
                                    entityPayerDt.LineAmount = entityPayerDt.TransactionAmount = entityPayerDt.StudentAmount = entityPayerDt.TotalStudentAmount = 0;
                                entityPayerDt.IsPaid = false;
                                entityPayerDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                                studentFeeDtDao.Update(entityPayerDt);
                                lstStudentFeeDt.Remove(entityPayerDt);
                            }

                            entityFee.StudentAmount = studentAmount + payerAmount;
                            entityFee.PayerAmount = 0;
                            entityFee.IsGeneratePayerAmount = isGeneratePayerAmount;
                            entityFee.LastUpdatedBy = AppSession.UserLogin.UserID;
                            studentFeeDao.Update(entityFee);
                        }
                    }
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