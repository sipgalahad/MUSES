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
    public partial class UpdateJobLevelWorkYearsRenumerationEntry : BasePageTrx
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

            Helper.SetControlEntrySetting(tacJobLevelWorkYearsID, new ControlEntrySetting(true, true, true), "mpTrx");
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
            vTransJobLevelWorkYearsRenumerationHd entity = BusinessLayer.GetvTransJobLevelWorkYearsRenumerationHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvTransJobLevelPositionRenumerationHdRowIndex(filterExpression, keyValue, "TransactionID DESC");
            vTransJobLevelWorkYearsRenumerationHd entity = BusinessLayer.GetvTransJobLevelWorkYearsRenumerationHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vTransJobLevelWorkYearsRenumerationHd entity, ref bool isShowWatermark, ref string watermarkText)
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
                rowCount = BusinessLayer.GetvTransJobLevelWorkYearsRenumerationDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vTransJobLevelWorkYearsRenumerationDt> lstEntity = BusinessLayer.GetvTransJobLevelWorkYearsRenumerationDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "JobLevelWorkYearsID ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
        #endregion

        #region Save Header
        public void SaveTransJobLevelWorkYearsRenumerationHd(IDbContext ctx, ref int TransactionID)
        {
            TransJobLevelWorkYearsRenumerationHdDao entityHdDao = new TransJobLevelWorkYearsRenumerationHdDao(ctx);
            if (hdnTransactionID.Value == "0")
            {
                TransJobLevelWorkYearsRenumerationHd entityHd = new TransJobLevelWorkYearsRenumerationHd();
                entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
                entityHd.StartEffectiveDate = Helper.GetDatePickerValue(Request.Form[txtStartEffectiveDate.UniqueID]);
                entityHd.RenumerationID = Convert.ToInt32(tacRenumeration.Value);
                entityHd.Remarks = txtRemarks.Text;
                entityHd.TransactionNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.JOB_LEVEL_WORK_YEARS, entityHd.TransactionDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;

                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);
                TransactionID = BusinessLayer.GetTransJobLevelWorkYearsRenumerationHdMaxID(ctx);
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
                SaveTransJobLevelWorkYearsRenumerationHd(ctx, ref OrderID);
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
                TransJobLevelWorkYearsRenumerationHd entityHd = BusinessLayer.GetTransJobLevelWorkYearsRenumerationHd(Convert.ToInt32(hdnTransactionID.Value));
                entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
                entityHd.StartEffectiveDate = Helper.GetDatePickerValue(Request.Form[txtStartEffectiveDate.UniqueID]);
                entityHd.RenumerationID = Convert.ToInt32(tacRenumeration.Value);
                entityHd.Remarks = txtRemarks.Text;
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateTransJobLevelWorkYearsRenumerationHd(entityHd);
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
            TransJobLevelWorkYearsRenumerationHdDao transJobLevelWorkYearsRenumerationHdDao = new TransJobLevelWorkYearsRenumerationHdDao(ctx);
            JobLevelWorkYearsDao jobLevelWorkYearsDao = new JobLevelWorkYearsDao(ctx);
            try
            {
                TransJobLevelWorkYearsRenumerationHd transJobLevelWorkYearsRenumerationHd = transJobLevelWorkYearsRenumerationHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                transJobLevelWorkYearsRenumerationHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                transJobLevelWorkYearsRenumerationHd.Remarks = txtRemarks.Text;
                transJobLevelWorkYearsRenumerationHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                transJobLevelWorkYearsRenumerationHdDao.Update(transJobLevelWorkYearsRenumerationHd);

                if (String.Compare(transJobLevelWorkYearsRenumerationHd.StartEffectiveDate.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd")) <= 0)
                {
                    List<JobLevelWorkYears> lstJobLevel = BusinessLayer.GetJobLevelWorkYearsList(String.Format("JobLevelWorkYearsID IN (SELECT JobLevelPositionID FROM TransJobLevelWorkYearsRenumerationDt WHERE TransactionID = {0})", hdnTransactionID.Value), ctx);
                    foreach (JobLevelWorkYears jobLevelWorkYears in lstJobLevel)
                    {
                        jobLevelWorkYears.CurrentTransactionID = Convert.ToInt32(hdnTransactionID.Value);
                        jobLevelWorkYears.LastProcessedDate = DateTime.Now;
                        jobLevelWorkYearsDao.Update(jobLevelWorkYears);
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
            TransJobLevelWorkYearsRenumerationHdDao transJobLevelWorkYearsRenumerationHdDao = new TransJobLevelWorkYearsRenumerationHdDao(ctx);
            
            try
            {
                TransJobLevelWorkYearsRenumerationHd transJobLevelWorkYearsRenumerationHd = transJobLevelWorkYearsRenumerationHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                transJobLevelWorkYearsRenumerationHd.GCTransactionStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                transJobLevelWorkYearsRenumerationHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                transJobLevelWorkYearsRenumerationHdDao.Update(transJobLevelWorkYearsRenumerationHd);

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
            TransJobLevelWorkYearsRenumerationHdDao transJobLevelWorkYearsRenumerationHdDao = new TransJobLevelWorkYearsRenumerationHdDao(ctx);
            
            try
            {
                TransJobLevelWorkYearsRenumerationHd transJobLevelWorkYearsRenumerationHd = transJobLevelWorkYearsRenumerationHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                if (String.Compare(transJobLevelWorkYearsRenumerationHd.StartEffectiveDate.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd")) <= 0)
                {
                    result = false;
                    errMessage = "Transaksi Sudah Diproses, Tidak Dapat Diubah";
                }
                else 
                {
                    transJobLevelWorkYearsRenumerationHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                    transJobLevelWorkYearsRenumerationHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    transJobLevelWorkYearsRenumerationHdDao.Update(transJobLevelWorkYearsRenumerationHd);
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
            TransJobLevelWorkYearsRenumerationHdDao transJobLevelWorkYearsRenumerationHdDao = new TransJobLevelWorkYearsRenumerationHdDao(ctx);
            
            try
            {
                TransJobLevelWorkYearsRenumerationHd transJobLevelWorkYearsRenumerationHd = transJobLevelWorkYearsRenumerationHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                transJobLevelWorkYearsRenumerationHd.GCTransactionStatus = Constant.TransactionStatus.VOID;
                transJobLevelWorkYearsRenumerationHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                transJobLevelWorkYearsRenumerationHdDao.Update(transJobLevelWorkYearsRenumerationHd);

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

        private void ControlToEntity(TransJobLevelWorkYearsRenumerationDt entityDt)
        {
            entityDt.JobLevelWorkYearsID = Convert.ToInt32(tacJobLevelWorkYearsID.Value);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int TransactionID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransJobLevelWorkYearsRenumerationDtDao entityDtDao = new TransJobLevelWorkYearsRenumerationDtDao(ctx);
            try
            {
                SaveTransJobLevelWorkYearsRenumerationHd(ctx, ref TransactionID);
                TransJobLevelWorkYearsRenumerationDt entityDt = new TransJobLevelWorkYearsRenumerationDt();
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
                BusinessLayer.DeleteTransJobLevelWorkYearsRenumerationDt(Convert.ToInt32(hdnTransactionID.Value), Convert.ToInt32(hdnEntryID.Value));
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