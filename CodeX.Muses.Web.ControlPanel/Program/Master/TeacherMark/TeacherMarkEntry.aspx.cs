using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Common;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;
using System.Globalization;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxCallbackPanel;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class TeacherMarkEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.TEACHER_MARK;
        }

        #region Html Getter
        protected string OnSchoolPeriodStatusStart() 
        {
            return Constant.SchoolPeriodStatus.START;
        }

        protected String OnGetSchoolPeriodFilterExpression() 
        {
            return String.Format("GCSchoolPeriodStatus = '{0}' AND SiteID = '{1}'", Constant.SchoolPeriodStatus.START, AppSession.UserLogin.SiteID);
        }
        #endregion

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                String filterExpression = String.Format("TeacherMarkID = {0}", Convert.ToInt32(ID));
                vTeacherMark entity = BusinessLayer.GetvTeacherMarkList(filterExpression)[0];
                SetControlProperties();
                EntityToControl(entity);
                cboMonth.Value = entity.PeriodNo.Substring(4, 2);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            
            //txtTeacherCode.Focus();
        }

        protected override void SetControlProperties()
        {
            RefreshCboMonth();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            RefreshCboMonth();
        }

        private void RefreshCboMonth() 
        {
            Int32 startMonth = 1;
            Int32 endMonth = 12;
            if (tacPeriodSection.Value != "" && tacSchoolPeriod.Value != "")
            {
                PeriodSection ps = BusinessLayer.GetPeriodSectionList(String.Format("SchoolPeriodID = {0} AND PeriodSectionID = {1}", tacSchoolPeriod.Value, tacPeriodSection.Value))[0];
                startMonth = ps.StartDate.Month;
                endMonth = ps.EndDate.Month;
            }

            cboMonth.DataSource = Enumerable.Range(1, 12).Where(x => x >= startMonth && x <= endMonth).Select(a => new
            {
                MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(a),
                MonthNumber = a.ToString("00")
            });
            cboMonth.TextField = "MonthName";
            cboMonth.ValueField = "MonthNumber";
            cboMonth.EnableCallbackMode = false;
            cboMonth.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboMonth.DropDownStyle = DropDownStyle.DropDownList;
            cboMonth.DataBind();
            cboMonth.SelectedIndex = 0;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(tacSchoolPeriod, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(tacPeriodSection, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboMonth, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtFinalMark, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(vTeacherMark entity)
        {
            tacSchoolPeriod.Text = entity.SchoolPeriodName;
            tacSchoolPeriod.Value = entity.SchoolPeriodID.ToString();
            tacPeriodSection.Text = entity.PeriodSectionName;
            tacPeriodSection.Value = entity.PeriodSectionID.ToString();
            hdnStartDate.Value = entity.StartDateInDatePickerFormat;
            hdnEndDate.Value = entity.EndDateInDatePickerFormat;
            txtFinalMark.Text = entity.FinalMark.ToString();
        }

        private void ControlToEntity(TeacherMark entity)
        {
            entity.SchoolPeriodID = Convert.ToInt32(tacSchoolPeriod.Value);
            entity.PeriodSectionID = Convert.ToInt32(tacPeriodSection.Value);
            DateTime startDate = Helper.GetDatePickerValue(hdnStartDate.Value);
            entity.PeriodNo = String.Format("{0}{1}", startDate.Year, cboMonth.Value);
            entity.FinalMark = Convert.ToInt32(txtFinalMark.Text);
            //entity.FinalMarkInString = Function.NumberInWords(Convert.ToInt64(entity.FinalMark));
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("SchoolPeriodID = {0} AND PeriodSectionID = {1} AND PeriodNo = {2}",tacSchoolPeriod.Value, tacPeriodSection.Value, cboMonth.Value);
            List<TeacherMark> lst = BusinessLayer.GetTeacherMarkList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Teacher Mark with SchoolPeriod " + tacSchoolPeriod.Text + " AND Period Section " + tacPeriodSection.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            Int32 ID = Convert.ToInt32(hdnID.Value);
            string FilterExpression = string.Format("SchoolPeriodID = {0} AND PeriodSectionID = {1} AND PeriodNo = {2} AND TeacherMarkID != {3}", tacSchoolPeriod.Value, tacPeriodSection.Value, cboMonth.Value, ID);
            List<TeacherMark> lst = BusinessLayer.GetTeacherMarkList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Teacher Mark with SchoolPeriod " + tacSchoolPeriod.Text + " AND Period Section " + tacPeriodSection.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            TeacherMarkDao teacherMarkDao = new TeacherMarkDao(ctx);
            bool result = false;
            try
            {
                TeacherMark entity = new TeacherMark();
                ControlToEntity(entity);
                entity.IsDeleted = false;
                entity.LastUpdatedBy = entity.CreatedBy = AppSession.UserLogin.UserID;
                teacherMarkDao.Insert(entity);
                retval = BusinessLayer.GetTeacherMarkMaxID(ctx).ToString();
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
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TeacherMarkDao teacherMarkDao = new TeacherMarkDao(ctx);
            try
            {
                TeacherMark entity = teacherMarkDao.Get(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                teacherMarkDao.Update(entity);
                ctx.CommitTransaction();
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
    }
}