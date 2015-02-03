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
    public partial class JournalEntry : BasePageTrx
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

        protected int minDate = -1;
        protected override void InitializeDataControl()
        {
            vGLTransactionHd entity = BusinessLayer.GetvGLTransactionHd(string.Format("TransactionCode = '{0}'", Constant.TransactionCode.JOURNAL_MEMORIAL_IKHTISAR), 0, "JournalDate DESC");
            if (entity != null)
            {
                hdnLastPostingDate.Value = entity.JournalDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                minDate = (DateTime.Now - entity.JournalDate).Days - 1;
            }
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

            divCreatedBy.InnerHtml = string.Format(@"{0} / {1}", entity.CreatedByName, entity.CreatedDate.ToString(Constant.FormatString.DATE_FORMAT));
            string lastUpdatedDate = string.Empty;
            if (entity.LastUpdatedDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT) == Constant.ConstantDate.DEFAULT_NULL)
                lastUpdatedDate = "";
            else
                lastUpdatedDate = " / " + entity.LastUpdatedDate.ToString(Constant.FormatString.DATE_FORMAT);
            divLastUpdatedBy.InnerHtml = string.Format(@"{0} {1}", entity.LastUpdatedByName, lastUpdatedDate);

            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                string filterExpression = string.Format("GLTransactionID = {0} AND GCItemDetailStatus != '{1}' ORDER BY DisplayOrder ASC", hdnID.Value, Constant.TransactionStatus.VOID);

                List<vGLTransactionDt> lstEntity = BusinessLayer.GetvGLTransactionDtList(filterExpression);
                decimal totalDebet = lstEntity.Sum(x => x.DebitAmount);
                decimal totalKredit = lstEntity.Sum(x => x.CreditAmount);
                rptJournalViewDt.DataSource = lstEntity;
                rptJournalViewDt.DataBind();

                txtTotalDebitView.Value = totalDebet.ToString();
                txtTotalKreditView.Value = totalKredit.ToString();
            }
        }
        #endregion

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
            GLTransactionHdDao entityHdDao = new GLTransactionHdDao(ctx);
            GLTransactionDtDao entityDtDao = new GLTransactionDtDao(ctx);
            try
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
                int GLTransactionID = BusinessLayer.GetGLTransactionMaxID(ctx);

                string[] lstSaveParam = hdnSaveParam.Value.Split('|');
                short i = 1;
                foreach (string saveParam in lstSaveParam)
                {
                    string[] param = saveParam.Split(',');
                    GLTransactionDt entityDt = new GLTransactionDt();
                    entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                    entityDt.GLTransactionID = GLTransactionID;
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

                retval = GLTransactionID.ToString();
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
            GLTransactionHdDao entityHdDao = new GLTransactionHdDao(ctx);
            GLTransactionDtDao entityDtDao = new GLTransactionDtDao(ctx);
            try
            {
                GLTransactionHd entityHd = entityHdDao.Get(Convert.ToInt32(hdnID.Value));
                entityHd.JournalDate = Helper.GetDatePickerValue(Request.Form[txtJournalDate.UniqueID]);
                entityHd.GCJournalGroup = Constant.JournalGroup.MEMORIAL;
                entityHd.TransactionCode = cboTransactionCode.Value.ToString();
                entityHd.Remarks = txtRemarks.Text;
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Update(entityHd);

                List<GLTransactionDt> lstGLTransactionDt = null;
                if (hdnListTransactionDtID.Value != "")
                    lstGLTransactionDt = BusinessLayer.GetGLTransactionDtList(string.Format("GLTransactionID = {0} AND GCItemDetailStatus != '{1}'", hdnID.Value, Constant.TransactionStatus.VOID));

                string[] lstSaveParam = hdnSaveParam.Value.Split('|');
                short i = 1;
                foreach (string saveParam in lstSaveParam)
                {
                    string[] param = saveParam.Split(',');

                    int transactionDtID = Convert.ToInt32(param[0]);
                    if (transactionDtID > 0)
                    {
                        GLTransactionDt entityDt = lstGLTransactionDt.FirstOrDefault(p => p.TransactionDtID == transactionDtID);
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

                        lstGLTransactionDt.Remove(entityDt);
                    }
                    else
                    {
                        GLTransactionDt entityDt = new GLTransactionDt();
                        entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                        entityDt.GLTransactionID = entityHd.GLTransactionID;
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
                foreach (GLTransactionDt entityDt in lstGLTransactionDt)
                {
                    entityDt.GCItemDetailStatus = Constant.TransactionStatus.VOID;
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
            GLTransactionHdDao GLTransactionHdDao = new GLTransactionHdDao(ctx);
            GLTransactionDtDao GlTransactionDtDao = new GLTransactionDtDao(ctx);
            try
            {
                GLTransactionHd itemTransactionHd = GLTransactionHdDao.Get(Convert.ToInt32(hdnID.Value));
                if (itemTransactionHd.DebitAmount == itemTransactionHd.CreditAmount)
                {
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
                }
                else
                {
                    result = false;
                    errMessage = "Journal Tidak Seimbang";
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