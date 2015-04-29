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
    public partial class CurriculumBannerDtCtl : BaseContentPopupCtl
    {
        public void InitializeBanner()
        {
            Curriculum entity = BusinessLayer.GetCurriculumList(string.Format("CurriculumID = {0}", AppSession.CurriculumID))[0];
            hdnTitleText.Value = entity.CurriculumName;
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