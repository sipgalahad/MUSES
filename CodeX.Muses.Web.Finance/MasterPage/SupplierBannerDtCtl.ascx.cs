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

namespace CodeX.Muses.Web.Finance.MasterPage
{
    public partial class SupplierBannerDtCtl : BaseContentPopupCtl
    {
        public void InitializeBanner()
        {
            vSupplier entity = BusinessLayer.GetvSupplierList(string.Format("BusinessPartnerID = {0}", AppSession.BusinessPartnerID))[0];
            hdnTitleText.Value = entity.BusinessPartnerName;
            divBusinessPartnerCode.InnerHtml = entity.BusinessPartnerCode;
            divContactPerson.InnerHtml = entity.ContactPerson;
            divPhoneNo.InnerHtml = entity.cfPhoneNo;
            divAddress.InnerHtml = entity.Address;
            //hdnPatientGender.Value = entity.GCSex;
        }

        public string OnGetTitleText()
        {
            return hdnTitleText.Value;
        }
    }
}