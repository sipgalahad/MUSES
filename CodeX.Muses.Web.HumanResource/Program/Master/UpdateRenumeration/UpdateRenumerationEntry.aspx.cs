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
    public partial class UpdateRenumerationEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.HumanResources.UPDATE_RENUMERATION;
        }

        #region Html Getter
        protected string OnGetFilterExpressionLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.ITEM_CONSUMPTION);
        }
        protected string OnGetFilterExpressionItemProduct()
        {
            return string.Format("IsDeleted = 0");
        }
        protected string OnGetFilterExpressionServiceUnit()
        {
            return string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }
        #endregion

        protected override void InitializeDataControl()
        {
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();

            SetControlProperties();
            hdnIsEditable.Value = "1";


            BindGridView(1, true, ref PageCount, ref RowCount);

            //Helper.SetControlEntrySetting(cboRenumerationCompID, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtAmount, new ControlEntrySetting(true, true, true), "mpTrx");
            
        }

        protected override void SetControlProperties()
        {
            List<RenumerationHd> listRenumerationHd = BusinessLayer.GetRenumerationHdList(string.Format("IsDeleted = 0"));
            Methods.SetComboBoxField<RenumerationHd>(cboRenumerationID, listRenumerationHd,"RenumerationName", "RenumerationID");


            //List<vRenumerationComp> listvRenumerationComp = BusinessLayer.GetvRenumerationCompList(string.Format("IsDeleted = 0"));
            //Methods.SetComboBoxField<vRenumerationComp>(cboRenumerationCompID, listvRenumerationComp, "RenumerationCompName", "RenumerationCompID");

        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnTransactionID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtTransactionNo, new ControlEntrySetting(false, false, false, ""));
            SetControlEntrySetting(txtTransactionDate, new ControlEntrySetting(true, true, false, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtStartEffectiveDate, new ControlEntrySetting(true, true, false, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(cboRenumerationID, new ControlEntrySetting(true, true, true));
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
            return BusinessLayer.GetvTransRenumerationHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vTransRenumerationHd entity = BusinessLayer.GetvTransRenumerationHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvTransRenumerationHdRowIndex(filterExpression, keyValue, "TransactionID DESC");
            vTransRenumerationHd entity = BusinessLayer.GetvTransRenumerationHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vTransRenumerationHd entity, ref bool isShowWatermark, ref string watermarkText)
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
            cboRenumerationID.Value = entity.RenumerationID.ToString();
            txtRemarks.Text = entity.Remarks;

            BindGridView(1, true, ref PageCount, ref RowCount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnTransactionID.Value != "")
                filterExpression = string.Format("TransactionID = {0} AND IsDeleted  = 0", hdnTransactionID.Value);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvTransRenumerationDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vTransRenumerationDt> lstEntity = BusinessLayer.GetvTransRenumerationDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "RenumerationCompName ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
        #endregion

        #region Save Header
        public void SaveTransRenumerationHd(IDbContext ctx, ref int TransactionID)
        {
            TransRenumerationHdDao entityHdDao = new TransRenumerationHdDao(ctx);
            if (hdnTransactionID.Value == "0")
            {
                TransRenumerationHd entityHd = new TransRenumerationHd();
                entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
                entityHd.StartEffectiveDate = Helper.GetDatePickerValue(Request.Form[txtStartEffectiveDate.UniqueID]);
                entityHd.RenumerationID = Convert.ToInt32(cboRenumerationID.Value);
                entityHd.Remarks = txtRemarks.Text;
                entityHd.TransactionNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.RENUMERATION, entityHd.TransactionDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;

                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);
                TransactionID = BusinessLayer.GetTransRenumerationHdMaxID(ctx);
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
                SaveTransRenumerationHd(ctx, ref OrderID);
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
                TransRenumerationHd entityHd = BusinessLayer.GetTransRenumerationHd(Convert.ToInt32(hdnTransactionID.Value));
                entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
                entityHd.StartEffectiveDate = Helper.GetDatePickerValue(Request.Form[txtStartEffectiveDate.UniqueID]);
                entityHd.RenumerationID = Convert.ToInt32(cboRenumerationID.Value);
                entityHd.Remarks = txtRemarks.Text;
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateTransRenumerationHd(entityHd);
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
            TransRenumerationHdDao transRenumerationHdDao = new TransRenumerationHdDao(ctx);
            RenumerationHdDao renumerationHdDao = new RenumerationHdDao(ctx);
            //TransRenumerationDtDao transRenumerationDtDao = new TransRenumerationDtDao(ctx);
            try
            {
                TransRenumerationHd transRenumerationHd = transRenumerationHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                transRenumerationHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                transRenumerationHd.Remarks = txtRemarks.Text;
                transRenumerationHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                transRenumerationHdDao.Update(transRenumerationHd);

                if (String.Compare(transRenumerationHd.StartEffectiveDate.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd")) <= 0)
                {
                    RenumerationHd renumerationHd = renumerationHdDao.Get(transRenumerationHd.RenumerationID);
                    renumerationHd.CurrentTransactionID = Convert.ToInt32(hdnTransactionID.Value);
                    renumerationHd.LastProcessedDate = DateTime.Now;
                    renumerationHdDao.Update(renumerationHd);
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
            TransRenumerationHdDao transRenumerationHdDao = new TransRenumerationHdDao(ctx);
            TransRenumerationDtDao transRenumerationDtDao = new TransRenumerationDtDao(ctx);
            try
            {
                TransRenumerationHd transRenumerationHd = transRenumerationHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                transRenumerationHd.GCTransactionStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                transRenumerationHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                transRenumerationHdDao.Update(transRenumerationHd);

                string filterExpressionPurchaseOrderHd = String.Format("TransactionID = {0} AND IsDeleted = 0", hdnTransactionID.Value);
                List<TransRenumerationDt> lstItemTransRenumerationDt = BusinessLayer.GetTransRenumerationDtList(filterExpressionPurchaseOrderHd, ctx);
                foreach (TransRenumerationDt transRenumerationDt in lstItemTransRenumerationDt)
                {
                    transRenumerationDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    transRenumerationDtDao.Update(transRenumerationDt);
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

        protected override bool OnReopenRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransRenumerationHdDao transRenumerationHdDao = new TransRenumerationHdDao(ctx);
            TransRenumerationDtDao transRenumerationDtDao = new TransRenumerationDtDao(ctx);
            try
            {
                TransRenumerationHd transRenumerationHd = transRenumerationHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                if (String.Compare(transRenumerationHd.StartEffectiveDate.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd")) <=0)
                {
                    result = false;
                    errMessage = "Transaksi Sudah Diproses, Tidak Dapat Diubah";
                }
                else 
                {
                    transRenumerationHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                    transRenumerationHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    transRenumerationHdDao.Update(transRenumerationHd);
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
            TransRenumerationHdDao transRenumerationHdDao = new TransRenumerationHdDao(ctx);
            TransRenumerationDtDao transRenumerationDtDao = new TransRenumerationDtDao(ctx);
            try
            {
                TransRenumerationHd transRenumerationHd = transRenumerationHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                transRenumerationHd.GCTransactionStatus = Constant.TransactionStatus.VOID;
                transRenumerationHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                transRenumerationHdDao.Update(transRenumerationHd);

                string filterExpressionPurchaseOrderHd = String.Format("TransactionID = {0} AND isDeleted = 0", hdnTransactionID.Value);
                List<TransRenumerationDt> lstTransRenumerationDt = BusinessLayer.GetTransRenumerationDtList(filterExpressionPurchaseOrderHd, ctx);
                foreach (TransRenumerationDt transRenumerationDt in lstTransRenumerationDt)
                {
                    transRenumerationDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    transRenumerationDtDao.Update(transRenumerationDt);
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
                if (hdnEntryID.Value.ToString() != "")
                {
                    adjustmentID = Convert.ToInt32(hdnTransactionID.Value);
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage, ref adjustmentID))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
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

        private void ControlToEntity(TransRenumerationDt entityDt)
        {
            //entityDt.RenumerationCompID = Convert.ToInt32(cboRenumerationCompID.Value);
            entityDt.RenumerationCompID = Convert.ToInt32(tacRenumerationCompID.Value);
            entityDt.Amount = Convert.ToDecimal(Request.Form[txtAmount.UniqueID]);
            entityDt.IsAllowChange = chkIsAllowChange.Checked;
            entityDt.IsUseFormula = chkIsUseFormula.Checked;
        }


        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int TransactionID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransRenumerationDtDao entityDtDao = new TransRenumerationDtDao(ctx);
            try
            {
                SaveTransRenumerationHd(ctx, ref TransactionID);
                TransRenumerationDt entityDt = new TransRenumerationDt();
                ControlToEntity(entityDt);
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

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransRenumerationDtDao entityDtDao = new TransRenumerationDtDao(ctx);
            try
            {
                TransRenumerationDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entityDt);
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

        private bool OnDeleteEntityDt(ref string errMessage, int ID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransRenumerationDtDao entityDtDao = new TransRenumerationDtDao(ctx);
            try
            {
                TransRenumerationDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
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
        //protected void cboRenumerationCompID_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        //{
        //    List<vRenumerationComp> lst = BusinessLayer.GetvRenumerationCompList(string.Format("isDeleted =  0"));
        //    Methods.SetComboBoxField<vRenumerationComp>(cboRenumerationCompID, lst, "RenumerationCompName", "RenumerationCompID");
        //}

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