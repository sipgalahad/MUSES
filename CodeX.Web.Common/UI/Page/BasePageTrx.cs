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
    public abstract class BasePageTrx : BasePageContent
    {
        #region Session & View State
        public Hashtable ControlEntryList
        {
            get
            {
                if (Session["__ControlEntryList"] == null)
                    Session["__ControlEntryList"] = new Hashtable();

                return (Hashtable)Session["__ControlEntryList"];
            }
            set { Session["__ControlEntryList"] = value; }
        }
        #endregion

        public int PageIndex = 0;
        public bool IsLoadFirstRecord = false;
        public bool IsAutoReInitControl = true;
        public bool isShowWatermark = false;
        public string watermarkText = "";
        protected override void OnLoad(EventArgs e)
        {
            if (!Page.IsCallback)
            {
                ControlEntryList.Clear();
                InitializeDataControl();
                InitializePopupList();
                OnControlEntrySetting();
                if (!IsLoadFirstRecord)
                {
                    if (IsAutoReInitControl)
                        ReInitControl();
                    SetControlEnabled(true);
                    SetControlProperties();
                }
                else
                {
                    LoadPage(PageIndex, ref isShowWatermark, ref watermarkText);
                }
            }
        }

        #region Button Event
        public void NextPageIndex(int rowCount, ref int pageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            LoadWords();
            if (rowCount > 0)
            {
                SetControlRequired();
                SetControlEnabled(false);
                pageIndex++;
                if (pageIndex == rowCount)
                    pageIndex = 0;
                OnLoadEntity(pageIndex, ref isShowWatermark, ref watermarkText);
                SetControlProperties();
            }
        }

        public void LoadPage(string keyValue, ref int pageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            LoadWords();
            SetControlRequired();
            SetControlEnabled(false);
            OnLoadEntity(keyValue, ref pageIndex, ref isShowWatermark, ref watermarkText);
            SetControlProperties();
        }

        public void LoadPage(int pageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            LoadWords();
            SetControlRequired();
            SetControlEnabled(false);
            OnLoadEntity(pageIndex, ref isShowWatermark, ref watermarkText);
            SetControlProperties();
        }

        public void PrevPageIndex(int rowCount, ref int pageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            LoadWords();
            if (rowCount > 0)
            {
                SetControlRequired();
                SetControlEnabled(false);
                pageIndex--;
                if (pageIndex < 0)
                    pageIndex = rowCount - 1;
                OnLoadEntity(pageIndex, ref isShowWatermark, ref watermarkText);
                SetControlProperties();
            }
        }

        public void AddRecord()
        {
            SetControlEnabled(true);
            ReInitControl();
            OnAddRecord();
            SetControlProperties();
        }

        public void RefreshControl()
        {
            SetControlRequired();
            SetControlEnabled(true);
            ReInitControl();
            OnAddRecord();
            SetControlProperties();
        }

        public void OnBtnSaveClick(ref string result, ref string retval, bool isAdd)
        {
            result = "save|";
            string errMessage = "";
            if (isAdd)
            {
                if (OnBeforeSaveAddRecord(ref errMessage))
                {
                    if (OnSaveAddRecord(ref errMessage, ref retval))
                        result += "success";
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
                    if (OnSaveEditRecord(ref errMessage, ref retval))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                    result += string.Format("fail|{0}", errMessage);
            }
        }

        public void OnBtnVoidClick(ref string result)
        {
            result = "void|";
            string errMessage = "";
            if (OnVoidRecord(ref errMessage))
                result += "success";
            else
                result += string.Format("fail|{0}", errMessage);
        }

        public void OnBtnProposeClick(ref string result)
        {
            result = "propose|";
            string errMessage = "";
            if (OnProposeRecord(ref errMessage))
                result += "success";
            else
                result += string.Format("fail|{0}", errMessage);
        }

        public void OnBtnApproveClick(ref string result)
        {
            result = "approve|";
            string errMessage = "";
            if (OnApproveRecord(ref errMessage))
                result += "success";
            else
                result += string.Format("fail|{0}", errMessage);
        }

        public void OnBtnReopenClick(ref string result)
        {
            result = "approve|";
            string errMessage = "";
            if (OnReopenRecord(ref errMessage))
                result += "success";
            else
                result += string.Format("fail|{0}", errMessage);
        }

        public void OnBtnCustomClick(ref string result, string type, ref string retval)
        {
            result = "customclick|";
            string errMessage = "";
            if (OnCustomButtonClick(type, ref errMessage))
                result += "success";
            else if (OnCustomButtonClick(type, ref errMessage, ref retval))
                result += "success";
            else
                result += string.Format("fail|{0}", errMessage);
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
        private void SetControlRequired()
        {
            foreach (DictionaryEntry entry in ControlEntryList)
            {
                Control ctrl = (Control)Helper.FindControlRecursive(this, entry.Key.ToString());
                ControlEntrySetting setting = (ControlEntrySetting)entry.Value;
                if (ctrl is CodeXAutoCompleteTextBox)
                {
                    CodeXAutoCompleteTextBox tac = (CodeXAutoCompleteTextBox)ctrl;
                    tac.ValidationGroup = "mpEntry";
                    tac.IsRequired = setting.IsRequired;
                }
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
                    if (setting.IsResetValue)
                    {
                        switch (setting.DefaultValue.ToString())
                        {
                            case Constant.DefaultValueEntry.DATE_NOW: SetControlValue(ctrl, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)); break;
                            case Constant.DefaultValueEntry.TIME_NOW: SetControlValue(ctrl, DateTime.Now.ToString(Constant.FormatString.TIME_FORMAT)); break;
                            default: SetControlValue(ctrl, setting.DefaultValue); break;
                        }
                    }
                }
            }
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
                    Helper.AddCssClass(((WebControl)ctrl), "required");
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

        public virtual string OnGetMenuCaption()
        {
            return "";
        }
        protected virtual void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
        }

        protected virtual void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
        }

        public virtual int OnGetRowCount()
        {
            return 0;
        }

        public virtual void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {

        }

        protected virtual void InitializeDataControl()
        {

        }

        protected virtual void InitializePopupList()
        {
        }

        public virtual void BindSearchList(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount) { }

        public virtual void OnAddRecord()
        {

        }
        public virtual bool IsRefreshControlAfterSaveAddRecord()
        {
            return true;
        }
        protected virtual void SetControlProperties()
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
        protected virtual bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            return false;
        }
        protected virtual bool OnVoidRecord(ref string errMessage)
        {
            return false;
        }
        protected virtual bool OnProposeRecord(ref string errMessage)
        {
            return false;
        }
        protected virtual bool OnApproveRecord(ref string errMessage)
        {
            return false;
        }
        protected virtual bool OnReopenRecord(ref string errMessage)
        {
            return false;
        }
        protected virtual bool OnCustomButtonClick(string type, ref string errMessage)
        {
            return false;
        }
        protected virtual bool OnCustomButtonClick(string type, ref string errMessage, ref string retval)
        {
            return false;
        }
        #endregion
    }
}
