using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Model;
using System.Web.UI.HtmlControls;
using System.Text;
using CodeX.Common;

namespace CodeX.Web.CommonLibs.MasterPage
{
    public partial class MPTrx : BaseMP
    {
        private BasePageTrx _basePageEntry;
        private BasePageTrx BasePageEntry
        {
            get
            {
                if (_basePageEntry == null)
                    _basePageEntry = (BasePageTrx)Page;
                return _basePageEntry;
            }
        }

        protected string menuCode = "";
        private GetUserMenuAccess menu;
        protected String GetMenuCaption()
        {
            string menuCaption = BasePageEntry.OnGetMenuCaption();
            if (menuCaption == "")
                return menu.MenuCaption;
            return menuCaption;
        }
        protected String GetBreadcrumbs()
        {
            List<GetUserMenuAccess> lstMenu = ((MPMain)((MPBaseContent)Master).Master).ListMenu;
            StringBuilder result = new StringBuilder();
            List<GetUserMenuAccess> imagesHierarchy = new List<GetUserMenuAccess>();

            GetUserMenuAccess currMenu = lstMenu.FirstOrDefault(p => p.MenuCode == menuCode);
            while (currMenu != null)
            {
                imagesHierarchy.Insert(0, currMenu);
                currMenu = lstMenu.FirstOrDefault(p => p.MenuID == currMenu.ParentID);
            }

            string breadcrumb = "";
            foreach (GetUserMenuAccess menu in imagesHierarchy)
            {
                if (breadcrumb != "")
                    breadcrumb += "<div class='divSeparator'> > </div>";
                breadcrumb += string.Format("<div>{0}</div>", menu.MenuCaption);
            }
            //string breadcrumb = string.Join(" > ", string.Format("<div>{0}</div>", imagesHierarchy.Select(i => i.MenuCaption)));
            return breadcrumb;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bool isAdd = !BasePageEntry.IsLoadFirstRecord;
                hdnIsAdd.Value = isAdd ? "1" : "0";
                int rowCount = BasePageEntry.OnGetRowCount();
                hdnRowCount.Value = rowCount.ToString();

                bool IsAllowAdd, IsAllowSave, IsAllowVoid, IsAllowNextPrev, IsAllowEdit;
                IsAllowAdd = IsAllowSave = IsAllowVoid = IsAllowNextPrev = IsAllowEdit = true;
                BasePageEntry.SetToolbarVisibility(ref IsAllowAdd, ref IsAllowSave, ref IsAllowVoid, ref IsAllowNextPrev);
                if (!IsAllowAdd)
                    btnMPEntryNew.Style.Add("display", "none");
                if (!IsAllowSave)
                    btnMPEntrySave.Style.Add("display", "none");
                if (!IsAllowVoid)
                    btnMPEntryVoid.Style.Add("display", "none");
                if (!IsAllowNextPrev)
                {
                    btnMPEntryNext.Style.Add("display", "none");
                    btnMPEntryPrev.Style.Add("display", "none");
                }
                if (BasePageEntry.IsRefreshControlAfterSaveAddRecord())
                    hdnIsRefreshControlAfterSaveAddRecord.Value = "1";
                else
                    hdnIsRefreshControlAfterSaveAddRecord.Value = "0";

                menuCode = BasePageEntry.OnGetMenuCode();
                menu = ((MPMain)((MPBaseContent)Master).Master).ListMenu.FirstOrDefault(p => p.MenuCode == menuCode);
                string CRUDMode = menu.CRUDMode;

                if (!IsAllowAdd) CRUDMode = CRUDMode.Replace("C", "");
                if (!IsAllowEdit) CRUDMode = CRUDMode.Replace("U", "");
                if (!IsAllowVoid) CRUDMode = CRUDMode.Replace("V", "");
                //if (!IsAllowPrint) CRUDMode = CRUDMode.Replace("P", "");
                hdnIsAllowEdit.Value = CRUDMode.Contains("U") ? "1" : "0";
                hdnIsAllowNextPrev.Value = IsAllowNextPrev ? "1" : "0";
                hdnIsAllowVoid.Value = CRUDMode.Contains("D") ? "1" : "0";
                hdnIsAllowReopen.Value = CRUDMode.Contains("O") ? "1" : "0";

                if (CRUDMode.Contains('A'))
                {
                    hdnProposeText.Value = GetLabel("Approve");
                    hdnIsAllowApprove.Value = "1";
                }
                else if (CRUDMode.Contains('P'))
                {
                    hdnProposeText.Value = GetLabel("Propose");
                    hdnIsAllowApprove.Value = "0";
                    hdnIsAllowPropose.Value = "1";
                }
                else
                    hdnIsAllowPropose.Value = "0";

                foreach (Control c in ulMPTrxToolbar.Controls)
                {
                    if (c is HtmlControl && ((HtmlControl)c).TagName.ToLower() == "li")
                    {
                        HtmlGenericControl li = c as HtmlGenericControl;
                        SetToolbarButtonVisibility(li, CRUDMode);
                    }
                    else if (c is ContentPlaceHolder)
                    {
                        foreach (Control c2 in c.Controls)
                        {
                            if (c2 is HtmlControl && ((HtmlControl)c2).TagName.ToLower() == "li")
                            {
                                HtmlGenericControl li = c2 as HtmlGenericControl;
                                SetToolbarButtonVisibility(li, CRUDMode);
                            }
                        }
                    }
                }
                if (!CRUDMode.Contains("A") && !CRUDMode.Contains("P"))
                    btnMPEntryPropose.Style.Add("display", "none");

                if (rowCount < 1)
                {
                    btnMPEntryNext.Style.Add("display", "none");
                    btnMPEntryPrev.Style.Add("display", "none");
                }
                if (isAdd)
                {
                    btnMPEntryVoid.Style.Add("display", "none");
                    btnMPEntryPropose.Style.Add("display", "none");
                    btnMPEntryReopen.Style.Add("display", "none");
                }
                else
                {
                    if (!CRUDMode.Contains("U"))
                        btnMPEntrySave.Style.Add("display", "none");
                    if (BasePageEntry.isShowWatermark)
                    {
                        hdnWatermark.Value = "1|" + BasePageEntry.watermarkText;
                        btnMPEntryReopen.Style.Remove("display");
                    }
                    else
                    {
                        hdnWatermark.Value = "0";
                        btnMPEntryReopen.Style.Add("display", "none");
                    }
                    hdnPageIndex.Value = BasePageEntry.PageIndex.ToString();
                }

            }
        }

        protected string OnGetMenuCode()
        {
            return BasePageEntry.OnGetMenuCode();
        }

        protected string GetProposeText()
        {
            return hdnProposeText.Value;
        }

        private void SetToolbarButtonVisibility(HtmlGenericControl li, string CRUDMode)
        {
            if (li.Attributes["CRUDMode"] != null)
            {
                string liCRUDMode = li.Attributes["CRUDMode"];
                if (!CRUDMode.Contains(liCRUDMode))
                    li.Style.Add("display", "none");
            }
        }

        protected void cbpMPEntryContent_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] param = e.Parameter.Split('|');
            int pageIndex = Convert.ToInt32(hdnPageIndex.Value);
            int rowCount = Convert.ToInt32(hdnRowCount.Value);
            bool isShowWatermark = false;
            string watermarkText = "";
            if (param[0] == "new")
            {
                BasePageEntry.AddRecord();
                pageIndex = -1;
            }
            else if (param[0] == "next")
                BasePageEntry.NextPageIndex(rowCount, ref pageIndex, ref isShowWatermark, ref watermarkText);
            else if (param[0] == "prev")
                BasePageEntry.PrevPageIndex(rowCount, ref pageIndex, ref isShowWatermark, ref watermarkText);
            else if (param[0] == "load")
                BasePageEntry.LoadPage(pageIndex, ref isShowWatermark, ref watermarkText);
            else if (param[0] == "refresh")
            {
                BasePageEntry.RefreshControl();
                pageIndex = -1;
            }
            else if (param[0] == "loadobject")
                BasePageEntry.LoadPage(param[1], ref pageIndex, ref isShowWatermark, ref watermarkText);

            string cpWatermark = isShowWatermark ? "1" : "0";
            if (isShowWatermark)
                cpWatermark = "1|" + watermarkText;
            else
                cpWatermark = "0";
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpParam"] = param[0];
            panel.JSProperties["cpPageIndex"] = pageIndex;
            panel.JSProperties["cpWatermark"] = cpWatermark;
        }

        protected void cbpMPEntryProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string retval = "";
            string result = "";
            string[] param = e.Parameter.Split('|');
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            if (param[0] == "save")
            {
                bool isAdd = (param[1] == "1");
                BasePageEntry.OnBtnSaveClick(ref result, ref retval, isAdd);
            }
            else if (param[0] == "void")
                BasePageEntry.OnBtnVoidClick(ref result);
            else if (param[0] == "approve")
                BasePageEntry.OnBtnApproveClick(ref result);
            else if (param[0] == "propose")
                BasePageEntry.OnBtnProposeClick(ref result);
            else if (param[0] == "reopen")
                BasePageEntry.OnBtnReopenClick(ref result);
            else if (param[0] == "customclick")
            {
                BasePageEntry.OnBtnCustomClick(ref result, param[1], ref retval);
                panel.JSProperties["cpType"] = param[1];
            }

            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpRetval"] = retval;
        }

        #region Popup List
        protected int PageCount = 0;
        protected int RowCount = 0;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        protected void cbpSearchList_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BasePageEntry.BindSearchList(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BasePageEntry.BindSearchList(1, true, ref pageCount, ref rowCount);
                    result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
        #endregion
    }
}
