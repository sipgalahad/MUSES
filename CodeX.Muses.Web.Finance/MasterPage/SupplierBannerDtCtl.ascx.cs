using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Common;
using CodeX.Web.Common;

namespace CodeX.Web.Finance.MasterPage
{
    public partial class SupplierBannerDtCtl : BaseContentPopupCtl
    {
        public void InitializeBanner(int businessPartnerID)
        {
            vSupplier entity = BusinessLayer.GetvSupplierList(string.Format("BusinessPartnerID = {0}", businessPartnerID))[0];
            hdnTitleText.Value = entity.BusinessPartnerName;
            divBusinessPartnerCode.InnerHtml = entity.BusinessPartnerCode;
            divContactPerson.InnerHtml = entity.ContactPerson;
            divPhoneNo.InnerHtml = entity.cfPhoneNo;
            divAddress.InnerHtml = entity.Address;
            divBank.InnerHtml = entity.Bank;
            divBankAccountHolder.InnerHtml = entity.BankAccountHolder;
            divBankReferenceNo.InnerHtml = entity.BankReferenceNo;
            //hdnPatientGender.Value = entity.GCSex;
        }

        public string OnGetTitleText()
        {
            return hdnTitleText.Value;
        }
    }
}