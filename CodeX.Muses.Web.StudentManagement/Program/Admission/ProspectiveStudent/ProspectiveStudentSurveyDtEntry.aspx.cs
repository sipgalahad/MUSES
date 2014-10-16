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
    public partial class ProspectiveStudentSurveyDtEntry : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;

                SetControlProperties();
                Registration entity = BusinessLayer.GetRegistration(Convert.ToInt32(hdnID.Value));

                EntityToControl(entity);
                cboInformationSource.Focus();
            }
        }

        private void SetControlProperties()
        {
            String filterExpression = String.Format("ParentID IN ('{0}') AND IsActive = 1 AND IsDeleted = 0",
                Constant.StandardCode.INFORMATION_SOURCE);
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(filterExpression);

            Methods.SetComboBoxField(cboInformationSource, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.INFORMATION_SOURCE).ToList(), "StandardCodeName", "StandardCodeID");
        }

        private void EntityToControl(Registration entity)
        {
            cboInformationSource.Value = entity.GCInformationSource;
        }

        private void ControlToEntity(Registration entity)
        {
            entity.GCInformationSource = cboInformationSource.Value == null ? "" : cboInformationSource.Value.ToString();
        }

        private bool OnSaveRecord(ref string errMessage)
        {
            try
            {
                Registration entity = BusinessLayer.GetRegistration(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateRegistration(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        private void OnBtnSaveClick(ref string result)
        {
            string errMessage = "";
            string retval = "";
            result = "save|";
            if (OnSaveRecord(ref errMessage))
                result += string.Format("success|{0}", retval);
            else
                result += string.Format("fail|{0}", errMessage);
        }

        protected void cbpMPEntryProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string[] param = e.Parameter.Split('|');
            if (param[0] == "save")
            {
                OnBtnSaveClick(ref result);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
    }
}