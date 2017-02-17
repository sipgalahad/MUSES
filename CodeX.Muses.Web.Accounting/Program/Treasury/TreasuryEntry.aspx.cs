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
using System.Web.Script.Serialization;

namespace CodeX.Muses.Web.Accounting.Program
{
    public partial class TreasuryEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Accounting.TREASURY_ENTRY;
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

        protected int minDate = -1;
        protected override void InitializeDataControl()
        {
            vTreasuryHd entity = BusinessLayer.GetvTreasuryHd(string.Format("TransactionCode = '{0}'", Constant.TransactionCode.JOURNAL_MEMORIAL_IKHTISAR), 0, "TransactionDate DESC");
            if (entity != null)
            {
                hdnLastPostingDate.Value = entity.TransactionDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                minDate = (DateTime.Now - entity.TransactionDate).Days - 1;
            }
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtJournalPrefix, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtTransactionNo, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(tacBook, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtTransactionDate, new ControlEntrySetting(true, false, true, Constant.DefaultValueEntry.DATE_NOW));
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
            tdTransactionNoEdit.Style.Add("display", "none");
            tdTransactionNoAdd.Style.Remove("display");

            divCreatedBy.InnerHtml = "";
            divLastUpdatedBy.InnerHtml = "";
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
            return BusinessLayer.GetvTreasuryHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vTreasuryHd entity = BusinessLayer.GetvTreasuryHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
            hdnGCTransactionStatus.Value = entity.GCTransactionStatus;
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvTreasuryHdRowIndex(filterExpression, keyValue, "TransactionID DESC");
            vTreasuryHd entity = BusinessLayer.GetvTreasuryHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
            hdnGCTransactionStatus.Value = entity.GCTransactionStatus;
        }

        private void EntityToControl(vTreasuryHd entity, ref bool isShowWatermark, ref string watermarkText)
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
            hdnID.Value = entity.TransactionID.ToString();
            txtTransactionNo.Text = entity.TransactionNo;
            cboTransactionCode.Value = entity.TransactionCode;
            tacBook.Value = entity.BookID.ToString();
            tacBook.Text = entity.BookName;

            txtTransactionDate.Text = entity.TransactionDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtRemarks.Text = entity.Remarks;

            divCreatedBy.InnerHtml = string.Format(@"{0} / {1}", entity.CreatedByName, entity.CreatedDate.ToString(Constant.FormatString.DATE_FORMAT));
            string lastUpdatedDate = string.Empty;
            if (entity.LastUpdatedDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT) == Constant.ConstantDate.DEFAULT_NULL)
                lastUpdatedDate = "";
            else
                lastUpdatedDate = " / " + entity.LastUpdatedDate.ToString(Constant.FormatString.DATE_FORMAT);
            divLastUpdatedBy.InnerHtml = string.Format(@"{0} {1}", entity.LastUpdatedByName, lastUpdatedDate);

            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                string filterExpression = string.Format("TransactionID = {0} AND GCItemDetailStatus != '{1}' ORDER BY DisplayOrder ASC", hdnID.Value, Constant.TransactionStatus.VOID);

                List<vTreasuryDt> lstEntity = BusinessLayer.GetvTreasuryDtList(filterExpression);
                decimal totalDebet = lstEntity.Sum(x => x.DebitAmount);
                decimal totalKredit = lstEntity.Sum(x => x.CreditAmount);
                rptJournalViewDt.DataSource = lstEntity;
                rptJournalViewDt.DataBind();

                txtTotalDebitView.Value = totalDebet.ToString();
                txtTotalKreditView.Value = totalKredit.ToString();
            }
        }
        #endregion

        public void SaveTreasuryHd(IDbContext ctx, ref int TransactionID)
        {
            TreasuryHdDao entityHdDao = new TreasuryHdDao(ctx);
            if (hdnID.Value == "" || hdnID.Value == "0")
            {
                DateTime journalDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
                bool isAllowSave = true;
                if (hdnLastPostingDate.Value != "")
                {
                    DateTime lastPostingDate = Helper.GetDatePickerValue(hdnLastPostingDate.Value);
                    if (journalDate <= lastPostingDate)
                        isAllowSave = false;
                }

                if (isAllowSave)
                {
                    TreasuryHd entityHd = new TreasuryHd();
                    entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
                    entityHd.GCJournalGroup = Constant.JournalGroup.MEMORIAL;
                    entityHd.TransactionCode = cboTransactionCode.Value.ToString();
                    entityHd.BookID = Convert.ToInt32(tacBook.Value);

                    entityHd.Remarks = txtRemarks.Text;
                    entityHd.TransactionNo = BusinessLayer.GenerateTransactionNo(entityHd.TransactionCode, entityHd.TransactionDate, txtJournalPrefix.Text, ctx);
                    entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;

                    ctx.CommandType = CommandType.Text;
                    ctx.Command.Parameters.Clear();
                    entityHd.CreatedBy = AppSession.UserLogin.UserID;
                    TransactionID = entityHdDao.Insert(entityHd);
                    
                    hdnID.Value = TransactionID.ToString();
                }
                else
                {
                    TransactionID = 0;
                }
            }
            else
            {
                TransactionID = Convert.ToInt32(hdnID.Value);
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TreasuryHdDao entityHdDao = new TreasuryHdDao(ctx);
            TreasuryDtDao entityDtDao = new TreasuryDtDao(ctx);
            try
            {
                TreasuryHd entityHd = new TreasuryHd();
                entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
                entityHd.GCJournalGroup = Constant.JournalGroup.MEMORIAL;
                entityHd.TransactionCode = cboTransactionCode.Value.ToString();
                entityHd.BookID = Convert.ToInt32(tacBook.Value);

                entityHd.Remarks = txtRemarks.Text;
                entityHd.TransactionNo = BusinessLayer.GenerateTransactionNo(entityHd.TransactionCode, entityHd.TransactionDate, txtJournalPrefix.Text, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;

                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                int TransactionID = entityHdDao.Insert(entityHd);

                JavaScriptSerializer json = new JavaScriptSerializer();
                List<string[]> lstSaveParam = json.Deserialize<List<string[]>>(hdnSaveParam.Value);
                short i = 1;
                foreach (string[] param in lstSaveParam)
                {
                    TreasuryDt entityDt = new TreasuryDt();
                    entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                    entityDt.TransactionID = TransactionID;
                    entityDt.GLAccount = Convert.ToInt32(param[1]);
                    if (param[2] == "")
                        entityDt.SubLedger = null;
                    else
                        entityDt.SubLedger = Convert.ToInt32(param[2]);
                    entityDt.Remarks = param[3];
                    entityDt.DebitAmount = Convert.ToDecimal(param[4]);
                    entityDt.CreditAmount = Convert.ToDecimal(param[5]);
                    entityDt.ReferenceNo = param[6];
                    if (entityDt.CreditAmount == 0)
                        entityDt.Position = "D";
                    else
                        entityDt.Position = "K";
                    entityDt.DisplayOrder = i++;
                    entityDt.CreatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Insert(entityDt);
                }

                retval = TransactionID.ToString();
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
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TreasuryHdDao entityHdDao = new TreasuryHdDao(ctx);
            TreasuryDtDao entityDtDao = new TreasuryDtDao(ctx);
            try
            {
                TreasuryHd entityHd = entityHdDao.Get(Convert.ToInt32(hdnID.Value));
                entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
                entityHd.GCJournalGroup = Constant.JournalGroup.MEMORIAL;
                entityHd.TransactionCode = cboTransactionCode.Value.ToString();
                entityHd.Remarks = txtRemarks.Text;
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Update(entityHd);

                List<TreasuryDt> lstTreasuryDt = null;
                if (hdnListTransactionDtID.Value != "")
                    lstTreasuryDt = BusinessLayer.GetTreasuryDtList(string.Format("TransactionID = {0} AND GCItemDetailStatus != '{1}'", hdnID.Value, Constant.TransactionStatus.VOID));

                JavaScriptSerializer json = new JavaScriptSerializer();
                List<string[]> lstSaveParam = json.Deserialize<List<string[]>>(hdnSaveParam.Value);
                short i = 1;
                foreach (string[] param in lstSaveParam)
                {
                    int transactionDtID = Convert.ToInt32(param[0]);
                    if (transactionDtID > 0)
                    {
                        TreasuryDt entityDt = lstTreasuryDt.FirstOrDefault(p => p.TransactionDtID == transactionDtID);
                        entityDt.GLAccount = Convert.ToInt32(param[1]);
                        if (param[2] == "")
                            entityDt.SubLedger = null;
                        else
                            entityDt.SubLedger = Convert.ToInt32(param[2]);
                        entityDt.Remarks = param[3];
                        entityDt.DebitAmount = Convert.ToDecimal(param[4]);
                        entityDt.CreditAmount = Convert.ToDecimal(param[5]);
                        entityDt.ReferenceNo = param[6];
                        if (entityDt.CreditAmount == 0)
                            entityDt.Position = "D";
                        else
                            entityDt.Position = "K";
                        entityDt.DisplayOrder = i++;
                        entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDtDao.Update(entityDt);

                        lstTreasuryDt.Remove(entityDt);
                    }
                    else
                    {
                        TreasuryDt entityDt = new TreasuryDt();
                        entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                        entityDt.TransactionID = entityHd.TransactionID;
                        entityDt.GLAccount = Convert.ToInt32(param[1]);
                        if (param[2] == "")
                            entityDt.SubLedger = null;
                        else
                            entityDt.SubLedger = Convert.ToInt32(param[2]);
                        entityDt.Remarks = param[3];
                        entityDt.DebitAmount = Convert.ToDecimal(param[4]);
                        entityDt.CreditAmount = Convert.ToDecimal(param[5]);
                        entityDt.ReferenceNo = param[6];
                        if (entityDt.CreditAmount == 0)
                            entityDt.Position = "D";
                        else
                            entityDt.Position = "K";
                        entityDt.DisplayOrder = i++;
                        entityDt.CreatedBy = AppSession.UserLogin.UserID;
                        entityDtDao.Insert(entityDt);
                    }
                }
                foreach (TreasuryDt entityDt in lstTreasuryDt)
                {
                    entityDt.GCItemDetailStatus = Constant.TransactionStatus.VOID;
                    entityDt.IsDeleted = true;
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Update(entityDt);
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

        protected override bool OnApproveRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TreasuryHdDao TreasuryHdDao = new TreasuryHdDao(ctx);
            TreasuryDtDao GlTransactionDtDao = new TreasuryDtDao(ctx);
            try
            {
                TreasuryHd itemTransactionHd = TreasuryHdDao.Get(Convert.ToInt32(hdnID.Value));
                itemTransactionHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                itemTransactionHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                TreasuryHdDao.Update(itemTransactionHd);

                string filterExpression = String.Format("TransactionID = {0} AND GCItemDetailStatus != '{1}' ORDER BY DisplayOrder", hdnID.Value, Constant.TransactionStatus.VOID);
                List<TreasuryDt> lstTreasuryDt = BusinessLayer.GetTreasuryDtList(filterExpression, ctx);
                foreach (TreasuryDt GlTransactionDt in lstTreasuryDt)
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

            return result;
        }
    }
}