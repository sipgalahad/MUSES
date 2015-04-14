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
using CodeX.Common;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class GradePromotionFormulaDtCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            grdView.DataSource = BusinessLayer.GetvGradePromotionFormulaDtList(string.Format("GradePromotionFormulaID = {0} ORDER BY DisplayOrder ASC", param));
            grdView.DataBind();
        }
    }
}