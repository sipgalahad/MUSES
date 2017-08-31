using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Data.Model;
using System.Reflection;
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
    //public partial class ReportViewer : BaseReportViewer
    //{
    //}
    public partial class ReportViewer : BasePage
    {
        static vSite oSite = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Form["param"] != null)
                    hdnParam.Value = Request.Form["param"].ToString();
                if (Request.Form["lang"] != null)
                    hdnLang.Value = Request.Form["lang"].ToString();
                if (Request.Form["facility"] != null)
                    hdnFacility.Value = Request.Form["facility"].ToString();
                if (Request.Form["serviceunit"] != null)
                    hdnServiceUnit.Value = Request.Form["serviceunit"].ToString();
                if (Request.Form["position"] != null)
                    hdnPosition.Value = Request.Form["position"].ToString();
                param = hdnParam.Value.Split('|');
                string reportCode = Page.Request.QueryString["id"];
                List<ReportMaster> lstReportMaster = BusinessLayer.GetReportMasterList(string.Format("ReportCode = '{0}'", reportCode));
                if (lstReportMaster.Count < 1)
                    throw new Exception(string.Format("Report with code {0} is not defined", reportCode));
                reportMaster = lstReportMaster[0];

                ttlTitle.Text = reportMaster.ReportName;
                hdnReportFileName.Value = reportMaster.ReportName;

                hdnFilterExpression.Value = "";

                divReportProperties.InnerHtml = string.Format("{0} - {1}, Print Date/Time:{2}, User ID:{3}", Helper.GetAppName(), reportMaster.ReportCode, DateTime.Now.ToString("dd-MMM-yyyy/HH:mm:ss"), AppSession.UserLogin.UserName);
                divPrintDateTime.InnerHtml = string.Format("{0}", DateTime.Now.ToString("dd-MMM-yyyy/HH:mm:ss"));

                oSite = BusinessLayer.GetvSiteList(string.Format("SiteID = '{0}'", AppSession.UserLogin.SiteID))[0];
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
        List<ConditionalStyle> lstConditionalStyle = null;

        protected decimal paperWidth = 0;
        protected decimal paperHeight = 0;
        protected decimal paperPrintWidth = 0;
        protected decimal paperPrintHeight = 0;
        protected string paperSize = "";
        protected string paperPrintPageContent = "";
        protected string paperPageContent = "";
        protected string fontSize = "";
        protected string fontFamily = "";
        protected string paperPortraitLandscape = "";
        protected string letterSpacingPrint = "";
        protected string fontWeight = "";
        protected string h1FontSize = "";
        protected string pagePaperPadding = "";
        protected string pageContentPaddingTop = "";
        protected string leftRightPosition = "";
        protected string divPageNumberStyle = "";
        protected string customMargin = "";
        protected string borderBottomDetail = "";
        protected int noOfPrintCopy = 0;

        protected string GetImageLogo()
        {
            string url = this.ResolveUrl(string.Format("~/Libs/Images/Client/{0}/logo.png", AppConfigManager.CDXAppClientID));
            string physicalPath = HttpContext.Current.Request.MapPath(url);
            if (!File.Exists(physicalPath))
                return ResolveUrl(string.Format("~/Libs/Images/Client/general/logo.png"));
            return url;
        }

        #region Generate Filter Expression
        string[] param = null;
        private string GetFilterExpression(string value)
        {
            StringBuilder sbResult = new StringBuilder(value);
            sbResult.Replace("@SiteID", AppSession.UserLogin.SiteID);
            sbResult.Replace("@UserID", AppSession.UserLogin.UserID.ToString());
            return sbResult.ToString();
        }

        List<Variable> lstParameterCodeValue = new List<Variable>();
        private List<Variable> GenerateFilterExpressionSP(List<ReportParameter> lstReportParameter, bool isShowParameter, bool isFooterTemplate = false)
        {
            string reportXML = this.ResolveUrl(string.Format("~/Libs/App_Data/report/filterparameter.xml", reportMaster.ReportUrl));
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
                if (value != "")
                {
                    if (reportParameter.Type == Constant.FilterParameterType.FREE_TEXT)
                    {
                        lstVariable.Add(new Variable { Code = reportParameter.FieldName, Value = GetFilterExpression(value) });
                    }
                    else
                    {
                        string paramText = paramSplit[1];
                        if (lstReportParameter[i].IsIncludeAsFilterExpressionDt || isFooterTemplate)
                            lstVariable.Add(new Variable { Code = reportParameter.FieldName, Value = GetFilterExpression(value) });

                        if (paramText != "")
                        {
                            if (lstReportParameter[i].IsShowAsSubTitle && !isFooterTemplate)
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
                }
                else
                {
                    if (lstReportParameter[i].IsIncludeAsFilterExpressionDt || isFooterTemplate)
                        lstVariable.Add(new Variable { Code = reportParameter.FieldName, Value = value });
                }
                displayParameter += "</table>";
            }
            if (!isFooterTemplate)
            {
                if (isShowParameter)
                    divContainerReportParameter.InnerHtml = displayParameter;
                else
                    divContainerReportParameter.Style.Add("display", "none");
            }
            return lstVariable;
        }

        private string GenerateFilterExpression(List<ReportParameter> lstReportParameter, bool isShowParameter, bool isDataSourceFromSP, bool isFooterTemplate)
        {
            string reportXML = this.ResolveUrl(string.Format("~/Libs/App_Data/report/filterparameter.xml", reportMaster.ReportUrl));
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
                    if (lstReportParameter[i].IsIncludeAsFilterExpressionDt || isFooterTemplate)
                    {
                        filterParameter += param[i];
                        if (i > 0 && filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += filterParameter;
                    }
                }
                else
                {
                    if (reportParameter.Type == Constant.FilterParameterType.DATE ||
                        reportParameter.Type == Constant.FilterParameterType.PAST_PERIOD ||
                        reportParameter.Type == Constant.FilterParameterType.UPCOMING_PERIOD)
                    {
                        string[] date = param[i].Split(';');
                        paramText = date[1];
                        if (lstReportParameter[i].IsIncludeAsFilterExpressionDt || isFooterTemplate)
                        {
                            string startDate = date[0].Substring(0, 8);
                            string endDate = date[0].Substring(8, 8);
                            filterParameter = string.Format("{0} BETWEEN '{1}' AND '{2}'", reportParameter.FieldName, startDate, endDate);
                            if (i > 0 && filterExpression != "")
                                filterExpression += " AND ";
                            filterExpression += filterParameter;
                        }
                    }
                    else if (reportParameter.Type == Constant.FilterParameterType.SINGLE_DATE)
                    {
                        string[] paramSplit = param[i].Split(';');
                        paramText = paramSplit[1];
                        string value = paramSplit[0];
                        if (lstReportParameter[i].IsIncludeAsFilterExpressionDt || isFooterTemplate)
                        {
                            filterParameter = string.Format("{0} = '{1}'", reportParameter.FieldName, value);
                            if (i > 0 && filterExpression != "")
                                filterExpression += " AND ";
                            filterExpression += filterParameter;
                        }
                    }
                    else if (reportParameter.Type == Constant.FilterParameterType.COMBO_BOX || reportParameter.Type == Constant.FilterParameterType.YEAR_COMBO_BOX || reportParameter.Type == Constant.FilterParameterType.CUSTOM_COMBO_BOX || reportParameter.Type == Constant.FilterParameterType.SEARCH_DIALOG)
                    {
                        string[] paramSplit = param[i].Split(';');
                        string value = paramSplit[0];
                        if (value != "")
                        {
                            paramText = paramSplit[1];
                            if (lstReportParameter[i].IsIncludeAsFilterExpressionDt || isFooterTemplate)
                            {
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
                        }
                    }
                    else
                    {
                        string[] paramSplit = param[i].Split(';');
                        paramText = paramSplit[1];
                        if (lstReportParameter[i].IsIncludeAsFilterExpressionDt || isFooterTemplate)
                        {
                            if (i > 0 && filterExpression != "")
                                filterExpression += " AND ";
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
                if (paramText != "")
                {
                    if (lstReportParameter[i].IsShowAsSubTitle && !isFooterTemplate && !isDataSourceFromSP)
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
            if (!isFooterTemplate)
            {
                if (!isDataSourceFromSP)
                {
                    if (isShowParameter)
                        divContainerReportParameter.InnerHtml = displayParameter;
                    else
                        divContainerReportParameter.Style.Add("display", "none");
                }
            }
            return filterExpression;
        }
        #endregion

        #region Regex Header Footer
        private static string SetTemplateText(string templateText, string dataSourceHd, object entityHd)
        {
            Regex regex = new Regex(@"{Site\.([(a-zA-Z0-9_.,)]*)}");
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

            regex = new Regex(@"{SettingParameter\.([(a-zA-Z0-9_.,)]*)}");
            collection = regex.Matches(templateText);
            foreach (Match m in collection)
            {
                var columnName = m.Groups[1].Value;
                SettingParameter sp = BusinessLayer.GetSettingParameter(columnName);
                templateText = templateText.Replace("{SettingParameter." + columnName + "}", sp.ParameterValue);
            }

            regex = new Regex(@"{SiteParameter\.([(a-zA-Z0-9_.,)]*)}");
            collection = regex.Matches(templateText);
            foreach (Match m in collection)
            {
                var columnName = m.Groups[1].Value;
                SiteParameter sp = BusinessLayer.GetSiteParameter(AppSession.UserLogin.SiteID, columnName);
                templateText = templateText.Replace("{SiteParameter." + columnName + "}", sp.ParameterValue);
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
                fieldValue = System.Web.HttpUtility.HtmlDecode(fieldValue);
                templateText = templateText.Replace("{" + columnName + "}", HttpContext.Current.Server.HtmlDecode(fieldValue));
            }
            return templateText;
        }
        #endregion

        string SubHeaderText1 = "";
        private void BindGridView()
        {
            #region Load Report File
            string reportXML = "";
            string physicalPath = "";
            if (hdnServiceUnit.Value != "")
            {
                string[] temp = reportMaster.ReportUrl.Split('/');
                string reportUrlFolder = "";
                for (int i = 0; i < temp.Length - 1; ++i)
                {
                    if (reportUrlFolder != "")
                        reportUrlFolder += "/";
                    reportUrlFolder += temp[i];
                }
                string reportUrl = temp[temp.Length - 1];
                reportXML = this.ResolveUrl(string.Format("~/Libs/App_Data/report/{0}/{1}/serviceunit/{2}/{3}.xml", AppConfigManager.CDXAppClientID, reportUrlFolder, hdnServiceUnit.Value, reportUrl));
                physicalPath = HttpContext.Current.Request.MapPath(reportXML);
                if (!File.Exists(physicalPath))
                {
                    reportXML = this.ResolveUrl(string.Format("~/Libs/App_Data/report/{0}/{1}.xml", AppConfigManager.CDXAppClientID, reportMaster.ReportUrl));
                    physicalPath = HttpContext.Current.Request.MapPath(reportXML);
                    if (!File.Exists(physicalPath))
                    {
                        reportXML = this.ResolveUrl(string.Format("~/Libs/App_Data/report/general/{0}.xml", reportMaster.ReportUrl));
                        physicalPath = HttpContext.Current.Request.MapPath(reportXML);
                        if (!File.Exists(physicalPath))
                            return;
                    }
                }
            }
            else
            {
                if (hdnLang.Value == "")
                {
                    reportXML = this.ResolveUrl(string.Format("~/Libs/App_Data/report/{0}/{1}.xml", AppConfigManager.CDXAppClientID, reportMaster.ReportUrl));
                    physicalPath = HttpContext.Current.Request.MapPath(reportXML);
                    if (!File.Exists(physicalPath))
                    {
                        reportXML = this.ResolveUrl(string.Format("~/Libs/App_Data/report/general/{0}.xml", reportMaster.ReportUrl));
                        physicalPath = HttpContext.Current.Request.MapPath(reportXML);
                        if (!File.Exists(physicalPath))
                            return;
                    }
                }
                else
                {
                    string[] temp = reportMaster.ReportUrl.Split('/');
                    string reportUrlFolder = "";
                    for (int i = 0; i < temp.Length - 1; ++i)
                    {
                        if (reportUrlFolder != "")
                            reportUrlFolder += "/";
                        reportUrlFolder += temp[i];
                    }
                    string reportUrl = temp[temp.Length - 1];
                    reportXML = this.ResolveUrl(string.Format("~/Libs/App_Data/report/{0}/{1}/lang/{2}/{3}.xml", AppConfigManager.CDXAppClientID, reportUrlFolder, hdnLang.Value, reportUrl));
                    physicalPath = HttpContext.Current.Request.MapPath(reportXML);
                    if (!File.Exists(physicalPath))
                    {
                        reportXML = this.ResolveUrl(string.Format("~/Libs/App_Data/report/{0}/{1}.xml", AppConfigManager.CDXAppClientID, reportMaster.ReportUrl));
                        physicalPath = HttpContext.Current.Request.MapPath(reportXML);
                        if (!File.Exists(physicalPath))
                        {
                            reportXML = this.ResolveUrl(string.Format("~/Libs/App_Data/report/general/{0}/lang/{1}/{2}.xml", reportUrlFolder, hdnLang.Value, reportUrl));
                            physicalPath = HttpContext.Current.Request.MapPath(reportXML);
                            if (!File.Exists(physicalPath))
                            {
                                reportXML = this.ResolveUrl(string.Format("~/Libs/App_Data/report/general/{0}.xml", reportMaster.ReportUrl));
                                physicalPath = HttpContext.Current.Request.MapPath(reportXML);
                                if (!File.Exists(physicalPath))
                                    return;
                            }
                        }
                    }
                }
            }
            XDocument xdocReport = XDocument.Load(physicalPath);
            var tempReportSetting = (from sd in xdocReport.Descendants("table")
                                     select new
                                     {
                                         IsCustom = sd.Attribute("iscustom") != null ? sd.Attribute("iscustom").Value == "1" : false,
                                         CustomReportURL = sd.Attribute("customreporturl") != null ? sd.Attribute("customreporturl").Value : "",
                                         DataSource = sd.Attribute("datasource") != null ? sd.Attribute("datasource").Value : "",
                                         DataSourceTableFooter = sd.Attribute("datasourcetablefooter") != null ? sd.Attribute("datasourcetablefooter").Value : "",
                                         IsDataSourceTableFooterFromSP = sd.Attribute("isdatasourcetablefooterfromsp") != null ? sd.Attribute("isdatasourcetablefooterfromsp").Value == "1" : false,
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
                                         IsHeaderShowPrintDateTime = sd.Attribute("isheadershowprintdatetime") != null ? sd.Attribute("isheadershowprintdatetime").Value == "1" : false,
                                         IsShowPageNumber = sd.Attribute("isshowpagenumber") != null ? sd.Attribute("isshowpagenumber").Value == "1" : true,
                                         IsShowParameter = sd.Attribute("isshowparameter") != null ? sd.Attribute("isshowparameter").Value == "1" : false,
                                         CustomPadding = sd.Attribute("custompadding") != null ? sd.Attribute("custompadding").Value : "",
                                         CustomMargin = sd.Attribute("custommargin") != null ? sd.Attribute("custommargin").Value : "0px",
                                         IsShowHeaderBorder = sd.Attribute("isshowheaderborder") != null ? sd.Attribute("isshowheaderborder").Value == "1" : false,
                                         IsReportItemShowBorderBottom = sd.Attribute("isreportitemshowborderbottom") != null ? sd.Attribute("isreportitemshowborderbottom").Value == "1" : false,
                                         NoOfPrintCopy = sd.Attribute("noofprintcopy") != null ? Convert.ToInt32(sd.Attribute("noofprintcopy").Value) : 0
                                     }).FirstOrDefault();

            noOfPrintCopy = tempReportSetting.NoOfPrintCopy;
            borderBottomDetail = "0px";
            if (tempReportSetting.IsReportItemShowBorderBottom)
                borderBottomDetail = "1px dotted black";
            fontFamily = tempReportSetting.FontFamily;
            customMargin = tempReportSetting.CustomMargin;
            if (tempReportSetting.IsUsingDotMatrix)
            {
                if (fontFamily == "")
                    fontFamily = "'Courier New'";
                else
                    fontFamily = string.Format("'{0}'", fontFamily);
                if (tempReportSetting.DotMatrixDPI == "")
                {
                    if (AppConfigManager.CDXDotMatrixDPI == "120x144")
                        letterSpacingPrint = "0cm";
                    else
                        letterSpacingPrint = "0.1cm";
                }
                else
                {
                    if (tempReportSetting.DotMatrixDPI == "120x144")
                        letterSpacingPrint = "0cm";
                    else
                        letterSpacingPrint = "0.1cm";
                }
                pagePaperPadding = "0";
                fontWeight = "normal";
                h1FontSize = "14pt";
                leftRightPosition = "0cm";
            }
            else
            {
                if (fontFamily == "")
                    fontFamily = "'Tahoma', 'TahomaLoad'";
                else
                    fontFamily = string.Format("'{0}'", fontFamily);
                letterSpacingPrint = "0";
                fontWeight = "bold;";
                h1FontSize = "12pt";
                pagePaperPadding = "0.2cm 0.7cm";
                leftRightPosition = "0.7cm";
            }

            string reportHeaderXML = this.ResolveUrl(string.Format("~/Libs/App_Data/report/{0}/CustomHeader.xml", AppConfigManager.CDXAppClientID, reportMaster.ReportUrl));
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
                        headerTemplate = headerTemplate.Replace("{ResolveUrl." + url + "}", ResolveUrl(url));
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

                    divPageHeader.InnerHtml = headerTemplate;
                }
            }


            fontSize = tempReportSetting.FontSize;

            SubHeaderText1 = tempReportSetting.SubHeaderText;
            if (!tempReportSetting.IsShowHeaderFooter)
            {
                if (!tempReportSetting.IsShowHeader)
                    divPageHeader.Style.Add("display", "none");
                if (!tempReportSetting.IsShowFooter)
                    divContainerPageFooter.Style.Add("display", "none");
            }
            if (!tempReportSetting.IsHeaderShowPrintDateTime)
                divPrintDateTime.Style.Add("display", "none");
            if (!tempReportSetting.IsShowPageNumber)
                divPageNumberStyle = "display:none";

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
                                                            IsIncludeAsFilterExpressionDt = sd.Attribute("isincludeasfilterexpressiondt") != null ? sd.Attribute("isincludeasfilterexpressiondt").Value == "1" : true
                                                        }).ToList<ReportParameter>();
            //if (!tempReportSetting.IsDataSourceFromSP)
            reportFilterExpression = GenerateFilterExpression(lstReportParameter, tempReportSetting.IsShowParameter, tempReportSetting.IsDataSourceFromSP, false);
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
                                     PaperPortraitLandscape = sd.Attribute("landscape").Value == "1" ? "landscape" : "portrait",
                                     Positions = (from grd in sd.Descendants("position")
                                                  select new
                                                  {
                                                      Position = grd.Attribute("position").Value,
                                                      PaddingTop = grd.Attribute("paddingtop").Value
                                                  }).ToList(),
                                 }).FirstOrDefault();

            if (hdnPosition.Value != "0" && hdnPosition.Value != "" && hdnPosition.Value != "1")
            {
                var tempPaperSizePadding = tempPaperSize.Positions.FirstOrDefault(p => p.Position == hdnPosition.Value);
                if (tempPaperSizePadding != null)
                    pageContentPaddingTop = tempPaperSizePadding.PaddingTop;
                else
                    pageContentPaddingTop = "";
            }
            else
                pageContentPaddingTop = "0";

            paperPortraitLandscape = tempPaperSize.PaperPortraitLandscape;
            paperHeight = tempPaperSize.Height;
            paperWidth = tempPaperSize.Width;
            paperPrintHeight = tempPaperSize.PrintHeight;
            paperPrintWidth = tempPaperSize.PrintWidth;
            paperSize = tempPaperSize.Size;
            paperPrintPageContent = tempPaperSize.PrintPageContent;
            paperPageContent = tempPaperSize.PageContent;
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

                    headerTemplate = SetTemplateText(headerTemplate, tempReportSetting.DataSourceHd, entityHd);

                    divContainerReportHeader.InnerHtml = headerTemplate;
                }

                x1 = xdocReport.Descendants("itemtemplate");
                if (x1.Count() > 0)
                {
                    string itemTemplate = x1.Single().Value;

                    itemTemplate = SetTemplateText(itemTemplate, tempReportSetting.DataSourceHd, entityHd);

                    divContainerReportItem.InnerHtml = itemTemplate;
                }

                x1 = xdocReport.Descendants("footertemplate");
                if (x1.Count() > 0)
                {
                    string footerTemplate = x1.Single().Value;

                    footerTemplate = SetTemplateText(footerTemplate, tempReportSetting.DataSourceHd, entityHd);

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
                                        TotalFieldName = sd.Attribute("totalfieldname") != null ? sd.Attribute("totalfieldname").Value : "",
                                        AdditionalClassFieldName = sd.Attribute("additionalclassfieldname") != null ? sd.Attribute("additionalclassfieldname").Value : "",
                                        FieldType = sd.Attribute("fieldtype") != null ? sd.Attribute("fieldtype").Value : "",
                                        HeaderText = sd.Attribute("headertext").Value,
                                        Width = sd.Attribute("width") != null ? Convert.ToDecimal(sd.Attribute("width").Value) : 0,
                                        IsShowSubTotal = sd.Attribute("isshowsubtotal") != null ? sd.Attribute("isshowsubtotal").Value == "1" : false,
                                        IsShowAvg = sd.Attribute("isshowavg") != null ? sd.Attribute("isshowavg").Value == "1" : false,
                                        AvgFormula = sd.Attribute("avgformula") != null ? sd.Attribute("avgformula").Value : "",
                                        InnerHtml = sd.Value,
                                        Align = sd.Attribute("align") != null ? sd.Attribute("align").Value : "left",
                                    }).ToList<TemplateField>();

                lstConditionalStyle = (from itx in xdocReport.Descendants("conditionalstyle")
                                       select new ConditionalStyle
                                       {
                                           DataField = itx.Attribute("datafield").Value,
                                           Style = itx.Attribute("style").Value
                                       }).ToList<ConditionalStyle>();
                #endregion

                #region Repeater Builder
                rptReport.HeaderTemplate = new MyTemplate(ListItemType.Header, lstTemplateField, lstGroupField, 0, xdocReport.Root.Elements("fields"), tempReportSetting.IsShowHeaderBorder, lstParameterCodeValue);
                if (lstTemplateField.Count > 0)
                {
                    rptReport.ItemTemplate = new MyTemplate(ListItemType.Item, lstTemplateField, lstConditionalStyle, lstGroupField, 0, tempReportSetting.TotalType, tempReportSetting.TotalText, xdocReport, tempReportSetting.DataSource);

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
                        List<Variable> lstVariable = GenerateFilterExpressionSP(lstReportParameter, tempReportSetting.IsShowParameter);
                        obj = BusinessLayer.GetDataReport(tempReportSetting.DataSource, lstVariable);
                    }

                    List<dynamic> lstEntityFooter = null;
                    string tableFooterTemplate = "";
                    if (tempReportSetting.DataSourceTableFooter != "")
                    {
                        if (tempReportSetting.IsDataSourceTableFooterFromSP)
                        {
                            List<Variable> lstVariable = GenerateFilterExpressionSP(lstReportParameter, tempReportSetting.IsShowParameter, true);
                            lstEntityFooter = BusinessLayer.GetDataReport(tempReportSetting.DataSourceTableFooter, lstVariable);
                        }
                        else
                        {
                            string filterExpressionDt = reportFilterExpression;
                            if (filterExpressionDt != "" && tempReportSetting.FilterExpression != "")
                                filterExpressionDt += " AND ";
                            filterExpressionDt += tempReportSetting.FilterExpression;

                            MethodInfo method = typeof(BusinessLayer).GetMethod(tempReportSetting.DataSourceTableFooter, new[] { typeof(string) });
                            lstEntityFooter = (List<dynamic>)method.Invoke(null, new object[] { filterExpressionDt });
                        }

                        x1 = xdocReport.Descendants("tablefootertemplate");
                        if (x1.Count() > 0)
                            tableFooterTemplate = x1.Single().Value;
                    }

                    rptReport.FooterTemplate = new MyTemplate(ListItemType.Footer, lstTemplateField, (IEnumerable<object>)obj, tempReportSetting.IsShowTotal, tempReportSetting.TotalType, tempReportSetting.TotalText, lstEntityFooter, tableFooterTemplate, tempReportSetting.DataSourceTableFooter);

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

                        IEnumerable<object> tempLst2 = (IEnumerable<object>)lst2;
                        foreach (object tempEntiy in tempLst2)
                        {
                            IList temp = (IList)(tempEntiy.GetType().GetProperty("Items").GetValue(tempEntiy, null));
                            object a = temp[0];

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
                BaseCustomReportCtl ctl = (BaseCustomReportCtl)LoadControl(tempReportSetting.CustomReportURL);
                ctl.Bind(reportFilterExpression, param);
                HtmlGenericControl divReportHeader = (HtmlGenericControl)ctl.FindControl("divReportHeader");
                if (divReportHeader != null)
                {
                    divContainerReportHeader.Style.Remove("display");
                    divContainerReportHeader.Controls.Add(divReportHeader);
                }
                HtmlGenericControl divReportFooter = (HtmlGenericControl)ctl.FindControl("divReportFooter");
                if (divReportFooter != null)
                {
                    divContainerReportFooter.Style.Remove("display");
                    divContainerReportFooter.Controls.Add(divReportFooter);
                }
                HtmlGenericControl divPageFooter = (HtmlGenericControl)ctl.FindControl("divPageFooter");
                if (divPageFooter != null)
                {
                    divContainerPageFooter.Style.Remove("display");
                    divContainerPageFooter.Controls.Clear();
                    divContainerPageFooter.Controls.Add(divPageFooter);
                }
                HtmlGenericControl divReportBody = (HtmlGenericControl)ctl.FindControl("divReportBody");
                if (divReportBody != null)
                    divContainerReportBody.Controls.Add(divReportBody);
            }

            if (tempReportSetting.IsUsingDotMatrix)
                tdImageLogo.Style.Add("display", "none");

            if (tempReportSetting.SubHeaderText != "")
            {
                subHeaderText.Style.Remove("display");
                subHeaderText.InnerHtml = SubHeaderText1;
            }
        }
        #region ReportParameter
        class ReportParameter
        {
            public string Code { get; set; }
            public bool IsShowAsSubTitle { get; set; }
            public bool IsRequired { get; set; }
            public bool IsIncludeAsFilterExpressionDt { get; set; }
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
            public string TotalFieldName { get; set; }
            public string AdditionalClassFieldName { get; set; }
            public string FieldType { get; set; }
            public string InnerHtml { get; set; }
            public bool IsShowSubTotal { get; set; }
            public bool IsShowAvg { get; set; }
            public string AvgFormula { get; set; }
            public string Align { get; set; }
        }
        #endregion

        #region ConditionalStyle
        class ConditionalStyle
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
        #endregion

        class MyTemplate : ITemplate
        {
            ListItemType _type;
            List<TemplateField> _lstTemplateField;
            List<ConditionalStyle> _lstConditionalStyle;
            List<GroupField> _lstGroupField;
            int _level;
            bool _isShowTotal;
            string _totalType;
            bool _isShowHeaderBorder;
            IEnumerable<object> _lstEntity;
            string _totalText;
            IEnumerable<XElement> _lstField;
            List<Variable> _lstParameterCodeValue;
            List<dynamic> _lstEntityFooter;
            string _tableFooterTemplate;
            string _dataSourceTableFooter;
            string _dataSourceDt;
            XDocument _xdocReport;
            public MyTemplate(ListItemType type, List<TemplateField> lstTemplateField, List<ConditionalStyle> lstConditionalStyle, List<GroupField> lstGroupField, int level, string totalType, string totalText, XDocument xdocReport, string dataSourceDt)
            {
                _type = type;
                _lstTemplateField = lstTemplateField;
                _lstConditionalStyle = lstConditionalStyle;
                _level = level;
                _lstGroupField = lstGroupField;
                _totalType = totalType;
                _totalType = totalType;
                _xdocReport = xdocReport;
                _dataSourceDt = dataSourceDt;
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
            public MyTemplate(ListItemType type, List<TemplateField> lstTemplateField, IEnumerable<object> lstEntity, bool isShowTotal, string totalType, string totalText, List<dynamic> lstEntityFooter, string tableFooterTemplate, string dataSourceTableFooter)
            {
                _type = type;
                _lstTemplateField = lstTemplateField;
                _isShowTotal = isShowTotal;
                _lstEntity = lstEntity;
                _totalText = totalText;
                _totalType = totalType;
                _lstEntityFooter = lstEntityFooter;
                _tableFooterTemplate = tableFooterTemplate;
                _dataSourceTableFooter = dataSourceTableFooter;
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
                    if (Colspan < 1 && Width > 0)
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
                    decimal Width = field.Attribute("width") != null ? Convert.ToDecimal(field.Attribute("width").Value) : 0;
                    if (Rowspan > 0)
                        rowSpanText = string.Format(" rowspan='{0}'", Rowspan);
                    if (Colspan > 0)
                        colSpanText = string.Format(" colspan='{0}'", Colspan);
                    string widthText = "";
                    if (Width > 0)
                        widthText = string.Format(" style='width:{0}%;'", Width);
                    result += string.Format("<th{1}{2}{3} align='center'>{0}</th>", text, rowSpanText, colSpanText, widthText);
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
                            ctl.InnerHtml = "<table class='tblReport' style='width:100%' cellpadding='0' cellspacing='0'><thead>";
                            ctl.InnerHtml += GenerateTableHeader(_lstField.Single(), "");
                            ctl.InnerHtml += "</thead><tbody class='reportBody'>";
                            container.Controls.Add(ctl.Controls[0]);
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
                            lcSubTotal.Text += string.Format("<tr class='trGrandTotal trReportBody'><td class='tdGrandTotal' colspan='{1}'>{0}</td>", _totalText, count);
                            foreach (TemplateField tf in lstTemplateFieldShowSubTotal)
                            {
                                if (tf.IsShowSubTotal)
                                {
                                    if (_totalType == "SUM")
                                    {
                                        if (tf.FieldType == "currency")
                                        {
                                            decimal subtotal = 0;
                                            if (tf.TotalFieldName != "")
                                                subtotal = _lstEntity.Sum(c => Convert.ToDecimal(c.GetType().GetProperty(tf.TotalFieldName).GetValue(c, null)));
                                            else
                                                subtotal = _lstEntity.Sum(c => Convert.ToDecimal(c.GetType().GetProperty(tf.FieldName).GetValue(c, null)));
                                            lcSubTotal.Text += string.Format("<td align='right' class='tdDetail tdSubTotalDetail'>{0:N}</td>", subtotal);
                                        }
                                        else
                                        {
                                            decimal subtotal = 0;
                                            if (tf.TotalFieldName != "")
                                                subtotal = _lstEntity.Sum(c => Convert.ToDecimal(c.GetType().GetProperty(tf.TotalFieldName).GetValue(c, null)));
                                            else
                                                subtotal = _lstEntity.Sum(c => Convert.ToDecimal(c.GetType().GetProperty(tf.FieldName).GetValue(c, null)));
                                            lcSubTotal.Text += string.Format("<td align='right' class='tdDetail tdSubTotalDetail'>{0:N}</td>", subtotal);
                                        }
                                    }
                                    else
                                        lcSubTotal.Text += string.Format("<td align='right' class='tdDetail tdSubTotalDetail'>{0:N}</td>", _lstEntity.Count());
                                }
                                else if (tf.IsShowAvg)
                                {
                                    String[] temp = tf.AvgFormula.Split('/');
                                    decimal subtotal = _lstEntity.Sum(c => Convert.ToDecimal(c.GetType().GetProperty(temp[0]).GetValue(c, null)));
                                    decimal diff = _lstEntity.Sum(c => Convert.ToDecimal(c.GetType().GetProperty(temp[1]).GetValue(c, null)));
                                    lcSubTotal.Text += string.Format("<td align='right' class='tdDetail tdSubTotalDetail'>{0:N}</td>", subtotal / diff);
                                }
                                else
                                    lcSubTotal.Text += "<td class='tdDetail tdGrandDetail'>&nbsp;</td>";
                            }
                            lcSubTotal.Text += "</tr>";
                            container.Controls.Add(lcSubTotal);
                        }
                        if (_lstEntityFooter != null)
                        {
                            foreach (object obj1 in _lstEntityFooter)
                            {
                                Literal lcSubTotal = new Literal();
                                string tableFooterTemplate = SetTemplateText(_tableFooterTemplate, _dataSourceTableFooter, obj1);
                                lcSubTotal.Text += string.Format("<tr class='trGrandTotal trReportBody'>{0}</tr>", tableFooterTemplate);
                                container.Controls.Add(lcSubTotal);
                            }
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
                            rptDetail.ItemTemplate = new MyTemplate(ListItemType.Item, _lstTemplateField, _lstConditionalStyle, _lstGroupField, _level + 1, _totalType, _totalText, _xdocReport, _dataSourceDt);

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
                                string innerHtml = "";
                                IEnumerable<XElement> x1 = _xdocReport.Descendants("grouptemplate" + (_level + 1));
                                if (x1.Count() > 0)
                                {
                                    string groupTemplate = x1.Single().Value;
                                    IList temp = (IList)DataBinder.Eval(container1.DataItem, "Items");
                                    object bindEntity = temp[0];
                                    groupTemplate = SetTemplateText(groupTemplate, _dataSourceDt, bindEntity);
                                    innerHtml = groupTemplate;
                                }
                                else
                                    innerHtml = _lstGroupField[_level].HeaderText.Replace("[GroupName]", DataBinder.Eval(container1.DataItem, "GroupName").ToString());
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
                                    lc.Text += string.Format("<td id='tdContainerGroup' runat='server' class='tdGroupName{2}' colspan='{1}'>{0}</td></tr>", innerHtml, _lstTemplateField.Count - tdLocation, className);
                                }
                                else
                                    lc.Text += string.Format("<tr id='tdContainerGroup' runat='server' class='trGroup{2} trReportBody'><td class='tdGroupName' colspan='{1}' style='padding-left:{3}0px;'>{0}</td></tr>", innerHtml, _lstTemplateField.Count, _level, _level * 2);
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
                                                    decimal subtotal = 0;
                                                    if (tf.TotalFieldName != "")
                                                        subtotal = lst.Sum(c => Convert.ToDecimal(c.GetType().GetProperty(tf.TotalFieldName).GetValue(c, null)));
                                                    else
                                                        subtotal = lst.Sum(c => Convert.ToDecimal(c.GetType().GetProperty(tf.FieldName).GetValue(c, null)));
                                                    lcSubTotal.Text += string.Format("<td align='right' class='tdDetail tdSubTotalDetail'>{0:N}</td>", subtotal);
                                                }
                                                else
                                                {
                                                    decimal subtotal = 0;
                                                    if (tf.TotalFieldName != "")
                                                        subtotal = lst.Sum(c => Convert.ToDecimal(c.GetType().GetProperty(tf.TotalFieldName).GetValue(c, null)));
                                                    else
                                                        subtotal = lst.Sum(c => Convert.ToDecimal(c.GetType().GetProperty(tf.FieldName).GetValue(c, null)));
                                                    lcSubTotal.Text += string.Format("<td align='right' class='tdDetail tdSubTotalDetail'>{0:N}</td>", subtotal);
                                                }
                                            }
                                            else
                                                lcSubTotal.Text += string.Format("<td align='right' class='tdDetail tdSubTotalDetail'>{0}</td>", lst.Count());
                                        }
                                        else if (tf.IsShowAvg)
                                        {
                                            String[] temp = tf.AvgFormula.Split('/');
                                            decimal subtotal = lst.Sum(c => Convert.ToDecimal(c.GetType().GetProperty(temp[0]).GetValue(c, null)));
                                            decimal diff = lst.Sum(c => Convert.ToDecimal(c.GetType().GetProperty(temp[1]).GetValue(c, null)));
                                            lcSubTotal.Text += string.Format("<td align='right' class='tdDetail tdSubTotalDetail'>{0:N}</td>", subtotal / diff);
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
                                string customStyle = "";
                                foreach (ConditionalStyle conditionalStyle in _lstConditionalStyle)
                                {
                                    bool conditionalValue = (bool)DataBinder.Eval(container1.DataItem, conditionalStyle.DataField);
                                    if (conditionalValue)
                                        customStyle = conditionalStyle.Style + " !important;";
                                }
                                lc.Text += "<tr class='trReportBody'>";
                                int ctr = 0;

                                foreach (TemplateField tf in _lstTemplateField)
                                {
                                    string stylePaddingLeftTag = "";
                                    if (ctr == 0 && _lstGroupField.Count > 0)
                                        stylePaddingLeftTag = string.Format(" style='padding-left:{0}0px;{1}'", _lstGroupField.Count, customStyle);
                                    else if (customStyle != "")
                                        stylePaddingLeftTag = string.Format(" style='{0}'", customStyle);

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
                                        lc.Text += string.Format("<td align='{1}' class='tdDetail'{2}>{0}</td>", innerHtml, tf.Align, stylePaddingLeftTag);
                                    }
                                    else
                                    {
                                        string additionalClass = "";
                                        if (tf.AdditionalClassFieldName != "")
                                            additionalClass = DataBinder.Eval(container1.DataItem, tf.AdditionalClassFieldName).ToString();

                                        if (tf.FieldType == "date")
                                            lc.Text += string.Format("<td align='center' class='tdDetail {2}'{1}>{0:dd-MMM-yyyy}</td>", DataBinder.Eval(container1.DataItem, tf.FieldName), stylePaddingLeftTag, additionalClass);
                                        else if (tf.FieldType == "autonumber")
                                            lc.Text += string.Format("<td align='right' class='tdDetail tdAutoNumber {2}'{1}>{0}.</td>", container1.ItemIndex + 1, stylePaddingLeftTag, additionalClass);
                                        else if (tf.FieldType == "currency")
                                            lc.Text += string.Format("<td align='right' class='tdDetail {2}'{1}>{0:N}</td>", DataBinder.Eval(container1.DataItem, tf.FieldName), stylePaddingLeftTag, additionalClass);
                                        else if (tf.FieldType == "number")
                                            lc.Text += string.Format("<td align='right' class='tdDetail {2}'{1}>{0:N}</td>", DataBinder.Eval(container1.DataItem, tf.FieldName), stylePaddingLeftTag, additionalClass);
                                        else if (tf.FieldType == "time")
                                            lc.Text += string.Format("<td align='center' class='tdDetail {2}'{1}>{0}</td>", DataBinder.Eval(container1.DataItem, tf.FieldName), stylePaddingLeftTag, additionalClass);
                                        else
                                            lc.Text += string.Format("<td class='tdDetail {2}'{1}>{0}</td>", DataBinder.Eval(container1.DataItem, tf.FieldName), stylePaddingLeftTag, additionalClass);
                                    }
                                    ctr++;
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

            string attachment = string.Format("attachment;filename=\"{0}.xls\"", hdnReportFileName.Value);
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