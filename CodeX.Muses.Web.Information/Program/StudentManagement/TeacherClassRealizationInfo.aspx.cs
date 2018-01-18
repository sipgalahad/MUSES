using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using DevExpress.Utils;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;
using DevExpress.Web.ASPxEditors;
using CodeX.Common;
using System.Web.UI.HtmlControls;


namespace CodeX.Muses.Web.Information.Program
{
    public partial class TeacherClassRealizationInfo : BasePageList
    {  
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.TEACHER_CLASS_REALIZATION_INFO;
        }

        protected string OnGetPeriodSectionFilterExpression()
        {
            return string.Format("GCPeriodSectionStatus != '{0}'", Constant.SchoolPeriodStatus.VOID);
        }

        protected string OnGetPeriodSectionNowFilterExpression()
        {
            return string.Format("GCPeriodSectionStatus != '{0}' AND StartDate <= '{1}' AND EndDate >= '{1}'", Constant.SchoolPeriodStatus.VOID, DateTime.Now.ToString("yyyyMMdd"));
        }

        protected string OnGetSchoolPeriodNowFilterExpression()
        {
            return string.Format("GCSchoolPeriodStatus != '{0}' AND StartDate <= '{1}' AND EndDate >= '{1}'", Constant.SchoolPeriodStatus.VOID, DateTime.Now.ToString("yyyyMMdd"));
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            txtDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);

            List<vSite> lstSite = BusinessLayer.GetvSiteList(String.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsHeader = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<vSite>(cboSite, lstSite, "SiteName", "SiteID");
            cboSite.SelectedIndex = 0;
        }

        List<vClassMeeting> lstClassMeeting = null;

        protected void cbpTeacher_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridViewTeacher();
        }

        private void BindGridViewTeacher()
        {
            if (tacPeriodSection.Value != "")
                lstClassMeeting = BusinessLayer.GetvClassMeetingList(string.Format("PeriodSectionID = {0} AND MeetingDate = '{1}' AND IsDeleted = 0", tacPeriodSection.Value, Helper.GetDatePickerValue(txtDate).ToString("yyyyMMdd")));
            else
                lstClassMeeting = new List<vClassMeeting>();
            List<vTeacher> lstEntity = BusinessLayer.GetvTeacherList(string.Format("TeacherID IN (SELECT TeacherID FROM TeacherSubject WHERE SiteID = '{0}') AND IsDeleted = 0", cboSite.Value));
            grdTeacher.DataSource = lstEntity;
            grdTeacher.DataBind();
        }

        protected void grdTeacher_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vTeacher entity = (vTeacher)e.Row.DataItem;
                HtmlGenericControl divMeeting = (HtmlGenericControl)e.Row.FindControl("divMeeting");
                divMeeting.InnerHtml = string.Join(",", lstClassMeeting.Where(p => p.TeacherID == entity.TeacherID || p.AssistantTeacherID == entity.TeacherID).Select(p => string.Format("{0} ({1})", p.SubjectName, p.SchoolClassInitial)));
            }
        }
    }
}