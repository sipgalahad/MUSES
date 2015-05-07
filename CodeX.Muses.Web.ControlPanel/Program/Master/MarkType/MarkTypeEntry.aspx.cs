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
using System.Globalization;
using DevExpress.Web.ASPxEditors;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class MarkTypeEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.MARK_TYPE;
        }

        public String GetMarkTypeNumber() 
        {
            return Constant.MarkType.NUMBER;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                SetControlProperties();
                MarkTypeHd entity = BusinessLayer.GetMarkTypeHd(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            txtMarkTypeCode.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsDeleted = 0 AND IsActive = 1", Constant.StandardCode.SUBJECT_MARK_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboMarkType, lstStandardCode, "StandardCodeName", "StandardCodeID");

        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtMarkTypeCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtMarkTypeName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboMarkType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtMinValue, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(txtMaxValue, new ControlEntrySetting(true, true, true, "0"));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(MarkTypeHd entity)
        {
            txtMarkTypeCode.Text = entity.MarkTypeCode;
            txtMarkTypeName.Text = entity.MarkTypeName;
            cboMarkType.Value = entity.GCMarkType;
            txtMinValue.Text = entity.MinValue.ToString();
            txtMaxValue.Text = entity.MaxValue.ToString();
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(MarkTypeHd entity)
        {
            entity.MarkTypeCode = txtMarkTypeCode.Text;
            entity.MarkTypeName = txtMarkTypeName.Text;
            entity.GCMarkType = cboMarkType.Value.ToString();
            entity.Remarks = txtRemarks.Text;
            if (entity.GCMarkType == Constant.MarkType.NUMBER)
            {
                entity.MinValue = Convert.ToDecimal(txtMinValue.Text);
                entity.MaxValue = Convert.ToDecimal(txtMaxValue.Text);
            }
            else
            {
                entity.MinValue = 0;
                entity.MaxValue = 0;
            }
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("MarkTypeCode = '{0}'", txtMarkTypeCode.Text);
            List<MarkTypeHd> lst = BusinessLayer.GetMarkTypeHdList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Mark Type With Code " + txtMarkTypeCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("MarkTypeCode = '{0}' AND MarkTypeID != {1}", txtMarkTypeCode.Text, hdnID.Value);
            List<MarkTypeHd> lst = BusinessLayer.GetMarkTypeHdList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Mark Type With Code " + txtMarkTypeCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            MarkTypeHdDao entityDao = new MarkTypeHdDao(ctx);
            bool result = false;
            try
            {
                MarkTypeHd entity = new MarkTypeHd();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetMarkTypeHdMaxID(ctx).ToString();
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
                MarkTypeHd entity = BusinessLayer.GetMarkTypeHd(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateMarkTypeHd(entity);
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