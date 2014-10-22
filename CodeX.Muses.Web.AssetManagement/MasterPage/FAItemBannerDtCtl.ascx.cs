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

namespace CodeX.Web.AssetManagement.MasterPage
{
    public partial class FAItemBannerDtCtl : BaseContentPopupCtl
    {
        public void InitializeBanner()
        {
            FAItem entity = BusinessLayer.GetFAItemList(string.Format("FixedAssetID = {0}", AppSession.FixedAssetID))[0];
            hdnTitleText.Value = entity.FixedAssetName;
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