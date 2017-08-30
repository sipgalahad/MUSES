using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Model;
using System.Text;
using System.Web.UI.HtmlControls;
using CodeX.Web.Common;

namespace CodeX.Web.CommonLibs.MasterPage
{
    public partial class MPList : BaseMP
    {
        protected string IntellisenseHints = "";
        protected string TextSearch = "";
        protected string menuCode = "";
        private BasePageList _basePageList;
        private BasePageList BasePageList
        {
            get
            {
                if (_basePageList == null)
                    _basePageList = (BasePageList)Page;
                return _basePageList;
            }
        }

        private GetUserMenuAccess menu;
        protected String GetMenuCaption()
        {
            return menu.MenuCaption;
        }
        protected String GetDBSyncInfoType()
        {
            return BasePageList.OnGetDBSyncInfoType();
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
                bool IsAllowAdd, IsAllowEdit, IsAllowDelete, IsAllowPrint;
                IsAllowAdd = IsAllowEdit = IsAllowDelete = IsAllowPrint = true;
                menuCode = BasePageList.OnGetMenuCode();
                BasePageList.SetCRUDMode(ref IsAllowAdd, ref IsAllowEdit, ref IsAllowDelete);

                menu = ((MPMain)((MPBaseContent)Master).Master).ListMenu.FirstOrDefault(p => p.MenuCode == menuCode);
                string CRUDMode = menu.CRUDMode;

                hdnMenuCaption.Value = menu.MenuCaption;
                if (!IsAllowAdd) CRUDMode = CRUDMode.Replace("C", "");
                if (!IsAllowEdit) CRUDMode = CRUDMode.Replace("U", "");
                if (!IsAllowDelete) CRUDMode = CRUDMode.Replace("D", "");
                if (!IsAllowPrint) CRUDMode = CRUDMode.Replace("E", "");

                foreach (Control c in ulMPListToolbar.Controls)
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

                if (!CRUDMode.Contains("C"))
                    ctxMenuAdd.Attributes.Add("class", "disabled");
                if (!CRUDMode.Contains("U"))
                    ctxMenuEdit.Attributes.Add("class", "disabled");
                if (!CRUDMode.Contains("D"))
                    ctxMenuDelete.Attributes.Add("class", "disabled");
                //if (!IsAllowAdd || (IsAllowAdd && !CRUDMode.Contains("C")))
                //    btnMPListAdd.Style.Add("display", "none");
                //if (!IsAllowEdit || (IsAllowEdit && !CRUDMode.Contains("U")))
                //    btnMPListEdit.Style.Add("display", "none");
                //if (!IsAllowDelete || (IsAllowDelete && !CRUDMode.Contains("D")))
                //    btnMPListDelete.Style.Add("display", "none");
                //if (!IsAllowPrint || (IsAllowPrint && !CRUDMode.Contains("P")))
                //    btnMPListPrint.Style.Add("display", "none");

                PopulateFilterParameter();
                if (Request.Form["txtSearchView"] != null)
                    TextSearch = Request.Form["txtSearchView"].ToString();

                hdnIsHeadQuarterPage.Value = BasePageList.IsHeadQuarterPage() ? "1" : "0";
            }
        }

        private void SetToolbarButtonVisibility(HtmlGenericControl li, string CRUDMode)
        {
            if (li.Attributes["CRUDMode"] != null)
            {
                string liCRUDMode = li.Attributes["CRUDMode"];
                if (!CRUDMode.Contains(liCRUDMode))
                {
                    li.Style.Add("display", "none");
                    li.Attributes.Add("isallow", "0");
                }
                else
                    li.Attributes.Add("isallow", "1");
            }
        }

        protected string OnGetReportCode()
        {
            return BasePageList.OnGetReportCode();
        }

        private void PopulateFilterParameter()
        {
            List<CAdvancedSearch> lstAdvancedSearch = _basePageList.SetFilterParameter();
            if (lstAdvancedSearch == null)
            {
                string[] fieldListText = null;
                string[] fieldListValue = null;
                _basePageList.SetFilterParameter(ref fieldListText, ref fieldListValue);

                if (fieldListText != null && fieldListValue != null)
                {
                    string lstFieldName = "";
                    string searchTooltip = "";
                    lstAdvancedSearch = new List<CAdvancedSearch>();
                    for (int i = 0; i < fieldListText.Length; ++i)
                    {
                        lstAdvancedSearch.Add(new CAdvancedSearch { FieldName = fieldListValue[i], HeaderText = fieldListText[i] });
                        if (lstFieldName != "")
                            lstFieldName += ";";
                        lstFieldName += fieldListValue[i];
                        if (searchTooltip != "")
                            searchTooltip += " / ";
                        searchTooltip += string.Format("'{0}'", fieldListText[i]);
                        //if (IntellisenseHints != "")
                        //    IntellisenseHints += ",";
                        //IntellisenseHints += string.Format("{{ \"text\":\"{0}\",\"fieldName\":\"{1}\",\"description\":\"{2}\" }}", fieldListText[i], fieldListValue[i], "");
                    }
                    hdnMPListQuickSearchFieldName.Value = lstFieldName;
                    divSearchTooltip.Attributes.Add("data-tip", searchTooltip);
                    rptMPListAdvancedSearch.DataSource = lstAdvancedSearch;
                    rptMPListAdvancedSearch.DataBind();
                }
                else
                    divFilter.Visible = false;
            }
            else
            {
                string searchTooltip = "";
                string lstFieldName = "";
                foreach (CAdvancedSearch advancedSearch in lstAdvancedSearch)
                {
                    if (advancedSearch.IsIncludeInQuickSearch)
                    {
                        if (searchTooltip != "")
                            searchTooltip += " / ";
                        searchTooltip += string.Format("'{0}'", advancedSearch.HeaderText);

                        if (lstFieldName != "")
                            lstFieldName += ";";
                        lstFieldName += advancedSearch.FieldName;
                    }

                }
                hdnMPListQuickSearchFieldName.Value = lstFieldName;
                divSearchTooltip.Attributes.Add("data-tip", searchTooltip);
                rptMPListAdvancedSearch.DataSource = lstAdvancedSearch;
                rptMPListAdvancedSearch.DataBind();
            }
        }

        protected void rptMPListAdvancedSearch_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                CAdvancedSearch entity = (CAdvancedSearch)e.Item.DataItem;
                TextBox txtMPListAdvancedSearch = (TextBox)e.Item.FindControl("txtMPListAdvancedSearch");
                TextBox txtMPListAdvancedSearchFromDate = (TextBox)e.Item.FindControl("txtMPListAdvancedSearchFromDate");
                TextBox txtMPListAdvancedSearchToDate = (TextBox)e.Item.FindControl("txtMPListAdvancedSearchToDate");
                HtmlTable tblSearchDialogDate = (HtmlTable)e.Item.FindControl("tblSearchDialogDate");
                if (entity.FieldType == "date")
                {
                    txtMPListAdvancedSearch.Attributes.Add("style", "display:none");
                    tblSearchDialogDate.Attributes.Remove("style");
                    Helper.SetControlEntrySetting(txtMPListAdvancedSearchFromDate, new ControlEntrySetting(true, true, false), "mpMPListAdvancedSearch");
                    Helper.SetControlEntrySetting(txtMPListAdvancedSearchToDate, new ControlEntrySetting(true, true, false), "mpMPListAdvancedSearch");
                }
                else
                {
                    tblSearchDialogDate.Attributes.Add("style", "display:none");
                    txtMPListAdvancedSearch.Attributes.Remove("style");
                }
            }
        }

        protected void cbpMPListProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string retval = "";
            string result = "";
            string url = "";
            string[] param = e.Parameter.Split('|');
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;

            if (param[0] == "add")
                BasePageList.OnBtnAddClick(ref result, ref url);
            else if (param[0] == "edit")
                BasePageList.OnBtnEditClick(ref result, ref url);
            else if (param[0] == "delete")
                BasePageList.OnBtnDeleteClick(ref result);
            else if (param[0] == "sync")
                BasePageList.OnBtnSyncClick(ref result);
            else if (param[0] == "customclick")
            {
                BasePageList.OnBtnCustomClick(ref result, param[1], ref retval);
                panel.JSProperties["cpType"] = param[1];
            }

            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpURL"] = url;
            panel.JSProperties["cpRetval"] = retval;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            bool isShowTitle = true;
            string fileName = "";
            Control controlHtml = BasePageList.OnGetExportControl(ref isShowTitle, ref fileName);
            if (controlHtml == null)
                controlHtml = BasePageList.OnGetExportControl();
            if (fileName == "")
                fileName = hdnMenuCaption.Value;
            Helper.ExportExcel(hdnMenuCaption.Value, hdnMenuCaption.Value, controlHtml, this, isShowTitle);
            //Control control = BasePageList.OnGetExportControl();

            //HtmlGenericControl div = new HtmlGenericControl("DIV");
            //HtmlGenericControl h1Title = new HtmlGenericControl("h1");
            //h1Title.InnerHtml = hdnMenuCaption.Value;
            //div.Controls.Add(h1Title);
            //div.Controls.Add(control);

            ////Response.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}.xls\"", hdnMenuCaption.Value));
            ////Response.Cache.SetCacheability(HttpCacheability.NoCache);
            ////Response.ContentType = "application/vnd.xls";
            ////System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            ////System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            ////div.RenderControl(htmlWrite);
            //////Response.Write(stringWrite.ToString());
            ////Response.Write("<html><head><style type='text/css'>.grdView > tbody > tr > td {color:green; border:1px solid;}</style></head>" + stringWrite.ToString() + "</html>");
            ////Response.End();


            //string attachment = string.Format("attachment;filename=\"{0}.xls\"", hdnMenuCaption.Value);
            //HttpContext.Current.Response.ClearContent();
            //HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            //HttpContext.Current.Response.ContentType = "application/ms-excel";
            //StringWriter stw = new StringWriter();
            //HtmlTextWriter htextw = new HtmlTextWriter(stw);
            //div.RenderControl(htextw);
            //HttpContext.Current.Response.Write(stw.ToString());
            //FileInfo fi = new FileInfo(Server.MapPath(ResolveUrl("~/Libs/Styles/excel.css")));
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //StreamReader sr = fi.OpenText();
            //while (sr.Peek() >= 0)
            //{
            //    sb.Append(sr.ReadLine());
            //}
            //sr.Close();
            //Response.Write("<html><head><style type='text/css'>" + sb.ToString() + "</style></head>" + stw.ToString() + "</html>");
            //stw = null;
            //htextw = null;
            //Response.Flush();
            //Response.End();
        }

    }
}