using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class ItemDistributionApprovalDetail : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        private string[] lstSelectedMember = null;

        public override string OnGetMenuCode()
        {
            if (Page.Request.QueryString.Count > 0 && Page.Request.QueryString["type"] == "cs")
                return Constant.MenuCode.Inventory.ITEM_DISTRIBUTION_CROSS_SITE_APPROVAL;
            return Constant.MenuCode.Inventory.ITEM_DISTRIBUTION_APPROVAL;
        }

        public override void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowEdit = IsAllowDelete = false;
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowAdd = false;
        }

        protected override void InitializeDataControl()
        {
            hdnDistributionID.Value = Page.Request.QueryString["id"];
            vItemDistributionHd entityItemDistribution = BusinessLayer.GetvItemDistributionHdList(String.Format("DistributionID = '{0}'", Convert.ToInt32(hdnDistributionID.Value)))[0];
            EntityToControl(entityItemDistribution);
        }

        private void EntityToControl(vItemDistributionHd entity)
        {
            hdnDistributionID.Value = entity.DistributionID.ToString();
            txtDistributionNo.Text = entity.DistributionNo;
            txtItemDistributionDate.Text = entity.DeliveryDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtItemDistributionTime.Text = entity.DeliveryTime;

            hdnFromSiteServiceUnitID.Value = entity.FromSiteServiceUnitID.ToString();
            txtFromServiceUnitCode.Text = entity.FromServiceUnitCode;
            txtFromServiceUnitName.Text = entity.FromServiceUnitName;

            hdnFromLocationID.Value = entity.FromLocationID.ToString();
            txtFromLocationCode.Text = entity.FromLocationCode;
            txtFromLocationName.Text = entity.FromLocationName;
            hdnToSiteServiceUnitID.Value = entity.ToSiteServiceUnitID.ToString();
            txtToServiceUnitCode.Text = entity.ToServiceUnitCode;
            txtToServiceUnitName.Text = entity.ToServiceUnitName;
            hdnToLocationID.Value = entity.ToLocationID.ToString();
            txtToLocationCode.Text = entity.ToLocationCode;
            txtToLocationName.Text = entity.ToLocationName;
            txtNotes.Text = entity.DeliveryRemarks;
            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(1, true, ref PageCount, ref RowCount);
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vItemDistributionDt entity = e.Row.DataItem as vItemDistributionDt;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (entity.GCItemDetailStatus == Constant.TransactionStatus.APPROVED || lstSelectedMember.Contains(entity.ID.ToString()))
                    chkIsSelected.Checked = true;
            }
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = string.Format("DistributionID = {0} AND IsDeleted = 0", hdnDistributionID.Value);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvItemDistributionDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<vItemDistributionDt> lstEntity = BusinessLayer.GetvItemDistributionDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
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

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ItemDistributionDtDao itemDtDao = new ItemDistributionDtDao(ctx);
            try
            {
                string filterExpressionSetDefaultDt = String.Format("DistributionID = {0} AND IsDeleted = 0", hdnDistributionID.Value);
                List<ItemDistributionDt> lstItemDistributionDtSetDefault = BusinessLayer.GetItemDistributionDtList(filterExpressionSetDefaultDt);

                string filterExpressionItemDistributionDt = String.Format("ID IN ({0})", hdnSelectedMember.Value.Substring(1));
                List<ItemDistributionDt> lstItemDistributionDt = BusinessLayer.GetItemDistributionDtList(filterExpressionItemDistributionDt);
                
                foreach (ItemDistributionDt itemDt in lstItemDistributionDtSetDefault)
                {
                    if (itemDt.GCItemDetailStatus == Constant.TransactionStatus.APPROVED && lstItemDistributionDt.Where(p => p.ID == itemDt.ID).Count() < 1)
                    {
                        itemDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                        itemDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        itemDtDao.Update(itemDt);
                    }
                }
                
                foreach (ItemDistributionDt itemDt in lstItemDistributionDt)
                {
                    itemDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
                    itemDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    itemDtDao.Update(itemDt);
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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