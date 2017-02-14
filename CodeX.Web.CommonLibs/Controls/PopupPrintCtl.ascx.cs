using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using System.Xml.Linq;
using CodeX.Data.Model;
using CodeX.Common;
using System.IO;
using DevExpress.Web.ASPxCallbackPanel;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing.Printing;
using System.Drawing;

namespace CodeX.Web.CommonLibs.Controls
{
    public partial class PopupPrintCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            if (param != "")
            {
                string[] temp = param.Split('|');
                bool isSelectLanguage = false;
                if (temp.Length > 1)
                    isSelectLanguage = temp[1] == "1";

                string menuCode = temp[0];
                string moduleName = Helper.GetModuleName();
                string ModuleID = Helper.GetModuleID(moduleName);
                hdnDepartmentID.Value = ((BasePage)Page).OnGetDepartmentID();

                if (isSelectLanguage)
                {
                    //List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.LANGUAGE_TYPE));
                    //Methods.SetComboBoxField<StandardCode>(cboLanguage, lstSc, "StandardCodeName", "TagProperty");
                    //StandardCode defaultLang = lstSc.FirstOrDefault(p => p.IsDefault);
                    //if (defaultLang == null)
                    //    defaultLang = lstSc.FirstOrDefault();
                    //cboLanguage.Value = defaultLang.TagProperty;

                    //if (lstSc.Count < 2)
                    //{
                    //    divLanguage.Style.Add("display", "none");
                    //    hdnIsChooseLang.Value = "0";
                    //}
                    //else
                    //    hdnIsChooseLang.Value = "1";
                }
                else
                {
                    divLanguage.Style.Add("display", "none");
                    hdnIsChooseLang.Value = "0";
                }

                List<GetReportUserList> lstReport = BusinessLayer.GetReportUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.ReportType.FORM, ModuleID, menuCode, "");
                rptPrint.DataSource = lstReport;
                rptPrint.DataBind();
            }
        }

        vSite oSite = null;
        protected void cbpProcessPrintPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string errMessage = "";
            bool isUsingDirectPrint = false;
            string result = "";
            if (OnPrintData(ref isUsingDirectPrint, ref errMessage))
                result = "success";
            else
                result = "fail|" + errMessage;

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpIsUsingDirectPrint"] = isUsingDirectPrint ? "1" : "0";
            panel.JSProperties["cpResult"] = result;
        }

        private bool OnPrintData(ref bool isUsingDirectPrint, ref string errMessage)
        {
            try
            {
                oSite = BusinessLayer.GetvSiteList(string.Format("SiteID = '{0}'", AppSession.UserLogin.SiteID))[0];
                List<ReportMaster> lstReportMaster = BusinessLayer.GetReportMasterList(string.Format("ReportCode = '{0}'", hdnReportCode.Value));
                ReportMaster reportMaster = lstReportMaster[0];
                #region Load Report File
                string reportXML = this.ResolveUrl(string.Format("~/Libs/App_Data/report/{0}/{1}.xml", AppConfigManager.CDXAppClientID, reportMaster.ReportUrl));
                string physicalPath = HttpContext.Current.Request.MapPath(reportXML);
                if (!File.Exists(physicalPath))
                {
                    reportXML = this.ResolveUrl(string.Format("~/Libs/App_Data/report/general/{0}.xml", reportMaster.ReportUrl));
                    physicalPath = HttpContext.Current.Request.MapPath(reportXML);
                    if (!File.Exists(physicalPath))
                        return false;
                }
                #endregion
                param = hdnFilterExpression.Value.Split('|');
                XDocument xdocReport = XDocument.Load(physicalPath);
                var tempReportSetting = (from sd in xdocReport.Descendants("table")
                                         select new
                                         {
                                             IsUsingZPL = sd.Attribute("isusingzpl") != null ? sd.Attribute("isusingzpl").Value == "1" : false,
                                             IsUsingDirectPrint = sd.Attribute("isusingdirectprint") != null ? sd.Attribute("isusingdirectprint").Value == "1" : false,
                                             FontFamily = sd.Attribute("fontfamily") != null ? sd.Attribute("fontfamily").Value : "",
                                             FontSize = sd.Attribute("fontsize") != null ? sd.Attribute("fontsize").Value : "",
                                             TotalLength = sd.Attribute("totallength") != null ? Convert.ToInt32(sd.Attribute("totallength").Value) : 40,
                                             FilterExpressionHd = sd.Attribute("filterexpressionhd") != null ? sd.Attribute("filterexpressionhd").Value : "",
                                             PrinterName = sd.Attribute("printername") != null ? sd.Attribute("printername").Value : "",
                                             DataSourceHd = sd.Attribute("datasourcehd") != null ? sd.Attribute("datasourcehd").Value : "",
                                         }).FirstOrDefault();
                isUsingDirectPrint = tempReportSetting.IsUsingDirectPrint;
                if (tempReportSetting.IsUsingDirectPrint)
                {
                    if (((BasePage)Page).OnBeforeDirectPrint(reportMaster, ref errMessage))
                    {
                        #region Report Parameter
                        List<ReportParameter> lstReportParameter = (from sd in xdocReport.Descendants("parameter")
                                                                    select new ReportParameter
                                                                    {
                                                                        Code = sd.Attribute("code").Value,
                                                                        IsShowAsSubTitle = sd.Attribute("isshowassubtitle") != null ? sd.Attribute("isshowassubtitle").Value == "1" : false
                                                                    }).ToList<ReportParameter>();
                        string reportFilterExpression = "";
                        reportFilterExpression = GenerateFilterExpression(lstReportParameter);
                        #endregion

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
                        IEnumerable<XElement> x1 = xdocReport.Descendants("itemtemplate");
                        if (x1.Count() > 0)
                        {
                            string printerName = tempReportSetting.PrinterName;
                            if (!tempReportSetting.IsUsingZPL)
                            {
                                PrintDocument printDocument = new PrintDocument();
                                printDocument.PrinterSettings.PrinterName = printerName;

                                string itemTemplate = x1.Single().Value;
                                itemTemplate = SetTemplateText(itemTemplate, tempReportSetting.DataSourceHd, entityHd);
                                string directPrintTextPrint = GenerateDirectPrintText(itemTemplate, tempReportSetting.TotalLength);

                                printDocument.PrintPage += (sender, args) => ProcessDirectPrint(tempReportSetting.FontFamily, Convert.ToDouble(tempReportSetting.FontSize), directPrintTextPrint, args);

                                printDocument.Print();
                            }
                            else
                            {
                                DirectPrinting print = new DirectPrinting();
                                string itemTemplate = x1.Single().Value;

                                itemTemplate = SetTemplateText(itemTemplate, tempReportSetting.DataSourceHd, entityHd);
                                #region Send Command to Printer
                                //Initialize Printer
                                print.OpenPrinter(printerName);
                                print.StartDocPrinter();
                                print.Send("\n");
                                print.Send("n\n"); // clear the image buffer
                                print.Send(itemTemplate);
                                print.Send("p1\n"); //print one label
                                print.EndDocPrinter();
                                #endregion
                            }
                        }
                        return true;
                    }
                    else
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        private string GenerateDirectPrintText(string templateText, int totalLength)
        {
            string[] temp = templateText.Split('\n');
            StringBuilder strBuild = new StringBuilder();
            foreach (string text in temp)
            {
                string tempResult = text;
                Regex regex = new Regex(@"<center>(.+?)</center>");
                MatchCollection collection = regex.Matches(tempResult);
                foreach (Match m in collection)
                {
                    var columnName = m.Groups[1].Value;
                    tempResult = tempResult.Replace(@"<center>" + columnName + "</center>", CenterString(columnName, totalLength));
                }
                regex = new Regex(@"Pos<(.+?)>");
                collection = regex.Matches(tempResult);
                foreach (Match m in collection)
                {
                    var columnName = m.Groups[1].Value;
                    string[] tempText = columnName.Split(',');

                    tempResult = tempResult.Replace(@"Pos<" + columnName + ">", String.Format("{0," + tempText[1] + "}", tempText[0]));
                    //tempResult = tempResult.Replace(@"Pos<" + columnName + ">", tempText[0].PadRight(Convert.ToInt32(tempText[1])));
                }

                strBuild.AppendLine(tempResult);
            }
            return strBuild.ToString();
        }

        private string CenterString(string stringToCenter, int totalLength)
        {
            return stringToCenter.PadLeft(((totalLength - stringToCenter.Length) / 2)
                                + stringToCenter.Length)
                        .PadRight(totalLength);
        }

        private void ProcessDirectPrint(string fontFamily, double fontSize, string directPrintTextPrint, PrintPageEventArgs e)
        {
            Font printFont = null;
            if (fontFamily != "")
                printFont = new Font(fontFamily, (float)fontSize);
            else
                printFont = new Font("Sans Serif", 8);
            e.Graphics.DrawString(directPrintTextPrint, printFont, Brushes.Black, 10, 10);
        }

        private string SetTemplateText(string templateText, string dataSourceHd, object entityHd)
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
                templateText = templateText.Replace("{" + columnName + "}", Server.HtmlDecode(fieldValue));
            }
            return templateText;
        }

        string[] param = null;
        private string GenerateFilterExpression(List<ReportParameter> lstReportParameter)
        {
            string reportXML = this.ResolveUrl(string.Format("~/Libs/App_Data/report/filterparameter.xml"));
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
                            filterParameter = string.Format("{0} = '{1}'", reportParameter.FieldName, value);
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
            }
            return filterExpression;
        }

        #region ReportParameter
        class ReportParameter
        {
            public string Code { get; set; }
            public bool IsShowAsSubTitle { get; set; }
            public bool IsRequired { get; set; }
        }
        #endregion
    }
}