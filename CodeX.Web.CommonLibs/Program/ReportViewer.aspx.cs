using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Data.Model;
using System.Reflection;
using CodeX.Report;
using CodeX.Web.Common.UI;
using CodeX.Common;
using CodeX.Web.Common;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Text;

namespace CodeX.Web.CommonLibs.Program
{
    public partial class ReportViewer : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Form["param"] != null)
                    hdnParam.Value = Request.Form["param"].ToString();
                param = hdnParam.Value.Split('|');
                string reportCode = Page.Request.QueryString["id"];
                List<ReportMaster> lstReportMaster = BusinessLayer.GetReportMasterList(string.Format("ReportCode = '{0}'", reportCode));
                if (lstReportMaster.Count < 1)
                    throw new Exception(string.Format("Report with code {0} is not defined", reportCode));
                reportMaster = lstReportMaster[0];

                hdnFilterExpression.Value = "";

                divReportProperties.InnerHtml = string.Format("VIDA - {0}, Print Date/Time:{1}, User ID:{2}", reportMaster.ReportCode, DateTime.Now.ToString("dd-MMM-yyyy/HH:mm:ss"), AppSession.UserLogin.UserName);

                vSite oSite = BusinessLayer.GetvSiteList(string.Format("SiteID = '{0}'", AppSession.UserLogin.SiteID))[0];
                if (oSite != null)
                {
                    divSiteName.InnerHtml = oSite.SiteName;
                    divAddressLine1.InnerHtml = oSite.StreetName;
                    divAddressLine2.InnerHtml = oSite.AddressLine2;
                    divPhoneFaxNo.InnerHtml = string.Format("Phone/Fax : {0}", string.IsNullOrEmpty(oSite.FaxNo1) ? oSite.PhoneNo1 : string.Format("{0}/{1}", oSite.PhoneNo1, oSite.FaxNo1));
                }

                BindGridView();
            }
        }

        string reportFilterExpression = "";
        ReportMaster reportMaster = null;
        List<GroupField> lstGroupField = null;
        List<TemplateField> lstTemplateField = null;

        protected decimal paperWidth = 0;
        protected decimal paperHeight = 0;
        protected decimal paperPrintWidth = 0;
        protected decimal paperPrintHeight = 0;
        protected string paperSize = "";
        protected string paperPrintPageContent = "";
        protected string paperPageContent = "";
        protected string fontSize = "";
        protected string paperPortraitLandscape = "";

        #region Generate Filter Expression
        string[] param = null;
        private string GetFilterExpression(string value)
        {
            StringBuilder sbResult = new StringBuilder(value);
            sbResult.Replace("@SiteID", AppSession.UserLogin.SiteID);
            sbResult.Replace("@UserID", AppSession.UserLogin.UserID.ToString());
            return sbResult.ToString();
        }

        private string GenerateFilterExpression(List<ReportParameter> lstReportParameter)
        {
            string reportXML = this.ResolveUrl(string.Format("~/Libs/App_Data/report/filterparameter.xml", reportMaster.ClassName));
            string physicalPath = HttpContext.Current.Request.MapPath(reportXML);
            if (!File.Exists(physicalPath))
                return "";
            XDocument xdocFilterParameter = XDocument.Load(physicalPath);
            string filterExpression = String.Empty;
            for (int i = 0; i < lstReportParameter.Count; ++i)
            {
                var reportParameter = (from sd in xdocFilterParameter.Descendants("filterparameter").Where(p => p.Attribute("code").Value == lstReportParameter[i].Code)
                                       select new
                                       {
                                           Code = sd.Attribute("code").Value,
                                           Name = sd.Attribute("name").Value,
                                           Caption = sd.Attribute("caption").Value,
                                           Type = sd.Parent.Attribute("type").Value,
                                           FieldName = sd.Attribute("fieldname") != null ? sd.Attribute("fieldname").Value : "",
                                           IsAllowSelectAll = sd.Attribute("isallowselectall") != null ? sd.Attribute("isallowselectall").Value == "1" : true
                                       }).FirstOrDefault();

                string filterParameter = String.Empty;
                if (reportParameter.Type == Constant.FilterParameterType.FREE_TEXT)
                {
                    if (i > 0 && filterExpression != "")
                        filterExpression += " AND ";
                    filterParameter += param[i];
                    filterExpression += filterParameter;
                }
                else
                {
                    if (reportParameter.Type == Constant.FilterParameterType.DATE ||
                        reportParameter.Type == Constant.FilterParameterType.PAST_PERIOD ||
                        reportParameter.Type == Constant.FilterParameterType.UPCOMING_PERIOD)
                    {
                        if (i > 0 && filterExpression != "")
                            filterExpression += " AND ";
                        string[] date = param[i].Split(';');
                        string startDate = date[0];
                        string endDate = date[1];
                        filterParameter = string.Format("{0} BETWEEN '{1}' AND '{2}'", reportParameter.FieldName, startDate, endDate);
                        filterExpression += filterParameter;
                    }
                    else if (reportParameter.Type == Constant.FilterParameterType.SINGLE_DATE)
                    {
                        string[] paramSplit = param[i].Split(';');
                        string value = paramSplit[0];
                        if (i > 0 && filterExpression != "")
                            filterExpression += " AND ";
                        filterParameter = string.Format("{0} = '{1}'", reportParameter.FieldName, value);
                        filterExpression += filterParameter;
                    }
                    else if (reportParameter.Type == Constant.FilterParameterType.COMBO_BOX || reportParameter.Type == Constant.FilterParameterType.YEAR_COMBO_BOX || reportParameter.Type == Constant.FilterParameterType.CUSTOM_COMBO_BOX || reportParameter.Type == Constant.FilterParameterType.SEARCH_DIALOG)
                    {
                        string[] paramSplit = param[i].Split(';');
                        string value = paramSplit[0];
                        if (i > 0 && filterExpression != "")
                        {
                            if (!reportParameter.IsAllowSelectAll || value != "")
                                filterExpression += " AND ";
                        }
                        if (!reportParameter.IsAllowSelectAll || value != "")
                            filterParameter = string.Format("{0} = '{1}'", reportParameter.FieldName, value);
                        filterExpression += filterParameter;
                    }
                    else
                    {
                        if (i > 0 && filterExpression != "")
                            filterExpression += " AND ";
                        string[] paramSplit = param[i].Split(';');
                        StringBuilder sbFilterExpressionVal = new StringBuilder();
                        StringBuilder sbTemp = new StringBuilder();

                        for (int idxValue = 0; idxValue < paramSplit.Length; idxValue++)
                        {
                            string value = paramSplit[idxValue];
                            if (sbTemp.ToString() != "")
                                sbTemp.Append(",");

                            sbTemp.Append("'").Append(value).Append("'");
                        }
                        sbFilterExpressionVal.Append(" IN (").Append(sbTemp.ToString()).Append(")");
                        filterParameter = string.Format("{0}{1}", reportParameter.FieldName, sbFilterExpressionVal.ToString());
                        filterExpression += filterParameter;
                    }
                }
            }
            return filterExpression;
        }
        #endregion

        private void BindGridView()
        {
            #region Load Report File
            string reportXML = this.ResolveUrl(string.Format("~/Libs/App_Data/report/general/{0}.xml", reportMaster.ClassName));
            string physicalPath = HttpContext.Current.Request.MapPath(reportXML);
            if (!File.Exists(physicalPath))
                return;
            XDocument xdocReport = XDocument.Load(physicalPath);
            var tempReportSetting = (from sd in xdocReport.Descendants("table")
                                     select new
                                     {
                                         DataSource = sd.Attribute("datasource") != null ? sd.Attribute("datasource").Value : "",
                                         FilterExpression = sd.Attribute("filterexpression") != null ? sd.Attribute("filterexpression").Value : "",
                                         FilterExpressionHd = sd.Attribute("filterexpressionhd") != null ? sd.Attribute("filterexpressionhd").Value : "",
                                         DataSourceHd = sd.Attribute("datasourcehd") != null ? sd.Attribute("datasourcehd").Value : "",
                                         HeaderText = sd.Attribute("headertext") != null ? sd.Attribute("headertext").Value : "",
                                         SubHeaderText = sd.Attribute("subheadertext") != null ? sd.Attribute("subheadertext").Value : "",
                                         PaperType = sd.Attribute("papertype").Value,
                                         FontSize = sd.Attribute("fontsize") != null ? sd.Attribute("fontsize").Value : "9pt",
                                         TotalText = sd.Attribute("totaltext") != null ? sd.Attribute("totaltext").Value : "",
                                         IsShowTotal = sd.Attribute("isshowtotal") != null ? sd.Attribute("isshowtotal").Value == "1" : false,
                                         IsShowHeaderFooter = sd.Attribute("isshowheaderfooter") != null ? sd.Attribute("isshowheaderfooter").Value == "1" : true,
                                         IsShowHeaderBorder = sd.Attribute("isshowheaderborder") != null ? sd.Attribute("isshowheaderborder").Value == "1" : false
                                     }).FirstOrDefault();
            fontSize = tempReportSetting.FontSize;

            if (!tempReportSetting.IsShowHeaderFooter)
            {
                divPageHeader.Style.Add("display", "none");
                divPageFooter.Style.Add("display", "none");
            }

            if (tempReportSetting.HeaderText != "")
            {
                headerText.Style.Remove("display");
                headerText.InnerHtml = tempReportSetting.HeaderText;
            }
            if (tempReportSetting.SubHeaderText != "")
            {
                subHeaderText.Style.Remove("display");
                subHeaderText.InnerHtml = tempReportSetting.SubHeaderText;
            }
            //SubHeaderText
            #endregion

            #region Report Parameter
            List<ReportParameter> lstReportParameter = (from sd in xdocReport.Descendants("parameter")
                                                   select new ReportParameter
                                                 {
                                                     Code = sd.Attribute("code").Value,
                                                     IsShowParameter = sd.Attribute("isshowparameter") != null ? sd.Attribute("isshowparameter").Value == "1" : false
                                                 }).ToList<ReportParameter>();
            reportFilterExpression = GenerateFilterExpression(lstReportParameter);
            #endregion

            #region Load Paper
            string paperSizeXML = this.ResolveUrl("~/Libs/App_Data/report/papersize.xml");
            physicalPath = HttpContext.Current.Request.MapPath(paperSizeXML);
            if (!File.Exists(physicalPath))
                return;
            XDocument xdocPaperSize = XDocument.Load(physicalPath);
            var tempPaperSize = (from sd in xdocPaperSize.Descendants("papersize").Where(p => p.Attribute("type").Value == tempReportSetting.PaperType)
                                 select new
                                 {
                                     Size = sd.Attribute("size").Value,
                                     Width = Convert.ToDecimal(sd.Attribute("width").Value),
                                     Height = Convert.ToDecimal(sd.Attribute("height").Value),
                                     PrintWidth = Convert.ToDecimal(sd.Attribute("printwidth").Value),
                                     PrintHeight = Convert.ToDecimal(sd.Attribute("printheight").Value),
                                     PageContent = sd.Attribute("pagecontent").Value,
                                     PrintPageContent = sd.Attribute("printpagecontent").Value,
                                     PaperPortraitLandscape = sd.Attribute("landscape").Value == "1" ? "landscape" : "portrait"
                                 }).FirstOrDefault();
            paperPortraitLandscape = tempPaperSize.PaperPortraitLandscape;
            paperHeight = tempPaperSize.Height;
            paperWidth = tempPaperSize.Width;
            paperPrintHeight = tempPaperSize.PrintHeight;
            paperPrintWidth = tempPaperSize.PrintWidth;
            paperSize = tempPaperSize.Size;
            paperPrintPageContent = tempPaperSize.PrintPageContent;
            paperPageContent = tempPaperSize.PageContent;
            #endregion

            #region Header & Footer Template
            object entityHd = null;
            if (tempReportSetting.DataSourceHd != "")
            {
                string filterExpressionHd = reportFilterExpression;
                if (filterExpressionHd != "" && tempReportSetting.FilterExpressionHd != "")
                    filterExpressionHd += " AND ";
                filterExpressionHd += tempReportSetting.FilterExpressionHd;

                MethodInfo method1 = typeof(BusinessLayer).GetMethod(tempReportSetting.DataSourceHd, new[] { typeof(string) });
                object obj1 = method1.Invoke(null, new object[] { filterExpressionHd });
                IEnumerable<object> lst = (IEnumerable<object>)obj1;
                entityHd = lst.FirstOrDefault();
            }
            IEnumerable<XElement> x1 = xdocReport.Descendants("headertemplate");
            if (x1.Count() > 0)
            {
                string headerTemplate = x1.Single().Value;
                divContainerReportHeader.Style.Remove("display");

                Regex regex = new Regex("{([(a-zA-Z0-9_.,)]*)}");
                MatchCollection collection = regex.Matches(headerTemplate);
                foreach (Match m in collection)
                {
                    var columnName = m.Groups[1].Value;
                    var fieldValue = entityHd.GetType().GetProperty(columnName).GetValue(entityHd, null).ToString();
                    headerTemplate = headerTemplate.Replace("{" + columnName + "}", fieldValue);
                }

                divContainerReportHeader.InnerHtml = headerTemplate;
            }

            x1 = xdocReport.Descendants("footertemplate");
            if (x1.Count() > 0)
            {
                string footerTemplate = x1.Single().Value;

                Regex regex = new Regex("{SettingParameter.([(a-zA-Z0-9_.,)]*)}");
                MatchCollection collection = regex.Matches(footerTemplate);
                foreach (Match m in collection)
                {
                    var columnName = m.Groups[1].Value;
                    SettingParameter sp = BusinessLayer.GetSettingParameter(columnName);
                    footerTemplate = footerTemplate.Replace("{SettingParameter." + columnName + "}", sp.ParameterValue);
                }

                regex = new Regex("{([(a-zA-Z0-9_.,)]*),N}");
                collection = regex.Matches(footerTemplate);
                foreach (Match m in collection)
                {
                    var columnName = m.Groups[1].Value;
                    var fieldValue = entityHd.GetType().GetProperty(columnName).GetValue(entityHd, null).ToString();
                    footerTemplate = footerTemplate.Replace("{" + columnName + ",N}", Convert.ToDecimal(fieldValue).ToString("N"));
                }

                regex = new Regex("{([(a-zA-Z0-9_.,)]*)}");
                collection = regex.Matches(footerTemplate);
                foreach (Match m in collection)
                {
                    var columnName = m.Groups[1].Value;
                    var fieldValue = entityHd.GetType().GetProperty(columnName).GetValue(entityHd, null).ToString();
                    footerTemplate = footerTemplate.Replace("{" + columnName + "}", fieldValue);
                }

                divContainerReportFooter.InnerHtml = footerTemplate;
            }
            #endregion

            #region Group Field & Template Field
            lstGroupField = (from sd in xdocReport.Descendants("group")
                             select new GroupField
                             {
                                 FieldName = sd.Attribute("fieldname").Value,
                                 HeaderText = sd.Attribute("headertext").Value,
                                 SubTotalText = sd.Attribute("subtotaltext") != null ? sd.Attribute("subtotaltext").Value : "",
                                 OrderBy = sd.Attribute("orderby") != null ? sd.Attribute("orderby").Value : "",
                                 OrderByType = sd.Attribute("orderbytype") != null ? sd.Attribute("orderbytype").Value : "",
                                 TdLocation = sd.Attribute("tdlocation") != null ? Convert.ToInt32(sd.Attribute("tdlocation").Value) : 0,
                                 IsShowSubTotal = sd.Attribute("isshowsubtotal") != null ? sd.Attribute("isshowsubtotal").Value == "1" : false,
                                 IsShowBorderTop = sd.Attribute("isshowbordertop") != null ? sd.Attribute("isshowbordertop").Value == "1" : false
                             }).ToList<GroupField>();

            lstTemplateField = (from sd in xdocReport.Descendants("field")
                                select new TemplateField
                                {
                                    FieldName = sd.Attribute("fieldname") != null ? sd.Attribute("fieldname").Value : "",
                                    FieldType = sd.Attribute("fieldtype") != null ? sd.Attribute("fieldtype").Value : "",
                                    HeaderText = sd.Attribute("headertext").Value,
                                    Width = sd.Attribute("width") != null ? Convert.ToInt32(sd.Attribute("width").Value) : 0,
                                    IsShowSubTotal = sd.Attribute("isshowsubtotal") != null ? sd.Attribute("isshowsubtotal").Value == "1" : false,
                                    InnerHtml = sd.Value,
                                    Align = sd.Attribute("align") != null ? sd.Attribute("align").Value : "left",
                                }).ToList<TemplateField>();
            #endregion

            #region Repeater Builder
            rptReport.HeaderTemplate = new MyTemplate(ListItemType.Header, lstTemplateField, lstGroupField, 0, xdocReport.Root.Elements("fields"), tempReportSetting.IsShowHeaderBorder);
            if (lstTemplateField.Count > 0)
            {
                rptReport.ItemTemplate = new MyTemplate(ListItemType.Item, lstTemplateField, lstGroupField, 0);

                string filterExpressionDt = reportFilterExpression;
                if (filterExpressionDt != "" && tempReportSetting.FilterExpression != "")
                    filterExpressionDt += " AND ";
                filterExpressionDt += tempReportSetting.FilterExpression;

                MethodInfo method = typeof(BusinessLayer).GetMethod(tempReportSetting.DataSource, new[] { typeof(string) });
                object obj = method.Invoke(null, new object[] { filterExpressionDt });

                rptReport.FooterTemplate = new MyTemplate(ListItemType.Footer, lstTemplateField, (IEnumerable<object>)obj, tempReportSetting.IsShowTotal, tempReportSetting.TotalText);

                if (lstGroupField.Count > 0)
                {
                    IEnumerable<object> lst = (IEnumerable<object>)obj;
                    object lst2 = null;

                    GroupField groupField = lstGroupField[0];
                    if (groupField.OrderBy != "")
                    {
                        if (groupField.OrderByType == "")
                            groupField.OrderByType = "ASC";

                        if (groupField.OrderByType == "ASC")
                            lst2 = lst.GroupBy(c => new { ID = c.GetType().GetProperty(groupField.OrderBy).GetValue(c, null), Name = c.GetType().GetProperty(groupField.FieldName).GetValue(c, null) })
                                .Select(group => new { GroupID = group.Key.ID, GroupName = group.Key.Name, Level = 0, Items = group.ToList() }).OrderBy(p => p.GroupID).ToList();
                        else
                            lst2 = lst.GroupBy(c => new { ID = c.GetType().GetProperty(groupField.OrderBy).GetValue(c, null), Name = c.GetType().GetProperty(groupField.FieldName).GetValue(c, null) })
                                .Select(group => new { GroupID = group.Key.ID, GroupName = group.Key.Name, Level = 0, Items = group.ToList() }).OrderByDescending(p => p.GroupID).ToList();
                    }
                    else
                    {
                        lst2 = lst.GroupBy(c => c.GetType().GetProperty(groupField.FieldName).GetValue(c, null))
                            .Select(group => new { GroupName = group.Key, Level = 0, Items = group.ToList() }).ToList();
                    }
                    rptReport.ItemDataBound += new RepeaterItemEventHandler(rptReport_ItemDataBound);
                    rptReport.DataSource = lst2;
                    rptReport.DataBind();
                }
                else
                {
                    rptReport.DataSource = (IList)obj;
                    rptReport.DataBind();
                }
            }
            #endregion
        }
        #region ReportParameter
        class ReportParameter
        {
            public string Code { get; set; }
            public bool IsShowParameter { get; set; }
        }
        #endregion


        #region GroupField
        class GroupField
        {
            public string HeaderText { get; set; }
            public string FieldName { get; set; }
            public string OrderBy { get; set; }
            public string OrderByType { get; set; }
            public string SubTotalText { get; set; }
            public bool IsShowSubTotal { get; set; }
            public bool IsShowBorderTop { get; set; }
            public int TdLocation { get; set; }
        }
        #endregion

        #region TemplateField
        class TemplateField
        {
            public int Width { get; set; }
            public string HeaderText { get; set; }
            public string FieldName { get; set; }
            public string FieldType { get; set; }
            public string InnerHtml { get; set; }
            public bool IsShowSubTotal { get; set; }
            public string Align { get; set; }
        }
        #endregion

        class MyTemplate : ITemplate
        {
            ListItemType _type;
            List<TemplateField> _lstTemplateField;
            List<GroupField> _lstGroupField;
            int _level;
            bool _isShowTotal;
            bool _isShowHeaderBorder;
            IEnumerable<object> _lstEntity;
            string _totalText;
            IEnumerable<XElement> _lstField;
            public MyTemplate(ListItemType type, List<TemplateField> lstTemplateField, List<GroupField> lstGroupField, int level)
            {
                _type = type;
                _lstTemplateField = lstTemplateField;
                _level = level;
                _lstGroupField = lstGroupField;
            }
            public MyTemplate(ListItemType type, List<TemplateField> lstTemplateField, List<GroupField> lstGroupField, int level, IEnumerable<XElement> lstField, bool isShowHeaderBorder)
            {
                _type = type;
                _lstTemplateField = lstTemplateField;
                _level = level;
                _lstGroupField = lstGroupField;
                _lstField = lstField;
                _isShowHeaderBorder = isShowHeaderBorder;
            }
            public MyTemplate(ListItemType type, List<TemplateField> lstTemplateField, IEnumerable<object> lstEntity, bool isShowTotal, string totalText)
            {
                _type = type;
                _lstTemplateField = lstTemplateField;
                _isShowTotal = isShowTotal;
                _lstEntity = lstEntity;
                _totalText = totalText;
            }

            #region Generate Table Header
            private string GenerateTableColumn(XElement field)
            {
                string result = "";
                if (field.Name.LocalName.Equals("field"))
                {
                    int Colspan = field.Attribute("colspan") != null ? Convert.ToInt32(field.Attribute("colspan").Value) : 0;
                    int Rowspan = field.Attribute("rowspan") != null ? Convert.ToInt32(field.Attribute("rowspan").Value) : 0;
                    string FieldName = field.Attribute("fieldname") != null ? field.Attribute("fieldname").Value : "";
                    string FieldType = field.Attribute("fieldtype") != null ? field.Attribute("fieldtype").Value : "";
                    string HeaderText = field.Attribute("headertext").Value;
                    int Width = field.Attribute("width") != null ? Convert.ToInt32(field.Attribute("width").Value) : 0;
                    bool IsShowSubTotal = field.Attribute("isshowsubtotal") != null ? field.Attribute("isshowsubtotal").Value == "1" : false;
                    string InnerHtml = field.Attribute("innerhtml") != null ? field.Attribute("innerhtml").Value : "";
                    string DefaultAlign = field.Attribute("align") != null ? field.Attribute("align").Value : "left";
                    string align = "";
                    if (FieldType == "currency" || FieldType == "number" || FieldType == "autonumber")
                        align = " align='right'";
                    else if (FieldType == "date" || FieldType == "time")
                        align = " align='center'";
                    else
                        align = " align='left'";

                    string rowSpanText = "";
                    string colSpanText = "";
                    if (Rowspan > 0)
                        rowSpanText = string.Format(" rowspan='{0}'", Rowspan);
                    if (Colspan > 0)
                        colSpanText = string.Format(" colspan='{0}'", Colspan);

                    if (FieldType == "customfield")
                        result += string.Format("<th{0}{1} align='{3}'>{2}</th>", rowSpanText, colSpanText, HeaderText, DefaultAlign);
                    else if (FieldType == "autonumber")
                        result += string.Format("<th{0}{1}{4} class='tdAutoNumber' style='width:{3}%;'>{2}</th>", rowSpanText, colSpanText, HeaderText, Width, align);
                    else if (Width > 0)
                        result += string.Format("<th{0}{1}{4} style='width:{3}%'>{2}</th>", rowSpanText, colSpanText, HeaderText, Width, align);
                    else
                        result += string.Format("<th{0}{1}{3}>{2}</th>", rowSpanText, colSpanText, HeaderText, align);
                }
                else if (field.Name.LocalName.Equals("boundfield"))
                {
                    string text = field.Attribute("text") != null ? field.Attribute("text").Value : "";
                    int Colspan = field.Attribute("colspan") != null ? Convert.ToInt32(field.Attribute("colspan").Value) : 0;
                    int Rowspan = field.Attribute("rowspan") != null ? Convert.ToInt32(field.Attribute("rowspan").Value) : 0;
                    string rowSpanText = "";
                    string colSpanText = "";
                    if (Rowspan > 0)
                        rowSpanText = string.Format(" rowspan='{0}'", Rowspan);
                    if (Colspan > 0)
                        colSpanText = string.Format(" colspan='{0}'", Colspan);
                    result += string.Format("<th{1}{2} align='center'>{0}</th>", text, rowSpanText, colSpanText);
                }
                return result;
            }

            private string GenerateTableHeader(XElement xHeader, string result)
            {
                string className = "tblHeader";
                if (_isShowHeaderBorder)
                    className += " tblBorder";

                result += string.Format("<tr class='{0}'>", className);
                IEnumerable<XElement> lstField1 = xHeader.Elements();
                foreach (XElement field in lstField1)
                {
                    result += GenerateTableColumn(field);
                }
                result += "</tr>";

                lstField1 = xHeader.Elements("boundfield");
                if (lstField1.Count() > 0)
                {
                    List<XElement> lstField3 = null;
                    result += string.Format("<tr class='{0}'>", className);
                    bool isBoundFieldExist = false;
                    foreach (XElement boundField in lstField1)
                    {
                        IEnumerable<XElement> lstField2 = boundField.Elements();

                        foreach (XElement field in lstField2)
                        {
                            if (field.Name.LocalName.Equals("boundfield"))
                                isBoundFieldExist = true;
                            if (lstField3 == null)
                                lstField3 = field.Elements().ToList();
                            else
                            {
                                IEnumerable<XElement> lstTemp = field.Elements();
                                foreach (XElement temp in lstTemp)
                                {
                                    lstField3.Add(temp);
                                }
                            }
                            result += GenerateTableColumn(field);
                        }
                    }
                    result += "</tr>";

                    if (isBoundFieldExist)
                    {
                        result += string.Format("<tr class='{0}'>", className);
                        foreach (XElement field in lstField3)
                        {
                            result += GenerateTableColumn(field);
                        }
                        result += "</tr>";
                    }
                }
                return result;
            }
            #endregion

            public void InstantiateIn(System.Web.UI.Control container)
            {
                switch (_type)
                {
                    case ListItemType.Header:
                        if (_lstField.Count() > 0)
                        {
                            HtmlGenericControl ctl = new HtmlGenericControl();
                            ctl.InnerHtml = GenerateTableHeader(_lstField.Single(), "");
                            ctl.InnerHtml += "</thead><tbody class='reportBody'>";
                            container.Controls.Add(ctl);
                        }
                        break;

                    case ListItemType.Footer:
                        if (_isShowTotal)
                        {
                            Literal lcSubTotal = new Literal();
                            List<TemplateField> lstTemplateFieldShowSubTotal = new List<TemplateField>();

                            int count = 0;
                            bool isCountNotSubtotal = true;
                            foreach (TemplateField tf in _lstTemplateField)
                            {
                                if (tf.IsShowSubTotal)
                                    isCountNotSubtotal = false;

                                if (isCountNotSubtotal)
                                    count++;
                                else
                                    lstTemplateFieldShowSubTotal.Add(tf);
                            }
                            lcSubTotal.Text += string.Format("<tr class='trGrandTotal'><td class='tdGrandTotal' colspan='{1}'>{0}</td>", _totalText, count);
                            foreach (TemplateField tf in lstTemplateFieldShowSubTotal)
                            {
                                if (tf.IsShowSubTotal)
                                {
                                    if (tf.FieldType == "currency")
                                    {
                                        decimal subtotal = _lstEntity.Sum(c => Convert.ToDecimal(c.GetType().GetProperty(tf.FieldName).GetValue(c, null)));
                                        lcSubTotal.Text += string.Format("<td align='right' class='tdDetail tdSubTotalDetail'>{0:N}</td>", subtotal);
                                    }
                                    else
                                    {
                                        int subtotal = _lstEntity.Sum(c => Convert.ToInt32(c.GetType().GetProperty(tf.FieldName).GetValue(c, null)));
                                        lcSubTotal.Text += string.Format("<td align='right' class='tdDetail tdSubTotalDetail'>{0}</td>", subtotal);
                                    }
                                }
                                else
                                    lcSubTotal.Text += "<td class='tdDetail tdGrandDetail'>&nbsp;</td>";
                            }
                            lcSubTotal.Text += "</tr>";
                            container.Controls.Add(lcSubTotal);
                        }
                        container.Controls.Add(new LiteralControl("</tbody>"));
                        break;

                    case ListItemType.Item:
                    case ListItemType.AlternatingItem:
                        if (_level < _lstGroupField.Count)
                        {
                            Literal lc = new Literal();
                            Repeater rptDetail = new Repeater();
                            rptDetail.ID = "rptDetail";
                            rptDetail.ItemTemplate = new MyTemplate(ListItemType.Item, _lstTemplateField, _lstGroupField, _level + 1);

                            container.Controls.Add(lc);
                            container.Controls.Add(rptDetail);

                            Literal lcSubTotal = null;
                            if (_lstGroupField[_level].IsShowSubTotal)
                            {
                                lcSubTotal = new Literal();
                                container.Controls.Add(lcSubTotal);
                            }
                            RepeaterItem container1 = (RepeaterItem)container;
                            container.DataBinding += (o, e) =>
                            {
                                int tdLocation = _lstGroupField[_level].TdLocation;
                                if (tdLocation > 0)
                                {
                                    tdLocation--;
                                    lc.Text += "<tr class='trGroup{2}'>";
                                    for (int i = 0; i < tdLocation; ++i)
                                        lc.Text += "<td>&nbsp;</td>";
                                    string className = "";
                                    if (_lstGroupField[_level].IsShowBorderTop)
                                        className = " borderTop";
                                    lc.Text += string.Format("<td class='tdGroupName{2}' colspan='{1}'>{0}</td></tr>", _lstGroupField[_level].HeaderText.Replace("[GroupName]", DataBinder.Eval(container1.DataItem, "GroupName").ToString()), _lstTemplateField.Count - tdLocation, className);
                                }
                                else
                                    lc.Text += string.Format("<tr class='trGroup{2}'><td class='tdGroupName' colspan='{1}' style='padding-left:{3}0px;'>{0}</td></tr>", _lstGroupField[_level].HeaderText.Replace("[GroupName]", DataBinder.Eval(container1.DataItem, "GroupName").ToString()), _lstTemplateField.Count, _level, _level * 2);
                                if (lcSubTotal != null)
                                {
                                    List<TemplateField> lstTemplateFieldShowSubTotal = new List<TemplateField>();

                                    int count = 0;
                                    bool isCountNotSubtotal = true;
                                    foreach (TemplateField tf in _lstTemplateField)
                                    {
                                        if (tf.IsShowSubTotal)
                                            isCountNotSubtotal = false;

                                        if (isCountNotSubtotal)
                                            count++;
                                        else
                                            lstTemplateFieldShowSubTotal.Add(tf);
                                    }
                                    object entity = container1.DataItem as object;
                                    IEnumerable<object> lst = (IEnumerable<object>)entity.GetType().GetProperty("Items").GetValue(entity, null);
                                    lcSubTotal.Text += string.Format("<tr class='trSubTotal{2}'><td class='tdSubTotal' colspan='{1}'>{0}</td>", _lstGroupField[_level].SubTotalText.Replace("[GroupName]", DataBinder.Eval(container1.DataItem, "GroupName").ToString()), count, _level, _level * 2);
                                    foreach (TemplateField tf in lstTemplateFieldShowSubTotal)
                                    {
                                        if (tf.IsShowSubTotal)
                                        {
                                            if (tf.FieldType == "currency")
                                            {
                                                decimal subtotal = lst.Sum(c => Convert.ToDecimal(c.GetType().GetProperty(tf.FieldName).GetValue(c, null)));
                                                lcSubTotal.Text += string.Format("<td align='right' class='tdDetail tdSubTotalDetail'>{0:N}</td>", subtotal);
                                            }
                                            else
                                            {
                                                int subtotal = lst.Sum(c => Convert.ToInt32(c.GetType().GetProperty(tf.FieldName).GetValue(c, null)));
                                                lcSubTotal.Text += string.Format("<td align='right' class='tdDetail tdSubTotalDetail'>{0}</td>", subtotal);
                                            }
                                        }
                                        else
                                            lcSubTotal.Text += "<td class='tdDetail tdSubTotalDetail'>&nbsp;</td>";
                                    }
                                    lcSubTotal.Text += "</tr>";
                                }
                            };
                        }
                        else
                        {
                            Literal lc = new Literal();
                            container.Controls.Add(lc);
                            RepeaterItem container1 = (RepeaterItem)container;
                            container.DataBinding += (o, e) =>
                            {
                                lc.Text += "<tr>";
                                foreach (TemplateField tf in _lstTemplateField)
                                {

                                    if (tf.FieldType == "customfield")
                                    {
                                        string innerHtml = tf.InnerHtml;
                                        Regex regex = new Regex("{([(a-zA-Z0-9_.,)]*)}");
                                        MatchCollection collection = regex.Matches(innerHtml);
                                        foreach (Match m in collection)
                                        {
                                            var columnName = m.Groups[1].Value;
                                            innerHtml = innerHtml.Replace("{" + columnName + "}", DataBinder.Eval(container1.DataItem, columnName).ToString());
                                        }
                                        lc.Text += string.Format("<td align='{1}' class='tdDetail'>{0}</td>", innerHtml, tf.Align);
                                    }
                                    else if (tf.FieldType == "date")
                                        lc.Text += string.Format("<td align='center' class='tdDetail'>{0:dd-MMM-yyyy}</td>", DataBinder.Eval(container1.DataItem, tf.FieldName));
                                    else if (tf.FieldType == "autonumber")
                                        lc.Text += string.Format("<td align='right' class='tdDetail tdAutoNumber'>{0}.</td>", container1.ItemIndex + 1);
                                    else if (tf.FieldType == "currency")
                                        lc.Text += string.Format("<td align='right' class='tdDetail'>{0:N}</td>", DataBinder.Eval(container1.DataItem, tf.FieldName));
                                    else if (tf.FieldType == "number")
                                        lc.Text += string.Format("<td align='right' class='tdDetail'>{0}</td>", DataBinder.Eval(container1.DataItem, tf.FieldName));
                                    else
                                        lc.Text += string.Format("<td class='tdDetail'>{0}</td>", DataBinder.Eval(container1.DataItem, tf.FieldName));
                                }
                                lc.Text += "</tr>";
                            };
                        }
                        break;
                }
            }
        }

        void rptReport_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                object entity = e.Item.DataItem as object;
                Repeater rptDetail = (Repeater)e.Item.FindControl("rptDetail");

                int level = Convert.ToInt32(entity.GetType().GetProperty("Level").GetValue(entity, null)) + 1;
                if (level < lstGroupField.Count)
                {
                    IEnumerable<object> lst = (IEnumerable<object>)entity.GetType().GetProperty("Items").GetValue(entity, null);
                    object lst2 = lst.GroupBy(c => c.GetType().GetProperty(lstGroupField[level].FieldName).GetValue(c, null))
                        .Select(group => new { GroupName = group.Key, Level = level, Items = group.ToList() }).ToList();

                    GroupField groupField = lstGroupField[level];
                    if (groupField.OrderBy != "")
                    {
                        if (groupField.OrderByType == "")
                            groupField.OrderByType = "ASC";

                        if (groupField.OrderByType == "ASC")
                            lst2 = lst.GroupBy(c => new { ID = c.GetType().GetProperty(groupField.OrderBy).GetValue(c, null), Name = c.GetType().GetProperty(groupField.FieldName).GetValue(c, null) })
                                .Select(group => new { GroupID = group.Key.ID, GroupName = group.Key.Name, Level = level, Items = group.ToList() }).OrderBy(p => p.GroupID).ToList();
                        else
                            lst2 = lst.GroupBy(c => new { ID = c.GetType().GetProperty(groupField.OrderBy).GetValue(c, null), Name = c.GetType().GetProperty(groupField.FieldName).GetValue(c, null) })
                                .Select(group => new { GroupID = group.Key.ID, GroupName = group.Key.Name, Level = level, Items = group.ToList() }).OrderByDescending(p => p.GroupID).ToList();
                    }
                    else
                    {
                        lst2 = lst.GroupBy(c => c.GetType().GetProperty(lstGroupField[level].FieldName).GetValue(c, null))
                            .Select(group => new { GroupName = group.Key, Level = level, Items = group.ToList() }).ToList();
                    }

                    rptDetail.ItemDataBound += new RepeaterItemEventHandler(rptReport_ItemDataBound);
                    rptDetail.DataSource = lst2;
                    rptDetail.DataBind();
                }
                else
                {
                    rptDetail.DataSource = (IList)entity.GetType().GetProperty("Items").GetValue(entity, null);
                    rptDetail.DataBind();
                }
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            //Response.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}.xls\"", hdnMenuCaption.Value));
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "application/vnd.xls";
            //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            //div.RenderControl(htmlWrite);
            ////Response.Write(stringWrite.ToString());
            //Response.Write("<html><head><style type='text/css'>.grdView > tbody > tr > td {color:green; border:1px solid;}</style></head>" + stringWrite.ToString() + "</html>");
            //Response.End();

            string attachment = string.Format("attachment;filename=\"{0}.xls\"", "test");
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            //StringWriter stw = new StringWriter();
            //HtmlTextWriter htextw = new HtmlTextWriter(stw);
            //divExportExcel.RenderControl(htextw);
            //HttpContext.Current.Response.Write(stw.ToString());
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.Write("<html><head></head>" + hdnExportExcel.Value + "</html>");
            //stw = null;
            //htextw = null;
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();

            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            //hw.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            //hw.Write("<html><head></head>" + hdnExportExcel.Value + "</html>");
            //StringReader sr = new StringReader(sw.ToString());
            //Document pdfDoc = new Document(PageSize.A4, 18f, 18f, 18f, 18f);
            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //pdfDoc.Open();
            //htmlparser.Parse(sr);
            //pdfDoc.Close();
            //Response.Write(pdfDoc);
            //Response.End();
        }
    }
}