using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace CodeX.Web.CustomControl
{
    public class CodeXAutoCompleteTextBoxClientSideEvent
    {
        private String _ValueChanged = "";
        private String _ButtonSearchClick = "";
        private String _Init;

        [Category("ClientSideEvents")]
        [NotifyParentProperty(true)]
        [Description("OnValueChanged")]
        public String ValueChanged
        {
            get { return _ValueChanged; }
            set { _ValueChanged = value; }
        }

        [Category("ClientSideEvents")]
        [NotifyParentProperty(true)]
        [Description("OnBtnSearchClick")]
        public String ButtonSearchClick
        {
            get { return _ButtonSearchClick; }
            set { _ButtonSearchClick = value; }
        }

        [Category("Layout")]
        [NotifyParentProperty(true)]
        [Description("TableItem_HorizontalAlign")]
        public String Init
        {
            get { return _Init; }
            set { _Init = value; }
        }
    }
}
