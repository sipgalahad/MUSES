using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class TransactionNumberingEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.TRANSACTION_NUMBERING;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                TransactionType entity = BusinessLayer.GetTransactionType(ID);
                OnSetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
            }
            txtTransactionName.Focus();
        }

        private void OnSetControlProperties()
        {
            cboGCNumberingMethod.Items.Add("Daily", "01");
            cboGCNumberingMethod.Items.Add("Monthly", "02");
            cboGCNumberingMethod.Items.Add("Yearly", "03");
            cboGCNumberingMethod.Items.Add("Continously", "04");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtTransactionCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtTransactionName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtTransactionInitial, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboGCNumberingMethod, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtCounterDigit, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(chkByDepartment, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(chkByNeedApproval, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(chkInventoryTransaction, new ControlEntrySetting(true, true, false));
            
        }

        private void EntityToControl(TransactionType entity)
        {
            txtTransactionCode.Text = entity.TransactionCode;
            txtTransactionName.Text = entity.TransactionName;
            txtTransactionInitial.Text = entity.TransactionInitial;
            cboGCNumberingMethod.Value = entity.NumberingMethod;
            txtCounterDigit.Text = entity.CounterDigit.ToString();
            chkByDepartment.Checked = entity.IsByDepartment;
            chkByNeedApproval.Checked = entity.IsNeedApproval;
            chkInventoryTransaction.Checked = entity.IsInventoryTransaction;
        }

        private void ControlToEntity(TransactionType entity)
        {
            entity.TransactionCode = txtTransactionCode.Text;
            entity.TransactionName = txtTransactionName.Text;
            entity.TransactionInitial = txtTransactionInitial.Text;
            entity.NumberingMethod = cboGCNumberingMethod.Value.ToString();
            entity.CounterDigit = Convert.ToInt16(txtCounterDigit.Text);
            entity.IsByDepartment = chkByDepartment.Checked;
            entity.IsNeedApproval = chkByNeedApproval.Checked;
            entity.IsInventoryTransaction = chkInventoryTransaction.Checked;
        }

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            try
            {
                TransactionType entity = BusinessLayer.GetTransactionType(hdnID.Value);
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateTransactionType(entity);
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