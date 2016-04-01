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

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class RItemRequestRealizationInformationDtCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        public override void InitializeDataControl(string param)
        {
            hdnItemRequestID.Value = param;
            vItemRequestHd entity = BusinessLayer.GetvItemRequestHdList(string.Format("ItemRequestID = {0}", param))[0];
            EntityToControl(entity);
            RowCountPerPage = Constant.GridViewPageSize.GRID_POPUP;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        private void EntityToControl(vItemRequestHd entity)
        {
            txtItemRequestNo.Text = entity.ItemRequestNo;
            txtItemOrderDate.Text = entity.TransactionDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtItemOrderTime.Text = entity.TransactionTime;

            hdnFromSiteServiceUnitID.Value = entity.FromSiteServiceUnitID.ToString();
            txtFromServiceUnitCode.Text = entity.FromServiceUnitCode;
            txtFromServiceUnitName.Text = entity.FromServiceUnitName;

            hdnFromLocationID.Value = entity.FromLocationID.ToString();
            txtFromLocationCode.Text = entity.FromLocationCode;
            txtFromLocationName.Text = entity.FromLocationName;
            hdnToSiteServiceUnitID.Value = entity.ToSiteServiceUnitID.ToString();
            txtToServiceUnitCode.Text = entity.ToServiceUnitCode;
            txtToServiceUnitName.Text = entity.ToServiceUnitName;
            txtNotes.Text = entity.Remarks;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnItemRequestID.Value != "")
                filterExpression = string.Format("ItemRequestID = {0} AND IsDeleted = 0", hdnItemRequestID.Value);
            
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvItemRequestDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 15);
            }
            List<vItemRequestDt> lstEntity = BusinessLayer.GetvItemRequestDtList(filterExpression, 15, pageIndex, "ItemName1 ASC");
            lstEntityDistribution = BusinessLayer.GetvItemDistributionDtList(filterExpression);
            
            rptView.DataSource = lstEntity;
            rptView.DataBind();
        }

        List<vItemDistributionDt> lstEntityDistribution = null;
        List<StandardCode> lstFundType = null;
        protected void rptView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vItemRequestDt entity = e.Item.DataItem as vItemRequestDt;

                vItemDistributionDt entityDistribution = lstEntityDistribution.FirstOrDefault(p => p.ItemID == entity.ItemID);

                HtmlTableCell tdTotalRequest = e.Item.FindControl("tdTotalRequest") as HtmlTableCell;
                HtmlTableCell tdTotalDistribution = e.Item.FindControl("tdTotalDistribution") as HtmlTableCell;
                decimal totalAmount = 0;
                if (entityDistribution != null)
                    totalAmount = entityDistribution.Quantity;
                tdTotalDistribution.InnerHtml = totalAmount.ToString("N");
                tdTotalRequest.InnerHtml = entity.Quantity.ToString("N");
            }
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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