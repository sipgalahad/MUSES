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
            string filterExpression = String.Format("ProspectiveStudentID = {0} AND GCAdmissionPaymentPeriod IN ('{1}','{2}') AND IsDeleted = 0", AppSession.ProspectiveStudentID, Constant.AdmissionPaymentPeriod.TAHUNAN, Constant.AdmissionPaymentPeriod.SEKALI_BAYAR);
            return filterExpression;
        }

        List<vStudentFeeDt> lstStudentFeeDt = null;
        public void BindGridView()
        {
            String filterExpression = GetFilterExpression();
            List<vStudentFee> lstStudentFee = BusinessLayer.GetvStudentFeeList(filterExpression);
            String lstStudentFeeID = String.Join(",", lstStudentFee.Select(x => x.StudentFeeID));
            if (lstStudentFeeID != "")
            {
                lstStudentFeeDt = BusinessLayer.GetvStudentFeeDtList(String.Format("StudentFeeID IN ({0}) AND IsDeleted = 0", lstStudentFeeID));
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
                    HtmlInputHidden hdnTotalAmount = e.Item.FindControl("hdnTotalAmount") as HtmlInputHidden;
                    hdnTotalAmount.Attributes.Add("class", String.Format("hdnTotalAmount{0} hdnTotalAmount", entity.StudentFeeID));
                    Decimal totalAmount = entity.StudentAmount - lstStudentFeeDt.Where(x => x.StudentFeeID == entity.StudentFeeID && x.GCTransactionStatus == Constant.TransactionStatus.CLOSED).Sum(x => x.StudentAmount);
                    hdnTotalAmount.Value = totalAmount.ToString();

                    HtmlTableCell tdTotalAmount = e.Item.FindControl("tdTotalAmount") as HtmlTableCell;
                    tdTotalAmount.InnerHtml = totalAmount.ToString("N");
                }
                else
                {
                    HtmlTableRow trDataHeader = e.Item.FindControl("trDataHeader") as HtmlTableRow;
                    trDataHeader.Style.Add("display", "none");

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
            StudentFeeDtDao studentFeeDtDao = new StudentFeeDtDao(ctx);
            try
            {
                List<ARInvoiceHd> lstARInvoiceHd = BusinessLayer.GetARInvoiceHdList(string.Format("ProspectiveStudentID = {0} AND GCTransactionStatus != '{1}'", AppSession.ProspectiveStudentID, Constant.TransactionStatus.VOID), ctx);
                foreach (ARInvoiceHd arInvoiceHD in lstARInvoiceHd)
                {
                    arInvoiceHD.GCTransactionStatus = Constant.TransactionStatus.VOID;
                    arInvoiceHD.LastUpdatedBy = AppSession.UserLogin.UserID;
                    arInvoiceHdDao.Update(arInvoiceHD);
                }

                List<StudentFeeDt> lstStudentFeeDt = BusinessLayer.GetStudentFeeDtList(string.Format("StudentFeeID IN ({0}) AND IsDeleted = 0", hdnLstStudentFeeID.Value), ctx);
                string[] lstSaveValue = hdnSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    int studentFeeID = Convert.ToInt32(temp[0]);
                    string[] lstSaveValue1 = temp[1].Split('^');

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
                            entityDt.LineAmount = entityDt.StudentAmount = entityDt.TotalTransactionAmount = entityDt.TransactionAmount = transactionAmount;
                            entityDt.IsPaid = false;
                            entityDt.CreatedBy = AppSession.UserLogin.UserID;
                            studentFeeDtDao.Insert(entityDt);
                        }
                        else
                        {
                            entityDt.DisplayOrder = ctr;
                            entityDt.DueDate = dueDate;
                            entityDt.IsTransactionAmountInPercentage = false;
                            entityDt.LineAmount = entityDt.StudentAmount = entityDt.TotalTransactionAmount = entityDt.TransactionAmount = transactionAmount;
                            entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            studentFeeDtDao.Update(entityDt);

                            lstStudentFeeDt.Remove(entityDt);
                        }
                        ctr++;
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