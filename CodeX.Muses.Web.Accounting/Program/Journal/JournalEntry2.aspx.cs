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
using DevExpress.Web.ASPxCallbackPanel;
using System.Data;
using CodeX.Common;

namespace CodeX.Muses.Web.Accounting.Program
{
    public partial class JournalEntry2 : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Accounting.JOURNAL_ENTRY;
        }

        #region Html Getter
        protected string GetJournalGroupPendapatanPenerimaan()
        {
            return string.Empty;
            //return Constant.JournalGroup.PENDAPATAN_PENERIMAAN;
        }
        protected string GetJournalGroupHutangPiutang()
        {
            return string.Empty;
            //return Constant.JournalGroup.HUTANG_PIUTANG;
        }
        protected string GetJournalGroupInventory()
        {
            return string.Empty;
            //return Constant.JournalGroup.INVENTORY;
        }
        protected string GetJournalGroupMemorial()
        {
            return Constant.JournalGroup.MEMORIAL;
        }
        #endregion

        public Int32 GetDisplayCount() 
        {
            return Convert.ToInt32(hdnDisplayCount.Value) - 1;
        }

        protected int minDate = -1;
        protected override void InitializeDataControl()
        {
            vGLTransactionHd entity = BusinessLayer.GetvGLTransactionHd(string.Format("TransactionCode = '{0}'", Constant.TransactionCode.JOURNAL_MEMORIAL_IKHTISAR), 0, "JournalDate DESC");
            if (entity != null)
            {
                hdnLastPostingDate.Value = entity.JournalDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                minDate = (DateTime.Now - entity.JournalDate).Days - 1;
            }

            #region Perkiraan Aktiva Tetap
            Helper.SetControlEntrySetting(hdnGLAccount1ID, new ControlEntrySetting(true, true), "mpTrx");
            Helper.SetControlEntrySetting(hdnSearchDialogTypeName1, new ControlEntrySetting(true, true), "mpTrx");
            Helper.SetControlEntrySetting(hdnSubLedgerID1, new ControlEntrySetting(true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtGLAccount1Code, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtGLAccount1Name, new ControlEntrySetting(false, false, false), "mpTrx");
            Helper.SetControlEntrySetting(lblSubLedgerDt1, new ControlEntrySetting(false, false), "mpTrx");
            Helper.SetControlEntrySetting(hdnSubLedgerDt1ID, new ControlEntrySetting(true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtSubLedgerDt1Code, new ControlEntrySetting(false, false, true), "mpTrx");
            Helper.SetControlEntrySetting(txtSubLedgerDt1Name, new ControlEntrySetting(false, false, false), "mpTrx");
            #endregion

            decimal totalDebit = -1;
            decimal totalKredit = -1;
            decimal selisih = -1;
            BindGridView(ref totalDebit, ref totalKredit, ref selisih);
            hdnIsEditable.Value = "1";
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtJournalPrefix, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtJournalNo, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtJournalDate, new ControlEntrySetting(true, false, true, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));

            SetControlEntrySetting(cboTransactionCode, new ControlEntrySetting(true, false, true));
        }

        protected override void SetControlProperties()
        {
            List<TransactionType> lstTransactionType = BusinessLayer.GetTransactionTypeList("TransactionCode LIKE '72%'");
            Methods.SetComboBoxField<TransactionType>(cboTransactionCode, lstTransactionType.Where(p => Convert.ToInt32(p.TransactionCode) > 7280 && Convert.ToInt32(p.TransactionCode) < 7290).ToList(), "TransactionName", "TransactionCode");
        }

        #region Load Entity
        public override void OnAddRecord()
        {
            hdnID.Value = "0";
            hdnIsEditable.Value = "1";
            hdnDisplayCount.Value = "1";
            tdTransactionNoEdit.Style.Add("display", "none");
            tdTransactionNoAdd.Style.Remove("display");
        }

        public string GetGCTransactionStatusOpen()
        {
            return Constant.TransactionStatus.OPEN;
        }

        protected string IsEditable()
        {
            return hdnIsEditable.Value;
        }

        protected string GetFilterExpression()
        {
            //string filterExpression = String.Format("TransactionCode = '{0}'", Constant.TransactionCode.JOURNAL_MEMORIAL);
            //if (hdnRecordFilterExpression.Value != "")
            //    filterExpression += string.Format(" AND {0}", hdnRecordFilterExpression.Value);
            //return filterExpression;
            return "";
        }

        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvGLTransactionHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vGLTransactionHd entity = BusinessLayer.GetvGLTransactionHd(filterExpression, PageIndex, "GLTransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
            hdnGCTransactionStatus.Value = entity.GCTransactionStatus;
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvGLTransactionHdRowIndex(filterExpression, keyValue, "GLTransactionID DESC");
            vGLTransactionHd entity = BusinessLayer.GetvGLTransactionHd(filterExpression, PageIndex, "GLTransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
            hdnGCTransactionStatus.Value = entity.GCTransactionStatus;
        }

        private void EntityToControl(vGLTransactionHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                hdnIsEditable.Value = "0";
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
            }
            else
                hdnIsEditable.Value = "1";
            tdTransactionNoAdd.Style.Add("display", "none");
            tdTransactionNoEdit.Style.Remove("display");
            hdnID.Value = entity.GLTransactionID.ToString();
            txtJournalNo.Text = entity.JournalNo;
            cboTransactionCode.Value = entity.TransactionCode;

            txtJournalDate.Text = entity.JournalDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtRemarks.Text = entity.Remarks;

            decimal totalDebet = -1;
            decimal totalKredit = -1;
            decimal selisih = -1;
            BindGridView(ref totalDebet, ref totalKredit, ref selisih);
            divCreatedBy.InnerHtml = string.Format(@"{0} / {1}", entity.CreatedByName, entity.CreatedDate.ToString(Constant.FormatString.DATE_FORMAT));
            string lastUpdatedDate = string.Empty;
            if (entity.LastUpdatedDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT) == Constant.ConstantDate.DEFAULT_NULL)
                lastUpdatedDate = "";
            else
                lastUpdatedDate = " / " + entity.LastUpdatedDate.ToString(Constant.FormatString.DATE_FORMAT);
            divLastUpdatedBy.InnerHtml = string.Format(@"{0} {1}", entity.LastUpdatedByName, lastUpdatedDate);
        }

        private void BindGridView(ref decimal totalDebet, ref decimal totalKredit, ref decimal selisih)
        {
            string filterExpression = "1 = 0";
            if (hdnID.Value != "")
                filterExpression = string.Format("GLTransactionID = {0} AND GCItemDetailStatus != '{1}' ORDER BY DisplayOrder ASC", hdnID.Value, Constant.TransactionStatus.VOID);
           
            List<vGLTransactionDt> lstEntity = BusinessLayer.GetvGLTransactionDtList(filterExpression);
            totalDebet = lstEntity.Where(x=> x.Position == "D").Sum(x => x.DebitAmount);
            totalKredit = lstEntity.Where(x => x.Position == "K").Sum(x => x.CreditAmount);
            selisih = totalDebet - totalKredit;
            txtTotalDebet.Text = totalDebet.ToString("N");
            txtTotalKredit.Text = totalKredit.ToString("N");
            txtTotalSelisih.Text = selisih.ToString("N");
            hdnDisplayCount.Value = Convert.ToString(lstEntity.Count() + 1);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
        #endregion

        #region Save Header
        public void SaveGLTransactionHd(IDbContext ctx, ref int GLTransactionID)
        {
            GLTransactionHdDao entityHdDao = new GLTransactionHdDao(ctx);
            if (hdnID.Value == "" || hdnID.Value == "0")
            {
                DateTime journalDate = Helper.GetDatePickerValue(Request.Form[txtJournalDate.UniqueID]);
                bool isAllowSave = true;
                if (hdnLastPostingDate.Value != "")
                {
                    DateTime lastPostingDate = Helper.GetDatePickerValue(hdnLastPostingDate.Value);
                    if (journalDate <= lastPostingDate)
                        isAllowSave = false;
                }
                
                if (isAllowSave)
                {
                    GLTransactionHd entityHd = new GLTransactionHd();
                    entityHd.JournalDate = Helper.GetDatePickerValue(Request.Form[txtJournalDate.UniqueID]);
                    entityHd.GCJournalGroup = Constant.JournalGroup.MEMORIAL;
                    entityHd.TransactionCode = cboTransactionCode.Value.ToString();

                    entityHd.Remarks = txtRemarks.Text;
                    entityHd.JournalNo = BusinessLayer.GenerateTransactionNo(entityHd.TransactionCode, entityHd.JournalDate, txtJournalPrefix.Text, ctx);
                    entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;

                    ctx.CommandType = CommandType.Text;
                    ctx.Command.Parameters.Clear();
                    entityHd.CreatedBy = AppSession.UserLogin.UserID;
                    entityHdDao.Insert(entityHd);
                    GLTransactionID = BusinessLayer.GetGLTransactionMaxID(ctx);
                    hdnID.Value = GLTransactionID.ToString();
                }
                else 
                {
                    GLTransactionID = 0;
                }
            }
            else
            {
                GLTransactionID = Convert.ToInt32(hdnID.Value);
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);

            try
            {
                int OrderID = 0;
                SaveGLTransactionHd(ctx, ref OrderID);
                if (OrderID != 0)
                {
                    retval = OrderID.ToString();
                }
                else 
                {
                    errMessage = "Journal Pada Periode ini Telah Diposting";
                    result = false;
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

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            try
            {
                GLTransactionHd entityHd = BusinessLayer.GetGLTransactionHd(Convert.ToInt32(hdnID.Value));
                entityHd.JournalDate = Helper.GetDatePickerValue(Request.Form[txtJournalDate.UniqueID]);
                entityHd.GCJournalGroup = Constant.JournalGroup.MEMORIAL;
                entityHd.TransactionCode = cboTransactionCode.Value.ToString();
                entityHd.Remarks = txtRemarks.Text;
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateGLTransactionHd(entityHd);
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
            if (Convert.ToDecimal(Request.Form[txtTotalSelisih.UniqueID]) == 0)
            {
                IDbContext ctx = DbFactory.Configure(true);
                GLTransactionHdDao GLTransactionHdDao = new GLTransactionHdDao(ctx);
                GLTransactionDtDao GlTransactionDtDao = new GLTransactionDtDao(ctx);
                try
                {
                    GLTransactionHd itemTransactionHd = GLTransactionHdDao.Get(Convert.ToInt32(hdnID.Value));
                    itemTransactionHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                    itemTransactionHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    GLTransactionHdDao.Update(itemTransactionHd);

                    string filterExpression = String.Format("GLTransactionID = {0} AND GCItemDetailStatus != '{1}' ORDER BY DisplayOrder", hdnID.Value, Constant.TransactionStatus.VOID);
                    List<GLTransactionDt> lstGLTransactionDt = BusinessLayer.GetGLTransactionDtList(filterExpression, ctx);
                    foreach (GLTransactionDt GlTransactionDt in lstGLTransactionDt)
                    {
                        GlTransactionDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
                        GlTransactionDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        GlTransactionDtDao.Update(GlTransactionDt);
                    }
                    ctx.CommitTransaction();
                }
                catch (Exception ex)
                {
                    errMessage = ex.Message;
                    result = false;
                    ctx.RollBackTransaction();
                }
                finally
                {
                    ctx.Close();
                }
            }
            else
            {
                result = false;
                errMessage = "Journal Tidak Seimbang";
            }

            return result;
        }

        protected override bool OnProposeRecord(ref string errMessage)
        {
            if (Convert.ToDecimal(txtTotalSelisih.Text) == 0)
            {
                try
                {
                    GLTransactionHd entity = BusinessLayer.GetGLTransactionHd(Convert.ToInt32(hdnID.Value));
                    entity.GCTransactionStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.UpdateGLTransactionHd(entity);
                    return true;
                }
                catch (Exception ex)
                {
                    errMessage = ex.Message;
                    return false;
                }
            }
            else 
            {
                errMessage = "Journal Tidak Seimbang";
                return false;
            }
        }

        protected override bool OnVoidRecord(ref string errMessage)
        {
            try
            {
                GLTransactionHd entity = BusinessLayer.GetGLTransactionHd(Convert.ToInt32(hdnID.Value));
                entity.GCTransactionStatus = Constant.TransactionStatus.VOID;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateGLTransactionHd(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        #endregion

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            int GLTransactionID = 0;
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    GLTransactionID = Convert.ToInt32(hdnID.Value);
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage, ref GLTransactionID))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                GLTransactionID = Convert.ToInt32(hdnID.Value);
                if (OnDeleteEntityDt(ref errMessage, GLTransactionID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpGLTransactionID"] = GLTransactionID.ToString();
        }

        private void ControlToEntity(GLTransactionDt entityDt)
        {
            entityDt.GLAccount = Convert.ToInt32(hdnGLAccount1ID.Value);
            if (hdnSubLedgerDt1ID.Value != "" && hdnSubLedgerDt1ID.Value != "0")
                entityDt.SubLedger = Convert.ToInt32(hdnSubLedgerDt1ID.Value);
            else
                entityDt.SubLedger = null;
            Decimal debit = Convert.ToDecimal(txtAmountD.Text);
            Decimal kredit = Convert.ToDecimal(txtAmountK.Text);
            if (debit != 0)
            {
                entityDt.Position = "D";
                entityDt.DebitAmount = debit;
                entityDt.CreditAmount = 0;
            }
            else
            {
                entityDt.Position = "K";
                entityDt.CreditAmount = kredit;
                entityDt.DebitAmount = 0;
            }
            entityDt.ReferenceNo = txtReferenceNo.Text;
            if (txtDisplayOrder.Text != "")
                entityDt.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
            else
                entityDt.DisplayOrder = 0;
            
            entityDt.Remarks = txtRemarksDt.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int GLTransactionID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            GLTransactionDtDao entityDtDao = new GLTransactionDtDao(ctx);
            try
            {
                SaveGLTransactionHd(ctx, ref GLTransactionID);
                if (GLTransactionID != 0) 
                {
                    GLTransactionDt entityDt = new GLTransactionDt();
                    ControlToEntity(entityDt);
                    entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                    entityDt.GLTransactionID = GLTransactionID;
                    entityDt.CreatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Insert(entityDt);
                }
                else
                {
                    errMessage = "Journal Pada Periode ini Telah Diposting";
                    result = false;
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
            GLTransactionDtDao entityDtDao = new GLTransactionDtDao(ctx);
            try
            {
                GLTransactionDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
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
            GLTransactionDtDao entityDtDao = new GLTransactionDtDao(ctx);
            try
            {
                GLTransactionDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                entityDt.GCItemDetailStatus = Constant.TransactionStatus.VOID;
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
            decimal totalDebit = 0;
            decimal totalKredit = 0;
            decimal selisih = 0;
            string result = "";
            BindGridView(ref totalDebit, ref totalKredit, ref selisih);
            result = string.Format("refresh|{0}|{1}|{2}", totalDebit, totalKredit, selisih);

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
        #endregion
    }
}