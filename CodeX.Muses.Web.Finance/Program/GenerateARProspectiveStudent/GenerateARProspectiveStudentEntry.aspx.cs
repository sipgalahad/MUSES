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
using System.Web.UI.HtmlControls;
namespace CodeX.Muses.Web.Finance.Program
{
    public partial class GenerateARProspectiveStudentEntry : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Finance.GENERATE_AR_INVOICE_PROSPECTIVE_STUDENT;
        }

        protected string OnGetPeriodAdmissionFilterExpression()
        {
            return string.Format("GCPeriodAdmissionStatus != '{0}'", Constant.SchoolPeriodStatus.VOID);
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<SchoolPeriod> lstSchoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SiteID = '{0}' AND GCSchoolPeriodStatus != '{1}'", AppSession.UserLogin.SiteID, Constant.SchoolPeriodStatus.VOID));
            Methods.SetComboBoxField<SchoolPeriod>(cboShoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
            cboShoolPeriod.SelectedIndex = 0;

            DateTime date = DateTime.Now.AddMonths(1);
            cboMonth.DataSource = Enumerable.Range(1, 12).Select(a => new
            {
                MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(a),
                MonthNumber = a
            });
            cboMonth.TextField = "MonthName";
            cboMonth.ValueField = "MonthNumber";
            cboMonth.EnableCallbackMode = false;
            cboMonth.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboMonth.DropDownStyle = DropDownStyle.DropDownList;
            cboMonth.DataBind();
            cboMonth.Value = date.Month.ToString();

            cboYear.DataSource = Enumerable.Range(DateTime.Now.Year - 1, 2).Reverse();
            cboYear.EnableCallbackMode = false;
            cboYear.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboYear.DropDownStyle = DropDownStyle.DropDownList;
            cboYear.DataBind();
            cboYear.Value = date.Year.ToString();

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        private string GetFilterExpression()
        {
            string filterExpression = "1 = 0";
            if (tacPeriodAdmission.Value != "" && tacPeriodAdmission.Value != "0")
            {
                filterExpression = hdnFilterExpression.Value;
                if (filterExpression != "")
                    filterExpression += " AND ";
                filterExpression += string.Format("PeriodAdmissionID = {0} AND GCRegistrationStatus IN ('{1}','{2}','{3}')", tacPeriodAdmission.Value, Constant.RegistrationStatus.AR_PROCESSED, Constant.RegistrationStatus.PAID, Constant.RegistrationStatus.SETTLED);
            }
            return filterExpression;
        }

        private string[] lstID = null;
        List<vStudentFeeDt> lstStudentFeeDt = null;
        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvRegistrationRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            lstID = hdnSelectedValue.Value.Split(',');
            List<vRegistration> lstEntity = BusinessLayer.GetvRegistrationList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ProspectiveStudentName ASC");
            if (lstEntity.Count > 0)
            {
                string lstProspectiveStudentID = string.Join(",", lstEntity.Select(p => p.ProspectiveStudentID).ToList());
                lstStudentFeeDt = BusinessLayer.GetvStudentFeeDtList(string.Format("ProspectiveStudentID IN ({0}) AND DueDate LIKE '{1}-{2}%' AND StudentFeeDtID NOT IN (SELECT StudentFeeDtID FROM vARInvoiceDt WHERE GCTransactionStatus != '{3}')", lstProspectiveStudentID, cboYear.Value, cboMonth.Value.ToString().PadLeft(2, '0'), Constant.TransactionStatus.VOID));
            }
            else
                lstStudentFeeDt = new List<vStudentFeeDt>();

            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vRegistration entity = (vRegistration)e.Row.DataItem;
                decimal totalAmount = lstStudentFeeDt.Where(p => p.ProspectiveStudentID == entity.ProspectiveStudentID).Sum(p => p.StudentAmount);
                HtmlGenericControl lblStudentAmount = (HtmlGenericControl)e.Row.FindControl("lblStudentAmount");
                lblStudentAmount.InnerHtml = totalAmount.ToString("N");

                CheckBox chkIsSelected = (CheckBox)e.Row.FindControl("chkIsSelected");
                if (totalAmount == 0)
                    chkIsSelected.Visible = false;
                if (lstID.Contains(entity.ProspectiveStudentID.ToString()))
                    chkIsSelected.Checked = true;
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            bool result = true;
            try
            {
                BusinessLayer.GenerateARInvoiceProspectiveStudent(hdnSelectedValue.Value, AppSession.UserLogin.SiteID, Convert.ToInt32(cboMonth.Value), Convert.ToInt32(cboYear.Value), AppSession.UserLogin.UserID, ctx);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }

            return result;
        }
    }
}