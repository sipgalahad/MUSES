using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using CodeX.Common;
using CodeX.Data.Core.Dal;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class ItemSiteEntryCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;

        public override void InitializeDataControl(string param)
        {
            hdnParam.Value = param;
            List<vSiteItem> lstSiteItem = BusinessLayer.GetvSiteItemList(string.Format("ItemID = {0} AND IsDeleted = 0", hdnParam.Value));
            hdnSelectedMember.Value = String.Join(",", lstSiteItem.Select(p => p.SiteID).ToList());

            rptSelected.DataSource = lstSiteItem;
            rptSelected.DataBind();

            BindGridView(1, true, ref PageCount);

            IsAdd = true;
        }

        protected void cbpPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        private string GetFilterExpression()
        {
            return "IsHeader = 0";
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Site entity = e.Row.DataItem as Site;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (lstSelectedMember.Contains(entity.SiteID))
                    chkIsSelected.Checked = true;
            }
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetSiteRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 10);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<Site> lstEntity = BusinessLayer.GetSiteList(filterExpression, 10, pageIndex, "SiteName ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SiteItemDao entityDao = new SiteItemDao(ctx);            
            ItemCostDao entityCostDao = new ItemCostDao(ctx);
            ItemPlanningDao entityPlanningDao = new ItemPlanningDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                int itemID = Convert.ToInt32(hdnParam.Value);

                List<SiteItem> lstSiteItem = BusinessLayer.GetSiteItemList(string.Format("ItemID = {0}", hdnParam.Value), ctx);
                foreach (String siteID in lstSelectedMember)
                {
                    SiteItem entity = lstSiteItem.FirstOrDefault(p => p.SiteID == siteID);
                    if (entity == null)
                    {
                        entity = new SiteItem();
                        entity.ItemID = itemID;
                        entity.SiteID = siteID;
                        entity.IsDeleted = false;
                        entity.CreatedBy = AppSession.UserLogin.UserID;
                        entityDao.Insert(entity);

                        ItemCost ic = new ItemCost();
                        ic.ItemID = itemID;
                        ic.SiteID = siteID;
                        ic.CreatedBy = AppSession.UserLogin.UserID;
                        entityCostDao.Insert(ic);

                        ItemPlanning ip = new ItemPlanning();
                        ip.BusinessPartnerID = null;
                        ip.ItemID = itemID;
                        ip.SiteID = siteID;
                        ip.CreatedBy = AppSession.UserLogin.UserID;
                        entityPlanningDao.Insert(ip);
                    }
                    else if (entity.IsDeleted)
                    {
                        entity.IsDeleted = false;
                        entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDao.Update(entity);
                    }
                }
                foreach (SiteItem entity in lstSiteItem)
                {
                    if (!hdnSelectedMember.Value.Contains(entity.SiteID))
                    {
                        entity.IsDeleted = true;
                        entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDao.Update(entity);
                    }
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                result = false;
                errMessage = ex.Message;
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