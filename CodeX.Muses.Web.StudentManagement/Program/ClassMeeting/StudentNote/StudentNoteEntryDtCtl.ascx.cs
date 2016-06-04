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
    public partial class StudentNoteEntryDtCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override void InitializeDataControl(string param)
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.STUDENT_NOTE_CATEGORY, Constant.StandardCode.STUDENT_NOTE_RATE));
            Methods.SetComboBoxField<StandardCode>(cboNoteCategory, lstSc.Where(p => p.ParentID == Constant.StandardCode.STUDENT_NOTE_CATEGORY).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboNoteRate, lstSc.Where(p => p.ParentID == Constant.StandardCode.STUDENT_NOTE_RATE).ToList(), "StandardCodeName", "StandardCodeID");

            lstSc.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "-- Semua --" });
            Methods.SetComboBoxField<StandardCode>(cboFilterNoteCategory, lstSc.Where(p => p.ParentID == Constant.StandardCode.STUDENT_NOTE_CATEGORY || p.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboFilterNoteRate, lstSc.Where(p => p.ParentID == Constant.StandardCode.STUDENT_NOTE_RATE || p.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
            cboFilterNoteCategory.SelectedIndex = 0;
            cboFilterNoteRate.SelectedIndex = 0;

            hdnStudentID.Value = param;
            Student entity = BusinessLayer.GetStudent(Convert.ToInt32(param));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.StudentCode, entity.StudentName);

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView();

            BindGridView2(CurrPage, true, ref PageCount, ref RowCount);

            Helper.SetControlEntrySetting(cboNoteCategory, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(cboNoteRate, new ControlEntrySetting(true, true, true), "mpTrxPopup");     
            Helper.SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, true), "mpTrxPopup");     
        }

        private void BindGridView()
        {
            string filterExpression = string.Format("StudentID = {0} AND ClassSubjectID = {1} AND PeriodSectionID = {2} AND ClassMeetingID = {3} AND IsDeleted = 0", hdnStudentID.Value, AppSession.ClassSubject.ClassSubjectID, AppSession.ClassSubject.PeriodSectionID, AppSession.ClassSubject.ClassMeetingID);
            List<vStudentNote> lstEntity = BusinessLayer.GetvStudentNoteList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        private void BindGridView2(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = string.Format("StudentID = {0} AND ClassSubjectID = {1} AND PeriodSectionID = {2} AND ClassMeetingID != {3} AND IsDeleted = 0", hdnStudentID.Value, AppSession.ClassSubject.ClassSubjectID, AppSession.ClassSubject.PeriodSectionID, AppSession.ClassSubject.ClassMeetingID);
            if (cboFilterNoteCategory.Value != null && cboFilterNoteCategory.Value.ToString() != "")
                filterExpression += string.Format(" AND GCNoteCategory = '{0}'", cboFilterNoteCategory.Value);
            if (cboFilterNoteRate.Value != null && cboFilterNoteRate.Value.ToString() != "")
                filterExpression += string.Format(" AND GCNoteRate = '{0}'", cboFilterNoteRate.Value);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvStudentNoteRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vStudentNote> lstEntity = BusinessLayer.GetvStudentNoteList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "NoteDate DESC");
            grdView2.DataSource = lstEntity;
            grdView2.DataBind();
        }

        protected void cbpViewPopup2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView2(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView2(1, true, ref pageCount, ref rowCount);
                    result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        #region Process Detail
        protected void cbpProcessPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                if (OnDeleteEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity(StudentNote entity)
        {
            entity.GCNoteCategory = cboNoteCategory.Value.ToString();
            entity.GCNoteRate = cboNoteRate.Value.ToString();
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                StudentNote entity = new StudentNote();
                ControlToEntity(entity);
                entity.ClassMeetingID = AppSession.ClassSubject.ClassMeetingID;
                entity.ClassSubjectID = AppSession.ClassSubject.ClassSubjectID;
                entity.PeriodSectionID = AppSession.ClassSubject.PeriodSectionID;
                entity.NoteDate = DateTime.Now;
                entity.NoteTime = DateTime.Now.ToString(Constant.FormatString.TIME_FORMAT);
                entity.StudentID = Convert.ToInt32(hdnStudentID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertStudentNote(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            try
            {
                StudentNote entity = BusinessLayer.GetStudentNote(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateStudentNote(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                StudentNote entity = BusinessLayer.GetStudentNote(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateStudentNote(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion
    }
}