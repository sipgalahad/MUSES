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
    public partial class ClassStudentBannerDtCtl : BaseContentPopupCtl
    {
        public void InitializeBanner()
        {
            vClassStudent entity = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0} AND StudentID = {1}", AppSession.ClassStudent.SchoolClassID, AppSession.ClassStudent.StudentID))[0];
            hdnTitleText.Value = entity.StudentName;
            imgPatientImage.Src = entity.StudentImageUrl;
            imgPatientImage.Attributes.Add("gender", entity.GCGender);
            divStudentCode.InnerHtml = entity.StudentCode;
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