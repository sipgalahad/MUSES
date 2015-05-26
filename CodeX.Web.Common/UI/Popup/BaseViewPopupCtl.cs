using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeX.Data.Model;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace CodeX.Web.Common.UI
{
    public abstract class BaseViewPopupCtl : BaseContentPopupCtl
    {
        private BaseMPPopupList MasterControl = null;

        public override void LoadMasterControl(string title)
        {
            MasterControl = (BaseMPPopupList)LoadControl("~/Libs/Controls/MPListPopupCtl.ascx");
            MasterControl.SetPageTitle(title);
            this.Parent.Controls.Add(MasterControl);
            MasterControl.GetPanelListPopup().Controls.Add(this);
        }

        public override void InitializeControl(string param)
        {
            base.InitializeControl(param);
            InitializeDataControl(param);

            //base.InitializeControl(param);
            //InitializeDataControl(param);
        }

        public abstract void InitializeDataControl(string param);

        public virtual void SetToolbarVisibility(ref bool IsAllowExport)
        {
            IsAllowExport = false;
        }
        public virtual Control OnGetExportControl(ref bool isShowTitle)
        {
            return null;
        }
        public virtual Control OnGetExportControl()
        {
            return null;
        }
        public virtual string OnGetPageTitle()
        {
            return "";
        }
    }
}
