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

namespace CodeX.Muses.Web.TeacherPage.Program
{
    public partial class SubjectMatterInformation : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.TeacherPage.WS_SUBJECT_MATTER;
        }
        protected override void InitializeDataControl()
        {
            BindGridView();
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            vClassSubject classSubject = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID)).FirstOrDefault();
            vSchoolClass schoolClass = BusinessLayer.GetvSchoolClassList(string.Format("SchoolClassID = {0}", classSubject.SchoolClassID)).FirstOrDefault();
            string filterExpression = string.Format("SubjectID = {0} AND IsDeleted = 0 AND GCGrade = '{1}'", classSubject.SubjectID, schoolClass.GCGrade);
            if (schoolClass.GCMajor != "")
                filterExpression += string.Format(" AND GCMajor = '{0}'", schoolClass.GCMajor);
            else
                filterExpression += " AND GCMajor IS NULL";

            grdView.DataSource = BusinessLayer.GetSubjectMatterList(filterExpression);
            grdView.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion
    }
}