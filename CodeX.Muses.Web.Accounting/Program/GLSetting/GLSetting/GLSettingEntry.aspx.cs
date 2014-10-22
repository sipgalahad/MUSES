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
    public partial class GLSettingEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Accounting.GL_SETTING;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                hdnID.Value = Request.QueryString["id"];
                vGLSetting entity = BusinessLayer.GetvGLSettingList(String.Format("ID = {0}", hdnID.Value))[0];
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
            }
            txtGLSettingCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtGLSettingCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtGLSettingName, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));

            #region Pengaturan Perkiraan
            SetControlEntrySetting(hdnGLAccountID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSearchDialogTypeName, new ControlEntrySetting(true, true));
            SetControlEntrySetting(hdnSubLedgerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtGLAccountNo, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtGLAccountName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblSubLedger, new ControlEntrySetting(false, false));
            SetControlEntrySetting(hdnSubLedger, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtSubLedgerCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtSubLedgerName, new ControlEntrySetting(false, false, false));
            #endregion
        }

        private void EntityToControl(vGLSetting entity)
        {
            txtGLSettingCode.Text = entity.GLSettingCode;
            txtGLSettingName.Text = entity.GLSettingName;
            txtRemarks.Text = entity.Remarks;

            #region Pengaturan Perkiraan
            #region 
            hdnGLAccountID.Value = entity.GLAccount.ToString();
            txtGLAccountNo.Text = entity.GLAccountNo;
            txtGLAccountName.Text = entity.GLAccountName;
            hdnSubLedgerID.Value = entity.SubLedgerID.ToString();
            hdnSearchDialogTypeName.Value = entity.SearchDialogTypeName;
            hdnIDFieldName.Value = entity.IDFieldName;
            hdnCodeFieldName.Value = entity.CodeFieldName;
            hdnDisplayFieldName.Value = entity.DisplayFieldName;
            hdnMethodName.Value = entity.MethodName;
            hdnFilterExpression.Value = entity.FilterExpression;

            hdnSubLedger.Value = entity.SubLedger.ToString();
            txtSubLedgerCode.Text = entity.SubLedgerCode.ToString();
            txtSubLedgerName.Text = entity.SubLedgerName.ToString();
            #endregion
            #endregion
        }

        private void ControlToEntity(GLSetting entity)
        {
            entity.GLSettingCode = txtGLSettingCode.Text;
            entity.GLSettingName = txtGLSettingName.Text;
            entity.Remarks = txtRemarks.Text;

            #region Pengaturan Perkiraan
            #region
            entity.GLAccount = Convert.ToInt32(hdnGLAccountID.Value);
            if (hdnSubLedger.Value != "" && hdnSubLedger.Value != "0")
                entity.SubLedger = Convert.ToInt32(hdnSubLedger.Value);
            else
                entity.SubLedger = null;
            #endregion
            #endregion
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            GLSettingDao GLSettingDao = new GLSettingDao(ctx);
            bool result = true;
            try
            {
                GLSetting entity = new GLSetting();
                ControlToEntity(entity);
                entity.LastUpdatedBy = entity.CreatedBy = AppSession.UserLogin.UserID;
                GLSettingDao.Insert(entity);
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

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            GLSettingDao GLSettingDao = new GLSettingDao(ctx);
            bool result = true;
            try
            {
                GLSetting entity = GLSettingDao.Get(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                GLSettingDao.Update(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                ctx.RollBackTransaction();
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
    }
}