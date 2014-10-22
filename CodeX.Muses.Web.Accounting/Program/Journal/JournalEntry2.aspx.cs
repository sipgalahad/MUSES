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

namespace Codex.Muses.Web.Accounting.Program
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
            #endregion

            decimal totalDebit = -1;
            decimal totalKredit = -1;
            decimal selisih = -1;
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
        }
        #endregion

    }
}