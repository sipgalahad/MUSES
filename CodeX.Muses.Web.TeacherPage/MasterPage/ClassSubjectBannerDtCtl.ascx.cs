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

namespace CodeX.Muses.Web.TeacherPage.MasterPage
{
    public partial class ClassSubjectBannerDtCtl : BaseContentPopupCtl
    {
        public void InitializeBanner()
        {
            vClassSubject entity = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID))[0];
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