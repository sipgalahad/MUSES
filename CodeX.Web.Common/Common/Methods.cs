using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;

namespace CodeX.Web.Common
{
    public class Methods
    {
        public static void SetComboBoxField<T>(DropDownList ctl, List<T> list, string textField, string valueField)
        {
            ctl.DataTextField = textField;
            ctl.DataValueField = valueField;
            ctl.DataSource = list;
            ctl.DataBind();
        }


        public static void SetComboBoxField<T>(ASPxComboBox ctl, List<T> list, string textField, string valueField)
        {
            SetComboBoxField(ctl, list, textField, valueField, DropDownStyle.DropDownList);
        }

        public static void SetComboBoxField<T>(ASPxComboBox ctl, List<T> list, string textField, string valueField, DropDownStyle dropDownStyle)
        {
            ctl.TextField = textField;
            ctl.ValueField = valueField;
            ctl.DataSource = list;
            ctl.CallbackPageSize = 50;
            ctl.EnableCallbackMode = false;
            ctl.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            ctl.DropDownStyle = dropDownStyle;
            ctl.DataBind();
        }

        public static void SetRadioButtonListField<T>(RadioButtonList ctl, List<T> list, string textField, string valueField)
        {
            ctl.DataTextField = textField;
            ctl.DataValueField = valueField;
            ctl.DataSource = list;
            ctl.DataBind();
        }
    }
}
