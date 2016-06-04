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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class StudentNoteViewDtCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');

            hdnStudentID.Value = temp[0];
            hdnNoteCategory.Value = temp[1];
            hdnNoteRate.Value = temp[2];

            Student entity = BusinessLayer.GetStudent(Convert.ToInt32(hdnStudentID.Value));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.StudentCode, entity.StudentName);

            if (hdnNoteCategory.Value == "")
                txtNoteCategory.Text = "-- Semua --";
            else
                txtNoteCategory.Text = BusinessLayer.GetStandardCode(hdnNoteCategory.Value).StandardCodeName;
            txtNoteRate.Text = BusinessLayer.GetStandardCode(hdnNoteRate.Value).StandardCodeName;

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = string.Format("StudentID = {0} AND ClassSubjectID = {1} AND PeriodSectionID = {2} AND IsDeleted = 0", hdnStudentID.Value, AppSession.ClassSubject.ClassSubjectID, AppSession.ClassSubject.PeriodSectionID);
            if (hdnNoteCategory.Value != "")
                filterExpression += string.Format(" AND GCNoteCategory = '{0}'", hdnNoteCategory.Value);
            if (hdnNoteRate.Value != "")
                filterExpression += string.Format(" AND GCNoteRate = '{0}'", hdnNoteRate.Value);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvStudentNoteRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vStudentNote> lstEntity = BusinessLayer.GetvStudentNoteList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "NoteDate DESC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount, ref rowCount);
                    result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
    }
}