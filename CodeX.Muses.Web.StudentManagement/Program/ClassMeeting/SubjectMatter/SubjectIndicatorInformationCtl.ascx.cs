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
    public partial class SubjectIndicatorInformationCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            SubjectBasicCompetency entity = BusinessLayer.GetSubjectBasicCompetency(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0}", entity.SubjectBasicCompetencyName);

            BindGridView();
        }

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetSubjectIndicatorList(string.Format("SubjectBasicCompetencyID = {0} AND IsDeleted = 0", hdnID.Value));
            grdView.DataBind();
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}