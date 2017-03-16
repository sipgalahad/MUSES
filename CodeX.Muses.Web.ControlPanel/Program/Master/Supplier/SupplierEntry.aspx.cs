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
using System.Web.UI.HtmlControls;
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class SupplierEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.SUPPLIER;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                int BusinessPartnerID = Convert.ToInt32(ID);
                BusinessPartners entity = BusinessLayer.GetBusinessPartners(BusinessPartnerID);
                vSupplier entitySup = BusinessLayer.GetvSupplierList(string.Format("BusinessPartnerID = {0}", BusinessPartnerID)).FirstOrDefault();
                BusinessPartnerTagField entityTagField = BusinessLayer.GetBusinessPartnerTagField(BusinessPartnerID);
                vAddress entityAddress = BusinessLayer.GetvAddressList(string.Format("AddressID = '{0}'", entity.AddressID))[0];

                SetControlProperties();
                EntityToControl(entity, entitySup, entityAddress, entityTagField);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            ctlEntityCode.InitializeMasterCodingControl(Constant.MasterCode.SUPPLIER);
            ctlEntityCode.SetControlVisibility(IsAdd);
            ctlEntityCode.SetFocus();
        }

        public override void OnAddRecord()
        {
            ctlEntityCode.SetControlVisibility(true);
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SUPPLIER_TYPE, Constant.StandardCode.BANK));
            Methods.SetComboBoxField<StandardCode>(cboSupplierType, lstSc, "StandardCodeName", "StandardCodeID");

            List<Term> listTerm = BusinessLayer.GetTermList("IsDeleted = 0");
            listTerm.Insert(0, new Term { TermID = 0, TermName = "" });
            Methods.SetComboBoxField<Term>(cboTerm, listTerm, "TermName", "TermID");

            lstSc.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });
            Methods.SetComboBoxField<StandardCode>(cboBank, lstSc.Where(p => p.StandardCodeID == "" || p.ParentID == Constant.StandardCode.BANK).ToList(), "StandardCodeName", "StandardCodeID");

            List<Site> listSite = BusinessLayer.GetSiteList("");
            listSite.Insert(0, new Site { SiteID = "", SiteName = "" });
            Methods.SetComboBoxField<Site>(cboSite, listSite, "SiteName", "SiteID");

            hdnAddressPrefix.Value = BusinessLayer.GetStandardCode(Constant.AddressType.BUSINESS_PARTNER).TagProperty;
        }

        protected override void OnControlEntrySetting()
        {
            #region General Information
            SetControlEntrySetting(txtSupplierName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtShortName, new ControlEntrySetting(true, true, false));
            #endregion

            #region Supplier Information
            SetControlEntrySetting(cboSite, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtVATRegistrationNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboTerm, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtMaxPOAmount, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(chkIsSetMaxPOItem, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtMaxPOItem, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(txtLeadTime, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(hdnSupplierLineID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSupplierLineCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtSupplierLineName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(cboSupplierType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(chkIsLineAmountRounded, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtLineAmountRoundedFormat, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(chkIsTotalAmountRounded, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtTotalAmountRoundedFormat, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(txtBankReferenceNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboBank, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtBankAccountHolder, new ControlEntrySetting(true, true, false));
            #endregion

            #region Supplier Status
            SetControlEntrySetting(chkIsBlacklist, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(chkIsPaymentHold, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(chkIsTaxable, new ControlEntrySetting(true, true, false));
            #endregion

            #region Address
            SetControlEntrySetting(txtContactPerson, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtContactPersonMobilePhoneNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtEmailAddress1, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtEmailAddress2, new ControlEntrySetting(true, true, false));
            #endregion

            #region Address
            SetControlEntrySetting(txtAddress, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtCounty, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtDistrict, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtCity, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtProvinceCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtProvinceName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(hdnZipCode, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtZipCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtTelephoneNo, new ControlEntrySetting(true, true, false));
            #endregion

            #region Other Information
            SetControlEntrySetting(txtNotes, new ControlEntrySetting(true, true, false));
            #endregion
        }

        protected override void OnReInitControl()
        {
            #region Custom Attribute
            foreach (RepeaterItem item in rptCustomAttribute.Items)
            {
                TextBox txt = (TextBox)item.FindControl("txtTagField");
                txt.Text = "";
            }
            #endregion
        }

        private void EntityToControl(BusinessPartners entity, vSupplier entitySup, vAddress entityAddress, BusinessPartnerTagField entityTagField)
        {
            #region General Information
            ctlEntityCode.SetText(entity.BusinessPartnerCode);
            txtSupplierName.Text = entity.BusinessPartnerName;
            txtShortName.Text = entity.ShortName;
            #endregion

            #region Supplier Information
            cboSite.Value = entity.SiteID;
            txtVATRegistrationNo.Text = entity.VATRegistrationNo;
            cboTerm.Value = entity.TermID.ToString();
            txtMaxPOAmount.Text = entitySup.MaxPOAmount.ToString();
            chkIsSetMaxPOItem.Checked = entitySup.MaxPOItem > 0;
            txtMaxPOItem.Text = entitySup.MaxPOItem.ToString();
            txtLeadTime.Text = entitySup.LeadTime.ToString();
            hdnSupplierLineID.Value = entitySup.SupplierLineID.ToString();
            txtSupplierLineCode.Text = entitySup.SupplierLineCode;
            txtSupplierLineName.Text = entitySup.SupplierLineName;
            cboSupplierType.Value = entitySup.GCSupplierType;
            txtLineAmountRoundedFormat.Text = entitySup.LineAmountRoundedFormat.ToString();
            chkIsLineAmountRounded.Checked = entitySup.IsLineAmountRounded;
            txtTotalAmountRoundedFormat.Text = entitySup.TotalAmountRoundedFormat.ToString();
            chkIsTotalAmountRounded.Checked = entitySup.IsTotalAmountRounded;
            txtBankAccountHolder.Text = entitySup.BankAccountHolder;
            cboBank.Value = entitySup.GCBank;
            txtBankReferenceNo.Text = entitySup.BankReferenceNo;
            #endregion

            #region Supplier Status
            chkIsBlacklist.Checked = entity.IsBlackList;
            chkIsPaymentHold.Checked = entitySup.IsPaymentHold;
            chkIsTaxable.Checked = entity.IsTaxable;
            #endregion

            #region Address
            txtContactPerson.Text = entity.ContactPerson;
            txtContactPersonMobilePhoneNo.Text = entity.ContactPersonMobilePhoneNo;
            txtEmailAddress1.Text = entity.EmailAddress1;
            txtEmailAddress2.Text = entity.EmailAddress2;
            #endregion

            #region Address
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
            #endregion

            #region Other Information
            txtNotes.Text = entity.Remarks;
            #endregion

            #region Custom Attribute
            foreach (RepeaterItem item in rptCustomAttribute.Items)
            {
                TextBox txt = (TextBox)item.FindControl("txtTagField");
                HtmlInputHidden hdn = (HtmlInputHidden)item.FindControl("hdnTagFieldCode");
                txt.Text = entityTagField.GetType().GetProperty("TagField" + hdn.Value).GetValue(entityTagField, null).ToString();
            }
            #endregion
        }

        private void ControlToEntity(IDbContext ctx, BusinessPartners entity, Supplier entitySup, Address entityAddress, BusinessPartnerTagField entityTagField)
        {
            #region General Information
            entity.BusinessPartnerName = txtSupplierName.Text;
            entity.ShortName = txtShortName.Text;
            #endregion

            #region Supplier Information
            if (cboSite.Value != null && cboSite.Value.ToString() != "")
                entity.SiteID = cboSite.Value.ToString();
            else
                entity.SiteID = null;
            entity.VATRegistrationNo = txtVATRegistrationNo.Text;

            if (cboTerm.Value != null && cboTerm.Value.ToString() != "0")
                entity.TermID = Convert.ToInt32(cboTerm.Value);
            else
                entity.TermID = null;
            entitySup.MaxPOAmount = Convert.ToDecimal(txtMaxPOAmount.Text);
            if (chkIsSetMaxPOItem.Checked)
                entitySup.MaxPOItem = Convert.ToInt32(Request.Form[txtMaxPOItem.UniqueID]);
            else
                entitySup.MaxPOItem = 0;
            entitySup.LeadTime = Convert.ToInt16(txtLeadTime.Text);
            entitySup.LeadTime = Convert.ToInt16(txtLeadTime.Text);

            entitySup.GCSupplierType = cboSupplierType.Value.ToString();
            if (hdnSupplierLineID.Value == "" || hdnSupplierLineID.Value == "0")
                entitySup.SupplierLineID = null;
            else
                entitySup.SupplierLineID = Convert.ToInt32(hdnSupplierLineID.Value);
            entitySup.IsLineAmountRounded = chkIsLineAmountRounded.Checked;
            if (entitySup.IsLineAmountRounded)
                entitySup.LineAmountRoundedFormat = Convert.ToDecimal(txtLineAmountRoundedFormat.Text);
            else
                entitySup.LineAmountRoundedFormat = 0;
            entitySup.IsTotalAmountRounded = chkIsTotalAmountRounded.Checked;
            if (entitySup.IsTotalAmountRounded)
                entitySup.TotalAmountRoundedFormat = Convert.ToDecimal(txtTotalAmountRoundedFormat.Text);
            else
                entitySup.TotalAmountRoundedFormat = 0;
            entitySup.BankAccountHolder = txtBankAccountHolder.Text;
            if (cboBank.Value != null && cboBank.Value.ToString() != "")
                entitySup.GCBank = cboBank.Value.ToString();
            else
                entitySup.GCBank = null;
            entitySup.BankReferenceNo = txtBankReferenceNo.Text;
            #endregion

            #region Supplier Status
            entity.IsBlackList = chkIsBlacklist.Checked;
            entitySup.IsPaymentHold = chkIsPaymentHold.Checked;
            entity.IsTaxable = chkIsTaxable.Checked;
            #endregion

            #region Address
            entity.ContactPerson = txtContactPerson.Text;
            entity.ContactPersonMobilePhoneNo = txtContactPersonMobilePhoneNo.Text;
            entity.EmailAddress1 = txtEmailAddress1.Text;
            entity.EmailAddress2 = txtEmailAddress2.Text;
            #endregion

            #region Address
            entityAddress.StreetName = txtAddress.Text;
            entityAddress.County = txtCounty.Text; // Desa
            entityAddress.District = txtDistrict.Text; //Kabupaten
            entityAddress.City = txtCity.Text;
            entityAddress.GCState = txtProvinceCode.Text == "" ? null : string.Format("{0}^{1}", Constant.StandardCode.PROVINCE, txtProvinceCode.Text);
            if (hdnZipCode.Value == "")
                entityAddress.ZipCode = null;
            else
                entityAddress.ZipCode = Convert.ToInt32(hdnZipCode.Value);
            entityAddress.PhoneNo1 = txtTelephoneNo.Text;
            #endregion

            #region Other Information
            entity.Remarks = txtNotes.Text;
            #endregion

            #region Custom Attribute
            foreach (RepeaterItem item in rptCustomAttribute.Items)
            {
                TextBox txt = (TextBox)item.FindControl("txtTagField");
                HtmlInputHidden hdn = (HtmlInputHidden)item.FindControl("hdnTagFieldCode");
                entityTagField.GetType().GetProperty("TagField" + hdn.Value).SetValue(entityTagField, txt.Text, null);
            }
            #endregion

            entity.BusinessPartnerCode = ctlEntityCode.GetCode(entity.BusinessPartnerName, ctx);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            BusinessPartnersDao entityDao = new BusinessPartnersDao(ctx);
            SupplierDao entitySupDao = new SupplierDao(ctx);
            AddressDao entityAddressDao = new AddressDao(ctx);
            BusinessPartnerTagFieldDao entityTagFieldDao = new BusinessPartnerTagFieldDao(ctx);
            try
            {
                BusinessPartners entity = new BusinessPartners();
                Supplier entitySup = new Supplier();
                Address entityAddress = new Address();
                BusinessPartnerTagField entityTagField = new BusinessPartnerTagField();
                ControlToEntity(ctx, entity, entitySup, entityAddress, entityTagField);

                entity.GCBusinessPartnerType = Constant.BusinessObjectType.SUPPLIER;

                entity.AddressID = null;

                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);

                entity.BusinessPartnerID = BusinessLayer.GetBusinessPartnersMaxID(ctx);
                entityAddress.GCAddressType = Constant.AddressType.BUSINESS_PARTNER;
                entity.AddressID = entityAddress.AddressID = string.Format("{0}{1}", hdnAddressPrefix.Value, entity.BusinessPartnerID);
                entityAddressDao.Insert(entityAddress);
                entityDao.Update(entity);

                entitySup.BusinessPartnerID = entity.BusinessPartnerID;
                entitySupDao.Insert(entitySup);

                entityTagField.BusinessPartnerID = entity.BusinessPartnerID;
                entityTagFieldDao.Insert(entityTagField);

                retval = entity.BusinessPartnerID.ToString();

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
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

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            BusinessPartnersDao entityDao = new BusinessPartnersDao(ctx);
            SupplierDao entitySupDao = new SupplierDao(ctx);
            AddressDao entityAddressDao = new AddressDao(ctx);
            BusinessPartnerTagFieldDao entityTagFieldDao = new BusinessPartnerTagFieldDao(ctx);
            try
            {
                int BusinessPartnerID = Convert.ToInt32(hdnID.Value);
                BusinessPartners entity = entityDao.Get(BusinessPartnerID);
                Supplier entitySup = entitySupDao.Get(BusinessPartnerID);
                BusinessPartnerTagField entityTagField = entityTagFieldDao.Get(BusinessPartnerID);
                Address entityAddress = entityAddressDao.Get(entity.AddressID);

                ControlToEntity(ctx, entity, entitySup, entityAddress, entityTagField);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entitySup.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityTagField.LastUpdatedBy = AppSession.UserLogin.UserID;

                entityAddressDao.Update(entityAddress);
                entitySupDao.Update(entitySup);
                entityTagFieldDao.Update(entityTagField);
                entityDao.Update(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (pnlCustomAttribute.Visible)
            {
                List<Variable> ListCustomAttribute = initListCustomAttribute();
                if (ListCustomAttribute.Count == 0)
                    pnlCustomAttribute.Visible = false;
                else
                {
                    rptCustomAttribute.DataSource = ListCustomAttribute;
                    rptCustomAttribute.DataBind();
                }
            }
        }

        private List<Variable> initListCustomAttribute()
        {
            List<Variable> ListCustomAttribute = new List<Variable>();
            TagField tagField = BusinessLayer.GetTagField(Constant.BusinessObjectType.SUPPLIER);
            if (tagField != null)
            {
                if (tagField.TagField1 != "") { ListCustomAttribute.Add(new Variable { Code = "1", Value = tagField.TagField1 }); }
                if (tagField.TagField2 != "") { ListCustomAttribute.Add(new Variable { Code = "2", Value = tagField.TagField2 }); }
                if (tagField.TagField3 != "") { ListCustomAttribute.Add(new Variable { Code = "3", Value = tagField.TagField3 }); }
                if (tagField.TagField4 != "") { ListCustomAttribute.Add(new Variable { Code = "4", Value = tagField.TagField4 }); }
                if (tagField.TagField5 != "") { ListCustomAttribute.Add(new Variable { Code = "5", Value = tagField.TagField5 }); }
                if (tagField.TagField6 != "") { ListCustomAttribute.Add(new Variable { Code = "6", Value = tagField.TagField6 }); }
                if (tagField.TagField7 != "") { ListCustomAttribute.Add(new Variable { Code = "7", Value = tagField.TagField7 }); }
                if (tagField.TagField8 != "") { ListCustomAttribute.Add(new Variable { Code = "8", Value = tagField.TagField8 }); }
                if (tagField.TagField9 != "") { ListCustomAttribute.Add(new Variable { Code = "9", Value = tagField.TagField9 }); }
                if (tagField.TagField10 != "") { ListCustomAttribute.Add(new Variable { Code = "10", Value = tagField.TagField10 }); }
                if (tagField.TagField11 != "") { ListCustomAttribute.Add(new Variable { Code = "11", Value = tagField.TagField11 }); }
                if (tagField.TagField12 != "") { ListCustomAttribute.Add(new Variable { Code = "12", Value = tagField.TagField12 }); }
                if (tagField.TagField13 != "") { ListCustomAttribute.Add(new Variable { Code = "13", Value = tagField.TagField13 }); }
                if (tagField.TagField14 != "") { ListCustomAttribute.Add(new Variable { Code = "14", Value = tagField.TagField14 }); }
                if (tagField.TagField15 != "") { ListCustomAttribute.Add(new Variable { Code = "15", Value = tagField.TagField15 }); }
                if (tagField.TagField16 != "") { ListCustomAttribute.Add(new Variable { Code = "16", Value = tagField.TagField16 }); }
                if (tagField.TagField17 != "") { ListCustomAttribute.Add(new Variable { Code = "17", Value = tagField.TagField17 }); }
                if (tagField.TagField18 != "") { ListCustomAttribute.Add(new Variable { Code = "18", Value = tagField.TagField18 }); }
                if (tagField.TagField19 != "") { ListCustomAttribute.Add(new Variable { Code = "19", Value = tagField.TagField19 }); }
                if (tagField.TagField20 != "") { ListCustomAttribute.Add(new Variable { Code = "20", Value = tagField.TagField20 }); }
            }
            return ListCustomAttribute;
        }
    }
}