using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Model;
using System.Web.UI.HtmlControls;
using System.Text;
using CodeX.Common;
using CodeX.Web.Common;
using CodeX.Web.CommonLibs.MasterPage;

namespace CodeX.Muses.Web.StudentManagement.MasterPage
{
    public partial class MPSchoolClassPageTrx : BaseMP
    {
        public List<GetUserMenuAccess> ListMenu = null;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!Page.IsPostBack)
            {
                string parentCode = "";
                if (!AppSession.SchoolClass.IsTeacherPage)
                    parentCode = Constant.MenuCode.StudentManagement.SCHOOL_CLASS_PAGE;
                else
                    parentCode = Constant.MenuCode.StudentManagement.TEACHER_SCHOOL_CLASS_PAGE;
                string filterExpression = string.Format("(ParentCode = '{0}' OR ParentID IN (SELECT MenuID FROM Menu WHERE ParentID = (SELECT MenuID FROM Menu WHERE MenuCode = '{0}')))", parentCode);
                ListMenu = BusinessLayer.GetUserMenuAccess(Constant.Module.STUDENT_MANAGEMENT, AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, filterExpression);
                ((MPBaseDetailPageTrx)Master).SetParentCode(parentCode);
                ((MPBaseDetailPageTrx)Master).SetListMenu(ListMenu);

                ((SchoolClassBannerDtCtl)ctlBanner).InitializeBanner();
                ((MPBaseDetailPageTrx)Master).SetTitleText(((SchoolClassBannerDtCtl)ctlBanner).OnGetTitleText());
            }
        }
    }
}