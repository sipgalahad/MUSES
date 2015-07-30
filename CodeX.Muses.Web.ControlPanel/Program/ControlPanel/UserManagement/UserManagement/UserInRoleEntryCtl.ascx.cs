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
    public partial class UserInRoleEntryCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');
            hdnUserID.Value = temp[0];
            hdnSiteID.Value = temp[1];

            Site entitySite = BusinessLayer.GetSite(hdnSiteID.Value);
            txtSiteName.Text = entitySite.SiteName;
            User entityHd = BusinessLayer.GetUser(Convert.ToInt32(hdnUserID.Value));
            txtUserName.Text = entityHd.UserName;

            if (param != "")
            {
                List<vUserInRole> lstSelected = BusinessLayer.GetvUserInRoleList(string.Format("UserID = {0} AND SiteID = '{1}'", hdnUserID.Value, hdnSiteID.Value));
                rptSelected.DataSource = lstSelected;
                rptSelected.DataBind();

                hdnSelectedMember.Value = String.Join(",", lstSelected.Select(p => p.RoleID).ToList());
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
            string filterExpression = string.Format("RoleName LIKE '%{0}%' AND IsDeleted = 0", hdnFilterItemName.Value);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetUserRoleRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 10);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<UserRole> lstEntity = BusinessLayer.GetUserRoleList(filterExpression, 10, pageIndex, "");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                UserRole entity = e.Row.DataItem as UserRole;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (lstSelectedMember.Contains(entity.RoleID.ToString()))
                    chkIsSelected.Checked = true;
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            UserInRoleDao entityDtDao = new UserInRoleDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                string[] lstSelectedIsMainRole = hdnSelectedIsMainRole.Value.Split(',');
                int UserID = Convert.ToInt32(hdnUserID.Value);

                List<UserInRole> lstUserInRole = BusinessLayer.GetUserInRoleList(string.Format("UserID = {0} AND SiteID = '{1}'", UserID, hdnSiteID.Value), ctx);
                int ct = 0;
                foreach (String itemID in lstSelectedMember)
                {
                    int RoleID = Convert.ToInt32(lstSelectedMember[ct]);
                    UserInRole entityDt = lstUserInRole.FirstOrDefault(p => p.RoleID == RoleID);
                    if (entityDt == null)
                    {
                        entityDt = new UserInRole();
                        entityDt.SiteID = hdnSiteID.Value;
                        entityDt.UserID = UserID;
                        entityDt.IsMainRole = lstSelectedIsMainRole[ct] == "1";
                        entityDt.RoleID = RoleID;
                        entityDtDao.Insert(entityDt);
                    }
                    else
                    {
                        entityDt.IsMainRole = lstSelectedIsMainRole[ct] == "1";
                        entityDtDao.Update(entityDt);
                    }
                    ct++;
                }
                foreach (UserInRole entity in lstUserInRole)
                {
                    if (!lstSelectedMember.Contains(entity.RoleID.ToString()))
                        entityDtDao.Delete(UserID, hdnSiteID.Value, entity.RoleID);
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