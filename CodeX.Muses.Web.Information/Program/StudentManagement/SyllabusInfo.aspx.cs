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
    public partial class SyllabusInfo : BasePageList
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.SYLLABUS_INFO;
        }

        protected string OnGetClassStudyTypeRegular()
        {
            return Constant.ClassStudyType.REGULAR;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<StandardCode> lstSchoolType = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SCHOOL_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboSchoolType, lstSchoolType, "StandardCodeName", "StandardCodeID");
            cboSchoolType.SelectedIndex = 0;

            BindGridView();
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = "1 = 0";
            if (tacSubject.Value != "" && tacCurriculum.Value != "")
                filterExpression = string.Format("SubjectID = {0} AND CurriculumID = {1} AND IsDeleted = 0", tacSubject.Value, tacCurriculum.Value);
            List<vSubjectCurriculum> lstEntity = BusinessLayer.GetvSubjectCurriculumList(filterExpression);;
            if (lstEntity.Count > 0)
                lstSyllabus = BusinessLayer.GetSubjectCurriculumSyllabusList(string.Format("SubjectCurriculumID IN ({0}) AND IsDeleted = 0", String.Join(",", lstEntity.Select(p => p.SubjectCurriculumID).ToList())));
            grdView.DataSource = lstEntity;
            grdView.DataBind();            
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vSubjectCurriculum entity = e.Row.DataItem as vSubjectCurriculum;
                HtmlGenericControl divSyllabusCount = (HtmlGenericControl)e.Row.FindControl("divSyllabusCount");
                divSyllabusCount.InnerHtml = lstSyllabus.Count(p => p.SubjectCurriculumID == entity.SubjectCurriculumID).ToString();
            }
        }

        List<SubjectCurriculumSyllabus> lstSyllabus = null;
        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion
    }
}