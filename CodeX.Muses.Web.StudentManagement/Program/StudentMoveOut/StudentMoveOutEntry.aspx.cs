using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;
using System.Web.UI.HtmlControls;
using CodeX.Data.Core.Dal;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class StudentMoveOutEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.STUDENT_MOVE_OUT;
        }

        protected string OnGetStudentFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND GCStudentStatus = '{1}' AND IsDeleted = 0", AppSession.UserLogin.SiteID, Constant.StudentStatus.ACTIVE);
        }

        protected string OnGetStudentMoveOutReasonOther()
        {
            return Constant.StudentMoveOutReason.OTHER;
        }

        protected override void InitializeDataControl()
        {
            txtMoveOutDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);

            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.STUDENT_MOVE_OUT_REASON));
            Methods.SetComboBoxField<StandardCode>(cboGCMoveOutReason, lstSc, "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(tacStudent, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtMoveOutDate, new ControlEntrySetting(true, true, true, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(cboGCMoveOutReason, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtMoveOutReason, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        #region Load Entity
        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string studentID = tacStudent.Value;
            if (studentID != "")
            {
                vStudentMoveOut entity = BusinessLayer.GetvStudentMoveOutList(string.Format("StudentID = {0}", studentID)).FirstOrDefault();
                if (entity != null)
                    EntityToControl(entity, ref isShowWatermark, ref watermarkText);
                else
                    InitAddControl();
            }
            else
                InitAddControl();
        }

        private void InitAddControl()
        {
            hdnID.Value = "";
        }

        private void EntityToControl(vStudentMoveOut entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
                isShowWatermark = true;
            else
                isShowWatermark = false;
            tacStudent.Value = entity.StudentID.ToString();
            tacStudent.Text = entity.StudentName;
            hdnID.Value = entity.StudentMoveOutID.ToString();
            txtMoveOutDate.Text = entity.MoveOutDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            cboGCMoveOutReason.Value = entity.GCMoveOutReason;
            if (entity.GCMoveOutReason == Constant.StudentMoveOutReason.OTHER)
            {
                txtMoveOutReason.Style.Remove("display");
                txtMoveOutReason.Text = entity.MoveOutReason;
            }
            else
            {
                txtMoveOutReason.Style.Add("display", "none");
                txtMoveOutReason.Text = "";
            }
            txtRemarks.Text = entity.Remarks;
        }
        #endregion

        private void ControlToEntity(StudentMoveOut entity)
        {
            entity.MoveOutDate = Helper.GetDatePickerValue(txtMoveOutDate);
            entity.GCMoveOutReason = cboGCMoveOutReason.Value.ToString();
            if (entity.GCMoveOutReason == Constant.StudentMoveOutReason.OTHER)
                entity.MoveOutReason = txtMoveOutReason.Text;
            else
                entity.MoveOutReason = null;
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            StudentMoveOutDao entityDao = new StudentMoveOutDao(ctx);
            try
            {
                StudentMoveOut entity = new StudentMoveOut();
                ControlToEntity(entity);
                entity.StudentID = Convert.ToInt32(tacStudent.Value);
                entity.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                ctx.RollBackTransaction();
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            StudentMoveOutDao entityDao = new StudentMoveOutDao(ctx);
            try
            {
                StudentMoveOut entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                ctx.RollBackTransaction();
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
    }
}