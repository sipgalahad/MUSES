using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeX.Data.Model;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using CodeX.Common;
using System.Web;
using System.IO;
using System.Xml.Linq;
using System.Reflection;
using System.Web.UI.WebControls;
using System.Collections;
using CodeX.Web.Common.UI;

namespace CodeX.Web.Common
{
    public static class ExcelReport
    {
        public static string GenerateExcelFile(HttpServerUtility Server, TemplateControl page, ref string reportName, string tempParam, string reportCode, bool bIsDailyReport = false)
        {
            isDailyReport = bIsDailyReport;
            param = tempParam.Split('|');
            List<ReportMaster> lstReportMaster = BusinessLayer.GetReportMasterList(string.Format("ReportCode = '{0}'", reportCode));
            if (lstReportMaster.Count < 1)
                throw new Exception(string.Format("Report with code {0} is not defined", reportCode));
            reportMaster = lstReportMaster[0];

            reportName = reportMaster.ReportName;
            oSite = BusinessLayer.GetvSiteList(string.Format("SiteID = '{0}'", AppSession.UserLogin.SiteID))[0];

            string exportExcelValue = "";
            BindGridView(Server, page, ref exportExcelValue);

            string tempExportExcelValue = "<style type='text/css'>* { box-sizing: border-box;-moz-box-sizing: border-box; }";
            tempExportExcelValue += ".tblReport .tdDetail, .tblHeader td { padding-right:0.1cm; padding-left:0.1cm;  }";
            tempExportExcelValue += "thead td            { font-weight: bold; }";
            tempExportExcelValue += ".tblHeader td            { border-bottom: 1px solid; }";
            tempExportExcelValue += ".tblHeader:nth-child(1) td            { border-top: 1px solid; }";
            tempExportExcelValue += ".tblBorder td:last-child            { border-right: 1px solid; }";
            tempExportExcelValue += ".tblBorder td            { border-left: 1px solid; border-collapse:collapse; }";
            tempExportExcelValue += "thead { display:table-row-group; }";

            tempExportExcelValue += ".tdGroupName, .tdSubTotal, .tdGrandTotal        { font-weight: bold; }";
            tempExportExcelValue += ".tdGrandTotal, .tdSubTotal                 { text-align: right; }";
            tempExportExcelValue += ".tdSubTotalDetail           { border-top: 1px dotted; padding: 0.5mm 0; }";
            tempExportExcelValue += ".reportBody tr.trGroup0:not(:first-child) > td { padding-top: 20px; }";
            tempExportExcelValue += ".reportBody tr.trGroup0:not(:first-child) > td > tr.trGroup1:not(:first-child) > td { padding-top: 20px; }";
            tempExportExcelValue += ".pageFooter         { border-top: 1px solid; position: absolute; bottom: 0.5cm; left: 0.7cm; right: 0.7cm; font-size: 8pt; }";

            tempExportExcelValue += ".tdAutoNumber       { padding-right:4px !important; }";
            tempExportExcelValue += ".borderTop          { border-top: 1px dotted; }";
            tempExportExcelValue += ".tdSignature        { padding-top:1.7cm; }";

            tempExportExcelValue += ".divContainerReportHeader *     { font-weight: normal; }";
            tempExportExcelValue += ".divContainerReportHeader b     { font-weight: bold !important; }";
            tempExportExcelValue += ".divContainerReportHeader       { margin-bottom: 0.5cm; }";
            tempExportExcelValue += ".divContainerReportFooter       { margin-top: 0.1cm; }";
            tempExportExcelValue += ".tdReportTotal *                { font-weight: bold; font-size: 8pt; }";
            tempExportExcelValue += "h1 { font-weight: bold; font-size: 12pt; margin-bottom: 0.5cm }";
            tempExportExcelValue += "h2 { font-weight: bold; font-size: 10pt; margin-bottom: 0.5cm; margin-top: -0.5cm; }";

            tempExportExcelValue += "</style>";

            exportExcelValue = tempExportExcelValue + "<div class='pageContent'>" + exportExcelValue + "</div>";

            string retval = @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">";
            retval += "<html><head></head>" + exportExcelValue + "</html>";

            return retval;
        }


        private static bool isDailyReport = false;
        private static string reportFilterExpression = "";
        private static ReportMaster reportMaster = null;
        private static List<GroupField> lstGroupField = null;
        private static List<TemplateField> lstTemplateField = null;
        private static vSite oSite = null;

        #region Generate Filter Expression
        public static string[] param = null;
        private static string GetFilterExpression(string value)
        {
            StringBuilder sbResult = new StringBuilder(value);
            sbResult.Replace("@SiteID", AppSession.UserLogin.SiteID);
            sbResult.Replace("@UserID", AppSession.UserLogin.UserID.ToString());
            return sbResult.ToString();
        }

        static List<Variable> lstParameterCodeValue = new List<Variable>();
        private static List<Variable> GenerateFilterExpressionSP(TemplateControl page, List<ReportParameter> lstReportParameter, bool isShowParameter)
        {
            string reportXML = page.ResolveUrl(string.Format("~/Libs/App_Data/report/filterparameter.xml", reportMaster.ReportUrl));
            string physicalPath = HttpContext.Current.Request.MapPath(reportXML);
            if (!File.Exists(physicalPath))
                return null;
            XDocument xdocFilterParameter = XDocument.Load(physicalPath);

            List<Variable> lstVariable = new List<Variable>();
            string displayParameter = "<table class='tblReportParameter' style='width:100%' cellpadding='0' cellspacing='0'><colgroup><col style='width:50%'></colgroup>";
            int ctrParameter = 1;
            for (int i = 0; i < lstReportParameter.Count; ++i)
            {
                var reportParameter = (from sd in xdocFilterParameter.Descendants("filterparameter").Where(p => p.Attribute("code").Value == lstReportParameter[i].Code)
                                       select new
                                       {
                                           Code = sd.Attribute("code").Value,
                                           Name = sd.Attribute("name").Value,
                                           Caption = sd.Attribute("caption").Value,
                                           Type = sd.Parent.Attribute("type").Value,
                                           FieldName = sd.Attribute("fieldname") != null ? sd.Attribute("fieldname").Value : ""
                                       }).FirstOrDefault();
                string[] paramSplit = param[i].Split(';');
                string value = paramSplit[0];
                string paramText = paramSplit[1];
                lstVariable.Add(new Variable { Code = reportParameter.FieldName, Value = GetFilterExpression(value) });

                if (paramText != "")
                {
                    if (lstReportParameter[i].IsShowAsSubTitle)
                    {
                        subHeaderText.Style.Remove("display");
                        if (subHeaderText.InnerHtml != "")
                            subHeaderText.InnerHtml += "<br/>";
                        subHeaderText.InnerHtml += string.Format("{0} : {1}", reportParameter.Caption, paramText);
                    }
                    else
                    {
                        if (ctrParameter % 2 == 1)
                        {
                            displayParameter += "<tr>";
                            displayParameter += string.Format("<td valign='top'><table class='tblReportParameterDt' cellpadding='0' cellspacing='0'><tr><td>{0}</td><td>:</td><td>{1}</td></tr></table></td>", reportParameter.Caption, paramText);
                        }
                        else if (ctrParameter % 2 == 0)
                        {
                            displayParameter += string.Format("<td valign='top' align='right'><table cellpadding='0' class='tblReportParameterDt' cellspacing='0'><tr><td>{0}</td><td>:</td><td>{1}</td></tr></table></td>", reportParameter.Caption, paramText);
                            displayParameter += "</tr>";
                        }
                        ctrParameter++;
                    }
                    lstParameterCodeValue.Add(new Variable { Code = reportParameter.Code, Value = paramText });
                    SubHeaderText1 = SubHeaderText1.Replace("{" + reportParameter.Code + "}", paramText);
                }
            }
            displayParameter += "</table>";
            //if (isShowParameter)
            //    divContainerReportParameter.InnerHtml = displayParameter;
            //else
            //    divContainerReportParameter.Style.Add("display", "none");
            return lstVariable;
        }

        private static string GenerateFilterExpression(TemplateControl page, List<ReportParameter> lstReportParameter, bool isShowParameter)
        {
            string reportXML = page.ResolveUrl(string.Format("~/Libs/App_Data/report/filterparameter.xml", reportMaster.ReportUrl));
            string physicalPath = HttpContext.Current.Request.MapPath(reportXML);
            if (!File.Exists(physicalPath))
                return "";
            XDocument xdocFilterParameter = XDocument.Load(physicalPath);
            string filterExpression = String.Empty;
            string displayParameter = "<table class='tblReportParameter' style='width:100%' cellpadding='0' cellspacing='0'><colgroup><col style='width:50%'></colgroup>";
            int ctrParameter = 1;
            for (int i = 0; i < lstReportParameter.Count; ++i)
            {
                var reportParameter = (from sd in xdocFilterParameter.Descendants("filterparameter").Where(p => p.Attribute("code").Value == lstReportParameter[i].Code)
                                       select new
                                       {
                                           Code = sd.Attribute("code").Value,
                                           Name = sd.Attribute("name").Value,
                                           Caption = sd.Attribute("caption").Value,
                                           Type = sd.Parent.Attribute("type").Value,
                                           SearchType = sd.Attribute("searchtype") != null ? sd.Attribute("searchtype").Value : "=",
                                           LikeFormula = sd.Attribute("likeformula") != null ? sd.Attribute("likeformula").Value : "{0}",
                                           FieldName = sd.Attribute("fieldname") != null ? sd.Attribute("fieldname").Value : ""
                                       }).FirstOrDefault();

                string paramText = "";
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
                        paramText = date[1];
                        string startDate = date[0].Substring(0, 8);
                        string endDate = date[0].Substring(8, 8);
                        filterParameter = string.Format("{0} BETWEEN '{1}' AND '{2}'", reportParameter.FieldName, startDate, endDate);
                        filterExpression += filterParameter;
                    }
                    else if (reportParameter.Type == Constant.FilterParameterType.SINGLE_DATE)
                    {
                        string[] paramSplit = param[i].Split(';');
                        paramText = paramSplit[1];
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
                        paramText = paramSplit[1];
                        if (i > 0 && filterExpression != "")
                        {
                            if (lstReportParameter[i].IsRequired || value != "")
                                filterExpression += " AND ";
                        }
                        if (lstReportParameter[i].IsRequired || value != "")
                        {
                            if (reportParameter.SearchType == "=")
                                filterParameter = string.Format("{0} = '{1}'", reportParameter.FieldName, value);
                            else
                                filterParameter = string.Format("{0} LIKE '%{1}%'", reportParameter.FieldName, reportParameter.LikeFormula.Replace("{0}", value));
                        }
                        filterExpression += filterParameter;
                    }
                    else
                    {
                        if (i > 0 && filterExpression != "")
                            filterExpression += " AND ";
                        string[] paramSplit = param[i].Split(';');
                        paramText = paramSplit[1];
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
                if (paramText != "")
                {
                    if (lstReportParameter[i].IsShowAsSubTitle)
                    {
                        subHeaderText.Style.Remove("display");
                        if (subHeaderText.InnerHtml != "")
                            subHeaderText.InnerHtml += "<br/>";
                        subHeaderText.InnerHtml += string.Format("{0} : {1}", reportParameter.Caption, paramText);
                    }
                    else
                    {
                        if (ctrParameter % 2 == 1)
                        {
                            displayParameter += "<tr>";
                            displayParameter += string.Format("<td valign='top'><table class='tblReportParameterDt' cellpadding='0' cellspacing='0'><tr><td>{0}</td><td>:</td><td>{1}</td></tr></table></td>", reportParameter.Caption, paramText);
                        }
                        else if (ctrParameter % 2 == 0)
                        {
                            displayParameter += string.Format("<td valign='top' align='right'><table cellpadding='0' class='tblReportParameterDt' cellspacing='0'><tr><td>{0}</td><td>:</td><td>{1}</td></tr></table></td>", reportParameter.Caption, paramText);
                            displayParameter += "</tr>";
                        }
                        ctrParameter++;
                    }
                    lstParameterCodeValue.Add(new Variable { Code = reportParameter.Code, Value = paramText });
                    SubHeaderText1 = SubHeaderText1.Replace("{" + reportParameter.Code + "}", paramText);
                }
            }
            displayParameter += "</table>";
            if (isShowParameter)
                divContainerReportParameter.InnerHtml = displayParameter;
            else
                divContainerReportParameter.Style.Add("display", "none");
            return filterExpression;
        }
        #endregion

        #region Regex Header Footer
        private static string SetTemplateText(HttpServerUtility Server, string templateText, string dataSourceHd, object entityHd)
        {
            Regex regex = new Regex("{Site.([(a-zA-Z0-9_.,)]*)}");
            MatchCollection collection = regex.Matches(templateText);
            foreach (Match m in collection)
            {
                var columnName = m.Groups[1].Value;
                var prop = oSite.GetType().GetProperty(columnName);
                if (prop == null)
                    throw new Exception(string.Format("Property {0} Not Found in Site", columnName));
                var fieldValue = prop.GetValue(oSite, null).ToString();
                templateText = templateText.Replace("{Site." + columnName + "}", fieldValue);
            }

            templateText = templateText.Replace("{AppSession.UserName}", AppSession.UserLogin.UserName);

            regex = new Regex("{SettingParameter.([(a-zA-Z0-9_.,)]*)}");
            collection = regex.Matches(templateText);
            foreach (Match m in collection)
            {
                var columnName = m.Groups[1].Value;
                SettingParameter sp = BusinessLayer.GetSettingParameter(columnName);
                templateText = templateText.Replace("{SettingParameter." + columnName + "}", sp.ParameterValue);
            }

            regex = new Regex("{([(a-zA-Z0-9_.,)]*),N}");
            collection = regex.Matches(templateText);
            foreach (Match m in collection)
            {
                var columnName = m.Groups[1].Value;
                var prop = entityHd.GetType().GetProperty(columnName);
                if (prop == null)
                    throw new Exception(string.Format("Property {0} Not Found in {1}", columnName, dataSourceHd));
                var fieldValue = prop.GetValue(entityHd, null).ToString();
                templateText = templateText.Replace("{" + columnName + ",N}", Convert.ToDecimal(fieldValue).ToString("N"));
            }

            regex = new Regex("{([(a-zA-Z0-9_.,)]*),DATE_FORMAT}");
            collection = regex.Matches(templateText);
            foreach (Match m in collection)
            {
                var columnName = m.Groups[1].Value;
                var prop = entityHd.GetType().GetProperty(columnName);
                if (prop == null)
                    throw new Exception(string.Format("Property {0} Not Found in {1}", columnName, dataSourceHd));
                var fieldValue = prop.GetValue(entityHd, null).ToString();
                templateText = templateText.Replace("{" + columnName + ",DATE_FORMAT}", Convert.ToDateTime(fieldValue).ToString("dd-MMM-yyyy"));
            }
            templateText = templateText.Replace("{DateTime.Now}", DateTime.Now.ToString(Constant.FormatString.DATE_FORMAT));

            regex = new Regex("{([(a-zA-Z0-9_.,)]*)}");
            collection = regex.Matches(templateText);
            foreach (Match m in collection)
            {
                var columnName = m.Groups[1].Value;
                var prop = entityHd.GetType().GetProperty(columnName);
                if (prop == null)
                    throw new Exception(string.Format("Property {0} Not Found in {1}", columnName, dataSourceHd));
                var fieldValue = prop.GetValue(entityHd, null).ToString();
                templateText = templateText.Replace("{" + columnName + "}", Server.HtmlDecode(fieldValue));
            }
            return templateText;
        }
        #endregion

        private static HtmlGenericControl subHeaderText = null;
        private static HtmlGenericControl divContainerReportParameter = null;
        private static string SubHeaderText1 = "";
        private static void BindGridView(HttpServerUtility Server, TemplateControl page, ref string exportExcelValue)
        {
            HtmlGenericControl divContent = new HtmlGenericControl("DIV");
            HtmlGenericControl pageHeader = new HtmlGenericControl("DIV");

            divContent.Controls.Add(pageHeader);

            HtmlGenericControl center1 = new HtmlGenericControl("center");
            pageHeader.Controls.Add(center1);

            HtmlGenericControl headerText = new HtmlGenericControl("H1");
            center1.Controls.Add(headerText);

            HtmlGenericControl center2 = new HtmlGenericControl("center");
            pageHeader.Controls.Add(center2);

            subHeaderText = new HtmlGenericControl("H1");
            center2.Controls.Add(subHeaderText);

            HtmlGenericControl divContainerReportHeader = new HtmlGenericControl("DIV");
            divContainerReportHeader.Attributes.Add("class", "divContainerReportHeader");
            divContainerReportParameter = new HtmlGenericControl("DIV");
            divContainerReportParameter.Attributes.Add("class", "divContainerReportParameter");

            pageHeader.Controls.Add(divContainerReportHeader);
            pageHeader.Controls.Add(divContainerReportParameter);

            HtmlGenericControl divContainerReportBody = new HtmlGenericControl();
            divContent.Controls.Add(divContainerReportBody);

            #region Load Report File
            string reportXML = page.ResolveUrl(string.Format("~/Libs/App_Data/report/{0}/{1}.xml", AppConfigManager.CDXAppClientID, reportMaster.ReportUrl));
            string physicalPath = HttpContext.Current.Request.MapPath(reportXML);
            if (!File.Exists(physicalPath))
            {
                reportXML = page.ResolveUrl(string.Format("~/Libs/App_Data/report/general/{0}.xml", reportMaster.ReportUrl));
                physicalPath = HttpContext.Current.Request.MapPath(reportXML);
                if (!File.Exists(physicalPath))
                    return;
            }
            XDocument xdocReport = XDocument.Load(physicalPath);
            var tempReportSetting = (from sd in xdocReport.Descendants("table")
                                     select new
                                     {
                                         IsCustom = sd.Attribute("iscustom") != null ? sd.Attribute("iscustom").Value == "1" : false,
                                         CustomReportURL = sd.Attribute("customreporturl") != null ? sd.Attribute("customreporturl").Value : "",
                                         DataSource = sd.Attribute("datasource") != null ? sd.Attribute("datasource").Value : "",
                                         FilterExpression = sd.Attribute("filterexpression") != null ? sd.Attribute("filterexpression").Value : "",
                                         FilterExpressionHd = sd.Attribute("filterexpressionhd") != null ? sd.Attribute("filterexpressionhd").Value : "",
                                         DataSourceHd = sd.Attribute("datasourcehd") != null ? sd.Attribute("datasourcehd").Value : "",
                                         HeaderText = sd.Attribute("headertext") != null ? sd.Attribute("headertext").Value : "",
                                         SubHeaderText = sd.Attribute("subheadertext") != null ? sd.Attribute("subheadertext").Value : "",
                                         DotMatrixDPI = sd.Attribute("dotmatrixdpi") != null ? sd.Attribute("dotmatrixdpi").Value : "",
                                         PaperType = sd.Attribute("papertype").Value,
                                         FontSize = sd.Attribute("fontsize") != null ? sd.Attribute("fontsize").Value : "9pt",
                                         FontFamily = sd.Attribute("fontfamily") != null ? sd.Attribute("fontfamily").Value : "",
                                         TotalText = sd.Attribute("totaltext") != null ? sd.Attribute("totaltext").Value : "",
                                         IsShowTotal = sd.Attribute("isshowtotal") != null ? sd.Attribute("isshowtotal").Value == "1" : false,
                                         TotalType = sd.Attribute("totaltype") != null ? sd.Attribute("totaltype").Value : "SUM",
                                         IsUsingDotMatrix = sd.Attribute("isusingdotmatrix") != null ? sd.Attribute("isusingdotmatrix").Value == "1" : false,
                                         IsDataSourceFromSP = sd.Attribute("isdatasourcefromsp") != null ? sd.Attribute("isdatasourcefromsp").Value == "1" : false,
                                         IsShowHeaderFooter = sd.Attribute("isshowheaderfooter") != null ? sd.Attribute("isshowheaderfooter").Value == "1" : true,
                                         IsShowHeader = sd.Attribute("isshowheader") != null ? sd.Attribute("isshowheader").Value == "1" : false,
                                         IsShowFooter = sd.Attribute("isshowfooter") != null ? sd.Attribute("isshowfooter").Value == "1" : false,
                                         IsShowPageNumber = sd.Attribute("isshowpagenumber") != null ? sd.Attribute("isshowpagenumber").Value == "1" : true,
                                         IsShowParameter = sd.Attribute("isshowparameter") != null ? sd.Attribute("isshowparameter").Value == "1" : false,
                                         CustomPadding = sd.Attribute("custompadding") != null ? sd.Attribute("custompadding").Value : "",
                                         IsShowHeaderBorder = sd.Attribute("isshowheaderborder") != null ? sd.Attribute("isshowheaderborder").Value == "1" : false
                                     }).FirstOrDefault();

            string reportHeaderXML = page.ResolveUrl(string.Format("~/Libs/App_Data/report/{0}/CustomHeader.xml", AppConfigManager.CDXAppClientID, reportMaster.ReportUrl));
            physicalPath = HttpContext.Current.Request.MapPath(reportHeaderXML);
            if (File.Exists(physicalPath))
            {
                XDocument xdocReportCustomHeader = XDocument.Load(physicalPath);
                IEnumerable<XElement> x1 = xdocReportCustomHeader.Descendants("pageheadertemplate");
                if (x1.Count() > 0)
                {
                    string headerTemplate = x1.Single().Value;

                    Regex regex = new Regex("{ResolveUrl.([(a-zA-Z0-9_.,~/)]*)}");
                    MatchCollection collection = regex.Matches(headerTemplate);
                    foreach (Match m in collection)
                    {
                        var url = m.Groups[1].Value;
                        headerTemplate = headerTemplate.Replace("{ResolveUrl." + url + "}", page.ResolveUrl(url));
                    }

                    regex = new Regex("{Site.([(a-zA-Z0-9_.,)]*)}");
                    collection = regex.Matches(headerTemplate);
                    foreach (Match m in collection)
                    {
                        var columnName = m.Groups[1].Value;
                        var prop = oSite.GetType().GetProperty(columnName);
                        if (prop == null)
                            throw new Exception(string.Format("Property {0} Not Found in Site", columnName));
                        var fieldValue = prop.GetValue(oSite, null).ToString();
                        headerTemplate = headerTemplate.Replace("{Site." + columnName + "}", fieldValue);
                    }
                    //headerTemplate = SetTemplateText(headerTemplate, tempReportSetting.DataSourceHd, entityHd);

                }
            }


            SubHeaderText1 = tempReportSetting.SubHeaderText;
            if (tempReportSetting.HeaderText != "")
            {
                headerText.Style.Remove("display");
                headerText.InnerHtml = tempReportSetting.HeaderText;
            }
            //SubHeaderText
            #endregion

            #region Report Parameter
            List<ReportParameter> lstReportParameter = (from sd in xdocReport.Descendants("parameter")
                                                        select new ReportParameter
                                                        {
                                                            Code = sd.Attribute("code").Value,
                                                            IsShowAsSubTitle = sd.Attribute("isshowassubtitle") != null ? sd.Attribute("isshowassubtitle").Value == "1" : false,
                                                            IsAllowInDailyReport = sd.Attribute("isallowindailyreport") != null ? sd.Attribute("isallowindailyreport").Value == "1" : false
                                                        }).ToList<ReportParameter>();

            if (isDailyReport)
                lstReportParameter = lstReportParameter.Where(p => p.IsAllowInDailyReport).ToList();

            if (!tempReportSetting.IsDataSourceFromSP)
                reportFilterExpression = GenerateFilterExpression(page, lstReportParameter, tempReportSetting.IsShowParameter);
            #endregion

            if (!tempReportSetting.IsCustom)
            {
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

                    headerTemplate = SetTemplateText(Server, headerTemplate, tempReportSetting.DataSourceHd, entityHd);

                    divContainerReportHeader.InnerHtml = headerTemplate;
                }

                x1 = xdocReport.Descendants("footertemplate");
                if (x1.Count() > 0)
                {
                    string footerTemplate = x1.Single().Value;

                    footerTemplate = SetTemplateText(Server, footerTemplate, tempReportSetting.DataSourceHd, entityHd);

                    //divContainerReportFooter.InnerHtml = footerTemplate;
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
                                        Width = sd.Attribute("width") != null ? Convert.ToDecimal(sd.Attribute("width").Value) : 0,
                                        IsShowSubTotal = sd.Attribute("isshowsubtotal") != null ? sd.Attribute("isshowsubtotal").Value == "1" : false,
                                        InnerHtml = sd.Value,
                                        Align = sd.Attribute("align") != null ? sd.Attribute("align").Value : "left",
                                    }).ToList<TemplateField>();
                #endregion

                #region Repeater Builder
                Repeater rptReport = new Repeater();
                divContainerReportBody.Controls.Add(rptReport);
                rptReport.HeaderTemplate = new MyTemplate(ListItemType.Header, lstTemplateField, lstGroupField, 0, xdocReport.Root.Elements("fields"), tempReportSetting.IsShowHeaderBorder, lstParameterCodeValue);
                if (lstTemplateField.Count > 0)
                {
                    rptReport.ItemTemplate = new MyTemplate(ListItemType.Item, lstTemplateField, lstGroupField, 0, tempReportSetting.TotalType, tempReportSetting.TotalText);

                    object obj = null;
                    if (!tempReportSetting.IsDataSourceFromSP)
                    {
                        string filterExpressionDt = reportFilterExpression;
                        if (filterExpressionDt != "" && tempReportSetting.FilterExpression != "")
                            filterExpressionDt += " AND ";
                        filterExpressionDt += tempReportSetting.FilterExpression;

                        MethodInfo method = typeof(BusinessLayer).GetMethod(tempReportSetting.DataSource, new[] { typeof(string) });
                        obj = method.Invoke(null, new object[] { filterExpressionDt });
                    }
                    else
                    {
                        List<Variable> lstVariable = GenerateFilterExpressionSP(page, lstReportParameter, tempReportSetting.IsShowParameter);
                        obj = BusinessLayer.GetDataReport(tempReportSetting.DataSource, lstVariable);
                    }

                    rptReport.FooterTemplate = new MyTemplate(ListItemType.Footer, lstTemplateField, (IEnumerable<object>)obj, tempReportSetting.IsShowTotal, tempReportSetting.TotalType, tempReportSetting.TotalText);

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
            else
            {
                BaseCustomReportCtl ctl = (BaseCustomReportCtl)page.LoadControl(tempReportSetting.CustomReportURL);
                ctl.Bind(reportFilterExpression, param);
                HtmlGenericControl divReportHeader = (HtmlGenericControl)ctl.FindControl("divReportHeader");
                if (divReportHeader != null)
                {
                    divContainerReportHeader.Style.Remove("display");
                    divContainerReportHeader.Controls.Add(divReportHeader);
                }
                HtmlGenericControl divReportBody = (HtmlGenericControl)ctl.FindControl("divReportBody");
                if (divReportBody != null)
                    divContainerReportBody.Controls.Add(divReportBody);
            }

            if (tempReportSetting.SubHeaderText != "")
            {
                subHeaderText.Style.Remove("display");
                subHeaderText.InnerHtml = SubHeaderText1;
            }
            var sb = new StringBuilder();
            divContent.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            exportExcelValue = sb.ToString();
        }
        #region ReportParameter
        class ReportParameter
        {
            public string Code { get; set; }
            public bool IsShowAsSubTitle { get; set; }
            public bool IsRequired { get; set; }
            public bool IsAllowInDailyReport { get; set; }
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
            public decimal Width { get; set; }
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
            string _totalType;
            bool _isShowHeaderBorder;
            IEnumerable<object> _lstEntity;
            string _totalText;
            IEnumerable<XElement> _lstField;
            List<Variable> _lstParameterCodeValue;
            public MyTemplate(ListItemType type, List<TemplateField> lstTemplateField, List<GroupField> lstGroupField, int level, string totalType, string totalText)
            {
                _type = type;
                _lstTemplateField = lstTemplateField;
                _level = level;
                _lstGroupField = lstGroupField;
                _totalType = totalType;
                _totalType = totalType;
            }
            public MyTemplate(ListItemType type, List<TemplateField> lstTemplateField, List<GroupField> lstGroupField, int level, IEnumerable<XElement> lstField, bool isShowHeaderBorder, List<Variable> lstParameterCodeValue)
            {
                _type = type;
                _lstTemplateField = lstTemplateField;
                _level = level;
                _lstGroupField = lstGroupField;
                _lstField = lstField;
                _isShowHeaderBorder = isShowHeaderBorder;
                _lstParameterCodeValue = lstParameterCodeValue;
            }
            public MyTemplate(ListItemType type, List<TemplateField> lstTemplateField, IEnumerable<object> lstEntity, bool isShowTotal, string totalType, string totalText)
            {
                _type = type;
                _lstTemplateField = lstTemplateField;
                _isShowTotal = isShowTotal;
                _lstEntity = lstEntity;
                _totalText = totalText;
                _totalType = totalType;
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
                    foreach (Variable parameterCodeText in _lstParameterCodeValue)
                    {
                        HeaderText = HeaderText.Replace("{" + parameterCodeText.Code + "}", parameterCodeText.Value);
                    }

                    decimal Width = field.Attribute("width") != null ? Convert.ToDecimal(field.Attribute("width").Value) : 0;
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

                    string widthText = "";
                    if (Colspan < 1)
                        widthText = string.Format(" style='width:{0}%;'", Width);

                    if (FieldType == "customfield")
                        result += string.Format("<th{0}{1}{4} align='{3}'>{2}</th>", rowSpanText, colSpanText, HeaderText, DefaultAlign, widthText);
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
                            ctl.InnerHtml = "<table class='tblReport' style='width:100%' cellpadding='0' cellspacing='0' border='1'><thead>";
                            ctl.InnerHtml += GenerateTableHeader(_lstField.Single(), "");
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
                                    if (_totalType == "SUM")
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
                                        lcSubTotal.Text += string.Format("<td align='right' class='tdDetail tdSubTotalDetail'>{0:N}</td>", _lstEntity.Count());
                                }
                                else
                                    lcSubTotal.Text += "<td class='tdDetail tdGrandDetail'>&nbsp;</td>";
                            }
                            lcSubTotal.Text += "</tr>";
                            container.Controls.Add(lcSubTotal);
                        }
                        container.Controls.Add(new LiteralControl("</tbody></table>"));
                        break;

                    case ListItemType.Item:
                    case ListItemType.AlternatingItem:
                        if (_level < _lstGroupField.Count)
                        {
                            Literal lc = new Literal();
                            Repeater rptDetail = new Repeater();
                            rptDetail.ID = "rptDetail";
                            rptDetail.ItemTemplate = new MyTemplate(ListItemType.Item, _lstTemplateField, _lstGroupField, _level + 1, _totalType, _totalText);

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
                                    lc.Text += "<tr class='trGroup{2} trReportBody'>";
                                    for (int i = 0; i < tdLocation; ++i)
                                        lc.Text += "<td>&nbsp;</td>";
                                    string className = "";
                                    if (_lstGroupField[_level].IsShowBorderTop)
                                        className = " borderTop";
                                    lc.Text += string.Format("<td class='tdGroupName{2}' colspan='{1}'>{0}</td></tr>", _lstGroupField[_level].HeaderText.Replace("[GroupName]", DataBinder.Eval(container1.DataItem, "GroupName").ToString()), _lstTemplateField.Count - tdLocation, className);
                                }
                                else
                                    lc.Text += string.Format("<tr class='trGroup{2} trReportBody'><td class='tdGroupName' colspan='{1}' style='padding-left:{3}0px;'>{0}</td></tr>", _lstGroupField[_level].HeaderText.Replace("[GroupName]", DataBinder.Eval(container1.DataItem, "GroupName").ToString()), _lstTemplateField.Count, _level, _level * 2);
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
                                    lcSubTotal.Text += string.Format("<tr class='trSubTotal{2} trReportBody'><td class='tdSubTotal' colspan='{1}'>{0}</td>", _lstGroupField[_level].SubTotalText.Replace("[GroupName]", DataBinder.Eval(container1.DataItem, "GroupName").ToString()), count, _level, _level * 2);
                                    foreach (TemplateField tf in lstTemplateFieldShowSubTotal)
                                    {
                                        if (tf.IsShowSubTotal)
                                        {
                                            if (_totalType == "SUM")
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
                                                lcSubTotal.Text += string.Format("<td align='right' class='tdDetail tdSubTotalDetail'>{0}</td>", lst.Count());
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
                                lc.Text += "<tr class='trReportBody'>";
                                foreach (TemplateField tf in _lstTemplateField)
                                {

                                    if (tf.FieldType == "customfield")
                                    {
                                        string innerHtml = tf.InnerHtml;
                                        Regex regex = new Regex("{([(a-zA-Z0-9_.,)]*),N}");
                                        MatchCollection collection = regex.Matches(innerHtml);
                                        foreach (Match m in collection)
                                        {
                                            var columnName = m.Groups[1].Value;
                                            innerHtml = innerHtml.Replace("{" + columnName + ",N}", DataBinder.Eval(container1.DataItem, columnName, "{0:N}").ToString());
                                        }

                                        regex = new Regex("{([(a-zA-Z0-9_.,)]*)}");
                                        collection = regex.Matches(innerHtml);
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
                                    else if (tf.FieldType == "time")
                                        lc.Text += string.Format("<td align='center' class='tdDetail'>{0}</td>", DataBinder.Eval(container1.DataItem, tf.FieldName));
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

        private static void rptReport_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
    }
}
