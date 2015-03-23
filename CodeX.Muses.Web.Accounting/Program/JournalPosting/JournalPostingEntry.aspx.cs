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
using DevExpress.Web.ASPxEditors;
using System.Globalization;
using System.Data.SqlClient;
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class JournalPostingEntry : BasePageList
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Accounting.JOURNAL_POSTING;
        }

        public override void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowDelete = IsAllowEdit = false;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            filterExpression = String.Format("TransactionCode = '{0}'", Constant.TransactionCode.JOURNAL_MEMORIAL_IKHTISAR);
            vGLTransactionHd entity = BusinessLayer.GetvGLTransactionHd(filterExpression, 0, "JournalDate DESC");
            if (entity != null)
            {
                divCreatedBy.InnerHtml = entity.CreatedByName;
                divCreatedDate.InnerHtml = entity.CreatedDate.ToString(Constant.FormatString.DATE_FORMAT);
                divJournalDate.InnerHtml = entity.JournalDate.ToString(Constant.FormatString.DATE_FORMAT);
                txtPeriod.Text = entity.JournalDate.AddMonths(1).ToString("MMM-yyyy");
                hdnPeriodNo.Value = entity.JournalDate.AddMonths(1).ToString("yyyyMM");
            }
            else
            {
                divCreatedBy.InnerHtml = "-";
                divCreatedDate.InnerHtml = "-";
                divJournalDate.InnerHtml = "-";
                filterExpression = String.Format("GCTransactionStatus != '{0}'", Constant.TransactionStatus.VOID);
                entity = BusinessLayer.GetvGLTransactionHd(filterExpression, 0, "JournalDate ASC");
                if (entity != null)
                {
                    txtPeriod.Text = entity.JournalDate.ToString("MMM-yyyy");
                    hdnPeriodNo.Value = entity.JournalDate.ToString("yyyyMM");
                }
            }

        }

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            if (PostingJournal(ref errMessage))
                result += "success";
            else
                result += string.Format("fail|{0}", errMessage);

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool PostingJournal(ref string errMessage)
        {
            try
            {
                bool result = BusinessLayer.PostingJournal(AppSession.UserLogin.SiteID, hdnPeriodNo.Value, AppSession.UserLogin.UserID);
                if (result)
                    return true;
                errMessage = "Ada Jurnal Yang Belum Seimbang. Proses Gagal Dilakukan";
                return false;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
    }
}