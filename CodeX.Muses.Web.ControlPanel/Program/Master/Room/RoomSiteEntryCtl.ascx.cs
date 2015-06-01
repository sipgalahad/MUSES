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
    public partial class RoomSiteEntryCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        public override void InitializeDataControl(string param)
        {
            hdnRoomID.Value = param;

            Room entityHd = BusinessLayer.GetRoom(Convert.ToInt32(hdnRoomID.Value));
            txtRoomName.Text = entityHd.RoomName;

            if (param != "")
            {
                List<vRoomSite> lstSelected = BusinessLayer.GetvRoomSiteList(string.Format("RoomID = {0}", hdnRoomID.Value));
                rptSelected.DataSource = lstSelected;
                rptSelected.DataBind();

                hdnSelectedMember.Value = String.Join(",", lstSelected.Select(p => p.SiteID).ToList());
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
            string filterExpression = string.Format("SiteID LIKE '%{0}%' AND SiteName LIKE '%{1}%'", hdnFilterItemCode.Value, hdnFilterItemName.Value);
            return filterExpression;
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
            List<Site> lstEntity = BusinessLayer.GetSiteList(filterExpression, 10, pageIndex, "");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Site entity = e.Row.DataItem as Site;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (lstSelectedMember.Contains(entity.SiteID.ToString()))
                    chkIsSelected.Checked = true;
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            RoomSiteDao entityDtDao = new RoomSiteDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                int RoomID = Convert.ToInt32(hdnRoomID.Value);

                List<RoomSite> lstRoomSite = BusinessLayer.GetRoomSiteList(string.Format("RoomID = {0}", RoomID), ctx);
                if (hdnSelectedMember.Value != "")
                {
                    int ct = 0;
                    foreach (String itemID in lstSelectedMember)
                    {
                        string SiteID = lstSelectedMember[ct];
                        RoomSite entityDt = lstRoomSite.FirstOrDefault(p => p.SiteID == SiteID);
                        if (entityDt == null)
                        {
                            entityDt = new RoomSite();
                            entityDt.RoomID = RoomID;
                            entityDt.SiteID = SiteID;
                            entityDtDao.Insert(entityDt);
                        }
                        else
                            lstRoomSite.Remove(entityDt);
                        ct++;
                    }
                }
                foreach (RoomSite entity in lstRoomSite)
                {
                    entityDtDao.Delete(RoomID, entity.SiteID);
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