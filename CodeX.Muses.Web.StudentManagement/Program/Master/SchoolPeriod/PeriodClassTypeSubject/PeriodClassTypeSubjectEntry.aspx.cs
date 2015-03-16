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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class PeriodClassTypeSubjectEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SP_SCHOOL_PERIOD_CLASS_TYPE_SUBJECT;
        }
        protected override void InitializeDataControl()
        {
            List<vPeriodClassType> lstClassType = BusinessLayer.GetvPeriodClassTypeList(string.Format("SchoolPeriodID = {0} AND GCClassStudyType = '{1}' AND IsDeleted = 0", AppSession.SchoolPeriodID, Constant.ClassStudyType.REGULAR));
            Methods.SetComboBoxField<vPeriodClassType>(cboClassType, lstClassType, "ClassTypeName", "PeriodClassTypeID");
            cboClassType.SelectedIndex = 0;

            BindGridView();

            Helper.SetControlEntrySetting(tacSubject, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(tacTeacher, new ControlEntrySetting(true, true, false), "mpTrx");
            Helper.SetControlEntrySetting(txtNoMeetingHoursInWeek, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtPassingGrade, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = "1 = 0";
            if (cboClassType.Value != null && cboClassType.Value.ToString() != "0")
            {
                filterExpression = string.Format("SchoolPeriodID = {0} AND PeriodClassTypeID = {1} AND GCClassStudyType = '{2}' AND IsDeleted = 0", AppSession.SchoolPeriodID, cboClassType.Value, Constant.ClassStudyType.REGULAR);
                vPeriodClassType entity = BusinessLayer.GetvPeriodClassTypeList(string.Format("PeriodClassTypeID = {0}", cboClassType.Value)).FirstOrDefault();
                hdnClassTypeID.Value = entity.ClassTypeID.ToString();

                hdnClassRowCount.Value = BusinessLayer.GetSchoolClassRowCount(string.Format("PeriodClassTypeID = {0} AND IsDeleted = 0", cboClassType.Value)).ToString();
            }
            List<vPeriodClassTypeSubject> lstEntity = BusinessLayer.GetvPeriodClassTypeSubjectList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        private void ControlToEntity(PeriodClassTypeSubject entity)
        {
            entity.SubjectID = Convert.ToInt32(tacSubject.Value);
            entity.TeacherID = Convert.ToInt32(tacTeacher.Value);
            entity.NoMeetingHoursInWeek = Convert.ToInt16(txtNoMeetingHoursInWeek.Text);
            entity.PassingGrade = Convert.ToInt16(txtPassingGrade.Text);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                PeriodClassTypeSubject entity = new PeriodClassTypeSubject();
                ControlToEntity(entity);
                entity.PeriodClassTypeID = Convert.ToInt32(cboClassType.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertPeriodClassTypeSubject(entity);
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
                PeriodClassTypeSubject entity = BusinessLayer.GetPeriodClassTypeSubject(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdatePeriodClassTypeSubject(entity);
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
                PeriodClassTypeSubject entity = BusinessLayer.GetPeriodClassTypeSubject(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedDate = DateTime.Now;
                BusinessLayer.UpdatePeriodClassTypeSubject(entity);
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