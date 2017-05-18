using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Core.Dal;
using CodeX.Common;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.Accounting.Program
{
    public partial class JournalList : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Accounting.JOURNAL_LIST;
        }

        public string GetGCTransactionStatusOpen() 
        {
            return Constant.TransactionStatus.OPEN;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            txtFromJournalDate.Text = DateTime.Today.AddDays(-7).ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtToJournalDate.Text = DateTime.Today.ToString(Constant.FormatString.DATE_PICKER_FORMAT);

            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.JOURNAL_GROUP));
            Methods.SetRadioButtonListField<StandardCode>(rblJournalGroup, lstStandardCode, "StandardCodeName", "StandardCodeID");
            rblJournalGroup.SelectedIndex = 0;

            hdnFilterExpression.Value = filterExpression;
            hdnID.Value = keyValue;
            filterExpression = GetFilterExpression();
            CurrPage = 1;

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        public override void SetFilterParameter(ref string[] fieldListText, ref string[] fieldListValue)
        {
            fieldListText = new string[] { "No Jurnal", "Catatan" };
            fieldListValue = new string[] { "JournalNo", "Remarks" };
        }

        private string GetFilterExpression()
        {
            string filterExpression = hdnFilterExpression.Value;
            string GCJournalGroup = rblJournalGroup.SelectedValue;
            switch (GCJournalGroup)
            {
                case Constant.JournalGroup.PENDAPATAN_PENERIMAAN: filterExpression = string.Format("TransactionCode BETWEEN '7200' AND '7220'"); break;
                case Constant.JournalGroup.HUTANG_PIUTANG: filterExpression = string.Format("TransactionCode BETWEEN '7221' AND '7240'"); break;
                case Constant.JournalGroup.INVENTORY: filterExpression = string.Format("TransactionCode BETWEEN '7241' AND '7260'"); break;
                case Constant.JournalGroup.PHARMACY: filterExpression = string.Format("TransactionCode BETWEEN '7261' AND '7270'"); break;
                case Constant.JournalGroup.FIXED_ASSET: filterExpression = string.Format("TransactionCode BETWEEN '7271' AND '7280'"); break;
                case Constant.JournalGroup.MEMORIAL: filterExpression = string.Format("TransactionCode BETWEEN '7281' AND '7299'"); break;
            }
            filterExpression += string.Format(" AND JournalDate BETWEEN '{0}' AND '{1}'", Helper.GetDatePickerValue(txtFromJournalDate).ToString("yyyyMMdd"), Helper.GetDatePickerValue(txtToJournalDate).ToString("yyyyMMdd"));
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvGLTransactionHdRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vGLTransactionHd> lstEntity = BusinessLayer.GetvGLTransactionHdList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "JournalNo");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
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

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            if (type == "approve")
                ApproveJournal(ref result, ref errMessage);
            else if (type == "void")
                VoidJournal(ref result, ref errMessage);
            else 
                UnapproveJournal(ref result, ref errMessage);

            return result;
        }

        private void ApproveJournal(ref bool result, ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            GLTransactionHdDao glTransactionhdDao = new GLTransactionHdDao(ctx);
            GLTransactionDtDao glTransactionDtDao = new GLTransactionDtDao(ctx);
            try
            {
                GLTransactionHd entityHD = glTransactionhdDao.Get(Convert.ToInt32(hdnID.Value));
                if (entityHD.CreditAmount == entityHD.DebitAmount)
                {
                    entityHD.GCTransactionStatus = Constant.TransactionStatus.APPROVED;

                    List<GLTransactionDt> lstEntityDt = BusinessLayer.GetGLTransactionDtList(String.Format("GLTransactionID = {0} AND GCItemDetailStatus IN ('{1}','{2}') AND IsDeleted = 0", hdnID.Value, Constant.TransactionStatus.OPEN, Constant.TransactionStatus.WAIT_FOR_APPROVAL), ctx);
                    foreach (GLTransactionDt entityDt in lstEntityDt)
                    {
                        entityDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
                        entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        glTransactionDtDao.Update(entityDt);
                    }
                    entityHD.LastUpdatedBy = AppSession.UserLogin.UserID;
                    glTransactionhdDao.Update(entityHD);
                    result = true;
                }
                else
                {
                    result = false;
                    errMessage = "Jurnal Tidak balance";
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
        }

        private void VoidJournal(ref bool result, ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            GLTransactionHdDao glTransactionhdDao = new GLTransactionHdDao(ctx);
            GLTransactionDtDao glTransactionDtDao = new GLTransactionDtDao(ctx);
            TreasuryHdDao entityTreasuryHdDao = new TreasuryHdDao(ctx);
            TreasuryDtDao entityTreasuryDtDao = new TreasuryDtDao(ctx);
            try
            {
                GLTransactionHd entityHD = glTransactionhdDao.Get(Convert.ToInt32(hdnID.Value));
                entityHD.GCTransactionStatus = Constant.TransactionStatus.VOID;

                List<GLTransactionDt> lstEntityDt = BusinessLayer.GetGLTransactionDtList(String.Format("GLTransactionID = {0} AND GCItemDetailStatus IN ('{1}','{2}') AND IsDeleted = 0", hdnID.Value, Constant.TransactionStatus.OPEN, Constant.TransactionStatus.WAIT_FOR_APPROVAL), ctx);
                foreach (GLTransactionDt entityDt in lstEntityDt)
                {
                    entityDt.GCItemDetailStatus = Constant.TransactionStatus.VOID;
                    entityDt.IsDeleted = true;
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    glTransactionDtDao.Update(entityDt);
                }
                entityHD.LastUpdatedBy = AppSession.UserLogin.UserID;
                glTransactionhdDao.Update(entityHD);

                TreasuryHd entityTreasuryHd = BusinessLayer.GetTreasuryHdList(string.Format("GLTransactionID = {0} AND GCTransactionStatus = '{1}'", entityHD.GLTransactionID, Constant.TransactionStatus.APPROVED), ctx).FirstOrDefault();
                if (entityTreasuryHd != null)
                {
                    entityTreasuryHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                    entityTreasuryHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityTreasuryHdDao.Update(entityTreasuryHd);

                    string filterExpression = String.Format("TransactionID = {0} AND GCItemDetailStatus != '{1}'", hdnID.Value, Constant.TransactionStatus.VOID);
                    List<TreasuryDt> lstTreasuryDt = BusinessLayer.GetTreasuryDtList(filterExpression, ctx);
                    foreach (TreasuryDt entityTreasuryDt in lstTreasuryDt)
                    {
                        entityTreasuryDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                        entityTreasuryDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityTreasuryDtDao.Update(entityTreasuryDt);
                    }
                }

                ctx.CommitTransaction();
                result = true;
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
        }

        private void UnapproveJournal(ref bool result, ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            GLTransactionHdDao glTransactionhdDao = new GLTransactionHdDao(ctx);
            GLTransactionDtDao glTransactionDtDao = new GLTransactionDtDao(ctx);
            try
            {
                GLTransactionHd entityHD = glTransactionhdDao.Get(Convert.ToInt32(hdnID.Value));
                entityHD.GCTransactionStatus = Constant.TransactionStatus.OPEN;

                List<GLTransactionDt> lstEntityDt = BusinessLayer.GetGLTransactionDtList(String.Format("GLTransactionID = {0} AND GCItemDetailStatus IN ('{1}','{2}') AND IsDeleted = 0", hdnID.Value, Constant.TransactionStatus.OPEN, Constant.TransactionStatus.WAIT_FOR_APPROVAL), ctx);
                foreach (GLTransactionDt entityDt in lstEntityDt)
                {
                    entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    glTransactionDtDao.Update(entityDt);
                }
                entityHD.LastUpdatedBy = AppSession.UserLogin.UserID;
                glTransactionhdDao.Update(entityHD);
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
            }
        }

        public override Control OnGetExportControl()
        {
            string filterExpression = "";
            string GCJournalGroup = hdnSelectedJournalGroup.Value;
            switch (GCJournalGroup)
            {
                case Constant.JournalGroup.PENDAPATAN_PENERIMAAN: filterExpression = string.Format("TransactionCode BETWEEN '7200' AND '7220'"); break;
                case Constant.JournalGroup.HUTANG_PIUTANG: filterExpression = string.Format("TransactionCode BETWEEN '7221' AND '7240'"); break;
                case Constant.JournalGroup.INVENTORY: filterExpression = string.Format("TransactionCode BETWEEN '7241' AND '7260'"); break;
                case Constant.JournalGroup.PHARMACY: filterExpression = string.Format("TransactionCode BETWEEN '7261' AND '7270'"); break;
                case Constant.JournalGroup.FIXED_ASSET: filterExpression = string.Format("TransactionCode BETWEEN '7271' AND '7280'"); break;
                case Constant.JournalGroup.MEMORIAL: filterExpression = string.Format("TransactionCode BETWEEN '7281' AND '7299'"); break;
            }
            filterExpression += string.Format(" AND JournalDate BETWEEN '{0}' AND '{1}'", Helper.GetDatePickerValue(txtFromJournalDate).ToString("yyyyMMdd"), Helper.GetDatePickerValue(txtToJournalDate).ToString("yyyyMMdd"));
            List<vGLTransactionHd> lstEntity = BusinessLayer.GetvGLTransactionHdList(hdnFilterExpression.Value);
            grdView.DataSource = lstEntity;
            grdView.DataBind();

            HtmlGenericControl div = new HtmlGenericControl("DIV");
            HtmlGenericControl h4 = new HtmlGenericControl("h4");
            h4.InnerHtml = String.Format("Periode : {0} - {1}", Helper.GetDatePickerValue(txtFromJournalDate.Text).ToString(Constant.FormatString.DATE_FORMAT), Helper.GetDatePickerValue(txtToJournalDate.Text).ToString(Constant.FormatString.DATE_FORMAT));
            div.Controls.Add(h4);
            div.Controls.Add(PanelContent1);
            return div;
        }
    }
}