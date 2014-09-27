using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using CodeX.Data.Model;
using System.Text;
using System.Reflection;
using CodeX.Common;
using CodeX.Common;

namespace CodeX.Report
{
    public partial class BaseDailyPortraitRpt : BaseRpt
    {
        private string GetFilterExpression(string value)
        {
            StringBuilder sbResult = new StringBuilder(value);
            sbResult.Replace("@SiteID", appSession.SiteID);
            
            return sbResult.ToString();
        }

        public BaseDailyPortraitRpt()
        {
            InitializeComponent();
        }

        public override void InitializeReport(string[] param)
        {
            lblReportTitle.Text = reportMaster.ReportTitle1;
            lblReportSubtitle.Text = reportMaster.ReportTitle2;
            lblReportProperties.Text = string.Format("OTTIMO - {0}, Print Date/Time:{1}, User ID:{2}", reportMaster.ReportCode, DateTime.Now.ToString("dd-MMM-yyyy/HH:mm:ss"), appSession.UserName);

            //Show or Hide Header
            xrLogo.Visible = reportMaster.IsShowHeader;
            lblSiteName.Visible = reportMaster.IsShowHeader;
            lblAddressLine1.Visible = reportMaster.IsShowHeader;
            lblAddressLine2.Visible = reportMaster.IsShowHeader;
            lblPhoneFaxNo.Visible = reportMaster.IsShowHeader;
            xrLogo.ImageUrl = ResolveUrl("~/ControlPanel/Libs/Images/logo.png");

            //Set Top Margin
            TopMargin.HeightF = TopMargin.HeightF + reportMaster.TopMargin;

            //Load Site Information
            if (reportMaster.IsShowHeader)
            {
                vSite oSite = BusinessLayer.GetvSiteList(string.Format("SiteID = '{0}'", appSession.SiteID))[0];
                if (oSite != null)
                {
                    lblSiteName.Text = oSite.SiteName;
                    lblAddressLine1.Text = oSite.StreetName;
                    lblAddressLine2.Text = oSite.AddressLine2;
                    lblPhoneFaxNo.Text = string.Format("Phone/Fax : {0}", string.IsNullOrEmpty(oSite.FaxNo1) ? oSite.PhoneNo1 : string.Format("{0}/{1}", oSite.PhoneNo1, oSite.FaxNo1));
                }
            }
            if (reportMaster.GCDataSourceType == Constant.DataSourceType.VIEW)
                BindingView(reportMaster, param);
            else
                BindingStoredProcedure(reportMaster, param);

            //Show or Hide Parameter
            this.lblReportParameterTitle.Visible = reportMaster.IsShowParameter;
            this.lblParameter0.Visible = this.lblReportParameterTitle.Visible;
            this.lblParameter1.Visible = this.lblReportParameterTitle.Visible;
            this.lblParameter2.Visible = this.lblReportParameterTitle.Visible;
            this.lblParameter3.Visible = this.lblReportParameterTitle.Visible;
            this.lblParameter4.Visible = this.lblReportParameterTitle.Visible;
            this.lblParameter5.Visible = this.lblReportParameterTitle.Visible;
            this.lblParameter6.Visible = this.lblReportParameterTitle.Visible;
            this.lblParameter7.Visible = this.lblReportParameterTitle.Visible;
        }

        private void BindingView(ReportMaster reportMaster, string[] param)
        {
            List<vReportParameter> listReportParameter = BusinessLayer.GetvReportParameterList(string.Format("ReportID = {0} ORDER BY DisplayOrder", reportMaster.ReportID));
            string filterExpression = String.Empty;
            for (int i = 0; i < listReportParameter.Count; ++i)
            {
                string filterParameter = String.Empty;
                vReportParameter reportParameter = listReportParameter[i];
                if (reportParameter.GCFilterParameterType == Constant.FilterParameterType.FREE_TEXT)
                {
                    if (i > 0 && filterExpression != "")
                        filterExpression += " AND ";
                    filterParameter += param[i];
                    filterExpression += filterParameter;
                }
                else
                {
                    if (reportParameter.GCFilterParameterType == Constant.FilterParameterType.DATE ||
                        reportParameter.GCFilterParameterType == Constant.FilterParameterType.PAST_PERIOD ||
                        reportParameter.GCFilterParameterType == Constant.FilterParameterType.UPCOMING_PERIOD)
                    {
                        if (i > 0 && filterExpression != "")
                            filterExpression += " AND ";
                        string[] date = param[i].Split(';');
                        string startDate = date[0];
                        string endDate = date[1];
                        filterParameter = string.Format("{0} BETWEEN '{1}' AND '{2}'", reportParameter.FieldName, startDate, endDate);
                        filterExpression += filterParameter;
                    }
                    else if (reportParameter.GCFilterParameterType == Constant.FilterParameterType.SINGLE_DATE)
                    {
                        string[] paramSplit = param[i].Split(';');
                        string value = paramSplit[0];
                        if (i > 0 && filterExpression != "")
                            filterExpression += " AND ";
                        filterParameter = string.Format("{0} = '{1}'", reportParameter.FieldName, value);
                        filterExpression += filterParameter;
                    }
                    else if (reportParameter.GCFilterParameterType == Constant.FilterParameterType.COMBO_BOX || reportParameter.GCFilterParameterType == Constant.FilterParameterType.YEAR_COMBO_BOX || reportParameter.GCFilterParameterType == Constant.FilterParameterType.CUSTOM_COMBO_BOX || reportParameter.GCFilterParameterType == Constant.FilterParameterType.SEARCH_DIALOG)
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
            SetReportParameterText(listReportParameter, param);
            string additionalFilterExpression = GetFilterExpression(reportMaster.AdditionalFilterExpression);
            if (filterExpression != "" && additionalFilterExpression != "")
                filterExpression += " AND ";
            filterExpression += additionalFilterExpression;

            MethodInfo method = typeof(BusinessLayer).GetMethod(reportMaster.ObjectTypeName, new[] { typeof(string) });
            object obj = method.Invoke(null, new string[] { filterExpression });
            this.DataSource = obj;
        }

        private void BindingStoredProcedure(ReportMaster reportMaster, string[] param)
        {
            List<vReportParameter> listReportParameter = BusinessLayer.GetvReportParameterList(string.Format("ReportID = {0} ORDER BY DisplayOrder", reportMaster.ReportID));
            string filterExpression = String.Empty;
            List<Variable> lstVariable = new List<Variable>();
            for (int i = 0; i < listReportParameter.Count; ++i)
            {
                string value = param[i];
                vReportParameter reportParameter = listReportParameter[i];
                lstVariable.Add(new Variable { Code = reportParameter.FieldName, Value = value });
            }
            SetReportParameterText(listReportParameter, param);
            this.DataSource = BusinessLayer.GetDataReport(reportMaster.ObjectTypeName, lstVariable);
        }

        private void SetReportParameterText(List<vReportParameter> listReportParameter, string[] param)
        {
            if (reportMaster.IsShowParameter)
            {
                for (int i = 0; i < listReportParameter.Count; ++i)
                {
                    string value = param[i];
                    vReportParameter reportParameter = listReportParameter[i];
                    string parameterText = string.Format("{0} : ", reportParameter.FilterParameterCaption);
                    if (reportParameter.GCFilterParameterType == Constant.FilterParameterType.FREE_TEXT) { }
                    else if (reportParameter.GCFilterParameterType == Constant.FilterParameterType.TEXT_BOX ||
                        reportParameter.GCFilterParameterType == Constant.FilterParameterType.CONSTANT ||
                        reportParameter.GCFilterParameterType == Constant.FilterParameterType.YEAR_COMBO_BOX)
                        parameterText += value;
                    else if (reportParameter.GCFilterParameterType == Constant.FilterParameterType.SINGLE_DATE)
                        parameterText += Helper.YYYYMMDDToDate(value).ToString(Constant.FormatString.DATE_FORMAT);
                    else if (reportParameter.GCFilterParameterType == Constant.FilterParameterType.SEARCH_DIALOG)
                    {
                        MethodInfo method = typeof(BusinessLayer).GetMethod(reportParameter.SearchDialogMethodName, new[] { typeof(string) });
                        object tempObj = method.Invoke(null, new string[] { string.Format("{0} = {1}", reportParameter.SearchDialogIDField, value) });
                        IList list = (IList)tempObj;
                        if (list.Count > 0)
                        {
                            object obj = list[0];
                            parameterText += obj.GetType().GetProperty(reportParameter.SearchDialogNameField).GetValue(obj, null);
                        }
                    }
                    else if (reportParameter.GCFilterParameterType == Constant.FilterParameterType.COMBO_BOX)
                    {
                        MethodInfo method = typeof(BusinessLayer).GetMethod(reportParameter.MethodName, new[] { typeof(string) });
                        object tempObj = method.Invoke(null, new string[] { string.Format("{0} = {1}", reportParameter.ValueFieldName, value) });
                        IList list = (IList)tempObj;
                        if (list.Count > 0)
                        {
                            object obj = list[0];
                            parameterText += obj.GetType().GetProperty(reportParameter.TextFieldName).GetValue(obj, null);
                        }
                    }
                    else if (reportParameter.GCFilterParameterType == Constant.FilterParameterType.CUSTOM_COMBO_BOX)
                    {
                        string[] lstText = reportParameter.ListText.Split('|');
                        string[] lstValue = reportParameter.ListValue.Split('|');
                        for (int j = 0; j < lstValue.Length; ++j)
                        {
                            if (lstValue[j] == value)
                                parameterText += lstText[j];
                        }
                    }
                    else
                    {
                        string[] temp = value.Split(';');
                        parameterText += string.Format("{0} - {1}", Helper.YYYYMMDDToDate(temp[0]).ToString(Constant.FormatString.DATE_FORMAT), Helper.YYYYMMDDToDate(temp[1]).ToString(Constant.FormatString.DATE_FORMAT));
                    }
                    FormatReportParameter(i.ToString(), parameterText);
                }
            }
        }

        private void FormatReportParameter(string parameterNo, string filterParameter)
        {
            XRControl lblParameter = this.ReportFooter.FindControl(string.Format("lblParameter{0}", parameterNo), true);
            if (lblParameter != null)
            {
                lblParameter.Visible = true;
                lblParameter.Text = filterParameter;
            }
        }
    }
}
