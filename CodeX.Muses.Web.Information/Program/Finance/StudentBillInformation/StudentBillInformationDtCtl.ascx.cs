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
    public partial class StudentBillInformationDtCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int CurrPage = 1;

        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');
            hdnStudentID.Value = temp[0];
            hdnViewType.Value = temp[1];

            Student entity = BusinessLayer.GetStudent(Convert.ToInt32(hdnStudentID.Value));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.VirtualAccountNo, entity.StudentName);

            BindGridView(1, true, ref PageCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = "";
            if (hdnViewType.Value == "0")
                filterExpression = String.Format("StudentID = {0} AND TotalStudentAmount != ISNULL(PaymentAmount,0) AND TotalStudentAmount > 0 AND ARInvoiceDtID IS NOT NULL AND IsDeleted = 0", hdnStudentID.Value);
            else
                filterExpression = String.Format("StudentID = {0} AND TotalStudentAmount != ISNULL(PaymentAmount,0) AND TotalStudentAmount > 0 AND IsDeleted = 0", hdnStudentID.Value);
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvStudentFeeDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 10);
            }

            List<vStudentFeeDt> lstEntity = BusinessLayer.GetvStudentFeeDtList(filterExpression, 10, pageIndex, "StudentFeeCompTypeID");
            grdPopupView.DataSource = lstEntity;
            grdPopupView.DataBind();
        }

        protected void cbpPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        public override void SetToolbarVisibility(ref bool IsAllowExport)
        {
            IsAllowExport = true;
        }    
    }
}