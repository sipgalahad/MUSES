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
    public partial class ProspectiveStudentRemarksDtEntry : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String ID = Request.QueryString["id"];
                Registration registration = BusinessLayer.GetRegistration(Convert.ToInt32(ID));
                hdnID.Value = registration.ProspectiveStudentID.ToString();

                SetControlProperties();
                ProspectiveStudent entity = BusinessLayer.GetProspectiveStudent(Convert.ToInt32(hdnID.Value));

                EntityToControl(entity);
                cboBloodType.Focus();
            }
        }

        private void SetControlProperties()
        {
            String filterExpression = String.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0",
                Constant.StandardCode.BLOOD_TYPE, Constant.StandardCode.LANGUAGE);
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(filterExpression);

            Methods.SetComboBoxField(cboBloodType, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.BLOOD_TYPE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboLanguage, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.LANGUAGE).ToList(), "StandardCodeName", "StandardCodeID");
        }

        private void EntityToControl(ProspectiveStudent entity)
        {
            cboBloodType.Value = entity.GCBloodType;
            cboLanguage.Value = entity.GCLanguage;
            txtHomeDistance.Text = entity.HomeDistance.ToString();
            txtMedicalHistory.Text = entity.MedicalHistory;
        }

        private void ControlToEntity(ProspectiveStudent entity)
        {
            entity.GCBloodType = cboBloodType.Value == null ? "" : cboBloodType.Value.ToString();
            entity.GCLanguage = cboLanguage.Value == null ? "" : cboLanguage.Value.ToString();
            entity.HomeDistance = Convert.ToDecimal(txtHomeDistance.Text);
            entity.MedicalHistory = txtMedicalHistory.Text;
        }

        private bool OnSaveRecord(ref string errMessage)
        {
            try
            {
                ProspectiveStudent entity = BusinessLayer.GetProspectiveStudent(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateProspectiveStudent(entity);
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