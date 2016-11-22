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
    public partial class UpdateRenumerationCompFormulaEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.HumanResources.UPDATE_RENUMERATION_COMP_FORMULA;
        }

        #region Html Getter
        protected string OnGetRenumerationCompFormulaBaseTariffTypeFromComponent()
        {
            return Constant.RenumerationFormulaBaseTariffType.RENUMERATION_COMP;
        }
        #endregion

        protected override void InitializeDataControl()
        {
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();

            SetControlProperties();
            hdnIsEditable.Value = "1";


            BindGridView(1, true, ref PageCount, ref RowCount);

            Helper.SetControlEntrySetting(cboGCBaseTariffType, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(tacRenumerationCompID, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtBaseNilai, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtTariffMultipleBy, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtMaxJam, new ControlEntrySetting(true, true, true), "mpTrx");
            
        }

        protected override void SetControlProperties()
        {
            List<RenumerationCompFormulaHd> listRenumerationCompFormulaHd = BusinessLayer.GetRenumerationCompFormulaHdList(string.Format("IsDeleted = 0"));
            Methods.SetComboBoxField<RenumerationCompFormulaHd>(cboFormulaID, listRenumerationCompFormulaHd, "FormulaName", "FormulaID");

            List<StandardCode> listSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.RENUMERATION_FORMULA_BASE_TARIFF_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboGCBaseTariffType, listSc, "StandardCodeName", "StandardCodeID");

        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnTransactionID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtTransactionNo, new ControlEntrySetting(false, false, false, ""));
            SetControlEntrySetting(txtTransactionDate, new ControlEntrySetting(true, true, false, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtStartEffectiveDate, new ControlEntrySetting(true, true, false, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(cboFormulaID, new ControlEntrySetting(true, true, true));
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
            return BusinessLayer.GetvTransRenumerationCompFormulaHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vTransRenumerationCompFormulaHd entity = BusinessLayer.GetvTransRenumerationCompFormulaHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvTransRenumerationCompFormulaHdRowIndex(filterExpression, keyValue, "TransactionID DESC");
            vTransRenumerationCompFormulaHd entity = BusinessLayer.GetvTransRenumerationCompFormulaHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vTransRenumerationCompFormulaHd entity, ref bool isShowWatermark, ref string watermarkText)
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
            cboFormulaID.Value = entity.FormulaID.ToString();
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
                rowCount = BusinessLayer.GetvTransRenumerationCompFormulaDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vTransRenumerationCompFormulaDt> lstEntity = BusinessLayer.GetvTransRenumerationCompFormulaDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "FromRenumerationCompName ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
        #endregion

        #region Save Header
        public void SaveTransRenumerationCompFormulaHd(IDbContext ctx, ref int TransactionID)
        {
            TransRenumerationCompFormulaHdDao entityHdDao = new TransRenumerationCompFormulaHdDao(ctx);
            if (hdnTransactionID.Value == "0")
            {
                TransRenumerationCompFormulaHd entityHd = new TransRenumerationCompFormulaHd();
                entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
                entityHd.StartEffectiveDate = Helper.GetDatePickerValue(Request.Form[txtStartEffectiveDate.UniqueID]);
                entityHd.FormulaID = Convert.ToInt32(cboFormulaID.Value);
                entityHd.Remarks = txtRemarks.Text;
                entityHd.TransactionNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.RENUMERATION_COMP_FORMULA, entityHd.TransactionDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;

                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);
                TransactionID = BusinessLayer.GetTransRenumerationCompFormulaHdMaxID(ctx);
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
                SaveTransRenumerationCompFormulaHd(ctx, ref OrderID);
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
                TransRenumerationCompFormulaHd entityHd = BusinessLayer.GetTransRenumerationCompFormulaHd(Convert.ToInt32(hdnTransactionID.Value));
                entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
                entityHd.StartEffectiveDate = Helper.GetDatePickerValue(Request.Form[txtStartEffectiveDate.UniqueID]);
                entityHd.FormulaID = Convert.ToInt32(cboFormulaID.Value);
                entityHd.Remarks = txtRemarks.Text;
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateTransRenumerationCompFormulaHd(entityHd);
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
            TransRenumerationCompFormulaHdDao transRenumerationCompFormulaHdDao = new TransRenumerationCompFormulaHdDao(ctx);
            RenumerationCompFormulaHdDao renumerationCompFormulaHdDao = new RenumerationCompFormulaHdDao(ctx);
            //TransRenumerationCompFormulaDtDao transRenumerationCompFormulaDtDao = new TransRenumerationCompFormulaDtDao(ctx);
            try
            {
                TransRenumerationCompFormulaHd transRenumerationCompFormulaHd = transRenumerationCompFormulaHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                transRenumerationCompFormulaHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                transRenumerationCompFormulaHd.Remarks = txtRemarks.Text;
                transRenumerationCompFormulaHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                transRenumerationCompFormulaHdDao.Update(transRenumerationCompFormulaHd);

                //string filterExpressionPurchaseOrderHd = String.Format("TransactionID = {0} AND isDeleted = 0", hdnTransactionID.Value);
                //List<TransRenumerationCompFormulaDt> lstTransRenumerationCompFormulaDt = BusinessLayer.GetTransRenumerationCompFormulaDtList(filterExpressionPurchaseOrderHd, ctx);
                //foreach (TransRenumerationCompFormulaDt transRenumerationCompFormulaDt in lstTransRenumerationCompFormulaDt)
                //{
                //    transRenumerationCompFormulaDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                //    transRenumerationCompFormulaDtDao.Update(transRenumerationCompFormulaDt);
                //}

                if (String.Compare(transRenumerationCompFormulaHd.StartEffectiveDate.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd")) <= 0)
                {
                    RenumerationCompFormulaHd renumerationCompFormulaHd = renumerationCompFormulaHdDao.Get(transRenumerationCompFormulaHd.FormulaID);
                    renumerationCompFormulaHd.CurrentTransactionID = Convert.ToInt32(hdnTransactionID.Value);
                    renumerationCompFormulaHd.LastProcessedDate = DateTime.Now;
                    renumerationCompFormulaHdDao.Update(renumerationCompFormulaHd);
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
            TransRenumerationCompFormulaHdDao transRenumerationCompFormulaHdDao = new TransRenumerationCompFormulaHdDao(ctx);
            TransRenumerationCompFormulaDtDao transRenumerationCompFormulaDtDao = new TransRenumerationCompFormulaDtDao(ctx);
            try
            {
                TransRenumerationCompFormulaHd transRenumerationCompFormulaHd = transRenumerationCompFormulaHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                transRenumerationCompFormulaHd.GCTransactionStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                transRenumerationCompFormulaHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                transRenumerationCompFormulaHdDao.Update(transRenumerationCompFormulaHd);

                string filterExpressionPurchaseOrderHd = String.Format("TransactionID = {0} AND IsDeleted = 0", hdnTransactionID.Value);
                List<TransRenumerationCompFormulaDt> lstItemTransRenumerationCompFormulaDt = BusinessLayer.GetTransRenumerationCompFormulaDtList(filterExpressionPurchaseOrderHd, ctx);
                foreach (TransRenumerationCompFormulaDt transTransRenumerationCompFormulaDt in lstItemTransRenumerationCompFormulaDt)
                {
                    transTransRenumerationCompFormulaDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    transRenumerationCompFormulaDtDao.Update(transTransRenumerationCompFormulaDt);
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
            TransRenumerationCompFormulaHdDao transRenumerationCompFormulaHdDao = new TransRenumerationCompFormulaHdDao(ctx);
            //TransRenumerationCompFormulaDtDao transRenumerationCompFormulaDtDao = new TransRenumerationCompFormulaDtDao(ctx);
            
            try
            {
                TransRenumerationCompFormulaHd transRenumerationCompFormulaHd = transRenumerationCompFormulaHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                if (String.Compare(transRenumerationCompFormulaHd.StartEffectiveDate.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd")) <= 0)
                {
                    result = false;
                    errMessage = "Transaksi Sudah Diproses, Tidak Dapat Diubah";
                }
                else
                {
                    transRenumerationCompFormulaHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                    transRenumerationCompFormulaHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    transRenumerationCompFormulaHdDao.Update(transRenumerationCompFormulaHd);
                }
                //string filterExpressionPurchaseOrderHd = String.Format("TransactionID = {0} AND ISsDeleted = 0", hdnTransactionID.Value);
                //List<TransRenumerationCompFormulaDt> lstTransRenumerationCompFormulaDt = BusinessLayer.GetTransRenumerationCompFormulaDtList(filterExpressionPurchaseOrderHd, ctx);
                //foreach (TransRenumerationCompFormulaDt transRenumerationCompFormulaDt in lstTransRenumerationCompFormulaDt)
                //{
                //    transRenumerationCompFormulaDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                //    transRenumerationCompFormulaDtDao.Update(transRenumerationCompFormulaDt);
                //}
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
            TransRenumerationCompFormulaHdDao transRenumerationCompFormulaHdDao = new TransRenumerationCompFormulaHdDao(ctx);
            TransRenumerationCompFormulaDtDao transRenumerationCompFormulaDtDao = new TransRenumerationCompFormulaDtDao(ctx);
            try
            {
                TransRenumerationCompFormulaHd transRenumerationCompFormulaHd = transRenumerationCompFormulaHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                transRenumerationCompFormulaHd.GCTransactionStatus = Constant.TransactionStatus.VOID;
                transRenumerationCompFormulaHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                transRenumerationCompFormulaHdDao.Update(transRenumerationCompFormulaHd);

                string filterExpressionPurchaseOrderHd = String.Format("TransactionID = {0} AND isDeleted = 0", hdnTransactionID.Value);
                List<TransRenumerationCompFormulaDt> lstTransRenumerationCompFormulaDt = BusinessLayer.GetTransRenumerationCompFormulaDtList(filterExpressionPurchaseOrderHd, ctx);
                foreach (TransRenumerationCompFormulaDt transRenumerationCompFormulaDt in lstTransRenumerationCompFormulaDt)
                {
                    transRenumerationCompFormulaDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    transRenumerationCompFormulaDtDao.Update(transRenumerationCompFormulaDt);
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

        private void ControlToEntity(TransRenumerationCompFormulaDt entityDt)
        {
            entityDt.GCBaseTariffType = cboGCBaseTariffType.Value.ToString();
            entityDt.IsTariffFlat = chkIsTariffFlat.Checked;

            if (entityDt.GCBaseTariffType == Constant.RenumerationFormulaBaseTariffType.RENUMERATION_COMP)
            {
                entityDt.FromRenumerationCompID = Convert.ToInt32(tacRenumerationCompID.Value);
                entityDt.BaseTariff = 0;
            }
            else
            {
                entityDt.BaseTariff = Convert.ToDecimal(txtBaseNilai.Text);
                entityDt.FromRenumerationCompID = null;
            }

            entityDt.MaxNHour = Convert.ToInt16(txtMaxJam.Text);

            if (txtTariffMultipleBy.Text == "0") 
                entityDt.BaseTariffMultiplyBy = 0;
            else
                entityDt.BaseTariffMultiplyBy = Convert.ToDecimal(txtTariffMultipleBy.Text);

        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int TransactionID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransRenumerationCompFormulaDtDao entityDtDao = new TransRenumerationCompFormulaDtDao(ctx);
            TransRenumerationCompFormulaDtHourDao entityDtHourDao = new TransRenumerationCompFormulaDtHourDao(ctx);
            try
            {
                SaveTransRenumerationCompFormulaHd(ctx, ref TransactionID);
                TransRenumerationCompFormulaDt entityDt = new TransRenumerationCompFormulaDt();
                ControlToEntity(entityDt);
                entityDt.TransactionID = TransactionID;
                entityDt.CreatedBy = AppSession.UserLogin.UserID;
                entityDt.TransactionDtID = entityDtDao.Insert(entityDt);

                if (hdnDtHourSave.Value != "")
                {
                    string[] lstSaveEntityDt = hdnDtHourSave.Value.Split('|');
                    foreach (string saveEntityDt in lstSaveEntityDt)
                    {
                        TransRenumerationCompFormulaDtHour entityDtHour = new TransRenumerationCompFormulaDtHour();
                        string[] temp = saveEntityDt.Split(';');
                        entityDtHour.FromHoursIndex = Convert.ToInt16(temp[0]);
                        entityDtHour.ToHoursIndex = Convert.ToInt16(temp[1]);
                        entityDtHour.MultiplyBy = Convert.ToDecimal(temp[2]);
                        entityDtHour.TransactionDtID = Convert.ToInt32(entityDt.TransactionDtID);
                        entityDtHourDao.Insert(entityDtHour);
                    }
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
            TransRenumerationCompFormulaDtDao entityDtDao = new TransRenumerationCompFormulaDtDao(ctx);
            TransRenumerationCompFormulaDtHourDao entityDtHourDao = new TransRenumerationCompFormulaDtHourDao(ctx);
            try
            {
                TransRenumerationCompFormulaDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entityDt);
                entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDtDao.Update(entityDt);

                Int32 TransactionDtID = Convert.ToInt32(hdnTransactionDtID.Value);
                string[] lstSaveEntityDt = hdnDtHourSave.Value.Split('|');
                List<TransRenumerationCompFormulaDtHour> lstEntityDt = BusinessLayer.GetTransRenumerationCompFormulaDtHourList(string.Format("TransactionDtID = {0}", TransactionDtID), ctx);

                foreach (string saveEntityDt in lstSaveEntityDt)
                {
                    string[] temp = saveEntityDt.Split(';');

                    short fromHoursIndex = Convert.ToInt16(temp[0]);
                    short toHoursIndex = Convert.ToInt16(temp[1]);
                    decimal multiplyBy = Convert.ToDecimal(temp[2]);
                    TransRenumerationCompFormulaDtHour entityDtHour = lstEntityDt.FirstOrDefault(p => p.FromHoursIndex == fromHoursIndex && p.ToHoursIndex == toHoursIndex);
                    if (entityDtHour == null)
                    {
                        entityDtHour = new TransRenumerationCompFormulaDtHour();
                        entityDtHour.TransactionDtID = entityDt.TransactionDtID;
                        entityDtHour.FromHoursIndex = fromHoursIndex;
                        entityDtHour.ToHoursIndex = fromHoursIndex;
                        entityDtHour.MultiplyBy = multiplyBy;
                        entityDtHourDao.Insert(entityDtHour);
                    }
                    else
                    {
                        if (entityDtHour.MultiplyBy != multiplyBy)
                        {
                            entityDtHour.MultiplyBy = multiplyBy;
                            entityDtHourDao.Update(entityDtHour);
                        }
                        lstEntityDt.Remove(entityDtHour);
                    }
                }

                foreach (TransRenumerationCompFormulaDtHour entity in lstEntityDt)
                {
                    entityDtHourDao.Delete(entity.TransactionDtID, entity.FromHoursIndex, entity.ToHoursIndex);
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
            TransRenumerationCompFormulaDtDao entityDtDao = new TransRenumerationCompFormulaDtDao(ctx);
            try
            {
                TransRenumerationCompFormulaDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
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