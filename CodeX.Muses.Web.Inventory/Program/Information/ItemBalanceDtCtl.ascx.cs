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
using CodeX.Common;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class ItemBalanceDtCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            String[] lstParam = param.Split('|');
            hdnItemID.Value = lstParam[0];
            hdnLstLocationID.Value = lstParam[1];

            ItemMaster im = BusinessLayer.GetItemMaster(Convert.ToInt32(hdnItemID.Value));
            txtItemName.Text = string.Format("{0} - {1}", im.ItemCode, im.ItemName1);

            BindGridView();
        }

        private void BindGridView()
        {
            List<vItemBalance> lstEntity = BusinessLayer.GetvItemBalanceList(string.Format("ItemID = {0} AND LocationID IN ({1}) AND IsDeleted = 0", hdnItemID.Value, hdnLstLocationID.Value));
            grdPopupView.DataSource = lstEntity;
            grdPopupView.DataBind();
        }

        protected void cbpPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}