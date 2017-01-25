using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxEditors;
using CodeX.Common;
using CodeX.Web.CustomControl;
using CodeX.Data.Model;

namespace CodeX.Web.Common.UI
{
    public abstract class BasePageEntry : BasePageContent
    {
        public bool IsAdd = false;
        #region Session & View State
        public Hashtable ControlEntryList
        {
            get
            {
                if (ViewState["__ControlEntryList"] == null)
                    ViewState["__ControlEntryList"] = new Hashtable();

                return (Hashtable)ViewState["__ControlEntryList"];
            }
            set { ViewState["__ControlEntryList"] = value; }
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            if (!Page.IsCallback)
            {
                ControlEntryList.Clear();
                OnControlEntrySetting();
                ReInitControl();
                InitializeDataControl();
                SetControlEnabled(IsAdd);
            }
        }

        #region Button Event
        public void RefreshControl()
        {
            IsAdd = true;
            SetControlEnabled(true);
            ReInitControl();
            SetControlProperties();
            OnAddRecord();
        }

        public void OnBtnSaveClick(ref string result, bool isAdd)
        {
            string errMessage = "";
            string retval = "";
            if (isAdd)
            {
                if (OnBeforeSaveAddRecord(ref errMessage))
                {
                    if (OnSaveAddRecord(ref errMessage, ref retval))
                        result += string.Format("success|{0}", retval);
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else
            {
                if (OnBeforeSaveEditRecord(ref errMessage))
                {
                    if (OnSaveEditRecord(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                    result += string.Format("fail|{0}", errMessage);
            }
        }
        #endregion

        #region Utility Function
        private void SetControlEnabled(bool isAdd)
        {
            foreach (DictionaryEntry entry in ControlEntryList)
            {
                Control ctrl = (Control)Helper.FindControlRecursive(this, entry.Key.ToString());
                ControlEntrySetting setting = (ControlEntrySetting)entry.Value;
                bool isEnabled = (isAdd ? setting.IsEditAbleInAddMode : setting.IsEditAbleInEditMode);
                SetControlAttribute(ctrl, isEnabled);
            }
        }

        public void ReInitControl()
        {
            LoadWords();
            foreach (DictionaryEntry entry in ControlEntryList)
            {
                Control ctrl = (Control)Helper.FindControlRecursive(this, entry.Key.ToString());
                if (ctrl is WebControl || ctrl is HtmlInputHidden)
                {
                    ControlEntrySetting setting = (ControlEntrySetting)entry.Value;
                    switch (setting.DefaultValue.ToString())
                    {
                        case Constant.DefaultValueEntry.DATE_NOW: SetControlValue(ctrl, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)); break;
                        case Constant.DefaultValueEntry.TIME_NOW: SetControlValue(ctrl, DateTime.Now.ToString(Constant.FormatString.TIME_FORMAT)); break;
                        default: SetControlValue(ctrl, setting.DefaultValue); break;
                    }
                }
            }
            OnReInitControl();
        }

        protected void SetControlEntrySetting(Control ctrl, ControlEntrySetting setting)
        {
            ControlEntryList.Add(ctrl.ID, setting);
            if (ctrl is ASPxEdit)
            {
                ASPxEdit ctl = ctrl as ASPxEdit;
                ctl.ValidationSettings.RequiredField.IsRequired = setting.IsRequired;
                ctl.ValidationSettings.RequiredField.ErrorText = "";
                ctl.ValidationSettings.CausesValidation = true;
                ctl.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
                ctl.ValidationSettings.ErrorFrameStyle.Paddings.Padding = new System.Web.UI.WebControls.Unit(0);

                //if (setting.IsRequired)
                ctl.ValidationSettings.ValidationGroup = "mpEntry";
            }
            else if (ctrl is CodeXAutoCompleteTextBox)
            {
                CodeXAutoCompleteTextBox tac = (CodeXAutoCompleteTextBox)ctrl;
                tac.ValidationGroup = "mpEntry";
                tac.IsRequired = setting.IsRequired;
            }
            else if (ctrl is WebControl)
            {
                if (setting.IsRequired)
                {
                    if (ctrl is RadioButtonList)
                    {
                        RadioButtonList rbl = ctrl as RadioButtonList;
                        if (rbl.Items.Count > 0)
                            rbl.Items[0].Attributes.Add("class", "required");
                    }
                    else
                        Helper.AddCssClass(((WebControl)ctrl), "required");
                }
                ((WebControl)ctrl).Attributes.Add("validationgroup", "mpEntry");
                if (setting.IsEditAbleInEditMode)
                    ((WebControl)ctrl).Attributes.Add("IsEditAbleInEditMode", "1");
                else
                    ((WebControl)ctrl).Attributes.Add("IsEditAbleInEditMode", "0");
            }
            else if (ctrl is HtmlGenericControl)
            {
                if (setting.IsEditAbleInEditMode)
                    ((HtmlGenericControl)ctrl).Attributes.Add("IsEditAbleInEditMode", "1");
                else
                    ((HtmlGenericControl)ctrl).Attributes.Add("IsEditAbleInEditMode", "0");
            }
        }

        private void SetControlAttribute(Control ctrl, bool isEnabled)
        {
            if (ctrl is ASPxEdit)
            {
                ((ASPxEdit)ctrl).ClientEnabled = isEnabled;
            }
            else if (ctrl is TextBox)
            {
                if (isEnabled)
                    ((TextBox)ctrl).ReadOnly = false;
                else
                    ((TextBox)ctrl).ReadOnly = true;
            }
            else if (ctrl is CodeXAutoCompleteTextBox)
            {
                if (isEnabled)
                    ((CodeXAutoCompleteTextBox)ctrl).Readonly = false;
                else
                    ((CodeXAutoCompleteTextBox)ctrl).Readonly = true;
            }
            else if (ctrl is DropDownList)
            {
                ((DropDownList)ctrl).Enabled = isEnabled;
            }
            else if (ctrl is CheckBox)
            {
                ((CheckBox)ctrl).Enabled = isEnabled;
            }
            else if (ctrl is HtmlGenericControl)
            {
                HtmlGenericControl lbl = ctrl as HtmlGenericControl;
                if (!isEnabled)
                    lbl.Attributes.Add("class", "lblDisabled");
            }
        }

        private void SetControlValue(Control ctrl, object value)
        {
            if (ctrl is ASPxEdit)
                ((ASPxEdit)ctrl).Value = value;
            else if (ctrl is TextBox)
                ((TextBox)ctrl).Text = value.ToString();
            else if (ctrl is CodeXAutoCompleteTextBox)
            {
                if (value is Variable)
                {
                    CodeXAutoCompleteTextBox tac = ctrl as CodeXAutoCompleteTextBox;
                    Variable varValue = value as Variable;
                    tac.Value = varValue.Code;
                    tac.Text = varValue.Value;
                }
            }
            else if (ctrl is DropDownList)
                Helper.SetDropDownListValue((DropDownList)ctrl, value.ToString());
            else if (ctrl is CheckBox)
            {
                if (value.ToString() == "")
                    ((CheckBox)ctrl).Checked = false;
                else
                    ((CheckBox)ctrl).Checked = Convert.ToBoolean(value);
            }
            else if (ctrl is HtmlInputHidden)
                ((HtmlInputHidden)ctrl).Value = value.ToString();

        }
        #endregion

        #region Virtual Function
        public virtual void OnAddRecord()
        {

        }

        protected virtual void InitializeDataControl()
        {

        }
        protected virtual void SetControlProperties()
        {
        }

        protected virtual void OnReInitControl()
        {
        }

        protected virtual void OnControlEntrySetting()
        {
        }

        protected virtual bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            return true;
        }

        protected virtual bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            return true;
        }

        protected virtual bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            return false;
        }
        protected virtual bool OnSaveEditRecord(ref string errMessage)
        {
            return false;
        }

        public virtual void SetCRUDMode(ref bool IsAllowSaveAndNew, ref bool IsAllowSaveAndClose)
        {
        }
        #endregion
    }
}
