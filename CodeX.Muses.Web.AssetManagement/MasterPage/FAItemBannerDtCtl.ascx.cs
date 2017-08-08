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
            vFAItemDt entity = BusinessLayer.GetvFAItemDtList(string.Format("FixedAssetDtID = {0}", AppSession.FixedAssetDtID))[0];
            hdnTitleText.Value = entity.FixedAssetName;
            divCode.InnerHtml = entity.FixedAssetDtCode;
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