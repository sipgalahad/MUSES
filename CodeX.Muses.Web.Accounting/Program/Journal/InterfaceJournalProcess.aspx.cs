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
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.Accounting.Program
{
    public partial class InterfaceJournalProcess : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Accounting.INTERFACE_JOURNAL_PROCESS;
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowAdd = IsAllowNextPrev = IsAllowSave = IsAllowVoid = false;
        }

        protected int minDate = -1;
        protected override void InitializeDataControl()
        {
            hdnDefaultFromJournalDate.Value = BusinessLayer.GetSiteParameter(AppSession.UserLogin.SiteID, Constant.SiteParameter.DEFAULT_INTERFACE_JOURNAL_START_DATE).ParameterValue;

            txtToJournalDate.Text = DateTime.Today.ToString(Constant.FormatString.DATE_PICKER_FORMAT);

            vGLTransactionHd entity = BusinessLayer.GetvGLTransactionHd(string.Format("TransactionCode = '{0}'", Constant.TransactionCode.JOURNAL_MEMORIAL_IKHTISAR), 0, "JournalDate DESC");
            if (entity != null)
            {
                hdnLastPostingDate.Value = entity.JournalDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                minDate = (DateTime.Now - entity.JournalDate).Days - 1;
            }
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND StandardCodeID != '{1}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.JOURNAL_GROUP, Constant.JournalGroup.MEMORIAL));
            Methods.SetRadioButtonListField<StandardCode>(rblJournalGroup, lstStandardCode, "StandardCodeName", "StandardCodeID");
            rblJournalGroup.SelectedIndex = 0;

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        List<vGLTransactionHdPerTransactionCode> lstMaxJournalDate = null;
        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "";
            string GCJournalGroup = rblJournalGroup.SelectedValue;
            switch (GCJournalGroup)
            {
                case Constant.JournalGroup.PENDAPATAN_PENERIMAAN: filterExpression = string.Format("TransactionCode BETWEEN '7200' AND '7220'"); break;
                case Constant.JournalGroup.HUTANG_PIUTANG: filterExpression = string.Format("TransactionCode BETWEEN '7221' AND '7240'"); break;
                case Constant.JournalGroup.INVENTORY: filterExpression = string.Format("TransactionCode BETWEEN '7241' AND '7260'"); break;
                case Constant.JournalGroup.PHARMACY: filterExpression = string.Format("TransactionCode BETWEEN '7261' AND '7270'"); break;
                case Constant.JournalGroup.FIXED_ASSET: filterExpression = string.Format("TransactionCode BETWEEN '7271' AND '7280'"); break;
            }

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetTransactionTypeRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<TransactionType> lstEntity = BusinessLayer.GetTransactionTypeList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
            string lstTransactionCode = string.Join(",", lstEntity.Select(p => string.Format("'{0}'", p.TransactionCode)).ToList());
            lstMaxJournalDate = BusinessLayer.GetvGLTransactionHdPerTransactionCodeList(string.Format("TransactionCode IN ({0})", lstTransactionCode));

            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TransactionType entity = (TransactionType)e.Row.DataItem;
                vGLTransactionHdPerTransactionCode maxJournalDate = lstMaxJournalDate.FirstOrDefault(p => p.TransactionCode == entity.TransactionCode);
                HtmlInputHidden hdnLastJournalDate = (HtmlInputHidden)e.Row.FindControl("hdnLastJournalDate");
                if (maxJournalDate != null)
                {
                    HtmlGenericControl divLastJournalDate = (HtmlGenericControl)e.Row.FindControl("divLastJournalDate");
                    hdnLastJournalDate.Value = divLastJournalDate.InnerHtml = maxJournalDate.MaxJournalDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                }
                else
                    hdnLastJournalDate.Value = hdnDefaultFromJournalDate.Value;
            }
        }

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

        protected override bool OnCustomButtonClick(string type, ref string errMessage, ref string retval)
        {
            try
            {
                string transactionCode = hdnID.Value;
                DateTime fromJournalDate = Helper.GetDatePickerValue(hdnFromJournalDate.Value);
                DateTime toJournalDate =  Helper.GetDatePickerValue(txtToJournalDate);
                bool isAllowSave = true;
                if (hdnLastPostingDate.Value != "")
                {
                    DateTime lastPostingDate = Helper.GetDatePickerValue(hdnLastPostingDate.Value);
                    if (fromJournalDate <= lastPostingDate || toJournalDate <= lastPostingDate)
                        isAllowSave = false;
                }
                if (isAllowSave)
                {
                    string journalDate = string.Format("{0}|{1}", fromJournalDate.ToString("yyyyMMdd"), toJournalDate.ToString("yyyyMMdd"));
                    retval = BusinessLayer.ProcessInterfaceJournal(AppSession.UserLogin.SiteID, journalDate, transactionCode, AppSession.UserLogin.UserID);
                    return true;
                }
                else
                {
                    errMessage = "Journal Pada Periode ini Telah Diposting";
                    return false;
                }
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }
    }
}