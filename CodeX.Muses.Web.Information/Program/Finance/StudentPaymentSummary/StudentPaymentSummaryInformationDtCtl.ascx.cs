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

namespace CodeX.Muses.Web.Information.Program
{
    public partial class StudentPaymentSummaryInformationDtCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int CurrPage = 1;

        public override void InitializeDataControl(string param)
        {
            String[] lstParam = param.Split('|');
            hdnSiteID.Value = lstParam[0];
            hdnMonth.Value = lstParam[1];
            hdnYear.Value = lstParam[2];
            hdnType.Value = lstParam[3];
            hdnStudentFeeCompTypeID.Value = lstParam[4];

            if (hdnType.Value == "ThisMonth")
                txtHeaderText.Text = "Bulan Ini";
            else if (hdnType.Value == "DownPayment")
                txtHeaderText.Text = "Uang Muka";
            else if (hdnType.Value == "ProspectiveStudent")
                txtHeaderText.Text = "Siswa Baru";
            else if (hdnType.Value == "ARStudent")
                txtHeaderText.Text = "Piutang";

            txtHeaderText2.Text = BusinessLayer.GetStudentFeeCompType(Convert.ToInt32(hdnStudentFeeCompTypeID.Value)).StudentFeeCompTypeName;

            BindGridView(1, true, ref PageCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            List<GetStudentReceiveSummaryDt> lstEntity = BusinessLayer.GetStudentReceiveSummaryDt(hdnSiteID.Value, Convert.ToInt32(hdnYear.Value), Convert.ToInt32(hdnMonth.Value), hdnType.Value, Convert.ToInt32(hdnStudentFeeCompTypeID.Value));
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