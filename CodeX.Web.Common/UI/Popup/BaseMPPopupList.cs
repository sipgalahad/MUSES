using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace CodeX.Web.Common.UI
{
    public abstract class BaseMPPopupList : System.Web.UI.UserControl
    {
        public abstract Control GetPanelListPopup();
        public virtual void SetPageTitle(string title) { }
    }
}
