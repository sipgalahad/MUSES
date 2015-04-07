using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common;
using CodeX.Common;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class StudentAttendanceDtCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            StandardCode entity = BusinessLayer.GetStandardCode(hdnID.Value);
            txtHeaderText.Text = entity.StandardCodeName;

            BindGridView();
        }

        private void BindGridView()
        {
            string filterExpression = String.Format("SchoolClassID = {0} AND PeriodSectionID = {1} AND StudentID = {2} AND GCAttendanceStatus = '{3}'", AppSession.ClassStudent.SchoolClassID, AppSession.ClassStudent.PeriodSectionID, AppSession.ClassStudent.StudentID, hdnID.Value);
            grdView.DataSource = BusinessLayer.GetClassStudentDailyAttendanceList(filterExpression);
            grdView.DataBind();
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}