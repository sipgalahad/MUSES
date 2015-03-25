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
using CodeX.Common;

namespace CodeX.Muses.Web.Information.Program
{
    public partial class ARProspectiveStudentMovementInformationDtCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        private ARProspectiveStudentInformation DetailPage
        {
            get { return (ARProspectiveStudentInformation)Page; }
        }

        public override void InitializeDataControl(string param)
        {
            String[] lstParam = param.Split('|');
            hdnProspectiveStudentID.Value = lstParam[0];

            ProspectiveStudent im = BusinessLayer.GetProspectiveStudent(Convert.ToInt32(hdnProspectiveStudentID.Value));
            txtItemName.Text = string.Format("{0} - {1}", im.ProspectiveStudentCode, im.ProspectiveStudentName);

            RowCountPerPage = Constant.GridViewPageSize.GRID_POPUP;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            List<string> lst = DetailPage.GetMovementDate().Split('|').ToList();
            string filterExpression = String.Format("MovementDate BETWEEN '{0}' AND '{1}' AND ProspectiveStudentID = {2}", lst[0], lst[1], hdnProspectiveStudentID.Value);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvARMovementRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_POPUP);
            }

            List<vARMovement> lstDistributionDt = BusinessLayer.GetvARMovementList(filterExpression, Constant.GridViewPageSize.GRID_POPUP, pageIndex);
            grdPopupView.DataSource = lstDistributionDt;
            grdPopupView.DataBind();
        }

        protected void cbpPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        public override void SetToolbarVisibility(ref bool IsAllowExport)
        {
            IsAllowExport = true;
        }

        public override string OnGetPageTitle()
        {
            return string.Format("Piutang Pasien Detil - {0}", Request.Form[txtItemName.UniqueID]);
        }

        public override Control OnGetExportControl()
        {
            List<string> lst = DetailPage.GetMovementDate().Split('|').ToList();
            string filterExpression = String.Format("MovementDate BETWEEN '{0}' AND '{1}' AND ProspectiveStudentID = {2}", lst[0], lst[1], hdnProspectiveStudentID.Value);
            List<vARMovement> lstDistributionDt = BusinessLayer.GetvARMovementList(filterExpression);
            grdPopupView.Columns.RemoveAt(6);
            grdPopupView.Columns.RemoveAt(0);
            
            grdPopupView.DataSource = lstDistributionDt;
            grdPopupView.DataBind();
            HtmlGenericControl div = new HtmlGenericControl("DIV");
            HtmlGenericControl h4 = new HtmlGenericControl("h4");
            h4.InnerHtml = String.Format("Siswa : {0}", Request.Form[txtItemName.UniqueID]);
            div.Controls.Add(h4);
            div.Controls.Add(grdPopupView);
            return div;
        }
    }
}