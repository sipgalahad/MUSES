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
    public partial class UserRolesTreasuryBookEntryCtl : BaseEntryPopupCtl
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
                List<vUserRoleTreasuryBook> lstSelected = BusinessLayer.GetvUserRoleTreasuryBookList(string.Format("RoleID = {0}", hdnRoleID.Value));
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
            string filterExpression = string.Format("BookCode LIKE '%{0}%' AND BookName LIKE '%{1}%' AND IsDeleted = 0", hdnFilterItemCode.Value, hdnFilterItemName.Value);
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
            UserRoleTreasuryBookDao entityDtDao = new UserRoleTreasuryBookDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                int RoleID = Convert.ToInt32(hdnRoleID.Value);

                List<UserRoleTreasuryBook> lstUserRoleTreasuryBook = BusinessLayer.GetUserRoleTreasuryBookList(string.Format("RoleID = {0} AND SiteID = '{1}'", RoleID, AppSession.UserLogin.SiteID), ctx);
                int ct = 0;
                if (hdnSelectedMember.Value != "")
                {
                    foreach (String itemID in lstSelectedMember)
                    {
                        int BookID = Convert.ToInt32(lstSelectedMember[ct]);
                        UserRoleTreasuryBook entityDt = lstUserRoleTreasuryBook.FirstOrDefault(p => p.BookID == BookID);
                        if (entityDt == null)
                        {
                            entityDt = new UserRoleTreasuryBook();
                            entityDt.RoleID = RoleID;
                            entityDt.SiteID = AppSession.UserLogin.SiteID;
                            entityDt.BookID = BookID;
                            entityDtDao.Insert(entityDt);
                        }
                        ct++;
                    }
                }
                foreach (UserRoleTreasuryBook entity in lstUserRoleTreasuryBook)
                {
                    if (!lstSelectedMember.Contains(entity.BookID.ToString()))
                        entityDtDao.Delete(RoleID, AppSession.UserLogin.SiteID, entity.BookID);
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