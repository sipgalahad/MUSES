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

namespace CodeX.Web.ControlPanelHQ.MasterPage
{
    public partial class SiteBannerDtCtl : BaseContentPopupCtl
    {
        public void InitializeBanner()
        {
            vSite entity = BusinessLayer.GetvSiteList(string.Format("SiteID = '{0}'", AppSession.SiteID))[0];
            hdnTitleText.Value = entity.SiteName;
            divBusinessPartnerCode.InnerHtml = entity.SiteID;
            //divContactPerson.InnerHtml = entity.ContactPerson;
            //divPhoneNo.InnerHtml = entity.cfPhoneNo;
            divAddress.InnerHtml = entity.Address;
            //hdnPatientGender.Value = entity.GCSex;
        }

        public string OnGetTitleText()
        {
            return hdnTitleText.Value;
        }
    }
}