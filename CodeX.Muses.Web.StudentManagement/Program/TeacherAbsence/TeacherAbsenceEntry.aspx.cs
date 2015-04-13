using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class TeacherAbsenceEntry : BasePageEntry
    {
        protected string OnGetTeacherFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }

        protected string OnGetAbsenceReasonOther()
        {
            return Constant.AbsenceReason.OTHER;
        }

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.TEACHER_ABSENCE;
        }

        protected override void InitializeDataControl()
        {
            string[] param = Page.Request.QueryString["id"].Split('|');
            if (param[0] == "edit")
            {
                IsAdd = false;
                String ID = param[1];
                hdnID.Value = ID;
                vTeacherAbsence entity = BusinessLayer.GetvTeacherAbsenceList(string.Format("TeacherAbsenceID = {0}", hdnID.Value)).FirstOrDefault();
                hdnSchoolPeriodID.Value = entity.SchoolPeriodID.ToString();
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                hdnSchoolPeriodID.Value = param[1];
                IsAdd = true;
            }
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(tacTeacher, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtStartDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtEndDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtStartTime, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtEndTime, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(chkIsFullDay, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboAbsenceReason, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtOtherAbsenceReason, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.ABSENCE_REASON));
            Methods.SetComboBoxField<StandardCode>(cboAbsenceReason, lstSc, "StandardCodeName", "StandardCodeID");
        }

        private void EntityToControl(vTeacherAbsence entity)
        {
            tacTeacher.Value = entity.TeacherID.ToString();
            tacTeacher.Text = entity.TeacherName;
            txtStartDate.Text = entity.StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtEndDate.Text = entity.EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtStartTime.Text = entity.StartTime;
            txtEndTime.Text = entity.EndTime;
            chkIsFullDay.Checked = entity.IsFullDay;
            cboAbsenceReason.Value = entity.GCAbsenceReason;
            txtOtherAbsenceReason.Text = entity.OtherAbsenceReason;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(TeacherAbsence entity)
        {
            entity.TeacherID = Convert.ToInt32(tacTeacher.Value);
            entity.StartDate = Helper.GetDatePickerValue(txtStartDate.Text);
            entity.EndDate = Helper.GetDatePickerValue(txtEndDate.Text);
            entity.GCAbsenceReason = cboAbsenceReason.Value.ToString();
            entity.OtherAbsenceReason = txtOtherAbsenceReason.Text;
            entity.StartTime = txtStartTime.Text;
            entity.EndTime = txtEndTime.Text;
            entity.IsFullDay = chkIsFullDay.Checked;
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            TeacherAbsenceDao entityDao = new TeacherAbsenceDao(ctx);
            bool result = false;
            try
            {
                TeacherAbsence entity = new TeacherAbsence();
                ControlToEntity(entity);
                entity.SchoolPeriodID = Convert.ToInt32(hdnSchoolPeriodID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetTeacherAbsenceMaxID(ctx).ToString();
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            try
            {
                TeacherAbsence entity = BusinessLayer.GetTeacherAbsence(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateTeacherAbsence(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }
    }
}