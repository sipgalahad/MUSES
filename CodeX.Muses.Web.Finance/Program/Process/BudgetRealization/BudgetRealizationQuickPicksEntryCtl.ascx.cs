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

namespace CodeX.Muses.Web.Finance.Program
{
    public partial class BudgetRealizationQuickPicksEntryCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        private string[] lstSelectedRequest = null;

        public override void InitializeDataControl(string param)
        {
            hdnProjectID.Value = param;
            BindGridView(1, true, ref PageCount);
        }

        private string GetFilterExpression()
        {
            string filterExpression = String.Format("BudgetRequestID IN ( SELECT BudgetRequestID FROM BudgetRequestHd WHERE GCTransactionStatus IN ('{0}','{1}') AND ProjectID = {2} ) AND IsDeleted = 0  AND (RequestAmount - RealizationAmount) != 0", Constant.TransactionStatus.APPROVED, Constant.TransactionStatus.PROCESSED, hdnProjectID.Value);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvBudgetRequestDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 10);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<vBudgetRequestDt> lstEntity = BusinessLayer.GetvBudgetRequestDtList(filterExpression, 10, pageIndex, "BudgetName ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vBudgetRequestDt entity = e.Row.DataItem as vBudgetRequestDt;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (lstSelectedMember.Contains(entity.BudgetRequestDtID.ToString()))
                    chkIsSelected.Checked = true;
            }
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            BudgetRealizationDtDao entityDtDao = new BudgetRealizationDtDao(ctx);
            
            bool result = false;
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            lstSelectedRequest = hdnSelectedRequest.Value.Split(',');
            try
            {
                int OrderID = 0;
                ((BudgetRealizationEntry)Page).SaveBudgetRealizationHd(ctx, ref OrderID);
                
                int count = 0;
                foreach (String BudgetRequestDtID in lstSelectedMember) 
                {
                    BudgetRealizationDt obj = new BudgetRealizationDt();
                    obj.BudgetRealizationID = OrderID;
                    obj.BudgetRequestDtID = Convert.ToInt32(BudgetRequestDtID);
                    obj.RealizationAmount = Convert.ToDecimal(lstSelectedRequest[count]);
                    obj.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                    obj.IsDeleted = false;
                    obj.CreatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Insert(obj);
                    count++;
                }

                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
    }
}