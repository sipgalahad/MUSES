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

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class ReportConfigurationEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.REPORT_CONFIGURATION;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                SetControlProperties();
                ReportMaster entity = BusinessLayer.GetReportMaster(Convert.ToInt32(ID));
                EntityToControl(entity);
                if (entity.ParentID != null && entity.ParentID > 0)
                {
                    ReportMaster entityParent = BusinessLayer.GetReportMaster((int)entity.ParentID);
                    txtParentCode.Text = entityParent.ReportCode;
                    txtParentName.Text = entityParent.ReportName;
                }
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            txtReportCode.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lst = BusinessLayer.GetStandardCodeList(String.Format("ParentID IN ('{0}') AND IsDeleted = 0", Constant.StandardCode.REPORT_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboReportType, lst.Where(p => p.ParentID == Constant.StandardCode.REPORT_TYPE).ToList(), "StandardCodeName", "StandardCodeID");
            cboReportType.SelectedIndex = 0;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtReportCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtReportName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboReportType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtReportUrl, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(hdnParentID, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtParentCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtParentName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(chkIsHeader, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(ReportMaster entity)
        {
            txtReportCode.Text = entity.ReportCode;
            txtReportName.Text = entity.ReportName;
            cboReportType.Value = entity.GCReportType;
            txtReportUrl.Text = entity.ReportUrl;
            chkIsHeader.Checked = entity.IsHeader;
            hdnParentID.Value = entity.ParentID.ToString();
            chkIsHeader.Checked = entity.IsHeader;
        }

        private void ControlToEntity(ReportMaster entity)
        {
            entity.ReportCode = txtReportCode.Text;
            entity.ReportName = txtReportName.Text;
            entity.GCReportType = cboReportType.Value.ToString();
            entity.ReportUrl = txtReportUrl.Text;
            if (hdnParentID.Value == "" || hdnParentID.Value.ToString() == "0")
                entity.ParentID = null;
            else
                entity.ParentID = Convert.ToInt32(hdnParentID.Value);
            entity.IsHeader = chkIsHeader.Checked;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("ReportCode = '{0}'", txtReportCode.Text);
            List<ReportMaster> lst = BusinessLayer.GetReportMasterList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Report With Code " + txtReportCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("ReportCode = '{0}' AND ReportID != {1}", txtReportCode.Text, hdnID.Value);
            List<ReportMaster> lst = BusinessLayer.GetReportMasterList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Report With Code " + txtReportCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            ReportMasterDao entityDao = new ReportMasterDao(ctx);
            bool result = false;
            try
            {
                ReportMaster entity = new ReportMaster();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetReportMasterMaxID(ctx).ToString();
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
                ReportMaster entity = BusinessLayer.GetReportMaster(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateReportMaster(entity);
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