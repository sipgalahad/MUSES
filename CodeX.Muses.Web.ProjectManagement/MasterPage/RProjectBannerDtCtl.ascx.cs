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

namespace CodeX.Muses.Web.ProjectManagement.MasterPage
{
    public partial class RProjectBannerDtCtl : BaseContentPopupCtl
    {
        public void InitializeBanner()
        {
            RProject entity = BusinessLayer.GetRProjectList(string.Format("ProjectID = {0}", AppSession.ProjectID))[0];
            hdnTitleText.Value = entity.ProjectName;
            divCode.InnerHtml = entity.ProjectCode;
            divDate.InnerHtml = string.Format("{0} - {1}", entity.StartDate.ToString(Constant.FormatString.DATE_FORMAT), entity.EndDate.ToString(Constant.FormatString.DATE_FORMAT));
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