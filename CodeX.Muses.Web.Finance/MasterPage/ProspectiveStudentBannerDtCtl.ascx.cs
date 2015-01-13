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
    public partial class ProspectiveStudentBannerDtCtl : BaseContentPopupCtl
    {
        public void InitializeBanner()
        {
            vProspectiveStudent entity = BusinessLayer.GetvProspectiveStudentList(string.Format("ProspectiveStudentID = {0}", AppSession.ProspectiveStudentID))[0];
            hdnTitleText.Value = entity.ProspectiveStudentName;
            imgPatientImage.Src = entity.ProspectiveStudentImageUrl;
            imgPatientImage.Attributes.Add("gender", entity.GCGender);
            divStudentCode.InnerHtml = entity.ProspectiveStudentCode;
            divDateOfBirth.InnerHtml = entity.DateOfBirth.ToString("dd/MM/yyyy");
            divPhoneNo.InnerHtml = entity.cfPhoneNo;
            divAddress.InnerHtml = entity.HomeAddress;
        }

        public string OnGetTitleText()
        {
            return hdnTitleText.Value;
        }
    }
}