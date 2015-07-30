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
using CodeX.Muses.Web.Accounting.Program;

namespace CodeX.Web.Accounting.Program
{
    public partial class JournalTemplateSaveCtl : BaseEntryPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            IsAdd = true;
            hdnLstJournalTemplateDt.Value = param;
            //txtTemplateCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtTemplateCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtTemplateName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        private void ControlToEntity(JournalTemplateHd entity)
        {
            entity.TemplateCode = txtTemplateCode.Text;
            entity.TemplateName = txtTemplateName.Text;
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            JournalTemplateHdDao entityHdDao = new JournalTemplateHdDao(ctx);
            JournalTemplateDtDao entityDtDao = new JournalTemplateDtDao(ctx);
            bool result = false;
            try
            {
                JournalTemplateHd entityHd = new JournalTemplateHd();
                ControlToEntity(entityHd);
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);
                entityHd.TemplateID = BusinessLayer.GetJournalTemplateHdMaxID(ctx);

                string[] lstSaveParam = hdnLstJournalTemplateDt.Value.Split('|');
                short i = 1;
                List<JournalTemplateDt> lstJournalTemplateDt = new List<JournalTemplateDt>();
                foreach (string saveParam in lstSaveParam)
                {
                    string[] param = saveParam.Split(';');
                    JournalTemplateDt entityDt = new JournalTemplateDt();
                    entityDt.TemplateID = entityHd.TemplateID;
                    entityDt.GLAccountID = Convert.ToInt32(param[1]);
                    if (param[2] == "")
                        entityDt.SubLedgerID = null;
                    else
                        entityDt.SubLedgerID = Convert.ToInt32(param[2]);

                    decimal DebitAmount = Convert.ToDecimal(param[4]);
                    decimal CreditAmount = Convert.ToDecimal(param[5]);
                    if (CreditAmount == 0)
                    {
                        entityDt.Position = "D";
                        entityDt.AmountPercentage = DebitAmount;
                    }
                    else
                    {
                        entityDt.Position = "K";
                        entityDt.AmountPercentage = CreditAmount;
                    }
                    entityDt.DisplayOrder = i++;
                    lstJournalTemplateDt.Add(entityDt);
                }

                decimal totalDebit = lstJournalTemplateDt.Where(p => p.Position == "D").Sum(p => p.AmountPercentage);
                decimal totalCredit = lstJournalTemplateDt.Where(p => p.Position == "K").Sum(p => p.AmountPercentage);
                foreach (JournalTemplateDt entityDt in lstJournalTemplateDt)
                {
                    if (entityDt.Position == "D")
                        entityDt.AmountPercentage = Convert.ToDecimal(entityDt.AmountPercentage / totalDebit) * 100;
                    else
                        entityDt.AmountPercentage = Convert.ToDecimal(entityDt.AmountPercentage / totalCredit) * 100;
                    entityDt.CreatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Insert(entityDt);
                }

                retval = entityHd.TemplateID.ToString();
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
    }
}