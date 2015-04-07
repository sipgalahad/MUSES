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
using DevExpress.Web.ASPxCallbackPanel;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class StudentAttendanceList : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.CS_STUDENT_ATTENDANCE;
        }

        protected override void InitializeDataControl()
        {
            List<ClassStudentDailyAttendance> csda = BusinessLayer.GetClassStudentDailyAttendanceList(String.Format("SchoolClassID = {0} AND PeriodSectionID = {1} AND StudentID = {2}", AppSession.ClassStudent.SchoolClassID, AppSession.ClassStudent.PeriodSectionID, AppSession.ClassStudent.StudentID));
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND StandardCodeID != '{1}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.STUDENT_ATTENDANCE, Constant.AttendanceStatus.HADIR));
            foreach (StandardCode sc in lstSc)
            {
                int count = csda.Where(p => p.GCAttendanceStatus == sc.StandardCodeID).Count();
                sc.TagProperty = count.ToString();
            }
            grdView.DataSource = lstSc;
            grdView.DataBind();
        }
    }
}