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

namespace Codex.Muses.Web.Accounting.Program
{
    public partial class SubLedgerTypeEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Accounting.SUB_LEDGER_TYPE;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                hdnID.Value = Request.QueryString["id"];
                SubLedgerType entity = BusinessLayer.GetSubLedgerTypeList(string.Format("SubLedgerTypeID = {0}", hdnID.Value))[0];
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            txtSubLedgerTypeCode.Focus();
        }

        protected override void SetControlProperties()
        {
            //List<StandardCode> lst = BusinessLayer.GetStandardCodeList(String.Format("ParentID IN ('{0}') AND IsActive = 1 AND IsDeleted = 0",Constant.StandardCode.GLACCOUNT_TYPE));
            //Methods.SetComboBoxField<StandardCode>(cboGCGLAccountType, lst.Where(x => x.ParentID == Constant.StandardCode.GLACCOUNT_TYPE).ToList(), "StandardCodeName", "StandardCodeID");
            //cboGCGLAccountType.SelectedIndex = 0;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtSubLedgerTypeCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtSubLedgerTypeName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtMethodName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtFilterExpression, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtIDFieldName, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtCodeFieldName, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtDisplayFieldName, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtSearchDialogTypeName, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtTableName, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(SubLedgerType entity)
        {
            txtSubLedgerTypeCode.Text = entity.SubLedgerTypeCode;
            txtSubLedgerTypeName.Text = entity.SubLedgerTypeName;
            txtMethodName.Text = entity.MethodName;
            txtFilterExpression.Text = entity.FilterExpression;
            txtIDFieldName.Text = entity.IDFieldName;
            txtCodeFieldName.Text = entity.CodeFieldName;
            txtDisplayFieldName.Text = entity.DisplayFieldName;
            txtSearchDialogTypeName.Text = entity.SearchDialogTypeName;
            txtTableName.Text = entity.TableName;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(SubLedgerType entity)
        {
            entity.SubLedgerTypeCode = txtSubLedgerTypeCode.Text;
            entity.SubLedgerTypeName = txtSubLedgerTypeName.Text;
            entity.MethodName = txtMethodName.Text;
            entity.FilterExpression = txtFilterExpression.Text;
            entity.IDFieldName = txtIDFieldName.Text;
            entity.CodeFieldName = txtCodeFieldName.Text;
            entity.DisplayFieldName = txtDisplayFieldName.Text;
            entity.SearchDialogTypeName = txtSearchDialogTypeName.Text;
            entity.TableName = txtTableName.Text;
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("SubLedgerTypeCode = '{0}'", txtSubLedgerTypeCode.Text);
            List<SubLedgerType> lst = BusinessLayer.GetSubLedgerTypeList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " SubLedgerType With Code " + txtSubLedgerTypeCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("SubLedgerTypeCode = '{0}' AND SubLedgerTypeID != {1}", txtSubLedgerTypeCode.Text, hdnID.Value);
            List<SubLedgerType> lst = BusinessLayer.GetSubLedgerTypeList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " SubLedgerType With Code " + txtSubLedgerTypeCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            SubLedgerTypeDao entityDao = new SubLedgerTypeDao(ctx);
            bool result = false;
            try
            {
                SubLedgerType entity = new SubLedgerType();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
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
                SubLedgerType entity = BusinessLayer.GetSubLedgerType(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSubLedgerType(entity);
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