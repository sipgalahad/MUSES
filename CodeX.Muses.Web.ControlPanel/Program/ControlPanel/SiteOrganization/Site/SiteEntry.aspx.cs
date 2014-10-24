using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class SiteEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.SITE_INFORMATION;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String SiteID = Request.QueryString["id"];
                hdnID.Value = SiteID;
                SetControlProperties();
                Site entity = BusinessLayer.GetSite(SiteID);
                vAddress entityAddress = BusinessLayer.GetvAddressList(string.Format("AddressID = '{0}'", entity.AddressID))[0];
                EntityToControl(entity, entityAddress);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            txtSiteID.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsDeleted = 0", Constant.StandardCode.HEALTHCARE_OPERATING_GROUP));
            Methods.SetComboBoxField<StandardCode>(cboOperatingGroup, lstStandardCode, "StandardCodeName", "StandardCodeID");

            hdnAddressPrefix.Value = BusinessLayer.GetStandardCode(Constant.AddressType.SITE).TagProperty;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtSiteID, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtSiteName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtShortName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtInitial, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboOperatingGroup, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtLicenseNo, new ControlEntrySetting(true, true, true));

            SetControlEntrySetting(txtAddress, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtCounty, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtDistrict, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtCity, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtProvinceCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtProvinceName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(hdnZipCode, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtZipCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtTelephoneNo, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(Site entity, vAddress entityAddress)
        {
            txtSiteID.Text = entity.SiteID;
            txtSiteName.Text = entity.SiteName;
            txtShortName.Text = entity.ShortName;
            txtInitial.Text = entity.Initial;
            cboOperatingGroup.Value = entity.GCOperatingGroup;
            txtLicenseNo.Text = entity.LicenseNo;

            txtAddress.Text = entityAddress.StreetName;
            txtCounty.Text = entityAddress.County; // Desa
            txtDistrict.Text = entityAddress.District; //Kabupaten
            txtCity.Text = entityAddress.City;
            if (entityAddress.GCState != "")
                txtProvinceCode.Text = entityAddress.GCState.Split('^')[1];
            else
                txtProvinceCode.Text = "";
            txtProvinceName.Text = entityAddress.State;
            hdnZipCode.Value = entityAddress.ZipCodeID.ToString();
            txtZipCode.Text = entityAddress.ZipCode;
            txtTelephoneNo.Text = entityAddress.PhoneNo1;
        }

        private void ControlToEntity(Site entity, Address entityAddress)
        {
            entity.SiteName = txtSiteName.Text;
            entity.ShortName = txtShortName.Text;
            entity.Initial = txtInitial.Text;
            entity.GCOperatingGroup = cboOperatingGroup.Value.ToString();
            entity.LicenseNo = txtLicenseNo.Text;

            entityAddress.StreetName = txtAddress.Text;
            entityAddress.County = txtCounty.Text; // Desa
            entityAddress.District = txtDistrict.Text; //Kabupaten
            entityAddress.City = txtCity.Text;
            entityAddress.GCState = txtProvinceCode.Text == "" ? null : string.Format("{0}^{1}", Constant.StandardCode.PROVINCE, txtProvinceCode.Text);
            if (hdnZipCode.Value == "" || hdnZipCode.Value == "0")
                entityAddress.ZipCode = null;
            else
                entityAddress.ZipCode = Convert.ToInt32(hdnZipCode.Value);
            entityAddress.PhoneNo1 = txtTelephoneNo.Text;
        }

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SiteDao entityDao = new SiteDao(ctx);
            AddressDao entityAddressDao = new AddressDao(ctx);
            try
            {
                Site entity = entityDao.Get(hdnID.Value);
                Address entityAddress = entityAddressDao.Get(entity.AddressID);
                ControlToEntity(entity, entityAddress);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;

                entityAddressDao.Update(entityAddress);
                entityDao.Update(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
    }
}