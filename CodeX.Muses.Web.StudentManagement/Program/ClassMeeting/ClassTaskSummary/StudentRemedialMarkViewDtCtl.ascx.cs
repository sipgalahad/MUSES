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
using CodeX.Data.Core.Dal;
using CodeX.Web.CustomControl;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class StudentRemedialMarkViewDtCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');
            hdnStudentID.Value = temp[0];
            hdnClassSubjectTaskID.Value = temp[1];
            Student entity = BusinessLayer.GetStudent(Convert.ToInt32(hdnStudentID.Value));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.StudentCode, entity.StudentName);

            ClassSubjectTask entityTask = BusinessLayer.GetClassSubjectTask(Convert.ToInt32(hdnClassSubjectTaskID.Value));
            txtHeaderText2.Text = string.Format("{0} - {1}", entityTask.ClassTaskCode, entityTask.Topic);

            ClassStudentSubjectTaskMark entityMark = BusinessLayer.GetClassStudentSubjectTaskMark(Convert.ToInt32(hdnClassSubjectTaskID.Value), Convert.ToInt32(hdnStudentID.Value));
            txtOriginalMark.Text = entityMark.OriginalMark.ToString();
            txtFinalMark.Text = entityMark.Mark.ToString();

            BindGridView();
        }

        private void BindGridView()
        {
            string filterExpression = string.Format("StudentID = {0} AND ClassSubjectTaskID = {1} ORDER BY DisplayOrder ASC", hdnStudentID.Value, hdnClassSubjectTaskID.Value);
            List<vClassStudentSubjectTaskRemedialMark> lstEntity = BusinessLayer.GetvClassStudentSubjectTaskRemedialMarkList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
    }
}