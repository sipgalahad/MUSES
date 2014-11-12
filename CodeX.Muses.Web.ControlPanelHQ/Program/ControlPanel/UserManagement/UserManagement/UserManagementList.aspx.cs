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
using System.Web.Security;
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanelHQ.Program
{
    public partial class UserManagementList : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.USER_ACCOUNTS;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            hdnFilterExpression.Value = filterExpression;
            hdnID.Value = keyValue;
            filterExpression = GetFilterExpression();
            if (keyValue != "")
            {
                int row = BusinessLayer.GetvUserRowIndex(filterExpression, keyValue) + 1;
                CurrPage = Helper.GetPageCount(row, Constant.GridViewPageSize.GRID_MASTER);
            }
            else
                CurrPage = 1;

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        private string GetFilterExpression()
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            if (!AppSession.UserLogin.IsSysAdmin)
                filterExpression += "UserID NOT IN (SELECT UserID FROM UserInRole WHERE RoleID = 1) AND IsDeleted = 0";
            else if (AppSession.UserLogin.UserID != 1)
                filterExpression += "UserID != 1 AND IsDeleted = 0";
            else
                filterExpression += "IsDeleted = 0";
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvUserRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vUser> lstEntity = BusinessLayer.GetvUserList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        public override void SetFilterParameter(ref string[] fieldListText, ref string[] fieldListValue)
        {
            fieldListText = new string[] { "UserName", "Email", "Full Name" };
            fieldListValue = new string[] { "UserName", "Email", "FullName" };
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

        protected override bool OnAddRecord(ref string url, ref string errMessage)
        {
            url = ResolveUrl("~/Program/ControlPanel/UserManagement/UserManagement/UserManagementEntry.aspx");
            return true;
        }

        protected override bool OnEditRecord(ref string url, ref string errMessage)
        {
            if (hdnID.Value.ToString() != "")
            {
                url = ResolveUrl(string.Format("~/Program/ControlPanel/UserManagement/UserManagement/UserManagementEntry.aspx?id={0}", hdnID.Value));
                return true;
            }
            return false;
        }

        protected override bool OnDeleteRecord(ref string errMessage)
        {
            if (hdnID.Value.ToString() != "")
            {
                int deletedID = Convert.ToInt32(hdnID.Value);
                if (AppSession.UserLogin.UserID == deletedID)
                {
                    errMessage = "Cannot Delete Your Own Account";
                    return false;
                }
                else if (deletedID == 1)
                {
                    errMessage = "Cannot Delete SysAdmin. This account is preloaded by system";
                    return false;
                }
                else
                {
                    UserAttribute ua = BusinessLayer.GetUserAttribute(deletedID);
                    ua.IsDeleted = true;
                    ua.LastUpdatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.UpdateUserAttribute(ua);
                    return true;
                }
            }
            return false;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            if (type == "resetpassword")
            {
                if (hdnID.Value != "")
                {
                    String newPassword = BusinessLayer.GetSettingParameter(Constant.SettingParameter.DEFAULT_PASSWORD).ParameterValue;
                    User entity = BusinessLayer.GetUser(Convert.ToInt32(hdnID.Value));
                    entity.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(newPassword, "sha1");
                    BusinessLayer.UpdateUser(entity);
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}