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
    public partial class ARInvoiceCustomerProcessEntryDtCtl : BaseEntryPopupCtl
    {
        private ARInvoiceCustomerProcessEntry DetailPage
        {
            get { return (ARInvoiceCustomerProcessEntry)Page; }
        }

        public override void InitializeDataControl(string param)
        {
            hdnARInvoiceID.Value = param;

            txtPeriodFrom.Text = DateTime.Now.AddMonths(-1).ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtPeriodTo.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);

            //List<Department> lstDepartment = BusinessLayer.GetDepartmentList("IsActive = 1");
            //lstDepartment.Insert(0, new Department { DepartmentID = "", DepartmentName = "" });
            //Methods.SetComboBoxField<Department>(cboDepartment, lstDepartment, "DepartmentName", "DepartmentID");
            //cboDepartment.SelectedIndex = 0;
        }

        #region Bind Grid
        private void BindGridView()
        {
            string filterExpression = "";
            List<StudentFee> lstStudentFee = BusinessLayer.GetStudentFeeList(String.Format("BusinessPartnerID = {0}",AppSession.BusinessPartnerID));
            if (lstStudentFee.Count() > 0) 
            {
                String lstStudentID = "";
                lstStudentID = String.Join(",", lstStudentFee.GroupBy(s => s.StudentID).Select(x => x.Key));
                filterExpression = string.Format("DueDate BETWEEN '{0}' AND '{1}' AND PayerAmount != 0 AND StudentID IN ({2}) AND StudentFeeDtID NOT IN (SELECT StudentFeeDtID FROM vARInvoiceDt WHERE GCTransactionStatus != '{3}' AND StudentFeeDtID IS NOT NULL)", Helper.GetDatePickerValue(txtPeriodFrom.Text), Helper.GetDatePickerValue(txtPeriodTo.Text), lstStudentID, Constant.TransactionStatus.VOID);
                List<vStudentFeeDt> lstStudentFeeDt = BusinessLayer.GetvStudentFeeDtList(filterExpression);
                grdView.DataSource = lstStudentFeeDt;
                grdView.DataBind();
            }
        }

        protected void cbpEntryPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion

        #region Save Entity
        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ARInvoiceDtDao entityDtDao = new ARInvoiceDtDao(ctx);
            int ARInvoiceID = Convert.ToInt32(hdnARInvoiceID.Value);
            try
            {
                if (hdnSelectedMember.Value != "")
                {
                    if (ARInvoiceID == 0)
                        DetailPage.SaveARInvoiceHd(ctx, ref ARInvoiceID);
                    List<vStudentFeeDt> lstStudentFeeDt = BusinessLayer.GetvStudentFeeDtList(string.Format("StudentFeeDtID IN ({0})", hdnSelectedMember.Value.Substring(1)), ctx);
                    foreach (vStudentFeeDt obj in lstStudentFeeDt)
                    {
                        ARInvoiceDt arInvoiceDt = new ARInvoiceDt();

                        arInvoiceDt.ARInvoiceID = ARInvoiceID;
                        arInvoiceDt.StudentFeeDtID = obj.StudentFeeDtID;
                        arInvoiceDt.StudentFeeCompTypeID = obj.StudentFeeCompTypeID;
                        arInvoiceDt.TransactionAmount = obj.PayerAmount;
                        arInvoiceDt.ClaimedAmount = obj.PayerAmount;
                        arInvoiceDt.DiscountAmount = 0;
                        arInvoiceDt.LastUpdatedBy = arInvoiceDt.CreatedBy = AppSession.UserLogin.UserID;
                        entityDtDao.Insert(arInvoiceDt);
                    }
                }
                retval = ARInvoiceID.ToString();
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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
    }
}