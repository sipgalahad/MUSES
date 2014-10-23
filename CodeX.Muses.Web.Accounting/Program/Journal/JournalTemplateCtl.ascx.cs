using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Common;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common;
using System.Data;
using CodeX.Data.Core.Dal;
using CodeX.Data.Model;
using Codex.Muses.Web.Accounting.Program;

namespace CodeX.Web.Accounting.Program
{
    public partial class JournalTemplateCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        
        public override void InitializeDataControl(string param)
        {
            IsAdd = true;
            hdnGLTransactionID.Value = param;
        }

        private JournalEntry DetailPage
        {
            get { return (JournalEntry)Page; }
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtTemplateCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtAmount, new ControlEntrySetting(true, true, true, "0"));
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            GLTransactionDtDao glTransactionDtDao = new GLTransactionDtDao(ctx);

            try
            {
                int GLTransactionID = 0;
                if (hdnGLTransactionID.Value != null && hdnGLTransactionID.Value != "")
                    GLTransactionID = Convert.ToInt32(hdnGLTransactionID.Value);
                DetailPage.SaveGLTransactionHd(ctx, ref GLTransactionID);
                if (GLTransactionID != 0)
                {
                    List<JournalTemplateDt> lstJournalTemplate = BusinessLayer.GetJournalTemplateDtList(String.Format("TemplateID = {0}", hdnTemplateID.Value));

                    foreach (JournalTemplateDt entity in lstJournalTemplate)
                    {
                        GLTransactionDt glTransactionDt = new GLTransactionDt();
                        glTransactionDt.GLTransactionID = GLTransactionID;
                        glTransactionDt.GLAccount = entity.GLAccountID;
                        glTransactionDt.SubLedger = entity.SubLedgerID;
                        glTransactionDt.Position = entity.Position;
                        Decimal amount = Convert.ToDecimal(txtAmount.Text);
                        if (entity.Position == "D")
                        {
                            glTransactionDt.DebitAmount = amount * (entity.AmountPercentage / 100);
                        }
                        else
                        {
                            glTransactionDt.CreditAmount = amount * (entity.AmountPercentage / 100);
                        }
                        glTransactionDt.DisplayOrder = Convert.ToInt16(entity.DisplayOrder);
                        glTransactionDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                        glTransactionDt.CreatedBy = AppSession.UserLogin.UserID;
                        glTransactionDtDao.Insert(glTransactionDt);
                    }

                    retval = GLTransactionID.ToString();
                }
                else 
                {
                    errMessage = "Jurnal Pada Periode ini Telah Diposting";
                    result = false;
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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
    }
}