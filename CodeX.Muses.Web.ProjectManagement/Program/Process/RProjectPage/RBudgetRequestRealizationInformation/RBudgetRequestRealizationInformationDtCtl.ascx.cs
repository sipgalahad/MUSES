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
    public partial class RBudgetRequestRealizationInformationDtCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        public override void InitializeDataControl(string param)
        {
            hdnBudgetRequestID.Value = param;
            vRBudgetRequestHd entity = BusinessLayer.GetvRBudgetRequestHdList(string.Format("BudgetRequestID = {0}", param))[0];
            EntityToControl(entity);
            RowCountPerPage = Constant.GridViewPageSize.GRID_POPUP;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        private void EntityToControl(vRBudgetRequestHd entity)
        {
            txtBudgetRequestNo.Text = entity.BudgetRequestNo;
            txtProjectTaskGroupName.Text = entity.ProjectTaskGroupName;
            txtNotes.Text = entity.Remarks;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnBudgetRequestID.Value != "")
                filterExpression = string.Format("BudgetRequestID = {0} AND IsDeleted = 0", hdnBudgetRequestID.Value);
            
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetRBudgetRequestDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 15);
            }
            if (lstFundType == null)
                lstFundType = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PROJECT_FUNDING));

            List<RBudgetRequestDt> lstEntity = BusinessLayer.GetRBudgetRequestDtList(filterExpression, 15, pageIndex, "BudgetRequestDtName ASC");
            if (lstEntity.Count > 0)
            {
                string lstBudgetRequestDtID = string.Join(",", lstEntity.Select(p => p.BudgetRequestDtID).ToList());
                lstEntityFund = BusinessLayer.GetRBudgetRequestDtFundList(string.Format("BudgetRequestDtID IN ({0})", lstBudgetRequestDtID));
            }
            else
                lstEntityFund = new List<RBudgetRequestDtFund>();

            lstEntityRealization = BusinessLayer.GetvRBudgetRealizationDtList(filterExpression);
            if (lstEntity.Count > 0)
            {
                string lstBudgetRealizationDtID = string.Join(",", lstEntityRealization.Select(p => p.BudgetRealizationDtID).ToList());
                lstEntityFundRealization = BusinessLayer.GetRBudgetRealizationDtFundList(string.Format("BudgetRealizationDtID IN ({0})", lstBudgetRealizationDtID));
            }
            else
                lstEntityFundRealization = new List<RBudgetRealizationDtFund>();

            rptViewHeader.DataSource = lstFundType;
            rptViewHeader.DataBind();
            rptViewHeader2.DataSource = lstFundType;
            rptViewHeader2.DataBind();

            thContainerAmount2.ColSpan = thContainerAmount.ColSpan = lstFundType.Count;
            thRequest.ColSpan = thRealization.ColSpan = lstFundType.Count + 1;

            rptView.DataSource = lstEntity;
            rptView.DataBind();
        }

        List<vRBudgetRealizationDt> lstEntityRealization = null;
        List<RBudgetRealizationDtFund> lstEntityFundRealization = null;
        List<StandardCode> lstFundType = null;
        List<RBudgetRequestDtFund> lstEntityFund = null;
        protected void rptView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                RBudgetRequestDt entity = e.Item.DataItem as RBudgetRequestDt;

                entityRealization = lstEntityRealization.FirstOrDefault(p => p.BudgetRealizationDtName == entity.BudgetRequestDtName);

                HtmlTableCell tdTotalRealizationAmount = e.Item.FindControl("tdTotalRealizationAmount") as HtmlTableCell;
                decimal totalAmount = 0;
                if (entityRealization != null)
                    totalAmount = entityRealization.TotalAmount;
                tdTotalRealizationAmount.InnerHtml = totalAmount.ToString("N");

                Repeater rptViewItem = e.Item.FindControl("rptViewItem") as Repeater;
                rptViewItem.DataSource = lstFundType;
                rptViewItem.DataBind();
                Repeater rptViewItemRealization = e.Item.FindControl("rptViewItemRealization") as Repeater;
                rptViewItemRealization.DataSource = lstFundType;
                rptViewItemRealization.DataBind();
            }
        }

        vRBudgetRealizationDt entityRealization = null;
        protected void rptViewItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                StandardCode entity = e.Item.DataItem as StandardCode;
                RBudgetRequestDt entityBudgetRequestDt = ((RepeaterItem)e.Item.Parent.Parent).DataItem as RBudgetRequestDt;
                HtmlTableCell tdTotalAmount = e.Item.FindControl("tdTotalAmount") as HtmlTableCell;

                decimal totalAmount = 0;
                tdTotalAmount.Attributes.Add("GCProjectFundType", entity.StandardCodeID);
                RBudgetRequestDtFund entityFund = lstEntityFund.FirstOrDefault(p => p.BudgetRequestDtID == entityBudgetRequestDt.BudgetRequestDtID && p.GCProjectFundType == entity.StandardCodeID);
                if (entityFund != null)
                    totalAmount = entityFund.TotalAmount;
                else
                    totalAmount = 0;
                tdTotalAmount.InnerHtml = totalAmount.ToString("N");
                tdTotalAmount.Attributes.Add("TotalAmount", totalAmount.ToString());
            }
        }

        protected void rptViewItemRealization_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                StandardCode entity = e.Item.DataItem as StandardCode;
                RBudgetRequestDt entityBudgetRequestDt = ((RepeaterItem)e.Item.Parent.Parent).DataItem as RBudgetRequestDt;
                HtmlTableCell tdTotalAmount = e.Item.FindControl("tdTotalAmount") as HtmlTableCell;

                tdTotalAmount.Attributes.Add("GCProjectFundType", entity.StandardCodeID);

                decimal totalAmount = 0;

                
                if (entityRealization != null)
                {
                    RBudgetRealizationDtFund entityFund = lstEntityFundRealization.FirstOrDefault(p => p.BudgetRealizationDtID == entityRealization.BudgetRealizationDtID && p.GCProjectFundType == entity.StandardCodeID);
                    if (entityFund != null)
                        totalAmount = entityFund.TotalAmount;
                    else
                        totalAmount = 0;
                    tdTotalAmount.InnerHtml = totalAmount.ToString("N");
                }
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