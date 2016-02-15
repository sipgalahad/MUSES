using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Common;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class StudentEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.STUDENT;
        }

        protected override void InitializeDataControl()
        {
            String[] param = Request.QueryString["id"].Split('|');
            if (param[0] == "edit")
            {
                IsAdd = false;
                String ID = param[1];
                hdnID.Value = ID;
                String filterExpression = String.Format("StudentID = {0}", Convert.ToInt32(ID));
                vStudent entity = BusinessLayer.GetvStudentList(filterExpression)[0];
                hdnSiteID.Value = entity.SiteID;
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                hdnSiteID.Value = param[1];
                SetControlProperties();
                IsAdd = true;
            }
            
            txtStudentCode.Focus();
        }

        #region Html Getter
        protected string OnGetProvinceFilterExpression()
        {
            return string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PROVINCE);
        }
        protected string OnGetReligionCatholic()
        {
            return Constant.Religion.CATHOLIC;
        }
        #endregion

        protected override void SetControlProperties()
        {
            String GCSchoolType = BusinessLayer.GetSiteParameter(hdnSiteID.Value, Constant.SiteParameter.SCHOOL_TYPE).ParameterValue;

            String filterExpression = String.Format("ParentID IN ('{0}','{1}','{2}','{3}','{4}','{5}','{6}') AND IsActive = 1 AND IsDeleted = 0",
                Constant.StandardCode.SALUTATION, Constant.StandardCode.SUFFIX, Constant.StandardCode.TITLE, Constant.StandardCode.GENDER, Constant.StandardCode.RELIGION,
                Constant.StandardCode.NATIONALITY, Constant.StandardCode.STUDENT_STATUS);
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(filterExpression);
            lstStandardCode.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });

            Methods.SetComboBoxField(cboSalutation, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.SALUTATION).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboSuffix, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.SUFFIX).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboTitle, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.TITLE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboNationality, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.NATIONALITY).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboStudentStatus, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.STUDENT_STATUS).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboGender, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.GENDER).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboReligion, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.RELIGION).ToList(), "StandardCodeName", "StandardCodeID");

            List<vSchoolGrade> lstGrade = BusinessLayer.GetvSchoolGradeList(string.Format("GCSchoolType = '{0}' ORDER BY DisplayOrder", GCSchoolType));
            List<vSchoolMajor> lstMajor = BusinessLayer.GetvSchoolMajorList(string.Format("GCSchoolType = '{0}'", GCSchoolType));
            lstMajor.Add(new vSchoolMajor { GCMajor = "", Major = "" });
            Methods.SetComboBoxField(cboGrade, lstGrade, "Grade", "GCGrade");
            Methods.SetComboBoxField(cboMajor, lstMajor, "Major", "GCMajor");

            hdnAddressPrefix.Value = BusinessLayer.GetStandardCode(Constant.AddressType.STUDENT).TagProperty;

            hdnSchoolType.Value = BusinessLayer.GetSettingParameter(Constant.SettingParameter.SCHOOL_TYPE).ParameterValue;
            if (hdnSchoolType.Value != Constant.SchoolType.KATOLIK)
            {
                trDateOfBaptism.Style.Add("display", "none");
                trPlaceOfBaptism.Style.Add("display", "none");
            }
        }

        protected override void OnControlEntrySetting()
        {
            vSite site = BusinessLayer.GetvSiteList(string.Format("SiteID = '{0}'", AppSession.UserLogin.SiteID)).FirstOrDefault();
            string defaultGCState = site.GCState == "" ? "" : site.GCState.Split('^')[1];
            //string defaultPhoneArea = BusinessLayer.GetSettingParameter(Constant.SettingParameter.PHONE_AREA).ParameterValue;

            #region Student Data
            SetControlEntrySetting(txtStudentCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtNationalStudentNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboSalutation, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboTitle, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtFirstName, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtMiddleName, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtLastName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtPreferredName, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboSuffix, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtBirthPlace, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboGender, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboNationality, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboReligion, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtDOB, new ControlEntrySetting(true, true, true, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtPlaceOfBaptism, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtDateOfBaptism, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtAgeInDay, new ControlEntrySetting(false, false, true, 0));
            SetControlEntrySetting(txtAgeInMonth, new ControlEntrySetting(false, false, true, 0));
            SetControlEntrySetting(txtAgeInYear, new ControlEntrySetting(false, false, true, 0));
            SetControlEntrySetting(chkIsFeeder, new ControlEntrySetting(true, true, false));
            #endregion

            #region Address
            SetControlEntrySetting(txtAddress, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtCounty, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtDistrict, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtCity, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(tacProvince, new ControlEntrySetting(true, true, false, new Variable { Code = defaultGCState, Value = site.State }));
            SetControlEntrySetting(tacZipCode, new ControlEntrySetting(true, true, false));
            #endregion

            #region Contact
            SetControlEntrySetting(txtTelephoneNo, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtMobilePhoneNo1, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtMobilePhoneNo2, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtEmailAddress1, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtEmailAddress2, new ControlEntrySetting(true, true, false));
            #endregion

            #region Other Information
            SetControlEntrySetting(txtVirtualAccountNo, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(cboGrade, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboMajor, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboStudentStatus, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtDropOutDate, new ControlEntrySetting(true, true, false));
            #endregion
        }

        private void EntityToControl(vStudent entity)
        {
            txtStudentCode.Text = entity.StudentCode;
            txtNationalStudentNo.Text = entity.NationalStudentNo;
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
            if (entity.GCReligion == Constant.Religion.CATHOLIC)
            {
                txtPlaceOfBaptism.Text = entity.PlaceOfBaptism;
                txtDateOfBaptism.Text = entity.DateOfBaptism.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            }
            txtAgeInYear.Text = entity.AgeInYear.ToString();
            txtAgeInMonth.Text = entity.AgeInMonth.ToString();
            txtAgeInDay.Text = entity.AgeInDay.ToString();
            chkIsFeeder.Checked = entity.IsFeeder;

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

            #region Additional Information
            txtVirtualAccountNo.Text = entity.VirtualAccountNo;
            cboStudentStatus.Value = entity.GCStudentStatus;
            txtDropOutDate.Text = entity.DropOutDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            cboGrade.Value = entity.GCGrade;
            cboMajor.Value = entity.GCMajor;
            txtRemarks.Text = entity.Remarks;
            #endregion
        }

        private void ControlToEntity(Student entity, Address entityAddress)
        {
            #region Student
            entity.StudentCode = txtStudentCode.Text;
            entity.NationalStudentNo = txtNationalStudentNo.Text;
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
            entity.StudentName = Helper.GenerateFullName(entity.Name, title, suffix);

            entity.DateOfBirth = Helper.GetDatePickerValue(txtDOB.Text);
            entity.GCNationality = cboNationality.Value.ToString();
            entity.GCReligion = cboReligion.Value.ToString();
            if (entity.GCReligion == Constant.Religion.CATHOLIC)
            {
                entity.PlaceOfBaptism = Request.Form[txtPlaceOfBaptism.UniqueID];
                entity.DateOfBaptism = Helper.GetDatePickerValue(Request.Form[txtDateOfBaptism.UniqueID]);
            }
            else
            {
                entity.PlaceOfBaptism = "";
                entity.DateOfBaptism = Helper.InitializeDateTimeNull();
            }
            entity.IsFeeder = chkIsFeeder.Checked;
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

            #region Additional Information
            entity.VirtualAccountNo = txtVirtualAccountNo.Text;
            entity.GCStudentStatus = cboStudentStatus.Value.ToString();
            entity.DropOutDate = Helper.GetDatePickerValue(Request.Form[txtDropOutDate.UniqueID]);
            entity.GCGrade = cboGrade.Value.ToString();
            if (cboMajor.Value != null)
                entity.GCMajor = cboMajor.Value.ToString();
            else
                entity.GCMajor = "";
            entity.Remarks = txtRemarks.Text;
            #endregion
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("StudentCode = '{0}'", txtStudentCode.Text);
            List<Student> lst = BusinessLayer.GetStudentList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Student with Code " + txtStudentCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            Int32 ID = Convert.ToInt32(hdnID.Value);
            string FilterExpression = string.Format("StudentCode = '{0}' AND StudentID != {1}", txtStudentCode.Text, ID);
            List<Student> lst = BusinessLayer.GetStudentList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Student with Code " + txtStudentCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            StudentDao entityDao = new StudentDao(ctx);
            AddressDao addressDao = new AddressDao(ctx);
            bool result = true;
            try
            {
                Student entity = new Student();
                Address address = new Address();
                ControlToEntity(entity, address);
                entity.PictureFileName = string.Format("{0}.jpg", entity.StudentCode);
                entity.SiteID = hdnSiteID.Value;
                entity.AddressID = null;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);

                entity.StudentID = BusinessLayer.GetStudentMaxID(ctx);
                address.GCAddressType = Constant.AddressType.STUDENT;
                entity.AddressID = address.AddressID = string.Format("{0}{1}", hdnAddressPrefix.Value, entity.StudentID);
                addressDao.Insert(address);

                entityDao.Update(entity);
                
                retval = entity.StudentID.ToString();
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

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            StudentDao entityDao = new StudentDao(ctx);
            AddressDao addressDao = new AddressDao(ctx);
            bool result = true;
            try
            {
                Student entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
                Address address = addressDao.Get(entity.AddressID);
                ControlToEntity(entity, address);
                
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                addressDao.Update(address);
                entityDao.Update(entity);
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
    }
}