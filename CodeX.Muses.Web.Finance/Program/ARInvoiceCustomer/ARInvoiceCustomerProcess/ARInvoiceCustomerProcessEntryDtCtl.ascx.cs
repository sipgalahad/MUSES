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
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        private string[] lstSelectedMember = null;
        
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

            //RowCountPerPage = Constant.GridViewPageSize.GRID_POPUP;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        #region Bind Grid
        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "";
            List<StudentFee> lstStudentFee = BusinessLayer.GetStudentFeeList(String.Format("BusinessPartnerID = {0}",AppSession.BusinessPartnerID));
            if (lstStudentFee.Count() > 0) 
            {
                String lstStudentID = "";
                lstStudentID = String.Join(",", lstStudentFee.GroupBy(s => s.StudentID).Select(x => x.Key));
                filterExpression = string.Format("DueDate BETWEEN '{0}' AND '{1}' AND PayerAmount != 0 AND StudentID IN ({2}) AND StudentFeeDtID NOT IN (SELECT StudentFeeDtID FROM vARInvoiceDt WHERE GCTransactionStatus != '{3}')", Helper.GetDatePickerValue(txtPeriodFrom.Text), Helper.GetDatePickerValue(txtPeriodTo.Text), lstStudentID, Constant.TransactionStatus.VOID);
                List<vStudentFeeDt> lstStudentFeeDt = BusinessLayer.GetvStudentFeeDtList(filterExpression);

                int count = 1;
                List<Student> lstStudent = BusinessLayer.GetStudentList(String.Format("StudentID IN ({0})", lstStudentID));
                var temp = lstStudentFeeDt.GroupBy(x => x.StudentID).Select(m => new { RowID = count++, StudentID = m.Key, StudentName = lstStudent.FirstOrDefault(x => x.StudentID == m.Key).StudentName, PayerAmount = m.Sum(x => x.PayerAmount) });

                if (isCountPageCount)
                {
                    rowCount = temp.Count();
                    pageCount = Helper.GetPageCount(rowCount, 10);
                }
                int currPage = pageCount;
                grdView.DataSource = temp.Where(x => x.RowID >= (currPage * 10) - 10 && (x.RowID <= currPage * 10));
                grdView.DataBind();
            }
        }

        //protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        //vPatientPaymentHd entity = e.Row.DataItem as vPatientPaymentHd;
        //        //CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
        //        //if (lstSelectedMember.Contains(entity.PaymentID.ToString()))
        //        //    chkIsSelected.Checked = true;
        //    }
        //}

        protected void cbpEntryPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount, ref rowCount);
                    result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
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
                if(ARInvoiceID == 0) DetailPage.SaveARInvoiceHd(ctx, ref ARInvoiceID);

                String[] lstStudentID = hdnSelectedMember.Value.Split(',');
                if (lstStudentID.Count() == 2) hdnSelectedMember.Value = lstStudentID[1];
                String filterExpression = string.Format("DueDate BETWEEN '{0}' AND '{1}' AND PayerAmount != 0 AND StudentID IN ({2})", Helper.GetDatePickerValue(txtPeriodFrom.Text), Helper.GetDatePickerValue(txtPeriodTo.Text), hdnSelectedMember.Value);
                List<vStudentFeeDt> lstStudentFeeDt = BusinessLayer.GetvStudentFeeDtList(filterExpression, ctx);
                foreach(vStudentFeeDt obj in lstStudentFeeDt)
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