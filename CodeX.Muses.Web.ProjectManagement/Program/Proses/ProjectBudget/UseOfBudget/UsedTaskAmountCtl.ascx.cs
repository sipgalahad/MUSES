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
    public partial class UsedTaskAmountCtl : BaseViewPopupCtl
    {
        String[] data;
        public override void InitializeDataControl(string param)
        {
            data = param.Split('|');
            hdnID.Value = data[0];
            vProjectBudget entity = BusinessLayer.GetvProjectBudgetList(String.Format("BudgetID = {0}", Convert.ToInt32(hdnID.Value)))[0];
            txtProposedBudgetNo.Text = entity.BudgetName;
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
            String filterExpression = string.Format("BudgetID = {0} AND IsDeleted = 0", hdnID.Value);
            return filterExpression;
        }

        private void BindGridView()
        {
            String filterExpression = OnGetFilterExpression();
            List<vProjectTaskBudget> lstEntity = BusinessLayer.GetvProjectTaskBudgetList(filterExpression);
            if (data[1] == "BI") 
            {
                vProjectTaskBudget entity = new vProjectTaskBudget();
                entity.ProjectTaskName = "Lain - Lain";
                vProjectBudget budget = BusinessLayer.GetvProjectBudgetList(String.Format("BudgetID = {0}", Convert.ToInt32(hdnID.Value)))[0];
                entity.UsedBudget = budget.UsedAmount - lstEntity.Sum(x => x.UsedBudget);
                entity.Remarks = "Pengeluaran Lain-lain";
                if(entity.UsedBudget != 0)
                    lstEntity.Add(entity);
            }
            grdView.DataSource = lstEntity;
            grdView.DataBind();

            txtTotalUsedBudget.Text = lstEntity.Sum(x => x.UsedBudget).ToString("N");
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}