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
    public partial class UpdateJobLevelPerformanceIndicatorRenumerationEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.HumanResources.UPDATE_JOB_LEVEL_WORKS_YEARS_RENUMERATION;
        }

        #region Html Getter
        protected string OnGetRenumerationFilterExpression()
        {
            return string.Format("GCRenumerationCompSource = '{0}' AND IsDeleted = 0", Constant.RenumerationCompSource.JOB_LEVEL);
        }
        #endregion

        protected override void InitializeDataControl()
        {
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();

            SetControlProperties();
            hdnIsEditable.Value = "1";

            BindGridView(1, true, ref PageCount, ref RowCount);

            Helper.SetControlEntrySetting(tacPerformanceIndicatorID, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        protected override void SetControlProperties()
        {
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnTransactionID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtTransactionNo, new ControlEntrySetting(false, false, false, ""));
            SetControlEntrySetting(txtTransactionDate, new ControlEntrySetting(true, true, false, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtStartEffectiveDate, new ControlEntrySetting(true, true, false, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(tacRenumeration, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false, ""));
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
            string filterExpression = String.Format("");
            return filterExpression;
        }
        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvTransJobLevelPositionRenumerationHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vTransJobLevelPerformanceIndicatorRenumerationHd entity = BusinessLayer.GetvTransJobLevelPerformanceIndicatorRenumerationHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvTransJobLevelPositionRenumerationHdRowIndex(filterExpression, keyValue, "TransactionID DESC");
            vTransJobLevelPerformanceIndicatorRenumerationHd entity = BusinessLayer.GetvTransJobLevelPerformanceIndicatorRenumerationHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vTransJobLevelPerformanceIndicatorRenumerationHd entity, ref bool isShowWatermark, ref string watermarkText)
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
            txtStartEffectiveDate.Text = entity.StartEffectiveDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtTransactionDate.Text = entity.TransactionDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            tacRenumeration.Value = entity.RenumerationID.ToString();
            tacRenumeration.Text = entity.RenumerationName;
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
                rowCount = BusinessLayer.GetvTransJobLevelPerformanceIndicatorRenumerationDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vTransJobLevelPerformanceIndicatorRenumerationDt> lstEntity = BusinessLayer.GetvTransJobLevelPerformanceIndicatorRenumerationDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "JobLevelPerformanceIndicatorID ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
        #endregion

        #region Save Header
        public void SaveTransJobLevelPerformanceIndicatorRenumerationHd(IDbContext ctx, ref int TransactionID)
        {
            TransJobLevelPerformanceIndicatorRenumerationHdDao entityHdDao = new TransJobLevelPerformanceIndicatorRenumerationHdDao(ctx);
            if (hdnTransactionID.Value == "0")
            {
                TransJobLevelPerformanceIndicatorRenumerationHd entityHd = new TransJobLevelPerformanceIndicatorRenumerationHd();
                entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
                entityHd.StartEffectiveDate = Helper.GetDatePickerValue(Request.Form[txtStartEffectiveDate.UniqueID]);
                entityHd.RenumerationID = Convert.ToInt32(tacRenumeration.Value);
                entityHd.Remarks = txtRemarks.Text;
                entityHd.TransactionNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.JOB_LEVEL_PERFORMANCE_INDICATOR, entityHd.TransactionDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;

                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);
                TransactionID = BusinessLayer.GetTransJobLevelPerformanceIndicatorRenumerationHdMaxID(ctx);
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
                SaveTransJobLevelPerformanceIndicatorRenumerationHd(ctx, ref OrderID);
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
                TransJobLevelPerformanceIndicatorRenumerationHd entityHd = BusinessLayer.GetTransJobLevelPerformanceIndicatorRenumerationHd(Convert.ToInt32(hdnTransactionID.Value));
                entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
                entityHd.StartEffectiveDate = Helper.GetDatePickerValue(Request.Form[txtStartEffectiveDate.UniqueID]);
                entityHd.RenumerationID = Convert.ToInt32(tacRenumeration.Value);
                entityHd.Remarks = txtRemarks.Text;
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateTransJobLevelPerformanceIndicatorRenumerationHd(entityHd);
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
            TransJobLevelPerformanceIndicatorRenumerationHdDao transJobLevelPerformanceIndicatorRenumerationHdDao = new TransJobLevelPerformanceIndicatorRenumerationHdDao(ctx);
            JobLevelPerformanceIndicatorDao jobLevelPerformanceIndicatorDao = new JobLevelPerformanceIndicatorDao(ctx);
            try
            {
                TransJobLevelPerformanceIndicatorRenumerationHd transJobLevelPerformanceIndicatorRenumerationHd = transJobLevelPerformanceIndicatorRenumerationHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                transJobLevelPerformanceIndicatorRenumerationHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                transJobLevelPerformanceIndicatorRenumerationHd.Remarks = txtRemarks.Text;
                transJobLevelPerformanceIndicatorRenumerationHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                transJobLevelPerformanceIndicatorRenumerationHdDao.Update(transJobLevelPerformanceIndicatorRenumerationHd);

                if (String.Compare(transJobLevelPerformanceIndicatorRenumerationHd.StartEffectiveDate.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd")) <= 0)
                {
                    List<JobLevelPerformanceIndicator> lstPerformanceIndicatorHd = BusinessLayer.GetJobLevelPerformanceIndicatorList(String.Format("JobLevelPerformanceIndicatorID IN (SELECT JobLevelPerformanceIndicatorID FROM TransJobLevelPerformanceIndicatorRenumerationDt WHERE TransactionID = {0})", hdnTransactionID.Value), ctx);
                    foreach (JobLevelPerformanceIndicator performanceIndicatorHd in lstPerformanceIndicatorHd)
                    {
                        performanceIndicatorHd.CurrentTransactionID = Convert.ToInt32(hdnTransactionID.Value);
                        performanceIndicatorHd.LastProcessedDate = DateTime.Now;
                        jobLevelPerformanceIndicatorDao.Update(performanceIndicatorHd);
                    }
                }

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
            TransJobLevelPerformanceIndicatorRenumerationHdDao transJobLevelPerformanceIndicatorRenumerationHdDao = new TransJobLevelPerformanceIndicatorRenumerationHdDao(ctx);
            
            try
            {
                TransJobLevelPerformanceIndicatorRenumerationHd transJobLevelPerformanceIndicatorRenumerationHd = transJobLevelPerformanceIndicatorRenumerationHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                transJobLevelPerformanceIndicatorRenumerationHd.GCTransactionStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                transJobLevelPerformanceIndicatorRenumerationHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                transJobLevelPerformanceIndicatorRenumerationHdDao.Update(transJobLevelPerformanceIndicatorRenumerationHd);

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
            TransJobLevelPerformanceIndicatorRenumerationHdDao transJobLevelPerformanceIndicatorRenumerationHdDao = new TransJobLevelPerformanceIndicatorRenumerationHdDao(ctx);
            
            try
            {
                TransJobLevelPerformanceIndicatorRenumerationHd transJobLevelPerformanceIndicatorRenumerationHd = transJobLevelPerformanceIndicatorRenumerationHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                if (String.Compare(transJobLevelPerformanceIndicatorRenumerationHd.StartEffectiveDate.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd")) <= 0)
                {
                    result = false;
                    errMessage = "Transaksi Sudah Diproses, Tidak Dapat Diubah";
                }
                else 
                {
                    transJobLevelPerformanceIndicatorRenumerationHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                    transJobLevelPerformanceIndicatorRenumerationHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    transJobLevelPerformanceIndicatorRenumerationHdDao.Update(transJobLevelPerformanceIndicatorRenumerationHd);
                }


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
            TransJobLevelPerformanceIndicatorRenumerationHdDao transJobLevelPerformanceIndicatorRenumerationHdDao = new TransJobLevelPerformanceIndicatorRenumerationHdDao(ctx);
            
            try
            {
                TransJobLevelPerformanceIndicatorRenumerationHd transJobLevelPerformanceIndicatorRenumerationHd = transJobLevelPerformanceIndicatorRenumerationHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                transJobLevelPerformanceIndicatorRenumerationHd.GCTransactionStatus = Constant.TransactionStatus.VOID;
                transJobLevelPerformanceIndicatorRenumerationHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                transJobLevelPerformanceIndicatorRenumerationHdDao.Update(transJobLevelPerformanceIndicatorRenumerationHd);

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

        private void ControlToEntity(TransJobLevelPerformanceIndicatorRenumerationDt entityDt)
        {
            entityDt.JobLevelPerformanceIndicatorID = Convert.ToInt32(tacPerformanceIndicatorID.Value);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int TransactionID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransJobLevelPerformanceIndicatorRenumerationDtDao entityDtDao = new TransJobLevelPerformanceIndicatorRenumerationDtDao(ctx);
            try
            {
                SaveTransJobLevelPerformanceIndicatorRenumerationHd(ctx, ref TransactionID);
                TransJobLevelPerformanceIndicatorRenumerationDt entityDt = new TransJobLevelPerformanceIndicatorRenumerationDt();
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
                BusinessLayer.DeleteTransJobLevelPerformanceIndicatorRenumerationDt(Convert.ToInt32(hdnTransactionID.Value), Convert.ToInt32(hdnEntryID.Value));
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