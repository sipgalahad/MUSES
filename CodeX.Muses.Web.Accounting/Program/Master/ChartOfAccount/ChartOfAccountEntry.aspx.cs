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
using CodeX.Common;

namespace CodeX.Muses.Web.Accounting.Program
{
    public partial class ChartOfAccountEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Accounting.CHART_OF_ACCOUNT;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                hdnID.Value = Request.QueryString["id"];
                vChartOfAccount entity = BusinessLayer.GetvChartOfAccountList(string.Format("GLAccountID = {0}", hdnID.Value))[0];
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            txtGLAccountNo.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lst = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.GLACCOUNT_TYPE));
            lst.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });
            Methods.SetComboBoxField<StandardCode>(cboGCGLAccountType, lst, "StandardCodeName", "StandardCodeID");
            cboGCGLAccountType.SelectedIndex = 0;

            List<Variable> lstPosition = new List<Variable>();
            lstPosition.Add(new Variable { Code = "D", Value = GetLabel("Debit") });
            lstPosition.Add(new Variable { Code = "K", Value = GetLabel("Kredit") });
            Methods.SetRadioButtonListField<Variable>(rblPosition, lstPosition, "Value", "Code");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtGLAccountNo, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtGLAccountName, new ControlEntrySetting(true, true, true));
            
            SetControlEntrySetting(hdnParentAccountID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtParentAccountNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtParentAccountName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(cboGCGLAccountType, new ControlEntrySetting(true, true, false));

            SetControlEntrySetting(hdnSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSubLedgerCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtSubLedgerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(rblPosition, new ControlEntrySetting(true, true, true, "D"));
            SetControlEntrySetting(txtAccountLevel, new ControlEntrySetting(true, true, true, "0"));

            SetControlEntrySetting(chkIsHeader, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(chkIsUsingDocumentControl, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(vChartOfAccount entity)
        {
            txtGLAccountNo.Text = entity.GLAccountNo;
            txtGLAccountName.Text = entity.GLAccountName;

            hdnParentAccountID.Value = entity.ParentGLAccount.ToString();
            txtParentAccountNo.Text = entity.ParentGLAccountNo;
            txtParentAccountName.Text = entity.ParentGLAccountName;
            cboGCGLAccountType.Value = entity.GCGLAccountType;

            hdnSubLedgerID.Value = entity.SubLedgerID.ToString();
            txtSubLedgerCode.Text = entity.SubLedgerCode;
            txtSubLedgerName.Text = entity.SubLedgerName;

            rblPosition.SelectedValue = entity.Position;
            chkIsHeader.Checked = entity.IsHeader;
            txtAccountLevel.Text = entity.AccountLevel.ToString();
            chkIsUsingDocumentControl.Checked = entity.IsUsingDocumentControl;
        }

        private void ControlToEntity(ChartOfAccount entity)
        {
            entity.GLAccountNo = txtGLAccountNo.Text;
            entity.GLAccountName = txtGLAccountName.Text;

            if (hdnParentAccountID.Value != "" && hdnParentAccountID.Value != "0")
                entity.ParentGLAccount = Convert.ToInt32(hdnParentAccountID.Value);
            else
                entity.ParentGLAccount = null;
            if (cboGCGLAccountType.Value != null && cboGCGLAccountType.Value.ToString() != "")
                entity.GCGLAccountType = cboGCGLAccountType.Value.ToString();
            else
                entity.GCGLAccountType = null;
            if (hdnSubLedgerID.Value != "" && hdnSubLedgerID.Value != "0")
                entity.SubLedgerID = Convert.ToInt32(hdnSubLedgerID.Value);
            else
                entity.SubLedgerID = null;
            entity.Position = rblPosition.SelectedValue;
            entity.IsHeader = chkIsHeader.Checked;
            entity.AccountLevel = Convert.ToInt16(txtAccountLevel.Text);
            entity.IsUsingDocumentControl = chkIsUsingDocumentControl.Checked;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("GLAccountNo = '{0}'", txtGLAccountNo.Text);
            List<ChartOfAccount> lst = BusinessLayer.GetChartOfAccountList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " GLAccountNo With Code " + txtGLAccountNo.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("GLAccountNo = '{0}' AND GLAccountID != {1}", txtGLAccountNo.Text, hdnID.Value);
            List<ChartOfAccount> lst = BusinessLayer.GetChartOfAccountList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " General Ledger With Code " + txtGLAccountNo.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            ChartOfAccountDao entityDao = new ChartOfAccountDao(ctx);
            bool result = false;
            try
            {
                ChartOfAccount entity = new ChartOfAccount();
                ControlToEntity(entity);
                entity.SiteID = AppSession.UserLogin.SiteID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetChartOfAccountMaxID(ctx).ToString();
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
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

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            try
            {
                ChartOfAccount entity = BusinessLayer.GetChartOfAccount(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateChartOfAccount(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
    }
}