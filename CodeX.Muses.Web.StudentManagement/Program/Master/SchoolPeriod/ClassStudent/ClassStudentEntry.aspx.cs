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
    public partial class ClassStudentEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SP_CLASS_STUDENT;
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
            if (cboClass.Value != null && cboClass.Value.ToString() != "0")
                filterExpression = string.Format("SchoolClassID = {0}", cboClass.Value);
            List<vClassStudent> lstEntity = BusinessLayer.GetvClassStudentList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "delete")
            {
                if (OnDeleteEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                BusinessLayer.DeleteClassStudent(Convert.ToInt32(cboClass.Value), Convert.ToInt32(hdnEntryID.Value));
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion
    }
}