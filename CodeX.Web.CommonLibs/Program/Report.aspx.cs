using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Data.Model;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Common;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxEditors;
using System.Reflection;
using System.Collections;
using System.Text;
using System.Xml.Linq;
using System.IO;
using DevExpress.Web.ASPxCallbackPanel;
using System.Globalization;

namespace CodeX.CodeX.Web.CommonLibs.Program
{
    public partial class Report : BasePage
    {
        public List<GetUserMenuAccess> ListMenu { get { return lstMenu; } }
        protected List<GetUserMenuAccess> lstMenu = null;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!Page.IsPostBack)
            {
                if (AppSession.UserLogin == null)
                    Response.Redirect("~/../ControlPanel/Login.aspx");

                hdnLoginData.Value = string.Format("{0}|{1}|{2}", AppSession.UserLogin.UserName, "fromprogram", AppSession.UserLogin.SiteID);

                lstMenu = BusinessLayer.GetUserMenuAccess(Constant.Module.REPORT, AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, "IsShowInPullDownMenu = 1 AND IsVisible = 1");
                
                rptMenu.DataSource = lstMenu.OrderBy(p => p.MenuIndex).ToList();
                rptMenu.DataBind();
            }
            string id = Request.Form[hdnReportCode.UniqueID] == null ? "" : Request.Form[hdnReportCode.UniqueID];
            if (id != "")
                BindTreeView();
        }

        protected string GetResolveUrl(string url)
        {
            if (url == "#")
                return "#";
            return ResolveUrl(url);
        }

        private string GetFilterExpressionParameterPage(string value)
        {
            StringBuilder sbResult = new StringBuilder(value);
            sbResult.Replace("@SiteID", AppSession.UserLogin.SiteID);
            sbResult.Replace("@UserID", AppSession.UserLogin.UserID.ToString());
            return sbResult.ToString();
        }

        List<GetReportUserList> lstAllReport = null;
        protected override void OnLoad(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    string moduleID = Request.QueryString["id"];
                    if (moduleID == "DR")
                    {
                        List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.DAILY_REPORT_TYPE));
                        List<GetDailyReportUserList> lstAllDailyReport = BusinessLayer.GetDailyReportUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID);
                        lstAllReport = new List<GetReportUserList>();
                        int ctr = 1;
                        foreach (StandardCode sc in lstStandardCode)
                        {
                            lstAllReport.Add(new GetReportUserList { ReportID = -ctr, ReportName = sc.StandardCodeName, DisplayOrder = (short)ctr, IsHeader = true, ParentID = null });
                            List<GetDailyReportUserList> lstAllDailyReport1 = lstAllDailyReport.Where(p => p.GCDailyReportType == sc.StandardCodeID).ToList();
                            foreach (GetDailyReportUserList rpt in lstAllDailyReport1)
                            {
                                lstAllReport.Add(new GetReportUserList { ReportID = rpt.ReportID, ReportCode = rpt.ReportCode, ReportName = rpt.ReportName, DisplayOrder = rpt.DisplayOrder, IsHeader = false, ParentID = -ctr });
                            }
                            ctr++;
                        }
                        List<GetReportUserList> lstReportParent = lstAllReport.Where(p => p.ParentID == null).OrderBy(p => p.DisplayOrder).ToList();
                        PopulateNodes(lstReportParent, tvwView.Nodes);
                    }
                    else
                    {
                        lstAllReport = BusinessLayer.GetReportUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.ReportType.REPORT, moduleID, "", "");
                        List<GetReportUserList> lstReportParent = lstAllReport.Where(p => p.ParentID == null).OrderBy(p => p.DisplayOrder).ToList();
                        PopulateNodes(lstReportParent, tvwView.Nodes);
                    }
                }
            }
        }

        private void PopulateNodes(List<GetReportUserList> lstReport, TreeNodeCollection nodes)
        {
            foreach (GetReportUserList report in lstReport)
            {
                Int32 childCount = lstAllReport.Where(p => p.ParentID == report.ReportID).Count();
                TreeNode tn = new TreeNode();
                tn.Text = report.ReportName;
                tn.Value = report.ReportID.ToString();
                if (childCount > 0)
                    tn.SelectAction = TreeNodeSelectAction.Expand;
                else
                    tn.NavigateUrl = string.Format("{0}|{1}", report.ReportID, report.ReportCode);
                nodes.Add(tn);

                tn.PopulateOnDemand = (childCount > 0);
            }
        }

        private void PopulateSubLevel(Int32 parentID, TreeNode parentNode)
        {
            PopulateNodes(lstAllReport.Where(p => p.ParentID == parentID).OrderBy(p => p.DisplayOrder).ToList(), parentNode.ChildNodes);
        }

        protected void tvwView_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            PopulateSubLevel(Convert.ToInt32(e.Node.Value), e.Node);
        }

        private void BindTreeView()
        {
            ReportMaster reportMaster = BusinessLayer.GetReportMasterList(string.Format("ReportCode = '{0}'", Request.Form[hdnReportCode.UniqueID])).FirstOrDefault();
            string reportXML = this.ResolveUrl(string.Format("~/Libs/App_Data/report/{0}/{1}.xml", AppConfigManager.CDXAppClientID, reportMaster.ReportUrl));
            string physicalPath = HttpContext.Current.Request.MapPath(reportXML);
            if (!File.Exists(physicalPath))
            {
                reportXML = this.ResolveUrl(string.Format("~/Libs/App_Data/report/general/{0}.xml", reportMaster.ReportUrl));
                physicalPath = HttpContext.Current.Request.MapPath(reportXML);
                if (!File.Exists(physicalPath))
                    return;
            }

            XDocument xdocReport = XDocument.Load(physicalPath);
            List<ReportParameterPage> lstReportParameter = (from sd in xdocReport.Descendants("parameter")
                                                            select new ReportParameterPage
                                                            {
                                                                FilterParameterCode = sd.Attribute("code").Value,
                                                                IsRequired = sd.Attribute("isrequired") != null ? sd.Attribute("isrequired").Value == "1" : false,
                                                            }).ToList<ReportParameterPage>();

            var tempReportSetting = (from sd in xdocReport.Descendants("table")
                                     select new
                                     {
                                         IsAllowExportExcel = sd.Attribute("isallowexportexcel") != null ? sd.Attribute("isallowexportexcel").Value == "1" : false,
                                         IsAllowPrint = sd.Attribute("isallowprint") != null ? sd.Attribute("isallowprint").Value == "1" : true
                                     }).FirstOrDefault();

            hdnIsAllowExportExcel.Value = tempReportSetting.IsAllowExportExcel ? "1" : "0";
            hdnIsAllowPrint.Value = tempReportSetting.IsAllowPrint ? "1" : "0";

            reportXML = this.ResolveUrl(string.Format("~/Libs/App_Data/report/filterparameter.xml", reportMaster.ReportUrl));
            physicalPath = HttpContext.Current.Request.MapPath(reportXML);
            if (!File.Exists(physicalPath))
                return;
            XDocument xdocFilterParameter = XDocument.Load(physicalPath);
            string filterExpression = String.Empty;

            for (int i = 0; i < lstReportParameter.Count; ++i)
            {
                ReportParameterPage reportParameter = lstReportParameter[i];
                var temp = (from sd in xdocFilterParameter.Descendants("filterparameter").Where(p => p.Attribute("code").Value == lstReportParameter[i].FilterParameterCode)
                            select new
                            {
                                FilterParameterName = sd.Attribute("name").Value,
                                FilterParameterCaption = sd.Attribute("caption").Value,
                                GCFilterParameterType = sd.Parent.Attribute("type").Value,
                                FieldName = sd.Attribute("fieldname") != null ? sd.Attribute("fieldname").Value : "",
                                DefaultValue = sd.Attribute("defaultvalue") != null ? sd.Attribute("defaultvalue").Value : "",

                                TxtCssClass = sd.Attribute("txtcssclass") != null ? sd.Attribute("txtcssclass").Value : "",

                                MethodName = sd.Attribute("methodname") != null ? sd.Attribute("methodname").Value : "",
                                TextFieldName = sd.Attribute("textfieldname") != null ? sd.Attribute("textfieldname").Value : "",
                                ValueFieldName = sd.Attribute("valuefieldname") != null ? sd.Attribute("valuefieldname").Value : "",
                                FilterExpression = sd.Attribute("filterexpression") != null ? sd.Attribute("filterexpression").Value : "",
                                ClientInstanceName = sd.Attribute("clientinstancename") != null ? sd.Attribute("clientinstancename").Value : "",

                                SearchDialogIDField = sd.Attribute("sdidfield") != null ? sd.Attribute("sdidfield").Value : "",
                                SearchDialogCodeField = sd.Attribute("sdcodefield") != null ? sd.Attribute("sdcodefield").Value : "",
                                SearchDialogNameField = sd.Attribute("sdnamefield") != null ? sd.Attribute("sdnamefield").Value : "",
                                SearchDialogMethodName = sd.Attribute("sdmethodname") != null ? sd.Attribute("sdmethodname").Value : "",
                                SearchDialogType = sd.Attribute("sdtype") != null ? sd.Attribute("sdtype").Value : "",
                                SearchDialogFilterExpression = sd.Attribute("sdfilterexpression") != null ? sd.Attribute("sdfilterexpression").Value : "",

                                ListText = sd.Attribute("listtext") != null ? sd.Attribute("listtext").Value : "",
                                ListValue = sd.Attribute("listvalue") != null ? sd.Attribute("listvalue").Value : "",

                                YearMinusNYear = sd.Attribute("yearminusnyear") != null ? Convert.ToInt32(sd.Attribute("yearminusnyear").Value) : 0,
                                YearPlusNYear = sd.Attribute("yearplusnyear") != null ? Convert.ToInt32(sd.Attribute("yearplusnyear").Value) : 0,
                            }).FirstOrDefault();
                reportParameter.FilterParameterCaption = temp.FilterParameterCaption;
                reportParameter.GCFilterParameterType = temp.GCFilterParameterType;
                reportParameter.DefaultValue = temp.DefaultValue;
                reportParameter.FieldName = temp.FieldName;

                reportParameter.TxtCssClass = temp.TxtCssClass;

                reportParameter.MethodName = temp.MethodName;
                reportParameter.TextFieldName = temp.TextFieldName;
                reportParameter.ValueFieldName = temp.ValueFieldName;
                reportParameter.FilterExpression = temp.FilterExpression;
                reportParameter.ClientInstanceName = temp.ClientInstanceName;

                reportParameter.SearchDialogIDField = temp.SearchDialogIDField;
                reportParameter.SearchDialogCodeField = temp.SearchDialogCodeField;
                reportParameter.SearchDialogNameField = temp.SearchDialogNameField;
                reportParameter.SearchDialogType = temp.SearchDialogType;
                reportParameter.SearchDialogFilterExpression = temp.SearchDialogFilterExpression;
                reportParameter.SearchDialogMethodName = temp.SearchDialogMethodName;

                reportParameter.ListText = temp.ListText;
                reportParameter.ListValue = temp.ListValue;

                reportParameter.YearMinusNYear = temp.YearMinusNYear;
                reportParameter.YearPlusNYear = temp.YearPlusNYear;
            }

            rptReportParameter.DataSource = lstReportParameter;
            rptReportParameter.DataBind();
        }

        #region ReportParameter
        class ReportParameterPage
        {
            public string FilterParameterCode { get; set; }
            public string FilterParameterCaption { get; set; }
            public bool IsRequired { get; set; }
            public string GCFilterParameterType { get; set; }
            public string DefaultValue { get; set; }
            public string FieldName { get; set; }

            public string TxtCssClass { get; set; }

            public string MethodName { get; set; }
            public string TextFieldName { get; set; }
            public string ValueFieldName { get; set; }
            public string FilterExpression { get; set; }
            public string ClientInstanceName { get; set; }

            public string SearchDialogIDField { get; set; }
            public string SearchDialogCodeField { get; set; }
            public string SearchDialogNameField { get; set; }
            public string SearchDialogType { get; set; }
            public string SearchDialogFilterExpression { get; set; }
            public string SearchDialogMethodName { get; set; }

            public string ListText { get; set; }
            public string ListValue { get; set; }

            public int YearMinusNYear { get; set; }
            public int YearPlusNYear { get; set; }
        }
        #endregion

        protected void cbpReportParameter_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindTreeView();
        }

        protected void rptReportParameter_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ReportParameterPage entity = (ReportParameterPage)e.Item.DataItem;
                entity.FilterExpression = GetFilterExpressionParameterPage(entity.FilterExpression);
                HtmlGenericControl div = null;
                if (entity.GCFilterParameterType == Constant.FilterParameterType.CONSTANT)
                {
                    HtmlTableRow trReportParameter = (HtmlTableRow)e.Item.FindControl("trReportParameter");
                    TextBox txtValue = (TextBox)e.Item.FindControl("txtValue");
                    txtValue.Text = entity.DefaultValue;
                    trReportParameter.Style.Add("display", "none");
                }
                else if (entity.GCFilterParameterType == Constant.FilterParameterType.TEXT_BOX)
                {
                    div = (HtmlGenericControl)e.Item.FindControl("divTxt");
                    HtmlGenericControl lbl = (HtmlGenericControl)e.Item.FindControl("lblColumn");
                    lbl.Attributes.Add("class", "lblMandatory");
                    TextBox txtValue = (TextBox)e.Item.FindControl("txtValue");
                    txtValue.Text = entity.DefaultValue;
                    if (entity.TxtCssClass != "")
                        txtValue.CssClass = entity.TxtCssClass;
                    Helper.SetControlEntrySetting(txtValue, new ControlEntrySetting(true, true, true), "mpReport");
                }
                else if (entity.GCFilterParameterType == Constant.FilterParameterType.SINGLE_DATE)
                {
                    div = (HtmlGenericControl)e.Item.FindControl("divDte");
                    HtmlGenericControl lbl = (HtmlGenericControl)e.Item.FindControl("lblColumn");
                    lbl.Attributes.Add("class", "lblMandatory");
                    TextBox txtDteValue = (TextBox)e.Item.FindControl("txtDteValue");
                    if (entity.DefaultValue == "@DateNow")
                        txtDteValue.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                    else
                        txtDteValue.Text = entity.DefaultValue;
                    Helper.SetControlEntrySetting(txtDteValue, new ControlEntrySetting(true, true, true), "mpReport");
                }
                else if (entity.GCFilterParameterType == Constant.FilterParameterType.SEARCH_DIALOG)
                {
                    div = (HtmlGenericControl)e.Item.FindControl("divSd");
                    HtmlGenericControl lbl = (HtmlGenericControl)e.Item.FindControl("lblColumn");

                    HtmlInputHidden hdnSdNewID = (HtmlInputHidden)e.Item.FindControl("hdnSdNewID");
                    HtmlInputHidden hdnSearchDialogFilterExpression = (HtmlInputHidden)e.Item.FindControl("hdnSearchDialogFilterExpression");
                    TextBox txtSdNewCode = (TextBox)e.Item.FindControl("txtSdNewCode");
                    TextBox txtSdNewText = (TextBox)e.Item.FindControl("txtSdNewText");

                    StringBuilder sbFilterExpression = new StringBuilder();
                    sbFilterExpression.Append(entity.SearchDialogFilterExpression).Replace("@SiteID", AppSession.UserLogin.SiteID).Replace("@UserID", AppSession.UserLogin.UserID.ToString());
                    hdnSearchDialogFilterExpression.Value = sbFilterExpression.ToString();

                    if (entity.SearchDialogCodeField == entity.SearchDialogNameField)
                        txtSdNewText.Visible = false;
                    if (!entity.IsRequired)
                        lbl.Attributes.Add("class", "lblLink lblReport");
                    else
                        lbl.Attributes.Add("class", "lblLink lblReport lblMandatory");
                    Helper.SetControlEntrySetting(txtSdNewCode, new ControlEntrySetting(true, true, entity.IsRequired), "mpReport");
                }
                else if (entity.GCFilterParameterType == Constant.FilterParameterType.COMBO_BOX || entity.GCFilterParameterType == Constant.FilterParameterType.CUSTOM_COMBO_BOX || entity.GCFilterParameterType == Constant.FilterParameterType.YEAR_COMBO_BOX)
                {
                    div = (HtmlGenericControl)e.Item.FindControl("divCbo");
                    ASPxComboBox cboValue = (ASPxComboBox)e.Item.FindControl("cboValue");

                    if (entity.GCFilterParameterType == Constant.FilterParameterType.COMBO_BOX)
                    {
                        MethodInfo method = typeof(BusinessLayer).GetMethod(entity.MethodName, new[] { typeof(string) });
                        object obj = method.Invoke(null, new string[] { entity.FilterExpression });
                        IList list = (IList)obj;

                        cboValue.DataSource = list;
                        cboValue.TextField = entity.TextFieldName;
                        cboValue.ValueField = entity.ValueFieldName;
                        cboValue.CallbackPageSize = 50;
                        cboValue.EnableCallbackMode = false;
                        cboValue.ClientInstanceName = entity.ClientInstanceName;
                        cboValue.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                        cboValue.DropDownStyle = DropDownStyle.DropDownList;
                        cboValue.DataBind();
                    }
                    else if (entity.GCFilterParameterType == Constant.FilterParameterType.CUSTOM_COMBO_BOX)
                    {
                        string[] lstText = entity.ListText.Split('|');
                        string[] lstValue = entity.ListValue.Split('|');
                        for (int i = 0; i < lstText.Length; ++i)
                            cboValue.Items.Add(new ListEditItem { Value = lstValue[i], Text = lstText[i] });
                        cboValue.CallbackPageSize = 50;
                        cboValue.EnableCallbackMode = false;
                        cboValue.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                        cboValue.DropDownStyle = DropDownStyle.DropDownList;
                    }
                    else
                    {
                        cboValue.DataSource = Enumerable.Range(DateTime.Now.Year - entity.YearMinusNYear, entity.YearPlusNYear + entity.YearMinusNYear + 1).Reverse();
                        cboValue.EnableCallbackMode = false;
                        cboValue.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                        cboValue.DropDownStyle = DropDownStyle.DropDownList;
                        cboValue.DataBind();
                    }
                    if (!entity.IsRequired)
                        cboValue.Items.Insert(0, new ListEditItem { Value = "", Text = "" });

                    cboValue.SelectedIndex = 0;
                    Helper.SetControlEntrySetting(cboValue, new ControlEntrySetting(true, true, entity.IsRequired), "mpReport");
                }
                else
                {
                    div = (HtmlGenericControl)e.Item.FindControl("divCbo");
                    TextBox txtValueDateFrom = (TextBox)e.Item.FindControl("txtValueDateFrom");
                    TextBox txtValueDateTo = (TextBox)e.Item.FindControl("txtValueDateTo");
                    TextBox txtValueNum = (TextBox)e.Item.FindControl("txtValueNum");

                    txtValueDateTo.Text = txtValueDateFrom.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                    Helper.SetControlEntrySetting(txtValueDateFrom, new ControlEntrySetting(true, true, true), "mpReport");
                    Helper.SetControlEntrySetting(txtValueDateTo, new ControlEntrySetting(true, true, true), "mpReport");
                    Helper.SetControlEntrySetting(txtValueNum, new ControlEntrySetting(true, true, true), "mpReport");

                    string filterExpression = "";
                    switch (entity.GCFilterParameterType)
                    {
                        case Constant.FilterParameterType.PAST_PERIOD: filterExpression = string.Format("ParentID = '{0}' AND IsActive = 1 AND StandardCodeID NOT BETWEEN '{0}^050' AND '{0}^060' AND IsDeleted = 0", Constant.StandardCode.REPORTING_PERIOD); break;
                        case Constant.FilterParameterType.UPCOMING_PERIOD: filterExpression = string.Format("ParentID = '{0}' AND IsActive = 1 AND StandardCodeID NOT BETWEEN '{0}^010' AND '{0}^020' AND IsDeleted = 0", Constant.StandardCode.REPORTING_PERIOD); break;
                        case Constant.FilterParameterType.DATE: filterExpression = string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.REPORTING_PERIOD); break;
                    }
                    ASPxComboBox cboValue = (ASPxComboBox)e.Item.FindControl("cboValue");
                    List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(filterExpression);
                    Methods.SetComboBoxField<StandardCode>(cboValue, lstStandardCode, "StandardCodeName", "StandardCodeID");
                    cboValue.SelectedIndex = 0;

                    HtmlInputHidden hdnYear = (HtmlInputHidden)e.Item.FindControl("hdnYear");
                    hdnYear.Value = DateTime.Now.Year.ToString();

                    ASPxComboBox cboMonth = (ASPxComboBox)e.Item.FindControl("cboMonth");
                    cboMonth.DataSource = Enumerable.Range(1, 12).Select(a => new
                    {
                        MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(a),
                        MonthNumber = a
                    });
                    cboMonth.TextField = "MonthName";
                    cboMonth.ValueField = "MonthNumber";
                    cboMonth.EnableCallbackMode = false;
                    cboMonth.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    cboMonth.DropDownStyle = DropDownStyle.DropDownList;
                    cboMonth.DataBind();
                    cboMonth.Items.Insert(0, new ListEditItem { Value = "", Text = "" });
                    cboMonth.Value = DateTime.Now.Month.ToString();
                }
                if (div != null)
                    div.Visible = true;
            }
        }

        protected void cbpReportProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string param = "";
            string errMessage = "";
            string result = "";
            if (OnProcessReport(ref param, ref errMessage))
                result = "success";
            else
                result = "fail|" + errMessage;

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpParam"] = param;
            panel.JSProperties["cpResult"] = result;
        }

        private bool OnProcessReport(ref string param, ref string errMessage)
        {
            try
            {
                int ctr = 0;
                foreach (RepeaterItem itemDt in rptReportParameter.Items)
                {
                    if (ctr > 0)
                        param += "|";
                    HtmlInputHidden hdnGCFilterParameterType = (HtmlInputHidden)itemDt.FindControl("hdnGCFilterParameterType");
                    if (hdnGCFilterParameterType.Value == Constant.FilterParameterType.TEXT_BOX || hdnGCFilterParameterType.Value == Constant.FilterParameterType.CONSTANT)
                    {
                        TextBox txtValue = (TextBox)itemDt.FindControl("txtValue");
                        param += string.Format("{0};{0}", txtValue.Text);
                    }
                    else if (hdnGCFilterParameterType.Value == Constant.FilterParameterType.SINGLE_DATE)
                    {
                        TextBox txtDteValue = (TextBox)itemDt.FindControl("txtDteValue");
                        DateTime date = Helper.GetDatePickerValue(txtDteValue.Text);
                        param += string.Format("{0};{1}", date.ToString("yyyyMMdd"), date.ToString("dd-MMM-yyyy"));
                    }
                    else if (hdnGCFilterParameterType.Value == Constant.FilterParameterType.SEARCH_DIALOG)
                    {
                        HtmlInputHidden hdnSdNewID = (HtmlInputHidden)itemDt.FindControl("hdnSdNewID");
                        TextBox txtSdNewText = (TextBox)itemDt.FindControl("txtSdNewText");
                        param += string.Format("{0};{1}", hdnSdNewID.Value, Request.Form[txtSdNewText.UniqueID]);
                    }
                    else if (hdnGCFilterParameterType.Value == Constant.FilterParameterType.COMBO_BOX || hdnGCFilterParameterType.Value == Constant.FilterParameterType.CUSTOM_COMBO_BOX || hdnGCFilterParameterType.Value == Constant.FilterParameterType.YEAR_COMBO_BOX)
                    {
                        ASPxComboBox cboValue = (ASPxComboBox)itemDt.FindControl("cboValue");
                        if (cboValue.Value != null && cboValue.Value.ToString() != "")
                            param += string.Format("{0};{1}", cboValue.Value, cboValue.Text);
                    }
                    else
                    {
                        ASPxComboBox cboMonth = (ASPxComboBox)itemDt.FindControl("cboMonth");
                        HtmlInputHidden hdnYear = (HtmlInputHidden)itemDt.FindControl("hdnYear");
                        ASPxComboBox cboValue = (ASPxComboBox)itemDt.FindControl("cboValue");
                        TextBox txtValueNum = (TextBox)itemDt.FindControl("txtValueNum");
                        TextBox txtValueDateFrom = (TextBox)itemDt.FindControl("txtValueDateFrom");
                        TextBox txtValueDateTo = (TextBox)itemDt.FindControl("txtValueDateTo");
                        DateTime startDate = DateTime.Today;
                        DateTime endDate = DateTime.Today;
                        int num = Convert.ToInt32(txtValueNum.Text);
                        switch (cboValue.Value.ToString())
                        {
                            //Month
                            case "X106^089": startDate = new DateTime(Convert.ToInt32(hdnYear.Value), Convert.ToInt32(cboMonth.Value), 1);
                                endDate = startDate.AddMonths(1).AddDays(-1); break;

                            //Custom
                            case "X106^090": startDate = Helper.GetDatePickerValue(txtValueDateFrom.Text);
                                endDate = Helper.GetDatePickerValue(txtValueDateTo.Text); break;
                            //Last Month
                            case "X106^015": 
                                //startDate = DateTime.Today.AddDays(-DateTime.Today.Day).AddMonths(-1);
                                //endDate = startDate.AddMonths(1).AddDays(-1); break;
                                startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
                                endDate = startDate.AddMonths(1).AddDays(-1); break;
                            //Yesterday
                            case "X106^017": startDate = DateTime.Today.AddDays(-1);
                                endDate = DateTime.Today.AddDays(-1); break;

                            //Next Month
                            case "X106^055": 
                                //startDate = DateTime.Today.AddMonths(1).AddDays(-DateTime.Today.Day);
                                //endDate = startDate.AddMonths(1).AddDays(-1); break;
                                startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1);
                                endDate = startDate.AddMonths(1).AddDays(-1); break;

                            //Tomorrow
                            case "X106^057": startDate = DateTime.Today.AddDays(1);
                                endDate = DateTime.Today.AddDays(1); break;
                        }
                        param += string.Format("{0}{1};{2} s/d {3}", startDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd"), startDate.ToString("dd-MMM-yyyy"), endDate.ToString("dd-MMM-yyyy"));
                    }
                    ctr++;
                }
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string param = "";
            string errMessage = "";
            if (OnProcessReport(ref param, ref errMessage))
            {
                string reportName = "";
                string exportExcelValue = ExcelReport.GenerateExcelFile(Server, this, ref reportName, param, Request.Form[hdnReportCode.UniqueID]);


                string attachment = string.Format("attachment;filename=\"{0}.xls\"", reportName);
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.AddHeader("content-disposition", attachment);
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                //StringWriter stw = new StringWriter();
                //HtmlTextWriter htextw = new HtmlTextWriter(stw);
                //divExportExcel.RenderControl(htextw);
                //HttpContext.Current.Response.Write(stw.ToString());
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                //HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                //HttpContext.Current.Response.Write("<html><head></head>" + exportExcelValue + "</html>");
                HttpContext.Current.Response.Write(exportExcelValue);
                //stw = null;
                //htextw = null;
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
    }
}