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

        protected override void SetControlProperties()
        {
            String filterExpression = String.Format("ParentID IN ('{0}','{1}','{2}','{3}','{4}','{5}','{6}') AND IsActive = 1 AND IsDeleted = 0",
                Constant.StandardCode.SALUTATION, Constant.StandardCode.SUFFIX, Constant.StandardCode.TITLE,
                Constant.StandardCode.NATIONALITY, Constant.StandardCode.SCHOOL_GRADE, Constant.StandardCode.STUDENT_STATUS, Constant.StandardCode.SCHOOL_MAJOR);
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(filterExpression);

            Methods.SetComboBoxField(cboGCSalutation, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.SALUTATION).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboGCSuffix, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.SUFFIX).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboGCTitle, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.TITLE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboGCNationality, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.NATIONALITY).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboGCGrade, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.SCHOOL_GRADE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboGCMajor, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.SCHOOL_MAJOR).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboGCStudentStatus, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.STUDENT_STATUS).ToList(), "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtStudentCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(cboGCStudentStatus, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtLastName, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(vStudent entity)
        {
            txtStudentCode.Text = entity.StudentCode;
            cboGCSalutation.Value = entity.GCSalutation;
            cboGCSuffix.Value = entity.GCSuffix;
            cboGCStudentStatus.Value = entity.GCStudentStatus;
            cboGCTitle.Value = entity.GCTitle;
            txtFirstName.Text = entity.FirstName;
            txtMiddleName.Text = entity.MiddleName;
            txtLastName.Text = entity.LastName;
            txtPreferredName.Text = entity.PreferredName;
            txtBirthPlace.Text = entity.CityOfBirth;
            txtDOB.Text = entity.DateOfBirth.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            cboGCNationality.Value = entity.GCNationality;
            cboGCGrade.Value = entity.GCGrade;
            cboGCMajor.Value = entity.GCMajor;
            txtEmailAddress1.Text = entity.EmailAddress1;
            txtEmailAddress2.Text = entity.EmailAddress2;
            txtMobilePhoneNo1.Text = entity.MobilePhoneNo1;
            txtMobilePhoneNo2.Text = entity.MobilePhoneNo2;
            txtPictureFileName.Text = entity.PictureFileName;
            txtRemarks.Text = entity.Remarks;

            #region Address
            txtAddress.Text = entity.StreetName;
            txtCounty.Text = entity.County; // Desa
            txtDistrict.Text = entity.District; //Kabupaten
            txtCity.Text = entity.City;
            List<String> lstProvince = entity.GCState.Split('^').ToList();
            if (lstProvince.Count > 1)
            {
                txtProvinceCode.Text = lstProvince[0];
                txtProvinceName.Text = lstProvince[1];
            }
            tacZipCode.Value = entity.ZipCodeID.ToString();
            tacZipCode.Text = entity.ZipCode.ToString();
            #endregion
        }

        private void ControlToEntity(Student entity, Address entityAddress)
        {
            #region Student
            entity.StudentCode = txtStudentCode.Text;
            entity.GCSalutation = cboGCSalutation.Value.ToString();
            entity.GCSuffix = cboGCSuffix.Value.ToString();
            entity.GCStudentStatus = cboGCStudentStatus.Value.ToString();
            entity.GCTitle = cboGCTitle.Value.ToString();
            entity.FirstName = txtFirstName.Text;
            entity.MiddleName = txtMiddleName.Text;
            entity.LastName = txtLastName.Text;
            entity.PreferredName = txtPreferredName.Text;
            entity.CityOfBirth = txtBirthPlace.Text;

            entity.DateOfBirth = Helper.GetDatePickerValue(txtDOB.Text);
            entity.GCNationality = cboGCNationality.Value.ToString();
            entity.GCGrade = cboGCGrade.Value.ToString();
            entity.GCMajor = cboGCMajor.Value.ToString();
            
            entity.EmailAddress1 = txtEmailAddress1.Text;
            entity.EmailAddress2 = txtEmailAddress2.Text;
            entity.MobilePhoneNo1 = txtMobilePhoneNo1.Text;
            entity.MobilePhoneNo2 = txtMobilePhoneNo2.Text;
            entity.PictureFileName = txtPictureFileName.Text;
            entity.Remarks = txtRemarks.Text;
            #endregion

            #region Address

            entityAddress.StreetName = txtAddress.Text;
            entityAddress.County = txtCounty.Text; // Desa
            entityAddress.District = txtDistrict.Text; //Kabupaten
            entityAddress.City = txtCity.Text;
            entityAddress.GCState = txtProvinceCode.Text == "" ? null : string.Format("{0}^{1}", Constant.StandardCode.PROVINCE, txtProvinceCode.Text);
            if (tacZipCode.Value == "" || tacZipCode.Value == "0")
                entityAddress.ZipCode = null;
            else
                entityAddress.ZipCode = Convert.ToInt32(tacZipCode.Value);
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
            bool result = false;
            try
            {
                Student entity = new Student();
                Address address = new Address();
                ControlToEntity(entity,address);
                addressDao.Insert(address);
                entity.AddressID = BusinessLayer.GetAddressMaxID(ctx);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetStudentMaxID(ctx).ToString();
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
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
            }
            catch (Exception ex)
            {
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