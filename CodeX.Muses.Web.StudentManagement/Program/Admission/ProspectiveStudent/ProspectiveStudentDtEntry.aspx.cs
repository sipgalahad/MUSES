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
    public partial class ProspectiveStudentDtEntry : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    hdnIsAdd.Value = "0";
                    String ID = Request.QueryString["id"];
                    hdnID.Value = ID;
                    String filterExpression = String.Format("RegistrationID = {0}", Convert.ToInt32(ID));
                    vRegistration entity = BusinessLayer.GetvRegistrationList(filterExpression)[0];
                    SetControlProperties();
                    EntityToControl(entity);
                }
                else
                {
                    txtDOB.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                    txtRegistrationDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                    txtRegistrationTime.Text = DateTime.Now.ToString(Constant.FormatString.TIME_FORMAT);
                    SetControlProperties();
                    hdnIsAdd.Value = "1";
                }

                hdnInitial.Value = BusinessLayer.GetPeriodAdmission(AppSession.PeriodAdmissionID).Initial;

                OnControlEntrySetting();
            }
        }

        #region Html Getter
        protected string OnGetProvinceFilterExpression()
        {
            return string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PROVINCE);
        }
        #endregion

        private void OnControlEntrySetting()
        {
            #region Registration Information
            Helper.SetControlEntrySetting(txtRegistrationNo, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtRegistrationDate, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(txtRegistrationTime, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(cboRegistrationType, new ControlEntrySetting(true, true, true), "mpEntry");
            #endregion

            #region Student Data
            Helper.SetControlEntrySetting(cboSalutation, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(cboTitle, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtFirstName, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtMiddleName, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtLastName, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(txtPreferredName, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(cboSuffix, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtBirthPlace, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(cboGender, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(cboReligion, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(txtDOB, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(txtAgeInDay, new ControlEntrySetting(false, false, true, 0), "mpEntry");
            Helper.SetControlEntrySetting(txtAgeInMonth, new ControlEntrySetting(false, false, true, 0), "mpEntry");
            Helper.SetControlEntrySetting(txtAgeInYear, new ControlEntrySetting(false, false, true, 0), "mpEntry");
            #endregion

            #region Patient Address
            Helper.SetControlEntrySetting(txtAddress, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(txtCounty, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtDistrict, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtCity, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(tacProvince, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(tacZipCode, new ControlEntrySetting(true, true, false), "mpEntry");
            #endregion

            #region Patient Contact
            Helper.SetControlEntrySetting(txtTelephoneNo, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(txtMobilePhoneNo1, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtMobilePhoneNo2, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtEmailAddress1, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtEmailAddress2, new ControlEntrySetting(true, true, false), "mpEntry");
            #endregion
        }

        private void SetControlProperties()
        {
            String filterExpression = String.Format("ParentID IN ('{0}','{1}','{2}','{3}','{4}','{5}','{6}') AND IsActive = 1 AND IsDeleted = 0",
                Constant.StandardCode.SALUTATION, Constant.StandardCode.SUFFIX, Constant.StandardCode.TITLE, Constant.StandardCode.GENDER, Constant.StandardCode.NATIONALITY, Constant.StandardCode.RELIGION, Constant.StandardCode.REGISTRATION_TYPE);
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(filterExpression);

            Methods.SetComboBoxField(cboSalutation, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.SALUTATION).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboSuffix, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.SUFFIX).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboTitle, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.TITLE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboNationality, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.NATIONALITY).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboGender, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.GENDER).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboReligion, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.RELIGION).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboRegistrationType, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.REGISTRATION_TYPE).ToList(), "StandardCodeName", "StandardCodeID");

            hdnAddressPrefix.Value = BusinessLayer.GetStandardCode(Constant.AddressType.PROSPECTIVE_STUDENT).TagProperty;
        }

        private void EntityToControl(vRegistration entity)
        {
            #region Registration Information
            txtRegistrationNo.Text = entity.RegistrationNo;
            txtRegistrationDate.Text = entity.RegistrationDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtRegistrationTime.Text = entity.RegistrationTime;
            cboRegistrationType.Value = entity.GCRegistrationType;
            #endregion

            #region Student Information
            cboSalutation.Value = entity.GCSalutation;
            cboSuffix.Value = entity.GCSuffix;
            cboTitle.Value = entity.GCTitle;
            cboGender.Value = entity.GCGender;
            txtFirstName.Text = entity.FirstName;
            txtMiddleName.Text = entity.MiddleName;
            txtLastName.Text = entity.LastName;
            txtPreferredName.Text = entity.PreferredName;
            txtBirthPlace.Text = entity.CityOfBirth;
            txtDOB.Text = entity.DateOfBirth.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            cboNationality.Value = entity.GCNationality;
            cboReligion.Value = entity.GCReligion;
            txtAgeInYear.Text = entity.AgeInYear.ToString();
            txtAgeInMonth.Text = entity.AgeInMonth.ToString();
            txtAgeInDay.Text = entity.AgeInDay.ToString();
            #endregion

            #region Address
            txtAddress.Text = entity.StreetName;
            txtCounty.Text = entity.County; // Desa
            txtDistrict.Text = entity.District; //Kabupaten
            txtCity.Text = entity.City;
            if (entity.GCState != "")
                tacProvince.Value = entity.GCState.Split('^')[1];
            else
                tacProvince.Value = "";
            tacProvince.Text = entity.State;
            tacZipCode.Value = entity.ZipCodeID.ToString();
            tacZipCode.Text = entity.ZipCode.ToString();
            #endregion

            #region Contact
            txtEmailAddress1.Text = entity.EmailAddress1;
            txtEmailAddress2.Text = entity.EmailAddress2;
            txtMobilePhoneNo1.Text = entity.MobilePhoneNo1;
            txtMobilePhoneNo2.Text = entity.MobilePhoneNo2;
            txtTelephoneNo.Text = entity.PhoneNo1;
            #endregion
        }

        private void ControlToEntity(Registration entityRegistration, ProspectiveStudent entity, Address entityAddress)
        {
            #region Registration Information
            entityRegistration.RegistrationDate = Helper.GetDatePickerValue(txtRegistrationDate.Text);
            entityRegistration.RegistrationTime = txtRegistrationTime.Text;
            entityRegistration.GCRegistrationType = cboRegistrationType.Value.ToString();
            #endregion

            #region Student
            entity.ProspectiveStudentCode = "";
            entity.GCSalutation = cboSalutation.Value == null ? "" : cboSalutation.Value.ToString();
            entity.GCSuffix = cboSuffix.Value == null ? "" : cboSuffix.Value.ToString();
            entity.GCTitle = cboTitle.Value == null ? "" : cboTitle.Value.ToString();
            entity.GCGender = cboGender.Value.ToString();
            entity.FirstName = txtFirstName.Text;
            entity.MiddleName = txtMiddleName.Text;
            entity.LastName = txtLastName.Text;
            entity.PreferredName = txtPreferredName.Text;
            entity.CityOfBirth = txtBirthPlace.Text;

            string suffix = cboSuffix.Value == null ? "" : cboSuffix.Text;
            string title = cboTitle.Value == null ? "" : cboTitle.Text;
            entity.Name = Helper.GenerateName(entity.LastName, entity.MiddleName, entity.FirstName);
            entity.ProspectiveStudentName = Helper.GenerateFullName(entity.Name, title, suffix);

            entity.DateOfBirth = Helper.GetDatePickerValue(txtDOB.Text);
            entity.GCNationality = cboNationality.Value.ToString();
            entity.GCReligion = cboReligion.Value.ToString();
            #endregion

            #region Address
            entityAddress.StreetName = txtAddress.Text;
            entityAddress.County = txtCounty.Text; // Desa
            entityAddress.District = txtDistrict.Text; //Kabupaten
            entityAddress.City = txtCity.Text;
            entityAddress.GCState = tacProvince.Value == "" ? null : string.Format("{0}^{1}", Constant.StandardCode.PROVINCE, tacProvince.Value);
            if (tacZipCode.Value == "" || tacZipCode.Value == "0")
                entityAddress.ZipCode = null;
            else
                entityAddress.ZipCode = Convert.ToInt32(tacZipCode.Value);
            #endregion

            #region Contact
            entity.EmailAddress1 = txtEmailAddress1.Text;
            entity.EmailAddress2 = txtEmailAddress2.Text;
            entity.MobilePhoneNo1 = txtMobilePhoneNo1.Text;
            entity.MobilePhoneNo2 = txtMobilePhoneNo2.Text;
            entityAddress.PhoneNo1 = txtTelephoneNo.Text;
            #endregion
        }

        private bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            RegistrationDao entityRegistrationDao = new RegistrationDao(ctx);
            ProspectiveStudentDao entityDao = new ProspectiveStudentDao(ctx);
            AddressDao addressDao = new AddressDao(ctx);
            bool result = true;
            try
            {
                Registration entityRegistration = new Registration();
                ProspectiveStudent entity = new ProspectiveStudent();
                Address address = new Address();
                ControlToEntity(entityRegistration, entity, address);

                entity.SiteID = AppSession.UserLogin.SiteID;
                entity.PeriodAdmissionID = AppSession.PeriodAdmissionID;
                entity.AddressID = null;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);

                entity.ProspectiveStudentID = BusinessLayer.GetProspectiveStudentMaxID(ctx);
                address.GCAddressType = Constant.AddressType.PROSPECTIVE_STUDENT;
                entity.AddressID = address.AddressID = string.Format("{0}{1}", hdnAddressPrefix.Value, entity.ProspectiveStudentID);
                addressDao.Insert(address);
                entityDao.Update(entity);

                entityRegistration.RegistrationNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.REGISTRATION, entityRegistration.RegistrationDate, hdnInitial.Value, ctx);
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityRegistration.PeriodAdmissionID = AppSession.PeriodAdmissionID;
                entityRegistration.GCRegistrationStatus = Constant.RegistrationStatus.OPEN;
                entityRegistration.ProspectiveStudentID = entity.ProspectiveStudentID;
                entityRegistration.CreatedBy = AppSession.UserLogin.UserID;
                entityRegistrationDao.Insert(entityRegistration);
                retval = BusinessLayer.GetRegistrationMaxID(ctx).ToString();

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnSaveEditRecord(ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            RegistrationDao entityRegistrationDao = new RegistrationDao(ctx);
            ProspectiveStudentDao entityDao = new ProspectiveStudentDao(ctx);
            AddressDao addressDao = new AddressDao(ctx);
            bool result = true;
            try
            {
                Registration entityRegistration = entityRegistrationDao.Get(Convert.ToInt32(hdnID.Value));
                ProspectiveStudent entity = entityDao.Get(entityRegistration.ProspectiveStudentID);
                Address address = addressDao.Get(entity.AddressID);
                ControlToEntity(entityRegistration, entity, address);

                entityRegistration.LastUpdatedBy = AppSession.UserLogin.UserID;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                addressDao.Update(address);
                entityDao.Update(entity);
                entityRegistrationDao.Update(entityRegistration);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }

            return result;
        }

        private void OnBtnSaveClick(ref string result)
        {
            string errMessage = "";
            string retval = "";
            result = "save|";
            if (hdnIsAdd.Value == "1")
            {
                if (OnSaveAddRecord(ref errMessage, ref retval))
                    result += string.Format("success|{0}", retval);
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else
            {
                if (OnSaveEditRecord(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
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