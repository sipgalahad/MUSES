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

namespace CodeX.Muses.Web.ControlPanel.MasterPage
{
    public partial class SiteServiceUnitBannerDtCtl : BaseContentPopupCtl
    {
        public void InitializeBanner()
        {
            vSiteServiceUnit entity = BusinessLayer.GetvSiteServiceUnitList(string.Format("SiteServiceUnitID = {0}", AppSession.SiteServiceUnitID))[0];
            hdnTitleText.Value = string.Format("{0} - {1}", entity.SiteName, entity.ServiceUnitName);
            //divBusinessPartnerCode.InnerHtml = entity.BusinessPartnerCode;
            //divContactPerson.InnerHtml = entity.ContactPerson;
            //divPhoneNo.InnerHtml = entity.cfPhoneNo;
            //divAddress.InnerHtml = entity.Address;
            //hdnPatientGender.Value = entity.GCSex;
        }

        public string OnGetTitleText()
        {
            return hdnTitleText.Value;
        }
    }
}