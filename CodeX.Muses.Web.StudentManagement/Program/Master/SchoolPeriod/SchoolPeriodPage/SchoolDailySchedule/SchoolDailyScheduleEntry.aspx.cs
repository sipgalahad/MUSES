using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using System.Data;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class SchoolDailyScheduleEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SP_SCHOOL_DAILY_SCHEDULE;
        }
        protected override void InitializeDataControl()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SCHOOL_DAILY_SCHEDULE_TYPE));
            rptRemarks.DataSource = lstSc;
            rptRemarks.DataBind();

            BindTable();
        }

        private void BindTable()
        {
            List<vSchoolDailyScheduleDt> lstEntity = BusinessLayer.GetvSchoolDailyScheduleDtList(string.Format("SchoolPeriodID = {0}", AppSession.SchoolPeriodID));
            rptDay1.DataSource = lstEntity.Where(p => p.DayNumber == 1).ToList();
            rptDay1.DataBind();
            rptDay2.DataSource = lstEntity.Where(p => p.DayNumber == 2).ToList();
            rptDay2.DataBind();
            rptDay3.DataSource = lstEntity.Where(p => p.DayNumber == 3).ToList();
            rptDay3.DataBind();
            rptDay4.DataSource = lstEntity.Where(p => p.DayNumber == 4).ToList();
            rptDay4.DataBind();
            rptDay5.DataSource = lstEntity.Where(p => p.DayNumber == 5).ToList();
            rptDay5.DataBind();
            rptDay6.DataSource = lstEntity.Where(p => p.DayNumber == 6).ToList();
            rptDay6.DataBind();
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }
    }
}