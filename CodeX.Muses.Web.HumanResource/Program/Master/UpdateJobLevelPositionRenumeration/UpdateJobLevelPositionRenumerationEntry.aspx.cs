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
    public partial class UpdateJobLevelPositionRenumerationEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.HumanResources.UPDATE_RENUMERATION_JOB_LEVEL_POSITION;
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

            Helper.SetControlEntrySetting(tacJobLevelID, new ControlEntrySetting(true, true, true), "mpTrx");
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
            vTransJobLevelPositionRenumerationHd entity = BusinessLayer.GetvTransJobLevelPositionRenumerationHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvTransJobLevelPositionRenumerationHdRowIndex(filterExpression, keyValue, "TransactionID DESC");
            vTransJobLevelPositionRenumerationHd entity = BusinessLayer.GetvTransJobLevelPositionRenumerationHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vTransJobLevelPositionRenumerationHd entity, ref bool isShowWatermark, ref string watermarkText)
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
                rowCount = BusinessLayer.GetvTransJobLevelPositionRenumerationDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vTransJobLevelPositionRenumerationDt> lstEntity = BusinessLayer.GetvTransJobLevelPositionRenumerationDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "OrganizationPositionID ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
        #endregion

        #region Save Header
        public void SaveTransJobLevelPositionRenumerationHd(IDbContext ctx, ref int TransactionID)
        {
            TransJobLevelPositionRenumerationHdDao entityHdDao = new TransJobLevelPositionRenumerationHdDao(ctx);
            if (hdnTransactionID.Value == "0")
            {
                TransJobLevelPositionRenumerationHd entityHd = new TransJobLevelPositionRenumerationHd();
                entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
                entityHd.StartEffectiveDate = Helper.GetDatePickerValue(Request.Form[txtStartEffectiveDate.UniqueID]);
                entityHd.RenumerationID = Convert.ToInt32(tacRenumeration.Value);
                entityHd.Remarks = txtRemarks.Text;
                entityHd.TransactionNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.RENUMERATION_JOB_LEVEL, entityHd.TransactionDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;

                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);
                TransactionID = BusinessLayer.GetTransJobLevelPositionRenumerationHdMaxID(ctx);
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
                SaveTransJobLevelPositionRenumerationHd(ctx, ref OrderID);
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
                TransJobLevelPositionRenumerationHd entityHd = BusinessLayer.GetTransJobLevelPositionRenumerationHd(Convert.ToInt32(hdnTransactionID.Value));
                entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
                entityHd.StartEffectiveDate = Helper.GetDatePickerValue(Request.Form[txtStartEffectiveDate.UniqueID]);
                entityHd.RenumerationID = Convert.ToInt32(tacRenumeration.Value);
                entityHd.Remarks = txtRemarks.Text;
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateTransJobLevelPositionRenumerationHd(entityHd);
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
            TransJobLevelPositionRenumerationHdDao TransJobLevelPositionRenumerationHdDao = new TransJobLevelPositionRenumerationHdDao(ctx);
            JobLevelPositionDao jobLevelDao = new JobLevelPositionDao(ctx);
            try
            {
                TransJobLevelPositionRenumerationHd TransJobLevelPositionRenumerationHd = TransJobLevelPositionRenumerationHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                TransJobLevelPositionRenumerationHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                TransJobLevelPositionRenumerationHd.Remarks = txtRemarks.Text;
                TransJobLevelPositionRenumerationHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                TransJobLevelPositionRenumerationHdDao.Update(TransJobLevelPositionRenumerationHd);

                if (String.Compare(TransJobLevelPositionRenumerationHd.StartEffectiveDate.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd")) <= 0)
                {
                    List<JobLevelPosition> lstJobLevel = BusinessLayer.GetJobLevelPositionList(String.Format("JobLevelID IN (SELECT JobLevelPositionID FROM TransJobLevelPositionRenumerationDt WHERE TransactionID = {0})", hdnTransactionID.Value), ctx);
                    foreach (JobLevelPosition jobLevel in lstJobLevel)
                    {
                        jobLevel.CurrentTransactionID = Convert.ToInt32(hdnTransactionID.Value);
                        jobLevel.LastProcessedDate = DateTime.Now;
                        jobLevelDao.Update(jobLevel);
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
            TransJobLevelPositionRenumerationHdDao TransJobLevelPositionRenumerationHdDao = new TransJobLevelPositionRenumerationHdDao(ctx);
            
            try
            {
                TransJobLevelPositionRenumerationHd TransJobLevelPositionRenumerationHd = TransJobLevelPositionRenumerationHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                TransJobLevelPositionRenumerationHd.GCTransactionStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                TransJobLevelPositionRenumerationHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                TransJobLevelPositionRenumerationHdDao.Update(TransJobLevelPositionRenumerationHd);

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
            TransJobLevelPositionRenumerationHdDao TransJobLevelPositionRenumerationHdDao = new TransJobLevelPositionRenumerationHdDao(ctx);
            
            try
            {
                TransJobLevelPositionRenumerationHd TransJobLevelPositionRenumerationHd = TransJobLevelPositionRenumerationHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                if (String.Compare(TransJobLevelPositionRenumerationHd.StartEffectiveDate.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd")) <= 0)
                {
                    result = false;
                    errMessage = "Transaksi Sudah Diproses, Tidak Dapat Diubah";
                }
                else 
                {
                    TransJobLevelPositionRenumerationHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                    TransJobLevelPositionRenumerationHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    TransJobLevelPositionRenumerationHdDao.Update(TransJobLevelPositionRenumerationHd);
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
            TransJobLevelPositionRenumerationHdDao TransJobLevelPositionRenumerationHdDao = new TransJobLevelPositionRenumerationHdDao(ctx);
            
            try
            {
                TransJobLevelPositionRenumerationHd TransJobLevelPositionRenumerationHd = TransJobLevelPositionRenumerationHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                TransJobLevelPositionRenumerationHd.GCTransactionStatus = Constant.TransactionStatus.VOID;
                TransJobLevelPositionRenumerationHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                TransJobLevelPositionRenumerationHdDao.Update(TransJobLevelPositionRenumerationHd);

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

        private void ControlToEntity(TransJobLevelPositionRenumerationDt entityDt)
        {
            entityDt.JobLevelPositionID = Convert.ToInt32(tacJobLevelID.Value);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int TransactionID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransJobLevelPositionRenumerationDtDao entityDtDao = new TransJobLevelPositionRenumerationDtDao(ctx);
            try
            {
                SaveTransJobLevelPositionRenumerationHd(ctx, ref TransactionID);
                TransJobLevelPositionRenumerationDt entityDt = new TransJobLevelPositionRenumerationDt();
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
                BusinessLayer.DeleteTransJobLevelPositionRenumerationDt(Convert.ToInt32(hdnTransactionID.Value), Convert.ToInt32(hdnEntryID.Value));
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