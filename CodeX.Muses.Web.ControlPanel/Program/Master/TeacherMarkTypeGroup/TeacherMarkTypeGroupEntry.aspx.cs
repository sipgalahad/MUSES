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
    public partial class TeacherMarkTypeGroupEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.TEACHER_MARK_TYPE_GROUP;
        }

        #region Html Getter
        
        #endregion

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                TeacherMarkTypeGroup entity = BusinessLayer.GetTeacherMarkTypeGroup(Convert.ToInt32(ID));
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }

            txtTeacherMarkTypeGroupName.Focus();
        }

        protected override void SetControlProperties()
        {
            
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtTeacherMarkTypeGroupName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtFinalMark, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtDisplayOrder, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(TeacherMarkTypeGroup entity)
        {
            txtTeacherMarkTypeGroupName.Text = entity.TeacherMarkTypeGroupName;
            txtFinalMark.Text = entity.FinalMarkPercentage.ToString();
            txtDisplayOrder.Text = entity.DisplayOrder.ToString();
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(TeacherMarkTypeGroup entity)
        {
            entity.TeacherMarkTypeGroupName = txtTeacherMarkTypeGroupName.Text;
            entity.FinalMarkPercentage = Convert.ToInt32(txtFinalMark.Text);
            entity.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            //string FilterExpression = string.Format("SchoolPeriodID = {0} AND PeriodSectionID = {1} AND PeriodNo = {2}",tacSchoolPeriod.Value, tacPeriodSection.Value, cboMonth.Value);
            //List<TeacherMarkTypeGroup> lst = BusinessLayer.GetTeacherMarkTypeGroupList(FilterExpression);

            //if (lst.Count > 0)
            //    errMessage = " Teacher Mark with SchoolPeriod " + tacSchoolPeriod.Text + " AND Period Section " + tacPeriodSection.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            //Int32 ID = Convert.ToInt32(hdnID.Value);
            //string FilterExpression = string.Format("SchoolPeriodID = {0} AND PeriodSectionID = {1} AND PeriodNo = {2} AND TeacherMarkTypeGroupID != {3}", tacSchoolPeriod.Value, tacPeriodSection.Value, cboMonth.Value, ID);
            //List<TeacherMarkTypeGroup> lst = BusinessLayer.GetTeacherMarkTypeGroupList(FilterExpression);

            //if (lst.Count > 0)
            //    errMessage = " Teacher Mark with SchoolPeriod " + tacSchoolPeriod.Text + " AND Period Section " + tacPeriodSection.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            TeacherMarkTypeGroupDao entityDao = new TeacherMarkTypeGroupDao(ctx);
            bool result = false;
            try
            {
                TeacherMarkTypeGroup entity = new TeacherMarkTypeGroup();
                ControlToEntity(entity);
                entity.SiteID = AppSession.UserLogin.SiteID;
                entity.LastUpdatedBy = entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetTeacherMarkTypeGroupMaxID(ctx).ToString();
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
            TeacherMarkTypeGroupDao entityDao = new TeacherMarkTypeGroupDao(ctx);
            try
            {
                TeacherMarkTypeGroup entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);
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