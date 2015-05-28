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
using System.Data;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;

namespace CodeX.Muses.Web.Finance.Program
{
    public partial class StudentScholarshipTransactionEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Finance.STUDENT_SCHOLARSHIP_TRANSACTION;
        }

        #region Html Getter
        protected string OnGetBusinessPartnerFilterExpression()
        {
            return string.Format("GCBusinessPartnerType = '{0}' AND IsDeleted = 0", Constant.BusinessObjectType.CUSTOMER);
        }

        protected string OnGetStudentFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND GCStudentStatus = '{1}' AND IsDeleted = 0", AppSession.UserLogin.SiteID, Constant.StudentStatus.ACTIVE);
        }
        #endregion

        protected override void InitializeDataControl()
        {
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();

            hdnIsEditable.Value = "1";

            BindGridView(1, true, ref PageCount, ref RowCount);

            Helper.SetControlEntrySetting(tacScholarship, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnTransactionID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtTransactionNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtTransactionDate, new ControlEntrySetting(true, false, true, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtStartingDate, new ControlEntrySetting(true, true, true, Constant.DefaultValueEntry.DATE_NOW));
            
            SetControlEntrySetting(txtReferenceNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        #region Load Entity
        public override void OnAddRecord()
        {
            hdnPageCount.Value = "0";
            hdnIsEditable.Value = "1";
        }

        protected string IsEditable()
        {
            return hdnIsEditable.Value;
        }

        protected string GetFilterExpression()
        {
            string filterExpression = "";
            return filterExpression;
        }

        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvStudentScholarshipTransactionHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vStudentScholarshipTransactionHd entity = BusinessLayer.GetvStudentScholarshipTransactionHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvStudentScholarshipTransactionHdRowIndex(filterExpression, keyValue, "TransactionID DESC");
            vStudentScholarshipTransactionHd entity = BusinessLayer.GetvStudentScholarshipTransactionHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vStudentScholarshipTransactionHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
                hdnIsEditable.Value = "0";
            }
            else
                hdnIsEditable.Value = "1";
            hdnTransactionID.Value = entity.TransactionID.ToString();
            txtTransactionNo.Text = entity.TransactionNo;
            txtTransactionDate.Text = entity.TransactionDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtStartingDate.Text = entity.StartingDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtReferenceNo.Text = entity.ReferenceNo;
            txtRemarks.Text = entity.Remarks;

            BindGridView(1, true, ref PageCount, ref RowCount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnTransactionID.Value != "")
                filterExpression = string.Format("TransactionID = {0}", hdnTransactionID.Value);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvStudentScholarshipTransactionDtCustomRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vStudentScholarshipTransactionDtCustom> lstEntity = BusinessLayer.GetvStudentScholarshipTransactionDtCustomList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
        #endregion

        #region Save Header
        public void SaveStudentScholarshipTransactionHd(IDbContext ctx, ref int TransactionID)
        {
            StudentScholarshipTransactionHdDao entityHdDao = new StudentScholarshipTransactionHdDao(ctx);
            if (hdnTransactionID.Value == "0")
            {
                StudentScholarshipTransactionHd entityHd = new StudentScholarshipTransactionHd();
                entityHd.TransactionDate = Helper.GetDatePickerValue(txtTransactionDate.Text);
                entityHd.StartingDate = Helper.GetDatePickerValue(txtStartingDate.Text);
                entityHd.ReferenceNo = txtReferenceNo.Text;
                entityHd.Remarks = txtRemarks.Text;

                entityHd.TransactionCode = Constant.TransactionCode.STUDENT_SCHOLARSHIP;
                entityHd.TransactionNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.STUDENT_SCHOLARSHIP, entityHd.TransactionDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;

                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);
                TransactionID = BusinessLayer.GetStudentScholarshipTransactionHdMaxID(ctx);
            }
            else
            {
                TransactionID = Convert.ToInt32(hdnTransactionID.Value);
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            try
            {
                int OrderID = 0;
                SaveStudentScholarshipTransactionHd(ctx, ref OrderID);
                retval = OrderID.ToString();
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                errMessage = ex.Message;
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
            try
            {
                StudentScholarshipTransactionHd entityHd = BusinessLayer.GetStudentScholarshipTransactionHd(Convert.ToInt32(hdnTransactionID.Value));
                entityHd.ReferenceNo = txtReferenceNo.Text;
                entityHd.StartingDate = Helper.GetDatePickerValue(txtStartingDate.Text);
                entityHd.Remarks = txtRemarks.Text;
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateStudentScholarshipTransactionHd(entityHd);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        protected override bool OnApproveRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            StudentScholarshipTransactionHdDao itemTransactionHdDao = new StudentScholarshipTransactionHdDao(ctx);
            StudentScholarshipTransactionDtDao itemTransactionDtDao = new StudentScholarshipTransactionDtDao(ctx);
            try
            {
                StudentScholarshipTransactionHd itemTransactionHd = itemTransactionHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                itemTransactionHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                itemTransactionHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                itemTransactionHdDao.Update(itemTransactionHd);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override bool OnProposeRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            StudentScholarshipTransactionHdDao itemTransactionHdDao = new StudentScholarshipTransactionHdDao(ctx);
            StudentScholarshipTransactionDtDao itemTransactionDtDao = new StudentScholarshipTransactionDtDao(ctx);
            try
            {
                StudentScholarshipTransactionHd itemTransactionHd = itemTransactionHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                itemTransactionHd.GCTransactionStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                itemTransactionHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                itemTransactionHdDao.Update(itemTransactionHd);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override bool OnReopenRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            StudentScholarshipTransactionHdDao itemTransactionHdDao = new StudentScholarshipTransactionHdDao(ctx);
            StudentScholarshipTransactionDtDao itemTransactionDtDao = new StudentScholarshipTransactionDtDao(ctx);
            try
            {
                StudentScholarshipTransactionHd itemTransactionHd = itemTransactionHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                itemTransactionHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                itemTransactionHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                itemTransactionHdDao.Update(itemTransactionHd);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override bool OnVoidRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            StudentScholarshipTransactionHdDao itemTransactionHdDao = new StudentScholarshipTransactionHdDao(ctx);
            StudentScholarshipTransactionDtDao itemTransactionDtDao = new StudentScholarshipTransactionDtDao(ctx);
            try
            {
                StudentScholarshipTransactionHd itemTransactionHd = itemTransactionHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                itemTransactionHd.GCTransactionStatus = Constant.TransactionStatus.VOID;
                itemTransactionHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                itemTransactionHdDao.Update(itemTransactionHd);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        #endregion

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            int transactionID = 0;
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    transactionID = Convert.ToInt32(hdnTransactionID.Value);
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage, ref transactionID))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                transactionID = Convert.ToInt32(hdnTransactionID.Value);
                if (OnDeleteEntityDt(ref errMessage, transactionID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpTransactionID"] = transactionID.ToString();
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int TransactionID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            StudentScholarshipTransactionDtDao entityDao = new StudentScholarshipTransactionDtDao(ctx);
            try
            {
                SaveStudentScholarshipTransactionHd(ctx, ref TransactionID);
                int scholarshipID = Convert.ToInt32(hdnScholarshipID.Value);
                string[] lstStudentID = hdnStudentSave.Value.Split(',');
                foreach (string studentID in lstStudentID)
                {
                    StudentScholarshipTransactionDt entity = new StudentScholarshipTransactionDt();
                    entity.TransactionID = TransactionID;
                    entity.ScholarshipID = scholarshipID;
                    entity.StudentID = Convert.ToInt32(studentID);
                    entityDao.Insert(entity);
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                result = false;
                errMessage = ex.Message;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            StudentScholarshipTransactionDtDao entityDao = new StudentScholarshipTransactionDtDao(ctx);
            try
            {
                int TransactionID = Convert.ToInt32(hdnTransactionID.Value);
                int scholarshipID = Convert.ToInt32(hdnScholarshipID.Value);

                List<StudentScholarshipTransactionDt> lstEntityDt = BusinessLayer.GetStudentScholarshipTransactionDtList(string.Format("TransactionID = {0} AND ScholarshipID = {1}", TransactionID, scholarshipID), ctx);
                if (hdnStudentSave.Value != "")
                {
                    string[] lstStudentID = hdnStudentSave.Value.Split(',');
                    foreach (string studentID in lstStudentID)
                    {
                        StudentScholarshipTransactionDt entity = lstEntityDt.FirstOrDefault(p => p.StudentID == Convert.ToInt32(studentID));
                        if (entity == null)
                        {
                            entity = new StudentScholarshipTransactionDt();
                            entity.TransactionID = TransactionID;
                            entity.ScholarshipID = scholarshipID;
                            entity.StudentID = Convert.ToInt32(studentID);
                            entityDao.Insert(entity);
                        }
                        else
                            lstEntityDt.Remove(entity);
                    }
                }

                foreach (StudentScholarshipTransactionDt entity in lstEntityDt)
                {
                    entityDao.Delete(entity.ID);
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                result = false;
                errMessage = ex.Message;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnDeleteEntityDt(ref string errMessage, int ID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            StudentScholarshipTransactionDtDao entityDao = new StudentScholarshipTransactionDtDao(ctx);
            try
            {
                int TransactionID = Convert.ToInt32(hdnTransactionID.Value);
                int scholarshipID = Convert.ToInt32(hdnEntryID.Value);
                List<StudentScholarshipTransactionDt> lstEntityDt = BusinessLayer.GetStudentScholarshipTransactionDtList(string.Format("TransactionID = {0} AND ScholarshipID = {1}", TransactionID, scholarshipID), ctx);
                foreach (StudentScholarshipTransactionDt entity in lstEntityDt)
                {
                    entityDao.Delete(entity.ID);
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion

        #region Callback
        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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
        #endregion
    }
}