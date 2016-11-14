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

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class OvertimeProposalEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int PageCount2 = 1;
        protected int RowCount2 = 1;

        protected string OnGetTransactionStatusApproved()
        {
            return Constant.TransactionStatus.APPROVED;
        }
            
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.HumanResources.OVERTIME_PROPOSAL;
        }


        protected override void InitializeDataControl()
        {
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();
            hdnRowCountPerPage2.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();

            SetControlProperties();
            hdnIsEditable.Value = "1";

            BindGridView(1, true, ref PageCount, ref RowCount);
            BindGridView2(1, true, ref PageCount2, ref RowCount2);

            Helper.SetControlEntrySetting(txtStartTime, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtEndTime, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtTotalHours, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtOvertimeDate, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(tacEmployeeID, new ControlEntrySetting(true, true, true), "mpTrx");

        }

        protected override void SetControlProperties()
        {

            List<StandardCode> listScOr = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.OVERTIME_REASON));
            Methods.SetComboBoxField<StandardCode>(cboGCOvertimeReason, listScOr, "StandardCodeName", "StandardCodeID");
        
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnTransactionID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtTransactionNo, new ControlEntrySetting(false, false, false, ""));
            SetControlEntrySetting(txtTransactionDate, new ControlEntrySetting(true, true, false, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false, ""));
            SetControlEntrySetting(cboGCOvertimeReason, new ControlEntrySetting(true, true, true));
        }

        #region Load Entity
        public override void OnAddRecord()
        {
            hdnPageCount.Value = "0";
            hdnPageCount2.Value = "0";
            hdnIsEditable.Value = "1";
        }

        protected string IsEditable()
        {
            return hdnIsEditable.Value;
        }

        protected string GetFilterExpression()
        {
            string filterExpression = String.Format("");
            return filterExpression;
        }
        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvOvertimeProposalHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vOvertimeProposalHd entity = BusinessLayer.GetvOvertimeProposalHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvOvertimeProposalHdRowIndex(filterExpression, keyValue, "TransactionID DESC");
            vOvertimeProposalHd entity = BusinessLayer.GetvOvertimeProposalHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vOvertimeProposalHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
                hdnIsEditable.Value = "0";
            }
            else
                hdnIsEditable.Value = "1";
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN || entity.GCTransactionStatus != Constant.TransactionStatus.VOID)
                hdnPrintStatus.Value = "true";
            else
                hdnPrintStatus.Value = "false";

            hdnTransactionID.Value = entity.TransactionID.ToString();
            txtTransactionNo.Text = entity.TransactionNo;
            txtTransactionDate.Text = entity.TransactionDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtRemarks.Text = entity.Remarks;
            cboGCOvertimeReason.Value = entity.GCOvertimeReason;

            BindGridView(1, true, ref PageCount, ref RowCount);
            BindGridView2(1, true, ref PageCount2, ref RowCount2);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
            hdnPageCount2.Value = PageCount2.ToString();
            hdnRowCount2.Value = RowCount2.ToString();
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnTransactionID.Value != "")
                filterExpression = string.Format("TransactionID = {0}", hdnTransactionID.Value);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvOvertimeProposalEmployeeRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vOvertimeProposalEmployee> lstEntity = BusinessLayer.GetvOvertimeProposalEmployeeList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "EmployeeName ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        private void BindGridView2(int pageIndex, bool isCountPageCount, ref int pageCount2, ref int rowCount2)
        {
            string filterExpression = "1 = 0";
            if (hdnTransactionID.Value != "")
                filterExpression = string.Format("TransactionID = {0} AND IsDeleted = 0", hdnTransactionID.Value);
            if (isCountPageCount)
            {
                rowCount2 = BusinessLayer.GetOvertimeProposalDateRowCount(filterExpression);
                pageCount2 = Helper.GetPageCount(rowCount2, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<OvertimeProposalDate> lstEntity2 = BusinessLayer.GetOvertimeProposalDateList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "OvertimeDate ASC");
            grdView2.DataSource = lstEntity2;
            grdView2.DataBind();
        }
        #endregion

        #region Save Header
        public void SaveOvertimeProposalHd(IDbContext ctx, ref int TransactionID)
        {
            OvertimeProposalHdDao entityHdDao = new OvertimeProposalHdDao(ctx);
            if (hdnTransactionID.Value == "0")
            {
                OvertimeProposalHd entityHd = new OvertimeProposalHd();
                entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
                entityHd.Remarks = txtRemarks.Text;
                entityHd.TransactionNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.OVERTIME_PROPOSAL, entityHd.TransactionDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                entityHd.GCOvertimeReason = cboGCOvertimeReason.Value.ToString();

                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);
                TransactionID = BusinessLayer.GetOvertimeProposalHdMaxID(ctx);
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
                SaveOvertimeProposalHd(ctx, ref OrderID);
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
                OvertimeProposalHd entityHd = BusinessLayer.GetOvertimeProposalHd(Convert.ToInt32(hdnTransactionID.Value));
                entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
                entityHd.Remarks = txtRemarks.Text;
                entityHd.GCOvertimeReason = cboGCOvertimeReason.Value.ToString();
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateOvertimeProposalHd(entityHd);
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
            OvertimeProposalHdDao overtimeProposalHdDao = new OvertimeProposalHdDao(ctx);
            try
            {
                OvertimeProposalHd overtimeProposalHd = overtimeProposalHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                overtimeProposalHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                overtimeProposalHd.Remarks = txtRemarks.Text;
                overtimeProposalHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                overtimeProposalHdDao.Update(overtimeProposalHd);
                

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
            OvertimeProposalHdDao overtimeProposalHdDao = new OvertimeProposalHdDao(ctx);
            
            try
            {
                OvertimeProposalHd overtimeProposalHd = overtimeProposalHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                overtimeProposalHd.GCTransactionStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                overtimeProposalHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                overtimeProposalHdDao.Update(overtimeProposalHd);

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
            OvertimeProposalHdDao overtimeProposalHdDao = new OvertimeProposalHdDao(ctx);
            
            try
            {
                OvertimeProposalHd overtimeProposalHd = overtimeProposalHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                overtimeProposalHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                overtimeProposalHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                overtimeProposalHdDao.Update(overtimeProposalHd);

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
            OvertimeProposalHdDao overtimeProposalHdDao = new OvertimeProposalHdDao(ctx);
            
            try
            {
                OvertimeProposalHd overtimeProposalHd = overtimeProposalHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                overtimeProposalHd.GCTransactionStatus = Constant.TransactionStatus.VOID;
                overtimeProposalHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                overtimeProposalHdDao.Update(overtimeProposalHd);

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
            int adjustmentID = 0;
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (OnSaveAddRecordEntityDt(ref errMessage, ref adjustmentID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else if (param[0] == "delete")
            {
                adjustmentID = Convert.ToInt32(hdnTransactionID.Value);
                if (OnDeleteEntityDt(ref errMessage, adjustmentID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpTransactionID"] = adjustmentID.ToString();
        }

        private void ControlToEntity(OvertimeProposalEmployee entityDt)
        {
            entityDt.EmployeeID = Convert.ToInt32(tacEmployeeID.Value);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int TransactionID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            OvertimeProposalEmployeeDao entityDtDao = new OvertimeProposalEmployeeDao(ctx);
            try
            {
                SaveOvertimeProposalHd(ctx, ref TransactionID);
                OvertimeProposalEmployee entityDt = new OvertimeProposalEmployee();
                ControlToEntity(entityDt);
                entityDt.TransactionID = TransactionID;
                entityDtDao.Insert(entityDt);
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
            try
            {
                BusinessLayer.DeleteOvertimeProposalEmployee(Convert.ToInt32(hdnTransactionID.Value), Convert.ToInt32(tacEmployeeID.Value));
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

        #region Process Detail2
        protected void cbpProcess2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            int adjustmentID = 0;
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    adjustmentID = Convert.ToInt32(hdnTransactionID.Value);
                    if (OnSaveEditRecordEntityDt2(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt2(ref errMessage, ref adjustmentID))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                adjustmentID = Convert.ToInt32(hdnTransactionID.Value);
                if (OnDeleteEntityDt2(ref errMessage, adjustmentID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult2"] = result;
            panel.JSProperties["cpTransactionID2"] = adjustmentID.ToString();
        }

        private void ControlToEntity2(OvertimeProposalDate entityDt)
        {
            entityDt.OvertimeDate = Helper.GetDatePickerValue(Request.Form[txtOvertimeDate.UniqueID]);
            entityDt.StartTime = txtStartTime.Text;
            entityDt.EndTime = txtEndTime.Text;
            entityDt.TotalHours = Convert.ToDecimal(txtTotalHours.Text);
        }


        private bool OnSaveAddRecordEntityDt2(ref string errMessage, ref int TransactionID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            OvertimeProposalDateDao entityDtDao = new OvertimeProposalDateDao(ctx);
            try
            {
                SaveOvertimeProposalHd(ctx, ref TransactionID);
                OvertimeProposalDate entityDt = new OvertimeProposalDate();
                ControlToEntity2(entityDt);
                entityDt.TransactionID = TransactionID;
                entityDt.CreatedBy = AppSession.UserLogin.UserID;
                entityDtDao.Insert(entityDt);
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

        private bool OnSaveEditRecordEntityDt2(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            OvertimeProposalDateDao entityDtDao = new OvertimeProposalDateDao(ctx);
            try
            {
                OvertimeProposalDate entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity2(entityDt);
                entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDtDao.Update(entityDt);
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

        private bool OnDeleteEntityDt2(ref string errMessage, int ID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            OvertimeProposalDateDao entityDtDao = new OvertimeProposalDateDao(ctx);
            try
            {
                OvertimeProposalDate entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDt.IsDeleted = true;
                entityDtDao.Update(entityDt);
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

        protected void cbpView2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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
            panel.JSProperties["cpResult2"] = result;
        }
        #endregion
    }
}