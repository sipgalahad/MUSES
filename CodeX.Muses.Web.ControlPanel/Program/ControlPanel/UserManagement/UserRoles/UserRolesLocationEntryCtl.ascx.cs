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
    public partial class UserRolesLocationEntryCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        public override void InitializeDataControl(string param)
        {
            hdnRoleID.Value = param;

            UserRole entityHd = BusinessLayer.GetUserRole(Convert.ToInt32(hdnRoleID.Value));
            txtRoleName.Text = entityHd.RoleName;

            if (param != "")
            {
                List<Location> lstSelected = BusinessLayer.GetLocationList(string.Format("LocationID IN (SELECT LocationID FROM LocationUserRole WHERE RoleID = {0} AND IsDeleted = 0) AND SiteID = '{1}'", hdnRoleID.Value, AppSession.UserLogin.SiteID));
                rptSelected.DataSource = lstSelected;
                rptSelected.DataBind();

                hdnSelectedMember.Value = String.Join(",", lstSelected.Select(p => p.LocationID).ToList());
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
            string filterExpression = string.Format("LocationCode LIKE '%{0}%' AND LocationName LIKE '%{1}%' AND SiteID = '{2}' AND IsDeleted = 0", hdnFilterItemCode.Value, hdnFilterItemName.Value, AppSession.UserLogin.SiteID);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetLocationRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 10);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<Location> lstEntity = BusinessLayer.GetLocationList(filterExpression, 10, pageIndex, "");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Location entity = e.Row.DataItem as Location;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (lstSelectedMember.Contains(entity.LocationID.ToString()))
                    chkIsSelected.Checked = true;
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            LocationUserRoleDao entityDtDao = new LocationUserRoleDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                int RoleID = Convert.ToInt32(hdnRoleID.Value);

                List<LocationUserRole> lstLocationUserRole = BusinessLayer.GetLocationUserRoleList(string.Format("RoleID = {0} AND LocationID IN (SELECT LocationID FROM Location WHERE SiteID = '{1}' AND IsDeleted = 0)", RoleID, AppSession.UserLogin.SiteID), ctx);
                int ct = 0;
                foreach (String itemID in lstSelectedMember)
                {
                    int LocationID = Convert.ToInt32(lstSelectedMember[ct]);
                    LocationUserRole entityDt = lstLocationUserRole.FirstOrDefault(p => p.LocationID == LocationID);
                    if (entityDt == null)
                    {
                        entityDt = new LocationUserRole();
                        entityDt.RoleID = RoleID;
                        entityDt.LocationID = LocationID;
                        entityDtDao.Insert(entityDt);
                    }
                    ct++;
                }
                foreach (LocationUserRole entity in lstLocationUserRole)
                {
                    if (!lstSelectedMember.Contains(entity.LocationID.ToString()))
                    {
                        entity.IsDeleted = true;
                        entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDtDao.Update(entity);
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