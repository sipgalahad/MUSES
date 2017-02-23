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
        protected string GetTreasuryBookFilterExpression()
        {
            if (hdnLstBookID.Value == "")
                return "1 = 0";
            return string.Format("BookID IN ({0}) AND IsDeleted = 0", hdnLstBookID.Value);
        }
        protected string GetServiceUnitFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND DepartmentID = 'OFFICE'", AppSession.UserLogin.SiteID);
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

            List<GetTreasuryBookUserList> lst = BusinessLayer.GetTreasuryBookUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, "");
            hdnLstBookID.Value = "";
            if (lst.Count > 0)
                hdnLstBookID.Value = string.Join(",", lst.Select(p => p.BookID).ToList());
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtTransactionNo, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(cboVoucherGroup, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(tacBook, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtTransactionDate, new ControlEntrySetting(true, false, true, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtSubLedgerName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtReferenceNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtReferenceDate, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(tacServiceUnit, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtJournalNo, new ControlEntrySetting(false, false, false));
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.VOUCHER_GROUP));
            Methods.SetComboBoxField<StandardCode>(cboVoucherGroup, lstSc, "StandardCodeName", "StandardCodeID");
        }

        #region Load Entity
        public override void OnAddRecord()
        {
            hdnID.Value = "0";
            hdnIsEditable.Value = "1";

            divCreatedBy.InnerHtml = "";
            divLastUpdatedBy.InnerHtml = "";
            trJournalNo.Style.Add("display", "none");
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

            if (entity.GCTransactionStatus == Constant.TransactionStatus.APPROVED)
            {
                txtJournalNo.Text = entity.JournalNo;
                trJournalNo.Style.Remove("display");
            }
            else
                trJournalNo.Style.Add("display", "none");
            
            hdnID.Value = entity.TransactionID.ToString();
            txtTransactionNo.Text = entity.TransactionNo;
            tacBook.Value = entity.BookID.ToString();
            tacBook.Text = entity.BookName;
            cboVoucherGroup.Value = entity.GCVoucherGroup;
            txtGLAccountName.Text = string.Format("{0} ({1})", entity.GLAccountName, entity.GLAccountNo);
            if (entity.SubLedgerCode != "")
                txtSubLedgerName.Text = string.Format("{0} ({1})", entity.SubLedgerName, entity.SubLedgerCode);
            else
                txtSubLedgerName.Text = "";
            txtReferenceNo.Text = entity.ReferenceNo;
            if (entity.ReferenceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT) != Constant.ConstantDate.DEFAULT_NULL)
                txtReferenceDate.Text = entity.ReferenceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            else
                txtReferenceDate.Text = "";
            txtTransactionDate.Text = entity.TransactionDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            tacServiceUnit.Value = entity.SiteServiceUnitID.ToString();
            tacServiceUnit.Text = entity.ServiceUnitName;
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
                decimal totalAmount = lstEntity.Sum(x => x.TotalAmount);
                rptJournalViewDt.DataSource = lstEntity;
                rptJournalViewDt.DataBind();

                txtTotalView.Value = totalAmount.ToString();
            }
        }
        #endregion

        private void ControlToEntity(TreasuryHd entityHd)
        {
            entityHd.GCVoucherGroup = cboVoucherGroup.Value.ToString();
            entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
            entityHd.BookID = Convert.ToInt32(tacBook.Value);
            entityHd.ReferenceNo = txtReferenceNo.Text;
            entityHd.ReferenceDate = Helper.GetDatePickerValue(Request.Form[txtReferenceDate.UniqueID]);
            if (tacServiceUnit.Value != "" && tacServiceUnit.Value != "0")
                entityHd.SiteServiceUnitID = Convert.ToInt32(tacServiceUnit.Value);
            else
                entityHd.SiteServiceUnitID = null;

            entityHd.Remarks = txtRemarks.Text;
        }

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
                    entityHd.GCJournalGroup = Constant.JournalGroup.MEMORIAL;
                    entityHd.TransactionCode = Constant.TransactionCode.TREASURY;
                    ControlToEntity(entityHd);
                    entityHd.TransactionNo = BusinessLayer.GenerateTransactionNo(entityHd.TransactionCode, entityHd.TransactionDate, ctx);
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
                entityHd.GCJournalGroup = Constant.JournalGroup.MEMORIAL;
                entityHd.TransactionCode = Constant.TransactionCode.TREASURY;
                ControlToEntity(entityHd);
                entityHd.TransactionNo = BusinessLayer.GenerateTransactionNo(entityHd.TransactionCode, entityHd.TransactionDate, ctx);
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
                    entityDt.TotalAmount = Convert.ToDecimal(param[4]);                    
                    entityDt.ReferenceNo = param[5];

                    if (entityHd.GCVoucherGroup == Constant.VoucherGroup.RECEIVE && entityDt.TotalAmount > 0)
                    {
                        entityDt.CreditAmount = entityDt.TotalAmount;
                        entityDt.DebitAmount = 0;
                    }
                    else
                    {
                        entityDt.DebitAmount = entityDt.TotalAmount;
                        entityDt.CreditAmount = 0;
                    }

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
                ControlToEntity(entityHd);
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
                        entityDt.TotalAmount = Convert.ToDecimal(param[4]);
                        entityDt.ReferenceNo = param[5];

                        if (entityHd.GCVoucherGroup == Constant.VoucherGroup.RECEIVE && entityDt.TotalAmount > 0)
                        {
                            entityDt.CreditAmount = entityDt.TotalAmount;
                            entityDt.DebitAmount = 0;
                        }
                        else
                        {
                            entityDt.DebitAmount = entityDt.TotalAmount;
                            entityDt.CreditAmount = 0;
                        }

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
                        entityDt.TotalAmount = Convert.ToDecimal(param[4]);
                        entityDt.ReferenceNo = param[5];

                        if (entityHd.GCVoucherGroup == Constant.VoucherGroup.RECEIVE && entityDt.TotalAmount > 0)
                        {
                            entityDt.CreditAmount = entityDt.TotalAmount;
                            entityDt.DebitAmount = 0;
                        }
                        else
                        {
                            entityDt.DebitAmount = entityDt.TotalAmount;
                            entityDt.CreditAmount = 0;
                        }
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
            GLTransactionHdDao entityHdDao = new GLTransactionHdDao(ctx);
            GLTransactionDtDao entityDtDao = new GLTransactionDtDao(ctx);
            TreasuryBookDao entityBookDao = new TreasuryBookDao(ctx);
            TransactionTypeDao entityTransactionTypeDao = new TransactionTypeDao(ctx);
            try
            {
                TreasuryHd itemTransactionHd = TreasuryHdDao.Get(Convert.ToInt32(hdnID.Value));
                if (itemTransactionHd.GCTransactionStatus == Constant.TransactionStatus.OPEN)
                {
                    TreasuryBook entityBook = entityBookDao.Get(itemTransactionHd.BookID);

                    GLTransactionHd entityHd = new GLTransactionHd();
                    entityHd.JournalDate = itemTransactionHd.TransactionDate;
                    entityHd.GCJournalGroup = Constant.JournalGroup.MEMORIAL;
                    if (entityBook.GCTreasuryBookType == Constant.TreasuryBookType.CASH)
                    {
                        if (itemTransactionHd.GCVoucherGroup == Constant.VoucherGroup.RECEIVE)
                            entityHd.TransactionCode = Constant.TransactionCode.JOURNAL_MEMORIAL_CASH_IN;
                        else
                            entityHd.TransactionCode = Constant.TransactionCode.JOURNAL_MEMORIAL_CASH_OUT;
                    }
                    else
                    {
                        if (itemTransactionHd.GCVoucherGroup == Constant.VoucherGroup.RECEIVE)
                            entityHd.TransactionCode = Constant.TransactionCode.JOURNAL_MEMORIAL_BANK_IN;
                        else
                            entityHd.TransactionCode = Constant.TransactionCode.JOURNAL_MEMORIAL_BANK_OUT;
                    }
                    TransactionType entityTransactionType = entityTransactionTypeDao.Get(entityHd.TransactionCode);

                    entityHd.IsGeneratedBySystem = true;
                    //entityHd.Remarks = txtRemarks.Text;
                    entityHd.Remarks = "";
                    entityHd.SiteServiceUnitID = itemTransactionHd.SiteServiceUnitID;
                    entityHd.JournalNo = BusinessLayer.GenerateTransactionNo(entityHd.TransactionCode, entityHd.JournalDate, entityTransactionType.TransactionInitial, ctx);
                    entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;

                    ctx.CommandType = CommandType.Text;
                    ctx.Command.Parameters.Clear();
                    entityHd.CreatedBy = AppSession.UserLogin.UserID;
                    int GLTransactionID = entityHdDao.Insert(entityHd);

                    itemTransactionHd.GLTransactionID = GLTransactionID;
                    itemTransactionHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                    itemTransactionHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    TreasuryHdDao.Update(itemTransactionHd);

                    {
                        GLTransactionDt entityDt = new GLTransactionDt();
                        entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                        entityDt.GLTransactionID = GLTransactionID;
                        entityDt.GLAccount = entityBook.GLAccount;
                        entityDt.SubLedger = entityBook.SubLedger;
                        entityDt.Remarks = entityHd.Remarks;
                        if (itemTransactionHd.GCVoucherGroup == Constant.VoucherGroup.RECEIVE && itemTransactionHd.TotalAmount > 0)
                        {
                            entityDt.DebitAmount = itemTransactionHd.TotalAmount;
                            entityHd.CreditAmount = 0;
                            entityDt.Position = "D";
                        }
                        else
                        {
                            entityDt.CreditAmount = itemTransactionHd.TotalAmount;
                            entityHd.DebitAmount = 0;
                            entityDt.Position = "K";
                        }
                        entityDt.ReferenceNo = "";
                        entityDt.DisplayOrder = 1;
                        entityDt.CreatedBy = AppSession.UserLogin.UserID;
                        entityDtDao.Insert(entityDt);
                    }

                    string filterExpression = String.Format("TransactionID = {0} AND GCItemDetailStatus != '{1}' ORDER BY DisplayOrder", hdnID.Value, Constant.TransactionStatus.VOID);
                    List<TreasuryDt> lstTreasuryDt = BusinessLayer.GetTreasuryDtList(filterExpression, ctx);
                    foreach (TreasuryDt GlTransactionDt in lstTreasuryDt)
                    {
                        GlTransactionDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
                        GlTransactionDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        GlTransactionDtDao.Update(GlTransactionDt);

                        GLTransactionDt entityDt = new GLTransactionDt();
                        entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                        entityDt.GLTransactionID = GLTransactionID;
                        entityDt.GLAccount = GlTransactionDt.GLAccount;
                        entityDt.SubLedger = GlTransactionDt.SubLedger;
                        entityDt.Remarks = GlTransactionDt.Remarks;
                        entityDt.DebitAmount = GlTransactionDt.DebitAmount;
                        entityDt.CreditAmount = GlTransactionDt.CreditAmount;
                        entityDt.ReferenceNo = GlTransactionDt.ReferenceNo;
                        entityDt.Position = GlTransactionDt.Position;
                        entityDt.DisplayOrder = (short)(GlTransactionDt.DisplayOrder + 1);
                        entityDt.CreatedBy = AppSession.UserLogin.UserID;
                        entityDtDao.Insert(entityDt);
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
    }
}