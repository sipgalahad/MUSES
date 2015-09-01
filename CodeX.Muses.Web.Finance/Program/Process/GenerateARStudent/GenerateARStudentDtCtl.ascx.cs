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

namespace CodeX.Muses.Web.Finance.Program
{
    public partial class GenerateARStudentDtCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');
            hdnStudentID.Value = temp[0];
            hdnMonth.Value = temp[1];
            hdnYear.Value = temp[2];

            Student student = BusinessLayer.GetStudent(Convert.ToInt32(temp[0]));
            txtStudent.Text = string.Format("{0} ({1})", student.StudentName, student.StudentCode);

            DateTime date = new DateTime(Convert.ToInt32(temp[2]), Convert.ToInt32(temp[1]), 1);
            txtPeriod.Text = date.ToString("MMM yyyy");

            RowCountPerPage = Constant.GridViewPageSize.GRID_POPUP;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = string.Format("StudentID = {0} AND DueDate LIKE '{1}-{2}%' AND StudentFeeDtID NOT IN (SELECT StudentFeeDtID FROM vARInvoiceDt WHERE GCTransactionStatus != '{3}' AND StudentFeeDtID IS NOT NULL) AND IsPaid = 0 AND StudentAmount > 0 AND IsDeleted = 0", hdnStudentID.Value, hdnYear.Value, hdnMonth.Value.ToString().PadLeft(2, '0'), Constant.TransactionStatus.VOID);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvStudentFeeDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_POPUP);
            }

            List<vStudentFeeDt> lstEntity = BusinessLayer.GetvStudentFeeDtList(filterExpression, Constant.GridViewPageSize.GRID_POPUP, pageIndex);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

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
    }
}