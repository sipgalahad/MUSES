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
    public partial class SubjectBannerDtCtl : BaseContentPopupCtl
    {
        public void InitializeBanner()
        {
            Subject entity = BusinessLayer.GetSubjectList(string.Format("SubjectID = {0}", AppSession.SubjectID))[0];
            hdnTitleText.Value = entity.SubjectName;
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