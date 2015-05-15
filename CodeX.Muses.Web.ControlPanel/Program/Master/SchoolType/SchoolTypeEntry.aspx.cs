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
    public partial class SchoolTypeEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.SCHOOL_UNIT;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                StandardCode entity = BusinessLayer.GetStandardCode(ID);
                SetControlProperties();
                hdnParentID.Value = entity.ParentID;
                EntityToControl(entity);
            }
            else
            {
                hdnParentID.Value = Constant.StandardCode.SCHOOL_TYPE;
                SetControlProperties();
                IsAdd = true;
            }
            txtStandardCodeID.Focus();
        }
        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtStandardCodeID, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtStandardCodeName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtTagProperty, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtNotes, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(StandardCode entity)
        {
            txtStandardCodeID.Text = entity.StandardCodeID.Split('^')[1];
            txtStandardCodeName.Text = entity.StandardCodeName;
            txtTagProperty.Text = entity.TagProperty;
            txtNotes.Text = entity.Notes;
        }

        private void ControlToEntity(StandardCode entity)
        {
            entity.StandardCodeName = txtStandardCodeName.Text;
            entity.TagProperty = txtTagProperty.Text;
            entity.Notes = txtNotes.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("StandardCodeID = '{0}^{1}'", hdnParentID.Value, txtStandardCodeID.Text);
            List<StandardCode> lst = BusinessLayer.GetStandardCodeList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Standard Code with ID " + txtStandardCodeID.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            try
            {
                StandardCode entity = new StandardCode();
                ControlToEntity(entity);
                entity.StandardCodeID = String.Format("{0}^{1}", hdnParentID.Value, txtStandardCodeID.Text);
                entity.ParentID = hdnParentID.Value;
                entity.IsEditableByUser = true;
                entity.IsHeader = false;
                entity.IsDefault = false;
                entity.IsActive = true;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertStandardCode(entity);

                retval = entity.StandardCodeID;
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            try
            {
                string StandardCodeID = string.Format("{0}^{1}", hdnParentID.Value, txtStandardCodeID.Text);
                StandardCode entity = BusinessLayer.GetStandardCode(StandardCodeID);
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateStandardCode(entity);
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