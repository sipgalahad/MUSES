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
    public partial class SubjectBasicCompetencyDtInformationCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            SubjectBasicCompetency entity = BusinessLayer.GetSubjectBasicCompetency(Convert.ToInt32(hdnID.Value));
            txtSubjectBasicCompetencyName.Text = entity.SubjectBasicCompetencyName;

            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SUBJECT_BASIC_COMPETENCY_DT_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboGCSubjectBasicCompetencyDtType, lstSc, "StandardCodeName", "StandardCodeID");
            cboGCSubjectBasicCompetencyDtType.SelectedIndex = 0;

            BindGridView();
        }

        private void BindGridView()
        {
            string filterExpression = string.Format("SubjectBasicCompetencyID = {0} AND GCSubjectBasicCompetencyDtType = '{1}'", hdnID.Value, cboGCSubjectBasicCompetencyDtType.Value);
            grdView.DataSource = BusinessLayer.GetSubjectBasicCompetencyDtList(filterExpression);
            grdView.DataBind();
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}