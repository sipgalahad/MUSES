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

namespace CodeX.Muses.Web.StudentManagement.MasterPage
{
    public partial class SchoolPeriodBannerDtCtl : BaseContentPopupCtl
    {
        public void InitializeBanner()
        {
            SchoolPeriod entity = BusinessLayer.GetSchoolPeriodList(string.Format("SchoolPeriodID = {0}", AppSession.SchoolPeriodID))[0];
            hdnTitleText.Value = entity.SchoolPeriodName;
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