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

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class ServiceUnitItemLogisticQuickPicksEntryCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;

        public override void InitializeDataControl(string param)
        {
            BindGridView(1, true, ref PageCount);
        }

        private string GetFilterExpression()
        {
            string GCItemType = string.Format("GCItemType = '{0}'", Constant.ItemType.PRODUCT);
            string filterExpression = string.Format("{0} AND ItemID NOT IN (SELECT ItemID FROM ServiceUnitItemLogistic WHERE SiteServiceUnitID = {1}) AND ItemName1 LIKE '%{2}%' AND ItemGroupID IN (SELECT ItemGroupID FROM ItemGroupMaster WHERE IsOperationalItem = 1 AND IsDeleted = 0) AND IsDeleted = 0", GCItemType, AppSession.SiteServiceUnitID, hdnFilterItem.Value);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetItemMasterRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 10);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<ItemMaster> lstEntity = BusinessLayer.GetItemMasterList(filterExpression, 10, pageIndex, "ItemName1 ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ItemMaster entity = e.Row.DataItem as ItemMaster;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (lstSelectedMember.Contains(entity.ItemID.ToString()))
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
            ServiceUnitItemLogisticDao entityDao = new ServiceUnitItemLogisticDao(ctx);
            bool result = false;
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            try
            {
                foreach (String paramedicID in lstSelectedMember)
                {
                    ServiceUnitItemLogistic entity = new ServiceUnitItemLogistic();
                    entity.SiteServiceUnitID = AppSession.SiteServiceUnitID;
                    entity.ItemID = Convert.ToInt32(paramedicID);
                    entityDao.Insert(entity);
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