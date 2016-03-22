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
using CodeX.Common;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxEditors;
using System.Net;
using CodeX.Data.Core.Dal;

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class RBudgetRealizationConfirmationList : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.RBUDGET_REQUEST_CONFIRMATION;
        }

        protected override void InitializeDataControl()
        {
            RowCountPerPage = Constant.GridViewPageSize.GRID_MATRIX;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        #region Bind Grid View
        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "";
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += String.Format("ProjectID = {0} AND GCTransactionStatus = '{1}'", AppSession.ProjectID, Constant.DistributionStatus.ON_DELIVERY);
            if (AppSession.IsMyProject)
            {
                vRProjectOrganizationMember entityOrganizationMember = BusinessLayer.GetvRProjectOrganizationMemberList(string.Format("ProjectID = {0} AND EmployeeID = {1}", AppSession.ProjectID, AppSession.UserLogin.EmployeeID)).FirstOrDefault();
                filterExpression += string.Format(" AND ProjectTaskGroupID IN (SELECT ProjectTaskGroupID FROM vRProjectTaskAssign WHERE DisplayPath LIKE '%/{0}/%')", entityOrganizationMember.ProjectOrganizationID);
            }

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvRBudgetRealizationHdRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vRBudgetRealizationHd> lstEntity = BusinessLayer.GetvRBudgetRealizationHdList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "RealizationDate DESC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
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
        #endregion

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            if (type == "approve")
            {
                bool result = true;
                IDbContext ctx = DbFactory.Configure(true);
                RBudgetRealizationHdDao distributionHdDao = new RBudgetRealizationHdDao(ctx);
                RBudgetRealizationDtDao distributionDtDao = new RBudgetRealizationDtDao(ctx);
                try
                {
                    string filterExpressionDistributionHd = String.Format("BudgetRealizationID IN ({0})", hdnParam.Value);

                    List<RBudgetRealizationHd> lstRBudgetRealizationHd = BusinessLayer.GetRBudgetRealizationHdList(filterExpressionDistributionHd, ctx);
                    foreach (RBudgetRealizationHd distributionHd in lstRBudgetRealizationHd)
                    {
                        distributionHd.GCTransactionStatus = Constant.DistributionStatus.RECEIVED;
                        distributionHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                        distributionHdDao.Update(distributionHd);
                    }

                    List<RBudgetRealizationDt> lstRBudgetRealizationDt = BusinessLayer.GetRBudgetRealizationDtList(filterExpressionDistributionHd, ctx);
                    foreach (RBudgetRealizationDt distributionDt in lstRBudgetRealizationDt)
                    {
                        distributionDt.GCItemDetailStatus = Constant.DistributionStatus.RECEIVED;
                        distributionDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        distributionDtDao.Update(distributionDt);
                    }
                    ctx.CommitTransaction();
                }
                catch (Exception ex)
                {
                    Helper.InsertErrorLog(ex);
                    errMessage = ex.Message;
                    result = false;
                    ctx.RollBackTransaction();
                }
                finally
                {
                    ctx.Close();
                }
                return result;
            }
            else
            {
                bool result = true;
                IDbContext ctx = DbFactory.Configure(true);
                RBudgetRealizationHdDao distributionHdDao = new RBudgetRealizationHdDao(ctx);
                RBudgetRealizationDtDao distributionDtDao = new RBudgetRealizationDtDao(ctx);
                try
                {
                    string filterExpressionDistributionHd = String.Format("DistributionID IN ({0})", hdnParam.Value);
                    List<RBudgetRealizationHd> lstRBudgetRealizationHd = BusinessLayer.GetRBudgetRealizationHdList(filterExpressionDistributionHd, ctx);

                    foreach (RBudgetRealizationHd distributionHd in lstRBudgetRealizationHd)
                    {
                        distributionHd.GCTransactionStatus = Constant.DistributionStatus.OPEN;
                        distributionHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                        distributionHdDao.Update(distributionHd);
                    }

                    List<RBudgetRealizationDt> lstRBudgetRealizationDt = BusinessLayer.GetRBudgetRealizationDtList(filterExpressionDistributionHd, ctx);
                    foreach (RBudgetRealizationDt distributionDt in lstRBudgetRealizationDt)
                    {
                        distributionDt.GCItemDetailStatus = Constant.DistributionStatus.OPEN;
                        distributionDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        distributionDtDao.Update(distributionDt);
                    }
                    ctx.CommitTransaction();
                }
                catch (Exception ex)
                {
                    Helper.InsertErrorLog(ex);
                    errMessage = ex.Message;
                    result = false;
                    ctx.RollBackTransaction();
                }
                finally
                {
                    ctx.Close();
                }
                return result;
            }
        }
    }
}