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
    public partial class UserLoginAttributeEntryCtl : BaseEntryPopupCtl
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
            User entity = BusinessLayer.GetUser(Convert.ToInt32(hdnUserID.Value));
            txtUserName.Text = entity.UserName;

            if (param != "")
            {
                List<LoginAttribute> lstSelected = BusinessLayer.GetLoginAttributeList(string.Format("LoginAttributeID IN (SELECT LoginAttributeID FROM UserLoginAttribute WHERE SiteID = '{0}' AND UserID = {1})", hdnSiteID.Value, hdnUserID.Value));
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
            filterExpression += string.Format(" AND LoginAttributeID IN (SELECT LoginAttributeID FROM UserRoleLoginAttribute WHERE RoleID IN (SELECT RoleID FROM UserInRole WHERE SiteID = '{0}' AND UserID = {1}) AND IsDeleted = 0)", hdnSiteID.Value, hdnUserID.Value);
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
            UserLoginAttributeDao entityDtDao = new UserLoginAttributeDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                int UserID = Convert.ToInt32(hdnUserID.Value);

                List<UserLoginAttribute> lstUserLoginAttribute = BusinessLayer.GetUserLoginAttributeList(string.Format("UserID = {0} AND SiteID = '{1}'", UserID, hdnSiteID.Value), ctx);
                int ct = 0;
                if (hdnSelectedMember.Value != "")
                {
                    foreach (String itemID in lstSelectedMember)
                    {
                        int LoginAttributeID = Convert.ToInt32(lstSelectedMember[ct]);
                        UserLoginAttribute entityDt = lstUserLoginAttribute.FirstOrDefault(p => p.LoginAttributeID == LoginAttributeID);
                        if (entityDt == null)
                        {
                            entityDt = new UserLoginAttribute();
                            entityDt.UserID = UserID;
                            entityDt.SiteID = hdnSiteID.Value;
                            entityDt.LoginAttributeID = LoginAttributeID;
                            entityDtDao.Insert(entityDt);
                        }
                        ct++;
                    }
                }
                foreach (UserLoginAttribute entity in lstUserLoginAttribute)
                {
                    if (!lstSelectedMember.Contains(entity.LoginAttributeID.ToString()))
                        entityDtDao.Delete(UserID, hdnSiteID.Value, entity.LoginAttributeID);
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