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
    public partial class StudentParentEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.ST_STUDENT_PARENT;
        }

        protected override void InitializeDataControl()
        {
            hdnIsAdd.Value = "1";

            SetControlProperties();
            List<vStudentFamily> lstEntity = BusinessLayer.GetvStudentFamilyList(string.Format("StudentID = {0} AND GCFamilyRelation IN ('{1}','{2}')", AppSession.StudentID, Constant.FamilyRelation.FATHER, Constant.FamilyRelation.MOTHER));
            if (lstEntity.Count > 0)
            {
                hdnIsAdd.Value = "0";
                vStudentFamily entityFather = lstEntity.FirstOrDefault(p => p.GCFamilyRelation == Constant.FamilyRelation.FATHER);
                vStudentFamily entityMother = lstEntity.FirstOrDefault(p => p.GCFamilyRelation == Constant.FamilyRelation.MOTHER);
                EntityToControl(entityFather, entityMother);
            }
            OnControlEntrySetting();
            txtFatherFirstName.Focus();
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Html Getter
        protected string OnGetProvinceFilterExpression()
        {
            return string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PROVINCE);
        }
        #endregion

        protected override void SetControlProperties()
        {
            String filterExpression = String.Format("ParentID IN ('{0}','{1}','{2}','{3}','{4}','{5}') AND IsActive = 1 AND IsDeleted = 0",
                Constant.StandardCode.EDUCATION, Constant.StandardCode.SUFFIX, Constant.StandardCode.TITLE, Constant.StandardCode.NATIONALITY, Constant.StandardCode.RELIGION, Constant.StandardCode.OCCUPATION);
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(filterExpression);

            Methods.SetComboBoxField(cboFatherEducationLevel, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.EDUCATION).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboFatherSuffix, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.SUFFIX).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboFatherTitle, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.TITLE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboFatherNationality, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.NATIONALITY).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboFatherReligion, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.RELIGION).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboFatherGCJob, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.OCCUPATION).ToList(), "StandardCodeName", "StandardCodeID");

            Methods.SetComboBoxField(cboMotherEducationLevel, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.EDUCATION).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboMotherSuffix, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.SUFFIX).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboMotherTitle, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.TITLE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboMotherNationality, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.NATIONALITY).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboMotherReligion, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.RELIGION).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboMotherGCJob, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.OCCUPATION).ToList(), "StandardCodeName", "StandardCodeID");

            hdnAddressPrefix.Value = BusinessLayer.GetStandardCode(Constant.AddressType.PROSPECTIVE_STUDENT_FAMILY).TagProperty;
        }

        private void OnControlEntrySetting()
        {
            #region Father Data
            Helper.SetControlEntrySetting(cboFatherTitle, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtFatherFirstName, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtFatherMiddleName, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtFatherLastName, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(cboFatherSuffix, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtFatherBirthPlace, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(txtFatherDOB, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(cboFatherReligion, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(cboFatherNationality, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(cboFatherEducationLevel, new ControlEntrySetting(true, true, true), "mpEntry");
            #endregion

            #region Father Company
            Helper.SetControlEntrySetting(txtFatherJobOffice, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(cboFatherGCJob, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtFatherOccupation, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtFatherSalary, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtFatherAddress, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtFatherCounty, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtFatherDistrict, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtFatherCity, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(tacFatherProvince, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(tacFatherZipCode, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtFatherTelephoneNo, new ControlEntrySetting(true, true, false), "mpEntry");
            #endregion

            #region Mother Data
            Helper.SetControlEntrySetting(cboMotherTitle, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtMotherFirstName, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtMotherMiddleName, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtMotherLastName, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(cboMotherSuffix, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtMotherBirthPlace, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(txtMotherDOB, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(cboMotherReligion, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(cboMotherNationality, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(cboMotherEducationLevel, new ControlEntrySetting(true, true, true), "mpEntry");
            #endregion

            #region Mother Company
            Helper.SetControlEntrySetting(txtMotherJobOffice, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(cboMotherGCJob, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtMotherOccupation, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtMotherSalary, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtMotherAddress, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtMotherCounty, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtMotherDistrict, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtMotherCity, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(tacMotherProvince, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(tacMotherZipCode, new ControlEntrySetting(true, true, false), "mpEntry");
            Helper.SetControlEntrySetting(txtMotherTelephoneNo, new ControlEntrySetting(true, true, false), "mpEntry");
            #endregion
        }

        private void EntityToControl(vStudentFamily entityFather, vStudentFamily entityMother)
        {
            #region Father
            cboFatherSuffix.Value = entityFather.GCSuffix;
            cboFatherTitle.Value = entityFather.GCTitle;
            txtFatherFirstName.Text = entityFather.FirstName;
            txtFatherMiddleName.Text = entityFather.MiddleName;
            txtFatherLastName.Text = entityFather.LastName;
            txtFatherBirthPlace.Text = entityFather.CityOfBirth;
            txtFatherDOB.Text = entityFather.DateOfBirth.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            cboFatherNationality.Value = entityFather.GCNationality;
            cboFatherReligion.Value = entityFather.GCReligion;
            cboFatherEducationLevel.Value = entityFather.GCEducationLevel;
            #endregion

            #region Father Office Address
            txtFatherJobOffice.Text = entityFather.CompanyName;
            cboFatherGCJob.Value = entityFather.GCJob;
            txtFatherOccupation.Text = entityFather.Occupation;
            txtFatherSalary.Text = entityFather.Salary.ToString();

            txtFatherAddress.Text = entityFather.OfficeStreetName;
            txtFatherCounty.Text = entityFather.OfficeCounty; // Desa
            txtFatherDistrict.Text = entityFather.OfficeDistrict; //Kabupaten
            txtFatherCity.Text = entityFather.OfficeCity;
            if (entityFather.OfficeGCState != "")
                tacFatherProvince.Value = entityFather.OfficeGCState.Split('^')[1];
            else
                tacFatherProvince.Value = "";
            tacFatherProvince.Text = entityFather.OfficeState;
            tacFatherZipCode.Value = entityFather.OfficeZipCodeID.ToString();
            tacFatherZipCode.Text = entityFather.OfficeZipCode.ToString();
            txtFatherTelephoneNo.Text = entityFather.OfficePhoneNo1;
            #endregion

            #region Mother
            cboMotherSuffix.Value = entityMother.GCSuffix;
            cboMotherTitle.Value = entityMother.GCTitle;
            txtMotherFirstName.Text = entityMother.FirstName;
            txtMotherMiddleName.Text = entityMother.MiddleName;
            txtMotherLastName.Text = entityMother.LastName;
            txtMotherBirthPlace.Text = entityMother.CityOfBirth;
            txtMotherDOB.Text = entityMother.DateOfBirth.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            cboMotherNationality.Value = entityMother.GCNationality;
            cboMotherReligion.Value = entityMother.GCReligion;
            cboMotherEducationLevel.Value = entityMother.GCEducationLevel;
            #endregion

            #region Mother Office Address
            txtMotherJobOffice.Text = entityMother.CompanyName;
            cboMotherGCJob.Value = entityMother.GCJob;
            txtMotherOccupation.Text = entityMother.Occupation;
            txtMotherSalary.Text = entityMother.Salary.ToString();

            txtMotherAddress.Text = entityMother.OfficeStreetName;
            txtMotherCounty.Text = entityMother.OfficeCounty; // Desa
            txtMotherDistrict.Text = entityMother.OfficeDistrict; //Kabupaten
            txtMotherCity.Text = entityMother.OfficeCity;
            if (entityMother.OfficeGCState != "")
                tacMotherProvince.Value = entityMother.OfficeGCState.Split('^')[1];
            else
                tacMotherProvince.Value = "";
            tacMotherProvince.Text = entityMother.OfficeState;
            tacMotherZipCode.Value = entityMother.OfficeZipCodeID.ToString();
            tacMotherZipCode.Text = entityMother.OfficeZipCode.ToString();
            txtMotherTelephoneNo.Text = entityMother.OfficePhoneNo1;
            #endregion
        }

        private void ControlToEntity(StudentFamily entityFather, StudentFamily entityMother, Address officeAddressFather, Address officeAddressMother)
        {
            #region Father
            entityFather.GCSalutation = "";
            entityFather.GCSuffix = cboFatherSuffix.Value == null ? "" : cboFatherSuffix.Value.ToString();
            entityFather.GCTitle = cboFatherTitle.Value == null ? "" : cboFatherTitle.Value.ToString();
            entityFather.FirstName = txtFatherFirstName.Text;
            entityFather.MiddleName = txtFatherMiddleName.Text;
            entityFather.LastName = txtFatherLastName.Text;
            entityFather.CityOfBirth = txtFatherBirthPlace.Text;
            entityFather.DateOfBirth = Helper.GetDatePickerValue(txtFatherDOB);

            string suffix = cboFatherSuffix.Value == null ? "" : cboFatherSuffix.Text;
            string title = cboFatherTitle.Value == null ? "" : cboFatherTitle.Text;
            string Name = Helper.GenerateName(entityFather.LastName, entityFather.MiddleName, entityFather.FirstName);
            entityFather.FamilyName = Helper.GenerateFullName(Name, title, suffix);

            entityFather.GCNationality = cboFatherNationality.Value.ToString();
            entityFather.GCReligion = cboFatherReligion.Value.ToString();
            entityFather.GCEducationLevel = cboFatherEducationLevel.Value.ToString();
            #endregion

            #region Office
            entityFather.CompanyName = txtFatherJobOffice.Text;
            entityFather.GCJob = cboFatherGCJob.Value == null ? "" : cboFatherGCJob.Value.ToString();
            entityFather.Occupation = txtFatherOccupation.Text;
            entityFather.Salary = txtFatherSalary.Text == "" ? 0 : Convert.ToDecimal(txtFatherSalary.Text);

            officeAddressFather.StreetName = txtFatherAddress.Text;
            officeAddressFather.County = txtFatherCounty.Text; // Desa
            officeAddressFather.District = txtFatherDistrict.Text; //Kabupaten
            officeAddressFather.City = txtFatherCity.Text;
            officeAddressFather.GCState = tacFatherProvince.Value == "" ? null : string.Format("{0}^{1}", Constant.StandardCode.PROVINCE, tacFatherProvince.Value);
            if (tacFatherZipCode.Value == "" || tacFatherZipCode.Value == "0")
                officeAddressFather.ZipCode = null;
            else
                officeAddressFather.ZipCode = Convert.ToInt32(tacFatherZipCode.Value);
            officeAddressFather.PhoneNo1 = txtFatherTelephoneNo.Text;
            #endregion

            #region Mother
            entityMother.GCSalutation = "";
            entityMother.GCSuffix = cboMotherSuffix.Value == null ? "" : cboMotherSuffix.Value.ToString();
            entityMother.GCTitle = cboMotherTitle.Value == null ? "" : cboMotherTitle.Value.ToString();
            entityMother.FirstName = txtMotherFirstName.Text;
            entityMother.MiddleName = txtMotherMiddleName.Text;
            entityMother.LastName = txtMotherLastName.Text;
            entityMother.CityOfBirth = txtMotherBirthPlace.Text;
            entityMother.DateOfBirth = Helper.GetDatePickerValue(txtMotherDOB);

            suffix = cboMotherSuffix.Value == null ? "" : cboMotherSuffix.Text;
            title = cboMotherTitle.Value == null ? "" : cboMotherTitle.Text;
            Name = Helper.GenerateName(entityMother.LastName, entityMother.MiddleName, entityMother.FirstName);
            entityMother.FamilyName = Helper.GenerateFullName(Name, title, suffix);

            entityMother.GCNationality = cboMotherNationality.Value.ToString();
            entityMother.GCReligion = cboMotherReligion.Value.ToString();
            entityMother.GCEducationLevel = cboMotherEducationLevel.Value.ToString();
            #endregion

            #region Office
            entityMother.CompanyName = txtMotherJobOffice.Text;
            entityMother.GCJob = cboMotherGCJob.Value == null ? "" : cboMotherGCJob.Value.ToString();
            entityMother.Occupation = txtMotherOccupation.Text;
            entityMother.Salary = txtMotherSalary.Text == "" ? 0 : Convert.ToDecimal(txtMotherSalary.Text);

            officeAddressMother.StreetName = txtMotherAddress.Text;
            officeAddressMother.County = txtMotherCounty.Text; // Desa
            officeAddressMother.District = txtMotherDistrict.Text; //Kabupaten
            officeAddressMother.City = txtMotherCity.Text;
            officeAddressMother.GCState = tacMotherProvince.Value == "" ? null : string.Format("{0}^{1}", Constant.StandardCode.PROVINCE, tacMotherProvince.Value);
            if (tacMotherZipCode.Value == "" || tacMotherZipCode.Value == "0")
                officeAddressMother.ZipCode = null;
            else
                officeAddressMother.ZipCode = Convert.ToInt32(tacMotherZipCode.Value);
            officeAddressMother.PhoneNo1 = txtMotherTelephoneNo.Text;
            #endregion
        }

        private bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            StudentFamilyDao entityDao = new StudentFamilyDao(ctx);
            AddressDao addressDao = new AddressDao(ctx);
            bool result = true;
            try
            {
                StudentFamily entityFather = new StudentFamily();
                StudentFamily entityMother = new StudentFamily();
                Address officeAddressFather = new Address();
                Address officeAddressMother = new Address();
                ControlToEntity(entityFather, entityMother, officeAddressFather, officeAddressMother);

                entityFather.StudentID = AppSession.StudentID;
                entityFather.GCFamilyRelation = Constant.FamilyRelation.FATHER;
                entityFather.OfficeAddressID = null;
                entityFather.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entityFather);

                entityFather.FamilyID = BusinessLayer.GetProspectiveStudentFamilyMaxID(ctx);
                officeAddressFather.GCAddressType = Constant.AddressType.STUDENT_FAMILY;
                entityFather.OfficeAddressID = officeAddressFather.AddressID = string.Format("{0}{1}", hdnAddressPrefix.Value, entityFather.FamilyID);
                addressDao.Insert(officeAddressFather);
                entityDao.Update(entityFather);

                addressDao.Insert(officeAddressMother);
                entityMother.StudentID = AppSession.StudentID;
                entityMother.GCFamilyRelation = Constant.FamilyRelation.MOTHER;
                entityMother.OfficeAddressID = null;
                entityMother.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entityMother);

                entityMother.FamilyID = BusinessLayer.GetProspectiveStudentFamilyMaxID(ctx);
                officeAddressFather.GCAddressType = Constant.AddressType.STUDENT_FAMILY;
                entityMother.OfficeAddressID = officeAddressMother.AddressID = string.Format("{0}{1}", hdnAddressPrefix.Value, entityMother.FamilyID);
                addressDao.Insert(officeAddressMother);
                entityDao.Update(entityMother);

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
            StudentFamilyDao entityDao = new StudentFamilyDao(ctx);
            AddressDao addressDao = new AddressDao(ctx);
            bool result = true;
            try
            {
                List<StudentFamily> lstEntity = BusinessLayer.GetStudentFamilyList(string.Format("StudentID = {0} AND GCFamilyRelation IN ('{1}','{2}')", AppSession.StudentID, Constant.FamilyRelation.FATHER, Constant.FamilyRelation.MOTHER), ctx);
                StudentFamily entityFather = lstEntity.FirstOrDefault(p => p.GCFamilyRelation == Constant.FamilyRelation.FATHER);
                StudentFamily entityMother = lstEntity.FirstOrDefault(p => p.GCFamilyRelation == Constant.FamilyRelation.MOTHER);

                List<Address> lstOfficeAddress = BusinessLayer.GetAddressList(string.Format("AddressID IN ('{0}','{1}')", entityFather.OfficeAddressID, entityMother.OfficeAddressID), ctx);
                Address officeAddressFather = lstOfficeAddress.FirstOrDefault(p => p.AddressID == entityFather.OfficeAddressID);
                Address officeAddressMother = lstOfficeAddress.FirstOrDefault(p => p.AddressID == entityMother.OfficeAddressID);

                ControlToEntity(entityFather, entityMother, officeAddressFather, officeAddressMother);

                addressDao.Update(officeAddressFather);
                entityFather.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entityFather);

                addressDao.Update(officeAddressMother);
                entityMother.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entityMother);

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

        protected override bool OnCustomButtonClick(string type, ref string errMessage, ref string retval)
        {
            if (hdnIsAdd.Value == "1")
                return OnSaveAddRecord(ref errMessage, ref retval);
            else
                return OnSaveEditRecord(ref errMessage); 
        }
    }
}