using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common;

namespace CodeX.Ottimo.Web.ControlPanel.Program
{
    public partial class TemplateContentViewCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            SchoolAnnouncement entity = BusinessLayer.GetSchoolAnnouncement(Convert.ToInt32(param));
            divTemplateContent.InnerHtml = entity.Remarks;            
        }
    }
}