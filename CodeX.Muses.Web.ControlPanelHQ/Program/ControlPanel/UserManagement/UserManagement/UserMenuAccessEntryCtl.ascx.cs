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
using CodeX.Data.Core.Dal;
using System.Text;
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanelHQ.Program
{
    public partial class UserMenuAccessEntryCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        public override void InitializeDataControl(string param)
        {
            ListStateMenuAccess.Clear();

            string[] temp = param.Split('|');
            hdnUserID.Value = temp[0];
            hdnSiteID.Value = temp[1];

            Site entitySite = BusinessLayer.GetSite(hdnSiteID.Value);
            txtSiteName.Text = entitySite.SiteName;

            User ur = BusinessLayer.GetUser(Convert.ToInt32(hdnUserID.Value));
            txtUserName.Text = ur.UserName;

            List<Module> lstModule = null;
            if (entitySite.ParentID == "")
                lstModule = BusinessLayer.GetModuleList(string.Format("ModuleID = '{0}'", Constant.Module.CONTROL_PANEL_HQ));
            else
                lstModule = BusinessLayer.GetModuleList(string.Format("ModuleID != '{0}'", Constant.Module.CONTROL_PANEL_HQ));
            Methods.SetComboBoxField<Module>(ddlModule, lstModule, "ModuleName", "ModuleID");
            hdnSelectedModule.Value = lstModule[0].ModuleID;

            ListUserMenuAccess = BusinessLayer.GetUserMenuList(hdnSelectedModule.Value, hdnSiteID.Value, Convert.ToInt32(hdnUserID.Value), AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID);

            BindGridView(1, true, ref PageCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            List<GetUserMenuList> lstEntity = ListUserMenuAccess.Where(p => p.MenuCaption.ToLower().Contains(hdnFilterMenuCaption.Value.ToLower())).ToList();
            if (hdnFilterRead.Value != "")
            {
                bool isReadChecked = (hdnFilterRead.Value == "1");
                lstEntity = lstEntity.Where(p => p.ENABLED == isReadChecked).ToList();
            }
            if (isCountPageCount)
            {
                pageCount = Helper.GetPageCount(lstEntity.Count, Constant.GridViewPageSize.GRID_MATRIX);
            }
            List<GetUserMenuList> lst = lstEntity.Skip((pageIndex - 1) * 10).Take(10).ToList();
            foreach (GetUserMenuList entity in lst)
            {
                CUserMenuAccessState obj = ListStateMenuAccess.FirstOrDefault(p => p.SiteID == entity.SiteID && p.MenuID == entity.MenuID);
                if (obj != null)
                    entity.CRUDModeUser = obj.CRUDMode;
            }

            grdView.DataSource = lst;
            grdView.DataBind();
        }

        protected void cbpMenuAccess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                SetStateMenuAccess();

                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount);
                    result = "changepage";
                }
                else if (param[0] == "changemodule")
                {
                    ListUserMenuAccess = BusinessLayer.GetUserMenuList(hdnSelectedModule.Value, hdnSiteID.Value, Convert.ToInt32(hdnUserID.Value), AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID);
                    BindGridView(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
                else
                {
                    BindGridView(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        protected void cbpMenuAccessProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            SetStateMenuAccess();
            string errMessage = "";
            string result = "";
            if (SaveMenuAccess(ref errMessage))
                result += "success";
            else
                result += "fail|" + errMessage;

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool SaveMenuAccess(ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            bool result = false;
            try
            {
                UserMenuDao entityDao = new UserMenuDao(ctx);

                StringBuilder listMenuAccessID = new StringBuilder();
                List<CUserMenuAccessState> ListState = ListStateMenuAccess.Where(p => p.SiteID == hdnSiteID.Value).ToList();
                if (ListState.Count > 0)
                {
                    foreach (CUserMenuAccessState row in ListState)
                    {
                        if (listMenuAccessID.ToString() != "")
                            listMenuAccessID.Append(",");
                        listMenuAccessID.Append(row.MenuID);
                    }
                    List<UserMenu> lstUserMenu = BusinessLayer.GetUserMenuList(string.Format("UserID = {0} AND SiteID = '{1}' AND MenuID IN ({2})", hdnUserID.Value, hdnSiteID.Value, listMenuAccessID.ToString()), ctx);

                    Int32 UserID = Convert.ToInt32(hdnUserID.Value);
                    foreach (CUserMenuAccessState row in ListState)
                    {
                        UserMenu obj = lstUserMenu.FirstOrDefault(p => p.MenuID == row.MenuID);
                        if (obj != null)
                        {
                            if (obj.CRUDMode != row.CRUDMode)
                            {
                                obj.CRUDMode = row.CRUDMode;
                                obj.LastUpdatedBy = AppSession.UserLogin.UserID;
                                entityDao.Update(obj);
                            }
                        }
                        else
                        {
                            obj = new UserMenu();
                            obj.UserID = UserID;
                            obj.SiteID = hdnSiteID.Value;
                            obj.MenuID = row.MenuID;
                            obj.CRUDMode = row.CRUDMode;
                            obj.CreatedBy = AppSession.UserLogin.UserID;
                            entityDao.Insert(obj);
                        }
                    }
                }
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        private void SetStateMenuAccess()
        {
            string[] listCRUDMode = hdnCRUDMode.Value.Split('|');
            string[] listMenuID = hdnListMenuID.Value.Split('|');

            for (int i = 0; i < listMenuID.Length; ++i)
            {
                if (listMenuID[i] != "")
                {
                    GetUserMenuList lst = ListUserMenuAccess.FirstOrDefault(p => p.SiteID == hdnSiteID.Value && p.MenuID == Convert.ToInt32(listMenuID[i]));
                    if (lst.CRUDModeUser != listCRUDMode[i])
                    {
                        CUserMenuAccessState obj = ListStateMenuAccess.FirstOrDefault(p => p.SiteID == hdnSiteID.Value && p.MenuID == Convert.ToInt32(listMenuID[i]));
                        if (obj == null)
                        {
                            obj = new CUserMenuAccessState();
                            obj.SiteID = hdnSiteID.Value;
                            obj.MenuID = Convert.ToInt32(listMenuID[i]);
                            ListStateMenuAccess.Add(obj);
                        }
                        obj.CRUDMode = listCRUDMode[i];
                    }
                }
            }
        }

        private const string SESSION_NAME_USER_MENU_ACCESS = "UserMenuAccess";
        private const string SESSION_NAME_STATE_MENU_ACCESS = "SelectedUserMenuAccess";
        private static List<CUserMenuAccessState> ListStateMenuAccess
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_NAME_STATE_MENU_ACCESS] == null) HttpContext.Current.Session[SESSION_NAME_STATE_MENU_ACCESS] = new List<CUserMenuAccessState>();
                return (List<CUserMenuAccessState>)HttpContext.Current.Session[SESSION_NAME_STATE_MENU_ACCESS];
            }
            set
            {
                HttpContext.Current.Session[SESSION_NAME_STATE_MENU_ACCESS] = value;
            }
        }
        public static List<GetUserMenuList> ListUserMenuAccess
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_NAME_USER_MENU_ACCESS] == null) HttpContext.Current.Session[SESSION_NAME_USER_MENU_ACCESS] = new List<GetUserMenuList>();
                return (List<GetUserMenuList>)HttpContext.Current.Session[SESSION_NAME_USER_MENU_ACCESS];
            }
            set
            {
                HttpContext.Current.Session[SESSION_NAME_USER_MENU_ACCESS] = value;
            }
        }

        private class CUserMenuAccessState
        {
            public string SiteID { get; set; }
            public int MenuID { get; set; }
            public string CRUDMode { get; set; }
        }

    }
}