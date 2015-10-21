using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common;

namespace CodeX.Web.CommonLibs.Program
{
    public partial class MPListPopupCtl : BaseMPPopupList
    {
        private BaseViewPopupCtl ctl = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            bool IsAllowExport = false;
            ctl = (BaseViewPopupCtl)pnlListPopup.Controls[0];
            ctl.SetToolbarVisibility(ref IsAllowExport);
            if (!IsAllowExport)
                btnMPListPopupExport.Style.Add("display", "none");
        }

        public override Control GetPanelListPopup()
        {
            return pnlListPopup;
        }

        public override void SetPageTitle(string title)
        {
            hdnPageTitle.Value = title;
        }

        public string GetLabel(string code)
        {
            return ctl.GetLabel(code);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            bool isShowTitle = true;
            string fileName = "";
            Control controlHtml = ctl.OnGetExportControl(ref isShowTitle, ref fileName);
            if (controlHtml == null)
                controlHtml = ctl.OnGetExportControl();
            string pageTitle = ctl.OnGetPageTitle();
            if (pageTitle == "")
                pageTitle = hdnPageTitle.Value;

            if (fileName == "")
                fileName = pageTitle;

            Helper.ExportExcel(fileName, pageTitle, controlHtml, this, isShowTitle);
        }
    }
}