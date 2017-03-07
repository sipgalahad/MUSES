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
    public partial class SchoolClassBannerDtCtl : BaseContentPopupCtl
    {
        public void InitializeBanner()
        {
            vSchoolClass entity = BusinessLayer.GetvSchoolClassList(string.Format("SchoolClassID = {0}", AppSession.SchoolClass.SchoolClassID))[0];
            hdnTitleText.Value = entity.SchoolClassName;
            divStudentCode.InnerHtml = entity.SchoolClassCode;
            //divDateOfBirth.InnerHtml = entity.DateOfBirth.ToString("dd/MM/yyyy");
            //divPhoneNo.InnerHtml = entity.cfPhoneNo;
            //divAddress.InnerHtml = entity.HomeAddress;
        }

        public string OnGetTitleText()
        {
            return hdnTitleText.Value;
        }
    }
}