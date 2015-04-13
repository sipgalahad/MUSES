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
    public partial class TeacherAbsenceBannerDtCtl : BaseContentPopupCtl
    {
        public void InitializeBanner()
        {
            vTeacherAbsence entity = BusinessLayer.GetvTeacherAbsenceList(string.Format("TeacherAbsenceID = {0}", AppSession.TeacherAbsenceID))[0];
            hdnTitleText.Value = entity.TeacherName;
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