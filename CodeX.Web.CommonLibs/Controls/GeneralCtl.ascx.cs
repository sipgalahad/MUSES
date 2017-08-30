using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using CodeX.Web.Common;
using System.Reflection;
using System.Collections;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common.UI;
using System.Web.UI.HtmlControls;
using System.Web.Script.Serialization;
using CodeX.Common;

namespace CodeX.Web.CommonLibs.Controls
{
    public partial class GeneralCtl : System.Web.UI.UserControl
    {
        protected string TodayDate;
        protected string SiteID;
        protected int UserID;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            LoadRightPanelContent();
        }

        protected string GetLabel(string code)
        {
            BasePage page = (BasePage)this.Page;
            return page.GetLabel(code);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (AppSession.UserLogin != null)
            {
                SiteID = AppSession.UserLogin.SiteID;
                UserID = AppSession.UserLogin.UserID;
            }
            else
            {
                SiteID = "";
                UserID = 0;
            }
            TodayDate = DateTime.Now.ToString("yyyyMMdd");
        }

        private void BindGridView(int type, GridView grdSearch, string param, string searchDialogType, string baseFilterExpression, ref string intellisenseHints, int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            try
            {
                #region Load XML
                if (param == "open")
                {
                    XDocument xdoc = Helper.LoadXMLFile(this, "search_dialog.xml");
                    var tempSearchDialog = (from sd in xdoc.Descendants("searchdialog").Where(p => p.Attribute("type").Value == searchDialogType)
                                            select new
                                            {
                                                SearchDialogBase = sd.Attribute("searchdialogbase") != null ? sd.Attribute("searchdialogbase").Value : "",
                                                FilterExpression = sd.Attribute("filterexpression") != null ? sd.Attribute("filterexpression").Value : ""
                                            }).FirstOrDefault();
                    if (tempSearchDialog == null)
                        throw new Exception(string.Format("Search Dialog with type {0} is not defined", searchDialogType));
                    if (tempSearchDialog.SearchDialogBase == "")
                    {
                        SearchDialogState = (from sd in xdoc.Descendants("searchdialog").Where(p => p.Attribute("type").Value == searchDialogType)
                                             select new CSearchDialogState
                                             {
                                                 MethodName = sd.Attribute("methodname").Value,
                                                 KeyFieldName = sd.Attribute("keyfieldname").Value,
                                                 RowCountMethodName = sd.Attribute("rowcountmethodname") != null ? sd.Attribute("rowcountmethodname").Value : "",
                                                 FilterExpression = "",
                                                 BaseFilterExpression = sd.Attribute("filterexpression") != null ? sd.Attribute("filterexpression").Value : "",
                                                 IsTreeView = sd.Attribute("istreeview") != null ? (sd.Attribute("istreeview").Value == "1") : false,
                                                 OrderByExpression = sd.Attribute("orderbyexpression") != null ? sd.Attribute("orderbyexpression").Value : "",
                                                 //OrderByColumnIndex = sd.Attribute("orderbycolumnindex") != null ? Convert.ToInt32(sd.Attribute("orderbycolumnindex").Value) : 0,
                                                 //OrderByType = sd.Attribute("orderbytype") != null ? sd.Attribute("orderbytype").Value : "ASC",
                                                 GridColumns = (from grd in sd.Descendants("gridcolumn")
                                                                select new GridColumn
                                                                {
                                                                    DataField = grd.Attribute("datafield").Value,
                                                                    HeaderText = grd.Attribute("headertext").Value,
                                                                    Width = grd.Attribute("width").Value,
                                                                    DisplayCustomField = grd.Attribute("displaycustomfield") != null ? grd.Attribute("displaycustomfield").Value : null,
                                                                    HorizontalAlign = grd.Attribute("horizontalalign") != null ? grd.Attribute("horizontalalign").Value : "left"
                                                                }).ToList<GridColumn>(),
                                                 IntellisenseTexts = (from itx in sd.Descendants("intellisensetext")
                                                                      select new QuickSearchIntellisense
                                                                      {
                                                                          DataField = itx.Attribute("datafield").Value,
                                                                          HeaderText = itx.Attribute("headertext").Value,
                                                                          FieldType = itx.Attribute("fieldtype") != null ? itx.Attribute("fieldtype").Value : "text",
                                                                          IsIncludeInQuickSearch = itx.Attribute("isincludeinquicksearch") != null ? itx.Attribute("isincludeinquicksearch").Value == "1" : true,
                                                                          Description = itx.Attribute("description") != null ? itx.Attribute("description").Value : ""
                                                                      }).ToList<QuickSearchIntellisense>(),
                                                 ConditionalStyles = (from itx in sd.Descendants("conditionalstyle")
                                                                      select new ConditionalStyle
                                                                      {
                                                                          DataField = itx.Attribute("datafield").Value,
                                                                          Style = itx.Attribute("style").Value
                                                                      }).ToList<ConditionalStyle>(),
                                                 DetailTableSearchs = (from itx in sd.Descendants("detailtablesearch")
                                                                       select new DetailTableSearch
                                                                       {
                                                                           DataField = itx.Attribute("datafield").Value,
                                                                           TableName = itx.Attribute("tablename").Value,
                                                                           BaseTableKeyFieldName = itx.Attribute("basetablekeyfieldname").Value,
                                                                           FilterExpression = itx.Attribute("filterexpression").Value,
                                                                           HeaderText = itx.Attribute("headertext").Value,
                                                                           IsIncludeInQuickSearch = itx.Attribute("isincludeinquicksearch") != null ? itx.Attribute("isincludeinquicksearch").Value == "1" : true,
                                                                           KeyFieldName = itx.Attribute("keyfieldname").Value
                                                                       }).ToList<DetailTableSearch>()

                                             }).FirstOrDefault();
                    }
                    else
                    {
                        SearchDialogState = (from sd in xdoc.Descendants("searchdialog").Where(p => p.Attribute("type").Value == tempSearchDialog.SearchDialogBase)
                                             select new CSearchDialogState
                                             {
                                                 MethodName = sd.Attribute("methodname").Value,
                                                 KeyFieldName = sd.Attribute("keyfieldname").Value,
                                                 RowCountMethodName = sd.Attribute("rowcountmethodname") != null ? sd.Attribute("rowcountmethodname").Value : "",
                                                 FilterExpression = "",
                                                 BaseFilterExpression = tempSearchDialog.FilterExpression,
                                                 IsTreeView = sd.Attribute("istreeview") != null ? (sd.Attribute("istreeview").Value == "1") : false,
                                                 OrderByExpression = sd.Attribute("orderbyexpression") != null ? sd.Attribute("orderbyexpression").Value : "",
                                                 //OrderByColumnIndex = sd.Attribute("orderbycolumnindex") != null ? Convert.ToInt32(sd.Attribute("orderbycolumnindex").Value) : 0,
                                                 //OrderByType = sd.Attribute("orderbytype") != null ? sd.Attribute("orderbytype").Value : "ASC",
                                                 GridColumns = (from grd in sd.Descendants("gridcolumn")
                                                                select new GridColumn
                                                                {
                                                                    DataField = grd.Attribute("datafield").Value,
                                                                    HeaderText = grd.Attribute("headertext").Value,
                                                                    Width = grd.Attribute("width").Value,
                                                                    DisplayCustomField = grd.Attribute("displaycustomfield") != null ? grd.Attribute("displaycustomfield").Value : null,
                                                                    HorizontalAlign = grd.Attribute("horizontalalign") != null ? grd.Attribute("horizontalalign").Value : "left"
                                                                }).ToList<GridColumn>(),
                                                 IntellisenseTexts = (from itx in sd.Descendants("intellisensetext")
                                                                      select new QuickSearchIntellisense
                                                                      {
                                                                          DataField = itx.Attribute("datafield").Value,
                                                                          HeaderText = itx.Attribute("headertext").Value,
                                                                          FieldType = itx.Attribute("fieldtype") != null ? itx.Attribute("fieldtype").Value : "text",
                                                                          IsIncludeInQuickSearch = itx.Attribute("isincludeinquicksearch") != null ? itx.Attribute("isincludeinquicksearch").Value == "1" : true,
                                                                          Description = itx.Attribute("description").Value != null ? itx.Attribute("description").Value : ""
                                                                      }).ToList<QuickSearchIntellisense>(),
                                                 ConditionalStyles = (from itx in sd.Descendants("conditionalstyle")
                                                                      select new ConditionalStyle
                                                                      {
                                                                          DataField = itx.Attribute("datafield").Value,
                                                                          Style = itx.Attribute("style").Value
                                                                      }).ToList<ConditionalStyle>(),
                                                 DetailTableSearchs = (from itx in sd.Descendants("detailtablesearch")
                                                                       select new DetailTableSearch
                                                                       {
                                                                           DataField = itx.Attribute("datafield").Value,
                                                                           TableName = itx.Attribute("tablename").Value,
                                                                           BaseTableKeyFieldName = itx.Attribute("basetablekeyfieldname").Value,
                                                                           FilterExpression = itx.Attribute("filterexpression").Value,
                                                                           HeaderText = itx.Attribute("headertext").Value,
                                                                           IsIncludeInQuickSearch = itx.Attribute("isincludeinquicksearch") != null ? itx.Attribute("isincludeinquicksearch").Value == "1" : true,
                                                                           KeyFieldName = itx.Attribute("keyfieldname").Value
                                                                       }).ToList<DetailTableSearch>()

                                             }).FirstOrDefault();
                    }

                    SearchDialogState.BaseFilterExpression = SearchDialogState.BaseFilterExpression.Replace("@SiteID", AppSession.UserLogin.SiteID);
                    if (baseFilterExpression != "")
                    {
                        if (SearchDialogState.BaseFilterExpression == "")
                            SearchDialogState.BaseFilterExpression = baseFilterExpression;
                        else
                            SearchDialogState.BaseFilterExpression += string.Format(" AND {0}", baseFilterExpression);
                    }
                }
                #endregion

                #region Bind Grid View
                string filterExpression = "";
                if (type == 1)
                {
                    filterExpression = SearchDialogState.BaseFilterExpression;
                    if (SearchDialogState.FilterExpression != "")
                    {
                        if (filterExpression != "" && filterExpression.Substring(filterExpression.Length - 1) != ";")
                            filterExpression += " AND ";
                        filterExpression += SearchDialogState.FilterExpression;
                    }
                }
                else if (type == 2)
                {
                    string searchText = txtSearchDialogSearch.Text.Replace(';', ' ').Replace(';', ' ').Replace(';', ' ').Replace(';', ' ').Replace(';', ' ');
                    foreach (QuickSearchIntellisense intellisenseText in SearchDialogState.IntellisenseTexts)
                    {
                        if (intellisenseText.IsIncludeInQuickSearch)
                        {
                            if (filterExpression != "")
                                filterExpression += " OR ";
                            filterExpression += string.Format("({0} LIKE '%{1}%')", intellisenseText.DataField, searchText);
                        }
                    }
                    foreach (DetailTableSearch detailTableSearch in SearchDialogState.DetailTableSearchs)
                    {
                        if (detailTableSearch.IsIncludeInQuickSearch)
                        {
                            if (filterExpression != "")
                                filterExpression += " OR ";
                            filterExpression += string.Format("({0} IN (SELECT {1} FROM {2} WHERE {3} LIKE '%{4}%'))", detailTableSearch.KeyFieldName, detailTableSearch.BaseTableKeyFieldName, detailTableSearch.TableName, detailTableSearch.DataField, searchText);
                        }
                    }
                    string tempFilterExpression = filterExpression;
                    filterExpression = SearchDialogState.BaseFilterExpression;
                    if (tempFilterExpression != "")
                    {
                        if (filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += string.Format("({0})", tempFilterExpression);
                    }
                }
                else
                {
                    JavaScriptSerializer json = new JavaScriptSerializer();
                    List<string[]> lstSearchDialogParam = json.Deserialize<List<string[]>>(hdnAdvancedSearchDialogValue.Value);
                    foreach (string[] searchDialogParam in lstSearchDialogParam)
                    {
                        string fieldName = searchDialogParam[0];
                        string isDetailTable = searchDialogParam[1];
                        string value = searchDialogParam[2];
                        if (value != "")
                        {
                            if (isDetailTable == "1")
                            {
                                DetailTableSearch detailTableSearch = SearchDialogState.DetailTableSearchs.FirstOrDefault(p => p.DataField == fieldName);
                                if (filterExpression != "")
                                    filterExpression += " AND ";
                                filterExpression += string.Format("({0} IN (SELECT {1} FROM {2} WHERE {3} LIKE '%{4}%'))", detailTableSearch.BaseTableKeyFieldName, detailTableSearch.KeyFieldName, detailTableSearch.TableName, detailTableSearch.DataField, value);
                            }
                            else
                            {
                                QuickSearchIntellisense intellisenseText = SearchDialogState.IntellisenseTexts.FirstOrDefault(p => p.DataField == fieldName);
                                if (intellisenseText.FieldType == "date")
                                {
                                    string[] temp = value.Split(';');
                                    if (temp[0] != "" && temp[1] != "")
                                    {
                                        DateTime fromDate = Helper.GetDatePickerValue(temp[0]);
                                        DateTime toDate = Helper.GetDatePickerValue(temp[1]);
                                        if (filterExpression != "")
                                            filterExpression += " AND ";
                                        filterExpression += string.Format("({0} BETWEEN '{1}' AND '{2}')", intellisenseText.DataField, fromDate.ToString(Constant.FormatString.DATE_FORMAT_112), toDate.ToString(Constant.FormatString.DATE_FORMAT_112));
                                    }
                                }
                                else
                                {
                                    if (filterExpression != "")
                                        filterExpression += " AND ";
                                    if (intellisenseText.DataField.Contains(','))
                                    {
                                        string tempFilterExpression1 = "";
                                        string[] lstDataField = intellisenseText.DataField.Split(',');
                                        foreach (String dataField in lstDataField)
                                        {
                                            if (tempFilterExpression1 != "")
                                                tempFilterExpression1 += " OR ";
                                            tempFilterExpression1 += string.Format("({0} LIKE '%{1}%')", dataField, value);
                                        }
                                        filterExpression += string.Format("({0})", tempFilterExpression1);
                                    }
                                    else
                                        filterExpression += string.Format("({0} LIKE '%{1}%')", intellisenseText.DataField, value);
                                }
                            }
                        }
                    }
                    string tempFilterExpression = filterExpression;
                    filterExpression = SearchDialogState.BaseFilterExpression;
                    if (tempFilterExpression != "")
                    {
                        if (filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += string.Format("({0})", tempFilterExpression);
                    }
                }

                //string orderByExpression = string.Format("{0} {1}", SearchDialogState.GridColumns[SearchDialogState.OrderByColumnIndex].DataField, SearchDialogState.OrderByType);
                MethodInfo method = typeof(BusinessLayer).GetMethod(SearchDialogState.MethodName, new[] { typeof(string), typeof(int), typeof(int), typeof(string) });
                IList list = null;
                if (method != null)
                {
                    object obj = method.Invoke(null, new object[] { filterExpression, 100, pageIndex, SearchDialogState.OrderByExpression });
                    list = (IList)obj;

                    if (isCountPageCount)
                    {
                        if (SearchDialogState.RowCountMethodName != "")
                        {
                            method = typeof(BusinessLayer).GetMethod(SearchDialogState.RowCountMethodName);
                            if (method != null)
                            {
                                obj = method.Invoke(null, new object[] { filterExpression });
                                rowCount = (int)obj;
                            }
                        }
                        else
                            rowCount = list.Count;
                        pageCount = Helper.GetPageCount(rowCount, 100);
                    }
                }
                else
                {
                    method = typeof(BusinessLayer).GetMethod(SearchDialogState.MethodName, new[] { typeof(string) });
                    if (method == null)
                        throw new Exception(string.Format("Method {0} is not found", SearchDialogState.MethodName));
                    if (SearchDialogState.OrderByExpression != "")
                    {
                        if (filterExpression == "")
                            filterExpression = "1 = 1";
                        filterExpression += " ORDER BY " + SearchDialogState.OrderByExpression;
                    }
                    object obj = method.Invoke(null, new string[] { filterExpression });
                    list = (IList)obj;

                    rowCount = list.Count;
                    pageCount = 1;
                }

                List<Words> words = Helper.LoadWords(this);

                BoundField keyField = new BoundField();
                keyField.DataField = SearchDialogState.KeyFieldName;
                keyField.ItemStyle.CssClass = "keyField";
                keyField.HeaderStyle.CssClass = "keyField";
                grdSearch.Columns.Add(keyField);

                int ctr = 0;
                foreach (GridColumn col in SearchDialogState.GridColumns)
                {
                    if (ctr < 1 && SearchDialogState.IsTreeView)
                    {
                        TemplateField codeField = new TemplateField();
                        codeField.ItemTemplate = new ColumnCodeTemplateField(col.DataField);
                        codeField.HeaderStyle.Width = new Unit(col.Width);
                        codeField.HeaderText = Helper.GetWordsLabel(words, col.HeaderText);
                        grdSearch.Columns.Add(codeField);
                    }
                    else
                    {
                        BoundField field = new BoundField();
                        if (col.DisplayCustomField != null)
                            field.DataField = col.DisplayCustomField;
                        else
                            field.DataField = col.DataField;
                        field.HeaderText = Helper.GetWordsLabel(words, col.HeaderText);
                        field.HeaderStyle.Width = new Unit(col.Width);
                        switch (col.HorizontalAlign)
                        {
                            case "center": field.HeaderStyle.CssClass = "thCenter"; field.ItemStyle.HorizontalAlign = HorizontalAlign.Center; break;
                            case "right": field.HeaderStyle.CssClass = "thRight"; field.ItemStyle.HorizontalAlign = HorizontalAlign.Right; break;
                            default: field.ItemStyle.HorizontalAlign = HorizontalAlign.Left; break;
                        }
                        grdSearch.Columns.Add(field);
                    }
                    ctr++;
                }

                grdSearch.DataSource = list;
                grdSearch.DataBind();
                #endregion

                #region Quick Search
                foreach (QuickSearchIntellisense col in SearchDialogState.IntellisenseTexts)
                {
                    if (intellisenseHints != "")
                        intellisenseHints += ",";
                    intellisenseHints += string.Format("{{ \"text\":\"{0}\",\"fieldName\":\"{1}\",\"description\":\"{2}\",\"fieldType\":\"{3}\" }}", Helper.GetWordsLabel(words, col.HeaderText), col.DataField, col.Description, col.FieldType);
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        protected void grdSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Object obj = e.Row.DataItem as Object;
                foreach (ConditionalStyle conditionalStyle in SearchDialogState.ConditionalStyles)
                {
                    bool conditionalValue = (bool)obj.GetType().GetProperty(conditionalStyle.DataField).GetValue(obj, null);
                    if (conditionalValue)
                    {
                        for (int i = 0; i < e.Row.Cells.Count; i++)
                            e.Row.Cells[i].Attributes.Add("style", conditionalStyle.Style + " !important");
                    }
                }
            }
        }

        public class ColumnCodeTemplateField : ITemplate
        {
            private string codeField = "";
            public ColumnCodeTemplateField(string codeField)
            {
                this.codeField = codeField;
            }

            public void InstantiateIn(Control container)
            {
                HtmlGenericControl div = new HtmlGenericControl("DIV");
                div.DataBinding += new EventHandler(div_DataBinding);
                container.Controls.Add(div);
            }

            void div_DataBinding(object sender, EventArgs e)
            {
                HtmlGenericControl div = (HtmlGenericControl)sender;
                object dataItem = DataBinder.GetDataItem(div.NamingContainer);
                div.Style.Add("margin-left", DataBinder.Eval(dataItem, "Level").ToString() + "0px");
                div.InnerHtml = DataBinder.Eval(dataItem, this.codeField).ToString();
            }
        }

        protected void cbpSearchDialog_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] param = e.Parameter.Split('|');
            string searchDialogType = "";
            string baseFilterExpression = "";

            int pageIndex = 1;
            bool isCountPageCount = true;
            if (param[0] == "open")
            {
                searchDialogType = param[1];
                baseFilterExpression = param[2];
            }
            else if (param[0] == "refresh")
                SearchDialogState.FilterExpression = hdnQuickSearchDialogFilterExpression.Value;
            else if (param[0] == "changepage")
            {
                pageIndex = Convert.ToInt32(param[1]);
                isCountPageCount = false;
            }

            //else if (param[0] == "sort")
            //{
            //SearchDialogState.OrderByColumnIndex = Convert.ToInt32(param[1]);
            //SearchDialogState.OrderByType = param[2];
            //}

            string intellisenseHints = "";
            int rowCount = 0;
            int pageCount = 0;
            BindGridView(1, grdSearch, param[0], searchDialogType, baseFilterExpression, ref intellisenseHints, pageIndex, isCountPageCount, ref pageCount, ref rowCount);

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpIntellisenseHints"] = intellisenseHints;
            panel.JSProperties["cpResult"] = string.Format("{0}|{1}|{2}", param[0], pageCount, rowCount);
            //panel.JSProperties["cpSortedIndex"] = SearchDialogState.OrderByColumnIndex;
            //panel.JSProperties["cpSortedType"] = SearchDialogState.OrderByType;
        }

        protected void cbpContainerSearchDialog2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] param = e.Parameter.Split('|');
            string searchDialogType = "";
            string baseFilterExpression = "";

            int pageIndex = 1;
            bool isCountPageCount = true;
            if (param[0] == "open")
            {
                searchDialogType = param[1];
                baseFilterExpression = param[2];
            }

            string intellisenseHints = "";
            int rowCount = 0;
            int pageCount = 0;
            BindGridView(2, grdSearch2, param[0], searchDialogType, baseFilterExpression, ref intellisenseHints, pageIndex, isCountPageCount, ref pageCount, ref rowCount);

            string searchTooltip = "";
            List<StandardCode> lstAdvancedSearch = new List<StandardCode>();
            foreach (QuickSearchIntellisense intellisenseText in SearchDialogState.IntellisenseTexts)
            {
                if (intellisenseText.IsIncludeInQuickSearch)
                {
                    if (searchTooltip != "")
                        searchTooltip += " / ";
                    searchTooltip += string.Format("'{0}'", intellisenseText.HeaderText);
                }

                lstAdvancedSearch.Add(new StandardCode { StandardCodeID = intellisenseText.DataField, StandardCodeName = intellisenseText.HeaderText, TagProperty = "0", Notes = intellisenseText.FieldType });
            }
            foreach (DetailTableSearch detailTableSearch in SearchDialogState.DetailTableSearchs)
            {
                if (detailTableSearch.IsIncludeInQuickSearch)
                {
                    if (searchTooltip != "")
                        searchTooltip += " / ";
                    searchTooltip += string.Format("'{0}'", detailTableSearch.HeaderText);
                }

                lstAdvancedSearch.Add(new StandardCode { StandardCodeID = detailTableSearch.DataField, StandardCodeName = detailTableSearch.HeaderText, TagProperty = "1" });
            }
            rptAdvancedSearchDialog.DataSource = lstAdvancedSearch;
            rptAdvancedSearchDialog.DataBind();

            divSearchTooltip.Attributes.Add("data-tip", searchTooltip);

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpIntellisenseHints"] = intellisenseHints;
            panel.JSProperties["cpResult"] = string.Format("{0}|{1}|{2}", param[0], pageCount, rowCount);
        }

        protected void rptAdvancedSearchDialog_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                StandardCode entity = (StandardCode)e.Item.DataItem;
                TextBox txtAdvancedSearchDialog = (TextBox)e.Item.FindControl("txtAdvancedSearchDialog");
                TextBox txtSearchFromSearchDialogDate = (TextBox)e.Item.FindControl("txtSearchFromSearchDialogDate");
                TextBox txtSearchToSearchDialogDate = (TextBox)e.Item.FindControl("txtSearchToSearchDialogDate");
                HtmlTable tblSearchDialogDate = (HtmlTable)e.Item.FindControl("tblSearchDialogDate");
                if (entity.Notes == "date")
                {
                    txtAdvancedSearchDialog.Attributes.Add("style", "display:none");
                    tblSearchDialogDate.Attributes.Remove("style");
                    Helper.SetControlEntrySetting(txtSearchFromSearchDialogDate, new ControlEntrySetting(true, true, false), "mpSearchDialogAdvancedSearch");
                    Helper.SetControlEntrySetting(txtSearchToSearchDialogDate, new ControlEntrySetting(true, true, false), "mpSearchDialogAdvancedSearch");
                }
                else
                {
                    tblSearchDialogDate.Attributes.Add("style", "display:none");
                    txtAdvancedSearchDialog.Attributes.Remove("style");
                }
            }
        }

        protected void cbpSearchDialog2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] param = e.Parameter.Split('|');
            string searchDialogType = "";
            string baseFilterExpression = "";

            int type = 2;
            int pageIndex = 1;
            bool isCountPageCount = true;
            if (param[0] == "refresh")
                SearchDialogState.FilterExpression = hdnQuickSearchDialogFilterExpression.Value;
            else if (param[0] == "refresh2")
            {
                type = 3;
                SearchDialogState.FilterExpression = hdnQuickSearchDialogFilterExpression.Value;
            }
            else if (param[0] == "changepage")
            {
                type = Convert.ToInt32(hdnSearchDialog2Type.Value);
                pageIndex = Convert.ToInt32(param[1]);
                isCountPageCount = false;
            }

            //else if (param[0] == "sort")
            //{
            //SearchDialogState.OrderByColumnIndex = Convert.ToInt32(param[1]);
            //SearchDialogState.OrderByType = param[2];
            //}

            string intellisenseHints = "";
            int rowCount = 0;
            int pageCount = 0;
            BindGridView(type, grdSearch2, param[0], searchDialogType, baseFilterExpression, ref intellisenseHints, pageIndex, isCountPageCount, ref pageCount, ref rowCount);

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpIntellisenseHints"] = intellisenseHints;
            panel.JSProperties["cpResult"] = string.Format("{0}|{1}|{2}", param[0], pageCount, rowCount);
            //panel.JSProperties["cpSortedIndex"] = SearchDialogState.OrderByColumnIndex;
            //panel.JSProperties["cpSortedType"] = SearchDialogState.OrderByType;
        }

        #region Search Dialog State
        private class CSearchDialogState
        {
            public string MethodName { get; set; }
            public string KeyFieldName { get; set; }
            public string RowCountMethodName { get; set; }
            public string BaseFilterExpression { get; set; }
            public bool IsTreeView { get; set; }
            public string OrderByExpression { get; set; }
            public string FilterExpression { get; set; }
            public List<GridColumn> GridColumns { get; set; }
            public List<QuickSearchIntellisense> IntellisenseTexts { get; set; }
            public List<ConditionalStyle> ConditionalStyles { get; set; }
            public List<DetailTableSearch> DetailTableSearchs { get; set; }
        }

        private const string SESSION_SEARCH_DIALOG_STATE = "SearchDialogState";
        private static CSearchDialogState SearchDialogState
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_SEARCH_DIALOG_STATE] == null) HttpContext.Current.Session[SESSION_SEARCH_DIALOG_STATE] = new CSearchDialogState();
                return (CSearchDialogState)HttpContext.Current.Session[SESSION_SEARCH_DIALOG_STATE];
            }
            set
            {
                HttpContext.Current.Session[SESSION_SEARCH_DIALOG_STATE] = value;
            }
        }

        private class GridColumn
        {
            private String _HorizontalAlign;
            public String HorizontalAlign
            {
                get { return _HorizontalAlign; }
                set { _HorizontalAlign = value; }
            }

            private String _DisplayCustomField;
            public String DisplayCustomField
            {
                get { return _DisplayCustomField; }
                set { _DisplayCustomField = value; }
            }

            private String _DataField;
            public String DataField
            {
                get { return _DataField; }
                set { _DataField = value; }
            }

            private String _HeaderText;
            public String HeaderText
            {
                get { return _HeaderText; }
                set { _HeaderText = value; }
            }

            private String _Width;
            public String Width
            {
                get { return _Width; }
                set { _Width = value; }
            }
        }

        private class QuickSearchIntellisense
        {
            private String _DataField;
            public String DataField
            {
                get { return _DataField; }
                set { _DataField = value; }
            }
            private String _FieldType;
            public String FieldType
            {
                get { return _FieldType; }
                set { _FieldType = value; }
            }

            private String _HeaderText;
            public String HeaderText
            {
                get { return _HeaderText; }
                set { _HeaderText = value; }
            }

            private Boolean _IsIncludeInQuickSearch;
            public Boolean IsIncludeInQuickSearch
            {
                get { return _IsIncludeInQuickSearch; }
                set { _IsIncludeInQuickSearch = value; }
            }

            private String _Description;
            public String Description
            {
                get { return _Description; }
                set { _Description = value; }
            }
        }

        private class ConditionalStyle
        {
            private String _DataField;
            public String DataField
            {
                get { return _DataField; }
                set { _DataField = value; }
            }

            private String _Style;
            public String Style
            {
                get { return _Style; }
                set { _Style = value; }
            }
        }

        private class DetailTableSearch
        {
            public String TableName { get; set; }
            public String DataField { get; set; }
            public String KeyFieldName { get; set; }
            public String BaseTableKeyFieldName { get; set; }
            public String FilterExpression { get; set; }
            public String HeaderText { get; set; }
            public Boolean IsIncludeInQuickSearch { get; set; }
        }
        #endregion

        private void LoadRightPanelContent()
        {
            try
            {
                string IsLoadContent = Request.Form["hdnRightPanelContentIsLoadContent"] ?? "0";
                if (IsLoadContent == "1")
                {
                    string url = Request.Form["hdnRightPanelContentUrl"] ?? "";
                    string title = Request.Form["hdnRightPanelContentTitle"] ?? "";
                    Control ctlParent = pnlRightPanelContentArea;
                    BaseContentPopupCtl ctl = (BaseContentPopupCtl)LoadControl(url);
                    ctlParent.Controls.Clear();
                    ctlParent.Controls.Add(ctl);

                    string firstTimeLoad = Request.Form["hdnRightPanelContentFirstTimeLoad"] ?? "0";
                    ctl.LoadMasterControl(title);
                    if (firstTimeLoad == "1")
                    {
                        string param = Request.Form["hdnRightPanelContentParam"] ?? "";
                        ctl.InitializeControl(param);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void cbpRightPanelContent_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
        }
    }
}