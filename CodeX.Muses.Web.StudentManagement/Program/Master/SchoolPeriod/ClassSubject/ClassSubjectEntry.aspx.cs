using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using System.Data;
using CodeX.Data.Core.Dal;
using CodeX.Common;
using DevExpress.Web.ASPxCallbackPanel;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ClassSubjectEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SP_CLASS_SUBJECT;
        }
        protected override void InitializeDataControl()
        {
            List<vSchoolClass> lstClassType = BusinessLayer.GetvSchoolClassList(string.Format("SchoolPeriodID = {0} AND IsDeleted = 0", AppSession.SchoolPeriodID));
            Methods.SetComboBoxField<vSchoolClass>(cboClass, lstClassType, "SchoolClassName", "SchoolClassID");
            cboClass.SelectedIndex = 0;

            BindGridView();
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = "1 = 0";
            if(cboClass.Value != null && cboClass.Value.ToString() != "0")
                filterExpression = string.Format("SchoolPeriodID = {0} AND SchoolClassID = {1}", AppSession.SchoolPeriodID, cboClass.Value);
            List<vClassSubjectCustom> lstEntity = BusinessLayer.GetvClassSubjectCustomList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion
    }
}