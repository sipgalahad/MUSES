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

                List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("StandardCodeID IN ('{0}','{1}')", Constant.AddressType.PROSPECTIVE_STUDENT_FAMILY, Constant.AddressType.PROSPECTIVE_STUDENT_FAMILY_OFFICE));
                hdnHomeAddressPrefix.Value = lstSc.FirstOrDefault(p => p.StandardCodeID == Constant.AddressType.PROSPECTIVE_STUDENT_FAMILY).TagProperty;
                hdnOfficeAddressPrefix.Value = lstSc.FirstOrDefault(p => p.StandardCodeID == Constant.AddressType.PROSPECTIVE_STUDENT_FAMILY_OFFICE).TagProperty;

                String ID = Request.QueryString["id"];
                vRegistration registration = BusinessLayer.GetvRegistrationList(string.Format("RegistrationID = {0}", ID)).FirstOrDefault();
                hdnID.Value = registration.ProspectiveStudentID.ToString();
                hdnStudentAddressID.Value = registration.AddressID.ToString();
                hdnStudentStreet.Value = registration.StreetName;
                hdnStudentCounty.Value = registration.County;
                hdnStudentDistrict.Value = registration.District;
                hdnStudentCity.Value = registration.City;
                hdnStudentGCProvince.Value = registration.GCState;
                hdnStudentProvince.Value = registration.State;
                hdnStudentTelephoneNo.Value = registration.PhoneNo1;
                hdnStudentZipCodeID.Value = registration.ZipCodeID.ToString();
                hdnStudentZipCode.Value = registration.ZipCode;
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

                #region Trustee Address
                Helper.SetControlEntrySetting(chkIsTrusteeAddressSameWithStudent, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(txtTrusteeAddress, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(txtTrusteeCounty, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(txtTrusteeDistrict, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(txtTrusteeCity, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(tacTrusteeProvince, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(tacTrusteeZipCode, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(txtTrusteeTelephoneNo, new ControlEntrySetting(true, true, false), "mpTrx");
                #endregion

                #region Trustee Company
                Helper.SetControlEntrySetting(txtTrusteeJobOffice, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(cboTrusteeGCJob, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(txtTrusteeOccupation, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(txtTrusteeSalary, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(txtTrusteeOfficeAddress, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(txtTrusteeOfficeCounty, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(txtTrusteeOfficeDistrict, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(txtTrusteeOfficeCity, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(tacTrusteeOfficeProvince, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(tacTrusteeOfficeZipCode, new ControlEntrySetting(true, true, false), "mpTrx");
                Helper.SetControlEntrySetting(txtTrusteeOfficeTelephoneNo, new ControlEntrySetting(true, true, false), "mpTrx");
                #endregion
            }
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = string.Format("ProspectiveStudentID = {0} AND IsStudentTrustee = 1 AND IsDeleted = 0", hdnID.Value);
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

        private void ControlToEntity(ProspectiveStudentFamily entity, Address homeAddress, Address officeAddress)
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

            #region Home
            entity.IsHomeAddressSameWithStudent = chkIsTrusteeAddressSameWithStudent.Checked;
            if (!entity.IsHomeAddressSameWithStudent)
            {
                homeAddress.StreetName = txtTrusteeAddress.Text;
                homeAddress.County = txtTrusteeCounty.Text; // Desa
                homeAddress.District = txtTrusteeDistrict.Text; //Kabupaten
                homeAddress.City = txtTrusteeCity.Text;
                homeAddress.GCState = tacTrusteeProvince.Value == "" ? null : string.Format("{0}^{1}", Constant.StandardCode.PROVINCE, tacTrusteeProvince.Value);
                if (tacTrusteeZipCode.Value == "" || tacTrusteeZipCode.Value == "0")
                    homeAddress.ZipCode = null;
                else
                    homeAddress.ZipCode = Convert.ToInt32(tacTrusteeZipCode.Value);
                homeAddress.PhoneNo1 = txtTrusteeTelephoneNo.Text;
            }
            #endregion

            #region Office
            entity.CompanyName = txtTrusteeJobOffice.Text;
            entity.GCJob = cboTrusteeGCJob.Value == null ? "" : cboTrusteeGCJob.Value.ToString();
            entity.Occupation = txtTrusteeOccupation.Text;
            entity.Salary = txtTrusteeSalary.Text == "" ? 0 : Convert.ToDecimal(txtTrusteeSalary.Text);

            officeAddress.StreetName = txtTrusteeOfficeAddress.Text;
            officeAddress.County = txtTrusteeOfficeCounty.Text; // Desa
            officeAddress.District = txtTrusteeOfficeDistrict.Text; //Kabupaten
            officeAddress.City = txtTrusteeOfficeCity.Text;
            officeAddress.GCState = tacTrusteeProvince.Value == "" ? null : string.Format("{0}^{1}", Constant.StandardCode.PROVINCE, tacTrusteeProvince.Value);
            if (tacTrusteeZipCode.Value == "" || tacTrusteeZipCode.Value == "0")
                officeAddress.ZipCode = null;
            else
                officeAddress.ZipCode = Convert.ToInt32(tacTrusteeZipCode.Value);
            officeAddress.PhoneNo1 = txtTrusteeOfficeTelephoneNo.Text;
            #endregion
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            ProspectiveStudentFamilyDao entityDao = new ProspectiveStudentFamilyDao(ctx);
            AddressDao addressDao = new AddressDao(ctx);
            bool result = true;
            try
            {
                ProspectiveStudentFamily entity = new ProspectiveStudentFamily();
                Address homeAddress = new Address();
                Address officeAddress = new Address();
                ControlToEntity(entity, homeAddress, officeAddress);
                entity.IsStudentTrustee = true;
                entity.ProspectiveStudentID = Convert.ToInt32(hdnID.Value);
                entity.OfficeAddressID = null;
                entity.IsDeleted = false;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);

                entity.FamilyID = BusinessLayer.GetProspectiveStudentFamilyMaxID(ctx);

                homeAddress.GCAddressType = Constant.AddressType.PROSPECTIVE_STUDENT_FAMILY;
                homeAddress.AddressID = string.Format("{0}{1}", hdnHomeAddressPrefix.Value, entity.FamilyID);
                if (entity.IsHomeAddressSameWithStudent)
                    entity.HomeAddressID = hdnStudentAddressID.Value;
                else
                    entity.HomeAddressID = homeAddress.AddressID;
                addressDao.Insert(homeAddress);

                officeAddress.GCAddressType = Constant.AddressType.PROSPECTIVE_STUDENT_FAMILY_OFFICE;
                entity.OfficeAddressID = officeAddress.AddressID = string.Format("{0}{1}", hdnOfficeAddressPrefix.Value, entity.FamilyID);
                addressDao.Insert(officeAddress);

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

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            ProspectiveStudentFamilyDao entityDao = new ProspectiveStudentFamilyDao(ctx);
            AddressDao addressDao = new AddressDao(ctx);
            bool result = true;
            try
            {
                ProspectiveStudentFamily entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                string homeAddressID = string.Format("{0}{1}", hdnHomeAddressPrefix.Value, entity.FamilyID);
                List<Address> lstOfficeAddress = BusinessLayer.GetAddressList(string.Format("AddressID IN ('{0}','{1}')", homeAddressID, entity.OfficeAddressID), ctx);
                Address homeAddress = lstOfficeAddress.FirstOrDefault(p => p.AddressID == homeAddressID);
                Address officeAddress = lstOfficeAddress.FirstOrDefault(p => p.AddressID == entity.OfficeAddressID);
                ControlToEntity(entity, homeAddress, officeAddress);

                if (entity.IsHomeAddressSameWithStudent)
                    entity.HomeAddressID = hdnStudentAddressID.Value;
                else
                    entity.HomeAddressID = homeAddress.AddressID;
                addressDao.Update(homeAddress);
                addressDao.Update(officeAddress);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
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