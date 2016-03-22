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

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class RBudgetRealizationConfirmationDetailCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        public override void InitializeDataControl(string param)
        {
            hdnDistributionID.Value = param;
            vRBudgetRealizationHd entity = BusinessLayer.GetvRBudgetRealizationHdList(string.Format("BudgetRealizationID = {0}", param))[0];
            EntityToControl(entity);
            RowCountPerPage = Constant.GridViewPageSize.GRID_POPUP;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        private void EntityToControl(vRBudgetRealizationHd entity)
        {
            txtBudgetRealizationNo.Text = entity.BudgetRealizationNo;
            txtProjectTaskGroupName.Text = entity.ProjectTaskGroupName;
            txtNotes.Text = entity.Remarks;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnDistributionID.Value != "")
                filterExpression = string.Format("BudgetRealizationID = {0} AND IsDeleted = 0", hdnDistributionID.Value);
            
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetRBudgetRealizationDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 15);
            }
            if (lstFundType == null)
                lstFundType = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PROJECT_FUNDING));

            List<RBudgetRealizationDt> lstEntity = BusinessLayer.GetRBudgetRealizationDtList(filterExpression, 15, pageIndex, "BudgetRealizationDtName ASC");
            if (lstEntity.Count > 0)
            {
                string lstBudgetRealizationDtID = string.Join(",", lstEntity.Select(p => p.BudgetRealizationDtID).ToList());
                lstEntityFund = BusinessLayer.GetRBudgetRealizationDtFundList(string.Format("BudgetRealizationDtID IN ({0})", lstBudgetRealizationDtID));
            }
            else
                lstEntityFund = new List<RBudgetRealizationDtFund>();

            rptViewHeader.DataSource = lstFundType;
            rptViewHeader.DataBind();

            thContainerAmount.ColSpan = lstFundType.Count;
            rptView.DataSource = lstEntity;
            rptView.DataBind();
        }

        List<StandardCode> lstFundType = null;
        List<RBudgetRealizationDtFund> lstEntityFund = null;
        protected void rptView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptViewItem = e.Item.FindControl("rptViewItem") as Repeater;
                rptViewItem.DataSource = lstFundType;
                rptViewItem.DataBind();
            }
        }

        protected void rptViewItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                StandardCode entity = e.Item.DataItem as StandardCode;
                RBudgetRealizationDt entityBudgetRealizationDt = ((RepeaterItem)e.Item.Parent.Parent).DataItem as RBudgetRealizationDt;
                HtmlTableCell tdTotalAmount = e.Item.FindControl("tdTotalAmount") as HtmlTableCell;

                decimal totalAmount = 0;
                tdTotalAmount.Attributes.Add("GCProjectFundType", entity.StandardCodeID);
                RBudgetRealizationDtFund entityFund = lstEntityFund.FirstOrDefault(p => p.BudgetRealizationDtID == entityBudgetRealizationDt.BudgetRealizationDtID && p.GCProjectFundType == entity.StandardCodeID);
                if (entityFund != null)
                    totalAmount = entityFund.TotalAmount;
                else
                    totalAmount = 0;
                tdTotalAmount.InnerHtml = totalAmount.ToString("N");
                tdTotalAmount.Attributes.Add("TotalAmount", totalAmount.ToString());
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