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
    public partial class UserTreasuryBookEntryCtl : BaseEntryPopupCtl
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
                List<TreasuryBook> lstSelected = BusinessLayer.GetTreasuryBookList(string.Format("BookID IN (SELECT BookID FROM UserTreasuryBook WHERE SiteID = '{0}' AND UserID = {1})", hdnSiteID.Value, hdnUserID.Value));
                rptSelected.DataSource = lstSelected;
                rptSelected.DataBind();

                hdnSelectedMember.Value = String.Join(",", lstSelected.Select(p => p.BookID).ToList());
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
            string filterExpression = string.Format("TreasuryBookCode LIKE '%{0}%' AND TreasuryBookName LIKE '%{1}%' AND IsDeleted = 0", hdnFilterItemCode.Value, hdnFilterItemName.Value);
            filterExpression += string.Format(" AND BookID IN (SELECT BookID FROM UserRoleTreasuryBook WHERE RoleID IN (SELECT RoleID FROM UserInRole WHERE SiteID = '{0}' AND UserID = {1}) AND IsDeleted = 0)", hdnSiteID.Value, hdnUserID.Value);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetTreasuryBookRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 10);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<TreasuryBook> lstEntity = BusinessLayer.GetTreasuryBookList(filterExpression, 10, pageIndex, "");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TreasuryBook entity = e.Row.DataItem as TreasuryBook;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (lstSelectedMember.Contains(entity.BookID.ToString()))
                    chkIsSelected.Checked = true;
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            UserTreasuryBookDao entityDtDao = new UserTreasuryBookDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                int UserID = Convert.ToInt32(hdnUserID.Value);

                List<UserTreasuryBook> lstUserTreasuryBook = BusinessLayer.GetUserTreasuryBookList(string.Format("UserID = {0} AND SiteID = '{1}'", UserID, hdnSiteID.Value), ctx);
                int ct = 0;
                if (hdnSelectedMember.Value != "")
                {
                    foreach (String itemID in lstSelectedMember)
                    {
                        int BookID = Convert.ToInt32(lstSelectedMember[ct]);
                        UserTreasuryBook entityDt = lstUserTreasuryBook.FirstOrDefault(p => p.BookID == BookID);
                        if (entityDt == null)
                        {
                            entityDt = new UserTreasuryBook();
                            entityDt.UserID = UserID;
                            entityDt.SiteID = hdnSiteID.Value;
                            entityDt.BookID = BookID;
                            entityDtDao.Insert(entityDt);
                        }
                        ct++;
                    }
                }
                foreach (UserTreasuryBook entity in lstUserTreasuryBook)
                {
                    if (!lstSelectedMember.Contains(entity.BookID.ToString()))
                        entityDtDao.Delete(UserID, hdnSiteID.Value, entity.BookID);
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