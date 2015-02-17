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

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class TeacherMarkTypeItemEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.TEACHER_MARK_TYPE_ITEM;
        }
        
        #region Html Getter
        protected String OnGetTeacherMarkTypeGroupFilterExpression() 
        { 
            return String.Format("SiteID = '{0}' AND IsDeleted = 0",AppSession.UserLogin.SiteID);
        }
        #endregion

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                String filterExpression = String.Format("TeacherMarkTypeItemID = {0}", Convert.ToInt32(ID));
                vTeacherMarkTypeItem entity = BusinessLayer.GetvTeacherMarkTypeItemList(filterExpression)[0];
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            
            txtTeacherMarkTypeItemName.Focus();
        }

        protected override void SetControlProperties()
        {
            
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(tacTeacherMarkTypeGroup, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtTeacherMarkTypeItemName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtFinalMark, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(vTeacherMarkTypeItem entity)
        {
            tacTeacherMarkTypeGroup.Text = entity.TeacherMarkTypeGroupName;
            tacTeacherMarkTypeGroup.Value = entity.TeacherMarkTypeGroupID.ToString();
            txtTeacherMarkTypeItemName.Text = entity.TeacherMarkTypeItemName;
            txtFinalMark.Text = entity.FinalMarkPercentage.ToString();
        }

        private void ControlToEntity(TeacherMarkTypeItem entity)
        {
            entity.TeacherMarkTypeGroupID = Convert.ToInt32(tacTeacherMarkTypeGroup.Value);
            entity.TeacherMarkTypeItemName = txtTeacherMarkTypeItemName.Text;
            entity.FinalMarkPercentage = Convert.ToInt32(txtFinalMark.Text);
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            //string FilterExpression = string.Format("SchoolPeriodID = {0} AND PeriodSectionID = {1} AND PeriodNo = {2}",tacSchoolPeriod.Value, tacPeriodSection.Value, cboMonth.Value);
            //List<TeacherMarkTypeItem> lst = BusinessLayer.GetTeacherMarkTypeItemList(FilterExpression);

            //if (lst.Count > 0)
            //    errMessage = " Teacher Mark with SchoolPeriod " + tacSchoolPeriod.Text + " AND Period Section " + tacPeriodSection.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            //Int32 ID = Convert.ToInt32(hdnID.Value);
            //string FilterExpression = string.Format("SchoolPeriodID = {0} AND PeriodSectionID = {1} AND PeriodNo = {2} AND TeacherMarkTypeItemID != {3}", tacSchoolPeriod.Value, tacPeriodSection.Value, cboMonth.Value, ID);
            //List<TeacherMarkTypeItem> lst = BusinessLayer.GetTeacherMarkTypeItemList(FilterExpression);

            //if (lst.Count > 0)
            //    errMessage = " Teacher Mark with SchoolPeriod " + tacSchoolPeriod.Text + " AND Period Section " + tacPeriodSection.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            TeacherMarkTypeItemDao teacherMarkDao = new TeacherMarkTypeItemDao(ctx);
            bool result = false;
            try
            {
                TeacherMarkTypeItem entity = new TeacherMarkTypeItem();
                ControlToEntity(entity);
                entity.IsDeleted = false;
                entity.LastUpdatedBy = entity.CreatedBy = AppSession.UserLogin.UserID;
                teacherMarkDao.Insert(entity);
                //retval = BusinessLayer.GetTeacherMarkMaxID(ctx).ToString();
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
            TeacherMarkTypeItemDao teacherMarkDao = new TeacherMarkTypeItemDao(ctx);
            try
            {
                TeacherMarkTypeItem entity = teacherMarkDao.Get(Convert.ToInt32(hdnID.Value));
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