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
    public partial class BudgetManagementBannerDtCtl : BaseContentPopupCtl
    {
        public void InitializeBanner()
        {
            ProjectBudgetHd entity = BusinessLayer.GetProjectBudgetHdList(string.Format("BudgetID = {0}", AppSession.BudgetID))[0];
            hdnTitleText.Value = entity.BudgetName;
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