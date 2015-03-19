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

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class StudentProgressRuleEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.STUDENT_PROGRESS_RULE;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                StudentProgressRuleHd entity = BusinessLayer.GetStudentProgressRuleHd(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
            }
            txtStudentProgressRuleCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtStudentProgressRuleCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtStudentProgressRuleName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(StudentProgressRuleHd entity)
        {
            txtStudentProgressRuleCode.Text = entity.StudentProgressRuleCode;
            txtStudentProgressRuleName.Text = entity.StudentProgressRuleName;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(StudentProgressRuleHd entity)
        {
            entity.StudentProgressRuleCode = txtStudentProgressRuleCode.Text;
            entity.StudentProgressRuleName = txtStudentProgressRuleName.Text;
            entity.Remarks = txtRemarks.Text;

        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("StudentProgressRuleCode = '{0}'", txtStudentProgressRuleCode.Text);
            List<StudentProgressRuleHd> lst = BusinessLayer.GetStudentProgressRuleHdList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " StudentProgressRuleHd With Code " + txtStudentProgressRuleCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("StudentProgressRuleCode = '{0}' AND StudentProgressRuleID != {1}", txtStudentProgressRuleCode.Text, hdnID.Value);
            List<StudentProgressRuleHd> lst = BusinessLayer.GetStudentProgressRuleHdList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " StudentProgressRuleHd With Code " + txtStudentProgressRuleCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            StudentProgressRuleHdDao entityDao = new StudentProgressRuleHdDao(ctx);
            bool result = false;
            try
            {
                StudentProgressRuleHd entity = new StudentProgressRuleHd();
                ControlToEntity(entity);
                entity.SiteID = AppSession.UserLogin.SiteID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetStudentProgressRuleHdMaxID(ctx).ToString();
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
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
                StudentProgressRuleHd entity = BusinessLayer.GetStudentProgressRuleHd(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateStudentProgressRuleHd(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
    }
}