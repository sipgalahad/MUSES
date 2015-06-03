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
    public partial class ProspectiveStudentTrusteeDtEntry : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String filterExpression = String.Format("ParentID IN ('{0}','{1}','{2}','{3}','{4}','{5}','{6}') AND StandardCodeID NOT IN ('{7}','{8}') AND IsActive = 1 AND IsDeleted = 0",
                    Constant.StandardCode.EDUCATION, Constant.StandardCode.SUFFIX, Constant.StandardCode.TITLE, Constant.StandardCode.NATIONALITY, Constant.StandardCode.RELIGION, Constant.StandardCode.FAMILY_RELATION, Constant.StandardCode.GENDER, Constant.FamilyRelation.FATHER, Constant.FamilyRelation.MOTHER);
                List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(filterExpression);

                Methods.SetComboBoxField(cboEducationLevel, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.EDUCATION).ToList(), "StandardCodeName", "StandardCodeID");
                Methods.SetComboBoxField(cboSuffix, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.SUFFIX).ToList(), "StandardCodeName", "StandardCodeID");
                Methods.SetComboBoxField(cboTitle, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.TITLE).ToList(), "StandardCodeName", "StandardCodeID");
                Methods.SetComboBoxField(cboNationality, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.NATIONALITY).ToList(), "StandardCodeName", "StandardCodeID");
                Methods.SetComboBoxField(cboReligion, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.RELIGION).ToList(), "StandardCodeName", "StandardCodeID");
                Methods.SetComboBoxField(cboFamilyRelation, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.FAMILY_RELATION).ToList(), "StandardCodeName", "StandardCodeID");
                Methods.SetComboBoxField(cboGender, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.GENDER).ToList(), "StandardCodeName", "StandardCodeID");

                String ID = Request.QueryString["id"];
                Registration registration = BusinessLayer.GetRegistration(Convert.ToInt32(ID));
                hdnID.Value = registration.ProspectiveStudentID.ToString();
                BindGridView();

                #region  Data
                Helper.SetControlEntrySetting(cboFamilyRelation, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(cboTitle, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(txtFirstName, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(txtMiddleName, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(txtLastName, new ControlEntrySetting(true, true, true), "mpTrx");
                Helper.SetControlEntrySetting(txtBirthPlace, new ControlEntrySetting(true, true, true), "mpTrx");
                Helper.SetControlEntrySetting(txtDOB, new ControlEntrySetting(true, true, true), "mpTrx");
                Helper.SetControlEntrySetting(cboSuffix, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(cboReligion, new ControlEntrySetting(true, true, true), "mpTrx");
                Helper.SetControlEntrySetting(cboNationality, new ControlEntrySetting(true, true, true), "mpTrx");
                Helper.SetControlEntrySetting(cboEducationLevel, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(cboGender, new ControlEntrySetting(true, true, true), "mpTrx");
                #endregion
            }
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = string.Format("ProspectiveStudentID = {0} AND GCFamilyRelation NOT IN ('{1}','{2}') AND IsDeleted = 0", hdnID.Value, Constant.FamilyRelation.FATHER, Constant.FamilyRelation.MOTHER);
            List<vProspectiveStudentFamily> lstEntity = BusinessLayer.GetvProspectiveStudentFamilyList(filterExpression);
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
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                if (OnDeleteEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity(ProspectiveStudentFamily entity)
        {
            entity.GCFamilyRelation = cboFamilyRelation.Value.ToString();
            entity.GCSalutation = "";
            entity.GCSuffix = cboSuffix.Value == null ? "" : cboSuffix.Value.ToString();
            entity.GCTitle = cboTitle.Value == null ? "" : cboTitle.Value.ToString();
            entity.FirstName = txtFirstName.Text;
            entity.MiddleName = txtMiddleName.Text;
            entity.LastName = txtLastName.Text;

            string suffix = cboSuffix.Value == null ? "" : cboSuffix.Text;
            string title = cboTitle.Value == null ? "" : cboTitle.Text;
            string Name = Helper.GenerateName(entity.LastName, entity.MiddleName, entity.FirstName);
            entity.FamilyName = Helper.GenerateFullName(Name, title, suffix);

            entity.CityOfBirth = txtBirthPlace.Text;
            entity.DateOfBirth = Helper.GetDatePickerValue(txtDOB.Text);
            entity.GCGender = cboGender.Value.ToString();

            entity.GCNationality = cboNationality.Value.ToString();
            entity.GCReligion = cboReligion.Value.ToString();
            entity.GCEducationLevel = cboEducationLevel.Value == null ? "" : cboEducationLevel.Value.ToString();
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                ProspectiveStudentFamily entity = new ProspectiveStudentFamily();
                ControlToEntity(entity);
                entity.ProspectiveStudentID = Convert.ToInt32(hdnID.Value);
                entity.OfficeAddressID = null;
                entity.IsDeleted = false;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertProspectiveStudentFamily(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            try
            {
                ProspectiveStudentFamily entity = BusinessLayer.GetProspectiveStudentFamily(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateProspectiveStudentFamily(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                ProspectiveStudentFamily entity = BusinessLayer.GetProspectiveStudentFamily(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedDate = DateTime.Now;
                BusinessLayer.UpdateProspectiveStudentFamily(entity);
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