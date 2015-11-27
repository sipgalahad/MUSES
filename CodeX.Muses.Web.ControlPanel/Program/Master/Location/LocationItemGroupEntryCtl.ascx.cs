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
    public partial class LocationItemGroupEntryCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;

            Location entity = BusinessLayer.GetLocation(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = entity.LocationName;

            if (param != "")
            {
                List<ItemGroupMaster> lstSelected = BusinessLayer.GetItemGroupMasterList(string.Format("ItemGroupID IN (SELECT ItemGroupID FROM LocationItemGroup WHERE LocationID = {0})", hdnID.Value));
                rptSelected.DataSource = lstSelected;
                rptSelected.DataBind();

                hdnSelectedMember.Value = String.Join(",", lstSelected.Select(p => p.ItemGroupID).ToList());
            }

            BindGridView(1, true, ref PageCount);
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
            string filterExpression = string.Format("ItemGroupCode LIKE '%{0}%' AND ItemGroupName1 LIKE '%{1}%' AND IsDeleted = 0", hdnFilterItemCode.Value, hdnFilterItemName.Value);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvItemGroupMasterRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 10);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<vItemGroupMaster> lstEntity = BusinessLayer.GetvItemGroupMasterList(filterExpression, 10, pageIndex, "");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vItemGroupMaster entity = e.Row.DataItem as vItemGroupMaster;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (lstSelectedMember.Contains(entity.ItemGroupID.ToString()))
                    chkIsSelected.Checked = true;
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            LocationItemGroupDao entityDtDao = new LocationItemGroupDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                int locationID = Convert.ToInt32(hdnID.Value);

                List<LocationItemGroup> lstLocationItemGroup = BusinessLayer.GetLocationItemGroupList(string.Format("LocationID = {0}", locationID), ctx);
                int ct = 0;
                if (hdnSelectedMember.Value != "")
                {
                    foreach (String itemID in lstSelectedMember)
                    {
                        int ItemGroupID = Convert.ToInt32(lstSelectedMember[ct]);
                        LocationItemGroup entityDt = lstLocationItemGroup.FirstOrDefault(p => p.ItemGroupID == ItemGroupID);
                        if (entityDt == null)
                        {
                            entityDt = new LocationItemGroup();
                            entityDt.LocationID = locationID;
                            entityDt.ItemGroupID = ItemGroupID;
                            entityDtDao.Insert(entityDt);
                        }
                        ct++;
                    }
                }
                foreach (LocationItemGroup entity in lstLocationItemGroup)
                {
                    if (!lstSelectedMember.Contains(entity.ItemGroupID.ToString()))
                        entityDtDao.Delete(locationID, entity.ItemGroupID);
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