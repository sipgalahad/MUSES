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
    public partial class CustomerBannerDtCtl : BaseContentPopupCtl
    {
        public void InitializeBanner()
        {
            vCustomer entity = BusinessLayer.GetvCustomerList(string.Format("BusinessPartnerID = {0}", AppSession.BusinessPartnerID))[0];
            hdnTitleText.Value = entity.BusinessPartnerName;
            divBusinessPartnerCode.InnerHtml = entity.BusinessPartnerCode;
        }

        public string OnGetTitleText()
        {
            return hdnTitleText.Value;
        }
    }
}