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

namespace CodeX.Muses.Web.ControlPanelHQ.Program
{
    public partial class UserRolesLoginAttributeEntryCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');
            hdnRoleID.Value = temp[0];
            hdnSiteID.Value = temp[1];

            Site entitySite = BusinessLayer.GetSite(hdnSiteID.Value);
            txtSiteName.Text = entitySite.SiteName;
            UserRole entity = BusinessLayer.GetUserRole(Convert.ToInt32(hdnRoleID.Value));
            txtRoleName.Text = entity.RoleName;
            if (param != "")
            {
                List<vUserRoleLoginAttribute> lstSelected = BusinessLayer.GetvUserRoleLoginAttributeList(string.Format("RoleID = {0} AND SiteID = '{1}'", hdnRoleID.Value, AppSession.UserLogin.SiteID));
                rptSelected.DataSource = lstSelected;
                rptSelected.DataBind();

                hdnSelectedMember.Value = String.Join(",", lstSelected.Select(p => p.LoginAttributeID).ToList());
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
            string filterExpression = string.Format("LoginAttributeCode LIKE '%{0}%' AND LoginAttributeName LIKE '%{1}%' AND IsDeleted = 0", hdnFilterItemCode.Value, hdnFilterItemName.Value);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetLoginAttributeRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 10);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<LoginAttribute> lstEntity = BusinessLayer.GetLoginAttributeList(filterExpression, 10, pageIndex, "");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LoginAttribute entity = e.Row.DataItem as LoginAttribute;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (lstSelectedMember.Contains(entity.LoginAttributeID.ToString()))
                    chkIsSelected.Checked = true;
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            UserRoleLoginAttributeDao entityDtDao = new UserRoleLoginAttributeDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                int RoleID = Convert.ToInt32(hdnRoleID.Value);

                List<UserRoleLoginAttribute> lstUserRoleLoginAttribute = BusinessLayer.GetUserRoleLoginAttributeList(string.Format("RoleID = {0} AND SiteID = '{1}'", RoleID, AppSession.UserLogin.SiteID), ctx);
                int ct = 0;
                foreach (String itemID in lstSelectedMember)
                {
                    int LoginAttributeID = Convert.ToInt32(lstSelectedMember[ct]);
                    UserRoleLoginAttribute entityDt = lstUserRoleLoginAttribute.FirstOrDefault(p => p.LoginAttributeID == LoginAttributeID);
                    if (entityDt == null)
                    {
                        entityDt = new UserRoleLoginAttribute();
                        entityDt.RoleID = RoleID;
                        entityDt.SiteID = AppSession.UserLogin.SiteID;
                        entityDt.LoginAttributeID = LoginAttributeID;
                        entityDtDao.Insert(entityDt);
                    }
                    ct++;
                }
                foreach (UserRoleLoginAttribute entity in lstUserRoleLoginAttribute)
                {
                    if (!lstSelectedMember.Contains(entity.LoginAttributeID.ToString()))
                        entityDtDao.Delete(RoleID, AppSession.UserLogin.SiteID, entity.LoginAttributeID);
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