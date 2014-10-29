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
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                String filterExpression = String.Format("StudentID = {0}", Convert.ToInt32(ID));
                vStudent entity = BusinessLayer.GetvStudentList(filterExpression)[0];
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
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
        #endregion

        protected override void SetControlProperties()
        {
            String filterExpression = String.Format("ParentID IN ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}') AND IsActive = 1 AND IsDeleted = 0",
                Constant.StandardCode.SALUTATION, Constant.StandardCode.SUFFIX, Constant.StandardCode.TITLE, Constant.StandardCode.GENDER, Constant.StandardCode.RELIGION,
                Constant.StandardCode.NATIONALITY, Constant.StandardCode.SCHOOL_GRADE, Constant.StandardCode.STUDENT_STATUS, Constant.StandardCode.SCHOOL_MAJOR);
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(filterExpression);
            lstStandardCode.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });

            Methods.SetComboBoxField(cboSalutation, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.SALUTATION).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboSuffix, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.SUFFIX).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboTitle, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.TITLE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboNationality, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.NATIONALITY).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboGrade, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.SCHOOL_GRADE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboMajor, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.SCHOOL_MAJOR || x.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboStudentStatus, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.STUDENT_STATUS).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboGender, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.GENDER).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboReligion, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.RELIGION).ToList(), "StandardCodeName", "StandardCodeID");

            hdnAddressPrefix.Value = BusinessLayer.GetStandardCode(Constant.AddressType.STUDENT).TagProperty;
        }

        protected override void OnControlEntrySetting()
        {
            vSite site = BusinessLayer.GetvSiteList(string.Format("SiteID = '{0}'", AppSession.UserLogin.SiteID)).FirstOrDefault();
            string defaultGCState = site.GCState == "" ? "" : site.GCState.Split('^')[1];
            //string defaultPhoneArea = BusinessLayer.GetSettingParameter(Constant.SettingParameter.PHONE_AREA).ParameterValue;

            #region Student Data
            SetControlEntrySetting(cboSalutation, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboTitle, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtFirstName, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtMiddleName, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtLastName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtPreferredName, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboSuffix, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtBirthPlace, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboGender, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboNationality, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboReligion, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtDOB, new ControlEntrySetting(true, true, true, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtAgeInDay, new ControlEntrySetting(false, false, true, 0));
            SetControlEntrySetting(txtAgeInMonth, new ControlEntrySetting(false, false, true, 0));
            SetControlEntrySetting(txtAgeInYear, new ControlEntrySetting(false, false, true, 0));
            #endregion

            #region Patient Address
            SetControlEntrySetting(txtAddress, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtCounty, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtDistrict, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtCity, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(tacProvince, new ControlEntrySetting(true, true, false, new Variable { Code = defaultGCState, Value = site.State }));
            SetControlEntrySetting(tacZipCode, new ControlEntrySetting(true, true, false));
            #endregion

            #region Patient Contact
            SetControlEntrySetting(txtTelephoneNo, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtMobilePhoneNo1, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtMobilePhoneNo2, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtEmailAddress1, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtEmailAddress2, new ControlEntrySetting(true, true, false));
            #endregion

            #region Other Information
            SetControlEntrySetting(cboGrade, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboMajor, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboStudentStatus, new ControlEntrySetting(true, true, true));
            #endregion
        }

        private void EntityToControl(vStudent entity)
        {
            txtStudentCode.Text = entity.StudentCode;
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
            cboStudentStatus.Value = entity.GCStudentStatus;
            cboGrade.Value = entity.GCGrade;
            cboMajor.Value = entity.GCMajor;
            txtRemarks.Text = entity.Remarks;
            #endregion
        }

        private void ControlToEntity(Student entity, Address entityAddress)
        {
            #region Student
            entity.StudentCode = txtStudentCode.Text;
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
            entity.GCStudentStatus = cboStudentStatus.Value.ToString();
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
                entity.SiteID = AppSession.UserLogin.SiteID;
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