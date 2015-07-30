using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common;
using CodeX.Common;
using CodeX.Data.Core.Dal;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class ProposedBudgetDetailCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            vProposedBudgetHd entity = BusinessLayer.GetvProposedBudgetHdList(String.Format("ProposedBudgetID = {0}", Convert.ToInt32(hdnID.Value)))[0];
            txtProposedBudgetNo.Text = entity.ProposedBudgetNo;
            txtPosition.Text = entity.Position;
            BindGridView();
        }

        protected string OnGetEmployeeFilterExpression()
        {
            string filterExpression = "";
            return filterExpression;
        }

        private String OnGetFilterExpression()
        {
            String filterExpression = "IsDeleted = 0";
            if (hdnID.Value != "")
                filterExpression += String.Format(" AND ProposedBudgetID = {0}", hdnID.Value);
            else
                filterExpression += String.Format(" AND ProposedBudgetID = 0");
            return filterExpression;
        }

        private void BindGridView()
        {
            String filterExpression = OnGetFilterExpression();

            List<StandardCode> lstFundType = BusinessLayer.GetStandardCodeList(String.Format("IsDeleted = 0 AND IsActive = 1 AND ParentID = '{0}'", Constant.StandardCode.PROJECT_FUNDING));
            rptViewHeader.DataSource = lstFundType.OrderBy(x => x.StandardCodeID);
            rptViewHeader.DataBind();

            List<vProposedBudgetDt> lstProposedBudget = BusinessLayer.GetvProposedBudgetDtList(filterExpression);
            grdView.DataSource = lstProposedBudget;
            grdView.DataBind();
            txtTotalProjectBudget.Text = lstProposedBudget.Sum(x => x.TotalAmount).ToString("N2");
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected void grdView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vProposedBudgetDt entity = e.Item.DataItem as vProposedBudgetDt;
                Repeater rptViewItem = e.Item.FindControl("rptViewItem") as Repeater;
                String[] lst = entity.ListFund.Split('|');
                rptViewItem.DataSource = lst;
                rptViewItem.DataBind();
            }

            if (grdView.Items.Count > 0)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    HtmlTableRow trEmpty = (HtmlTableRow)e.Item.FindControl("trEmpty");
                    trEmpty.Style.Add("Display", "none");
                }
            }
        }
    }
}