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
    public partial class StudentFinalMarkFormulaHdEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.STUDENT_FINAL_MARK_FORMULA;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                StudentFinalMarkFormulaHd entity = BusinessLayer.GetStudentFinalMarkFormulaHd(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
            }
            txtStudentFinalMarkFormulaCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtStudentFinalMarkFormulaCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtStudentFinalMarkFormulaName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(StudentFinalMarkFormulaHd entity)
        {
            txtStudentFinalMarkFormulaCode.Text = entity.StudentFinalMarkFormulaCode;
            txtStudentFinalMarkFormulaName.Text = entity.StudentFinalMarkFormulaName;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(StudentFinalMarkFormulaHd entity)
        {
            entity.StudentFinalMarkFormulaCode = txtStudentFinalMarkFormulaCode.Text;
            entity.StudentFinalMarkFormulaName = txtStudentFinalMarkFormulaName.Text;
            entity.Remarks = txtRemarks.Text;

        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("StudentFinalMarkFormulaCode = '{0}'", txtStudentFinalMarkFormulaCode.Text);
            List<StudentFinalMarkFormulaHd> lst = BusinessLayer.GetStudentFinalMarkFormulaHdList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " StudentFinalMarkFormulaHd With Code " + txtStudentFinalMarkFormulaCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("StudentFinalMarkFormulaCode = '{0}' AND StudentFinalMarkFormulaID != {1}", txtStudentFinalMarkFormulaCode.Text, hdnID.Value);
            List<StudentFinalMarkFormulaHd> lst = BusinessLayer.GetStudentFinalMarkFormulaHdList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " StudentFinalMarkFormulaHd With Code " + txtStudentFinalMarkFormulaCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            StudentFinalMarkFormulaHdDao entityDao = new StudentFinalMarkFormulaHdDao(ctx);
            bool result = false;
            try
            {
                StudentFinalMarkFormulaHd entity = new StudentFinalMarkFormulaHd();
                ControlToEntity(entity);
                entity.SiteID = AppSession.UserLogin.SiteID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetStudentFinalMarkFormulaHdMaxID(ctx).ToString();
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
                StudentFinalMarkFormulaHd entity = BusinessLayer.GetStudentFinalMarkFormulaHd(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateStudentFinalMarkFormulaHd(entity);
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