using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace CodeX.Web.CustomControl
{
    public class CodeXAutoCompleteTextBox : WebControl
    {
        HtmlGenericControl divAutoComplete = new HtmlGenericControl("div");
        HtmlInputText txtAutoComplete = new HtmlInputText();
        public String Text
        {
            get { return txtAutoComplete.Value; }
            set { txtAutoComplete.Value = value; }
        }

        CodeXAutoCompleteTextBoxClientSideEvent _ClientSideEvents = new CodeXAutoCompleteTextBoxClientSideEvent();
        [DefaultValue("")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("Styles")]
        [Description("DataControlField_HeaderStyle")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CodeXAutoCompleteTextBoxClientSideEvent ClientSideEvents
        {
            get { return _ClientSideEvents; }
            set { _ClientSideEvents = value; }
        }

        private CodeXAutoCompleteTextBoxFilterType _FilterType = CodeXAutoCompleteTextBoxFilterType.Contains;
        public CodeXAutoCompleteTextBoxFilterType FilterType
        {
            get { return _FilterType; }
            set { _FilterType = value; }
        }

        private bool _IsRequired = false;
        public bool IsRequired
        {
            get { return _IsRequired; }
            set { _IsRequired = value; }
        }

        private bool _Readonly = false;
        public bool Readonly
        {
            get { return _Readonly; }
            set { _Readonly = value; }
        }
        private string _Value = "";
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        private string _ValidationGroup = "";
        public string ValidationGroup
        {
            get { return _ValidationGroup; }
            set { _ValidationGroup = value; }
        }
        public string ValueField { get; set; }
        public string TextField { get; set; }
        public string SearchText { get; set; }
        public string ClientInstanceName { get; set; }
        public string MethodName { get; set; }
        public string FilterExpression { get; set; }
        public string GetFilterExpressionFunction { get; set; }
        public string SearchFields { get; set; }
        public string OrderByExpression { get; set; }

        private Unit TempWidth;
        protected override void OnInit(EventArgs e)
        {
            _IsRequired = GetHtmlInputHiddenValue("hdnIsRequired") == "1";
            _ValidationGroup = GetHtmlInputHiddenValue("hdnValidationGroup");
            _Value = GetHtmlInputHiddenValue("hdnAutoCompleteValue");

            TempWidth = new Unit(string.Format("{0}px", Width.Value));
            Width = new Unit(string.Format("{0}px", Width.Value + 50));

            base.OnInit(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            #region Create Control
            System.Web.UI.Control parent;
            System.Web.UI.Control container;

            // Get a reference to the ScriptManager object for the page
            // if one exists.
            ScriptManager sm = ScriptManager.GetCurrent(Page);


            if (sm == null || !sm.EnablePartialRendering)
            {
                // If partial rendering is not enabled, set the parent
                // and container as a basic control.
                container = new System.Web.UI.Control();
                parent = container;
            }
            else
            {
                // If partial rendering is enabled, set the parent as
                // a new UpdatePanel object and the container to the
                // content template of the UpdatePanel object.
                UpdatePanel up = new UpdatePanel();
                container = up.ContentTemplateContainer;
                parent = up;
            }

            HtmlGenericControl divContainerAutoComplete = CreateDiv("containerAutoComplete");
            divAutoComplete.Controls.Add(divContainerAutoComplete);

            WebControl templateScript = new WebControl(HtmlTextWriterTag.Script);
            divAutoComplete.Controls.Add(templateScript);
            templateScript.Attributes["class"] = "tmpltAutoComplete";
            templateScript.Attributes["type"] = "text/x-jquery-tmpl";

            String templateScriptContent = "<div>";
            templateScriptContent += SearchText;
            templateScriptContent += "<input type='hidden' value='${" + TextField + "}' class='hdnAutoCompleteRowText'/>";
            templateScriptContent += "<input type='hidden' value='${" + ValueField + "}' class='hdnAutoCompleteRowValue'/>";
            templateScriptContent += "</div>";
            templateScript.Controls.Add(new LiteralControl(templateScriptContent));

            txtAutoComplete.Attributes.Add("style", string.Format("width:{0};", TempWidth));
            if (_ValidationGroup != "")
                txtAutoComplete.Attributes.Add("validationgroup", ValidationGroup);
            if (_IsRequired)
                txtAutoComplete.Attributes.Add("class", "required txtAutoComplete");
            else
                txtAutoComplete.Attributes.Add("class", "txtAutoComplete");
            HtmlGenericControl divListAutoCompleteResultBox = CreateDiv("divListAutoCompleteResultBox");
            divListAutoCompleteResultBox.Controls.Add(CreateDiv("divListAutoCompleteResult"));

            divContainerAutoComplete.Controls.Add(CreateInputHidden("hdnAutoCompleteValue", "hdnAutoCompleteValue", _Value));
            divContainerAutoComplete.Controls.Add(CreateInputHidden("hdnAutoCompleteText"));
            divContainerAutoComplete.Controls.Add(CreateInputHidden("hdnIsRequired", "hdnIsRequired", _IsRequired ? "1" : "0"));
            divContainerAutoComplete.Controls.Add(CreateInputHidden("hdnValidationGroup", "hdnValidationGroup", _ValidationGroup));
            divContainerAutoComplete.Controls.Add(txtAutoComplete);
            divContainerAutoComplete.Controls.Add(CreateInputButton("btnAutoCompleteSearchMore btnSearch", ""));
            divContainerAutoComplete.Controls.Add(divListAutoCompleteResultBox);

            container.Controls.Add(divAutoComplete);
            Controls.Add(parent);
            #endregion

            //if (SaveState == "")
            RegisterJavaScript();
            base.Render(writer);
        }

        private string GetHtmlInputHiddenValue(string name)
        {
            string temp = this.ClientID.Replace('_', '$') + "_" + name;
            if (Page.Request.Form[temp] != null)
                return Page.Request.Form[temp].ToString();
            return "";
        }

        protected override void OnLoad(EventArgs e)
        {
            
        }

        private HtmlInputHidden CreateInputHidden(string cssClass, string name = "", string value = "")
        {
            HtmlInputHidden hdn = new HtmlInputHidden();
            if (name != "")
                hdn.ID = this.ID + "_" + name;
            hdn.Value = value;
            hdn.Attributes.Add("class", cssClass);
            return hdn;
        }

        private HtmlInputButton CreateInputButton(string cssClass, string value)
        {
            HtmlInputButton hdn = new HtmlInputButton();
            hdn.Attributes.Add("class", cssClass);
            hdn.Attributes.Add("value", value);
            return hdn;
        }

        private HtmlGenericControl CreateDiv(string cssClass)
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Attributes.Add("class", cssClass);
            return div;
        }

        private void SaveDataSourceState()
        {
            //StringBuilder saveState = new StringBuilder();
            //saveState.Append(DataSourceBusinessLayer.FilterExpression).Append("|");
            //if (DataSourceBusinessLayer.IsFilterExpressionChanged)
            //    saveState.Append("1");
            //else
            //    saveState.Append("0");
            //hdnSaveState.Value = saveState.ToString();
        }

        private void RegisterJavaScript()
        {
            WebControl script = new WebControl(HtmlTextWriterTag.Script);
            divAutoComplete.Controls.Add(script);
            script.Attributes["id"] = string.Format("dxss_{0}", this.ClientID);
            script.Attributes["type"] = "text/javascript";

            if (_ClientSideEvents.ValueChanged == "")
                _ClientSideEvents.ValueChanged = "function(){}";
            if (_ClientSideEvents.ButtonSearchClick == "")
                _ClientSideEvents.ButtonSearchClick = "function(){}";

            script.Controls.Add(new LiteralControl("$(function () {"));

            int filterType = 0;
            if (_FilterType == CodeXAutoCompleteTextBoxFilterType.Contains)
                filterType = 1;

            script.Controls.Add(new LiteralControl(string.Format("var {0}helper = new CodeXClientAutoCompleteHelper();", ClientInstanceName)));
            script.Controls.Add(new LiteralControl(string.Format("{0}helper.init(\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\");", ClientInstanceName, this.ClientID, SearchFields, MethodName, FilterExpression, GetFilterExpressionFunction, OrderByExpression, filterType)));
            script.Controls.Add(new LiteralControl(string.Format("{0}helper.setClientSideEvents({1},{2});", ClientInstanceName, _ClientSideEvents.ValueChanged, _ClientSideEvents.ButtonSearchClick)));
            script.Controls.Add(new LiteralControl(string.Format("{0}helper.initializeControl();", ClientInstanceName)));

            script.Controls.Add(new LiteralControl(string.Format("window.{0} = new CodeXClientAutoComplete();", ClientInstanceName)));
            script.Controls.Add(new LiteralControl(string.Format("{0}.init({0}helper);", ClientInstanceName)));
            if(Readonly)
                script.Controls.Add(new LiteralControl(string.Format("{0}.setEnabled(false);", ClientInstanceName)));
            script.Controls.Add(new LiteralControl("});"));
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
        }
    }

}