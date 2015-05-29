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
using System.Data;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class TeacherEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.TEACHER;
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
        protected string OnGetRoomFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }
        #endregion

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                String filterExpression = String.Format("TeacherID = {0}", Convert.ToInt32(ID));
                vTeacher entity = BusinessLayer.GetvTeacherList(filterExpression)[0];
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            
            txtTeacherCode.Focus();
        }

        protected override void SetControlProperties()
        {
            hdnAddressPrefix.Value = BusinessLayer.GetStandardCode(Constant.AddressType.EMPLOYEE).TagProperty;

            String filterExpression = String.Format("ParentID IN ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}') AND IsDeleted = 0 AND IsActive = 1",
                Constant.StandardCode.SALUTATION, Constant.StandardCode.SUFFIX, Constant.StandardCode.TITLE, Constant.StandardCode.GENDER, 
                Constant.StandardCode.DEPARTMENT, Constant.StandardCode.EMPLOYEE_OCCUPATION, Constant.StandardCode.EMPLOYMENT_STATUS, Constant.StandardCode.EMPLOYEE_OCCUPATION_LEVEL);
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(filterExpression);

            lstStandardCode.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });
            Methods.SetComboBoxField<StandardCode>(cboGCSalutation, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.SALUTATION || x.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboGCSuffix, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.SUFFIX || x.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboGCTitle, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.TITLE || x.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboGender, lstStandardCode.Where(sc => sc.ParentID == Constant.StandardCode.GENDER).ToList(), "StandardCodeName", "StandardCodeID");

            Methods.SetComboBoxField<StandardCode>(cboGCDepartment, lstStandardCode.Where(sc => sc.ParentID == Constant.StandardCode.DEPARTMENT).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboGCOccupation, lstStandardCode.Where(sc => sc.ParentID == Constant.StandardCode.EMPLOYEE_OCCUPATION).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboGCOccupationLevel, lstStandardCode.Where(sc => sc.ParentID == Constant.StandardCode.EMPLOYEE_OCCUPATION_LEVEL || sc.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboGCEmployeeStatus, lstStandardCode.Where(sc => sc.ParentID == Constant.StandardCode.EMPLOYMENT_STATUS).ToList(), "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            vSite site = BusinessLayer.GetvSiteList(string.Format("SiteID = '{0}'", AppSession.UserLogin.SiteID)).FirstOrDefault();
            string defaultGCState = site.GCState == "" ? "" : site.GCState.Split('^')[1];

            #region Personal Data
            SetControlEntrySetting(txtTeacherCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtInitial, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboGCSalutation, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboGCTitle, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtFirstName, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtMiddleName, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtLastName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboGender, new ControlEntrySetting(true, true, true));      
            SetControlEntrySetting(cboGCSuffix, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(tacRoom, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtDOB, new ControlEntrySetting(true, true, true, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtAgeInDay, new ControlEntrySetting(false, false, true, 0));
            SetControlEntrySetting(txtAgeInMonth, new ControlEntrySetting(false, false, true, 0));
            SetControlEntrySetting(txtAgeInYear, new ControlEntrySetting(false, false, true, 0));
            #endregion

            #region Data Karyawan
            SetControlEntrySetting(cboGCDepartment, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboGCOccupation, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboGCOccupationLevel, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtHiredDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtTerminatedDate, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtVATRegistrationNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboGCEmployeeStatus, new ControlEntrySetting(true, true, true));
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
            SetControlEntrySetting(txtTelephoneNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtMobilePhoneNo1, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtMobilePhoneNo2, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtEmailAddress1, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtEmailAddress2, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtOfficeExtension, new ControlEntrySetting(true, true, false));
            #endregion

            #region Inforamsi Lain
            SetControlEntrySetting(txtPictureFileName, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
            #endregion
        }

        private void EntityToControl(vTeacher entity)
        {
            txtTeacherCode.Text = entity.TeacherCode;
            cboGCSalutation.Value = entity.GCSalutation;
            cboGCSuffix.Value = entity.GCSuffix;
            cboGCTitle.Value = entity.GCTitle;
            txtFirstName.Text = entity.FirstName;
            txtMiddleName.Text = entity.MiddleName;
            txtLastName.Text = entity.LastName;
            tacRoom.Value = entity.RoomID.ToString();
            tacRoom.Text = entity.RoomName;
            cboGender.Value = entity.GCGender;
            txtBirthPlace.Text = entity.CityOfBirth;
            txtDOB.Text = entity.DateOfBirth.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtRemarks.Text = entity.Remarks;

            #region Data Karyawan
            cboGCDepartment.Value = entity.GCDepartment;
            cboGCOccupation.Value = entity.GCOccupation;
            cboGCOccupationLevel.Value = entity.GCOccupationLevel;
            txtVATRegistrationNo.Text = entity.VATRegistrationNo;
            cboGCEmployeeStatus.Value = entity.GCEmployeeStatus;
            if (entity.HiredDate.ToString("dd-MM-yyyy") != Constant.ConstantDate.DEFAULT_NULL)
                txtHiredDate.Text = entity.HiredDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            if (entity.TerminatedDate.ToString("dd-MM-yyyy") != Constant.ConstantDate.DEFAULT_NULL)
                txtTerminatedDate.Text = entity.TerminatedDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            #endregion

            #region Alamat Karyawan
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
            tacZipCode.Text = entity.ZipCode;
            #endregion

            #region Data Kontak Karyawan
            txtEmailAddress1.Text = entity.EmailAddress1;
            txtEmailAddress2.Text = entity.EmailAddress2;
            txtMobilePhoneNo1.Text = entity.MobilePhoneNo1;
            txtMobilePhoneNo2.Text = entity.MobilePhoneNo2;
            txtOfficeExtension.Text = entity.OfficeExtensionNo;
            txtTelephoneNo.Text = entity.PhoneNo1;
            #endregion

            #region Informasi Lain
            txtPictureFileName.Text = entity.PictureFileName;
            txtRemarks.Text = entity.Remarks;
            #endregion
        }

        private void ControlToEntity(Employee entity, Teacher entityTeacher, Address entityAddress)
        {
            #region Personal Data
            if (cboGCSalutation.Value != null)
                entity.GCSalutation = cboGCSalutation.Value.ToString();
            else
                entity.GCSalutation = null;
            if (cboGCSuffix.Value != null)
                entity.GCSuffix = cboGCSuffix.Value.ToString();
            else
                entity.GCSuffix = null;
            if (cboGCTitle.Value != null)
                entity.GCTitle = cboGCTitle.Value.ToString();
            else
                entity.GCTitle = null;
            entity.FirstName = txtFirstName.Text;
            entity.MiddleName = txtMiddleName.Text;
            entity.LastName = txtLastName.Text;
            if (tacRoom.Value == "" || tacRoom.Value == "0")
                entityTeacher.RoomID = null;
            else
                entityTeacher.RoomID = Convert.ToInt32(tacRoom.Value);
            entity.GCGender = cboGender.Value.ToString();
            entity.CityOfBirth = txtBirthPlace.Text;
            entity.DateOfBirth = Helper.GetDatePickerValue(txtDOB.Text);
            entity.Remarks = txtRemarks.Text;

            string suffix = cboGCSuffix.Value == null ? "" : cboGCSuffix.Text;
            string title = cboGCTitle.Value == null ? "" : cboGCTitle.Text;
            entity.Name = Helper.GenerateName(entity.LastName, entity.MiddleName, entity.FirstName);
            entity.FullName = Helper.GenerateFullName(entity.Name, title, suffix);
            #endregion

            #region Data Karyawan
            entity.GCDepartment = Helper.GetComboBoxValue(cboGCDepartment, true);
            entity.GCOccupation = Helper.GetComboBoxValue(cboGCOccupation, true);
            entity.GCOccupationLevel = Helper.GetComboBoxValue(cboGCOccupationLevel, true);
            entity.VATRegistrationNo = txtVATRegistrationNo.Text;
            entity.GCEmployeeStatus = Helper.GetComboBoxValue(cboGCEmployeeStatus, true);
            if (txtHiredDate.Text != "")
                entity.HiredDate = Helper.GetDatePickerValue(txtHiredDate);
            if (txtTerminatedDate.Text != "")
                entity.TerminatedDate = Helper.GetDatePickerValue(txtTerminatedDate);
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
            entityAddress.PhoneNo1 = txtTelephoneNo.Text;
            #endregion

            #region Contact
            entity.EmailAddress1 = txtEmailAddress1.Text;
            entity.EmailAddress2 = txtEmailAddress2.Text;
            entity.MobilePhoneNo1 = txtMobilePhoneNo1.Text;
            entity.MobilePhoneNo2 = txtMobilePhoneNo2.Text;
            entity.OfficeExtensionNo = txtOfficeExtension.Text;
            #endregion
            
            #region Informasi Lain
            entity.PictureFileName = txtPictureFileName.Text;
            entity.Remarks = txtRemarks.Text;
            #endregion
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            EmployeeDao entityDao = new EmployeeDao(ctx);
            TeacherDao entityTeacherDao = new TeacherDao(ctx);
            AddressDao addressDao = new AddressDao(ctx);
            bool result = false;
            try
            {
                Employee entity = new Employee();
                Teacher entityTeacher = new Teacher();
                Address address = new Address();
                ControlToEntity(entity, entityTeacher, address);
                entity.GCEmployeeType = Constant.EmployeeType.TEACHER;
                entity.EmployeeCode = BusinessLayer.GenerateEmployeeCode(entity.GCDepartment, entity.HiredDate, ctx);
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entity.SiteID = AppSession.UserLogin.SiteID;
                entity.AddressID = null;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);

                entity.EmployeeID = BusinessLayer.GetEmployeeMaxID(ctx);
                address.GCAddressType = Constant.AddressType.EMPLOYEE;
                entity.AddressID = address.AddressID = string.Format("{0}{1}", hdnAddressPrefix.Value, entity.EmployeeID);
                addressDao.Insert(address);

                entityTeacher.TeacherID = entity.EmployeeID;
                entityTeacherDao.Insert(entityTeacher);

                entityDao.Update(entity);

                retval = entity.EmployeeID.ToString();
                ctx.CommitTransaction();
                result = true;
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
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            EmployeeDao entityDao = new EmployeeDao(ctx);
            TeacherDao entityTeacherDao = new TeacherDao(ctx);
            AddressDao addressDao = new AddressDao(ctx);
            try
            {
                Employee entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
                Teacher entityTeacher = entityTeacherDao.Get(entity.EmployeeID);
                Address address = addressDao.Get(entity.AddressID);
                ControlToEntity(entity, entityTeacher, address);

                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                addressDao.Update(address);
                entityTeacherDao.Update(entityTeacher);
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