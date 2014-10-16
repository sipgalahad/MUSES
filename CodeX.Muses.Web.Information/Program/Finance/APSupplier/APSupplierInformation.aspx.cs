using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Data.Model;
using CodeX.Web.Common;
using CodeX.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;
using System.Text;
using CodeX.Data.Core.Dal;
using System.Web.UI.HtmlControls;
using CodeX.Web.CommonLibs.MasterPage;
using CodeX.Common;

namespace CodeX.Muses.Web.Information.Program
{
    public partial class APSupplierInformation : BasePageList
    {
        protected int PageCount = 0;
        protected int RowCount = 0;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;     
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.AP_SUPPLIER_INFORMATION;
        }

        public String GetMovementDate() 
        {

            return Request.Form[hdnMovementDate.UniqueID];
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            txtDateFrom.Text = DateTime.Now.AddDays(-7).ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtDateTo.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            if (isCountPageCount)
            {
                string filterExpression = string.Format("GCBusinessPartnerType = '{0}' AND IsDeleted = 0", Constant.BusinessObjectType.SUPPLIER);
                rowCount = BusinessLayer.GetBusinessPartnersRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 12);
            }

            String MovementDate = String.Format("{0}|{1}", Helper.GetDatePickerValue(txtDateFrom.Text).ToString("yyyyMMdd"), Helper.GetDatePickerValue(txtDateTo.Text).ToString("yyyyMMdd"));
            hdnMovementDate.Value = MovementDate;
            List<GetAPSupplierInformation> lstEntity = BusinessLayer.GetAPSupplierInformationList(MovementDate, pageIndex, 12);
            lvwView.DataSource = lstEntity;
            lvwView.DataBind();
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

        public override Control OnGetExportControl()
        {
            String MovementDate = String.Format("{0}|{1}", Helper.GetDatePickerValue(txtDateFrom.Text).ToString("yyyyMMdd"), Helper.GetDatePickerValue(txtDateTo.Text).ToString("yyyyMMdd"));
            List<GetAPSupplierInformation> lstEntity = BusinessLayer.GetAPSupplierInformationList(MovementDate, 1, 5000);
            lvwView.DataSource = lstEntity;
            lvwView.DataBind();

            HtmlGenericControl div = new HtmlGenericControl("DIV");
            HtmlGenericControl h4 = new HtmlGenericControl("h4");
            h4.InnerHtml = String.Format("Periode : {0} - {1}", Helper.GetDatePickerValue(txtDateFrom.Text).ToString(Constant.FormatString.DATE_FORMAT), Helper.GetDatePickerValue(txtDateTo.Text).ToString(Constant.FormatString.DATE_FORMAT));
            div.Controls.Add(h4);
            div.Controls.Add(pnlGridView);
            return div;
        }
    }
}