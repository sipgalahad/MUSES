using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Data.Model;
using CodeX.Web.Common;
using CodeX.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.Information.Program
{
    public partial class ExtracurricularScheduleInfo : BasePageList
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.EXTRACURRICULAR_SCHEDULE_INFO;
        }

        protected string OnGetClassStudyTypeExtracurricular()
        {
            return Constant.ClassStudyType.EXTRACURRICULAR;
        }

        protected string OnGetSchoolPeriodNowFilterExpression()
        {
            return string.Format("GCSchoolPeriodStatus != '{0}' AND StartDate <= '{1}' AND EndDate >= '{1}'", Constant.SchoolPeriodStatus.VOID, DateTime.Now.ToString("yyyyMMdd"));
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<vSite> lstSite = BusinessLayer.GetvSiteList(String.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsHeader = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<vSite>(cboSite, lstSite, "SiteName", "SiteID");
            cboSite.SelectedIndex = 0;

            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SCHOOL_DAILY_SCHEDULE_TYPE, Constant.StandardCode.SCHOOL_DAY));
            
            List<StandardCode> lstSchoolDay = lstSc.Where(p => p.ParentID == Constant.StandardCode.SCHOOL_DAY).ToList();
            decimal width = 100 / lstSchoolDay.Count;
            if (lstSchoolDay.Count(p => p.StandardCodeID == string.Format("{0}^001", Constant.StandardCode.SCHOOL_DAY)) < 1)
                tdSchoolDay1.Style.Add("display", "none");
            if (lstSchoolDay.Count(p => p.StandardCodeID == string.Format("{0}^002", Constant.StandardCode.SCHOOL_DAY)) < 1)
                tdSchoolDay2.Style.Add("display", "none");
            if (lstSchoolDay.Count(p => p.StandardCodeID == string.Format("{0}^003", Constant.StandardCode.SCHOOL_DAY)) < 1)
                tdSchoolDay3.Style.Add("display", "none");
            if (lstSchoolDay.Count(p => p.StandardCodeID == string.Format("{0}^004", Constant.StandardCode.SCHOOL_DAY)) < 1)
                tdSchoolDay4.Style.Add("display", "none");
            if (lstSchoolDay.Count(p => p.StandardCodeID == string.Format("{0}^005", Constant.StandardCode.SCHOOL_DAY)) < 1)
                tdSchoolDay5.Style.Add("display", "none");
            if (lstSchoolDay.Count(p => p.StandardCodeID == string.Format("{0}^006", Constant.StandardCode.SCHOOL_DAY)) < 1)
                tdSchoolDay6.Style.Add("display", "none");
            tdSchoolDay1.Style.Add("width", string.Format("{0}%", width));
            tdSchoolDay2.Style.Add("width", string.Format("{0}%", width));
            tdSchoolDay3.Style.Add("width", string.Format("{0}%", width));
            tdSchoolDay4.Style.Add("width", string.Format("{0}%", width));
            tdSchoolDay5.Style.Add("width", string.Format("{0}%", width));
            tdSchoolDay6.Style.Add("width", string.Format("{0}%", width));

            BindGridView();
        }

        #region Bind Grid View
        private void BindGridView()
        {
            if (tacSchoolClass.Value != "")
            {
                if (tacSchoolPeriod.Value != "")
                {
                    List<vClassSchedule> lstClassSchedule = BusinessLayer.GetvClassScheduleList(string.Format("SchoolPeriodID = {0} AND SchoolClassID = {1} AND GCClassStudyType = '{2}' AND IsDeleted = 0", tacSchoolPeriod.Value, tacSchoolClass.Value, Constant.ClassStudyType.EXTRACURRICULAR));

                    rptDay1.DataSource = lstClassSchedule.Where(p => p.DayNumber == 1).ToList();
                    rptDay1.DataBind();
                    rptDay2.DataSource = lstClassSchedule.Where(p => p.DayNumber == 2).ToList();
                    rptDay2.DataBind();
                    rptDay3.DataSource = lstClassSchedule.Where(p => p.DayNumber == 3).ToList();
                    rptDay3.DataBind();
                    rptDay4.DataSource = lstClassSchedule.Where(p => p.DayNumber == 4).ToList();
                    rptDay4.DataBind();
                    rptDay5.DataSource = lstClassSchedule.Where(p => p.DayNumber == 5).ToList();
                    rptDay5.DataBind();
                    rptDay6.DataSource = lstClassSchedule.Where(p => p.DayNumber == 6).ToList();
                    rptDay6.DataBind();
                }
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion
    }
}