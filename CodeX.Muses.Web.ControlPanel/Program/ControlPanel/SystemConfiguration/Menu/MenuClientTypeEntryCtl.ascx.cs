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
    public partial class MenuClientTypeEntryCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        public override void InitializeDataControl(string param)
        {
            hdnMenuID.Value = param;

            MenuMaster entityHd = BusinessLayer.GetMenuMaster(Convert.ToInt32(hdnMenuID.Value));
            txtMenuName.Text = entityHd.MenuCaption;

            if (param != "")
            {
                List<vMenuClientType> lstSelected = BusinessLayer.GetvMenuClientTypeList(string.Format("MenuID = {0}", hdnMenuID.Value));
                rptSelected.DataSource = lstSelected;
                rptSelected.DataBind();

                hdnSelectedMember.Value = String.Join(",", lstSelected.Select(p => p.cfGCClientType).ToList());
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
            string filterExpression = string.Format("ParentID = '{0}' AND StandardCodeID LIKE '%{1}%' AND StandardCodeName LIKE '%{2}%' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.CLIENT_TYPE, hdnFilterItemCode.Value, hdnFilterItemName.Value);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetStandardCodeRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 10);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<StandardCode> lstEntity = BusinessLayer.GetStandardCodeList(filterExpression, 10, pageIndex, "");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                StandardCode entity = e.Row.DataItem as StandardCode;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (lstSelectedMember.Contains(entity.cfStandardCodeID.ToString()))
                    chkIsSelected.Checked = true;
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            MenuClientTypeDao entityDtDao = new MenuClientTypeDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                int MenuID = Convert.ToInt32(hdnMenuID.Value);

                List<MenuClientType> lstMenuClientType = BusinessLayer.GetMenuClientTypeList(string.Format("MenuID = {0}", MenuID), ctx);
                int ct = 0;
                if (hdnSelectedMember.Value != "")
                {
                    foreach (String itemID in lstSelectedMember)
                    {
                        string GCClientType = string.Format("{0}^{1}", Constant.StandardCode.CLIENT_TYPE, lstSelectedMember[ct]);
                        MenuClientType entityDt = lstMenuClientType.FirstOrDefault(p => p.GCClientType == GCClientType);
                        if (entityDt == null)
                        {
                            entityDt = new MenuClientType();
                            entityDt.MenuID = MenuID;
                            entityDt.GCClientType = GCClientType;
                            entityDtDao.Insert(entityDt);
                        }
                        ct++;
                    }
                }
                foreach (MenuClientType entity in lstMenuClientType)
                {
                    if (!lstSelectedMember.Contains(entity.GCClientType.ToString()))
                        entityDtDao.Delete(MenuID, entity.GCClientType);
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